using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.IO;
using ADES_22.Model;
using ADES_22.DBAccess;
using System.Text;

namespace ADES_22
{
    public partial class Emp_Info : System.Web.UI.Page
    {
        public static string empID = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((String)Session["login"] != "Yes" || (String)Session["login"] == null)
            {
                Response.Redirect("~/Login.aspx", false);
            }

            BindEList();

            if (!this.IsPostBack)
            {
                GetEmpDetails();
                GetDepartmentList();
                GetRole();
                GetDepartmentHeads();
                BindRegion();
                BindFilterDeptNames();
            }
        }
        
        public void GetEmpDetails()
        {
            try
            {
                EmpDetails emp = new EmpDetails();
                emp.Param = "View";
                gvitems.DataSource = DBAccess.DBAccess.GetEmpInformation(emp);
                gvitems.DataBind();
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("GetEmpDetails: " + ex);
            }
        }

        public void GetDepartmentList()
        {
            try
            {
                ddlDept.DataSource = DBAccess.DBAccess.BindStatus("EmpDept");
                ddlDept.DataBind();
                ddlDept.Items.Insert(0, "Select");
                ddlDept.SelectedIndex = 0;
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("GetDepartmentList: " + ex.Message);
            }
        }

        public void GetRole()
        {
            try
            {
                ddlrole.DataSource = DBAccess.DBAccess.BindStatus("EmpRole");
                ddlrole.DataBind();
                ddlrole.Items.Insert(0, "Select");
                ddlrole.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetRole: " + ex.Message);
            }
        }

        public void GetDepartmentHeads()
        {
            try
            {
                DdlReportingTo.Items.Clear();
                DdlReportingTo.DataSource = DBAccess.DBAccess.GetDepartmentHeads();
                DdlReportingTo.DataBind();
                DdlReportingTo.Items.Insert(0, "Select");
                DdlReportingTo.SelectedIndex = 0;
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("GetDepartmentHeads: " + ex.Message);
            }
        }

        public void BindRegion()
        {
            try
            {
                DdlRegion.DataSource = DBAccess.DBAccess.BindCRegion();
                DdlRegion.DataBind();
                DdlRegion.Items.Insert(0, "Select");
                DdlRegion.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetDepartmentHeads: " + ex.Message);
            }
        }

        public void BindEList()
        {
            try
            {
                List<string> MList = DBAccess.DBAccess.GetEmpIDList();
                StringBuilder stringBuilder = new StringBuilder();

                for (int i = 0; i < MList.Count; i++)
                    stringBuilder.Append(string.Format("<option value='{0}' />", MList[i]));

                EList.InnerHtml = stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindEList: " + ex.Message);
            }
        }

        public void BindFilterDeptNames()
        {
            try
            {
                ddlFilterDept.DataSource = DBAccess.DBAccess.BindStatus("EmpDept");
                ddlFilterDept.DataBind();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindFilterDeptNames: " + ex.Message);
            }
        }

        public void Clear()
        {
            try
            {
                txtEmpID.Text = string.Empty;
                txtEmpName.Text = string.Empty;
                txtEmail.Text = string.Empty;
                txtEmpPass.Attributes.Add("value", "");
                ddlrole.SelectedIndex = 0;
                ddlDept.SelectedIndex = 0;
                DdlReportingTo.SelectedIndex = 0;
                DdlRegion.SelectedIndex = 0;
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("Clear: " + ex.Message);
            }
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                GetDepartmentHeads();
                btnedit.Value = "Add";
                txtEmpID.Enabled = true;
                hfNeworEdit.Value = "New";
                modaltitle.Text = "Add Employee";

                HelperClass.OpenModal(this, "EditModal", true);

                txtEmpID.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnadd_Click: " + ex.Message);
            }
        }

        protected void btnedit_ServerClick(object sender, EventArgs e) // Save and Update
        {
            try
            {
                if (hfNeworEdit.Value == "New")
                {
                    int cnt = DBAccess.DBAccess.ExistsUserID(txtEmpID.Text);
                    if (cnt == 1)
                    {
                        HelperClass.OpenWarningToaster(this, "Such Employee ID already exists!");
                        HelperClass.OpenModal(this, "EditModal", false);
                        txtEmpID.Focus();
                        return;
                    }
                }
                
                EmpDetails empInfo = new EmpDetails();
                empInfo.EmpID = txtEmpID.Text;
                empInfo.EmpName = txtEmpName.Text;
                empInfo.Param = "Save";

                if (ddlDept.SelectedIndex == 0)
                    empInfo.Dept = string.Empty;
                else
                    empInfo.Dept = ddlDept.SelectedItem.Text;

                if (ddlrole.SelectedIndex == 0)
                    empInfo.Role = string.Empty;
                else
                    empInfo.Role = ddlrole.SelectedItem.Text;

                if (DdlReportingTo.SelectedIndex == 0)
                    empInfo.ReportingTo = string.Empty;
                else
                    empInfo.ReportingTo = DdlReportingTo.SelectedItem.Text;

                if (DdlRegion.SelectedIndex == 0)
                    empInfo.Region = string.Empty;
                else
                    empInfo.Region = DdlRegion.SelectedItem.Text;

                empInfo.Email = txtEmail.Text;
                empInfo.Password = HelperClass.EncodePasswordToBase64(txtEmpPass.Text);

                string success = string.Empty;
                success = DBAccess.DBAccess.InsertUpdateEmp(empInfo);

                if (success == "Inserted")
                {
                    HelperClass.ClearModal(this);
                    HelperClass.OpenSuccessToaster(this, "Employee details inserted successfully!");
                }
                else if (success == "Updated")
                {
                    HelperClass.ClearModal(this);
                    HelperClass.OpenSuccessToaster(this, "Employee details updated successfully!");
                }
                else
                {
                    HelperClass.OpenModal(this, "EditModal", false);
                    HelperClass.OpenErrorModal(this, "Error, while inserting records.");
                    return;
                }

                if (hfNeworEdit.Value == "New")
                {
                    SendEmail(txtEmpID.Text, txtEmpName.Text, txtEmail.Text, txtEmpPass.Text);
                }
                else
                {
                    if ((String)ViewState["EmailID"] != txtEmail.Text || (String)ViewState["EPassword"] != txtEmpPass.Text)
                        SendEmail(txtEmpID.Text, txtEmpName.Text, txtEmail.Text, txtEmpPass.Text);
                }            
                
                GetEmpDetails();
                Clear();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnedit_ServerClick: " + ex.Message);
            }
        }

        public void SendEmail(string empID, string empname, string email, string password)
        {
            try
            {
                WService ws = new WService();
                string AppPass = HelperClass.AppPassword;
                ws.From = HelperClass.Email_ID + ";" + HelperClass.EncodePasswordToBase64(AppPass);
                ws.To = email;
                ws.Subject = "ADES Login Details";
                ws.MsgBody = "Hello " + empname + ", <br/><br/> Your login credentials are as follows: <br/><br/> User ID: " + empID + "<br/> Password: " + password + "<br/><br/>Have a great day!"; /*<br/><br>Regards, <br/>AmiT ";*/

                DBAccess.DBAccess.InsertEmailDetails(ws);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("SendEmail: " + ex.Message);
            }
        }

        protected void gvitems_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                hfNeworEdit.Value = "Edit";
                modaltitle.Text = "Edit Employee";
                GetDepartmentHeads();

                GridViewRow gvRow = gvitems.SelectedRow;

                Label lblId = gvRow.FindControl("lblempID") as Label;
                txtEmpID.Text = lblId.Text;

                Label lblName = gvRow.FindControl("lblempName") as Label;
                txtEmpName.Text = lblName.Text;

                Label lblDept = gvRow.FindControl("lblDept") as Label;
                ddlDept.Text = lblDept.Text;

                Label lblRole = gvRow.FindControl("lblrole") as Label;
                ddlrole.Text = lblRole.Text;

                Label lblmail = gvRow.FindControl("lblemail") as Label;
                txtEmail.Text = lblmail.Text;

                Label lblpwd = gvRow.FindControl("HiddenField1") as Label;
                txtEmpPass.Attributes.Add("value", HelperClass.DecodeFrom64(((HiddenField)gvRow.FindControl("HiddenField1")).Value));

                Label lblReporting = gvRow.FindControl("lblReportingTo") as Label;
                if (DdlReportingTo.Items.FindByValue(lblReporting.Text) != null)
                    DdlReportingTo.SelectedValue = lblReporting.Text;
                else
                    DdlReportingTo.SelectedIndex = 0;

                Label lblReg = gvRow.FindControl("lblRegion") as Label;
                if (DdlRegion.Items.FindByValue(lblReg.Text) != null)
                    DdlRegion.SelectedValue = lblReg.Text;
                else
                    DdlRegion.SelectedIndex = 0;

                HelperClass.OpenModal(this, "EditModal", true);

                ViewState["EmailID"] = txtEmail.Text;
                ViewState["EPassword"] = HelperClass.DecodeFrom64(((HiddenField)gvRow.FindControl("HiddenField1")).Value);

                btnedit.Value = "Update";
                txtEmpID.Enabled = false;
                txtEmpName.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("gvitems_SelectedIndexChanged: " + ex.Message);
            }
        }

        protected void gvitems_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                empID = (gvitems.Rows[e.RowIndex].FindControl("lblempID") as Label).Text;

                ConfirmText.Text = "Are you sure you want to delete Employee- " + empID + "?";
                HelperClass.OpenModal(this, "myConfirmationModal", true);
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("gvitems_RowDeleting: " + ex.Message);
            }
        }

        protected void saveConfirmYes_ServerClick(object sender, EventArgs e)
        {
            try
            {
                EmpDetails empInfo = new EmpDetails();
                empInfo.EmpID = empID;
                empInfo.Param = "Delete";

                DBAccess.DBAccess.InsertUpdateEmp(empInfo);
                gvitems.EditIndex = -1;

                HelperClass.ClearModal(this);
                HelperClass.OpenSuccessToaster(this, "Record deleted successfully!");

                GetEmpDetails();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("saveConfirmYes_ServerClick: " + ex.Message);
            }
        }

        protected void BtnFilterSearch_Click(object sender, EventArgs e)
        {
            try
            {
                EmpDetails emp = new EmpDetails();
                emp.EmpID = txtFilterEmpID.Text;

                string FilterDept = "";
                for (int i = 0; i < ddlFilterDept.Items.Count; i++)
                {
                    if (ddlFilterDept.Items[i].Selected == true)
                    {
                        if (FilterDept == "")
                            FilterDept = ddlFilterDept.Items[i].Value;
                        else
                            FilterDept += "," + ddlFilterDept.Items[i].Value;
                    }
                }

                emp.Dept = FilterDept;
                emp.Param = "View";

                gvitems.DataSource = DBAccess.DBAccess.GetEmpInformation(emp);
                gvitems.DataBind();
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("BtnFilterSearch_Click: " + ex.Message);
            }
        }
    }
}