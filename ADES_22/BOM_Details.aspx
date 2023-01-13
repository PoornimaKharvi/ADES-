<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BOM_Details.aspx.cs" Inherits="ADES_22.BOM_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style type="text/css">
        .border {
            width: 95%;
        }
        #gvKitDetails .rowHover:hover {
            background-color: #ffc266;
        }
        .searchtext{
            width:150px;
            padding:4px;
            margin:15px;
        }
        .no-results-container {
            background: #f34105;
            color: #fff;
            font-weight: bold;
            padding: 1em;
            width: 400px
        }
    </style>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="container-fluid" width="padding:0px">
                <div id="mainContent" class="importdiv" style="padding:0px;">
                    <table id="tblKit" class="filterTable tbl" style="padding:0px;margin-left:0px">
                        <%--<asp:HiddenField ID="hdnBOMDetails" runat="server" />--%>
                        <tr>
                            <td style="padding:0px">
                                <asp:TextBox runat="server" ID="txtSearchKitName"  placeholder="Search KitName" ClientIDMode="Static" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                                </td>
                            <td style="padding-left:150px">
                                <asp:TextBox runat="server" ID="txtSearchItem"  placeholder="Search ItemName" ClientIDMode="Static" AutoCompleteType="Disabled" CssClass="form-control txt-serach-field"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button runat="server" CssClass="Buttons" Text="Save" ID="btnSave" Style="margin-left: 0px" OnClick="btnSave_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="row" style="padding:4px">
                    <div class="col-lg-3" style="height: 83vh; overflow: auto;padding:4px">
                        <asp:GridView runat="server" ID="gvKitDetails" Style="background-color:white;width:100%" CssClass="grid" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found !!" EmptyDataRowStyle-CssClass="empty-row-style" ClientIDMode="Static" RowStyle-CssClass="rowHover">
                            <Columns>
                                <asp:TemplateField HeaderText="Kit Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblKName" runat="server" Text='<%# Eval("kitName") %>'></asp:Label>
                                        <asp:HiddenField runat="server" ID="hdnkitNo" Value='<%# Eval("kitNo") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lkbtnSelect" Text="Select" ToolTip="Select" OnClick="lkbtnSelect_Click" ></asp:LinkButton> 
                                        <%-- OnMouseOver="ChangeColor(this)" OnMouseOut=" ReturnColor(this);--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                         <div id="noResultMessage" class="no-results-container">
                          NO RESULT FOUND!
                        </div>
                    </div>
                    <div class="col-lg-9" style="height: 83vh;width:73%; overflow: auto;padding:4px">
                        <asp:ListView runat="server" ID="lvItemDetails">
                            <LayoutTemplate>
                                <table id="lvtable" runat="server" class="list border emp1_tbl" style="width:100%">
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
                                        <asp:CheckBox runat="server" ID="ckbSelect" Checked='<%# Eval("SelectedCheck") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblItemName" runat="server" CssClass="search_kitname" Text='<%# Eval("ItemName") %>'>   
                                        </asp:Label>
                                        <asp:HiddenField ID="hdnItemNo" runat="server" Value='<%# Eval("ItemNo") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("ItemDescription") %>'>   
                                        </asp:Label>
                                    </td>
                                    <td  style="align-content:Center" >
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
                                        <asp:TextBox ID="txtQty" runat="server" TextMode="Number" Text='<%# Eval("quantity") %>' CssClass="form-control" Style="width: 100px" min="0" ></asp:TextBox>
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
        <Triggers>
             <%--<asp:AsyncPostBackTrigger ControlID="lkbtnSelect" EventName="Click" />--%>
           <%-- <asp:PostBackTrigger ControlID="gvKitDetails" />--%>
            <%--<asp:PostBackTrigger ControlID="lvItemDetails" />--%>
           <%-- <asp:PostBackTrigger ControlID="btnSave" />--%>
        </Triggers>
    </asp:UpdatePanel>

    <script src="QuickSearch-Jquery/jquery.quicksearch.js"></script>

    <script lang="javascript" type="text/javascript">

        $(".dropdown-container1").css("display", "block");
        document.getElementById("headerName").textContent = "BOM Details";
        var user = "<%= (String)Session["username"] %>";
        $("#lblUserID").text("Hi " + user);

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            $('input#txtSearchKitName').quicksearch('table#gvKitDetails tbody tr:not(:first-child)',
                { noResults: "#noResultMessage" });
            //$('input#txtSearchItem').quicksearch('table#gvKitDetails tbody tr:not(:first-child)',
            //    { noResults: "#noResultMessage1" });
            document.getElementById("txtSearchItem").addEventListener("keyup", searchKitName);
            function searchKitName() {

                let txtvalue = $(".txt-serach-field").val().toLowerCase();
                if (txtvalue == null) {
                    for (let i = 1; i < $(".emp1_tbl tr").length; i++) {
                        let tr = $(".emp1_tbl tr")[i];
                        $(tr).css("display", "table-row");
                    }
                    return;
                }
                for (let i = 1; i < $(".emp1_tbl tr").length; i++) {
                    let tr = $(".emp1_tbl tr")[i];
                    let gvtxtval = $(tr).find(".search_kitname").text().toLowerCase();
                    if (gvtxtval.includes(txtvalue)) {
                        $(tr).css("display", "table-row");
                    }
                    else {
                        $(tr).css("display", "none");
                        //  $(".noResultMessage").textContent = "No Result Found!";
                    }
                }
            }

            $(".dropdown-container1").css("display", "block");

            $(function () {
               // debugger;
                $("[id*=lvItemDetails] td").click(function () {
                    selectRow($(this).closest("tr"));
                });
            });

            function selectRow(row) {
              //  debugger;
                var firstInput = row[0].getElementsByTagName('input')[0];
                firstInput.checked = !firstInput.checked;
            }

          
        });

        $('input#txtSearchKitName').quicksearch('table#gvKitDetails tbody tr:not(:first-child)',
            { noResults: "#noResultMessage" });

        document.getElementById("txtSearchItem").addEventListener("keyup", searchKitName);

        function searchKitName() {

            let txtvalue = $(".txt-serach-field").val().toLowerCase();
            if (txtvalue == null) {
                for (let i = 1; i < $(".emp1_tbl tr").length; i++) {
                    let tr = $(".emp1_tbl tr")[i];
                    $(tr).css("display", "table-row");
                }
                return;
            }
            for (let i = 1; i < $(".emp1_tbl tr").length; i++) {
                let tr = $(".emp1_tbl tr")[i];
                let gvtxtval = $(tr).find(".search_kitname").text().toLowerCase();
                if (gvtxtval.includes(txtvalue)) {
                    $(tr).css("display", "table-row");
                }
                else {
                    $(tr).css("display", "none");
                  //  $(".noResultMessage").textContent = "No Result Found!";
                }
            }
        }
      

        $(function () {
           // debugger;
            $("[id*=lvItemDetails] td").click(function () {
                selectRow($(this).closest("tr"));
            });
        });

        function selectRow(row) {
           // debugger;
            var firstInput = row[0].getElementsByTagName('input')[0];
            firstInput.checked = !firstInput.checked;
        }

   

    </script>
</asp:Content>
