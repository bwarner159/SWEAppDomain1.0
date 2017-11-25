<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewPictures.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PhotoSec</title>
</head>
<body>
    <h1>Your Pictures</h1>
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
	<div align="right">
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
</body>
</html>
