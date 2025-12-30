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
            DateTime serverTime = DateTime.Now; // gives you current Time in server timeZone
            DateTime utcTime = serverTime.ToUniversalTime(); // convert it to Utc using timezone setting of server computer

            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi);
            localTime = localTime.Date;
            
            DateTime dateTo = localTime.AddDays(1);
            ViewBag.patient = ManageHome.GetTotalPatientGrid(Settings.AppPath).Count();
            //var jobs = ManageJobs.GetJobsToExportInExcel();
            DateTime now = DateTime.Now;

            var startDate = new DateTime(localTime.Year, localTime.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            var today = DateTime.Today;
            var month = new DateTime(today.Year, today.Month, 1);
            var first = month.AddMonths(-1);
            var SDate = month.AddMonths(-6);
            var last = month.AddDays(-1);

            // Last Month Sales
            var AppointmentsThisMonth = (from lm in db.Appointments
                             where lm.AppointmentDate >= startDate
                             && lm.AppointmentDate <= endDate
                             && lm.IsDeleted==false && lm.IsActive==true
                             select lm).ToList();

            // New Customers Today

            var TotalAppointmentToday = (from lm in db.Appointments
                                  where lm.AppointmentDate >= localTime && lm.AppointmentDate <= dateTo && lm.IsActive==true && lm.IsDeleted==false
                                  select lm).ToList();

            ////// Current Month Order Delievered
            var TotalPatientLastSixMonth = (from lm in db.Appointments
                                              where lm.AppointmentDate >= SDate
                                              && lm.AppointmentDate <= DateTime.Now
                                              select lm.ID).ToList();

            var PreviousMonthTotalAppointment = (from lm in db.Appointments
                                              where lm.AppointmentDate >= first
                                              && lm.AppointmentDate <= last
                                              //&& lm.VisitType == "Sampling"
                                              select lm.ID).ToList();

            //var SamplesDeliveredToday =     (from lm in db.VisitLogs
            //                                      where lm.VisitDate >= localTime
            //                                      && lm.VisitDate <= dateTo
            //                                      && lm.VisitType == "Sampling"
            //                                      select lm.JobID).ToList();




            ViewBag.AppointmentsThisMonth = AppointmentsThisMonth.Count();
            ViewBag.TotalAppointmentToday = TotalAppointmentToday.Count();
            ViewBag.TotalPatientLastSixM = TotalPatientLastSixMonth.Count();
            ViewBag.PreviousMonthAppointment = PreviousMonthTotalAppointment.Count();
            //ViewBag.SamplesDeliveredToday = SamplesDeliveredToday.Count();
            ViewBag.TotalVisits = Dashboard.TotalVisits().Count();
            ViewBag.SoPresentToday = Dashboard.SoPresentToday().Count();
            //ViewBag.TotalFirstVisits = Dashboard.TotalFirstvisits().Count();
            //ViewBag.TotalSampling = Dashboard.TotalSamplingToday().Count();
            ViewBag.TotalFollowUps = Dashboard.TotalFollowUpsToday().Count();
            //ViewBag.TotalAdditionSamples = Dashboard.TotalAdditionSamplesToday().Count();
            //ViewBag.TotalFinalVisits = Dashboard.TotalFinalVisitsToday().Count();
            ManageHome objHome = new ManageHome();
            var datetime = DateTime.Now;
            List<TotalAppointmentDeparmentWiseToday_Result> result = objHome.TotalAppointmentToday();
            List<Sp_Top10VisitsCityWiseGraph1_1_Result> citywise = objHome.TopSalesCityWise(localTime);
            //List<Sp_NoVisitsToday_Result> NoVisits = db.Sp_NoVisitsToday().ToList();
            //List<Sp_Top10CustomerVisitSOWise1_1_Result> result1 = objRetailers.TopSales(localTime);
            //List<Sp_Top10VisitsCityWiseGraph_Result> citywise = objRetailers.TopSalesCityWise(localTime);
            //List<Sp_PresentSOBarGraph1_1_Result> PresentSO = objRetailers.TotalPresentSO();
            //List<Sp_NoVisitsToday_Result> NoVisits = db.Sp_NoVisitsToday().ToList();
            //List<Sp_SOVisitsTodayFinal_Result> SOVisits = objRetailers.SOVisitsToday();
            //ViewBag.DataPoints = JsonConvert.SerializeObject(result1);
            //ViewBag.DataPoints1 = JsonConvert.SerializeObject(citywise);
            //ViewBag.DataPoints2 = JsonConvert.SerializeObject(NoVisits);
            //ViewBag.DataPoints3 = JsonConvert.SerializeObject(SOVisits);
            ViewBag.DataPoints = JsonConvert.SerializeObject(result);
            ViewBag.DataPoints1 = JsonConvert.SerializeObject(citywise);
            ViewBag.DataPoints2 = JsonConvert.SerializeObject(citywise);
            ViewBag.DataPoints3 = JsonConvert.SerializeObject(citywise);
            return View();
        }

        [CustomAuthorize]
        public ActionResult UserHome()
        {
            return View();
        }

        public JsonResult RetailerGraph()
        {
            RetailerGraphData result;
            if (RegionalheadID == 0)
            {
                result = FOS.Setup.Dashboard.RetailerGraph();
            }
            else
            {
                result = FOS.Setup.Dashboard.RetailerGraph(RegionalheadID);
            }
            return Json(result);
        }

        public JsonResult JobsGraph()
        {
            JobGraphData result;
            if (RegionalheadID == 0)
            {
                result = FOS.Setup.Dashboard.JobsGraph();
            }
            else
            {
                result = FOS.Setup.Dashboard.JobsGraph(RegionalheadID);
            }
            return Json(result);
        }

        //public JsonResult CityGraph()
        //{
        //    List<CityGraphData> result = FOS.Setup.Dashboard.CityGraph();
        //    return Json(result);
        //}

        public JsonResult AreaGraph()
        {
            List<AreaGraphData> result = FOS.Setup.Dashboard.AreaGraph();
            return Json(result);
        }

        //public JsonResult RegionalHeadGraph()
        //{
        //    List<RegionalHeadGraphData> result = FOS.Setup.Dashboard.RegionalHeadGraph();
        //    return Json(result);
        //}

        public int SalesOfficerGraph()
        {
            if (RegionalheadID == 0)
            {
                return FOS.Setup.Dashboard.SalesOfficerGraph();
            }
            else
            {
                return FOS.Setup.Dashboard.SalesOfficerGraph(RegionalheadID);
            }
        }

        public int DealerGraph()
        {
            return FOS.Setup.Dashboard.DealerGraph();
        }

        //public int GetCount()
        //{
        //    int count;
        //    var objRetailer = new RetailerData();
        //    if (RegionalheadID == 0)
        //    {
        //        count = FOS.Setup.ManageRetailer.GetPendingRetailerCountApproval();
        //    }
        //    else
        //    {
        //        count = FOS.Setup.ManageRetailer.GetPendingRetailerCountApproval(RegionalheadID);
        //    }
        //    return count;
        //}

        public int GetTotalRetailer()
        {
            int count;
            var objRetailer = new RetailerData();

            if (RegionalheadID == 0)
            {
                count = db.Retailers.Count();
            }
            else
            {
                count = db.Retailers.Where(r => r.SaleOfficer.RegionalHeadID == RegionalheadID).Count();
            }
            return count;
        }

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

        //public JsonResult SoJobGraph()
        //{
        //    List<SojobGraphData> result = FOS.Setup.Dashboard.SoJobGraph();
        //    return Json(result);
        //}

        public ActionResult Reports()
        {
            return View();
        }

        //public int GetCount()
        //{
        //    int count;
        //    var objRetailer = new RetailerData();

        //    count = FOS.Setup.ManageRetailer.GetDeletedRetailerCountApproval();

        //    return count;
        //}
    }
}