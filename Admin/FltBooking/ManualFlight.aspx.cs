using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_FltBooking_ManualFlight : System.Web.UI.Page
{
    UserDetail objUserDetail;
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
                   // CommanBinding.BindCompanyDetails(ref ddlCompany, objUserDetail.userID);
                }


            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        List<EL.Flight.Itinerary> Itineraries = new List<EL.Flight.Itinerary>();

        Itineraries.Add(new EL.Flight.Itinerary());
        int sectors = Convert.ToInt16(ddlSectors.SelectedValue);
        for (int i = 0; i < sectors; i++)
        {
            EL.Flight.Itinerary_Sector s = new EL.Flight.Itinerary_Sector();
            Itineraries[0].Sectors.Add(s);
            rptrSect.DataSource = Itineraries[0].Sectors;
            rptrSect.DataBind();
            btnAdd.Visible = true;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        GetSetDatabase GSD = new GetSetDatabase();
        int s = 0;

        int i =GSD.SET_FlightManuals(txtPFrom.Text, txtPTo.Text, Convert.ToDateTime(txtDepDate.Text).ToString("yyyy-MM-dd"), Convert.ToDateTime(txtRetDate.Text).ToString("yyyy-MM-dd"), Convert.ToInt32(ddlJourney.SelectedValue), Convert.ToInt32(txtAdt.Value), Convert.ToInt32(txtChd.Value),
            Convert.ToInt32(txtInfants.Value), Convert.ToDouble(txtBase.Value), Convert.ToDouble(txtTax.Value), Convert.ToDouble(txtMarkup.Text), ddlCurrency.SelectedValue, txtAirline.Value
            , txtFType.Value);

        if (i > 0)
        {


            foreach (RepeaterItem item in rptrSect.Items)
            {
                TextBox txtDFrom = (TextBox)item.FindControl("txtFrom");
                TextBox txtDTo = (TextBox)item.FindControl("txtTo");
                TextBox txtAirV = (TextBox)item.FindControl("txtAirV");
                TextBox txtFLTNO = (TextBox)item.FindControl("txtFLTNO");
                TextBox txtClass = (TextBox)item.FindControl("txtClass");
                TextBox FromDate = (TextBox)item.FindControl("FromDate");
                TextBox FromTime = (TextBox)item.FindControl("FromTime");
                TextBox ToDate = (TextBox)item.FindControl("ToDate");
                TextBox ToTime = (TextBox)item.FindControl("ToTime");
                TextBox txtCabin = (TextBox)item.FindControl("txtCabin");
                TextBox txtEquipType = (TextBox)item.FindControl("txtEquipType");
                TextBox txtTechStop = (TextBox)item.FindControl("txtTechStop");
                DropDownList ddlIsReturn = (DropDownList)item.FindControl("ddlIsReturn");
                TextBox txtoptcr = (TextBox)item.FindControl("txtoptcr");
                TextBox txtmarkcr = (TextBox)item.FindControl("txtmarkcr");
                TextBox txtbinfo = (TextBox)item.FindControl("txtbinfo");
                TextBox txtNoSeats = (TextBox)item.FindControl("txtNoSeats");
                string DepTIME = FromDate.Text + " " + FromTime.Text;
                string ArrTIME = ToDate.Text + " " + ToTime.Text;

                FlightDetails(i, txtAirV.Text, txtClass.Text, txtCabin.Text, txtNoSeats.Text==""?0: Convert.ToInt32(txtNoSeats.Text), txtFLTNO.Text, txtDFrom.Text,
                    txtDTo.Text, DepTIME, ArrTIME, txtEquipType.Text, txtTechStop.Text == "" ? 0 : Convert.ToInt32(txtTechStop.Text), Convert.ToInt32(ddlIsReturn.SelectedValue), txtoptcr.Text, txtmarkcr.Text, txtbinfo.Text, "Admin");

                s++;
            }
                if (s == rptrSect.Items.Count)
                {
                    ltrMsg.Text = "Record Inserted.";
                    //rptrSect.DataSource = null;
                    //rptrSect.DataSourceID = null;
                    //rptrSect.DataBind();
                }
                else
                    ltrMsg.Text = "Not Inserted ...?";
            
        }
    }

    public int FlightDetails(int ID,string AirV,string AClass,string CabinClass,int NoSeats,string FltNum,string DepAirport,string ArrAirport,string DepDate,string ArrDate,string EquipType,int TechStopOver
        ,int IsReturn,string OptrCarrier,string MrktCarrier,string BaggageInfo,string ModifiedBy)
    {
        try
        {
            SqlConnection connection = DataConnection.GetConnectionFareManual();
            string query = "INSERT INTO [dbo].[SectorDetail](ID,AirV,[Class],CabinClass,NoSeats,FltNum,DepAirport,ArrAirport,DepDate,ArrDate,EquipType,TechStopOver,IsReturn,OptrCarrier,MrktCarrier," +
                " BaggageInfo, ModifiedBy, ModifiedDate)VALUES(@ID,@AirV,@Class,@CabinClass,@NoSeats,@FltNum,@DepAirport,@ArrAirport,@DepDate,@ArrDate,@EquipType,@TechStopOver,@IsReturn," +
                "@OptrCarrier,@MrktCarrier, @BaggageInfo, @ModifiedBy, @ModifiedDate) ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", ID);
            command.Parameters.AddWithValue("@AirV", AirV);
            command.Parameters.AddWithValue("@Class", AClass);
            command.Parameters.AddWithValue("@CabinClass", CabinClass);
            command.Parameters.AddWithValue("@NoSeats", NoSeats);
            command.Parameters.AddWithValue("@FltNum", FltNum);
            command.Parameters.AddWithValue("@DepAirport", DepAirport);
            command.Parameters.AddWithValue("@ArrAirport", ArrAirport);
            command.Parameters.AddWithValue("@DepDate", DepDate);
            command.Parameters.AddWithValue("@ArrDate", ArrDate);
            command.Parameters.AddWithValue("@EquipType", EquipType);
            command.Parameters.AddWithValue("@TechStopOver", TechStopOver);
            command.Parameters.AddWithValue("@IsReturn", IsReturn);
            command.Parameters.AddWithValue("@OptrCarrier", OptrCarrier);
            command.Parameters.AddWithValue("@MrktCarrier", MrktCarrier);
            command.Parameters.AddWithValue("@BaggageInfo", BaggageInfo);
            command.Parameters.AddWithValue("@ModifiedBy", ModifiedBy);
            command.Parameters.AddWithValue("@ModifiedDate", System.DateTime.Now);
            connection.Open();
            int i = command.ExecuteNonQuery();
            return i;
        }
        catch(Exception ex)
        {
            ex.Message.ToString();
            return 0;
        }
    }

    public int MainFlight(string from, string to, string fromDt, string toDt,int jtype, int Adt, int Chi, int Inf, double basef, double taxf, double markup, string Curr, string Carrier, string FareType)
    {
        try
        {

            //SqlConnection connection = DataConnection.GetConnectionFareManual();
            //string query = "INSERT INTO PriceDetails ([From],[To],Fdate,Tdate,Jtype,Adt,Chd,Inf,BaseFare,Tax,Markup,Currency,ValCarrier,FareType)" +
            //    "VALUES(@From,@To,@Fdate,@Tdate,@Jtype,@Adt,@Chd,@Inf,@BaseFare,@Tax,@Markup,@Currency,@ValCarrier,@FareType)";

            //SqlCommand command = new SqlCommand(query, connection);
            //command.Parameters.AddWithValue("@From", from);
            //command.Parameters.AddWithValue("@To", to);
            //command.Parameters.AddWithValue("@Fdate", fromDt);
            //command.Parameters.AddWithValue("@Tdate", toDt);
            //command.Parameters.AddWithValue("@Jtype", jtype);
            //command.Parameters.AddWithValue("@Adt", Adt);
            //command.Parameters.AddWithValue("@Chd", Chi);
            //command.Parameters.AddWithValue("@Inf", Inf);
            //command.Parameters.AddWithValue("@BaseFare", basef);
            //command.Parameters.AddWithValue("@Tax", taxf);
            //command.Parameters.AddWithValue("@Markup", markup);
            //command.Parameters.AddWithValue("@Currency", Curr);
            //command.Parameters.AddWithValue("@ValCarrier", Carrier);
            //command.Parameters.AddWithValue("@FareType", FareType);
            //connection.Open();
            //int i=command.ExecuteNonQuery();



            return 0;
        }
        catch(Exception ex)
        {
            ex.Message.ToString();
            return 0;
        }
    }

    protected void btnFind_Click(object sender, EventArgs e)
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        DataTable dt = null; ;
        
        dt = objGetSetDatabase.GET_Flight_Manuals(txtPFrom.Text.Trim(), txtPTo.Text.Trim(), txtAirline.Value.Trim());
        if (dt.Rows.Count > 0)
        {
           
            rptViewDetails.DataSource = dt;
            rptViewDetails.DataBind();
        }
    }
}