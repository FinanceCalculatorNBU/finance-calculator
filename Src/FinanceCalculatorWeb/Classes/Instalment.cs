using System;
using System.Collections.Generic;
using System.Linq;

namespace Finance
{
    internal class Instalment
    {
        public decimal Amount { get; set; }
        public decimal DaysAfterFirstAdvance { get; set; }

        internal decimal Calculate(decimal APR)
        {
            decimal divisor = (decimal)Math.Pow(1 + (double)APR, (double)DaysToYears);
            var sum = Amount / divisor;
            return sum;
        }

        private decimal DaysToYears
        {
            get
            {
                return DaysAfterFirstAdvance / 365.25M;
            }
        }
    }
}