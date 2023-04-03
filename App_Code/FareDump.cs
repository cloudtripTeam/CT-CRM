using System.Data;
namespace BusinessLog
{
    public class FareDump
    {
        public static bool InsertLocations(DataTable dt)
        {
            return DataAccessLog.FareDumping.InsertLocations(dt);
        }

        public static bool InsertDate(DataTable dt)
        {
            return DataAccessLog.FareDumping.InsertDate(dt);
        }

        public static DataSet GetFlightSearchdata(string currency)
        {
            return DataAccessLog.FareDumping.GetFlightSearchdata(currency);
        }

        public static DataTable GetFlightFares()
        {
            return DataAccessLog.FareDumping.GetFlightFares();
        }

        public static bool InsertDumpFares(DataTable dt)
        {
           return DataAccessLog.FareDumping.InsertDumpFaresData(dt);
        }
        
        
    }
}
namespace DataEntityLog
{

    /// <summary>
    /// Summary description for Dates
    /// </summary>
    public class FareDumpDates
    {
        public string departdate { get; set; }
        public string returndate { get; set; }
        public string interval { get; set; }
        public string currency { get; set; }
    }


    /// <summary>
    /// Summary description for Location
    /// </summary>
    public class FareDumpLocation
    {
        public FareDumpLocation() { }

        public string origin { get; set; }
        public string destination { get; set; }
        public string currency { get; set; }
    }

    public class FareDump
    {
        public FareDump() { }

        public string origin { get; set; }
        public string destination { get; set; }
        public string departdate { get; set; }
        public string returndate { get; set; }
        public string status { get; set; }
        public string stautsDescription { set; get; }
        public string currency { set; get; }
    }


    public class FareDumpResult
    {
        public FareDumpResult() { }

        public string From { get; set; }
        public string DestfromName { get; set; }
        public string To { get; set; }
        public string DesttoName { get; set; }
        public string AvailSeats { get; set; }
        //public string Duration { get; set; }
        public string Travel_DateStart { get; set; }
        public string Travel_DateEnd { get; set; }
        public string Airline_Name { get; set; }
        public string Airline_Code { get; set; }
        public string Class { get; set; }
        public string ClassType { get; set; }
        public string BaseFare { get; set; }
        public string Tax { get; set; }
        public string Total { get; set; }
        public string FilledBy { get; set; }
        public string FillDate { get; set; }
        public string Directflt { get; set; }
        public string Country { get; set; }
        public string Country_Code { get; set; }
        public string Continent_Name { get; set; }
        public string Continent_Code { get; set; }
        public string Markup { get; set; }
        public string Provider { get; set; }
        public string Currency { get; set; }
    }

}

