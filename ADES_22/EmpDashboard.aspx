<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmpDashboard.aspx.cs" Inherits="ADES_22.EmpDashboard" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="js/bootstrap-multiselect.js"></script>
    <link href="Css/bootstrap-multiselect.css" rel="stylesheet" />

    <%----- Style CSS -----%>

    <style>
        #li_dash {
            font-weight: bold;
        }

        .title {
            font-size: 22px;
            font-weight: bold;
            margin-left: 50px;
        }

        .lbltxt {
            font-size: 20px;
            margin-left: 6px;
        }

        .gc {
            overflow: auto;
            padding: 0px;
            width: 94%;
            margin-left: 25px;
            margin-top: -15px;
            overflow: auto;
            height: 70vh;
        }

        .table {
            width: 94.6%;
            border: 1px solid white;
            margin-left: -25px;
        }

            .table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {
                border: 1px solid white;
            }

        .divStyle {
            margin-left: -10px;
            margin-top: 2px;
        }

        .ViewAttachments {
            background-color: #EEEEEE;
            backface-visibility: hidden;
            width: 400px;
            position: relative;
            box-shadow: 5px 10px 18px #888888;
        }

        .fileSize {
            font-size: 15px;
        }

        .multiselect, .txtsize {
            width: 220px;
        }

        #ddlStatus_CList ~ .btn-group  .multiselect-container {
            overflow: auto;
            height: 150px;
        }

        .maxheight{
            max-height: 100px;
        }
    </style>

    <%----- Div Container -----%>

    <asp:UpdatePanel runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="divStyle">
                <table class="table">
                    <tr>
                        <td>
                            <span class="title">Dashboard -</span> <span class="lbltxt">Assigned to me </span>
                        </td>

                        <td>
                            <button id="BtnFilter" runat="server" class="Buttons FilterBtn float" onclick="return openNav()" title="Filter">
                                <i class="glyphicon glyphicon-filter glyphfilter" runat="server" />
                                Filter
                            </button>
                        </td>
                    </tr>
                </table>
            </div>

            <%----- Grid Container -----%>

            <div id="gridContainer" class="gridContainer gc" runat="server">
                <div id="scrollMaintainDiv" style="overflow: auto; height: 70vh">
                    <asp:GridView ID="griddash" runat="server" CssClass="grid" Width="100%" ClientIDMode="Static" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found !!" EmptyDataRowStyle-CssClass="empty-row-style">
                        <Columns>
                            <asp:TemplateField HeaderText="Project ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblPID" runat="server" Text='<%# Eval("PID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Project Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblPName" runat="server" Text='<%# Eval("PName") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Customer Name">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfModule" runat="server" Value='<%# Eval("Module") %>' />
                                    <asp:Label ID="lblCName" runat="server" Text='<%# Eval("CName") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Issue ID">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnk_IssueID" runat="server" Text='<%# Eval("IssueID") %>' OnClick="lnk_IssueID_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Issue Type">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfType" runat="server" Value='<%# Eval("IssueType") %>' ClientIDMode="AutoID" />
                                    <asp:Label ID="lblType" runat="server" Text='<%# Eval("IssueTypeDisplayName") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Issue Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblIssueName" runat="server" Text='<%# Eval("IssueName") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("IssueDesc") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Priority">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("Priority") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label runat="server" ToolTip="Any Changes Done Before This Issue" Text="Changes" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("Changes") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Steps to Reproduce">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("Steps") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Environment">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("Environment") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Reported Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblReportedDate" runat="server" Text='<%# Eval("ReportedDate") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Reporter Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblRType" runat="server" Text='<%# Eval("ReporterType") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Reported By">
                                <ItemTemplate>
                                    <asp:Label ID="lblReportedBy" runat="server" Text='<%# Eval("ReportedBy") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("Status") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Attachments">
                                <ItemTemplate>
                                    <div style="text-align: center">
                                        <asp:LinkButton ID="Link_Attachments" runat="server" ClientIDMode="Static" CssClass="glyphicon glyphicon-open-file fileSize" OnClientClick="return Attachments(event);" OnClick="Link_Attachments_Click" ToolTip="View Attachments" CausesValidation="false" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

            <asp:HiddenField ID="hfXValue" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hfYValue" runat="server" ClientIDMode="Static" />

            <%----- View Attachments -----%>

            <div id="ViewAttachments" class="ViewAttachments" runat="server">
                <div class="triangle-right"></div>
                <asp:Panel ID="InfoPanel" runat="server" CssClass="PanelCss" ClientIDMode="Static">
                    <div id="PanelBody" runat="server" class="panel-body" style="max-height: 600px; overflow: auto;">

                        <asp:GridView ID="GridAttachments" runat="server" CssClass="grid maxheight" Width="100%" ClientIDMode="Static" AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField HeaderText="Attachments" ShowHeader="false">
                                    <ItemTemplate>
                                        <div style="text-align: center">
                                            <asp:HiddenField ID="hfIDD" runat="server" Value='<%# Eval("IDD") %>' />
                                            <asp:LinkButton ID="link_filename" runat="server" Text='<%# Eval("FName") %>' OnClick="link_filename_Click" CausesValidation="false" />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </asp:Panel>
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
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="GridAttachments" />
            <asp:PostBackTrigger ControlID="griddash" />
        </Triggers>
    </asp:UpdatePanel>

    <%----- Filter Panel -----%>

    <div id="myNav" class="overlay">
        <i class="closebtn" style="z-index: 2005" onclick="closeNav();">&times;</i>
        <div class="overlay-content">
            <span class="FilterCss">Filter</span>
            <br />
            <br />

            <div class="OuterDiv">
                <div class="InnerDiv">
                    <span>Project ID</span>
                    <asp:TextBox ID="txtPrjID" runat="server" CssClass="form-control txtsize" ClientIDMode="Static" AutoCompleteType="Disabled" list="filter_IDList" />
                    <datalist id="filter_IDList" runat="server" clientidmode="static" />
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
                    <span>Priority</span><br />
                    <asp:ListBox ID="ddlPriority_CList" runat="server" CssClass="multiDropdown form-control txtsize" ClientIDMode="Static" SelectionMode="Multiple" />
                </div>
            </div>
            <br />

            <div class="OuterDiv">
                <div class="InnerDiv">
                    <span>Status</span><br />
                    <asp:ListBox ID="ddlStatus_CList" runat="server" CssClass="multiDropdown form-control txtsize" ClientIDMode="Static" SelectionMode="Multiple" />
                </div>
            </div>
            <br />
            <br />

            <div class="filterDiv">
                <asp:Button ID="BtnFilterSearch" runat="server" Text="Apply Filters" OnClick="BtnFilterSearch_Click" CssClass="btnlarge" />
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
                includeSelectAllOption: true
            });
        }

        document.getElementById("headerName").textContent = "Defect Tracker";

        $(".dropdown-container").css("display", "block");

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

            $(".dropdown-container").css("display", "block");
        });
    </script>
</asp:Content>
