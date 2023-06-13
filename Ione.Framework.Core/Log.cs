using Ione.Framework.Core.Logs;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace Ione.Framework.Core
{
    public static class Log
    {
        static Log()
        {
            if (!Directory.Exists(Config.PathLogs))
                Directory.CreateDirectory(Config.PathLogs);
            if (!Directory.Exists(Path.Combine(Config.PathLogs, DateTime.Now.FormatDate("yyyyMM"))))
                Directory.CreateDirectory(Path.Combine(Config.PathLogs, DateTime.Now.FormatDate("yyyyMM")));
            if (!Directory.Exists(Path.Combine(Config.PathLogs, DateTime.Now.FormatDate("yyyyMM"),"Exceptions")))
                Directory.CreateDirectory(Path.Combine(Config.PathLogs, DateTime.Now.FormatDate("yyyyMM"), "Exceptions"));
            if (!Directory.Exists(Path.Combine(Config.PathLogs, DateTime.Now.FormatDate("yyyyMM"),"Requests")))
                Directory.CreateDirectory(Path.Combine(Config.PathLogs, DateTime.Now.FormatDate("yyyyMM"), "Requests"));


        }
        public static string GetLog(this Exception ex)
        {
            string message = ex.Message;
            if (ex.InnerException != null)
                message += Environment.NewLine + ex.InnerException.Message;

            string trace = ex.StackTrace;
            if (ex.InnerException != null)
                message += Environment.NewLine + ex.InnerException.StackTrace;

            return Environment.NewLine + "#" + Environment.NewLine +
                    "Date        >> " + DateTime.Now.FormatDate("yyyy-MM-dd HH:mm:ss zzz") + "*/" + Environment.NewLine +
                    "Message     >> " + message + "*/" + Environment.NewLine +
                    "Stack Trace >> " + trace + "*/" + Environment.NewLine +
                    "Source      >> " + ex.Source + "*/" + Environment.NewLine;
        }
        public static string GetLog(this IoneException ex)
        {
            string message = ex.Message;
            if (ex.InnerException != null)
                message += Environment.NewLine + ex.InnerException.Message;

            string trace = ex.StackTrace;
            if (ex.InnerException != null)
                message += Environment.NewLine + ex.InnerException.StackTrace;

            return Environment.NewLine + "#" + Environment.NewLine +
                    "Error Code  >> " + ex.ErrorCode + "*/" + Environment.NewLine + 
                    "Date        >> " + DateTime.Now.FormatDate("yyyy-MM-dd HH:mm:ss zzz") + "*/" + Environment.NewLine +
                    "Message     >> " + message + "*/" + Environment.NewLine +
                    "Stack Trace >> " + trace + "*/" + Environment.NewLine +
                    "Source      >> " + ex.Source + "*/" + Environment.NewLine;
        }
        public static string GetLog(this Request ex)
        {
            return Environment.NewLine + "#" + Environment.NewLine +
                    "Date           >> " + ex.Date.FormatDate("yyyy-MM-dd HH:mm:ss zzz") + "*/" + Environment.NewLine +
                    "ExecutionTime  >> " + string.Format("{0}",ex.ExecutionTime) + "*/" + Environment.NewLine +
                    "Address        >> " + ex.Address + "*/" + Environment.NewLine +
                    "Input          >> " + ex.Input + "*/" + Environment.NewLine +
                    "Method         >> " + ex.Method + "*/" + Environment.NewLine +
                    "Format         >> " + ex.Format + "*/" + Environment.NewLine +
                    "Output         >> " + ex.Output + "*/" + Environment.NewLine +
                    "Status         >> " + ex.Status + "*/" + Environment.NewLine +
                    "DeviceInfo     >> " + ex.DeviceInfo.SerializeObject() + Environment.NewLine;
        }
        public static void SaveLog(this Exception ex)
        {
            try
            {
                if (ex == null)
                {
                    ex = new Exception("-");
                }
                DateTime now = DateTime.Now;

                string filelog = Path.Combine(Config.PathLogs, DateTime.Now.FormatDate("yyyyMM"), "Exceptions", String.Concat(now.Day, ".log"));
                string content = ex.GetLog();

                if (!File.Exists(filelog))
                {
                    String[] data = new string[] { content };
                    File.WriteAllLines(filelog, data);
                }
                else
                {
                    File.AppendAllText(filelog, content);
                }
            }
            catch { }
        }
        public static void SaveLog(this IoneException ex)
        {
            try
            {
                if (ex == null)
                {
                    ex = new IoneException("-");
                }
                DateTime now = DateTime.Now;

                string filelog = Path.Combine(Config.PathLogs, DateTime.Now.FormatDate("yyyyMM"), "Exceptions", String.Concat(now.Day, ".log"));
                string content = ex.GetLog();

                if (!File.Exists(filelog))
                {
                    String[] data = new string[] { content };
                    File.WriteAllLines(filelog, data);
                }
                else
                {
                    File.AppendAllText(filelog, content);
                }
            }
            catch { }
        }

        public static void SaveLog(this Request ex)
        {
            try
            {
                if (ex == null) return;
                DateTime now = DateTime.Now;

                string filelog = Path.Combine(Config.PathLogs, DateTime.Now.FormatDate("yyyyMM"), "Requests", String.Concat(now.Day, ".log"));
                string content = ex.GetLog();

                if (!File.Exists(filelog))
                {
                    String[] data = new string[] { content };
                    File.WriteAllLines(filelog, data);
                }
                else
                {
                    File.AppendAllText(filelog, content);
                }
            }
            catch { }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentMethod(Exception ex)
        {
            try
            {
                StackTrace st = new StackTrace(ex,true);
                StackFrame sf = st.GetFrame(st.FrameCount - 1);
                return sf.GetMethod().Name;
            }
            catch 
            {
                return "";
            }

        }
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentClass(Exception ex)
        {
            try
            {
                StackTrace st = new StackTrace(ex,true);
                StackFrame sf = st.GetFrame(st.FrameCount -1);
                return sf.GetType().Name;
            }
            catch
            {
                return "";
            }
        }
    }
}
