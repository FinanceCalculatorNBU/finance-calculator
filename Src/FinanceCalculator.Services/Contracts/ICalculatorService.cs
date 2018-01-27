using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceCalculator.Models;

namespace FinanceCalculator.Services.Contracts
{
    public interface ICalculatorService
    {
        CreditCalcResults CalculateCredit(CreditCalcParams parameters);

        RefinancingCalcResults CalculateRefinancing(RefinancingCalcParams parameters);

        LeasingCalcResults CalculateLeasing(LeasingCalcParams parameters);
    }
}
