
using ADES_22.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using iTextSharp.text.html.simpleparser;


namespace ADES_22
{
    public partial class DispatchProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["connectionString"] == null)

                if (!IsPostBack)
                {
                    //DateTime now = DateTime.Now;
                    //var startDate = new DateTime(now.Year, now.Month, 1);
                    //var endDate = startDate.AddMonths(1).AddDays(-1);
                    //txtFDate.Text = startDate.ToString("yyyy-MM-dd");
                    //txtEDate.Text = endDate.ToString("yyyy-MM-dd");

                    List<string> list = BindPoNo("");
                    StringBuilder stringBuilder = new StringBuilder();
                    for (int i = 0; i < list.Count; i++)
                    {
                        stringBuilder.Append(string.Format("<option value='{0}'>", list[i]));
                    }
                    dlPONumber.InnerHtml = stringBuilder.ToString();
                    getDispatchDetails();
                }
        }
        protected void txtPONumber_TextChanged(object sender, EventArgs e)
        {
            try
            {
                List<string> list = BindInvoiceNo(txtPONumber.Text);
                ddlInvoiceNumber.DataSource = list;
                ddlInvoiceNumber.DataBind();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("txtPONumber_TextChanged: " + ex.Message);
            }
        }
        public void loadInvoiceValue()
        {
            try
            {
                if (hdnInvoice.Value != "")
                {
                    ddlInvoiceNumber.Items.Clear();
                    List<string> result = new List<string>();
                    result = DBAccess.DBAccess.getInvoiceNo("InvoiceList", txtPONumber.Text);
                    ddlInvoiceNumber.DataSource = result;
                    ddlInvoiceNumber.DataBind();
                    if (ddlInvoiceNumber.Items.Count > 0)
                    {
                        ddlInvoiceNumber.Items.FindByText(hdnInvoice.Value.ToString()).Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
        }
        public void getDispatchDetails()
        {
            try
            {
                List<DispatchDetails1> DisEntry = new List<DispatchDetails1>();
                FilterDisDetails ddlValue = new FilterDisDetails();
                ddlValue.PoNo = txtPONumber.Text.ToString();
                //ddlValue.InvoiceNo = hdnInvoice.Value.ToString();
                ddlValue.InvoiceNo = ddlInvoiceNumber.Text.ToString();
                ddlValue.FromDate = txtFDate.Text;
                ddlValue.ToDate = txtEDate.Text;

                Session["DispatchDetails"] = DisEntry = DBAccess.DBAccess.getDispatchDetailValue(ddlValue);
                GridView1.DataSource = DisEntry;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("getDispatchDetails:" + ex.Message);
            }
        }
        public static List<string> BindPoNo(string blank)
        {
            List<string> result = new List<string>();
            try
            {
                result = DBAccess.DBAccess.getPONO("POList", blank);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("getPoN: " + ex.Message);
            }
            return result;
        }
        public static List<string> BindAddablePoNo(string blank)
        {
            List<string> result = new List<string>();
            try
            {
                DataTable dt = new DataTable();
                dt = DBAccess.DBAccess.getAddablePoNo("POInvoiceList");
                result = dt.AsEnumerable().Select(x => x.Field<string>("PONumber")).ToList();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindAddablePoNo: " + ex.Message);
            }

            return result;
        }
        public static List<string> BindInvoiceNo(string blank)
        {
            List<string> result = new List<string>();
            try
            {
                result = DBAccess.DBAccess.getInvoiceNo("InvoiceList", blank);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetInvoiceNo: " + ex.Message);
            }
            return result;
        }
        public void BindCourierName()
        {
            try
            {
                addCourierName.DataSource = HelperClass.GetCourierName();
                addCourierName.DataTextField = "Text";
                addCourierName.DataValueField = "Value";
                addCourierName.DataBind();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindCourierddl: " + ex.Message);
            }
        }
        public void BindStatus()
        {
            try
            {
                addStatus.DataSource = HelperClass.GetStatusValue();
                addStatus.DataTextField = "Text";
                addStatus.DataValueField = "Value";
                addStatus.DataBind();

            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindStatus: " + ex.Message);
            }
        }
        public void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFDate.Text != "" && txtEDate.Text != "")
                {
                    var startDate = Convert.ToDateTime(txtFDate.Text.Trim());
                    var endDate = Convert.ToDateTime(txtEDate.Text.Trim());
                    if (endDate < startDate)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openValidationModal('To date must be greater than From date!!!');", true);
                    }
                }
                if (txtPONumber.Text == "")
                 {
                     HelperClass.OpenWarningToaster(this, "Please enter PONumber");
                 }
                 else
                 {
                getDispatchDetails();
                 }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnView_Click: " + ex.Message);
            }
        }
        public void NewEntry_Click(object sender, EventArgs e)
        {
            try
            {
                addPoNo.Text = "";
                List<string> list = BindAddablePoNo("");
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < list.Count; i++)
                {
                    stringBuilder.Append(string.Format("<option value='{0}'>", list[i]));
                }
                dlPolists.InnerHtml = stringBuilder.ToString();

                if (addInvNo.Items.FindByText("") != null)
                {
                    addInvNo.Text = "";
                }
                addInvDate.Text = "";
                addInvValue.Text = " ";
                addProdName.Text = "";
                addReceivedBy.Text = "";
                BindStatus();
                addRegion.Text = "";
                addEmail.Text = "";
                addCustomer.Text = "";
                BindCourierName();
                addConsignName.Text = "";
                addContent.Text = "";
                addDeliveryDate.Text = "";
                addFileName.Text = " ";
                hfFile.Value = " ";
                hfFileName.Value = " ";
                addPoNo.Enabled = true;
                addInvNo.Enabled = true;
                Addhff.Value = "New";
                AddButton.Text = "Add";
                DispatchTitle.Text = "Add Dispatch Details";
                HelperClass.OpenModal(this, "newEntry1", true);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("NewEntry_Click:" + ex.Message);
            }

        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtPONumber.Text = "";
                ddlInvoiceNumber.Items.Clear();
                txtFDate.Text = "";
                txtEDate.Text = ""; 
                getDispatchDetails();
                btnNewEntry.Visible = true;

            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnClear_Click: " + ex.Message);
            }
        }
        protected void Saveclick_Click(object sender, EventArgs e)
        {
            try
            {
                DispatchDetails1 DisEntry = new DispatchDetails1();
                DisEntry.InvoiceNo = addInvNo.Text;
                DisEntry.InvoiceDate = addInvDate.Text;
                DisEntry.InvoiceValue = addInvValue.Text;
                DisEntry.PoNumber = addPoNo.Text;
                DisEntry.PODate = hdnPoDate.Value;
                DisEntry.Customer = addCustomer.Text;
                DisEntry.Region = addRegion.Text;
                DisEntry.ProdName = addProdName.Text;
                DisEntry.ConsignName = addConsignName.Text;
                DisEntry.CourierName = addCourierName.SelectedValue;
                DisEntry.Content = addContent.Text;
                DisEntry.Email = addEmail.Text;
                DisEntry.Status = addStatus.SelectedValue;
                DisEntry.DeliveryDate = addDeliveryDate.Text;
                DisEntry.RecievedBy = addReceivedBy.Text;

                if (hfFile.Value == "")
                {

                }
                else
                {
                    string file = hfFile.Value;
                    byte[] fileinbytes = System.Convert.FromBase64String(file.Substring(file.LastIndexOf(',') + 1));
                    DisEntry.AttachedFile = fileinbytes;
                    DisEntry.FileUpload = hfFileName.Value;
                }
                if (Addhff.Value == "New")
                {
                    DBAccess.DBAccess.saveUpdateDispatch(DisEntry, "Save");
                    HelperClass.ClearModal(this);
                    HelperClass.OpenSuccessToaster(this, "Dispatch Entry successfully saved!");
                }
                else
                {
                    DBAccess.DBAccess.saveUpdateDispatch(DisEntry, "Save");
                    HelperClass.ClearModal(this);
                    HelperClass.OpenSuccessToaster(this, "Dispatch Entry successfully Updated!");
                    //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "clearscreen()", true);
                    //ScriptManager.RegisterStartupScript(this, typeof(Page), "Success" + 1, "<script>SuccessToastr('Dispatch Entry successfully Updated!')</script>", false);
                }
                GridView1.ShowFooter = false;
                getDispatchDetails();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Saveclick_Click: " + ex.Message);
            }
        }
        protected void EditNew_Click(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = (((sender as LinkButton).NamingContainer) as GridViewRow).RowIndex;

                addPoNo.Text = (GridView1.Rows[rowIndex].FindControl("txtPoNo") as Label).Text;
                List<string> list = BindInvoiceNo(addPoNo.Text);

                addInvNo.DataSource = list;
                addInvNo.DataBind();

                string val = (GridView1.Rows[rowIndex].FindControl("txtInvoiceNo") as Label).Text;
                if (addInvNo.Items.FindByValue(val) != null)
                {
                    addInvNo.Text = val;
                }
                addInvDate.Text = (GridView1.Rows[rowIndex].FindControl("txtInvoiceDate") as Label).Text;
                addInvValue.Text = (GridView1.Rows[rowIndex].FindControl("txtInvoiceValue") as Label).Text;
                addProdName.Text = (GridView1.Rows[rowIndex].FindControl("txtProdName") as Label).Text;
                addReceivedBy.Text = (GridView1.Rows[rowIndex].FindControl("txtRecvBy") as Label).Text;
                addRegion.Text = (GridView1.Rows[rowIndex].FindControl("txtRegion") as Label).Text;
                string value1 = (GridView1.Rows[rowIndex].FindControl("txtStatus") as Label).Text;
                BindStatus();
                if (addStatus.Items.FindByValue(value1) != null)
                {
                    addStatus.SelectedValue = value1;
                }
                addEmail.Text = (GridView1.Rows[rowIndex].FindControl("txtEmail") as Label).Text;
                string value2 = (GridView1.Rows[rowIndex].FindControl("txtCourierName") as Label).Text;
                BindCourierName();
                if (addCourierName.Items.FindByValue(value2) != null)
                {
                    addCourierName.SelectedValue = value2;
                }
                addContent.Text = (GridView1.Rows[rowIndex].FindControl("txtContent") as Label).Text;
                addCustomer.Text = (GridView1.Rows[rowIndex].FindControl("txtCustomer") as Label).Text;
                addDeliveryDate.Text = (GridView1.Rows[rowIndex].FindControl("txtDelivDate") as Label).Text;
                addConsignName.Text = (GridView1.Rows[rowIndex].FindControl("txtConsignName") as Label).Text;
                addFileName.Text = (GridView1.Rows[rowIndex].FindControl("FileUpload") as LinkButton).Text;
                hfFile.Value = (GridView1.Rows[rowIndex].FindControl("hfAttachedFileInBase64") as HiddenField).Value;
                hfFileName.Value = (GridView1.Rows[rowIndex].FindControl("FileUpload") as LinkButton).Text;

                addPoNo.Enabled = false;
                addInvNo.Enabled = false;
                Addhff.Value = "Edit";
                AddButton.Text = "Update";
                DispatchTitle.Text = "Edit Dispatch Details";
                HelperClass.OpenModal(this, "newEntry1", true);
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#newEntry1').modal('show');", true);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("EditNew_Click: " + ex.Message);
            }
        }
        protected void addPoNo_TextChanged(object sender, EventArgs e)
        {
            List<string> result = new List<string>();
            try
            {
                DataTable dt = new DataTable();
                dt = DBAccess.DBAccess.getAddablePoNo("POInvoiceList");
                result = dt.AsEnumerable().Where(x => x.Field<string>("PONumber") == addPoNo.Text).Select(x => x.Field<string>("InvoiceNumber")).ToList();

                addInvNo.DataSource = result;
                addInvNo.DataBind();
                addInvNo_SelectedIndexChanged(null, null);
                HelperClass.OpenModal(this, "newEntry1", false);
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#newEntry1').modal();", true);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("addPoNo_TextChanged: " + ex.Message);
            }
        }
        protected void addInvNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = DBAccess.DBAccess.getNewDisDetails("New", addPoNo.Text, addInvNo.SelectedValue);
                if (dt.Rows.Count != 0)
                {
                    addInvDate.Text = Convert.ToDateTime(dt.Rows[0]["Invoicedate"]).ToString("dd-MM-yy");
                    addInvValue.Text = dt.Rows[0]["InvoiceValue"].ToString();
                    addCustomer.Text = dt.Rows[0]["Customer"].ToString();
                    addRegion.Text = dt.Rows[0]["Region"].ToString();
                    addProdName.Text = dt.Rows[0]["ProductionName"].ToString();
                    hdnPoDate.Value = Convert.ToDateTime(dt.Rows[0]["PODate"]).ToString("dd-MM-yy");
                }
                if (sender != null)
                {
                    HelperClass.OpenModal(this, "newEntry1", false);
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#newEntry1').modal();", true);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("addInvNo_SelectedIndexChanged: " + ex.Message);
            }
        }
        protected void FileUpload_Click(object sender, EventArgs e)
        {
            try
            {
                int index = ((GridViewRow)((sender as Control)).NamingContainer).RowIndex;
                string fileextension = ((System.Web.UI.WebControls.LinkButton)(GridView1.Rows[index].FindControl("FileUpload"))).Text;

                List<ProposalEntryDetails> listProposalEntry = new List<ProposalEntryDetails>();
                listProposalEntry = Session["ProposalData"] as List<ProposalEntryDetails>;
                byte[] bytes = listProposalEntry[index].attachedFile;

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
                Logger.WriteErrorLog("FileUpload_Click:" + ex.Message);
            }
        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                ViewState["DeleteRowIndex"] = e.RowIndex;
                HelperClass.OpenModal(this, "myConfirmationModal", true);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GridView1_RowDeleting:" + ex.Message);
            }
        }
        protected void saveConfirmYes_ServerClick(object sender, EventArgs e)
        {
            try
            {
                int DeleteRowIndex = (int)ViewState["DeleteRowIndex"];
                DispatchDetails1 DispatchEntry = new DispatchDetails1();
                DispatchEntry.PoNumber = (GridView1.Rows[DeleteRowIndex].FindControl("txtPoNo") as Label).Text;
                DispatchEntry.InvoiceNo = (GridView1.Rows[DeleteRowIndex].FindControl("txtInvoiceNo") as Label).Text;
                DispatchEntry.ProdName = (GridView1.Rows[DeleteRowIndex].FindControl("txtProdName") as Label).Text;
                DBAccess.DBAccess.saveUpdateDispatch(DispatchEntry, "Delete");
                ViewState["DeleteRowIndex"] = -1;
                HelperClass.OpenSuccessToaster(this, "Dispatch Details Deleted Succesfully!");
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "Success" + 1, "<script>SuccessToastr('Dispatch Details Deleted Succesfully')</script>", false);
                getDispatchDetails();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("saveConfirmYes_ServerClick:" + ex.Message);
            }

        }

        //protected void btnExport_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        // getDispatchDetails();
        //        List<DispatchDetails1> DisEntry = new List<DispatchDetails1>();
        //        FilterDisDetails ddlValue = new FilterDisDetails();
        //        ddlValue.PoNo = txtPONumber.Text.ToString();
        //        ddlValue.InvoiceNo = ddlInvoiceNumber.Text.ToString();
        //        ddlValue.FromDate = txtFDate.Text;
        //        ddlValue.ToDate = txtEDate.Text;

        //        DisEntry = DBAccess.DBAccess.getDispatchDetailValue(ddlValue);
        //        ExportGridToPDF();

        //        //DataTable dt = new DataTable();
        //        //List<DispatchDetails1> ls = new List<DispatchDetails1>();
        //        //ls = Session["DispatchDetails"];
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.WriteErrorLog("btnExport_Click:" + ex.Message);
        //    }
        //}

        //[Obsolete]
        //private void ExportGridToPDF()
        //{
        //    try
        //    {
        //        Response.ContentType = "application/pdf";
        //        Response.AddHeader("content-disposition", "attachment;filename=Dispatch_Details.pdf");
        //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //        StringWriter sw = new StringWriter();
        //        HtmlTextWriter hw = new HtmlTextWriter(sw);
        //        GridView1.RenderControl(hw);
        //        StringReader sr = new StringReader(sw.ToString());
        //        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        //        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        //        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        //        pdfDoc.Open();
        //        htmlparser.Parse(sr);
        //        pdfDoc.Close();
        //        Response.Write(pdfDoc);
        //        Response.End();
        //        GridView1.AllowPaging = true;
        //        GridView1.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.WriteErrorLog("ExportGridToPDF:" + ex.Message);
        //    }
        //}

        //public override void VerifyRenderingInServerForm(Control control)
        //{
        //    //required to avoid the runtime error "  
        //    //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
        //}
    }
}

