using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Globalization; 
using System.Threading;
using System.IO; 
using System.Security.Principal; 

namespace FOS.Web.UI.Common.CustomAttributes
{
    public class CustomAuthorize : ActionFilterAttribute, IAuthorizationFilter
    { 
         
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            string curUrl = string.IsNullOrEmpty(Settings.VistualDirectory) ? HttpContext.Current.Request.ServerVariables["URL"] : HttpContext.Current.Request.ServerVariables["URL"].Replace(string.Format("/{0}", Settings.VistualDirectory),"");     
            string username =  HttpContext.Current.Session["UserName"] == null ? "" : HttpContext.Current.Session["UserName"].ToString();

            SessionManager.Destroy("UpdateCheck");
            SessionManager.Destroy("DeleteCheck");
            SessionManager.Destroy("WriteCheck");

            bool allowAccess = false;
            int allowUpdate;
            int allowDelete;
            int allowWrite;

            string reason = "login"; //login , auth
            if (!string.IsNullOrEmpty(username))
            {
                reason = "auth";
                //Authorization
                allowAccess = true;
                allowUpdate = 1;
                allowDelete = 1;
                allowWrite = 1;

                HttpContext.Current.Session["UpdateCheck"] = allowUpdate;
                HttpContext.Current.Session["DeleteCheck"] = allowDelete;
                HttpContext.Current.Session["WriteCheck"] = allowWrite;

            }
            else
            {
                allowAccess = false;
            }

            if (!allowAccess)
            {
                filterContext.Result = new RedirectResult("~/AdminPanel/Login?returnUrl=" + curUrl+ "&reason=" + reason);
            }

            //string curUrl = HttpContext.Current.Request.ServerVariables["URL"];            
            
            //string returnUrl = string.Empty;

            //returnUrl = string.Empty;
            //if (!filterContext.HttpContext.Request.IsAjaxRequest())
            //{
            //    returnUrl = curUrl;
            //}

            ////if (string.IsNullOrEmpty(AuthToken))
            ////{
            ////    Logout(returnUrl);
            ////    return;
            ////}

            //Auth auth = RESTManager.Validate(AuthToken);
            //if (auth.IsValid)
            //{
            //    AuthToken = auth.AuthToken;

            //    //keep the login alive here.                
            //    FormsAuthentication.SetAuthCookie(auth.Username, true);
            //    filterContext.HttpContext.User = new GenericPrincipal(new GenericIdentity(auth.Username, "Forms"), null);
            //}
            //else
            //{                
               
            //    FormsAuthentication.SignOut();
            //    System.Web.HttpContext.Current.Session.Abandon();

            //    filterContext.Result = new RedirectResult("/Account/Login?returnUrl=" + returnUrl);
               
            //}

 
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            UrlHelper url = new UrlHelper(context.RequestContext);

            string path = string.Empty;
            
            //var userPages = CurrentUser.IsValidUserPageUrl(HttpContext.Current.Request.Ra)

            ////if(url.RequestContext.HttpContext.Request.IsLocal)
            //{
            //    path = url.RequestContext.HttpContext.Request.Url.LocalPath;

            //    ServiceManager.SetUserActivityConfiguration(CurrentUser.UserId, "LastPage", path.Replace("/IBI/", ""));
            //}
                

            string s = path;
        }


        //for IAuthorizeFilter
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            //filterContext.
            // TODO: set the culture here so that it's picked up by the model binder

            //CultureInfo culture = new CultureInfo(Settings.DefaultCulture);
            //Thread.CurrentThread.CurrentCulture = culture;
            //Thread.CurrentThread.CurrentUICulture = culture;
        } 
 
      
    }
}