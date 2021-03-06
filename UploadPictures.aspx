﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadPictures.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PhotoSec</title>
</head>
<body style="background:url(C:\Users\Brett\Pictures\backgroundImg.jpg)">
    <form id="form1" runat="server">
    <div style="float:left">
        <img src="C:\Users\Brett\Pictures\animatedlock.jpg" width="50" height="50"/>
    </div>
    <div style="display:inline-block">
        <h1>Upload Your Pictures!</h1>
    </div>
    <div>
        <p>Browse File Directory and Submit Picture</p>
        <asp:FileUpload ID="picFile" runat="server" />
    </div>
    <div>
        <asp:Button ID="submitPicture" runat="server" Text="Submit" OnClick="submitButton"/> </br>     
            <asp:Label ID="result" runat="server"></asp:Label></br>
        <asp:Button ID="goToViewPics" runat="server" Text="View Pictures" OnClick="GoToViewPictures"/>
        
    </div>
    </form>
</body>
</html>