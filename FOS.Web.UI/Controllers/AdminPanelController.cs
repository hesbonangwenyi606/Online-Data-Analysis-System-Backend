using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FOS.DataLayer;
using FOS.AdminPanel;
using System.IO;
using FOS.Web.UI.Models;
using FOS.Shared;
using FluentValidation.Results;
using System.Web.Security;
using FOS.Web.UI.Common;
using FOS.Web.UI.Common.CustomAttributes;
using Shared.Diagnostics.Logging;
using System.Text;
using FOS.Setup;

namespace FOS.Web.UI.Controllers
{
    public class AdminPanelController : Controller
    {
        private FOSDataModel db = new FOSDataModel();

        // LOGIN Work...
        #region Access
        public int DeleteAccess(int ID)
        {
            return FOS.Setup.ManageArea.DeleteAccess(ID);
        }
        public ActionResult Access()
        {
            FOSDataModel db = new FOSDataModel();
            //var objAreaData = new AreaData();

            int RHID = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            //List<Region> RegionObj = FOS.Setup.ManageRegion.GetRegionList(RHID);
            //var objRegion = RegionObj.FirstOrDefault();
            //List<SaleOfficerData> So = FOS.Setup.ManageRetailer.GetSaleOfficerRegionWise(objRegion.ID);

            //objAreaData.Regions = RegionObj;
            //objAreaData.SaleOfficersFrom = So;
            return View();
        }
        public JsonResult GetSaleOfficerRegionWise(int RegionID)
        {
            //var result = FOS.Setup.ManageRetailer.GetSaleOfficerRegionWise(RegionID);
            var result = 1;
            return Json(result);
        }
        //public JsonResult AccessDataHandler(DTParameters param, Int32 CityID)
        //{
        //    try
        //    {
        //        var dtsource = new List<AreaData>();

        //        dtsource = ManageArea.GetAccessForGrid(CityID);

        //        List<String> columnSearch = new List<string>();

        //        foreach (var col in param.Columns)
        //        {
        //            columnSearch.Add(col.Search.Value);
        //        }

        //        List<AreaData> data = ManageArea.GetResult7(param.Search.Value, param.SortOrder, param.Start, param.Length, dtsource, columnSearch);
        //        int count = ManageArea.Count7(param.Search.Value, dtsource, columnSearch);
        //        DTResult<AreaData> result = new DTResult<AreaData>
        //        {
        //            draw = param.Draw,
        //            data = data,
        //            recordsFiltered = count,
        //            recordsTotal = count
        //        };
        //        return Json(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { error = ex.Message });
        //    }
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUpdateAccesss([Bind(Exclude = "TID")] AreaData newData)
        {
            Boolean boolFlag = true;
            ValidationResult results = new ValidationResult();
            try
            {
                if (newData != null)
                {

                    if (boolFlag)
                    {
                        int Response = ManageArea.AddUpdateAccess(newData);
                        if (Response == 1)
                        {
                            return Content("1");
                        }
                        else if (Response == 2)
                        {
                            return Content("2");
                        }
                        else
                        {
                            return Content("0");
                        }
                    }
                    else
                    {
                        IList<ValidationFailure> failures = results.Errors;
                        StringBuilder sb = new StringBuilder();
                        sb.Append(String.Format("{0}:{1}", "* Error *", "<br/>"));
                        foreach (ValidationFailure failer in results.Errors)
                        {
                            sb.AppendLine(String.Format("{0}:{1}{2}", failer.PropertyName, failer.ErrorMessage, "<br/>"));
                            Response.StatusCode = 422;
                            return Json(new { errors = sb.ToString() });
                        }
                    }
                }

                return Content("0");
            }
            catch (Exception exp)
            {
                return Content("Exception : " + exp.Message);
            }
        }
        #endregion Access

        #region LOGIN

        //Login View Method...
        public ActionResult Login1()
        {
            return View();
        }

        //Login View Method...
        public ActionResult Login()
        {
            return View();
        }

        //Session Create/Login Method...
        public JsonResult UserAuth(string userName, string password, string returnUrl)
        {
            Log.Instance.Info("A new user is trying to sign in");
            int userId = 0;
            userId = FOS.AdminPanel.ManageLogin.UserAuth(userName, password);
            string response = FOS.AdminPanel.ManageLogin.UserAuthTest(userName, password);
            if (userId > 0)
            {
                bool UserRoleStatus = RoleRegionalHeadExist(userId);
                ViewBag.res = userId.ToString();

                Log.Instance.Info("Correct credentials");

                SessionManager.Store("UserName", userName.ToString());
                SessionManager.Store("UserID", userId);
                SessionManager.Store("UserPages", Common.CurrentUser.GetUserPages(userId));

                //SetRegionalHeadIDRelatedToUser(userId);

                FormsAuthentication.SetAuthCookie(userName, false);
            }
            else
            {
                Log.Instance.Info("Login failed");
                SessionManager.Destroy("UserName");
            }

            string pageUrl = string.IsNullOrEmpty(returnUrl) ? string.Format("{0}/Home/UserHome", Settings.AppPath) : returnUrl;
            //return Json(new { status = userId, url =pageUrl }, JsonRequestBehavior.AllowGet);
            return Json(new { status = response, url = pageUrl }, JsonRequestBehavior.AllowGet);
            //New Home Page Logic
            //string pageUrl;
            //bool DashboardAccess = Common.CurrentUser.IsValidUserPageUrl("/Home/Home");
            //if (!DashboardAccess)
            //{
            //    pageUrl = string.IsNullOrEmpty(returnUrl) ? string.Format("{0}/Home/UserHome", Settings.AppPath) : returnUrl;
            //}
            //else
            //{
            //    pageUrl = string.IsNullOrEmpty(returnUrl) ? string.Format("{0}/Home/Home", Settings.AppPath) : returnUrl;
            //}
            //return Json(new { status = userId, url =pageUrl }, JsonRequestBehavior.AllowGet);
            return Json(new { status = response, url = pageUrl }, JsonRequestBehavior.AllowGet);
        }

        //Session Destroy/LogOut Method...
        public void Logout()
        {
            Session["AppAuth"] = false;
            Session["UserName"] = null;

            FormsAuthentication.SignOut();
            Session.Abandon();
            Session.RemoveAll();

            Response.Redirect("~/AdminPanel/login");
        }

        #endregion LOGIN

        // USER Form Work...

        #region USER

        //View...
        [CustomAuthorize]
        public ActionResult Users()
        {
            var objUser = new UserData();
            objUser.Roles = new List<Shared.Role>();
            objUser.Roles = FOS.AdminPanel.ManageUserRoles.GetRolesList(); ;

            return View(objUser);
        }

        // Add / Update User...
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUpdateUser(UserData newUser)
        {
            Boolean boolFlag = true;
            ValidationResult results = new ValidationResult();
            try
            {
                if (newUser != null)
                {
                    if (boolFlag)
                    {
                        if (ManageUsers.AddUpdateUser(newUser))
                        {
                            return Content("1");
                        }
                        else
                        {
                            return Content("0");
                        }
                    }
                }

                return Content("0");
            }
            catch (Exception exp)
            {
                return Content("Exception : " + exp.Message);
            }
        }

        //Get All Users...
        public JsonResult UserDataHandler(DTParameters param)
        {
            try
            {
                var dtsource = new List<UserData>();

                dtsource = ManageUsers.GetUsersForGrid();

                List<String> columnSearch = new List<string>();

                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }

                List<UserData> data = ManageUsers.GetResult(param.Search.Value, param.SortOrder, param.Start, param.Length, dtsource, columnSearch);
                int count = ManageUsers.Count(param.Search.Value, dtsource, columnSearch);
                DTResult<UserData> result = new DTResult<UserData>
                {
                    draw = param.Draw,
                    data = data,
                    recordsFiltered = count,
                    recordsTotal = count
                };
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        //Delete User Method...
        public int DeleteUser(int UserID)
        {
            return ManageUsers.DeleteUser(UserID);
        }

        #endregion USER

        // User Roles Form Work...

        #region ROLES

        //View...
        [CustomAuthorize]
        public ActionResult Role()
        {
            return View();
        }

        // Get PagesAction Data ...
        public JsonResult GetPagesActionData()
        {
            List<ActionPage> obj = new List<ActionPage>();
            //obj.Add(new ActionPage { Page = 5, Update = true , Delete = true , Read = true , Write = true});
            return Json(obj);
        }

        public string GetRolePages(int RoleID)
        {
            string Res = "";

            List<UserPage> roles = ManageUserRoles.GetRolePages(RoleID);
            if (roles.Count > 0)
            {
                Res = "";
                foreach (UserPage d in roles)
                {
                    Res += "<input type='checkbox' name='page' data-id='" + d.PageID + "' id='page' class='page' style='float: left; margin: 12px  10px 0 0px;'  " + (d.Status == true ? "Checked" : "") + @"/><li class='page_border_role'><span>" + d.ParentPageName + "</span><span style=float:right;font-size: 12px;'><input name='action" + d.PageID + "' type='checkbox' class='cbAction" + d.PageID + "' data-id='" + d.PageID + "' data-update='" + d.Update + "' style='margin: -2px 2px 0 0px;' " + (d.Update == true ? "Checked" : "") + @" /> Update</span><span style='float:right; margin-right:20px;font-size: 12px;'><input type='checkbox' name='action" + d.PageID + "' class='cbAction" + d.PageID + "' data-id='" + d.PageID + "' data-delete='" + d.Delete + "' style='margin: -2px 2px 0 0px;' " + (d.Delete == true ? "Checked" : "") + @"/> Delete</span><span style='float:right; margin-right:20px;font-size: 12px;'><input type='checkbox' name='action" + d.PageID + "'class='cbAction" + d.PageID + "' data-id='" + d.PageID + "' data-write='" + d.Write + "' style='margin: -2px 2px 0 0px;' " + (d.Write == true ? "Checked" : "") + @"/> Write</span><span style='float:right; margin-right:20px;font-size: 12px;'><input type='checkbox' name='action" + d.PageID + "' class='cbAction" + d.PageID + "' data-id='" + d.PageID + "' data-read='" + d.Read + "' style='margin: -2px 2px 0 0px;' " + (d.Read == true ? "Checked" : "") + @"/> Read</span></li>"; //<span style=float:right;font-size: 12px'><input name='action" + d.PageID + "' type='checkbox' class='cbAction" + d.PageID + "' data-id='" + d.PageID + "' data-update='" + d.Update + "' style='margin: -2px 2px 0 0px;' /> Update</span><span style='float:right; margin-right:20px;font-size: 12px;'><input type='checkbox' name='action" + d.PageID + "' class='cbAction" + d.PageID + "' data-id='" + d.PageID + "' data-delete='" + d.Delete + "' style='margin: -2px 2px 0 0px;' /> Delete</span><span style='float:right; margin-right:20px;font-size: 12px;'><input type='checkbox' name='action" + d.PageID + "'class='cbAction" + d.PageID + "' data-id='" + d.PageID + "' data-write='" + d.Write + "' style='margin: -2px 2px 0 0px;' /> Write</span><span style='float:right; margin-right:20px;font-size: 12px;'><input type='checkbox' name='action" + d.PageID + "' class='cbAction" + d.PageID + "' data-id='" + d.PageID + "' data-read='" + d.Read + "' style='margin: -2px 2px 0 0px;' /> Read</span>
                }
                Res += "";
            }

            return Res;
        }

        //Form Load Method ...
        public string GetPages()
        {
            string Res = "";
            List<UserPage> roles = ManageUserRoles.GetPages();
            if (roles.Count > 0)
            {
                Res = "";
                foreach (UserPage d in roles)
                {
                    Res += "<input type='checkbox' name='page' data-id='" + d.PageID + "' id='page' class='page' style='float: left; margin: 12px  10px 0 0px;' /><li class='page_border_role'><span>" + d.ParentPageName + "</span><span style=float:right;font-size: 12px;'><input name='action" + d.PageID + "' type='checkbox' class='cbAction" + d.PageID + "' data-id='" + d.PageID + "' data-update='" + d.Update + "' style='margin: -2px 2px 0 0px;' /> Update</span><span style='float:right; margin-right:20px;font-size: 12px;'><input type='checkbox' name='action" + d.PageID + "' class='cbAction" + d.PageID + "' data-id='" + d.PageID + "' data-delete='" + d.Delete + "' style='margin: -2px 2px 0 0px;' /> Delete</span><span style='float:right; margin-right:20px;font-size: 12px;'><input type='checkbox' name='action" + d.PageID + "'class='cbAction" + d.PageID + "' data-id='" + d.PageID + "' data-write='" + d.Write + "' style='margin: -2px 2px 0 0px;' /> Write</span><span style='float:right; margin-right:20px;font-size: 12px;'><input type='checkbox' name='action" + d.PageID + "' class='cbAction" + d.PageID + "' data-id='" + d.PageID + "' data-read='" + d.Read + "' style='margin: -2px 2px 0 0px;' /> Read</span></li>"; //<span style=float:right;font-size: 12px'><input name='action" + d.PageID + "' type='checkbox' class='cbAction" + d.PageID + "' data-id='" + d.PageID + "' data-update='" + d.Update + "' style='margin: -2px 2px 0 0px;' /> Update</span><span style='float:right; margin-right:20px;font-size: 12px;'><input type='checkbox' name='action" + d.PageID + "' class='cbAction" + d.PageID + "' data-id='" + d.PageID + "' data-delete='" + d.Delete + "' style='margin: -2px 2px 0 0px;' /> Delete</span><span style='float:right; margin-right:20px;font-size: 12px;'><input type='checkbox' name='action" + d.PageID + "'class='cbAction" + d.PageID + "' data-id='" + d.PageID + "' data-write='" + d.Write + "' style='margin: -2px 2px 0 0px;' /> Write</span><span style='float:right; margin-right:20px;font-size: 12px;'><input type='checkbox' name='action" + d.PageID + "' class='cbAction" + d.PageID + "' data-id='" + d.PageID + "' data-read='" + d.Read + "' style='margin: -2px 2px 0 0px;' /> Read</span>
                }
                Res += "";
            }
            return Res;
        }

        public int AddUpdateRole(string RoleID, string RoleName, List<ActionPage> PageActions)
        {
            Boolean boolFlag = true;
            List<ActionPage> pa = PageActions;

            //List<int> PageList = RolePages.Split(',').Select(x => Int32.Parse(x)).ToList();
            //List<bool> PageRights = RolePages.Split(',').Select(x => Boolean.Parse(x)).ToList();

            try
            {
                if (RoleName != null)
                {
                    if (boolFlag)
                    {
                        //for (int i = 0; i < PageRights.Count; i = i + 4)
                        //{
                        //    // PageRights[i] // update
                        //    // PageRights[i+1] ///

                        //    // Make A Class And The Save The Values To The Class...
                        //    // Make Object In JQuery ... and pass it to the class which has same variables a Jquery
                        //    // object. In Parameter Class Is written

                        //}
                        if (ManageUserRoles.InsertOrUpdateRoles(int.Parse(RoleID), RoleName, PageActions))
                        {
                            return 0;
                        }
                        else
                        {
                            return 1;
                        }
                    }
                    else
                    {
                    }
                }

                return 0;
            }
            catch (Exception)
            {
                return 4;
            }
        }

        //Get All Roles Method...
        public JsonResult RoleDataHandler(DTParameters param)
        {
            try
            {
                var dtsource = new List<FOS.Shared.Role>();

                dtsource = ManageUserRoles.GetRolesForGrid();

                List<String> columnSearch = new List<string>();

                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }

                List<FOS.Shared.Role> data = ManageUserRoles.GetResult(param.Search.Value, param.SortOrder, param.Start, param.Length, dtsource, columnSearch);
                int count = ManageUserRoles.Count(param.Search.Value, dtsource, columnSearch);
                DTResult<FOS.Shared.Role> result = new DTResult<FOS.Shared.Role>
                {
                    draw = param.Draw,
                    data = data,
                    recordsFiltered = count,
                    recordsTotal = count
                };
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        //Delete Role Method...
        public int DeleteRole(int RoleID)
        {
            return ManageUserRoles.DeleteRole(RoleID);
        }

        #endregion ROLES

        #region MENU

        public ActionResult UserMenu()
        {
            int userId = SessionManager.Get<int>("UserID");
            if (userId > 0)
            {
                List<UserPage> userNavMenu = CurrentUser.UserPages; //SessionManager.Get<List<UserPage>>(SessionKeys.CurUser_UserPages);

                return View(userNavMenu);
            }

            return View(new List<UserPage>());
        }

        #endregion MENU

        // Check If the Login User Have Regional Head Role Or Not...
        public bool RoleRegionalHeadExist(int ID)
        {
            var user = db.Users.Where(s => s.ID == ID).FirstOrDefault();
            var role = user.Roles.Select(r => new { RoleID = r.RoleID, RoleName = r.RoleName }).FirstOrDefault();
            var count = user.Roles.Where(r => r.RoleID == role.RoleID && r.RoleName == "Regional Head" && r.Users.Where(u => u.ID == user.ID).Count() > 0).Count();

            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //public void SetRegionalHeadIDRelatedToUser(int ID)
        //{
        //    var RHeads = db.Users.Where(s => s.ID == ID).FirstOrDefault().RegionalHeads.Select(r => r.ID).ToList();
        //    SessionManager.Store("RegionalHeads", RHeads);
        //}

        //public List<int> GetRegionalHeadIDRelatedToUser()
        //{
        //    return  SessionManager.Get<List<int>>("RegionalHeads");
        //}

        public static int GetRegionalHeadIDRelatedToUser()
        {
            try
            {
                return SessionManager.Get<List<int>>("RegionalHeads").FirstOrDefault();
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}