using System.Web.Mvc;

namespace FinanceCalculator.Models
{
    public class RefinancingCalculatorVM
    {
        public RefinancingCalcParamsVM Params { get; set; }
        public RefinancingCalcResultsVM Result { get; set; }

        public bool IsModelValid(ModelStateDictionary dic)
        {
            if ((Params.CurrentCreditAmount ?? 0) <= 0 || (Params.CurrentCreditAmount ?? 0) > 99999999)
            {
                dic.AddModelError("", "Моля въведете размер на кредита по-голям от нула.");
            }
            if ((Params.CurrentCreditRate ?? 0) <= 0 || (Params.CurrentCreditRate ?? 0) > 99)
            {
                dic.AddModelError("", "Моля въведете размер на лихвата по-голяма от нула и по-малка от 100.");
            }
            if ((Params.CurrentCreditPeriod ?? 0) <= 0 || (Params.CurrentCreditPeriod ??0) > 999)
            {
                dic.AddModelError("", "Моля въведете коректно число за срок на кредита (до 999 месеца).");
            }

            if ((Params.CurrentCreditMadeInstallments ?? 0) <= 0 || (Params.CurrentCreditMadeInstallments ?? 0) >= (Params.CurrentCreditPeriod ?? 0))
            {
                dic.AddModelError("", "Моля въведете коректно число за брой на направените вноски (техния брой трябва да е по-голям от нула и по-малък от срока на кредита).");
            }
            if (!Params.CurrentCreditPreTermFee.HasValue || (Params.CurrentCreditPreTermFee ?? 0) < 0 || (Params.CurrentCreditPreTermFee ?? 0) >= (Params.CurrentCreditRate ?? 0))
            {
                dic.AddModelError("", "Моля въведете коректен размер на такса за предсрочно погасяване (по-голям или равен на нула и по-малък от размера на лихвата)");
            }
            if ((Params.CurrentCreditRate ?? 0) <= 0 || (Params.CurrentCreditRate ?? 0) > 99)
            {
                dic.AddModelError("", "Моля въведете коректно число за лихва на новия кредит (по-голям от нула и по-малък от 99)");
            }
            if (!Params.NewCreditInitialFeesPercent.HasValue || ((Params.NewCreditInitialFeesPercent ?? 0) < 0 || (Params.NewCreditInitialFeesPercent ?? 0) >= 100000000))
            {
                dic.AddModelError("", "Моля въведете коректно число за първоначални такси (%), което да е по-голямо от нула.");
            }
            if (!Params.NewCreditInitialFeesCurrency.HasValue || ((Params.NewCreditInitialFeesCurrency ?? 0) < 0 || (Params.NewCreditInitialFeesCurrency ?? 0) >= 100000000))
            {
                dic.AddModelError("", "Моля въведете коректно число за първоначални такси (валута), което да е по-голямо от нула.");
            }
            return dic.IsValid;
        }
    }
}