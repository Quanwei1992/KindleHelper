using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace loglibrary
{
        public enum LogType
        {
            Overall,
        }
        
        public  class LogHelper:IDisposable
        {
            static MemoryStream mstream = new MemoryStream();
            static string path;
            public static string LogPath
            {
                get
                {
                    return AppDomain.CurrentDomain.BaseDirectory + @"\log";
                }
            }

            public enum LogLevel
            {
                Info,
                Error
            }

            public static void Info(string message, LogType logType = LogType.Overall)
            {
                if (string.IsNullOrEmpty(message))
                    return;
                path = string.Format(@"\{0}\", logType.ToString());

                WriteLog(path, "", message);
            }

            public static void Error(string message, LogType logType = LogType.Overall)
            {
                if (string.IsNullOrEmpty(message))
                    return;
                path = string.Format(@"\{0}\", logType.ToString());
                WriteLog(path, "Error ", message);
            }

            public static void Error(Exception e, LogType logType = LogType.Overall)
            {
                if (e == null)
                    return;
                path = string.Format(@"\{0}\", logType.ToString());
                WriteLog(path, "Error ", e.Message);
            }
            static string goodpath;
            static string filename;
            private static void WriteLog(string path, string prefix, string message)
            {
                goodpath = LogPath + path;
                filename = string.Format("{0}{1}.log", prefix, DateTime.Now.ToString("yyyyMMdd"));
                byte[] buffer = Encoding.Default.GetBytes(DateTime.Now.ToString("HH:mm:ss") + " " + message + "\r\n");
                mstream.Write(buffer,0,buffer.Length);
                mstream.Flush(); 
            }
        /// <summary>
        /// 必须在日志操作后调用
        /// </summary>
            public static void Flush()
            {
            try
            {
                if (!Directory.Exists(goodpath))
                    Directory.CreateDirectory(goodpath);
                using (FileStream fs = new FileStream(goodpath + filename, FileMode.Append, FileAccess.Write,
                                                      FileShare.Write, 1024, FileOptions.Asynchronous))
                {
                    mstream.WriteTo(fs);    
                    fs.Flush();
                    fs.Close();
                }
                }
            catch
            {
            }
            }
        #region IDisposable Support
        private  bool disposedValue = false; // 要检测冗余调用
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                // TODO: 释放托管状态(托管对象)。
                mstream.Close();
                mstream.Dispose();
                path = null;
                goodpath = null;
                filename = null;
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。
               
                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~LogHelper() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            GC.SuppressFinalize(this);
        }
        #endregion
    }
    }
