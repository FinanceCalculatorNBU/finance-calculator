using System.Web.Mvc;
namespace FinanceCalculator.Web.ViewModels
{
    public class LeasingCalcParamsVM
    {
        public LeasingCalcParamsVM()
        {
            this.TreatInitialManagementFeeAsPercent = true;
        }

        /// <summary>
        /// Цена на стоката с ДДС 
        /// </summary>
        public decimal? Price { get; set; }
        /// <summary>
        /// Първоначална вноска
        /// </summary>
        public decimal? InitialInstallment { get; set; }
        /// <summary>
        /// Период на лизинга (месеци)
        /// </summary>
        public int? Period { get; set; }
        /// <summary>
        /// Месечна вноска
        /// </summary>
        public decimal? MonthlyInstallment { get; set; }
        /// <summary>
        /// Първоначална такса за обработка валута
        /// </summary>
        public decimal? InitialManagementFee { get; set; }
        /// <summary>
        /// Указва дали Първоначална такса за обработка е процент
        /// </summary>
        public bool TreatInitialManagementFeeAsPercent { get; set; }

        public bool IsModelValid(ModelStateDictionary dic)
        {
            if (!this.Price.HasValue && ((this.Price) <= 100 || (this.Price) >= 100000000))
            {
                dic.AddModelError("", "Моля въведете размер на цена на стоката по-голям от сто и по-малко от 100 000 000.");
            }
            if (!this.InitialInstallment.HasValue || ((this.InitialInstallment) < 0 || (this.InitialInstallment) > 0.9M * (this.Price.Value)))
            {
                dic.AddModelError("", "Моля въведете размер на лихвата по-голяма от нула и по-малка от 100.");
            }
            if ((this.Period ?? 0) <= 0 || (this.Period) > 120)
            {
                dic.AddModelError("", "Моля въведете коректно число за период на лизинга (до 120 месеца).");
            }

            if (((this.MonthlyInstallment ?? 0) <= 0) || (this.MonthlyInstallment >= this.Price))
            {
                dic.AddModelError("", "Моля въведете коректно число за месечна вноска (по-голямо от нула и по-малко от цената на стоката).");
            }
            if (this.InitialManagementFee.HasValue)
            {
                if (this.TreatInitialManagementFeeAsPercent && (this.InitialManagementFee < 0 || this.InitialManagementFee >= 49))
                    dic.AddModelError("", "Моля въведете коректно число за такса кандидатстване.");
                else if (this.InitialManagementFee < 0 || this.InitialManagementFee > (this.Price.Value - this.InitialInstallment.Value) / 2)
                    dic.AddModelError("", "Моля въведете коректно число за такса кандидатстване.");
            }
            if ((this.MonthlyInstallment ?? 0) * (this.Period ?? 0) + (this.InitialInstallment ?? 0) < (this.Price ?? 0))
            {
                dic.AddModelError("", "Сумата на месечните вноски не покрива задълженията по лизинга.");
            }
            return dic.IsValid;
        }
    }
}