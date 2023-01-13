<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DispatchProduct.aspx.cs" Inherits="ADES_22.DispatchProduct" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div id="main">
                    <div id="import" runat="server" class="importdiv">
                        <table id="tbl1" class="filterTable tbl">
                            <asp:HiddenField ID="hdnInvoice" runat="server" />
                            <tr>
                                <td>
                                    <asp:Label runat="server">Tally PO Number</asp:Label></td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtPONumber" class="form-control textsize1" AutoCompleteType="Disabled" list="dlPONumber" AutoPostBack="true" OnTextChanged="txtPONumber_TextChanged"></asp:TextBox>
                                    <datalist runat="server" id="dlPONumber" clientidmode="static"></datalist>
                                </td>
                                <td>
                                    <asp:Label runat="server">Invoice No</asp:Label></td>
                                <td>
                                    <asp:DropDownList ID="ddlInvoiceNumber" runat="server" class="form-control textsize1" AutoPostBack="true"></asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Button runat="server" ID="btnView" CssClass="Buttons" Text="View" OnClick="btnView_Click" />
                                </td>
                                <td>
                                    <asp:Button runat="server" CssClass="Buttons" ID="btnClear" Text="Clear" OnClick="btnClear_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server">From Date</asp:Label></td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtFDate" CssClass="form-control textsize1" TextMode="Date" AutoPostBack="true"></asp:TextBox></td>
                                <td>
                                    <asp:Label runat="server">To Date</asp:Label></td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtEDate" CssClass="form-control textsize1" TextMode="Date" AutoPostBack="true"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button runat="server" CssClass="Buttons" ID="btnNewEntry" Text="New Entry" OnClick="NewEntry_Click" />
                                </td>
                                <%--<td>
                                    <asp:Button runat="server" CssClass="Buttons" ID="btnExport" Text="Export" OnClick="btnExport_Click"/>
                                </td>--%>
                            </tr>
                            <tr>
                            </tr>
                        </table>
                        <asp:HiddenField ID="hdnGridregion" runat="server" />
                        <div id="gridContainer" class="gridContainer" style="width: 98%; height: 76vh; margin: 0px; margin-top: 0px" runat="server">
                            <asp:GridView ID="GridView1" CssClass="grid" runat="server" OnRowDeleting="GridView1_RowDeleting" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found !!" EmptyDataRowStyle-CssClass="empty-row-style">
                                <Columns>
                                    <asp:TemplateField HeaderText="PO Number">
                                        <ItemTemplate>
                                            <asp:Label ID="txtPoNo" Text='<%# Eval("PoNumber") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Invoice No">
                                        <ItemTemplate>
                                            <asp:Label ID="txtInvoiceNo" Text='<%# Eval("InvoiceNo") %>' runat="server">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Invoice Date">
                                        <ItemTemplate>
                                            <asp:Label ID="txtInvoiceDate" Text='<%# Eval("InvoiceDate") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Invoice Value">
                                        <ItemTemplate>
                                            <asp:Label ID="txtInvoiceValue" Text='<%# Eval("InvoiceValue") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Customer">
                                        <ItemTemplate>
                                            <asp:Label ID="txtCustomer" Text='<%# Eval("Customer") %>' Width="100px" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Region">
                                        <ItemTemplate>
                                            <asp:Label ID="txtRegion" Text='<%# Eval("Region") %>' runat="server">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Product Name">
                                        <ItemTemplate>
                                            <asp:Label ID="txtProdName" Text='<%# Eval("ProdName") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Consignment No.">
                                        <ItemTemplate>
                                            <asp:Label ID="txtConsignName" Text='<%# Eval("ConsignName") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Courier Name">
                                        <ItemTemplate>
                                            <asp:Label ID="txtCourierName" Text='<%# Eval("CourierName") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Content">
                                        <ItemTemplate>
                                            <asp:Label ID="txtContent" Text='<%# Eval("Content") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Email">
                                        <ItemTemplate>
                                            <asp:Label ID="txtEmail" Text='<%# Eval("Email") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="txtStatus" Text='<%# Eval("Status") %>' runat="server">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Delivery Date">
                                        <ItemTemplate>
                                            <asp:Label ID="txtDelivDate" Text='<%# Eval("DeliveryDate") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Received By">
                                        <ItemTemplate>
                                            <asp:Label ID="txtRecvBy" Text='<%# Eval("RecievedBy") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Attachment" HeaderStyle-Width="200px">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnAttachedFile" Value='<%# Eval("attachmentfile") %>' runat="server" />
                                            <asp:LinkButton ID="FileUpload" Text='<%# Eval("FileUpload") %>' runat="server" OnClientClick="if(!getImage(this)){return false};" />
                                            <%-- OnClick="FileUpload_Click"--%>
                                            <asp:HiddenField runat="server" ID="hfAttachedFileInBase64" Value='<%# Eval("AttachmentFileBase64") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <div style="text-align: center">
                                                <asp:LinkButton runat="server" CssClass="glyphicon glyphicon-pencil EditlinkBtn " ToolTip="Edit" OnClick="EditNew_Click"></asp:LinkButton>
                                                <asp:LinkButton runat="server" CssClass="glyphicon glyphicon-trash DeletelinkBtn" ToolTip="Delete" CommandName="Delete"></asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <%--------------------------------------Modal Popup for New Entry----------------------------------------- --%>
                        <div class="modal fade add-edit-modal" id="newEntry1" tabindex="-1">
                            <div class="modal-dialog" style="width: 1000px;">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <asp:Label class="modal-title" ID="DispatchTitle" runat="server">Add Dispatch Details</asp:Label>
                                        <a data-dismiss="modal" class="glyphicon glyphicon-remove closeButton"></a>
                                        <asp:HiddenField runat="server" ID="Addhff" />
                                    </div>
                                    <div class="modal-body">
                                        <table class="modal-table">
                                            <tr>
                                                <td>
                                                    <asp:Label runat="server">PO Number</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="addPoNo" CssClass="form-control textsize" list="dlPolists" autocomplete="off" ReadOnly="false" AutoPostBack="true" OnTextChanged="addPoNo_TextChanged"></asp:TextBox>
                                                    <datalist id="dlPolists" runat="server" clientidmode="static"></datalist>
                                                    <asp:HiddenField ID="hdnPoDate" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:Label runat="server">Invoice Number</asp:Label></td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="addInvNo" CssClass="form-control  textsize" AutoPostBack="true" OnSelectedIndexChanged="addInvNo_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server">Invoice Date</asp:Label></td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="addInvDate" CssClass="form-control textsize" ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label runat="server">Invoice Value</asp:Label></td>
                                                <td>
                                                    <asp:Label runat="server" ID="addInvValue" CssClass="form-control textsize" ReadOnly="true">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server">Customer</asp:Label></td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="addCustomer" CssClass="form-control textsize" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server">Product Name</asp:Label></td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="addProdName" CssClass="form-control textsize" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label runat="server">Consign Name</asp:Label></td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="addConsignName" CssClass="form-control textsize" ReadOnly="true"></asp:TextBox>
                                                </td>

                                                <td>
                                                    <asp:Label runat="server">Region</asp:Label></td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="addRegion" CssClass="form-control textsize" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server">Courier Name</asp:Label></td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="addCourierName" CssClass="form-control textsize">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label runat="server">Status</asp:Label></td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="addStatus" CssClass="form-control textsize">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server">Content</asp:Label></td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="addContent" CssClass="form-control textsize" AutoCompleteType="Disabled"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server">Email</asp:Label></td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="addEmail" CssClass="form-control textsize" AutoCompleteType="Disabled"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label runat="server">Delivery Date</asp:Label></td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="addDeliveryDate" CssClass="form-control textsize" TextMode="Date"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server">Received By</asp:Label></td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="addReceivedBy" CssClass="form-control textsize" AutoCompleteType="Disabled"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server">Attachment</asp:Label></td>
                                                <td colspan="2">
                                                    <asp:FileUpload runat="server" ID="addFileUpload" ClientIDMode="Static" class="form-control textsize" />
                                                    <asp:Label runat="server" ID="addFileName">filename</asp:Label>
                                                    <asp:HiddenField runat="server" ID="hfFile" ClientIDMode="Static" />
                                                    <asp:HiddenField runat="server" ID="hfFileName" ClientIDMode="Static" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button runat="server" CssClass="btn AddEditBtn" ID="AddButton" Text="Save" OnClientClick="return ValidateAddButtonClick();" OnClick="Saveclick_Click" /><%--"return AddButtonClick()"--%>
                                        <asp:Button runat="server" CssClass="btn btn-danger CancelBtn" ID="CancelButton" Text="Cancel" OnClientClick="return clearscreen();" data-dismiss="modal" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%-------------------------------------Modal Popup for Delete row Conformation------------------------ --%>
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

                    <%-------------------------------------------------------Modal popup for Attached Image-----------------------------%>
                    <div class="modal fade  add-edit-modal" id="showLargeImage" role="dialog" style="min-width: 400px;">
                        <div class="modal-dialog " style="width: 400px">
                            <div class="modal-content">
                                <div class="modal-header">

                                    <asp:Label class="modal-title" runat="server">Attached Image</asp:Label>
                                    <a data-dismiss="modal" class="glyphicon glyphicon-remove closeButton"></a>
                                </div>
                                <div class="modal-body">
                                    <asp:Image ID="largeImage" runat="server" CssClass="modal-image" />
                                </div>
                                <div class="modal-footer">
                                </div>
                            </div>
                        </div>
                    </div>
                    <%-------------------------------------------------------------------------------------------------%>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="GridView1" />
            <asp:AsyncPostBackTrigger ControlID="btnNewEntry" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnView" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript">  
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {

            //ControlDispatchPage()
        });
        $(document).ready(function () {
            // ControlDispatchPage()
        });
        function ControlDispatchPage() {
            var wHeight = $(window).height() - 500;
            $('.gridContainer').css('height', wHeight);
        }

        function getImage(img) {
            var str = ($(img)[0].text).toLowerCase();
            if (str.endsWith(".pdf")) {
                return true;
            }
            else {
                $('[id*=largeImage]').attr('src', '');
                $('[id*=showLargeImage]').modal('show');
                $('[id*=largeImage]').attr('src', ($(img)[0].parentNode.firstElementChild.value));
                return false;
            }
        };

        function ValidateAddButtonClick() {
            if ($("[id$=addPoNo]").val() == "") {
                WarningToastr("Please select PO Number!");
                $("[id$=addPoNo]").focus();
                return false;
            }
            var EmailText = $("[id$=addEmail]").val();
            if (EmailText != "") {
                if (validateEmail(EmailText) == false) {
                    WarningToastr('Please enter valid Email-ID!');
                    $("[id$=addaEmail]").focus();
                    return false;
                }
            }
            var allowedExtension = ['jpeg', 'jpg', 'png'];
            var fileExtension = document.getElementById('addFileUpload').value.split('.').pop().toLowerCase();
            var isValidFile;
            if (fileExtension != "") {
                for (var index in allowedExtension) {
                    if (fileExtension === allowedExtension[index]) {
                        isValidFile = true;
                        var fileInput = document.getElementById('addFileUpload');
                        if (fileInput.files[0] != undefined) {
                            var reader = new FileReader();
                            reader.readAsDataURL(fileInput.files[0]);
                            reader.onload = function () {
                                $("#hfFile").val(reader.result);
                                $("#hfFileName").val($("#addFileUpload").val().split('\\').pop());
                                __doPostBack('<%= AddButton.UniqueID%>', '');
                            }
                        }
                        else {
                            __doPostBack('<%= AddButton.UniqueID%>', '');
                        }
                    }
                }
                if (!isValidFile) {
                    WarningToastr("Please Upload file in .jpg or .png formate!");
                    $("[id$=addFileUpload]").focus();
                    return false;
                }
            }
            return true;
        }

















        function validateFile() {
            var allowedExtension = ['jpeg', 'jpg', 'png'];
            var fileExtension = document.getElementById('addFileUpload').value.split('.').pop().toLowerCase();
            var isValidFile = false;

            for (var index in allowedExtension) {
                if (fileExtension === allowedExtension[index]) {
                    isValidFile = true;
                    break;
                }
            }
            if (!isValidFile) {
                WarningToastr("Please Upload file in .jpeg or jpg formate!");
                return false;
            }
        }


        function validateEmail(sEmail) {
            var filter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
            if (filter.test(sEmail)) {
                return true;
            }
            else {
                return false;
            }
        };

        function openConfirmModal(msg) {
            $('[id*=myConfirmationModal]').modal('show');
            $("#confirmationmessageText").text(msg);
        }
        function saveConfirmNo() {
            $('[id*=myConfirmationModal]').modal('hide');
        }

        function opValidationModal(msg) {
            $('[id*=myTimeValidation]').modal('show');
            $("#ValidationText").text(msg);
        }

        function saveConfirmNo1() {
            $('[id*=myTimeValidation]').modal('hide');
        }
        document.getElementById("headerName").textContent = "Dispatch Details";
    </script>
</asp:Content>

