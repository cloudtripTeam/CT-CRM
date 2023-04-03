using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web.Services;

public partial class Admin_AgentAssignedBooking : System.Web.UI.Page
{
    DataTable dtAssignedBooking = new DataTable();
    int subTotalRowIndex = 0;
    int SubTotalBooking = 0;
    int TotalBooking = 0;
    string currentId = string.Empty;
    public static string bookingRemarks;

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
                    if (role != "superadmin" && role != "admin" && role != "team head" && role != "team head ft" && role != "marketing head" && role != "onlinetl" && role != "team head ca")
                    {
                        txtAgent.Text = objUserDetail.userID;
                        txtAgent.Enabled = false;
                    }

                    else
                    {
                        txtAgent.Enabled = true;
                    }
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
        this.gvAssignedBooking.DataSource = null;
        dtAssignedBooking.Clear();
        GetSetDatabase db = new GetSetDatabase();
        UserDetail objUserDetail = Session["UserDetails"] as UserDetail;

        if (objUserDetail != null)
        {
            string role = objUserDetail.userRole.ToLower();
            if (role != "superadmin" && role != "admin" && role != "team head" && role != "team head ft" 
                && role != "team head ca" && role != "marketing head" && role != "onlinetl")
            {
                dtAssignedBooking = db.BookingAssignement(txtInvNo.Text == "" ? null : txtInvNo.Text,
                    objUserDetail.userID, string.Empty,
                    txtFromDate.Text == "" ? null : txtFromDate.Text,
                    txtToDate.Text == "" ? null : Convert.ToDateTime(txtToDate.Text).AddDays(1).ToString("dd/MM/yyyy"),
                    "SELECT",
                    CommanBinding.GetCompanyCodes(ddlCompany), CommanBinding.GetCompanyCodes(ddlBookingStatus));
            }
            else
            {
                dtAssignedBooking = db.BookingAssignement(txtInvNo.Text == "" ? null : txtInvNo.Text,
                    txtAgent.Text == "" ? null : txtAgent.Text,
                    string.Empty,
                    txtFromDate.Text == "" ? null : txtFromDate.Text,
                    txtToDate.Text == "" ? null : Convert.ToDateTime(txtToDate.Text).AddDays(1).ToString("dd/MM/yyyy"),
                    "SELECT",
                    CommanBinding.GetCompanyCodes(ddlCompany), CommanBinding.GetCompanyCodes(ddlBookingStatus));
            }
            ViewState["AssignedBooking"] = dtAssignedBooking;


            if (dtAssignedBooking.Rows.Count > 0)
            {
                DataView dtview = new DataView(dtAssignedBooking);
                dtview.Sort = "Assigned_To ASC";
                dtAssignedBooking = dtview.ToTable();

                gvAssignedBooking.DataSource = dtAssignedBooking;
                gvAssignedBooking.DataBind();
                gvAssignedBooking.Visible = true;
                btnExport.Visible = true;
            }
            else
            {
                gvAssignedBooking.DataSource = dtAssignedBooking;
                gvAssignedBooking.DataBind();
                gvAssignedBooking.Visible = false;
                ltrInvc.Text = "No Record Found.";
                btnExport.Visible = false;
            }
        }
    }

    protected void OnRowCreated(object sender, GridViewRowEventArgs e)
    {

        SubTotalBooking = 0;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                DataTable dt = (e.Row.DataItem as DataRowView).DataView.Table;
                string BookingId = dt.Rows[e.Row.RowIndex]["Assigned_To"].ToString();
                TotalBooking += 1;
                if (BookingId != currentId)
                {
                    if (e.Row.RowIndex > 0)
                    {
                        for (int i = subTotalRowIndex; i < e.Row.RowIndex; i++)
                        {

                            SubTotalBooking += 1;
                        }
                        this.AddTotalRow("Sub Total", "", "", "", "Bookings : " + SubTotalBooking.ToString());
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
        for (int i = subTotalRowIndex; i < gvAssignedBooking.Rows.Count; i++)
        {

            SubTotalBooking += 1;
        }
        this.AddTotalRow("Sub Total", "", "", "", "Bookings : " + SubTotalBooking.ToString());
        this.AddTotalRow("Grand Total", "", "", "", "Total Bookings: " + TotalBooking.ToString());
    }

    private void AddTotalRow(string labelText, string value, string totalCost, string totalTrns, string totalBookings)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        if (gvAssignedBooking.Controls.Count > 0)
        {
            row.BackColor = ColorTranslator.FromHtml("#e8e3e3");
            row.Font.Bold = true;
            row.Cells.AddRange(new TableCell[9] { new TableCell (), //Empty Cell
                                        new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Right},
                                         new TableCell { Text = totalBookings, HorizontalAlign = HorizontalAlign.Center },
                                        new TableCell { Text = value, HorizontalAlign = HorizontalAlign.Right },
                                        new TableCell { Text = totalCost, HorizontalAlign = HorizontalAlign.Right },
                                         new TableCell { Text = "", HorizontalAlign = HorizontalAlign.Right },
                                       new TableCell { Text = totalTrns, HorizontalAlign = HorizontalAlign.Right },
                                        new TableCell { Text = totalTrns, HorizontalAlign = HorizontalAlign.Right },
                                         new TableCell { Text = totalTrns, HorizontalAlign = HorizontalAlign.Right }
                                       });

            gvAssignedBooking.Controls[0].Controls.Add(row);
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)ViewState["AssignedBooking"];
        if (dt != null)
        {


            string attachment = "attachment; filename=" + "AssignedBooking_" + DateTime.Now.ToString("ddMMMyyyy") + ".xls";
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


    [WebMethod]
    public static string BookingRemarks(string bookingID)
    {
        string hotelRemarks = string.Empty;
        DataTable dt = new DataTable();
        try
        {
            GetSetDatabase objGetSetDatabase = new GetSetDatabase();
            dt = objGetSetDatabase.GET_Booking_Remarks("SELECT", bookingID);
            if (dt.Rows.Count > 0)
            {
                bookingRemarks = string.Empty;
                foreach (DataRow dr in dt.Rows)
                {
                    hotelRemarks = string.Empty;
                    if (Convert.ToString(dr["Remarks"]).Contains("Hotel Remarks :"))
                    {
                        hotelRemarks += Convert.ToString(dr["Remarks"]).Length > 15 ? Convert.ToString(dr["Remarks"]).Substring(15, Convert.ToString(dr["Remarks"]).Length - 15) : "";
                    }
                    else
                    {
                        hotelRemarks += Convert.ToString(dr["Remarks"]);
                    }
                    bookingRemarks += "<tr><td class='gdvr'>" + hotelRemarks + "</td><td class='gdvr'>" + dr["Remarks_By"] + "</td><td class='gdvr'>" + dr["DatenTime"] + "</td></tr>";
                }
            }
            return bookingRemarks;
        }
        catch
        {
            return "";
        }
    }
}

