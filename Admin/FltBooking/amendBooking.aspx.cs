using System;
using System.Linq;
using System.Web.Services;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections.Generic;
using System.Web.UI;
using System.IO;

public partial class Admin_FltBooking_amendBooking : System.Web.UI.Page
{
    UserDetail objUserDetail;
    private string oldStatus;
    private string oldBookingBy;
    private string oldAssignedBy;
    private string oldCompany;
    public string bookingRemarks;
    public int transRow = 0;
    double totCost = 0.0;
    double totSell = 0.0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserDetails"] != null)
        {
            objUserDetail = Session["UserDetails"] as UserDetail;
            if (!IsPostBack)
            {
                if (!objUserDetail.isAuth("amend Booking"))
                {
                    Response.Redirect("Default.aspx");
                    return;
                }
                if (Request.QueryString["BID"] != null && Request.QueryString["PID"] != null)
                {
                    hfBookingID.Value = Request.QueryString["BID"].ToString();
                    hfProdID.Value = Request.QueryString["PID"].ToString();
                    // UserDetail objUserDetail = HttpContext.Current.Session["UserDetails"] as UserDetail;
                    hfUpdatedBy.Value = objUserDetail.userID;

                    hidRole.Value = objUserDetail.userRole.ToLower();
                    CommanBinding.BindCompanyDetails(ref ddlCompany, objUserDetail.userID);
                    CommanBinding.BindSourceMedia(ref ddlSourceMedia);
                    BindUser(ref ddlBookingBy);
                    BindUser(ref ddlAssignedTo);


                    // 26 Nov 2020 (Dinesh) Booking By Dropdown is Enabled for Role "operatorft" by Renz 
                    if (hidRole.Value == "admin" || hidRole.Value == "superadmin" ||
                        hidRole.Value == "marketing head" || hidRole.Value == "team head ca" ||
                        hidRole.Value == "onlinetl" || hidRole.Value == "team head ft" || hidRole.Value == "operatorft" || hidRole.Value == "team head")
                    {
                        ddlBookingBy.Enabled = true;
                        ddlCompany.Enabled = true;
                        ddlAssignedTo.Enabled = true;
                    }
                    else if (objUserDetail.userID.ToLower() == "jeremy" ||
                        objUserDetail.userID.ToLower() == "martinft")
                    {
                        ddlBookingBy.Enabled = false;
                        ddlCompany.Enabled = true;
                        ddlAssignedTo.Enabled = true;
                    }

                    else
                    {
                        ddlBookingBy.Enabled = false;
                        ddlCompany.Enabled = false;
                        ddlAssignedTo.Enabled = false;
                    }
                    if (objUserDetail.userID.ToLower() == "renz".ToLower() || objUserDetail.userID.ToLower() == "prabhat".ToLower()
                        || objUserDetail.userID.ToLower() == "rohan".ToLower())
                    {
                        btnHide.Visible = true;
                    }
                    if (objUserDetail.userID.ToLower() == "adminsup".ToLower() || objUserDetail.userID.ToLower() == "pankaj".ToLower()
                         || objUserDetail.userID.ToLower() == "rohan".ToLower())
                    {
                        //chkIsLocked.Visible = true;
                        dvLockBooking.Visible = true;
                        dvDate.Visible = true;
                    }
                    bookingDetails(hfBookingID.Value, hfProdID.Value);

                    //Added BY dinesh 8 Feb  2021 for Aaron and Pankaj if Booking Status is Reissued the Disabled for all agents expect for Pankaj/Aaron
                    if (objUserDetail.userID.ToLower() == "pankaj" || objUserDetail.userID.ToLower() == "aryan" ||
                        objUserDetail.userID.ToLower() == "adminsup")
                    {
                        if (ddlStatus.SelectedValue == "ReIssued" || ddlStatus.SelectedValue == "ChargeBack")
                        {
                            ddlStatus.Enabled = true;
                        }
                    }
                    else
                    {
                        if (ddlStatus.SelectedValue != "ReIssued" && ddlStatus.SelectedValue != "ChargeBack")
                        {
                            ddlStatus.Enabled = true;
                        }
                        else
                        {
                            Response.Redirect("~/Admin/AccessDenied.aspx");
                        }
                    }
                }
            }
            oldStatus = hfOldStatus.Value;
            oldBookingBy = hfOldBookingBy.Value;
            oldAssignedBy = hfOldAssignedBy.Value;
            oldCompany = hfOldCompany.Value;
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }
    protected void btnAddress_Click(object sender, EventArgs e)
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();

        objGetSetDatabase.SET_Contact_Detail(hfBookingID.Value, hfProdID.Value, "", "1", txtPhone.Text.Trim(), txtMobile.Text.Trim(), "", txtEmail.Text.Trim(), DdlCountry.SelectedValue, "", txtCity.Text.Trim(), txtAddress.Text.Trim(), "", "", hfUpdatedBy.Value, "Update");
    }
    protected void btnAddRemarks_Click(object sender, EventArgs e)
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        objGetSetDatabase.SET_Booking_Detail(hfBookingID.Value, hfProdID.Value, "", "", "", "", "", "", "", txtRemarks.Text.Trim(), "", "", "", "", "", hfUpdatedBy.Value, "", "", "Update", "", "");
    }
    protected void rptPrice_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            string SNo = ((Button)(rptPrice.Items[e.Item.ItemIndex].FindControl("btnDelete"))).ToolTip;
            GetSetDatabase objGetSetDatabase = new GetSetDatabase();

            if (objGetSetDatabase.SET_Amount_Charges_Detail(hfBookingID.Value, hfProdID.Value, SNo, "", "", "", "", "", "", "", "", hfUpdatedBy.Value, "Delete") == "true")
            {
                Itinerary.FlightDetails iti = new Itinerary.FlightDetails(hfBookingID.Value, hfProdID.Value, false, false, false, false, false, false, true, false, false);

                #region Bind Pricing
                rptPrice.DataSource = iti.ACD;
                rptPrice.DataBind();
                bookingRem(hfBookingID.Value);
                #endregion

            }
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();

        if (objGetSetDatabase.SET_Amount_Charges_Detail(hfBookingID.Value, hfProdID.Value, "", ddlPayType.Value, ddlChargeFor.Value, txtCostPriceF.Value.Trim(), txtSalePriceF.Value.Trim(), "OK", "", "", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), hfUpdatedBy.Value, "Insert") == "true")
        {

            Itinerary.FlightDetails iti = new Itinerary.FlightDetails(hfBookingID.Value, hfProdID.Value, false, false, false, false, false, false, true, false, false);

            #region Bind Pricing
            rptPrice.DataSource = iti.ACD;
            rptPrice.DataBind();
            bookingRem(hfBookingID.Value);
            #endregion
            txtSalePriceF.Value = "";
            txtCostPriceF.Value = "";

        }
    }

    protected void btnPaxUpdate_Click(object sender, EventArgs e)
    {

        foreach (RepeaterItem i in rptrPax.Items)
        {
            TextBox txtPaxType = (TextBox)i.FindControl("txtPaxType");
            TextBox txtTitle = (TextBox)i.FindControl("txtTitle");
            TextBox txtFirstName = (TextBox)i.FindControl("txtFirstName");
            TextBox txtLastName = (TextBox)i.FindControl("txtLastName");
            TextBox txtTickets = (TextBox)i.FindControl("txtTickets");
            TextBox txtDOB = (TextBox)i.FindControl("txtDOB");
            HiddenField hidBID = (HiddenField)i.FindControl("hidBID");
            HiddenField hidPID = (HiddenField)i.FindControl("hidPID");
            HiddenField hidSNO = (HiddenField)i.FindControl("hidSNO");

            GetSetDatabase objGetSetDatabase = new GetSetDatabase();
            objGetSetDatabase.SET_Passenger_Detail(hidBID.Value, hidPID.Value, hidSNO.Value, "", txtTitle.Text, txtFirstName.Text, "", txtLastName.Text, "", "", "", "", "", "", txtDOB.Text, txtPaxType.Text, "", hfUpdatedBy.Value, "Update", txtTickets.Text);
        }


    }

    private void bookingDetails(string BookingID, string ProdID)
    {

        Itinerary.FlightDetails iti = new Itinerary.FlightDetails(BookingID, ProdID, true, true, true, true, true, true, true, true, true);

        #region special right given to joseph and kim to chnage the Booking By and approved by Pankaj on 24-5-2019
        //if ((objUserDetail.userID.ToLower() == "ronald" || objUserDetail.userID.ToLower() == "kim" || objUserDetail.userID.ToLower() == "joseph" || objUserDetail.userID.ToLower() == "dinesh") && (iti.BD.BookingStatus.ToLower() == "incomplete" || iti.BD.BookingStatus.ToLower() == "decline" || iti.BD.BookingStatus.ToLower() == "option" || iti.BD.BookingStatus.ToLower() == "cancelled" || iti.BD.BookingStatus.ToLower() == "queue" || iti.BD.BookingStatus.ToLower() == "follow up" ))
        if ((objUserDetail.userID.ToLower() == "ronald" || objUserDetail.userID.ToLower() == "kim" || objUserDetail.userID.ToLower() == "joseph" || objUserDetail.userID.ToLower() == "dinesh") && (iti.BD.BookingStatus.ToLower() != "completed"))
        {
            ddlBookingBy.Enabled = true;
            ddlCompany.Enabled = true;
            ddlAssignedTo.Enabled = true;
        }
        #endregion
        bookingRem(BookingID);
        // ddlStatus.Items.Add
        if (iti.BD.BookingStatus.ToLower() == "issued" || iti.BD.BookingStatus.ToLower() == "completed" || iti.BD.BookingStatus.ToLower() == "eticket sent")
        {

            ddlStatus.Items.Add(new ListItem("ETicket Sent", "ETicket Sent"));
            ddlStatus.Items.Add(new ListItem("Completed", "Completed"));
        }
        if (iti.BD.BookingStatus.ToLower() != "issued" && (objUserDetail.userRole.ToLower() == "operator" || objUserDetail.userRole.ToLower() == "operatorjoy" || objUserDetail.userRole.ToLower() == "online" || objUserDetail.userRole.ToLower() == "agentft"))
        {
            ddlStatus.Items.Remove("Issued");

        }

        //if (iti.BD.BookingStatus.ToLower() != "completed" && iti.BD.BookingStatus.ToLower() != "refund" && iti.BD.BookingStatus.ToLower() != "deposit forfeited" || objUserDetail.userRole.ToLower() == "admin" || objUserDetail.userRole.ToLower() == "supadmin")
       
        if(1==1){
            pnlBooking.Visible = true;
            lblmsg.Text = "";
            #region Bind Pricing
            rptPrice.DataSource = iti.ACD;
            rptPrice.DataBind();
            #endregion

            #region bind Passengers
            rptrPax.DataSource = iti.PD;
            rptrPax.DataBind();
            #endregion

            #region bind Sectors
            hfOldvalidating.Value = iti.SM.ValCarrier;
            txtValidatingCarrier.Text = iti.SM.ValCarrier;
            rptrSect.DataSource = iti.SD;
            rptrSect.DataBind();
            #endregion

            #region bind Transactions
            rptrTransaction.DataSource = iti.TM;
            rptrTransaction.DataBind();
            #endregion

            #region binding other details

            lblBookingID.Text = iti.BM.BookingID;
            txtPnr.Text = iti.BD.PNR;
            lblBookkingDate.Text = Convert.ToDateTime(iti.BD.BookingDateTime).ToString("dd MMM yyyy");
            txtDestination.Text = iti.SM.Destination;
            ddlCurrency.SelectedValue = iti.BM.CurrencyType;
            ddlStatus.SelectedValue = iti.BD.BookingStatus;
            hfOldStatus.Value = iti.BD.BookingStatus;
            txtSuppleirRef.Text = iti.BD.SupplierRef;

            ddlBookingBy.SelectedValue = iti.BD.BookingBy;
            ddlAssignedTo.SelectedValue = iti.BD.AssignedTo;


            hfOldBookingBy.Value = iti.BD.BookingBy;
            hfOldCompany.Value = iti.BM.BookingByCompany;
            try
            {
                if (!string.IsNullOrEmpty(iti.SM.IssuedBy))
                { ddlSupplier.SelectedValue = iti.SM.IssuedBy; }

                ddlSourceMedia.SelectedValue = iti.BD.SourceMedia;
            }
            catch { }
            try
            {
                if (!string.IsNullOrEmpty(iti.BM.InvoiceDate))
                    txtInvoiceDate.Text = Convert.ToDateTime(iti.BM.InvoiceDate).ToString("dd/MM/yyyy");
                //txtIssuedDate.Text = Convert.ToDateTime(iti.SM.IssuedDate).ToString("dd/MM/yyyy");
            }
            catch (Exception)
            {
                throw;
            }

            if (iti.BD.BookingByType == "INTR" && (objUserDetail.userRole.ToLower() == "admin" || objUserDetail.userRole.ToLower() == "superadmin"
                 || objUserDetail.userRole.ToLower() == "marketing head") || objUserDetail.userRole == "Team Head FT")
            {
                ddlSourceMedia.Enabled = true;
            }
            else
            {
                ddlSourceMedia.Enabled = false;
            }

            try
            {


                if (!string.IsNullOrEmpty(iti.BM.BookingByCompany))
                {
                    if (ddlCompany.Items.FindByValue(iti.BM.BookingByCompany) == null)
                    {
                        pnlBooking.Visible = false;
                        lblmsg.Text = "You don't have rights to edit it.";
                        return;

                    }
                    else
                    {
                        ddlCompany.SelectedValue = iti.BM.BookingByCompany;
                        pnlBooking.Visible = true;
                        lblmsg.Text = "";
                    }

                }


                if (objUserDetail.userRole.ToLower() == "admin" || objUserDetail.userRole.ToLower() == "superadmin" || objUserDetail.userRole.ToLower() == "team leader" || objUserDetail.userRole.ToLower() == "team head ca")
                {
                    hfOldCompany.Value = iti.BM.BookingByCompany;
                    ddlCompany.Enabled = true;
                }
                else
                { ddlCompany.Enabled = false; }
            }
            catch { }

            try
            {
                //if (!string.IsNullOrEmpty(iti.BD.AtolType))
                //{ ddlATOL.SelectedValue = iti.BD.AtolType; }
            }
            catch { }
            if (iti.TM != null)
            {
                transRow = iti.TM.Count;
            }



            txtAddress.Text = iti.CD.PAddress;
            txtCity.Text = iti.CD.City;
            DdlCountry.SelectedValue = iti.CD.Country;
            txtEmail.Text = iti.CD.EmailID;
            txtPhone.Text = iti.CD.PhoneNo;
            txtMobile.Text = iti.CD.MobileNo;


            #endregion

            #region bind ticket supplier list
            CommanBinding.BindSupplierDetails(ref ddlSupplier, "");
            if (objUserDetail.userRole.ToLower() != "admin" && objUserDetail.userRole.ToLower() != "superadmin" && objUserDetail.userRole.ToLower() != "team head" && objUserDetail.userID.ToLower() != "vick")
            {
                foreach (ListItem LI in ddlSupplier.Items)
                {
                    if (LI.Value != "10950" && LI.Value != "Z1440" && LI.Value != "W0950" && LI.Value != "4067" && LI.Value != "The Flight Trotters")
                    {
                        LI.Attributes["disabled"] = "disabled";

                    }
                    if (ddlCompany.SelectedValue.Contains("FLTTROTT") && LI.Value == "10950")
                    {
                        LI.Attributes["disabled"] = "disabled";
                    }

                }
            }
            #endregion
            if (iti.AD.Count() > 0)
            {
                rptrAuthdoc.DataSource = iti.AD;
                rptrAuthdoc.DataBind();
            }
        }
        else
        {

            pnlBooking.Visible = false;
            lblmsg.Text = "booking has been completed and you don't have rights to edit it.";

        }

    }

    private string bookingRem(string bookingID)
    {
        DataTable dt = new DataTable();
        try
        {
            GetSetDatabase objGetSetDatabase = new GetSetDatabase();
            dt = objGetSetDatabase.GET_Booking_Remarks("SELECT", bookingID);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    bookingRemarks += "<tr><td class='gdvr'>" + dr["Remarks"] + "</td><td class='gdvr'>" + dr["Remarks_By"] + "</td><td class='gdvr'>" + dr["DatenTime"] + "</td></tr>";
                }
            }
            return bookingRemarks;
        }
        catch { return ""; }
    }

    protected void Update_Summary_Click(object sender, EventArgs e)
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        objGetSetDatabase.SET_Booking_Detail(hfBookingID.Value, hfProdID.Value, "", "", "", "", "", "", ddlStatus.SelectedValue, "", "", txtPnr.Text.Trim(), "", "", "", hfUpdatedBy.Value, "", "", "Update", "", "", txtSuppleirRef.Text);

    }

    [WebMethod]
    public static string Update_Summary(string BookingID, string ProdID, string Destination, string PNR, string BookingStatus,
        string UpdatedBy, string Supplier, string AtolType, string Company, string SupplierRef, string Role, string BookingBy,
        string SourceMedia, string IssuedDate, string ValidatingCarrier, string AssignedTo, string GDS)
    {

        if (BookingStatus.ToLower() == "completed" || BookingStatus.ToLower() == "deposit forfeited")
        {
            //only admin and supadmin can mark a booking as completed
            if (Role == "admin" || Role == "superadmin")
            {
                return ChangeStatus(BookingID, ProdID, Destination, PNR, BookingStatus, UpdatedBy,
                    Supplier, AtolType, Company, SupplierRef, Role, BookingBy, SourceMedia, IssuedDate, ValidatingCarrier, AssignedTo, GDS);
            }
            else
            {
                GetSetDatabase objGetSetDatabase = new GetSetDatabase();
                objGetSetDatabase.SET_Booking_Detail(BookingID, ProdID, "", "", "", "", "", "", "",
                "Don't have rights to mark booking as completed or deposit forfeited", "", "", "", "",
                "", UpdatedBy + "/Sys", "", "", "Update", "", AssignedTo, "", "", GDS);
                return "false";
            }
        }
        else
        {
            return ChangeStatus(BookingID, ProdID, Destination, PNR, BookingStatus, UpdatedBy, Supplier, AtolType, Company, SupplierRef,
                Role, BookingBy, SourceMedia, IssuedDate, ValidatingCarrier, AssignedTo, GDS);
        }
    }


    private static bool GenerateInvoiceNumber(string bookingRef, string bookingStatus)
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();

        DataTable dt = objGetSetDatabase.GET_Booking_Master(bookingRef);
        if (dt != null && dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["InvoiceNo"].ToString() == "" || dt.Rows[0]["InvoiceNo"].ToString() == null)
            {
                GetSetDatabase GetSetDatabase = new GetSetDatabase();
                string prefix = string.Empty;
                if (dt.Rows[0]["GroupCompanyID"].ToString() == "FLTTRT")
                    prefix = "INVFTTRT";
                else
                    prefix = "INVFNH";
                string invoiceNumber = GetSetDatabase.GenerateIDs(prefix);
                invoiceNumber = invoiceNumber.Replace(prefix.ToUpper(), "");
                if (objGetSetDatabase.SET_Booking_Master(bookingRef, invoiceNumber, "", "", "", "", "update") == "false")
                    return false;
                else return true;

            }
            else { return true; }
        }
        else return false;
    }

    private static string ChangeStatus(string BookingID, string ProdID, string Destination, string PNR, string BookingStatus,
        string UpdatedBy, string Supplier, string AtolType, string Company, string SupplierRef, string Role, string bookingBy,
        string SourceMedia, string IssuedDate, string ValidatingCarrier, string AssignedTo, string GDS)
    {

        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        #region recheck details
        Itinerary.FlightDetails temp_iti = new Itinerary.FlightDetails(BookingID, ProdID, true, true, true, false, false, false, true, true, false);
        string oldStatus = temp_iti.BD.BookingStatus;
        string suppleirRef = temp_iti.BD.SupplierRef;
        string oldBookingBy = temp_iti.BD.BookingBy;
        string oldAssignedBy = temp_iti.BD.AssignedTo;
        string oldCompany = temp_iti.BM.BookingByCompany;
        double profit = 0.0;
        try
        {
            int totalPax = temp_iti.PD.Count();
            double totalCost = (temp_iti.ACD.Sum(x => Convert.ToDouble(x.CostPrice)) * Convert.ToInt16(temp_iti.ACD[0].NoOfPax));
            double totalSell = (temp_iti.ACD.Sum(x => Convert.ToDouble(x.SellPrice)) * Convert.ToInt16(temp_iti.ACD[0].NoOfPax));

            profit = (totalSell - totalCost);
        }
        catch (Exception)
        {

            throw;
        }


        int transRow = 0;
        if (temp_iti.TM != null)
        {
            transRow = temp_iti.TM.Count;
        }
        #endregion
        if (objGetSetDatabase.SET_Booking_Detail(BookingID, ProdID, "", bookingBy, "", "", "", "",
                BookingStatus, "", "", PNR.ToUpper().Trim(), SourceMedia, "", "",
                UpdatedBy, "", "", "Update", AtolType, AssignedTo, SupplierRef, Company, GDS) == "true")
        {

            if (BookingStatus != oldStatus)
            {
                string remarks = string.Empty;
                if (BookingStatus.ToLower() == "queue" && transRow == 0)
                {
                    remarks = "System generated remarks - Booking Status changed from " + oldStatus + " to " + BookingStatus + " BUT PAYMENT NOT RECIEVED";
                }
                else if (BookingStatus.ToLower() == "issued")
                {

                    remarks = "System generated remarks - Booking Status changed from " + oldStatus + " to " + BookingStatus + " and current profit is " + profit.ToString();
                }
                else
                {
                    remarks = "System generated remarks - Booking Status changed from " + oldStatus + " to " + BookingStatus;
                }
                objGetSetDatabase.SET_Booking_Detail(BookingID, ProdID, "", "", "", "", "", "", "", remarks, "", "", "", "", "", UpdatedBy + "/Sys", "", "", "Update", "", "");
            }
            if (!string.IsNullOrEmpty(Company))
            {
                if (Company != oldCompany)
                {
                    string remarks = string.Empty;
                    remarks = "System generated remarks - Booking Comapny changed from " + oldCompany + " to " + Company;
                    objGetSetDatabase.SET_Booking_Detail(BookingID, ProdID, "", "", "", "", "", "", "", remarks, "", "", "",
                     "", "", UpdatedBy + "/Sys", "", "", "Update", "", "");
                }
            }

            if (oldAssignedBy != AssignedTo)
            {
                string remarks = string.Empty;
                remarks = "System generated remarks -Booking assigned to changed from " + oldAssignedBy + " to " + AssignedTo;
                objGetSetDatabase.SET_Booking_Detail(BookingID, ProdID, "", "", "", "", "", "", "", remarks, "", "", "", "", "",
                    UpdatedBy + "/Sys", "", "", "Update", "", AssignedTo);

                objGetSetDatabase.BookingAssignement(BookingID, AssignedTo, UpdatedBy, "", "", "INSERT", "", "");
            }
            if (oldBookingBy != bookingBy)
            {
                string remarks = string.Empty;
                remarks = "System generated remarks -Booking by changed from " + oldBookingBy + " to " + bookingBy;
                objGetSetDatabase.SET_Booking_Detail(BookingID, ProdID, "", "", "", "", "", "", "", remarks, "", "", "", "", "",
                    UpdatedBy + "/Sys", "", "", "Update", "", bookingBy);
            }
            if (BookingStatus.ToLower() == "issued" || BookingStatus.ToLower() == "deposit forfeited")
            {
                //generate invoice number if already not generated
                GenerateInvoiceNumber(BookingID, BookingStatus);
                FandHServices.FandHServicesClient objServices = new FandHServices.FandHServicesClient();
                DataTable dt = objServices.GET_Agents_P_L(BookingID, null, null, null, Company, BookingStatus);
                if (dt != null && dt.Rows.Count > 0)
                {
                    string remarks = string.Empty;
                    remarks = "System generated remarks - Total Profit is " + dt.Rows[0]["Profit_Amout"].ToString() + " when invoice is generated";
                    objGetSetDatabase.SET_Booking_Detail(BookingID, ProdID, "", "", "", "", "", "", "", remarks, "", "", "", "", "",
                        UpdatedBy + "/Sys", "", "", "Update", "", AssignedTo);
                }
            }
            return objGetSetDatabase.SET_Sectors_Master(BookingID, ProdID, "", "", "", Destination, ValidatingCarrier, "",
                UpdatedBy, "Update", Supplier, "");
        }
        else { return "false"; }


    }

    [WebMethod]
    public static string Update_Address(string BookingID, string ProdID, string Phone, string Mobile, string Email, string Country, string City, string Address, string UpdatedBy)
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        return objGetSetDatabase.SET_Contact_Detail(BookingID, ProdID, "", "1", Phone.Trim(), Mobile.Trim(), "", Email.Trim(), Country, "", City.Trim(), Address.Trim(), "", "", UpdatedBy, "Update");

        //if (objGetSetDatabase.SET_Contact_Detail(BookingID, ProdID, "", "1", Phone.Trim(), Mobile.Trim(), "", Email.Trim(), Country, "", City.Trim(), Address.Trim(), "", "", UpdatedBy, "Update") !="false")
        //{

        //    string remarks = "Contact Details Changed -  " + Value;
        //    objGetSetDatabase.SET_Booking_Detail(BookingID, "001", "", "", "", "", "", "", "", remarks, "", "", "", "", "", UpdatedBy + "/Sys", "", "", "Update", "", "");

        //    return "true";
        //}
        //else
        //    return "false";
    }

    [WebMethod]
    public static string AddRemarks(string BookingID, string ProdID, string Remarks, string UpdatedBy)
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        return objGetSetDatabase.SET_Booking_Detail(BookingID, ProdID, "", "", "", "", "", "", "", Remarks.Trim(), "", "", "", "", "", UpdatedBy, "", "", "Update", "", "");

    }
    [WebMethod]
    public static string ChangeXPDate(string BookingID, string UpdatedBy, string BookingDate)
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        return objGetSetDatabase.SET_Booking_Date(BookingID, UpdatedBy, BookingDate) != 0 ? "" : null;

    }

    protected void rptrSect_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Button delButton = (Button)e.Item.FindControl("btnDelete");
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (e.Item.ItemIndex == 0)
            {
                delButton.Visible = false;
            }
        }



    }
    protected void rptrTransaction_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Literal lit = (Literal)e.Item.FindControl("TrnsNo");

            TextBox txtTransAmount = (TextBox)e.Item.FindControl("txtTrnsAmount" + lit.Text);

            if (txtTransAmount != null)
            {
                if (hidRole.Value == "admin" || hidRole.Value == "superadmin" || hidRole.Value == "marketing head" || hidRole.Value == "team leader")
                {
                    txtTransAmount.Enabled = true;

                }
                else { txtTransAmount.Enabled = false; }
            }
        }
    }

    protected void rptPrice_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //Button delButton = (Button)e.Item.FindControl("btnDelete");
        //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //{
        //    if (e.Item.ItemIndex == 0)
        //    { delButton.Visible = false; }
        //}
    }

    protected void rptrPax_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }

    protected void rptrPax_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Button delButton = (Button)e.Item.FindControl("btnDelete");
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (e.Item.ItemIndex == 0)
            { delButton.Visible = false; }
        }
    }

    protected void rptrSect_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }

    [WebMethod]
    public static string DeletePax(string SrNo)
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        if (objGetSetDatabase.SET_Passenger_Detail("", "", SrNo, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "Delete", "") == "true")
        {

            return "true";
        }
        else
        {
            return "false";
        }



    }

    [WebMethod]
    public static string DeleteSector(string SrNo)
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        if (objGetSetDatabase.SET_Sector_Detail("", "", SrNo, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "Delete", "", "") == "true")
        {
            return "true";
        }
        else
        {
            return "false";
        }



    }

    [WebMethod]
    public static string UpdatePassenger(string BookingID, string ProdID, string ID, string UpdateField, string Value, string UpdatedBy)
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        string recordUpdated = "false";
        string remarks = string.Empty;
        Value = Value.ToUpper();
        switch (UpdateField)
        {

            case "PaxType":
                recordUpdated = objGetSetDatabase.SET_Passenger_Detail("", "", ID, "", "", "", "", "", "", "", "", "", "", "", "", Value, "", UpdatedBy, "Update", "");


                remarks = "System generated remarks - PaxType changed  to " + Value + " SNO - " + ID;
                objGetSetDatabase.SET_Booking_Detail(BookingID, "001", "", "", "", "", "", "", "", remarks, "", "", "", "", "", UpdatedBy + "/Sys", "", "", "Update", "", "");

                break;
            case "Title":
                recordUpdated = objGetSetDatabase.SET_Passenger_Detail("", "", ID, "", Value, "", "", "", "", "", "", "", "", "", "", "", "", UpdatedBy, "Update", "");
                remarks = "System generated remarks - Title changed  to " + Value + " SNO - " + ID;
                objGetSetDatabase.SET_Booking_Detail(BookingID, "001", "", "", "", "", "", "", "", remarks, "", "", "", "", "", UpdatedBy + "/Sys", "", "", "Update", "", "");
                break;
            case "FirstName":
                recordUpdated = objGetSetDatabase.SET_Passenger_Detail("", "", ID, "", "", Value, "", "", "", "", "", "", "", "", "", "", "", UpdatedBy, "Update", "");
                remarks = "System generated remarks - FirstName changed  to " + Value + " SNO - " + ID;
                objGetSetDatabase.SET_Booking_Detail(BookingID, "001", "", "", "", "", "", "", "", remarks, "", "", "", "", "", UpdatedBy + "/Sys", "", "", "Update", "", "");
                break;
            case "MidName":
                recordUpdated = objGetSetDatabase.SET_Passenger_Detail("", "", ID, "", "", "", Value, "", "", "", "", "", "", "", "", "", "", UpdatedBy, "Update", "");
                remarks = "System generated remarks - Middle Name changed  to " + Value + " SNO - " + ID;
                objGetSetDatabase.SET_Booking_Detail(BookingID, "001", "", "", "", "", "", "", "", remarks, "", "", "", "", "", UpdatedBy + "/Sys", "", "", "Update", "", "");
                break;
            case "LastName":
                recordUpdated = objGetSetDatabase.SET_Passenger_Detail("", "", ID, "", "", "", "", Value, "", "", "", "", "", "", "", "", "", UpdatedBy, "Update", "");
                remarks = "System generated remarks - LastName changed  to " + Value + " SNO - " + ID;
                objGetSetDatabase.SET_Booking_Detail(BookingID, "001", "", "", "", "", "", "", "", remarks, "", "", "", "", "", UpdatedBy + "/Sys", "", "", "Update", "", "");
                break;
            case "Tickets":
                recordUpdated = objGetSetDatabase.SET_Passenger_Detail("", "", ID, "", "", "", "", "", "", "", "", "", "", "", "", "", "", UpdatedBy, "Update", Value);
                remarks = "System generated remarks - Tickets changed  to " + Value + " SNO - " + ID;
                objGetSetDatabase.SET_Booking_Detail(BookingID, "001", "", "", "", "", "", "", "", remarks, "", "", "", "", "", UpdatedBy + "/Sys", "", "", "Update", "", "");
                break;
            case "DOB":
                if (validateDate(Value))
                {
                    recordUpdated = objGetSetDatabase.SET_Passenger_Detail("", "", ID, "", "", "", "", "", "", "", "", "", "", "", Convert.ToDateTime(Value).ToString("yyyy/MM/dd"), "", "", UpdatedBy, "Update", "");
                    remarks = "System generated remarks - DOB changed  to " + Value + " SNO - " + ID;
                    objGetSetDatabase.SET_Booking_Detail(BookingID, "001", "", "", "", "", "", "", "", remarks, "", "", "", "", "", UpdatedBy + "/Sys", "", "", "Update", "", "");
                }
                else
                    recordUpdated = "false";
                break;

        }
        return recordUpdated;


    }

    [WebMethod]
    public static string UpdateSector(string BookingID, string ProdID, string ID, string UpdateField, string Value, string UpdatedBy)
    {
        string remarks = string.Empty;
        if (UpdateField == "CabinClass" || UpdateField == "BaggageAllownce")
        {
            Value = Value;
        }
        else
        {
            Value = Value.ToUpper();
        }
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        string recordUpdated = "false";

        switch (UpdateField)
        {
            case "From":
                recordUpdated = objGetSetDatabase.SET_Sector_Detail(BookingID, ProdID, ID, "", Value, "",
                    "", "", "", "", "", "", "", "", "", "", "", "", "", "", UpdatedBy, "Update", "", "");
                remarks = "System generated remarks - Departure changed  to " + Value + "SNO - " + ID;
                objGetSetDatabase.SET_Booking_Detail(BookingID, "001", "", "", "", "", "", "", "", remarks, "", "", "", "", "", UpdatedBy + "/Sys", "", "", "Update", "", "");
                break;
            case "To":
                recordUpdated = objGetSetDatabase.SET_Sector_Detail(BookingID, ProdID, ID, "", "", "", Value,
                    "", "", "", "", "", "", "", "", "", "", "", "", "", UpdatedBy, "Update", "", "");
                remarks = "System generated remarks - Arrival changed  to " + Value + "SNO - " + ID;
                objGetSetDatabase.SET_Booking_Detail(BookingID, "001", "", "", "", "", "", "", "", remarks, "", "", "", "", "", UpdatedBy + "/Sys", "", "", "Update", "", "");
                break;
            case "AirV":
                recordUpdated = objGetSetDatabase.SET_Sector_Detail(BookingID, ProdID, ID, Value, "",
                    "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", UpdatedBy, "Update", "", "");
                remarks = "System generated remarks - AirV changed  to " + Value + "SNO - " + ID;
                objGetSetDatabase.SET_Booking_Detail(BookingID, "001", "", "", "", "", "", "", "", remarks, "", "", "", "", "", UpdatedBy + "/Sys", "", "", "Update", "", "");
                break;
            case "FLTNO":
                recordUpdated = objGetSetDatabase.SET_Sector_Detail(BookingID, ProdID, ID, "", "", "",
                    "", "", Value, "", "", "", "", "", "", "", "", "", "", "", UpdatedBy, "Update", "", "");
                remarks = "System generated remarks - FLTNO changed  to " + Value + "SNO - " + ID;
                objGetSetDatabase.SET_Booking_Detail(BookingID, "001", "", "", "", "", "", "", "", remarks, "", "", "", "", "", UpdatedBy + "/Sys", "", "", "Update", "", "");
                break;
            case "Class":

                recordUpdated = objGetSetDatabase.SET_Sector_Detail(BookingID, ProdID, ID, "", "", "",
                    "", "", "", Value, "", "", "", "", "", "", "", "", "", "", UpdatedBy, "Update", "", "");
                remarks = "System generated remarks - Class changed  to " + Value + "SNO - " + ID;
                objGetSetDatabase.SET_Booking_Detail(BookingID, "001", "", "", "", "", "", "", "", remarks, "", "", "", "", "", UpdatedBy + "/Sys", "", "", "Update", "", "");
                break;
            case "FromDate":

                recordUpdated = objGetSetDatabase.SET_Sector_Detail(BookingID, ProdID, ID, "", "", Value,
                    "", "", "", "", "", "", "", "", "", "", "", "", "", "", UpdatedBy, "Update", "", "");
                remarks = "System generated remarks - FromDate changed  to " + Value + "SNO - " + ID;
                objGetSetDatabase.SET_Booking_Detail(BookingID, "001", "", "", "", "", "", "", "", remarks, "", "", "", "", "", UpdatedBy + "/Sys", "", "", "Update", "", "");

                break;
            case "FromTime":
                recordUpdated = objGetSetDatabase.SET_Sector_Detail(BookingID, ProdID, ID, "", "",
                    objGetSetDatabase.TimeFormat(Value), "", "", "", "", "", "", "", "", "", "", "", "", "", "", UpdatedBy, "Update", "", "");
                remarks = "System generated remarks - FromTime changed  to " + Value + "SNO - " + ID;
                objGetSetDatabase.SET_Booking_Detail(BookingID, "001", "", "", "", "", "", "", "", remarks, "", "", "", "", "", UpdatedBy + "/Sys", "", "", "Update", "", "");
                break;
            case "ToDate":
                recordUpdated = objGetSetDatabase.SET_Sector_Detail(BookingID, ProdID, ID, "", "", "", "", Value,
                    "", "", "", "", "", "", "", "", "", "", "", "", UpdatedBy, "Update", "", "");
                remarks = "System generated remarks - ToDate changed  to " + Value + "SNO - " + ID;
                objGetSetDatabase.SET_Booking_Detail(BookingID, "001", "", "", "", "", "", "", "", remarks, "", "", "", "", "", UpdatedBy + "/Sys", "", "", "Update", "", "");
                break;
            case "ToTime":
                recordUpdated = objGetSetDatabase.SET_Sector_Detail(BookingID, ProdID, ID, "", "", "", "",
                   objGetSetDatabase.TimeFormat(Value), "", "", "", "", "", "", "", "", "", "", "", "", UpdatedBy, "Update", "", "");
                remarks = "System generated remarks - ToTime changed  to " + Value + "SNO - " + ID;
                objGetSetDatabase.SET_Booking_Detail(BookingID, "001", "", "", "", "", "", "", "", remarks, "", "", "", "", "", UpdatedBy + "/Sys", "", "", "Update", "", "");
                break;
            case "Status":
                recordUpdated = objGetSetDatabase.SET_Sector_Detail(BookingID, ProdID, ID, "",
                    "", "", "", "", "", "", Value, "", "", "", "", "", "", "", "", "", UpdatedBy, "Update", "", "");
                remarks = "System generated remarks - Status changed  to " + Value + "SNO - " + ID;
                objGetSetDatabase.SET_Booking_Detail(BookingID, "001", "", "", "", "", "", "", "", remarks, "", "", "", "", "", UpdatedBy + "/Sys", "", "", "Update", "", "");
                break;
            case "TripType":
                recordUpdated = objGetSetDatabase.SET_Sector_Detail(BookingID, ProdID, ID, "",
                    "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", Value, UpdatedBy, "Update", "", "");
                remarks = "System generated remarks - Status changed  to " + Value + "SNO - " + ID;
                objGetSetDatabase.SET_Booking_Detail(BookingID, "001", "", "", "", "", "", "", "", remarks, "", "", "", "", "", UpdatedBy + "/Sys", "", "", "Update", "", "");
                break;
            case "AirlineConfirmationCode":
                recordUpdated = objGetSetDatabase.SET_Sector_Detail(BookingID, ProdID, ID, "",
                    "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", UpdatedBy, "Update", Value, "");
                remarks = "System generated remarks - Status changed  to " + Value + "SNO - " + ID;
                objGetSetDatabase.SET_Booking_Detail(BookingID, "001", "", "", "", "", "", "", "", remarks, "", "", "", "", "", UpdatedBy + "/Sys", "", "", "Update", "", "");
                break;
            case "CabinClass":
                recordUpdated = objGetSetDatabase.SET_Sector_Detail(BookingID, ProdID, ID, "",
                    "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", UpdatedBy, "Update", "", Value);

                remarks = "System generated remarks - Status changed  to " + Value + "SNO - " + ID;
                objGetSetDatabase.SET_Booking_Detail(BookingID, "001", "", "", "", "", "", "", "", remarks, "", "", "", "", "", UpdatedBy + "/Sys", "", "", "Update", "", "");
                break;
            case "BaggageAllownce":
                recordUpdated = objGetSetDatabase.SET_Sector_Detail(BookingID, ProdID, ID, "",
                    "", "", "", "", "", "", "", "", "", "", Value, "", "", "", "", "", UpdatedBy, "Update", "", "");

                remarks = "System generated remarks - Status changed  to " + Value + "SNO - " + ID;
                objGetSetDatabase.SET_Booking_Detail(BookingID, "001", "", "", "", "", "", "", "", remarks, "", "", "", "", "", UpdatedBy + "/Sys", "", "", "Update", "", "");
                break;
        }

        return recordUpdated;
    }

    [WebMethod]
    public static string UpdateTransaction(string BookingID, string TrnID, string UpdateField, string Value, string UpdatedBy, string Role)
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        string recordUpdated = "false";
        Value = Value.ToUpper();
        if (Role.ToLower() == "admin" || Role.ToLower() == "superadmin" || Role.ToLower() == "marketing head" || Role.ToLower() == "team leader")
        {
            switch (UpdateField)
            {
                case "TrnsAmount":
                    recordUpdated = objGetSetDatabase.SET_Transaction_Master(BookingID, TrnID, "", "", Value, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", UpdatedBy, "Update");
                    break;
            }

            if (recordUpdated == "true")
            {

                string remarks = "System generated remarks - Transaction Amount changed  to " + Value;
                objGetSetDatabase.SET_Booking_Detail(BookingID, "001", "", "", "", "", "", "", "", remarks, "", "", "", "", "", UpdatedBy + "/Sys", "", "", "Update", "", "");
            }
        }
        return recordUpdated;

    }

    private static Boolean validateDate(string fecha)
    {
        DateTime dDate;
        // return DateTime.TryParseExact(fecha, "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dDate);
        return true;
    }



    private void BindUser(ref DropDownList ddlBookingBy)
    {

        GetSetDatabase objGetSetDatabase = new GetSetDatabase();

        DataTable dtUser = objGetSetDatabase.GET_UserAccount("", "", "", "", "INTR", "", "");
        if (dtUser != null)
        {
            foreach (DataRow dr in dtUser.Rows)
            {
                if (dr["UserID"].ToString().ToLower() != "adminsup" && dr["UserID"].ToString().ToLower() != "admin")
                    ddlBookingBy.Items.Add(new ListItem(dr["UserID"].ToString(), dr["UserID"].ToString()));
            }

            ddlBookingBy.Items.Insert(0, new ListItem("Online", "1"));

        }

    }

    protected void btnAddpax_Click(object sender, EventArgs e)
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();

        if (objGetSetDatabase.SET_Passenger_Detail(hfBookingID.Value, hfProdID.Value, "", "",
            txtTitle.Text, txtFName.Text, txtMName.Text, txtLName.Text, "", "", "", "", "", "", txtDOB.Text, ddlType.SelectedValue, "",
            hfUpdatedBy.Value, "Insert", txtTickets.Text) == "true")
        {
            Itinerary.FlightDetails iti = new Itinerary.FlightDetails(hfBookingID.Value, hfProdID.Value, false, false, true, false, false, false, true, false, false);

            #region Bind Pax
            rptrPax.DataSource = iti.PD;
            rptrPax.DataBind();
            txtTitle.Text = "";
            txtFName.Text = "";
            txtMName.Text = "";
            txtTickets.Text = "";
            txtLName.Text = "";
            txtDOB.Text = "";
            #endregion

        }
        bookingRem(hfBookingID.Value);
    }

    protected void btnAddSectors_Click(object sender, EventArgs e)
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        if (objGetSetDatabase.SET_Sector_Detail(hfBookingID.Value, hfProdID.Value, "", txtAirline.Text, txtFrom.Text, txtFromDate.Text + " " + objGetSetDatabase.TimeFormat(txtFromTime.Text), txtTo.Text, txtToDate.Text + " " + objGetSetDatabase.TimeFormat(txtToTime.Text),
             txtFlightNo.Text, txtClass.Text, txtStatus.Text, "", "", "", txtBaggageAllownce.Text, "", "", "", "", txtTripID.Text, hfUpdatedBy.Value, "InsertSecOnly", txtAirlineConfirmationCode.Text, ddlCabin.SelectedValue) == "true")
        {

            Itinerary.FlightDetails iti = new Itinerary.FlightDetails(hfBookingID.Value, hfProdID.Value, false, false, false, false, false, true, true, false, false);

            #region Bind Sectors
            rptrSect.DataSource = iti.SD;
            rptrSect.DataBind();
            #endregion
            txtAirline.Text = "";
            txtFrom.Text = "";
            txtFromDate.Text = "";
            txtFromTime.Text = "";
            txtFlightNo.Text = "";
            txtClass.Text = "";
            txtStatus.Text = "";
            txtTo.Text = "";
            txtToDate.Text = "";
            txtToTime.Text = "";
            txtAirlineConfirmationCode.Text = "";
            txtTripID.Text = "";
        }
        bookingRem(hfBookingID.Value);

    }

    protected void btnDeletePrice_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem item in rptPrice.Items)
        {
            string SNo = ((Button)(rptPrice.Items[item.ItemIndex].FindControl("btnDelete"))).ToolTip;
            GetSetDatabase objGetSetDatabase = new GetSetDatabase();
            objGetSetDatabase.SET_Amount_Charges_Detail(hfBookingID.Value, hfProdID.Value, SNo, "", "", "", "", "", "", "", "", hfUpdatedBy.Value, "Delete");
        }
        Itinerary.FlightDetails iti = new Itinerary.FlightDetails(hfBookingID.Value, hfProdID.Value, false, false, false, false, false, false, true, false,false);
        //#region Bind Pricing
        rptPrice.DataSource = iti.ACD;
        rptPrice.DataBind();
        bookingRem(hfBookingID.Value);
        //#endregion
    }

    protected void UploadBtn_Click(object sender, EventArgs e)
    {
        System.Web.HttpPostedFile objHttpPostedFile = FileUpLoad1.PostedFile;
        if (FileUpLoad1.HasFile)
        {
            var fileName= DocType.SelectedValue+"_"+hfBookingID.Value + "__" + FileUpLoad1.FileName;

            var path = Server.MapPath("../../Docs/")+ fileName;

            FileUpLoad1.SaveAs(path);

            if (File.Exists(path))
            {
                GetSetDatabase objGetSetDatabase = new GetSetDatabase();

                if (objGetSetDatabase.Get_Set_Doc_Upload(DocType.SelectedValue, "../../Docs/" + fileName, hfBookingID.Value))
                {
                    Itinerary.FlightDetails iti = new Itinerary.FlightDetails(hfBookingID.Value, hfProdID.Value, false, false, true, false, false, false, false, false, true);

                    //#region Bind Pax
                    rptrAuthdoc.DataSource = iti.AD;
                    rptrAuthdoc.DataBind();
                    //txtTitle.Text = "";
                    //txtFName.Text = "";
                    //txtMName.Text = "";
                    //txtTickets.Text = "";
                    //txtLName.Text = "";
                    //txtDOB.Text = "";
                    //#endregion

                }
                bookingRem(hfBookingID.Value);
            }
        }
        else
        {
            Label1.Text = "No File Uploaded.";
        }
       
    }

    protected void btnHide_Click(object sender, EventArgs e)
    {
        GetSetDatabase objhide = new GetSetDatabase();
        int i = objhide.HideDupe(Request.QueryString["BID"]);
        if (i != 0) lblmsg.Text = "booking deleted now"; else lblmsg.Text = "try again.";
        bookingRem(hfBookingID.Value);
    }



    [WebMethod]
    public static string Update_Pax_Details(string BookingID,string ProdID, string PaxID, string PaxType, string PaxTitle, 
        string PaxFirstName, string PaxMiddleName,string PaxLastName, string PaxTickets, string PaxDOB, string UpdatedBy)
    {

        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        string remarks = "System generated remarks - Pax Details updated  for SNO - " + PaxID;

        var result = objGetSetDatabase.Update_Pax_Details(BookingID,
                                                          ProdID,
                                                          PaxID,
                                                          PaxType,
                                                          PaxTitle,
                                                          PaxFirstName,
                                                          PaxMiddleName,
                                                          PaxLastName,
                                                          PaxTickets,
                                                          PaxDOB,
                                                          UpdatedBy);

        objGetSetDatabase.SET_Booking_Detail(BookingID, ProdID, "", "", "", "", "", "", "", remarks, "", "", "", "", "", UpdatedBy + "/Sys", "", "", "Update", "", "");
        return "true";
    }
}




