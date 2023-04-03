using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.IO;

public partial class Admin_AgentsPL : System.Web.UI.Page
{
    FandHServices.FandHServicesClient objServices = new FandHServices.FandHServicesClient();
    string currentId = string.Empty;
    decimal subTotal = 0;
    decimal subTotalTC = 0;

    decimal subTrnTotal = 0;

    decimal total = 0;
    decimal totalCost = 0;
    decimal totalTrns = 0;
    int subTotalRowIndex = 0;
    int SubTotalBooking = 0;
    int TotalBooking = 0;

    DataTable dtInvoice = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserDetails"] != null)
        {

            if (!IsPostBack)
            {
                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
                if (!objUserDetail.isAuth("Agents P & L"))
                {
                    Response.Redirect("Default.aspx");
                    return;
                }
                else
                {
                    CommanBinding.BindCompanyDetails(ref ddlCompany, objUserDetail.userID);
                    txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    string role = objUserDetail.userRole.ToLower();
                    if (role != "superadmin" && role != "admin" && role != "team head"  && role != "marketing head"  )
                    {
                        txtAgent.Text = objUserDetail.userID;
                        txtAgent.Enabled = false;
                    }

                    else { txtAgent.Enabled = true; }

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
        this.gvInvoice.DataSource = null;
        dtInvoice.Clear();
        GetSetDatabase db = new GetSetDatabase();
        UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
        
        if (objUserDetail != null)
        {
            string role = objUserDetail.userRole.ToLower();
            if (role != "superadmin" && role != "admin" && role != "team head" && role != "team head ft" && role != "team head ca" && role != "marketing head" && role != "onlinetl")
            {
                dtInvoice = objServices.GET_Agents_P_L(txtInvNo.Text == "" ? null : txtInvNo.Text, objUserDetail.userID, txtFromDate.Text == "" ? null : txtFromDate.Text, txtToDate.Text == "" ? null : Convert.ToDateTime(txtToDate.Text).AddDays(0).ToString("dd/MM/yyyy"),
               CommanBinding.GetCompanyCodes(ddlCompany), CommanBinding.GetCompanyCodes(ddlBookingStatus));
            }
            else {
                dtInvoice = objServices.GET_Agents_P_L(txtInvNo.Text == "" ? null : txtInvNo.Text, txtAgent.Text == "" ? null : txtAgent.Text, txtFromDate.Text == "" ? null : txtFromDate.Text, txtToDate.Text == "" ? null : Convert.ToDateTime(txtToDate.Text).AddDays(0).ToString("dd/MM/yyyy"),
               CommanBinding.GetCompanyCodes(ddlCompany), CommanBinding.GetCompanyCodes(ddlBookingStatus));

            }
            ViewState["ProfitLoss"] = dtInvoice;
            if (ddlBookingType.SelectedValue != "")
            {

                var result1 = from r in dtInvoice.AsEnumerable()
                              where
                                   r.Field<string>("BookingByType") == ddlBookingType.SelectedValue
                              select r;
                if (result1.Count() > 0)
                {
                    dtInvoice = result1.CopyToDataTable();
                }
                else
                    dtInvoice = null;

            }

            if (dtInvoice.Rows.Count > 0)
            {
                DataView dtview = new DataView(dtInvoice);
                dtview.Sort = "Booking_By ASC";
                dtInvoice = dtview.ToTable();

                gvInvoice.DataSource = dtInvoice;
                gvInvoice.DataBind();
                gvInvoice.Visible = true;
                btnExport.Visible = true;
            }
            else
            {
                gvInvoice.DataSource = dtInvoice;
                gvInvoice.DataBind();
                gvInvoice.Visible = false;
                ltrInvc.Text = "No Record Found.";
                btnExport.Visible = false;

            }
        }   

    }
    //protected void OnRowCreated(object sender, GridViewRowEventArgs e)
    //{
    //    subTotal = 0;
    //    subTotalTC = 0;
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        try { 
    //        DataTable dt = (e.Row.DataItem as DataRowView).DataView.Table;
    //        string BookingId = dt.Rows[e.Row.RowIndex]["Booking_By"].ToString();
    //        total += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["Profit_Amout"]);
    //        totalCost += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["Cost_Price"]);
    //        if (BookingId != currentId)
    //        {
    //            if (e.Row.RowIndex > 0)
    //            {
    //                for (int i = subTotalRowIndex; i < e.Row.RowIndex; i++)
    //                {
    //                    subTotal += Convert.ToDecimal(gvInvoice.Rows[i].Cells[2].Text);
    //                    subTotalTC += Convert.ToDecimal(gvInvoice.Rows[i].Cells[3].Text);
    //                }
    //                this.AddTotalRow("Sub Total", subTotal.ToString("N2"), subTotalTC.ToString("N2"));
    //                subTotalRowIndex = e.Row.RowIndex;
    //            }
    //            currentId = BookingId;
    //        }
    //            }
    //        catch (Exception ex) { }
    //        }
    //}

    //protected void OnDataBound(object sender, EventArgs e)
    //{
    //    for (int i = subTotalRowIndex; i < gvInvoice.Rows.Count; i++)
    //    {
    //        subTotal += Convert.ToDecimal(gvInvoice.Rows[i].Cells[2].Text);
    //        subTotalTC += Convert.ToDecimal(gvInvoice.Rows[i].Cells[3].Text);
    //    }
    //    this.AddTotalRow("Sub Total", subTotal.ToString("N2"), subTotalTC.ToString("N2"));
    //    this.AddTotalRow("Grand Total", total.ToString("N2"), totalCost.ToString("N2"));
    //}

    //private void AddTotalRow(string labelText, string value ,string totalCost)
    //{
    //    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
    //    if (gvInvoice.Controls.Count > 0)
    //    {
    //        row.BackColor = ColorTranslator.FromHtml("#e8e3e3");
    //        row.Font.Bold = true;
    //        row.Cells.AddRange(new TableCell[4] { new TableCell (), //Empty Cell
    //                                    new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Right},
    //                                    new TableCell { Text = value, HorizontalAlign = HorizontalAlign.Right },
    //                                    new TableCell { Text = totalCost, HorizontalAlign = HorizontalAlign.Right }});

    //        gvInvoice.Controls[0].Controls.Add(row);
    //    }
    //}


    protected void OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        subTotal = 0;
        subTotalTC = 0;
        subTrnTotal = 0;
        SubTotalBooking = 0;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                DataTable dt = (e.Row.DataItem as DataRowView).DataView.Table;
                string BookingId = dt.Rows[e.Row.RowIndex]["Booking_By"].ToString();
                total += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["Profit_Amout"]);
                totalCost += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["Cost_Price"]);
                totalTrns += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["Trns_Amount"]);
                TotalBooking += 1;
                if (BookingId != currentId)
                {
                    if (e.Row.RowIndex > 0)
                    {
                        for (int i = subTotalRowIndex; i < e.Row.RowIndex; i++)
                        {
                            subTotal += Convert.ToDecimal(gvInvoice.Rows[i].Cells[2].Text);
                            subTotalTC += Convert.ToDecimal(gvInvoice.Rows[i].Cells[3].Text);
                            subTrnTotal += Convert.ToDecimal(gvInvoice.Rows[i].Cells[5].Text);
                            SubTotalBooking += 1;
                        }
                        this.AddTotalRow("Sub Total", subTotal.ToString("N2"), subTotalTC.ToString("N2"), subTrnTotal.ToString("N2"), "Bookings : " + SubTotalBooking.ToString());
                        subTotalRowIndex = e.Row.RowIndex;
                    }
                    currentId = BookingId;
                }
            }
            catch (Exception ex) { }
        }
    }
    protected void OnDataBound(object sender, EventArgs e)
    {
        for (int i = subTotalRowIndex; i < gvInvoice.Rows.Count; i++)
        {
            subTotal += Convert.ToDecimal(gvInvoice.Rows[i].Cells[2].Text);
            subTotalTC += Convert.ToDecimal(gvInvoice.Rows[i].Cells[3].Text);
            subTrnTotal += Convert.ToDecimal(gvInvoice.Rows[i].Cells[5].Text);
            SubTotalBooking += 1;
        }
        this.AddTotalRow("Sub Total", subTotal.ToString("N2"), subTotalTC.ToString("N2"), subTrnTotal.ToString("N2"), "Bookings : " + SubTotalBooking.ToString());
        this.AddTotalRow("Grand Total", total.ToString("N2"), totalCost.ToString("N2"), totalTrns.ToString("N2"), "Total Bookings: " + TotalBooking.ToString());
    }
    private void AddTotalRow(string labelText, string value, string totalCost, string totalTrns, string totalBookings)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        if (gvInvoice.Controls.Count > 0)
        {
            row.BackColor = ColorTranslator.FromHtml("#e8e3e3");
            row.Font.Bold = true;
            row.Cells.AddRange(new TableCell[7] { new TableCell (), //Empty Cell
                                        new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Right},
                                        new TableCell { Text = value, HorizontalAlign = HorizontalAlign.Right },
                                        new TableCell { Text = totalCost, HorizontalAlign = HorizontalAlign.Right },
                                         new TableCell { Text = "", HorizontalAlign = HorizontalAlign.Right },
                                       new TableCell { Text = totalTrns, HorizontalAlign = HorizontalAlign.Right },
                                        new TableCell { Text = totalBookings, HorizontalAlign = HorizontalAlign.Center }});

            gvInvoice.Controls[0].Controls.Add(row);
        }
    }

    protected void gvInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        TableCell sprice;
        TableCell tprice;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            sprice = e.Row.Cells[4];
            tprice = e.Row.Cells[5];

            tprice.Text = string.IsNullOrEmpty(tprice.Text) ? "0" : tprice.Text;
            sprice.Text = (Convert.ToDecimal(sprice.Text) <= Convert.ToDecimal(tprice.Text)) ? "Full Payment" : Convert.ToDecimal(tprice.Text) == 0 ? "No Payment" : (Convert.ToDecimal(sprice.Text) > Convert.ToDecimal(tprice.Text)) ? "Partial Payment" : "";
            e.Row.Cells[2].ForeColor = Convert.ToDecimal(e.Row.Cells[2].Text) < 0 ? Color.Red : Color.Black;

        }
    }

    


    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)ViewState["ProfitLoss"];
        if (dt != null)
        {
           

            string attachment = "attachment; filename="+ "AgentPL_" + DateTime.Now.ToString("ddMMMyyyy") + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (i = 0; i < dt.Columns.Count; i++)
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