using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Collections;
using System.Web.Services;
using System.Web.Script.Serialization;

public partial class Admin_ConvertFare : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserDetails"] != null)
        {
            if (!IsPostBack)
            {
                

            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }

   
}