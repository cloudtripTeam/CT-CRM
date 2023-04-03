using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Elmah;

namespace DataAccessLog
{
   public class FareDumping
    {

        public static bool InsertLocations(DataTable dt)
        {
           
            bool nFlag = false;
            try
            {

                using (SqlConnection con = DataConnection.GetConnection())
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "dbo.SP_FlightFares";
                    cmd.Connection = con;

                    SqlParameter param = new SqlParameter("@counter", SqlDbType.Int);
                    param.Value = 1;
                    cmd.Parameters.Add(param);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            SqlParameter paramCurrency = new SqlParameter("@currency", SqlDbType.VarChar);
                            paramCurrency.Value = dt.Rows[0]["currency"];
                            cmd.Parameters.Add(paramCurrency);
                        }
                    }

                   
                    cmd.ExecuteNonQuery();
                    con.Close();
                }



                using (SqlBulkCopy bulkcpy = new SqlBulkCopy((DataConnection.GetConnection()).ConnectionString, SqlBulkCopyOptions.KeepIdentity))
                {
                    bulkcpy.BatchSize = 10000;
                    bulkcpy.BulkCopyTimeout = int.MaxValue;
                    bulkcpy.DestinationTableName = "dbo.FR_Dmp_Destination";
                    bulkcpy.ColumnMappings.Add("origin", "FR_Dmp_Origin");
                    bulkcpy.ColumnMappings.Add("destination", "FR_Dmp_Destination");
                    bulkcpy.ColumnMappings.Add("currency", "FR_Dmp_Currency");
                    bulkcpy.WriteToServer(dt);
                    nFlag = true;
                }
            }
            catch (Exception ex)
            {
                nFlag = false;
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return nFlag;
        }

        public static bool InsertDate(DataTable dt)
        {
           
            bool nFlag = false;
            try
            {
                using (SqlBulkCopy bulkcpy = new SqlBulkCopy(DataConnection.GetConnection().ConnectionString, SqlBulkCopyOptions.KeepIdentity))
                {
                    bulkcpy.BatchSize = 10000;
                    bulkcpy.BulkCopyTimeout = int.MaxValue;
                    bulkcpy.DestinationTableName = "dbo.FR_Dmp_Dates";
                    bulkcpy.ColumnMappings.Add("departdate", "FR_Dmp_Depart");
                    bulkcpy.ColumnMappings.Add("returndate", "FR_Dmp_Return");
                    bulkcpy.ColumnMappings.Add("interval", "FR_Dmp_DayInterval");
                    bulkcpy.ColumnMappings.Add("currency", "FR_Dmp_Currency");
                    bulkcpy.WriteToServer(dt);
                    nFlag = true;
                }
            }
            catch (Exception ex)
            {
                nFlag = false;
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return nFlag;
        }

        public static DataSet GetFlightSearchdata(string currency)
        {
            
            DataSet dsFlightdata = new DataSet();
            using (SqlConnection con = DataConnection.GetConnection())
            {
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.SP_FlightFares";
                cmd.Connection = con;

                SqlParameter param = new SqlParameter("@counter", SqlDbType.Int);
                param.Value = 0;
                cmd.Parameters.Add(param);

                SqlParameter param1 = new SqlParameter("@currency", SqlDbType.VarChar);
                param1.Value = currency;
                cmd.Parameters.Add(param1);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dsFlightdata);
                con.Close();
            }
            return dsFlightdata;
        }

        public static DataTable GetFlightFares()
        {
            DataSet dsFares = new DataSet();
            
            try
            {
                using (SqlConnection sqlcon = DataConnection.GetConnection())
                {
                    sqlcon.Open();
                    string sql = "SELECT [From] as Origin,[DestfromName]as OName,[To] as Destination,[DesttoName] as DName,[NoAdt],convert(varchar(10),[Travel_DateStart],103)as DateStart,convert(varchar(10),[Travel_DateEnd],103) as DateEnd" +
                                    ",[Airline_Name] as AirName,[Airline_Code] as AirCode,[Class],[ClassType],[BaseFare],[Tax],[Total]," +
                                    "[FilledBy],convert(varchar(10),[FillDate],103) as FillDate,[Directflt]" +
                                    ",[Country],[Country_Code]as CountryCode,[Continent_Name] as ContinentName," +
                                    "[Continent_Code] as ContinentCode,[Markup],[Provider]" +
                                    " FROM [Offlinefares].[dbo].[FlightFares]";
                    using (SqlCommand sqlCmd = new SqlCommand(sql, sqlcon))
                    {
                        SqlDataAdapter sda = new SqlDataAdapter();
                        sda.SelectCommand = sqlCmd;
                        sda.SelectCommand.Connection = sqlcon;
                        sda.SelectCommand.ExecuteNonQuery();
                        sda.Fill(dsFares, "FlightDetails");
                    }
                }
            }
            catch(Exception ex) { dsFares = null; ErrorSignal.FromCurrentContext().Raise(ex); }
            return dsFares.Tables[0];
        }

        public static bool InsertDumpFaresData(DataTable dt)
        {

            try
            {
                if (dt.Rows.Count > 0)
                {
                    using (SqlBulkCopy bulkcpy = new SqlBulkCopy(DataConnection.GetConnection().ConnectionString, SqlBulkCopyOptions.KeepIdentity))
                    {
                        bulkcpy.BatchSize = 10000;
                        bulkcpy.BulkCopyTimeout = int.MaxValue;

                        bulkcpy.DestinationTableName = "FR_Dmp_AirFareOffers";
                        bulkcpy.ColumnMappings.Add("From", "From");
                        bulkcpy.ColumnMappings.Add("DestfromName", "DestfromName");
                        bulkcpy.ColumnMappings.Add("To", "To");
                        bulkcpy.ColumnMappings.Add("DesttoName", "DesttoName");
                        bulkcpy.ColumnMappings.Add("Travel_DateStart", "Travel_DateStart");
                        bulkcpy.ColumnMappings.Add("Travel_DateEnd", "Travel_DateEnd");
                        bulkcpy.ColumnMappings.Add("Airline_Name", "Airline_Name");
                        bulkcpy.ColumnMappings.Add("Airline_Code", "Airline_Code");
                        bulkcpy.ColumnMappings.Add("Class", "Class");
                        bulkcpy.ColumnMappings.Add("ClassType", "ClassType");
                        bulkcpy.ColumnMappings.Add("BaseFare", "BaseFare");
                        bulkcpy.ColumnMappings.Add("Tax", "Tax");
                        bulkcpy.ColumnMappings.Add("Total", "Total");
                        bulkcpy.ColumnMappings.Add("FilledBy", "FilledBy");
                        bulkcpy.ColumnMappings.Add("FillDate", "FillDate");
                        bulkcpy.ColumnMappings.Add("Directflt", "Directflt");
                        bulkcpy.ColumnMappings.Add("Country", "Country");
                        bulkcpy.ColumnMappings.Add("Country_Code", "Country_Code");
                        bulkcpy.ColumnMappings.Add("Continent_Name", "Continent_Name");
                        bulkcpy.ColumnMappings.Add("Continent_Code", "Continent_Code");
                        bulkcpy.ColumnMappings.Add("Markup", "Comm");
                        bulkcpy.ColumnMappings.Add("AvailSeats", "AvailSeats");
                        bulkcpy.ColumnMappings.Add("ExpOffers_Date", "ExpOffers_Date");
                        bulkcpy.ColumnMappings.Add("Currency", "Currency");
                        bulkcpy.WriteToServer(dt);
                    }
                    
                }
                return true;
            }
            catch(Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); return false; }

        }
    }
}
