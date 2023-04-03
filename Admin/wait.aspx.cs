using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_wait : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["q"] != null)
        {
            SecureQueryString qs = new SecureQueryString(Request["q"]);
            string page = qs["redirectPage"].ToString();
            string searchID = qs["searchID"].ToString();
            string medium, totalprice, valcarrier = string.Empty;
            if (qs["medium"] != null)
            {
                medium = qs["medium"].ToString();
                totalprice = qs["price"].ToString();
                valcarrier = qs["valcar"].ToString();
                hidNextPage.Value = page + "?id=" + searchID + "&medium=" + medium + "&price=" + totalprice + "&valcar=" + valcarrier;
            }
            else
            {
                hidNextPage.Value = page + "?id=" + searchID;

            }
            //searchDestination.Text = qs["origin"].ToString() + " to " + qs["destination"].ToString();
            lblfrom.Text = qs["origin"].ToString() + " (" + qs["originCode"].ToString() + ")";
            lblTo.Text = qs["destination"].ToString() + " (" + qs["destinationCode"].ToString() + ")";
            lblDates.Text = qs["DepartDate"].ToString();

            if (qs["ReturnDate"].ToString() != "")
            {
                lblDates.Text += " to " + qs["ReturnDate"].ToString();
            }


        }
    }
}