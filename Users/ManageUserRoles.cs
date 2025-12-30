using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FOS.DataLayer;
using FOS.Shared;
using System.Web;
using System.Security.Principal;
using System.Security.Policy;
using System.Web.Security;    
using System.Transactions;
using System.Web.UI.WebControls;
using Shared.Diagnostics.Logging;

namespace FOS.AdminPanel
{
    public class ManageUserRoles
    {

        // Get Pages ...
        public static List<UserPage> GetPages()
        {
            List<UserPage> data = new List<UserPage>();
            try
            {
                using (FOSDataModel dbContext = new FOSDataModel())
                {
                    data = dbContext.Pages.Where(p => p.PageID > 0 && p.Controller != null && p.ShowMenu == true).OrderBy(p => p.ParentPageID).Select(p => new UserPage
                    {
                        PageID = p.PageID,
                        ParentPageName = p.Name,
                        Controller = p.Controller,

                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(ex, "Pages Not Get");
                throw ex;
            }

            return data;

        }


        // Get Role Pages ...
        public static List<UserPage> GetRolePages(int RoleID)
        {
            List<UserPage> data = new List<UserPage>();
            try
            {
                using (FOSDataModel dbContext = new FOSDataModel())
                {
                    var role = dbContext.Roles.Where(r => r.RoleID == RoleID).FirstOrDefault();

                    if (role != null)
                    {
                        data = dbContext.RolePages.Where(r => r.RoleID == RoleID && r.Page.Controller != null && r.Page.ShowMenu == true).OrderBy(p => p.Page.ParentPageID).Select(rp => new UserPage
                        {
                            PageID = rp.Page.PageID,
                            ParentPageName = rp.Page.Name,
                            Update = (bool)rp.Update,
                            Read = (bool)rp.Read,
                            Write = (bool)rp.Write,
                            Delete = (bool)rp.Delete,
                            Status = (bool)rp.Status
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


        // Insert OR Update Roles ...
        public static bool InsertOrUpdateRoles(int RoleID, string RoleName, List<ActionPage> PageActions)
        {
            bool retVal = false;
            try
            {
                using (FOSDataModel dbContext = new FOSDataModel())
                {
                    FOS.DataLayer.Role dbRole = new FOS.DataLayer.Role();

                    bool isNew = true;
                    if (RoleID > 0)
                    {
                        isNew = false;
                        dbRole = dbContext.Roles.Where(r => r.RoleID == RoleID).FirstOrDefault();
                        //dbRole.DateModified = DateTime.Now;
                    }

                    dbRole.RoleName = RoleName;

                    //check already exist
                    var existingRoleCount = dbContext.Roles.Where(u => u.RoleName == dbRole.RoleName
                        && (isNew || (!isNew && u.RoleID != dbRole.RoleID))
                    ).Count();

                    if (existingRoleCount > 0)
                    {
                        throw new Exception("Role already exists");
                    }


                    dbRole.RolePages.Clear();

                    foreach (var rolePage in PageActions)
                    {

                        //check if ParentPage menu is not added then add ParentPageMenu to this role also
                        var currentPage = dbContext.Pages.Where(p => p.PageID == rolePage.Page).FirstOrDefault();
                        if (currentPage != null && currentPage.ParentPageID != null)
                        {

                            if (dbRole.RolePages.Where(rp => rp.PageID == currentPage.ParentPageID).Count() == 0)
                            {

                                FOS.DataLayer.RolePage dbRolePage_Parent = new FOS.DataLayer.RolePage
                                {
                                    PageID = currentPage.ParentPageID.Value,
                                    Access = 1111,
                                    Read = rolePage.Read,
                                    Write = false,
                                    Delete = false,
                                    Update = false,
                                };

                                dbRole.RolePages.Add(dbRolePage_Parent);
                            }
                        }
                        //end parent page entry

                        FOS.DataLayer.RolePage dbRolePage = new FOS.DataLayer.RolePage
                        {
                            RoleID = RoleID,
                            PageID = rolePage.Page,
                            Access = 1111,
                            Read = rolePage.Read, // true,
                            Write = rolePage.Write, //true,
                            Delete = rolePage.Delete, //true,
                            Update = rolePage.Update, // true
                            Status = rolePage.Status
                        };

                        dbRole.RolePages.Add(dbRolePage);

                    }

                    dbRole.RolePages.Add(new FOS.DataLayer.RolePage
                    {
                        RoleID = RoleID,
                        PageID = 29,
                        Access = 1111,
                        Read = true, 
                        Write = false, 
                        Delete = false,
                        Update = false,
                        Status = true
                    });

                    if (isNew)
                    {
                        dbContext.Roles.Add(dbRole);
                    }

                    dbContext.SaveChanges();

                    retVal = true; //dbRole.RoleID;
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(ex, "Role Creation Failed");
                throw ex;
            }
            return retVal;
        }


        // Delete Role ...
        public static int DeleteRole(int RoleID)
        {
            if (RoleID == 1)
            {
               // throw new Exception("You cannot delete admin role");
            }

            try
            {
                using (FOSDataModel dbContext = new FOSDataModel())
                {
                    var role = dbContext.Roles.Where(r => r.RoleID == RoleID).FirstOrDefault();
                    if (role != null)
                    {
                        role.RolePages.Clear();

                        foreach (var usr in role.Users)
                        {
                            usr.Roles.Clear();
                        }

                        dbContext.SaveChanges();
                        dbContext.Roles.Remove(role);
                        dbContext.SaveChanges();
                        return 0;
                    }
                    else
                    {
                       // throw new Exception("User does not exist");
                    }

                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(ex, "Role Deletion Failed");
                throw ex;
            }

            return 1;
        }


        // Get All Roles List ...
        public static List<FOS.Shared.Role> GetRolesList(int roleId = 0)
        {
            List<FOS.Shared.Role> data = new List<FOS.Shared.Role>();
            try
            {
                using (FOSDataModel dbContext = new FOSDataModel())
                {
                    data = dbContext.Roles.Where(r => (roleId == 0 || r.RoleID == roleId)).Select(r => new FOS.Shared.Role
                    {
                        RoleName = r.RoleName,
                        RoleID = r.RoleID
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(ex, "Get Role List Failed");
                throw ex;
            }

            return data;
        }


        // Get All Roles List For Grid ...
        public static List<FOS.Shared.Role> GetRolesForGrid(int roleId = 0)
        {
            List<FOS.Shared.Role> data = new List<FOS.Shared.Role>();
            try
            {
                using (FOSDataModel dbContext = new FOSDataModel())
                {
                    data = dbContext.Roles.Where(r => (roleId == 0 || r.RoleID == roleId)).Select(r => new FOS.Shared.Role
                    {
                        RoleName = r.RoleName,
                        RoleID = r.RoleID
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(ex, "Get Role List Failed");
                throw ex;
            }

            return data;
        }


        public static List<FOS.Shared.Role> GetResult(string search, string sortOrder, int start, int length, List<FOS.Shared.Role> dtResult, List<string> columnFilters)
        {
            return FilterResult(search, dtResult, columnFilters).SortBy(sortOrder).Skip(start).Take(length).ToList();
        }


        public static int Count(string search, List<FOS.Shared.Role> dtResult, List<string> columnFilters)
        {
            return FilterResult(search, dtResult, columnFilters).Count();
        }


        private static IQueryable<FOS.Shared.Role> FilterResult(string search, List<FOS.Shared.Role> dtResult, List<string> columnFilters)
        {
            IQueryable<FOS.Shared.Role> results = dtResult.AsQueryable();

            results = results.Where(p => (search == null || (p.RoleName != null && p.RoleName.ToLower().Contains(search.ToLower())))
                && (columnFilters[2] == null || (p.RoleName != null && p.RoleName.ToLower().Contains(columnFilters[2].ToLower())))
                );

            return results;
        }
    

    }
}


