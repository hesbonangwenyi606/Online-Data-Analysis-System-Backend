using FOS.DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FOS.Shared
{
    public class PatientData
    {
        public int ID { get; set; }
        [Required(ErrorMessage="*Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "*Password")]
        public string Password { get; set; }
        [NotMapped]
        [Compare("Password")]
        public string ConPass { get; set; }
        [Required(ErrorMessage="*Required")]
        public string MobileNo { get; set; }
        [Required(ErrorMessage="*Required")]
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
    public class AppointmentData
    {
        public int ID { get; set; }
        public string PatientName { get; set; }
        public string Relation { get; set; }
        public string RelationName { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string Remarks { get; set; }
        public string MobileNo { get; set; }
        public List<Department> Department { get; set; }
        public int DepartmentID { get; set; }
        public List<GetAllEmployee1_2_Result> Doctor { get; set; }
        public int DoctorID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public List<Day> Days { get; set; }
        public int DayID { get; set; }
        public string Day { get; set; }
        //public string AppointmentData { get; set; }
        public List<GetAllSlotDoctorWise1_3_Result> SlotName { get; set; }
        public int? SlotID { get; set; }
        public string Slot { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string DrName { get; set; }
        public string Email { get; set; }
        
    }
    public class PatientAppointmentData
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
  
}
