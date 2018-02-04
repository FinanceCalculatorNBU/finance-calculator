namespace FinanceCalculator.Models
{
    using System.ComponentModel.DataAnnotations;
    public class LeasingCalcResults
    {
        /// <summary>
        /// ГОДИШЕН ПРОЦЕНТЕН РАЗХОД
        /// </summary>
        [Required]
        public decimal AnnualPercentRate { get; set; }
        /// <summary>
        /// ОБЩО ИЗПЛАТЕНО С ТАКСИ
        /// </summary>
        [Required]
        [DataType(DataType.Currency)]
        public decimal TotalPaidWithFees { get; set; }
        /// <summary>
        /// ОБЩО ТАКСИ
        /// </summary>
        [Required]
        [DataType(DataType.Currency)]
        public decimal TotalFees { get; set; }
    }
}
