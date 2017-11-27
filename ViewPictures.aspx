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
    <div align="right">
        Sort By: 
        <select>
            <option value="size">Size</option>
            <option value="date">Date</option>
            <option value="name">Name</option>
        </select>
        </br>
    </div>
    <div>
		<img src="">
		-FileName</br>
		-Date</br>
		-Size
	</div>
	</br>
	<div>
		<img src="">
		-FileName </br>
		-Date </br>
		-Size
	</div>
	</br>
	<div>
		<img src="">
		-FileName</br>
		-Date</br>
		-Size
	</div>
	</br>
	<div style="display:inline" >
        <div style="float: left; width:45%">
            <asp:Button id="UploadedFiles" runat="server" Text="Upload Pictures" />
        </div>
        <div style="float: right; width: 10%;">
		    Show 
		    <select>
			    <option value="10">10</option>
			    <option value="20">20</option>
			    <option value="30">30</option>
			    <option value="40">40</option>
			    <option value="50">50</option>
		    </select>
		    Per page
        </div>
	</div>
    </form>
</body>
</html>
