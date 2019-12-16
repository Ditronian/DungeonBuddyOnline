<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GameInformationGM.aspx.cs" Inherits="WebSite_GameInformationGM" MasterPageFile="~/Main.master"%>


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
                        <asp:TextBox ID="descriptionTextBox" CssClass="textField" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
                <br />

                <div id="buttonDiv">
                    <div id="buttonLeft">
                        <asp:FileUpload ID="imageUploader" runat="server" />
                        <asp:Button ID="uploadButton" runat="server" Text="Upload Image" OnClick="uploadButton_Click" />
                    </div>
                    <div id="buttonRight">
                    </div>
                </div>
                <asp:Label ID="angryUploadLabel" runat="server" Text="&nbsp;"></asp:Label>
                <div id="middleDiv">
                    <h3 class="center">Additional Information</h3>
                    <asp:TextBox ID="additionalTextBox" CssClass="textField" runat="server" TextMode="MultiLine"></asp:TextBox>
                </div>
                <br />
                <div id="bottomDiv">
                    <div id="leftBottom">
                        <table>
                            <tr>
                                <td>Accepting Players?</td>
                            </tr>
                            <tr>
                                <td><asp:RadioButtonList ID="acceptingPlayersList" runat="server">
                                     <asp:ListItem>Yes</asp:ListItem>
                                     <asp:ListItem>No</asp:ListItem>
                                     </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="centerBottom">
                        <table id="nameTable">
                            <tr>
                                <td>Game Name: </td>
                                <td><asp:TextBox ID="nameTextBox" CssClass="fullWidth"  runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Game Setting: </td>
                                <td><asp:TextBox ID="settingTextBox" CssClass="fullWidth" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td colspan="2"><asp:Label ID="angryLabel" runat="server" Text="&nbsp;"></asp:Label></td>
                            </tr>
                        </table>
                    </div>
                    <div id="rightBottom">
                        <asp:Button ID="saveButton" CssClass="largeButton" BackColor="DarkOliveGreen" runat="server" Text="Save Game" OnClick="saveButton_Click"/>
                        <asp:Button ID="deleteButton" CssClass="largeButton" BackColor="Firebrick" runat="server" Text="Delete Game" OnClientClick="return confirm('Are you sure you would like to delete this game?  There is no turning back.')" OnClick="deleteButton_Click"/>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>