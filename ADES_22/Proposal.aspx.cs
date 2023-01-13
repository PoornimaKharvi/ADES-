using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using ADES_22.DBAccess;
using ADES_22.Model;
using System.Data;
using System.Web.Services;
using System.Web.Script.Services;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Drawing;

namespace ADES_22
{
    public partial class Proposal : System.Web.UI.Page
    {
        static string appPath = HttpContext.Current.Server.MapPath("");
        //static string ProposalNumber = "";
        //static string ProposalVersion = "";
        DataTable dtbl = null;
        private readonly object ex;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                //DateTime now = DateTime.Now;
                //var startDate = new DateTime(now.Year, now.Month, 1);
                //var endDate = startDate.AddMonths(1).AddDays(-1);
                //txtfromdate.Text = startDate.ToString("yyyy-MM-dd");
                //txttodate.Text = endDate.ToString("yyyy-MM-dd");

                getGeneralDropDownValue();
                List<string> list = Getdata("");
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < list.Count; i++)
                {
                    stringBuilder.Append(string.Format("<option value='{0}'>", list[i]));
                }
                dlCustName.InnerHtml = stringBuilder.ToString();
                selectedProposal();
            }
        }
        protected void txtCustName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                List<string> list = GetRegionValues(txtCustName.Text);
                ddlRegion.DataSource = list;
                ddlRegion.DataBind();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("txtCustName_TextChanged:" + ex.Message);
            }
        }

        public static List<string> Getdata(string blank)
        {
            List<string> result = new List<string>();
            try
            {
                result = DBAccess.DBAccess.GetCustName("Prompt", blank);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Getdata" + ex.Message);
            }
            return result;
        }

        public void selectedProposal()
        {
            try
            {
                List<ProposalEntryDetails> listProposalEntry = new List<ProposalEntryDetails>();
                DropDownValues ddlValue = new DropDownValues();
                ddlValue.Region = ddlRegion.SelectedValue;
                ddlValue.Customer = txtCustName.Text.ToString();
                ddlValue.Owner = ddlOwner.SelectedValue;
                ddlValue.Status = ddlStatus.SelectedValue;
                ddlValue.FromDate = txtfromdate.Text;
                ddlValue.ToDate = txttodate.Text;
                ddlValue.KeyField = lbkeyfield.SelectedValue;

                Session["ProposalData"] = listProposalEntry = DBAccess.DBAccess.getProposalEntry(ddlValue);
                GridView1.DataSource = listProposalEntry;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("selectedProposal" + ex.Message);
            }
        }

        public void getGeneralDropDownValue()
        {
            try
            {
                DataTable dtDropD1 = new DataTable();
                DataTable dtDropD2 = new DataTable();
                DataTable dtDropD3 = new DataTable();
                DataTable dtDropD4 = new DataTable();
                DataTable dtDropD5 = new DataTable();


                DataTable[] dtArray = DBAccess.DBAccess.getDropDownValue("Prompt", out dtDropD1, out dtDropD2, out dtDropD3, out dtDropD4, out dtDropD5);

                DataTable table1 = dtArray[0];
                DataTable table2 = dtArray[1];
                DataTable table3 = dtArray[2];
                DataTable table4 = dtArray[3];
                DataTable table5 = dtArray[4];


                ddlRegion.DataSource = "";
                ddlRegion.DataBind();
                ddlRegion.Items.Insert(0, new ListItem(" ", ""));

                ddlStatus.DataSource = table3;
                ddlStatus.DataTextField = "Status";
                ddlStatus.DataValueField = "Status";
                ddlStatus.DataBind();
                ddlStatus.Items.Insert(0, new ListItem("All", ""));

                ddlOwner.DataSource = table4;
                ddlOwner.DataTextField = "Owner";
                ddlOwner.DataValueField = "Owner";
                ddlOwner.DataBind();
                ddlOwner.Items.Insert(0, new ListItem("All", ""));


                lbkeyfield.DataSource = table5;
                lbkeyfield.DataTextField = "KeyField";
                lbkeyfield.DataValueField = "KeyField";
                lbkeyfield.DataBind();
        
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("getGeneralDropDownValue" + ex.Message);
            }
        }

        public void getGeneralDropDownValue1()
        {
            try
            {
                DataTable dtDropD1 = new DataTable();
                DataTable dtDropD2 = new DataTable();
                DataTable dtDropD3 = new DataTable();
                DataTable dtDropD4 = new DataTable();
                DataTable dtDropD5 = new DataTable();


                DataTable[] dtArray = DBAccess.DBAccess.getDropDownValue1("Prompt", out dtDropD1, out dtDropD2, out dtDropD3, out dtDropD4, out dtDropD5);

                DataTable table1 = dtArray[0];
                DataTable table2 = dtArray[1];
                DataTable table3 = dtArray[2];
                DataTable table4 = dtArray[3];
                DataTable table5 = dtArray[4];



                ddlAddstatus1.DataSource = table3;
                ddlAddstatus1.DataTextField = "Status";
                ddlAddstatus1.DataValueField = "Status";
                ddlAddstatus1.DataBind();
                ddlAddstatus1.Items.Insert(0, new ListItem("None", ""));

                ddlAddowner1.DataSource = table4;
                ddlAddowner1.DataTextField = "Owner";
                ddlAddowner1.DataValueField = "Owner";
                ddlAddowner1.DataBind();
                ddlAddowner1.Items.Insert(0, new ListItem("None", ""));


                lbAddkeyfield.DataSource = table5;
                lbAddkeyfield.DataTextField = "KeyField";
                lbAddkeyfield.DataValueField = "KeyField";
                lbAddkeyfield.DataBind();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("getGeneralDropDownValue1" + ex.Message);
            }

        }

        public static List<string> GetRegionValues(string blank)
        {
            List<string> result = new List<string>();
            try
            {
                result = DBAccess.DBAccess.getRegionValues(blank);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetRegionValues" + ex.Message);
            }
            return result;
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtfromdate.Text != "" && txttodate.Text != "")
                {
                    var startDate = Convert.ToDateTime(txtfromdate.Text.Trim());
                    var endDate = Convert.ToDateTime(txttodate.Text.Trim());
                    if (endDate < startDate)
                    {
                        HelperClass.OpenValidationModal(this, "To date must be greater than From date!!!");
                    }
                }
                selectedProposal();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnView_Click" + ex.Message);
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                List<ProposalEntryDetails> listProposalEntry = new List<ProposalEntryDetails>();
                DropDownValues ddlValue = new DropDownValues();
                ddlValue.Region = ddlRegion.SelectedValue;
                ddlValue.Customer = txtCustName.Text.ToString();
                ddlValue.Owner = ddlOwner.SelectedValue;
                ddlValue.Status = ddlStatus.SelectedValue;
                ddlValue.FromDate = txtfromdate.Text;
                ddlValue.ToDate = txttodate.Text;
                ddlValue.KeyField = lbkeyfield.SelectedValue;
                Session["ExportData"] = dt = DBAccess.DBAccess.getExportValues(ddlValue);

                string templatefile = string.Empty;
                string Filename = "ProposalEntry.xlsx";

                string Source = string.Empty;
                Source = Proposal.GetReportPath(Filename);
                string Template = string.Empty;
                Template = "ProposalEntry" + DateTime.Now + ".xls";
                string destination = string.Empty;
                destination = Path.Combine(appPath, "Temp", SafeFileName(Template));

                if (!File.Exists(Source))
                {
                    Console.WriteLine("Proposal Report- \n " + Source);
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    FileInfo newFile = new FileInfo(Source);
                    ExcelPackage Excel = new ExcelPackage(newFile, true);
                    System.Diagnostics.Debug.WriteLine(Excel);
                    ExcelWorksheet exelworksheet = Excel.Workbook.Worksheets[0];
                    int cellRow = 1;
                    exelworksheet.Cells[cellRow, cellRow, cellRow, dt.Columns.Count - 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    //Color backcolor = Color.FromArgb(224, 76, 76);
                    exelworksheet.Cells[cellRow, cellRow, cellRow, dt.Columns.Count - 2].Value = "Proposal Report";
                    exelworksheet.Cells[cellRow, cellRow, cellRow, dt.Columns.Count - 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    exelworksheet.Cells[cellRow, cellRow, cellRow, dt.Columns.Count - 2].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#093d81"));
                    exelworksheet.Cells[cellRow, cellRow, cellRow, dt.Columns.Count - 2].Merge = true;
                    exelworksheet.Cells[cellRow, cellRow, cellRow, dt.Columns.Count - 2].Style.Font.Size = 18;
                    exelworksheet.Cells[cellRow, cellRow, cellRow, dt.Columns.Count - 2].Style.Font.Bold = true;
                    exelworksheet.Cells[cellRow, cellRow, cellRow, dt.Columns.Count - 2].Style.Font.Color.SetColor(Color.White);

                    cellRow = 2;

                    exelworksheet.Cells[cellRow, cellRow, cellRow, dt.Columns.Count - 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    exelworksheet.Cells[cellRow, cellRow, cellRow, dt.Columns.Count - 2].Value = "Date : " + DateTime.Now.ToString();
                    exelworksheet.Cells[cellRow, cellRow, cellRow, dt.Columns.Count - 2].Merge = true;
                    exelworksheet.Cells[cellRow, cellRow, cellRow, dt.Columns.Count - 2].Style.Font.Size = 12;


                    cellRow = cellRow + 2;

                    exelworksheet.Cells[cellRow, 1].Value = "From Date";
                    exelworksheet.Cells[cellRow, 1].Style.Font.Bold = true;
                    exelworksheet.Cells[cellRow, 1].Style.Font.Size = 12;
                    exelworksheet.Cells[cellRow, 2].Value = txtfromdate.Text.Trim();
                    exelworksheet.Cells[cellRow, 4].Value = "To Date";
                    exelworksheet.Cells[cellRow, 4].Style.Font.Bold = true;
                    exelworksheet.Cells[cellRow, 4].Style.Font.Size = 12;
                    exelworksheet.Cells[cellRow, 5].Value = txttodate.Text.Trim();

                    exelworksheet.Cells[cellRow, 7].Value = "Owner";
                    exelworksheet.Cells[cellRow, 7].Style.Font.Bold = true;
                    exelworksheet.Cells[cellRow, 7].Style.Font.Size = 12;
                    if (ddlOwner.SelectedValue.Trim() != "")
                    {
                        exelworksheet.Cells[cellRow, 8].Value = ddlOwner.SelectedValue;
                    }
                    else
                    {
                        exelworksheet.Cells[cellRow, 8].Value = "All";
                    }
                    cellRow = cellRow + 2;

                    exelworksheet.Cells[cellRow, 1].Value = "Region";
                    exelworksheet.Cells[cellRow, 1].Style.Font.Bold = true;
                    exelworksheet.Cells[cellRow, 1].Style.Font.Size = 12;
                    exelworksheet.Cells[cellRow, 2].Value = ddlRegion.SelectedValue;
                    exelworksheet.Cells[cellRow, 4].Value = "Customer";
                    exelworksheet.Cells[cellRow, 4].Style.Font.Bold = true;
                    exelworksheet.Cells[cellRow, 4].Style.Font.Size = 12;
                    if (txtCustName.Text.ToString() != "")
                    {
                        exelworksheet.Cells[cellRow, 5].Value = txtCustName.Text.ToString();
                    }
                    else
                    {
                        exelworksheet.Cells[cellRow, 5].Value = "All";
                    }

                    exelworksheet.Cells[cellRow, 7].Value = "Key Field";
                    exelworksheet.Cells[cellRow, 7].Style.Font.Bold = true;
                    exelworksheet.Cells[cellRow, 7].Style.Font.Size = 12;
                    exelworksheet.Cells[cellRow, 8].Value = lbkeyfield.SelectedValue;
                    cellRow = cellRow + 2;

                    for (int i = 0; i < dt.Columns.Count - 2; i++)
                    {
                        exelworksheet.Cells[cellRow, i + 1].Value = dt.Columns[i].ColumnName.ToString();
                        exelworksheet.Cells[cellRow, i + 1].Style.Font.Bold = true;
                        exelworksheet.Cells[cellRow, i + 1].Style.Font.Size = 12;

                        // Color backcolor = Color.FromArgb(93, 123, 157);
                        Color backcolor = ColorTranslator.FromHtml("#b1d1fc");
                        exelworksheet.Cells[cellRow, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        exelworksheet.Cells[cellRow, i + 1].Style.Fill.BackgroundColor.SetColor(backcolor);
                        exelworksheet.Cells[cellRow, i + 1].Style.Font.Color.SetColor(Color.Black);

                        exelworksheet.Cells[cellRow, i + 1].AutoFitColumns();
                    }
                    cellRow++;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count - 2; j++)
                        {
                            exelworksheet.Cells[cellRow, j + 1].Value = dt.Rows[i][j].ToString();
                        }

                        cellRow++;
                    }

                    for (int i = 1; i <= dt.Columns.Count - 2; i++)
                    {
                        exelworksheet.Cells[3, i, dt.Rows.Count + 5, i].AutoFitColumns();
                    }
                    DownloadFile(destination, Excel.GetAsByteArray());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnExport_Click" + ex.Message);
            }

        }

        private static void DownloadFile(string filename, byte[] bytearray)
        {
            try
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + Path.GetFileName(filename) + "\"");
                HttpContext.Current.Response.OutputStream.Write(bytearray, 0, bytearray.Length);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.SuppressContent = true;
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("DownloadFile" + ex.Message);
            }

        }

        public static string SafeFileName(string name)
        {
            StringBuilder str = new StringBuilder(name);
            try
            {
                foreach (char c in System.IO.Path.GetInvalidFileNameChars())
                {
                    str = str.Replace(c, '_');
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("SafeFileName" + ex.Message);
            }
            return str.ToString();
        }

        public static string GetReportPath(string reportName)
        {
            string src = "";
            try
            {
                if (HttpContext.Current.Session["Language"] == null)
                    src = Path.Combine(appPath, "ExcelDocument", reportName);
                else
                {
                    if (HttpContext.Current.Session["Language"].ToString() != "en")
                        src = Path.Combine(appPath, "ExcelDocument-" + HttpContext.Current.Session["Language"].ToString() + "", reportName);
                    else
                        src = Path.Combine(appPath, "ExcelDocument", reportName);
                }
            }
            catch (Exception e)
            {
                Logger.WriteErrorLog("GetReportPath" + e.Message);
            }
            return src;

        }
 
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Session["DeleteRowIndex"] = e.RowIndex;
                ProposalEntryDetails proposalEntry = new ProposalEntryDetails();
                proposalEntry.ProposalNumber = (GridView1.Rows[e.RowIndex].FindControl("txtProposalNo") as Label).Text;
                proposalEntry.ProposalVersion = (GridView1.Rows[e.RowIndex].FindControl("txtVersion") as Label).Text;
                //ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openConfirmModal('Are you sure, you want to delete this record?');", true);
                HelperClass.openConfirmModal(this, "Are you sure, you want to delete this record?");
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GridView1_RowDeleting" + ex.Message);
            }
        }

        protected void saveConfirmYes_ServerClick(object sender, EventArgs e)
        {
            try
            {
                int DeleteRowIndex = (int)Session["DeleteRowIndex"];
                ProposalEntryDetails proposalEntry = new ProposalEntryDetails();
                proposalEntry.ProposalNumber = (GridView1.Rows[DeleteRowIndex].FindControl("txtProposalNo") as Label).Text;
                proposalEntry.ProposalVersion = (GridView1.Rows[DeleteRowIndex].FindControl("txtVersion") as Label).Text;
                DBAccess.DBAccess.InsertUpdateGridView(proposalEntry, "Delete");
                GridView1.EditIndex = -1;
                selectedProposal();

            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("saveConfirmYes_ServerClick" + ex.Message);
            }
        }     
 
        protected void Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                selectedProposal();
                HelperClass.ClearModal(this);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Cancel_Click" + ex.Message);
            }
        }
 
        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtCustName.Text = "";
                txtCustName_TextChanged(null, null);
                txtfromdate.Text = "";
                txttodate.Text = "";
                ddlOwner.SelectedValue = "";
                ddlStatus.SelectedValue = "";
                ddlRegion.SelectedValue = "";
               
                GridView1.DataSource="";
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnClear_Click" + ex.Message);
            }
        }

        public static List<string> GetAddCustName(string blank)
        {
            List<string> result = new List<string>();
            try
            {
                //DataTable dt= Getdata("");
                // List<string> output = new List<string>();
                result = DBAccess.DBAccess.GetCustName("Prompt", blank);
                //result= output.AsEnumerable().Select(x=>x.Field<string>()).ToList();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetAddCustName:" + ex.Message);
            }
            return result;
        }

        protected void txtAddCustomer_TextChanged1(object sender, EventArgs e)
        {
            try
            {
                List<string> list = GetRegionValues(txtAddCustomer.Text);
                ddlAddregion1.DataSource = list;
                ddlAddregion1.DataBind();
                if (sender != null)
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal2", "$('#newproposal').modal('show');", true);
                    HelperClass.OpenModal(this, "newproposal", false);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(" txtAddCustomer_TextChanged1:" + ex.Message);
            }
        }

        protected void btnNewEntry_Click(object sender, EventArgs e)
        {
            try
            {
                txtAddCustomer.Text = "";
                List<string> list = GetAddCustName("");
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < list.Count; i++)
                {
                    stringBuilder.Append(string.Format("<option value='{0}'>", list[i]));
                }
                dlCustName1.InnerHtml = stringBuilder.ToString();
                txtAddCustomer_TextChanged1(null, null);
                getGeneralDropDownValue1();
                if (ddlAddowner1.Items.FindByValue(" ") != null)
                {
                    ddlAddowner1.SelectedValue = "";
                }
                if (ddlAddstatus1.Items.FindByValue(" ") != null)
                {
                    ddlAddstatus1.SelectedValue = "";
                }
                if (lbAddkeyfield.Items.FindByValue(" ") != null)
                {
                    lbAddkeyfield.SelectedValue = "";
                }

                txtAddpropval.Text = "";
                txtAddsubmitdate.Text = "";
                txtAddpropno.Text = "";
                txtAddpropno.Enabled = true;
                txtAddversion.Text = "";
                txtAddversion.Enabled = true;
                txtAddpropdate.Text = "";
                txtAddstatusason.Text = "";
                hfFile.Value = "";
                hfFileName.Value = "";
                hfNewOrEdit.Value = "New";
                btnSave.Text = "Add";
                addFileName.Text = "";
                Proposalentrytitle.Text = "Add Proposal Details";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal1", "$('#newproposal').modal('show');", true);
                HelperClass.OpenModal(this, "newproposal", true);

            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnNewEntry_Click: " + ex.Message);
            }
        }

        protected void lbEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = (((sender as LinkButton).NamingContainer) as GridViewRow).RowIndex;
                txtAddCustomer.Text = (GridView1.Rows[rowIndex].FindControl("txtCustomer") as Label).Text;
                //string value = (GridView1.Rows[rowIndex].FindControl("txtRegion") as Label).Text;
                //if (ddlAddregion1.Items.FindByValue(value) != null)
                //{
                //    ddlAddregion1.SelectedValue = value;
                //}                
                txtAddCustomer_TextChanged1(null, null);
                getGeneralDropDownValue1();
                string value = (GridView1.Rows[rowIndex].FindControl("txtStatus") as Label).Text;
                if (ddlAddstatus1.Items.FindByValue(value) != null)
                {
                    ddlAddstatus1.SelectedValue = value;
                }
                value = (GridView1.Rows[rowIndex].FindControl("txtOwner") as Label).Text;
                if (ddlAddowner1.Items.FindByValue(value) != null)
                {
                    ddlAddowner1.SelectedValue = value;
                }
                value = (GridView1.Rows[rowIndex].FindControl("txtKeyField") as Label).Text;
                string[] keys=value.Split(',');
                for (int i = 0; i < keys.Length; i++)
                {
                    if (lbAddkeyfield.Items.FindByValue(keys[i]) != null)
                    {
                        lbAddkeyfield.Items.FindByValue(keys[i]).Selected = true;
                    }                  
                }
                txtAddpropno.Text = (GridView1.Rows[rowIndex].FindControl("txtProposalNo") as Label).Text;
                txtAddpropno.Enabled = false;
                txtAddversion.Text = (GridView1.Rows[rowIndex].FindControl("txtVersion") as Label).Text;
                txtAddversion.Enabled = false;
                txtAddpropdate.Text = (GridView1.Rows[rowIndex].FindControl("txtProposalDate") as Label).Text;
                txtAddpropval.Text = (GridView1.Rows[rowIndex].FindControl("txtProposalValue") as Label).Text;
                txtAddsubmitdate.Text = (GridView1.Rows[rowIndex].FindControl("txtSubmitDate") as Label).Text;
                txtAddstatusason.Text = (GridView1.Rows[rowIndex].FindControl("txtStatusAsOn") as Label).Text;
                addFileName.Text = (GridView1.Rows[rowIndex].FindControl("fileUpload") as LinkButton).Text;
                hfFile.Value = (GridView1.Rows[rowIndex].FindControl("hdnattachedfileInBase64") as HiddenField).Value;
                hfFileName.Value = (GridView1.Rows[rowIndex].FindControl("fileUpload") as LinkButton).Text;

                hfNewOrEdit.Value = "Edit";
                btnSave.Text = "Update";
                Proposalentrytitle.Text = "Edit PE";

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "$('#newproposal').modal('show');", true);
                HelperClass.OpenModal(this, "newproposal", false);

            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("lbEdit_Click: " + ex.Message);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                ProposalEntryDetails proposalEntry = new ProposalEntryDetails();
                proposalEntry.Customer = txtAddCustomer.Text;
                proposalEntry.ProposalNumber = txtAddpropno.Text;
                proposalEntry.ProposalVersion = txtAddversion.Text;
                proposalEntry.Region = ddlAddregion1.SelectedValue;
                proposalEntry.ProposalDate = txtAddpropdate.Text;
                proposalEntry.ProposalOwner = ddlAddowner1.SelectedValue;
                proposalEntry.ProposalValue = txtAddpropval.Text;
                proposalEntry.SubmittedDate = txtAddsubmitdate.Text;
                proposalEntry.Status = ddlAddstatus1.SelectedValue;
                proposalEntry.StatusAsOn = txtAddstatusason.Text;
                //List<ListItem> keys = new List<ListItem>();
                List<string> ls = new List<string>();
                foreach (ListItem list in lbAddkeyfield.Items)
                {
                    if (list.Selected==true)
                    {
                        ls.Add(list.ToString());
                        //keys.Add(list);
                    }
                }
               String keys= String.Join(",", ls);
               proposalEntry.KeyField = keys;
                //if ( lbAddkeyfield.Items.Count != 0)
                //{
                //    for (int i = 0; i <  lbAddkeyfield.Items.Count; i++)
                //    {
                //        if ( lbAddkeyfield.Items[i].Selected)
                //        {
                //            List<ListItem> item = new List<ListItem>();
                //            item.Add(lbAddkeyfield.Items[i]);
                //        }
                //    }
                //    proposalEntry.KeyField = lbAddkeyfield.ToString();
                //}

                if (hfFile.Value == "")
                {
                }
                else
                {
                    string file = hfFile.Value;
                    byte[] fileinbytes = System.Convert.FromBase64String(file.Substring(file.LastIndexOf(',') + 1));
                    proposalEntry.attachedFile = fileinbytes;
                    proposalEntry.FileUpload = hfFileName.Value;

                }
                if (hfNewOrEdit.Value == "New")
                {
                    DBAccess.DBAccess.InsertUpdateGridView(proposalEntry, "Save");
                    HelperClass.ClearModal(this);
                    HelperClass.OpenSuccessToaster(this, "Proposal Entry information successfully saved!");
                }
                else
                {
                    DBAccess.DBAccess.InsertUpdateGridView(proposalEntry, "Update");
                    HelperClass.ClearModal(this);
                    HelperClass.OpenSuccessToaster(this, "Proposal Entry information successfully updated!");
                }
                selectedProposal();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnSave_Click: " + ex.Message);
            }
        }

        protected void btnFileDownLoad_Click(object sender, EventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(hfRowIndex.Value);
                string fileextension = ((System.Web.UI.WebControls.LinkButton)(GridView1.Rows[index].FindControl("fileUpload"))).Text;

                //string fileUpload= (GridView1.Rows[index].FindControl("fileUpload") as LinkButton).Text;

                List<ProposalEntryDetails> listProposalEntry = new List<ProposalEntryDetails>();
                listProposalEntry = Session["ProposalData"] as List<ProposalEntryDetails>;
                (GridView1.Rows[index].FindControl("fileUpload") as LinkButton).Text = listProposalEntry[index].FileUpload;
                byte[] bytes = listProposalEntry[index].attachedFile;

                //fileextension = listProposalEntry[index].FileUpload;
                // fileUpload = listProposalEntry[index].FileUpload;


                //if (fileextension.Substring(fileextension.IndexOf('.') + 1).ToLower() == "pdf")
                //if (fileextension.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                //{
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;
                response.ContentType = "application/force-download";
                response.AddHeader("Content-Disposition", "attachment;filename=" + (GridView1.Rows[index].FindControl("fileUpload") as LinkButton).Text);
                response.BinaryWrite(bytes);
                response.Flush(); // Sends all currently buffered output to the client.
                response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                HttpContext.Current.ApplicationInstance.CompleteRequest();// Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
            }
            //}
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnFileDownLoad_Click" + ex.Message);
            }
        }
    }
}




