using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using ADES_22.Model;
using ADES_22.DBAccess;

namespace ADES_22
{
    public partial class EmpDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((String)Session["login"] != "Yes" || (String)Session["login"] == null)
            {
                Response.Redirect("~/Login.aspx", false);
            }

            if (!Page.IsPostBack)
            {
                BindPrjIDList();
                BindPriority();
                BindIssueType();
                BindStatus("IssueStatus");
                BindDashboardDetails();
            }
        }

        public void BindDashboardDetails()
        {
            try
            {
                DefectTracker df = new DefectTracker();
                df.Assignee = (String)Session["username"];
                df.Param = "TasksAssignedToMe";

                griddash.DataSource = DBAccess.DBAccess.GetEmpDashboardInfo(df);
                griddash.DataBind();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindDashboardDetails: " + ex.Message);
            }
        }

        public void BindPrjIDList()
        {
            try
            {
                List<string> PList = DBAccess.DBAccess.GetProjectIDList();
                StringBuilder stringBuilder = new StringBuilder();

                for (int i = 0; i < PList.Count; i++)
                {
                    stringBuilder.Append(string.Format("<option value='{0}'  />", PList[i]));
                }

                filter_IDList.InnerHtml = stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindPList: " + ex.Message);
            }
        }

        public void BindStatus(string Category)
        {
            try
            {
                ddlStatus_CList.DataSource = DBAccess.DBAccess.BindStatus(Category);
                ddlStatus_CList.DataBind();
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("BindStatus: " + ex.Message);
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
                ddlPriority_CList.DataSource = DBAccess.DBAccess.BindStatus("IssuePriority");
                ddlPriority_CList.DataBind();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindPriority: " + ex.Message);
            }
        }

        protected void BtnFilterSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DefectTracker df = new DefectTracker();
                df.PID = txtPrjID.Text;
                df.Assignee = (String)Session["username"];

                string IssueType = "";
                for (int i = 0; i < ddlIssueType_CList.Items.Count; i++)
                {
                    if (ddlIssueType_CList.Items[i].Selected == true)
                    {
                        if (IssueType == "")
                            IssueType = "'" + ddlIssueType_CList.Items[i].Value + "'";
                        else
                            IssueType += ",'" + ddlIssueType_CList.Items[i].Value + "'";
                    }
                }
                df.IssueType = IssueType;

                string priority = "";
                for(int i=0;i<ddlPriority_CList.Items.Count;i++)
                {
                    if (ddlPriority_CList.Items[i].Selected == true)
                    {
                        if (priority == "")
                            priority = "'" + ddlPriority_CList.Items[i].Text + "'";
                        else
                            priority += ",'" + ddlPriority_CList.Items[i].Text + "'";
                    }
                }
                df.Priority= priority;

                string status = "";
                for (int i = 0; i < ddlStatus_CList.Items.Count; i++)
                {
                    if (ddlStatus_CList.Items[i].Selected == true)
                    {
                        if (status == "")
                            status = "'" + ddlStatus_CList.Items[i].Text + "'";
                        else
                            status += ",'" + ddlStatus_CList.Items[i].Text + "'";
                    }
                }
                df.Status = status;
                df.Param = "TasksAssignedToMe";

                griddash.DataSource= DBAccess.DBAccess.GetEmpDashboardInfo(df);
                griddash.DataBind();
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("BtnFilterSearch_Click: " + ex.Message);
            }
        }

        protected void lnk_IssueID_Click(object sender, EventArgs e)
        {
            try
            {
                HelperClass.OpenModal(this, "ViewHistoryModal", true);

                int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
                Modal_PName.Text = (griddash.Rows[rowIndex].FindControl("lblPName") as Label).Text;
                Modal_IssueID.Text = (griddash.Rows[rowIndex].FindControl("lnk_IssueID") as LinkButton).Text;

                string ID = (griddash.Rows[rowIndex].FindControl("lblPID") as Label).Text;
                string Name = Modal_PName.Text;
                string IID = Modal_IssueID.Text;
                string IType = (griddash.Rows[rowIndex].FindControl("hfType") as HiddenField).Value;
                string ReporterType = (griddash.Rows[rowIndex].FindControl("lblRType") as Label).Text;
                string Param = "HistoryView_" + ReporterType + "BacklogDetails";
                DataTable dt = DBAccess.DBAccess.GetHistoryIssueInfo(ID, Name, IID, IType, Param);

                if (ReporterType == "Internal")
                {
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
                                        string b = dt.Rows[j].ItemArray[i + 3].ToString();
                                        string c = dt.Rows[j].ItemArray[i + 4].ToString();

                                        History history = new History();
                                        history.Msg = a;
                                        history.ReportedBy = b;
                                        history.RDate = c;

                                        HList.Add(history);
                                    }

                                    if (dt.Rows[j].ItemArray[i - 6].ToString() != dt.Rows[j].ItemArray[i - 5].ToString())
                                    {
                                        string a = "";
                                        if (dt.Rows[j].ItemArray[i - 6].ToString() == "")
                                            a = "Issue assigned to '" + dt.Rows[j].ItemArray[i - 5].ToString() + "'";
                                        else
                                            a = "Assignee changed from '" + dt.Rows[j].ItemArray[i - 6].ToString() + "' to '" + dt.Rows[j].ItemArray[i - 5].ToString() + "'";

                                        string b = dt.Rows[j].ItemArray[i + 3].ToString();
                                        string c = dt.Rows[j].ItemArray[i + 4].ToString();

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
                else
                {
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
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("lnk_IssueID_Click: " + ex.Message);
            }
        }

        protected void Link_Attachments_Click(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
                ViewState["rowIndex"] = rowIndex;
                BacklogFiles backlog = new BacklogFiles();
                backlog.PID = (griddash.Rows[rowIndex].FindControl("lblPID") as Label).Text;
                backlog.CName = (griddash.Rows[rowIndex].FindControl("lblCName") as Label).Text;
                backlog.Module = (griddash.Rows[rowIndex].FindControl("hfModule") as HiddenField).Value;
                backlog.IssueID = (griddash.Rows[rowIndex].FindControl("lnk_IssueID") as LinkButton).Text;
                backlog.IssueName = (griddash.Rows[rowIndex].FindControl("lblIssueName") as Label).Text;
                backlog.ReportedBy = (griddash.Rows[rowIndex].FindControl("lblReportedBy") as Label).Text;
                backlog.ReporterType = (griddash.Rows[rowIndex].FindControl("lblRType") as Label).Text;
                backlog.Param = "View_"+ backlog.ReporterType+"Backlog_FileDetails";

                List<BacklogFiles> li = DBAccess.DBAccess.GetBacklogFileInfo(backlog);
                if (li.Count > 0)
                {
                    GridAttachments.DataSource = li;
                    GridAttachments.DataBind();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SetPosition", "SetPosition("+ hfXValue.Value + "," + hfYValue.Value + ");",true);
                }
                else
                    HelperClass.OpenWarningToaster(this, "No attachments uploaded!");
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("Link_Attachments_Click: " + ex.Message);
            }
        }

        protected void link_filename_Click(object sender, EventArgs e)
        {
            try
            {
                int row = (int)ViewState["rowIndex"];
                int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);

                BacklogFiles backlog = new BacklogFiles();
                backlog.IDD = Convert.ToInt16((GridAttachments.Rows[rowIndex].FindControl("hfIDD") as HiddenField).Value);
                backlog.PID = (griddash.Rows[row].FindControl("lblPID") as Label).Text;
                backlog.CName = (griddash.Rows[row].FindControl("lblCName") as Label).Text;
                backlog.Module = (griddash.Rows[row].FindControl("hfModule") as HiddenField).Value;
                backlog.IssueID = (griddash.Rows[row].FindControl("lnk_IssueID") as LinkButton).Text;
                backlog.IssueName = (griddash.Rows[row].FindControl("lblIssueName") as Label).Text;
                backlog.IssueType = (griddash.Rows[row].FindControl("hfType") as HiddenField).Value;
                backlog.ReporterType = (griddash.Rows[row].FindControl("lblRType") as Label).Text;
                backlog.ReportedBy = (griddash.Rows[row].FindControl("lblReportedBy") as Label).Text;
                backlog.FName = (GridAttachments.Rows[rowIndex].FindControl("link_filename") as LinkButton).Text;

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

                ScriptManager.RegisterStartupScript(this, this.GetType(), "SetPosition", "SetPosition(" + hfXValue.Value + "," + hfYValue.Value + ");", true);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("link_filename_Click: " + ex.Message);
            }
        }
    }
}