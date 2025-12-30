using FOS.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.RegularExpressions;
using Shared.Diagnostics.Logging;
using FOS.Shared;

namespace FOS.AdminPanel
{
    public class ManageLogin
    {
        // Check If The User is Valid OR Not ...
        public static int UserAuth(string userName, string password)
        {
            int Res;

            try
            {

                using (FOSDataModel dbContext = new FOSDataModel())
                {
                    User CurrentUser = dbContext.Users.Where(u => u.UserName.ToLower() == userName.ToLower() && u.Password == password).FirstOrDefault();
                    if (CurrentUser == null)
                    {
                        return -1;
                    }

                    if(CurrentUser.UserName.ToLower() != userName.ToLower()) 
                    {
                        return -1;
                    }

                    if (CurrentUser!=null)
                    {
                        if (CurrentUser.Password == password)
                        {
                            //conditions matched
                            return CurrentUser.ID; 
                        }
                        else
                        {
                            //invalid Password
                            return -2;
                        }
                    }
                    else
                    {
                        //if user not found
                        return -1;
                    }
                }



            }
            catch (Exception ex)
            {
                Log.Instance.Error( ex, "Login Failed", new[]{userName, password});
                return -3;
            }

        }

        public static string UserAuthTest(string userName, string password)
        {
            int Res;

            try
            {

                using (FOSDataModel dbContext = new FOSDataModel())
                {
                    User CurrentUser = dbContext.Users.Where(u => u.UserName.ToLower() == userName.ToLower() && u.Password == password).FirstOrDefault();
                    if (CurrentUser == null)
                    {
                        return "-1";
                    }

                    if (CurrentUser.UserName.ToLower() != userName.ToLower())
                    {
                        return "-1";
                    }

                    if (CurrentUser != null)
                    {
                        if (CurrentUser.Password == password)
                        {
                            //conditions matched
                            return CurrentUser.ID.ToString();
                        }
                        else
                        {
                            //invalid Password
                            return "-2";
                        }
                    }
                    else
                    {
                        //if user not found
                        return "-1";
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(ex, "Login Failed", new[] { userName, password });
                //return -3;
                return ex.ToString();
            }

        }


        #region NavMenu


        // Get All Menu Links ...
        public static string GetMenuLink(FOS.Shared.UserPage page)
        {
            //if (page.Type.ToLower() == "razor")
            //{
            //    //return Url.Action(page.Action, page.Controller);
            //    return page.Path;
            //}

            //if (page.Type.ToLower() == "aspx")
            //{
            //    return page.Path;
            //}

            return page.Path;
        }


        // Get All pages ...
        public static List<UserPage> GetPages()
        {
            List<UserPage> data = new List<UserPage>();
            try
            {
                using (FOSDataModel dbContext = new FOSDataModel())
                {
                    data = dbContext.Pages.Where(p => p.PageID > 0 && p.ParentPageID != null).OrderBy(p => p.ParentPageID).Select(p => new UserPage
                    {
                        PageID = p.PageID,
                        PageName = p.Name,
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return data;

        }


        // Get All Role Pages In Edit Mode ...
        public static List<UserPage> GetRolePages(int roleId)
        {
            List<UserPage> data = new List<UserPage>();
            try
            {
                using (FOSDataModel dbContext = new FOSDataModel())
                {
                    var role = dbContext.Roles.Where(r => r.RoleID == roleId).FirstOrDefault();

                    if (role != null)
                    {
                        data = role.RolePages.Select(rp => new UserPage
                        {
                            PageID = rp.PageID,
                            PageName = rp.Page.Name

                        }).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return data;

        }


        #endregion

    }
}