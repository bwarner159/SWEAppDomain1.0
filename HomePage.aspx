<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HomePage.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PhotoSec1.2</title>
</head>
<body>
    <h1>Welcome to PhotoSec!</h1>
    <p>This website is for the use of safely storing, viewing, deleting, downloading, and seeing a list of pictures that you (the user) upload. All photos will be secured and encrypted.</p>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="SignUp" runat="server" Text="Signup" />
        <asp:Button ID="Login" runat="server" Text="Login" />
    </div>
    </form>
</body>
</html>
