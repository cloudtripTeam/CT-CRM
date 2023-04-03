using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Globalization;

public partial class Admin_ManualFareUpdate : System.Web.UI.Page
{
    
    SqlConnection con = DataConnection.GetConnectionFareManual();
    private DataTable dt1 = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        CultureInfo uk = new CultureInfo("en-GB");
    }
    public void bindOperator()
    {
        DataTable dsOperator = new DataTable();
        FandHServices.FandHServicesClient objServices = new FandHServices.FandHServicesClient();
        dsOperator = objServices.bindOperator(txtFrom.Text, txtTo.Text);
        ddloperator.DataSource = dsOperator;
        ddloperator.DataTextField = "AirName";
        ddloperator.DataValueField = "AirIATA";
        ddloperator.DataBind();
        ddloperator.Items.Insert(0, new ListItem("All", "All"));

    }
    public void bindFareSheet(string oprator, string from, string to)//,string validfromdate,string validtodate)
    {
        DataTable dsFareSheet = new DataTable();
        FandHServices.FandHServicesClient objServices = new FandHServices.FandHServicesClient();
        dsFareSheet = objServices.bindFareSheet(oprator, from, to);
        if (dsFareSheet.Rows.Count > 0)
        {
            gvFareSheet.Visible = true;
            gvFareSheet.DataSource = dsFareSheet;
            gvFareSheet.DataBind();
            btnADD.Visible = true;
            btnDelete.Visible = true;
            btnUpdate.Visible = true;

            dsFareSheet.Dispose();
        }
        else
        {

            btnADD.Visible = false;
            btnDelete.Visible = false;
            btnUpdate.Visible = false;

            gvFareSheet.Visible = false;
        }



    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string from = txtFrom.Text.ToString();
        string to = txtTo.Text.ToString();
        string Operator = ddloperator.SelectedValue;
        bindFareSheet(Operator, from, to);
        bindOperator();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        FandHServices.FandHServicesClient objServices = new FandHServices.FandHServicesClient();
        foreach (GridViewRow gr in gvFareSheet.Rows)
        {
            if (gvFareSheet.Rows[gr.RowIndex].FindControl("chkSelect") != null)
            {
                if (((CheckBox)gvFareSheet.Rows[gr.RowIndex].FindControl("chkSelect")).Checked)
                {
                    string grn = ((Label)(gvFareSheet.Rows[gr.RowIndex].FindControl("lblFlightID"))).Text;
                    objServices.DeleteFare(grn);
                }
            }
        }
        lblMsg.Text = "Record Deleted .";
        bindFareSheet(ddloperator.SelectedValue, txtFrom.Text, txtTo.Text);
        bindOperator();
    }
    protected void chkBxHeader_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)gvFareSheet.HeaderRow.FindControl("chkBxHeader");
        if (chkAll.Checked == true)
        {
            foreach (GridViewRow gvRow in gvFareSheet.Rows)
            {
                CheckBox chkSel = (CheckBox)gvRow.FindControl("chkSelect");
                chkSel.Checked = true;
            }
        }
        else
        {
            foreach (GridViewRow gvRow in gvFareSheet.Rows)
            {
                CheckBox chkSel = (CheckBox)gvRow.FindControl("chkSelect");
                chkSel.Checked = false;
            }

        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        FandHServices.FandHServicesClient objServices = new FandHServices.FandHServicesClient();
        //Create stringbuilder to store multiple DML statements 
        StringBuilder strSql = new StringBuilder(string.Empty);

        //Create sql connection and command
        //SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();

        //Loop through gridview rows to find checkbox 
        //and check whether it is checked or not 
        string strFlightID = string.Empty;
        string strAdult = string.Empty;
        string strValidFrom = string.Empty;
        string strValidTo = string.Empty;
        string strUpdate = string.Empty;
        string FIata = string.Empty;
        string FName = string.Empty;
        string TIata = string.Empty;
        string TName = string.Empty;
        int JTp = 0;
        int Arv = 0;
        for (int i = 0; i < gvFareSheet.Rows.Count; i++)
        {
            CheckBox chkUpdate = (CheckBox)gvFareSheet.Rows[i].Cells[0].FindControl("chkSelect");
            if (chkUpdate != null)
            {
                if (chkUpdate.Checked)
                {
                    // Get the values of textboxes using findControl
                    //strFlightID = gvFareSheet.Rows[i].Cells[0].Text;
                    strFlightID = ((Label)gvFareSheet.Rows[i].FindControl("lblFlightID")).Text;
                    strAdult = ((TextBox)gvFareSheet.Rows[i].FindControl("lblAdultTotal")).Text;
                    strValidFrom = ((TextBox)gvFareSheet.Rows[i].FindControl("lblValidFrom")).Text;
                    strValidTo = ((TextBox)gvFareSheet.Rows[i].FindControl("lblValidTo")).Text;
                    string strO = ((TextBox)gvFareSheet.Rows[i].FindControl("txtO")).Text;
                    string strON = ((TextBox)gvFareSheet.Rows[i].FindControl("txtOName")).Text;
                    FIata = ((TextBox)gvFareSheet.Rows[i].FindControl("txtFrom")).Text;
                    FName = ((TextBox)gvFareSheet.Rows[i].FindControl("txtFromF")).Text;
                    TIata = ((TextBox)gvFareSheet.Rows[i].FindControl("txtTo")).Text;
                    TName = ((TextBox)gvFareSheet.Rows[i].FindControl("txtToF")).Text;
                    JTp = Convert.ToInt32(((TextBox)gvFareSheet.Rows[i].FindControl("lblJType")).Text);
                    Arv = Convert.ToInt32(((TextBox)gvFareSheet.Rows[i].FindControl("lblArrday")).Text);


                    /////////////////////////////////////////////////////////////////////////////////////////
                    char[] ch = { '-' };
                    string[] fdt = strValidFrom.Split(ch);
                    string frmdt = fdt[2] + "-" + fdt[1] + "-" + fdt[0];
                    string datef = frmdt.ToString();
                    char[] chr = { '-' };

                    string[] fdtr = strValidTo.Split(chr);
                    string frmdtr = fdtr[2] + "-" + fdtr[1] + "-" + fdtr[0];
                    string redate = frmdtr;
                    ////////////////////////////////////////////////////////////////////////////////////////////
                    strUpdate = "update FlightFares set Price=" + strAdult + ", VFrom='" + datef + "', VTill='" + redate + "',AirIATA='" + strO + "', AirName='" + strON + "',FrIATA='" + FIata + "', FrName='" + FName + "',ToIATA='" + TIata + "',ToName='" + TName + "',JType='" + JTp + "',ArrDay='" + Arv + "',ModifyBy='" + System.Environment.MachineName + "',LastModifyDate='" + System.DateTime.Now + "' where FrID=" + strFlightID + "";
                    strSql.Append(strUpdate);
                    chkUpdate.Checked = false;
                }
            }
        }
        try
        {
            string Qr = strSql.ToString();
            objServices.UpdateFares(Qr);
            lblMsg.Text = "Updated Successuly";
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
            con.Close();
        }

    }
    protected void btnADD_Click(object sender, EventArgs e)
    {
        FandHServices.FandHServicesClient objServices = new FandHServices.FandHServicesClient();
        int i = objServices.AddFares(txtFrom.Text.Trim(), txtTo.Text.Trim(), ddloperator.SelectedValue);
        if (i != 0)
        {
            lblMsg.Text = "Record Add Successfully.";
        }
        else
        {
            lblMsg.Text = "Record not addedd !!!";
        }
        bindFareSheet(ddloperator.SelectedValue, txtFrom.Text.Trim(), txtTo.Text.Trim());
        bindOperator();
        SetFocus(lblMsg);
    }
}