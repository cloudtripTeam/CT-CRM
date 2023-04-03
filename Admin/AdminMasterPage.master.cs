using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AdminMasterPage : System.Web.UI.MasterPage
{
    public string strMenu { set; get; }
    public string strMenuNew { set; get; }
    public string strUserDertails = null;
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            Binding binding = new BasicHttpBinding();
            EndpointAddress endpointAddress = new EndpointAddress("http://dataservice.cloudtrip.us/FandHServices.svc?wsdl");
            FandHServices.FandHServicesClient client = new FandHServices.FandHServicesClient(binding, endpointAddress);
            
        }
        catch
        { }

        if (Session["UserDetails"] != null)
        {
            /*Caputre start*/
            var Temp_UserFirstName = Session["UserDetails"] as UserDetail;
            strUserDertails = Convert.ToString(Temp_UserFirstName.userFirstName);
            Session["CapturePage"] = HttpContext.Current.Request.Url.AbsolutePath.Replace("/", "").Replace(".", "");

            /*Caputre start*/
            UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
            List<string> Group = (from AdminPermission str in objUserDetail.PermissionDetails
                                  select str.GroupName).Distinct().ToList();


            strMenu = "<ul class='nav navbar-nav' id='ulMenu'>";
            foreach (string strG in Group)
            {
                if (strG != "Step2")
                {
                    strMenu += "<li><a class='dropdown -toggle' data-toggle='dropdown' href='#'>" + strG + "<span class='caret'></span></a><ul class='dropdown-menu'>";
                    //strMenu += "<li><a>" + strG + "</a><ul>";

                    List<AdminPermission> allPage = (from AdminPermission str in objUserDetail.PermissionDetails
                                                     where str.GroupName.Equals(strG, StringComparison.OrdinalIgnoreCase)
                                                     select str).ToList();

                    foreach (AdminPermission aP in allPage)
                    {
                        strMenu += "<li><a href='" + getrootPath(aP.PageUrl) + "'>" + aP.PageName.Replace(".aspx", "") + "</a></li>";
                    }
                    strMenu += "</ul></li>";
                }

            }
            strMenu += "<li><a href='" + ApiCredentionl.WebsiteUrl + "Logout.aspx' style='color:red;border: 1px solid #f00;font-weight: bold;'>Sign Out</a></li></ul>";




            strMenuNew = "<ul class='sidebar-menu'>";
            strMenuNew += "<li class='header'>MAIN NAVIGATION</li>";
            foreach (string strG in Group)
            {
                if (strG != "Step2")
                {
                   
                    strMenuNew += " <li class='treeview'><a href='#'><i class='fa fa-bars'></i> <span>"+ strG + "</span> <i class='fa fa-angle-left pull-right'></i></a>";
                    //strMenu += "<li><a>" + strG + "</a><ul>";

                    List<AdminPermission> allPage = (from AdminPermission str in objUserDetail.PermissionDetails
                                                     where str.GroupName.Equals(strG, StringComparison.OrdinalIgnoreCase)
                                                     select str).ToList();
                    strMenuNew += "<ul class='treeview-menu'>";

                    foreach (AdminPermission aP in allPage)
                    {
                        strMenuNew += "<li class='active'><a href='" + getrootPath(aP.PageUrl) + "'><i class='fa fa-circle-o text-info'></i>" + aP.PageName.Replace(".aspx", "") + "</a></li>";
                        
                    }
                    strMenuNew += "</ul></li>";
                }

            }
            strMenuNew += "<li><a href='" + ApiCredentionl.WebsiteUrl + "Logout.aspx' style='color:#fd0000;border: 5px solid #a9a9a9;font-weight: bold;text-align:center;margin: 20px auto 0 auto;;padding: 5px 0px;width:90%;border-radius:10px'>SIGN OUT</a></li></ul>";

            CurrencyExchange.CurrencyExchange xe = new CurrencyExchange.CurrencyExchange();
            if (Session["ExchangeRate"] == null)
            {
                Session["ExchangeRate"] = xe.GetExchangeRate("GBP", "INR");
            }
            //lblUserID.Text = "Welcome " + objUserDetail.userFirstName + " " + objUserDetail.userLastName + "<br/>" + DateTime.Now.ToString("dd MMM yy HH:mm") + "<br/> <b style='color:green'>XE Rate: ₹ " + xe.GetExchangeRate("GBP", "INR")+"</b>";
           
            nameuser.InnerText = lblUserIdname.Text = nameid.InnerText= objUserDetail.userFirstName + " " + objUserDetail.userLastName;
            memberfrom.InnerText = "Role: "+objUserDetail.userRole;

            if (objUserDetail.userRole.ToLower() != "fareft" && objUserDetail.userRole.ToLower() != "agentft" && objUserDetail.userRole.ToLower() != "team head ft" && objUserDetail.userRole.ToLower() != "no rights")
                checkNotice();
        }
        else
        { Response.Redirect("~/Login.aspx"); }


        //Configuration config = WebConfigurationManager.OpenWebConfiguration("~/Web.Config");
        //SessionStateSection section = (SessionStateSection)config.GetSection("system.web/sessionState");
        //int timeout = (int)section.Timeout.TotalMinutes * 1000 * 60;
        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "SessionAlert", "<script type='text/javascript' >Timer('1')</script>", false);
    }

    private string getrootPath(string path)
    {
        string p = string.Empty;
        if (path.ToLower().Contains("reports/"))
        { p = ApiCredentionl.WebsiteUrl + "admin/" + path; }

        else

        { p = ApiCredentionl.WebsiteUrl + "admin/" + path; }




        return p;

    }

    private void checkNotice()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");



        string xmlFile;
        DataSet dataSet = new DataSet();

        try
        {
            if (File.Exists(Server.MapPath("~/App_Data/ReqXml/Noticeboard.xml")))
            {
                xmlFile = Server.MapPath("~/App_Data/ReqXml/Noticeboard.xml");

                dataSet.ReadXml(xmlFile);
                if (dataSet != null)
                {
                    if (dataSet.Tables.Count > 0)
                    {
                        if (dataSet.Tables[0] != null)
                        {

                            string notice = string.Empty;
                            int count = 0;
                            notice += "<div style='float:left; width: 10000px;;'><ul style='list-style:none'>";
                            foreach (DataRow dr in dataSet.Tables[0].Rows)
                            {

                                if (Convert.ToDateTime(dr["Applicable_Date"]) <= DateTime.Today && Convert.ToDateTime(dr["Expiry_Date"]) >= DateTime.Today)
                                {
                                    count++;
                                    if (Convert.ToDateTime(dr["Applicable_Date"]).AddDays(3) >= DateTime.Today)
                                    {
                                        notice += "<li style='background-color:green; color:white;float:left;margin-right:50px; '>" + count + ". &nbsp;&nbsp;" + dr["NoticeMessage"] + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;---&nbsp;&nbsp;" + dr["NoticeBy"] + "&nbsp;&nbsp;(" + dr["Created_Date"] + ") </li>";
                                    }
                                    else
                                    {
                                        notice += "<li style='display:inline;margin-left:50px'>" + count + ". &nbsp;&nbsp;" + dr["NoticeMessage"] + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;---&nbsp;&nbsp;" + dr["NoticeBy"] + "&nbsp;&nbsp;(" + dr["Created_Date"] + ") </li>";
                                    }
                                }
                            }
                            notice += "</ul></div>";

                            if (!string.IsNullOrEmpty(notice))
                            {
                                ltMarquee.Text = "<marquee behavior='scroll' direction='left' style='background: #4fc3a1; height:35px;  color: #000; border: 1px solid #4fc3a1;'><a href='noticeboard.aspx'>" + notice + "</a></marquee>";
                            }
                        }
                    }
                }


                if (dataSet.Tables.Count > 0)
                {
                    DataRow[] foundRows;
                    DataTable dtclone = dataSet.Tables[0].Clone();
                    dtclone.Columns["Applicable_Date"].DataType = typeof(DateTime);
                    //dtclone.Columns["Expiry_Date"].DataType = typeof(DateTime);
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        dtclone.ImportRow(row);
                    }

                    //foundRows = dtclone.Select("Applicable_Date <= #" + DateTime.Today + "# AND Expiry_Date >= #" + DateTime.Today + "#", "Applicable_Date");
                    foundRows = dtclone.Select("Applicable_Date <= #" + DateTime.Today + "#", "Applicable_Date");
                }
            }
        }
        catch (Exception ex)
        {

        }



    }
}
