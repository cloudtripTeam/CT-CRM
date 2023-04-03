using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Xml;
using System.IO;
using System.Collections;
using System.Web.UI;
using System.Configuration;

/// <summary>
/// Summary description for ClassPaymentdetail
/// </summary>
public class Classfunction
{
    SqlConnection con;
    DataSet dsCompDetail;
    Byte[] keybyte;
    Byte[] hashbyte;
    string HashText;
    public Classfunction()
    {
        con = new SqlConnection();
        con.ConnectionString = getConnectionString("237");// "server=CRYSTAL-7D78575\\DATASERVER;Initial Catalog=CRYSTMS;User Id=sa;Password=admin09;Integrated Security=false;Max Pool Size=1000000; Connect Timeout=60;"; 
    }
    public DataTable GetApplicableFor()
    {
        DataSet dsApplicableFor;
        string _CommandText = string.Empty;

        try
        {
            SqlParameter[] param = new SqlParameter[1];
            _CommandText = "Get_RegForMrktCompany";

            param[0] = new SqlParameter("@paramRegForMrkt", SqlDbType.VarChar, 50);
            param[0].Value = "3517_CT";


            dsApplicableFor = SqlHelper.ExecuteDataset(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            return dsApplicableFor.Tables[0];
        }

        catch (Exception ex)
        {
            dsApplicableFor = null;
            return dsApplicableFor.Tables[0];
        }
    }

    public DataSet GetCompDetail()
    {

        dsCompDetail = new DataSet();
        SqlHelper.FillDataset(con, CommandType.StoredProcedure, "GetCompanyDetail", dsCompDetail, new string[] { "CompanyDetail" });
        return dsCompDetail;
    }


    public DataTable GetAuthorizationDetail(string User_ID, string Application_ID, int Option_ID)
    {
        DataSet dsCardDetail;
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[3];

        try
        {
            using (SqlConnection _connection = con)
            {
                _CommandText = "MoresandG_AuthorizationDetail_Get";

                if (!string.IsNullOrEmpty(User_ID))
                {
                    param[0] = new SqlParameter("@User_ID", SqlDbType.NVarChar, (50));
                    param[0].Value = User_ID;
                }
                if (!string.IsNullOrEmpty(Application_ID))
                {
                    param[1] = new SqlParameter("@Application_ID", SqlDbType.VarChar, (50));
                    param[1].Value = Application_ID;
                }
                if (Option_ID > 0)
                {
                    param[2] = new SqlParameter("@Option_ID", SqlDbType.Int);
                    param[2].Value = Option_ID;
                }
                dsCardDetail = SqlHelper.ExecuteDataset(_connection, CommandType.StoredProcedure, _CommandText, param);
                return dsCardDetail.Tables[0];
            }
        }
        catch
        {
            dsCardDetail = null;
            return dsCardDetail.Tables[0];
        }

    }





    private string getConnectionString(string server)
    {
        string connStr = "";
        try
        {

            switch (server)
            {
                case "237":

                    connStr = ConfigurationSettings.AppSettings["237"]; ;//"server=88.208.200.237;Initial Catalog=CRYSTMS;User Id=sa;Password=Moresand91;Integrated Security=false;Max Pool Size=1000000; Connect Timeout=60;";
                    break;
                case "59":
                    connStr = ConfigurationSettings.AppSettings["59"]; ; //"server=88.208.192.59;Initial Catalog=CRYSTMS;User Id=sa;Password=Moresand91;Integrated Security=false;Max Pool Size=1000000; Connect Timeout=60;";
                    break;
                case "93":
                    connStr = ConfigurationSettings.AppSettings["93"]; ; //"server=77.68.40.93;Initial Catalog=CRYSTMS;User Id=sa;Password=Moresand91;Integrated Security=false;Max Pool Size=1000000; Connect Timeout=60;";
                    break;

                case "6":
                    connStr = ConfigurationSettings.AppSettings["6"]; ; //"server=88.208.232.6;Initial Catalog=CRYSTMS;User Id=sa;Password=Moresand91;Integrated Security=false;Max Pool Size=1000000; Connect Timeout=60;";
                    break;

                case "55":
                    connStr = ConfigurationSettings.AppSettings["55"]; ; //"server=88.208.232.55;Initial Catalog=CRYSTMS;User Id=sa;Password=Moresand91;Integrated Security=false;Max Pool Size=1000000; Connect Timeout=60;";
                    break;

                case "20":
                    connStr = ConfigurationSettings.AppSettings["20"]; ; //"server=88.208.232.55;Initial Catalog=CRYSTMS;User Id=sa;Password=Moresand91;Integrated Security=false;Max Pool Size=1000000; Connect Timeout=60;";
                    break;
                //case "237":
                //    connStr = "server=CRYSTAL-7D78575\\DATASERVER;Initial Catalog=CRYSTMS;User Id=sa;Password=admin09;Integrated Security=false;Max Pool Size=1000000; Connect Timeout=60;";
                //    break;

                //case "59":
                //    connStr = "server=CRYSTAL-7D78575\\DATASERVER;Initial Catalog=CRYSTMS;User Id=sa;Password=admin09;Integrated Security=false;Max Pool Size=1000000; Connect Timeout=60;";
                //    break;

                //case "6":
                //    connStr = "server=CRYSTAL-7D78575\\DATASERVER;Initial Catalog=CRYSTMS;User Id=sa;Password=admin09;Integrated Security=false;Max Pool Size=1000000; Connect Timeout=60;";
                //    break;

                //case "55":
                //    connStr = "server=CRYSTAL-7D78575\\DATASERVER;Initial Catalog=CRYSTMS;User Id=sa;Password=admin09;Integrated Security=false;Max Pool Size=1000000; Connect Timeout=60;";
                //    break;


                default:
                    connStr = "";
                    break;
            }
        }
        catch (Exception)
        {

        }
        return connStr;
    }
    public int RandomId()
    {
        //Random rand = new Random((int)DateTime.Now.Ticks);
        Random rand = new Random();
        int RandomId;
        RandomId = rand.Next(100000, 999999);
        return RandomId;

    }



    public DataSet GetFareDetails(int iOptional)
    {
        //...... iOptional = 0  for display paymentdetails by COMP_DTL_Company_ID
        //...... iOptional = 1  for display paymentdetails by VEND_PAY_ID

        DataSet dsFareDetail = new DataSet();
        //dsCompDetail = new DataSet();
        if (iOptional == 0)
        {
            SqlHelper.FillDataset(con, CommandType.StoredProcedure, "[AirF_Markup_Get]", dsFareDetail, new string[] { "[MarkupDetail]" });
        }
        if (iOptional == 1)
        {
            SqlHelper.FillDataset(con, CommandType.StoredProcedure, "Air_FareIn_Get", dsFareDetail, new string[] { "ExcelFareDetail" });
        }

        return dsFareDetail;
    }
    public void fillComBoBox(System.Web.UI.WebControls.DropDownList cmb, DataTable dt, string text, string value)
    {
        cmb.DataSource = dt;
        cmb.DataTextField = text;
        cmb.DataValueField = value;
        cmb.DataBind();
    }
    public int InsertAirFareMarkup(string from, string to, string airV, string provider, string category, string _class,
       string journey, double amount, string amountType, string tourCode, DateTime validFromDate, DateTime validToDate,
       string applicableFor, string modifiedBy, DateTime TravelStartdate, DateTime TravelEndDate)
    {
        //-----------------code of multiple transaction-----------------
        int effected = 0;
        //using (SqlConnection con1 = new SqlConnection(getConnectionString("237")))
        //using (SqlConnection con2 = new SqlConnection(getConnectionString("59")))
        //using (SqlConnection con3 = new SqlConnection(getConnectionString("6")))
        //using (SqlConnection con4 = new SqlConnection(getConnectionString("55")))
        using (SqlConnection con5 = new SqlConnection(getConnectionString("20")))
        {
            try
            {
                //con1.Open();
                //con2.Open();
                //con3.Open();
                //con4.Open();
                con5.Open();
                //SqlTransaction tran1 = con1.BeginTransaction();
                //SqlTransaction tran2 = con2.BeginTransaction();
                //SqlTransaction tran3 = con3.BeginTransaction();
                //SqlTransaction tran4 = con4.BeginTransaction();
                SqlTransaction tran5 = con5.BeginTransaction();

                try
                {
                    //effected = SaveTransactionData(from, to, airV, provider, category, _class, journey, amount, amountType, tourCode,
                    //        validFromDate, validToDate, applicableFor, modifiedBy, TravelStartdate, TravelEndDate, con1, tran1);

                    //effected = SaveTransactionData(from, to, airV, provider, category, _class, journey, amount, amountType, tourCode,
                    //         validFromDate, validToDate, applicableFor, modifiedBy, TravelStartdate, TravelEndDate, con2, tran2);

                    //effected = SaveTransactionData(from, to, airV, provider, category, _class, journey, amount, amountType, tourCode,
                    //    validFromDate, validToDate, applicableFor, modifiedBy, TravelStartdate, TravelEndDate, con3, tran3);

                    //effected = SaveTransactionData(from, to, airV, provider, category, _class, journey, amount, amountType, tourCode,
                    //    validFromDate, validToDate, applicableFor, modifiedBy, TravelStartdate, TravelEndDate, con4, tran4);
                    effected = SaveTransactionData(from, to, airV, provider, category, _class, journey, amount, amountType, tourCode,
                       validFromDate, validToDate, applicableFor, modifiedBy, TravelStartdate, TravelEndDate, con5, tran5);



                    //tran1.Save("savepoint1");
                    //tran2.Save("savepoint2");
                    //tran3.Save("savepoint3");
                    //tran4.Save("savepoint4");
                    tran5.Save("savepoint5");
                    //tran1.Commit();
                    //tran2.Commit();
                    //tran3.Commit();
                    //tran4.Commit();
                    tran5.Commit();
                }
                catch (Exception ex)
                {
                    //tran1.Rollback();
                    //tran2.Rollback();
                    //tran3.Rollback();
                    //tran4.Rollback();
                    tran5.Rollback();
                }
            }
            catch (Exception)
            {
                // error handling for connection failure
            }
            finally
            {
                //con1.Close();
                //con2.Close();
                //con3.Close();
                //con4.Close();
                con5.Close();
            }
        }


        return effected;

    }


    public DataSet GetAirF_Markup(string from, string to, string airV, string provider, string category, string serclass, string serMarkupFrom,
        string serMarkupTo, string serAmountType, string tourCode, string servalidFromDate, string servalidtoDate,
        string serExpiryFromDate, string serExpiryToDate, string applicableFor, string modifiedBy, string Jtype)
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        try
        {

            string M_str_sqlcon = getConnectionString("20");
            if (!string.IsNullOrEmpty(M_str_sqlcon))
            {
                using (SqlConnection _objConnection = new SqlConnection(M_str_sqlcon))
                {
                    try
                    {
                        SqlParameter[] arrSqlParam = new SqlParameter[17];
                        if (!String.IsNullOrEmpty(from))
                        {
                            arrSqlParam[0] = new SqlParameter("@paramFrom", from);
                        }
                        if (!String.IsNullOrEmpty(to))
                        {
                            arrSqlParam[1] = new SqlParameter("@paramTo", to);
                        }
                        if (!String.IsNullOrEmpty(airV))
                        {
                            arrSqlParam[2] = new SqlParameter("@paramAirV", airV);
                        }
                        if (!String.IsNullOrEmpty(provider))
                        {
                            arrSqlParam[3] = new SqlParameter("@paramProvider", provider);
                        }
                        if (!String.IsNullOrEmpty(category))
                        {
                            arrSqlParam[4] = new SqlParameter("@paramCategory", category);
                        }
                        if (!String.IsNullOrEmpty(serclass))
                        {
                            arrSqlParam[5] = new SqlParameter("@paramClass", serclass);
                        }
                        if (!String.IsNullOrEmpty(serMarkupFrom))
                        {
                            arrSqlParam[6] = new SqlParameter("@paramAmountFrom", serMarkupFrom);
                        }

                        if (!String.IsNullOrEmpty(serMarkupTo))
                        {
                            arrSqlParam[7] = new SqlParameter("@paramAmountTo", serMarkupTo);
                        }
                        if (!String.IsNullOrEmpty(serAmountType))
                        {
                            arrSqlParam[8] = new SqlParameter("@paramAmountType", serAmountType);
                        }
                        if (!String.IsNullOrEmpty(Jtype))
                        {
                            arrSqlParam[9] = new SqlParameter("@paramJourney", Jtype);
                        }

                        if (!String.IsNullOrEmpty(tourCode))
                        {
                            arrSqlParam[10] = new SqlParameter("@paramTourCode", tourCode);
                        }
                        if (!String.IsNullOrEmpty(servalidFromDate))
                        {
                            arrSqlParam[11] = new SqlParameter("@paramValidFromDate", Convert.ToDateTime(servalidFromDate).ToShortDateString());
                        }
                        if (!String.IsNullOrEmpty(servalidtoDate))
                        {
                            arrSqlParam[12] = new SqlParameter("@paramValidToDate", Convert.ToDateTime(servalidtoDate).ToShortDateString());
                        }
                        if (!String.IsNullOrEmpty(serExpiryFromDate))
                        {
                            arrSqlParam[13] = new SqlParameter("@paramExpiryFromDate", Convert.ToDateTime(serExpiryFromDate));
                        }
                        if (!String.IsNullOrEmpty(serExpiryToDate))
                        {
                            arrSqlParam[14] = new SqlParameter("@paramExpiryToDate", Convert.ToDateTime(serExpiryToDate));
                        }
                        if (!String.IsNullOrEmpty(applicableFor))
                        {
                            arrSqlParam[15] = new SqlParameter("@paramApplicableFor", applicableFor);
                        }

                        int _top = 10000;
                        if (_top != 0)
                        {
                            arrSqlParam[16] = new SqlParameter("@paramTop", _top);
                        }

                        ds = SqlHelper.ExecuteDataset(_objConnection, CommandType.StoredProcedure, "AirF_Markup_Get", arrSqlParam);

                        if (ds.Tables.Count > 0)
                        {

                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                return ds;
                            }
                            else
                            {
                                ds = null;
                            }
                        }
                        else
                        {
                            ds = null;
                        }

                        //_objConnection.Open();
                        //_objCommand.ExecuteNonQuery();
                        //isSuccess = true;


                    }
                    catch (Exception ex)
                    {
                        return ds = null;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            return ds = null;
        }
        return ds;
    }



    public DataSet GetAirF_Markup_Excel(string from, string to, string airV, string provider, string category, string serclass, string serMarkupFrom,
        string serMarkupTo, string serAmountType, string tourCode, string servalidFromDate, string servalidtoDate,
        string serExpiryFromDate, string serExpiryToDate, string applicableFor, string modifiedBy, string Jtype)
    {
        try
        {
            using (SqlConnection _objConnection = DataConnection.GetConnectionMarkup())
            {
                SqlParameter[] arrSqlParam = new SqlParameter[17];
                if (!String.IsNullOrEmpty(from))
                {
                    arrSqlParam[0] = new SqlParameter("@paramFrom", from);
                }
                if (!String.IsNullOrEmpty(to))
                {
                    arrSqlParam[1] = new SqlParameter("@paramTo", to);
                }
                if (!String.IsNullOrEmpty(airV))
                {
                    arrSqlParam[2] = new SqlParameter("@paramAirV", airV);
                }
                if (!String.IsNullOrEmpty(provider))
                {
                    arrSqlParam[3] = new SqlParameter("@paramProvider", provider);
                }
                if (!String.IsNullOrEmpty(category))
                {
                    arrSqlParam[4] = new SqlParameter("@paramCategory", category);
                }
                if (!String.IsNullOrEmpty(serclass))
                {
                    arrSqlParam[5] = new SqlParameter("@paramClass", serclass);
                }
                if (!String.IsNullOrEmpty(serMarkupFrom))
                {
                    arrSqlParam[6] = new SqlParameter("@paramAmountFrom", serMarkupFrom);
                }

                if (!String.IsNullOrEmpty(serMarkupTo))
                {
                    arrSqlParam[7] = new SqlParameter("@paramAmountTo", serMarkupTo);
                }
                if (!String.IsNullOrEmpty(serAmountType))
                {
                    arrSqlParam[8] = new SqlParameter("@paramAmountType", serAmountType);
                }
                if (!String.IsNullOrEmpty(Jtype))
                {
                    arrSqlParam[9] = new SqlParameter("@paramJourney", Jtype);
                }

                if (!String.IsNullOrEmpty(tourCode))
                {
                    arrSqlParam[10] = new SqlParameter("@paramTourCode", tourCode);
                }
                if (!String.IsNullOrEmpty(servalidFromDate))
                {
                    arrSqlParam[11] = new SqlParameter("@paramValidFromDate", Convert.ToDateTime(servalidFromDate).ToShortDateString());
                }
                if (!String.IsNullOrEmpty(servalidtoDate))
                {
                    arrSqlParam[12] = new SqlParameter("@paramValidToDate", Convert.ToDateTime(servalidtoDate).ToShortDateString());
                }
                if (!String.IsNullOrEmpty(serExpiryFromDate))
                {
                    arrSqlParam[13] = new SqlParameter("@paramExpiryFromDate", Convert.ToDateTime(serExpiryFromDate));
                }
                if (!String.IsNullOrEmpty(serExpiryToDate))
                {
                    arrSqlParam[14] = new SqlParameter("@paramExpiryToDate", Convert.ToDateTime(serExpiryToDate));
                }
                if (!String.IsNullOrEmpty(applicableFor))
                {
                    arrSqlParam[15] = new SqlParameter("@paramApplicableFor", applicableFor);
                }

                int _top = 10000;
                if (_top != 0)
                {
                    arrSqlParam[16] = new SqlParameter("@paramTop", _top);
                }
                return SqlHelper.ExecuteDataset(_objConnection, CommandType.StoredProcedure, "AirF_Markup_GetExcel", arrSqlParam);
            }
        }
        catch (Exception ex)
        {
            return null;
        }

    }





    public int UpdateAirFareMarkup(string RecID, string from, string to, string airV, string provider, string category, string _class,
     string journey, string amount, string amountType, string tourCode, DateTime validFromDate, DateTime validToDate, DateTime TravelStartDate,
     DateTime TravelEndDate, string applicableFor, string modifiedBy)
    {

        //-----------------code of multiple transaction-----------------
        int effected = 0;
        //using (SqlConnection con1 = new SqlConnection(getConnectionString("237")))
        //using (SqlConnection con2 = new SqlConnection(getConnectionString("59")))
        //using (SqlConnection con3 = new SqlConnection(getConnectionString("6")))
        //using (SqlConnection con4 = new SqlConnection(getConnectionString("55")))
        using (SqlConnection con5 = new SqlConnection(getConnectionString("20")))
        {
            try
            {
                //con1.Open();
                //con2.Open();
                //con3.Open();
                //con4.Open();
                con5.Open();
                //SqlTransaction tran1 = con1.BeginTransaction();
                //SqlTransaction tran2 = con2.BeginTransaction();
                //SqlTransaction tran3 = con3.BeginTransaction();
                //SqlTransaction tran4 = con4.BeginTransaction();
                SqlTransaction tran5 = con5.BeginTransaction();

                try
                {
          //          effected = UpdateAirFareMarkupUsingTrans(RecID, from, to, airV, provider, category, _class,
          //journey, amount, amountType, tourCode, validFromDate, validToDate, TravelStartDate,
          //TravelEndDate, applicableFor, modifiedBy, con1, tran1);

          //          effected = UpdateAirFareMarkupUsingTrans(RecID, from, to, airV, provider, category, _class,
          //journey, amount, amountType, tourCode, validFromDate, validToDate, TravelStartDate,
          //TravelEndDate, applicableFor, modifiedBy, con2, tran2);

          //          effected = UpdateAirFareMarkupUsingTrans(RecID, from, to, airV, provider, category, _class,
          //journey, amount, amountType, tourCode, validFromDate, validToDate, TravelStartDate,
          //TravelEndDate, applicableFor, modifiedBy, con3, tran3);

          //          effected = UpdateAirFareMarkupUsingTrans(RecID, from, to, airV, provider, category, _class,
          //journey, amount, amountType, tourCode, validFromDate, validToDate, TravelStartDate,
          //TravelEndDate, applicableFor, modifiedBy, con4, tran4);
                    effected = UpdateAirFareMarkupUsingTrans(RecID, from, to, airV, provider, category, _class,
     journey, amount, amountType, tourCode, validFromDate, validToDate, TravelStartDate,
     TravelEndDate, applicableFor, modifiedBy, con5, tran5);



                    //tran1.Save("savepoint1");
                    //tran2.Save("savepoint2");
                    //tran3.Save("savepoint3");
                    //tran4.Save("savepoint4");
                    tran5.Save("savepoint5");
                    //tran1.Commit();
                    //tran2.Commit();
                    //tran3.Commit();
                    //tran4.Commit();
                    tran5.Commit();
                }
                catch (Exception)
                {
                    //tran1.Rollback();
                    //tran2.Rollback();
                    //tran3.Rollback();
                    //tran4.Rollback();
                    tran5.Rollback();
                    effected = -1;
                    Page page = HttpContext.Current.CurrentHandler as Page;
                    page.ClientScript.RegisterStartupScript(this.GetType(), "ch", "<script>alert('Mismatch In Key So Transaction had been Roll Back')</script>");

                }
            }
            catch (Exception)
            {

            }
            finally
            {
                //con1.Close();
                //con2.Close();
                //con3.Close();
                //con4.Close();
                con5.Close();
            }
        }


        return effected;

    }

    public int DeleteAll_AirFareMarkup(ArrayList recIDs)
    {
        int effected = 0;
        //using (SqlConnection con1 = new SqlConnection(getConnectionString("237")))
        //using (SqlConnection con2 = new SqlConnection(getConnectionString("59")))
        //using (SqlConnection con3 = new SqlConnection(getConnectionString("6")))
        //using (SqlConnection con4 = new SqlConnection(getConnectionString("55")))
        using (SqlConnection con5 = new SqlConnection(getConnectionString("20")))
        {
            try
            {
                //con1.Open();
                //con2.Open();
                //con3.Open();
                //con4.Open();
                con5.Open();
                //SqlTransaction tran1 = con1.BeginTransaction();
                //SqlTransaction tran2 = con2.BeginTransaction();
                //SqlTransaction tran3 = con3.BeginTransaction();
                //SqlTransaction tran4 = con4.BeginTransaction();
                SqlTransaction tran5 = con5.BeginTransaction();

                try
                {
                    //effected = DeleteAll_AirFareMarkupUsingTrans(recIDs, con1, tran1);

                    //effected = DeleteAll_AirFareMarkupUsingTrans(recIDs, con2, tran2);

                    //effected = DeleteAll_AirFareMarkupUsingTrans(recIDs, con3, tran3);

                    //effected = DeleteAll_AirFareMarkupUsingTrans(recIDs, con4, tran4);
                    effected = DeleteAll_AirFareMarkupUsingTrans(recIDs, con5, tran5);
                    if (effected == 0)
                    {
                        //tran1.Rollback();
                        //tran2.Rollback();
                        //tran3.Rollback();
                        //tran4.Rollback();
                        tran5.Rollback();
                        Page page = HttpContext.Current.CurrentHandler as Page;
                        page.ClientScript.RegisterStartupScript(this.GetType(), "ch", "<script>alert('Mismatch Key Id  In All Server')</script>");

                        return 0;
                    }

                    //tran1.Save("savepoint1");
                    //tran2.Save("savepoint2");
                    //tran3.Save("savepoint3");
                    //tran4.Save("savepoint4");
                    tran5.Save("savepoint5");
                    //tran1.Commit();
                    //tran2.Commit();
                    //tran3.Commit();
                    //tran4.Commit();
                    tran5.Commit();
                }
                catch (Exception ex)
                {
                    //tran1.Rollback();
                    //tran2.Rollback();
                    //tran3.Rollback();
                    //tran4.Rollback();
                    tran5.Rollback();
                    effected = -1;
                    Page page = HttpContext.Current.CurrentHandler as Page;
                    page.ClientScript.RegisterStartupScript(this.GetType(), "ch", "<script>alert('Mismatch In Key So Transaction had been Roll Back')</script>");

                }
            }
            catch (Exception)
            {
                // error handling for connection failure
            }
            finally
            {
                //con1.Close();
                //con2.Close();
                //con3.Close();
                //con4.Close();
                con5.Close();
            }
        }


        return effected;

    }


    public void releaseObject(object obj)
    {
        try
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
            obj = null;
        }
        catch (Exception ex)
        {
            obj = null;
            Page page = HttpContext.Current.CurrentHandler as Page;
            page.ClientScript.RegisterStartupScript(this.GetType(), "ch", "<script>alert('Exception Occured while releasing object')</script>");

        }
        finally
        {
            GC.Collect();
        }
    }
    public int SaveTransactionData(string from, string to, string airV, string provider, string category, string _class,
      string journey, double amount, string amountType, string tourCode, DateTime validFromDate, DateTime validToDate,
      string applicableFor, string modifiedBy, DateTime TravelStartdate, DateTime TravelEndDate, SqlConnection _objConnection, SqlTransaction tranNo)
    {
        int effected = 0;
        using (SqlCommand _objCommand = new SqlCommand())
        {
            _objCommand.CommandType = CommandType.StoredProcedure;
            _objCommand.CommandText = "AirFareMarkup_Insert";
            _objCommand.Connection = _objConnection;
            _objCommand.Transaction = tranNo;
            _objCommand.Parameters.AddWithValue("@paramFrom", from);
            _objCommand.Parameters.AddWithValue("@paramTo", to);
            _objCommand.Parameters.AddWithValue("@paramAirV", airV);
            _objCommand.Parameters.AddWithValue("@paramProvider", provider);
            _objCommand.Parameters.AddWithValue("@paramCategory", category);
            _objCommand.Parameters.AddWithValue("@paramClass", _class);
            _objCommand.Parameters.AddWithValue("@paramJourney", journey);
            _objCommand.Parameters.AddWithValue("@paramAmount", amount);
            _objCommand.Parameters.AddWithValue("@paramAmountType", amountType);
            _objCommand.Parameters.AddWithValue("@paramTourCode", tourCode);
            _objCommand.Parameters.AddWithValue("@paramValidFromDate", validFromDate);
            _objCommand.Parameters.AddWithValue("@paramValidToDate", validToDate);
            _objCommand.Parameters.AddWithValue("@paramTravelStartDate", TravelStartdate);
            _objCommand.Parameters.AddWithValue("@paramTravelEndDate", TravelEndDate);
            _objCommand.Parameters.AddWithValue("@paramApplicableFor", applicableFor);
            _objCommand.Parameters.AddWithValue("@paramModifiedBy", modifiedBy);
            //_objConnection.Open();
            effected = _objCommand.ExecuteNonQuery();
        }


        return effected;
    }
    public int DeleteAll_AirFareMarkupUsingTrans(ArrayList recIDs, SqlConnection _objConnection, SqlTransaction tranNo)
    {
        int count = 0;
        foreach (string id in recIDs)
        {
            using (SqlCommand _objCommand = new SqlCommand())
            {
                _objCommand.CommandType = CommandType.StoredProcedure;
                _objCommand.CommandText = "AirFareMarkup_Delete";
                _objCommand.Connection = _objConnection;
                _objCommand.Transaction = tranNo;
                _objCommand.Parameters.AddWithValue("@paramRecID", id);
                count = _objCommand.ExecuteNonQuery();
            }

        }
        return count;

    }
    public int UpdateAirFareMarkupUsingTrans(string RecID, string from, string to, string airV, string provider, string category, string _class,
    string journey, string amount, string amountType, string tourCode, DateTime validFromDate, DateTime validToDate, DateTime TravelStartDate,
    DateTime TravelEndDate, string applicableFor, string modifiedBy, SqlConnection _objConnection, SqlTransaction tranNo)
    {
        int effected = 0;

        using (SqlCommand _objCommand = new SqlCommand())
        {

            _objCommand.CommandType = CommandType.StoredProcedure;
            _objCommand.CommandText = "AirFareMarkup_Update";
            _objCommand.Connection = _objConnection;
            _objCommand.Transaction = tranNo;
            _objCommand.Parameters.AddWithValue("@paramRecID", RecID);
            _objCommand.Parameters.AddWithValue("@paramFrom", from);
            _objCommand.Parameters.AddWithValue("@paramTo", to);
            _objCommand.Parameters.AddWithValue("@paramAirV", airV);
            _objCommand.Parameters.AddWithValue("@paramProvider", provider);
            _objCommand.Parameters.AddWithValue("@paramCategory", category);
            _objCommand.Parameters.AddWithValue("@paramClass", _class);
            _objCommand.Parameters.AddWithValue("@paramJourney", journey);
            _objCommand.Parameters.AddWithValue("@paramAmount", amount);
            _objCommand.Parameters.AddWithValue("@paramAmountType", amountType);
            _objCommand.Parameters.AddWithValue("@paramTourCode", tourCode);
            _objCommand.Parameters.AddWithValue("@paramValidFromDate", validFromDate);
            _objCommand.Parameters.AddWithValue("@paramValidToDate", validToDate);
            _objCommand.Parameters.AddWithValue("@paramApplicableFor", applicableFor);
            _objCommand.Parameters.AddWithValue("@paramTravelStartDate", TravelStartDate);
            _objCommand.Parameters.AddWithValue("@paramTravelEndDate", TravelEndDate);
            _objCommand.Parameters.AddWithValue("@paramModifiedBy", modifiedBy);
            // _objConnection.Open();
            effected = _objCommand.ExecuteNonQuery();

        }
        return effected;

    }

    public DataSet GetSeason()
    {
        DataSet ds = new DataSet();

        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            _CommandText = "getSeasonName";
            param[0] = new SqlParameter("@Counter", SqlDbType.Int);
            param[0].Value = "1";
            ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, _CommandText, param);
            return ds;
        }

        catch (Exception ex)
        {
            ds = null;
            return ds;
        }
    }
    public int SaveSeason(string SeasonName, DateTime TravelStartdate, DateTime TravelEndDate)
    {
        int effected = 0;
        using (SqlCommand _objCommand = new SqlCommand())
        {
            _objCommand.CommandType = CommandType.StoredProcedure;
            _objCommand.CommandText = "getSeasonName";
            _objCommand.Connection = con;
            _objCommand.Parameters.AddWithValue("@paramSeasonName", SeasonName);
            _objCommand.Parameters.AddWithValue("@paramTravelStartDate", TravelStartdate);
            _objCommand.Parameters.AddWithValue("@paramTarvelEndDate", TravelEndDate);
            _objCommand.Parameters.AddWithValue("@Counter", "2");
            if (_objCommand.Connection.State == ConnectionState.Closed)
            {
                _objCommand.Connection.Open();
                effected = _objCommand.ExecuteNonQuery();
                _objCommand.Connection.Close();
            }
            else
            {
                _objCommand.Connection.Close();
                _objCommand.Connection.Open();
                effected = _objCommand.ExecuteNonQuery();
                _objCommand.Connection.Close();
            }
        }
        return effected;
    }

    public int UpdateSeason(string SeasonName, DateTime TravelStartdate, DateTime TravelEndDate, String SeasonIDs)
    {
        int effected = 0;
        using (SqlCommand _objCommand = new SqlCommand())
        {
            _objCommand.CommandType = CommandType.StoredProcedure;
            _objCommand.CommandText = "getSeasonName";
            _objCommand.Connection = con;
            _objCommand.Parameters.AddWithValue("@paramSeasonName", SeasonName);
            _objCommand.Parameters.AddWithValue("@paramTravelStartDate", TravelStartdate);
            _objCommand.Parameters.AddWithValue("@paramTarvelEndDate", TravelEndDate);
            _objCommand.Parameters.AddWithValue("@paramSeasonID", SeasonIDs);
            _objCommand.Parameters.AddWithValue("@Counter", "4");
            if (_objCommand.Connection.State == ConnectionState.Closed)
            {
                _objCommand.Connection.Open();
                effected = _objCommand.ExecuteNonQuery();
                _objCommand.Connection.Close();
            }
        }
        return effected;
    }
    public int DeleteSeason(ArrayList SeasonIDs)
    {
        int count = 0;

        foreach (int id in SeasonIDs)
        {


            using (SqlCommand _objCommand = new SqlCommand())
            {
                _objCommand.CommandType = CommandType.StoredProcedure;
                _objCommand.CommandText = "getSeasonName";
                _objCommand.Connection = con;
                _objCommand.Parameters.AddWithValue("@paramSeasonID", id);
                _objCommand.Parameters.AddWithValue("@Counter", "3");
                if (_objCommand.Connection.State == ConnectionState.Closed)
                {
                    _objCommand.Connection.Open();
                    count = _objCommand.ExecuteNonQuery();
                    _objCommand.Connection.Close();
                }
            }
        }
        return count;

    }

    #region AddLoungePassMarkup

    public DataTable ViewLoungePassMarkup(string LPM_Id, string LPM_Dest, string LPM_Provider, string LPM_Markup, string LPM_MarkupTO, string LPM_TravelStartDate, string LPM_TravelEndDate, string LPM_ValidFrom, string LPM_ValidTo, string LPM_ModifyDate, string LPM_ModifyDateTo, int counter)
    {
        DataSet dsMarkup;
        string _CommandText = "sp_LoungePassMarkup";
        SqlParameter[] param = new SqlParameter[12];
        try
        {
            if (!string.IsNullOrEmpty(LPM_Id))
            {
                param[0] = new SqlParameter("@paramLPM_Id", SqlDbType.Int);
                param[0].Value = Convert.ToInt32(LPM_Id);
            }
            if (!string.IsNullOrEmpty(LPM_Dest))
            {
                param[1] = new SqlParameter("@paramLPM_Dest", SqlDbType.VarChar);
                param[1].Value = LPM_Dest;
            }
            if (!string.IsNullOrEmpty(LPM_Provider))
            {
                param[2] = new SqlParameter("@paramLPM_Provider", SqlDbType.VarChar);
                param[2].Value = LPM_Provider;
            }
            if (!string.IsNullOrEmpty(LPM_Markup))
            {
                param[3] = new SqlParameter("@paramLPM_Markup", SqlDbType.Money);
                param[3].Value = Convert.ToDouble(LPM_Markup);
            }
            if (!string.IsNullOrEmpty(LPM_MarkupTO))
            {
                param[4] = new SqlParameter("@paramLPM_MarkupTo", SqlDbType.Money);
                param[4].Value = Convert.ToDouble(LPM_MarkupTO);
            }
            if (!string.IsNullOrEmpty(LPM_TravelStartDate))
            {
                param[5] = new SqlParameter("@paramLPM_TravelStartDate", SqlDbType.DateTime);
                param[5].Value = Convert.ToDateTime(LPM_TravelStartDate);
            }
            if (!string.IsNullOrEmpty(LPM_TravelEndDate))
            {
                param[6] = new SqlParameter("@paramLPM_TravelEndDate", SqlDbType.DateTime);
                param[6].Value = Convert.ToDateTime(LPM_TravelEndDate);
            }
            if (!string.IsNullOrEmpty(LPM_ValidFrom))
            {
                param[7] = new SqlParameter("@paramLPM_ValidFrom", SqlDbType.DateTime);
                param[7].Value = Convert.ToDateTime(LPM_ValidFrom);
            }
            if (!string.IsNullOrEmpty(LPM_ValidTo))
            {
                param[8] = new SqlParameter("@paramLPM_ValidTo", SqlDbType.DateTime);
                param[8].Value = Convert.ToDateTime(LPM_ValidTo);
            }

            if (!string.IsNullOrEmpty(LPM_ModifyDate))
            {
                param[9] = new SqlParameter("@paramLPM_ModifyDate", SqlDbType.DateTime);
                param[9].Value = Convert.ToDateTime(LPM_ModifyDate);
            }

            if (!string.IsNullOrEmpty(LPM_ModifyDateTo))
            {
                param[10] = new SqlParameter("@paramLPM_ModifyDateTo", SqlDbType.DateTime);
                param[10].Value = Convert.ToDateTime(LPM_ModifyDateTo);
            }
            param[11] = new SqlParameter("@Counter", SqlDbType.Int);
            param[11].Value = counter;

            dsMarkup = SqlHelper.ExecuteDataset(DataConnection.GetConnectionMarkup(), CommandType.StoredProcedure, _CommandText, param);
            return dsMarkup.Tables[0];
        }

        catch (Exception ex)
        {
            dsMarkup = null;
            return dsMarkup.Tables[0];
        }


    }
    public bool LoungePassMarkup_Insert(string LPM_Dest, string LPM_Provider, string LPM_Markup, string LPM_TravelStartDate, string LPM_TravelEndDate, string LPM_ValidFrom, string LPM_ValidTo, string LPM_ModifyBy, int counter)
    {
        string _CommandText = "sp_LoungePassMarkup";
        SqlParameter[] param = new SqlParameter[9];
        try
        {

            if (!string.IsNullOrEmpty(LPM_Dest))
            {
                param[0] = new SqlParameter("@paramLPM_Dest", SqlDbType.VarChar);
                param[0].Value = LPM_Dest;
            }
            if (!string.IsNullOrEmpty(LPM_Provider))
            {
                param[1] = new SqlParameter("@paramLPM_Provider", SqlDbType.VarChar);
                param[1].Value = LPM_Provider;
            }
            if (!string.IsNullOrEmpty(LPM_Markup))
            {
                param[2] = new SqlParameter("@paramLPM_Markup", SqlDbType.Money);
                param[2].Value = Convert.ToDouble(LPM_Markup);
            }

            if (!string.IsNullOrEmpty(LPM_TravelStartDate))
            {
                param[3] = new SqlParameter("@paramLPM_TravelStartDate", SqlDbType.DateTime);
                param[3].Value = Convert.ToDateTime(LPM_TravelStartDate);
            }
            if (!string.IsNullOrEmpty(LPM_TravelEndDate))
            {
                param[4] = new SqlParameter("@paramLPM_TravelEndDate", SqlDbType.DateTime);
                param[4].Value = Convert.ToDateTime(LPM_TravelEndDate);
            }
            if (!string.IsNullOrEmpty(LPM_ValidFrom))
            {
                param[5] = new SqlParameter("@paramLPM_ValidFrom", SqlDbType.DateTime);
                param[5].Value = Convert.ToDateTime(LPM_ValidFrom);
            }
            if (!string.IsNullOrEmpty(LPM_ValidTo))
            {
                param[6] = new SqlParameter("@paramLPM_ValidTo", SqlDbType.DateTime);
                param[6].Value = Convert.ToDateTime(LPM_ValidTo);
            }

            if (!string.IsNullOrEmpty(LPM_ModifyBy))
            {
                param[7] = new SqlParameter("@paramLPM_ModifyBy", SqlDbType.VarChar);
                param[7].Value = LPM_ModifyBy;
            }


            param[8] = new SqlParameter("@Counter", SqlDbType.Int);
            param[8].Value = counter;

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionMarkup(), CommandType.StoredProcedure, _CommandText, param);
            if (count > 0)
                return true;
            else
                return false;

        }
        catch { return false; }
    }

    public bool LoungePassMarkup_Delete(string LPM_Id, int counter)
    {
        string _CommandText = "sp_LoungePassMarkup";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            if (!string.IsNullOrEmpty(LPM_Id))
            {
                param[0] = new SqlParameter("@paramLPM_Id", SqlDbType.Int);
                param[0].Value = Convert.ToInt32(LPM_Id);
            }

            param[1] = new SqlParameter("@Counter", SqlDbType.Int);
            param[1].Value = counter;

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionMarkup(), CommandType.StoredProcedure, _CommandText, param);
            if (count > 0)
                return true;
            else
                return false;

        }
        catch { return false; }
    }


    #endregion
}

