using System;
using System.Collections;
using System.Web.UI;
using System.IO;
using BLL;


public partial class Admin_Payment_sagepay_CallBack : System.Web.UI.Page
{
    
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
               
                string strStatus = Request.QueryString["Status"];
                string VendorTxCode = Request.QueryString["VendorTxCode"];
                string guid = Request.QueryString["uid"].ToString();

              
                //_SearchDetails.TransctionAmount = _SearchDetails.Itinerary.GrandTotal + Convert.ToDouble(PaymentCallbackDetails.Surcharge);



                if (strStatus.ToUpper() == "OK")
                {
                    if (objSaveInDB.setTrnsactionInDB(guid))
                    {
                       
                        SecureQueryString sq = new SecureQueryString();
                        sq["isPrev"] = "YES";
                        sq["MailSent"] = MailSent.ToString();
                        sq["uid"] = guid;
                        sq.ExpireTime = DateTime.Now.AddMinutes(60);
                        Response.Redirect("~/Confirmation.aspx?q=" + sq.ToString(), true);
                    }
                }
                else
                {
                    if (objSaveInDB.setTrnsactionInDB(guid))
                    {
                        SecureQueryString sq = new SecureQueryString();
                        sq["isPrev"] = "YES";
                        sq["MailSent"] = MailSent.ToString();
                        sq.ExpireTime = DateTime.Now.AddMinutes(60);
                        Response.Redirect("~/Payment/TransactionFailed.aspx?q=" + sq.ToString(), true);
                    }
                }
            }
            //}
        }
    
}