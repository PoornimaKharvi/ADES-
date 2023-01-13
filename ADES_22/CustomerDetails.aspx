<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomerDetails.aspx.cs" Inherits="ADES_22.CustomerDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" type="text/css">
    <script src="js/bootstrap-multiselect.js"></script>
    <link href="Css/bootstrap-multiselect.css" rel="stylesheet" />

    <%----- Style CSS -----%>

    <style>
        #li_cust {
            background-color: #f0f0f0;
            color: black;
            font-weight: bold;
        }

        .btncontainer {
            padding: 0px;
            width: 20%;
            position: sticky;
            margin-left: 85%;
            margin-top: 10px;
        }

        .width {
            width: 94%;
            margin-left: 30px;
            height: 80vh;
            overflow: auto;
        }

        .modal-body{
            color: black;
        }

        .modal-table1{
            width: 100%;
            overflow: unset;
        }

        .modal-table1 tr td{
            padding: 5px;
        }

        .form-control {
            width: 200px;
        }

        .tbbbb{
            margin-left: -38%;
        }
    </style>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div id="btncontainer" class="btncontainer">
                <table class="tbbbb">
                    <tr>
                        <td>
                            <button id="BtnFilter" runat="server" class="Buttons FilterBtn" onclick="return openNav()">
                                <i class="glyphicon glyphicon-filter glyphfilter" runat="server" />
                                Filter
                            </button>
                        </td>
                        <td>
                            &nbsp;&nbsp;<asp:Button ID="Btnadd" runat="server" Text="Add Customer" CssClass="Buttons" OnClick="Btnadd_Click" />
                        </td>
                    </tr>
                </table>
            </div>

            <%----- GridView -----%>

            <div id="gridContainer" class="gridContainer width" runat="server">
                <asp:GridView ID="GridCust" runat="server" CssClass="grid" AutoGenerateColumns="false" Width="100%" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found !!" EmptyDataRowStyle-CssClass="empty-row-style">
                    <Columns>
                        <asp:TemplateField HeaderText="Customer Name">
                            <ItemTemplate> 
                                <asp:HiddenField ID="lblCID" runat="server" Value='<%# Eval("ID") %>' />
                                <asp:Label ID="lblCName" runat="server" Text='<%# Eval("CName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Customer Description">
                            <ItemTemplate>
                                <asp:Label ID="lblCDescription" runat="server" Text='<%# Eval("CDescription") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Region">
                            <ItemTemplate>
                                <asp:Label ID="lblCRegion" runat="server" Text='<%# Eval("CRegion") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <div style="text-align: center">
                                    <asp:LinkButton ID="Btnedit" runat="server" CssClass="glyphicon glyphicon-pencil EditlinkBtn" ToolTip="Edit" OnClick="Btnedit_Click" CommandName="Select" CausesValidation="false" />&nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton ID="Btndlt" runat="server" OnClick="Btndlt_Click" CssClass="glyphicon glyphicon-trash DeletelinkBtn" ToolTip="Delete" CausesValidation="false" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <%----- Delete Modal -----%>

            <div class="modal add-edit-modal" id="myConfirmationModal" role="dialog" style="min-width: 300px;">
                <div class="modal-dialog modal-dialog-centered" style="width: 450px">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Confirmation? <i class="glyphicon glyphicon-remove closeButton" data-dismiss="modal"></i></h4>
                        </div>

                        <div class="modal-body">
                            <asp:Label ID="ConfirmText" runat="server" CssClass="ConfirmText" />
                        </div>

                        <div class="modal-footer">
                            <input type="button" value="Yes" class="btn AddEditBtn" id="DeleteConfirmYes" runat="server" onserverclick="DeleteConfirmYes_ServerClick" />
                            <input type="button" value="No" class="btn btn-danger CancelBtn" id="DeleteConfirmNo" data-dismiss="modal" />
                        </div>
                    </div>
                </div>
            </div>

            <%----- Add/Edit Modal -----%>

            <div class="modal add-edit-modal" id="AddEditModal" role="dialog" style="min-width: 300px;">
                <div class="modal-dialog modal-dialog-centered" style="width: 750px">
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:Label ID="modaltitle" CssClass="modal-title" runat="server" Text="Add Customer" />
                            <a data-dismiss="modal" class="glyphicon glyphicon-remove closeButton"></a>
                        </div>

                        <br />
                        <div class="modal-body">
                            <table class="modal-table1">
                                <tr>
                                    <td>
                                        <asp:HiddenField ID="hfNeworEdit" runat="server" />
                                        <asp:HiddenField ID="hfCID" runat="server" />
                                        <span>Customer Name *</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCName" runat="server" AutoCompleteType="Disabled" CssClass="form-control" TabIndex="2" />
                                    </td>

                                    <td>
                                        <span>Customer Description</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCDescription" runat="server" AutoCompleteType="Disabled" CssClass="form-control" TabIndex="3" />
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <span>Region *</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DdlRegion" runat="server" CssClass="form-control" TabIndex="4" />
                                    </td>
                                </tr> 
                            </table>
                        </div>
                        <br />

                        <div class="modal-footer">
                            <input type="submit" value="Update" class="btn AddEditBtn" tabindex="5" id="Btnedit" onclick="return CustomerValidation();" onserverclick="Btnedit_ServerClick" runat="server" />
                            <input type="button" value="Cancel" class="btn btn-danger CancelBtn" tabindex="6" id="Btneditcancel" onclick="editcancel()" data-dismiss="modal" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
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
                    <span>Customer Name</span>
                    <asp:TextBox ID="FilterCName" runat="server" CssClass="form-control" Width="220px" AutoCompleteType="Disabled" list="CList" />
                    <datalist id="CList" runat="server" clientidmode="static" />
                </div>
            </div>
            <br />

            <div class="OuterDiv">
                <div class="InnerDiv">
                    <span>Region</span><br />
                    <asp:ListBox ID="FilterDDLRegion" runat="server" CssClass="multiDropdown form-control txtsize" ClientIDMode="Static" />
                </div>
            </div>
            <br />

            <div class="filterDiv">
                <asp:Button ID="BtnFilterSearch" runat="server" Text="Apply Filters" CssClass="btnlarge" OnClick="BtnFilterSearch_Click" />
            </div>
        </div>
    </div>

    <%----- Scripts -----%>

    <script>
        document.getElementById("myNav").style.width = "0%";

        function openNav() {
            document.getElementById("myNav").style.width = "340px";
            return false;
        }

        function closeNav() {
            document.getElementById("myNav").style.width = "0%";
            return false;
        }

        function CustomerValidation() {
            if ($("[id$=txtCName]").val() == "") {
                WarningToastr("Customer name is required!");
                $("[id$=txtCName]").focus();
                return false;
            }

            if ($("[id$=Region]").val() == "Select") {
                WarningToastr("Region is required!");
                $("[id$=Region]").focus();
                return false;
            }
            return true;
        }

        $(document).ready(function () {
            ConntrolSetter();
        });

        function ConntrolSetter() {
            $('.multiDropdown').multiselect({
                includeSelectAllOption: true
            });
        }

        $(window).resize(function () {
            var Height = $(window).height() - 290;
            $('.gridContainer').css('height', Height);
        });

        document.getElementById("headerName").textContent = "Customer Details";

        $(".dropdown-container3").css("display", "block");

        var user = "<%= (String)Session["username"] %>";
        $("#lblUserID").text("Hi " + user);

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            $("#toggle_pwd").click(function () {
                $(this).toggleClass("fa-eye fa-eye-slash");
                var type = $(this).hasClass("fa-eye-slash") ? "text" : "password";
                $("[id*=txtEmpPass]").attr("type", type);
            });

            var user = "<%= (String)Session["username"] %>";
            $("#lblUserID").text("Hi " + user);

            function openNav() {
                document.getElementById("myNav").style.width = "340px";
                return false;
            }

            function closeNav() {
                document.getElementById("myNav").style.width = "0%";
                return false;
            }

            $(".dropdown-container3").css("display", "block");
        });
    </script>
</asp:Content>
