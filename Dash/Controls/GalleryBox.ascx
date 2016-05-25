<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GalleryBox.ascx.cs" Inherits="Dash.Controls.GalleryBox" %>

<asp:Panel ID="DivMain" class="panel panel-blue" runat="server">
    <div id="title" class="boxTile">
        <asp:Label ID="lblGalleryName" runat="server" CssClass="labelGalleryName" Text="Label"></asp:Label>
    </div>

    <asp:Panel ID="DivHead" class="panel-heading" runat="server">

        <div class="panel-info">

            <%--            <div class="panel-headline">1.0.0</div>
            <div class="panel-subhead">WooDrill Version</div>--%>
        </div>
    </asp:Panel>

    <div class="panel-footer">
        <div class="footer-text">
            <%--<asp:Image ID="imgIcon" class="imageIcon" runat="server" ImageUrl="~/Images/DashBoard.png" />--%>

            <asp:Image ID="ImgIcon" CssClass="imageIcon" runat="server" />

            <asp:HyperLink ID="hLink" runat="server">Gallery details  <i class="fa fa-arrow-circle-right"></i> </asp:HyperLink>
        </div>
    </div>
</asp:Panel>