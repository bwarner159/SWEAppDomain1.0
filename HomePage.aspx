<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HomePage.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PhotoSec</title>
</head>
<body>
    <div style="float:left">
        <img src="C:\Users\Brett\Pictures\animatedlock.jpg" width="50" height="50"/>
    </div>
    <div style="display:inline-block">
        <h1>Welcome to PhotoSec!</h1>
    </div>
    
    <p>This website is for the use of safely storing, viewing, deleting, downloading, and seeing a list of pictures that you (the user) upload. All photos will be secured and encrypted.</p>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="SignUp" runat="server" Text="Signup" OnClick="GoToSignUpPage" />
        <asp:Button ID="Login" runat="server" Text="Login" OnClick="GoToLoginPage" />
    </div>
    </form>
</body>
</html>
