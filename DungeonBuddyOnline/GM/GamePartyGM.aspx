<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GamePartyGM.aspx.cs" Inherits="WebSite_GamePartyGM" MasterPageFile="~/Main.master"%>


<asp:Content runat="server" ContentPlaceHolderID="HeadPlaceHolder">
    <link rel="stylesheet" type="text/css" href="../CSS/ObjectTable.css" />
    <link rel="stylesheet" type="text/css" href="../CSS/GamePartyGM.css" />
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="BodyPlaceHolder">
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
                <table id="addTable" class="centerTable">
                    <tr>
                        <td class="centered">Name</td>
                        <td class="centered">Race</td>
                        <td class="centered">Perception</td>
                        <td class="centered">Current HP</td>
                        <td class="centered">Max HP</td>
                        <td class="centered">Size</td>
                    </tr>
                    <tr>
                        <td><asp:TextBox ID="partyPCNameTextBox" runat="server"></asp:TextBox></td>
                        <td><asp:TextBox ID="partyPCRaceTextBox" runat="server"></asp:TextBox></td>
                        <td><asp:TextBox ID="partyPCPerceptionTextBox" runat="server" TextMode="Number"></asp:TextBox></td>
                        <td><asp:TextBox ID="partyPCCurrentHPTextBox" runat="server" TextMode="Number"></asp:TextBox></td>
                        <td><asp:TextBox ID="partyPCMaxHPTextBox" runat="server" TextMode="Number"></asp:TextBox></td>
                        <td><asp:TextBox ID="partyPCSizeTextBox" runat="server" MaxLength="1"></asp:TextBox></td>
                        <td><asp:Button ID="addPartyPCButton" runat="server" Text="Add PC" OnClick="addPartyPCButton_Click" /></td>
                    </tr>
                    <tr>
                        <td><asp:TextBox ID="partyNPCNameTextBox" runat="server"></asp:TextBox></td>
                        <td><asp:TextBox ID="partyNPCRaceTextBox" runat="server"></asp:TextBox></td>
                        <td><asp:TextBox ID="partyNPCPerceptionTextBox" runat="server" TextMode="Number"></asp:TextBox></td>
                        <td><asp:TextBox ID="partyNPCCurrentHPTextBox" runat="server" TextMode="Number"></asp:TextBox></td>
                        <td><asp:TextBox ID="partyNPCMaxHPTextBox" runat="server" TextMode="Number"></asp:TextBox></td>
                        <td><asp:TextBox ID="partyNPCSizeTextBox" runat="server" MaxLength="1"></asp:TextBox></td>
                        <td><asp:Button ID="addPartyNPCButton" runat="server" Text="Add NPC" OnClick="addPartyNPCButton_Click" /></td>
                    </tr>
                </table>
                <p></p>
                <div>
                    <asp:Button ID="saveButton" CssClass="standAloneButton" BackColor="DarkOliveGreen" runat="server" Text="Save Party" OnClick="saveButton_Click" />
                    <p></p>
                    <asp:Label ID="angryLabel" runat="server" Text="&nbsp;"></asp:Label>
                </div>
                <p></p>
                <h1 class="pageHeader">Join Requests</h1>
                <asp:PlaceHolder ID="JoinRequestsPlaceholder" runat="server"></asp:PlaceHolder>
            </div>
            <asp:HiddenField ID="hiddenSortColumn" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="hiddenSortDir" runat="server"></asp:HiddenField>
        </div>
    <script type="text/javascript" src="<%=ResolveUrl("~/Javascript/ObjectTable.js") %>"></script>
    </asp:Content>