<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ImportInvoice.aspx.cs" Inherits="ADES_22.ImportInvoice" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .SignalFileLabel {
            font-size: 20px;
            margin-left: 7px;
            color: darkorange;
        }
    </style>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>

            <div class="container-fluid">
                <div id="main1">
                    <div id="import1" runat="server" class="importdiv">
                        <table class="filterTable tbl" id="tbl1">
                            <asp:HiddenField ID="hdnInvoice" runat="server" />
                            <tr>
                                <td>
                                    <asp:Label runat="server">Tally PO Number</asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtPO" runat="server" CssClass="form-control textsize1" list="dlPoNumber" AutoCompleteType="Disabled" AutoPostBack="true" OnTextChanged="txtPO_TextChanged"></asp:TextBox>
                                    <datalist runat="server" id="dlPoNumber" clientidmode="static"></datalist>
                                </td>
                                <td>
                                    <asp:Label runat="server">Invoice No</asp:Label></td>
                                <td>
                                    <asp:TextBox ID="ddlInvoice" runat="server" CssClass="form-control textsize1" list="dlInvNo" AutoPostBack="true" AutoCompleteType="Disabled" OnTextChanged="ddlInvoice_SelectedIndexChanged"></asp:TextBox>

                                    <datalist runat="server" id="dlInvNo" clientidmode="static"></datalist>
                                </td>
                                <td>
                                    <asp:Label runat="server">Customer</asp:Label></td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtCust" AutoCompleteType="Disabled" CssClass="form-control textsize1" AutoPostBack="true"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server">PO Date</asp:Label></td>
                                <td>
                                    <asp:TextBox runat="server" ID="textpodate" AutoCompleteType="Disabled" CssClass="form-control textsize1" AutoPostBack="true"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label runat="server">Invoice Date</asp:Label></td>
                                <td>
                                    <asp:TextBox runat="server" ID="textinvoicedate" AutoCompleteType="Disabled" CssClass="form-control textsize1"></asp:TextBox></td>
                                <td colspan="6">
                                    <asp:Button runat="server" ID="btnView" CssClass="Buttons" Text="View" OnClick="btnView_Click" />
                                    <asp:Button runat="server" ID="btnClear" CssClass="Buttons" Text="Clear" OnClick="btnClear_Click" />
                                    <asp:Button runat="server" ID="btnImport" CssClass="Buttons" Text="Import Invoice" OnClick="btnImport_Click" />
                                    <asp:Button runat="server" ID="btnDelete" CssClass="Buttons" Text="Delete" Visible="false" OnClick="btnDelete_Click" />
                                </td>
                            </tr>
                        </table>

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <div id="gridContainer" class="gridContainer" style="height: 60vh" runat="server">
                                    <asp:GridView runat="server" CssClass="grid" Style="width: 96.5%" AutoGenerateColumns="False" ID="gridProducts" OnRowDataBound="gridProducts_RowDataBound" GridLines="Both">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Product Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl1" runat="server" Text='<%#Eval("productname") %>' />
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quantity">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl2" runat="server" Text='<%#Eval("quantity") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl3" runat="server" Text='<%#Eval("unit") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Invoice Value">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl4" runat="server" Text='<%#Eval("InvoiceValue") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="MJNumber">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="dropdown4" runat="server" CssClass="form-control" Width="150px" AppendDataBoundItems="true"></asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="gridProducts" />

                                <asp:AsyncPostBackTrigger ControlID="btnImport" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnView" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnDelete" EventName="Click" />

                            </Triggers>
                        </asp:UpdatePanel>

                        <div class="text-left" style="margin-top: -100px;" id="pdffile" runat="server">
                            <a runat="server" id="A1" onclick="return signalClick();" style="margin-left: 4px; display: inline-block; color: #216b69; font-size: 18px">
                                <i class="glyphicon glyphicon-cloud-upload" style="font-size: 25px; color: orange"></i>
                                <asp:Label runat="server" CssClass="SignalFileLabel">Invoice pdf : </asp:Label>
                            </a>

                            <asp:LinkButton runat="server" ID="signalfilename" Font-Size="Medium" OnClick="signalfilename_Click" ForeColor="black"></asp:LinkButton>

                            <asp:FileUpload runat="server" ID="selectSignalfile" ClientIDMode="Static" CssClass="form-control" Style="color: black; display: none" />
                            <asp:HiddenField runat="server" ID="hfFile" ClientIDMode="Static" />
                            <asp:HiddenField runat="server" ID="hfFileName" ClientIDMode="Static" />

                            <asp:Button runat="server" ID="SaveSignalfile" OnClick="SaveSignalfile_Click" CssClass="Buttons" Style="margin-left: 4px;display:none; " Text="Save Signal File" OnClientClick="return  AddButtonClick();" />
                           
                        </div>
                        <div style="margin-top: -30px; float: left; margin-left: 450px;">
                            <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="Buttons" OnClick="btnSave_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="signalfilename" />
        </Triggers>
    </asp:UpdatePanel>


    <%---------------Import Invoice Verification-------------------------------%>
    <div id="import2" runat="server" class="importdiv">
        <div id="main">
            <table class="filterTable tbl">
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <tr>
                    <td>
                        <asp:Label runat="server" class="control-label">Tally PO Number</asp:Label></td>
                    <td>
                        <asp:TextBox runat="server" ID="textpo1" CssClass="form-control textsize1"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label runat="server">PO Date</asp:Label></td>
                    <td>
                        <asp:TextBox runat="server" ID="textpdate1" CssClass="form-control textsize1"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server">Invoice No.</asp:Label></td>
                    <td>
                        <asp:TextBox runat="server" ID="textinvoice1" CssClass="form-control textsize1"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label runat="server">Invoice Date</asp:Label></td>
                    <td>
                        <asp:TextBox runat="server" ID="textinvdate1" CssClass="form-control textsize1"></asp:TextBox>
                    </td>
                </tr>
            </table>

            <div id="gridContainer1" class="gridContainer" runat="server" style="padding: 10px;">
                <asp:GridView runat="server" CssClass="grid" AutoGenerateColumns="false" ID="gridTally" Style="width: 95%">
                    <AlternatingRowStyle CssClass="alt" />
                    <Columns>
                        <asp:TemplateField HeaderText="Product Name">
                            <ItemTemplate>
                                <asp:Label ID="lbl1" runat="server" Text='<%# Eval("ProductionName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:Label ID="lbl2" runat="server" Text='<%#Eval("Quantity") %>' />
                            </ItemTemplate>

                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit">
                            <ItemTemplate>
                                <asp:Label ID="lbl3" runat="server" Text='<%#Eval("Unit") %>' />
                            </ItemTemplate>

                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Invoice Value">
                            <ItemTemplate>
                                <asp:Label ID="lbl4" runat="server" Text='<%#Eval("InvoiceValue") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <div style="width: 100%; margin-top: -100px;" class="text-left">
                <table style="width: 30%;">
                    <tr>
                        <td>
                            <asp:Label runat="server" CssClass="ConfirmText" Style="padding: 7px; font-size: 20px;">Verified?</asp:Label>
                        </td>
                        <td>
                            <asp:Button ID="btnSuccess" runat="server" Text="Yes" CssClass="successBtn" OnClick="btnVerify_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnCancelImport" runat="server" Text="Cancel Import" CssClass="btn btn-danger lg" OnClick="btnCancelImport_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>

    <%--******************************---Modals----*******************************--%>

    <div class="modal fade add-edit-modal" id="myConfirmationModal" role="dialog" style="min-width: 300px;">
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
                    <input type="button" value="Yes" class="btn AddEditBtn" id="saveConfirmYes" onserverclick="saveConfirmYes_ServerClick" runat="server" data-dismiss="modal" />
                    <input type="button" value="No" class="btn btn-danger CancelBtn" id="saveConfirmNo" onclick="saveConfirmNo()" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade  add-edit-modal " id="myConfirmationModal1" role="dialog" style="min-width: 300px;">
        <div class="modal-dialog " style="width: 450px">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:Label class="modal-title" runat="server">Choose File</asp:Label>
                    <a data-dismiss="modal" class="glyphicon glyphicon-remove closeButton"></a>
                </div>
                <div class="modal-body">
                    <asp:FileUpload ID="globe_Check" CssClass="form-control" runat="server" BackColor="LightGray" />
                </div>
                <div class="modal-footer">
                    <input type="button" value="Import" class="btn AddEditBtn" id="saveConfirmYes1" onserverclick="saveConfirmYes_ServerClick1" runat="server" data-dismiss="modal" />
                    <input type="button" value="Cancel" class="btn btn-danger CancelBtn" id="saveConfirmNo1" onclick="saveConfirmNo1()" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade add-edit-modal " id="myTimeValidation" role="dialog" style="min-width: 300px;">
        <div class="modal-dialog" style="width: 450px">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:Label class="modal-title" runat="server">Error?</asp:Label>
                    <a data-dismiss="modal" class="glyphicon glyphicon-remove closeButton"></a>
                </div>
                <div class="modal-body">
                    <span id="ValidationText" class="ConfirmText">Error</span>
                </div>
                <div class="modal-footer">
                    <input type="button" value="Yes" class="btn AddEditBtn" id="saveConfirmNo2" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>

    <%------------------------------------------------------------------------------%>
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            ControlPage()
        });

        $(document).ready(function () {
            ControlPage()
        });
        function ControlPage() {
            var wHeight1 = $(window).height() - 160;
            $('.gridContainer').css('height', wHeight1);

            var wHeight = $(window).height() - 260;
            $('.gridContainer1').css('height', wHeight);
        }
        $(window).resize(function () {
            var Height = $(window).height() - 160;
            $('.gridContainer').css('height', Height);
        });

        function AddButtonClick() {
            var allowedExtension = ['pdf'];
            var fileExtension = document.getElementById('selectSignalfile').value.split('.').pop().toLowerCase();
            
            var isValidFile;
            if (fileExtension != "") {
                for (var index in allowedExtension) {
                    if (fileExtension === allowedExtension[index]) {
                        isValidFile = true;
                        var fileInput = document.getElementById('selectSignalfile');
                        if (fileInput.files[0] != undefined) {
                            var reader = new FileReader();
                            reader.readAsDataURL(fileInput.files[0]);
                            reader.onload = function () {
                                $("#hfFile").val(reader.result);
                                $("#hfFileName").val($("#selectSignalfile").val().split('\\').pop());
                                __doPostBack('<%= SaveSignalfile.UniqueID%>', '');
                            }
                        }
                        else {
                            __doPostBack('<%= SaveSignalfile.UniqueID%>', '');
                        }
                    }
                }
                if (!isValidFile) {
                    WarningToastr("Please Upload file in .pdf formate!");
                    return false;
                }
            }
            return true;
            var fileInput = document.getElementById('selectSignalfile');
            if (fileInput.files[0] != undefined) {
                var reader = new FileReader();
                reader.readAsDataURL(fileInput.files[0]);
                reader.onload = function () {
                    $("#hfFile").val(reader.result);
                    $("#hfFileName").val($("#selectSignalfile").val().split('\\').pop());
                    __doPostBack('<%= SaveSignalfile.UniqueID%>', '');
                }
            } else {
                __doPostBack('<%= SaveSignalfile.UniqueID%>', '');
            };
            return false;
        }
        function signalClick() {
            document.getElementById("selectSignalfile").click();
            return false;
        }
        function UploadSignalFile(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=SaveSignalfile.ClientID %>").click();
            }
        }

        function openValidationModal(msg) {
            $('[id*=myTimeValidation]').modal('show');
            $("#ValidationText").text(msg);
        }

        function saveConfirmNo2() {
            $('[id*=myTimeValidation]').modal('hide');
        }

        function openConfirmModal(msg) {
            $('[id*=myConfirmationModal]').modal('show');
            $("#confirmationmessageText").text(msg);
            $('[id*=myConfirmationModal1]').modal('hide');
        }

        function saveConfirmNo() {
            $('[id*=myConfirmationModal]').modal('hide');

        }

        function openConfirmModal1(msg) {
            $('[id*=myConfirmationModal1]').modal('show');
            $("#confirmationmessageText1").text(msg);
        }

        function saveConfirmNo1() {
            $('[id*=myConfirmationModal1]').modal('hide');
            $('[id*=import1').show();
            $('[id*=import2').hide();
        }

       
        document.getElementById("headerName").textContent = "Import Invoice";
        var user = "<%= (String)Session["username"] %>";
        $("#lblUserID").text("Hi " + user);
    </script>

</asp:Content>
