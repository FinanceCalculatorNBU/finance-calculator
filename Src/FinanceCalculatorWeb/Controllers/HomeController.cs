using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinanceCalculator.Models;
using FinanceCalculator.Calculators;
using FinanceCalculator.Services.Contracts;
using FinanceCalculator.Web.ViewModels;

namespace FinanceCalculator.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICalculatorService calculatorService;

        public HomeController(ICalculatorService calculatorService)
        {
            this.calculatorService = calculatorService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult FinanceCalculator()
        {
            CreditCalculatorParamsVM model = new CreditCalculatorParamsVM();

            return View(model);
        }

        [HttpPost]
        public ActionResult FinanceCalculator(CreditCalculatorParamsVM model)
        {
            if (!model.IsModelValid(ModelState))
            {
                return PartialView("_InvalidCreditParamsPartial", model);
            }

            var parameters = GetParamsFromModel(model);
            CreditCalcResults creditResult = this.calculatorService.CalculateCredit(parameters);
            CreditCalculatorResultVM creditViewModelResult = GetResultsForModel(creditResult);

            return PartialView("_CreditResultsPartial", creditViewModelResult);
        }

        [HttpGet]
        public ActionResult RefinancingCalculator()
        {
            RefinancingCalcParamsVM model = new RefinancingCalcParamsVM();

            return View(model);
        }

        [HttpPost]
        public ActionResult RefinancingCalculator(RefinancingCalcParamsVM model)
        {
            if (!model.IsModelValid(ModelState))
            {
                return PartialView("_InvalidRefinancingParamsPartial", model);
            }

            var parameters = GetParamsFromModel(model);
            RefinancingCalcResults refinancingResult = this.calculatorService.CalculateRefinancing(parameters);
            RefinancingCalcResultsVM refinancingViewModelResult = GetResultsForModel(refinancingResult);

            return PartialView("_RefinancingResultsPartial", refinancingViewModelResult);
        }

        [HttpGet]
        public ActionResult LeasingCalculator()
        {
            LeasingCalcParamsVM model = new LeasingCalcParamsVM();

            return View(model);
        }

        [HttpPost]
        public ActionResult LeasingCalculator(LeasingCalcParamsVM model)
        {
            if (!model.IsModelValid(ModelState))
            {
                return PartialView("_InvalidLeasingParamsPartial", model);
            }

            var parameters = GetParamsFromModel(model);
            LeasingCalcResults leasingResult = this.calculatorService.CalculateLeasing(parameters);
            LeasingCalcResultsVM leasingViewModelResult = GetResultsForModel(leasingResult);

            return PartialView("_LeasingResultsPartial", leasingViewModelResult);
        }

        private LeasingCalcParams GetParamsFromModel(LeasingCalcParamsVM model)
        {
            LeasingCalcParams res = new LeasingCalcParams();

            res.Price = model.Price ?? 0;
            res.InitialInstallment = model.InitialInstallment ?? 0;
            res.Period = model.Period ?? 0;
            res.MonthlyInstallment = model.MonthlyInstallment ?? 0;
            res.InitialManagementFee = model.InitialManagementFee;
            res.TreatInitialManagementFeeAsPercent = model.TreatInitialManagementFeeAsPercent;

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

        private RefinancingCalcParams GetParamsFromModel(RefinancingCalcParamsVM model)
        {
            RefinancingCalcParams res = new RefinancingCalcParams();

            res.CurrentCreditAmount = model.CurrentCreditAmount ?? 0;
            res.CurrentCreditMadeInstallments = model.CurrentCreditMadeInstallments ?? 0;
            res.CurrentCreditPeriod = model.CurrentCreditPeriod ?? 0;
            res.CurrentCreditPreTermFee = model.CurrentCreditPreTermFee;
            res.CurrentCreditRate = model.CurrentCreditRate;
            res.NewCreditInitialFeesCurrency = model.NewCreditInitialFeesCurrency;
            res.NewCreditInitialFeesPercent = model.NewCreditInitialFeesPercent;
            res.NewCreditRate = model.NewCreditRate;

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

        private CreditCalcParams GetParamsFromModel(CreditCalculatorParamsVM model)
        {
            CreditCalcParams res = new CreditCalcParams();

            res.Amount = model.Amount ?? 0;
            res.Period = model.Period ?? 0;
            res.Rate = model.Rate ?? 0;
            res.PromotionPeriod = model.PromotionPeriod;
            res.PromotionRate = model.PromotionRate;
            res.GratisPeriod = model.GratisPeriod;
            res.IsAnnuityInstallments = model.IsAnnuityInstallments;
            res.ApplicationFee = model.ApplicationFee;
            res.TreatApplicationFeeAsPercent = model.TreatApplicationFeeAsPercent;
            res.ProcessingFee = model.ProcessingFee;
            res.TreatProcessingFeeAsPercent = model.TreatProcessingFeeAsPercent;
            res.OtherInitialFees = model.OtherInitialFees;
            res.TreatOtherInitialFeesAsPercent = model.TreatOtherInitialFeesAsPercent;
            res.MonthlyManagementFee = model.MonthlyManagementFee;
            res.TreatMonthlyManagementFeeAsPercent = model.TreatMonthlyManagementFeeAsPercent;
            res.OtherMonthlyFees = model.OtherMonthlyFees;
            res.TreatOtherMonthlyFeesAsPercent = model.TreatOtherMonthlyFeesAsPercent;
            res.AnnualManagementFee = model.AnnualManagementFee;
            res.TreatAnnualManagementFeeAsPercent = model.TreatAnnualManagementFeeAsPercent;
            res.OtherAnnualFees = model.OtherAnnualFees;
            res.TreatOtherAnnualFeesAsPercent = model.TreatOtherAnnualFeesAsPercent;

            return res;
        }

        private CreditCalculatorResultVM GetResultsForModel(CreditCalcResults res)
        {
            CreditCalculatorResultVM mRes = new CreditCalculatorResultVM();
            mRes.AnnualPercentageRate = res.AnnualPercentageRate;

            mRes.TotalFees = res.TotalFees;
            mRes.TotalInstallments = res.TotalInstallments;
            mRes.TotalInstallmentsWithTotalFeesAndRates = res.TotalInstallmentsWithTotalFeesAndRates;
            mRes.TotalRates = res.TotalRates;
            mRes.MonthlyInstallments = new List<MonthlyResultVM>();

            foreach (var m in res.MonthlyInstallments)
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
