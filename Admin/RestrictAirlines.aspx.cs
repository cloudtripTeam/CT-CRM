using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_RestrictAirlines : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserDetails"] != null)
        {
            if (!IsPostBack)
            {
                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
                ////if (!objUserDetail.isAuth("PageDestination"))
                ////{
                ////    Response.Redirect("Default.aspx");
                ////    return;
                ////}
                CommanBinding.BindCompanyDetails(ref ddlCompany, objUserDetail.userID);
                //if (ddlCompany.Items.Count > 1)
                //{ ddlCompany.SelectedValue = "FLTXPT"; }
                //ListItem LI = new ListItem();
                //LI.Value="FLTXPT";
                // LI.Text="FlightXpert";
                // ddlCompany.Items.Add(LI);
               
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
        DataTable restrictedTable = cdt.RestrictedAirlinesTable();
        UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
        foreach (string air in Airlines)
        {
            DataRow dr = restrictedTable.NewRow();
            restrictedTable.Rows.Add(cdt.RestrictedAirlinesRow(dr, air, ddlCompany.SelectedValue, "", txtDetination.Text, "", objUserDetail.userID));
        }
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        objGetSetDatabase.AddPreferedAirlines(restrictedTable);
        BindDetails();

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindDetails();

    }

    private void BindDetails()
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        DataTable dt = objGetSetDatabase.GetPreferedAirlines(txtDetination.Text,txtAirline.Text,ddlCompany.SelectedValue);
        DataTable dtCampaing = null;
        if (ddlCampaign.SelectedValue != "" && ddlCampaign.SelectedValue.ToUpper() != "ALL" && ddlCampaign.SelectedValue.ToUpper() != "ANY")
        {
             dtCampaing = objGetSetDatabase.GET_CredencialByCampaign(ddlCampaign.SelectedValue);
            
        }

        if (dt != null)
        {
            dt.Columns.Add(new DataColumn("Campaign"));
            if (dtCampaing != null && dtCampaing.Rows.Count > 0)
            {
                int j = 1;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["Campaign"] = dtCampaing.Rows[j]["Psuedo_Code"];
                    if (j == (dtCampaing.Rows.Count-1))
                    {
                        j = 1;
                    }
                    else { j++; }
                }
            }
            rptrDetails.DataSource = dt;
            rptrDetails.DataBind();
        }
    }

    [WebMethod(EnableSession = true)]
    public static string DeleteAirline(string ID)
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        UserDetail objUserDetail = HttpContext.Current.Session["UserDetails"] as UserDetail;
        return objGetSetDatabase.DeletePreferedAirlines(ID, "Delete").ToString().ToLower();

        //return objGetSetDatabase.DeleteFlightMarkup(ID, "").ToString().ToLower();
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        CommanBinding.BindCampaignDetails(ref ddlCampaign, ddlCompany.SelectedValue);
    }
}