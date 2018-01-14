using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Finance
{
    class LeasingCalcResults
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
