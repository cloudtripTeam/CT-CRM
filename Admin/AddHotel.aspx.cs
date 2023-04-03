using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_AddHotel : System.Web.UI.Page
{
    BLL.SaveInDB DB = new BLL.SaveInDB();
    UserDetail objUserDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserDetails"] != null)
        {
            objUserDetail = Session["UserDetails"] as UserDetail;
            if (!IsPostBack)
            {
                if (!objUserDetail.isAuth("AddHotel"))
                {
                    Response.Redirect("Default.aspx");
                    return;
                }

                if (Request.QueryString["BID"] != null && Request.QueryString["PID"] != null)
                {
                    hfBookingID.Value = Request.QueryString["BID"].ToString();
                    hfProdID.Value = Request.QueryString["PID"].ToString();                   
                    hfUpdatedBy.Value = objUserDetail.userID;                    
                   
                    bookingDetails(hfBookingID.Value, hfProdID.Value);

                    if (ViewState["Pricing"] == null)
                    {
                        DataTable dt = DB.CreateAmountChargesDataTable();
                        ViewState["Pricing"] = dt;
                    }


                }

            }
            
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }


    private void bookingDetails(string BookingID, string ProdID)
    {

        Itinerary.FlightDetails iti = new Itinerary.FlightDetails(BookingID, ProdID, true, true, true, true, true, true, true, true,true);


        #region bind Passengers
        rptrPax.DataSource = iti.PD;
        rptrPax.DataBind();
        #endregion

        #region bind Sectors
       
        rptrSect.DataSource = iti.SD;
        rptrSect.DataBind();
        #endregion

        #region binding other details

        lblBookingID.Text = iti.BM.BookingID;       
        lblBookkingDate.Text = Convert.ToDateTime(iti.BD.BookingDateTime).ToString("dd MMM yyyy");
        hfOldStatus.Value = iti.BD.BookingStatus;
      
        hfOldCompany.Value = iti.BM.BookingByCompany;
        hfAdults.Value =  Convert.ToString(iti.PD.FindAll(x => x.PaxType == "ADT" || x.PaxType == "ITX").Count());
        hfChilds.Value = Convert.ToString(iti.PD.FindAll(x => x.PaxType == "CHD" || x.PaxType == "CNN" || x.PaxType == "INN").Count());
        hfInfants.Value = Convert.ToString(iti.PD.FindAll(x => x.PaxType == "INF" || x.PaxType == "INF").Count());

        #endregion




    }

    

    protected void rptPrice_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

    }

    protected void btnSaveHotel_Click(object sender, EventArgs e)
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
        DataTable dtPrice = (DataTable)ViewState["Pricing"];
        if (dtPrice == null || dtPrice.Rows.Count == 0)
        {
            lblmsg.Text = "Sorry Unable to create booking, Please Add pricing to create new booking";
            return;
        }

        GetSetDatabase gsd = new GetSetDatabase();
      string prodid = gsd.getMaxProductID(hfBookingID.Value);
        try
        {
            gsd.SET_Booking_Detail(hfBookingID.Value, prodid, "", objUserDetail.userID, "INTR", DateTime.Now.ToString(), "", "", "", txtRemarks.Text, "", txtHTLPNR.Text, "", "HTL", "", "", txtHTLSupplier.Text, "", "Insert", "");
            //gsd.SET_Passenger_Detail(hfBookingID.Value, prodid,

            gsd.SaveHotel("INSERT", hfBookingID.Value, prodid, txtHTLName.Text, txtHTLPNR.Text,
            Convert.ToInt32(hfAdults.Value), Convert.ToInt32(hfChilds.Value),
            Convert.ToInt32(hfInfants.Value), Convert.ToDateTime(txtHTLCheckIn.Text).ToString("yyyy/MM/dd"),
            Convert.ToDateTime(txtHTLCheckOut.Text).ToString("yyyy/MM/dd"), txtHTLDest.Text, ddlHTLMealType.SelectedValue,
            txtHTLRoomType.Text, txtHTLSupplier.Text, "REF", txtHTLAdd1.Text, txtHTLAdd2.Text, txtHTLPostal.Text, txtHTLCity.Text,
            txtHTLCountry.Text, txtHTLTelephone.Text, txtHTLEmailID.Text, Convert.ToInt32(ddlHTLNoRooms.SelectedValue));

            lblmsg.Text = "hotel added in existing booking successfully.";
        }
        catch (Exception)
        {

            lblmsg.Text = "Sorry to add a hotel in existing booking.";
        }
        
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataTable dtPrice = (DataTable)ViewState["Pricing"];
        if (dtPrice == null)
        {
            dtPrice = DB.CreateAmountChargesDataTable(); ;

        }
        DataRow dr = dtPrice.NewRow();

        dr["BOK_MST_Booking_ID"] = hfBookingID.Value;
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
        rptPrice.DataSource = dtPrice;
        rptPrice.DataBind();

    }

    protected void rptPrice_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            DataTable dtPrice = (DataTable)ViewState["Pricing"];
            if (dtPrice != null)
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
    }
}