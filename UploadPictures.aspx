<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadPictures.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PhotoSec</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Upload Your Pictures!</h1>
        <p>Browse File Directory and Submit Picture</p>
        <asp:FileUpload ID="FileUpload1" runat="server" /> </br>
        <asp:Image ID="Image1" runat="server" /> </br>
        <asp:Button ID="Button2" runat="server" Text="Submit" />
    </div>
    </form>
</body>
</html>
