<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateGame.aspx.cs" Inherits="WebSite_CreateGame" MasterPageFile="~/Main.master"%>

<asp:Content runat="server" ContentPlaceHolderID="HeadPlaceHolder">
    <link rel="stylesheet" type="text/css" href="CSS/CreateGame.css" />
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="BodyPlaceHolder">
        <section id="topBar">
            <div id="topBarLeft">
                <span id="logoSpan"> <label>DungeonBuddy Online</label></span>
            </div>
            <div id="topBarRight">
                <span id="gameNameLabel">Create Game</span>
            </div>
        </section>

        <div id="centerDiv" class="highlightDiv">
            <div id="innerDiv">
                <h1 class="pageHeader">Create New Game</h1>
                <table>
                    <tr>
                        <td>Game Name: </td>
                        <td><asp:TextBox ID="gameNameTextBox" CssClass="roundTextBox" ValidationGroup="validate" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="2"><asp:RequiredFieldValidator ID="gameNameFieldValidator" CssClass="margin" ForeColor="Red" runat="server" ErrorMessage="You must enter a game name." ControlToValidate="gameNameTextBox" ValidationGroup="validate"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>Game Setting: </td>
                        <td><asp:TextBox ID="gameSettingTextBox" CssClass="roundTextBox" ValidationGroup="validate" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="2"><asp:RequiredFieldValidator ID="gameSettingFieldValidator" CssClass="margin" ForeColor="Red" runat="server" ErrorMessage="You must enter a game setting." ControlToValidate="gameSettingTextBox" ValidationGroup="validate"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>Accepting Players? </td>
                        <td><asp:RadioButtonList ID="acceptPlayersRadioList" runat="server">
                            <asp:ListItem Value="true" Selected="True">Yes</asp:ListItem>
                            <asp:ListItem Value="false">No</asp:ListItem>
                            </asp:RadioButtonList></td>
                    </tr>
                    <tr>
                        <td><asp:Button ID="createGameButton" CssClass="largeButton" runat="server" Text="Create Game" ValidationGroup="validate" OnClick="createGameButton_Click"/></td>
                    </tr>

                </table>
                <br />
                <asp:Label ID="angryLabel" runat="server" Text="&nbsp;"></asp:Label>
            </div>
        </div>
</asp:Content>