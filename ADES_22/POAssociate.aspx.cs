using ADES_22.Model;
using iTextSharp.xmp.impl.xpath;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Drawing;
using System.IO;

namespace ADES_22
{
    public partial class POAssociate : System.Web.UI.Page
    {
        //static string appPath = HttpContext.Current.Server.MapPath("");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCustName();
               
                if (Request.QueryString["CUSTOMER"] != null && Request.QueryString["PONO"] != null)
                {
                    BindKitDetails(Request.QueryString["CUSTOMER"], Request.QueryString["PONO"]);
                }
                else
                {
                    btnView_Click(null, null);
                }
            }
        }
        public void BindCustName()
        {
            List<string> result = new List<string>();
            try
            {
                result = DBAccess.DBAccess.GetCustomerName("CustomerList");
                ddlCustomer.DataSource = result;
                ddlCustomer.DataBind();
                //ddlCustomer_SelectedIndexChanged(null, null);

            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetCustName" + ex.Message);
            }
        }
        public void BindPoNo()
        {
            List<string> result = new List<string>();
            try
            {
                
                DropDownValues ddlValue = new DropDownValues();
                ddlValue.Region = ddlRegion.SelectedValue;
                ddlValue.Customer = ddlCustomer.SelectedValue;
                //DataTable dt = DBAccess.DBAccess.GetPONumber(ddlValue);
                //var PONO = dt.AsEnumerable().Where(x => x.Field<string>("Customer") == ddlValue.Customer && x.Field<string>("Region") == ddlValue.Region).Select(x => x.Field<string>("TallyPoNumber")).ToList();
                //ddlPONo.DataSource = PONO;
                //ddlPONo.DataBind();
                ddlPONo.DataSource= DBAccess.DBAccess.GetPONumber(ddlValue);
                ddlPONo.DataBind();


            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetPoNo: " + ex.Message);
            }
        }
        public void BindKitDetails(string cust,string pono)
        {
            try
            {
                string  custname=null;
                string   ponumber=null;
                if(cust!=null && pono!=null)
                {
                    custname = cust;
                    ponumber = pono;
                    ddlCustomer.Text = cust;

                    //ddlPONo.Text = pono;
                    //ddlCustomer_SelectedIndexChanged(null, null);
                    //ddlRegion.SelectedValue =;
                    
                    List<string> list = new List<string>();
                    list.Add(pono);
                   ddlPONo.DataSource = list;
                    ddlPONo.DataBind();
                    //ddlRegion_SelectedIndexChanged(null, null);

                }
                else
                {
                    custname = ddlCustomer.Text;
                    ponumber = ddlPONo.Text;
                }
                DataTable dt = DBAccess.DBAccess.GetPOKitDetails("View", custname, ponumber);
                Session["KitMaster"] = dt;
                var KitDetails = dt.AsEnumerable().Select(x => x.Field<string>("KitName")).Distinct().ToList();
                DataTable dtkitDetails = new DataTable();
                dtkitDetails.Columns.Add("KitName");
                foreach (var kit in KitDetails)
                {
                    DataRow row = dtkitDetails.NewRow();
                    row["KitName"] = kit;
                    dtkitDetails.Rows.Add(row);
                }

                var Status = dt.AsEnumerable().Select(x => x.Field<string>("Status")).FirstOrDefault();
                lblStatus.Text = "<b>Status:</b> " + Status;
                btnSave.Visible = false;
                btnAddEdit.Visible = false;
                btnApprove.Visible = false;
                lblStatus.Visible = false;
                btnPrint.Visible = false;
                if ((Status == "RequestedForApproval" || Status == "Approved") && (string)Session["Role"] == "Team Member")
                {
                    lblStatus.Visible = true;
                    btnPrint.Visible = true;
                }
                else if (Status == "" && (string)Session["Role"] == "Team Member")
                {
                    btnSave.Visible = true;
                    btnAddEdit.Visible = true;
                }
                else if (Status == "RequestedForApproval" && ((string)Session["Role"] == "Team Leader" || (string)Session["Role"] == "Admin"))//(string)Session["Role"] == "Team Manager"
                {
                    lblStatus.Visible = true;
                    btnSave.Visible = true;
                    btnApprove.Visible = true;
                    btnAddEdit.Visible = true;
                    btnPrint.Visible = true;
                    btnApprove.Text = "Approve";
                }
                else if (Status == "Approved" && ((string)Session["Role"] == "Team Leader" || (string)Session["Role"] == "Admin")) //(string)Session["Role"] == "Team Manager"
                {
                    lblStatus.Visible = true;
                    btnPrint.Visible = true;
                }
                else if (Status == "" && ((string)Session["Role"] == "Team Leader" || (string)Session["Role"] == "Admin")) //(string)Session["Role"] == "Team Manager"
                {
                    btnSave.Visible = true;
                    btnAddEdit.Visible = true;
                }
                else
                {
                    btnSave.Visible = true;
                    btnAddEdit.Visible = true;
                }
                gvKitDetails.DataSource = dtkitDetails;
                gvKitDetails.DataBind();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindKitDetails:" + ex.Message);
            }
        }
        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<string> list = BindRegionToCustomer(ddlCustomer.Text);
                ddlRegion.DataSource = list;
                ddlRegion.DataBind();
                ddlRegion_SelectedIndexChanged(null, null);
                //BindPoNo();

            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("ddlCustomer_SelectedIndexChanged:" + ex.Message);
            }
        }
        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindPoNo();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("ddlRegion_SelectedIndexChanged:" + ex.Message);
            }
        }

        public static List<string> BindRegionToCustomer(string CustomerName)
        {
            List<string> result = new List<string>();
            try
            {
                result = DBAccess.DBAccess.getRegionValues(CustomerName);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("BindRegionToCustomer" + ex.Message);
            }
            return result;
        }
        protected void gvPOAssociate_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    GridView gvItemDetails = e.Row.FindControl("gvItemDetails") as GridView;
                    string kitname = (e.Row.FindControl("lblkitname") as Label).Text;

                    DataTable dt = new DataTable();
                    if (Session["KitMaster"] == null)
                    {
                        dt = DBAccess.DBAccess.GetPOKitDetails("View", ddlCustomer.Text, ddlPONo.Text);
                    }
                    else
                    {
                        dt = (DataTable)Session["KitMaster"];
                    }
                    DataTable ItemDetails = dt.AsEnumerable().Where(x => x.Field<string>("KitName") == kitname).CopyToDataTable();
                    gvItemDetails.DataSource = ItemDetails;
                    gvItemDetails.DataBind();
                    if (btnSave.Visible == false)
                    {
                        for (int j = 0; j < gvItemDetails.Rows.Count; j++)
                        {
                            (gvItemDetails.Rows[j].FindControl("txtQuantity") as TextBox).ReadOnly = true;
                            (gvItemDetails.Rows[j].FindControl("txtShortage") as TextBox).ReadOnly = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("gvPOAssociate_RowDataBound:" + ex.Message);
            }
        }
        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender == null)
                {
                    if (ddlCustomer.Text != "")
                    {
                        ddlCustomer_SelectedIndexChanged(null, null);
                        BindKitDetails(null,null);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    BindKitDetails(null,null);
                }
               // Response.Redirect("POAssociate.aspx");
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnView_Click: " + ex.Message);
            }
        }
        protected void btnAddEdit_Click(object sender, EventArgs e)
        {
            try
            {
                List<POKitAccess> list = new List<POKitAccess>();
                Session["KitDetails"] = list = DBAccess.DBAccess.GetKitLMasterDetails("AddView", ddlCustomer.Text, ddlPONo.Text);
                lvKitDetails.DataSource = list;
                lvKitDetails.DataBind();
                if (ddlCustomer.Text != "" && ddlPONo.Text != "")
                {
                    HelperClass.OpenModal(this, "AddEditKits", true);
                }
                else
                {
                    HelperClass.OpenWarningToaster(this, "Please Select CustomerName and PoNumber..!!");
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnAddEdit_Click:" + ex.Message);
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < lvKitDetails.Items.Count; i++)
                {
                    POKitAccess POEntryDetails = new POKitAccess();
                    if ((lvKitDetails.Items[i].FindControl("ckbSelect") as CheckBox).Checked)
                    {
                        POEntryDetails.param = "SaveAddViewData";
                    }
                    else
                    {
                        POEntryDetails.param = "DeleteAddViewData";
                    }
                    POEntryDetails.cust = ddlCustomer.Text;
                    POEntryDetails.pono = ddlPONo.Text;
                    POEntryDetails.Kitno = (lvKitDetails.Items[i].FindControl("hdnKitNo") as HiddenField).Value;
                    POEntryDetails.Kitname = (lvKitDetails.Items[i].FindControl("lblKitName") as Label).Text;
                    POEntryDetails.KQty = (lvKitDetails.Items[i].FindControl("txtQty") as TextBox).Text;
                    string success = DBAccess.DBAccess.SaveKitDetails(POEntryDetails);
                }
                HelperClass.ClearModal(this);
                HelperClass.OpenSuccessToaster(this, "Customer POAssociate  kit Details successfully Inserted!");
                BindKitDetails(null, null);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnAdd_Click: " + ex.Message);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlCustomer.Text != " " && ddlPONo.Text != "")
                {
                    for (int i = 0; i < gvKitDetails.Rows.Count; i++)
                    {
                        GridView gv = ((GridView)gvKitDetails.Rows[i].FindControl("gvItemDetails"));
                        for (int j = 0; j < gv.Rows.Count; j++)
                        {
                            POItemAccess POItemEntryDetails = new POItemAccess();
                            POItemEntryDetails.Cust = ddlCustomer.Text;
                            POItemEntryDetails.Pono = ddlPONo.Text;
                            POItemEntryDetails.Kitname = (gvKitDetails.Rows[i].FindControl("lblKitName") as Label).Text;
                            POItemEntryDetails.KitNo = (gv.Rows[j].FindControl("hdnKitNo") as HiddenField).Value;
                            POItemEntryDetails.KitQty = (gv.Rows[j].FindControl("hdnKitQty") as HiddenField).Value;
                            POItemEntryDetails.ItemDesc = (gv.Rows[j].FindControl("hdnItemDesc") as HiddenField).Value;
                            POItemEntryDetails.Itemname = (gv.Rows[j].FindControl("lblIName") as Label).Text;
                            POItemEntryDetails.ItemNo = (gv.Rows[j].FindControl("hdnItemNo") as HiddenField).Value;
                            POItemEntryDetails.IsAccessories = (bool)(gv.Rows[j].FindControl("cbxIsAccessories") as CheckBox).Checked;
                            POItemEntryDetails.SName = (gv.Rows[j].FindControl("lblISupplierName") as Label).Text;
                            POItemEntryDetails.PartNo = (gv.Rows[j].FindControl("lblIPartNo") as Label).Text;
                            POItemEntryDetails.ItemQty = (gv.Rows[j].FindControl("hdnItemQty") as HiddenField).Value;
                            POItemEntryDetails.Qty = (gv.Rows[j].FindControl("txtQuantity") as TextBox).Text;
                            POItemEntryDetails.shortage = (gv.Rows[j].FindControl("txtShortage") as TextBox).Text;
                            string success = DBAccess.DBAccess.SaveCustomerItemDetails(POItemEntryDetails, "Save");
                        }
                    }
                    HelperClass.ClearModal(this);
                    HelperClass.OpenSuccessToaster(this, "Customer POAssociate Item Details successfully Inserted!");
                    BindKitDetails(null, null);
                    if ((string)Session["Role"] == "Team Member")
                    {
                        btnApprove.Visible = true;
                    }
                    else if ((string)Session["Role"] == "Team Leader" || (string)Session["Role"] == "Admin")
                    {
                        btnApprove.Text = "Approve";
                        btnApprove.Visible = true;
                    }
                }
                else
                {
                    HelperClass.OpenWarningToaster(this, "Please Select CustomerName and PoNumber..!");
                }
            }
            
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnSave_Click: " + ex.Message);
            }
        }
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                if ((string)Session["Role"] == "Team Leader" || (string)Session["Role"] == "Admin")
                {
                    confirmationmessageText.Text = "Are you sure,you want to approve?";
                }
                HelperClass.ClearModal(this);
                HelperClass.OpenModal(this, "myConfirmationModal", true);

            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnApprove_Click: " + ex.Message);
            }
        }
        protected void ConfirmYes_Click(object sender, EventArgs e)
        {
            try
            {
                var StatusUpdate = "";
                POItemAccess RequestStatusDetails = new POItemAccess();
                if ((string)Session["Role"] == "Team Member")
                {
                    RequestStatusDetails.param = "ApproveRequested";
                    StatusUpdate = "RequestedForApproval";
                }
                else if ((string)Session["Role"] == "Team Leader" || (string)Session["Role"] == "Admin")
                {
                    RequestStatusDetails.param = "Approve";
                    StatusUpdate = "Approved";
                }
                RequestStatusDetails.Cust = ddlCustomer.Text;
                RequestStatusDetails.Pono = ddlPONo.Text;
                RequestStatusDetails.Status = StatusUpdate;
                RequestStatusDetails.ApprovedBy = (string)Session["username"];
                RequestStatusDetails.ApproveRequestBy = (string)Session["username"];
                RequestStatusDetails.ApproveRequestTS = DateTime.Now.ToString();
                RequestStatusDetails.ApprovedTS = DateTime.Now.ToString();
                string success = DBAccess.DBAccess.RequestAprrovalStatus(RequestStatusDetails);
                HelperClass.ClearModal(this);
                //string EmailTo = "poojapkotan@gmail.com";
                SendMail();
                //SendingEmails();
                BindKitDetails(null, null);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("ConfirmYes_Click: " + ex.Message);
            }
        }
        public void getEmailId()
        {
            try
            {
                DataTable dtUsername = DBAccess.DBAccess.GetPOKitDetails("View", ddlCustomer.Text, ddlPONo.Text);
                var UserName = dtUsername.AsEnumerable().Where(x => x.Field<string>("Customer") == ddlCustomer.Text && x.Field<string>("PoNumber") == ddlPONo.Text).Select(x => x.Field<string>("ApproveRequestedBy")).FirstOrDefault();
                Session["EmailId"] = UserName;
                DataTable dtEmailId = DBAccess.DBAccess.GetEmployeeId(UserName);
                var EmailId = dtEmailId.AsEnumerable().Where(x => x.Field<string>("Employeeid") == UserName).Select(x => x.Field<string>("Email")).FirstOrDefault();
                Session["EmailId"] = EmailId;
                //var ToAddressEmailID= dtToEmailId.AsEnumerable().Where(x => x.Field<string>("Role") =="Team Manager"&& x.Field<string>("Department")="Application Team").Select(x => x.Field<string>("Email")).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("getEmailId: " + ex.Message);
            }
        }
        public void SendMail()
        {
            try
            {
                WService ws = new WService();
                string AppPass = HelperClass.AppPassword;
                ws.From = HelperClass.Email_ID + ";" + HelperClass.EncodePasswordToBase64(AppPass);
                if ((string)Session["Role"] == "Team Member")
                {
                    //change to address to whom u have to send mail(team manager/team member/admin)?
                    ws.To = "poojapkotan@gmail.com";
                    ws.Subject = "ADES-Packing List Request For Approval";
                    ws.MsgBody = "Hello Sir,<br>" + "CustomerName:" + ddlCustomer.Text + "<br>PoNo:" + ddlPONo.Text + "<br>Has requested for approval,Please do check and confirm.<br>" + "<br>" + "<a href=\"https://localhost:44313/Login.aspx\"> Click Here To Login</a>" + "<br>" + "<br>Thank you.";
                }
                if ((string)Session["Role"] == "Team Leader" || (string)Session["Role"] == "Admin")//(string)Session["Role"] == "Team Manager"
                {
                    getEmailId();
                    try
                    {
                        ws.To = Session["EmailId"].ToString();
                        ws.Subject = "ADES-Packing List Approved";
                        ws.MsgBody = "Hello Sir,<br>" + "CustomerName:" + ddlCustomer.Text + "<br>PoNo:" + ddlPONo.Text + "<br>Approved the request<br>" + "<br>" + "<a href=\"https://localhost:44313/Login.aspx\"> Click Here To Login</a>" + "<br>" + "<br>Thank you.";
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog("Admin approving without request");
                    }


                }
                DBAccess.DBAccess.InsertEmailDetails(ws);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("SendMail: " + ex.Message);
            }

        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string cust = ddlCustomer.Text;
                string PONO = ddlPONo.Text;
                if (cust != "" && PONO != "")
                {
                    var url = String.Format("BOMPrint.aspx?CUSTOMER={0}&PONO={1}", cust, PONO);
                    Response.Redirect(url, false);
                }
                else
                {
                    HelperClass.OpenWarningToaster(this, "Customer Name and PONo is mandatory!");
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("btnPrint_Click: " + ex.Message);
            }
        }

        //protected void btnExport_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        DataTable dt = DBAccess.DBAccess.GetPOKitDetails("View", ddlCustomer.Text, ddlPONo.Text);
        //        string templatefile = string.Empty;
        //        string Filename = "CustomerBOM.xlsx";

        //        string Source = string.Empty;
        //        Source = POAssociate.GetReportPath(Filename);
        //        string Template = string.Empty;
        //        Template = "CustomerBOM" + DateTime.Now + ".xls";
        //        string destination = string.Empty;
        //        destination = Path.Combine(appPath, "Temp", SafeFileName(Template));
        //        if (!File.Exists(Source))
        //        {
        //            Console.WriteLine("BOM Details- \n" + Source);
        //        }
        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            FileInfo newFile = new FileInfo(Source);
        //            ExcelPackage Excel = new ExcelPackage(newFile, true);
        //            System.Diagnostics.Debug.WriteLine(Excel);
        //            ExcelWorksheet exelworksheet = Excel.Workbook.Worksheets[0];
        //            int cellRow = 1;

        //            exelworksheet.Cells[cellRow, cellRow, cellRow, dt.Columns.Count - 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //            exelworksheet.Cells[cellRow, cellRow, cellRow, dt.Columns.Count - 2].Value = "Customer & PO name";
        //            exelworksheet.Cells[cellRow, cellRow, cellRow, dt.Columns.Count - 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //            exelworksheet.Cells[cellRow, cellRow, cellRow, dt.Columns.Count - 2].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#ffff1a"));
        //            exelworksheet.Cells[cellRow, cellRow, cellRow, dt.Columns.Count - 2].Merge = true;
        //            exelworksheet.Cells[cellRow, cellRow, cellRow, dt.Columns.Count - 2].Style.Font.Size = 18;
        //            exelworksheet.Cells[cellRow, cellRow, cellRow, dt.Columns.Count - 2].Style.Font.Bold = true;
        //            exelworksheet.Cells[cellRow, cellRow, cellRow, dt.Columns.Count - 2].Style.Font.Color.SetColor(Color.Black);

        //            cellRow = 2;
        //            exelworksheet.Cells[cellRow, 1].Value = "SL NO.";
        //            exelworksheet.Cells[cellRow, 1].Style.Font.Bold = true;
        //            exelworksheet.Cells[cellRow, 1].Style.Font.Size = 12;
        //            exelworksheet.Cells[cellRow, 1].Style.ShrinkToFit = true;

        //            exelworksheet.Cells[cellRow, 2].Value = "BOM";
        //            exelworksheet.Cells[cellRow, 2].Style.Font.Bold = true;
        //            exelworksheet.Cells[cellRow, 2].Style.Font.Size = 12;

        //            exelworksheet.Cells[cellRow, 3].Value = "Part details";
        //            exelworksheet.Cells[cellRow, 3].Style.Font.Bold = true;
        //            exelworksheet.Cells[cellRow, 3].Style.Font.Size = 12;

        //            exelworksheet.Cells[cellRow, 4].Value = "Part numbers";
        //            exelworksheet.Cells[cellRow, 4].Style.Font.Bold = true;
        //            exelworksheet.Cells[cellRow, 4].Style.Font.Size = 12;

        //            exelworksheet.Cells[cellRow, 5].Value = "Description";
        //            exelworksheet.Cells[cellRow, 5].Style.Font.Bold = true;
        //            exelworksheet.Cells[cellRow, 5].Style.Font.Size = 12;

        //            exelworksheet.Cells[cellRow, 6].Value = "Qty";
        //            exelworksheet.Cells[cellRow, 6].Style.Font.Bold = true;
        //            exelworksheet.Cells[cellRow, 6].Style.Font.Size = 12;

        //            exelworksheet.Cells[cellRow, 7].Value = "Availability/remarks";
        //            exelworksheet.Cells[cellRow, 7].Style.Font.Bold = true;
        //            exelworksheet.Cells[cellRow, 7].Style.Font.Size = 12;



        //            cellRow = cellRow + 2;

        //            for (int i = 0; i < dt.Columns.Count - 2; i++)
        //            {
        //                exelworksheet.Cells[cellRow, i + 1].Value = dt.Columns[i].ColumnName.ToString();
        //                exelworksheet.Cells[cellRow, i + 1].Style.Font.Bold = true;
        //                exelworksheet.Cells[cellRow, i + 1].Style.Font.Size = 12;

        //                // Color backcolor = Color.FromArgb(93, 123, 157);
        //                Color backcolor = ColorTranslator.FromHtml("#b1d1fc");
        //                exelworksheet.Cells[cellRow, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //                exelworksheet.Cells[cellRow, i + 1].Style.Fill.BackgroundColor.SetColor(backcolor);
        //                exelworksheet.Cells[cellRow, i + 1].Style.Font.Color.SetColor(Color.Black);

        //                exelworksheet.Cells[cellRow, i + 1].AutoFitColumns();
        //            }
        //            cellRow++;
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                for (int j = 0; j < dt.Columns.Count - 2; j++)
        //                {
        //                    exelworksheet.Cells[cellRow, j + 1].Value = dt.Rows[i][j].ToString();
        //                }
        //                cellRow++;
        //            }
        //            for (int i = 1; i <= dt.Columns.Count - 2; i++)
        //            {
        //                exelworksheet.Cells[3, i, dt.Rows.Count + 5, i].AutoFitColumns();
        //            }
        //            DownloadFile(destination, Excel.GetAsByteArray());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.WriteErrorLog("btnExport_Click: " + ex.Message);
        //    }
        //}

        //public void SendingEmails()
        //{
        //    try
        //    {
        //        if ((string)Session["Role"] == "Team Member")
        //        {
        //            StringBuilder sb = new StringBuilder();

        //            sb.AppendLine("Hello Sir,<br>");
        //            sb.AppendLine("CustomerName:" + ddlCustomer.Text);
        //            sb.AppendLine("<br>PoNo:" + ddlPONo.Text);
        //            sb.AppendLine("<br>Has requested for approval,Please do check and confirm.<br>");
        //            sb.AppendLine("<br>" + "<a href=\"https://localhost:44313/Login.aspx\"> Click Here To Login</a>" + "<br>");
        //            sb.AppendLine("<br>Thank you.");

        //            var result = sb.ToString();
        //            string fromEmail = "amitpeenyabangalore@gmail.com";
        //            string fromPassword = "Y2F6bm5la29neG1wc2plZA=="; //ramtfvtkykhfhlgp
        //            MailMessage message = new MailMessage();
        //            message.From = new MailAddress(fromEmail);
        //            message.Subject = "Request For Approval";
        //            message.To.Add(new MailAddress("poornimakharvi805@gmail.com"));
        //            // message.To.Add(new MailAddress("poornimakharvi805@gmail.com"));
        //            //message.CC.Add(new MailAddress("poojapkotan@gmail.com"));
        //            message.Body = result;
        //            message.IsBodyHtml = true;
        //            var smtpClient = new SmtpClient("smtp.gmail.com")
        //            {
        //                Port = 587,
        //                Credentials = new NetworkCredential(fromEmail, fromPassword),
        //                EnableSsl = true,
        //            };
        //            smtpClient.Send(message);
        //            HelperClass.OpenSuccessToaster(this, "Request Mail Sent Succesfully");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.WriteErrorLog("SendingEmailToMananger: " + ex.Message);
        //    }
        //}

        //private static void DownloadFile(string filename, byte[] bytearray)
        //{
        //    try
        //    {
        //        HttpContext.Current.Response.Clear();
        //        HttpContext.Current.Response.Charset = "";
        //        HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + Path.GetFileName(filename) + "\"");
        //        HttpContext.Current.Response.OutputStream.Write(bytearray, 0, bytearray.Length);
        //        HttpContext.Current.Response.Flush();
        //        HttpContext.Current.Response.SuppressContent = true;
        //        HttpContext.Current.ApplicationInstance.CompleteRequest();
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.WriteErrorLog("DownloadFile" + ex.Message);
        //    }

        //}
        //public static string SafeFileName(string name)
        //{
        //    StringBuilder str = new StringBuilder(name);
        //    try
        //    {
        //        foreach (char c in System.IO.Path.GetInvalidFileNameChars())
        //        {
        //            str = str.Replace(c, '_');
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.WriteErrorLog("SafeFileName" + ex.Message);
        //    }
        //    return str.ToString();
        //}
        //public static string GetReportPath(string reportName)
        //{
        //    string src = "";
        //    try
        //    {
        //        if (HttpContext.Current.Session["Language"] == null)
        //            src = Path.Combine(appPath, "ExcelDocument", reportName);
        //        else
        //        {
        //            if (HttpContext.Current.Session["Language"].ToString() != "en")
        //                src = Path.Combine(appPath, "ExcelDocument-" + HttpContext.Current.Session["Language"].ToString() + "", reportName);
        //            else
        //                src = Path.Combine(appPath, "ExcelDocument", reportName);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Logger.WriteErrorLog("GetReportPath" + e.Message);
        //    }
        //    return src;
        //}


    }
}