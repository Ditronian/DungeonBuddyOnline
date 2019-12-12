<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MagicShopTool.aspx.cs" Inherits="GM_MagicShopTool" MasterPageFile="~/Main.master"%>


<asp:Content runat="server" ContentPlaceHolderID="HeadPlaceHolder">
    <link rel="stylesheet" type="text/css" href="../CSS/ObjectTable.css" />
    <link rel="stylesheet" type="text/css" href="../CSS/MagicShopTool.css" />
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
                <h1 class="pageHeader">Magic Shop Tool</h1>
                <br />
                <h3 class="centerControl"><asp:Label ID="shopNameLabel"  runat="server" Text="Shop Name: "></asp:Label></h3>
                <br />
                <asp:DropDownList ID="shopQualityDropDownList" CssClass="standAloneDropdown centerControl" runat="server">
                            <asp:ListItem>Shop Quality</asp:ListItem>
                            <asp:ListItem>Common</asp:ListItem>
                            <asp:ListItem>Uncommon</asp:ListItem>
                            <asp:ListItem>Rare</asp:ListItem>
                            <asp:ListItem>Very Rare</asp:ListItem>
                            <asp:ListItem>Legendary</asp:ListItem>
                            </asp:DropDownList>
                <br />
                <asp:PlaceHolder ID="ShopTablePlaceHolder" runat="server"></asp:PlaceHolder>
                <table id="addTable" class="centerTable">
                    <tr>
                        <td class="centered">Name</td>
                        <td class="centered">Rarity</td>
                        <td class="centered">Value</td>
                        <td class="centered">Minimum Value</td>
                        <td class="centered">Maximum Value</td>
                    </tr>
                    <tr>
                        <td><asp:TextBox ID="nameTextBox" runat="server"></asp:TextBox></td>
                        <td><asp:DropDownList ID="rarityDropDownList" CssClass="tableDropdown" onchange="getValues()" runat="server">
                            <asp:ListItem>Common</asp:ListItem>
                            <asp:ListItem>Uncommon</asp:ListItem>
                            <asp:ListItem>Rare</asp:ListItem>
                            <asp:ListItem>Very Rare</asp:ListItem>
                            <asp:ListItem>Legendary</asp:ListItem>
                            </asp:DropDownList></td>
                        <td><asp:TextBox ID="valueTextBox" runat="server" TextMode="Number"></asp:TextBox></td>
                        <td><asp:TextBox ID="minimumValueTextBox" runat="server" TextMode="Number"></asp:TextBox></td>
                        <td><asp:TextBox ID="maximumValueTextBox" runat="server" TextMode="Number"></asp:TextBox></td>
                        <td><asp:Button ID="itemButton" runat="server" Text="Add Item" OnClick="itemButton_Click" /></td>
                    </tr>
                </table>
                <p></p>
                <div>
                    <asp:DropDownList ID="shopDropDownList" CssClass="standAloneDropdown" runat="server"></asp:DropDownList>
                    <asp:Button ID="newButton" CssClass="standAloneButton" runat="server" Text="New Shop" OnClick="newButton_Click" />
                    <asp:Button ID="generateButton" CssClass="standAloneButton" runat="server" Text="Generate Shop" OnClick="generateButton_Click" />
                    <asp:Button ID="loadButton" CssClass="standAloneButton" runat="server" Text="Load Shop" OnClick="loadButton_Click" />
                    <asp:Button ID="saveButton" CssClass="standAloneButton" runat="server" Text="Save Shop" OnClick="saveButton_Click" OnClientClick="return getShopName()" />
                    <asp:Button ID="deleteButton" BackColor="Firebrick" CssClass="standAloneButton" runat="server" Text="Delete Shop" OnClick="deleteButton_Click" />
                    <br />
                    <asp:Label ID="angryLabel" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <asp:HiddenField ID="hiddenShopName" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="hiddenSortColumn" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="hiddenSortDir" runat="server"></asp:HiddenField>
        </div>
        <script type="text/javascript" src="<%=ResolveUrl("~/Javascript/ObjectTable.js") %>"></script>
        <script type="text/javascript" src="<%=ResolveUrl("~/Javascript/MagicShopTool.js") %>"></script>
    </asp:Content>