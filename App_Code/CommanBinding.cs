using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

/// <summary>
/// Summary description for CommanBinding
/// </summary>
public class CommanBinding 
{
	public CommanBinding()
	{
		//
		// TODO: Add constructor logic here
		//

	}
    public static void BindCompanyDetails(ref DropDownList ddlCompany,string userID)
    { 
        GetSetDatabase objGetSet=new GetSetDatabase();
      
        DataTable dt = objGetSet.GET_User_Company_Access(userID);
        ddlCompany.Items.Clear();
        if (dt != null)
        {
            ddlCompany.DataSource= dt;
            ddlCompany.DataValueField = "CompanyID";
            ddlCompany.DataTextField = "CompanyName";
            ddlCompany.DataBind();
        }
        if (dt.Rows.Count >= 1)
        {
            ddlCompany.Items.Insert(0, new ListItem("Select Company", ""));
        }
    }
    public static void BindCompanyDetails(ref ListBox ddlCompany, string userID)
    {
        GetSetDatabase objGetSet = new GetSetDatabase();

        DataTable dt = objGetSet.GET_User_Company_Access(userID);
        ddlCompany.Items.Clear();
        if (dt != null)
        {
            ddlCompany.DataSource = dt;
            ddlCompany.DataValueField = "CompanyID";
            ddlCompany.DataTextField = "CompanyName";
            ddlCompany.DataBind();
        }
        //if (dt.Rows.Count > 1)
        //    ddlCompany.Items.Insert(0, new ListItem("Select Company", ""));
    }
    public static void BindCampaignDetails(ref DropDownList ddlCampaign,string CompanyID)
    {
        GetSetDatabase objGetSet = new GetSetDatabase();
        DataTable dt = objGetSet.GET_Campaign_Master("", CompanyID );
        ddlCampaign.Items.Clear();
        if (dt != null)
        {
            ddlCampaign.DataSource = dt;
            ddlCampaign.DataValueField = "CampID";
            ddlCampaign.DataTextField = "CampName";
            ddlCampaign.DataBind();
        }
        ddlCampaign.Items.Insert(0, new ListItem("Select Campaign", ""));
        ddlCampaign.Items.Add(new ListItem("SMS", "SMS"));
        ddlCampaign.Items.Add( new ListItem("Newsletter", "Newsletter"));
        ddlCampaign.Items.Add(new ListItem("Unknown", "Unknown"));
    }

    public static void BindPreferedCampaign(ref DropDownList ddlCampaign)
    {
        GetSetDatabase objGetSet = new GetSetDatabase();
        DataTable dt = objGetSet.GET_Pref_Campaign_Master();
        ddlCampaign.Items.Clear();
        if (dt != null)
        {
            ddlCampaign.DataSource = dt;
            ddlCampaign.DataValueField = "CampID";
            ddlCampaign.DataTextField = "CampName";
            ddlCampaign.DataBind();
        }
        ddlCampaign.Items.Insert(0, new ListItem("Select Campaign", ""));
    }

    public static string GetCompanyCodes(DropDownList ddlCompany)
    {
        string value = ddlCompany.SelectedValue.ToLower();
        if (ddlCompany.SelectedValue == "" || ddlCompany.SelectedValue == "select" || ddlCompany.SelectedValue == "any" || ddlCompany.SelectedValue == "all")
        {
            value = string.Empty;
            foreach(ListItem li in ddlCompany.Items)
            {
                if (li.Value != "" && li.Value != "select" && li.Value != "any" && li.Value != "all")
                value += li.Value + ",";

            }
            if (value != "")
            {
                value = value.Remove(value.LastIndexOf(","));
            }
            return value;
        }
        else
        {
            value = ddlCompany.SelectedValue;
            return value;
        }
    }
    public static string GetCompanyCodes(ListBox ddlCompany)
    {
        string value = ddlCompany.SelectedValue.ToLower();

        value = string.Empty;
        foreach (ListItem li in ddlCompany.Items)
        {
            if (li.Selected == true)
            {
                if (li.Value != "" && li.Value != "select" && li.Value != "any" && li.Value != "all")
                    value += li.Value + ",";
            }

        }
        if (value == "")
        {

            //if no company selected system will treat as all company
            foreach (ListItem li in ddlCompany.Items)
            {

                if (li.Value != "" && li.Value != "select" && li.Value != "any" && li.Value != "all")
                    value += li.Value + ",";


            }
        }
        if (value != "")
        {

            value = value.Remove(value.LastIndexOf(","));
        }
        return value;

    }



    public static string ConvertDataTableToJSON(DataTable table)
    {

      return  JsonConvert.SerializeObject(table);


        //JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
        //List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
        //Dictionary<string, object> childRow;
        //foreach (DataRow row in table.Rows)
        //{
        //    childRow = new Dictionary<string, object>();
        //    foreach (DataColumn col in table.Columns)
        //    {
        //        childRow.Add(col.ColumnName, row[col]);
        //    }
        //    parentRow.Add(childRow);
        //}
        //return jsSerializer.Serialize(parentRow);

    }

    public static void BindSupplierDetails(ref DropDownList ddlSupplier, string supplier)
    {
        FandHServices.FandHServicesClient objlos = new FandHServices.FandHServicesClient();
        DataTable dt = new DataTable();
        dt = objlos.GET_Supplier_Master("SELECT", supplier,"","");
        ddlSupplier.Items.Clear(); 
       
        if (dt != null)
        {
            ddlSupplier.DataSource = dt;
            ddlSupplier.DataValueField = "SUP_Code";
            ddlSupplier.DataTextField = "SUP_Name";
            ddlSupplier.DataBind();
        }
        if (dt.Rows.Count > 1)
            ddlSupplier.Items.Insert(0, new ListItem("Select Supplier", ""));
    }

    public static void BindBookingStatus(ref DropDownList ddlStatus, string role)
    {
        GetSetDatabase objGetSet = new GetSetDatabase();

        DataTable dt = objGetSet.GET_User_Company_Access(role);
        ddlStatus.Items.Clear();
        if (dt != null)
        {
            ddlStatus.DataSource = dt;
            ddlStatus.DataValueField = "StatusID";
            ddlStatus.DataTextField = "StatusName";
            ddlStatus.DataBind();
        }
        if (dt.Rows.Count > 1)
            ddlStatus.Items.Insert(0, new ListItem("Select Status", ""));
    }

    public static void BindSourceMedia(ref DropDownList ddlsourceMedia)
    {
        GetSetDatabase objGetSet = new GetSetDatabase();

        DataTable dt = objGetSet.GET_SourceMedia();
        ddlsourceMedia.Items.Clear();
        if (dt != null)
        {
            ddlsourceMedia.DataSource = dt;
            ddlsourceMedia.DataValueField = "Camp_ID";
            ddlsourceMedia.DataTextField = "Camp_Name";
            ddlsourceMedia.DataBind();
        }
        if (dt.Rows.Count > 1)
        {
            ddlsourceMedia.Items.Insert(0, new ListItem("Select Media", ""));
            ddlsourceMedia.Items.Insert(1, new ListItem("Chat online", "Chat online"));
            ddlsourceMedia.Items.Insert(1, new ListItem("Chat offline", "Chat offline"));
            ddlsourceMedia.Items.Insert(2, new ListItem("Phone online", "Phone online"));
            ddlsourceMedia.Items.Insert(2, new ListItem("Phone offline", "Phone offline"));
            ddlsourceMedia.Items.Insert(3, new ListItem("Email", "Email"));
            ddlsourceMedia.Items.Insert(4, new ListItem("Newsletters", "Newsletters"));
            ddlsourceMedia.Items.Insert(5, new ListItem("Other", "Other"));
        }
    }
    public static DataTable BindCampaignDetailsForDT(string UserId, string Counter)
    {
        GetSetDatabase objGetSet = new GetSetDatabase();
        DataTable dt = objGetSet.GET_User_CampaignUser_Access_For_FT(UserId, Counter);
        if (dt != null)
        {
            return dt;
        }
        else
        {
            return null;
        }
    }

}