using FOS.DataLayer;
using FOS.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FOS.Web.UI.Common
{
    public class CurrentUser
    {

        public static List<UserPage> GetUserPages(int userId)
        {
            List<UserPage> list = new List<UserPage>();
            List<UserPage> finalList = new List<UserPage>();

            try
            {
                using (FOSDataModel dbContext = new FOSDataModel())
                {

                    User usr = dbContext.Users.Where(u => u.ID == userId).FirstOrDefault();

                    if (usr != null)
                    {
                        foreach (FOS.DataLayer.Role role in usr.Roles)
                        {

                            foreach (FOS.DataLayer.RolePage rolePage in role.RolePages)
                            {
                                //start - logic for admin
                                if (rolePage.PageID == 0)
                                {
                                    list.Clear();
                                    foreach (FOS.DataLayer.Page p in dbContext.Pages.Where(p => p.PageID > 0))
                                    {

                                        list.Add(new UserPage
                                        {
                                            PageID = p.PageID,
                                            ParentPageID = p.ParentPageID ?? 0,
                                            ParentPageName = (p.Page1 != null) ? p.Page1.Name : "",
                                            PageName = p.Name,
                                            MenuInitials = p.MenuInitials,
                                            Controller = p.Controller,
                                            Action = p.Action,
                                            ShowMenu = p.ShowMenu ?? false,
                                            Path = p.Path,
                                            Access = "1111",
                                            Read = true,
                                            Write = true,
                                            Delete = true,
                                            Update = true,
                                            Icon = p.Icon,
                                            TotalChildPages = p.Pages1.Where(c => c.ShowMenu == true).Count()
                                        });
                                    }
                                    return list;
                                }
                                //end - logic for admin

                                list.Add(new UserPage
                                {
                                    PageID = rolePage.PageID,
                                    ParentPageID = rolePage.Page.ParentPageID ?? 0,
                                    ParentPageName = (rolePage.Page.Page1 != null) ? rolePage.Page.Page1.Name : "",
                                    PageName = rolePage.Page.Name,
                                    MenuInitials = rolePage.Page.MenuInitials,
                                    Controller = rolePage.Page.Controller,
                                    Action = rolePage.Page.Action,
                                    Path = rolePage.Page.Path,
                                    ShowMenu = rolePage.Page.ShowMenu ?? false,
                                    Access = rolePage.Access.ToString(),
                                    Read = rolePage.Read ?? false,
                                    Write = rolePage.Write ?? false,
                                    Delete = rolePage.Delete ?? false,
                                    Update = rolePage.Update ?? false,
                                    Icon  = rolePage.Page.Icon,
                                    TotalChildPages = rolePage.Page.Pages1.Count()
                                });
                            }
                        }
                    }
                }


                foreach (UserPage uPage in list)
                {
                    if (uPage.ParentPageID == 0)
                    {
                        var firstChild = list.Where(p => p.ParentPageID == uPage.PageID && p.ShowMenu == true && p.Read).FirstOrDefault();

                        if (firstChild != null)
                        {
                            uPage.Controller = firstChild.Controller;
                            uPage.Action = firstChild.Action;
                            uPage.Path = firstChild.Path;
                        }
                    }
                }
            }
            catch (Exception)
            {
                //Logger.AppendToSystemLog(Logger.EntryTypes.Error, Logger.EntryCategories.IBIData, ex);
                //throw ex;
            }

            return list;
        }


        //Check Read Authorization. 
        public static bool IsValidUserPageUrl(string pagePath)
        {
            List<UserPage> usrPages = SessionManager.Get<List<UserPage>>("UserPages");

            if (usrPages != null)
            {
                UserPage uPage = usrPages.Where(p => p.Path == pagePath).LastOrDefault();
                if (uPage != null && uPage.Read)
                {
                    return true;
                }
            }

            return false;
        }

        //Check Update Authorization. 
        public static bool IsUpdateValid(string pagePath)
        {
            List<UserPage> usrPages = SessionManager.Get<List<UserPage>>("UserPages");

            if (usrPages != null)
            {
                UserPage uPage = usrPages.Where(p => p.Path == pagePath).LastOrDefault();
                if (uPage != null && uPage.Update)
                {
                    return true;
                }
            }

            return false;
        }

        //Check Delete Authorization. 
        public static bool IsDeleteValid(string pagePath)
        {
            List<UserPage> usrPages = SessionManager.Get<List<UserPage>>("UserPages");

            if (usrPages != null)
            {
                UserPage uPage = usrPages.Where(p => p.Path == pagePath).LastOrDefault();
                if (uPage != null && uPage.Delete)
                {
                    return true;
                }
            }

            return false;
        }

        //Check Create Authorization. 
        public static bool IsWriteValid(string pagePath)
        {
            List<UserPage> usrPages = SessionManager.Get<List<UserPage>>("UserPages");

            if (usrPages != null)
            {
                UserPage uPage = usrPages.Where(p => p.Path == pagePath).LastOrDefault();
                if (uPage != null && uPage.Write)
                {
                    return true;
                }
            }

            return false;
        }



        public static List<UserPage> UserPages
        {
            get
            {
                List<UserPage> usrPages = SessionManager.Get<List<UserPage>>("UserPages");

                if (usrPages == null)
                {
                    usrPages = GetUserPages(SessionManager.Get<int>("UserID"));
                }

                return usrPages;
            }
            set
            {
                SessionManager.Store("UserPages", value);
            }
        }

    }
}