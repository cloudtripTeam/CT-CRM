using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Genesis.Net;
using System.IO;


public partial class Admin_Payment_emerchant_eMerchantServer_Reg : System.Web.UI.Page
{ 
       
        protected void Page_Load(object sender, EventArgs e)
        {
             string guid = Request.QueryString.Get("fID");

             BLL.SearchDetails SearchDetails = BLL.SearchDetails.Current(guid);
             EL.Payment.eMerchant eMerchant = new EL.Payment.eMerchant();


            System.Security.Cryptography.X509Certificates.X509Certificate certificate = new System.Security.Cryptography.X509Certificates.X509Certificate(@"E:\Working\genesis_dotnet-master\WebApplication1\Certificates\genesis_sandbox_comodo_ca.pem");
            //Genesis.Net.Configuration config = new Configuration(Environments.Staging, eMerchant.TerminalKey, eMerchant.UserID, eMerchant.Password, certificate, Endpoints.eMerchantPay);
            Genesis.Net.Configuration config = null;
           
            if (eMerchant.PayConnectingServer == "LIVE")
            {
                config = new Configuration(Environments.Production, eMerchant.TerminalKey, eMerchant.UserID, eMerchant.Password, certificate, Endpoints.eMerchantPay);
            }
            else if (eMerchant.PayConnectingServer == "TEST")
            {
                config = new Configuration(Environments.Staging, eMerchant.TerminalKey, eMerchant.UserID, eMerchant.Password, certificate, Endpoints.eMerchantPay);
            }

            IGenesisClient Igc = Genesis.Net.GenesisClientFactory.Create(config);
            
            Genesis.Net.Common.Composite[] transactionType = new Genesis.Net.Common.Composite[1];
            transactionType[0] = new Genesis.Net.Common.Composite();
            transactionType[0].Add("name", eMerchant.TransactionType);

            Genesis.Net.Entities.Address address = new Genesis.Net.Entities.Address();
            address.Address1 = SearchDetails.Billing.Address1;
            address.Address2 = SearchDetails.Billing.Address2;
            address.City = SearchDetails.Billing.City;
            address.Country = Genesis.Net.Common.Iso3166CountryCodes.GB;
            address.FirstName = SearchDetails.Billing.FirstNames;
            address.LastName = SearchDetails.Billing.Surname;


            Genesis.Net.Entities.Requests.Initial.WpfCreate wpfCreate = new Genesis.Net.Entities.Requests.Initial.WpfCreate();

            wpfCreate.Amount = Convert.ToDecimal(SearchDetails.Itinerary.GrandTotal) + Convert.ToDecimal(SearchDetails.PaymentCallbackDetails.Surcharge);
            SearchDetails.TransctionAmount = SearchDetails.Itinerary.GrandTotal +  Convert.ToDouble(SearchDetails.PaymentCallbackDetails.Surcharge);
            wpfCreate.BillingAddress = address;
            wpfCreate.ShippingAddress = wpfCreate.BillingAddress;

            wpfCreate.Currency = Genesis.Net.Common.Iso4217CurrencyCodes.GBP;
            wpfCreate.CustomerEmail = SearchDetails.Billing.Email;
            wpfCreate.CustomerPhone = SearchDetails.Billing.Phone;
            wpfCreate.TransactionId = Guid.NewGuid().ToString();
            wpfCreate.NotificationUrl = WebsiteStaticData.WebsiteUrl + "Payment/eMerchant/Notification.aspx?PayFor=ONL&BookingID=" + SearchDetails.BookingID + "&TRNID=" + SearchDetails.trnID + "&Provider=" + SearchDetails.Provider + "&ProdID=" + SearchDetails.SearchProd[0].ProdID + "&AtolAmt=0&uid=" + guid;
            wpfCreate.ReturnCancelUrl = WebsiteStaticData.WebsiteUrl + "Payment/eMerchant/Cancel.aspx?PayFor=ONL&BookingID=" + SearchDetails.BookingID + "&TRNID=" + SearchDetails.trnID + "&Provider=" + SearchDetails.Provider + "&ProdID=" + SearchDetails.SearchProd[0].ProdID + "&AtolAmt=0&uid=" + guid;
            wpfCreate.ReturnFailureUrl = WebsiteStaticData.WebsiteUrl + "Payment/eMerchant/Failure.aspx?PayFor=ONL&BookingID=" + SearchDetails.BookingID + "&TRNID=" + SearchDetails.trnID + "&Provider=" + SearchDetails.Provider + "&ProdID=" + SearchDetails.SearchProd[0].ProdID + "&AtolAmt=0&uid=" + guid;
            wpfCreate.ReturnSuccessUrl = WebsiteStaticData.WebsiteUrl + "Payment/eMerchant/Success.aspx?PayFor=ONL&BookingID=" + SearchDetails.BookingID + "&TRNID=" + SearchDetails.trnID + "&Provider=" + SearchDetails.Provider + "&ProdID=" + SearchDetails.SearchProd[0].ProdID + "&AtolAmt=0&uid=" + guid;
            wpfCreate.TransactionTypes = transactionType;
            wpfCreate.Usage = "Travel Junction UK";
            wpfCreate.Description = "Air Ticket - " + SearchDetails.flightFareSearchRQ.Segments[0].Origin.AirportCityCode + " to " + SearchDetails.flightFareSearchRQ.Segments[0].Destination.AirportCityCode;

            Result<Genesis.Net.Entities.Responses.Successful.WpfCreateSuccessResponse, Genesis.Net.Entities.Responses.Error.WpfCreateErrorResponse> response = Igc.Execute(wpfCreate);

            if (response.IsSuccessful)
            {               

                
                SearchDetails.PaymentCallbackDetails.VendorTxCode = response.SuccessResponse.UniqueId;
                SearchDetails.PaymentCallbackDetails.Status = response.SuccessResponse.Status;


                string txtSearchDetails = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(SearchDetails);
                string Pth = Server.MapPath(@"~\App_Data\SearchDetails\" + guid + ".txt");
                File.WriteAllText(Pth, txtSearchDetails);
                Response.Redirect(response.SuccessResponse.RedirectUrl, false);
            }
            else
            {
                
            }
        }
        public string GetTransactionFailMailBody()
        {
            return "Trasaction Fail";
        }
   
}