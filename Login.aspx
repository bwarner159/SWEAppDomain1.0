<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 
        {width: 12px;}
    </style>
</head>
<body>
    <h1>Welcome to PhotoSec!</h1>

    <p>This website is for the use of safely storing, viewing, deleting, downloading, and seeing a list of pictures that you (the user) upload. All photos will be secured and encrypted.</p>

    <form id="form2" runat="server">

        Username:<asp:TextBox ID="txtUsername" runat="server"></asp:TextBox></br>

        Password:<asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox></br>

        <asp:Button ID="submitNow" runat="server" Text="Submit" OnClick="submit" /> </br>

        <asp:Label ID="loginresult" runat="server"></asp:Label>
        <asp:Label ID="txtUsernameID" runat="server"></asp:Label>
        <asp:Label ID="txtPasswordId" runat="server"></asp:Label>
        <asp:Label ID="userIdentity" runat="server"></asp:Label>

    </form>
</body>
</html>
