using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



public partial class logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Abandon();

        // mobileapi.Airfaresearchrq request = new Airfaresearchrq();
        // mobileapi.Authentication auth = new Authentication();
        // auth.CompanyId = "";
        // auth.CredentialId = "";
        // auth.CredentialPassword = "";
        // auth.CredentialType = "LIVE";
        // request.Authentication = auth;
        // mobileapi.Cabin cb = new mobileapi.Cabin();
        // cb.Class = "Y";      
        // request.Cabin = cb;
        // request.Direct = "false";
        // request.Flexi = "false";
        // request.JourneyType = "R";
        //// mobileapi.Segments segs = new Segments();
        // mobileapi.Segment[] segs = new mobileapi.Segment[2];
        // segs[0] = new mobileapi.Segment();
        // segs[1] = new mobileapi.Segment();
        // segs[0].Origin = "LON";
        // segs[0].Destination = "BKK";
        // segs[0].Date = "15/11/2017";

        // segs[1].Origin = "BKK";
        // segs[1].Destination = "LON";
        // segs[1].Date = "25/11/2017";
        // request.Segments = new Segments();
        // request.Segments.Segment = segs;
        // request.PaxDetail = new Paxdetail();
        // request.PaxDetail.NoAdult = "1";
        // request.PaxDetail.NoChild = "0";
        // request.PaxDetail.NoInfant = "0";
        // Airlines air = new Airlines();
        // air.Airline = "ALL";
        // request.Airlines = air;

        // string txtSearchDetails = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(request);


        Response.Redirect("~/login.aspx");
    }
}