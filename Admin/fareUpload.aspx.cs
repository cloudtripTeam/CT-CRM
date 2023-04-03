using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_fareUpload : System.Web.UI.Page
{
    
    private SqlConnection con = DataConnection.GetConnectionFareUpload();
    private DataTable dt1 = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserDetails"] != null)
        {
            if (!IsPostBack)
            {
                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;

                if (!objUserDetail.isAuth("FareUpload"))
                {
                    Response.Redirect("~/Admin/AccessDenied.aspx");
                    return;
                }
            }
        }
    }

    public void bindOperator()
    {
        DataTable dtOperatior = new DataTable();
        dtOperatior = Get_Data_Flight("SELECT", txtFrom.Text, txtTo.Text, null, ddlCompany.SelectedValue);
        if (dtOperatior.Rows.Count > 0)
        {
            dtOperatior = dtOperatior.DefaultView.ToTable(true, "AirIATA");
            ddloperator.DataSource = dtOperatior;
            ddloperator.DataTextField = "AirIATA";
            ddloperator.DataValueField = "AirIATA";
            ddloperator.DataBind();
            ddloperator.Items.Insert(0, new ListItem("All", ""));
        }
    }

    public void bindFareSheet()
    {
        try
        {
            DataTable dtFlights = new DataTable();
            dtFlights = Get_Data_Flight("SELECT", txtFrom.Text, txtTo.Text, string.IsNullOrEmpty(ddloperator.SelectedValue) ? null : ddloperator.SelectedValue, ddlCompany.SelectedValue);

            if (dtFlights.Rows.Count > 0)
            {
                gvFareSheet.Visible = true;
                gvFareSheet.DataSource = dtFlights;
                gvFareSheet.DataBind();
                btnADD.Visible = true;
                btnDelete.Visible = true;
                btnUpdateFare.Visible = true;
                dtFlights.Dispose();
            }
            else
            {
                btnADD.Visible = false;
                btnDelete.Visible = false;
                btnUpdateFare.Visible = false;
                gvFareSheet.Visible = false;
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindFareSheet();
        bindOperator();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string Company = ddlCompany.SelectedValue == "USA" ? "FlightFaresUSA" : "FlightFares";
        foreach (GridViewRow row in gvFareSheet.Rows)
        {
            if (gvFareSheet.Rows[row.RowIndex].FindControl("chkSelect") != null && ((CheckBox)gvFareSheet.Rows[row.RowIndex].FindControl("chkSelect")).Checked)
                SqlHelper.ExecuteNonQuery(con, CommandType.Text, "Delete from " + Company + " WHERE FrID='" + ((Label)gvFareSheet.Rows[row.RowIndex].FindControl("lblFlightID")).Text + "'");
        }
        lblMsg.Text = "Record Deleted .";
        bindFareSheet();
        bindOperator();
    }

    protected void btnUpdateFare_Click(object sender, EventArgs e)
    {
        StringBuilder stringBuilder = new StringBuilder(string.Empty);
        SqlCommand sqlCommand = new SqlCommand();
        string Company = ddlCompany.SelectedValue == "USA" ? "FlightFaresUSA" : "FlightFares";
        for (int index = 0; index < gvFareSheet.Rows.Count; ++index)
        {
            CheckBox control = (CheckBox)gvFareSheet.Rows[index].Cells[0].FindControl("chkSelect");
            if (control != null && control.Checked)
            {
                string text1 = ((Label)gvFareSheet.Rows[index].FindControl("lblFlightID")).Text;
                string text2 = ((TextBox)gvFareSheet.Rows[index].FindControl("lblAdultTotal")).Text;
                string text3 = ((TextBox)gvFareSheet.Rows[index].FindControl("lblValidFrom")).Text;
                string text4 = ((TextBox)gvFareSheet.Rows[index].FindControl("lblValidTo")).Text;
                string text5 = ((TextBox)gvFareSheet.Rows[index].FindControl("txtO")).Text;
                string text6 = ((TextBox)gvFareSheet.Rows[index].FindControl("txtFrom")).Text;
                string text7 = ((TextBox)gvFareSheet.Rows[index].FindControl("txtTo")).Text;
                int num = ((CheckBox)gvFareSheet.Rows[index].FindControl("chkIsOffer")).Checked ? 1 : 0;
                int int32_1 = Convert.ToInt32(((TextBox)gvFareSheet.Rows[index].FindControl("txtJType")).Text);
                int int32_2 = Convert.ToInt32(((TextBox)gvFareSheet.Rows[index].FindControl("txtArrDay")).Text);
                string str1 = Convert.ToDateTime(text3).ToString("yyyy/MM/dd");
                string str2 = Convert.ToDateTime(text4).ToString("yyyy/MM/dd");
                string str3 = "update " + Company + " set Price=" + text2 + ", VFrom='" + str1 + "', VTill='" + str2 + "',AirIATA='" + text5 + "', AirName='" + text5 + "',FrIATA='" + text6 + "', FrName='" + text6 + "',ToIATA='" + text7 + "',ToName='" + text7 + "',JType='" + int32_1 + "',ArrDay='" + int32_2 + "',ModifyBy='" + Environment.MachineName + "',LastModifyDate=GETDATE(),IsReco='" + num + "',Currency='USD' where FrID=" + text1;
                stringBuilder.Append(str3);
                control.Checked = false;
            }
        }
        try
        {
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = stringBuilder.ToString();
            sqlCommand.Connection = con;
            con.Open();
            sqlCommand.ExecuteNonQuery();
            lblMsg.Text = "Updated Successuly";
        }
        catch (SqlException ex)
        {
            string message = "Error in Updation" + ex.Message;
            lblMsg.Text = message;
            throw new Exception(message);
        }
        finally
        {
            con.Close();
        }
    }

    protected void btnADD_Click(object sender, EventArgs e)
    {
        if (Set_Data_Flight("INSERT", txtFrom.Text, txtTo.Text, ddloperator.SelectedValue, ddlCompany.SelectedValue) != 0)
            lblMsg.Text = "Record Add Successfully.";
        else
            lblMsg.Text = "Record not addedd !!!";
        bindFareSheet();
        bindOperator();
        SetFocus(lblMsg);
    }

    private DataTable Get_Data_Flight(string statement, string Dep, string Dest, string Airline, string Company)
    {
        DataTable dtFlight = new DataTable();
        Company = ddlCompany.SelectedValue == "USA" ? "USP_CRUD_FlightFaresUSA" : "USP_CRUD_FlightFares";
        try
        {
            SqlCommand cmd = new SqlCommand(Company, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Statement", statement);
            cmd.Parameters.AddWithValue("@FrIATA", Dep);
            cmd.Parameters.AddWithValue("@ToIATA", Dest);
            cmd.Parameters.AddWithValue("@AirIATA", Airline);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dtFlight);
        }
        catch (Exception ex)
        {

        }
        finally
        {
            con.Close();
        }
        return dtFlight;
    }
    private int Set_Data_Flight(string statement, string Dep, string Dest, string Airline, string Company)
    {
        Company = ddlCompany.SelectedValue == "USA" ? "USP_CRUD_FlightFaresUSA" : "USP_CRUD_FlightFares";
        int i = 0;
        try
        {
            SqlCommand cmd = new SqlCommand(Company, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Statement", statement);
            cmd.Parameters.AddWithValue("@FrIATA", Dep);
            cmd.Parameters.AddWithValue("@ToIATA", Dest);
            cmd.Parameters.AddWithValue("@AirIATA", Airline);
            con.Open();
            i = cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {

        }
        finally
        {
            con.Close();
        }
        return i;
    }
}