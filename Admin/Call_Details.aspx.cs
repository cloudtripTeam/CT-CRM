using Newtonsoft.Json;
using System;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web.Services;
using System.Web.UI.WebControls;
using MoreLinq;

public partial class Admin_Call_Details : System.Web.UI.Page
{
    GetSetDatabase objDl = new GetSetDatabase();
    static Binding binding = new BasicHttpBinding();
    static EndpointAddress endpointAddress = new EndpointAddress("http://dataservice.cloudtrip.us/FandHServices.svc?wsdl");

    //Create Client of the Service        
    FandHServices.FandHServicesClient client = new FandHServices.FandHServicesClient(binding, endpointAddress);
    public int UniqueCalls
    {
        get;
        set;
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        //if (Session["UserDetails"] != null)
        //{
        //    UserDetail objUserDetail = Session["UserDetails"] as UserDetail;

        //    ddlBrand.Items.Clear();
        //    if (objUserDetail.userRole.ToLower() == "agentft")
        //    {
        //        ddlBrand.Items.Add(new ListItem("FLTTROTT", "Flight Trotters"));

        //    }
        //    else {
        //        ddlBrand.Items.Add(new ListItem("DIAL4TRV", "Dial4travel"));
        //        ddlBrand.Items.Add(new ListItem("TRVJUNCTION", "Travel Junction"));
        //        ddlBrand.Items.Add(new ListItem("FLTXPT", "Flight XpertUk"));
        //        ddlBrand.Items.Add(new ListItem("OTHER", "Other"));
        //    }
        //}
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

                //if (objUserDetail.userRole.ToLower() == "agentft" || objUserDetail.userRole.ToLower() == "team head ft")
                //{
                //    ddlBrand.Items.Add(new ListItem( "Flight Trotters","FLTTROTT"));
                //}
                //else
                //{
                //    ddlBrand.Items.Add(new ListItem("Dial4travel","DIAL4TRV"));
                //    ddlBrand.Items.Add(new ListItem("Travel Junction", "TRVJUNCTION"));
                //    ddlBrand.Items.Add(new ListItem("Flight XpertUk","FLTXPT"));
                //    ddlBrand.Items.Add(new ListItem("Other", "OTHER"));
                //}
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }
    protected void btnInsert_Click(object sender, EventArgs e)
    {

        bool result = false;
        if (btnInsert.Text.Equals("Insert"))
        {
            string callref = client.GenerateIDs("CLREF");
            result = objDl.SET_Call_Details(callref, "INSERT", hfUpdatedBy.Value, ddlSource.Value, ddlBrand.SelectedValue, txtContact.Value, txtPaxName.Value, txtEmail.Value, txtOrigin.Value, txtDestination.Value, txtOBDate.Value, txtIBDate.Value, txtAirline.Value, ddlReason.Value, ddlStatus.Value, txtRemarks.Value, txtAdults.Value, txtChilds.Value, Infants.Value,Convert.ToString(bookingID.Value), ddlSourceMedia.SelectedValue);
        }
        if (btnInsert.Text.Equals("Update"))
        {
            result = objDl.SET_Call_Details(hfID.Value, "UPDATE", hfUpdatedBy.Value, ddlSource.Value, ddlBrand.SelectedValue, txtContact.Value, txtPaxName.Value, txtEmail.Value, txtOrigin.Value, txtDestination.Value, txtOBDate.Value, txtIBDate.Value, txtAirline.Value, ddlReason.Value, ddlStatus.Value, txtRemarks.Value, txtAdults.Value, txtChilds.Value, Infants.Value,Convert.ToString(bookingID.Value), ddlSourceMedia.SelectedValue);
        }
        if (result != true)
        {
            btnInsert.Text = "Insert";
        }
        bindDetails();
        clearControls();
    }
    private void bindDetails()
    {
        UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
        string role = objUserDetail.userRole.ToLower();
        if (role != "superadmin" && role != "admin" && role != "team head" && role != "team head ft" && role != "marketing head" && role != "team head ca")
        {
            DataTable dt = objDl.GET_Call_Details("", txtContact.Value.Trim(), txtEmail.Value.Trim(), txtPaxName.Value.Trim(), txtOrigin.Value.Trim(), txtDestination.Value.Trim(), txtAgent.Value, txtCreationDate.Value, txtCreationdateTo.Value, CommanBinding.GetCompanyCodes(ddlBrand), ddlType.Value, ddlStatus.Value.ToLower() == "select status" ? "" : ddlStatus.Value);
            if (dt != null)
            {
                rptrDetails.DataSource = dt;
                ViewState["calls"] = dt;
                btnExport.Visible = true;
            }
            else
            {
                btnExport.Visible = false;
            }
        }
        else if (role == "team head ft")
        {
            DataTable dt = objDl.GET_Call_Details("", txtContact.Value.Trim(), txtEmail.Value.Trim(), txtPaxName.Value.Trim(), txtOrigin.Value.Trim(), txtDestination.Value.Trim(), txtAgent.Value, txtCreationDate.Value, txtCreationdateTo.Value, CommanBinding.GetCompanyCodes(ddlBrand), ddlType.Value, ddlStatus.Value.ToLower() == "select status" ? "" : ddlStatus.Value);
            if (dt != null)
            {
                var calls = dt.AsEnumerable().DistinctBy(x => x.Field<string>("Contact_Number"));
                UniqueCalls = calls.Count();
                rptrDetails.DataSource = dt;
                ViewState["calls"] = dt;
                btnExport.Visible = true;
            }
            else
            {
                btnExport.Visible = false;
            }
        }
        else
        {
            DataTable dt = objDl.GET_Call_Details("", txtContact.Value.Trim(), txtEmail.Value.Trim(), txtPaxName.Value.Trim(), txtOrigin.Value.Trim(), txtDestination.Value.Trim(), txtAgent.Value, txtCreationDate.Value, txtCreationdateTo.Value, CommanBinding.GetCompanyCodes(ddlBrand), ddlType.Value, ddlStatus.Value.ToLower() == "select status" ? "" : ddlStatus.Value);
            if (dt != null)
            {
                var calls = dt.AsEnumerable().DistinctBy(x => x.Field<string>("Contact_Number"));
                this.UniqueCalls = calls.Count();
                rptrDetails.DataSource = dt;
                ViewState["calls"] = dt;
                btnExport.Visible = true;
            }
            else
            {
                btnExport.Visible = false;
            }
        }
        rptrDetails.DataBind();
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

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        bindDetails();
    }
     
    [WebMethod(EnableSession = true)]
    public static string GetCallSummary(string CallRef, string Brand)
    {
        GetSetDatabase objDl1 = new GetSetDatabase();
        return JsonConvert.SerializeObject(objDl1.GET_Call_Details(CallRef, "", "", "", "", "", "", "", "", Brand, ""), Formatting.Indented);
    }
     
    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)ViewState["calls"];
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

    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSourceMedia.Items.Clear();
        ddlSourceMedia.Items.Insert(0, new ListItem("Select Campaign", ""));
        if (ddlBrand.SelectedIndex != 0)
        {
            CommanBinding.BindCampaignDetails(ref ddlSourceMedia, ddlBrand.SelectedValue);
        }
    }


    //private void ToggleElements(RepeaterItem item, bool isEdit)
    //{
    //    //Toggle Buttons.
    //    item.FindControl("lnkEdit").Visible = !isEdit;
    //    item.FindControl("lnkUpdate").Visible = isEdit;
    //    item.FindControl("lnkCancel").Visible = isEdit;
    //}

    //protected void gvCalldetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    string valName = ((HiddenField)gvCalldetails.Rows[e.RowIndex].FindControl("hdltbName")).Value.ToString();
    //    ddlSource.Value = ((HiddenField)gvCalldetails.Rows[e.RowIndex].FindControl("hdltrSource")).Value.ToString();
    //    ddlBrand.Value = ((HiddenField)gvCalldetails.Rows[e.RowIndex].FindControl("hdltrBrand")).Value.ToString();
    //    txtContact.Value = ((Literal)gvCalldetails.Rows[e.RowIndex].FindControl("ltrCont")).Text.ToString();
    //    txtPaxName.Value = ((Literal)gvCalldetails.Rows[e.RowIndex].FindControl("ltrPaxName")).Text.ToString();
    //    txtEmail.Value = ((HiddenField)gvCalldetails.Rows[e.RowIndex].FindControl("hdltrEmail")).Value.ToString();
    //    txtOrigin.Value = ((HiddenField)gvCalldetails.Rows[e.RowIndex].FindControl("hdltrOrigin")).Value.ToString();
    //    txtDestination.Value = ((Literal)gvCalldetails.Rows[e.RowIndex].FindControl("ltrDestination")).Text.ToString();
    //    txtOBDate.Value = ((HiddenField)gvCalldetails.Rows[e.RowIndex].FindControl("hdltrOB")).Value.ToString();
    //    txtIBDate.Value = ((HiddenField)gvCalldetails.Rows[e.RowIndex].FindControl("hdltrIB")).Value.ToString();
    //    txtNop.Value = ((Literal)gvCalldetails.Rows[e.RowIndex].FindControl("ltrNoP")).Text.ToString();
    //    txtAirline.Value = ((Literal)gvCalldetails.Rows[e.RowIndex].FindControl("ltrAirline")).Text.ToString();
    //    ddlReason.Value = ((HiddenField)gvCalldetails.Rows[e.RowIndex].FindControl("hdltrReason")).Value.ToString();
    //    ddlSource.Value = ((Literal)gvCalldetails.Rows[e.RowIndex].FindControl("ltrStatus")).Text.ToString();
    //    txtRemarks.Value = ((Literal)gvCalldetails.Rows[e.RowIndex].FindControl("ltrRemarks")).Text.ToString();
    //    hfID.Value = ((HiddenField)gvCalldetails.Rows[e.RowIndex].FindControl("hdID")).Value.ToString();
    //    btnInsert.Text = "Update";
    //}
}