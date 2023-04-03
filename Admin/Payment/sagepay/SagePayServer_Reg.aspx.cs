using BLL;
using SagePay.IntegrationKit;
using SagePay.IntegrationKit.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Payment_sagepay_SagePayServer_Reg : System.Web.UI.Page
{
    public partial class SagePayServer_Reg : System.Web.UI.Page
    {
        public IServerPaymentResult ServerPaymentResult { get; set; }
        public IServerPayment ServerPaymentRequest { get; set; }
        

        protected void Page_Load(object sender, EventArgs e)
        {
            EL.Payment.Billing bill = (EL.Payment.Billing)Session["payNow"];
            string guid = Request.QueryString.Get("BookingID");
            string Currency = Request.QueryString.Get("BookingID");

            BLL.SearchDetails SearchDetails = BLL.SearchDetails.Current(guid);
            EL.Payment.SagePayment SagePayment = new EL.Payment.SagePayment();

            //default exchange rate
            decimal ExchnageRate = 1;
            //convert requested currency to GBP(base currency)
            if (SearchDetails.Itinerary.Currency != "GBP")
            {
                GetSetDatabase getsetDb = new GetSetDatabase();
                //ExchnageRate = getsetDb.GetExchangeRate("GBP", SearchDetails.Itinerary.Currency);
            }
            SagePayment.PayAmount = Convert.ToDecimal((Convert.ToDecimal(SearchDetails.Itinerary.GrandTotal) / ExchnageRate).ToString("F"));


            SagePayment.NotificationUrl = WebsiteStaticData.WebsiteUrl + "Payment/sagepay/Notification.aspx?uid=" + guid;

            SagePayment.Billing = SearchDetails.Billing;
            SagePayment.Shipping = SearchDetails.Shipping;
            SagePayIntegration integration = new SagePayIntegration();
            ServerPaymentRequest = integration.ServerPaymentRequest();
            SagePayment.SetServerPaymentRequestData(ServerPaymentRequest);

            ServerPaymentResult = integration.GetServerPaymentRequest(ServerPaymentRequest, SagePayment.serverPaymentUrl);
            if (ServerPaymentResult.Status == SagePay.IntegrationKit.ResponseStatus.OK)
            {
                string txtSearchDetails = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(SearchDetails);
                //string Pth = Server.MapPath(@"~\App_Data\SearchDetails\" + SagePayment.VendorTxCode + ".txt");
                string Pth = Server.MapPath(@"~\App_Data\SearchDetails\" + guid + ".txt");
                File.WriteAllText(Pth, txtSearchDetails);

                //for testing 

                //File.WriteAllText(Server.MapPath(@"~\App_Data\SearchDetails\" + SagePayment.VendorTxCode + "-path.txt"), SagePayment.NotificationUrl);
                File.WriteAllText(Server.MapPath(@"~\App_Data\SearchDetails\" + guid + "-path.txt"), SagePayment.NotificationUrl);
                Response.Redirect(ServerPaymentResult.NextUrl, false);
            }
            //else
            //{
            //    try
            //    {
            //        _objSend.Sendcustomermail(BLL.WebsiteStaticData.EmailID2, BLL.WebsiteStaticData.EmailID2, "Online Booking (Transaction Failed)", GetTransactionFailMailBody(), "", "");
            //    }
            //    catch { }

            //    Miscellaneous.SetError("", ServerPaymentResult.StatusDetail);
            //}
        }
        public string GetTransactionFailMailBody()
        {
            return "Trasaction Fail";
        }
    }
}