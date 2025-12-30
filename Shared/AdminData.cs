using FOS.DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FOS.Shared
{
    public class AdminData
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "* Required")]
        public string Name { get; set; }
    }
    public class SubDepartmentData
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "*Required")]
        public string Name { get; set; }
        public string SubDepartment { get; set; }
        public string Department { get; set; }
        public List<Department> Dept { get; set; }
        public int DeptID { get; set; }
    }
    public class DesignationData
    {
        public int ID { get; set; }
        [Required(ErrorMessage="*Required")]
        public string Designation { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
    public class DoctorData
    {
        public int EmpID { get; set; }
        public string Prefix { get; set; }
        [Required(ErrorMessage = "*Required")]
        public string EFName { get; set; }
        public string EMName { get; set; }
        public string ELName { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime JoiningData { get; set; }
        public string JoiningDate { get; set; }
        [RegularExpression("^[0-9]{5}-[0-9]{7}-[0-9]$", ErrorMessage = "CNIC No must follow the XXXXX-XXXXXXX-X format!")]
        public string CNIC { get; set; }
        public List<DesignationData> Designation { get; set; }
        public string DesignationName { get; set; }
        public int DesignationID { get; set; }
        public int? PayScale { get; set; }
        public string HPhoneNo { get; set; }
        public string OPhoneNo { get; set; }
        public string OfficeAdd { get; set; }
        public string HomeAdd { get; set; }
        public string Gender { get; set; }
        public List<Department> Dept { get; set; }
        public int DeptID { get; set; }
        public List<SubDepartmentData> SubDept { get; set; }
        public int SubDeptID { get; set; }
        public string MobileNo { get; set; }
        public string PMDC_No { get; set; }
        public DateTime DateOfTermination { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public List<Day> Days { get; set; }
        public string WorkingDaysID { get; set; }
     
        public string UserName { get; set; }

        public string Password { get; set; }
        [NotMapped] // Does not effect with your database
        [Compare("Password")]
        public string ConfirmPass { get; set; }
        [DisplayName("Doctor Image *")]
        public string DrImgPath { get; set; }
        public HttpPostedFileBase DrImg { get; set; }
        //public HttpPostedFileBase ImageFile { get; set; }

    }
    public class DoctorSlotsData
    {
        public int ID { get; set; }
        public int Duration { get; set; }
        public string StartingTime { get; set; }
        public string EndingTime { get; set; }
        public List<GetDaysDoctorWise1_1_Result> Days { get; set; }
        public int DayID { get; set; }
        public List<Department> Departments { get; set; }
        public int DeptID { get; set; }
        //public List<GetAllEmployee_Result> Doctor { get; set; }
        public List<DoctorData> Doctor { get; set; }
        public int DoctorID { get; set; }
        public List<Slot> Slot { get; set; }
        //public System.Web.Mvc.MultiSelectList Slot { get; set; }
        public string SlotID { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
    public class RequestData
    {
        public int ID { get; set; }
        public string PatientName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public string DoctorName { get; set; }
        public List<Department> Dept { get; set; }
        public int DeptID { get; set; }
        public List<SubDepartmentData> SubDept { get; set; }
        public int SubDeptID { get; set; }
        public List<DoctorData> Doctor { get; set; }
        public int DoctorID { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public string RequestedDate { get; set; }
        public string Slot { get; set; }
        public List<Slot> Slots { get; set; }
        public List<GetAllSlotDoctorWise1_3_Result> SlotName { get; set; }
        public string SelectedSlot { get; set; }
        public int SlotID { get; set; }
        public string Status { get; set; }
    }
}