using BLL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Genesis.Net;


public partial class Admin_Payment_emerchant_Success : System.Web.UI.Page
{
    //FlightServicesLive.FlightServices objFBS = new FlightServicesLive.FlightServices();
    Hashtable htbl = new Hashtable();
    //SendMail _objSend = new SendMail();
    Miscellaneous misce = new Miscellaneous();
    SaveInDB objSaveInDB = new SaveInDB();
    string AtolAmt = string.Empty;
    bool MailSent = false;


    protected void Page_Init(object sender, EventArgs e)
    {
        Page.Header.Title = "Payment Details";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'> window.history.forward(); function noBack() { window.history.forward(); }</script>", false);
        Response.Expires = 1;
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);// Now(-1);
        Response.AddHeader("pragma", "no-cache");
        Response.AddHeader("cache-control", "private");
        Response.CacheControl = "no-cache";


    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {

               
                string strStatus = Request.QueryString["Status"];
                string guid = Request.QueryString["uid"].ToString();

                try
                {


                    string log = "Status =" + strStatus + "; guid = " + guid;
                    string Pth = Server.MapPath(@"~\App_Data\Payment\" + guid + ".txt");
                    File.WriteAllText(Pth, log);

                }
                catch (Exception ex)
                {
                    string log = "Status =" + strStatus + "; guid = " + guid;
                    string Pth = Server.MapPath(@"~\App_Data\Payment\" + guid + ".txt");
                    File.WriteAllText(Pth, log);

                    
                }


               
                if (objSaveInDB.setTrnsactionInDB(guid))
                {
                   
                    //SecureQueryString sq = new SecureQueryString();
                    //sq["isPrev"] = "YES";
                    //sq["MailSent"] = MailSent.ToString();
                    //sq["uid"] = guid;
                    //sq.ExpireTime = DateTime.Now.AddMinutes(60);
                    //Response.Redirect("~/Confirmation.aspx?q=" + sq.ToString(), true);
                }
                
            }
            catch (Exception ex)
            {

                string Pth = Server.MapPath(@"~\App_Data\Payment\" + Request.QueryString["uid"].ToString() + "-callback.txt");
                File.WriteAllText(Pth, ex.StackTrace + ex.Source);
            }
        }
        //}
    }
}