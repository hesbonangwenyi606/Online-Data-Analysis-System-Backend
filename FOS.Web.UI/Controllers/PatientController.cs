using FOS.DataLayer;
using FOS.Setup;
using FOS.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Transactions;
using System.Web.Http;
using System.Web.Mvc;

namespace FOS.Web.UI.Controllers
{
    public class PatientController : Controller
    {
        public ActionResult Index()
        {
            FOSDataModel db = new FOSDataModel();
            DoctorData obj = new DoctorData();
            //var data = db.GetDoctorForGrid1_1().Select(x=> new DoctorData
            //{
            //    EmpID = x.EmpID,
            //    EFName = x.Name,
            //    Dept = 
            //})
            return View();
        }
        #region Appointment
        public ActionResult Appointment(int ID)
        { 
            AppointmentData obj = new AppointmentData();
            var Dept = ManagePatient.GetAllDepartment();
            var DeptID = Dept.FirstOrDefault();
            obj.Department = Dept;
            var Doctor = ManagePatient.GetAllDoctor(DeptID.ID);
            var Doc = Doctor.FirstOrDefault();
            obj.Doctor = Doctor;
            int day = 0;
            obj.SlotName = ManagePatient.GetAllSlot(Doc.EmpID,day);
            ViewBag.DocID = ID;

             return View(obj);
        }
        public ActionResult GetAppointment([Bind(Exclude = "TID")] AppointmentData obj)
        {
            bool status;
            if (obj.ID == 0)
            {
                try
                {
                    status = true;
                    //using (FOSDataModel db = new FOSDataModel())
                    //{
                        //var data = db.Appointments.Where(u => u.PatientName == obj.PatientName && u.MobileNo == obj.MobileNo).FirstOrDefault();
                        //if (data == null)
                        //{
                        //    status = true;
                        //}
                        //else
                        //{
                        //    status = false;
                        //}
                    //}
                    if (status == true)
                    {
                        Appointment PatientObj = new Appointment();
                        using (TransactionScope ts = new TransactionScope())
                        {
                            using (FOSDataModel db = new FOSDataModel())
                            {
                                if (obj.ID == 0)
                                {
                                    PatientObj.ID = db.Appointments.OrderByDescending(x => x.ID).Select(x => x.ID).FirstOrDefault() + 1;
                                    PatientObj.PatientName = obj.PatientName;
                                    Session["PatientName"] = obj.PatientName;
                                    PatientObj.MobileNo = obj.MobileNo;
                                    Session["MobileNo"] = obj.MobileNo;
                                    PatientObj.Email = obj.Email;
                                    PatientObj.DoctorID = obj.DoctorID;
                                    FOSDataModel dbContext = new FOSDataModel();
                                    var DoctorName = dbContext.GetDoctorName(PatientObj.DoctorID).FirstOrDefault();
                                    Session["Doctor"] = DoctorName;
                                    PatientObj.DepartmentID = obj.DepartmentID;
                                    PatientObj.AppointmentDate = obj.AppointmentDate;
                                    DateTime dat = obj.AppointmentDate;
                                    var dtt = dat.ToString("dd/MM/yyyy");
                                    //var dtt = DateTime.ParseExact(dat.ToString(), "dd/MM/yyyy", null);
                                    Session["Date"] = dtt;
                                    DateTime now = Convert.ToDateTime(obj.AppointmentDate);
                                    string s = now.DayOfWeek.ToString();
                                    Session["AppointmentDay"] = s;
                                  
                                    var SelectedDayID = dbContext.Days.Where(x => x.Name == s).Select(u => u.ID).FirstOrDefault();
                                    PatientObj.DayID = SelectedDayID;
                                    PatientObj.Slot = obj.Slot;
                                    //string slot = dbContext.Slots.Where(x => x.ID == obj.SlotID).Select(u => u.Name).FirstOrDefault();
                                    Session["SelectedSlot"] = obj.Slot;
                                    PatientObj.CreatedOn = DateTime.Now;
                                    PatientObj.IsActive = true;
                                    PatientObj.IsDeleted = false;
                                    db.Appointments.Add(PatientObj);
                                    db.SaveChanges();
                                    CheckSlot csObj = new CheckSlot();
                                    var AppointmentID = db.Appointments.OrderByDescending(x => x.ID).Select(x => x.ID).FirstOrDefault();
                                    csObj.DayID = SelectedDayID;
                                    csObj.Date = obj.AppointmentDate;
                                    csObj.DoctorID = obj.DoctorID;
                                    csObj.Slot = obj.Slot;
                                    csObj.AID = AppointmentID;
                                    db.CheckSlots.Add(csObj);
                                    DoctorSlot dsObj = new DoctorSlot();
                                    dsObj = db.DoctorSlots.Where(x => x.DoctorID == obj.DoctorID && x.DayID == SelectedDayID && x.SlotID == obj.SlotID).FirstOrDefault();
                                    dsObj.DoctorID = obj.DoctorID;
                                    dsObj.DayID = SelectedDayID;
                                    dsObj.Status = true;
									db.SaveChanges();
								}
                                else
                                {
                                    PatientObj.ID = obj.ID;
                                    PatientObj.PatientName = obj.PatientName;
                                    PatientObj.MobileNo = obj.MobileNo;
                                    PatientObj.DoctorID = obj.DoctorID;
                                    PatientObj.DepartmentID = obj.DepartmentID;
                                    PatientObj.DayID = obj.DayID;
                                    PatientObj.Slot = obj.Slot;
                                    PatientObj.IsActive = true;
                                    PatientObj.IsDeleted = false;
									db.SaveChanges();
								}
                               
                                ts.Complete();
                                SendSMS();

                            }
                        }


                    }
                    return RedirectToAction("Index", "Patient");
                    //return Content("1");
                }

                catch (Exception ex)
                {
                   
                }
            }
            //return RedirectToAction("Appointment", "Patient");
            return Content("0");
        }
        public void SendSMS()
        {
            var message = "Dear "+Session["PatientName"]+ " Your slot " + Session["SelectedSlot"] + " confirmed on date "+ Session["Date"] + " Your concerned Doctor is " + Session["Doctor"];
            Session["Message"] = message;
            string C_no = Session["MobileNo"].ToString();
            string URL = "https://bsms.telecard.com.pk/SMSportal/Customer/apikey.aspx";
            string urlParameters = "?apikey=8fefe3e7fff34e1aa0f0bbd289757590&msg=" + message + "&mobileno=" + C_no + "";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                   
            }
            //iframe_message.Attributes.Add("Src", finalurl);
            //iframe_print.Attributes.Add("Src", urlPath)
            //SaveSMS();
            //return Content("<script>alert('Appointment Successfully!')</script>");
        }
        public void SaveSMS()
        {
            try
            {
                SentSM sms = new SentSM();
                using (FOSDataModel db = new FOSDataModel())
                {
                    sms.MobileNo = Session["MobileNo"].ToString();
                    sms.Message = Session["Message"].ToString();
                    db.SentSMS.Add(sms);
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {

            }
           
        }
        public JsonResult GetDoctorDeptWise(int DepartmentID)
        {
            var response = ManagePatient.GetAllDoctor(DepartmentID);
            return Json(response);
        }
        public JsonResult GetSlotDoctorWise(int DoctorID, string AppointmentDate)
        {
            if(AppointmentDate == "" || AppointmentDate == "1/1/0001 12:00:00 AM" || AppointmentDate == null)
            {
                return Json("Please Select Valid Date");
            }
            DateTime now = Convert.ToDateTime(AppointmentDate);
            string s = now.DayOfWeek.ToString();
            FOSDataModel db = new FOSDataModel();
            var SelectedDayID = db.Days.Where(x => x.Name == s).Select(u => u.ID).FirstOrDefault();
            int DayID = Convert.ToInt32(SelectedDayID);
            var data = ManagePatient.GetAllSlot2(DoctorID, DayID,now);
            //String obj;
            //List<AppointmentData> ADobj;
            //foreach(var item in data)
            //{
            //    ADobj = new List<AppointmentData>();
            //    if(db.CheckSlots.Where(x => x.Slot != item.ID)==item)
            //    //ADobj.Add(item.ID);
            //}
            var response = data;
            return Json(response);
        }
        public JsonResult GetDoctorTimeTable(int DoctorID)
        {
            FOSDataModel db = new FOSDataModel();
            var DoctorTable = db.GetDoctorTable1_2(DoctorID).ToList();
            return Json(DoctorTable);
        }
        #endregion 
        public ActionResult Blog()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Doctors()
        {
            return View();
        }
        public ActionResult Department()
        {
            return View();
        }
        #region Doctor
        public ActionResult SearchDoctor()
        {
            AppointmentData obj = new AppointmentData();
            var Dept = ManagePatient.GetAllDepartment();
            obj.Department = Dept;
            var DeptID = Dept.FirstOrDefault();
            var Doctor = ManagePatient.GetAllDoctor(DeptID.ID);
            obj.Doctor = Doctor;
      
            return View(obj);
        }
        public JsonResult GetDoctorForGrid(int DrName,int Department)
        {
            var resoponse = ManagePatient.GetDoctorForTableView2(DrName,Department);
            var DoctorImage = resoponse.Image;
            if(DoctorImage != null)
            {
                resoponse.Image = DoctorImage.Replace("E:/ODAS/ODAS1.2/ODAS1.2/FOS.Web.UI", "..");
            }
            else
            {
                resoponse.Image = "../Images/defaul.jpeg";
            }
            ViewBag.Doctor = null;
            FOSDataModel db = new FOSDataModel();

            List<Employee> Position = db.Employees.Where(x => x.DeptID == Department).ToList();
            List<Employee> proinfo = Position;

            ViewBag.Doctor2 = proinfo;
      
            return Json(resoponse);
        }
        public JsonResult GetDoctorForGrid2(AppointmentData obj)
        {

            ViewBag.Doctor = null;
            FOSDataModel db = new FOSDataModel();
            List<Employee> Position = db.Employees.Where(x => x.DeptID == 2).ToList();
            List<Employee> proinfo = Position;

            ViewBag.Doctor2 = proinfo;
            // var content = "1";
            var response = "abc";

            JsonSerializerSettings jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            var result = JsonConvert.SerializeObject(proinfo, Formatting.Indented, jss);

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        #endregion
        #region PatientRegistration
        public ActionResult PatientRegistration()
        {
            return View();
        }
        public ActionResult AddUpdatePatient(PatientData obj)
        {
            bool status;
            if (obj.ID == 0)
            {
                try
                {
                    using (FOSDataModel db = new FOSDataModel())
                    {
                        var data = db.Patients.Where(u => u.Name == obj.Name && u.UserName == obj.UserName).FirstOrDefault();
                        if (data == null)
                        {
                            status = true;
                        }
                        else
                        {
                            status = false;
                        }
                    }
                    if (status == true)
                    {
                        Patient PatientObj = new Patient();
                        using(TransactionScope ts = new TransactionScope())
                        {
                            using(FOSDataModel db = new FOSDataModel())
                            {
                                if (obj.ID == 0)
                                {
                                    PatientObj.ID = db.Patients.OrderByDescending(x => x.ID).Select(x => x.ID).FirstOrDefault() + 1;
                                    PatientObj.Name = obj.Name;
                                    PatientObj.UserName = obj.UserName;
                                    PatientObj.Password = obj.Password;
                                    PatientObj.ConfirmPass = obj.ConPass;
                                    PatientObj.MobileNo = obj.MobileNo;
                                    PatientObj.Email = obj.Email;
                                    PatientObj.IsActive = true;
                                    PatientObj.IsDeleted = false;
                                    db.Patients.Add(PatientObj);
                                }
                                else
                                {
                                    PatientObj.ID = obj.ID;
                                    PatientObj.Name = obj.Name;
                                    PatientObj.UserName = obj.UserName;
                                    PatientObj.Password = obj.Password;
                                    PatientObj.ConfirmPass = obj.ConPass;
                                    PatientObj.MobileNo = obj.MobileNo;
                                    PatientObj.Email = obj.Email;
                                    PatientObj.IsActive = true;
                                    PatientObj.IsDeleted = false;
                                }
                                db.SaveChanges();
                                ts.Complete();
                               
                            }
                        }
                        

                    }
                    return Content("1");
                }

                catch (Exception ex)
                {

                }
            }
            return Content("1");
        }
        #endregion

    }
}
