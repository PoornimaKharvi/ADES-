<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PDT.aspx.cs" Inherits="ADES_22.PDT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="Scripts/Toaster/toastr.min.js"></script>
    <link href="Scripts/Toaster/toastr.min.css" rel="stylesheet" />
    <script src="Scripts/DateTimePicker/bootstrap-datepicker.js"></script>
    <link href="Scripts/DateTimePicker/bootstrap-datepicker3.css" rel="stylesheet" />

    <script src="Scripts/DateTimePicker/bootstrap-datepicker.en-IE.min.js"></script>
    <script src="Scripts/DateTimePicker/bootstrap-datepicker.zh-CN.min.js"></script>
    <script src="Scripts/DateTimePickerFor331/moment.js"></script>
    <script src="Scripts/DateTimePickerFor331/bootstrap-datetimepicker.min.js"></script>
    <link href="Scripts/DateTimePickerFor331/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <link href="Scripts/MultiSelectDropdown/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="Scripts/MultiSelectDropdown/bootstrap-multiselect.js"></script>

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
            background: #f7f7f7 ;
        }

            .griContainer table tr:last-child td a {
                font-size: 20px;
                color: white;
            }

            .griContainer table tr:last-child td span {
                font-size: 23px;
                color: #0c8dfb;
            }
            
        .tab-content .filterTbl tr td
        {
            padding:7px;
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
            background-color:lightgrey;
            height:50px;
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

   <%--  <div style="width: 95%; margin: auto;padding-right:0px;height: 88vh">                --%>

        <asp:HiddenField runat="server" ID="hdnScrollPos" ClientIDMode="Static" />

        <div ><%--style="float: left; margin-top: 10px; width: 100%"--%>
            <asp:UpdatePanel runat="server" ID="PDtUP2">
                <ContentTemplate>
                    <div class="navbar-collapse collapse" style="height: 42px !important;padding:0px">
                        <ul id="masterul" class="nav navbar-nav nextPrevious submenus-style">
                            <li id="tabHoliday" runat="server"><a runat="server" class="submenuData" id="A15" clientidmode="static" data-toggle="tab" href="#tabHolidayContent">Holidays</a>
                                <i></i>
                            </li>
                            <li id="tabWeeklyOff" runat="server"><a runat="server" class="submenuData" id="A1" clientidmode="static" data-toggle="tab" href="#tabWeeklyOffContent">Weekly Offs</a>
                                <i></i>
                            </li>
                            <li id="tabDailyDown" runat="server"><a runat="server" class="submenuData" id="A2" clientidmode="static" data-toggle="tab" href="#tabDailyDownContent">Daily Downs</a>
                                <i></i>
                            </li>
                            <%--<li id="tabShiftDown" runat="server"><a runat="server" class="submenuData" id="A3" clientidmode="static" data-toggle="tab" href="#tabShiftDownContent">Shift Downs</a>
                                <i></i>
                            </li>--%>
                            <li id="tabAllDown" runat="server"><a runat="server" class="submenuData" id="A4" clientidmode="static" data-toggle="tab" href="#tabAllDownContent">All Downs</a>
                                <i></i>
                            </li>
                        </ul>
                    </div>
                    <div style="display: none">
                        <asp:Button runat="server" ID="btnHodiday" />
                        <asp:Button runat="server" ID="btnWeeklyOff" />
                        <asp:Button runat="server" ID="btnDailyDown" />
                        <asp:Button runat="server" ID="btnShiftDown" />
                        <asp:Button runat="server" ID="btnAllDown" />
                    </div>
                </ContentTemplate>
                <Triggers>
                    <%--    <asp:AsyncPostBackTrigger ControlID="btnDeleteRecordsYes" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnDeleteWeeklyOffData" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnDeleteShiftDownData" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnDeleteAllDown" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnDeleteDailyDownData" EventName="Click" />--%>
                </Triggers>
            </asp:UpdatePanel>

            <div class="tab-content themetoggle" id="processParamContainer" style="overflow: auto; width: 100%; margin: -10px auto;">

                <div id="tabHolidayContent" class="tab-pane fade">
                    <asp:UpdatePanel ID="holidayUP1" runat="server">
                        <ContentTemplate>

                            <fieldset class="field-set">
                                <legend>&nbsp;<b>Filter By</b></legend>
                                <table class="filterTbl filter-field-table">
                                    <tr >
                                        <td>From
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFromYear" runat="server" CssClass="form-control datetimeCss" placeholder="Year" AutoCompleteType="Disabled"></asp:TextBox>
                                        </td>
                                        <td>To
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtToYear" runat="server" CssClass="form-control datetimeCss" placeholder="Year" AutoCompleteType="Disabled"></asp:TextBox>
                                        </td>
                                        <td>Month
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlMonth" CssClass="form-control dropdown-list" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged" AutoPostBack="true" ClientIDMode="Static">
                                            </asp:DropDownList>
                                        </td>
                                        <td>Day
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlDay" CssClass="form-control dropdown-list">
                                            </asp:DropDownList>
                                        </td>
                                        <td>Reason</td>
                                        <td>
                                            <input id="ddlReason" runat="server" class="form-control" onkeypress="return restrictSpecialCharacter(event);" clientidmode="static">
                                        </td>
                                        <td>
                                            <asp:Button runat="server" ID="btnSaveHoliday" Text="Save" CssClass="Buttons" OnClick="btnSaveHoliday_Click" OnClientClick="return showLoaderWithValidation('ddlReason');" />
                                             <asp:Button runat="server" ID="btnExportHoliday" Text="Export" CssClass="Buttons" OnClick="btnExportHoliday_Click"  />
                                        </td>
                                    </tr>
                                </table>

                            </fieldset>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnHodiday" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnWeeklyOff" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnDailyDown" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnShiftDown" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnAllDown" EventName="Click" />
                            <asp:PostBackTrigger ControlID="btnExportHoliday" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="holidayUP2" runat="server">
                        <ContentTemplate>
                            <table style="margin-top: 10px; border: none; width: 100%;" class="tblAction">
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
                                                                <asp:TextBox ID="txtFilterByDateFrom" ClientIDMode="Static" runat="server" CssClass="form-control datetimeCss" AutoCompleteType="Disabled" placeholder="Date"></asp:TextBox></td>
                                                            <td>
                                                                <asp:Label runat="server" ID="Label1" ClientIDMode="Static" Text="To Date" Style="font-size: 14px; vertical-align: middle; margin-left: 10px;" /></td>
                                                            <td>
                                                                <asp:TextBox ID="txtFilterByDateTo" ClientIDMode="Static" runat="server" CssClass="form-control datetimeCss" AutoCompleteType="Disabled"></asp:TextBox></td>
                                                        </tr>

                                                    </table>
                                                </td>
                                                <td>
                                                    <table id="holidayReasonContainer" runat="server">
                                                        <tr>
                                                            <td>Reason</td>
                                                            <td>
                                                                <asp:DropDownList runat="server" ID="ddlFilterByReason" ClientIDMode="Static" CssClass="form-control dropdown-list">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <%--<td>
                                                    <table id="holidayMachineFilterContainer" runat="server">
                                                        <tr>
                                                            <td>Machine
                                                            </td>
                                                            <td>
                                                                <asp:ListBox ID="ddlFilterByMachines" runat="server" SelectionMode="Multiple" ClientIDMode="Static" Style="margin-left: 10px; width: 200px; display: inline-block;" Rows="1"></asp:ListBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>--%>
                                                <td>
                                                    <asp:Button runat="server" ID="btnApplyHoliday" OnClick="btnApplyHoliday_Click" CssClass="Buttons" ClientIDMode="Static" Text="Apply" Style="margin-left: 10px; display: inline-block;" OnClientClick="return showLoader();" />
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
                            <asp:AsyncPostBackTrigger ControlID="btnDailyDown" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnShiftDown" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnAllDown" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="holidayUP3" runat="server">
                        <ContentTemplate>
                            <div id="divHolidayGrid" style="overflow: auto; color: black;" class="grid griContainer">
                                <asp:GridView runat="server" ID="gvHolidays" AutoGenerateColumns="false" ShowHeader="true" ShowFooter="false" CssClass="P1Table" ClientIDMode="Static" AllowPaging="true" OnPageIndexChanging="gvHolidays_PageIndexChanging" OnPreRender="gvHolidays_PreRender" PageSize="100" EmptyDataText="No Data Found">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="chkSelectHoliday" ClientIDMode="Static" onclick="checkclick(this)" />
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" HorizontalAlign="Left" ForeColor="Black" />
                                            <HeaderStyle Width="50px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date" AccessibleHeaderText="Date">
                                            <ItemTemplate>
                                                <asp:HiddenField runat="server" ID="hdnDate" Value='<%# Eval("Date") %>' />
                                                <asp:Label runat="server" ID="lblHoliday" Text='<%# Eval("Holiday") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="25%" ForeColor="Black" />
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

                            <div class="modal fade" id="holidayListConfirmModal" role="dialog">
                                <div class="modal-dialog modal-dialog-centered">
                                    <div class="modal-content modalContent confirm-modal-content">
                                        <div class="modal-header modalHeader confirm-modal-header">
                                            <i class="glyphicon glyphicon glyphicon glyphicon-question-sign modal-icons"></i>
                                            <br />
                                            <h4 class="confirm-modal-title">Confirmation!</h4>
                                            <br />
                                            <span class="confirm-modal-msg">Are you sure you want to delete Records?</span>
                                        </div>
                                        <div class="modal-footer modalFooter modal-footer">
                                            <asp:Button runat="server" Text="Yes" ID="btnDeleteRecordsYes" CssClass="confirm-modal-btn" OnClick="btnDeleteRecordsYes_Click" OnClientClick="return clearModalScreen();" />
                                            <input type="button" value="No" data-dismiss="modal" class="confirm-modal-btn" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnHodiday" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnWeeklyOff" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnDailyDown" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnShiftDown" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnAllDown" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnSaveHoliday" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>
                <%------WeeklyOffContents ------%>
                <div id="tabWeeklyOffContent" class="tab-pane fade">
                    <asp:UpdatePanel runat="server" ID="WeeklyOffUP1">
                        <ContentTemplate>
                            <fieldset class="field-set">
                                <legend>&nbsp;<b>Filter By</b></legend>
                                <table class="filterTbl filter-field-table">
                                    <tr>
                                        <td>From</td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtFromWeeklyOffs" CssClass="form-control datetimeCss" AutoCompleteType="Disabled" ClientIDMode="Static" />
                                        </td>
                                        <td>To
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtToWeeklyOffs" CssClass="form-control datetimeCss" AutoCompleteType="Disabled" />
                                        </td>
                                        <td>Days
                                        </td>
                                        <td>
                                            <asp:ListBox ID="ddlDays" runat="server" SelectionMode="Multiple" ClientIDMode="Static" CssClass="dropdown-list">
                                                <asp:ListItem Text="Sun" />
                                                <asp:ListItem Text="Mon" />
                                                <asp:ListItem Text="Tue" />
                                                <asp:ListItem Text="Wed" />
                                                <asp:ListItem Text="Thu" />
                                                <asp:ListItem Text="Fri" />
                                                <asp:ListItem Text="Sat" />
                                            </asp:ListBox>
                                        </td>
                                        <td>Week
                                        </td>
                                        <td>

                                            <asp:ListBox ID="ddlWeek" runat="server" SelectionMode="Multiple" ClientIDMode="Static" CssClass="from-control dropdown-list">
                                                <asp:ListItem Value="1" Text="1st Week" />
                                                <asp:ListItem Value="2" Text="2nd Week" />
                                                <asp:ListItem Value="3" Text="3rd Week" />
                                                <asp:ListItem Value="4" Text="4th Week" />
                                                <asp:ListItem Value="5" Text="5th Week" />
                                            </asp:ListBox>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtWeeklyOffReason" CssClass="form-control" onkeypress="return restrictSpecialCharacter(event);" ClientIDMode="Static"> </asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button runat="server" ID="btnSaveWeekOffs" Text="Save" CssClass="Buttons" OnClick="btnSaveWeekOffs_Click" OnClientClick="return showLoaderWithValidation('txtWeeklyOffReason');" />
                                            <asp:Button runat="server" ID="btnExportWeekOff" Text="Export" CssClass="Buttons" OnClick="btnExportWeekOff_Click" />
                                        </td>
                                       <%--  <td class="td-filters-style" onclick="showColumnFilterPanel(this, event)">
                                        <i class="glyphicon glyphicon-filter" style="font-size: 17px;"></i>
                                    </td>--%>
                                    </tr>
                                </table>
                            </fieldset>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnHodiday" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnWeeklyOff" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnDailyDown" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnShiftDown" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnAllDown" EventName="Click" />
                            <asp:PostBackTrigger ControlID="btnExportWeekOff" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel runat="server" ID="WeeklyOffUP2">
                        <ContentTemplate>
                            <table style="margin-top: 10px; border: none; width: 100%;" class="tblAction">
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
                                                    <asp:DropDownList runat="server" ID="ddlWeeklyOffFilterType" CssClass="form-control dropdown-list" AutoPostBack="true" OnSelectedIndexChanged="ddlWeeklyOffFilterType_SelectedIndexChanged">
                                                        <asp:ListItem Value="ByDate" Text="By Date" />
                                                        <asp:ListItem Value="ByReason" Text="By Reason" />
                                                       <%-- <asp:ListItem Value="ByMachine" Text="By Machine" />--%>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <table id="weeklyOffFilterDateContainer" runat="server">
                                                        <tr>
                                                            <td>From
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" ID="txtFromWeekOffs" CssClass="form-control datetimeCss" AutoCompleteType="Disabled" />
                                                            </td>
                                                            <td>To
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" ID="txtToWeekOffs" CssClass="form-control datetimeCss" AutoCompleteType="Disabled" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <%--<td>
                                                    <table id="weeklyOffFilterMachineContainer" runat="server">
                                                        <tr>
                                                            <td>Machine
                                                            </td>
                                                            <td>
                                                                <asp:ListBox ID="lbFilterWeekByMachine" runat="server" SelectionMode="Multiple" ClientIDMode="Static" CssClass="dropdown-list"></asp:ListBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>--%>
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
                            <asp:AsyncPostBackTrigger ControlID="btnDailyDown" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnShiftDown" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnAllDown" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel runat="server" ID="WeeklyOffUP3">
                        <ContentTemplate>
                            <div id="divWeeklyOffsGrid" style="overflow: auto; height: 60vh" class="griContainer">
                                <asp:GridView runat="server" ID="gvWeekOffs" AutoGenerateColumns="false" ShowHeader="true" ShowFooter="false" CssClass="P1Table" ClientIDMode="Static" AllowPaging="true" OnPageIndexChanging="gvWeekOffs_PageIndexChanging" OnPreRender="gvWeekOffs_PreRender" PageSize="100" EmptyDataText="No Data Found">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="chkSelectWeekOffs" onclick="checkclick(this);" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date" AccessibleHeaderText="Date">
                                            <ItemTemplate>
                                                <asp:HiddenField runat="server" ID="hdnID" Value='<%# Eval("ID") %>' />
                                                <asp:Label runat="server" ID="lblWeekHoliday" Text='<%# Eval("FromDateTime") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Day" AccessibleHeaderText="Day">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblDay" Text='<%# Eval("Day") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Down Reason" AccessibleHeaderText="DownReason">
                                            <ItemTemplate>
                                           <asp:HiddenField runat="server" ID="hdnStartDate" Value='<%# Eval("FromDateTime1") %>' />
                                              <asp:HiddenField runat="server" ID="hdnEndDate" Value='<%# Eval("ToDateTime1") %>' />
                                                <asp:Label runat="server" ID="lblOffReason" Text='<%# Eval("Reason") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Machine" AccessibleHeaderText="Machine">
                                            <ItemTemplate>                                                
                                                <asp:Label runat="server" ID="lblMachineId" Text='<%# Eval("MachineID") %>' />
                                            </ItemTemplate>
                                            <ItemStyle ForeColor="Black" />
                                        </asp:TemplateField>--%>
                                    </Columns>
                                    <PagerStyle CssClass="paginationCss" />
                                </asp:GridView>
                            </div>
                            <div class="modal fade" id="weeklyOffDeleteConfirmModal" role="dialog">
                                <div class="modal-dialog modal-dialog-centered">
                                    <div class="modal-content modalContent confirm-modal-content">
                                        <div class="modal-header modalHeader confirm-modal-header">
                                            <i class="glyphicon glyphicon glyphicon glyphicon-question-sign modal-icons"></i>
                                            <br />
                                            <h4 class="confirm-modal-title">Confirmation!</h4>
                                            <br />
                                            <span class="confirm-modal-msg">Are you sure you want to delete Records?</span>
                                        </div>
                                        <div class="modal-footer modalFooter modal-footer">
                                            <asp:Button runat="server" Text="Yes" ID="btnDeleteWeeklyOffData" CssClass="confirm-modal-btn" OnClick="btnDeleteWeeklyOffData_Click" OnClientClick="return clearModalScreen();" />
                                            <input type="button" value="No" data-dismiss="modal" class="confirm-modal-btn" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnHodiday" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnWeeklyOff" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnDailyDown" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnShiftDown" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnAllDown" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>


                <%--------DailyDownContent  -----%>
                <div id="tabDailyDownContent" class="tab-pane fade">
                    <asp:UpdatePanel runat="server" ID="DailyDownUP1">
                        <ContentTemplate>
                            <fieldset class="field-set">
                                <legend>&nbsp;<b>Filter By</b></legend>
                                <table class="filterTbl filter-field-table">
                                    <tr>
                                        <td>From Date

                                        </td>

                                        <td>
                                            <asp:TextBox runat="server" ID="txtFromDailyDowns" CssClass="form-control datetimeCss" AutoCompleteType="Disabled" />
                                        </td>
                                        <td>To Date

                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtToDailyDowns" CssClass="form-control datetimeCss" AutoCompleteType="Disabled" />
                                        </td>
                                        <td>From Time

                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtFromTimeDailydown" CssClass="form-control date datetimeCss" AutoCompleteType="Disabled" Style="position: relative" />
                                        </td>
                                        <td>To
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtToTimeDailydown" CssClass="form-control date datetimeCss" AutoCompleteType="Disabled" Style="position: relative" />
                                        </td>
                                        <td>Reason
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtDailyDownReason" CssClass="form-control" onkeypress="return restrictSpecialCharacter(event);" ClientIDMode="Static"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button runat="server" ID="btnDailyDownSave" Text="Save" CssClass="Buttons" OnClick="btnDailyDownSave_Click" OnClientClick="return showLoaderWithValidation('txtDailyDownReason');" />
                                             <asp:Button runat="server" ID="btnExportDailyDown" Text="Export" CssClass="Buttons" OnClick="btnExportDailyDown_Click"  />
                                        </td>
                                         
                                    </tr>
                                </table>

                            </fieldset>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnHodiday" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnWeeklyOff" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnDailyDown" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnShiftDown" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnAllDown" EventName="Click" />
                            <asp:PostBackTrigger ControlID="btnExportDailyDown" />
                        </Triggers>
                    </asp:UpdatePanel>

                    <asp:UpdatePanel runat="server" ID="DailyDownUP2">
                        <ContentTemplate>
                            <table style="margin-top: 10px; border: none; width: 100%;" class="tblAction">
                                <tr>
                                    <td style="width: 50px; text-align: center;">
                                        <asp:CheckBox runat="server" ID="chkSelectAllDailyDowns" onclick="clickAllcheck(this);" CssClass="chkstyle" ClientIDMode="Static" />
                                    </td>
                                    <td id="tdRepeat3" style="width: 30px;">
                                        <asp:LinkButton runat="server" ID="lnkDailyDownsReload" CssClass="glyphicon glyphicon-repeat" Font-Size="16" ToolTip="Reload" OnClick="lnkDailyDownsReload_Click" />
                                    </td>
                                    <td id="tdDelete3" style="width: 30px;">
                                        <asp:LinkButton runat="server" ID="lnkDailyDownDelete" CssClass="glyphicon glyphicon-trash" Font-Size="16" ToolTip="Delete" OnClientClick="return openDeleteConfirmModal('dailyDownDeleteConfirmModal','gvDailyDowns','chkSelectDailyDowns')" />
                                    </td>
                                    <td id="tdFilter3" style="width: 30px;">

                                        <asp:LinkButton runat="server" ID="lnkDailyDownFilter" CssClass="glyphicon glyphicon-filter" Font-Size="18" ToolTip="Filter" OnClick="lnkDailyDownFilter_Click" />
                                    </td>
                                    <td>

                                        <table id="divDailyDownFilter" style="" runat="server" clientidmode="static" class="downFilterTbl">
                                            <tr>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="ddlDailyDownFilterType" CssClass="form-control dropdown-list" AutoPostBack="true" OnSelectedIndexChanged="ddlDailyDownFilterType_SelectedIndexChanged">
                                                        <asp:ListItem Value="ByDate" Text="By Date" />
                                                        <asp:ListItem Value="ByReason" Text="By Reason" />
                                                        <%--<asp:ListItem Value="ByMachine" Text="By Machine" />--%>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <table id="dailyDownFilterDateTbl" runat="server">
                                                        <tr>
                                                            <td>From
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" ID="txtFilterFromDailyDowns" CssClass="form-control datetimeCss" AutoCompleteType="Disabled" />
                                                            </td>
                                                            <td>To
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" ID="txtFilterToDailyDowns" CssClass="form-control datetimeCss" AutoCompleteType="Disabled" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <%--<td>
                                                    <table id="dailyDownFilterMachineTbl" runat="server">
                                                        <tr>
                                                            <td>Machine
                                                            </td>
                                                            <td>
                                                                <asp:ListBox ID="ddlDailyDownFilterMachines" runat="server" SelectionMode="Multiple" ClientIDMode="Static" CssClass="dropdown-list"></asp:ListBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>--%>
                                                <td>
                                                    <table id="dailyDownFilterReasonTbl" runat="server">
                                                        <tr>
                                                            <td>Reason
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList runat="server" ID="ddlDailyDownsByReason" CssClass="form-control dropdown-list">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <asp:Button runat="server" ID="btnDailyDownFilterAppy" CssClass="Buttons" Text="Apply" OnClick="btnDailyDownFilterAppy_Click" OnClientClick="return showLoader();" />

                                                    <asp:LinkButton runat="server" ID="lnkDailyDownFilterClose" CssClass="closeButton" ToolTip="Close" Text="&times;" OnClick="lnkDailyDownFilterClose_Click" />
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
                            <asp:AsyncPostBackTrigger ControlID="btnDailyDown" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnShiftDown" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnAllDown" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel runat="server" ID="DailyDownUP3">
                        <ContentTemplate>
                            <div id="divDailyDownsGrid" style="overflow: auto; height: 60vh" class="griContainer">
                                <asp:GridView runat="server" ID="gvDailyDowns" AutoGenerateColumns="false" ShowHeader="true" ShowFooter="false" CssClass="P1Table" ClientIDMode="Static" AllowPaging="true" OnPageIndexChanging="gvDailyDowns_PageIndexChanging" OnPreRender="gvDailyDowns_PreRender" PageSize="100" EmptyDataText="No Data Found">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:HiddenField runat="server" ID="hdnID" Value='<%# Eval("ID") %>' />
                                                <asp:CheckBox runat="server" ID="chkSelectDailyDowns" onclick="checkclick(this);" />
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Start DateTime" AccessibleHeaderText="StartDateTime">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblDailyHoliday" Text='<%# Eval("FromDateTime") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="30%" ForeColor="Black" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="End DateTime" AccessibleHeaderText="EndDateTime">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblBreakReason" Text='<%# Eval("ToDateTime") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="30%" ForeColor="Black" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Down Reason" AccessibleHeaderText="DownReason">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblMachineId" Text='<%# Eval("Reason") %>' />
                                            </ItemTemplate>
                                            <ItemStyle ForeColor="Black" />
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Machine" AccessibleHeaderText="Machine">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblStartTime" Text='<%# Eval("MachineID") %>' />
                                            </ItemTemplate>
                                            <ItemStyle ForeColor="Black" />
                                        </asp:TemplateField>--%>
                                          <asp:TemplateField HeaderText="End Datetime">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblEndTime" Text='<%# Eval("ToDateTime") %>' />
                                            </ItemTemplate>
                                            <ItemStyle ForeColor="Black" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="paginationCss" />
                                </asp:GridView>
                            </div>
                            <div class="modal fade" id="dailyDownDeleteConfirmModal" role="dialog">
                                <div class="modal-dialog modal-dialog-centered">
                                    <div class="modal-content modalContent confirm-modal-content">
                                        <div class="modal-header modalHeader confirm-modal-header">
                                            <i class="glyphicon glyphicon glyphicon glyphicon-question-sign modal-icons"></i>
                                            <br />
                                            <h4 class="confirm-modal-title">Confirmation!</h4>
                                            <br />
                                            <span class="confirm-modal-msg">Are you sure you want to delete Records?</span>
                                        </div>
                                        <div class="modal-footer modalFooter modal-footer">
                                            <asp:Button runat="server" Text="Yes" ID="btnDeleteDailyDownData" CssClass="confirm-modal-btn" OnClick="btnDeleteDailyDownData_Click" OnClientClick="return clearModalScreen();" />
                                            <input type="button" value="No" data-dismiss="modal" class="confirm-modal-btn" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnHodiday" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnWeeklyOff" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnDailyDown" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnShiftDown" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnAllDown" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <%-- ShiftDownContent --%>
                <div id="tabShiftDownContent" class="tab-pane fade">
                    <asp:UpdatePanel runat="server" ID="ShiftDownUP1">
                        <ContentTemplate>
                            <fieldset class="field-set">
                                <legend>&nbsp;<b>Filter By</b></legend>
                                <table class="filterTbl filter-field-table" style="width:100%">
                                    <tr>
                                        <td>From
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtFromShiftDowns" CssClass="form-control datetimeCss" AutoCompleteType="Disabled" />
                                        </td>
                                        <td>To
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtToShiftDowns" CssClass="form-control datetimeCss" AutoCompleteType="Disabled" />
                                        </td>
                                        <td>Days
                                        </td>
                                        <td>
                                            <asp:ListBox ID="ddlOffDays" runat="server" SelectionMode="Multiple" ClientIDMode="Static" CssClass="dropdown-list">
                                                <asp:ListItem Text="Sun" />
                                                <asp:ListItem Text="Mon" />
                                                <asp:ListItem Text="Tue" />
                                                <asp:ListItem Text="Wed" />
                                                <asp:ListItem Text="Thu" />
                                                <asp:ListItem Text="Fri" />
                                                <asp:ListItem Text="Sat" />
                                            </asp:ListBox>
                                        </td>
                                        <td>Shift
                                        </td>
                                        <td>
                                            <asp:ListBox runat="server" ID="ddlshiftsDowns" SelectionMode="Multiple" ClientIDMode="Static" CssClass="dropdown-list"></asp:ListBox>
                                        </td>
                                        <td>Down Reason
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtShiftDownReason" ClientIDMode="Static" CssClass="form-control" onkeypress="return restrictSpecialCharacter(event);"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;
                                            <asp:Button runat="server" ID="btnSaveShiftDown" CssClass="Btns" Text="Save" OnClick="btnSaveShiftDown_Click" OnClientClick="return showLoaderWithValidation('txtShiftDownReason');" />
                                             <asp:Button runat="server" ID="btnExportShiftDown" CssClass="Btns" Text="Export" OnClick="btnExportShiftDown_Click" />
                                        </td>
                                       <%--  <td class="td-filters-style" onclick="showColumnFilterPanel(this, event)">
                                            <i class="glyphicon glyphicon-filter" style="font-size: 17px;"></i>
                                        </td>--%>
                                    </tr>
                                </table>
                            </fieldset>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnHodiday" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnWeeklyOff" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnDailyDown" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnShiftDown" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnAllDown" EventName="Click" />
                            <asp:PostBackTrigger ControlID="btnExportShiftDown" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel runat="server" ID="ShiftDownUP2">
                        <ContentTemplate>
                            <table style="margin-top: 10px; border: none; width: 100%;" class="tblAction">
                                <tr>
                                    <td style="width: 50px; text-align: center;">
                                        <asp:CheckBox runat="server" ID="chkAllShiftDown" onclick="clickAllcheck(this);" CssClass="chkstyle" ClientIDMode="Static" />
                                    </td>
                                    <td style="width: 30px;">
                                        <asp:LinkButton runat="server" ID="lnkReloadShiftDown" CssClass="glyphicon glyphicon-repeat" Font-Size="16" ToolTip="Reload" OnClick="lnkReloadShiftDown_Click" />
                                    </td>
                                    <td style="width: 30px;">
                                        <asp:LinkButton runat="server" ID="lnkShiftDownDelete" CssClass="glyphicon glyphicon-trash" Font-Size="16" ToolTip="Delete" OnClientClick="return openDeleteConfirmModal('shiftDownDeleteConfirmModal','gvMachineShift','chkSelectShiftDown')" />
                                    </td>
                                    <td style="width: 30px;">
                                        <asp:LinkButton runat="server" ID="lnkShiftDownFilter" CssClass="glyphicon glyphicon-filter" Font-Size="18" ToolTip="Filter" OnClick="lnkShiftDownFilter_Click" />
                                    </td>
                                    <td>
                                        <div>
                                            <table id="divShiftDownFilterContainer" runat="server" clientidmode="static" class="downFilterTbl">
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList runat="server" ID="ddlShiftFilterType" CssClass="form-control dropdown-list" AutoPostBack="true" OnSelectedIndexChanged="ddlShiftFilterType_SelectedIndexChanged">
                                                            <asp:ListItem Value="ByDate" Text="By Date" />
                                                            <asp:ListItem Value="ByReason" Text="By Reason" />
                                                           <%-- <asp:ListItem Value="ByMachine" Text="By Machine" />--%>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <table runat="server" id="shiftFilterDateTbl">
                                                            <tr>
                                                                <td>From
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox runat="server" ID="txtFromShiftDownFilter" CssClass="form-control datetimeCss" AutoCompleteType="Disabled" />
                                                                </td>
                                                                <td>To
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox runat="server" ID="txtToShiftDownFilter" CssClass="form-control datetimeCss" AutoCompleteType="Disabled" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>
                                                        <table runat="server" id="shiftFilterDayTbl">
                                                            <tr>
                                                                <td>Days
                                                                </td>
                                                                <td>
                                                                    <asp:ListBox ID="lbDayShiftDownFilter" runat="server" SelectionMode="Multiple" ClientIDMode="Static" CssClass="dropdown-list">
                                                                        <asp:ListItem Text="Sun" />
                                                                        <asp:ListItem Text="Mon" />
                                                                        <asp:ListItem Text="Tue" />
                                                                        <asp:ListItem Text="Wed" />
                                                                        <asp:ListItem Text="Thu" />
                                                                        <asp:ListItem Text="Fri" />
                                                                        <asp:ListItem Text="Sat" />
                                                                    </asp:ListBox>
                                                                </td>
                                                                <td>Shift
                                                                </td>
                                                                <td>
                                                                    <asp:ListBox runat="server" ID="lbShiftNameShiftDownFilter" SelectionMode="Multiple" ClientIDMode="Static" CssClass="dropdown-list"></asp:ListBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <%--<td>
                                                        <table runat="server" id="shiftFilterMachineTbl" clientidmode="static">
                                                            <tr>
                                                                <td>Machine
                                                                </td>
                                                                <td>
                                                                    <asp:ListBox ID="ddlShiftDownFilterMachines" runat="server" SelectionMode="Multiple" ClientIDMode="Static" CssClass="dropdown-list"></asp:ListBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>--%>
                                                    <td>
                                                        <table runat="server" id="shiftFilterReasonTbl">
                                                            <tr>
                                                                <td>
                                                                    <asp:DropDownList runat="server" ID="ddlShiftFilterReason" CssClass="form-control dropdown-list"></asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>



                                                    <td>
                                                        <asp:Button runat="server" ID="btnApplyShiftDownFilter" CssClass="Buttons" Text="Apply" OnClick="btnApplyShiftDownFilter_Click" OnClientClick="return showLoader();" />
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton runat="server" ID="lnkShiftDownFilterClose" CssClass="closeButton" ToolTip="Close" Text="&times;" OnClick="lnkShiftDownFilterClose_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>

                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnHodiday" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnWeeklyOff" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnDailyDown" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnShiftDown" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnAllDown" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel runat="server" ID="ShiftDownUP3">
                        <ContentTemplate>
                            <div id="gvMachineShiftContainer" style="overflow: auto; height: 60vh;" class="griContainer">
                                <asp:GridView runat="server" ID="gvMachineShift" AutoGenerateColumns="false" CssClass="P1Table" ShowFooter="false" ClientIDMode="Static" AllowPaging="true" OnPageIndexChanging="gvMachineShift_PageIndexChanging" OnPreRender="gvMachineShift_PreRender" PageSize="100" EmptyDataText="No Data Found">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="chkSelectShiftDown" onclick="checkclick(this);" />
                                                <asp:HiddenField runat="server" ID="hdnID" Value='<%# Eval("ID") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Machine" AccessibleHeaderText="Machine">
                                            <ItemTemplate>                                                
                                                <asp:Label runat="server" ID="lblMachineId" Text='<%# Eval("MachineID") %>' />
                                            </ItemTemplate>
                                            <ItemStyle ForeColor="Black" />
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Shift" AccessibleHeaderText="Shift">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblShift" Text='<%# Eval("ShiftName") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="20%" ForeColor="Black" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Down Reason" AccessibleHeaderText="DownReason">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblShiftDownReason" Text='<%# Eval("Reason") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="40%" ForeColor="Black" />
                                        </asp:TemplateField>
                                        <%--  <asp:TemplateField HeaderText="Day">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblShiftDay" Text='<%# Eval("Day") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="40%" ForeColor="Black" />
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Date" AccessibleHeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblShiftDate" Text='<%# Eval("FromDateTime") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="20%" ForeColor="Black" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <PagerStyle CssClass="paginationCss" />
                                </asp:GridView>
                            </div>
                            <div class="modal fade" id="shiftDownDeleteConfirmModal" role="dialog">
                                <div class="modal-dialog modal-dialog-centered">
                                    <div class="modal-content modalContent confirm-modal-content">
                                        <div class="modal-header modalHeader confirm-modal-header">
                                            <i class="glyphicon glyphicon glyphicon glyphicon-question-sign modal-icons"></i>
                                            <br />
                                            <h4 class="confirm-modal-title">Confirmation!</h4>
                                            <br />
                                            <span class="confirm-modal-msg">Are you sure you want to delete Records?</span>
                                        </div>
                                        <div class="modal-footer modalFooter modal-footer">
                                            <asp:Button runat="server" Text="Yes" ID="btnDeleteShiftDownData" CssClass="confirm-modal-btn" OnClick="btnDeleteShiftDownData_Click" OnClientClick="return clearModalScreen();" />
                                            <input type="button" value="No" data-dismiss="modal" class="confirm-modal-btn" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnHodiday" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnWeeklyOff" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnDailyDown" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnShiftDown" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnAllDown" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>

                <%-- AllDownContents --%>
                <div id="tabAllDownContent" class="tab-pane fade">
                    <asp:UpdatePanel runat="server" ID="AllDownUP1">
                        <ContentTemplate>
                            <fieldset class="field-set">
                                <legend>&nbsp;<b>Filter By</b></legend>
                                <table class="filterTbl filter-field-table">
                                    <tr>
                                        <td>From
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtFromAllDowns" CssClass="form-control datetimeCss" AutoCompleteType="Disabled" />
                                        </td>
                                        <td>To
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtToAllDowns" CssClass="form-control datetimeCss" AutoCompleteType="Disabled" />
                                        </td>
                                        <%-- <td>
                                           Plant
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlPlantAllDown" CssClass="form-control dropdown-list" OnSelectedIndexChanged="ddlPlantAllDown_SelectedIndexChanged" AutoPostBack="true" />
                                        </td>--%>
                                        <td>Machine
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlMachineAllDown" CssClass="form-control dropdown-list" />
                                        </td>
                                        <td>Downs
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlAllDowns" CssClass="form-control dropdown-list">
                                                <asp:ListItem Value="Holidays" Text="Holidays" />
                                                <asp:ListItem Value="WeeklyOff" Text="Weekly Offs" />
                                                <asp:ListItem Value="DailyDown" Text="Daily Downs" />
                                                <%--<asp:ListItem Value="Shift" Text="Shift Downs" />--%>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Button runat="server" ID="btnViewAllDowns" CssClass="Buttons" Text="View" OnClick="btnViewAllDowns_Click" />
                                             <asp:Button runat="server" ID="btnExportAllDown" CssClass="Buttons" Text="Export" OnClick="btnExportAllDown_Click" />
                                        </td>
                                        
                                    </tr>
                                </table>
                            </fieldset>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnHodiday" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnWeeklyOff" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnDailyDown" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnShiftDown" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnAllDown" EventName="Click" />
                            <asp:PostBackTrigger ControlID="btnExportAllDown" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel runat="server" ID="AllDownUP2">
                        <ContentTemplate>
                            <table style="margin-top: 10px; border: none; width: 100%;" class="tblAction">
                                <tr>
                                    <td style="width: 50px; text-align: left;">
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
                            <asp:AsyncPostBackTrigger ControlID="btnDailyDown" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnShiftDown" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnAllDown" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel runat="server" ID="AllDownUP3">
                        <ContentTemplate>
                            <div id="divAllDownsGrid" style="overflow: auto; height: 60vh" class="griContainer">
                                <asp:GridView runat="server" ID="gvAllDowns" AutoGenerateColumns="false" ShowHeader="true" ShowFooter="false" CssClass="P1Table" ClientIDMode="Static" AllowPaging="true" OnPageIndexChanging="gvAllDowns_PageIndexChanging" OnPreRender="gvAllDowns_PreRender" PageSize="100" EmptyDataText="No Data Found">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="chkSelectDowns" onclick="checkclick(this);" />
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Holiday" AccessibleHeaderText="Holiday">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblAllHolidays" Text='<%# Eval("FromDateTime") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="20%" ForeColor="Black" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Down Category" AccessibleHeaderText="DownCategory">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblAllDowns" Text='<%# Eval("DownType") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="20%" ForeColor="Black" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Down Reason" AccessibleHeaderText="DownReason">
                                            <ItemTemplate>
                                                <asp:HiddenField runat="server" ID="hdnID" Value='<%# Eval("ID") %>' />
                                                <asp:Label runat="server" ID="lblAllReason" Text='<%# Eval("Reason") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="30%" ForeColor="Black" />
                                        </asp:TemplateField>
                                       <%-- <asp:TemplateField HeaderText="Machine" AccessibleHeaderText="Machine">
                                            <ItemTemplate>                                                
                                                <asp:Label runat="server" ID="lblAllMachineId" Text='<%# Eval("MachineID") %>' />
                                            </ItemTemplate>
                                            <ItemStyle ForeColor="Black" />
                                        </asp:TemplateField>--%>
                                    </Columns>
                                    <PagerStyle CssClass="paginationCss" />
                                </asp:GridView>
                            </div>
                            <div class="modal fade" id="allDownDeleteConfirmModal" role="dialog">
                                <div class="modal-dialog modal-dialog-centered">
                                    <div class="modal-content modalContent confirm-modal-content">
                                        <div class="modal-header modalHeader confirm-modal-header">
                                            <i class="glyphicon glyphicon glyphicon glyphicon-question-sign modal-icons"></i>
                                            <br />
                                            <h4 class="confirm-modal-title">Confirmation!</h4>
                                            <br />
                                            <span class="confirm-modal-msg">Are you sure you want to delete Records?</span>
                                        </div>
                                        <div class="modal-footer modalFooter modal-footer">
                                            <asp:Button runat="server" Text="Yes" ID="btnDeleteAllDown" CssClass="confirm-modal-btn" OnClick="btnDeleteAllDown_Click" OnClientClick="return clearModalScreen();" />
                                            <input type="button" value="No" data-dismiss="modal" class="confirm-modal-btn" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnHodiday" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnWeeklyOff" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnDailyDown" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnShiftDown" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnAllDown" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
   <%--</div>--%>
    <div class="modal infoModal" id="rejectedItemsModal" role="dialog" style="min-width: 300px;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog  modal-dialog-centered " style="width: 50%">
            <div class="modal-content modalThemeCss">
                <div class="modal-header">
                    <h4 class="modal-title">Down Time already created for below list</h4>
                </div>
                <div class="modal-body">
                    <div id="rejectedItemsContainer">
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" data-dismiss="modal" class="Btns">Close</button>
                </div>
            </div>
        </div>
    </div>

   <%-- <div class="panel panel-default panel-subitems column-filter-panel" id="panelColumnFilter" style="padding: 10px">--%>
        <%--  <i class="glyphicon glyphicon-remove" style="float: right" onclick="hideColumnFilterPanels(event,'tdColumnFilters','panelColumnFilter')"></i>--%>
        <%--hidePanels(this,'panelFilter')--%>
        <div class="panel-body" style="padding-top: 10px;display:none">
            <div id="divColumnFilter">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:HiddenField runat="server" ID="selectedMenu" ClientIDMode="Static" />
                        <asp:CheckBox runat="server" ID="cbColumnSelectAll" ClientIDMode="Static" Text="Select All" onchange="return selectAllColumnClick()" />
                        <asp:CheckBoxList runat="server" ID="cblColumnSelector" CssClass="checkbox-list" ClientIDMode="Static"></asp:CheckBoxList>
                        <asp:Button runat="server" ID="btnColumnSelectorOK" Text="OK" ClientIDMode="Static" Style="margin-left: 20px" CssClass="Btns" OnClick="btnColumnSelectorOK_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
   <%-- </div>--%>

    <div class="modal fade" id="warningModal" role="dialog">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content modalContent warning-modal-content">
                <div class="modal-header modalHeader warning-modal-header">
                    <i class="glyphicon glyphicon-warning-sign modal-icons"></i>
                    <br />
                    <h4 class="warning-modal-title">Warning</h4>
                    <br />
                    <span class="warning-modal-msg" id="lblWarningMsg">Reocrd insertion failed...</span>
                </div>
                <div class="modal-footer modalFooter modal-footer">
                    <input type="button" value="OK" class="warning-modal-btn" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>

     <Script>
        function clearModalScreen() {
            $(".modal-backdrop").removeClass("modal-backdrop in");
            return true;
        }
        $(".submenuData").click(function () {
            //debugger;
            //  $('[id*=hdnScrollPos]').val(0);
            $.blockUI({ message: '<img runat="server" src="Images/Loading.gif" />' });
            if ($("#mainBody").hasClass("light-mode")) {
                $(".submenuData").removeClass("selected-menu-style selected-Submenu").addClass("other-menu-style");
                $(".submenuData").closest('li').find('i').removeClass();
                $(this).removeClass("other-menu-style selected-Submenu").addClass("selected-menu-style");
            } else {
                $(".submenuData").removeClass("selected-menu-style selected-Submenu").addClass("other-menu-style");
                $(".submenuData").closest('li').find('i').removeClass();
                $(this).removeClass("other-menu-style selected-Submenu").addClass("selected-menu-style");
            }

            // $(this).closest('li').find('i').addClass("arrow up selected-Submenu-ArrowUp");
            submenu = $(this).attr('href');
            $("#activeMenu").val(submenu);
            $(submenu).addClass("in active");
            if (submenu == "#tabWeeklyOffContent") {
                __doPostBack('<%= btnWeeklyOff.UniqueID%>', '');
            }
            else if (submenu == "#tabDailyDownContent") {
                __doPostBack('<%= btnDailyDown.UniqueID%>', '');
            }
            else if (submenu == "#tabShiftDownContent") {
                __doPostBack('<%= btnShiftDown.UniqueID%>', '');
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
          //  debugger;
            var lilist = $("#masterul li");
            for (let i = 0; i < lilist.length; i++) {
                let li = lilist[i];
                let display = $(li).css('display');
              //  debugger;
                if (display == "block") {
                    localStorage.setItem("selectedPDTSubMenu", $(li).find('a').attr('href'));
                    activeSubMenu();
                    enterBlock = true;
                    break;
                }
            }
            // localStorage.setItem("selectedPPSubMenu", "#processParamMenu0");
        }
        function activeSubMenu() {
            if (localStorage.getItem("selectedPDTSubMenu")) {

                if (localStorage.getItem("selectedPDTSubMenu")) {
                    //debugger;
                    submenu = localStorage.getItem("selectedPDTSubMenu");
                }
                $(submenu).addClass("in active");
                $("a[href$='" + submenu + "']").removeClass("selected-menu-style").addClass("selected-Submenu");
                //$("a[href$='" + submenu + "']").closest('li').find('i').addClass("arrow up selected-Submenu-ArrowUp");
                $('#hdnSubMenuData').val(submenu);
            } else {
                $(".sub-sub-menudiv").removeClass("in active");
            }
        }
        function showLoaderWithValidation(txt) {
           // debugger;
            if ($('#' + txt).val().trim() == "") {
                OpenWarningModal("Please enter Down Reason.");
                return false;
            }
            /*$.blockUI({ message: '<img runat="server" src="Images/Loading.gif" />' });*/
            return true;
         }
         function OpenWarningModal(msg) {
             $('#warningModal').modal('show');
             $("#warningModalMsg").text(msg);
         }
        function showLoader() {
           // debugger;
            $.blockUI({ message: '<img runat="server" src="Images/Loading.gif" />' });
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
        //function openWarningModal(msg) {
        //    $('#lblWarningMsg').text(msg);
        //    $('[id*=warningModal]').modal('show');
        //}
        function closeDeletePopup() {
            $('[id*=holidayListConfirmModal]').modal('hide');
        }
        function openConfirmModal() {
           // debugger;
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
                openWarningModal("Select records for delete.");
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
                        var appendString = "<table class='P1Table'><tr><th>Start Time</th><th>End Time</th><th>Machine</th><th>Down Reason</th></tr>";
                        for (let i = 0; i < itemData.length; i++) {
                            appendString += '<tr><td>' + itemData[i].FromDateTime + '</td><td>' + itemData[i].ToDateTime + '</td><td>' + itemData[i].MachineID + '</td><td>' + itemData[i].Reason + '</td></tr>'
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
           //  debugger;
             var grid = null;
             if (obj == document.getElementById("chkSelectAllHoliday")) {
                 grid = document.getElementById("gvHolidays");
             }
             else if (obj == document.getElementById("chkSelectAllWeekOffs")) {
                 grid = document.getElementById("gvWeekOffs");
             }
             else if (obj == document.getElementById("chkSelectAllDailyDowns")) {
                 grid = document.getElementById("gvDailyDowns");
             }
             else if (obj == document.getElementById("chkSelectAllDowns")) {
                 grid = document.getElementById("gvAllDowns");
             }
             else if (obj == document.getElementById("chkAllShiftDown")) {
                 grid = document.getElementById("gvMachineShift");
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
            // debugger;
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
             else if (GridView == document.getElementById("gvDailyDowns")) {
                 selectAllCheckbox = document.getElementById("chkSelectAllDailyDowns");
             }
             else if (GridView == document.getElementById("gvMachineShift")) {
                 selectAllCheckbox = document.getElementById("chkAllShiftDown");
             }
             else if (GridView == document.getElementById("gvAllDowns")) {
                 selectAllCheckbox = document.getElementById("chkSelectAllDowns");
             }

             selectAllCheckbox.checked = checked;
         }
         function resize() {

             var heights = window.innerHeight - 370;
             document.getElementById("divHolidayGrid").style.height = heights + "px";
             //document.getElementById("divWeeklyOffsGrid").style.height = heights + "px";
             //document.getElementById("divDailyDownsGrid").style.height = heights + "px";
             //document.getElementById("divAllDownsGrid").style.height = heights + "px";
         }
         $(document).ready(function () {

             var d = new Date();
             console.log("date =" + d);
             resize();
             window.onresize = function () {
                 resize();
             };
             setDateTimeToControls();
             activeSubMenu();
         });
         function setDateTimeToControls() {
            // debugger;
             $('[id$=txtFromWeeklyOffs]').datepicker({
                 format: 'dd-mm-yyyy',
                 //useCurrent: true,
                 locale: 'en-US'
             });
             $('[id$=TextBox1]').datepicker({
                 format: 'dd-mm-yyyy',
                 //useCurrent: true,
                 locale: 'en-US'
             });

             $('[id$=txtToWeeklyOffs]').datepicker({
                 format: 'dd-mm-yyyy',
                 useCurrent: true,
                 locale: 'en-US'
             });
             $('[id$=txtFromDailyDowns]').datepicker({
                 format: 'dd-mm-yyyy',
                 useCurrent: true,
                 locale: 'en-US'
             });
             $('[id$=txtToDailyDowns]').datepicker({
                 format: 'dd-mm-yyyy',
                 useCurrent: true,
                 locale: 'en-US'
             });
             $('[id$=txtFromYear]').datepicker({
                 minViewMode: 2,
                 format: 'yyyy',
                 todayHighlight: true,
                 autoclose: true,
                 language: 'en',
             });
             $('[id$=txtToYear]').datepicker({
                 minViewMode: 2,
                 format: 'yyyy',
                 todayHighlight: true,
                 autoclose: true,
                 language: 'en',
             });
             $('[id$=txtFromTimeDailydown]').datetimepicker({
                 format: 'HH:mm:ss',
                 locale: 'en-US'
             });
             $('[id$=txtToTimeDailydown]').datetimepicker({
                 format: 'HH:mm:ss',
                 locale: 'en-US'
             });
             $('[id$=txtFromShiftDowns]').datepicker({
                 format: 'dd-mm-yyyy',
                 useCurrent: true,
                 locale: 'en-US'
             });
             $('[id$=txtFromShiftDownFilter]').datepicker({
                 format: 'dd-mm-yyyy',
                 useCurrent: true,
                 locale: 'en-US'
             });
             $('[id$=txtToShiftDownFilter]').datepicker({
                 format: 'dd-mm-yyyy',
                 useCurrent: true,
                 locale: 'en-US'
             });

             $('[id$=txtToShiftDowns]').datepicker({
                 format: 'dd-mm-yyyy',
                 useCurrent: true,
                 locale: 'en-US'
             });
             $('[id$=txtFromAllDowns]').datepicker({
                 format: 'dd-mm-yyyy',
                 useCurrent: true,
                 locale: 'en-US'
             });
             $('[id$=txtToAllDowns]').datepicker({
                 format: 'dd-mm-yyyy',
                 useCurrent: true,
                 locale: 'en-US'
             });
             $('[id$=txtFromWeekOffs]').datepicker({
                 format: 'dd-mm-yyyy',
                 useCurrent: true,
                 locale: 'en-US'
             });
             $('[id$=txtToWeekOffs]').datepicker({
                 format: 'dd-mm-yyyy',
                 useCurrent: true,
                 locale: 'en-US'
             });
             $('[id$=txtFilterFromDailyDowns]').datepicker({
                 format: 'dd-mm-yyyy',
                 useCurrent: true,
                 locale: 'en-US'
             });
             $('[id$=txtFilterToDailyDowns]').datepicker({
                 format: 'dd-mm-yyyy',
                 useCurrent: true,
                 locale: 'en-US'
             });

             $('[id$=ddlMachineIDs]').multiselect({
                 includeSelectAllOption: true
             });
             $('[id$=ddlFilterByMachines]').multiselect({
                 includeSelectAllOption: true
             });

             $('[id$=ddlshiftsDowns]').multiselect({
                 includeSelectAllOption: true
             });
             $('[id$=lbShiftNameShiftDownFilter]').multiselect({
                 includeSelectAllOption: true
             });

             $('[id$=ddlDays]').multiselect({
                 includeSelectAllOption: true
             });
             $('[id$=ddlWeek]').multiselect({
                 includeSelectAllOption: true
             });
             $('[id$=ddlDailyDownFilterMachines]').multiselect({
                 includeSelectAllOption: true
             });
             $('[id$=ddlShiftDownFilterMachines]').multiselect({
                 includeSelectAllOption: true
             });
             $('[id$=lbFilterWeekByMachine]').multiselect({
                 includeSelectAllOption: true
             });
             $('[id$=ddlOffDays]').multiselect({
                 includeSelectAllOption: true
             });
             $('[id$=lbDayShiftDownFilter]').multiselect({
                 includeSelectAllOption: true
             });
             $('[id$=txtFilterByDateFrom]').datepicker({
                 format: 'dd-mm-yyyy',
                 useCurrent: false,
                 locale: 'en-US'
             });
             $('[id$=txtFilterByDateTo]').datepicker({
                 format: 'dd-mm-yyyy',
                 useCurrent: false,
                 locale: 'en-US'
             });
            
         }
         var bigDiv = document.getElementById('gvMachineShiftContainer');
         bigDiv.onscroll = function () {
             $('[id*=hdnScrollPos]').val(bigDiv.scrollTop);
         }
         var bigDivAllDown = document.getElementById('divAllDownsGrid');
         bigDivAllDown.onscroll = function () {
             $('[id*=hdnScrollPos]').val(bigDivAllDown.scrollTop);
         }
         var bigDivDaily = document.getElementById('divDailyDownsGrid');
         bigDivDaily.onscroll = function () {
             $('[id*=hdnScrollPos]').val(bigDivDaily.scrollTop);
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
             bigDiv.scrollTop = $('[id*=hdnScrollPos]').val();
             bigDivAllDown.scrollTop = $('[id*=hdnScrollPos]').val();
             bigDivDaily.scrollTop = $('[id*=hdnScrollPos]').val();
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
             //evt.stopPropagation();
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

         Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {

             var bigDiv = document.getElementById('gvMachineShiftContainer');
             var bigDivAllDown = document.getElementById('divAllDownsGrid');
             var bigDivDaily = document.getElementById('divDailyDownsGrid');
             var bigDivWeekly = document.getElementById('divWeeklyOffsGrid');
             var bigDivHoliday = document.getElementById('divHolidayGrid');
             $(document).ready(function () {
                 $.unblockUI({}); $('.ajax-loader').hide();
                 var d = new Date();
                 console.log("date =" + d);
                 //alert(d);
                 setDateTimeToControls();
                 bigDiv.scrollTop = $('[id*=hdnScrollPos]').val();
                 bigDivAllDown.scrollTop = $('[id*=hdnScrollPos]').val();
                 bigDivDaily.scrollTop = $('[id*=hdnScrollPos]').val();
                 bigDivWeekly.scrollTop = $('[id*=hdnScrollPos]').val();
                 bigDivHoliday.scrollTop = $('[id*=hdnScrollPos]').val();
                 activeSubMenu();
             });

             $(".submenuData").click(function () {
                 //debugger;
                 //  $('[id*=hdnScrollPos]').val(0);
                 $.blockUI({ message: '<img runat="server" src="Images/Loading.gif" />' });
                 if ($("#mainBody").hasClass("light-mode")) {
                     $(".submenuData").removeClass("selected-menu-style selected-Submenu").addClass("other-menu-style");
                     $(".submenuData").closest('li').find('i').removeClass();
                     $(this).removeClass("other-menu-style selected-Submenu").addClass("selected-menu-style");
                 } else {
                     $(".submenuData").removeClass("selected-menu-style selected-Submenu").addClass("other-menu-style");
                     $(".submenuData").closest('li').find('i').removeClass();
                     $(this).removeClass("other-menu-style selected-Submenu").addClass("selected-menu-style");
                 }

                 // $(this).closest('li').find('i').addClass("arrow up selected-Submenu-ArrowUp");
                 submenu = $(this).attr('href');
                 $("#activeMenu").val(submenu);
                 $(submenu).addClass("in active");
                 if (submenu == "#tabWeeklyOffContent") {
                     __doPostBack('<%= btnWeeklyOff.UniqueID%>', '');
                }
                else if (submenu == "#tabDailyDownContent") {
                    __doPostBack('<%= btnDailyDown.UniqueID%>', '');
                }
                else if (submenu == "#tabShiftDownContent") {
                    __doPostBack('<%= btnShiftDown.UniqueID%>', '');
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
            bigDiv.onscroll = function () {
                $('[id*=hdnScrollPos]').val(bigDiv.scrollTop);
            }

            bigDivAllDown.onscroll = function () {
                $('[id*=hdnScrollPos]').val(bigDivAllDown.scrollTop);
            }

            bigDivDaily.onscroll = function () {
                $('[id*=hdnScrollPos]').val(bigDivDaily.scrollTop);
            }

            bigDivWeekly.onscroll = function () {
                $('[id*=hdnScrollPos]').val(bigDivWeekly.scrollTop);
            }

            bigDivHoliday.onscroll = function () {
                $('[id*=hdnScrollPos]').val(bigDivHoliday.scrollTop);
            }
            window.onload = function () {
                bigDiv.scrollTop = $('[id*=hdnScrollPos]').val();
                bigDivAllDown.scrollTop = $('[id*=hdnScrollPos]').val();
                bigDivDaily.scrollTop = $('[id*=hdnScrollPos]').val();
                bigDivWeekly.scrollTop = $('[id*=hdnScrollPos]').val();
                bigDivHoliday.scrollTop = $('[id*=hdnScrollPos]').val();
            }

            resize();
            window.onresize = function () {
                resize();
             };

             /*-------------------------------------- Column Filter ---------------------------------------*/

             function showColumnFilterPanel(element, evt) {
                 showhideColumnFilterPanels_ClickedPosition_WithGivenLeftTop(element, evt, 'panelColumnFilter', '', '65');
             }
             function hideColumnFilterPanels(evt, tdid, panelid) {
                 if ($("#" + panelid).css('visibility') == "visible") {
                     $("#" + tdid).toggleClass('highlight-selected-icon');
                     $("#" + panelid).toggleClass("show-dropdown-menu");
                 }
                 //evt.stopPropagation();
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

     </Script>

</asp:Content>
