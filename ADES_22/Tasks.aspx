<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tasks.aspx.cs" Inherits="ADES_22.Tasks" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>--%>
    <script src="Scripts/DateTimePicker331/moment.js"></script>
    <script src="Scripts/DateTimePicker331/bootstrap-datetimepicker.min.js"></script>
    <link href="Scripts/DateTimePicker331/bootstrap-datetimepicker.min.css" rel="stylesheet" />
   
    <script src="Scripts/DateTimePicker/bootstrap-datepicker.js"></script>
    <link href="Scripts/DateTimePicker/bootstrap-datepicker3.css" rel="stylesheet" />
 
    <script src="Scripts/DateTimePicker/bootstrap-datepicker.en-IE.min.js"></script>
    <script src="Scripts/DateTimePicker/bootstrap-datepicker.zh-CN.min.js"></script>

 <style>
        .textsize1 {
            width: 190px;
        }

        .textsize2 {
            width: 110px;
        }

        .textsize3 {
            width: 200px;
        }

        .colorset {
            font-weight: 500;
            color: darkslateblue;
        }

        .grid .Subtasklinkbtn {
            color: orange;
        }

        .add-edit-modal .modal-table {
            width: 100%;
        }

        .add-edit-modal .modal-table1 {
            width: 91%;
        }
        .add-edit-modal .modal-table1 tr td{
            padding:0px;
        }
    </style>
   <div class="container-fluid" style="margin-top: 0px; margin-left: 0px; width: 96%">
       <div id="main">
                    <div id="import" runat="server" class="importdiv">
                        <div id="div1" runat="server" class="col" style="display: none;">
                        <table id="tbl1" class="filterTable tbl">
                            <asp:HiddenField ID="hdntask" runat="server" />
                            <tr>
                                <td>
                                    <asp:Label runat="server" Text="Projects" ID="lbprojectid"></asp:Label></td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlviewprojectid" CssClass="form-control textsize1"></asp:DropDownList></td>
                                   
                                <td>
                                    <asp:Button runat="server" ID="btnView" Text="View" CssClass="Buttons" OnClick="btnView_Click" /></td>
                                <td>
                                    <asp:Button runat="server" ID="btnAdd" Text="Add" CssClass="Buttons" OnClick="btnAdd_Click" />
                                    <asp:Button runat="server" ID="btnhdsave" Text="text" OnClick="btnhdSave_Click" Visible="false" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnrowindex" runat="server" />
                                    <asp:HiddenField ID="hdnweekclickcount" runat="server" /> 
                                </td>
                                                     
                            </tr>
                        </table>
                    </div>
                        </div>
                </div>
          <asp:UpdatePanel runat="server">
              <ContentTemplate>
                 <asp:HiddenField ID="hdnGridregion" runat="server" />
                   <div id="div2" runat="server" class="col" style="display: none;">
                <div id="gridContainer" runat="server" style="width: 100%;height:78vh; position: relative;overflow:auto">
                    <asp:GridView ID="GVView" CssClass="grid" runat="server" Style="width: 100%;border:black 1.5px solid" OnRowDeleting="GridView1_RowDeleting"
                        AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found" EmptyDataRowStyle-CssClass="empty-row-style">

                        <Columns>
                            <asp:TemplateField HeaderText="Main Task" HeaderStyle-Width="600px">
                                <ItemTemplate>
                                    <asp:Label ID="lbMainTask" Text='<%# Eval("Maintask") %>' runat="server"> 
                                    </asp:Label>
                                     <asp:HiddenField ID="hdnid" runat="server" Value='<%#Eval("Id") %>' />
                                    <asp:HiddenField ID="hdnweekno" runat="server" Value='<%#Eval("Weekno") %>' />
                                    <asp:HiddenField ID="hdnyear" runat="server" Value='<%#Eval("Year") %>' />
                                    <asp:HiddenField ID="hdnprojectid" runat="server" Value='<%#Eval("Projectid") %>' />
                               <asp:HiddenField ID="hdnmaintaskstatus" runat="server" Value='<%#Eval("MaintaskStatus") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                            <asp:TemplateField HeaderText="Estimated Effort (HH:mm)" HeaderStyle-Width="20px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtEstimatedEffort" Text='<%# Eval("Estimatedeffort") %>' runat="server" CssClass="form-control textsize2 allow-hh-mm-format estimated-effort" AutoCompleteType="Disabled"  />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Delivery Date" HeaderStyle-Width="80px">
                                <ItemTemplate>
                                    <asp:Label ID="lbDeliverydate" Text='<%# Eval("DeliveryDate") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                          <asp:TemplateField HeaderText="Status" HeaderStyle-Width="80px">
                                <ItemTemplate>
                                    <asp:Label ID="lbStatus" Text='<%# Eval("MaintaskStatus") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="60px">
                                <ItemTemplate>
                                    <div style="text-align: center">
                                        <asp:LinkButton runat="server" ID="lbEdit" CssClass="glyphicon glyphicon-pencil  EditlinkBtn" ToolTip="Edit" OnClick="lbEdit_Click"></asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lbDelete" CssClass="glyphicon glyphicon-trash  DeletelinkBtn" ToolTip="Delete" CommandName="Delete"></asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                </div>

                   <div class="modal  add-edit-modal" id="newtask" tabindex="-1">
                    <div class="modal-dialog" style="width: 750px">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Label class="modal-title" ID="Taskdetails" runat="server">New Task</asp:Label>
                                <a data-dismiss="modal" class="glyphicon glyphicon-remove closeButton"></a>
                            </div>
                            <div class="modal-body" style="overflow:auto ;height:65vh">
                                <table class="modal-table1" >
                                    <tr >
                                        <td>
                                            <asp:Label runat="server" Text="Number Of Rows To Add" ID="lbrows" CssClass="colorset"></asp:Label></td>

                                        <td>
                                            <asp:TextBox runat="server" ID="txtaddrows" autogeneratedField="False" CssClass="form-control textsize"  AutoCompleteType="Disabled"></asp:TextBox>
                                        <td><asp:Label ID="lbAddProjectID" runat="server" Text="Project ID" CssClass="colorset"></asp:Label></td>
                                        <td><asp:DropDownList runat="server" ID="ddlProjectID" CssClass="form-control textsize"></asp:DropDownList></td>
                                        <%--<td>
                                            <asp:Label runat="server" Text="Year:" ID="lbaddyear1" CssClass="colorset"></asp:Label></td>
                                        <td>
                                            <asp:Label runat="server" ID="lbaddyear"></asp:Label></td>
                                        <td></td>
                                        <td></td>

                                        <td>
                                            <asp:Label runat="server" Text="Week No:" ID="lbaddweekno" CssClass="colorset"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddladdweekno" CssClass="form-control "></asp:DropDownList>
                                        </td>--%>

                                        <%--<td></td>--%>
                                    </tr>
                                </table>

                                <asp:HiddenField ID="hdnfield2" runat="server" />
                                <div id="gridContainer1" class="gridContainer" runat="server" Style="width: 98%;display:inline-block;overflow: auto;height: 50vh;">
                                    <asp:GridView ID="GVAddtask" CssClass="grid" runat="server"   
                                        AutoGenerateColumns="false" ShowHeaderWhenEmpty="true"  ClientIDMode="Static">
                                        <Columns>
                                            <%--<asp:TemplateField HeaderText="Project ID"  >
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" list="dladdprojectid" ID="txtaddprojectid" Text='<%#Eval("Projectid") %>' CssClass="form-control textsize1"></asp:TextBox>
                                                    <datalist id="dladdprojectid" runat="server" clientidmode="AutoID"></datalist>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                            <asp:TemplateField HeaderText="Main Task" HeaderStyle-Width="200px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtaddMaintask" Text='<%# Eval("Maintask") %>' runat="server" CssClass="form-control textsize1" TextMode="MultiLine" Height="50px" Width="350px"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <%--<asp:TemplateField HeaderText="Task Type"  >
                                                <ItemTemplate>
                                                    <asp:DropDownList runat="server" CssClass="form-control textsize1" ID="ddladdtasktype">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                            <%--<asp:TemplateField HeaderText="Request"  >
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddladdrequest" runat="server" CssClass="form-control textsize1">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                            <asp:TemplateField HeaderText="Estimated Effort (HH:mm)"  >
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtAddEstimatedEffort" Text='<%# Eval("Estimatedeffort") %>' runat="server" CssClass="form-control textsize1 allow-hh-mm-format"  AutoCompleteType="Disabled"></asp:TextBox>
                                                </ItemTemplate>                                                 
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Delivery Date">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDeliverydate" runat="server" CssClass="form-control textsize1 timepicker"  AutoCompleteType="Disabled" Text='<%#Eval("DeliveryDate") %>'></asp:TextBox>
                                                </ItemTemplate>                                                 
                                            </asp:TemplateField>

                                            <%--<asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlAddStatus" runat="server" CssClass="form-control textsize1"></asp:DropDownList>
                                                </ItemTemplate>                                                 
                                            </asp:TemplateField>--%>
                                            <%--<asp:TemplateField HeaderText="Assigned To"  >
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddladdAssignedto" runat="server" CssClass="form-control textsize1">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                            
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div style="padding:0px;width:1%;display:inline-block">
                                    <asp:LinkButton runat="server" ToolTip="Add New Row" CssClass="glyphicon glyphicon-plus-sign" ID="lbAddnewrow" OnClick="lbAddnewrow_Click" Style="font-size:25px"></asp:LinkButton>

                                </div>
                            </div>
                            <div class="modal-footer">

                                <asp:Button runat="server" ID="btnSave" CssClass="btn AddEditBtn" Text="Save" OnClick="btnSave_Click"  />
                                <asp:Button runat="server" ID="btnCancel" CssClass="btn  btn-danger CancelBtn" Text="Cancel" OnClientClick="return clearscreen();" ></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>

                   <div class="modal fade add-edit-modal" id="Edittask" tabindex="-1">
                    <div class="modal-dialog" style="width: 700px">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Label CssClass="modal-title" ID="TaskEditDetails" runat="server">Edit Task Details</asp:Label>
                                <a data-dismiss="modal" class="glyphicon glyphicon-remove closeButton"></a>
                            </div>
                            <div class="modal-body" style="overflow: unset">
                                <table class="modal-table">
                                    <%--<tr >
                                        <td>
                                            <asp:Label runat="server" Text="Year" ID="lbedityear"></asp:Label></td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtedityear" AutoCompleteType="Disabled" CssClass="form-control textsize3" ReadOnly="true"></asp:TextBox></td>
                                        <td>
                                            <asp:Label runat="server" Text="WeekNo" ID="lbeditweekno"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txteditweekno" AutoCompleteType="Disabled" CssClass="form-control textsize3" ReadOnly="true"></asp:TextBox></td>
                                    </tr>--%>
                                    <tr>
                                        <td><Span>Project ID</Span></td>
                                      <td><asp:Label ID="lbeditprojectid" runat="server" CssClass="form-control textsize3" Enabled="false" Backcolor="#EEEEEE"/>
                                        <asp:HiddenField ID="hdneditid" runat="server" /></td>
                                        <td>
                                            <asp:Label runat="server" Text="Main Task" ID="lbeditmaintask"></asp:Label></td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txteditmaintask" AutoCompleteType="Disabled" CssClass="form-control textsize3" TextMode="MultiLine" Height="50px" Width="202px"></asp:TextBox></td>
                                        
                                        <%--<td>
                                            <asp:Label runat="server" Text="Projects" ID="lbeditprojectid"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" list="dleditprojectid" ID="txteditprojectid" CssClass="form-control textsize3"></asp:TextBox>
                                            <datalist id="dleditprojectid" runat="server" clientidmode="static"></datalist></td>
                                        <td>
                                            <asp:Label runat="server" Text="Task Type" ID="lbedittasktype"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList runat="server" CssClass="form-control textsize3" ID="ddledittasktype">
                                            </asp:DropDownList></td>--%>
                                    </tr>
                                    <tr>
                                        
                                        <td>
                                            <asp:Label runat="server" Text="Estimated Effort (HH:mm)" ID="lbeditestimatedeffort"></asp:Label></td>
                                        <td style="position: relative">
                                            <asp:TextBox runat="server" ID="txteditestimatedeffort" AutoCompleteType="Disabled" CssClass="form-control textsize3 allow-hh-mm-format"></asp:TextBox></td>
                                        <td><span>Delivery Date</span></td>
                                        <td><asp:TextBox runat="server" AutoCompleteType="Disabled" ID="txteditdeliverydate" CssClass="form-control textsize3 timepicker"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td><span>Status</span></td>
                                        <td><asp:DropDownList ID="ddleditstatus" runat="server" CssClass="form-control textsize3"></asp:DropDownList></td>
                                    </tr>
                                    <%--<tr>
                                        <td>
                                            <asp:Label runat="server" Text="Request" ID="lbeditrequest"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="ddleditrequest" runat="server" CssClass="form-control textsize3">
                                            </asp:DropDownList></td>
                                        <td>
                                            <asp:Label runat="server" Text="Assigned To" ID="lbeditassignedto"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="ddleditAssignedto" runat="server" CssClass="form-control textsize3">
                                            </asp:DropDownList></td>
                                    </tr>--%>

                                </table>
                            </div>
                            <div class="modal-footer">
                                <asp:Button runat="server" ID="BtnUpdate" CssClass="btn AddEditBtn" Text="Update" OnClick="btnUpdate_Click" />
                                <asp:Button runat="server" Text="Cancel" ID="BtnEditCancel" CssClass="btn  btn-danger CancelBtn" OnClientClick="return clearscreen();"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>
              </ContentTemplate>
             <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnhdsave" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnView" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
             </Triggers>
        </asp:UpdatePanel>
    </div>
    <div class="modal  add-edit-modal" id="myConfirmationModal" role="dialog" style="min-width: 300px;">
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

    <script type="text/javascript">
        $(document).ready(function () {
            ControlSetter();
        });
        function TaskValidation() {
            var grid = document.getElementById("<%=GVAddtask.ClientID%>");
            for (var i = 1; i < $("#GVAddtask tr").length; i++) {
                debugger;
                let tr = $("#GVAddtask tr")[i]; 
                
                if ($('[id$=ddlProjectID]').val() != "" || $(tr).find("#txtaddMaintask").val() != "") 
                {
                    if ($('[id$=ddlProjectID]').val() == "") {
                        WarningToastr("ProjectID is required");
                        $("[id$=ddlProjectID]")[i - 1].focus();
                        return false;
                     }
                     if ($(tr).find("#txtaddMaintask").val() == "") {
                         WarningToastr("Maintask is required at row" +i);
                        $("[id$=txtaddMaintask]")[i - 1].focus();
                        return false;
                    }                                       
                    if ($(tr).find("#txtAddEstimatedEffort").val() == "") {
                        WarningToastr("EstimatedEffort Is Required at row" +i);
                        $("[id$=txtAddEstimatedEffort]")[i - 1].focus;
                        return false;
                    } 
                    if ($(tr).find("#ddlAddStatus").val() == "") {
                        WarningToastr("Status Is Required at row" + i);
                        $("[id$=ddlAddStatus]")[i - 1].focus;
                        return false;
                    }  
                }                   
            }
            return true;
        }
        function openConfirmModal(msg) {
            $('[id*=myConfirmationModal]').modal('show');
            $("#confirmationmessageText").text(msg);
        }
        function saveConfirmNo() {
            $('[id*=myConfirmationModal]').modal('hide');
        }
        function ControlSetter() {
            $('.col').show();
            $(".datetimepicker").datetimepicker({
                format: 'HH',
                locale: 'en-US'
            });
            $('[id$=txtyear]').datepicker({
                minViewMode: 2,
                format: 'yyyy',
                todayHighlight: true,
                autoclose: true,
                language: 'en-US',
            });
            $('[id$=txtedityear]').datepicker({
                minViewMode: 2,
                format: 'yyyy',
                todayHighlight: true,
                autoclose: true,
                language: 'en-US',
            });
            $(".timepicker").datepicker({
                viewMode: "date",
                minViewMode: "date",                
                format: 'dd-mm-yyyy',
                todayHighlight: true,
                autoclose: true,
                language: 'en-US',
            });
            let keyPressed = false;
            $(".allow-hh-mm-format").keypress(function (evt) {
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                var pos = evt.target.selectionStart;
                if ((charCode < 48 || charCode > 57)) {
                    return false;
                }
                if ($(this).val().length > 4) {
                    return false;
                }

                keyPressed = true;
                console.log("press: pos - " + pos + ", length: " + $(this).val().length);
                return true;
            });
            $(".allow-hh-mm-format").keyup(function (evt) {
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                var pos = evt.target.selectionStart;
                console.log("up: pos - " + pos + ", length: " + $(this).val().length);

                let position = 2;

                if ($(this).val().length >= 2 && !$(this).val().includes(":")) {
                    let value = [$(this).val().slice(0, position), ":", $(this).val().slice(position)].join('');
                    $(this).val(value);
                }

                if ($(this).val().length == 3 && $(this).val().includes(":")) {
                    $(this).val($(this).val().replace(":", ""));
                }
                if (keyPressed) {
                    keyPressed = false;
                }
                return true;
            });
            $(".allow-hh-mm-format").blur(function () {
                debugger;
                let value = $(this).val().substring(0, 5);
                if (value.length == 1) {
                    $(this).val("0" + value + ":00");
                } else if (value.length == 2) {
                    $(this).val(value + ":00");
                }
                else if (value.length == 4 && value.includes(":")) {
                    //$(this).val(value.substring(0, 2) + ":" + value.substring(2, 3) + "0");
                    $(this).val(value + "0");
                } else {
                    $(this).val(value);
                }
            });
        }
        $(".estimated-effort").keydown(function (event) {
            if (event.key === "Enter") {
                let rowIndex = ($(this).closest('tr').prevAll().length) - 1;
                $('[id$=hdnrowindex]').val(rowIndex);
                __doPostBack('<%= btnhdsave.UniqueID%>', '');
            }
        });
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            let keyPressed = false;
            $(".dropdown-container2").css("display", "block");
            $(".allow-hh-mm-format").keypress(function (evt) {

                var charCode = (evt.which) ? evt.which : evt.keyCode;
                var pos = evt.target.selectionStart;
                if ((charCode < 48 || charCode > 57)) {
                    return false;
                }
                if ($(this).val().length > 4) {
                    return false;
                }
                keyPressed = true;
                console.log("press: pos - " + pos + ", length: " + $(this).val().length);
                return true;
            });
            $(".allow-hh-mm-format").keyup(function (evt) {
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                var pos = evt.target.selectionStart;
                console.log("up: pos - " + pos + ", length: " + $(this).val().length);

                let position = 2;

                if ($(this).val().length >= 2 && !$(this).val().includes(":")) {
                    let value = [$(this).val().slice(0, position), ":", $(this).val().slice(position)].join('');
                    $(this).val(value);
                }

                if ($(this).val().length == 3 && $(this).val().includes(":")) {
                    $(this).val($(this).val().replace(":", ""));
                }
                if (keyPressed) {
                    keyPressed = false;
                }
                return true;
            });
            $(".allow-hh-mm-format").blur(function () {
                debugger;
                let value = $(this).val().substring(0, 5);
                if (value.length == 1) {
                    $(this).val("0" + value + ":00");
                } else if (value.length == 2) {
                    $(this).val(value + ":00");
                }
                else if (value.length == 4 && value.includes(":")) {
                    //$(this).val(value.substring(0, 2) + ":" + value.substring(2, 3) + "0");
                    $(this).val(value + "0");
                } else {
                    $(this).val(value);
                }
            });
            $(document).ready(function () {
                ControlSetter();
            });

            $(".estimated-effort").keydown(function (event) {
                if (event.key === "Enter") {
                    let rowIndex = ($(this).closest('tr').prevAll().length) - 1;
                    $('[id$=hdnrowindex]').val(rowIndex);
                    __doPostBack('<%= btnhdsave.UniqueID%>', '');
                }
            });
        });
        document.getElementById("headerName").textContent = "Main Task";
        $(".dropdown-container2").css("display", "block");
        var user = "<%= (String)Session["username"] %>";
        $("#lblUserID").text("Hi " + user);
    </script>
</asp:Content>
