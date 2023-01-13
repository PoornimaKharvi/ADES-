using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Windows.Controls;
//using System.Windows.Controls;
using ADES_22.Model;
//using Control = System.Web.UI.Control;

namespace ADES_22
{
    public partial class STaskTransactionDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtyear.Text = DateTime.Now.Year.ToString();
                string currentdate = DateTime.Now.Date.ToString();
                String list = "";
                list = DBAccess.DBAccess.GetWeekNumbers(currentdate);
                txtweekno.Text = list;
                BindEmployee();
                ddlemployee.SelectedValue = Session["username"].ToString();
                Selectedtasktrans();
            }
        }

        public void BindGridView(string name)
        {
            tasktransactiondetails tasktransactiondetails = new tasktransactiondetails();
            List<tasktransactiondetails> list = new List<tasktransactiondetails>();
            tasktransactiondetails.AssignedTo = Session["Name"].ToString();
            string dept = Session["Dept"].ToString();
            DataTable dtRemarksDetails = new DataTable();
            Session["TransDetails"] = list = DBAccess.DBAccess.GetMemberTaskDetails(tasktransactiondetails, dept, "View_TaskTransactionDetails", out dtRemarksDetails);
            lvTaskTransReport.DataSource = list;
            lvTaskTransReport.DataBind();
        }
        protected void btnSave_click(object sender, EventArgs e)
        {
            try
            {
                string dept = "";
                tasktransactiondetails tasktrans = new tasktransactiondetails();
                string success = "";
                for (int i = 0; i < lvTaskTransReport.Items.Count(); i++)
                {
                    tasktrans.SubTaskIDD = ((lvTaskTransReport.Items[i]).FindControl("hdnsubtaskid") as HiddenField).Value;
                    tasktrans.SubtaskStatus = ((lvTaskTransReport.Items[i]).FindControl("ddlsubtaskstatus") as DropDownList).SelectedValue;
                    tasktrans.SubtaskStatus = ((lvTaskTransReport.Items[i]).FindControl("ddlsubtaskstatus") as DropDownList).SelectedValue;
                    tasktrans.Projectid = ((lvTaskTransReport.Items[i]).FindControl("lbprojectid") as Label).Text;
                    tasktrans.Year = ((lvTaskTransReport.Items[i]).FindControl("hdnyearno") as HiddenField).Value;
                    tasktrans.Weekno = ((lvTaskTransReport.Items[i]).FindControl("hdnweekno") as HiddenField).Value;
                    tasktrans.TaskType = ((lvTaskTransReport.Items[i]).FindControl("hdntasktype") as HiddenField).Value;
                    tasktrans.Maintask = ((lvTaskTransReport.Items[i]).FindControl("lbmaintask") as Label).Text;
                    tasktrans.Subtask = (lvTaskTransReport.Items[i].FindControl("lbsubtask") as Label).Text;
                    tasktrans.MainTaskEstimatedeffort = ((lvTaskTransReport.Items[i]).FindControl("hdnmaintaskestimatedeffort") as HiddenField).Value;
                    tasktrans.SubTaskEstimatedeffort = ((lvTaskTransReport.Items[i]).FindControl("lbEstmteffort") as Label).Text;
                    tasktrans.Request = ((lvTaskTransReport.Items[i]).FindControl("hdnrequest") as HiddenField).Value;
                    tasktrans.AssignedTo = ((lvTaskTransReport.Items[i]).FindControl("hdnassignedto") as HiddenField).Value;
                    tasktrans.MainTaskIDD = ((lvTaskTransReport.Items[i]).FindControl("hdnmaintaskid") as HiddenField).Value;
                    tasktrans.SubTaskIDD = ((lvTaskTransReport.Items[i]).FindControl("hdnsubtaskid") as HiddenField).Value;
                    tasktrans.ProblemID = ((lvTaskTransReport.Items[i]).FindControl("hdnproblemID") as HiddenField).Value;
                    success = DBAccess.DBAccess.UpdateSubtaskDetails(tasktrans);
                    success = SaveTransactionDetails(tasktrans, i, "txtDay1", "hfDay1FileName", "hfDay1Remarks", "hfDay1Date");
                    success = SaveTransactionDetails(tasktrans, i, "txtDay2", "hfDay2FileName", "hfDay2Remarks", "hfDay2Date");
                    success = SaveTransactionDetails(tasktrans, i, "txtDay3", "hfDay3FileName", "hfDay3Remarks", "hfDay3Date");
                    success = SaveTransactionDetails(tasktrans, i, "txtDay4", "hfDay4FileName", "hfDay4Remarks", "hfDay4Date");
                    success = SaveTransactionDetails(tasktrans, i, "txtDay5", "hfDay5FileName", "hfDay5Remarks", "hfDay5Date");
                    success = SaveTransactionDetails(tasktrans, i, "txtDay6", "hfDay6FileName", "hfDay6Remarks", "hfDay6Date");
                    success = SaveTransactionDetails(tasktrans, i, "txtDay7", "hfDay7FileName", "hfDay7Remarks", "hfDay7Date");
                }
                if (success == "Inserted")
                {
                    HelperClass.OpenSuccessToaster(this, "Details successfully Inserted!");
                    Selectedtasktrans();
                }
                else if (success == "Updated")
                {
                    HelperClass.OpenSuccessToaster(this, "Details successfully  Updated!");
                    Selectedtasktrans();
                }
                else
                {
                    HelperClass.OpenErrorModal(this, "Error, while  saving records.");
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnSave_click" + ex.Message);
            }
        }
        private string SaveTransactionDetails(tasktransactiondetails tasktrans, int i, string txtDayValue, string hdnfFileName, string hfDayRemarks, string hfDayDate)
        {
            string success = "";
            try
            {
                if ((lvTaskTransReport.Items[i].FindControl(txtDayValue) as TextBox).Enabled)
                {
                    tasktrans.Spenthour = (lvTaskTransReport.Items[i].FindControl(txtDayValue) as TextBox).Text;
                    tasktrans.Remarks = (lvTaskTransReport.Items[i].FindControl(hfDayRemarks) as HiddenField).Value;
                    tasktrans.Date = (lvTaskTransReport.Items[i].FindControl(hfDayDate) as HiddenField).Value;
                    tasktrans.FileName = (lvTaskTransReport.Items[i].FindControl(hdnfFileName) as HiddenField).Value;
                    if (tasktrans.FileName == "")
                    {

                    }
                    else
                    {
                        string drawing = tasktrans.FileName;
                        byte[] drawinginbytes = System.Convert.FromBase64String(drawing.Substring(drawing.LastIndexOf(',') + 1));
                        tasktrans.File = drawinginbytes;
                        tasktrans.FileName = tasktrans.FileName;
                    }
                    success = DBAccess.DBAccess.InsertTaskTransDetails(tasktrans, "Save_TaskTransactionDetails");
                }

            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("SaveTransactionDetails: " + ex.Message);
            }
            return success;
        }
        public void Selectedtasktrans()
        {
            List<tasktransactiondetails> list = new List<tasktransactiondetails>();
            tasktransactiondetails tasktrans = new tasktransactiondetails();
            try
            {

                tasktrans.Weekno = txtweekno.Text;
                tasktrans.Year = txtyear.Text;
                //tasktrans.Projectid = txtstransdtlsprojects.Text;


                tasktrans.AssignedTo = ddlemployee.SelectedValue;
                string dept = Session["Dept"].ToString();
                DataTable dtRemarksDetails = new DataTable();

                Session["Tasktransdata"] = list = DBAccess.DBAccess.GetMemberTaskDetails(tasktrans, dept, "View_TaskTransactionDetails", out dtRemarksDetails);
                lvTaskTransReport.DataSource = list;
                lvTaskTransReport.DataBind();
                if (Session["Role"].ToString() == "Team Leader" || Session["Role"].ToString() == "Team Manager")
                {
                    lbemployee.Visible = true;
                    ddlemployee.Visible = true;
                    //BindEmployee();

                }
            }
            catch (Exception e)
            {
                Logger.WriteErrorLog("Selectedtasktrans" + e.Message);
            }
        }
        public void BindEmployee()
        {
            try
            {
                DataTable employee = new DataTable();
                DataTable projectid = new DataTable();
                string planner = Session["username"].ToString();
                DataTable dt = DBAccess.DBAccess.GetEmployeeReportTo(planner, out employee, out projectid);
                var emp = employee.AsEnumerable().Select(x => x.Field<string>("Employeeid")).ToList();
                emp.Add(planner);
                ddlemployee.DataSource = emp;
                ddlemployee.DataBind();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindAssignedTo: " + ex.Message);
            }
        }
        protected void listView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    DropDownList ddl = new DropDownList();
                    string hdnvalue = "";
                    ddl = (e.Item.FindControl("ddlsubtaskstatus") as DropDownList);
                    if (ddl != null)
                    {
                        hdnvalue = (e.Item.FindControl("hdnsubtaskstatus") as HiddenField).Value;
                        List<string> subtaskstatus = DBAccess.DBAccess.SubtaskStatus();
                        ddl.DataSource = subtaskstatus;
                        ddl.DataBind();
                        if (hdnvalue != "")
                        {
                            ddl.SelectedValue = hdnvalue;
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
        }
        public void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                Selectedtasktrans();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnView_Click" + ex.Message);
            }
        }
        protected void lbPrevweek_Click(object sender, EventArgs e)
        {
            try
            {
                int weekcount = Convert.ToInt32(txtweekno.Text) - 1;
                txtweekno.Text = weekcount.ToString();
                Selectedtasktrans();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("lbPrevweek_Click" + ex.Message);
            }
        }
        protected void lbNextweek_Click(object sender, EventArgs e)
        {
            try
            {
                int weekcount = Convert.ToInt32(txtweekno.Text) + 1;
                txtweekno.Text = weekcount.ToString();
                Selectedtasktrans();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("lbNextweek_Click" + ex.Message);
            }
        }
        protected void lbEditDay1_Click(object sender, EventArgs e)
        {
            try
            {
                int rowindex = ((sender as LinkButton).NamingContainer as ListViewItem).DataItemIndex;
                hfEditedRow.Value = rowindex.ToString();
                lblProjectID_Edit.Text = ((lvTaskTransReport.Items[rowindex]).FindControl("lbprojectid") as Label).Text;
                lblMainTask_Edit.Text = ((lvTaskTransReport.Items[rowindex]).FindControl("lbmaintask") as Label).Text;
                lblTask_Edit.Text = ((lvTaskTransReport.Items[rowindex]).FindControl("lbsubtask") as Label).Text;
                lblTask_Edit.Text = ((lvTaskTransReport.Items[rowindex]).FindControl("lbsubtask") as Label).Text;
                txtSpentHour_Edit.Text = (lvTaskTransReport.Items[rowindex].FindControl("txtDay" + hfEditedDayNo.Value) as TextBox).Text;
                txtRemarks_Edit.Text = (lvTaskTransReport.Items[rowindex].FindControl("hfDay" + hfEditedDayNo.Value + "Remarks") as HiddenField).Value;
                hfFile.Value = (lvTaskTransReport.Items[rowindex].FindControl("hfDay" + hfEditedDayNo.Value + "FileInBase64") as HiddenField).Value;
                hfFileName.Value = (lvTaskTransReport.Items[rowindex].FindControl("hfDay" + hfEditedDayNo.Value + "FileName") as HiddenField).Value;
                addFileName.Text= (lvTaskTransReport.Items[rowindex].FindControl("hfDay" + hfEditedDayNo.Value + "FileName") as HiddenField).Value;
                //lblDrawingName.Text = hfDrawingName.Value;
                HelperClass.OpenModal(this, "EditTaskDetails", true);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("lbNextweek_Click" + ex.Message);
            }
        }
        protected void btnUpdateEntry_Click(object sender, EventArgs e)
        {
            try
            {
                if (hfEditedRow.Value == "") return;
                int i = Convert.ToInt32(hfEditedRow.Value);
                tasktransactiondetails tasktrans = new tasktransactiondetails();
                string success = "";
                tasktrans.Projectid = ((lvTaskTransReport.Items[i]).FindControl("lbprojectid") as Label).Text;
                tasktrans.Year = ((lvTaskTransReport.Items[i]).FindControl("hdnyearno") as HiddenField).Value;
                tasktrans.Weekno = ((lvTaskTransReport.Items[i]).FindControl("hdnweekno") as HiddenField).Value;
                tasktrans.TaskType = ((lvTaskTransReport.Items[i]).FindControl("hdntasktype") as HiddenField).Value;
                tasktrans.AssignedTo = ((lvTaskTransReport.Items[i]).FindControl("hdnassignedto") as HiddenField).Value;
                tasktrans.MainTaskIDD = ((lvTaskTransReport.Items[i]).FindControl("hdnmaintaskid") as HiddenField).Value;
                tasktrans.SubTaskIDD = ((lvTaskTransReport.Items[i]).FindControl("hdnsubtaskid") as HiddenField).Value;
                if (hfFile.Value == "")
                {

                }
                else
                {
                    string drawing = hfFile.Value;
                    byte[] drawinginbytes = System.Convert.FromBase64String(drawing.Substring(drawing.LastIndexOf(',') + 1));
                    tasktrans.File = drawinginbytes;
                    tasktrans.FileName = hfFileName.Value;
                }
                tasktrans.Spenthour = txtSpentHour_Edit.Text;
                tasktrans.Remarks = txtRemarks_Edit.Text;
                tasktrans.Date = (lvTaskTransReport.Items[i].FindControl("hfDay" + hfEditedDayNo.Value + "Date") as HiddenField).Value;
                success = DBAccess.DBAccess.InsertTaskTransDetails(tasktrans, "Save_TaskTransactionDetails");
                if (success == "Inserted")
                {
                    HelperClass.OpenSuccessToaster(this, "Details successfully Inserted!");
                    Selectedtasktrans();
                }
                else if (success == "Updated")
                {
                    HelperClass.OpenSuccessToaster(this, "Details successfully  Updated!");
                    Selectedtasktrans();
                }
                else
                {
                    HelperClass.OpenErrorModal(this, "Error, while  saving records.");
                    HelperClass.OpenModal(this, "EditTaskDetails", true);
                    return;
                }
                hfEditedRow.Value = "";
                HelperClass.ClearModal(this);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("lbNextweek_Click" + ex.Message);
            }
        }
        protected void lnkStatus_Click(object sender, EventArgs e)
        {
            try
            {
                int rowindex = ((sender as LinkButton).NamingContainer as ListViewItem).DataItemIndex;
                ViewState["SubTaskId"] = ((lvTaskTransReport.Items[rowindex]).FindControl("hdnsubtaskid") as HiddenField).Value;
                HelperClass.OpenModal(this, "editsubtaskstatus", true);


                getStatus();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("lnkStatus_Click" + ex.Message);
            }
        }
        public void getStatus()
        {
            try
            {
                List<string> status = new List<string>();
                status = DBAccess.DBAccess.SubtaskStatus();
                ddleditsubtaskstatus.DataSource = status;
                ddleditsubtaskstatus.DataBind();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("getStatus" + ex.Message);
            }
        }
        protected void btnFileDownLoad_Click1(object sender, EventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(hfRowIndex.Value);
                string fileextension = ((System.Web.UI.WebControls.LinkButton)(lvTaskTransReport.Items[index].FindControl("lnkDay1FileName"))).Text;


                List<tasktransactiondetails> tasktrans = new List<tasktransactiondetails>();
                tasktrans = Session["Tasktransdata"] as List<tasktransactiondetails>;
                (lvTaskTransReport.Items[index].FindControl("lnkDay1FileName") as LinkButton).Text = tasktrans[index].Day1FileName;
                byte[] bytes = tasktrans[index].Day1bytefile;
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;
                response.ContentType = "application/force-download";
                response.AddHeader("Content-Disposition", "attachment;filename=" + (lvTaskTransReport.Items[index].FindControl("lnkDay1FileName") as LinkButton).Text);
                response.BinaryWrite(bytes);
                response.Flush(); // Sends all currently buffered output to the client.
                response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                HttpContext.Current.ApplicationInstance.CompleteRequest();// Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnFileDownLoad_Click" + ex.Message);
            }
        }
        protected void btnFileDownLoad_Click2(object sender, EventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(hfRowIndex.Value);
                string fileextension = ((System.Web.UI.WebControls.LinkButton)(lvTaskTransReport.Items[index].FindControl("lnkDay2FileName"))).Text;


                List<tasktransactiondetails> tasktrans = new List<tasktransactiondetails>();
                tasktrans = Session["Tasktransdata"] as List<tasktransactiondetails>;
                (lvTaskTransReport.Items[index].FindControl("lnkDay2FileName") as LinkButton).Text = tasktrans[index].Day2FileName;
                byte[] bytes = tasktrans[index].Day2bytefile;
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;
                response.ContentType = "application/force-download";
                response.AddHeader("Content-Disposition", "attachment;filename=" + (lvTaskTransReport.Items[index].FindControl("lnkDay2FileName") as LinkButton).Text);
                response.BinaryWrite(bytes);
                response.Flush(); // Sends all currently buffered output to the client.
                response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                HttpContext.Current.ApplicationInstance.CompleteRequest();// Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnFileDownLoad_Click" + ex.Message);
            }
        }
        protected void btnFileDownLoad_Click3(object sender, EventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(hfRowIndex.Value);
                string fileextension = ((System.Web.UI.WebControls.LinkButton)(lvTaskTransReport.Items[index].FindControl("lnkDay3FileName"))).Text;


                List<tasktransactiondetails> tasktrans = new List<tasktransactiondetails>();
                tasktrans = Session["Tasktransdata"] as List<tasktransactiondetails>;
                (lvTaskTransReport.Items[index].FindControl("lnkDay3FileName") as LinkButton).Text = tasktrans[index].Day3FileName;
                byte[] bytes = tasktrans[index].Day3bytefile;
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;
                response.ContentType = "application/force-download";
                response.AddHeader("Content-Disposition", "attachment;filename=" + (lvTaskTransReport.Items[index].FindControl("lnkDay3FileName") as LinkButton).Text);
                response.BinaryWrite(bytes);
                response.Flush(); // Sends all currently buffered output to the client.
                response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                HttpContext.Current.ApplicationInstance.CompleteRequest();// Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnFileDownLoad_Click" + ex.Message);
            }
        }
        protected void btnFileDownLoad_Click4(object sender, EventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(hfRowIndex.Value);
                string fileextension = ((System.Web.UI.WebControls.LinkButton)(lvTaskTransReport.Items[index].FindControl("lnkDay4FileName"))).Text;


                List<tasktransactiondetails> tasktrans = new List<tasktransactiondetails>();
                tasktrans = Session["Tasktransdata"] as List<tasktransactiondetails>;
                (lvTaskTransReport.Items[index].FindControl("lnkDay4FileName") as LinkButton).Text = tasktrans[index].Day4FileName;
                byte[] bytes = tasktrans[index].Day4bytefile;
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;
                response.ContentType = "application/force-download";
                response.AddHeader("Content-Disposition", "attachment;filename=" + (lvTaskTransReport.Items[index].FindControl("lnkDay4FileName") as LinkButton).Text);
                response.BinaryWrite(bytes);
                response.Flush(); // Sends all currently buffered output to the client.
                response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                HttpContext.Current.ApplicationInstance.CompleteRequest();// Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnFileDownLoad_Click" + ex.Message);
            }
        }
        protected void btnFileDownLoad_Click5(object sender, EventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(hfRowIndex.Value);
                string fileextension = ((System.Web.UI.WebControls.LinkButton)(lvTaskTransReport.Items[index].FindControl("lnkDay5FileName"))).Text;


                List<tasktransactiondetails> tasktrans = new List<tasktransactiondetails>();
                tasktrans = Session["Tasktransdata"] as List<tasktransactiondetails>;
                (lvTaskTransReport.Items[index].FindControl("lnkDay5FileName") as LinkButton).Text = tasktrans[index].Day5FileName;
                byte[] bytes = tasktrans[index].Day5bytefile;
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;
                response.ContentType = "application/force-download";
                response.AddHeader("Content-Disposition", "attachment;filename=" + (lvTaskTransReport.Items[index].FindControl("lnkDay5FileName") as LinkButton).Text);
                response.BinaryWrite(bytes);
                response.Flush(); // Sends all currently buffered output to the client.
                response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                HttpContext.Current.ApplicationInstance.CompleteRequest();// Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnFileDownLoad_Click" + ex.Message);
            }
        }
        protected void btnFileDownLoad_Click6(object sender, EventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(hfRowIndex.Value);
                string fileextension = ((System.Web.UI.WebControls.LinkButton)(lvTaskTransReport.Items[index].FindControl("lnkDay6FileName"))).Text;


                List<tasktransactiondetails> tasktrans = new List<tasktransactiondetails>();
                tasktrans = Session["Tasktransdata"] as List<tasktransactiondetails>;
                (lvTaskTransReport.Items[index].FindControl("lnkDay6FileName") as LinkButton).Text = tasktrans[index].Day6FileName;
                byte[] bytes = tasktrans[index].Day6bytefile;
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;
                response.ContentType = "application/force-download";
                response.AddHeader("Content-Disposition", "attachment;filename=" + (lvTaskTransReport.Items[index].FindControl("lnkDay6FileName") as LinkButton).Text);
                response.BinaryWrite(bytes);
                response.Flush(); // Sends all currently buffered output to the client.
                response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                HttpContext.Current.ApplicationInstance.CompleteRequest();// Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnFileDownLoad_Click" + ex.Message);
            }
        }
        protected void btnFileDownLoad_Click7(object sender, EventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(hfRowIndex.Value);
                string fileextension = ((System.Web.UI.WebControls.LinkButton)(lvTaskTransReport.Items[index].FindControl("lnkDay7FileName"))).Text;


                List<tasktransactiondetails> tasktrans = new List<tasktransactiondetails>();
                tasktrans = Session["Tasktransdata"] as List<tasktransactiondetails>;
                (lvTaskTransReport.Items[index].FindControl("lnkDay7FileName") as LinkButton).Text = tasktrans[index].Day7FileName;
                byte[] bytes = tasktrans[index].Day7bytefile;
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;
                response.ContentType = "application/force-download";
                response.AddHeader("Content-Disposition", "attachment;filename=" + (lvTaskTransReport.Items[index].FindControl("lnkDay7FileName") as LinkButton).Text);
                response.BinaryWrite(bytes);
                response.Flush(); // Sends all currently buffered output to the client.
                response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                HttpContext.Current.ApplicationInstance.CompleteRequest();// Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnFileDownLoad_Click" + ex.Message);
            }
        }

    }
}
