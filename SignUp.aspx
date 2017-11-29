<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SignUp.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PhotoSec</title>
</head>
<body style="background:url(C:\Users\Brett\Pictures\backgroundImg.jpg)">
    <div style="float:left">
        <img src="C:\Users\Brett\Pictures\animatedlock.jpg" width="50" height="50"/>
    </div>
    <div style="display:inline-block">
        <h1>Welcome to PhotoSec!</h1>
    </div>
    <form id="form1" runat="server">
    <div>
        Username: </br>
        <asp:TextBox BorderStyle="Solid" ID="txtUsername" runat="server" MaxLength="40"></asp:TextBox> </br>
        Password: </br>
        <asp:TextBox BorderStyle="Solid" ID="txtPassword" runat="server" MaxLength="30" TextMode="Password"></asp:TextBox> </br>
        <asp:Button ID="btnAddUser" runat="server" Text="Submit" OnClick="submit" /> </br>
        <asp:Label ID="errorLbl" runat="server" Text="" Visible="false"></asp:Label>
    </div>
    </form>
</body>
</html>
