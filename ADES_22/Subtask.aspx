<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Subtask.aspx.cs" Inherits="ADES_22.Subtask" %>
<%--<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>--%>
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
    <link href="Scripts/DateTimePicker331/Custom.css" rel="stylesheet" />

    <style>
        .textsize1 {
            width: 150px;
        }

        .textsize2 {
            width: 190px;
        }

        .textsize3 {
            width: 230px;
        }
        .textsize4{
            width:170px;
        }

        .colorset {
            font-weight: 500;
            color: darkslateblue;
        }

        .add-edit-modal .modal-table {
            width: 59%;
        }

        .add-edit-modal .modal-table3 {
            width: 30%;
        }

        .add-edit-modal .modal-table tr td {
            padding: 2.5px;
        }

        .add-edit-modal .modal-table1 tr td {
            padding: 6px;
        }

        .add-edit-modal .modal-table1 {
            width: 100%;
        }

        .add-edit-modal .modal-table5 {
            width: 55%;
        }

            .add-edit-modal .modal-table5 tr td {
                padding: 4px;
            }
            .OuterDiv {
            background-color: whitesmoke;
            padding: 5px;
            width: 100px;
            border-radius: 15px;
        }
    </style>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="container-fluid" style="margin-top: 0px; margin-left: 0px; width: 96%">
                <div id="main">
                    <div id="import" runat="server" class="importdiv">
                        <table id="tbl1" class="filterTable tbl">
                            <asp:HiddenField ID="hdnsubtask" runat="server" />
                            <tr>
                                <td>
                                    <asp:Label runat="server" Text="Projetcs"></asp:Label></td>
                                <td>
                                   <asp:ListBox runat="server" ID="lbprojectid" CssClass="multiDropdown form-control" SelectionMode="Multiple" />
                                    </td>
                                   
                               
                                <td>
                                    <span>Employee</span>
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlemployee" CssClass="form-control textsize1" ></asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Label runat="server" Text="Week No."></asp:Label></td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtweekno" CssClass="form-control textsize1" AutoCompleteType="Disabled" TextMode="Number" min="1" max="53" step="1"></asp:TextBox></td>


                                <td>
                                <td>
                                   <asp:Button runat="server" ID="btnview" Text="View" CssClass="Buttons" OnClick="btnView_Click" />
                                 
                                 
                                    <asp:Button runat="server" ID="btnhdsave" Text="text" OnClick="btnhdSave_Click" Visible="false" ClientIDMode="Static" />
                                    <asp:Button runat="server" ID="btnAdd" Text="Add" CssClass="Buttons" OnClick="btnAdd_Click" />
                                    <asp:HiddenField ID="hdnrowindex" runat="server" />                               
                                    <%--<asp:Button runat="server" ID="btnTask" Text="Task" CssClass="Buttons" OnClick="btnTask_Click" />--%></td>
                               <td>  <asp:LinkButton runat="server" ID="lbPrevweek" ToolTip="Previous Week" CssClass="glyphicon glyphicon-chevron-left" OnClick="lbPrevweek_Click" Style="font-size:20px"></asp:LinkButton>
                                  <asp:LinkButton runat="server" ID="lbNextweek" ToolTip="Next Week" CssClass="glyphicon glyphicon-chevron-right" OnClick="lbNextweek_Click" Style="font-size:20px"></asp:LinkButton>
                                  </td>
                                 <td><div class="OuterDiv">
                                       <div class="InnerDiv">
                                           <asp:LinkButton runat="server" ID="lbtnDashboardview" ToolTip="Dashboard View" CssClass="glyphicon glyphicon-modal-window"  Style="font-size:20px"></asp:LinkButton>   
                                 
                                  <asp:LinkButton runat="server" ID="lbtnplanedview" ToolTip="Planned View" CssClass="glyphicon glyphicon-edit" Style="font-size:20px"></asp:LinkButton>
                                        </div>
                                    </div>
                                  </td>

                            </tr>

                        </table>
                    </div>
                </div>

                <asp:HiddenField ID="hdnGridregion" runat="server" />
               <div id="gridContainer" class="gridContainer" runat="server" style="text-align: center; position: relative; overflow: unset;height:78vh; position: relative;overflow:auto">
                   <%--<asp:GridView ID="GVView" CssClass="grid" runat="server" Style="width: 100%"
                        AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found" OnRowDeleting="GridView_RowDeleting" EmptyDataRowStyle-CssClass="empty-row-style">

                        <Columns>
                            <asp:TemplateField HeaderText="Project ID" HeaderStyle-Width="170px">
                                <ItemTemplate>
                                    <asp:Label ID="lbsubProjectid" Text='<%# Eval("Projectid") %>' runat="server"></asp:Label>
                                    <asp:HiddenField ID="hdnaddmaintaskidd" Value='<%# Eval("MainTaskIDD") %>' runat="server" />
                                    <asp:HiddenField ID="hdnid" Value='<%# Eval("Id") %>' runat="server" />
                                    <asp:HiddenField ID="hdnweekno" runat="server" Value='<%#Eval("Weekno") %>' />
                                    <asp:HiddenField ID="hdnyear" runat="server" Value='<%#Eval("Year") %>' />
                                    <asp:HiddenField ID="hdntasktype" Value='<%# Eval("Tasktype") %>' runat="server" />
                                    <asp:HiddenField ID="hdnproblemtype" Value='<%# Eval("Request") %>' runat="server" />
                                    <asp:HiddenField ID="hdnproblemtypename" Value='<%# Eval("RequestName") %>' runat="server" />
                                    <asp:HiddenField ID="hdntasktypename" Value='<%# Eval("TasktypeName") %>' runat="server" />
                                    <asp:HiddenField ID="hdnmaintaskestimatedeffort" Value='<%# Eval("MainTaskEstimatedeffort") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Main Task" HeaderStyle-Width="300px">
                                <ItemTemplate>
                                    <asp:Label ID="lbsubMainTask" Text='<%# Eval("Maintask") %>' runat="server"> 
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Subtask" HeaderStyle-Width="300px">
                                <ItemTemplate>
                                    <asp:Label ID="lbsubtask" Text='<%# Eval("Subtask") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Estimated EFfort" HeaderStyle-Width="200px">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtestimatedeffortsub" Text='<%# Eval("Estimatedeffortsub") %>' CssClass="form-control textsize2 datetimepicker estimated-effort" Style="position: relative"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Task Type" HeaderStyle-Width="200px">
                                <ItemTemplate>
                                    <asp:Label ID="lbtasktype" Text='<%#Eval("Tasktype") %>' runat="server">                    
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Request" HeaderStyle-Width="200px">
                                <ItemTemplate>
                                    <asp:Label ID="lbrequest" Text='<%#Eval("RequestName") %>' runat="server">                    
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Status" HeaderStyle-Width="200px">
                                <ItemTemplate>
                                    <asp:Label ID="lbstatus" Text='<%#Eval("SubtaskStatus") %>' runat="server">                    
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Dependencies">
                                            <ItemTemplate>
                                                 <asp:Label ID="lbdependencies" runat="server"  Text='<%#Eval("Dependencies") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>                                                
                                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Assigned To" HeaderStyle-Width="200px">
                                <ItemTemplate>
                                    <asp:Label ID="lbAssignedto" Text='<%#Eval("Assignedto") %>' runat="server">                    
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Delivery Date" HeaderStyle-Width="200px">
                                <ItemTemplate>
                                    <asp:Label ID="lbdeliverydate" Text='<%#Eval("DeliveryDate") %>' runat="server">                   
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="ManualEntry Remark" HeaderStyle-Width="200px">
                                <ItemTemplate>
                                    <asp:Label ID="lbmanualentryremark" Text='<%#Eval("ManualEntryRemark") %>' runat="server">         
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="lbEdit" CssClass="glyphicon glyphicon-pencil  EditlinkBtn" ToolTip="Edit" OnClick="lbEdit_Click"></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="lbDelete" CssClass="glyphicon glyphicon-trash  DeletelinkBtn" ToolTip="Delete" CommandName="Delete"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </div>--%>
                <asp:ListView runat="server" ID="lvSubtask" ItemPlaceholderID="placeHoldersubtask">
                <LayoutTemplate>
                    <table class="filterTable tbl grid" style="width: 100%; border: 1px solid">
                        <tr>
                            <asp:PlaceHolder runat="server" ID="placeHoldersubtask" />
                        </tr>
                    </table>
                </LayoutTemplate>
                    <ItemTemplate>
                        <tr runat="server"  visible='<%# Eval("HeaderVisibility") %>'>
                            <th>Week No</th>
                            <th>Employee</th>
                            <th>Project ID</th>
                            <th>TaskType</th>
                            <th>Maintask</th>
                            <th>Subtask</th>
                            <th>Manual Entry</th>
                            <th>Estimated Effort</th>                           
                            <%--<th>Request</th>--%>
                            <th>Remarks Engineer</th>
                            <th>View MOM</th>
                            <th>Subtask Status</th>
                            <th>Maintask Status</th>
                            <th>Project Status</th>
                            <%--<th>Status</th>
                            <th>Dependencies</th>
                            <th>Assigned To</th>
                            <th>Delivery Date</th>--%>
                            <th>Action</th>
                        </tr>
                        <tr>
                           <td><asp:Label ID="lbweekno" Text='<%# Eval("Weekno") %>' runat="server"/></td>
                            <td><asp:Label ID="lbemployee" Text='<%# Eval("Employees") %>' runat="server"/></td>
                            <td><asp:Label ID="lbsubProjectid" Text='<%# Eval("Projectid") %>' runat="server"/>
                               <asp:HiddenField ID="hdnweekno" runat="server" Value='<%#Eval("Weekno") %>' />
                               <asp:HiddenField ID="hdnyear" runat="server" Value='<%#Eval("Year") %>' />
                               <asp:HiddenField ID="hdnaddmaintaskidd" Value='<%# Eval("MainTaskIDD") %>' runat="server" />
                               <asp:HiddenField ID="hdnid" Value='<%# Eval("Id") %>' runat="server" />
                               <asp:HiddenField ID="hdnproblemtype" Value='<%# Eval("Request") %>' runat="server" />
                           </td> 
                            <td><asp:Label ID="lbtasktype" Text='<%#Eval("Tasktype") %>' runat="server"/> </td>   
                            <td><asp:Label ID="lbsubMainTask" Text='<%# Eval("Maintask") %>' runat="server"/></td> 
                            <td><asp:Label ID="lbsubtask" Text='<%# Eval("Subtask") %>' runat="server" /></td>
                          <td><asp:Label ID="lbmanualentryremark" Text='<%#Eval("ManualEntryRemark") %>' runat="server"/></td>
                            <td><asp:TextBox runat="server" ID="txtestimatedeffortsub" Text='<%# Eval("Estimatedeffortsub") %>' CssClass="form-control textsize2 datetimepicker estimated-effort" Style="position: relative" /></td>
                             <td><asp:Label ID="lbremarksengineer" Text='<%#Eval("RemarksEngineer") %>' runat="server"/></td>
                             <td><asp:Label ID="lbviewmom" Text='<%#Eval("ViewMOM") %>' runat="server"/></td>
                             <%--<td><asp:Label ID="lbrequest" Text='<%#Eval("RequestName") %>' runat="server"/></td>--%>
                             <td><asp:Label ID="lbstatus" Text='<%#Eval("SubtaskStatus") %>' runat="server"/></td>
                            <td><asp:Label ID="Label1" Text='<%#Eval("MaintaskStatus") %>' runat="server"/></td>
                            <td><asp:Label ID="Label2" Text='<%#Eval("ProjectStatus") %>' runat="server"/></td>
                             <%--<td><asp:Label ID="lbdependencies" runat="server"  Text='<%#Eval("Dependencies") %>'/></td>--%>
                            <%--<td><asp:Label ID="lbAssignedto" Text='<%#Eval("Assignedto") %>' runat="server"/></td>
                            <td><asp:Label ID="lbdeliverydate" Text='<%#Eval("DeliveryDate") %>' runat="server"/></td>--%>
                            
                            <td><asp:LinkButton runat="server" ID="lbEdit" CssClass="glyphicon glyphicon-pencil  EditlinkBtn" ToolTip="Edit" OnClick="lbEdit_Click"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="lbDelete" CssClass="glyphicon glyphicon-trash  DeletelinkBtn" ToolTip="Delete" CommandName="Delete"></asp:LinkButton></td>
                        <tr/>
                    </ItemTemplate>
               </asp:ListView>
                   </div>
                <div class="modal fade add-edit-modal" id="newsubtask" tabindex="-1">
                    <div class="modal-dialog" style="width: 1450px;">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Label class="modal-title" ID="taskdetails" runat="server">New Subtask</asp:Label>
                                <a data-dismiss="modal" class="glyphicon glyphicon-remove closeButton"></a>
                                <asp:HiddenField runat="server" ID="hfNewOrEdit" />
                            </div>
                            <div class="modal-body" style="overflow: auto;height:62vh">
                                <table class="modal-table5">
                                    <tr>
                                        <td>
                                            <span class="colorset">Number Of Rows To Add</span> </td>                                    
                                   <td><asp:TextBox runat="server" ID="txtrows" autogeneratedField="False" CssClass="form-control"  Style="width: 170px"></asp:TextBox></td>
                                  
                                   <td>  <span class="colorset">Week No:</span></td>
                                      <td><asp:DropDownList ID="ddladdweekno" runat="server" CssClass="form-control" Style="width: 170px"></asp:DropDownList>
                                   </td>
                                        
                                         <td>
                                         <span class="colorset">Year:</span></td>
                                        <td> <asp:Label runat="server" ID="lbyear"></asp:Label></td>
                                        <%--<asp:HiddenField runat="server" ID="hdnmaintaskidd" />
                                        <asp:HiddenField runat="server" ID="hdntaktype" />
                                        <asp:HiddenField runat="server" ID="hdnrequst" />--%>

                                    </tr>
                                </table>
                                <%--<table class="modal-table" style="width: 83%">
                                    <tr>
                                        <td>
                                            <span class="colorset">Project ID:</span>
                                            <asp:Label runat="server" ID="lbshowpopprojectid"></asp:Label>
                                        </td>
                                      
                                        <td>
                                            <span class="colorset">TaskType:</span>
                                            
                                            <asp:Label runat="server" ID="lbshowpoptasktype"></asp:Label></td>
                                        <td>
                                            <span class="colorset">Request:</span>
                                            
                                            <asp:Label ID="lbshowpoprequest" runat="server"></asp:Label>
                                       </td>
                                    </tr>
                                    <tr>
                                        
                                        <td colspan="3"> <span class="colorset">MainTask:</span>
                                             <asp:Label runat="server" ID="lbshowpopmaintask"></asp:Label>
                                        </td>
                                    </tr>
                                </table>--%>

                                <asp:HiddenField ID="hdnfield2" runat="server" />
                                <%--<div id="gridContainer1" class="gridContainer" runat="server" Style="width: 98%;display:inline-block">
                                    <asp:GridView ID="GVAdd" CssClass="grid" runat="server"
                                        AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" >

                                        <Columns>
                                            <asp:TemplateField HeaderText="Project ID">
                                                <ItemTemplate>--%>
                                <%--<asp:TextBox runat="server" DataTextField='<%# Eval("Projectid") %>'  CssClass="form-control textsize4" ID="txtstaddprojectid" list="dladdstprojectid">
                                                    </asp:TextBox><datalist id="dladdstprojectid" runat="server" clientidmode="static"></datalist>--%>
                                                    <%--<asp:DropDownList ID="ddladdprojectid" runat="server" CssClass="form-control textsize4" OnSelectedIndexChanged="ddladdprojectid_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList><asp:HiddenField runat="server" ID="hdnmaintaskidd" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Main Task">
                                                <ItemTemplate>
                                                    <asp:DropDownList runat="server" CssClass="form-control textsize4" ID="ddladdmaintask"  >
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Subtask">
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" AutoCompleteType="Disabled" ID="txtaddsubtask" CssClass="form-control textsize3"></asp:TextBox>
                                                    <%--<asp:HiddenField ID="hdnMaintaskestimatedaeffort" runat="server" Value='<%# Eval("Estimatedeffort") %>' />--%>
                                             <%--<asp:HiddenField ID="hdnid" runat="server" Value='<%# Eval("Id") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                           <%-- <asp:TemplateField HeaderText="Task Type" >
                                                <ItemTemplate>
                                                   <%-- <asp:Label ID="txtaddTaskType" Text='<%# Eval("Tasktype") %>' runat="server" />--%>
                                                   <%-- <asp:DropDownList ID="ddladdtasktype" runat="server" CssClass="form-control textsize4"></asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                            <%--<asp:TemplateField HeaderText="Request">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddladdrequest" runat="server" CssClass="form-control textsize4">--%>
                                                   <%-- </asp:DropDownList>
                                                </ItemTemplate>                                                
                                            </asp:TemplateField>--%>

                                            <%--<asp:TemplateField HeaderText="Manual Entry">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtmanualentry" Text='<%# Eval("ManualEntryRemark") %>' runat="server" CssClass="form-control textsize3"/>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                           <%-- <asp:TemplateField HeaderText="Delivery Date">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtdeliverydate" Text='<%# Eval("DeliveryDate") %>' runat="server" CssClass="form-control textsize3 datepicker" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                           
                                           <%-- <asp:TemplateField HeaderText="Estimated EFfort(HH:mm)">
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" ID="txtaddestimatedeffortsub" Text='<%# Eval("Estimatedeffortsub") %>' CssClass="form-control textsize4 datetimepicker" Style="position: relative" AutoCompleteType="Disabled"></asp:TextBox>

                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                             <%--<asp:TemplateField HeaderText="Dependencies">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddladddependencies" runat="server" CssClass="form-control textsize4">
                                                    </asp:DropDownList>
                                                </ItemTemplate>                                                
                                            </asp:TemplateField>--%>

                                            <%--<asp:TemplateField HeaderText="Assigned To">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddladdassignedto" runat="server" CssClass="form-control textsize4">
                                                    </asp:DropDownList>
                                                </ItemTemplate>                                                
                                            </asp:TemplateField>--%>

                                             <%--<asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddladdstatus" runat="server" CssClass="form-control textsize4">
                                                    </asp:DropDownList>
                                                </ItemTemplate>                                                
                                            </asp:TemplateField>--%>

                                       <%-- </Columns>
                                    </asp:GridView>--%>
                                <div id="gridContainer1" class="gridContainer" runat="server" Style="width: 98%;display:inline-block">
                                <asp:ListView runat="server" ID="lvAddSubtask" ItemPlaceholderID="placeHolderaddsubtask">
                <LayoutTemplate>
                    <table class="filterTable tbl grid" style="width: 97%; border: 1px solid">
                        <tr>
                            <asp:PlaceHolder runat="server" ID="placeHolderaddsubtask" />
                        </tr>
                    </table>
              </LayoutTemplate>
               <ItemTemplate>
                 <tr runat="server"  visible='<%# Eval("HeaderVisibility") %>'>
                    <th>Project ID</th>
                    <th>Maintask</th>
                    <th>Subtask</th>
                    <th>Estimated Effort</th>
                    <th>TaskType</th>
                    <th>Request</th>
                    <th>Status</th>
                    <th>Dependencies</th>
                    <th>Assigned To</th>
                    <th>Delivery Date</th>
                    <th>Manual Entry</th>
                    
                 </tr>
             <tr>
               <td><asp:DropDownList ID="ddladdprojectid" runat="server" CssClass="form-control textsize4" OnSelectedIndexChanged="ddladdprojectid_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td> 
               <td><asp:DropDownList runat="server" CssClass="form-control textsize4" ID="ddladdmaintask"/></td> 
               <td><asp:TextBox runat="server" AutoCompleteType="Disabled" ID="txtaddsubtask" CssClass="form-control textsize3" /></td>
               <td><asp:TextBox runat="server" ID="txtaddestimatedeffortsub" Text='<%# Eval("Estimatedeffortsub") %>' CssClass="form-control textsize2 datetimepicker estimated-effort" Style="position: relative" /></td>
              <td><asp:DropDownList ID="ddladdtasktype" runat="server" CssClass="form-control textsize4"/></td>   
              <td><asp:DropDownList ID="ddladdrequest" runat="server" CssClass="form-control textsize4"/></td>
              <td><asp:DropDownList ID="ddladdassignedto" runat="server" CssClass="form-control textsize4"/></td>
              <td><asp:DropDownList ID="ddladdstatus" runat="server" CssClass="form-control textsize4"/></td>
              <td><asp:DropDownList ID="ddladddependencies" runat="server" CssClass="form-control textsize4"/></td>
              <td><asp:TextBox ID="txtdeliverydate"  runat="server" CssClass="form-control textsize3 datepicker" /></td>
              <td><asp:TextBox ID="txtmanualentry" runat="server" CssClass="form-control textsize3"/></td>
              
           <tr/>
       </ItemTemplate>
                            </asp:ListView>

                                </div>
                                <div style="padding:2px;width:1%;display:inline-block"><asp:LinkButton runat="server" ToolTip="Add New Row" CssClass="glyphicon glyphicon-plus-sign" ID="lbAddnewrow" OnClick="lbAddnewrow_Click" Style="font-size:25px"></asp:LinkButton></div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button runat="server" ID="BtnSave" CssClass="btn AddEditBtn" Text="Save" OnClick="btnSave_Click" OnClientClick="return subtaskvalidation();" />
                                <asp:Button runat="server" Text="Cancel" ID="btnCancel" CssClass="btn  btn-danger CancelBtn" OnClientClick="return clearscreen();"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal fade add-edit-modal" id="Editsubtask" tabindex="-1">
                    <div class="modal-dialog" style="width: 815px">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Label CssClass="modal-title" ID="SubTaskEditDetails" runat="server">Edit SubTask Details</asp:Label>
                                <a data-dismiss="modal" class="glyphicon glyphicon-remove closeButton"></a>
                            </div>
                            <div class="modal-body" style="overflow: unset">
                                <table class="modal-table1">
                                    <tr>
                                       <%-- <td>
                                            <asp:Label ID="lbedityear" Text="Year" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtedityear" runat="server" CssClass="form-control textsize1" AutoCompleteType="Disabled" ReadOnly="true" ></asp:TextBox></td>
                                        <td>
                                            <asp:Label ID="lbeditweek" Text="Week No" runat="server"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="txteditweekno" runat="server" CssClass="form-control textsize1" AutoCompleteType="Disabled" ReadOnly="true" ></asp:TextBox>
                                            <asp:HiddenField runat="server" ID="hdneditid" />
                                            <asp:HiddenField runat="server" ID="hdneditmaintaskidd" />
                                            <asp:HiddenField runat="server" ID="hdnedittasktype" />
                                            <asp:HiddenField runat="server" ID="hdneditmaintaskestimatedeffort" />
                                            <asp:HiddenField runat="server" ID="hdneditmaintask" />
                                            <asp:HiddenField runat="server" ID="hdneditrequest" />
                                        </td>--%>
                                        <td><span>Project ID</span></td>
                                        <td><asp:DropDownList runat="server" ID="ddleditprojectid" CssClass="form-control textsize2"></asp:DropDownList>
                                            <asp:HiddenField runat="server" ID="hdneditid" />
                                            <asp:HiddenField runat="server" ID="hdneditmaintaskidd" />
                                            <asp:HiddenField runat="server" ID="hdnedittasktype" />
                                            <asp:HiddenField runat="server" ID="hdneditmaintaskestimatedeffort" />
                                            <asp:HiddenField runat="server" ID="hdneditmaintask" />
                                            <asp:HiddenField runat="server" ID="hdneditrequest" />
                                            <asp:HiddenField runat="server" ID="hdnweekno" />
                                            <asp:HiddenField runat="server" ID="hdnyear" />
                                        </td>
                                        <td><span>MainTask</span></td>
                                        <td><asp:DropDownList runat="server" ID="ddleditmaintask" CssClass="form-control textsize2"></asp:DropDownList></td>
                                        
                                    </tr>
                                    <tr>
                                        <td><span>Subtask</span></td>
                                        <td><asp:TextBox ID="txteditsubtask" runat="server" CssClass="form-control textsize2" AutoCompleteType="Disabled"></asp:TextBox></td>
                                        <td><span>Status</span></td>
                                        <td><asp:DropDownList runat="server" ID="ddleditstatus" CssClass="form-control textsize2"></asp:DropDownList></td>
                                        
                                        </tr>
                                    <tr>
                                        <td><span>Request</span></td>
                                        <td><asp:DropDownList runat="server" ID="ddleditrequest" CssClass="form-control textsize2"></asp:DropDownList></td>
                                        
                                        
                                        <td><span>Task Type</span></td>
                                        <td><asp:DropDownList runat="server" ID="ddledittasktype" CssClass="form-control textsize2"></asp:DropDownList></td>
                                        </tr>
                                    <tr>
                                        <td><span>Estimated Effort(HH:mm)</span></td>
                                        <td>  <asp:TextBox ID="txteditesimatedeffort" runat="server" CssClass="form-control textsize2 datetimepicker" Style="position: relative"></asp:TextBox></td>
                                        <td><span>Assigned To</span></td>
                                        <td><asp:DropDownList ID="ddleditassignedto" runat="server" CssClass="form-control textsize2">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td><span>Delivery Date</span></td>
                                        <td><asp:TextBox runat="server" ID="txtedideliverydate"  CssClass="form-control textsize2 datepicker"></asp:TextBox></td>
                                        <td><span>ManualEntry Remark</span></td>
                                        <td><asp:TextBox runat="server" ID="txteditmanualentry" CssClass="form-control textsize2"></asp:TextBox></td>
                                   </tr>
                                      
                                </table>
                            </div>
                            <div class="modal-footer">
                                <asp:Button runat="server" ID="BtnUpdate" CssClass="btn AddEditBtn" Text="Update" OnClick="btnUpdate_Click" />
                                <asp:Button runat="server" Text="Cancel" ID="BtnEditCancel" CssClass="btn  btn-danger CancelBtn" OnClientClick="return clearscreen();"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>

    <div class="modal fade add-edit-modal" id="myConfirmationModal" role="dialog" style="min-width: 300px;">
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
    <script>
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {

            $(document).ready(function () {
                ControlSetter();              
            });
            $(".datetimepicker").datetimepicker({
                format: 'HH:mm',
                locale: 'en-US'
            });
            $(".estimated-effort").keydown(function (event) {
                //alert(event.key);
                if (event.key === "Enter") {
                    let rowIndex = ($(this).closest('tr').prevAll().length) - 1;
                    /*alert(rowIndex);*/
                    //$("#hdnrowindex").val(rowIndex);
                    $('[id$=hdnrowindex]').val(rowIndex);
                    //alert($('[id$=hdnrowindex]').val());
                    //document.getElementById("hdnrowindex").value = rowIndex;
                    __doPostBack('<%= btnhdsave.UniqueID%>', '');
                }
            });
        });
        function subtaskvalidation() {
            var grid = document.getElementById("<%=lvAddSubtask.ClientID%>");
            for (var i = 1; i < $("#grid   tr").length; i++) {
                let tr = $("#grid tr")[i];

                if ($(tr).find("#txtaddsubtask").val() != "") 
                {
                    if ($(tr).find("#txtaddestimatedeffortsub").val() == "") {
                        WarningToastr("EstimatedEffort Is Required at row" + i);
                        $("[id$=txtaddestimatedeffortsub]")[i - 1].focus();
                        return false;
                    }
                    if ($(tr).find("#ddladdassignedto").val() == "") {
                        WarningToastr("Assignedto Is Required at row" +i);
                        $("[id$=ddladdassignedto]")[i - 1].focus();
                        return false;
                    }
                    
                }

            }
            return true;
        }
        $(document).ready(function () {
            ControlSetter();
            
        });
        function openConfirmModal(msg) {
            $('[id*=myConfirmationModal]').modal('show');
            $("#confirmationmessageText").text(msg);
        }
        function saveConfirmNo() {
            $('[id*=myConfirmationModal]').modal('hide');
        }
        function ControlSetter() {
            $('.multiDropdown').multiselect({
                includeSelectAllOption: true
            });
            
            $('[id$=txtyear]').datepicker({
                minViewMode: 2,
                format: 'yyyy',
                todayHighlight: true,
                autoclose: true,
                language: 'en-US',
            });
            $(".datetimepicker").datetimepicker({
                format: 'HH:mm',
                locale: 'en-US'
                // format: 'LT'
            });
            $('.datepicker').datepicker({
                viewMode: "date",
                minViewMode: "date",
                format: 'dd-mm-yyyy',
                todayHighlight: true,
                autoclose: true,
                language: 'en-US',
            });
        }
        $(".estimated-effort").keydown(function (event) {
            //alert(event.key);
            if (event.key === "Enter") {
                let rowIndex = ($(this).closest('tr').prevAll().length) - 1;
                /*alert(rowIndex);*/
                //$("#hdnrowindex").val(rowIndex);
                $('[id$=hdnrowindex]').val(rowIndex);
                //alert($('[id$=hdnrowindex]').val());
                //document.getElementById("hdnrowindex").value = rowIndex;
                __doPostBack('<%= btnhdsave.UniqueID%>', '');
            }
        });
        document.getElementById("headerName").textContent = "Subtask";
    </script>
</asp:Content>
