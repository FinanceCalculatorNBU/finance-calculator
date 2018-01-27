using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceCalculator.Services.Contracts;
using FinanceCalculator.Models;
using FinanceCalculator.Calculators.Contracts;

namespace FinanceCalculator.Services
{
    public class CalculatorService : ICalculatorService
    {
        private readonly ICreditCalculator creditCalculator;
        private readonly IRefinancingCalculator refinancingCalculator;
        private readonly ILeasingCalculator leasingCalculator;

        public CalculatorService(ICreditCalculator creditCalculator, IRefinancingCalculator refinancingCalculator, ILeasingCalculator leasingCalculator)
        {
            this.creditCalculator = creditCalculator;
            this.refinancingCalculator = refinancingCalculator;
            this.leasingCalculator = leasingCalculator;
        }

        public CreditCalcResults CalculateCredit(CreditCalcParams parameters)
        {
            return this.creditCalculator.Calculate(parameters);
        }

        public RefinancingCalcResults CalculateRefinancing(RefinancingCalcParams parameters)
        {
            return this.refinancingCalculator.Calculate(parameters);
        }

        public LeasingCalcResults CalculateLeasing(LeasingCalcParams parameters)
        {
            return this.leasingCalculator.Calculate(parameters);
        }
    }
}
