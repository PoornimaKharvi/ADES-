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
    public partial class Subtask1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetCustomername();
                BindWeekNumbers();
                BindAssignedTo();
                Session["TaskType"] = DBAccess.DBAccess.GetTaskType();
                Session["Dependencies"] = DBAccess.DBAccess.GetDependencies();
                Session["Request"] = DBAccess.DBAccess.GetRequest();
                ddlweekno.SelectedValue = DBAccess.DBAccess.GetWeekNumbers(DateTime.Now.Date.ToString());
                ddlweekno2.SelectedValue = DBAccess.DBAccess.GetWeekNumbers(DateTime.Now.Date.ToString());
                rbtnViewChange.SelectedValue = "Planner View";
                for (int i = 0; i < lbxemployee.Items.Count; i++)
                {
                    lbxemployee.Items[i].Selected = true;
                }
                for (int i = 0; i < lbprojectid.Items.Count; i++)
                {
                    lbprojectid.Items[i].Selected = true;
                }
                Selectedsubtask();

            }
        }
        public void BindProjectId(DropDownList ddl)
        {
            try
            {
                List<string> projectid = DBAccess.DBAccess.GetProjectIDList();
                ddl.DataSource = projectid;
                ddl.DataBind();
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
                DataTable employee = new DataTable();
                DataTable projectid = new DataTable();
                string planner = Session["username"].ToString();
                DataTable dt = DBAccess.DBAccess.GetEmployeeReportTo(planner, out employee, out projectid);
                lbxemployee.DataSource = employee.AsEnumerable().Select(x => x.Field<string>("Employeeid")).ToList();
                lbxemployee.DataBind();
                lbprojectid.DataSource = projectid.AsEnumerable().Select(x => x.Field<string>("ProjectID")).Distinct().ToList(); ;
                lbprojectid.DataBind();

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
                subtask.Weekno1 = ddlweekno2.SelectedValue;
                subtask.Weekno = ddlweekno.SelectedValue;
                if (rbtnViewChange.SelectedValue == "Planner View")
                {
                    subtask.Param = "PlannerView";
                    DataTable RemarkAndViewMOM = new DataTable();
                    DataTable dtDropD2 = new DataTable();
                    DataTable dt = DBAccess.DBAccess.GetSubtaskentry(subtask, out RemarkAndViewMOM, out dtDropD2);
                    DataTable employee = new DataTable();
                    DataTable projectid = new DataTable();
                    DataTable dt1 = DBAccess.DBAccess.GetEmployeeReportTo(Session["username"].ToString(), out employee, out projectid);
                    int listcountFlag = 0;
                    if (dtDropD2.Rows.Count > 0)
                    {

                        for (int i = 0; i < dtDropD2.Rows.Count; i++)
                        {
                            Subtaskdetails sub = new Subtaskdetails();
                            sub.Weekno = dtDropD2.Rows[i]["WeekNo"].ToString();
                            sub.Assignedto = dtDropD2.Rows[i]["AssignedTo"].ToString();
                            sub.Projectid = dtDropD2.Rows[i]["ProjectID"].ToString();
                            sub.Maintask = dtDropD2.Rows[i]["Maintask"].ToString();
                            sub.Subtask = dtDropD2.Rows[i]["Subtask"].ToString();
                            sub.Tasktype = dtDropD2.Rows[i]["Tasktype"].ToString();
                            sub.ManualEntryRemark = dtDropD2.Rows[i]["ManualEntryRemarks"].ToString();
                            sub.Estimatedeffortsub = dtDropD2.Rows[i]["EstimatedEffort"].ToString().Replace('.', ':');
                            sub.Id = dtDropD2.Rows[i]["IDD"].ToString();
                            sub.Request = dtDropD2.Rows[i]["ProblemType"].ToString();
                            sub.Dependencies = dtDropD2.Rows[i]["Dependency"].ToString();
                            sub.Year = dtDropD2.Rows[i]["YearNo"].ToString();
                            sub.MainTaskIDD = dtDropD2.Rows[i]["MainTaskIDD"].ToString();
                            sub.DeliveryDate = Convert.ToDateTime(dtDropD2.Rows[i]["DeliveryDate"]).ToString("dd-MM-yyyy");
                            sub.ProblemID = dtDropD2.Rows[i]["ProblemID"].ToString();
                            sub.SubtaskStatus = dtDropD2.Rows[i]["Status"].ToString();
                            sub.EmpList = employee.AsEnumerable().Select(x => x.Field<string>("Employeeid")).ToList();
                            sub.TaskTypelist = (List<string>)Session["TaskType"];
                            sub.Dependencylist = (List<string>)Session["Dependencies"];
                            sub.Requestlist = (List<string>)Session["Request"];
                            list.Add(sub);
                        }
                    }
                    else
                    {
                        listcountFlag = 1;
                        list.Add(new Subtaskdetails());
                    }

                    GVView.Visible = true;
                    GVView.DataSource = list;
                    GVView.DataBind();
                    GVView.Enabled = true;

                    if (listcountFlag == 1)
                    {
                        GVView.Rows[0].Visible = false;
                    }
                   
                    lvdashboardview.Visible = false;
                    btnSaveRow.Visible = true;

                    lbxteam.Visible = false;
                    lbteam.Visible = false;

                    lbcustomer.Visible = false;
                    lbxcustomer.Visible = false;

                    ddlweekno2.Visible = false;
                    lbweekno2.Visible = false;

                }
                else if (rbtnViewChange.SelectedValue == "Dashboard View")
                {
                    subtask.Param = "DashboardView";
                    DataTable RemarkAndViewMOM = new DataTable();
                    DataTable dtDropD2 = new DataTable();

                    DataTable dtArray = DBAccess.DBAccess.GetSubtaskentry(subtask, out RemarkAndViewMOM, out dtDropD2);

                    for (int i = 0; i < dtDropD2.Rows.Count; i++)
                    {
                        Subtaskdetails sub = new Subtaskdetails();
                        sub.Weekno = dtDropD2.Rows[i]["WeekNo"].ToString();
                        int weekno = Convert.ToInt32(sub.Weekno);
                        sub.Employees = dtDropD2.Rows[i]["AssignedTo"].ToString();
                        var table1 = RemarkAndViewMOM.AsEnumerable().Where(x => x.Field<int>("WeekNo") == weekno && x.Field<string>("AssignedTo") == sub.Employees).Select(x => x.Field<string>("Remarks")).ToList();
                        sub.RemarksEngineer = String.Join(",", table1.Where(x => !String.IsNullOrEmpty(x)));
                                DataTable dtfiledetails = new DataTable();
                                var filedetail = RemarkAndViewMOM.AsEnumerable().Where(x => x.Field<int>("WeekNo") == weekno && x.Field<string>("AssignedTo") == sub.Employees && !String.IsNullOrEmpty(x.Field<string>("FileName"))).ToList();
                                Session["filenameDownload"] = filedetail;
                               if (filedetail.Count > 0)
                                {
                                    dtfiledetails = filedetail.CopyToDataTable();
                                }
                                foreach (DataRow row in dtfiledetails.Rows)
                                {
                                    FileDetails filedata = new FileDetails();
                                        filedata.FileName = row["FileName"].ToString();
                                    if (row["FileData"].ToString() == "")
                                    {
                                        filedata.FileInBase64 = "";
                                    }
                                    else
                                    {
                                        byte[] bytes = (byte[])row["FileData"];
                                        filedata.FileInBase64 = Convert.ToBase64String(bytes);
                                        filedata.Fileinbyte = bytes;
                                    }
                                    sub.fileDetails.Add(filedata);
                                    //Session["filenameDownload"] = sub.fileDetails;
                                }
                      
                        sub.Projectid = dtDropD2.Rows[i]["ProjectID"].ToString();
                        sub.Maintask = dtDropD2.Rows[i]["Maintask"].ToString();
                        sub.Subtask = dtDropD2.Rows[i]["Subtask"].ToString();
                        sub.Tasktype = dtDropD2.Rows[i]["Tasktype"].ToString();
                        sub.ManualEntryRemark = dtDropD2.Rows[i]["ManualEntryRemarks"].ToString();
                        sub.Estimatedeffortsub = dtDropD2.Rows[i]["EstimatedEffort"].ToString().Replace('.', ':');
                        sub.Request = dtDropD2.Rows[i]["ProblemType"].ToString();
                        sub.Dependencies = dtDropD2.Rows[i]["Dependency"].ToString();
                        sub.DeliveryDate = dtDropD2.Rows[i]["DeliveryDate"] == DBNull.Value ? "" : Convert.ToDateTime(dtDropD2.Rows[i]["DeliveryDate"]).ToString("dd-MM-yyyy");
                        sub.SubtaskStatus = dtDropD2.Rows[i]["Status"].ToString();
                        sub.MaintaskStatus = dtDropD2.Rows[i]["MainTaskStatus"].ToString();
                        sub.ProjectStatus = dtDropD2.Rows[i]["DispatchStatus"].ToString();
                        if (list.Count == 0)
                        {
                            sub.HeaderVisibility = true;
                        }
                        list.Add(sub);
                    }

                    lvdashboardview.Visible = true;
                    lvdashboardview.DataSource = list;
                    lvdashboardview.DataBind();
                    //lvSubtask.Visible = false;
                    GVView.Visible = false;
                    lvdashboardview.Visible = true;
                    btnSaveRow.Visible = false;
                    lbemployee.Visible = true;
                    lbxemployee.Visible = true;

                    lbxteam.Visible = false;
                    lbteam.Visible = false;

                    lbcustomer.Visible = true;
                    lbxcustomer.Visible = true;

                    ddlweekno2.Visible = true;
                    lbweekno2.Visible = true;
                }
            }

            catch (Exception e)
            {
                Logger.WriteErrorLog("Selectedsubtask" + e.Message);
            }
        }
        protected void GVView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddl = new DropDownList();
                    string hdnvalue = "";
                    ddl = (e.Row.FindControl("ddlPlanEmployee") as DropDownList);
                    if (ddl != null)
                    {
                        hdnvalue = (e.Row.FindControl("hdnplanemployee") as HiddenField).Value;
                        if (hdnvalue != "")
                        {

                            ddl.SelectedValue = hdnvalue;

                        }
                        ddl.Items.Insert(0, new ListItem("None", ""));
                    }

                    ddl = (e.Row.FindControl("ddlplanprojectid") as DropDownList);
                    if (ddl != null)
                    {
                        string employee = (e.Row.FindControl("ddlPlanEmployee") as DropDownList).SelectedValue;
                        hdnvalue = (e.Row.FindControl("hdnplanprojectid") as HiddenField).Value;
                        ddl.DataSource = DBAccess.DBAccess.GetfootProjectID(employee);
                        ddl.DataBind();
                        if (hdnvalue != "")
                        {
                            if (ddl.Items.FindByValue(hdnvalue) != null)
                            {
                                ddl.SelectedValue = hdnvalue;
                            }
                        }
                    }
                    ddl = (e.Row.FindControl("ddlplanmaintask") as DropDownList);
                    if (ddl != null)
                    {
                        Subtaskdetails task = new Subtaskdetails();
                        task.Projectid = (e.Row.FindControl("ddlplanprojectid") as DropDownList).SelectedValue;
                        hdnvalue = (e.Row.FindControl("hdnplanmaintask") as HiddenField).Value;
                        string hdnvalue1 = (e.Row.FindControl("hdnmaintaskidd") as HiddenField).Value;
                        ddl.DataSource = DBAccess.DBAccess.GetMaintaskValues(task);
                        //ddl.DataSource = list;
                        ddl.DataTextField = "MainTaskIDD";
                        ddl.DataValueField = "Maintask";
                        ddl.DataBind();
                        //if (hdnvalue != "")
                        //{
                        //    if (ddl.Items.FindByText(hdnvalue) != null)
                        //    {
                        //        ddl.SelectedValue = hdnvalue;
                        //    }
                        //}
                        if (hdnvalue1 != "")
                        {
                            if (ddl.Items.FindByValue(hdnvalue1) != null)
                            {
                                ddl.SelectedValue = hdnvalue1;
                            }
                        }
                    }

                    ddl = (e.Row.FindControl("ddlplanTasktype") as DropDownList);
                    if (ddl != null)
                    {
                        hdnvalue = (e.Row.FindControl("hdnplantasktype") as HiddenField).Value;
                        if (hdnvalue != null)
                        {
                            if (ddl.Items.FindByValue(hdnvalue) != null)
                            {
                                ddl.SelectedValue = hdnvalue;
                            }
                        }
                    }

                    ddl = (e.Row.FindControl("ddlplandependancy") as DropDownList);
                    if (ddl != null)
                    {
                        hdnvalue = (e.Row.FindControl("hdnplandependancy") as HiddenField).Value;
                        if (hdnvalue != null)
                        {
                            if (ddl.Items.FindByValue(hdnvalue) != null)
                            {
                                ddl.SelectedValue = hdnvalue;
                            }
                        }

                    }


                    ddl = (e.Row.FindControl("ddlplanrequest") as DropDownList);
                    if (ddl != null)
                    {
                        hdnvalue = (e.Row.FindControl("hdnplanrequest") as HiddenField).Value;
                        if (hdnvalue != null)
                        {
                            if (ddl.Items.FindByValue(hdnvalue) != null)
                            {
                                ddl.SelectedValue = hdnvalue;
                            }
                        }
                    }


                }

                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    DropDownList ddl1 = (DropDownList)e.Row.FindControl("ddlfootEmployee");
                    if (ddl1 != null)
                    {
                        string planner = Session["username"].ToString();
                        DataTable employee = new DataTable();
                        DataTable projectid = new DataTable();
                        DataTable dt1 = DBAccess.DBAccess.GetEmployeeReportTo(planner, out employee, out projectid);

                        ddl1.DataSource = employee.AsEnumerable().Select(x => x.Field<string>("Employeeid")).ToList();
                        ddl1.DataBind();
                        ddl1.Items.Insert(0, new ListItem("None", ""));
                    }
                    ddlfootEmployee_SelectedIndexChanged(null, null);
                    ddlfootProjectID_SelectedIndexChanged(null, null);
                    DropDownList ddl2 = (DropDownList)e.Row.FindControl("footTaskType");
                    if (ddl2 != null)
                    {
                        ddl2.DataSource = (List<string>)Session["TaskType"];
                        ddl2.DataBind();
                        ddl2.Items.Insert(0, new ListItem("None", ""));
                    }
                    DropDownList ddl3 = (DropDownList)e.Row.FindControl("footDependancy");
                    if (ddl3 != null)
                    {
                        ddl3.DataSource = (List<string>)Session["Dependencies"];
                        ddl3.DataBind();
                        ddl3.Items.Insert(0, new ListItem("None", ""));
                    }
                    DropDownList ddl4 = (DropDownList)e.Row.FindControl("footRequest");
                    if (ddl4 != null)
                    {
                        ddl4.DataSource = (List<string>)Session["Request"];
                        ddl4.DataBind();
                        ddl4.Items.Insert(0, new ListItem("None", ""));
                    }
                }

            }
            catch (Exception ex)
            {

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
        protected void saveConfirmYes_ServerClick(object sender, EventArgs e)
        {
            try
            {
                int DeleteRowIndex = (int)Session["DeleteRowIndex"];
                Subtaskdetails subtask = new Subtaskdetails();
                subtask.Id = (GVView.Rows[DeleteRowIndex].FindControl("hdnplanidd") as HiddenField).Value;
                string success = DBAccess.DBAccess.DeleteInsertSubTaskDetailsGridview(subtask, "Delete_SubTaskDetails");
                GVView.EditIndex = -1;
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
        protected void ddlfootEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    string employee = (GVView.FooterRow.FindControl("ddlfootEmployee") as DropDownList).SelectedValue;
                    DropDownList ddl = GVView.FooterRow.FindControl("ddlfootProjectID") as DropDownList;
                    List<string> list = DBAccess.DBAccess.GetfootProjectID(employee);
                    ddl.DataSource = list;
                    ddl.DataBind();
                    ddl.Items.Insert(0, new ListItem("None", ""));
                }
            }
            catch (Exception ex)
            {

            }

        }
        protected void ddlfootProjectID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender != null)
            {
                DataTable dt = new DataTable();
                Subtaskdetails task = new Subtaskdetails();
                List<Subtaskdetails> list = new List<Subtaskdetails>();
                task.Projectid = (GVView.FooterRow.FindControl("ddlfootProjectID") as DropDownList).SelectedValue;
                DropDownList ddl = GVView.FooterRow.FindControl("footMainTask") as DropDownList;
                DataTable dt1 = DBAccess.DBAccess.GetMaintaskValues1(task, out dt);
                list=DBAccess.DBAccess.GetMaintaskValues(task);
                ddl.DataSource = list;
                ddl.DataTextField = "MainTaskIDD";
                ddl.DataValueField =  "Maintask";
                //ddl.DataSource = dt1;
                //ddl.DataTextField = "MainTask";
                //ddl.DataValueField = "IDD";
                //List<string> list = DBAccess.DBAccess.GetMaintaskValues(task);
                //ddl.DataSource = list;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("None", ""));
            }
        }
        protected void GVView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("Save"))
                {
                    Subtaskdetails subtask = new Subtaskdetails();
                    int Result;
                    string currentdate = DateTime.Now.Date.ToString();
                    String list = "";
                    string list1 = "";
                    list = DBAccess.DBAccess.GetWeekNumbers(currentdate);
                    list1 = DBAccess.DBAccess.GetCurrentYear(currentdate);
                    subtask.Weekno = list;
                    subtask.Year = list1;
                    subtask.Assignedto = (GVView.FooterRow.FindControl("ddlfootEmployee") as DropDownList).SelectedValue.Trim();
                    subtask.Projectid = (GVView.FooterRow.FindControl("ddlfootProjectID") as DropDownList).SelectedValue.Trim();
                    subtask.Tasktype = (GVView.FooterRow.FindControl("footTaskType") as DropDownList).SelectedValue.Trim();
                    subtask.MainTaskIDD = (GVView.FooterRow.FindControl("footMainTask") as DropDownList).SelectedValue.Trim();
                    subtask.Maintask= (GVView.FooterRow.FindControl("footMainTask") as DropDownList).SelectedItem.Text;
                    subtask.SubtaskStatus = "Open";
                    subtask.Subtask = (GVView.FooterRow.FindControl("footSubTask") as TextBox).Text.Trim();
                    subtask.ManualEntryRemark = (GVView.FooterRow.FindControl("footManualEntry") as TextBox).Text.Trim();
                    subtask.Estimatedeffortsub = (GVView.FooterRow.FindControl("footEstimatedEffort") as TextBox).Text.Trim();
                    subtask.Dependencies = (GVView.FooterRow.FindControl("footDependancy") as DropDownList).SelectedValue.Trim();
                    subtask.Request = (GVView.FooterRow.FindControl("footRequest") as DropDownList).SelectedValue.Trim();
                    subtask.DeliveryDate = (GVView.FooterRow.FindControl("footDeliveryDate") as TextBox).Text.Trim();
                    Result = DBAccess.DBAccess.SaveSubTaskDetails(subtask, "Save_SubTaskDetails");
                    Session["EmployeeEmailandPassword"] = DBAccess.DBAccess.GetEmployeeemaildetails(subtask.Assignedto);
                    EmpDetails emp = new EmpDetails();
                    if (Session["EmployeeEmailandPassword"] != null)
                    {
                        emp = (EmpDetails)Session["EmployeeEmailandPassword"];
                    }
                    string teamleader = Session["Name"].ToString();
                    SendEmail(emp.EmpID, emp.EmpName, emp.Email, emp.Password, subtask.Subtask, teamleader);

                    if (Result > 0)
                    {
                        HelperClass.ClearModal(this);
                        HelperClass.OpenSuccessToaster(this, "SubTask Details saved successfully!");
                        Selectedsubtask();
                    }
                    else
                    {

                        HelperClass.OpenErrorModal(this, "Error, While Saving Records.");
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Session["DeleteRowIndex"] = e.RowIndex;
            HelperClass.openConfirmModal(this, "Are you sure, you want to delete this record?");
        }
        protected void rbtnViewChange_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Selectedsubtask();
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnSaveRow_Click1(object sender, EventArgs e)
        {
            try
            {
                int Result = 0;
                for (int i = 0; i < GVView.Rows.Count; i++)
                {
                    string updatedValue = ((GVView.Rows[i]).FindControl("hdnUpdate") as HiddenField).Value;
                    if (string.Equals("updated", updatedValue, StringComparison.OrdinalIgnoreCase))
                    {
                        Subtaskdetails subtask = new Subtaskdetails();
                        Taskdetails task = new Taskdetails();
                        subtask.Id = ((GVView.Rows[i]).FindControl("hdnplanidd") as HiddenField).Value;
                        subtask.Weekno = ((GVView.Rows[i]).FindControl("hdnplanweekno") as HiddenField).Value;
                        subtask.Year = ((GVView.Rows[i]).FindControl("hdnplanyear") as HiddenField).Value;
                        subtask.Assignedto = ((GVView.Rows[i]).FindControl("ddlPlanEmployee") as DropDownList).SelectedValue;
                        subtask.Projectid = ((GVView.Rows[i]).FindControl("ddlplanprojectid") as DropDownList).SelectedValue;
                        //subtask.Maintask = ((GVView.Rows[i]).FindControl("ddlplanmaintask") as DropDownList).SelectedValue;
                        subtask.MainTaskIDD = ((GVView.Rows[i]).FindControl("ddlplanmaintask") as DropDownList).SelectedValue;
                        subtask.Maintask = ((GVView.Rows[i]).FindControl("ddlplanmaintask") as DropDownList).SelectedItem.Text;
                        subtask.Tasktype = ((GVView.Rows[i]).FindControl("ddlplanTasktype") as DropDownList).SelectedValue;
                        //subtask.MainTaskIDD = hdnmaintaskidd.Value;                    
                        subtask.Subtask = ((GVView.Rows[i]).FindControl("lbSubTask") as TextBox).Text;
                        subtask.ManualEntryRemark = ((GVView.Rows[i]).FindControl("lbManualEntry") as TextBox).Text;
                        subtask.DeliveryDate = ((GVView.Rows[i]).FindControl("lbDeliveryDate") as TextBox).Text;
                        //HiddenField hdnmaintaskidd= (GVAdd.Rows[i]).FindControl("txtsubtask") as HiddenField;                    
                        subtask.Estimatedeffortsub = ((GVView.Rows[i]).FindControl("lbEstimatedEffort") as TextBox).Text;
                        subtask.Dependencies = ((GVView.Rows[i]).FindControl("ddlplandependancy") as DropDownList).SelectedValue;
                        subtask.Request = ((GVView.Rows[i]).FindControl("ddlplanrequest") as DropDownList).SelectedValue;
                        subtask.SubtaskStatus = ((GVView.Rows[i]).FindControl("hdnplanstatus") as HiddenField).Value;
                        if (Session["MainTaskDetails"] != null)
                        {
                            task = (Taskdetails)Session["MainTaskDetails"];
                        }
                        //if (subtask.Subtask == "" || subtask.Estimatedeffortsub == "" || subtask.Assignedto == "")
                        //{
                        //    continue;
                        //}
                        //if (Session["MainTaskDetails"] != null)
                        //{
                        //    task = (Taskdetails)Session["MainTaskDetails"];
                        //}
                        //subtask.MainTaskEstimatedeffort = task.Estimatedeffort;
                        //subtask.Tasktype = task.Tasktype;
                        //subtask.Request = task.Request;
                        //subtask.MainTaskIDD = task.Id;                    
                        //subtask.ProblemID = task.ProblemID;
                        Result = DBAccess.DBAccess.SaveSubTaskDetails(subtask, "Update_SubTaskDetails");
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
                        ((GVView.Rows[i]).FindControl("hdnUpdate") as HiddenField).Value = "";
                    }
                }
                if (Result > 0)
                {
                    HelperClass.ClearModal(this);
                    HelperClass.OpenSuccessToaster(this, "SubTask Details saved successfully!");
                    Selectedsubtask();
                }
                else
                {

                    HelperClass.OpenErrorModal(this, "Error, While Saving Records.");
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void ddlPlanEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int rowindex = (((sender as DropDownList).NamingContainer) as GridViewRow).RowIndex;
                string ddlemployee = (GVView.Rows[rowindex].FindControl("ddlPlanEmployee") as DropDownList).SelectedValue;
                DropDownList ddl = GVView.Rows[rowindex].FindControl("ddlplanprojectid") as DropDownList;
                List<string> list = DBAccess.DBAccess.GetfootProjectID(ddlemployee);
                ddl.DataSource = list;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("None", ""));
            }
            catch (Exception ex)
            {

            }
        }
        protected void ddlplanprojectid_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                Subtaskdetails task = new Subtaskdetails();
                int rowindex = (((sender as DropDownList).NamingContainer) as GridViewRow).RowIndex;
                task.Projectid = (GVView.Rows[rowindex].FindControl("ddlplanprojectid") as DropDownList).SelectedValue;
                DropDownList ddl = GVView.Rows[rowindex].FindControl("ddlplanmaintask") as DropDownList;
                DataTable dt1=DBAccess.DBAccess.GetMaintaskValues1(task,out dt);
                ddl.DataSource = dt1;
                ddl.DataTextField = "MainTask";
                ddl.DataValueField = "IDD";
                //List<string> list = DBAccess.DBAccess.GetMaintaskValues(task);
                //ddl.DataSource = list;
                //ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("None", ""));
            }
            catch (Exception ex)
            {

            }

        }
        protected void btnFileDownLoad_Click(object sender, EventArgs e)
        {
                try
                {
                ListViewItem item = ((sender as LinkButton).NamingContainer as ListViewItem);
                string val = (item.FindControl("hf") as HiddenField).Value;
                string filename = (item.FindControl("hdnfilename") as HiddenField).Value;
                byte[] fileinbyte = System.Convert.FromBase64String(val.Substring(val.LastIndexOf(',') + 1));
                //int index = ((sender as LinkButton).NamingContainer as ListViewItem).DataItemIndex;
                //List<FileDetails> subtaskfileinfo = new List<FileDetails>();
                //subtaskfileinfo = Session["filenameDownload"] as List<FileDetails>;
                    //byte[] bytes = subtaskfileinfo[index].Fileinbyte;
                    HttpResponse response = HttpContext.Current.Response;
                    response.Clear();
                    response.ClearContent();
                    response.ClearHeaders();
                    response.Buffer = true;
                    response.ContentType = "application/force-download";
                    response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
                    response.BinaryWrite(fileinbyte);
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