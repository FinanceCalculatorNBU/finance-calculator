using System;
using FinanceCalculator.Models;
using FinanceCalculator.Calculators.Contracts;

namespace FinanceCalculator.Calculators
{
    public class LeasingCalculator : ILeasingCalculator
    {
        public LeasingCalcResults Calculate(LeasingCalcParams p)
        {
            IsParamsValid(p);
            LeasingCalcResults res = new LeasingCalcResults();
            decimal loanAmount = (decimal)p.Price - (decimal)p.InitialInstallment;
            APRCalculator aprCalc = new APRCalculator(loanAmount);
            decimal initialFee = GetFeeAmount(p.Price.Value, p.InitialManagementFee, p.TreatInitialManagementFeeAsPercent);
            aprCalc.AddInstalment(initialFee, 0);
            for (int i = 1; i <= p.Period; i++)
            {
                aprCalc.AddInstalment(p.MonthlyInstallment.Value, 365.25M / 12M * i);
            }
            res.AnnualPercentRate = Math.Round(aprCalc.Calculate(), 2);
            res.TotalFees = Math.Round(initialFee, 2);
            res.TotalPaidWithFees = Math.Round(p.MonthlyInstallment.Value * p.Period.Value + p.InitialInstallment.Value + res.TotalFees, 2);
            return res;
        }

        private decimal GetFeeAmount(decimal fromAmount, decimal? fee, bool percent)
        {
            if (!percent) return fee ?? 0;
            return fromAmount * (fee ?? 0) / 100;
        }

        public void IsParamsValid(LeasingCalcParams Params)
        {
            if (!Params.Price.HasValue && ((Params.Price) <= 100 || (Params.Price) >= 100000000))
            {
                throw new ArgumentException("Моля въведете размер на цена на стоката по-голям от сто и по-малко от 100 000 000.");
            }
            if (!Params.InitialInstallment.HasValue || ((Params.InitialInstallment) < 0 || (Params.InitialInstallment) > 0.9M * (Params.Price.Value)))
            {
                throw new ArgumentException("Моля въведете размер на лихвата по-голяма от нула и по-малка от 100.");
            }
            if ((Params.Period ?? 0) <= 0 || (Params.Period) > 120)
            {
                throw new ArgumentException("Моля въведете коректно число за период на лизинга (до 120 месеца).");
            }

            if (((Params.MonthlyInstallment ?? 0) <= 0) || (Params.MonthlyInstallment >= Params.Price))
            {
                throw new ArgumentException("Моля въведете коректно число за месечна вноска (по-голямо от нула и по-малко от цената на стоката).");
            }
            if (Params.InitialManagementFee.HasValue)
            {
                if (Params.TreatInitialManagementFeeAsPercent && (Params.InitialManagementFee < 0 || Params.InitialManagementFee >= 49))
                    throw new ArgumentException("Моля въведете коректно число за такса кандидатстване.");
                else if (Params.InitialManagementFee < 0 || Params.InitialManagementFee > (Params.Price.Value - Params.InitialInstallment.Value) / 2)
                    throw new ArgumentException("Моля въведете коректно число за такса кандидатстване.");
            }
            if ((Params.MonthlyInstallment ?? 0) * (Params.Period ?? 0) + (Params.InitialInstallment ?? 0) < (Params.Price ?? 0))
            {
                throw new ArgumentException("Сумата на месечните вноски не покрива задълженията по лизинга.");
            }
        }
    }

}
