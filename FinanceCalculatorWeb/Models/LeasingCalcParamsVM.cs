using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinanceCalculatorWeb.Models
{
    public class LeasingCalcParamsVM
    {
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
    }
}