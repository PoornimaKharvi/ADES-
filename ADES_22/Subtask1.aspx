<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Subtask1.aspx.cs" EnableEventValidation="false" Inherits="ADES_22.Subtask1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="js/bootstrap-multiselect.js"></script>
    <link href="Css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="Scripts/DateTimePicker331/moment.js"></script>
    <script src="Scripts/DateTimePicker331/bootstrap-datetimepicker.min.js"></script>
    <link href="Scripts/DateTimePicker331/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <script src="Scripts/DateTimePicker/bootstrap-datepicker.js"></script>
    <link href="Scripts/DateTimePicker/bootstrap-datepicker3.css" rel="stylesheet" />
    <script src="Scripts/DateTimePicker/bootstrap-datepicker.en-IE.min.js"></script>
    <script src="Scripts/DateTimePicker/bootstrap-datepicker.zh-CN.min.js"></script>
    <style>
        .multiselect, .textsize2 {
            width: 130px;
        }

        .textsize1 {
            width: 70px;
        }

        .tbl tr td {
            padding: 5px;
        }

        .textsize3 {
            width: 170px
        }

        .textsize4 {
            width: 140px;
        }
        .textsize5{
            width:125px;
        }
        .textsize6{
            width:130px;
        }
        .textsize7{
            width:65px;
        }
        .textsize9{
            width:85px;
        }
        .textsize8{
            width:250px;
        }
        .add-edit-modal .modal-table1 tr td {
            padding: 8px;
        }

        .add-edit-modal .modal-table1 {
            width: 100%;
        }

        .add-edit-modal .modal-table5 {
            width: 50%;
        }

        .gridContainer {
            padding: 0px;
            margin: auto;
            margin-top: 10px;
        }
         .textboxcss {            border: none;            background-color: transparent;              color: black;        }        .select {            -webkit-appearance: none;        }        select option {            color: black;        }
    </style>
 <asp:UpdatePanel runat="server"> 
   <ContentTemplate>
     <div class="container-fluid" style="margin-top: 1px; margin-left: 0px;padding-left:10px; width: 100%">
        <div id="main" style="margin-top:-9px">
            <div id="import" runat="server" class="importdiv" style="overflow: unset">
                <div id="div2" runat="server" class="col" style="display: none;">
                <table id="tbl1" class="filterTable tbl">
                    <asp:HiddenField ID="hdnsubtask" runat="server" />
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lbteam" Text="Team" Visible="false"></asp:Label></td>
                        <td>
                            <asp:ListBox runat="server" ID="lbxteam" CssClass="multiDropdown form-control textsize2" SelectionMode="Multiple" Visible="false" /></td>

                        <td>
                            <asp:Label runat="server" Text="Projetcs"></asp:Label></td>
                        <td>
                            <asp:ListBox runat="server" ID="lbprojectid" CssClass="multiDropdown form-control textsize2" SelectionMode="Multiple" /></td>

                        <td>
                            <asp:Label ID="lbcustomer" runat="server" Text="Customer" Visible="false"></asp:Label></td>
                        <td>
                            <asp:ListBox runat="server" ID="lbxcustomer" CssClass="multiDropdown form-control textsize2 " SelectionMode="Multiple" Visible="false" /></td>

                        <td>
                            <asp:Label ID="lbemployee" runat="server" Text="Employee"></asp:Label></td>
                        <td>
                            <asp:ListBox runat="server" ID="lbxemployee" CssClass="multiDropdown form-control textsize2" SelectionMode="Multiple" /></td>

                        <td>
                            <asp:Label runat="server" Text="Week No"></asp:Label></td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlweekno" CssClass="form-control textsize1" /></td>

                        <td>
                            <asp:Label ID="lbweekno2" runat="server" Text="Week No" Visible="false"></asp:Label></td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlweekno2" CssClass="form-control textsize1" Visible="false" />
                        </td>

                        <td>
                            <asp:Button runat="server" ID="btnview" Text="View" CssClass="Buttons" OnClick="btnView_Click" />
                            <asp:Button runat="server" ID="btnhdsave" Text="text" Visible="false" ClientIDMode="Static"  />
                            <asp:Button runat="server" ID="btnFileDownLoad" Visible="false" ClientIDMode="Static" OnClick="btnFileDownLoad_Click"/>
                            <asp:HiddenField ID="hdnrowindex" runat="server" />
                             <asp:HiddenField ID="hfRowIndex" ClientIDMode="Static" runat="server" />
                            <asp:Button runat="server" ID="btnSaveRow" Text="Save" CssClass="Buttons" Onclick="btnSaveRow_Click1" />
                        </td>

                       
                        <td >
                            <asp:RadioButtonList ID="rbtnViewChange" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbtnViewChange_SelectedIndexChanged" RepeatLayout="Flow">
                                <asp:ListItem Text="Planner View" Value="Planner View" runat="server" />
                                <asp:ListItem Text="Dashboard View" Value="Dashboard View" runat="server" />
                                </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
       </div>
        <asp:HiddenField ID="hdnGridregion" runat="server" />
        <div id="gridContainer" class="gridContainer" runat="server" Style="text-align:center;overflow:auto;width:99%;margin-left:2px;height:84vh;margin-top: -3px;">
                    <div style="overflow:unset">
                        <div id="div3" runat="server" class="col" style="display: none;">
                    <asp:GridView ID="GVView" CssClass="grid" runat="server"  OnRowDataBound="GVView_RowDataBound" OnRowCommand="GVView_RowCommand" OnRowDeleting="GridView1_RowDeleting" FooterStyle-BackColor="#EEEEEE"
                        AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found"  EmptyDataRowStyle-CssClass="empty-row-style" ShowFooter="true" Style="width:115%;height:100%;border:black 1.5px solid" ClientIDMode="Static">
                        
                    <Columns>
                     <asp:TemplateField HeaderText="Employee" HeaderStyle-Width="125px">
                         <ItemTemplate>
                          <asp:DropDownList ID="ddlPlanEmployee"  runat="server" CssClass="form-control textsize5 textboxcss select" DataSource='<%# Eval("EmpList") %>'  OnSelectedIndexChanged="ddlPlanEmployee_SelectedIndexChanged" AutoPostBack="true"/>
                          <asp:HiddenField ID="hdnplanemployee" runat="server" Value='<%# Eval("Assignedto") %>' ClientIDMode="Static" />
                          <asp:HiddenField runat="server" Value='<%# Eval("Id") %>' ID="hdnplanidd" />
                          <asp:HiddenField runat="server" Value='<%# Eval("Weekno") %>' ID="hdnplanweekno" />
                          <asp:HiddenField runat="server" Value='<%# Eval("Year") %>' ID="hdnplanyear"/>
                          <asp:HiddenField runat="server" Value='<%# Eval("SubtaskStatus") %>' ID="hdnplanstatus" />
                              <asp:HiddenField runat="server" ID="hdnUpdate" ClientIDMode="Static" />
                        </ItemTemplate>
                        <FooterTemplate>
                           <asp:DropDownList ID="ddlfootEmployee" runat="server" CssClass="form-control textsize5" OnSelectedIndexChanged="ddlfootEmployee_SelectedIndexChanged" AutoPostBack="true" />
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Project ID" HeaderStyle-Width="125px" >
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlplanprojectid"   runat="server" CssClass="form-control textsize5 textboxcss select" OnSelectedIndexChanged="ddlplanprojectid_SelectedIndexChanged" AutoPostBack="true"/>
                            <asp:HiddenField ID="hdnplanprojectid" runat="server" Value='<%# Eval("Projectid") %>' ClientIDMode="Static"/>
                        </ItemTemplate>
                        <FooterTemplate>
                             <asp:DropDownList ID="ddlfootProjectID" runat="server" CssClass="form-control textsize5" OnSelectedIndexChanged="ddlfootProjectID_SelectedIndexChanged" AutoPostBack="true"/>
                        </FooterTemplate>
                   </asp:TemplateField>

               <asp:TemplateField HeaderText="Task Type" HeaderStyle-Width="135px">
                 <ItemTemplate>
                   <asp:DropDownList ID="ddlplanTasktype" runat="server" CssClass="form-control textsize6 textboxcss select" DataSource='<%# Eval("TaskTypelist") %>'/>
                     <asp:HiddenField runat="server" ID="hdnplantasktype" Value='<%#Eval("Tasktype")%>' ClientIDMode ="Static" />
                 </ItemTemplate>
                 <FooterTemplate>
                    <asp:DropDownList ID="footTaskType" runat="server" CssClass="form-control textsize6" />
                 </FooterTemplate>
               </asp:TemplateField>
                          
              <asp:TemplateField HeaderText="MainTask" HeaderStyle-Width="135px">
                 <ItemTemplate>
                   <asp:DropDownLIst ID="ddlplanmaintask" runat="server" CssClass="form-control textsize6 textboxcss select" />
                   <asp:HiddenField runat="server" ID="hdnplanmaintask" Value='<%#Eval("Maintask")%>' ClientIDMode="Static"/>
                       <asp:HiddenField runat="server" ID="hdnmaintaskidd" Value='<%#Eval("MainTaskIDD")%>' ClientIDMode="Static"/>
                 </ItemTemplate>
                 
                 <FooterTemplate>
                    <asp:DropDownList ID="footMainTask" runat="server" CssClass="form-control textsize6" />
                 </FooterTemplate>
               </asp:TemplateField>
      
             <asp:TemplateField HeaderText="SubTask" >
                 <ItemTemplate>
                   <asp:TextBox  ID="lbSubTask" runat="server" CssClass="form-control textsize8 textboxcss" Text='<%#Eval("Subtask")%>' AutoCompleteType="Disabled" TextMode="MultiLine"
                       Rows="3"/>
                 </ItemTemplate>
                 
                 <FooterTemplate>
                    <asp:TextBox ID="footSubTask" runat="server" CssClass="form-control textsize8" AutoCompleteType="Disabled"  TextMode="MultiLine" Rows="3"/>
                 </FooterTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Manual Entry" HeaderStyle-Width="120px">
                 <ItemTemplate>
                   <asp:TextBox ID="lbManualEntry" runat="server" CssClass="form-control textsize4 textboxcss" Text='<%#Eval("ManualEntryRemark")%>' AutoCompleteType="Disabled" TextMode="MultiLine" Rows="3"/>
                 </ItemTemplate>
                 
                 <FooterTemplate>
                    <asp:TextBox ID="footManualEntry" runat="server"  CssClass="form-control textsize4" AutoCompleteType="Disabled"  TextMode="MultiLine" Rows="3"/>
                 </FooterTemplate>
               </asp:TemplateField>
             
             <asp:TemplateField HeaderText="Estimated Effort (HH:mm)" HeaderStyle-Width="50px">
                 <ItemTemplate>
                   <asp:TextBox ID="lbEstimatedEffort" runat="server" CssClass="form-control textsize7 allow-hh-mm-format textboxcss "  Text='<%#Eval("Estimatedeffortsub")%>' AutoCompleteType="Disabled"/>
                 </ItemTemplate>
                 
                 <FooterTemplate>
                    <asp:TextBox ID="footEstimatedEffort" runat="server" CssClass="form-control textsize7  allow-hh-mm-format" style="position:relative;overflow:unset" AutoCompleteType="Disabled"/>
                 </FooterTemplate>
               </asp:TemplateField>
                            
               <asp:TemplateField HeaderText="Dependancy" HeaderStyle-Width="135px">
                 <ItemTemplate>
                   <asp:DropDownList ID="ddlplandependancy" runat="server" CssClass="form-control textsize6 textboxcss select" DataSource='<%# Eval("Dependencylist") %>'/>
                     <asp:HiddenField runat="server" ID="hdnplandependancy" Value='<%#Eval("Dependencies")%>' ClientIDMode ="Static" />
                 </ItemTemplate>
                 
                 <FooterTemplate>
                    <asp:DropDownList ID="footDependancy" runat="server" CssClass="form-control textsize6" />
                 </FooterTemplate>
               </asp:TemplateField>
 
             <asp:TemplateField HeaderText="Request" HeaderStyle-Width="135px">
                 <ItemTemplate>
                   <asp:DropDownList ID="ddlplanrequest" runat="server" CssClass="form-control textsize6 textboxcss select" DataSource='<%# Eval("Requestlist") %>'/>
                     <asp:HiddenField runat="server" ID="hdnplanrequest" Value='<%#Eval("Request")%>' ClientIDMode ="Static" />
                 </ItemTemplate>
                 
                 <FooterTemplate>
                    <asp:DropDownList ID="footRequest" runat="server"  CssClass="form-control textsize6" />
                 </FooterTemplate>
               </asp:TemplateField>

                <asp:TemplateField HeaderText="Delivery Date" HeaderStyle-Width="80px">
                 <ItemTemplate>
                   <asp:TextBox ID="lbDeliveryDate" runat="server" CssClass="form-control textsize9 datepicker textboxcss select" style="position:relative" Text='<%#Eval("DeliveryDate")%>' AutoCompleteType="Disabled"/>
                 </ItemTemplate>
                 
                 <FooterTemplate>
                    <asp:TextBox ID="footDeliveryDate" runat="server" CssClass="form-control textsize9 datepicker" AutoCompleteType="Disabled"/>
                 </FooterTemplate>
               </asp:TemplateField>
                              
               <asp:TemplateField HeaderText="Action" HeaderStyle-Width="35px">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="lbDelete" CssClass="glyphicon glyphicon-trash  DeletelinkBtn" ToolTip="Delete" CommandName="Delete"></asp:LinkButton>
                    </ItemTemplate>
                    <FooterTemplate>
                    <asp:LinkButton ID="lbfootSave" runat="server" CssClass="glyphicon glyphicon-plus-sign" ToolTip="Save" CommandName="Save" Style="font-size:20px"/>
                 </FooterTemplate>
               </asp:TemplateField>
                            
                        </Columns>
                            
                    </asp:GridView>
                </div>
                        </div>
        
        <div id="div1" runat="server" class="col" style="display: none;">
        <%--<div style="padding-left:10px; width:96%;overflow:auto;height:79vh;margin-top:-530px">--%>
            <asp:ListView runat="server" ID="lvdashboardview" ItemPlaceholderID="placeHoldersubtask">
                <LayoutTemplate>
                    <table class="filterTable tbl grid" style="width: 150%; border:black 1.5px solid">
                        <tr runat="server" visible='<%# Eval("HeaderVisibility") %>'>
                            <th>Week No</th>
                            <th>Employee</th>
                            <th>Project ID</th>
                            <th>Task Type</th>
                            <th>MainTask</th>
                            <th>SubTask</th>
                            <th>Manual Entry</th>
                            <th>Estimated Effort (HH:mm)</th>
                            <th>Dependancy</th>
                            <th>Request</th>
                            <th>Delivery Date</th>
                            <th>Remarks Engineer</th>
                            <th>View MOM</th>
                            <th>Subtask Status</th>
                            <th>Maintask Status</th>
                        </tr>
                        <tr>
                            <asp:PlaceHolder runat="server" ID="placeHoldersubtask" />
                        </tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td style="width:50px">
                            <asp:Label ID="lbweekno" Text='<%# Eval("Weekno") %>' runat="server" />
                        </td>
                        <td style="width:100px">
                            <asp:Label ID="lbemployee" Text='<%# Eval("Employees") %>' runat="server" />
                        </td>
                        <td style="width:100px">
                            <asp:Label ID="lbsubProjectid" Text='<%# Eval("Projectid") %>' runat="server" />
                            <asp:HiddenField ID="hdnweekno" runat="server" Value='<%#Eval("Weekno") %>' />
                            <asp:HiddenField ID="hdnyear" runat="server" Value='<%#Eval("Year") %>' />
                            <asp:HiddenField ID="hdnaddmaintaskidd" Value='<%# Eval("MainTaskIDD") %>' runat="server" />
                            <asp:HiddenField ID="hdnid" Value='<%# Eval("Id") %>' runat="server" />
                            <asp:HiddenField ID="hdnproblemtype" Value='<%# Eval("Request") %>' runat="server" />
                        </td>
                        <td style="width:100px"> 
                            <asp:Label ID="lbtasktype" Text='<%#Eval("Tasktype") %>' runat="server" />
                        </td>
                        <td style="width:100px">
                            <asp:Label ID="lbsubMainTask" Text='<%# Eval("Maintask") %>' runat="server" />
                        </td>
                        <td>
                            <asp:Label ID="lbsubtask" Text='<%# Eval("Subtask") %>' runat="server" />
                        </td>
                        <td style="width:120px">
                            <asp:Label ID="lbmanualentryremark" Text='<%#Eval("ManualEntryRemark") %>' runat="server" />
                        </td>
                        <td style="width:60px">
                            <asp:Label ID="lbestimatedeffort" Text='<%#Eval("Estimatedeffortsub") %>' runat="server" />
                        </td>
                        <td style="width:100px">
                            <asp:Label ID="lbdependancy" Text='<%#Eval("Dependencies") %>' runat="server" />
                        </td>
                        <td>
                            <asp:Label ID="lbrequest" Text='<%#Eval("Request") %>' runat="server" />
                        </td>
                        <td style="width: 90px">
                            <asp:Label ID="lbdeliverydate" Text='<%#Eval("DeliveryDate") %>' runat="server" />
                        </td>
                        <td>
                         <asp:Label ID="lbremarksengineer" Text='<%#Eval("RemarksEngineer") %>' runat="server" />
                        </td>
                        <td style="width:200px">
                            <asp:ListView runat="server" ID="lvFileDetails" DataSource='<%#Eval("fileDetails")%>' >
                                <LayoutTemplate>
                                    <table style="width:100%;border: #80808087 1px solid;">
                                        <tr>
                                          <asp:PlaceHolder runat="server" ID="itemplaceholder" />
                                        </tr>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><asp:LinkButton runat="server" ID="lbfilename" Text='<%#Eval("FileName") %>'
                                            OnClick="btnFileDownLoad_Click"></asp:LinkButton>
                                        <asp:HiddenField runat="server" ID="hf" Value='<%#Eval("FileInBase64") %>' />
                                            <asp:HiddenField runat="server" ID="hfbytevalue" Value='<%#Eval("Fileinbyte") %>' />
                                            <asp:HiddenField runat="server" ID="hdnfilename" Value='<%#Eval("FileName") %>' />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                         </td>

                        <td style="width:80px">
                            <asp:Label ID="lbstatus" Text='<%#Eval("SubtaskStatus") %>' runat="server" />
                        </td>
                        <td style="width:80px">
                            <asp:Label ID="Label1" Text='<%#Eval("MaintaskStatus") %>' runat="server" />
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
            <asp:PostBackTrigger ControlID="lvdashboardview" />
           
        </Triggers>
    </asp:UpdatePanel>

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
        
        $("[id$=GVView]").on("click", "td", function () {                $(this).closest('tr').find('#hdnUpdate').val("updated");                var tblID = $(this).closest('table').prop('id');                var tbl = document.getElementById(tblID);                var tblRowCount = tbl.rows.length - 1;                var currentTR = $(this).closest('tr');                var currentClickRowIndex = $(currentTR).index();                if (tblRowCount == currentClickRowIndex) {                    return;                }                       $("[id$=GVView] tr:not(:last-child) td").find('input').removeClass("form-control");            $("[id$=GVView] tr:not(:last-child) td").find('input').addClass("textboxcss");            $("[id$=GVView] tr:not(:last-child) td").find('select').addClass("select");            $("[id$=GVView] tr:not(:last-child) td").find('select').addClass("textboxcss");            $("[id$=GVVIew] tr:not(:last-child) td").find('select').removeClass("form-control");            $(this).closest('td').find('input').removeClass("textboxcss");            $(this).closest('td').find('input').addClass("form-control");            $(this).closest('td').find('select').addClass("form-control");            $(this).closest('td').find('select').removeClass("textboxcss");            $(this).closest('td').find('select').removeClass("select");            $("[id$=GVView] tr:not(:last-child) td").find('input[type="checkbox"]').removeClass("form-control textboxcss");        });


        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            $(".dropdown-container2").css("display", "block");
            
            $(document).ready(function () {
                ControlSetter();
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
                let value = $(this).val().substring(0, 5);
                if (value.length == 1) {
                    $(this).val("0" + value + ":00");
                } else if (value.length == 2) {
                    $(this).val(value + ":00");
                }
                else if (value.length == 4 && value.includes(":")) {
                    $(this).val(value + "0");
                } else {
                    $(this).val(value);
                }

            });
            $("[id$=GVView]").on("click", "td", function () {                $(this).closest('tr').find('#hdnUpdate').val("updated");                var tblID = $(this).closest('table').prop('id');                var tbl = document.getElementById(tblID);                var tblRowCount = tbl.rows.length - 1;                var currentTR = $(this).closest('tr');                var currentClickRowIndex = $(currentTR).index();                if (tblRowCount == currentClickRowIndex) {                    return;                }                $("[id$=GVView] tr:not(:last-child) td").find('input').removeClass("form-control");                $("[id$=GVView] tr:not(:last-child) td").find('input').addClass("textboxcss");                $("[id$=GVView] tr:not(:last-child) td").find('select').addClass("select");                $("[id$=GVView] tr:not(:last-child) td").find('select').addClass("textboxcss");                $("[id$=GVVIew] tr:not(:last-child) td").find('select').removeClass("form-control");                $(this).closest('td').find('input').removeClass("textboxcss");                $(this).closest('td').find('input').addClass("form-control");                $(this).closest('td').find('select').addClass("form-control");                $(this).closest('td').find('select').removeClass("textboxcss");                $(this).closest('td').find('select').removeClass("select");                $("[id$=GVView] tr:not(:last-child) td").find('input[type="checkbox"]').removeClass("form-control textboxcss");            });
            $(".datetimepicker").datetimepicker({
                format: 'HH',
                locale: 'en-US'
            });
            $(".estimated-effort").keydown(function (event) {
                if (event.key === "Enter") {
                    let rowIndex = ($(this).closest('tr').prevAll().length) - 1;
                    $('[id$=hdnrowindex]').val(rowIndex);
                    __doPostBack('<%= btnhdsave.UniqueID%>', '');
                }
            });
        });
        $(document).ready(function () {
            ControlSetter();
        });
        function openConfirmModal(msg) {
            $('[id*=myConfirmationModal]').modal('show');
            $("#confirmationmessageText").text(msg);
        }
        function ControlSetter() {
            $('.col').show();
            $('.multiDropdown').multiselect({
                includeSelectAllOption: true,
                enableFiltering: true,
                maxHeight: 500,
                buttonWidth: '170px',
                enableCaseInsensitiveFiltering: true
            });
            $(".datetimepicker").datetimepicker({
                format: 'HH',
                locale: 'en-US'
            });

            $('.datepicker').datepicker({
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
                let value = $(this).val().substring(0, 5);
                if (value.length == 1) {
                    $(this).val("0" + value + ":00");
                } else if (value.length == 2) {
                    $(this).val(value + ":00");
                }
                else if (value.length == 4 && value.includes(":")) {
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
        $(".dropdown-container2").css("display", "block");
        var user = "<%= (String)Session["username"] %>";
        $("#lblUserID").text("Hi " + user);
        document.getElementById("headerName").textContent = "Sub Task";
    </script>
</asp:Content>
