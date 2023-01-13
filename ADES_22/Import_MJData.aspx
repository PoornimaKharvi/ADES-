<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Import_MJData.aspx.cs" Inherits="ADES_22.Import_MJData" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />

    <%----- Style CSS -----%>

    <style>
        #li_mj {
            background-color: #f0f0f0;
            color: black;
            font-weight: bold;
        }

        .table tr {
            border: 5px solid white;
        }

        .dbtn {
            color: white;
            background-color: white;
            border: 3px solid white;
            width: 35px;
        }

        .gc {
            margin-left: 1%;
            width: 98%;
            overflow: auto;
            height: 30%;
        }

        .grid {
            width: 98%;
            overflow: auto;
        }

        #main {
            width: 100%;
            padding: 0px;
            margin: 5px;
        }

        #main1 {
            width: auto;
            padding: 0px;
            margin: 5px;
        }

        #ddlpname {
            width: 30%;
        }

        .gridContainer2 {
            overflow: auto;
            padding: 0px;
            margin-left: 50px;
            width: 90%;
        }

        .tbl1 {
            width: 100%;
            margin-left: 50px;
        }

        #DivVerify {
            margin-top: 5px;
            margin-left: 450px;
            width: 100%;
            text-align: center;
        }

        .modal-body {
            color: black;
        }

        .wd {
            width: 120px;
        }

        .tbVerify {
            width: 30%;
            text-align: center;
        }
    </style>

    <%----- Import1 -----%>

    <div class="container-fluid">
        <div id="main1">
            <div id="import1" runat="server">
                <asp:HiddenField ID="hiddenMj" runat="server" />
                <asp:HiddenField ID="hdnProdName" runat="server" />
                <table class="table">
                    <tr>
                        <td>
                            <asp:Label runat="server" Text="Tally PO Number:" />
                        </td>
                        <td>
                            <input type="text" id="txtpo" runat="server" class="form-control txtpo" autocomplete="off" list="datalist" onchange="SearchPO()" />
                            <datalist id="datalist"></datalist>
                        </td>
                        <td>
                            <asp:Button ID="Button6" runat="server" CssClass="dbtn" />
                        </td>
                        <td>
                            <asp:Label runat="server" Text="PO Date:" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtpodate" runat="server" AutoCompleteType="Disabled" CssClass="form-control txtpo" />
                        </td>
                        <td>
                            <asp:Button ID="Button1" runat="server"  CssClass="dbtn" />
                            <asp:Button ID="Button2" runat="server" CssClass="dbtn" />
                        </td>
                        <td>
                            <asp:Button ID="btnImport" runat="server" Text="Import Tally" CssClass="Buttons" OnClick="btnImport_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnView" runat="server" Text="View" CssClass="Buttons" OnClick="btnView_Click" />
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label runat="server" Text="Product Name:" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlpname" runat="server" CssClass="form-control" />
                        </td>
                        <td>
                            <asp:Button ID="Button7" runat="server" CssClass="dbtn" />
                        </td>
                        <td>
                            <asp:Label runat="server" Text="MJ Number:" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlmjnum" runat="server" CssClass="form-control" />
                        </td>
                        <td>
                            <asp:Button ID="Button3" runat="server" CssClass="dbtn" />
                            <asp:Button ID="Button4" runat="server" CssClass="dbtn" />
                        </td>
                        <td>
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" CssClass="Buttons" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" CssClass="Buttons" />
                        </td>
                    </tr>
                </table>
            </div>

            <%-----  1st GridView -----%>

            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div id="gridContainer1" class="gridContainer gc" runat="server">
                        <asp:GridView ID="gridTally" CssClass="grid" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found !!" EmptyDataRowStyle-CssClass="empty-row-style">
                            <Columns>
                                <asp:TemplateField HeaderText="Item Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl1" runat="server" Text='<%#Eval("ItemName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Go Down">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl2" runat="server" Text='<%#Eval("GoDown") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Serial #">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl3" runat="server" Text='<%#Eval("SlNo") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl4" runat="server" Text='<%#Eval("Quantity") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl5" runat="server" Text='<%#Eval("Unit") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </ContentTemplate>

                <Triggers>
                    <asp:PostBackTrigger ControlID="gridTally" />
                    <asp:AsyncPostBackTrigger ControlID="btnImport" EventName="Click" /> 
                    <asp:AsyncPostBackTrigger ControlID="btnView" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnDelete" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>

    <%----- Import2 -----%>

    <div id="import2" runat="server" style="margin-top: 0px">
        <div id="main">
            <table class="tbl1">
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <tr>
                    <td>
                        <label runat="server">Tally PO Number</label></td>
                    <td>
                        <asp:TextBox runat="server" ID="txtPo1" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <label runat="server">PO Date</label></td>
                    <td>
                        <asp:TextBox runat="server" ID="POdate1" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="height: 10px;"></td>
                </tr>
                <tr>
                    <td>
                        <label runat="server">Product Name</label></td>
                    <td>
                        <asp:TextBox runat="server" ID="ProdName1" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <label runat="server">MJ Number</label></td>
                    <td>
                        <asp:TextBox runat="server" ID="MJno1" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
            </table>

            <%----- 2nd GridView -----%>

            <div style="height: 10px;"></div>
            <div id="Div1" class="gridContainer2" runat="server">
                <asp:GridView ID="gridTally2" CssClass="grid" runat="server" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found !!" EmptyDataRowStyle-CssClass="empty-row-style">
                    <Columns>
                        <asp:TemplateField HeaderText="Item Name">
                            <ItemTemplate>
                                <asp:Label ID="lbl1" runat="server" Text='<%# Eval("ItemName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Go Down">
                            <ItemTemplate>
                                <asp:Label ID="lbl2" runat="server" Text='<%#Eval("GoDown") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Serial #">
                            <ItemTemplate>
                                <asp:Label ID="lbl3" runat="server" Text='<%#Eval("SerialNo") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:Label ID="lbl4" runat="server" Text='<%#Eval("Quantity") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Unit">
                            <ItemTemplate>
                                <asp:Label ID="lbl5" runat="server" Text='<%#Eval("Unit") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <%----- Verify Table -----%>

            <div style="height: 10px"></div>
            <div id="DivVerify">
                <table class="tbVerify">
                    <tr>
                        <td>
                            <label runat="server" style="font-size: large">Verified? </label>
                        </td>
                        <td>
                            <asp:Button ID="Button5" runat="server" Text="Yes" CssClass="successBtn" OnClick="btnVerify_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnCancelImport" runat="server" Text="Cancel Import" CssClass="btn btn-danger CancelBtn wd" OnClick="btnCancelImport_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>

    <%----- FileUpload Modal -----%>

    <div class="modal add-edit-modal" id="FileUploadModal" role="dialog" style="min-width: 300px;">
        <div class="modal-dialog modal-dialog-centered" style="width: 450px">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Choose File<i class="glyphicon glyphicon-remove closeButton" data-dismiss="modal"></i></h4>
                </div>

                <div class="modal-body">
                    <asp:FileUpload ID="globe_Check" CssClass="form-control" runat="server" BackColor="LightGray" />
                </div>

                <div class="modal-footer">
                    <input type="button" value="Import" class="btn AddEditBtn" id="saveConfirmYes1" runat="server" onserverclick="saveConfirmYes1_ServerClick" />
                    <input type="button" value="Cancel" class="btn btn-danger CancelBtn" id="saveConfirmNo1" onclick="saveConfirmNo1()" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>

    <%----- Delete Modal -----%>

    <div class="modal add-edit-modal" id="myConfirmationModal" role="dialog" style="min-width: 300px;">
        <div class="modal-dialog modal-dialog-centered" style="width: 450px">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Confirmation?<i class="glyphicon glyphicon-remove closeButton" data-dismiss="modal"></i></h4>
                </div>

                <div class="modal-body">
                    <span id="confirmationmessageText" class="ConfirmText">Are you sure want to delete this record?</span>
                </div>

                <div class="modal-footer">
                    <input type="button" value="Yes" class="btn AddEditBtn" id="saveConfirmYes" onserverclick="saveConfirmYes_ServerClick" runat="server" style="background-color: #093d81; color: white" />
                    <input type="button" value="No" class="btn btn-danger CancelBtn" id="saveConfirmNo" onclick="saveConfirmNo()" style="background-color: red; color: white" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>

    <%----- Validation Modal -----%>

    <div class="modal add-edit-modal" id="myTimeValidation" role="dialog" style="min-width: 300px;">
        <div class="modal-dialog modal-dialog-centered" style="width: 450px">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title"">Error?<i class="glyphicon glyphicon-remove closeButton" data-dismiss="modal"></i></h4>
                </div>
                <div class="modal-body">
                    <span id="ValidationText" class="ConfirmText">Data for PO already exists. Do you want to load again?</span>
                </div>
                <div class="modal-footer">
                    <input type="button" value="Yes" class="btn AddEditBtn" id="LoadData_Yes" runat="server" onserverclick="LoadData_Yes_ServerClick" />
                    <input type="button" value="No" class="btn btn-danger CancelBtn" id="LoadData_No" runat="server" onserverclick="LoadData_No_ServerClick" />
                </div>
            </div>
        </div>
    </div>

    <%----- Scripts -----%>
    
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            var wHeight = $(window).height() - 280;
            $('.gridContainer1').css('height', wHeight);
            Searchcompo();

            var wHeight1 = $(window).height() - 260;
            $('.gridContainer2').css('height', wHeight1);
        });

        $(document).ready(function () {
            var wHeight = $(window).height() - 280;
            $('.gridContainer1').css('height', wHeight);

            var wHeight1 = $(window).height() - 260;
            $('.gridContainer2').css('height', wHeight1);
            Searchcompo();
        });

        $('[id*=ddlmjnum]').change(function () {
            var selectedmj = $(this).val();
            $('[id*=hiddenMj]').val(selectedmj);
            var param = $("[id$=txtpo]").val();
            param = '{poNum:"' + param + '",mjNo:"' + selectedmj + '"}';
            $.ajax({
                type: "POST",
                url: "Import_MJData.aspx/SearchMJprod",
                contentType: "application/json; charset=utf-8",
                data: param,
                dataType: "json",
                success: OnSuccessGetMJproduct,
                error: function (response) {
                    console.log(response.d);
                }
            });
        });

        function OnSuccessGetMJproduct(Result) {
            var stringb = '';
            $("[id$=ddlpname").val(Result.d);
            $('[id*=hdnProdName]').val(Result.d);
        }

        $('[id*=ddlpname]').change(function () {
            var selectedProd = $(this).val();
            $('[id*=hdnProdName]').val(selectedProd);
            var param = $("[id$=txtpo]").val();
            param = '{poNum:"' + param + '",prodName:"' + selectedProd + '"}';
            $.ajax({
                type: "POST",
                url: "Import_MJData.aspx/SearchMJ",
                contentType: "application/json; charset=utf-8",
                data: param,
                dataType: "json",
                success: OnSuccessGetMJNUM,
                error: function (response) {
                    console.log(response.d);
                }
            });
        });

        function OnSuccessGetMJNUM(Result) {
            var stringb = '';
            $("[id$=ddlmjnum").val(Result.d);
            $('[id*=hiddenMj]').val(Result.d);
        }

        function Searchcompo() {
            var param = { blank: '' };
            $.ajax({
                type: "POST",
                url: "Import_MJData.aspx/GetAutoCompleteData",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(param),
                dataType: "json",
                success: OnSuccessGetFirst,
                error: function (response) {
                    console.log(response.d);
                }
            });
        }

        function OnSuccessGetFirst(Result) {
            var i = 0;
            var options = '';
            for (i; i < Result.d.length; i++) {
                options += '<option value="' + Result.d[i] + '" />';
                document.getElementById('datalist').innerHTML = options;
            }
        }

        function SearchPO() {
            var param = $("[id$=txtpo]").val();
            param = '{blank:"' + param + '"}';

            $.ajax({
                type: "POST",
                url: "Import_MJData.aspx/SearchPODetails",
                contentType: "application/json; charset=utf-8",
                data: param,
                dataType: "json",
                success: OnSuccessGetFirst1,
                error: function (response) {
                    console.log(response.d);
                }
            });
        }

        function OnSuccessGetFirst1(Result) {
            var i = 0;
            console.log("resINvoice", Result);
            $("[id$=ddlpname").empty();
            $("[id$=ddlmjnum").empty();
            var option = '';
            var stringb = '';
            var abc = '';
            for (i; i < Result.d.length; i++) {
                $("[id$=ddlmjnum").append($("<option></option>").html(Result.d[i]["mjnum"]));
                $("[id$=ddlpname").append($("<option></option>").html(Result.d[i]["prodname"]));
            }
            $("[id$=txtpodate]").val((Result.d[0]["podate"]).split(" ")[0]);
            $('[id*=hdnProdName]').val(Result.d[0]["prodname"]);
            $('[id*=hiddenMj]').val(Result.d[0]["mjnum"]);
        }

        document.getElementById("headerName").textContent = "Import MJ Data";

        var user = "<%= (String)Session["username"] %>";
        $("#lblUserID").text("Hi " + user);
    </script>
</asp:Content>
