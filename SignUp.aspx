<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SignUp.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h1>Welcome to PhotoSec!</h1>
    <p>This website is for the use of safely storing, viewing, deleting, downloading, and seeing a list of pictures that you (the user) upload. All photos will be secured and encrypted.</p>
    <form id="form1" runat="server">
    <div>
        First Name: </br>
        <asp:TextBox ID="firstNameTxt" runat="server"></asp:TextBox> </br>
        Last Name: </br>
        <asp:TextBox ID="lastNameTxt" runat="server"></asp:TextBox> </br>
        Username: </br>
        <asp:TextBox ID="userNameTxt" runat="server"></asp:TextBox> </br>
        Email: </br>
        <asp:TextBox ID="emailTxt" runat="server"></asp:TextBox> </br>
        Confirm Email: </br>
        <asp:TextBox ID="confirmEmailTxt" runat="server"></asp:TextBox> </br>
        Password: </br>
        <asp:TextBox ID="passWordTxt" runat="server"></asp:TextBox> </br>
        Confirm Password: </br>
        <asp:TextBox ID="confirmPassWordTxt" runat="server"></asp:TextBox> </br>
        <asp:Button ID="submitButton" runat="server" Text="Submit" OnClick="submit" />
        <asp:Label ID="errorLbl" runat="server" Text=""></asp:Label>
    </div>
    </form>
</body>
</html>
