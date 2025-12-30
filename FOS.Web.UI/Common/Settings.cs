using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FOS.Web.UI.Common
{
    public class Settings
    { 
        public static string AppPath { get { return ConfigurationManager.AppSettings["AppPath"].ToString(); } }
        public static string VistualDirectory { get { return ConfigurationManager.AppSettings["VirtualDirectory"].ToString(); } }

       
    }
}