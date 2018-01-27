using System.Collections.Generic;

namespace FinanceCalculator.Models
{
    public class CreditCalcResults
    {
        /// <summary>
        /// ГОДИШЕН ПРОЦЕНТЕН РАЗХОД
        /// </summary>
        public decimal AnnualPercentageRate { get; set; }
        /// <summary>
        /// ПОГАСЕНИ С ЛИХВИ И ТАКСИ
        /// </summary>
        public decimal TotalInstallmentsWithTotalFeesAndRates { get; set; }
        /// <summary>
        /// ТАКСИ И КОМИСИОННИ
        /// </summary>
        public decimal TotalFees { get; set; }
        /// <summary>
        /// ЛИХВИ
        /// </summary>
        public decimal TotalRates { get; set; }
        /// <summary>
        /// ВНОСКИ
        /// </summary>
        public decimal TotalInstallments { get; set; }
        /// <summary>
        /// Колекция с редове от погасителния план
        /// </summary>
        public List<MonthlyResult> MonthlyInstallments { get; set; }
    }
}
