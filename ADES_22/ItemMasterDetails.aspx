<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ItemMasterDetails.aspx.cs" Inherits="ADES_22.ItemMasterDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .no-results-container {
            background: #f34105;
            color: #fff;
            font-weight: bold;
            padding: 1em;
            width: 99%;
        }
        #gvItemMaster .rowHover:hover {
            background-color:orange;
           /* #ffc266*/
        }
    </style>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div id="mainContent" class="importdiv">
                    <table id="tblItem" class="filterTable tbl">
                        <asp:HiddenField ID="hdnItemMaster" runat="server" />
                        <tr>
                            <td>
                                <asp:TextBox runat="server" ID="txtSearchItemName" ClientIDMode="Static" Placeholder="Search" CssClass="form-control  textsize1" AutoCompleteType="Disabled"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button runat="server" ID="btnAddDetails" Text="Add Details" CssClass="Buttons" OnClick="btnAddDetails_Click" />
                            </td>
                        </tr>
                    </table>
                    <div class="gridContainer" style="width: 99%; padding: 0px; height: 82vh" id="GridContainer1">
                        <asp:GridView runat="server" ID="gvItemMaster" ClientIDMode="Static" Style="width: 100%; overflow: auto" CssClass="grid" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found !!" EmptyDataRowStyle-CssClass="empty-row-style" OnRowDeleting="gvItemMaster_RowDeleting" RowStyle-CssClass="rowHover">
                            <Columns>
                                <asp:TemplateField HeaderText="Item Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Display Order" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblINo" runat="server" Text='<%# Eval("ItemNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIDescription" runat="server" Text='<%# Eval("ItemDescription") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Is Accessories?" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="cbAccessories" Checked='<%# Eval("IsAccessories") %>' OnClick="return false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Supplier Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblISupplierName" runat="server" Text='<%# Eval("SupplierName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Part No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIPartNo" runat="server" Text='<%# Eval("PartNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <div style="text-align: center">
                                            <asp:LinkButton runat="server" ID="lkbtnEdit" CssClass="glyphicon glyphicon-pencil EditlinkBtn " ToolTip="Edit" OnClick="lkbtnEdit_Click"></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lkbtnDelete" CssClass="glyphicon glyphicon-trash DeletelinkBtn" ToolTip="Delete" CommandName="Delete"></asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <div id="noResultMessage" class="no-results-container" style="width:100%">
                           NO RESULT FOUND!
                        </div>
                    </div>
                </div>
            </div>

            <%--------------------------Add Item Master Modal---------%>

            <div class="modal  add-edit-modal" id="AddItemMaster" role="dialog" style="min-width: 400px;">
                <div class="modal-dialog " style="width: 620px">
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:Label class="modal-title" ID="ItemMasterTitle" runat="server">Add Item Master</asp:Label>
                            <a data-dismiss="modal" class="glyphicon glyphicon-remove closeButton"></a>
                            <asp:HiddenField runat="server" ID="hdnAddItemMaster" />
                        </div>
                        <div class="modal-body">
                            <table class="modal-table">
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="lblItemName">Item Name *</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtItemName" CssClass="form-control textsize" AutoCompleteType="Disabled"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblItemNo">Display Order</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtItemNo" CssClass="form-control textsize" TextMode="Number"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="lblDescription">Description</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtDescription" CssClass="form-control textsize" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblAccessories">Is Accessories?</asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBox runat="server" ID="cbxAccessories" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="lblSupplierName">Supplier Name</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtSupplierName" CssClass="form-control textsize" AutoCompleteType="Disabled"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblPartNo">Part No.</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtPartNo" CssClass="form-control textsize" AutoCompleteType="Disabled"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <asp:Button CssClass="btn AddEditBtn" ID="btnSave" runat="server" Text="Add" OnClientClick="return ItemValidationInsert();" OnClick="btnSave_Click" />
                            <asp:Button CssClass="btn btn-danger CancelBtn" ID="btnCancel" Text="Cancel" runat="server" data-dismiss="modal" />
                        </div>
                    </div>
                </div>
            </div>

            <%------------------------ Delete Confirmation Modal------------------------------------------%>

            <div class="modal  add-edit-modal" id="myConfirmationModal" role="dialog" style="min-width: 300px;">
                <div class="modal-dialog " style="width: 450px">
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:Label class="modal-title" runat="server">Confirmation</asp:Label>

                            <a data-dismiss="modal" class="glyphicon glyphicon-remove closeButton"></a>
                        </div>
                        <div class="modal-body">
                            <span id="confirmationmessageText" class="ConfirmText">Do you want to delete this record?</span>
                        </div>
                        <div class="modal-footer">
                            <asp:Button Text="Yes" ID="btnYes" class="btn AddEditBtn" runat="server" OnClick="btnYes_Click" />
                            <asp:Button Text="No" ID="btnNo" runat="server" class="btn btn-danger CancelBtn" data-dismiss="modal" />
                        </div>
                    </div>
                </div>
            </div>

            <%-----------------------------------------------------------------------------------------%>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script src="QuickSearch-Jquery/jquery.quicksearch.js"></script>
    <script>
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            $('input#txtSearchItemName').quicksearch('table#gvItemMaster tbody  tr:not(:first-child)',
                { noResults: "#noResultMessage" });
            $(".dropdown-container1").css("display", "block");
        });

        $('input#txtSearchItemName').quicksearch('table#gvItemMaster tbody  tr:not(:first-child)',
            { noResults: "#noResultMessage" });
        
        function ItemValidationInsert() {

            if ($("[id$=txtItemName]").val() == "") {
                WarningToastr("Item Name is required!");
                $("[id$=txtItemName]").focus();

                return false;
            }
            return true;
        };
        document.getElementById("headerName").textContent = "Item Master Details";
        $(".dropdown-container1").css("display", "block");
        var user = "<%= (String)Session["username"] %>";
        $("#lblUserID").text("Hi " + user);
    </script>
</asp:Content>
