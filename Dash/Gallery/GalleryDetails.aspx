<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GalleryDetails.aspx" Inherits="Dash.Gallery.GalleryDetails" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/ToggleCheckBox.css" rel="stylesheet" />
    <link href="../Content/CircleButton.css" rel="stylesheet" />
    <style>
        @font-face {
            font-family: "GalleryTitleFont";
            src: url('../fonts/leaguespartan-bold.ttf') format('truetype');
        }

        .mainContainer {
            min-width: 1130px;
        }

        .imageMaxSize img {
            max-height: 70px;
            max-width: 120px;
        }

        .divGrid {
            margin-bottom: 20px;
            /*border-color: rgba(35, 40, 45, 0.27);
        border-style: solid;
        border-width: 1px;*/
            display: table;
            width: 97%;
            display: inline-block;
        }

        .divGridTop {
            border-color: rgba(35, 40, 45, 0.27);
            border-style: solid;
            border-width: 1px;
            min-width: 1185px;
            height: 170px;
            background-color: white;
            overflow: hidden;
            display: inline-block;
        }

        .RadGrid .Row50 td {
            height: 50px;
            vertical-align: middle;
        }

        .divCirclePerc {
            margin-top: 4px;
            float: right;
            padding-bottom: 10px;
            margin-right: 10px;
        }

        .divLastMonthChart {
            margin-top: 12px;
            float: right;
        }

        .divDetails {
            float: left;
        }

        .GalleryTitle {
            display: inline-block;
            text-decoration: none;
            font-family: "GalleryTitleFont", Verdana, Tahoma;
            letter-spacing: -2px;
            text-align: left;
            color: #4A3B3B;
            font-size: 30px;
            margin-top: 5px;
            margin-left: 10px;
            float: left;
        }

        .TitleButtons {
            float: left;
            display: inline-block;
            margin-left: 20px;
            margin-top: 4px;
        }

        .LeftAlign {
            float: left;
            display: inline-block;
        }

        .OptionTitle {
            padding-top: 9px;
            margin-right: 3px;
            font-size: 12px;
            letter-spacing: -.045em;
            font-family: "GalleryTitleFont", Verdana, Tahoma;
            display: inline-block;
            float: left;
        }

        .OptionsContainer {
            width: 250px;
            font-size: 12px;
            font-weight: normal;
            font-family: "Hammersmith One", sans-serif;
            letter-spacing: 0;
        }

        .detailsTitle {
            font-family: "GalleryTitleFont", Verdana, Tahoma;
            font-size: 10px;
            DISPLAY: INLINE-BLOCK;
            WIDTH: 145px;
            text-align: left;
        }

        .detailsValue {
            font-family: "GalleryTitleFont", Verdana, Tahoma;
            font-size: 10px;
            DISPLAY: INLINE-BLOCK;
            WIDTH: 77px;
            text-align: right;
        }

        .pchContainer {
            width: 132px;
            height: 152px;
            background-color: black;
            border-radius: 62px 62px 5px 5px;
            margin-top: 5px;
            background-color: #C6E1F9;
            border-color: #C3BABA;
            border-width: 1px;
            border-style: solid;
            display: inline-block;
            -webkit-box-shadow: 1px 1px 3px 0px rgba(0,0,0,0.35);
            -moz-box-shadow: 1px 1px 3px 0px rgba(0,0,0,0.35);
            box-shadow: 1px 1px 3px 0px rgba(0,0,0,0.35);
            margin-left: 10px;
        }

        .pchDetails {
            display: none;
        }

        .pchFooter {
            height: 20px;
            background-color: rgba(255, 255, 255, 0.79);
            bottom: 5px;
            position: relative;
            border-radius: 0px 0px 5px 5px;
            text-align: center;
            font-variant: small-caps;
        }
    </style>
    <script type="text/javascript">

        function doAjaxPostBack() {
            setTimeout("__doPostBack('ctl00_CPC_RadGridList', '')", '0');
        }
    </script>
    <%--<telerik:RadScriptManager ID="RadScriptManagerList" runat="server"></telerik:RadScriptManager>--%>
    <div class="mainContainer">

        <div class="divGrid divGridTop">
            <div class="divDetails">
                <div class="GalleryTitle">
                    <asp:Label ID="lblGalleryName" runat="server" Text=""></asp:Label>

                    <div class="OptionsContainer">
                        <asp:PlaceHolder ID="GalleryInfo" runat="server"></asp:PlaceHolder>

                        <div class="OptionTitle">
                            Expired Coupons
                        </div>

                        <div class="divToggleButton LeftAlign">
                            <asp:CheckBox ID="chkToggleButton" runat="server" onclick="doAjaxPostBack();" OnCheckedChanged="chkToggleButton_CheckedChanged" Checked="True" />
                            <asp:Label ID="lblToggleButton"
                                AssociatedControlID="chkToggleButton" runat="server"
                                ToolTip="Toggle between full list and active list" />
                        </div>
                    </div>

                    <span class="detailsTitle"></span>
                    <span class="detailsValue"></span>
                </div>

                <span class="TitleButtons">

                    <ul class="Menu">
                        <li>
                            <asp:HyperLink ID="hlredemptStat" class="round green" runat="server">
                                Redemption Statistics<span class="round">CHECK REDEMPTION STATISTICS</span>
                            </asp:HyperLink>
                        </li>
                        <li>
                            <asp:HyperLink ID="hlprintsStat" class="round red" runat="server">
                                Prints Statistics<span class="round">CHECK<BR />PRINTS STATISTICS</span>
                            </asp:HyperLink>
                        </li>
                    </ul>
                </span>
            </div>

            <div class="divCirclePerc">
                <asp:PlaceHolder ID="circlePercentage" runat="server"></asp:PlaceHolder>
            </div>

            <div class="divLastMonthChart">
                <span class="spark">
                    <telerik:RadHtmlChart ID="LastMonthChart" runat="server" Width="450px" Skin="Bootstrap" Height="170px">

                        <PlotArea>
                            <Series>
                                <telerik:AreaSeries DataFieldY="PrintSuccessful">
                                    <MarkersAppearance Visible="False" />
                                    <LabelsAppearance Visible="False">
                                    </LabelsAppearance>
                                    <TooltipsAppearance ClientTemplate="#=dataItem.ttDesc#">
                                    </TooltipsAppearance>
                                </telerik:AreaSeries>
                            </Series>

                            <XAxis DataLabelsField="cDate" Width="0px" MinorTickSize="0px">
                                <LabelsAppearance RotationAngle="0" Visible="False" />
                                <MajorGridLines Visible="false" />
                                <MinorGridLines Visible="false" />
                            </XAxis>
                            <YAxis Width="0px" MinorTickSize="0px" MajorTickSize="0px">
                                <LabelsAppearance RotationAngle="0" Visible="False" />
                                <MajorGridLines Visible="false" />
                                <MinorGridLines Visible="false" />
                            </YAxis>
                        </PlotArea>
                    </telerik:RadHtmlChart>
                </span>
            </div>
        </div>

        <div class="divGrid">

            <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">

                <telerik:RadGrid ID="RadGridList" runat="server" AllowPaging="True" AllowSorting="True" OnNeedDataSource="RadGridList_NeedDataSource" Skin="Bootstrap" AutoGenerateColumns="False" PageSize="6" OnItemDataBound="RadGridList_ItemDataBound" ShowStatusBar="True">

                    <GroupingSettings CollapseAllTooltip="Collapse all groups" />

                    <ExportSettings>
                        <Pdf PageWidth="">
                        </Pdf>
                    </ExportSettings>

                    <ClientSettings EnableRowHoverStyle="True">
                        <Scrolling UseStaticHeaders="True" />
                    </ClientSettings>

                    <AlternatingItemStyle Font-Names="Open Sans" Font-Size="12px" />

                    <MasterTableView Font-Names="Arial" Font-Size="10px">

                        <Columns>
                            <telerik:GridBoundColumn DataField="Id" FilterControlAltText="Filter column column" HeaderText="Id" UniqueName="Id">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>

                            <telerik:GridImageColumn DataImageUrlFields="product_img_url" FilterControlAltText="" ImageHeight="" ImageWidth="" UniqueName="image" Resizable="False" ShowFilterIcon="False" ShowSortIcon="False" ItemStyle-CssClass="imageMaxSize" ImageAlign="Middle" HeaderText="Image">
                                <ItemStyle CssClass="imageMaxSize" Width="150px" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </telerik:GridImageColumn>

                            <telerik:GridBoundColumn DataField="description_gallery" FilterControlAltText="" HeaderText="Name" UniqueName="name">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="250px" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="MACA_DESC" FilterControlAltText="" HeaderText="Mfr" UniqueName="MACA" Visible="False">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="250px" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="start_date" FilterControlAltText="" DataFormatString="{0:dd/MM/yy}" HeaderText="Start Date" UniqueName="start_date" DataType="System.DateTime">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="end_date" FilterControlAltText="" DataFormatString="{0:dd/MM/yy}" HeaderText="End Date" UniqueName="end_date" DataType="System.DateTime">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="publish_end_date" FilterControlAltText="" DataFormatString="{0:dd/MM/yy}" HeaderText="Gallery end date" UniqueName="publish_end_date" DataType="System.DateTime">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="individual_limit" FilterControlAltText="" DataFormatString="{0:#,#;-#,#;0}" HeaderText="Limit" UniqueName="individual_limit">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="cPrintSuccessful_Today" FilterControlAltText="" DataFormatString="{0:#,#;-#,#;0}" HeaderText="Today prints" UniqueName="cPrintSuccessful_Today">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="cPrintSuccessful_Total" FilterControlAltText="" DataFormatString="{0:#,#;-#,#;0}" HeaderText="Total prints" UniqueName="cPrintSuccessful_Total">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="VersoRedeemedToday" FilterControlAltText="" DataFormatString="{0:#,#;-#,#;0}" HeaderText="Verso Today" UniqueName="VersoRedeemedToday">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="VersoRedeemedHistory" FilterControlAltText="" DataFormatString="{0:#,#;-#,#;0}" HeaderText="Est Redempt" UniqueName="VersoRedeemedHistory">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="bud_percen" FilterControlAltText="" DataFormatString="{0:#,#;-#,#;0}" HeaderText="Budget %" UniqueName="bud_percen">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="perc" FilterControlAltText="" HeaderText="Fill Rate" UniqueName="PercColumn">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="daysLeft" FilterControlAltText="" HeaderText="Days Left" UniqueName="daysLeft">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="StatColumn" FilterControlAltText="" HeaderText="Statistis" UniqueName="StatColumn">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>

                    <HeaderStyle Font-Names="Open Sans" Font-Size="12px" />
                    <ItemStyle Font-Names="Open Sans" Font-Size="12px" />
                </telerik:RadGrid>
            </telerik:RadAjaxPanel>

            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
            </telerik:RadAjaxLoadingPanel>
        </div>
    </div>
</asp:Content>