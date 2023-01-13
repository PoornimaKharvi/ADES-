<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BOM_Details.aspx.cs" Inherits="ADES_22.BOM_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .border {
            width: 95%;
        }
        #gvKitDetails .rowHover:hover {
            background-color: #ffc266;
        }
    </style>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="container-fluid" width="padding:0px">
                <div id="mainContent" class="importdiv">
                    <table id="tblKit" class="filterTable tbl">
                        <asp:HiddenField ID="hdnBOMDetails" runat="server" />
                        <tr>
                            <td>
                                <asp:Button runat="server" CssClass="Buttons" Text="Save" ID="btnSave" Style="margin-left: 1177px" OnClick="btnSave_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="row" style="padding:4px">
                    <div class="col-lg-2" style="height: 83vh; overflow: auto;width:15%;padding:4px">
                        <asp:GridView runat="server" ID="gvKitDetails" Style="background-color:white;width:95%" CssClass="grid" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found !!" EmptyDataRowStyle-CssClass="empty-row-style" ClientIDMode="Static" RowStyle-CssClass="rowHover">
                            <Columns>
                                <asp:TemplateField HeaderText="Kit Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblKName" runat="server" Text='<%# Eval("kitName") %>'></asp:Label>
                                        <asp:HiddenField runat="server" ID="hdnkitNo" Value='<%# Eval("kitNo") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lkbtnSelect" Text="Select" ToolTip="Select" OnClick="lkbtnSelect_Click" ></asp:LinkButton> <%-- OnMouseOver="ChangeColor(this)" OnMouseOut=" ReturnColor(this);--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col-lg-10" style="height: 83vh; overflow: auto;padding:4px">
                        <asp:ListView runat="server" ID="lvItemDetails">
                            <LayoutTemplate>
                                <table id="lvtable" runat="server" class="list border" style="width:95%">
                                    <tr runat="server">
                                        <th runat="server">Select</th>
                                        <th runat="server">Item Name</th>
                                        <th runat="server">Description</th>
                                        <th runat="server">Is Accessories?</th>
                                        <th runat="server">Supplier Name</th>
                                        <th runat="server">Part No.</th>
                                        <th runat="server">Quantity</th>
                                    </tr>
                                    <tr id="ItemPlaceholder" runat="server">
                                    </tr>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:CheckBox runat="server" ID="ckbSelect" Checked='<%# Eval("SelectedCheck") %>'/>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName") %>'>   
                                        </asp:Label>
                                        <asp:HiddenField ID="hdnItemNo" runat="server" Value='<%# Eval("ItemNo") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("ItemDescription") %>'>   
                                        </asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBox runat="server" ID="cbAccessories" Checked='<%# Eval("IsAccessories") %>' OnClick="return false" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSupplierName" runat="server" Text='<%# Eval("SupplierName") %>'>   
                                        </asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPartNo" runat="server" Text='<%# Eval("PartNo") %>'>   
                                        </asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtQty" runat="server" TextMode="Number" Text='<%# Eval("quantity") %>' CssClass="form-control" Style="width: 100px"></asp:TextBox>
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script lang="javascript" type="text/javascript">
        document.getElementById("headerName").textContent = "BOM Details";

        //function ColorChange(ckbSelect) {
        //    var IsChecked = ckbSelect.click;
          
        //    if (IsChecked) {
        //        ckbSelect.parentElement.parentElement.style.backgroundColor = '#ffebcc';
        //        ckbSelect.parentElement.parentElement.style.color = 'black';
        //    }
        //    else  {
        //        ckbSelect.parentElement.parentElement.style.backgroundColor = '#ffffff';
        //        ckbSelect.parentElement.parentElement.style.color = 'black';
        //    }
        //    return;
        //}
        //function SelectedRowSetColor() {


        //    $("#lkbtnSelect").click(function () {
        //        $(".successMerit").css("display") === "block";
        //    });

        //}

        
        //var originalColor = null;

        //function ChangeColor(row) {
            
        //    originalColor = row.style.BackgroundColor;
           
        //    row.style.BackgroundColor = 'red';
           
        //}

        //function ReturnColor(row) {
          
        //    row.style.BackgroundColor = originalColor;
        //}
        //function ChangeRowColor(rowID) {
        //    var color = document.getElementById(rowID).style.backgroundColor;
        //    alert(color);

        //    if (color != 'yellow')
        //        document.getElementById("lblKName").style.backgroundColor = color;

        //    alert(oldColor);

        //    if (color == 'yellow')
        //        document.getElementById(rowID).style.backgroundColor = document.getElementById("lblKName").style.backgroundColor;
        //    else
        //        document.getElementById(rowID).style.backgroundColor = 'yellow';

        //}
    </script>
</asp:Content>
