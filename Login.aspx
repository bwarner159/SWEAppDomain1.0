﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PhotoSec</title>
</head>
<body style="background:url(C:\Users\bwarner6\Downloads\spirationlight\spiration-light.png)">
    <div style="float:left">
        <img src="C:\Users\Brett\Pictures\animatedlock.jpg" width="50" height="50"/>
    </div>
    <div style="display:inline-block">
        <h1>Welcome to PhotoSec!</h1>
    </div>
    <form id="form2" runat="server">
        Username:<asp:TextBox ID="userNameTxt" runat="server" MaxLength="40" Height="16px"></asp:TextBox></br>
        Password:<asp:TextBox ID="passWordTxt" runat="server" TextMode="Password" MaxLength="30"></asp:TextBox></br>
        <asp:Button ID="submitButton" runat="server" Text="Submit" OnClick="submit" Width="191px" /> </br>
        <asp:Label ID="errLbl" runat="server" Text=""></asp:Label>
    </form>
</body>
</html>