using System.Web.Mvc;

namespace FinanceCalculator.Models
{
    public class LeasingCalculatorVM
    {
        public LeasingCalcParamsVM Params { get; set; }
        public LeasingCalcResultsVM Result { get; set; }
        public bool IsModelValid(ModelStateDictionary dic)
        {
            if (!Params.Price.HasValue && ((Params.Price) <= 100 || (Params.Price) >= 100000000))
            {
                dic.AddModelError("", "Моля въведете размер на цена на стоката по-голям от сто и по-малко от 100 000 000.");
            }
            if (!Params.InitialInstallment.HasValue || ((Params.InitialInstallment) < 0 || (Params.InitialInstallment) > 0.9M * (Params.Price.Value)))
            {
                dic.AddModelError("", "Моля въведете размер на лихвата по-голяма от нула и по-малка от 100.");
            }
            if ((Params.Period ?? 0) <= 0 || (Params.Period) > 120)
            {
                dic.AddModelError("", "Моля въведете коректно число за период на лизинга (до 120 месеца).");
            }

            if (((Params.MonthlyInstallment ?? 0) <= 0) || (Params.MonthlyInstallment >= Params.Price))
            {
                dic.AddModelError("", "Моля въведете коректно число за месечна вноска (по-голямо от нула и по-малко от цената на стоката).");
            }
            if (Params.InitialManagementFee.HasValue)
            {
                if (Params.TreatInitialManagementFeeAsPercent && (Params.InitialManagementFee < 0 || Params.InitialManagementFee >= 49))
                    dic.AddModelError("", "Моля въведете коректно число за такса кандидатстване.");
                else if (Params.InitialManagementFee < 0 || Params.InitialManagementFee > (Params.Price.Value - Params.InitialInstallment.Value) / 2)
                    dic.AddModelError("", "Моля въведете коректно число за такса кандидатстване.");
            }
            if ((Params.MonthlyInstallment ?? 0) * (Params.Period ?? 0) + (Params.InitialInstallment ?? 0) < (Params.Price ?? 0))
            {
                dic.AddModelError("", "Сумата на месечните вноски не покрива задълженията по лизинга.");
            }
            return dic.IsValid;
        }
    }
}