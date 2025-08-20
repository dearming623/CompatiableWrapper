using System;
using System.Collections.Generic;
using System.Threading;
using MoleQ.Support.Wrapper.Common.Util;

namespace MoleQ.Support.Wrapper.Common.Util
{

    public class LogManager
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        static LogManager()
        {
            Start();
        }

        #region 队列方法

        /// <summary>
        /// 日志队列
        /// </summary>
        private static Queue<Log> ListQueue = new Queue<Log>();

        class Log
        {
            public string File { get; set; }
            public string Msg { get; set; }
        }


        public static void WriteLog(string logFile, string msg)
        {
            Log log = new Log()
            {
                File = logFile,
                Msg = msg
            };

            ListQueue.Enqueue(log);
        }

        private static void Start()//启动
        {
            Thread thread = new Thread(threadStart);
            thread.IsBackground = true;
            thread.Start();
        }

        private static void threadStart()
        {
            while (true)
            {
                if (ListQueue.Count > 0)
                {
                    try
                    {
                        ScanQueue();
                    }
                    catch (Exception ex)
                    {
                        throw;
                        //LO_LogInfo.WLlog(ex.ToString());
                    }
                }
                else
                {
                    //没有任务，休息3秒钟
                    Thread.Sleep(1000);
                }
            }
        }
        //要执行的方法
        private static void ScanQueue()
        {
            while (ListQueue.Count > 0)
            {
                try
                {
                    //从队列中取出
                    Log log = ListQueue.Dequeue();
                    //ThreadLog(log.File, log.Msg);
                    Logger.Write(log.File, log.Msg);

                    //Console.WriteLine(queueinfo.feedid);
                    //取出的queueinfo就可以用了，里面有你要的东西
                    //以下就是处理程序了
                    //。。。。。。

                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        #endregion

        private static string logPath = string.Empty;

        /// <summary>
        /// 保存日志的文件夹
        /// </summary>
        public static string LogPath
        {
            get
            {
                if (logPath == string.Empty)
                {
                    if (System.Web.HttpContext.Current == null)
                        // Windows Forms 应用
                        logPath = AppDomain.CurrentDomain.BaseDirectory + @"Logs\";
                    else
                        // Web 应用
                        logPath = AppDomain.CurrentDomain.BaseDirectory + @"Logs\";
                }
                return logPath;
            }
            set { logPath = value; }
        }

        private static string logFielPrefix = string.Empty;
        /// <summary>
        /// 日志文件前缀
        /// </summary>
        public static string LogFielPrefix
        {
            get { return logFielPrefix; }
            set { logFielPrefix = value; }
        }


        /// <summary>
        /// 写日志
        /// </summary>
        private static void ThreadLog(string logFile, string msg)
        {
            try
            {
                System.IO.StreamWriter sw = System.IO.File.AppendText(
                    LogPath + DateTime.Now.ToString("yyyyMMdd") +
                    LogFielPrefix + " " + logFile + ".Log"

                    );
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff : ") + msg);
                sw.Close();
            }
            catch
            { }
        }

        /// <summary>
        /// 写日志
        /// </summary>
        public static void WriteLog(LogFile logFile, string msg)
        {
            WriteLog(logFile.ToString(), msg);
        }
    }

    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogFile
    {
        Trace,
        Warning,
        Error,
        SQL
    }
}
