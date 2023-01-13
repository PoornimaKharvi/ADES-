<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="PurchaseOrder.aspx.cs" Inherits="ADES_22.PurchaseOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .textsize1 {
            width: 200px;
        }
    </style>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="container-fluid" style="margin-top: 0px; margin-left: 0px; width: 100%">
                <div id="main">
                    <div id="import" runat="server" class="importdiv">
                        <div id="div1" runat="server" class="col" style="display: none;">
                        <table id="tbl1" class="filterTable tbl" style="width: 100%">
                            <asp:HiddenField ID="hdnPurchase" runat="server" />
                            <tr>
                                <td>
                                    <asp:Label runat="server" Text="Customer:" />
                                </td>
                                <td>
                                    <asp:TextBox type="text" ID="txtCustName" runat="server" class="form-control textsize1" AutoCompleteType="Disabled" AutoPostBack="true"  list="dlCustName" OnTextChanged="txtCustName_TextChanged" />
                                    <datalist id="dlCustName" runat="server" clientidmode="static">
                                    </datalist>
                                </td>
                                <td>
                                    <asp:Label runat="server" Text="Region:" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlRegion" runat="server" class="form-control textsize1">
                                    </asp:DropDownList>
                                </td>


                                <td>
                                    <asp:Label runat="server" Text="Status:" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlStatus" runat="server" class="form-control textsize1">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>

                                    <asp:Label runat="server" Text="From Date:" />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtfromdate" class="form-control textsize1" TextMode="Date"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label runat="server" Text="To Date:" />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txttodate" class="form-control textsize1" TextMode="Date"></asp:TextBox>
                                </td>
                                <td colspan="4">
                                    <asp:Button ID="btnView" runat="server" Text="View" CssClass="Buttons" OnClick="btnView_Click" />
                                    <asp:Button runat="server" CssClass="Buttons" ID="BtnNewEntry" Text="New Entry" OnClick="BtnNewEntry_Click" />
                                    <asp:Button ID="btClear" runat="server" Text="Clear" CssClass="Buttons" OnClick="btnClear_Click" />
                                    <asp:Button runat="server" ID="btnExport" CssClass="Buttons" Text="Export" OnClick="btnExport_Click" />
                                </td>
                            </tr>
                        </table>
                            </div>
                    </div>
                </div>
                <%---gridview design----%>
                <asp:HiddenField ID="hdnGridregion" runat="server" />
                <div id="div2" runat="server" class="col" style="display: none;">
                <div id="gridContainer" class="gridContainer" runat="server" style="width: 98%;">
                    <asp:GridView ID="GridView1" CssClass="grid" runat="server"
                        AutoGenerateColumns="false"
                        OnRowDeleting="GridView1_RowDeleting" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found" EmptyDataRowStyle-CssClass="empty-row-style" Style="width: 99%">
                        <Columns>
                            <asp:TemplateField HeaderText="Customer">
                                <ItemTemplate>
                                    <asp:Label ID="txtCustomer" Text='<%# Eval("Customer") %>' AutoPostBack="true" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Region">
                                <ItemTemplate>
                                    <asp:Label ID="txtRegion" Text='<%# Eval("Region") %>' AutoPostBack="true" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="PO Date" HeaderStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:Label ID="txtPODate" Text='<%# Eval("POdate") %>' AutoPostBack="true" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="PO Number">
                                <ItemTemplate>
                                    <asp:Label ID="txtPONo" Text='<%# Eval("POnumber") %>' AutoPostBack="true" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="PO Value">
                                <ItemTemplate>
                                    <asp:Label ID="txtPOValue" Text='<%# Eval("POvalue") %>' AutoPostBack="true" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Quote Reference">
                                <ItemTemplate>
                                    <asp:Label ID="txtQuoteRef" Text='<%# Eval("QuoteRef") %>' AutoPostBack="true" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="txtStatus" Text='<%# Eval("Status") %>' AutoPostBack="true" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Status As On">
                                <ItemTemplate>
                                    <asp:Label ID="txtStatusAsOn" Text='<%# Eval("StatusAsOn") %>' AutoPostBack="true" runat="server" TextMode="Date" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Tally POdate">
                                <ItemTemplate>
                                    <asp:Label ID="txtTallyPOdate" Text='<%# Eval("tallyPOdate") %>' runat="server" TextMode="Date" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Tally PO Number">
                                <ItemTemplate>
                                    <asp:Label ID="txtTallyPO" Text='<%# Eval("tallyPOnumber") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Attachment">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnAttachedFile" Value='<%# Eval("attachmentfile") %>' runat="server" />
                                    <asp:LinkButton ID="fileUpload" Text='<%# Eval("Attachment") %>' runat="server" OnClientClick="return FileDownloadClick1(this);" />  <%--  OnClick="fileUpload_Click1"--%>
                                    <asp:HiddenField runat="server" ID="hdnattachedfileInBase64" Value='<%#Eval("AttachmentFileBase64") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="lbEdit" CssClass="glyphicon glyphicon-pencil  EditlinkBtn" ToolTip="Edit" OnClick="lbEdit_Click"></></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="lbDelete" CssClass="glyphicon glyphicon-trash  DeletelinkBtn" CommandName="Delete" ToolTip="Delete"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                </div>
                <%---------Popup for adding new entry----------%>

                <div class="modal fade add-edit-modal" id="neworder" tabindex="-1">
                    <div class="modal-dialog" style="width: 700px">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Label class="modal-title" ID="purchaseordertitle" runat="server">Add Purchase Order</asp:Label>
                                <a data-dismiss="modal" class="glyphicon glyphicon-remove closeButton"></a>
                                <asp:HiddenField runat="server" ID="hfNewOrEdit" />
                            </div>
                            <div class="modal-body">
                                <table class="modal-table">
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" class="col-form-label" Text="Customer"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtAddCustName" CssClass="form-control textsize" list="dladdCustName" AutoCompleteType="Disabled" AutoPostBack="true" OnTextChanged="txtAddCustName_TextChanged"></asp:TextBox>
                                            <datalist id="dladdCustName" runat="server" clientidmode="static">
                                            </datalist>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" class="col-form-label" Text="Region"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddladdregion" runat="server" CssClass="form-control textsize ">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" class="col-form-label" Text="PO NO."></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtAddpopno" CssClass="form-control textsize" AutoCompleteType="Disabled" ClientIDMode="Static"></asp:TextBox>
                                        </td>


                                        <td>
                                            <asp:Label runat="server" class="col-form-label" Text="PO Date "></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtAddpopdate" CssClass="form-control textsize" TextMode="Date"></asp:TextBox>
                                        </td>
                                    </tr>


                                    <tr>
                                        <td>
                                            <asp:Label runat="server" class="col-form-label" Text="Status "></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlAddstatus" runat="server" CssClass="form-control textsize">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" class="col-form-label" Text="PO Value"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtAddpopval" CssClass="form-control textsize" AutoCompleteType="Disabled"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:Label runat="server" class="col-form-label" Text="Quote Reference"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtaddqutref" CssClass="form-control textsize" list="dladdquotRef"></asp:TextBox>
                                            <datalist id="dladdquotRef" runat="server" clientidmode="static">
                                            </datalist>
                                        </td>


                                        <td>
                                            <asp:Label runat="server" class="col-form-label" Text="Status As On"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtAddstatusason" CssClass="form-control textsize" TextMode="Date"></asp:TextBox>
                                        </td>
                                    </tr>


                                    <tr>
                                        <td>

                                            <asp:Label runat="server" class="col-form-label" Text="Tally PO NO"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddladdtallypono" runat="server" AutoPostBack="true" CssClass="form-control textsize" OnSelectedIndexChanged="ddladdtallypono_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>

                                        <td>
                                            <asp:Label runat="server" class="col-form-label" Text="Tally PO Date"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtAddtallypodate" CssClass="form-control textsize" AutoCompleteType="Disabled"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" class="col-form-label" Text="Attachment"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:FileUpload runat="server" ID="fileupload" ClientIDMode="Static" class="form-control textsize" />
                                            <asp:Label runat="server" ID="addFileName" class="col-form-label" Text="filename"></asp:Label>
                                            <asp:HiddenField runat="server" ID="hfFile" ClientIDMode="Static" />
                                            <asp:HiddenField runat="server" ID="hfFileName" ClientIDMode="Static" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="modal-footer">
                                <asp:Button runat="server" CssClass="btn AddEditBtn" ID="btnSave" Text="Save" OnClientClick="return getNewEntity();" OnClick="btnSave_Click" />
                                <asp:Button runat="server" CssClass="btn btn-danger CancelBtn" ID="btnCancel" Text="Cancel" OnClientClick="return clearscreen()"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:HiddenField runat="server" ID="hfRowIndex" ClientIDMode="Static" />
                <asp:Button runat="server" ID="btnFileDownLoad" OnClick="btnFileDownLoad_Click" Visible="false" />
            </div>
        </ContentTemplate>
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="btnView" EventName="Click" />
                <asp:PostBackTrigger ControlID="BtnNewEntry" />
            <asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="Click" />--%>
            <asp:PostBackTrigger ControlID="btnExport" />
            <asp:PostBackTrigger ControlID="btnFileDownLoad" />
        </Triggers>
    </asp:UpdatePanel>



    <div class="modal fade add-edit-modal" id="myConfirmationModal" role="dialog" style="min-width: 300px;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:Label class="modal-title" runat="server">Confirmation</asp:Label>
                </div>
                <div class="modal-body">
                    <%--<img src="Images/confirm.png" width="40" />&nbsp;&nbsp;&nbsp;--%>
                    <span id="confirmationmessageText" style="font-size: 17px;">Confirmation</span>
                </div>
                <div class="modal-footer">
                    <input type="button" value="Yes" class="btn AddEditBtn" id="saveConfirmYes" onserverclick="saveConfirmYes_ServerClick" runat="server" data-dismiss="modal" />
                    <input type="button" value="No" class="btn btn-danger CancelBtn" id="saveConfirmNo" onclick="saveConfirmNo()" data-dismiss="modal" />
                </div>
            </div>

        </div>
    </div>

    <div class="modal fade add-edit-modal" id="myTimeValidation" role="dialog" style="min-width: 300px;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:Label class="modal-title" runat="server">Error?</asp:Label>
                </div>
                <div class="modal-body">
                    <span id="ValidationText" style="font-size: 17px;">Error</span>
                </div>
                <div class="modal-footer">
                    <input type="button" value="OK" class="btn AddEditBtn" id="saveConfirmNo1" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="showLargeImage" role="dialog" style="min-width: 500px;">
        <div class="modal-dialog" style="width: 80vw; height: 90vh; padding: 3px">
            <div class="modal-content" style="border: 2px solid #5D7B9D; width: 100%; height: 100%;">
                <div class="modal-header" style="position: relative; padding: 0px; border-bottom: none">
                    <a data-dismiss="modal" class="glyphicon glyphicon-remove" style="float: right; z-index: 5; color: red; font-size: 30px"></a>
                </div>
                <div class="modal-body" style="text-align: center; padding: 0px; width: 100%; height: 100%">
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">  
        $(document).ready(function () {

            ConntrolSetter();
        });
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            ConntrolSetter();
            var wHeight = $(window).height() - 190;
            $('.gridContainer').css('height', wHeight);

        function FileDownloadClick1(element) {
                let rowIndex = ($(element).closest("tr").prevAll().length) - 1;
                $("#hfRowIndex").val(rowIndex);
                __doPostBack('<%= btnFileDownLoad.UniqueID%>', '');
                return false;
            };
        });
        function ConntrolSetter() {
            $('.col').show();
        }
        $(document).ready(function () {
            var wHeight = $(window).height() - 190;
            $('.gridContainer').css('height', wHeight);
        });

        function FileDownloadClick1(element) {
            let rowIndex = ($(element).closest("tr").prevAll().length) - 1;
            $("#hfRowIndex").val(rowIndex);
            __doPostBack('<%= btnFileDownLoad.UniqueID%>', '');
            return false;
        };

        function getNewEntity() {

            if ($("[id$=txtAddCustName]").val() == "") {
                WarningToastr("Please Select Customer Name");
                $("[id$=txtAddCustName]").focus();
                return false;
            }
            
            if ($("[id$=txtAddpopno]").val() == "") {
                WarningToastr("Please Enter PO Number");
                $("[id$=txtAddpopno]").focus();
                return false;
            }
            
            if ($("[id$=txtAddpopdate]").val() == "") {
                WarningToastr("Please Enter PO Date");
                $("[id$=txtAddpopdate]").focus();
                return false;
            } 
            if ($("[id$=txtAddpopval]").val() == "") {
                WarningToastr("Please Enter  PO Value");
                $("[id$=txtAddpopval]").focus();
                return false; 
            }

            var fileInput = document.getElementById('fileupload');
            if (fileInput.files[0] != undefined) {
                var reader = new FileReader();
                reader.readAsDataURL(fileInput.files[0]);
                reader.onload = function () {
                    $("#hfFile").val(reader.result);
                    $("#hfFileName").val($("#fileupload").val().split('\\').pop());
                    __doPostBack('<%=btnSave.UniqueID%>', '');
                }

            }
            else {
                __doPostBack('<%=btnSave.UniqueID%>', '');
            }

            return false;
        }

        function openConfirmModal(msg) {
            $('[id*=myConfirmationModal]').modal('show');
            $("#confirmationmessageText").text(msg);
        }

        function saveConfirmNo() {
            $('[id*=myConfirmationModal]').modal('hide');
        }

        function openValidationModal(msg) {
            $('[id*=myTimeValidation]').modal('show');
            $("#ValidationText").text(msg);
        }

        function saveConfirmNo1() {
            $('[id*=myTimeValidation]').modal('hide');
        }
        
        $(window).resize(function () {
            var Height = $(window).height() - 300;
            $('.gridContainer').css('height', Height);
        });


        document.getElementById("headerName").textContent = "Purchase Order";
        var user = "<%= (String)Session["username"] %>";
        $("#lblUserID").text("Hi " + user);
    </script>



</asp:Content>
