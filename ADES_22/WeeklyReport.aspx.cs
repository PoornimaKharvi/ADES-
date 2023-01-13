using ADES_22.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace ADES_22
{
    public partial class WeeklyReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindListview();
                //BindManager();
            }
        }    
        private void BindListview()
        {
            try
            {
                string planner = Session["username"].ToString();
                string year = DateTime.Now.Year.ToString();
                List<WeeklyTaskReport> weeklyTaskReports = new List<WeeklyTaskReport>();
                WeeklyTaskReport report = new WeeklyTaskReport();
                report.EmployeeID = planner;
                report.Weekno = DBAccess.DBAccess.GetWeekNumbers(DateTime.Now.Date.ToString());
                report.Year = year;               
               
                DataTable dtDropD1 = new DataTable();
                DataTable dtDropD2 = new DataTable();
                DataTable dtDropD3 = new DataTable();
                DataTable dtDropD4 = new DataTable();

                DataTable dtArray = DBAccess.DBAccess.GetWeeklyReportq1(report.EmployeeID, report.Weekno, report.Year, out dtDropD1, out dtDropD2, out dtDropD3,out dtDropD4);
                
                for(int i=0;i< dtDropD1.Rows.Count;i++)
                {
                    report = new WeeklyTaskReport();
                    report.Weekno = dtDropD1.Rows[i]["WeekNo"].ToString();
                    report.Year= dtDropD1.Rows[i]["YearNo"].ToString();
                    report.TeamSize = dtDropD1.Rows[i]["TeamSize"].ToString();
                    report.AvailableHours = dtDropD1.Rows[i]["AvailableHours"].ToString();
                    report.PlannedHours = dtDropD1.Rows[i]["PlannedHours"].ToString();
                    report.PToA = dtDropD1.Rows[i]["PtoA"].ToString();
                    report.PlannedTask = dtDropD1.Rows[i]["PlannedTasks"].ToString();
                    report.TaskTakenPerPlan = dtDropD1.Rows[i]["TasksTakenUpAsPerPlan"].ToString();
                    report.AdherenceToPlan = dtDropD1.Rows[i]["AdherenceToPlan"].ToString();
                    report.UToP = dtDropD1.Rows[i]["UtoP"].ToString();
                    report.UtilizedHours = dtDropD1.Rows[i]["UtilizedHours"].ToString();
                    report.TaskNotPlannedButTakenUp = dtDropD1.Rows[i]["TasksNotPlannedButTakenUp"].ToString();              
                    report.WeekNumberText = "Current Week Number";

                    if (dtDropD3.DataSet == null)
                    {
                        report.SkippedTask = dtDropD2.AsEnumerable().Select(x => x.Field<string>("SkippedTasks")).FirstOrDefault();
                        report.TaskStatus = dtDropD2.AsEnumerable().Select(x => x.Field<string>("TaskStatus")).FirstOrDefault();
                        report.ProductionSupport = dtDropD2.AsEnumerable().Select(x => x.Field<int>("ProductionSupport")).FirstOrDefault();
                        if (weeklyTaskReports.Count != 0)
                        { 
                            report.Dependencies = dtDropD2.AsEnumerable().Select(x => x.Field<string>("Dependencies")).FirstOrDefault();
                        report.MajorTask = dtDropD2.AsEnumerable().Select(x => x.Field<string>("MajarTasks")).FirstOrDefault();
                        }
                        
                    }
                    if (dtDropD3.DataSet != null)
                    {
                        var task = String.Join(",", dtDropD2.AsEnumerable().Select(x => x.Field<string>("Task").ToString()).ToArray());
                        report.SkippedTask = task.ToString();
                        var taskstatus = String.Join(",", dtDropD3.AsEnumerable().Select(x => x.Field<string>("Task").ToString()).ToArray());
                        report.TaskStatus = taskstatus.ToString();
                        report.ProductionSupport = dtDropD4.AsEnumerable().Select(x => x.Field<int>("ProductionSupport")).FirstOrDefault();
                    }
                    if (weeklyTaskReports.Count == 0)
                    {
                        report.HeaderVisibility = true;
                        report.WeekNumberText = "Last Week Number";
                        //report.SkippedTaskLabel = true;
                        report.SkippedTaskTextBox = false;
                    }
                    report.Planner = planner;
                    weeklyTaskReports.Add(report);
                }

                lvReport.DataSource = weeklyTaskReports;
                lvReport.DataBind();
            }
            catch (Exception e)
             {
            Logger.WriteErrorLog("BindListview" + e.Message);
             }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            WeeklyTaskReport report = new WeeklyTaskReport();
            try
            {                
                int rowcount=lvReport.Items.Count();
                for(int i=0;i<rowcount;i++)
                {                    
                    if(((HiddenField)lvReport.Items[i].FindControl("hdncheckweek")).Value== "Last Week Number")
                    {
                        Session["Planner"]= ((Label)lvReport.Items[i].FindControl("lbPlanner")).Text;
                        Session["SkippedTask"]= ((Label)lvReport.Items[i].FindControl("lbskippedtask")).Text;
                        Session["TaskStatus"]= ((Label)lvReport.Items[i].FindControl("lbtaskstatus")).Text;
                        //Session["ProductionSupport"]= ((Label)lvReport.Items[i].FindControl("lbproductionsupport")).Text;
                        continue;
                    }                    
                    report.Year = ((HiddenField)lvReport.Items[i].FindControl("hdnyearno")).Value;
                    report.Weekno= ((HiddenField)lvReport.Items[i].FindControl("hdnweekno")).Value;
                    report.EmployeeID = Session["Planner"].ToString();
                    report.Dependencies = ((TextBox)lvReport.Items[i].FindControl("txtDependencies")).Text;
                    report.MajorTask = ((TextBox)lvReport.Items[i].FindControl("txtMajorTask")).Text;
                    //report.UpdatedTask = ((TextBox)lvReport.Items[i].FindControl("txtUpdatedTask")).Text;
                    //report.ProductionSupport = Session["ProductionSupport"].ToString();
                    report.SkippedTask = Session["SkippedTask"].ToString();
                    report.TaskStatus = Session["TaskStatus"].ToString();
                    int Result=DBAccess.DBAccess.InsertWeeklyTaskReport(report, "Save");
                }
                BindListview();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnSave_Click" + ex.Message);
            }
        }

        //public void BindManager()
        //{
        //    List<String> Managers = new List<string>();
        //    try
        //    {
        //        ddlManager.DataSource=DBAccess.DBAccess.GetManageranmes("Team Leader");
        //        ddlManager.DataBind();
        //        ddlManager.Items.Insert(0, new ListItem("Select", ""));
        //    }
        //    catch(Exception ex)
        //    {
        //        Logger.WriteErrorLog("BindManager" + ex.Message);
        //    }
        //}
    }           
}