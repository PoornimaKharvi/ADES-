using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADES_22.Model
{
    public class HelperClass
    {
        static string[] formats = new string[] { "dd-MM-yyyy HH:mm:ss", "dd-MM-yyyy HH:mm", "dd-MM-yyyy", "dd-MMM-yyyy", "dd-MMM-yyyy HH:mm", "dd-MMM-yyyy HH:mm:ss", "yyyy-MM-dd", "yyyy-MM-dd HH:mm:ss", "dd-MM-yyyyTHH:mm:ss", "dd-MM-yyyyTHH:mm", "dd-MMM-yyyyTHH:mm", "dd-MMM-yyyyTHH:mm:ss", "yyyy-MM-ddTHH:mm:ss", "yyyy-MM-ddTHH:mm", "dd-MM-yyyy HH:mm:ss tt", "dd-MM-yyyy H:mm:ss tt", "d-MM-yyyy HH:mm:ss tt", "d-M-yyyy hh:mm:ss tt", "d-M-yyyy HH:mm:ss tt", "MM-dd-yyyy HH:mm:ss tt", "M-d-yyyy HH:mm:ss tt", "yyyy-MM-dd HH:mm:ss tt", "yyyy-M-d HH:mm:ss tt", "MM/dd/yyyy hh:mm:ss tt", "dd/MM/yyyy HH:mm:ss", "M/dd/yyyy hh:mm:ss tt", "MM/d/yyyy hh:mm:ss tt", "M/d/yyyy hh:mm:ss tt", "M/dd/yyyy h:mm:ss tt", "MM/d/yyyy h:mm:ss tt", "M/d/yyyy h:mm:ss tt", "MM/dd/yyyy h:mm:ss tt", "dd-MM-yyyy HH:mm:ss.fff", "HH:mm:ss", "yyyy-MMM-dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss.fff", "d/MM/yyyy", "dd/MM/yyyy", "d/M/yyyy", "dd/M/yyyy", "MM/dd/yyyy", "M/dd/yyyy", "M/d/yyyy", "MM/d/yyyy", "dd-MMM-yy" };

        public static string Email_ID = "amitpeenyabangalore@gmail.com";
        public static string AppPassword = "caznnekogxmpsjed";

        public static DateTime GetDateTime(string strDatetime)
        {
            DateTime datetime = DateTime.Now;

            try
            {
                if (!DateTime.TryParseExact(strDatetime.Trim(), formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out datetime))
                {
                    datetime = DateTime.Now;
                    Logger.WriteErrorLog(string.Format("Not able to convert datetime string {0} to DateTime", strDatetime));
                }
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("GetDateTime: " + ex.Message);
            }

            return datetime;
        }

        public static void OpenModal(Page page, string modalId, bool isAnimationRequired)
        {
            try
            {
                ScriptManager.RegisterStartupScript(page, page.GetType(), modalId, "OpenModal('" + modalId + "','" + isAnimationRequired + "');", true);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("OpenModal: " + ex.Message);
            }
        }

        public static void ClearModal(Page page)
        {
            try
            {
                ScriptManager.RegisterStartupScript(page.Page, page.GetType(), "clearModal", "clearscreen();", true);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("ClearModal: " + ex.Message);
            }
        }

        public static void OpenSuccessToaster(Page page, string msg)
        {
            try
            {           
                ScriptManager.RegisterStartupScript(page, page.GetType(), "successMsg", "SuccessToastr('" + msg + "','');", true);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("OpenSuccessToaster: " + ex.Message);
            }
        }

        public static void OpenWarningToaster(Page page, string msg)
        {
            try
            {
                ScriptManager.RegisterStartupScript(page, page.GetType(), "WarningMsg", "WarningToastr('" + msg + "','');", true);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("OpenWarningToaster: " + ex.Message);
            }
        }

        public static void OpenWarningModal(Page page, string msg)
        {
            try
            {
                ScriptManager.RegisterStartupScript(page, page.GetType(), "warningModal", "OpenWarningModal('" + msg + "');", true);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("OpenWarningModal: " + ex.Message);
            }
        }

        public static void OpenErrorModal(Page page, string msg)
        {
            try
            {
                ScriptManager.RegisterStartupScript(page, page.GetType(), "errorModal", "OpenErrorModal('" + msg + "');", true);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("OpenErrorModal: " + ex.Message);
            }
        }

        public static void OpenValidationModal(Page page,string msg)
        {
            try
            {
                ScriptManager.RegisterStartupScript(page, page.GetType(), "validationModal", "openValidationModal('" + msg + "');", true);
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("openValidationModal:" + ex.Message);
            }
        }

        public static void openConfirmModal(Page page,string msg)
        {
            try
            {
                ScriptManager.RegisterStartupScript(page, page.GetType(), "confirmModel", "openConfirmModal('" + msg + "');", true);
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("openConfirmModal:" + ex.Message);
            }
        }

        public static string EncodePasswordToBase64(string password)
        {
            string EncodedData = string.Empty;

            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                EncodedData = Convert.ToBase64String(encData_byte);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }

            return EncodedData;
        }

        public static string DecodeFrom64(string encodedData)
        {
            string Result = string.Empty;

            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(encodedData);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                Result = new String(decoded_char);
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("DecodeFrom64: " + ex.Message);
            }

            return Result;
        }

        public static List<ListItem> BindIssueType()
        {
            List<ListItem> list = new List<ListItem>(); 

            try
            {
                list= DBAccess.DBAccess.BindIssueTypeValues();
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("BindIssueType: " + ex.Message);
            }
            
            return list;
        }

        public static string IssueTypeDisplayName(string IType)
        {
            string DisplayName = string.Empty;

            try
            {
                List<ListItem> list = BindIssueType();
                DisplayName = list.Where(x => x.Value == IType).Select(x => x.Text).FirstOrDefault();  
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("IssueTypeDisplayName: " + ex.Message);
            }

            return DisplayName;
        }

        public static List<ListItem> BindTaskType()
        {
            List<ListItem> list = new List<ListItem>();

            try
            {
                list.Add(new ListItem() { Text = "Development", Value = "Development" });
                list.Add(new ListItem() { Text = "Test", Value = "Test" });
                list.Add(new ListItem() { Text = "Support", Value = "Support" });
                list.Add(new ListItem() { Text = "Deployment", Value = "Deployment" });
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindTaskType: " + ex.Message);
            }

            return list;
        }

        public static string TaskTypeDisplayName(string IType)
        {
            string DisplayName = string.Empty;

            try
            {
                List<ListItem> list = BindTaskType();
                DisplayName = list.Where(x => x.Value == IType).Select(x => x.Text).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("IssueTypeDisplayName: " + ex.Message);
            }

            return DisplayName;
        }

        public static List<ListItem> BindDependenciues()
        {
            List<ListItem> list = new List<ListItem>();

            try
            {
                list.Add(new ListItem() { Text = "Customer", Value = "Customer" });
                list.Add(new ListItem() { Text = "Development", Value = "Development" });
                list.Add(new ListItem() { Text = "Financial", Value = "Financial" });
                list.Add(new ListItem() { Text = "Application", Value = "Application" });
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindTaskType: " + ex.Message);
            }

            return list;
        }
        public static List<ListItem> GetCourierName()
        {
            List<ListItem> list = new List<ListItem>();

            try
            {
                list.Add(new ListItem() { Text = "DTDC", Value = "DTDC" });
                list.Add(new ListItem() { Text = "Blue Dart", Value = "Blue Dart" });
                list.Add(new ListItem() { Text = "Professional Courier", Value = "Professional Courier" });
                list.Add(new ListItem() { Text = "Shree Maruthi Courier", Value = "Shree Maruthi Courier" });
                list.Add(new ListItem() { Text = "ST Courier", Value = "ST Courier" });
                list.Add(new ListItem() { Text = "VRL Travels", Value = "VRL Travels" });
                list.Add(new ListItem() { Text = "Flyking Courier", Value = "Flyking Courier" });
                list.Add(new ListItem() { Text = "DHL", Value = "DHL" });
                list.Add(new ListItem() { Text = "GMS Courier Services", Value = "GMS Courier Services" });
                list.Add(new ListItem() { Text = "By Hand", Value = "By Hand" });
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindCourierName: " + ex.Message);
            }

            return list;
        }

        public static List<ListItem> GetStatusValue()
        {
            List<ListItem> list = new List<ListItem>();

            try
            {
                list.Add(new ListItem() { Text = "New", Value = "New" });
                list.Add(new ListItem() { Text = "Waiting", Value = "Waiting" });
                list.Add(new ListItem() { Text = "Dispatched", Value = "Dispatched" });
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindStatusValue: " + ex.Message);
            }

            return list;
        }

        public static List<ListItem> GetMainTaskStatusValues()
        {
            List<ListItem> list = new List<ListItem>();

            try
            {
                list.Add(new ListItem() { Text = "Completed", Value = "Completed" });
                list.Add(new ListItem() { Text = "InProgress", Value = "InProgress" });
                list.Add(new ListItem() { Text = "Pending", Value = "Pending" });
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetStatusValues:" + ex.Message);
            }

            return list;
        }

        public static StringBuilder GetStringBuilder(List<string> result)
        {
            StringBuilder stringbuilder = new StringBuilder();

            try
            {
                for (int i = 0; i < result.Count; i++)
                {
                    stringbuilder.Append(String.Format("<option value='{0}'/>", result[i]));
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetStringBuilder: " + ex.Message);
            }

            return stringbuilder;
        }
    }
}