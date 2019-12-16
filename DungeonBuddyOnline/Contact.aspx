<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Contact.aspx.cs" Inherits="Contact" MasterPageFile="~/Main.master"%>

<asp:Content runat="server" ContentPlaceHolderID="HeadPlaceHolder">
    <link rel="stylesheet" type="text/css" href="CSS/Contact.css" />
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="BodyPlaceHolder">
        <section id="topBar">
            <div id="topBarLeft">
                <span id="logoSpan"> <label>DungeonBuddy Online</label></span>
            </div>
            <div id="topBarRight">
                <span id="gameNameLabel">Contact</span>
            </div>
        </section>

        <div id="centerDiv" class="highlightDiv">
            <div id="innerDiv">
                <h1 class="pageHeader">Contact Admin</h1>
                <table id ="contactTable">
                    <tr>
                        <td>Contact Type: </td>
                        <td><asp:DropDownList ID="contactTypeList" CssClass="fullWidth" runat="server">
                            <asp:ListItem>Bug Report</asp:ListItem>
                            <asp:ListItem>Feature Request</asp:ListItem>
                            <asp:ListItem>Other</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                </table>
                <br />
                <h3 class="center">Message</h3>
                <asp:TextBox ID="messageTextField" CssClass="textField" runat="server" TextMode="MultiLine"></asp:TextBox>
                <p></p>
                <asp:Button ID="sendMessageButton" CssClass="largeButton" runat="server" Text="Send Message" OnClick="sendMessageButton_Click" />
                <p></p>
                <asp:Label ID="angryLabel" runat="server" Text="&nbsp;"></asp:Label>
            </div>
        </div>
</asp:Content>