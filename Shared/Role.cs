using FOS.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Shared
{
    public class Role
    {


        public int RoleID { get; set; }
        public string RoleName { get; set; }

        public int[] RolePages { get; set; }
    }


    public class RolePage
    {
        public int RoleID { get; set; }
        public int PageID { get; set; }
        public string Access { get; set; }
        public Nullable<System.DateTime> LastUpdated { get; set; }
        public Nullable<bool> Read { get; set; }
        public Nullable<bool> Write { get; set; }
        public Nullable<bool> Delete { get; set; }
        public Nullable<bool> Update { get; set; }

        public string RoleName { get; set; }
        public string PageName { get; set; }
    }

    public class Page
    {
        public int PageID { get; set; }
        public string PageName { get; set; }
    }

    public class UserRole
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }
    }

    public class UserPage
    {

        public int PageID{get;set;}
        public int ParentPageID{get;set;}
        public string ParentPageName{get; set;}
        public string Path { get;set;}
        public string PageName{get;set;}
        public string MenuInitials{ get; set;}
        public string Controller{get; set;}
        public string Action{ get; set;}
        public bool ShowMenu{ get;set;}
        public string Access { get;set;}
        public bool Read{ get;set; }
        public bool Write{get; set;}
        public bool Delete{get;set;}
        public bool Update{get;set;}
        public int TotalChildPages { get; set; }
        public string Type { get; set; }
        public bool Status { get; set; }
        public string Icon { get; set; }

    }

    public class ActionPage
    {
        public int Page { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public bool Write { get; set; }
        public bool Read { get; set; }
        public bool Status { get; set; }
    }
}
