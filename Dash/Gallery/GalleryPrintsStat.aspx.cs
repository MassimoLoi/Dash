using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using Telerik.Web.UI;

namespace Dash.Gallery
{
    public partial class GalleryPrintsStat : System.Web.UI.Page
    {
        //public string CitiesString = "IT-BA,IT-BG,IT-BI,IT-BO,IT-BN,IT-BL,IT-BS,IT-BR,IT-BT,IT-BZ,IT-RC,IT-RA,IT-RG,IT-RE,IT-RI,IT-RO,IT-RN,IT-RM,IT-AG,IT-AN,IT-AO,IT-AL,IT-IS,IT-AV,IT-AT,IT-IM,IT-AR,IT-AP,IT-AQ,IT-GR,IT-LC,IT-LE,IT-LI,IT-TP,IT-TS,IT-TR,IT-LO,IT-TV,IT-LU,IT-LT,IT-TO,IT-TN,IT-TA,IT-TE,IT-CL,IT-CN,IT-CO,IT-CH,IT-CI,IT-CE,IT-CA,IT-CB,IT-CZ,IT-CT,IT-CR,IT-CS,IT-SV,IT-SP,IT-SR,IT-SS,IT-SO,IT-SI,IT-KR,IT-SA,IT-GE,IT-FR,IT-FG,IT-FE,IT-FC,IT-FM,IT-FI,IT-NO,IT-NA,IT-NU,IT-EN,IT-MB,IT-MC,IT-ME,IT-MI,IT-MN,IT-MO,IT-MS,IT-MT,IT-UD,IT-PU,IT-PT,IT-PV,IT-PR,IT-PZ,IT-PE,IT-PD,IT-PG,IT-PA,IT-PC,IT-PO,IT-PN,IT-PI,IT-VE,IT-VC,IT-VB,IT-VA,IT-VI,IT-VV,IT-VT,IT-VS,IT-VR,IT-GO,IT-OG,IT-OR,IT-OT";
        private string[] colors = new string[5] { "#d6e9fa", "#abd1f1", "#87bdeb", "#5496cf", "#337ab7" };

        private DateTime startDate;
        private DateTime endDate;
        private bool useRcsData = false;

        private int galleryID = 0;
        private int offerID = 0;
        private DashUser user;

        private CitieValues mfList;

        public string GetDate(string dType)
        {
            if (dType == "START")
            {
                return this.tbStartDate.Text;
            }
            else
            {
                return this.tbEndDate.Text;
            }
        }

        public string GetMapDetails()
        {
            string json = "'" + Newtonsoft.Json.JsonConvert.SerializeObject(this.mfList, Formatting.None) + "'";
            return json;
        }

        protected void btnDateChanged_Click(object sender, EventArgs e)
        {
        }

        //protected void CreateGeoMap()
        //{
        //    using (var adoData = new AdoHelper())
        //    {
        //        adoData.OpenDataTable("[sPH_GetCouponMapDetails] 1654 ");

        //        this.MaleFemaleChart(adoData);
        //    }
        //}

        protected void CreateLastMonthChart()
        {
            using (MsSqlHelper adoData = new MsSqlHelper())
            {
                adoData.OpenDataTable("[sPH_GetLastMonthTrend] " + this.offerID.ToString() + "," + this.galleryID.ToString() + ",0,'" + this.startDate.ToString("yyyy-MM-dd") + "','" + this.endDate.ToString("yyyy-MM-dd") + "'");

                this.LastMonthChart.DataSource = adoData.Data;
                this.LastMonthChart.DataBind();
            }
        }

        private void CreateUserOfferChart()
        {
            var adoData = new MsSqlHelper();

            adoData.OpenDataTable("select top 10 * from[dbo].[MonthlyPrintsStats] where syn_id = 15 order by yearNumber desc, monthNumber desc");

            //List<object> dataSource = new List<object>();

            //for (int i = 0; i < adoData.Data.Rows.Count; i++)
            //{
            //    dataSource.Add(new
            //    {
            //        Month = adoData.Data.Rows[i]["MonthNumber"],
            //        Year = adoData.Data.Rows[i]["yearNumber"],
            //        Quantity = adoData.Data.Rows[i]["Quantity"],
            //        Consumers = adoData.Data.Rows[i]["Consumers"],
            //        Offers = adoData.Data.Rows[i]["Offers"],
            //        ttDesc = "Number of prints: ",
            //        Item = i
            //    });
            //}

            this.radGridUserOffers.DataSource = adoData.Data;
            this.radGridUserOffers.DataBind();

            //"select top 10 * from[dbo].[weeklyPrintsStats] where syn_id = 15 order by yearNumber desc, weekNumber desc"
            //"select top 10 * from[dbo].[YearlyPrintsStats] where syn_id = 15 order by yearNumber desc"

            //"SELECT Sum(1) quantity, " +
            //"       Count(DISTINCT consumer_id) AS consumers, " +
            //"       Count(DISTINCT offers_id) AS offers " +
            //"FROM vph_prints ' +
            //"WHERE printed = 1 and creation_date between '2015-08-01' and '2016-09-01' and syn_id = 15 "
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.mfList = new CitieValues();

            this.user = this.Session["User"].GetUser();
            this.galleryID = this.Request.QueryString.GetValue<int>("galleryId");
            this.offerID = this.Request.QueryString.GetValue<int>("offerId");

            if (galleryID == 0 & offerID == 0)
                galleryID = 12;

            if (this.tbStartDate.Text != "")
            {
                this.startDate = DateTime.ParseExact(this.tbStartDate.Text, "yyyy-MM-dd", null);
                this.endDate = DateTime.ParseExact(this.tbEndDate.Text, "yyyy-MM-dd", null);
            }
            else
            {
                using (var adoData = new MsSqlHelper())
                {
                    adoData.OpenDataTable("[sPH_GetMinMaxUsageDate] " + this.offerID + "," + this.galleryID);

                    this.startDate = (DateTime) adoData.Data.Rows[0]["minDate"];
                    this.endDate = (DateTime) adoData.Data.Rows[0]["maxDate"];
                    this.tbStartDate.Text = this.startDate.ToString("yyyy-MM-dd");
                    this.tbEndDate.Text = this.endDate.ToString("yyyy-MM-dd");
                }
            }

            if (!IsPostBack)
            {
                using (var adoData = new MsSqlHelper())
                {
                    adoData.OpenDataTable("[sPH_UseRcsData]  " + this.offerID + "," + this.galleryID + ",1");

                    this.useRcsData = (bool) adoData.Data.Rows[0]["UseRcsData"];
                }

                this.CreateLastMonthChart();
                this.MaleFemaleChart();
                this.CreateCouponUsageByDayHour();
                this.CreateChartByHour();
                //this.CreateUserOfferChart();
            }

            this.ShowGalleryDetails();
            this.PlaceHolderIframe.Controls.Add(new LiteralControl("<iframe id='mapHolder' src='/Map/newmap.aspx?offerId=" + this.offerID + "&galleryId=" + this.galleryID + "' width='980px' height='590px' ></iframe>"));
        }

        protected void radioProvincia_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void CreateChartByHour()
        {
            using (var adoData = new MsSqlHelper())
            {
                adoData.OpenDataTable("[sPH_GetHourUsage] " + this.offerID.ToString() + "," + this.galleryID.ToString() + ",'" + this.startDate.ToString("yyyy-MM-dd") + "','" + this.endDate.ToString("yyyy-MM-dd") + "'");

                List<object> dataSource = new List<object>();

                for (int i = 0; i <= 23; i++)
                {
                    dataSource.Add(new
                    {
                        PrintSuccessful = adoData.Data.Rows[0]["H" + i.ToString("00")],
                        ttDesc = "Number of prints: " + Convert.ToInt32(adoData.Data.Rows[0]["H" + i.ToString("00")]).ToString("#,#0"),
                        Item = i
                    });
                }

                this.radchartHour.DataSource = dataSource;
                this.radchartHour.DataBind();
            }
        }

        public string hideShowTab(int tab)
        {
            string retString = "";

            switch (tab)
            {
                case 1:

                    if (!this.useRcsData)
                        retString = "style=\"display: none;\"";

                    break;

                default:
                    break;
            }

            return retString;
        }

        private void CreateCouponUsageByDayHour()
        {
            string clockIcon = "<i class='fa fa-clock-o'></i>";

            using (var adoData = new MsSqlHelper())
            {
                adoData.OpenDataTable("[sPH_GetCouponUsageByDayHour] " + this.offerID + "," + this.galleryID + ",0,'" + this.startDate.ToString("yyyy-MM-dd") + "','" + this.endDate.ToString("yyyy-MM-dd") + "'");

                List<Object> dataSource = new List<object>();
                string[] sDetails = new string[4] { "", "", "", "" };
                string[] sheaders = new string[4] { "From 19:00 to 23:00 " + clockIcon, "From 13:00 to 18:00 " + clockIcon, "From 09:00 to 12:00 " + clockIcon, "From 00:00 to 08:00 " + clockIcon };
                int[] sTotals = new int[4] { 0, 0, 0, 0 };

                int totH = 0;

                for (int i = 0; i < adoData.Data.Rows.Count; i++)
                {
                    sTotals[0] += Convert.ToInt32(adoData.Data.Rows[i]["h19-23"]);
                    sTotals[1] += Convert.ToInt32(adoData.Data.Rows[i]["h13-18"]);
                    sTotals[2] += Convert.ToInt32(adoData.Data.Rows[i]["h9-12"]);
                    sTotals[3] += Convert.ToInt32(adoData.Data.Rows[i]["h0-08"]);

                    dataSource.Add(new
                    {
                        H0008 = adoData.Data.Rows[i]["h0-08"],
                        H0912 = adoData.Data.Rows[i]["h9-12"],
                        H1318 = adoData.Data.Rows[i]["h13-18"],
                        H1923 = adoData.Data.Rows[i]["h19-23"],
                        Item = adoData.Data.Rows[i]["wDay"]
                    });
                }

                string width = "90px";
                totH = sTotals[0] + sTotals[1] + sTotals[2] + sTotals[3];

                for (int i = 0; i < sheaders.Length; i++)
                {
                    sDetails[i] = "<div class='dhDetails'> " +
                                  "<span class='cdhHeader'>" + sheaders[i] + "</span>" +
                                  "<span class='cdhTitle'>Total number of prints </span><span class=cdnValue> " + sTotals[i].ToString("#,#") + " </span>" +
                                  "</div>";
                }

                this.holderDaysCircle.Controls.Add(new LiteralControl("<div class='dhCircleCnt'>" + Utils.CreateCircleHtmlPercentage(width, "#DBECFA", "7", "#fff", sTotals[0], totH, "50", "55", "30", "#5496CF") + sDetails[0] + "</div>"));
                this.holderDaysCircle.Controls.Add(new LiteralControl("<div class='dhCircleCnt'>" + Utils.CreateCircleHtmlPercentage(width, "#abd1f1", "7", "#fff", sTotals[1], totH, "50", "55", "30") + sDetails[1] + "</div>"));
                this.holderDaysCircle.Controls.Add(new LiteralControl("<div class='dhCircleCnt'>" + Utils.CreateCircleHtmlPercentage(width, "#87bdeb", "7", "#fff", sTotals[2], totH, "50", "55", "30") + sDetails[2] + "</div>"));
                this.holderDaysCircle.Controls.Add(new LiteralControl("<div class='dhCircleCnt'>" + Utils.CreateCircleHtmlPercentage(width, "#5496cf", "7", "#fff", sTotals[3], totH, "50", "55", "30") + sDetails[3] + "</div>"));

                this.DayHourChart.DataSource = dataSource;

                this.DayHourChart.DataBind();
            }
        }

        private void MaleFemaleChart()
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
                    PrintsTotal = row.GetField<int>("TotStampe")
                }).ToList();

                var listByOwner = this.mfList.CitiesList.GroupBy(i => 1).Select(lg => new
                {
                    F_Over18_25 = lg.Sum(item => item.F_Over18_25),
                    F_Over26_35 = lg.Sum(item => item.F_Over26_35),
                    F_Over36_45 = lg.Sum(item => item.F_Over36_45),
                    F_Over46_55 = lg.Sum(item => item.F_Over46_55),
                    F_Over56_70 = lg.Sum(item => item.F_Over56_70),
                    F_Over70 = lg.Sum(item => item.F_Over70),
                    M_Over18_25 = lg.Sum(item => item.M_Over18_25),
                    M_Over26_35 = lg.Sum(item => item.M_Over26_35),
                    M_Over36_45 = lg.Sum(item => item.M_Over36_45),
                    M_Over46_55 = lg.Sum(item => item.M_Over46_55),
                    M_Over56_70 = lg.Sum(item => item.M_Over56_70),
                    M_Over70 = lg.Sum(item => item.M_Over70),
                    Female = lg.Sum(item => item.Female),
                    Male = lg.Sum(item => item.Male)
                    ,
                }).ToList();

                List<Object> dataSource = new List<object>();

                if (listByOwner.Count > 0)
                {
                    dataSource.Add(new { Female = listByOwner[0].F_Over18_25, Male = listByOwner[0].M_Over18_25, Item = "Tra 18-25" });
                    dataSource.Add(new { Female = listByOwner[0].F_Over26_35, Male = listByOwner[0].M_Over26_35, Item = "Tra 26-35" });
                    dataSource.Add(new { Female = listByOwner[0].F_Over36_45, Male = listByOwner[0].M_Over36_45, Item = "Tra 36-45" });
                    dataSource.Add(new { Female = listByOwner[0].F_Over46_55, Male = listByOwner[0].M_Over46_55, Item = "Tra 46-55" });
                    dataSource.Add(new { Female = listByOwner[0].F_Over56_70, Male = listByOwner[0].M_Over56_70, Item = "Tra 56-70" });
                    dataSource.Add(new { Female = listByOwner[0].F_Over70, Male = listByOwner[0].M_Over70, Item = "Over 70" });

                    this.MFChart.DataSource = dataSource;

                    this.MFChart.DataBind();

                    PieSeries pS = new PieSeries();

                    pS.SeriesItems.Add(new PieSeriesItem(listByOwner[0].Female, System.Drawing.Color.FromArgb(255, 153, 255), "Female"));
                    pS.SeriesItems.Add(new PieSeriesItem(listByOwner[0].Male, System.Drawing.Color.FromArgb(84, 150, 207), "Male"));

                    pS.LabelsAppearance.DataFormatString = "{0:#,#}";
                    pS.LabelsAppearance.Position = Telerik.Web.UI.HtmlChart.PieAndDonutLabelsPosition.Center;

                    pS.TooltipsAppearance.ClientTemplate = "Gender: #=data.category#  <br> Value: #= kendo.format(\"{0:N}\", data.value)#  ";

                    this.lblMaleLegend.Text = "<i class='fa fa-male fa-2x'></i>" + " Male: 46%";
                    this.lblFemaleLegend.Text = "<i class='fa fa-female fa-2x'></i>" + " Female: 54%";

                    this.MFChartPie.PlotArea.Series.Add(pS);
                }
            }
        }

        private void ShowGalleryDetails()
        {
            DateTime dtStart = Utils.ToDTM(this.tbStartDate.Text);
            DateTime dtEnd = Utils.ToDTM(this.tbEndDate.Text);

            using (var adoData = new MsSqlHelper())
            {
                adoData.OpenDataTable("[dbo].[sPH_GetGalleryHeader] " + this.offerID + "," + this.galleryID);

                if (adoData.Data.Rows[0]["iDesc"].ToString() != "")
                {
                    this.lblGalleryName.Text = adoData.Data.Rows[0]["iDesc"].ToString();
                }
                else
                {
                    this.lblGalleryName.Text = adoData.Data.Rows[0]["name"].ToString();
                }

                this.GalleryInfo1.Controls.Add(new LiteralControl("<span class='detailsTitle'>Coupon printed (history)</span>"));
                this.GalleryInfo1.Controls.Add(new LiteralControl("<span class='detailsValue'>" + adoData.Data.Rows[0]["cHPrintSuccessful_Total"].ToString().ToInt32().ToString("#,#") + "</span>"));

                this.GalleryInfo1.Controls.Add(new LiteralControl("<span class='detailsTitle'>Coupon printed (active)</span>"));
                this.GalleryInfo1.Controls.Add(new LiteralControl("<span class='detailsValue'>" + adoData.Data.Rows[0]["cPrintSuccessful_Total"].ToString().ToInt32().ToString("#,#") + "</span>"));

                this.GalleryInfo1.Controls.Add(new LiteralControl("<span class='detailsTitle'>Coupon printed (today)</span>"));
                this.GalleryInfo1.Controls.Add(new LiteralControl("<span class='detailsValue'>" + adoData.Data.Rows[0]["cPrintSuccessful_Today"].ToString().ToInt32().ToString("#,#") + "</span>"));

                this.GalleryInfo1.Controls.Add(new LiteralControl("<span class='detailsTitle'>Active coupons</span>"));
                this.GalleryInfo1.Controls.Add(new LiteralControl("<span class='detailsValue'>" + adoData.Data.Rows[0]["ActivecCoupons"].ToString().ToInt32().ToString("#,#") + "</span>"));

                this.GalleryInfo1.Controls.Add(new LiteralControl("<span class='detailsTitle'>Active Mfrs</span>"));
                this.GalleryInfo1.Controls.Add(new LiteralControl("<span class='detailsValue'>" + adoData.Data.Rows[0]["ActiveMfrs"].ToString().ToInt32().ToString("#,#") + "</span>"));

                this.GalleryInfo2.Controls.Add(new LiteralControl("<span class='detailsTitle'>Days selected</span>"));
                this.GalleryInfo2.Controls.Add(new LiteralControl("<span class='detailsValue'>" + ((dtEnd.Subtract(dtStart).Days) + 1).ToString() + "</span>"));

                this.GalleryInfo3.Controls.Add(new LiteralControl(Utils.CreateCircleHtmlPercentageBoxed("130px", "#87bdeb", "7", "#fff", 100, 130, "50", "55", "30", "#fff", "", "Fill rate")));
                this.GalleryInfo3.Controls.Add(new LiteralControl(Utils.CreateCircleHtmlPercentageBoxed("130px", "#87bdeb", "7", "#fff", 25, 100, "50", "55", "30", "#fff", "", "Est Redempt")));
                this.GalleryInfo3.Controls.Add(new LiteralControl(Utils.CreateCircleHtmlPercentageBoxed("130px", "#87bdeb", "7", "#fff", 100, 130, "50", "55", "30", "#fff", "-120", "Days Left")));
            }
        }

        private class CitieValues
        {
            public List<CityValue> CitiesList;

            public CitieValues()
            {
                this.CitiesList = new List<CityValue>();
            }

            //public int PrintsTotal { get; set; }
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

        protected void radGridUserOffers_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
        }

        protected void btnLoadOfferUsers_Click(object sender, EventArgs e)
        {
            CreateUserOfferChart();
        }
    }
}