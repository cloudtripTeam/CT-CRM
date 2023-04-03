using System;
using System.Data;


/// <summary>
/// Summary description for CreateDataTable
/// </summary>
public class CreateDataTable
{
    public CreateDataTable()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region Create Suppliment charges DataTable

    public DataTable CreateAmountChargesDataTable()
    {
        DataTable dt = new DataTable();
        dt.TableName = "AmountChargesTypes";
        dt.Columns.Add("BOK_MST_Booking_ID", typeof(String));
        dt.Columns.Add("BOK_DTL_Prod_Booking_ID", typeof(String));
        dt.Columns.Add("AMT_CHG_MST_Charge_ID", typeof(String));
        dt.Columns.Add("AMT_CHG_DTL_Charges_For", typeof(String));
        dt.Columns.Add("AMT_CHG_DTL_Cost_Price", typeof(double));
        dt.Columns.Add("AMT_CHG_DTL_Sell_Price", typeof(double));
        dt.Columns.Add("AMT_CHG_DTL_Charges_Status", typeof(String));
        dt.Columns.Add("AMT_CHG_DTL_Supplier_ID", typeof(String));
        dt.Columns.Add("AMT_CHG_DTL_Charges_Remarks", typeof(String));
        dt.Columns.Add("AMT_CHG_DTL_Charges_Date", typeof(DateTime));
        dt.Columns.Add("AMT_CHG_DTL_ModifiedBy", typeof(String));
        dt.Columns.Add("AMT_CHG_DTL_ModifiedDate", typeof(DateTime));
        return dt;
    }

    public DataRow CreateAmountChargesDtaRow(DataRow dr, string BookingID, string ProdID, string Charge_ID, string Charges_For, 
        double Cost_Price, double Sell_Price, string Charges_Status, string Supplier_ID, string Charges_Remarks, 
        string Charges_Date, string ModifiedBy, string ModifiedDate)
    {

        dr["BOK_MST_Booking_ID"] = BookingID;
        dr["BOK_DTL_Prod_Booking_ID"] = ProdID;
        dr["AMT_CHG_MST_Charge_ID"] = Charge_ID;
        dr["AMT_CHG_DTL_Charges_For"] = Charges_For;
        dr["AMT_CHG_DTL_Cost_Price"] = Cost_Price;
        dr["AMT_CHG_DTL_Sell_Price"] = Sell_Price;
        dr["AMT_CHG_DTL_Charges_Status"] = Charges_Status;
        dr["AMT_CHG_DTL_Supplier_ID"] = Supplier_ID;
        dr["AMT_CHG_DTL_Charges_Remarks"] = Charges_Remarks;
        dr["AMT_CHG_DTL_Charges_Date"] = Charges_Date == "" ? DateTime.Now : Convert.ToDateTime(Charges_Date);
        dr["AMT_CHG_DTL_ModifiedBy"] = ModifiedBy;
        dr["AMT_CHG_DTL_ModifiedDate"] = ModifiedDate == "" ? DateTime.Now : Convert.ToDateTime(ModifiedDate);
        return dr;
    }

    #endregion
    
    #region Create PaxDetail DataTable

    public DataTable CreatePaxDataTable()
    {
        DataTable dt = new DataTable();
        dt.TableName = "PassengersTypes";
        dt.Columns.Add("BOK_MST_Booking_ID", typeof(String));
        dt.Columns.Add("BOK_DTL_Prod_Booking_ID", typeof(String));
        dt.Columns.Add("PAX_DTL_Pax_ID", typeof(String));
        dt.Columns.Add("PAX_DTL_Title", typeof(String));
        dt.Columns.Add("PAX_DTL_Pax_First_Name", typeof(String));
        dt.Columns.Add("PAX_DTL_Pax_Middle_Name", typeof(String));
        dt.Columns.Add("PAX_DTL_Pax_Last_Name", typeof(String));
        dt.Columns.Add("PAX_DTL_Frequent_Flyer_No", typeof(String));
        dt.Columns.Add("PAX_DTL_Passport_No", typeof(String));
        dt.Columns.Add("PAX_DTL_Nationality", typeof(String));
        dt.Columns.Add("PAX_DTL_Expiry_Date", typeof(DateTime));
        dt.Columns.Add("PAX_DTL_Place_of_Issue", typeof(String));
        dt.Columns.Add("PAX_DTL_Place_of_Birth", typeof(String));
        dt.Columns.Add("PAX_DTL_Pax_DOB", typeof(DateTime));
        dt.Columns.Add("PAX_DTL_Pax_Type", typeof(String));
        dt.Columns.Add("PAX_DTL_Pax_Sex", typeof(String));
        dt.Columns.Add("PAX_DTL_ModifiedBy", typeof(String));
        dt.Columns.Add("PAX_DTL_ModifiedDate", typeof(DateTime));
        return dt;
    }

    public DataRow CreatePaxDtaRow(DataRow dr, string BookingID, string ProdID, string Pax_ID, string Title,
        string Pax_First_Name, string Pax_Middle_Name, string Pax_Last_Name, string Frequent_Flyer_No, string Passport_No,
        string Nationality, string Expiry_Date, string Place_of_Issue, string Place_of_Birth, string Pax_DOB, string Pax_Type,
        string Pax_Sex, string ModifiedBy, string ModifiedDate)
    {
        dr["BOK_MST_Booking_ID"] = BookingID;
        dr["BOK_DTL_Prod_Booking_ID"] = ProdID;
        dr["PAX_DTL_Pax_ID"] = Pax_ID;
        dr["PAX_DTL_Title"] = Title;
        dr["PAX_DTL_Pax_First_Name"] = Pax_First_Name;
        dr["PAX_DTL_Pax_Middle_Name"] = Pax_Middle_Name;
        dr["PAX_DTL_Pax_Last_Name"] = Pax_Last_Name;
        dr["PAX_DTL_Frequent_Flyer_No"] = Frequent_Flyer_No;
        dr["PAX_DTL_Passport_No"] = Passport_No;
        dr["PAX_DTL_Nationality"] = Nationality;
        dr["PAX_DTL_Expiry_Date"] = Expiry_Date == "" ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(Expiry_Date);
        dr["PAX_DTL_Place_of_Issue"] = Place_of_Issue;
        dr["PAX_DTL_Place_of_Birth"] = Place_of_Birth;
        dr["PAX_DTL_Pax_DOB"] = Pax_DOB == "" ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(Pax_DOB);
        dr["PAX_DTL_Pax_Type"] = Pax_Type;
        dr["PAX_DTL_Pax_Sex"] = Pax_Sex;
        dr["PAX_DTL_ModifiedBy"] = ModifiedBy;
        dr["PAX_DTL_ModifiedDate"] = ModifiedDate == "" ? DateTime.Now : Convert.ToDateTime(ModifiedDate);
        return dr;
    }

    #endregion

    #region Create SectorDetail DataTable

    public DataTable CreateSectorDataTable()
    {
        DataTable dt = new DataTable();
        dt.TableName = "SectorTypes";
        dt.Columns.Add("BOK_MST_Booking_ID", typeof(String));
        dt.Columns.Add("BOK_DTL_Prod_Booking_ID", typeof(String));
        dt.Columns.Add("SEC_DTL_Carier_Name", typeof(String));
        dt.Columns.Add("SEC_DTL_From_Destination", typeof(String));
        dt.Columns.Add("SEC_DTL_From_Date_Time", typeof(DateTime));
        dt.Columns.Add("SEC_DTL_To_Destination", typeof(String));
        dt.Columns.Add("SEC_DTL_To_Date_Time", typeof(DateTime));
        dt.Columns.Add("SEC_DTL_Flight_No", typeof(String));
        dt.Columns.Add("SEC_DTL_Class", typeof(String));
        dt.Columns.Add("SEC_DTL_Status", typeof(String));
        dt.Columns.Add("SEC_DTL_Fare_Basis", typeof(String));
        dt.Columns.Add("SEC_DTL_Not_Valid_Befor", typeof(String));
        dt.Columns.Add("SEC_DTL_Not_Valid_After", typeof(String));
        dt.Columns.Add("SEC_DTL_Baggage_Allownce", typeof(String));
        dt.Columns.Add("SEC_DTL_Airport_Terminal_From", typeof(String));
        dt.Columns.Add("SEC_DTL_Airport_Terminal_To", typeof(String));
        dt.Columns.Add("SEC_DTL_Seg_ID", typeof(String));
        dt.Columns.Add("SEC_DTL_Seg_Remarks", typeof(String));
        dt.Columns.Add("SEC_DTL_TripID", typeof(String));
        dt.Columns.Add("SEC_DTL_ModifiedBy", typeof(String));
        dt.Columns.Add("SEC_DTL_ModifiedDate", typeof(DateTime));
        return dt;
    }

    public DataRow CreateSectorDtaRow(DataRow dr, string BookingID, string ProdID, string Carier_Name, string From_Destination, 
        string From_Date_Time, string To_Destination, string To_Date_Time, string Flight_No, string fClass, string Status, 
        string Fare_Basis, string Not_Valid_Befor, string Not_Valid_After, string Baggage_Allownce, string Airport_Terminal_From, 
        string Airport_Terminal_To,string Seg_ID, string Seg_Remarks, string TripID, string ModifiedBy, string ModifiedDate)
    {
        dr["BOK_MST_Booking_ID"] = BookingID;
        dr["BOK_DTL_Prod_Booking_ID"] = ProdID;
        dr["SEC_DTL_Carier_Name"] = Carier_Name;
        dr["SEC_DTL_From_Destination"] = From_Destination;
        dr["SEC_DTL_From_Date_Time"] = From_Date_Time == "" ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(From_Date_Time);
        dr["SEC_DTL_To_Destination"] = To_Destination;
        dr["SEC_DTL_To_Date_Time"] = To_Date_Time == "" ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(To_Date_Time);
        dr["SEC_DTL_Flight_No"] = Flight_No;
        dr["SEC_DTL_Class"] = fClass;
        dr["SEC_DTL_Status"] = Status;
        dr["SEC_DTL_Fare_Basis"] = Fare_Basis;
        dr["SEC_DTL_Not_Valid_Befor"] = Not_Valid_Befor;
        dr["SEC_DTL_Not_Valid_After"] = Not_Valid_After;
        dr["SEC_DTL_Baggage_Allownce"] = Baggage_Allownce;
        dr["SEC_DTL_Airport_Terminal_From"] = Airport_Terminal_From;
        dr["SEC_DTL_Airport_Terminal_To"] = Airport_Terminal_To;
        dr["SEC_DTL_Seg_ID"] = Seg_ID;
        dr["SEC_DTL_Seg_Remarks"] = Seg_Remarks;
        dr["SEC_DTL_TripID"] = TripID;
        dr["SEC_DTL_ModifiedBy"] = ModifiedBy;
        dr["SEC_DTL_ModifiedDate"] = ModifiedDate == "" ? DateTime.Now : Convert.ToDateTime(ModifiedDate);
        return dr;
    }

    #endregion

    #region Dump Fare Table

    public DataTable CreateDumpFareTable()
    {
        DataTable dt = new DataTable();

        try
        {
            dt.TableName = "DumpFares";

            dt.Columns.Add("From", typeof(string));
            dt.Columns.Add("DestfromName", typeof(string));
            dt.Columns.Add("To", typeof(string));
            dt.Columns.Add("DesttoName", typeof(string));

            dt.Columns.Add("Travel_DateStart", typeof(DateTime));
            dt.Columns.Add("Travel_DateEnd", typeof(DateTime));

            dt.Columns.Add("Airline_Name", typeof(string));
            dt.Columns.Add("Airline_Code", typeof(string));

            dt.Columns.Add("Class", typeof(String));
            dt.Columns.Add("ClassType", typeof(String));

            dt.Columns.Add("BaseFare", typeof(double));
            dt.Columns.Add("Tax", typeof(double));
            dt.Columns.Add("Total", typeof(double));

            dt.Columns.Add("FilledBy", typeof(string));
            dt.Columns.Add("FillDate", typeof(DateTime));
            dt.Columns.Add("Directflt", typeof(string));
            dt.Columns.Add("Country", typeof(string));
            dt.Columns.Add("Country_Code", typeof(string));
            dt.Columns.Add("Continent_Name", typeof(string));
            dt.Columns.Add("Continent_Code", typeof(string));
            dt.Columns.Add("Markup", typeof(double));
            dt.Columns.Add("Provider", typeof(string));

            dt.Columns.Add("AvailSeats", typeof(int));
            dt.Columns.Add("ExpOffers_Date", typeof(DateTime));
            dt.Columns.Add("Currency", typeof(string));
        }
        catch (Exception ex)
        {
           Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Console.WriteLine(ex.Message + ex.StackTrace);
            dt = null;
        }
        return dt;

    }
    #endregion

    #region Restricted Airlines
    public DataTable RestrictedAirlinesTable()
    {
        DataTable dt = new DataTable();

        try
        {
            dt.TableName = "RestrictedAirlines";

            dt.Columns.Add("AL_Airline_Code", typeof(string));
            dt.Columns.Add("AL_Comp_Code", typeof(string));
            dt.Columns.Add("AL_Destination", typeof(string));
            dt.Columns.Add("AL_RES_Type", typeof(string));
            dt.Columns.Add("AL_ModifiedBy", typeof(string)); 
            dt.Columns.Add("AL_Compaign_Code", typeof(string));
          
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Console.WriteLine(ex.Message + ex.StackTrace);
            dt = null;
        }
        return dt;

    }

    public DataRow RestrictedAirlinesRow(DataRow dr, string airlineCode, string CompanyCode, string CampaignCode,
        string destination,string ResType,string ModifyBy)
    {
      

        try
        {         

            dr["AL_Airline_Code"] = airlineCode.Trim().ToUpper();
            dr["AL_Comp_Code"] = CompanyCode.Trim().ToUpper();
            dr["AL_Destination"] = destination.Trim().ToUpper();
            dr["AL_RES_Type"] = ResType.Trim().ToUpper();
            dr["AL_ModifiedBy"] = ModifyBy.Trim().ToUpper();
            dr["AL_Compaign_Code"] = CampaignCode.Trim().ToUpper();

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Console.WriteLine(ex.Message + ex.StackTrace);
            dr = null;
        }
        return dr;

    }
    #endregion

    #region Blackout Return Date
    public DataTable BlackoutReturnTable()
    {
        DataTable dt = new DataTable();

        try
        {
            dt.TableName = "BackoutReturn";

            dt.Columns.Add("BLK_OUT_RT_AirlineCode", typeof(string));
            dt.Columns.Add("BLK_LST_Destination", typeof(string));
            dt.Columns.Add("BLK_OUT_RT_FromDate", typeof(DateTime));
            dt.Columns.Add("BLK_OUT_RT_ToDate", typeof(DateTime));
            dt.Columns.Add("BLK_OUT_RT_ModifyBy", typeof(string));
           

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Console.WriteLine(ex.Message + ex.StackTrace);
            dt = null;
        }
        return dt;

    }

    public DataRow BlackoutReturnRow(DataRow dr, string airlineCode, string destination, DateTime fromDate,
        DateTime todate,  string ModifyBy)
    {


        try
        {

            dr["BLK_OUT_RT_AirlineCode"] = airlineCode.Trim().ToUpper();
            dr["BLK_LST_Destination"] = destination.Trim().ToUpper();
            dr["BLK_OUT_RT_FromDate"] = fromDate;
            dr["BLK_OUT_RT_ToDate"] = todate;
            dr["BLK_OUT_RT_ModifyBy"] = ModifyBy.Trim().ToUpper();
            

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Console.WriteLine(ex.Message + ex.StackTrace);
            dr = null;
        }
        return dr;

    }
    #endregion



}

