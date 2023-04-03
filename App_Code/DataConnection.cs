using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for DataConnection
/// </summary>
public class DataConnection
{
    private DataConnection()
    {

    }
    public static SqlConnection GetConnection()
    {
        SqlConnection Con = new SqlConnection();
        Con.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
        return Con;
    }
    public static SqlConnection GetConnectionXL()
    {
        SqlConnection Con = new SqlConnection();
        Con.ConnectionString = ConfigurationSettings.AppSettings["ConnectionStringXL"].ToString();
        return Con;
    }
    public static SqlConnection GetConnectionMarkup()
    {
        SqlConnection Con = new SqlConnection();
        Con.ConnectionString = ConfigurationSettings.AppSettings["ConnectionStringMarkup"].ToString();
        return Con;
    }
    public static SqlConnection GetConnectionMarkupUS()
    {
        SqlConnection Con = new SqlConnection();
        Con.ConnectionString = ConfigurationSettings.AppSettings["ConnectionStringMarkupUS"].ToString();
        return Con;
    }
    public static SqlConnection GetConnectionHotel()
    {
        SqlConnection Con = new SqlConnection();
        Con.ConnectionString = ConfigurationSettings.AppSettings["ConnectionHotel"].ToString();
        return Con;
    }
    public static SqlConnection GetConnectionCache()
    {
        SqlConnection Con = new SqlConnection();
        Con.ConnectionString = ConfigurationSettings.AppSettings["ConnectionStringCache"].ToString();
        return Con;
    }

    public static SqlConnection GetConnectionFareManual()
    {
        SqlConnection Con = new SqlConnection();
        Con.ConnectionString = ConfigurationSettings.AppSettings["ConnectionStringFareManual"].ToString();
        return Con;
    }

    public static SqlConnection GetConnectionFareUpload()
    {
        SqlConnection Con = new SqlConnection();
        Con.ConnectionString = ConfigurationSettings.AppSettings["CONS"].ToString();
        return Con;
    }
    public static SqlConnection GetConnectionLog()
    {
        SqlConnection Con = new SqlConnection();
        Con.ConnectionString = ConfigurationSettings.AppSettings["CONLOG"].ToString();
        return Con;
    }
    public static SqlConnection GetConnectionCustomItinerary()
    {
        SqlConnection Con = new SqlConnection();
        Con.ConnectionString = ConfigurationSettings.AppSettings["ConnectionCustomItinerary"].ToString();
        return Con;
    }

}
