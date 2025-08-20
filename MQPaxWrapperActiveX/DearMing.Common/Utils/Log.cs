#region <<Notes>>
/*----------------------------------------------------------------
 * Copy right (c) 2024  All rights reserved
 * CLR Ver: 4.0.30319.42000
 * Computer: MOLEQ-MING
 * Company: 
 * namespace: DearMing.Common.Utils
 * Unique: 862ddaab-ac04-4b45-81fc-0c4423fd0f89
 * File name: Log
 * Domain: MOLEQ-MING
 * 
 * @author: Ming
 * @email: t8ming@live.com
 * @date: 07/18/2024 17:00:31
 *----------------------------------------------------------------*/
#endregion <<Notes>>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NLog.Layouts;
using NLog;

namespace DearMing.Common.Utils
{
    public class Log
    {
        public static void f(string tag, string msg)
        {
            logger.Fatal(GetLogMsg(tag, msg));
        }

        private static string GetLogMsg(string tag, string msg)
        {
            return string.Format("{0}: {1}", tag, msg);
        }

        public static void Flush()
        {
            NLog.LogManager.Flush();
        }

        /// <summary>
        /// Flush any pending log messages (in case of asynchronous targets).
        /// </summary>
        /// <param name="timeoutMilliseconds">Maximum time to allow for the flush. Any messages after that time will be discarded.</param>
        public static void Flush(int? timeoutMilliseconds)
        {
            if (timeoutMilliseconds != null)
                NLog.LogManager.Flush(timeoutMilliseconds.Value);

            NLog.LogManager.Flush();
        }

        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();


        public static void i(string tag, string msg)
        {
            string str = GetLogMsg(tag, msg);
            logger.Info(str);
        }

        public static void d(string tag, string msg)
        {
            string str = GetLogMsg(tag, msg);
            logger.Debug(str);
        }

        public static void e(string tag, string msg)
        {
            logger.Error(GetLogMsg(tag, msg));
        }

        public static void w(string tag, string msg)
        {
            logger.Warn(GetLogMsg(tag, msg));
        }

        public static void t(string tag, string msg)
        {
            logger.Trace(GetLogMsg(tag, msg));
        }

        public static void Enable()
        {
            SetEnable(true);
        }

        public static void Disable()
        {
            SetEnable(false);
        }

        public static bool IsConfiged()
        {
            return null != NLog.LogManager.Configuration;
        }

        public static void SetEnable(bool enable)
        {
            if (!enable)
            {
                LogManager.Configuration = null;
                return;
            }

            string basedir = System.AppDomain.CurrentDomain.BaseDirectory;
            string shortdate = DateTime.Now.ToString("yyyy-MM-dd");
            string path = Path.Combine(basedir, "logs");
            path = Path.Combine(path, shortdate + ".log");
            var config = new NLog.Config.LoggingConfiguration();
            var logfile = new NLog.Targets.FileTarget("logfile")
            {
                FileName = path,
                Layout = new NLog.Layouts.CsvLayout()
                {
                    WithHeader = false,
                    Delimiter = CsvColumnDelimiterMode.Custom,
                    CustomColumnDelimiter = "",
                    Columns = {
                        new CsvColumn("time","[${longdate}] "),
                        new CsvColumn("level", "${substring:inner=${uppercase:${level}}:length=1:start=0}/"),
                        new CsvColumn("message", "${message}"),
                    }
                },
            };

            config.AddRule(NLog.LogLevel.Debug, NLog.LogLevel.Fatal, logfile, "*");
            NLog.LogManager.Configuration = config;

            //logger = NLog.LogManager.GetCurrentClassLogger(); 
            //logger.Debug("打印测试日志"); 
        }
    }
}
