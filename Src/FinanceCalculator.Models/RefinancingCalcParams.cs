namespace FinanceCalculator.Models
{
    using System.ComponentModel.DataAnnotations;
    public class RefinancingCalcParams
    {
        /// <summary>
        /// Настоящ кредит Размер на кредита
        /// </summary>
        [Range(1, 99999999)]
        [DataType(DataType.Currency)]
        public decimal? CurrentCreditAmount { get; set; }
        /// <summary>
        /// Настоящ кредит Лихва (%)
        /// </summary>
        [Range(1, 100)]
        public decimal? CurrentCreditRate { get; set; }
        /// <summary>
        /// Настоящ кредит Срок на кредита (месеци) 
        /// </summary>
        [Range(1, 999)]
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
        [Range(1, 100)]
        public decimal? NewCreditRate { get; set; }
        /// <summary>
        /// Нов кредит Първоначални такси (%)
        /// </summary>
        public decimal? NewCreditInitialFeesPercent { get; set; }
        /// <summary>
        /// Нов кредит Първоначални такси (валута)
        /// </summary>
        public decimal? NewCreditInitialFeesCurrency { get; set; }
    }
}
