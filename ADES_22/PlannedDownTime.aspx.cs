using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADES_22.Model;
using ADES_22.DBAccess;
using System.Globalization;
using System.Drawing;
using ADES_22.Exports;
using System.Data;
using iTextSharp.text.log;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System.ComponentModel.Design;
using System.util;
using Util = ADES_22.Model.Util;

namespace ADES_22
{
    public partial class PlannedDownTime : System.Web.UI.Page
    {
        //int allChecked = 0;

        public object Web_TPMTrakDashboard { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                // Session["FltrDate"] = null;
                // Session["FltrReason"] = null;
                BindHolidaysDropDowns();
                setActiveTab("holiday");

                btnHodiday_Click(null, null);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "setActiveValue", "setActiveSubmenuValue();", true);
            }
            // ReadOrModifyPage();
        }
        private void setCompanyControl()
        {
            try
            {
                if (Session["UserRole"] == null)
                {
                    Response.Redirect("~/Login.aspx", false);
                }
                else
                {
                    string userRole = (string)Session["UserRole"];
                    if (userRole.Replace(" ", "").Trim().ToLower() == "superadmin")
                    {
                        //lblCompany.Visible = false;
                        //ddlCompany.Visible = true;

                    }
                    else
                    {
                        // lblCompany.Visible = true;
                        // ddlCompany.Visible = false;
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("" + ex);
            }
        }
        private void ReadOrModifyPage()
        {
            try
            {
                List<MenuShowHide> list = new List<MenuShowHide>();
                if (Session["ScreenShowHide"] == null)
                {
                    Session["ScreenShowHide"] = DBAccess.DBAccess.getMenuListForLoginUser(Session["CompanyName"].ToString(), Session["Username"].ToString(), "ScreenView");
                    list = (List<MenuShowHide>)Session["ScreenShowHide"];
                }
                else
                {
                    list = (List<MenuShowHide>)Session["ScreenShowHide"];
                }
                string accestype = list.Where(x => x.Screen == "PlannedDownTime").Select(x => x.Value).FirstOrDefault();
                if (accestype == "Read")
                {
                    btnApplyHoliday.Visible = false;
                    btnApplyWeekOffsFilter.Visible = false;
                    DBAccess.DBAccess.ShowHideColumnOfGrid(gvHolidays, false, "Select");
                    DBAccess.DBAccess.ShowHideColumnOfGrid(gvWeekOffs, false, "Select");
                    DBAccess.DBAccess.ShowHideColumnOfGrid(gvAllDowns, false, "Select");
                }
                else
                {
                    btnApplyHoliday.Visible = true;
                    btnApplyWeekOffsFilter.Visible = true;
                    DBAccess.DBAccess.ShowHideColumnOfGrid(gvHolidays, true, "Select");
                    DBAccess.DBAccess.ShowHideColumnOfGrid(gvWeekOffs, true, "Select");
                    DBAccess.DBAccess.ShowHideColumnOfGrid(gvAllDowns, true, "Select");
                }
            }
            catch (Exception ex)
            {

            }
        }

        #region ---------- New ---------------------

        private void setActiveTab(string tab)
        {
            try
            {

                tabHoliday.Attributes["class"] = "";
                tabWeeklyOff.Attributes["class"] = "";
                tabAllDown.Attributes["class"] = "";

                //tabHolidayContent.Attributes["class"] = "";
                //tabWeeklyOffContent.Attributes["class"] = "";
                //tabAllDownContent.Attributes["class"] = "";

                gvHolidays.Visible = false;
                gvWeekOffs.Visible = false;
                gvAllDowns.Visible = false;

                hdnActiveTab.Value = tab;
                if (tab == "holiday")
                {
                    tabHoliday.Attributes.Add("class", "active");
                    //tabHolidayContent.Attributes.Add("class", "active");
                   // tabWeeklyOffContent.Attributes.Add("class", "in active");
                   // tabAllDownContent.Attributes.Add("class", " in active");
                    tabHoliday.Style.Add("background-color", "#cdcdcd");
                    tabWeeklyOff.Style.Add("background-color", "transparent");
                    tabAllDown.Style.Add("background-color", "transparent");
                   
                    gvHolidays.Visible = true;
                }
                else if (tab == "weeklyoff")
                {
                    //   tabHoliday.Attributes.Add("display", "none");
                    //tabHoliday.Attributes.Add("class", " in active");
                    tabWeeklyOff.Attributes.Add("class", "active");
                   // tabWeeklyOffContent.Attributes.Add("class", "active");
                    tabHoliday.Style.Add("background-color", "transparent");
                    tabWeeklyOff.Style.Add("background-color", "#cdcdcd");
                    tabAllDown.Style.Add("background-color", "transparent");
                    
                    gvWeekOffs.Visible = true;
                }
                else if (tab == "alldown")
                {
                    //   tabHoliday.Attributes.Add("display", "none");
                    tabAllDown.Attributes.Add("class", "active");
                  //  tabAllDownContent.Attributes.Add("class", "active");
                    tabHoliday.Style.Add("background-color", "transparent");
                    tabWeeklyOff.Style.Add("background-color", "transparent");
                    tabAllDown.Style.Add("background-color", "#cdcdcd");
                   
                    gvAllDowns.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("setActiveTab" + ex);
            }
        }

        #region---------Holiday---------------
        private void BindHolidaysDropDowns()
        {
            try
            {
                txtFromHolidayYear.Text = DateTime.Now.Year.ToString();
                txtToYear.Text = DateTime.Now.Year.ToString();
                var months = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
                for (int i = 0; i < months.Length - 1; i++)
                {
                    ddlMonth.Items.Add(new ListItem(months[i], i.ToString()));
                }
                for (int i = 1; i <= 31; i++)
                {
                    ddlDay.Items.Add(i.ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindHolidaysDropDowns" + ex);
            }
        }

        protected void chkSelectAllHoliday_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkSelectAllHoliday.Checked)
                {
                    foreach (GridViewRow row in gvHolidays.Rows)
                    {
                        (row.FindControl("chkSelectHoliday") as CheckBox).Checked = true;
                        row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    }
                }
                else
                {
                    foreach (GridViewRow row in gvHolidays.Rows)
                    {
                        (row.FindControl("chkSelectHoliday") as CheckBox).Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("chkSelectAllHoliday_CheckedChanged" + ex);
            }


        }
        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string selectedMonth = ddlMonth.SelectedItem.Text;
                switch (selectedMonth)
                {
                    case "January":
                    case "March":
                    case "May":
                    case "July":
                    case "August":
                    case "October":
                    case "December":
                        ddlDay.Items.Clear();
                        for (int i = 1; i <= 31; i++)
                        {
                            ddlDay.Items.Add(i.ToString());
                        }
                        break;
                    case "February":
                        ddlDay.Items.Clear();
                        for (int i = 1; i <= 29; i++)
                        {
                            ddlDay.Items.Add(i.ToString());
                        }
                        break;
                    case "April":
                    case "June":
                    case "September":
                    case "November":
                        ddlDay.Items.Clear();
                        for (int i = 1; i < 31; i++)
                        {
                            ddlDay.Items.Add(i.ToString());
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("ddlMonth_SelectedIndexChanged" + ex);
            }
        }
        protected void btnHodiday_Click(object sender, EventArgs e)
        {
            try
            {
                //selectedMenu.Value = "Holiday";
                setActiveTab("holiday");
                divHolidayFilter.Visible = false;
                if (divHolidayFilter.Visible)
                {
                    btnApplyHoliday_Click(null, null);
                }
                else
                {
                    BindHolidayData("", "", "", "");
                }
                BindHolidayFilterReason("");
                ddlReason.Value = "";
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnHodiday_Click" + ex);
            }
        }
        private void BindHolidayData(string value, string value2, string reason, string param)
        {
            try
            {
                List<HolidayListDetails> holidayListDetailsList = new List<HolidayListDetails>();
                holidayListDetailsList = DBAccess.DBAccess.getHolidayList(value, value2, reason, param);
                //showhideColumnsOfGrid(company, "PDT_Holiday",gvHolidays);
                gvHolidays.DataSource = holidayListDetailsList;
                gvHolidays.DataBind();
                chkSelectAllHoliday.Checked = false;
                Session["HolidayList"] = holidayListDetailsList;
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindHolidayData" + ex);
            }
        }
        private void BindHolidayFilterReason(string param)
        {
            try
            {
                List<string> list = DBAccess.DBAccess.getAllReasons();
                if (param == "")
                {
                    ddlFilterByReason.DataSource = list;
                    ddlFilterByReason.DataBind();
                    ddlFilterByReason.Items.Insert(0, "All");
                }
                else if (param == "deleteTheData")
                {
                    string reason = ddlFilterByReason.Text;
                    int flag = 0;
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (reason == list[i])
                        {
                            flag = 1;
                            break;
                        }
                    }
                    if (flag == 0)
                    {
                        ddlFilterByReason.DataSource = list;
                        ddlFilterByReason.DataBind();
                        ddlFilterByReason.Items.Insert(0, "All");
                    }
                }
                else
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (ddlFilterByReason.Items.FindByValue(list[i]) == null)
                        {
                            ddlFilterByReason.Items.Add(list[i]);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindHolidayFilterReason" + ex);
            }
        }
        protected void lbtnReload_Click(object sender, EventArgs e)
        {
            try
            {
                btnHodiday_Click(null, null);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("lbtnReload_Click" + ex);
            }
        }
        protected void lnkFilter_Click(object sender, EventArgs e)
        {
            try
            {
                divHolidayFilter.Visible = true;
                ddlFilter.SelectedValue = "ByDate";
                ddlFilter_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("lnkFilter_Click" + ex);
            }
        }
        protected void btnSaveHoliday_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("StartTime", typeof(DateTime));
                dt.Columns.Add("EndTime", typeof(DateTime));
                dt.Columns.Add("DownReason", typeof(string));
                dt.Columns.Add("DownType", typeof(string));
                dt.Columns.Add("DayName", typeof(string));

                if (txtFromHolidayYear.Text.Trim() == "")
                {
                    HelperClass.OpenWarningToaster(this, "Please select From Year.");
                    return;
                }
                if (txtToYear.Text.Trim() == "")
                {
                    HelperClass.OpenWarningToaster(this, "Please select To Year.");
                    return;
                }
                int fromYear = Convert.ToInt16(txtFromHolidayYear.Text.Trim());
                int toYear = Convert.ToInt16(txtToYear.Text.Trim());
                string month = (Convert.ToInt16((ddlMonth.SelectedValue == null ? "" : ddlMonth.SelectedValue.ToString())) + 1).ToString("00");
                string day = (Convert.ToInt16((ddlDay.SelectedItem == null ? "" : ddlDay.SelectedItem.ToString()))).ToString("00");

                string reason = ddlReason.Value.Trim();
                for (int i = fromYear; i <= toYear; i++)
                {
                    string holiday = i + "-" + month + "-" + day + " 00:00:00";
                    DateTime incrementDate = Util.GetDateTime(holiday);
                    var tblRow = dt.NewRow();
                    tblRow[0] = incrementDate.ToString("yyyy-MM-dd HH:mm:ss");
                    tblRow[1] = incrementDate.ToString("yyyy-MM-dd HH:mm:ss");
                    tblRow[2] = reason;
                    tblRow[3] = "Holiday";
                    tblRow[4] = "";
                    dt.Rows.Add(tblRow);
                }

                if (dt.Rows.Count > 0)
                {
                    DBAccess.DBAccess.deletePORawDataRecords();
                    List<PDTData> rejectedShiftDownItems = new List<PDTData>();
                    rejectedShiftDownItems = DBAccess.DBAccess.savePDTDetails(dt, "HolidaySave");
                    if (rejectedShiftDownItems.Count > 0)
                    {
                        if (rejectedShiftDownItems.Count != dt.Rows.Count)
                        {
                            ddlReason.Value = "";
                        }
                        Session["PDTRejectedItems"] = rejectedShiftDownItems;
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "displayRejectedItems();", true);
                    }
                    else
                    {
                        ddlReason.Value = "";
                        Session["PDTRejectedItems"] = null;
                        HelperClass.OpenSuccessToaster(this, "Holiday Inserted Successfully");
                    }
                }
                BindHolidayFilterReason("appendTheData");
                if (divHolidayFilter.Visible)
                {
                    btnApplyHoliday_Click(null, null);
                }
                else
                {
                    BindHolidayData("", "", "", "");
                }

                //divHolidayFilter.Visible = false;
                //ddlReason.Value = "";
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnSaveHoliday_Click: " + ex.Message);
            }


        }
        protected void lnkHolidayFilterClose_Click(object sender, EventArgs e)
        {
            try
            {
                divHolidayFilter.Visible = false;
                btnHodiday_Click(null, null);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("lnkHolidayFilterClose_Click: " + ex.Message);
            }

        }
        protected void btnApplyHoliday_Click(object sender, EventArgs e)
        {
            try
            {
                string filterValue = ddlFilter.SelectedValue == null ? "" : ddlFilter.SelectedValue.ToString();
                switch (filterValue)
                {
                    case "ByDate":
                        DateTime fromdate = DateTime.Now.Date;
                        DateTime todate = DateTime.Now.Date;
                        fromdate = Util.GetDateTime(txtFilterByDateFrom.Text + " 00:00:00");
                        todate = Util.GetDateTime(txtFilterByDateTo.Text + " 00:00:00");
                        string reason = ddlFilterByReason.SelectedValue == "All" ? "" : ddlFilterByReason.SelectedValue.ToString();

                        BindHolidayData(fromdate.ToString("yyyy-MM-dd HH:mm:ss"), todate.ToString("yyyy-MM-dd HH:mm:ss"), reason, "ByDate");
                        break;
                    case "ByReason":
                        BindHolidayData(ddlFilterByReason.SelectedValue == "All" ? "" : ddlFilterByReason.SelectedValue.ToString(), "", "", "ByReason");
                        break;

                }

            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnApplyHoliday_Click: " + ex.Message);
            }

        }

        protected void btnDeleteRecordsYes_Click(object sender, EventArgs e)
        {
            try
            {
                string fltrDate = "", fltrReason = "";
                List<string> fltrMachine = new List<string>();
                //if (txtFilterByDate.Visible)
                //{
                //    fltrDate = txtFilterByDate.Text;
                //}
                //if (ddlFilterByReason.Visible)
                //{
                //    fltrReason = ddlFilterByReason.SelectedValue == null ? "" : ddlFilterByReason.SelectedValue.ToString();
                //}
                Session["FltrDate"] = fltrDate;
                Session["FltrReason"] = fltrReason;
                string sqlQuery = "";
                for (int i = 0; i < gvHolidays.Rows.Count; i++)
                {
                    if ((gvHolidays.Rows[i].FindControl("chkSelectHoliday") as CheckBox).Checked)
                    {
                        string holiday = (gvHolidays.Rows[i].FindControl("hdnDate") as HiddenField).Value;
                        if (holiday != "")
                        {
                            DateTime date = DateTime.Now.Date;
                            date = Util.GetDateTime(holiday);
                            holiday = date.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        string ss = @"delete from HolidayList where  Holiday='" + holiday + "'";
                        sqlQuery += ss;
                    }

                }
                int success = DBAccess.DBAccess.deleteHolidayDetails(sqlQuery);
                if (success <= 0)
                {
                    HelperClass.OpenWarningToaster(this, "Failed to delete the Record..!");
                }
                else
                {
                    HelperClass.OpenSuccessToaster(this, "Successfully Deleted Record");
                }
                chkSelectAllHoliday.Checked = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "closeDeleteConfirmModal('holidayListConfirmModal');", true);
                BindHolidayFilterReason("deleteTheData");
                if (divHolidayFilter.Visible)
                {
                    btnApplyHoliday_Click(null, null);
                }
                else
                {
                    BindHolidayData("", "", "", "");
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnDeleteRecordsYes_Click: " + ex.Message);
            }

        }
        protected void ddlFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string filterValue = ddlFilter.SelectedValue == null ? "" : ddlFilter.SelectedValue.ToString();
                holidayDateContainer.Visible = false;
                holidayReasonContainer.Visible = false;
                switch (filterValue)
                {
                    case "ByDate":
                        txtFilterByDateFrom.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                        txtFilterByDateTo.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                        holidayDateContainer.Visible = true;
                        holidayReasonContainer.Visible = true;
                        break;
                    case "ByReason":
                        holidayReasonContainer.Visible = true;
                        //lblDate.Visible = false;
                        //txtFilterByDate.Visible = false;
                        //List<string> reasons = DataBaseAccess.getAllReasons();
                        //ddlFilterByReason.DataSource = reasons;
                        //ddlFilterByReason.DataBind();
                        //lblReason.Visible = true;
                        //ddlFilterByReason.Visible = true;
                        ////lblMachine.Visible = false;
                        ////ddlFilterByMachines.Visible = false;
                        break;


                }

            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("ddlFilter_SelectedIndexChanged: " + ex.Message);
            }
        }
        #endregion

        #region ------- Weeklyoff ---------------

        protected void btnWeeklyOff_Click(object sender, EventArgs e)
        {
            try
            {
                selectedMenu.Value = "WeeklyOff";
                setActiveTab("weeklyoff");
                setWeeklyOffFilterData();
                txtFromWeeklyOffs.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                txtToWeeklyOffs.Text = DateTime.Now.Date.AddDays(7).ToString("dd-MM-yyyy");
                divWeeklyFilter.Visible = false;
                BindWeeklyOffData();
                BindWeeklyReason("");
                txtWeeklyOffReason.Text = "";
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnWeeklyOff_Click" + ex);
            }

        }
        private void BindWeeklyReason(string param)
        {
            try
            {
                List<string> list = DBAccess.DBAccess.getPDTDownReason("WeeklyOff");
                if (param == "")
                {
                    ddlWeeklyOffFilterReason.DataSource = list;
                    ddlWeeklyOffFilterReason.DataBind();
                    ddlWeeklyOffFilterReason.Items.Insert(0, "All");
                }
                else if (param == "deleteTheData")
                {
                    string reason = ddlWeeklyOffFilterReason.Text;
                    int flag = 0;
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (reason == list[i])
                        {
                            flag = 1;
                            break;
                        }
                    }
                    if (flag == 0)
                    {
                        ddlWeeklyOffFilterReason.DataSource = list;
                        ddlWeeklyOffFilterReason.DataBind();
                        ddlWeeklyOffFilterReason.Items.Insert(0, "All");
                    }
                }
                else
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (ddlWeeklyOffFilterReason.Items.FindByValue(list[i]) == null)
                        {
                            ddlWeeklyOffFilterReason.Items.Add(list[i]);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindWeeklyReason" + ex);
            }
        }
        private void setWeeklyOffFilterData()
        {
            try
            {
                txtFromWeekOffs.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                txtToWeekOffs.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("setWeeklyOffFilterData" + ex);
            }
        }
        private void BindWeeklyOffData()
        {
            try
            {

                string fromDate = "", toDate = "", reason = "";
                string viewType = "";
                if (divWeeklyFilter.Visible)
                {
                    viewType = ddlWeeklyOffFilterType.SelectedValue;
                    if (ddlWeeklyOffFilterType.SelectedValue == "ByDate")
                    {
                        DateTime fromdate = Util.GetDateTime(txtFromWeekOffs.Text.Trim());
                        fromDate = fromdate.ToString("yyyy-MM-dd");
                        DateTime todate = Util.GetDateTime(txtToWeekOffs.Text);
                        toDate = todate.ToString("yyyy-MM-dd");

                        reason = ddlWeeklyOffFilterReason.SelectedValue == "All" ? "" : ddlWeeklyOffFilterReason.SelectedValue;
                    }
                    else if (ddlWeeklyOffFilterType.SelectedValue == "ByReason")
                    {
                        reason = ddlWeeklyOffFilterReason.SelectedValue == "All" ? "" : ddlWeeklyOffFilterReason.SelectedValue;
                    }
                }
                List<PDTData> list = DBAccess.DBAccess.getWeeklyOffDetails(fromDate, toDate, reason, viewType);
                gvWeekOffs.DataSource = list;
                gvWeekOffs.DataBind();
                Session["PDTList"] = list;
                chkSelectAllWeekOffs.Checked = false;
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindWeeklyOffData" + ex);
            }

        }
        protected void ddlWeeklyOffFilterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                weeklyOffFilterDateContainer.Visible = false;
                //weeklyOffFilterMachineContainer.Visible = false;
                weeklyOffFilterReasonContainer.Visible = false;
                if (ddlWeeklyOffFilterType.SelectedValue == "ByDate")
                {
                    weeklyOffFilterDateContainer.Visible = true;
                    // weeklyOffFilterMachineContainer.Visible = true;
                    weeklyOffFilterReasonContainer.Visible = true;
                }
                else if (ddlWeeklyOffFilterType.SelectedValue == "ByReason")
                {
                    weeklyOffFilterReasonContainer.Visible = true;
                }
                //else if (ddlWeeklyOffFilterType.SelectedValue == "ByMachine")
                //{
                //    weeklyOffFilterMachineContainer.Visible = true;
                //}
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("ddlWeeklyOffFilterType_SelectedIndexChanged" + ex);
            }
        }
        protected void btnSaveWeekOffs_Click(object sender, EventArgs e)
        {
            try
            {
                string reason = txtWeeklyOffReason.Text.Trim();

                if (reason == "")
                {
                    HelperClass.OpenWarningToaster(this, "Please Enter the Reason..!");
                    return;
                }
                DataTable dt = new DataTable();
                dt.Columns.Add("StartTime", typeof(DateTime));
                dt.Columns.Add("EndTime", typeof(DateTime));
                dt.Columns.Add("DownReason", typeof(string));
                dt.Columns.Add("DownType", typeof(string));
                dt.Columns.Add("DayName", typeof(string));

                DateTime fromdate = Util.GetDateTime(txtFromWeeklyOffs.Text.Trim());
                DateTime todate = Util.GetDateTime(txtToWeeklyOffs.Text);
                DateTime incrementDate = fromdate;
                string selectedDays = "", selectedWeek = "";

                foreach (ListItem item in ddlDays.Items)
                {
                    if (item.Selected)
                    {
                        selectedDays += item.Value.ToLower() + ",";
                    }
                }
                if (selectedDays.Trim() == "")
                {
                    HelperClass.OpenWarningToaster(this, "Please select Day!");
                    return;
                }
                foreach (ListItem item in ddlWeek.Items)
                {
                    if (item.Selected)
                    {
                        selectedWeek += item.Value + ",";
                    }
                }
                if (selectedWeek.Trim() == "")
                {
                    HelperClass.OpenWarningToaster(this, "Please select week..!");
                    return;
                }

                while (incrementDate <= todate)
                {
                    string incrementDateDay = incrementDate.ToString("ddd").ToLower();
                    if (selectedDays.IndexOf(incrementDateDay, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        string incrementDateWeek = GetWeekOfMonth(incrementDate).ToString();
                        if (selectedWeek.IndexOf(incrementDateWeek, StringComparison.OrdinalIgnoreCase) >= 0)
                        {


                            var tblRow = dt.NewRow();
                            tblRow[0] = incrementDate.ToString("yyyy-MM-dd HH:mm:ss");
                            tblRow[1] = incrementDate.ToString("yyyy-MM-dd HH:mm:ss");
                            tblRow[2] = reason.Trim();
                            tblRow[3] = "WeeklyOff";
                            tblRow[4] = incrementDate.ToString("dddd").ToLower();
                            dt.Rows.Add(tblRow);

                        }
                    }
                    incrementDate = incrementDate.AddDays(1);
                }
                if (dt.Rows.Count > 0)
                {
                    DBAccess.DBAccess.deletePORawDataRecords();
                    List<PDTData> rejectedWeeklyOffItems = new List<PDTData>();
                    rejectedWeeklyOffItems = DBAccess.DBAccess.savePDTDetails(dt, "PDTsave");


                    if (rejectedWeeklyOffItems.Count > 0)
                    {
                        if (dt.Rows.Count != rejectedWeeklyOffItems.Count)
                        {
                            txtWeeklyOffReason.Text = "";
                        }
                        Session["PDTRejectedItems"] = rejectedWeeklyOffItems;
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "displayRejectedItems();", true);
                    }
                    else
                    {
                        txtWeeklyOffReason.Text = "";
                        Session["PDTRejectedItems"] = null;
                        HelperClass.OpenSuccessToaster(this, "Weekly-Off Inserted Successfully");
                    }
                }
                BindWeeklyReason("appendTheData");
                BindWeeklyOffData();
            }
            catch (Exception ex)
            {

                Logger.WriteErrorLog("btnSaveWeekOffs_Click" + ex);
            }
        }
        public static int GetIso8601WeekOfYear(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

        }
        public static int GetWeekNumberOfMonth(DateTime date)
        {

            date = date.Date;
            DateTime firstMonthDay = new DateTime(date.Year, date.Month, 1);
            DateTime firstMonthMonday = firstMonthDay.AddDays((DayOfWeek.Monday + 7 - firstMonthDay.DayOfWeek) % 7);
            if (firstMonthMonday > date)
            {
                firstMonthDay = firstMonthDay.AddMonths(-1);
                firstMonthMonday = firstMonthDay.AddDays((DayOfWeek.Monday + 7 - firstMonthDay.DayOfWeek) % 7);
            }
            return (date - firstMonthMonday).Days / 7 + 1;


        }
        public static int GetWeekOfMonth(DateTime date)
        {
            DateTime selectedDate = date;
            DateTime beginningOfMonth = new DateTime(date.Year, date.Month, 1);
            DateTime beginningDate = beginningOfMonth;
            //while (date.Date.AddDays(1).DayOfWeek != CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
            while (date.Date.AddDays(1).DayOfWeek != DayOfWeek.Sunday)
            {

                date = date.AddDays(1);
            }
            int result, flag = 0;
            if ((beginningDate.Date.DayOfWeek != DayOfWeek.Sunday))
            {
                while ((beginningDate.Date.DayOfWeek != DayOfWeek.Sunday))
                {
                    if ((beginningDate.Date.DayOfWeek == selectedDate.DayOfWeek))
                    {
                        flag = 1;
                        break;
                    }
                    beginningDate = beginningDate.Date.AddDays(1);
                }
            }
            else
            {
                flag = 1;
            }
            if (flag == 0)
            {
                result = (int)Math.Truncate((double)date.Subtract(beginningOfMonth).TotalDays / 7f);
            }
            else
            {
                result = (int)Math.Truncate((double)date.Subtract(beginningOfMonth).TotalDays / 7f) + 1;
            }

            return result;
        }
        protected void btnApplyWeekOffsFilter_Click(object sender, EventArgs e)
        {
            try
            {
                BindWeeklyOffData();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnApplyWeekOffsFilter_Click" + ex);
            }
        }
        protected void lnkWeeklyFilter_Click(object sender, EventArgs e)
        {
            try
            {
                divWeeklyFilter.Visible = true;
                ddlWeeklyOffFilterType.SelectedValue = "ByDate";
                ddlWeeklyOffFilterType_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("lnkWeeklyFilter_Click" + ex);
            }
        }
        protected void lnkWeeklyFilterClose_Click(object sender, EventArgs e)
        {
            try
            {
                btnWeeklyOff_Click(null, null);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("lnkWeeklyFilterClose_Click" + ex);
            }
        }
        protected void lbtnReloadWeekOffs_Click(object sender, EventArgs e)
        {
            try
            {
                btnWeeklyOff_Click(null, null);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("lbtnReloadWeekOffs_Click" + ex);
            }
        }
        protected void lbtnDeleteWeekOffs_Click(object sender, EventArgs e)
        {
            try
            {
                BindWeeklyOffData();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("lbtnDeleteWeekOffs_Click" + ex);
            }
        }
        protected void btnDeleteWeeklyOffData_Click(object sender, EventArgs e)
        {
            try
            {
                string deletedid = "";
                for (int i = 0; i < gvWeekOffs.Rows.Count; i++)
                {
                    if ((gvWeekOffs.Rows[i].FindControl("chkSelectWeekOffs") as CheckBox).Checked)
                    {
                        string id = "";
                        id = (gvWeekOffs.Rows[i].FindControl("hdnID") as HiddenField).Value;
                        deletedid += id + ",";
                    }
                }
                if (deletedid.Length > 0)
                {
                    deletedid = deletedid.Remove(deletedid.Length - 1);
                }
                int success = DBAccess.DBAccess.deletePDTDetails(deletedid, false);
                if (success <= 0)
                {
                    HelperClass.OpenWarningToaster(this, "Failed to Delete Weekly-Off Details");
                }
                else
                {
                    HelperClass.OpenSuccessToaster(this, "Weekly-Off Records Deleted Successfully");
                }
                //chkSelectAllWeekOffs.Checked = false;
                BindWeeklyReason("deleteTheData");
                BindWeeklyOffData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "closeDeleteConfirmModal('weeklyOffDeleteConfirmModal');", true);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnDeleteWeeklyOffData_Click" + ex);
            }
        }

        #endregion

        #region --------All Down ----------
        protected void btnAllDown_Click(object sender, EventArgs e)
        {
            try
            {
                selectedMenu.Value = "AllDown";
                setActiveTab("alldown");
                txtFromAllDowns.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                txtToAllDowns.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                BindAllDownData();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnAllDown_Click" + ex);
            }
        }
        private void BindAllDownData()
        {
            try
            {
                DateTime fromdate = Util.GetDateTime(txtFromAllDowns.Text.Trim());
                DateTime todate = Util.GetDateTime(txtToAllDowns.Text.Trim());
                string downtype = ddlAllDowns.SelectedValue;
                List<PDTData> list = DBAccess.DBAccess.getAllDownDetails(fromdate, todate, downtype);
                gvAllDowns.DataSource = list;
                gvAllDowns.DataBind();
                chkSelectAllDowns.Checked = false;
                Session["PDTList"] = list;
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindAllDownData" + ex);
            }
        }
        protected void lnkAllDownReload_Click(object sender, EventArgs e)
        {
            try
            {
                btnAllDown_Click(null, null);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("lnkAllDownReload_Click" + ex);
            }
        }
        protected void btnViewAllDowns_Click(object sender, EventArgs e)
        {
            try
            {
                BindAllDownData();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnViewAllDowns_Click" + ex);
            }
        }
        protected void btnDeleteAllDown_Click(object sender, EventArgs e)
        {
            try
            {
                string deletedid = "";
                bool isHolidayData = false;
                for (int i = 0; i < gvAllDowns.Rows.Count; i++)
                {
                    if ((gvAllDowns.Rows[i].FindControl("chkSelectDowns") as CheckBox).Checked)
                    {
                        string id = (gvAllDowns.Rows[i].FindControl("hdnID") as HiddenField).Value;
                        if ((gvAllDowns.Rows[i].FindControl("lblAllDowns") as Label).Text == "Holidays")
                        {
                            id = Util.GetDateTime(id).ToString("yyyy-MM-dd HH:mm:ss");
                            deletedid += "'" + id + "',";
                            isHolidayData = true;
                        }
                        else
                        {
                            deletedid += id + ",";
                        }
                    }
                }
                if (deletedid.Length > 0)
                {
                    deletedid = deletedid.Remove(deletedid.Length - 1);
                }
                int success = DBAccess.DBAccess.deletePDTDetails(deletedid, isHolidayData);
                if (success <= 0)
                {
                    HelperClass.OpenWarningToaster(this, "Error While Deleting the Record");
                }
                else
                {
                    HelperClass.OpenSuccessToaster(this, "Holiday Details Deleted Successfully");
                }
                //chkSelectAllWeekOffs.Checked = false;
                BindAllDownData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage1", "closeDeleteConfirmModal('allDownDeleteConfirmModal');", true);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnDeleteAllDown_Click" + ex);
            }
        }

        #endregion


        #endregion

        //---------------------Client-Side Invoking Method---------------------------------//

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static List<PDTData> getPDTRejectedData()
        {
            List<PDTData> finallist = new List<PDTData>();
            if (HttpContext.Current.Session["PDTRejectedItems"] != null)
            {
                finallist = (List<PDTData>)HttpContext.Current.Session["PDTRejectedItems"];
            }
            return finallist;
        }
        //---------------------Paging-----------------------------------------//
        protected void gvWeekOffs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvWeekOffs.PageIndex = e.NewPageIndex;
                if (Session["PDTList"] != null)
                {
                    gvWeekOffs.DataSource = Session["PDTList"] as List<PDTData>;
                    gvWeekOffs.DataBind();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("gvWeekOffs_PageIndexChanging" + ex);
            }
        }

        protected void gvWeekOffs_PreRender(object sender, EventArgs e)
        {
            try
            {
                GridView grid = (GridView)sender;
                if (grid != null)
                {
                    GridViewRow pagerRow = (GridViewRow)grid.BottomPagerRow;
                    if (pagerRow != null)
                    {
                        pagerRow.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("gvWeekOffs_PreRender" + ex);
            }
        }

        protected void gvHolidays_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvHolidays.PageIndex = e.NewPageIndex;
                if (Session["HolidayList"] != null)
                {
                    gvHolidays.DataSource = Session["HolidayList"] as List<HolidayListDetails>;
                    gvHolidays.DataBind();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("gvHolidays_PageIndexChanging" + ex);
            }
        }

        protected void gvHolidays_PreRender(object sender, EventArgs e)
        {
            try
            {
                GridView grid = (GridView)sender;
                if (grid != null)
                {
                    GridViewRow pagerRow = (GridViewRow)grid.BottomPagerRow;
                    if (pagerRow != null)
                    {
                        pagerRow.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("gvHolidays_PreRender" + ex);
            }
        }

        protected void gvAllDowns_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvAllDowns.PageIndex = e.NewPageIndex;
                if (Session["PDTList"] != null)
                {
                    gvAllDowns.DataSource = Session["PDTList"] as List<PDTData>;
                    gvAllDowns.DataBind();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("gvAllDowns_PageIndexChanging" + ex);
            }
        }

        protected void gvAllDowns_PreRender(object sender, EventArgs e)
        {
            try
            {
                GridView grid = (GridView)sender;
                if (grid != null)
                {
                    GridViewRow pagerRow = (GridViewRow)grid.BottomPagerRow;
                    if (pagerRow != null)
                    {
                        pagerRow.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("gvAllDowns_PreRender" + ex);
            }
        }

        #region -----Column Fileters ------------------------------------
        //protected void btnColumnSelectorOK_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string company = getCompanyId();

        //        if (selectedMenu.Value == "Holiday")
        //        {
        //            saveUserPreference(company, "PDT_Holiday");
        //            BindHolidayData("","","","","");
        //        }
        //        if (selectedMenu.Value == "WeeklyOff")
        //        {
        //            saveUserPreference(company, "PDT_WeeklyOffs");
        //            BindWeeklyOffData();
        //        }
        //        if (selectedMenu.Value == "AllDown")
        //        {
        //            saveUserPreference(company, "PDT_AllDown");
        //            BindAllDownData();
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //private void saveUserPreference(string companyid, string screen)
        //{
        //    try
        //    {
        //        foreach (ListItem item in cblColumnSelector.Items)
        //        {
        //            int success = 0;
        //            string[] value = item.Value.Split(',');
        //            ColumnList columnList = new ColumnList();
        //            columnList.CompanyID = companyid;
        //            columnList.FieldName = value[0];
        //            columnList.FieldOrder = value[1] == "" ? 0 : Convert.ToInt16(value[1]);
        //            columnList.FieldDisplayName = item.Text;
        //            columnList.ObjectType = "Column";
        //            columnList.FieldVisibility = item.Selected;
        //            columnList.Module = "Masters";
        //            columnList.Screen = screen;
        //            success = DBAccess.DBAccess.saveUserPreferences(columnList);
        //        }
        //        //showhideColumnsOfGrid(companyid,screen,gridView);
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //private void showhideColumnsOfGrid( string screen, GridView gridview)
        //{
        //    try
        //    {
        //        List<ColumnList> columnList = DBAccess.DBAccess.getUserPreferences(companyid, "Masters", screen);

        //        if (columnList.Count > 0)
        //        {

        //            cblColumnSelector.DataSource = columnList;
        //            cblColumnSelector.DataTextField = "FieldDisplayName";
        //            cblColumnSelector.DataValueField = "DataValueField";
        //            cblColumnSelector.DataBind();

        //            int checkedCount = 0;
        //            foreach (DataControlField col in gridview.Columns)
        //            {
        //                if (col.AccessibleHeaderText != "")
        //                {
        //                    bool isVisible = columnList.Where(x => x.FieldName == col.AccessibleHeaderText).Select(x => x.FieldVisibility).FirstOrDefault() == null ? false : columnList.Where(x => x.FieldName == col.AccessibleHeaderText).Select(x => x.FieldVisibility).FirstOrDefault();
        //                    string columnName = columnList.Where(x => x.FieldName == col.AccessibleHeaderText).Select(x => x.FieldDisplayName).FirstOrDefault();
        //                    col.HeaderText = columnName;
        //                    col.Visible = isVisible;
        //                    if (isVisible)
        //                    {
        //                        cblColumnSelector.Items.FindByText(columnName).Selected = true;
        //                        checkedCount++;
        //                    }
        //                    else
        //                    {
        //                        if (columnName != null)
        //                        {
        //                            cblColumnSelector.Items.FindByText(columnName).Selected = false;
        //                        }

        //                    }
        //                }

        //            }

        //            if (cblColumnSelector.Items.Count == checkedCount)
        //            {
        //                cbColumnSelectAll.Checked = true;
        //            }
        //            else
        //            {
        //                cbColumnSelectAll.Checked = false;
        //            }
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "closePanel", "hideColumnFilterPanels(event,'tdColumnFilters','panelColumnFilter');", true);
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        #endregion


    }
}