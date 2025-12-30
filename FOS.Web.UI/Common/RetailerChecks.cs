using FOS.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FOS.Web.UI.Common
{
    public class RetailerChecks
    {

        static FOSDataModel db = new FOSDataModel();

        //public static int CheckCardNoExist(string CardNo , int ID)
        //{
        //    int Res = 0;

        //    if (CardNo == "" || CardNo == null)
        //    {
        //        Res = 2;
        //    }
        //    else
        //    {
        //        if (db.Retailers.Where(r => r.ID != ID && r.CardNumber == CardNo).Count() > 0)
        //        {
        //            Res = 1;
        //        }
        //    }

        //    return Res;
        //}

        //public static int CheckAccountNoExist(string AccountNo, int ID)
        //{
        //    int Res = 0;

        //    if (AccountNo == "" || AccountNo == null)
        //    {
        //        Res = 2;
        //    }
        //    else
        //    {
        //        if (db.Retailers.Where(r => r.ID != ID && r.AccountNo == AccountNo).Count() > 0)
        //        {
        //            Res = 1;
        //        }
        //    }

        //    return Res;
        //}

        //public static int CheckCNICExist(string CNIC , int ID)
        //{
        //    int Res = 0;

        //    if (CNIC == "" || CNIC == null)
        //    {
        //        Res = 2;
        //    }
        //    else
        //    {
        //        if (db.Retailers.Where(r => r.ID != ID && r.CNIC == CNIC).Count() > 0)
        //        {
        //            Res = 1;
        //        }
        //    }

        //    return Res;
        //}


    }
}