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
   public class AreaData
    {
        public int TID { get; set; }
        public int ID { get; set; }
        [DisplayName("Owner Name *")]
        [Required(ErrorMessage = "* Required")]
        public string OwnerName { get; set; }

        [DisplayName("Area Name *")]
        [Required(ErrorMessage = "* Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "* Required")]
        public int RegionID { get; set; }

        [DisplayName("Region Name *")]
        public string RegionName { get; set; }

        [Required(ErrorMessage = "* Required")]


        [DisplayName("City Name *")]
        public string CityName { get; set; }

        [DisplayName("City Name *")]
        public string ShortCode { get; set; }
        public System.DateTime LastUpdate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }

        //public List<Region> Regions { get; set; }
        [Required(ErrorMessage = "* Required")]
        public int CityID { get; set; }
        //public List<CityData> Cities { get; set; }
        public List<SaleOfficerData> SaleOfficersFrom { get; set; }
        public List<SaleOfficerData> SaleOfficersTo { get; set; }
        public int intSaleOfficerIDfrom { get; set; }
        public int intSaleOfficerIDto { get; set; }
        public List<AreaData> Areas { get; set; }
        public int AreaID { get; set; }
        //public List<RegionalHeadData> RegionalHeads { get; set; }
        public int RegionalHeadID { get; set; }

        [DisplayName("Phone No 1 *")]
        [RegularExpression(@"^(92)[0-9]{10}$", ErrorMessage = "Invalid Phone No")]
        public string Phone1 { get; set; }

        [RegularExpression(@"^(92)[0-9]{10}$", ErrorMessage = "Invalid Phone No")]
        public string Phone2 { get; set; }
        [RegularExpression(@"^[0-9]{5}-[0-9]{7}-[0-9]{1}$", ErrorMessage = "Invalid CNIC")]
        public string CNIC { get; set; }
        public string Email { get; set; }
        public string RetailerCode { get; set; }
        [DisplayName("Shop Name *")]
        [Required(ErrorMessage = "* Required")]
        public string ShopName { get; set; }
        public string Address { get; set; }
        //public List<RetailerData> Retailers { get; set; }
        public string rID { get; set; }
        [Required(ErrorMessage = "* Required")]
        public int RetailerID { get; set; }
        public string RetailerName { get; set; }
        public string SaleOfficerName { get; set; }
        [DisplayName("Message *")]
        [Required(ErrorMessage = "* Required")]
        public string Message { get; set; }
        [Required(ErrorMessage = "* Required")]
        public string Language { get; set; }
        public int DealerID { get; set; }
        //public List<DealerData> Dealers { get; set; }
        public int SaleOfficerID { get; set; }
        public string SchoolName { get; set; }
        public string DealerName { get; set; }
        public string Date { get; set; }
        public int? ReportedTo { get; set; }
        public string ReportedToName { get; set; }
        public int? ReportedFor { get; set; }
        public string ReportedForName { get; set; }
    }

    public class AreaGraphData
    {
        public string CityName { get; set; }
        public int AreaCount { get; set; }
    }
    
}
