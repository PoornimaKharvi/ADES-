<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProjectList.aspx.cs" Inherits="ADES_22.ProjectList" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" type="text/css">
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
        #li_plist {
            font-weight: bold;
        }

        .title {
            font-size: 22px;
            font-weight: bold;
            margin-left: 13%;
        }

        .table {
            width: 98%;
            border: 1px solid white;
        }

            .table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {
                border: 1px solid white;
            }

        .btncontainer {
            padding: 0px;
            width: 100%;
            margin-left: -1.95%;
        }

        .gc {
            width: 94%;
            margin-left: 2%;
            overflow: auto;
            margin-top: -1%;
        }

        .modal-tb {
            margin-left: 25px;
        }

            .modal-tb td {
                padding: 8px;
            }

        .width {
            width: 230px;
        }

        .widthTask {
            width: 220px;
        }

        .tblist {
            width: 340px;
        }

        .GridFiles {
            width: 100%;
            overflow: auto;
            height: 140px;
        }

        .HeaderStyle {
            width: 60px;
            text-align: center;
        }

        .multiselect-container {
            overflow: auto;
            height: 450px;
        }

        .LinkWeight{
            font-weight: 500;
        }

        .width1{
            width: 220px;
        }

        #ddlUsers ~ .btn-group  .multiselect-container {
            overflow: auto;
            height: 400px;
        }

        #DDLMainTaskSelector ~ .btn-group  .multiselect-container {
            overflow: auto;
            height: 170px;
        }
    </style>

    <%----- Div Container -----%>

    <asp:UpdatePanel runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div id="btncontainer" class="btncontainer">
                <table class="table">
                    <tr>
                        <td>
                            <%--<asp:Label CssClass="title" runat="server" Text="Projects" />--%>
                        </td>

                        <td style="float: right; padding-left: 10px;">
                            <button id="BtnFilter" runat="server" class="Buttons FilterBtn" onclick="return openNav()" title="Filter">
                                <i class="glyphicon glyphicon-filter glyphfilter" runat="server" />
                                Filter
                            </button>

                            <asp:Button ID="btnadd" runat="server" Text="Add Project" OnClick="btnadd_Click" CssClass="Buttons" />
                        </td>
                    </tr>
                </table>
            </div>

            <%----- Grid Container -----%>

            <div id="gridContainer" class="gridContainer gc" runat="server">
                <asp:GridView ID="gridprj" runat="server" CssClass="grid" AutoGenerateColumns="false" Width="100%" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found !!" EmptyDataRowStyle-CssClass="empty-row-style">
                    <Columns>
                        <asp:TemplateField HeaderText="Project ID">
                            <ItemTemplate>
                                <asp:LinkButton ID="lblID" runat="server" Text='<%# Eval("PID") %>' OnClick="lblID_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Project Name">
                            <ItemTemplate>
                                <asp:Label ID="lblPname" runat="server" Text='<%# Eval("Pname") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Customer Name">
                            <ItemTemplate>
                                <asp:Label ID="lblCname" runat="server" Text='<%# Eval("CName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Assigned Users">
                            <ItemTemplate>
                                <asp:Label ID="lblUsers" runat="server" Text='<%# Eval("Users") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Project Owner">
                            <ItemTemplate>
                                <asp:Label ID="lblProjectOwner" runat="server" Text='<%# Eval("ProjectOwner") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Project Created By">
                            <ItemTemplate>
                                <asp:Label ID="lblCreatedBy" runat="server" Text='<%# Eval("ProjectCreatedBy") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Estimated Effort (HH)">
                            <ItemTemplate>
                                <asp:Label ID="lblEstimatedEffort" runat="server" Text='<%# Eval("EstimatedEffort") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Delivery Date">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfDate" runat="server" Value='<%# Eval("DeliveryDate") %>' />
                                <asp:Label ID="lblDeliveryDate" runat="server" Text='<%# Eval("Date") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("ProjectStatus") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <div style="text-align: center">
                                    <asp:LinkButton ID="btnedit" runat="server" CssClass="glyphicon glyphicon-pencil EditlinkBtn" ToolTip="Edit" OnClick="btnedit_Click" CausesValidation="false"></asp:LinkButton>&nbsp;&nbsp;

                                    <asp:LinkButton ID="btndlt" runat="server" CssClass="glyphicon glyphicon-trash DeletelinkBtn" ToolTip="Delete" OnClick="btndlt_Click" CausesValidation="false"></asp:LinkButton>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <asp:HiddenField ID="hfXValue" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hfYValue" runat="server" ClientIDMode="Static" />

            <%----- Add/Edit Project Modal -----%>

            <div class="modal add-edit-modal" id="AddProjectModal" role="dialog" style="min-width: 300px">
                <div class="modal-dialog modal-dialog-centered" style="width: 1150px">
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:Label ID="modaltitle" CssClass="modal-title" runat="server" Text="Add Project" />
                            <a data-dismiss="modal" class="glyphicon glyphicon-remove closeButton"></a>
                            <asp:HiddenField runat="server" ID="hfNewOrEdit" />
                        </div>
                        <br />

                        <div class="modal-body" style="overflow: visible">
                            <table class="modal-table">
                                <tr>
                                    <td>
                                        <span>Project ID *</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtprjID" runat="server" ClientIDMode="Static" TabIndex="1" CssClass="form-control width" AutoCompleteType="Disabled" />
                                    </td>

                                    <td>
                                        <span>Project Name *</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtprjname" runat="server" ClientIDMode="Static" TabIndex="2" CssClass="form-control width" AutoCompleteType="Disabled" />
                                    </td>

                                    <td>
                                        <span>Customer Name</span>
                                    </td>
                                    <td>
                                        <input type="text" id="txtcname" runat="server" class="form-control" autocomplete="off" list="datalist_Customer" tabindex="3" />
                                        <datalist id="datalist_Customer" runat="server" clientidmode="Static" />
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <span>Assign User *</span>
                                    </td>
                                    <td>
                                        <asp:ListBox ID="ddlUsers" runat="server" TabIndex="4" CssClass="multiDropdown form-control width1" ClientIDMode="Static" Width="250px" SelectionMode="Multiple" />
                                    </td>

                                    <td>
                                        <span>Project Owner *</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlProjectOwner" runat="server" TabIndex="5" CssClass="form-control" ClientIDMode="Static" />
                                    </td>

                                    <td>
                                        <span>Estimated Effort *</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEstimatedEffort" runat="server" TabIndex="6" ClientIDMode="Static" CssClass="form-control width allow-hh-format" AutoCompleteType="Disabled" />
                                    </td>
                                </tr>

                                <tr>

                                    <td>
                                        <span>Delivery Date *</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDeliveryDate" runat="server" TabIndex="7" ClientIDMode="Static" TextMode="Date" AutoCompleteType="Disabled" Enabled="true" CssClass="form-control width" />
                                    </td>

                                    <td>
                                        <span>Status *</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlProjectStatus" runat="server" TabIndex="8" ClientIDMode="Static" CssClass="form-control" />
                                    </td>

                                    <td>
                                        <span>Project Created By</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCreatedBy" runat="server" CssClass="form-control" ClientIDMode="Static" ReadOnly="true" />
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <span>Main Task</span>
                                    </td>
                                    <td>
                                        <asp:ListBox ID="DDLMainTaskSelector" runat="server" CssClass="multiDropdown1 form-control width1" ClientIDMode="Static" TabIndex="9" SelectionMode="Multiple" />
                                    </td>

                                    <td>
                                        <span>Work Content</span>
                                    </td>
                                    <td>
                                        <asp:FileUpload ID="WC_Attachment" runat="server" TabIndex="10" ClientIDMode="Static" CssClass="form-control width" AllowMultiple="false" onchange="FUChange(this);" />
                                        <asp:HiddenField runat="server" ID="WC_hfDocument" ClientIDMode="Static" />
                                        <asp:HiddenField runat="server" ID="WC_hfDocumentName" ClientIDMode="Static" />
                                    </td>

                                    <td>
                                        <span>Project Deliverable Document</span>
                                    </td>
                                    <td>
                                        <asp:FileUpload ID="PDD_Attachment" runat="server" TabIndex="11" ClientIDMode="Static" CssClass="form-control width" AllowMultiple="false" onchange="FUChange(this);" />
                                        <asp:HiddenField runat="server" ID="PDD_hfDocument" ClientIDMode="Static" />
                                        <asp:HiddenField runat="server" ID="PDD_hfDocumentName" ClientIDMode="Static" />
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <span>HMI PLC DAP</span>
                                    </td>
                                    <td>
                                        <asp:FileUpload ID="HMI_Attachment" runat="server" TabIndex="12" CssClass="form-control width" AllowMultiple="false" onchange="FUChange(this);" />
                                        <asp:HiddenField runat="server" ID="HMI_hfDocument" ClientIDMode="Static" />
                                        <asp:HiddenField runat="server" ID="HMI_hfDocumentName" ClientIDMode="Static" />
                                    </td>

                                    <td>
                                        <span>Software DAP</span>
                                    </td>
                                    <td>
                                        <asp:FileUpload ID="SW_Attachment" runat="server" TabIndex="13" CssClass="form-control width" AllowMultiple="false" onchange="FUChange(this);" />
                                        <asp:HiddenField runat="server" ID="SW_hfDocument" ClientIDMode="Static" />
                                        <asp:HiddenField runat="server" ID="SW_hfDocumentName" ClientIDMode="Static" />
                                    </td>

                                    <td>
                                        <span>MOMs</span>
                                    </td>
                                    <td>
                                        <asp:FileUpload ID="MOM_Attachment" runat="server" TabIndex="14" CssClass="form-control width" AllowMultiple="false" onchange="FUChange(this);" />
                                        <asp:HiddenField runat="server" ID="MOM_hfDocument" ClientIDMode="Static" />
                                        <asp:HiddenField runat="server" ID="MOM_hfDocumentName" ClientIDMode="Static" />
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <span>Attachment</span>
                                    </td>
                                    <td>
                                        <asp:FileUpload ID="attachmentupload" CssClass="form-control width" TabIndex="15" runat="server" AllowMultiple="true" onchange="FUChange(this);" />
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

                                                    <asp:TemplateField HeaderText="Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFType" runat="server" Text='<%# Eval("FileType") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="HeaderStyle">
                                                        <ItemTemplate>
                                                            <div style="text-align: center">
                                                                <asp:LinkButton ID="link_download" runat="server" TabIndex="16" CssClass="glyphicon glyphicon-download" ToolTip="Download File" OnClick="link_download_Click" />
                                                                &nbsp;&nbsp;
                                                    <asp:LinkButton ID="link_Delete" runat="server" TabIndex="15" CssClass="glyphicon glyphicon-trash clr" ToolTip="Delete File" OnClick="link_Delete_Click" />
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
                            <input type="submit" value="Add" class="btn AddEditBtn" id="AddProject_Yes" tabindex="17" runat="server" onclick="return ProjectValidation();" onserverclick="AddProject_Yes_ServerClick" />
                            <input type="button" value="Cancel" class="btn btn-danger CancelBtn" tabindex="18" id="AddProject_No" data-dismiss="modal">
                        </div>
                    </div>
                </div>
            </div>

            <%----- Main Task Modal -----%>

            <div class="modal add-edit-modal" id="AddTaskModal" role="dialog" style="min-width: 300px">
                <div class="modal-dialog modal-dialog-centered" style="width: 690px">
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:Label ID="Label1" CssClass="modal-title" runat="server" Text="Add Main Task" />
                            <a data-dismiss="modal" class="glyphicon glyphicon-remove closeButton"></a>
                            <asp:HiddenField runat="server" ID="HiddenField1" />
                        </div>

                        <div class="modal-body" style="overflow: visible">
                            <table class="modal-table">
                                <tr>
                                    <td>
                                        <span>Project ID</span>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblProjectID" runat="server" CssClass="form-control widthTask" TabIndex="1" />
                                    </td>

                                    <td>
                                        <span>Main Task</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMainTask" runat="server" CssClass="form-control" TextMode="MultiLine" TabIndex="2" />
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <span>Sub Task</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSubTask" runat="server" CssClass="form-control widthTask" TabIndex="3" />
                                    </td>

                                    <td>
                                        <span>Task Type</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DdlTaskType" runat="server" CssClass="form-control widthTask" TabIndex="4" />
                                    </td>
                                </tr>

                                <tr>

                                    <td>
                                        <span>Estimated Effort</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTaskEstimatedEffort" runat="server" CssClass="form-control widthTask datetimepicker1" TabIndex="5" Style="position: relative; overflow: unset" />
                                    </td>

                                    <td>
                                        <span>Delivery Date</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTaskDeliveryDate" runat="server" CssClass="form-control" TextMode="Date" TabIndex="7" />
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <span>Status</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DdlStatus" runat="server" CssClass="form-control widthTask" TabIndex="6" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </div>

                        <div class="modal-footer">
                            <input type="submit" value="Add" class="btn AddEditBtn" id="AddTask" tabindex="15" runat="server" onclick="return ProjectValidation();" />
                            <input type="button" value="Cancel" class="btn btn-danger CancelBtn" tabindex="16" id="CloseModal" data-dismiss="modal">
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="gridFiles" />
            <asp:PostBackTrigger ControlID="AddProject_Yes" />
            <asp:PostBackTrigger ControlID="gridprj" />
        </Triggers>

    </asp:UpdatePanel>

    <%----- Delete Project Confirmation Modal -----%>

    <div class="modal add-edit-modal" id="ConfirmationModal" role="dialog" style="min-width: 300px;">
        <div class="modal-dialog modal-dialog-centered" style="width: 450px">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Confirmation?</h4>
                    <a data-dismiss="modal" class="glyphicon glyphicon-remove closeButton"></a>
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

    <%----- Delete File Confirmation Modal -----%>

    <div class="modal add-edit-modal" id="ConfirmationFileModal" role="dialog" style="min-width: 300px;">
        <div class="modal-dialog modal-dialog-centered" style="width: 450px">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Confirmation?</h4>
                    <a data-dismiss="modal" class="glyphicon glyphicon-remove closeButton"></a>
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

    <%----- PageRedirect Modal -----%>

    <div class="modal add-edit-modal" id="PageRedirectModal" role="dialog" style="min-width: 300px;">
        <div class="modal-dialog modal-dialog-centered" style="width: 450px">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Page Redirection</h4>
                    <a data-dismiss="modal" class="glyphicon glyphicon-remove closeButton"></a>
                </div>

                <div class="modal-body" style="color: black">
                    <span>Please select the page you want to redirect to. </span>
                    <br /><br />
                    <asp:LinkButton ID="Link_MainTask" runat="server" Text="Main Task" CssClass="LinkWeight" OnClick="Link_MainTask_Click" />
                    <br />
                    <asp:LinkButton ID="Link_Internal" runat="server" Text="Internal Issues" CssClass="LinkWeight"  OnClick="Link_Internal_Click" />
                    <br />
                    <asp:LinkButton ID="Link_External" runat="server" Text="Customer Site Issues" CssClass="LinkWeight"  OnClick="Link_External_Click" />
                </div>
            </div>
        </div>
    </div>

    <%----- Filter Panel -----%>

    <div id="myNav" class="overlay">
        <i class="closebtn" style="z-index: 2005" onclick="closeNav();">&times;</i>
        <div class="overlay-content">
            <span class="FilterCss">Filter</span>
            <br />
            <br />
            <br />

            <div class="OuterDiv">
                <div class="InnerDiv">
                    <span>Project ID</span>
                    <asp:TextBox ID="txtProjID" runat="server" CssClass="form-control" AutoCompleteType="Disabled" list="filter_IDlist" />
                    <datalist id="filter_IDlist" runat="server" clientidmode="static" />
                </div>
            </div>
            <br />

            <div class="OuterDiv">
                <div class="InnerDiv">
                    <span>Customer Name</span>
                    <asp:TextBox ID="txtCustName" runat="server" CssClass="form-control" AutoCompleteType="Disabled" list="filter_CNamelist" />
                    <datalist id="filter_CNamelist" runat="server" clientidmode="static" />
                </div>
            </div>
            <br />
            <br />

            <div class="filterDiv">
                <asp:Button ID="BtnFilterSearch" runat="server" Text="Apply Filters" CssClass="btnlarge" OnClick="BtnFilterSearch_Click" />
            </div>
        </div>
    </div>

    <%----- Scripts -----%>

    <script type="text/javascript">
        document.getElementById("myNav").style.width = "0%";
        $(".ViewAttachments").css("display", "none");

        function openNav() {
            document.getElementById("myNav").style.width = "340px";
            return false;
        }

        function closeNav() {
            document.getElementById("myNav").style.width = "0%";
            return false;
        }

        function Attachments(event) {
            if (event.pageX == null && event.clientX != null) {
                var doc = document.documentElement, body = document.body;
                event.pageX = event.clientX + (doc && doc.scrollLeft || body && body.scrollLeft || 0) - (doc && doc.clientLeft || body && body.clientLeft || 0);
                event.pageY = event.clientY + (doc && doc.scrollTop || body && body.scrollTop || 0) - (doc && doc.clientTop || body && body.clientTop || 0);
            }

            let x = event.pageX - $(".ViewAttachments").width() + 25;
            let y = event.pageY + 10;

            $("#hfXValue").val(x);
            $("#hfYValue").val(y);

            return true;
        }

        function SetPosition(x, y) {
            $(".ViewAttachments").css({ "position": "absolute", "top": y, "left": x, "display": "block" });
        }

        $(".ViewAttachments").on("mouseleave", function (e) {
            $(".ViewAttachments").css("display", "none");
        });

        $(document).ready(function () {
            ConntrolSetter();
        });

        function ConntrolSetter() {
            $('.multiDropdown').multiselect({
                includeSelectAllOption: true,
                enableFiltering: true,
                enableCaseInsensitiveFiltering: true
            });

            $('.multiDropdown1').multiselect({
                includeSelectAllOption: true
            });

            $(".datetimepicker").datetimepicker({
                format: 'HH',
                locale: 'en-US'
            });

            $(".datetimepicker1").datetimepicker({
                format: 'HH:mm',
                locale: 'en-US'
            });
        }

        function ProjectValidation() {
            if ($("[id$=txtprjID]").val() == "") {
                WarningToastr("Project ID is required!");
                $("[id$=txtprjID]").focus();
                return false;
            }

            if ($("[id$=txtprjname]").val() == "") {
                WarningToastr("Project name is required!");
                $("[id$=txtprjname]").focus();
                return false;
            }

            if ($("[id$=ddlProjectOwner]").val() == "Select") {
                WarningToastr("Owner name is required!");
                $("[id$=ddlProjectOwner]").focus();
                return false;
            }

            if ($("[id$=txtEstimatedEffort]").val() == "") {
                WarningToastr("Estimated effort is required!");
                $("[id$=txtEstimatedEffort]").focus();
                return false;
            }

            if ($("[id$=txtDeliveryDate]").val() == "") {
                WarningToastr("Delivery date is required!");
                $("[id$=txtDeliveryDate]").focus();
                return false;
            }

            if ($("[id$=ddlProjectStatus]").val() == "") {
                WarningToastr("Project status is required!");
                $("[id$=ddlProjectStatus]").focus();
                return false;
            }

            var UserDate = document.getElementById("txtDeliveryDate").value;
            var ToDate = new Date();

            if (new Date(UserDate).getTime() <= ToDate.getTime()) {
                WarningToastr("Delivery date must not be less than today's date!");
                $("[id$=ddlProjectStatus]").focus();
                return false;
            }

            return true;
        }

        function validateEmail(sEmail) {
            var filter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
            if (filter.test(sEmail)) {
                return true;
            }
            else {
                return false;
            }
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

            var CheckID = element.name.split('$').pop();
            if (CheckID == 'WC_Attachment') {
                $("#WC_hfDocument").val(document);
                $("#WC_hfDocumentName").val(documentName);
            }
            else if (CheckID == 'PDD_Attachment') {
                $("#PDD_hfDocument").val(document);
                $("#PDD_hfDocumentName").val(documentName);
            }
            else if (CheckID == 'HMI_Attachment') {
                $("#HMI_hfDocument").val(document);
                $("#HMI_hfDocumentName").val(documentName);
            }
            else if (CheckID == 'SW_Attachment') {
                $("#SW_hfDocument").val(document);
                $("#SW_hfDocumentName").val(documentName);
            }
            else if (CheckID == 'MOM_Attachment') {
                $("#MOM_hfDocument").val(document);
                $("#MOM_hfDocumentName").val(documentName);
            }
            else {
                $("#hfDocument").val(document);
                $("#hfDocumentName").val(documentName);
            }
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

        document.getElementById("headerName").textContent = "Projects";

        var user = "<%= (String)Session["username"] %>";
        $("#lblUserID").text("Hi " + user);
        $(".dropdown-container2").css("display", "block");

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            $(document).ready(function () {
                ConntrolSetter();
            });

            function openNav() {
                document.getElementById("myNav").style.width = "340px";
                return false;
            }

            function closeNav() {
                document.getElementById("myNav").style.width = "0%";
            }

            $(".dropdown-container2").css("display", "block");
        });
    </script>
</asp:Content>
