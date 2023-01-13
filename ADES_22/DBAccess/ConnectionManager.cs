using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Configuration;

namespace ADES_22.DBAccess
{
    public class ConnectionManager
    {
        static string conString = WebConfigurationManager.ConnectionStrings["ConnString"].ToString();
        public static bool timeOut = false;

        public static SqlConnection GetConnection()
        {
            bool writeDown = false;
            DateTime dt = DateTime.Now;
            SqlConnection conn = null;

            if (HttpContext.Current == null || HttpContext.Current.Session == null || HttpContext.Current.Session["connectionString"] == null)
            {
                conn = new SqlConnection(conString);
            }
            else
            {
                conString = HttpContext.Current.Session["connectionString"] as string;
                conn = new SqlConnection(conString);
            }

            do
            {
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    if (writeDown == false)
                    {
                        dt = DateTime.Now.AddSeconds(60);
                        Logger.WriteErrorLog(ex.Message);
                        writeDown = true;

                    }
                    if (dt < DateTime.Now)
                    {
                        Logger.WriteErrorLog(ex.Message);
                        throw;
                    }

                    Thread.Sleep(1000);
                }

            } while (conn.State != ConnectionState.Open);
            return conn;
        }
    }
}