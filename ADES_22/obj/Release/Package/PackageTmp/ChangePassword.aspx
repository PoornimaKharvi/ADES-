<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="ADES_22.ChangePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title> Change Password </title>
        <script src="Scripts/bootstrap.js"></script>
        <script src="Scripts/bootstrap.min.js"></script>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
        <script type="text/javascript" src="//cdn.jsdelivr.net/jquery/1/jquery.min.js"></script>
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css"/>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"></script>
        <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />

        <%----- Style CSS -----%>

        <style>
            .BodyStyle {
                overflow: hidden;
                font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            }

            .ResetPass {
                width: 40%;
                background-color: #fff;
                margin-left: 450px;
                margin-top: -600px;
                box-shadow: 5px 10px 18px #888888;
                border-radius: 15px;
            }

            .header {
                color: orange;
                font-size: x-large;
                font-weight: bold;
                margin-left: 200px;
                margin-top: 100px;
            }

            .orange {
                margin-left: -10px;
                margin-top: -10px;
                margin-bottom: -80px;
                margin-right: -20px;
                border: 3px solid orange;
                width: 750px;
                background-color: orange;
                height: 760px;
            }

            .span {
                font-weight: 600;
                font-size: 18px;
                margin-left: 120px;
            }

            .input-group-addon {
                padding: 6px;
                font-size: 14px;
                font-weight: 200;
                line-height: 1;
                color: #555;
                text-align: center;
                background-color: #eee;
                border: 1px solid #ccc;
                border-radius: 4px;
                width: auto;
                margin-left: 90px;
            }

            #TxtNewPass, #TxtConfirmPass {
                Width: 60%;
                margin-left: 120px;
            }

            .ResetBtn {
                color: white;
                background-color: orange;
                padding: 9px 9px;
                border-radius: 15px;
                width: 300px;
                font-size: medium;
                font-weight: bold;
                margin-left: 150px;
                border: 3px solid orange;
            }

            #ErrorMsg {
                color: red;
                text-align: center;
                margin-left: 205px;
            }

            #Link_BackToLogin {
                margin-left: 245px;
                margin-top: 10px;
                font-size: medium;
                font-weight: 600;
            }
        </style>
    </head>

    <%----- Div Container -----%>

    <body class="BodyStyle">
        <form id="form1" runat="server">
           
            <div id="orange" class="orange">
                <br />
            </div>
            
            <div id="ResetPass" runat="server" class="ResetPass">
                <br />
                <asp:Label ID="header" Text="Reset Your Password" CssClass="header" runat="server" />

                <div id="divbody" class="divbody" runat="server">
                    <br /><br />

                    <span class="span"> New Password</span>
                    <br />

                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="TxtNewPass" runat="server" CssClass="form-control" TextMode="Password" Placeholder="Enter New Password" Width="367px" ToolTip="Must contain atleast one number and one uppercase and lower case letter and one special character, and atleast 8 or more characters" required />
                            </td>
                            
                            <td>
                                &nbsp;<span id="toggle_pwd1" class="fa fa-fw fa-eye field_icon"/>
                            </td>
                        </tr>
                    </table>
                    <br /><br />
                    
                    <span class="span"> Confirm New Password</span>
                    <br />
                    
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="TxtConfirmPass" runat="server" CssClass="form-control" TextMode="Password" Placeholder="Enter Confirm Password" Width="367px" ToolTip="Must contain atleast one number and one uppercase and lower case letter and one special character, and atleast 8 or more characters" required />
                            </td>

                            <td>
                                &nbsp;<span id="toggle_pwd" class="fa fa-fw fa-eye field_icon"/>
                            </td>
                        </tr>
                    </table>
                    <br />

                    <asp:Button ID="BtnReset" runat="server" CssClass="ResetBtn" Text="Reset Password" OnClientClick="return PasswordValidation();" OnClick="BtnReset_Click" />
                    <br /><br />
                
                    <asp:Label ID="ErrorMsg" runat="server" />
                    <br />

                    <asp:LinkButton ID="Link_BackToLogin" runat="server" Text="Back To Login" OnClick="Link_BackToLogin_Click" />
                    <br /><br />
                </div>
            </div>
        </form>

        <%----- Scripts -----%>

        <script type="text/javascript">
            $("#toggle_pwd").click(function () {
                $(this).toggleClass("fa-eye fa-eye-slash");
                var type = $(this).hasClass("fa-eye-slash") ? "text" : "password";
                $("[id*=TxtConfirmPass]").attr("type", type);
            });

            $("#toggle_pwd1").click(function () {
                $(this).toggleClass("fa-eye fa-eye-slash");
                var type = $(this).hasClass("fa-eye-slash") ? "text" : "password";
                $("[id*=TxtNewPass]").attr("type", type);
            });

            function PasswordValidation() {
                var NewPass = $("[id$=TxtNewPass]").val();
                var ConfirmPass = $("[id$=TxtConfirmPass]").val();

                if (NewPass != "" && (validatePassword(NewPass) == false)) {
                    document.getElementById("ErrorMsg").textContent = "Please enter valid password!";
                    document.getElementById("ErrorMsg").style.marginLeft = "205px";
                    $("[id$=TxtNewPass]").focus();
                    return false;
                }
                else
                {
                    if (ConfirmPass != "" && (validatePassword(ConfirmPass) == false)) {
                        document.getElementById("ErrorMsg").textContent = "Please enter valid password!";
                        document.getElementById("ErrorMsg").style.marginLeft = "205px";
                        $("[id$=TxtConfirmPass]").focus(); 
                        return false;
                    }
                }

                if (NewPass != ConfirmPass) {
                    document.getElementById("ErrorMsg").textContent = "New and Confirm password must match!";
                    document.getElementById("ErrorMsg").style.marginLeft = "177px";
                    return false;
                }

                return true;
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
        </script>
    </body>
</html>

