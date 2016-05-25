using Dash;
using Dash.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dash.Gallery
{
    public partial class List : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadGalleryList();
        }

        private void LoadGalleryList()
        {
            MsSqlHelper adoData = new MsSqlHelper();
            var divContainer = new System.Web.UI.WebControls.Panel();
            GalleryBox cBox = new GalleryBox();

            divContainer.CssClass = "DivContainerContent";

            adoData.OpenDataTable("[sPH_GetGalleryStatsBoxApp] " + 0 + "," + 0 + ",0");

            var boxList = new List<BoxDetails>();

            boxList = adoData.Data.AsEnumerable().Select(row => new BoxDetails
            {
                Id = row.GetField<int>("id"),
                AvgDaysInGallery = row.GetField<int>("AvgDaysInGallery"),
                CountriesId = row.GetField<int>("Countries_Id"),
                //DevSite = row.GetField<string>("DevSite"),
                AvgFaceValue = row.GetField<decimal>("AvgFaceValue"),
                CpnsActive = row.GetField<int>("CpnActive"),
                Logo = row.GetField<string>("Logo"),
                MfrPartecipanti = row.GetField<int>("MfrPartecipanti"),
                Name = row.GetField<string>("Name"),
                PrintSuccessfulToday = row.GetField<int>("PrintSuccessful_Today"),
                PrintSuccessfulTotal = row.GetField<int>("PrintSuccessful_Total"),
                PruductionSite = row.GetField<string>("SiteProduction"),
                Redeemed = row.GetField<int>("Redeemed"),
                RisparmioPotenziale = row.GetField<decimal>("RisparmioPotenziale"),
                Type = row.GetField<string>("Type")
            }).ToList();

            for (int i = 0; i < 10; i++)
            {
                cBox = GalleryBoxHelper.CreateBox(boxList[i], this);

                divContainer.Controls.Add(cBox);
            }

            this.PlaceHolderBoxes.Controls.Clear();

            if (divContainer.Controls.Count > 0)
                this.PlaceHolderBoxes.Controls.Add(divContainer);
        }
    }
}