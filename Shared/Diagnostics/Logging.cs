using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Configuration;
using System.Web;

namespace Shared.Diagnostics.Logging
{
    public static class Log
    {
        private static string AppName = ConfigurationManager.AppSettings["AppName"];
        public static Logger Instance { get; private set; }
        static Log()
        {

#if DEBUG
            // Setup the logging view for Sentinel - http://sentinel.codeplex.com
            var sentinalTarget = new NLogViewerTarget()
            {
                Name = "sentinal",
                Address = "udp://127.0.0.1:9999",
                IncludeNLogData = false
            };
            var sentinalRule = new LoggingRule("*", LogLevel.Trace, sentinalTarget);
            LogManager.Configuration.AddTarget("sentinal", sentinalTarget);
            LogManager.Configuration.LoggingRules.Add(sentinalRule);

            // Setup the logging view for Harvester - http://harvester.codeplex.com
            var harvesterTarget = new OutputDebugStringTarget()
            {
                Name = "harvester",
                Layout = "${log4jxmlevent:includeNLogData=false}"
            };
            var harvesterRule = new LoggingRule("*", LogLevel.Trace, harvesterTarget);
            LogManager.Configuration.AddTarget("harvester", harvesterTarget);
            LogManager.Configuration.LoggingRules.Add(harvesterRule);
#endif

            /**** FILE Log Target + Rule ******************************/
            var fileTarget = new NLog.Targets.FileTarget()
            {

                Name = "file",
                Layout = "${longdate} - ${level:uppercase=true}: ${message}${onexception:${newline}EXCEPTION\\: ${exception:format=ToString}}",
                FileName = "${basedir}/Logs/" + AppName + "_${shortdate}.log",// HttpContext.Current.Server.MapPath("~") + "\\Logs\\" + DateTime.Now.ToShortDateString() + ".txt", //"${basedir}/logs/{shortdate}.txt",
                KeepFileOpen = false,
                ArchiveFileName = "${basedir}/Logs/archive/Debug_" + AppName + "_${shortdate}.{##}.log",
                ArchiveNumbering = ArchiveNumberingMode.Sequence,
                ArchiveEvery = FileArchivePeriod.Day,
                MaxArchiveFiles = 30,
                CreateDirs = true

            };
            var fileTargetRule = new LoggingRule("*", LogLevel.Trace, fileTarget);
            LogManager.Configuration.AddTarget("file", fileTarget);
            LogManager.Configuration.LoggingRules.Add(fileTargetRule);
            /**********************************************************/

            /**** EVENT LOG Target + Rule *************************/
            var eventLogTarget = new NLog.Targets.EventLogTarget()
            {
                Name = "eventlog",
                Source = AppName,
                Layout = "${message}${newline}${exception:format=ToString}",
                Category = AppName + " Error",
                EntryType = "NLog"

            };
            var eventlogTargetRule = new LoggingRule("*", LogLevel.Error, eventLogTarget);
            LogManager.Configuration.AddTarget("eventlog", eventLogTarget);
            LogManager.Configuration.LoggingRules.Add(eventlogTargetRule);
            /**********************************************************/


            LogManager.ReconfigExistingLoggers();

            Instance = LogManager.GetCurrentClassLogger();
        }

        public static string GetDetailedError(Exception ex)
        {
            string description = string.Empty;

            Exception tmpException = ex;

            while (tmpException != null)
            {
                description += tmpException.Message + Environment.NewLine;
                description += tmpException.StackTrace + Environment.NewLine;
                description += "--" + Environment.NewLine;

                tmpException = tmpException.InnerException;
            }

            return description;
        }
    }
}