using FOS.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FOS.Web.UI.Common
{
    public class NumberCheck
    {

        //internal static int CheckNumberExist(string Phone1, string phone2)
        //{
        //    int Res = 0;
        //    FOSDataModel db = new FOSDataModel();

        //    if (Phone1 == "" && phone2 == "")
        //    {
        //        return Res;
        //    }

        //    if(db.RegionalHeads.Where(r => r.Phone1 == Phone1 || r.Phone1 == phone2 || r.Phone2 == Phone1 || r.Phone2 == phone2).Count() > 0)
        //    {
        //        Res = 1; 
        //    }

        //    if(db.Dealers.Where(r => r.Phone1 == Phone1 || r.Phone1 == phone2 || r.Phone2 == Phone1 || r.Phone2 == phone2).Count() > 0)
        //    {
        //        Res = 1; 
        //    }

        //    if(db.SaleOfficers.Where(r => r.Phone1 == Phone1 || r.Phone1 == phone2 || r.Phone2 == Phone1 || r.Phone2 == phone2).Count() > 0)
        //    {
        //        Res = 1; 
        //    }

        //    if (db.Retailers.Where(r => r.Phone1 == Phone1 || r.Phone1 == phone2 || r.Phone2 == Phone1 || r.Phone2 == phone2).Count() > 0)
        //    {
        //        Res = 1; 
        //    }

        //    return Res;
        //}

        internal static int CheckRegionalHeadNumberExist(int ID, string Phone1, string phone2)
        {
            int Res = 0;
            FOSDataModel db = new FOSDataModel();

            if (Phone1 == "" && phone2 == "")
            {
                return Res;
            }

            //if (db.RegionalHeads.Where(r => r.ID != ID && (r.Phone1 == Phone1 || r.Phone1 == phone2 || r.Phone2 == Phone1 || r.Phone2 == phone2)).Count() > 0)
            //{
            //    Res = 1;
            //}

            //if (db.Dealers.Where(r => r.Phone1 == Phone1 || r.Phone1 == phone2 || r.Phone2 == Phone1 || r.Phone2 == phone2).Count() > 0)
            //{
            //    Res = 1;
            //}

            //if (db.SaleOfficers.Where(r => r.Phone1 == Phone1 || r.Phone1 == phone2 || r.Phone2 == Phone1 || r.Phone2 == phone2).Count() > 0)
            //{
            //    Res = 1;
            //}

            //if (db.Retailers.Where(r => r.Phone1 == Phone1 || r.Phone1 == phone2 || r.Phone2 == Phone1 || r.Phone2 == phone2).Count() > 0)
            //{
            //    Res = 1;
            //}

            return Res;
        }

        //internal static int CheckDealerNumberExist(int ID, string Phone1, string phone2)
        //{
        //    int Res = 0;
        //    FOSDataModel db = new FOSDataModel();

        //    if (Phone1 == "" && phone2 == "")
        //    {
        //        return Res;
        //    }

        //    if (db.RegionalHeads.Where(r => r.Phone1 == Phone1 || r.Phone1 == phone2 || r.Phone2 == Phone1 || r.Phone2 == phone2).Count() > 0)
        //    {
        //        Res = 1;
        //    }

        //    if (db.Dealers.Where(r => r.ID != ID && (r.Phone1 == Phone1 || r.Phone1 == phone2 || r.Phone2 == Phone1 || r.Phone2 == phone2)).Count() > 0)
        //    {
        //        Res = 1;
        //    }

        //    if (db.SaleOfficers.Where(r => r.Phone1 == Phone1 || r.Phone1 == phone2 || r.Phone2 == Phone1 || r.Phone2 == phone2).Count() > 0)
        //    {
        //        Res = 1;
        //    }

        //    if (db.Retailers.Where(r => r.Phone1 == Phone1 || r.Phone1 == phone2 || r.Phone2 == Phone1 || r.Phone2 == phone2).Count() > 0)
        //    {
        //        Res = 1;
        //    }

        //    return Res;
        //}

        internal static int CheckSalesOfficerNumberExist(int ID, string Phone1, string phone2)
        {
            int Res = 0;
            FOSDataModel db = new FOSDataModel();

            if (Phone1 == "" && phone2 == "")
            {
                return Res;
            }

            //if (db.RegionalHeads.Where(r => r.Phone1 == Phone1 || r.Phone1 == phone2 || r.Phone2 == Phone1 || r.Phone2 == phone2).Count() > 0)
            //{
            //    Res = 1;
            //}

            //if (db.Dealers.Where(r => r.Phone1 == Phone1 || r.Phone1 == phone2 || r.Phone2 == Phone1 || r.Phone2 == phone2).Count() > 0)
            //{
            //    Res = 1;
            //}

            //if (db.SaleOfficers.Where(r=> r.ID != ID && (r.Phone1 == Phone1 || r.Phone1 == phone2 || r.Phone2 == Phone1 || r.Phone2 == phone2)).Count() > 0)
            //{
            //    Res = 1;
            //}

            //if (db.Retailers.Where(r => r.Phone1 == Phone1 || r.Phone1 == phone2 || r.Phone2 == Phone1 || r.Phone2 == phone2).Count() > 0)
            //{
            //    Res = 1;
            //}

            return Res;
        }

        //internal static int CheckRetailerNumberExist(int ID, string Phone1, string phone2)
        //{
        //    int Res = 0;
        //    FOSDataModel db = new FOSDataModel();

        //    if (Phone1 == "" && phone2 == "")
        //    {
        //        return Res;
        //    }

        //    if (db.RegionalHeads.Where(r => r.Phone1 == Phone1 || r.Phone1 == phone2 || r.Phone2 == Phone1 || r.Phone2 == phone2).Count() > 0)
        //    {
        //        Res = 1;
        //    }

        //    if (db.Dealers.Where(r => r.Phone1 == Phone1 || r.Phone1 == phone2 || r.Phone2 == Phone1 || r.Phone2 == phone2).Count() > 0)
        //    {
        //        Res = 1;
        //    }

        //    if (db.SaleOfficers.Where(r => r.Phone1 == Phone1 || r.Phone1 == phone2 || r.Phone2 == Phone1 || r.Phone2 == phone2).Count() > 0)
        //    {
        //        Res = 1;
        //    }

        //    if (db.Retailers.Where(r => r.ID != ID && (r.Phone1 == Phone1 || r.Phone1 == phone2 || r.Phone2 == Phone1 || r.Phone2 == phone2)).Count() > 0)
        //    {
        //        Res = 1;
        //    }

        //    return Res;
        //}

        //internal static int CheckRetailerNumber1Exist(int ID, string Phone1)
        //{
        //    int Res = 0;
        //    FOSDataModel db = new FOSDataModel();

        //    if (db.RegionalHeads.Where(r => r.Phone1 == Phone1 || r.Phone2 == Phone1).Count() > 0)
        //    {
        //        Res = 1;
        //    }

        //    if (db.Dealers.Where(r => r.Phone1 == Phone1 || r.Phone2 == Phone1).Count() > 0)
        //    {
        //        Res = 1;
        //    }

        //    if (db.SaleOfficers.Where(r => r.Phone1 == Phone1 || r.Phone2 == Phone1).Count() > 0)
        //    {
        //        Res = 1;
        //    }

        //    if (db.Retailers.Where(r => r.ID != ID && (r.Phone1 == Phone1 || r.Phone2 == Phone1)).Count() > 0)
        //    {
        //        Res = 1;
        //    }

        //    return Res;
        //}

        //internal static int CheckRetailerNumber2Exist(int ID, string Phone2)
        //{
        //    int Res = 0;
        //    FOSDataModel db = new FOSDataModel();

        //    if (db.RegionalHeads.Where(r => r.Phone1 == Phone2 || r.Phone2 == Phone2).Count() > 0)
        //    {
        //        Res = 1;
        //    }

        //    if (db.Dealers.Where(r => r.Phone1 == Phone2 || r.Phone2 == Phone2).Count() > 0)
        //    {
        //        Res = 1;
        //    }

        //    if (db.SaleOfficers.Where(r => r.Phone1 == Phone2 || r.Phone2 == Phone2).Count() > 0)
        //    {
        //        Res = 1;
        //    }

        //    if (db.Retailers.Where(r => r.ID != ID && (r.Phone1 == Phone2 || r.Phone2 == Phone2)).Count() > 0)
        //    {
        //        Res = 1;
        //    }

        //    return Res;
        //}

        //internal static int CheckDealerNumber1Exist(int ID, string Phone1)
        //{
        //    int Res = 0;
        //    FOSDataModel db = new FOSDataModel();

        //    if (db.RegionalHeads.Where(r => r.Phone1 == Phone1 || r.Phone2 == Phone1).Count() > 0)
        //    {
        //        Res = 1;
        //    }

        //    if (db.Dealers.Where(r => r.ID != ID && (r.Phone1 == Phone1 || r.Phone2 == Phone1)).Count() > 0)
        //    {
        //        Res = 1;
        //    }

        //    if (db.SaleOfficers.Where(r => r.Phone1 == Phone1 || r.Phone2 == Phone1).Count() > 0)
        //    {
        //        Res = 1;
        //    }

        //    if (db.Retailers.Where(r => r.Phone1 == Phone1 || r.Phone2 == Phone1).Count() > 0)
        //    {
        //        Res = 1;
        //    }

        //    return Res;
        //}

        //internal static int CheckDealerNumber2Exist(int ID, string Phone2)
        //{
        //    int Res = 0;
        //    FOSDataModel db = new FOSDataModel();

        //    if (db.RegionalHeads.Where(r => r.Phone1 == Phone2 || r.Phone2 == Phone2).Count() > 0)
        //    {
        //        Res = 1;
        //    }

        //    if (db.Dealers.Where(r => r.ID != ID && (r.Phone1 == Phone2 || r.Phone2 == Phone2)).Count() > 0)
        //    {
        //        Res = 1;
        //    }

        //    if (db.SaleOfficers.Where(r => r.Phone1 == Phone2 || r.Phone2 == Phone2).Count() > 0)
        //    {
        //        Res = 1;
        //    }

        //    if (db.Retailers.Where(r => r.Phone1 == Phone2 || r.Phone2 == Phone2).Count() > 0)
        //    {
        //        Res = 1;
        //    }

        //    return Res;
        //}

        internal static int CheckSalesOfficerNumber1Exist(int ID, string Phone1)
        {
            int Res = 0;
            //FOSDataModel db = new FOSDataModel();

            //if (db.RegionalHeads.Where(r => r.Phone1 == Phone1 || r.Phone2 == Phone1).Count() > 0)
            //{
            //    Res = 1;
            //}

            //if (db.Dealers.Where(r => r.Phone1 == Phone1 || r.Phone2 == Phone1).Count() > 0)
            //{
            //    Res = 1;
            //}

            //if (db.SaleOfficers.Where(r => r.ID != ID && (r.Phone1 == Phone1 || r.Phone2 == Phone1)).Count() > 0)
            //{
            //    Res = 1;
            //}

            //if (db.Retailers.Where(r => r.Phone1 == Phone1 || r.Phone2 == Phone1).Count() > 0)
            //{
            //    Res = 1;
            //}

            return Res;
        }

        internal static int CheckSalesOfficerNumber2Exist(int ID, string Phone2)
        {
            int Res = 0;
            //FOSDataModel db = new FOSDataModel();

            //if (db.RegionalHeads.Where(r => r.Phone1 == Phone2 || r.Phone2 == Phone2).Count() > 0)
            //{
            //    Res = 1;
            //}

            //if (db.Dealers.Where(r => r.Phone1 == Phone2 || r.Phone2 == Phone2).Count() > 0)
            //{
            //    Res = 1;
            //}

            //if (db.SaleOfficers.Where(r => r.ID != ID && (r.Phone1 == Phone2 || r.Phone2 == Phone2)).Count() > 0)
            //{
            //    Res = 1;
            //}

            //if (db.Retailers.Where(r => r.Phone1 == Phone2 || r.Phone2 == Phone2).Count() > 0)
            //{
            //    Res = 1;
            //}

            return Res;
        }

        internal static int CheckRHNumber1Exist(int ID, string Phone1)
        {
            int Res = 0;
            FOSDataModel db = new FOSDataModel();

            //if (db.RegionalHeads.Where(r => r.ID != ID && (r.Phone1 == Phone1 || r.Phone2 == Phone1)).Count() > 0)
            //{
            //    Res = 1;
            //}

            //if (db.Dealers.Where(r => r.Phone1 == Phone1 || r.Phone2 == Phone1).Count() > 0)
            //{
            //    Res = 1;
            //}

            //if (db.SaleOfficers.Where(r => r.Phone1 == Phone1 || r.Phone2 == Phone1).Count() > 0)
            //{
            //    Res = 1;
            //}

            //if (db.Retailers.Where(r => r.Phone1 == Phone1 || r.Phone2 == Phone1).Count() > 0)
            //{
            //    Res = 1;
            //}

            return Res;
        }

        internal static int CheckRHNumber2Exist(int ID, string Phone2)
        {
            int Res = 0;
            FOSDataModel db = new FOSDataModel();

            //if (db.RegionalHeads.Where(r => r.ID != ID && (r.Phone1 == Phone2 || r.Phone2 == Phone2)).Count() > 0)
            //{
            //    Res = 1;
            //}

            //if (db.Dealers.Where(r => r.Phone1 == Phone2 || r.Phone2 == Phone2).Count() > 0)
            //{
            //    Res = 1;
            //}

            //if (db.SaleOfficers.Where(r => r.Phone1 == Phone2 || r.Phone2 == Phone2).Count() > 0)
            //{
            //    Res = 1;
            //}

            //if (db.Retailers.Where(r => r.Phone1 == Phone2 || r.Phone2 == Phone2).Count() > 0)
            //{
            //    Res = 1;
            //}

            return Res;
        }

    }
}