<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NPCTool.aspx.cs" Inherits="WebSite_NPCTool" MasterPageFile="~/Main.master"%>


<asp:Content runat="server" ContentPlaceHolderID="HeadPlaceHolder">
    <link rel="stylesheet" type="text/css" href="../CSS/NPCTool.css"" />
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
                <h1 class="pageHeader">NPC Tool</h1>
                <section id="twoPartDivs">
                    <div id="leftDiv">
                        <table id="addTable" class="centerTable">
                            <tr>
                                <td>Sex:</td>
                                <td><asp:DropDownList ID="sexDropDown" CssClass="fullWidth" runat="server"><asp:ListItem>Male</asp:ListItem><asp:ListItem>Female</asp:ListItem></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td>Race</td>
                                <td><asp:DropDownList ID="raceDropDown" CssClass="fullWidth" runat="server">
                                        <asp:ListItem>Human</asp:ListItem>
                                        <asp:ListItem>Elven</asp:ListItem>
                                        <asp:ListItem>Dwarven</asp:ListItem>
                                        <asp:ListItem>Halfling</asp:ListItem>
                                        <asp:ListItem>Gnomish</asp:ListItem>
                                        <asp:ListItem>Gnoll</asp:ListItem>
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td>First Name:</td>
                                <td><asp:TextBox ID="firstNameTextBox" CssClass="fullWidth" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Last Name:</td>
                                <td><asp:TextBox ID="lastNameTextBox" CssClass="fullWidth" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Appearance:</td>
                                <td><asp:TextBox ID="appearanceTextBox" CssClass="fullWidth" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Mannerism:</td>
                                <td><asp:TextBox ID="mannerismTextBox" CssClass="fullWidth" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Flaw/Secret:</td>
                                <td><asp:TextBox ID="flawTextBox" CssClass="fullWidth" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Interaction Style:</td>
                                <td><asp:TextBox ID="interactionTextBox" CssClass="fullWidth" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Talent:</td>
                                <td><asp:TextBox ID="talentTextBox" CssClass="fullWidth" runat="server"></asp:TextBox></td>
                            </tr>
                        </table>

                        <p></p>
                        <div id="buttonDiv">
                            <table>
                                <tr>
                                    <td><asp:Button ID="rollFirstNameButton" runat="server" Text="Roll First Name" CssClass="largeButton fullWidth" OnClick="rollFirstNameButton_Click"/></td>
                                    <td><asp:Button ID="rollLastNameButton" runat="server" Text="Roll Last Name" CssClass="largeButton fullWidth" OnClick="rollLastNameButton_Click"/></td>
                                    <td><asp:Button ID="rollFullNameButton" runat="server" Text="Roll Full Name" CssClass="largeButton fullWidth" OnClick="rollFullNameButton_Click"/></td>
                                </tr>
                                <tr>
                                    <td><asp:Button ID="rollAppearanceButton" runat="server" Text="Roll Appearance" CssClass="largeButton fullWidth" OnClick="rollAppearanceButton_Click"/></td>
                                    <td><asp:Button ID="rollMannerismButton" runat="server" Text="Roll Mannerism" CssClass="largeButton fullWidth" OnClick="rollMannerismButton_Click"/></td>
                                    <td><asp:Button ID="rollInteractionButton" runat="server" Text="Roll Interaction" CssClass="largeButton fullWidth" OnClick="rollInteractionButton_Click"/></td>
                                </tr>
                                <tr>
                                    <td><asp:Button ID="rollFlawButton" runat="server" Text="Roll Flaw/Secret" CssClass="largeButton fullWidth" OnClick="rollFlawButton_Click"/></td>
                                    <td><asp:Button ID="rollTalentButton" runat="server" Text="Roll Talent" CssClass="largeButton fullWidth" OnClick="rollTalentButton_Click"/></td>
                                    <td><asp:Button ID="rollAllButton" runat="server" Text="Roll All Traits" CssClass="largeButton fullWidth" OnClick="rollAllButton_Click"/></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div id="rightDiv">
                        <asp:TextBox ID="biographyTextBox" CssClass="bigBoiBox" runat="server" TextMode="MultiLine"></asp:TextBox>
                        <br />
                        <span class="center">Biography</span>
                    </div>
                </section>
                <p></p>
                <span>Location: </span><asp:TextBox ID="locationTextBox" runat="server"></asp:TextBox>
                <p></p>
                <asp:Button ID="saveButton" CssClass="largeButton" BackColor="DarkOliveGreen" runat="server" Text="Save NPC" OnClick="saveButton_Click"/>
                <asp:Button ID="rollEntireNpcButton" CssClass="largeButton" runat="server" Text="Roll Entire NPC" OnClick="rollEntireNpcButton_Click"/>
                <p></p>
                <asp:Label ID="angryLabel" runat="server" Text="&nbsp;"></asp:Label>
            </div>
        </div>
</asp:Content>