using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FOS.Shared
{

    public class RetailerData
    {
        public int TID { get; set; }
        [Key] 
        public int ID { get; set; }

        public int JobDetailID { get; set; }

        public Nullable<int> RegionalHeadID { get; set; }
        public int HiddenRegionalHeadID { get; set; }
        [DisplayName("Sales Officer Name *")]
        public string RegionalHeadName { get; set; }

        [DisplayName("Retailer Name *")]
        public string Name { get; set; }
        
        public string RetailerCode { get; set; }
        public string ContactPerson { get; set; }
        public string BankName { get; set; }
        public string BankName2 { get; set; }

        //[RegularExpression(@"^[0-9]{5}-[0-9]{7}-[0-9]{1}$", ErrorMessage = "Invalid CNIC")]
        public string CNIC { get; set; }
        public string Email { get; set; }

        public string AccountNo { get; set; }
        public string AccountNo2 { get; set; }

        //[RegularExpression(@"^[0-9]{16}$", ErrorMessage = "Invalid Card Number")]
        public string CardNumber { get; set; }

        //[Required(ErrorMessage = "* Required")]
        [DisplayName("Dealer Name *")]
        public int? DealerID { get; set; }
        public int? NoOfBranches { get; set; }
        public int? NoOfTeachers { get; set; }
        public int? StudentStrength { get; set; }

        [DisplayName("Dealer Name *")]
        public string DealerName { get; set; }

        [DisplayName("Sales Officer *")]
        public int SaleOfficerID { get; set; }

        [DisplayName("Sale Officer Name *")]
        public string SaleOfficerName { get; set; }
        public Boolean RetailerJobStatus { get; set; }
        
        
        [DisplayName("Shop Name *")]
        //[Required(ErrorMessage = "* Required")]
        public string ShopName { get; set; }

        public List<SelectListItem> ShopNames { get; set; }
        public IEnumerable<RegionData> Regions { get; set; }
        public String RegionID { get; set; }
        //[DisplayName("Region *")]
        public String ShopID { get; set; }
      //  public IEnumerable ShopNames { get; set; }
        public int[] Ids { get; set; }

        public string Image { get; set; }
        public string Address { get; set; }

        public string TypeOfShop { get; set; }
        public string ShopCategory { get; set; }

        [DisplayName("Location(x,y)")]
        public string Location { get; set; }
        public decimal? Lattitude { get; set; }
        public decimal? Longitude { get; set; }
        public string LocationName { get; set; }

        [DisplayName("Shop Margin *")]
        public string LocationMargin { get; set; }

        [DisplayName("Phone No 1 *")]
        //[RegularExpression(@"^(92)[0-9]{10}$", ErrorMessage = "Invalid Phone No")]
        public string Phone1 { get; set; }

        //[RegularExpression(@"^(92)[0-9]{10}$", ErrorMessage = "Invalid Phone No")]
        public string Phone2 { get; set; }
        public string ContactPersonCell { get; set; }
        public string LandLineNo { get; set; }
        public string Market { get; set; }

        [DisplayName("Region Name *")]
        public string RegionName { get; set; }

        [DisplayName("City *")]
        public int? CityID { get; set; }
        [DisplayName("City *")]
        public string CItyName { get; set; }

        [DisplayName("Area *")]
        public int AreaID { get; set; }

        [DisplayName("Area *")]
        public string AreaName { get; set; }
        public int AreaNameID { get; set; }
        public string RetailerType { get; set; }
        public string Type { get; set; }


        public System.DateTime LastUpdate { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }

        public List<RegionalHeadData> RegionalHead { get; set; }
        public List<SaleOfficerData> SaleOfficers { get; set; }
        public List<DealerData> Dealers { get; set; }
        public List<CityData> Cities { get; set; }
        public List<AreaData> Areas { get; set; }
        public List<BankData> Banks { get; set; }
        public List<Fees> FeeStructure { get; set; }

    }

    public class BankData
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }

    public class Fees
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }

    public class RetailerGraphData
    {
        public int ApproveRetailers;
        public int PendingRetailers;
    }

    public class RetailersReport
    {
        public Int32 RetailerID { get; set; }
        public string retailername { get; set; }
        public string saleofficername { get; set; }
        public string shopname { get; set; }
        public string cityname { get; set; }
        public string retailertype { get; set; }
        public Int32? spreviousorder1kg { get; set; }
        public DateTime? visiteddate { get; set; }      
    }

    public class RetailersMonthlyReport
    {
        public Int32 RetailerID { get; set; }
        public string retailername { get; set; }
        public string saleofficername { get; set; }
        public string shopname { get; set; }
        public string cityname { get; set; }
        public string retailertype { get; set; }
        public Int32? spreviousorder1kg { get; set; }
        public DateTime? visiteddate { get; set; }
        public int? montht { get; set; }
        public int? year { get; set; }
        public string months { get; set; }
        public string monthss { get { return Convert.ToDateTime(montht + "-" + year).ToString("MMM-yyyy"); } }
    }

    public class CityWiseFosReport
    {
        public Int32? retailerid { get; set; }
        public string rname { get; set; }
        public string shopname { get; set; }
        public string rtype { get; set; }
        public string mkt { get; set; }
        public string saleofficername { get; set; }
        public string cityname { get; set; }
        public Int32? till_last_mnth_sale { get; set; }
        public Int32? crnt_mnth_sale { get; set; }
        public Int32? avg_last_two_mnth_sale { get; set; }
    }

    public class CityMktRtailrWiseReport
    {
         public string cityname { get; set; }
        public string market { get; set; }
        public string retailer { get; set; }
        public string category { get; set; }
        public Int32? till_last_mnth_sale { get; set; }
        public Int32? crnt_mnth_sale { get; set; }
        public Int32? avg_last_two_mnth_sale { get; set; }
    }

    public class RetailerPoints
    {
        public string RetailerName { get; set; }
        public string CNIC { get; set; }
        public string RetailerType { get; set; }
        public string cityName { get; set; }
        public decimal? Claim_Amount { get; set; }
        public decimal? Transferred_Amount { get; set; }
        public decimal? Balance_Amount { get; set; }
    }

    public class PosAvailablility    {
        public string cityname { get; set; }
        public string market { get; set; }
        public string shopname { get; set; }
        public string category { get; set; }
        public string fos { get; set; }
        public Int32 pos_availability { get; set; }
    }

    public class CityWisePainters
    {
        public int ID { get; set; }
        public bool Registered { get; set; }
        public string transactiontype { get; set; }
        public string city { get; set; }
        public string Market { get; set; }
        public string POS { get; set; }
        public string magcard { get; set; }
        public string walletnumber { get; set; }
        public string cnic { get; set; }
        public string PhoneNumber { get; set; }
        public Int32 painterid { get; set; }
        public string pname { get; set; }
        public Int32 points_redeemed_1kg { get; set; }
        public Int32 points_redeemed_5kg { get; set; }
        public Int32 total_points_redeemed { get; set; }
        public Int32 points_transferred { get; set; }
        public Int32 balance_points { get; set; }
    }

}