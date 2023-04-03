using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_FollowUp : System.Web.UI.Page
{
    DataTable dtFollow = new DataTable();
    
    UserDetail objUserDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        objUserDetail = Session["UserDetails"] as UserDetail;

        if (Session["UserDetails"] != null)
        {
            if (!IsPostBack)
            {
                objUserDetail = Session["UserDetails"] as UserDetail;
                if (!objUserDetail.isAuth("SentEticket"))
                {
                    Response.Redirect("~/Admin/AccessDenied.aspx");
                    return;
                }
            if (!string.IsNullOrEmpty(Request.QueryString.Get("BID")))
                {
                    hfXP.Value = Request.QueryString.Get("BID");
                    BookingDetails(hfXP.Value);
                }

            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }

    public void BookingDetails(string XP)
    {
        try
        {
            GetSetDatabase objGetSetDatabase = new GetSetDatabase();
            dtFollow = objGetSetDatabase.GET_Booking_Follow(XP);
            if (dtFollow.Rows.Count > 0)
            {
                rpFollows.DataSource = dtFollow;
                rpFollows.DataBind();
            }
        }
        catch
        {

        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        GetSetDatabase objGetSetDatabaseIN = new GetSetDatabase();
        int i = objGetSetDatabaseIN.SET_Booking_Follow(hfXP.Value, txtRem.Text, txtFromDate.Text, objUserDetail.userID);
        if (i > 0)
        {
            ltrmsg.Text = "Follow added.";
            BookingDetails(hfXP.Value);
                }
        else
            ltrmsg.Text = "Follow not added.";
    }
}