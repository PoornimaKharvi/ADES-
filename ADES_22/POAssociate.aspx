<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="POAssociate.aspx.cs" Inherits="ADES_22.POAssociate" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .gvKit {
            height: 80vh;
            overflow: auto;
            width: 98%;
            padding: 5px;
        }
    </style>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div id="mainContent" class="importdiv">
                    <table id="tblKit" class="filterTable tbl">
                        <tr>
                            <td>
                                <asp:Label runat="server">Customer</asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlCustomer" CssClass="form-control" Width="190px" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged"  AutoPostBack="true">
                                    <%--OnTextChanged="ddlCustomer_TextChanged"--%>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label runat="server" class="col-form-label" Text="Region"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlRegion" runat="server" CssClass="form-control" Width="190px" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label runat="server">PO Number</asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlPONo" CssClass="form-control" Width="190px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button runat="server" ID="btnView" Text="View" CssClass="Buttons" OnClick="btnView_Click" />
                               <%-- <asp:Button runat="server" ID="btnExport" Text="Export" CssClass="Buttons" OnClick="btnExport_Click" />--%>
                                   <asp:Button ID="btnPrint" runat="server" Text="Export" CssClass="Buttons" OnClick="btnPrint_Click" />

                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblStatus"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table class="filterTable tbl">
                        <tr>
                            <td>
                                <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="Buttons" OnClick="btnSave_Click" />
                            </td>
                            <td>
                                <asp:Button runat="server" ID="btnAddEdit" Text="Add/Edit Kits" CssClass="Buttons" OnClick="btnAddEdit_Click" />
                            </td>
                            <td>
                                <asp:Button runat="server" ID="btnApprove" Text="RequestApproval" CssClass="Buttons" OnClick="btnApprove_Click" Width="130px" />

                            </td>
                           
                        </tr>
                    </table>
                </div>
            </div>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="gvKit" id="gContainerPO" style="height:75vh">
                        <asp:GridView ID="gvKitDetails" runat="server" AutoGenerateColumns="false" CssClass="grid" Style="width: 100%" OnRowDataBound="gvPOAssociate_RowDataBound" DataKeyNames="KitName" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found !!" EmptyDataRowStyle-CssClass="empty-row-style">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <i runat="server" id="plusMinus" class="glyphicon glyphicon-plus plus" onclick="DrillDownFun(this);" tooltip="Click"></i>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Kit Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblkitname" runat="server" Text='<%# Eval("KitName") %>'></asp:Label>
                                        <div id="ExpandCollapse" runat="server" class="inner-grid-div" style="display: none; overflow: auto; width: 100%;">
                                            <asp:GridView ID="gvItemDetails" runat="server" AutoGenerateColumns="false" Style="width: 100%" CssClass="ChildGrid" howHeaderWhenEmpty="true" EmptyDataText="No Data Found !!" EmptyDataRowStyle-CssClass="empty-row-style">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Item Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                            <asp:HiddenField ID="hdnItemNo" runat="server" Value='<%# Eval("ItemNo") %>' />
                                                            <asp:HiddenField runat="server" ID="hdnkitNo" Value='<%# Eval("KitNo") %>' />
                                                            <asp:HiddenField runat="server" ID="hdnKitQty" Value='<%# Eval("KitQty") %>' />
                                                            <asp:HiddenField runat="server" ID="hdnItemQty" Value='<%# Eval("ItemQty") %>' />
                                                            <asp:HiddenField runat="server" ID="hdnItemDesc" Value='<%# Eval("ItemDescription") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Part No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIPartNo" runat="server" Text='<%# Eval("PartNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Is Accessories?">
                                                        <ItemTemplate>
                                                            <asp:CheckBox runat="server" ID="cbxIsAccessories" Checked='<%# Eval("IsAccessories") %>' OnClick="return false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Supplier Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblISupplierName" runat="server" Text='<%# Eval("SupplierName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtQuantity" runat="server" TextMode="Number" Text='<%# Eval("Qty") %>' CssClass="form-control" Style="width: 100px" min="0"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Short Supply">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtShortage" CssClass="form-control" Text='<%# Eval("Shortage") %>' Style="width: 100px" runat="server" ></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <%--<asp:PostBackTrigger ControlID="gvKitDetails" />--%>
                    <%--  <asp:PostBackTrigger  ControlID="gvItemDetails"/>--%>
                    <%--<asp:PostBackTrigger  ControlID="btnSave"/>
                    <asp:PostBackTrigger ControlID="btnAddEdit" />--%>

                    <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnAddEdit" EventName="Click" />
                    <asp:PostBackTrigger ControlID="btnPrint" />
                    <%-- <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />

                    <asp:AsyncPostBackTrigger ControlID="" EventName="Click" />--%>
                </Triggers>
            </asp:UpdatePanel>
            <%-----------------------------Modal popup for Add/Edit Kits-----------------------%>

            <div class="modal fade add-edit-modal" id="AddEditKits" role="dialog" style="min-width: 400px;">
                <div class="modal-dialog " style="width: 800px">
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:Label class="modal-title" runat="server" ID="AddEditKitTitle">Assign Kits</asp:Label>
                            <a data-dismiss="modal" class="glyphicon glyphicon-remove closeButton"></a>
                            <asp:HiddenField runat="server" ID="hdnModalKits" />
                        </div>
                        <div class="modal-body">
                            <div style="overflow: auto; height: 70vh">
                                <asp:ListView runat="server" ID="lvKitDetails">
                                    <LayoutTemplate>
                                        <table runat="server" class="list" style="width: 95%">
                                            <tr runat="server">
                                                <th runat="server">Select</th>
                                                <th runat="server">Kit Name</th>
                                                <th runat="server">Quantity</th>
                                            </tr>
                                            <tr id="ItemPlaceholder" runat="server">
                                            </tr>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:CheckBox runat="server" ID="ckbSelect" Checked='<%# Eval("Selectedkit") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblKitName" runat="server" Text='<%# Eval("KitName") %>'>   
                                                </asp:Label>
                                                <asp:HiddenField runat="server" ID="hdnKitNo" Value='<%# Eval("KitNo") %>' />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtQty" CssClass="form-control" Style="width: 100px" runat="server" TextMode="Number" Text='<%# Eval("KQty") %>' min="0"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <div class="emptylist">
                                            <span runat="server">No Data Found!</span>
                                        </div>
                                    </EmptyDataTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button CssClass="btn AddEditBtn" ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" /><%--OnClick="btnAdd_Click"--%>
                            <asp:Button CssClass="btn btn-danger CancelBtn" ID="btnCancel" Text="Cancel" runat="server" data-dismiss="modal" />
                        </div>
                    </div>
                </div>
            </div>

            <%------------------------Modal popup for Request/Approve Confirmation--------------%>
            <div class="modal fade add-edit-modal" id="myConfirmationModal" role="dialog" style="min-width: 300px;">
                <div class="modal-dialog " style="width: 450px">
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:Label class="modal-title" runat="server">Confirmation</asp:Label>

                            <a data-dismiss="modal" class="glyphicon glyphicon-remove closeButton"></a>
                        </div>
                        <div class="modal-body">
                            <asp:Label runat="server" ID="confirmationmessageText" class="ConfirmText">Are you sure,you want to Request for Approve?</asp:Label>
                        </div>
                        <div class="modal-footer">
                            <asp:Button runat="server" ID="ConfirmYes" CssClass="btn AddEditBtn" Text="Yes" OnClick="ConfirmYes_Click" />
                            <asp:Button runat="server" ID="ConfirmNo" CssClass="btn btn-danger CancelBtn" Text="No" data-dismiss="modal" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


    <%--------------------------------------------------------------------------------------%>
    <script type="text/javascript">
        function DrillDownFun(element) {
            if ($(element).hasClass("plus")) {
                $(element).removeClass('plus');
                $(element).removeClass('glyphicon glyphicon-plus');
                $(element).addClass('glyphicon glyphicon-minus');
                /*$(element).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(element).closest('tr').find('.inner-grid-div').css('display', 'block') + "</td></tr>");*/
                $(element).closest('tr').find('.inner-grid-div').css('display', 'block');
            }
            else {
                $(element).addClass('plus');
                $(element).removeClass('glyphicon glyphicon-minus');
                $(element).addClass('glyphicon glyphicon-plus');
                $(element).closest('tr').find('.inner-grid-div').css('display', 'none');
            }
        };
        $(".dropdown-container1").css("display", "block");
        document.getElementById("headerName").textContent = "Customer PO BOM Association Details ";
        var user = "<%= (String)Session["username"] %>";
        $("#lblUserID").text("Hi " + user);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            $(".dropdown-container1").css("display", "block");
        });
    </script>
</asp:Content>
