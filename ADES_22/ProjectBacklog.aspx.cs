using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;
using System.Net.Mail;
using System.Net;
using ADES_22.Model;
using ADES_22.DBAccess;
using System.Configuration;
using System.Globalization;

namespace ADES_22
{
    public partial class ProjectBacklog : System.Web.UI.Page
    {
        public string PID = string.Empty; public string IssueID = string.Empty;
        public string File_PID = string.Empty; public string Assignee = string.Empty;
        public int Flag = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((String)Session["login"] != "Yes" || (String)Session["login"] == null)
            {
                Response.Redirect("~/Login.aspx", false);
            }

            if (!Page.IsPostBack)
            {
                BindPList();
                BindIssueType();
                BindPriority();

                if (Request.QueryString["ProjectID"] == null)
                {
                    lblProjectTitle.Text = "Customer Site Issues";
                    BindIssueInfo();
                }
                else
                {
                    lblProjectTitle.Text = Request.QueryString["ProjectID"].ToString() + " - Customer Site Issues";
                    txtProjID.Text = Request.QueryString["ProjectID"].ToString();
                    BtnFilterSearch_Click(null, null);
                }

                BindModules();
                BindEnvironment();
                BindAssigneeList();
            }
        }

        public void BindIssueInfo()
        {
            try
            {
                DefectTracker df = new DefectTracker();

                if ((String)Session["Role"] != "Team Leader" && (String)Session["Role"] != "Team Manager" && (String)Session["Role"] != "Admin")
                    df.Assignee = (String)Session["username"];

                df.Param = "View_ExternalBacklogDetails";

                gridprjback.DataSource = DBAccess.DBAccess.GetIssueInfo(df);
                gridprjback.DataBind();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindIssueInfo: " + ex.Message);
            }
        }

        public void BindIssueType()
        {
            try
            {
                ddlIssueType_CList.DataSource = HelperClass.BindIssueType();
                ddlIssueType_CList.DataTextField = "Text";
                ddlIssueType_CList.DataValueField = "Value";
                ddlIssueType_CList.DataBind();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindIssueType: " + ex.Message);
            }
        }

        public void BindPriority()
        {
            try
            {
                ddlPriority.Items.Clear();
                ddlPriority.DataSource = DBAccess.DBAccess.BindStatus("IssuePriority");
                ddlPriority.DataBind();

                ddlPriority.Items.Insert(0, "Select");
                ddlPriority.SelectedIndex = 0;
                ddlPriority.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindPriority: " + ex.Message);
            }
        }

        public void BindProjectList()
        {
            try
            {
                ddlPname.Items.Clear();
                List<string> plist = DBAccess.DBAccess.GetProjectsList();
                ddlPname.DataSource = plist;
                ddlPname.DataBind();

                ddlPname.Items.Insert(0, "Select");
                ddlPname.SelectedIndex = 0;
                ddlPname.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindProjectList: " + ex.Message);
            }
        }

        public void BindModules()
        {
            try
            {
                List<string> MList = DBAccess.DBAccess.GetProjectModules();
                StringBuilder stringBuilder = new StringBuilder();

                for (int i = 0; i < MList.Count; i++)
                    stringBuilder.Append(string.Format("<option value='{0}' />", MList[i]));

                datalist.InnerHtml = stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindModules: " + ex.Message);
            }
        }

        public void BindEnvironment()
        {
            try
            {
                List<string> EList = DBAccess.DBAccess.GetExternalEnvironmentList();
                StringBuilder stringBuilder = new StringBuilder();

                for (int i = 0; i < EList.Count; i++)
                    stringBuilder.Append(string.Format("<option value='{0}' />", EList[i]));

                datalist_Environment.InnerHtml = stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindEnvironment: " + ex.Message);
            }
        }

        public void BindAssigneeList()
        {
            try
            {
                ddlAssignee.Items.Clear();
                List<string> empList = DBAccess.DBAccess.GetAssigneeList();
                ddlAssignee.DataSource = empList;
                ddlAssignee.DataBind();

                ddlAssignee.Items.Insert(0, "Select");
                ddlAssignee.SelectedIndex = 0;
                ddlAssignee.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindAssigneeList: " + ex.Message);
            }
        }

        public void BindFileNames()
        {
            try
            {
                BacklogFiles backlog = new BacklogFiles();
                backlog.PID = File_PID;
                backlog.Module = ddlModule.Value;
                backlog.CName = lblCustName.Text;
                backlog.IssueID = hfIssueID.Value;
                backlog.IssueName = txtIssueName.Text;

                if (hfNewOrEdit.Value == "New")
                    backlog.ReportedBy = (String)Session["username"];
                else
                    backlog.ReportedBy = lblReportedBy.Text;

                backlog.ReporterType = "External";
                backlog.Param = "View_ExternalBacklog_FileDetails";

                gridFiles.DataSource = DBAccess.DBAccess.GetBacklogFileInfo(backlog);
                gridFiles.DataBind();

                if (gridFiles.Rows.Count == 0)
                {
                    GridFileContainer.Visible = false;
                    gridFiles.Visible = false;
                }
                else
                {
                    GridFileContainer.Visible = true;
                    gridFiles.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindFileNames: " + ex.Message);
            }
        }

        public void BindPList()
        {
            try
            {
                List<string> PList = DBAccess.DBAccess.GetProjectIDList();
                StringBuilder stringBuilder = new StringBuilder();

                for (int i = 0; i < PList.Count; i++)
                    stringBuilder.Append(string.Format("<option value='{0}' />", PList[i]));

                flist.InnerHtml = stringBuilder.ToString();

                ddlStatus_CList.DataSource = DBAccess.DBAccess.BindStatus("IssueStatus");
                ddlStatus_CList.DataBind();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindPList: " + ex.Message);
            }
        }

        public void ArrowMark(string s)
        {
            try
            {
                ddlPname.Style.Add("-webkit-appearance", s);
                ddlIssue.Style.Add("-webkit-appearance", s);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("ArrowMark: " + ex.Message);
            }
        }

        public void ClearInsertModal()
        {
            try
            {
                lblCustName.Text = string.Empty;
                ddlModule.Value = string.Empty;
                ddlIssue.SelectedIndex = 0;
                ddlPname.Items.Clear();
                txtDesc.Text = string.Empty;
                txtIssueName.Text = string.Empty;
                ddlPriority.SelectedIndex = 0;
                ddlAssignee.SelectedIndex = 0;
                ddlStatus.SelectedIndex = 0;
                txtChanges.Text = string.Empty;
                txtSteps.Text = string.Empty;
                lblReportedBy.Text = string.Empty;
                attachFile.FileContent.Dispose();
                txtEnvironment.Value = string.Empty;
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("ClearInsertModal: " + ex.Message);
            }
        }

        public void Enabled(Boolean b)
        {
            try
            {
                ddlPname.Enabled = b;
                ddlModule.Disabled = !b;
                ddlIssue.Enabled = b;
                txtIssueName.Enabled = b;
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Enabled: " + ex.Message);
            }
        }

        public void CheckAssigneeNew()
        {
            try
            {
                string userIDs = DBAccess.DBAccess.GetUserIDs(ddlPname.SelectedItem.Text);
                string[] Users = userIDs.Split(',');
                string MailIDs = DBAccess.DBAccess.GetEmpEmailID(Users);

                SendMailNew(MailIDs);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("CheckAssigneeNew: " + ex.Message);
            }
        }

        public void CheckAssigneeEdit()
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                string userIDs = DBAccess.DBAccess.GetUserIDs(ddlPname.SelectedItem.Text);
                string[] Users = userIDs.Split(',');
                string MailIDs = DBAccess.DBAccess.GetEmpEmailID(Users);

                if (hfNewOrEdit.Value == "Edit")
                {
                    string ID = PID.ToString();
                    string Name = ddlPname.SelectedItem.Text;
                    string IID = hfIssueID.Value;
                    string IType = ddlIssue.SelectedValue;
                    string Param = "HistoryView_ExternalBacklogDetails";
                    DataTable dt = DBAccess.DBAccess.GetHistoryIssueInfo(ID, Name, IID, IType, Param);

                    if (dt.Rows.Count > 0)
                    {
                        string Date = "", Reporter = "";

                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                if (dt.Columns[i].ColumnName == "Old_Status" && dt.Columns[i + 1].ColumnName == "New_Status")
                                {
                                    if (dt.Rows[j].ItemArray[i].ToString() != dt.Rows[j].ItemArray[i + 1].ToString())
                                    {
                                        string a = "Status changed from '<b>" + dt.Rows[j].ItemArray[i].ToString() + "'</b> to '<b>" + dt.Rows[j].ItemArray[i + 1].ToString() + "</b>'";
                                        string b = dt.Rows[j].ItemArray[i + 4].ToString();
                                        string c = dt.Rows[j].ItemArray[i + 5].ToString();

                                        sb.AppendLine("<span style=color:blue>" + a + "</span><br/>" + "<span style=color:gray>" + c + "</span><br/>" + b + "<br/><br/>");
                                    }

                                    if (dt.Rows[j].ItemArray[i - 7].ToString() != dt.Rows[j].ItemArray[i - 6].ToString())
                                    {
                                        string a = "";
                                        if (dt.Rows[j].ItemArray[i - 7].ToString() == "")
                                            a = "Issue assigned to '<b>" + dt.Rows[j].ItemArray[i - 6].ToString() + "</b>'";
                                        else
                                            a = "Assignee changed from '<b>" + dt.Rows[j].ItemArray[i - 7].ToString() + "</b>' to '<b>" + dt.Rows[j].ItemArray[i - 6].ToString() + "</b>'";

                                        string b = dt.Rows[j].ItemArray[i + 4].ToString();
                                        string c = dt.Rows[j].ItemArray[i + 5].ToString();

                                        sb.AppendLine("<span style=color:blue>" + a + "</span><br/>" + "<span style=color:gray>" + c + "</span><br/>" + b + "<br/><br/>");
                                    }
                                }

                                if (dt.Columns[i].ColumnName == "ReporteDate")
                                    Date = dt.Rows[j].ItemArray[i].ToString();

                                if (dt.Columns[i].ColumnName == "ReportedBy")
                                    Reporter = dt.Rows[j].ItemArray[i].ToString();
                            }
                        }
                        sb.AppendLine("<span style=color:blue>Issue Created" + "</span><br/>" + "<span style=color:gray>" + Date + "</span><br/>" + Reporter + "<br/><br/>");
                    }
                }
                SendMailEdit(MailIDs, sb.ToString());
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("CheckAssigneeEdit: " + ex.Message);
            }
        }

        public void SendMailNew(string MailIDs)
        {
            WService ws = new WService();
            string AppPass = HelperClass.AppPassword;
            ws.From = HelperClass.Email_ID + ";" + HelperClass.EncodePasswordToBase64(AppPass);
            ws.To = MailIDs;

            StringBuilder sb = new StringBuilder();

            if (ddlAssignee.SelectedIndex != 0)
                sb.AppendLine("A new issue has been raised in ADES Defect Tracker. <br/><br/> <b>Project Name:</b> " + ddlPname.SelectedItem.Text + "<br/> <b>Module:</b> " + ddlModule.Value + "<br/> <b>Issue Type:</b> " + ddlIssue.SelectedItem.Text + "<br/> <b>Issue Name:</b> " + txtIssueName.Text + "<br/> <b>Issue Description:</b> " + txtDesc.Text + "<br/> <b>Priority:</b> " + ddlPriority.SelectedItem.Text + "<br/> <b>Assigned To:</b> " + ddlAssignee.SelectedItem.Text + "<br/> <b>Any changes that caused the issue:</b> " + txtChanges.Text + "<br/> <b>Steps to reproduce:</b> " + txtSteps.Text + "<br/> <b>Reported by:</b> " + lblReportedBy.Text + "<br/> <b>Environment:</b> " + txtEnvironment.Value + "<br/> <b>Customer name:</b> " + lblCustName.Text + "<br/> <b>Status:</b> " + ddlStatus.SelectedItem.Text);
            else
                sb.AppendLine("A new issue has been raised in ADES Defect Tracker. <br/><br/> <b>Project Name:</b> " + ddlPname.SelectedItem.Text + "<br/> <b>Module:</b> " + ddlModule.Value + "<br/> <b>Issue Type:</b> " + ddlIssue.SelectedItem.Text + "<br/> <b>Issue Name:</b> " + txtIssueName.Text + "<br/> <b>Issue Description:</b> " + txtDesc.Text + "<br/> <b>Priority:</b> " + ddlPriority.SelectedItem.Text + "<br/> <b>Assigned To:</b> " + "" + "<br/> <b>Any changes that caused the issue:</b> " + txtChanges.Text + "<br/> <b>Steps to reproduce:</b> " + txtSteps.Text + "<br/> <b>Reported by:</b> " + lblReportedBy.Text + "<br/> <b>Environment:</b> " + txtEnvironment.Value + "<br/> <b>Customer name:</b> " + lblCustName.Text + "<br/> <b>Status:</b> " + ddlStatus.SelectedItem.Text);

            sb.AppendLine("<br/><br/>For more information, log in to <a href='" + ConfigurationManager.AppSettings["ResetPasswordLink"].ToString() + "Login.aspx'>" + ConfigurationManager.AppSettings["ResetPasswordLink"].ToString() + "Login.aspx</a>");

            ws.Subject = "ADES - New Issue Raised";
            ws.MsgBody = sb.ToString();

            DBAccess.DBAccess.InsertEmailDetails(ws);
        }

        public void SendMailEdit(string MailIDs, string History)
        {
            WService ws = new WService();
            string AppPass = HelperClass.AppPassword;
            ws.From = HelperClass.Email_ID + ";" + HelperClass.EncodePasswordToBase64(AppPass);
            ws.To = MailIDs;

            StringBuilder sb = new StringBuilder();

            if (ddlAssignee.SelectedIndex != 0)
                sb.AppendLine("Following issue has been updated in ADES Defect Tracker. <br/><br/> <b>Project Name:</b> " + ddlPname.SelectedItem.Text + "<br/> <b>Module:</b> " + ddlModule.Value + "<br/> <b>Issue Type:</b> " + ddlIssue.SelectedItem.Text + "<br/> <b>Issue Name:</b> " + txtIssueName.Text + "<br/> <b>Issue Description:</b> " + txtDesc.Text + "<br/> <b>Priority:</b> " + ddlPriority.SelectedItem.Text + "<br/> <b>Assigned To:</b> " + ddlAssignee.SelectedItem.Text + "<br/> <b>Any changes that caused the issue:</b> " + txtChanges.Text + "<br/> <b>Steps to reproduce:</b> " + txtSteps.Text + "<br/> <b>Reported by:</b> " + lblReportedBy.Text + "<br/> <b>Environment:</b> " + txtEnvironment.Value + "<br/> <b>Customer name:</b> " + lblCustName.Text + "<br/> <b>Status:</b> " + ddlStatus.SelectedItem.Text);
            else
                sb.AppendLine("Following issue has been updated in ADES Defect Tracker. <br/><br/> <b>Project Name:</b> " + ddlPname.SelectedItem.Text + "<br/> <b>Module:</b> " + ddlModule.Value + "<br/> <b>Issue Type:</b> " + ddlIssue.SelectedItem.Text + "<br/> <b>Issue Name:</b> " + txtIssueName.Text + "<br/> <b>Issue Description:</b> " + txtDesc.Text + "<br/> <b>Priority:</b> " + ddlPriority.SelectedItem.Text + "<br/> <b>Assigned To:</b> " + "" + "<br/> <b>Any changes that caused the issue:</b> " + txtChanges.Text + "<br/> <b>Steps to reproduce:</b> " + txtSteps.Text + "<br/> <b>Reported by:</b> " + lblReportedBy.Text + "<br/> <b>Environment:</b> " + txtEnvironment.Value + "<br/> <b>Customer name:</b> " + lblCustName.Text + "<br/> <b>Status:</b> " + ddlStatus.SelectedItem.Text);

            if (History != null && History != "")
            {
                sb.AppendLine("<br/><br/><b>Issue Activities:</b><br/><br/>");
                sb.AppendLine(History);
            }

            sb.AppendLine("<br/><br/>For more information, log in to <a href='" + ConfigurationManager.AppSettings["ResetPasswordLink"].ToString() + "Login.aspx'>" + ConfigurationManager.AppSettings["ResetPasswordLink"].ToString() + "Login.aspx</a>");

            ws.Subject = "ADES - Issue Updated";
            ws.MsgBody = sb.ToString();

            DBAccess.DBAccess.InsertEmailDetails(ws);
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            try
            {
                ClearInsertModal();
                Enabled(true);

                HelperClass.OpenModal(this, "InfoProjectModal", true);

                ddlStatus.Items.Clear();
                ddlStatus.DataSource = DBAccess.DBAccess.BindStatus("IssueStatus");
                ddlStatus.DataBind();
                ddlStatus.Items.Insert(0, "Select");
                ddlStatus.SelectedIndex = 0;

                lblReportedBy.Text = (String)Session["username"];
                modaltitle.Text = "Add Issue";
                InfoIssue_Yes.Value = "Add";
                hfNewOrEdit.Value = "New";

                BindProjectList();
                BindModules();
                string arrow = "display";
                ArrowMark(arrow);

                InfoIssue_Yes.Visible = true;
                gridFiles.Visible = false;
                GridFileContainer.Visible = false;
                attachFile.Enabled = true;

                ddlStatus.SelectedItem.Text = "Open";
                ddlStatus.Enabled = false;
                ddlPname.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnadd_Click: " + ex.Message);
            }
        }

        protected void ddlPname_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlPname.SelectedItem.Text != "Select")
                {
                    try
                    {
                        string custName = DBAccess.DBAccess.GetCustName(ddlPname.SelectedItem.Text);

                        lblCustName.Text = custName;
                        PID = DBAccess.DBAccess.GetProjectID(ddlPname.SelectedItem.Text);

                        HelperClass.OpenModal(this, "InfoProjectModal", false);
                        ddlPname.Focus();
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog("ddlPname_SelectedIndexChanged: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("ddlPname_SelectedIndexChanged: " + ex.Message);
            }
        }

        protected void btnedit_Click(object sender, EventArgs e)
        {
            try
            {
                HelperClass.OpenModal(this, "InfoProjectModal", true);

                modaltitle.Text = "Edit Issue";
                InfoIssue_Yes.Value = "Update";
                hfNewOrEdit.Value = "Edit";

                ddlStatus.Enabled = true;
                ddlStatus.Items.Clear();
                ddlStatus.DataSource = DBAccess.DBAccess.BindStatus("IssueStatus");
                ddlStatus.DataBind();
                ddlStatus.Items.Insert(0, "Select");
                ddlStatus.SelectedIndex = 0;

                BindProjectList();
                Enabled(false);

                string arrow = "none";
                ArrowMark(arrow);

                int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
                File_PID = (gridprjback.Rows[rowIndex].FindControl("lblPID") as HiddenField).Value;
                ddlModule.Value = (gridprjback.Rows[rowIndex].FindControl("lblModule") as Label).Text;
                txtIssueName.Text = (gridprjback.Rows[rowIndex].FindControl("lblName") as LinkButton).Text;
                hfIssueID.Value = (gridprjback.Rows[rowIndex].FindControl("lblIssueID") as LinkButton).Text;
                lblCustName.Text = (gridprjback.Rows[rowIndex].FindControl("lblCName") as HiddenField).Value;
                txtDesc.Text = (gridprjback.Rows[rowIndex].FindControl("lblSummary") as Label).Text;
                txtChanges.Text = (gridprjback.Rows[rowIndex].FindControl("lblChanges") as HiddenField).Value;
                txtSteps.Text = (gridprjback.Rows[rowIndex].FindControl("lblSteps") as HiddenField).Value;
                lblReportedBy.Text = (gridprjback.Rows[rowIndex].FindControl("hfReportedBy") as HiddenField).Value;
                txtEnvironment.Value = (gridprjback.Rows[rowIndex].FindControl("lblEnvironment") as HiddenField).Value;

                string IssueType = (gridprjback.Rows[rowIndex].FindControl("hfType") as HiddenField).Value;
                if (ddlIssue.Items.FindByValue(IssueType) != null)
                    ddlIssue.SelectedValue = IssueType;

                string Pname = (gridprjback.Rows[rowIndex].FindControl("lblPname") as Label).Text;
                if (ddlPname.Items.FindByValue(Pname) != null)
                    ddlPname.SelectedValue = Pname;

                string Priority = (gridprjback.Rows[rowIndex].FindControl("lblPriority") as Label).Text;
                if (ddlPriority.Items.FindByValue(Priority) != null)
                    ddlPriority.SelectedValue = Priority;

                Assignee = (gridprjback.Rows[rowIndex].FindControl("lblAssignedTo") as Label).Text;
                if (ddlAssignee.Items.FindByValue(Assignee) != null)
                    ddlAssignee.SelectedValue = Assignee;

                string Status = (gridprjback.Rows[rowIndex].FindControl("lblStatus") as Label).Text;
                if (ddlStatus.Items.FindByValue(Status) != null)
                    ddlStatus.SelectedValue = Status;

                ViewState["Steps"] = txtSteps.Text;
                ViewState["Environment"] = txtEnvironment.Value;
                ViewState["Changes"] = txtChanges.Text;
                ViewState["IssueDesc"] = txtDesc.Text;
                ViewState["Priority"] = ddlPriority.SelectedValue;
                ViewState["Assignee"] = ddlAssignee.SelectedValue;
                ViewState["Status"] = ddlStatus.SelectedValue;

                BindFileNames();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnedit_Click: " + ex.Message);
            }
        }

        protected void InfoIssue_Yes_ServerClick(object sender, EventArgs e)
        {
            try
            {
                DefectTracker defectTracker = new DefectTracker();
                PID = DBAccess.DBAccess.GetProjectID(ddlPname.SelectedItem.Text);
                defectTracker.PID = PID;
                defectTracker.PName = ddlPname.SelectedItem.Text;
                defectTracker.CName = lblCustName.Text;
                defectTracker.Module = ddlModule.Value;
                defectTracker.IssueName = txtIssueName.Text;
                defectTracker.IssueDesc = txtDesc.Text;
                defectTracker.IssueType = ddlIssue.SelectedValue;
                defectTracker.Changes = txtChanges.Text;
                defectTracker.Steps = txtSteps.Text;
                defectTracker.ReporterType = "External";
                defectTracker.Environment = txtEnvironment.Value;
                defectTracker.Status = ddlStatus.SelectedItem.Text;

                if (ddlPriority.SelectedItem.Text == "Select")
                    defectTracker.Priority = string.Empty;
                else
                    defectTracker.Priority = ddlPriority.SelectedItem.Text;

                if (ddlAssignee.SelectedItem.Text == "Select")
                    defectTracker.Assignee = string.Empty;
                else
                    defectTracker.Assignee = ddlAssignee.SelectedItem.Text;

                if (hfNewOrEdit.Value == "New")
                {
                    defectTracker.ReportedBy = (String)Session["username"];
                    string xdt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    defectTracker.ReportedDate = Convert.ToDateTime(xdt);
                    defectTracker.Param = "Insert_ExternalBacklogDetails";
                }
                else
                {
                    defectTracker.ReportedBy = lblReportedBy.Text;
                    defectTracker.IssueID = hfIssueID.Value;
                    defectTracker.Param = "Update_ExternalBacklogDetails";
                }

                defectTracker.UpdatedBy = (String)Session["username"];
                string dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                defectTracker.UpdatedTS = Convert.ToDateTime(dt);

                string success = "";
                success = DBAccess.DBAccess.InsertUpdateIssues(defectTracker);

                if (success == "Inserted")
                {
                    CheckAssigneeNew();
                    HelperClass.ClearModal(this);
                    HelperClass.OpenSuccessToaster(this, "Issue details inserted successfully!");
                }
                else if (success == "Updated")
                {
                    if ((String)ViewState["IssueDesc"] != txtDesc.Text || (String)ViewState["Priority"] != ddlPriority.SelectedValue || (String)ViewState["Assignee"] != ddlAssignee.SelectedValue || (String)ViewState["Changes"] != txtChanges.Text || (String)ViewState["Steps"] != txtSteps.Text || (String)ViewState["Environment"] != txtEnvironment.Value || (String)ViewState["Status"] != ddlStatus.SelectedValue)
                        Flag = 1;
                    else
                        Flag = 0;

                    if (Flag == 1)
                    {
                        CheckAssigneeEdit();
                    }

                    HelperClass.ClearModal(this);
                    HelperClass.OpenSuccessToaster(this, "Issue details updated sucessfully!");
                }
                else if (success == "Exist")
                {
                    HelperClass.ClearModal(this);
                    HelperClass.OpenWarningModal(this, "Issue details already exists!");
                }
                else
                {
                    HelperClass.OpenModal(this, "InfoProjectModal", false);
                    HelperClass.OpenErrorModal(this, "Error, while inserting records!");
                    return;
                }

                string documents = hfDocument.Value;
                string documentNames = hfDocumentName.Value;

                success = string.Empty;

                if (documents != string.Empty)
                {
                    string[] document = Regex.Split(documents, ";;;");
                    string[] documentName = Regex.Split(documentNames, ";;;");

                    int i = 0;
                    foreach (string doc in document)
                    {
                        byte[] documentInBytes = null;
                        documentInBytes = System.Convert.FromBase64String(doc.Substring(doc.LastIndexOf(',') + 1));
                        defectTracker = new DefectTracker();
                        defectTracker.PID = DBAccess.DBAccess.GetProjectID(ddlPname.SelectedItem.Text);
                        defectTracker.CName = lblCustName.Text;
                        defectTracker.Module = ddlModule.Value;

                        if (hfNewOrEdit.Value == "New")
                        {
                            if (ddlAssignee.SelectedIndex == 0)
                                defectTracker.IssueID = DBAccess.DBAccess.GetIssueID(defectTracker.PID, ddlPname.SelectedItem.Text, lblCustName.Text, ddlModule.Value, txtIssueName.Text, "External", "", ddlIssue.SelectedValue);
                            else
                                defectTracker.IssueID = DBAccess.DBAccess.GetIssueID(defectTracker.PID, ddlPname.SelectedItem.Text, lblCustName.Text, ddlModule.Value, txtIssueName.Text, "External", ddlAssignee.SelectedItem.Text, ddlIssue.SelectedValue);
                        }
                        else
                            defectTracker.IssueID = hfIssueID.Value;

                        defectTracker.IssueName = txtIssueName.Text;
                        defectTracker.IssueType = ddlIssue.SelectedValue;

                        if (hfNewOrEdit.Value == "New")
                            defectTracker.ReportedBy = (String)Session["username"];
                        else
                            defectTracker.ReportedBy = lblReportedBy.Text;

                        defectTracker.ReporterType = "External";
                        defectTracker.Document = documentInBytes;
                        defectTracker.DocumentName = documentName[i];
                        defectTracker.Param = "Save_ExternalBacklog_FileDetails";

                        success = DBAccess.DBAccess.InsertUpdateIssues(defectTracker);
                        i++;
                    }

                    if (success == "Inserted")
                    {
                        HelperClass.ClearModal(this);
                        HelperClass.OpenSuccessToaster(this, "File details saved successfully!");

                        hfDocument.Value = string.Empty;
                        hfDocumentName.Value = string.Empty;
                    }
                    else
                    {
                        HelperClass.ClearModal(this);
                        HelperClass.OpenErrorModal(this, "Error, while saving file details!");
                    }
                }

                ClearInsertModal();
                BindIssueInfo();
                BtnFilterSearch_Click(null, null);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("InfoIssue_Yes_ServerClick: " + ex.Message);
            }
        }

        protected void btndlt_Click(object sender, EventArgs e)
        {
            try
            {
                int DeleteRowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
                ViewState["DeleteRowIndex"] = DeleteRowIndex;

                string IssueID = (gridprjback.Rows[DeleteRowIndex].FindControl("lblIssueID") as LinkButton).Text;

                ConfirmText.Text = "Are you sure you want to delete Issue- " + IssueID + "?";
                HelperClass.OpenModal(this, "ConfirmationModal", true);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btndlt_Click: " + ex.Message);
            }
        }

        protected void Delete_Yes_ServerClick(object sender, EventArgs e)
        {
            try
            {
                //Delete Issue
                int DeleteRowIndex = (int)ViewState["DeleteRowIndex"];

                DefectTracker defectTracker = new DefectTracker();
                defectTracker.PID = (gridprjback.Rows[DeleteRowIndex].FindControl("lblPID") as HiddenField).Value;
                defectTracker.CName = (gridprjback.Rows[DeleteRowIndex].FindControl("lblCName") as HiddenField).Value;
                defectTracker.Module = (gridprjback.Rows[DeleteRowIndex].FindControl("lblModule") as Label).Text;
                defectTracker.IssueID = (gridprjback.Rows[DeleteRowIndex].FindControl("lblIssueID") as LinkButton).Text;
                defectTracker.IssueName = (gridprjback.Rows[DeleteRowIndex].FindControl("lblName") as LinkButton).Text;
                defectTracker.IssueType = (gridprjback.Rows[DeleteRowIndex].FindControl("hfType") as HiddenField).Value;
                defectTracker.ReporterType = "External";
                defectTracker.ReportedBy = (gridprjback.Rows[DeleteRowIndex].FindControl("hfReportedBy") as HiddenField).Value;
                defectTracker.ReportedDate = Convert.ToDateTime((gridprjback.Rows[DeleteRowIndex].FindControl("lblReportedDate") as Label).Text);
                defectTracker.Assignee = (gridprjback.Rows[DeleteRowIndex].FindControl("lblAssignedTo") as Label).Text;
                defectTracker.Param = "Delete_ExternalBacklogDetails";

                string success = DBAccess.DBAccess.InsertUpdateIssues(defectTracker);
                if (success == "Deleted")
                {
                    ViewState["DeleteRowIndex"] = -1;

                    HelperClass.ClearModal(this);
                    HelperClass.OpenSuccessToaster(this, "Issue deleted successfully!");
                }
                else
                {
                    HelperClass.OpenErrorModal(this, "Error, while deleting record!");
                    return;
                }

                //Delete Issue Files
                DBAccess.DBAccess.DeleteIssueFiles(defectTracker);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Delete_Yes_ServerClick: " + ex.Message);
            }

            BindIssueInfo();
            BtnFilterSearch_Click(null, null);
        }

        protected void link_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                int DeleteRowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
                ViewState["DeleteRowIndex"] = DeleteRowIndex;

                string Fname = (gridFiles.Rows[DeleteRowIndex].FindControl("lblFileName") as Label).Text;
                ConfirmFileText.Text = "Are you sure you want to delete " + Fname + "?";

                HelperClass.OpenModal(this, "ConfirmationFileModal", true);
                HelperClass.OpenModal(this, "InfoProjectModal", false);

            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("link_Delete_Click: " + ex.Message);
            }
        }

        protected void FileDelete_Yes_ServerClick(object sender, EventArgs e)
        {
            try
            {
                int RowIndex = (int)ViewState["DeleteRowIndex"];
                int IDD = Convert.ToInt16((gridFiles.Rows[RowIndex].FindControl("hfIDD") as HiddenField).Value);

                BacklogFiles file = new BacklogFiles();
                file.IDD = IDD;
                file.Param = "Delete_ExternalBacklog_FileDetails";

                string success = DBAccess.DBAccess.DeleteBacklogFile(file);
                if (success == "Deleted")
                {
                    ViewState["DeleteRowIndex"] = -1;
                    HelperClass.OpenSuccessToaster(this, "File deleted successfully!");
                    HelperClass.OpenModal(this, "InfoProjectModal", false);
                }
                else
                {
                    HelperClass.OpenModal(this, "InfoProjectModal", false);
                    HelperClass.OpenErrorModal(this, "Error, while deleting the file!");
                    return;
                }

                BindFileNames();
                BtnFilterSearch_Click(null, null);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("FileDelete_Yes_ServerClick: " + ex.Message);
            }
        }

        protected void link_download_Click(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);

                BacklogFiles backlog = new BacklogFiles();
                backlog.IDD = Convert.ToInt16((gridFiles.Rows[rowIndex].FindControl("hfIDD") as HiddenField).Value);
                backlog.PID = DBAccess.DBAccess.GetProjectID(ddlPname.SelectedItem.Text);
                backlog.CName = lblCustName.Text;
                backlog.Module = ddlModule.Value;
                backlog.IssueID = hfIssueID.Value;
                backlog.IssueName = txtIssueName.Text;
                backlog.IssueType = ddlIssue.SelectedValue;
                backlog.ReporterType = "External";
                backlog.ReportedBy = lblReportedBy.Text;
                backlog.FName = (gridFiles.Rows[rowIndex].FindControl("lblFileName") as Label).Text;
                backlog.Param = "View_ExternalBacklog_FileDetails";

                byte[] bytes = (byte[])(DBAccess.DBAccess.GetBacklogByte(backlog));

                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;
                response.ContentType = "application/force-download";
                response.AddHeader("Content-Disposition", "attachment;filename=" + backlog.FName);
                response.BinaryWrite(bytes);
                response.Flush(); // Sends all currently buffered output to the client.
                response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                HttpContext.Current.ApplicationInstance.CompleteRequest();// Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("link_download_Click: " + ex.Message);
            }
        }

        protected void BtnFilterSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DefectTracker df = new DefectTracker();
                df.FilterSearch = "Yes";
                df.PID = txtProjID.Text;

                string IssueType = "";
                for (int i = 0; i < ddlIssueType_CList.Items.Count; i++)
                {
                    if (ddlIssueType_CList.Items[i].Selected == true)
                    {
                        if (IssueType == "")
                            IssueType = ddlIssueType_CList.Items[i].Value;
                        else
                            IssueType += "," + ddlIssueType_CList.Items[i].Value;
                    }
                }
                df.IssueType = IssueType;

                string Status = string.Empty;
                for (int i = 0; i < ddlStatus_CList.Items.Count; i++)
                {
                    if (ddlStatus_CList.Items[i].Selected == true)
                    {
                        if (Status == string.Empty)
                            Status = ddlStatus_CList.Items[i].Text;
                        else
                            Status += "," + ddlStatus_CList.Items[i].Text;
                    }
                }

                df.Status = Status;
                df.StartDate = txtFromDate.Text;
                df.EndDate = txtToDate.Text;

                if ((String)Session["Role"] != "Team Leader" && (String)Session["Role"] != "Team Manager" && (String)Session["Role"] != "Admin")
                    df.Assignee = (String)Session["username"];

                df.Param = "View_ExternalBacklogDetails";

                gridprjback.DataSource = DBAccess.DBAccess.GetIssueInfo(df);
                gridprjback.DataBind();
                btnadd.Enabled = true;
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BtnFilterSearch_Click: " + ex.Message);
            }
        }

        protected void lblIssueID_Click(object sender, EventArgs e)  //View History
        {
            try
            {
                HelperClass.OpenModal(this, "ViewHistoryModal", true);

                int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
                Modal_PName.Text = (gridprjback.Rows[rowIndex].FindControl("lblPName") as Label).Text;
                Modal_IssueID.Text = (gridprjback.Rows[rowIndex].FindControl("lblIssueID") as LinkButton).Text;

                string ID = (gridprjback.Rows[rowIndex].FindControl("lblPID") as HiddenField).Value;
                string Name = Modal_PName.Text;
                string IID = Modal_IssueID.Text;
                string IType = (gridprjback.Rows[rowIndex].FindControl("hfType") as HiddenField).Value;
                string Param = "HistoryView_ExternalBacklogDetails";
                DataTable dt = DBAccess.DBAccess.GetHistoryIssueInfo(ID, Name, IID, IType, Param);

                if (dt.Rows.Count > 0)
                {
                    string Date = "", Reporter = "";

                    List<History> HList = new List<History>();

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (dt.Columns[i].ColumnName == "Old_Status" && dt.Columns[i + 1].ColumnName == "New_Status")
                            {
                                if (dt.Rows[j].ItemArray[i].ToString() != dt.Rows[j].ItemArray[i + 1].ToString())
                                {
                                    string a = "Status changed from '" + dt.Rows[j].ItemArray[i].ToString() + "' to '" + dt.Rows[j].ItemArray[i + 1].ToString() + "'";
                                    string b = dt.Rows[j].ItemArray[i + 4].ToString();
                                    string c = dt.Rows[j].ItemArray[i + 5].ToString();

                                    History history = new History();
                                    history.Msg = a;
                                    history.ReportedBy = b;
                                    history.RDate = c;

                                    HList.Add(history);
                                }

                                if (dt.Rows[j].ItemArray[i - 7].ToString() != dt.Rows[j].ItemArray[i - 6].ToString())
                                {
                                    string a = "";
                                    if (dt.Rows[j].ItemArray[i - 7].ToString() == "")
                                        a = "Issue assigned to '" + dt.Rows[j].ItemArray[i - 6].ToString() + "'";
                                    else
                                        a = "Assignee changed from '" + dt.Rows[j].ItemArray[i - 7].ToString() + "' to '" + dt.Rows[j].ItemArray[i - 6].ToString() + "'";

                                    string b = dt.Rows[j].ItemArray[i + 4].ToString();
                                    string c = dt.Rows[j].ItemArray[i + 5].ToString();

                                    History history = new History();
                                    history.Msg = a;
                                    history.ReportedBy = b;
                                    history.RDate = c;

                                    HList.Add(history);
                                }
                            }

                            if (dt.Columns[i].ColumnName == "ReporteDate")
                                Date = dt.Rows[j].ItemArray[i].ToString();

                            if (dt.Columns[i].ColumnName == "ReportedBy")
                                Reporter = dt.Rows[j].ItemArray[i].ToString();
                        }
                    }

                    History history1 = new History();
                    history1.Msg = "Issue Created";
                    history1.RDate = Date;
                    history1.ReportedBy = Reporter;

                    HList.Add(history1);

                    HistoryList.DataSource = HList;
                    HistoryList.DataBind();
                }
                else
                {
                    HistoryList.DataSource = "";
                    HistoryList.DataBind();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("lblIssueID_Click: " + ex.Message);
            }
        }

        protected void lblName_Click(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
                LblTaskProjectID.Text = (gridprjback.Rows[rowIndex].FindControl("lblPID") as HiddenField).Value;
                hfProblemID.Value= (gridprjback.Rows[rowIndex].FindControl("lblIssueID") as LinkButton).Text;
                txtYear.Text = DateTime.Now.Year.ToString();
                txtSubTask.Text = (gridprjback.Rows[rowIndex].FindControl("lblName") as LinkButton).Text;

                Subtaskdetails task = new Subtaskdetails();
                task.Projectid = LblTaskProjectID.Text;
                DdlMainTask.DataSource = DBAccess.DBAccess.GetMaintaskValues(task);
                DdlMainTask.DataBind();
                DdlMainTask.Items.Insert(0, "Select");

                if (DdlMainTask.Items.Count == 1)
                {
                    HelperClass.ClearModal(this);
                    HelperClass.OpenWarningToaster(this, "Main task is not created for selected project!");
                    return;
                }

                ddlTaskType.DataSource = DBAccess.DBAccess.BindStatus("TaskType");
                ddlTaskType.DataBind();
                ddlTaskType.Items.Insert(0, "Select");

                ddlDependency.DataSource = DBAccess.DBAccess.BindStatus("Dependency");
                ddlDependency.DataBind();
                ddlDependency.Items.Insert(0, "Select");

                ddlTaskAssignee.Items.Clear();
                string Assignees = DBAccess.DBAccess.GetProjectAssigneeList(LblTaskProjectID.Text);
                string[] AssigneeList = Assignees.Split(',');
                foreach (string Assignee in AssigneeList)
                    ddlTaskAssignee.Items.Add(Assignee);
                ddlTaskAssignee.Items.Insert(0, "Select");

                ddlTaskStatus.DataSource = DBAccess.DBAccess.BindStatus("SubTaskStatus");
                ddlTaskStatus.DataBind();
                ddlTaskStatus.Items.Insert(0, "Select");
                ddlTaskStatus.SelectedIndex = 1;
                ddlTaskStatus.Enabled = false;

                GetWeekList();

                HelperClass.OpenModal(this, "SubTaskModal", true);

                txtYear.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("lblName_Click: " + ex.Message);
            }
        }

        public void GetWeekList()
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            DateTime start = new DateTime((DateTime.Now.Year), 1, 1);
            //DateTime start = Convert.ToDateTime("01-01-2032");
            if ((DateTime.IsLeapYear(start.Year) && start.ToString("dddd") == "Wednesday") || (start.ToString("dddd") == "Thursday"))
            {
                List<int> list1 = new List<int>();
                if (weekNum == 51)
                {
                    for (int i = -1; i < 3; i++)
                    {
                        list1.Add(weekNum + i);
                    }
                    DdlWeek.DataSource = list1;
                    DdlWeek.DataBind();
                    DdlWeek.SelectedValue = weekNum.ToString();
                }
                else if (weekNum == 52)
                {
                    for (int i = -1; i < 2; i++)
                    {
                        list1.Add(weekNum + i);
                    }
                    DdlWeek.DataSource = list1;
                    DdlWeek.DataBind();
                    DdlWeek.SelectedValue = weekNum.ToString();
                }
                else if (weekNum == 53)
                {
                    for (int i = -1; i < 1; i++)
                    {
                        list1.Add(weekNum + i);
                    }
                    DdlWeek.DataSource = list1;
                    DdlWeek.DataBind();
                    DdlWeek.SelectedValue = weekNum.ToString();
                }
                else
                {
                    for (int i = -1; i < 4; i++)
                    {
                        list1.Add(weekNum + i);
                    }
                    DdlWeek.DataSource = list1;
                    DdlWeek.DataBind();
                    DdlWeek.SelectedValue = weekNum.ToString();
                }
            }
            else
            {
                List<int> list = new List<int>();
                if (weekNum == 50)
                {
                    for (int i = -1; i < 3; i++)
                    {
                        list.Add(weekNum + i);
                    }
                    DdlWeek.DataSource = list;
                    DdlWeek.DataBind();
                    DdlWeek.SelectedValue = weekNum.ToString();
                }
                else if (weekNum == 51)
                {
                    for (int i = -1; i < 2; i++)
                    {
                        list.Add(weekNum + i);
                    }
                    DdlWeek.DataSource = list;
                    DdlWeek.DataBind();
                    DdlWeek.SelectedValue = weekNum.ToString();
                }
                else if (weekNum == 52)
                {
                    for (int i = -1; i < 1; i++)
                    {
                        list.Add(weekNum + i);
                    }
                    DdlWeek.DataSource = list;
                    DdlWeek.DataBind();
                    DdlWeek.SelectedValue = weekNum.ToString();
                }
                else
                {
                    for (int i = -1; i < 4; i++)
                    {
                        list.Add(weekNum + i);
                    }
                    DdlWeek.DataSource = list;
                    DdlWeek.DataBind();
                    DdlWeek.SelectedValue = weekNum.ToString();
                }
            }
        }

        protected void AddTask_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Subtaskdetails subtask = new Subtaskdetails();
                subtask.Projectid = LblTaskProjectID.Text;
                subtask.Weekno = DdlWeek.SelectedItem.Text;
                subtask.Year = txtYear.Text;
                subtask.Maintask = DdlMainTask.SelectedItem.Text;

                List<Subtaskdetails> list = new List<Subtaskdetails>();
                list = DBAccess.DBAccess.GetMainTaskDetails(subtask);
                subtask.MainTaskIDD = list[0].MainTaskIDD;
                subtask.MainTaskEstimatedeffort = list[0].MainTaskEstimatedeffort;
                subtask.Subtask = txtSubTask.Text;
                subtask.Tasktype = ddlTaskType.SelectedItem.Text;
                subtask.Estimatedeffortsub = txtTaskEstimatedEffort.Text;
                subtask.Request = ddlTaskRequest.SelectedItem.Value;
                subtask.Dependencies = ddlDependency.SelectedItem.Text;
                subtask.Assignedto = ddlTaskAssignee.SelectedItem.Text;
                subtask.ManualEntryRemark = txtManualEntry.Text;
                subtask.SubtaskStatus = ddlTaskStatus.SelectedItem.Text;
                subtask.DeliveryDate = txtTaskDate.Text;
                subtask.ProblemID = hfProblemID.Value;

                int Result = DBAccess.DBAccess.SaveSubTaskInfo(subtask, "Save_SubTaskDetails");
                if (Result != 0)
                {
                    Session["EmployeeEmailandPassword"] = DBAccess.DBAccess.GetEmployeeemaildetails(subtask.Assignedto);
                    EmpDetails emp = new EmpDetails();
                    if (Session["EmployeeEmailandPassword"] != null)
                    {
                        emp = (EmpDetails)Session["EmployeeEmailandPassword"];
                    }
                    string teamleader = Session["Name"].ToString();
                    SendEmail(emp.EmpName, emp.Email, subtask.Subtask, teamleader);

                    HelperClass.ClearModal(this);
                    HelperClass.OpenSuccessToaster(this, "SubTask Details saved successfully!");
                }
                else
                {
                    HelperClass.OpenModal(this, "SubTaskModal", false);
                    HelperClass.OpenErrorModal(this, "Error, While Saving Records.");
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("AddTask_ServerClick: " + ex.Message);
            }
        }

        public void SendEmail(string empname, string email, string task, string teamleader)
        {
            try
            {
                WService ws = new WService();
                string AppPass = HelperClass.AppPassword;
                ws.From = HelperClass.Email_ID + ";" + HelperClass.EncodePasswordToBase64(AppPass);
                ws.To = email;
                ws.Subject = "Amit - Task Assignment";
                ws.MsgBody = "Hello " + empname + ", <br/><br/>" + "You are assigned with the task <b>" + task + "</b><br/><br/>" + "<a href =\"https://localhost:44313/Login.aspx\"> Click Here To Login</a>" + "<br/>" + "<br/>Thank you.";

                DBAccess.DBAccess.InsertEmailDetails(ws);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("SendEmail: " + ex.Message);
            }
        }

        protected void BtnInfo_Click(object sender, EventArgs e)
        {
            try
            {
                HelperClass.OpenModal(this, "ViewMoreInfoModal", true);
                int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
                lbl_ReportedBy.Text = (gridprjback.Rows[rowIndex].FindControl("hfReportedBy") as HiddenField).Value;
                lbl_CName.Text = (gridprjback.Rows[rowIndex].FindControl("lblCName") as HiddenField).Value;
                lbl_Steps1.Text = (gridprjback.Rows[rowIndex].FindControl("lblSteps") as HiddenField).Value;
                lbl_Changes1.Text = (gridprjback.Rows[rowIndex].FindControl("lblChanges") as HiddenField).Value;
                lbl_Environment.Text = (gridprjback.Rows[rowIndex].FindControl("lblEnvironment") as HiddenField).Value;
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BtnInfo_Click: " + ex.Message);
            }
        }
    }
}