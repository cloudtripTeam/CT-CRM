using System;
using SagePay.IntegrationKit.Messages;
using SagePay.IntegrationKit;
using System.IO;
using BLL;


public partial class Admin_Payment_sagepay_Notification : System.Web.UI.Page
{
    private const string OK = "OK";
    private const string INVALID = "INVALID";
   
    protected void Page_Load(object sender, EventArgs e)
    {
        IServerNotificationRequest serverNotificationRequest = new SagePayIntegration().GetServerNotificationRequest();
        try
        {
            //Order order = OrderDataService.handleNotification(serverNotificationRequest);
            EL.Payment.PaymentCallbackDetails PaymentCallbackDetails = new EL.Payment.PaymentCallbackDetails(serverNotificationRequest);
            //string payFor = Request.QueryString["PayFor"].ToString().ToUpper();
            string uid = Request.QueryString["uid"].ToString();
            //string uid = "";
            Response.Clear();
            Response.ContentType = "text/plain";
            // Write that JSON to txt file
            string ordr = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(PaymentCallbackDetails);
            string Pth = Server.MapPath(@"~\App_Data\Payment\" + uid + ".txt");

            File.WriteAllText(Pth, ordr);

            #region Transaction successfull
            if (serverNotificationRequest.Status == SagePay.IntegrationKit.ResponseStatus.OK)
            {
                Response.Write(string.Format("Status={0}\n", serverNotificationRequest.Status == ResponseStatus.ERROR ? INVALID : OK));
                Response.Write(string.Format("RedirectURL={0}Payment/sagepay/CallBack.aspx?Status={1}&VendorTxCode={2}&uid={3}", WebsiteStaticData.WebsiteUrl, serverNotificationRequest.Status, serverNotificationRequest.VendorTxCode, uid));
                //Response.Redirect("~/Payment/CallBack.aspx?Status=" + serverNotificationRequest.Status + "&VendorTxCode=" + serverNotificationRequest.VendorTxCode);
            }
            #endregion

            #region Transaction Failed

            else
            {

                Response.Write(string.Format("Status={0}\n", serverNotificationRequest.Status == ResponseStatus.ERROR ? INVALID : OK));
                Response.Write(string.Format("RedirectURL={0}Payment/sagepay/TransactionFailed.aspx?Status={1}&VendorTxCode={2}&uid={3}", WebsiteStaticData.WebsiteUrl, serverNotificationRequest.Status, serverNotificationRequest.VendorTxCode, uid));



            }
            #endregion
        }
        catch (Exception ex)
        {
            // ErrorSignal.FromCurrentContext().Raise(ex);
            //Debug.WriteLine("ERROR: " + System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
        }
    }
}