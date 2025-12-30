using FOS.DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Shared
{
    public class ComplaintData
    {

        public ComplaintData()
        {
            Statuses = new List<EnumDropdown>();
            Statuses.Add(new Shared.EnumDropdown { id = (int)StatusEnum.Pending, val = StatusEnum.Pending.ToString() });
            Statuses.Add(new Shared.EnumDropdown { id = (int)StatusEnum.Completed, val = StatusEnum.Completed.ToString() });

            Priorities = new List<EnumDropdown>();
            Priorities.Add(new Shared.EnumDropdown { id = (int)PriorityEnum.Low, val = PriorityEnum.Low.ToString() });
            Priorities.Add(new Shared.EnumDropdown { id = (int)PriorityEnum.Medium, val = PriorityEnum.Medium.ToString() });
            Priorities.Add(new Shared.EnumDropdown { id = (int)PriorityEnum.High, val = PriorityEnum.High.ToString() });

        }
        public int ComplaintID { get; set; }

        [DisplayName("Title *")]
        [Required(ErrorMessage = "* Required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "* Required")]
        [DisplayName("Detail *")]
        public string Detail { get; set; }
        public string Remarks { get; set; }
        public int? RegionalHeadID { get; set; }
        public int HiddenRegionalHeadID { get; set; }

        //[DataType(DataType.Date)]
        //[Required(ErrorMessage = "* Required")]
        //[DisplayName("Due Date *")]
        //public DateTime? DueDate { get; set; }

        [Required(ErrorMessage = "* Required")]
        [DisplayName("Due Date *")]
        public string DueDateString { get; set; }
    
        [Required(ErrorMessage = "* Required")]
        [DisplayName("Priority *")]
        public string Priority { get; set; }
        public string PriorityName { get; set; }

        [Required(ErrorMessage = "* Required")]
        [DisplayName("Status *")]
        public string Status { get; set; }
        public string StatusName { get; set; }

        //[Required(ErrorMessage = "* Required")]
        public int Type { get; set; }
        public int? RetailerId { get; set; }
        public int HiddenRetailerID { get; set; }

        public string SaleOfficerName { get; set; }
        public int? SaleOfficerID { get; set; }
        public int HiddenSaleOfficerID { get; set; }

        public int? CityID { get; set; }
        public System.DateTime LastUpdate { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public bool IsDeleted { get; set; }

        //public List<RegionalHeadData> RegionalHead { get; set; }
        //public IEnumerable<RegionalHeadTypeData> RegionalHeadTypeData { get; set; }
        public List<SaleOfficerData> SaleOfficers { get; set; }
        //public List<RetailerData> Retailers { get; set; }
        public List<EnumDropdown> Statuses { get; set; }
        public List<EnumDropdown> Priorities { get; set; }

        [Required(ErrorMessage = "* Required")]
        //public ICollection<Area> Areas { get; set; }

        [DisplayName("Retailer Name")]
        public String RetailerName { get; set; }

        public String AreaID { get; set; }

        [DisplayName("Area Name")]
        public String AreaName { get; set; }

        [DisplayName("City Name")]
        public String CityName { get; set; }

        public String RemUpdatedOn { get; set; }

    }

    //public enum StatusEnum
    //{
    //    Pending = 0,
    //    Completed = 1
    //}

    //public enum PriorityEnum
    //{
    //    Low = 1,
    //    Medium = 2,
    //    High = 3
    //}

    //public class EnumDropdown
    //{
    //    public int id { get; set; }
    //    public string val { get; set; }
    //}
}