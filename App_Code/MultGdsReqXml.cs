using System;
using System.Collections;
using System.Xml;
using System.IO;
using System.Xml.XPath;
using System.Text;
using System.Xml.Xsl;
/// <summary>
/// Summary description for MultGdsReqXml
/// </summary>
public class MultGdsReqXml
{
    public MultGdsReqXml()
    {

    }
    //public static string getMultGdsReqXml(SearchParameters SearchParam)
    //{

    //    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
    //    string xmlStr = "<AirSearchQuery>" +
    //   "<Master>" +
    //   "<CompanyId>" + ApiCredentionl.companyID + "</CompanyId>" +
    //   "<AgentId>1</AgentId>" +
    //   "<BranchId>1</BranchId>" +
    //   "<CoustmerType>AGNT</CoustmerType>" +
    //   "</Master>";
    //    if (SearchParam.IsReturn)
    //    {
    //        xmlStr += "<JourneyType>R</JourneyType>";
    //    }
    //    else
    //    {
    //        xmlStr += "<JourneyType>O</JourneyType>";
    //    }

    //    xmlStr += "<Segments>" +
    //    "<Segment id='1'>" +
    //    "<Origin>" + SearchParam.FlyingFrom + "</Origin>" +
    //    "<Destination>" + SearchParam.GoingTo + "</Destination>" +
    //    "<Date>" + Convert.ToDateTime(SearchParam.DepartureDate).ToString("dd-MM-yyyy") + "</Date>" +
    //    "</Segment>";
    //    if (SearchParam.IsReturn)
    //    {
    //        xmlStr += "<Segment id='2'>" +
    //        "<Origin>" + SearchParam.GoingTo + "</Origin>" +
    //        "<Destination>" + SearchParam.FlyingFrom + "</Destination>" +
    //        "<Date>" + Convert.ToDateTime(SearchParam.ReturnDate).ToString("dd-MM-yyyy") + "</Date>" +
    //        "</Segment>";
    //    }
    //    xmlStr += "</Segments>" +
    //   "<PaxDetail>" +
    //   "<NoAdult>" + SearchParam.Adult.ToString() + "</NoAdult>" +
    //   "<NoChild>" + SearchParam.Child.ToString() + "</NoChild>" +
    //   "<NoInfant>" + SearchParam.Infant.ToString() + "</NoInfant>" +
    //   "</PaxDetail>";
    //    if (SearchParam.IsCalendar)
    //        xmlStr += "<Flexi>1</Flexi>";
    //    else
    //        xmlStr += "<Flexi>0</Flexi>";
    //    if (SearchParam.NonStop)
    //        xmlStr += "<Direct>1</Direct>";
    //    else
    //        xmlStr += "<Direct>0</Direct>";
    //    xmlStr += "<Cabin>" +
    //    "<Class>" + SearchParam.Service + "</Class>" +
    //    "</Cabin>" +
    //    "<Airlines><Airline>" +
    //    SearchParam.PreferedAirlines +
    //    "</Airline></Airlines>" +
    //     "<Authentication>" +
    //    "<HAP>" + ApiCredentionl.CredentialId + "</HAP>" +
    //    "<HapPassword>" + ApiCredentionl.CredentialPassword + "</HapPassword>" +
    //    "<HapType>" + ApiCredentionl.CredentialType + "</HapType>" +
    //    "</Authentication>" +
    //     "</AirSearchQuery>";

    //    return xmlStr;
    //}

    public static string getMultGdsReqXml(string _from, string _to, string _fromDate, string _toDate,
    bool isReturn, string _class, string prefAir, int _adults, int _childs, int _infants, bool isFlxDate, bool FNonStop)
    {

        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        string xmlStr = "<AirSearchQuery>" +
       "<Master>" +
       "<CompanyId>" + ApiCredentionl.companyID + "</CompanyId>" +
       "<AgentId>1</AgentId>" +
       "<BranchId>1</BranchId>" +
       "<CoustmerType>AGNT</CoustmerType>" +
       "</Master>";
        if (isReturn)
        {
            xmlStr += "<JourneyType>R</JourneyType>";
        }
        else
        {
            xmlStr += "<JourneyType>O</JourneyType>";
        }

        xmlStr += "<Segments>" +
        "<Segment id='1'>" +
        "<Origin>" + _from + "</Origin>" +
        "<Destination>" + _to + "</Destination>" +
        "<Date>" + Convert.ToDateTime(_fromDate).ToString("dd-MM-yyyy") + "</Date>" +
        "</Segment>";
        if (isReturn)
        {
            xmlStr += "<Segment id='2'>" +
            "<Origin>" + _to + "</Origin>" +
            "<Destination>" + _from + "</Destination>" +
            "<Date>" + Convert.ToDateTime(_toDate).ToString("dd-MM-yyyy") + "</Date>" +
            "</Segment>";
        }
        xmlStr += "</Segments>" +
       "<PaxDetail>" +
       "<NoAdult>" + _adults.ToString() + "</NoAdult>" +
       "<NoChild>" + _childs.ToString() + "</NoChild>" +
       "<NoInfant>" + _infants.ToString() + "</NoInfant>" +
       "</PaxDetail>";
        if (isFlxDate)
            xmlStr += "<Flexi>1</Flexi>";
        else
            xmlStr += "<Flexi>0</Flexi>";
        if (FNonStop)
            xmlStr += "<Direct>1</Direct>";
        else
            xmlStr += "<Direct>0</Direct>";
        xmlStr += "<Cabin>" +
        "<Class>" + _class + "</Class>" +
        "</Cabin>" +
        "<Airlines><Airline>" +
        prefAir +
        "</Airline></Airlines>" +
         "<Authentication>" +
        "<HAP>" + ApiCredentionl.CredentialId + "</HAP>" +
        "<HapPassword>" + ApiCredentionl.CredentialPassword + "</HapPassword>" +
        "<HapType>" + ApiCredentionl.CredentialType + "</HapType>" +
        "</Authentication>" +
         "</AirSearchQuery>";

        return xmlStr;
    }

//    public static string GetMultiGdsFareCheckReqXml(SearchParameters SearchParam, string Itinerary, PaxInfo _paxes,
//        string EmailID, string MobileNo, string PhoneNo)
//    {
//        try
//        {
//            string FareCheckXml = "<FareMatchRequest>";
//            FareCheckXml += "<FlightDetail>" +
//                             Itinerary +
//                            reqPaxDetail(_paxes, EmailID, MobileNo, PhoneNo) +
//                            "</PaxDetail>" +
//                            "<SearchDetail>" +
//                            getMultGdsReqXml(SearchParam) +
//                            "</SearchDetail>" +
//                            "</FareMatchRequest>";
//            return FareCheckXml;
//        }
//        catch
//        {
//            return "";
//        }
//    }

//    public static string GetMultiGdsFareCheckReqXml(SearchParameters SearchParam, string Itinerary, PaxInfo _paxes,
//       string EmailID, string MobileNo, string PhoneNo, string BookingID, string bookSession)
//    {
//        try
//        {

//            string FareCheckXml = "<BookingXML>" +
//               "<SessionId>" + bookSession + "</SessionId>" +
//                                  "<TempBookingId>" + BookingID + "</TempBookingId>";

//            FareCheckXml += "<FlightDetail>" +
//                             Itinerary +
//                            reqPaxDetail(_paxes, EmailID, MobileNo, PhoneNo) +
//                            "</PaxDetail>" +
//                            "<SearchDetail>" +
//                            getMultGdsReqXml(SearchParam) +
//                            "</SearchDetail>" +
//                            "</BookingXML>";
//            return FareCheckXml;
//        }
//        catch
//        {
//            return "";
//        }
//    }
//    public static string reqPaxDetail(PaxInfo _paxes, string email, string mobile, string phone)
//    {
//        ArrayList adt = new ArrayList();
//        ArrayList chd = new ArrayList();
//        ArrayList inf = new ArrayList();
//        int a = 1, c = 1, Inft = 1;
//        for (int i = 0; i < _paxes.Count; i++)
//        {
//            if (_paxes[i].PaxType.ToUpper() == "ADULT")
//            {
//                adt.Add("ADT" + a++);

//            }
//            else if (_paxes[i].PaxType.ToUpper() == "CHILD")
//            {
//                // chd.Add("CHD" + c++);
//                chd.Add("CHD1");
//            }
//            else
//            {
//                inf.Add("INF" + Inft++);
//            }
//        }
//        string reqPaxXml = "</FlightDetail>" +
//                             "<ClientDetail>" +
//                             "<ClientId>" + adt[0].ToString() + "</ClientId>" +
//                             "<Title>" + _paxes[0].Title + "</Title>" +
//                             "<FirstName>" + _paxes[0].FirstName + "</FirstName>" +
//                             "<LastName>" + _paxes[0].LastName + "</LastName>" +
//                             "<Age />" +
//                             "<DOB>" + Convert.ToDateTime(_paxes[0].DOB).ToString("dd-MM-yyyy") + "</DOB>" +
//                             "<Gender></Gender>" +
//                             "<Email>" + email + "</Email>" +
//                             "<Mobile>" + mobile + "</Mobile>" +
//                             "<Phone>" + phone + "</Phone>" +
//                             "<Meal>" + _paxes[0].Meal + "</Meal>" +
//                             "<Seat>" + _paxes[0].Seat + "</Seat>" +
//                             "<Passport />" +
//                             "<Nationality />" +
//                             "</ClientDetail>" +
//                             "<PaxDetail>" +
//                             "<NoOfAdult>" + adt.Count + "</NoOfAdult>" +
//                             "<NoOfChild>" + chd.Count + "</NoOfChild>" +
//                             "<NoOfInfant>" + inf.Count + "</NoOfInfant>" +
//                             "<AdditionalDetails>";
//        for (int i = 0; i < adt.Count; i++)
//        {
//            reqPaxXml += "<LocalId>" + adt[i].ToString() + "</LocalId>";
//        }
//        for (int i = 0; i < chd.Count; i++)
//        {
//            reqPaxXml += "<LocalId>" + chd[i].ToString() + "</LocalId>";
//        }
//        for (int i = 0; i < inf.Count; i++)
//        {
//            reqPaxXml += "<LocalId>" + inf[i].ToString() + "</LocalId>";
//        }
//        reqPaxXml += "</AdditionalDetails>";
//        a = 0; c = 0; Inft = 0;
//        for (int i = 0; i < _paxes.Count; i++)
//        {
//            if (_paxes[i].PaxType == "ADULT")
//            {
//                reqPaxXml += "<Adult>" +
//                 "<LocalId>" + adt[a] + "</LocalId>" +
//                 "<Type>ADT</Type>" +
//                 "<Title>" + _paxes[i].Title + "</Title>" +
//                 "<FirstName>" + _paxes[i].FirstName + "</FirstName>" +
//                 "<LastName>" + _paxes[i].LastName + "</LastName>" +
//                 "<Age />" +
//                 "<DOB>" + Convert.ToDateTime(_paxes[i].DOB).ToString("dd-MM-yyyy") + "</DOB>" +
//                 "<Gender></Gender>" +
//                 "<Email />" +
//                 "<Phone />" +
//                 "<Meal>" + _paxes[i].Meal + "</Meal>" +
//                 "<Seat>" + _paxes[i].Seat + "</Seat>" +
//                 "<Passport />" +
//                 "<Nationality />";
//                if (a < inf.Count)
//                {
//                    reqPaxXml += "<InfAsso>" + inf[a] + "</InfAsso>";
//                }
//                reqPaxXml += "</Adult>";
//                a++;
//            }
//            else if (_paxes[i].PaxType == "CHILD")
//            {
//                reqPaxXml += "<Child>" +
//                     "<LocalId>" + chd[c++] + "</LocalId>" +
//                     "<Type>CHD</Type>" +
//                     "<Title>" + _paxes[i].Title + "</Title>" +
//                     "<FirstName>" + _paxes[i].FirstName + "</FirstName>" +
//                     "<LastName>" + _paxes[i].LastName + "</LastName>" +
//                     "<Age />" +
//                      "<DOB>" + Convert.ToDateTime(_paxes[i].DOB).ToString("dd-MM-yyyy") + "</DOB>" +
//                     "<Gender></Gender>" +
//                     "<Email />" +
//                     "<Phone />" +
//                     "<Meal>" + _paxes[i].Meal + "</Meal>" +
//                     "<Seat>" + _paxes[i].Seat + "</Seat>" +
//                     "<Passport />" +
//                     "<Nationality />" +
//                     "<InfAsso />" +
//                 "</Child>";
//            }
//            else
//            {
//                reqPaxXml += "<Infant>" +
//                    "<LocalId>" + inf[Inft++] + "</LocalId>" +
//                    "<Type>INF</Type>" +
//                    "<Title>" + _paxes[i].Title + "</Title>" +
//                    "<FirstName>" + _paxes[i].FirstName + "</FirstName>" +
//                    "<LastName>" + _paxes[i].LastName + "</LastName>" +
//                    "<Age />" +
//                    "<DOB>" + Convert.ToDateTime(_paxes[i].DOB).ToString("dd-MM-yyyy") + "</DOB>" +
//                    "<Gender></Gender>" +
//                    "<Email />" +
//                    "<Phone />" +
//                    "<Meal>" + _paxes[i].Meal + "</Meal>" +
//                    "<Seat>" + _paxes[i].Seat + "</Seat>" +
//                    "<Passport />" +
//                    "<Nationality />" +
//                    "<InfAsso />" +
//                "</Infant>";
//            }
//        }
//        return reqPaxXml;

//    }

//    //    public static string booingDetailPaxDetail(XmlDocument xmlDoc)
//    //    {
//    //        string returnStr = @" <table width='600px'><tr style='font-size: 13px; font-family: Arial, Helvetica, sans-serif; font-weight: bold; color: #005193;'><td>S.No</td><td>Pax Type</td><td>Name</td><td>Date Of Birth</td></tr>";
//    //        int ctr = 1;

//    //        XmlNodeList xmlNode = xmlDoc.SelectNodes("/BookingXML/PaxDetail/Adult");
//    //        foreach (XmlNode Node in xmlNode)
//    //        {
//    //            returnStr += " <tr style='font-size: 12px; font-family: Arial, Helvetica, sans-serif; font-weight: normal; color:#000000;'>" +
//    //                     "<td>" + ctr++.ToString() + "</td><td>Adult</td>" +
//    //                     "<td>" + Node["Title"].InnerText + " " + Node["FirstName"].InnerText + " " + Node["LastName"].InnerText + "</td>" +
//    //                     "<td>" + Convert.ToDateTime(Node["DOB"].InnerText).ToString("dd MMM yyyy") + "</td></tr>";
//    //        }
//    //        xmlNode = xmlDoc.SelectNodes("/BookingXML/PaxDetail/Child");
//    //        foreach (XmlNode Node in xmlNode)
//    //        {
//    //            returnStr += " <tr style='font-size: 12px; font-family: Arial, Helvetica, sans-serif; font-weight: normal; color:#000000;'>" +
//    //                    "<td>" + ctr++.ToString() + "</td><td>Child</td>" +
//    //                    "<td>" + Node["Title"].InnerText + " " + Node["FirstName"].InnerText + " " + Node["LastName"].InnerText + "</td>" +
//    //                    "<td>" + Convert.ToDateTime(Node["DOB"].InnerText).ToString("dd MMM yyyy") + "</td></tr>";
//    //        }
//    //        xmlNode = xmlDoc.SelectNodes("/BookingXML/PaxDetail/Infant");
//    //        foreach (XmlNode Node in xmlNode)
//    //        {
//    //            returnStr += " <tr style='font-size: 12px; font-family: Arial, Helvetica, sans-serif; font-weight: normal; color:#000000;'>" +
//    //                    "<td>" + ctr++.ToString() + "</td><td>Infant</td>" +
//    //                    "<td>" + Node["Title"].InnerText + " " + Node["FirstName"].InnerText + " " + Node["LastName"].InnerText + "</td>" +
//    //                    "<td>" + Convert.ToDateTime(Node["DOB"].InnerText).ToString("dd MMM yyyy") + "</td></tr>";
//    //        }
//    //        returnStr += "</table>";
//    //        return returnStr;
//    //    }

//    //    public static string booingDetailSectorDetail(XmlDocument xmlDoc)
//    //    {
//    //        XmlNodeList xmlNode = xmlDoc.SelectNodes("/BookingXML/FlightDetail/Itinerary/Sectors/Sector");
//    //        string returnStr = @"<table cellpadding='2' cellspacing='2' width='100%'>                               
//    //                        <tr style='font-size: 12px; font-family: Arial, Helvetica, sans-serif; font-weight: bold; color: #005193;'>
//    //                        <td width='19%'>From</td>
//    //                        <td width='19%'>To</td>
//    //                        <td width='7%'>Flight No</td>
//    //                        <td width='9%'>Cabin Class</td>
//    //                        <td width='5%'>Ter.In</td>
//    //                        <td width='5%'>Ter.Out</td>
//    //                        <td width='18%'>Dept. Date & Time</td>
//    //                        <td width='18%'>Arrival Date & Time</td></tr>";

//    //        foreach (XmlNode Node in xmlNode)
//    //        {
//    //            returnStr += "<tr style='font-size: 12px; font-family: Arial, Helvetica, sans-serif; font-weight: normal; color:#000000;'>" +
//    //                             "<td>" + Node.SelectSingleNode("Departure/CityName").InnerText + ",<br/>" + Node.SelectSingleNode("Departure/AirpName").InnerText + " (" + Node.SelectSingleNode("Departure/AirpCode").InnerText + ")</td>" +
//    //                             "<td>" + Node.SelectSingleNode("Arrival/CityName").InnerText + ",<br/>" + Node.SelectSingleNode("Arrival/AirpName").InnerText + " (" + Node.SelectSingleNode("Arrival/AirpCode").InnerText + ")</td>" +
//    //                             "<td valign='top'>" + Node["AirV"].InnerText + " " + Node["FltNum"].InnerText + " </td>" +
//    //                             "<td valign='top'>" + Node.SelectSingleNode("CabinClass/Des").InnerText + "</td>";
//    //            if (string.IsNullOrEmpty(Node.SelectSingleNode("Departure/Terminal").InnerText))
//    //                returnStr += "<td valign='top'>-</td>";
//    //            else
//    //                returnStr += "<td valign='top'>" + Node.SelectSingleNode("Departure/Terminal").InnerText + "</td>";

//    //            if (string.IsNullOrEmpty(Node.SelectSingleNode("Arrival/Terminal").InnerText))
//    //                returnStr += "<td valign='top'>-</td>";
//    //            else
//    //                returnStr += "<td valign='top'>" + Node.SelectSingleNode("Arrival/Terminal").InnerText + "</td>";
//    //            returnStr += "<td valign='top' style='font-size:11px;'>" + Node.SelectSingleNode("Departure/Day").InnerText + ", " + Node.SelectSingleNode("Departure/Date").InnerText + " " + Node.SelectSingleNode("Departure/Time").InnerText + "</td>" +
//    //                         "<td valign='top' style='font-size:11px;'>" + Node.SelectSingleNode("Arrival/Day").InnerText + ", " + Node.SelectSingleNode("Arrival/Date").InnerText + " " + Node.SelectSingleNode("Arrival/Time").InnerText + "</td></tr>";

//    //        }

//    //        returnStr += "</table>";
//    //        return returnStr;
//    //    }

//    //    public static string booingDetailPriceDetail(XmlDocument xmlDoc, ref double totCost)
//    //    {
//    //        int totPax = 0;
//    //        string returnStr = @"<table width='600' border='0' cellspacing='0' cellpadding='0'>
//    //                            <tr style='font-size: 13px; font-family: Arial, Helvetica, sans-serif; font-weight: bold; color: #005193;'>
//    //                                <td width='150'>Pax Type</td>
//    //                                <td width='100' height='20'>Fare per person</td>
//    //                                <td width='50'>&nbsp;</td>
//    //                                <td width='100'>Number of pax</td>
//    //                                <td width='50'>&nbsp;</td>
//    //                                <td width='150'>Total Price</td>
//    //                            </tr>
//    //                            <tr>
//    //                                <td height='10' colspan='6'>
//    //                                </td>
//    //                            </tr>";

//    //        XmlNodeList xmlNode = xmlDoc.SelectNodes("/BookingXML/FlightDetail/Itinerary/Adult");
//    //        returnStr += "<tr style='font-size: 12px; font-family: Arial, Helvetica, sans-serif; color:#000000;'>" +
//    //                                "<td>Adult</td>" +
//    //                                "<td>&pound;" + (Convert.ToDouble(xmlNode[0]["AdtBFare"].InnerText) + Convert.ToDouble(xmlNode[0]["AdTax"].InnerText) + Convert.ToDouble(xmlNode[0]["markUp"].InnerText) + Convert.ToDouble(xmlNode[0]["Commission"].InnerText)).ToString("f2") + "</td>" +
//    //                                "<td>X</td>" +
//    //                                "<td>" + xmlNode[0]["NoAdult"].InnerText + "</td>" +
//    //                                "<td>=</td>" +
//    //                                "<td>&pound;" + ((Convert.ToDouble(xmlNode[0]["AdtBFare"].InnerText) + Convert.ToDouble(xmlNode[0]["AdTax"].InnerText) + Convert.ToDouble(xmlNode[0]["markUp"].InnerText) + Convert.ToDouble(xmlNode[0]["Commission"].InnerText)) * Convert.ToDouble(xmlNode[0]["NoAdult"].InnerText)).ToString("f2") + "</td></tr>" +
//    //                                "<tr><td height='10' colspan='6'></td></tr>";
//    //        totPax += Convert.ToInt32(xmlNode[0]["NoAdult"].InnerText);
//    //        xmlNode = xmlDoc.SelectNodes("/BookingXML/FlightDetail/Itinerary/Child");
//    //        if (xmlNode.Count > 0)
//    //        {
//    //            returnStr += "<tr style='font-size: 12px; font-family: Arial, Helvetica, sans-serif; color:#000000;'>" +
//    //                                    "<td>Child</td>" +
//    //                                    "<td>&pound;" + (Convert.ToDouble(xmlNode[0]["ChdBFare"].InnerText) + Convert.ToDouble(xmlNode[0]["CHTax"].InnerText) + Convert.ToDouble(xmlNode[0]["markUp"].InnerText) + Convert.ToDouble(xmlNode[0]["Commission"].InnerText)).ToString("f2") + "</td>" +
//    //                                    "<td>X</td>" +
//    //                                    "<td>" + xmlNode[0]["NoChild"].InnerText + "</td>" +
//    //                                    "<td>=</td>" +
//    //                                    "<td>&pound;" + ((Convert.ToDouble(xmlNode[0]["ChdBFare"].InnerText) + Convert.ToDouble(xmlNode[0]["CHTax"].InnerText) + Convert.ToDouble(xmlNode[0]["markUp"].InnerText) + Convert.ToDouble(xmlNode[0]["Commission"].InnerText)) * Convert.ToDouble(xmlNode[0]["NoChild"].InnerText)).ToString("f2") + "</td></tr>" +
//    //                                    "<tr><td height='10' colspan='6'></td></tr>";
//    //            totPax += Convert.ToInt32(xmlNode[0]["NoChild"].InnerText);
//    //        }
//    //        xmlNode = xmlDoc.SelectNodes("/BookingXML/FlightDetail/Itinerary/Infant");
//    //        if (xmlNode.Count > 0)
//    //        {
//    //            returnStr += "<tr style='font-size: 12px; font-family: Arial, Helvetica, sans-serif; color:#000000;'>" +
//    //                                    "<td>Infant</td>" +
//    //                                    "<td>&pound;" + (Convert.ToDouble(xmlNode[0]["InfBFare"].InnerText) + Convert.ToDouble(xmlNode[0]["InTax"].InnerText) + Convert.ToDouble(xmlNode[0]["markUp"].InnerText) + Convert.ToDouble(xmlNode[0]["Commission"].InnerText)).ToString("f2") + "</td>" +
//    //                                    "<td>X</td>" +
//    //                                    "<td>" + xmlNode[0]["NoInfant"].InnerText + "</td>" +
//    //                                    "<td>=</td>" +
//    //                                    "<td>&pound;" + ((Convert.ToDouble(xmlNode[0]["InfBFare"].InnerText) + Convert.ToDouble(xmlNode[0]["InTax"].InnerText) + Convert.ToDouble(xmlNode[0]["markUp"].InnerText) + Convert.ToDouble(xmlNode[0]["Commission"].InnerText)) * Convert.ToDouble(xmlNode[0]["NoInfant"].InnerText)).ToString("f2") + "</td></tr>" +
//    //                                    "<tr><td height='10' colspan='6'></td></tr>";
//    //            totPax += Convert.ToInt32(xmlNode[0]["NoInfant"].InnerText);
//    //        }
//    //        double totBookintCost = Convert.ToDouble(xmlDoc.SelectSingleNode("./BookingXML/FlightDetail/Itinerary/GrandTotal").InnerText);


//    //        returnStr += " <tr><td height='10' colspan='6'><hr /></td></tr>" +
//    //                                "<tr style='font-size: 12px; font-family: Arial, Helvetica, sans-serif; color:#000000;'><td>Airlines Failure Fee</td>" +
//    //                                "<td>&pound;" + SuperPage.SAFIFee.ToString() + "</td>" +
//    //                                "<td>X</td>" +
//    //                                "<td>" + totPax.ToString() + "</td>" +
//    //                                "<td>=</td>" +
//    //                                "<td>&pound;" + (SuperPage.SAFIFee * Convert.ToDouble(totPax)).ToString("f2") + "</td></tr>" +
//    //                                "<tr><td height='10' colspan='6'></td></tr>" +
//    //                                "<tr style='font-size: 12px; font-family: Arial, Helvetica, sans-serif; color:#000000;'><td>ATOL Protection Charges.</td>" +
//    //                                "<td>&pound;" + SuperPage.ATOLFee.ToString() + "</td>" +
//    //                                "<td>X</td>" +
//    //                                "<td>" + totPax.ToString() + "</td>" +
//    //                                "<td>=</td>" +
//    //                                "<td>&pound;" + (SuperPage.ATOLFee * Convert.ToDouble(totPax)).ToString("f2") + "</td></tr>";
//    //        totBookintCost += ((SuperPage.ATOLFee + SuperPage.SAFIFee) * Convert.ToDouble(totPax));

//    //        returnStr += "<tr><td height='10' colspan='6'><hr /></td></tr>" +
//    //                                    "<tr style='font-size: 13px; font-family: Arial, Helvetica, sans-serif; color: black; font-weight: bold;'>" +
//    //                                    "<td align='right'colspan='4' style='padding-right: 15px;'><b>Total Cost</b></td>" +
//    //                                    "<td>=</td>" +
//    //                                    "<td>&pound;" + (totBookintCost).ToString("f2") + "</td></tr></table>";
//    //        totCost = totBookintCost;
//    //        return returnStr;

//    //    }

//    public static string GetMailBody(XmlDocument xmlDoc, string xsltPath, string htmlType)
//    {
//        MemoryStream stream = new MemoryStream(ASCIIEncoding.Default.GetBytes(xmlDoc.OuterXml));
//        XPathDocument document = new XPathDocument(stream);
//        StringWriter writer = new StringWriter();
//        XslCompiledTransform transform = new XslCompiledTransform(true);
//        XsltArgumentList xsltArguments = null;
//        GetRecords objGetRecord = new GetRecords();
//        xsltArguments = new XsltArgumentList();
//        xsltArguments.AddExtensionObject("urn:actl-xslt", objGetRecord);
//        xsltArguments.AddParam("type", "", htmlType);
//        XsltSettings xsltSettings = new XsltSettings();
//        xsltSettings.EnableScript = true;
//        transform.Load(xsltPath, xsltSettings, new XmlUrlResolver());
//        transform.Transform(document, xsltArguments, writer);
//        return writer.ToString();
//    }

//    public static bool MakePnrRequestXml(string strXmlDetails, SearchParameters _searchParam,
//        string Itinerary, PaxInfo _paxes,
//       string EmailID, string MobileNo, string PhoneNo, string BookingID, string bookSession, string filePath)
//    {
//        try
//        {
//            ReadWrite.SaveWSFile("<SaveXml><PnrRequestXml>" +
//                GetMultiGdsFareCheckReqXml(_searchParam, Itinerary, _paxes, EmailID, MobileNo, PhoneNo, BookingID, bookSession) +
//                "</PnrRequestXml><Payment>" + strXmlDetails + "</Payment></SaveXml>", filePath);
//            return true;
//        }
//        catch { return false; }
//    }

//    public static string ConfirmationPageAndMail(ref XmlDocument xmlDoc, bool PnrFirmStatus, bool isMailBody)
//    {
//        StringBuilder sb = new StringBuilder();
//        try
//        {
//            sb.Append(@"<html><head> <style type='text/css'>
//                    .confm-cont {
//                        width: 60%;
//                        height: auto;
//                        margin: 0 auto;
//                        font-size: 12px;
//                        line-height: 22px;
//                    }
//
//                    .fl-details {
//                        color: #fff;
//                        background-color: #103360;
//                        font: normal 13px arial;
//                        border-bottom: 1px solid #e8e8e8;
//                        padding-top: 10px;
//                        padding-left: 10px;
//                        padding-bottom: 10px;
//                        margin-top: 10px;
//                        margin-bottom: 10px;
//                    }
//
//                    .confm-cont h3 {
//                        color: #fc7700;
//                        font: normal 13px arial;
//                        border-bottom: 1px solid #e8e8e8;
//                        padding-top: 10px;
//                        padding-bottom: 10px;
//                        margin-top: 10px;
//                        margin-bottom: 10px;
//                    }
//                    .confm-cont h4 {
//                        color: #424242;
//                        font: bold 13px arial;
//                        border-bottom: 1px solid #e8e8e8;
//                        padding-top: 10px;
//                         padding-left: 10px;
//                        padding-bottom: 10px;
//                        margin-top: 10px;
//                        margin-bottom: 10px;
//                        background-color:#e8e8e8;
//                    }
//
//                    .confm-rt {
//                        color: #fc7700;
//                        font: normal 16px arial;
//                        padding-bottom: 5px;
//                        float: right;
//                    }
//
//                    .price-rt {
//                        color: #fc7700;
//                        font: normal 16px arial;
//                    }
//
//                    .bd-main-out {
//                        height: auto;
//                        margin: 10px auto;
//                    }
//
//                        .bd-main-out > ul {
//                            width: 50%;
//                            float: left;
//                            margin: 0px;
//                            line-height: 25px;
//                        }
//                            .bd-main-out > ul > li {
//                                width: 100%;
//                            }
//
//                    .mains {
//                        border-bottom: 1px solid #e8e8e8;
//                        color: #999;
//                        margin-top: 5px;
//                    }");
//            if (!isMailBody)
//            {
//                sb.Append(@"@media (max-width:450px) {
//                        .confm-cont {
//                            width: 90%;
//                            height: auto;
//                            margin: 0 auto;
//                            font-size: 12px;
//                            line-height: 22px;
//                        }
//
//                            .confm-cont h3 {
//                                color: #fc7700;
//                                font: normal 13px arial;
//                                border-bottom: 1px solid #e8e8e8;
//                                padding-top: 10px;
//                                padding-bottom: 10px;
//                                margin-top: 10px;
//                                margin-bottom: 10px;
//                            }
//
//                        .confm-rt {
//                            color: #fc7700;
//                            font: normal 16px arial;
//                            padding-bottom: 5px;
//                            float: left;
//                            width: 100%;
//                        }
//
//                        .bd-main-out {
//                            height: auto;
//                            margin: 0 auto;
//                        }
//
//                            .bd-main-out > ul {
//                                width: 100%;
//                                margin: 0px;
//                            }
//
//                                .bd-main-out > ul > li {
//                                    width: 100%;
//                                }
//                    }");
//            }
//            sb.Append(@"</style></head>");
//            sb.Append("<body>");
//            XmlNode xnd = null;
//            if (PnrFirmStatus)
//                xnd = xmlDoc.SelectSingleNode("SaveXml/PnrResponseXml");
//            else
//                xnd = xmlDoc.SelectSingleNode("SaveXml/PnrRequestXml");
//            sb.Append("<div class='confm-cont'>" +
//           "<div class='confm-rt'>Your booking reference no | " + xnd.SelectSingleNode("BookingXML/TempBookingId").InnerText + "</div>" +
//           "<p class='book-font'></p>" +
//            "<h3>" + xnd.SelectSingleNode("BookingXML/ClientDetail/Title").InnerText + " " + xnd.SelectSingleNode("BookingXML/ClientDetail/FirstName").InnerText + " " + xnd.SelectSingleNode("BookingXML/ClientDetail/LastName").InnerText + "</h3>");
//            if (PnrFirmStatus)
//                sb.Append("<p class='borderin'>Thank you for booking with flightspro. Your booking is confirmed and we will e-mail you your E-ticket with 48 hours. For more information please call our customer care support on <span class='book-font'>" + WebsiteContactDetails.ContactNo1 + "/" + WebsiteContactDetails.ContactNo2 + " </span>or <span class='book-font'>e-mail us on " + WebsiteContactDetails.EmailID1 + "</span></p>");
//            else
//                sb.Append("<p class='borderin'>Booking is decline due to non - availability of seats with the same class of service while confirming your reservation. Please accept our apologies for the inconvenience. For more information please call our customer care team for alternate flights options on <span class='book-font'>" + WebsiteContactDetails.ContactNo1 + "/" + WebsiteContactDetails.ContactNo2 + " </span>or <span class='book-font'>e-mail us on " + WebsiteContactDetails.EmailID1 + "</span></p>");
//            sb.Append("<div class='bd-main-out'>" +
//                "<ul>" +
//                    "<li><b>Phone No | </b>" + xnd.SelectSingleNode("BookingXML/ClientDetail/Mobile").InnerText + "</li>" +
//                "</ul>" +
//                "<ul>" +
//                    "<li><b>EmailID | </b>" + xnd.SelectSingleNode("BookingXML/ClientDetail/Email").InnerText + "</li>" +
//                "</ul>" +
//                "<ul>" +
//                    "<li><b>Destination | </b> " + xnd.SelectSingleNode("BookingXML/FlightDetail/Itinerary/Sectors/Sector[isReturn='false'][last()]/Arrival/CityName").InnerText + ", " + xnd.SelectSingleNode("BookingXML/FlightDetail/Itinerary/Sectors/Sector[isReturn='false'][last()]/Arrival/AirpName").InnerText + ", " + xnd.SelectSingleNode("BookingXML/FlightDetail/Itinerary/Sectors/Sector[isReturn='false'][last()]/Arrival/AirpCode").InnerText + " </li>" +
//                "</ul>" +
//                "<ul>" +
//                    "<li><b>Booking Date | </b>" + DateTime.Now.ToString("ddd, dd MMM yyyy") + "</li>" +
//                "</ul>" +
//                "<ul>" +
//                "<li class='confm-rt'><b>PNR | </b>" + (xnd.SelectSingleNode("BookingXML/FlightDetail/Itinerary/PNRInfo/PNRNo") != null ? xnd.SelectSingleNode("BookingXML/FlightDetail/Itinerary/PNRInfo/PNRNo").InnerText : xnd.SelectSingleNode("BookingXML/TempBookingId").InnerText) + "</li>" +
//                "</ul>" +
//                "<ul>" +
//                    "<li><b>Travel Date | </b>" + xnd.SelectSingleNode("BookingXML/FlightDetail/Itinerary/Sectors/Sector[1]/Departure/Date").InnerText + " " + xnd.SelectSingleNode("BookingXML/FlightDetail/Itinerary/Sectors/Sector[1]/Departure/Time").InnerText + "</li>" +
//                "</ul>" +
//            "</div>" +
//            "<div style='clear: both;'></div>" +
//            "<div class='fl-details'>Passenger Details</div>");
//            int ctr = 1;
//            foreach (XmlNode xn in xnd.SelectNodes("BookingXML/PaxDetail/Adult"))
//            {
//                sb.Append("<div class='bd-main-out'>" +
//                   "<ul>" +
//                       "<li><b>Passenger | </b>" + (ctr++.ToString()) + "</li>" +
//                       "<li><b>Pax Type | </b> Adult</li>" +
//                   "</ul>" +
//                   "<ul>" +
//                       "<li><b>Name | </b>" + xn.SelectSingleNode("Title").InnerText + " " + xn.SelectSingleNode("FirstName").InnerText + " " + xn.SelectSingleNode("LastName").InnerText + "</li>" +
//                       "<li><b>Date Of Birth | </b>" + xn.SelectSingleNode("DOB").InnerText + "</li>" +
//                   "</ul>" +
//               "</div>" +
//               "<div style='clear: both;'></div>");
//            }
//            foreach (XmlNode xn in xnd.SelectNodes("BookingXML/PaxDetail/Child"))
//            {
//                sb.Append("<div class='bd-main-out'>" +
//                   "<ul>" +
//                       "<li><b>Passenger | </b>" + (ctr++.ToString()) + "</li>" +
//                       "<li><b>Pax Type | </b> Adult</li>" +
//                   "</ul>" +
//                   "<ul>" +
//                       "<li><b>Name | </b>" + xn.SelectSingleNode("Title").InnerText + " " + xn.SelectSingleNode("FirstName").InnerText + " " + xn.SelectSingleNode("LastName").InnerText + "</li>" +
//                       "<li><b>Date Of Birth | </b>" + xn.SelectSingleNode("DOB").InnerText + "</li>" +
//                   "</ul>" +
//               "</div>" +
//               "<div style='clear: both;'></div>");
//            }
//            foreach (XmlNode xn in xnd.SelectNodes("BookingXML/PaxDetail/Infant"))
//            {
//                sb.Append("<div class='bd-main-out'>" +
//                   "<ul>" +
//                       "<li><b>Passenger | </b>" + (ctr++.ToString()) + "</li>" +
//                       "<li><b>Pax Type | </b> Adult</li>" +
//                   "</ul>" +
//                   "<ul>" +
//                       "<li><b>Name | </b>" + xn.SelectSingleNode("Title").InnerText + " " + xn.SelectSingleNode("FirstName").InnerText + " " + xn.SelectSingleNode("LastName").InnerText + "</li>" +
//                       "<li><b>Date Of Birth | </b>" + xn.SelectSingleNode("DOB").InnerText + "</li>" +
//                   "</ul>" +                 
//               "</div>" +
//               "<div style='clear: both;'></div>");
//            }
//            sb.Append("<div class='fl-details'>Flight Details</div>");
//            ctr = 0;
//            foreach (XmlNode xn in xnd.SelectNodes("BookingXML/FlightDetail/Itinerary/Sectors/Sector[isReturn='false']"))
//            {
//                sb.Append("<div class='bd-main-out'>" +
//                    (ctr == 0 ? "<h4>InBound</h4>" : "") +
//                   "<ul class='mains' style='width: 100%;'>" +
//                       "<li><b>" + xn.SelectSingleNode("AirlineName").InnerText + " | </b>" + xn.SelectSingleNode("AirV").InnerText + " - " + xn.SelectSingleNode("FltNum").InnerText + "</li>" +
//                   "</ul>" +
//                   "<ul style='width: 100%;'>" +
//                       "<li><b>" + Convert.ToDateTime(xn.SelectSingleNode("Departure/Date").InnerText).ToString("dddd dd MMMM yyyy") + "</b> | duration  " + xn.SelectSingleNode("ActualTime").InnerText + "</li>" +
//                   "</ul>" +
//                   "<ul>" +
//                       "<li>Dep:<b> " + xn.SelectSingleNode("Departure/Time").InnerText + "</b></li>" +
//                       "<li>Arr: <b>" + xn.SelectSingleNode("Arrival/Time").InnerText + "</b></li>" +
//                   "</ul>" +
//                   "<ul>" +
//                   "<li><b>" + xn.SelectSingleNode("Departure/CityName").InnerText + ", " + xn.SelectSingleNode("Departure/CountryName").InnerText + "</b> | " + xn.SelectSingleNode("Departure/AirpName").InnerText + " " + xn.SelectSingleNode("Departure/AirpCode").InnerText + (xn.SelectSingleNode("Departure/Terminal").InnerText != "" ? (" | Terminal " + xn.SelectSingleNode("Departure/Terminal").InnerText) : "") + "</li>" +
//                       "<li><b>" + xn.SelectSingleNode("Arrival/CityName").InnerText + ", " + xn.SelectSingleNode("Arrival/CountryName").InnerText + " </b> | " + xn.SelectSingleNode("Arrival/AirpName").InnerText + " " + xn.SelectSingleNode("Arrival/AirpCode").InnerText + (xn.SelectSingleNode("Arrival/Terminal").InnerText != "" ? (" | Terminal " + xn.SelectSingleNode("Arrival/Terminal").InnerText) : "") + "</li>" +
//                   "</ul>" +
//               "</div>");
//                ctr++;
//            }
//            ctr = 0;
//            foreach (XmlNode xn in xnd.SelectNodes("BookingXML/FlightDetail/Itinerary/Sectors/Sector[isReturn='true']"))
//            {
//                sb.Append("<div style='clear: both;'></div>" +
//                "<div class='bd-main-out'>" +
//                   (ctr == 0 ? "<h4>OutBound</h4>" : "") +
//                  "<ul class='mains' style='width: 100%;'>" +
//                       "<li><b>" + xn.SelectSingleNode("AirlineName").InnerText + " | </b>" + xn.SelectSingleNode("AirV").InnerText + " - " + xn.SelectSingleNode("FltNum").InnerText + "</li>" +
//                   "</ul>" +
//                   "<ul style='width: 100%;'>" +
//                       "<li><b>" + Convert.ToDateTime(xn.SelectSingleNode("Departure/Date").InnerText).ToString("dddd dd MMMM yyyy") + "</b> | duration  " + xn.SelectSingleNode("ActualTime").InnerText + "</li>" +
//                   "</ul>" +
//                   "<ul>" +
//                       "<li>Dep:<b> " + xn.SelectSingleNode("Departure/Time").InnerText + "</b></li>" +
//                       "<li>Arr: <b>" + xn.SelectSingleNode("Arrival/Time").InnerText + "</b></li>" +
//                   "</ul>" +
//                   "<ul>" +
//                   "<li><b>" + xn.SelectSingleNode("Departure/CityName").InnerText + ", " + xn.SelectSingleNode("Departure/CountryName").InnerText + "</b> | " + xn.SelectSingleNode("Departure/AirpName").InnerText + " " + xn.SelectSingleNode("Departure/AirpCode").InnerText + (xn.SelectSingleNode("Departure/Terminal").InnerText != "" ? (" | Terminal " + xn.SelectSingleNode("Departure/Terminal").InnerText) : "") + "</li>" +
//                       "<li><b>" + xn.SelectSingleNode("Arrival/CityName").InnerText + ", " + xn.SelectSingleNode("Arrival/CountryName").InnerText + " </b> | " + xn.SelectSingleNode("Arrival/AirpName").InnerText + " " + xn.SelectSingleNode("Arrival/AirpCode").InnerText + (xn.SelectSingleNode("Arrival/Terminal").InnerText != "" ? (" | Terminal " + xn.SelectSingleNode("Arrival/Terminal").InnerText) : "") + "</li>" +
//                   "</ul>" +
//               "</div>");
//                ctr++;
//            }
//            sb.Append(@"<div style='clear: both;'></div>
//                        <div class='bd-main-out'>
//                           <div class='fl-details'>IMPORTANT TERMS &amp; CONDITIONS</div>
//                            <p>
//                                1.<b>	Please note: -</b> Airfares are guaranteed upon ticketing only. If there would be any issue with the payment, we will notify you as soon as possible through email and phone. Otherwise, we will send you the ticket within 48 hours of your booking.
//                            </p>
//                            <p>
//                                2.	We advise you to reconfirm your both outbound /inbound flight timings 72 hours prior to the departure with the airline helpdesk. Airline may change the flight schedule or departure terminal any time.
//                            </p>
//                            <p>
//                                3.	All passengers departing UK must be advised that all personal electrical devices like phones, laptops, tablets, computers, music players etc. must be charged so that if requested by airport security they can be switched on and shown that they are working. If the device is not shown to be working then it will have to be left at the airport and on some occasions the passenger may be denied boarding.
//                            </p>
//                            <p>
//                                4.	All passengers travelling to the United States or transiting via the United States will have their personal devices checked prior to boarding.
//                            </p>
//                            <p>
//                                5.	Due to security measures we recommend our passengers to reach airport at least 3 hours prior to scheduled departure of the flight.
//                            </p>
//                            <p>
//                                6.	All travelers’ passport must be valid for minimum 6 months after the date of your return journey .You must ensure that your passport and visas are valid for all destinations on your journey. This includes transit airports also.
//                            </p>
//                            <p>
//                                7.	Once your booking is confirmed and if there is any error in name, amendment on a flight ticket is not permitted by the airline. Make sure you should enter the correct name as per passport.
//                            </p>
//                            <p>
//                                8.	All British passengers flying to United States or transiting in the United States must have an authorization of the ESTA (Electronic System for Travel Authorization) in their possession.
//                            </p>
//                            <p>
//                                9.	In order to fly to Australia for business and/or for pleasure, (for a stay not more than 90 days), an Electronic Travel Authorization called “ETA” or E-Visitor (depending on the nationality).
//                            </p>
//                            <p>
//                                10.	Free baggage allowance provide to the passenger where is applicable by the airlines side, varying according to routes and class of seat. Airlines may charge additional fee for checked-in baggage and extra baggage or other optional services. Please call the airlines directly for the most recent updates regarding the baggage allowance, weight and dimensions of bags.
//                            </p>
//                            <p>
//                                11.	Once the ticket is issued, your ticket will become non- refundable &amp; non changeable. Any changes or cancellation may incur a fee. However in some cases the airlines may not allowed any change.
//                            </p>
//                            <p>
//                                12.	If your flight has a change involving two different airports with the itinerary, it is your responsibility to organize the transfer to the right airport and also check the transit visa requirement.
//                            </p>                           
//                        </div>
//                    </div></body></html>");
//        }
//        catch (Exception)
//        {

//            throw;
//        }
//        return sb.ToString();
//    }
}
