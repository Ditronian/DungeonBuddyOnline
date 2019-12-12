<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GameInformation.aspx.cs" Inherits="WebSite_GameInformation" MasterPageFile="~/Main.master"%>


<asp:Content runat="server" ContentPlaceHolderID="HeadPlaceHolder">
    <link rel="stylesheet" type="text/css" href="../CSS/GameInformation.css" />
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
                <h1 class="pageHeader">Game Information</h1>

                <div id="topDiv">
                    <div id="leftTop">
                        <br />
                        <asp:Image ID="gameImage" runat="server" />
                    </div>
                    <div id="rightTop">
                        <h3 class="center">Game Description</h3>
                        <asp:TextBox ID="descriptionTextBox" CssClass="textField" ReadOnly="true" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
                <br />

                <div id="buttonDiv">
                    <div id="buttonLeft">
                    </div>
                    <div id="buttonRight">
                    </div>
                </div>
                <asp:Label ID="angryLabel" runat="server" Text=""></asp:Label>
                <div id="middleDiv">
                    <h3 class="center">Additional Information</h3>
                    <asp:TextBox ID="additionalTextBox" CssClass="textField" runat="server" ReadOnly="true" TextMode="MultiLine"></asp:TextBox>
                </div>
                <br />
                <div id="bottomDiv">
                    <div id="leftBottom">
                        <table>
                            <tr>
                                <td>Game Name: </td><td><asp:TextBox ID="nameTextBox" ReadOnly="true" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Game Setting: </td><td><asp:TextBox ID="settingTextBox" ReadOnly="true" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td><asp:Label ID="angryLabelBottom" runat="server" Text=""></asp:Label></td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>