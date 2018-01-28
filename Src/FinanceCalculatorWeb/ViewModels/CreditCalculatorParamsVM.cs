using System.Web.Mvc;

namespace FinanceCalculator.Web.ViewModels
{
    public class CreditCalculatorParamsVM
    {
        public CreditCalculatorParamsVM()
        {
            this.IsAnnuityInstallments = true;
            this.TreatApplicationFeeAsPercent = true;
            this.TreatProcessingFeeAsPercent = true;
            this.TreatMonthlyManagementFeeAsPercent = true;
            this.TreatAnnualManagementFeeAsPercent = true;
        }

        /// <summary>
        /// Размер на кредита
        /// </summary>
        public decimal? Amount { get; set; }
        /// <summary>
        /// Срок (месеци)
        /// </summary>
        public int? Period { get; set; }
        /// <summary>
        /// Лихва (%) 
        /// </summary>
        public decimal? Rate { get; set; }
        /// <summary>
        /// Промоционален период (месеци) 
        /// </summary>
        public int? PromotionPeriod { get; set; }
        /// <summary>
        /// Промоционална лихва(%) 
        /// </summary>
        public decimal? PromotionRate { get; set; }
        /// <summary>
        /// Гратисен период (месеци) 
        /// </summary>
        public int? GratisPeriod { get; set; }
        /// <summary>
        /// Вид погасителен план
        /// </summary>
        public bool IsAnnuityInstallments { get; set; }
        /// <summary>
        /// Първоначални такси Такса кандидатстване 
        /// </summary>
        public decimal? ApplicationFee { get; set; }
        /// <summary>
        /// Указва дали такса кандидатстване е процент
        /// </summary>
        public bool TreatApplicationFeeAsPercent { get; set; }
        /// <summary>
        /// Първоначални такси Такса обработка 
        /// </summary>
        public decimal? ProcessingFee { get; set; }
        /// <summary>
        /// Указва дали такса обработка е процент
        /// </summary>
        public bool TreatProcessingFeeAsPercent { get; set; }
        /// <summary>
        /// Първоначални такси Други такси 
        /// </summary>
        public decimal? OtherInitialFees { get; set; }
        /// <summary>
        /// Указва дали Други такси е процент
        /// </summary>
        public bool TreatOtherInitialFeesAsPercent { get; set; }
        /// <summary>
        /// Годишни такси Годишна такса управление
        /// </summary>
        public decimal? AnnualManagementFee { get; set; }
        /// <summary>
        /// Указва дали Годишна такса управление е процент
        /// </summary>
        public bool TreatAnnualManagementFeeAsPercent { get; set; }
        /// <summary>
        /// Годишни такси Други годишни такси 
        /// </summary>
        public decimal? OtherAnnualFees { get; set; }
        /// <summary>
        /// Указва дали Други годишни такси е процент
        /// </summary>
        public bool TreatOtherAnnualFeesAsPercent { get; set; }
        /// <summary>
        /// Месечни такси Месечна такса управление 
        /// </summary>
        public decimal? MonthlyManagementFee { get; set; }
        /// <summary>
        /// Указва дали Месечна такса управление е процент
        /// </summary>
        public bool TreatMonthlyManagementFeeAsPercent { get; set; }
        /// <summary>
        /// Месечни такси Други месечни такси 
        /// </summary>
        public decimal? OtherMonthlyFees { get; set; }
        /// <summary>
        /// Указва дали Други месечни такси е процент
        /// </summary>
        public bool TreatOtherMonthlyFeesAsPercent { get; set; }

        public bool IsModelValid(ModelStateDictionary dic)
        {
            if ((this.Amount ?? 0) <= 0 || (this.Amount ?? 0) > 999999999)
            {
                dic.AddModelError("", "Моля въведете размер на кредита по-голям от нула и по-малко от 1 000 000 000.");
            }
            if ((this.Period ?? 0) <= 0 || (this.Period ?? 0) > 960)
            {
                dic.AddModelError("", "Моля въведете коректно число за срок (до 960 месеца).");
            }
            if ((this.Rate ?? 0) <= 0)
            {
                dic.AddModelError("", "Моля въведете размер на лихвата по-голяма от нула.");
            }
            if (this.PromotionPeriod.HasValue && (this.PromotionPeriod <= 0 || this.PromotionPeriod >= this.Period))
            {
                dic.AddModelError("", "Моля въведете коректно число за промоционален период (промоционалният период трябва да е по-голям от нула и по-малък от срока на кредита).");
            }
            if (!this.PromotionRate.HasValue && this.PromotionPeriod.HasValue)
            {
                dic.AddModelError("", "Моля въведете коректно число за промоционална лихва(въвели сте промоционален период, но не сте въвели промоционална лихва)");
            }
            if (this.PromotionRate.HasValue && !this.PromotionPeriod.HasValue)
            {
                dic.AddModelError("", "Моля въведете коректно число за промоционален период(въвели сте промоционална лихва, но не сте въвели промоционален период)");
            }
            if (this.GratisPeriod.HasValue && (this.GratisPeriod <= 0 || this.GratisPeriod >= this.Period))
            {
                dic.AddModelError("", "Моля въведете коректно число за гратисен период (гратисният период трябва да е по-малък от срока на кредита)");
            }
            if (this.ApplicationFee.HasValue)
            {
                if (this.TreatApplicationFeeAsPercent && (this.ApplicationFee <= 0 || this.ApplicationFee >= 41))
                    dic.AddModelError("", "Моля въведете коректно число за такса кандидатстване.");
                else if (this.ApplicationFee <= 0)
                    dic.AddModelError("", "Моля въведете коректно число за такса кандидатстване.");
            }
            if ((this.ApplicationFee.HasValue || this.ProcessingFee.HasValue || this.OtherInitialFees.HasValue) && this.Amount.HasValue)
            {
                if (this.getFeeAmount(this.Amount ?? 0, this.ApplicationFee ?? 0, this.TreatApplicationFeeAsPercent)
                    + this.getFeeAmount(this.Amount ?? 0, this.ProcessingFee ?? 0, this.TreatProcessingFeeAsPercent)
                    + this.getFeeAmount(this.Amount ?? 0, this.OtherInitialFees ?? 0, this.TreatOtherInitialFeesAsPercent) > (this.Amount / 2))
                {
                    dic.AddModelError("", "Първоначалните такси не трябва да надвишават 50% от размера на кредита.");
                }
            }

            if (this.AnnualManagementFee.HasValue)
            {
                if (this.TreatAnnualManagementFeeAsPercent && (this.AnnualManagementFee <= 0 || this.AnnualManagementFee >= 41))
                {
                    dic.AddModelError("", "Моля въведете коректно число за Годишна такса.");
                }
                else if (this.AnnualManagementFee <= 0)
                {
                    dic.AddModelError("", "Моля въведете коректно число за Годишна такса.");
                }
            }
            if (this.OtherAnnualFees.HasValue)
            {
                if (this.TreatOtherAnnualFeesAsPercent && (this.OtherAnnualFees <= 0 || this.OtherAnnualFees >= 41))
                {
                    dic.AddModelError("", "Моля въведете коректно число за други годишни такси.");
                }
                if (!this.TreatOtherAnnualFeesAsPercent && (this.OtherAnnualFees <= 0 || this.OtherAnnualFees >= this.Amount))
                {
                    dic.AddModelError("", "Моля въведете коректно число за други годишни такси.");
                }
            }
            if (this.MonthlyManagementFee.HasValue)
            {
                if (this.TreatMonthlyManagementFeeAsPercent && (this.MonthlyManagementFee <= 0 || this.MonthlyManagementFee >= 41))
                {
                    dic.AddModelError("", "Моля въведете коректно число за месечна такса управление.");
                }
                if (!this.TreatMonthlyManagementFeeAsPercent && (this.MonthlyManagementFee <= 0 || this.MonthlyManagementFee >= this.Amount))
                {
                    dic.AddModelError("", "Моля въведете коректно число за месечна такса управление.");
                }
            }
            if (this.OtherMonthlyFees.HasValue)
            {
                if (this.TreatOtherMonthlyFeesAsPercent && (this.OtherMonthlyFees <= 0 || this.OtherMonthlyFees >= 41))
                {
                    dic.AddModelError("", "Моля въведете коректно число за други месечни такси.");
                }
                if (!this.TreatOtherMonthlyFeesAsPercent && (this.OtherMonthlyFees <= 0 || this.OtherMonthlyFees >= this.Amount))
                {
                    dic.AddModelError("", "Моля въведете коректно число за други месечни такси.");
                }
            }

            return dic.IsValid;
        }

        private decimal getFeeAmount(decimal fromAmount, decimal fee, bool percent)
        {
            if (!percent) return fee;
            return fromAmount * fee / 100;
        }
    }
}