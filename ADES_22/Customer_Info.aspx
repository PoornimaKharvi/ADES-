<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Customer_Info.aspx.cs" Inherits="ADES_22.Customer_Info" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" type="text/css">

    <%----- Style CSS -----%>

    <style>
        #li_cust {
            background-color: #f0f0f0;
            color: black;
            font-weight: bold;
        }

        .btncontainer {
            padding: 0px;
            width: 150px;
            position: sticky;
            margin-left: 1180px;
            margin-top: 20px;
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
    </style>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>

            <div id="btncontainer" class="btncontainer">
                <asp:Button ID="Btnadd" runat="server" Text="Add Customer" CssClass="Buttons" OnClick="Btnadd_Click" />
            </div>

            <%----- GridView -----%>

            <div id="gridContainer" class="gridContainer width" runat="server">
                <asp:GridView ID="GridCust" runat="server" CssClass="grid" AutoGenerateColumns="false" Width="100%" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found !!" EmptyDataRowStyle-CssClass="empty-row-style">
                    <Columns>
                        <asp:TemplateField HeaderText="Customer ID">
                            <ItemTemplate>
                                <asp:Label ID="lblCID" runat="server" Text='<%# Eval("ID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Customer Name">
                            <ItemTemplate> 
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

            <div class="modal fade add-edit-modal" id="myConfirmationModal" role="dialog" style="min-width: 300px;">
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

            <div class="modal fade add-edit-modal" id="AddEditModal" role="dialog" style="min-width: 300px;">
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

    <%----- Scripts -----%>

    <script>
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

        $(window).resize(function () {
            var Height = $(window).height() - 290;
            $('.gridContainer').css('height', Height);
        });

        document.getElementById("headerName").textContent = "Customer Details";

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
        });
    </script>
</asp:Content>
