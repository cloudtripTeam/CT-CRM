using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Holidays_Holiday_Offer_City : System.Web.UI.Page
{
    FandHServices.FandHServicesClient objlos = new FandHServices.FandHServicesClient();
    public int ID { set; get; }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
       
        bool qr;
        qr = objlos.SET_Holiday_Offer_City_Overview("INSERT",0,txtCityCode.Text,txtOverviews.Text,txtFlightTime.Text,txtTimeZone.Text,txtAirlines.Text,txtWhereFly.Text,"");
        if (qr == true) { ltrMsg.Text = "Record Inserted"; }
        else { ltrMsg.Text = "Not Insert!!!"; }
        clear();
      
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string Code = txtCityCode.Text == "" ? null : txtCityCode.Text;

        BindOverview(Code);
        
    }
    private void BindOverview(string CityCode)
    {
        DataTable dt = new DataTable();
        dt=objlos.GET_Holiday_Offer_City_Overview("SELECT", CityCode);
        if (dt.Rows.Count > 0)
        grdCITY.DataSource = dt;
        grdCITY.DataBind();

    
    }
   
    protected void grdCITY_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        txtCityCode.Text = ((Literal)(grdCITY.Rows[e.RowIndex].FindControl("ltrCode"))).Text;
        txtFlightTime.Text = ((Literal)(grdCITY.Rows[e.RowIndex].FindControl("ltrFTime"))).Text;
        txtTimeZone.Text = ((Literal)(grdCITY.Rows[e.RowIndex].FindControl("ltrTimeZone"))).Text;
        txtWhereFly.Text = ((Literal)(grdCITY.Rows[e.RowIndex].FindControl("ltrWhere"))).Text;
        txtOverviews.Text = ((Literal)(grdCITY.Rows[e.RowIndex].FindControl("ltrDetails"))).Text;
        txtAirlines.Text = ((Literal)(grdCITY.Rows[e.RowIndex].FindControl("ltrAirlines"))).Text;
        ltrID.Text = ((Literal)(grdCITY.Rows[e.RowIndex].FindControl("ltrID"))).Text;
        btnUpdates.Visible = true;
        btnCancle.Visible = true;
        btnSearch.Visible = false;
        btnSave.Visible = false;
    }
    protected void btnUpdates_Click(object sender, EventArgs e)
    {
     
        bool qr;
        qr = objlos.SET_Holiday_Offer_City_Overview("UPDATE", Convert.ToInt32(ltrID.Text), txtCityCode.Text, txtOverviews.Text, txtFlightTime.Text, txtTimeZone.Text, txtAirlines.Text, txtWhereFly.Text, "");
        if (qr == true) { ltrMsg.Text = "Record Updated"; }
        else
        {
            ltrMsg.Text = "Not Update!!!";
        }
        btnSave.Visible = true;
        btnSearch.Visible = true;
        btnUpdates.Visible = false;
        btnCancle.Visible = false;
    }
    protected void btnCancle_Click(object sender, EventArgs e)
    {
        btnSearch.Visible = true;
        btnSave.Visible = true;
        btnUpdates.Visible = false;
        btnCancle.Visible = false;
        clear();
    }
    private void clear()
    {
        txtCityCode.Text = "";
        txtFlightTime.Text = "";
        txtTimeZone.Text = "";
        txtWhereFly.Text = "";
        txtOverviews.Text = "";
        txtAirlines.Text = "";
    }
}