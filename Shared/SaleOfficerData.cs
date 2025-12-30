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
    public class SaleOfficerData
    {
        public int TID { get; set; }
        public int ID { get; set; }
        public Nullable<int> sID { get; set; }

        [DisplayName("Sale Officer *")]
        [Required(ErrorMessage = "* Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "* Required")]
        [DisplayName("User Name *")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "* Required")]
        [DisplayName("Password *")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }


        //[Required(ErrorMessage = "* Required")]
        public int Type { get; set; }
        public int? RegionalHeadID { get; set; }
        public int HiddenRegionalHeadID { get; set; }
        //[Required(ErrorMessage = "* Required")]
        [RegularExpression(@"^(92)[0-9]{10}$", ErrorMessage = "Invalid Phone No")]
        [Required(ErrorMessage = "* Required")]
        [DisplayName("Phone No 1 *")]
        public string Phone1 { get; set; }

        [RegularExpression(@"^(92)[0-9]{10}$", ErrorMessage = "Invalid Phone No")]
        [DisplayName("Phone No 2")]
        public string Phone2 { get; set; }

        public int? CityID { get; set; }
        public System.DateTime LastUpdate { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public bool IsDeleted { get; set; }

        //public List<RegionalHeadData> RegionalHead { get; set; }
        //public IEnumerable<RegionalHeadTypeData> RegionalHeadTypeData { get; set; }

 

        [Required(ErrorMessage = "* Required")]
        //public ICollection<Area> Areas { get; set; }

        [DisplayName("Regional Head Name")]
        public String RegionalHeadName { get; set; }

        public String AreaID { get; set; }

        [DisplayName("Area Name")]
        public String AreaName { get; set; }

        [DisplayName("City Name")]
        public String CityName { get; set; }
    }

}