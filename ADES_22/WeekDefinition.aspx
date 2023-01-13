<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WeekDefinition.aspx.cs" Inherits="ADES_22.WeekDefinition" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--<%: Styles.Render("~/bundles/datecss") %>--%>
    <%--<%: Scripts.Render("~/bundles/datejs") %>--%>

    <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>--%>
    <script src="Scripts/DateTimePicker331/moment.js"></script>
    <script src="Scripts/DateTimePicker331/bootstrap-datetimepicker.min.js"></script>
    <link href="Scripts/DateTimePicker331/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <%--<link href="Scripts/DateTimePicker331/Custom.css" rel="stylesheet" />--%>
    <script src="Scripts/DateTimePicker/bootstrap-datepicker.js"></script>
    <link href="Scripts/DateTimePicker/bootstrap-datepicker3.css" rel="stylesheet" />
    <%--<link href="Scripts/DateTimePicker/bootstrap-datetimepicker.css" rel="stylesheet" />--%>
    <script src="Scripts/DateTimePicker/bootstrap-datepicker.en-IE.min.js"></script>
    <script src="Scripts/DateTimePicker/bootstrap-datepicker.zh-CN.min.js"></script>

    <style type="text/css">
        #li_week {
            background-color: #f0f0f0;
            color: black;
            font-weight: bold;
        }

        th {
            cursor: pointer;
        }

        ::-webkit-scrollbar {
            width: 18px;
        }

        /* Track */
        ::-webkit-scrollbar-track {
            box-shadow: inset 0 0 5px grey;
            border-radius: 10px;
        }

        .table tbody > tr > th {
            vertical-align: middle;
        }

        .table > tr > td {
            vertical-align: middle;
        }

        /* Handle */
        ::-webkit-scrollbar-thumb {
            background-color: blue;
            border-radius: 15px;
        }

            /* Handle on hover */
            ::-webkit-scrollbar-thumb:hover {
                background: #000000;
            }

        .table thead > tr > th {
            vertical-align: top;
        }

        /*.HeaderCss th {
            color: white;
            background-color: #2E6886 !important;
            height: 60px;
            vertical-align: inherit;
        }*/

        #tableData {
            width: 100%;
        }

        .table thead > tr > th, .table tbody > tr > th, .table tfoot > tr > th, .table thead > tr > td, .table tbody > tr > td, .table tfoot > tr > td {
            border-top: 0px none;
        }

        a {
            color: black;
        }

        .table .lbl {
            padding-top: 15px;
        }

        #MainContent_updateTableViewData {
            margin-top: -15px;
        }

        .machineClick {
            text-decoration: underline;
            cursor: pointer;
        }

        th[data-content='OEE'] td {
            text-decoration: underline;
            cursor: pointer;
        }

        .hypercol {
            text-decoration: underline;
            cursor: pointer;
        }

        .GridHeader {
            text-align: center !important;
        }

        #tblfilter tr td {
            vertical-align: middle;
        }

        #divWeekDefinition{
            height: 80vh;
        }

        .maxheight{
            overflow: auto;
            margin-left: 20%;
        }
        
        .blue{
            background-color: #0c98e6;
            border: 1px solid #0c98e6;
            color: #fff;
        }
    </style>

    <div class="container-fluid">
       <%-- <asp:UpdatePanel runat="server">
            <ContentTemplate>--%>
                <div style="display: flex; justify-content: center; align-content: center;">
                    <asp:Label ID="lblMessages" EnableViewState="False" runat="server" Style="font-weight: bold; font-family: Calibri; font-style: italic; vertical-align: central; text-align: center; width: 600px; word-wrap: break-word;" Font-Size="Larger" ClientIDMode="Static"></asp:Label>
                </div>
                <div style="display: flex; justify-content: center; align-content: center;">
                    <table id="tblfilter" class="table table-bordered" style="width: auto;">
                        <tr>
                            <td class="commanTd" style="min-width: 50px; height: 50px">Year</td>
                            <td class="input-group" style="min-width: 60px; width: 130px;">
                                <div class="input-group-addon">
                                    <i class="glyphicon glyphicon-calendar"></i>
                                </div>
                                <asp:TextBox ID="txtYear" runat="server" AutoPostBack="true" Style="min-width: 100px; min-height: 40px; width: 100px; background-color: white;" CssClass="form-control date1" placeholder="Year Start" AutoCompleteType="Disabled"></asp:TextBox>
                            </td>

                            <td class="commanTd" style="width: 150px;">Starting Day of Week</td>
                            <td style="min-width: 120px;">
                                <asp:DropDownList ID="ddlStartingDayOfWeek" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Sunday" Value="0" />
                                    <asp:ListItem Text="Monday" Value="1" />
                                   <%-- <asp:ListItem Text="Tuesday" Value="2" />
                                    <asp:ListItem Text="Wednesday" Value="3" />
                                    <asp:ListItem Text="Thursday" Value="4" />
                                    <asp:ListItem Text="Friday" Value="5" />
                                    <asp:ListItem Text="Saturday" Value="6" />--%>
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: center; width: auto;">
                                <asp:Button runat="server" Text="Generate" CssClass="Buttons blue" ID="btnWeekGenerate" OnClick="btnWeekGenerate_Click"></asp:Button>
                            </td>
                            <td style="text-align: center; width: auto;">
                                <asp:Button runat="server" Text="View" CssClass="Buttons blue" ID="btnWeekView" OnClick="btnWeekView_Click"></asp:Button>
                            </td>
                        </tr>

                    </table>
                </div>
                <div style="/*display: flex;*/ justify-content: center; align-content: center;">
                    <div id="divWeekDefinition" style="overflow: auto; min-height: 200px; width:auto">
                        <asp:GridView runat="server" ID="gvWeekDefinition" AutoGenerateColumns="False"
                            CssClass="grid cockpit maxheight" ShowHeaderWhenEmpty="true" Width="61%">
                            <Columns>
                                <asp:TemplateField HeaderText="Week Date">
                                    <ItemTemplate>
                                        <span style="white-space: nowrap"><%#Eval("WeekDate")%></span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Week Number">
                                    <ItemTemplate>
                                        <span style="white-space: nowrap"><%#Eval("WeekNumber")%></span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Month Val">
                                    <ItemTemplate>
                                        <span style="white-space: nowrap"><%#Eval("MonthVal")%></span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Year Number">
                                    <ItemTemplate>
                                        <span style="white-space: nowrap"><%#Eval("YearNo")%></span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                            <EmptyDataTemplate>
                                <div style="height: 100%; background-color: white; text-align: center; color: red">No Week Information Available</div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
          <%--  </ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
    <script>
        function HideLabel() {
            var seconds = 2;
            setTimeout(function () {
                document.getElementById("<%=lblMessages.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };

        function resize() {
            var heights = window.innerHeight - 180;
            document.getElementById("divWeekDefinition").style.height = heights + "px";
        }

        $(".dropdown-container3").css("display", "block");

        $(document).ready(function () {
            resize();

            window.onresize = function () {
                resize();
            };

            $('[id$=txtYear]').datepicker({
                minViewMode: 2,
                format: 'yyyy',
                todayHighlight: true,
                autoclose: true,
                language: '<%=GetGlobalResourceObject("CommanResource","dateLanguage")%>',
            });

            $('[id$=txtYear]').keypress(function (e) {
                return false;
            });

            $('[id$=txtYear]').keydown(function (e) {
                return false;
            });
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            resize();
            window.onresize = function () {
                resize();
            };
            $('[id$=txtYear]').datepicker({
                minViewMode: 2,
                format: 'yyyy',
                todayHighlight: true,
                autoclose: true,
                language: '<%=GetGlobalResourceObject("CommanResource","dateLanguage")%>',
            });
            $('[id$=txtYear]').keypress(function (e) {
                return false;
            });
            $('[id$=txtYear]').keydown(function (e) {
                return false;
            });
            $(window).resize(function () {
                var Height = $(window).height() - 390;
                $('#divWeekDefinition').css('height', Height);
            });
        });

        $(window).resize(function () {
            var Height = $(window).height() - 390;
            $('#divWeekDefinition').css('height', Height);
        });

        document.getElementById("headerName").textContent = "Week Definition";

        var user = "<%= (String)Session["username"] %>";
        $("#lblUserID").text("Hi " + user);

        $(".dropdown-container3").css("display", "block");
    </script>
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>--%>
