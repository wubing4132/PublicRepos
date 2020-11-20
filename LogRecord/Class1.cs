using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace LogRecord
{
    public class LogOperator
    {

        #region 日志处理

        #region 属性

        private static string LogName_Info = "InfoLog_";

        private static string LogName_Error = "ErrorLog_";

        private static Object thisLock = new Object();

        #endregion

        #region 变量

        /// <summary>
        /// 日志文件夹路径名称
        /// </summary>
        private static string LogDire = AppDomain.CurrentDomain.BaseDirectory.ToString() + "Log\\";


        #endregion

        ///<summary>
        ///日志类型
        ///</summary>
        public enum LogType
        {
            INFO,
            ERROR
        }

        /// <summary>
        /// 记录组件日志
        /// </summary>
        /// <param name="logInfo">日志内容</param>
        public static void AddLog(string Msg, LogType? logtype = null)
        {
            try
            {
                logtype = logtype ?? LogType.INFO;

                CheckDir();
                string sFileName = null;;
                if (logtype == LogType.INFO)
                {
                    sFileName = LogDire + LogName_Info + "M" + DateTime.Now.Month.ToString() + "D" + DateTime.Now.Day.ToString() + ".txt";
                }
                if (logtype == LogType.ERROR)
                {
                    sFileName = LogDire + LogName_Error + "M" + DateTime.Now.Month.ToString() + "D" + DateTime.Now.Day.ToString() + ".txt";
                }
                WriteLog(Msg, sFileName);
            }
            catch
            {
            }
        }

        private static void CheckDir()
        {
            if (!Directory.Exists(LogDire))
            {
                // 不存在，创建
                Directory.CreateDirectory(LogDire);
            }
        }

        private static void WriteLog(string Msg, string sFileName)
        {
            string LogMsg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + "\t" + Msg;

            lock (thisLock)
            {
                StreamWriter writer = File.AppendText(sFileName);
                writer.WriteLine(LogMsg);
                writer.Flush();
                writer.Close();
            }
        }

        //public static DataView ReadLog(LogType? logtype = null, DateTime? StartTime = null, DateTime? EndTime = null)
        //{
        //    logtype = logtype ?? LogType.INFO;
        //    StartTime = StartTime ?? DateTime.MinValue;
        //    EndTime = EndTime ?? DateTime.MaxValue;


        //    try
        //    {
        //        string sFileName = null;
        //        if (logtype == LogType.INFO)
        //        {
        //            sFileName = LogDire + LogName_Info + DateTime.Now.Month.ToString() + ".txt";
        //        }
        //        if (logtype == LogType.ERROR)
        //        {
        //            sFileName = LogDire + LogName_Error + DateTime.Now.Month.ToString() + ".txt";
        //        }

        //        DataTable dt = new DataTable();
        //        dt.Columns.Add("日志时间");
        //        dt.Columns.Add("日志内容");

        //        FileStream fs = new FileStream(sFileName, FileMode.Open, FileAccess.Read);
        //        StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);
        //        string strLine = sr.ReadLine();

        //        while (strLine != null)
        //        {
        //            string[] strArray = new string[3];
        //            strArray = strLine.Split('|');
        //            DataRow dr = dt.NewRow();
        //            dr[0] = strArray[0];
        //            dr[1] = strArray[1];
        //            dr[2] = strArray[2];
        //            dt.Rows.Add(dr);
        //            strLine = sr.ReadLine();
        //        }
        //        sr.Close();
        //        fs.Close();

        //        //筛选               
        //        DataView dv = dt.DefaultView;
        //        dv.RowFilter = " 日志时间 >= '" + StartTime + "' and 日志时间 <= '" + EndTime + "' ";
        //        return dv;
        //    }
        //    catch (Exception ex)
        //    {
        //        DataTable dt = new DataTable();
        //        DataView dv = new DataView();
        //        dt.Columns.Add("错误信息");
        //        dt.Rows.Add(ex.Message);
        //        dv = dt.DefaultView;
        //        return dv;
        //    }

        //}

        #endregion
    }
}
