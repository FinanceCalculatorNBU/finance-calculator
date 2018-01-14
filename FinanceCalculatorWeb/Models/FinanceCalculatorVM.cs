using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinanceCalculatorWeb.Models
{
    public class FinanceCalculatorVM
    {
        public FinanceCalculatorParamsVM Params { get; set; }
        public FinanceCalculatorResultVM Result { get; set; }
        public bool IsModelValid(ModelStateDictionary dic)
        {
            if ((Params.Amount ?? 0) <= 0 || (Params.Amount ?? 0) > 999999999) 
            {
                dic.AddModelError("", "Моля въведете размер на кредита по-голям от нула и по-малко от 1 000 000 000.");
            }
            if((Params.Period ?? 0)<=0 ||(Params.Period ?? 0) > 960)
            {
                dic.AddModelError("", "Моля въведете коректно число за срок (до 960 месеца).");
            }
            if((Params.Rate ?? 0) <= 0)
            {
                dic.AddModelError("", "Моля въведете размер на лихвата по-голяма от нула.");
            }
            if(Params.PromotionPeriod.HasValue && (Params.PromotionPeriod  <= 0 || Params.PromotionPeriod >= Params.Period))
            {
                dic.AddModelError("", "Моля въведете коректно число за промоционален период (промоционалният период трябва да е по-голям от нула и по-малък от срока на кредита).");
            }
            if (!Params.PromotionRate.HasValue && Params.PromotionPeriod.HasValue)
            {
                dic.AddModelError("", "Моля въведете коректно число за промоционална лихва(въвели сте промоционален период, но не сте въвели промоционална лихва)");
            }
            if (Params.PromotionRate.HasValue && !Params.PromotionPeriod.HasValue)
            {
                dic.AddModelError("", "Моля въведете коректно число за промоционален период(въвели сте промоционална лихва, но не сте въвели промоционален период)");
            }
            if(Params.GratisPeriod.HasValue && (Params.GratisPeriod <= 0 || Params.GratisPeriod >= Params.Period) )
            {
                dic.AddModelError("", "Моля въведете коректно число за гратисен период (гратисният период трябва да е по-малък от срока на кредита)");
            }
            if (Params.ApplicationFee.HasValue)
            {
                if (Params.TreatApplicationFeeAsPercent && (Params.ApplicationFee <= 0 || Params.ApplicationFee >= 41))
                    dic.AddModelError("", "Моля въведете коректно число за такса кандидатстване.");
                else if (Params.ApplicationFee <= 0)
                    dic.AddModelError("", "Моля въведете коректно число за такса кандидатстване.");
            }
            if((Params.ApplicationFee.HasValue || Params.ProcessingFee.HasValue || Params.OtherInitialFees.HasValue) && Params.Amount.HasValue)
            {
                if(getFeeAmount(Params.Amount ?? 0, Params.ApplicationFee ?? 0, Params.TreatApplicationFeeAsPercent)
                    + getFeeAmount(Params.Amount ?? 0, Params.ProcessingFee ?? 0, Params.TreatProcessingFeeAsPercent)
                    + getFeeAmount(Params.Amount ?? 0, Params.OtherInitialFees ?? 0, Params.TreatOtherInitialFeesAsPercent) > (Params.Amount / 2))
                {
                    dic.AddModelError("", "Първоначалните такси не трябва да надвишават 50% от размера на кредита.");
                }
            }          
            
            if(Params.AnnualManagementFee.HasValue)                
            {
                if(Params.TreatAnnualManagementFeeAsPercent && (Params.AnnualManagementFee <= 0 || Params.AnnualManagementFee >= 41))
                {
                    dic.AddModelError("", "Моля въведете коректно число за Годишна такса.");
                }
                else if (Params.AnnualManagementFee <= 0 )
                {
                    dic.AddModelError("", "Моля въведете коректно число за Годишна такса.");
                }
            }
            if(Params.OtherAnnualFees.HasValue)
            {
                if (Params.TreatOtherAnnualFeesAsPercent && (Params.OtherAnnualFees <= 0 || Params.OtherAnnualFees >= 41))
                {
                    dic.AddModelError("", "Моля въведете коректно число за други годишни такси.");
                }
                if (!Params.TreatOtherAnnualFeesAsPercent && (Params.OtherAnnualFees <= 0 || Params.OtherAnnualFees >= Params.Amount))
                {
                    dic.AddModelError("", "Моля въведете коректно число за други годишни такси.");
                }
            }
            if(Params.MonthlyManagementFee.HasValue)
            {
                if(Params.TreatMonthlyManagementFeeAsPercent && (Params.MonthlyManagementFee <= 0 || Params.MonthlyManagementFee >= 41))
                {
                    dic.AddModelError("", "Моля въведете коректно число за месечна такса управление.");
                }
                if(!Params.TreatMonthlyManagementFeeAsPercent && (Params.MonthlyManagementFee <= 0 || Params.MonthlyManagementFee >= Params.Amount))
                {
                    dic.AddModelError("", "Моля въведете коректно число за месечна такса управление.");
                }
            }
            if (Params.OtherMonthlyFees.HasValue)
            {
                if (Params.TreatOtherMonthlyFeesAsPercent && (Params.OtherMonthlyFees <= 0 || Params.OtherMonthlyFees >= 41))
                {
                    dic.AddModelError("", "Моля въведете коректно число за други месечни такси.");
                }
                if (!Params.TreatOtherMonthlyFeesAsPercent && (Params.OtherMonthlyFees <= 0 || Params.OtherMonthlyFees >= Params.Amount))
                {
                    dic.AddModelError("", "Моля въведете коректно число за други месечни такси.");
                }
            }

            return dic.IsValid;
        }
        private decimal getFeeAmount(decimal fromAmount, decimal fee, bool percent)
        {
            if (!percent) return fee;
            return fromAmount * fee / 100;
        }
    }
}