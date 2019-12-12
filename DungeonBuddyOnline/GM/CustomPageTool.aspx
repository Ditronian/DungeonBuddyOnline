<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomPageTool.aspx.cs" Inherits="WebSite_CustomPageTool" MasterPageFile="~/Main.master"%>


<asp:Content runat="server" ContentPlaceHolderID="HeadPlaceHolder">
    <link rel="stylesheet" type="text/css" href="../CSS/ObjectTable.css" />
    <link rel="stylesheet" type="text/css" href="../CSS/CustomPageTool.css" />
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
                <h1 class="pageHeader">Custom Page Tool</h1>
                <asp:PlaceHolder ID="PageTablePlaceHolder" runat="server"></asp:PlaceHolder>
                <p></p>
                <div id="addPageDiv">
                    <table>
                        <tr>
                            <td>Page Name</td>
                        </tr>
                        <tr>
                            <td><asp:TextBox ID="pageNameTextBox" CssClass="fullWidth" runat="server"></asp:TextBox></td>
                            <td><asp:FileUpload ID="PageUploader" runat="server" /></td>
                        </tr>
                    </table>
                </div>
                <p></p>
                <div id="miscDiv">
                    <asp:Button ID="saveButton" CssClass="standAloneButton" runat="server" Text="Save Pages" OnClick="saveButton_Click" />
                    <asp:Button ID="uploadButton" CssClass="standAloneButton" runat="server" Text="Upload" OnClick="uploadButton_Click" />
                </div>
                <p></p>
                <asp:Label ID="angryLabel" runat="server" Text=""></asp:Label>
            </div>
        </div>
</asp:Content>