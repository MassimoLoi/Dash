using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;

namespace Dash.Map
{
    public partial class NewMap : System.Web.UI.Page
    {
        public string CitiesString = "IT-BA,IT-BG,IT-BI,IT-BO,IT-BN,IT-BL,IT-BS,IT-BR,IT-BT,IT-BZ,IT-RC,IT-RA,IT-RG,IT-RE,IT-RI,IT-RO,IT-RN,IT-RM,IT-AG,IT-AN,IT-AO,IT-AL,IT-IS,IT-AV,IT-AT,IT-IM,IT-AR,IT-AP,IT-AQ,IT-GR,IT-LC,IT-LE,IT-LI,IT-TP,IT-TS,IT-TR,IT-LO,IT-TV,IT-LU,IT-LT,IT-TO,IT-TN,IT-TA,IT-TE,IT-CL,IT-CN,IT-CO,IT-CH,IT-CI,IT-CE,IT-CA,IT-CB,IT-CZ,IT-CT,IT-CR,IT-CS,IT-SV,IT-SP,IT-SR,IT-SS,IT-SO,IT-SI,IT-KR,IT-SA,IT-GE,IT-FR,IT-FG,IT-FE,IT-FC,IT-FM,IT-FI,IT-NO,IT-NA,IT-NU,IT-EN,IT-MB,IT-MC,IT-ME,IT-MI,IT-MN,IT-MO,IT-MS,IT-MT,IT-UD,IT-PU,IT-PT,IT-PV,IT-PR,IT-PZ,IT-PE,IT-PD,IT-PG,IT-PA,IT-PC,IT-PO,IT-PN,IT-PI,IT-VE,IT-VC,IT-VB,IT-VA,IT-VI,IT-VV,IT-VT,IT-VS,IT-VR,IT-GO,IT-OG,IT-OR,IT-OT";

        private string[] colors = new string[5] { "#82bef2", "#65a8e3", "#4191d7", "#2a7ec7", "#136ebc" };

        private DateTime endDate;
        private CitieValues mfList;
        private DateTime startDate;

        private int galleryID = 0;
        private int offerID = 0;

        public string GetMapData()
        {
            List<string> citiesList = this.CitiesString.Split(',').ToList();

            string cityColorList = "";
            //int increment = 1;

            Boolean areaMap = this.radioRegione.Checked;

            if (areaMap)
            {
                cityColorList = GetColorsByRegion();
            }
            else
            {
                cityColorList = GetColorsByProvince();
            }

            if (cityColorList.Length > 0)
                return cityColorList.Substring(0, cityColorList.Length - 1);
            else
                return "";
        }

        public string GetMapDetails()
        {
            string json = "";

            if (this.radioRegione.Checked)
            {
                List<CityValue> query = this.mfList.CitiesList.GroupBy(x => new { x.jvRegionCode, x.Regione, x.Area }).Select(ac => new CityValue { jvRegionCode = ac.Key.jvRegionCode, Regione = ac.Key.Regione, Area = ac.Key.Area, PrintsTotal = ac.Sum(c => c.PrintsTotal) }).ToList();

                CitieValues tempCV = new CitieValues();

                tempCV.CitiesList = query;

                json = "'" + Newtonsoft.Json.JsonConvert.SerializeObject(tempCV, Formatting.None) + "'";
            }
            else
            {
                json = "'" + Newtonsoft.Json.JsonConvert.SerializeObject(this.mfList, Formatting.None) + "'";
            }

            return json;
        }

        private void CreateSum()
        {
            string[] sDetails = new string[5] { "", "", "", "", "" };

            AreaTotals[] areas = new AreaTotals[5]
            {
                new AreaTotals { AreaDesc = "Area Nielsen 1" },
                new AreaTotals { AreaDesc = "Area Nielsen 2" },
                new AreaTotals { AreaDesc = "Area Nielsen 3" },
                new AreaTotals { AreaDesc = "Area Nielsen 4" },
                new AreaTotals { AreaDesc = "Non Dichiarato" }
            };

            int totH = 0;

            foreach (CityValue item in this.mfList.CitiesList)
            {
                if (item.Area == 0)
                    areas[4].Prints += item.PrintsTotal;
                else

                    areas[item.Area - 1].Prints += item.PrintsTotal;
            }

            string width = "90px";
            totH = areas[0].Prints + areas[1].Prints + areas[2].Prints + areas[3].Prints;

            for (int i = 0; i < areas.Length; i++)
            {
                sDetails[i] = "<div class='dhDetails'> " +
                              "<span class='cdhHeader'>" + areas[i].AreaDesc + "</span>" +
                              "<span class='cdhTitle'>Total number of prints </span><span class=cdnValue> " + areas[i].Prints.ToString("#,#") + " </span>" +
                              "<span class='cdhTitle'>Total regions </span><span class=cdnValue> " + areas[i].Prints.ToString("#,#") + " </span>" +
                              "<span class='cdhTitle'>Total provinces </span><span class=cdnValue> " + areas[i].Prints.ToString("#,#") + " </span>" +
                              "</div>";
            }

            this.phDetailsMap.Controls.Add(new LiteralControl("<div class='dhCircleCnt'>" + Utils.CreateCircleHtmlPercentage(width, "#87bdeb", "7", "#fff", areas[0].Prints, totH, "50", "55", "30") + sDetails[0] + "</div>"));
            this.phDetailsMap.Controls.Add(new LiteralControl("<div class='dhCircleCnt'>" + Utils.CreateCircleHtmlPercentage(width, "#87bdeb", "7", "#fff", areas[1].Prints, totH, "50", "55", "30") + sDetails[1] + "</div>"));
            this.phDetailsMap.Controls.Add(new LiteralControl("<div class='dhCircleCnt'>" + Utils.CreateCircleHtmlPercentage(width, "#87bdeb", "7", "#fff", areas[2].Prints, totH, "50", "55", "30") + sDetails[2] + "</div>"));
            this.phDetailsMap.Controls.Add(new LiteralControl("<div class='dhCircleCnt'>" + Utils.CreateCircleHtmlPercentage(width, "#87bdeb", "7", "#fff", areas[3].Prints, totH, "50", "55", "30") + sDetails[3] + "</div>"));
        }

        private string GetColor(int curValue, int totValue, string defaultColor)
        {
            decimal curPerc = (curValue / (decimal) totValue) * 100;
            string color = defaultColor;

            if (curPerc == 0)
            {
                color = this.colors[0];
            }
            else if (curPerc >= 1 & curPerc <= 5)
            {
                color = this.colors[1];
            }
            else if (curPerc >= 6 & curPerc <= 10)
            {
                color = this.colors[2];
            }
            else if (curPerc >= 11 & curPerc <= 100)
            {
                color = this.colors[4];
            }

            return color;
        }

        private string GetColorsByProvince()
        {
            var query = this.mfList.CitiesList.GroupBy(x => new { x.Provincia }).Select(ac => new { Provincia = ac.Key.Provincia, PrintsTotal = ac.Sum(c => c.PrintsTotal) }).ToList();

            int totPrints = this.mfList.CitiesList.AsEnumerable().Sum(p => p.PrintsTotal);
            //int maxValue = query.AsEnumerable().Max(p => p.PrintsTotal);

            string colorMap = "";

            foreach (var cItem in query)
            {
                colorMap += "\"" + cItem.Provincia + "\":'" + this.GetColor(cItem.PrintsTotal, totPrints, this.colors[0]) + "',";
            }

            return (colorMap);
        }

        private string GetColorsByRegion()
        {
            var query = this.mfList.CitiesList.GroupBy(x => new { x.jvRegionCode }).Select(ac => new { regionCode = ac.Key.jvRegionCode, PrintsTotal = ac.Sum(c => c.PrintsTotal) }).ToList();

            int totPrints = this.mfList.CitiesList.AsEnumerable().Sum(p => p.PrintsTotal);
            //int maxValue = query.AsEnumerable().Max(p => p.PrintsTotal);

            string colorMap = "";

            foreach (var cItem in query)
            {
                colorMap += "\"" + cItem.regionCode + "\":'" + this.GetColor(cItem.PrintsTotal, totPrints, this.colors[0]) + "',";
            }

            return (colorMap);
        }

        protected void CreateGeoMap()
        {
            using (var adoData = new MsSqlHelper())
            {
                adoData.OpenDataTable("[dbo].[sPH_GetMapData] " + offerID + "," + galleryID);

                this.mfList.CitiesList = adoData.Data.AsEnumerable().Select(row => new CityValue
                {
                    Provincia = "IT-" + (row.GetField<string>("Provincia")).ToUpper().Trim(),
                    ProvinciaDesc = row.GetField<string>("StateDesc").Replace("'", " "),
                    Regione = (row.GetField<string>("Regione")).Replace("'", " "),
                    Area = row.GetField<int>("Area"),
                    Male = row.GetField<int>("Maschi"),
                    Female = row.GetField<int>("Femmine"),
                    F_Over18_25 = row.GetField<int>("f_Over18_25"),
                    F_Over26_35 = row.GetField<int>("f_Over26_35"),
                    F_Over36_45 = row.GetField<int>("f_Over36_45"),
                    F_Over46_55 = row.GetField<int>("f_Over46_55"),
                    F_Over56_70 = row.GetField<int>("f_Over56_70"),
                    F_Over70 = row.GetField<int>("f_Over70"),
                    M_Over18_25 = row.GetField<int>("m_Over18_25"),
                    M_Over26_35 = row.GetField<int>("m_Over26_35"),
                    M_Over36_45 = row.GetField<int>("m_Over36_45"),
                    M_Over46_55 = row.GetField<int>("m_Over46_55"),
                    M_Over56_70 = row.GetField<int>("m_Over56_70"),
                    M_Over70 = row.GetField<int>("m_Over70"),
                    PrintsTotal = row.GetField<int>("TotStampe"),
                    jvRegionCode = "IT-" + row.GetField<string>("jvCode")
                }).ToList();
            }

            GetColorsByRegion();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.mfList = new CitieValues();

            this.galleryID = this.Request.QueryString.GetValue<int>("galleryId");
            this.offerID = this.Request.QueryString.GetValue<int>("offerId");

            this.CreateGeoMap();
            this.CreateSum();
        }

        private class AreaTotals
        {
            public string AreaDesc { get; set; }

            public string Cities { get; set; }

            public int Prints { get; set; }

            public string Regions { get; set; }
        }

        private class CitieValues
        {
            public List<CityValue> CitiesList;

            public int PrintsTotal
            {
                get
                {
                    return this.CitiesList.AsEnumerable().Sum(p => p.PrintsTotal);
                    ;
                }
            }

            public CitieValues()
            {
                this.CitiesList = new List<CityValue>();
            }
        }

        private class CityValue
        {
            public int Area { get; set; }

            public int Avail_Female { get; set; }

            public int Avail_Male { get; set; }

            public int F_Over18_25 { get; set; }

            public int F_Over26_35 { get; set; }

            public int F_Over36_45 { get; set; }

            public int F_Over46_55 { get; set; }

            public int F_Over56_70 { get; set; }

            public int F_Over70 { get; set; }

            public int Female { get; set; }

            public string jvRegionCode { get; set; }

            public int M_Over18_25 { get; set; }

            public int M_Over26_35 { get; set; }

            public int M_Over36_45 { get; set; }

            public int M_Over46_55 { get; set; }

            public int M_Over56_70 { get; set; }

            public int M_Over70 { get; set; }

            public int Male { get; set; }

            public int PrintsTotal { get; set; }

            public string Provincia { get; set; }

            public string ProvinciaDesc { get; set; }

            public string Regione { get; set; }
        }
    }
}