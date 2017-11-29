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
	</br>
	<div>
        <asp:TextBox ID="downloadTxt" runat="server" ></asp:TextBox>
        <asp:Button ID="downloadButton" runat="server" Text="Download" OnClick="DownloadPicture" />
    </div>
    <div>
        <asp:TextBox ID="deleteTxt" runat="server" ></asp:TextBox>
        <asp:Button ID="deleteButton" runat="server" Text="Delete" Width="107px" OnClick="DeletePicture" /></br>
        <asp:Label ID="downloadLbl" runat="server"></asp:Label>
        <asp:Label ID="deleteLbl" runat="server"></asp:Label>
	</div>
    </div>
    </form>
</body>
</html>
