using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADES_22.Model;

namespace ADES_22
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ErrorMsg.Visible = false;
            }
        }

        protected void BtnSendLink_Click(object sender, EventArgs e)
        {
            try
            {
                string EmailID = DBAccess.DBAccess.CheckUserID(TxtUserID.Text);

                if (EmailID == null)
                {
                    ErrorMsg.Visible = true;
                    ErrorMsg.Text = "Please enter valid User-ID!";
                    ErrorMsg.ForeColor = Color.Red;
                    ErrorMsg.Style.Add("margin-left", "215px");
                }
                else
                {
                    WService ws = new WService();
                    string AppPass = HelperClass.AppPassword;
                    ws.From = HelperClass.Email_ID + ";" + HelperClass.EncodePasswordToBase64(AppPass);
                    ws.To = EmailID;
                    ws.Subject = "ADES - Password Change Request";
                    ws.MsgBody = string.Format("We received a request to reset your ADES Login Password. <br/><br/>Click on the following link to reset your password: <br/><br/>" + ConfigurationManager.AppSettings["ResetPasswordLink"].ToString() + "ChangePassword.aspx?UserID={0}", TxtUserID.Text);

                    DBAccess.DBAccess.InsertEmailDetails(ws);

                    DateTime dt = Convert.ToDateTime(DateTime.Now.AddMinutes(30).ToString("yyyy-MM-dd HH:mm"));
                    DBAccess.DBAccess.UpdateExpiryDate("Forgot", dt,TxtUserID.Text);

                    ErrorMsg.Visible = true;
                    ErrorMsg.Text = "Link has been sent to your registered email!";
                    ErrorMsg.ForeColor = Color.Black;
                    ErrorMsg.Style.Add("margin-left", "165px");
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BtnSendLink_Click: " + ex.Message);
            }
        }

        protected void Link_BackToLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx");
        }
    }
}