namespace FinanceCalculator.Models
{
    using System.ComponentModel.DataAnnotations;
    public class LeasingCalcParams
    {
        /// <summary>
        /// Цена на стоката с ДДС 
        /// </summary>
        [DataType(DataType.Currency)]
        [Range(100, 100000000)]
        public decimal? Price { get; set; }
        /// <summary>
        /// Първоначална вноска
        /// </summary>
        [DataType(DataType.Currency)]
        [Range(1, 100)]
        public decimal? InitialInstallment { get; set; }
        /// <summary>
        /// Период на лизинга (месеци)
        /// </summary>
        [Range(1, 120)]
        public int? Period { get; set; }
        /// <summary>
        /// Месечна вноска
        /// </summary>
        [DataType(DataType.Currency)]
        public decimal? MonthlyInstallment { get; set; }
        /// <summary>
        /// Първоначална такса за обработка валута
        /// </summary>
        public decimal? InitialManagementFee { get; set; }
        /// <summary>
        /// Указва дали Първоначална такса за обработка е процент
        /// </summary>
        public bool TreatInitialManagementFeeAsPercent { get; set; }
    }
}
