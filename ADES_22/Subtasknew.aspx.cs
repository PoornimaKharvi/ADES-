using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADES_22.Model;
using System.Data;

namespace ADES_22
{
    public partial class Subtasknew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProjectId();
                GetCustomername();
                BindWeekNumbers();
                BindWeekNumbers2();
                BindAssignedTo();
                string currentdate = DateTime.Now.Date.ToString();
                string currentyear = DateTime.Now.Year.ToString();
                String list = "";
                //list = DBAccess.DBAccess.GetWeekNumbers(currentdate, currentyear);
                ddlweekno.SelectedValue = list;
                string currentdate1 = DateTime.Now.Date.ToString();
                string currentyear1 = DateTime.Now.Year.ToString();
                String list1 = "";
                //list1 = DBAccess.DBAccess.GetWeekNumbers(currentdate1, currentyear1);
                ddlweekno2.SelectedValue = list1;
                Selectedsubtask();
            }
        }
        public void BindProjectId()
        {
            try
            {
                List<string> projectid = DBAccess.DBAccess.GetProjectIDList();
                lbprojectid.DataSource = projectid;
                lbprojectid.DataBind();
            }
            catch (Exception e)
            {
                Logger.WriteErrorLog("BindProjectId" + e.Message);
            }
        }
        public void GetCustomername()
        {
            List<string> str = new List<string>();
            try
            {
                str = DBAccess.DBAccess.GetCustName("", "");
                lbxcustomer.DataSource = str;
                lbxcustomer.DataBind();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetCustomername" + ex.Message);
            }
        }
        public void BindWeekNumbers()
        {
            try
            {
                List<string> str = new List<string>();
                str = DBAccess.DBAccess.GetddlWeekNumbers();
                ddlweekno.DataSource = str;
                ddlweekno.DataBind();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindWeekNumbers" + ex.Message);
            }
        }
        public void BindWeekNumbers2()
        {
            try
            {
                List<string> str = new List<string>();
                str = DBAccess.DBAccess.GetddlWeekNumbers();
                ddlweekno2.DataSource = str;
                ddlweekno2.DataBind();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindWeekNumbers" + ex.Message);
            }
        }
        public void BindAssignedTo()
        {
            try
            {
                lbxemployee.DataSource = DBAccess.DBAccess.GetfilterAssignedValues();
                lbxemployee.DataBind();
                lbxemployee.Items.Insert(0, new ListItem("Select", ""));
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindAssignedTo: " + ex.Message);
            }
        }
        public void Selectedsubtask()
        {
            try
            {
                List<Subtaskdetails> list = new List<Subtaskdetails>();
                Subtaskdetails subtask = new Subtaskdetails();
                string ProjectID = "";
                for (int i = 0; i < lbprojectid.Items.Count; i++)
                {
                    if (lbprojectid.Items[i].Selected == true)
                    {
                        if (ProjectID == "")
                            ProjectID = "'" + lbprojectid.Items[i].Value + "'";
                        else
                            ProjectID += ",'" + lbprojectid.Items[i].Value + "'";
                    }
                }
                subtask.Projectid = ProjectID;

                string Employee = "";
                for (int i = 0; i < lbxemployee.Items.Count; i++)
                {
                    if (lbxemployee.Items[i].Selected == true)
                    {
                        if (Employee == "")
                            Employee = "'" + lbxemployee.Items[i].Value + "'";
                        else
                            Employee += ",'" + lbxemployee.Items[i].Value + "'";
                    }
                }
                subtask.Employees = Employee;

                subtask.Year = DateTime.Now.Year.ToString();
                subtask.Weekno = ddlweekno.SelectedValue;
                subtask.Param = "PlannerView";
                DataTable dtDropD1 = new DataTable();
                DataTable dtDropD2 = new DataTable();
                DataTable dt = DBAccess.DBAccess.GetSubtaskentry(subtask, out dtDropD1, out dtDropD2);
                for (int i = 0; i < dtDropD2.Rows.Count; i++)
                {
                    Subtaskdetails sub = new Subtaskdetails();
                    sub.Weekno = dtDropD2.Rows[i]["WeekNo"].ToString();
                    sub.Employees = dtDropD2.Rows[i]["AssignedTo"].ToString();
                    sub.Projectid = dtDropD2.Rows[i]["ProjectID"].ToString();
                    sub.Maintask = dtDropD2.Rows[i]["Maintask"].ToString();
                    sub.Subtask = dtDropD2.Rows[i]["Subtask"].ToString();
                    sub.Tasktype = dtDropD2.Rows[i]["Tasktype"].ToString();
                    sub.ManualEntryRemark = dtDropD2.Rows[i]["ManualEntryRemarks"].ToString();
                    sub.Estimatedeffortsub = dtDropD2.Rows[i]["EstimatedEffort"].ToString();
                    //subtask.RemarksEngineer= dtDropD2.Rows[i][""].ToString();
                    //subtask.ViewMOM= dtDropD2.Rows[i][""].ToString();
                    sub.SubtaskStatus = dtDropD2.Rows[i]["Status"].ToString();
                    if(sub.SubtaskStatus!=null&& sub.SubtaskStatus != "")
                    {
                        sub.Visibility = true;
                    }
                    sub.MaintaskStatus = dtDropD2.Rows[i]["MainTaskStatus"].ToString();
                    if (sub.MaintaskStatus != null && sub.MaintaskStatus != "")
                    {
                        sub.mainVisibility = true;
                    }
                    sub.ProjectStatus = dtDropD2.Rows[i]["DispatchStatus"].ToString();
                    if (sub.ProjectStatus != null && sub.ProjectStatus != "")
                    {
                        sub.projectVisibility = true;
                    }
                    sub.Id = dtDropD2.Rows[i]["IDD"].ToString();
                    sub.Request = dtDropD2.Rows[i]["ProblemType"].ToString();
                    sub.Year = dtDropD2.Rows[i]["YearNo"].ToString();
                    sub.MainTaskIDD = dtDropD2.Rows[i]["MainTaskIDD"].ToString();
                    sub.DeliveryDate= dtDropD2.Rows[i]["DeliveryDate"].ToString();
                    sub.ProblemID= dtDropD2.Rows[i]["ProblemID"].ToString();
                    if (list.Count == 0)
                    {
                        sub.HeaderVisibility = true;
                    }
                    list.Add(sub);
                }
                lvSubtask.Visible = true;
                lvSubtask.DataSource = list;
                lvSubtask.DataBind();
                lvdashboardview.Visible = false;
                btnAdd.Visible = true;

                lbxteam.Visible = false;
                lbteam.Visible = false;

                lbcustomer.Visible = false;
                lbxcustomer.Visible = false;

                btndashboardview.Visible = false;
                btnview.Visible = true;

                ddlweekno2.Visible = false;
                lbweekno2.Visible = false;
            }
            catch (Exception e)
            {
                Logger.WriteErrorLog("Selectedsubtask" + e.Message);
            }
        }
        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                Selectedsubtask();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnView_Click" + ex.Message);
            }
        }
        public void BindEditProjectID()
        {
            try
            {
                List<string> projectid = DBAccess.DBAccess.GetProjectIDList();
                ddleditprojectid.DataSource = projectid;
                ddleditprojectid.DataBind();
                ddleditprojectid.Items.Insert(0, new ListItem("Select", ""));
            }
            catch (Exception ex)
            {

            }
        }
        public void BindRequest()
        {
            try
            {
                ddleditrequest.DataSource = HelperClass.BindIssueType();
                ddleditrequest.DataTextField = "Text";
                ddleditrequest.DataValueField = "Value";
                ddleditrequest.DataBind();
                ddleditrequest.Items.Insert(0, new ListItem("Select", ""));
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindRequest: " + ex.Message);
            }
        }
        public void BindTaskType()
        {
            try
            {
                ddledittasktype.DataSource = HelperClass.BindTaskType();
                ddledittasktype.DataTextField = "Text";
                ddledittasktype.DataValueField = "Value";
                ddledittasktype.DataBind();
                ddledittasktype.Items.Insert(0, new ListItem("Select", ""));
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindRequest: " + ex.Message);
            }
        }
        protected void lbEdit_Click(object sender, EventArgs e)
        {
            try
            {
                Subtaskdetails task = new Subtaskdetails();
                List<string> list = new List<string>();
                string list2 = "";
                int rowindex = (((sender as LinkButton).NamingContainer) as ListViewItem).DataItemIndex;
                hdnweekno.Value = (lvSubtask.Items[rowindex].FindControl("hdnweekno") as HiddenField).Value;
                hdnyear.Value = (lvSubtask.Items[rowindex].FindControl("hdnyear") as HiddenField).Value;
                hdneditid.Value = (lvSubtask.Items[rowindex].FindControl("hdnid") as HiddenField).Value;
                hdneditmaintaskidd.Value = (lvSubtask.Items[rowindex].FindControl("hdnaddmaintaskidd") as HiddenField).Value;
                txteditsubtask.Text = ((lvSubtask.Items[rowindex]).FindControl("lbsubtask") as Label).Text;
                BindEditProjectID();
                string value = (lvSubtask.Items[rowindex].FindControl("lbsubProjectid") as Label).Text;
                if (ddleditprojectid.Items.FindByValue(value) != null)
                {
                    ddleditprojectid.SelectedValue = value;
                }
                task.Projectid = (lvSubtask.Items[rowindex].FindControl("lbsubProjectid") as Label).Text;
                List<Subtaskdetails> sublst1 = new List<Subtaskdetails>();
                sublst1 = DBAccess.DBAccess.GetMaintaskValues(task);
                list2 = DBAccess.DBAccess.GetAssignedValues(task);
                List<string> list3 = new List<string>();
                list3 = list2.Split(',').ToList();
                ddleditmaintask.DataSource = list;
                ddleditmaintask.DataBind();
                ddleditassignedto.DataSource = list3;
                ddleditassignedto.DataBind();
                value = (lvSubtask.Items[rowindex].FindControl("lbsubMainTask") as Label).Text;
                if (ddleditmaintask.Items.FindByValue(value) != null)
                {
                    ddleditmaintask.SelectedValue = value;
                }
                value = (lvSubtask.Items[rowindex].FindControl("hdnassignedto") as HiddenField).Value;
                if (ddleditassignedto.Items.FindByValue(value) != null)
                {
                    ddleditassignedto.SelectedValue = value;
                }
                hdneditrequest.Value = (lvSubtask.Items[rowindex].FindControl("hdnproblemtype") as HiddenField).Value;
                //BindAssignedTo();
                BindTaskType();
                value = (lvSubtask.Items[rowindex].FindControl("lbtasktype") as Label).Text;
                if (ddledittasktype.Items.FindByValue(value) != null)
                {
                    ddledittasktype.SelectedValue = value;
                }
                BindRequest();
                value = (lvSubtask.Items[rowindex].FindControl("hdnproblemtype") as HiddenField).Value;
                if (ddleditrequest.Items.FindByValue(value) != null)
                {
                    ddleditrequest.SelectedValue = value;
                }
                txteditesimatedeffort.Text = ((lvSubtask.Items[rowindex]).FindControl("txtestimatedeffortsub") as TextBox).Text;
                dhneditdeliverydate.Value = ((lvSubtask.Items[rowindex]).FindControl("hdndeliverydate") as HiddenField).Value;
                txteditmanualentry.Text = ((lvSubtask.Items[rowindex]).FindControl("lbmanualentryremark") as Label).Text;
                hdnsubtaskstatus.Value = ((lvSubtask.Items[rowindex]).FindControl("lbstatus") as Label).Text;
                hdnmaintaskstatus.Value = ((lvSubtask.Items[rowindex]).FindControl("Label1") as Label).Text;
                hdnprojectstatus.Value = ((lvSubtask.Items[rowindex]).FindControl("Label2") as Label).Text;
                hdneditproblemid.Value=(lvSubtask.Items[rowindex].FindControl("hdnproblemid") as HiddenField).Value;
                HelperClass.OpenModal(this, "Editsubtask", false);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("lbEdit_Click" + ex.Message);
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Subtaskdetails subtask = new Subtaskdetails();
                subtask.Year = hdnyear.Value;
                subtask.Weekno = hdnweekno.Value;
                subtask.Projectid = ddleditprojectid.SelectedValue;
                subtask.Id = hdneditid.Value;
                subtask.Subtask = txteditsubtask.Text;
                subtask.Estimatedeffortsub = txteditesimatedeffort.Text;
                subtask.Assignedto = ddleditassignedto.SelectedValue;
                subtask.Maintask = ddleditmaintask.SelectedValue;
                subtask.MainTaskIDD = hdneditmaintaskidd.Value;
                subtask.Tasktype = ddledittasktype.SelectedValue;
                subtask.ManualEntryRemark = txteditmanualentry.Text;
                subtask.DeliveryDate = txtedideliverydate.Text;
                subtask.SubtaskStatus = hdnsubtaskstatus.Value;
                subtask.MaintaskStatus = hdnmaintaskstatus.Value;
                subtask.ProjectStatus = hdnprojectstatus.Value;
                subtask.ProblemID = hdneditproblemid.Value;
                //subtask.SubtaskStatus = ddleditstatus.SelectedValue;
                //subtask.MainTaskEstimatedeffort = hdneditmaintaskestimatedeffort.Value;
                subtask.Request = ddleditrequest.SelectedValue;
                int Result = DBAccess.DBAccess.InsertTaskDetailsGridview(subtask, "Update_SubTaskDetails");
                if (Result > 0)
                {
                    HelperClass.ClearModal(this);
                    HelperClass.OpenSuccessToaster(this, "Estimated Effort Updated successfully!");
                    Selectedsubtask();
                }
                else
                {
                    HelperClass.OpenModal(this, "Editsubtask", false);
                    HelperClass.OpenErrorModal(this, "Error, While Updating Records.");
                }

            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Update_click:" + ex.Message);
            }
        }
        protected void lbDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int rowindex = (((sender as LinkButton).NamingContainer) as ListViewItem).DataItemIndex;
                Session["DeleteRowIndex"] = rowindex;
                HelperClass.openConfirmModal(this, "Are you sure, you want to delete this record?");
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("lbDelete_Click" + ex.Message);
            }
        }
        protected void saveConfirmYes_ServerClick(object sender, EventArgs e)
        {
            try
            {
                int DeleteRowIndex = (int)Session["DeleteRowIndex"];
                Subtaskdetails subtask = new Subtaskdetails();
                subtask.Id = (lvSubtask.Items[DeleteRowIndex].FindControl("hdnid") as HiddenField).Value;
                string success = DBAccess.DBAccess.DeleteInsertSubTaskDetailsGridview(subtask, "Delete_SubTaskDetails");
                lvSubtask.EditIndex = -1;
                if (success == "Deleted")
                {
                    HelperClass.OpenSuccessToaster(this, "Subtask Details Deleted successfully");
                    Selectedsubtask();
                }
                else
                {
                    HelperClass.OpenErrorModal(this, "error, while deleting records.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("saveConfirmYes_ServerClick" + ex.Message);
            }
        }
        protected void lbtnsubtaskstatus_Click(object sender, EventArgs e)
        {
            try
            {
                int rowindex = (((sender as LinkButton).NamingContainer) as ListViewItem).DataItemIndex;
                hdneditsubtaskstatusid.Value = ((lvSubtask.Items[rowindex]).FindControl("hdnid") as HiddenField).Value;
                BindStatus();
                string value = ((lvSubtask.Items[rowindex]).FindControl("lbstatus") as Label).Text;
                if (ddleditsubtaskstatus.Items.FindByValue(value) != null)
                {
                    ddleditsubtaskstatus.SelectedValue = value;
                }
                HelperClass.OpenModal(this, "editsubtaskstatus", false);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("lbtnsubtaskstatus_Click" + ex.Message);
            }

        }
        public void BindStatus()
        {
            try
            {
                List<String> list = new List<String>();
                list = DBAccess.DBAccess.SubtaskStatus();
                ddleditsubtaskstatus.DataSource = list;
                ddleditsubtaskstatus.DataBind();
                ddleditsubtaskstatus.Items.Insert(0, new ListItem("Select", ""));
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindStatus: " + ex.Message);
            }
        }
        protected void btneditsubtaskstatus_Click(object sender, EventArgs e)
        {
            try
            {
                Subtaskdetails subtask = new Subtaskdetails();
                subtask.SubtaskStatus = ddleditsubtaskstatus.SelectedValue;
                subtask.Id = hdneditsubtaskstatusid.Value;
                //DBAccess.DBAccess.UpdateSubtaskDetails(subtask);
                HelperClass.ClearModal(this);
                HelperClass.OpenSuccessToaster(this, "Subtask Status Saved Successfullly");
                Selectedsubtask();
            }
            catch (Exception ex)
            {

            }
        }

        protected void lbtnmaintaskstatus_Click(object sender, EventArgs e)
        {
            try
            {
                int rowindex = (((sender as LinkButton).NamingContainer) as ListViewItem).DataItemIndex;
                hdnmaintaskid.Value = ((lvSubtask.Items[rowindex]).FindControl("hdnaddmaintaskidd") as HiddenField).Value;
                BindMainStatus();
                string value = ((lvSubtask.Items[rowindex]).FindControl("Label1") as Label).Text;
                if (ddlmaintaskvalue.Items.FindByValue(value) != null)
                {
                    ddlmaintaskvalue.SelectedValue = value;
                }
                HelperClass.OpenModal(this, "editmaintaskstatus", false);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("lbtnmaintaskstatus_Click" + ex.Message);
            }

        }
        public void BindMainStatus()
        {
            try
            {
                List<String> list = new List<String>();
                list = DBAccess.DBAccess.GetMainTaskStatus();
                ddlmaintaskvalue.DataSource = list;
                ddlmaintaskvalue.DataBind();
                ddlmaintaskvalue.Items.Insert(0, new ListItem("Select", ""));
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindStatus: " + ex.Message);
            }
        }
        protected void btnmaintaskstatus_Click(object sender, EventArgs e)
        {
            try
            {
                Taskdetails task = new Taskdetails();
                task.MaintaskStatus = ddlmaintaskvalue.SelectedValue;
                task.Id = hdnmaintaskid.Value;
                //DBAccess.DBAccess.UpdateMiantaskDetails(task);
                //DBAccess.DBAccess.InsertTaskDetailsGridview(task, "Update_MainTaskDetails");
                HelperClass.ClearModal(this);
                HelperClass.OpenSuccessToaster(this, "Maintask Status Saved Successfullly");
                Selectedsubtask();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("lbtnsubtaskstatus_Click" + ex.Message);
            }
        }
        protected void lbtnprojectstatus_Click(object sender, EventArgs e)
        {
            try
            {
                int rowindex = (((sender as LinkButton).NamingContainer) as ListViewItem).DataItemIndex;
                hdndispatchprojectid.Value = ((lvSubtask.Items[rowindex]).FindControl("lbsubProjectid") as Label).Text;
                BindProjectStatus();
                string value = ((lvSubtask.Items[rowindex]).FindControl("Label2") as Label).Text;
                if (ddldispatchstatus.Items.FindByValue(value) != null)
                {
                    ddldispatchstatus.SelectedValue = value;
                }
                HelperClass.OpenModal(this, "editdispatchstatus", false);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("lbtnmaintaskstatus_Click" + ex.Message);
            }

        }
        public void BindProjectStatus()
        {
            try
            {
                List<String> list = new List<String>();
                //list = DBAccess.DBAccess.GetProjectStatus();
                //ddldispatchstatus.DataSource = list;
                //ddldispatchstatus.DataBind();
                //ddldispatchstatus.Items.Insert(0, new ListItem("Select", ""));
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindStatus: " + ex.Message);
            }
        }
        protected void btnsavedispatch_Click(object sender, EventArgs e)
        {
            try
            {
                Subtaskdetails subtask = new Subtaskdetails();
                subtask.SubtaskStatus = ddldispatchstatus.SelectedValue;
                subtask.Projectid = hdndispatchprojectid.Value;
                //DBAccess.DBAccess.UpdateProjectStatus(subtask);
                HelperClass.ClearModal(this);
                HelperClass.OpenSuccessToaster(this, "Project Status Saved Successfullly");
                Selectedsubtask();
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            BindAddWeekNumbers();
            lbyear.Text = DateTime.Now.Year.ToString();
            txtrows.Text = "5";
            string param = "Oldrow";
            BindGridview(param);
            //Taskdetails taskdetails = new Taskdetails();
            //if (Session["MainTaskDetails"] != null)
            //{
            //    taskdetails = (Taskdetails)Session["MainTaskDetails"];
            //}
            //lbshowpopprojectid.Text = taskdetails.Projectid;
            //lbshowpopmaintask.Text = taskdetails.Maintask;
            //lbshowpoptasktype.Text = taskdetails.TaskTypeName;
            //lbshowpoprequest.Text = taskdetails.RequestName;
            //hdnmaintaskidd.Value = taskdetails.Id;
            //lbweekno.Text = taskdetails.Weekno;
            //lbyear.Text = taskdetails.Year;            
            HelperClass.OpenModal(this, "newsubtask", true);
        }
        public void BindAddWeekNumbers()
        {
            try
            {
                string currentdate = DateTime.Now.Date.ToString();
                string currentyear = DateTime.Now.Year.ToString();
                String list = "";
                List<int> list1 = new List<int>();
                //list = DBAccess.DBAccess.GetWeekNumbers(currentdate, currentyear);
                //for (int i = -1; i < 4; i++)
                //{
                //    list1.Add(Convert.ToInt32(list) + i);
                //}
                //ddladdweekno.DataSource = list1;
                //ddladdweekno.DataBind();
                //ddladdweekno.SelectedValue = list;
            }
            catch
            {

            }
        }
        protected void lbAddnewrow_Click(object sender, EventArgs e)
        {
            try
            {
                string param = "NewRow";
                List<Subtaskdetails> BeforeAddRowClickdList = new List<Subtaskdetails>();
                for (int i = 0; i < lvAddSubtask.Items.Count; i++)
                {
                    Subtaskdetails subtask = new Subtaskdetails();
                    subtask.Projectid = (lvAddSubtask.Items[i].FindControl("ddladdprojectid") as DropDownList).SelectedValue;
                    subtask.Maintask = (lvAddSubtask.Items[i].FindControl("ddladdmaintask") as DropDownList).SelectedValue;
                    subtask.Subtask = (lvAddSubtask.Items[i].FindControl("txtaddsubtask") as TextBox).Text;
                    subtask.Tasktype = (lvAddSubtask.Items[i].FindControl("ddladdtasktype") as DropDownList).SelectedValue;
                    subtask.Request = (lvAddSubtask.Items[i].FindControl("ddladdrequest") as DropDownList).SelectedValue;
                    subtask.Estimatedeffortsub = (lvAddSubtask.Items[i].FindControl("txtaddestimatedeffortsub") as TextBox).Text;
                    subtask.ManualEntryRemark = (lvAddSubtask.Items[i].FindControl("txtmanualentry") as TextBox).Text;
                    subtask.Assignedto = (lvAddSubtask.Items[i].FindControl("ddladdassignedto") as DropDownList).SelectedValue;
                    subtask.SubtaskStatus = (lvAddSubtask.Items[i].FindControl("ddladdstatus") as DropDownList).SelectedValue;
                    BeforeAddRowClickdList.Add(subtask);
                }
                Session["ExistingGridvalues"] = BeforeAddRowClickdList;
                BindGridview(param);
                HelperClass.OpenModal(this, "newsubtask", false);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("lbAddnewrow_Click:" + ex.Message);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int Result = 0;
                for (int i = 0; i < lvAddSubtask.Items.Count; i++)
                {
                    Subtaskdetails subtask = new Subtaskdetails();
                    Taskdetails task = new Taskdetails();
                    // subtask.Weekno = lbweekno.Text;
                    //subtask.Projectid = lbshowpopprojectid.Text;
                    //subtask.Maintask = lbshowpopmaintask.Text;
                    subtask.Projectid = ((lvAddSubtask.Items[i]).FindControl("ddladdprojectid") as DropDownList).SelectedValue;
                    subtask.Maintask = ((lvAddSubtask.Items[i]).FindControl("ddladdmaintask") as DropDownList).SelectedValue;
                    string maintaskid = "";
                    maintaskid = DBAccess.DBAccess.GetMaintaskIdd(subtask);
                    //HiddenField hdn = (GVAdd.Rows[i]).FindControl("hdnmaintaskidd") as HiddenField;
                    //hdn.Value = maintaskid.ToString();
                    subtask.MainTaskIDD = maintaskid.ToString();
                    subtask.Year = lbyear.Text;
                    subtask.Weekno = ddladdweekno.SelectedValue;
                    subtask.Subtask = ((lvAddSubtask.Items[i]).FindControl("txtaddsubtask") as TextBox).Text;
                    subtask.Tasktype = ((lvAddSubtask.Items[i]).FindControl("ddladdtasktype") as DropDownList).SelectedValue;
                    //subtask.Dependencies = ((GVAdd.Rows[i]).FindControl("ddladddependencies") as DropDownList).SelectedValue;
                    subtask.Request = ((lvAddSubtask.Items[i]).FindControl("ddladdrequest") as DropDownList).SelectedValue;
                    subtask.ManualEntryRemark = ((lvAddSubtask.Items[i]).FindControl("txtmanualentry") as TextBox).Text;
                    subtask.Estimatedeffortsub = ((lvAddSubtask.Items[i]).FindControl("txtaddestimatedeffortsub") as TextBox).Text;
                    subtask.Assignedto = ((lvAddSubtask.Items[i]).FindControl("ddladdassignedto") as DropDownList).SelectedValue; subtask.SubtaskStatus = ((lvAddSubtask.Items[i]).FindControl("ddladdstatus") as DropDownList).SelectedValue;
                    subtask.DeliveryDate = ((lvAddSubtask.Items[i]).FindControl("txtdeliverydate") as TextBox).Text;
                    if (subtask.Subtask == "" || subtask.Estimatedeffortsub == "" || subtask.Assignedto == "")
                    {
                        continue;
                    }
                    //if (Session["MainTaskDetails"] != null)
                    //{
                    //    task = (Taskdetails)Session["MainTaskDetails"];
                    //}
                    //subtask.MainTaskEstimatedeffort = task.Estimatedeffort;
                    //subtask.Tasktype = task.Tasktype;
                    //subtask.Request = task.Request;
                    //subtask.MainTaskIDD = task.Id;                    
                    //subtask.ProblemID = task.ProblemID;
                    Result = DBAccess.DBAccess.SaveSubTaskDetails(subtask, "Save_SubTaskDetails");
                    if (Result == 0)
                    {
                        break;
                    }
                    Session["EmployeeEmailandPassword"] = DBAccess.DBAccess.GetEmployeeemaildetails(subtask.Assignedto);
                    EmpDetails emp = new EmpDetails();
                    if (Session["EmployeeEmailandPassword"] != null)
                    {
                        emp = (EmpDetails)Session["EmployeeEmailandPassword"];
                    }
                    string teamleader = Session["Name"].ToString();
                    SendEmail(emp.EmpID, emp.EmpName, emp.Email, emp.Password, subtask.Subtask, teamleader);
                }
                if (Result > 0)
                {
                    HelperClass.ClearModal(this);
                    HelperClass.OpenSuccessToaster(this, "SubTask Details saved successfully!");
                    Selectedsubtask();
                }
                else
                {
                    HelperClass.OpenModal(this, "newsubtask", true);
                    HelperClass.OpenErrorModal(this, "Error, While Saving Records.");
                }

            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnSave_Click" + ex.Message);
            }
        }
        protected void BindGridview(string param)
        {
            Subtaskdetails subtask = new Subtaskdetails();
            int rows = Convert.ToInt32(txtrows.Text);
            List<Subtaskdetails> BeforeAddRowClickdList = new List<Subtaskdetails>();
            if (param == "NewRow")
            {
                if (Session["ExistingGridvalues"] != null)
                {
                    BeforeAddRowClickdList = (List<Subtaskdetails>)Session["ExistingGridvalues"];
                }
                rows = BeforeAddRowClickdList.Count + Convert.ToInt32(txtrows.Text);
            }

            List<Subtaskdetails> list1 = new List<Subtaskdetails>();
            for (int i = 0; i < rows; i++)
            {
                if (i == 0)
                {
                    subtask.HeaderVisibility = true;
                    list1.Add(subtask);
                }
                else
                {
                    list1.Add(new Subtaskdetails());
                }
            }
            Session["currentdata"] = list1;
            lvAddSubtask.DataSource = list1;
            lvAddSubtask.DataBind();

            List<string> taskTypeList = DBAccess.DBAccess.GetTaskType();
            List<ListItem> issueTypeList = HelperClass.BindIssueType();
            List<string> statusvalues = DBAccess.DBAccess.SubtaskStatus();
            List<string> dependencies = DBAccess.DBAccess.GetDependencies();
            List<string> projectid = DBAccess.DBAccess.GetProjectIDList();
            List<string> list = new List<string>();
            string list2 = "";
            Subtaskdetails task = new Subtaskdetails();

            for (int i = 0; i < lvAddSubtask.Items.Count; i++)
            {
                DropDownList ddl1 = (lvAddSubtask.Items[i]).FindControl("ddladdprojectid") as DropDownList;
                ddl1.DataSource = projectid;
                ddl1.DataBind();
                ddl1.Items.Insert(0, new ListItem("Select", ""));

                //ddladdprojectid_SelectedIndexChanged(null,null);
                DropDownList ddl = ((lvAddSubtask.Items[i]).FindControl("ddladdmaintask") as DropDownList);
                task.Projectid = ((lvAddSubtask.Items[i]).FindControl("ddladdprojectid") as DropDownList).SelectedValue;
                List<Subtaskdetails> sublst = new List<Subtaskdetails>();
                sublst = DBAccess.DBAccess.GetMaintaskValues(task);
                list2 = DBAccess.DBAccess.GetAssignedValues(task);
                ddl.DataSource = list;
                ddl.DataBind();


                DropDownList ddl2 = (lvAddSubtask.Items[i]).FindControl("ddladdassignedto") as DropDownList;
                ddl2.DataSource = list2;
                ddl2.DataBind();
                ddl2.Items.Insert(0, new ListItem("Select", ""));
                HelperClass.OpenModal(this, "newsubtask", true);

                DropDownList ddl3 = (lvAddSubtask.Items[i]).FindControl("ddladdtasktype") as DropDownList;
                ddl3.DataSource = taskTypeList;
                ddl3.DataBind();
                ddl3.Items.Insert(0, new ListItem("Select", ""));

                DropDownList ddl4 = (lvAddSubtask.Items[i]).FindControl("ddladdstatus") as DropDownList;
                ddl4.DataSource = statusvalues;
                ddl4.DataBind();
                ddl4.Items.Insert(0, new ListItem("Select", ""));

                DropDownList ddl5 = (lvAddSubtask.Items[i]).FindControl("ddladdrequest") as DropDownList;
                ddl5.DataSource = issueTypeList;
                ddl5.DataTextField = "Text";
                ddl5.DataValueField = "Value";
                ddl5.DataBind();
                ddl5.Items.Insert(0, new ListItem("Select", ""));

                DropDownList ddl6 = (lvAddSubtask.Items[i]).FindControl("ddladddependencies") as DropDownList;
                ddl6.DataSource = dependencies;
                ddl6.DataBind();
                ddl6.Items.Insert(0, new ListItem("Select", ""));


                if (param == "NewRow")
                {
                    if (i < BeforeAddRowClickdList.Count)
                    {
                        DropDownList ddlprojectid = (lvAddSubtask.Items[i]).FindControl("ddladdprojectid") as DropDownList;
                        ddlprojectid.SelectedValue = BeforeAddRowClickdList[i].Projectid;
                        DropDownList ddlmaintask = (lvAddSubtask.Items[i]).FindControl("ddladdmaintask") as DropDownList;
                        task.Projectid = ((lvAddSubtask.Items[i]).FindControl("ddladdprojectid") as DropDownList).SelectedValue;
                        List<Subtaskdetails> sublst1 = new List<Subtaskdetails>();
                        sublst1 = DBAccess.DBAccess.GetMaintaskValues(task);
                        ddlmaintask.DataSource = list;
                        ddlmaintask.DataBind();
                        ddlmaintask.SelectedValue = BeforeAddRowClickdList[i].Maintask;
                        //task.Maintask = ddlmaintask.SelectedValue;

                        TextBox txt1 = (lvAddSubtask.Items[i]).FindControl("txtaddsubtask") as TextBox;
                        txt1.Text = BeforeAddRowClickdList[i].Subtask;
                        DropDownList ddltasktype = (lvAddSubtask.Items[i]).FindControl("ddladdtasktype") as DropDownList;
                        ddltasktype.SelectedValue = BeforeAddRowClickdList[i].Tasktype;
                        //DropDownList ddldepencies = (GVAdd.Rows[i]).FindControl("ddladddependencies") as DropDownList;
                        //ddldepencies.SelectedValue = BeforeAddRowClickdList[i].Dependencies;
                        DropDownList ddlrequest = (lvAddSubtask.Items[i]).FindControl("ddladdrequest") as DropDownList;
                        ddlrequest.SelectedValue = BeforeAddRowClickdList[i].Request;
                        TextBox txt3 = (lvAddSubtask.Items[i]).FindControl("txtmanualentry") as TextBox;
                        txt3.Text = BeforeAddRowClickdList[i].ManualEntryRemark;
                        TextBox txt2 = (lvAddSubtask.Items[i]).FindControl("txtaddestimatedeffortsub") as TextBox;
                        txt2.Text = BeforeAddRowClickdList[i].Estimatedeffortsub;
                        DropDownList ddlassignedto = (lvAddSubtask.Items[i]).FindControl("ddladdassignedto") as DropDownList;
                        ddlassignedto.SelectedValue = BeforeAddRowClickdList[i].Assignedto;
                        DropDownList ddlstatus = (lvAddSubtask.Items[i]).FindControl("ddladdstatus") as DropDownList;
                        ddlstatus.SelectedValue = BeforeAddRowClickdList[i].SubtaskStatus;
                    }
                }
            }
        }
        public void SendEmail(string empID, string empname, string email, string password, string task, string teamleader)
        {
            try
            {
                WService ws = new WService();
                string AppPass = HelperClass.AppPassword;
                ws.From = HelperClass.Email_ID + ";" + HelperClass.EncodePasswordToBase64(AppPass);
                ws.To = email;
                ws.Subject = "Amit - Task Details";
                ws.MsgBody = "Hello " + empname + ", <br/><br/>" + teamleader + " assigned task " + task + " to you. " + "<a href =\"https://localhost:44313/Login.aspx\"> Click Here To Login</a>" + "<br>" + "<br>Thank you.";
                DBAccess.DBAccess.InsertEmailDetails(ws);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("SendEmail: " + ex.Message);
            }
        }
        protected void ddladdprojectid_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            string list2 = "";
            List<string> list3 = new List<string>();

            Subtaskdetails task = new Subtaskdetails();
            try
            {
                for (int i = 0; i < lvAddSubtask.Items.Count; i++)
                {
                    DropDownList ddl = ((lvAddSubtask.Items[i]).FindControl("ddladdmaintask") as DropDownList);
                    DropDownList ddl2 = ((lvAddSubtask.Items[i]).FindControl("ddladdassignedto") as DropDownList);
                    task.Projectid = ((lvAddSubtask.Items[i]).FindControl("ddladdprojectid") as DropDownList).SelectedValue;
                    List<Subtaskdetails> sublist1 = new List<Subtaskdetails>();
                    sublist1 = DBAccess.DBAccess.GetMaintaskValues(task);
                    list2 = DBAccess.DBAccess.GetAssignedValues(task);
                    list3 = list2.Split(',').ToList();
                    ddl.DataSource = list;
                    ddl.DataBind();
                    ddl.Items.Insert(0, new ListItem("Select", ""));
                    ddl2.DataSource = list3;
                    ddl2.DataBind();
                    ddl2.Items.Insert(0, new ListItem("Select", ""));
                    HelperClass.OpenModal(this, "newsubtask", false);
                }
            }
            catch (Exception ex)
            {
            }
        }
        protected void lbtnDashboardview_Click(object sender, EventArgs e)
        {
            try
            {
                Selecteddashboardsubtask();
            }
            catch (Exception ex)
            {
                
            }
        }
        public void Selecteddashboardsubtask()
        {
            List<Subtaskdetails> list = new List<Subtaskdetails>();
            Subtaskdetails subtask = new Subtaskdetails();
            try
            {
                subtask.Weekno = ddlweekno.SelectedValue;
                subtask.Weekno1 = ddlweekno2.SelectedValue;
                string ProjectID = "";
                for (int i = 0; i < lbprojectid.Items.Count; i++)
                {
                    if (lbprojectid.Items[i].Selected == true)
                    {
                        if (ProjectID == "")
                            ProjectID = "'" + lbprojectid.Items[i].Value + "'";
                        else
                            ProjectID += ",'" + lbprojectid.Items[i].Value + "'";
                    }
                }
                subtask.Projectid = ProjectID;

                string Employee = "";
                for (int i = 0; i < lbxemployee.Items.Count; i++)
                {
                    if (lbxemployee.Items[i].Selected == true)
                    {
                        if (Employee == "")
                            Employee = "'" + lbxemployee.Items[i].Value + "'";
                        else
                            Employee += ",'" + lbxemployee.Items[i].Value + "'";
                    }
                }
                subtask.Employees = Employee;

                string Customer = "";
                for (int i = 0; i < lbxcustomer.Items.Count; i++)
                {
                    if (lbxcustomer.Items[i].Selected == true)
                    {
                        if (Customer == "")
                            Customer = "'" + lbxcustomer.Items[i].Value + "'";
                        else
                            Customer += ",'" + lbxcustomer.Items[i].Value + "'";
                    }
                }
                subtask.Customer = Customer;


                subtask.Year = DateTime.Now.Year.ToString();
                subtask.Param = "DashboardView";
                DataTable dtDropD1 = new DataTable();
                DataTable dtDropD2 = new DataTable();
                DataTable dtArray = DBAccess.DBAccess.GetSubtaskentry(subtask, out dtDropD1, out dtDropD2);
                for (int i = 0; i < dtDropD2.Rows.Count; i++)
                {
                    Subtaskdetails sub = new Subtaskdetails();
                    sub.Weekno = dtDropD2.Rows[i]["WeekNo"].ToString();
                    sub.Employees = dtDropD2.Rows[i]["AssignedTo"].ToString();
                    sub.Projectid = dtDropD2.Rows[i]["ProjectID"].ToString();
                    sub.Maintask = dtDropD2.Rows[i]["Maintask"].ToString();
                    sub.Subtask = dtDropD2.Rows[i]["Subtask"].ToString();
                    sub.Tasktype = dtDropD2.Rows[i]["Tasktype"].ToString();
                    sub.ManualEntryRemark = dtDropD2.Rows[i]["ManualEntryRemarks"].ToString();
                    sub.Estimatedeffortsub = dtDropD2.Rows[i]["EstimatedEffort"].ToString();
                    //subtask.RemarksEngineer= dtDropD2.Rows[i][""].ToString();
                    //subtask.ViewMOM= dtDropD2.Rows[i][""].ToString();
                    sub.SubtaskStatus = dtDropD2.Rows[i]["Status"].ToString();
                    sub.MaintaskStatus = dtDropD2.Rows[i]["MainTaskStatus"].ToString();
                    sub.ProjectStatus = dtDropD2.Rows[i]["DispatchStatus"].ToString();
                    if (list.Count == 0)
                    {
                        sub.HeaderVisibility = true;
                    }
                    list.Add(sub);
                }
                lvdashboardview.DataSource = list;
                lvdashboardview.DataBind();
                lvSubtask.Visible = false;
                lvdashboardview.Visible = true;
                btnAdd.Visible = false;
                lbemployee.Visible = true;
                lbxemployee.Visible = true;

                lbxteam.Visible = false;
                lbteam.Visible = false;
                
                lbcustomer.Visible = true;
                lbxcustomer.Visible = true;

                btndashboardview.Visible = true;
                btnview.Visible = false;
                ddlweekno2.Visible = true;
                lbweekno2.Visible = true;
            }
            catch (Exception e)
            {
                Logger.WriteErrorLog("Selecteddashboardsubtask" + e.Message);
            }
        }
        protected void btndashboardview_Click(object sender, EventArgs e)
        {
            try
            {
                Selecteddashboardsubtask();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btndashboardview_Click: " + ex.Message);
            }
        }
        protected void lbtnplanedview_Click(object sender, EventArgs e)
        {
            try
            {
                Selectedsubtask();
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnhdSave_Click(object sender, EventArgs e)
        {
            try
            {
                Subtaskdetails subtask = new Subtaskdetails();
                //subtask.Projectid = txtsubtaskprojectid.Text;
                int rowIndex = Convert.ToInt32(hdnrowindex.Value);
                subtask.Weekno = ((lvSubtask.Items[rowIndex]).FindControl("hdnweekno") as HiddenField).Value;
                subtask.Year = ((lvSubtask.Items[rowIndex]).FindControl("hdnyear") as HiddenField).Value;
                subtask.Id = ((lvSubtask.Items[rowIndex]).FindControl("hdnid") as HiddenField).Value;
                subtask.MainTaskIDD = ((lvSubtask.Items[rowIndex]).FindControl("hdnaddmaintaskidd") as HiddenField).Value;
                subtask.Tasktype = (lvSubtask.Items[rowIndex].FindControl("lbtasktype") as Label).Text;
                subtask.Request = ((lvSubtask.Items[rowIndex]).FindControl("hdnproblemtype") as HiddenField).Value;
                //subtask.MainTaskEstimatedeffort = ((GVView.Rows[rowIndex]).FindControl("hdnmaintaskestimatedeffort") as HiddenField).Value;
                subtask.Projectid = ((lvSubtask.Items[rowIndex]).FindControl("lbsubProjectid") as Label).Text;
                subtask.Maintask = ((lvSubtask.Items[rowIndex]).FindControl("lbsubMainTask") as Label).Text;
                subtask.Estimatedeffortsub = ((lvSubtask.Items[rowIndex]).FindControl("txtestimatedeffortsub") as TextBox).Text;
                subtask.Subtask = ((lvSubtask.Items[rowIndex]).FindControl("lbsubtask") as Label).Text;
                //subtask.Request= ((GVView.Rows[rowIndex]).FindControl("lbrequest") as Label).Text;
                subtask.DeliveryDate = ((lvSubtask.Items[rowIndex]).FindControl("hdndeliverydate") as HiddenField).Value;
                subtask.ManualEntryRemark = ((lvSubtask.Items[rowIndex]).FindControl("lbmanualentryremark") as Label).Text;
                subtask.Assignedto = ((lvSubtask.Items[rowIndex]).FindControl("lbAssignedto") as Label).Text;
                subtask.SubtaskStatus = ((lvSubtask.Items[rowIndex]).FindControl("lbstatus") as Label).Text;
                int Result = DBAccess.DBAccess.InsertTaskDetailsGridview(subtask, "Update_SubTaskDetails");
                if (Result > 0)
                {
                    HelperClass.OpenSuccessToaster(this, "Estimated Effort Updated successfully!");
                }
                else
                {
                    HelperClass.OpenErrorModal(this, "Error, While Updating Records.");
                }
                //SelectedsubTask1();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
        }
        //protected void rblMeasurementSystem_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (rblMeasurementSystem.SelectedValue == "PlannerView")
        //        {
        //            Selectedsubtask();
        //        }
        //        else if (rblMeasurementSystem.SelectedValue == "DashboaredView")
        //        {
        //            Selecteddashboardsubtask();
        //        }
        //    }
        //    catch(Exception ex)
        //    {

        //    }
        //}


    }
}