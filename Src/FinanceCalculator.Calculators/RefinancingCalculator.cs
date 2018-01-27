using System;
using FinanceCalculator.Models;
using FinanceCalculator.Calculators.Contracts;

namespace FinanceCalculator.Calculators
{
    public class RefinancingCalculator : IRefinancingCalculator
    {
        public RefinancingCalcResults Calculate(RefinancingCalcParams p)
        {
            IsParamsValid(p);
            RefinancingCalcResults res = new RefinancingCalcResults();
            res.CurrRate = p.CurrentCreditRate.Value;
            res.NewRate = p.NewCreditRate.Value;
            res.CurrPeriod = p.CurrentCreditPeriod.Value;
            res.NewPeriod = p.CurrentCreditPeriod.Value - p.CurrentCreditMadeInstallments.Value;

            decimal principalInstallments = p.CurrentCreditAmount.Value - GetPrincipalInstalments(p.CurrentCreditAmount.Value, p.CurrentCreditPeriod.Value, p.CurrentCreditMadeInstallments.Value, p.CurrentCreditRate.Value);
            res.CurrPreTermFee = (p.CurrentCreditAmount.Value - principalInstallments) * p.CurrentCreditPreTermFee.Value / 100;
            res.CurrMonthlyInstallment = PMT(p.CurrentCreditRate.Value, p.CurrentCreditPeriod.Value, p.CurrentCreditAmount.Value);
            int newCreditPeriod = (p.CurrentCreditPeriod.Value - p.CurrentCreditMadeInstallments.Value);
            res.CurrTotalPaid = Math.Round(PMT(p.CurrentCreditRate.Value, p.CurrentCreditPeriod.Value, p.CurrentCreditAmount.Value) * newCreditPeriod, 2);
            res.NewMonthlyInstallment = PMT(p.NewCreditRate.Value, newCreditPeriod, p.CurrentCreditAmount.Value - principalInstallments);

            decimal newCreditFees = getFeeAmount(p.CurrentCreditAmount.Value, p.NewCreditInitialFeesPercent, true) + p.NewCreditInitialFeesCurrency.Value;
            decimal newCreditToBePaid = res.NewMonthlyInstallment * newCreditPeriod;
            res.NewTotalPaid = newCreditToBePaid + newCreditFees + res.CurrPreTermFee;

            res.CurrPreTermFee = Math.Round(res.CurrPreTermFee, 2);
            res.NewTotalPaid = Math.Round(res.NewTotalPaid, 2);
            res.NewMonthlyInstallment = Math.Round(res.NewMonthlyInstallment, 2);
            res.CurrMonthlyInstallment = Math.Round(res.CurrMonthlyInstallment, 2);
            return res;
        }

        public decimal PMT(decimal yearlyInterestRate, int totalNumberOfMonths, decimal loanAmount)
        {
            var rate = (decimal)yearlyInterestRate / 100 / 12;
            var denominator = (decimal)Math.Pow((1 + (double)rate), totalNumberOfMonths) - 1;
            return (rate + (rate / denominator)) * loanAmount;
        }

        private decimal getFeeAmount(decimal fromAmount, decimal? fee, bool percent)
        {
            if (!percent) return fee ?? 0;
            return fromAmount * (fee ?? 0) / 100;
        }

        private decimal GetPrincipalInstalments(decimal amount, int period, int numberOfMonths, decimal rate)
        {
            decimal installment = PMT(rate, period, amount);
            decimal principalInstallmentSum = 0;
            decimal principalRemainder = amount;
            for (int i = 1; i <= numberOfMonths; i++)
            {
                decimal rateInstallment = (decimal)(principalRemainder * rate / 100) / 12;
                decimal principalInstallment = installment - rateInstallment;
                principalInstallmentSum += principalInstallment;
                principalRemainder -= principalInstallment;
            }
            return principalRemainder;
        }

        public void IsParamsValid(RefinancingCalcParams Params)
        {
            if ((Params.CurrentCreditAmount) <= 0 || (Params.CurrentCreditAmount) > 99999999)
            {
                throw new ArgumentException("Моля въведете размер на кредита по-голям от нула и по-малко от 1 000 000 000.");
            }
            if ((Params.CurrentCreditRate) <= 0 || (Params.CurrentCreditRate) > 99)
            {
                throw new ArgumentException("Моля въведете размер на лихвата по-голяма от нула и по-малка от 100.");
            }
            if ((Params.CurrentCreditPeriod) <= 0 || (Params.CurrentCreditPeriod) > 999)
            {
                throw new ArgumentException("Моля въведете коректно число за срок на кредита (до 999 месеца).");
            }
            if ((Params.CurrentCreditMadeInstallments <= 0) || (Params.CurrentCreditMadeInstallments >= Params.CurrentCreditPeriod))
            {
                throw new ArgumentException("Моля въведете коректно число за брой на направените вноски (техния брой трябва да е по-голям от нула и по-малък от срока на кредита).");
            }
            if (!Params.CurrentCreditPreTermFee.HasValue || (Params.CurrentCreditPreTermFee < 0) || (Params.CurrentCreditPreTermFee >= Params.CurrentCreditRate))
            {
                throw new ArgumentException("Моля въведете коректен размер на такса за предсрочно погасяване (по-голям или равен на нула и по-малък от размера на лихвата)");
            }
            if ((Params.CurrentCreditRate) <= 0 || (Params.CurrentCreditRate) > 99)
            {
                throw new ArgumentException("Моля въведете коректно число за лихва на новия кредит (по-голям от нула и по-малък от 99)");
            }
            if (!Params.NewCreditInitialFeesPercent.HasValue || ((Params.NewCreditInitialFeesPercent ?? 0) < 0 || (Params.NewCreditInitialFeesPercent ?? 0) >= 100000000))
            {
                throw new ArgumentException("Моля въведете коректно число за първоначални такси (%), което да е по-голямо от нула.");
            }
            if (!Params.NewCreditInitialFeesCurrency.HasValue || ((Params.NewCreditInitialFeesCurrency ?? 0) < 0 || (Params.NewCreditInitialFeesCurrency ?? 0) >= 100000000))
            {
                throw new ArgumentException("Моля въведете коректно число за първоначални такси (валута), което да е по-голямо от нула.");
            }
        }
    }
}
