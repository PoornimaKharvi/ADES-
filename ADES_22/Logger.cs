using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Web;

namespace ADES_22
{
    public class Logger
    {
        private static readonly string appPath;
        private static readonly object _locker;

        static Logger()
        {
            string str = string.Format("{0:dd_MMM_yyyy}", (object)DateTime.Now);
            Logger.appPath = Path.GetDirectoryName(HttpContext.Current.Server.MapPath("~/"));
            Logger._locker = new object();
            string path = Logger.appPath + "\\Logs";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            Logger.appPath = Path.Combine(Logger.appPath, "Logs\\FactoryLog_" + str + ".txt");
        }

        public static void WriteDebugLog(string str)
        {
            StreamWriter streamWriter = (StreamWriter)null;
            if (!Monitor.TryEnter(Logger._locker, 1000))
                return;
            try
            {
                streamWriter = new StreamWriter(Logger.appPath, true, Encoding.UTF8, 8195);
                streamWriter.WriteLine(string.Format("{0} : DEBUG - {1}", (object)DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), (object)str));
                streamWriter.Flush();
                streamWriter.Close();
                streamWriter.Dispose();
                streamWriter = (StreamWriter)null;
            }
            catch
            {
            }
            finally
            {
                Monitor.Exit(Logger._locker);
                if (streamWriter != null)
                {
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
        }

        public static void WriteErrorLog(string str)
        {
            StreamWriter streamWriter = (StreamWriter)null;
            if (!Monitor.TryEnter(Logger._locker, 1000))
                return;
            try
            {
                streamWriter = new StreamWriter(Logger.appPath, true, Encoding.UTF8, 8195);
                streamWriter.WriteLine(string.Format("{0} : EXCEPTION - {1}", (object)DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), (object)str));
                streamWriter.Flush();
                streamWriter.Close();
                streamWriter.Dispose();
                streamWriter = (StreamWriter)null;
            }
            catch
            {
            }
            finally
            {
                Monitor.Exit(Logger._locker);
                if (streamWriter != null)
                {
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
        }

        public static void WriteErrorLog(Exception ex)
        {
            StreamWriter streamWriter = (StreamWriter)null;
            if (!Monitor.TryEnter(Logger._locker, 1000))
                return;
            try
            {
                streamWriter = new StreamWriter(Logger.appPath, true, Encoding.UTF8, 8195);
                streamWriter.WriteLine(string.Format("{0} : Exception - {1}", (object)DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), (object)ex));
                streamWriter.Flush();
                streamWriter.Close();
                streamWriter.Dispose();
                streamWriter = (StreamWriter)null;
            }
            catch
            {
            }
            finally
            {
                Monitor.Exit(Logger._locker);
                if (streamWriter != null)
                {
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
        }
    }
}