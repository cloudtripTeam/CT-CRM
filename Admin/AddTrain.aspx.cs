using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AddTrain : System.Web.UI.Page
{
    UserDetail objUserDetail;
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
                    hfUpdatedBy.Value = objUserDetail.userID;

                    bookingDetails(hfBookingID.Value, hfProdID.Value);




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
        hfAdults.Value = Convert.ToString(iti.PD.FindAll(x => x.PaxType == "ADT" || x.PaxType == "ITX").Count());
        hfChilds.Value = Convert.ToString(iti.PD.FindAll(x => x.PaxType == "CHD" || x.PaxType == "CNN" || x.PaxType == "INN").Count());
        hfInfants.Value = Convert.ToString(iti.PD.FindAll(x => x.PaxType == "INF" || x.PaxType == "INF").Count());

        #endregion




    }

    protected void rptPrice_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }

    protected void rptPrice_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

    }

    protected void btnSaveTrain_Click(object sender, EventArgs e)
    {
        GetSetDatabase gsd = new GetSetDatabase();
        string prodid = gsd.getMaxProductID(hfBookingID.Value);
        try
        {
            gsd.SET_Booking_Detail(hfBookingID.Value, prodid, "", objUserDetail.userID, "INTR", DateTime.Now.ToString(), "", "", "", txtRemarks.Text, "", txtHTLPNR.Text, "", "HTL", "", "", txtHTLSupplier.Text, "", "Insert", "");
           

            lblmsg.Text = "Train added in existing booking successfully.";
        }
        catch (Exception)
        {

            lblmsg.Text = "Sorry to add a Train in existing booking.";
        }

    }




    protected void btnAdd_Click(object sender, EventArgs e)
    {

    }
}