<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NPCTracker.aspx.cs" Inherits="WebSite_GM_NPCTracker" MasterPageFile="~/Main.master"%>


<asp:Content runat="server" ContentPlaceHolderID="HeadPlaceHolder">
    <link rel="stylesheet" type="text/css" href="../CSS/NPCTracker.css" />
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

        <div id="centerDiv" class="highlightDiv">
            <div id="innerDiv">
                <h1 class="pageHeader">NPC Tracker</h1>
                <section id="twoPartDivs">
                    <div id="leftDiv">
                        <asp:PlaceHolder ID="NPCTablePlaceHolder" runat="server"></asp:PlaceHolder>
                    </div>
                    <div id="rightDiv">
                        <table>
                            <tr>
                                <td><span class="floatLeft">Name: </span></td>
                                <td><asp:TextBox ID="nameTextBox" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td><span class="floatLeft">Sex: </span></td>
                                <td><asp:TextBox ID="sexTextBox" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td><span class="floatLeft">Race: </span></td>
                                <td><asp:TextBox ID="raceTextBox" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td><span class="floatLeft">Location: </span></td>
                                <td><asp:TextBox ID="locationTextBox" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td><span class="floatLeft">Appearance: </span></td>
                                <td><asp:TextBox ID="appearanceTextBox" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td><span class="floatLeft">Mannerism: </span></td>
                                <td><asp:TextBox ID="mannerismTextBox" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td><span class="floatLeft">Interaction Style: </span></td>
                                <td><asp:TextBox ID="interactionTextBox" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td><span class="floatLeft">Flaw/Secret: </span></td>
                                <td><asp:TextBox ID="flawTextBox" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td><span class="floatLeft">Talent: </span></td>
                                <td><asp:TextBox ID="talentTextBox" runat="server"></asp:TextBox></td>
                            </tr>
                        </table>
                        <br />
                        <asp:TextBox ID="bioTextBox" runat="server" CssClass="bigBoiBox" TextMode="MultiLine"></asp:TextBox>
                        <br />
                        <span>Biography</span>
                    </div>
                </section>
                <p></p>
                <asp:Button ID="saveButton" CssClass="largeButton" runat="server" Text="Save NPC" OnClick="saveButton_Click"/>
                <asp:Button ID="deleteButton" BackColor="Firebrick" CssClass="largeButton" runat="server" Text="Delete NPC" OnClick="deleteButton_Click"/>
                <p></p>
                <asp:Label ID="angryLabel" runat="server" Text=""></asp:Label>
            </div>
            <asp:HiddenField ID="hiddenSortColumn" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="hiddenSortDir" runat="server"></asp:HiddenField>
        </div>
    <script type="text/javascript" src="<%=ResolveUrl("~/Javascript/ObjectTable.js") %>"></script>
    </asp:Content>
        