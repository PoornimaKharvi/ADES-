using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Media.Media3D;
using ADES_22.Model;

namespace ADES_22
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        public string UserID = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                UserID = Request.QueryString["UserID"];
                ViewState["UserID"] = UserID;

                DateTime ExpiryTime = DBAccess.DBAccess.GetExpiryDateTime(UserID);
                DateTime CurrentTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));

                if (ExpiryTime < CurrentTime)
                {
                    Response.Redirect(string.Format("~/ValidityFailForm.aspx?UserID={0}", UserID), false);
                }

                TxtNewPass.Focus();
            }
        }

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            try
            {
                DBAccess.DBAccess.ChangeUserPassword(TxtNewPass.Text, (String)ViewState["UserID"]);

                string EmailID = DBAccess.DBAccess.GetEmailID((String)ViewState["UserID"]);
                SendEmail(EmailID);

                DateTime dt = new DateTime();
                DBAccess.DBAccess.UpdateExpiryDate("Updated", dt, (String)ViewState["UserID"]);

                TxtNewPass.ReadOnly = true;
                TxtConfirmPass.ReadOnly = true;
                BtnReset.Enabled = false;

                ErrorMsg.Text = "Password Changed!";
                ErrorMsg.ForeColor = System.Drawing.Color.LimeGreen;
                ErrorMsg.Style.Add("margin-left", "200px");
                ErrorMsg.Style.Add("font-size", "x-large");
                ErrorMsg.Style.Add("font-weight", "400");
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("BtnReset_Click: " + ex.Message);
            }
        }

        protected void Link_BackToLogin_Click(object sender, EventArgs e)
        {
            try 
            {
                Response.Redirect("~/Login.aspx");
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("Link_BackToLogin_Click: " + ex.Message);
            }
        }

        public void SendEmail(string EmailID)
        {
            try
            {
                WService ws = new WService();
                string AppPass = HelperClass.AppPassword;
                ws.From = HelperClass.Email_ID + ";" + HelperClass.EncodePasswordToBase64(AppPass);
                ws.To = EmailID;
                ws.Subject = "ADES - Password Change";
                ws.MsgBody = "Your password was changed on " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                DBAccess.DBAccess.InsertEmailDetails(ws);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("SendEmail: " + ex.Message);
            }
        }
    }
}