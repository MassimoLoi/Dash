<%@ Page Title="" Language="C#" MasterPageFile="../Site.Master" AutoEventWireup="true" CodeBehind="GalleryPrintsStat.aspx.cs" Inherits="Dash.Gallery.GalleryPrintsStat" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <link href="../Content/TabStrip.css" rel="stylesheet" />
    <script type="text/javascript" src="//cdn.jsdelivr.net/jquery/1/jquery.min.js"></script>
    <script type="text/javascript" src="//cdn.jsdelivr.net/momentjs/latest/moment.min.js"></script>

    <script type="text/javascript" src="//cdn.jsdelivr.net/bootstrap.daterangepicker/2/daterangepicker.js"></script>
    <link rel="stylesheet" type="text/css" href="//cdn.jsdelivr.net/bootstrap.daterangepicker/2/daterangepicker.css" />

    <%--<link href="../Css/bootstrap.min.date.css" rel="stylesheet" />--%>

    <script type="text/javascript">

        $.fn.exBounce = function () {
            var self = this;
            (function runEffect() {
                self.effect("bounce", { times: 2, distance: 5 }, 2000, runEffect);
            })();
            return this;
        };
    </script>

    <style>
        .mainContainer {
            width: 1040px;
            margin-left: 10px;
            margin-right: 20px;
            text-align: left;
        }

        .divAlignLeft {
            position: relative;
            float: left;
            display: inline-block;
        }

        .genderTip {
            position: absolute;
            font-style: normal;
            font-variant: normal;
            font-weight: normal;
            font-stretch: normal;
            font-size: 12px;
            line-height: normal;
            opacity: 1;
            border-radius: 4px;
            padding: 6px;
            white-space: nowrap;
            z-index: 12000;
            line-height: normal;
            background-repeat: repeat-x;
            background-position: 0 0;
            color: #fff;
        }

        .gMale {
            background-color: #5496cf;
            border: 1px solid rgb(51, 122, 183);
            color: rgb(0, 0, 0);
            margin-top: 330px;
            margin-left: 740px;
            width: 120px;
            text-align: center;
        }

        .gFemale {
            background-color: #ffb3ff;
            border: 1px solid #D6818F;
            color: rgb(0, 0, 0);
            margin-top: 330px;
            margin-left: 870px;
            width: 120px;
            text-align: center;
        }

        .jvectormap-container {
            width: 500px;
            height: 500px;
            background-color: #F5F5F5;
        }

        #cityDetails {
            width: 224px;
            height: 100px;
            position: fixed;
            top: 50px;
            right: 20px;
            color: black;
            padding: 10px;
            border: 1px solid #e3e3e3;
            border-radius: 4px;
            display: none;
            -webkit-box-shadow: 0px 0px 15px 0px rgba(50, 50, 50, 0.47);
            -moz-box-shadow: 0px 0px 15px 0px rgba(50, 50, 50, 0.47);
            box-shadow: 0px 0px 15px 0px rgba(50, 50, 50, 0.47);
            background: #d0e4f7;
            background: url(data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiA/Pgo8c3ZnIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgd2lkdGg9IjEwMCUiIGhlaWdodD0iMTAwJSIgdmlld0JveD0iMCAwIDEgMSIgcHJlc2VydmVBc3BlY3RSYXRpbz0ibm9uZSI+CiAgPGxpbmVhckdyYWRpZW50IGlkPSJncmFkLXVjZ2ctZ2VuZXJhdGVkIiBncmFkaWVudFVuaXRzPSJ1c2VyU3BhY2VPblVzZSIgeDE9IjAlIiB5MT0iMCUiIHgyPSIxMDAlIiB5Mj0iMTAwJSI+CiAgICA8c3RvcCBvZmZzZXQ9IjAlIiBzdG9wLWNvbG9yPSIjZDBlNGY3IiBzdG9wLW9wYWNpdHk9IjEiLz4KICAgIDxzdG9wIG9mZnNldD0iMjQlIiBzdG9wLWNvbG9yPSIjNzNiMWU3IiBzdG9wLW9wYWNpdHk9IjEiLz4KICAgIDxzdG9wIG9mZnNldD0iNTAlIiBzdG9wLWNvbG9yPSIjMGE3N2Q1IiBzdG9wLW9wYWNpdHk9IjEiLz4KICAgIDxzdG9wIG9mZnNldD0iNzklIiBzdG9wLWNvbG9yPSIjNTM5ZmUxIiBzdG9wLW9wYWNpdHk9IjEiLz4KICAgIDxzdG9wIG9mZnNldD0iMTAwJSIgc3RvcC1jb2xvcj0iIzg3YmNlYSIgc3RvcC1vcGFjaXR5PSIxIi8+CiAgPC9saW5lYXJHcmFkaWVudD4KICA8cmVjdCB4PSIwIiB5PSIwIiB3aWR0aD0iMSIgaGVpZ2h0PSIxIiBmaWxsPSJ1cmwoI2dyYWQtdWNnZy1nZW5lcmF0ZWQpIiAvPgo8L3N2Zz4=);
            background: -moz-linear-gradient(-45deg, #d0e4f7 0%, #73b1e7 24%, #0a77d5 50%, #539fe1 79%, #87bcea 100%);
            background: -webkit-linear-gradient(-45deg, #d0e4f7 0%,#73b1e7 24%,#0a77d5 50%,#539fe1 79%,#87bcea 100%);
            background: linear-gradient(135deg, #d0e4f7 0%,#73b1e7 24%,#0a77d5 50%,#539fe1 79%,#87bcea 100%);
            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#d0e4f7', endColorstr='#87bcea',GradientType=1 );
        }

        #map1 {
            height: 600px;
        }

        #mapHolder {
            border: none;
            padding: 0px;
            border: none;
            margin: 0px;
            overflow: hidden;
        }

        #dayhourDivCircle {
            display: inline-block;
            float: right;
            width: 100px;
            margin-right: 195px;
            margin-top: 90px;
        }

        #dayhourDivChart {
            float: left;
            display: inline-block;
            width: 720px;
        }

        .dhCircleCnt {
            width: 280px;
            height: 91px;
            background-color: black;
            border-radius: 43px 5px 5px 43px;
            margin-top: 5px;
            background-color: #C6E1F9;
            border-color: #C3BABA;
            border-width: 1px;
            border-style: solid;
        }

            .dhCircleCnt:hover {
                background-color: #E2FFD2;
            }

        .cdhHeader {
            display: inline-block;
            float: left;
            width: 175px;
            text-align: center;
            margin-bottom: 10px;
        }

        .cdhTitle {
            display: inline-block;
            float: left;
            width: 130px;
        }

        .cdnValue {
            display: inline-block;
            width: 40px;
            margin-left: 4px;
            text-align: right;
        }

        .dhDetails {
            display: inline-block;
            float: right;
            width: 186px;
            padding-right: 10px;
            padding-top: 2px;
            color: #1B619E;
            font-size: 12px;
            padding-top: 20px;
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

        .headerLeft {
            display: inline-block;
            float: left;
            width: 240px;
            height: 150px;
        }

        #headerRight {
            display: inline-block;
            float: right;
            width: 450px;
            height: 155px;
            text-align: right;
        }

        #RangeSelector {
            display: inline-block;
            font-size: 15px;
            float: right;
            margin-bottom: 11px;
            position: relative;
            top: -9px;
        }

        .detailsTitle {
            DISPLAY: inline-block;
            WIDTH: 140px;
            text-align: left;
            font-size: 12px;
        }

        .detailsValue {
            DISPLAY: inline-block;
            WIDTH: 89px;
            text-align: right;
            font-size: 12px;
        }
    </style>

    <div class="mainContainer">

        <div class="Box well ">
            <div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lblGalleryName" runat="server" Text="Label"></asp:Label>

                        <div id="RangeSelector">

                            <div class="hidden">

                                <asp:TextBox ID="tbStartDate" runat="server" Text=""></asp:TextBox>
                                <asp:TextBox ID="tbEndDate" runat="server" Text=""></asp:TextBox>
                                <asp:TextBox ID="tbStartTab" runat="server" Text="0"></asp:TextBox>

                                <asp:Button ID="btnDateChanged" runat="server" Text="Button" OnClick="btnDateChanged_Click" />
                            </div>

                            <div id="reportrange" class="pull-left" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc">
                                <i class="glyphicon glyphicon-calendar fa fa-calendar"></i>
                                <span></span><b class="caret"></b>
                            </div>
                        </div>
                    </legend>

                    <div class="headerLeft">
                        <asp:PlaceHolder ID="GalleryInfo1" runat="server"></asp:PlaceHolder>
                    </div>

                    <div class="headerLeft">
                        <asp:PlaceHolder ID="GalleryInfo2" runat="server"></asp:PlaceHolder>
                    </div>

                    <div id="headerRight">
                        <asp:PlaceHolder ID="GalleryInfo3" runat="server"></asp:PlaceHolder>
                    </div>
                </fieldset>
            </div>
        </div>

        <div class="css3-tabstrip">
            <ul>
                <li>
                    <input type="radio" name="css3-tabstrip-0" checked="checked" id="css3-tabstrip-0-0" />
                    <label for="css3-tabstrip-0-0">Prints Trend</label>
                    <div>

                        <div class="tabCotent">

                            <telerik:RadHtmlChart ID="LastMonthChart" BackColor="Transparent" runat="server" Width="1000px" Skin="Bootstrap" Height="520px" Font-Names="Open Sans">

                                <PlotArea>
                                    <Series>
                                        <telerik:AreaSeries DataFieldY="PrintSuccessful">
                                            <LineAppearance LineStyle="Smooth" Width="2px" />
                                            <MarkersAppearance Visible="true" />
                                            <LabelsAppearance Visible="False" />
                                            <TooltipsAppearance>

                                                <ClientTemplate>
                                                    #=dataItem.ttDesc#
                                                </ClientTemplate>
                                            </TooltipsAppearance>
                                        </telerik:AreaSeries>
                                    </Series>

                                    <XAxis BaseUnit="Auto" DataLabelsField="cDate">

                                        <LabelsAppearance DataFormatString="{0:dd/MM/yyyy}" Step="15" Visible="true" />
                                        <MajorGridLines Visible="false" />
                                        <MinorGridLines Visible="false" />
                                    </XAxis>
                                    <YAxis>
                                        <LabelsAppearance DataFormatString="{0:#,#}" Visible="true" />
                                        <MajorGridLines Visible="false" />
                                        <MinorGridLines Visible="false" />
                                    </YAxis>
                                </PlotArea>
                            </telerik:RadHtmlChart>
                        </div>
                    </div>
                </li>
                <li <%=hideShowTab(1)%>>
                    <input type="radio" name="css3-tabstrip-0" id="css3-tabstrip-0-1" />
                    <label for="css3-tabstrip-0-1">
                        Age & Gender
                    </label>
                    <div>

                        <div class="tabCotent">
                            <div class="divAlignLeft">

                                <telerik:RadHtmlChart ID="MFChart" BackColor="Transparent" runat="server" Width="720px" Skin="Office2010Blue" Height="520px" Font-Names="Open Sans">

                                    <Legend>
                                        <Appearance Visible="false"></Appearance>
                                    </Legend>

                                    <PlotArea>

                                        <Series>

                                            <telerik:ColumnSeries DataFieldY="Male" Name="Male" Stacked="true" Gap="0.5">

                                                <LabelsAppearance Visible="false"></LabelsAppearance>

                                                <Appearance>
                                                    <Overlay Gradient="Glass" />
                                                    <FillStyle BackgroundColor="#5496cf" />
                                                </Appearance>
                                            </telerik:ColumnSeries>

                                            <telerik:ColumnSeries DataFieldY="Female" Name="Female" Gap="0.5">

                                                <LabelsAppearance Visible="false"></LabelsAppearance>

                                                <Appearance>
                                                    <Overlay Gradient="Glass" />
                                                    <FillStyle BackgroundColor="#ffb3ff" />
                                                </Appearance>
                                            </telerik:ColumnSeries>
                                        </Series>

                                        <XAxis DataLabelsField="Item">

                                            <MajorGridLines Visible="false" />
                                            <MinorGridLines Visible="false" />
                                        </XAxis>

                                        <YAxis>
                                            <LabelsAppearance DataFormatString="{0:#,#}" Visible="true" />
                                            <MajorGridLines Visible="false" />
                                            <MinorGridLines Visible="false" />
                                        </YAxis>
                                    </PlotArea>
                                </telerik:RadHtmlChart>
                            </div>

                            <div>
                                <div class="genderTip gMale">
                                    <asp:Label ID="lblMaleLegend" runat="server" Text="Label"></asp:Label>
                                </div>

                                <div class="genderTip gFemale">
                                    <asp:Label ID="lblFemaleLegend" runat="server" Text="Label"></asp:Label>
                                </div>

                                <div class="divAlignLeft">

                                    <telerik:RadHtmlChart ID="MFChartPie" runat="server" Width="300px" Height="300px">
                                        <Appearance>
                                            <FillStyle></FillStyle>
                                        </Appearance>

                                        <ChartTitle>
                                            <Appearance Visible="false" Align="Center" BackgroundColor="Transparent" Position="Top">
                                            </Appearance>
                                        </ChartTitle>

                                        <Legend>
                                            <Appearance Position="bottom" Visible="false">
                                                <TextStyle FontSize="18" Color="Blue" FontFamily='"Raleway", sans-serif' />
                                            </Appearance>
                                        </Legend>

                                        <Navigator>
                                            <SelectionHint Visible="true">
                                            </SelectionHint>
                                        </Navigator>
                                    </telerik:RadHtmlChart>
                                </div>
                            </div>
                        </div>
                    </div>
                </li>
                <li>
                    <input type="radio" name="css3-tabstrip-0" id="css3-tabstrip-0-2" />
                    <label for="css3-tabstrip-0-2">Distribution Map</label>
                    <div>
                        <div class="tabCotent">
                            <asp:PlaceHolder ID="PlaceHolderIframe" runat="server"></asp:PlaceHolder>
                        </div>
                    </div>
                </li>

                <li>
                    <input type="radio" name="css3-tabstrip-0" id="css3-tabstrip-0-3" />
                    <label for="css3-tabstrip-0-3">Hour Stats</label>
                    <div>
                        <div class="tabCotent">

                            <telerik:RadHtmlChart ID="radchartHour" BackColor="Transparent" runat="server" Width="1000px" Skin="Bootstrap" Height="520px" Font-Names="Open Sans">

                                <PlotArea>
                                    <Series>
                                        <telerik:AreaSeries DataFieldY="PrintSuccessful">
                                            <LineAppearance LineStyle="Smooth" Width="2px" />
                                            <MarkersAppearance Visible="true" />
                                            <LabelsAppearance Visible="False" />
                                            <TooltipsAppearance>

                                                <ClientTemplate>
                                                    #=dataItem.ttDesc#
                                                </ClientTemplate>
                                            </TooltipsAppearance>
                                        </telerik:AreaSeries>
                                    </Series>

                                    <XAxis DataLabelsField="Item">

                                        <MajorGridLines Visible="false" />
                                        <MinorGridLines Visible="false" />
                                    </XAxis>

                                    <YAxis>
                                        <LabelsAppearance DataFormatString="{0:#,#}" Visible="true" />
                                        <MajorGridLines Visible="false" />
                                        <MinorGridLines Visible="false" />
                                    </YAxis>
                                </PlotArea>
                            </telerik:RadHtmlChart>
                        </div>
                    </div>
                </li>

                <li>
                    <input type="radio" name="css3-tabstrip-0" id="css3-tabstrip-0-4" />
                    <label for="css3-tabstrip-0-4">Day Stat</label>
                    <div>
                        <div>
                            <div class="tabCotent">

                                <div id="dayhourDivChart">
                                    <telerik:RadHtmlChart ID="DayHourChart" BackColor="Transparent" runat="server" Width="700px" Skin="Office2010Blue" Height="520px" Font-Names="Open Sans">

                                        <Legend>
                                            <Appearance Visible="false" Position="Top"></Appearance>
                                        </Legend>

                                        <PlotArea>

                                            <Series>

                                                <telerik:ColumnSeries DataFieldY="H0008" Name="from 00 to 08" Stacked="true" Gap="0.5">

                                                    <LabelsAppearance Visible="false"></LabelsAppearance>

                                                    <Appearance>
                                                        <Overlay Gradient="Glass" />
                                                        <FillStyle BackgroundColor="#5496cf" />
                                                    </Appearance>
                                                </telerik:ColumnSeries>

                                                <telerik:ColumnSeries DataFieldY="H0912" Name="from 09 to 12" Stacked="true" Gap="0.5">

                                                    <LabelsAppearance Visible="false"></LabelsAppearance>

                                                    <Appearance>
                                                        <Overlay Gradient="Glass" />
                                                        <FillStyle BackgroundColor="#87bdeb" />
                                                    </Appearance>
                                                </telerik:ColumnSeries>

                                                <telerik:ColumnSeries DataFieldY="H1318" Name="from 13 to 18" Stacked="true" Gap="0.5">

                                                    <LabelsAppearance Visible="false"></LabelsAppearance>

                                                    <Appearance>
                                                        <Overlay Gradient="Glass" />
                                                        <FillStyle BackgroundColor="#abd1f1" />
                                                    </Appearance>
                                                </telerik:ColumnSeries>

                                                <telerik:ColumnSeries DataFieldY="H1923" Name="from 19 to 23" Stacked="true" Gap="0.5">

                                                    <LabelsAppearance Visible="false"></LabelsAppearance>

                                                    <Appearance>
                                                        <Overlay Gradient="Glass" />
                                                        <FillStyle BackgroundColor="#d6e9fa" />
                                                    </Appearance>
                                                </telerik:ColumnSeries>
                                            </Series>

                                            <XAxis DataLabelsField="Item">

                                                <MajorGridLines Visible="false" />
                                                <MinorGridLines Visible="false" />
                                            </XAxis>

                                            <YAxis>
                                                <LabelsAppearance DataFormatString="{0:#,#}" Visible="true" />
                                                <MajorGridLines Visible="false" />
                                                <MinorGridLines Visible="false" />
                                            </YAxis>
                                        </PlotArea>
                                    </telerik:RadHtmlChart>
                                </div>
                                <div id="dayhourDivCircle">
                                    <asp:PlaceHolder ID="holderDaysCircle" runat="server"></asp:PlaceHolder>
                                </div>
                            </div>
                        </div>
                    </div>
                </li>

                <li>
                    <input type="radio" name="css3-tabstrip-0" id="css3-tabstrip-0-5" />
                    <label for="css3-tabstrip-0-5">Hour Stats</label>
                    <asp:UpdatePanel ID="UpdatePanel" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="btnLoadOfferUsers" runat="server" Text="Load" OnClick="btnLoadOfferUsers_Click" />
                            <div>
                                <div class="tabCotent">

                                    <telerik:RadGrid ID="radGridUserOffers" runat="server" AllowPaging="True" AllowSorting="True" Skin="Bootstrap" Font-Names="Arial Narrow" Font-Size="8px" OnNeedDataSource="radGridUserOffers_NeedDataSource">
                                        <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                        <AlternatingItemStyle Font-Names="Arial Narrow" Font-Size="8px" />
                                    </telerik:RadGrid>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </li>
            </ul>
        </div>
    </div>

    <script type="text/javascript">

        $(function () {
            $('#reportrange').on('hide.daterangepicker', function (ev, picker) {
                dateSelected();
            });
        });

        $(function () {
            $('#reportrange').daterangepicker({
                "opens": "left",
                "startDate": moment("<% =this.GetDate("START")  %>"),
                "endDate": moment("<% =this.GetDate("END")  %>"),
                ranges: {
                    'Today': [moment(), moment()],
                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')],
                    'Last 2 Months': [moment().subtract(2, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
                }
            });
        });

        function dateSelected() {
            var startDate = moment($('#reportrange').data('daterangepicker').startDate.format('YYYY-MM-DD'));
            var endDate = moment($('#reportrange').data('daterangepicker').endDate.format('YYYY-MM-DD'));

            $('#CPC_tbStartDate').val(startDate.format('YYYY-MM-DD'));
            $('#CPC_tbEndDate').val(endDate.format('YYYY-MM-DD'));

            setSelectedTab();

            setText(startDate, endDate);

            $('#CPC_btnDateChanged').click();
        };

        function setText(strStart, strEnd) {
            if (strStart == undefined) {
                strStart = moment($('#CPC_tbStartDate').val());
                strEnd = moment($('#CPC_tbEndDate').val());
            }

            $('#reportrange span').html(strStart.format('MMMM D, YYYY') + ' - ' + strEnd.format('MMMM D, YYYY'));
        };

        $(document).ready(function () {
            $("#settingsImage").exBounce();
            setStartUpTab();
            setText();
        });

        function setStartUpTab() {
            var selectedTab = $('#CPC_tbStartTab').val();

            $('#css3-tabstrip-0-' + selectedTab).prop("checked", true)
        }

        function setSelectedTab() {
            $('#CPC_tbStartTab').val(getSelectedTab());
        }

        function getSelectedTab() {
            var checked = 0;

            for (i = 0; i <= 4; i++) {
                if ($('#css3-tabstrip-0-' + i.toString()).is(':checked')) {
                    checked = i
                    break;
                }
            }
            return (checked);
        }
    </script>
</asp:Content>