using FOS.DataLayer;
using FOS.Setup;
using FOS.Shared;
using FOS.Web.UI.Common;
using FOS.Web.UI.Common.CustomAttributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace FOS.Web.UI.Controllers
{
    public class HomeController : Controller
    {
        private FOSDataModel db = new FOSDataModel();
        private static int _regionalHeadID = 0;

        private static int RegionalheadID
        {
            get
            {
                if (_regionalHeadID == 0)
                {
                    _regionalHeadID = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
                }

                return _regionalHeadID;
            }
        }

        [CustomAuthorize]
        public ActionResult Home()
        {
            // ViewBag.rptid = "";
            //ViewBag.retailers = ManageRetailer.GetRetailerForGrid(Settings.AppPath).Count();
            //var jobs = ManageJobs.GetJobsToExportInExcel();
            //var today = DateTime.Today;
            //var max = new DateTime(today.Year, today.Month, 1); // first of this month
            //var min = max.AddMonths(-1); // first of last month
            //var firstDayOfMonth = new DateTime(today.Year, today.Month, 1);
            //var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            //var today = DateTime.Today;
            //var month = new DateTime(today.Year, today.Month, 1);
            //var first = month.AddMonths(-1);
            //var last = month.AddDays(-1);
            //var endDate = month.AddMonths(1).AddDays(-1);
            DateTime serverTime = DateTime.Now; // gives you current Time in server timeZone
            DateTime utcTime = serverTime.ToUniversalTime(); // convert it to Utc using timezone setting of server computer

            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi);

            //DateTime now = TimeZone.CurrentTimeZone.ToLocalTime(DateTime.Now);
            var startDate = new DateTime(localTime.Year, localTime.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
         //   var today = DateTime.Today;
            DateTime serverT = DateTime.Today; // gives you current Time in server timeZone
            DateTime utcTimee = serverTime.ToUniversalTime(); // convert it to Utc using timezone setting of server computer

            TimeZoneInfo tzii = TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");
            DateTime today = TimeZoneInfo.ConvertTimeFromUtc(utcTimee, tzii);
            var today1 = today.Date;
         //   var today = localTime;
            var month = new DateTime(today.Year, today.Month, 1);
            var first = month.AddMonths(-1);
            var last = month.AddDays(-1);

            // Last Month Sales
            //var lastMonth = (from lm in db.Retailers
            //                 where lm.LastUpdate >= startDate
            //                 && lm.LastUpdate <= endDate
                             
            //                 select lm).ToList();

            // New Customers Today

            //var TodaysCustomer = (from lm in db.Retailers
            //                      where lm.LastUpdate >= today1 && lm.LastUpdate <= localTime && lm.Status == true
            //                      select lm).ToList();

            //// Current Month Order Delievered
            //var ThismonthSamplerDelievered = (from lm in db.JobsDetails join 
            //                                ji in db.JobItems on lm.JobID equals ji.JobID
            //                                where lm.JobDate >= startDate
            //                                && lm.JobDate <= endDate
            //                                && lm.VisitPurpose == "Sampling"
            //                                select ji.JobID).ToList();

            //var PreviousMonthSamplerDelievered = (from lm in db.JobsDetails
            //                                  join ji in db.JobItems on lm.JobID equals ji.JobID
            //                                  where lm.JobDate >= first
            //                                  && lm.JobDate <= last
            //                                  && lm.VisitPurpose == "Sampling"
            //                                  select ji.JobID).ToList();

            //var ThisMonthSampleDelievered =     (from lm in db.JobsDetails
            //                                      join ji in db.JobItems on lm.JobID equals ji.JobID
            //                                      where lm.JobDate == DateTime.Today
            //                                      && lm.VisitPurpose == "Sampling"
            //                                      select ji.JobID).ToList();


             

            //ViewBag.Lastmonthsale = lastMonth.Count();  
            //ViewBag.ThisMonthSale = TodaysCustomer.Count();
            //ViewBag.ThisMonthSaleDone = ThismonthSamplerDelievered.Count();
            //ViewBag.PreviousMonthSaleDone = PreviousMonthSamplerDelievered.Count();
            //ViewBag.TodaySaleDone = ThisMonthSampleDelievered.Count();
            //ViewBag.SOPresentToday = Dashboard.GetPresentSOToday().Count();
            //ViewBag.FSPlanndeToday = Dashboard.FSPlannedtoday().Count();
            //ViewBag.FSVisitedToday = Dashboard.FSVisitedtoday().Count(); 
            //ViewBag.RSPlannedToday = Dashboard.RSPlannedToday().Count();
            //ViewBag.RSVisitedToday = Dashboard.RSVisitedToday().Count();
            //ManageRetailer objRetailers = new ManageRetailer();
            //var datetime = localTime;
            //List<Sp_Top10CustomerVisitSOWise_Result> result = objRetailers.TopSales(datetime);
            //List<Sp_Top10CustomerVisitSOWise_Result> result1 = objRetailers.TopSales(datetime);
            //List<Sp_Top10VisitsCityWiseGraph_Result> citywise = objRetailers.TopSalesCityWise(datetime);
            //List<Sp_PresentSOBarGraph1_2_Result> PresentSO = objRetailers.TotalPresentSO();
            //List<Sp_SOVisitsToday1_1_Result> SOVisits = objRetailers.SOVisitsToday();
            //ViewBag.DataPoints = JsonConvert.SerializeObject(result1);
            //ViewBag.DataPoints1 = JsonConvert.SerializeObject(citywise);
            //ViewBag.DataPoints2 = JsonConvert.SerializeObject(PresentSO);
            //ViewBag.DataPoints3 = JsonConvert.SerializeObject(SOVisits);
            return View();
        }

        [CustomAuthorize]
        public ActionResult UserHome()
        {
            return View();
        }

        public JsonResult RetailerGraph()
        {
            //RetailerGraphData result;
            var result = 1;
            if (RegionalheadID == 0)
            {
                result = 1;
                //result = FOS.Setup.Dashboard.RetailerGraph();
            }
            else
            {
                //result = FOS.Setup.Dashboard.RetailerGraph(RegionalheadID);
            }
            return Json(result);
        }

        public JsonResult JobsGraph()
        {
            var result = 1;
            //JobGraphData result;
            //if (RegionalheadID == 0)
            //{
            //    result = FOS.Setup.Dashboard.JobsGraph();
            //}
            //else
            //{
            //    result = FOS.Setup.Dashboard.JobsGraph(RegionalheadID);
            //}
            return Json(result);
        }

        public JsonResult CityGraph()
        {
            //List<CityGraphData> result = FOS.Setup.Dashboard.CityGraph();
            var result = 1;
            return Json(result);
        }

        public JsonResult AreaGraph()
        {
            //List<AreaGraphData> result = FOS.Setup.Dashboard.AreaGraph();
            var result = 1;
            return Json(result);
        }

        public JsonResult RegionalHeadGraph()
        {
            //List<RegionalHeadGraphData> result = FOS.Setup.Dashboard.RegionalHeadGraph();
            var result = 1;
            return Json(result);
        }

        public int SalesOfficerGraph()
        {
            if (RegionalheadID == 0)
            {
                return 1;
                //return FOS.Setup.Dashboard.SalesOfficerGraph();
            }
            else
            {
                //return FOS.Setup.Dashboard.SalesOfficerGraph(RegionalheadID);
            }
            return 1;
        }

        public int DealerGraph()
        {
            return 1;
            //return FOS.Setup.Dashboard.DealerGraph(); 
        }

        public int GetCount()
        {
            int count = 1;
            //count = FOS.Setup.ManageRetailer.GetDeletedRetailerCountApproval();
            return count;
        }

        //public int GetTotalRetailer()
        //{
        //    int count;
        //    var objRetailer = new RetailerData();

        //    if (RegionalheadID == 0)
        //    {
        //        count = db.Retailers.Count();
        //    }
        //    else
        //    {
        //        count = db.Retailers.Where(r => r.SaleOfficer.RegionalHeadID == RegionalheadID).Count();
        //    }
        //    return count;
        //}

        //public int GetTotalJobs()
        //{
        //    var objJobs = new JobsDetailData();
        //    int count;

        //    if (RegionalheadID == 0)
        //    {
        //        count = db.JobsDetails.Where(jd => jd.Job.IsDeleted == false).Count();
        //    }
        //    else
        //    {
        //        count = db.JobsDetails.Where(j => j.RegionalHeadID == RegionalheadID && j.Job.IsDeleted == false).Count();
        //    }
        //    return count;
        //}

        public int GetTotalSalesOfficer()
        {
            int count;
            var objSalesOfficer = new SaleOfficerData();

            if (RegionalheadID == 0)
            {
                count = db.SaleOfficers.Count();
            }
            else
            {
                count = db.SaleOfficers.Where(s => s.RegionalHeadID == RegionalheadID).Count();
            }

            return count;
        }

        public JsonResult SoJobGraph()
        {
            //List<SojobGraphData> result = FOS.Setup.Dashboard.SoJobGraph();
            var result = 1;
            return Json(result);
        }
    }
}