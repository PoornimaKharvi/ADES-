using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADES_22.Model;
using System.Text;
using static System.Net.WebRequestMethods;

namespace ADES_22
{
    public partial class ImportInvoice : System.Web.UI.Page
    {
        static int amount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["connectionString"] == null)
                if (!IsPostBack)
                {
                    import1.Style.Add("display", "block");
                    import2.Style.Add("display", "none");
                    BindPoNumber();
                    pdffile.Style.Add("display", "none");
                    btnSave.Visible = false;
                    selectSignalfile.Attributes["onchange"] = "UploadSignalFile(this)";
                }
        }
        public void BindPoNumber()
        {
            try
            {
                List<string> list = GetPONO("");
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < list.Count; i++)
                {
                    stringBuilder.Append(string.Format("<option value='{0}'>", list[i]));
                }
                dlPoNumber.InnerHtml = stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindPoNumber:" + ex.Message);
            }
        }
        protected void txtPO_TextChanged(object sender, EventArgs e)
        {
            List<string> list1 = new List<string>();
            try
            {
                DataTable dt = new DataTable();
                Pono filtValues = new Pono();
                filtValues.PoNo = txtPO.Text;

                dt = DBAccess.DBAccess.POCheckInvoice(filtValues, "ParameterDetails");
                list1 = dt.AsEnumerable().Where(x => x.Field<string>("PONumber") == txtPO.Text).Select(x => x.Field<string>("InvoiceNumber")).ToList();
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < list1.Count; i++)
                {
                    stringBuilder.Append(string.Format("<option value='{0}'>", list1[i]));

                }
                dlInvNo.InnerHtml = stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("txtPO_TextChanged:" + ex.Message);
            }
        }
        protected void ddlInvoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<dataListInvoice> result = new List<dataListInvoice>();
            try
            {
                DataTable dt = new DataTable();
                Pono filtValues = new Pono();
                filtValues.PoNo = txtPO.Text;

                dt = DBAccess.DBAccess.POCheckInvoice(filtValues, "ParameterDetails");
                var details = dt.AsEnumerable().Where(x => x.Field<string>("PONumber") == txtPO.Text && x.Field<string>("InvoiceNumber") == ddlInvoice.Text).Select(x => new { invdate = x.Field<DateTime>("Invoicedate"), podate = x.Field<DateTime>("PODate"), custname = x.Field<string>("Customer") }).FirstOrDefault();
                
                if (details != null)
                {
                    textinvoicedate.Text = Convert.ToDateTime(details.invdate).ToString("yyyy-MM-dd HH:mm:ss");
                    textpodate.Text = Convert.ToDateTime(details.podate).ToString("yyyy-MM-dd HH:mm:ss");
                    txtCust.Text = details.custname;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("ddlInvoice_SelectedIndexChanged:" + ex.Message);
            }
        }
        public static List<string> GetPONO(string blank)
        {
            List<string> result = new List<string>();
            try
            {
                result = DBAccess.DBAccess.getPONumber(blank);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetPONO: " + ex.Message);
            }
            return result;
        }
        public static List<string> GetAutoCompleteInvoice(string blank)
        {
            List<string> result = new List<string>();
            try
            {
                result = DBAccess.DBAccess.InvoiceAccess1(blank);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetAutoCompleteInvoice: " + ex.Message);
            }
            return result;
        }
        protected void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openConfirmModal1('');", true);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnImport_Click: " + ex.Message);
            }

        }
        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPO.Text == "" || textinvoicedate.Text == "" || textpodate.Text == "" || ddlInvoice.Text == "")
                {
                    btnDelete.Visible = false;
                    HelperClass.OpenWarningToaster(this, "PONumber and Invoice No. is not selected");
                }
                else
                {
                    pdffile.Style.Add("display", "block");
                    btnSave.Visible = true;

                    List<InvoiceDetails1> listComponents = new List<InvoiceDetails1>();
                    DateTime now = DateTime.Now;
                    filterValues1 filtValues = new filterValues1();
                    filtValues.PoNumber = txtPO.Text;
                    filtValues.InvoiceNo = ddlInvoice.Text;
                    filtValues.PoDate = Convert.ToDateTime(textpodate.Text).ToString("yyyy-MM-dd HH:mm:ss");
                    filtValues.InvoiceDt = Convert.ToDateTime(textinvoicedate.Text).ToString("yyyy-MM-dd HH:mm:ss");
                    listComponents = DBAccess.DBAccess.ViewInvoiceDetails(filtValues);

                    if (listComponents.Count > 0)
                    {
                        signalfilename.Text = listComponents[0].FileUpload;
                        Session["attFile"] = listComponents[0].attachedFile;
                    }
                    else
                    {
                        signalfilename.Text = "";
                        Session["attFile"] = null;
                    }

                    gridProducts.DataSource = listComponents;
                    gridProducts.DataBind();
                }
                string poNumber = txtPO.Text;
                if (poNumber != "")
                {
                    for (int i = 0; i < gridProducts.Rows.Count; i++)
                    {
                        string proname = ((Label)gridProducts.Rows[i].FindControl("lbl1")).Text;
                        if (proname != "")
                        {
                            DropDownList editddl = (DropDownList)gridProducts.Rows[i].FindControl("dropdown4");

                            if (editddl != null)
                            {
                                List<string> result = new List<string>();
                                result = DBAccess.DBAccess.ViewMJList(poNumber, proname, "MJList");
                                if (result.Count != 0)
                                {
                                    editddl.Items.Insert(0, new ListItem("", ""));
                                    editddl.DataSource = result;
                                    editddl.DataBind();
                                }
                                else
                                {
                                    editddl.Items.Insert(0, new ListItem("Select", ""));
                                    editddl.Items.Insert(1, new ListItem("Software", "Software"));
                                    editddl.Items.Insert(2, new ListItem("I&C", "I&C"));
                                    editddl.Items.Insert(3, new ListItem("Spares", "Spares"));
                                    editddl.DataBind();
                                }
                            }
                        }
                    }
                    btnDelete.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnView_Click: " + ex.Message);
            }
        }
        protected void SaveSignalfile_Click(object sender, EventArgs e)
        {
            Invoicetable parameter = new Invoicetable();
            try
            {
                for (int i = 0; i < gridProducts.Rows.Count; i++)
                {
                    Invoicetable details = new Invoicetable();
                    details.invoiceno = ddlInvoice.Text;
                    details.pono = txtPO.Text;
                    details.production = (gridProducts.Rows[i].FindControl("lbl1") as Label).Text;

                if (hfFile.Value == "")
                {

                }
                else
                {
                   string file = hfFile.Value;
                byte[] fileinbytes = System.Convert.FromBase64String(file.Substring(file.LastIndexOf(',') + 1));
                details.attachedFile = fileinbytes;
                details.FileUpload = hfFileName.Value;

                }

                DBAccess.DBAccess.SavePdfFile(details);
                    btnView_Click(null, null);
                    HelperClass.OpenSuccessToaster(this, "PDF file successfully saved"); 
                }
                HelperClass.OpenSuccessToaster(this, "PDF file successfully Updated");

            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("SaveSignalfile_Click:" + ex.Message);
            }
        }
        protected void signalfilename_Click(object sender, EventArgs e)
        {
            try
            {
                List<InvoiceDetails1> listComponents = new List<InvoiceDetails1>();

                DateTime now = DateTime.Now;
                filterValues1 filtValues = new filterValues1();
                filtValues.PoNumber = txtPO.Text;
                filtValues.InvoiceNo = ddlInvoice.Text;
                filtValues.PoDate = textpodate.Text;
                filtValues.InvoiceDt = textinvoicedate.Text;

                listComponents = DBAccess.DBAccess.ViewInvoiceDetails(filtValues);
                signalfilename.Text = listComponents[0].FileUpload;
                Session["attFile"] = listComponents[0].attachedFile;
                string fileextension = listComponents[0].FileUpload;
                byte[] bytes = listComponents[0].attachedFile;

                if (fileextension.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    HttpResponse response = HttpContext.Current.Response;
                    response.Clear();
                    response.ClearContent();
                    response.ClearHeaders();
                    response.Buffer = true;
                    response.ContentType = "application/force-download";
                    response.AddHeader("Content-Disposition", "attachment;filename=" + signalfilename.Text);
                    response.BinaryWrite(bytes);
                    response.Flush(); // Sends all currently buffered output to the client.
                    response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                    HttpContext.Current.ApplicationInstance.CompleteRequest();// Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("signalfilename_Click: " + ex.Message);
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPO.Text == "" || textinvoicedate.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Warning" + 1, "<script>WarningToastr('Please select PO number and Invoice number!!')</script>", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openConfirmModal('Are you sure, you want to delete this record?');", true);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnDelete_Click:" + ex.Message);
            }
        }
        protected void saveConfirmYes_ServerClick(object sender, EventArgs e)
        {
            try
            {
                filterValues1 filtValues = new filterValues1();
                filtValues.PoNumber = txtPO.Text;
                filtValues.InvoiceNo = ddlInvoice.Text;

                bool confirm = DBAccess.DBAccess.DeleteInvoiceDetails(filtValues, "Delete");
                if (confirm == false)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Success" + 1, "<script>WarningToastr('PONumber exists in Dispatch.')</script>", false);
                    pdffile.Style.Add("display", "block");
                    btnSave.Visible = true;
                }
                else
                {
                    btnClear_Click(null, null);
                    pdffile.Style.Add("display", "none");
                    btnSave.Visible = false;
                    BindPoNumber();
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Success" + 1, "<script>SuccessToastr('File deleted successfully.')</script>", false);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("saveConfirmYes_ServerClick: " + ex.Message);
            }
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                //btnView.Visible = true;
               // btnImport.Visible = true;
                txtPO.Text = "";
                ddlInvoice.Text = "";
                textinvoicedate.Text = "";
                textpodate.Text = "";
                txtCust.Text = "";
                signalfilename.Text = "";
                gridProducts.DataSource = "";
                gridProducts.DataBind();
                pdffile.Style.Add("display", "none");
                btnSave.Visible = false;
                btnDelete.Visible = false;
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnClear_Click: " + ex.Message);
            }
        }
        protected void btnVerify_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = Session["Datatable_dtQ_check"] as DataTable;
                bool verify = DBAccess.DBAccess.VerifyInvoice(dt);
                if (verify)
                {
                    foreach (DataRow dtrow in dt.Rows)
                    {
                        bool success = DBAccess.DBAccess.SaveInvoiceDataTodb(dtrow);
                        btnView.Visible = true;
                        btnImport.Visible = true;
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Success" + 1, "<script>SuccessToastr('Invoice Data is Imported Successfully')</script>", false);
                        BindPoNumber();
                    }
                }
                else
                {
                    btnImport.Visible = true;
                    btnView.Visible = true;
                    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openValidationModal('Data for Invoice already exists, Are you sure you want to load data again!!');", true);
                }
                getImportedData();
                btnView_Click(null, null);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnVerify_Click: " + ex.Message);
            }
        }
        public void getImportedData()
        {
            try
            {
                txtPO.Text = textpo1.Text;
                ddlInvoice.Text = textinvoice1.Text;
                textpodate.Text = textpdate1.Text;
                textinvoicedate.Text = textinvdate1.Text;

                DataTable listComponents = new DataTable();
                Pono filtValues = new Pono();
                filtValues.PoNo = txtPO.Text;
                listComponents = DBAccess.DBAccess.POCheckInvoice(filtValues, "ParameterDetails");
                txtCust.Text = listComponents.Rows[0]["Customer"].ToString();
                import1.Style.Add("display", "block");
                import2.Style.Add("display", "none");
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("getImportedData: " + ex.Message);
            }

        }
        protected void btnCancelImport_Click(object sender, EventArgs e)
        {
            try
            {
                import1.Style.Add("display", "block");
                import2.Style.Add("display", "none");
                btnClear.Visible = true;
                btnImport.Visible = true;
                btnView.Visible = true;
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnCancelImport_Click: " + ex.Message);
            }
        }
        protected void saveConfirmYes_ServerClick1(object sender, EventArgs e)
        {
            try
            {
                if (globe_Check.HasFile)
                {
                    //import1.Style.Add("display", "none");
                    // import2.Style.Add("display", "block");
                    importFile();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openConfirmModal1('');", false);
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Warning" + 1, "<script>WaringToastr('Please choose Excel file to import')</script>", false);
                    
                   // return;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("saveConfirmYes_ServerClick1: " + ex.Message);
            }
        }
        public void importFile()
        {
            try
            {
                int success = 0;
                DataTable dtQ_Check = new DataTable();
                if (globe_Check.HasFile)
                {
                    string fileName = globe_Check.FileName;
                    if (Path.GetExtension(fileName) != ".xlsx")
                    {
                        import1.Style.Add("display", "block");
                        import2.Style.Add("display", "none");
                         ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openConfirmModal1('');", false);
                        //HelperClass.OpenModal(this, "myConfirmationModal1", false);
                        //HelperClass.OpenWarningToaster(this,"Please choose a valid excel(.xlsx) file.");
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Warning" + 1, "<script>WarningToastr('Please choose a valid excel(.xlsx) file.')</script>", false);
                    }
                    else
                    {
                        if (!Directory.Exists(Server.MapPath("ImportedFiles")))
                        {
                            import1.Style.Add("display", "none");
                            import2.Style.Add("display", "block");
                            dtQ_Check = GetDataTableFromFile(globe_Check);
                            if (dtQ_Check != null && dtQ_Check.Rows.Count > 0)
                            {
                                Session["PONumber"] = dtQ_Check.Rows[0][12];
                                textpo1.Text = Session["PONumber"].ToString();
                                DataRow r = dtQ_Check.Rows[0];
                                Session["PDate"] = dtQ_Check.Rows[0][13];
                                var pdate = Session["PDate"].ToString().Trim();
                                textpdate1.Text = Convert.ToDateTime(pdate).ToString("yyyy-MM-dd");

                                Session["InvoiceNo"] = dtQ_Check.Rows[0][7];
                                textinvoice1.Text = Session["InvoiceNo"].ToString();

                                Session["InvoiceDt"] = dtQ_Check.Rows[0][0];
                                var indate = Session["InvoiceDt"].ToString().Trim();
                                textinvdate1.Text = Convert.ToDateTime(indate).ToString("yyyy-MM-dd");
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "Warning" + 1, "<script>WarningToastr('Import failed. Empty excel file.')</script>", false);
                                return;
                            }
                        }
                        else
                        {
                            import1.Style.Add("display", "none");
                            import2.Style.Add("display", "block");
                            List<string> PONumber = new List<string>();
                            List<string> ProductNam = new List<string>();
                            List<string> MJNo = new List<string>();
                            string podate = string.Empty;
                            dtQ_Check = GetDataTableFromFile(globe_Check);
                            if (dtQ_Check != null && dtQ_Check.Rows.Count > 0)
                            {
                                Session["Datatable_dtQ_check"] = dtQ_Check;

                                Session["PONumber"] = dtQ_Check.Rows[0][7];
                                textpo1.Text = Session["PONumber"].ToString();

                                Session["PDate"] = dtQ_Check.Rows[0][8];
                                var pdate = Session["PDate"].ToString().Trim();
                                textpdate1.Text = Convert.ToDateTime(pdate).ToString("yyyy-MM-dd");

                                Session["InvoiceNo"] = dtQ_Check.Rows[0][0];
                                textinvoice1.Text = Session["InvoiceNo"].ToString();

                                Session["InvoiceDate"] = dtQ_Check.Rows[0][1];
                                var indate = Session["InvoiceDate"].ToString().Trim();
                                textinvdate1.Text = Convert.ToDateTime(indate).ToString("yyyy-MM-dd");


                                gridTally.DataSource = dtQ_Check;
                                gridTally.DataBind();
                                btnView.Visible = false;
                                btnImport.Visible = false;
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "Warning" + 1, "<script>WarningToastr('Import failed. Empty excel file.')</script>", false);
                                return;
                            }
                        }
                    }
                    if (success <= 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Success" + 1, "<script>WarningToastr('Import failed. Some production orders in excel file already exists.')</script>", true);
                        return;
                    }
                    else if (success > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Success" + 1, "<script>SuccessToastr('Excel file imported successfully.')</script>", false);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Success" + 1, "<script>WarningToastr('Please choose a valid file to import.')</script>", false);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("importFile:" + ex.Message);
            }
        }
        private DataTable GetDataTableFromFile(FileUpload fileName)
        {
            string invoice = "", invoicedate = "";
            string poNumber = "", poDate = "", supplierRef = "";
            string quantityUnit = "";
            DataTable dtProdOrder = new DataTable();
            ExcelPackage Excel = new ExcelPackage(fileName.PostedFile.InputStream);
            var worksheet = Excel.Workbook.Worksheets.First();

            try
            {
                List<string> excelData = new List<string>();
                var tbl = new DataTable();
                var wsProductionorder = Excel.Workbook.Worksheets[0];

                dtProdOrder.Columns.Add("InvoiceNumber");
                dtProdOrder.Columns.Add("Invoicedate");
                dtProdOrder.Columns.Add("ProductionName");
                dtProdOrder.Columns.Add("Quantity");
                dtProdOrder.Columns.Add("Suppliercode");
                dtProdOrder.Columns.Add("Unit");
                dtProdOrder.Columns.Add("ProductDescription");
                dtProdOrder.Columns.Add("PONumber");
                dtProdOrder.Columns.Add("PODate");
                dtProdOrder.Columns.Add("InvoiceValue");

                for (int row1 = worksheet.Dimension.Start.Row; row1 <= worksheet.Dimension.End.Row; row1++)
                {
                    if (worksheet.Cells[row1, 1].Value.ToString().Equals("AceMicromatic Manufacturing Intelligence Tech Pvt.Ltd", StringComparison.OrdinalIgnoreCase))
                    {
                        invoice = worksheet.Cells["G4"].Value.ToString();
                        invoicedate = worksheet.Cells["J4"].Value.ToString();
                        supplierRef = worksheet.Cells["G8"].Value.ToString();
                        poNumber = worksheet.Cells["G10"].Value.ToString();
                        poDate = worksheet.Cells["J10"].Value.ToString();
                        break;
                    }
                }
                int i = worksheet.Dimension.Start.Row;
                while (!(worksheet.Cells[i, 1].Value.ToString().Equals("Sl", StringComparison.OrdinalIgnoreCase)))
                {
                    i++;
                }
                int startrow = i;
                int endrow = i;

                try
                {
                    while (((worksheet.Cells[i, 1].Value == null ? "" : worksheet.Cells[i, 1].Value.ToString()) != "") ||
                        ((worksheet.Cells[i, 2].Value == null ? "" : worksheet.Cells[i, 2].Value.ToString()) != "") || !((worksheet.Cells[i, 3].Value == null ? "" : worksheet.Cells[i, 3].Value.ToString()).Contains("Output")))
                    {

                        if (((worksheet.Cells[i, 1].Value == null ? "" : worksheet.Cells[i, 1].Value.ToString()) == "") &&
                        ((worksheet.Cells[i, 2].Value == null ? "" : worksheet.Cells[i, 2].Value.ToString()) == "") && !((worksheet.Cells[i, 3].Value == null ? "" : worksheet.Cells[i, 3].Value.ToString()).Contains("Output")))
                        {
                            break;
                        }
                        endrow++;
                        i++;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error while importing production order excel file : " + ex.Message);
                }

                string[] unitq = new string[0];
                double qty = 0.00; string secndcol = "";
                for (int s = startrow + 2; s < endrow; s++)
                {
                    if (worksheet.Cells[s, 1].Value == null || worksheet.Cells[s, 2].Value == null)
                    {
                        if (worksheet.Cells[s, 1].Value == null)
                            worksheet.Cells[s, 1].Value = "";
                        if (worksheet.Cells[s, 2].Value == null)
                            worksheet.Cells[s, 2].Value = "";
                    }
                    if (string.IsNullOrEmpty(worksheet.Cells[s, 1].Value.ToString()) && !string.IsNullOrEmpty(worksheet.Cells[s, 2].Value.ToString()))
                    {
                        secndcol += worksheet.Cells[s, 2].Value.ToString();
                    }
                    else if (!string.IsNullOrEmpty(worksheet.Cells[s, 1].Value.ToString()) && !string.IsNullOrEmpty(worksheet.Cells[s, 2].Value.ToString()))
                    {
                        amount = Convert.ToInt32(worksheet.Cells[s, 13].Value);

                        var style1 = worksheet.Cells[s, 10].Style;
                        string format = style1.Numberformat.Format;
                        format = format.Replace("\"", "");
                        unitq = format.Split(' ');
                        quantityUnit = worksheet.Cells[s, 10].Value.ToString();
                        double.TryParse(quantityUnit, out qty);
                        if (dtProdOrder.Rows.Count > 0)
                            dtProdOrder.Rows[dtProdOrder.Rows.Count - 1]["ProductDescription"] = secndcol;
                        DataRow row = dtProdOrder.Rows.Add();
                        row["ProductionName"] = worksheet.Cells[s, 2].Value.ToString();

                        row["Unit"] = unitq[1];
                        row["Quantity"] = qty.ToString("0.00");
                        row["InvoiceNumber"] = invoice;
                        row["InvoiceValue"] = amount;
                        row["Invoicedate"] = invoicedate;
                        row["Suppliercode"] = supplierRef;
                        row["PONumber"] = poNumber;
                        row["PODate"] = poDate;
                        secndcol = "";
                    }

                }
                if (dtProdOrder.Rows.Count > 0)
                    dtProdOrder.Rows[dtProdOrder.Rows.Count - 1]["ProductDescription"] = secndcol;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while importing production order excel file : " + ex.Message);
            }

            return dtProdOrder;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool success = false;

            Invoicetable parameter = new Invoicetable();
            try
            {
                for (int i = 0; i < gridProducts.Rows.Count; i++)
                {
                    Invoicetable details = new Invoicetable();
                    details.invoiceno = ddlInvoice.Text;
                    details.pono = txtPO.Text;
                    details.production = (gridProducts.Rows[i].FindControl("lbl1") as Label).Text;
                    details.mjno = (gridProducts.Rows[i].FindControl("dropdown4") as DropDownList).SelectedItem.Text;
                    details.customer = txtCust.Text;
                    success = DBAccess.DBAccess.UpdateInvoice(details);
                    HelperClass.OpenSuccessToaster(this, "Invoice Details Successfully Updated");
                    List<InvoiceDetails1> listComponents = new List<InvoiceDetails1>();

                    DateTime now = DateTime.Now;
                    filterValues1 filtValues = new filterValues1();
                    filtValues.PoNumber = txtPO.Text;
                    filtValues.InvoiceNo = ddlInvoice.Text;
                    filtValues.PoDate = textpodate.Text;
                    filtValues.InvoiceDt = textinvoicedate.Text;
                    listComponents = DBAccess.DBAccess.ViewInvoiceDetails(filtValues);

                    pdffile.Style.Add("display", "block");
                    btnSave.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnSave_Click:" + ex.Message);
            }
        }
        protected void gridProducts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlmj = (DropDownList)e.Row.FindControl("dropdown4");
                ddlmj.SelectedValue = DataBinder.Eval(e.Row.DataItem, "mjNumber").ToString();
            }
        }

    }
}
