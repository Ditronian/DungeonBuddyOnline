<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JoinGame.aspx.cs" Inherits="WebSite_JoinGame" MasterPageFile="~/Main.master"%>


<asp:Content runat="server" ContentPlaceHolderID="HeadPlaceHolder">
    <link rel="stylesheet" type="text/css" href="CSS/JoinGame.css" />
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="BodyPlaceHolder">
        <section id="topBar">
            <div id="topBarLeft">
                <span id="logoSpan"> <label>DungeonBuddy Online</label></span>
            </div>
            <div id="topBarRight">
                <span id="gameNameLabel">Join Game</span>
            </div>
        </section>
        <p></p>
        <p></p>
        <div id="centerDiv" class="highlightDiv">
            <div id="innerDiv">
                <h1 class="pageHeader">Join Game</h1>
                <asp:PlaceHolder ID="JoinGameTablePlaceHolder" runat="server"></asp:PlaceHolder>
                <p></p>
                <table id="requestTable">
                    <tr>
                        <td>Character Name:</td>
                        <td><asp:TextBox ID="nameTextBox" CssClass="roundTextBox" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Race:</td>
                        <td><asp:TextBox ID="raceTextBox" CssClass="roundTextBox" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Passive Perception:</td>
                        <td><asp:TextBox ID="perceptionTextBox" CssClass="roundTextBox" runat="server" TextMode="Number"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Hitpoints:</td>
                        <td><asp:TextBox ID="hpTextBox" CssClass="roundTextBox" runat="server" TextMode="Number"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Size (ie S or M):</td>
                        <td><asp:TextBox ID="sizeTextBox" CssClass="roundTextBox" runat="server" MaxLength="1"></asp:TextBox></td>
                    </tr>
                </table>
                <asp:Button ID="submitJoinButton" CssClass="largeButton" runat="server" Text="Submit Join Request" OnClick="submitJoinButton_Click" />
                <p></p>
                <asp:Label ID="angryLabel" runat="server" Text="&nbsp;"></asp:Label>
            </div>
        </div>
</asp:Content>