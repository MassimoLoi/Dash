<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewMap.aspx.cs" Inherits="Dash.Map.NewMap" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title></title>
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.10.2.min.js"></script>

    <script src="../src/jquery-jvectormap.js"></script>

    <script src="../src/jquery-mousewheel.js"></script>
    <script src="../src/jvectormap.js"></script>

    <script src="../src/abstract-element.js"></script>
    <script src="../src/abstract-canvas-element.js"></script>
    <script src="../src/abstract-shape-element.js"></script>

    <script src="../src/svg-element.js"></script>
    <script src="../src/svg-group-element.js"></script>
    <script src="../src/svg-canvas-element.js"></script>
    <script src="../src/svg-shape-element.js"></script>
    <script src="../src/svg-path-element.js"></script>
    <script src="../src/svg-circle-element.js"></script>
    <script src="../src/svg-image-element.js"></script>
    <script src="../src/svg-text-element.js"></script>

    <script src="../src/map-object.js"></script>
    <script src="../src/region.js"></script>
    <script src="../src/marker.js"></script>

    <script src="../src/vector-canvas.js"></script>
    <script src="../src/simple-scale.js"></script>
    <script src="../src/ordinal-scale.js"></script>
    <script src="../src/numeric-scale.js"></script>
    <script src="../src/color-scale.js"></script>
    <script src="../src/legend.js"></script>
    <script src="../src/data-series.js"></script>
    <script src="../src/proj.js"></script>
    <script src="../src/map.js"></script>

    <script src="../src/jquery-jvectormap-it-mill-en.js"></script>
    <script src="../src/jquery-jvectormap-it_regions-mill.js"></script>
    <link rel="stylesheet" type="text/css" href="//fonts.googleapis.com/css?family=Open+Sans" />

    <style>
        label {
            font-weight: normal !important;
        }

        .radioArea {
            display: inline;
            width: 100%;
            padding: 0;
            margin-bottom: 20px;
            font-size: 21px;
            line-height: inherit;
            color: #333;
            border: 0;
            font-weight: normal !important;
            margin-left: 25px;
        }

        .jvectormap-tip {
            display: none !important;
        }

        .jvectormap-container {
            width: 500px;
            height: 500px;
            background-color: #F5F5F5;
        }

        #map1 {
            height: 460px;
            width: 500px;
            display: inline-block;
            float: left;
        }

        #cityDetails {
            width: 280px;
            height: 91px;
            background-color: black;
            border-radius: 43px 5px 5px 43px;
            margin-top: 5px;
            background-color: #C6E1F9;
            border-color: #C3BABA;
            border-width: 1px;
            border-style: solid;
            position: absolute;
            left: 380px;
            top: 130px;
            display: none;
            font-family: 'Open Sans', sans-serif;
        }

        body {
            font-family: Open Sans;
            font-size: 14px;
            line-height: 1.4285;
            color: #333;
        }

        #dayhourDivCircle {
            display: inline-block;
            float: right;
            width: 100px;
            margin-right: 180px;
            margin-top: -10px;
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
            text-align: left;
            margin-bottom: 4px;
            padding-left: 10px;
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
        }

        #detailsContainer {
            float: right;
            display: inline-block;
            width: 280px;
        }
    </style>

    <script>
        var Data = jQuery.parseJSON(<%=GetMapDetails()%>);

        $(document).ready(function () {
            createMap();
        });

        //jQuery.noConflict();

        function GetCityDetails(cityname) {
            var descString = "";

            if (!$("#cityDetails").is(":visible")) {
                $("#cityDetails").fadeIn("slow");
            }

            if ($("#radioRegione").prop('checked')) {
                for (var i = 0; i < Data.CitiesList.length; i++) {
                    if (Data.CitiesList[i].jvRegionCode == cityname) {
                        descString = GetDatails("",
                                                Data.CitiesList[i].Regione,
                                                Data.CitiesList[i].Area,
                                                Data.CitiesList[i].PrintsTotal, Data.PrintsTotal);

                        break;
                    }
                }
            } else {
                for (var i = 0; i < Data.CitiesList.length; i++) {
                    if (Data.CitiesList[i].Provincia == cityname) {
                        descString = GetDatails(
                            Data.CitiesList[i].ProvinciaDesc,
                            Data.CitiesList[i].Regione,
                            Data.CitiesList[i].Area,
                            Data.CitiesList[i].PrintsTotal, Data.PrintsTotal);

                        break;
                    }
                }
            }

            if (descString == "")
                $("#cityDetails").hide();
            else
                $("#cityDetails").html(descString);
        }

        function GetDatails(prv, regione, area, prints, total) {
            var cPerc = (prints / total) * 100;

            var cValue = ((250 * cPerc) / 100)
            var text = parseFloat(cPerc).toFixed(2) + "%";

            var details = "<svg id='svg' height='90px' width='90px' viewBox='0 0 100 100'>" +
                          "<circle cx='50' cy='50' r='45' fill='#87bdeb' />" +
                          "<path fill='none' stroke-linecap='round' stroke-width='7' stroke='#fff' stroke-dasharray='" + cValue + ",250' d='M50 10 a 40 40 0 0 1 0 80 a 40 40 0 0 1 0 -80' />" +
                          "<text x='50' y='55' fill='#fff' text-anchor='middle' dy='7' font-family=''Open Sans', sans-serif' font-size='26'>" + text + "</text>" +
                          "</svg>" +
                          "<div class='dhDetails'>" +
                          "<span class='cdhHeader'>Area " + area + "</span>" +
                          "<span class='cdhHeader'> " + prv + " </span>" +
                          "<span class='cdhHeader'> " + regione + " </span>" +
                          "<span class='cdhHeader'> TOTAL PRINTS: " + prints + " </span>" +
                          "</div>";

            return (details);
        }

        function createMap() {
            var mapType = "it_mill_en"

            $(window).scroll(function () {
                if ($("#cityDetails").is(":visible")) {
                    $("#cityDetails").fadeOut("slow");
                }
            });

            $("#map1").mouseleave(function () {
                $("#cityDetails").fadeOut("slow");
            });

            if ($("#radioRegione").prop('checked')) {
                mapType = "it_regions_mill"
            }

            jQuery(function () {
                var $ = jQuery;

                var regionStyling = { initial: { fill: '#9cc9ec' }, hover: { fill: "#eb0b0b" } };

                $('#map1').vectorMap({
                    zoomButtons: false,
                    zoomOnScroll: false,
                    map: mapType,
                    backgroundColor: 'transparent',
                    panOnDrag: true,
                    regionStyle: regionStyling,
                    focusOn: {
                        x: 0.5,
                        y: 0.5,
                        scale: 1,
                        animate: true
                    },
                    series: {
                        regions: [{
                            attribute: 'fill',
                            values: {
                    <%=GetMapData()%>
                    }
                    }
                    ]
                    },
                        onRegionTipShow: function (event, label, code) {
                            GetCityDetails(code);
                        },
                        onRegionClick: function (event, label, code) {
                            GetCityDetails(label);
                        }
                    });
                })

            }
    </script>
</head>
<body>

    <form id="form1" runat="server">

        <fieldset>
            <legend>
                <asp:RadioButton ID="radioProvincia" CssClass="radioArea" runat="server" GroupName="mapType" AutoPostBack="True" Text="Province" />
                <asp:RadioButton ID="radioRegione" CssClass="radioArea" runat="server" GroupName="mapType" AutoPostBack="True" Text="Region" />
            </legend>
            <div>

                <div id="cityDetails">
                </div>
                <div id="map1"></div>
                <%--<div id="map2"></div>--%>

                <div id="detailsContainer">

                    <asp:PlaceHolder ID="phDetailsMap" runat="server"></asp:PlaceHolder>
                </div>
            </div>
        </fieldset>
    </form>
</body>
</html>