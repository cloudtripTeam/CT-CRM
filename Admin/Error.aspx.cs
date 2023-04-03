using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Error : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var error = Session["error"];
        if (error != null && error != "")
        {
            lblError.Text = Convert.ToString(((Exception)error).InnerException);
        }
    }
}