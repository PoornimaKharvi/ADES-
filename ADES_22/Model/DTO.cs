using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ADES_22.Model
{
    public class DTO
    {

    }

    #region ----- Employee -----

    public class EmpDetails
    {
        public string EmpID { get; set; }
        public string EmpName { get; set; }
        public string Dept { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ReportingTo { get; set; }
        public string Region { get; set; }
        public string Param { get; set; }
    }

    #endregion

    #region ----- Customer -----

    public class Customer
    {
        public int ID { get;set; }
        public string CName { get; set; }
        public string CDescription { get; set; }
        public string CRegion { get; set; }
    }

    #endregion

    #region ----- Import MJ Data -----

    public class Pono
    {
        public string PoNo { get; set; }
    }

    public class filtervalues
    {
        public string PoNumber { get; set; }
        public string PoDate { get; set; }
        public string Product { get; set; }
        public string MJnumber { get; set; }
    }

    public class dataList
    {
        public string mjnum { get; set; }
        public string prodname { get; set; }
        public string podate { get; set; }
    }

    #endregion

    #region ----- Defect Tracker -----

    public class DefectTracker
    {
        public string PID { get; set; }
        public string Module { get; set; }
        public string PName { get; set; }
        public string CName { get; set; }
        public string Email { get; set; }
        public string Users { get; set; }
        public string IssueID { get; set; }
        public string IssueName { get; set; }
        public string IssueType { get; set; }
        public string IssueTypeDisplayName { get; set; }
        public string IssueDesc { get; set; }
        public string Priority { get; set; }
        public string Assignee { get; set; }
        public string Status { get; set; }
        public string Changes { get; set; }
        public string Steps { get; set; }
        public DateTime ReportedDate { get; set; }
        public string ReporterType { get; set; }
        public string ReportedBy { get; set; }
        public string Environment { get; set; }
        public byte[] Document { get; set; }
        public string DocumentName { get; set; }
        public string FileType { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedTS { get; set; }
        public string OldAssignee { get; set; }
        public string NewAssignee { get; set; }
        public string OldStatus { get; set; }
        public string NewStatus { get; set; }
        public string FilterSearch { get; set; }
        public string Param { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string EstimatedEffort { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Date { get; set; }
        public string ProjectCreatedBy { get; set; }
        public string ProjectStatus { get; set; }
        public string ProjectOwner { get; set; }
    }

    public class Files
    {
        public int IDD { get; set; }
        public string FName { get; set; }
        public string FileType { get; set; }
        public string ProjID { get; set; }
        public string Param { get; set; }
    }

    public class BacklogFiles
    {
        public string PID { get; set; }
        public string Module { get; set; }
        public string CName { get; set; }
        public string IssueID { get; set; }
        public string IssueName { get; set; }
        public string IssueType { get; set; }
        public string ReportedBy { get; set; }
        public string ReporterType { get; set; }
        public int IDD { get; set; }
        public string FName { get; set; }
        public string Param { get; set; }
    }

    public class History
    {
        public string Msg { get; set; }
        public string RDate { get; set; }
        public string ReportedBy { get; set; }
    }

    #endregion

    #region ----- Service -----

    public class WService
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string MsgBody { get; set; }
    }

    #endregion

    // filter values for ProposalEntry
    public class DropDownValues
    {
        public string Region { get; set; }
        public string Customer { get; set; }
        public string Owner { get; set; }
        public string Status { get; set; }
        public string KeyField { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }

    }

    //Purchase order details
    public class purchaseOrder
    {
        public string POnumber { get; set; }
        public string POdate { get; set; }
        public string POvalue { get; set; }
        public string Region { get; set; }
        public string Customer { get; set; }
        public string QuoteRef { get; set; }
        public string Status { get; set; }
        public string StatusAsOn { get; set; }
        public string Attachment { get; set; }
        public string tallyPOnumber { get; set; }
        public string tallyPOdate { get; set; }
        public string FileUpload { get; set; }
        public byte[] attachedFile { get; set; }
        public string attachmentfile { get; set; }
        public string AttachmentFileBase64 { get; set; }
    }

    //ProposalEntry details
    public class proposalEntryDetails
    {
        public string Region { get; set; }
        public string Customer { get; set; }
        public string ProposalNumber { get; set; }
        public string ProposalVersion { get; set; }
        public string ProposalDate { get; set; }
        public string ProposalOwner { get; set; }
        public string ProposalValue { get; set; }
        public string SubmittedDate { get; set; }
        public string Status { get; set; }
        public string StatusAsOn { get; set; }
        public string KeyField { get; set; }
        public string FileUpload { get; set; }
        public string FIlePath { get; set; }
        public byte[] attachedFile { get; set; }
        public string attachmentfile { get; set; }
        public string AttachmentFileBase64 { get; set; }
    }


    #region"---Import Invoice----"
    public class filterValues1
    {
        public string PoNumber { get; set; }
        public string PoDate { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDt { get; set; }
        public string custname { get; set; }
    }
    public class InvoiceDetails1
    {
        public string productname { get; set; }
        public string quantity { get; set; }
        public string unit { get; set; }
        public string mjNumber { get; set; }
        public string FileUpload { get; set; }
        public byte[] attachedFile { get; set; }
        public string attachmentfile { get; set; }
        public string AttachmentFileBase64 { get; set; }
        public string InvoiceValue { get; set; }
    }
    public class dataListInvoice
    {
        public string podate { get; set; }
        public string invoicedate { get; set; }
        public string custname { get; set; }
        public string invno { get; set; }
        public string pronum { get; set; }
    }
    public class podetailinvoice
    {
        public string PoDate { get; set; }
        public string custname { get; set; }
        public string invdate { get; set; }
    }
    public class Invoicetable
    {
        public string customer { get; set; }
        public string invoiceno { get; set; }
        public string invoicedate { get; set; }
        public string pono { get; set; }
        public string podate { get; set; }
        public string production { get; set; }
        public string item { get; set; }
        public string sno { get; set; }
        public string qty { get; set; }
        public string unit { get; set; }
        public string batch { get; set; }
        public string proddesc { get; set; }
        public string mjno { get; set; }
        public string supcode { get; set; }
        public string FileUpload { get; set; }
        public byte[] attachedFile { get; set; }
        public string attachmentfile { get; set; }
        public string AttachmentFileBase64 { get; set; }
    }
    public class InvNo
    {
        public string Invoice { get; set; }
    }
    #endregion

    #region "-----Packing List---"
    #region"----Dispatch Details----"
    public class dispatchDetails
    {
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceValue { get; set; }
        public string PoNumber { get; set; }
        public string PoDate { get; set; }
        public string Customer { get; set; }
        public string Region { get; set; }
        public string ProdName { get; set; }
        public string ConsignName { get; set; }
        public string CourierName { get; set; }
        public string Content { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string DeliveryDate { get; set; }
        public string ReceivedBy { get; set; }
        public string FileUpload { get; set; }
        public byte[] attachedFile { get; set; }
        public string attachmentfile { get; set; }
    }

    //Filter values for DispatchDetails
    public class filterDisDetails
    {
        public string PoNo { get; set; }
        public string InvoiceNo { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
    public class PurchaseOrder
    {
        public string POnumber { get; set; }
        public string POdate { get; set; }
        public string POvalue { get; set; }
        public string Region { get; set; }
        public string Customer { get; set; }
        public string QuoteRef { get; set; }
        public string Status { get; set; }
        public string StatusAsOn { get; set; }
        public string Attachment { get; set; }
        public string tallyPOnumber { get; set; }
        public string tallyPOdate { get; set; }
        public string FileUpload { get; set; }
        public byte[] attachedFile { get; set; }
        public string attachmentfile { get; set; }
        public string AttachmentFileBase64 { get; set; }
    }
    public class DispatchDetails1
    {
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceValue { get; set; }
        public string PoNumber { get; set; }
        public string PODate { get; set; }
        public string Customer { get; set; }
        public string Region { get; set; }
        public string ProdName { get; set; }
        public string ConsignName { get; set; }
        public string CourierName { get; set; }
        public string Content { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string DeliveryDate { get; set; }
        public string RecievedBy { get; set; }
        public string FileUpload { get; set; }
        public byte[] AttachedFile { get; set; }
        public string AttachmentFile { get; set; }
        public string AttachmentFileBase64 { get; set; }
    }
    public class DispatchDetails2
    {
        public string InvoiceNumber { get; set; }
        public string PoNumber { get; set; }
    }
    public class FilterDisDetails
    {
        public string PoNo { get; set; }
        public string InvoiceNo { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
    public class FilterInvoice
    {
        public string PoNumber { get; set; }
        public string PoDate { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDt { get; set; }
        public string custname { get; set; }
    }
    public class InvoiceDetails
    {
        public string ProductName { get; set; }
        public string Quantity { get; set; }
        public string Unit { get; set; }
        public string MjNumber { get; set; }
        public string FileUpload { get; set; }
        public byte[] AttachedFile { get; set; }
        public string AttachmentFile { get; set; }
        public string InvoiceValue { get; set; }
    }
    public class DataListInvoice
    {
        public string PoDate { get; set; }
        public string InvoiceDate { get; set; }
        public string CustnName { get; set; }
        public string InvNo { get; set; }
        public string ProNum { get; set; }
    }
    public class ProposalEntryDetails
    {
        public string Region { get; set; }
        public string Customer { get; set; }
        public string ProposalNumber { get; set; }
        public string ProposalVersion { get; set; }
        public string ProposalDate { get; set; }
        public string ProposalOwner { get; set; }
        public string ProposalValue { get; set; }
        public string SubmittedDate { get; set; }
        public string Status { get; set; }
        public string StatusAsOn { get; set; }
        public string KeyField { get; set; }
        public string FileUpload { get; set; }
        public string FIlePath { get; set; }
        public byte[] attachedFile { get; set; }
        public string attachmentfile { get; set; }
        public string AttachmentFileBase64 { get; internal set; }
    }
    public class purchaseOrderDetails
    {
        public string SlNo { get; set; }
        public string itemName { get; set; }
        public string goDown { get; set; }
        public string quantity { get; set; }
        public string unit { get; set; }
    }
    public class podetail
    {
        public string PoDate { get; set; }
        public string Product { get; set; }
        public string MJno { get; set; }
    }
    #endregion

    #region "-----Kit Master Details---"
    public class kitMaster
    {
        public string kitNo { get; set; }
        public string kitName { get; set; }
        public kitMaster(string kitNo, string kitName)
        {
            this.kitNo = kitNo;
            this.kitName = kitName;
        }
    }
    public class KitMaster1
    {
        public string KitNo { get; set; }
        public string KitName { get; set; }
    }
    #endregion

    #region "----Item Master Details---"
    public class ItemMaster
    {
        public string ItemNo { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public string supplierName { get; set; }
        public string partNo { get; set; }
        public bool IsAccessories { get; set; }
    }
    #endregion

    #region "---BOM Details----"
    public class BOMDetails
    {
        public string kitno { get; set; }
        public string kitname { get; set; }
        public string itemNo { get; set; }
        public string itemName { get; set; }
        public string ItemDescription { get; set; }
        public string supplierName { get; set; }
        public string partNo { get; set; }
        public string quantity { get; set; }
        public bool isAccessories { get; set; }
        public string IsSelected { get; set; }
        public bool SelectedCheck { get; set; }
        public string param { get; set; }
    }
    #endregion

    #region"----POAssociate-----"

    public class POKitAccess
    {
        public string cust { get; set; }
        public string pono { get; set; }
        public string Kitname { get; set; }
        public string Kitno { get; set; }
        public string KQty { get; set; }
        public string IsSelected { get; set; }
        public bool Selectedkit { get; set; }
        public string param { get; set; }
        public bool IsAccessories { get; set; }
    }
    public class POItemAccess
    {
        public string param { set; get; }
        public string Cust { get; set; }
        public string Pono { get; set; }
        public string Kitname { get; set; }
        public string Itemname { get; set; }
        public string ItemDesc { get; set; }
        public string KitNo { get; set; }
        public string ItemNo { get; set; }
        public string KitQty { get; set; }
        public bool IsAccessories { get; set; }
        public string SName { get; set; }
        public string PartNo { get; set; }
        public string ItemQty { get; set; }
        public string Qty { get; set; }
        public string shortage { get; set; }
        public string Status { get; set; }
        public string ApprovedBy { get; set; }
        public string ApproveRequestBy { get; set; }
        public string ApproveRequestTS { get; set; }
        public string ApprovedTS { get; set; }

        public string prevKitName { get; set; }
        public int  id { get; set; }
        public int PresentRowSpan { get; set; }
        public int PreviousRowSpan { get; set; } 
        public bool KitVisiblility { get; set; } = true;
        public bool HeaderVisibility1 { get; set; } = false;
        public bool HeaderVisibility2 { get; set; } = false;
        public bool AccessoriesHeader { get; set; } = false;


        //public bool HeaderVisibility3 { get; set; } = false;
        //public bool HeaderVisibility4 { get; set; } = false;

    }
    public class SendMailMessage
    {
        public string from { get; set; }
        public string to { get; set; }
        public string subeject { get; set; }
        public string body { get; set; }
        public bool isbodyhtml { get; set; }

    }
    public class BOMPrint1
    {
        public int ID { get; set; }
        public string KitName { get; set; }
        public string PartName { get; set; }
        public string PartNo { get; set; }
        public string Description { get; set; }
        public string Quantity { get; set; }
        public string remarks { get; set; }
        public bool HeaderVisibility { get; set; } = false;
    }


    #endregion


    #endregion

    public class Taskdetails
    {
        public string Projectid { get; set; }
        public string Projectname { get; set; }
        public string Weekno { get; set; }
        public string Year { get; set; }
        public string Tasktype { get; set; }
        public string Maintask { get; set; }
        public string Estimatedeffort { get; set; }
        public string Request { get; set; }
        public string Assignedto { get; set; }
        public string Param { get; set; }
        public string Id { get; set; }
        public string RequestName { get; set; }
        public string TaskTypeName { get; set; }
        public string ProblemID { get; set; }
        public string MaintaskStatus { get; set; }
        public string DeliveryDate { get; set; }
    }
    public class Subtaskdetails
    {
        public string Projectid { get; set; }
        public string Weekno { get; set; }
        public string Weekno1 { get; set; }
        public string Year { get; set; }
        public string Team { get; set; }
        public string Customer { get; set; }
        public string Tasktype { get; set; }
        public string TasktypeName { get; set; }
        public string Maintask { get; set; }
        //public string Estimatedeffort { get; set; }
        public string Request { get; set; }
        public string RequestName { get; set; }
        public string Subtask { get; set; }
        public string Assignedto { get; set; }
        public string Estimatedeffortsub { get; set; }
        public string Id { get; set; }
        public string Param { get; set; }
        public string MainTaskEstimatedeffort { get; set; }
        public string MainTaskIDD { get; set; }
        public string ProblemID { get; set; }        
        public string SubtaskStatus { get; set; }
        public string MaintaskStatus { get; set; }
        public string ProjectStatus { get; set; }
        public string DeliveryDate { get; set; }
        public string ManualEntryRemark { get; set; }
        public string Dependencies { get; set; }
        public bool HeaderVisibility { get; set; } = false;
        public bool Visibility { get; set; } = false;
        public bool mainVisibility { get; set; } = false;
        public bool projectVisibility { get; set; } = false;
        public string  Employees { get; set; }
        public string RemarksEngineer { get; set; }
        public string ViewMOM { get; set; }
        public List<string> EmpList { get; set; } = new List<string>();
        public List<string> TaskTypelist { get; set; } = new List<string>();
        public List<string> Dependencylist { get; set; } = new List<string>();
        public List<string> Requestlist { get; set; } = new List<string>();
        public List<FileDetails> fileDetails { get; set; } = new List<FileDetails>();
    }
    public class WeeklyTaskReport
    {
        public string EmployeeID { get; set; }
        public string Weekno { get; set; }
        public string Year { get; set; }
        public string TeamSize { get; set; }
        public string AvailableHours { get; set; }
        public string PlannedHours { get; set; }
        public string PToA { get; set; }
        public string PlannedTask { get; set; }
        public string  TaskTakenPerPlan { get; set; }
        public string AdherenceToPlan { get; set; }
        public string  UtilizedHours { get; set; }
        public string  UToP { get; set; }
        public string TaskNotPlannedButTakenUp { get; set; }
        public bool HeaderVisibility { get; set; } = false;
        public string Planner { get; set; }
        public string SkippedTask { get; set; }
        public string TaskStatus { get; set; }
        public string WeekNumberText { get; set; }
        public bool SkippedTaskLabel { get; set; } = false;
        public bool SkippedTaskTextBox { get; set; } = true;
        public string Priority { get; set; }
        public int ProductionSupport { get; set; }
        public string  Dependencies { get; set; }
        public string  MajorTask { get; set; }
        public string UpdatedTask { get; set; }


    }
    public class FileDetails
    {
        public string FileName { get; set; } = string.Empty;
        public string FileInBase64 { get; set; } = string.Empty;
        public byte[] Fileinbyte { get; set; }
    }
    public class TaskReport
    {
        public string SkippedTask { get; set; }
        public string TaskStatus { get; set; }
        public string Priority { get; set; }
    }

    #region  ------PDT Class---
    public class MenuShowHide
    {
        public string Module { get; set; }
        public string Screen { get; set; }
        public string Value { get; set; }
        public bool Visible { get; set; }
    }
    public class HolidayListDetails
    {
        public string Holiday { get; set; }
        public string Date { get; set; }
        public string Reason { get; set; }
        public string MachineID { get; set; }

    }

    public class PDTData
    {
        public string ID { get; set; }
        public string MachineID { get; set; }
        public string Reason { get; set; }
        public string FromDateTime { get; set; }
        public string ToDateTime { get; set; }
        public string FromDateTime1 { get; set; }
        public string ToDateTime1 { get; set; }
        public string ShiftName { get; set; }
        public string DownType { get; set; }
        public string Day { get; set; }
    }
    public class ColumnList
    {
        public string CompanyID { get; set; }
        public string FieldName { get; set; }
        public int FieldOrder { get; set; }
        public string FieldDisplayName { get; set; }
        public bool FieldVisibility { get; set; }
        public string Module { get; set; }
        public string Screen { get; set; }
        public string ObjectType { get; set; }
        public string DataValueField { get; set; }
    }
    public class CompanyGroupDetails
    {
        public string GroupID { get; set; }
        public string CorporateID { get; set; }
        public string GroupName { get; set; }
        public string CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string Place { get; set; }
        public string Country { get; set; }
        public string Pin { get; set; }
        public string AuthonticationMethod { get; set; }
        public string TwoFactAuthoMethod { get; set; }
        public string AuthonticationMethodDislplay { get; set; }
        public string TwoFactAuthoMethodDisplay { get; set; }
        public string EffectiveFromDate { get; set; }
        public string EffectiveToDate { get; set; }

        public string UserID { get; set; }
        public string Password { get; set; }

        public string NewOrEditParam { get; set; }
    }
    public class RoleDetails
    {
        public string MTBID { get; set; }
        public string CompanyID { get; set; }
        public string RoleID { get; set; }
        public string RoleName { get; set; }
        public string EffectiveFromDate { get; set; }
        public string EffectiveToDate { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedTS { get; set; }
        public string NewOrEditParam { get; set; }
    }
    public class UserData
    {
        public string MTBID { get; set; }
        public string CorporateID { get; set; }
        public string CompanyID { get; set; }
        public string UserID { get; set; }
        public string Username { get; set; }
        public bool IsGroupUser { get; set; }
        public string IOTID { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public byte[] UserImage { get; set; }
        public string UserImageInBase64 { get; set; }
        public string Role { get; set; }
        public bool IsEmployee { get; set; }
        public string EffectiveFromDate { get; set; }
        public string EffectiveToDate { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedTS { get; set; }
        public string EmployeeID { get; set; }
    }
    public class DownTimeAndRejectionDetails
    {
        public string CompanyID { get; set; }
        public string DownTime { get; set; }
        public string DownTimeNo { get; set; }
        public string Rejection { get; set; }
        public string RejectionNo { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string OldCategory { get; set; }
        public string OldSubCategory { get; set; }
        public string CategoryID { get; set; }
        public string SubCategoryID { get; set; }
        public string Sequence { get; set; }
        public string InterfaceID { get; set; }
        public bool OE { get; set; }
        public bool AE { get; set; }
        public bool PE { get; set; }
        public bool QE { get; set; }
        public bool MLFlag { get; set; }
        public string OEff { get; set; }
        public string AEff { get; set; }
        public string PEff { get; set; }
        public string QEff { get; set; }
        public string MLFlag_String { get; set; }
        public string Threshold { get; set; }
        public string Description { get; set; }
        public string EffectiveFromDate { get; set; }
        public string EffectiveToDate { get; set; }
        public string NewOrEditParam { get; set; }
    }

    public class MTBDetails
    {
        public string MTBID { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string EffectiveFromDate { get; set; }
        public string EffectiveToDate { get; set; }
        public string NewOrEditParam { get; set; }
    }
    public class CellData
    {
        public string CompanyID { get; set; }
        public string PlantID { get; set; }
        public string CellId { get; set; }
        public string GroupID { get; set; }
        public string CellDesc { get; set; }
        public string GroupDesc { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedTS { get; set; }
        public string EffectiveToDate { get; set; }

        public string EffectiveFromDate { get; set; }
    }
    public class SignatureComparisionDetails
    {
        //public ObjectId Id { get; set; }
        public string MachineID { get; set; }
        public string CompanyID { get; set; }
        public string PlantID { get; set; }
        public string ParameterID { get; set; }
        public string SignatureID { get; set; }
        // public string Seconds { get; set; }
        // public string SignatureValue { get; set; }
        public string Frequency { get; set; }
        public string ParameterValue { get; set; }
        //[BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? UpdatedDatetime { get; set; }
    }
    public class SignatureComparisionData
    {
        public string Company { get; set; }
        public string Plant { get; set; }
        public string MachineID { get; set; }
        public string ParameterID { get; set; }
        public string SignatureID { get; set; }
        public List<SignatureComparisionDetails> SignatureData { get; set; }
    }
    public class InspectionData
    {
        public string CompanyID { get; set; }
        public string PlantID { get; set; }
        public string MachineID { get; set; }
        public string ComponentID { get; set; }
        public string Operation { get; set; }
        public string RevDate { get; set; }
        public string RevID { get; set; }
        public string RevNumber { get; set; }
        public string CharacteristicID { get; set; }
        public string CharacteristicDesc { get; set; }
        public string LSL { get; set; }

        public string LWarning { get; set; }
        public string UWarning { get; set; }
        public string LOperating { get; set; }
        public string UOperating { get; set; }
        public string USL { get; set; }
        public string SpecificMean { get; set; }
        public string SampleSize { get; set; }
        public string InputMethod { get; set; }
        public string DataTemplateText { get; set; }
        public string DataTemplateValue { get; set; }
        public string InspectedByValue { get; set; }
        public string InspectedByText { get; set; }
        public bool IsEnabled { get; set; }
        public string ListOfValue { get; set; }
        public string UpdatedBy { get; set; }
        public string OldRevNumber { get; set; }
        public string SortOrder { get; set; }
        public string Unit { get; set; }
        public bool IsMandatory { get; set; }
        public string Mandatory { get; set; }
        public string MacroLocation { get; set; }
        public string MeasurementCondition { get; set; }
        public string SpecificationOfEquipment { get; set; }


        public string Value { get; set; }
        public string WorkOrder { get; set; }
        public string SerialNumber { get; set; }
        public string Remarks { get; set; }
        public string RowType { get; set; } //Header or Data
        public List<InspectionEntryData> InspectionEntryDatas { get; set; }
    }
    public class InspectionEntryData
    {
        public string SerialNumber { get; set; }
        public string HeaderTemplateVisibility { get; set; }
        public string DataTemplateVisibility { get; set; }
        public string SingleWOTempDisplay { get; set; }
        public string MultipleWOTempDisplay { get; set; }
        public string OperatorDisplay { get; set; }
        public string QAEngrDisplay { get; set; }
        public string OperatorColumnDisplay { get; set; }
        public string QAEngrColumnDisplay { get; set; }
        public string OperatorValue { get; set; }
        public string QAEngrValue { get; set; }
        public string TxtTemplateClass { get; set; } //based on template
        public string OprUpdatedByTS { get; set; }
        public string QAEngrUpdatedByTS { get; set; }
        public string OprBackColor { get; set; }
        public string OprColor { get; set; }
        public string QAEngrBackColor { get; set; }
        public string QAEngrColor { get; set; }
        public string Remarks { get; set; }
        public bool chkOprValue { get; set; }
        public bool chkQAvalue { get; set; }
        public bool chkDisplay { get; set; }
        public string lblDisplay { get; set; }
    }
    public class AlertShiftAllocation
    {
        public int SLNO { get; set; }
        public string CompanyID { get; set; }
        public string PlantID { get; set; }
        public string Consumer { get; set; }
        public string EmailId { get; set; }
        public string PhoneNo { get; set; }
        public string shift1 { get; set; }
        public string shift2 { get; set; }
        public string shift3 { get; set; }
        public string date1 { get; set; }
        public bool chkdate1shift1 { get; set; }
        public bool chkdate1shift2 { get; set; }
        public bool chkdate1shift3 { get; set; }

        public string date2 { get; set; }
        public bool chkdate2shift1 { get; set; }
        public bool chkdate2shift2 { get; set; }
        public bool chkdate2shift3 { get; set; }

        public string date3 { get; set; }
        public bool chkdate3shift1 { get; set; }
        public bool chkdate3shift2 { get; set; }
        public bool chkdate3shift3 { get; set; }
        public string date4 { get; set; }
        public bool chkdate4shift1 { get; set; }
        public bool chkdate4shift2 { get; set; }
        public bool chkdate4shift3 { get; set; }
        public string date5 { get; set; }
        public bool chkdate5shift1 { get; set; }
        public bool chkdate5shift2 { get; set; }
        public bool chkdate5shift3 { get; set; }
        public string date6 { get; set; }
        public bool chkdate6shift1 { get; set; }
        public bool chkdate6shift2 { get; set; }
        public bool chkdate6shift3 { get; set; }
        public string date7 { get; set; }
        public bool chkdate7shift1 { get; set; }
        public bool chkdate7shift2 { get; set; }
        public bool chkdate7shift3 { get; set; }
    }
    public class PMMasterData
    {
        public string CompanyID { get; set; }
        public string PlantID { get; set; }
        public string MTB { get; set; }
        public string MachineModel { get; set; }
        public string MachineSerialNumber { get; set; }
        public string MachineID { get; set; }
        public string UniqueCode { get; set; }
        public string Area { get; set; }
        public string SerialNumber { get; set; }
        public string ActivityID { get; set; }
        public string Activity { get; set; }
        public string Frequency { get; set; }
        public string FrequencyID { get; set; }
        public string ActivityType { get; set; }
        public string CheckPoint { get; set; }
        public string TargetTime { get; set; }
        public string MachineStatus { get; set; }
        public string SOP { get; set; }
        public string SOPFileName { get; set; }
        public string SOPFileID { get; set; }

        public string DeleteFlag { get; set; }
        public string EffectiveFromDate { get; set; }
        public string EffectiveToDate { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedTS { get; set; }

        public string LSL { get; set; }
        public string USL { get; set; }
        public string EntryType { get; set; }

        public string Department { get; set; }
    }
    public class ShiftDataModel
    {
        public string CompanyID { get; set; }
        public string shiftId { get; set; }
        public string ShiftName { get; set; }
        public string FromDay { get; set; }
        public string FromTime { get; set; }
        public string ToDay { get; set; }
        public string ToTime { get; set; }
    }
    public class SPCFocasEntity
    {
        public string Company { get; set; }
        public string Machine { get; set; }
        public string ReadFlagLoc { get; set; }
        public string ComponentLoc { get; set; }
        public string OperationLoc { get; set; }
        public string DateLoc { get; set; }
        public string TimeLoc { get; set; }
        public bool IsEnabled { get; set; }
        public string OperatorLoc { get; set; }
    }
    public class SPCTIDGaugeEntity
    {
        public string Company { get; set; }
        public string Machine { get; set; }
        public string RowID { get; set; }
        public string ChannelID { get; set; }
        public string ChannelName { get; set; }
        public string CharactristicID { get; set; }
        public string NoOfDigitAfterDecimal { get; set; }
    }
    public class EnergyMasterDetails
    {
        public string CompanyID { get; set; }
        public string PlantID { get; set; }
        public string MachineID { get; set; }
        public string InterfaceID { get; set; }
        public string MachineType { get; set; }
        public string MeterType { get; set; }
        public string SortOrder { get; set; }
        public string IPAddress { get; set; }
        public string LowerPowerThreshold { get; set; }
        public string PortNumber { get; set; }
        public bool IsEnabled { get; set; }

        public string NodeID { get; set; }
        public string EMNodeID { get; set; }
        public string SubSystem { get; set; }
        public string EMModelNo { get; set; }

        public string EffectiveFromDate { get; set; }
        public string EffectiveToDate { get; set; }
        public string NewOrEditParam { get; set; }
    }
    public class PredictiveMaintData
    {
        public string MTB { get; set; }
        public string AlarmNo { get; set; }
        public string AlarmDesc { get; set; }
        public string DurationType { get; set; }
        public string DurationIn { get; set; }
        public string TargetDLocation { get; set; }
        public string CurrentDLocation { get; set; }
        public bool IsEnabled { get; set; }
        public string UpdatedBy { get; set; }
    }
    public class InspectionDefaultEntity
    {
        public string Company { get; set; }
        public string MachineID { get; set; }
        public string Component { get; set; }
        public string Operation { get; set; }
        public string Operator { get; set; }
        public string ComponentInterfaceID { get; set; }
        public string OperationInterfaceID { get; set; }
        public string OperatorInterfaceID { get; set; }
    }
    public class PlantLevelDetails
    {
        public string GroupID { get; set; }
        public string CompanyID { get; set; }

        public string PlantID { get; set; }
        public string ShopID { get; set; }
        public string CellID { get; set; }

        public string Description { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string GeoLocation { get; set; }
        public string City { get; set; }
        public string EffectiveFromDate { get; set; }
        public string EffectiveToDate { get; set; }

        public string CompanyAdmin { get; set; }

        public string NewOrEditParam { get; set; }
        public string Currency { get; set; }
    }
    public class MachineData
    {

        public string CompanyID { get; set; }
        public string MachineID { get; set; }
        public string IOTID { get; set; }
        public string InterfaceID { get; set; }
        public string MachineDisplayName { get; set; }
        public string MachineType { get; set; }
        public string MachineMTB { get; set; }
        public string MachineModel { get; set; }
        public string CNCMake { get; set; }
        public string CNCModel { get; set; }
        public string PLCMake { get; set; }
        public string PLCModel { get; set; }
        public string IPAddress { get; set; }
        public string IPPortNo { get; set; }
        public string Mchrrate { get; set; }
        public bool TPMTrakEnabled { get; set; }
        public bool MultiSpindleFlag { get; set; }
        public string DeviceType { get; set; }
        public string MachinewiseOwner { get; set; }
        public bool CriticalMachineEnabled { get; set; }
        public bool EnergyMachineEnabled { get; set; }
        public bool DAPEnabled { get; set; }
        public bool EthernetEnabled { get; set; }
        public bool Nto1Device { get; set; }
        public string DNCIP { get; set; }
        public string DNCIPPortNo { get; set; }
        public bool DNCTransferEnabled { get; set; }
        public bool ProgramFolderEnabled { get; set; }
        public string AutoSetupChangeDown { get; set; }
        public bool AGIEnabled { get; set; }
        public string OPCUAURL { get; set; }
        public string ControllerType { get; set; }
        public string SerialNumber { get; set; }
        public string OEETarget { get; set; }
        public bool EnablePartCountByMacro { get; set; }

        public string S1 { get; set; }
        public string S2 { get; set; }
        public string S3 { get; set; }
        public string S4 { get; set; }
        public string Protocol { get; set; }
        public string OPCUrl { get; set; }
        public bool OEEEnabled { get; set; }
        public bool CNCParamEnabled { get; set; }
        public bool EneryEnabled { get; set; }
        public bool DeviceEnabled { get; set; }
        public string RowID { get; set; }

        public string UpdatedTS { get; set; }
        public string UpdatedBy { get; set; }

        public string MTBID { get; set; }
        public string CustomerID { get; set; }
        public string DispatchDate { get; set; }


        public string SyncedStatus { get; set; }
        public string EffectiveFromDate { get; set; }
        public string EffectiveToDate { get; set; }

        public string Axis { get; set; }
        public string Pallet { get; set; }
        public string MachineCategory { get; set; }
        public string MachineSubCategory { get; set; }

        public string TargetRevenue { get; set; }
    }
    public class OEEMacroLocationData
    {
        public string ID { get; set; }
        public string CompanyID { get; set; }
        public string MachineID { get; set; }
        public string DataReadFlagLocation { get; set; }
        public string DataStartLocation { get; set; }
        public string DataEndLocation { get; set; }
        public string NewOrEditParam { get; set; }
    }
    public class SPCEquatorEntity
    {
        public string Company { get; set; }
        public string Machine { get; set; }
        public string EquatorName { get; set; }
        public string FolderPath { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public string TemplateNameText { get; set; }
        public string TemplateNameValue { get; set; }
        public string COSelectionText { get; set; }
        public string COSelectionValue { get; set; }
    }
    public class ComponentInfoData
    {
        public string ComponentID { get; set; }
        public string CompanyID { get; set; }
        public string Interfaceid { get; set; }
        public string Customer { get; set; }
        public string Description { get; set; }

        public string EffectiveFromDate { get; set; }
        public string EffectiveToDate { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedTS { get; set; }
        public byte[] Drawing { get; set; }
        public string DrawingName { get; set; }
        public string DrawingInBase64 { get; set; }
    }

    #endregion

    #region----Tasktransaction Details----
    public class tasktransactiondetails
    {
        public string Id { get; set; }
        public string Projectid { get; set; } = string.Empty;
        public string Projectname { get; set; } = string.Empty;
        public string Weekno { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public string Maintask { get; set; } = string.Empty;
        public string Subtask { get; set; } = string.Empty;
        public string SubTaskEstimatedeffort { get; set; } = string.Empty;
        public string MainTaskEstimatedeffort { get; set; } = string.Empty;
        public string EstimatedEffort { get; set; } = string.Empty;
        public string Task { get; set; } = string.Empty;
        public string Spenthour { get; set; } = string.Empty;
        public string Remarks { get; set; } = string.Empty;
        public string MainTaskIDD { get; set; } = string.Empty;
        public string SubTaskIDD { get; set; } = string.Empty;
        public string TaskType { get; set; } = string.Empty;
        public string AssignedTo { get; set; } = string.Empty;
        public string Request { get; set; } = string.Empty;
        public string Param { get; set; } = string.Empty;
        public string ProblemID { get; set; } = string.Empty;
        public string SubtaskStatus { get; set; } = string.Empty;
        public bool HeaderVisibility { get; set; } = false;
        public bool Visible { get; set; } = false;
        public bool Maintaskvisible { get; set; } = false;
        public string Day1Value { get; set; } = string.Empty;
        public string Day1Date { get; set; } = string.Empty;
        public string Day1Remarks { get; set; } = string.Empty;
        public bool Day1EditBtnVisibility { get; set; } = true;
        public bool Day1TextBoxEable { get; set; } = true;
        public string Day1FileName { get; set; } = string.Empty;
        public string Day1FileInBase64 { get; set; } = string.Empty;
        public byte[] Day1bytefile { get; set; }
        public byte[] Day2bytefile { get; set; }
        public byte[] Day3bytefile { get; set; }
        public byte[] Day4bytefile { get; set; }

        public byte[] Day5bytefile { get; set; }
        public byte[] Day6bytefile { get; set; }
        public byte[] Day7bytefile { get; set; }
        public string Day2Value { get; set; } = string.Empty;
        public string Day2Date { get; set; } = string.Empty;
        public string Day2Remarks { get; set; } = string.Empty;
        public bool Day2EditBtnVisibility { get; set; } = true;
        public bool Day2TextBoxEable { get; set; } = true;
        public string Day2FileName { get; set; } = string.Empty;
        public string Day2FileInBase64 { get; set; } = string.Empty;
        public string Day3Value { get; set; } = string.Empty;
        public string Day3Date { get; set; } = string.Empty;
        public string Day3Remarks { get; set; } = string.Empty;
        public bool Day3EditBtnVisibility { get; set; } = true;
        public bool Day3TextBoxEable { get; set; } = true;
        public string Day3FileName { get; set; } = string.Empty;
        public string Day3FileInBase64 { get; set; } = string.Empty;
        public string Day4Value { get; set; } = string.Empty;
        public string Day4Date { get; set; } = string.Empty;
        public string Day4Remarks { get; set; } = string.Empty;
        public bool Day4EditBtnVisibility { get; set; } = true;
        public bool Day4TextBoxEable { get; set; } = true;
        public string Day4FileName { get; set; } = string.Empty;
        public string Day4FileInBase64 { get; set; } = string.Empty;
        public string Day5Value { get; set; } = string.Empty;
        public string Day5Date { get; set; } = string.Empty;
        public string Day5Remarks { get; set; } = string.Empty;
        public bool Day5EditBtnVisibility { get; set; } = true;
        public bool Day5TextBoxEable { get; set; } = true;
        public string Day5FileName { get; set; } = string.Empty;
        public string Day5FileInBase64 { get; set; } = string.Empty;
        public string Day6Value { get; set; } = string.Empty;
        public string Day6Date { get; set; } = string.Empty;
        public string Day6Remarks { get; set; } = string.Empty;
        public bool Day6EditBtnVisibility { get; set; } = true;
        public bool Day6TextBoxEable { get; set; } = true;
        public string Day6FileName { get; set; } = string.Empty;
        public string Day6FileInBase64 { get; set; } = string.Empty;
        public string Day7Value { get; set; } = string.Empty;
        public string Day7Date { get; set; } = string.Empty;
        public string Day7Remarks { get; set; } = string.Empty;
        public bool Day7EditBtnVisibility { get; set; } = true;
        public bool Day7TextBoxEable { get; set; } = true;
        public string Day7FileName { get; set; } = string.Empty;
        public string Day7FileInBase64 { get; set; } = string.Empty;
        public byte[] File { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FileInBase64 { get; set; } = string.Empty;
    
        public string Day1 { get; set; }
        public string Day2 { get; set; }
        public string Day3 { get; set; }
        public string Day4 { get; set; }
        public string Day5 { get; set; }
        public string Day6 { get; set; }
        public string Day7 { get; set; }

    }
    #endregion
}
