using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceCalculator.Models;

namespace FinanceCalculator.Calculators.Contracts
{
    public interface ILeasingCalculator
    {
        LeasingCalcResults Calculate(LeasingCalcParams p);

        void IsParamsValid(LeasingCalcParams Params);
    }
}
