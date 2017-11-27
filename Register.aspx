<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    Username:<asp:TextBox ID="txtUsername" runat="server"/>
    Password:<asp:TextBox ID="txtPassword" runat="server" TextMode="Password"/>
    <asp:Button ID="btnAddUser" runat="server" Text="Submit" OnClick="RegisterUser" />
</form>
</body>
</html>
