using System;
using System.Linq;
using System.Web.UI;
using Telerik.Web.UI;

namespace Dash.Gallery
{
    public partial class GalleryDetails : System.Web.UI.Page
    {
        private MsSqlHelper adoData = new MsSqlHelper();

        private int galleryID = 0;
        private DashUser user;

        public int CalcVersoRedem(DateTime startDate, DateTime endDate, int cpnsRdm, decimal estimatedRedem, int totalprints)
        {
            decimal estCpnsRdm = ((totalprints + (totalprints / 10)) * estimatedRedem) / 100;
            decimal retValue = 0;

            if (cpnsRdm > estCpnsRdm)
            {
                retValue = cpnsRdm;
            }
            else
            {
                retValue = estCpnsRdm;
            }

            return Convert.ToInt32(retValue);
        }

        public void LoadData()
        {
            string showExpires = (this.chkToggleButton.Checked ? "1" : "0");

            this.adoData.OpenDataTable("[sPH_NewGetCouponList] " + Convert.ToInt32('0' + this.user.ManufacturerId).ToString() + "," + this.galleryID.ToString() + ",0,0," + showExpires);

            this.RadGridList.DataSource = this.adoData.Data;
        }

        protected void chkToggleButton_CheckedChanged(object sender, EventArgs e)
        {
            this.RadGridList.Rebind();
        }

        protected void CreateLastMonthChart()
        {
            using (var adoData = new MsSqlHelper())
            {
                adoData.OpenDataTable("select top 60 sum(s.PrintSuccessful) as PrintSuccessful,s.Date as cDate, CONVERT(char(10), s.Date,126) + ' ' + DATENAME(dw,s.Date) + ' -> ' + cast(sum(s.PrintSuccessful) as varchar(10)) as ttDesc  from StatDetails s " +
                                      "left join Applications_Has_Offers aho on s.OfferId = aho.offers_id  " +
                                      "left join vPH_Offers o on s.OfferId = o.id  " +
                                      "where aho.applications_id = " + this.galleryID + " " +
                                      "group by s.date  " +
                                      "order by s.date desc  ");

                this.LastMonthChart.DataSource = adoData.Data;
                this.LastMonthChart.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.galleryID = this.Request.QueryString.GetValue<int>("id");

            if (this.galleryID == 0)
            {
                this.galleryID = 12;
            }

            this.hlprintsStat.NavigateUrl = "GalleryPrintsStat.aspx?GalleryId=" + galleryID;
            this.hlredemptStat.NavigateUrl = "CouponDetailsNew.Aspx?GalleryId=" + galleryID;

            this.user = this.Session["User"].GetUser();

            if (!this.IsPostBack)
            {
                MsSqlHelper adoGalleryDetails = new MsSqlHelper();

                adoGalleryDetails.OpenDataTable("[sPH_GetGalleryHeader] 0," + this.galleryID);

                if (adoGalleryDetails.Data.Rows[0]["iDesc"].ToString() != "")
                {
                    this.lblGalleryName.Text = adoGalleryDetails.Data.Rows[0]["iDesc"].ToString();
                }
                else
                {
                    this.lblGalleryName.Text = adoGalleryDetails.Data.Rows[0]["name"].ToString();
                }

                this.Controls.Add(new LiteralControl("<span class='detailsTitle'>Coupon printed (history)</span>"));
                this.Controls.Add(new LiteralControl("<span class='detailsValue'>" + adoGalleryDetails.Data.Rows[0]["cHPrintSuccessful_Total"].ToString().ToInt32().ToString("#,#") + "</span>"));

                this.Controls.Add(new LiteralControl("<span class='detailsTitle'>Coupon printed (active)</span>"));
                this.Controls.Add(new LiteralControl("<span class='detailsValue'>" + adoGalleryDetails.Data.Rows[0]["cPrintSuccessful_Total"].ToString().ToInt32().ToString("#,#") + "</span>"));

                this.Controls.Add(new LiteralControl("<span class='detailsTitle'>Coupon printed (today)</span>"));
                this.Controls.Add(new LiteralControl("<span class='detailsValue'>" + adoGalleryDetails.Data.Rows[0]["cPrintSuccessful_Today"].ToString().ToInt32().ToString("#,#") + "</span>"));

                this.Controls.Add(new LiteralControl("<span class='detailsTitle'>Active coupons</span>"));
                this.Controls.Add(new LiteralControl("<span class='detailsValue'>" + adoGalleryDetails.Data.Rows[0]["ActivecCoupons"].ToString().ToInt32().ToString("#,#") + "</span>"));

                this.Controls.Add(new LiteralControl("<span class='detailsTitle'>Active Mfrs</span>"));
                this.Controls.Add(new LiteralControl("<span class='detailsValue'>" + adoGalleryDetails.Data.Rows[0]["ActiveMfrs"].ToString().ToInt32().ToString("#,#") + "</span>"));

                this.CreateLastMonthChart();

                this.LoadData();

                int galleryGlobalLimit = 0;
                int totPrint = 0;

                for (int iRow = 0; iRow < this.adoData.Data.Rows.Count; iRow++)
                {
                    if (this.adoData.Data.Rows[iRow]["Global_Limit"].ToString() != "" && this.adoData.Data.Rows[iRow]["Global_Limit"].ToString() != "-1")
                    {
                        galleryGlobalLimit += Convert.ToInt32("0" + this.adoData.Data.Rows[iRow]["Global_Limit"].ToString());
                    }

                    totPrint += Convert.ToInt32("0" + this.adoData.Data.Rows[iRow]["cPrintSuccessful_Total"].ToString());
                }

                this.circlePercentage.Controls.Clear();
                this.circlePercentage.Controls.Add(new LiteralControl(Utils.CreateCircleHtmlPercentageBoxed("130px", "#87bdeb", "7", "#fff", totPrint, galleryGlobalLimit, "50", "55", "30", "#fff", "", "Gallery fill rate")));
                //this.circlePercentage.Controls.Add(new LiteralControl(Utils.CreateCircleHtmlPercentage("140px", "#337ab7", "7", "#fff", totPrint, galleryGlobalLimit, "50", "55", "30")));
            }
        }

        protected void RadGridList_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataItem = (GridDataItem) e.Item;

                int totPrint = Convert.ToInt32("0" + DataBinder.Eval(e.Item.DataItem, "cPrintSuccessful_Total").ToString());
                int globalLimit = DataBinder.Eval(e.Item.DataItem, "Global_Limit").ToString().ToInt32();
                string budget = DataBinder.Eval(e.Item.DataItem, "bud_percen").ToString();
                Decimal bud_percen = 0;
                string textInsideDaysLeft = "";
                string fontSize = "30";
                string textYpos = "55";
                string backColor = "";

                int Id = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Id").ToString());

                if (budget != "")
                    bud_percen = Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "bud_percen").ToString().Replace('.', ','));

                DateTime startDate = Utils.ToDTM(DataBinder.Eval(e.Item.DataItem, "start_date").ToString());
                DateTime endDate = Utils.ToDTM(DataBinder.Eval(e.Item.DataItem, "publish_end_date").ToString());
                int redeemed = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Redeemed").ToString());

                decimal estimatedRedem = Convert.ToDecimal("0" + DataBinder.Eval(e.Item.DataItem, "bud_percen"));

                var totDays = (int) (endDate - startDate).TotalDays;
                var elapsedDays = (int) (DateTime.Now.Date - startDate).TotalDays;

                string statString = "<ul class=\"MenuMini\">" +
                                    " <li> <a href=\"" + "GalleryPrintsStat.aspx?offerId=" + Id.ToString() + "\" class=\"roundMini minigreen\">Redempt</a> </li> " +
                                    "<li> <a href=\"" + "CouponDetailsNew.aspx?offerId=" + Id.ToString() + "\" class=\"roundMini minired\">Prints</a> </li> </ul>";

                if (totPrint > globalLimit)
                {
                    totPrint = globalLimit;
                }

                dataItem["PercColumn"].Text = Utils.CreateCircleHtmlPercentage("60px", "#337ab7", "7", "#fff", totPrint, globalLimit, "50", "55", "30");

                if ((totDays - elapsedDays) <= 0)
                {
                    totDays = 100;
                    elapsedDays = 100;
                    textInsideDaysLeft = "closed";
                    fontSize = "20";
                    textYpos = "48";
                    backColor = "#0e7b1b";
                }
                else
                {
                    textInsideDaysLeft = "-" + (totDays - elapsedDays);
                    fontSize = "30";
                    textYpos = "55";
                    backColor = "#28bb2b";
                }

                dataItem["daysLeft"].Text = Utils.CreateCircleHtmlPercentage("60px", backColor, "7", "#fff", elapsedDays, totDays, "50", textYpos, fontSize, "#fff", textInsideDaysLeft);

                dataItem["StatColumn"].Text = statString;

                int estRedem = this.CalcVersoRedem(startDate, endDate, redeemed, estimatedRedem, totPrint);

                dataItem["VersoRedeemedHistory"].Text = Utils.CreateCircleHtmlPercentage("60px", "#742398", "7", "#fff", estRedem, totPrint, "50", "55", "30", "#fff");

                dataItem["bud_percen"].Text = Utils.CreateCircleHtmlPercentage("60px", "#742398", "7", "#fff", bud_percen, 100, "50", "55", "30", "#fff");
            }
        }

        protected void RadGridList_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            this.LoadData();
        }
    }
}