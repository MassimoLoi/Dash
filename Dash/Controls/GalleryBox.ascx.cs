using System;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dash.Controls
{
    public partial class GalleryBox : System.Web.UI.UserControl
    {
        private const string ImagePath = "https://rivenditori.valassis.it/galleries/img/";

        public string BoxIcon
        {
            get
            {
                return this.ImgIcon.ImageUrl;
            }
            set
            {
                this.ImgIcon.ImageUrl = ImagePath + value;
            }
        }

        public string DetailLink
        {
            get
            {
                return this.hLink.NavigateUrl;
            }
            set
            {
                this.hLink.NavigateUrl = value;
            }
        }

        public Color MainColor { get; set; }

        public string Name
        {
            get
            {
                return this.lblGalleryName.Text;
            }
            set
            {
                this.lblGalleryName.Text = value;
            }
        }

        public string PanelIcon { get; set; }

        public GalleryBox()
        {
            //Color red = ColorTranslator.FromHtml("#FF0000");
            //string redHex = ColorTranslator.ToHtml(Color.Red);
            //this.DivMain.Style.Add("border-color", redHex);
        }

        public void AddLine()
        {
            this.DivHead.Controls.Add(new LiteralControl("<br>"));
            this.DivHead.Controls.Add(new LiteralControl("<hr>"));
        }

        public void AddParagraph(string Title)
        {
            var lTitle = new Label();
            var rowPanel = new Panel();

            lTitle.CssClass = "paragText";
            lTitle.Text = Title;

            rowPanel.Controls.Add(lTitle);
            rowPanel.CssClass = "paragBox";

            this.DivHead.Controls.Add(rowPanel);
        }

        public void AddText(string Title, string Value, string help = "")
        {
            var lTitle = new Label();
            var lValue = new Label();
            var rowPanel = new Panel();

            lTitle.CssClass = "labelTitle";
            lTitle.Text = Title;

            lValue.CssClass = "labelValue";
            lValue.Text = Value;
            rowPanel.Controls.Add(lTitle);
            rowPanel.Controls.Add(lValue);
            rowPanel.CssClass = "rowPanel";

            if (help != "")
            {
                var helpPic = new System.Web.UI.WebControls.Image();
                var mainTooltip = new System.Web.UI.WebControls.HyperLink();

                helpPic.ImageUrl = "~/Images/questionMark.png";
                helpPic.CssClass = "imgHelp";

                //helpPic.Attributes.Add("data-toggle","tooltip");
                //helpPic.Attributes.Add("data-html","tooltip");
                //helpPic.Attributes.Add("title","This is a regular Bootstrap tooltip<ul><li>item 1</li><li>item 2</li></ul>");

                mainTooltip.Attributes.Add("data-placement", "bottom");
                mainTooltip.Attributes.Add("class", "tooltipPos  red-tooltip ");
                mainTooltip.Attributes.Add("data-toggle", "tooltip");
                mainTooltip.Attributes.Add("data-html", "tooltip");
                mainTooltip.Attributes.Add("title", help);

                mainTooltip.Controls.Add(helpPic);

                //<a  data-toggle="tooltip" data-html="tooltip" title="This is a regular Bootstrap tooltip<ul><li>item 1</li><li>item 2</li></ul>" >
                //<img class="imgHelp"  src="Images/questionMark.png">
                //</a>

                rowPanel.Controls.Add(mainTooltip);
            }

            this.DivHead.Controls.Add(rowPanel);
            //this.DivHead.Controls.Add(lValue);
            //this.DivHead.Controls.Add(new LiteralControl("<br>"));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string redHex = ColorTranslator.ToHtml(this.MainColor);

            this.DivMain.Style.Add("border-color", redHex);
            this.DivHead.Style.Add("background-color", redHex);
            this.DivHead.Style.Add("border-color", redHex);
        }
    }
}