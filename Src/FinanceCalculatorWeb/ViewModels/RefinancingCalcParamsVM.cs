using System.Web.Mvc;

namespace FinanceCalculator.Web.ViewModels
{
    public class RefinancingCalcParamsVM
    {
        /// <summary>
        /// Настоящ кредит Размер на кредита
        /// </summary>
        public decimal? CurrentCreditAmount { get; set; }
        /// <summary>
        /// Настоящ кредит Лихва (%)
        /// </summary>
        public decimal? CurrentCreditRate { get; set; }
        /// <summary>
        /// Настоящ кредит Срок на кредита (месеци) 
        /// </summary>
        public int? CurrentCreditPeriod { get; set; }
        /// <summary>
        /// Настоящ кредит Брой на направените вноски
        /// </summary>
        public int? CurrentCreditMadeInstallments { get; set; }
        /// <summary>
        /// Настоящ кредит Такса за предсрочно погасяване (%)
        /// </summary>
        public decimal? CurrentCreditPreTermFee { get; set; }
        /// <summary>
        /// Нов кредит Лихва
        /// </summary>
        public decimal? NewCreditRate { get; set; }
        /// <summary>
        /// Нов кредит Първоначални такси (%)
        /// </summary>
        public decimal? NewCreditInitialFeesPercent { get; set; }
        /// <summary>
        /// Нов кредит Първоначални такси (валута)
        /// </summary>
        public decimal? NewCreditInitialFeesCurrency { get; set; }

        public bool IsModelValid(ModelStateDictionary dic)
        {
            if ((this.CurrentCreditAmount ?? 0) <= 0 || (this.CurrentCreditAmount ?? 0) > 99999999)
            {
                dic.AddModelError("", "Моля въведете размер на кредита по-голям от нула.");
            }
            if ((this.CurrentCreditRate ?? 0) <= 0 || (this.CurrentCreditRate ?? 0) > 99)
            {
                dic.AddModelError("", "Моля въведете размер на лихвата по-голяма от нула и по-малка от 100.");
            }
            if ((this.CurrentCreditPeriod ?? 0) <= 0 || (this.CurrentCreditPeriod ?? 0) > 999)
            {
                dic.AddModelError("", "Моля въведете коректно число за срок на кредита (до 999 месеца).");
            }

            if ((this.CurrentCreditMadeInstallments ?? 0) <= 0 || (this.CurrentCreditMadeInstallments ?? 0) >= (this.CurrentCreditPeriod ?? 0))
            {
                dic.AddModelError("", "Моля въведете коректно число за брой на направените вноски (техния брой трябва да е по-голям от нула и по-малък от срока на кредита).");
            }
            if (!this.CurrentCreditPreTermFee.HasValue || (this.CurrentCreditPreTermFee ?? 0) < 0 || (this.CurrentCreditPreTermFee ?? 0) >= (this.CurrentCreditRate ?? 0))
            {
                dic.AddModelError("", "Моля въведете коректен размер на такса за предсрочно погасяване (по-голям или равен на нула и по-малък от размера на лихвата)");
            }
            if ((this.CurrentCreditRate ?? 0) <= 0 || (this.CurrentCreditRate ?? 0) > 99)
            {
                dic.AddModelError("", "Моля въведете коректно число за лихва на новия кредит (по-голям от нула и по-малък от 99)");
            }
            if (!this.NewCreditInitialFeesPercent.HasValue || ((this.NewCreditInitialFeesPercent ?? 0) < 0 || (this.NewCreditInitialFeesPercent ?? 0) >= 100000000))
            {
                dic.AddModelError("", "Моля въведете коректно число за първоначални такси (%), което да е по-голямо от нула.");
            }
            if (!this.NewCreditInitialFeesCurrency.HasValue || ((this.NewCreditInitialFeesCurrency ?? 0) < 0 || (this.NewCreditInitialFeesCurrency ?? 0) >= 100000000))
            {
                dic.AddModelError("", "Моля въведете коректно число за първоначални такси (валута), което да е по-голямо от нула.");
            }
            return dic.IsValid;
        }
    }
}