<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ValidityFailForm.aspx.cs" Inherits="ADES_22.ValidityFailForm" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Validity Expired</title>
    <style>
        .DivStyle {
            text-align: center;
            margin-top: -550px !important;
            z-index: 100;
            width: 40%;
            background-color: #fff;
            margin-left: 450px;
            margin-top: -600px;
            box-shadow: 5px 10px 18px #888888;
            border-radius: 15px;
        }

        .ValidityMsg {
            font-weight: bold;
            font-size: 25px;
            text-align: center;
        }

        .LoginLink {
            margin-top: 10px;
            font-size: 20px;
            font-weight: 600;
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
            z-index: 50;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="orange" class="orange">
     
        </div>
            
        <div class="DivStyle">
            <br /><br /><br /><br /><br />
            <span class="ValidityMsg">Oops.. Link validity has expired!</span> <br /><br /><br />
            <asp:LinkButton ID="LinkLogin" runat="server" CssClass="LoginLink" Text="Back To Login" OnClick="LinkLogin_Click" />
            <br /><br /><br /><br /><br />
        </div>
    </form>
</body>
</html>
