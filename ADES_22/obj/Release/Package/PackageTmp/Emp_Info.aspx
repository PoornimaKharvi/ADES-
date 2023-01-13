<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Emp_Info.aspx.cs" Inherits="ADES_22.Emp_Info" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />

    <%----- Style CSS -----%>

    <style>
        #li_emp{
            background-color: #f0f0f0;
            color: black;
            font-weight: bold;
        }

        .tb{
            margin-left: 10px;
        }

        .width {
            width: 92%;
            margin-left: 45px;
            height: 80vh;
            overflow: auto;
        }

        .btncontainer{
            padding:0px; 
            width:150px; 
            position: sticky;
            margin-left: 1180px;
            margin-top: 20px;
        }

        .modal-body{
            color: black;
        }

        .modal-table1{
            width: 100%;
            overflow: unset;
        }

        .modal-table1 tr td{
            padding: 5px;
        }

        .form-control {
            width: 200px;
        }
    </style>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div id="btncontainer" class="btncontainer">
                <asp:Button ID="btnadd" runat="server" Text="Add Employee" CssClass="Buttons" OnClick="btnadd_Click"/>
            </div>

            <%----- GridView -----%>

            <div id="gridContainer" class="gridContainer width" runat="server">
                <asp:GridView ID="gvitems" runat="server" CssClass="grid" DataKeyNames="empID" OnRowDeleting="gvitems_RowDeleting" OnSelectedIndexChanged="gvitems_SelectedIndexChanged" AutoGenerateColumns="false" Width="100%" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found !!" EmptyDataRowStyle-CssClass="empty-row-style">
                    <Columns>
                        <asp:TemplateField HeaderText="Employee ID">
                            <ItemTemplate>
                                <asp:Label ID="lblempID" runat="server" Text='<%# Eval("empID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate> 
                                <asp:Label ID="lblempName" runat="server" Text='<%# Eval("empName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Department">
                            <ItemTemplate>
                                <asp:Label ID="lblDept" runat="server" Text='<%# Eval("Dept") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Role">
                            <ItemTemplate>
                                <asp:Label ID="lblrole" runat="server" Text='<%# Eval("role") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Email">
                            <ItemTemplate>
                                <asp:HiddenField runat="server" ID="HiddenField1" Value='<%# Eval("password") %>' />
                                <asp:Label ID="lblemail" runat="server" Text='<%# Eval("email") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Reporting To">
                            <ItemTemplate>
                                <asp:Label ID="lblReportingTo" runat="server" Text='<%# Eval("ReportingTo") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <div style="text-align: center">
                                    <asp:LinkButton ID="btnedit" runat="server" CssClass="glyphicon glyphicon-pencil EditlinkBtn" ToolTip="Edit" CommandName="Select" CausesValidation="false" />&nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton ID="btndlt" runat="server" CssClass="glyphicon glyphicon-trash DeletelinkBtn" ToolTip="Delete" CommandName="Delete" CausesValidation="false" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <%----- Delete Modal -----%>

            <div class="modal fade add-edit-modal" id="myConfirmationModal" role="dialog" style="min-width: 300px;">
                <div class="modal-dialog modal-dialog-centered" style="width: 450px">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Confirmation? <i class="glyphicon glyphicon-remove closeButton" data-dismiss="modal"></i></h4>
                        </div>

                        <div class="modal-body">
                            <asp:Label ID="ConfirmText" runat="server" CssClass="ConfirmText" />
                        </div>

                        <div class="modal-footer">
                            <input type="button" value="Yes" class="btn AddEditBtn" id="saveConfirmYes" onserverclick="saveConfirmYes_ServerClick" runat="server" data-dismiss="modal" />
                            <input type="button" value="No" class="btn btn-danger CancelBtn" id="saveConfirmNo" onclick="saveConfirmNo()" data-dismiss="modal" />
                        </div>
                    </div>
                </div>
            </div>

            <%----- Add/Edit Modal -----%>

            <div class="modal fade add-edit-modal" id="EditModal" role="dialog" style="min-width: 300px;">
                <div class="modal-dialog modal-dialog-centered" style="width: 750px">
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:Label ID="modaltitle" CssClass="modal-title" runat="server" Text="Add Employee" />
                            <a data-dismiss="modal" class="glyphicon glyphicon-remove closeButton"></a>
                        </div>

                        <br />
                        <div class="modal-body">
                            <table class="modal-table1">
                                <tr>
                                    <td>
                                        <span>Employee ID *</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmpID" runat="server" AutoCompleteType="Disabled" CssClass="form-control" TabIndex="1" />
                                    </td>

                                    <td>
                                       <span>Employee Name *</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmpName" runat="server" AutoCompleteType="Disabled" CssClass="form-control" TabIndex="2" />
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <span>Department *</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control" TabIndex="3" />
                                    </td>

                                    <td>
                                        <span>Role *</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlrole" runat="server" CssClass="form-control" TabIndex="4" />
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <span>Email ID *</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmail" runat="server" AutoCompleteType="Disabled" CssClass="form-control" TabIndex="5"/>
                                    </td>

                                    <td>
                                        <span>Password *</span>
                                    </td>
                                    <td style="width: 200px">
                                        <asp:TextBox ID="txtEmpPass" runat="server" TextMode="Password" TabIndex="6" ToolTip="Must contain atleast one number and one uppercase and lower case letter and one special character, and atleast 8 or more characters" AutoCompleteType="Disabled" CssClass="form-control" />
                                    </td>
                                    <td>
                                        <span id="toggle_pwd" class="fa fa-fw fa-eye field_icon"></span>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <span>Reporting To</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DdlReportingTo" runat="server" CssClass="form-control" TabIndex="7" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />

                        <div class="modal-footer">
                            <input type="submit" value="Update" class="btn AddEditBtn" tabindex="8" id="btnedit" onclick="return EmpValidation();" onserverclick="btnedit_ServerClick" runat="server" />
                            <input type="button" value="Cancel" class="btn btn-danger CancelBtn" tabindex="9" id="btneditcancel" onclick="editcancel()" data-dismiss="modal" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
    
    <%----- Scripts -----%>

    <script type="text/javascript">
        $("#toggle_pwd").click(function () {
            $(this).toggleClass("fa-eye fa-eye-slash");
            var type = $(this).hasClass("fa-eye-slash") ? "text" : "password";
            $("[id*=txtEmpPass]").attr("type", type);
        });

        function EmpValidation() {
            if ($("[id$=txtEmpID]").val() == "") {
                WarningToastr("Employee ID is required!");
                $("[id$=txtEmpID]").focus();
                return false;
            }

            if ($("[id$=txtEmpName]").val() == "") {
                WarningToastr("Employee name is required!");
                $("[id$=txtEmpName]").focus();
                return false;
            }

            if ($("[id$=ddlDept]").val() == "Select") {
                WarningToastr("Department is required!");
                $("[id$=ddlDept]").focus();
                return false;
            }

            if ($("[id$=ddlrole]").val() == "Select") {
                WarningToastr("Role is required!");
                $("[id$=ddlrole]").focus();
                return false;
            }

            if ($("[id$=txtEmpPass]").val() == "") {
                WarningToastr("Password is required!");
                $("[id$=txtEmpPass]").focus();
                return false;
            }

            var EmailText = $("[id$=txtEmail]").val();
            if (EmailText != "") {
                if (validateEmail(EmailText) == false) {
                    WarningToastr('Please enter valid Email-ID!');
                    $("[id$=txtEmail]").focus();
                    return false;
                }

                var Passwd = $("[id$=txtEmpPass]").val();
                if (validatePassword(Passwd) == false) {
                    WarningToastr('Please enter valid password!');
                    $("[id$=txtEmpPass]").focus();
                    return false;
                }
            }
            else {
                WarningToastr('Email-ID is required!');
                $("[id$=txtEmail]").focus();
                return false;
            }
            return true;
        };

        function validateEmail(sEmail) {
            var filter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
            if (filter.test(sEmail)) {
                return true;
            }
            else {
                return false;
            }
        }

        function validatePassword(passwd) {
            var pattern = /^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%&]).*$/;
            if (pattern.test(passwd)) {
                return true;
            }
            else {
                return false;
            }
        }

        $(window).resize(function () {
            var Height = $(window).height() - 290;
            $('.gridContainer').css('height', Height);
        });

        document.getElementById("headerName").textContent = "Employee Details";

        var user = "<%= (String)Session["username"] %>";
        $("#lblUserID").text("Hi " + user);

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            $("#toggle_pwd").click(function () {
                $(this).toggleClass("fa-eye fa-eye-slash");
                var type = $(this).hasClass("fa-eye-slash") ? "text" : "password";
                $("[id*=txtEmpPass]").attr("type", type);
            });

            var user = "<%= (String)Session["username"] %>";
            $("#lblUserID").text("Hi " + user);
        });
    </script>
</asp:Content> 