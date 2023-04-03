using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using EL.Flight;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/// <summary>
/// Summary description for Common
/// </summary>
public class Common
{
    static string dateTimeNow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy");
    private const int Keysize = 256;
    private const int DerivationIterations = 1000;
    public void ExporttoExcel(System.Data.DataTable table)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        //HttpContext.Current.Response.Buffer = true;
        //HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        //HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Bookings_" + DateTime.Now + ".xlsx");

        //HttpContext.Current.Response.Charset = "utf-8";
        //HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");


        HttpContext.Current.Response.Charset = Encoding.UTF8.WebName;
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=Reports_" + DateTime.Now + ".xls");
        HttpContext.Current.Response.AddHeader("Content-Type", "application/Excel");
        HttpContext.Current.Response.ContentType = "application/application/vnd.ms-excel";

        HttpContext.Current.Response.Write(build(table));
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
    }


    private StringBuilder build(System.Data.DataTable table)
    {
        var sb = new StringBuilder();
        string tab = "";


        for (int j = 0; j < table.Columns.Count; j++)
        {

            sb.Append(table.Columns[j].ColumnName);
            sb.Append("\t");
        }

        sb.Append("\n");
        foreach (DataRow row in table.Rows)
        {
            for (int i = 0; i < table.Columns.Count; i++)
            {

                sb.Append(tab + row[i].ToString());
                tab = "\t";
            }
            tab = "";
            sb.Append("\n");
        }

        return sb;
    }

    //Dictionary<int, double> stops = new Dictionary<int, double>();
    public string GetStopOvers(EL.Flight.Itinerary itin, string type, ref Dictionary<int, double> stops)
    {
        List<EL.Flight.Itinerary_Sector> sects = null;
        sects = GetSectores(itin, type, ref stops);
        string stop = string.Empty;
        if (sects != null)
        {
            switch (sects.Count - 1)
            {
                case 0:
                    stop = "non stop";
                    break;
                case 1:
                    stop = "1 stop";
                    break;
                case 2:
                    stop = "2 stops";
                    break;
                case 3:
                    stop = "3 stops";
                    break;
                case 4:
                    stop = "4 stops";
                    break;

                case 5:
                    stop = "5 stops";
                    break;
                case 6:
                    stop = "6 stops";
                    break;


            }

        }
        return stop;
    }
    public string GetStopOvers(int stops)
    {
        string stop = string.Empty;
        switch (stops)
        {
            case 0:
                stop = "non stop";
                break;
            case 1:
                stop = "1 stop";
                break;
            case 2:
                stop = "2 stops";
                break;
            case 3:
                stop = "3 stops";
                break;
            case 4:
                stop = "4 stops";
                break;

            case 5:
                stop = "5 stops";
                break;
            case 6:
                stop = "6 stops";
                break;
        }
        return stop;
    }
    public string GetArrivingDay(string dtArr, int Days)
    {
        string ArrDay = string.Empty;
        ArrDay = Convert.ToDateTime(dtArr).AddDays(Days).ToString("dd MMM yyyy");
        return ArrDay;
    }
    public List<EL.Flight.Itinerary_Sector> GetSectores(EL.Flight.Itinerary itin, string type, ref Dictionary<int, double> stops)
    {
        List<EL.Flight.Itinerary_Sector> sects = null;
        type = type.ToLower();
        if (type == "outbound")
        {
            sects = itin.Sectors.FindAll(
                   delegate (EL.Flight.Itinerary_Sector sect)
                   {

                       return sect.IsReturn != true;
                   }
                   );
            if (stops.ContainsKey(sects.Count - 1) == false)
            {
                stops.Add(sects.Count - 1, itin.GrandTotal);
            }

        }
        else if (type == "inbound")
        {

            sects = itin.Sectors.FindAll(
                       delegate (EL.Flight.Itinerary_Sector sect)
                       {

                           return sect.IsReturn == true;
                       }
                       );
        }
        return sects;
    }

    public string GetTotalJourneyTime(EL.Flight.Itinerary itin, string type)
    {
        TimeSpan totalTime = new TimeSpan();
        Dictionary<int, double> stops = new Dictionary<int, double>();
        List<EL.Flight.Itinerary_Sector> sects = null;
        sects = GetSectores(itin, type, ref stops);
        foreach (EL.Flight.Itinerary_Sector sec in sects)
        {
            try
            {
                TimeSpan ts = new TimeSpan(int.Parse(sec.ActualTime.Split(':')[0]),    // hours
                       int.Parse(sec.ActualTime.Split(':')[1]),    // minutes
                       0);

                totalTime += ts;
            }
            catch { }

        }

        return ((totalTime.Days * 24) + totalTime.Hours) + "hrs : " + totalTime.Minutes + "mins";

    }

    public List<EL.Flight.Itinerary_Sector> getAllOUT_IN_Sectors(EL.Flight.Itinerary itin, string sector_Type)
    {
        List<EL.Flight.Itinerary_Sector> sects = new List<Itinerary_Sector>();
        sects = null;
        sector_Type = sector_Type.ToLower();
        if (sector_Type == "inbound")
        {
            sects = itin.Sectors.FindAll(
             delegate (EL.Flight.Itinerary_Sector sect)
             {

                 return sect.IsReturn == true;
             }
             );

        }
        else if (sector_Type == "outbound")
        {
            sects = itin.Sectors.FindAll(
                delegate (EL.Flight.Itinerary_Sector sect)
                {

                    return sect.IsReturn != true;
                }
                );
        }
        return sects;
    }
    public EL.Flight.Itinerary_Sector getFirstOUT_IN_Sectors(EL.Flight.Itinerary itin, string sector_Type)
    {
        sector_Type = sector_Type.ToLower();
        EL.Flight.Itinerary_Sector sect1 = new Itinerary_Sector();
        sect1 = null;
        if (sector_Type == "inbound")
        {
            sect1 = itin.Sectors.Find(
                     delegate (EL.Flight.Itinerary_Sector sect)
                     {

                         return sect.IsReturn == true;
                     }
                     );
        }
        else if (sector_Type == "outbound")
        {
            sect1 = itin.Sectors.Find(
                delegate (EL.Flight.Itinerary_Sector sect)
                {

                    return sect.IsReturn != true;
                }
                );

        }
        return sect1;
    }

    public EL.Flight.Itinerary_Sector getLastOUT_IN_Sectors(EL.Flight.Itinerary itin, string sector_Type)
    {

        sector_Type = sector_Type.ToLower();
        EL.Flight.Itinerary_Sector sect1 = new Itinerary_Sector();
        sect1 = null;
        if (sector_Type == "inbound")
        {
            sect1 = itin.Sectors.FindLast(
              delegate (EL.Flight.Itinerary_Sector sect)
              {

                  return sect.IsReturn == true;
              }
              );
        }
        else if (sector_Type == "outbound")
        {
            sect1 = itin.Sectors.FindLast(
                delegate (EL.Flight.Itinerary_Sector sect)
                {

                    return sect.IsReturn != true;
                }
                );
        }
        return sect1;

    }

    public bool ShowHiddenContent(string ip)
    {

        if (ip == "180.151.101.150" || ip == "203.92.41.202")
            return true;
        else
            return false;

    }

    public string GetAtolLink(string supplier, string atoltype, string xp, string prod)
    {
        if (supplier == "")
        {
            return "";
        }
        //else if (supplier == "10950" && atoltype == "Flights Only/PUBLIC BONDED")
        //{

        //    return "<a class='btnbook' href='https://www.flightsandholidays.biz/admin/Atol_Certificate.aspx?BID=" + xp + "&PID=" + prod + "'>ATOL</a>";
        //}
        //else
        //{
        //    return "<a class='btnbook' href='https://www.flightsandholidays.biz/admin/atol_reciept.aspx?BID=" + xp + "&PID=" + prod + "'>ATOL Reciept</a>";
        //}
        return "";
    }

    #region added by dinesh 17 Sep 2020 
    //updated on 09 oct 2020
    //For Getting the City and Country based on IP addess

    public IpInfo GetIpAddress(string ipAddress)
    {
        //string IpStack_PrivateKey = ConfigurationManager.AppSettings["IpStack_PrivateKey"].ToString();
        string API_URL = "https://freegeoip.app/json/";
        IpInfo ipInfo = new IpInfo();

        try
        {

            string url = API_URL + ipAddress;
            var request = WebRequest.Create(url);

            using (WebResponse wrs = request.GetResponse())
            using (Stream stream = wrs.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                string json = reader.ReadToEnd();
                var obj = JObject.Parse(json);
                ipInfo = JsonConvert.DeserializeObject<IpInfo>(json);
            }
        }
        catch (Exception ex)
        {
            ipInfo.country_name = null;
        }
        finally
        {

        }
        return ipInfo;
    }



    public class IpInfo
    {
        public string ip { get; set; }
        public string country_code { get; set; }
        public string country_name { get; set; }
        public string region_code { get; set; }
        public string region_name { get; set; }
        public string city { get; set; }
        public string zip_code { get; set; }
        public string time_zone { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public int metro_code { get; set; }
    }

    public string GET_SET_IpCityCountry(string Country, string City, string Region_Name, string Region)
    {
        string retResult = string.Empty;
        if (Region == "Country")
        {
            if (!string.IsNullOrWhiteSpace(Country))
            {
                retResult = Country;
            }
            else
            {
                retResult = "No Country associated with this IP-Address";
            }
        }
        if (Region == "City")
        {
            if (!string.IsNullOrWhiteSpace(City) && !string.IsNullOrWhiteSpace(Region_Name))
            {
                retResult = City + " (" + Region_Name + ")";
            }
            else if (!string.IsNullOrWhiteSpace(City))
            {
                retResult = City;
            }
            else
            {
                retResult = "No City associated with this IP-Address";
            }
        }
        return retResult;
    }

    #endregion

    List<clsAirport> lstclsAirport = new List<clsAirport>();
    clsAirport objAirport = null;

    public List<clsAirport> GetAutoCompleteAirport(string Prefix)
    {
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                param[0] = new SqlParameter("@prefixText", SqlDbType.NVarChar, 50);
                param[0].Value = Prefix;
                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_Airport_AutoComplete", param);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objAirport = new clsAirport
                    {
                        City_Name = Convert.ToString(dr["City_Name"]),
                        Airport_Name = Convert.ToString(dr["Airport_Name"]),
                        Airport_Code = Convert.ToString(dr["Airport_Code"]),
                        Country_Code = Convert.ToString(dr["Country_Code"]),
                        Country_Name = Convert.ToString(dr["Country_Name"]),
                        City_Code = Convert.ToString(dr["City_Code"])
                    };
                    lstclsAirport.Add(objAirport);
                }
            }
        }
        catch
        {

        }
        finally
        {

        }
        return lstclsAirport;
    }


    ///Encryption and Description
    public static string EncryptString(string plainText, string passPhrase)
    {
        // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
        // so that the same Salt and IV values can be used when decrypting.  
        var saltStringBytes = Generate256BitsOfRandomEntropy();
        var ivStringBytes = Generate256BitsOfRandomEntropy();
        var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
        {
            var keyBytes = password.GetBytes(Keysize / 8);
            using (var symmetricKey = new RijndaelManaged())
            {
                symmetricKey.BlockSize = 256;
                symmetricKey.Mode = CipherMode.CBC;
                symmetricKey.Padding = PaddingMode.PKCS7;
                using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                            cryptoStream.FlushFinalBlock();
                            // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
                            var cipherTextBytes = saltStringBytes;
                            cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                            cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                            memoryStream.Close();
                            cryptoStream.Close();
                            return Convert.ToBase64String(cipherTextBytes);
                        }
                    }
                }
            }
        }
    }

    public static string DecryptString(string cipherText, string passPhrase)
    {
        string stringToDecrypt = cipherText.Replace(" ", "+");

        // Get the complete stream of bytes that represent:
        // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
        var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(stringToDecrypt);
        // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
        var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
        // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
        var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
        // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
        var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

        using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
        {
            var keyBytes = password.GetBytes(Keysize / 8);
            using (var symmetricKey = new RijndaelManaged())
            {
                symmetricKey.BlockSize = 256;
                symmetricKey.Mode = CipherMode.CBC;
                symmetricKey.Padding = PaddingMode.PKCS7;
                using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                {
                    using (var memoryStream = new MemoryStream(cipherTextBytes))
                    {
                        using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            var plainTextBytes = new byte[cipherTextBytes.Length];
                            var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                            memoryStream.Close();
                            cryptoStream.Close();
                            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                        }
                    }
                }
            }
        }
    }

    private static byte[] Generate256BitsOfRandomEntropy()
    {
        var randomBytes = new byte[32]; // 32 Bytes will give us 256 bits.
        using (var rngCsp = new RNGCryptoServiceProvider())
        {
            // Fill the array with cryptographically secure random bytes.
            rngCsp.GetBytes(randomBytes);
        }
        return randomBytes;
    }

    public static string GetVisitorsIPAddress()
    {
        string VisitorsIPAddress = string.Empty;
        try
        {
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                VisitorsIPAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
            {
                VisitorsIPAddress = HttpContext.Current.Request.UserHostAddress;
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {

        }
        return VisitorsIPAddress;
    }

    public static class ErrorClass
    {
        public static string ErrorNumber { get; set; }
        public static string ErrorProcedure { get; set; }
        public static string ErrorLine { get; set; }
        public static string ErrorMessage { get; set; }
    }

    public class clsAirport
    {
        public string City_Name { get; set; }
        public string Airport_Name { get; set; }
        public string Airport_Code { get; set; }
        public string Country_Code { get; set; }
        public string Country_Name { get; set; }
        public string City_Code { get; set; }
    }


    public static string GetCabinClass(string value)
    {
        string result = string.Empty;

        if (!string.IsNullOrEmpty(value))
        {
            result = value;
        }
        else
        {
            result = "Economy";
        }
        return result;
    }

    public static string GetCabinClassChangeValue(string value)
    {
        string result = string.Empty;

        if (!string.IsNullOrEmpty(value))
        {
            if (value.ToLower() == "premium")
            {
                result = "Premium Economy";
            }
            else
            {
                result = value;
            }
        }
        else
        {
            result = "Economy";
        }
        return result;
    }

    public class DateTimeFilter
    {
        public string _FirstDayOfMonth = dateTimeNow;
        public string FirstDayOfMonth
        {
            get { return _FirstDayOfMonth; }
            set { _FirstDayOfMonth = value; }
        }

        private string _NextDayofMonth = DateTime.Today.AddDays(1).ToString("dd/MM/yyyy");
        public string NextDayofMonth
        {
            get { return _NextDayofMonth; }
            set { _NextDayofMonth = value; }
        }

        private string _TodayDayofMonth = DateTime.Today.ToString("dd/MM/yyyy");
        public string TodayDayofMonth
        {
            get { return _TodayDayofMonth; }
            set { _TodayDayofMonth = value; }
        }

        private string _FirstDay = Convert.ToString(DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy"));
        public string FirstDay
        {
            get { return _FirstDay; }
            set { _FirstDay = value; }
        }

        private string _SecondDate = Convert.ToString(DateTime.Now.AddDays(-2).ToString("dd/MM/yyyy"));
        public string SecondDate
        {
            get { return _SecondDate; }
            set { _SecondDate = value; }
        }

        private string _ThirdDate = Convert.ToString(DateTime.Now.AddDays(-3).ToString("dd/MM/yyyy"));
        public string ThirdDate
        {
            get { return _ThirdDate; }
            set { _ThirdDate = value; }
        }

        private string _FourtDate = Convert.ToString(DateTime.Now.AddDays(-4).ToString("dd/MM/yyyy"));
        public string FourtDate
        {
            get { return _FourtDate; }
            set { _FourtDate = value; }
        }

        private string _FifthDate = Convert.ToString(DateTime.Now.AddDays(-5).ToString("dd/MM/yyyy"));
        public string FifthDate
        {
            get { return _FifthDate; }
            set { _FifthDate = value; }
        }
    }
}


#region Wether Div Changes API Code

public class WeatherInfo
{
    public ResultViewModel GetWeatherInfo(string cityName)
    {

        string apiKey = "13aaa4c065ba1ac0776fe2a56186ba88";
        ResultViewModel rslt = new ResultViewModel();
        string url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&cnt=1&APPID={1}", cityName, apiKey);
        using (WebClient client = new WebClient())
        {
            string json = client.DownloadString(url);
            RootObject weatherInfo = (new JavaScriptSerializer()).Deserialize<RootObject>(json);
            if (weatherInfo != null)
            {
                rslt.Country = weatherInfo.sys.country;
                rslt.City = weatherInfo.name;
                rslt.Lat = Convert.ToString(weatherInfo.coord.lat);
                rslt.Lon = Convert.ToString(weatherInfo.coord.lon);
                rslt.Description = weatherInfo.weather[0].description;
                rslt.Humidity = Convert.ToString(weatherInfo.main.humidity);
                rslt.Temp = Convert.ToString(weatherInfo.main.temp);
                rslt.TempFeelsLike = Convert.ToString(weatherInfo.main.feels_like);
                rslt.TempMax = Convert.ToString(weatherInfo.main.temp_max);
                rslt.TempMin = Convert.ToString(weatherInfo.main.temp_min);
                rslt.WeatherIcon = weatherInfo.weather[0].icon;
            }
        }
        return rslt;
    }
}

public class ResultViewModel
{
    public string City { get; set; }
    public string Country { get; set; }
    public string Lat { get; set; }
    public string Lon { get; set; }
    public string Description { get; set; }
    public string Humidity { get; set; }
    public string TempFeelsLike { get; set; }
    public string Temp { get; set; }
    public string TempMax { get; set; }
    public string TempMin { get; set; }
    public string WeatherIcon { get; set; }
}

public class Coord
{
    public double lon { get; set; }
    public double lat { get; set; }
}

public class Weather
{
    public int id { get; set; }
    public string main { get; set; }
    public string description { get; set; }
    public string icon { get; set; }
}

public class Main
{
    public double temp { get; set; }
    public double feels_like { get; set; }
    public double temp_min { get; set; }
    public double temp_max { get; set; }
    public int pressure { get; set; }
    public int humidity { get; set; }
}

public class Wind
{
    public double speed { get; set; }
    public double deg { get; set; }
}

public class Clouds
{
    public int all { get; set; }
}

public class Sys
{
    public int type { get; set; }
    public int id { get; set; }
    public string country { get; set; }
    public int sunrise { get; set; }
    public int sunset { get; set; }
}

public class RootObject
{
    public Coord coord { get; set; }
    public List<Weather> weather { get; set; }
    public string @base { get; set; }
    public Main main { get; set; }
    public int visibility { get; set; }
    public Wind wind { get; set; }
    public Clouds clouds { get; set; }
    public int dt { get; set; }
    public Sys sys { get; set; }
    public int timezone { get; set; }
    public int id { get; set; }
    public string name { get; set; }
    public int cod { get; set; }
}
#endregion


public class PassengerPrice
{

    public PassengerPrice()
    {


    }


    private string _passsengrType = string.Empty;
    public string PassengerType
    {
        get { return _passsengrType; }
        set { _passsengrType = value; }
    }


    private int _noOfPax = 0;
    public int NoOfPax
    {
        get { return _noOfPax; }
        set { _noOfPax = value; }
    }
    private double _taxes = 0;
    public double Taxes
    {
        get { return _taxes; }
        set { _taxes = value; }
    }
    private double _basePrice = 0;
    public double BasePrice
    {
        get { return _basePrice; }
        set { _basePrice = value; }
    }
    private double _markUp = 0;
    public double MarkUp
    {
        get { return _markUp; }
        set { _markUp = value; }
    }
    private double _commission = 0;
    public double Commission
    {
        get { return _commission; }
        set { _commission = value; }
    }

}


