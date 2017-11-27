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
    </div>
    <div>
        <asp:Button runat="server" ID="goToUploadPg" Text="Go to Upload Page" OnClick="GoToUpload"/>
    </div>
    <div>
        <asp:Image runat="server" ID="Id1" ImageUrl="ViewPictures.aspx.cs?Id=1"/>
        <asp:Image runat="server" ID="Id2" ImageUrl="ViewPictures.aspx.cs?Id=2"/>
        <asp:Image runat="server" ID="Id3" ImageUrl="ViewPictures.aspx.cs?Id=3"/>
        <asp:Image runat="server" ID="Id4" ImageUrl="ViewPictures.aspx.cs?Id=4"/>
        <asp:Image runat="server" ID="Id5" ImageUrl="ViewPictures.aspx.cs?Id=5"/>
	</div>
    </form>
</body>
</html>
