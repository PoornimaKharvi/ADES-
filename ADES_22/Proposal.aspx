<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Proposal.aspx.cs" Inherits="ADES_22.Proposal" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .multiselect, .textsize1 {
            width: 150px;
        }

        .multiselect, .textsize2 {
            width: 330px;
        }
        .textsize3{
            width:230px
        }
        .multiselect, .textsize4 {
            width: 230px;
        }
        .add-edit-modal tr td{
            padding:7px;
        }
    </style>

    <script src="js/bootstrap-multiselect.js"></script>
    <link href="Css/bootstrap-multiselect.css" rel="stylesheet" />

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="container-fluid" style="margin-top: 0px; margin-left: 0px; width: 98%">
                <div id="main">
                    <div id="import" runat="server" class="importdiv" style="overflow: unset">
                        <div id="div1" runat="server" class="col" style="display: none;">
                        <table id="tbl1" class="filterTable tbl" style="width: 100%">
                            <asp:HiddenField ID="hdnProposal" runat="server" />
                            <tr>
                                <td>
                                    <asp:Label runat="server" Text="Customer:" />
                                </td>
                                <td>
                                    <asp:TextBox type="text" ID="txtCustName" runat="server" class="form-control textsize1" AutoCompleteType="Disabled" list="dlCustName" AutoPostBack="true" OnTextChanged="txtCustName_TextChanged" />
                                    <datalist runat="server" id="dlCustName" clientidmode="static"></datalist>
                                </td>
                                <td>
                                    <asp:Label runat="server" Text="Region:" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlRegion" runat="server" class="form-control textsize1">
                                    </asp:DropDownList>
                                </td>

                                <td>
                                    <asp:Label runat="server" Text="Owner:" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlOwner" class="form-control textsize1" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Label runat="server" Text="Status:" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlStatus" runat="server" class="form-control textsize1">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 88px">
                                    <asp:Label runat="server" Text="From Date:" />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtfromdate" class="form-control textsize1" TextMode="Date"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label runat="server" Text="To Date:" />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txttodate" class="form-control textsize1" TextMode="Date"></asp:TextBox>
                                </td>
                                <td style="width: 79px">
                                    <asp:Label runat="server" Text="Key Field:" />
                                </td>
                                <td colspan="3">
                                    <asp:ListBox ID="lbkeyfield" runat="server" CssClass="multiDropdown form-control textsize2" SelectionMode="Multiple"></asp:ListBox>
                                </td>
                                <td colspan="4">
                                    <asp:Button runat="server" ID="btnView" Text="View" CssClass="Buttons" OnClick="btnView_Click" />
                                    <asp:Button runat="server" ID="btnNewEntry" CssClass="Buttons" Text="New Entry" OnClick="btnNewEntry_Click" />
                                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="Buttons" OnClick="btnClear_Click" />
                                    <asp:Button runat="server" ID="btnExport" Text="Export" CssClass="Buttons" OnClick="btnExport_Click" />
                                </td>
                            </tr>

                        </table>
                            </div>
                    </div>
                </div>
                <asp:HiddenField ID="hdnGridregion" runat="server" />
                <div id="div2" runat="server" class="col" style="display: none;">
                <div id="gridContainer" class="gridContainer" style="height: 46vh; width: 100%" runat="server">
                    <asp:GridView ID="GridView1" CssClass="grid" runat="server"
                        AutoGenerateColumns="false" OnRowDeleting="GridView1_RowDeleting" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found" EmptyDataRowStyle-CssClass="empty-row-style">

                        <Columns>
                            <asp:TemplateField HeaderText="Customer">
                                <ItemTemplate>
                                    <asp:Label ID="txtCustomer" Text='<%# Eval("Customer") %>' AutoPostBack="true" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Region">
                                <ItemTemplate>
                                    <asp:Label ID="txtRegion" Text='<%# Eval("Region") %>' AutoPostBack="true" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Proposal Number">
                                <ItemTemplate>
                                    <asp:Label ID="txtProposalNo" Text='<%# Eval("ProposalNumber") %>' AutoPostBack="true" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Version">
                                <ItemTemplate>
                                    <asp:Label ID="txtVersion" Text='<%# Eval("ProposalVersion") %>' AutoPostBack="true" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Proposal Date">
                                <ItemTemplate>
                                    <asp:Label ID="txtProposalDate" Text='<%# Eval("ProposalDate") %>' AutoPostBack="true" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Owner">
                                <ItemTemplate>
                                    <asp:Label ID="txtOwner" Text='<%# Eval("ProposalOwner") %>' AutoPostBack="true" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Proposal Value" HeaderStyle-Width="80px">
                                <ItemTemplate>
                                    <asp:Label ID="txtProposalValue" Text='<%# Eval("ProposalValue") %>' AutoPostBack="true" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Submitted Date">
                                <ItemTemplate>
                                    <asp:Label ID="txtSubmitDate" Text='<%# Eval("SubmittedDate") %>' AutoPostBack="true" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="txtStatus" Text='<%# Eval("Status") %>' AutoPostBack="true" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Status As On" HeaderStyle-Width="81px">
                                <ItemTemplate>
                                    <asp:Label ID="txtStatusAsOn" Text='<%# Eval("StatusAsOn") %>' AutoPostBack="true" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Key Field">
                                <ItemTemplate>
                                    <asp:Label ID="txtKeyField" Text='<%# Eval("KeyField") %>' AutoPostBack="true" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Attachment">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnattachedfile" Value='<%#Eval("attachmentfile") %>' runat="server" />
                                    <asp:LinkButton ID="fileUpload" Text='<%# Eval("FileUpload") %>' runat="server"  OnClientClick="return FileDownloadClick1(this);" />
                                    <%--OnClientClick="return FileDownloadClick1(this);" --%>
                                    <asp:HiddenField runat="server" ID="hdnattachedfileInBase64" Value='<%#Eval("AttachmentFileBase64") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="lbEdit" CssClass="glyphicon glyphicon-pencil  EditlinkBtn" ToolTip="Edit" OnClick="lbEdit_Click"></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="lbDelete" CssClass="glyphicon glyphicon-trash  DeletelinkBtn" ToolTip="Delete" CommandName="Delete"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                </div>
                <div class="modal add-edit-modal" id="newproposal" tabindex="-1">
                    <div class="modal-dialog" style="width: 1110px;">
                        <div class="modal-content">
                            <div class="modal-header">

                                <asp:Label class="modal-title" ID="Proposalentrytitle" runat="server">Proposal Entry</asp:Label>
                                <a data-dismiss="modal" class="glyphicon glyphicon-remove closeButton"></a>

                                <asp:HiddenField runat="server" ID="hfNewOrEdit" />
                            </div>
                            <div class="modal-body" style="overflow: unset;padding:9px;height:32vh;margin-top:12px">
                                <table class="modal-table" >
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" class="col-form-label" Text="Customer *"></asp:Label></td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtAddCustomer" CssClass="form-control textsize3" AutoCompleteType="Disabled" list="dlCustName1" AutoPostBack="true" OnTextChanged="txtAddCustomer_TextChanged1"></asp:TextBox>
                                            <datalist id="dlCustName1" runat="server" clientidmode="static"></datalist>
                                        </td>

                                        <td>
                                            <asp:Label runat="server" class="col-form-label" Text="Region"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="ddlAddregion1" runat="server" class="form-control textsize3">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" class="col-form-label" Text="Proposal Date"></asp:Label></td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtAddpropdate" CssClass="form-control textsize3" TextMode="Date"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" class="col-form-label" Text="Submitted Date"></asp:Label></td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtAddsubmitdate" CssClass="form-control textsize3" TextMode="Date"></asp:TextBox>
                                        </td>

                                        <td>
                                            <asp:Label runat="server" class="col-form-label" Text="Proposal No *"></asp:Label></td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtAddpropno" CssClass="form-control textsize3" AutoCompleteType="Disabled"></asp:TextBox>

                                        </td>
                                        <td>
                                            <asp:Label runat="server" class="col-form-label" Text="Version *"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtAddversion" CssClass="form-control textsize3"></asp:TextBox>
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" class="col-form-label" Text="Owner"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlAddowner1" class=" form-control textsize3" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        
                                        <td>
                                            <asp:Label runat="server" class="col-form-label" Text="Proposal Value *"></asp:Label></td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtAddpropval" CssClass="form-control textsize3" AutoCompleteType="Disabled">
                                            </asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" class="col-form-label" Text="Status"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="ddlAddstatus1" runat="server" class="form-control textsize3">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" class="col-form-label" Text="Status As On"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtAddstatusason" CssClass="form-control textsize3" TextMode="Date"></asp:TextBox></td>


                                        
                                        <td>
                                            <asp:Label runat="server" class="col-form-label" Text="Key Field"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:ListBox ID="lbAddkeyfield" CssClass="multiDropdown form-control textsize4" SelectionMode="Multiple" runat="server" ></asp:ListBox>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" class="col-form-label" Text="Attachment"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:FileUpload runat="server" ID="fileupload" ClientIDMode="Static" class="form-control textsize3" />
                                            <asp:HiddenField runat="server" ID="hfFile" ClientIDMode="Static" />
                                            <asp:HiddenField runat="server" ID="hfFileName" ClientIDMode="Static" />

                                            <asp:Label runat="server" ID="addFileName"></asp:Label>
                                        </td>

                                    </tr>
                                </table>
                            </div>
                            <div class="modal-footer">
                                <asp:Button runat="server" ID="btnSave" CssClass="btn AddEditBtn" Text="Save" OnClientClick="return getNewEntity();" OnClick="btnSave_Click" />
                                <asp:Button runat="server" Text="Cancel" ID="btnCancel" CssClass="btn  btn-danger CancelBtn" OnClientClick="return clearscreen();"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>

                <asp:HiddenField runat="server" ID="hfRowIndex" ClientIDMode="Static" />
                <asp:Button runat="server" ID="btnFileDownLoad" OnClick="btnFileDownLoad_Click" Visible="false" />
                </div>
        </ContentTemplate>
        <Triggers>
            <%--<asp:PostBackTrigger ControlID="GridView1" />--%>
            <%-- <asp:AsyncPostBackTrigger ControlID="btnView" EventName="Click" />--%>
            <%--<asp:AsyncPostBackTrigger ControlID="btnNewEntry" EventName="Click" />--%>
            <%--  <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />--%>
            <%--<asp:PostBackTrigger ControlID="btnExport" />--%>

            <%-- <asp:PostBackTrigger ControlID="btnNewEntry" />
            <asp:PostBackTrigger ControlID="btnClear" />
            <asp:PostBackTrigger ControlID="btnView" />--%>
            <asp:PostBackTrigger ControlID="btnExport" />
            <asp:PostBackTrigger ControlID="btnFileDownLoad" />

        </Triggers>
    </asp:UpdatePanel>
    

    <div class="modal add-edit-modal" id="myConfirmationModal" role="dialog" style="min-width: 300px;">
        <div class="modal-dialog modal-dialog-centered" style="width: 450px">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:Label class="modal-title" runat="server">Confirmation</asp:Label>
                    <a data-dismiss="modal" class="glyphicon glyphicon-remove closeButton"></a>
                </div>
                <div class="modal-body">
                    <span id="confirmationmessageText" class="ConfirmText">Confirmation</span>
                </div>
                <div class="modal-footer">
                    <input type="button" value="Yes" class="btn AddEditBtn" id="saveConfirmYes" onserverclick="saveConfirmYes_ServerClick" runat="server" data-dismiss="modal" />
                    <input type="button" value="No" class="btn btn-danger CancelBtn" id="saveConfirmNo" onclick="saveConfirmNo()" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>

    <div class="modal add-edit-modal" id="myTimeValidation" role="dialog" style="min-width: 300px;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" style="color: white;">Error?</h4>
                </div>
                <div class="modal-body">
                    <span id="ValidationText" style="font-size: 17px;">Error</span>
                </div>
                <div class="modal-footer">
                    <input type="button" value="OK" class="btn btn-info" id="saveConfirmNo1" style="background-color: #093d81; color: white" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>


    <script type="text/javascript">  

        $(document).ready(function () {
            
            ConntrolSetter();
        });
        function FileDownloadClick1(element) {
            debugger;
            let rowIndex =($(element).closest("tr").prevAll().length) - 1;
            $("#hfRowIndex").val(rowIndex);
            alert(rowIndex);
            __doPostBack('<%= btnFileDownLoad.UniqueID%>', '');
            return false;
        };

        function ConntrolSetter() {
            $('.col').show(); 
            $('.multiDropdown').multiselect({
                includeSelectAllOption: true
                
            });
            var wHeight = $(window).height() - 200;
            $('.gridContainer').css('height', wHeight);
        }
        function openConfirmModal(msg) {
            debugger;
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
       
        function getNewEntity() {

            if ($("[id$=txtAddCustomer]").val() == "") {
                WarningToastr("Please Select Customer Name");
                $("[id$=tctAddCustomer]").focus();
                return false;
            }
            if ($("[id$=txtAddpropno]").val() == "") {
                WarningToastr("Please Enter Proposal Number");
                $("[id$=txtAddpropno]").focus();
                return false;
            }
            if ($("[id$=txtAddpropval]").val() == "") {
                WarningToastr("Please Enter Proposal Value");
                $("[id$=txtAddpropval]").focus();
                return false;
            }
            if ($("[id$=txtAddversion]").val() == "") {
                WarningToastr("Please Enter Version");
                $("[id$=txtAddversion]").focus();
                return false; txtAddversion
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

        $(window).resize(function () {
            var Height = $(window).height() - 290;
            $('.gridContainer').css('height', Height);
        });
        document.getElementById("headerName").textContent = "Proposal Entry";
        var user = "<%= (String)Session["username"] %>";
        $("#lblUserID").text("Hi " + user);

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            //ScriptManager.GetCurrent(this).RegisterPostBackControl(FileUpload);
            ConntrolSetter();

            function FileDownloadClick1(element) {
                debugger;
                let rowIndex = ($(element).closest("tr").prevAll().length) - 1;
                $("#hfRowIndex").val(rowIndex);
                __doPostBack('<%= btnFileDownLoad.UniqueID%>', '');
                return false;
            };

        });
    </script>

</asp:Content>


