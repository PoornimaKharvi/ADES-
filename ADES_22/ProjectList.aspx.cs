using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using ADES_22.Model;
using ADES_22.DBAccess;
using System.Configuration;
using iTextSharp.xmp.impl;

namespace ADES_22
{
    public partial class ProjectList : System.Web.UI.Page
    {
        public static string PID = "";
        public int flag = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((String)Session["login"] != "Yes" || (String)Session["login"] == null)
            {
                Response.Redirect("~/Login.aspx", false);
            }

            if (!Page.IsPostBack)
            {
                BindProjectOwner();
                BindProjectStatus("ProjectStatus");
                BindPrjIDList();
                BindCustNames();
                BindUsers();
                BindMainTask();
                BindCustomerNames();
                BindProjectInfo();
            }
        }

        public void BindProjectInfo()
        {
            try
            {
                DefectTracker df = new DefectTracker();
                df.Param = "View";

                gridprj.Visible = true;
                gridprj.DataSource = DBAccess.DBAccess.GetProjectInfo(df);
                gridprj.DataBind();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindProjectInfo: " + ex.Message);
            }
        }

        public void BindPrjIDList()
        {
            try
            {
                List<string> PList = DBAccess.DBAccess.GetProjectIDList();
                StringBuilder stringBuilder = new StringBuilder();
                  
                for (int i = 0; i < PList.Count; i++)
                    stringBuilder.Append(string.Format("<option value='{0}' />", PList[i]));

                filter_IDlist.InnerHtml = stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindPrjIDList: " + ex.Message);
            }
        }

        public void BindCustNames()
        {
            try
            {
                List<string> PList = DBAccess.DBAccess.GetCNames();
                StringBuilder stringBuilder = new StringBuilder();

                for (int i = 0; i < PList.Count; i++)
                    stringBuilder.Append(string.Format("<option value='{0}' />", PList[i]));

                filter_CNamelist.InnerHtml = stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindCustNames: " + ex.Message);
            }
        }

        public void BindUsers()
        {
            try
            {
                ddlUsers.DataSource = DBAccess.DBAccess.GetUsersList();
                ddlUsers.DataBind();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindUsers: " + ex.Message);
            }
        }

        public void BindMainTask()
        {
            try
            {
                DDLMainTaskSelector.Items.Clear();
                DDLMainTaskSelector.DataSource = DBAccess.DBAccess.BindStatus("TaskType");
                DDLMainTaskSelector.DataBind();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindUsers: " + ex.Message);
            }
        }

        public void BindFileNames()
        {
            try
            {
                List<Files> File = new List<Files>();
                File = DBAccess.DBAccess.GetFileInfo("ViewProjectFileDetails", txtprjID.Text);

                gridFiles.Visible = true;
                gridFiles.DataSource = File;
                gridFiles.DataBind();

                if (gridFiles.Rows.Count == 0)
                {
                    GridFileContainer.Visible = false;
                    gridFiles.Visible = false;
                }
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("BindFileNames: " + ex.Message);
            }
        }

        public void BindProjectStatus(string category)
        { 
            try
            {
                ddlProjectStatus.DataSource = DBAccess.DBAccess.BindStatus(category);
                ddlProjectStatus.DataBind();
                ddlProjectStatus.Items.Insert(0, "Select");
                ddlProjectStatus.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindIssueType: " + ex.Message);
            }
        }

        public void BindProjectOwner()
        {
            try
            {
                ddlProjectOwner.DataSource = DBAccess.DBAccess.GetApplicationTeamNames();
                ddlProjectOwner.DataBind();
                ddlProjectOwner.Items.Insert(0, "Select");
                ddlProjectOwner.SelectedIndex = 0;
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("BindProjectOwner: " + ex.Message);
            }
        }

        public void BindCustomerNames()
        {
            try
            {
                List<string> PList = DBAccess.DBAccess.BindCustomerNames();
                StringBuilder stringBuilder = new StringBuilder();

                for (int i = 0; i < PList.Count; i++)
                    stringBuilder.Append(string.Format("<option value='{0}' />", PList[i]));

                datalist_Customer.InnerHtml = stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindCustNames: " + ex.Message);
            }
        }

        public void UploadFile(string PrjID, string documents, string documentNames, string FileType)
        {
            try
            {
                DefectTracker defectTracker = new DefectTracker();
                string success = string.Empty;

                if (documents != "")
                {
                    string[] document = Regex.Split(documents, ";;;");
                    string[] documentName = Regex.Split(documentNames, ";;;");
                    
                    int i = 0;
                    foreach (string doc in document)
                    {
                        byte[] documentInBytes = null;
                        documentInBytes = System.Convert.FromBase64String(doc.Substring(doc.LastIndexOf(',') + 1));
                        defectTracker.PID = PrjID;
                        defectTracker.Document = documentInBytes;
                        defectTracker.DocumentName = documentName[i];
                        defectTracker.FileType = FileType;
                        defectTracker.Param = "SaveProjectFileDetails";
                        success = DBAccess.DBAccess.InsertUpdateProject(defectTracker);
                        i++;
                    }

                    if (success == "Inserted")
                    {
                        HelperClass.OpenSuccessToaster(this, "File details saved successfully!");

                        documents = string.Empty;
                        documentNames = string.Empty;
                        FileType = string.Empty;
                    }
                    else
                    {
                        HelperClass.OpenModal(this, "AddProjectModal", false);
                        HelperClass.OpenErrorModal(this, "Error, while saving file details!");
                    }
                }
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("UploadFile: " + ex.Message);
            }
        }

        protected void lblID_Click(object sender, EventArgs e)
        {
            try
            {
                string dept = (String)Session["Dept"];
                string role = (String)Session["Role"];

                int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
                string PName = (gridprj.Rows[rowIndex].FindControl("lblPname") as Label).Text;
                PID = DBAccess.DBAccess.GetProjectID(PName);

                if ((dept == "QA Team" || dept == "Development Team") && (role == "Team Member"))
                    Response.Redirect(string.Format("~/Internal_Issue.aspx?ProjectID={0}", PID), false);

                else if((dept == "Application Team") && (role == "Team Member"))
                    Response.Redirect(String.Format("~/ProjectBacklog.aspx?ProjectID={0}", PID), false);

                else if((dept == "Application Team" || dept == "QA Team") && (role == "Team Leader" || role=="Team Manager"))
                    Response.Redirect(String.Format("~/ProjectBacklog.aspx?ProjectID={0}", PID), false);

                else if((dept == "Development Team") && (role == "Team Leader" || role=="Team Manager"))
                    HelperClass.OpenModal(this, "PageRedirectModal", true);

                else
                    HelperClass.OpenModal(this, "PageRedirectModal", true);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("lblID_Click: " + ex.Message);
            }
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            try
            {
                txtprjID.Focus();
                txtprjID.Text = string.Empty;
                txtprjname.Text = string.Empty;
                ddlUsers.ClearSelection();
                DDLMainTaskSelector.ClearSelection();
                txtcname.Value = string.Empty;
                txtDeliveryDate.Text = String.Empty;
                txtEstimatedEffort.Text=String.Empty;
                ddlProjectOwner.SelectedIndex = 0;
                ddlProjectStatus.SelectedIndex=1;
                ddlProjectStatus.Enabled = false;
                txtCreatedBy.Text = (String)Session["username"];

                modaltitle.Text = "Add Project";
                AddProject_Yes.Value = "Add";
                hfNewOrEdit.Value = "New";

                txtprjID.Enabled = true;
                gridFiles.Visible = false;
                GridFileContainer.Visible = false;
                DDLMainTaskSelector.Attributes.Remove("disabled");

                HelperClass.OpenModal(this, "AddProjectModal", true);

                txtprjID.Focus();
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("btnadd_Click: " + ex.Message);
            }
        }

        protected void btnedit_Click(object sender, EventArgs e)
        {
            try
            {
                modaltitle.Text = "Edit Project";
                AddProject_Yes.Value = "Update";
                hfNewOrEdit.Value = "Edit";
                ddlUsers.ClearSelection();
                ddlProjectStatus.Enabled = true;
                txtprjID.Enabled = false;
                gridFiles.Visible = true;
                GridFileContainer.Visible = true;
                DDLMainTaskSelector.Attributes.Add("disabled", "true");

                HelperClass.OpenModal(this, "AddProjectModal", true);

                int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
                txtprjID.Text = (gridprj.Rows[rowIndex].FindControl("lblID") as LinkButton).Text;
                txtprjname.Text = (gridprj.Rows[rowIndex].FindControl("lblPname") as Label).Text;
                txtcname.Value = (gridprj.Rows[rowIndex].FindControl("lblCname") as Label).Text;
                txtEstimatedEffort.Text = (gridprj.Rows[rowIndex].FindControl("lblEstimatedEffort") as Label).Text;
                DateTime dt = Convert.ToDateTime((gridprj.Rows[rowIndex].FindControl("hfDate") as HiddenField).Value);
                var date = dt.ToString("yyyy-MM-dd");
                txtDeliveryDate.Text = date;
                txtCreatedBy.Text = (gridprj.Rows[rowIndex].FindControl("lblCreatedBy") as Label).Text;

                string ProjectOwner = (gridprj.Rows[rowIndex].FindControl("lblProjectOwner") as Label).Text;
                if (ddlProjectOwner.Items.FindByValue(ProjectOwner) != null)
                    ddlProjectOwner.SelectedValue = ProjectOwner;

                string Status = (gridprj.Rows[rowIndex].FindControl("lblStatus") as Label).Text;
                if (ddlProjectStatus.Items.FindByValue(Status) != null)
                    ddlProjectStatus.SelectedValue = Status;

                string Users = (gridprj.Rows[rowIndex].FindControl("lblUsers") as Label).Text;
                string[] UserID = Users.Split(',');
                for (int i = 0; i < UserID.Length; i++)
                {
                    if (ddlUsers.Items.FindByText(UserID[i]) != null)
                        ddlUsers.Items.FindByText(UserID[i]).Selected = true;
                }

                ViewState["ProjectName"] = txtprjname.Text;
                ViewState["CustomerName"] = txtcname.Value;
                ViewState["AssignedUsers"] = Users.ToString();
                ViewState["EstimatedEffort"] = txtEstimatedEffort.Text;
                ViewState["DeliveryDate"] = txtDeliveryDate.Text;
                ViewState["ProjectOwner"] = ProjectOwner;
                ViewState["ProjectStatus"] = Status;

                //Bind filenames to grid
                BindFileNames();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnedit_Click: " + ex.Message);
            }
        }

        protected void AddProject_Yes_ServerClick(object sender, EventArgs e)
        {
            try
            {
                //EMail Code 

                int count = 0;

                string Mails = "", MailID = "", Users = "";
                for (int i = 0; i < ddlUsers.Items.Count; i++)
                {
                    if (ddlUsers.Items[i].Selected == true)
                    {
                        count = 1;
                        if (Users == "")
                            Users = ddlUsers.Items[i].Text;
                        else
                            Users += "," + ddlUsers.Items[i].Text;

                        MailID = DBAccess.DBAccess.GetEmailID(ddlUsers.Items[i].Text);
                        if (Mails == "")
                            Mails = MailID;
                        else
                            Mails += "," + MailID;
                    }
                }

                if (count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Warning" + 1, "<script>WarningToastr('Users must be assigned!')</script>", false);
                    HelperClass.OpenModal(this, "AddProjectModal", false);
                    return;
                }
                else
                {
                    string UserIDs = "";
                    for (int i = 0; i < ddlUsers.Items.Count; i++)
                    {
                        if (ddlUsers.Items[i].Selected == true)
                        {
                            if (UserIDs == "")
                                UserIDs = ddlUsers.Items[i].Text;
                            else
                                UserIDs += "," + ddlUsers.Items[i].Text;
                        }
                    }

                    //Details Upload

                    DateTime dt = Convert.ToDateTime(txtDeliveryDate.Text);
                    dt = Convert.ToDateTime(dt.ToString("yyyy-MM-dd"));
                    dt = dt.Date;
                    string status = ddlProjectStatus.SelectedItem.Text;

                    string CreatedBy = String.Empty;
                    if (hfNewOrEdit.Value == "New")
                        CreatedBy = (String)Session["username"];
                    else
                        CreatedBy = txtCreatedBy.Text;

                    DefectTracker defectTracker = new DefectTracker
                    {
                        PID = txtprjID.Text,
                        PName = txtprjname.Text,
                        CName = txtcname.Value,
                        Users = UserIDs,
                        ProjectCreatedBy = CreatedBy,
                        DeliveryDate = dt,
                        ProjectOwner = ddlProjectOwner.SelectedItem.Text,
                        EstimatedEffort = txtEstimatedEffort.Text,
                        ProjectStatus = ddlProjectStatus.SelectedItem.Text,
                        Param = "Save"
                    };

                    string success = string.Empty;
                    success = DBAccess.DBAccess.InsertUpdateProject(defectTracker);

                    if (success == "Inserted")
                    {
                        SendEmailNew(Mails, txtprjID.Text, txtprjname.Text, txtcname.Value, txtDeliveryDate.Text, txtEstimatedEffort.Text, ddlProjectOwner.SelectedItem.Text, ddlProjectStatus.SelectedItem.Value);
                        HelperClass.ClearModal(this);
                        HelperClass.OpenSuccessToaster(this, "Project details saved successfully!");
                    }
                    else if (success == "Updated")
                    { 
                        if ((String)ViewState["ProjectName"] != txtprjname.Text || (String)ViewState["CustomerName"] != txtcname.Value || (String)ViewState["AssignedUsers"] != Users || (String)ViewState["EstimatedEffort"]!=txtEstimatedEffort.Text || (String)ViewState["DeliveryDate"] != txtDeliveryDate.Text || (String)ViewState["ProjectOwner"]!=ddlProjectOwner.SelectedItem.Text || (String)ViewState["ProjectStatus"]!= ddlProjectStatus.SelectedItem.Text)
                            flag = 1;
                        else
                            flag = 0;

                        if (flag == 1)
                            SendEmailEdit(Mails, txtprjID.Text, txtprjname.Text, txtcname.Value, txtDeliveryDate.Text, txtEstimatedEffort.Text, ddlProjectOwner.SelectedItem.Text, ddlProjectStatus.SelectedItem.Value,txtCreatedBy.Text);

                        HelperClass.ClearModal(this);
                        HelperClass.OpenSuccessToaster(this, "Project details updated successfully!");
                    }
                    else
                    {
                        HelperClass.OpenModal(this, "AddProjectModal", false);
                        HelperClass.OpenErrorModal(this, "Error, while inserting records!");
                        return;
                    }

                    //Attachment Upload

                    if (WC_Attachment.HasFile || WC_Attachment.HasFiles)
                        UploadFile(txtprjID.Text, WC_hfDocument.Value, WC_hfDocumentName.Value, "WC");

                    if (PDD_Attachment.HasFile || PDD_Attachment.HasFiles)
                        UploadFile(txtprjID.Text, PDD_hfDocument.Value, PDD_hfDocumentName.Value, "PDD");

                    if (HMI_Attachment.HasFile || HMI_Attachment.HasFiles)
                        UploadFile(txtprjID.Text, HMI_hfDocument.Value, HMI_hfDocumentName.Value, "HMI");

                    if (SW_Attachment.HasFile || SW_Attachment.HasFiles)
                        UploadFile(txtprjID.Text, SW_hfDocument.Value, SW_hfDocumentName.Value, "SW");

                    if (MOM_Attachment.HasFile || MOM_Attachment.HasFiles)
                        UploadFile(txtprjID.Text, MOM_hfDocument.Value, MOM_hfDocumentName.Value, "MOMs");

                    if (attachmentupload.HasFile || attachmentupload.HasFiles)
                        UploadFile(txtprjID.Text, hfDocument.Value, hfDocumentName.Value, "");

                    //MainTask details upload
                    int Result = 0;
                    for (int i = 0; i < DDLMainTaskSelector.Items.Count; i++)
                    {
                        if (DDLMainTaskSelector.Items[i].Selected == true)
                        {
                            Taskdetails td = new Taskdetails();
                            td.Maintask = DDLMainTaskSelector.Items[i].Text;
                            td.MaintaskStatus = "Open";
                            td.Projectid = txtprjID.Text;
                            Result = DBAccess.DBAccess.SaveMainTaskData(td, "Save_MainTaskDetails");
                        }
                    }

                    HelperClass.ClearModal(this);

                    BindPrjIDList();
                    BindCustNames();
                    BindProjectInfo();
                }
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("AddProject_Yes_ServerClick: " + ex.Message);
            }
        }
        
        public void SendEmailNew(string emails, string ProjID, string ProjName, string CustName,string Deliverydate, string EstimatedEffort, string ProjectOwner, string ProjectStatus )
        {
            try
            {
                string CreatedBy = (String)Session["username"];

                WService ws = new WService();
                string AppPass = HelperClass.AppPassword;
                ws.From = HelperClass.Email_ID + ";" + HelperClass.EncodePasswordToBase64(AppPass) ;
                ws.To = emails;
                ws.Subject = "ADES - Project Creation";
                ws.MsgBody = "A new project has been created in ADES Software. <br/><br/> <b>Project ID:</b> " + ProjID + "<br/> <b>Project Name:</b> " + ProjName + "<br/> <b>Customer Name:</b> " + CustName + "<br> <b>Project Owner:</b> " + ProjectOwner + "<br/> <b>Estimated Effort (HH): </b> " + EstimatedEffort + "<br/> <b>Delivery Date:</b> " + Deliverydate + "<br/> <b>Project Status: </b>" + ProjectStatus + "<br/> <b>Project Created by: </b>" + CreatedBy + "<br/><br/>For more information, log in to <a href='" + ConfigurationManager.AppSettings["ResetPasswordLink"].ToString() + "Login.aspx'>" + ConfigurationManager.AppSettings["ResetPasswordLink"].ToString() + "Login.aspx</a>";

                DBAccess.DBAccess.InsertEmailDetails(ws);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("SendEmail: " + ex.Message);
            }
        }

        public void SendEmailEdit(string emails, string ProjID, string ProjName, string CustName, string Deliverydate, string EstimatedEffort, string ProjectOwner, string ProjectStatus,string CreatedBy)
        {
            try
            {
                string Updatedby = (String)Session["username"];

                WService ws = new WService();
                string AppPass = HelperClass.AppPassword;
                ws.From = HelperClass.Email_ID + ";" + HelperClass.EncodePasswordToBase64(AppPass);
                ws.To = emails;
                ws.Subject = "ADES - Project Updation";
                ws.MsgBody = "Following project has been updated in ADES Software. <br/><br/> <b>Project ID:</b> " + ProjID + "<br/> <b>Project Name:</b> " + ProjName + "<br/> <b>Customer Name:</b> " + CustName + "<br> <b>Project Owner:</b> " + ProjectOwner + "<br/> <b>Estimated Effort (HH): </b> " + EstimatedEffort + "<br/> <b>Delivery Date:</b> " + Deliverydate + "<br/> <b>Project Status: </b>" + ProjectStatus + "<br/> <b>Project Created by: </b>" + CreatedBy + "<br/> <b>Project Updated by: </b>" + Updatedby + "<br/><br/>For more information, log in to <a href='" + ConfigurationManager.AppSettings["ResetPasswordLink"].ToString() + "Login.aspx'>" + ConfigurationManager.AppSettings["ResetPasswordLink"].ToString() + "Login.aspx</a>";

                DBAccess.DBAccess.InsertEmailDetails(ws);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("SendEmail: " + ex.Message);
            }
        }
         
        protected void btndlt_Click(object sender, EventArgs e)
        {
            try
            {
                int DeleteRowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
                ViewState["DeleteRowIndex"] = DeleteRowIndex;

                string pname = (gridprj.Rows[DeleteRowIndex].FindControl("lblPname") as Label).Text;
                ConfirmText.Text = "Are you sure you want to delete Project- " + pname + "?";

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
                int DeleteRowIndex = (int)ViewState["DeleteRowIndex"];

                DefectTracker defectTracker = new DefectTracker();
                defectTracker.PID = (gridprj.Rows[DeleteRowIndex].FindControl("lblID") as LinkButton).Text;
                defectTracker.Param = "Delete";

                string success = DBAccess.DBAccess.InsertUpdateProject(defectTracker);
                if (success == "Deleted")
                {
                    ViewState["DeleteRowIndex"] = -1;
                    HelperClass.OpenSuccessToaster(this, "Project details deleted successfully!");
                }
                else
                    HelperClass.OpenErrorModal(this, "Error, while deleting records!");
   
                BindPrjIDList();
                BindCustNames();
                BindProjectInfo();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("saveConfirmYes_ServerClick: " + ex.Message);
            }
        }

        protected void link_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                int DeleteRowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
                ViewState["DeleteRowIndex"] = DeleteRowIndex;
                
                string Fname = (gridFiles.Rows[DeleteRowIndex].FindControl("lblFileName") as Label).Text;
                ConfirmFileText.Text = "Are you sure you want to delete " + Fname + "?";

                HelperClass.OpenModal(this, "AddProjectModal", false);
                HelperClass.OpenModal(this, "ConfirmationFileModal", true);
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("link_Delete_Click: " + ex.Message);
            }
        }

        protected void FileDelete_Yes_ServerClick(object sender, EventArgs e)
        {
            try
            {
                int RowIndex = (int)ViewState["DeleteRowIndex"];
                string Fname = (gridFiles.Rows[RowIndex].FindControl("lblFileName") as Label).Text;
                int IDD = Convert.ToInt16((gridFiles.Rows[RowIndex].FindControl("hfIDD") as HiddenField).Value);
                string prjID = txtprjID.Text;

                Files file = new Files
                {
                    FName = Fname,
                    IDD = IDD,
                    ProjID = prjID,
                    Param = "DeleteFile"
                };

                string success = DBAccess.DBAccess.DeleteFile(file);

                if (success == "Deleted")
                {
                    ViewState["DeleteRowIndex"] = -1;
                    HelperClass.OpenSuccessToaster(this, "File deleted successfully!");
                    BindFileNames();
                    HelperClass.OpenModal(this,"AddProjectModal", false);
                    
                }
                else
                {
                    HelperClass.OpenModal(this, "AddProjectModal", false);
                    HelperClass.OpenErrorModal(this, "Error, while deleting the file!");
                    return;
                }

                BindFileNames();
                BindProjectInfo();
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("FileDelete_Yes_ServerClick: " + ex.Message);
            }
        }

        protected void link_download_Click(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);

                Files file = new Files
                {
                    IDD = Convert.ToInt16((gridFiles.Rows[rowIndex].FindControl("hfIDD") as HiddenField).Value),
                    ProjID = txtprjID.Text,
                    FName = (gridFiles.Rows[rowIndex].FindControl("lblFileName") as Label).Text,
                    FileType = (gridFiles.Rows[rowIndex].FindControl("lblFType") as Label).Text,
                };

                byte[] bytes = (byte[])(DBAccess.DBAccess.GetFileInfo(file));

                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;
                response.ContentType = "application/force-download";
                response.AddHeader("Content-Disposition", "attachment;filename=" + file.FName);
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
                df.PID = txtProjID.Text;
                df.CName = txtCustName.Text;
                df.Param = "View";

                gridprj.DataSource = DBAccess.DBAccess.GetProjectInfo(df);
                gridprj.DataBind();
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("BtnFilterSearch_Click: " + ex.Message);
            }
        }

        protected void Link_MainTask_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Tasks.aspx",false); 
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BtnInternal_Click: " + ex.Message);
            }
        }

        protected void Link_Internal_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(string.Format("~/Internal_Issue.aspx?ProjectID={0}", PID), false);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BtnInternal_Click: " + ex.Message);
            }
        }

        protected void Link_External_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(String.Format("~/ProjectBacklog.aspx?ProjectID={0}", PID), false);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BtnExternal_Click: " + ex.Message);
            }
        }
    }
}