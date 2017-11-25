<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YourPictures.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1>Your Pictures</h1>
	<div>
		<img src="blah">
		-FileName </br>
		-Date </br>
		-Size		
	</div>
	</br>
	<div>
        <asp:Button ID="Button1" runat="server" Text="Download" />
        <asp:Button ID="Button2" runat="server" Text="Delete" />
	</div>
    </div>
    </form>
</body>
</html>
