using System;
using System.Collections.Generic;
using System.Linq;

namespace FinanceCalculator.Common.Enums
{
    public enum InstalmentType
    {
        Payment = 0,
        Advance = 1
    }

    public enum InstalmentFrequency
    {
        Daily = 0,
        Weekly = 1,
        Fortnightly = 2,
        FourWeekly = 3,
        Monthly = 4,
        Quarterly = 5,
        Annually = 6
    }
}
