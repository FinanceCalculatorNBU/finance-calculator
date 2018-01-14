using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Finance
{
    class RefinancingCalcParams
    {
        /// <summary>
        /// Настоящ кредит Размер на кредита
        /// </summary>
        public decimal? CurrentCreditAmount { get; set; }
        /// <summary>
        /// Настоящ кредит Лихва (%)
        /// </summary>
        public decimal? CurrentCreditRate { get; set; }
        /// <summary>
        /// Настоящ кредит Срок на кредита (месеци) 
        /// </summary>
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
