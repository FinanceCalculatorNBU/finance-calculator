using System;

namespace FinanceCalculator.Web.ViewModels
{
    public class MonthlyResultVM
    {
        /// <summary>
        /// Номер на ред №
        /// </summary>
        public int RowNumber { get; set; }
        /// <summary>
        /// Дата
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Месечна вноска
        /// </summary>
        public decimal Installment { get; set; }
        /// <summary>
        /// Вноска главница
        /// </summary>
        public decimal PrinicpalInstallment { get; set; }
        /// <summary>
        /// Вноска лихва
        /// </summary>
        public decimal RateInstallment { get; set; }
        /// <summary>
        /// Остатък главница
        /// </summary>
        public decimal PrincipalRemainder { get; set; }
        /// <summary>
        /// Такси и комисионни
        /// </summary>
        public decimal Fees { get; set; }
        /// <summary>
        /// Паричен поток
        /// </summary>
        public decimal TotalInstallment { get; set; }
    }
}