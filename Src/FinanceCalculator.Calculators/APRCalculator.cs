using System;
using System.Collections.Generic;
using System.Linq;
using FinanceCalculator.Models;
using FinanceCalculator.Common.Enums;

namespace FinanceCalculator.Calculators
{
    public class APRCalculator
    {
        public APRCalculator(decimal firstAdvance)
            : this(firstAdvance, new List<Instalment>(), new List<Instalment>())
        {
        }

        public APRCalculator(decimal firstAdvance, List<Instalment> advances, List<Instalment> payments)
        {
            _Advances = advances;
            _Payments = payments;
            _Advances.Add(new Instalment() { Amount = firstAdvance, DaysAfterFirstAdvance = 0 });
        }

        public decimal SinglePaymentCalculation(decimal payment, int DaysAfterAdvance)
        {
            return (decimal)Math.Round((Math.Pow((double)_Advances[0].Amount / (double)payment, (-365.25 / DaysAfterAdvance)) - 1) * 100, 1, MidpointRounding.AwayFromZero);
        }

        public decimal Calculate(decimal guess = 0)
        {
            decimal rateToTry = guess / 100;
            decimal difference = 1;
            decimal amountToAdd = 0.0001M;

            while (difference != 0)
            {
                decimal advances = _Advances.Sum(a => a.Calculate(rateToTry));
                decimal payments = _Payments.Sum(p => p.Calculate(rateToTry));

                difference = payments - advances;

                if (difference <= 0.0000001M && difference >= -0.0000001M)
                {
                    break;
                }

                if (difference > 0)
                {
                    amountToAdd = amountToAdd * 2;
                    rateToTry = rateToTry + amountToAdd;
                }

                else
                {
                    amountToAdd = amountToAdd / 2;
                    rateToTry = rateToTry - amountToAdd;
                }
            }

            return Math.Round(rateToTry * 100, 4);
        }

        public void AddInstalment(decimal amount, decimal daysAfterFirstAdvance, InstalmentType instalmentType = InstalmentType.Payment)
        {
            var instalment = new Instalment() { Amount = amount, DaysAfterFirstAdvance = daysAfterFirstAdvance };
            switch (instalmentType)
            {
                case InstalmentType.Payment:
                    _Payments.Add(instalment);
                    break;
                case InstalmentType.Advance:
                    _Advances.Add(instalment);
                    break;
            }
        }

        private static decimal getDaysBewteenInstalments(InstalmentFrequency instalmentFrequency)
        {
            switch (instalmentFrequency)
            {
                case InstalmentFrequency.Daily:
                    return 1;
                case InstalmentFrequency.Weekly:
                    return 7;
                case InstalmentFrequency.Fortnightly:
                    return 14;
                case InstalmentFrequency.FourWeekly:
                    return 28;
                case InstalmentFrequency.Monthly:
                    return 365.25M / 12M;
                case InstalmentFrequency.Quarterly:
                    return 365.25M / 4M;
                case InstalmentFrequency.Annually:
                    return 365.25M;
            }
            return 1;
        }

        public void AddRegularInstalments(decimal amount, int numberOfInstalments, InstalmentFrequency instalmentFrequency, decimal daysAfterFirstAdvancefirstInstalment = 0)
        {
            decimal daysBetweenInstalments = getDaysBewteenInstalments(instalmentFrequency);
            if (daysAfterFirstAdvancefirstInstalment == 0)
            {
                daysAfterFirstAdvancefirstInstalment = daysBetweenInstalments;
            }
            for (int i = 0; i < numberOfInstalments; i++)
            {
                _Payments.Add(new Instalment() { Amount = amount, DaysAfterFirstAdvance = daysAfterFirstAdvancefirstInstalment + (daysBetweenInstalments * i) });
            }
        }

        public void AddDecreasingInstalments(decimal amount, decimal interest, int numberOfInstalments, InstalmentFrequency instalmentFrequency, decimal daysAfterFirstAdvancefirstInstalment = 0)
        {
            decimal daysBetweenInstalments = getDaysBewteenInstalments(instalmentFrequency);
            if (daysAfterFirstAdvancefirstInstalment == 0)
            {
                daysAfterFirstAdvancefirstInstalment = daysBetweenInstalments;
            }
            decimal glavnica = amount / numberOfInstalments;

            decimal lihva = interest;

            for (int i = 0; i < numberOfInstalments; i++)
            {
                decimal vnoskaLihva = (amount * lihva / 100) / 12;
                decimal finalInst = vnoskaLihva + glavnica;
                amount = amount - glavnica;

                if (i % 12 == 0 && i < numberOfInstalments - 1 && i != 0)
                {
                    finalInst += 5000;
                }

                _Payments.Add(new Instalment() { Amount = finalInst, DaysAfterFirstAdvance = daysAfterFirstAdvancefirstInstalment + (daysBetweenInstalments * i) });
            }
        }

        private readonly List<Instalment> _Advances;
        private readonly List<Instalment> _Payments;
    }
}