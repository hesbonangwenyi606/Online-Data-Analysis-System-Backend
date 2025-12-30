using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FOS.DataLayer;
using FOS.Shared;
using System.Web.UI.WebControls;
using Shared.Diagnostics.Logging;

namespace FOS.AdminPanel
{
    public class ManageUsers
    {

        //Insert Update Region Method...
        public static Boolean AddUpdateUser(UserData obj)
        {
            Boolean boolFlag = false;

            try
            {

                    using (FOSDataModel dbContext = new FOSDataModel())
                    {
                        User UserObj = new User();
                        

                        if (obj.ID == 0)
                        {
                            UserObj.ID = dbContext.Users.OrderByDescending(u => u.ID).Select(u => u.ID).FirstOrDefault() + 1;
                            UserObj.UserName = obj.UserName;
                            UserObj.Password = obj.Password;
                            UserObj.EmailID = obj.EmailID;
                            UserObj.PhoneNo = obj.PhoneNo;
                            UserObj.Address = obj.Address;
                            UserObj.DOB = obj.DOB;
                            UserObj.IsActive = obj.IsActive;
                            UserObj.CreatedDate = DateTime.Now;
                            UserObj.ModifiedDate = DateTime.Now;
                            dbContext.Users.Add(UserObj);
                            dbContext.SaveChanges();

                            var usr = dbContext.Users.Where(u => u.ID == UserObj.ID).FirstOrDefault();
                            var role = dbContext.Roles.Where(r => r.RoleID == obj.RoleID).FirstOrDefault();

                            usr.Roles.Add(role);
                            dbContext.SaveChanges();
                        }
                        else
                        {
                            UserObj = dbContext.Users.Where(u => u.ID == obj.ID).FirstOrDefault();

                            UserObj.UserName = obj.UserName;
                            UserObj.Password = obj.Password;
                            UserObj.EmailID = obj.EmailID;
                            UserObj.PhoneNo = obj.PhoneNo;
                            UserObj.Address = obj.Address;
                            UserObj.DOB = obj.DOB;
                            UserObj.IsActive = obj.IsActive;
                            UserObj.ModifiedDate = DateTime.Now;

                            var usr = dbContext.Users.Where(u => u.ID == obj.ID).FirstOrDefault();
                            var role = dbContext.Roles.Where(r => r.RoleID == obj.RoleID).FirstOrDefault();

                            usr.Roles.Clear();
                            dbContext.SaveChanges();
                            usr.Roles.Add(role);

                            dbContext.SaveChanges();
                        }

                        boolFlag = true;
                    }
                }
            catch (Exception ex)
            {
                Log.Instance.Error(ex, "Add User Failed");
                boolFlag = false;
            }
            return boolFlag;
        }


        //Delete User...
        public static int DeleteUser(int UserID)
        {
            int Resp = 0;

            try
            {
                using (FOSDataModel dbContext = new FOSDataModel())
                {

                    User obj = dbContext.Users.Where(u => u.ID == UserID).FirstOrDefault();


                    dbContext.Users.Remove(obj);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(ex, "Add User Failed");
                Resp = 1;
            }
            return Resp;
        }


        // Get Users For Grid...
        public static List<UserData> GetUsersForGrid()
        {
            List<UserData> userData = new List<UserData>();

            try
            {
                using (FOSDataModel dbContext = new FOSDataModel())
                {

                    userData = dbContext.Users.Where(u => u.IsDeleted == false)
                               .Select(u => new UserData
                                {
                                    ID = u.ID,
                                    UserName = u.UserName,
                                    Password = u.Password,
                                    RoleID = (u.Roles.FirstOrDefault() == null ? 0 : u.Roles.FirstOrDefault().RoleID),
                                    RoleName = (u.Roles.FirstOrDefault() == null ? "No Role" : u.Roles.FirstOrDefault().RoleName),
                                    PhoneNo = u.PhoneNo == null ? "" : u.PhoneNo,
                                    Address = u.Address == null ? "" : u.Address,
                                    EmailID = u.EmailID == null ? "" : u.EmailID,
                                    DOB = u.DOB,
                                    IsActive = u.IsActive
                                }).ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(ex, "Get Users Failed");
                throw;
            }

            return userData;
        }


        public static List<UserData> GetResult(string search, string sortOrder, int start, int length, List<UserData> dtResult, List<string> columnFilters)
        {
            return FilterResult(search, dtResult, columnFilters).SortBy(sortOrder).Skip(start).Take(length).ToList();
        }


        public static int Count(string search, List<UserData> dtResult, List<string> columnFilters)
        {
            return FilterResult(search, dtResult, columnFilters).Count();
        }


        private static IQueryable<UserData> FilterResult(string search, List<UserData> dtResult, List<string> columnFilters)
        {
            IQueryable<UserData> results = dtResult.AsQueryable();

            results = results.Where(p => (search == null || (p.UserName != null && p.UserName.ToLower().Contains(search.ToLower()) || p.RoleName != null && p.RoleName.ToString().ToLower().Contains(search.ToLower()) || p.Address != null && p.Address.ToString().ToLower().Contains(search.ToLower()) || p.EmailID != null && p.EmailID.ToString().ToLower().Contains(search.ToLower()) || p.PhoneNo != null && p.PhoneNo.ToString().ToLower().Contains(search.ToLower())))
                && (columnFilters[2] == null || (p.UserName != null && p.UserName.ToLower().Contains(columnFilters[2].ToLower())))
                && (columnFilters[3] == null || (p.RoleName != null && p.RoleName.ToString().ToLower().Contains(columnFilters[3].ToLower())))
                && (columnFilters[3] == null || (p.Address != null && p.Address.ToString().ToLower().Contains(columnFilters[3].ToLower())))
                && (columnFilters[3] == null || (p.EmailID != null && p.EmailID.ToString().ToLower().Contains(columnFilters[3].ToLower())))
                && (columnFilters[3] == null || (p.PhoneNo != null && p.PhoneNo.ToString().ToLower().Contains(columnFilters[3].ToLower())))
                );

            return results;
        }

        
    }
}
