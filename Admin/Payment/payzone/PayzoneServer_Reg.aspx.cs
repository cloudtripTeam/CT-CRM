using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Payment_payzone_PayzoneServer_Reg : System.Web.UI.Page
{
    protected string m_szFormAction;
    protected string m_szHashDigest;
    protected string m_szMerchantID;
    protected string m_szAmount;
    protected string m_szCurrencyCode;
    protected string m_szOrderID;
    protected string m_szTransactionType;
    protected string m_szTransactionDateTime;
    protected string m_szCallbackURL;
    protected string m_szOrderDescription;
    protected string m_szCustomerName;
    protected string m_szAddress1;
    protected string m_szAddress2;
    protected string m_szAddress3;
    protected string m_szAddress4;
    protected string m_szCity;
    protected string m_szState;
    protected string m_szPostCode;
    protected string m_szCountryCode;
    protected string m_szCV2Mandatory;
    protected string m_szAddress1Mandatory;
    protected string m_szCityMandatory;
    protected string m_szPostCodeMandatory;
    protected string m_szStateMandatory;
    protected string m_szCountryMandatory;
    protected string m_szResultDeliveryMethod;
    protected string m_szServerResultURL;
    protected string m_szPaymentFormDisplaysResult;
    protected string m_szServerResultURLCookieVariables;
    protected string m_szServerResultURLFormVariables;
    protected string m_szServerResultURLQueryStringVariables;

    protected void Page_Load(object sender, EventArgs e)
    {

        EL.Payment.Billing bill = (EL.Payment.Billing)Session["payNow"];
       string BookingID = Request.QueryString.Get("BookingID");

        string szStringToHash;

        PayzoneSetting ps = new PayzoneSetting();
        m_szMerchantID = PayzoneSetting.MerchantID;
        m_szResultDeliveryMethod = PaymentFormHelper.GetResultDeliveryMethod(PayzoneSetting.ResultDeliveryMethod);

        m_szFormAction = "https://mms." + PayzoneSetting.PaymentProcessorDomain + "/Pages/PublicPages/PaymentForm.aspx";

        // the amount in *minor* currency (i.e. £10.00 passed as "1000")
        m_szAmount = Convert.ToString(1000);
        // the currency	- ISO 4217 3-digit numeric (e.g. GBP = 826)
        m_szCurrencyCode = Convert.ToString(826);
        // order ID
        m_szOrderID = BookingID;
        // the transaction type - can be SALE or PREAUTH
        m_szTransactionType = "SALE";
        // the GMT/UTC relative date/time for the transaction (MUST either be in GMT/UTC 
        // or MUST include the correct timezone offset)
        m_szTransactionDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss zzz");
        // order description
        m_szOrderDescription = BookingID;
        // these variables allow the payment form to be "seeded" with initial values
        m_szCustomerName = bill.FirstNames + " " + bill.Surname;
        m_szAddress1 = bill.Address1;
        m_szAddress2 = bill.Address2;
        m_szAddress3 = "";
        m_szAddress4 = "";
        m_szCity = bill.City;
        m_szState = bill.Region;
        m_szPostCode = bill.PostCode;
        // the country code - ISO 3166-1  3-digit numeric (e.g. UK = 826)
        m_szCountryCode = Convert.ToString(826);
        // use these to control which fields on the hosted payment form are
        // mandatory
        m_szCV2Mandatory = Convert.ToString(true);
        m_szAddress1Mandatory = Convert.ToString(false);
        m_szCityMandatory = Convert.ToString(false);
        m_szPostCodeMandatory = Convert.ToString(false);
        m_szStateMandatory = Convert.ToString(false);
        m_szCountryMandatory = Convert.ToString(false);
        // the URL on this system that the payment form will push the results to (only applicable for 
        // ResultDeliveryMethod = "SERVER")
        if (PayzoneSetting.ResultDeliveryMethod != RESULT_DELIVERY_METHOD.SERVER)
        {
            m_szServerResultURL = "";
        }
        else
        {
            m_szServerResultURL = PaymentFormHelper.GetSiteSecureBaseURL(Request) + "ReceiveTransactionResult.aspx";
        }
        // set this to true if you want the hosted payment form to display the transaction result
        // to the customer (only applicable for ResultDeliveryMethod = "SERVER")
        if (PayzoneSetting.ResultDeliveryMethod != RESULT_DELIVERY_METHOD.SERVER)
        {
            m_szPaymentFormDisplaysResult = "";
        }
        else
        {
            m_szPaymentFormDisplaysResult = Convert.ToString(false);
        }
        // the callback URL on this site that will display the transaction result to the customer
        // (always required unless ResultDeliveryMethod = "SERVER" and PaymentFormDisplaysResult = "true")
        if (PayzoneSetting.ResultDeliveryMethod == RESULT_DELIVERY_METHOD.SERVER &&
            Convert.ToBoolean(m_szPaymentFormDisplaysResult))
        {
            m_szCallbackURL = "";
        }
        else
        {
            m_szCallbackURL = PaymentFormHelper.GetSiteSecureBaseURL(Request) + "DisplayTransactionResult.aspx?BookingID=1234";
        }

        // get the string to be hashed
        szStringToHash = PaymentFormHelper.GenerateStringToHash(PayzoneSetting.MerchantID,
                                                               PayzoneSetting.Password,
                                                               m_szAmount,
                                                               m_szCurrencyCode,
                                                               m_szOrderID,
                                                               m_szTransactionType,
                                                               m_szTransactionDateTime,
                                                               m_szCallbackURL,
                                                               m_szOrderDescription,
                                                               m_szCustomerName,
                                                               m_szAddress1,
                                                               m_szAddress2,
                                                               m_szAddress3,
                                                               m_szAddress4,
                                                               m_szCity,
                                                               m_szState,
                                                               m_szPostCode,
                                                               m_szCountryCode,
                                                               m_szCV2Mandatory,
                                                               m_szAddress1Mandatory,
                                                               m_szCityMandatory,
                                                               m_szPostCodeMandatory,
                                                               m_szStateMandatory,
                                                               m_szCountryMandatory,
                                                               PaymentFormHelper.GetResultDeliveryMethod(PayzoneSetting.ResultDeliveryMethod),
                                                               m_szServerResultURL,
                                                               m_szPaymentFormDisplaysResult,
                                                               PayzoneSetting.PreSharedKey,
                                                               PayzoneSetting.HashMethod);

        // pass this string into the hash function to create the hash digest
        m_szHashDigest = PaymentFormHelper.CalculateHashDigest(szStringToHash,
                                                               PayzoneSetting.PreSharedKey,
                                                               PayzoneSetting.HashMethod);

        //lbAmount.Text = m_szAmount;
        //lbCurrency.Text = m_szCurrencyCode;
        //lbOrderID.Text = m_szOrderID;
        //lbOrderDescription.Text = m_szOrderDescription;
    }
}