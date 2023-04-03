using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Newtonsoft.Json;

/// <summary>
/// Summary description for Model
/// </summary>
public class DDLModel
{
    
    public DDLModel(string Value, string Text)
    {
        DdlValue = Value;
        DdlText = Text;
    }
    public string DdlValue { set; get; }
    public string DdlText { set; get; }
}
public class CompanyAndBookingStatus
{
    public CompanyAndBookingStatus(DataRow dr)
    {
        CompanyID = dr["CompanyID"].ToString();
        CompanyName = dr["CompanyName"].ToString();
        BookingStatus = dr["BookingStatus"].ToString();
    }
    public string CompanyID { set; get; }
    public string CompanyName { set; get; }
    public string BookingStatus { set; get; }
}
public class AllBookingDetail
{
    public AllBookingDetail(DataRow dr)
    {
        BookingID = dr["BookingID"].ToString();
        ProdID = dr["ProdID"].ToString();
        CurrencyType = dr["CurrencyType"].ToString();
        BookingByCompany = dr["BookingByCompany"].ToString();
        Provider = dr["Provider"].ToString();
        BookingBy = dr["BookingBy"].ToString();
        BookingByType = dr["BookingByType"].ToString();
        BookingDateTime = string.IsNullOrEmpty(dr["BookingDateTime"].ToString()) ? "" : Convert.ToDateTime(dr["BookingDateTime"]).ToString("dd MMM yy");
        BookingStatus = dr["BookingStatus"].ToString();
        BookingRemarks = dr["BookingRemarks"].ToString();
        TotAmt = dr["TotAmt"].ToString();
        PNR = dr["PNR"].ToString();
        SourceMedia = dr["SourceMedia"].ToString();
        ProductType = dr["ProductType"].ToString();
        isLocked = dr["isLocked"].ToString();
        ModifiedBy = dr["ModifiedBy"].ToString();
        Supplier = dr["Supplier"].ToString();
        MailIssued = dr["MailIssued"].ToString();
        ModifiedDate = string.IsNullOrEmpty(dr["ModifiedDate"].ToString()) ? "" : Convert.ToDateTime(dr["ModifiedDate"]).ToString("dd MMM yy HH:mm");
        PhoneNo = dr["PhoneNo"].ToString();
        MobileNo = dr["MobileNo"].ToString();
        EmailID = dr["EmailID"].ToString();
        Title = dr["Title"].ToString();
        FName = dr["FName"].ToString();
        MName = dr["MName"].ToString();
        LName = dr["LName"].ToString();
        TravelDate = string.IsNullOrEmpty(dr["TravelDate"].ToString()) ? "" : Convert.ToDateTime(dr["TravelDate"]).ToString("dd MMM yy");
    }

    public string BookingID { set; get; }
    public string ProdID { set; get; }
    public string CurrencyType { set; get; }
    public string BookingByCompany { set; get; }
    public string Provider { set; get; }
    public string BookingBy { set; get; }
    public string BookingByType { set; get; }
    public string BookingDateTime { set; get; }
    public string BookingStatus { set; get; }
    public string BookingRemarks { set; get; }
    public string TotAmt { set; get; }
    public string PNR { set; get; }
    public string SourceMedia { set; get; }
    public string ProductType { set; get; }
    public string isLocked { set; get; }
    public string ModifiedBy { set; get; }
    public string Supplier { set; get; }
    public string MailIssued { set; get; }
    public string ModifiedDate { set; get; }
    public string PhoneNo { set; get; }
    public string MobileNo { set; get; }
    public string EmailID { set; get; }
    public string Title { set; get; }
    public string FName { set; get; }
    public string MName { set; get; }
    public string LName { set; get; }
    public string TravelDate { set; get; }
}
namespace Itinerary
{
    public class BookingMaster
    {
        public BookingMaster(DataRow dr)
        {
            BookingID = dr["BookingID"].ToString();
            InvoiceNo = dr["InvoiceNo"].ToString();
            BookingType = dr["BookingType"].ToString();
            CurrencyType = dr["CurrencyType"].ToString();
            BookingByCompany = dr["BookingByCompany"].ToString();
            BookingStatus = dr["BookingStatus"].ToString();
            InvoiceDate = dr["Invoice_Date"].ToString();
        }
        public string BookingID { set; get; }
        public string InvoiceNo { set; get; }
        public string BookingType { set; get; }
        public string CurrencyType { set; get; }
        public string BookingByCompany { set; get; }
        public string BookingStatus { set; get; }
        public string InvoiceDate { set; get; }

    }
    public class BookingDetails
    {
        public BookingDetails(DataRow dr)
        {
            BookingID = dr["BookingID"].ToString();
            ProdID = dr["ProdID"].ToString();
            Provider = dr["Provider"].ToString();
            BookingBy = dr["BookingBy"].ToString();
            BookingByType = dr["BookingByType"].ToString();
            BookingDateTime = string.IsNullOrEmpty(dr["BookingDateTime"].ToString()) ? "" : Convert.ToDateTime(dr["BookingDateTime"]).ToString("dd MMM yy HH:mm");
            BookingStatus = dr["BookingStatus"].ToString();
            BookingRemarks = dr["BookingRemarks"].ToString();
            TotAmt = string.IsNullOrEmpty(dr["TotAmt"].ToString()) ? "0.00" : Convert.ToDouble(dr["TotAmt"]).ToString("f2");
            PNR = dr["PNR"].ToString();
            SourceMedia = dr["SourceMedia"].ToString();
            ProductType = dr["ProductType"].ToString();
            isLocked = dr["isLocked"].ToString();
            ModifiedBy = dr["ModifiedBy"].ToString();
            Supplier = dr["Supplier"].ToString();
            MailIssued = dr["MailIssued"].ToString();
            AtolType = dr["ATOL_Type"].ToString();
            ModifiedDate = string.IsNullOrEmpty(dr["ModifiedDate"].ToString()) ? "" : Convert.ToDateTime(dr["ModifiedDate"]).ToString("dd MMM yy HH:mm");
            SupplierRef = dr["Supplier_Ref"].ToString();
            AssignedTo = dr["Booking_Assigned_To"].ToString();
        }
        public string BookingID { set; get; }
        public string ProdID { set; get; }
        public string Provider { set; get; }
        public string BookingBy { set; get; }
        public string BookingByType { set; get; }
        public string BookingDateTime { set; get; }
        public string BookingStatus { set; get; }
        public string BookingRemarks { set; get; }
        public string TotAmt { set; get; }
        public string PNR { set; get; }
        public string SourceMedia { set; get; }
        public string ProductType { set; get; }
        public string isLocked { set; get; }
        public string ModifiedBy { set; get; }
        public string Supplier { set; get; }
        public string MailIssued { set; get; }
        public string ModifiedDate { set; get; }
        public string AtolType { set; get; }
        public string SupplierRef { set; get; }
        public string AssignedTo { set; get; }

        //These Field added for the Show City And Country based on Ipaddress
        public string IpAddress { get; set; }
        public string IpCity { get; set; }
        public string IpCountry { get; set; }
    }

    public class PassengerDetail
    {
        public PassengerDetail(DataRow dr)
        {
            SrNo = dr["SrNo"].ToString();
            BookingID = dr["BookingID"].ToString();
            ProdID = dr["ProdID"].ToString();
            PaxID = dr["PaxID"].ToString();
            Title = dr["Title"].ToString();
            FName = dr["FName"].ToString();
            MName = dr["MName"].ToString();
            LName = dr["LName"].ToString();
            //FFNo = dr["FFNo"].ToString();
            //PNo = dr["PNo"].ToString();
            //Nationality = dr["Nationality"].ToString();
            //PExpDate = string.IsNullOrEmpty(dr["PExpDate"].ToString()) ? "" : Convert.ToDateTime(dr["PExpDate"]).ToString("dd MMM yy");
            //POI = dr["POI"].ToString();
            //POB = dr["POB"].ToString();
            DOB = string.IsNullOrEmpty(dr["DOB"].ToString()) ? "" : Convert.ToDateTime(dr["DOB"]).ToString("dd MMM yy");
            PaxType = dr["PaxType"].ToString();
            PaxSex = dr["PaxSex"].ToString();
            ModifiedBy = dr["ModifiedBy"].ToString();
            ModifiedDate = string.IsNullOrEmpty(dr["ModifiedDate"].ToString()) ? "" : Convert.ToDateTime(dr["ModifiedDate"]).ToString("dd MMM yy HH:mm");
            Tickets = dr["Tickets"].ToString();
        }
        public string SrNo { set; get; }
        public string BookingID { set; get; }
        public string ProdID { set; get; }
        public string PaxID { set; get; }
        public string Title { set; get; }
        public string FName { set; get; }
        public string MName { set; get; }
        public string LName { set; get; }
        public string FFNo { set; get; }
        public string PNo { set; get; }
        public string Nationality { set; get; }
        public string PExpDate { set; get; }
        public string POI { set; get; }
        public string POB { set; get; }
        public string DOB { set; get; }
        public string PaxType { set; get; }
        public string PaxSex { set; get; }
        public string ModifiedBy { set; get; }
        public string ModifiedDate { set; get; }
        public string Tickets { set; get; }

    }
    public class ContactDetails
    {
        public ContactDetails(DataRow dr)
        {
            SrNo = dr["SrNo"].ToString();
            BookingID = dr["BookingID"].ToString();
            ProdID = dr["ProdID"].ToString();
            PaxID = dr["PaxID"].ToString();
            PhoneNo = dr["PhoneNo"].ToString();
            MobileNo = dr["MobileNo"].ToString();
            Fax = dr["Fax"].ToString();
            EmailID = dr["EmailID"].ToString();
            Country = dr["Country"].ToString();
            PState = dr["PState"].ToString();
            City = dr["City"].ToString();
            PAddress = dr["PAddress"].ToString();
            PostCode = dr["PostCode"].ToString();
            AddressType = dr["AddressType"].ToString();
            ModifiedBy = dr["ModifiedBy"].ToString();
            ModifiedDate = string.IsNullOrEmpty(dr["ModifiedDate"].ToString()) ? "" : Convert.ToDateTime(dr["ModifiedDate"]).ToString("dd MMM yy HH:mm");
        }
        public string SrNo { set; get; }
        public string BookingID { set; get; }
        public string ProdID { set; get; }
        public string PaxID { set; get; }
        public string PhoneNo { set; get; }
        public string MobileNo { set; get; }
        public string Fax { set; get; }
        public string EmailID { set; get; }
        public string Country { set; get; }
        public string PState { set; get; }
        public string City { set; get; }
        public string PAddress { set; get; }
        public string PostCode { set; get; }
        public string AddressType { set; get; }
        public string ModifiedBy { set; get; }
        public string ModifiedDate { set; get; }

    }
    public class SectorMaster
    {
        public SectorMaster(DataRow dr)
        {
            BookingID = dr["BookingID"].ToString();
            ProdID = dr["ProdID"].ToString();
            JType = dr["JType"].ToString();
            LastTktDate = dr["LastTktDate"].ToString();
            Origin = dr["Origin"].ToString();
            Destination = dr["Destination"].ToString();
            ValCarrier = dr["ValCarrier"].ToString();
            CabinClass = dr["CabinClass"].ToString();
            ModifiedBy = dr["ModifiedBy"].ToString();
            IssuedBy = dr["Ticket_IssuedBy"].ToString();
            IssuedDate = dr["Ticket_IssuedDate"].ToString();
            ModifiedDate = string.IsNullOrEmpty(dr["ModifiedDate"].ToString()) ? "" : Convert.ToDateTime(dr["ModifiedDate"]).ToString("dd MMM yy HH:mm");
        }
        public string BookingID { set; get; }
        public string ProdID { set; get; }
        public string JType { set; get; }
        public string LastTktDate { set; get; }
        public string Origin { set; get; }
        public string Destination { set; get; }
        public string ValCarrier { set; get; }
        public string CabinClass { set; get; }
        public string ModifiedBy { set; get; }
        public string ModifiedDate { set; get; }
        public string IssuedBy { set; get; }
        public string IssuedDate { set; get; }

    }
    public class SectorDetails
    {
        public SectorDetails(DataRow dr)
        {
            SrNo = dr["SrNo"].ToString();
            BookingID = dr["BookingID"].ToString();
            ProdID = dr["ProdID"].ToString();
            CarierName = dr["CarierName"].ToString();
            FromDest = dr["FromDest"].ToString();
            FromDateTime = string.IsNullOrEmpty(dr["FromDateTime"].ToString()) ? "" : Convert.ToDateTime(dr["FromDateTime"]).ToString("dd MMM yy HH:mm");
            ToDest = dr["ToDest"].ToString();
            ToDateTime = string.IsNullOrEmpty(dr["ToDateTime"].ToString()) ? "" : Convert.ToDateTime(dr["ToDateTime"]).ToString("dd MMM yy HH:mm");
            FlightNo = dr["FlightNo"].ToString();
            FClass = dr["FClass"].ToString();
            FStatus = dr["FStatus"].ToString();
            FareBasis = dr["FareBasis"].ToString();
            NotValidBefor = dr["NotValidBefor"].ToString();
            NotValidAfter = dr["NotValidAfter"].ToString();
            BaggageAllownce = dr["BaggageAllownce"].ToString();
            TerminalFrom = dr["TerminalFrom"].ToString();
            TerminalTo = dr["TerminalTo"].ToString();
            SegID = dr["SegID"].ToString();
            SegRemarks = dr["SegRemarks"].ToString();
            TripID = dr["TripID"].ToString();
            ModifiedBy = dr["ModifiedBy"].ToString();
            ModifiedDate = string.IsNullOrEmpty(dr["ModifiedDate"].ToString()) ? "" : Convert.ToDateTime(dr["ModifiedDate"]).ToString("dd MMM yy HH:mm");
            AirlineConfirmationCode = dr["AirlineConfirmationCode"].ToString();
            CabinClass = Common.GetCabinClass(Convert.ToString(dr["CabinClass"].ToString()));
        }
        public string SrNo { set; get; }
        public string BookingID { set; get; }
        public string ProdID { set; get; }
        public string CarierName { set; get; }
        public string FromDest { set; get; }
        public string FromDateTime { set; get; }
        public string ToDest { set; get; }
        public string ToDateTime { set; get; }
        public string FlightNo { set; get; }
        public string FClass { set; get; }
        public string FStatus { set; get; }
        public string FareBasis { set; get; }
        public string NotValidBefor { set; get; }
        public string NotValidAfter { set; get; }
        public string BaggageAllownce { set; get; }
        public string TerminalFrom { set; get; }
        public string TerminalTo { set; get; }
        public string SegID { set; get; }
        public string SegRemarks { set; get; }
        public string TripID { set; get; }
        public string ModifiedBy { set; get; }
        public string ModifiedDate { set; get; }
        public string AirlineConfirmationCode { set; get; }
        public string CabinClass { get; set; }


        



    }

    public class AmountChargesDetail
    {
        public AmountChargesDetail(DataRow dr)
        {
            SrNo = dr["SrNo"].ToString();
            BookingID = dr["BookingID"].ToString();
            ProdID = dr["ProdID"].ToString();
            ChargeID = dr["ChargeID"].ToString();
            ChargesFor = dr["ChargesFor"].ToString();
            CostPrice = string.IsNullOrEmpty(dr["CostPrice"].ToString()) ? "0.00" : Convert.ToDouble(dr["CostPrice"]).ToString("f2");
            SellPrice = string.IsNullOrEmpty(dr["SellPrice"].ToString()) ? "0.00" : Convert.ToDouble(dr["SellPrice"]).ToString("f2");
            ChargesStatus = dr["ChargesStatus"].ToString();
            SupplireID = dr["SupplireID"].ToString();
            ChrgesRemarks = dr["ChrgesRemarks"].ToString();
            ChargesDate = string.IsNullOrEmpty(dr["ChargesDate"].ToString()) ? "" : Convert.ToDateTime(dr["ChargesDate"]).ToString("dd MMM yy");
            ModifiedBy = dr["ModifiedBy"].ToString();
            ModifiedDate = string.IsNullOrEmpty(dr["ModifiedDate"].ToString()) ? "" : Convert.ToDateTime(dr["ModifiedDate"]).ToString("dd MMM yy HH:mm");
            NoOfPax = dr["NoOfPax"].ToString();
        }
        public string SrNo { set; get; }
        public string BookingID { set; get; }
        public string ProdID { set; get; }
        public string ChargeID { set; get; }
        public string ChargesFor { set; get; }
        public string CostPrice { set; get; }
        public string SellPrice { set; get; }
        public string ChargesStatus { set; get; }
        public string SupplireID { set; get; }
        public string ChrgesRemarks { set; get; }
        public string ChargesDate { set; get; }
        public string ModifiedBy { set; get; }
        public string ModifiedDate { set; get; }
        public string NoOfPax { set; get; }
    }
    public class TransactionMaster
    {
        public TransactionMaster(DataRow dr)
        {          
            BookingID = dr["BookingID"].ToString();
            TrnsNo = dr["TrnsNo"].ToString();
            TrnsType = dr["TrnsType"].ToString();
            TrnsPaymentStatus = dr["TrnsPaymentStatus"].ToString();
            TrnsAmount = string.IsNullOrEmpty(dr["TrnsAmount"].ToString()) ? "0.00" : Convert.ToDouble(dr["TrnsAmount"]).ToString("f2");
            TrnsCurrencyType = dr["TrnsCurrencyType"].ToString();
            TrnsBy = dr["TrnsBy"].ToString();
            TrnsDateTime = string.IsNullOrEmpty(dr["TrnsDateTime"].ToString()) ? "" : Convert.ToDateTime(dr["TrnsDateTime"]).ToString("dd MMM yy HH:mm");
            TrnsRemarks = dr["TrnsRemarks"].ToString();
            TrnsSecurityKey = dr["TrnsSecurityKey"].ToString();
            TrnsStatus = dr["TrnsStatus"].ToString();
            TrnsStatusDetail = dr["TrnsStatusDetail"].ToString();
            TrnsAuthNo = dr["TrnsAuthNo"].ToString();            
            TrnsModifiedBy = dr["TrnsModifiedBy"].ToString();
            TrnsModifiedDate = string.IsNullOrEmpty(dr["TrnsModifiedDate"].ToString()) ? "" : Convert.ToDateTime(dr["TrnsModifiedDate"]).ToString("dd MMM yy HH:mm");
            TrnsCardCVV = dr["Card_CVV"].ToString();
            TrnsCardExp = dr["Card_Expiry"].ToString();
            TrnsCard_No = dr["Card_No"].ToString();
            TrnsHolder_Name = dr["Holder_Name"].ToString();
            TrnsCard_Address = dr["Card_Address"].ToString();
            


        }
       
        public string BookingID { set; get; }
        public string TrnsNo { set; get; }
        public string TrnsType { set; get; }
        public string TrnsPaymentStatus { set; get; }
        public string TrnsAmount { set; get; }
        public string TrnsCurrencyType { set; get; }
        public string TrnsBy { set; get; }
        public string TrnsDateTime { set; get; }
        public string TrnsRemarks { set; get; }
        public string TrnsSecurityKey { set; get; }
        public string TrnsStatus { set; get; }
        public string TrnsStatusDetail { set; get; }      
        public string TrnsAuthNo { set; get; }       
        public string TrnsModifiedBy { set; get; }
        public string TrnsModifiedDate { set; get; }
        public string TrnsCardCVV { set; get; }
        public string TrnsCardExp { set; get; }
        public string TrnsCard_No { set; get; }
        public string TrnsHolder_Name { set; get; }
        public string TrnsCard_Address { set; get; }
        
    }
    public class TransactionDetail
    {
        public TransactionDetail(DataRow dr)
        {
            BookingID = dr["BookingID"].ToString();
            TrnsNo = dr["TrnsNo"].ToString();
            TrnsType = dr["TrnsType"].ToString();
            TrnsPaymentStatus = dr["TrnsPaymentStatus"].ToString();
            TrnsAmount = string.IsNullOrEmpty(dr["TrnsAmount"].ToString()) ? "0.00" : Convert.ToDouble(dr["TrnsAmount"]).ToString("f2");
            TrnsCurrencyType = dr["TrnsCurrencyType"].ToString();
            TrnsBy = dr["TrnsBy"].ToString();
            TrnsDateTime = string.IsNullOrEmpty(dr["TrnsDateTime"].ToString()) ? "" : Convert.ToDateTime(dr["TrnsDateTime"]).ToString("dd MMM yy HH:mm");
            TrnsRemarks = dr["TrnsRemarks"].ToString();
            TrnsSecurityKey = dr["TrnsSecurityKey"].ToString();
            TrnsStatus = dr["TrnsStatus"].ToString();
            TrnsStatusDetail = dr["TrnsStatusDetail"].ToString();
            TrnsAuthNo = dr["TrnsAuthNo"].ToString();
            TrnsModifiedBy = dr["TrnsModifiedBy"].ToString();
            TrnsModifiedDate = string.IsNullOrEmpty(dr["TrnsModifiedDate"].ToString()) ? "" : Convert.ToDateTime(dr["TrnsModifiedDate"]).ToString("dd MMM yy HH:mm");
        }

        public string BookingID { set; get; }
        public string TrnsNo { set; get; }
        public string TrnsType { set; get; }
        public string TrnsPaymentStatus { set; get; }
        public string TrnsAmount { set; get; }
        public string TrnsCurrencyType { set; get; }
        public string TrnsBy { set; get; }
        public string TrnsDateTime { set; get; }
        public string TrnsRemarks { set; get; }
        public string TrnsSecurityKey { set; get; }
        public string TrnsStatus { set; get; }
        public string TrnsStatusDetail { set; get; }
        public string TrnsAuthNo { set; get; }
        public string TrnsModifiedBy { set; get; }
        public string TrnsModifiedDate { set; get; }
    }



    public class FlightDetails
    {
        public FlightDetails(string BID, string PID, bool ChkBM, bool ChkBD, bool ChkPD, bool ChkCD, bool ChkSM, bool ChkSD, bool ChkACD, bool ChkTM,bool ChkAD)
        {
            GetSetDatabase objGetSetDatabase = new GetSetDatabase();
            if (ChkBM)
            {
                DataTable dt = objGetSetDatabase.GET_Booking_Master(BID);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        BM = new BookingMaster(dt.Rows[0]);
                    }
                }
            }
            if (ChkBD)
            {
                DataTable dt = objGetSetDatabase.GET_Booking_Detail(BID, PID, "", "", "", "", "", "", "", "","");
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        Common common = new Common();
                        BD = new BookingDetails(dt.Rows[0]);
                        string ipAddress = string.Empty;
                        string ipCity = string.Empty;
                        string ipCountry = string.Empty;
                        Common.IpInfo ipInfo = new Common.IpInfo();
                        DataTable ipDt = null;

                        var getComp = objGetSetDatabase.GET_Booking_Master(BID);
                        if (getComp.Rows.Count > 0)
                        {
                            if (Convert.ToString(getComp.Rows[0]["BookingByCompany"]) == "TRVJUNCTION_USA" ||
                            Convert.ToString(getComp.Rows[0]["BookingByCompany"]) == "FLTTROTT") //This check is added for New IP-ADDRESS only for US Team and Flight Trotters
                            {
                                ipDt = objGetSetDatabase.GET_SET_IPAddress(BID, "Select_USA", "", "");
                                if (ipDt.Rows.Count > 0)
                                {
                                    BD.IpAddress = Convert.ToString(ipDt.Rows[0]["IPAddress"]);
                                    BD.IpCity = common.GET_SET_IpCityCountry(Convert.ToString(ipDt.Rows[0]["IPCountry"]), Convert.ToString(ipDt.Rows[0]["IPCity"]), "", "City");
                                    BD.IpCountry = common.GET_SET_IpCityCountry(Convert.ToString(ipDt.Rows[0]["IPCountry"]), Convert.ToString(ipDt.Rows[0]["IPCity"]), "", "Country");
                                }
                            }
                            else
                            {
                                ipDt = objGetSetDatabase.GET_SET_IPAddress(BID, "Select", "", "");
                                if (ipDt.Rows.Count > 0)
                                {
                                    ipAddress = Convert.ToString(ipDt.Rows[0]["IPAddress"]);
                                    ipCity = Convert.ToString(ipDt.Rows[0]["IPCity"]);
                                    ipCountry = Convert.ToString(ipDt.Rows[0]["IPCountry"]);
                                }

                                if (string.IsNullOrEmpty(ipCity) && string.IsNullOrEmpty(ipCountry))
                                {
                                    //Fetch City and Country 1 st time based on IpAddress and Update into DB
                                    ipInfo = common.GetIpAddress(ipAddress);
                                    ipDt = objGetSetDatabase.GET_SET_IPAddress(BID, "Update", ipInfo.city, ipInfo.country_name);
                                    BD.IpAddress = ipAddress;
                                    BD.IpCity = ipInfo.city == null || ipInfo.city == "" ? "No City associated with this IP-Address" : ipInfo.city;
                                    BD.IpCountry = ipInfo.country_name == null || ipInfo.country_name == "" ? "No Country associated with this IP-Address" : ipInfo.country_name;
                                }
                                else
                                {
                                    // from Database Values
                                    BD.IpAddress = ipAddress;
                                    BD.IpCity = (ipCity == null || ipCity == "") ? "No City associated with this IP-Address" : ipCity;
                                    BD.IpCountry = (ipCountry == null || ipCountry == "") ? "No Country associated with this IP-Address" : ipCountry;
                                }
                            }
                        }
                    }
                }
            }
            if (ChkPD)
            {
                List<PassengerDetail> _pd = new List<PassengerDetail>();
                DataTable dt = objGetSetDatabase.GET_Passenger_Detail(BID, PID);
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        _pd.Add(new PassengerDetail(dr));
                    }
                }
                PD = _pd;
            }
            if (ChkCD)
            {
                DataTable dt = objGetSetDatabase.GET_Contact_Detail(BID, PID);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        CD = new ContactDetails(dt.Rows[0]);
                    }
                }
            }
            if (ChkSM)
            {
                DataTable dt = objGetSetDatabase.GET_Sectors_Master(BID, PID);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        SM = new SectorMaster(dt.Rows[0]);
                    }
                }
            }
            if (ChkSD)
            {
                List<SectorDetails> _sd = new List<SectorDetails>();
                DataTable dt = objGetSetDatabase.GET_Sector_Detail(BID, PID);
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        _sd.Add(new SectorDetails(dr));
                    }
                }
                SD = _sd;
            }
            if (ChkACD)
            {
                List<AmountChargesDetail> _acd = new List<AmountChargesDetail>();
                DataTable dt = objGetSetDatabase.GET_Amount_Charges_Detail(BID, PID, "", "", "", "", "", "", "");
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        _acd.Add(new AmountChargesDetail(dr));
                    }
                }
                ACD = _acd;
            }
            if (ChkTM)
            {
                List<TransactionMaster> _tm = new List<TransactionMaster>();
                DataTable dt = objGetSetDatabase.GET_Transaction_Master(BID,"");
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        
                        foreach (DataRow dr in dt.Rows)
                        {
                            _tm.Add(new TransactionMaster(dr));
                           
                        }
                        TM = _tm;

                    }
                }
            }

            if (ChkBD)
            {
                List<Remarks> _rmk = new List<Remarks>();
                DataTable dt = objGetSetDatabase.GET_Booking_Remarks("Select",BID);
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        _rmk.Add(new Remarks(dr));
                    }
                }
                RM = _rmk;
            }

            if(ChkAD)
            {
                List<AuthDoc> _adl = new List<AuthDoc>();
                DataTable dt = objGetSetDatabase.Get_Only_Doc_Upload(BID);
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        _adl.Add(new AuthDoc(dr));
                    }
                }
                AD = _adl;
            }
        }
        public BookingMaster BM { set; get; }
        public BookingDetails BD { set; get; }
        public List<PassengerDetail> PD { set; get; }
        public ContactDetails CD { set; get; }
        public SectorMaster SM { set; get; }
        public List<SectorDetails> SD { set; get; }
        public List<AmountChargesDetail> ACD { set; get; }
        public List<TransactionMaster> TM { set; get; }

        public List<Remarks> RM { set; get; }
        public List<AuthDoc> AD { get; set; }
    }
    public class AuthDoc
    {
        public AuthDoc(DataRow dr)
        {
            SrNo = dr["id"].ToString();
            DocType = dr["DocType"].ToString();
            DocPath = dr["DocPath"].ToString();
            BookingId = dr["BookingId"].ToString();
            UploadDate = string.IsNullOrEmpty(dr["UploadDate"].ToString()) ? "" : Convert.ToDateTime(dr["UploadDate"]).ToString("dd MMM yy HH:mm:ss");
        }
        public string SrNo { set; get; }
        public string DocType { set; get; }
        public string DocPath { set; get; }
        public string BookingId { set; get; }
        public string UploadDate { set; get; }


    }
    public class Remarks
    {
        public Remarks(DataRow dr)
        {
            SrNo = dr["SNo"].ToString();
            BookingID = dr["Ref_No"].ToString();
            Remark = dr["Remarks"].ToString();
            RemarksBy = dr["Remarks_By"].ToString();
            DatenTime = string.IsNullOrEmpty(dr["DatenTime"].ToString()) ? "" : Convert.ToDateTime(dr["DatenTime"]).ToString("dd MMM yy HH:mm:ss");
        }
        public string SrNo { set; get; }
        public string BookingID { set; get; }
        public string Remark { set; get; }
        public string RemarksBy { set; get; }
        public string DatenTime { set; get; }
        

    }
}

public class FlightMarkupDetail
{
    public FlightMarkupDetail(DataRow dr)
    {
        ID = dr["MarkupID"].ToString();
        MarkupFrom = dr["MarkupFrom"].ToString();
        MarkupTo = dr["MarkupTo"].ToString();
        AirV = dr["AirV"].ToString();
        Provider = dr["Provider"].ToString();
        Category = dr["Category"].ToString();
        MarkupClass = dr["MarkupClass"].ToString();
        FareType = dr["FareType"].ToString();
        Amount = string.IsNullOrEmpty(dr["Amount"].ToString()) ? "" : Convert.ToDouble(dr["Amount"]).ToString("f2");
        AmountType = dr["AmountType"].ToString();
        ValidFromDate = string.IsNullOrEmpty(dr["ValidFromDate"].ToString()) ? "" : Convert.ToDateTime(dr["ValidFromDate"]).ToString("dd MMM yy");
        ValidToDate = string.IsNullOrEmpty(dr["ValidToDate"].ToString()) ? "" : Convert.ToDateTime(dr["ValidToDate"]).ToString("dd MMM yy");
        CompanyID = dr["CompanyID"].ToString();
        CampID = dr["CampID"].ToString();
        JourneyType = dr["JourneyType"].ToString();
        ModifiedBy = dr["ModifiedBy"].ToString();
        ModifiedDate = string.IsNullOrEmpty(dr["ModifiedDate"].ToString()) ? "" : Convert.ToDateTime(dr["ModifiedDate"]).ToString("dd MMM yy HH:mm");
    }
    public string ID { set; get; }
    public string MarkupFrom { set; get; }
    public string MarkupTo { set; get; }
    public string AirV { set; get; }
    public string Provider { set; get; }
    public string Category { set; get; }
    public string MarkupClass { set; get; }
    public string FareType { set; get; }
    public string Amount { set; get; }
    public string AmountType { set; get; }
    public string ValidFromDate { set; get; }
    public string ValidToDate { set; get; }
    public string CompanyID { set; get; }
    public string CampID { set; get; }
    public string JourneyType { set; get; }
    public string ModifiedBy { set; get; }
    public string ModifiedDate { set; get; }
}
namespace AdminMenuDetails
{
    public class Menu
    {
        public Menu(DataRow dr)
        {
            MenuName = dr["MenuName"].ToString();
            MenuSequence = dr["MenuSequence"].ToString();
            PageName = dr["PageName"].ToString();
            PageUrl = dr["PageUrl"].ToString();
        }
        public string MenuName { set; get; }
        public string MenuSequence { set; get; }
        public string PageName { set; get; }
        public string PageUrl { set; get; }
    }
    public class MenuDetails
    {
        List<Menu> _menu = new List<Menu>();
        public MenuDetails(string _userID, DataTable dt)
        {
            UserID = _userID;
            foreach (DataRow dr in dt.Rows)
            {
                _menu.Add(new Menu(dr));
            }
            Menu = _menu;
        }
        public List<Menu> Menu { set; get; }
        public string UserID { set; get; }

    }
}
public class DumpAirOffer
{
    public DumpAirOffer(DataRow dr)
    {
        FaredetailId = dr["FaredetailId"].ToString();
        From = dr["From"].ToString();
        DestfromName = dr["DestfromName"].ToString();
        To = dr["To"].ToString();
        DesttoName = dr["DesttoName"].ToString();
        Airline_Name = dr["Airline_Name"].ToString();
        Airline_Code = dr["Airline_Code"].ToString();
        Class = dr["Class"].ToString();
        ClassType = dr["ClassType"].ToString();
        BaseFare = string.IsNullOrEmpty(dr["BaseFare"].ToString()) ? "0.00" : Convert.ToDouble(dr["BaseFare"]).ToString("f2");
        Tax = string.IsNullOrEmpty(dr["Tax"].ToString()) ? "0.00" : Convert.ToDouble(dr["Tax"]).ToString("f2");
        Total = string.IsNullOrEmpty(dr["Total"].ToString()) ? "0.00" : Convert.ToDouble(dr["Total"]).ToString("f2");
        FilledBy = dr["FilledBy"].ToString();
        FillDate = string.IsNullOrEmpty(dr["FillDate"].ToString()) ? "" : Convert.ToDateTime(dr["FillDate"]).ToString("dd MMM yy HH:mm");
        //Directflt = dr["Directflt"].ToString();
        Travel_DateStart = string.IsNullOrEmpty(dr["Travel_DateStart"].ToString()) ? "" : Convert.ToDateTime(dr["Travel_DateStart"]).ToString("dd MMM yy");
        Travel_DateEnd = string.IsNullOrEmpty(dr["Travel_DateEnd"].ToString()) ? "" : Convert.ToDateTime(dr["Travel_DateEnd"]).ToString("dd MMM yy");
        //SplRequest = dr["SplRequest"].ToString();
        ExpOffers_Date = string.IsNullOrEmpty(dr["ExpOffers_Date"].ToString()) ? "" : Convert.ToDateTime(dr["ExpOffers_Date"]).ToString("dd MMM yy");
        //Country = dr["Country"].ToString();
        //Website = dr["Website"].ToString();
        //Luggage = dr["Luggage"].ToString();
        //Rules = dr["Rules"].ToString();
        //Comm = dr["Comm"].ToString();
        //Country_Code = dr["Country_Code"].ToString();
        //Continent_Name = dr["Continent_Name"].ToString();
        //Continent_Code = dr["Continent_Name"].ToString();
        //Keyword = dr["Keyword"].ToString();
        //LastModifiedDate = string.IsNullOrEmpty(dr["LastModifiedDate"].ToString()) ? "" : Convert.ToDateTime(dr["LastModifiedDate"]).ToString("dd MMM yy HH:mm");
        //ShowOffer = dr["ShowOffer"].ToString();
        //AvailSeats = dr["AvailSeats"].ToString();
    }
    public string FaredetailId { set; get; }
    public string From { set; get; }
    public string DestfromName { set; get; }
    public string To { set; get; }
    public string DesttoName { set; get; }
    public string Airline_Name { set; get; }
    public string Airline_Code { set; get; }
    public string Class { set; get; }
    public string ClassType { set; get; }
    public string BaseFare { set; get; }
    public string Tax { set; get; }
    public string Total { set; get; }
    public string FilledBy { set; get; }
    public string FillDate { set; get; }
    //public string Directflt { set; get; }
    public string Travel_DateStart { set; get; }
    public string Travel_DateEnd { set; get; }
    //public string SplRequest { set; get; }
    public string ExpOffers_Date { set; get; }
    //public string Country { set; get; }
    //public string Website { set; get; }
    //public string Luggage { set; get; }
    //public string Rules { set; get; }
    //public string Comm { set; get; }
    //public string Country_Code { set; get; }
    //public string Continent_Name { set; get; }
    //public string Continent_Code { set; get; }
    //public string Keyword { set; get; }
    //public string LastModifiedDate { set; get; }
    //public string ShowOffer { set; get; }
    //public string AvailSeats { set; get; }
}
public class PageDestination
{
    public PageDestination(DataRow dr)
    {
        SrNo = dr["SrNo"].ToString();
        Dest = dr["Dest"].ToString();
        Airport = dr["Airport"].ToString();
        CClass = dr["CClass"].ToString();
        Page = dr["Page"].ToString();
        Company = dr["Company"].ToString();
        ModifyBy = dr["ModifyBy"].ToString();
        ModifyDate = string.IsNullOrEmpty(dr["ModifyDate"].ToString()) ? "" : Convert.ToDateTime(dr["ModifyDate"]).ToString("dd MMM yy HH:mm");
    }
    public string SrNo { set; get; }
    public string Dest { set; get; }
    public string Airport { set; get; }
    public string CClass { set; get; }
    public string Page { set; get; }
    public string Company { set; get; }
    public string ModifyBy { set; get; }
    public string ModifyDate { set; get; }
}

public class AddGroup
{

    public string BindData(string Company, string CampDetails)
    {
        string _Queary = string.Empty;
        DataTable dt = new DataTable();
        string _JsonDtPrice = string.Empty;
        if (Company != "" && CampDetails == "")
        {
            _Queary = "select id,Company,Campaign,GroupWiseCode,AirportCode from GroupCodes where Company='" + Company + "'";
        }
        else if (CampDetails != "" && Company != "")
        {
            _Queary = "select top 10 id,Company,Campaign,GroupWiseCode,AirportCode from GroupCodes where Company='" + Company + "' and Campaign='" + CampDetails + "'";
        }
        else
        {
            _Queary = "select top 10 id,Company,Campaign,GroupWiseCode,AirportCode from GroupCodes";
        }
        using (SqlConnection conection = DataConnection.GetConnectionMarkup())
        {
            dt = SqlHelper.ExecuteDataset(conection, CommandType.Text, _Queary).Tables[0];
            _JsonDtPrice = JsonConvert.SerializeObject(dt);
        }
        return _JsonDtPrice;
    }
    public string DeleteData(int _ID)
    {
        try
        {
            using (SqlConnection conection = DataConnection.GetConnectionMarkup())
            {
                SqlHelper.ExecuteNonQuery(conection, CommandType.Text, "delete GroupCodes where id='" + _ID + "'");
            }
            return "_YES";
        }
        catch (Exception ex)
        {
            return "_NO";
        }

    }
}

