<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WeeklyReport.aspx.cs" Inherits="ADES_22.WeeklyReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .textsize1 {
            width: 170px;
        }
        </style>
    <div class="container-fluid">
        <div id="main">
           <div id="import" runat="server" class="importdiv">
               <asp:Button runat="server" ID="btnSave" CssClass="Buttons" Style="margin-left:1162px" Text="Save"  OnClientClick="return Savelistviewdata();" OnClick="btnSave_Click"/>
             <%--<table id="tbl1" class="filterTable tbl">
                 <tr>
                        <td><span>Manager</span></td>
                        <td><asp:DropDownList runat="server" ID="ddlManager"  CssClass="form-control textsize1"></asp:DropDownList></td>
                        <td> <span>Week No:</span></td>
                        <td> <asp:TextBox runat="server" ID="txtWeekno"  CssClass="form-control textsize1" AutoCompleteType="Disabled"></asp:TextBox></td>
                 </tr>
              </table>--%>
           </div>
        </div>
             <div id="gridContainer" runat="server" style="width: 100%; padding-top: 5px; height:80vh;overflow:auto">
              <asp:ListView runat="server" ID="lvReport" ItemPlaceholderID="placeHolderReport">
                <LayoutTemplate>
                    <table class="filterTable tbl grid" style="width: 97%; border: 1px solid">
                        <tr>
                            <asp:PlaceHolder runat="server" ID="placeHolderReport" />
                        </tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr runat="server"  visible='<%# Eval("HeaderVisibility") %>'>
                        <th colspan="6">Weekly Meeting Presentation</th>
                        <th colspan="2">Planner</th>
                        <th colspan="2"><asp:Label runat="server" ID="lbPlanner" Text='<%#Eval("Planner")%>'></asp:Label></th>
                    </tr>
                    <tr>
                        <th colspan="4"><asp:Label runat="server" Text='<%#Eval("WeekNumberText") %>'></asp:Label></th>
                        <th colspan="2"><asp:Label ID="lbreportweekno" runat="server" Text='<%#Eval("Weekno")%>' ></asp:Label></th>
                        <th colspan="2">Hours in the Week</th>
                        <th colspan="2">25</th>
                    </tr>
                    <tr>
                        <th>Team Size</th>
                        <th>Available Hours</th>
                        <th>Planned Hours</th>
                        <th style="width: 66px">P To A</th>
                        <th>Planned Task</th>
                        <th>Tasks Taken Up As Per Plan</th>
                        <th>Adherence to Plan</th>
                        <th>Utilized Hours</th>
                        <th style="width: 68px">U To P</th>
                        <th>Tasks Not Planned But taken Up</th>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbTeamSize" Text='<%# Eval("TeamSize") %>' runat="server" /></td>
                            <asp:HiddenField ID="hdnyearno" Value='<%#Eval("Year") %>' runat="server"/>
                         <asp:HiddenField ID="hdnweekno" Value='<%#Eval("WeekNo") %>' runat="server"/>
                        <asp:HiddenField ID="hdncheckweek" runat="server" Value='<%# Eval("WeekNumberText") %>'/>
                        <td>
                            <asp:Label ID="lbAvailableHours" Text='<%# Eval("AvailableHours") %>' runat="server" /></td>
                        <td>
                            <asp:Label ID="lbPlannedHours" Text='<%# Eval("PlannedHours") %>' runat="server" /></td>
                        <td>
                            <asp:Label ID="lbPToA" Text='<%# Eval("PToA") %>' runat="server" /></td>
                        <td>
                            <asp:Label ID="lbPlannedTask" Text='<%# Eval("PlannedTask") %>' runat="server" /></td>
                        <td>
                            <asp:Label ID="lbTaskTakenPerPlan" Text='<%# Eval("TaskTakenPerPlan") %>' runat="server" /></td>
                        <td>
                            <asp:Label ID="lbAdherenceToPlan" Text='<%# Eval("AdherenceToPlan") %>' runat="server" /></td>
                        <td>
                            <asp:Label ID="lbUtilizedHours" Text='<%# Eval("UtilizedHours") %>' runat="server" /></td>
                        <td>
                            <asp:Label ID="lbUToP" Text='<%# Eval("UToP") %>' runat="server" /></td>
                        <td>
                            <asp:Label ID="lbTaskNotPlannedButTakenUp" Text='<%# Eval("TaskNotPlannedButTakenUp") %>' runat="server" /></td>
                    </tr>
                    <tr runat="server"  visible='<%# Eval("HeaderVisibility") %>'>
                        <td colspan="10"><asp:Label ID="lbskipptask" runat="server" Text="Skipped Task" style="font-weight: 700;"></asp:Label></td>
                    </tr>
                  
                    <tr  runat="server" visible='<%#Eval("HeaderVisibility") %>'>
                        <td colspan="10" style="height: 60px;">
                            <asp:Label runat="server" ID="lbskippedtask" Style="max-width: 1300px" Text='<%#Eval("SkippedTask") %>'></asp:Label >
                        </td>
                    </tr>
                   
                    <tr runat="server"  visible='<%#Eval("SkippedTaskTextBox") %>'>
                        
                        <td colspan="7"><asp:Label runat="server" Text="Mention 4 Dependencies" style="font-weight: 700;"></asp:Label></td>
                        <td colspan="3"><asp:Label runat="server" Text="Priority" style="font-weight: 700;"></asp:Label></td>
                    </tr>

                    <tr runat="server" visible='<%#Eval("SkippedTaskTextBox") %>'> 
                         <td colspan="7" ><asp:TextBox runat="server" ID="txtDependencies" CssClass="form-control" Rows="3" TextMode="MultiLine" Style="max-width: 900px" Text='<%#Eval("Dependencies") %>' ></asp:TextBox></td> <td colspan="3"><asp:TextBox runat="server" ID="txtUpdatedTask" CssClass="form-control" Rows="3" TextMode="MultiLine" Style="max-width: 500px" ></asp:TextBox></td>                                                                             </tr>

                    <tr runat="server"  visible='<%# Eval("HeaderVisibility") %>'>
                        <td colspan="10">
                            <asp:Label runat="server" ID="lbtasksts" Text="Task Status" style="font-weight: 700;"></asp:Label>
                        </td>
                    </tr>

                    <tr runat="server" visible='<%# Eval("HeaderVisibility") %>'>
                        <td colspan="10" style="height: 60px;">  <asp:Label runat="server" ID="lbtaskstatus" Style="max-width: 1300px" Text='<%# Eval("TaskStatus") %>' ></asp:Label >
                        </td>
                    </tr>

                    <tr runat="server"  visible='<%# Eval("HeaderVisibility") %>'>
                      <td colspan="2"><asp:Label runat="server" Text="Production Support" style="font-weight: 700;"></asp:Label></td>
                        <td colspan="8"><asp:Label runat="server" ID="lbproductionsupport" Text= '<%#Eval("ProductionSupport") %>'></asp:Label></td>
                    </tr>

                    <tr runat="server" visible='<%#Eval("SkippedTaskTextBox") %>'><td colspan="10"><asp:Label runat="server" Text="Major Task" style="font-weight: 700;"></asp:Label></td>
                    </tr>

                    <tr runat="server" visible='<%#Eval("SkippedTaskTextBox") %>'>
                        <td colspan="10">
                        <asp:TextBox runat="server" ID="txtMajorTask" CssClass="form-control" Rows="3"  TextMode="MultiLine" Style="max-width: 1300px" Text='<%#Eval("MajorTask") %>'></asp:TextBox></td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
        </div>
    </div>
          
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            $(document).ready(function () {
            });
            function Savelistviewdata() {
                debugger;
                var listview = document.getElementById("<%=lvReport.ClientID%>");
                debugger;
                alert($("#lvReport tr").length());
            }
            $(".dropdown-container2").css("display", "block");
        });
        document.getElementById("headerName").textContent = "Weekly Task Report";
        $(".dropdown-container2").css("display", "block");
        var user = "<%= (String)Session["username"] %>";
        $("#lblUserID").text("Hi " + user);
    </script>
</asp:Content>
