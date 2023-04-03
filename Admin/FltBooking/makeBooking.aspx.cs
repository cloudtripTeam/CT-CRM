using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
using System.Reflection;
using System.Web.UI.HtmlControls;
using System.Threading;
using System.Globalization;

public partial class Admin_FltBooking_makeBooking : System.Web.UI.Page
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

                if (!objUserDetail.isAuth("Make Reservation"))
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




    private string Get_PNRRetrieval_SOAP_Request(
     string recloc,
     string companyID,
     string provider,
     string destination)
    {
        return "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\"><soap:Body><RetrieveRecordLocator xmlns=\"http://tempuri.org/\"><RecordLocatorRQ> <![CDATA[" + this.PNRRetrieveXmlRQ(recloc, companyID, provider, destination) + "]]></RecordLocatorRQ></RetrieveRecordLocator></soap:Body></soap:Envelope>";
    }
    private string PNRRetrieveXmlRQ(
     string recloc,
     string companyID,
     string provider,
     string destination)
    {
        string empty = string.Empty;
        return "<PNRRQ><Authentication><CompanyId>" + companyID + "</CompanyId> </Authentication><Provider>" + provider + "</Provider><PNR>" + recloc + "</PNR><Destination>" + destination + "</Destination></PNRRQ>";
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        var rt=Get_PNRRetrieval_SOAP_Request(txtPnr.Text.Trim().ToUpper(), ddlCompany.SelectedValue, "1P", txtDestination.Text.Trim().ToUpper());
        FlightFareService _flightFareService = new FlightFareService();
        var ert= _flightFareService.CallFlightService(this.Get_PNRRetrieval_SOAP_Request(txtPnr.Text.Trim().ToUpper(), ddlCompany.SelectedValue, "1P", txtDestination.Text.Trim().ToUpper()), "RetrieveRecordLocator");


        BLL.FlightsBL fb = new BLL.FlightsBL();

        EL.Flight.Itineraries itin = fb.RecLocRetrieval(txtPnr.Text.Trim().ToUpper(), ddlCompany.SelectedValue, "1P", txtDestination.Text.Trim().ToUpper());

        if (itin.Items.Count > 0)
        {
            if (!string.IsNullOrEmpty(itin.Items[0].Warnings))
            {
                pnlBooking.Visible = false;
                lblMsg.Text = itin.Items[0].Warnings;
            }
            else
            {
                lblDestination.InnerText = txtDestination.Text;
                lblPNR.InnerText = txtPnr.Text;
                pnlBooking.Visible = true;
                rptrSect.DataSource = itin.Items[0].Sectors;
                rptrSect.DataBind();
                rptrPax.DataSource = itin.Items[0].PassengersDetails;
                rptrPax.DataBind();
                rptPrice.DataSource = getPassenegrsPrice(itin);
                rptPrice.DataBind();
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

    }

    private DataTable getPassenegrsPrice(EL.Flight.Itineraries itin)
    {
        
        DataTable acd = DB.CreateAmountChargesDataTable();

        List<PassengerPrice> pricebreakDown = new List<PassengerPrice>();

        if (itin.Items[0].AdultInfo != null)
        {
            if (itin.Items[0].AdultInfo.NoAdult > 0)
            {
                PassengerPrice adtPP = new PassengerPrice();
                adtPP.PassengerType = "ADT";
                adtPP.NoOfPax = itin.Items[0].AdultInfo.NoAdult;
                adtPP.BasePrice = itin.Items[0].AdultInfo.AdtBFare;
                adtPP.Taxes = itin.Items[0].AdultInfo.AdTax;
                adtPP.MarkUp = itin.Items[0].AdultInfo.MarkUp;
                adtPP.Commission = itin.Items[0].AdultInfo.Commission;
                pricebreakDown.Add(adtPP);

                addPriceBreakup(ref acd, adtPP);

            }

        }
        if (itin.Items[0].ChildInfo != null)
        {
            if (itin.Items[0].ChildInfo.NoChild > 0)
            {
                PassengerPrice chdPP = new PassengerPrice();
                chdPP.PassengerType = "CNN";
                chdPP.NoOfPax = itin.Items[0].ChildInfo.NoChild;
                chdPP.BasePrice = itin.Items[0].ChildInfo.ChdBFare;
                chdPP.Taxes = itin.Items[0].ChildInfo.CHTax;
                chdPP.MarkUp = itin.Items[0].ChildInfo.MarkUp;
                chdPP.Commission = itin.Items[0].ChildInfo.Commission;
                pricebreakDown.Add(chdPP);
                addPriceBreakup(ref acd, chdPP);
            }

        }
        if (itin.Items[0].InfantInfo != null)
        {
            if (itin.Items[0].InfantInfo.NoInfant > 0)
            {
                PassengerPrice infPP = new PassengerPrice();
                infPP.PassengerType = "INF";
                infPP.NoOfPax = itin.Items[0].InfantInfo.NoInfant;
                infPP.BasePrice = itin.Items[0].InfantInfo.InfBFare;
                infPP.Taxes = itin.Items[0].InfantInfo.InTax;
                infPP.MarkUp = itin.Items[0].InfantInfo.MarkUp;
                infPP.Commission = itin.Items[0].InfantInfo.Commission;
                pricebreakDown.Add(infPP);
                addPriceBreakup(ref acd, infPP);
            }
        }
        dtPrice = acd;
        return dtPrice;
    }

    private DataTable SectorDt()
    {
        DataTable dt = new DataTable();
        return dt;
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
       
        if (dtPrice == null)
        {
            dtPrice = DB.CreateAmountChargesDataTable();;

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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
        GetSetDatabase objsave = new GetSetDatabase();
        DataTable pdt = DB.CreatePaxDataTable();
        int paxid = 0;

        if (dtPrice== null || dtPrice.Rows.Count == 0)
        {
            lblMsg.Text = "Sorry Unable to create booking, Please Add pricing to create new booking";
            return;
        }
        foreach(RepeaterItem rt in rptrPax.Items)
        {

            DataRow dr=pdt.NewRow();
            dr["BOK_MST_Booking_ID"] = "";
            dr["BOK_DTL_Prod_Booking_ID"] = "";
            dr["PAX_DTL_Pax_ID"] = ++paxid;
            dr["PAX_DTL_Title"] = ((HtmlInputText)(rt.FindControl("txtTitle"))).Value;
            dr["PAX_DTL_Pax_First_Name"] = ((HtmlInputText)(rt.FindControl("txtFirstName"))).Value;
            dr["PAX_DTL_Pax_Middle_Name"] = "";
            dr["PAX_DTL_Pax_Last_Name"] = ((HtmlInputText)(rt.FindControl("txtLastName"))).Value;
            dr["PAX_DTL_Frequent_Flyer_No"] = "";
            dr["PAX_DTL_Passport_No"] = "";
            dr["PAX_DTL_Nationality"] = "";
            dr["PAX_DTL_Expiry_Date"] = Convert.ToDateTime("01-01-1900");
            dr["PAX_DTL_Place_of_Issue"] = "";
            dr["PAX_DTL_Place_of_Birth"] = "";
            //dr["PAX_DTL_Pax_DOB"] = Convert.ToDateTime(((HtmlInputText)(rt.FindControl("txtDOB"))).Value).ToString("yyyy/MM/dd");
            try {
               HtmlInputText DOB=  (HtmlInputText)rt.FindControl("txtDOB");
               if ( !string.IsNullOrEmpty(DOB.Value.Trim()))
                   dr["PAX_DTL_Pax_DOB"] = Convert.ToDateTime(DOB.Value.Trim()).ToString("dd/MM/yyyy"); ;
            
            }
            catch { dr["PAX_DTL_Pax_DOB"] = Convert.ToDateTime("01-01-1900"); }
             
            dr["PAX_DTL_Pax_Type"] = ((HtmlInputText)(rt.FindControl("txtPaxType"))).Value;
            dr["PAX_DTL_Pax_Sex"] ="";
            dr["PAX_DTL_ModifiedBy"] = objUserDetail.userID;
            dr["PAX_DTL_ModifiedDate"] = DateTime.Now;
            pdt.Rows.Add(dr);
            
        }

        DataTable sdt = DB.CreateSectorDataTable();

        int segID = 0;
        foreach(RepeaterItem ris in rptrSect.Items)
        {
            DataRow dr = sdt.NewRow();

            dr["BOK_MST_Booking_ID"] = "";
            dr["BOK_DTL_Prod_Booking_ID"] = "";
            dr["SEC_DTL_Carier_Name"] = ((TextBox)(ris.FindControl("txtAirV"))).Text;
            dr["SEC_DTL_From_Destination"] = ((TextBox)(ris.FindControl("txtFrom"))).Text;
            dr["SEC_DTL_From_Date_Time"] = ((TextBox)(ris.FindControl("FromDate"))).Text + " " + ((TextBox)(ris.FindControl("FromTime"))).Text;
            dr["SEC_DTL_To_Destination"] = ((TextBox)(ris.FindControl("txtTo"))).Text;
            dr["SEC_DTL_To_Date_Time"] = ((TextBox)(ris.FindControl("ToDate"))).Text + " " + ((TextBox)(ris.FindControl("ToTime"))).Text; ;
            dr["SEC_DTL_Flight_No"] = ((TextBox)(ris.FindControl("txtFLTNO"))).Text;
            dr["SEC_DTL_Class"] = ((TextBox)(ris.FindControl("txtClass"))).Text;
            dr["SEC_DTL_Status"] = ((HtmlInputText)(ris.FindControl("txtStatus"))).Value;
            dr["SEC_DTL_Fare_Basis"] = "";
            dr["SEC_DTL_Not_Valid_Befor"] = "";
            dr["SEC_DTL_Not_Valid_After"] = "";
            dr["SEC_DTL_Baggage_Allownce"] = "";
            dr["SEC_DTL_Airport_Terminal_From"] = "";
            dr["SEC_DTL_Airport_Terminal_To"] = "";
            dr["SEC_DTL_Seg_ID"] = ++segID;
            dr["SEC_DTL_Seg_Remarks"] = "";
            dr["SEC_DTL_TripID"] = "";
            dr["SEC_DTL_ModifiedBy"] = objUserDetail.userID;
            dr["SEC_DTL_ModifiedDate"] = DateTime.Now;
            sdt.Rows.Add(dr);
        }
        string b;
        b = objsave.SaveOfflineBookingInDB(sdt, dtPrice, pdt, ddlStatus.SelectedValue, ddlCurrency.SelectedValue, ddlCompany.SelectedValue, "", objUserDetail.userID, txtRemarks.Text, lblDestination.InnerText, lblPNR.InnerText, txtPhone.Text, txtMobile.Text, txtEmail.Text, txtAddress.Text, txtCity.Text, DdlCountry.SelectedValue);
        if (b != "false")
        {
           
            lblMsg.Text = "New Booking Created. Booking Ref - " + b;
        }
        else { lblMsg.Text = "Sorry, Unable to create a new booking."; }
    }
    protected void rptPrice_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if(e.CommandName=="del")
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
//public class ListtoDataTableConverter
//{
//    public DataTable ToDataTable<T>(List<T> items)
//    {
//        DataTable dataTable = new DataTable(typeof(T).Name);
//        //Get all the properties
//        PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
//        foreach (PropertyInfo prop in Props)
//        {
//            //Setting column names as Property names
//            dataTable.Columns.Add(prop.Name);
//        }
//        foreach (T item in items)
//        {
//            var values = new object[Props.Length];
//            for (int i = 0; i < Props.Length; i++)
//            {
//                //inserting property values to datatable rows
//                values[i] = Props[i].GetValue(item, null);
//            }
//            dataTable.Rows.Add(values);
//        }
//        //put a breakpoint here and check datatable
//        return dataTable;
//    }
//}