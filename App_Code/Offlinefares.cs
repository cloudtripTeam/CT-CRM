using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for PromaCode
/// </summary>
public class Offlinefares
{
    public Offlinefares()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region SeasonMaster
    public bool AddSeason(string SeasonName, string StartDate, string EndDate, string Duration, string Product, string NoOfPax, string modifyBy, string company)
    {
        string _CommandText = string.Empty;
        SqlParameter[] par = new SqlParameter[9];
        bool addseason = false;
        try
        {
            _CommandText = "SeasonMasterDetail";
            if (!string.IsNullOrEmpty(SeasonName))
            {
                par[0] = new SqlParameter("@paramSeasonName", SqlDbType.VarChar, 500);
                par[0].Value = SeasonName;
            }
            if (!string.IsNullOrEmpty(StartDate))
            {
                par[1] = new SqlParameter("@paramStartDate", SqlDbType.DateTime);
                par[1].Value = Convert.ToDateTime(StartDate);
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                par[2] = new SqlParameter("@paramEndDate", SqlDbType.DateTime);
                par[2].Value = Convert.ToDateTime(EndDate);
            }
            if (!string.IsNullOrEmpty(Duration))
            {
                par[3] = new SqlParameter("@paramDuration", SqlDbType.Int);
                par[3].Value = Convert.ToInt32(Duration);
            }
            if (!string.IsNullOrEmpty(Product))
            {
                par[4] = new SqlParameter("@paramProduct", SqlDbType.VarChar, 500);
                par[4].Value = Product;
            }
            if (!string.IsNullOrEmpty(NoOfPax))
            {
                par[5] = new SqlParameter("@paramNoOfPax", SqlDbType.Int);
                par[5].Value = NoOfPax;
            }
            if (!string.IsNullOrEmpty(modifyBy))
            {
                par[6] = new SqlParameter("@paramModifyBy", SqlDbType.VarChar);
                par[6].Value = modifyBy;
            }
            if (!string.IsNullOrEmpty(company))
            {
                par[7] = new SqlParameter("@paramCompanyCode", SqlDbType.VarChar);
                par[7].Value = company;
            }
            par[8] = new SqlParameter("@Counter", SqlDbType.Int);
            par[8].Value = 2;
            int id = SqlHelper.ExecuteNonQuery(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, par);
            if (id > 0)
            {
                addseason = true;
            }
            return addseason;
        }
        catch (Exception ex)
        {
            addseason = false;
            return addseason;
        }
    }
    public DataTable SearchSeasonDetail(string SeasonName, string StartDate, string EndDate, string Duration, string Product, string NoOfPax, string company)
    {
        DataSet dsSeasonDetail = new DataSet();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[8];
        try
        {
            _CommandText = "SeasonMasterDetail";
            if (!string.IsNullOrEmpty(SeasonName))
            {
                param[0] = new SqlParameter("@paramSeasonName", SqlDbType.VarChar, 500);
                param[0].Value = SeasonName;
            }
            if (!string.IsNullOrEmpty(StartDate))
            {
                param[1] = new SqlParameter("@paramStartDate", SqlDbType.DateTime);
                param[1].Value = Convert.ToDateTime(StartDate);
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                param[2] = new SqlParameter("@paramEndDate", SqlDbType.DateTime);
                param[2].Value = Convert.ToDateTime(EndDate);
            }
            if (!string.IsNullOrEmpty(Duration))
            {
                param[3] = new SqlParameter("@paramDuration", SqlDbType.Int);
                param[3].Value = Convert.ToInt16(Duration);
            }
            if (!string.IsNullOrEmpty(Product))
            {
                param[4] = new SqlParameter("@paramProduct", SqlDbType.VarChar, 500);
                param[4].Value = Product;
            }
            if (!string.IsNullOrEmpty(NoOfPax))
            {
                param[5] = new SqlParameter("@paramNoOfPax", SqlDbType.Int);
                param[5].Value = Convert.ToInt16(NoOfPax);
            }
            if (!string.IsNullOrEmpty(company))
            {
                param[6] = new SqlParameter("@paramCompanyCode", SqlDbType.VarChar);
                param[6].Value = company;
            }
            param[7] = new SqlParameter("@Counter", SqlDbType.Int);
            param[7].Value = 5;
            dsSeasonDetail = SqlHelper.ExecuteDataset(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            return dsSeasonDetail.Tables[0];
        }

        catch (Exception ex)
        {
            dsSeasonDetail = null;
            return dsSeasonDetail.Tables[0];
        }
    }
    public DataTable GetSeasonDetail(string date)
    {
        DataSet ds = new DataSet();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            _CommandText = "SeasonMasterDetail";
            if (!string.IsNullOrEmpty(date))
            {
                param[0] = new SqlParameter("@paramModify_Date", SqlDbType.DateTime);
                param[0].Value = Convert.ToDateTime(date);
            }
            param[1] = new SqlParameter("@Counter", SqlDbType.Int);
            param[1].Value = 1;
            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            return ds.Tables[0];
        }

        catch
        {
            ds = null;
            return ds.Tables[0];
        }
    }


    public bool UpdateSeasonDetail(int SeasonID, string SeasonName, string StartDate, string EndDate, int Duration, string Product, int NoOfPax, string modifyBy, string company, string status)
    {
        string _CommandText = string.Empty;
        SqlParameter[] par = new SqlParameter[11];
        bool updateproma = false;

        try
        {
            _CommandText = "SeasonMasterDetail";
            par[0] = new SqlParameter("@paramSeasonName", SqlDbType.VarChar, 500);
            par[0].Value = SeasonName;
            par[1] = new SqlParameter("@paramStartDate", SqlDbType.DateTime);
            par[1].Value = Convert.ToDateTime(StartDate);
            par[2] = new SqlParameter("@paramEndDate", SqlDbType.DateTime);
            par[2].Value = Convert.ToDateTime(EndDate); ;
            par[3] = new SqlParameter("@paramDuration", SqlDbType.Int);
            par[3].Value = Convert.ToInt32(Duration);
            par[4] = new SqlParameter("@paramProduct", SqlDbType.VarChar, 500);
            par[4].Value = Product;
            par[5] = new SqlParameter("@Counter", SqlDbType.Int);
            par[5].Value = 4;
            par[6] = new SqlParameter("@paramSeasonID", SqlDbType.Int);
            par[6].Value = SeasonID;
            par[7] = new SqlParameter("@paramNoOfPax", SqlDbType.Int);
            par[7].Value = NoOfPax;
            if (!string.IsNullOrEmpty(modifyBy))
            {
                par[8] = new SqlParameter("@paramModifyBy", SqlDbType.VarChar);
                par[8].Value = modifyBy;
            }
            if (!string.IsNullOrEmpty(company))
            {
                par[9] = new SqlParameter("@paramCompanyCode", SqlDbType.VarChar);
                par[9].Value = company;
            }
            if (!string.IsNullOrEmpty(status))
            {
                par[10] = new SqlParameter("@paramStatus", SqlDbType.Bit);
                par[10].Value = Convert.ToInt16(status);
            }
            int id = SqlHelper.ExecuteNonQuery(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, par);
            if (id > 0)
            {
                updateproma = true;

            }
            return updateproma;
        }
        catch (Exception ex)
        {
            updateproma = false;
            return updateproma;
        }

    }
    public bool DeleteSeason(int SeasonIDs)
    {
        string _CommandText = "SeasonMasterDetail";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            param[0] = new SqlParameter("@paramSeasonID", SqlDbType.Int);
            param[0].Value = SeasonIDs;


            param[1] = new SqlParameter("@Counter", SqlDbType.Int);
            param[1].Value = 3;

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            if (count > 0)
                return true;
            else
                return false;

        }
        catch { return false; }
    }

    #endregion
    #region HotelDestination

    public DataTable GetHotelDestination(string date)
    {

        DataSet ds = new DataSet();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            _CommandText = "HotelDestinationDetail";
            if (!string.IsNullOrEmpty(date))
            {
                param[0] = new SqlParameter("@paramModify_Date", SqlDbType.DateTime);
                param[0].Value = Convert.ToDateTime(date);
            }
            param[1] = new SqlParameter("@Counter", SqlDbType.Int);
            param[1].Value = 1;
            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            return ds.Tables[0];
        }

        catch (Exception ex)
        {
            ds = null;
            return ds.Tables[0];
        }
    }
    public DataTable SearchHotelDestination(string Destination, string Markup, string SeasonID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[4];
        try
        {
            _CommandText = "HotelDestinationDetail";
            if (!string.IsNullOrEmpty(Destination))
            {
                param[0] = new SqlParameter("@paramDestination", SqlDbType.VarChar, 500);
                param[0].Value = Destination;
            }
            if (!String.IsNullOrEmpty(Markup))
            {
                param[1] = new SqlParameter("@paramMarkup", SqlDbType.Money);
                param[1].Value = Convert.ToDouble(Markup);
            }
            if (!String.IsNullOrEmpty(SeasonID))
            {
                param[2] = new SqlParameter("@paramSeasonID", SqlDbType.Int);
                param[2].Value = Convert.ToInt64(SeasonID);
            }
            param[3] = new SqlParameter("@Counter", SqlDbType.Int);
            param[3].Value = 5;
            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        catch
        {
            dt = null;
            return dt;
        }
    }
    public bool SaveHotelDestination(string Destination, string Markup, string SeasonID, string modifyBy)
    {
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[5];
        try
        {
            _CommandText = "HotelDestinationDetail";
            if (!string.IsNullOrEmpty(Destination))
            {
                param[0] = new SqlParameter("@paramDestination", SqlDbType.VarChar, 500);
                param[0].Value = Destination;
            }
            if (!String.IsNullOrEmpty(Markup))
            {
                param[1] = new SqlParameter("@paramMarkup", SqlDbType.Money);
                param[1].Value = Convert.ToDouble(Markup);
            }
            if (!String.IsNullOrEmpty(SeasonID))
            {
                param[2] = new SqlParameter("@paramSeasonID", SqlDbType.Int);
                param[2].Value = Convert.ToInt64(SeasonID);
            }
            if (!string.IsNullOrEmpty(modifyBy))
            {
                param[3] = new SqlParameter("@paramModifyBy", SqlDbType.VarChar);
                param[3].Value = modifyBy;
            }
            param[4] = new SqlParameter("@Counter", SqlDbType.Int);
            param[4].Value = 2;
            int id = SqlHelper.ExecuteNonQuery(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            if (id > 0)
            {
                return true;
            }
            else
            { return false; }

        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public bool UpdateHotelDestination(string Destination, string Markup, string SeasonID, string modifyBy, string DestinationID)
    {
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[6];
        try
        {
            _CommandText = "HotelDestinationDetail";
            if (!string.IsNullOrEmpty(Destination))
            {
                param[0] = new SqlParameter("@paramDestination", SqlDbType.VarChar, 500);
                param[0].Value = Destination;
            }
            if (!String.IsNullOrEmpty(Markup))
            {
                param[1] = new SqlParameter("@paramMarkup", SqlDbType.Money);
                param[1].Value = Convert.ToDouble(Markup);
            }
            if (!String.IsNullOrEmpty(SeasonID))
            {
                param[2] = new SqlParameter("@paramSeasonID", SqlDbType.Int);
                param[2].Value = Convert.ToInt64(SeasonID);
            }
            if (!string.IsNullOrEmpty(modifyBy))
            {
                param[3] = new SqlParameter("@paramModifyBy", SqlDbType.VarChar);
                param[3].Value = modifyBy;
            }
            if (!string.IsNullOrEmpty(DestinationID))
            {
                param[4] = new SqlParameter("@paramDestinationID", SqlDbType.Int);
                param[4].Value = Convert.ToInt64(DestinationID);
            }
            param[5] = new SqlParameter("@Counter", SqlDbType.Int);
            param[5].Value = 4;
            int id = SqlHelper.ExecuteNonQuery(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            if (id > 0)
            {
                return true;
            }
            else
            { return false; }

        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public bool DeleteHotelDestination(int DestinationID)
    {
        string _CommandText = "HotelDestinationDetail";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            param[0] = new SqlParameter("@paramDestinationID", SqlDbType.Int);
            param[0].Value = DestinationID;


            param[1] = new SqlParameter("@Counter", SqlDbType.Int);
            param[1].Value = 3;

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            if (count > 0)
                return true;
            else
                return false;

        }
        catch { return false; }
    }
    #endregion
    #region FlightDestination
    public DataTable GetFlightDestination(string date)
    {

        DataSet ds = new DataSet();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            _CommandText = "FlightDestinationDetail";
            if (!string.IsNullOrEmpty(date))
            {
                param[0] = new SqlParameter("@paramModify_Date", SqlDbType.DateTime);
                param[0].Value = Convert.ToDateTime(date);
            }
            param[1] = new SqlParameter("@Counter", SqlDbType.Int);
            param[1].Value = "1";
            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            return ds.Tables[0];
        }

        catch (Exception ex)
        {
            ds = null;
            return ds.Tables[0];
        }
    }
    public DataTable SearchFlightDestination(string DestinationFrom, string DestinationTo, string Markup, string SeasonID, string AirlineCode, string PageID, string provider)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[8];
        try
        {
            _CommandText = "FlightDestinationDetail";
            if (!string.IsNullOrEmpty(DestinationFrom))
            {
                param[0] = new SqlParameter("@paramDestinationFrom", SqlDbType.VarChar, 500);
                param[0].Value = DestinationFrom;
            }
            if (!String.IsNullOrEmpty(DestinationTo))
            {
                param[1] = new SqlParameter("@paramDestinationTo", SqlDbType.VarChar, 500);
                param[1].Value = DestinationTo;
            }
            if (!String.IsNullOrEmpty(Markup))
            {
                param[2] = new SqlParameter("@paramMarkup", SqlDbType.Money);
                param[2].Value = Convert.ToDouble(Markup);
            }
            if (!String.IsNullOrEmpty(SeasonID))
            {
                param[3] = new SqlParameter("@paramSeasonID", SqlDbType.Int);
                param[3].Value = Convert.ToInt64(SeasonID);
            }
            if (!String.IsNullOrEmpty(AirlineCode))
            {
                param[4] = new SqlParameter("@paramAirlineCode", SqlDbType.VarChar, 50);
                param[4].Value = AirlineCode;
            }
            if (!string.IsNullOrEmpty(PageID))
            {
                param[5] = new SqlParameter("@paramPageID", SqlDbType.VarChar);
                param[5].Value = PageID;
            }
            if (!string.IsNullOrEmpty(provider))
            {
                param[6] = new SqlParameter("@paramProvider", SqlDbType.VarChar);
                param[6].Value = provider;
            }
            param[7] = new SqlParameter("@Counter", SqlDbType.Int);
            param[7].Value = 5;
            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        catch (Exception ex)
        {
            dt = null;
            return dt;
        }
    }
    public bool SaveFlightDestination(string DestinationFrom, string DestinationTo, string Markup, string SeasonID, string AirlineCode, string modifyBy, string PageID, string provider)
    {
        string _CommandText = string.Empty;
        SqlParameter[] par = new SqlParameter[9];
        try
        {
            _CommandText = "FlightDestinationDetail";
            if (!string.IsNullOrEmpty(DestinationFrom))
            {
                par[0] = new SqlParameter("@paramDestinationFrom", SqlDbType.VarChar, 500);
                par[0].Value = DestinationFrom;
            }
            if (!string.IsNullOrEmpty(DestinationTo))
            {
                par[1] = new SqlParameter("@paramDestinationTo", SqlDbType.VarChar, 500);
                par[1].Value = DestinationTo;
            }
            if (!string.IsNullOrEmpty(Markup))
            {
                par[2] = new SqlParameter("@paramMarkup", SqlDbType.Money);
                par[2].Value = Convert.ToDouble(Markup);
            }

            if (!string.IsNullOrEmpty(SeasonID))
            {
                par[3] = new SqlParameter("@paramSeasonID", SqlDbType.Int);
                par[3].Value = Convert.ToInt32(SeasonID);
            }
            if (!string.IsNullOrEmpty(AirlineCode))
            {
                par[4] = new SqlParameter("@paramAirlineCode", SqlDbType.VarChar);
                par[4].Value = AirlineCode;
            }
            if (!string.IsNullOrEmpty(modifyBy))
            {
                par[5] = new SqlParameter("@paramModifyBy", SqlDbType.VarChar);
                par[5].Value = modifyBy;
            }
            if (!string.IsNullOrEmpty(PageID))
            {
                par[6] = new SqlParameter("@paramPageID", SqlDbType.VarChar);
                par[6].Value = PageID;
            }
            if (!string.IsNullOrEmpty(provider))
            {
                par[7] = new SqlParameter("@paramProvider", SqlDbType.VarChar);
                par[7].Value = provider;
            }
            par[8] = new SqlParameter("@Counter", SqlDbType.Int);
            par[8].Value = 2;
            int id = SqlHelper.ExecuteNonQuery(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, par);
            if (id > 0)
            {
                return true;
            }
            else
            { return false; }

        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public bool UpdateFlightDestination(string DestinationFrom, string DestinationTo, string Markup, string SeasonID, string AirlineCode, string modifyBy, string Destination_ID, string PageID, string provider)
    {
        string _CommandText = string.Empty;
        SqlParameter[] par = new SqlParameter[10];
        try
        {
            _CommandText = "FlightDestinationDetail";
            if (!string.IsNullOrEmpty(DestinationFrom))
            {
                par[0] = new SqlParameter("@paramDestinationFrom", SqlDbType.VarChar, 500);
                par[0].Value = DestinationFrom;
            }
            if (!string.IsNullOrEmpty(DestinationTo))
            {
                par[1] = new SqlParameter("@paramDestinationTo", SqlDbType.VarChar, 500);
                par[1].Value = DestinationTo;
            }
            if (!string.IsNullOrEmpty(Markup))
            {
                par[2] = new SqlParameter("@paramMarkup", SqlDbType.Money);
                par[2].Value = Convert.ToDouble(Markup);
            }

            if (!string.IsNullOrEmpty(SeasonID))
            {
                par[3] = new SqlParameter("@paramSeasonID", SqlDbType.Int);
                par[3].Value = Convert.ToInt32(SeasonID);
            }
            if (!string.IsNullOrEmpty(AirlineCode))
            {
                par[4] = new SqlParameter("@paramAirlineCode", SqlDbType.VarChar);
                par[4].Value = AirlineCode;
            }
            if (!string.IsNullOrEmpty(modifyBy))
            {
                par[5] = new SqlParameter("@paramModifyBy", SqlDbType.VarChar);
                par[5].Value = modifyBy;
            }
            if (!string.IsNullOrEmpty(Destination_ID))
            {
                par[6] = new SqlParameter("paramDestination_ID", SqlDbType.Int);
                par[6].Value = Convert.ToInt32(Destination_ID);
            }
            if (!string.IsNullOrEmpty(PageID))
            {
                par[7] = new SqlParameter("@paramPageID", SqlDbType.VarChar);
                par[7].Value = PageID;
            }
            if (!string.IsNullOrEmpty(provider))
            {
                par[8] = new SqlParameter("@paramProvider", SqlDbType.VarChar);
                par[8].Value = provider;
            }
            par[9] = new SqlParameter("@Counter", SqlDbType.Int);
            par[9].Value = 4;
            int id = SqlHelper.ExecuteNonQuery(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, par);
            if (id > 0)
            {
                return true;
            }
            else
            { return false; }

        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public bool DeleteFlightDestination(int Destination_ID)
    {
        string _CommandText = "FlightDestinationDetail";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            param[0] = new SqlParameter("@paramDestination_ID", SqlDbType.Int);
            param[0].Value = Destination_ID;

            param[1] = new SqlParameter("@Counter", SqlDbType.Int);
            param[1].Value = 3;

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            if (count > 0)
                return true;
            else
                return false;

        }
        catch { return false; }
    }

    #endregion
    #region BaggageAllowances
    public bool BaggageAll_Insert(string AirlineCode, string DestCode, string ClassType, string BaggageAll, string ModifyBy, string BaggageURL, string OnlineCheckingUrl, string counter)
    {
        string _CommandText = "sp_BaggageAllowances";
        SqlParameter[] param = new SqlParameter[8];
        try
        {
            if (!string.IsNullOrEmpty(AirlineCode))
            {
                param[0] = new SqlParameter("@paramAirCode", SqlDbType.VarChar);
                param[0].Value = AirlineCode;
            }
            if (!string.IsNullOrEmpty(DestCode))
            {
                param[1] = new SqlParameter("@paramDestCode", SqlDbType.VarChar);
                param[1].Value = DestCode;
            }
            if (!string.IsNullOrEmpty(ClassType))
            {
                param[2] = new SqlParameter("@paramClassType", SqlDbType.VarChar);
                param[2].Value = ClassType;
            }
            if (!string.IsNullOrEmpty(BaggageAll))
            {
                param[3] = new SqlParameter("@paramBaggage_All", SqlDbType.VarChar);
                param[3].Value = BaggageAll;
            }
            if (!string.IsNullOrEmpty(ModifyBy))
            {
                param[4] = new SqlParameter("@paramModifyBy", SqlDbType.VarChar);
                param[4].Value = ModifyBy;
            }
            if (!string.IsNullOrEmpty(BaggageURL))
            {
                param[5] = new SqlParameter("@paramBaggageURL", SqlDbType.NVarChar);
                param[5].Value = BaggageURL;
            }
            if (!string.IsNullOrEmpty(OnlineCheckingUrl))
            {
                param[6] = new SqlParameter("@paramOnlinecheckingURL", SqlDbType.NVarChar);
                param[6].Value = OnlineCheckingUrl;
            }
            param[7] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[7].Value = counter;

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            if (count > 0)
                return true;
            else
                return false;

        }
        catch { return false; }
    }
    public bool BaggageAll_Update(string AirlineCode, string DestCode, string ClassType, string BaggageAll, string ModifyBy, string BaggageURL, string OnlineCheckingUrl, string BaggageAllId, string counter)
    {
        string _CommandText = "sp_BaggageAllowances";
        SqlParameter[] param = new SqlParameter[9];
        try
        {
            if (!string.IsNullOrEmpty(AirlineCode))
            {
                param[0] = new SqlParameter("@paramAirCode", SqlDbType.VarChar);
                param[0].Value = AirlineCode;
            }
            if (!string.IsNullOrEmpty(DestCode))
            {
                param[1] = new SqlParameter("@paramDestCode", SqlDbType.VarChar);
                param[1].Value = DestCode;
            }
            if (!string.IsNullOrEmpty(ClassType))
            {
                param[2] = new SqlParameter("@paramClassType", SqlDbType.VarChar);
                param[2].Value = ClassType;
            }
            if (!string.IsNullOrEmpty(BaggageAll))
            {
                param[3] = new SqlParameter("@paramBaggage_All", SqlDbType.VarChar);
                param[3].Value = BaggageAll;
            }
            if (!string.IsNullOrEmpty(ModifyBy))
            {
                param[4] = new SqlParameter("@paramModifyBy", SqlDbType.VarChar);
                param[4].Value = ModifyBy;
            }
            if (!string.IsNullOrEmpty(BaggageURL))
            {
                param[5] = new SqlParameter("@paramBaggageURL", SqlDbType.NVarChar);
                param[5].Value = BaggageURL;
            }
            if (!string.IsNullOrEmpty(OnlineCheckingUrl))
            {
                param[6] = new SqlParameter("@paramOnlinecheckingURL", SqlDbType.NVarChar);
                param[6].Value = OnlineCheckingUrl;
            }
            if (!string.IsNullOrEmpty(BaggageAllId))
            {
                param[7] = new SqlParameter("@paramBaggAllId", SqlDbType.Int);
                param[7].Value = Convert.ToInt32(BaggageAllId);
            }
            param[8] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[8].Value = counter;

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            if (count > 0)
                return true;
            else
                return false;

        }
        catch { return false; }
    }

    public DataTable ViewBaggageAll(string AirlineCode, string DestCode, string ClassType, string modifyDate, string BaggageAllId, string counter)
    {
        DataSet dsBaggageAll;
        string _CommandText = "sp_BaggageAllowances";
        SqlParameter[] param = new SqlParameter[6];
        try
        {
            if (!string.IsNullOrEmpty(AirlineCode))
            {

                param[0] = new SqlParameter("@paramAirCode", SqlDbType.VarChar);
                param[0].Value = AirlineCode;
            }
            if (!string.IsNullOrEmpty(DestCode))
            {
                param[1] = new SqlParameter("@paramDestCode", SqlDbType.VarChar);
                param[1].Value = DestCode;
            }
            if (!string.IsNullOrEmpty(ClassType))
            {
                param[2] = new SqlParameter("@paramClassType", SqlDbType.VarChar);
                param[2].Value = ClassType;
            }
            if (!string.IsNullOrEmpty(modifyDate))
            {
                param[3] = new SqlParameter("@paramModify_Date", SqlDbType.DateTime);
                param[3].Value = Convert.ToDateTime(modifyDate);
            }
            if (!string.IsNullOrEmpty(BaggageAllId))
            {
                param[4] = new SqlParameter("@paramBaggAllId", SqlDbType.Int);
                param[4].Value = Convert.ToInt32(BaggageAllId);
            }
            param[5] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[5].Value = counter;

            dsBaggageAll = SqlHelper.ExecuteDataset(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            return dsBaggageAll.Tables[0];
        }

        catch (Exception ex)
        {
            dsBaggageAll = null;
            return dsBaggageAll.Tables[0];
        }
    }
    public bool BaggageAll_Delete(int BaggageAllId, string counter)
    {
        string _CommandText = "sp_BaggageAllowances";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            param[0] = new SqlParameter("@paramBaggAllId", SqlDbType.Int);
            param[0].Value = BaggageAllId;
            param[1] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[1].Value = counter;

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            if (count > 0)
                return true;
            else
                return false;

        }
        catch { return false; }
    }
    #endregion
    #region AirlieDestination
    public bool AirlieDestination_Insert(string AirlineCode, string DestCode, string ModifyBy, string counter)
    {
        string _CommandText = "sp_AirlieDestination";
        SqlParameter[] param = new SqlParameter[4];
        try
        {
            if (!string.IsNullOrEmpty(AirlineCode))
            {
                param[0] = new SqlParameter("@paramAirCode", SqlDbType.VarChar);
                param[0].Value = AirlineCode;
            }
            if (!string.IsNullOrEmpty(DestCode))
            {
                param[1] = new SqlParameter("@paramDestCode", SqlDbType.VarChar);
                param[1].Value = DestCode;
            }
            if (!string.IsNullOrEmpty(ModifyBy))
            {
                param[2] = new SqlParameter("@paramModifyBy", SqlDbType.VarChar);
                param[2].Value = ModifyBy;
            }
            param[3] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[3].Value = counter;

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            if (count > 0)
                return true;
            else
                return false;

        }
        catch { return false; }
    }
    public DataTable ViewAirlieDest(string AirlineCode, string DestCode, string modifyDate, string counter)
    {
        DataSet dsAirlieDest;
        string _CommandText = "sp_AirlieDestination";
        SqlParameter[] param = new SqlParameter[4];
        try
        {
            if (!string.IsNullOrEmpty(AirlineCode))
            {
                param[0] = new SqlParameter("@paramAirCode", SqlDbType.VarChar);
                param[0].Value = AirlineCode;
            }
            if (!string.IsNullOrEmpty(DestCode))
            {
                param[1] = new SqlParameter("@paramDestCode", SqlDbType.VarChar);
                param[1].Value = DestCode;
            }
            if (!string.IsNullOrEmpty(modifyDate))
            {
                param[2] = new SqlParameter("@paramModify_Date", SqlDbType.DateTime);
                param[2].Value = Convert.ToDateTime(modifyDate);
            }
            param[3] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[3].Value = counter;

            dsAirlieDest = SqlHelper.ExecuteDataset(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            return dsAirlieDest.Tables[0];
        }

        catch (Exception ex)
        {
            dsAirlieDest = null;
            return dsAirlieDest.Tables[0];
        }
    }
    public bool AirlieDest_Delete(int AirlieDestId, string counter)
    {
        string _CommandText = "sp_AirlieDestination";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            param[0] = new SqlParameter("@paramAirDest_ID", SqlDbType.Int);
            param[0].Value = AirlieDestId;
            param[1] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[1].Value = counter;

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            if (count > 0)
                return true;
            else
                return false;

        }
        catch { return false; }
    }
    #endregion
    #region DealOfTheDay
    public bool DealOfDay_Insert(string ReferenceNo, string DepartFrom, string ArrivalTo,
        string Airline, string HotelName, string StarCategory, string FromDate,
        string ToDate, string BoardBasis, string Product, string Nights,
        string Price, string BookedBy, string counter, string Imageurl)
    {
        string _CommandText = "sp_DealOfTheDay";
        SqlParameter[] param = new SqlParameter[15];
        try
        {
            if (!string.IsNullOrEmpty(ReferenceNo))
            {
                param[0] = new SqlParameter("@paramReferenceNo", SqlDbType.VarChar);
                param[0].Value = ReferenceNo;
            }
            if (!string.IsNullOrEmpty(DepartFrom))
            {
                param[1] = new SqlParameter("@paramDepart_From", SqlDbType.VarChar);
                param[1].Value = DepartFrom;
            }
            if (!string.IsNullOrEmpty(ArrivalTo))
            {
                param[2] = new SqlParameter("@paramArrival_To", SqlDbType.VarChar);
                param[2].Value = ArrivalTo;
            }
            if (!string.IsNullOrEmpty(Airline))
            {
                param[3] = new SqlParameter("@paramAirline", SqlDbType.VarChar);
                param[3].Value = Airline;
            }
            if (!string.IsNullOrEmpty(HotelName))
            {
                param[4] = new SqlParameter("@paramHotel_Name", SqlDbType.VarChar);
                param[4].Value = HotelName;
            }
            if (!string.IsNullOrEmpty(StarCategory))
            {
                param[5] = new SqlParameter("@paramStar_Category", SqlDbType.VarChar);
                param[5].Value = StarCategory;
            }
            if (!string.IsNullOrEmpty(FromDate))
            {
                param[6] = new SqlParameter("@paramFromDate", SqlDbType.DateTime);
                param[6].Value = Convert.ToDateTime(FromDate);
            }
            if (!string.IsNullOrEmpty(ToDate))
            {
                param[7] = new SqlParameter("@paramToDate", SqlDbType.DateTime);
                param[7].Value = Convert.ToDateTime(ToDate);
            }
            if (!string.IsNullOrEmpty(BoardBasis))
            {
                param[8] = new SqlParameter("@paramBoardBasis", SqlDbType.VarChar);
                param[8].Value = BoardBasis;
            }
            if (!string.IsNullOrEmpty(Product))
            {
                param[9] = new SqlParameter("@paramProduct", SqlDbType.VarChar);
                param[9].Value = Product;
            }
            if (!string.IsNullOrEmpty(Nights))
            {
                param[10] = new SqlParameter("@paramNights", SqlDbType.Int);
                param[10].Value = Convert.ToInt16(Nights);
            }
            if (!string.IsNullOrEmpty(Price))
            {
                param[11] = new SqlParameter("@paramPrice", SqlDbType.Money);
                param[11].Value = Convert.ToDouble(Price);
            }
            if (!string.IsNullOrEmpty(BookedBy))
            {
                param[12] = new SqlParameter("@paramBookedBy", SqlDbType.VarChar);
                param[12].Value = BookedBy;
            }
            param[13] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[13].Value = counter;
            if (!string.IsNullOrEmpty(Imageurl))
            {
                param[14] = new SqlParameter("@paramImagePath", SqlDbType.NVarChar);
                param[14].Value = Imageurl;
            }

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            if (count > 0)
                return true;
            else
                return false;

        }
        catch { return false; }
    }

    public DataTable ViewDealOfTheDay(string ReferenceNo, string DepartFrom, string ArrivalTo,
        string Airline, string HotelName, string StarCategory, string FromDate, string FromDateEnd,
        string ToDate, string ToDateEnd, string BoardBasis, string Product, string ModifyDate,
          string counter)
    {
        DataSet dsDealOfTheDay;
        string _CommandText = "sp_DealOfTheDay";
        SqlParameter[] param = new SqlParameter[14];
        try
        {
            if (!string.IsNullOrEmpty(ReferenceNo))
            {
                param[0] = new SqlParameter("@paramReferenceNo", SqlDbType.VarChar);
                param[0].Value = ReferenceNo;
            }
            if (!string.IsNullOrEmpty(DepartFrom))
            {
                param[1] = new SqlParameter("@paramDepart_From", SqlDbType.VarChar);
                param[1].Value = DepartFrom;
            }
            if (!string.IsNullOrEmpty(ArrivalTo))
            {
                param[2] = new SqlParameter("@paramArrival_To", SqlDbType.VarChar);
                param[2].Value = ArrivalTo;
            }
            if (!string.IsNullOrEmpty(Airline))
            {
                param[3] = new SqlParameter("@paramAirline", SqlDbType.VarChar);
                param[3].Value = Airline;
            }
            if (!string.IsNullOrEmpty(HotelName))
            {
                param[4] = new SqlParameter("@paramHotel_Name", SqlDbType.VarChar);
                param[4].Value = HotelName;
            }
            if (!string.IsNullOrEmpty(StarCategory))
            {
                param[5] = new SqlParameter("@paramStar_Category", SqlDbType.VarChar);
                param[5].Value = StarCategory;
            }
            if (!string.IsNullOrEmpty(FromDate))
            {
                param[6] = new SqlParameter("@paramFromDate", SqlDbType.DateTime);
                param[6].Value = Convert.ToDateTime(FromDate);
            }
            if (!string.IsNullOrEmpty(FromDateEnd))
            {
                param[7] = new SqlParameter("@paramFromDateEnd", SqlDbType.DateTime);
                param[7].Value = Convert.ToDateTime(FromDateEnd);
            }
            if (!string.IsNullOrEmpty(ToDate))
            {
                param[8] = new SqlParameter("@paramToDate", SqlDbType.DateTime);
                param[8].Value = Convert.ToDateTime(ToDate);
            }
            if (!string.IsNullOrEmpty(ToDateEnd))
            {
                param[9] = new SqlParameter("@paramToDateEnd", SqlDbType.DateTime);
                param[9].Value = Convert.ToDateTime(ToDateEnd);
            }
            if (!string.IsNullOrEmpty(BoardBasis))
            {
                param[10] = new SqlParameter("@paramBoardBasis", SqlDbType.VarChar);
                param[10].Value = BoardBasis;
            }
            if (!string.IsNullOrEmpty(Product))
            {
                param[11] = new SqlParameter("@paramProduct", SqlDbType.VarChar);
                param[11].Value = Product;
            }
            if (!string.IsNullOrEmpty(ModifyDate))
            {
                param[12] = new SqlParameter("@paramModifyDate", SqlDbType.DateTime);
                param[12].Value = Convert.ToDateTime(ModifyDate);
            }
            param[13] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[13].Value = counter;

            dsDealOfTheDay = SqlHelper.ExecuteDataset(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            return dsDealOfTheDay.Tables[0];
        }

        catch (Exception ex)
        {
            dsDealOfTheDay = null;
            return dsDealOfTheDay.Tables[0];
        }
    }
    public bool DealOfTheDay_Delete(string ReferanceNo, string counter)
    {
        string _CommandText = "sp_DealOfTheDay";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            param[0] = new SqlParameter("paramReferenceNo", SqlDbType.VarChar);
            param[0].Value = ReferanceNo;
            param[1] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[1].Value = counter;

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            if (count > 0)
                return true;
            else
                return false;

        }
        catch { return false; }
    }
    #endregion
    #region  MarketingContactNo
    public bool MarketingContactNo_Insert(string MarketingWebsite, string Destination, string Product, string ContactNoI, string ContactNoII, string ContactNoIII, string ModifyBy, string counter)
    {
        string _CommandText = "sp_MarketingContactNo";
        SqlParameter[] param = new SqlParameter[8];
        try
        {
            if (!string.IsNullOrEmpty(MarketingWebsite))
            {
                param[0] = new SqlParameter("@parmMarketingWebsite", SqlDbType.NVarChar);
                param[0].Value = MarketingWebsite;
            }
            if (!string.IsNullOrEmpty(Destination))
            {
                param[1] = new SqlParameter("@parmDestination", SqlDbType.VarChar);
                param[1].Value = Destination;
            }
            if (!string.IsNullOrEmpty(Product))
            {
                param[2] = new SqlParameter("@parmProduct", SqlDbType.VarChar);
                param[2].Value = Product;
            }
            if (!string.IsNullOrEmpty(ContactNoI))
            {
                param[3] = new SqlParameter("@parmContactNoI", SqlDbType.VarChar);
                param[3].Value = ContactNoI;
            }
            if (!string.IsNullOrEmpty(ContactNoII))
            {
                param[4] = new SqlParameter("@parmContactNoII", SqlDbType.VarChar);
                param[4].Value = ContactNoII;
            }
            if (!string.IsNullOrEmpty(ContactNoIII))
            {
                param[5] = new SqlParameter("@parmContactNoIII", SqlDbType.VarChar);
                param[5].Value = ContactNoIII;
            }
            if (!string.IsNullOrEmpty(ModifyBy))
            {
                param[6] = new SqlParameter("@parmModifyBy", SqlDbType.VarChar);
                param[6].Value = ModifyBy;
            }
            param[7] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[7].Value = counter;

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            if (count > 0)
                return true;
            else
                return false;

        }
        catch { return false; }
    }
    public DataTable ViewMarketingContactNo(string Destination, string Product, string ContactNoI, string ContactNoII, string ContactNoIII, string modifyDate, string counter)
    {
        DataSet dsAirlieDest;
        string _CommandText = "sp_MarketingContactNo";
        SqlParameter[] param = new SqlParameter[7];
        try
        {
            if (!string.IsNullOrEmpty(Destination))
            {
                param[0] = new SqlParameter("@parmDestination", SqlDbType.VarChar);
                param[0].Value = Destination;
            }
            if (!string.IsNullOrEmpty(Product))
            {
                param[1] = new SqlParameter("@parmProduct", SqlDbType.VarChar);
                param[1].Value = Product;
            }
            if (!string.IsNullOrEmpty(ContactNoI))
            {
                param[2] = new SqlParameter("@parmContactNoI", SqlDbType.VarChar);
                param[2].Value = ContactNoI;
            }
            if (!string.IsNullOrEmpty(ContactNoII))
            {
                param[3] = new SqlParameter("@parmContactNoII", SqlDbType.VarChar);
                param[3].Value = ContactNoII;
            }
            if (!string.IsNullOrEmpty(ContactNoIII))
            {
                param[4] = new SqlParameter("@parmContactNoIII", SqlDbType.VarChar);
                param[4].Value = ContactNoIII;
            }
            if (!string.IsNullOrEmpty(modifyDate))
            {
                param[5] = new SqlParameter("@parmModifyDate", SqlDbType.DateTime);
                param[5].Value = Convert.ToDateTime(modifyDate);
            }
            param[6] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[6].Value = counter;

            dsAirlieDest = SqlHelper.ExecuteDataset(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            return dsAirlieDest.Tables[0];
        }

        catch (Exception ex)
        {
            dsAirlieDest = null;
            return dsAirlieDest.Tables[0];
        }
    }
    public bool MarketingContactNo_Delete(int MarketingId, string counter)
    {
        string _CommandText = "sp_MarketingContactNo";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            param[0] = new SqlParameter("@parmMarketingId", SqlDbType.Int);
            param[0].Value = MarketingId;
            param[1] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[1].Value = counter;

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            if (count > 0)
                return true;
            else
                return false;

        }
        catch { return false; }
    }
    #endregion
    #region AirlieDestMaster

    public DataTable GetAirlineDetail(string ClassType)
    {
        DataSet ds = new DataSet();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            _CommandText = "Usp_Airline_Dest_Master";
            if (!string.IsNullOrEmpty(ClassType))
            {
                param[0] = new SqlParameter("@ClassType", SqlDbType.VarChar);
                param[0].Value = ClassType;
            }
            param[1] = new SqlParameter("@Counter", SqlDbType.Int);
            param[1].Value = 1;
            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;
        }
        catch
        {
            ds = null;
            return ds.Tables[0];
        }
    }
    public DataSet GetRepeatersData(string AirlineCode, string ClassType)
    {
        DataSet ds = new DataSet();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            _CommandText = "Usp_Airline_Dest_Master";

            param[0] = new SqlParameter("@Airline_Code", SqlDbType.VarChar);
            param[0].Value = AirlineCode;
            param[1] = new SqlParameter("@ClassType", SqlDbType.VarChar);
            param[1].Value = ClassType;

            param[2] = new SqlParameter("@Counter", SqlDbType.Int);
            param[2].Value = 2;
            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            if (ds != null && ds.Tables.Count > 0)
                return ds;
            else
                return null;
        }
        catch
        {
            ds = null;
            return ds;
        }
    }

    public void MappAirlineDest(string Airline_Code, string Class_Type, string Destination_Code, string ModifyBy)
    {
        DataSet ds = new DataSet();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[5];
        try
        {
            _CommandText = "Usp_Airline_Dest_Master";

            param[0] = new SqlParameter("@Airline_Code", SqlDbType.VarChar);
            param[0].Value = Airline_Code;
            param[1] = new SqlParameter("@Class_Type", SqlDbType.VarChar);
            param[1].Value = Class_Type;
            param[2] = new SqlParameter("@Destination_Code", SqlDbType.VarChar);
            param[2].Value = Destination_Code;
            param[3] = new SqlParameter("@ModifyBy", SqlDbType.VarChar);
            param[3].Value = ModifyBy;

            param[4] = new SqlParameter("@Counter", SqlDbType.Int);
            param[4].Value = 3;
            SqlHelper.ExecuteNonQuery(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);

        }
        catch (Exception ex)
        {

        }
    }

    public void UnmappAirlineDest(int AirlineDestMst_ID)
    {
        DataSet ds = new DataSet();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            _CommandText = "Usp_Airline_Dest_Master";

            param[0] = new SqlParameter("@AirlineDestMst_ID", SqlDbType.Int);
            param[0].Value = AirlineDestMst_ID;

            param[1] = new SqlParameter("@Counter", SqlDbType.Int);
            param[1].Value = 4;
            SqlHelper.ExecuteNonQuery(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);

        }
        catch (Exception ex)
        {

        }
    }
    #endregion
    #region  FlightDestSearch
    public DataTable GetSeason(string id, string counter)
    {
        DataSet ds = new DataSet();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            _CommandText = "sp_FlightDestSearch";
            if (!string.IsNullOrEmpty(id))
            {
                param[0] = new SqlParameter("@paramSeasonID", SqlDbType.Int);
                param[0].Value = Convert.ToInt32(id);
            }
            param[1] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[1].Value = counter;
            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            return ds.Tables[0];
        }

        catch (Exception ex)
        {
            ds = null;
            return ds.Tables[0];
        }
    }
    public bool FltDestSearch_Insert(DataTable FltDestSearch, string counter)
    {
        string ss;
        string _CommandText = "sp_FlightDestSearch";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            param[0] = new SqlParameter("@paramFlightDetails", FltDestSearch);
            param[1] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[1].Value = counter;

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            if (count > 0)
                return true;
            else
                return false;

        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public DataTable GetFLT_DESTDetails(string SeasonID, string Provider, string DestinationFrom, string DestinationTo, string AirlineCode, string ExpOffersDateStart, string OffersDateEnd, string classtype, string counter)
    {
        DataSet ds = new DataSet();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[9];
        try
        {

            _CommandText = "sp_FlightDestSearch";
            if (!string.IsNullOrEmpty(SeasonID))
            {
                param[0] = new SqlParameter("@paramSeasonID", SqlDbType.Int);
                param[0].Value = Convert.ToInt32(SeasonID);
            }
            if (!string.IsNullOrEmpty(Provider))
            {
                param[1] = new SqlParameter("@paramProvider", SqlDbType.VarChar);
                param[1].Value = Provider;

            }
            if (!string.IsNullOrEmpty(DestinationFrom))
            {
                param[2] = new SqlParameter("@paramDestinationFrom", SqlDbType.VarChar);
                param[2].Value = DestinationFrom;
            }
            if (!string.IsNullOrEmpty(DestinationTo))
            {
                param[3] = new SqlParameter("@paramDestinationTo", SqlDbType.VarChar);
                param[3].Value = DestinationTo;
            }
            if (!string.IsNullOrEmpty(AirlineCode))
            {
                param[4] = new SqlParameter("@paramAirlineCode", SqlDbType.VarChar);
                param[4].Value = AirlineCode;
            }
            if (!string.IsNullOrEmpty(ExpOffersDateStart))
            {
                param[5] = new SqlParameter("@paramExpOffersDateStart", SqlDbType.DateTime);
                param[5].Value = Convert.ToDateTime(ExpOffersDateStart);
            }
            if (!string.IsNullOrEmpty(OffersDateEnd))
            {
                param[6] = new SqlParameter("@paramExpOffersDateEnd", SqlDbType.DateTime);
                param[6].Value = Convert.ToDateTime(OffersDateEnd);
            }
            if (!string.IsNullOrEmpty(classtype))
            {
                param[7] = new SqlParameter("@paramclassType", SqlDbType.VarChar);
                param[7].Value = classtype;
            }
            param[8] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[8].Value = counter;
            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            return ds.Tables[0];
        }

        catch (Exception ex)
        {
            ds = null;
            return ds.Tables[0];
        }
    }



    public bool FltDestSearch_Update(string DestSearchID, string FareID, string AirlineCode, string Baggage, string Markup, string ExpOffersDateStart, string ModiFyBy, string totalFare, string classtype, string counter)
    {
        string _CommandText = "sp_FlightDestSearch";
        SqlParameter[] param = new SqlParameter[10];
        try
        {
            if (!string.IsNullOrEmpty(DestSearchID))
            {
                param[0] = new SqlParameter("@paramFlightDestinationSearch_ID", SqlDbType.Int);
                param[0].Value = Convert.ToInt32(DestSearchID);
            }
            if (!string.IsNullOrEmpty(FareID))
            {
                param[1] = new SqlParameter("@paramFaredetailId", SqlDbType.Int);
                param[1].Value = Convert.ToInt32(FareID);
            }
            if (!string.IsNullOrEmpty(AirlineCode))
            {
                param[2] = new SqlParameter("@paramAirlineCode", SqlDbType.VarChar);
                param[2].Value = AirlineCode;

            }
            if (!string.IsNullOrEmpty(Baggage))
            {
                param[3] = new SqlParameter("@paramBaggage", SqlDbType.VarChar);
                param[3].Value = Baggage;
            }
            if (!string.IsNullOrEmpty(Markup))
            {
                param[4] = new SqlParameter("@paramMarkup", SqlDbType.Money);
                param[4].Value = Convert.ToDouble(Markup);
            }
            if (!string.IsNullOrEmpty(ExpOffersDateStart))
            {
                param[5] = new SqlParameter("@paramExpOffersDateStart", SqlDbType.DateTime);
                param[5].Value = Convert.ToDateTime(ExpOffersDateStart);
            }
            if (!string.IsNullOrEmpty(ModiFyBy))
            {
                param[6] = new SqlParameter("@paramModifyBy", SqlDbType.VarChar);
                param[6].Value = ModiFyBy;
            }
            if (!string.IsNullOrEmpty(totalFare))
            {
                param[7] = new SqlParameter("@paramTotal", SqlDbType.Money);
                param[7].Value = Convert.ToDouble(totalFare);
            }
            if (!string.IsNullOrEmpty(classtype))
            {
                param[8] = new SqlParameter("@paramclassType", SqlDbType.VarChar);
                param[8].Value = classtype;
            }
            param[9] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[9].Value = counter;

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            if (count > 0)
                return true;
            else
                return false;

        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool FltDestSearch_Delete(string Id, string counter)
    {
        string _CommandText = "sp_FlightDestSearch";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            param[0] = new SqlParameter("@paramFlightDestinationSearch_ID", SqlDbType.Int);
            param[0].Value = Convert.ToInt32(Id);
            param[1] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[1].Value = counter;

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            if (count > 0)
                return true;
            else
                return false;

        }
        catch { return false; }
    }
    #endregion
    
    public int AirlineImage_Insert(string ImageId, string AirlineCode, string classtype, string Benefit, string ModifyBy, string counter)
    {
        string _CommandText = "sp_AirlineImages";
        int response;
        SqlParameter[] param = new SqlParameter[7];
        try
        {
            if (!String.IsNullOrEmpty(ImageId))
            {
                param[0] = new SqlParameter("@paramImagePath", SqlDbType.NVarChar);
                param[0].Value = ImageId;
            }
            if (!String.IsNullOrEmpty(AirlineCode))
            {
                param[1] = new SqlParameter("@paramAirlineCode", SqlDbType.NVarChar);
                param[1].Value = AirlineCode;
            }
            if (!String.IsNullOrEmpty(classtype))
            {
                param[2] = new SqlParameter("@paramClasstype", SqlDbType.NVarChar);
                param[2].Value = classtype;
            }
            if (!String.IsNullOrEmpty(Benefit))
            {
                param[3] = new SqlParameter("@paramBenefits", SqlDbType.NVarChar);
                param[3].Value = Benefit;
            }
            if (!String.IsNullOrEmpty(ModifyBy))
            {
                param[4] = new SqlParameter("@paramModifyBy", SqlDbType.NVarChar);
                param[4].Value = ModifyBy;
            }
            if (!String.IsNullOrEmpty(counter))
            {
                param[5] = new SqlParameter("@Counter", SqlDbType.NVarChar);
                param[5].Value = counter;
            }
            param[6] = new SqlParameter("@Id", SqlDbType.Int);
            param[6].Direction = ParameterDirection.Output;
            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            response = Convert.ToInt32(param[6].Value);
            return response;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }
    public DataTable GetAirlineImage(string Date, string AirlineCode, string classtype, string AirImageId, string counter)
    {
        DataSet ds = new DataSet();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[5];
        try
        {
            _CommandText = "sp_AirlineImages";
            if (!string.IsNullOrEmpty(Date))
            {
                param[0] = new SqlParameter("@paramModifyDate", SqlDbType.DateTime);
                param[0].Value = Convert.ToDateTime(Date);
            }
            if (!String.IsNullOrEmpty(AirlineCode))
            {
                param[1] = new SqlParameter("@paramAirlineCode", SqlDbType.NVarChar);
                param[1].Value = AirlineCode;
            }
            if (!String.IsNullOrEmpty(classtype))
            {
                param[2] = new SqlParameter("@paramClasstype", SqlDbType.NVarChar);
                param[2].Value = classtype;
            }
            if (!String.IsNullOrEmpty(AirImageId))
            {
                param[3] = new SqlParameter("@paramAirImage_Id", SqlDbType.Int);
                param[3].Value = Convert.ToInt32(AirImageId);
            }
            param[4] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[4].Value = counter;
            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            return ds.Tables[0];
        }

        catch (Exception ex)
        {
            ds = null;
            return ds.Tables[0];
        }
    }
    public bool AirlineImage_Delete(int Id, string counter)
    {
        string _CommandText = "sp_AirlineImages";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            param[0] = new SqlParameter("@paramAirImage_Id", SqlDbType.Int);
            param[0].Value = Id;
            param[1] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[1].Value = counter;

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            if (count > 0)
                return true;
            else
                return false;

        }
        catch { return false; }
    }
    //public bool HolidayCategory_Insert(string CAT_CODE, string CAT_NAME, string DESCRIPTION, string CAT_TITLE, string URL, string MATADATA, string IMAGE, string IMAGE_TITLE, string OVERVIEW, string PLACE_TO_GO, string THINGS_TO_GO, string ESSENTIAL_INFO, string REMARKS, string CREATED_BY, string Counter)
    //{
    //    string _CommandText = "SP_CATEGORY";

    //    SqlParameter[] param = new SqlParameter[15];
    //    try
    //    {
    //        if (!String.IsNullOrEmpty(CAT_CODE))
    //        {
    //            param[0] = new SqlParameter("@PARAM_CODE", SqlDbType.NVarChar);
    //            param[0].Value = CAT_CODE;
    //        }
    //        if (!String.IsNullOrEmpty(CAT_NAME))
    //        {
    //            param[1] = new SqlParameter("@PARAM_NAME", SqlDbType.NVarChar);
    //            param[1].Value = CAT_NAME;
    //        }
    //        if (!String.IsNullOrEmpty(DESCRIPTION))
    //        {
    //            param[2] = new SqlParameter("@PARAM_DESCRIPTION", SqlDbType.NVarChar);
    //            param[2].Value = DESCRIPTION;
    //        }
    //        if (!String.IsNullOrEmpty(CAT_TITLE))
    //        {
    //            param[3] = new SqlParameter("@PARAM_TITLE", SqlDbType.NVarChar);
    //            param[3].Value = CAT_TITLE;
    //        }
    //        if (!String.IsNullOrEmpty(URL))
    //        {
    //            param[4] = new SqlParameter("@PARAM_URL", SqlDbType.NVarChar);
    //            param[4].Value = URL;
    //        }
    //        if (!String.IsNullOrEmpty(MATADATA))
    //        {
    //            param[5] = new SqlParameter("@PARAM_MATADATA", SqlDbType.NVarChar);
    //            param[5].Value = MATADATA;
    //        }
    //        if (!String.IsNullOrEmpty(IMAGE))
    //        {
    //            param[6] = new SqlParameter("@PARAM_IMAGE", SqlDbType.NVarChar);
    //            param[6].Value = IMAGE;
    //        }

    //        if (!String.IsNullOrEmpty(IMAGE_TITLE))
    //        {
    //            param[7] = new SqlParameter("@PARAM_IMAGE_TITLE", SqlDbType.NVarChar);
    //            param[7].Value = IMAGE_TITLE;
    //        }
    //        if (!String.IsNullOrEmpty(OVERVIEW))
    //        {
    //            param[8] = new SqlParameter("@PARAM_OVERVIEW", SqlDbType.NVarChar);
    //            param[8].Value = OVERVIEW;
    //        }
    //        if (!String.IsNullOrEmpty(PLACE_TO_GO))
    //        {
    //            param[9] = new SqlParameter("@PARAM_PLACE_TO_GO", SqlDbType.NVarChar);
    //            param[9].Value = PLACE_TO_GO;
    //        }
    //        if (!String.IsNullOrEmpty(THINGS_TO_GO))
    //        {
    //            param[10] = new SqlParameter("@PARAM_THINGS_TO_GO", SqlDbType.NVarChar);
    //            param[10].Value = THINGS_TO_GO;
    //        }
    //        if (!String.IsNullOrEmpty(ESSENTIAL_INFO))
    //        {
    //            param[11] = new SqlParameter("@PARAM_ESSENTIAL_INFO", SqlDbType.NVarChar);
    //            param[11].Value = ESSENTIAL_INFO;
    //        }
    //        if (!String.IsNullOrEmpty(REMARKS))
    //        {
    //            param[12] = new SqlParameter("@PARAM_REMARKS", SqlDbType.NVarChar);
    //            param[12].Value = REMARKS;
    //        }
    //        if (!String.IsNullOrEmpty(CREATED_BY))
    //        {
    //            param[13] = new SqlParameter("@PARAM_CREATED_BY", SqlDbType.NVarChar);
    //            param[13].Value = CREATED_BY;
    //        }
    //        if (!String.IsNullOrEmpty(Counter))
    //        {
    //            param[14] = new SqlParameter("@Counter", SqlDbType.NVarChar);
    //            param[14].Value = Counter;
    //        }

    //        int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
    //        if (count > 0)
    //            return true;
    //        else
    //            return false;
    //    }
    //    catch (Exception ex)
    //    {
    //        return false;
    //    }
    //}
    //public DataTable GetHolidayCategory(string CAT_CODE, string CAT_NAME, string date, string cat_ID, string counter)
    //{
    //    DataSet ds = new DataSet();
    //    string _CommandText = string.Empty;
    //    SqlParameter[] param = new SqlParameter[5];
    //    try
    //    {
    //        _CommandText = "SP_CATEGORY";
    //        if (!String.IsNullOrEmpty(CAT_CODE))
    //        {
    //            param[0] = new SqlParameter("@PARAM_CODE", SqlDbType.NVarChar);
    //            param[0].Value = CAT_CODE;
    //        }
    //        if (!String.IsNullOrEmpty(CAT_NAME))
    //        {
    //            param[1] = new SqlParameter("@PARAM_NAME", SqlDbType.NVarChar);
    //            param[1].Value = CAT_NAME;
    //        }
    //        if (!String.IsNullOrEmpty(date))
    //        {
    //            param[2] = new SqlParameter("@PARAM_CREATED_DATE", SqlDbType.DateTime);
    //            param[2].Value = Convert.ToDateTime(date);
    //        }
    //        if (!String.IsNullOrEmpty(cat_ID))
    //        {
    //            param[3] = new SqlParameter("@PARAM_ID", SqlDbType.Int);
    //            param[3].Value = Convert.ToInt32(cat_ID);
    //        }
    //        param[4] = new SqlParameter("@Counter", SqlDbType.VarChar);
    //        param[4].Value = counter;
    //        ds = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
    //        return ds.Tables[0];

    //    }
    //    catch (Exception ex)
    //    {
    //        ds = null;
    //        return ds.Tables[0];
    //    }
    //}
    //public bool HolidayCategory_Delete(int Id, string counter)
    //{
    //    string _CommandText = "SP_CATEGORY";
    //    SqlParameter[] param = new SqlParameter[2];
    //    try
    //    {
    //        param[0] = new SqlParameter("@PARAM_ID", SqlDbType.Int);
    //        param[0].Value = Id;
    //        param[1] = new SqlParameter("@Counter", SqlDbType.VarChar);
    //        param[1].Value = counter;

    //        int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
    //        if (count > 0)
    //            return true;
    //        else
    //            return false;

    //    }
    //    catch { return false; }
    //}


    //public bool HolidayDest_Insert(string CAT_CODE, string DEST_CODE, string DEST_NAME, string DESCRIPTION, string DEST_TITLE, string URL, string MATADATA, string IMAGE, string IMAGE_TITLE, string Dest, string OVERVIEW, string PLACE_TO_GO, string THINGS_TO_GO, string ESSENTIAL_INFO, string REMARKS, string CREATED_BY, string Counter)
    //{
    //    string _CommandText = "SP_DESTINATION";

    //    SqlParameter[] param = new SqlParameter[17];
    //    try
    //    {
    //        if (!String.IsNullOrEmpty(CAT_CODE))
    //        {
    //            param[0] = new SqlParameter("@PARAMCAT_CODE", SqlDbType.VarChar);
    //            param[0].Value = CAT_CODE;
    //        }
    //        if (!String.IsNullOrEmpty(DEST_CODE))
    //        {
    //            param[1] = new SqlParameter("@PARAM_CODE", SqlDbType.NVarChar);
    //            param[1].Value = DEST_CODE;
    //        }
    //        if (!String.IsNullOrEmpty(DEST_NAME))
    //        {
    //            param[2] = new SqlParameter("@PARAM_NAME", SqlDbType.NVarChar);
    //            param[2].Value = DEST_NAME;
    //        }
    //        if (!String.IsNullOrEmpty(DESCRIPTION))
    //        {
    //            param[3] = new SqlParameter("@PARAM_DESCRIPTION", SqlDbType.NVarChar);
    //            param[3].Value = DESCRIPTION;
    //        }
    //        if (!String.IsNullOrEmpty(DEST_TITLE))
    //        {
    //            param[4] = new SqlParameter("@PARAM_TITLE", SqlDbType.NVarChar);
    //            param[4].Value = DEST_TITLE;
    //        }
    //        if (!String.IsNullOrEmpty(URL))
    //        {
    //            param[5] = new SqlParameter("@PARAM_URL", SqlDbType.NVarChar);
    //            param[5].Value = URL;
    //        }
    //        if (!String.IsNullOrEmpty(MATADATA))
    //        {
    //            param[6] = new SqlParameter("@PARAM_METATAG", SqlDbType.NVarChar);
    //            param[6].Value = MATADATA;
    //        }
    //        if (!String.IsNullOrEmpty(IMAGE))
    //        {
    //            param[7] = new SqlParameter("@PARAM_IMAGE", SqlDbType.NVarChar);
    //            param[7].Value = IMAGE;
    //        }

    //        if (!String.IsNullOrEmpty(IMAGE_TITLE))
    //        {
    //            param[8] = new SqlParameter("@PARAM_IMAGE_TITLE", SqlDbType.NVarChar);
    //            param[8].Value = IMAGE_TITLE;
    //        }
    //        if (!String.IsNullOrEmpty(Dest))
    //        {
    //            param[9] = new SqlParameter("@PARAM_POPULAR_DEST", SqlDbType.NVarChar);
    //            param[9].Value = Dest;
    //        }

    //        if (!String.IsNullOrEmpty(OVERVIEW))
    //        {
    //            param[10] = new SqlParameter("@PARAM_OVERVIEW", SqlDbType.NVarChar);
    //            param[10].Value = OVERVIEW;
    //        }
    //        if (!String.IsNullOrEmpty(PLACE_TO_GO))
    //        {
    //            param[11] = new SqlParameter("@PARAM_PLACE_TO_GO", SqlDbType.NVarChar);
    //            param[11].Value = PLACE_TO_GO;
    //        }
    //        if (!String.IsNullOrEmpty(THINGS_TO_GO))
    //        {
    //            param[12] = new SqlParameter("@PARAM_THINGS_TO_GO", SqlDbType.NVarChar);
    //            param[12].Value = THINGS_TO_GO;
    //        }
    //        if (!String.IsNullOrEmpty(ESSENTIAL_INFO))
    //        {
    //            param[13] = new SqlParameter("@PARAM_ESSENTIAL_INFO", SqlDbType.NVarChar);
    //            param[13].Value = ESSENTIAL_INFO;
    //        }
    //        if (!String.IsNullOrEmpty(REMARKS))
    //        {
    //            param[14] = new SqlParameter("@PARAM_REMARKS", SqlDbType.NVarChar);
    //            param[14].Value = REMARKS;
    //        }
    //        if (!String.IsNullOrEmpty(CREATED_BY))
    //        {
    //            param[15] = new SqlParameter("@PARAM_CREATERD_BY", SqlDbType.NVarChar);
    //            param[15].Value = CREATED_BY;
    //        }
    //        if (!String.IsNullOrEmpty(Counter))
    //        {
    //            param[16] = new SqlParameter("@Counter", SqlDbType.NVarChar);
    //            param[16].Value = Counter;
    //        }

    //        int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
    //        if (count > 0)
    //            return true;
    //        else
    //            return false;
    //    }
    //    catch (Exception ex)
    //    {
    //        return false;
    //    }
    //}
    //public DataTable GetHolidayDest(string CAT_CODE, string DEST_CODE, string date, string DEST_ID, string DestName, string counter)
    //{
    //    DataSet ds = new DataSet();
    //    string _CommandText = string.Empty;
    //    SqlParameter[] param = new SqlParameter[6];
    //    try
    //    {
    //        _CommandText = "SP_DESTINATION";
    //        if (!String.IsNullOrEmpty(CAT_CODE))
    //        {
    //            param[0] = new SqlParameter("@PARAMCAT_CODE", SqlDbType.NVarChar);
    //            param[0].Value = CAT_CODE;
    //        }
    //        if (!String.IsNullOrEmpty(DEST_CODE))
    //        {
    //            param[1] = new SqlParameter("@PARAM_CODE", SqlDbType.NVarChar);
    //            param[1].Value = DEST_CODE;
    //        }
    //        if (!String.IsNullOrEmpty(date))
    //        {
    //            param[2] = new SqlParameter("@PARAM_CREATED_DATE", SqlDbType.DateTime);
    //            param[2].Value = Convert.ToDateTime(date);
    //        }
    //        if (!String.IsNullOrEmpty(DEST_ID))
    //        {
    //            param[3] = new SqlParameter("@PARAM_ID", SqlDbType.Int);
    //            param[3].Value = Convert.ToInt32(DEST_ID);
    //        }
    //        if (!String.IsNullOrEmpty(DestName))
    //        {
    //            param[4] = new SqlParameter("@PARAM_NAME", SqlDbType.VarChar);
    //            param[4].Value = DestName;
    //        }
    //        param[5] = new SqlParameter("@Counter", SqlDbType.VarChar);
    //        param[5].Value = counter;
    //        ds = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
    //        return ds.Tables[0];

    //    }
    //    catch (Exception ex)
    //    {
    //        ds = null;
    //        return ds.Tables[0];
    //    }
    //}
    //public bool HolidayDEST_Delete(int Id, string counter)
    //{
    //    string _CommandText = "SP_DESTINATION";
    //    SqlParameter[] param = new SqlParameter[2];
    //    try
    //    {
    //        param[0] = new SqlParameter("@PARAM_ID", SqlDbType.Int);
    //        param[0].Value = Id;
    //        param[1] = new SqlParameter("@Counter", SqlDbType.VarChar);
    //        param[1].Value = counter;

    //        int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
    //        if (count > 0)
    //            return true;
    //        else
    //            return false;

    //    }
    //    catch { return false; }
    //}

    //public DataTable GetDest(string CAT_CODE, string counter)
    //{
    //    DataSet ds = new DataSet();
    //    string _CommandText = string.Empty;
    //    SqlParameter[] param = new SqlParameter[2];
    //    try
    //    {
    //        _CommandText = "SP_DESTINATION";
    //        if (!String.IsNullOrEmpty(CAT_CODE))
    //        {
    //            param[0] = new SqlParameter("@PARAMCAT_CODE", SqlDbType.NVarChar);
    //            param[0].Value = CAT_CODE;
    //        }
    //        param[1] = new SqlParameter("@Counter", SqlDbType.VarChar);
    //        param[1].Value = counter;
    //        ds = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
    //        return ds.Tables[0];

    //    }
    //    catch (Exception ex)
    //    {
    //        ds = null;
    //        return ds.Tables[0];
    //    }
    //}

    //public DataTable GetCountry(string CAT_CODE, string counter)
    //{
    //    DataSet ds = new DataSet();
    //    string _CommandText = string.Empty;
    //    SqlParameter[] param = new SqlParameter[2];
    //    try
    //    {
    //        _CommandText = "SP_DESTINATION";
    //        if (!String.IsNullOrEmpty(CAT_CODE))
    //        {
    //            param[0] = new SqlParameter("@PARAMCAT_CODE", SqlDbType.NVarChar);
    //            param[0].Value = CAT_CODE;
    //        }
    //        param[1] = new SqlParameter("@Counter", SqlDbType.VarChar);
    //        param[1].Value = counter;
    //        ds = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
    //        return ds.Tables[0];

    //    }
    //    catch (Exception ex)
    //    {
    //        ds = null;
    //        return ds.Tables[0];
    //    }
    //}
    //public DataTable GetCat(string counter)
    //{
    //    DataSet ds = new DataSet();
    //    string _CommandText = string.Empty;
    //    SqlParameter[] param = new SqlParameter[1];
    //    try
    //    {
    //        _CommandText = "SP_DESTINATION";

    //        param[0] = new SqlParameter("@Counter", SqlDbType.VarChar);
    //        param[0].Value = counter;
    //        ds = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
    //        return ds.Tables[0];

    //    }
    //    catch (Exception ex)
    //    {
    //        ds = null;
    //        return ds.Tables[0];
    //    }
    //}
    #region Add Fare

    public bool AddFare(string SeasonId, string NoAdt, string Duration, string From, string DestfromName, string To, string DesttoName,
        string Airline_Name, string Airline_Code, string Class, string ClassType, string BaseFare, string Tax, string Markup, string Total, string FilledBy,
        string Directflt, string StartDate, string EndDate, string ExpOffers_Date, string Country, string Country_Code, string Continent_Name,
        string Continent_Code, string Provider, string SrchQry_From, string SrchQry_To, string Flag, string Counter)
    {
        string _CommandText = string.Empty;
        SqlParameter[] par = new SqlParameter[29];
        bool addseason = false;
        try
        {
            _CommandText = "sp_FlightFareLogDetail";
            if (!string.IsNullOrEmpty(SeasonId))
            {
                par[0] = new SqlParameter("@paramSeasonId", SqlDbType.Int);
                par[0].Value = Convert.ToInt32(SeasonId);
            }
            if (!string.IsNullOrEmpty(NoAdt))
            {
                par[1] = new SqlParameter("@paramNoAdt", SqlDbType.Int);
                par[1].Value = Convert.ToInt32(NoAdt);
            }
            if (!string.IsNullOrEmpty(Duration))
            {
                par[2] = new SqlParameter("@paramDuration", SqlDbType.Int);
                par[2].Value = Convert.ToInt32(Duration);
            }
            if (!string.IsNullOrEmpty(From))
            {
                par[3] = new SqlParameter("@paramFrom", SqlDbType.VarChar);
                par[3].Value = From;
            }
            if (!string.IsNullOrEmpty(DestfromName))
            {
                par[4] = new SqlParameter("@paramDestfromName", SqlDbType.VarChar);
                par[4].Value = DestfromName;
            }
            if (!string.IsNullOrEmpty(To))
            {
                par[5] = new SqlParameter("@paramTo", SqlDbType.VarChar);
                par[5].Value = To;
            }
            if (!string.IsNullOrEmpty(DesttoName))
            {
                par[6] = new SqlParameter("@paramDesttoName", SqlDbType.VarChar);
                par[6].Value = DesttoName;
            }
            if (!string.IsNullOrEmpty(Airline_Name))
            {
                par[7] = new SqlParameter("@paramAirline_Name", SqlDbType.VarChar);
                par[7].Value = Airline_Name;
            }
            if (!string.IsNullOrEmpty(Airline_Code))
            {
                par[8] = new SqlParameter("@paramAirline_Code", SqlDbType.VarChar);
                par[8].Value = Airline_Code;
            }
            if (!string.IsNullOrEmpty(Class))
            {
                par[9] = new SqlParameter("@paramClass", SqlDbType.VarChar);
                par[9].Value = Class;
            }
            if (!string.IsNullOrEmpty(ClassType))
            {
                par[10] = new SqlParameter("@paramClassType", SqlDbType.VarChar);
                par[10].Value = ClassType;
            }
            if (!string.IsNullOrEmpty(BaseFare))
            {
                par[11] = new SqlParameter("@paramBaseFare", SqlDbType.Money);
                par[11].Value = Convert.ToDouble(BaseFare);
            }
            if (!string.IsNullOrEmpty(Tax))
            {
                par[12] = new SqlParameter("@paramTax", SqlDbType.Money);
                par[12].Value = Convert.ToDouble(Tax);
            }
            if (!string.IsNullOrEmpty(Markup))
            {
                par[13] = new SqlParameter("@paramMarkup", SqlDbType.Money);
                par[13].Value = Convert.ToDouble(Markup);
            }
            if (!string.IsNullOrEmpty(Total))
            {
                par[14] = new SqlParameter("@paramTotal", SqlDbType.Money);
                par[14].Value = Convert.ToDouble(Total);
            }

            if (!string.IsNullOrEmpty(FilledBy))
            {
                par[15] = new SqlParameter("@paramFilledBy", SqlDbType.VarChar);
                par[15].Value = FilledBy;
            }
            if (!string.IsNullOrEmpty(Directflt))
            {
                par[16] = new SqlParameter("@paramDirectflt", SqlDbType.VarChar);
                par[16].Value = Directflt;
            }
            if (!string.IsNullOrEmpty(StartDate))
            {
                par[17] = new SqlParameter("@paramTravel_DateStart", SqlDbType.DateTime);
                par[17].Value = Convert.ToDateTime(StartDate);
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                par[18] = new SqlParameter("@paramTravel_DateEnd", SqlDbType.DateTime);
                par[18].Value = Convert.ToDateTime(EndDate);
            }
            if (!string.IsNullOrEmpty(ExpOffers_Date))
            {
                par[19] = new SqlParameter("@paramExpOffers_Date", SqlDbType.DateTime);
                par[19].Value = Convert.ToDateTime(ExpOffers_Date);
            }
            if (!string.IsNullOrEmpty(Country))
            {
                par[20] = new SqlParameter("@paramCountry", SqlDbType.VarChar);
                par[20].Value = Country;
            }
            if (!string.IsNullOrEmpty(Country_Code))
            {
                par[21] = new SqlParameter("@paramCountry_Code", SqlDbType.VarChar);
                par[21].Value = Country_Code;
            }
            if (!string.IsNullOrEmpty(Continent_Name))
            {
                par[22] = new SqlParameter("@paramContinent_Name", SqlDbType.VarChar);
                par[22].Value = Continent_Name;
            }
            if (!string.IsNullOrEmpty(Continent_Code))
            {
                par[23] = new SqlParameter("@paramContinent_Code", SqlDbType.VarChar);
                par[23].Value = Continent_Code;
            }
            if (!string.IsNullOrEmpty(Provider))
            {
                par[24] = new SqlParameter("@paramProvider", SqlDbType.VarChar);
                par[24].Value = Provider;
            }
            if (!string.IsNullOrEmpty(SrchQry_From))
            {
                par[25] = new SqlParameter("@paramSrchQry_From", SqlDbType.VarChar);
                par[25].Value = SrchQry_From;
            }
            if (!string.IsNullOrEmpty(SrchQry_To))
            {
                par[26] = new SqlParameter("@paramSrchQry_To", SqlDbType.VarChar);
                par[26].Value = SrchQry_To;
            }
            if (!string.IsNullOrEmpty(Flag))
            {
                par[27] = new SqlParameter("@paramFlag", SqlDbType.Int);
                par[27].Value = Convert.ToInt32(Flag);
            }
            par[28] = new SqlParameter("@Counter", SqlDbType.VarChar);
            par[28].Value = Counter;

            int id = SqlHelper.ExecuteNonQuery(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, par);
            if (id > 0)
            {
                addseason = true;
            }
            return addseason;
        }
        catch (Exception ex)
        {
            addseason = false;
            return addseason;
        }
    }
    public DataTable ViewFare(string FilledBy, string SeasonId, string From, string To, string Airline_Code, string ClassType, string Markup,
        string Total, string StartDate, string EndDate, string ExpOffers_Date, string Country_Code, string Continent_Code, string Provider, string Counter)
    {
        DataSet dsSeasonDetail = new DataSet();
        string _CommandText = string.Empty;
        SqlParameter[] par = new SqlParameter[15];
        try
        {
            _CommandText = "sp_FlightFareLogDetail";
            if (!string.IsNullOrEmpty(FilledBy))
            {
                par[0] = new SqlParameter("@paramFilledBy", SqlDbType.VarChar);
                par[0].Value = FilledBy;
            }
            if (!string.IsNullOrEmpty(SeasonId))
            {
                par[1] = new SqlParameter("@paramSeasonId", SqlDbType.Int);
                par[1].Value = Convert.ToInt32(SeasonId);
            }

            if (!string.IsNullOrEmpty(From))
            {
                par[2] = new SqlParameter("@paramFrom", SqlDbType.VarChar);
                par[2].Value = From;
            }

            if (!string.IsNullOrEmpty(To))
            {
                par[3] = new SqlParameter("@paramTo", SqlDbType.VarChar);
                par[3].Value = To;
            }

            if (!string.IsNullOrEmpty(Airline_Code))
            {
                par[4] = new SqlParameter("@paramAirline_Code", SqlDbType.VarChar);
                par[4].Value = Airline_Code;
            }

            if (!string.IsNullOrEmpty(ClassType))
            {
                par[5] = new SqlParameter("@paramClassType", SqlDbType.VarChar);
                par[5].Value = ClassType;
            }

            if (!string.IsNullOrEmpty(Markup))
            {
                par[6] = new SqlParameter("@paramMarkup", SqlDbType.Money);
                par[6].Value = Convert.ToDouble(Markup);
            }
            if (!string.IsNullOrEmpty(Total))
            {
                par[7] = new SqlParameter("@paramTotal", SqlDbType.Money);
                par[7].Value = Convert.ToDouble(Total);
            }

            if (!string.IsNullOrEmpty(StartDate))
            {
                par[8] = new SqlParameter("@paramTravel_DateStart", SqlDbType.DateTime);
                par[8].Value = Convert.ToDateTime(StartDate);
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                par[9] = new SqlParameter("@paramTravel_DateEnd", SqlDbType.DateTime);
                par[9].Value = Convert.ToDateTime(EndDate);
            }
            if (!string.IsNullOrEmpty(ExpOffers_Date))
            {
                par[10] = new SqlParameter("@paramExpOffers_Date", SqlDbType.DateTime);
                par[10].Value = Convert.ToDateTime(ExpOffers_Date);
            }

            if (!string.IsNullOrEmpty(Country_Code))
            {
                par[11] = new SqlParameter("@paramCountry_Code", SqlDbType.VarChar);
                par[11].Value = Country_Code;
            }

            if (!string.IsNullOrEmpty(Continent_Code))
            {
                par[12] = new SqlParameter("@paramContinent_Code", SqlDbType.VarChar);
                par[12].Value = Continent_Code;
            }
            if (!string.IsNullOrEmpty(Provider))
            {
                par[13] = new SqlParameter("@paramProvider", SqlDbType.VarChar);
                par[13].Value = Provider;
            }
            par[14] = new SqlParameter("@Counter", SqlDbType.VarChar);
            par[14].Value = Counter;

            dsSeasonDetail = SqlHelper.ExecuteDataset(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, par);
            return dsSeasonDetail.Tables[0];
        }

        catch (Exception ex)
        {
            dsSeasonDetail = null;
            return dsSeasonDetail.Tables[0];
        }
    }
    public bool DeleteFare(string fareId, string Counter)
    {
        string _CommandText = "sp_FlightFareLogDetail";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            param[0] = new SqlParameter("@paramFaredetailId", SqlDbType.Int);
            param[0].Value = Convert.ToInt64(fareId);
            param[1] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[1].Value = Counter;

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            if (count > 0)
                return true;
            else
                return false;

        }
        catch { return false; }
    }
    #endregion


    #region Add Fare NOLCC
    public bool AddFare_NOLCC(string SeasonId, string NoAdt, string Duration, string From, string DestfromName, string To, string DesttoName,
        string Airline_Name, string Airline_Code, string Class, string ClassType, string BaseFare, string Tax, string Markup, string Total, string FilledBy,
        string Directflt, string StartDate, string EndDate, string ExpOffers_Date, string Country, string Country_Code, string Continent_Name,
        string Continent_Code, string Provider, string SrchQry_From, string SrchQry_To, string Flag, string Counter)
    {
        string _CommandText = string.Empty;
        SqlParameter[] par = new SqlParameter[29];
        bool addseason = false;
        try
        {
            _CommandText = "sp_FlightFareLogDetail_NOLCC";
            if (!string.IsNullOrEmpty(SeasonId))
            {
                par[0] = new SqlParameter("@paramSeasonId", SqlDbType.Int);
                par[0].Value = Convert.ToInt32(SeasonId);
            }
            if (!string.IsNullOrEmpty(NoAdt))
            {
                par[1] = new SqlParameter("@paramNoAdt", SqlDbType.Int);
                par[1].Value = Convert.ToInt32(NoAdt);
            }
            if (!string.IsNullOrEmpty(Duration))
            {
                par[2] = new SqlParameter("@paramDuration", SqlDbType.Int);
                par[2].Value = Convert.ToInt32(Duration);
            }
            if (!string.IsNullOrEmpty(From))
            {
                par[3] = new SqlParameter("@paramFrom", SqlDbType.VarChar);
                par[3].Value = From;
            }
            if (!string.IsNullOrEmpty(DestfromName))
            {
                par[4] = new SqlParameter("@paramDestfromName", SqlDbType.VarChar);
                par[4].Value = DestfromName;
            }
            if (!string.IsNullOrEmpty(To))
            {
                par[5] = new SqlParameter("@paramTo", SqlDbType.VarChar);
                par[5].Value = To;
            }
            if (!string.IsNullOrEmpty(DesttoName))
            {
                par[6] = new SqlParameter("@paramDesttoName", SqlDbType.VarChar);
                par[6].Value = DesttoName;
            }
            if (!string.IsNullOrEmpty(Airline_Name))
            {
                par[7] = new SqlParameter("@paramAirline_Name", SqlDbType.VarChar);
                par[7].Value = Airline_Name;
            }
            if (!string.IsNullOrEmpty(Airline_Code))
            {
                par[8] = new SqlParameter("@paramAirline_Code", SqlDbType.VarChar);
                par[8].Value = Airline_Code;
            }
            if (!string.IsNullOrEmpty(Class))
            {
                par[9] = new SqlParameter("@paramClass", SqlDbType.VarChar);
                par[9].Value = Class;
            }
            if (!string.IsNullOrEmpty(ClassType))
            {
                par[10] = new SqlParameter("@paramClassType", SqlDbType.VarChar);
                par[10].Value = ClassType;
            }
            if (!string.IsNullOrEmpty(BaseFare))
            {
                par[11] = new SqlParameter("@paramBaseFare", SqlDbType.Money);
                par[11].Value = Convert.ToDouble(BaseFare);
            }
            if (!string.IsNullOrEmpty(Tax))
            {
                par[12] = new SqlParameter("@paramTax", SqlDbType.Money);
                par[12].Value = Convert.ToDouble(Tax);
            }
            if (!string.IsNullOrEmpty(Markup))
            {
                par[13] = new SqlParameter("@paramMarkup", SqlDbType.Money);
                par[13].Value = Convert.ToDouble(Markup);
            }
            if (!string.IsNullOrEmpty(Total))
            {
                par[14] = new SqlParameter("@paramTotal", SqlDbType.Money);
                par[14].Value = Convert.ToDouble(Total);
            }

            if (!string.IsNullOrEmpty(FilledBy))
            {
                par[15] = new SqlParameter("@paramFilledBy", SqlDbType.VarChar);
                par[15].Value = FilledBy;
            }
            if (!string.IsNullOrEmpty(Directflt))
            {
                par[16] = new SqlParameter("@paramDirectflt", SqlDbType.VarChar);
                par[16].Value = Directflt;
            }
            if (!string.IsNullOrEmpty(StartDate))
            {
                par[17] = new SqlParameter("@paramTravel_DateStart", SqlDbType.DateTime);
                par[17].Value = Convert.ToDateTime(StartDate);
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                par[18] = new SqlParameter("@paramTravel_DateEnd", SqlDbType.DateTime);
                par[18].Value = Convert.ToDateTime(EndDate);
            }
            if (!string.IsNullOrEmpty(ExpOffers_Date))
            {
                par[19] = new SqlParameter("@paramExpOffers_Date", SqlDbType.DateTime);
                par[19].Value = Convert.ToDateTime(ExpOffers_Date);
            }
            if (!string.IsNullOrEmpty(Country))
            {
                par[20] = new SqlParameter("@paramCountry", SqlDbType.VarChar);
                par[20].Value = Country;
            }
            if (!string.IsNullOrEmpty(Country_Code))
            {
                par[21] = new SqlParameter("@paramCountry_Code", SqlDbType.VarChar);
                par[21].Value = Country_Code;
            }
            if (!string.IsNullOrEmpty(Continent_Name))
            {
                par[22] = new SqlParameter("@paramContinent_Name", SqlDbType.VarChar);
                par[22].Value = Continent_Name;
            }
            if (!string.IsNullOrEmpty(Continent_Code))
            {
                par[23] = new SqlParameter("@paramContinent_Code", SqlDbType.VarChar);
                par[23].Value = Continent_Code;
            }
            if (!string.IsNullOrEmpty(Provider))
            {
                par[24] = new SqlParameter("@paramProvider", SqlDbType.VarChar);
                par[24].Value = Provider;
            }
            if (!string.IsNullOrEmpty(SrchQry_From))
            {
                par[25] = new SqlParameter("@paramSrchQry_From", SqlDbType.VarChar);
                par[25].Value = SrchQry_From;
            }
            if (!string.IsNullOrEmpty(SrchQry_To))
            {
                par[26] = new SqlParameter("@paramSrchQry_To", SqlDbType.VarChar);
                par[26].Value = SrchQry_To;
            }
            if (!string.IsNullOrEmpty(Flag))
            {
                par[27] = new SqlParameter("@paramFlag", SqlDbType.Int);
                par[27].Value = Convert.ToInt32(Flag);
            }
            par[28] = new SqlParameter("@Counter", SqlDbType.VarChar);
            par[28].Value = Counter;

            int id = SqlHelper.ExecuteNonQuery(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, par);
            if (id > 0)
            {
                addseason = true;
            }
            return addseason;
        }
        catch (Exception ex)
        {
            addseason = false;
            return addseason;
        }
    }

    public bool UpdateFare_NOLCC(string SeasonId, string NoAdt, string Duration, string From, string DestfromName, string To, string DesttoName,
     string Airline_Name, string Airline_Code, string Class, string ClassType, string BaseFare, string Tax, string Markup, string Total, string FilledBy,
     string Directflt, string StartDate, string EndDate, string ExpOffers_Date, string Country, string Country_Code, string Continent_Name,
     string Continent_Code, string Provider, string SrchQry_From, string SrchQry_To, string Flag,string fareId, string Counter)
    {
        string _CommandText = string.Empty;
        SqlParameter[] par = new SqlParameter[30];
        bool addseason = false;
        try
        {
            _CommandText = "sp_FlightFareLogDetail_NOLCC";
            if (!string.IsNullOrEmpty(SeasonId))
            {
                par[0] = new SqlParameter("@paramSeasonId", SqlDbType.Int);
                par[0].Value = Convert.ToInt32(SeasonId);
            }
            if (!string.IsNullOrEmpty(NoAdt))
            {
                par[1] = new SqlParameter("@paramNoAdt", SqlDbType.Int);
                par[1].Value = Convert.ToInt32(NoAdt);
            }
            if (!string.IsNullOrEmpty(Duration))
            {
                par[2] = new SqlParameter("@paramDuration", SqlDbType.Int);
                par[2].Value = Convert.ToInt32(Duration);
            }
            if (!string.IsNullOrEmpty(From))
            {
                par[3] = new SqlParameter("@paramFrom", SqlDbType.VarChar);
                par[3].Value = From;
            }
            if (!string.IsNullOrEmpty(DestfromName))
            {
                par[4] = new SqlParameter("@paramDestfromName", SqlDbType.VarChar);
                par[4].Value = DestfromName;
            }
            if (!string.IsNullOrEmpty(To))
            {
                par[5] = new SqlParameter("@paramTo", SqlDbType.VarChar);
                par[5].Value = To;
            }
            if (!string.IsNullOrEmpty(DesttoName))
            {
                par[6] = new SqlParameter("@paramDesttoName", SqlDbType.VarChar);
                par[6].Value = DesttoName;
            }
            if (!string.IsNullOrEmpty(Airline_Name))
            {
                par[7] = new SqlParameter("@paramAirline_Name", SqlDbType.VarChar);
                par[7].Value = Airline_Name;
            }
            if (!string.IsNullOrEmpty(Airline_Code))
            {
                par[8] = new SqlParameter("@paramAirline_Code", SqlDbType.VarChar);
                par[8].Value = Airline_Code;
            }
            if (!string.IsNullOrEmpty(Class))
            {
                par[9] = new SqlParameter("@paramClass", SqlDbType.VarChar);
                par[9].Value = Class;
            }
            if (!string.IsNullOrEmpty(ClassType))
            {
                par[10] = new SqlParameter("@paramClassType", SqlDbType.VarChar);
                par[10].Value = ClassType;
            }
            if (!string.IsNullOrEmpty(BaseFare))
            {
                par[11] = new SqlParameter("@paramBaseFare", SqlDbType.Money);
                par[11].Value = Convert.ToDouble(BaseFare);
            }
            if (!string.IsNullOrEmpty(Tax))
            {
                par[12] = new SqlParameter("@paramTax", SqlDbType.Money);
                par[12].Value = Convert.ToDouble(Tax);
            }
            if (!string.IsNullOrEmpty(Markup))
            {
                par[13] = new SqlParameter("@paramMarkup", SqlDbType.Money);
                par[13].Value = Convert.ToDouble(Markup);
            }
            if (!string.IsNullOrEmpty(Total))
            {
                par[14] = new SqlParameter("@paramTotal", SqlDbType.Money);
                par[14].Value = Convert.ToDouble(Total);
            }

            if (!string.IsNullOrEmpty(FilledBy))
            {
                par[15] = new SqlParameter("@paramFilledBy", SqlDbType.VarChar);
                par[15].Value = FilledBy;
            }
            if (!string.IsNullOrEmpty(Directflt))
            {
                par[16] = new SqlParameter("@paramDirectflt", SqlDbType.VarChar);
                par[16].Value = Directflt;
            }
            if (!string.IsNullOrEmpty(StartDate))
            {
                par[17] = new SqlParameter("@paramTravel_DateStart", SqlDbType.DateTime);
                par[17].Value = Convert.ToDateTime(StartDate);
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                par[18] = new SqlParameter("@paramTravel_DateEnd", SqlDbType.DateTime);
                par[18].Value = Convert.ToDateTime(EndDate);
            }
            if (!string.IsNullOrEmpty(ExpOffers_Date))
            {
                par[19] = new SqlParameter("@paramExpOffers_Date", SqlDbType.DateTime);
                par[19].Value = Convert.ToDateTime(ExpOffers_Date);
            }
            if (!string.IsNullOrEmpty(Country))
            {
                par[20] = new SqlParameter("@paramCountry", SqlDbType.VarChar);
                par[20].Value = Country;
            }
            if (!string.IsNullOrEmpty(Country_Code))
            {
                par[21] = new SqlParameter("@paramCountry_Code", SqlDbType.VarChar);
                par[21].Value = Country_Code;
            }
            if (!string.IsNullOrEmpty(Continent_Name))
            {
                par[22] = new SqlParameter("@paramContinent_Name", SqlDbType.VarChar);
                par[22].Value = Continent_Name;
            }
            if (!string.IsNullOrEmpty(Continent_Code))
            {
                par[23] = new SqlParameter("@paramContinent_Code", SqlDbType.VarChar);
                par[23].Value = Continent_Code;
            }
            if (!string.IsNullOrEmpty(Provider))
            {
                par[24] = new SqlParameter("@paramProvider", SqlDbType.VarChar);
                par[24].Value = Provider;
            }
            if (!string.IsNullOrEmpty(SrchQry_From))
            {
                par[25] = new SqlParameter("@paramSrchQry_From", SqlDbType.VarChar);
                par[25].Value = SrchQry_From;
            }
            if (!string.IsNullOrEmpty(SrchQry_To))
            {
                par[26] = new SqlParameter("@paramSrchQry_To", SqlDbType.VarChar);
                par[26].Value = SrchQry_To;
            }
            if (!string.IsNullOrEmpty(Flag))
            {
                par[27] = new SqlParameter("@paramFlag", SqlDbType.Int);
                par[27].Value = Convert.ToInt32(Flag);
            }
            par[28] = new SqlParameter("@paramFaredetailId", SqlDbType.Int);
            par[28].Value = Convert.ToInt32(fareId);
            par[29] = new SqlParameter("@Counter", SqlDbType.VarChar);
            par[29].Value = Counter;

            int id = SqlHelper.ExecuteNonQuery(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, par);
            if (id > 0)
            {
                addseason = true;
            }
            return addseason;
        }
        catch (Exception ex)
        {
            addseason = false;
            return addseason;
        }
    }
    public DataTable ViewFare_NOLCC(string FilledBy, string SeasonId, string From, string To, string Airline_Code, string ClassType, string Markup,
                                    string Total, string StartDate, string EndDate, string ExpOffers_Date, string Country_Code, string Continent_Code, string Provider, string Counter)
    {
        DataSet dsSeasonDetail = new DataSet();
        string _CommandText = string.Empty;
        SqlParameter[] par = new SqlParameter[15];
        try
        {
            _CommandText = "sp_FlightFareLogDetail_NOLCC";
            if (!string.IsNullOrEmpty(FilledBy))
            {
                par[0] = new SqlParameter("@paramFilledBy", SqlDbType.VarChar);
                par[0].Value = FilledBy;
            }
            if (!string.IsNullOrEmpty(SeasonId))
            {
                par[1] = new SqlParameter("@paramSeasonId", SqlDbType.Int);
                par[1].Value = Convert.ToInt32(SeasonId);
            }

            if (!string.IsNullOrEmpty(From))
            {
                par[2] = new SqlParameter("@paramFrom", SqlDbType.VarChar);
                par[2].Value = From;
            }

            if (!string.IsNullOrEmpty(To))
            {
                par[3] = new SqlParameter("@paramTo", SqlDbType.VarChar);
                par[3].Value = To;
            }

            if (!string.IsNullOrEmpty(Airline_Code))
            {
                par[4] = new SqlParameter("@paramAirline_Code", SqlDbType.VarChar);
                par[4].Value = Airline_Code;
            }

            if (!string.IsNullOrEmpty(ClassType))
            {
                par[5] = new SqlParameter("@paramClassType", SqlDbType.VarChar);
                par[5].Value = ClassType;
            }

            if (!string.IsNullOrEmpty(Markup))
            {
                par[6] = new SqlParameter("@paramMarkup", SqlDbType.Money);
                par[6].Value = Convert.ToDouble(Markup);
            }
            if (!string.IsNullOrEmpty(Total))
            {
                par[7] = new SqlParameter("@paramTotal", SqlDbType.Money);
                par[7].Value = Convert.ToDouble(Total);
            }

            if (!string.IsNullOrEmpty(StartDate))
            {
                par[8] = new SqlParameter("@paramTravel_DateStart", SqlDbType.DateTime);
                par[8].Value = Convert.ToDateTime(StartDate);
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                par[9] = new SqlParameter("@paramTravel_DateEnd", SqlDbType.DateTime);
                par[9].Value = Convert.ToDateTime(EndDate);
            }
            if (!string.IsNullOrEmpty(ExpOffers_Date))
            {
                par[10] = new SqlParameter("@paramExpOffers_Date", SqlDbType.DateTime);
                par[10].Value = Convert.ToDateTime(ExpOffers_Date);
            }

            if (!string.IsNullOrEmpty(Country_Code))
            {
                par[11] = new SqlParameter("@paramCountry_Code", SqlDbType.VarChar);
                par[11].Value = Country_Code;
            }

            if (!string.IsNullOrEmpty(Continent_Code))
            {
                par[12] = new SqlParameter("@paramContinent_Code", SqlDbType.VarChar);
                par[12].Value = Continent_Code;
            }
            if (!string.IsNullOrEmpty(Provider))
            {
                par[13] = new SqlParameter("@paramProvider", SqlDbType.VarChar);
                par[13].Value = Provider;
            }
            par[14] = new SqlParameter("@Counter", SqlDbType.VarChar);
            par[14].Value = Counter;

            dsSeasonDetail = SqlHelper.ExecuteDataset(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, par);
            return dsSeasonDetail.Tables[0];
        }

        catch (Exception ex)
        {
            dsSeasonDetail = null;
            return dsSeasonDetail.Tables[0];
        }
    }
    public bool DeleteFare_NOLCC(string fareId, string Counter)
    {
        string _CommandText = "sp_FlightFareLogDetail_NOLCC";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            param[0] = new SqlParameter("@paramFaredetailId", SqlDbType.Int);
            param[0].Value = Convert.ToInt64(fareId);
            param[1] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[1].Value = Counter;

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            if (count > 0)
                return true;
            else
                return false;

        }
        catch { return false; }
    }


    public DataTable GetDestinations()
    {
        DataSet ds = new DataSet();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            _CommandText = "SeasonMasterDetail";
            param[0] = new SqlParameter("@Counter", SqlDbType.Int);
            param[0].Value = 6;
            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            return ds.Tables[0];
        }

        catch
        {
            ds = null;
            return ds.Tables[0];
        }
    }
    public bool DeleteFare(DataTable fareId,int Counter)
    {
        string _CommandText = "sp_FlightFareDelete";
        SqlParameter[] param = new SqlParameter[2];
        try
        {       
            param[0] = new SqlParameter("@paramFlightFareLogDetail", fareId);
            param[1] = new SqlParameter("@Counter", SqlDbType.Int);
            param[1].Value = Counter;
            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            if (count > 0)
                return true;
            else
                return false;

        }
        catch { return false; }
    }
    public bool DeleteFareLCC(DataTable fareId, int Counter)
    {
        string _CommandText = "sp_FlightFareDelete";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            param[0] = new SqlParameter("@paramFlightFareLogDetail_LCC", fareId);
            param[1] = new SqlParameter("@Counter", SqlDbType.Int);
            param[1].Value = Counter;
            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            if (count > 0)
                return true;
            else
                return false;

        }
        catch { return false; }
    }
    #endregion
    #region Add TwinCenter


    public int InsertTwinCenter(DataTable TwinCenterDetails, string Dest, string Origin, string Night, string Price, string Description, string ModifyBy, string Counter, string Heading, string savepercent, string comment,string startdate,string Enddate,string overView,string company)
    {
        string _CommandText = "sp_TwinCenter";
        SqlParameter[] param = new SqlParameter[16];
        try
        {
            param[0] = new SqlParameter("@paramTwinCenterDetails", TwinCenterDetails);
            if (!string.IsNullOrEmpty(Dest))
            {
                param[1] = new SqlParameter("@paramTC_Destination", SqlDbType.VarChar, 50);
                param[1].Value = Dest;
            }
            if (!string.IsNullOrEmpty(Origin))
            {
                param[2] = new SqlParameter("@paramTC_Origine", SqlDbType.VarChar, 50);
                param[2].Value = Origin;
            }
            if (!string.IsNullOrEmpty(Night))
            {
                param[3] = new SqlParameter("@paramTC_Nights", SqlDbType.Int);
                param[3].Value = Convert.ToInt16(Night);
            }
            if (!string.IsNullOrEmpty(Price))
            {

                param[4] = new SqlParameter("@paramTC_Price", SqlDbType.Money);
                param[4].Value = Convert.ToSingle(Price);
            }

            if (!string.IsNullOrEmpty(Description))
            {
                param[5] = new SqlParameter("@paramTC_Description", SqlDbType.VarChar);
                param[5].Value = Description;
            }
            if (!string.IsNullOrEmpty(ModifyBy))
            {
                param[6] = new SqlParameter("@paramTC_ModifyBy", SqlDbType.VarChar);
                param[6].Value = ModifyBy;
            }
            param[7] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[7].Value = Counter;
           
            param[8] = new SqlParameter("@paramStatusOut", SqlDbType.Int);
            param[8].Direction = ParameterDirection.Output;
            if (!string.IsNullOrEmpty(Heading))
            {
                param[9] = new SqlParameter("@paramTC_Heading", SqlDbType.VarChar, 500);
                param[9].Value = Heading;
            }
            if (!string.IsNullOrEmpty(savepercent))
            {
                param[10] = new SqlParameter("@paramTC_SavePercent", SqlDbType.Money);
                param[10].Value = Convert.ToDouble(savepercent);
            }
            if (!string.IsNullOrEmpty(comment))
            {
                param[11] = new SqlParameter("@paramTC_Comment", SqlDbType.VarChar);
                param[11].Value = comment;
            }
            if (!string.IsNullOrEmpty(startdate))
            {
                param[12] = new SqlParameter("@paramTC_StartDate", SqlDbType.DateTime);
                param[12].Value = Convert.ToDateTime(startdate);
            }
            if (!string.IsNullOrEmpty(Enddate))
            {
                param[13] = new SqlParameter("@paramTC_EndDate", SqlDbType.DateTime);
                param[13].Value = Convert.ToDateTime(Enddate);
            }
            if (!string.IsNullOrEmpty(overView))
            {
                param[14] = new SqlParameter("@paramTC_Overview", SqlDbType.VarChar);
                param[14].Value = overView;
            }
            if (!string.IsNullOrEmpty(company))
            {
                param[15] = new SqlParameter("@paramTC_Company", SqlDbType.VarChar);
                param[15].Value = company;
            }
            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            return Convert.ToInt16(param[8].Value);
        }
        catch { return 0; }
    }
    public DataTable GetTwinCenter(string date, string counter)
    {
        DataSet ds = new DataSet();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            _CommandText = "sp_TwinCenter";

            param[0] = new SqlParameter("@paramTC_ModifyDate", SqlDbType.DateTime);
            param[0].Value = Convert.ToDateTime(date);
            param[1] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[1].Value = counter;
            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            return ds.Tables[0];
        }

        catch
        {
            return ds.Tables[0];
        }
    }
    public DataSet GetTwinCenterDetail(string id, string counter)
    {
        DataSet ds = new DataSet();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            _CommandText = "sp_TwinCenter";

            param[0] = new SqlParameter("@paramTC_MultiCityId", SqlDbType.Int);
            param[0].Value = Convert.ToInt32(id);
            param[1] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[1].Value = counter;
            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            return ds;
        }

        catch
        {
            return ds;
        }
    }

    public DataTable SearchTwinCenter(string dest, string origin, string night, string price, string StartFrom, string StartTo, string EndFrom, string EndTo, string type, string company, string counter)
    {
        DataSet ds = new DataSet();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[11];
        try
        {
            _CommandText = "sp_TwinCenter";

            if (!string.IsNullOrEmpty(dest))
            {
                param[0] = new SqlParameter("@paramTC_Destination", SqlDbType.VarChar, 50);
                param[0].Value = dest;
            }
            if (!string.IsNullOrEmpty(origin))
            {
                param[1] = new SqlParameter("@paramTC_Origine", SqlDbType.VarChar, 50);
                param[1].Value = origin;
            }
            if (!string.IsNullOrEmpty(night))
            {
                param[2] = new SqlParameter("@paramTC_Nights", SqlDbType.Int);
                param[2].Value = Convert.ToInt16(night);
            }
            if (!string.IsNullOrEmpty(price))
            {
                param[3] = new SqlParameter("@paramTC_Price", SqlDbType.Money);
                param[3].Value = Convert.ToSingle(price);
            }
            if (!string.IsNullOrEmpty(StartFrom))
            {
                param[4] = new SqlParameter("@paramTC_StartDate", SqlDbType.DateTime);
                param[4].Value = Convert.ToDateTime(StartFrom);
            }
            if (!string.IsNullOrEmpty(StartTo))
            {
                param[5] = new SqlParameter("@paramTC_StartDateE", SqlDbType.DateTime);
                param[5].Value = Convert.ToDateTime(StartTo);
            }
            if (!string.IsNullOrEmpty(EndFrom))
            {
                param[6] = new SqlParameter("@paramTC_EndDate", SqlDbType.DateTime);
                param[6].Value = Convert.ToDateTime(EndFrom);
            }
            if (!string.IsNullOrEmpty(EndTo))
            {
                param[7] = new SqlParameter("@paramTC_EndDateE", SqlDbType.DateTime);
                param[7].Value = Convert.ToDateTime(EndTo);
            }
            if (!string.IsNullOrEmpty(type))
            {
                param[8] = new SqlParameter("@paramTC_Comment", SqlDbType.VarChar);
                param[8].Value = type;
            }
            if (!string.IsNullOrEmpty(company))
            {
                param[9] = new SqlParameter("@paramTC_Company", SqlDbType.VarChar);
                param[9].Value = company;
            }
                param[10] = new SqlParameter("@Counter", SqlDbType.VarChar);
                param[10].Value = counter;

            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            return ds.Tables[0];
        }

        catch
        {
            return ds.Tables[0];
        }
    }
   
    public bool DeleteTwinCenter(string id, string counter)
    {
        string _CommandText = string.Empty;

        SqlParameter[] param = new SqlParameter[2];
        try
        {
            _CommandText = "sp_TwinCenter";

            param[0] = new SqlParameter("@paramTC_MultiCityId", SqlDbType.Int);
            param[0].Value = Convert.ToInt32(id);
            param[1] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[1].Value = counter;

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            if (count > 0)
                return true;
            else
                return false;

        }
        catch { return false; }

    }

    public bool InsertTwinCenterImage(DataTable TwinCenterImage, string counter)
    {
        string _CommandText = string.Empty;

        SqlParameter[] param = new SqlParameter[2];
        try
        {
            _CommandText = "sp_TwinCenter";

            param[0] = new SqlParameter("@paramTwinCenterImage", TwinCenterImage);

            param[1] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[1].Value = counter;

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            if (count > 0)
                return true;
            else
                return false;

        }
        catch { return false; }

    }

    public DataTable GetTwinCenterImage(string Dest, string date, string counter)
    {
        DataSet ds = new DataSet();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            _CommandText = "sp_TwinCenter";
            if (!string.IsNullOrEmpty(Dest))
            {
                param[0] = new SqlParameter("@paramSTCI_Destination", SqlDbType.VarChar);
                param[0].Value = Dest;
            }
            if (!string.IsNullOrEmpty(date))
            {
                param[1] = new SqlParameter("@paramSTCI_ModifyDate", SqlDbType.DateTime);
                param[1].Value = Convert.ToDateTime(date);
            }
            param[2] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[2].Value = counter;
            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            return ds.Tables[0];
        }

        catch
        {
            return ds.Tables[0];
        }
    }


    public bool Update_TwinCenter(string Origin, string Night, string Price, string Description, string ModifyBy, string Heading, string savepercent, string comment, string id, string startdate, string Enddate, string overView, string Counter)
    {
        string _CommandText = "sp_TwinCenter";
        SqlParameter[] param = new SqlParameter[13];
        try
        {         
           
            if (!string.IsNullOrEmpty(Origin))
            {
                param[0] = new SqlParameter("@paramTC_Origine", SqlDbType.VarChar, 50);
                param[0].Value = Origin;
            }
            if (!string.IsNullOrEmpty(Night))
            {
                param[1] = new SqlParameter("@paramTC_Nights", SqlDbType.Int);
                param[1].Value = Convert.ToInt16(Night);
            }
            if (!string.IsNullOrEmpty(Price))
            {

                param[2] = new SqlParameter("@paramTC_Price", SqlDbType.Money);
                param[2].Value = Convert.ToSingle(Price);
            }

            if (!string.IsNullOrEmpty(Description))
            {
                param[3] = new SqlParameter("@paramTC_Description", SqlDbType.VarChar);
                param[3].Value = Description;
            }
            if (!string.IsNullOrEmpty(ModifyBy))
            {
                param[4] = new SqlParameter("@paramTC_ModifyBy", SqlDbType.VarChar);
                param[4].Value = ModifyBy;
            }
      
            if (!string.IsNullOrEmpty(Heading))
            {
                param[5] = new SqlParameter("@paramTC_Heading", SqlDbType.VarChar, 500);
                param[5].Value = Heading;
            }
            if (!string.IsNullOrEmpty(savepercent))
            {
                param[6] = new SqlParameter("@paramTC_SavePercent", SqlDbType.Money);
                param[6].Value = Convert.ToDouble(savepercent);
            }
            if (!string.IsNullOrEmpty(comment))
            {
                param[7] = new SqlParameter("@paramTC_Comment", SqlDbType.VarChar);
                param[7].Value = comment;
            }
            if (!string.IsNullOrEmpty(id))
            {
                param[8] = new SqlParameter("@paramTC_MultiCityId", SqlDbType.Int);
                param[8].Value = Convert.ToInt32(id);
            }
            if (!string.IsNullOrEmpty(startdate))
            {
                param[9] = new SqlParameter("@paramTC_StartDate", SqlDbType.DateTime);
                param[9].Value = Convert.ToDateTime(startdate);
            }
            if (!string.IsNullOrEmpty(Enddate))
            {
                param[10] = new SqlParameter("@paramTC_EndDate", SqlDbType.DateTime);
                param[10].Value = Convert.ToDateTime(Enddate);
            }
            if (!string.IsNullOrEmpty(overView))
            {
                param[11] = new SqlParameter("@paramTC_Overview", SqlDbType.VarChar);
                param[11].Value = overView;
            }
            param[12] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[12].Value = Counter;

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            if (count > 0)
            {
                return true;
            }
            else { return false; }
        }
        catch { return true; }
    }

    public bool Update_TwinCenterDTL(string Origin, string Night, string ModifyBy, string ImagePath, string id,string starrating,string hotelName, string Counter)
    {
        string _CommandText = "sp_TwinCenter";
        SqlParameter[] param = new SqlParameter[8];
        try
        {

            if (!string.IsNullOrEmpty(Origin))
            {
                param[0] = new SqlParameter("@paramTC_Origine", SqlDbType.VarChar, 50);
                param[0].Value = Origin;
            }
            if (!string.IsNullOrEmpty(Night))
            {
                param[1] = new SqlParameter("@paramTC_Nights", SqlDbType.Int);
                param[1].Value = Convert.ToInt16(Night);
            }        
            if (!string.IsNullOrEmpty(ModifyBy))
            {
                param[2] = new SqlParameter("@paramTC_ModifyBy", SqlDbType.VarChar);
                param[2].Value = ModifyBy;
            }

            if (!string.IsNullOrEmpty(ImagePath))
            {
                param[3] = new SqlParameter("@paramTCD_Image", SqlDbType.VarChar);
                param[3].Value = ImagePath;
            }
            if (!string.IsNullOrEmpty(id))
            {
                param[4] = new SqlParameter("@paramTC_MultiCityId", SqlDbType.Int);
                param[4].Value = Convert.ToInt32(id);
            }
            if (!string.IsNullOrEmpty(starrating))
            {
                param[5] = new SqlParameter("@paramTCD_StarRating", SqlDbType.Int);
                param[5].Value = Convert.ToInt32(starrating);
            }
            if (!string.IsNullOrEmpty(hotelName))
            {
                param[6] = new SqlParameter("@paramTCD_HotelName", SqlDbType.VarChar);
                param[6].Value = hotelName;
            }

                param[7] = new SqlParameter("@Counter", SqlDbType.VarChar);
                param[7].Value = Counter;

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            if (count > 0)
            {
                return true;
            }
            else { return false; }
        }
        catch { return true; }
    }
   
    #endregion



    public static DataTable BindCountry(string Continant, string Counter)
    {
        DataSet ds = new DataSet();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            _CommandText = "GET_Country_Continent";
            if (!String.IsNullOrEmpty(Continant))
            {
                param[0] = new SqlParameter("@paramContinent", SqlDbType.VarChar);
                param[0].Value = Continant;
            }
            param[1] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[1].Value = Counter;
            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            return ds.Tables[0];

        }
        catch (Exception ex)
        {
            ds = null;
            return ds.Tables[0];
        }
    }
    public DataTable BindContinent(string Counter)
    {
        DataSet ds = new DataSet();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            _CommandText = "GET_Country_Continent";

            param[0] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[0].Value = Counter;
            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            return ds.Tables[0];

        }
        catch (Exception ex)
        {
            ds = null;
            return ds.Tables[0];
        }
    }
   

    public static void GetAirline(DropDownList ddl, bool bindValue)
    {

        try
        {
            if (HttpContext.Current.Cache["Airline"] == null)
            {
                DataTable dtcom = BindAirline();
                if (dtcom.Rows.Count > 0)
                {
                    HttpContext.Current.Cache["Airline"] = dtcom;
                    ddl.DataSource = dtcom;
                    ddl.DataTextField = "Airline_Name";
                    if (bindValue)
                    {
                        ddl.DataValueField = "Airline_Code";
                    }
                    else
                    {
                        ddl.DataValueField = "Airline_Code";
                    }

                    ddl.DataBind();
                    ddl.Items.Insert(0, new ListItem("Select One", ""));
                }
            }
            else
            {
                ddl.DataSource = (DataTable)HttpContext.Current.Cache["Airline"];
                ddl.DataTextField = "Airline_Name";
                if (bindValue)
                {
                    ddl.DataValueField = "Airline_Code";
                }
                else
                {
                    ddl.DataValueField = "Airline_Code";
                }
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("Select One", ""));
            }
        }
        catch (Exception ex)
        {

        }
    }
    public static DataTable BindAirline()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string _CommandText = string.Empty;   
        try
        {
            using (SqlConnection con = DataConnection.GetConnection())
            {
                _CommandText = "Airline_Detail_Get";

                ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, _CommandText);
            }
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        catch
        {
            ds = null;
            return dt;
        }
    }

}
