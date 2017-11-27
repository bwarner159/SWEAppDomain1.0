<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YourPictures.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="float:left">
        <img src="C:\Users\Brett\Pictures\animatedlock.jpg" width="50" height="50"/>
    </div>
    <div style="display:inline-block">
        <h1>Your Pictures</h1>
    </div>
    <div>
	<div>
		<img id="picture" src="C:\Users\Brett\Pictures\petland.jpg" />
		-FileName </br>
		-Date </br>
		-Size		
	</div>
	</br>
	<div>
        <asp:Button ID="downloadButton" runat="server" Text="Download" OnClick="DownloadPicture" />
        <asp:Button ID="deleteButton" runat="server" Text="Delete" />
	</div>
    </div>
    </form>
</body>
</html>
