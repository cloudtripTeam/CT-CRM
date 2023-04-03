using System;
using System.Data;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI.WebControls;

public partial class Admin_BookingDetails : CompressedPage
{
    public Common com = new Common();
    public string UserName { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (Session["UserDetails"] != null)
        {
           
            if (!IsPostBack)
            {
                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
                if (!objUserDetail.isAuth("BookingDetails.aspx"))
                {
                    Response.Redirect("~/Admin/AccessDenied.aspx");
                    return;
                }  
                CommanBinding.BindCompanyDetails(ref ddlCompany,objUserDetail.userID);
                txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                
                SearchBookingDetails();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }

    #region Booking Details



    [WebMethod(EnableSession = true)]
    public static string GetBookingSummary(string BookingID, string ProdID)
    {
        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
        return javaScriptSerializer.Serialize(new Itinerary.FlightDetails(BookingID, ProdID, true, true, false, true, true, false, false, false, false));
    }

    [WebMethod(EnableSession = true)]
    public static string GetPassengerDetails(string BookingID, string ProdID)
    {
        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
        return javaScriptSerializer.Serialize(new Itinerary.FlightDetails(BookingID, ProdID, false, false, true, false, false, false, false, false, false));

    }

    [WebMethod(EnableSession = true)]
    public static string GetSectorDetails(string BookingID, string ProdID)
    {
        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
        return javaScriptSerializer.Serialize(new Itinerary.FlightDetails(BookingID, ProdID, false, false, false, false, false, true, false, false, false));

    }

    [WebMethod(EnableSession = true)]
    public static string GetChargeDetails(string BookingID, string ProdID)
    {
        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
        return javaScriptSerializer.Serialize(new Itinerary.FlightDetails(BookingID, ProdID, false, false, false, false, false, false, true, false, false));

    }

    [WebMethod(EnableSession = true)]
    public static string GetTrnsDetails(string BookingID, string ProdID)
    {
        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
        return javaScriptSerializer.Serialize(new Itinerary.FlightDetails(BookingID, ProdID, false, false, false, false, false, false, false, true, false));

    }

    [WebMethod(EnableSession = true)]
    public static string GetRemarks(string BookingID, string ProdID)
    {
        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
        return javaScriptSerializer.Serialize(new Itinerary.FlightDetails(BookingID, ProdID, false, true, false, false, false, false, false, false, false));

    }

    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SearchBookingDetails();
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSourceMedia.Items.Clear();
        ddlSourceMedia.Items.Insert(0, new ListItem("Select Campaign", ""));
        if (ddlCompany.SelectedIndex != 0)
        {
            CommanBinding.BindCampaignDetails(ref ddlSourceMedia, ddlCompany.SelectedValue);
        }
    }

    public  void SearchBookingDetails()
    {      
        try
        {
            GetSetDatabase objGetSetDatabase = new GetSetDatabase();
            DataTable dt = null; ;
             UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
            UserName = objUserDetail.userID.ToLower();

            string bookingStatus = string.Empty;
             string bookingMultiStatus = string.Empty;
             int count = 0;
             foreach (ListItem item in ddlBookingStatus.Items)
             {
                 if (item.Selected)
                 {
                     count++;
                     if (count == 1)
                     {
                         bookingStatus = item.Value;
                         bookingMultiStatus = "BookingStatus = '" + item.Value + "'";
                     }
                     else
                     { bookingStatus = string.Empty;
                     bookingMultiStatus += " OR BookingStatus = '" + item.Value + "'";
                     }
                 }
             }

             if (objUserDetail != null)
             {
                string CurrentDate = txtFromDate.Text;
                string role = objUserDetail.userRole.ToLower();
                dt = objGetSetDatabase.GET_BookingDetail_Date(txtBookingID.Text.Trim(),
                                                              string.Empty,
                                                              string.Empty,
                                                              CommanBinding.GetCompanyCodes(ddlCompany),
                                                              chkSelf.Checked ? objUserDetail.userID : "",
                                                              CurrentDate,
                                                              txtToDate.Text.Trim(),
                                                              bookingStatus,
                                                              txtPNRConfirmation.Text.Trim(),
                                                              ddlSourceMedia.SelectedValue,
                                                              string.Empty,
                                                              txtPhoneNo.Text,
                                                              txtMobileNo.Text.Trim(),
                                                              txtEmailAddress.Text.Trim(),
                                                              txtPaxFirstName.Text.Trim(),
                                                              string.Empty,
                                                              txtPaxLastName.Text.Trim(),
                                                              string.Empty,
                                                              txtSupplierRef.Text,
                                                              txtJFromDate.Text == "" ? null : txtJFromDate.Text,
                                                              txtJToDate.Text == "" ? null : Convert.ToDateTime(txtJToDate.Text).AddDays(1).ToString("dd/MM/yyyy"),
                                                              txtTransRef.Text,
                                                              "ALL");
                
                btnExport.Enabled = true;


                if (count > 1)
                {

                    dt = dt.Select(bookingMultiStatus).CopyToDataTable();
                }


                //fare team can only view online bookings
                if (role == "fares" || role == "online")
                {
                    var result1 = from r in dt.AsEnumerable()
                                  where
                                       r.Field<string>("BookingByType") == "DICT" || r.Field<string>("BookingBy").ToLower() == objUserDetail.userID.ToLower()
                                  select r;
                    if (result1.Count() > 0)
                    {
                        dt = result1.CopyToDataTable();
                    }
                    else
                        dt = null;

                }

                else if (ddlBookingType.SelectedValue != "")
                {

                    var result1 = from r in dt.AsEnumerable()
                                  where
                                       r.Field<string>("BookingByType") == ddlBookingType.SelectedValue
                                  select r;
                    if (result1.Count() > 0)
                    {
                        dt = result1.CopyToDataTable();
                    }
                    else
                        dt = null;

                }
                #region fileter Payment Type
                try
                {
                    if (ddlPaymentType.SelectedValue == "Full")
                    {
                        var result1 = from r in dt.AsEnumerable()
                                      where
                                          r.Field<decimal>("Sell_Price") <= r.Field<decimal>("Trns_Amount")
                                      select r;
                        if (result1.Count() > 0)
                        {
                            dt = result1.CopyToDataTable();
                        }
                        else
                            dt = null;
                    }
                    else if (ddlPaymentType.SelectedValue == "Partial")
                    {

                        var result1 = from r in dt.AsEnumerable()
                                      where
                                          r.Field<decimal>("Sell_Price") > r.Field<decimal>("Trns_Amount") && r.Field<decimal>("Trns_Amount") > 0
                                      select r;
                        if (result1.Count() > 0)
                        {
                            dt = result1.CopyToDataTable();
                        }
                        else
                            dt = null;
                    }
                    else if (ddlPaymentType.SelectedValue == "NoPayment")
                    {

                        var result1 = from r in dt.AsEnumerable()
                                      where
                                           r.Field<decimal>("Trns_Amount") == 0
                                      select r;
                        if (result1.Count() > 0)
                        {
                            dt = result1.CopyToDataTable();
                        }
                        else
                            dt = null;
                    }
                }
                catch (Exception ex)
                { }

                #endregion


                if (dt != null)
                {
                    ViewState["_BookingDetails"] = dt;
                    rptrDetails.DataSource = dt;
                    rptrDetails.DataBind();
                    lblmessage.Text = "";
                    btnExport.Visible = true;
                }
                else
                {
                    lblmessage.Text = "No record found.";
                    btnExport.Visible = false;

                }
            }
        }
        catch { }

    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)ViewState["_BookingDetails"];
        if (dt != null)
        {
            string attachment = "attachment; filename=" + "BookingDetails_" + DateTime.Now.ToString("ddMMMyyyy") + ".xls";
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