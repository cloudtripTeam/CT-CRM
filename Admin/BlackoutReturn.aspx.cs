using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_BlackoutReturn : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (Session["UserDetails"] != null)
        {
            if (!IsPostBack)
            {
                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
                if (!objUserDetail.isAuth("BlackoutReturn"))
                {
                    Response.Redirect("~/Admin/AccessDenied.aspx");
                    return;
                }
               

            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string[] Airlines = txtAirline.Text.Split(',');
        CreateDataTable cdt = new CreateDataTable();
        DataTable blackoutTable = cdt.BlackoutReturnTable();
        UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
        foreach (string air in Airlines)
        {
            DataRow dr = blackoutTable.NewRow();
            blackoutTable.Rows.Add(cdt.BlackoutReturnRow(dr, air, txtDetination.Text.Trim(), Convert.ToDateTime(txtFromDate.Text.Trim()), Convert.ToDateTime(txtTodate.Text.Trim()), objUserDetail.userID));
        }
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        objGetSetDatabase.AddBlackoutDates(blackoutTable);

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindDetails();

    }

    private void BindDetails()
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        DataTable dt = objGetSetDatabase.GetBlackoutReturn(txtDetination.Text, txtAirline.Text);
        if (dt != null)
        {
            rptrDetails.DataSource = dt;
            rptrDetails.DataBind();
        }
    }

    [WebMethod(EnableSession = true)]
    public static string DeleteBlackout(string ID)
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        UserDetail objUserDetail = HttpContext.Current.Session["UserDetails"] as UserDetail;
        return objGetSetDatabase.DeleteBlackoutDate(ID, "Delete").ToString().ToLower();

        
    }
}