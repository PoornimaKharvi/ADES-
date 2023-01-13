<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProjectBacklog.aspx.cs" Inherits="ADES_22.ProjectBacklog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <script src="js/bootstrap-multiselect.js"></script>
    <link href="Css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="Scripts/DateTimePicker331/moment.js"></script>
    <script src="Scripts/DateTimePicker331/bootstrap-datetimepicker.min.js"></script>
    <link href="Scripts/DateTimePicker331/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <script src="Scripts/DateTimePicker/bootstrap-datepicker.js"></script>
    <link href="Scripts/DateTimePicker/bootstrap-datepicker3.css" rel="stylesheet" />
    <script src="Scripts/DateTimePicker/bootstrap-datepicker.en-IE.min.js"></script>
    <script src="Scripts/DateTimePicker/bootstrap-datepicker.zh-CN.min.js"></script>

    <%----- Style CSS -----%>

    <style>
        #li_backlog_External {
            background-color: #f0f0f0;
            color: black;
            font-weight: bold;
        }

        .title {
            font-size: 22px;
            font-weight: bold;
            margin-left: 0px;
            margin-top: 60px;
            color: black;
        }

        .tb {
            margin-left: 20px;
            margin-right: 20px;
        }

            .tb tr td {
                padding: 3px;
            }

        .form-control {
            width: 220px;
            resize: none;
        }


        .btncontainer {
            padding: 0px;
            width: 100%;
            position: sticky;
            margin-left: 43px;
            margin-top: 6px;
        }

        .table {
            margin-left: -25px;
            width: 94%;
            border: 1px solid white;
        }

            .table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {
                border: 1px solid white;
            }

        .gridContainer {
            overflow: auto;
            padding: 0px;
            width: 94%;
            height: 85vh;
            margin-left: 30px;
            margin-top: -15px;
        }

        .GridFiles {
            width: 100%;
            overflow: auto;
            height: 140px;
        }

        .HeaderStyle {
            width: 50px;
            text-align: center;
        }

        .multiselect, .txtsize {
            width: 220px;
        }

        .oc {
            margin-top: 5px;
        }
    </style>

    <%----- Div Container -----%>

    <asp:UpdatePanel runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div id="btncontainer" class="btncontainer">
                <table class="table">
                    <tr>
                        <td>
                            <asp:Label ID="lblProjectTitle" CssClass="title" runat="server" Text="Project Name" />
                        </td>

                        <td style="float: right; padding-left: 20px; padding-right: 20px;">
                            <button id="BtnFilter" runat="server" class="Buttons FilterBtn" onclick="return openNav()">
                                <i class="glyphicon glyphicon-filter glyphfilter" runat="server" />
                                Filter
                            </button>

                            <asp:Button ID="btnadd" runat="server" Text="Add Issue" OnClick="btnadd_Click" CssClass="Buttons" />
                        </td>
                    </tr>
                </table>
            </div>

            <%----- Grid Container -----%>

            <div id="gridContainer" class="gridContainer" runat="server">
                <asp:GridView ID="gridprjback" runat="server" CssClass="grid" AutoGenerateColumns="false" Width="100%" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found !!" EmptyDataRowStyle-CssClass="empty-row-style">
                    <Columns>
                        <asp:TemplateField HeaderText="Project Name" AccessibleHeaderText="ProjectName">
                            <ItemTemplate>
                                <asp:HiddenField ID="lblPID" runat="server" Value='<%# Eval("PID") %>' />
                                <asp:Label ID="lblPName" runat="server" Text='<%# Eval("PName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Module" AccessibleHeaderText="Module">
                            <ItemTemplate>
                                <asp:Label ID="lblModule" runat="server" Text='<%# Eval("Module") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Issue ID" AccessibleHeaderText="IssueID">
                            <ItemTemplate>
                                <asp:LinkButton ID="lblIssueID" runat="server" Text='<%# Eval("IssueID") %>' OnClick="lblIssueID_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Issue Type" AccessibleHeaderText="IssueType">
                            <ItemTemplate>
                                <asp:Label ID="lblType" runat="server" Text='<%# Eval("IssueTypeDisplayName") %>' />
                                <asp:HiddenField ID="hfType" runat="server" Value='<%# Eval("IssueType") %>' ClientIDMode="AutoID" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Issue Name" AccessibleHeaderText="IssueName">
                            <ItemTemplate>
                                <asp:LinkButton ID="lblName" runat="server" Text='<%# Eval("IssueName") %>' OnClick="lblName_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Description" AccessibleHeaderText="Description">
                            <ItemTemplate>
                                <asp:Label ID="lblSummary" runat="server" Text='<%# Eval("IssueDesc") %>' />
                                <asp:HiddenField ID="lblCName" runat="server" Value='<%# Eval("CName") %>' ClientIDMode="Static" />
                                <asp:HiddenField ID="lblOldAssignedTo" runat="server" Value='<%# Eval("OldAssignee") %>' />
                                <asp:HiddenField ID="lblNewAssignedTo" runat="server" Value='<%# Eval("NewAssignee") %>' />
                                <asp:HiddenField ID="lblOldStatus" runat="server" Value='<%# Eval("OldStatus") %>' />
                                <asp:HiddenField ID="lblNewStatus" runat="server" Value='<%# Eval("NewStatus") %>' />
                                <asp:HiddenField ID="lblChanges" runat="server" Value='<%# Eval("Changes") %>' ClientIDMode="Static" />
                                <asp:HiddenField ID="lblSteps" runat="server" Value='<%# Eval("Steps") %>' ClientIDMode="Static" />
                                <asp:HiddenField ID="lblEnvironment" runat="server" Value='<%# Eval("Environment") %>' ClientIDMode="Static" />
                                <asp:HiddenField ID="lblAttachment" runat="server" Value='<%# Eval("DocumentName") %>' />
                                <asp:HiddenField ID="hfReportedBy" runat="server" Value='<%# Eval("ReportedBy") %>' ClientIDMode="Static" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Priority" AccessibleHeaderText="Priority">
                            <ItemTemplate>
                                <asp:Label ID="lblPriority" runat="server" Text='<%# Eval("Priority") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Assigned To" AccessibleHeaderText="Assignee">
                            <ItemTemplate>
                                <asp:Label ID="lblAssignedTo" runat="server" Text='<%# Eval("Assignee") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Reported Date" AccessibleHeaderText="RDate">
                            <ItemTemplate>
                                <asp:Label ID="lblReportedDate" runat="server" Text='<%# Eval("ReportedDate") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Status" AccessibleHeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Action" AccessibleHeaderText="Action">
                            <ItemTemplate>
                                <div>
                                    <asp:LinkButton ID="BtnInfo" runat="server" ClientIDMode="Static" CssClass="glyphicon glyphicon-info-sign InfoLinkBtn" ToolTip="More Info" OnClick="BtnInfo_Click" CausesValidation="false" />&nbsp;
                            <asp:LinkButton ID="btnedit" runat="server" CssClass="glyphicon glyphicon-pencil EditlinkBtn" OnClick="btnedit_Click" ToolTip="Edit" CausesValidation="false" />&nbsp;
                            <asp:LinkButton ID="btndlt" runat="server" CssClass="glyphicon glyphicon-trash DeletelinkBtn" OnClick="btndlt_Click" ToolTip="Delete" CausesValidation="false" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <%----- History Info -----%>

            <div class="modal add-edit-modal" id="ViewHistoryModal" role="dialog" style="min-width: 500px;">
                <div class="modal-dialog modal-dialog-centered HistoryCenter">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" style="color: black;">Issue Activities <i class="glyphicon glyphicon-remove closeButton" data-dismiss="modal"></i></h4>
                        </div>

                        <div class="modal-body HistoryBody">
                            <div>
                                <asp:Label ID="Modal_PName" runat="server" CssClass="ModalPName" />
                                <span>- </span>
                                <asp:Label ID="Modal_IssueID" runat="server" CssClass="ModalIID" /><br />
                                <br />

                                <div id="FilterDivBlock" clientidmode="static">
                                    <span style="font-weight: 500">Filters: </span>
                                    <table class="modal-table">
                                        <tr>
                                            <td>
                                                <span>Assigned To:</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAssignee" runat="server" CssClass="form-control" AutoCompleteType="Disabled" />
                                            </td>

                                            <td>
                                                <span>Reported By:</span>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="Filter_ddlReportedBy" runat="server" CssClass="form-control" />
                                            </td>

                                            <td>
                                                <span>Status:</span>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="Filter_ddlStatus" runat="server" CssClass="form-control" />
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <span>Start Date:</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Filter_txtSDate" runat="server" TextMode="Date" CssClass="form-control" />
                                            </td>

                                            <td>
                                                <span>End Date:</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Filter_txtEDate" runat="server" TextMode="Date" CssClass="form-control" />
                                            </td>

                                            <td colspan="2" style="text-align: center">
                                                <asp:Button ID="ModalFilterButton" runat="server" Text="Apply Filters" CssClass="Buttons fbtn" />
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                </div>

                                <div class="Modaldiv">
                                    <asp:ListView ID="HistoryList" runat="server">
                                        <LayoutTemplate>
                                            <table style="width: 100%">
                                                <tr runat="server" id="itemplaceholder"></tr>
                                            </table>
                                        </LayoutTemplate>

                                        <ItemTemplate>
                                            <tr>
                                                <td class="HList">
                                                    <asp:Label ID="lblHistory" runat="server" Text='<%# Eval("Msg") %>' CssClass="ModalStatus" /><br />
                                                    <asp:Label ID="lblTime" runat="server" Text='<%# Eval("RDate") %>' CssClass="ModalRTime" /><br />
                                                    <asp:Label ID="lblReporter" runat="server" Text='<%# Eval("ReportedBy") %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>

                                        <EmptyDataTemplate>
                                            <div class="EmptyDiv">
                                                <span class="NoRecords">No history as of now!</span>
                                            </div>
                                        </EmptyDataTemplate>
                                    </asp:ListView>
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer">
                            <input type="button" value="Close" class="btn btn-danger CancelBtn" data-dismiss="modal" />
                        </div>
                    </div>
                </div>
            </div>

            <%----- Add/Edit Issue Modal -----%>

            <div class="modal add-edit-modal" id="InfoProjectModal" role="dialog" style="min-width: 1000px">
                <div class="modal-dialog modal-dialog-centered" style="width: 1170px">
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:Label ID="modaltitle" CssClass="modal-title" runat="server" Text="Add Issue" />
                            <a data-dismiss="modal" class="glyphicon glyphicon-remove closeButton"></a>
                            <asp:HiddenField runat="server" ID="hfNewOrEdit" />
                        </div>

                        <div class="modal-body" style="color: black">
                            <br />
                            <table class="modal-table">
                                <tr>
                                    <td>
                                        <span>Project * </span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlPname" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPname_SelectedIndexChanged" ClientIDMode="Static" CssClass="form-control" TabIndex="1" />
                                    </td>

                                    <td>
                                        <span>Customer Name</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="lblCustName" runat="server" CssClass="form-control" ReadOnly="true" />
                                    </td>

                                    <td>
                                        <span>Module *</span>
                                    </td>
                                    <td>
                                        <input type="text" id="ddlModule" runat="server" class="form-control" autocomplete="off" list="datalist" tabindex="2" />
                                        <datalist id="datalist" runat="server" clientidmode="Static" />
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <span>Issue Type * </span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlIssue" runat="server" CssClass="form-control" TabIndex="3">
                                            <asp:ListItem Text="Select" />
                                            <asp:ListItem Text="New Request" Value="NR" />
                                            <asp:ListItem Text="Enhancement Request" Value="ER" />
                                            <asp:ListItem Text="Defect Request" Value="DR" />
                                            <asp:ListItem Text="Bug Request" Value="BR" />
                                        </asp:DropDownList>
                                    </td>

                                    <td>
                                        <asp:HiddenField ID="hfIssueID" runat="server" ClientIDMode="Static" />
                                        <span>Issue Name *</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtIssueName" runat="server" CssClass="form-control" AutoCompleteType="Disabled" TabIndex="4" />
                                    </td>

                                    <td>
                                        <span>Issue Description</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control" TextMode="MultiLine" TabIndex="5" />
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <span>Priority</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlPriority" runat="server" CssClass="form-control" TabIndex="6" />
                                    </td>

                                    <td>
                                        <span>Assigned To</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlAssignee" runat="server" CssClass="form-control" TabIndex="7" />
                                    </td>

                                    <td>
                                        <span>Any changes that caused the issue </span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtChanges" runat="server" CssClass="form-control" TextMode="MultiLine" TabIndex="8" />
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <span>Steps to reproduce </span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSteps" runat="server" CssClass="form-control" TextMode="MultiLine" TabIndex="9" />
                                    </td>

                                    <td>
                                        <span>Reported By</span>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblReportedBy" runat="server" CssClass="form-control" />
                                    </td>

                                    <td>
                                        <span>Environment</span>
                                    </td>
                                    <td>
                                        <input type="text" id="txtEnvironment" runat="server" class="form-control" autocomplete="off" list="datalist_Environment" tabindex="10" />
                                        <datalist id="datalist_Environment" runat="server" clientidmode="Static" />
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <span>Status *</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" TabIndex="11" />
                                    </td>

                                    <td>
                                        <span>Attachment</span>
                                    </td>
                                    <td>
                                        <asp:FileUpload ID="attachFile" runat="server" Width="220px" CssClass="form-control" AllowMultiple="true" onchange="FUChange(this);" TabIndex="12" />
                                        <asp:HiddenField runat="server" ID="hfDocument" ClientIDMode="Static" />
                                        <asp:HiddenField runat="server" ID="hfDocumentName" ClientIDMode="Static" />
                                    </td>
                                </tr>

                                <tr>
                                    <td colspan="6">
                                        <div id="GridFileContainer" runat="server" class="GridFiles">
                                            <asp:GridView ID="gridFiles" runat="server" CssClass="grid GridFiles" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataText="No File Uploaded !!" EmptyDataRowStyle-CssClass="empty-row-style">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Filename">
                                                        <ItemTemplate>
                                                            <asp:HiddenField runat="server" ID="hfIDD" Value='<%# Eval("IDD") %>' />
                                                            <asp:Label ID="lblFileName" runat="server" Text='<%# Eval("FName") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="100px">
                                                        <ItemTemplate>
                                                            <div style="text-align: center">
                                                                <asp:LinkButton ID="link_download" runat="server" CssClass="glyphicon glyphicon-download" ToolTip="Download File" OnClick="link_download_Click" />
                                                                &nbsp;&nbsp;
                                                    <asp:LinkButton ID="link_Delete" runat="server" CssClass="glyphicon glyphicon-trash clr" ToolTip="Delete File" OnClick="link_Delete_Click" />
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </div>

                        <div class="modal-footer">
                            <input type="submit" value="Add" class="btn AddEditBtn" id="InfoIssue_Yes" onclick="return IssueValidation();" onserverclick="InfoIssue_Yes_ServerClick" runat="server" tabindex="13" />
                            <input type="button" value="Cancel" class="btn btn-danger CancelBtn" id="InfoIssue_No" runat="server" tabindex="14" data-dismiss="modal" />
                        </div>
                    </div>
                </div>
            </div>

            <%----- Add SubTask Modal -----%>

            <div class="modal add-edit-modal" id="SubTaskModal" role="dialog" style="min-width: 1000px">
                <div class="modal-dialog modal-dialog-centered" style="width: 1170px">
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:Label ID="Label1" CssClass="modal-title" runat="server" Text="Add Sub Task" />
                            <a data-dismiss="modal" class="glyphicon glyphicon-remove closeButton"></a>
                        </div>

                        <div class="modal-body" style="color: black;overflow: unset">
                            <br />
                            <table class="modal-table">
                                <tr>
                                    <td>
                                        <span>Year </span>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtYear" runat="server" CssClass="form-control" TabIndex="1" />
                                    </td>

                                    <td>
                                        <span>Week No. </span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DdlWeek" runat="server" CssClass="form-control" TabIndex="2" />
                                    </td>

                                    <td>
                                        <span>Project ID </span>
                                    </td>
                                    <td>
                                        <asp:HiddenField ID="hfProblemID" runat="server" />
                                        <asp:Label ID="LblTaskProjectID" runat="server" CssClass="form-control" TabIndex="3" />
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <span>Assigned To *</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlTaskAssignee" runat="server" CssClass="form-control" TabIndex="4" />
                                    </td>

                                    <td>
                                        <span>Main Task *</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DdlMainTask" runat="server" CssClass="form-control" TabIndex="5" />
                                    </td>

                                    <td>
                                        <span>Task Type *</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlTaskType" runat="server" CssClass="form-control" TabIndex="6" />
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <span>Sub Task *</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSubTask" runat="server" CssClass="form-control" TextMode="MultiLine" AutoCompleteType="Disabled" TabIndex="7" />
                                    </td>

                                    <td>
                                        <span>Manual Entry</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtManualEntry" runat="server" CssClass="form-control" AutoCompleteType="Disabled" TextMode="MultiLine" TabIndex="8" />
                                    </td>

                                    <td>
                                        <span>Estimated Effort (HH:mm) *</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTaskEstimatedEffort" runat="server" AutoCompleteType="Disabled" CssClass="form-control allow-hh-mm-format" TabIndex="9" />
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <span>Request *</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlTaskRequest" runat="server" CssClass="form-control" TabIndex="10">
                                            <asp:ListItem Text="Select" />
                                            <asp:ListItem Text="New Request" Value="NR" />
                                            <asp:ListItem Text="Enhancement Request" Value="ER" />
                                            <asp:ListItem Text="Defect Request" Value="DR" />
                                            <asp:ListItem Text="Bug Request" Value="BR" />
                                        </asp:DropDownList>
                                    </td>

                                    <td>
                                        <span>Dependency</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlDependency" runat="server" CssClass="form-control" TabIndex="11" />
                                    </td>

                                    <td>
                                        <span>Delivery Date</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTaskDate" runat="server" CssClass="form-control" TextMode="Date" TabIndex="12" />
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <span>Status</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlTaskStatus" runat="server" CssClass="form-control" TabIndex="13" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </div>

                        <div class="modal-footer">
                            <input type="submit" value="Add" class="btn AddEditBtn" id="AddTask" onclick="return TaskValidation();" onserverclick="AddTask_ServerClick" runat="server" tabindex="14" />
                            <input type="button" value="Cancel" class="btn btn-danger CancelBtn" id="Canceltask" runat="server" tabindex="15" data-dismiss="modal" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="gridFiles" />
            <asp:AsyncPostBackTrigger ControlID="InfoIssue_Yes" />
            <asp:PostBackTrigger ControlID="gridprjback" />
        </Triggers>
    </asp:UpdatePanel>

    <%----- Delete Issue Modal -----%>

    <div class="modal add-edit-modal" id="ConfirmationModal" role="dialog" style="min-width: 300px;">
        <div class="modal-dialog modal-dialog-centered" style="width: 450px">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" style="color: black;">Confirmation? <i class="glyphicon glyphicon-remove closeButton" data-dismiss="modal"></i></h4>
                </div>

                <div class="modal-body" style="color: black">
                    <asp:Label ID="ConfirmText" runat="server" CssClass="ConfirmText" />
                </div>

                <div class="modal-footer">
                    <input type="button" value="Yes" class="btn AddEditBtn" id="Delete_Yes" onserverclick="Delete_Yes_ServerClick" runat="server" />
                    <input type="button" value="No" class="btn btn-danger CancelBtn" id="Delete_No" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>

    <%----- Delete Issue File Modal -----%>

    <div class="modal add-edit-modal" id="ConfirmationFileModal" role="dialog" style="min-width: 300px; z-index: 1200">
        <div class="modal-dialog modal-dialog-centered" style="width: 450px">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" style="color: black;">Confirmation? <i class="glyphicon glyphicon-remove closeButton" data-dismiss="modal"></i></h4>
                </div>

                <div class="modal-body" style="color: black">
                    <asp:Label ID="ConfirmFileText" runat="server" CssClass="ConfirmText" />
                </div>

                <div class="modal-footer">
                    <input type="button" value="Yes" class="btn AddEditBtn" id="FileDelete_Yes" onserverclick="FileDelete_Yes_ServerClick" runat="server" />
                    <input type="button" value="No" class="btn btn-danger CancelBtn" id="FileDelete_No" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>

    <%----- Filter Panel -----%>

    <div id="myNav" class="overlay">
        <i class="cb" style="z-index: 2005" onclick="closeNav();">&times;</i>
        <div class="overlay-content oc">
            <span class="FilterCss">Filter</span>
            <br />
            <br />

            <div class="OuterDiv">
                <div class="InnerDiv">
                    <span>Project ID</span>
                    <asp:TextBox ID="txtProjID" runat="server" CssClass="form-control" AutoCompleteType="Disabled" list="flist" />
                    <datalist id="flist" runat="server" clientidmode="static" />
                </div>
            </div>
            <br />

            <div class="OuterDiv">
                <div class="InnerDiv">
                    <span>Issue Type</span><br />
                    <asp:ListBox ID="ddlIssueType_CList" runat="server" CssClass="multiDropdown form-control txtsize" ClientIDMode="Static" SelectionMode="Multiple" />
                </div>
            </div>
            <br />

            <div class="OuterDiv">
                <div class="InnerDiv">
                    <span>Status</span><br />
                    <asp:ListBox ID="ddlStatus_CList" runat="server" CssClass="multiDropdown form-control txtsize" SelectionMode="Multiple" />
                </div>
            </div>
            <br />

            <div id="DivFromDate" runat="server" class="OuterDiv">
                <div class="InnerDiv">
                    <span>From Date</span>
                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TextMode="Date" />
                </div>
            </div>
            <br />

            <div id="DivToDate" runat="server" class="OuterDiv">
                <div class="InnerDiv">
                    <span>To Date</span>
                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" TextMode="Date" />
                </div>
            </div>
            <br />

            <div class="filterDiv">
                <asp:Button ID="BtnFilterSearch" runat="server" Text="Apply Filters" CssClass="btnlarge" OnClick="BtnFilterSearch_Click" />
            </div>
        </div>
    </div>

    <%----- More Info Modal -----%>

    <div class="modal fade add-edit-modal" id="ViewMoreInfoModal" role="dialog" style="min-width: 300px;">
        <div class="modal-dialog modal-dialog-centered" style="width: 745px">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" style="color: black;">More Info<i class="glyphicon glyphicon-remove closeButton" data-dismiss="modal"></i></h4>
                </div>

                <div class="modal-body" style="color: black">
                    <table class="modal-table">
                        <tr>
                            <td>
                                <span class="fontStyle">Reported By:</span>
                            </td>
                            <td>
                                <asp:Label ID="lbl_ReportedBy" runat="server" ClientIDMode="Static" />
                            </td>

                            <td>
                                <span class="fontStyle">Customer Name:</span>
                            </td>
                            <td>
                                <asp:Label ID="lbl_CName" runat="server" ClientIDMode="Static" />
                            </td>
                        </tr>
                
                        <tr>
                            <td>
                                <span class="fontStyle">Any changes that caused the issue:</span>
                            </td>
                            <td>
                                <asp:TextBox ID="lbl_Changes1" runat="server" CssClass="form-control" ClientIDMode="Static" TextMode="MultiLine" Rows="4" ReadOnly="true" />
                            </td>

                            <td>
                                <span class="fontStyle">Steps to reproduce:</span>
                            </td>
                            <td>
                                <asp:TextBox ID="lbl_Steps1" runat="server" CssClass="form-control" ClientIDMode="Static" ReadOnly="true" Rows="4" TextMode="MultiLine" />
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <span class="fontStyle">Environment:</span>
                            </td>
                            <td>
                                <asp:Label ID="lbl_Environment" runat="server" ClientIDMode="Static" />
                            </td>
                        </tr>
                    </table>   
                </div>

                <div class="modal-footer">
                    <input type="button" value="Close" class="btn btn-danger CancelBtn" id="BtnCancel" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>

    <%----- Scripts -----%>

    <script type="text/javascript">
        document.getElementById("myNav").style.width = "0%";
        document.getElementById("FilterDivBlock").style.display = "none";
     
        function openNav() {
            document.getElementById("myNav").style.width = "340px";
            return false;
        }

        function closeNav() {
            document.getElementById("myNav").style.width = "0%";
            return false;
        }

        function IssueValidation() {
            if ($("[id$=ddlPname]").val() == "Select") {
                WarningToastr("Project name is required!");
                $("[id$=ddlPname]").focus();
                return false;
            }

            if ($("[id$=ddlModule]").val() == "") {
                WarningToastr("Module is required!");
                $("[id$=ddlModule]").focus();
                return false;
            }

            if ($("[id$=ddlIssue]").val() == "Select") {
                WarningToastr("Issue type is required!");
                $("[id$=ddlIssue]").focus();
                return false;
            }

            if ($("[id$=txtIssueName]").val() == "") {
                WarningToastr("Issue name is required!");
                $("[id$=txtIssueName]").focus();
                return false;
            }

            if ($("[id$=ddlStatus]").val() == "Select") {
                WarningToastr("Status is required!");
                $("[id$=ddlStatus]").focus();
                return false;
            }
            return true;
        };

        function TaskValidation() {
            if ($("[id$=ddlTaskAssignee]").val() == "Select") {
                WarningToastr("Task assignee is required!");
                $("[id$=ddlTaskAssignee]").focus();
                return false;
            }

            if ($("[id$=DdlMainTask]").val() == "Select") {
                WarningToastr("Main task is required!");
                $("[id$=DdlMainTask]").focus();
                return false;
            }

            if ($("[id$=ddlTaskType]").val() == "Select") {
                WarningToastr("Task type is required!");
                $("[id$=ddlTaskType]").focus();
                return false;
            }

            if ($("[id$=txtSubTask]").val() == "") {
                WarningToastr("Sub task is required!");
                $("[id$=txtSubTask]").focus();
                return false;
            }

            if ($("[id$=txtTaskEstimatedEffort]").val() == "") {
                WarningToastr("Estimated effort is required!");
                $("[id$=txtTaskEstimatedEffort]").focus();
                return false;
            }

            if ($("[id$=ddlTaskRequest]").val() == "Select") {
                WarningToastr("Request is required!");
                $("[id$=ddlTaskRequest]").focus();
                return false;
            }

            return true;
        };

        function clearAll() {
            $('#attachfile').contents.clearAll();
        }

        $(document).ready(function () {
            ConntrolSetter();
        });

        function ConntrolSetter() {
            $('.multiDropdown').multiselect({
                includeSelectAllOption: true
            });

            $(".datetimepicker").datetimepicker({
                format: 'HH',
                locale: 'en-US'
            });
        }

        async function FUChange(element) {
            const filePathsPromises = [];
            const fileName = [];

            var fileExtension = ['xlsx', 'pdf', 'pptx', 'docx', 'png', 'jpg', 'jpeg'];

            debugger;
            for (var i = 0; i < $(element).get(0).files.length; ++i) {
                filePathsPromises.push(ToBase64($(element).get(0).files[i]));
                fileName.push($(element).get(0).files[i].name);

                if ($.inArray($(element).get(0).files[i].name.split('.').pop().toLowerCase(), fileExtension) == -1) {
                    WarningToastr("Allowed file types are xlsx, pdf, ppt, docx, png, jpg and jpeg only!", "");
                    $(element).val("");
                    return;
                }
            }

            debugger;
            const filePaths = await Promise.all(filePathsPromises);
            mappedFiles = filePaths.map((base64File) => ({ file: base64File }));
            let document = "", documentName = "";
            for (let i = 0; i < mappedFiles.length; i++) {
                if (document == "") {
                    document = mappedFiles[i].file;
                    documentName = fileName[i];
                } else {
                    document += ";;;" + mappedFiles[i].file;
                    documentName += ";;;" + fileName[i];
                }
            }

            $("#hfDocument").val(document);
            $("#hfDocumentName").val(documentName);
        }

        function ToBase64(file) {
            return new Promise((resolve, reject) => {
                const reader = new FileReader();
                reader.readAsDataURL(file);
                reader.onload = () => resolve(reader.result);
                reader.onerror = error => reject(error);
            });
        };

        $(window).resize(function () {
            var Height = $(window).height() - 290;
            $('.gridContainer').css('height', Height);
        });

        $(".dropdown-container").css("display", "block");

        document.getElementById("headerName").textContent = "Defect Tracker";

        var user = "<%= (String)Session["username"] %>";
        $("#lblUserID").text("Hi " + user);

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            function openNav() {
                document.getElementById("myNav").style.width = "340px";
                return false;
            }

            function closeNav() {
                document.getElementById("myNav").style.width = "0%";
                return false;
            }

            $(document).ready(function () {
                ConntrolSetter();
            });

            $(".dropdown-container").css("display", "block");
        });
    </script>
</asp:Content>
