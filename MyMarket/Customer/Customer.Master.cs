using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyMarket.Customer
{
    public partial class Customer : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Url.AbsoluteUri.ToString().Contains("DefaultCustomer.aspx"))
            {
                //load the usercontrol
                Control sliderUserControl = (Control)Page.LoadControl("BannerUserControl.ascx");
                pnlSliderUC.Controls.Add(sliderUserControl);
            }
        }
    }
}