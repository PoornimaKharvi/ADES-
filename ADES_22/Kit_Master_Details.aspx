<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Kit_Master_Details.aspx.cs" Inherits="ADES_22.Kit_Master_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .no-results-container {
            background: #f34105;
            color: #fff;
            font-weight: bold;
            padding: 1em;
            width: 500px
        }
        #gvKitMaster .rowHover:hover {
            background-color: orange;
        }
    </style>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>

            <div class="container-fluid">
                <div id="mainContent" class="importdiv">
                    <table id="tblKit" class="filterTable tbl">
                        <asp:HiddenField ID="hdnKitMaster" runat="server" />
                        <tr>
                            <td>
                                <asp:TextBox runat="server" ID="txtSearchKitName" Placeholder="Search " CssClass="form-control  textsize1 txt-serach-field" ClientIDMode="Static" AutoCompleteType="Disabled" ></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button runat="server" ID="btnAddDetails" Text="Add Details" CssClass="Buttons" OnClick="btnAddDetails_Click"/>
                            </td>
                        </tr>
                    </table>
                  

                    <div class="gridContainer" style="width: 50%; padding-left: 0px;margin-right:658px; height: 80vh" id="GridContainer1">
                        <asp:GridView runat="server" ID="gvKitMaster" Style="width: 100%;overflow:auto" CssClass="grid emp1_tbl" ClientIDMode="Static" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found !!" EmptyDataRowStyle-CssClass="empty-row-style" OnRowDeleting="gvKitMaster_RowDeleting" RowStyle-CssClass="rowHover">
                            <Columns>
                                <asp:TemplateField HeaderText="Kit Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblKName" runat="server" Text='<%# Eval("kitName") %>' AutoPostBack="true" CssClass="search_kitname"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Display Order" ItemStyle-HorizontalAlign="Center" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblKNo" runat="server" Text='<%# Eval("kitNo") %>' AutoPostBack="true"></asp:Label>
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
            <%------------------------------Add Kit Master Modal--------------------------------%>
            <div class="modal  add-edit-modal" id="AddKitMaster" role="dialog" style="min-width: 400px;">
                <div class="modal-dialog " style="width: 600px">
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:Label class="modal-title" runat="server" ID="KitMasterTitle">Add Kit Master</asp:Label>
                            <a data-dismiss="modal" class="glyphicon glyphicon-remove closeButton"></a>
                            <asp:HiddenField runat="server" ID="hdnModalKitMaster" />
                        </div>
                        <div class="modal-body">
                            <table class="modal-table">
                                <tr>

                                    <td>
                                        <asp:Label runat="server" ID="lblKitName">Kit Name *</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtKitName" AutoCompleteType="Disabled" CssClass="form-control textsize "></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblKitNo">Display Order</asp:Label>
                                    </td>
                                    <td>

                                        <asp:TextBox runat="server" ID="txtKitNo" AutoCompleteType="Disabled" TextMode="Number" CssClass="form-control textsize"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <asp:Button CssClass="btn AddEditBtn" ID="btnSave" runat="server" Text="Add" OnClientClick="return KitValidationInsert();" OnClick="btnSave_Click" />
                            <asp:Button CssClass="btn btn-danger CancelBtn" ID="btnCancel" Text="Cancel" runat="server" data-dismiss="modal" />
                        </div>
                    </div>
                </div>
            </div>
            <%------------------------------Delete Confirmation Modal-------%>
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
                            <asp:Button Text="Yes" ID="btnYes" class="btn AddEditBtn" runat="server" OnClick="btnYes_Click" /><%-- onclick="btnYes_Click" OnClientClick="return KitValidationInsert();" />--%>
                            <asp:Button Text="No" ID="btnNo" runat="server" class="btn btn-danger CancelBtn" data-dismiss="modal" />
                        </div>
                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>


    <%---------------------------------------------------------------------------------%>
    <script src="QuickSearch-Jquery/jquery.quicksearch.js"></script>
    <script>
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            $('input#txtSearchKitName').quicksearch('table#gvKitMaster tbody tr:not(:first-child)',
                { noResults: "#noResultMessage" });
            //function searchKitName() {

            //    let txtvalue = $(".txt-serach-field").val().toLowerCase();
            //    if (txtvalue == null) {
            //        for (let i = 1; i < $(".emp1_tbl tr").length; i++) {
            //            let tr = $(".emp1_tbl tr")[i];
            //            $(tr).css("display", "table-row");
            //        }
            //        return;
            //    }
            //    for (let i = 1; i < $(".emp1_tbl tr").length; i++) {
            //        let tr = $(".emp1_tbl tr")[i];
            //        let gvtxtval = $(tr).find(".search_kitname").text().toLowerCase();
            //        if (gvtxtval.includes(txtvalue)) {
            //            $(tr).css("display", "table-row");
            //        }
            //        else {
            //            $(tr).css("display", "none");
            //            $(".noResultMessage").textContent = "No Result Found!";
            //        }
            //    }
            //}
            $(".dropdown-container1").css("display", "block");

        });
        $('input#txtSearchKitName').quicksearch('table#gvKitMaster tbody tr:not(:first-child)',
            { noResults: "#noResultMessage" });

       /* document.getElementById("txtSearchKitName").addEventListener("keyup", searchKitName);*/

        document.getElementById("headerName").textContent = "Kit Master Details";
        var user = "<%= (String)Session["username"] %>";
        $("#lblUserID").text("Hi " + user);
        function KitValidationInsert() {
            if ($("[id$=txtKitName]").val() == "") {
                WarningToastr("Kit Name is required!");
                $("[id$=txtKitName]").focus();
                return false;
            }
            return true;
        };

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
                    $(".noResultMessage").textContent = "No Result Found!";
                }
            }
        }
        $(".dropdown-container1").css("display", "block");

    </script>

 
</asp:Content>
