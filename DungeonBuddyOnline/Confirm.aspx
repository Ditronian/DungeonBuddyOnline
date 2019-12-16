<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Confirm.aspx.cs" Inherits="Confirm" %>

<!DOCTYPE html>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/SubWindow.css" />
    <link rel="stylesheet" type="text/css" href="CSS/Contact.css" />
    <link rel="stylesheet" type="text/css" href="CSS/Master.css" />
</head>
<body id="body1">
    <form id="form1" runat="server">
        <section id="topBar">
            <div id="topBarLeft">
                <span id="logoSpan"> <label>DungeonBuddy Online</label></span>
            </div>
            <div id="topBarRight">
                <span id="gameNameLabel">Confirmation</span>
            </div>
        </section>
        <p></p>
        <p></p>
        <div id="centerDiv" class="highlightDiv">
            <div id="innerDiv">
                <h1 class="pageHeader">Confirmation</h1>
                <br />
                <asp:Label ID="angryLabel" runat="server" Text="&nbsp;"></asp:Label>
                <p></p>
                <a href="https://dungeonbuddy.net">Return to Login</a>
            </div>
        </div>
    </form>
</body>
</html>
