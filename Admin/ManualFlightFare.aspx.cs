using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class Admin_ManualFlightFare : System.Web.UI.Page
{
    DataServices.FandHServicesClient objServices = new DataServices.FandHServicesClient();
    private DataTable dt1 = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserDetails"] != null)
        {

            if (!IsPostBack)
            {
                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
                if (!objUserDetail.isAuth("BookingDetails.aspx"))
                {
                    Response.Redirect("Default.aspx");
                    return;
                }
                CommanBinding.BindCompanyDetails(ref ddlCompany, objUserDetail.userID);
              
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }
    public void bindOperator()
    {
        DataSet dsOperator = new DataSet();
        dsOperator = objServices.FF_bindOperator(txtFrom.Text.Replace("'", "''"), txtTo.Text.Replace("'", "''"), ddlAirline.SelectedValue);
        //////////////////////////////////////////////////////////
        if (dsOperator.Tables[0].Rows.Count > 0)
        {

            rptAirline.DataSource = dsOperator.Tables[0];
            rptAirline.DataBind();
            ddlAirline.DataSource = dsOperator.Tables[0];
            ddlAirline.DataValueField = "AirIATA";
            ddlAirline.DataTextField = "AirName";
            ddlAirline.DataBind();
            ddlAirline.Items.Insert(0, "--All--");
            rptTravelperiod.DataSource = dsOperator.Tables[1];
            rptTravelperiod.DataBind();
        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string from = txtFrom.Text.ToString();
        string to = txtTo.Text.ToString();
        bindOperator();
    }
    protected void btnFind_Click(object sender, EventArgs e)
    {
        bindOperator();
    }
    protected void btnADD_Click(object sender, EventArgs e)
    {
        objServices.FF_UpdateFareStatus(txtFrom.Text, txtTo.Text);
        //////////////////////////////////////////////////////////////////////////
        StringBuilder strSql = new StringBuilder(string.Empty);

        SqlCommand cmd = new SqlCommand();
        for (int a = 0; a < rptTravelperiod.Items.Count; a++)
        {
            HtmlInputCheckBox chkSelectT = (HtmlInputCheckBox)rptTravelperiod.Items[a].FindControl("chkoptT");
            if (chkSelectT != null)
            {
                if (chkSelectT.Checked)
                {
                    string SD = ((Label)rptTravelperiod.Items[a].FindControl("lblFD")).Text;
                    string TD = ((Label)rptTravelperiod.Items[a].FindControl("lblTD")).Text;
                    decimal FR = Convert.ToDecimal(((Label)rptTravelperiod.Items[a].FindControl("lblFare")).Text);
                    string FN = ((Label)rptTravelperiod.Items[a].FindControl("lblFN")).Text;
                    string TN = ((Label)rptTravelperiod.Items[a].FindControl("lblTN")).Text;
                    for (int i = 0; i < rptAirline.Items.Count; i++)
                    {
                        HtmlInputCheckBox chkSelect = (HtmlInputCheckBox)rptAirline.Items[i].FindControl("chkopt");
                        if (chkSelect != null)
                        {
                            if (chkSelect.Checked)
                            {
                                string strFlightID = chkSelect.Value;
                                int Perc = Convert.ToInt32(((TextBox)rptAirline.Items[i].FindControl("txtAirline")).Text);
                                decimal tof = (FR + (FR * Perc / 100));
                                string ANm = ((Label)rptAirline.Items[i].FindControl("lblON")).Text;
                                string strInsert = "INSERT INTO FlightFares(Price,FrIATA,FrName,ToIATA,ToName,AirIATA,AirName,VFrom,VTill,sts,JType,ArrDay)values(" + tof + ", '" + txtFrom.Text + "','" + FN + "','" + txtTo.Text + "','" + TN + "','" + strFlightID + "','" + ANm + "','" + SD + "','" + TD + "',2,1,1)";
                                strSql.Append(strInsert);

                            }
                        }
                    }
                }
            }
        }
        try
        {
            int i = 0;

            i = objServices.FF_UpdateFares(strSql.ToString());
            if (i != 0)
            {
                objServices.FF_FlightFaresDelete(txtFrom.Text, txtTo.Text);
            }
            else
            {
            }
            lblMsg.Text = "Fare Added";
        }
        catch (SqlException ex)
        {
            string errorMsg = "Error in Updation";
            errorMsg += ex.Message;
            lblMsg.Text = errorMsg;
            throw new Exception(errorMsg);
        }
        finally
        {

        }
    }
}