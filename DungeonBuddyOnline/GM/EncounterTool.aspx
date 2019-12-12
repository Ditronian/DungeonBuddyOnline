<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EncounterTool.aspx.cs" Inherits="WebSite_EncounterTool" MasterPageFile="~/Main.master"%>


<asp:Content runat="server" ContentPlaceHolderID="HeadPlaceHolder">
    <link rel="stylesheet" type="text/css" href="../CSS/ObjectTable.css" />
    <link rel="stylesheet" type="text/css" href="../CSS/EncounterTool.css" />
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
                <h1 class="pageHeader">Encounter Tool</h1>
                <h3 class="pageHeader"><asp:Label ID="encounterNameLabel"  runat="server" Text="Encounter Name: "></asp:Label></h3>
                <asp:PlaceHolder ID="CombatTablePlaceHolder" runat="server"></asp:PlaceHolder>
                <table id="addTable" class="centerTable">
                    <tr>
                        <td class="centered">Name</td>
                        <td class="centered">Initiative</td>
                        <td class="centered">Current HP</td>
                        <td class="centered">Max HP</td>
                        <td class="centered">Armor Class</td>
                    </tr>
                    <tr>
                        <td><asp:TextBox ID="monsterNameTextBox" runat="server"></asp:TextBox></td>
                        <td><asp:TextBox ID="monsterInitiativeTextBox" runat="server" TextMode="Number"></asp:TextBox></td>
                        <td><asp:TextBox ID="monsterCurrentHPTextBox" runat="server" TextMode="Number"></asp:TextBox></td>
                        <td><asp:TextBox ID="monsterMaxHPTextBox" runat="server" TextMode="Number"></asp:TextBox></td>
                        <td><asp:TextBox ID="monsterACTextBox" runat="server" TextMode="Number"></asp:TextBox></td>
                        <td><asp:Button ID="monsterButton" runat="server" Text="Add Monster" OnClick="monsterButton_Click" /></td>
                    </tr>
                        <tr>
                        <td><asp:TextBox ID="friendlyNameTextBox" runat="server"></asp:TextBox></td>
                        <td><asp:TextBox ID="friendlyInitiativeTextBox" runat="server" TextMode="Number"></asp:TextBox></td>
                        <td><asp:TextBox ID="friendlyCurrentHPTextBox" runat="server" TextMode="Number"></asp:TextBox></td>
                        <td><asp:TextBox ID="friendlyMaxHPTextBox" runat="server" TextMode="Number"></asp:TextBox></td>
                        <td><asp:TextBox ID="friendlyACTextBox" runat="server" TextMode="Number"></asp:TextBox></td>
                        <td><asp:Button ID="friendlyButton" runat="server" Text="Add Friendly" OnClick="friendlyButton_Click" /></td>
                    </tr>
                </table>
                <p></p>
                <div>
                    <asp:DropDownList ID="encounterDropDownList" CssClass="standAloneDropdown" runat="server"></asp:DropDownList>
                    <asp:Button ID="newButton" CssClass="standAloneButton" runat="server" Text="New Encounter" OnClick="newButton_Click" />
                    <asp:Button ID="loadButton" CssClass="standAloneButton" runat="server" Text="Load Encounter" OnClick="loadButton_Click" />
                    <asp:Button ID="saveButton" CssClass="standAloneButton" runat="server" Text="Save Encounter" OnClick="saveButton_Click" OnClientClick="return getEncounterName()" />
                    <asp:Button ID="deleteButton" BackColor="Firebrick" CssClass="standAloneButton" runat="server" Text="Delete Encounter" OnClick="deleteButton_Click" />
                    <p></p>
                    <asp:Label ID="angryLabel" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <asp:HiddenField ID="hiddenEncounterName" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="hiddenSortColumn" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="hiddenSortDir" runat="server"></asp:HiddenField>
        </div>
        <script type="text/javascript" src="<%=ResolveUrl("~/Javascript/ObjectTable.js") %>"></script>
    </asp:Content>

