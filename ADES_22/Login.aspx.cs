using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADES_22.DBAccess;
using ADES_22.Model;

namespace ADES_22
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtusername.Focus();
                errormsg.Visible = false;
            }
        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            try
            {
                errormsg.Text = "Incorrect Username or Password!";

                string passwd = DBAccess.DBAccess.CheckLoginData(txtusername.Text);

                if (passwd != null)
                {
                    string pass = txtpass.Text;
                    if (passwd == pass)
                    {
                        EmpDetails emp = new EmpDetails
                        {
                            EmpID = txtusername.Text,
                            Password = txtpass.Text
                        };

                        List<EmpDetails> list = new List<EmpDetails>();
                        list = DBAccess.DBAccess.CheckUserRole(emp);

                        if (list.Count == 0)
                            errormsg.Visible = true;
                        else
                        {
                            string Dept = list[0].Dept.ToString();
                            string Role = list[0].Role.ToString();
                            string Name = list[0].EmpName.ToString();

                            Session["Dept"] = Dept.ToString();
                            Session["Role"] = Role.ToString();
                            Session["Name"] = Name.ToString();

                            if (!string.IsNullOrWhiteSpace(Dept) && !string.IsNullOrWhiteSpace(Role))
                            {
                                Session["username"] = txtusername.Text;
                                Session["Dept"] = Dept;
                                Session["Role"] = Role;
                                Session["login"] = "Yes";

                                if (Dept == "Admin" && Role == "Admin")
                                {
                                    FormsAuthentication.SetAuthCookie(this.txtusername.Text.Trim(), true);
                                    FormsAuthenticationTicket ticket1 = new FormsAuthenticationTicket(1, this.txtusername.Text.Trim(),
                                        DateTime.Now, DateTime.Now.AddMinutes(480), true, txtusername.Text);
                                    HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket1));
                                    Response.Cookies.Add(cookie1);
                                    Response.Redirect("~/Emp_Info.aspx", false);
                                    return;
                                }

                                else if (Role == "Team Member")
                                {
                                    FormsAuthentication.SetAuthCookie(this.txtusername.Text.Trim(), true);
                                    FormsAuthenticationTicket ticket1 = new FormsAuthenticationTicket(1, this.txtusername.Text.Trim(),
                                        DateTime.Now, DateTime.Now.AddMinutes(480), true, txtusername.Text);
                                    HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket1));
                                    Response.Cookies.Add(cookie1);
                                    Response.Redirect("~/EmpDashboard.aspx", false);
                                    return;
                                }

                                else if (Role == "Team Leader" || Role=="Team Manager")
                                {
                                    FormsAuthentication.SetAuthCookie(this.txtusername.Text.Trim(), true);
                                    FormsAuthenticationTicket ticket1 = new FormsAuthenticationTicket(1, this.txtusername.Text.Trim(),
                                        DateTime.Now, DateTime.Now.AddMinutes(480), true, txtusername.Text);
                                    HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket1));
                                    Response.Cookies.Add(cookie1);
                                    Response.Redirect("~/ProjectList.aspx", false);
                                    return;
                                }
                            }
                        }
                    }
                }
                Session["login"] = "";
                errormsg.Visible = true;
                txtpass.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnlogin_Click: " + ex.Message);
            }
        }

        protected void LinkForgot_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ForgotPassword.aspx");
        }
    }
}