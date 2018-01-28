namespace FinanceCalculator.Web.ViewModels
{
    public class LeasingCalcResultsVM
    {
        /// <summary>
        /// ГОДИШЕН ПРОЦЕНТЕН РАЗХОД
        /// </summary>
        public decimal AnnualPercentRate { get; set; }
        /// <summary>
        /// ОБЩО ИЗПЛАТЕНО С ТАКСИ
        /// </summary>
        public decimal TotalPaidWithFees { get; set; }
        /// <summary>
        /// ОБЩО ТАКСИ
        /// </summary>
        public decimal TotalFees { get; set; }
    }
}