<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Subtasknew.aspx.cs" EnableEventValidation="false" Inherits="ADES_22.Subtasknew" %>

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
            width: 170px;
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
    </style>
    
    <asp:UpdatePanel runat="server"> 
        <ContentTemplate>
    <div class="container-fluid" style="margin-top: 0px; margin-left: 0px;padding-left:10px; width: 100%">
        <div id="main">
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
                            <asp:Button runat="server" ID="btndashboardview" Text="View" CssClass="Buttons" Visible="false" OnClick="btndashboardview_Click" />
                            <asp:Button runat="server" ID="btnhdsave" Text="text" Visible="false" ClientIDMode="Static" OnClick="btnhdSave_Click" />
                            <asp:Button runat="server" ID="btnAdd" Text="Add" CssClass="Buttons" OnClick="btnAdd_Click" />
                            <asp:HiddenField ID="hdnrowindex" runat="server" />
                        </td>

                        <%--<td>
                            <asp:LinkButton runat="server" ID="lbPrevweek" ToolTip="Previous Week" CssClass="glyphicon glyphicon-chevron-left"  Style="font-size: 20px"></asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lbNextweek" ToolTip="Next Week" CssClass="glyphicon glyphicon-chevron-right"  Style="font-size: 20px"></asp:LinkButton>
                        </td> --%>
                        <%--<td>
                            <asp:RadioButtonList ID="rblMeasurementSystem" runat="server" OnSelectedIndexChanged="rblMeasurementSystem_SelectedIndexChanged" ClientIDMode="Static">
                                
                                   <asp:ListItem Text="DashboaredView" Value="DashboaredView" runat="server" />
                                <asp:ListItem Text="PlannerView" Value="PlannerView" runat="server" />
                                </asp:RadioButtonList>
                        </td>--%>

                        <td style="padding: 2px">

                            <asp:LinkButton runat="server" ID="lbtnDashboardview" ToolTip="Dashboard View" CssClass="glyphicon glyphicon-modal-window" Style="font-size: 25px" OnClick="lbtnDashboardview_Click"></asp:LinkButton></td>
                        <td>
                            <asp:LinkButton runat="server" ID="lbtnplanedview" ToolTip="Planned View" CssClass="glyphicon glyphicon-list" Style="font-size: 25px" OnClick="lbtnplanedview_Click"></asp:LinkButton>

                        </td>
                    </tr>
                </table>
            </div>
        </div>
       </div>
        <div id="div1" runat="server" class="col" style="display: none;">
        <div style="position: relative;padding-left:10px; ">
                <asp:ListView runat="server" ID="lvSubtask" ClientIDMode="Static">
                    <LayoutTemplate>
                        <table id="dd" class="filterTable tbl grid" style="width: 96%;border: 1px solid">
                            <tr runat="server" visible='<%# Eval("HeaderVisibility") %>'>
                                <th>Week No</th>
                                <th>Employee</th>
                                <th>Project ID</th>
                                <th>TaskType</th>
                                <th>Maintask</th>
                                <th>Subtask</th>
                                <th>Manual Entry</th>
                                <th>Estimated Effort(HH)</th>
                                <th>Remarks Engineer</th>
                                <th>View MOM</th>
                                <th>Subtask Status</th>
                                <th>Maintask Status</th>
                                <th>Project Status</th>
                                <th>Action</th>
                            </tr>
                            <tr id="itemplaceholder" runat="server"></tr>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Label ID="lbweekno" Text='<%# Eval("Weekno") %>' runat="server" /></td>
                            <td>
                                <asp:Label ID="lbassignedto" Text='<%# Eval("Employees") %>' runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="lbsubProjectid" Text='<%# Eval("Projectid") %>' runat="server" />
                                <asp:HiddenField ID="hdnweekno" runat="server" Value='<%#Eval("Weekno") %>' />
                                <asp:HiddenField ID="hdnyear" runat="server" Value='<%#Eval("Year") %>' />
                                <asp:HiddenField ID="hdnaddmaintaskidd" Value='<%# Eval("MainTaskIDD") %>' runat="server" />
                                <asp:HiddenField ID="hdnid" Value='<%# Eval("Id") %>' runat="server" />
                                <asp:HiddenField ID="hdnproblemtype" Value='<%# Eval("Request") %>' runat="server" />
                                <asp:HiddenField ID="hdnassignedto" Value='<%# Eval("AssignedTo") %>' runat="server" />
                                <asp:HiddenField ID="hdndeliverydate" Value='<%# Eval("DeliveryDate") %>' runat="server" />
                                <asp:HiddenField ID="hdnproblemid" Value='<%# Eval("ProblemID") %>' runat="server" />
                            </td>
                            <td>
                                <asp:Label ID="lbtasktype" Text='<%#Eval("Tasktype") %>' runat="server" />
                            </td>
                            <td>
                                <asp:Label ID="lbsubMainTask" Text='<%# Eval("Maintask") %>' runat="server" /></td>
                            <td>
                                <asp:Label ID="lbsubtask" Text='<%# Eval("Subtask") %>' runat="server" /></td>
                            <td>
                                <asp:Label ID="lbmanualentryremark" Text='<%#Eval("ManualEntryRemark") %>' runat="server" /></td>
                            <td>
                                <asp:TextBox runat="server" ID="txtestimatedeffortsub" Text='<%# Eval("Estimatedeffortsub") %>' CssClass="form-control textsize2 datetimepicker estimated-effort" Style="position: relative" MaxLength="2" /></td>
                            <td>
                                <asp:Label ID="lbremarksengineer" Text='<%#Eval("RemarksEngineer") %>' runat="server" /></td>
                            <td>
                                <asp:Label ID="lbviewmom" Text='<%#Eval("ViewMOM") %>' runat="server" /></td>
                            <td>
                                <asp:Label ID="lbstatus" Text='<%#Eval("SubtaskStatus") %>' runat="server" />
                                <asp:LinkButton runat="server" ID="lbtnsubtaskstatus" CssClass="glyphicon glyphicon-edit" Style="font-size: 15px" OnClick="lbtnsubtaskstatus_Click" Visible='<%# Eval("Visibility") %>'></asp:LinkButton></td>
                            <td>
                                <asp:Label ID="Label1" Text='<%#Eval("MaintaskStatus") %>' runat="server" />
                                <asp:LinkButton runat="server" ID="lbtnmaintaskstatus" CssClass="glyphicon glyphicon-edit" Style="font-size: 15px" OnClick="lbtnmaintaskstatus_Click" Visible='<%# Eval("mainVisibility") %>'></asp:LinkButton></td>
                            <td>
                                <asp:Label ID="Label2" Text='<%#Eval("ProjectStatus") %>' runat="server" />
                                <asp:LinkButton runat="server" ID="lbtnprojectstatus" CssClass="glyphicon glyphicon-edit" Style="font-size: 15px" OnClick="lbtnprojectstatus_Click" Visible='<%# Eval("projectVisibility") %>'></asp:LinkButton></td>

                            <%--<td><asp:Label ID="lbdependencies" runat="server"  Text='<%#Eval("Dependencies") %>'/></td>--%>
                            <%--<td><asp:Label ID="lbdependencies" runat="server"  Text='<%#Eval("Dependencies") %>'/></td>--%>
                            <%--<td><asp:Label ID="lbAssignedto" Text='<%#Eval("Assignedto") %>' runat="server"/></td>
                            <td><asp:Label ID="lbdeliverydate" Text='<%#Eval("DeliveryDate") %>' runat="server"/></td>--%>

                            <td>
                                <asp:LinkButton runat="server" ID="lbEdit" CssClass="glyphicon glyphicon-pencil  EditlinkBtn" ToolTip="Edit" OnClick="lbEdit_Click"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="lbDelete" CssClass="glyphicon glyphicon-trash  DeletelinkBtn" ToolTip="Delete" OnClick="lbDelete_Click"></asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
            </div>
       </div>
        <div style="position: relative;padding-left:10px; ">
            <asp:ListView runat="server" ID="lvdashboardview" ItemPlaceholderID="placeHoldersubtask">
                <LayoutTemplate>
                    <table class="filterTable tbl grid" style="width: 96%; border: 1px solid">
                        <tr runat="server" visible='<%# Eval("HeaderVisibility") %>'>
                            <th>Week No</th>

                            <th>Employee</th>
                            <th>Project ID</th>
                            <th>TaskType</th>
                            <th>Maintask</th>
                            <th>Subtask</th>
                            <th>Manual Entry</th>
                            <th>Estimated Effort(HH)</th>

                            <th>Remarks Engineer</th>
                            <th>View MOM</th>
                            <th>Subtask Status</th>
                            <th>Maintask Status</th>
                            <th>Project Status</th>
                        </tr>
                        <tr>
                            <asp:PlaceHolder runat="server" ID="placeHoldersubtask" />
                        </tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>

                    <tr>
                        <td>
                            <asp:Label ID="lbweekno" Text='<%# Eval("Weekno") %>' runat="server" /></td>
                        <td>
                            <asp:Label ID="lbemployee" Text='<%# Eval("Employees") %>' runat="server" /></td>
                        <td>
                            <asp:Label ID="lbsubProjectid" Text='<%# Eval("Projectid") %>' runat="server" />
                            <asp:HiddenField ID="hdnweekno" runat="server" Value='<%#Eval("Weekno") %>' />
                            <asp:HiddenField ID="hdnyear" runat="server" Value='<%#Eval("Year") %>' />
                            <asp:HiddenField ID="hdnaddmaintaskidd" Value='<%# Eval("MainTaskIDD") %>' runat="server" />
                            <asp:HiddenField ID="hdnid" Value='<%# Eval("Id") %>' runat="server" />
                            <asp:HiddenField ID="hdnproblemtype" Value='<%# Eval("Request") %>' runat="server" />
                        </td>
                        <td>
                            <asp:Label ID="lbtasktype" Text='<%#Eval("Tasktype") %>' runat="server" />
                        </td>
                        <td>
                            <asp:Label ID="lbsubMainTask" Text='<%# Eval("Maintask") %>' runat="server" /></td>
                        <td>
                            <asp:Label ID="lbsubtask" Text='<%# Eval("Subtask") %>' runat="server" /></td>
                        <td>
                            <asp:Label ID="lbmanualentryremark" Text='<%#Eval("ManualEntryRemark") %>' runat="server" /></td>
                        <td>
                            <asp:Label ID="lbestimatedeffort" Text='<%#Eval("Estimatedeffortsub") %>' runat="server" /></td>
                        <td>
                            <asp:Label ID="lbremarksengineer" Text='<%#Eval("RemarksEngineer") %>' runat="server" /></td>
                        <td>
                            <asp:Label ID="lbviewmom" Text='<%#Eval("ViewMOM") %>' runat="server" /></td>

                        <td>
                            <asp:Label ID="lbstatus" Text='<%#Eval("SubtaskStatus") %>' runat="server" /></td>
                        <td>
                            <asp:Label ID="Label1" Text='<%#Eval("MaintaskStatus") %>' runat="server" /></td>
                        <td>
                            <asp:Label ID="Label2" Text='<%#Eval("ProjectStatus") %>' runat="server" /></td>


                    </tr>
                </ItemTemplate>
            </asp:ListView>
        </div>
    
    </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnview" />
            <asp:PostBackTrigger ControlID="btnAdd" />
        </Triggers>
    </asp:UpdatePanel>
        
        </div>
    
    <div class="modal  add-edit-modal" id="newsubtask" tabindex="-1">
        <div class="modal-dialog" style="width: 1300px;">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:Label class="modal-title" ID="taskdetails" runat="server">New Subtask</asp:Label>
                    <a data-dismiss="modal" class="glyphicon glyphicon-remove closeButton"></a>
                    <asp:HiddenField runat="server" ID="hfNewOrEdit" />
                </div>
                <div class="modal-body" style="height:73vh" >
                    <table class="modal-table5">
                        <tr>
                            <td>
                                <span class="colorset">Number Of Rows To Add</span> </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtrows" autogeneratedField="False" CssClass="form-control" Style="width: 170px"></asp:TextBox></td>

                            <td colspan="4">
                                <span class="colorset">Year:</span>

                                <asp:Label runat="server" ID="lbyear"></asp:Label></td>


                            <td><span class="colorset">Week No:</span></td>
                            <td>
                                <asp:DropDownList ID="ddladdweekno" runat="server" CssClass="form-control" Style="width: 100px"></asp:DropDownList>
                            </td>



                        </tr>
                    </table>


                    <asp:HiddenField ID="hdnfield2" runat="server" />

                    <div id="gridContainer1" class="gridContainer" runat="server" style="width: 98%; display: inline-block; overflow: auto;height: 60vh;">
    
                        <asp:ListView runat="server" ID="lvAddSubtask" ItemPlaceholderID="placeHolderaddsubtask">
                            <LayoutTemplate>
                                <table class="filterTable tbl grid" style="width: 97%; border: 1px solid">
                                    <tr>
                                        <asp:PlaceHolder runat="server" ID="placeHolderaddsubtask" />
                                    </tr>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr runat="server" visible='<%# Eval("HeaderVisibility") %>'>
                                    
                                    <th>Project ID</th>
                                    <th>Maintask</th>
                                    <th>TaskType</th>
                                    <th>Assigned To</th>
                                    <th>Subtask</th>
                                    <th>Manual Entry</th>
                                    <th>Estimated Effort(HH)</th>
                                    <th>Request</th>
                                    <th>Dependencies</th>
                                    <th>Status</th>
                                    <th>Delivery Date</th>


                                </tr>
                                <tr>

                                    
                                    <td>
                                        <asp:DropDownList ID="ddladdprojectid" runat="server" CssClass="form-control textsize4" AutoPostBack="true" OnSelectedIndexChanged="ddladdprojectid_SelectedIndexChanged"></asp:DropDownList></td>
                                    <td>
                                        <asp:DropDownList runat="server" CssClass="form-control textsize4" ID="ddladdmaintask" /></td>
                                    <td>
                                        <asp:DropDownList ID="ddladdtasktype" runat="server" CssClass="form-control textsize4" /></td>
                                    <td>
                                        <asp:DropDownList ID="ddladdassignedto" runat="server" CssClass="form-control textsize4" /></td>
                                    <td>
                                        <asp:TextBox runat="server" AutoCompleteType="Disabled" ID="txtaddsubtask" CssClass="form-control textsize3" TextMode="MultiLine" /></td>
                                    <td>
                                        <asp:TextBox ID="txtmanualentry" runat="server" CssClass="form-control textsize3" TextMode="MultiLine" AutoCompleteType="Disabled" /></td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtaddestimatedeffortsub" Text='<%# Eval("Estimatedeffortsub") %>' CssClass="form-control textsize2 datetimepicker estimated-effort" Style="position: relative;overflow:unset" AutoCompleteType="Disabled" MaxLength="2" /></td>

                                    <td>
                                        <asp:DropDownList ID="ddladdrequest" runat="server" CssClass="form-control textsize4" /></td>


                                    <td>
                                        <asp:DropDownList ID="ddladddependencies" runat="server" CssClass="form-control textsize4" /></td>
                                    <td>
                                        <asp:DropDownList ID="ddladdstatus" runat="server" CssClass="form-control textsize4" /></td>
                                    <td>
                                        <asp:TextBox ID="txtdeliverydate" runat="server" CssClass="form-control textsize3 datepicker" AutoCompleteType="Disabled" /></td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>

                    </div>
                    <div style="padding: 2px; width: 1%; display: inline-block">
                        <asp:LinkButton runat="server" ToolTip="Add New Row" CssClass="glyphicon glyphicon-plus-sign" ID="lbAddnewrow" OnClick="lbAddnewrow_Click" Style="font-size: 25px"></asp:LinkButton>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" ID="BtnSave" CssClass="btn AddEditBtn" Text="Save" OnClick="btnSave_Click" OnClientClick="return subtaskvalidation();" />
                    <asp:Button runat="server" Text="Cancel" ID="btnCancel" CssClass="btn  btn-danger CancelBtn" OnClientClick="return clearscreen();"></asp:Button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal  add-edit-modal" id="Editsubtask" tabindex="-1">
        <div class="modal-dialog" style="width: 775px">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:Label CssClass="modal-title" ID="SubTaskEditDetails" runat="server">Edit SubTask Details</asp:Label>
                    <a data-dismiss="modal" class="glyphicon glyphicon-remove closeButton"></a>
                </div>
                <div class="modal-body" style="overflow: unset">
                    <table class="modal-table1">
                        <tr>

                            <td><span>Project ID</span></td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddleditprojectid" CssClass="form-control textsize3"></asp:DropDownList>
                                <asp:HiddenField runat="server" ID="hdneditid" />
                                <asp:HiddenField runat="server" ID="hdneditmaintaskidd" />
                                <asp:HiddenField runat="server" ID="hdnedittasktype" />
                                <asp:HiddenField runat="server" ID="hdneditmaintaskestimatedeffort" />
                                <asp:HiddenField runat="server" ID="hdneditmaintask" />
                                <asp:HiddenField runat="server" ID="hdneditrequest" />
                                <asp:HiddenField runat="server" ID="hdnweekno" />
                                <asp:HiddenField runat="server" ID="hdnyear" />
                                <asp:HiddenField runat="server" ID="hdnsubtaskstatus" />
                                <asp:HiddenField runat="server" ID="hdnmaintaskstatus" />
                                <asp:HiddenField runat="server" ID="hdnprojectstatus" />
                                <asp:HiddenField runat="server" ID="dhneditdeliverydate" />
                                <asp:HiddenField runat="server" ID="hdneditproblemid" />
                            </td>
                            <td><span>MainTask</span></td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddleditmaintask" CssClass="form-control textsize3"></asp:DropDownList></td>

                        </tr>
                        <tr>
                            <td><span>Subtask</span></td>
                            <td>
                                <asp:TextBox ID="txteditsubtask" runat="server" CssClass="form-control textsize3" AutoCompleteType="Disabled"></asp:TextBox></td>
                            <td><span>Request</span></td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddleditrequest" CssClass="form-control textsize3"></asp:DropDownList></td>

                        </tr>
                        <tr>



                            <td><span>Task Type</span></td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddledittasktype" CssClass="form-control textsize3"></asp:DropDownList></td>
                            <td><span>Assigned To</span></td>
                            <td>
                                <asp:DropDownList ID="ddleditassignedto" runat="server" CssClass="form-control textsize3">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td><span>Estimated Effort(HH)</span></td>
                            <td>
                                <asp:TextBox ID="txteditesimatedeffort" runat="server" CssClass="form-control textsize3 datetimepicker" Style="position: relative" AutoCompleteType="Disabled"></asp:TextBox></td>
                            <td><span>Delivery Date</span></td>
                            <td>
                                <asp:TextBox runat="server" ID="txtedideliverydate" CssClass="form-control textsize3 datepicker" AutoCompleteType="Disabled"></asp:TextBox></td>
                        </tr>
                        <tr>

                            <td><span>ManualEntry Remark</span></td>
                            <td>
                                <asp:TextBox runat="server" ID="txteditmanualentry" CssClass="form-control textsize3"></asp:TextBox></td>
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

    <div class="modal  add-edit-modal" id="editsubtaskstatus" tabindex="-1">
        <div class="modal-dialog" style="width: 400px">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:Label CssClass="modal-title" ID="Label3" runat="server">Edit SubTask Status</asp:Label>
                    <a data-dismiss="modal" class="glyphicon glyphicon-remove closeButton"></a>
                </div>
                <div class="modal-body" style="overflow: unset">
                    <table class="modal-table1">
                        <tr>
                            <td><span>Subtask Status</span></td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddleditsubtaskstatus" CssClass="form-control textsize3"></asp:DropDownList>
                                <asp:HiddenField runat="server" ID="hdneditsubtaskstatusid" />
                                <asp:HiddenField runat="server" ID="hdnstatuschangeprojectid" />
                                <asp:HiddenField runat="server" ID="hdnstatuschangeweekno" />
                                <asp:HiddenField runat="server" ID="hdnstatuschangeyearno" />
                                <asp:HiddenField runat="server" ID="hdnstatuschangetasktype" />
                                <asp:HiddenField runat="server" ID="hdnstatuschangemaintask" />
                                <asp:HiddenField runat="server" ID="hdnstatuschangesubtask" />
                                <asp:HiddenField runat="server" ID="hdnstatuschangeestimatedeffort" />
                                <asp:HiddenField runat="server" ID="hdnstatuschangeProblemtype" />
                                <asp:HiddenField runat="server" ID="hdnstatuschangeProblemid" />
                                <asp:HiddenField runat="server" ID="hdnstatuschangeassignedto" />
                                <asp:HiddenField runat="server" ID="hdnstatuschangemaintaskidd" />
                                <asp:HiddenField runat="server" ID="hdnstatuschangedeliverydate" />
                                <asp:HiddenField runat="server" ID="hdnstatuschangemanualentry" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btneditsubtaskstatus" runat="server" Text="Save" CssClass="btn AddEditBtn"
                        OnClick="btneditsubtaskstatus_Click" />
                </div>
            </div>
        </div>
    </div>

    <div class="modal  add-edit-modal" id="editmaintaskstatus" tabindex="-1">
        <div class="modal-dialog" style="width: 400px">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:Label CssClass="modal-title" ID="Label4" runat="server">Edit Maintask Status</asp:Label>
                    <a data-dismiss="modal" class="glyphicon glyphicon-remove closeButton"></a>
                </div>
                <div class="modal-body" style="overflow: unset">
                    <table class="modal-table1">
                        <tr>
                            <td><span>MainTask Status</span></td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlmaintaskvalue" CssClass="form-control textsize3"></asp:DropDownList>
                                <asp:HiddenField runat="server" ID="hdnmaintaskid" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnmaintaskstatus" runat="server" Text="Save" CssClass="btn AddEditBtn"
                        OnClick="btnmaintaskstatus_Click" />
                </div>
            </div>
        </div>
    </div>

    <div class="modal  add-edit-modal" id="editdispatchstatus" tabindex="-1">
        <div class="modal-dialog" style="width: 400px">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:Label CssClass="modal-title" ID="lbdispatchtitle" runat="server">Edit Dispatch Status</asp:Label>
                    <a data-dismiss="modal" class="glyphicon glyphicon-remove closeButton"></a>
                </div>
                <div class="modal-body" style="overflow: unset">
                    <table class="modal-table1">
                        <tr>
                            <td><span>Project Status</span></td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddldispatchstatus" CssClass="form-control textsize3"></asp:DropDownList>
                                <asp:HiddenField runat="server" ID="hdndispatchstatus" />
                                <asp:HiddenField runat="server" ID="hdndispatchprojectid" />

                            </td>
                        </tr>
                    </table>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnsavedispatch" runat="server" Text="Save" CssClass="btn AddEditBtn"
                        OnClick="btnsavedispatch_Click" />
                </div>
            </div>
        </div>
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
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            $(document).ready(function () {
                ControlSetter();
            });
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
                maxHeight: 600,
                buttonWidth:'170px',
                enableCaseInsensitiveFiltering: true
            });
            $(".datetimepicker").datetimepicker({
                format: 'HH',
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
            if (event.key === "Enter") {
                let rowIndex = ($(this).closest('tr').prevAll().length) - 1;
                $('[id$=hdnrowindex]').val(rowIndex);
                __doPostBack('<%= btnhdsave.UniqueID%>', '');
            }
        });
        document.getElementById("headerName").textContent = "Subtask";
    </script>
</asp:Content>
