using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinanceCalculator.Models
{
    public class CreditCalcResults
    {
        /// <summary>
        /// ГОДИШЕН ПРОЦЕНТЕН РАЗХОД
        /// </summary>
        [Required]
        public decimal AnnualPercentageRate { get; set; }
        /// <summary>
        /// ПОГАСЕНИ С ЛИХВИ И ТАКСИ
        /// </summary>
        [Required]
        public decimal TotalInstallmentsWithTotalFeesAndRates { get; set; }
        /// <summary>
        /// ТАКСИ И КОМИСИОННИ
        /// </summary>
        [Required]
        public decimal TotalFees { get; set; }
        /// <summary>
        /// ЛИХВИ
        /// </summary>
        [Required]
        public decimal TotalRates { get; set; }
        /// <summary>
        /// ВНОСКИ
        /// </summary>
        [Required]
        public decimal TotalInstallments { get; set; }
        /// <summary>
        /// Колекция с редове от погасителния план
        /// </summary>
        public List<MonthlyResult> MonthlyInstallments { get; set; }
    }
}
