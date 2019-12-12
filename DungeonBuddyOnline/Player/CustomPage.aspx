<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomPage.aspx.cs" Inherits="Player_CustomPage" MasterPageFile="~/Main.master"%>

<asp:Content runat="server" ContentPlaceHolderID="HeadPlaceHolder">
    <link rel="stylesheet" type="text/css" href="../CSS/CustomPage.css" />
    <base target="_parent"/>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="BodyPlaceHolder">
    <div>
        <asp:PlaceHolder ID="ObjectPlaceHolder" runat="server"></asp:PlaceHolder>
    </div>
</asp:Content>