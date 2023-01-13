using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ADES_22.Model;
using ADES_22.DBAccess;
using System.Web.Services;
using System.Web.Script.Services;
using System.IO;
using OfficeOpenXml;

namespace ADES_22
{
    public partial class Import_MJData : System.Web.UI.Page
    {
        public static string[] formats = new string[] { "d/MM/yyyy", "d/M/yy", "dd/M/yyyy", "dd-MM-yy", "dd/MM/yy", "d-M-yy", "d-MM-yy", "d/M/yyyy", "dd/MM/yyyy", "MM/dd/yyyy", "yyyy/MM/dd", "DD/MM/yyyy", "dd/MMM/yyyy", "dd-MM-yyyy HH:mm:ss", "dd-MM-yyyy HH:mm", "dd-MM-yyyy", "dd-MMM-yyyy", "dd-MMM-yyyy HH:mm", "dd-MMM-yyyy HH:mm:ss", "yyyy-MM-dd", "yyyy-MM-dd HH:mm:ss", "dd-MM-yyyyTHH:mm:ss", "dd-MM-yyyyTHH:mm", "dd-MMM-yyyyTHH:mm", "dd-MMM-yyyyTHH:mm:ss", "yyyy-MM-ddTHH:mm:ss", "yyyy-MM-ddTHH:mm" };
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((String)Session["login"] != "Yes" || (String)Session["login"] == null)
            {
                Response.Redirect("~/Login.aspx", false);
            }

            import1.Visible = true;
            import2.Visible = false;
        }

        [WebMethod]
        public static List<string> GetAutoCompleteData(string blank)
        {
            List<string> result = new List<string>();

            try
            {
                result = DBAccess.DBAccess.POAccess(blank);   
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("GetAutoCompleteData: " + ex.Message);
            }

            return result;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<dataList> SearchPODetails(string blank)
        {
            List<dataList> result = new List<dataList>();

            try
            {
                DataTable listComponents = new DataTable();

                Pono filtValues = new Pono
                {
                    PoNo = blank
                };

                listComponents = DBAccess.DBAccess.POCheck(filtValues, "ParameterDetails");

                for (int i = 0; i < listComponents.Rows.Count; i++)
                {
                    dataList podata = new dataList
                    {
                        prodname = listComponents.Rows[i]["ProductName"].ToString(),
                        mjnum = listComponents.Rows[i]["MJNumber"].ToString()
                    };
                    var pdate = listComponents.Rows[i]["PODate"].ToString();
                    podata.podate = Convert.ToDateTime(pdate).ToString("yyyy-MM-dd HH:mm:ss");
                    result.Add(podata);
                }
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("SearchPODetails: " + ex.Message);
            }

            return result;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string SearchMJ(string poNum, string prodName)
        {
            string result = string.Empty;

            try
            {
                result = DBAccess.DBAccess.GetMjNum(poNum, prodName, "ParameterDetails");   
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("SearchMJ: " + ex.Message);
            }

            return result;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string SearchMJprod(string poNum, string mjNo)
        {
            string result = string.Empty;

            try
            {
                result = DBAccess.DBAccess.GetMjProduct(poNum, mjNo, "ParameterDetails");
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("SearchMJprod: " + ex.Message);
            }

            return result;
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtpodate.Text == string.Empty)
                {
                    HelperClass.OpenWarningToaster(this, "Please select a PO number!");
                    return;
                }
                else
                {
                    List<purchaseOrderDetails> listComponents = new List<purchaseOrderDetails>();
                    DateTime now = DateTime.Now;

                    filtervalues filtValues = new filtervalues
                    {
                        PoNumber = txtpo.Value,
                        MJnumber = hiddenMj.Value,
                        Product = hdnProdName.Value,
                        PoDate = Convert.ToDateTime(txtpodate.Text).ToString("yyyy-MM-dd")
                    };

                    listComponents = DBAccess.DBAccess.ViewPoDetails(filtValues);

                    gridContainer1.Style.Add("Height", "440px");
                    gridTally.Visible = true;
                    gridTally.DataSource = listComponents;
                    gridTally.DataBind();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnView_Click: " + ex.Message);
            }
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtpo.Value == string.Empty)
                    Session["gridpo"] = "No"; //Import
                else
                    Session["gridpo"] = "Yes"; //View and then Import

                HelperClass.OpenModal(this, "FileUploadModal", true);
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("btnImport_Click: " + ex.Message);
            }
        }

        protected void saveConfirmYes1_ServerClick(object sender, EventArgs e) //Import button
        {
            try
            {
                Session["mj"] = hiddenMj.Value;
                Session["pn"] = hdnProdName.Value;
                Session["grid"] = gridTally2.DataSource;
                Session["date"] = txtpodate.Text;
                Session["po"] = txtpo.Value;

                int cnt = 0;
                string fileName = globe_Check.FileName;

                if (Path.GetExtension(fileName) != ".xlsx")
                {
                    cnt = 1;
                    HelperClass.OpenWarningToaster(this, "Please choose a valid excel (.xlsx) file!");  
                    HelperClass.OpenModal(this, "FileUploadModal",false);
                }
                else
                {
                    import1.Visible = false;
                    import2.Visible = true;
                    ImportFile();
                }

                if (cnt == 1)
                {
                    ddlpname.Items.Insert(0, new ListItem((String)Session["pn"], ""));
                    ddlmjnum.Items.Insert(0, new ListItem((String)Session["mj"], ""));
                }
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("saveConfirmYes1_ServerClick: " + ex.Message);
            }
        }

        public void ImportFile()
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
                        HelperClass.OpenModal(this, "FileUploadModal", false);
                        HelperClass.OpenWarningToaster(this, "Please choose a valid excel (.xlsx) file!");
                        return;
                    }
                    else
                    {
                        if (!Directory.Exists(Server.MapPath("ImportedFiles")))
                        {
                            Directory.CreateDirectory(Server.MapPath("ImportedFiles"));
                            string savedFileName = Server.MapPath("ImportedFiles//" + fileName);
                            globe_Check.SaveAs(savedFileName);
                            dtQ_Check = GetDataTableFromFile(globe_Check);
                            if (dtQ_Check != null && dtQ_Check.Rows.Count > 0)
                            {
                                Session["PONumber"] = dtQ_Check.Rows[0][12];
                                txtPo1.Text = Session["PONumber"].ToString();
                                DataRow r = dtQ_Check.Rows[0];
                                DateTime date = DateTime.Now;
                                Session["PDate"] = dtQ_Check.Rows[0][13].ToString().Trim();
                                var pdate = Session["PDate"].ToString().Trim();
                                POdate1.Text = Convert.ToDateTime(pdate).ToString("yyyy-MM-dd");
                                Session["ProductName"] = dtQ_Check.Rows[0][7];
                                ProdName1.Text = Session["ProductName"].ToString();
                                ProdName1.ToolTip = ProdName1.Text;
                                Session["MJNo"] = dtQ_Check.Rows[0][0];
                                MJno1.Text = Session["MJNo"].ToString();

                                import1.Visible = false;
                                gridTally.DataSource = "";
                                gridTally.DataBind();
                                gridTally.Visible = false;
                                gridTally2.DataSource = dtQ_Check;
                                gridTally2.DataBind();
                            }
                            else
                            {
                                HelperClass.OpenWarningToaster(this, "Import failed. Empty excel file!");
                                return;
                            }
                        }
                        else
                        {
                            List<string> PONumber = new List<string>();
                            List<string> ProductNam = new List<string>();
                            List<string> MJNo = new List<string>();

                            string podate = string.Empty;

                            string savedFileName = Server.MapPath("ImportedFiles//" + fileName);
                            globe_Check.SaveAs(savedFileName);
                            dtQ_Check = GetDataTableFromFile(globe_Check);

                            if (dtQ_Check != null && dtQ_Check.Rows.Count > 0)
                            {
                                Session["Datatable_dtQ_check"] = dtQ_Check;
                                Session["PONumber"] = dtQ_Check.Rows[0][12];
                                txtPo1.Text = Session["PONumber"].ToString();
                                DateTime date = DateTime.Now;
                                Session["PDate"] = dtQ_Check.Rows[0][13].ToString().Trim();
                                var pdate = Session["PDate"].ToString().Trim();
                                POdate1.Text = Convert.ToDateTime(pdate).ToString("yyyy-MM-dd");
                                Session["ProductName"] = dtQ_Check.Rows[0][7];
                                ProdName1.Text = Session["ProductName"].ToString();
                                ProdName1.ToolTip = ProdName1.Text;
                                Session["MJNo"] = dtQ_Check.Rows[0][0];
                                MJno1.Text = Session["MJNo"].ToString();

                                gridContainer1.Style.Add("Height", "0px");
                                import1.Visible = false;
                                gridTally.DataSource = "";
                                gridTally.DataBind();
                                gridTally.Visible = false;
                                gridTally2.DataSource = dtQ_Check;
                                gridTally2.DataBind();
                            }
                            else
                            {
                                HelperClass.OpenWarningToaster(this, "Import failed. Empty excel file!");
                                return;
                            }
                        }
                    }

                    if (success != 0)
                    {
                        HelperClass.OpenWarningToaster(this, "Import failed. Some production orders in excel file already exists.");
                        return;
                    }
                    else if (success == 0)
                    {
                        HelperClass.OpenSuccessToaster(this, "Excel file imported successfully!");
                        return;
                    }
                }
                else
                {
                    HelperClass.OpenWarningToaster(this, "Please choose a file to import!");
                    HelperClass.OpenModal(this, "FileUploadModal", true);

                    import1.Visible = true;
                    import2.Visible = false;
                }
            }
            catch (Exception ex)
            {
                if (Session["PDate"] != null)
                {
                    Logger.WriteErrorLog(ex.Message + "PO Date" + Session["PDate"].ToString());
                }
                else
                {
                    Logger.WriteErrorLog("ImportFile: " + ex.Message);
                }
            }
        }

        private DataTable GetDataTableFromFile(FileUpload fileName)
        {
            string mj = string.Empty, mjdate = string.Empty;
            string itemname = string.Empty, batchno = string.Empty, quantity = string.Empty, finalproduct = string.Empty, serialno = string.Empty, quantity1 = string.Empty;

            DataTable dtProdOrder = new DataTable();
            ExcelPackage Excel = new ExcelPackage(fileName.PostedFile.InputStream);
            var worksheet = Excel.Workbook.Worksheets[0];

            try
            {
                List<string> excelData = new List<string>();
                var tbl = new DataTable();
                var wsProductionorder = Excel.Workbook.Worksheets[0];

                dtProdOrder.Columns.Add("MJNo");
                dtProdOrder.Columns.Add("MJDate");
                dtProdOrder.Columns.Add("ItemName");
                dtProdOrder.Columns.Add("Godown");
                dtProdOrder.Columns.Add("SerialNo");
                dtProdOrder.Columns.Add("Quantity");
                dtProdOrder.Columns.Add("Unit");
                dtProdOrder.Columns.Add("ProductName");
                dtProdOrder.Columns.Add("GoDown1");
                dtProdOrder.Columns.Add("BatchNo");
                dtProdOrder.Columns.Add("ProductQuantity");
                dtProdOrder.Columns.Add("ProductUnit");
                dtProdOrder.Columns.Add("PONumber");
                dtProdOrder.Columns.Add("PODate");

                for (int row1 = worksheet.Dimension.Start.Row; row1 <= worksheet.Dimension.End.Row; row1++)
                {
                    if (worksheet.Cells[row1, 1].Value == null)
                    {
                        break;
                    }
                    else
                    {
                        if (worksheet.Cells[row1, 1].Value.ToString().Equals("Manufacturing Journal Voucher", StringComparison.OrdinalIgnoreCase))
                        {
                            mj = worksheet.Cells[row1 + 1, 2].Value.ToString();
                            mjdate = worksheet.Cells[row1 + 1, 4].Value.ToString();
                            DateTime dt = DateTime.FromOADate(Convert.ToInt32(mjdate));
                            break;
                        }
                    }
                }

                int i = worksheet.Dimension.Start.Row;
                while (!(worksheet.Cells[i, 1].Value.ToString().Equals("Source (Consumption)", StringComparison.OrdinalIgnoreCase)))
                {
                    i++;
                }
                
                int startrow = i;
                int endrow = i;

                try
                {
                    while (worksheet.Cells[i, 1].Value == null || !worksheet.Cells[i, 1].Value.ToString().Equals("Destination (Production)"))
                    {

                        endrow++;
                        i++;
                    }
                }
                catch (Exception)
                {

                }

                for (int s = startrow + 1; s < endrow; s++)
                {
                    if (worksheet.Cells[s, 2].Value == null)
                    {

                    }
                    else if (worksheet.Cells[s, 2].Value.ToString().Equals("Main Location", StringComparison.OrdinalIgnoreCase))
                    {
                        itemname = worksheet.Cells[s, 1].Value.ToString();

                    }
                    else if (!worksheet.Cells[s, 2].Value.ToString().Equals("Main Location", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(worksheet.Cells[s, 2].Value.ToString()))
                    {
                        serialno = worksheet.Cells[s, 1].Value.ToString();
                        quantity = worksheet.Cells[s, 2].Value.ToString();
                        var style1 = worksheet.Cells[s, 2].Style;
                        string format = style1.Numberformat.Format;
                        format = format.Replace("\"", "");
                        string[] unitq = format.Split(' ');
                        string str1 = worksheet.Cells[s, 2].Value.ToString();
                        double qty = 0.00;
                        double.TryParse(quantity, out qty);
                        DataRow dtrow = dtProdOrder.Rows.Add();
                        dtrow["MJNo"] = mj;
                        DateTime dt = DateTime.FromOADate(Convert.ToDouble(mjdate));
                        dtrow["MJDate"] = dt;
                        dtrow["ItemName"] = itemname;
                        dtrow["GoDown"] = "Main Location";
                        dtrow["SerialNo"] = serialno;
                        dtrow["Quantity"] = qty.ToString("0.00");
                        dtrow["Unit"] = unitq[1];
                    }
                }

                finalproduct = worksheet.Cells[endrow + 1, 1].Value.ToString();
                batchno = worksheet.Cells[endrow + 2, 1].Value.ToString();
                quantity1 = worksheet.Cells[endrow + 2, 2].Value.ToString();
                var style = worksheet.Cells[endrow + 2, 2].Style;
                string quant1 = style.Numberformat.Format;
                quant1 = quant1.Replace("\"", "");
                string[] unitp = quant1.Split(' ');
                string str2 = worksheet.Cells[endrow + 2, 2].Value.ToString();
                double qty1 = 0.00;
                double.TryParse(quantity1, out qty1);

                while (!worksheet.Cells[i, 1].Value.ToString().Equals("Narration:", StringComparison.OrdinalIgnoreCase))
                {
                    i++;
                }

                if (worksheet.Cells[i, 1].Value.ToString().Contains("Narration:"))
                {
                    string str3 = worksheet.Cells[i, 2].Value.ToString();
                    string[] po = str3.Split('>');
                    for (int c = 0; c < po.Length - 1; c++)
                    {
                        po[c] = po[c].Replace("<", "");
                    }
                    for (int j = 0; j < dtProdOrder.Rows.Count; j++)
                    {
                        dtProdOrder.Rows[j]["PONumber"] = po[0].ToString();
                        DateTime date = DateTime.Now;
                        string po1 = po[1].ToString().Trim();
                        DateTime.TryParseExact(po1, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date);
                        dtProdOrder.Rows[j]["PODate"] = date;
                        dtProdOrder.Rows[j]["ProductName"] = finalproduct;
                        dtProdOrder.Rows[j]["GoDown1"] = "Production";
                        dtProdOrder.Rows[j]["BatchNo"] = batchno;
                        dtProdOrder.Rows[j]["ProductQuantity"] = qty1.ToString("0.00");
                        dtProdOrder.Rows[j]["ProductUnit"] = unitp[1];
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetDataTableFromFile: " + ex.Message);
            }

            return dtProdOrder;
        }

        protected void btnVerify_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = Session["Datatable_dtQ_check"] as DataTable;
                bool verify = DBAccess.DBAccess.Verify(dt); //Checks if data to be stored is already present in DB

                if (verify) //If not present
                {
                    string success = "";
                    foreach (DataRow dtrow in dt.Rows)
                    {
                        success = (DBAccess.DBAccess.SaveImportDataTodb(dtrow)).ToString();
                    }

                    if (success == "Inserted")
                    {
                        HelperClass.OpenSuccessToaster(this, "Purchase order details saved successfully!");

                        import1.Visible = true;
                        import2.Visible = false;
                        gridTally.Visible = true;

                        txtpo.Value = string.Empty;
                        txtpodate.Text = string.Empty;
                        ddlmjnum.Text = string.Empty;
                        ddlpname.Text = string.Empty;
                    }
                    else
                    {
                        HelperClass.OpenErrorModal(this, "Error, while saving records!");

                        filtervalues filtValues = new filtervalues
                        {
                            PoNumber = txtPo1.Text,
                            MJnumber = MJno1.Text
                        };

                        DBAccess.DBAccess.DeletePoDetails(filtValues, "Delete");
                    }
                }
                else //If present
                {
                    HelperClass.OpenModal(this, "myTimeValidation", true);

                    import1.Visible = false;
                    import2.Visible = true;
                    gridTally2.Visible = true;
                    return;
                }

                GetImportedData();
                btnView_Click(null, null);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnVerify_Click: " + ex.Message);
            }
        }

        protected void LoadData_Yes_ServerClick(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = Session["Datatable_dtQ_check"] as DataTable;
                string success = string.Empty;

                foreach (DataRow dtrow in dt.Rows)
                {
                    success = (DBAccess.DBAccess.SaveImportDataTodb(dtrow)).ToString();
                }

                if (success == "Updated")
                {
                    HelperClass.OpenSuccessToaster(this, "Purchase order details updated successfully!");
                    import1.Visible = true;
                    import2.Visible = false;
                    gridTally.Visible = true;
                    txtpo.Value = string.Empty;
                    txtpodate.Text = string.Empty;
                    ddlmjnum.Text = string.Empty;
                    ddlpname.Text = string.Empty;
                }
                else if (success == "FALSE")
                    HelperClass.OpenWarningModal(this, "PO already exists in Invoice!");
                else
                {
                    HelperClass.OpenErrorModal(this, "Error, while updating records!");
                    return;
                }

                GetImportedData();
                btnView_Click(null, null);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("LoadData_Yes_ServerClick: " + ex.Message);
            }
        }

        protected void LoadData_No_ServerClick(object sender, EventArgs e)
        {
            try
            {
                import1.Visible = true;
                import2.Visible = false;
                gridTally.Visible = true;

                txtpo.Value = (String)Session["po"];
                txtpodate.Text = (String)Session["date"];

                ddlmjnum.Items.Insert(0, new ListItem((String)Session["mj"], ""));
                ddlpname.Items.Insert(0, new ListItem((String)Session["pn"], ""));

                GetImportedData();
                btnView_Click(null, null);
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("LoadData_No_ServerClick: " + ex.Message);
            }
        }

        public void GetImportedData()
        {
            try
            {
                txtpo.Value = txtPo1.Text;
                txtpodate.Text = POdate1.Text;
                string selProdName = ProdName1.Text;
                ddlpname.Items.Insert(0, new ListItem(selProdName, ""));
                hdnProdName.Value = selProdName;
                string selMjNo = MJno1.Text;
                ddlmjnum.Items.Insert(0, new ListItem(selMjNo, ""));
                hiddenMj.Value = selMjNo;
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetImportedData: " + ex.Message);
            }
        }

        protected void btnCancelImport_Click(object sender, EventArgs e)
        {
            try
            {
                import1.Visible = true;
                import2.Visible = false;
                gridTally.Visible = true;

                txtpo.Value = (String)Session["po"];
                txtpodate.Text = (String)Session["date"];
                ddlmjnum.Items.Clear();
                ddlpname.Items.Clear();
                ddlmjnum.Items.Insert(0, new ListItem((String)Session["mj"], ""));
                ddlpname.Items.Insert(0, new ListItem((String)Session["pn"], ""));

                if ((String)Session["gridpo"] == "No")
                    gridTally.Visible=false;
                else
                    btnView_Click(null, null);
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("btnCancelImport_Click: " + ex.Message);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtpodate.Text == string.Empty)
                    HelperClass.OpenWarningToaster(this, "Please select a PO number!");
                else
                    HelperClass.OpenModal(this, "myConfirmationModal", true);
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("btnDelete_Click: " + ex.Message);
            }
        }

        protected void saveConfirmYes_ServerClick(object sender, EventArgs e) //Delete button
        {
            string abc = hiddenMj.Value;
            string mj = hiddenMj.Value;
            string pn = hdnProdName.Value;
            int cnt = 0;

            try
            {
                if (txtpo.Value == string.Empty || abc == string.Empty)
                {
                    HelperClass.OpenWarningToaster(this, "Please select PO and MJ number!");
                    return;
                }
                else
                {
                    filtervalues filtValues = new filtervalues
                    {
                        PoNumber = txtpo.Value,
                        MJnumber = abc.ToString()
                    };

                    bool confirm = DBAccess.DBAccess.DeletePoDetails(filtValues, "Delete");

                    if (confirm == true)
                    {
                        HelperClass.ClearModal(this);
                        HelperClass.OpenSuccessToaster(this, "File deleted successfully!");
                        btnClear_Click(null, null);
                    }
                    else
                    {
                        cnt = 1;
                        HelperClass.OpenWarningToaster(this, "PO already exists in Invoice!");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("saveConfirmYes_ServerClick: " + ex.Message);
            }

            if (cnt == 1)
            {
                ddlpname.Items.Insert(0, new ListItem(pn, ""));
                ddlmjnum.Items.Insert(0, new ListItem(mj, ""));
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtpo.Value = string.Empty;
                txtpodate.Text = string.Empty;
                ddlpname.Text = string.Empty;
                ddlpname.Items.Clear();
                ddlmjnum.Text = string.Empty;
                ddlmjnum.Items.Clear();
                hdnProdName.Value = string.Empty;
                hiddenMj.Value = string.Empty;
                gridTally.DataSource = string.Empty;
                gridTally.DataBind();
                gridTally.Visible = false;
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog("btnClear_Click: " + ex.Message);
            }
        }
    }
}