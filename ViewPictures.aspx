<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewPictures.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PhotoSec</title>
</head>
<body>
    <form runat="server">
    <div style="float:left">
        <img src="C:\Users\Brett\Pictures\animatedlock.jpg" width="50" height="50"/>
    </div>
    <div style="display:inline-block">
        <h1>Your Pictures</h1>
        </br>
    </div>
        
    <div>
        <asp:Button runat="server" ID="goToUploadPg" Text="Go to Upload Page" OnClick="GoToUpload"/>
        </br>
    </div>
    <div>
        <asp:Button ID="ViewPicturesButton" runat="server" OnClick="viewPicturesButton"/>
    </div>
    <div>
        <asp:Image runat="server" ID="Id1" ImageUrl="ImageHandler.ashx?Id={1}"/>
	</div>
    </form>
</body>
</html>
