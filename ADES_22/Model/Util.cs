using ADES_22.DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ADES_22.Model
{
    public class Util
    {
        public static string CompanyMachineLabel = "Company >> Machine";
        public static string ListName = "Basket Name";
        public static string MachineName = "Machine";
        public static string CompanyEmail = "amitpeenyabangalore@gmail.com";
        public static string CompanyEmailPassword = "caznnekogxmpsjed";

        static string[] formats = new string[] { "dd-MM-yyyy HH:mm:ss", "dd-MM-yyyy HH:mm", "dd-MM-yyyy", "dd-MMM-yyyy", "dd-MMM-yyyy HH:mm", "dd-MMM-yyyy HH:mm:ss", "yyyy-MM-dd", "yyyy-MM-dd HH:mm:ss", "dd-MM-yyyyTHH:mm:ss", "dd-MM-yyyyTHH:mm", "dd-MMM-yyyyTHH:mm", "dd-MMM-yyyyTHH:mm:ss", "yyyy-MM-ddTHH:mm:ss", "yyyy-MM-ddTHH:mm", "dd-MM-yyyy HH:mm:ss tt", "dd-MM-yyyy H:mm:ss tt", "d-MM-yyyy HH:mm:ss tt", "d-M-yyyy hh:mm:ss tt", "d-M-yyyy HH:mm:ss tt", "MM-dd-yyyy HH:mm:ss tt", "M-d-yyyy HH:mm:ss tt", "yyyy-MM-dd HH:mm:ss tt", "yyyy-M-d HH:mm:ss tt", "MM/dd/yyyy hh:mm:ss tt", "dd/MM/yyyy HH:mm:ss", "M/dd/yyyy hh:mm:ss tt", "MM/d/yyyy hh:mm:ss tt", "M/d/yyyy hh:mm:ss tt", "M/dd/yyyy h:mm:ss tt", "MM/d/yyyy h:mm:ss tt", "M/d/yyyy h:mm:ss tt", "MM/dd/yyyy h:mm:ss tt", "dd-MM-yyyy HH:mm:ss.fff", "HH:mm:ss", "yyyy-MMM-dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss.fff", "dd-MM-yyyy hh:mm:ss tt" };
        public static DateTime GetDateTime(string strDatetime)
        {
            DateTime datetime = DateTime.Now;
            if (!DateTime.TryParseExact(strDatetime.Trim(), formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out datetime))
            {
                datetime = DateTime.Now;
                Logger.WriteErrorLog(string.Format("Not able to convert datetime string {0} to DateTime", strDatetime));
            }
            return datetime;
        }
        public static string GetLogicalDayStart(string LRunDay)
        {
            Logger.WriteDebugLog(LRunDay);
            DateTime ts = Util.GetDateTime(LRunDay);
            SqlConnection Con = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand("SELECT dbo.f_GetLogicalDayStart( '" + ts.ToString("yyyy-MM-dd 13:00:00") + "')", Con);

            object SEDate = null;
            try
            {
                SEDate = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex);
            }
            finally
            {
                if (Con != null)
                {
                    Con.Close();
                }
            }
            if (SEDate == null || Convert.IsDBNull(SEDate))
            {
                return string.Empty;
            }
            return string.Format("{0:yyyy-MM-dd HH:mm:ss}", Convert.ToDateTime(SEDate));
        }
        public static string GetLogicalDayEnd(string LRunDay)
        {
            string dateString = string.Empty;
            DateTime dateValue = Util.GetDateTime(LRunDay);

            SqlConnection Con = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand("SELECT dbo.f_GetLogicalDayEnd( '" + dateValue.ToString("yyyy-MM-dd 13:00:00") + "')", Con);


            object SEDate = null;
            try
            {
                SEDate = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex);
            }
            finally
            {
                if (Con != null)
                {
                    Con.Close();
                }
            }
            if (SEDate == null || Convert.IsDBNull(SEDate))
            {
                return string.Empty;
            }
            return string.Format("{0:yyyy-MM-dd HH:mm:ss}", Convert.ToDateTime(SEDate));
        }
        public static string CurrentStartEndTime(string date, out string todate, out string shiftName)
        {
            SqlConnection Con = ConnectionManager.GetConnection();
            SqlCommand Cmd = null;
            SqlDataReader reader = null;
            string frmDate = string.Empty;
            todate = shiftName = string.Empty;
            try
            {
                Cmd = new SqlCommand("s_GetCurrentShiftTime", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@StartDate", date);
                //Cmd.Parameters.AddWithValue("@CompanyID", company);
                reader = Cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        frmDate = Convert.ToDateTime(reader["StartTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");//"2018-10-24 06:00:00"
                        todate = Convert.ToDateTime(reader["Endtime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");//"2018-10-24 14:00:00";
                        shiftName = reader["ShiftName"].ToString();//""
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.ToString());
            }
            finally
            {
                if (reader != null) reader.Close();
                if (Con != null) Con.Close();
            }
            return frmDate;
        }
        public static string getDatePart(string input)
        {
            string output = "";
            try
            {
                if (input.Contains(" "))
                {
                    output = input.Split(' ')[0];
                }
                else
                {
                    output = input;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("getDatePart: " + ex.Message);
            }
            return output;
        }
        public static string getDateTimePart(string input, string timepart)
        {
            string output = "";
            try
            {
                if (input.Contains(" "))
                {
                    output = input;
                }
                else
                {
                    if (timepart == "")
                    {
                        output = input + " " + DateTime.Now.ToString("HH:mm:ss");
                    }
                    else
                    {
                        output = input + " " + timepart;
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("getDateTimePart: " + ex.Message);
            }
            return output;
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public static string getHHMMtimeFormate(string input)
        {
            string output = "";
            try
            {
                if (input != "")
                {
                    string date = Regex.Split(input, " ")[0];
                    string time = Regex.Split(input, " ")[1];
                    if (Regex.Split(time, ":").Length == 3)
                    {
                        output = date + " " + Regex.Split(time, ":")[0] + ":" + Regex.Split(time, ":")[1];
                    }
                    else
                    {
                        output = input;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("getHHMMtimeFormate: " + ex.Message);
            }
            return output;
        }
        public static string getHHMMSStimeFormate(string input)
        {
            string output = "";
            try
            {
                if (input != "")
                {
                    string date = Regex.Split(input, " ")[0];
                    string time = Regex.Split(input, " ")[1];
                    if (Regex.Split(time, ":").Length == 3)
                    {

                        output = input;
                    }
                    else
                    {
                        output = date + " " + Regex.Split(time, ":")[0] + ":" + Regex.Split(time, ":")[1] + ":00";
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("getHHMMSStimeFormate: " + ex.Message);
            }
            return output;
        }
    }
    public static class ExtentionMethods
    {
        public static string ToSQLDateTimeFormat(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string ToSQLDateFormat(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd");
        }

        public static Boolean IsBetween(this DateTime dt, DateTime startDate, DateTime endDate, Boolean compareTime = false)
        {
            return compareTime ?
               dt >= startDate && dt <= endDate :
               dt.Date >= startDate.Date && dt.Date <= endDate.Date;
        }
    }
}