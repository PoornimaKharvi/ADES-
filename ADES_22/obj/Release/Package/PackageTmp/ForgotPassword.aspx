<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="ADES_22.ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Forgot Password?</title>
        <script src="Scripts/bootstrap.js"></script>
        <script src="Scripts/bootstrap.min.js"></script>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
        <script type="text/javascript" src="//cdn.jsdelivr.net/jquery/1/jquery.min.js"></script>
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css" />
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

            .table-left {
                margin-left: 120px;
            }

            .span {
                font-weight: 600;
                font-size: 18px;
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

            #TxtUserID {
                margin-top: 15px;
                Width: 60%;
                margin-left: 120px;
            }

            .BtnSendLink {
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
                margin-left: 215px;
            }

            .TrForgot {
                float: right;
                margin-top: 5px;
            }

            #LinkForgot {
                font-weight: 600;
            }

            #Link_BackToLogin {
                margin-left: 245px;
                font-size: medium;
                font-weight: 600;
            }
        </style>
    </head>

    <%-----  Div Container -----%>

    <body class="BodyStyle">
        <form id="form1" runat="server">
            <div id="orange" class="orange">
                <br />
            </div>

            <div id="login" class="login">
                <br /><br />
                <asp:Label ID="header" Text="Trouble Logging In?" CssClass="header" runat="server" />

                <div id="divbody" class="divbody">
                    <br /><br />
                    <table class="table-left">
                        <tr>
                            <td>
                                <span class="span">Enter your User ID and we'll send you a link to<br />get back into your account. </span><br />
                            </td>
                        </tr>
                    </table>

                    <asp:TextBox ID="TxtUserID" runat="server" CssClass="form-control" AutoCompleteType="Disabled" Placeholder="Enter User ID" required />
                    <br /><br />

                    <asp:Button ID="BtnSendLink" runat="server" CssClass="BtnSendLink" Text="Send Login Link" OnClientClick="return EValidation();" OnClick="BtnSendLink_Click" />
                    <br /><br />

                    <asp:Label ID="ErrorMsg" runat="server" ClientIDMode="Static" Text="Please enter valid -ID!" />
                    <br /><br />

                    <asp:LinkButton ID="Link_BackToLogin" runat="server" Text="Back To Login" OnClick="Link_BackToLogin_Click" />
                    <br /><br />
                </div>
            </div>
        </form>

        <%----- Scripts -----%>

        <script type="text/javascript">
            $("[id$=TxtUserID]").focus();
        </script>   
    </body>
</html>
