using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_Atol_ReportFull : System.Web.UI.Page
{
    FandHServices.FandHServicesClient objServices = new FandHServices.FandHServicesClient();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserDetails"] != null)
        {

            if (!IsPostBack)
            {
                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
                if (!objUserDetail.isAuth("Atol_Report"))
                {
                    Response.Redirect("Default.aspx");
                    return;
                }
                else
                {

                    CommanBinding.BindCompanyDetails(ref ddlCompany, objUserDetail.userID);

                    string role = objUserDetail.userRole.ToLower();

                    #region bind ticket supplier list
                    CommanBinding.BindSupplierDetails(ref ddlSupplier, "");
                    #endregion


                }
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }

    }

    protected void btnFind_Click(object sender, EventArgs e)
    {
        GetSetDatabase db = new GetSetDatabase();
        UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
        DataTable dtAtol = new DataTable();
        string bookingStatus = string.Empty;
        string bookingMultiStatus = string.Empty;
        int count = 0;

        if (objUserDetail != null)
        {
            string role = objUserDetail.userRole.ToLower();

            dtAtol = db.GET_Atol_Report_Full("", "", txtFromDate.Text == "" ? null : txtFromDate.Text, txtToDate.Text == "" ? null : Convert.ToDateTime(txtToDate.Text).AddDays(1).ToString("dd/MM/yyyy"),
           CommanBinding.GetCompanyCodes(ddlCompany), CommanBinding.GetCompanyCodes(ddlBookingStatus), txtDepartFrom.Text, txtDepartTo.Text,
           txtAirline.Text, ddlSupplier.SelectedValue, txtIssuedDateFrom.Text, txtIssuedDateTo.Text);
            #region filter multi status

            //foreach (ListItem item in ddlBookingStatus.Items)
            //{
            //    if (item.Selected)
            //    {
            //        count++;
            //        if (count == 1)
            //        {
            //            bookingStatus = item.Value;
            //            bookingMultiStatus = "BookingStatus = '" + item.Value + "'";
            //        }
            //        else
            //        {
            //            bookingStatus = string.Empty;
            //            bookingMultiStatus += " OR BookingStatus = '" + item.Value + "'";
            //        }
            //    }
            //}
            //if (count > 0)
            //{
            //    DataRow[] dr = dtAtol.Select(bookingMultiStatus);
            //    if (dr != null && dr.Count() > 0)
            //    {
            //        dtAtol = dr.CopyToDataTable();
            //    }
            //    else
            //    {
            //        dtAtol = null;
            //    }

            //}

            #region Filter Atol Type
            if (dtAtol != null && dtAtol.Rows.Count > 0 && ddlAtolType.SelectedValue != "")
            {
                string atoltype = "ATOL_Type = '" + ddlAtolType.SelectedValue + "'";
                DataRow[] dr = dtAtol.Select(atoltype);
                if (dr != null && dr.Count() > 0)
                {
                    dtAtol = dr.CopyToDataTable();
                }
                else
                {
                    dtAtol = null;
                }

            }
            #endregion


            #endregion



            if (dtAtol != null && dtAtol.Rows.Count > 0)
            {

                rptAtolReport.DataSource = dtAtol;
                ViewState["AtolReport"] = dtAtol;
                btnExport.Visible = true;
                rptAtolReport.DataBind();
                rptAtolReport.Visible = true;
                Control FooterTemplate = rptAtolReport.Controls[rptAtolReport.Controls.Count - 1].Controls[0];
                Literal ltrFooter = FooterTemplate.FindControl("ltrTotalProfit") as Literal;
                Literal ltrTotalNetProfit = FooterTemplate.FindControl("ltrTotalNetProfit") as Literal;
                Literal ltrTotPax = FooterTemplate.FindControl("ltrTotPax") as Literal;
                int TotPax = 0; double totSell = 0.0; double totNetProf = 0;
                foreach (DataRow dr in dtAtol.Rows)
                {
                    TotPax += Convert.ToInt32(dr["NoOfPax"]);

                    totSell += Convert.ToDouble(dr["Sell_Price"].ToString());

                    totNetProf += Convert.ToDouble(dr["Cost_Price"]);
                }
                ltrTotPax.Text = TotPax.ToString();
                ltrFooter.Text = totSell.ToString();
                ltrTotalNetProfit.Text = totNetProf.ToString();
                lblmessage.Text = "Note :  Sale and Net Fares are based on Fare and Taxes only";
            }
            else
            {
                rptAtolReport.DataSource = dtAtol;
                rptAtolReport.DataBind();
                rptAtolReport.Visible = false;
                lblmessage.Text = "No Record Found.";
                btnExport.Visible = false;
            }
        }

    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)ViewState["AtolReport"];
        if (dt != null)
        {

            System.Data.DataView view = new System.Data.DataView(dt);
            System.Data.DataTable selected = view.ToTable("Selected", false, "BookingRef", "Booking_Date_Time", "Origin", "Destination", "NoOfPax"
                , "LeadPax", "PNR", "DepartDate", "Validating_Carrier", "Currency_Type"
                 , "Cost_Price", "Sell_Price", "Supplier_Name", "ATOL_Type", "Company", "BookingStatus");
            // DataSet ds = new DataSet();
            // ds.Tables.Add(dt);
            //string filepath = Server.MapPath(@"~\Admin\Markup\") + "AgentPL_" + DateTime.Now.ToString("ddMMMyyyy") + ".xls";
            //ExcelLibrary.DataSetHelper.CreateWorkbook(filepath, ds);

            //ltrInvc.Text = "Markup file generated, <a href='//www.flightsandholidays.biz/admin/Markup/AgentPL_" + DateTime.Now.ToString("ddMMMyyyy") + ".xls' >click here to download</a>";
            selected.Columns["BookingRef"].ColumnName = "Invoice No.";
            selected.Columns["Booking_Date_Time"].ColumnName = "Invoice Date";

            selected.Columns["Origin"].ColumnName = "Departure";
            selected.Columns["NoOfPax"].ColumnName = "Passengers";//Lead Passenger

            selected.Columns["LeadPax"].ColumnName = "Lead Passenger";
            selected.Columns["Validating_Carrier"].ColumnName = "Airline";//Lead Passenger

            selected.Columns["Currency_Type"].ColumnName = "Currency";
            selected.Columns["Cost_Price"].ColumnName = "Net Fare";

            selected.Columns["Sell_Price"].ColumnName = "Sales";
            selected.Columns["Supplier_Name"].ColumnName = "Supplier";

            selected.Columns["ATOL_Type"].ColumnName = "Atol Type";
            selected.Columns["BookingStatus"].ColumnName = "Status";

            string attachment = "attachment; filename=" + "Atol_Report_" + DateTime.Now.ToString("ddMMMyyyy") + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in selected.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in selected.Rows)
            {
                tab = "";
                for (i = 0; i < selected.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }
    }
}