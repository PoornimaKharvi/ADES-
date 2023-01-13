 <%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="STaskTransactionDetails.aspx.cs" Inherits="ADES_22.STaskTransactionDetails" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
            width: 190px;
        }
        .textsize2
        {
            width:185px;
        }
        .textsize4{
            width:100px;
        }
        .textbox5{
            width:60px;
        }
        .tableBorder {
            border: 2px solid transparent;
        }

        .day-text-box {
            min-width: 53px;
            margin-right: 0px;
            display: inline-block;
            padding:3px 2px;
        }
        .grid tr td:nth-child(3) {
            position: sticky;
            left: 0px;
            z-index: 1;
            background-color: white;
        }
         .grid tr th:nth-child(3){ 
             position: sticky;
            left: 0px;
            z-index: 3;
         }
         .filehidden{
             width: 80px;
             text-overflow: ellipsis;
             overflow: hidden;
             white-space: nowrap;
             display: block;
         }
    </style>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="container-fluid" style="margin-top: 0px; margin-left: 0px; width: 96%">
                <div id="main">
                    <div id="import" runat="server" class="importdiv">
                        <table id="tbl1" class="filterTable tbl">
                            <asp:HiddenField ID="hdnSubtasktrans" runat="server" />
                            <tr>
                                 <td><asp:Label runat="server" Text="Employee" ID="lbemployee" Visible="false"></asp:Label></td>
                                <td><asp:DropDownList runat="server" ID="ddlemployee" Visible="false" CssClass= "form-control" style="width:185px"></asp:DropDownList></td>

                                <td>
                                    <asp:Label runat="server" Text="Year"></asp:Label></td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtyear" AutoPostBack="true" CssClass="form-control textsize1" AutoCompleteType="Disabled"></asp:TextBox></td>
                                <td>
                                    <asp:Label runat="server" Text="Week No."></asp:Label></td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtweekno" CssClass="form-control textsize1" AutoCompleteType="Disabled" TextMode="Number" min="1" max="53" step="1"></asp:TextBox></td>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnView" Text="View" CssClass="Buttons" runat="server" OnClick="btnView_Click" />
                                </td>
                                <td>
                                    <asp:Button runat="server" ID="btnsave" Text="Save" CssClass="Buttons" OnClick="btnSave_click" /></td>
                            </tr>
                        </table>
                    </div>
                </div>

                <asp:HiddenField ID="hdnGridregion" runat="server" />
                <div id="gridContainer" runat="server" style="width: 102%; position: relative;height:83vh;overflow:auto">
                    <asp:ListView runat="server" ID="lvTaskTransReport" ItemPlaceholderID="placeHolderReport" ClientIDMode="Static" OnItemDataBound="listView_ItemDataBound">
                        <LayoutTemplate>
                            <table id="tblTaskDetails" class="filterTable tbl grid" style="width: 116%; border:black 1.5px solid">
                                <tr>
                                    <asp:PlaceHolder runat="server" ID="placeHolderReport" />
                                </tr>
                            </table>
                        </LayoutTemplate>


                        <ItemTemplate>
                            <tr runat="server"  visible='<%# Eval("HeaderVisibility") %>'>
                                    <th style="width:100px"><asp:Label ID="lbheadprojectid" runat="server" Text="Project ID"></asp:Label></th>
                                    <th><asp:Label ID="lbheadmaintask" runat="server" Text="MainTask"/></th>
                                    <th><asp:Label ID="lbheadsubtask" runat="server" Text="Subtask"/></th>
                                    <th><asp:Label ID="lbheadstatus" runat="server" Text="Status"/></th>
                                    <th><asp:Label ID="lbheadesimatedeffort" runat="server" Text="Estimated Time (HH:mm)"/></th>
                                    <th runat="server" visible='<%# Eval("Visible") %>'>
                                     <asp:Label ID="Label1" runat="server" Text="Request"/></th>
                                    <th>Mon</br><asp:Label ID="lbheadmonday" runat="server" Text='<%#Eval("Day1") %>'/></th>
                                    <th>Tue</br><asp:Label ID="lbheadtuesday" runat="server" Text='<%#Eval("Day2") %>'/></th>
                                    <th>Wed</br><asp:Label ID="lbheadwednesday" runat="server" Text='<%#Eval("Day3") %>'/></th>
                                    <th>Thu</br><asp:Label ID="lbheadthursday" runat="server" Text='<%#Eval("Day4") %>'/></th>
                                    <th>Fri</br><asp:Label ID="lbheadfriday" runat="server" Text='<%#Eval("Day5") %>'/></th>
                                    <th>Sat</br><asp:Label ID="lbheadsaturday" runat="server" Text='<%#Eval("Day6") %>'/></th>
                                    <th>Sun</br><asp:Label ID="lbheadsunday" runat="server" Text='<%#Eval("Day7") %>'/></th>
                                </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lbprojectid" Text='<%#Eval("Projectid") %>'></asp:Label>
                                    <asp:HiddenField ID="hdnyearno" Value='<%#Eval("Year") %>' runat="server" />
                                    <asp:HiddenField ID="hdnweekno" Value='<%#Eval("WeekNo") %>' runat="server" />
                                    <asp:HiddenField ID="hdnmaintaskid" Value='<%#Eval("MainTaskIDD") %>' runat="server" />
                                    <asp:HiddenField ID="hdnsubtaskid" Value='<%#Eval("SubTaskIDD") %>' runat="server" />
                                    <asp:HiddenField ID="hdnproblemID" Value='<%#Eval("ProblemID") %>' runat="server" />
                                    <asp:HiddenField runat="server" ID="hdnIDD" Value='<%#Eval("Id") %>'/>
                                    <asp:HiddenField ID="hdntasktype" Value='<%#Eval("TaskType") %>' runat="server" />
                                    <asp:HiddenField ID="hdnrequest" Value='<%#Eval("Request") %>' runat="server" />
                                    <asp:HiddenField ID="hdnassignedto" Value='<%# Eval("AssignedTo") %>' runat="server" />
                                    <asp:HiddenField ID="hdnmaintaskestimatedeffort" Value='<%# Eval("MainTaskEstimatedeffort") %>' runat="server" />
                                </td>
                                <%--visible='<%# Eval("Maintaskvisible") %>'--%>
                                <td runat="server">
                                    <asp:Label runat="server" ID="lbmaintask" Text='<%#Eval("Maintask") %>' />
                                </td>

                                <td style="width:380px">
                                    <asp:Label runat="server" ID="lbsubtask" Text='<%#Eval("Subtask") %>' />
                                </td>
                                <td style="width:80px">
                                  <asp:DropDownList runat="server" ID="ddlsubtaskstatus" CssClass="form-control textsize4"></asp:DropDownList>
                                 <asp:HiddenField runat="server" ID="hdnsubtaskstatus" Value='<%#Eval("SubtaskStatus") %>'  ClientIDMode="Static"/>
                                    <%--<asp:Label runat="server" ID="lblStatus" Text='<%#Eval("SubtaskStatus") %>'></asp:Label>
                                    <asp:LinkButton runat="server" CssClass="glyphicon glyphicon-edit" ID="lnkStatus" OnClick="lnkStatus_Click"></asp:LinkButton>--%>
                                </td>

                                <td style="width:88px">
                                    <asp:Label runat="server" ID="lbEstmteffort" Text='<%#Eval("SubTaskEstimatedeffort") %>' />
                                </td>
                                <td runat="server" visible='<%# Eval("Visible") %>'><asp:Label runat="server" ID="lbrequest" Text='<%#Eval("Request") %>'></asp:Label></td>

                                <td style="width: 100px">
                                    <div class="div-day">
                                        <asp:TextBox ID="txtDay1" runat="server" CssClass="allow-hh-mm-format form-control day-text-box" Width="53" placeholder="HH:mm" Style="position: relative; overflow: unset" Text='<%# Eval("Day1Value") %>' Enabled='<%# Eval("Day1TextBoxEable") %>' AutoCompleteType="Disabled"/>
                                        <asp:LinkButton ID="lbEditDay1" runat="server" ClientIDMode="Static" CssClass="glyphicon glyphicon-pencil" Visible='<%# Eval("Day1EditBtnVisibility") %>' OnClientClick="return EditClicked(this);" OnClick="lbEditDay1_Click" />
                                    
                                        <asp:LinkButton ID="lnkDay1FileName" runat="server" Text='<%# Eval("Day1FileName") %>' 
                                            ToolTip='<%# Eval("Day1FileName") %>' OnClientClick="return FileDownloadClick1(this);" class="filehidden"></asp:LinkButton>
                                        <asp:HiddenField ID="hfDay1Remarks" runat="server" Value='<%# Eval("Day1Remarks") %>' />
                                        <asp:HiddenField ID="hfDay1Date" runat="server" Value='<%# Eval("Day1Date") %>' />
                                        <asp:HiddenField ID="hfDay1FileName" runat="server" Value='<%# Eval("Day1FileName") %>' />
                                        <asp:HiddenField ID="hfDay1FileInBase64" runat="server" Value='<%# Eval("Day1FileInBase64") %>' />
                                        <asp:HiddenField runat="server" ID="_1hfDayNo" Value="1" />
                                         </div>
                                    </div>
                                </td>
                                <td style="width: 100px">
                                    <div class="div-day">
                                        <asp:TextBox ID="txtDay2" runat="server" CssClass="allow-hh-mm-format  form-control  day-text-box" Width="53px" placeholder="HH:mm" Style="position: relative; overflow: unset" Text='<%# Eval("Day2Value") %>' Enabled='<%# Eval("Day2TextBoxEable") %>' AutoCompleteType="Disabled" />
                                        <asp:LinkButton ID="lbEditDay2" runat="server" ClientIDMode="Static" CssClass="glyphicon glyphicon-pencil" Visible='<%# Eval("Day2EditBtnVisibility") %>' OnClientClick="return EditClicked(this);" OnClick="lbEditDay1_Click" />
                                        <asp:LinkButton ID="lnkDay2FileName" runat="server" Text='<%# Eval("Day2FileName") %>' OnClientClick="return FileDownloadClick2(this);" class="filehidden" ToolTip='<%# Eval("Day2FileName") %>'  ></asp:LinkButton>

                                        <asp:HiddenField ID="hfDay2Remarks" runat="server" Value='<%# Eval("Day2Remarks") %>' />
                                        <asp:HiddenField ID="hfDay2Date" runat="server" Value='<%# Eval("Day2Date") %>' />
                                        <asp:HiddenField ID="hfDay2FileName" runat="server" Value='<%# Eval("Day2FileName") %>' />
                                        <asp:HiddenField ID="hfDay2FileInBase64" runat="server" Value='<%# Eval("Day2FileInBase64") %>' />
                                        <asp:HiddenField runat="server" ID="_2hfDayNo" Value="2" />
                                    </div>
                                </td>

                                <td style="width: 100px">
                                    <div class="div-day">
                                        <asp:TextBox ID="txtDay3" runat="server" CssClass="allow-hh-mm-format form-control  day-text-box" Width="53px" placeholder="HH:mm" Style="position: relative; overflow: unset" Text='<%# Eval("Day3Value") %>' Enabled='<%# Eval("Day3TextBoxEable") %>' AutoCompleteType="Disabled"/>
                                        <asp:LinkButton ID="lbEditDay3" runat="server" ClientIDMode="Static" CssClass="glyphicon glyphicon-pencil" Visible='<%# Eval("Day3EditBtnVisibility") %>' OnClientClick="return EditClicked(this);" OnClick="lbEditDay1_Click" />
                                        <asp:LinkButton ID="lnkDay3FileName" runat="server" Text='<%# Eval("Day3FileName") %>' OnClientClick="return FileDownloadClick3(this);" class="filehidden" ToolTip='<%# Eval("Day3FileName") %>'></asp:LinkButton>
                                        <asp:HiddenField ID="hfDay3Remarks" runat="server" Value='<%# Eval("Day3Remarks") %>' />
                                        <asp:HiddenField ID="hfDay3Date" runat="server" Value='<%# Eval("Day3Date") %>' />
                                        <asp:HiddenField ID="hfDay3FileName" runat="server" Value='<%# Eval("Day3FileName") %>' />
                                       <asp:HiddenField ID="hfDay3FileInBase64" runat="server" Value='<%# Eval("Day3FileInBase64") %>' />
                                        <asp:HiddenField runat="server" ID="_3hfDayNo" Value="3" />
                                    </div>
                                </td>
                                <td style="width: 100px">
                                    <div class="div-day">
                                        <asp:TextBox ID="txtDay4" runat="server" CssClass="allow-hh-mm-format form-control  day-text-box" Width="53px" placeholder="HH:mm" Style="position: relative; overflow: unset" Text='<%# Eval("Day4Value") %>' Enabled='<%# Eval("Day4TextBoxEable") %>' AutoCompleteType="Disabled"/>
                                        <asp:LinkButton ID="lbEditDay4" runat="server" ClientIDMode="Static" CssClass="glyphicon glyphicon-pencil" Visible='<%# Eval("Day4EditBtnVisibility") %>' OnClientClick="return EditClicked(this);" OnClick="lbEditDay1_Click" />
                                        <asp:LinkButton ID="lnkDay4FileName" runat="server" Text='<%# Eval("Day4FileName") %>' OnClientClick="return FileDownloadClick4(this);" class="filehidden" ToolTip='<%# Eval("Day4FileName") %>'></asp:LinkButton>
                                        <asp:HiddenField ID="hfDay4Remarks" runat="server" Value='<%# Eval("Day4Remarks") %>' />
                                        <asp:HiddenField ID="hfDay4Date" runat="server" Value='<%# Eval("Day4Date") %>' />
                                        <asp:HiddenField ID="hfDay4FileName" runat="server" Value='<%# Eval("Day4FileName") %>' />
                                       <asp:HiddenField ID="hfDay4FileInBase64" runat="server" Value='<%# Eval("Day4FileInBase64") %>' />
                                        <asp:HiddenField runat="server" ID="_4hfDayNo" Value="4" />
                                    </div>
                                </td>
                                <td style="width: 100px">
                                    <div class="div-day">
                                        <asp:TextBox ID="txtDay5" runat="server" CssClass="allow-hh-mm-format  form-control  day-text-box" Width="53px" placeholder="HH:mm" Style="position: relative; overflow: unset" Text='<%# Eval("Day5Value") %>' Enabled='<%# Eval("Day5TextBoxEable") %>' AutoCompleteType="Disabled"/>
                                        <asp:LinkButton ID="lbEditDay5" runat="server" ClientIDMode="Static" CssClass="glyphicon glyphicon-pencil" Visible='<%# Eval("Day5EditBtnVisibility") %>' OnClientClick="return EditClicked(this);" OnClick="lbEditDay1_Click" />
                                        <asp:LinkButton ID="lnkDay5FileName" runat="server" Text='<%# Eval("Day5FileName") %>' ToolTip='<%# Eval("Day5FileName") %>' OnClientClick="return FileDownloadClick5(this);" class="filehidden"></asp:LinkButton>
                                        <asp:HiddenField ID="hfDay5Remarks" runat="server" Value='<%# Eval("Day5Remarks") %>' />
                                        <asp:HiddenField ID="hfDay5Date" runat="server" Value='<%# Eval("Day5Date") %>' />
                                        <asp:HiddenField ID="hfDay5FileName" runat="server" Value='<%# Eval("Day5FileName") %>' />
                                       <asp:HiddenField ID="hfDay5FileInBase64" runat="server" Value='<%# Eval("Day5FileInBase64") %>' />
                                        <asp:HiddenField runat="server" ID="_5hfDayNo" Value="5" />
                                    </div>
                                </td>
                                <td style="width: 100px">
                                    <div class="div-day">
                                        <asp:TextBox ID="txtDay6" runat="server" CssClass="allow-hh-mm-format  form-control  day-text-box" Width="53px" placeholder="HH:mm" Style="position: relative; overflow: unset" Text='<%# Eval("Day6Value") %>' Enabled='<%# Eval("Day6TextBoxEable") %>' AutoCompleteType="Disabled"/>
                                        <asp:LinkButton ID="lbEditDay6" runat="server" ClientIDMode="Static" CssClass="glyphicon glyphicon-pencil" Visible='<%# Eval("Day6EditBtnVisibility") %>' OnClientClick="return EditClicked(this);" OnClick="lbEditDay1_Click" />
                                        <asp:LinkButton ID="lnkDa6FileName" runat="server" Text='<%# Eval("Day6FileName") %>' OnClientClick="return FileDownloadClick6(this);" class="filehidden" ToolTip='<%# Eval("Day6FileName") %>'></asp:LinkButton>
                                        <asp:HiddenField ID="hfDay6Remarks" runat="server" Value='<%# Eval("Day6Remarks") %>' />
                                        <asp:HiddenField ID="hfDay6Date" runat="server" Value='<%# Eval("Day6Date") %>' />
                                        <asp:HiddenField ID="hfDay6FileName" runat="server" Value='<%# Eval("Day6FileName") %>' />
                                       <asp:HiddenField ID="hfDay6FileInBase64" runat="server" Value='<%# Eval("Day6FileInBase64") %>' />
                                        <asp:HiddenField runat="server" ID="_6hfDayNo" Value="6" />
                                    </div>
                                </td>
                                <td style="width: 100px">
                                    <div class="div-day">
                                        <asp:TextBox ID="txtDay7" runat="server" CssClass="allow-hh-mm-format  form-control  day-text-box" Width="53px" placeholder="HH:mm" Style="position: relative; overflow: unset" Text='<%# Eval("Day7Value") %>' Enabled='<%# Eval("Day7TextBoxEable") %>' AutoCompleteType="Disabled"/>
                                        <asp:LinkButton ID="lbEditDay7" runat="server" ClientIDMode="Static" CssClass="glyphicon glyphicon-pencil" Visible='<%# Eval("Day7EditBtnVisibility") %>' OnClientClick="return EditClicked(this);" OnClick="lbEditDay1_Click" />
                                        <asp:LinkButton ID="lnkDay7FileName" runat="server" Text='<%# Eval("Day7FileName") %>' OnClientClick="return FileDownloadClick7(this);" class="filehidden" ToolTip='<%# Eval("Day7FileName") %>'></asp:LinkButton>
                                        <asp:HiddenField ID="hfDay7Remarks" runat="server" Value='<%# Eval("Day7Remarks") %>' />
                                        <asp:HiddenField ID="hfDay7Date" runat="server" Value='<%# Eval("Day7Date") %>' />
                                        <asp:HiddenField ID="hfDay7FileName" runat="server" Value='<%# Eval("Day7FileName") %>' />
                                       <asp:HiddenField ID="hfDay7FileInBase64" runat="server" Value='<%# Eval("Day7FileInBase64") %>' />
                                        <asp:HiddenField runat="server" ID="_7hfDayNo" Value="7" />
                                    </div>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>

                    <div id="DivRemarks" style="width: 100%; margin-top: 10px; display: none">
                        <div style="margin-bottom: 10px">
                            <span id="lblShowTask" style="font-size: 16px; color: #959595; font-weight: bold;"></span>
                        </div>
                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="6" Style="min-width: 80%" />
                        <%--<asp:FileUpload runat="server" ID="hfFileUpload_Edit1" ClientIDMode="Static" CssClass="form-control" onchange="FUChange1(this);"/>--%>
                    </div>
                </div>

                <div class="modal add-edit-modal" id="EditTaskDetails" tabindex="-1">
                    <div class="modal-dialog" style="width: 940px">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Label CssClass="modal-title" ID="SubTaskEditDetails" runat="server">Update Time Sheet</asp:Label>
                                <a data-dismiss="modal" class="glyphicon glyphicon-remove closeButton"></a>
                            </div>
                            <div class="modal-body" style="overflow: unset">
                                <table class="filterTable tbl grid" style="width:100%">
                                    <tr>
                                        <th>Project ID</th>
                                        <th>MainTask</th>
                                        <th>Task</th>
                                        <th>Spent Hours (HH:mm)</th>
                                        <th>Remarks</th>
                                        <th>File Upload</th>
                                    </tr>
                                    <tr>
                                        <asp:HiddenField runat="server" ID="hfEditedRow" />
                                        <asp:HiddenField runat="server" ID="hfEditedDayNo" />
                                        <td style="width:120px">
                                            <asp:Label runat="server" ID="lblProjectID_Edit"></asp:Label>
                                        </td>
                                        <td style="width:120px">
                                            <asp:Label runat="server" ID="lblMainTask_Edit"></asp:Label>
                                        </td>
                                        <td style="width:280px">
                                            <asp:Label runat="server" ID="lblTask_Edit"></asp:Label>
                                        </td>
                                        <td style="width:60px">
                                            <asp:TextBox ID="txtSpentHour_Edit" runat="server" CssClass="form-control textbox5 allow-hh-mm-format" Style="position: relative" ></asp:TextBox>

                                        </td>
                                        <td style="width:170px">
                                            <asp:TextBox runat="server" ID="txtRemarks_Edit" TextMode="MultiLine" Rows="2" CssClass="form-control"></asp:TextBox>

                                        </td>
                                        <td style="width:150px">
                                            <asp:FileUpload runat="server" ID="hfFileUpload_Edit" ClientIDMode="Static" CssClass="form-control" onchange="FUChange(this);" />
                                            <asp:HiddenField runat="server" ID="hfFile" ClientIDMode="Static" />
                                            <asp:HiddenField runat="server" ID="hfFileName" ClientIDMode="Static" />
                                             <asp:Label runat="server" ID="addFileName">Filename</asp:Label>
                                        </td>
                                    </tr>

                                </table>
                            </div>
                            <div class="modal-footer">
                                <asp:Button runat="server" ID="btnUpdateEntry" CssClass="btn AddEditBtn" Text="Update" OnClientClick="return UpdateEntryClick();" OnClick="btnUpdateEntry_Click" />
                                <%--OnClick="btnUpdateEntry_Click"--%>
                                <asp:Button runat="server" Text="Cancel" ID="BtnEditCancel" CssClass="btn  btn-danger CancelBtn" OnClientClick="return clearscreen();"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:HiddenField runat="server" ID="hfRowIndex" ClientIDMode="Static" />
                <asp:Button runat="server" ID="btnFileDownLoad1" OnClick="btnFileDownLoad_Click1" Visible="false" />
                <asp:Button runat="server" ID="btnFileDownLoad2" OnClick="btnFileDownLoad_Click2" Visible="false" />
                <asp:Button runat="server" ID="btnFileDownLoad3" OnClick="btnFileDownLoad_Click3" Visible="false" />
                <asp:Button runat="server" ID="btnFileDownLoad4" OnClick="btnFileDownLoad_Click4" Visible="false" />
                <asp:Button runat="server" ID="btnFileDownLoad5" OnClick="btnFileDownLoad_Click5" Visible="false" />
                <asp:Button runat="server" ID="btnFileDownLoad6" OnClick="btnFileDownLoad_Click6" Visible="false" />
                <asp:Button runat="server" ID="btnFileDownLoad7" OnClick="btnFileDownLoad_Click7" Visible="false" />
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
                                        <asp:DropDownList runat="server" ID="ddleditsubtaskstatus" CssClass="form-control textsize3" ></asp:DropDownList>
                                    
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btneditsubtaskstatus" runat="server" Text="Save" CssClass="btn AddEditBtn"  />
                        </div>
                        
                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnFileDownLoad1" />
             <asp:PostBackTrigger ControlID="btnFileDownLoad2" />
             <asp:PostBackTrigger ControlID="btnFileDownLoad3" />
             <asp:PostBackTrigger ControlID="btnFileDownLoad4" />
             <asp:PostBackTrigger ControlID="btnFileDownLoad5" />
             <asp:PostBackTrigger ControlID="btnFileDownLoad6" />
              <asp:PostBackTrigger ControlID="lvTaskTransReport" />
        </Triggers>
    </asp:UpdatePanel>
    <script>
        function UpdateEntryClick() {
            return true;
        }
        async function FUChange(element) {
            const filePathsPromises = [];
            const fileName = [];

            var fileExtension = ['xlsx', 'pdf', 'pptx', 'docx', 'png', 'jpg', 'jpeg'];
            for (var i = 0; i < $(element).get(0).files.length; ++i) {
                filePathsPromises.push(ToBase64($(element).get(0).files[i]));
                fileName.push($(element).get(0).files[i].name);

                if ($.inArray($(element).get(0).files[i].name.split('.').pop().toLowerCase(), fileExtension) == -1) {
                    WarningToastr("Allowed file types are xlsx, pdf, ppt, docx, png, jpg and jpeg only!", "");
                    $(element).val("");
                    return;
                }
            }
            const filePaths = await Promise.all(filePathsPromises);
            mappedFiles = filePaths.map((base64File) => ({ file: base64File }));
            let document = "", documentName = "";
            for (let i = 0; i < mappedFiles.length; i++) {
                if (document == "") {
                    document = mappedFiles[i].file;
                    documentName = fileName[i];
                } else {
                    document += ";;;" + mappedFiles[i].file;
                    documentName += ";;;" + fileName[i];
                }
            }

            $("#hfFile").val(document);
            $("#hfFileName").val(documentName);
        };

        function ToBase64(file) {
            return new Promise((resolve, reject) => {
                const reader = new FileReader();
                reader.readAsDataURL(file);
                reader.onload = () => resolve(reader.result);
                reader.onerror = error => reject(error);
            });
        };
        let rowNo = -1, colNo = -1;
        let dayNo;
      
        $(".day-text-box").focus(function () {
            debugger;
            dayNo = $(this).closest("td").find("[id$=hfDayNo]").val();
            let remarks = $(this).closest("td").find("[id$=hfDay" + dayNo + "Remarks]").val();
            $("[id$=txtRemarks]").val(remarks);
            rowNo = $(this).closest("tr").index();
            colNo = $(this).closest("td").index();
            $("#lblShowTask").text("Remarks: " + $(this).closest("tr").find("[id$=lbsubtask]").text());
            $("#DivRemarks").css("display", "block");
        });
        $("[id$=txtRemarks]").blur(function () {
            debugger;
            if (rowNo != -1 && colNo != -1) {
                let tr = $("#tblTaskDetails tr")[rowNo];
                let td = $(tr).find("td")[colNo];
                $(td).find("[id$=hfDay" + dayNo + "Remarks]").val($("[id$=txtRemarks]").val());
                rowNo = -1;
                colNo = -1;
                dayNo = "";
                $("[id$=txtRemarks]").val("");
            }
            $("#lblShowTask").text("");
            $("#DivRemarks").css("display", "none");
        });

        function EditClicked(element) {
            $("[id$=hfEditedDayNo]").val($(element).closest("td").find("[id$=hfDayNo]").val());
            return true;
        }

        function FileDownloadClick1(element) {
            let rowIndex = ($(element).closest("tr").prevAll().length) - 2;
            //alert(rowIndex);
            $("#hfRowIndex").val(rowIndex);
            __doPostBack('<%= btnFileDownLoad1.UniqueID%>', '');
            return false;
        };
        function FileDownloadClick2(element) {
            let rowIndex = ($(element).closest("tr").prevAll().length) - 2;
            //alert(rowIndex);
            $("#hfRowIndex").val(rowIndex);
            __doPostBack('<%= btnFileDownLoad2.UniqueID%>', '');
            return false;
        };
        function FileDownloadClick3(element) {
            let rowIndex = ($(element).closest("tr").prevAll().length) - 2;
            //alert(rowIndex);
            $("#hfRowIndex").val(rowIndex);
            __doPostBack('<%= btnFileDownLoad3.UniqueID%>', '');
            return false;
        };
        function FileDownloadClick4(element) {
            let rowIndex = ($(element).closest("tr").prevAll().length) - 2;
            //alert(rowIndex);
            $("#hfRowIndex").val(rowIndex);
            __doPostBack('<%= btnFileDownLoad4.UniqueID%>', '');
            return false;
        };
        function FileDownloadClick5(element) {
            let rowIndex = ($(element).closest("tr").prevAll().length) - 2;
            //alert(rowIndex);
            $("#hfRowIndex").val(rowIndex);
            __doPostBack('<%= btnFileDownLoad5.UniqueID%>', '');
            return false;
        };
        function FileDownloadClick6(element) {
            let rowIndex = ($(element).closest("tr").prevAll().length) - 2;
            //alert(rowIndex);
            $("#hfRowIndex").val(rowIndex);
            __doPostBack('<%= btnFileDownLoad6.UniqueID%>', '');
            return false;
        };
        function FileDownloadClick7(element) {
            let rowIndex = ($(element).closest("tr").prevAll().length) - 2;
            //alert(rowIndex);
            $("#hfRowIndex").val(rowIndex);
            __doPostBack('<%= btnFileDownLoad7.UniqueID%>', '');
            return false;
        };
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {

            $(document).ready(function () {
                ControlSetter6();
            })
            
            $(".dropdown-container2").css("display", "block");
            $(".day-text-box").focus(function () {
                dayNo = $(this).closest("td").find("[id$=hfDayNo]").val();
                let remarks = $(this).closest("td").find("[id$=hfDay" + dayNo + "Remarks]").val();
                $("[id$=txtRemarks]").val(remarks);
                rowNo = $(this).closest("tr").index();
                colNo = $(this).closest("td").index();
                $("#lblShowTask").text($(this).closest("tr").find("[id$=lbsubtask]").text());
                $("#DivRemarks").css("display", "block");
            });
            $("[id$=txtRemarks]").blur(function () {
                if (rowNo != -1 && colNo != -1) {
                    let tr = $("#tblTaskDetails tr")[rowNo];
                    let td = $(tr).find("td")[colNo];
                    $(td).find("[id$=hfDay" + dayNo + "Remarks]").val($("[id$=txtRemarks]").val());
                    rowNo = -1;
                    colNo = -1;
                    dayNo = "";
                    $("[id$=txtRemarks]").val("");
                }
                $("#lblShowTask").text("");
                $("#DivRemarks").css("display", "none");
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
        });

        $(document).ready(function () {
            ControlSetter6();
        });

        function ControlSetter6() {
            $('[id$=txtyear]').datepicker({
                minViewMode: 2,
                format: 'yyyy',
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
                    $(this).val(value + "0");
                } else {
                    $(this).val(value);
                }

            });
        }
        document.getElementById("headerName").textContent = "Time Entry";
        $(".dropdown-container2").css("display", "block");
        var user = "<%= (String)Session["username"] %>";
        $("#lblUserID").text("Hi " + user);
    </script>
</asp:Content>
