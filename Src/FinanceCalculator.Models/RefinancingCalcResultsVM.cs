namespace FinanceCalculator.Models
{
    public class RefinancingCalcResultsVM
    {
        /// <summary>
        /// Текущ кредит Лихва 
        /// </summary>
        public decimal CurrRate { get; set; }
        /// <summary>
        /// Нов кредит Лихва
        /// </summary>
        public decimal NewRate { get; set; }
        /// <summary>
        /// Текущ кредит Срок на кредита
        /// </summary>
        public int CurrPeriod { get; set; }
        /// <summary>
        /// Нов кредит Срок на кредита
        /// </summary>
        public int NewPeriod { get; set; }
        /// <summary>
        /// Такса за предсрочно погасяване Текущ кредит
        /// </summary>
        public decimal CurrPreTermFee { get; set; }
        /// <summary>
        /// Текущ кредит Месечна вноска Анюитетна
        /// </summary>
        public decimal CurrMonthlyInstallment { get; set; }
        /// <summary>
        /// Нов кредит Месечна вноска Анюитетна
        /// </summary>
        public decimal NewMonthlyInstallment { get; set; }
        /// <summary>
        /// Текущ кредит Общо изплатени
        /// </summary>
        public decimal CurrTotalPaid { get; set; }
        /// <summary>
        /// Нов кредит Общо изплатени
        /// </summary>
        public decimal NewTotalPaid { get; set; }
    }
}