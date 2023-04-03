using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_FlightSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void lnkbtnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {

            string jtype = string.Empty;
            if (journeyType.Value == "O")
            { jtype = "1"; }
            else if (journeyType.Value == "R")
            { jtype = "2"; }
            else 
            { jtype = "3"; }
           

            string url = "fltdeeplink.aspx?org=" + txtDeparture.Value + "&dest=" + txtDestination.Value + "&departDate=" + txtDepDate.Value + "&returnDate=" + txtRetDate.Value + "&adt=" + ddlAdult.Value + "&chd=" + ddlChild.Value + "&inf=" + ddlInfant.Value + "&classType=" + ddlClass.Value + "&airline=" + ddlAirline.Value + "&JType=" + jtype + "&DFlights=" + chkDirect.Checked.ToString() + "&isFlx=false&campaign=BKOFFICE&company=BKOFFICE";

            Response.Redirect(url,false);
        }

    }
}