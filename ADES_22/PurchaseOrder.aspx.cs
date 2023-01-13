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
    public partial class PurchaseOrder : System.Web.UI.Page
    {
        static string appPath = HttpContext.Current.Server.MapPath("");
        // static string poNo = "";
        DataTable dtbl = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //DateTime now = DateTime.Now;
                //var startDate = new DateTime(now.Year, now.Month, 1);
                //var endDate = startDate.AddMonths(1).AddDays(-1);

                //txtfromdate.Text = startDate.ToString("yyyy-MM-dd");
                //txttodate.Text = endDate.ToString("yyyy-MM-dd");

                getStatusValues();               
                List<string> list = Getdata("");
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < list.Count; i++)
                {
                    stringBuilder.Append(string.Format("<option value='{0}'>", list[i]));
                }
                dlCustName.InnerHtml = stringBuilder.ToString();
                getPurchaseDetails();
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

        protected void txtAddCustName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                List<string> list = GetRegionValues(txtAddCustName.Text);
                ddladdregion.DataSource = list;
                ddladdregion.DataBind();
                if (sender != null)
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal2", "$('#neworder').modal('show');", true);
                    HelperClass.OpenModal(this, "neworder", true);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("txtCustName_TextChanged1:" + ex.Message);
            }
        }

        public void getPurchaseDetails()
        {
            try
            {
                List<purchaseOrder> listPurchase = new List<purchaseOrder>();
                DropDownValues ddlValue = new DropDownValues();
                ddlValue.Region = ddlRegion.SelectedValue;
                ddlValue.Customer = txtCustName.Text.ToString();
                ddlValue.FromDate = txtfromdate.Text;
                ddlValue.ToDate = txttodate.Text;
                ddlValue.Status = ddlStatus.SelectedValue;

                Session["PurchaseData"] = listPurchase = DBAccess.DBAccess.getPurchaseDetails(ddlValue);
                GridView1.DataSource = listPurchase;
                GridView1.DataBind();                
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("getPurchaseDetails:" +ex.Message);
            }
        }

        public void getStatusValues()
        {
            try
            {
                DataTable dtDropD1 = new DataTable();
                DataTable dtDropD2 = new DataTable();
                DataTable dtDropD3 = new DataTable();

                DataTable[] dtArray = DBAccess.DBAccess.getFilterValues("Prompt", out dtDropD1, out dtDropD2, out dtDropD3);

                ddlStatus.DataSource = dtArray[2];
                ddlStatus.DataTextField = "Status";
                ddlStatus.DataValueField = "Status";
                ddlStatus.DataBind();
                ddlStatus.Items.Insert(0, new ListItem("All", ""));


                //List<ListItem> lidt = new List<ListItem>();
                //lidt.Add(new ListItem() { Text = "C1", Value = "1" });
                //lidt.Add(new ListItem() { Text = "C2", Value = "2" });
                //ddlStatus.DataSource = lidt;
                //ddlStatus.DataTextField = "Text";
                //ddlStatus.DataValueField = "Value";
                //ddlStatus.DataBind();

                //ddlStatus.SelectedValue
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("getStatusValues:" +ex.Message);
            }
        }
     
        public void getStatusValues1()
        {
            try
            {
                DataTable dtDropD1 = new DataTable();
                DataTable dtDropD2 = new DataTable();
                DataTable dtDropD3 = new DataTable();

                DataTable[] dtArray = DBAccess.DBAccess.getFilterValues("Prompt", out dtDropD1, out dtDropD2, out dtDropD3);

                ddlAddstatus.DataSource = dtArray[2];
                ddlAddstatus.DataTextField = "Status";
                ddlAddstatus.DataValueField = "Status";
                ddlAddstatus.DataBind();              
                ddlAddstatus.Items.Insert(0, new ListItem("None", ""));

                ddladdtallypono.DataSource = dtArray[1];
                ddladdtallypono.DataTextField = "PoNumber";
                ddladdtallypono.DataValueField = "PoNumber";
             
                ddladdtallypono.DataBind();
                ddladdtallypono.Items.Insert(0, new ListItem("None", ""));
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("getStatusValues1:" +ex.Message);
            }
        }

        public static List<string> Getdata(string blank)
        {
            List<string> result = new List<string>();
            try
            {
               result = DBAccess.DBAccess.GetCustName("Prompt", blank);              
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("Getdata:" +ex.Message);
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
                        //ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openValidationModal('To date must be greater than From date!!!');", true);
                        HelperClass.OpenValidationModal(this, "To date must be greater than From date!!!");
                    }
                }
                getPurchaseDetails();
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("btnView_Click:" +ex.Message);
            }
        }

        public static List<string> getQuoteRef(string blank)
        {
            List<string> result = new List<string>();
            try
            {
            result = DBAccess.DBAccess.getQuoteRefvalue("Prompt", blank);
            }
            catch(Exception ex)
            {
               Logger.WriteErrorLog("getQuoteRef:" +ex.Message);
            }
            return result;
        }

        public static List<string> GetRegionValues(string blank)
        {
            List<string> result = new List<string>();
            try
            {
             result = DBAccess.DBAccess.getRegionValues(blank);
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("GetRegionValues:" +ex.Message);
            }
            return result;
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                List<PurchaseOrder> listProposalEntry = new List<PurchaseOrder>();
                DropDownValues ddlValue = new DropDownValues();
                ddlValue.Region = ddlRegion.SelectedValue;
                ddlValue.Customer = txtCustName.Text.ToString();
                ddlValue.FromDate = txtfromdate.Text;
                ddlValue.ToDate = txttodate.Text;
                ddlValue.Status = ddlStatus.SelectedValue;
                Session["ExportData"] = dt = DBAccess.DBAccess.getExportedOrders(ddlValue);


                string templatefile = string.Empty;
                string Filename = "PurchaseOrder.xlsx";

                string Source = string.Empty;
                Source = Proposal.GetReportPath(Filename);
                string Template = string.Empty;
                Template = "PurchaseOrder" + DateTime.Now + ".xls";
                string destination = string.Empty;
                destination = Path.Combine(appPath, "Temp", SafeFileName(Template));

                if (!File.Exists(Source))
                {
                    Console.WriteLine("Purchase order Report- \n " + Source);
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    FileInfo newFile = new FileInfo(Source);
                    ExcelPackage Excel = new ExcelPackage(newFile, true);
                    ExcelWorksheet exelworksheet = Excel.Workbook.Worksheets[0];
                    int cellRow = 1;
                    exelworksheet.Cells[cellRow, cellRow, cellRow, dt.Columns.Count - 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    exelworksheet.Cells[cellRow, cellRow, cellRow, dt.Columns.Count - 1].Value = "Purchase order Report";
                    exelworksheet.Cells[cellRow, cellRow, cellRow, dt.Columns.Count - 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    exelworksheet.Cells[cellRow, cellRow, cellRow, dt.Columns.Count - 1].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#093d81"));
                    exelworksheet.Cells[cellRow, cellRow, cellRow, dt.Columns.Count - 1].Merge = true;
                    exelworksheet.Cells[cellRow, cellRow, cellRow, dt.Columns.Count - 1].Style.Font.Size = 18;
                    exelworksheet.Cells[cellRow, cellRow, cellRow, dt.Columns.Count - 1].Style.Font.Bold = true;
                    exelworksheet.Cells[cellRow, cellRow, cellRow, dt.Columns.Count - 1].Style.Font.Color.SetColor(Color.White);

                    cellRow = 2;

                    exelworksheet.Cells[cellRow, cellRow, cellRow, dt.Columns.Count - 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    exelworksheet.Cells[cellRow, cellRow, cellRow, dt.Columns.Count - 1].Value = "Date : " + DateTime.Now.ToString();
                    exelworksheet.Cells[cellRow, cellRow, cellRow, dt.Columns.Count - 1].Merge = true;
                    exelworksheet.Cells[cellRow, cellRow, cellRow, dt.Columns.Count - 1].Style.Font.Size = 12;

                    cellRow = cellRow + 2;

                    exelworksheet.Cells[cellRow, 1].Value = "From Date";
                    exelworksheet.Cells[cellRow, 1].Style.Font.Bold = true;
                    exelworksheet.Cells[cellRow, 1].Style.Font.Size = 12;
                    exelworksheet.Cells[cellRow, 2].Value = txtfromdate.Text.Trim();
                    exelworksheet.Cells[cellRow, 4].Value = "To Date";
                    exelworksheet.Cells[cellRow, 4].Style.Font.Bold = true;
                    exelworksheet.Cells[cellRow, 4].Style.Font.Size = 12;
                    exelworksheet.Cells[cellRow, 5].Value = txttodate.Text.Trim();

                    cellRow = cellRow + 2;

                    exelworksheet.Cells[cellRow, 1].Value = "Region";
                    exelworksheet.Cells[cellRow, 1].Style.Font.Bold = true;
                    exelworksheet.Cells[cellRow, 1].Style.Font.Size = 12;
                    exelworksheet.Cells[cellRow, 2].Value = ddlRegion.SelectedValue;
                    exelworksheet.Cells[cellRow, 4].Value = "Customer";
                    exelworksheet.Cells[cellRow, 4].Style.Font.Bold = true;
                    exelworksheet.Cells[cellRow, 4].Style.Font.Size = 12;
                    exelworksheet.Cells[cellRow, 5].Value = txtCustName.Text;
                    exelworksheet.Cells[cellRow, 7].Value = "Status";
                    exelworksheet.Cells[cellRow, 7].Style.Font.Bold = true;
                    exelworksheet.Cells[cellRow, 7].Style.Font.Size = 12;
                    exelworksheet.Cells[cellRow, 8].Value = ddlStatus.SelectedValue ;
                    if (txtCustName.Text.ToString() != "")
                    {
                        exelworksheet.Cells[cellRow, 5].Value = txtCustName.Text.ToString();
                    }
                    else
                    {
                        exelworksheet.Cells[cellRow, 5].Value = "All";
                    }

                    cellRow = cellRow + 2;

                    for (int i = 0; i < dt.Columns.Count - 1; i++)
                    {
                        exelworksheet.Cells[cellRow, i + 1].Value = dt.Columns[i].ColumnName.ToString();
                        exelworksheet.Cells[cellRow, i + 1].Style.Font.Bold = true;
                        exelworksheet.Cells[cellRow, i + 1].Style.Font.Size = 12;

                        Color backcolor = ColorTranslator.FromHtml("#b1d1fc");
                        exelworksheet.Cells[cellRow, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        exelworksheet.Cells[cellRow, i + 1].Style.Fill.BackgroundColor.SetColor(backcolor);
                        exelworksheet.Cells[cellRow, i + 1].Style.Font.Color.SetColor(Color.Black);

                        exelworksheet.Cells[cellRow, i + 1].AutoFitColumns();
                    }
                    cellRow++;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count - 1; j++)
                        {
                            exelworksheet.Cells[cellRow, j + 1].Value = dt.Rows[i][j].ToString();
                        }

                        cellRow++;
                    }

                    for (int i = 1; i <= dt.Columns.Count - 1; i++)
                    {
                        exelworksheet.Cells[3, i, dt.Rows.Count + 5, i].AutoFitColumns();
                    }
                    DownloadFile(destination, Excel.GetAsByteArray());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnExport_Click" +ex.Message);
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
                Logger.WriteErrorLog("DownloadFile:" +ex.Message);
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
            catch(Exception ex)
            {
              Logger.WriteErrorLog("SafeFileName:" +ex.Message);
            }
            return str.ToString();
        }

        public static string GetReportPath(string reportName)
        {
            string src;
             
                 if (HttpContext.Current.Session["Language"] == null)
                    src = Path.Combine(appPath, "ExcelDocument", reportName);
                else
                {
                    if (HttpContext.Current.Session["Language"].ToString() != "en")
                        src = Path.Combine(appPath, "ExcelDocument-" + HttpContext.Current.Session["Language"].ToString() + "", reportName);
                    else
                        src = Path.Combine(appPath, "ExcelDocument", reportName);
                }
              return src;
        }

        //protected void btnCancel_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
                 
        //        getPurchaseDetails();
        //    }
        //    catch(Exception ex)
        //    {
        //        Logger.WriteErrorLog("btnCancel_Click:" + ex.Message);
        //    }             
        //}

        protected void FileUpload_Click(object sender, EventArgs e)
        {
            try
            {
                int index = ((GridViewRow)((sender as Control)).NamingContainer).RowIndex;
                string fileextension = ((System.Web.UI.WebControls.LinkButton)(GridView1.Rows[index].FindControl("FileUpload"))).Text;

                List<purchaseOrder> listPurchaseEntry = new List<purchaseOrder>();
                listPurchaseEntry = Session["PurchaseData"] as List<purchaseOrder>;
                byte[] bytes = listPurchaseEntry[index].attachedFile;

                //if (fileextension.Substring(fileextension.IndexOf('.') + 1).ToLower() == "pdf")
                if (fileextension.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = "application/pdf";
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("FileUpload_Click:" +ex.Message);
            }

        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                 Session["DeleteRowIndex"] = e.RowIndex;
                purchaseOrder purchaseEntry = new purchaseOrder();
                purchaseEntry.POnumber = (GridView1.Rows[e.RowIndex].FindControl("txtTallyPO") as Label).Text;

                bool res = DBAccess.DBAccess.DeletePO(purchaseEntry, "Delete");
                if (res == false)
                {
                    //ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openValidationModal('This PO is already dispatched!!!');", true);
                    HelperClass.OpenValidationModal(this, "This PO is already dispatched!!!");
                }
                else
                {
                    //ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openConfirmModal('Are you sure, you want to delete this record?');", true);
                    HelperClass.openConfirmModal(this, "Are you sure, you want to delete this record?");
                }
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("GridView1_RowDeleting:" +ex.Message);
            }
        }

        protected void saveConfirmYes_ServerClick(object sender, EventArgs e)
        {
            try
            {
                int DeleteRowIndex = (int)Session["DeleteRowIndex"];
                purchaseOrder purchaseEntry = new purchaseOrder();
                purchaseEntry.POnumber = (GridView1.Rows[DeleteRowIndex].FindControl("txtTallyPO") as Label).Text;
                DBAccess.DBAccess.DeletePO(purchaseEntry, "Delete");
                Session["DeleteRowIndex"] = -1;
                getPurchaseDetails();
            }
            catch (Exception ex)
            {
                Console.Write("Error while deleting data - " +ex.Message);
            }

        }

        protected void editTallyPO_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int index = ((GridViewRow)((sender as Control)).NamingContainer).RowIndex;
                string selectedValue = (GridView1.Rows[index].FindControl("editTallyPO") as DropDownList).SelectedItem.Text;
                if (selectedValue != "")
                {
                    string selectedDate = DBAccess.DBAccess.getSelectedTallyDate(selectedValue);
                    (GridView1.Rows[index].FindControl("editTallyPOdate") as TextBox).Text = selectedDate;
                }
            }
            catch (Exception ex)
            {
               Console.Write("editTallyPO_SelectedIndexChanged:" + ex.Message);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtCustName.Text = "";
                txtCustName_TextChanged(null, null);
                ddlRegion.SelectedValue = "";
                txtfromdate.Text = "";
                txttodate.Text = "";
                ddlStatus.SelectedValue = "";
                GridView1.DataSource = "";
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Console.Write("btnClear_Click:" + ex.Message);
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

        public static List<string> GetAddQuoteRef(string blank)
        {
            List<string> result1 = new List<string>();
            try
            {
                result1 = DBAccess.DBAccess.getQuoteRefvalue("Prompt", blank);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetAddQuoteRef:" + ex.Message);
            }
            return result1;
        }
 
        protected void BtnNewEntry_Click(object sender, EventArgs e)
        {
            try
            {
                txtAddCustName.Text = "";
                List<string> list = GetAddCustName("");
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < list.Count; i++)
                {
                    stringBuilder.Append(string.Format("<option value='{0}'>", list[i]));
                }
                dladdCustName.InnerHtml = stringBuilder.ToString();
                txtAddCustName_TextChanged(null, null);
                
                getStatusValues1();
                
                if (ddlAddstatus.Items.FindByValue("")!= null)
                {
                    ddlAddstatus.SelectedValue = "";
                }
                if(ddladdtallypono.Items.FindByValue("")!=null)
                {
                    ddladdtallypono.SelectedValue = "";
                }
                ddladdtallypono_SelectedIndexChanged(null, null);
                txtaddqutref.Text = "";
                List<string> list1 = GetAddQuoteRef("");
                StringBuilder stringBuilder1 = new StringBuilder();
                for (int i = 0; i < list1.Count; i++)
                {
                    stringBuilder1.Append(string.Format("<option value='{0}'>", list1[i]));
                }
                dladdquotRef.InnerHtml = stringBuilder1.ToString();              
                txtAddstatusason.Text = "";
                txtAddpopno.Text = "";
                txtAddpopno.Enabled = true;
                txtAddpopdate.Text = "";
                txtAddpopval.Text = "";
                hfFile.Value = "";
                hfFileName.Value = "";
                hfNewOrEdit.Value = "New";
                btnSave.Text = "Add";
                purchaseordertitle.Text = "Add Purchase Order";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "$('#neworder').modal('show');", true);
                HelperClass.OpenModal(this, "neworder", true);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BtnNewEntry_Click: " + ex.Message);
            }
        }

        protected void lbEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = (((sender as LinkButton).NamingContainer) as GridViewRow).RowIndex;
                txtAddCustName.Text = (GridView1.Rows[rowIndex].FindControl("txtCustomer") as Label).Text;
                txtAddCustName_TextChanged(null, null);
                getStatusValues1();
                string value = (GridView1.Rows[rowIndex].FindControl("txtTallyPO") as Label).Text;
                ddladdtallypono.Items.Add(value);
                if (ddladdtallypono.Items.FindByValue(value) != null)
                {
                    ddladdtallypono.SelectedValue = value;
                }               
                ddladdtallypono_SelectedIndexChanged(null, null);
                value = (GridView1.Rows[rowIndex].FindControl("txtStatus") as Label).Text;
                if (ddlAddstatus.Items.FindByValue(value) != null)
                {
                    ddlAddstatus.SelectedValue = value;
                }
                txtaddqutref.Text = (GridView1.Rows[rowIndex].FindControl("txtQuoteRef") as Label).Text;
                List<string> list1 = GetAddQuoteRef("");
                StringBuilder stringBuilder1 = new StringBuilder();
                for (int i = 0; i < list1.Count; i++)
                {
                    stringBuilder1.Append(string.Format("<option value='{0}'>", list1[i]));
                }
                dladdquotRef.InnerHtml = stringBuilder1.ToString();
                txtAddpopno.Text = (GridView1.Rows[rowIndex].FindControl("txtPONo") as Label).Text;
                txtAddpopno.Enabled = false;
                txtAddpopval.Text = (GridView1.Rows[rowIndex].FindControl("txtPOValue") as Label).Text;
                txtAddpopdate.Text = (GridView1.Rows[rowIndex].FindControl("txtPODate") as Label).Text;                                
                txtAddstatusason.Text = (GridView1.Rows[rowIndex].FindControl("txtStatusAsOn") as Label).Text;
                addFileName.Text = (GridView1.Rows[rowIndex].FindControl("FileUpload") as LinkButton).Text;
                hfFile.Value = (GridView1.Rows[rowIndex].FindControl("hdnattachedfileInBase64") as HiddenField).Value;
                hfFileName.Value = (GridView1.Rows[rowIndex].FindControl("FileUpload") as LinkButton).Text;
               
                hfNewOrEdit.Value = "Edit";
                btnSave.Text = "Update";
                purchaseordertitle.Text = "Edit PE";
                HelperClass.OpenModal(this, "neworder", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "$('#neworder').modal('show');", true);
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
                purchaseOrder purchaseEntry = new purchaseOrder();               
                purchaseEntry.Customer = txtAddCustName.Text;
                purchaseEntry.POnumber = txtAddpopno.Text;
                purchaseEntry.tallyPOnumber = ddladdtallypono.SelectedValue ;
                purchaseEntry.Region = ddladdregion.SelectedValue ;
                purchaseEntry.POdate = txtAddpopdate.Text;
                purchaseEntry.POvalue = txtAddpopval.Text;
                purchaseEntry.tallyPOdate = txtAddtallypodate.Text;
                purchaseEntry.Status = ddlAddstatus.SelectedValue;
                purchaseEntry.StatusAsOn = txtAddstatusason.Text;
                purchaseEntry.QuoteRef = txtaddqutref.Text;
                if (hfFile.Value == "")
                {

                }
                else
                {
                    string file = hfFile.Value;
                    byte[] fileinbytes = System.Convert.FromBase64String(file.Substring(file.LastIndexOf(',') + 1));
                    purchaseEntry.attachedFile = fileinbytes;
                    purchaseEntry.FileUpload = hfFileName.Value;

                }
                if (hfNewOrEdit.Value == "New")
                {
                    DBAccess.DBAccess.saveUpdatePurchase(purchaseEntry, "Save");
                   //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "clearscreen()", true);
                  //ScriptManager.RegisterStartupScript(this, typeof(Page), "Success" + 1, "<script>SuccessToastr('Purchase order information successfully saved!')</script>", false);
                   HelperClass.ClearModal(this);
                   HelperClass.OpenSuccessToaster(this, "Purchase order information successfully saved!!");
                }
                else
                {
                    DBAccess.DBAccess.saveUpdatePurchase(purchaseEntry, "Update");
                    //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "clearscreen()", true);
                    //ScriptManager.RegisterStartupScript(this, typeof(Page), "Success" + 1, "<script>SuccessToastr('Purchase order information successfully updated!')</script>", false);
                   HelperClass.ClearModal(this);
                   HelperClass.OpenSuccessToaster(this, "Purchase order information successfully updated!");

                }
                
                  getPurchaseDetails();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnSave_Click: " + ex.Message);
            }
        }

        public string GetAddPoNo(string blank)
        {
            string result = "";
            try
            {

                result = DBAccess.DBAccess.getSelectedTallyDate(ddladdtallypono.SelectedValue);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetAddPoNo:" + ex.Message);
            }
            return result;
        }

        protected void ddladdtallypono_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string list = GetAddPoNo(ddladdtallypono.SelectedValue);
                txtAddtallypodate.Text = list;
                txtAddtallypodate.DataBind();
                if (sender != null)
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal2", "$('#neworder').modal('show');", true);
                    HelperClass.OpenModal(this, "neworder", true);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("ddladdtallypono_SelectedIndexChanged:" + ex.Message);
            }
        }

        protected void btnFileDownLoad_Click(object sender, EventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(hfRowIndex.Value);
                string fileextension = ((System.Web.UI.WebControls.LinkButton)(GridView1.Rows[index].FindControl("fileUpload"))).Text;               
                List<purchaseOrder> listpurchaseorder = new List<purchaseOrder>();
                listpurchaseorder = Session["PurchaseData"] as List<purchaseOrder>;
                (GridView1.Rows[index].FindControl("fileUpload") as LinkButton).Text = listpurchaseorder[index].Attachment;
                byte[] bytes = listpurchaseorder[index].attachedFile;

                
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

  