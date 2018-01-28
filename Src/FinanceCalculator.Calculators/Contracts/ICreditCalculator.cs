using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceCalculator.Models;

namespace FinanceCalculator.Calculators.Contracts
{
    public interface ICreditCalculator
    {
        CreditCalcResults Calculate(CreditCalcParams p);

        decimal PMT(decimal yearlyInterestRate, int totalNumberOfMonths, decimal loanAmount);

        void IsParamsValid(CreditCalcParams Params);
    }
}
