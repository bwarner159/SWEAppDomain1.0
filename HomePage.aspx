<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HomePage.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PhotoSec</title>
</head>
<body style="background:url(C:\Users\bwarner6\Downloads\spirationlight\spiration-light.png)">  
    <div style="float:left">
        <img src="C:\Users\Brett\Pictures\animatedlock.jpg" width="50" style="height: 50px" onclick="GoHome"/>
    </div>
    <div style="text-align:center; margin-left:50px">
        <h1 style="width: 914px">Welcome to PhotoSec!</h1>
    </div>
    
        
    
    <p style="text-align:center;">This website is for the use of safely storing, viewing, deleting, downloading, and seeing a list of pictures that you (the user) upload. All photos will be secured and encrypted.</p>
    <form id="form1" runat="server">
    <div style="text-align:center">
        <asp:Button ID="SignUp" runat="server" Text="Signup" OnClick="GoToSignUpPage" style="margin-left: 0px" Width="150px" />
        <asp:Button ID="Login" runat="server" Text="Login" OnClick="GoToLoginPage" Width="150px" />
    </div>
    </form>
</body>
</html>