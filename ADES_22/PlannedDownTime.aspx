<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PlannedDownTime.aspx.cs" Inherits="ADES_22.PlannedDownTime" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script src="Scripts/DateTimePicker/bootstrap-datepicker.js"></script>
    <link href="Scripts/DateTimePicker/bootstrap-datepicker3.css" rel="stylesheet" />
    <script src="js/bootstrap-multiselect.js"></script>
    <link href="Css/bootstrap-multiselect.css" rel="stylesheet" />

    <style>
        
        .closeButton {
            display: inline-block;
            font-size: 30px;
            color: red;
            vertical-align: middle;
            margin-right: 5px;
            cursor: pointer;
            text-align: center;
            height: auto;
        }

        .txtCommon {
            width: 150px;
        }
        .multiselect .listbox{
            width:200px;
        }

        .closeButton:hover {
            text-shadow: 0 0 3px red;
            display: inline-block;
            font-size: 30px;
            color: red;
            vertical-align: middle;
            margin-right: 5px;
            cursor: pointer;
            text-align: center;
            height: auto;
        }


        #rejectedItemsContainer {
            height: 60vh;
            overflow: auto;
        }


        #shiftFilterMachineTbl button {
            width: 100px;
            overflow: hidden;
        }

        .griContainer table tr:last-child td {
            position: sticky;
            bottom: 0px;
            background: #f7f7f7;
        }

            .griContainer table tr:last-child td a {
                font-size: 20px;
                color: white;
            }

            .griContainer table tr:last-child td span {
                font-size: 23px;
                color: #0c8dfb;
            }


        .multiselect-container {
            /*height: 300px;
            overflow-x: auto;*/
        }

        .multiselect-selected-text {
            padding-right: 181px;
        }

        .multiselect .dropdown-toggle {
            width: 50%;
        }

        .multiselect {
            width: 200px;
            overflow: hidden;
            display: block;
        }

        .multiselect-container label {
            color: black;
        }

        .multiselect span {
            color: black;
        }

        .tblAction {
            background-color: lightgrey;
        }

        .light-mode .tblAction {
            background-color: white;
        }

        .tblAction > tbody > tr td {
            padding: 2px 5px;
        }

        .downFilterTbl tr td {
            padding: 0px !important;
            padding-left: 5px !important;
        }

        .datetimeCss {
            width: 110px;
        }

        .P1Table .paginationCss td {
            position: sticky;
            bottom: 0px;
            background: #64696f;
            padding: 1px 10px !important;
        }

        .light-mode P1Table .paginationCss td {
            background: #ebebeb !important;
        }

        P1Table .paginationCss td a {
            font-size: 23px;
            color: white;
        }

        P1Table .paginationCss td span {
            font-size: 20px;
            color: #0fdcf1;
        }
    </style>

    <div class="container-fluid" style="width: 100%; margin: auto; margin-top: 0px; height: 100%">
        <asp:HiddenField runat="server" ID="hdnActiveTab" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="selectedMenu" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="hdnScrollPos" ClientIDMode="Static" />

        <div style="float: left; margin-top: 5px; width: 100%">
            <%----------SubMenu---------------------%>
            <asp:UpdatePanel runat="server" ID="PDtUP2">
                <ContentTemplate>
                    <div class="navbar-collapse collapse" style="padding-left: 0px; height: 42px !important;">
                        <ul id="masterul" class="nav navbar-nav nextPrevious submenus-style ">
                            <li id="tabHoliday" runat="server"><a runat="server" class="submenuData" id="A15" clientidmode="static" data-toggle="tab" href="#tabHolidayContent">Holidays</a>
                                <i></i>
                            </li>
                            <li id="tabWeeklyOff" runat="server"><a runat="server" class="submenuData" id="A1" clientidmode="static" data-toggle="tab" href="#tabWeeklyOffContent">Weekly Offs</a>
                                <i></i>
                            </li>
                            <li id="tabAllDown" runat="server"><a runat="server" class="submenuData" id="A4" clientidmode="static" data-toggle="tab" href="#tabAllDownContent">All Downs</a>
                                <i></i>
                            </li>
                        </ul>
                    </div>
                    <div style="display: none">
                        <asp:Button runat="server" ID="btnHodiday" OnClick="btnHodiday_Click" />
                        <asp:Button runat="server" ID="btnWeeklyOff" OnClick="btnWeeklyOff_Click" />
                        <asp:Button runat="server" ID="btnAllDown" OnClick="btnAllDown_Click" />
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSaveHoliday" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnSaveWeekOffs" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnViewAllDowns" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>

            <div class="tab-content themetoggle" id="processParamContainer" style="overflow: auto; width: 100%; margin: -10px auto;">
                <%-----------holidayContent  -------%>
                <div id="tabHolidayContent" class="tab-pane fade" ClientIDMode="Static">
                    <asp:UpdatePanel ID="holidayUP1" runat="server">
                        <ContentTemplate>
                            <fieldset class="field-set">
                                <legend>&nbsp;<b>Filter By</b></legend>
                                <table class="filterTbl filter-field-table" style="width: 80%">
                                    <tr>
                                        <td>
                                            <asp:Label runat="server">From</asp:Label>
                                        </td>
                                        <td style="padding-right: 6px;">
                                            <%--<asp:TextBox ID="txtFromYear" " runat="server" CssClass="form-control datetimeCss txtCommon " placeholder="Year" AutoCompleteType="Disabled"></asp:TextBox>--%>
                                            <asp:TextBox ID="txtFromHolidayYear" runat="server" CssClass="form-control datetimeCss txtCommon" AutoCompleteType="Disabled"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label runat="server">To</asp:Label>
                                        </td>
                                        <td style="padding-right: 6px;">
                                            <asp:TextBox ID="txtToYear" runat="server" CssClass="form-control datetimeCss txtCommon " placeholder="Year" AutoCompleteType="Disabled"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label runat="server">Month</asp:Label>
                                        </td>
                                        <td style="padding-right: 6px;">
                                            <asp:DropDownList runat="server" ID="ddlMonth" CssClass="form-control dropdown-list txtCommon " OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged" AutoPostBack="true" ClientIDMode="Static">
                                            </asp:DropDownList>
                                        </td>
                                        <td colspan="2">
                                            <asp:Label runat="server">Day</asp:Label>
                                        </td>
                                        <td style="padding-right: 6px;">
                                            <asp:DropDownList runat="server" ID="ddlDay" CssClass="form-control dropdown-list">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Label runat="server">Reason</asp:Label></td>
                                        <td style="padding-right: 15px;">
                                            <input id="ddlReason" runat="server" class="form-control txtCommon" onkeypress="return restrictSpecialCharacter(event);" clientidmode="static" autocomplete="off">
                                        </td>
                                        <td colspan="2">
                                            <asp:Button runat="server" ID="btnSaveHoliday" Text="Save" CssClass="Buttons" OnClick="btnSaveHoliday_Click" OnClientClick="return showLoaderWithValidation('ddlReason');" />

                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnHodiday" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnWeeklyOff" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnAllDown" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="holidayUP2" runat="server">
                        <ContentTemplate>
                            <table style="margin-top: 10px; border: none; width: 80%; height: 45px" class="tblAction">
                                <tr>
                                    <td style="width: 50px; text-align: center; padding: 2px;">
                                        <asp:CheckBox runat="server" ID="chkSelectAllHoliday" CssClass="chkstyle" onclick="clickAllcheck(this);" ClientIDMode="Static" />
                                    </td>
                                    <td id="tdRepeat" style="width: 30px;">
                                        <asp:LinkButton runat="server" ID="lbtnReload" CssClass="glyphicon glyphicon-repeat" Font-Size="16" ToolTip="Reload" OnClick="lbtnReload_Click" />
                                    </td>
                                    <td id="tdDelete" style="width: 30px;">
                                        <asp:LinkButton runat="server" ID="lbtnDelete" CssClass="glyphicon glyphicon-trash" Font-Size="16" ToolTip="Delete" OnClientClick="return openDeleteConfirmModal('holidayListConfirmModal','gvHolidays','chkSelectHoliday')" />
                                    </td>
                                    <td id="tdFilter" style="width: 30px;">
                                        <asp:LinkButton runat="server" ID="lnkFilter" CssClass="glyphicon glyphicon-filter" Font-Size="18" ToolTip="Filter" OnClick="lnkFilter_Click" />
                                    </td>
                                    <td style="text-align: left;">
                                        <table id="divHolidayFilter" runat="server" clientidmode="static" class="downFilterTbl">
                                            <tr>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="ddlFilter" AutoPostBack="true" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged" ClientIDMode="Static" CssClass="form-control  dropdown-list">
                                                        <asp:ListItem Value="ByDate" Text="By Date" />
                                                        <asp:ListItem Value="ByReason" Text="By Reason" />

                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <table id="holidayDateContainer" runat="server">
                                                        <tr>
                                                            <td>
                                                                <asp:Label runat="server" ID="lblDate" ClientIDMode="Static" Text="From Date" /></td>
                                                            <td>
                                                                <asp:TextBox ID="txtFilterByDateFrom" ClientIDMode="Static" runat="server" AutoPostBack="true" CssClass="form-control datetimeCss" AutoCompleteType="Disabled" placeholder="Date"></asp:TextBox></td>
                                                            <td>
                                                                <asp:Label runat="server" ID="Label1" ClientIDMode="Static" Text="To Date" Style="font-size: 14px; vertical-align: middle; margin-left: 10px;" /></td>
                                                            <td>
                                                                <asp:TextBox ID="txtFilterByDateTo" ClientIDMode="Static" AutoPostBack="true" runat="server" CssClass="form-control datetimeCss" AutoCompleteType="Disabled"></asp:TextBox></td>
                                                        </tr>

                                                    </table>
                                                </td>
                                                <td style="margin-right: 60px">
                                                    <table id="holidayReasonContainer" runat="server">
                                                        <tr>
                                                            <td>Reason</td>
                                                            <td>
                                                                <asp:DropDownList runat="server" ID="ddlFilterByReason" AutoPostBack="true"  ClientIDMode="Static" CssClass="form-control dropdown-list">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td></td>
                                                <td>
                                                    <asp:Button runat="server" ID="btnApplyHoliday" OnClick="btnApplyHoliday_Click" CssClass="Buttons"   ClientIDMode="Static" Text="Apply" Style="margin-left: 10px; display: inline-block;" OnClientClick="return showLoader();" />
                                                </td>
                                                <td>
                                                    <asp:LinkButton runat="server" ID="lnkHolidayFilterClose" CssClass="closeButton" ToolTip="Close" Text="&times;" OnClick="lnkHolidayFilterClose_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>

                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnHodiday" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnWeeklyOff" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnAllDown" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="holidayUP3" runat="server">
                        <ContentTemplate>
                            <div id="divHolidayGrid" style="overflow: auto; color: black; padding-top: 2px; width: 80%; height: 65vh" class="grid griContainer">

                                <asp:GridView runat="server" ID="gvHolidays" AutoGenerateColumns="false" ShowHeader="true" ShowFooter="false" CssClass="grid" ClientIDMode="Static" AllowPaging="true" OnPageIndexChanging="gvHolidays_PageIndexChanging" OnPreRender="gvHolidays_PreRender" PageSize="100" EmptyDataText="No Data Found...!!" EmptyDataRowStyle-CssClass="empty-row-style" ShowHeaderWhenEmpty="true" Style="width: 100%; height: 70%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="chkSelectHoliday" ClientIDMode="Static" onclick="checkclick(this)" />
                                            </ItemTemplate>
                                            <ItemStyle Width="5%" HorizontalAlign="center" />
                                            <HeaderStyle Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date" AccessibleHeaderText="Date">
                                            <ItemTemplate>
                                                <asp:HiddenField runat="server" ID="hdnDate" Value='<%# Eval("Date") %>' />
                                                <asp:Label runat="server" ID="lblHoliday" Text='<%# Eval("Holiday") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" HorizontalAlign="center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Down Reason" AccessibleHeaderText="DownReason">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblReason" Text='<%# Eval("Reason") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="40%" ForeColor="Black" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="paginationCss" />
                                </asp:GridView>
                            </div>
                            <div class="modal  add-edit-modal" id="holidayListConfirmModal" role="dialog" style="min-width: 300px;">
                                <div class="modal-dialog " style="width: 450px">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <asp:Label class="modal-title" runat="server">Confirmation</asp:Label>
                                            <a data-dismiss="modal" class="glyphicon glyphicon-remove closeButton"></a>
                                        </div>
                                        <div class="modal-body">
                                            <span id="confirmationmessageText" class="ConfirmText">Do you want to delete this record?</span>
                                        </div>

                                        <div class="modal-footer ">
                                            <asp:Button runat="server" Text="Yes" ID="Button1" CssClass="btn AddEditBtn" OnClick="btnDeleteRecordsYes_Click" OnClientClick="return clearModalScreen();" />
                                            <input type="button" value="No" data-dismiss="modal" class="btn btn-danger CancelBtn" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnHodiday" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnWeeklyOff" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnAllDown" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnSaveHoliday" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>

                <%------------WeeklyoffContent -----------------%>
                <div id="tabWeeklyOffContent" class="tab-pane fade">
                    <asp:UpdatePanel runat="server" ID="WeeklyOffUP1">
                        <ContentTemplate>
                            <fieldset class="field-set">
                                <legend>&nbsp;<b>Filter By</b></legend>
                                <table class="filterTbl filter-field-table" style="width: 80%">
                                    <tr>
                                        <td style="padding-right: 3px;">
                                            <asp:Label runat="server">From</asp:Label></td>
                                        <td style="padding-right: 10px;">
                                            <asp:TextBox runat="server" ID="txtFromWeeklyOffs" CssClass="form-control datetimeCss txtCommon" AutoCompleteType="Disabled" ClientIDMode="Static" />

                                        </td>
                                        <td style="padding-right: 3px;">
                                            <asp:Label runat="server">To</asp:Label>
                                        </td>
                                        <td style="padding-right: 10px;">
                                            <asp:TextBox runat="server" ID="txtToWeeklyOffs" CssClass="form-control datetimeCss txtCommon" AutoCompleteType="Disabled" />
                                        </td>
                                        <td style="padding-right: 3px;">
                                            <asp:Label runat="server">Days</asp:Label>
                                        </td>
                                        <td style="padding-right: 10px;">
                                            <asp:ListBox ID="ddlDays" runat="server" SelectionMode="Multiple" ClientIDMode="Static" CssClass="form-control  dropdown-list" Style="height: 34px; width: 100px">
                                                <asp:ListItem Text="Sun" />
                                                <asp:ListItem Text="Mon" />
                                                <asp:ListItem Text="Tue" />
                                                <asp:ListItem Text="Wed" />
                                                <asp:ListItem Text="Thu" />
                                                <asp:ListItem Text="Fri" />
                                                <asp:ListItem Text="Sat" />
                                            </asp:ListBox>

                                        </td>
                                        <td style="padding-right: 3px;">
                                            <asp:Label runat="server">Week</asp:Label>
                                        </td>
                                        <td style="padding-right: 10px;">

                                            <asp:ListBox ID="ddlWeek" runat="server" SelectionMode="Multiple" ClientIDMode="Static" CssClass="form-control  dropdown listbox" Style="width: 100px"><%--style="height:34px;width:60px"--%>
                                                <asp:ListItem Value="1" Text="1st Week" />
                                                <asp:ListItem Value="2" Text="2nd Week" />
                                                <asp:ListItem Value="3" Text="3rd Week" />
                                                <asp:ListItem Value="4" Text="4th Week" />
                                                <asp:ListItem Value="5" Text="5th Week" />
                                            </asp:ListBox>
                                        </td>
                                        <td style="padding-right: 3px;">
                                            <asp:Label runat="server">Reason</asp:Label></td>
                                        <td style="padding-right: 6px;">
                                            <asp:TextBox runat="server" ID="txtWeeklyOffReason" CssClass="form-control txtCommon" onkeypress="return restrictSpecialCharacter(event);" ClientIDMode="Static" AutoCompleteType="Disabled"> </asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button runat="server" ID="btnSaveWeekOffs" Text="Save" CssClass="Buttons" OnClick="btnSaveWeekOffs_Click" OnClientClick="return showLoaderWithValidation('txtWeeklyOffReason');" />
                                        </td>

                                    </tr>
                                </table>
                            </fieldset>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnHodiday" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnWeeklyOff" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnAllDown" EventName="Click" />
                             <asp:AsyncPostBackTrigger ControlID="btnApplyWeekOffsFilter" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel runat="server" ID="WeeklyOffUP2">
                        <ContentTemplate>
                            <table style="margin-top: 10px; border: none; width: 80%; height: 45px" class="tblAction">
                                <tr>
                                    <td style="width: 50px; text-align: center;">
                                        <asp:CheckBox runat="server" ID="chkSelectAllWeekOffs" onclick="clickAllcheck(this);" CssClass="chkstyle" ClientIDMode="Static" />
                                    </td>
                                    <td id="tdRepeat2" style="width: 30px;">
                                        <asp:LinkButton runat="server" ID="lbtnReloadWeekOffs" CssClass="glyphicon glyphicon-repeat" Font-Size="16" ToolTip="Reload" OnClick="lbtnReloadWeekOffs_Click" />
                                    </td>
                                    <td id="tdDelete2" style="width: 30px;">
                                        <asp:LinkButton runat="server" ID="lbtnDeleteWeekOffs" CssClass="glyphicon glyphicon-trash" Font-Size="16" ToolTip="Delete" OnClientClick="return openDeleteConfirmModal('weeklyOffDeleteConfirmModal','gvWeekOffs','chkSelectWeekOffs')" />
                                    </td>
                                    <td id="tdFilter2" style="width: 30px;">
                                        <asp:LinkButton runat="server" ID="lnkWeeklyFilter" CssClass="glyphicon glyphicon-filter" Font-Size="18" ToolTip="Filter" OnClick="lnkWeeklyFilter_Click" />
                                    </td>
                                    <td>
                                        <table id="divWeeklyFilter" runat="server" clientidmode="static" class="downFilterTbl">
                                            <tr>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="ddlWeeklyOffFilterType" CssClass="form-control dropdown-list" AutoPostBack="true"  OnSelectedIndexChanged="ddlWeeklyOffFilterType_SelectedIndexChanged"  ClientIDMode="Static">
                                                        <asp:ListItem Value="ByDate" Text="By Date" />
                                                        <asp:ListItem Value="ByReason" Text="By Reason" />
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <table id="weeklyOffFilterDateContainer" runat="server">
                                                        <tr>
                                                            <td>From Date
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" ID="txtFromWeekOffs" CssClass="form-control datetimeCss" AutoCompleteType="Disabled" />
                                                            </td>
                                                            <td>To Date
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" ID="txtToWeekOffs" CssClass="form-control datetimeCss" AutoCompleteType="Disabled" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <table id="weeklyOffFilterReasonContainer" runat="server">
                                                        <tr>
                                                            <td>Reason
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList runat="server" ID="ddlWeeklyOffFilterReason" Style="min-width: 120px" CssClass="form-control dropdown-list">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <asp:Button runat="server" ID="btnApplyWeekOffsFilter" CssClass="Buttons" Text="Apply" OnClick="btnApplyWeekOffsFilter_Click" OnClientClick="return showLoader();" />
                                                </td>
                                                <td>
                                                    <asp:LinkButton runat="server" ID="lnkWeeklyFilterClose" CssClass="closeButton" ToolTip="Close" Text="&times;" OnClick="lnkWeeklyFilterClose_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnHodiday" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnWeeklyOff" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnAllDown" EventName="Click" />
                             <asp:AsyncPostBackTrigger ControlID="btnApplyWeekOffsFilter" EventName="Click" />
                             <asp:AsyncPostBackTrigger ControlID="lnkWeeklyFilter" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel runat="server" ID="WeeklyOffUP3">
                        <ContentTemplate>
                            <div id="divWeeklyOffsGrid" style="overflow: auto; padding-top: 2px; height: 65vh; width: 80%" class="griContainer">
                                <asp:GridView runat="server" ID="gvWeekOffs" AutoGenerateColumns="false" ShowHeader="true" ShowFooter="false" CssClass="grid" ClientIDMode="Static" AllowPaging="true" OnPageIndexChanging="gvWeekOffs_PageIndexChanging" OnPreRender="gvWeekOffs_PreRender" PageSize="100" EmptyDataText="No Data Found" EmptyDataRowStyle-CssClass="empty-row-style" ShowHeaderWhenEmpty="true" Style="width: 100%; height: 70%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="chkSelectWeekOffs" onclick="checkclick(this);" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date" AccessibleHeaderText="Date">
                                            <ItemTemplate>
                                                <asp:HiddenField runat="server" ID="hdnID" Value='<%# Eval("ID") %>' />
                                                <asp:Label runat="server" ID="lblWeekHoliday" Text='<%# Eval("FromDateTime") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" HorizontalAlign="center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Day" AccessibleHeaderText="Day">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblDay" Text='<%# Eval("Day") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" HorizontalAlign="center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Down Reason" AccessibleHeaderText="DownReason">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblOffReason" Text='<%# Eval("Reason") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="paginationCss" />
                                </asp:GridView>
                            </div>
                            <div class="modal  add-edit-modal" id="weeklyOffDeleteConfirmModal" role="dialog" style="min-width: 300px;">
                                <div class="modal-dialog " style="width: 450px">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <asp:Label class="modal-title" runat="server">Confirmation</asp:Label>
                                            <a data-dismiss="modal" class="glyphicon glyphicon-remove closeButton"></a>
                                        </div>
                                        <div class="modal-body">
                                            <span id="confirmationmessageText1" class="ConfirmText">Do you want to delete this record?</span>
                                        </div>

                                        <div class="modal-footer ">
                                            <asp:Button runat="server" Text="Yes" ID="btnDeleteWeeklyOffData" CssClass="btn AddEditBtn" OnClick="btnDeleteWeeklyOffData_Click" OnClientClick="return clearModalScreen();" />
                                            <input type="button" value="No" data-dismiss="modal" class="btn btn-danger CancelBtn" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnHodiday" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnWeeklyOff" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnAllDown" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnApplyWeekOffsFilter" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="lnkWeeklyFilter" EventName="Click" />
                             <asp:AsyncPostBackTrigger ControlID="lbtnReloadWeekOffs" EventName="Click" />
                            
                        </Triggers>
                    </asp:UpdatePanel>
                </div>

                <%------------AllDown  ----------------------%>
                <div id="tabAllDownContent" class="tab-pane fade">
                    <asp:UpdatePanel runat="server" ID="AllDownUP1">
                        <ContentTemplate>
                            <fieldset class="field-set">
                                <legend>&nbsp;<b>Filter By</b></legend>
                                <table class="filterTbl filter-field-table">
                                    <tr>
                                        <td style="padding-right: 6px;">
                                            <asp:Label runat="server">From
                                            </asp:Label>
                                        </td>
                                        <td style="padding-right: 15px;">
                                            <asp:TextBox runat="server" ID="txtFromAllDowns" CssClass="form-control datetimeCss" AutoCompleteType="Disabled" />
                                        </td>
                                        <td style="padding-right: 6px;">
                                            <asp:Label runat="server">To
                                            </asp:Label>
                                        </td>
                                        <td style="padding-right: 15px;">
                                            <asp:TextBox runat="server" ID="txtToAllDowns" CssClass="form-control datetimeCss" AutoCompleteType="Disabled" />
                                        </td>
                                        <td style="padding-right: 6px;">
                                            <asp:Label runat="server">Downs
                                            </asp:Label>
                                        </td>
                                        <td style="padding-right: 15px;">
                                            <asp:DropDownList runat="server" ID="ddlAllDowns" CssClass="form-control dropdown-list">
                                                <asp:ListItem Value="Holidays" Text="Holidays" />
                                                <asp:ListItem Value="WeeklyOff" Text="Weekly Offs" />
                                            </asp:DropDownList>
                                        </td>
                                        <td style="padding-right: 6px;">
                                            <asp:Button runat="server" ID="btnViewAllDowns" CssClass="Buttons" Text="View" OnClick="btnViewAllDowns_Click" />
                                        </td>

                                    </tr>
                                </table>
                            </fieldset>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnHodiday" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnWeeklyOff" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnAllDown" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel runat="server" ID="AllDownUP2">
                        <ContentTemplate>
                            <table style="margin-top: 10px; border: none; width: 80%; height: 45px" class="tblAction">
                                <tr>
                                    <td style="width: 30px;">
                                        <asp:CheckBox runat="server" ID="chkSelectAllDowns" onclick="clickAllcheck(this);" CssClass="chkstyle" ClientIDMode="Static" />&nbsp;&nbsp;&nbsp;
                                        <asp:LinkButton runat="server" ID="lnkAllDownReload" CssClass="glyphicon glyphicon-repeat" Font-Size="16" ToolTip="Reload" OnClick="lnkAllDownReload_Click" />&nbsp;&nbsp;
                                         <asp:LinkButton runat="server" ID="lnkAllDownDelete" CssClass="glyphicon glyphicon-trash" Font-Size="16" ToolTip="Delete" OnClientClick="return openDeleteConfirmModal('allDownDeleteConfirmModal','gvAllDowns','chkSelectDowns')" />
                                    </td>

                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnHodiday" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnWeeklyOff" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnAllDown" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel runat="server" ID="AllDownUP3">
                        <ContentTemplate>
                            <div id="divAllDownsGrid" style="overflow: auto; padding-top: 2px; height: 65vh; width: 80%" class="griContainer">

                                <asp:GridView runat="server" ID="gvAllDowns" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true" ShowFooter="false" CssClass="grid" ClientIDMode="Static" AllowPaging="true" OnPageIndexChanging="gvAllDowns_PageIndexChanging" Style="width: 100%; height: 70%" OnPreRender="gvAllDowns_PreRender" PageSize="100" EmptyDataText="No Data Found !!" EmptyDataRowStyle-CssClass="empty-row-style">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="chkSelectDowns" onclick="checkclick(this);" />
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Holiday" AccessibleHeaderText="Holiday">
                                            <ItemTemplate>
                                                <asp:HiddenField runat="server" ID="hdnID" Value='<%# Eval("ID") %>' />
                                                <asp:Label runat="server" ID="lblAllHolidays" Text='<%# Eval("FromDateTime") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="30%" ForeColor="Black" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Down Category" AccessibleHeaderText="DownCategory">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblAllDowns" Text='<%# Eval("DownType") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="20%" HorizontalAlign="center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Down Reason" AccessibleHeaderText="DownReason">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblAllReason" Text='<%# Eval("Reason") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="60%" ForeColor="Black" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <PagerStyle CssClass="paginationCss" />
                                </asp:GridView>
                            </div>
                             <div class="modal  add-edit-modal" id="allDownDeleteConfirmModal" role="dialog" style="min-width: 300px;">
                                <div class="modal-dialog " style="width: 450px">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <asp:Label class="modal-title" runat="server">Confirmation</asp:Label>
                                            <a data-dismiss="modal" class="glyphicon glyphicon-remove closeButton"></a>
                                        </div>
                                        <div class="modal-body">
                                            <span id="confirmationmessageText2" class="ConfirmText">Do you want to delete this record?</span>
                                        </div>

                                        <div class="modal-footer ">
                                            <asp:Button runat="server" Text="Yes" ID="btnDeleteAllDown" CssClass="btn AddEditBtn" OnClick="btnDeleteAllDown_Click" OnClientClick="return clearModalScreen();" />
                                            <input type="button" value="No" data-dismiss="modal" class="btn btn-danger CancelBtn" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                          
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnHodiday" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnWeeklyOff" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnAllDown" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>

            <%--------------Modals-----------------%>

            <div class="modal fade add-edit-modal" id="rejectedItemsModal" role="dialog" style="min-width: 200px;" data-backdrop="static" data-keyboard="false">
                <div class="modal-dialog " style="width: 50%;height:50%">
                    <div class="modal-content">
                        <div class="modal-header">

                            <h4 class="modal-title">Down Time already created for below list</h4>
                            <a data-dismiss="modal" class="glyphicon glyphicon-remove closeButton"></a>
                        </div>
                        <div class="modal-body" style="width: 100%">
                            <div id="rejectedItemsContainer">
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-danger CancelBtn">Close</button>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade add-edit-modal" id="warningModal" role="dialog" style="min-width: 300px;">
                <div class="modal-dialog" style="width: 50%">
                    <div class="modal-content">
                        <div class="modal-header">
                            <i class="glyphicon glyphicon-warning-sign modal-icons"></i>
                            <br />
                            <h4 class="modal-title">Warning</h4>
                        </div>
                        <div class="modal-body">
                            <span class="warning-modal-msg" id="lblWarningMsg">Reocrd Insertion Failed...!!</span>
                        </div>
                        <div class="modal-footer modalFooter modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-danger CancelBtn">Close</button>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <script>
        function clearModalScreen() {
            $(".modal-backdrop").removeClass("modal-backdrop in");
            return true;
        }

        $(".submenuData").click(function () {
             //debugger;
            // $('[id*=hdnScrollPos]').val(0);
            // $.blockUI({ message: '<img runat="server" src="Images/Loading.gif" />' });
            if ($("#mainBody").hasClass("light-mode")) {
               //  debugger;
                $(".submenuData").removeClass("selected-menu-style selected-Submenu").addClass("other-menu-style");
                $(".submenuData").closest('li').find('i').removeClass();
                $(this).removeClass("other-menu-style selected-Submenu").addClass("selected-menu-style");
                //  debugger;
            } else {
               // debugger;
                $(".submenuData").removeClass("selected-menu-style selected-Submenu").addClass("other-menu-style");
                $(".submenuData").closest('li').find('i').removeClass();
                $(this).removeClass("other-menu-style selected-Submenu").addClass("selected-menu-style");
            }

            $(this).closest('li').find('i').addClass("arrow up selected-Submenu-ArrowUp");
            submenu = $(this).attr('href');
            $("#activeMenu").val(submenu);
            $(submenu).addClass("in active");
            if (submenu == "#tabWeeklyOffContent") {
                __doPostBack('<%= btnWeeklyOff.UniqueID%>', '');
            }
            else if (submenu == "#tabAllDownContent") {
                __doPostBack('<%= btnAllDown.UniqueID%>', '');
            }
            else {
                __doPostBack('<%= btnHodiday.UniqueID%>', '');
            }
            localStorage.setItem("selectedPDTSubMenu", submenu);
            $('#hdnSubMenuData').val(submenu);
            hideColumnFilterPanels(event, 'tdColumnFilters', 'panelColumnFilter');
        });

        function setActiveSubmenuValue() {
            // debugger;
            var lilist = $("#masterul li");
            for (let i = 0; i < lilist.length; i++) {
                let li = lilist[i];
                let display = $(li).css('display');
                //debugger;
                if (display == "block") {
                    // debugger;
                    localStorage.setItem("selectedPDTSubMenu", $(li).find('a').attr('href'));
                    activeSubMenu();
                    enterBlock = true;
                    break;
                }
            }
            localStorage.setItem("selectedPPSubMenu", "#processParamMenu0");
        }

        function activeSubMenu() {
            if (localStorage.getItem("selectedPDTSubMenu")) {
               // debugger;
                if (localStorage.getItem("selectedPDTSubMenu")) {
                   // debugger;
                    submenu = localStorage.getItem("selectedPDTSubMenu");
                }
                $(submenu).addClass("in active");
                $("a[href$='" + submenu + "']").removeClass("selected-menu-style").addClass("selected-Submenu");
               // $("a[href$='" + submenu + "']").closest('li').find('i').addClass("arrow up selected-Submenu-ArrowUp");
                $('#hdnSubMenuData').val(submenu);
                // $(this).closest('li').css("background-color", "#EDEEF5");
            } else {
              //  debugger;
                $(".sub-sub-menudiv").removeClass("in active");
            }
        }

        function showLoaderWithValidation(txt) {
            //debugger;
            if ($('#' + txt).val().trim() == "") {
                WarningToastr("Please Enter Down Reason!");
                return false;
            }
            // $.blockUI({ message: '<img runat="server" src="Images/Loading.gif" />' });
            return true;
        }

        function showLoader() {
            //debugger;
            // $.blockUI({ message: '<img runat="server" src="Images/Loading.gif" />' });
            return true;
        }

        function restrictSpecialCharacter(evt) {

            var charCode = (evt.which) ? evt.which : evt.keyCode;
            var pos = evt.target.selectionStart;
            if (pos == 0) {
                if ((charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122)) {
                    return true;
                }
                else {
                    return false;
                }
            }
            if (charCode == 45 || charCode == 95) {
                return true;
            }
            if ((charCode >= 33 && charCode <= 47)) {
                return false;
            } else if ((charCode >= 58 && charCode <= 64)) {
                return false;
            } else if ((charCode >= 91 && charCode <= 96)) {
                return false;
            }
            return true;
        }

        function openWarningModal(msg) {
           // $('#lblWarningMsg').text(msg);
            $('[id*=warningModal]').modal('show');
        }

        function closeDeletePopup() {
            $('[id*=holidayListConfirmModal]').modal('hide');
        }

        function openConfirmModal() {
            //  debugger;
            let selectForDelete = false;
            for (let i = 1; i < $("#gvHolidays tr").length; i++) {
                let tr = $("#gvHolidays tr")[i];
                if ($(tr).find("#chkSelectHoliday").prop("checked")) {
                    selectForDelete = true;
                    break;
                }
            }
            if (selectForDelete) {
                $('[id*=holidayListConfirmModal]').modal('show');
            } else {
                openWarningModal("Select records for delete.");
                //WarningToastr("PleaseSelect records for delete!");
            }
            return false;
        }

        function openDeleteConfirmModal(modalID, gridID, chkID) {
            //$('[id*=' + id + ']').modal('show');


            let selectForDelete = false;
            for (let i = 1; i < $("#" + gridID + " tr").length; i++) {
                let tr = $("#" + gridID + " tr")[i];
                if ($(tr).find("#" + chkID).prop("checked")) {
                    selectForDelete = true;
                    break;
                }
            }
            if (selectForDelete) {
                $('[id*=' + modalID + ']').modal('show');
            } else {
                WarningToastr("Please Select records for delete!");
               // openWarningModal("Select records for delete.");
            }

            return false;
        }

        function closeDeleteConfirmModal(id) {
            $('[id*=' + id + ']').modal('hide');
            return false;
        }

        function displayRejectedItems() {
            $.ajax({
                async: false,
                type: "POST",
                url: "PlannedDownTime.aspx/getPDTRejectedData",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var itemData = response.d;
                    $('#rejectedItemsContainer').empty();
                    if (itemData.length > 0) {
                        var appendString = "<table class='grid'><tr><th>Start Time</th><th>End Time</th><th>Down Reason</th></tr>";
                        for (let i = 0; i < itemData.length; i++) {
                            appendString += '<tr><td>' + itemData[i].FromDateTime + '</td><td>' + itemData[i].ToDateTime + '</td><td>' + itemData[i].Reason + '</td></tr>'
                        }
                        appendString += "</table>";
                        $('#rejectedItemsContainer').append(appendString);
                        $('[id*=rejectedItemsModal]').modal('show');
                    }
                },
                error: function (jqXHR, textStatus, err) {
                    alert('Error: ' + err);
                }
            });
        }

        function clickAllcheck(obj) {
            // debugger;
            var grid = null;
            if (obj == document.getElementById("chkSelectAllHoliday")) {
                grid = document.getElementById("gvHolidays");
            }
            else if (obj == document.getElementById("chkSelectAllWeekOffs")) {
                grid = document.getElementById("gvWeekOffs");
            }
            else if (obj == document.getElementById("chkSelectAllDowns")) {
                grid = document.getElementById("gvAllDowns");
            }
            if (obj.checked) {
                if (grid.rows.length > 0) {
                    for (i = 1; i < grid.rows.length; i++) {
                        cell = grid.rows[i].cells[0];
                        for (j = 0; j < cell.childNodes.length; j++) {

                            if (cell.childNodes[j].type == "checkbox") {
                                cell.childNodes[j].checked = true;
                            }
                        }
                        // grid.rows[i].style.backgroundColor = "#dbdde0";
                    }
                }
            }
            else {
                if (grid.rows.length > 0) {
                    for (i = 1; i < grid.rows.length; i++) {
                        cell = grid.rows[i].cells[0];
                        for (j = 0; j < cell.childNodes.length; j++) {
                            if (cell.childNodes[j].type == "checkbox") {
                                cell.childNodes[j].checked = false;
                            }
                        }
                        // grid.rows[i].style.backgroundColor = "#EDEEF5";
                    }
                }
            }
        }

        function checkclick(obj) {
            //   debugger;
            var row = obj.parentNode.parentNode;
            //if (obj.checked) {
            //    row.style.backgroundColor = "#dbdde0";
            //}
            //else {
            //    row.style.backgroundColor = "#EDEEF5";
            //}
            var GridView = row.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var checked = true;
                if (inputList[i].type == "checkbox") {
                    if (!inputList[i].checked) {
                        checked = false;
                        break;
                    }
                }
            }
            var selectAllCheckbox = null;
            if (GridView == document.getElementById("gvHolidays")) {
                selectAllCheckbox = document.getElementById("chkSelectAllHoliday");
            }
            else if (GridView == document.getElementById("gvWeekOffs")) {
                selectAllCheckbox = document.getElementById("chkSelectAllWeekOffs");
            }
            else if (GridView == document.getElementById("gvAllDowns")) {
                selectAllCheckbox = document.getElementById("chkSelectAllDowns");
            }

            selectAllCheckbox.checked = checked;
        }

        function resize() {
           //var heights = window.innerHeight - 370;
           //document.getElementById("divHolidayGrid").style.height = heights + "px";
           //document.getElementById("divWeeklyOffsGrid").style.height = heights + "px";
           //document.getElementById("divAllDownsGrid").style.height = heights + "px";
        }

        function setDateTimeToControls() {
            // debugger;
            $('[id$=txtFromHolidayYear]').datepicker({
                minViewMode: 2,
                format: 'yyyy',
                todayHighlight: true,
                autoclose: true,
                language: 'en-US',
            });
            $('[id$=txtToYear]').datepicker({
                minViewMode: 2,
                format: 'yyyy',
                todayHighlight: true,
                autoclose: true,
                language: 'en-US',
            });
            $('[id$=txtFilterByDateFrom]').datepicker({
                format: 'dd-mm-yyyy',
                todayHighlight: true,
                useCurrent: false,
                locale: 'en-US'
            });
            $('[id$=txtFilterByDateTo]').datepicker({
                format: 'dd-mm-yyyy',
                todayHighlight: true,
                useCurrent: false,
                locale: 'en-US'
            });

            $('[id$=txtFromWeeklyOffs]').datepicker({
                format: 'dd-mm-yyyy',
                useCurrent: false,
                todayHighlight: true,
                locale: 'en-US'
            });

            $('[id$=txtToWeeklyOffs]').datepicker({
                format: 'dd-mm-yyyy',
                useCurrent: false,
                todayHighlight: true,
                locale: 'en-US'
            });
            $('[id$=txtFromWeekOffs]').datepicker({
                format: 'dd-mm-yyyy',
                useCurrent: false,
                todayHighlight: true,
                locale: 'en-US'
            });

            $('[id$=txtToWeekOffs]').datepicker({
                format: 'dd-mm-yyyy',
                useCurrent: false,
                todayHighlight: true,
                locale: 'en-US'
            });

            $('[id$=txtFromAllDowns]').datepicker({
                format: 'dd-mm-yyyy',
                useCurrent: false,
                todayHighlight: true,
                locale: 'en-US'
            });
            $('[id$=txtToAllDowns]').datepicker({
                format: 'dd-mm-yyyy',
                useCurrent: false,
                todayHighlight: true,
                locale: 'en-US'
            });
            $('[id$=ddlDays]').multiselect({
                includeSelectAllOption: true,
                //  enableFiltering: true,
                //   enableCaseInsensitiveFiltering: true
            });
            $('[id$=ddlWeek]').multiselect({
                includeSelectAllOption: true,
                //  enableFiltering: true,
                // enableCaseInsensitiveFiltering: true
            });
        }

        $(document).ready(function () {

            var d = new Date();
            console.log("date =" + d);
            resize();
            window.onresize = function () {
                resize();
            };
            setDateTimeToControls();
            // debugger;


             $('#gvHolidays tr:not(:last-child)').click(function (event) {
                // debugger;
                if ($(event.target).is(':checkbox')) {
                    return;
                }
                var chkboxSelection = $(this).find('input:checkbox')[0].checked;
                if (chkboxSelection == true) {
                    // $(this).closest('tr').css("background-color", "#EDEEF5");
                    $(this).find('input:checkbox')[0].checked = false;

                }
                else if (chkboxSelection == false) {
                    // $(this).closest('tr').css("background-color", "#dbdde0");
                    $(this).find('input:checkbox')[0].checked = true;

                }
                checkclick($(this).find('input:checkbox')[0]);

            });
            $('#gvWeekOffs tr:not(:last-child)').on('click', function (event) {
                if ($(event.target).is(':checkbox')) {
                    return;
                }
                var chkboxSelection = $(this).find('input:checkbox')[0].checked;
                if (chkboxSelection == true) {
                    // $(this).closest('tr').css("background-color", "#EDEEF5");
                    $(this).find('input:checkbox')[0].checked = false;

                }
                else if (chkboxSelection == false) {
                    //   $(this).closest('tr').css("background-color", "#dbdde0");
                    $(this).find('input:checkbox')[0].checked = true;

                }
                checkclick($(this).find('input:checkbox')[0]);
            });
            $('#gvAllDowns tr:not(:last-child)').on('click', function (event) {
                if ($(event.target).is(':checkbox')) {
                    return;
                }
                var chkboxSelection = $(this).find('input:checkbox')[0].checked;
                if (chkboxSelection == true) {
                    //  $(this).closest('tr').css("background-color", "#EDEEF5");
                    $(this).find('input:checkbox')[0].checked = false;

                }
                else if (chkboxSelection == false) {
                    // $(this).closest('tr').css("background-color", "#dbdde0");
                    $(this).find('input:checkbox')[0].checked = true;

                }
                checkclick($(this).find('input:checkbox')[0]);
            });

            activeSubMenu();
        });

        var bigDivAllDown = document.getElementById('divAllDownsGrid');
        bigDivAllDown.onscroll = function () {
            $('[id*=hdnScrollPos]').val(bigDivAllDown.scrollTop);
        }

        var bigDivWeekly = document.getElementById('divWeeklyOffsGrid');
        bigDivWeekly.onscroll = function () {
            $('[id*=hdnScrollPos]').val(bigDivWeekly.scrollTop);
        }

        var bigDivHoliday = document.getElementById('divHolidayGrid');
        bigDivHoliday.onscroll = function () {
            $('[id*=hdnScrollPos]').val(bigDivHoliday.scrollTop);
        }

        window.onload = function () {
            bigDivAllDown.scrollTop = $('[id*=hdnScrollPos]').val();
            bigDivWeekly.scrollTop = $('[id*=hdnScrollPos]').val();
            bigDivHoliday.scrollTop = $('[id*=hdnScrollPos]').val();
        }

        /*-------------------------------------- Column Filter ---------------------------------------*/
        function showColumnFilterPanel(element, evt) {
            showhideColumnFilterPanels_ClickedPosition_WithGivenLeftTop(element, evt, 'panelColumnFilter', '', '65');
        }

        function hideColumnFilterPanels(evt, tdid, panelid) {
            if ($("#" + panelid).css('visibility') == "visible") {
                $("#" + tdid).toggleClass('highlight-selected-icon');
                $("#" + panelid).toggleClass("show-dropdown-menu");
            }
            evt.stopPropagation();
        }

        function selectAllColumnClick() {
            if ($("#cbColumnSelectAll").prop('checked')) {
                for (let i = 0; i < $("[id*=cblColumnSelector] tr input").length; i++) {
                    let input = $("[id*=cblColumnSelector] tr input")[i];
                    $(input).prop('checked', true);
                }
            } else {
                for (let i = 0; i < $("[id*=cblColumnSelector] tr input").length; i++) {
                    let input = $("[id*=cblColumnSelector] tr input")[i];
                    $(input).prop('checked', false);
                }
            }
        }

        function columnSelectionChange() {
            let checkedrows = $('#cblColumnSelector input:checked').length;
            let totalrows = $('#cblColumnSelector input').length;

            if (totalrows == checkedrows) {
                $("#cbColumnSelectAll").prop('checked', true);
            } else {
                $("#cbColumnSelectAll").prop('checked', false);
            }
        }

        $('#cblColumnSelector').change(function (event) {
            columnSelectionChange();
        });

        document.getElementById("headerName").textContent = "Planned DownTime";

        $(".dropdown-container3").css("display", "block");
        var user = "<%= (String)Session["username"] %>";
        $("#lblUserID").text("Hi " + user);


        /*------------------------------- Column Fileter End -----------------------------------*/

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
          //  debugger;
            var bigDivAllDown = document.getElementById('divAllDownsGrid');
            var bigDivWeekly = document.getElementById('divWeeklyOffsGrid');
            var bigDivHoliday = document.getElementById('divHolidayGrid');
            $(document).ready(function () {

                // $.unblockUI({}); $('.ajax-loader').hide();
                var d = new Date();
                console.log("date =" + d);
                //alert(d);
               // debugger;
                setDateTimeToControls();
                bigDivAllDown.scrollTop = $('[id*=hdnScrollPos]').val();
                bigDivWeekly.scrollTop = $('[id*=hdnScrollPos]').val();
                bigDivHoliday.scrollTop = $('[id*=hdnScrollPos]').val();
                activeSubMenu();
            });
            $(".dropdown-container3").css("display", "block");
            
            $(".submenuData").click(function () {
               //  debugger;
              //  $('[id*=hdnScrollPos]').val(0);
                // $.blockUI({ message: '<img runat="server" src="Images/Loading.gif" />' });
                if ($("#mainBody").hasClass("light-mode")) {
                   // debugger;
                   $(".submenuData").removeClass("selected-menu-style selected-Submenu").addClass("other-menu-style");
                    $(".submenuData").closest('li').find('i').removeClass();
                    $(this).removeClass("other-menu-style selected-Submenu").addClass("selected-menu-style");
                    //   $(this).closest('li').css("background-color", "#EDEEF5");
                } else {
                 //   debugger;
                    $(".submenuData").removeClass("selected-menu-style selected-Submenu").addClass("other-menu-style");
                    $(".submenuData").closest('li').find('i').removeClass();
                    $(this).removeClass("other-menu-style selected-Submenu").addClass("selected-menu-style");
                }

               // $(this).closest('li').find('i').addClass("arrow up selected-Submenu-ArrowUp");
                submenu = $(this).attr('href');
                $("#activeMenu").val(submenu);
                $(submenu).addClass("in active");
                if (submenu == "#tabWeeklyOffContent") {
                   // debugger;
                  //  $("[id$=tabWeeklyOffContent]").css("display", "block");
                    __doPostBack('<%= btnWeeklyOff.UniqueID%>', '');
                }
                else if (submenu == "#tabAllDownContent") {
                   //   debugger;
                  // $("[id$=tabAllDownContent]").css("display", "block");
                    __doPostBack('<%= btnAllDown.UniqueID%>', '');
                }
                else {
                  //  debugger;
                  //  $("[id$=tabHolidayContent]").css("display", "block");
                    __doPostBack('<%= btnHodiday.UniqueID%>', '');
                }
                localStorage.setItem("selectedPDTSubMenu", submenu);
                $('#hdnSubMenuData').val(submenu);
                //  hideColumnFilterPanels(event, 'tdColumnFilters', 'panelColumnFilter');
            });

            bigDivAllDown.onscroll = function () {
                $('[id*=hdnScrollPos]').val(bigDivAllDown.scrollTop);
            }

            bigDivWeekly.onscroll = function () {
                $('[id*=hdnScrollPos]').val(bigDivWeekly.scrollTop);
            }

            bigDivHoliday.onscroll = function () {
                $('[id*=hdnScrollPos]').val(bigDivHoliday.scrollTop);
            }
            window.onload = function () {

                bigDivAllDown.scrollTop = $('[id*=hdnScrollPos]').val();
                bigDivWeekly.scrollTop = $('[id*=hdnScrollPos]').val();
                bigDivHoliday.scrollTop = $('[id*=hdnScrollPos]').val();
            }

            resize();
            window.onresize = function () {
                resize();
            };

            $('#gvHolidays tr:not(:last-child)').click(function (event) {
                if ($(event.target).is(':checkbox')) {
                    return;
                }
                var chkboxSelection = $(this).find('input:checkbox')[0].checked;
                if (chkboxSelection == true) {
                    // $(this).closest('tr').css("background-color", "#EDEEF5");
                    $(this).find('input:checkbox')[0].checked = false;

                }
                else if (chkboxSelection == false) {
                    // $(this).closest('tr').css("background-color", "#dbdde0");
                    $(this).find('input:checkbox')[0].checked = true;

                }
                checkclick($(this).find('input:checkbox')[0]);
            });

            $('#gvWeekOffs tr:not(:last-child)').on('click', function (event) {
                if ($(event.target).is(':checkbox')) {
                    return;
                }
                var chkboxSelection = $(this).find('input:checkbox')[0].checked;
                if (chkboxSelection == true) {
                    // $(this).closest('tr').css("background-color", "#EDEEF5");
                    $(this).find('input:checkbox')[0].checked = false;

                }
                else if (chkboxSelection == false) {
                    //$(this).closest('tr').css("background-color", "#dbdde0");
                    $(this).find('input:checkbox')[0].checked = true;

                }
                checkclick($(this).find('input:checkbox')[0]);
            });

            $('#gvAllDowns tr:not(:last-child)').on('click', function (event) {
                if ($(event.target).is(':checkbox')) {
                    return;
                }
                var chkboxSelection = $(this).find('input:checkbox')[0].checked;
                if (chkboxSelection == true) {
                    //  $(this).closest('tr').css("background-color", "#EDEEF5");
                    $(this).find('input:checkbox')[0].checked = false;

                }
                else if (chkboxSelection == false) {
                    //  $(this).closest('tr').css("background-color", "#dbdde0");
                    $(this).find('input:checkbox')[0].checked = true;

                }
                checkclick($(this).find('input:checkbox')[0]);

            });

            /*-------------------------------------- Column Filter ---------------------------------------*/

            function showColumnFilterPanel(element, evt) {
                showhideColumnFilterPanels_ClickedPosition_WithGivenLeftTop(element, evt, 'panelColumnFilter', '', '65');
            }

            function hideColumnFilterPanels(evt, tdid, panelid) {
                if ($("#" + panelid).css('visibility') == "visible") {
                    $("#" + tdid).toggleClass('highlight-selected-icon');
                    $("#" + panelid).toggleClass("show-dropdown-menu");
                }
                evt.stopPropagation();
            }

            function selectAllColumnClick() {
                if ($("#cbColumnSelectAll").prop('checked')) {
                    for (let i = 0; i < $("[id*=cblColumnSelector] tr input").length; i++) {
                        let input = $("[id*=cblColumnSelector] tr input")[i];
                        $(input).prop('checked', true);
                    }
                } else {
                    for (let i = 0; i < $("[id*=cblColumnSelector] tr input").length; i++) {
                        let input = $("[id*=cblColumnSelector] tr input")[i];
                        $(input).prop('checked', false);
                    }
                }
            }

            function columnSelectionChange() {
                let checkedrows = $('#cblColumnSelector input:checked').length;
                let totalrows = $('#cblColumnSelector input').length;

                if (totalrows == checkedrows) {
                    $("#cbColumnSelectAll").prop('checked', true);
                } else {
                    $("#cbColumnSelectAll").prop('checked', false);
                }
            }

            $('#cblColumnSelector').change(function (event) {
                columnSelectionChange();
            });

            /*------------------------------- Column Fileter End -----------------------------------*/
        });

    </script>

</asp:Content>

