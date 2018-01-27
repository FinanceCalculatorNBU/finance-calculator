using FinanceCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCalculator.Calculators.Contracts
{
    public interface ILeasingCalculator
    {
        LeasingCalcResults Calculate(LeasingCalcParams p);

        void IsParamsValid(LeasingCalcParams Params);
    }
}
