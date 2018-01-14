using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Finance
{
    class FinanceCalculator
    {
       /// <summary>
       /// Смята резултатите на финансовия калкулатор
       /// </summary>
       /// <param name="p"></param>
       /// <returns></returns>
        public FinanceCalcResults Calculate(FinanceCalcParams p)
        {
            IsParamsValid(p);
            FinanceCalcResults res = null;
            if (p.IsAnnuityInstallments) res=CalculateAnnuity(p);
            else res=CalculateDescending(p);
            APRCalculator aprCalc = new APRCalculator(p.Amount);
            foreach(var m in res.MonthlyInstallments)
            {
                if (m.RowNumber == 0) aprCalc.AddInstalment(m.Fees, 0);
                else aprCalc.AddInstalment(m.TotalInstallment, 365.25M / 12M * m.RowNumber);
                res.TotalFees += m.Fees;
                res.TotalRates += m.RateInstallment;
                res.TotalInstallments += m.Installment;
            }
            res.AnnualPercentageRate = aprCalc.Calculate();
            res.TotalInstallmentsWithTotalFeesAndRates = res.TotalInstallments + res.TotalFees;
            return res;
        }
        /// <summary>
        /// Функция, която закръглява до 2ри знак след десетичната запетая
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static decimal Round(decimal val)
        {
           return Math.Round(val, 2);
        }
        /// <summary>
        /// Функция, пресмяща анюитетните вноски
        /// </summary>
        /// <param name="yearlyInterestRate"></param>
        /// <param name="totalNumberOfMonths"></param>
        /// <param name="loanAmount"></param>
        /// <returns></returns>
        public decimal PMT(decimal yearlyInterestRate, int totalNumberOfMonths, decimal loanAmount)
        {
            var rate = (decimal)yearlyInterestRate / 100 / 12;
            var denominator = (decimal)Math.Pow((1 + (double)rate), totalNumberOfMonths) - 1;
            return Round((rate + (rate / denominator)) * loanAmount);
        }
        /// <summary>
        /// Пресмята и запълва първия ред от погасителния план
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private MonthlyResult GetZeroMonth(FinanceCalcParams p)
        {
            MonthlyResult m = new MonthlyResult();
            m.RowNumber = 0;
            m.Date = DateTime.Now;
            m.Installment = m.PrinicpalInstallment = m.RateInstallment = 0;
            m.PrincipalRemainder = p.Amount;
            m.Fees = InitialFeesCalc(p, m);
            m.TotalInstallment = p.Amount - m.Fees;
            return m;
        }
        /// <summary>
        /// Пресмята таксите, според това дали са избрани като валута или процент
        /// </summary>
        /// <param name="fromAmount"></param>
        /// <param name="fee"></param>
        /// <param name="percent"></param>
        /// <returns></returns>
        private decimal getFeeAmount(decimal fromAmount,decimal? fee, bool percent)
        {
            if (!percent) return fee ?? 0;
            return fromAmount * (fee ?? 0)/100;
        }
        /// <summary>
        /// Пресмята първоначалните такси
        /// </summary>
        /// <param name="p"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        private decimal InitialFeesCalc(FinanceCalcParams p, MonthlyResult m)
        {          

            m.Fees = getFeeAmount(p.Amount, p.ApplicationFee, p.TreatApplicationFeeAsPercent);
            m.Fees += getFeeAmount(p.Amount, p.ProcessingFee, p.TreatProcessingFeeAsPercent);
            m.Fees += getFeeAmount(p.Amount, p.OtherInitialFees, p.TreatOtherInitialFeesAsPercent);
            return Round(m.Fees);
        }
        /// <summary>
        /// Функция, пресмятаща Месечните такси
        /// </summary>
        /// <param name="p"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        private decimal MonthlyFeesCalc(FinanceCalcParams p, MonthlyResult m)
        {
            m.Fees = getFeeAmount(m.PrincipalRemainder, p.MonthlyManagementFee, p.TreatMonthlyManagementFeeAsPercent);
            m.Fees += getFeeAmount(m.PrincipalRemainder, p.OtherMonthlyFees, p.TreatOtherMonthlyFeesAsPercent);
            return Round(m.Fees);
        }
        /// <summary>
        /// Функция, пресмятаща годишните такси
        /// </summary>
        /// <param name="p"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        private decimal AnnualFeesCalc(FinanceCalcParams p, MonthlyResult m)
        {
            m.Fees = getFeeAmount(m.PrincipalRemainder, p.AnnualManagementFee, p.TreatAnnualManagementFeeAsPercent);
            m.Fees += getFeeAmount(m.PrincipalRemainder, p.OtherAnnualFees, p.TreatOtherAnnualFeesAsPercent);
            return Round(m.Fees);
        }
        /// <summary>
        /// Функция, пресмятаща резултатите от погасителния план при анюитетни вноски
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private FinanceCalcResults CalculateAnnuity(FinanceCalcParams p)
        {
            FinanceCalcResults res = new FinanceCalcResults();
            res.MonthlyInstallments = new List<MonthlyResult>();
            res.MonthlyInstallments.Add(GetZeroMonth(p));
            bool hasPromotion = p.PromotionPeriod.HasValue;
            decimal pmtPromotional = hasPromotion?PMT(p.PromotionRate.Value,p.Period-(p.GratisPeriod??0), p.Amount):0;
            decimal pmtNormal = hasPromotion?0:PMT(p.Rate, p.Period-(p.GratisPeriod??0), p.Amount);
            decimal promotionalInstallment = 0;
            for (int i = 1; i <= p.Period; i++)
            {
                MonthlyResult m = new MonthlyResult();
                if (i == 1) m.PrincipalRemainder = p.Amount;
                else m.PrincipalRemainder = res.MonthlyInstallments[i - 1].CurrentPrincipallRemainder();
                m.RowNumber = i;
                m.Date = DateTime.Now.AddMonths(i);
                if (i <= (p.PromotionPeriod ?? 0)) 
                {
                    m.RateInstallment = Round((decimal)(m.PrincipalRemainder*p.PromotionRate/100)/12);
                    if (i <= (p.GratisPeriod ?? 0))
                    {
                        m.Installment = m.RateInstallment;
                    }
                    else
                    {
                        m.Installment = pmtPromotional;
                    }
                    m.PrinicpalInstallment = m.Installment-m.RateInstallment;
                    promotionalInstallment += m.PrinicpalInstallment;

                }
                else
                { 
                    m.RateInstallment = Round((decimal)(m.PrincipalRemainder * p.Rate / 100) / 12);
                    if (i <= (p.GratisPeriod ?? 0))
                    {
                        m.Installment = m.RateInstallment;
                    }
                    else
                    {
                        if (pmtNormal == 0)
                        {
                            pmtNormal = PMT(p.Rate, p.Period - Math.Max(p.PromotionPeriod.Value, p.GratisPeriod??0), p.Amount - promotionalInstallment);
                        } 
                        if (i == p.Period)
                        {
                            pmtNormal = PMT(p.Rate, 1, m.PrincipalRemainder);
                        }
                        m.Installment = pmtNormal;
                    }
                    m.PrinicpalInstallment = m.Installment - m.RateInstallment; 
                }
                if (i % 12 == 1 && i != 1) m.Fees = MonthlyFeesCalc(p, m) + AnnualFeesCalc(p, m); 
                else m.Fees = MonthlyFeesCalc(p, m);
                m.TotalInstallment = m.Installment + m.Fees;
                res.MonthlyInstallments.Add(m);
            }
          return res;
        }

       /// <summary>
       /// Функция, пресмятаща резултатите от погасителния план при намаляващи месечни вноски
       /// </summary>
       /// <param name="p"></param>
       /// <returns></returns>
        private FinanceCalcResults CalculateDescending(FinanceCalcParams p)
        {
            FinanceCalcResults res = new FinanceCalcResults();
            res.MonthlyInstallments = new List<MonthlyResult>();
            res.MonthlyInstallments.Add(GetZeroMonth(p));
           decimal principalInstallment=Round(p.Amount/(p.Period-(p.GratisPeriod??0)));
           for (int i = 1; i <= p.Period; i++)
           {
               MonthlyResult m = new MonthlyResult();
               if (i == 1) m.PrincipalRemainder = p.Amount;
               else m.PrincipalRemainder = res.MonthlyInstallments[i - 1].CurrentPrincipallRemainder();
               m.RowNumber = i;
               m.Date = DateTime.Now.AddMonths(i);
               bool isInPromotion=(i <= (p.PromotionPeriod ?? 0));
               bool isInGratis = (i <= (p.GratisPeriod ?? 0));
               m.RateInstallment = Round((decimal)(m.PrincipalRemainder * (isInPromotion?p.PromotionRate:p.Rate) / 100) / 12);
               if(isInGratis)
               {
                   m.PrinicpalInstallment = 0;
               }
               else
               {
                   m.PrinicpalInstallment = (i == p.Period) ? Round(p.Amount - (i - 1 - (p.GratisPeriod ?? 0)) * principalInstallment) : principalInstallment;
               }
               m.Installment = (isInGratis ? 0 : m.PrinicpalInstallment) + m.RateInstallment;
               if (i % 12 == 1 && i != 1) m.Fees = MonthlyFeesCalc(p, m) + AnnualFeesCalc(p, m);
               else m.Fees = MonthlyFeesCalc(p, m);
               m.TotalInstallment = m.Installment + m.Fees;
               res.MonthlyInstallments.Add(m);
           }
            return res;
        }

        public void IsParamsValid(FinanceCalcParams Params)
        { 
            if ((Params.Amount ) <= 0 || (Params.Amount) > 999999999)
            {
                throw new ArgumentException("Моля въведете размер на кредита по-голям от нула и по-малко от 1 000 000 000.");
            }
            if ((Params.Period ) <= 0 || (Params.Period ) > 960)
            {
                throw new ArgumentException("Моля въведете коректно число за срок (до 960 месеца).");
            }
            if ((Params.Rate ) <= 0)
            {
                throw new ArgumentException("Моля въведете размер на лихвата по-голяма от нула.");
            }
            if (Params.PromotionPeriod.HasValue && (Params.PromotionPeriod <= 0 || Params.PromotionPeriod >= Params.Period))
            {
                throw new ArgumentException("Моля въведете коректно число за промоционален период (промоционалният период трябва да е по-голям от нула и по-малък от срока на кредита).");
            }
            if (!Params.PromotionRate.HasValue && Params.PromotionPeriod.HasValue)
            {
                throw new ArgumentException("Моля въведете коректно число за промоционална лихва(въвели сте промоционален период, но не сте въвели промоционална лихва)");
            }
            if (Params.PromotionRate.HasValue && !Params.PromotionPeriod.HasValue)
            {
                throw new ArgumentException("Моля въведете коректно число за промоционален период(въвели сте промоционална лихва, но не сте въвели промоционален период)");
            }
            if (Params.GratisPeriod.HasValue && (Params.GratisPeriod <= 0 || Params.GratisPeriod >= Params.Period))
            {
                throw new ArgumentException("Моля въведете коректно число за гратисен период (гратисният период трябва да е по-малък от срока на кредита)");
            }
            if (Params.ApplicationFee.HasValue)
            {
                if (Params.TreatApplicationFeeAsPercent && (Params.ApplicationFee <= 0 || Params.ApplicationFee >= 41))
                    throw new ArgumentException("Моля въведете коректно число за такса кандидатстване.");
                else if (Params.ApplicationFee <= 0)
                    throw new ArgumentException("Моля въведете коректно число за такса кандидатстване.");
            }
            if ((Params.ApplicationFee.HasValue || Params.ProcessingFee.HasValue || Params.OtherInitialFees.HasValue) && Params.Amount > 0)
            {
                if (getFeeAmount(Params.Amount, Params.ApplicationFee, Params.TreatApplicationFeeAsPercent)
                    + getFeeAmount(Params.Amount, Params.ProcessingFee ?? 0, Params.TreatProcessingFeeAsPercent)
                    + getFeeAmount(Params.Amount, Params.OtherInitialFees ?? 0, Params.TreatOtherInitialFeesAsPercent) > (Params.Amount / 2))
                {
                    throw new ArgumentException("Първоначалните такси не трябва да надвишават 50% от размера на кредита.");
                }
            }

            if (Params.AnnualManagementFee.HasValue)
            {
                if (Params.TreatAnnualManagementFeeAsPercent && (Params.AnnualManagementFee <= 0 || Params.AnnualManagementFee >= 41))
                {
                    throw new ArgumentException("Моля въведете коректно число за Годишна такса.");
                }
                else if (Params.AnnualManagementFee <= 0)
                {
                    throw new ArgumentException("Моля въведете коректно число за Годишна такса.");
                }
            }
            if (Params.OtherAnnualFees.HasValue)
            {
                if (Params.TreatOtherAnnualFeesAsPercent && (Params.OtherAnnualFees <= 0 || Params.OtherAnnualFees >= 41))
                {
                    throw new ArgumentException("Моля въведете коректно число за други годишни такси.");
                }
                if (!Params.TreatOtherAnnualFeesAsPercent && (Params.OtherAnnualFees <= 0 || Params.OtherAnnualFees >= Params.Amount))
                {
                    throw new ArgumentException("Моля въведете коректно число за други годишни такси.");
                }
            }
            if (Params.MonthlyManagementFee.HasValue)
            {
                if (Params.TreatMonthlyManagementFeeAsPercent && (Params.MonthlyManagementFee <= 0 || Params.MonthlyManagementFee >= 41))
                {
                    throw new ArgumentException("Моля въведете коректно число за месечна такса управление.");
                }
                if (!Params.TreatMonthlyManagementFeeAsPercent && (Params.MonthlyManagementFee <= 0 || Params.MonthlyManagementFee >= Params.Amount))
                {
                    throw new ArgumentException("Моля въведете коректно число за месечна такса управление.");
                }
            }
            if (Params.OtherMonthlyFees.HasValue)
            {
                if (Params.TreatOtherMonthlyFeesAsPercent && (Params.OtherMonthlyFees <= 0 || Params.OtherMonthlyFees >= 41))
                {
                    throw new ArgumentException("Моля въведете коректно число за други месечни такси.");
                }
                if (!Params.TreatOtherMonthlyFeesAsPercent && (Params.OtherMonthlyFees <= 0 || Params.OtherMonthlyFees >= Params.Amount))
                {
                    throw new ArgumentException("Моля въведете коректно число за други месечни такси.");
                }
            }
        }
    }
}
