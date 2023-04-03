using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Threading;
using System.Globalization;


public partial class Admin_FltBooking_ManualBooking : System.Web.UI.Page
{
    public static DataTable dtPrice { set; get; }
    UserDetail objUserDetail;
    BLL.SaveInDB DB = new SaveInDB();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserDetails"] != null)
        {
            objUserDetail = Session["UserDetails"] as UserDetail;
            if (!IsPostBack)
            {

                if (!objUserDetail.isAuth("Manual Booking"))
                {
                    Response.Redirect("~/Admin/AccessDenied.aspx");
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        
        BLL.FlightsBL fb = new BLL.FlightsBL();
        List<EL.Flight.Itinerary> Itineraries = new List<EL.Flight.Itinerary>();
       
        Itineraries.Add(new EL.Flight.Itinerary());
        int adts = Convert.ToInt16(ddlAdults.SelectedValue);
        int chds = Convert.ToInt16(ddlChildren.SelectedValue);
        int infs = Convert.ToInt16(ddlInfants.SelectedValue);

        int sectors = Convert.ToInt16(ddlSectors.SelectedValue);
   

        #region Add Passengers

        for (int i = 0; i < adts; i++)
        {

            EL.PassengerDetail p = new EL.PassengerDetail();
            p.PassengerType = "ADT";
            Itineraries[0].PassengersDetails.Add(p);
        }

        for (int i = 0; i < chds; i++)
        {

            EL.PassengerDetail p = new EL.PassengerDetail();
            p.PassengerType = "CHD";
            Itineraries[0].PassengersDetails.Add(p);
        }

        for (int i = 0; i < infs; i++)
        {

            EL.PassengerDetail p = new EL.PassengerDetail();
            p.PassengerType = "INF";
            Itineraries[0].PassengersDetails.Add(p);
        }
        #endregion

        #region Add Sectors
        for (int i = 0; i < sectors; i++)
        {
            EL.Flight.Itinerary_Sector s = new EL.Flight.Itinerary_Sector();
            
           Itineraries[0].Sectors.Add(s);


        }

        #endregion

        if (Itineraries.Count > 0)
        {
            if (!string.IsNullOrEmpty(Itineraries[0].Warnings))
            {
                pnlBooking.Visible = false;
                lblMsg.Text = Itineraries[0].Warnings;
            }
            else
            {
                lblDestination.InnerText = txtDestination.Text;
                lblPNR.InnerText = txtPnr.Text;
                pnlBooking.Visible = true;
                rptrSect.DataSource = Itineraries[0].Sectors;
                rptrSect.DataBind();
                rptrPax.DataSource = Itineraries[0].PassengersDetails;
                rptrPax.DataBind();
               
                pnlAddress.Visible = true;

                //ListtoDataTableConverter converter = new ListtoDataTableConverter();
                //dtFlight = converter.ToDataTable(itin.Items[0].Sectors);
                //dtPax = converter.ToDataTable(itin.Items[0].PassengersDetails);
            }
        }
        else
        {
            pnlBooking.Visible = false;
        }
        //if(ddlCompany.SelectedValue=="FFDeal")
        //{
        //    rptrSect.Visible = false;
        //    pnlManulaSector.Visible = true;
        //}

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
        GetSetDatabase objsave = new GetSetDatabase();
        DataTable pdt = DB.CreatePaxDataTable();
        int paxid = 0;

        if (dtPrice == null || dtPrice.Rows.Count == 0)
        {
            lblMsg.Text = "Sorry Unable to create booking, Please Add pricing to create new booking";
            return;
        }
        foreach (RepeaterItem rt in rptrPax.Items)
        {

            DataRow dr = pdt.NewRow();
            dr["BOK_MST_Booking_ID"] = "";
            dr["BOK_DTL_Prod_Booking_ID"] = "";
            dr["PAX_DTL_Pax_ID"] = ++paxid;
            dr["PAX_DTL_Title"] = ((HtmlInputText)(rt.FindControl("txtTitle"))).Value;
            dr["PAX_DTL_Pax_First_Name"] = ((HtmlInputText)(rt.FindControl("txtFirstName"))).Value.Trim();
            dr["PAX_DTL_Pax_Middle_Name"] = ((HtmlInputText)(rt.FindControl("txtMiddleName"))).Value.Trim();
            dr["PAX_DTL_Pax_Last_Name"] = ((HtmlInputText)(rt.FindControl("txtLastName"))).Value.Trim();
            dr["PAX_DTL_Frequent_Flyer_No"] = "";
            dr["PAX_DTL_Passport_No"] = "";
            dr["PAX_DTL_Nationality"] = "";
            dr["PAX_DTL_Expiry_Date"] = Convert.ToDateTime("01-01-2000");
            dr["PAX_DTL_Place_of_Issue"] = "";
            dr["PAX_DTL_Place_of_Birth"] = "";
            //dr["PAX_DTL_Pax_DOB"] = Convert.ToDateTime(((HtmlInputText)(rt.FindControl("txtDOB"))).Value).ToString("yyyy/MM/dd");
            try
            {
                HtmlInputText DOB = (HtmlInputText)rt.FindControl("txtDOB");
                if (!string.IsNullOrEmpty(DOB.Value.Trim()))
                    dr["PAX_DTL_Pax_DOB"] = Convert.ToDateTime(DOB.Value.Trim()).ToString("dd/MM/yyyy"); ;

            }
            catch { dr["PAX_DTL_Pax_DOB"] = Convert.ToDateTime("01-01-1900"); }

            dr["PAX_DTL_Pax_Type"] = ((HtmlInputText)(rt.FindControl("txtPaxType"))).Value;
            dr["PAX_DTL_Pax_Sex"] = "";
            dr["PAX_DTL_ModifiedBy"] = objUserDetail.userID;
            dr["PAX_DTL_ModifiedDate"] = DateTime.Now;
            pdt.Rows.Add(dr);

        }

        DataTable sdt = DB.CreateSectorDataTable();

        int segID = 0;
        foreach (RepeaterItem ris in rptrSect.Items)
        {
            DataRow dr = sdt.NewRow();
            string fromDate = string.Empty;
            string toDate = string.Empty;
            if (!((TextBox)(ris.FindControl("FromDate"))).Text.Contains("/"))
            {
                fromDate = (((TextBox)(ris.FindControl("FromDate"))).Text.Insert(2, "/").Insert(5, "/"));
            }
            else {
                fromDate = ((TextBox)(ris.FindControl("FromDate"))).Text;
            }

            if (!((TextBox)(ris.FindControl("ToDate"))).Text.Contains("/"))
            {
                toDate = (((TextBox)(ris.FindControl("ToDate"))).Text.Insert(2, "/").Insert(5, "/"));
            }
            else { toDate = ((TextBox)(ris.FindControl("ToDate"))).Text; }


            string fromtime = ((TextBox)(ris.FindControl("FromTime"))).Text.PadLeft(4, '0').Insert(2, ":");
            string toTome = ((TextBox)(ris.FindControl("ToTime"))).Text.PadLeft(4, '0').Insert(2, ":");

            dr["BOK_MST_Booking_ID"] = "";
            dr["BOK_DTL_Prod_Booking_ID"] = "";
            dr["SEC_DTL_Carier_Name"] = ((TextBox)(ris.FindControl("txtAirV"))).Text.ToUpper();
            dr["SEC_DTL_From_Destination"] = ((TextBox)(ris.FindControl("txtFrom"))).Text.ToUpper();
            dr["SEC_DTL_From_Date_Time"] = fromDate + " " + fromtime;
            dr["SEC_DTL_To_Destination"] = ((TextBox)(ris.FindControl("txtTo"))).Text.ToUpper();
            dr["SEC_DTL_To_Date_Time"] = toDate + " " + toTome;
            dr["SEC_DTL_Flight_No"] = ((TextBox)(ris.FindControl("txtFLTNO"))).Text;
            dr["SEC_DTL_Class"] = ((TextBox)(ris.FindControl("txtClass"))).Text.ToUpper();
            dr["SEC_DTL_Status"] = ((HtmlInputText)(ris.FindControl("txtStatus"))).Value.ToUpper();
            dr["SEC_DTL_Fare_Basis"] = "";
            dr["SEC_DTL_Not_Valid_Befor"] = "";
            dr["SEC_DTL_Not_Valid_After"] = "";
            dr["SEC_DTL_Baggage_Allownce"] = ((HtmlInputText)ris.FindControl("txtBaggageAllownce")).Value;
            dr["SEC_DTL_Airport_Terminal_From"] = "";
            dr["SEC_DTL_Airport_Terminal_To"] = "";
            dr["SEC_DTL_Seg_ID"] = ++segID;
            dr["SEC_DTL_Seg_Remarks"] = "";
            dr["SEC_DTL_TripID"] = "";
            dr["SEC_DTL_ModifiedBy"] = objUserDetail.userID;
            dr["SEC_DTL_ModifiedDate"] = DateTime.Now;
            var CabinClassValue = (DropDownList)ris.FindControl("ddlCabinClass");
            dr["SEC_DTL_Cabin_Class"] = CabinClassValue.SelectedValue;
            sdt.Rows.Add(dr);
        }
        string b;
        b = objsave.SaveOfflineBookingInDB(sdt, dtPrice, pdt, ddlStatus.SelectedValue, ddlCurrency.SelectedValue, ddlCompany.SelectedValue, ddlCabinClassMaster.SelectedValue, objUserDetail.userID, txtRemarks.Text, lblDestination.InnerText, lblPNR.InnerText, txtPhone.Text, txtMobile.Text, txtEmail.Text.Trim(), txtAddress.Text, txtCity.Text, DdlCountry.SelectedValue);
        if (b !="false")
        {
            lblMsg.Text = "New Booking Created. Booking Ref - " + b;
        }
        else { lblMsg.Text = "Sorry, Unable to create a new booking."; }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {

        if (dtPrice == null)
        {
            dtPrice = DB.CreateAmountChargesDataTable(); ;

        }
        DataRow dr = dtPrice.NewRow();

        dr["BOK_MST_Booking_ID"] = "";
        dr["BOK_DTL_Prod_Booking_ID"] = "";
        dr["AMT_CHG_MST_Charge_ID"] = ddlPayType.Value;
        dr["AMT_CHG_DTL_Charges_For"] = ddlChargeFor.Value;
        dr["AMT_CHG_DTL_Cost_Price"] = txtCostPriceF.Value;
        dr["AMT_CHG_DTL_Sell_Price"] = txtSalePriceF.Value;
        dr["AMT_CHG_DTL_Charges_Status"] = "OK";
        dr["AMT_CHG_DTL_Supplier_ID"] = "";
        dr["AMT_CHG_DTL_Charges_Remarks"] = "";
        dr["AMT_CHG_DTL_Charges_Date"] = DateTime.Now;
        dr["AMT_CHG_DTL_ModifiedBy"] = "";
        dr["AMT_CHG_DTL_ModifiedDate"] = DateTime.Now;
        dtPrice.Rows.Add(dr);
        //if (dtPrice != null) { dtPrice.Merge(acd); }
        //else { dtPrice = acd; }
        rptPrice.DataSource = dtPrice;
        rptPrice.DataBind();

    }

    protected void rptPrice_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            string btn = ((Button)(rptPrice.Items[e.Item.ItemIndex].FindControl("btnDelete"))).ToolTip;
            string[] ff = btn.Split('|');
            DataRow[] result = dtPrice.Select("AMT_CHG_MST_Charge_ID ='" + ff[0] + "'  AND AMT_CHG_DTL_Charges_For = '" + ff[1] + "'");
            foreach (var drow in result)
            {
                drow.Delete();
            }
            dtPrice.AcceptChanges();
            rptPrice.DataSource = dtPrice;
            rptPrice.DataBind();
        }
    }

    private void addPriceBreakup(ref DataTable acd, PassengerPrice adtPP)
    {
        DataRow drFare = acd.NewRow();
        drFare["BOK_MST_Booking_ID"] = "";
        drFare["BOK_DTL_Prod_Booking_ID"] = "";
        drFare["AMT_CHG_MST_Charge_ID"] = "Fare";
        drFare["AMT_CHG_DTL_Charges_For"] = adtPP.PassengerType;
        drFare["AMT_CHG_DTL_Cost_Price"] = adtPP.BasePrice;
        drFare["AMT_CHG_DTL_Sell_Price"] = adtPP.BasePrice;
        drFare["AMT_CHG_DTL_Charges_Status"] = "OK";
        drFare["AMT_CHG_DTL_Supplier_ID"] = "";
        drFare["AMT_CHG_DTL_Charges_Remarks"] = "";
        drFare["AMT_CHG_DTL_Charges_Date"] = DateTime.Now;
        drFare["AMT_CHG_DTL_ModifiedBy"] = "";
        drFare["AMT_CHG_DTL_ModifiedDate"] = DateTime.Now;
        acd.Rows.Add(drFare);

        DataRow drtax = acd.NewRow();
        drtax["BOK_MST_Booking_ID"] = "";
        drtax["BOK_DTL_Prod_Booking_ID"] = "";
        drtax["AMT_CHG_MST_Charge_ID"] = "Tax";
        drtax["AMT_CHG_DTL_Charges_For"] = adtPP.PassengerType;
        drtax["AMT_CHG_DTL_Cost_Price"] = adtPP.Taxes;
        drtax["AMT_CHG_DTL_Sell_Price"] = adtPP.Taxes;
        drtax["AMT_CHG_DTL_Charges_Status"] = "OK";
        drtax["AMT_CHG_DTL_Supplier_ID"] = "";
        drtax["AMT_CHG_DTL_Charges_Remarks"] = "";
        drtax["AMT_CHG_DTL_Charges_Date"] = DateTime.Now;
        drtax["AMT_CHG_DTL_ModifiedBy"] = "";
        drtax["AMT_CHG_DTL_ModifiedDate"] = DateTime.Now;
        acd.Rows.Add(drtax);


    }
}