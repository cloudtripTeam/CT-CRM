using Ionic.Zlib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;

/// <summary>
/// Summary description for FlightService
/// </summary>
public class FlightFareService
{
    public FlightFareService()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string SearchFare(string _from, string _to, string _fromDate, string _toDate, bool isReturn, string _class, string prefAir, int _adults, int _childs, int _infants, bool isFlxDate, bool FNonStop, string currency)
    {
        try
        {
            return CallFlightService(Get_Search_SOAP_Request(_from, _to, _fromDate, _toDate, isReturn, _class, prefAir, _adults, _childs, _infants, isFlxDate, FNonStop, currency), "GetLowFares");
        }
        catch (System.Threading.ThreadAbortException)
        {
            return "<GetLowFaresResponse><Error>ThreadAbortException</Error></GetLowFaresResponse>";
        }
    }
    public string CallFlightService(string RQ, string SOAPActionName)
    {
        //return "";
        HttpWebRequest Request;
        try
        {
            Request = (HttpWebRequest)WebRequest.Create(ConfigurationSettings.AppSettings["FlightService"].ToString());
            Request.Credentials = CredentialCache.DefaultCredentials;
            Request.Proxy = null;
            Request.Headers.Add("Accept-Encoding: gzip");
            Request.Headers.Add("SOAPAction: \"http://tempuri.org/" + SOAPActionName + "\"");
            Request.ContentType = "text/xml;charset=UTF-8";
            Request.Method = "POST";
            Stream s = Request.GetRequestStream();
            s.Write(System.Text.Encoding.ASCII.GetBytes(RQ.ToString()), 0, RQ.ToString().Length);

            s.Close();

            XmlDocument XMLdoc = new XmlDocument();
            try
            {
                HttpWebResponse Response = (HttpWebResponse)Request.GetResponse();
                Stream compressedStream = Response.GetResponseStream();

                Stream decompressedStream = null;
                if (Response.ContentEncoding.ToLower().Contains("gzip"))
                {
                    decompressedStream = new GZipStream(compressedStream, CompressionMode.Decompress);
                }
                else
                {
                    decompressedStream = compressedStream;
                }

                XMLdoc.Load(decompressedStream);

                XmlNode filteredResponse = XMLdoc.SelectSingleNode("//*[local-name()='Body']/*");

                return filteredResponse.OuterXml;

                // return XMLdoc.OuterXml;

            }
            catch (WebException EX)
            {
                Stream receiveStream = null;

                if (EX.Response != null)
                {
                    receiveStream = EX.Response.GetResponseStream();
                    StreamReader streamReader = new StreamReader(receiveStream, Encoding.UTF8);
                    string result = streamReader.ReadToEnd();
                    XmlDocument responseXmlDocument = new XmlDocument();
                    XmlDocument filteredDocument = null;
                    responseXmlDocument.LoadXml(result);
                    XmlNode filteredResponse = responseXmlDocument.SelectSingleNode("//*[local-name()='Body']/*");
                    filteredDocument = new XmlDocument();
                    return filteredResponse.OuterXml;
                }
                else
                {
                    return "";
                }
            }

        }
        catch (Exception ex)
        {
            return "";
        }


    }
    private string Get_Search_SOAP_Request(string _from, string _to, string _fromDate, string _toDate, bool isReturn, string _class, string prefAir, int _adults,
        int _childs, int _infants, bool isFlxDate, bool FNonStop, string currency)
    {
        string query = "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">" +
                  "<soap:Body>" +
                    "<GetLowFares xmlns=\"http://tempuri.org/\">" +
                      "<FligthSearchRQ> <![CDATA[" + SearchXmlRQ(_from, _to, _fromDate, _toDate, isReturn, _class, prefAir, _adults, _childs, _infants, isFlxDate, FNonStop,currency) + "]]></FligthSearchRQ>" +
                    "</GetLowFares>" +
                  "</soap:Body>" +
                "</soap:Envelope>";
        return query;

    }
    private string SearchXmlRQ(string _from, string _to, string _fromDate, string _toDate, bool isReturn, string _class, string prefAir, int _adults, int _childs,
         int _infants, bool isFlxDate, bool FNonStop,string currency)
    {
        string xmlSearchRQ = string.Empty; string companyId = string.Empty; string CredentialId = string.Empty;   string CredentialPassword = string.Empty;  
        if (currency == "GBP")
        {
            companyId = "TRVJUNCTION"; CredentialId = "TRVJUNCTION"; CredentialPassword = "tr@365";
        }
        else if (currency == "USD")
        { companyId = "TRIPFARE"; CredentialId = "TRIPFARE"; CredentialPassword = "tfusa@547"; }
        else if (currency == "CAD")
        { companyId = "TRVJUNCTION_CA"; CredentialId = "TRVJUNCTION_CA"; CredentialPassword = "tr@365"; }
        else if (currency == "EUR")
        { companyId = "TRVJUNCTION_EU"; CredentialId = "TRVJUNCTION_EU"; CredentialPassword = "tr@365"; }

        xmlSearchRQ = "<AirFareSearchRQ>" +
      "<Authentication>" +
          "<CompanyId>" + companyId + "</CompanyId>" +
          "<CredentialId>" + CredentialId + "</CredentialId>" +
          "<CredentialPassword>" + CredentialPassword + "</CredentialPassword>" +
          "<CredentialType>LIVE</CredentialType>" +
      "</Authentication>" +
      "<JourneyType>" + (isReturn ? "R" : "O") + "</JourneyType>" +
      "<Segments>" +
          "<Segment id='1'>" +
            "<Origin>" + _from + "</Origin>" +
            "<Destination>" + _to + "</Destination>" +
            "<Date>" + _fromDate + "</Date>" +
        "</Segment>";
        if (isReturn)
        {
            xmlSearchRQ += "<Segment id='2'>" +
                 "<Origin>" + _to + "</Origin>" +
                 "<Destination>" + _from + "</Destination>" +
                 "<Date>" + _toDate + "</Date>" +
             "</Segment>";
        }
        xmlSearchRQ += "</Segments>" +
      "<PaxDetail>" +
          "<NoAdult>" + _adults + "</NoAdult>" +
          "<NoChild>" + _childs + "</NoChild>" +
          "<NoInfant>" + _infants + "</NoInfant>" +
      "</PaxDetail>" +
      "<Flexi>" + (isFlxDate ? 1 : 0) + "</Flexi>" +
      "<Direct>" + (FNonStop ? 1 : 0) + "</Direct>" +
      "<Cabin>" +
          "<Class>" + _class + "</Class>" +
      "</Cabin>" +
      "<Airlines>" +
          "<Airline>" + prefAir + "</Airline>" +
      "</Airlines>" +
  "</AirFareSearchRQ>";
        return xmlSearchRQ;
    }
}