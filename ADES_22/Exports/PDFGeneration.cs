using iTextSharp.text;
using iTextSharp.text.pdf;
using ADES_22.DBAccess;
using ADES_22.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ADES_22.Exports
{
    public class PDFGeneration
    {
        static string NoDataFoundMSG = "NoDataFound", NoDataFoundMSGToShow = "No Data Found.", ErrorMSG = "Error", ErrorMSGToShow = "Error to export file.";
        public static string generateGroupPDF(List<ColumnList> columnList, out string msgToShow)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                if (HttpContext.Current.Session["MastersData"] == null)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }
                List<CompanyGroupDetails> list = HttpContext.Current.Session["MastersData"] as List<CompanyGroupDetails>;
                if (list.Count <= 0)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("Group Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                var visibleCoumns = columnList.Where(x => x.FieldVisibility == true).ToList();
                //Contrent
                PdfPTable contentTbl = new PdfPTable(visibleCoumns.Count);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 20;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Group ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Group Name")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Contact Person")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Place")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("State")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Country")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Pin")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Address")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Email")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Phone")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective From")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective To")));

                //for (int i = 0; i < list.Count; i++)
                //{
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].GroupID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].GroupName)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].ContactPerson)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Place)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].State)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Country)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Pin)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Address)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Email)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Phone)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                //}

                for (int i = 0; i < visibleCoumns.Count; i++)
                {
                    if (visibleCoumns[i].FieldVisibility)
                    {
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(visibleCoumns[i].FieldDisplayName)));
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < visibleCoumns.Count; j++)
                    {
                        if (visibleCoumns[j].FieldVisibility)
                        {
                            switch (visibleCoumns[j].FieldName)
                            {
                                case "GroupID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].GroupID)));
                                    break;
                                case "GroupName":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].GroupName)));
                                    break;
                                case "ContactPerson":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].ContactPerson)));
                                    break;
                                case "Place":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Place)));
                                    break;
                                case "State":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].State)));
                                    break;
                                case "Country":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Country)));
                                    break;
                                case "Pin":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Pin)));
                                    break;
                                case "Address":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Address)));
                                    break;
                                case "Email":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Email)));
                                    break;
                                case "Phone":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Phone)));
                                    break;
                                case "EffectiveFrom":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                                    break;
                                case "EffectiveTo":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                                    break;
                            }
                        }
                    }
                }

                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=GroupData.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }

        public static string generateCompanyPDF(List<ColumnList> columnList, out string msgToShow)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                if (HttpContext.Current.Session["MastersData"] == null)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }
                List<CompanyGroupDetails> list = HttpContext.Current.Session["MastersData"] as List<CompanyGroupDetails>;
                if (list.Count <= 0)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("Company Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                //Contrent
                var visibleCoumns = columnList.Where(x => x.FieldVisibility == true).ToList();

                PdfPTable contentTbl = new PdfPTable(visibleCoumns.Count);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 20;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Group ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Company ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Company Name")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Contact Person")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Place")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("State")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Country")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Pin")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Address")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Email")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Phone")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Company Admin")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Authentication Method")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Two Factor Authentication Method")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective From")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective To")));

                //for (int i = 0; i < list.Count; i++)
                //{
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].GroupID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].CompanyID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].CompanyName)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].ContactPerson)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Place)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].State)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Country)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Pin)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Address)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Email)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Phone)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].UserID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].AuthonticationMethod)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].TwoFactAuthoMethod)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                //}
                for (int i = 0; i < visibleCoumns.Count; i++)
                {
                    if (visibleCoumns[i].FieldVisibility)
                    {
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(visibleCoumns[i].FieldDisplayName)));
                    }
                }
                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < visibleCoumns.Count; j++)
                    {
                        if (visibleCoumns[j].FieldVisibility)
                        {
                            switch (visibleCoumns[j].FieldName)
                            {
                                case "GroupID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].GroupID)));
                                    break;
                                case "CompanyID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].CompanyID)));
                                    break;
                                case "CompanyName":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].CompanyName)));
                                    break;
                                case "ContactPerson":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].ContactPerson)));
                                    break;
                                case "Place":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Place)));
                                    break;
                                case "State":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].State)));
                                    break;
                                case "Country":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Country)));
                                    break;
                                case "Pin":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Pin)));
                                    break;
                                case "Address":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Address)));
                                    break;
                                case "Email":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Email)));
                                    break;
                                case "Phone":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Phone)));
                                    break;
                                case "CompanyAdmin":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].UserID)));
                                    break;
                                case "AuthenticationMethod":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].AuthonticationMethod)));
                                    break;
                                case "TwoFactorAuthenticationMethod":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].TwoFactAuthoMethod)));
                                    break;
                                case "EffectiveFrom":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                                    break;
                                case "EffectiveTo":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                                    break;
                            }
                        }
                    }
                }

                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=CompanyData.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }

        public static string generateRolePDF(List<ColumnList> columnList, string company, out string msgToShow)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                if (HttpContext.Current.Session["MastersData"] == null)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }
                List<RoleDetails> list = HttpContext.Current.Session["MastersData"] as List<RoleDetails>;
                if (list.Count <= 0)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("Role Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });


                //selected input
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldText("")) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingTop = 10, Border = 0 });

                //Contrent
                var visibleCoumns = columnList.Where(x => x.FieldVisibility == true).ToList();
                PdfPTable contentTbl = new PdfPTable(visibleCoumns.Count);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 10;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Role Name")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective From")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective To")));

                //for (int i = 0; i < list.Count; i++)
                //{
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].RoleName)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                //}

                for (int i = 0; i < visibleCoumns.Count; i++)
                {
                    if (visibleCoumns[i].FieldVisibility)
                    {
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(visibleCoumns[i].FieldDisplayName)));
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < visibleCoumns.Count; j++)
                    {
                        if (visibleCoumns[j].FieldVisibility)
                        {
                            switch (visibleCoumns[j].FieldName)
                            {
                                case "RoleName":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].RoleName)));
                                    break;
                                case "EffectiveFrom":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                                    break;
                                case "EffectiveTo":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                                    break;
                            }
                        }
                    }
                }

                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=RoleData.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }

        public static string generateUserPDF(List<ColumnList> columnList, string company, out string msgToShow)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                if (HttpContext.Current.Session["MastersData"] == null)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }
                List<UserData> list = HttpContext.Current.Session["MastersData"] as List<UserData>;
                if (list.Count <= 0)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("User Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                //selected input
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldText("Company: " + company)) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingTop = 10, Border = 0 });

                //Contrent
                var visibleCoumns = columnList.Where(x => x.FieldVisibility == true).ToList();

                PdfPTable contentTbl = new PdfPTable(visibleCoumns.Count);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 10;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Corporate ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("User ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Username")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Address")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Email")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Mobile No.")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Role")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Is Employee?")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Employee ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("IOT ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective From")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective To")));

                //for (int i = 0; i < list.Count; i++)
                //{
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].CorporateID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].UserID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Username)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Address)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Email)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MobileNo)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Role)));
                //    addCheckBox(contentTbl, list[i].IsEmployee);
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].EmployeeID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].IOTID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                //}

                for (int i = 0; i < visibleCoumns.Count; i++)
                {
                    if (visibleCoumns[i].FieldVisibility)
                    {
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(visibleCoumns[i].FieldDisplayName)));
                    }
                }
                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < visibleCoumns.Count; j++)
                    {
                        if (visibleCoumns[j].FieldVisibility)
                        {
                            switch (visibleCoumns[j].FieldName)
                            {
                                case "CorporateID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].CorporateID)));
                                    break;
                                case "UserID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].UserID)));
                                    break;
                                case "Username":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Username)));
                                    break;
                                case "Address":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Address)));
                                    break;
                                case "Email":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Email)));
                                    break;
                                case "MobileNo":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MobileNo)));
                                    break;
                                case "Role":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Role)));
                                    break;
                                case "IsEmployee":
                                    addCheckBox(contentTbl, list[i].IsEmployee);
                                    break;
                                case "EmployeeID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].EmployeeID)));
                                    break;
                                case "IOTID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].IOTID)));
                                    break;
                                case "EffectiveFrom":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                                    break;
                                case "EffectiveTo":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                                    break;
                            }
                        }
                    }
                }

                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=UserData.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }

        public static string generatePlantPDF(List<ColumnList> columnList, string company, out string msgToShow)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                if (HttpContext.Current.Session["MastersData"] == null)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }
                List<PlantLevelDetails> list = HttpContext.Current.Session["MastersData"] as List<PlantLevelDetails>;
                if (list.Count <= 0)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("Plant Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                //selected input
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldText("Company: " + company)) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingTop = 10, Border = 0 });

                //Contrent
                var visibleCoumns = columnList.Where(x => x.FieldVisibility == true).ToList();

                PdfPTable contentTbl = new PdfPTable(visibleCoumns.Count);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 10;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Plant ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Plant Desc")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Plant Code")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Address")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Country")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Region")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Geo Location")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("City")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective From")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective To")));

                //for (int i = 0; i < list.Count; i++)
                //{
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].PlantID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Description)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Code)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Address)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Country)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Region)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].GeoLocation)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].City)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                //}
                for (int i = 0; i < visibleCoumns.Count; i++)
                {
                    if (visibleCoumns[i].FieldVisibility)
                    {
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(visibleCoumns[i].FieldDisplayName)));
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < visibleCoumns.Count; j++)
                    {
                        if (visibleCoumns[j].FieldVisibility)
                        {
                            switch (visibleCoumns[j].FieldName)
                            {
                                case "PlantID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].PlantID)));
                                    break;
                                case "PlantDesc":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Description)));
                                    break;
                                case "PlantCode":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Code)));
                                    break;
                                case "Address":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Address)));
                                    break;
                                case "Country":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Country)));
                                    break;
                                case "Region":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Region)));
                                    break;
                                case "GeoLocation":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].GeoLocation)));
                                    break;
                                case "City":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].City)));
                                    break;
                                case "Currency":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Currency)));
                                    break;
                                case "EffectiveFrom":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                                    break;
                                case "EffectiveTo":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                                    break;
                            }
                        }
                    }
                }
                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=PlantData.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }

        public static string generateMachinePDF(List<ColumnList> columnList, string company, out string msgToShow)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                if (HttpContext.Current.Session["MastersData"] == null)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }
                List<MachineData> list = HttpContext.Current.Session["MastersData"] as List<MachineData>;
                if (list.Count <= 0)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

            //    List<MachineData> machineCharestics = DBAccess.DBAccess.getMachineMasterDetails(list[0].CompanyID, list[0].MachineID, "MachineCharacteristic");
            //    List<MachineData> machineDatasource = DBAccess.DBAccess.getMachineMasterDetails(list[0].CompanyID, list[0].MachineID, "MachineSource");

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;

                #region ----Machine Information
                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("Machine Information")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                //selected input
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldText("")) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingTop = 10, Border = 0 });

                //Contrent
                var visibleCoumns = columnList.Where(x => x.FieldVisibility == true).ToList();

                PdfPTable contentTbl = new PdfPTable(visibleCoumns.Count);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 10;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Machine ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Machine Name")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("IoT ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("MTB")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Machine Type")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Machine Model")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("CNC Make")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("CNC Model")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("PLC Make")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("PLC Model")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Serial Number")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective From")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective To")));

                //for (int i = 0; i < list.Count; i++)
                //{
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineDisplayName)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].IOTID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineMTB)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineType)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineModel)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].CNCMake)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].CNCModel)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].PLCMake)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].PLCModel)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].SerialNumber)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                //}
                for (int i = 0; i < visibleCoumns.Count; i++)
                {
                    if (visibleCoumns[i].FieldVisibility)
                    {
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(visibleCoumns[i].FieldDisplayName)));
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < visibleCoumns.Count; j++)
                    {
                        if (visibleCoumns[j].FieldVisibility)
                        {
                            switch (visibleCoumns[j].FieldName)
                            {
                                case "MachineID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineID)));
                                    break;
                                case "MachineName":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineDisplayName)));
                                    break;
                                case "IOTID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].IOTID)));
                                    break;
                                case "MTB":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineMTB)));
                                    break;
                                case "MachineType":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineType)));
                                    break;
                                case "MachineModel":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineModel)));
                                    break;
                                case "CNCMake":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].CNCMake)));
                                    break;
                                case "CNCModel":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].CNCModel)));
                                    break;
                                case "PLCMake":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].PLCMake)));
                                    break;
                                case "PLCModel":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].PLCModel)));
                                    break;
                                case "SerialNumber":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].SerialNumber)));
                                    break;
                                case "Axis":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Axis)));
                                    break;
                                case "EffectiveFrom":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                                    break;
                                case "EffectiveTo":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                                    break;
                            }
                        }
                    }
                }

                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });
                #endregion




                //#region ----Machine Information
                ////header
                //mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("Machine Characteristics")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });
                ////Contrent
                //contentTbl = new PdfPTable(6);
                //contentTbl.WidthPercentage = 100;
                //contentTbl.SpacingBefore = 20;
                //contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                //contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Machine Owner")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Machine Hour Rate")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("OEE Target")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Multi Spindle Flag?")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Auto Setup Change Down")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Critical Machine?")));

                //for (int i = 0; i < list.Count; i++)
                //{
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachinewiseOwner)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Mchrrate)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].OEETarget)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineMTB)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].AutoSetupChangeDown)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineModel)));
                //}

                //mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });
                //#endregion
                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=MachineData.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }



        #region ------Down Time Masters-------------
        public static string generateDownTimeCategoryPDF(List<ColumnList> columnList, string company, out string msgToShow)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                if (HttpContext.Current.Session["LossCodeMasterData"] == null)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }
                List<DownTimeAndRejectionDetails> list = HttpContext.Current.Session["LossCodeMasterData"] as List<DownTimeAndRejectionDetails>;
                if (list.Count <= 0)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("Loss Code - Category Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                //selected input
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldText("")) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingTop = 10, Border = 0 });

                //Contrent
                var visibleCoumns = columnList.Where(x => x.FieldVisibility == true).ToList();

                PdfPTable contentTbl = new PdfPTable(visibleCoumns.Count);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 10;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Category")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Interface ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Description")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective From")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective To")));

                //for (int i = 0; i < list.Count; i++)
                //{
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Category)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].CategoryID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Description)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                //}
                for (int i = 0; i < visibleCoumns.Count; i++)
                {
                    if (visibleCoumns[i].FieldVisibility)
                    {
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(visibleCoumns[i].FieldDisplayName)));
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < visibleCoumns.Count; j++)
                    {
                        if (visibleCoumns[j].FieldVisibility)
                        {
                            switch (visibleCoumns[j].FieldName)
                            {
                                case "Category":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Category)));
                                    break;
                                case "InterfaceID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].CategoryID)));
                                    break;
                                case "Description":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Description)));
                                    break;
                                case "EffectiveFrom":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                                    break;
                                case "EffectiveTo":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                                    break;
                            }
                        }
                    }
                }

                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=LossCodeCategoryData.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }

        public static string generateDownTimeSubCategoryPDF(List<ColumnList> columnList, string company, out string msgToShow)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                if (HttpContext.Current.Session["LossCodeMasterData"] == null)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }
                List<DownTimeAndRejectionDetails> list = HttpContext.Current.Session["LossCodeMasterData"] as List<DownTimeAndRejectionDetails>;
                if (list.Count <= 0)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("Loss Code - Sub Category Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                //selected input
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldText("")) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingTop = 10, Border = 0 });

                //Contrent
                var visibleCoumns = columnList.Where(x => x.FieldVisibility == true).ToList();

                PdfPTable contentTbl = new PdfPTable(visibleCoumns.Count);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 10;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Sub-Category")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Interface ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Description")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Category")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective From")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective To")));

                //for (int i = 0; i < list.Count; i++)
                //{
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].SubCategory)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].SubCategoryID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Description)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Category)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                //}

                for (int i = 0; i < visibleCoumns.Count; i++)
                {
                    if (visibleCoumns[i].FieldVisibility)
                    {
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(visibleCoumns[i].FieldDisplayName)));
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < visibleCoumns.Count; j++)
                    {
                        if (visibleCoumns[j].FieldVisibility)
                        {
                            switch (visibleCoumns[j].FieldName)
                            {
                                case "SubCategory":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].SubCategory)));
                                    break;
                                case "InterfaceID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].SubCategoryID)));
                                    break;
                                case "Description":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Description)));
                                    break;
                                case "Category":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Category)));
                                    break;
                                case "EffectiveFrom":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                                    break;
                                case "EffectiveTo":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                                    break;
                            }
                        }
                    }
                }

                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=LossCodeSubCategoryData.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }

        public static string generateDownTimePDF(List<ColumnList> columnList, string company, out string msgToShow)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                if (HttpContext.Current.Session["LossCodeMasterData"] == null)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }
                List<DownTimeAndRejectionDetails> list = HttpContext.Current.Session["LossCodeMasterData"] as List<DownTimeAndRejectionDetails>;
                if (list.Count <= 0)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("Loss Code Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                //selected input
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldText("")) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingTop = 10, Border = 0 });

                //Contrent
                var visibleCoumns = columnList.Where(x => x.FieldVisibility == true).ToList();

                PdfPTable contentTbl = new PdfPTable(visibleCoumns.Count);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 10;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Loss Code")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Loss Number")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Interface ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Description")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Category")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Sub-Category")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("ML Flag")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Threshold (s)")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective From")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective To")));

                //for (int i = 0; i < list.Count; i++)
                //{
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].DownTime)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].DownTimeNo)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].InterfaceID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Description)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Category)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].SubCategory)));
                //    addCheckBox(contentTbl, list[i].MLFlag);
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Threshold)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                //}

                for (int i = 0; i < visibleCoumns.Count; i++)
                {
                    if (visibleCoumns[i].FieldVisibility)
                    {
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(visibleCoumns[i].FieldDisplayName)));
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < visibleCoumns.Count; j++)
                    {
                        if (visibleCoumns[j].FieldVisibility)
                        {
                            switch (visibleCoumns[j].FieldName)
                            {
                                case "LossCode":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].DownTime)));
                                    break;
                                case "InterfaceID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].DownTimeNo)));
                                    break;
                                case "Description":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Description)));
                                    break;
                                case "Category":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Category)));
                                    break;
                                case "SubCategory":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].SubCategory)));
                                    break;
                                case "EffectTo":
                                    //addCheckBox(contentTbl, list[i].AE);
                                    if (list[i].AE == true)
                                    {
                                        contentTbl.AddCell(new PdfPCell(getPdfCellContentText("AE")));
                                    }
                                    else
                                    {
                                        contentTbl.AddCell(new PdfPCell(getPdfCellContentText("")));
                                    }
                                    break;
                                case "Threshold":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Threshold)));
                                    break;
                                case "EffectiveFrom":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                                    break;
                                case "EffectiveTo":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                                    break;
                            }
                        }
                    }
                }

                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=LossCodeData.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }
        #endregion

        #region ------Rejection Masters-------------
        public static string generateRejectionCategoryPDF(List<ColumnList> columnList, string company, out string msgToShow)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                if (HttpContext.Current.Session["RejectionMasterData"] == null)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }
                List<DownTimeAndRejectionDetails> list = HttpContext.Current.Session["RejectionMasterData"] as List<DownTimeAndRejectionDetails>;
                if (list.Count <= 0)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("Rejection Code - Category Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                //selected input
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldText("")) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingTop = 10, Border = 0 });

                //Contrent
                var visibleCoumns = columnList.Where(x => x.FieldVisibility == true).ToList();

                PdfPTable contentTbl = new PdfPTable(visibleCoumns.Count);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 10;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Category")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Interface ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Description")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective From")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective To")));

                //for (int i = 0; i < list.Count; i++)
                //{
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Category)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].CategoryID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Description)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                //}

                for (int i = 0; i < visibleCoumns.Count; i++)
                {
                    if (visibleCoumns[i].FieldVisibility)
                    {
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(visibleCoumns[i].FieldDisplayName)));
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < visibleCoumns.Count; j++)
                    {
                        if (visibleCoumns[j].FieldVisibility)
                        {
                            switch (visibleCoumns[j].FieldName)
                            {
                                case "Category":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Category)));
                                    break;
                                case "InterfaceID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].CategoryID)));
                                    break;
                                case "Description":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Description)));
                                    break;
                                case "EffectiveFrom":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                                    break;
                                case "EffectiveTo":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                                    break;
                            }
                        }
                    }
                }

                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=RejectionCodeCategoryData.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }

        public static string generateRejectionSubCategoryPDF(List<ColumnList> columnList, string company, out string msgToShow)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                if (HttpContext.Current.Session["RejectionMasterData"] == null)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }
                List<DownTimeAndRejectionDetails> list = HttpContext.Current.Session["RejectionMasterData"] as List<DownTimeAndRejectionDetails>;
                if (list.Count <= 0)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("Rejection Code - Sub Category Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });


                //selected input
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldText("")) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingTop = 10, Border = 0 });

                //Contrent
                var visibleCoumns = columnList.Where(x => x.FieldVisibility == true).ToList();

                PdfPTable contentTbl = new PdfPTable(visibleCoumns.Count);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 10;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Sub-Category")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Interface ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Description")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Category")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective From")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective To")));

                //for (int i = 0; i < list.Count; i++)
                //{
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].SubCategory)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].SubCategoryID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Description)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Category)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                //}

                for (int i = 0; i < visibleCoumns.Count; i++)
                {
                    if (visibleCoumns[i].FieldVisibility)
                    {
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(visibleCoumns[i].FieldDisplayName)));
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < visibleCoumns.Count; j++)
                    {
                        if (visibleCoumns[j].FieldVisibility)
                        {
                            switch (visibleCoumns[j].FieldName)
                            {
                                case "SubCategory":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].SubCategory)));
                                    break;
                                case "InterfaceID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].SubCategoryID)));
                                    break;
                                case "Description":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Description)));
                                    break;
                                case "Category":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Category)));
                                    break;
                                case "EffectiveFrom":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                                    break;
                                case "EffectiveTo":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                                    break;
                            }
                        }
                    }
                }

                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=RejectionCodeSubCategoryData.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }

        public static string generateRejectionCodeePDF(List<ColumnList> columnList, string company, out string msgToShow)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                if (HttpContext.Current.Session["RejectionMasterData"] == null)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }
                List<DownTimeAndRejectionDetails> list = HttpContext.Current.Session["RejectionMasterData"] as List<DownTimeAndRejectionDetails>;
                if (list.Count <= 0)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("Rejection Code Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });


                //selected input
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldText("")) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingTop = 10, Border = 0 });

                //Contrent
                var visibleCoumns = columnList.Where(x => x.FieldVisibility == true).ToList();

                PdfPTable contentTbl = new PdfPTable(visibleCoumns.Count);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 10;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Rejection Code")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Rejection Number")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Interface ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Description")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Category")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Sub-Category")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective From")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective To")));

                //for (int i = 0; i < list.Count; i++)
                //{
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Rejection)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].RejectionNo)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].InterfaceID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Description)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Category)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].SubCategory)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                //}

                for (int i = 0; i < visibleCoumns.Count; i++)
                {
                    if (visibleCoumns[i].FieldVisibility)
                    {
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(visibleCoumns[i].FieldDisplayName)));
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < visibleCoumns.Count; j++)
                    {
                        if (visibleCoumns[j].FieldVisibility)
                        {
                            switch (visibleCoumns[j].FieldName)
                            {
                                case "RejectionCode":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Rejection)));
                                    break;
                                case "InterfaceID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].RejectionNo)));
                                    break;
                                case "Description":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Description)));
                                    break;
                                case "Category":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Category)));
                                    break;
                                case "SubCategory":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].SubCategory)));
                                    break;
                                case "EffectiveFrom":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                                    break;
                                case "EffectiveTo":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                                    break;
                            }
                        }
                    }
                }

                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=RejectionCodeData.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }
        #endregion


        #region ------------ MTB Masters--------------------

        public static string generateMTBPDF(List<ColumnList> columnList, string mtb, out string msgToShow)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                if (HttpContext.Current.Session["MTBMastersData"] == null)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }
                List<MTBDetails> list = HttpContext.Current.Session["MTBMastersData"] as List<MTBDetails>;
                if (list.Count <= 0)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("MTB Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                //Contrent
                var visibleCoumns = columnList.Where(x => x.FieldVisibility == true).ToList();

                PdfPTable contentTbl = new PdfPTable(visibleCoumns.Count);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 20;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("MTB ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("MTB Description")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("User Id")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective From")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective To")));

                //for (int i = 0; i < list.Count; i++)
                //{
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MTBID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Description)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].UserId)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                //}


                for (int i = 0; i < visibleCoumns.Count; i++)
                {
                    if (visibleCoumns[i].FieldVisibility)
                    {
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(visibleCoumns[i].FieldDisplayName)));
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < visibleCoumns.Count; j++)
                    {
                        if (visibleCoumns[j].FieldVisibility)
                        {
                            switch (visibleCoumns[j].FieldName)
                            {
                                case "MTBID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MTBID)));
                                    break;
                                case "MTBDescription":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Description)));
                                    break;
                                case "UserID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].UserId)));
                                    break;
                                case "EffectiveFrom":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                                    break;
                                case "EffectiveTo":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                                    break;
                            }
                        }
                    }
                }

                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=MTBData.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }


        public static string generateMTBRolePDF(List<ColumnList> columnList, string mtbid, out string msgToShow)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                if (HttpContext.Current.Session["MTBMastersData"] == null)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }
                List<RoleDetails> list = HttpContext.Current.Session["MTBMastersData"] as List<RoleDetails>;
                if (list.Count <= 0)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("Role Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                //selected input
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldText("MTB: " + mtbid)) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingTop = 10, Border = 0 });

                //Contrent
                var visibleCoumns = columnList.Where(x => x.FieldVisibility == true).ToList();

                PdfPTable contentTbl = new PdfPTable(visibleCoumns.Count);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 10;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Role Name")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective From")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective To")));

                //for (int i = 0; i < list.Count; i++)
                //{
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].RoleName)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                //}

                for (int i = 0; i < visibleCoumns.Count; i++)
                {
                    if (visibleCoumns[i].FieldVisibility)
                    {
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(visibleCoumns[i].FieldDisplayName)));
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < visibleCoumns.Count; j++)
                    {
                        if (visibleCoumns[j].FieldVisibility)
                        {
                            switch (visibleCoumns[j].FieldName)
                            {
                                case "RoleName":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].RoleName)));
                                    break;
                                case "EffectiveFrom":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                                    break;
                                case "EffectiveTo":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                                    break;
                            }
                        }
                    }
                }



                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=MTBRoleData.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }

        public static string generateMTBUserPDF(List<ColumnList> columnList, string mtbid, out string msgToShow)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                if (HttpContext.Current.Session["MTBMastersData"] == null)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }
                List<UserData> list = HttpContext.Current.Session["MTBMastersData"] as List<UserData>;
                if (list.Count <= 0)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("User Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                //selected input
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldText("MTB: " + mtbid)) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingTop = 10, Border = 0 });

                //Contrent
                var visibleCoumns = columnList.Where(x => x.FieldVisibility == true).ToList();

                PdfPTable contentTbl = new PdfPTable(visibleCoumns.Count);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 10;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("MTB ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("User ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Username")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Address")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Email")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Mobile No.")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Role")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective From")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective To")));

                //for (int i = 0; i < list.Count; i++)
                //{
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MTBID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].UserID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Username)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Address)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Email)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MobileNo)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Role)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                //}
                for (int i = 0; i < visibleCoumns.Count; i++)
                {
                    if (visibleCoumns[i].FieldVisibility)
                    {
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(visibleCoumns[i].FieldDisplayName)));
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < visibleCoumns.Count; j++)
                    {
                        if (visibleCoumns[j].FieldVisibility)
                        {
                            switch (visibleCoumns[j].FieldName)
                            {
                                case "MTBID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MTBID)));
                                    break;
                                case "UserID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].UserID)));
                                    break;
                                case "Username":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Username)));
                                    break;
                                case "Address":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Address)));
                                    break;
                                case "Email":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Email)));
                                    break;
                                case "MobileNo":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MobileNo)));
                                    break;
                                case "Role":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Role)));
                                    break;
                                case "EffectiveFrom":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                                    break;
                                case "EffectiveTo":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                                    break;
                            }
                        }
                    }
                }

                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=MTBUserData.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }



        public static string generateMTBMachinePDF(List<ColumnList> columnList, string mtbid, out string msgToShow)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                if (HttpContext.Current.Session["MTBMastersData"] == null)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }
                List<MachineData> list = HttpContext.Current.Session["MTBMastersData"] as List<MachineData>;
                if (list.Count <= 0)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;

                #region ----Machine Information
                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("Machine Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                //selected input
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldText("MTB: " + mtbid)) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingTop = 10, Border = 0 });

                //Contrent
                var visibleCoumns = columnList.Where(x => x.FieldVisibility == true).ToList();

                PdfPTable contentTbl = new PdfPTable(visibleCoumns.Count);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 10;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Machine ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Machine Type")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Machine Model")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("CNC Make")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("CNC Model")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("MTB Sl. No.")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Dispatch Date")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Customer ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective From")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective To")));

                //for (int i = 0; i < list.Count; i++)
                //{
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineType)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineModel)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].CNCMake)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].CNCModel)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].SerialNumber)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].DispatchDate))));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].CustomerID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                //}


                for (int i = 0; i < visibleCoumns.Count; i++)
                {
                    if (visibleCoumns[i].FieldVisibility)
                    {
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(visibleCoumns[i].FieldDisplayName)));
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < visibleCoumns.Count; j++)
                    {
                        if (visibleCoumns[j].FieldVisibility)
                        {
                            switch (visibleCoumns[j].FieldName)
                            {
                                case "MachineID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineID)));
                                    break;
                                case "MTBID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MTBID)));
                                    break;
                                case "MachineType":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineType)));
                                    break;
                                case "MachineModel":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineModel)));
                                    break;
                                case "CNCMake":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].CNCMake)));
                                    break;
                                case "CNCModel":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].CNCModel)));
                                    break;
                                case "MTBSlNo":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].SerialNumber)));
                                    break;
                                case "DispatchDate":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].DispatchDate)));
                                    break;
                                case "CustomerID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].CustomerID)));
                                    break;
                                case "EffectiveFrom":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                                    break;
                                case "EffectiveTo":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                                    break;
                            }
                        }
                    }
                }

                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });
                #endregion

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=MTBMachineData.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }
        #endregion


        #region -------Shop ---------------
        public static string generateShopPDF(List<ColumnList> columnList, string selectedinput, out string msgToShow, string shopname)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                if (HttpContext.Current.Session["PlantLevelMasterData"] == null)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }
                List<PlantLevelDetails> list = HttpContext.Current.Session["PlantLevelMasterData"] as List<PlantLevelDetails>;
                if (list.Count <= 0)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader(shopname + " Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                //selected input
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldText(selectedinput)) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingTop = 10, Border = 0 });

                //Contrent
                var visibleCoumns = columnList.Where(x => x.FieldVisibility == true).ToList();

                PdfPTable contentTbl = new PdfPTable(visibleCoumns.Count);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 10;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Plant ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(shopname + " ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(shopname+" Desc")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Geo Location")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective From")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective To")));

                //for (int i = 0; i < list.Count; i++)
                //{
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].PlantID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].ShopID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Description)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].GeoLocation)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                //}
                for (int i = 0; i < visibleCoumns.Count; i++)
                {
                    if (visibleCoumns[i].FieldVisibility)
                    {
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(visibleCoumns[i].FieldDisplayName)));
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < visibleCoumns.Count; j++)
                    {
                        if (visibleCoumns[j].FieldVisibility)
                        {
                            switch (visibleCoumns[j].FieldName)
                            {
                                case "ShopID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].ShopID)));
                                    break;
                                case "ShopDesc":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Description)));
                                    break;
                                case "GeoLocation":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].GeoLocation)));
                                    break;
                                case "EffectiveFrom":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                                    break;
                                case "EffectiveTo":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                                    break;
                            }
                        }
                    }
                }

                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + shopname + "Data.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }

        public static string generateShopCellPDF(List<ColumnList> columnList, string selectedinput, out string msgToShow, string shopname, string shopcellname)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                if (HttpContext.Current.Session["ShopOrCellLevelMasterData"] == null)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }
                List<PlantLevelDetails> list = HttpContext.Current.Session["ShopOrCellLevelMasterData"] as List<PlantLevelDetails>;
                if (list.Count <= 0)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader(shopcellname + " Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                //selected input
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldText(selectedinput)) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingTop = 10, Border = 0 });

                //Contrent
                var visibleCoumns = columnList.Where(x => x.FieldVisibility == true).ToList();
                PdfPTable contentTbl = new PdfPTable(visibleCoumns.Count);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 10;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Plant ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(shopname + " ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(shopcellname + " ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(shopcellname + " Desc")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective From")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective To")));

                //for (int i = 0; i < list.Count; i++)
                //{
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].PlantID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].ShopID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].CellID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Description)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                //}

                for (int i = 0; i < visibleCoumns.Count; i++)
                {
                    if (visibleCoumns[i].FieldVisibility)
                    {
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(visibleCoumns[i].FieldDisplayName)));
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < visibleCoumns.Count; j++)
                    {
                        if (visibleCoumns[j].FieldVisibility)
                        {
                            switch (visibleCoumns[j].FieldName)
                            {
                                case "CellID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].CellID)));
                                    break;
                                case "CellDesc":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Description)));
                                    break;
                                case "EffectiveFrom":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                                    break;
                                case "EffectiveTo":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                                    break;
                            }
                        }
                    }
                }

                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + shopcellname + "Data.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }

        public static string generateShopGroupPDF(List<ColumnList> columnList, string selectedinput, out string msgToShow, string shopname, string shopgroupname)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                if (HttpContext.Current.Session["ShopOrCellLevelMasterData"] == null)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }
                List<PlantLevelDetails> list = HttpContext.Current.Session["ShopOrCellLevelMasterData"] as List<PlantLevelDetails>;
                if (list.Count <= 0)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader(shopgroupname + " Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                //selected input
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldText(selectedinput)) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingTop = 10, Border = 0 });

                //Contrent
                var visibleCoumns = columnList.Where(x => x.FieldVisibility == true).ToList();
                PdfPTable contentTbl = new PdfPTable(visibleCoumns.Count);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 10;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Plant ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(shopname + " ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(shopgroupname + " ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(shopgroupname + " Desc")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective From")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective To")));

                //for (int i = 0; i < list.Count; i++)
                //{
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].PlantID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].ShopID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].GroupID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Description)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                //}

                for (int i = 0; i < visibleCoumns.Count; i++)
                {
                    if (visibleCoumns[i].FieldVisibility)
                    {
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(visibleCoumns[i].FieldDisplayName)));
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < visibleCoumns.Count; j++)
                    {
                        if (visibleCoumns[j].FieldVisibility)
                        {
                            switch (visibleCoumns[j].FieldName)
                            {
                                case "GroupID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].GroupID)));
                                    break;
                                case "GroupDesc":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Description)));
                                    break;
                                case "EffectiveFrom":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                                    break;
                                case "EffectiveTo":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                                    break;
                            }
                        }
                    }
                }

                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + shopgroupname + "Data.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }
        public static string generateShopCellGroupPDF(List<ColumnList> columnList, string selectedinput, out string msgToShow, string shopname, string shopcellname, string shopcellgroupname)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                if (HttpContext.Current.Session["ShopCellLevelMasterData"] == null)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }
                List<PlantLevelDetails> list = HttpContext.Current.Session["ShopCellLevelMasterData"] as List<PlantLevelDetails>;
                if (list.Count <= 0)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader(shopcellgroupname + " Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                //selected input
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldText(selectedinput)) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingTop = 10, Border = 0 });

                //Contrent
                var visibleCoumns = columnList.Where(x => x.FieldVisibility == true).ToList();
                PdfPTable contentTbl = new PdfPTable(visibleCoumns.Count);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 10;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Plant ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(shopname + " ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(shopcellname + " ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(shopcellgroupname + " ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(shopcellgroupname + " Desc")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective From")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective To")));

                //for (int i = 0; i < list.Count; i++)
                //{
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].PlantID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].ShopID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].CellID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].GroupID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Description)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                //}
                for (int i = 0; i < visibleCoumns.Count; i++)
                {
                    if (visibleCoumns[i].FieldVisibility)
                    {
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(visibleCoumns[i].FieldDisplayName)));
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < visibleCoumns.Count; j++)
                    {
                        if (visibleCoumns[j].FieldVisibility)
                        {
                            switch (visibleCoumns[j].FieldName)
                            {
                                case "CellID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].CellID)));
                                    break;
                                case "GroupID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].GroupID)));
                                    break;
                                case "GroupDesc":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Description)));
                                    break;
                                case "EffectiveFrom":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                                    break;
                                case "EffectiveTo":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                                    break;
                            }
                        }
                    }
                }

                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + shopcellgroupname + "Data.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }
        #endregion

        #region -------Cell ---------------
        public static string generateCellPDF(List<ColumnList> columnList, string selectedinput, out string msgToShow, string cellname)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                if (HttpContext.Current.Session["PlantLevelMasterData"] == null)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }
                List<CellData> list = HttpContext.Current.Session["PlantLevelMasterData"] as List<CellData>;
                if (list.Count <= 0)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader(cellname + " Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                //selected input
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldText(selectedinput)) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingTop = 10, Border = 0 });

                //Contrent
                var visibleCoumns = columnList.Where(x => x.FieldVisibility == true).ToList();
                PdfPTable contentTbl = new PdfPTable(visibleCoumns.Count);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 10;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Plant ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(cellname + " ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(cellname + " Desc")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective From")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective To")));

                //for (int i = 0; i < list.Count; i++)
                //{
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].PlantID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].CellId)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].CellDesc)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                //}
                for (int i = 0; i < visibleCoumns.Count; i++)
                {
                    if (visibleCoumns[i].FieldVisibility)
                    {
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(visibleCoumns[i].FieldDisplayName)));
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < visibleCoumns.Count; j++)
                    {
                        if (visibleCoumns[j].FieldVisibility)
                        {
                            switch (visibleCoumns[j].FieldName)
                            {
                                case "CellID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].CellId)));
                                    break;
                                case "CellDesc":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].CellDesc)));
                                    break;
                                case "EffectiveFrom":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                                    break;
                                case "EffectiveTo":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                                    break;
                            }
                        }
                    }
                }

                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + cellname + "Data.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }

        public static string generateCellGroupPDF(List<ColumnList> columnList, string selectedinput, out string msgToShow, string cellname, string cellgroupname)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                if (HttpContext.Current.Session["ShopOrCellLevelMasterData"] == null)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }
                List<CellData> list = HttpContext.Current.Session["ShopOrCellLevelMasterData"] as List<CellData>;
                if (list.Count <= 0)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader(cellgroupname + " Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                //selected input
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldText(selectedinput)) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingTop = 10, Border = 0 });

                //Contrent
                var visibleCoumns = columnList.Where(x => x.FieldVisibility == true).ToList();
                PdfPTable contentTbl = new PdfPTable(visibleCoumns.Count);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 10;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Plant ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(cellname + " ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(cellgroupname + " ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(cellgroupname + " Desc")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective From")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective To")));

                //for (int i = 0; i < list.Count; i++)
                //{
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].PlantID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].CellId)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].GroupID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].GroupDesc)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                //}

                for (int i = 0; i < visibleCoumns.Count; i++)
                {
                    if (visibleCoumns[i].FieldVisibility)
                    {
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(visibleCoumns[i].FieldDisplayName)));
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < visibleCoumns.Count; j++)
                    {
                        if (visibleCoumns[j].FieldVisibility)
                        {
                            switch (visibleCoumns[j].FieldName)
                            {
                                case "GroupID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].GroupID)));
                                    break;
                                case "GroupDesc":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].GroupDesc)));
                                    break;
                                case "EffectiveFrom":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                                    break;
                                case "EffectiveTo":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                                    break;
                            }
                        }
                    }
                }

                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + cellgroupname + "Data.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }

        #endregion


        #region -------Group ---------------
        public static string generateGroupPDF(List<ColumnList> columnList, string selectedinput, out string msgToShow, string groupname)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                if (HttpContext.Current.Session["PlantLevelMasterData"] == null)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }
                List<CellData> list = HttpContext.Current.Session["PlantLevelMasterData"] as List<CellData>;
                if (list.Count <= 0)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader(groupname + " Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                //selected input
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldText(selectedinput)) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingTop = 10, Border = 0 });

                //Contrent
                var visibleCoumns = columnList.Where(x => x.FieldVisibility == true).ToList();

                PdfPTable contentTbl = new PdfPTable(visibleCoumns.Count);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 20;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Plant ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(groupname + " ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(groupname + " Desc")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective From")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective To")));

                //for (int i = 0; i < list.Count; i++)
                //{
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].PlantID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].GroupID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].GroupDesc)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                //}
                for (int i = 0; i < visibleCoumns.Count; i++)
                {
                    if (visibleCoumns[i].FieldVisibility)
                    {
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(visibleCoumns[i].FieldDisplayName)));
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < visibleCoumns.Count; j++)
                    {
                        if (visibleCoumns[j].FieldVisibility)
                        {
                            switch (visibleCoumns[j].FieldName)
                            {
                                case "GroupID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].GroupID)));
                                    break;
                                case "GroupDesc":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].GroupDesc)));
                                    break;
                                case "EffectiveFrom":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                                    break;
                                case "EffectiveTo":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                                    break;
                            }
                        }
                    }
                }


                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + groupname + "Data.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }

        #endregion

        private static PdfPCell getPdfCellWithBoldHeader(string value)
        {
            iTextSharp.text.Font font = FontFactory.GetFont("Calibri (Body)", 13, iTextSharp.text.Font.BOLD | iTextSharp.text.Font.UNDERLINE);
            iTextSharp.text.Font boldFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 10, iTextSharp.text.Font.BOLD);
            Chunk chunk = new Chunk(value, font);
            chunk.Font.Color = new iTextSharp.text.BaseColor(0, 32, 96);
            chunk.Font.Size = 10;
            Phrase phrase = new Phrase();
            phrase.Add(chunk);
            PdfPCell cell = new PdfPCell(phrase);
            cell.ExtraParagraphSpace = 3;
            cell.BorderColor = new BaseColor(122, 121, 121);
            return cell;
        }
        private static PdfPCell getPdfCellContentText(string value)
        {
            iTextSharp.text.Font font = FontFactory.GetFont("Calibri (Body)", 10);
            Chunk chunk = new Chunk(value, font);
            chunk.Font.Color = new iTextSharp.text.BaseColor(10, 10, 10);
            chunk.Font.Size = 7;
            Phrase phrase = new Phrase();
            phrase.Add(chunk);
            PdfPCell cell = new PdfPCell(phrase);
            cell.ExtraParagraphSpace = 3;
            cell.BorderColor = new BaseColor(122, 121, 121);
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            return cell;
        }
        private static PdfPCell getPdfCellTableHeaderText(string value)
        {
            iTextSharp.text.Font font = FontFactory.GetFont("Calibri (Body)", 11, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font boldFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 10, iTextSharp.text.Font.BOLD);
            Chunk chunk = new Chunk(value, font);
            chunk.Font.Color = new iTextSharp.text.BaseColor(77, 77, 77);
            chunk.Font.Size = 8;
            Phrase phrase = new Phrase();
            phrase.Add(chunk);
            PdfPCell cell = new PdfPCell(phrase);
            cell.ExtraParagraphSpace = 3;
            cell.BorderColor = new BaseColor(122, 121, 121);
            cell.BackgroundColor = new BaseColor(214, 215, 223);
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            return cell;
        }
        private static PdfPCell getPdfCellWithBoldText(string value)
        {
            iTextSharp.text.Font font = FontFactory.GetFont("Calibri (Body)", 11, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font boldFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 10, iTextSharp.text.Font.BOLD);
            Chunk chunk = new Chunk(value, font);
            chunk.Font.Color = new iTextSharp.text.BaseColor(77, 77, 77);
            chunk.Font.Size = 8;
            Phrase phrase = new Phrase();
            phrase.Add(chunk);
            PdfPCell cell = new PdfPCell(phrase);
            cell.ExtraParagraphSpace = 3;
            cell.BorderColor = new BaseColor(122, 121, 121);
            return cell;
        }
        private static string getDateTimeFormate(string input)
        {
            string datetime = "";
            try
            {
                if (input != "")
                {
                    datetime = Util.GetDateTime(input).ToString("dd-MM-yyyy HH:mm:ss");
                }

            }
            catch (Exception ex)
            {

            }
            return datetime;
        }
        private static void addCheckBox(PdfPTable table, bool IsCheck)
        {
            try
            {
                if (IsCheck)
                {
                    byte[] checkfile;
                    checkfile = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/Images/Check.png"));//ImagePath  
                    iTextSharp.text.Image checkjpg = iTextSharp.text.Image.GetInstance(checkfile);
                    checkjpg.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                    checkjpg.ScaleToFit(10f, 10f);
                    PdfPCell cell = new PdfPCell(checkjpg, false);
                    cell.BorderWidth = 1;
                    cell.Padding = 2;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    table.AddCell(cell).BorderColor = new BaseColor(122, 121, 121);
                }
                else
                {
                    byte[] uncheckfile;
                    uncheckfile = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/Images/Uncheck.png"));//ImagePath  
                    iTextSharp.text.Image uncheckjpg = iTextSharp.text.Image.GetInstance(uncheckfile);
                    uncheckjpg.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                    uncheckjpg.ScaleToFit(10f, 10f);
                    PdfPCell cell = new PdfPCell(uncheckjpg, false);
                    cell.BorderWidth = 1;
                    cell.Padding = 2;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    table.AddCell(cell).BorderColor = new BaseColor(122, 121, 121);
                }
            }
            catch (Exception ex)
            {

            }
        }


        #region ------Signature Comparision Masters-------------
        public static string generateSignatureComparisionMasterPDF(out string msgToShow)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                if (HttpContext.Current.Session["SigntrCompMasterData"] == null)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }
                List<SignatureComparisionDetails> list = HttpContext.Current.Session["SigntrCompMasterData"] as List<SignatureComparisionDetails>;
                if (list.Count <= 0)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("Signatue Comparison Master Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                //Contrent
                PdfPTable contentTbl = new PdfPTable(4);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 20;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Signature ID")));
                contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Parameter ID")));
                contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Seconds")));
                contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Signature Value")));

                for (int i = 0; i < list.Count; i++)
                {
                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].SignatureID)));
                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].ParameterID)));
                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Frequency)));
                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].ParameterValue)));
                }

                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=SignatureComparisonData.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }
        #endregion

        #region ------Component Information Masters-------------
        public static string generateComponentInfoMasterPDF(List<ColumnList> columnList, string company, out string msgToShow)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                if (HttpContext.Current.Session["ComponentMasterData"] == null)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }
                List<ComponentInfoData> list = HttpContext.Current.Session["ComponentMasterData"] as List<ComponentInfoData>;
                if (list.Count <= 0)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("Component Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });


                //selected input
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldText("")) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingTop = 10, Border = 0 });

                //Contrent
                var visibleCoumns = columnList.Where(x => x.FieldVisibility == true).ToList();

                PdfPTable contentTbl = new PdfPTable(visibleCoumns.Count);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 10;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Component ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("IOT ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Customer")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Description")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective From")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Effective To")));
                //for (int i = 0; i < list.Count; i++)
                //{
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].ComponentID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Interfaceid)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Customer)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Description)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                //}
                for (int i = 0; i < visibleCoumns.Count; i++)
                {
                    if (visibleCoumns[i].FieldVisibility)
                    {
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(visibleCoumns[i].FieldDisplayName)));
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < visibleCoumns.Count; j++)
                    {
                        if (visibleCoumns[j].FieldVisibility)
                        {
                            switch (visibleCoumns[j].FieldName)
                            {
                                case "ComponentID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].ComponentID)));
                                    break;
                                case "InterfaceID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Interfaceid)));
                                    break;
                                case "Customer":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Customer)));
                                    break;
                                case "Description":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Description)));
                                    break;
                                case "EffectiveFrom":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveFromDate))));
                                    break;
                                case "EffectiveTo":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].EffectiveToDate))));
                                    break;
                            }
                        }
                    }
                }

                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=ComponentData.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }
        #endregion
        #region ------Inspection Information Masters-------------
        public static string generateInspectionMasterPDF(List<ColumnList> columnList, out string msgToShow)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                if (HttpContext.Current.Session["InspectionMasterData"] == null)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }
                List<InspectionData> list = HttpContext.Current.Session["InspectionMasterData"] as List<InspectionData>;
                if (list.Count <= 0)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("Inspection Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                //filter
                string company = "", plant = "", component = "", operation = "", revnumber = "";
                if (list.Count > 0)
                {
                    company = list[0].CompanyID;
                    plant = list[0].PlantID;
                    component = list[0].ComponentID;
                    operation = list[0].Operation;
                    revnumber = list[0].RevNumber;
                }
                PdfPTable contentTbl = new PdfPTable(5);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 20;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                contentTbl.AddCell(new PdfPCell(getPdfCellWithBoldText("")));
                contentTbl.AddCell(new PdfPCell(getPdfCellWithBoldText("Plant: " + plant)));
                contentTbl.AddCell(new PdfPCell(getPdfCellWithBoldText("Component: " + component)));
                contentTbl.AddCell(new PdfPCell(getPdfCellWithBoldText("Operation: " + operation)));
                contentTbl.AddCell(new PdfPCell(getPdfCellWithBoldText("Revision Number: " + revnumber)));
                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                //Contrent
                var visibleCoumns = columnList.Where(x => x.FieldVisibility == true).ToList();

                contentTbl = new PdfPTable(visibleCoumns.Count);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 20;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Characteristic ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Characteristic Code")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("LSL")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Lower Warning Zone")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Lower Operating Zone")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Specific Mean")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Upper Operating Zone")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Upper Warning Zone")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("USL")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Unit")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Sample Size")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Input Method")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Data Template")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("List of Value")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Inspected By")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Sort Order")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Rev Date")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Is Mandatory?")));
                //for (int i = 0; i < list.Count; i++)
                //{
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].CharacteristicID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].CharacteristicDesc)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].LSL)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].LWarning)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].LOperating)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].SpecificMean)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].UOperating)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].UWarning)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].USL)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Unit)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].SampleSize)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].InputMethod)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].DataTemplateText)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].ListOfValue)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].InspectedByText)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].SortOrder)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].RevDate)));
                //    addCheckBox(contentTbl, list[i].IsMandatory);
                //}

                for (int i = 0; i < visibleCoumns.Count; i++)
                {
                    if (visibleCoumns[i].FieldVisibility)
                    {
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(visibleCoumns[i].FieldDisplayName)));
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < visibleCoumns.Count; j++)
                    {
                        if (visibleCoumns[j].FieldVisibility)
                        {
                            switch (visibleCoumns[j].FieldName)
                            {
                                case "ComponentID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].ComponentID)));
                                    break;
                                case "Operation":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Operation)));
                                    break;
                                case "CharacteristicID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].CharacteristicID)));
                                    break;
                                case "CharacteristicDesc":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].CharacteristicDesc)));
                                    break;
                                case "LSL":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].LSL)));
                                    break;
                                case "LowerWarningZone":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].LWarning)));
                                    break;
                                case "LowerOperatingZone":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].LOperating)));
                                    break;
                                case "SpecificMean":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].SpecificMean)));
                                    break;
                                case "UpperOperatingZone":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].UOperating)));
                                    break;
                                case "UpperWarningZone":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].UWarning)));
                                    break;
                                case "USL":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].USL)));
                                    break;
                                case "Unit":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Unit)));
                                    break;
                                case "SampleSize":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].SampleSize)));
                                    break;
                                case "InputMethod":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].InputMethod)));
                                    break;
                                case "DataTemplate":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].DataTemplateText)));
                                    break;
                                case "ListOfValue":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].ListOfValue)));
                                    break;
                                case "InspectedBy":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].InspectedByText)));
                                    break;
                                case "SortOrder":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].SortOrder)));
                                    break;
                                case "RevDate":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].RevDate)));
                                    break;
                                case "IsMandatory":
                                    addCheckBox(contentTbl, list[i].IsMandatory);
                                    break;
                                case "MacroLocation":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MacroLocation)));
                                    break;
                                case "MeasurementCondition":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MeasurementCondition)));
                                    break;
                                case "SpecificationOfEquipment":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].SpecificationOfEquipment)));
                                    break;
                            }
                        }
                    }
                }

                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=InspectionData.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }
        #endregion

        #region ------Customer Level PM Masters-------------
        public static string generateCustomerLvlPMMasterPDF(List<ColumnList> columnList, string frequency, out string msgToShow)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                if (HttpContext.Current.Session["PMMasterData"] == null)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }
                List<PMMasterData> list = HttpContext.Current.Session["PMMasterData"] as List<PMMasterData>;
                if (list.Count <= 0)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("Customer Level PM Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                //filter
                string company = "", plant = "", mtb = "", machinemodel = "", machineid = "";
                if (list.Count > 0)
                {
                    company = list[0].CompanyID;
                    plant = list[0].PlantID;
                    mtb = list[0].MTB;
                    machinemodel = list[0].MachineModel;
                    machineid = list[0].MachineID;
                }
                PdfPTable contentTbl = new PdfPTable(6);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 20;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                contentTbl.AddCell(new PdfPCell(getPdfCellWithBoldText("")));
                contentTbl.AddCell(new PdfPCell(getPdfCellWithBoldText("Plant: " + plant)));
                contentTbl.AddCell(new PdfPCell(getPdfCellWithBoldText("MTB: " + mtb)));
                contentTbl.AddCell(new PdfPCell(getPdfCellWithBoldText("Machine Model: " + machinemodel)));
                contentTbl.AddCell(new PdfPCell(getPdfCellWithBoldText("Machine ID: " + machineid)));
                contentTbl.AddCell(new PdfPCell(getPdfCellWithBoldText("Frequency: " + frequency)));
                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                //Contrent
                var visibleCoumnsTemp = columnList.Where(x => x.FieldVisibility == true).ToList();
                var visibleCoumns = visibleCoumnsTemp.Where(x => x.FieldName != "SOP").ToList();

                contentTbl = new PdfPTable(visibleCoumns.Count);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 20;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("SL No.")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("MTB")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Machine Model")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Machine ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Item ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Area")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Activity")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Activity Type")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Frequency")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Check Point")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Target time (min)")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Machine Status")));
                //for (int i = 0; i < list.Count; i++)
                //{
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].SerialNumber)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MTB)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineModel)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].UniqueCode)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Area)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Activity)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].ActivityType)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Frequency)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].CheckPoint)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].TargetTime)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineStatus)));
                //}
                for (int i = 0; i < visibleCoumns.Count; i++)
                {
                    if (visibleCoumns[i].FieldVisibility)
                    {
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(visibleCoumns[i].FieldDisplayName)));
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < visibleCoumns.Count; j++)
                    {
                        if (visibleCoumns[j].FieldVisibility)
                        {
                            switch (visibleCoumns[j].FieldName)
                            {
                                case "MachineModel":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineModel)));
                                    break;
                                case "SlNo":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].SerialNumber)));
                                    break;
                                case "MTB":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MTB)));
                                    break;
                                case "ItemID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].UniqueCode)));
                                    break;
                                case "Area":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Area)));
                                    break;
                                case "Activity":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Activity)));
                                    break;
                                case "ActivityType":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].ActivityType)));
                                    break;
                                case "Frequency":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Frequency)));
                                    break;
                                case "CheckPoint":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].CheckPoint)));
                                    break;
                                case "TargetTime":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].TargetTime)));
                                    break;
                                case "MachineStatus":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineStatus)));
                                    break;
                                case "LSL":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].LSL)));
                                    break;
                                case "USL":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].USL)));
                                    break;
                                case "EntryType":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].EntryType)));
                                    break;
                            }
                        }
                    }
                }


                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=PMData.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }
        #endregion

        #region ------Model Level PM Masters-------------
        public static string generateModelLvlPMMasterPDF(List<ColumnList> columnList, string frequency, out string msgToShow)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                if (HttpContext.Current.Session["PMMasterData"] == null)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }
                List<PMMasterData> list = HttpContext.Current.Session["PMMasterData"] as List<PMMasterData>;
                if (list.Count <= 0)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("Model Level PM Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                //filter
                string mtb = "", machinemodel = "";
                if (list.Count > 0)
                {
                    mtb = list[0].MTB;
                    machinemodel = list[0].MachineModel;
                }
                PdfPTable contentTbl = new PdfPTable(3);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 20;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                contentTbl.AddCell(new PdfPCell(getPdfCellWithBoldText("MTB: " + mtb)));
                contentTbl.AddCell(new PdfPCell(getPdfCellWithBoldText("Machine Model: " + machinemodel)));
                contentTbl.AddCell(new PdfPCell(getPdfCellWithBoldText("Frequency: " + frequency)));
                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                //Contrent
                var visibleCoumnsTemp = columnList.Where(x => x.FieldVisibility == true).ToList();
                var visibleCoumns = visibleCoumnsTemp.Where(x => x.FieldName != "SOP").ToList();

                contentTbl = new PdfPTable(visibleCoumns.Count);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 20;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("SL No.")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("MTB")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Machine Model")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Item ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Area")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Activity")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Activity Type")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Frequency")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Check Point")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Target time (min)")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Machine Status")));
                //for (int i = 0; i < list.Count; i++)
                //{
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].SerialNumber)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MTB)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineModel)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].UniqueCode)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Area)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Activity)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].ActivityType)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Frequency)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].CheckPoint)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].TargetTime)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineStatus)));
                //}

                for (int i = 0; i < visibleCoumns.Count; i++)
                {
                    if (visibleCoumns[i].FieldVisibility)
                    {
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(visibleCoumns[i].FieldDisplayName)));
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < visibleCoumns.Count; j++)
                    {
                        if (visibleCoumns[j].FieldVisibility)
                        {
                            switch (visibleCoumns[j].FieldName)
                            {
                                case "SlNo":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].SerialNumber)));
                                    break;
                                case "MTB":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MTB)));
                                    break;
                                case "MachineModel":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineModel)));
                                    break;
                                case "ItemID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].UniqueCode)));
                                    break;
                                case "Area":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Area)));
                                    break;
                                case "Activity":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Activity)));
                                    break;
                                case "ActivityType":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].ActivityType)));
                                    break;
                                case "Frequency":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Frequency)));
                                    break;
                                case "CheckPoint":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].CheckPoint)));
                                    break;
                                case "TargetTime":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].TargetTime)));
                                    break;
                                case "MachineStatus":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineStatus)));
                                    break;
                                case "LSL":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].LSL)));
                                    break;
                                case "USL":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].USL)));
                                    break;
                                case "EntryType":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].EntryType)));
                                    break;
                            }
                        }
                    }
                }

                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=ModelLevelPMData.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }
        #endregion

        #region ------Machine Level PM Masters-------------
        public static string generateMachineLvlPMMasterPDF(List<ColumnList> columnList, string machineslno, string frequency, out string msgToShow)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                if (HttpContext.Current.Session["PMMasterData"] == null)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }
                List<PMMasterData> list = HttpContext.Current.Session["PMMasterData"] as List<PMMasterData>;
                if (list.Count <= 0)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("Machine Level PM Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                //filter
                string mtb = "", machinemodel = "";
                if (list.Count > 0)
                {
                    mtb = list[0].MTB;
                    machinemodel = list[0].MachineModel;
                }
                PdfPTable contentTbl = new PdfPTable(4);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 20;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                contentTbl.AddCell(new PdfPCell(getPdfCellWithBoldText("MTB: " + mtb)));
                contentTbl.AddCell(new PdfPCell(getPdfCellWithBoldText("Machine Model: " + machinemodel)));
                contentTbl.AddCell(new PdfPCell(getPdfCellWithBoldText("Machine Sl. No.: " + machineslno)));
                contentTbl.AddCell(new PdfPCell(getPdfCellWithBoldText("Frequency: " + frequency)));
                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                //Contrent
                var visibleCoumnsTemp = columnList.Where(x => x.FieldVisibility == true).ToList();
                var visibleCoumns = visibleCoumnsTemp.Where(x => x.FieldName != "SOP").ToList();

                contentTbl = new PdfPTable(visibleCoumns.Count);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 20;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("SL No.")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("MTB")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Machine Model")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Machine Sl. No.")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Item ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Area")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Activity")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Activity Type")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Frequency")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Check Point")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Target time (min)")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Machine Status")));
                //for (int i = 0; i < list.Count; i++)
                //{
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].SerialNumber)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MTB)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineModel)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineSerialNumber)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].UniqueCode)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Area)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Activity)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].ActivityType)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Frequency)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].CheckPoint)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].TargetTime)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineStatus)));
                //}
                for (int i = 0; i < visibleCoumns.Count; i++)
                {
                    if (visibleCoumns[i].FieldVisibility)
                    {
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(visibleCoumns[i].FieldDisplayName)));
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < visibleCoumns.Count; j++)
                    {
                        if (visibleCoumns[j].FieldVisibility)
                        {
                            switch (visibleCoumns[j].FieldName)
                            {
                                case "SlNo":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].SerialNumber)));
                                    break;
                                case "MTB":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MTB)));
                                    break;
                                case "MachineModel":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineModel)));
                                    break;
                                case "ItemID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].UniqueCode)));
                                    break;
                                case "Area":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Area)));
                                    break;
                                case "Activity":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Activity)));
                                    break;
                                case "ActivityType":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].ActivityType)));
                                    break;
                                case "Frequency":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Frequency)));
                                    break;
                                case "CheckPoint":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].CheckPoint)));
                                    break;
                                case "TargetTime":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].TargetTime)));
                                    break;
                                case "MachineStatus":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineStatus)));
                                    break;
                                case "LSL":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].LSL)));
                                    break;
                                case "USL":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].USL)));
                                    break;
                                case "EntryType":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].EntryType)));
                                    break;
                            }
                        }
                    }
                }

                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=MachineLevelPMData.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }
        #endregion

        #region ------Week Definition Master-------------
        public static string generateWeekDefinationPDF(string company, string year, string month, string weeknumber, out string msgToShow)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                if (HttpContext.Current.Session["WeekDefData"] == null)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }
                DataTable list = HttpContext.Current.Session["WeekDefData"] as DataTable;
                if (list.Rows.Count <= 0)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("Week Definition")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                //filter

                PdfPTable contentTbl = new PdfPTable(4);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 20;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                contentTbl.AddCell(new PdfPCell(getPdfCellWithBoldText("")));
                contentTbl.AddCell(new PdfPCell(getPdfCellWithBoldText("Year: " + year)));
                contentTbl.AddCell(new PdfPCell(getPdfCellWithBoldText("Month: " + month)));
                contentTbl.AddCell(new PdfPCell(getPdfCellWithBoldText("Week Number: " + weeknumber)));
                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                //Contrent
                contentTbl = new PdfPTable(2);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 20;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Week Date")));
                contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Week Number : Month : Year")));
                for (int i = 0; i < list.Rows.Count; i++)
                {
                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list.Rows[i]["WeekDate"].ToString())));
                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list.Rows[i]["WeekMonthYear"].ToString())));
                }

                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=WeekDefinition.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }
        #endregion

        #region ------Shift Details Master-------------
        public static string generateShiftDetailsPDF(List<ColumnList> columnList, string company, out string msgToShow)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                if (HttpContext.Current.Session["ShiftMasterData"] == null)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }
                List<ShiftDataModel> list = HttpContext.Current.Session["ShiftMasterData"] as List<ShiftDataModel>;
                if (list.Count <= 0)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("Shift Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                //selected input
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldText("")) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingTop = 10, Border = 0 });

                //Contrent
                var visibleCoumns = columnList.Where(x => x.FieldVisibility == true).ToList();

                PdfPTable contentTbl = new PdfPTable(visibleCoumns.Count);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 10;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Shift ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Shift Name")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("From Day")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("From Time")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("To Day")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("To Time")));
                //for (int i = 0; i < list.Count; i++)
                //{
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].shiftId)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].ShiftName)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].FromDay)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].FromTime)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].ToDay)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].ToTime)));
                //}
                for (int i = 0; i < visibleCoumns.Count; i++)
                {
                    if (visibleCoumns[i].FieldVisibility)
                    {
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(visibleCoumns[i].FieldDisplayName)));
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < visibleCoumns.Count; j++)
                    {
                        if (visibleCoumns[j].FieldVisibility)
                        {
                            switch (visibleCoumns[j].FieldName)
                            {
                                case "ShiftID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].shiftId)));
                                    break;
                                case "ShiftName":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].ShiftName)));
                                    break;
                                case "FromDay":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].FromDay)));
                                    break;
                                case "FromTime":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].FromTime)));
                                    break;
                                case "ToDay":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].ToDay)));
                                    break;
                                case "ToTime":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].ToTime)));
                                    break;
                            }
                        }
                    }
                }

                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=ShiftDetails.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }
        #endregion

        #region ------Planned Down Time Master-------------
        public static string generateHolidayPDTPDF(List<ColumnList> columnList, out string msgToShow)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                if (HttpContext.Current.Session["HolidayList"] == null)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }
                List<HolidayListDetails> list = HttpContext.Current.Session["HolidayList"] as List<HolidayListDetails>;
                if (list.Count <= 0)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("Holiday Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });


                //selected input
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldText("")) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingTop = 10, Border = 0 });

                //Contrent
                var visibleCoumns = columnList.Where(x => x.FieldVisibility == true).ToList();

                PdfPTable contentTbl = new PdfPTable(visibleCoumns.Count);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 10;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Date")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Down Reason")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Machine")));
                //for (int i = 0; i < list.Count; i++)
                //{
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Holiday)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Reason)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineID)));
                //}
                for (int i = 0; i < visibleCoumns.Count; i++)
                {
                    if (visibleCoumns[i].FieldVisibility)
                    {
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(visibleCoumns[i].FieldDisplayName)));
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < visibleCoumns.Count; j++)
                    {
                        if (visibleCoumns[j].FieldVisibility)
                        {
                            switch (visibleCoumns[j].FieldName)
                            {
                                case "Date":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Holiday)));
                                    break;
                                case "DownReason":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Reason)));
                                    break;
                                case "Machine":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineID)));
                                    break;
                            }
                        }
                    }
                }

                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=HolidatList.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }
        public static string generateWeeklyOffPDTPDF(List<ColumnList> columnList, out string msgToShow)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                if (HttpContext.Current.Session["PDTList"] == null)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }
                List<PDTData> list = HttpContext.Current.Session["PDTList"] as List<PDTData>;
                if (list.Count <= 0)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("Weekly Off Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                //selected input
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldText("")) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingTop = 10, Border = 0 });

                //Contrent
                var visibleCoumns = columnList.Where(x => x.FieldVisibility == true).ToList();

                PdfPTable contentTbl = new PdfPTable(visibleCoumns.Count);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 10;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Date")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Day")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Down Reason")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Machine")));
                //for (int i = 0; i < list.Count; i++)
                //{
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].FromDateTime)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Day)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Reason)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineID)));
                //}
                for (int i = 0; i < visibleCoumns.Count; i++)
                {
                    if (visibleCoumns[i].FieldVisibility)
                    {
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(visibleCoumns[i].FieldDisplayName)));
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < visibleCoumns.Count; j++)
                    {
                        if (visibleCoumns[j].FieldVisibility)
                        {
                            switch (visibleCoumns[j].FieldName)
                            {
                                case "Date":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].FromDateTime)));
                                    break;
                                case "Day":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Day)));
                                    break;
                                case "DownReason":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Reason)));
                                    break;
                                case "Machine":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineID)));
                                    break;
                            }
                        }
                    }
                }

                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=WeekOffList.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }
        public static string generateDailyDownPDTPDF(List<ColumnList> columnList,out string msgToShow)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                if (HttpContext.Current.Session["PDTList"] == null)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }
                List<PDTData> list = HttpContext.Current.Session["PDTList"] as List<PDTData>;
                if (list.Count <= 0)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("Daily Downs Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                //selected input
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldText("")) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingTop = 10, Border = 0 });

                //Contrent
                var visibleCoumns = columnList.Where(x => x.FieldVisibility == true).ToList();
                PdfPTable contentTbl = new PdfPTable(visibleCoumns.Count);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 10;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Start DateTime")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("End DateTime")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Down Reason")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Machine")));
                //for (int i = 0; i < list.Count; i++)
                //{
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].FromDateTime)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].ToDateTime)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Reason)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineID)));
                //}

                for (int i = 0; i < visibleCoumns.Count; i++)
                {
                    if (visibleCoumns[i].FieldVisibility)
                    {
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(visibleCoumns[i].FieldDisplayName)));
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < visibleCoumns.Count; j++)
                    {
                        if (visibleCoumns[j].FieldVisibility)
                        {
                            switch (visibleCoumns[j].FieldName)
                            {
                                case "StartDateTime":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].FromDateTime)));
                                    break;
                                case "EndDateTime":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].ToDateTime)));
                                    break;
                                case "DownReason":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Reason)));
                                    break;
                                case "Machine":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineID)));
                                    break;
                            }
                        }
                    }
                }

                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=DailyDownList.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }
        public static string generateShiftDownPDTPDF(List<ColumnList> columnList , out string msgToShow)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                if (HttpContext.Current.Session["PDTList"] == null)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }
                List<PDTData> list = HttpContext.Current.Session["PDTList"] as List<PDTData>;
                if (list.Count <= 0)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("Shift Downs Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                //selected input
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldText("")) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingTop = 10, Border = 0 });

                //Contrent
                var visibleCoumns = columnList.Where(x => x.FieldVisibility == true).ToList();

                PdfPTable contentTbl = new PdfPTable(visibleCoumns.Count);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 10;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Machine")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Shift")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Down Reason")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Date")));
                //for (int i = 0; i < list.Count; i++)
                //{
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].ShiftName)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Reason)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].FromDateTime)));
                //}
                for (int i = 0; i < visibleCoumns.Count; i++)
                {
                    if (visibleCoumns[i].FieldVisibility)
                    {
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(visibleCoumns[i].FieldDisplayName)));
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < visibleCoumns.Count; j++)
                    {
                        if (visibleCoumns[j].FieldVisibility)
                        {
                            switch (visibleCoumns[j].FieldName)
                            {
                                //case "Machine":
                                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineID)));
                                //    break;
                                case "Shift":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].ShiftName)));
                                    break;
                                case "DownReason":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Reason)));
                                    break;
                                case "Date":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].FromDateTime)));
                                    break;
                            }
                        }
                    }
                }

                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=ShiftDownList.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }

        public static string generateAllDownPDTPDF(List<ColumnList> columnList, string headername, out string msgToShow)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                if (HttpContext.Current.Session["PDTList"] == null)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }
                List<PDTData> list = HttpContext.Current.Session["PDTList"] as List<PDTData>;
                if (list.Count <= 0)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader(headername + " Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                //selected input
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldText("")) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingTop = 10, Border = 0 });

                //Contrent
                var visibleCoumns = columnList.Where(x => x.FieldVisibility == true).ToList();

                PdfPTable contentTbl = new PdfPTable(visibleCoumns.Count);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 10;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Holiday")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Down Reason")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Machine")));
                //for (int i = 0; i < list.Count; i++)
                //{
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].FromDateTime)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Reason)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineID)));
                //}
                for (int i = 0; i < visibleCoumns.Count; i++)
                {
                    if (visibleCoumns[i].FieldVisibility)
                    {
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(visibleCoumns[i].FieldDisplayName)));
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < visibleCoumns.Count; j++)
                    {
                        if (visibleCoumns[j].FieldVisibility)
                        {
                            switch (visibleCoumns[j].FieldName)
                            {
                                case "Holiday":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].FromDateTime)));
                                    break;
                                case "DownCategory":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].DownType)));
                                    break;
                                case "DownReason":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Reason)));
                                    break;
                                case "Machine":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineID)));
                                    break;
                            }
                        }
                    }
                }


                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=PDTList.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }
        #endregion


        #region ----- Hour Definition Masters-------------
        public static string generateHourlyDefinitionPDF(string shift, string fromday, string fromtime, string today, string totime, out string msgToShow)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                if (HttpContext.Current.Session["HourlyDatatable"] == null)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }
                DataTable list = HttpContext.Current.Session["HourlyDatatable"] as DataTable;
                if (list.Rows.Count <= 0)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("Hour Definition Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                //filter
                PdfPTable contentTbl = new PdfPTable(3);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 20;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                contentTbl.AddCell(new PdfPCell(getPdfCellWithBoldText("Shift: " + shift)));
                contentTbl.AddCell(new PdfPCell(getPdfCellWithBoldText("From: " + fromday + " - " + fromtime)));
                contentTbl.AddCell(new PdfPCell(getPdfCellWithBoldText("To: " + today + " - " + totime)));
                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });


                //Contrent
                contentTbl = new PdfPTable(4);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 20;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Hour Definition")));
                contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("From Time")));
                contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Minutes")));
                contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("To Time")));

                for (int i = 0; i < list.Rows.Count; i++)
                {
                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list.Rows[i]["HourName"].ToString())));
                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(Util.GetDateTime(list.Rows[i]["HourStart"].ToString()).ToString("hh:mm:ss tt"))));
                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list.Rows[i]["Minutes"].ToString())));
                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(Util.GetDateTime(list.Rows[i]["HourEnd"].ToString()).ToString("hh:mm:ss tt"))));
                }

                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=HourDefinitionData.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }
        #endregion

        #region ------Focas OEE Macro Masters-------------
        public static string generateFocasOEEMacroLocationPDF(string company, string machine, string protocol, string interfaceid, string selctedSubMenu, out string msgToShow)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                if (HttpContext.Current.Session["OEEMacroLocData"] == null)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

                string title = "", colSpan = "";

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;

                PdfPTable contentTbl = new PdfPTable(4);

                PdfPTable filterTbl = new PdfPTable(4);
                filterTbl.WidthPercentage = 100;
                filterTbl.SpacingBefore = 20;
                filterTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                filterTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                filterTbl.AddCell(new PdfPCell(getPdfCellWithBoldText("")) { HorizontalAlignment = Element.ALIGN_LEFT, });
                filterTbl.AddCell(new PdfPCell(getPdfCellWithBoldText("Machine: " + machine)) { HorizontalAlignment = Element.ALIGN_LEFT, });
                filterTbl.AddCell(new PdfPCell(getPdfCellWithBoldText("Interface: " + interfaceid)) { HorizontalAlignment = Element.ALIGN_LEFT, });
                filterTbl.AddCell(new PdfPCell(getPdfCellWithBoldText("Protocol: " + protocol)) { HorizontalAlignment = Element.ALIGN_LEFT, });
                int minHeight = 0;
                int spacebefore = 7;
                if (protocol.Equals("FOCAS", StringComparison.OrdinalIgnoreCase))
                {
                    if (selctedSubMenu.Equals("#FocasMenu2"))
                    {
                        SPCFocasEntity list = HttpContext.Current.Session["OEEMacroLocData"] as SPCFocasEntity;
                        //header
                        mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("Inspection Macro Location Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                        mainTable.AddCell(new PdfPCell(filterTbl) { Colspan = 1, Border = 0 });
                        mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("")) { HorizontalAlignment = Element.ALIGN_CENTER, Border = 0, MinimumHeight = minHeight });

                        //Contrent
                        contentTbl = new PdfPTable(2);
                        contentTbl.WidthPercentage = 100;
                        contentTbl.SpacingBefore = spacebefore;
                        contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Data Read Flag Macro Location")));
                        contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list.ReadFlagLoc)));
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Component Macro Location")));
                        contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list.ComponentLoc)));
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Operation Macro Location")));
                        contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list.OperationLoc)));
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Date Macro Location")));
                        contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list.DateLoc)));
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Time Macro Location")));
                        contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list.TimeLoc)));
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Is Enabled?")));
                        addCheckBox(contentTbl, list.IsEnabled);
                    }
                    else
                    {
                        List<OEEMacroLocationData> list = HttpContext.Current.Session["OEEMacroLocData"] as List<OEEMacroLocationData>;
                        //header
                        mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("OEE Macro Location Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                        mainTable.AddCell(new PdfPCell(filterTbl) { Colspan = 1, Border = 0 });
                        mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("")) { HorizontalAlignment = Element.ALIGN_CENTER, Border = 0, MinimumHeight = minHeight });

                        //Contrent
                        contentTbl = new PdfPTable(4);
                        contentTbl.WidthPercentage = 100;
                        contentTbl.SpacingBefore = spacebefore;
                        contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Machine ID")));
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Data Read Flag Location")));
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Data Start Location")));
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Data End Location")));

                        for (int i = 0; i < list.Count; i++)
                        {
                            contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineID)));
                            contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].DataReadFlagLocation)));
                            contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].DataStartLocation)));
                            contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].DataEndLocation)));
                        }

                    }

                }
                else if (interfaceid.Equals("Equator", StringComparison.OrdinalIgnoreCase))
                {
                    List<SPCEquatorEntity> list = HttpContext.Current.Session["OEEMacroLocData"] as List<SPCEquatorEntity>;
                    //header
                    mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("Equator Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                    mainTable.AddCell(new PdfPCell(filterTbl) { Colspan = 1, Border = 0 });
                    mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("")) { HorizontalAlignment = Element.ALIGN_CENTER, Border = 0, MinimumHeight = minHeight });

                    //Contrent
                    contentTbl = new PdfPTable(5);
                    contentTbl.WidthPercentage = 100;
                    contentTbl.SpacingBefore = spacebefore;
                    contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Equator Name")));
                    contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Folder Path")));
                    contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("User ID")));
                    contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Template Name")));
                    contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("CO Selection")));

                    for (int i = 0; i < list.Count; i++)
                    {
                        contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].EquatorName)));
                        contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].FolderPath)));
                        contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].UserID)));
                        contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].TemplateNameText)));
                        contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].COSelectionText)));
                    }

                }
                else if (interfaceid.Equals("TID (GaugeData)", StringComparison.OrdinalIgnoreCase))
                {
                    List<SPCTIDGaugeEntity> list = HttpContext.Current.Session["OEEMacroLocData"] as List<SPCTIDGaugeEntity>;
                    //header
                    mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("TID (GaugeData) Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                    mainTable.AddCell(new PdfPCell(filterTbl) { Colspan = 1, Border = 0 });
                    mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("")) { HorizontalAlignment = Element.ALIGN_CENTER, Border = 0, MinimumHeight = minHeight });

                    //Contrent
                    contentTbl = new PdfPTable(4);
                    contentTbl.WidthPercentage = 100;
                    contentTbl.SpacingBefore = spacebefore;
                    contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Channel ID")));
                    contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Channel Name")));
                    contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Characteristic ID")));
                    contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("No. Of Digit After Decimal Place")));

                    for (int i = 0; i < list.Count; i++)
                    {
                        contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].ChannelID)));
                        contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].ChannelName)));
                        contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].CharactristicID)));
                        contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].NoOfDigitAfterDecimal)));
                    }
                }

                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=MachineInspectionData.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }
        #endregion

        #region ------Energy Master -------------
        public static string generateEnergyMachineMasterDataPDF(List<ColumnList> columnList, string company, out string msgToShow)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                if (HttpContext.Current.Session["EnergyMachineData"] == null)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }
                List<EnergyMasterDetails> list = HttpContext.Current.Session["EnergyMachineData"] as List<EnergyMasterDetails>;
                if (list.Count <= 0)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("Energy Meter Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                //selected input
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldText("")) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingTop = 10, Border = 0 });

                //Contrent
                var visibleCoumns = columnList.Where(x => x.FieldVisibility == true).ToList();

                PdfPTable contentTbl = new PdfPTable(visibleCoumns.Count);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 10;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Meter ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("IOT ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Is Enabled?")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Sort Order")));

                //for (int i = 0; i < list.Count; i++)
                //{
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].InterfaceID)));
                //    addCheckBox(contentTbl, list[i].IsEnabled);
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].SortOrder)));
                //}

                for (int i = 0; i < visibleCoumns.Count; i++)
                {
                    if (visibleCoumns[i].FieldVisibility)
                    {
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(visibleCoumns[i].FieldDisplayName)));
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < visibleCoumns.Count; j++)
                    {
                        if (visibleCoumns[j].FieldVisibility)
                        {
                            switch (visibleCoumns[j].FieldName)
                            {
                                case "MachineID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineID)));
                                    break;
                                case "InterfaceID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].InterfaceID)));
                                    break;
                                case "IsEnabled":
                                    addCheckBox(contentTbl, list[i].IsEnabled);
                                    break;
                                case "SortOrder":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].SortOrder)));
                                    break;
                            }
                        }
                    }
                }

                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=EnergyMeterData.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }

        public static string generateEnergyNodeMasterDataPDF(List<ColumnList> columnList, string company, out string msgToShow)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                if (HttpContext.Current.Session["EnergyNodeData"] == null)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }
                List<EnergyMasterDetails> list = HttpContext.Current.Session["EnergyNodeData"] as List<EnergyMasterDetails>;
                if (list.Count <= 0)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("Energy Node Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                //selected input
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldText("")) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingTop = 10, Border = 0 });

                //Contrent
                var visibleCoumns = columnList.Where(x => x.FieldVisibility == true).ToList();

                PdfPTable contentTbl = new PdfPTable(visibleCoumns.Count);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 10;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Machine ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Node ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("EM Node ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("IOT ID")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Sub System")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("EM Model No.")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("IP Address")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Is Enabled?")));
                //contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Sort Order")));

                //for (int i = 0; i < list.Count; i++)
                //{
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].NodeID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].EMNodeID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].InterfaceID)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].SubSystem)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].EMModelNo)));
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].IPAddress)));
                //    addCheckBox(contentTbl, list[i].IsEnabled);
                //    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].SortOrder)));
                //}

                for (int i = 0; i < visibleCoumns.Count; i++)
                {
                    if (visibleCoumns[i].FieldVisibility)
                    {
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(visibleCoumns[i].FieldDisplayName)));
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < visibleCoumns.Count; j++)
                    {
                        if (visibleCoumns[j].FieldVisibility)
                        {
                            switch (visibleCoumns[j].FieldName)
                            {
                                case "MachineID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineID)));
                                    break;
                                case "NodeID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].NodeID)));
                                    break;
                                case "InterfaceID":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].InterfaceID)));
                                    break;
                                case "SubSystem":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].SubSystem)));
                                    break;
                                case "EMModelNo":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].EMModelNo)));
                                    break;
                                case "IPAddress":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].IPAddress)));
                                    break;
                                case "IsEnabled":
                                    addCheckBox(contentTbl, list[i].IsEnabled);
                                    break;
                                case "SortOrder":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].SortOrder)));
                                    break;
                            }
                        }
                    }
                }

                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=EnergyNodeData.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }
        #endregion

        #region -----Loss Code & Rejection Analysis------
        public static string generateLossAndRejectionDataPDF(string[] data, int columnCount, string name, string selectedInput, out string msgToShow)
        {
            string successMsg = "";
            msgToShow = "";
            try

            {
                if (columnCount <= 0)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader(name)) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldText(selectedInput)) { Colspan = 1, Border = 0 });

                //Contrent
                PdfPTable contentTbl = new PdfPTable(columnCount);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 10;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //  contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Meter ID")));

                bool eneterContent = false;
                for (int i = 0; i < data.Length; i++)
                {
                    if (data[i] == ";;")
                    {
                        eneterContent = true;
                        continue;
                    }
                    if (eneterContent)
                    {
                        contentTbl.AddCell(new PdfPCell(getPdfCellContentText(data[i])));
                    }
                    else
                    {
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(data[i])));
                    }

                }

                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + name.Replace(" ", "") + ".pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }

        public static string generateLossAndRejectionChartPDF(string categoryimage, string subcategoryimage, string downtimeimage, string name, bool isCategoryChecked, bool isSubCategoryChecked, bool isDowntimeChecked, string selectedMenu, string selectedInput, string page, out string msgToShow)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                string categoryName = "", subcategoryName = "", downtimeName = "";
                if (page == "DownTime")
                {
                    if (selectedMenu == "Time")
                    {
                        categoryName = "Loss-Category Wise Chart";
                        subcategoryName = "Loss-Subcategory Wise Chart";
                        downtimeName = "Stoppage Reason Wise Chart";
                    }
                    else
                    {
                        categoryName = "Frequency-Category Wise Chart";
                        subcategoryName = "Frequency-Subcategory Wise Chart";
                        downtimeName = "Frequency-Stoppage Reason Wise Chart";
                    }
                }
                else
                {
                    categoryName = "Frequency-Category Wise Chart";
                    subcategoryName = "Frequency-Subcategory Wise Chart";
                    downtimeName = "Frequency-Rejection Code Wise Chart";
                }

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 20, 20);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                //  mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader(name)) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                //PdfPTable headertable = new PdfPTable(3);
                //headertable.AddCell(new PdfPCell(getPdfCellWithBoldHeader(company)));
                //headertable.AddCell(new PdfPCell(getPdfCellWithBoldHeader(plant)));
                //headertable.AddCell(new PdfPCell(getPdfCellWithBoldHeader(others)));
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldText(selectedInput)) { Border = 0 });


                //Contrent
                PdfPTable contentTbl = new PdfPTable(1);
                contentTbl.WidthPercentage = 100;
                //contentTbl.SpacingBefore = 20;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;

                if (isCategoryChecked)
                {
                    contentTbl.AddCell(new PdfPCell(getPdfCellWithBoldHeader(categoryName)) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0, PaddingTop = 10, PaddingBottom = 10 });
                    contentTbl.AddCell(getChartImage(categoryimage));


                    //DataTable dt = new DataTable();
                    //dt = (DataTable)HttpContext.Current.Session["CategoryTableData"];
                    //contentTbl.AddCell(new PdfPCell(getTableDataForDownAnalisys(dt)) { Padding=10 }) ;

                }

                if (isSubCategoryChecked)
                {
                    contentTbl.AddCell(new PdfPCell(getPdfCellWithBoldHeader(subcategoryName)) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0, PaddingTop = 10, PaddingBottom = 10 });
                    contentTbl.AddCell(getChartImage(subcategoryimage));

                    //DataTable dt = new DataTable();
                    //dt = (DataTable)HttpContext.Current.Session["SubCategoryTableData"];
                    //contentTbl.AddCell(new PdfPCell(getTableDataForDownAnalisys(dt)) { Padding = 10 });
                }

                if (isDowntimeChecked)
                {
                    contentTbl.AddCell(new PdfPCell(getPdfCellWithBoldHeader(downtimeName)) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0, PaddingTop = 10, PaddingBottom = 10 });
                    contentTbl.AddCell(getChartImage(downtimeimage));

                    //DataTable dt = new DataTable();
                    //dt = (DataTable)HttpContext.Current.Session["DownTimeTableData"];
                    //contentTbl.AddCell(new PdfPCell(getTableDataForDownAnalisys(dt)) { Padding = 10 });
                }

                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + name.Replace(" ", "") + ".pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }

        private static PdfPTable getTableDataForDownAnalisys(DataTable dt)
        {

            List<string> cols = dt.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToList();
            cols.Remove("Companyid"); cols.Remove("DownTime");
            PdfPTable pdfPTable = new PdfPTable(cols.Count);

            try
            {

                for (int i = 0; i < cols.Count; i++)
                {
                    pdfPTable.AddCell(new PdfPCell(getPdfCellTableHeaderText(cols[i])));
                }
                foreach (DataRow rowItem in dt.Rows)
                {
                    for (int j = 0; j < cols.Count; j++)
                    {
                        if (cols[j] == "DownTimeInSS")
                        {
                            string valueinSec = rowItem[cols[j]].ToString();
                            decimal valueinDecimal = 0;
                            if (valueinSec != "" || valueinSec != null)
                            {
                                valueinDecimal = Convert.ToDecimal(valueinSec);
                            }
                            string valueinHHmm = convertSecondsToDatetimeFormat(valueinDecimal, "hh:mm").Replace(":", ".");
                            pdfPTable.AddCell(new PdfPCell(getPdfCellContentText(valueinHHmm)));
                        }
                        else
                        {
                            pdfPTable.AddCell(new PdfPCell(getPdfCellContentText(rowItem[cols[j]].ToString())));
                        }

                    }
                }

            }
            catch (Exception ex)
            {

            }
            return pdfPTable;
        }
        private static string convertSecondsToDatetimeFormat(decimal totalcolumnValue, string selectedDisplayTime)
        {
            string totalValue = "";
            try
            {
                Int64 mySeconds = System.Convert.ToInt64(totalcolumnValue);

                Int64 myHours = mySeconds / 3600; //3600 Seconds in 1 hour
                mySeconds %= 3600;

                Int64 myMinutes = mySeconds / 60; //60 Seconds in a minute
                mySeconds %= 60;

                string mySec = mySeconds.ToString(), myMin = myMinutes.ToString(), myHou = myHours.ToString();

                if (myHours < 10) { myHou = myHou.Insert(0, "0"); }

                if (myMinutes < 10) { myMin = myMin.Insert(0, "0"); }

                if (mySeconds < 10) { mySec = mySec.Insert(0, "0"); }


                if (selectedDisplayTime == "hh:mm:ss")
                {

                    totalValue = myHou + ":" + myMin + ":" + mySec;
                }
                else if (selectedDisplayTime == "hh:mm")
                {

                    totalValue = myHou + ":" + myMin;
                }
                else if (selectedDisplayTime == "mm:ss")
                {
                    TimeSpan t = TimeSpan.FromSeconds(Convert.ToDouble(totalcolumnValue));
                    //totalValue = t.ToString(@"mm\:ss");
                    totalValue = string.Format("{0}:{1}", (int)t.TotalMinutes, t.Seconds);
                }
                else
                {
                    totalValue = totalcolumnValue.ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return totalValue;
        }
        private static PdfPCell getChartImage(string value)
        {
            byte[] bytesData = Convert.FromBase64String(value);
            iTextSharp.text.Image imggraph = iTextSharp.text.Image.GetInstance(bytesData);
            //imggraph.SetAbsolutePosition(0, 0);
            //imggraph.ScaleAbsoluteHeight(470);
            //imggraph.ScaleAbsoluteWidth(800);
            PdfPCell chartcell = new PdfPCell(imggraph, true);
            chartcell.PaddingTop = 10;
            chartcell.PaddingBottom = 10;
            chartcell.PaddingLeft = 40;
            chartcell.PaddingRight = 40;
            return chartcell;
        }
        #endregion


        #region -----OEE Trend-----

        public static string generateOEETrendPDF(string oeeimagevalue, string peimagevalue, string qeimagevalue, string aeimagevalue, string selectedparam, string selectedinput, DataTable dt, out string msgToShow)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                //string categoryName = "", subcategoryName = "", downtimeName = "";

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 20, 20);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("OEE Trend Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                PdfPTable headertable = new PdfPTable(3);
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldText(selectedinput)) { Border = 0 });


                //Contrent
                PdfPTable contentTbl = new PdfPTable(1);
                contentTbl.WidthPercentage = 100;
                //contentTbl.SpacingBefore = 20;
                contentTbl.SplitLate = false;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;

                int totalcount = 0;
                if (selectedparam.Contains("Oee"))
                {
                    contentTbl.AddCell(new PdfPCell(getPdfCellWithBoldHeader("OEE")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0, PaddingTop = 10, PaddingBottom = 10 });
                    contentTbl.AddCell(getChartImage(oeeimagevalue));
                    totalcount++;
                }
                if (selectedparam.Contains("Pe"))
                {
                    contentTbl.AddCell(new PdfPCell(getPdfCellWithBoldHeader("PE")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0, PaddingTop = 10, PaddingBottom = 10 });
                    contentTbl.AddCell(getChartImage(peimagevalue));
                    totalcount++;
                }
                if (selectedparam.Contains("Ae"))
                {
                    contentTbl.AddCell(new PdfPCell(getPdfCellWithBoldHeader("AE")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0, PaddingTop = 10, PaddingBottom = 10 });
                    contentTbl.AddCell(getChartImage(aeimagevalue));
                    totalcount++;
                }
                if (selectedparam.Contains("Qe"))
                {
                    contentTbl.AddCell(new PdfPCell(getPdfCellWithBoldHeader("QE")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0, PaddingTop = 10, PaddingBottom = 10 });
                    contentTbl.AddCell(getChartImage(qeimagevalue));
                    totalcount++;
                }

                List<string> cols = dt.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToList();
                PdfPTable pdfPTable = new PdfPTable((cols.Count - 4) + totalcount);

                for (int i = 0; i < cols.Count; i++)
                {
                    if (cols[i] == "Oee" || cols[i] == "Pe" || cols[i] == "Qe" || cols[i] == "Ae")
                    {
                        if (selectedparam.Contains(cols[i]))
                        {
                            pdfPTable.AddCell(new PdfPCell(getPdfCellTableHeaderText(cols[i])));
                        }
                    }
                    else
                    {
                        pdfPTable.AddCell(new PdfPCell(getPdfCellTableHeaderText(cols[i])));
                    }


                }
                foreach (DataRow rowItem in dt.Rows)
                {
                    for (int j = 0; j < cols.Count; j++)
                    {
                        if (cols[j] == "Oee" || cols[j] == "Pe" || cols[j] == "Qe" || cols[j] == "Ae")
                        {
                            if (selectedparam.Contains(cols[j]))
                            {
                                pdfPTable.AddCell(new PdfPCell(getPdfCellContentText(rowItem[cols[j]].ToString())));
                            }
                        }
                        else
                        {
                            pdfPTable.AddCell(new PdfPCell(getPdfCellContentText(rowItem[cols[j]].ToString())));
                        }

                    }
                }
                contentTbl.AddCell(new PdfPCell(pdfPTable) { Padding = 20 });

                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=OEETrend.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }

        public static string generateOEETrendYMDPDF(string yearimagevalue, string monthimagevalue, string dayorweekimageValue, string selectedinput, string selectedparam, bool isYMDChecked, bool idYMWChecked, DataTable yeardt, DataTable monthdt, DataTable daydt, DataTable weekdt, out string msgToShow)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                //string categoryName = "", subcategoryName = "", downtimeName = "";

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 20, 20);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("OEE Trend Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                PdfPTable headertable = new PdfPTable(3);
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldText(selectedinput)) { Border = 0 });


                //Contrent
                PdfPTable contentTbl = new PdfPTable(1);
                contentTbl.WidthPercentage = 100;
                //contentTbl.SpacingBefore = 20;
                contentTbl.SplitLate = false;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;



                contentTbl.AddCell(new PdfPCell(getPdfCellWithBoldHeader("Year Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0, PaddingTop = 10, PaddingBottom = 10 });
                contentTbl.AddCell(getChartImage(yearimagevalue));
                contentTbl.AddCell(new PdfPCell(getTableDataForOEETrend(yeardt, selectedparam)) { Padding = 20 });

                contentTbl.AddCell(new PdfPCell(getPdfCellWithBoldHeader("Month Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0, PaddingTop = 10, PaddingBottom = 10 });
                contentTbl.AddCell(getChartImage(monthimagevalue));
                contentTbl.AddCell(new PdfPCell(getTableDataForOEETrend(monthdt, selectedparam)) { Padding = 20 });

                if (isYMDChecked)
                {
                    contentTbl.AddCell(new PdfPCell(getPdfCellWithBoldHeader("Day Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0, PaddingTop = 10, PaddingBottom = 10 });
                    contentTbl.AddCell(getChartImage(dayorweekimageValue));
                    contentTbl.AddCell(new PdfPCell(getTableDataForOEETrend(daydt, selectedparam)) { Padding = 20 });
                }
                else
                {
                    contentTbl.AddCell(new PdfPCell(getPdfCellWithBoldHeader("Week Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0, PaddingTop = 10, PaddingBottom = 10 });
                    contentTbl.AddCell(getChartImage(dayorweekimageValue));
                    contentTbl.AddCell(new PdfPCell(getTableDataForOEETrend(weekdt, selectedparam)) { Padding = 20 });
                }

                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=OEETrend.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }

        private static PdfPTable getTableDataForOEETrend(DataTable dt, string selectedparam)
        {
            List<string> cols = dt.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToList();
            PdfPTable pdfPTable = new PdfPTable(cols.Count);
            try
            {


                for (int i = 0; i < cols.Count; i++)
                {
                    if (cols[i] == "Oee" || cols[i] == "Pe" || cols[i] == "Qe" || cols[i] == "Ae")
                    {
                        if (selectedparam.Contains(cols[i]))
                        {
                            pdfPTable.AddCell(new PdfPCell(getPdfCellTableHeaderText(cols[i])));
                        }
                    }
                    else
                    {
                        pdfPTable.AddCell(new PdfPCell(getPdfCellTableHeaderText(cols[i])));
                    }
                }
                foreach (DataRow rowItem in dt.Rows)
                {
                    for (int j = 0; j < cols.Count; j++)
                    {
                        if (cols[j] == "Oee" || cols[j] == "Pe" || cols[j] == "Qe" || cols[j] == "Ae")
                        {
                            if (selectedparam.Contains(cols[j]))
                            {
                                pdfPTable.AddCell(new PdfPCell(getPdfCellContentText(rowItem[cols[j]].ToString())));
                            }
                        }
                        else
                        {
                            pdfPTable.AddCell(new PdfPCell(getPdfCellContentText(rowItem[cols[j]].ToString())));
                        }

                    }
                }

            }
            catch (Exception ex)
            {

            }
            return pdfPTable;
        }
        #endregion

        #region ------Predictive Maint Information Masters-------------
        public static string generatePredictiveMaintMasterPDF(List<ColumnList> columnList, string mtbID, out string msgToShow)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                if (HttpContext.Current.Session["PredictiveMaintMasterData"] == null)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }
                List<PredictiveMaintData> list = HttpContext.Current.Session["PredictiveMaintMasterData"] as List<PredictiveMaintData>;
                if (list.Count <= 0)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("Predtive Maintenance Details")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });


                //selected input
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldText("MTB: " + mtbID)) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingTop = 10, Border = 0 });

                //Contrent
                var visibleCoumns = columnList.Where(x => x.FieldVisibility == true).ToList();

                PdfPTable contentTbl = new PdfPTable(visibleCoumns.Count);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 10;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                for (int i = 0; i < visibleCoumns.Count; i++)
                {
                    if (visibleCoumns[i].FieldVisibility)
                    {
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(visibleCoumns[i].FieldDisplayName)));
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < visibleCoumns.Count; j++)
                    {
                        if (visibleCoumns[j].FieldVisibility)
                        {
                            switch (visibleCoumns[j].FieldName)
                            {
                                case "AlarmNumber":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].AlarmNo)));
                                    break;
                                case "AlarmDesc":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].AlarmDesc)));
                                    break;
                                case "DurationType":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].DurationType)));
                                    break;
                                case "DurationIn":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].DurationIn)));
                                    break;
                                case "PMCLocation":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].TargetDLocation))));
                                    break;
                                case "PMCCurrentLocation":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(getDateTimeFormate(list[i].CurrentDLocation))));
                                    break;
                                case "IsEnabled":
                                    addCheckBox(contentTbl, list[i].IsEnabled);
                                    break;
                            }
                        }
                    }
                }

                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=PredictiveMaintenanceData.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }
        #endregion

        #region ------Inspection Default Masters-------------
        public static string generateInspectionDefaultMasterPDF(List<ColumnList> columnList, out string msgToShow)
        {
            string successMsg = "";
            msgToShow = "";
            try
            {
                if (HttpContext.Current.Session["InspectionMasterData"] == null)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }
                List<InspectionDefaultEntity> list = HttpContext.Current.Session["InspectionMasterData"] as List<InspectionDefaultEntity>;
                if (list.Count <= 0)
                {
                    successMsg = NoDataFoundMSG;
                    msgToShow = NoDataFoundMSGToShow;
                    return successMsg;
                }

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                PdfPTable mainTable = new PdfPTable(1);
                mainTable.SplitLate = false;
                mainTable.WidthPercentage = 100;
                mainTable.SpacingBefore = 20;
                mainTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


                //header
                mainTable.AddCell(new PdfPCell(getPdfCellWithBoldHeader("Inspection Default")) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 1, Border = 0 });

                //filter
                string company = "";
                if (list.Count > 0)
                {
                    company = list[0].Company;
                }
                PdfPTable contentTbl = new PdfPTable(1);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 20;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                contentTbl.AddCell(new PdfPCell(getPdfCellWithBoldText("")));
                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                //Contrent
                var visibleCoumns = columnList.Where(x => x.FieldVisibility == true).ToList();

                contentTbl = new PdfPTable(visibleCoumns.Count + 1);
                contentTbl.WidthPercentage = 100;
                contentTbl.SpacingBefore = 20;
                contentTbl.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                contentTbl.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;

                contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText("Machine ID")));
                for (int i = 0; i < visibleCoumns.Count; i++)
                {
                    if (visibleCoumns[i].FieldVisibility)
                    {
                        contentTbl.AddCell(new PdfPCell(getPdfCellTableHeaderText(visibleCoumns[i].FieldDisplayName)));
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].MachineID)));
                    for (int j = 0; j < visibleCoumns.Count; j++)
                    {
                        if (visibleCoumns[j].FieldVisibility)
                        {
                            switch (visibleCoumns[j].FieldName)
                            {
                                case "Component":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Component)));
                                    break;
                                case "Operation":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Operation)));
                                    break;
                                case "Operator":
                                    contentTbl.AddCell(new PdfPCell(getPdfCellContentText(list[i].Operator)));
                                    break;
                            }
                        }
                    }
                }

                mainTable.AddCell(new PdfPCell(contentTbl) { Colspan = 1, Border = 0 });

                pdfDoc.Add(mainTable);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=InspectionDefault.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                //Response.End();
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
                msgToShow = ErrorMSGToShow;
                successMsg = ErrorMSG;
            }
            return successMsg;
        }
        #endregion
    }
}