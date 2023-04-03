using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.IO;
using EL.Payment;
using Genesis.Net;

public partial class Admin_Payment_emerchant_Notification : System.Web.UI.Page
{
    private const string OK = "OK";
    private const string INVALID = "INVALID";

  
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            string uid = Request["wpf_unique_id"];
            string bookingID = Request.QueryString["BookingID"].ToString().ToUpper();

            string log = "unique id =" + uid + "; bookingId = " + bookingID;
            string Pth = Server.MapPath(@"~\App_Data\Payment\" + bookingID + ".txt");
            File.WriteAllText(Pth, log);

        }
        catch (Exception ex)
        {


            string bookingID = Request.QueryString["BookingID"].ToString().ToUpper();

            string log = "unique id =error; bookingId = " + bookingID;
            string Pth = Server.MapPath(@"~\App_Data\Payment\" + bookingID + ".txt");
            File.WriteAllText(Pth, log);

            //ErrorSignal.FromCurrentContext().Raise(ex);
            //Debug.WriteLine("ERROR: " + System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
        }


        if (HttpContext.Current.Request.HttpMethod == "POST")
        {
            if (String.IsNullOrEmpty(Request.Form.Get("signature")))
            {
                this.renderFailedNotification(Response, System.Net.HttpStatusCode.BadRequest);
                return;
            }

            if (String.IsNullOrEmpty(Request.Form.Get("unique_id")) && String.IsNullOrEmpty(Request.Form.Get("wpf_unique_id")))
            {
                this.renderFailedNotification(Response, System.Net.HttpStatusCode.BadRequest);
                return;
            }

            string requestData = HttpContext.Current.Request.Form.ToString();

            Genesis.Net.Entities.Notification notification = Genesis.Net.Entities.Notification.Parse(requestData);

            if (notification == null)
            {
                this.renderFailedNotification(Response, System.Net.HttpStatusCode.BadRequest);
                return;
            }

            /*
             * Validates the Signature, given in the post request
             */
            if (!notification.IsAuthentic(this.getGatewayConfig()))
            {
                this.renderFailedNotification(Response, System.Net.HttpStatusCode.Unauthorized);
                return;
            }

            if (notification is Genesis.Net.Entities.WpfNotification)
            {
                Genesis.Net.Entities.WpfNotification wpfNotification = (Genesis.Net.Entities.WpfNotification)notification;
                /* You could use these WpfNotification properties to process the notification in your application
                 * and to store them in your database
                 * 
                wpfNotification.PaymentTransactionTerminalToken;
                wpfNotification.PaymentTransactionTransactionType;
                wpfNotification.PaymentTransactionUniqueId;
                wpfNotification.WpfStatus;
                wpfNotification.WpfTransactionId;
                wpfNotification.WpfUniqueId;
                */
            }
            else
            {
                Genesis.Net.Entities.ThreeDNotification threeDNotification = (Genesis.Net.Entities.ThreeDNotification)notification;
                /* You could use this ThreeDNotification properties to process the notification in your application
                 * and to store them in your database
                 *
                threeDNotification.Status;
                threeDNotification.TerminalToken;
                threeDNotification.TransactionId;
                threeDNotification.TransactionType;
                threeDNotification.UniqueId;
                */

            }
            this.renderNotificationResponse(Response, notification);
        }


    }

    protected Genesis.Net.Configuration getGatewayConfig()
    {
        System.Security.Cryptography.X509Certificates.X509Certificate certificate = new System.Security.Cryptography.X509Certificates.X509Certificate(@"E:\Working\genesis_dotnet-master\WebApplication1\Certificates\genesis_sandbox_comodo_ca.pem");
        //Genesis.Net.Configuration config = new Configuration(Environments.Staging, eMerchant.TerminalKey, eMerchant.UserID, eMerchant.Password, certificate, Endpoints.eMerchantPay);
        Genesis.Net.Configuration config = null;
        EL.Payment.eMerchant eMerchant = new EL.Payment.eMerchant();

        if (eMerchant.PayConnectingServer == "LIVE")
        {
            config = new Configuration(Environments.Production, eMerchant.TerminalKey, eMerchant.UserID, eMerchant.Password, certificate, Endpoints.eMerchantPay);
        }
        else if (eMerchant.PayConnectingServer == "TEST")
        {
            config = new Configuration(Environments.Staging, eMerchant.TerminalKey, eMerchant.UserID, eMerchant.Password, certificate, Endpoints.eMerchantPay);
        }

        return config;
    }

    protected void renderNotificationResponse(HttpResponse response, Genesis.Net.Entities.Notification notification)
    {
        string successfulResponseBody = "<?xml version=\"1.0\" encoding=\"utf-8\"?><notification_echo><{0}>{1}</{0}></notification_echo>";

        if (notification is Genesis.Net.Entities.WpfNotification)
        {
            successfulResponseBody = String.Format(successfulResponseBody, "wpf_unique_id", ((Genesis.Net.Entities.WpfNotification)notification).WpfUniqueId);
        }
        else
        {
            successfulResponseBody = String.Format(successfulResponseBody, "unique_id", ((Genesis.Net.Entities.ThreeDNotification)notification).UniqueId);
        }

        response.Clear();
        response.ContentType = "text/xml";
        response.Write(successfulResponseBody);
        response.StatusCode = (int)System.Net.HttpStatusCode.OK;
        response.End();
    }

    protected void renderFailedNotification(HttpResponse response, System.Net.HttpStatusCode httpStatusCode)
    {
        response.Clear();
        response.StatusCode = (int)httpStatusCode;
        response.Write(httpStatusCode.ToString());
        response.End();
    }
}