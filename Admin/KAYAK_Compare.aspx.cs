using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_KAYAK_Compare : System.Web.UI.Page
{
    //SqlConnection con = new SqlConnection("Data Source=188.121.44.217;database=dbskywuser;uid=skywuser;pwd=zK4pu?43;");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserDetails"] != null)
        {

            if (!IsPostBack)
            {
                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
                //if (!objUserDetail.isAuth("Kayak_Compare"))
                //{
                //    Response.Redirect("Default.aspx");
                //    return;
                //}
                //else
                //{                  

                //    string role = objUserDetail.userRole.ToLower();


                //}
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }
    protected void btnInsert_Click(object sender, EventArgs e)
    {
        int i = 0;
        UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
        GetSetDatabase gs = new GetSetDatabase();
        i = gs.SetKAYAKCompare("INSERT", 0, txtTitle.Text, txtDescription.Text, txtFrom.Text, txtTo.Text, objUserDetail.userID);
        gs.GetKAYAK("SELECT", txtFrom.Text.Trim(), txtTo.Text.Trim());
        ltrMsg.Text = i > 0 ? "Record Inserted." : "Try Again.";
    }
    protected void btnFind_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        GetSetDatabase gs = new GetSetDatabase();
        dt = gs.GetKAYAK("SELECT", txtFrom.Text.Trim(), txtTo.Text.Trim());

        if (dt.Rows.Count > 0)
        {
            gvKAYAK.DataSource = dt;
            gvKAYAK.DataBind();
        }
    }

    protected void gvKAYAK_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int i = 0;
        UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
        GetSetDatabase gs = new GetSetDatabase();
        GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
        int id = int.Parse((row.FindControl("hfID") as HiddenField).Value);
        string from = (row.FindControl("txtFromU") as TextBox).Text;
        string to = (row.FindControl("txtToU") as TextBox).Text;
        string title = (row.FindControl("txtTitleU") as TextBox).Text;
        string desc = (row.FindControl("txtDescriptionU") as TextBox).Text;

        if(e.CommandName=="update")
        {
            i = gs.SetKAYAKCompare("UPDATE", id, title, desc, from, to, objUserDetail.userID);   
        }
        else if(e.CommandName=="delete")
        {
            i = gs.SetKAYAKCompare("DELETE", id, title, desc, from, to,"");         
        }
       // gs.GetKAYAK("SELECT", from, to);
        ltrMsg.Text = i > 0 ? "Record Modified." : "Try Again.";
    }

    
}
