<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GameParty.aspx.cs" Inherits="WebSite_GameParty" MasterPageFile="~/Main.master" %>

<asp:Content runat="server" ContentPlaceHolderID="HeadPlaceHolder">
    <link rel="stylesheet" type="text/css" href="../CSS/ObjectTable.css" />
    <link rel="stylesheet" type="text/css" href="../CSS/GamePartyPlayer.css" />
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="BodyPlaceHolder" onLoad="resort(0)">
        <section id="topBar">
            <div id="topBarLeft">
                <span id="logoSpan"> <label>DungeonBuddy Online</label></span>
            </div>
            <div id="topBarRight">
                <asp:Label ID="gameNameLabel" runat="server" Text="Game Name"></asp:Label>
            </div>
        </section>
        <p></p>
        <p></p>
        <div id="centerDiv" class="highlightDiv">
            <div id="innerDiv">
                <h1 class="pageHeader">Game Party</h1>
                <asp:PlaceHolder ID="PartyTablePlaceHolder" runat="server"></asp:PlaceHolder>
                <p></p>
                <div>
                    <asp:Button ID="saveButton" BackColor="DarkOliveGreen" CssClass="standAloneButton" runat="server" Text="Save Character(s)" OnClick="saveButton_Click" />
                    <asp:Button ID="leaveGameButton" CssClass="standAloneButton" runat ="server" Text="Leave Game" OnClientClick="return confirm('Are you sure you would like to quit the game completely?')" OnClick="leaveGameButton_Click" BackColor="Firebrick" />
                    <p></p>
                    <asp:Label ID="angryLabel" runat="server" Text="&nbsp;"></asp:Label>
                </div>
            </div>
            <asp:HiddenField ID="hiddenSortColumn" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="hiddenSortDir" runat="server"></asp:HiddenField>
        </div>
    <script type="text/javascript" src="<%=ResolveUrl("~/Javascript/MyTools.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Javascript/ObjectTable.js") %>"></script>
</asp:Content>