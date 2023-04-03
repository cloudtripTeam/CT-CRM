using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_FltBooking_OwnFlight : System.Web.UI.Page
{
    public static DataTable dtPrice { set; get; }
    UserDetail objUserDetail;
    BLL.SaveInDB DB = new SaveInDB();
    GetSetDatabase GSD = new GetSetDatabase();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserDetails"] != null)
        {
            if (!IsPostBack)
            {
                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;


                if (!objUserDetail.isAuth("Own Flight"))
                {
                    Response.Redirect("~/Admin/AccessDenied.aspx");
                    return;
                }
                else
                {
                    CommanBinding.BindCompanyDetails(ref ddlCompany, objUserDetail.userID);
                }


            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }
    private DataTable GetDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[11] { new DataColumn("MainFlight_GUID", typeof(string)),
                        new DataColumn("Airline", typeof(string)),
                        new DataColumn("DepartureDate", typeof(DateTime)),
                        new DataColumn("DepartureTime", typeof(decimal)),
                        new DataColumn("ArrivalDate", typeof(DateTime)),
                        new DataColumn("ArrivalTime", typeof(decimal)),
                        new DataColumn("FlightNo", typeof(string)),
                        new DataColumn("From", typeof(string)),
                        new DataColumn("To", typeof(string)),
                        new DataColumn("Trip", typeof(string)),
                        new DataColumn("FLight_UID",typeof(string)) });
        dt.TableName = "FlightIO";
        return dt;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (dtPrice == null)
        {
            dtPrice = GetDataTable();
            Random rnd = new Random();
            ViewState["FlightGUID"] = rnd.Next(0000000, 9999999);
        }
        //  DataTable dtITI = GetDataTable();

        DataRow dr = dtPrice.NewRow();
        dr["MainFlight_GUID"] = "MF_" + ViewState["FlightGUID"].ToString();
        dr["Airline"] = txtAirV.Text;
        dr["DepartureDate"] = Convert.ToDateTime(txtFromDate.Text).ToString("yyyy/MM/dd");
        dr["DepartureTime"] = txtFromTime.Text;
        dr["ArrivalDate"] = Convert.ToDateTime(txtToDate.Text).ToString("yyyy/MM/dd");
        dr["ArrivalTime"] = txtToTime.Text;
        dr["FlightNo"] = txtFLTNO.Text;
        dr["From"] = txtFrom.Text;
        dr["To"] = txtTo.Text;
        dr["Trip"] = ddlTrip.Value;
        dr["Flight_UID"] = "IF_" + ViewState["FlightGUID"].ToString();
        dtPrice.Rows.Add(dr);
        //ViewState["CurrentTable"] = dtPrice;
        rptrSect.DataSource = dtPrice;
        rptrSect.DataBind();

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (dtPrice.Rows.Count > 1)
        {
            
            bool i = GSD.SET_Flight_Itinerary_Details("MF_" + ViewState["FlightGUID"].ToString(), Convert.ToDouble(txtPrice.Text), "C", txtSource.Text, txtDestination.Text, txtAirline.Text,
                 Convert.ToDateTime(txtVFDate.Text), Convert.ToDateTime(txtVTDate.Text), "admin", ddlCompany.SelectedValue, 0, dtPrice);
            if (i == true)
                ltrMessage.Text = "Data Insert Successfully.";
            dtPrice = null; ;
        }
    }

    [WebMethod(EnableSession = true)]
    public static string GetFlightItineraryDetails(string MainFlightID)
    {
        GetSetDatabase objFD=new GetSetDatabase();
        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
        return javaScriptSerializer.Serialize(objFD.GET_Flight_Itinerary_Details (MainFlightID));
    } 

    protected void btnISearch_Click(object sender, EventArgs e)
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        DataTable dt = null; ;
        UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
        dt = GSD.GET_Flight_Itinerary(txtSSource.Text.Trim(), txtSDestination.Text.Trim(), txtSOperator.Text.Trim());
        if (dt.Rows.Count > 0)
        {
            pnlFlightDetails.Visible = true;
            rptFlightIDetails.DataSource = dt;
            rptFlightIDetails.DataBind();
        }
    }
}