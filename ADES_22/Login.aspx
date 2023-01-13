<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ADES_22.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title> Login Form </title>
       <%-- <script src="Scripts/bootstrap.js"></script>
        <script src="Scripts/bootstrap.min.js"></script>--%>
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

            .login {
                width: 40%;
                background-color: #fff;
                margin-left: 30%;
                margin-top: -39%;
                box-shadow: 5px 10px 18px #888888;
                border-radius: 15px;
            }

            .header {
                color: orange;
                font-size: x-large;
                font-weight: bold;
                margin-top: 5%;
            }

            .orange {
                margin-left: -10px;
                border: 3px solid orange;
                width: 50%;
                background-color: orange;
                height: 100vh;
            }

            .span {
                font-weight: 600;
                font-size: 18px;
                margin-left: 20%;
            }

            #txtusername {
                Width: 61%;
                margin-left: 20%;
            }

            .TablePassword{
                width: 68%;
                margin-left: 20% !important;
            }

            .btnlogin {
                color: white;
                background-color: orange;
                padding: 9px 9px;
                border-radius: 15px;
                width: 50%;
                font-size: medium;
                font-weight: bold;
                margin-left: 25%;
                border: 3px solid orange;
            }

            #errormsg {
                color: red;
                text-align: center;
                margin-left: 205px;
            }

            .TrForgot {
                float: right;
                margin-top: 5px;
            }

            #LinkForgot {
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
   
            <div id="login" class="login">
                <br />
                <div style="text-align:center">
                    <asp:Label ID="header" Text="Login to your account" CssClass="header" runat="server" />
                </div>
                <div id="divbody" class="divbody">
                    <br /><br />

                    <span class="span"> User ID</span><br />
                    <asp:TextBox ID="txtusername" runat="server" CssClass="form-control" AutoCompleteType="Disabled"  Placeholder="Enter User ID" required />
                    <br /><br />
                    
                    <span class="span"> Password</span><br />
                    
                    <table class="TablePassword">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtpass" runat="server" CssClass="form-control" TextMode="Password" Placeholder="Enter Password" required />
                            </td>
                            <td>
                                &nbsp;<span id="toggle_pwd" class="fa fa-fw fa-eye field_icon"/>
                            </td>
                        </tr>

                        <tr>
                            <td class="TrForgot">
                                <asp:LinkButton ID="LinkForgot" runat="server" Text="Forgot Password?" OnClick="LinkForgot_Click" />
                            </td>
                        </tr>
                    </table>
                    <br />

                    <asp:Button ID="btnlogin" runat="server" CssClass="btnlogin" Text="Login" OnClick="btnlogin_Click" />
                    <br /><br />
                
                    <asp:Label ID="errormsg" runat="server" Text="Incorrect Username or Password!" />
                    <br /><br /><br />
                </div>
            </div>
        </form>

        <%----- Scripts -----%>

        <script type="text/javascript">
            $("#toggle_pwd").click(function () {
                $(this).toggleClass("fa-eye fa-eye-slash");
                var type = $(this).hasClass("fa-eye-slash") ? "text" : "password";
                $("[id*=txtpass]").attr("type", type);
            });
        </script>
    </body>
</html>
