using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADES_22.Model;
using System.Globalization;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.Services;

namespace ADES_22
{
    public partial class Subtask : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetCustomername();                       
                CultureInfo ciCurr = CultureInfo.CurrentCulture;
                int weekNum = ciCurr.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                txtweekno.Text = weekNum.ToString();
                Selectedsubtask();
            }
        }
        public void BindTaskdetails()
        {
            try
            {
                List<Subtaskdetails> list = new List<Subtaskdetails>();
                Subtaskdetails subtask = new Subtaskdetails();
                Taskdetails taskdetails = new Taskdetails();
                if (Session["MainTaskDetails"] != null)
                {
                    taskdetails = (Taskdetails)Session["MainTaskDetails"];
                }
                //txtsubtaskprojectid.Text = taskdetails.Projectid;
                //txtyear.Text = taskdetails.Year;
                txtweekno.Text = taskdetails.Weekno;
                subtask.Year = taskdetails.Year;
                subtask.Weekno = taskdetails.Weekno;
                subtask.Projectid = taskdetails.Projectid;
                subtask.Maintask = taskdetails.Maintask;
                subtask.Assignedto = taskdetails.Assignedto;
                subtask.MainTaskIDD = taskdetails.Id;
                subtask.Param = "View_SubTaskDetails";
                Session["Subtsk"] = list = DBAccess.DBAccess.GetSubtaskentry(subtask);
                lvSubtask.DataSource = list;
                lvSubtask.DataBind();
            }
            catch (Exception e)
            {
                Logger.WriteErrorLog("BindTaskdetails" + e.Message);
            }
        }
        public void BindProjectID()
        {
            
            try
            {
                List<string> projectid = DBAccess.DBAccess.GetProjectIDList();
                ddleditprojectid.DataSource = projectid;
                ddleditprojectid.DataBind();
                ddleditprojectid.Items.Insert(0, new ListItem("Select", ""));
            }
            catch(Exception ex)
            {

            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            DateTime start = new DateTime((DateTime.Now.Year), 1, 1);
            if ((DateTime.IsLeapYear(start.Year) && start.ToString("dddd") == "Wednesday") || (start.ToString("dddd") == "Thursday"))
            {
                List<int> list1 = new List<int>();
                if (weekNum == 51)
                {
                    for (int i = -1; i < 3; i++)
                    {
                        list1.Add(weekNum + i);
                    }
                    ddladdweekno.DataSource = list1;
                    ddladdweekno.DataBind();
                    ddladdweekno.SelectedValue = weekNum.ToString();
                }
                else if (weekNum == 52)
                {
                    for (int i = -1; i < 2; i++)
                    {
                        list1.Add(weekNum + i);
                    }
                    ddladdweekno.DataSource = list1;
                    ddladdweekno.DataBind();
                    ddladdweekno.SelectedValue = weekNum.ToString();
                }
                else if (weekNum == 53)
                {
                    for (int i = -1; i < 1; i++)
                    {
                        list1.Add(weekNum + i);
                    }
                    ddladdweekno.DataSource = list1;
                    ddladdweekno.DataBind();
                    ddladdweekno.SelectedValue = weekNum.ToString();
                }
                else
                {
                    for (int i = -1; i < 4; i++)
                    {
                        list1.Add(weekNum + i);
                    }
                    ddladdweekno.DataSource = list1;
                    ddladdweekno.DataBind();
                    ddladdweekno.SelectedValue = weekNum.ToString();
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
                    ddladdweekno.DataSource = list;
                    ddladdweekno.DataBind();
                    ddladdweekno.SelectedValue = weekNum.ToString();
                }
                else if (weekNum == 51)
                {
                    for (int i = -1; i < 2; i++)
                    {
                        list.Add(weekNum + i);
                    }
                    ddladdweekno.DataSource = list;
                    ddladdweekno.DataBind();
                    ddladdweekno.SelectedValue = weekNum.ToString();
                }
                else if (weekNum == 52)
                {
                    for (int i = -1; i < 1; i++)
                    {
                        list.Add(weekNum + i);
                    }
                    ddladdweekno.DataSource = list;
                    ddladdweekno.DataBind();
                    ddladdweekno.SelectedValue = weekNum.ToString();
                }
                else
                {
                    for (int i = -1; i < 4; i++)
                    {
                        list.Add(weekNum + i);
                    }
                    ddladdweekno.DataSource = list;
                    ddladdweekno.DataBind();
                    ddladdweekno.SelectedValue = weekNum.ToString();
                }
            }
            lbyear.Text = DateTime.Now.Year.ToString();
            txtrows.Text = "5";
            string param = "Oldrow";
            BindGridview(param);
            HelperClass.OpenModal(this, "newsubtask", true);
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
            List<string> assignedvalues = DBAccess.DBAccess.GetAssignedValues();
            List<ListItem> taskTypeList = HelperClass.BindTaskType();
            List<ListItem> issueTypeList = HelperClass.BindIssueType();
            List<ListItem> statusvalues = HelperClass.GetMainTaskStatusValues();
            List<ListItem> dependencies = HelperClass.BindDependenciues();
            List<string> projectid = DBAccess.DBAccess.GetProjectIDList();
            List<string> list = new List<string>();
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
                List<Subtaskdetails> sublist = new List<Subtaskdetails>();
                sublist = DBAccess.DBAccess.GetMaintaskValues(task);
                ddl.DataSource = list;
                ddl.DataBind();
                HelperClass.OpenModal(this, "newsubtask", false);

                

                DropDownList ddl2 = (lvAddSubtask.Items[i]).FindControl("ddladdassignedto") as DropDownList;
                ddl2.DataSource = assignedvalues;
                ddl2.DataBind();
                ddl2.Items.Insert(0, new ListItem("Select", ""));

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
                        List<Subtaskdetails> sublist1 = new List<Subtaskdetails>();
                        sublist1 = DBAccess.DBAccess.GetMaintaskValues(task);
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
        public void Selectedsubtask()
        {
            List<Subtaskdetails> list = new List<Subtaskdetails>();
            Subtaskdetails subtask = new Subtaskdetails();
            try
            {
                subtask.Weekno = txtweekno.Text; 
                subtask.Param = "View_SubTaskDetails";
                Session["subtask"] = list = DBAccess.DBAccess.GetSubtaskentry(subtask);
                lvSubtask.DataSource = list;
                lvSubtask.DataBind();
            }
            catch (Exception e)
            {
                Logger.WriteErrorLog("Selectedsubtask" + e.Message);
            }
        }
        protected void lbEdit_Click(object sender, EventArgs e)
        {
            try
            {
                Subtaskdetails task = new Subtaskdetails();
                List<string> list = new List<string>();
                
                int rowindex = (((sender as LinkButton).NamingContainer) as ListViewItem).DataItemIndex;
                hdnweekno.Value= (lvSubtask.Items[rowindex].FindControl("hdnweekno") as HiddenField).Value;
                hdnyear.Value = (lvSubtask.Items[rowindex].FindControl("hdnyear") as HiddenField).Value;
                hdneditid.Value = (lvSubtask.Items[rowindex].FindControl("hdnid") as HiddenField).Value;
                hdneditmaintaskidd.Value = (lvSubtask.Items[rowindex].FindControl("hdnaddmaintaskidd") as HiddenField).Value;
                txteditsubtask.Text = ((lvSubtask.Items[rowindex]).FindControl("lbsubtask") as Label).Text;
                BindProjectID();
                string value = (lvSubtask.Items[rowindex].FindControl("lbsubProjectid") as Label).Text;
                if (ddleditprojectid.Items.FindByValue(value) != null)
                {
                    ddleditprojectid.SelectedValue = value;
                }
                task.Projectid = ((lvSubtask.Items[rowindex]).FindControl("lbsubProjectid") as Label).Text;
                List<Subtaskdetails> sublist = new List<Subtaskdetails>();
                sublist = DBAccess.DBAccess.GetMaintaskValues(task);
                ddleditmaintask.DataSource = list;
                ddleditmaintask.DataBind();
                value = (lvSubtask.Items[rowindex].FindControl("lbsubMainTask") as Label).Text;
                if (ddleditmaintask.Items.FindByValue(value) != null)
                {
                    ddleditmaintask.SelectedValue = value;
                }
                BindAssignedTo();
                BindTaskType();
                BindStatus();             
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
                value = (lvSubtask.Items[rowindex].FindControl("lbAssignedto") as Label).Text;
                if (ddleditassignedto.Items.FindByValue(value) != null)
                {
                    ddleditassignedto.SelectedValue = value;
                }
                value = (lvSubtask.Items[rowindex].FindControl("lbstatus") as Label).Text;
                if (ddleditstatus.Items.FindByValue(value) != null)
                {
                    ddleditstatus.SelectedValue = value;
                }
                txteditesimatedeffort.Text = ((lvSubtask.Items[rowindex]).FindControl("txtestimatedeffortsub") as TextBox).Text;
                txtedideliverydate.Text = ((lvSubtask.Items[rowindex]).FindControl("lbdeliverydate") as Label).Text;
                txteditmanualentry.Text = ((lvSubtask.Items[rowindex]).FindControl("lbmanualentryremark") as Label).Text;
                HelperClass.OpenModal(this, "Editsubtask", false);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("lbEdit_Click" + ex.Message);
            }
        }
        public void BindAssignedTo()
        {
            try
            {
                ddleditassignedto.DataSource = DBAccess.DBAccess.GetAssignedValues();
                ddleditassignedto.DataBind();
                ddleditassignedto.Items.Insert(0, new ListItem("Select", ""));
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindAssignedTo: " + ex.Message);
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
        public void BindStatus()
        {
            try
            {
                ddleditstatus.DataSource = HelperClass.GetMainTaskStatusValues();
                ddleditstatus.DataTextField = "Text";
                ddleditstatus.DataValueField = "Value";
                ddleditstatus.DataBind();
                ddleditstatus.Items.Insert(0, new ListItem("Select", ""));
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindStatus: " + ex.Message);
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
        protected void btnhdSave_Click(object sender, EventArgs e)
        {
            try
            {
                Subtaskdetails subtask = new Subtaskdetails();
                int rowIndex = Convert.ToInt32(hdnrowindex.Value);
                subtask.Weekno = ((lvSubtask.Items[rowIndex]).FindControl("hdnweekno") as HiddenField).Value;
                subtask.Year = ((lvSubtask.Items[rowIndex]).FindControl("hdnyear") as HiddenField).Value;
                subtask.Id = ((lvSubtask.Items[rowIndex]).FindControl("hdnid") as HiddenField).Value;
                subtask.MainTaskIDD = ((lvSubtask.Items[rowIndex]).FindControl("hdnaddmaintaskidd") as HiddenField).Value;
                subtask.Tasktype = ((lvSubtask.Items[rowIndex]).FindControl("hdntasktype") as HiddenField).Value;
                subtask.Request = ((lvSubtask.Items[rowIndex]).FindControl("hdnproblemtype") as HiddenField).Value;
                subtask.Projectid = ((lvSubtask.Items[rowIndex]).FindControl("lbsubProjectid") as Label).Text;
                subtask.Maintask = ((lvSubtask.Items[rowIndex]).FindControl("lbsubMainTask") as Label).Text;
                subtask.Estimatedeffortsub = ((lvSubtask.Items[rowIndex]).FindControl("txtestimatedeffortsub") as TextBox).Text;
                subtask.Subtask = ((lvSubtask.Items[rowIndex]).FindControl("lbsubtask") as Label).Text;
                subtask.DeliveryDate= ((lvSubtask.Items[rowIndex]).FindControl("lbdeliverydate") as Label).Text;
                subtask.ManualEntryRemark= ((lvSubtask.Items[rowIndex]).FindControl("lbmanualentryremark") as Label).Text;
                subtask.Assignedto = ((lvSubtask.Items[rowIndex]).FindControl("lbAssignedto") as Label).Text;
                subtask.SubtaskStatus= ((lvSubtask.Items[rowIndex]).FindControl("lbstatus") as Label).Text;
                int Result = DBAccess.DBAccess.InsertTaskDetailsGridview(subtask, "Update_SubTaskDetails");
                if (Result > 0)
                {
                    HelperClass.OpenSuccessToaster(this, "Estimated Effort Updated successfully!");
                }
                else
                {
                    HelperClass.OpenErrorModal(this, "Error, While Updating Records.");
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
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
                subtask.SubtaskStatus = ddleditstatus.SelectedValue;
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
        protected void GridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Session["DeleteRowIndex"] = e.RowIndex;
                HelperClass.openConfirmModal(this, "Are you sure, you want to delete this record?");
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GridView_RowDeleting" + ex.Message);
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
        public void btnTask_Click(object sender, EventArgs e)
        {
            Response.Redirect("Tasks.aspx", false);
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
                HelperClass.OpenModal(this, "newsubtask", true);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("lbAddnewrow_Click:" + ex.Message);
            }
        }
        protected void lbPrevweek_Click(object sender, EventArgs e)
        {
            try
            {
                int weekcount = Convert.ToInt32(txtweekno.Text) - 1;
                txtweekno.Text = weekcount.ToString();
                Selectedsubtask();
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
                Selectedsubtask();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("lbNextweek_Click" + ex.Message);
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
            Subtaskdetails task = new Subtaskdetails();
            try
            {
                for (int i = 0; i < lvAddSubtask.Items.Count; i++)
                {
                    DropDownList ddl = ((lvAddSubtask.Items[i]).FindControl("ddladdmaintask") as DropDownList);
                    task.Projectid = ((lvAddSubtask.Items[i]).FindControl("ddladdprojectid") as DropDownList).SelectedValue;
                    List<Subtaskdetails> sublist = new List<Subtaskdetails>();
                    sublist = DBAccess.DBAccess.GetMaintaskValues(task);
                    ddl.DataSource = list;
                    ddl.DataBind();
                    HelperClass.OpenModal(this, "newsubtask", true);
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void GetCustomername()
        {
            List<string> str = new List<string>();
            try
            {
                str = DBAccess.DBAccess.GetCustName("", "");
                ddlemployee.DataSource = str;
                ddlemployee.DataBind();
            }
            catch(Exception e)
            {

            }
        }
    }
}
