<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Dash.Gallery.List" MasterPageFile="~/Site.Master" Title="Gallery List" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <link href="../Content/GalleryBox.css" rel="stylesheet" />

    <div id="divBoxes">
        <asp:PlaceHolder ID="PlaceHolderBoxes" runat="server"></asp:PlaceHolder>
    </div>
</asp:Content>