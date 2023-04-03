using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreLinq;

public partial class Admin_Reports_CallReports_II : System.Web.UI.Page
{
    GetSetDatabase objDl = new GetSetDatabase();
    //Create object of the Binding
    static Binding binding = new BasicHttpBinding();
    //Create endpointAddress of the Service
    static EndpointAddress endpointAddress = new
    EndpointAddress("http://dataservice.cloudtrip.us/FandHServices.svc?wsdl");

    //Create Client of the Service        
    FandHServices.FandHServicesClient client = new FandHServices.FandHServicesClient(binding, endpointAddress);
    public int UniqueCalls
    {
        get;
        set;
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        


    }

    protected void Page_Load(object sender, EventArgs e)
    {


        if (Session["UserDetails"] != null)
        {
            if (!IsPostBack)
            {
                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
                if (!objUserDetail.isAuth("Lead"))
                {
                    Response.Redirect("~/Admin/AccessDenied.aspx");
                    return;
                }

                
                hfUpdatedBy.Value = objUserDetail.userID;
                txtCreationDate.Value = DateTime.Today.ToString("dd-MM-yyyy");

                ddlBrand.Items.Clear();
              
                CommanBinding.BindCompanyDetails(ref ddlBrand, objUserDetail.userID);
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }
   
    private void bindDetails()
    {
        UserDetail objUserDetail = Session["UserDetails"] as UserDetail;

        string role = objUserDetail.userRole.ToLower();
        if (role != "superadmin" && role != "admin" && role != "team head" && role != "team head ft" && role != "marketing head")
        {

            DataTable dt = objDl.GET_Call_Details("", txtContact.Value.Trim(), txtEmail.Value.Trim(), txtPaxName.Value.Trim(), txtOrigin.Value.Trim(), txtDestination.Value.Trim(), txtAgent.Value, txtCreationDate.Value, txtCreationdateTo.Value, CommanBinding.GetCompanyCodes(ddlBrand), ddlType.Value, ddlStatus.Value.ToLower() == "any" ? "" : ddlStatus.Value);
            if (dt != null)
            {
                rptrDetails.DataSource = dt;
                ViewState["calls_II"] = dt;
                btnExport.Visible = true;
            }
            else { btnExport.Visible = false; }
        }
        else if (role == "team head ft")
        {
            DataTable dt = objDl.GET_Call_Details("", txtContact.Value.Trim(), txtEmail.Value.Trim(), txtPaxName.Value.Trim(), txtOrigin.Value.Trim(), txtDestination.Value.Trim(), txtAgent.Value, txtCreationDate.Value, txtCreationdateTo.Value, CommanBinding.GetCompanyCodes(ddlBrand), ddlType.Value, ddlStatus.Value.ToLower() == "any" ? "" : ddlStatus.Value);
            if (dt != null)
            {
                var calls = dt.AsEnumerable().DistinctBy(x => x.Field<string>("Contact_Number"));
                this.UniqueCalls = calls.Count();
                rptrDetails.DataSource = dt;
                ViewState["calls_II"] = dt;
                btnExport.Visible = true;
            }
            else { btnExport.Visible = false; }
        }
        else
        {
            DataTable dt = objDl.GET_Call_Details("", txtContact.Value.Trim(), txtEmail.Value.Trim(), txtPaxName.Value.Trim(), txtOrigin.Value.Trim(), txtDestination.Value.Trim(), txtAgent.Value, txtCreationDate.Value, txtCreationdateTo.Value, CommanBinding.GetCompanyCodes(ddlBrand), ddlType.Value, ddlStatus.Value.ToLower() == "any" ? "" : ddlStatus.Value);
            if (dt != null)
            {
                var calls = dt.AsEnumerable().DistinctBy(x => x.Field<string>("Contact_Number"));
                this.UniqueCalls = calls.Count();
                rptrDetails.DataSource = dt;
                ViewState["calls_II"] = dt;
                btnExport.Visible = true;
            }
            else { btnExport.Visible = false; }
        }
        rptrDetails.DataBind();
        //gvCalldetails.DataSource = objDl.GET_Call_Details("", "", "", "", "", "");
        //gvCalldetails.DataBind();
    }

    private void clearControls()
    {

        txtAirline.Value = string.Empty;
        txtContact.Value = string.Empty;
        txtDestination.Value = string.Empty;
        txtEmail.Value = string.Empty;
        txtIBDate.Value = string.Empty;
        txtOBDate.Value = string.Empty;
        txtOrigin.Value = string.Empty;

        txtPaxName.Value = string.Empty;
        txtRemarks.Value = string.Empty;


    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindDetails();
    }

   



    [WebMethod(EnableSession = true)]
    public static string GetCallSummary(string CallRef, string Brand)
    {
        GetSetDatabase objDl1 = new GetSetDatabase();
        return JsonConvert.SerializeObject(objDl1.GET_Call_Details(CallRef, "", "", "", "", "", "", "", "", Brand, ""), Formatting.Indented);


        //JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
        //return javaScriptSerializer.Serialize(objDl1.GET_Call_Details(CallRef, "", "", "", "", ""));
    }

    

   


    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)ViewState["calls_II"];
        if (dt != null)
        {


            string attachment = "attachment; filename=" + "CallDetails_" + DateTime.Now.ToString("ddMMMyyyy") + ".xls";
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