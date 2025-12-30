using FOS.Setup;
using FOS.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FOS.DataLayer;
using FluentValidation.Results;
using FOS.Web.UI.Models;
using FOS.Web.UI.Common.CustomAttributes;

using FOS.Web.UI.DataSets;
using CrystalDecisions.CrystalReports.Engine;
using Shared.Diagnostics.Logging;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Reporting.WebForms;
using System.Text;
using System.Transactions;
using FOS.Setup.Validation;

namespace FOS.Web.UI.Controllers
{
    public class AdminController : Controller
    {
        #region Department
        [CustomAuthorize]
        public ActionResult Department()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUpdateDepartment([Bind(Exclude = "TID")] AdminData newDepartment)
        {
            Boolean boolFlag = true;
            ValidationResult results = new ValidationResult();
            try
            {
                if (newDepartment != null)
                {
                    if (newDepartment.ID == 0)
                    {
                        DepartmentValidator validator = new DepartmentValidator();
                        results = validator.Validate(newDepartment);
                        boolFlag = results.IsValid;
                    }

                    if (boolFlag)
                    {
                        int Response = ManageAdmin.AddUpdateDepartment(newDepartment);

                        if (Response == 1)
                        {
                            return Content("1");
                        }
                        else if (Response == 2)
                        {
                            return Content("2");
                        }
                        else
                        {
                            return Content("0");
                        }
                    }
                    else
                    {
                        IList<ValidationFailure> failures = results.Errors;
                        StringBuilder sb = new StringBuilder();
                        sb.Append(String.Format("{0}:{1}", "*** Error ***", "<br/>"));
                        foreach (ValidationFailure failer in results.Errors)
                        {
                            sb.AppendLine(String.Format("{0}:{1}{2}", failer.PropertyName, failer.ErrorMessage, "<br/>"));
                            Response.StatusCode = 422;
                            return Json(new { errors = sb.ToString() });
                        }
                    }
                }

                return Content("0");
            }
            catch (Exception exp)
            {
                return Content("Exception : " + exp.Message);
            }
        }
        //Get All Region Method...
        public JsonResult DepartmentDataHandler(DTParameters param)
        {
            try
            {
                var dtsource = new List<AdminData>();

                dtsource = ManageAdmin.GetDepartmentForGrid();

                List<String> columnSearch = new List<string>();

                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }

                List<AdminData> data = ManageAdmin.GetResult(param.Search.Value, param.SortOrder, param.Start, param.Length, dtsource, columnSearch);
                int count = ManageAdmin.Count(param.Search.Value, dtsource, columnSearch);
                DTResult<AdminData> result = new DTResult<AdminData>
                {
                    draw = param.Draw,
                    data = data,
                    recordsFiltered = count,
                    recordsTotal = count
                };
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        //Delete Department Method...
        public int DeleteDepartment(int DepartmentID)
        {
            return FOS.Setup.ManageAdmin.DeleteDepartment(DepartmentID);
        }
    
        #endregion

        #region Sub Department
        public ActionResult SubDepartment()
        {
            SubDepartmentData data = new SubDepartmentData();
            data.Dept = ManageAdmin.GetAllDepartment();
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUpdateSubDepartment([Bind(Exclude = "TID")] SubDepartmentData newSubDepartment)
        {
            Boolean boolFlag = true;
            ValidationResult results = new ValidationResult();
            try
            {
                if (newSubDepartment != null)
                {
                    if (newSubDepartment.ID == 0)
                    {
                       FOSDataModel db = new FOSDataModel();
                       var checkData = db.SubDepartments.Where(u => u.Name == newSubDepartment.SubDepartment && u.DeptID == newSubDepartment.DeptID).FirstOrDefault();

                       if (checkData == null)
                       {
                           boolFlag = true;
                       }
                       else
                       {
                           boolFlag = false;
                       }
                    }

                    if (boolFlag == true)
                    {
                        int Response = ManageAdmin.AddUpdateSubDepartment(newSubDepartment);

                        if (Response == 1)
                        {
                            return Content("1");
                        }
                        else if (Response == 2)
                        {
                            return Content("2");
                        }
                        else
                        {
                            return Content("0");
                        }
                    }
                    else
                    {
                        return Content("2");
                    }
                }

                return Content("0");
            }
            catch (Exception exp)
            {
                return Content("Exception : " + exp.Message);
            }
        }
        //Get All Region Method...
        public JsonResult SubDepartmentDataHandler(DTParameters param)
        {
            try
            {
                var dtsource = new List<SubDepartmentData>();

                dtsource = ManageAdmin.GetSubDepartmentForGrid();

                List<String> columnSearch = new List<string>();

                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }

                List<SubDepartmentData> data = ManageAdmin.GetResultSubDepartment(param.Search.Value, param.SortOrder, param.Start, param.Length, dtsource, columnSearch);
                int count = ManageAdmin.CountSubDepartment(param.Search.Value, dtsource, columnSearch);
                DTResult<SubDepartmentData> result = new DTResult<SubDepartmentData>
                {
                    draw = param.Draw,
                    data = data,
                    recordsFiltered = count,
                    recordsTotal = count
                };
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        //Delete Department Method...
        public int DeleteSubDepartment(int ID)
        {
            return FOS.Setup.ManageAdmin.DeleteSubDepartment(ID);
        }
        #endregion

        #region Designation
        [CustomAuthorize]
        public ActionResult Designation()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUpdateDesignation([Bind(Exclude = "TID")] DesignationData newDepartment)
        {
            Boolean boolFlag = true;
            ValidationResult results = new ValidationResult();
            try
            {
                if (newDepartment != null)
                {
                    if (newDepartment.ID == 0)
                    {
                        DesignationValidator validator = new DesignationValidator();
                        results = validator.Validate(newDepartment);
                        boolFlag = results.IsValid;
                    }

                    if (boolFlag)
                    {
                        int Response = ManageAdmin.AddUpdateDesignation(newDepartment);

                        if (Response == 1)
                        {
                            return Content("1");
                        }
                        else if (Response == 2)
                        {
                            return Content("2");
                        }
                        else
                        {
                            return Content("0");
                        }
                    }
                    else
                    {
                        IList<ValidationFailure> failures = results.Errors;
                        StringBuilder sb = new StringBuilder();
                        sb.Append(String.Format("{0}:{1}", "*** Error ***", "<br/>"));
                        foreach (ValidationFailure failer in results.Errors)
                        {
                            sb.AppendLine(String.Format("{0}:{1}{2}", failer.PropertyName, failer.ErrorMessage, "<br/>"));
                            Response.StatusCode = 422;
                            return Json(new { errors = sb.ToString() });
                        }
                    }
                }

                return Content("0");
            }
            catch (Exception exp)
            {
                return Content("Exception : " + exp.Message);
            }
        }
        //Get All Region Method...
        public JsonResult DesignationDataHandler(DTParameters param)
        {
            try
            {
                var dtsource = new List<DesignationData>();

                dtsource = ManageAdmin.GetDesignationForGrid();

                List<String> columnSearch = new List<string>();

                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }

                List<DesignationData> data = ManageAdmin.GetResult(param.Search.Value, param.SortOrder, param.Start, param.Length, dtsource, columnSearch);
                int count = ManageAdmin.Count(param.Search.Value, dtsource, columnSearch);
                DTResult<DesignationData> result = new DTResult<DesignationData>
                {
                    draw = param.Draw,
                    data = data,
                    recordsFiltered = count,
                    recordsTotal = count
                };
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        //Delete Department Method...
        public int DeleteDesignation(int DesignationID)
        {
            return FOS.Setup.ManageAdmin.DeleteDesignation(DesignationID);
        }

        #endregion

        #region Doctor
        public ActionResult Doctor()
        {


            DoctorData obj = new DoctorData();
            List<Department> AllDept = ManageAdmin.GetAllDepartment();
            obj.Dept = AllDept;
            List<SubDepartmentData> AllSubDept = ManageAdmin.GetSubDepartment();
            obj.SubDept = AllSubDept;
            List<DesignationData> AllDesig = ManageAdmin.GetAllDesignation();
            obj.Designation = AllDesig;
            //List<Day> dayObj = ManageAdmin.GetAllDay();
            //obj.Days = dayObj;
            return View(obj);
        }
        public JsonResult GetSubDepartmentListByDeptWise(int DeptID)
        {
            var result = ManageAdmin.GetSubDepartmentDepartmentWise(DeptID);
            return Json(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUpdateDoctor([Bind(Exclude = "TID")] DoctorData newDoctor)
        {
            Boolean boolFlag = true;
            ValidationResult results = new ValidationResult();
            try
            {
                if (newDoctor != null)
                {
                    if (newDoctor.EmpID == 0)
                    {
                        FOSDataModel db = new FOSDataModel();
                        var checkData = db.Employees.Where(u => u.EFName == newDoctor.EFName && u.ELName == newDoctor.ELName && u.CNIC == newDoctor.CNIC).FirstOrDefault();

                        if (checkData == null)
                        {
                            boolFlag = true;
                        }
                        else
                        {
                            boolFlag = false;
                        }
                    }

                    if (boolFlag == true)
                    {
                        if (newDoctor.DrImg != null)
                        {
                            string TitleFileName = Path.GetFileNameWithoutExtension(newDoctor.DrImg.FileName);
                            string TitleFileExtenstion = Path.GetExtension(newDoctor.DrImg.FileName);
                            TitleFileName = DateTime.Now.ToString("yyyyMMdd") + "-" + TitleFileName.Trim() + TitleFileExtenstion;
                            var uploadPath = "E:/Online Appointment System/ODAS1.2/FOS.Web.UI/Images/";
                            newDoctor.DrImgPath = uploadPath + TitleFileName;
                            newDoctor.DrImg.SaveAs(newDoctor.DrImgPath);
                        }
                        int Response = ManageAdmin.AddUpdateDoctor(newDoctor);

                        if (Response == 1)
                        {
                            Content("<script language='javascript' type='text/javascript'>alert('Save Successfully!');</script>");
                            return RedirectToAction("Doctor", "Admin");
                            
                        }
                        else if (Response == 2)
                        {
                            return RedirectToAction("Doctor", "Admin");
                        }
                        else
                        {
                            return RedirectToAction("Doctor", "Admin");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Doctor", "Admin");
                    }
                }

                return RedirectToAction("Doctor", "Admin");
            }
            catch (Exception exp)
            {
                return Content("Exception : " + exp.Message);
            }
        }
        public JsonResult DoctorDataHandler(DTParameters param)
        {
            try
            {
                var dtsource = new List<DoctorData>();

                dtsource = ManageAdmin.GetDoctorForGrid();

                List<String> columnSearch = new List<string>();

                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }

                List<DoctorData> data = ManageAdmin.GetResultDoctor(param.Search.Value, param.SortOrder, param.Start, param.Length, dtsource, columnSearch);
                int count = ManageAdmin.CountDoctor(param.Search.Value, dtsource, columnSearch);
                DTResult<DoctorData> result = new DTResult<DoctorData>
                {
                    draw = param.Draw,
                    data = data,
                    recordsFiltered = count,
                    recordsTotal = count
                };
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        #endregion

        #region Doctor Slot
        public ActionResult CreateDoctorSlots()
        {
            DoctorSlotsData obj = new DoctorSlotsData();
            var Dept = ManageAdmin.GetAllDepartment();
            var DeptObj = Dept.FirstOrDefault();
            obj.Departments = Dept;
            var DoctorData = ManageAdmin.GetAllDoctor(DeptObj.ID);
            var DoctorID = DoctorData.FirstOrDefault();
            obj.Doctor = DoctorData;
            obj.Days = ManageAdmin.GetDoctorWorkingDays(DoctorID.EmpID);
            obj.Slot = ManageAdmin.GetAllSlot();
            return View(obj);
        }
        public JsonResult GetDoctorSlot(DateTime StartTime, DateTime EndTime, double Duration)
        {
            var STime = StartTime;
            var ETime = EndTime;
            var dtNext = StartTime;

            //var newdate = new DateTime(STime);
            //double duration = 15;
            var datetimeList2 = new List<Slot>();
            var datetimeList = new List<string>();
            while (dtNext <= ETime)
            {
                string timeSlot = dtNext.ToShortTimeString();
                var slots =  new Slot
                {
                    ID = 0,
                    Name = timeSlot
                };
                datetimeList2.Add(slots);
                dtNext = dtNext.AddMinutes(Duration);
                
            }

            return Json(datetimeList2);
        }
        public JsonResult GetDoctorDepartmentWise(int DeptID)
        {
            var DoctorList = ManageAdmin.GetAllDoctor(DeptID);
            return Json(DoctorList);
        }
        public JsonResult GetDoctorDays(int DoctorID)
        {
            var DoctorDays = ManageAdmin.GetDoctorWorkingDays(DoctorID);
            return Json(DoctorDays);
        }
        [HttpPost]
        public ActionResult AddUpdateDoctorSlot([Bind(Exclude="TID")] DoctorSlotsData slotObj)
        {
            bool checkStatus = true;
            try
            {
                using(FOSDataModel db = new FOSDataModel())
                {
                    var data = db.DoctorIncomeOutcomes.Where(u => u.ID == slotObj.ID).FirstOrDefault();
                    if(data == null)
                    {
                        checkStatus = true;
                    }
                    else
                    {
                        checkStatus = false;
                    }
                }
                if(checkStatus == true)
                {
                    try
                    {
                        using(TransactionScope ts = new TransactionScope())
                        {
                            using(FOSDataModel db = new FOSDataModel())
                            {
                                DoctorIncomeOutcome DoctorSlotObj = new DoctorIncomeOutcome();
                                if(slotObj.ID == 0)
                                {
                                    DoctorSlotObj.ID = db.DoctorIncomeOutcomes.OrderByDescending(u => u.ID).Select(x => x.ID).FirstOrDefault() + 1;
                                    DoctorSlotObj.StartingTime = slotObj.StartingTime;
                                    DoctorSlotObj.EndingTime = slotObj.EndingTime;
                                    DoctorSlotObj.DoctorID = slotObj.DoctorID;
                                    DoctorSlotObj.DepartmentID = slotObj.DeptID;
                                    DoctorSlotObj.DayID = slotObj.DayID;
                                    DoctorSlotObj.IsActive = true;
                                    DoctorSlotObj.IsDeleted = false;
                                    DoctorSlot dsObj;
                                    string[] slots = slotObj.SlotID.Split(',');
                              

                                    foreach (var Slot in slots)
                                    {
                                        dsObj = new DoctorSlot();
                                        //dsObj.ID = db.DoctorSlots.OrderByDescending(u=> u.ID).Select(x=> x.ID).FirstOrDefault() + 1;
                                        //dsObj.IOID = DoctorSlotObj.ID;03021795144 03084517927
                                        dsObj.DoctorID = DoctorSlotObj.DoctorID;
                                        dsObj.Slot = Slot.ToString();
                                        dsObj.DayID = DoctorSlotObj.DayID;
                                        dsObj.IsActive = true;
                                        dsObj.IsDeleted = false;
                                        db.DoctorSlots.Add(dsObj);
                                    }
                                    db.DoctorIncomeOutcomes.Add(DoctorSlotObj);
                                    db.SaveChanges();
                                    //Res = 1;
                                    ts.Complete();
                                    return Content("1");
                                }
                            }
                        }
                    }
                    catch(Exception ex)
                    {

                    }
                }
            }
            catch(Exception ex)
            {

            }
            return Content("0");
        }
        #endregion

        #region AppointmentRequest
        public ActionResult AppointementRequest()
        {
            RequestData obj = new RequestData();
            List<Department> department = ManageAdmin.GetAllDepartment();
            obj.Dept = department;
            int DeptID = 0;
            List<DoctorData> employees = ManageAdmin.GetAllDoctor(DeptID);
            var Doc = employees.FirstOrDefault();
            obj.Doctor = employees;
            int day = 0;
            obj.SlotName = ManagePatient.GetAllSlot(Doc.EmpID, day);
            //obj.Slot = ManageAdmin.GetAllSlot();
            return View(obj);
        }
        public JsonResult LoadPatientReport(int DoctorID,DateTime FromDate, DateTime ToDate)
        {
            FOSDataModel db = new FOSDataModel();

            //DateTime start = Convert.ToDateTime(string.IsNullOrEmpty(StartingDate) ? DateTime.Now.ToString() : StartingDate);
            //DateTime end = Convert.ToDateTime(string.IsNullOrEmpty(EndingDate) ? DateTime.Now.ToString() : EndingDate);
            //DateTime final = end.AddDays(1);
            Microsoft.Reporting.WebForms.LocalReport ReportViewer1 = new Microsoft.Reporting.WebForms.LocalReport();

            try
            {

                List<GetPatientDocrorWise1_1_Result> data = db.GetPatientDocrorWise1_1(DoctorID,FromDate,ToDate).ToList();

                //ReportParameter[] prm = new ReportParameter[3];
                //prm[0] = new ReportParameter("DateFrom", StartingDate);
                //prm[1] = new ReportParameter("DateTo", EndingDate);
                //prm[2] = new ReportParameter("SaleOfficer", SaleOfficer);
                ReportViewer1.ReportPath = Server.MapPath("~/Views/Reports/AllPatientReport.rdlc");
                ReportViewer1.EnableExternalImages = true;
                ReportDataSource dt2 = new ReportDataSource("DataSet1", data);
                //ReportViewer1.SetParameters(prm);
                ReportViewer1.DataSources.Clear();
                ReportViewer1.DataSources.Add(dt2);
                ReportViewer1.Refresh();



                Warning[] warnings;
                string[] streamIds;
                string contentType;
                string encoding;
                string extension;

                //Export the RDLC Report to Byte Array.
                byte[] bytes = ReportViewer1.Render("PDF", null, out contentType, out encoding, out extension, out streamIds, out warnings);
                //using (FileStream fs = File.Create(Server.MapPath("~/download/") + dt2))
                //{
                //    fs.Write(bytes, 0, bytes.Length);
                //}
                //Download the RDLC Report in Word, Excel, PDF and Image formats.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = contentType;
                Response.AddHeader("content-disposition", "attachment;filename=PatientReport" + DateTime.Now + ".Pdf");
                Response.BinaryWrite(bytes);
                Response.Flush();

                Response.End();
                //return Json(0);
            }

            catch (Exception exp)
            {
                Log.Instance.Error(exp, "Report Not Working");

            }
            return Json(0);
        }
        public JsonResult PatientReport(int PatientID)
        {
            FOSDataModel db = new FOSDataModel();

            //DateTime start = Convert.ToDateTime(string.IsNullOrEmpty(StartingDate) ? DateTime.Now.ToString() : StartingDate);
            //DateTime end = Convert.ToDateTime(string.IsNullOrEmpty(EndingDate) ? DateTime.Now.ToString() : EndingDate);
            //DateTime final = end.AddDays(1);
            Microsoft.Reporting.WebForms.LocalReport ReportViewer1 = new Microsoft.Reporting.WebForms.LocalReport();

            try
            {

                List<GetPatientForReport_Result> data = db.GetPatientForReport(PatientID).ToList();

                //ReportParameter[] prm = new ReportParameter[3];
                //prm[0] = new ReportParameter("DateFrom", StartingDate);
                //prm[1] = new ReportParameter("DateTo", EndingDate);
                //prm[2] = new ReportParameter("SaleOfficer", SaleOfficer);
                ReportViewer1.ReportPath = Server.MapPath("~/Views/Reports/PatientReport.rdlc");
                ReportViewer1.EnableExternalImages = true;
                ReportDataSource dt2 = new ReportDataSource("DataSet1", data);
                //ReportViewer1.SetParameters(prm);
                ReportViewer1.DataSources.Clear();
                ReportViewer1.DataSources.Add(dt2);
                ReportViewer1.Refresh();



                Warning[] warnings;
                string[] streamIds;
                string contentType;
                string encoding;
                string extension;

                //Export the RDLC Report to Byte Array.
                byte[] bytes = ReportViewer1.Render("PDF", null, out contentType, out encoding, out extension, out streamIds, out warnings);
                //using (FileStream fs = File.Create(Server.MapPath("~/download/") + dt2))
                //{
                //    fs.Write(bytes, 0, bytes.Length);
                //}
                //Download the RDLC Report in Word, Excel, PDF and Image formats.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = contentType;
                Response.AddHeader("content-disposition", "attachment;filename=PatientReport" + DateTime.Now + ".Pdf");
                Response.BinaryWrite(bytes);
                Response.Flush();

                Response.End();
                //return Json(0);
            }

            catch (Exception exp)
            {
                Log.Instance.Error(exp, "Report Not Working");

            }
            return Json(0);
        }
        public JsonResult AppointmentRequestHandler(DTParameters param, int DoctorID, DateTime StartingDate, DateTime EndingDate)
        {
            try
            {
                var dtsource = new List<RequestData>();
                RequestData rd = new RequestData();
                //var lastname = sessionStorage.getItem("key");
                int DID = DoctorID;
                DateTime from = StartingDate;
                DateTime to = EndingDate;
                dtsource = ManageAdmin.GetAppointmentForGrid(DID,from,to);

                List<String> columnSearch = new List<string>();

                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }

                List<RequestData> data = ManageAdmin.GetAppointmentResult(param.Search.Value, param.SortOrder, param.Start, param.Length, dtsource, columnSearch);
                int count = ManageAdmin.AppointmentCount(param.Search.Value, dtsource, columnSearch);
                DTResult<RequestData> result = new DTResult<RequestData>
                {
                    draw = param.Draw,
                    data = data,
                    recordsFiltered = count,
                    recordsTotal = count
                };
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        public ActionResult UpdateAppointment([Bind(Exclude = "TID")] RequestData newDepartment)
        {
            Boolean boolFlag = true;
            ValidationResult results = new ValidationResult();
            try
            {
                if (newDepartment != null)
                {
                  

                    if (boolFlag)
                    {
                        int Response = ManageAdmin.UpdatePatientRequest(newDepartment);
                     
                        if (Response == 1)
                        {
                            return Content("1");
                        }
                        else if (Response == 2)
                        {
                            return Content("2");
                        }
                        else
                        {
                            return Content("0");
                        }
                    }
                    else
                    {
                        IList<ValidationFailure> failures = results.Errors;
                        StringBuilder sb = new StringBuilder();
                        sb.Append(String.Format("{0}:{1}", "*** Error ***", "<br/>"));
                        foreach (ValidationFailure failer in results.Errors)
                        {
                            sb.AppendLine(String.Format("{0}:{1}{2}", failer.PropertyName, failer.ErrorMessage, "<br/>"));
                            Response.StatusCode = 422;
                            return Json(new { errors = sb.ToString() });
                        }
                    }
                }

                return Content("0");
            }
            catch (Exception exp)
            {
                return Content("Exception : " + exp.Message);
            }
        }
        #endregion

        #region AvailableDoctorSlot
        public ActionResult AvailableDoctor()
        {
            RequestData obj = new RequestData();
            List<Department> department = ManageAdmin.GetAllDepartment();
            obj.Dept = department;
            int DeptID = 0;
            List<SubDepartmentData> AllSubDept = ManageAdmin.GetSubDepartment();
            obj.SubDept = AllSubDept;
            List<DoctorData> employees = ManageAdmin.GetAllDoctor(DeptID);
            obj.Doctor = employees;
            obj.Slots = ManageAdmin.GetAllSlot();
            return View(obj);
        }

        public JsonResult AvailableDoctorRequestHandler(DTParameters param, int DoctorID, DateTime StartingDate)
        {
            try
            {
                DateTime now = Convert.ToDateTime(StartingDate);
                string s = now.DayOfWeek.ToString();
                FOSDataModel db = new FOSDataModel();
                var SelectedDayID = db.Days.Where(x => x.Name == s).Select(u => u.ID).FirstOrDefault();
                int DayID = Convert.ToInt32(SelectedDayID);

                var dtsource = new List<RequestData>();
                RequestData rd = new RequestData();
                //var lastname = sessionStorage.getItem("key");
                int DID = DoctorID;
                DateTime from = StartingDate;
                //DateTime to = EndingDate;
                dtsource = ManageAdmin.GetAvailableDoctorForGrid(DID, from, DayID);

                List<String> columnSearch = new List<string>();

                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }

                List<RequestData> data = ManageAdmin.GetAvailableDoctorResult(param.Search.Value, param.SortOrder, param.Start, param.Length, dtsource, columnSearch);
                int count = ManageAdmin.AvailableDoctorCount(param.Search.Value, dtsource, columnSearch);
                DTResult<RequestData> result = new DTResult<RequestData>
                {
                    draw = param.Draw,
                    data = data,
                    recordsFiltered = count,
                    recordsTotal = count
                };
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
     
        #endregion

    }
}
