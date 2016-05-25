using Dash.Controls;
using Dash.Gallery;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web;
using System.Web.UI;

namespace Dash
{
    public static class GalleryBoxHelper
    {
        public static GalleryBox CreateBox(BoxDetails dBox, Page sourcePage)
        {
            var cBox = (GalleryBox) sourcePage.LoadControl("~/Controls/GalleryBox.ascx");

            cBox.AddText("Stampe Totali", dBox.PrintSuccessfulTotal.ToString("#,#0"), "<span class='tooltiptext'> Le stampe sono i coupon cacciati dai crucchi <br> prova a capo <br> <b>prova</b> </span>");
            cBox.AddText("Stampe Oggi", dBox.PrintSuccessfulToday.ToString("#,#0"));

            if (dBox.Redeemed != 0)
            {
                cBox.AddText("Redenti", dBox.Redeemed.ToString("#,#0"), "<span class='tooltiptext'> I coupon redenti sono i coupon che vengono processati tramite verso <br> prova a capo <br> <b>prova</b> </span>");
            }

            cBox.AddText("Aziende partecipanti", dBox.MfrPartecipanti.ToString("#,#0"));
            cBox.AddText("Tempo medio permanenza", dBox.AvgDaysInGallery.ToString("#,#0"));

            cBox.AddLine();
            cBox.AddText("Valore Facciale Medio", dBox.AvgFaceValue.ToString("€ #,#0.00"));
            cBox.AddText("Rimborso Potenziale", dBox.RisparmioPotenziale.ToString("€ #,#0.0"));
            cBox.DetailLink = "~/Gallery/GalleryDetails.Aspx?id=" + dBox.Id.ToString();
            cBox.Name = dBox.Name;

            if (dBox.Logo == "")
            {
                cBox.BoxIcon = "noimage.png";
            }
            else
            {
                cBox.BoxIcon = dBox.Logo;
            }

            if (dBox.Type == "M" | dBox.MfrPartecipanti == 1)
            {
                cBox.MainColor = Color.Green;
            }
            else
            {
                if (dBox.MfrPartecipanti > 1)
                {
                    cBox.MainColor = Color.FromArgb(107, 55, 175);
                }
                else
                {
                    cBox.MainColor = Color.Crimson;
                }
            }

            return cBox;
        }
    }
}