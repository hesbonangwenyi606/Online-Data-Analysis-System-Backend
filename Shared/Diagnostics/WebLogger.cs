using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NLog;
using NLog.Targets;
using NLog.Config; 

namespace Shared.Diagnostics
{
    public class WebLogger
    {
        private static String CurrentLogFileName
        {
            get
            {
                string logFileName = "Log_" + DateTime.Now.ToShortDateString().Replace("/", ""); //SessionManager.Get<String>(SessionKeys.LogFileName);
                
                if(String.IsNullOrEmpty(logFileName))
                    return "${basedir}/Logs/${shortdate}_TEMP.txt" ;
                else
                    return "${basedir}/Logs/${shortdate}_" + logFileName.ToUpper() + ".txt";
            }

        }
        

        private static FileTarget GetTargetFile()
        {

            FileTarget target = LogManager.Configuration.FindTargetByName(CurrentLogFileName) as FileTarget;
            if (target == null)
                target = new FileTarget();

            target.FileName = CurrentLogFileName;

            return target;
        }
         
        private static LoggingConfiguration GetConfiguration() {


            LoggingConfiguration config = LogManager.Configuration;

            var logFile = new FileTarget();
            config.AddTarget("custom", logFile);

            logFile.FileName = CurrentLogFileName;
            logFile.Layout = "${date} | ${message}";

            var rule = new LoggingRule("*", LogLevel.Info, logFile);
            config.LoggingRules.Clear();
            config.LoggingRules.Add(rule);

            //LogManager.Configuration = config;

            return config;
        }

        //private static string paramArrayToString(object[] param)
        //{
        //    return Newtonsoft.Json.JsonConvert.ToString(param);            
        //}


        public static void Log(Exception ex, object[] param = null)
        {
            //if (!(Common.Settings.LogsEnabled))
            //    return;

            //LogManager.Configuration = GetConfiguration();

            NLog.Logger logger = LogManager.GetCurrentClassLogger();
            logger.Info(ex.Message, param);
        }


        public static void Log(string info, object[] param = null)
        {
            //if (!(Common.Settings.LogsEnabled))
            //    return; 
             
             LogManager.Configuration = GetConfiguration();
             NLog.Logger logger = LogManager.GetCurrentClassLogger();

             logger.Info(info, param);
        }

        public static void LogError(String error, object[] param = null)
        {
            //if (!(Common.Settings.LogsEnabled))
            //    return;

            NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Error(error, param);
        }

        public static void LogError(Exception ex)
        {
            //if (!(Common.Settings.LogsEnabled))
            //    return;

            NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Error(ex);
        }



    }
}