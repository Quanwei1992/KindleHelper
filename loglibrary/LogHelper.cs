using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace loglibrary
{
          /// <summary>
         /// 日志类型
        /// </summary>
        public enum LogType
        {
            Overall,
        }
        /// <summary>
        /// 日志文件写入工具类，项目中第二个核心类
        /// </summary>
        public  class LogHelper:IDisposable
        {
            /// <summary>
            /// 内部内存流
            /// </summary>
            static MemoryStream mstream = new MemoryStream();
            /// <summary>
            /// 日志文件路径
            /// </summary>
            static string path;
            /// <summary>
            /// 日志文件路径，默认为当前可执行文件目录下的log目录
            /// </summary>
            public static string LogPath
            {
                get
                {
                    return AppDomain.CurrentDomain.BaseDirectory + @"\log";
                }
            }
            /// <summary>
            /// 写入信息
            /// </summary>
            /// <param name="message">信息文本</param>
            /// <param name="logType">log类型</param>
            public static void Info(string message, LogType logType = LogType.Overall)
            {
                if (string.IsNullOrEmpty(message))
                    return;
                path = string.Format(@"\{0}\", logType.ToString());

                WriteLog(path, "", message);
            }
            /// <summary>
            /// 记录所产生的错误，只有Flush后才会被写入日志文件
            /// </summary>
            /// <param name="message">错误信息</param>
            /// <param name="logType">log类型</param>
            public static void Error(string message, LogType logType = LogType.Overall)
            {
                if (string.IsNullOrEmpty(message))
                    return;
                path = string.Format(@"\{0}\", logType.ToString());
                WriteLog(path, "Error ", message);
            }
            /// <summary>
            /// 记录所产生的错误，只有Flush后才会被写入日志文件
            /// </summary>
            /// <param name="e">包含错误信息的异常</param>
            /// <param name="logType">log类型</param>
            public static void Error(Exception e, LogType logType = LogType.Overall)
            {
                if (e == null)
                    return;
                path = string.Format(@"\{0}\", logType.ToString());
                WriteLog(path, "Error ", e.Message);
            }
            /// <summary>
            /// 完整的文件路径
            /// </summary>
            static string _FullLogPath;
            /// <summary>
            /// 这是只读的
            /// 外部可见的完整的文件路径
            /// </summary>
            public static string FullLogPath
            {
                get { return LogHelper._FullLogPath; }
            }
            /// <summary>
            /// 日志文件名
            /// </summary>
            static string filename;
            /// <summary>
            /// 写入日志，此方法为支持方法，不应该直接调用（好吧，你也调用不了）
            /// </summary>
            /// <param name="path">路径</param>
            /// <param name="prefix">日志类型</param>
            /// <param name="message">信息</param>
            private static void WriteLog(string path, string prefix, string message)
            {
                _FullLogPath = LogPath + path;
                filename = string.Format("{0}{1}.log", prefix, DateTime.Now.ToString("yyyyMMdd"));
                byte[] buffer = Encoding.Default.GetBytes(DateTime.Now.ToString("HH:mm:ss") + " " + message + "\r\n");
                mstream.Write(buffer,0,buffer.Length);
            }
            /// <summary>
           /// 必须在日志操作后调用
           /// </summary>
            public static void Flush()
            {
             try
             {
                if (!Directory.Exists(_FullLogPath))
                    Directory.CreateDirectory(_FullLogPath);
                using (FileStream fs = new FileStream(_FullLogPath + filename, FileMode.Append, FileAccess.Write,
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
            /// <summary>
            /// 释放资源
            /// </summary>
            /// <param name="disposing">是否释放非托管资源，为否则释放</param>
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
                _FullLogPath = null;
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
            //GC.SuppressFinalize(this);//Fix:注释部分错误代码，此代码可能导致对象无法被正确释放
        }
        #endregion

        public static void WarnFormat(string format, object args)
        {
            string message=string.Format(format,args);
            if (string.IsNullOrEmpty(message))
                return;
            path = string.Format(@"\{0}\", LogType.Overall.ToString());
            WriteLog(path, "Warning ", message);
        }
        }
    }
