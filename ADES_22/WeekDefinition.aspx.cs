using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADES_22
{
    public partial class WeekDefinition : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((String)Session["login"] != "Yes" || (String)Session["login"] == null)
            {
                Response.Redirect("~/Login.aspx", false);
            }

            if (!IsPostBack)
            {
                txtYear.Text = System.DateTime.Now.Year.ToString();
                btnWeekView_Click(null, EventArgs.Empty);
            }
        }

        public int GetWeekNumber()
        {
            DateTime lastYearDate = Convert.ToDateTime(DateTime.Now.Year - 1 + "/12/31");
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(lastYearDate, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return weekNum;
        }

        protected void btnWeekGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                int year = Convert.ToInt32(txtYear.Text);
                int weekNo = 1;
                DateTime nextDay = new DateTime(year, 1, 1);
                DateTime lastDayOfYear = new DateTime(year, 12, 31);
                string startingDayOfweek = ddlStartingDayOfWeek.SelectedItem.Text;

                DBAccess.DBAccess.DeleteFromCalender(year);

                DataTable dtInsertInToCalender = new DataTable();
                dtInsertInToCalender.Columns.AddRange(new DataColumn[] { new DataColumn("WeekDate", typeof(DateTime)), new DataColumn("WeekNumber", typeof(int)), new DataColumn("MonthVal", typeof(int)), new DataColumn("YearNo", typeof(int)) });

                while (nextDay <= lastDayOfYear)
                {
                    DataRow dataRow = dtInsertInToCalender.NewRow();
                    dataRow["WeekDate"] = nextDay;

                    int weekCheck = weekNo - 1;
                    if (weekCheck == 0)
                    {
                        dataRow["WeekNumber"] = GetWeekNumber();
                    }
                    else
                    {
                        dataRow["WeekNumber"] = weekNo - 1;
                    }

                    dataRow["MonthVal"] = nextDay.Month;
                    dataRow["YearNo"] = nextDay.Year;
                    dtInsertInToCalender.Rows.Add(dataRow);
                    nextDay = nextDay.AddDays(1);
                    if (nextDay.DayOfWeek.ToString().Equals(startingDayOfweek, StringComparison.OrdinalIgnoreCase))
                        weekNo++;
                }

                DBAccess.DBAccess.BulkInsertIntoCalender(dtInsertInToCalender);

                btnWeekView_Click(null, EventArgs.Empty);
                lblMessages.ForeColor = System.Drawing.Color.Green;
                lblMessages.Text = "Week information for the year " + year + " has been generated.";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Error while Generating Week Definition :" + ex.Message);
                lblMessages.ForeColor = System.Drawing.Color.Red;
                lblMessages.Text = ex.Message;
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            }
        }

        protected void btnWeekView_Click(object sender, EventArgs e)
        {
            try
            {
                int year = Convert.ToInt32(txtYear.Text);
                DataTable dtWeekInformation = DBAccess.DBAccess.GetWeekInformationFromDB(year);
                gvWeekDefinition.DataSource = dtWeekInformation;
                gvWeekDefinition.DataBind();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnWeekView_Click: " + ex.Message);
                lblMessages.ForeColor = System.Drawing.Color.Red;
                lblMessages.Text = ex.Message;
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            }
        }
    }
}