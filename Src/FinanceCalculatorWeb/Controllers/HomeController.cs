using Finance;
using FinanceCalculatorWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinanceCalculatorWeb.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult FinanceCalculator()
        {
            FinanceCalculatorVM model = new FinanceCalculatorVM();
            model.Params = new FinanceCalculatorParamsVM();
            model.Params.IsAnnuityInstallments = true;
            model.Params.TreatApplicationFeeAsPercent = true;
            model.Params.TreatProcessingFeeAsPercent = true;
            model.Params.TreatMonthlyManagementFeeAsPercent = true;
            model.Params.TreatAnnualManagementFeeAsPercent = true;
            return View(model);
        }
        [HttpPost]
        public ActionResult FinanceCalculator(FinanceCalculatorVM model)
        {
            if (model.IsModelValid(ModelState))
            {
                var paramats = GetParamsFromModel(model);
                FinanceCalculator calc = new FinanceCalculator();
                FinanceCalcResults res = calc.Calculate(paramats);
                model.Result = GetResultsForModel(res);
            }
            return View(model);            
        }

        [HttpGet]
        public ActionResult RefinancingCalculator()
        {
            RefinancingCalculatorVM model = new RefinancingCalculatorVM();
            model.Params = new RefinancingCalcParamsVM();
            return View(model);
        }
        [HttpPost]
        public ActionResult RefinancingCalculator(RefinancingCalculatorVM model)
        {
            if (model.IsModelValid(ModelState))
            {
                var paramats = GetParamsFromModel(model);
                RefinancingCalculator calc = new RefinancingCalculator();
                RefinancingCalcResults res = calc.Calculate(paramats);
                model.Result = GetResultsForModel(res);
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult LeasingCalculator()
        {
            LeasingCalculatorVM model = new LeasingCalculatorVM();
            model.Params = new LeasingCalcParamsVM();
            model.Params.TreatInitialManagementFeeAsPercent = true;           
            return View(model);
        }
        [HttpPost]
        public ActionResult LeasingCalculator(LeasingCalculatorVM model)
        {
            if (model.IsModelValid(ModelState))
            {
                var paramats = GetParamsFromModel(model);
                LeasingCalculator calc = new LeasingCalculator();
                LeasingCalcResults res = calc.Calculate(paramats);
                model.Result = GetResultsForModel(res);
            }
            return View(model);
        }

        private LeasingCalcParams GetParamsFromModel(LeasingCalculatorVM model)
        {
            LeasingCalcParams res = new LeasingCalcParams();
            res.Price = model.Params.Price ?? 0;
            res.InitialInstallment = model.Params.InitialInstallment ?? 0;
            res.Period = model.Params.Period ?? 0;
            res.MonthlyInstallment = model.Params.MonthlyInstallment ?? 0;
            res.InitialManagementFee = model.Params.InitialManagementFee;
            res.TreatInitialManagementFeeAsPercent = model.Params.TreatInitialManagementFeeAsPercent;
            return res;
        }

        private LeasingCalcResultsVM GetResultsForModel(LeasingCalcResults res)
        {
            LeasingCalcResultsVM mRes = new LeasingCalcResultsVM();
            mRes.AnnualPercentRate = res.AnnualPercentRate;
            mRes.TotalFees = res.TotalFees;
            mRes.TotalPaidWithFees = res.TotalPaidWithFees;
            return mRes;
        }

        private RefinancingCalcParams GetParamsFromModel(RefinancingCalculatorVM model)
        {
            RefinancingCalcParams res = new RefinancingCalcParams();
            res.CurrentCreditAmount = model.Params.CurrentCreditAmount ?? 0;
            res.CurrentCreditMadeInstallments = model.Params.CurrentCreditMadeInstallments ?? 0;
            res.CurrentCreditPeriod = model.Params.CurrentCreditPeriod ?? 0;
            res.CurrentCreditPreTermFee = model.Params.CurrentCreditPreTermFee;
            res.CurrentCreditRate = model.Params.CurrentCreditRate;
            res.NewCreditInitialFeesCurrency = model.Params.NewCreditInitialFeesCurrency;
            res.NewCreditInitialFeesPercent = model.Params.NewCreditInitialFeesPercent;
            res.NewCreditRate = model.Params.NewCreditRate;
            return res;
        }

        private RefinancingCalcResultsVM GetResultsForModel(RefinancingCalcResults res)
        {
            RefinancingCalcResultsVM mRes = new RefinancingCalcResultsVM();
            mRes.CurrMonthlyInstallment = res.CurrMonthlyInstallment;
            mRes.CurrPeriod = res.CurrPeriod;
            mRes.CurrPreTermFee = res.CurrPreTermFee;
            mRes.CurrRate = res.CurrRate;
            mRes.CurrTotalPaid = res.CurrTotalPaid;
            mRes.NewMonthlyInstallment = res.NewMonthlyInstallment;
            mRes.NewPeriod = res.NewPeriod;
            mRes.NewRate = res.NewRate;
            mRes.NewTotalPaid = res.NewTotalPaid;     
            return mRes;
        }
            
        private FinanceCalcParams GetParamsFromModel(FinanceCalculatorVM model)
        {
            FinanceCalcParams res = new FinanceCalcParams();
            res.Amount = model.Params.Amount??0;
            res.Period = model.Params.Period??0;
            res.Rate = model.Params.Rate ?? 0;
            res.PromotionPeriod = model.Params.PromotionPeriod;
            res.PromotionRate = model.Params.PromotionRate;
            res.GratisPeriod = model.Params.GratisPeriod;
            res.IsAnnuityInstallments = model.Params.IsAnnuityInstallments;
            res.ApplicationFee = model.Params.ApplicationFee;
            res.TreatApplicationFeeAsPercent = model.Params.TreatApplicationFeeAsPercent;
            res.ProcessingFee = model.Params.ProcessingFee;
            res.TreatProcessingFeeAsPercent = model.Params.TreatProcessingFeeAsPercent;
            res.OtherInitialFees = model.Params.OtherInitialFees;
            res.TreatOtherInitialFeesAsPercent = model.Params.TreatOtherInitialFeesAsPercent;
            res.MonthlyManagementFee = model.Params.MonthlyManagementFee;
            res.TreatMonthlyManagementFeeAsPercent = model.Params.TreatMonthlyManagementFeeAsPercent;
            res.OtherMonthlyFees = model.Params.OtherMonthlyFees;
            res.TreatOtherMonthlyFeesAsPercent = model.Params.TreatOtherMonthlyFeesAsPercent;
            res.AnnualManagementFee = model.Params.AnnualManagementFee;
            res.TreatAnnualManagementFeeAsPercent=model.Params.TreatAnnualManagementFeeAsPercent;
            res.OtherAnnualFees = model.Params.OtherAnnualFees;
            res.TreatOtherAnnualFeesAsPercent = model.Params.TreatOtherAnnualFeesAsPercent;
            return res;
        }
        private FinanceCalculatorResultVM GetResultsForModel(FinanceCalcResults res)
        {
            FinanceCalculatorResultVM mRes = new FinanceCalculatorResultVM();
            mRes.AnnualPercentageRate = res.AnnualPercentageRate;
            
            mRes.TotalFees = res.TotalFees;
            mRes.TotalInstallments = res.TotalInstallments;
            mRes.TotalInstallmentsWithTotalFeesAndRates = res.TotalInstallmentsWithTotalFeesAndRates;
            mRes.TotalRates = res.TotalRates;
            mRes.MonthlyInstallments = new List<MonthlyResultVM>();
            foreach(var m in res.MonthlyInstallments)
            {
                MonthlyResultVM mcalc = new MonthlyResultVM();
                mcalc.Date = m.Date;
                mcalc.Fees = m.Fees;
                mcalc.Installment = m.Installment;
                mcalc.PrincipalRemainder = m.PrincipalRemainder;
                mcalc.PrinicpalInstallment = m.PrinicpalInstallment;
                mcalc.RateInstallment = m.RateInstallment;
                mcalc.RowNumber = m.RowNumber;
                mcalc.TotalInstallment = m.TotalInstallment;
                mRes.MonthlyInstallments.Add(mcalc);
            }
            return mRes;
        }


    }
}
