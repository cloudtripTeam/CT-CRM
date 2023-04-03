using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using System.IO;
using System.Drawing;
using System.Reflection;
using DataEntityLog;
using BusinessLog;
using System.Xml.XPath;
using System.Text;
using System.Xml.Xsl;
using System.Web;
using System.Web.Services;
using Elmah;
using System.Xml.Linq;


public partial class Admin_DumpFlightFares : CompressedPage
{

   // FlightService.FlightService objFlightFareService = new FlightService.FlightService();
    public static DataSet dsAirLineDetails = new DataSet();
    public static DataSet dsAirPortDetails = new DataSet();
    public static DataTable dtCurrent = new DataTable();
    public string cabin = string.Empty;
    public static DataTable dtFinaltbl = new DataTable();
    BusinessLog.FareDump faredump = new BusinessLog.FareDump();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
            if (!objUserDetail.isAuth("DumpFlightFares.aspx"))
            {
                Response.Redirect("Default.aspx");
                return;
                
            }  
            LoadExistingData();
        }
        else
        {

            dtCurrent = Session["DumpFlightFare"] as DataTable;
        }
    }

    private static List<DataEntityLog.FareDump> SaveDataIntoDb(List<FareDumpLocation> locations, List<FareDumpDates> dates,string currency)
    {

        List<DataEntityLog.FareDump> fare = new List<DataEntityLog.FareDump>();
        if (locations.Count > 0 && dates.Count > 0)
        {

            DataTable dtLocation = GetDataTableFromObject<FareDumpLocation>(locations, "Location", currency);
            DataTable dtDates = GetDataTableFromObject<FareDumpDates>(dates, "Date", currency);

            List<string> origin = ConvertDataColumnToList(dtLocation, "origin");
            List<string> destination = ConvertDataColumnToList(dtLocation, "destination");
            DataTable Loccombine = LocationCombination(origin, destination, currency);

            if (BusinessLog.FareDump.InsertDate(dtDates))
            {
                DataTable dtMerge = MergeTable(dtLocation, dtDates);

                DataRow[] drFlxi = dtDates.Select("interval>'0'");
                DataTable dtFlxi = FillDatatoTable(drFlxi);

                foreach (DataRow dr in drFlxi)
                    dr.Delete();


                foreach (DataRow dr in dtFlxi.Rows)
                {
                    dtDates.ImportRow(dr);
                }
            }

            fare = CreateFlightSearchDataTable(Loccombine, dtDates);
        }
        return fare;

    }

    #region Custom Method
    private static DataTable GetDataTableFromObject<T>(object obj, string tablename,string currency)
    {

        Type t = obj.GetType();
        List<T> tobj = (List<T>)Convert.ChangeType(obj, t);

        PropertyInfo[] property = tobj[0].GetType().GetProperties();
        DataTable dt = new DataTable(tablename);
        for (int i = 0; i < property.Length; i++)
        {
            string columnname = ((System.Reflection.MemberInfo)(property[i])).Name.ToString();
            dt.Columns.Add(columnname);
        }

        foreach (var o in tobj)
        {
            DataRow dr = dt.NewRow();
            for (int proprty = 0; proprty < property.Length; proprty++)
            {
                string column = ((System.Reflection.MemberInfo)(property[proprty])).Name.ToString();
                if (column == "currency")
                {
                    dr[column] = currency;
                }
                else
                {
                    dr[column] = o.GetType().GetProperty(column).GetValue(o, null);
                }
            }

            dt.Rows.Add(dr);

        }

        if (dt != null)
            return dt;
        return null;
    }
    private static List<string> ConvertDataColumnToList(DataTable dt, string columnname)
    {
        List<string> objList = dt.AsEnumerable()
                           .Select(r => r.Field<string>(columnname))
                           .ToList();

        return objList;
    }
    private static DataTable UniFormTable()
    {
        DataTable dt = new DataTable("FlightFare");
        dt.Columns.Add("origin");
        dt.Columns.Add("destination");
        dt.Columns.Add("departdate");
        dt.Columns.Add("returndate");
        dt.Columns.Add("interval");
        dt.Columns.Add("currency");
        return dt;
    }
    private static DataTable DatecombineTable()
    {
        DataTable dt = new DataTable("DateCombine");
        dt.Columns.Add("departdate");
        dt.Columns.Add("returndate");
        dt.Columns.Add("interval");
        dt.Columns.Add("currency");
        return dt;
    }
    private static DataTable MergeTable(DataTable dtloc, DataTable dtdate)
    {
        DataTable dtMrge = UniFormTable();
        int index = 0;
        if (dtloc.Rows.Count >= dtdate.Rows.Count)
        {
            foreach (DataRow dr in dtloc.Rows)
            {
                DataRow drMrg = dtMrge.NewRow();
                DataRow drdate = null;
                if (index < dtdate.Rows.Count)
                {
                    drdate = dtdate.Rows[index];
                    drMrg["origin"] = dr["origin"].ToString().Trim();
                    drMrg["destination"] = dr["destination"].ToString().Trim();
                    drMrg["departdate"] = drdate["departdate"].ToString().Trim();
                    drMrg["returndate"] = drdate["returndate"].ToString().Trim();
                    drMrg["interval"] = drdate["interval"].ToString().Trim();
                    drMrg["currency"] = drdate["currency"].ToString().Trim();
                }
                else
                {
                    drMrg["origin"] = dr["origin"].ToString().Trim();
                    drMrg["destination"] = dr["destination"].ToString().Trim();
                    drMrg["departdate"] = "";
                    drMrg["returndate"] = "";
                    drMrg["interval"] = "";
                    
                }

                dtMrge.Rows.Add(drMrg);
                index++;

            }
        }
        else if (dtloc.Rows.Count < dtdate.Rows.Count)
        {

            foreach (DataRow dr in dtdate.Rows)
            {
                DataRow drMrg = dtMrge.NewRow();
                DataRow drloc = null;
                if (index < dtloc.Rows.Count)
                {
                    drloc = dtloc.Rows[index];
                    drMrg["origin"] = drloc["origin"].ToString().Trim();
                    drMrg["destination"] = drloc["destination"].ToString().Trim();
                    drMrg["departdate"] = dr["departdate"].ToString().Trim();
                    drMrg["returndate"] = dr["returndate"].ToString().Trim();
                    drMrg["interval"] = dr["interval"].ToString().Trim();
                }
                else
                {
                    drMrg["origin"] = "";
                    drMrg["destination"] = "";
                    drMrg["departdate"] = dr["departdate"].ToString().Trim();
                    drMrg["returndate"] = dr["returndate"].ToString().Trim();
                    drMrg["interval"] = dr["interval"].ToString().Trim();
                }
                dtMrge.Rows.Add(drMrg);
                index++;
            }
        }


        return dtMrge;
    }
    private static DataTable FillDatatoTable(DataRow[] dr)
    {
        DataTable dt = DatecombineTable();
        for (int i = 0; i < dr.Length; i++)
        {
            DateTime departdate = Convert.ToDateTime(dr[i]["departdate"].ToString());
            DateTime returndate = Convert.ToDateTime(dr[i]["returndate"].ToString());
            double interval = Convert.ToDouble(dr[i]["interval"].ToString());
            string currency =dr[i]["currency"].ToString();

            while (departdate < returndate)
            {
                DataRow drtemp = dt.NewRow();
                drtemp["departdate"] = departdate.ToString("dd/MM/yyyy");
                if (returndate > departdate.AddDays(interval))
                {
                    drtemp["returndate"] = departdate.AddDays(interval).ToString("dd/MM/yyyy");
                }
                else
                {
                    drtemp["returndate"] = returndate.ToString("dd/MM/yyyy");
                }
                drtemp["interval"] = Convert.ToString(0);
                drtemp["currency"] = currency;
                departdate = departdate.AddDays(interval + 1.0);

                dt.Rows.Add(drtemp);

            }
        }
        return dt;
    }
    private static DataTable LocationCombination(List<string> origin, List<string> destination,string currency)
    {
        DataTable dt = new DataTable("LocCombination");
        dt.Columns.Add("origin");
        dt.Columns.Add("destination");
        dt.Columns.Add("currency");

        foreach (string orgin in origin)
        {
            if (!String.IsNullOrEmpty(orgin.Trim()))
            {
                foreach (string desti in destination)
                {
                    if (!String.IsNullOrEmpty(desti.Trim()))
                    {
                        DataRow dr = dt.NewRow();
                        dr["origin"] = orgin.Trim();
                        dr["destination"] = desti.Trim();
                        dr["currency"] = currency;
                        dt.Rows.Add(dr);
                    }
                }
            }
        }

        if (BusinessLog.FareDump.InsertLocations(dt))
            return dt;
        return null;
    }
    private static List<DataEntityLog.FareDump> CreateFlightSearchDataTable(DataTable dtLocation, DataTable dtDates)
    {
        List<DataEntityLog.FareDump> _fare = new List<DataEntityLog.FareDump>();

        try
        {
            foreach (DataRow drLoction in dtLocation.Rows)
            {
                foreach (DataRow drDate in dtDates.Rows)
                {
                    DataEntityLog.FareDump fare = new DataEntityLog.FareDump();
                    fare.origin = Convert.ToString(drLoction["origin"]).Trim();
                    fare.destination = Convert.ToString(drLoction["destination"]).Trim();
                    fare.departdate = Convert.ToString(drDate["departdate"]).Trim();
                    fare.returndate = Convert.ToString(drDate["returndate"]).Trim();
                    fare.currency = Convert.ToString(drDate["currency"]).Trim();
                    fare.status = Convert.ToString(0);

                    _fare.Add(fare);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _fare;

    }
    private static int FetchItimeary(string itineary, string origin, string destination, string departdate, string returndate, string AmtLessPercent)
    {
        DataTable dt = new DataTable();
        CreateDataTable faredumpTable = new CreateDataTable();

        dt = faredumpTable.CreateDumpFareTable();

        XmlDocument xmldoc = new XmlDocument();
        xmldoc.LoadXml(itineary);
        int Row = 0;
        foreach (XmlNode node in xmldoc.SelectNodes("Itineraries/Itinerary"))
        {
            try
            {
                FareDumpResult fareresult = new FareDumpResult();
                DataRow dr = dt.NewRow();
                dr["From"] = node.SelectSingleNode("Sectors/Sector[IsReturn='false'][1]/Departure/AirportCode").InnerText;
                dr["DestfromName"] = node.SelectSingleNode("Sectors/Sector[IsReturn='false'][1]/Departure/AirportName").InnerText;
                dr["To"] = node.SelectSingleNode("Sectors/Sector[IsReturn='true'][1]/Departure/AirportCode").InnerText;
                dr["DesttoName"] = node.SelectSingleNode("Sectors/Sector[IsReturn='true'][1]/Departure/AirportName").InnerText;
                try
                {
                    dr["Travel_DateStart"] = Convert.ToDateTime(node.SelectSingleNode("Sectors/Sector[IsReturn='false'][1]/Departure/Date").InnerText + " " + node.SelectSingleNode("Sectors/Sector[IsReturn='false'][1]/Departure/Time").InnerText);
                }
                catch
                {
                    dr["Travel_DateStart"] = node.SelectSingleNode("Sectors/Sector[IsReturn='false'][1]/Departure/Date").InnerText;
                }
                try
                {
                    dr["Travel_DateEnd"] = Convert.ToDateTime(node.SelectSingleNode("Sectors/Sector[IsReturn='true'][1]/Departure/Date").InnerText + " " + node.SelectSingleNode("Sectors/Sector[IsReturn='true'][1]/Departure/Time").InnerText);
                    dr["ExpOffers_Date"] = Convert.ToDateTime(node.SelectSingleNode("Sectors/Sector[IsReturn='true'][1]/Departure/Date").InnerText + " " + node.SelectSingleNode("Sectors/Sector[IsReturn='true'][1]/Departure/Time").InnerText);
                }
                catch
                {
                    dr["Travel_DateEnd"] = node.SelectSingleNode("Sectors/Sector[IsReturn='true'][1]/Departure/Date").InnerText;
                    dr["ExpOffers_Date"] = DateTime.Today.AddDays(15);
                }
                dr["Airline_Name"] = node.SelectSingleNode("Sectors/Sector[IsReturn='false'][1]/AirlineName").InnerText;
                dr["Airline_Code"] = node.SelectSingleNode("Sectors/Sector[IsReturn='false'][1]/AirV").InnerText;
                dr["Class"] = node.SelectSingleNode("Sectors/Sector[IsReturn='false'][1]/Class").InnerText;
                try
                {
                    dr["ClassType"] = node.SelectSingleNode("Sectors/Sector[IsReturn='false'][1]/CabinClass/Name").InnerText;
                }
                catch { }
                double PerAmount = AmtLessPercent == "" ? 0 : Convert.ToDouble(AmtLessPercent) / 100;
                dr["BaseFare"] = (Convert.ToDouble(node.SelectSingleNode("BaseFare").InnerText) - (Convert.ToDouble(node.SelectSingleNode("BaseFare").InnerText) * PerAmount)).ToString("f2");
                dr["Tax"] = (Convert.ToDouble(node.SelectSingleNode("Taxes").InnerText) - (Convert.ToDouble(node.SelectSingleNode("Taxes").InnerText) * PerAmount)).ToString("f2");
                try
                {
                    double markup = (Convert.ToDouble(node.SelectSingleNode("MarkUp").InnerText.Trim()) + Convert.ToDouble(node.SelectSingleNode("Commission").InnerText.Trim()));
                    dr["Markup"] = (markup - (markup * PerAmount)).ToString("f2");
                }
                catch
                {
                    dr["Markup"] = "0";

                }
                dr["Total"] = (Convert.ToDouble(node.SelectSingleNode("GrandTotal").InnerText) - (Convert.ToDouble(node.SelectSingleNode("GrandTotal").InnerText) * PerAmount)).ToString("f2");
                dr["Provider"] = node.SelectSingleNode("Provider").InnerText;
                dr["FilledBy"] = "Service";
                dr["FillDate"] = DateTime.Now;
                dr["Directflt"] = node.SelectNodes("Itineraries/Itinerary/Sectors/Sector").Count > 2 ? "No" : "Yes";
                dr["Country"] = node.SelectSingleNode("Sectors/Sector[IsReturn='true'][1]/Departure/AirportCountryName").InnerText;
                dr["Country_Code"] = node.SelectSingleNode("Sectors/Sector[IsReturn='true'][1]/Departure/AirportCountryCode").InnerText;
                dr["Continent_Name"] = "";
                dr["Continent_Code"] = "";
                dr["Currency"] = node.SelectSingleNode("Currency").InnerText;
                try
                {
                    dr["AvailSeats"] = Convert.ToInt16(node.SelectSingleNode("Sectors/Sector[IsReturn='false'][1]/NoSeats").InnerText.Trim());
                }
                catch { dr["AvailSeats"] = 9; }

                dt.Rows.Add(dr);

                fareresult.From = dr["From"].ToString();
                fareresult.DestfromName = dr["DestfromName"].ToString();
                fareresult.To = dr["To"].ToString();
                fareresult.DesttoName = dr["DesttoName"].ToString();
                fareresult.AvailSeats = dr["AvailSeats"].ToString();
                fareresult.Travel_DateStart = dr["Travel_DateStart"].ToString();
                fareresult.Travel_DateEnd = dr["Travel_DateEnd"].ToString();
                fareresult.Airline_Name = dr["Airline_Name"].ToString();
                fareresult.Airline_Code = dr["Airline_Code"].ToString();
                fareresult.Class = dr["Class"].ToString();
                fareresult.ClassType = dr["ClassType"].ToString();
                fareresult.BaseFare = dr["BaseFare"].ToString();
                fareresult.Tax = dr["Tax"].ToString();
                fareresult.Total = dr["Total"].ToString();
                fareresult.FilledBy = dr["FilledBy"].ToString();
                fareresult.FillDate = dr["FillDate"].ToString();
                fareresult.Directflt = dr["Directflt"].ToString();
                fareresult.Country = dr["Country"].ToString();
                fareresult.Country_Code = dr["Country_Code"].ToString();
                fareresult.Continent_Name = dr["Continent_Name"].ToString();
                fareresult.Continent_Code = dr["Continent_Code"].ToString();
                fareresult.Markup = dr["Markup"].ToString();
                fareresult.Provider = dr["Provider"].ToString();
                fareresult.Currency = dr["Currency"].ToString();

                _fareresult.Add(fareresult);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }

        }
        BusinessLog.FareDump.InsertDumpFares(dt);
        return dt.Rows.Count;

    }
    public void LoadExistingData()
    {
        try
        {
            DataSet ds = BusinessLog.FareDump.GetFlightSearchdata(ddlCurrency.SelectedValue);
            DataTable dt = MergeLocationData(ds);
            List<FareDumpLocation> locationlist = GetLocationobject(dt);
            List<FareDumpDates> datelist = GetDatesObject(ds.Tables["Table2"]);
            JSonHelper helper = new JSonHelper();
            hdnLocation.Value = helper.ConvertObjectToJSon<List<FareDumpLocation>>(locationlist);
            hdnDates.Value = helper.ConvertObjectToJSon<List<FareDumpDates>>(datelist);
            _fare = SaveDataIntoDb(locationlist, datelist,ddlCurrency.SelectedValue);

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
    }
    private DataTable LocationTableSchema()
    {
        DataTable dt = new DataTable("Location");
        dt.Columns.Add("origin");
        dt.Columns.Add("destination");
        dt.Columns.Add("currency");
        return dt;
    }
    private DataTable MergeLocationData(DataSet ds)
    {
        DataTable dtlocation = LocationTableSchema();
        if (ds.Tables[0].Rows.Count >= ds.Tables[1].Rows.Count)
        {
            for (int count = 0; count < ds.Tables[0].Rows.Count; count++)
            {
                DataRow dr = dtlocation.NewRow();
                dr["origin"] = ds.Tables[0].Rows[count][0].ToString().Trim();
                if (count < ds.Tables[1].Rows.Count)
                {
                    dr["destination"] = ds.Tables[1].Rows[count][0].ToString().Trim();
                    dr["currency"] = ds.Tables[1].Rows[count][1].ToString().Trim();
                }
                else
                {
                    dr["destination"] = "";
                    dr["currency"] = ddlCurrency.SelectedValue;
                }

                dtlocation.Rows.Add(dr);
            }
        }
        else
        {
            for (int count = 0; count < ds.Tables[1].Rows.Count; count++)
            {
                DataRow dr = dtlocation.NewRow();
                if (count < ds.Tables[0].Rows.Count)
                {
                    dr["origin"] = ds.Tables[0].Rows[count][0].ToString().Trim();
                }
                else
                {
                    dr["origin"] = "";
                }
                dr["destination"] = ds.Tables[1].Rows[count][0].ToString().Trim();
                dr["currency"] = ds.Tables[1].Rows[count][1].ToString().Trim();
                dtlocation.Rows.Add(dr);
            }


        }

        return dtlocation;
    }
    private List<FareDumpLocation> GetLocationobject(DataTable dt)
    {
        var locationlist = (from rw in dt.AsEnumerable()
                            select new FareDumpLocation()
                            {
                                origin = Convert.ToString(rw["origin"]),
                                destination = Convert.ToString(rw["destination"])
                            }).ToList();

        return locationlist;
    }
    private List<FareDumpDates> GetDatesObject(DataTable dt)
    {
        var datelist = (from rw in dt.AsEnumerable()
                        select new FareDumpDates()
                        {
                            departdate = Convert.ToString(rw["FR_Dmp_Depart"]),
                            returndate = Convert.ToString(rw["FR_Dmp_Return"]),
                            interval = Convert.ToString(rw["FR_Dmp_DayInterval"])

                        }).ToList();

        return datelist;
    }
    #endregion

    public static List<DataEntityLog.FareDump> _fare = new List<DataEntityLog.FareDump>();
    public static List<FareDumpResult> _fareresult = new List<FareDumpResult>();

    #region Ajax calling Web Method
    [WebMethod]
    public static string SaveFlightSearchdata(List<FareDumpLocation> loc, List<FareDumpDates> date,string currency)
    {
        _fare = SaveDataIntoDb(loc, date, currency);
        JSonHelper helper = new JSonHelper();
        string JSON = helper.ConvertObjectToJSon<List<DataEntityLog.FareDump>>(_fare);
        return JSON;
    }

    [WebMethod]
    public static string CheckStatus()
    {
        JSonHelper helper = new JSonHelper();
        string JSON = helper.ConvertObjectToJSon<List<DataEntityLog.FareDump>>(_fare);
        return JSON;
    }

    [WebMethod]
    public static string DownloadFare(List<DataEntityLog.FareDump> fare, string cabin, string AmtLessPercent,string currency)
    {

        _fare = new List<DataEntityLog.FareDump>();
        _fare = fare;
        _fare = HitGds(_fare, cabin, AmtLessPercent, currency);
        if (_fare != null)
        {
            JSonHelper helper = new JSonHelper();
            string JSON = helper.ConvertObjectToJSon<List<DataEntityLog.FareDump>>(_fare);
            return JSON;
        }
        else { return ""; }

    }

    [WebMethod]
    public static string ViewFares()
    {
        JSonHelper helper = new JSonHelper();
        string JSON = helper.ConvertObjectToJSon<List<FareDumpResult>>(_fareresult);
        return JSON;
    }

    [WebMethod]
    public static string GetFares()
    {
        JSonHelper helper = new JSonHelper();
        string JSON = helper.ConvertObjectToJSon<List<DataEntityLog.FareDump>>(_fare);
        return JSON;
    }

    public static List<DataEntityLog.FareDump> HitGds(List<DataEntityLog.FareDump> Fares, string cabin, string AmtLessPercent,string currency)
    {
        try
        {
            //FlightService.FlightService objFlightFareService = new FlightService.FlightService();
            FlightFareService objFlightFareService = new FlightFareService();
            _fareresult = new List<FareDumpResult>();

            for (int count = 0; count < Fares.Count; count++)
            {
                try
                {
                    if (Convert.ToDateTime(Fares[count].departdate) > DateTime.Today && Convert.ToDateTime(Fares[count].returndate) < DateTime.Today.AddDays(350))
                    {
                        //string searchQuery = MultGdsReqXml.getMultGdsReqXml(Fares[count].origin, Fares[count].destination, Fares[count].departdate, Fares[count].returndate,
                        // true, cabin, "", 1, 0, 0, false, false);

                        // string result = objFlightFareService.SearchFare(searchQuery);
                        string result = objFlightFareService.SearchFare(Fares[count].origin, Fares[count].destination, Fares[count].departdate, Fares[count].returndate,
                        true, cabin, "", 1, 0, 0, false, false, currency);
                        #region
                        //string result = File.ReadAllText(HttpContext.Current.Server.MapPath("~/XMLFile.xml"));
                        result = result = result.Replace(" xmlns=\"http://tempuri.org/\"", "");
                        //result=result.Remove
                        if (result != "")
                        {
                            XmlDocument xmldocTemp = new XmlDocument();
                            xmldocTemp.LoadXml(result);
                            result = xmldocTemp.SelectSingleNode("GetLowFaresResponse").InnerXml;
                            #endregion
                            if (result.IndexOf("Itinerary") != -1)
                            {
                                Fares[count].status = "1";
                                string data = GetHtmlFronXml(result);
                                int Rowcount = FetchItimeary(data, Fares[count].origin, Fares[count].destination, Fares[count].departdate, Fares[count].returndate, AmtLessPercent);

                                //string data = xml2html.GetHtmlFronXml(result);
                                //int Rowcount = FetchItimeary(data, Fares[count].origin, Fares[count].destination, Fares[count].departdate, Fares[count].returndate);
                            }
                            else
                            {
                                Fares[count].status = "-1";
                                Fares[count].stautsDescription = "No Fare Found!!";
                            }
                        }
                        else
                        {
                            Fares[count].status = "-1";
                            Fares[count].stautsDescription = "Travel date is invalid!!";
                        }
                        System.Threading.Thread.Sleep(1000);
                    }
                    else
                    {
                        Fares[count].status = "-1";
                        Fares[count].stautsDescription = "No Fare Found!!";
                    }
                }
                catch (Exception ex) { Fares[count].status = "-1"; Fares[count].stautsDescription = ex.ToString(); ErrorSignal.FromCurrentContext().Raise(ex); }

            }
            return Fares;
        }
        catch (System.Threading.ThreadAbortException ex) { return null; }
        catch (Exception e) { return null; }
    }

    private static string GetHtmlFronXml(string ResultXml)
    {
        string XsltPath = "DistinctAirlines.xslt";

        string _HtmlResult = string.Empty;
        XmlDocument _objDoc = new XmlDocument();
        _objDoc.LoadXml(ResultXml);
        byte[] bytes = Encoding.ASCII.GetBytes(_objDoc.OuterXml);
        MemoryStream mem = new MemoryStream(bytes);
        XPathDocument xpathdoc = new XPathDocument(mem);
        MemoryStream stream = new MemoryStream();
        StringWriter writer = new StringWriter();



        XsltSettings xsltSettings = new XsltSettings();
        xsltSettings.EnableScript = true;
        XslCompiledTransform trans = new XslCompiledTransform(true);
        trans.Load(HttpContext.Current.Server.MapPath("~/XSLT/" + XsltPath + ""), xsltSettings, new XmlUrlResolver());
        trans.Transform(xpathdoc, null, writer);
        _HtmlResult = writer.ToString();
        return _HtmlResult;
    }
    private static XElement RemoveAllNamespaces(XElement xmlDocument)
    {
        if (!xmlDocument.HasElements)
        {
            XElement xElement = new XElement(xmlDocument.Name.LocalName);
            xElement.Value = xmlDocument.Value;

            foreach (XAttribute attribute in xmlDocument.Attributes())
                xElement.Add(attribute);

            return xElement;
        }
        return new XElement(xmlDocument.Name.LocalName, xmlDocument.Elements().Select(el => RemoveAllNamespaces(el)));
    }
    #endregion


    protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadExistingData();
    }
}
