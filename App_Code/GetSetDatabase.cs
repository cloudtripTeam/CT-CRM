using FandHServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for GetSetDatabase
/// </summary>
public class GetSetDatabase
{

    private static Binding binding = (Binding)new BasicHttpBinding();
    private static EndpointAddress endpointAddress = new EndpointAddress("http://dataservice.cloudtrip.us/FandHServices.svc?wsdl");
    private FandHServicesClient client = new FandHServicesClient(GetSetDatabase.binding, GetSetDatabase.endpointAddress);

    public static DataTable GetAutoCompleteAirport(string Prefix)
    {
        return new FandHServicesClient((Binding)new BasicHttpBinding(), new EndpointAddress("http://dataservice.cloudtrip.us/FandHServices.svc?wsdl")).GetAutoCompleteAirport(Prefix);
    }
    public string GenerateIDs(string _prefix)
    {
        return this.client.GenerateIDs(_prefix);
    }




    public string SET_Transaction_Details(string BookingID, string TrnsNo, string Card_No, string Holder_Name, string Exp_Date, string Valid_From, string Issue_No, string Security_Code, string Card_Type, string Country, string Couty_State, string City, string Post_Code, string Address, string Card_Charges, string Charges_Type, string ModifiedBy, string Counter)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[19];

            if (!string.IsNullOrEmpty(BookingID))
            {
                param[0] = new SqlParameter("@ParamBookingID", SqlDbType.VarChar, 50);
                param[0].Value = BookingID;
            }
            if (!string.IsNullOrEmpty(TrnsNo))
            {
                param[1] = new SqlParameter("@ParamTrnsNo", SqlDbType.VarChar, 50);
                param[1].Value = TrnsNo;
            }
            if (!string.IsNullOrEmpty(Card_No))
            {
                param[2] = new SqlParameter("@ParamCard_No", SqlDbType.VarChar, 100);
                param[2].Value = Card_No;
            }
            if (!string.IsNullOrEmpty(Holder_Name))
            {
                param[3] = new SqlParameter("@ParamHolder_Name", SqlDbType.VarChar, 200);
                param[3].Value = Holder_Name;
            }
            if (!string.IsNullOrEmpty(Exp_Date))
            {
                param[4] = new SqlParameter("@ParamExp_Date", SqlDbType.VarChar, 50);
                param[4].Value = Exp_Date;
            }
            if (!string.IsNullOrEmpty(Valid_From))
            {
                param[5] = new SqlParameter("@ParamValid_From", SqlDbType.VarChar, 50);
                param[5].Value = Valid_From;
            }
            if (!string.IsNullOrEmpty(Issue_No))
            {
                param[6] = new SqlParameter("@ParamIssue_No", SqlDbType.VarChar, 100);
                param[6].Value = Issue_No;
            }
            if (!string.IsNullOrEmpty(Security_Code))
            {
                param[7] = new SqlParameter("@ParamSecurity_Code", SqlDbType.VarChar, 100);
                param[7].Value = Security_Code;
            }
            if (!string.IsNullOrEmpty(Card_Type))
            {
                param[8] = new SqlParameter("@ParamCard_Type", SqlDbType.VarChar, 200);
                param[8].Value = Card_Type;
            }
            if (!string.IsNullOrEmpty(Country))
            {
                param[9] = new SqlParameter("@ParamCountry", SqlDbType.VarChar, 200);
                param[9].Value = Country;
            }
            if (!string.IsNullOrEmpty(Couty_State))
            {
                param[10] = new SqlParameter("@ParamCouty_State", SqlDbType.VarChar, 200);
                param[10].Value = Couty_State;
            }
            if (!string.IsNullOrEmpty(City))
            {
                param[11] = new SqlParameter("@ParamCity", SqlDbType.VarChar, 200);
                param[11].Value = City;
            }
            if (!string.IsNullOrEmpty(Post_Code))
            {
                param[12] = new SqlParameter("@ParamPost_Code", SqlDbType.VarChar, 50);
                param[12].Value = Post_Code;
            }
            if (!string.IsNullOrEmpty(Address))
            {
                param[13] = new SqlParameter("@ParamAddress", SqlDbType.VarChar, 1000);
                param[13].Value = Address;
            }
            if (!string.IsNullOrEmpty(Card_Charges))
            {
                param[14] = new SqlParameter("@ParamCard_Charges", SqlDbType.Money);
                param[14].Value = Convert.ToDouble(Card_Charges);
            }
            if (!string.IsNullOrEmpty(Charges_Type))
            {
                param[15] = new SqlParameter("@ParamCharges_Type", SqlDbType.VarChar, 50);
                param[15].Value = Charges_Type;
            }
            if (!string.IsNullOrEmpty(ModifiedBy))
            {
                param[16] = new SqlParameter("@ParamModifiedBy", SqlDbType.VarChar, 50);
                param[16].Value = ModifiedBy;
            }
            param[17] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
            param[17].Value = Counter;

            param[18] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 500);
            param[18].Direction = ParameterDirection.Output;

            using (SqlConnection conection = DataConnection.GetConnection())
            {
                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_Transaction_Details", param);
                return param[18].Value.ToString();
            }
        }
        catch
        {
            return "false";
        }
    }

    public System.Data.DataTable GET_Transaction_Details(string BookingID, string TrnsNo)
    {
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(BookingID))
                {
                    param[0] = new SqlParameter("@ParamBookingID", SqlDbType.VarChar, 50);
                    param[0].Value = BookingID;
                }
                if (!string.IsNullOrEmpty(TrnsNo))
                {
                    param[1] = new SqlParameter("@ParamTrnsNo", SqlDbType.VarChar, 50);
                    param[1].Value = TrnsNo;
                }
                param[2] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[2].Value = "Select";

                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_Transaction_Details", param);
                return ds.Tables[0];
            }
        }
        catch
        {
            return null;
        }
    }



    #region booking Flow

    public bool Get_Set_Doc_Upload(string DocType,string DocPath,string BookingId)
    {
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(BookingId))
                {
                    param[0] = new SqlParameter("@ParamBookingID", SqlDbType.VarChar, 50);
                    param[0].Value = BookingId;
                }
                if (!string.IsNullOrEmpty(DocType))
                {
                    param[1] = new SqlParameter("@ParamDocType", SqlDbType.VarChar, 50);
                    param[1].Value = DocType;
                }
                if (!string.IsNullOrEmpty(DocPath))
                {
                    param[2] = new SqlParameter("@ParamDocPath", SqlDbType.VarChar, 500);
                    param[2].Value = DocPath;
                }


                SqlHelper.ExecuteNonQuery(conection,CommandType.Text, "insert into authdoc (DocType,DocPath,BookingId,UploadDate) values(@ParamDocType,@ParamDocPath,@ParamBookingID,GETDATE())", param);
                return true;
            }
        }
        catch(Exception ex)
        {
            return false;
        }
        
    }
    public DataTable Get_Only_Doc_Upload(string BookingId)
    {
        DataTable dt = new DataTable();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(BookingId))
                {
                    param[0] = new SqlParameter("@ParamBookingID", SqlDbType.VarChar, 50);
                    param[0].Value = BookingId;
                }
               


              
                SqlCommand command = new SqlCommand(" select * from authdoc where bookingid=@ParamBookingID",conection);
                command.Parameters.AddWithValue("@ParamBookingID", BookingId);

               
                SqlDataAdapter adp = new SqlDataAdapter(command);

                adp.Fill(dt);

                return dt;
            }
        }
        catch (Exception ex)
        {
            return dt;
        }

    }


    #region Booking Status master Details

    public string SET_Booking_Status_Master(string StatusName, string Description, string Counter)
    {
        SqlParameter[] param = new SqlParameter[4];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(StatusName))
                {
                    param[0] = new SqlParameter("@ParamStatusName", SqlDbType.NVarChar, 50);
                    param[0].Value = StatusName;
                }
                if (!string.IsNullOrEmpty(Description))
                {
                    param[1] = new SqlParameter("@ParamDescription", SqlDbType.NVarChar, 500);
                    param[1].Value = Description;
                }

                param[2] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[2].Value = Counter;

                param[3] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 500);
                param[3].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_Booking_Status_Master", param);
                return param[3].Value.ToString();
            }
        }
        catch
        {
            return "false";
        }
    }

    public DataTable GET_Booking_Status_Master()
    {
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {

                param[0] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[0].Value = "Select";

                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_Booking_Status_Master", param);
                return ds.Tables[0];
            }
        }
        catch
        {
            return null;
        }
    }

    #endregion

    #region Booking Master

    public string SET_Booking_Master(string BookingID, string InvoiceNo, string BookingType, string CurrencyType,
        string BookingByCompany, string BookingStatus, string Counter)
    {
        SqlParameter[] param = new SqlParameter[9];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(BookingID))
                {
                    param[0] = new SqlParameter("@ParamBookingID", SqlDbType.VarChar, 50);
                    param[0].Value = BookingID;
                }
                if (!string.IsNullOrEmpty(InvoiceNo))
                {
                    param[1] = new SqlParameter("@ParamInvoiceNo", SqlDbType.VarChar, 50);
                    param[1].Value = InvoiceNo;
                }
                if (!string.IsNullOrEmpty(BookingType))
                {
                    param[2] = new SqlParameter("@ParamBookingType", SqlDbType.VarChar, 50);
                    param[2].Value = BookingType;
                }
                if (!string.IsNullOrEmpty(CurrencyType))
                {
                    param[3] = new SqlParameter("@ParamCurrencyType", SqlDbType.VarChar, 50);
                    param[3].Value = CurrencyType;
                }
                if (!string.IsNullOrEmpty(BookingByCompany))
                {
                    param[4] = new SqlParameter("@ParamBookingByCompany", SqlDbType.VarChar, 50);
                    param[4].Value = BookingByCompany;
                }
                if (!string.IsNullOrEmpty(BookingStatus))
                {
                    param[5] = new SqlParameter("@ParamBookingStatus", SqlDbType.VarChar, 50);
                    param[5].Value = BookingStatus;
                }

                param[6] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[6].Value = Counter;

                param[7] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 500);
                param[7].Direction = ParameterDirection.Output;
                if (!string.IsNullOrEmpty(InvoiceNo))
                {
                    param[8] = new SqlParameter("@ParamInvoiceDate", SqlDbType.DateTime);
                    param[8].Value = DateTime.Now;
                }

                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_Booking_Master", param);
                return param[7].Value.ToString();
            }
        }
        catch
        {
            return "false";
        }
    }

    public DataTable GET_Booking_Master(string BookingID)
    {
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                param[0] = new SqlParameter("@ParamBookingID", SqlDbType.VarChar, 50);
                param[0].Value = BookingID;

                param[1] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[1].Value = "Select";

                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_Booking_Master", param);
                return ds.Tables[0];
            }
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    #endregion
    #region Booking Date Change
    public int SET_Booking_Date(string BookingID, string BookingBy, string BookingDate)
    {
        SqlParameter[] param = new SqlParameter[3];
        int i = 0;
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(BookingID))
                {
                    param[0] = new SqlParameter("@ParamBookingID", SqlDbType.VarChar, 50);
                    param[0].Value = BookingID;
                }
                if (!string.IsNullOrEmpty(BookingBy))
                {
                    param[1] = new SqlParameter("@ParamBookingBy", SqlDbType.VarChar, 100);
                    param[1].Value = BookingBy;
                }
                if (!string.IsNullOrEmpty(BookingDate))
                {
                    param[2] = new SqlParameter("@ParamBookingDate", SqlDbType.DateTime);
                    param[2].Value = Convert.ToDateTime(BookingDate);
                }
                i = SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "USP_ChangeDate", param);
                return i;
            }

        }
        catch
        {
            return i;

        }
    }
    #endregion

    #region Booking Details

    public string SET_Booking_Detail(string BookingID, string ProdID, string Provider, string BookingBy,
 string BookingByType, string BookingDateTime, string FromDateTime, string ToDateTime, string BookingStatus,
 string BookingRemarks, string TotalAmount, string PNRConfirmation, string SourceMedia, string ProductType,
 string isLocked, string ModifiedBy, string Supplier, string MailIssued, string Counter, string AtolType, string AssignedTo = "",
 string SupplierRef = "", string Company = "", string GDS = "")
    {
        SqlParameter[] param = new SqlParameter[24];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(BookingID))
                {
                    param[0] = new SqlParameter("@ParamBookingID", SqlDbType.VarChar, 50);
                    param[0].Value = BookingID;
                }
                if (!string.IsNullOrEmpty(ProdID))
                {
                    param[1] = new SqlParameter("@ParamProdID", SqlDbType.VarChar, 50);
                    param[1].Value = ProdID;
                }
                if (!string.IsNullOrEmpty(Provider))
                {
                    param[2] = new SqlParameter("@ParamProvider", SqlDbType.VarChar, 50);
                    param[2].Value = Provider;
                }
                if (!string.IsNullOrEmpty(BookingBy))
                {
                    param[3] = new SqlParameter("@ParamBookingBy", SqlDbType.VarChar, 100);
                    param[3].Value = BookingBy;
                }
                if (!string.IsNullOrEmpty(BookingByType))
                {
                    param[4] = new SqlParameter("@ParamBookingByType", SqlDbType.VarChar, 100);
                    param[4].Value = BookingByType;
                }
                if (Counter.ToLower() != "update")
                {
                    if (!string.IsNullOrEmpty(BookingDateTime))
                    {
                        param[5] = new SqlParameter("@ParamBookingDateTime", SqlDbType.DateTime);
                        param[5].Value = Convert.ToDateTime(BookingDateTime);
                    }
                }
                //if (!string.IsNullOrEmpty(FromDateTime))
                //{
                //    param[6] = new SqlParameter("@ParamFromDateTime", SqlDbType.DateTime);
                //    param[6].Value = Convert.ToDateTime(FromDateTime);
                //}
                //if (!string.IsNullOrEmpty(ToDateTime))
                //{
                //    param[7] = new SqlParameter("@ParamToDateTime", SqlDbType.DateTime);
                //    param[7].Value = Convert.ToDateTime(ToDateTime);
                //}
                if (!string.IsNullOrEmpty(BookingStatus))
                {
                    param[8] = new SqlParameter("@ParamBookingStatus", SqlDbType.VarChar, 50);
                    param[8].Value = BookingStatus;
                }
                if (!string.IsNullOrEmpty(BookingRemarks))
                {
                    param[9] = new SqlParameter("@ParamBookingRemarks", SqlDbType.VarChar, 2000);
                    param[9].Value = BookingRemarks;
                }
                if (!string.IsNullOrEmpty(TotalAmount))
                {
                    param[10] = new SqlParameter("@ParamTotalAmount", SqlDbType.Money);
                    param[10].Value = Convert.ToDouble(TotalAmount);
                }
                if (!string.IsNullOrEmpty(PNRConfirmation))
                {
                    param[11] = new SqlParameter("@ParamPNRConfirmation", SqlDbType.VarChar, 50);
                    param[11].Value = PNRConfirmation;
                }
                if (!string.IsNullOrEmpty(SourceMedia))
                {
                    param[12] = new SqlParameter("@ParamSourceMedia", SqlDbType.VarChar, 50);
                    param[12].Value = SourceMedia;
                }
                if (!string.IsNullOrEmpty(ProductType))
                {
                    param[13] = new SqlParameter("@ParamProductType", SqlDbType.VarChar, 50);
                    param[13].Value = ProductType;
                }
                if (!string.IsNullOrEmpty(isLocked))
                {
                    param[14] = new SqlParameter("@ParamisLocked", SqlDbType.Bit);
                    param[14].Value = Convert.ToBoolean(isLocked);
                }
                if (!string.IsNullOrEmpty(ModifiedBy))
                {
                    param[15] = new SqlParameter("@ParamModifiedBy", SqlDbType.VarChar, 100);
                    param[15].Value = ModifiedBy;
                }
                if (!string.IsNullOrEmpty(Supplier))
                {
                    param[16] = new SqlParameter("@ParamSupplier", SqlDbType.VarChar, 100);
                    param[16].Value = Supplier;
                }
                if (!string.IsNullOrEmpty(MailIssued))
                {
                    param[17] = new SqlParameter("@ParamMailIssued", SqlDbType.Bit);
                    param[17].Value = Convert.ToBoolean(MailIssued);
                }
                param[18] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[18].Value = Counter;

                param[19] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 500);
                param[19].Direction = ParameterDirection.Output;

                if (!string.IsNullOrEmpty(AtolType))
                {
                    param[20] = new SqlParameter("@ParamAtolType", SqlDbType.VarChar, 100);
                    param[20].Value = AtolType;
                }
                if (!string.IsNullOrEmpty(AssignedTo))
                {
                    param[21] = new SqlParameter("@ParamAssignedTo", SqlDbType.VarChar, 100);
                    param[21].Value = AssignedTo;
                }

                if (!string.IsNullOrEmpty(SupplierRef.Trim()))
                {
                    param[22] = new SqlParameter("@ParamSupplierRef", SqlDbType.VarChar, 100);
                    param[22].Value = SupplierRef.Trim();
                }
                if (!string.IsNullOrEmpty(GDS))
                {
                    param[23] = new SqlParameter("@ParamGDS", SqlDbType.VarChar, 100);
                    param[23].Value = GDS;
                }

                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_Booking_Detail", param);
                if (Company != "")
                {
                    SET_Booking_Master(BookingID, "", "", "", Company, "", "Update");
                }
                return param[19].Value.ToString();
            }
        }
        catch
        {
            return "false";
        }
    }

    public DataTable GET_Booking_Detail(string BookingID, string ProdID, string Provider, string BookingBy,
        string FromDateTime, string ToDateTime, string BookingStatus, string PNRConfirmation, string SourceMedia,
        string ProductType, string AtolType)
    {
        SqlParameter[] param = new SqlParameter[12];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(BookingID))
                {
                    param[0] = new SqlParameter("@ParamBookingID", SqlDbType.VarChar, 50);
                    param[0].Value = BookingID;
                }
                if (!string.IsNullOrEmpty(ProdID))
                {
                    param[1] = new SqlParameter("@ParamProdID", SqlDbType.VarChar, 50);
                    param[1].Value = ProdID;
                }
                if (!string.IsNullOrEmpty(Provider))
                {
                    param[2] = new SqlParameter("@ParamProvider", SqlDbType.VarChar, 50);
                    param[2].Value = Provider;
                }
                if (!string.IsNullOrEmpty(BookingBy))
                {
                    param[3] = new SqlParameter("@ParamBookingBy", SqlDbType.VarChar, 100);
                    param[3].Value = BookingBy;
                }
                if (!string.IsNullOrEmpty(FromDateTime))
                {
                    param[4] = new SqlParameter("@ParamFromDateTime", SqlDbType.DateTime);
                    param[4].Value = Convert.ToDateTime(FromDateTime);
                }
                if (!string.IsNullOrEmpty(ToDateTime))
                {
                    param[5] = new SqlParameter("@ParamToDateTime", SqlDbType.DateTime);
                    param[5].Value = Convert.ToDateTime(ToDateTime);
                }
                if (!string.IsNullOrEmpty(BookingStatus))
                {
                    param[6] = new SqlParameter("@ParamBookingStatus", SqlDbType.VarChar, 50);
                    param[6].Value = BookingStatus;
                }

                if (!string.IsNullOrEmpty(PNRConfirmation))
                {
                    param[7] = new SqlParameter("@ParamPNRConfirmation", SqlDbType.VarChar, 50);
                    param[7].Value = PNRConfirmation;
                }
                if (!string.IsNullOrEmpty(SourceMedia))
                {
                    param[8] = new SqlParameter("@ParamSourceMedia", SqlDbType.VarChar, 50);
                    param[8].Value = SourceMedia;
                }
                if (!string.IsNullOrEmpty(ProductType))
                {
                    param[9] = new SqlParameter("@ParamProductType", SqlDbType.VarChar, 50);
                    param[9].Value = ProductType;
                }

                param[10] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[10].Value = "Select";

                if (!string.IsNullOrEmpty(AtolType))
                {
                    param[11] = new SqlParameter("@ParamAtolType", SqlDbType.VarChar, 100);
                    param[11].Value = AtolType;
                }


                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_Booking_Detail", param);
                return ds.Tables[0];
            }
        }
        catch
        {
            return null;
        }
    }

    #endregion

    #region Amount Charge Details

    public string SET_Amount_Charges_Detail(string BookingID, string ProdID, string SrNo, string ChargeID,
        string ChargesFor, string CostPrice, string SellPrice, string ChargesStatus, string SupplierID,
        string ChargesRemarks, string ChargesDate, string ModifiedBy, string Counter)
    {
        SqlParameter[] param = new SqlParameter[14];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(BookingID))
                {
                    param[0] = new SqlParameter("@ParamBookingID", SqlDbType.VarChar, 50);
                    param[0].Value = BookingID;
                }
                if (!string.IsNullOrEmpty(ProdID))
                {
                    param[1] = new SqlParameter("@ParamProdID", SqlDbType.VarChar, 50);
                    param[1].Value = ProdID;
                }
                if (!string.IsNullOrEmpty(SrNo))
                {
                    param[2] = new SqlParameter("@ParamSrNo", SqlDbType.Int);
                    param[2].Value = Convert.ToInt32(SrNo);
                }
                if (!string.IsNullOrEmpty(ChargeID))
                {
                    param[3] = new SqlParameter("@ParamChargeID", SqlDbType.VarChar, 50);
                    param[3].Value = ChargeID;
                }
                if (!string.IsNullOrEmpty(ChargesFor))
                {
                    param[4] = new SqlParameter("@ParamChargesFor", SqlDbType.VarChar, 50);
                    param[4].Value = ChargesFor;
                }
                if (!string.IsNullOrEmpty(CostPrice))
                {
                    param[5] = new SqlParameter("@ParamCostPrice", SqlDbType.Money);
                    param[5].Value = Convert.ToDouble(CostPrice);
                }
                if (!string.IsNullOrEmpty(SellPrice))
                {
                    param[6] = new SqlParameter("@ParamSellPrice", SqlDbType.Money);
                    param[6].Value = Convert.ToDouble(SellPrice);
                }
                if (!string.IsNullOrEmpty(ChargesStatus))
                {
                    param[7] = new SqlParameter("@ParamChargesStatus", SqlDbType.VarChar, 50);
                    param[7].Value = ChargesStatus;
                }
                if (!string.IsNullOrEmpty(SupplierID))
                {
                    param[8] = new SqlParameter("@ParamSupplierID", SqlDbType.VarChar, 50);
                    param[8].Value = SupplierID;
                }
                if (!string.IsNullOrEmpty(ChargesRemarks))
                {
                    param[9] = new SqlParameter("@ParamChargesRemarks", SqlDbType.VarChar, 500);
                    param[9].Value = ChargesRemarks;
                }
                if (!string.IsNullOrEmpty(ChargesDate))
                {
                    param[10] = new SqlParameter("@ParamChargesDate", SqlDbType.DateTime);
                    param[10].Value = Convert.ToDateTime(ChargesDate);
                }
                if (!string.IsNullOrEmpty(ModifiedBy))
                {
                    param[11] = new SqlParameter("@ParamModifiedBy", SqlDbType.VarChar, 50);
                    param[11].Value = ModifiedBy;
                }

                param[12] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[12].Value = Counter;

                param[13] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 500);
                param[13].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_Amount_Charges_Detail", param);
                return param[13].Value.ToString();
            }
        }
        catch (Exception ex)
        {
            return "false";
        }
    }

    public DataTable GET_Amount_Charges_Detail(string BookingID, string ProdID, string SrNo, string ChargeID,
        string ChargesFor, string CostPrice, string SellPrice, string ChargesStatus, string SupplierID)
    {
        SqlParameter[] param = new SqlParameter[8];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(BookingID))
                {
                    param[0] = new SqlParameter("@ParamBookingID", SqlDbType.VarChar, 50);
                    param[0].Value = BookingID;
                }
                if (!string.IsNullOrEmpty(ProdID))
                {
                    param[1] = new SqlParameter("@ParamProdID", SqlDbType.VarChar, 50);
                    param[1].Value = ProdID;
                }
                if (!string.IsNullOrEmpty(SrNo))
                {
                    param[2] = new SqlParameter("@ParamSrNo", SqlDbType.Int);
                    param[2].Value = Convert.ToInt32(SrNo);
                }
                if (!string.IsNullOrEmpty(ChargeID))
                {
                    param[3] = new SqlParameter("@ParamChargeID", SqlDbType.VarChar, 50);
                    param[3].Value = ChargeID;
                }
                if (!string.IsNullOrEmpty(ChargesFor))
                {
                    param[4] = new SqlParameter("@ParamChargesFor", SqlDbType.VarChar, 50);
                    param[4].Value = ChargesFor;
                }
                if (!string.IsNullOrEmpty(ChargesStatus))
                {
                    param[5] = new SqlParameter("@ParamChargesStatus", SqlDbType.VarChar, 50);
                    param[5].Value = ChargesStatus;
                }
                if (!string.IsNullOrEmpty(SupplierID))
                {
                    param[6] = new SqlParameter("@ParamSupplierID", SqlDbType.VarChar, 50);
                    param[6].Value = SupplierID;
                }
                param[7] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[7].Value = "Select";

                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_Amount_Charges_Detail", param);
                return ds.Tables[0];
            }
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    #endregion

    #region Contact Details

    public string SET_Contact_Detail(string BookingID, string ProdID, string SrNo, string PaxID, string PhoneNo,
        string MobileNo, string FAX, string EmailAddress, string Country, string State, string City, string Address,
        string PostCode, string AddressType, string ModifiedBy, string Counter)
    {
        SqlParameter[] param = new SqlParameter[17];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(BookingID))
                {
                    param[0] = new SqlParameter("@ParamBookingID", SqlDbType.VarChar, 50);
                    param[0].Value = BookingID;
                }
                if (!string.IsNullOrEmpty(ProdID))
                {
                    param[1] = new SqlParameter("@ParamProdID", SqlDbType.VarChar, 50);
                    param[1].Value = ProdID;
                }
                if (!string.IsNullOrEmpty(SrNo))
                {
                    param[2] = new SqlParameter("@ParamSrNo", SqlDbType.Int);
                    param[2].Value = Convert.ToInt32(SrNo);
                }
                if (!string.IsNullOrEmpty(PaxID))
                {
                    param[3] = new SqlParameter("@ParamPaxID", SqlDbType.VarChar, 50);
                    param[3].Value = PaxID;
                }
                if (!string.IsNullOrEmpty(PhoneNo))
                {
                    param[4] = new SqlParameter("@ParamPhoneNo", SqlDbType.VarChar, 100);
                    param[4].Value = PhoneNo;
                }
                if (!string.IsNullOrEmpty(MobileNo))
                {
                    param[5] = new SqlParameter("@ParamMobileNo", SqlDbType.VarChar, 100);
                    param[5].Value = MobileNo;
                }
                if (!string.IsNullOrEmpty(FAX))
                {
                    param[6] = new SqlParameter("@ParamFAX", SqlDbType.VarChar, 100);
                    param[6].Value = FAX;
                }
                if (!string.IsNullOrEmpty(EmailAddress))
                {
                    param[7] = new SqlParameter("@ParamEmailAddress", SqlDbType.VarChar, 500);
                    param[7].Value = EmailAddress;
                }
                if (!string.IsNullOrEmpty(Country))
                {
                    param[8] = new SqlParameter("@ParamCountry", SqlDbType.VarChar, 200);
                    param[8].Value = Country;
                }
                if (!string.IsNullOrEmpty(State))
                {
                    param[9] = new SqlParameter("@ParamState", SqlDbType.VarChar, 100);
                    param[9].Value = State;
                }
                if (!string.IsNullOrEmpty(City))
                {
                    param[10] = new SqlParameter("@ParamCity", SqlDbType.VarChar, 200);
                    param[10].Value = City;
                }
                if (!string.IsNullOrEmpty(Address))
                {
                    param[11] = new SqlParameter("@ParamAddress", SqlDbType.VarChar, 2000);
                    param[11].Value = Address;
                }
                if (!string.IsNullOrEmpty(PostCode))
                {
                    param[12] = new SqlParameter("@ParamPostCode", SqlDbType.VarChar, 50);
                    param[12].Value = PostCode;
                }
                if (!string.IsNullOrEmpty(AddressType))
                {
                    param[13] = new SqlParameter("@ParamAddressType", SqlDbType.VarChar, 50);
                    param[13].Value = AddressType;
                }
                if (!string.IsNullOrEmpty(ModifiedBy))
                {
                    param[14] = new SqlParameter("@ParamModifiedBy", SqlDbType.VarChar, 50);
                    param[14].Value = ModifiedBy;
                }
                param[15] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[15].Value = Counter;

                param[16] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 500);
                param[16].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_Contact_Detail", param);
                return param[16].Value.ToString();
            }
        }
        catch
        {
            return "false";
        }
    }

    public DataTable GET_Contact_Detail(string BookingID, string ProdID)
    {
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(BookingID))
                {
                    param[0] = new SqlParameter("@ParamBookingID", SqlDbType.VarChar, 50);
                    param[0].Value = BookingID;
                }
                if (!string.IsNullOrEmpty(ProdID))
                {
                    param[1] = new SqlParameter("@ParamProdID", SqlDbType.VarChar, 50);
                    param[1].Value = ProdID;
                }

                param[2] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[2].Value = "Select";

                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_Contact_Detail", param);
                return ds.Tables[0];
            }
        }
        catch
        {
            return null;
        }
    }

    #endregion

    #region Passenger Detail

    public string SET_Passenger_Detail(string BookingID, string ProdID, string SrNo, string PaxID, string Title,
        string PaxFirstName, string PaxMiddleName, string PaxLastName, string FrequentFlyerNo, string PassportNo,
        string Nationality, string ExpiryDate, string PlaceofIssue, string PlaceofBirth, string PaxDOB,
        string PaxType, string PaxSex, string ModifiedBy, string Counter, string Tickets)
    {
        SqlParameter[] param = new SqlParameter[21];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(BookingID))
                {
                    param[0] = new SqlParameter("@ParamBookingID", SqlDbType.VarChar, 50);
                    param[0].Value = BookingID;
                }
                if (!string.IsNullOrEmpty(ProdID))
                {
                    param[1] = new SqlParameter("@ParamProdID", SqlDbType.VarChar, 50);
                    param[1].Value = ProdID;
                }
                if (!string.IsNullOrEmpty(SrNo))
                {
                    param[2] = new SqlParameter("@ParamSrNo", SqlDbType.Int);
                    param[2].Value = Convert.ToInt32(SrNo);
                }
                if (!string.IsNullOrEmpty(PaxID))
                {
                    param[3] = new SqlParameter("@ParamPaxID", SqlDbType.VarChar, 50);
                    param[3].Value = PaxID;
                }
                if (!string.IsNullOrEmpty(Title))
                {
                    param[4] = new SqlParameter("@ParamTitle", SqlDbType.VarChar, 20);
                    param[4].Value = Title;
                }
                if (!string.IsNullOrEmpty(PaxFirstName))
                //if (1 == 1)
                {
                    param[5] = new SqlParameter("@ParamPaxFirstName", SqlDbType.VarChar, 100);
                    param[5].Value = PaxFirstName;
                }
                if (!string.IsNullOrEmpty(PaxMiddleName))
                //if(1 ==1 )
                {
                    param[6] = new SqlParameter("@ParamPaxMiddleName", SqlDbType.VarChar, 100);
                    param[6].Value = PaxMiddleName;
                }
                if (!string.IsNullOrEmpty(PaxLastName))
                //if (1 == 1)
                {
                    param[7] = new SqlParameter("@ParamPaxLastName", SqlDbType.VarChar, 100);
                    param[7].Value = PaxLastName;
                }
                if (!string.IsNullOrEmpty(FrequentFlyerNo))
                {
                    param[8] = new SqlParameter("@ParamFrequentFlyerNo", SqlDbType.VarChar, 50);
                    param[8].Value = FrequentFlyerNo;
                }
                if (!string.IsNullOrEmpty(PassportNo))
                {
                    param[9] = new SqlParameter("@ParamPassportNo", SqlDbType.VarChar, 100);
                    param[9].Value = PassportNo;
                }
                if (!string.IsNullOrEmpty(Nationality))
                {
                    param[10] = new SqlParameter("@ParamNationality", SqlDbType.VarChar, 100);
                    param[10].Value = Nationality;
                }
                if (!string.IsNullOrEmpty(ExpiryDate))
                {
                    param[11] = new SqlParameter("@ParamExpiryDate", SqlDbType.DateTime);
                    param[11].Value = Convert.ToDateTime(ExpiryDate);
                }
                if (!string.IsNullOrEmpty(PlaceofIssue))
                {
                    param[12] = new SqlParameter("@ParamPlaceofIssue", SqlDbType.VarChar, 100);
                    param[12].Value = PlaceofIssue;
                }
                if (!string.IsNullOrEmpty(PlaceofBirth))
                {
                    param[13] = new SqlParameter("@ParamPlaceofBirth", SqlDbType.VarChar, 100);
                    param[13].Value = PlaceofBirth;
                }
                if (!string.IsNullOrEmpty(PaxDOB))
                {
                    param[14] = new SqlParameter("@ParamPaxDOB", SqlDbType.DateTime);
                    param[14].Value = Convert.ToDateTime(PaxDOB);
                }
                if (!string.IsNullOrEmpty(PaxType))
                {
                    param[15] = new SqlParameter("@ParamPaxType", SqlDbType.VarChar, 50);
                    param[15].Value = PaxType;
                }
                if (!string.IsNullOrEmpty(PaxSex))
                {
                    param[16] = new SqlParameter("@ParamPaxSex", SqlDbType.VarChar, 50);
                    param[16].Value = PaxSex;
                }
                if (!string.IsNullOrEmpty(ModifiedBy))
                {
                    param[17] = new SqlParameter("@ParamModifiedBy", SqlDbType.VarChar, 50);
                    param[17].Value = ModifiedBy;
                }
                param[18] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[18].Value = Counter;

                param[19] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 500);
                param[19].Direction = ParameterDirection.Output;
                if (!string.IsNullOrEmpty(Tickets))
                {
                    param[20] = new SqlParameter("@ParamPaxTickets", SqlDbType.VarChar, 500);
                    param[20].Value = Tickets;
                }

                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_Passenger_Detail", param);
                return param[19].Value.ToString();
            }
        }
        catch
        {
            return "false";
        }
    }

    public DataTable GET_Passenger_Detail(string BookingID, string ProdID)
    {
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(BookingID))
                {
                    param[0] = new SqlParameter("@ParamBookingID", SqlDbType.VarChar, 50);
                    param[0].Value = BookingID;
                }
                if (!string.IsNullOrEmpty(ProdID))
                {
                    param[1] = new SqlParameter("@ParamProdID", SqlDbType.VarChar, 50);
                    param[1].Value = ProdID;
                }
                param[2] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[2].Value = "Select";

                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_Passenger_Detail", param);
                return ds.Tables[0];
            }
        }
        catch
        {
            return null;
        }
    }

    #endregion

    #region Sector Detail

    public string SET_Sector_Detail(string BookingID, string ProdID, string SrNo, string CarierName,
        string FromDestination, string FromDateTime, string ToDestination, string ToDateTime, string FlightNo,
        string Class, string FlightStatus, string FareBasis, string NotValidBefor, string NotValidAfter,
        string BaggageAllownce, string AirportTerminalFrom, string AirportTerminalTo, string SegID, string SegRemarks,
        string TripID, string ModifiedBy, string Counter, string AirlineConfirmationCode, string CabinClass)
    {
        SqlParameter[] param = new SqlParameter[25];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(BookingID))
                {
                    param[0] = new SqlParameter("@ParamBookingID", SqlDbType.VarChar, 50);
                    param[0].Value = BookingID;
                }
                if (!string.IsNullOrEmpty(ProdID))
                {
                    param[1] = new SqlParameter("@ParamProdID", SqlDbType.VarChar, 50);
                    param[1].Value = ProdID;
                }
                if (!string.IsNullOrEmpty(SrNo))
                {
                    param[2] = new SqlParameter("@ParamSrNo", SqlDbType.Int);
                    param[2].Value = Convert.ToInt32(SrNo);
                }
                if (!string.IsNullOrEmpty(CarierName))
                {
                    param[3] = new SqlParameter("@ParamCarierName", SqlDbType.VarChar, 100);
                    param[3].Value = CarierName;
                }
                if (!string.IsNullOrEmpty(FromDestination))
                {
                    param[4] = new SqlParameter("@ParamFromDestination", SqlDbType.VarChar, 100);
                    param[4].Value = FromDestination;
                }
                if (!string.IsNullOrEmpty(FromDateTime))
                {
                    param[5] = new SqlParameter("@ParamFromDateTime", SqlDbType.DateTime);
                    param[5].Value = Convert.ToDateTime(FromDateTime);
                }
                if (!string.IsNullOrEmpty(ToDestination))
                {
                    param[6] = new SqlParameter("@ParamToDestination", SqlDbType.VarChar, 100);
                    param[6].Value = ToDestination;
                }
                if (!string.IsNullOrEmpty(ToDateTime))
                {
                    param[7] = new SqlParameter("@ParamToDateTime", SqlDbType.DateTime);
                    param[7].Value = Convert.ToDateTime(ToDateTime);
                }
                if (!string.IsNullOrEmpty(FlightNo))
                {
                    param[8] = new SqlParameter("@ParamFlightNo", SqlDbType.VarChar, 50);
                    param[8].Value = FlightNo;
                }
                if (!string.IsNullOrEmpty(Class))
                {
                    param[9] = new SqlParameter("@ParamClass", SqlDbType.VarChar, 100);
                    param[9].Value = Class;
                }
                if (!string.IsNullOrEmpty(FlightStatus))
                {
                    param[10] = new SqlParameter("@ParamFlightStatus", SqlDbType.VarChar, 100);
                    param[10].Value = FlightStatus;
                }
                if (!string.IsNullOrEmpty(FareBasis))
                {
                    param[11] = new SqlParameter("@ParamFareBasis", SqlDbType.VarChar, 100);
                    param[11].Value = FareBasis;
                }
                if (!string.IsNullOrEmpty(NotValidBefor))
                {
                    param[12] = new SqlParameter("@ParamNotValidBefor", SqlDbType.VarChar, 100);
                    param[12].Value = NotValidBefor;
                }
                if (!string.IsNullOrEmpty(NotValidAfter))
                {
                    param[13] = new SqlParameter("@ParamNotValidAfter", SqlDbType.VarChar, 100);
                    param[13].Value = NotValidAfter;
                }
                if (!string.IsNullOrEmpty(BaggageAllownce))
                {
                    param[14] = new SqlParameter("@ParamBaggageAllownce", SqlDbType.VarChar, 500);
                    param[14].Value = BaggageAllownce;
                }
                if (!string.IsNullOrEmpty(AirportTerminalFrom))
                {
                    param[15] = new SqlParameter("@ParamAirportTerminalFrom", SqlDbType.VarChar, 200);
                    param[15].Value = AirportTerminalFrom;
                }
                if (!string.IsNullOrEmpty(AirportTerminalTo))
                {
                    param[16] = new SqlParameter("@ParamAirportTerminalTo", SqlDbType.VarChar, 200);
                    param[16].Value = AirportTerminalTo;
                }
                if (!string.IsNullOrEmpty(SegID))
                {
                    param[17] = new SqlParameter("@ParamSegID", SqlDbType.VarChar, 50);
                    param[17].Value = SegID;
                }
                if (!string.IsNullOrEmpty(SegRemarks))
                {
                    param[18] = new SqlParameter("@ParamSegRemarks", SqlDbType.VarChar, 1000);
                    param[18].Value = SegRemarks;
                }
                if (!string.IsNullOrEmpty(TripID))
                {
                    param[19] = new SqlParameter("@ParamTripID", SqlDbType.VarChar, 50);
                    param[19].Value = TripID;
                }
                if (!string.IsNullOrEmpty(ModifiedBy))
                {
                    param[20] = new SqlParameter("@ParamModifiedBy", SqlDbType.VarChar, 50);
                    param[20].Value = ModifiedBy;
                }

                param[21] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[21].Value = Counter;

                if (!string.IsNullOrEmpty(AirlineConfirmationCode))
                {
                    param[22] = new SqlParameter("@ParamAirlineConfirmationCode", SqlDbType.VarChar, 50);
                    param[22].Value = AirlineConfirmationCode;
                }

                if (!string.IsNullOrEmpty(CabinClass))
                {
                    param[23] = new SqlParameter("@ParamCabinClass", SqlDbType.VarChar, 50);
                    param[23].Value = CabinClass;
                }

                param[24] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 500);
                param[24].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_Sector_Detail", param);
                return param[24].Value.ToString();
            }
        }
        catch (Exception exp)
        {
            return "false";
        }
    }

    public DataTable GET_Sector_Detail(string BookingID, string ProdID)
    {
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(BookingID))
                {
                    param[0] = new SqlParameter("@ParamBookingID", SqlDbType.VarChar, 50);
                    param[0].Value = BookingID;
                }
                if (!string.IsNullOrEmpty(ProdID))
                {
                    param[1] = new SqlParameter("@ParamProdID", SqlDbType.VarChar, 50);
                    param[1].Value = ProdID;
                }
                param[2] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[2].Value = "Select";

                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_Sector_Detail", param);
                return ds.Tables[0];
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }


    public DataTable GET_Sector_Detail_NO_XP(string BookingID, string Counter)
    {
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                param[0] = new SqlParameter("@ParamBookingID", SqlDbType.VarChar, 50);
                param[0].Value = BookingID;

                param[1] = new SqlParameter("@ParamNoXP", SqlDbType.VarChar, 50);
                param[1].Value = "";

                param[2] = new SqlParameter("@ParamCounter", SqlDbType.VarChar, 500);
                param[2].Value = Counter;

                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_Booking_NO_XP", param);
                return ds.Tables[0];
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }



    public string TimeFormat(string time)
    {
        if (time.Length == 4 || time.Length == 5)
        { time = " " + time; }
        string[] time1 = time.Split(' ');

        if (time1.Length > 1)
        {
            if (time1[1].IndexOf(":") == -1)
            {
                if (time1[1].Length == 2)
                {
                    time1[1] = "00:" + time;
                }
                else if (time1[1].Length == 3)
                {
                    time1[1] = "0" + time;
                    time1[1] = time.Insert(2, ":");
                }
                else if (time1[1].Length == 4)
                {
                    time1[1] = time1[1].Insert(2, ":");
                }

            }
        }
        return time1[0] + " " + time1[1];

    }
    #endregion

    #region Sector Master

    public string SET_Sectors_Master(string BookingID, string ProdID, string JourneyType, string LastTktDate,
        string Origin, string Destination, string ValidatingCarrier, string CabinClass, string ModifiedBy,
        string Counter, string supplier, string IssuedDate)
    {
        SqlParameter[] param = new SqlParameter[13]; try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(BookingID))
                {
                    param[0] = new SqlParameter("@ParamBookingID", SqlDbType.VarChar, 50);
                    param[0].Value = BookingID;
                }
                if (!string.IsNullOrEmpty(ProdID))
                {
                    param[1] = new SqlParameter("@ParamProdID", SqlDbType.VarChar, 50);
                    param[1].Value = ProdID;
                }
                if (!string.IsNullOrEmpty(JourneyType))
                {
                    param[2] = new SqlParameter("@ParamJourneyType", SqlDbType.VarChar, 50);
                    param[2].Value = JourneyType;
                }
                if (!string.IsNullOrEmpty(LastTktDate))
                {
                    param[3] = new SqlParameter("@ParamLastTktDate", SqlDbType.VarChar, 200);
                    param[3].Value = LastTktDate;
                }
                if (!string.IsNullOrEmpty(Origin))
                {
                    param[4] = new SqlParameter("@ParamOrigin", SqlDbType.VarChar, 50);
                    param[4].Value = Origin;
                }
                if (!string.IsNullOrEmpty(Destination))
                {
                    param[5] = new SqlParameter("@ParamDestination", SqlDbType.VarChar, 50);
                    param[5].Value = Destination;
                }
                if (!string.IsNullOrEmpty(ValidatingCarrier))
                {
                    param[6] = new SqlParameter("@ParamValidatingCarrier", SqlDbType.VarChar, 50);
                    param[6].Value = ValidatingCarrier;
                }
                if (!string.IsNullOrEmpty(CabinClass))
                {
                    param[7] = new SqlParameter("@ParamCabinClass", SqlDbType.VarChar, 50);
                    param[7].Value = CabinClass;
                }
                if (!string.IsNullOrEmpty(ModifiedBy))
                {
                    param[8] = new SqlParameter("@ParamModifiedBy", SqlDbType.VarChar, 50);
                    param[8].Value = ModifiedBy;
                }
                param[9] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[9].Value = Counter;

                param[10] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 500);
                param[10].Direction = ParameterDirection.Output;
                if (!string.IsNullOrEmpty(supplier))
                {
                    param[11] = new SqlParameter("@ParamTicket_IssuedBy", SqlDbType.VarChar, 250);
                    param[11].Value = supplier;
                }
                if (!string.IsNullOrEmpty(IssuedDate))
                {
                    param[12] = new SqlParameter("@ParamTicket_IssuedDate", SqlDbType.DateTime);
                    param[12].Value = Convert.ToDateTime(IssuedDate);
                }

                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_Sectors_Master", param);
                return param[10].Value.ToString();
            }
        }
        catch
        {
            return "false";
        }
    }

    public DataTable GET_Sectors_Master(string BookingID, string ProdID)
    {
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(BookingID))
                {
                    param[0] = new SqlParameter("@ParamBookingID", SqlDbType.VarChar, 50);
                    param[0].Value = BookingID;
                }
                if (!string.IsNullOrEmpty(ProdID))
                {
                    param[1] = new SqlParameter("@ParamProdID", SqlDbType.VarChar, 50);
                    param[1].Value = ProdID;
                }
                param[2] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[2].Value = "Select";

                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_Sectors_Master", param);
                return ds.Tables[0];
            }
        }
        catch
        {
            return null;
        }
    }

    #endregion

    #region SET_FlightDetails

    public bool SET_FlightDetails(string BM_BookingID, string BM_InvoiceNo, string BM_BookingType,
        string BM_CurrencyType, string BM_BookingByCompany, string BM_BookingStatus, string BM_IsInsertBM,
        string BD_ProdID, string BD_Provider, string BD_BookingBy, string BD_BookingByType,
        string BD_BookingDateTime, string BD_BookingStatus,
        string BD_BookingRemarks, string BD_TotalAmount, string BD_PNRConfirmation, string BD_SourceMedia,
        string BD_ProductType, string BD_isLocked, string BD_ModifiedBy, string BD_Supplier,
        string BD_MailIssued, string SM_JourneyType, string SM_LastTktDate, string SM_Origin,
        string SM_Destination, string SM_ValidatingCarrier, string SM_CabinClass, string SM_ModifiedBy,
        string CD_PaxID, string CD_PhoneNo, string CD_MobileNo, string CD_FAX, string CD_EmailAddress,
        string CD_Country, string CD_State, string CD_City, string CD_Address, string CD_PostCode,
        string CD_AddressType, string CD_ModifiedBy, DataTable AirSectors, DataTable AmountCharges,
        DataTable Passengers)
    {
        SqlParameter[] param = new SqlParameter[46];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(BM_BookingID))
                {
                    param[0] = new SqlParameter("@ParamBM_BookingID", SqlDbType.VarChar, 50);
                    param[0].Value = BM_BookingID;
                }
                if (!string.IsNullOrEmpty(BM_InvoiceNo))
                {
                    param[1] = new SqlParameter("@ParamBM_InvoiceNo", SqlDbType.VarChar, 50);
                    param[1].Value = BM_InvoiceNo;
                }
                if (!string.IsNullOrEmpty(BM_BookingType))
                {
                    param[2] = new SqlParameter("@ParamBM_BookingType", SqlDbType.VarChar, 50);
                    param[2].Value = BM_BookingType;
                }
                if (!string.IsNullOrEmpty(BM_CurrencyType))
                {
                    param[3] = new SqlParameter("@ParamBM_CurrencyType", SqlDbType.VarChar, 50);
                    param[3].Value = BM_CurrencyType;
                }
                if (!string.IsNullOrEmpty(BM_BookingByCompany))
                {
                    param[4] = new SqlParameter("@ParamBM_BookingByCompany", SqlDbType.VarChar, 50);
                    param[4].Value = BM_BookingByCompany;
                }
                if (!string.IsNullOrEmpty(BM_BookingStatus))
                {
                    param[5] = new SqlParameter("@ParamBM_BookingStatus", SqlDbType.VarChar, 50);
                    param[5].Value = BM_BookingStatus;
                }
                param[6] = new SqlParameter("@ParamBM_IsInsertBM", SqlDbType.VarChar, 50);
                param[6].Value = BM_IsInsertBM;
                if (!string.IsNullOrEmpty(BD_ProdID))
                {
                    param[7] = new SqlParameter("@ParamBD_ProdID", SqlDbType.VarChar, 50);
                    param[7].Value = BD_ProdID;
                }
                if (!string.IsNullOrEmpty(BD_Provider))
                {
                    param[8] = new SqlParameter("@ParamBD_Provider", SqlDbType.VarChar, 50);
                    param[8].Value = BD_Provider;
                }
                if (!string.IsNullOrEmpty(BD_BookingBy))
                {
                    param[9] = new SqlParameter("@ParamBD_BookingBy", SqlDbType.VarChar, 100);
                    param[9].Value = BD_BookingBy;
                }
                if (!string.IsNullOrEmpty(BD_BookingByType))
                {
                    param[10] = new SqlParameter("@ParamBD_BookingByType", SqlDbType.VarChar, 100);
                    param[10].Value = BD_BookingByType;
                }
                if (!string.IsNullOrEmpty(BD_BookingDateTime))
                {
                    param[11] = new SqlParameter("@ParamBD_BookingDateTime", SqlDbType.DateTime);
                    param[11].Value = Convert.ToDateTime(BD_BookingDateTime);
                }
                if (!string.IsNullOrEmpty(BD_BookingStatus))
                {
                    param[12] = new SqlParameter("@ParamBD_BookingStatus", SqlDbType.VarChar, 50);
                    param[12].Value = BD_BookingStatus;
                }
                if (!string.IsNullOrEmpty(BD_BookingRemarks))
                {
                    param[13] = new SqlParameter("@ParamBD_BookingRemarks", SqlDbType.VarChar, 2000);
                    param[13].Value = BD_BookingRemarks;
                }
                if (!string.IsNullOrEmpty(BD_TotalAmount))
                {
                    param[14] = new SqlParameter("@ParamBD_TotalAmount", SqlDbType.Money);
                    param[14].Value = Convert.ToDouble(BD_TotalAmount);
                }
                if (!string.IsNullOrEmpty(BD_PNRConfirmation))
                {
                    param[15] = new SqlParameter("@ParamBD_PNRConfirmation", SqlDbType.VarChar, 50);
                    param[15].Value = BD_PNRConfirmation;
                }
                if (!string.IsNullOrEmpty(BD_SourceMedia))
                {
                    param[16] = new SqlParameter("@ParamBD_SourceMedia", SqlDbType.VarChar, 50);
                    param[16].Value = BD_SourceMedia;
                }
                if (!string.IsNullOrEmpty(BD_ProductType))
                {
                    param[17] = new SqlParameter("@ParamBD_ProductType", SqlDbType.VarChar, 50);
                    param[17].Value = BD_ProductType;
                }
                if (!string.IsNullOrEmpty(BD_isLocked))
                {
                    param[18] = new SqlParameter("@ParamBD_isLocked", SqlDbType.Bit);
                    param[18].Value = Convert.ToBoolean(BD_isLocked);
                }
                if (!string.IsNullOrEmpty(BD_ModifiedBy))
                {
                    param[19] = new SqlParameter("@ParamBD_ModifiedBy", SqlDbType.VarChar, 100);
                    param[19].Value = BD_ModifiedBy;
                }
                if (!string.IsNullOrEmpty(BD_Supplier))
                {
                    param[20] = new SqlParameter("@ParamBD_Supplier", SqlDbType.VarChar, 100);
                    param[20].Value = BD_Supplier;
                }
                if (!string.IsNullOrEmpty(BD_MailIssued))
                {
                    param[21] = new SqlParameter("@ParamBD_MailIssued", SqlDbType.Bit);
                    param[21].Value = Convert.ToBoolean(BD_MailIssued);
                }

                if (!string.IsNullOrEmpty(SM_JourneyType))
                {
                    param[22] = new SqlParameter("@ParamSM_JourneyType", SqlDbType.VarChar, 50);
                    param[22].Value = SM_JourneyType;
                }
                if (!string.IsNullOrEmpty(SM_LastTktDate))
                {
                    param[23] = new SqlParameter("@ParamSM_LastTktDate", SqlDbType.VarChar, 200);
                    param[23].Value = SM_LastTktDate;
                }
                if (!string.IsNullOrEmpty(SM_Origin))
                {
                    param[24] = new SqlParameter("@ParamSM_Origin", SqlDbType.VarChar, 50);
                    param[24].Value = SM_Origin;
                }
                if (!string.IsNullOrEmpty(SM_Destination))
                {
                    param[25] = new SqlParameter("@ParamSM_Destination", SqlDbType.VarChar, 50);
                    param[25].Value = SM_Destination;
                }
                if (!string.IsNullOrEmpty(SM_ValidatingCarrier))
                {
                    param[26] = new SqlParameter("@ParamSM_ValidatingCarrier", SqlDbType.VarChar, 50);
                    param[26].Value = SM_ValidatingCarrier;
                }
                if (!string.IsNullOrEmpty(SM_CabinClass))
                {
                    param[27] = new SqlParameter("@ParamSM_CabinClass", SqlDbType.VarChar, 50);
                    param[27].Value = SM_CabinClass;
                }
                if (!string.IsNullOrEmpty(SM_ModifiedBy))
                {
                    param[28] = new SqlParameter("@ParamSM_ModifiedBy", SqlDbType.VarChar, 50);
                    param[28].Value = SM_ModifiedBy;
                }

                if (!string.IsNullOrEmpty(CD_PaxID))
                {
                    param[29] = new SqlParameter("@ParamCD_PaxID", SqlDbType.VarChar, 50);
                    param[29].Value = CD_PaxID;
                }
                if (!string.IsNullOrEmpty(CD_PhoneNo))
                {
                    param[30] = new SqlParameter("@ParamCD_PhoneNo", SqlDbType.VarChar, 100);
                    param[30].Value = CD_PhoneNo;
                }
                if (!string.IsNullOrEmpty(CD_MobileNo))
                {
                    param[31] = new SqlParameter("@ParamCD_MobileNo", SqlDbType.VarChar, 100);
                    param[31].Value = CD_MobileNo;
                }
                if (!string.IsNullOrEmpty(CD_FAX))
                {
                    param[32] = new SqlParameter("@ParamCD_FAX", SqlDbType.VarChar, 100);
                    param[32].Value = CD_FAX;
                }
                if (!string.IsNullOrEmpty(CD_EmailAddress))
                {
                    param[33] = new SqlParameter("@ParamCD_EmailAddress", SqlDbType.VarChar, 500);
                    param[33].Value = CD_EmailAddress;
                }
                if (!string.IsNullOrEmpty(CD_Country))
                {
                    param[34] = new SqlParameter("@ParamCD_Country", SqlDbType.VarChar, 200);
                    param[34].Value = CD_Country;
                }
                if (!string.IsNullOrEmpty(CD_State))
                {
                    param[35] = new SqlParameter("@ParamCD_State", SqlDbType.VarChar, 100);
                    param[35].Value = CD_State;
                }
                if (!string.IsNullOrEmpty(CD_City))
                {
                    param[36] = new SqlParameter("@ParamCD_City", SqlDbType.VarChar, 200);
                    param[36].Value = CD_City;
                }
                if (!string.IsNullOrEmpty(CD_Address))
                {
                    param[37] = new SqlParameter("@ParamCD_Address", SqlDbType.VarChar, 2000);
                    param[37].Value = CD_Address;
                }
                if (!string.IsNullOrEmpty(CD_PostCode))
                {
                    param[38] = new SqlParameter("@ParamCD_PostCode", SqlDbType.VarChar, 50);
                    param[38].Value = CD_PostCode;
                }
                if (!string.IsNullOrEmpty(CD_AddressType))
                {
                    param[39] = new SqlParameter("@ParamCD_AddressType", SqlDbType.VarChar, 50);
                    param[39].Value = CD_AddressType;
                }
                if (!string.IsNullOrEmpty(CD_ModifiedBy))
                {
                    param[40] = new SqlParameter("@ParamCD_ModifiedBy", SqlDbType.VarChar, 50);
                    param[40].Value = CD_ModifiedBy;
                }
                param[41] = new SqlParameter("@ParamPassengers", Passengers);
                param[42] = new SqlParameter("@ParamAmountCharges", AmountCharges);
                param[43] = new SqlParameter("@ParamAirSectors", AirSectors);

                param[44] = new SqlParameter("@ParamStatus", SqlDbType.Bit);
                param[44].Direction = ParameterDirection.Output;

                param[45] = new SqlParameter("@ParamErrorNO", SqlDbType.VarChar, 500);
                param[45].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "SET_FlightDetails", param);
                return Convert.ToBoolean(param[44].Value);
            }
        }
        catch
        {
            return false;
        }
    }

    #endregion

    #region Transaction Master

    public string SET_Transaction_Master(string BookingID, string TrnsNo, string TrnsType, string TrnsPaymentStatus,
    string TrnsAmount, string TrnsCurrencyType, string TrnsBy, string TrnsDateTime, string TrnsRemarks,
    string TrnsSecurityKey, string TrnsStatus, string TrnsStatusDetail, string TrnsVSPTxID, string TrnsAuthNo,
    string TrnsAVSCV2, string TrnsAddressResult, string TrnsPostCodeResult, string TrnsCV2Result, string Trns3DSecureStatus,
    string TrnsCAVV, string TrnsModifiedBy, string Counter)
    {
        SqlParameter[] param = new SqlParameter[23];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(BookingID))
                {
                    param[0] = new SqlParameter("@ParamBookingID", SqlDbType.VarChar, 50);
                    param[0].Value = BookingID;
                }
                if (!string.IsNullOrEmpty(TrnsNo))
                {
                    param[1] = new SqlParameter("@ParamTrnsNo", SqlDbType.VarChar, 50);
                    param[1].Value = TrnsNo;
                }
                if (!string.IsNullOrEmpty(TrnsType))
                {
                    param[2] = new SqlParameter("@ParamTrnsType", SqlDbType.VarChar, 200);
                    param[2].Value = TrnsType;
                }
                if (!string.IsNullOrEmpty(TrnsPaymentStatus))
                {
                    param[3] = new SqlParameter("@ParamTrnsPaymentStatus", SqlDbType.VarChar, 100);
                    param[3].Value = TrnsPaymentStatus;
                }
                if (!string.IsNullOrEmpty(TrnsAmount))
                {
                    param[4] = new SqlParameter("@ParamTrnsAmount", SqlDbType.Money);
                    param[4].Value = Convert.ToDouble(TrnsAmount);
                }
                if (!string.IsNullOrEmpty(TrnsCurrencyType))
                {
                    param[5] = new SqlParameter("@ParamTrnsCurrencyType", SqlDbType.VarChar, 50);
                    param[5].Value = TrnsCurrencyType;
                }
                if (!string.IsNullOrEmpty(TrnsBy))
                {
                    param[6] = new SqlParameter("@ParamTrnsBy", SqlDbType.VarChar, 100);
                    param[6].Value = TrnsBy;
                }
                if (!string.IsNullOrEmpty(TrnsDateTime))
                {
                    param[7] = new SqlParameter("@ParamTrnsDateTime", SqlDbType.DateTime);
                    param[7].Value = Convert.ToDateTime(TrnsDateTime);
                }
                if (!string.IsNullOrEmpty(TrnsRemarks))
                {
                    param[8] = new SqlParameter("@ParamTrnsRemarks", SqlDbType.VarChar, 2000);
                    param[8].Value = TrnsRemarks;
                }
                if (!string.IsNullOrEmpty(TrnsSecurityKey))
                {
                    param[9] = new SqlParameter("@ParamTrnsSecurityKey", SqlDbType.VarChar, 50);
                    param[9].Value = TrnsSecurityKey;
                }
                if (!string.IsNullOrEmpty(TrnsStatus))
                {
                    param[10] = new SqlParameter("@ParamTrnsStatus", SqlDbType.VarChar, 50);
                    param[10].Value = TrnsStatus;
                }
                if (!string.IsNullOrEmpty(TrnsStatusDetail))
                {
                    param[11] = new SqlParameter("@ParamTrnsStatusDetail", SqlDbType.VarChar, 500);
                    param[11].Value = TrnsStatusDetail;
                }
                if (!string.IsNullOrEmpty(TrnsVSPTxID))
                {
                    param[12] = new SqlParameter("@ParamTrnsVSPTxID", SqlDbType.VarChar, 50);
                    param[12].Value = TrnsVSPTxID;
                }
                if (!string.IsNullOrEmpty(TrnsAuthNo))
                {
                    param[13] = new SqlParameter("@ParamTrnsAuthNo", SqlDbType.VarChar, 50);
                    param[13].Value = TrnsAuthNo;
                }
                if (!string.IsNullOrEmpty(TrnsAVSCV2))
                {
                    param[14] = new SqlParameter("@ParamTrnsAVSCV2", SqlDbType.VarChar, 50);
                    param[14].Value = TrnsAVSCV2;
                }
                if (!string.IsNullOrEmpty(TrnsAddressResult))
                {
                    param[15] = new SqlParameter("@ParamTrnsAddressResult", SqlDbType.VarChar, 200);
                    param[15].Value = TrnsAddressResult;
                }
                if (!string.IsNullOrEmpty(TrnsPostCodeResult))
                {
                    param[16] = new SqlParameter("@ParamTrnsPostCodeResult", SqlDbType.VarChar, 100);
                    param[16].Value = TrnsPostCodeResult;
                }
                if (!string.IsNullOrEmpty(TrnsCV2Result))
                {
                    param[17] = new SqlParameter("@ParamTrnsCV2Result", SqlDbType.VarChar, 50);
                    param[17].Value = TrnsCV2Result;
                }
                if (!string.IsNullOrEmpty(Trns3DSecureStatus))
                {
                    param[18] = new SqlParameter("@ParamTrns3DSecureStatus", SqlDbType.VarChar, 50);
                    param[18].Value = Trns3DSecureStatus;
                }
                if (!string.IsNullOrEmpty(TrnsCAVV))
                {
                    param[19] = new SqlParameter("@ParamTrnsCAVV", SqlDbType.VarChar, 50);
                    param[19].Value = TrnsCAVV;
                }
                if (!string.IsNullOrEmpty(TrnsModifiedBy))
                {
                    param[20] = new SqlParameter("@ParamTrnsModifiedBy", SqlDbType.VarChar, 50);
                    param[20].Value = TrnsModifiedBy;
                }
                param[21] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[21].Value = Counter;

                param[22] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 500);
                param[22].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_Transaction_Master", param);
                return param[22].Value.ToString();
            }
        }
        catch
        {
            return "false";
        }
    }

    public DataTable GET_Transaction_Master(string BookingID, string TrnsNo)
    {
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(BookingID))
                {
                    param[0] = new SqlParameter("@ParamBookingID", SqlDbType.VarChar, 50);
                    param[0].Value = BookingID;
                }
                if (!string.IsNullOrEmpty(TrnsNo))
                {
                    param[1] = new SqlParameter("@ParamTrnsNo", SqlDbType.VarChar, 50);
                    param[1].Value = TrnsNo;
                }
                param[2] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[2].Value = "Select";

                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_Transaction_Master", param);
                return ds.Tables[0];
            }
        }
        catch
        {
            return null;
        }
    }

    #endregion

    #endregion

    #region Account Details

    #region Company Detail

    public string SET_CompanyDetail(string CompanyID, string CompanyName, string Address1, string Address2, string City,
        string PostalCode, string County, string Country, string PhoneNo, string MobileNo, string FaxNo, string EmailID,
        string IATANo, string ABTANo, string ATOLNo, string ContactNo, string ContactName, string RegDate, string CreditLimit,
        string GroupID, string PayType, string ModifyedBy, string Consortium, string IsSafi, string Counter)
    {
        SqlParameter[] param = new SqlParameter[26];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(CompanyID))
                {
                    param[0] = new SqlParameter("@ParamCompanyID", SqlDbType.VarChar, 50);
                    param[0].Value = CompanyID;
                }
                if (!string.IsNullOrEmpty(CompanyName))
                {
                    param[1] = new SqlParameter("@ParamCompanyName", SqlDbType.VarChar, 100);
                    param[1].Value = CompanyName;
                }
                if (!string.IsNullOrEmpty(Address1))
                {
                    param[2] = new SqlParameter("@ParamAddress1", SqlDbType.VarChar, 500);
                    param[2].Value = Address1;
                }
                if (!string.IsNullOrEmpty(Address2))
                {
                    param[3] = new SqlParameter("@ParamAddress2", SqlDbType.VarChar, 500);
                    param[3].Value = Address2;
                }
                if (!string.IsNullOrEmpty(City))
                {
                    param[4] = new SqlParameter("@ParamCity", SqlDbType.VarChar, 100);
                    param[4].Value = City;
                }
                if (!string.IsNullOrEmpty(PostalCode))
                {
                    param[5] = new SqlParameter("@ParamPostalCode", SqlDbType.VarChar, 50);
                    param[5].Value = PostalCode;
                }
                if (!string.IsNullOrEmpty(County))
                {
                    param[6] = new SqlParameter("@ParamCounty", SqlDbType.VarChar, 100);
                    param[6].Value = County;
                }
                if (!string.IsNullOrEmpty(Country))
                {
                    param[7] = new SqlParameter("@ParamCountry", SqlDbType.VarChar, 100);
                    param[7].Value = Country;
                }
                if (!string.IsNullOrEmpty(PhoneNo))
                {
                    param[8] = new SqlParameter("@ParamPhoneNo", SqlDbType.VarChar, 50);
                    param[8].Value = PhoneNo;
                }
                if (!string.IsNullOrEmpty(MobileNo))
                {
                    param[9] = new SqlParameter("@ParamMobileNo", SqlDbType.VarChar, 50);
                    param[9].Value = MobileNo;
                }
                if (!string.IsNullOrEmpty(FaxNo))
                {
                    param[10] = new SqlParameter("@ParamFaxNo", SqlDbType.VarChar, 50);
                    param[10].Value = FaxNo;
                }
                if (!string.IsNullOrEmpty(EmailID))
                {
                    param[11] = new SqlParameter("@ParamEmailID", SqlDbType.VarChar, 75);
                    param[11].Value = EmailID;
                }
                if (!string.IsNullOrEmpty(IATANo))
                {
                    param[12] = new SqlParameter("@ParamIATANo", SqlDbType.VarChar, 50);
                    param[12].Value = IATANo;
                }
                if (!string.IsNullOrEmpty(ABTANo))
                {
                    param[13] = new SqlParameter("@ParamABTANo", SqlDbType.VarChar, 50);
                    param[13].Value = ABTANo;
                }
                if (!string.IsNullOrEmpty(ATOLNo))
                {
                    param[14] = new SqlParameter("@ParamATOLNo", SqlDbType.VarChar, 50);
                    param[14].Value = ATOLNo;
                }
                if (!string.IsNullOrEmpty(ContactNo))
                {
                    param[15] = new SqlParameter("@ParamContactNo", SqlDbType.VarChar, 50);
                    param[15].Value = ContactNo;
                }
                if (!string.IsNullOrEmpty(ContactName))
                {
                    param[16] = new SqlParameter("@ParamContactName", SqlDbType.VarChar, 75);
                    param[16].Value = ContactName;
                }
                if (!string.IsNullOrEmpty(RegDate))
                {
                    param[17] = new SqlParameter("@ParamRegDate", SqlDbType.DateTime);
                    param[17].Value = Convert.ToDateTime(RegDate);
                }
                if (!string.IsNullOrEmpty(CreditLimit))
                {
                    param[18] = new SqlParameter("@ParamCreditLimit", SqlDbType.Money);
                    param[18].Value = Convert.ToDouble(CreditLimit);
                }
                if (!string.IsNullOrEmpty(GroupID))
                {
                    param[19] = new SqlParameter("@ParamGroupID", SqlDbType.VarChar, 50);
                    param[19].Value = GroupID;
                }
                if (!string.IsNullOrEmpty(PayType))
                {
                    param[20] = new SqlParameter("@ParamPayType", SqlDbType.VarChar, 50);
                    param[20].Value = PayType;
                }
                if (!string.IsNullOrEmpty(ModifyedBy))
                {
                    param[21] = new SqlParameter("@ParamModifyedBy", SqlDbType.VarChar, 50);
                    param[21].Value = ModifyedBy;
                }
                if (!string.IsNullOrEmpty(Consortium))
                {
                    param[22] = new SqlParameter("@ParamConsortium", SqlDbType.VarChar, 50);
                    param[22].Value = Consortium;
                }
                if (!string.IsNullOrEmpty(IsSafi))
                {
                    param[23] = new SqlParameter("@ParamIsSafi", SqlDbType.Bit);
                    param[23].Value = Convert.ToBoolean(IsSafi);
                }

                param[24] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[24].Value = Counter;

                param[25] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 500);
                param[25].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_CompanyDetail", param);
                return param[25].Value.ToString();
            }
        }
        catch
        {
            return "false";
        }
    }

    public DataTable GET_CompanyDetail(string CompanyID, string CompanyName, string City,
        string PostalCode, string County, string Country, string PhoneNo, string MobileNo, string FaxNo, string EmailID,
        string IATANo, string ABTANo, string ATOLNo, string ContactNo, string ContactName,
        string GroupID, string PayType, string Consortium, string IsSafi)
    {
        SqlParameter[] param = new SqlParameter[20];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(CompanyID))
                {
                    param[0] = new SqlParameter("@ParamCompanyID", SqlDbType.VarChar, 50);
                    param[0].Value = CompanyID;
                }
                if (!string.IsNullOrEmpty(CompanyName))
                {
                    param[1] = new SqlParameter("@ParamCompanyName", SqlDbType.VarChar, 100);
                    param[1].Value = CompanyName;
                }

                if (!string.IsNullOrEmpty(City))
                {
                    param[2] = new SqlParameter("@ParamCity", SqlDbType.VarChar, 100);
                    param[2].Value = City;
                }
                if (!string.IsNullOrEmpty(PostalCode))
                {
                    param[3] = new SqlParameter("@ParamPostalCode", SqlDbType.VarChar, 50);
                    param[3].Value = PostalCode;
                }
                if (!string.IsNullOrEmpty(County))
                {
                    param[4] = new SqlParameter("@ParamCounty", SqlDbType.VarChar, 100);
                    param[4].Value = County;
                }
                if (!string.IsNullOrEmpty(Country))
                {
                    param[5] = new SqlParameter("@ParamCountry", SqlDbType.VarChar, 100);
                    param[5].Value = Country;
                }
                if (!string.IsNullOrEmpty(PhoneNo))
                {
                    param[6] = new SqlParameter("@ParamPhoneNo", SqlDbType.VarChar, 50);
                    param[6].Value = PhoneNo;
                }
                if (!string.IsNullOrEmpty(MobileNo))
                {
                    param[7] = new SqlParameter("@ParamMobileNo", SqlDbType.VarChar, 50);
                    param[7].Value = MobileNo;
                }
                if (!string.IsNullOrEmpty(FaxNo))
                {
                    param[8] = new SqlParameter("@ParamFaxNo", SqlDbType.VarChar, 50);
                    param[8].Value = FaxNo;
                }
                if (!string.IsNullOrEmpty(EmailID))
                {
                    param[9] = new SqlParameter("@ParamEmailID", SqlDbType.VarChar, 75);
                    param[9].Value = EmailID;
                }

                if (!string.IsNullOrEmpty(ContactNo))
                {
                    param[13] = new SqlParameter("@ParamContactNo", SqlDbType.VarChar, 50);
                    param[13].Value = ContactNo;
                }
                if (!string.IsNullOrEmpty(ContactName))
                {
                    param[14] = new SqlParameter("@ParamContactName", SqlDbType.VarChar, 75);
                    param[14].Value = ContactName;
                }


                param[19] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[19].Value = "Select";

                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_CompanyDetail", param);
                return ds.Tables[0];
            }
        }
        catch
        {
            return null;
        }
    }

    public DataTable GET_User_Company_Access(string UserID)
    {
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(UserID))
                {
                    param[0] = new SqlParameter("@ParamUserID", SqlDbType.VarChar, 50);
                    param[0].Value = UserID;
                }

                param[1] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[1].Value = "SelectUserCompany";

                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_Auth_Company_Authorization", param);
                return ds.Tables[0];
            }
        }
        catch
        {
            return null;
        }
    }

    #endregion

    #region UserAccoun

    public string SET_UserAccount(string CompanyID, string UserID, string UserPassword, string UserTitle, string UserFirstName,
        string UserLastName, string UserType, string UserisActive, string UserRole, string ModifiedBy, string ModifiedDateTime,
        string RegisterDateTime, string RegisterBy, string isIpCheck, string Counter)
    {
        SqlParameter[] param = new SqlParameter[16];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(CompanyID))
                {
                    param[0] = new SqlParameter("@ParamCompanyID", SqlDbType.VarChar, 50);
                    param[0].Value = CompanyID;
                }
                if (!string.IsNullOrEmpty(UserID))
                {
                    param[1] = new SqlParameter("@ParamUserID", SqlDbType.VarChar, 50);
                    param[1].Value = UserID;
                }
                if (!string.IsNullOrEmpty(UserPassword))
                {
                    param[2] = new SqlParameter("@ParamUserPassword", SqlDbType.VarChar, 50);
                    param[2].Value = UserPassword;
                }
                if (!string.IsNullOrEmpty(UserTitle))
                {
                    param[3] = new SqlParameter("@ParamUserTitle", SqlDbType.VarChar, 50);
                    param[3].Value = UserTitle;
                }
                if (!string.IsNullOrEmpty(UserFirstName))
                {
                    param[4] = new SqlParameter("@ParamUserFirstName", SqlDbType.VarChar, 50);
                    param[4].Value = UserFirstName;
                }
                if (!string.IsNullOrEmpty(UserLastName))
                {
                    param[5] = new SqlParameter("@ParamUserLastName", SqlDbType.VarChar, 50);
                    param[5].Value = UserLastName;
                }
                if (!string.IsNullOrEmpty(UserType))
                {
                    param[6] = new SqlParameter("@ParamUserType", SqlDbType.VarChar, 50);
                    param[6].Value = UserType;
                }
                if (!string.IsNullOrEmpty(UserisActive))
                {
                    param[7] = new SqlParameter("@ParamUserisActive", SqlDbType.Bit);
                    param[7].Value = Convert.ToBoolean(UserisActive);
                }
                if (!string.IsNullOrEmpty(UserRole))
                {
                    param[8] = new SqlParameter("@ParamUserRole", SqlDbType.VarChar, 50);
                    param[8].Value = UserRole;
                }
                if (!string.IsNullOrEmpty(ModifiedBy))
                {
                    param[9] = new SqlParameter("@ParamModifiedBy", SqlDbType.VarChar, 50);
                    param[9].Value = ModifiedBy;
                }
                if (!string.IsNullOrEmpty(ModifiedDateTime))
                {
                    param[10] = new SqlParameter("@ParamModifiedDateTime", SqlDbType.DateTime);
                    param[10].Value = Convert.ToDateTime(ModifiedDateTime);
                }
                if (!string.IsNullOrEmpty(RegisterDateTime))
                {
                    param[11] = new SqlParameter("@ParamRegisterDateTime", SqlDbType.DateTime);
                    param[11].Value = Convert.ToDateTime(RegisterDateTime);
                }
                if (!string.IsNullOrEmpty(RegisterBy))
                {
                    param[12] = new SqlParameter("@ParamRegisterBy", SqlDbType.VarChar, 50);
                    param[12].Value = RegisterBy;
                }
                param[13] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[13].Value = Counter;

                param[14] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 500);
                param[14].Direction = ParameterDirection.Output;
                if (!string.IsNullOrEmpty(isIpCheck))
                {
                    param[15] = new SqlParameter("@ParamIs_IP_Check", SqlDbType.Bit);
                    param[15].Value = Convert.ToBoolean(isIpCheck);
                }
                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_UserAccount", param);
                return param[14].Value.ToString();
            }
        }
        catch
        {
            return "false";
        }
    }

    public DataTable GET_UserAccount(string CompanyID, string UserID, string UserFirstName, string UserLastName, string UserType,
        string UserisActive, string UserRole)
    {
        SqlParameter[] param = new SqlParameter[8];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(CompanyID))
                {
                    param[0] = new SqlParameter("@ParamCompanyID", SqlDbType.VarChar, 50);
                    param[0].Value = CompanyID;
                }
                if (!string.IsNullOrEmpty(UserID))
                {
                    param[1] = new SqlParameter("@ParamUserID", SqlDbType.VarChar, 50);
                    param[1].Value = UserID;
                }
                if (!string.IsNullOrEmpty(UserFirstName))
                {
                    param[2] = new SqlParameter("@ParamUserFirstName", SqlDbType.VarChar, 50);
                    param[2].Value = UserFirstName;
                }
                if (!string.IsNullOrEmpty(UserLastName))
                {
                    param[3] = new SqlParameter("@ParamUserLastName", SqlDbType.VarChar, 50);
                    param[3].Value = UserLastName;
                }
                if (!string.IsNullOrEmpty(UserType))
                {
                    param[4] = new SqlParameter("@ParamUserType", SqlDbType.VarChar, 50);
                    param[4].Value = UserType;
                }
                if (!string.IsNullOrEmpty(UserisActive))
                {
                    param[5] = new SqlParameter("@ParamUserisActive", SqlDbType.Bit);
                    param[5].Value = Convert.ToBoolean(UserisActive);
                }
                if (!string.IsNullOrEmpty(UserRole))
                {
                    param[6] = new SqlParameter("@ParamUserRole", SqlDbType.VarChar, 50);
                    param[6].Value = UserRole;
                }
                param[7] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[7].Value = "Select";

                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_UserAccount", param);
                return ds.Tables[0];
            }
        }
        catch
        {
            return null;
        }
    }
    public DataTable GET_UserAccount(string CompanyID, string UserID, string UserFirstName, string UserLastName, string UserType,
        string UserisActive, string UserRole, string Counter)
    {
        SqlParameter[] param = new SqlParameter[8];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(CompanyID))
                {
                    param[0] = new SqlParameter("@ParamCompanyID", SqlDbType.VarChar, 50);
                    param[0].Value = CompanyID;
                }
                if (!string.IsNullOrEmpty(UserID))
                {
                    param[1] = new SqlParameter("@ParamUserID", SqlDbType.VarChar, 50);
                    param[1].Value = UserID;
                }
                if (!string.IsNullOrEmpty(UserFirstName))
                {
                    param[2] = new SqlParameter("@ParamUserFirstName", SqlDbType.VarChar, 50);
                    param[2].Value = UserFirstName;
                }
                if (!string.IsNullOrEmpty(UserLastName))
                {
                    param[3] = new SqlParameter("@ParamUserLastName", SqlDbType.VarChar, 50);
                    param[3].Value = UserLastName;
                }
                if (!string.IsNullOrEmpty(UserType))
                {
                    param[4] = new SqlParameter("@ParamUserType", SqlDbType.VarChar, 50);
                    param[4].Value = UserType;
                }
                if (!string.IsNullOrEmpty(UserisActive))
                {
                    param[5] = new SqlParameter("@ParamUserisActive", SqlDbType.Bit);
                    param[5].Value = Convert.ToBoolean(UserisActive);
                }
                if (!string.IsNullOrEmpty(UserRole))
                {
                    param[6] = new SqlParameter("@ParamUserRole", SqlDbType.VarChar, 50);
                    param[6].Value = UserRole;
                }
                param[7] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[7].Value = Counter;

                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_UserAccount", param);
                return ds.Tables[0];
            }
        }
        catch
        {
            return null;
        }
    }

    #endregion

    #region User Account Details

    public string SET_UserAccountDetails(string SrNo, string UserID, string Address1, string Address2, string City,
        string PostalCode, string Country, string PhoneNo, string MobileNo, string EmailID, string ModifiedBy, string Counter)
    {
        SqlParameter[] param = new SqlParameter[13];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(SrNo))
                {
                    param[0] = new SqlParameter("@ParamSrNo", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(SrNo);
                }
                if (!string.IsNullOrEmpty(UserID))
                {
                    param[1] = new SqlParameter("@ParamUserID", SqlDbType.VarChar, 50);
                    param[1].Value = UserID;
                }
                if (!string.IsNullOrEmpty(Address1))
                {
                    param[2] = new SqlParameter("@ParamAddress1", SqlDbType.VarChar, 500);
                    param[2].Value = Address1;
                }
                if (!string.IsNullOrEmpty(Address2))
                {
                    param[3] = new SqlParameter("@ParamAddress2", SqlDbType.VarChar, 500);
                    param[3].Value = Address2;
                }
                if (!string.IsNullOrEmpty(City))
                {
                    param[4] = new SqlParameter("@ParamCity", SqlDbType.VarChar, 50);
                    param[4].Value = City;
                }
                if (!string.IsNullOrEmpty(PostalCode))
                {
                    param[5] = new SqlParameter("@ParamPostalCode", SqlDbType.VarChar, 50);
                    param[5].Value = PostalCode;
                }
                if (!string.IsNullOrEmpty(Country))
                {
                    param[6] = new SqlParameter("@ParamCountry", SqlDbType.VarChar, 100);
                    param[6].Value = Country;
                }
                if (!string.IsNullOrEmpty(PhoneNo))
                {
                    param[7] = new SqlParameter("@ParamPhoneNo", SqlDbType.VarChar, 50);
                    param[7].Value = PhoneNo;
                }
                if (!string.IsNullOrEmpty(MobileNo))
                {
                    param[8] = new SqlParameter("@ParamMobileNo", SqlDbType.VarChar, 50);
                    param[8].Value = MobileNo;
                }
                if (!string.IsNullOrEmpty(EmailID))
                {
                    param[9] = new SqlParameter("@ParamEmailID", SqlDbType.VarChar, 75);
                    param[9].Value = EmailID;
                }
                if (!string.IsNullOrEmpty(ModifiedBy))
                {
                    param[10] = new SqlParameter("@ParamModifiedBy", SqlDbType.VarChar, 50);
                    param[10].Value = ModifiedBy;
                }
                param[11] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[11].Value = Counter;

                param[12] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 500);
                param[12].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_UserAccountDetails", param);
                return param[12].Value.ToString();
            }
        }
        catch
        {
            return "false";
        }
    }

    public DataTable GET_UserAccountDetails(string SrNo, string UserID)
    {
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(SrNo))
                {
                    param[0] = new SqlParameter("@ParamSrNo", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(SrNo);
                }
                if (!string.IsNullOrEmpty(UserID))
                {
                    param[1] = new SqlParameter("@ParamUserID", SqlDbType.VarChar, 50);
                    param[1].Value = UserID;
                }
                param[2] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[2].Value = "Select";

                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_UserAccountDetails", param);
                return ds.Tables[0];
            }
        }
        catch
        {
            return null;
        }
    }

    #endregion

    #region CampaignMaster

    public string SET_Campaign_Master(string CompanyID, string CampID, string CampName, string CampPassword, string CampAddress,
        string CampCity, string CampCountry, string CampPhone, string CampEmailId, string CampContactName, string CampContactEmail,
        string CampContactNumber, string CampModifiledBy, string CampisActive, string CampRemarks, string Counter)
    {
        SqlParameter[] param = new SqlParameter[17];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(CompanyID))
                {
                    param[0] = new SqlParameter("@ParamCompanyID", SqlDbType.VarChar, 50);
                    param[0].Value = CompanyID;
                }
                if (!string.IsNullOrEmpty(CampID))
                {
                    param[1] = new SqlParameter("@ParamCampID", SqlDbType.VarChar, 50);
                    param[1].Value = CampID;
                }
                if (!string.IsNullOrEmpty(CampName))
                {
                    param[2] = new SqlParameter("@ParamCampName", SqlDbType.VarChar, 200);
                    param[2].Value = CampName;
                }
                if (!string.IsNullOrEmpty(CampPassword))
                {
                    param[3] = new SqlParameter("@ParamCampPassword", SqlDbType.VarChar, 200);
                    param[3].Value = CampPassword;
                }
                if (!string.IsNullOrEmpty(CampAddress))
                {
                    param[4] = new SqlParameter("@ParamCampAddress", SqlDbType.VarChar, 500);
                    param[4].Value = CampAddress;
                }
                if (!string.IsNullOrEmpty(CampCity))
                {
                    param[5] = new SqlParameter("@ParamCampCity", SqlDbType.VarChar, 100);
                    param[5].Value = CampCity;
                }
                if (!string.IsNullOrEmpty(CampCountry))
                {
                    param[6] = new SqlParameter("@ParamCampCountry", SqlDbType.VarChar, 100);
                    param[6].Value = CampCountry;
                }
                if (!string.IsNullOrEmpty(CampPhone))
                {
                    param[7] = new SqlParameter("@ParamCampPhone", SqlDbType.VarChar, 50);
                    param[7].Value = CampPhone;
                }
                if (!string.IsNullOrEmpty(CampEmailId))
                {
                    param[8] = new SqlParameter("@ParamCampEmailId", SqlDbType.VarChar, 50);
                    param[8].Value = CampEmailId;
                }
                if (!string.IsNullOrEmpty(CampContactName))
                {
                    param[9] = new SqlParameter("@ParamCampContactName", SqlDbType.VarChar, 100);
                    param[9].Value = CampContactName;
                }
                if (!string.IsNullOrEmpty(CampContactEmail))
                {
                    param[10] = new SqlParameter("@ParamCampContactEmail", SqlDbType.VarChar, 50);
                    param[10].Value = CampContactEmail;
                }
                if (!string.IsNullOrEmpty(CampContactNumber))
                {
                    param[11] = new SqlParameter("@ParamCampContactNumber", SqlDbType.VarChar, 50);
                    param[11].Value = CampContactNumber;
                }
                if (!string.IsNullOrEmpty(CampModifiledBy))
                {
                    param[12] = new SqlParameter("@ParamCampModifiledBy", SqlDbType.VarChar, 50);
                    param[12].Value = CampModifiledBy;
                }
                if (!string.IsNullOrEmpty(CampisActive))
                {
                    param[13] = new SqlParameter("@ParamCampisActive", SqlDbType.Bit);
                    param[13].Value = Convert.ToBoolean(CampisActive);
                }
                if (!string.IsNullOrEmpty(CampRemarks))
                {
                    param[14] = new SqlParameter("@ParamCampRemarks", SqlDbType.VarChar, 2000);
                    param[14].Value = CampRemarks;
                }
                param[15] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[15].Value = Counter;

                param[16] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 500);
                param[16].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_Campaign_Master", param);
                return param[16].Value.ToString();
            }
        }
        catch
        {
            return "false";
        }
    }

    public DataTable GET_Campaign_Master(string CampID, string CompanyID)
    {
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(CampID))
                {
                    param[0] = new SqlParameter("@ParamCampID", SqlDbType.VarChar, 50);
                    param[0].Value = CampID;
                }
                if (!string.IsNullOrEmpty(CompanyID))
                {
                    param[1] = new SqlParameter("@ParamCompanyID", SqlDbType.VarChar, 50);
                    param[1].Value = CompanyID;
                }
                param[2] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[2].Value = "Select";
                if (string.IsNullOrEmpty(CampID) && string.IsNullOrEmpty(CompanyID))
                {
                    DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_Campaign_Master", param);
                    return ds.Tables[0];
                }
                else
                {
                    DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_Campaign_Master", param);
                    return ds.Tables[0];
                }
            }
        }
        catch
        {
            return null;
        }
    }

    public DataTable GET_Pref_Campaign_Master()
    {
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {

                param[2] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[2].Value = "Prefered";

                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_Campaign_Master", param);
                return ds.Tables[0];

            }
        }
        catch
        {
            return null;
        }
    }


    public DataTable GET_SourceMedia()
    {

        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "Get_SourceMedia");
                return ds.Tables[0];

            }
        }
        catch(Exception ex)
        {
            return null;
        }
    }
    #endregion

    #region Credential Master

    public string SET_CredentialMaster(string CredentialID, string CompanyID, string ProductType, string ProductCode,
        string UserID, string Password, string PsuedoCode, string WSAPORSession, string OrdID, string PassLen, string ID,
        string Type, string Active, string ServiceURL, string NameSpace, string NoOfFares, string ModifiedBy, string Counter)
    {
        SqlParameter[] param = new SqlParameter[19];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(CredentialID))
                {
                    param[0] = new SqlParameter("@ParamCredentialID", SqlDbType.VarChar, 50);
                    param[0].Value = CredentialID;
                }
                if (!string.IsNullOrEmpty(CompanyID))
                {
                    param[1] = new SqlParameter("@ParamCompanyID", SqlDbType.VarChar, 50);
                    param[1].Value = CompanyID;
                }
                if (!string.IsNullOrEmpty(ProductType))
                {
                    param[2] = new SqlParameter("@ParamProductType", SqlDbType.VarChar, 50);
                    param[2].Value = ProductType;
                }
                if (!string.IsNullOrEmpty(ProductCode))
                {
                    param[3] = new SqlParameter("@ParamProductCode", SqlDbType.VarChar, 50);
                    param[3].Value = ProductCode;
                }
                if (!string.IsNullOrEmpty(UserID))
                {
                    param[4] = new SqlParameter("@ParamUserID", SqlDbType.VarChar, 100);
                    param[4].Value = UserID;
                }
                if (!string.IsNullOrEmpty(Password))
                {
                    param[5] = new SqlParameter("@ParamPassword", SqlDbType.VarChar, 100);
                    param[5].Value = Password;
                }
                if (!string.IsNullOrEmpty(PsuedoCode))
                {
                    param[6] = new SqlParameter("@ParamPsuedoCode", SqlDbType.VarChar, 100);
                    param[6].Value = PsuedoCode;
                }
                if (!string.IsNullOrEmpty(WSAPORSession))
                {
                    param[7] = new SqlParameter("@ParamWSAPORSession", SqlDbType.VarChar, 100);
                    param[7].Value = WSAPORSession;
                }
                if (!string.IsNullOrEmpty(OrdID))
                {
                    param[8] = new SqlParameter("@ParamOrdID", SqlDbType.VarChar, 100);
                    param[8].Value = OrdID;
                }
                if (!string.IsNullOrEmpty(PassLen))
                {
                    param[9] = new SqlParameter("@ParamPassLen", SqlDbType.VarChar, 100);
                    param[9].Value = PassLen;
                }
                if (!string.IsNullOrEmpty(ID))
                {
                    param[10] = new SqlParameter("@ParamID", SqlDbType.VarChar, 20);
                    param[10].Value = ID;
                }
                if (!string.IsNullOrEmpty(Type))
                {
                    param[11] = new SqlParameter("@ParamType", SqlDbType.VarChar, 20);
                    param[11].Value = Type;
                }
                if (!string.IsNullOrEmpty(Active))
                {
                    param[12] = new SqlParameter("@ParamActive", SqlDbType.Bit);
                    param[12].Value = Convert.ToBoolean(Active);
                }
                if (!string.IsNullOrEmpty(ServiceURL))
                {
                    param[13] = new SqlParameter("@ParamServiceURL", SqlDbType.VarChar, 100);
                    param[13].Value = ServiceURL;
                }
                if (!string.IsNullOrEmpty(NameSpace))
                {
                    param[14] = new SqlParameter("@ParamNameSpace", SqlDbType.VarChar, 100);
                    param[14].Value = NameSpace;
                }
                if (!string.IsNullOrEmpty(NoOfFares))
                {
                    param[15] = new SqlParameter("@ParamNoOfFares", SqlDbType.Int);
                    param[15].Value = Convert.ToInt32(NoOfFares);
                }
                if (!string.IsNullOrEmpty(ModifiedBy))
                {
                    param[16] = new SqlParameter("@ParamModifiedBy", SqlDbType.VarChar, 50);
                    param[16].Value = ModifiedBy;
                }
                param[17] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[17].Value = Counter;

                param[18] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 500);
                param[18].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_CredentialMaster", param);
                return param[18].Value.ToString();
            }
        }
        catch
        {
            return "false";
        }
    }

    public DataTable GET_CredentialMaster(string CredentialID, string CompanyID, string ProductType, string ProductCode)
    {
        SqlParameter[] param = new SqlParameter[5];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(CredentialID))
                {
                    param[0] = new SqlParameter("@ParamCredentialID", SqlDbType.VarChar, 50);
                    param[0].Value = CredentialID;
                }
                if (!string.IsNullOrEmpty(CompanyID))
                {
                    param[1] = new SqlParameter("@ParamCompanyID", SqlDbType.VarChar, 50);
                    param[1].Value = CompanyID;
                }
                if (!string.IsNullOrEmpty(ProductType))
                {
                    param[2] = new SqlParameter("@ParamProductType", SqlDbType.VarChar, 50);
                    param[2].Value = ProductType;
                }
                if (!string.IsNullOrEmpty(ProductCode))
                {
                    param[3] = new SqlParameter("@ParamProductCode", SqlDbType.VarChar, 50);
                    param[3].Value = ProductCode;
                }
                param[4] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[4].Value = "Select";

                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_CredentialMaster", param);
                return ds.Tables[0];
            }
        }
        catch
        {
            return null;
        }
    }

    #endregion

    #region CredentialDetails

    public string SET_CredentialDetails(string SrNo, string CampID, string CredentialID, string Active, string ModifiedBy,
        string Counter)
    {
        SqlParameter[] param = new SqlParameter[7];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(SrNo))
                {
                    param[0] = new SqlParameter("@ParamSrNo", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(SrNo);
                }
                if (!string.IsNullOrEmpty(CampID))
                {
                    param[1] = new SqlParameter("@ParamCampID", SqlDbType.VarChar, 50);
                    param[1].Value = CampID;
                }
                if (!string.IsNullOrEmpty(CredentialID))
                {
                    param[2] = new SqlParameter("@ParamCredentialID", SqlDbType.VarChar, 50);
                    param[2].Value = CredentialID;
                }
                if (!string.IsNullOrEmpty(Active))
                {
                    param[3] = new SqlParameter("@ParamActive", SqlDbType.Bit);
                    param[3].Value = Convert.ToBoolean(Active);
                }
                if (!string.IsNullOrEmpty(ModifiedBy))
                {
                    param[4] = new SqlParameter("@ParamModifiedBy", SqlDbType.VarChar, 50);
                    param[4].Value = ModifiedBy;
                }

                param[5] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[5].Value = Counter;

                param[6] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 500);
                param[6].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_CredentialDetails", param);
                return param[6].Value.ToString();
            }
        }
        catch
        {
            return "false";
        }
    }

    public DataTable GET_CredentialDetails(string CampID, string CredentialID)
    {
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(CampID))
                {
                    param[0] = new SqlParameter("@ParamCampID", SqlDbType.VarChar, 50);
                    param[0].Value = CampID;
                }
                if (!string.IsNullOrEmpty(CredentialID))
                {
                    param[1] = new SqlParameter("@ParamCredentialID", SqlDbType.VarChar, 50);
                    param[1].Value = CredentialID;
                }
                param[2] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[2].Value = "Select";

                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_CredentialDetails", param);
                return ds.Tables[0];
            }
        }
        catch
        {
            return null;
        }
    }

    #endregion

    #region Add Black listed IP

    public DataTable GET_BlackListedIP(string SrNo, string IP, string WebsiteFor, string isAuthentication)
    {
        SqlParameter[] param = new SqlParameter[5];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!String.IsNullOrEmpty(SrNo))
                {
                    param[0] = new SqlParameter("@ParamSrNo", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(SrNo);
                }

                if (!String.IsNullOrEmpty(IP))
                {
                    param[1] = new SqlParameter("@ParamIP", SqlDbType.VarChar, 50);
                    param[1].Value = IP;
                }

                if (!String.IsNullOrEmpty(WebsiteFor))
                {
                    param[2] = new SqlParameter("@ParamWebsiteFor", SqlDbType.VarChar, 50);
                    param[2].Value = WebsiteFor;
                }
                if (!String.IsNullOrEmpty(isAuthentication))
                {
                    param[3] = new SqlParameter("@ParamAuthentication", SqlDbType.Bit);
                    param[3].Value = Convert.ToBoolean(isAuthentication);
                }
                param[4] = new SqlParameter("@Counter", SqlDbType.NVarChar, 500);
                param[4].Value = "SELECT";

                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_BlackListedIP", param);
                return ds.Tables[0];
            }
        }
        catch
        {
            return null;
        }
    }

    public string SET_BlackListedIP(string SrNo, string IP, string WebsiteFor, string Authentication, string ModifiedBy, string Counter)
    {
        SqlParameter[] param = new SqlParameter[7];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!String.IsNullOrEmpty(SrNo))
                {
                    param[0] = new SqlParameter("@ParamSrNo", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(SrNo);
                }

                if (!String.IsNullOrEmpty(IP))
                {
                    param[1] = new SqlParameter("@ParamIP", SqlDbType.VarChar, 50);
                    param[1].Value = IP;
                }

                if (!String.IsNullOrEmpty(WebsiteFor))
                {
                    param[2] = new SqlParameter("@ParamWebsiteFor", SqlDbType.VarChar, 50);
                    param[2].Value = WebsiteFor;
                }
                if (!String.IsNullOrEmpty(Authentication))
                {
                    param[3] = new SqlParameter("@ParamAuthentication", SqlDbType.Bit);
                    param[3].Value = Convert.ToBoolean(Authentication);
                }
                if (!String.IsNullOrEmpty(ModifiedBy))
                {
                    param[4] = new SqlParameter("@ParamModifiedBy", SqlDbType.VarChar, 50);
                    param[4].Value = ModifiedBy;
                }
                param[5] = new SqlParameter("@ParamStatus", SqlDbType.NVarChar, 500);
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@Counter", SqlDbType.NVarChar, 500);
                param[6].Value = Counter;

                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_BlackListedIP", param);

                return param[5].Value.ToString();
            }
        }
        catch
        {
            return "false";
        }
    }

    #endregion

    #endregion

    #region Page Permission

    public DataTable GET_Auth_PageGroup(int GroupID, string GroupName)
    {
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (GroupID > 0)
                {
                    param[0] = new SqlParameter("@ParamGroupID", SqlDbType.Int);
                    param[0].Value = GroupID;
                }

                if (!String.IsNullOrEmpty(GroupName))
                {
                    param[1] = new SqlParameter("@ParamGroupName", SqlDbType.NVarChar, 50);
                    param[1].Value = GroupName;
                }
                param[2] = new SqlParameter("@Counter", SqlDbType.NVarChar, 500);
                param[2].Value = "SELECT";

                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_Auth_PageGroup", param);
                return ds.Tables[0];
            }
        }
        catch
        {
            return null;
        }
    }

    public string SET_Auth_PageGroup(int GroupID, string GroupName, string GroupDetail, int GroupSequence, string Counter)
    {
        SqlParameter[] param = new SqlParameter[6];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (GroupID > 0)
                {
                    param[0] = new SqlParameter("@ParamGroupID", SqlDbType.Int);
                    param[0].Value = GroupID;
                }

                if (!String.IsNullOrEmpty(GroupName))
                {
                    param[1] = new SqlParameter("@ParamGroupName", SqlDbType.NVarChar, 50);
                    param[1].Value = GroupName;
                }

                if (!String.IsNullOrEmpty(GroupDetail))
                {
                    param[2] = new SqlParameter("@ParamGroupDetail", SqlDbType.NVarChar, 500);
                    param[2].Value = GroupDetail;
                }
                if (GroupSequence > 0)
                {
                    param[3] = new SqlParameter("@ParamGroupSequence", SqlDbType.Int);
                    param[3].Value = GroupSequence;
                }
                param[4] = new SqlParameter("@ParamStatus", SqlDbType.NVarChar, 500);
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@Counter", SqlDbType.NVarChar, 500);
                param[5].Value = Counter;

                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_Auth_PageGroup", param);

                return param[4].Value.ToString();
            }
        }
        catch
        {
            return "false";
        }
    }

    public DataTable GET_Auth_PageName(int PageID, string PageUrl, int GroupID)
    {
        SqlParameter[] param = new SqlParameter[4];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (PageID > 0)
                {
                    param[0] = new SqlParameter("@ParamPageID", SqlDbType.Int);
                    param[0].Value = PageID;
                }

                if (!String.IsNullOrEmpty(PageUrl))
                {
                    param[1] = new SqlParameter("@ParamPageUrl", SqlDbType.NVarChar, 100);
                    param[1].Value = PageUrl;
                }
                if (GroupID > 0)
                {
                    param[2] = new SqlParameter("@ParamGroupID", SqlDbType.Int);
                    param[2].Value = GroupID;
                }
                param[3] = new SqlParameter("@Counter", SqlDbType.NVarChar, 500);
                param[3].Value = "SELECT";

                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_Auth_PageName", param);
                return ds.Tables[0];
            }
        }
        catch
        {
            return null;
        }
    }

    public string SET_Auth_PageName(int PageID, string PageName, string PageUrl, string PageDescription, int GroupID, string Counter)
    {
        SqlParameter[] param = new SqlParameter[7];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (PageID > 0)
                {
                    param[0] = new SqlParameter("@ParamPageID", SqlDbType.Int);
                    param[0].Value = PageID;
                }
                if (!String.IsNullOrEmpty(PageName))
                {
                    param[1] = new SqlParameter("@ParamPageName", SqlDbType.NVarChar, 50);
                    param[1].Value = PageName;
                }
                if (!String.IsNullOrEmpty(PageUrl))
                {
                    param[2] = new SqlParameter("@ParamPageUrl", SqlDbType.NVarChar, 100);
                    param[2].Value = PageUrl;
                }
                if (!String.IsNullOrEmpty(PageDescription))
                {
                    param[3] = new SqlParameter("@ParamPageDescription", SqlDbType.NVarChar, 500);
                    param[3].Value = PageDescription;
                }
                if (GroupID > 0)
                {
                    param[4] = new SqlParameter("@ParamGroupID", SqlDbType.Int);
                    param[4].Value = GroupID;
                }

                param[5] = new SqlParameter("@ParamStatus", SqlDbType.NVarChar, 500);
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@Counter", SqlDbType.NVarChar, 500);
                param[6].Value = Counter;

                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_Auth_PageName", param);

                return param[5].Value.ToString();
            }
        }
        catch
        {
            return "false";
        }
    }

    //public DataTable GET_Auth_PageAuthorization(string UserID, int PageID, string PageUrl, string Counter)
    //{
    //    SqlParameter[] param = new SqlParameter[4];
    //    try
    //    {
    //        using (SqlConnection conection = DataConnection.GetConnection())
    //        {
    //            if (!String.IsNullOrEmpty(UserID))
    //            {
    //                param[0] = new SqlParameter("@ParamUserID", SqlDbType.NVarChar, 50);
    //                param[0].Value = UserID;
    //            }
    //            if (PageID > 0)
    //            {
    //                param[1] = new SqlParameter("@ParamPageID", SqlDbType.Int);
    //                param[1].Value = PageID;
    //            }
    //            if (!String.IsNullOrEmpty(PageUrl))
    //            {
    //                param[2] = new SqlParameter("@ParamPageUrl", SqlDbType.NVarChar, 100);
    //                param[2].Value = PageUrl;
    //            }
    //            param[3] = new SqlParameter("@Counter", SqlDbType.NVarChar, 500);
    //            param[3].Value = Counter;

    //            DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_Auth_PageAuthorization", param);
    //            return ds.Tables[0];
    //        }
    //    }
    //    catch
    //    {
    //        return null;
    //    }
    //}

    //public string SET_Auth_PageAuthorization(string UserID, int PageID, string PageUrl, string Counter)
    //{
    //    SqlParameter[] param = new SqlParameter[5];
    //    try
    //    {
    //        using (SqlConnection conection = DataConnection.GetConnection())
    //        {
    //            if (!String.IsNullOrEmpty(UserID))
    //            {
    //                param[0] = new SqlParameter("@ParamUserID", SqlDbType.NVarChar, 50);
    //                param[0].Value = UserID;
    //            }
    //            if (PageID > 0)
    //            {
    //                param[1] = new SqlParameter("@ParamPageID", SqlDbType.Int);
    //                param[1].Value = PageID;
    //            }
    //            if (!String.IsNullOrEmpty(PageUrl))
    //            {
    //                param[2] = new SqlParameter("@ParamPageUrl", SqlDbType.NVarChar, 100);
    //                param[2].Value = PageUrl;
    //            }

    //            param[3] = new SqlParameter("@ParamStatus", SqlDbType.NVarChar, 500);
    //            param[3].Direction = ParameterDirection.Output;

    //            param[4] = new SqlParameter("@Counter", SqlDbType.NVarChar, 500);
    //            param[4].Value = Counter;

    //            SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_Auth_PageAuthorization", param);

    //            return param[3].Value.ToString();
    //        }
    //    }
    //    catch
    //    {
    //        return "false";
    //    }
    //}

    //public DataTable GetAdminUserDetails(string UserID, string CompanyCode, string UserType,
    //    bool IsActive, string UserRole)
    //{
    //    SqlParameter[] param = new SqlParameter[5];
    //    try
    //    {
    //        using (SqlConnection conection = DataConnection.GetConnection())
    //        {
    //            if (!string.IsNullOrEmpty(UserID)) { param[0] = new SqlParameter("@ParamUserID", SqlDbType.NVarChar, 50); param[0].Value = UserID; }
    //            if (!string.IsNullOrEmpty(CompanyCode))
    //            {
    //                param[1] = new SqlParameter("@ParamCompanyCode", SqlDbType.NVarChar, 50);
    //                param[1].Value = CompanyCode;
    //            }
    //            if (!string.IsNullOrEmpty(UserType))
    //            {
    //                param[2] = new SqlParameter("@ParamUserType", SqlDbType.NVarChar, 50);
    //                param[2].Value = UserType;
    //            }
    //            param[3] = new SqlParameter("@ParamIsActive", SqlDbType.Bit);
    //            param[3].Value = IsActive;
    //            if (!string.IsNullOrEmpty(UserRole))
    //            {
    //                param[4] = new SqlParameter("@ParamUserRole", SqlDbType.NVarChar, 50);
    //                param[4].Value = UserRole;
    //            }
    //            DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "get_ACC_UserAccount", param);
    //            return ds.Tables[0];
    //        }
    //    }
    //    catch
    //    {
    //        return null;
    //    }

    //}

    public DataTable GET_Auth_Page_Option(string OptionID, string OptionName, string PageID, string Counter)
    {
        SqlParameter[] param = new SqlParameter[4];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!String.IsNullOrEmpty(OptionID))
                {
                    param[0] = new SqlParameter("@ParamOptionID", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(OptionID);
                }
                if (!String.IsNullOrEmpty(OptionName))
                {
                    param[1] = new SqlParameter("@ParamOptionName", SqlDbType.NVarChar, 50);
                    param[1].Value = OptionName;
                }
                if (!String.IsNullOrEmpty(PageID))
                {
                    param[2] = new SqlParameter("@ParamPageID", SqlDbType.Int);
                    param[2].Value = Convert.ToInt32(PageID);
                }
                param[3] = new SqlParameter("@Counter", SqlDbType.NVarChar, 500);
                param[3].Value = Counter;

                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_Auth_Page_Option", param);
                return ds.Tables[0];
            }
        }
        catch
        {
            return null;
        }
    }

    public string SET_Auth_Page_Option(string OptionID, string OptionName, string OptionfullName, string OptionDescription,
        string PageID, string Counter)
    {
        SqlParameter[] param = new SqlParameter[7];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!String.IsNullOrEmpty(OptionID))
                {
                    param[0] = new SqlParameter("@ParamOptionID", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(OptionID);
                }
                if (!String.IsNullOrEmpty(OptionName))
                {
                    param[1] = new SqlParameter("@ParamOptionName", SqlDbType.NVarChar, 50);
                    param[1].Value = OptionName;
                }
                if (!String.IsNullOrEmpty(OptionfullName))
                {
                    param[2] = new SqlParameter("@ParamOptionfullName", SqlDbType.NVarChar, 500);
                    param[2].Value = OptionfullName;
                }
                if (!String.IsNullOrEmpty(OptionDescription))
                {
                    param[3] = new SqlParameter("@ParamOptionDescription", SqlDbType.NVarChar, 500);
                    param[3].Value = OptionDescription;
                }
                if (!String.IsNullOrEmpty(PageID))
                {
                    param[4] = new SqlParameter("@ParamPageID", SqlDbType.Int);
                    param[4].Value = Convert.ToInt32(PageID);
                }

                param[5] = new SqlParameter("@ParamStatus", SqlDbType.NVarChar, 500);
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@Counter", SqlDbType.NVarChar, 500);
                param[6].Value = Counter;

                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_Auth_Page_Option", param);

                return param[5].Value.ToString();
            }
        }
        catch
        {
            return "false";
        }
    }

    public DataTable GET_Auth_Roll_Master(string MstID, string MstName, string MstDescription, string Counter)
    {
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!String.IsNullOrEmpty(MstID))
                {
                    param[0] = new SqlParameter("@ParamMstID", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(MstID);
                }
                if (!String.IsNullOrEmpty(MstName))
                {
                    param[1] = new SqlParameter("@ParamMstName", SqlDbType.NVarChar, 50);
                    param[1].Value = MstName;
                }

                param[2] = new SqlParameter("@Counter", SqlDbType.NVarChar, 500);
                param[2].Value = Counter;

                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_Auth_Roll_Master", param);
                return ds.Tables[0];
            }
        }
        catch
        {
            return null;
        }
    }

    public string SET_Auth_Roll_Master(string MstID, string MstName, string MstDescription, string Counter)
    {
        SqlParameter[] param = new SqlParameter[5];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!String.IsNullOrEmpty(MstID))
                {
                    param[0] = new SqlParameter("@ParamMstID", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(MstID);
                }
                if (!String.IsNullOrEmpty(MstName))
                {
                    param[1] = new SqlParameter("@ParamMstName", SqlDbType.NVarChar, 50);
                    param[1].Value = MstName;
                }
                if (!String.IsNullOrEmpty(MstDescription))
                {
                    param[2] = new SqlParameter("@ParamMstDescription", SqlDbType.NVarChar, 500);
                    param[2].Value = MstDescription;
                }

                param[3] = new SqlParameter("@ParamStatus", SqlDbType.NVarChar, 500);
                param[3].Direction = ParameterDirection.Output;

                param[4] = new SqlParameter("@Counter", SqlDbType.NVarChar, 500);
                param[4].Value = Counter;

                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_Auth_Roll_Master", param);

                return param[3].Value.ToString();
            }
        }
        catch
        {
            return "false";
        }
    }

    public DataTable GET_Auth_Roll_Authorization(string AuthID, string MstName, string OptionName, string PageID,
        string BookingStatus, string PageName, string Counter)
    {
        SqlParameter[] param = new SqlParameter[7];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!String.IsNullOrEmpty(AuthID))
                {
                    param[0] = new SqlParameter("@ParamAuthID", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(AuthID);
                }
                if (!String.IsNullOrEmpty(MstName))
                {
                    param[1] = new SqlParameter("@ParamMstName", SqlDbType.NVarChar, 50);
                    param[1].Value = MstName;
                }
                if (!String.IsNullOrEmpty(OptionName))
                {
                    param[2] = new SqlParameter("@ParamOptionName", SqlDbType.NVarChar, 50);
                    param[2].Value = OptionName;
                }
                if (!String.IsNullOrEmpty(PageID))
                {
                    param[3] = new SqlParameter("@ParamPageID", SqlDbType.Int);
                    param[3].Value = Convert.ToInt32(PageID);
                }
                param[4] = new SqlParameter("@Counter", SqlDbType.NVarChar, 500);
                param[4].Value = Counter;
                if (!String.IsNullOrEmpty(BookingStatus))
                {
                    param[5] = new SqlParameter("@ParamBookingStatus", SqlDbType.NVarChar, 50);
                    param[5].Value = BookingStatus;
                }
                if (!String.IsNullOrEmpty(PageName))
                {
                    param[6] = new SqlParameter("@ParamPageName", SqlDbType.NVarChar, 500);
                    param[6].Value = PageName;
                }


                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_Auth_Roll_Authorization", param);
                return ds.Tables[0];
            }
        }
        catch
        {
            return null;
        }
    }

    public string SET_Auth_Roll_Authorization(string AuthID, string MstName, string OptionName, string PageID,
        string CompanyID, string IsView, string IsAdd, string IsUpdate, string IsDelete, string AllAuthID,
        string BookingStatus, string Counter)
    {
        SqlParameter[] param = new SqlParameter[13];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!String.IsNullOrEmpty(AuthID))
                {
                    param[0] = new SqlParameter("@ParamAuthID", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(AuthID);
                }
                if (!String.IsNullOrEmpty(MstName))
                {
                    param[1] = new SqlParameter("@ParamMstName", SqlDbType.NVarChar, 50);
                    param[1].Value = MstName;
                }
                if (!String.IsNullOrEmpty(OptionName))
                {
                    param[2] = new SqlParameter("@ParamOptionName", SqlDbType.NVarChar, 50);
                    param[2].Value = OptionName;
                }
                if (!String.IsNullOrEmpty(PageID))
                {
                    param[3] = new SqlParameter("@ParamPageID", SqlDbType.Int);
                    param[3].Value = Convert.ToInt32(PageID);
                }
                if (!String.IsNullOrEmpty(CompanyID))
                {
                    param[4] = new SqlParameter("@ParamCompanyID", SqlDbType.NVarChar, 50);
                    param[4].Value = CompanyID;
                }
                if (!String.IsNullOrEmpty(IsView))
                {
                    param[5] = new SqlParameter("@ParamIsView", SqlDbType.Bit);
                    param[5].Value = Convert.ToBoolean(IsView);
                }
                if (!String.IsNullOrEmpty(IsAdd))
                {
                    param[6] = new SqlParameter("@ParamIsAdd", SqlDbType.Bit);
                    param[6].Value = Convert.ToBoolean(IsAdd);
                }
                if (!String.IsNullOrEmpty(IsUpdate))
                {
                    param[7] = new SqlParameter("@ParamIsUpdate", SqlDbType.Bit);
                    param[7].Value = Convert.ToBoolean(IsUpdate);
                }
                if (!String.IsNullOrEmpty(IsDelete))
                {
                    param[8] = new SqlParameter("@ParamIsDelete", SqlDbType.Bit);
                    param[8].Value = Convert.ToBoolean(IsDelete);
                }
                param[9] = new SqlParameter("@ParamStatus", SqlDbType.NVarChar, 500);
                param[9].Direction = ParameterDirection.Output;

                param[10] = new SqlParameter("@Counter", SqlDbType.NVarChar, 500);
                param[10].Value = Counter;

                if (!string.IsNullOrEmpty(AllAuthID))
                {
                    param[11] = new SqlParameter("@ParamAllAuthID", SqlDbType.NVarChar, 4000);
                    param[11].Value = AllAuthID;
                }
                if (!String.IsNullOrEmpty(BookingStatus))
                {
                    param[12] = new SqlParameter("@ParamBookingStatus", SqlDbType.NVarChar, 50);
                    param[12].Value = BookingStatus;
                }
                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_Auth_Roll_Authorization", param);

                return param[12].Value.ToString();
            }
        }
        catch
        {
            return "false";
        }
    }

    public DataTable GET_Auth_Roll_Authorization_New(string MstName, string IsAuthentication, string Counter)
    {
        SqlParameter[] param = new SqlParameter[4];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!String.IsNullOrEmpty(MstName))
                {
                    param[1] = new SqlParameter("@ParamMstName", SqlDbType.NVarChar, 50);
                    param[1].Value = MstName;
                }
                if (!String.IsNullOrEmpty(IsAuthentication))
                {
                    param[2] = new SqlParameter("@ParamIsAuthentication", SqlDbType.Int);
                    param[2].Value = Convert.ToBoolean(IsAuthentication);
                }
                param[3] = new SqlParameter("@Counter", SqlDbType.NVarChar, 500);
                param[3].Value = Counter;

                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_Auth_Roll_Authorization_New", param);
                return ds.Tables[0];
            }
        }
        catch
        {
            return null;
        }
    }
    public DataTable GET_Auth_Roll_Authorization_New(string MstName, string IsAuthentication, string UserMstName, string Counter)
    {
        SqlParameter[] param = new SqlParameter[5];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!String.IsNullOrEmpty(MstName))
                {
                    param[1] = new SqlParameter("@ParamMstName", SqlDbType.NVarChar, 50);
                    param[1].Value = MstName;
                }
                if (!String.IsNullOrEmpty(IsAuthentication))
                {
                    param[2] = new SqlParameter("@ParamIsAuthentication", SqlDbType.Int);
                    param[2].Value = Convert.ToBoolean(IsAuthentication);
                }
                if (!String.IsNullOrEmpty(IsAuthentication))
                {
                    param[3] = new SqlParameter("@ParamUserMstName", SqlDbType.NVarChar, 50);
                    param[3].Value = UserMstName;
                }
                param[4] = new SqlParameter("@Counter", SqlDbType.NVarChar, 500);
                param[4].Value = Counter;

                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_Auth_Roll_Authorization_New", param);
                return ds.Tables[0];
            }
        }
        catch
        {
            return null;
        }
    }

    public string SET_Auth_Roll_Authorization_New(string AuthID, string MstName, string OptionID, string IsAuthentication, string Counter)
    {
        SqlParameter[] param = new SqlParameter[6];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!String.IsNullOrEmpty(AuthID))
                {
                    param[0] = new SqlParameter("@ParamAuthID", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(AuthID);
                }
                if (!String.IsNullOrEmpty(MstName))
                {
                    param[1] = new SqlParameter("@ParamMstName", SqlDbType.NVarChar, 50);
                    param[1].Value = MstName;
                }
                if (!String.IsNullOrEmpty(OptionID))
                {
                    param[2] = new SqlParameter("@ParamOptionID", SqlDbType.Int);
                    param[2].Value = Convert.ToInt32(OptionID);
                }
                if (!String.IsNullOrEmpty(IsAuthentication))
                {
                    param[3] = new SqlParameter("@ParamIsAuthentication", SqlDbType.Bit);
                    param[3].Value = Convert.ToBoolean(IsAuthentication);
                }

                param[4] = new SqlParameter("@ParamStatus", SqlDbType.NVarChar, 500);
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@Counter", SqlDbType.NVarChar, 500);
                param[5].Value = Counter;

                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_Auth_Roll_Authorization_New", param);
                return param[4].Value.ToString();
            }
        }
        catch
        {
            return "false";
        }
    }
    #endregion

    #region Company Permission

    public DataTable GET_Auth_Coomapany_Authorization(string UserID, string compID, string isAuth, string Counter)
    {
        SqlParameter[] param = new SqlParameter[4];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {

                param[0] = new SqlParameter("@ParamUserID", SqlDbType.NVarChar, 50);
                param[0].Value = UserID;

                param[1] = new SqlParameter("@Counter", SqlDbType.NVarChar, 500);
                param[1].Value = Counter;


                if (!string.IsNullOrEmpty(compID))
                {
                    param[2] = new SqlParameter("@ParamCompanyID", SqlDbType.NVarChar, 500);
                    param[2].Value = compID;
                }

                if (!string.IsNullOrEmpty(isAuth))
                {
                    if (isAuth.ToLower() == "true")
                    {
                        param[3] = new SqlParameter("@ParamIsAuthentication", SqlDbType.Bit);
                        param[3].Value = true;
                    }
                    else
                    {
                        param[3] = new SqlParameter("@ParamIsAuthentication", SqlDbType.Bit);
                        param[3].Value = false;
                    }
                }
                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_Auth_Company_Authorization", param);
                return ds.Tables[0];
            }
        }
        catch
        {
            return null;
        }
    }

    #endregion

    #region Search booking
    public DataTable GET_BookingDetail(string BookingID, string ProdID, string Provider, string BookingByCompany, string BookingBy, string FromDateTime,
        string ToDateTime, string BookingStatus, string PNRConfirmation, string SourceMedia, string ProductType, string PhoneNo,
        string MobileNo, string EmailAddress, string PaxFirstName, string PaxMiddleName, string PaxLastName, string RollName, string SupplierRef, string AuthNo = "")
    {
        SqlParameter[] param = new SqlParameter[19];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(BookingID))
                {
                    param[0] = new SqlParameter("@ParamBookingID", SqlDbType.VarChar, 50);
                    param[0].Value = BookingID;
                }
                if (!string.IsNullOrEmpty(ProdID))
                {
                    param[1] = new SqlParameter("@ParamProdID", SqlDbType.VarChar, 50);
                    param[1].Value = ProdID;
                }
                if (!string.IsNullOrEmpty(Provider))
                {
                    param[2] = new SqlParameter("@ParamProvider", SqlDbType.VarChar, 50);
                    param[2].Value = Provider;
                }
                if (!string.IsNullOrEmpty(BookingByCompany))
                {
                    param[3] = new SqlParameter("@ParamBookingByCompany", SqlDbType.VarChar);
                    param[3].Value = BookingByCompany;
                }
                if (!string.IsNullOrEmpty(BookingBy))
                {
                    param[4] = new SqlParameter("@ParamBookingBy", SqlDbType.VarChar, 100);
                    param[4].Value = BookingBy;
                }
                if (!string.IsNullOrEmpty(FromDateTime))
                {
                    param[5] = new SqlParameter("@ParamFromDateTime", SqlDbType.DateTime);
                    param[5].Value = Convert.ToDateTime(FromDateTime);
                }
                if (!string.IsNullOrEmpty(ToDateTime))
                {
                    param[6] = new SqlParameter("@ParamToDateTime", SqlDbType.DateTime);
                    param[6].Value = Convert.ToDateTime(ToDateTime);
                }
                if (!string.IsNullOrEmpty(BookingStatus))
                {
                    param[7] = new SqlParameter("@ParamBookingStatus", SqlDbType.VarChar, 50);
                    param[7].Value = BookingStatus;
                }
                if (!string.IsNullOrEmpty(PNRConfirmation))
                {
                    param[8] = new SqlParameter("@ParamPNRConfirmation", SqlDbType.VarChar, 50);
                    param[8].Value = PNRConfirmation;
                }
                if (!string.IsNullOrEmpty(SourceMedia))
                {
                    param[9] = new SqlParameter("@ParamSourceMedia", SqlDbType.VarChar, 50);
                    param[9].Value = SourceMedia;
                }
                if (!string.IsNullOrEmpty(ProductType))
                {
                    param[10] = new SqlParameter("@ParamProductType", SqlDbType.VarChar, 50);
                    param[10].Value = ProductType;
                }
                if (!string.IsNullOrEmpty(PhoneNo))
                {
                    param[11] = new SqlParameter("@ParamPhoneNo", SqlDbType.VarChar, 100);
                    param[11].Value = PhoneNo;
                }
                if (!string.IsNullOrEmpty(MobileNo))
                {
                    param[12] = new SqlParameter("@ParamMobileNo", SqlDbType.VarChar, 100);
                    param[12].Value = MobileNo;
                }
                if (!string.IsNullOrEmpty(EmailAddress))
                {
                    param[13] = new SqlParameter("@ParamEmailAddress", SqlDbType.VarChar, 500);
                    param[13].Value = EmailAddress;
                }
                if (!string.IsNullOrEmpty(PaxFirstName))
                {
                    param[14] = new SqlParameter("@ParamPaxFirstName", SqlDbType.VarChar, 100);
                    param[14].Value = PaxFirstName;
                }
                if (!string.IsNullOrEmpty(PaxLastName))
                {
                    param[15] = new SqlParameter("@ParamPaxLastName", SqlDbType.VarChar, 100);
                    param[15].Value = PaxLastName;
                }
                if (!string.IsNullOrEmpty(RollName))
                {
                    param[16] = new SqlParameter("@ParamRollName", SqlDbType.NVarChar, 50);
                    param[16].Value = RollName;
                }
                if (!string.IsNullOrEmpty(SupplierRef))
                {
                    param[17] = new SqlParameter("@ParamSupplierRef", SqlDbType.NVarChar, 100);
                    param[17].Value = SupplierRef;
                }
                if (!string.IsNullOrEmpty(AuthNo))
                {
                    param[18] = new SqlParameter("@ParamAuth_No", SqlDbType.NVarChar, 100);
                    param[18].Value = AuthNo;
                }
                //[GET_BookingDetailWithTransaction]
                // DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_BookingDetail", param);
                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_BookingDetailWithTransaction", param);
                if (ds != null)
                    return ds.Tables[0];
                else
                    return null;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public DataTable GET_BookingDetail_Date(string BookingID, string ProdID, string Provider, string BookingByCompany, string BookingBy,
     string FromDateTime, string ToDateTime, string BookingStatus, string PNRConfirmation, string SourceMedia, string ProductType,
     string PhoneNo, string MobileNo, string EmailAddress, string PaxFirstName, string PaxMiddleName, string PaxLastName,
     string RollName, string SupplierRef, string DepDate, string RetDate, string AuthNo = "", string Year = "")
    {
        SqlParameter[] param = new SqlParameter[22];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(BookingID))
                {
                    param[0] = new SqlParameter("@ParamBookingID", SqlDbType.VarChar, 50);
                    param[0].Value = BookingID;
                }
                if (!string.IsNullOrEmpty(ProdID))
                {
                    param[1] = new SqlParameter("@ParamProdID", SqlDbType.VarChar, 50);
                    param[1].Value = ProdID;
                }
                if (!string.IsNullOrEmpty(Provider))
                {
                    param[2] = new SqlParameter("@ParamProvider", SqlDbType.VarChar, 50);
                    param[2].Value = Provider;
                }
                if (!string.IsNullOrEmpty(BookingByCompany))
                {
                    param[3] = new SqlParameter("@ParamBookingByCompany", SqlDbType.VarChar);
                    param[3].Value = BookingByCompany;
                }
                if (!string.IsNullOrEmpty(BookingBy))
                {
                    param[4] = new SqlParameter("@ParamBookingBy", SqlDbType.VarChar, 100);
                    param[4].Value = BookingBy;
                }
                if (!string.IsNullOrEmpty(FromDateTime))
                {
                    param[5] = new SqlParameter("@ParamFromDateTime", SqlDbType.DateTime);
                    param[5].Value = Convert.ToDateTime(FromDateTime);
                }
                if (!string.IsNullOrEmpty(ToDateTime))
                {
                    param[6] = new SqlParameter("@ParamToDateTime", SqlDbType.DateTime);
                    param[6].Value = Convert.ToDateTime(ToDateTime);
                }
                if (!string.IsNullOrEmpty(BookingStatus))
                {
                    param[7] = new SqlParameter("@ParamBookingStatus", SqlDbType.VarChar, 50);
                    param[7].Value = BookingStatus;
                }
                if (!string.IsNullOrEmpty(PNRConfirmation))
                {
                    param[8] = new SqlParameter("@ParamPNRConfirmation", SqlDbType.VarChar, 50);
                    param[8].Value = PNRConfirmation;
                }
                if (!string.IsNullOrEmpty(SourceMedia))
                {
                    param[9] = new SqlParameter("@ParamSourceMedia", SqlDbType.VarChar, 50);
                    param[9].Value = SourceMedia;
                }
                if (!string.IsNullOrEmpty(ProductType))
                {
                    param[10] = new SqlParameter("@ParamProductType", SqlDbType.VarChar, 50);
                    param[10].Value = ProductType;
                }
                if (!string.IsNullOrEmpty(PhoneNo))
                {
                    param[11] = new SqlParameter("@ParamPhoneNo", SqlDbType.VarChar, 100);
                    param[11].Value = PhoneNo;
                }
                if (!string.IsNullOrEmpty(MobileNo))
                {
                    param[12] = new SqlParameter("@ParamMobileNo", SqlDbType.VarChar, 100);
                    param[12].Value = MobileNo;
                }
                if (!string.IsNullOrEmpty(EmailAddress))
                {
                    param[13] = new SqlParameter("@ParamEmailAddress", SqlDbType.VarChar, 500);
                    param[13].Value = EmailAddress;
                }
                if (!string.IsNullOrEmpty(PaxFirstName))
                {
                    param[14] = new SqlParameter("@ParamPaxFirstName", SqlDbType.VarChar, 100);
                    param[14].Value = PaxFirstName;
                }
                if (!string.IsNullOrEmpty(PaxLastName))
                {
                    param[15] = new SqlParameter("@ParamPaxLastName", SqlDbType.VarChar, 100);
                    param[15].Value = PaxLastName;
                }
                if (!string.IsNullOrEmpty(RollName))
                {
                    param[16] = new SqlParameter("@ParamRollName", SqlDbType.NVarChar, 50);
                    param[16].Value = RollName;
                }
                if (!string.IsNullOrEmpty(SupplierRef))
                {
                    param[17] = new SqlParameter("@ParamSupplierRef", SqlDbType.NVarChar, 100);
                    param[17].Value = SupplierRef;
                }
                if (!string.IsNullOrEmpty(DepDate))
                {
                    param[18] = new SqlParameter("@paramFromDepartDate", SqlDbType.Date);
                    param[18].Value = DepDate;
                }
                if (!string.IsNullOrEmpty(RetDate))
                {
                    param[19] = new SqlParameter("@paramToDepartDate", SqlDbType.Date);
                    param[19].Value = RetDate;
                }
                if (!string.IsNullOrEmpty(AuthNo))
                {
                    param[20] = new SqlParameter("@ParamAuth_No", SqlDbType.NVarChar, 100);
                    param[20].Value = AuthNo;
                }
                if (!string.IsNullOrEmpty(Year))
                {
                    param[21] = new SqlParameter("@ParamYear", SqlDbType.NVarChar, 100);
                    param[21].Value = Year;
                }
                //DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_BookingDetailWithTransactionUsingDateV", param);
                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_BookingDetailWithTransactionUsingDateV", param);
                // DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_BookingDetailWithTransactionUsingDateV_Bkp", param);
                if (ds != null)
                    return ds.Tables[0];
                else
                    return null;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }


    public DataTable GET_BookingDetail_Date_New_BD(string BookingID, string ProdID, string Provider, string BookingByCompany, string BookingBy,
     string FromDateTime, string ToDateTime, string BookingStatus, string PNRConfirmation, string SourceMedia, string ProductType,
     string PhoneNo, string MobileNo, string EmailAddress, string PaxFirstName, string PaxMiddleName, string PaxLastName,
     string RollName, string SupplierRef, string DepDate, string RetDate, string AuthNo = "", string Year = "")
    {
        SqlParameter[] param = new SqlParameter[19];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(BookingID))
                {
                    param[0] = new SqlParameter("@ParamBookingID", SqlDbType.VarChar, 50);
                    param[0].Value = BookingID;
                }
                if (!string.IsNullOrEmpty(ProdID))
                {
                    param[1] = new SqlParameter("@ParamProdID", SqlDbType.VarChar, 50);
                    param[1].Value = ProdID;
                }
                if (!string.IsNullOrEmpty(Provider))
                {
                    param[2] = new SqlParameter("@ParamProvider", SqlDbType.VarChar, 50);
                    param[2].Value = Provider;
                }
                if (!string.IsNullOrEmpty(BookingByCompany))
                {
                    param[3] = new SqlParameter("@ParamBookingByCompany", SqlDbType.VarChar);
                    param[3].Value = BookingByCompany;
                }
                if (!string.IsNullOrEmpty(BookingBy))
                {
                    param[4] = new SqlParameter("@ParamBookingBy", SqlDbType.VarChar, 100);
                    param[4].Value = BookingBy;
                }
                if (!string.IsNullOrEmpty(FromDateTime))
                {
                    param[5] = new SqlParameter("@ParamFromDateTime", SqlDbType.DateTime);
                    param[5].Value = Convert.ToDateTime(FromDateTime);
                }
                if (!string.IsNullOrEmpty(ToDateTime))
                {
                    param[6] = new SqlParameter("@ParamToDateTime", SqlDbType.DateTime);
                    param[6].Value = Convert.ToDateTime(ToDateTime);
                }
                if (!string.IsNullOrEmpty(BookingStatus))
                {
                    param[7] = new SqlParameter("@ParamBookingStatus", SqlDbType.VarChar, 50);
                    param[7].Value = BookingStatus;
                }
                if (!string.IsNullOrEmpty(PNRConfirmation))
                {
                    param[8] = new SqlParameter("@ParamPNRConfirmation", SqlDbType.VarChar, 50);
                    param[8].Value = PNRConfirmation;
                }
                if (!string.IsNullOrEmpty(SourceMedia))
                {
                    param[9] = new SqlParameter("@ParamSourceMedia", SqlDbType.VarChar, 50);
                    param[9].Value = SourceMedia;
                }
                if (!string.IsNullOrEmpty(ProductType))
                {
                    param[10] = new SqlParameter("@ParamProductType", SqlDbType.VarChar, 50);
                    param[10].Value = ProductType;
                }
                if (!string.IsNullOrEmpty(PhoneNo))
                {
                    param[11] = new SqlParameter("@ParamPhoneNo", SqlDbType.VarChar, 100);
                    param[11].Value = PhoneNo;
                }
                if (!string.IsNullOrEmpty(MobileNo))
                {
                    param[12] = new SqlParameter("@ParamMobileNo", SqlDbType.VarChar, 100);
                    param[12].Value = MobileNo;
                }
                if (!string.IsNullOrEmpty(EmailAddress))
                {
                    param[13] = new SqlParameter("@ParamEmailAddress", SqlDbType.VarChar, 500);
                    param[13].Value = EmailAddress;
                }
                if (!string.IsNullOrEmpty(PaxFirstName))
                {
                    param[14] = new SqlParameter("@ParamPaxFirstName", SqlDbType.VarChar, 100);
                    param[14].Value = PaxFirstName;
                }
                if (!string.IsNullOrEmpty(PaxLastName))
                {
                    param[15] = new SqlParameter("@ParamPaxLastName", SqlDbType.VarChar, 100);
                    param[15].Value = PaxLastName;
                }
                if (!string.IsNullOrEmpty(RollName))
                {
                    param[16] = new SqlParameter("@ParamRollName", SqlDbType.NVarChar, 50);
                    param[16].Value = RollName;
                }
                if (!string.IsNullOrEmpty(SupplierRef))
                {
                    param[17] = new SqlParameter("@ParamSupplierRef", SqlDbType.NVarChar, 100);
                    param[17].Value = SupplierRef;
                }
                if (!string.IsNullOrEmpty(AuthNo))
                {
                    param[18] = new SqlParameter("@ParamAuth_No", SqlDbType.NVarChar, 100);
                    param[18].Value = AuthNo;
                }
                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_BookingDetailWithTransactionUsingDateV_BD2", param);
                if (ds != null)
                    return ds.Tables[0];
                else
                    return null;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }


    public DataTable GET_BookingDetail_Date_New_BD3(string BookingByCompany, string BookingBy,
     string FromDateTime, string ToDateTime, string BookingStatus, string ProductType, int IsArrival)
    {
        SqlParameter[] param = new SqlParameter[7];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {

                if (!string.IsNullOrEmpty(BookingByCompany))
                {
                    param[0] = new SqlParameter("@ParamBookingByCompany", SqlDbType.VarChar);
                    param[0].Value = BookingByCompany;
                }
                if (!string.IsNullOrEmpty(BookingBy))
                {
                    param[1] = new SqlParameter("@ParamBookingBy", SqlDbType.VarChar, 100);
                    param[1].Value = BookingBy;
                }
                if (!string.IsNullOrEmpty(FromDateTime))
                {
                    param[2] = new SqlParameter("@ParamFromDateTime", SqlDbType.DateTime);
                    param[2].Value = Convert.ToDateTime(FromDateTime);
                }
                if (!string.IsNullOrEmpty(ToDateTime))
                {
                    param[3] = new SqlParameter("@ParamToDateTime", SqlDbType.DateTime);
                    param[3].Value = Convert.ToDateTime(ToDateTime);
                }
                if (!string.IsNullOrEmpty(BookingStatus))
                {
                    param[4] = new SqlParameter("@ParamBookingStatus", SqlDbType.VarChar, 50);
                    param[4].Value = BookingStatus;
                }
                if (!string.IsNullOrEmpty(BookingStatus))
                {
                    param[4] = new SqlParameter("@ParamBookingStatus", SqlDbType.VarChar, 50);
                    param[4].Value = BookingStatus;
                }
                if (!string.IsNullOrEmpty(ProductType))
                {
                    param[5] = new SqlParameter("@ParamProductType", SqlDbType.VarChar, 50);
                    param[5].Value = ProductType;
                }
                if (!string.IsNullOrEmpty(IsArrival.ToString()))
                {
                    param[6] = new SqlParameter("@IsArrival", SqlDbType.Bit);
                    param[6].Value = IsArrival;
                }

                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_BookingDetailWithTransactionUsingDateV_BD3_Temp", param);
                if (ds != null)
                    return ds.Tables[0];
                else
                    return null;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }


    public DataTable GET_BookingDetailByTransaction(string BookingID, string ProdID, string Provider, string BookingByCompany, string BookingBy, string FromDateTime,
        string ToDateTime, string BookingStatus, string PNRConfirmation, string SourceMedia, string ProductType, string PhoneNo,
        string MobileNo, string EmailAddress, string PaxFirstName, string PaxMiddleName, string PaxLastName, string RollName, string SupplierRef, string AuthNo)
    {
        SqlParameter[] param = new SqlParameter[19];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(BookingID))
                {
                    param[0] = new SqlParameter("@ParamBookingID", SqlDbType.VarChar, 50);
                    param[0].Value = BookingID;
                }
                if (!string.IsNullOrEmpty(ProdID))
                {
                    param[1] = new SqlParameter("@ParamProdID", SqlDbType.VarChar, 50);
                    param[1].Value = ProdID;
                }
                if (!string.IsNullOrEmpty(Provider))
                {
                    param[2] = new SqlParameter("@ParamProvider", SqlDbType.VarChar, 50);
                    param[2].Value = Provider;
                }
                if (!string.IsNullOrEmpty(BookingByCompany))
                {
                    param[3] = new SqlParameter("@ParamBookingByCompany", SqlDbType.VarChar);
                    param[3].Value = BookingByCompany;
                }
                if (!string.IsNullOrEmpty(BookingBy))
                {
                    param[4] = new SqlParameter("@ParamBookingBy", SqlDbType.VarChar, 100);
                    param[4].Value = BookingBy;
                }
                if (!string.IsNullOrEmpty(FromDateTime))
                {
                    param[5] = new SqlParameter("@ParamFromDateTime", SqlDbType.DateTime);
                    param[5].Value = Convert.ToDateTime(FromDateTime);
                }
                if (!string.IsNullOrEmpty(ToDateTime))
                {
                    param[6] = new SqlParameter("@ParamToDateTime", SqlDbType.DateTime);
                    param[6].Value = Convert.ToDateTime(ToDateTime);
                }
                if (!string.IsNullOrEmpty(BookingStatus))
                {
                    param[7] = new SqlParameter("@ParamBookingStatus", SqlDbType.VarChar, 50);
                    param[7].Value = BookingStatus;
                }
                if (!string.IsNullOrEmpty(PNRConfirmation))
                {
                    param[8] = new SqlParameter("@ParamPNRConfirmation", SqlDbType.VarChar, 50);
                    param[8].Value = PNRConfirmation;
                }
                if (!string.IsNullOrEmpty(SourceMedia))
                {
                    param[9] = new SqlParameter("@ParamSourceMedia", SqlDbType.VarChar, 50);
                    param[9].Value = SourceMedia;
                }
                if (!string.IsNullOrEmpty(ProductType))
                {
                    param[10] = new SqlParameter("@ParamProductType", SqlDbType.VarChar, 50);
                    param[10].Value = ProductType;
                }
                if (!string.IsNullOrEmpty(PhoneNo))
                {
                    param[11] = new SqlParameter("@ParamPhoneNo", SqlDbType.VarChar, 100);
                    param[11].Value = PhoneNo;
                }
                if (!string.IsNullOrEmpty(MobileNo))
                {
                    param[12] = new SqlParameter("@ParamMobileNo", SqlDbType.VarChar, 100);
                    param[12].Value = MobileNo;
                }
                if (!string.IsNullOrEmpty(EmailAddress))
                {
                    param[13] = new SqlParameter("@ParamEmailAddress", SqlDbType.VarChar, 500);
                    param[13].Value = EmailAddress;
                }
                if (!string.IsNullOrEmpty(PaxFirstName))
                {
                    param[14] = new SqlParameter("@ParamPaxFirstName", SqlDbType.VarChar, 100);
                    param[14].Value = PaxFirstName;
                }
                if (!string.IsNullOrEmpty(PaxLastName))
                {
                    param[15] = new SqlParameter("@ParamPaxLastName", SqlDbType.VarChar, 100);
                    param[15].Value = PaxLastName;
                }
                if (!string.IsNullOrEmpty(RollName))
                {
                    param[16] = new SqlParameter("@ParamRollName", SqlDbType.NVarChar, 50);
                    param[16].Value = RollName;
                }
                if (!string.IsNullOrEmpty(SupplierRef))
                {
                    param[17] = new SqlParameter("@ParamSupplierRef", SqlDbType.NVarChar, 100);
                    param[17].Value = SupplierRef;
                }
                if (!string.IsNullOrEmpty(AuthNo))
                {
                    param[18] = new SqlParameter("@ParamAuth_No", SqlDbType.NVarChar, 100);
                    param[18].Value = AuthNo;
                }
                //[GET_BookingDetailWithTransaction]
                // DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_BookingDetail", param);
                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_BookingDetailWithTransaction_No", param);
                if (ds != null)
                    return ds.Tables[0];
                else
                    return null;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public DataTable SearchByTransaction(string BookingID, string ProdID, string Provider, string BookingByCompany, string BookingBy, string FromDateTime,
        string ToDateTime, string BookingStatus, string PNRConfirmation, string SourceMedia, string ProductType, string PhoneNo,
        string MobileNo, string EmailAddress, string PaxFirstName, string PaxMiddleName, string PaxLastName, string RollName, string SupplierRef, string AuthNo)
    {
        SqlParameter[] param = new SqlParameter[19];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(BookingID))
                {
                    param[0] = new SqlParameter("@ParamBookingID", SqlDbType.VarChar, 50);
                    param[0].Value = BookingID;
                }
                if (!string.IsNullOrEmpty(ProdID))
                {
                    param[1] = new SqlParameter("@ParamProdID", SqlDbType.VarChar, 50);
                    param[1].Value = ProdID;
                }
                if (!string.IsNullOrEmpty(Provider))
                {
                    param[2] = new SqlParameter("@ParamProvider", SqlDbType.VarChar, 50);
                    param[2].Value = Provider;
                }
                if (!string.IsNullOrEmpty(BookingByCompany))
                {
                    param[3] = new SqlParameter("@ParamBookingByCompany", SqlDbType.VarChar);
                    param[3].Value = BookingByCompany;
                }
                if (!string.IsNullOrEmpty(BookingBy))
                {
                    param[4] = new SqlParameter("@ParamBookingBy", SqlDbType.VarChar, 100);
                    param[4].Value = BookingBy;
                }
                if (!string.IsNullOrEmpty(FromDateTime))
                {
                    param[5] = new SqlParameter("@ParamFromDateTime", SqlDbType.DateTime);
                    param[5].Value = Convert.ToDateTime(FromDateTime);
                }
                if (!string.IsNullOrEmpty(ToDateTime))
                {
                    param[6] = new SqlParameter("@ParamToDateTime", SqlDbType.DateTime);
                    param[6].Value = Convert.ToDateTime(ToDateTime);
                }
                if (!string.IsNullOrEmpty(BookingStatus))
                {
                    param[7] = new SqlParameter("@ParamBookingStatus", SqlDbType.VarChar, 50);
                    param[7].Value = BookingStatus;
                }
                if (!string.IsNullOrEmpty(PNRConfirmation))
                {
                    param[8] = new SqlParameter("@ParamPNRConfirmation", SqlDbType.VarChar, 50);
                    param[8].Value = PNRConfirmation;
                }
                if (!string.IsNullOrEmpty(SourceMedia))
                {
                    param[9] = new SqlParameter("@ParamSourceMedia", SqlDbType.VarChar, 50);
                    param[9].Value = SourceMedia;
                }
                if (!string.IsNullOrEmpty(ProductType))
                {
                    param[10] = new SqlParameter("@ParamProductType", SqlDbType.VarChar, 50);
                    param[10].Value = ProductType;
                }
                if (!string.IsNullOrEmpty(PhoneNo))
                {
                    param[11] = new SqlParameter("@ParamPhoneNo", SqlDbType.VarChar, 100);
                    param[11].Value = PhoneNo;
                }
                if (!string.IsNullOrEmpty(MobileNo))
                {
                    param[12] = new SqlParameter("@ParamMobileNo", SqlDbType.VarChar, 100);
                    param[12].Value = MobileNo;
                }
                if (!string.IsNullOrEmpty(EmailAddress))
                {
                    param[13] = new SqlParameter("@ParamEmailAddress", SqlDbType.VarChar, 500);
                    param[13].Value = EmailAddress;
                }
                if (!string.IsNullOrEmpty(PaxFirstName))
                {
                    param[14] = new SqlParameter("@ParamPaxFirstName", SqlDbType.VarChar, 100);
                    param[14].Value = PaxFirstName;
                }
                if (!string.IsNullOrEmpty(PaxLastName))
                {
                    param[15] = new SqlParameter("@ParamPaxLastName", SqlDbType.VarChar, 100);
                    param[15].Value = PaxLastName;
                }
                if (!string.IsNullOrEmpty(RollName))
                {
                    param[16] = new SqlParameter("@ParamRollName", SqlDbType.NVarChar, 50);
                    param[16].Value = RollName;
                }
                if (!string.IsNullOrEmpty(SupplierRef))
                {
                    param[17] = new SqlParameter("@ParamSupplierRef", SqlDbType.NVarChar, 100);
                    param[17].Value = SupplierRef;
                }
                if (!string.IsNullOrEmpty(AuthNo))
                {
                    param[18] = new SqlParameter("@ParamAuth_No", SqlDbType.NVarChar, 100);
                    param[18].Value = AuthNo;
                }
                //[GET_BookingDetailWithTransaction]
                // DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_BookingDetail", param);
                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "SearchByTransaction_No", param);
                if (ds != null)
                    return ds.Tables[0];
                else
                    return null;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    #endregion

    #region Airline Markup
    public string SET_FlightMarkupDetail(string ID, string From, string To, string AirV, string Provider, string Category,
        string CClass, string FareType, string Amount, string AmountType, string ValidFromDate, string ValidToDate,
        string CompanyID, string CampID, string JourneyType, string ModifiedBy, string Counter)
    {
        SqlParameter[] param = new SqlParameter[18];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnectionMarkup())
            {

                if (!string.IsNullOrEmpty(ID))
                {
                    param[0] = new SqlParameter("@ParamID", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(ID);
                }

                param[1] = new SqlParameter("@ParamFrom", SqlDbType.VarChar, 50);
                param[1].Value = string.IsNullOrEmpty(From) ? "ANY" : From.ToUpper();

                param[2] = new SqlParameter("@ParamTo", SqlDbType.VarChar, 50);
                param[2].Value = string.IsNullOrEmpty(To) ? "ANY" : To.ToUpper();

                param[3] = new SqlParameter("@ParamAirV", SqlDbType.VarChar, 50);
                param[3].Value = string.IsNullOrEmpty(AirV) ? "ANY" : AirV.ToUpper();

                param[4] = new SqlParameter("@ParamProvider", SqlDbType.VarChar, 50);
                param[4].Value = string.IsNullOrEmpty(Provider) ? "ANY" : Provider.ToUpper();

                param[5] = new SqlParameter("@ParamCategory", SqlDbType.VarChar, 50);
                param[5].Value = string.IsNullOrEmpty(Category) ? "ANY" : Category.ToUpper();

                param[6] = new SqlParameter("@ParamClass", SqlDbType.VarChar, 50);
                param[6].Value = string.IsNullOrEmpty(CClass) ? "ANY" : CClass.ToUpper();

                param[7] = new SqlParameter("@ParamFareType", SqlDbType.VarChar, 50);
                param[7].Value = string.IsNullOrEmpty(FareType) ? "ANY" : FareType.ToUpper();

                param[8] = new SqlParameter("@ParamAmount", SqlDbType.Money);
                param[8].Value = Convert.ToDouble(Amount);

                param[9] = new SqlParameter("@ParamAmountType", SqlDbType.VarChar, 50);
                param[9].Value = AmountType.ToUpper();

                param[10] = new SqlParameter("@ParamValidFromDate", SqlDbType.DateTime);
                param[10].Value = string.IsNullOrEmpty(ValidFromDate) ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(ValidFromDate);

                param[11] = new SqlParameter("@ParamValidToDate", SqlDbType.DateTime);
                param[11].Value = string.IsNullOrEmpty(ValidToDate) ? Convert.ToDateTime("01-01-2100") : Convert.ToDateTime(ValidToDate);

                param[12] = new SqlParameter("@ParamCompanyID", SqlDbType.VarChar, 50);
                param[12].Value = CompanyID;

                param[13] = new SqlParameter("@ParamCampID", SqlDbType.VarChar, 50);
                param[13].Value = string.IsNullOrEmpty(CampID) ? "ANY" : CampID;

                param[14] = new SqlParameter("@ParamJourneyType", SqlDbType.VarChar, 50);
                param[14].Value = string.IsNullOrEmpty(JourneyType) ? "ANY" : JourneyType.ToUpper();

                param[15] = new SqlParameter("@ParamModifiedBy", SqlDbType.VarChar, 50);
                param[15].Value = string.IsNullOrEmpty(ModifiedBy) ? "" : ModifiedBy;

                param[16] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[16].Value = Counter;

                param[17] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 500);
                param[17].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_FlightMarkupDetail", param);
                return param[17].Value.ToString();
            }
        }
        catch
        {
            return "false";
        }
    }

    public string SET_FlightMarkupDetailUS(string ID, string From, string To, string AirV, string Provider, string Category,
      string CClass, string FareType, string Amount, string AmountType, string ValidFromDate, string ValidToDate,
      string CompanyID, string CampID, string JourneyType, string ModifiedBy, string Counter, int paxCount, int DTD, string RestrictedClass)
    {
        SqlParameter[] param = new SqlParameter[21];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnectionMarkupUS())
            {

                if (!string.IsNullOrEmpty(ID))
                {
                    param[0] = new SqlParameter("@ParamID", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(ID);
                }

                param[1] = new SqlParameter("@ParamFrom", SqlDbType.VarChar, 50);
                param[1].Value = string.IsNullOrEmpty(From) ? "ANY" : From.ToUpper();

                param[2] = new SqlParameter("@ParamTo", SqlDbType.VarChar, 50);
                param[2].Value = string.IsNullOrEmpty(To) ? "ANY" : To.ToUpper();

                param[3] = new SqlParameter("@ParamAirV", SqlDbType.VarChar, 50);
                param[3].Value = string.IsNullOrEmpty(AirV) ? "ANY" : AirV.ToUpper();

                param[4] = new SqlParameter("@ParamProvider", SqlDbType.VarChar, 50);
                param[4].Value = string.IsNullOrEmpty(Provider) ? "ANY" : Provider.ToUpper();

                param[5] = new SqlParameter("@ParamCategory", SqlDbType.VarChar, 50);
                param[5].Value = string.IsNullOrEmpty(Category) ? "ANY" : Category.ToUpper();

                param[6] = new SqlParameter("@ParamClass", SqlDbType.VarChar, 50);
                param[6].Value = string.IsNullOrEmpty(CClass) ? "ANY" : CClass.ToUpper();

                param[7] = new SqlParameter("@ParamFareType", SqlDbType.VarChar, 50);
                param[7].Value = string.IsNullOrEmpty(FareType) ? "ANY" : FareType.ToUpper();

                param[8] = new SqlParameter("@ParamAmount", SqlDbType.Money);
                param[8].Value = Convert.ToDouble(Amount);

                param[9] = new SqlParameter("@ParamAmountType", SqlDbType.VarChar, 50);
                param[9].Value = AmountType.ToUpper();

                param[10] = new SqlParameter("@ParamValidFromDate", SqlDbType.DateTime);
                param[10].Value = string.IsNullOrEmpty(ValidFromDate) ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(ValidFromDate);

                param[11] = new SqlParameter("@ParamValidToDate", SqlDbType.DateTime);
                param[11].Value = string.IsNullOrEmpty(ValidToDate) ? Convert.ToDateTime("01-01-2100") : Convert.ToDateTime(ValidToDate);

                param[12] = new SqlParameter("@ParamCompanyID", SqlDbType.VarChar, 50);
                param[12].Value = CompanyID;

                param[13] = new SqlParameter("@ParamCampID", SqlDbType.VarChar, 50);
                param[13].Value = string.IsNullOrEmpty(CampID) ? "ANY" : CampID;

                param[14] = new SqlParameter("@ParamJourneyType", SqlDbType.VarChar, 50);
                param[14].Value = string.IsNullOrEmpty(JourneyType) ? "ANY" : JourneyType.ToUpper();

                param[15] = new SqlParameter("@ParamModifiedBy", SqlDbType.VarChar, 50);
                param[15].Value = string.IsNullOrEmpty(ModifiedBy) ? "" : ModifiedBy;

                param[16] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[16].Value = Counter;

                param[17] = new SqlParameter("@paramPaxCount", SqlDbType.VarChar, 500);
                param[17].Value = paxCount;

                param[18] = new SqlParameter("@ParamDaysToDeparture", SqlDbType.VarChar, 500);
                param[18].Value = DTD;

                param[19] = new SqlParameter("@paramRestrictedClass", SqlDbType.VarChar, 200);
                param[19].Value = RestrictedClass;

                param[20] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 500);
                param[20].Direction = ParameterDirection.Output;


                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_FlightMarkupDetail", param);
                return param[16].Value.ToString();
            }
        }
        catch (Exception ex)
        {
            return "false";
        }
    }

    public string SET_FlightMarkupDetailUK(string ID, string From, string To, string AirV, string Provider, string Category,
     string CClass, string FareType, string Amount, string AmountType, string ValidFromDate, string ValidToDate,
     string CompanyID, string CampID, string JourneyType, string ModifiedBy, string Counter, int paxCount, int DTD, string Stops, bool GroupCode, string DBD)
    {
        SqlParameter[] param = new SqlParameter[23];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnectionMarkup())
            {

                if (!string.IsNullOrEmpty(ID))
                {
                    param[0] = new SqlParameter("@ParamID", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(ID);
                }

                param[1] = new SqlParameter("@ParamFrom", SqlDbType.VarChar, 50);
                param[1].Value = string.IsNullOrEmpty(From) ? "ANY" : From.ToUpper();

                param[2] = new SqlParameter("@ParamTo", SqlDbType.VarChar, 50);
                param[2].Value = string.IsNullOrEmpty(To) ? "ANY" : To.ToUpper();

                param[3] = new SqlParameter("@ParamAirV", SqlDbType.VarChar, 50);
                param[3].Value = string.IsNullOrEmpty(AirV) ? "ANY" : AirV.ToUpper();

                param[4] = new SqlParameter("@ParamProvider", SqlDbType.VarChar, 50);
                param[4].Value = string.IsNullOrEmpty(Provider) ? "ANY" : Provider.ToUpper();

                param[5] = new SqlParameter("@ParamCategory", SqlDbType.VarChar, 50);
                param[5].Value = string.IsNullOrEmpty(Category) ? "ANY" : Category.ToUpper();

                param[6] = new SqlParameter("@ParamClass", SqlDbType.VarChar, 50);
                param[6].Value = string.IsNullOrEmpty(CClass) ? "ANY" : CClass.ToUpper();

                param[7] = new SqlParameter("@ParamFareType", SqlDbType.VarChar, 50);
                param[7].Value = string.IsNullOrEmpty(FareType) ? "ANY" : FareType.ToUpper();

                param[8] = new SqlParameter("@ParamAmount", SqlDbType.Money);
                param[8].Value = Convert.ToDouble(Amount);

                param[9] = new SqlParameter("@ParamAmountType", SqlDbType.VarChar, 50);
                param[9].Value = AmountType.ToUpper();

                param[10] = new SqlParameter("@ParamValidFromDate", SqlDbType.DateTime);
                param[10].Value = string.IsNullOrEmpty(ValidFromDate) ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(ValidFromDate);

                param[11] = new SqlParameter("@ParamValidToDate", SqlDbType.DateTime);
                param[11].Value = string.IsNullOrEmpty(ValidToDate) ? Convert.ToDateTime("01-01-2100") : Convert.ToDateTime(ValidToDate);

                param[12] = new SqlParameter("@ParamCompanyID", SqlDbType.VarChar, 50);
                param[12].Value = CompanyID;

                param[13] = new SqlParameter("@ParamCampID", SqlDbType.VarChar, 50);
                param[13].Value = string.IsNullOrEmpty(CampID) ? "ANY" : CampID;

                param[14] = new SqlParameter("@ParamJourneyType", SqlDbType.VarChar, 50);
                param[14].Value = string.IsNullOrEmpty(JourneyType) ? "ANY" : JourneyType.ToUpper();

                param[15] = new SqlParameter("@ParamModifiedBy", SqlDbType.VarChar, 50);
                param[15].Value = string.IsNullOrEmpty(ModifiedBy) ? "" : ModifiedBy;

                param[16] = new SqlParameter("@Counter", SqlDbType.VarChar, 100);
                param[16].Value = Counter;

                param[17] = new SqlParameter("@paramPaxCount", SqlDbType.VarChar, 100);
                param[17].Value = paxCount;

                param[18] = new SqlParameter("@ParamDaysToDeparture", SqlDbType.VarChar, 100);
                param[18].Value = DTD;

                param[19] = new SqlParameter("@paramStops", SqlDbType.VarChar, 200);
                param[19].Value = Stops;

                param[20] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 100);
                param[20].Direction = ParameterDirection.Output;

                param[21] = new SqlParameter("@ParamGroupCode", SqlDbType.VarChar, 500);
                param[21].Value = GroupCode;

                param[22] = new SqlParameter("@ParamDBD", SqlDbType.VarChar, 100);
                param[22].Value = DBD;

                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_FlightMarkupDetail_UK", param);
                return param[20].Value.ToString();
            }
        }
        catch (Exception ex)
        {
            return "false";
        }
    }


    public bool SET_FlightMarkupDetail(string Counter)
    {
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnectionMarkup())
            {
                param[0] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[0].Value = Counter;

                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_FlightMarkupDetail", param);
                return true;
            }
        }
        catch
        {
            return false;
        }
    }
    public bool SET_FlightMarkupDetailUS(string Counter)
    {
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnectionMarkupUS())
            {



                param[0] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[0].Value = Counter;


                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_FlightMarkupDetail", param);
                return true;
            }
        }
        catch
        {
            return false;
        }
    }
    public DataTable GET_FlightMarkupDetail(string ID, string From, string To, string AirV, string Provider, string Category,
        string CClass, string FareType, string CompanyID, string CampID, string JourneyType, string RollName, string PageName)
    {
        SqlParameter[] param = new SqlParameter[14];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnectionMarkup())
            {
                if (!string.IsNullOrEmpty(ID))
                {
                    param[0] = new SqlParameter("@ParamID", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(ID);
                }
                if (!string.IsNullOrEmpty(From))
                {
                    param[1] = new SqlParameter("@ParamFrom", SqlDbType.VarChar, 50);
                    param[1].Value = From.ToUpper();
                }
                if (!string.IsNullOrEmpty(To))
                {
                    param[2] = new SqlParameter("@ParamTo", SqlDbType.VarChar, 50);
                    param[2].Value = To.ToUpper();
                }
                if (!string.IsNullOrEmpty(AirV))
                {
                    param[3] = new SqlParameter("@ParamAirV", SqlDbType.VarChar, 50);
                    param[3].Value = AirV.ToUpper();
                }
                if (!string.IsNullOrEmpty(Provider))
                {
                    param[4] = new SqlParameter("@ParamProvider", SqlDbType.VarChar, 50);
                    param[4].Value = Provider.ToUpper();
                }
                if (!string.IsNullOrEmpty(Category))
                {
                    param[5] = new SqlParameter("@ParamCategory", SqlDbType.VarChar, 50);
                    param[5].Value = Category.ToUpper();
                }
                if (!string.IsNullOrEmpty(CClass))
                {
                    param[6] = new SqlParameter("@ParamClass", SqlDbType.VarChar, 50);
                    param[6].Value = CClass.ToUpper();
                }
                if (!string.IsNullOrEmpty(FareType))
                {
                    param[7] = new SqlParameter("@ParamFareType", SqlDbType.VarChar, 50);
                    param[7].Value = FareType.ToUpper();
                }
                if (!string.IsNullOrEmpty(CompanyID))
                {
                    param[8] = new SqlParameter("@ParamCompanyID", SqlDbType.VarChar);
                    param[8].Value = CompanyID;
                }
                if (!string.IsNullOrEmpty(CampID))
                {
                    param[9] = new SqlParameter("@ParamCampID", SqlDbType.VarChar, 50);
                    param[9].Value = CampID;
                }
                if (!string.IsNullOrEmpty(JourneyType))
                {
                    param[10] = new SqlParameter("@ParamJourneyType", SqlDbType.VarChar, 50);
                    param[10].Value = JourneyType.ToUpper();
                }
                if (!string.IsNullOrEmpty(RollName))
                {
                    param[11] = new SqlParameter("@ParamRollName", SqlDbType.NVarChar, 50);
                    param[11].Value = RollName;
                }
                if (!string.IsNullOrEmpty(PageName))
                {
                    param[12] = new SqlParameter("@ParamPageName", SqlDbType.NVarChar, 500);
                    param[12].Value = PageName;
                }
                param[13] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[13].Value = "Select";

                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_FlightMarkupDetail", param);
                return ds.Tables[0];
            }
        }
        catch
        {
            return null;
        }
    }



    public DataTable GET_FlightMarkupDetailUS(string ID, string From, string To, string AirV, string Provider, string Category,
     string CClass, string FareType, string CompanyID, string CampID, string JourneyType, string RollName, string PageName,
     int PaxCount, string ModifyBy,string Amount,string AmountType,string FromDate,string ToDate,string UserId,int DTD,string RestrictedClass)
    {
        SqlParameter[] param = new SqlParameter[22];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnectionMarkupUS())
            {
                if (!string.IsNullOrEmpty(ID))
                {
                    param[0] = new SqlParameter("@ParamID", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(ID);
                }
                if (!string.IsNullOrEmpty(From))
                {
                    param[1] = new SqlParameter("@ParamFrom", SqlDbType.VarChar, 50);
                    param[1].Value = From.ToUpper();
                }
                if (!string.IsNullOrEmpty(To))
                {
                    param[2] = new SqlParameter("@ParamTo", SqlDbType.VarChar, 50);
                    param[2].Value = To.ToUpper();
                }
                if (!string.IsNullOrEmpty(AirV))
                {
                    param[3] = new SqlParameter("@ParamAirV", SqlDbType.VarChar, 50);
                    param[3].Value = AirV.ToUpper();
                }
                if (!string.IsNullOrEmpty(Provider))
                {
                    param[4] = new SqlParameter("@ParamProvider", SqlDbType.VarChar, 50);
                    param[4].Value = Provider.ToUpper();
                }
                if (!string.IsNullOrEmpty(Category))
                {
                    param[5] = new SqlParameter("@ParamCategory", SqlDbType.VarChar, 50);
                    param[5].Value = Category.ToUpper();
                }
                if (!string.IsNullOrEmpty(CClass))
                {
                    param[6] = new SqlParameter("@ParamClass", SqlDbType.VarChar, 50);
                    param[6].Value = CClass.ToUpper();
                }
                if (!string.IsNullOrEmpty(FareType))
                {
                    param[7] = new SqlParameter("@ParamFareType", SqlDbType.VarChar, 50);
                    param[7].Value = FareType.ToUpper();
                }
                if (!string.IsNullOrEmpty(CompanyID))
                {
                    param[8] = new SqlParameter("@ParamCompanyID", SqlDbType.VarChar);
                    param[8].Value = CompanyID;
                }
                if (!string.IsNullOrEmpty(CampID))
                {
                    param[9] = new SqlParameter("@ParamCampID", SqlDbType.VarChar, 50);
                    param[9].Value = CampID;
                }
                if (!string.IsNullOrEmpty(JourneyType))
                {
                    param[10] = new SqlParameter("@ParamJourneyType", SqlDbType.VarChar, 50);
                    param[10].Value = JourneyType.ToUpper();
                }
                if (!string.IsNullOrEmpty(RollName))
                {
                    param[11] = new SqlParameter("@ParamRollName", SqlDbType.NVarChar, 50);
                    param[11].Value = RollName;
                }
                if (!string.IsNullOrEmpty(PageName))
                {
                    param[12] = new SqlParameter("@ParamPageName", SqlDbType.NVarChar, 500);
                    param[12].Value = PageName;
                }

                param[13] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[13].Value = "Select";

               
                param[14] = new SqlParameter("@paramPaxCount", SqlDbType.VarChar, 500);
                param[14].Value = PaxCount;
               

                if (!string.IsNullOrEmpty(ModifyBy))
                {
                    param[15] = new SqlParameter("@ParamModifiedBy", SqlDbType.VarChar, 500);
                    param[15].Value = ModifyBy;
                }

                if (!string.IsNullOrEmpty(Amount))
                {
                    param[16] = new SqlParameter("@ParamAmount", SqlDbType.VarChar, 500);
                    param[16].Value = Amount;
                }
                if (!string.IsNullOrEmpty(AmountType))
                {
                    param[17] = new SqlParameter("@ParamAmountType", SqlDbType.VarChar, 500);
                    param[17].Value = AmountType;
                }
                if (!string.IsNullOrEmpty(FromDate))
                {
                    param[18] = new SqlParameter("@ParamValidFromDate", SqlDbType.VarChar, 50);
                    param[18].Value = Convert.ToDateTime(FromDate);
                }
                if (!string.IsNullOrEmpty(ToDate))
                {
                    param[19] = new SqlParameter("@ParamValidToDate", SqlDbType.DateTime);
                    param[19].Value = Convert.ToDateTime(ToDate);
				}
                if (!string.IsNullOrEmpty(UserId))
                {
                    param[20] = new SqlParameter("@ParamUserID", SqlDbType.VarChar, 500);
                    param[20].Value = UserId;
                }
               
                param[21] = new SqlParameter("@ParamDaysToDeparture", SqlDbType.Int, 500);
                param[21].Value = DTD;

                if (!string.IsNullOrEmpty(RestrictedClass))
                {
                    param[20] = new SqlParameter("@paramRestrictedClass", SqlDbType.VarChar, 500);
                    param[20].Value = RestrictedClass;
                }

                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_FlightMarkupDetail", param);
                return ds.Tables[0];
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public DataTable GET_FlightMarkupDetailUK(string ID, string From, string To, string AirV, string Provider, string Category,
    string CClass, string FareType, string CompanyID, string CampID, string JourneyType, string RollName, string PageName, int PaxCount, string ModifyBy)
    {
        SqlParameter[] param = new SqlParameter[16];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnectionMarkup())
            {
                if (!string.IsNullOrEmpty(ID))
                {
                    param[0] = new SqlParameter("@ParamID", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(ID);
                }
                if (!string.IsNullOrEmpty(From))
                {
                    param[1] = new SqlParameter("@ParamFrom", SqlDbType.VarChar, 50);
                    param[1].Value = From.ToUpper();
                }
                if (!string.IsNullOrEmpty(To))
                {
                    param[2] = new SqlParameter("@ParamTo", SqlDbType.VarChar, 50);
                    param[2].Value = To.ToUpper();
                }
                if (!string.IsNullOrEmpty(AirV))
                {
                    param[3] = new SqlParameter("@ParamAirV", SqlDbType.VarChar, 50);
                    param[3].Value = AirV.ToUpper();
                }
                if (!string.IsNullOrEmpty(Provider))
                {
                    param[4] = new SqlParameter("@ParamProvider", SqlDbType.VarChar, 50);
                    param[4].Value = Provider.ToUpper();
                }
                if (!string.IsNullOrEmpty(Category))
                {
                    param[5] = new SqlParameter("@ParamCategory", SqlDbType.VarChar, 50);
                    param[5].Value = Category.ToUpper();
                }
                if (!string.IsNullOrEmpty(CClass))
                {
                    param[6] = new SqlParameter("@ParamClass", SqlDbType.VarChar, 50);
                    param[6].Value = CClass.ToUpper();
                }
                if (!string.IsNullOrEmpty(FareType))
                {
                    param[7] = new SqlParameter("@ParamFareType", SqlDbType.VarChar, 50);
                    param[7].Value = FareType.ToUpper();
                }
                if (!string.IsNullOrEmpty(CompanyID))
                {
                    param[8] = new SqlParameter("@ParamCompanyID", SqlDbType.VarChar);
                    param[8].Value = CompanyID;
                }
                if (!string.IsNullOrEmpty(CampID))
                {
                    param[9] = new SqlParameter("@ParamCampID", SqlDbType.VarChar, 50);
                    param[9].Value = CampID;
                }
                if (!string.IsNullOrEmpty(JourneyType))
                {
                    param[10] = new SqlParameter("@ParamJourneyType", SqlDbType.VarChar, 50);
                    param[10].Value = JourneyType.ToUpper();
                }
                if (!string.IsNullOrEmpty(RollName))
                {
                    param[11] = new SqlParameter("@ParamRollName", SqlDbType.NVarChar, 50);
                    param[11].Value = RollName;
                }
                if (!string.IsNullOrEmpty(PageName))
                {
                    param[12] = new SqlParameter("@ParamPageName", SqlDbType.NVarChar, 500);
                    param[12].Value = PageName;
                }

                param[13] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[13].Value = "Select";

                param[14] = new SqlParameter("@paramPaxCount", SqlDbType.VarChar, 500);
                param[14].Value = PaxCount;

                if (!string.IsNullOrEmpty(PageName))
                {
                    param[14] = new SqlParameter("@paramPaxCount", SqlDbType.VarChar, 500);
                    param[14].Value = PaxCount;
                }

                if (!string.IsNullOrEmpty(ModifyBy))
                {
                    param[15] = new SqlParameter("@ParamModifiedBy", SqlDbType.VarChar, 500);
                    param[15].Value = ModifyBy;
                }


                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_FlightMarkupDetail_UK", param);
                return ds.Tables[0];
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }


    public DataTable GET_SkyScannerDetail(string FromDate, string ToDate)
    {
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {

                if (!string.IsNullOrEmpty(FromDate))
                {
                    param[0] = new SqlParameter("@ParamFromDate", SqlDbType.VarChar, 50);
                    param[0].Value = Convert.ToDateTime(FromDate);
                }
                if (!string.IsNullOrEmpty(ToDate))
                {
                    param[1] = new SqlParameter("@ParamToDate", SqlDbType.DateTime);
                    param[1].Value = Convert.ToDateTime(ToDate);
                }


                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SkyScannerReport_tmp", param);
                return ds.Tables[0];
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }


    public string UpdateMarkupExcel(string ID, string UpdateField, string Value, string UpdetedBy)
    {
        try
        {
            string FieldName = "";
            switch (UpdateField)
            {
                case "MarkupFrom": FieldName = "Agnt_AirF_Markup_From"; break;
                case "MarkupTo": FieldName = "Agnt_AirF_Markup_To"; break;
                case "AirV": FieldName = "Agnt_AirF_Markup_AirV"; break;
                case "Provider": FieldName = "Agnt_AirF_Markup_Provider"; break;
                case "Category": FieldName = "Agnt_AirF_Markup_Category"; break;
                case "MarkupClass": FieldName = "Agnt_AirF_Markup_Class"; break;
                case "FareType": FieldName = "Agnt_AirF_Markup_Fare_Type"; break;
                case "JourneyType": FieldName = "Agnt_AirF_Markup_JourneyType"; break;
                case "Amount": FieldName = "Agnt_AirF_Markup_Amount"; break;
                case "AmountType": FieldName = "Agnt_AirF_Markup_Amount_Type"; break;
                case "ValidFromDate": FieldName = "Agnt_AirF_Markup_ValidFromDate"; break;
                case "ValidToDate": FieldName = "Agnt_AirF_Markup_ValidToDate"; break;
                case "DaysToDeparture": FieldName = "DaysToDeparture"; break;
                case "PaxCount": FieldName = "Agnt_AirF_No_Of_Pax"; break;

            }
            string Query = "";
            if (UpdateField == "Amount")
            {
                Query = "update FlightMarkupDetail set " + FieldName + "=" + Value.ToUpper() + ",Agnt_AirF_Markup_ModifiedBy='" + UpdetedBy + "',Agnt_AirF_Markup_LastModifiedDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' Where Agnt_AirF_Markup_ID=" + ID;
            }
            else
            {
                if (UpdateField == "ValidFromDate" || UpdateField == "ValidToDate")
                {
                    Query = "update FlightMarkupDetail set " + FieldName + "='" + Convert.ToDateTime(Value).ToString("yyyy-MM-dd") + "',Agnt_AirF_Markup_ModifiedBy='" + UpdetedBy + "',Agnt_AirF_Markup_LastModifiedDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' Where Agnt_AirF_Markup_ID=" + ID;
                }
                else
                {
                    Query = "update FlightMarkupDetail set " + FieldName + "='" + Value.ToUpper() + "',Agnt_AirF_Markup_ModifiedBy='" + UpdetedBy + "',Agnt_AirF_Markup_LastModifiedDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' Where Agnt_AirF_Markup_ID=" + ID;
                }
            }
            using (SqlConnection connection = DataConnection.GetConnectionMarkup())
            {
                if (SqlHelper.ExecuteNonQuery(connection, CommandType.Text, Query) == 1)
                {
                    return "true";
                }
                else
                    return "false";

            }
        }
        catch (Exception ex) { return "false"; }
    }

    public string UpdateMarkupExcelUK(string ID, string UpdateField, string Value, string UpdetedBy)
    {
        try
        {
            string FieldName = "";
            switch (UpdateField)
            {
                case "MarkupFrom": FieldName = "Agnt_AirF_Markup_From"; break;
                case "MarkupTo": FieldName = "Agnt_AirF_Markup_To"; break;
                case "AirV": FieldName = "Agnt_AirF_Markup_AirV"; break;
                case "Provider": FieldName = "Agnt_AirF_Markup_Provider"; break;
                case "Category": FieldName = "Agnt_AirF_Markup_Category"; break;
                case "MarkupClass": FieldName = "Agnt_AirF_Markup_Class"; break;
                case "FareType": FieldName = "Agnt_AirF_Markup_Fare_Type"; break;
                case "JourneyType": FieldName = "Agnt_AirF_Markup_JourneyType"; break;
                case "Amount": FieldName = "Agnt_AirF_Markup_Amount"; break;
                case "AmountType": FieldName = "Agnt_AirF_Markup_Amount_Type"; break;
                case "ValidFromDate": FieldName = "Agnt_AirF_Markup_ValidFromDate"; break;
                case "ValidToDate": FieldName = "Agnt_AirF_Markup_ValidToDate"; break;
                case "DaysToDeparture": FieldName = "DaysToDeparture"; break;
                case "PaxCount": FieldName = "Agnt_AirF_No_Of_Pax"; break;
                case "Stops": FieldName = "Agnt_AirF_Stops"; break;
                case "IsGroupCode": FieldName = "IsGroupCode"; break;
                case "DaysBetweenDeparture": FieldName = "DaysBetweenDeparture"; break;
            }
            string Query = "";
            if (UpdateField == "Amount")
            {
                Query = "update FlightMarkupDetail_UK set " + FieldName + "=" + Value.ToUpper() + ",Agnt_AirF_Markup_ModifiedBy='" + UpdetedBy + "',Agnt_AirF_Markup_LastModifiedDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' Where Agnt_AirF_Markup_ID=" + ID;
            }
            else
            {
                if (UpdateField == "ValidFromDate" || UpdateField == "ValidToDate")
                {
                    Query = "update FlightMarkupDetail_UK set " + FieldName + "='" + Convert.ToDateTime(Value).ToString("yyyy-MM-dd") + "',Agnt_AirF_Markup_ModifiedBy='" + UpdetedBy + "',Agnt_AirF_Markup_LastModifiedDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' Where Agnt_AirF_Markup_ID=" + ID;
                }
                else
                {
                    Query = "update FlightMarkupDetail_UK set " + FieldName + "='" + Value.ToUpper() + "',Agnt_AirF_Markup_ModifiedBy='" + UpdetedBy + "',Agnt_AirF_Markup_LastModifiedDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' Where Agnt_AirF_Markup_ID=" + ID;
                }
            }
            using (SqlConnection connection = DataConnection.GetConnectionMarkup())
            {
                if (SqlHelper.ExecuteNonQuery(connection, CommandType.Text, Query) == 1)
                {
                    return "true";
                }
                else
                    return "false";

            }
        }
        catch (Exception ex) { return "false"; }
    }
    public string UpdateMarkupExcelUS(string ID, string UpdateField, string Value, string UpdetedBy)
    {
        try
        {
            string FieldName = "";
            switch (UpdateField)
            {
                case "MarkupFrom": FieldName = "Agnt_AirF_Markup_From"; break;
                case "MarkupTo": FieldName = "Agnt_AirF_Markup_To"; break;
                case "AirV": FieldName = "Agnt_AirF_Markup_AirV"; break;
                case "Provider": FieldName = "Agnt_AirF_Markup_Provider"; break;
                case "Category": FieldName = "Agnt_AirF_Markup_Category"; break;
                case "MarkupClass": FieldName = "Agnt_AirF_Markup_Class"; break;
                case "FareType": FieldName = "Agnt_AirF_Markup_Fare_Type"; break;
                case "JourneyType": FieldName = "Agnt_AirF_Markup_JourneyType"; break;
                case "Amount": FieldName = "Agnt_AirF_Markup_Amount"; break;
                case "AmountType": FieldName = "Agnt_AirF_Markup_Amount_Type"; break;
                case "ValidFromDate": FieldName = "Agnt_AirF_Markup_ValidFromDate"; break;
                case "ValidToDate": FieldName = "Agnt_AirF_Markup_ValidToDate"; break;
                case "DaysToDeparture": FieldName = "DaysToDeparture"; break;
                case "PaxCount": FieldName = "Agnt_AirF_No_Of_Pax"; break;
                case "RestrictedClass": FieldName = "Agnt_AirF_Restricted_Class"; break;
                case "TripType": FieldName = "Agnt_AirF_TripType"; break;
            }
            string Query = "";
            if (UpdateField == "Amount")
            {
                Query = "update FlightMarkupDetail set " + FieldName + "=" + Value.ToUpper() + ",Agnt_AirF_Markup_ModifiedBy='" + UpdetedBy + "',Agnt_AirF_Markup_LastModifiedDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' Where Agnt_AirF_Markup_ID=" + ID;
            }
            else
            {
                if (UpdateField == "ValidFromDate" || UpdateField == "ValidToDate")
                {
                    Query = "update FlightMarkupDetail set " + FieldName + "='" + Convert.ToDateTime(Value).ToString("yyyy-MM-dd") + "',Agnt_AirF_Markup_ModifiedBy='" + UpdetedBy + "',Agnt_AirF_Markup_LastModifiedDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' Where Agnt_AirF_Markup_ID=" + ID;
                }
                else
                {
                    Query = "update FlightMarkupDetail set " + FieldName + "='" + Value.ToUpper() + "',Agnt_AirF_Markup_ModifiedBy='" + UpdetedBy + "',Agnt_AirF_Markup_LastModifiedDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' Where Agnt_AirF_Markup_ID=" + ID;
                }
            }
            using (SqlConnection connection = DataConnection.GetConnectionMarkupUS())
            {
                if (SqlHelper.ExecuteNonQuery(connection, CommandType.Text, Query) == 1)
                {
                    return "true";
                }
                else
                    return "false";

            }
        }
        catch (Exception ex) { return "false"; }
    }

    public bool DeleteAllFlightMarkupUS(string Counter)
    {
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnectionMarkupUS())
            {



                param[0] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[0].Value = Counter;


                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_FlightMarkupDetail", param);
                return true;
            }
        }
        catch
        {
            return false;
        }
    }
    public bool DeleteAllFlightMarkupUK(string Counter)
    {
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnectionMarkupUS())
            {



                param[0] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[0].Value = Counter;


                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_FlightMarkupDetail", param);
                return true;
            }
        }
        catch
        {
            return false;
        }
    }

    public string DeleteFlightMarkup(string ID, string Counter)
    {
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnectionMarkup())
            {

                if (!string.IsNullOrEmpty(ID))
                {
                    param[0] = new SqlParameter("@ParamID", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(ID);
                }

                param[1] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[1].Value = Counter;

                param[2] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 500);
                param[2].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_FlightMarkupDetail", param);
                return param[2].Value.ToString();
            }
        }
        catch
        {
            return "false";
        }
    }

    public string DeleteFlightMarkupUS(string ID, string Counter)
    {
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnectionMarkupUS())
            {

                if (!string.IsNullOrEmpty(ID))
                {
                    param[0] = new SqlParameter("@ParamID", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(ID);
                }

                param[1] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[1].Value = Counter;

                param[2] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 500);
                param[2].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_FlightMarkupDetail", param);
                return param[2].Value.ToString();
            }
        }
        catch
        {
            return "false";
        }
    }


    public string DeleteFlightMarkupUK(string ID, string Counter)
    {
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnectionMarkup())
            {

                if (!string.IsNullOrEmpty(ID))
                {
                    param[0] = new SqlParameter("@ParamID", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(ID);
                }

                param[1] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[1].Value = Counter;

                param[2] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 500);
                param[2].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_FlightMarkupDetail_UK", param);
                return param[2].Value.ToString();
            }
        }
        catch
        {
            return "false";
        }
    }
    public string UpdateDiscountMarkup_US(bool UseStatus)
    {
        try
        {

            using (SqlConnection connection = DataConnection.GetConnectionMarkupUS())
            {

                string query = "Update FlightMarkupDetail set isActive='" + UseStatus + "' where Agnt_AirF_Markup_Amount<0";

                if (SqlHelper.ExecuteNonQuery(connection, CommandType.Text, query) > 1)
                {
                    return "true";
                }
                else
                    return "false";

            }
        }
        catch (Exception ex)
        {
            return "false";
        }


    }
    public string UpdateDiscountMarkup_UK(bool UseStatus)
    {
        try
        {

            using (SqlConnection connection = DataConnection.GetConnectionMarkup())
            {

                string query = "Update FlightMarkupDetail_UK set isActive='" + UseStatus + "' where Agnt_AirF_Markup_Amount<0 and COMP_DTL_Company_ID NOT IN ('FLTTROTT_CA','C2BCA','TRVJUNCTION_CA')";


                if (SqlHelper.ExecuteNonQuery(connection, CommandType.Text, query) > 1)
                {
                    return "true";
                }
                else
                    return "false";

            }
        }
        catch (Exception ex)
        {
            return "false";
        }


    }
    public string UpdateDiscountMarkup_CA(bool UseStatus)
    {
        try
        {

            using (SqlConnection connection = DataConnection.GetConnectionMarkup())
            {

                string query = "Update FlightMarkupDetail_UK set isActive='" + UseStatus + "' where Agnt_AirF_Markup_Amount<0 and COMP_DTL_Company_ID in ('FLTTROTT_CA','C2BCA','TRVJUNCTION_CA')";

                if (SqlHelper.ExecuteNonQuery(connection, CommandType.Text, query) > 1)
                {
                    return "true";
                }
                else
                    return "false";

            }
        }
        catch (Exception ex)
        {
            return "false";
        }


    }

    public bool CheckDiscountMarkup_US()
    {
        bool finalstatus = true;
        try
        {
            using (SqlConnection connection = DataConnection.GetConnectionMarkupUS())
            {
                SqlCommand command = new SqlCommand("select top 1 * from FlightMarkupDetail where isActive='true' and Agnt_AirF_Markup_Amount<0", connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    finalstatus = true;
                }
                else
                {
                    finalstatus = false;
                }


            }
        }
        catch (Exception ex)
        {
            return true;
        }

        return finalstatus;
    }




    #endregion

    #region Air Offer Master

    public string SET_AirOfferMaster(string AirOfferMasterID, string Description, string Provider, string FromDestination,
        string ToDestination, string DepartDate, string ArrivalDate, string ExpairDate, string ClassCategory, string SearchDays,
        string NoOfSeats, string WeekEndCharges, string JourneyType, string FixDate, string Company, string CampID, string MinDays,
        string BookDays, string ModifyBy, ref string AirOfferID, string FareType, string DeleteAirOffer, string Counter)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[24];
            using (SqlConnection Connection = DataConnection.GetConnection())
            {
                if (!String.IsNullOrEmpty(AirOfferMasterID))
                {
                    param[0] = new SqlParameter("@ParamAirOfferMasterID", SqlDbType.Int);
                    param[0].Value = Convert.ToInt64(AirOfferMasterID);
                }
                if (!String.IsNullOrEmpty(Description))
                {
                    param[1] = new SqlParameter("@ParamDescription", SqlDbType.VarChar);
                    param[1].Value = Description;
                }
                if (!String.IsNullOrEmpty(Provider))
                {
                    param[2] = new SqlParameter("@ParamProvider", SqlDbType.VarChar);
                    param[2].Value = Provider;
                }
                if (!String.IsNullOrEmpty(FromDestination))
                {
                    param[3] = new SqlParameter("@ParamFromDestination", SqlDbType.VarChar);
                    param[3].Value = FromDestination;
                }
                if (!String.IsNullOrEmpty(ToDestination))
                {
                    param[4] = new SqlParameter("@ParamToDestination", SqlDbType.VarChar);
                    param[4].Value = ToDestination;
                }
                if (!String.IsNullOrEmpty(DepartDate))
                {
                    param[5] = new SqlParameter("@ParamDepartDate", SqlDbType.DateTime);
                    param[5].Value = Convert.ToDateTime(DepartDate);
                }
                if (!String.IsNullOrEmpty(ArrivalDate))
                {
                    param[6] = new SqlParameter("@ParamArrivalDate", SqlDbType.DateTime);
                    param[6].Value = Convert.ToDateTime(ArrivalDate);
                }
                if (!String.IsNullOrEmpty(ExpairDate))
                {
                    param[7] = new SqlParameter("@ParamExpairDate", SqlDbType.DateTime);
                    param[7].Value = Convert.ToDateTime(ExpairDate);
                }
                if (!String.IsNullOrEmpty(ClassCategory))
                {
                    param[8] = new SqlParameter("@ParamClassCategory", SqlDbType.VarChar);
                    param[8].Value = ClassCategory;
                }
                if (!String.IsNullOrEmpty(SearchDays))
                {
                    param[9] = new SqlParameter("@ParamSearchDays", SqlDbType.VarChar);
                    param[9].Value = SearchDays;
                }
                if (!String.IsNullOrEmpty(NoOfSeats))
                {
                    param[10] = new SqlParameter("@ParamNoOfSeats", SqlDbType.VarChar);
                    param[10].Value = NoOfSeats;
                }
                if (!String.IsNullOrEmpty(WeekEndCharges))
                {
                    param[11] = new SqlParameter("@ParamWeekEndCharges", SqlDbType.Money);
                    param[11].Value = Convert.ToDouble(WeekEndCharges);
                }
                if (!String.IsNullOrEmpty(JourneyType))
                {
                    param[12] = new SqlParameter("@ParamJourneyType", SqlDbType.VarChar);
                    param[12].Value = JourneyType;
                }
                if (!String.IsNullOrEmpty(FixDate))
                {
                    param[13] = new SqlParameter("@ParamFixDate", SqlDbType.VarChar);
                    param[13].Value = FixDate;
                }
                if (!String.IsNullOrEmpty(Company))
                {
                    param[14] = new SqlParameter("@ParamCompany", SqlDbType.VarChar);
                    param[14].Value = Company;
                }
                if (!String.IsNullOrEmpty(CampID))
                {
                    param[15] = new SqlParameter("@ParamCampID", SqlDbType.VarChar);
                    param[15].Value = CampID;
                }
                if (!String.IsNullOrEmpty(MinDays))
                {
                    param[16] = new SqlParameter("@ParamMinDays", SqlDbType.VarChar);
                    param[16].Value = MinDays;
                }

                if (!String.IsNullOrEmpty(BookDays))
                {
                    param[17] = new SqlParameter("@ParamBookDays", SqlDbType.VarChar);
                    param[17].Value = BookDays;
                }

                if (!String.IsNullOrEmpty(ModifyBy))
                {
                    param[18] = new SqlParameter("@ParamModifyBy", SqlDbType.VarChar);
                    param[18].Value = ModifyBy;
                }
                param[19] = new SqlParameter("@ParamAirOfferID", SqlDbType.Int);
                param[19].Direction = ParameterDirection.Output;

                if (!String.IsNullOrEmpty(FareType))
                {
                    param[20] = new SqlParameter("@ParamFareType", SqlDbType.VarChar);
                    param[20].Value = FareType;
                }
                param[21] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[21].Value = Counter;

                param[22] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 500);
                param[22].Direction = ParameterDirection.Output;

                if (!String.IsNullOrEmpty(DeleteAirOffer) && Counter == "Delete")
                {
                    param[23] = new SqlParameter("@ParamDeleteAirOffer", SqlDbType.VarChar);
                    param[23].Value = DeleteAirOffer;
                }
                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "GET_SET_AirOfferMaster", param);

                if (Counter.ToLower() == "insert")
                    AirOfferID = param[19].Value.ToString();
                return param[22].Value.ToString();
            }
        }
        catch { return "false"; }
    }

    public DataTable Get_AirOfferMaster(string FromDestination, string ToDestination, string JourneyType, string Company, string CampID, string FareType, string CarierName)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[8];
            using (SqlConnection Connection = DataConnection.GetConnection())
            {


                if (!String.IsNullOrEmpty(FromDestination))
                {
                    param[0] = new SqlParameter("@ParamFromDestination", SqlDbType.VarChar);
                    param[0].Value = FromDestination;
                }
                if (!String.IsNullOrEmpty(ToDestination))
                {
                    param[1] = new SqlParameter("@ParamToDestination", SqlDbType.VarChar);
                    param[1].Value = ToDestination;
                }

                if (!String.IsNullOrEmpty(JourneyType))
                {
                    param[2] = new SqlParameter("@ParamJourneyType", SqlDbType.VarChar);
                    param[2].Value = JourneyType;
                }
                if (!String.IsNullOrEmpty(Company))
                {
                    param[3] = new SqlParameter("@ParamCompany", SqlDbType.VarChar);
                    param[3].Value = Company;
                }
                if (!String.IsNullOrEmpty(CampID))
                {
                    param[4] = new SqlParameter("@ParamCampID", SqlDbType.VarChar);
                    param[4].Value = CampID;
                }
                if (!String.IsNullOrEmpty(FareType))
                {
                    param[5] = new SqlParameter("@ParamFareType", SqlDbType.VarChar);
                    param[5].Value = FareType;
                }
                if (!String.IsNullOrEmpty(CarierName))
                {
                    param[6] = new SqlParameter("@ParamCarierName", SqlDbType.VarChar);
                    param[6].Value = CarierName;
                }
                param[7] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[7].Value = "Select";
                DataSet ds = SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "GET_SET_AirOfferMaster", param);
                return ds.Tables[0];

            }
        }
        catch { return null; }
    }

    public bool InsertAirOffferSector(DataTable OffferSector)
    {
        string _CommandText = "AirOfferSector_Insert";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            param[0] = new SqlParameter("@ParamAirOfferSector", OffferSector);
            param[1] = new SqlParameter("@ParamError", SqlDbType.VarChar, 8000);
            param[1].Direction = ParameterDirection.Output;
            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            if (count > 0)
                return true;
            else
                return false;
        }
        catch { return false; }
    }

    public bool InsertAirOffferPricing(DataTable OffferPricing)
    {
        string _CommandText = "AirOfferPricing_Insert";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            param[0] = new SqlParameter("@ParamAirOfferPricing", OffferPricing);

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);
            if (count > 0)
                return true;
            else
                return false;
        }
        catch { return false; }
    }

    public DataTable GenAirOfferSector(string AirOfferID)
    {
        DataSet ds;
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            _CommandText = "AirOfferSector_Select";
            if (!string.IsNullOrEmpty(AirOfferID))
            {
                param[0] = new SqlParameter("@paramAirOfferID", SqlDbType.Int);
                param[0].Value = AirOfferID;
            }
            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);

            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }

        }
        catch
        {
            return null;

        }
    }
    public DataTable GenAirOfferPricing(string AirOfferID)
    {
        DataSet ds;
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            _CommandText = "AirOfferPricing_Select";
            if (!string.IsNullOrEmpty(AirOfferID))
            {
                param[0] = new SqlParameter("@paramAirOfferID", SqlDbType.Int);
                param[0].Value = AirOfferID;
            }
            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnection(), CommandType.StoredProcedure, _CommandText, param);

            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }

        }
        catch
        {
            return null;

        }
    }
    #endregion

    #region Air Offer Dump air offer

    public DataTable GET_FR_Dmp_AirFareOffers(string FaredetailId, string From, string To, string Airline_Code, string Class,
        string ClassType, string BaseFare, string Tax, string Total, string FilledBy, string FillDateFrom, string FillDateTo,
        string Directflt, string Travel_DateStartFrom, string Travel_DateStartTo, string Travel_DateEndFrom,
        string Travel_DateEndTo, string SplRequest, string ExpOffers_DateFrom, string ExpOffers_DateTo, string Website,
        string Country_Code, string Continent_Code, string Keyword, string ShowOffer, string AvailSeats)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[28];
            using (SqlConnection Connection = DataConnection.GetConnection())
            {
                if (!String.IsNullOrEmpty(FaredetailId))
                {
                    param[0] = new SqlParameter("@ParamFaredetailId", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(FaredetailId);
                }
                if (!String.IsNullOrEmpty(From))
                {
                    param[1] = new SqlParameter("@ParamFrom", SqlDbType.VarChar, 50);
                    param[1].Value = From;
                }

                if (!String.IsNullOrEmpty(To))
                {
                    param[2] = new SqlParameter("@ParamTo", SqlDbType.VarChar, 50);
                    param[2].Value = To;
                }
                if (!String.IsNullOrEmpty(Airline_Code))
                {
                    param[3] = new SqlParameter("@ParamAirline_Code", SqlDbType.VarChar, 50);
                    param[3].Value = Airline_Code;
                }
                if (!String.IsNullOrEmpty(Class))
                {
                    param[4] = new SqlParameter("@ParamClass", SqlDbType.VarChar, 50);
                    param[4].Value = Class;
                }
                if (!String.IsNullOrEmpty(ClassType))
                {
                    param[5] = new SqlParameter("@ParamClassType", SqlDbType.VarChar, 50);
                    param[5].Value = ClassType;
                }
                if (!String.IsNullOrEmpty(BaseFare))
                {
                    param[6] = new SqlParameter("@ParamBaseFare", SqlDbType.Money);
                    param[6].Value = Convert.ToDouble(BaseFare);
                }
                if (!String.IsNullOrEmpty(Tax))
                {
                    param[7] = new SqlParameter("@ParamTax", SqlDbType.Money);
                    param[7].Value = Convert.ToDouble(Tax);
                }
                if (!String.IsNullOrEmpty(Total))
                {
                    param[8] = new SqlParameter("@ParamTotal", SqlDbType.Money);
                    param[8].Value = Convert.ToDouble(Total);
                }
                if (!String.IsNullOrEmpty(FilledBy))
                {
                    param[9] = new SqlParameter("@ParamFilledBy", SqlDbType.VarChar, 50);
                    param[9].Value = FilledBy;
                }
                if (!String.IsNullOrEmpty(FillDateFrom))
                {
                    param[10] = new SqlParameter("@ParamFillDateFrom", SqlDbType.DateTime);
                    param[10].Value = Convert.ToDateTime(FillDateFrom);
                }
                if (!String.IsNullOrEmpty(FillDateTo))
                {
                    param[11] = new SqlParameter("@ParamFillDateTo", SqlDbType.DateTime);
                    param[11].Value = Convert.ToDateTime(FillDateTo);
                }
                if (!String.IsNullOrEmpty(Directflt))
                {
                    param[12] = new SqlParameter("@ParamDirectflt", SqlDbType.VarChar, 50);
                    param[12].Value = Directflt;
                }

                if (!String.IsNullOrEmpty(Travel_DateStartFrom))
                {
                    param[13] = new SqlParameter("@ParamTravel_DateStartFrom", SqlDbType.DateTime);
                    param[13].Value = Convert.ToDateTime(Travel_DateStartFrom);
                }
                if (!String.IsNullOrEmpty(Travel_DateStartTo))
                {
                    param[14] = new SqlParameter("@ParamTravel_DateStartTo", SqlDbType.DateTime);
                    param[14].Value = Convert.ToDateTime(Travel_DateStartTo);
                }
                if (!String.IsNullOrEmpty(Travel_DateEndFrom))
                {
                    param[15] = new SqlParameter("@ParamTravel_DateEndFrom", SqlDbType.DateTime);
                    param[15].Value = Convert.ToDateTime(Travel_DateEndFrom);
                }
                if (!String.IsNullOrEmpty(Travel_DateEndTo))
                {
                    param[16] = new SqlParameter("@ParamTravel_DateEndTo", SqlDbType.DateTime);
                    param[16].Value = Convert.ToDateTime(Travel_DateEndTo);
                }
                if (!String.IsNullOrEmpty(SplRequest))
                {
                    param[17] = new SqlParameter("@ParamSplRequest", SqlDbType.VarChar, 450);
                    param[17].Value = SplRequest;
                }

                if (!String.IsNullOrEmpty(ExpOffers_DateFrom))
                {
                    param[18] = new SqlParameter("@ParamExpOffers_DateFrom", SqlDbType.DateTime);
                    param[18].Value = Convert.ToDateTime(ExpOffers_DateFrom);
                }
                if (!String.IsNullOrEmpty(ExpOffers_DateTo))
                {
                    param[19] = new SqlParameter("@ParamExpOffers_DateTo", SqlDbType.DateTime);
                    param[19].Value = Convert.ToDateTime(ExpOffers_DateTo);
                }
                if (!String.IsNullOrEmpty(Website))
                {
                    param[20] = new SqlParameter("@ParamWebsite", SqlDbType.VarChar, 500);
                    param[20].Value = Website;
                }
                if (!String.IsNullOrEmpty(Country_Code))
                {
                    param[21] = new SqlParameter("@ParamCountry_Code", SqlDbType.VarChar, 50);
                    param[21].Value = Country_Code;
                }
                if (!String.IsNullOrEmpty(Continent_Code))
                {
                    param[22] = new SqlParameter("@ParamContinent_Code", SqlDbType.VarChar, 50);
                    param[22].Value = Continent_Code;
                }
                if (!String.IsNullOrEmpty(Keyword))
                {
                    param[23] = new SqlParameter("@ParamKeyword", SqlDbType.VarChar, 450);
                    param[23].Value = Keyword;
                }
                if (!String.IsNullOrEmpty(ShowOffer))
                {
                    param[24] = new SqlParameter("@ParamShowOffer", SqlDbType.Bit);
                    param[25].Value = Convert.ToBoolean(ShowOffer);
                }
                if (!String.IsNullOrEmpty(AvailSeats))
                {
                    param[26] = new SqlParameter("@ParamAvailSeats", SqlDbType.Int);
                    param[26].Value = Convert.ToInt32(AvailSeats);
                }
                param[27] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[27].Value = "Select";

                DataSet ds = SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "GET_SET_FR_Dmp_AirFareOffers", param);
                return ds.Tables[0];

            }
        }
        catch { return null; }
    }

    public bool SET_FR_Dmp_AirFareOffers(string FaredetailId, string From, string To, string Airline_Code, string Class, string ClassType, string BaseFare, string Tax,
        string Total, string FilledBy, string FillDate, string Travel_DateStart, string Travel_DateEnd, string ExpOffers_Date, string Counter)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[16];

            if (!String.IsNullOrEmpty(FaredetailId))
            {
                param[0] = new SqlParameter("@ParamFaredetailId", SqlDbType.Int);
                param[0].Value = Convert.ToInt32(FaredetailId);
            }
            if (!String.IsNullOrEmpty(From))
            {
                param[1] = new SqlParameter("@ParamFrom", SqlDbType.VarChar, 50);
                param[1].Value = From;
            }

            if (!String.IsNullOrEmpty(To))
            {
                param[2] = new SqlParameter("@ParamTo", SqlDbType.VarChar, 50);
                param[2].Value = To;
            }
            if (!String.IsNullOrEmpty(Airline_Code))
            {
                param[3] = new SqlParameter("@ParamAirline_Code", SqlDbType.VarChar, 50);
                param[3].Value = Airline_Code;
            }
            if (!String.IsNullOrEmpty(Class))
            {
                param[4] = new SqlParameter("@ParamClass", SqlDbType.VarChar, 50);
                param[4].Value = Class;
            }
            if (!String.IsNullOrEmpty(ClassType))
            {
                param[5] = new SqlParameter("@ParamClassType", SqlDbType.VarChar, 50);
                param[5].Value = ClassType;
            }
            if (!String.IsNullOrEmpty(BaseFare))
            {
                param[6] = new SqlParameter("@ParamBaseFare", SqlDbType.Money);
                param[6].Value = Convert.ToDouble(BaseFare);
            }
            if (!String.IsNullOrEmpty(Tax))
            {
                param[7] = new SqlParameter("@ParamTax", SqlDbType.Money);
                param[7].Value = Convert.ToDouble(Tax);
            }
            if (!String.IsNullOrEmpty(Total))
            {
                param[8] = new SqlParameter("@ParamTotal", SqlDbType.Money);
                param[8].Value = Convert.ToDouble(Total);
            }
            if (!String.IsNullOrEmpty(FilledBy))
            {
                param[9] = new SqlParameter("@ParamFilledBy", SqlDbType.VarChar, 50);
                param[9].Value = FilledBy;
            }
            if (!String.IsNullOrEmpty(FillDate))
            {
                param[10] = new SqlParameter("@ParamFillDate", SqlDbType.DateTime);
                param[10].Value = Convert.ToDateTime(FillDate);
            }
            if (!String.IsNullOrEmpty(Travel_DateStart))
            {
                param[11] = new SqlParameter("@ParamTravel_DateStart", SqlDbType.DateTime);
                param[11].Value = Convert.ToDateTime(Travel_DateStart);
            }

            if (!String.IsNullOrEmpty(Travel_DateEnd))
            {
                param[12] = new SqlParameter("@ParamTravel_DateEnd", SqlDbType.DateTime);
                param[12].Value = Convert.ToDateTime(Travel_DateEnd);
            }


            if (!String.IsNullOrEmpty(ExpOffers_Date))
            {
                param[13] = new SqlParameter("@ParamExpOffers_Date", SqlDbType.DateTime);
                param[13].Value = Convert.ToDateTime(ExpOffers_Date);
            }
            param[14] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 500);
            param[14].Direction = ParameterDirection.Output;

            param[15] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
            param[15].Value = Counter;
            using (SqlConnection Connection = DataConnection.GetConnection())
            {
                int i = SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "GET_SET_FR_Dmp_AirFareOffers", param);
                return i > 0 ? true : false;

            }
        }
        catch { return false; }
    }

    public DataSet GetHomeAirlineFares(string FaredetailId, string DestinationFrom, string DestinationTo, string DestinationToName,
        string Class, string PageType, string Company, string ContinentCode, string CityName, string TravelDateFrom,
        string TravelDateTo, string AirV, string Counter)
    {
        DataSet dsDest;
        SqlParameter[] param = new SqlParameter[13];
        try
        {
            using (SqlConnection connection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(FaredetailId))
                {
                    param[0] = new SqlParameter("@paramFaredetailId", SqlDbType.VarChar);
                    param[0].Value = Convert.ToInt32(FaredetailId);
                }
                if (!string.IsNullOrEmpty(DestinationFrom))
                {
                    param[1] = new SqlParameter("@paramDestinationFrom", SqlDbType.VarChar);
                    param[1].Value = DestinationFrom;
                }
                if (!string.IsNullOrEmpty(DestinationTo))
                {
                    param[2] = new SqlParameter("@paramDestinationTo", SqlDbType.VarChar);
                    param[2].Value = DestinationTo;
                }
                if (!string.IsNullOrEmpty(DestinationToName))
                {
                    param[3] = new SqlParameter("@paramDestinationToName", SqlDbType.VarChar);
                    param[3].Value = DestinationToName;
                }
                if (!string.IsNullOrEmpty(Class))
                {
                    param[4] = new SqlParameter("@paramClass", SqlDbType.VarChar);
                    param[4].Value = Class;
                }
                if (!string.IsNullOrEmpty(PageType))
                {
                    param[5] = new SqlParameter("@paramPageType", SqlDbType.VarChar);
                    param[5].Value = PageType;
                }
                if (!string.IsNullOrEmpty(Company))
                {
                    param[6] = new SqlParameter("@paramCompany", SqlDbType.VarChar);
                    param[6].Value = Company;
                }
                if (!string.IsNullOrEmpty(ContinentCode))
                {
                    param[7] = new SqlParameter("@paramContinentCode", SqlDbType.VarChar);
                    param[7].Value = ContinentCode;
                }
                if (!string.IsNullOrEmpty(CityName))
                {
                    param[8] = new SqlParameter("@paramCityName", SqlDbType.VarChar);
                    param[8].Value = CityName;
                }
                if (!string.IsNullOrEmpty(TravelDateFrom))
                {
                    param[9] = new SqlParameter("@paramTravelDateFrom", SqlDbType.DateTime);
                    param[9].Value = Convert.ToDateTime(TravelDateFrom);
                }
                if (!string.IsNullOrEmpty(TravelDateTo))
                {
                    param[10] = new SqlParameter("@paramTravelDateTo", SqlDbType.DateTime);
                    param[10].Value = Convert.ToDateTime(TravelDateTo);
                }
                if (!string.IsNullOrEmpty(AirV))
                {
                    param[11] = new SqlParameter("@paramAirV", SqlDbType.VarChar, 50);
                    param[11].Value = AirV;
                }
                param[12] = new SqlParameter("@Counter", SqlDbType.VarChar);
                param[12].Value = Counter;

                dsDest = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetFlightFareHome", param);
                return dsDest;
            }
        }
        catch
        {
            dsDest = null;
            return dsDest;
        }
    }


    public string SET_PageDestination(string SrNo, string Destination, string Airport, string ClassType,
        string Page, string Company, string ModifyBy, string Counter)
    {
        SqlParameter[] param = new SqlParameter[9];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(SrNo))
                {
                    param[0] = new SqlParameter("@ParamSrNo", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(SrNo);
                }
                if (!string.IsNullOrEmpty(Destination))
                {
                    param[1] = new SqlParameter("@ParamDestination", SqlDbType.VarChar, 50);
                    param[1].Value = Destination.ToUpper();
                }
                if (!string.IsNullOrEmpty(Airport))
                {
                    param[2] = new SqlParameter("@ParamAirport", SqlDbType.VarChar, 50);
                    param[2].Value = Airport.ToUpper();
                }
                if (!string.IsNullOrEmpty(ClassType))
                {
                    param[3] = new SqlParameter("@ParamClassType", SqlDbType.VarChar, 50);
                    param[3].Value = ClassType.ToUpper();
                }
                if (!string.IsNullOrEmpty(Page))
                {
                    param[4] = new SqlParameter("@ParamPage", SqlDbType.VarChar, 50);
                    param[4].Value = Page;
                }
                if (!string.IsNullOrEmpty(Company))
                {
                    param[5] = new SqlParameter("@ParamCompany", SqlDbType.VarChar, 50);
                    param[5].Value = Company;
                }

                if (!string.IsNullOrEmpty(ModifyBy))
                {
                    param[6] = new SqlParameter("@ParamModifyBy", SqlDbType.VarChar, 50);
                    param[6].Value = ModifyBy;
                }
                param[7] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[7].Value = Counter;

                param[8] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 500);
                param[8].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_PageDestination", param);
                return param[8].Value.ToString();
            }
        }
        catch
        {
            return "false";
        }
    }

    public DataTable GET_PageDestination(string SrNo, string Destination, string Airport, string ClassType,
        string Page, string Company)
    {
        SqlParameter[] param = new SqlParameter[7];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(SrNo))
                {
                    param[0] = new SqlParameter("@ParamSrNo", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(SrNo);
                }
                if (!string.IsNullOrEmpty(Destination))
                {
                    param[1] = new SqlParameter("@ParamDestination", SqlDbType.VarChar, 50);
                    param[1].Value = Destination;
                }
                if (!string.IsNullOrEmpty(Airport))
                {
                    param[2] = new SqlParameter("@ParamAirport", SqlDbType.VarChar, 50);
                    param[2].Value = Airport;
                }
                if (!string.IsNullOrEmpty(ClassType))
                {
                    param[3] = new SqlParameter("@ParamClassType", SqlDbType.VarChar, 50);
                    param[3].Value = ClassType;
                }
                if (!string.IsNullOrEmpty(Page))
                {
                    param[4] = new SqlParameter("@ParamPage", SqlDbType.VarChar, 50);
                    param[4].Value = Page;
                }
                if (!string.IsNullOrEmpty(Company))
                {
                    param[5] = new SqlParameter("@ParamCompany", SqlDbType.VarChar, 50);
                    param[5].Value = Company;
                }
                param[6] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[6].Value = "Select";

                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_PageDestination", param);
                return ds.Tables[0];
            }
        }
        catch
        {
            return null;
        }
    }
    #endregion

    #region airline details
    public DataTable GetAirlines()
    {
        try
        {
            using (SqlConnection connection = DataConnection.GetConnectionHotel())
            {
                DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Airline_Detail_Get", null);
                if (ds.Tables[0] != null)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
        }
        catch
        {

            return null;
        }
    }

    #endregion

    #region Prefered Airlines

    public DataTable GetPreferedAirlines(string destination, string airline, string company)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[3];
            using (SqlConnection connection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(destination))
                {
                    param[0] = new SqlParameter("@ParamDestination", SqlDbType.VarChar);
                    param[0].Value = destination;
                }
                if (!string.IsNullOrEmpty(airline))
                {
                    param[1] = new SqlParameter("@ParamAirline_Code", SqlDbType.VarChar);
                    param[1].Value = airline;
                }
                if (!string.IsNullOrEmpty(company))
                {
                    param[2] = new SqlParameter("@ParamComp_Code", SqlDbType.VarChar);
                    param[2].Value = company;
                }

                DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "GET_SET_RestrictedAirlines", param);
                if (ds.Tables[0] != null)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
        }
        catch
        {

            return null;
        }
    }


    public bool AddPreferedAirlines(string airlineCode, string companyCode, string ModifiedBy)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[3];
            using (SqlConnection connection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(airlineCode))
                {
                    param[0] = new SqlParameter("@ParamAirline_Code", SqlDbType.VarChar, 3);
                    param[0].Value = airlineCode.ToUpper();
                }

                param[1] = new SqlParameter("@ParamComp_Code", SqlDbType.VarChar, 50);
                param[1].Value = string.IsNullOrEmpty(companyCode) ? "ANY" : companyCode.ToUpper();

                param[2] = new SqlParameter("@ParamModifiedBy", SqlDbType.VarChar, 50);
                param[2].Value = ModifiedBy;
                param[3] = new SqlParameter("@Counter", SqlDbType.VarChar, 50);
                param[3].Value = "Insert";

                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "GET_SET_RestrictedAirlines", param);
                return true;
            }
        }
        catch
        {

            return false;
        }
    }

    public bool AddPreferedAirlines(DataTable preferedAirlines)
    {
        try
        {
            foreach (DataRow dr in preferedAirlines.Rows)
            {
                try
                {
                    SqlParameter[] param = new SqlParameter[7];
                    using (SqlConnection connection = DataConnection.GetConnection())
                    {
                        if (!string.IsNullOrEmpty(dr["AL_Airline_Code"].ToString()))
                        {
                            param[0] = new SqlParameter("@ParamAirline_Code", SqlDbType.VarChar, 3);
                            param[0].Value = dr["AL_Airline_Code"].ToString();
                        }

                        param[1] = new SqlParameter("@ParamComp_Code", SqlDbType.VarChar, 50);
                        param[1].Value = dr["AL_Comp_Code"].ToString();

                        param[2] = new SqlParameter("@ParamDestination", SqlDbType.VarChar, 3);
                        param[2].Value = dr["AL_Destination"];

                        param[3] = new SqlParameter("@ParamRES_Type", SqlDbType.VarChar, 50);
                        param[3].Value = dr["AL_RES_Type"];

                        param[4] = new SqlParameter("@ParamModifiedBy", SqlDbType.VarChar, 200);
                        param[4].Value = dr["AL_ModifiedBy"];


                        param[5] = new SqlParameter("@ParamCompaign_Code", SqlDbType.VarChar, 50);
                        param[5].Value = dr["AL_Compaign_Code"];

                        param[6] = new SqlParameter("@Counter", SqlDbType.VarChar, 50);
                        param[6].Value = "Insert";

                        SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "GET_SET_RestrictedAirlines", param);

                    }
                }
                catch { }

            }
            return true;
        }
        catch
        {

            return false;
        }
    }

    public string DeletePreferedAirlines(string ID, string Counter)
    {
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(ID))
                {
                    param[0] = new SqlParameter("@ParamSNo", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(ID);
                }

                param[1] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[1].Value = "Delete";

                param[2] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 500);
                param[2].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_RestrictedAirlines", param);
                return param[2].Value.ToString();
            }
        }
        catch
        {
            return "false";
        }
    }

    #endregion

  

    #region Page Tracker
    public DataTable SearchPageTrackerOnline(string _Origin, string _Destination, string _FromDepartDate, string _ToDepartDate,
   string _fromReturnDate, string _toReturnDate, string _company, string _fromHitDate, string _toHitDate,
   string _Site, string _IPAddress, string _Page, string _Page_Url, string _IPCountry, string _IPCiry,
   string _Browser, string _SessionID, string redirectFrom, string _remarks, string SourceMediaList, string Counter)
    {

        try
        {

            SqlParameter[] param = new SqlParameter[20];

            if (!string.IsNullOrEmpty(_Origin))
            {
                param[0] = new SqlParameter("@paramOrigin", SqlDbType.NVarChar, 50);
                param[0].Value = _Origin;
            }
            if (!string.IsNullOrEmpty(_Destination))
            {
                param[1] = new SqlParameter("@paramDestination", SqlDbType.NVarChar, 50);
                param[1].Value = _Destination;
            }

            if (!string.IsNullOrEmpty(_FromDepartDate))
            {
                param[2] = new SqlParameter("@paramFromDepartDate", SqlDbType.DateTime);
                param[2].Value = Convert.ToDateTime(_FromDepartDate);
            }
            if (!string.IsNullOrEmpty(_ToDepartDate))
            {
                param[3] = new SqlParameter("@paramToDepartDate", SqlDbType.DateTime);
                param[3].Value = Convert.ToDateTime(_ToDepartDate);
            }
            if (!string.IsNullOrEmpty(_fromReturnDate))
            {
                param[4] = new SqlParameter("@paramFromReturnDate", SqlDbType.DateTime);
                param[4].Value = Convert.ToDateTime(_fromReturnDate);
            }

            if (!string.IsNullOrEmpty(_toReturnDate))
            {
                param[5] = new SqlParameter("@paramToReturnDate", SqlDbType.DateTime);
                param[5].Value = Convert.ToDateTime(_toReturnDate);
            }
            if (!string.IsNullOrEmpty(_company))
            {
                param[6] = new SqlParameter("@paramCompany", SqlDbType.NVarChar);
                param[6].Value = _company;
            }
            if (!string.IsNullOrEmpty(_fromHitDate))
            {
                param[7] = new SqlParameter("@paramFromDatenTime", SqlDbType.DateTime);
                param[7].Value = Convert.ToDateTime(_fromHitDate);
            }

            if (!string.IsNullOrEmpty(_toHitDate))
            {
                param[8] = new SqlParameter("@paramToDatenTime", SqlDbType.DateTime);
                param[8].Value = Convert.ToDateTime(_toHitDate);
            }
            if (!string.IsNullOrEmpty(_Site))
            {
                param[9] = new SqlParameter("@paramSite", SqlDbType.NVarChar, 100);
                param[9].Value = _Site;
            }
            if (!string.IsNullOrEmpty(_IPAddress))
            {
                param[10] = new SqlParameter("@paramIP", SqlDbType.NVarChar, 50);
                param[10].Value = _IPAddress;
            }

            if (!string.IsNullOrEmpty(_Page))
            {
                param[11] = new SqlParameter("@paramPage", SqlDbType.NVarChar, 50);
                param[11].Value = _Page;
            }
            if (!string.IsNullOrEmpty(_Page_Url))
            {
                param[12] = new SqlParameter("@paramPageUrl", SqlDbType.NVarChar, 500);
                param[12].Value = _Page_Url;
            }
            if (!string.IsNullOrEmpty(_IPCountry))
            {
                param[13] = new SqlParameter("@paramIPCountry", SqlDbType.NVarChar, 50);
                param[13].Value = _IPCountry;
            }

            if (!string.IsNullOrEmpty(_IPCiry))
            {
                param[14] = new SqlParameter("@paramIPCity", SqlDbType.NVarChar, 50);
                param[14].Value = _IPCiry;
            }
            if (!string.IsNullOrEmpty(_Browser))
            {
                param[15] = new SqlParameter("@paramBrowser", SqlDbType.NVarChar, 50);
                param[15].Value = _Browser;
            }
            if (!string.IsNullOrEmpty(_SessionID))
            {
                param[16] = new SqlParameter("@paramSessionID", SqlDbType.NVarChar, 200);
                param[16].Value = _SessionID;
            }
            if (!string.IsNullOrEmpty(redirectFrom))
            {
                param[16] = new SqlParameter("@paramRedirectFrom", SqlDbType.NVarChar, 100);
                param[16].Value = redirectFrom;
            }
            if (!string.IsNullOrEmpty(_remarks))
            {
                param[17] = new SqlParameter("@paramRemarks", SqlDbType.NVarChar, 500);
                param[17].Value = _remarks;
            }
            if (!string.IsNullOrEmpty(SourceMediaList))
            {
                param[18] = new SqlParameter("@ParamSourceMediaList", SqlDbType.VarChar, 4000);
                param[18].Value = SourceMediaList;
            }
            if (!string.IsNullOrEmpty(Counter))
            {
                if (_company == "FLTTROTT" || _company == "TRAVELOFLIUK" || _company == "TRVJUNCTION_USA" || _company == "C2BUS")
                {
                    param[19] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                    param[19].Value = "Select_FT";
                }
                else
                {
                    param[19] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                    param[19].Value = Counter;
                }
            }
            using (SqlConnection connection = DataConnection.GetConnection())
            {
                DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ST_PageTracker_Get", param);
                return ds.Tables[0] != null ? ds.Tables[0] : null;
            }
        }
        catch
        {
            return null;
        }
    }
    #endregion

    #region Call Details

    public bool SET_Sales_CallDetails(string SrNo, string CallRefrence, string CallEntryType, string CallType, string CompanyID, string SourceOfcall,
        string PaxName, string PhoneNo, string MobileNo, string PaxEmailID, string NoOfAdult, string NoOfChild, string NoOfInfant, string Origin, string Destination,
        string DepartDate, string ReturnDate, string PNR, string BookingRef, string ProdID, string PenguinFolderNo, string PaxBudget, string ReasonTransfer,
        string TransferTo, string Remarks, string CallEnterBy, string CallModifyBy, string Counter)
    {
        try
        {

            SqlParameter[] param = new SqlParameter[28];

            if (!string.IsNullOrEmpty(SrNo))
            {
                param[0] = new SqlParameter("@ParamSrNo", SqlDbType.Int);
                param[0].Value = Convert.ToInt32(SrNo);
            }
            if (!string.IsNullOrEmpty(CallRefrence))
            {
                param[1] = new SqlParameter("@ParamCallRefrence", SqlDbType.VarChar, (50));
                param[1].Value = CallRefrence;
            }
            if (!string.IsNullOrEmpty(CallEntryType))
            {
                param[2] = new SqlParameter("@ParamCallEntryType", SqlDbType.VarChar, (50));
                param[2].Value = CallEntryType;
            }
            if (!string.IsNullOrEmpty(CallType))
            {
                param[3] = new SqlParameter("@ParamCallType", SqlDbType.VarChar, (100));
                param[3].Value = CallType;
            }
            if (!string.IsNullOrEmpty(CompanyID))
            {
                param[4] = new SqlParameter("@ParamCompanyID", SqlDbType.VarChar, (50));
                param[4].Value = CompanyID;
            }
            if (!string.IsNullOrEmpty(SourceOfcall))
            {
                param[5] = new SqlParameter("@ParamSourceOfcall", SqlDbType.VarChar, (50));
                param[5].Value = SourceOfcall;
            }
            if (!string.IsNullOrEmpty(PaxName))
            {
                param[6] = new SqlParameter("@ParamPaxName", SqlDbType.VarChar, (100));
                param[6].Value = PaxName;
            }
            if (!string.IsNullOrEmpty(PhoneNo))
            {
                param[7] = new SqlParameter("@ParamPhoneNo", SqlDbType.VarChar, (100));
                param[7].Value = PhoneNo;
            }
            if (!string.IsNullOrEmpty(MobileNo))
            {
                param[8] = new SqlParameter("@ParamMobileNo", SqlDbType.VarChar, (100));
                param[8].Value = MobileNo;
            }
            if (!string.IsNullOrEmpty(PaxEmailID))
            {
                param[9] = new SqlParameter("@ParamPaxEmailID", SqlDbType.VarChar, (100));
                param[9].Value = PaxEmailID;
            }
            if (!string.IsNullOrEmpty(NoOfAdult))
            {
                param[10] = new SqlParameter("@ParamNoOfAdult", SqlDbType.Int);
                param[10].Value = Convert.ToInt32(NoOfAdult);
            }
            if (!string.IsNullOrEmpty(NoOfChild))
            {
                param[11] = new SqlParameter("@ParamNoOfChild", SqlDbType.Int);
                param[11].Value = Convert.ToInt32(NoOfChild);
            }
            if (!string.IsNullOrEmpty(NoOfInfant))
            {
                param[12] = new SqlParameter("@ParamNoOfInfant", SqlDbType.Int);
                param[12].Value = Convert.ToInt32(NoOfInfant);
            }
            if (!string.IsNullOrEmpty(Origin))
            {
                param[13] = new SqlParameter("@ParamOrigin", SqlDbType.VarChar, (100));
                param[13].Value = Origin;
            }
            if (!string.IsNullOrEmpty(Destination))
            {
                param[14] = new SqlParameter("@ParamDestination", SqlDbType.VarChar, (100));
                param[14].Value = Destination;
            }
            if (!string.IsNullOrEmpty(DepartDate))
            {
                param[15] = new SqlParameter("@ParamDepartDate", SqlDbType.DateTime);
                param[15].Value = Convert.ToDateTime(DepartDate);
            }
            if (!string.IsNullOrEmpty(ReturnDate))
            {
                param[16] = new SqlParameter("@ParamReturnDate", SqlDbType.DateTime);
                param[16].Value = Convert.ToDateTime(ReturnDate);
            }
            if (!string.IsNullOrEmpty(PNR))
            {
                param[17] = new SqlParameter("@ParamPNR", SqlDbType.VarChar, (100));
                param[17].Value = PNR;
            }
            if (!string.IsNullOrEmpty(BookingRef))
            {
                param[18] = new SqlParameter("@ParamBookingRef", SqlDbType.VarChar, (50));
                param[18].Value = BookingRef;
            }
            if (!string.IsNullOrEmpty(ProdID))
            {
                param[19] = new SqlParameter("@ParamProdID", SqlDbType.VarChar, (50));
                param[19].Value = ProdID;
            }
            if (!string.IsNullOrEmpty(PenguinFolderNo))
            {
                param[20] = new SqlParameter("@ParamPenguinFolderNo", SqlDbType.VarChar, (200));
                param[20].Value = PenguinFolderNo;
            }
            if (!string.IsNullOrEmpty(PaxBudget))
            {
                param[21] = new SqlParameter("@ParamPaxBudget", SqlDbType.Money);
                param[21].Value = Convert.ToDouble(PaxBudget);
            }
            if (!string.IsNullOrEmpty(ReasonTransfer))
            {
                param[22] = new SqlParameter("@ParamReasonTransfer", SqlDbType.VarChar, (500));
                param[22].Value = ReasonTransfer;
            }
            if (!string.IsNullOrEmpty(TransferTo))
            {
                param[23] = new SqlParameter("@ParamTransferTo", SqlDbType.VarChar, (100));
                param[23].Value = TransferTo;
            }
            if (!string.IsNullOrEmpty(Remarks))
            {
                param[24] = new SqlParameter("@ParamRemarks", SqlDbType.VarChar, (8000));
                param[24].Value = Remarks;
            }
            if (!string.IsNullOrEmpty(CallEnterBy))
            {
                param[25] = new SqlParameter("@ParamCallEnterBy", SqlDbType.VarChar, (100));
                param[25].Value = CallEnterBy;
            }
            if (!string.IsNullOrEmpty(CallModifyBy))
            {
                param[26] = new SqlParameter("@ParamCallModifyBy", SqlDbType.VarChar, (50));
                param[26].Value = CallModifyBy;
            }
            if (!string.IsNullOrEmpty(Counter))
            {
                param[27] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[27].Value = Counter;
            }
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                int i = SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_Sales_CallDetails", param);
                return i > 0 ? true : false;
            }
        }
        catch
        {
            return false;
        }
    }

    public DataTable GET_Sales_CallDetails(string SrNo, string CallRefrence, string CallEntryType, string CallType, string CompanyID, string SourceOfcall,
        string PaxName, string PhoneNo, string MobileNo, string PaxEmailID, string NoOfAdult, string NoOfChild, string NoOfInfant, string Origin, string Destination,
        string PenguinFolderNo, string PaxBudget, string CallEnterBy, string CallDateFrom, string CallDateTo, string Counter)
    {
        try
        {

            SqlParameter[] param = new SqlParameter[21];

            if (!string.IsNullOrEmpty(SrNo))
            {
                param[0] = new SqlParameter("@ParamSrNo", SqlDbType.Int);
                param[0].Value = Convert.ToInt32(SrNo);
            }
            if (!string.IsNullOrEmpty(CallRefrence))
            {
                param[1] = new SqlParameter("@ParamCallRefrence", SqlDbType.VarChar, (50));
                param[1].Value = CallRefrence;
            }
            if (!string.IsNullOrEmpty(CallEntryType))
            {
                param[2] = new SqlParameter("@ParamCallEntryType", SqlDbType.VarChar, (50));
                param[2].Value = CallEntryType;
            }
            if (!string.IsNullOrEmpty(CallType))
            {
                param[3] = new SqlParameter("@ParamCallType", SqlDbType.VarChar, (100));
                param[3].Value = CallType;
            }
            if (!string.IsNullOrEmpty(CompanyID))
            {
                param[4] = new SqlParameter("@ParamCompanyID", SqlDbType.VarChar, (50));
                param[4].Value = CompanyID;
            }
            if (!string.IsNullOrEmpty(SourceOfcall))
            {
                param[5] = new SqlParameter("@ParamSourceOfcall", SqlDbType.VarChar, (50));
                param[5].Value = SourceOfcall;
            }
            if (!string.IsNullOrEmpty(PaxName))
            {
                param[6] = new SqlParameter("@ParamPaxName", SqlDbType.VarChar, (100));
                param[6].Value = PaxName;
            }
            if (!string.IsNullOrEmpty(PhoneNo))
            {
                param[7] = new SqlParameter("@ParamPhoneNo", SqlDbType.VarChar, (100));
                param[7].Value = PhoneNo;
            }
            if (!string.IsNullOrEmpty(MobileNo))
            {
                param[8] = new SqlParameter("@ParamMobileNo", SqlDbType.VarChar, (100));
                param[8].Value = MobileNo;
            }
            if (!string.IsNullOrEmpty(PaxEmailID))
            {
                param[9] = new SqlParameter("@ParamPaxEmailID", SqlDbType.VarChar, (100));
                param[9].Value = PaxEmailID;
            }
            if (!string.IsNullOrEmpty(NoOfAdult))
            {
                param[10] = new SqlParameter("@ParamNoOfAdult", SqlDbType.Int);
                param[10].Value = Convert.ToInt32(NoOfAdult);
            }
            if (!string.IsNullOrEmpty(NoOfChild))
            {
                param[11] = new SqlParameter("@ParamNoOfChild", SqlDbType.Int);
                param[11].Value = Convert.ToInt32(NoOfChild);
            }
            if (!string.IsNullOrEmpty(NoOfInfant))
            {
                param[12] = new SqlParameter("@ParamNoOfInfant", SqlDbType.Int);
                param[12].Value = Convert.ToInt32(NoOfInfant);
            }
            if (!string.IsNullOrEmpty(Origin))
            {
                param[13] = new SqlParameter("@ParamOrigin", SqlDbType.VarChar, (100));
                param[13].Value = Origin;
            }
            if (!string.IsNullOrEmpty(Destination))
            {
                param[14] = new SqlParameter("@ParamDestination", SqlDbType.VarChar, (100));
                param[14].Value = Destination;
            }
            if (!string.IsNullOrEmpty(PenguinFolderNo))
            {
                param[15] = new SqlParameter("@ParamPenguinFolderNo", SqlDbType.VarChar, (200));
                param[15].Value = PenguinFolderNo;
            }
            if (!string.IsNullOrEmpty(PaxBudget))
            {
                param[16] = new SqlParameter("@ParamPaxBudget", SqlDbType.Money);
                param[16].Value = Convert.ToDouble(PaxBudget);
            }
            if (!string.IsNullOrEmpty(CallEnterBy))
            {
                param[17] = new SqlParameter("@ParamCallEnterBy", SqlDbType.VarChar, (100));
                param[17].Value = CallEnterBy;
            }
            if (!string.IsNullOrEmpty(CallDateFrom))
            {
                param[18] = new SqlParameter("@ParamCallDateFrom", SqlDbType.DateTime);
                param[18].Value = Convert.ToDateTime(CallDateFrom);
            }
            if (!string.IsNullOrEmpty(CallDateTo))
            {
                param[19] = new SqlParameter("@ParamCallDateTo", SqlDbType.DateTime);
                param[19].Value = Convert.ToDateTime(CallDateTo);
            }
            if (!string.IsNullOrEmpty(Counter))
            {
                param[20] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[20].Value = Counter;
            }
            using (SqlConnection connection = DataConnection.GetConnection())
            {
                DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "GET_SET_Sales_CallDetails", param);
                return ds.Tables[0] != null ? ds.Tables[0] : null;
            }
        }
        catch
        {
            return null;
        }
    }

    #endregion

    #region Change Fare UK

    public string SET_ChangeFare_UK(string SrNo, string Origin, string Destination, string FromDateStart, string FromDateEnd, string Airline, string CabinClass,
        string aClass, string BaseFare, string Tax, string Markup, string MarkupTJ, string MarkupXpt, string Commission,
        string ModifyBy, string Counter, string Campaign, string offerType, string journeyType, string active, string FlightType, string modifiedDate)
    {
        string[] classes = aClass.Split(',');
        foreach (string cls in classes)
        {
            SqlParameter[] param = new SqlParameter[20];
            try
            {
                using (SqlConnection conection = DataConnection.GetConnection())
                {

                    if (!string.IsNullOrEmpty(SrNo))
                    {
                        param[0] = new SqlParameter("@ParamSrNo", SqlDbType.Int);
                        param[0].Value = Convert.ToInt32(SrNo);
                    }
                    if (!string.IsNullOrEmpty(Origin))
                    {
                        param[1] = new SqlParameter("@ParamOrigin", SqlDbType.VarChar, 50);
                        param[1].Value = Origin;
                    }
                    if (!string.IsNullOrEmpty(Destination))
                    {
                        param[2] = new SqlParameter("@ParamDestination", SqlDbType.VarChar, 50);
                        param[2].Value = Destination;
                    }
                    if (!string.IsNullOrEmpty(FromDateStart))
                    {
                        param[3] = new SqlParameter("@ParamFromDateStart", SqlDbType.DateTime);
                        param[3].Value = Convert.ToDateTime(FromDateStart);
                    }
                    if (!string.IsNullOrEmpty(FromDateEnd))
                    {
                        param[4] = new SqlParameter("@ParamFromDateEnd", SqlDbType.DateTime);
                        param[4].Value = Convert.ToDateTime(FromDateEnd);
                    }
                    if (!string.IsNullOrEmpty(Airline))
                    {
                        param[5] = new SqlParameter("@ParamAirline", SqlDbType.VarChar, 50);
                        param[5].Value = Airline;
                    }
                    if (!string.IsNullOrEmpty(CabinClass))
                    {
                        param[6] = new SqlParameter("@ParamCabinClass", SqlDbType.VarChar, 50);
                        param[6].Value = CabinClass;
                    }
                    if (!string.IsNullOrEmpty(cls))
                    {
                        param[7] = new SqlParameter("@ParamClass", SqlDbType.VarChar, 50);
                        param[7].Value = cls;
                    }
                    if (!string.IsNullOrEmpty(BaseFare))
                    {
                        param[8] = new SqlParameter("@ParamBaseFare", SqlDbType.Money);
                        param[8].Value = Convert.ToDouble(BaseFare);
                    }
                    if (!string.IsNullOrEmpty(Tax))
                    {
                        param[9] = new SqlParameter("@ParamTax", SqlDbType.Money);
                        param[9].Value = Convert.ToDouble(Tax);
                    }
                    if (!string.IsNullOrEmpty(Markup))
                    {
                        param[10] = new SqlParameter("@ParamMarkup", SqlDbType.Money);
                        param[10].Value = Convert.ToDouble(Markup);
                    }

                    if (!string.IsNullOrEmpty(Commission))
                    {
                        param[11] = new SqlParameter("@ParamCommission", SqlDbType.Money);
                        param[11].Value = Convert.ToDouble(Commission);
                    }
                    if (!string.IsNullOrEmpty(ModifyBy))
                    {
                        param[12] = new SqlParameter("@ParamModifyBy", SqlDbType.VarChar, 50);
                        param[12].Value = ModifyBy;
                    }
                    param[13] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                    param[13].Value = Counter;

                    param[14] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 500);
                    param[14].Direction = ParameterDirection.Output;

                    if (!string.IsNullOrEmpty(Campaign))
                    {
                        param[15] = new SqlParameter("@ParamCampaign", SqlDbType.VarChar, 100);
                        param[15].Value = Campaign;
                    }
                    if (!string.IsNullOrEmpty(offerType))
                    {
                        param[16] = new SqlParameter("@ParamOfferType", SqlDbType.VarChar, 50);
                        param[16].Value = offerType;
                    }
                    if (!string.IsNullOrEmpty(journeyType))
                    {
                        param[17] = new SqlParameter("@ParamTripType", SqlDbType.VarChar, 10);
                        param[17].Value = journeyType;
                    }
                    if (active.ToLower() == "true" || active.ToLower() == "false")
                    {

                        if (active.ToLower() == "true")
                        {
                            param[18] = new SqlParameter("@ParamActive", SqlDbType.Bit);
                            param[18].Value = 1;
                        }
                        else
                        {
                            param[18] = new SqlParameter("@ParamActive", SqlDbType.Bit);
                            param[18].Value = 0;
                        }
                    }

                    if (!string.IsNullOrEmpty(FlightType))
                    {
                        param[19] = new SqlParameter("@ParamFlightType", SqlDbType.VarChar, 50);
                        param[19].Value = FlightType;
                    }

                    if (!string.IsNullOrEmpty(modifiedDate))
                    {
                        param[20] = new SqlParameter("@ParamModifyDate", SqlDbType.DateTime);
                        param[20].Value = Convert.ToDateTime(modifiedDate);
                    }

                    SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_ChangeFare_UK", param);
                    string output = param[14].Value.ToString();
                    if (output != "true")
                        return output;
                }

            }
            catch (Exception ex)
            {
                return "false";
            }
        }
        return "true";
    }

    public DataTable GET_ChangeFare_UK(string SrNo, string Origin, string Destination, string FromDateStart, string FromDateEnd, string Airline, string CabinClass,
        string aClass, string TravelDate, string Counter, string ModifyBy, string ModifyDate, string Campaign, string journeyType, bool Active, string flightType)
    {
        SqlParameter[] param = new SqlParameter[18];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(SrNo))
                {
                    param[0] = new SqlParameter("@ParamSrNo", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(SrNo);
                }
                if (!string.IsNullOrEmpty(Origin))
                {
                    param[1] = new SqlParameter("@ParamOrigin", SqlDbType.VarChar, 50);
                    param[1].Value = Origin;
                }
                if (!string.IsNullOrEmpty(Destination))
                {
                    param[2] = new SqlParameter("@ParamDestination", SqlDbType.VarChar, 50);
                    param[2].Value = Destination;
                }
                if (!string.IsNullOrEmpty(FromDateStart))
                {
                    param[3] = new SqlParameter("@ParamFromDateStart", SqlDbType.DateTime);
                    param[3].Value = Convert.ToDateTime(FromDateStart);
                }
                if (!string.IsNullOrEmpty(FromDateEnd))
                {
                    param[4] = new SqlParameter("@ParamFromDateEnd", SqlDbType.DateTime);
                    param[4].Value = Convert.ToDateTime(FromDateEnd);
                }
                if (!string.IsNullOrEmpty(Airline))
                {
                    param[5] = new SqlParameter("@ParamAirline", SqlDbType.VarChar, 50);
                    param[5].Value = Airline;
                }
                if (!string.IsNullOrEmpty(CabinClass))
                {
                    param[6] = new SqlParameter("@ParamCabinClass", SqlDbType.VarChar, 50);
                    param[6].Value = CabinClass;
                }
                if (!string.IsNullOrEmpty(aClass))
                {
                    param[7] = new SqlParameter("@ParamClass", SqlDbType.VarChar, 50);
                    param[7].Value = aClass;
                }
                if (!string.IsNullOrEmpty(TravelDate))
                {
                    param[8] = new SqlParameter("@ParamTravelDate", SqlDbType.DateTime);
                    param[8].Value = Convert.ToDateTime(TravelDate);
                }
                param[9] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[9].Value = Counter;

                if (!string.IsNullOrEmpty(ModifyBy))
                {
                    param[10] = new SqlParameter("@ParamModifyBy", SqlDbType.VarChar, 50);
                    param[10].Value = ModifyBy;
                }
                if (!string.IsNullOrEmpty(ModifyDate))
                {
                    param[11] = new SqlParameter("@ParamModifyDate", SqlDbType.DateTime);
                    param[11].Value = Convert.ToDateTime(ModifyDate);
                }
                if (!string.IsNullOrEmpty(Campaign))
                {
                    param[12] = new SqlParameter("@ParamCampaign", SqlDbType.VarChar, 100);
                    param[12].Value = Campaign;
                }
                if (!string.IsNullOrEmpty(journeyType))
                {
                    param[13] = new SqlParameter("@ParamTripType", SqlDbType.VarChar, 10);
                    param[13].Value = journeyType;
                }
                if (Active)
                {
                    param[14] = new SqlParameter("@ParamActive", SqlDbType.Bit);
                    param[14].Value = 1;
                }
                else
                {
                    param[14] = new SqlParameter("@ParamActive", SqlDbType.Bit);
                    param[14].Value = 0;
                }

                if (!string.IsNullOrEmpty(flightType))
                {
                    param[15] = new SqlParameter("@ParamFlightType", SqlDbType.VarChar, 100);
                    param[15].Value = flightType;
                }


                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_ChangeFare_UK", param);
                return ds.Tables[0];
            }
        }
        catch (Exception e)
        {
            return null;
        }
    }


    public string DeleteOwnFares_UK(string ID, string Counter)
    {
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(ID))
                {
                    param[0] = new SqlParameter("@ParamSrNo", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(ID);
                }

                param[1] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[1].Value = Counter;

                param[2] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 500);
                param[2].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_ChangeFare_UK", param);
                return param[2].Value.ToString();
            }
        }
        catch (Exception ex)
        {
            return "false";
        }
    }


    public string UpdateOwnFareUK_Excel(string ID, string UpdateField, string Value, string UpdetedBy)
    {
        try
        {
            string FieldName = "";
            string result = string.Empty;
            switch (UpdateField)
            {
                case "BaseFare": FieldName = "Change_Fare_BaseFare"; break;
                case "Tax": FieldName = "Change_Fare_Tax"; break;
                case "Markup": FieldName = "Change_Fare_Markup"; break;
                case "Commission": FieldName = "Change_Fare_Commission"; break;
                case "FromDateStart": FieldName = "Change_Fare_FromDateStart"; break;
                case "FromDateEnd": FieldName = "Change_Fare_FromDateEnd"; break;
                case "Active": FieldName = "ISACTIVE"; break;
                case "FlightType": FieldName = "Change_Fare_Flight_type"; break;
                case "Airline": FieldName = "Change_Fare_Airline"; break;
                case "Origin": FieldName = "Change_Fare_Origin"; break;
                case "Destination": FieldName = "Change_Fare_Destination"; break;
                case "AirlineClass": FieldName = "Change_Fare_Class"; break;
                case "OfferType": FieldName = "Change_Fare_OfferType"; break;
                case "TripType": FieldName = "Change_Fare_TripType"; break;
            }
            string Query = "";

            if (UpdateField == "FromDateStart" || UpdateField == "FromDateEnd")
            {
                DateTime dt;

                if (DateTime.TryParse(Value, out dt))
                {
                    Query = "UPDATE CHANGEFARE set " + FieldName + "='" + Convert.ToDateTime(Value).ToString("yyyy-MM-dd") + "'," +
                        "Change_Fare_ModifyBy='" + UpdetedBy + "'," +
                        "Change_Fare_ModifyDateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                        "Where Change_Fare_SrNo=" + ID;
                }
                else
                {
                    result = "Invalid date format";
                }
            }
            else
            {
                Query = "UPDATE CHANGEFARE set " + FieldName + "='" + Value.ToUpper() + "'," +
                    "Change_Fare_ModifyBy='" + UpdetedBy + "'," +
                    "Change_Fare_ModifyDateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                    "Where Change_Fare_SrNo=" + ID;
            }

            using (SqlConnection connection = DataConnection.GetConnection())
            {
                if (SqlHelper.ExecuteNonQuery(connection, CommandType.Text, Query) == 1)
                {
                    result = "true";
                }
                else
                {
                    result = "false";
                }
            }
            return result;
        }
        catch (Exception ex)
        {
            return "false";
        }
    }



    public string SET_ChangeFare_UK(string SrNo, string Origin, string Destination, string FromDateStart, string FromDateEnd, string Airline, string CabinClass,
           string aClass, string BaseFare, string Tax, string Markup, string Commission,
           string ModifyBy, string Counter, string Campaign, string offerType, string journeyType, string active, string FlightType, string modifiedDate)
    {
        string[] classes = aClass.Split(',');
        foreach (string cls in classes)
        {
            SqlParameter[] param = new SqlParameter[20];
            try
            {
                using (SqlConnection conection = DataConnection.GetConnection())
                {

                    if (!string.IsNullOrEmpty(SrNo))
                    {
                        param[0] = new SqlParameter("@ParamSrNo", SqlDbType.Int);
                        param[0].Value = Convert.ToInt32(SrNo);
                    }
                    if (!string.IsNullOrEmpty(Origin))
                    {
                        param[1] = new SqlParameter("@ParamOrigin", SqlDbType.VarChar, 50);
                        param[1].Value = Origin;
                    }
                    if (!string.IsNullOrEmpty(Destination))
                    {
                        param[2] = new SqlParameter("@ParamDestination", SqlDbType.VarChar, 50);
                        param[2].Value = Destination;
                    }
                    if (!string.IsNullOrEmpty(FromDateStart))
                    {
                        param[3] = new SqlParameter("@ParamFromDateStart", SqlDbType.DateTime);
                        param[3].Value = Convert.ToDateTime(FromDateStart);
                    }
                    if (!string.IsNullOrEmpty(FromDateEnd))
                    {
                        param[4] = new SqlParameter("@ParamFromDateEnd", SqlDbType.DateTime);
                        param[4].Value = Convert.ToDateTime(FromDateEnd);
                    }
                    if (!string.IsNullOrEmpty(Airline))
                    {
                        param[5] = new SqlParameter("@ParamAirline", SqlDbType.VarChar, 50);
                        param[5].Value = Airline;
                    }
                    if (!string.IsNullOrEmpty(CabinClass))
                    {
                        param[6] = new SqlParameter("@ParamCabinClass", SqlDbType.VarChar, 50);
                        param[6].Value = CabinClass;
                    }
                    if (!string.IsNullOrEmpty(cls))
                    {
                        param[7] = new SqlParameter("@ParamClass", SqlDbType.VarChar, 50);
                        param[7].Value = cls;
                    }
                    if (!string.IsNullOrEmpty(BaseFare))
                    {
                        param[8] = new SqlParameter("@ParamBaseFare", SqlDbType.Money);
                        param[8].Value = Convert.ToDouble(BaseFare);
                    }
                    if (!string.IsNullOrEmpty(Tax))
                    {
                        param[9] = new SqlParameter("@ParamTax", SqlDbType.Money);
                        param[9].Value = Convert.ToDouble(Tax);
                    }
                    if (!string.IsNullOrEmpty(Markup))
                    {
                        param[10] = new SqlParameter("@ParamMarkup", SqlDbType.Money);
                        param[10].Value = Convert.ToDouble(Markup);
                    }

                    if (!string.IsNullOrEmpty(Commission))
                    {
                        param[11] = new SqlParameter("@ParamCommission", SqlDbType.Money);
                        param[11].Value = Convert.ToDouble(Commission);
                    }
                    if (!string.IsNullOrEmpty(ModifyBy))
                    {
                        param[12] = new SqlParameter("@ParamModifyBy", SqlDbType.VarChar, 50);
                        param[12].Value = ModifyBy;
                    }
                    param[13] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                    param[13].Value = Counter;

                    param[14] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 500);
                    param[14].Direction = ParameterDirection.Output;

                    if (!string.IsNullOrEmpty(Campaign))
                    {
                        param[15] = new SqlParameter("@ParamCampaign", SqlDbType.VarChar, 100);
                        param[15].Value = Campaign;
                    }
                    if (!string.IsNullOrEmpty(offerType))
                    {
                        param[16] = new SqlParameter("@ParamOfferType", SqlDbType.VarChar, 50);
                        param[16].Value = offerType;
                    }
                    if (!string.IsNullOrEmpty(journeyType))
                    {
                        param[17] = new SqlParameter("@ParamTripType", SqlDbType.VarChar, 10);
                        param[17].Value = journeyType;
                    }
                    if (active.ToLower() == "true" || active.ToLower() == "false")
                    {

                        if (active.ToLower() == "true")
                        {
                            param[18] = new SqlParameter("@ParamActive", SqlDbType.Bit);
                            param[18].Value = 1;
                        }
                        else
                        {
                            param[18] = new SqlParameter("@ParamActive", SqlDbType.Bit);
                            param[18].Value = 0;
                        }
                    }

                    if (!string.IsNullOrEmpty(FlightType))
                    {
                        param[19] = new SqlParameter("@ParamFlightType", SqlDbType.VarChar, 50);
                        param[19].Value = FlightType;
                    }

                    if (!string.IsNullOrEmpty(modifiedDate))
                    {
                        param[20] = new SqlParameter("@ParamModifyDate", SqlDbType.DateTime);
                        param[20].Value = Convert.ToDateTime(modifiedDate);
                    }

                    SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_ChangeFare_UK", param);
                    string output = param[14].Value.ToString();
                    if (output != "true")
                        return output;
                }

            }
            catch (Exception ex)
            {
                return "false";
            }
        }
        return "true";
    }

    public string SET_FlightMarkupDetailUS(bool UseStatus)
    {
        try
        {
            using (SqlConnection connection = DataConnection.GetConnectionMarkup())
            {
                //string query = "Update FlightMarkupDetail set isActive='" + UseStatus + "' where COMP_DTL_Company_ID in ('FLTTROTT_CA','C2BCA','TRVJUNCTION_CA') ";
                string query = "Update FlightMarkupDetail set isActive='" + UseStatus + "' where COMP_DTL_Company_ID IN ('FLTTROTT_CA','C2BCA','TRVJUNCTION_CA') and Agnt_AirF_Markup_ID NOT IN (99172,99171,99170) ";
                if (SqlHelper.ExecuteNonQuery(connection, CommandType.Text, query) > 1)
                {
                    return "true";
                }
                else
                {
                    return "false";
                }
            }
        }
        catch (Exception ex)
        {
            return "false";
        }
    }


    public string SET_FlightMarkupDetailUK(bool UseStatus)
    {
        try
        {
            using (SqlConnection connection = DataConnection.GetConnectionMarkup())
            {
                //string query = "Update FlightMarkupDetail set isActive='" + UseStatus + "' where COMP_DTL_Company_ID in ('FLTTROTT_CA','C2BCA','TRVJUNCTION_CA') ";
                string query = "Update FlightMarkupDetail set isActive='" + UseStatus + "' where COMP_DTL_Company_ID IN ('FLTTROTT_CA','C2BCA','TRVJUNCTION_CA') and Agnt_AirF_Markup_ID NOT IN (99172,99171,99170) ";
                if (SqlHelper.ExecuteNonQuery(connection, CommandType.Text, query) > 1)
                {
                    return "true";
                }
                else
                {
                    return "false";
                }
            }
        }
        catch (Exception ex)
        {
            return "false";
        }
    }


    public bool CheckDiscountMarkup_UK()
    {
        bool finalstatus = true;
        SqlConnection connection = DataConnection.GetConnectionMarkup();
        connection.Open();

        try
        {
            SqlCommand command = new SqlCommand("select top 1 * from FlightMarkupDetail_UK where isActive='true' and COMP_DTL_Company_ID NOT IN ('FLTTROTT_CA','C2BCA','TRVJUNCTION_CA')", connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                finalstatus = true;
            }
            else
            {
                finalstatus = false;
            }
        }
        catch (Exception ex)
        {
            return true;
        }
        finally
        {
            connection.Close();
        }
        return finalstatus;
    }

    public bool CheckDiscountMarkup_CA()
    {
        bool finalstatus = true;
        SqlConnection connection = DataConnection.GetConnectionMarkup();
        connection.Open();

        try
        {
            SqlCommand command = new SqlCommand("select top 1 * from FlightMarkupDetail_UK where isActive='true' and COMP_DTL_Company_ID in ('FLTTROTT_CA','C2BCA','TRVJUNCTION_CA')", connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                finalstatus = true;
            }
            else
            {
                finalstatus = false;
            }
        }
        catch (Exception ex)
        {
            return true;
        }
        finally
        {
            connection.Close();
        }
        return finalstatus;
    }

    #endregion

    #region Ticket Raise
    public bool Get_SET_Ticket_Details(string TicketDetail, string CompanyName, string CreatedBy, string AssignedTo, string Counter)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[5];

            if (!string.IsNullOrEmpty(TicketDetail))
            {
                param[0] = new SqlParameter("@TicketDetail", SqlDbType.VarChar);
                param[0].Value = TicketDetail;
            }
            if (!string.IsNullOrEmpty(CompanyName))
            {
                param[1] = new SqlParameter("@CompanyName", SqlDbType.VarChar, (50));
                param[1].Value = CompanyName;
            }
            if (!string.IsNullOrEmpty(CreatedBy))
            {
                param[2] = new SqlParameter("@CreatedBy", SqlDbType.VarChar, (50));
                param[2].Value = CreatedBy;
            }
            if (!string.IsNullOrEmpty(Counter))
            {
                param[3] = new SqlParameter("@Counter", SqlDbType.VarChar, (50));
                param[3].Value = Counter;
            }
            if (!string.IsNullOrEmpty(AssignedTo))
            {
                param[4] = new SqlParameter("@AssignedTo", SqlDbType.VarChar, (50));
                param[4].Value = AssignedTo;
            }
            using (SqlConnection conection = DataConnection.GetConnectionMarkup())
            {
                int i = SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "Get_Set_Ticket_Raise", param);
                return i > 0 ? true : false;
            }
        }
        catch
        {
            return false;
        }

    }
    public DataTable Get_Ticket_Details(string TicketDetail, string CompanyName, string CreatedBy, string Counter)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[4];

            if (!string.IsNullOrEmpty(TicketDetail))
            {
                param[0] = new SqlParameter("@TicketDetail", SqlDbType.VarChar);
                param[0].Value = TicketDetail;
            }
            if (!string.IsNullOrEmpty(CompanyName))
            {
                param[1] = new SqlParameter("@CompanyName", SqlDbType.VarChar, (50));
                param[1].Value = CompanyName;
            }
            if (!string.IsNullOrEmpty(CreatedBy))
            {
                param[2] = new SqlParameter("@CreatedBy", SqlDbType.VarChar, (50));
                param[2].Value = CreatedBy;
            }
            if (!string.IsNullOrEmpty(Counter))
            {
                param[3] = new SqlParameter("@Counter", SqlDbType.VarChar, (50));
                param[3].Value = Counter;
            }

            using (SqlConnection conection = DataConnection.GetConnectionMarkup())
            {
                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "Get_Set_Ticket_Raise", param);
                return ds.Tables[0];
            }
        }
        catch (Exception ex)
        {
            return null;
        }

    }
    #endregion

    #region Meta Searches
    public DataTable GET_MetaSearches(string fromDate, string toDate)
    {
        SqlParameter[] param = new SqlParameter[2];
        try
        {

            if (!string.IsNullOrEmpty(fromDate))
            {
                param[0] = new SqlParameter("@ParamSearchDateFrom", SqlDbType.DateTime);
                param[0].Value = Convert.ToDateTime(fromDate);
            }

            if (!string.IsNullOrEmpty(toDate))
            {
                param[1] = new SqlParameter("@ParamSearchDateTo", SqlDbType.DateTime);
                if (fromDate != "")
                {
                    param[1].Value = Convert.ToDateTime(toDate).AddDays(1);
                }
                else
                { param[1].Value = Convert.ToDateTime(toDate); }
            }

            using (SqlConnection connection = DataConnection.GetConnectionCache())
            {
                DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "MetaSearch", param);
                return ds.Tables[0] != null ? ds.Tables[0] : null;
            }

        }
        catch
        {
            return null;
        }
    }

    #endregion


    #region Calls / Enquiry
    public bool SET_Call_Details(string Cl_Ref, string Cl_Type, string Cl_Name, string Cl_Source, string Cl_Brand, string Cl_ContNo, string Cl_PaxName,
        string Cl_Email, string Cl_Origin, string Cl_Destination, string Cl_OBDate, string Cl_IBDate, string Cl_Airline, string Cl_Reason,
        string Cl_Status, string Cl_Remark, string Adults, string Childs, string Infants, string BookingID, string sourceMedia)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[22];

            if (!string.IsNullOrEmpty(Cl_Ref))
            {
                param[0] = new SqlParameter("@CL_Detail_Call_Ref", SqlDbType.VarChar, (50));
                param[0].Value = Cl_Ref;
            }
            if (!string.IsNullOrEmpty(Cl_Type))
            {
                param[1] = new SqlParameter("@CL_Query_Type", SqlDbType.VarChar, (50));
                param[1].Value = Cl_Type;
            }
            if (!string.IsNullOrEmpty(Cl_Name))
            {
                param[2] = new SqlParameter("@CL_DTL_Agent_Name", SqlDbType.VarChar, (250));
                param[2].Value = Cl_Name;
            }
            if (!string.IsNullOrEmpty(Cl_Source))
            {
                param[3] = new SqlParameter("@CL_DTL_Call_Source", SqlDbType.VarChar, (100));
                param[3].Value = Cl_Source;
            }
            if (!string.IsNullOrEmpty(Cl_Brand))
            {
                param[4] = new SqlParameter("@CL_DTL_Brand_Name", SqlDbType.VarChar, (50));
                param[4].Value = Cl_Brand;
            }
            if (!string.IsNullOrEmpty(Cl_ContNo))
            {
                param[5] = new SqlParameter("@CL_DTL_Contact_Number", SqlDbType.VarChar, (50));
                param[5].Value = Cl_ContNo;
            }
            if (!string.IsNullOrEmpty(Cl_PaxName))
            {
                param[6] = new SqlParameter("@CL_DTL_Lead_Pax_Name", SqlDbType.VarChar, (100));
                param[6].Value = Cl_PaxName;
            }
            if (!string.IsNullOrEmpty(Cl_Email))
            {
                param[7] = new SqlParameter("@CL_DTL_Email_Address", SqlDbType.VarChar, (100));
                param[7].Value = Cl_Email;
            }
            if (!string.IsNullOrEmpty(Cl_Origin))
            {
                param[8] = new SqlParameter("@CL_DTL_Origin", SqlDbType.VarChar, (100));
                param[8].Value = Cl_Origin;
            }
            if (!string.IsNullOrEmpty(Cl_Destination))
            {
                param[9] = new SqlParameter("@CL_DTL_Destination", SqlDbType.VarChar, (100));
                param[9].Value = Cl_Destination;
            }
            //if (!string.IsNullOrEmpty(Cl_NoP))
            //{
            //    param[10] = new SqlParameter("@CL_DTL_No_of_Passenger", SqlDbType.Int);
            //    param[10].Value = Convert.ToInt32(Cl_NoP);
            //}
            if (!string.IsNullOrEmpty(Cl_OBDate))
            {
                param[10] = new SqlParameter("@CL_DTL_Outbound_Date", SqlDbType.DateTime);
                param[10].Value = Convert.ToDateTime(Cl_OBDate);
            }
            if (!string.IsNullOrEmpty(Cl_IBDate))
            {
                param[11] = new SqlParameter("@CL_DTL_Return_Date", SqlDbType.DateTime);
                param[11].Value = Convert.ToDateTime(Cl_IBDate);
            }
            if (!string.IsNullOrEmpty(Cl_Airline))
            {
                param[12] = new SqlParameter("@CL_DTL_Airline", SqlDbType.VarChar, (100));
                param[12].Value = Cl_Airline;
            }
            if (!string.IsNullOrEmpty(Cl_Reason))
            {
                param[13] = new SqlParameter("@CL_DTL_Reason_of_Call", SqlDbType.VarChar, (100));
                param[13].Value = Cl_Reason;
            }
            if (!string.IsNullOrEmpty(Cl_Status))
            {
                param[14] = new SqlParameter("@CL_DTL_Status", SqlDbType.VarChar, (100));
                param[14].Value = Cl_Status;
            }
            if (!string.IsNullOrEmpty(Cl_Remark))
            {
                param[15] = new SqlParameter("@CL_DTL_Remarks", SqlDbType.VarChar);
                param[15].Value = Cl_Remark;
            }
            if (!string.IsNullOrEmpty(Adults))
            {
                param[16] = new SqlParameter("@CL_DTL_Adults", SqlDbType.VarChar);
                param[16].Value = Adults;
            }
            if (!string.IsNullOrEmpty(Childs))
            {
                param[17] = new SqlParameter("@CL_DTL_Childs", SqlDbType.VarChar);
                param[17].Value = Childs;
            }
            if (!string.IsNullOrEmpty(Infants))
            {
                param[18] = new SqlParameter("@CL_DTL_Infants", SqlDbType.VarChar);
                param[18].Value = Infants;
            }

            if (!string.IsNullOrEmpty(sourceMedia))
            {
                param[19] = new SqlParameter("@CL_DTL_SourceMedia", SqlDbType.VarChar);
                param[19].Value = sourceMedia;
            }
            if (!string.IsNullOrEmpty(BookingID))
            {
                param[20] = new SqlParameter("@CL_DTL_BookingID", SqlDbType.VarChar);
                param[20].Value = BookingID;
            }
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                int i = SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "USP_Call_Details", param);
                return i > 0 ? true : false;
            }
        }
        catch
        {
            return false;
        }

    }
    public DataTable GET_Call_Details(string Cl_Ref, string Cl_ContNo, string Cl_Email, string Cl_PaxName, string Cl_Origin, string Cl_Destination, string Agent, string CL_Date, string CL_ToDate, string CL_Brand, string type, string CL_Status = "")
    {
        try
        {
            SqlParameter[] param = new SqlParameter[12];

            if (!string.IsNullOrEmpty(Cl_Ref))
            {
                param[0] = new SqlParameter("@CL_Detail_Call_Ref", SqlDbType.VarChar, (50));
                param[0].Value = Cl_Ref;
            }
            if (!string.IsNullOrEmpty(Cl_ContNo))
            {
                param[1] = new SqlParameter("@CL_DTL_Contact_Number", SqlDbType.VarChar, (50));
                param[1].Value = Cl_ContNo;
            }
            if (!string.IsNullOrEmpty(Cl_Email))
            {
                param[2] = new SqlParameter("@CL_DTL_Email_Address", SqlDbType.VarChar, (50));
                param[2].Value = Cl_Email;
            }
            if (!string.IsNullOrEmpty(Cl_PaxName))
            {
                param[3] = new SqlParameter("@CL_DTL_Lead_Pax_Name", SqlDbType.VarChar, (100));
                param[3].Value = Cl_PaxName;
            }
            if (!string.IsNullOrEmpty(Cl_Origin))
            {
                param[4] = new SqlParameter("@CL_DTL_Origin", SqlDbType.VarChar, (50));
                param[4].Value = Cl_Origin;
            }
            if (!string.IsNullOrEmpty(Cl_Destination))
            {
                param[5] = new SqlParameter("@CL_DTL_Destination", SqlDbType.VarChar, (50));
                param[5].Value = Cl_Destination;
            }
            if (!string.IsNullOrEmpty(Agent))
            {
                param[6] = new SqlParameter("@CL_DTL_Agent", SqlDbType.VarChar, (100));
                param[6].Value = Agent;
            }
            if (!string.IsNullOrEmpty(CL_Date))
            {
                param[7] = new SqlParameter("@CL_DTL_Create_Date", SqlDbType.Date);
                param[7].Value = Convert.ToDateTime(CL_Date);
            }
            if (!string.IsNullOrEmpty(CL_ToDate))
            {
                param[8] = new SqlParameter("@CL_DTL_Create_DateTo", SqlDbType.Date);
                param[8].Value = Convert.ToDateTime(CL_ToDate);
            }

            if (!string.IsNullOrEmpty(type))
            {
                param[9] = new SqlParameter("@CL_Type", SqlDbType.VarChar, (10));
                param[9].Value = type;
            }

            if (!string.IsNullOrEmpty(CL_Brand))
            {
                param[10] = new SqlParameter("@CL_DTL_Brand_Name", SqlDbType.VarChar, (500));
                param[10].Value = CL_Brand;
            }

            if (!string.IsNullOrEmpty(CL_Status))
            {
                param[11] = new SqlParameter("@CL_Status", SqlDbType.VarChar, (50));
                param[11].Value = CL_Status;
            }

            using (SqlConnection connection = DataConnection.GetConnection())
            {
                DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "USP_GET_Call_Details", param);
                return ds.Tables[0] != null ? ds.Tables[0] : null;
            }
        }
        catch
        {
            return null;
        }
    }
    #endregion

    #region TicketIssuance
    public bool SET_TicketIssuance(string SNo, string airline, double charges, string currency, string issuer, string Modifyby, string counter)
    {
        SqlParameter[] param = new SqlParameter[8];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(airline))
                {
                    param[0] = new SqlParameter("@TKT_Airline", SqlDbType.VarChar, 3);
                    param[0].Value = airline;

                }
                if (charges != -1)
                {
                    param[1] = new SqlParameter("@TKT_Charges", SqlDbType.Money);
                    param[1].Value = charges;
                }
                if (!string.IsNullOrEmpty(currency))
                {
                    param[2] = new SqlParameter("@TKT_Currency", SqlDbType.VarChar, 3);
                    param[2].Value = currency;
                }
                if (!string.IsNullOrEmpty(issuer))
                {
                    param[3] = new SqlParameter("@TKT_Issuer", SqlDbType.VarChar, 250);
                    param[3].Value = issuer;
                }

                param[4] = new SqlParameter("@TKT_ModifyBy", SqlDbType.VarChar, 150);
                param[4].Value = Modifyby;

                param[5] = new SqlParameter("@TKT_Counter", SqlDbType.VarChar, 50);
                param[5].Value = counter;

                if (!string.IsNullOrEmpty(SNo))
                {
                    param[6] = new SqlParameter("@TKT_SNo", SqlDbType.BigInt);
                    param[6].Value = Convert.ToInt32(SNo);

                }
                return Convert.ToBoolean(SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "Get_Set_Ticket_Issuance_Charges", param));
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public DataTable Get_TicketIssuance(string SNo, string airline, double charges, string currency, string issuer)
    {
        SqlParameter[] param = new SqlParameter[5];
        DataSet ds;
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(airline))
                {
                    param[0] = new SqlParameter("@TKT_Airline", SqlDbType.VarChar, 3);
                    param[0].Value = airline;

                }
                if (charges != -1)
                {
                    param[1] = new SqlParameter("@TKT_Charges", SqlDbType.Money);
                    param[1].Value = charges;
                }
                if (!string.IsNullOrEmpty(currency))
                {
                    param[2] = new SqlParameter("@TKT_Currency", SqlDbType.VarChar, 3);
                    param[2].Value = currency;
                }
                if (!string.IsNullOrEmpty(issuer))
                {
                    param[3] = new SqlParameter("@TKT_Issuer", SqlDbType.VarChar, 100);
                    param[3].Value = issuer;
                }
                param[4] = new SqlParameter("@TKT_Counter", SqlDbType.VarChar, 100);
                param[4].Value = "SELECT";



                ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "Get_Set_Ticket_Issuance_Charges", param);
                if (ds != null)
                {
                    if (ds.Tables != null)
                    {
                        return ds.Tables[0];
                    }
                }
                return null;
            }
        }
        catch
        {
            return null;
        }
    }

    #endregion

    #region Profit Loss Report

    public DataTable Get_ProfitLoss(string fromDate, string toDate, string BookingByCompany)
    {
        SqlParameter[] param = new SqlParameter[3];
        DataSet ds;
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(fromDate))
                {
                    param[0] = new SqlParameter("@paramBookingFromDate", SqlDbType.Date);
                    param[0].Value = Convert.ToDateTime(fromDate);

                }

                if (!string.IsNullOrEmpty(toDate))
                {
                    param[1] = new SqlParameter("@paramBookingToDate", SqlDbType.Date);
                    param[1].Value = Convert.ToDateTime(toDate);
                }
                if (!string.IsNullOrEmpty(BookingByCompany))
                {
                    param[2] = new SqlParameter("@ParamBookingByCompany", SqlDbType.VarChar);
                    param[2].Value = BookingByCompany;
                }

                ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_Profit_Loss", param);
                if (ds != null)
                {
                    if (ds.Tables != null)
                    {
                        return ds.Tables[0];
                    }
                }
                return null;
            }
        }
        catch
        {
            return null;
        }
    }
    #endregion

    #region Ticketing Alert
    public DataTable Get_Ticketing_Alert(string BookingByCompany, string BookBy)
    {
        SqlParameter[] param = new SqlParameter[2];
        DataSet ds;
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(BookingByCompany))
                {
                    param[0] = new SqlParameter("@ParamBookingByCompany", SqlDbType.VarChar);
                    param[0].Value = BookingByCompany;
                }
                if (!string.IsNullOrEmpty(BookBy))
                {
                    param[1] = new SqlParameter("@ParamBookingBy", SqlDbType.VarChar);
                    param[1].Value = BookBy;
                }

                ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_Ticket_Alert", param);
                if (ds != null)
                {
                    if (ds.Tables != null)
                    {
                        return ds.Tables[0];
                    }
                }
                return null;
            }
        }
        catch
        {
            return null;
        }



    }

    #endregion

    #region Remarks

    public DataTable GET_Booking_Remarks(string Query, string BKG_RMK_Ref_No)
    {
        SqlParameter[] param = new SqlParameter[2];
        DataSet ds;
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                param[0] = new SqlParameter("@Query", SqlDbType.Char, 50);
                param[0].Value = Query;

                param[1] = new SqlParameter("@BKG_RMK_Ref_No", SqlDbType.VarChar, 50);
                param[1].Value = BKG_RMK_Ref_No;

                ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "USP_Booking_Remarks", param);
                return ds.Tables[0];
            }
        }
        catch
        {
            return null;
        }
    }
    #endregion

    #region Blackout Return Dates 

    public DataTable GetBlackoutReturn(string destination, string airline)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[3];
            using (SqlConnection connection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(destination))
                {
                    param[0] = new SqlParameter("@ParamDestination", SqlDbType.VarChar);
                    param[0].Value = destination;
                }
                if (!string.IsNullOrEmpty(airline))
                {
                    param[1] = new SqlParameter("@ParamAirline_Code", SqlDbType.VarChar);
                    param[1].Value = airline;
                }

                param[2] = new SqlParameter("@Counter", SqlDbType.VarChar);
                param[2].Value = "Select";


                DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "GET_SET_BlackoutReturn", param);
                if (ds.Tables[0] != null)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
        }
        catch
        {

            return null;
        }
    }
    public bool AddBlackoutDates(string destination, string airline, DateTime fromDate, DateTime toDate, string ModifiedBy)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[6];
            using (SqlConnection connection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(destination))
                {
                    param[0] = new SqlParameter("@ParamDestination", SqlDbType.VarChar);
                    param[0].Value = destination;
                }
                if (!string.IsNullOrEmpty(airline))
                {
                    param[1] = new SqlParameter("@ParamAirline_Code", SqlDbType.VarChar);
                    param[1].Value = airline;
                }
                if (!string.IsNullOrEmpty(destination))
                {
                    param[2] = new SqlParameter("@ParamFromDate", SqlDbType.Date);
                    param[2].Value = fromDate;
                }
                if (!string.IsNullOrEmpty(airline))
                {
                    param[3] = new SqlParameter("@ParamToDate", SqlDbType.Date);
                    param[3].Value = toDate;
                }

                param[4] = new SqlParameter("@ParamModifiedBy", SqlDbType.VarChar, 200);
                param[4].Value = ModifiedBy;
                param[5] = new SqlParameter("@Counter", SqlDbType.VarChar, 50);
                param[5].Value = "Insert";

                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "GET_SET_BlackoutReturn", param);
                return true;
            }
        }
        catch
        {

            return false;
        }
    }

    public bool AddBlackoutDates(DataTable blackoutDates)
    {
        try
        {
            foreach (DataRow dr in blackoutDates.Rows)
            {
                try
                {
                    AddBlackoutDates(dr["BLK_LST_Destination"].ToString(), dr["BLK_OUT_RT_AirlineCode"].ToString(),
                        Convert.ToDateTime(dr["BLK_OUT_RT_FromDate"]), Convert.ToDateTime(dr["BLK_OUT_RT_ToDate"]), dr["BLK_OUT_RT_ModifyBy"].ToString());


                }
                catch { }

            }
            return true;
        }
        catch
        {

            return false;
        }
    }

    public string DeleteBlackoutDate(string ID, string Counter)
    {
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(ID))
                {
                    param[0] = new SqlParameter("@ParamSNo", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(ID);
                }

                param[1] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[1].Value = "Delete";

                param[2] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 500);
                param[2].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_BlackoutReturn", param);
                return param[2].Value.ToString();
            }
        }
        catch
        {
            return "false";
        }
    }

    #endregion

    public bool SET_FlightDetailsDEC(
          string BM_BookingID,
          string BM_InvoiceNo,
          string BM_BookingType,
          string BM_CurrencyType,
          string BM_BookingByCompany,
          string BM_BookingStatus,
          string BM_IsInsertBM,
          string BD_ProdID,
          string BD_Provider,
          string BD_BookingBy,
          string BD_BookingByType,
          string BD_BookingDateTime,
          string BD_BookingStatus,
          string BD_BookingRemarks,
          string BD_TotalAmount,
          string BD_PNRConfirmation,
          string BD_SourceMedia,
          string BD_ProductType,
          string BD_isLocked,
          string BD_ModifiedBy,
          string BD_Supplier,
          string BD_MailIssued,
          string SM_JourneyType,
          string SM_LastTktDate,
          string SM_Origin,
          string SM_Destination,
          string SM_ValidatingCarrier,
          string SM_CabinClass,
          string SM_ModifiedBy,
          string CD_PaxID,
          string CD_PhoneNo,
          string CD_MobileNo,
          string CD_FAX,
          string CD_EmailAddress,
          string CD_Country,
          string CD_State,
          string CD_City,
          string CD_Address,
          string CD_PostCode,
          string CD_AddressType,
          string CD_ModifiedBy,
          DataTable AirSectors,
          DataTable AmountCharges,
          DataTable Passengers)
    {
        SqlParameter[] sqlParameterArray = new SqlParameter[46];
        try
        {
            using (SqlConnection connection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(BM_BookingID))
                {
                    sqlParameterArray[0] = new SqlParameter("@ParamBM_BookingID", SqlDbType.VarChar, 50);
                    sqlParameterArray[0].Value = (object)BM_BookingID;
                }
                if (!string.IsNullOrEmpty(BM_InvoiceNo))
                {
                    sqlParameterArray[1] = new SqlParameter("@ParamBM_InvoiceNo", SqlDbType.VarChar, 50);
                    sqlParameterArray[1].Value = (object)BM_InvoiceNo;
                }
                if (!string.IsNullOrEmpty(BM_BookingType))
                {
                    sqlParameterArray[2] = new SqlParameter("@ParamBM_BookingType", SqlDbType.VarChar, 50);
                    sqlParameterArray[2].Value = (object)BM_BookingType;
                }
                if (!string.IsNullOrEmpty(BM_CurrencyType))
                {
                    sqlParameterArray[3] = new SqlParameter("@ParamBM_CurrencyType", SqlDbType.VarChar, 50);
                    sqlParameterArray[3].Value = (object)BM_CurrencyType;
                }
                if (!string.IsNullOrEmpty(BM_BookingByCompany))
                {
                    sqlParameterArray[4] = new SqlParameter("@ParamBM_BookingByCompany", SqlDbType.VarChar, 50);
                    sqlParameterArray[4].Value = (object)BM_BookingByCompany;
                }
                if (!string.IsNullOrEmpty(BM_BookingStatus))
                {
                    sqlParameterArray[5] = new SqlParameter("@ParamBM_BookingStatus", SqlDbType.VarChar, 50);
                    sqlParameterArray[5].Value = (object)BM_BookingStatus;
                }
                sqlParameterArray[6] = new SqlParameter("@ParamBM_IsInsertBM", SqlDbType.VarChar, 50);
                sqlParameterArray[6].Value = (object)BM_IsInsertBM;
                if (!string.IsNullOrEmpty(BD_ProdID))
                {
                    sqlParameterArray[7] = new SqlParameter("@ParamBD_ProdID", SqlDbType.VarChar, 50);
                    sqlParameterArray[7].Value = (object)BD_ProdID;
                }
                if (!string.IsNullOrEmpty(BD_Provider))
                {
                    sqlParameterArray[8] = new SqlParameter("@ParamBD_Provider", SqlDbType.VarChar, 50);
                    sqlParameterArray[8].Value = (object)BD_Provider;
                }
                if (!string.IsNullOrEmpty(BD_BookingBy))
                {
                    sqlParameterArray[9] = new SqlParameter("@ParamBD_BookingBy", SqlDbType.VarChar, 100);
                    sqlParameterArray[9].Value = (object)BD_BookingBy;
                }
                if (!string.IsNullOrEmpty(BD_BookingByType))
                {
                    sqlParameterArray[10] = new SqlParameter("@ParamBD_BookingByType", SqlDbType.VarChar, 100);
                    sqlParameterArray[10].Value = (object)BD_BookingByType;
                }
                if (!string.IsNullOrEmpty(BD_BookingDateTime))
                {
                    sqlParameterArray[11] = new SqlParameter("@ParamBD_BookingDateTime", SqlDbType.DateTime);
                    sqlParameterArray[11].Value = (object)Convert.ToDateTime(BD_BookingDateTime);
                }
                if (!string.IsNullOrEmpty(BD_BookingStatus))
                {
                    sqlParameterArray[12] = new SqlParameter("@ParamBD_BookingStatus", SqlDbType.VarChar, 50);
                    sqlParameterArray[12].Value = (object)BD_BookingStatus;
                }
                if (!string.IsNullOrEmpty(BD_BookingRemarks))
                {
                    sqlParameterArray[13] = new SqlParameter("@ParamBD_BookingRemarks", SqlDbType.VarChar, 2000);
                    sqlParameterArray[13].Value = (object)BD_BookingRemarks;
                }
                if (!string.IsNullOrEmpty(BD_TotalAmount))
                {
                    sqlParameterArray[14] = new SqlParameter("@ParamBD_TotalAmount", SqlDbType.Money);
                    sqlParameterArray[14].Value = (object)Convert.ToDouble(BD_TotalAmount);
                }
                if (!string.IsNullOrEmpty(BD_PNRConfirmation))
                {
                    sqlParameterArray[15] = new SqlParameter("@ParamBD_PNRConfirmation", SqlDbType.VarChar, 50);
                    sqlParameterArray[15].Value = (object)BD_PNRConfirmation;
                }
                if (!string.IsNullOrEmpty(BD_SourceMedia))
                {
                    sqlParameterArray[16] = new SqlParameter("@ParamBD_SourceMedia", SqlDbType.VarChar, 50);
                    sqlParameterArray[16].Value = (object)BD_SourceMedia;
                }
                if (!string.IsNullOrEmpty(BD_ProductType))
                {
                    sqlParameterArray[17] = new SqlParameter("@ParamBD_ProductType", SqlDbType.VarChar, 50);
                    sqlParameterArray[17].Value = (object)BD_ProductType;
                }
                if (!string.IsNullOrEmpty(BD_isLocked))
                {
                    sqlParameterArray[18] = new SqlParameter("@ParamBD_isLocked", SqlDbType.Bit);
                    sqlParameterArray[18].Value = (object)Convert.ToBoolean(BD_isLocked);
                }
                if (!string.IsNullOrEmpty(BD_ModifiedBy))
                {
                    sqlParameterArray[19] = new SqlParameter("@ParamBD_ModifiedBy", SqlDbType.VarChar, 100);
                    sqlParameterArray[19].Value = (object)BD_ModifiedBy;
                }
                if (!string.IsNullOrEmpty(BD_Supplier))
                {
                    sqlParameterArray[20] = new SqlParameter("@ParamBD_Supplier", SqlDbType.VarChar, 100);
                    sqlParameterArray[20].Value = (object)BD_Supplier;
                }
                if (!string.IsNullOrEmpty(BD_MailIssued))
                {
                    sqlParameterArray[21] = new SqlParameter("@ParamBD_MailIssued", SqlDbType.Bit);
                    sqlParameterArray[21].Value = (object)Convert.ToBoolean(BD_MailIssued);
                }
                if (!string.IsNullOrEmpty(SM_JourneyType))
                {
                    sqlParameterArray[22] = new SqlParameter("@ParamSM_JourneyType", SqlDbType.VarChar, 50);
                    sqlParameterArray[22].Value = (object)SM_JourneyType;
                }
                if (!string.IsNullOrEmpty(SM_LastTktDate))
                {
                    sqlParameterArray[23] = new SqlParameter("@ParamSM_LastTktDate", SqlDbType.VarChar, 200);
                    sqlParameterArray[23].Value = (object)SM_LastTktDate;
                }
                if (!string.IsNullOrEmpty(SM_Origin))
                {
                    sqlParameterArray[24] = new SqlParameter("@ParamSM_Origin", SqlDbType.VarChar, 50);
                    sqlParameterArray[24].Value = (object)SM_Origin;
                }
                if (!string.IsNullOrEmpty(SM_Destination))
                {
                    sqlParameterArray[25] = new SqlParameter("@ParamSM_Destination", SqlDbType.VarChar, 50);
                    sqlParameterArray[25].Value = (object)SM_Destination;
                }
                if (!string.IsNullOrEmpty(SM_ValidatingCarrier))
                {
                    sqlParameterArray[26] = new SqlParameter("@ParamSM_ValidatingCarrier", SqlDbType.VarChar, 50);
                    sqlParameterArray[26].Value = (object)SM_ValidatingCarrier;
                }
                if (!string.IsNullOrEmpty(SM_CabinClass))
                {
                    sqlParameterArray[27] = new SqlParameter("@ParamSM_CabinClass", SqlDbType.VarChar, 50);
                    sqlParameterArray[27].Value = (object)SM_CabinClass;
                }
                if (!string.IsNullOrEmpty(SM_ModifiedBy))
                {
                    sqlParameterArray[28] = new SqlParameter("@ParamSM_ModifiedBy", SqlDbType.VarChar, 50);
                    sqlParameterArray[28].Value = (object)SM_ModifiedBy;
                }
                if (!string.IsNullOrEmpty(CD_PaxID))
                {
                    sqlParameterArray[29] = new SqlParameter("@ParamCD_PaxID", SqlDbType.VarChar, 50);
                    sqlParameterArray[29].Value = (object)CD_PaxID;
                }
                if (!string.IsNullOrEmpty(CD_PhoneNo))
                {
                    sqlParameterArray[30] = new SqlParameter("@ParamCD_PhoneNo", SqlDbType.VarChar, 100);
                    sqlParameterArray[30].Value = (object)CD_PhoneNo;
                }
                if (!string.IsNullOrEmpty(CD_MobileNo))
                {
                    sqlParameterArray[31] = new SqlParameter("@ParamCD_MobileNo", SqlDbType.VarChar, 100);
                    sqlParameterArray[31].Value = (object)CD_MobileNo;
                }
                if (!string.IsNullOrEmpty(CD_FAX))
                {
                    sqlParameterArray[32] = new SqlParameter("@ParamCD_FAX", SqlDbType.VarChar, 100);
                    sqlParameterArray[32].Value = (object)CD_FAX;
                }
                if (!string.IsNullOrEmpty(CD_EmailAddress))
                {
                    sqlParameterArray[33] = new SqlParameter("@ParamCD_EmailAddress", SqlDbType.VarChar, 500);
                    sqlParameterArray[33].Value = (object)CD_EmailAddress;
                }
                if (!string.IsNullOrEmpty(CD_Country))
                {
                    sqlParameterArray[34] = new SqlParameter("@ParamCD_Country", SqlDbType.VarChar, 200);
                    sqlParameterArray[34].Value = (object)CD_Country;
                }
                if (!string.IsNullOrEmpty(CD_State))
                {
                    sqlParameterArray[35] = new SqlParameter("@ParamCD_State", SqlDbType.VarChar, 100);
                    sqlParameterArray[35].Value = (object)CD_State;
                }
                if (!string.IsNullOrEmpty(CD_City))
                {
                    sqlParameterArray[36] = new SqlParameter("@ParamCD_City", SqlDbType.VarChar, 200);
                    sqlParameterArray[36].Value = (object)CD_City;
                }
                if (!string.IsNullOrEmpty(CD_Address))
                {
                    sqlParameterArray[37] = new SqlParameter("@ParamCD_Address", SqlDbType.VarChar, 2000);
                    sqlParameterArray[37].Value = (object)CD_Address;
                }
                if (!string.IsNullOrEmpty(CD_PostCode))
                {
                    sqlParameterArray[38] = new SqlParameter("@ParamCD_PostCode", SqlDbType.VarChar, 50);
                    sqlParameterArray[38].Value = (object)CD_PostCode;
                }
                if (!string.IsNullOrEmpty(CD_AddressType))
                {
                    sqlParameterArray[39] = new SqlParameter("@ParamCD_AddressType", SqlDbType.VarChar, 50);
                    sqlParameterArray[39].Value = (object)CD_AddressType;
                }
                if (!string.IsNullOrEmpty(CD_ModifiedBy))
                {
                    sqlParameterArray[40] = new SqlParameter("@ParamCD_ModifiedBy", SqlDbType.VarChar, 50);
                    sqlParameterArray[40].Value = (object)CD_ModifiedBy;
                }
                sqlParameterArray[41] = new SqlParameter("@ParamPassengers", (object)Passengers);
                sqlParameterArray[42] = new SqlParameter("@ParamAmountCharges", (object)AmountCharges);
                sqlParameterArray[43] = new SqlParameter("@ParamAirSectors", (object)AirSectors);
                sqlParameterArray[44] = new SqlParameter("@ParamStatus", SqlDbType.Bit);
                sqlParameterArray[44].Direction = ParameterDirection.Output;
                sqlParameterArray[45] = new SqlParameter("@ParamErrorNO", SqlDbType.VarChar, 500);
                sqlParameterArray[45].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SET_FlightDetailsDEC", sqlParameterArray);

                string str = "Status=" + sqlParameterArray[44].Value.ToString() + "  ErrorNO=" + sqlParameterArray[45].Value.ToString();
            
                //File.WriteAllText("D:\\Websites\\TravelJunction.co.uk\\TravelJunction_V2\\App_Data\\Errors\\" + BM_BookingID + "-GetSetInDB.txt", Convert.ToString(str));

                return Convert.ToBoolean(sqlParameterArray[44].Value);
            }
        }
        catch (Exception ex)
        {
            File.WriteAllText("D:\\Websites\\TravelJunction.co.uk\\TravelJunction_V2\\App_Data\\Errors\\" + BM_BookingID + "-GetSetInDB.txt", Convert.ToString(ex.Message + "\\n\\t" + ex.StackTrace + "\\n\\t" + ex.Source));
            return false;
        }
    }
    public string SaveOfflineBookingInDB(DataTable dtSect, DataTable dtCharges, DataTable dtPaxes, string BookingStatus, string Currency, string CompanyID,
            string CabinClass, string BookingBy, String BookingRemarks, string destination, string PNR,
         string Phone, string Mobile, string EmailID, string Add, string City, string Country)
    {
        string id = string.Empty;
        try
        {


            GetSetDatabase GetSetDatabase = new GetSetDatabase();
           
            string BookingID = string.Empty;
           
            BookingID = GetSetDatabase.GenerateIDs("CT");

            string productId = "001";
            double TotalCost = 0;
            #region Set UP Booking Ref and Product Id
            foreach (DataRow dr in dtSect.Rows)
            {
                dr["BOK_MST_Booking_ID"] = BookingID;
                dr["BOK_DTL_Prod_Booking_ID"] = productId;
            }
            foreach (DataRow dr in dtCharges.Rows)
            {

                dr["BOK_MST_Booking_ID"] = BookingID;
                dr["BOK_DTL_Prod_Booking_ID"] = productId;
            }
            foreach (DataRow dr in dtPaxes.Rows)
            {
                dr["BOK_MST_Booking_ID"] = BookingID;
                dr["BOK_DTL_Prod_Booking_ID"] = productId;

            }

            #endregion


            if (GetSetDatabase.SET_FlightDetailsDEC(BookingID, BookingID, "ARF",
                                       Currency, CompanyID, BookingStatus,
                                      "true", productId, string.Empty,
                                       BookingBy, "INTR", DateTime.Now.ToString(), BookingStatus, BookingRemarks,
                                       TotalCost.ToString(), PNR, string.Empty,
                                       "ARF", "false", BookingBy, "", "false",
                                       "Return", string.Empty, dtSect.Rows[0]["SEC_DTL_From_Destination"].ToString(),
                                       destination, dtSect.Rows[0]["SEC_DTL_Carier_Name"].ToString(),
                                       CabinClass, BookingBy, "1", Phone,
                                       Mobile, "0000", EmailID, Country, ".", City, Add, "", "Delivery", string.Empty,
                                       dtSect, dtCharges, dtPaxes))
            {
                SET_Booking_Detail(BookingID, productId, "", BookingBy,"", DateTime.Now.ToString(), "", "", "", BookingRemarks, "", "", "", "", "", BookingBy, "", "", "Update", "", "","",CompanyID,"");
                return BookingID;
            }
            else
            {
                return "false";
            }

        }
        catch (Exception ex)
        {


            return "false";
        }
    }

    public string SaveOfflineBookingInDBOld(DataTable dtSect, DataTable dtCharges, DataTable dtPaxes, string BookingStatus, string Currency, string CompanyID,
           string CabinClass, string BookingBy, String BookingRemarks, string destination, string PNR,
        string Phone, string Mobile, string EmailID, string Add, string City, string Country, DateTime bookingdatetime)
    {
        string id = string.Empty;
        try
        {


            GetSetDatabase GetSetDatabase = new GetSetDatabase();
            CompanyID = CompanyID.ToUpper();
            string BookingID = string.Empty;
            if (CompanyID == "FLTTROTT" || CompanyID == "FLTTROTT_CA" || CompanyID == "FLTTROTT_USA")
            { BookingID = GetSetDatabase.GenerateIDs("FI"); }
            else { BookingID = GetSetDatabase.GenerateIDs("XP"); }

            string productId = "001";
            double TotalCost = 0;
            #region Set UP Booking Ref and Product Id
            foreach (DataRow dr in dtSect.Rows)
            {
                dr["BOK_MST_Booking_ID"] = BookingID;
                dr["BOK_DTL_Prod_Booking_ID"] = productId;
            }
            foreach (DataRow dr in dtCharges.Rows)
            {

                dr["BOK_MST_Booking_ID"] = BookingID;
                dr["BOK_DTL_Prod_Booking_ID"] = productId;
            }
            foreach (DataRow dr in dtPaxes.Rows)
            {
                dr["BOK_MST_Booking_ID"] = BookingID;
                dr["BOK_DTL_Prod_Booking_ID"] = productId;

            }

            #endregion


            if (GetSetDatabase.SET_FlightDetails(BookingID, BookingID, "ARF",
                                       Currency, CompanyID, BookingStatus,
                                      "true", productId, "",
                                       BookingBy, "INTR", bookingdatetime.ToString(), BookingStatus, BookingRemarks,
                                       TotalCost.ToString(), PNR, "",
                                       "ARF", "false", BookingBy, "", "false",
                                        "Return",
                                       "", dtSect.Rows[0]["SEC_DTL_From_Destination"].ToString(),
                                       destination, "",
                                       CabinClass, BookingBy, "1", Phone,
                                       Mobile, "0000", EmailID, Country, ".", City, Add, "", "Delivery", "",
                                       dtSect, dtCharges, dtPaxes))
            { return BookingID; }
            else { return "false"; }

        }
        catch (Exception ex)
        {


            return "false";
        }
    }

    #region Agents P & L

    public DataTable GetInvoiceProfit(string query, string bref, string modBy, string startDt, string endDt)
    {
        DataTable dt = new DataTable();
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("PROC_Agent_ProfitnLose", conection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Query", query);
                cmd.Parameters.AddWithValue("@AGNT_P_L_BookingRef", bref);
                cmd.Parameters.AddWithValue("@AGNT_P_L_Profit", null);
                cmd.Parameters.AddWithValue("@AGNT_P_L_ModifyBy", modBy);
                cmd.Parameters.AddWithValue("@StartDate", startDt);
                cmd.Parameters.AddWithValue("@EndDate", endDt);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }

        }
        catch (Exception ex) { }
        finally
        { }
        return dt;
    }
    public int SetInvoiceProfit(string query, string bref, decimal profit, string modBy, string remarks)
    {
        int i = 0;
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("PROC_Agent_ProfitnLose", conection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Query", query);
                cmd.Parameters.AddWithValue("@AGNT_P_L_BookingRef", bref);
                cmd.Parameters.AddWithValue("@AGNT_P_L_Profit", profit);
                cmd.Parameters.AddWithValue("@AGNT_P_L_ModifyBy", modBy);
                cmd.Parameters.AddWithValue("@AGNT_P_L_Remarks", remarks);
                conection.Open();
                i = cmd.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            return -1;
        }
        finally { }
        return i;
    }


    #endregion


    #region Company card usages
    public DataTable GET_CompanyCardUses(string Query, string transDate, string transDateto, string lastfour, string ourref, string userName)
    {
        DataTable dt = new DataTable();
        try
        {
            using (SqlConnection con = DataConnection.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("PROC_CompanyCardUses", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Query", Query);
                cmd.Parameters.AddWithValue("@CCU_TransactionDate", string.IsNullOrEmpty(transDate) ? null : transDate);
                cmd.Parameters.AddWithValue("@CCU_TransactionDateTo", string.IsNullOrEmpty(transDateto) ? null : transDateto);
                cmd.Parameters.AddWithValue("@CCU_LastFour", string.IsNullOrEmpty(lastfour) ? null : lastfour);
                cmd.Parameters.AddWithValue("@CCU_OurRef", string.IsNullOrEmpty(ourref) ? null : ourref);
                cmd.Parameters.AddWithValue("@CCU_UserName", string.IsNullOrEmpty(userName) ? null : userName);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }

        }
        catch (Exception ex) { }

        return dt;
    }
    public bool SET_CompanyCardUses(string Query, string transDate, string paxname, string merchant, string currency, string amount, string lastfour, string merchantRef, string ourRef, string userName, string notes)
    {
        try
        {
            using (SqlConnection con = DataConnection.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("PROC_CompanyCardUses", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Query", Query);
                if (!string.IsNullOrEmpty(transDate))
                {
                    cmd.Parameters.AddWithValue("@CCU_TravelDate", Convert.ToDateTime(transDate));
                }
                cmd.Parameters.AddWithValue("@CCU_PaxName", paxname);
                cmd.Parameters.AddWithValue("@CCU_Merchant", merchant);
                cmd.Parameters.AddWithValue("@CCU_Currency", currency);
                cmd.Parameters.AddWithValue("@CCU_Amount", Convert.ToDecimal(amount));
                cmd.Parameters.AddWithValue("@CCU_LastFour", lastfour);
                cmd.Parameters.AddWithValue("@CCU_MerchantRef", merchantRef);
                cmd.Parameters.AddWithValue("@CCU_OurRef", ourRef);
                cmd.Parameters.AddWithValue("@CCU_UserName", userName);
                cmd.Parameters.AddWithValue("@CCU_Notes", notes);
                con.Open();
                return Convert.ToBoolean(cmd.ExecuteNonQuery());
            }
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {

        }
    }
    #endregion


    #region Kayak FlihtsInline
    public DataTable GetKAYAK(string Query, string From, string To)
    {
        DataTable dt = new DataTable();
        From = string.IsNullOrEmpty(From) ? null : From;
        To = string.IsNullOrEmpty(To) ? null : To;
        try
        {
            using (SqlConnection con = DataConnection.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("PROC_KAYAK_Compare", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Query", "SELECT");
                cmd.Parameters.AddWithValue("@KAYAK_From", From);
                cmd.Parameters.AddWithValue("@KAYAK_To", To);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

            }

        }
        catch (Exception ex) { }
        return dt;

    }

    public int SetKAYAKCompare(string query, int id, string Title, string Description, string From, string To, string ModifyBy)
    {
        int i = 0;

        try
        {
            using (SqlConnection con = DataConnection.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("PROC_KAYAK_Compare", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Query", query);
                cmd.Parameters.AddWithValue("@KAYAK_ID", id);
                cmd.Parameters.AddWithValue("@KAYAK_Title", Title);
                cmd.Parameters.AddWithValue("@KAYAK_Description", Description);
                cmd.Parameters.AddWithValue("@KAYAK_From", From);
                cmd.Parameters.AddWithValue("@KAYAK_To", To);
                cmd.Parameters.AddWithValue("@ModifyBy", ModifyBy);
                con.Open();
                i = cmd.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            // ltrMsg.Text = "Try Agin";
        }

        return i;
    }
    #endregion

    #region CPC
    public DataTable GetCPC_Cost(string Destination, string SourceMedia)
    {
        DataTable dt = new DataTable();

        try
        {
            using (SqlConnection con = DataConnection.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("GET_SET_CPC_Cost_Master", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Query", "SELECT");
                if (!string.IsNullOrEmpty(Destination))
                    cmd.Parameters.AddWithValue("@ParamDestination", Destination);

                if (!string.IsNullOrEmpty(SourceMedia))
                    cmd.Parameters.AddWithValue("@ParamCampaign", SourceMedia);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

            }

        }
        catch (Exception ex) { }
        return dt;

    }


    public DataTable GetCPC_Online_Cost_Detail(string FromDate, string ToDate, string CampaignId, string Counter, int MetaClicks, string UserId)
    {
        DataTable dt = new DataTable();
        try
        {
            using (SqlConnection con = DataConnection.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("GET_SET_Online_CPC_Detail", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                if (!string.IsNullOrEmpty(FromDate))
                {
                    cmd.Parameters.AddWithValue("@ParamFromDate", Convert.ToDateTime(FromDate));
                }

                if (!string.IsNullOrEmpty(ToDate))
                {
                    cmd.Parameters.AddWithValue("@ParamToDate", Convert.ToDateTime(ToDate));
                }

                if (!string.IsNullOrEmpty(CampaignId))
                {
                    cmd.Parameters.AddWithValue("@ParamCampaign", CampaignId);
                }

                cmd.Parameters.AddWithValue("@ParmaMetaClicks", MetaClicks);
                cmd.Parameters.AddWithValue("@Counter", Counter);
                cmd.Parameters.AddWithValue("@ParamUserId", UserId);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
        }
        catch (Exception ex)
        {
            return null;
        }
        return dt;

    }

    #endregion

    #region Atol Report
    public DataTable GET_Atol_Report(string BookingRef, string BookingBy, string fromDate, string toDate, string company, string status,
        string DepartDateFrom = "", string DepartDateTo = "", string airline = "", string supplier = "", string issuedFromDate = "", string issuedToDate = "")
    {

        SqlParameter[] param = new SqlParameter[12];
        DataSet ds;
        try
        {
            if (!string.IsNullOrEmpty(BookingRef))
            {
                param[0] = new SqlParameter("@ParamBookingID", SqlDbType.VarChar, 50);
                param[0].Value = BookingRef;
            }

            if (!string.IsNullOrEmpty(BookingBy))
            {
                param[1] = new SqlParameter("@ParamBookingBy", SqlDbType.VarChar, 50);
                param[1].Value = BookingBy;
            }

            if (!string.IsNullOrEmpty(fromDate))
            {
                param[2] = new SqlParameter("@paramFromDate", SqlDbType.Date);
                param[2].Value = fromDate;
            }

            if (!string.IsNullOrEmpty(toDate))
            {
                param[3] = new SqlParameter("@paramToDate", SqlDbType.Date);
                param[3].Value = toDate;
            }
            if (!string.IsNullOrEmpty(company))
            {
                param[4] = new SqlParameter("@paramCompany", SqlDbType.VarChar, 5000);
                param[4].Value = company;
            }
            if (!string.IsNullOrEmpty(status))
            {
                param[5] = new SqlParameter("@paramStatus", SqlDbType.VarChar, 5000);
                param[5].Value = status;
            }
            if (!string.IsNullOrEmpty(DepartDateFrom))
            {
                param[6] = new SqlParameter("@paramFromDepartDate", SqlDbType.Date);
                param[6].Value = DepartDateFrom;
            }

            if (!string.IsNullOrEmpty(DepartDateTo))
            {
                param[7] = new SqlParameter("@paramToDepartDate", SqlDbType.Date);
                param[7].Value = Convert.ToDateTime(DepartDateTo).AddDays(1);
            }
            if (!string.IsNullOrEmpty(airline))
            {
                param[8] = new SqlParameter("@ParamAirline", SqlDbType.VarChar, 50);
                param[8].Value = airline;
            }

            if (!string.IsNullOrEmpty(supplier))
            {
                param[9] = new SqlParameter("@ParamSupplier", SqlDbType.VarChar, 100);
                param[9].Value = supplier;
            }

            if (!string.IsNullOrEmpty(issuedFromDate))
            {
                param[10] = new SqlParameter("@paramFromIssuedDate", SqlDbType.Date);
                param[10].Value = issuedFromDate;
            }

            if (!string.IsNullOrEmpty(issuedToDate))
            {
                param[11] = new SqlParameter("@paramToIssuedDate", SqlDbType.Date);
                param[11].Value = Convert.ToDateTime(issuedToDate).AddDays(1);
            }

            using (SqlConnection conection = DataConnection.GetConnection())
            {
                ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "Get_ATOL_Report", param);

                return ds.Tables[0];
            }
        }
        catch (Exception ex)
        {
            return null;
        }

    }

    public DataTable GET_Atol_Report_Full(string BookingRef, string BookingBy, string fromDate, string toDate, string company, string status,
        string DepartDateFrom = "", string DepartDateTo = "", string airline = "", string supplier = "", string issuedFromDate = "", string issuedToDate = "")
    {

        SqlParameter[] param = new SqlParameter[12];
        DataSet ds;
        try
        {
            if (!string.IsNullOrEmpty(BookingRef))
            {
                param[0] = new SqlParameter("@ParamBookingID", SqlDbType.VarChar, 50);
                param[0].Value = BookingRef;
            }

            if (!string.IsNullOrEmpty(BookingBy))
            {
                param[1] = new SqlParameter("@ParamBookingBy", SqlDbType.VarChar, 50);
                param[1].Value = BookingBy;
            }

            if (!string.IsNullOrEmpty(fromDate))
            {
                param[2] = new SqlParameter("@paramFromDate", SqlDbType.Date);
                param[2].Value = fromDate;
            }

            if (!string.IsNullOrEmpty(toDate))
            {
                param[3] = new SqlParameter("@paramToDate", SqlDbType.Date);
                param[3].Value = toDate;
            }
            if (!string.IsNullOrEmpty(company))
            {
                param[4] = new SqlParameter("@paramCompany", SqlDbType.VarChar, 5000);
                param[4].Value = company;
            }
            if (!string.IsNullOrEmpty(status))
            {
                param[5] = new SqlParameter("@paramStatus", SqlDbType.VarChar, 5000);
                param[5].Value = status;
            }
            if (!string.IsNullOrEmpty(DepartDateFrom))
            {
                param[6] = new SqlParameter("@paramFromDepartDate", SqlDbType.Date);
                param[6].Value = DepartDateFrom;
            }

            if (!string.IsNullOrEmpty(DepartDateTo))
            {
                param[7] = new SqlParameter("@paramToDepartDate", SqlDbType.Date);
                param[7].Value = Convert.ToDateTime(DepartDateTo).AddDays(1);
            }
            if (!string.IsNullOrEmpty(airline))
            {
                param[8] = new SqlParameter("@ParamAirline", SqlDbType.VarChar, 50);
                param[8].Value = airline;
            }

            if (!string.IsNullOrEmpty(supplier))
            {
                param[9] = new SqlParameter("@ParamSupplier", SqlDbType.VarChar, 100);
                param[9].Value = supplier;
            }

            if (!string.IsNullOrEmpty(issuedFromDate))
            {
                param[10] = new SqlParameter("@paramFromIssuedDate", SqlDbType.Date);
                param[10].Value = issuedFromDate;
            }

            if (!string.IsNullOrEmpty(issuedToDate))
            {
                param[11] = new SqlParameter("@paramToIssuedDate", SqlDbType.Date);
                param[11].Value = Convert.ToDateTime(issuedToDate).AddDays(1);
            }

            using (SqlConnection conection = DataConnection.GetConnection())
            {
                ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "Get_ATOL_Report_Full", param);

                return ds.Tables[0];
            }
        }
        catch (Exception ex)
        {
            return null;
        }

    }


    public DataTable GET_Atol_Report_SMT(string BookingRef, string fromDate, string toDate, string AtolType, string PNR, string ticket, string DepartDateFrom = "", string DepartDateTo = "", string supplier = "")
    {

        SqlParameter[] param = new SqlParameter[10];
        DataSet ds;
        try
        {
            if (!string.IsNullOrEmpty(BookingRef))
            {
                param[0] = new SqlParameter("@ParamBookingID", SqlDbType.VarChar, 50);
                param[0].Value = BookingRef;
            }

            if (!string.IsNullOrEmpty(AtolType))
            {
                param[1] = new SqlParameter("@ParamAtolType", SqlDbType.VarChar, 1000);
                param[1].Value = AtolType;
            }

            if (!string.IsNullOrEmpty(fromDate))
            {
                param[2] = new SqlParameter("@paramFromDate", SqlDbType.Date);
                param[2].Value = fromDate;
            }

            if (!string.IsNullOrEmpty(toDate))
            {
                param[3] = new SqlParameter("@paramToDate", SqlDbType.Date);
                param[3].Value = toDate;
            }

            if (!string.IsNullOrEmpty(DepartDateFrom))
            {
                param[4] = new SqlParameter("@paramFromDepartDate", SqlDbType.Date);
                param[4].Value = DepartDateFrom;
            }

            if (!string.IsNullOrEmpty(DepartDateTo))
            {
                param[5] = new SqlParameter("@paramToDepartDate", SqlDbType.Date);
                //param[5].Value = Convert.ToDateTime(DepartDateTo).AddDays(1);
                param[5].Value = Convert.ToDateTime(DepartDateTo);
            }
            if (!string.IsNullOrEmpty(PNR))
            {
                param[6] = new SqlParameter("@paramPNR", SqlDbType.VarChar, 50);
                param[6].Value = PNR;
            }

            if (!string.IsNullOrEmpty(ticket))
            {
                param[7] = new SqlParameter("@ParamTicketNumber", SqlDbType.VarChar, 100);
                param[7].Value = ticket;
            }

            if (!string.IsNullOrEmpty(supplier))
            {
                param[8] = new SqlParameter("@ParamSupplier", SqlDbType.VarChar, 100);
                param[8].Value = supplier;
            }


            using (SqlConnection conection = DataConnection.GetConnection())
            {
                ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "Get_Atol_Report_SmartTech", param);

                return ds.Tables[0];
            }
        }
        catch (Exception ex)
        {
            return null;
        }

    }

    #endregion

    #region Hotels
    public bool SaveHotel(string query, string BID, string PID, string HotelName, string SuppRef, int Adt, int Chi, int Inf,
        string CI, string CO, string Dest, string MType, string RType, string HTLSupp, string HTLRef, string Add1, string Add2, string po, string city, string country, string phone, string email, int noRoom)
    {

        try
        {
            using (SqlConnection con = DataConnection.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("USP_HTL_DTL_Hotel", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Query", query);
                cmd.Parameters.AddWithValue("@HTL_DTL_BookingID", BID);
                cmd.Parameters.AddWithValue("@HTL_DTL_ProductID", PID);
                cmd.Parameters.AddWithValue("@HTL_DTL_Hotel", HotelName);
                cmd.Parameters.AddWithValue("@HTL_DTL_SupplierRef", SuppRef);
                cmd.Parameters.AddWithValue("@HTL_DTL_Adult", Adt);
                cmd.Parameters.AddWithValue("@HTL_DTL_Child", Chi);
                cmd.Parameters.AddWithValue("@HTL_DTL_Infants", Inf);
                cmd.Parameters.AddWithValue("@HTL_DTL_CheckIn", CI);
                cmd.Parameters.AddWithValue("@HTL_DTL_CheckOut", CO);
                cmd.Parameters.AddWithValue("@HTL_DTL_Destination", Dest);
                cmd.Parameters.AddWithValue("@HTL_DTL_MealType", MType);
                cmd.Parameters.AddWithValue("HTL_DTL_RoomType", RType);
                cmd.Parameters.AddWithValue("@HTL_DTL_HotelSupplier", HTLSupp);
                cmd.Parameters.AddWithValue("@HTL_DTL_Ref", HTLRef);
                cmd.Parameters.AddWithValue("@HTL_DTL_Address1", Add1);
                cmd.Parameters.AddWithValue("@HTL_DTL_Address2", Add2);
                cmd.Parameters.AddWithValue("@HTL_DTL_PostalCode", po);
                cmd.Parameters.AddWithValue("@HTL_DTL_City", city);
                cmd.Parameters.AddWithValue("@HTL_DTL_Country", country);
                cmd.Parameters.AddWithValue("@HTL_DTL_Telephone", phone);
                cmd.Parameters.AddWithValue("@HTL_DTL_Email", email);
                cmd.Parameters.AddWithValue("@HTL_DTL_NoRooms", noRoom);

                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
        }
        catch (Exception ex)
        {
            return false;
        }



    }
    #endregion

    #region Product ID
    public string getMaxProductID(string bookingID)
    {

        using (SqlConnection con = DataConnection.GetConnection())
        {
            SqlCommand cmd = new SqlCommand("Get_Max_ProductID", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ParamBookingID", bookingID);
            cmd.Parameters.Add("@ParamMaxProdID", SqlDbType.NVarChar, 25).Direction = ParameterDirection.Output;
            con.Open();
            cmd.ExecuteNonQuery();
            if (cmd.Parameters["@ParamMaxProdID"].Value != null || cmd.Parameters["@ParamMaxProdID"].Value == "")
            {
                return "00" + Convert.ToInt16(cmd.Parameters["@ParamMaxProdID"].Value.ToString()) + 1;
            }
            else
                return "000";

        }
    }
    #endregion

    public DataTable GET_TicketIssuedBy(string fromInvoiceDate, string toInvoiceDate, string company, string query = "summary")
    {

        SqlParameter[] param = new SqlParameter[4];
        DataSet ds;
        try
        {
            if (!string.IsNullOrEmpty(fromInvoiceDate))
            {
                param[0] = new SqlParameter("@paramFromInvoiceDate", SqlDbType.Date);
                param[0].Value = fromInvoiceDate;
            }

            if (!string.IsNullOrEmpty(toInvoiceDate))
            {
                param[1] = new SqlParameter("@paramToInvoiceDate", SqlDbType.Date);
                param[1].Value = toInvoiceDate;
            }


            if (!string.IsNullOrEmpty(company))
            {
                param[2] = new SqlParameter("@paramIssuedBy", SqlDbType.VarChar, 50);
                param[2].Value = company;
            }

            if (!string.IsNullOrEmpty(query))
            {
                param[3] = new SqlParameter("@Counter", SqlDbType.VarChar, 50);
                param[3].Value = query;
            }


            using (SqlConnection conection = DataConnection.GetConnection())
            {
                ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "Get_Ticket_Issued_By", param);

                return ds.Tables[0];
            }
        }
        catch (Exception ex)
        {
            return null;
        }

    }

    public DataTable SearchByCharges(string ChargesID, string startDt, string endDt)
    {
        SqlParameter[] param = new SqlParameter[4];
        DataSet ds;
        try
        {

            if (!string.IsNullOrEmpty(ChargesID))
            {
                param[0] = new SqlParameter("@ParamChargesID", SqlDbType.VarChar, 50);
                param[0].Value = ChargesID;
            }

            if (!string.IsNullOrEmpty(startDt))
            {
                param[1] = new SqlParameter("@paramFromDate", SqlDbType.Date);
                param[1].Value = startDt;
            }

            if (!string.IsNullOrEmpty(endDt))
            {
                param[2] = new SqlParameter("@paramToDate", SqlDbType.Date);
                param[2].Value = endDt;
            }




            using (SqlConnection conection = DataConnection.GetConnection())
            {
                ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "SearchByCharges", param);

                return ds.Tables[0];
            }


        }
        catch (Exception ex) { return null; }
        finally
        { }

    }

    public DataTable BookingAssignement(string bookingref, string assignedTo, string assignedBy,
       string booking_startDt, string booking_endDt, string query, string companies, string Status)
    {
        SqlParameter[] param = new SqlParameter[8];
        DataSet ds;
        try
        {

            if (!string.IsNullOrEmpty(bookingref))
            {
                param[0] = new SqlParameter("@AS_Booking_Ref", SqlDbType.VarChar, 50);
                param[0].Value = bookingref;
            }

            if (!string.IsNullOrEmpty(assignedTo))
            {
                param[1] = new SqlParameter("@AS_Assigned_To", SqlDbType.VarChar, 50);
                param[1].Value = assignedTo;
            }

            if (!string.IsNullOrEmpty(booking_startDt))
            {
                param[2] = new SqlParameter("@AS_BookingDateFrom", SqlDbType.Date);
                param[2].Value = booking_startDt;
            }
            if (!string.IsNullOrEmpty(booking_endDt))
            {
                param[3] = new SqlParameter("@AS_BookingDateTo", SqlDbType.Date);
                param[3].Value = booking_endDt;
            }

            if (!string.IsNullOrEmpty(query))
            {
                param[4] = new SqlParameter("@Query", SqlDbType.VarChar, 50);
                param[4].Value = query;
            }
            if (!string.IsNullOrEmpty(assignedBy))
            {
                param[5] = new SqlParameter("@AS_Assigned_From", SqlDbType.VarChar, 50);
                param[5].Value = assignedBy;
            }
            if (!string.IsNullOrEmpty(companies))
            {
                param[6] = new SqlParameter("@AS_Companies", SqlDbType.VarChar, 2000);
                param[6].Value = companies;
            }
            if (!string.IsNullOrEmpty(Status))
            {
                param[7] = new SqlParameter("@AS_Status", SqlDbType.VarChar, 2000);
                param[7].Value = Status;
            }


            using (SqlConnection conection = DataConnection.GetConnection())
            {
                ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "USP_BookiingAssignMaster", param);

                return ds.Tables[0];
            }


        }
        catch (Exception ex) { return null; }
        finally
        { }

    }


    public DataTable SearchInRemarks(string bookingref, string remaks, string remarksBy, string remakrs_startDt, string remakrs_endDt)
    {
        SqlParameter[] param = new SqlParameter[5];
        DataSet ds;
        try
        {

            if (!string.IsNullOrEmpty(bookingref))
            {
                param[0] = new SqlParameter("@paramBookingRef", SqlDbType.VarChar, 50);
                param[0].Value = bookingref;
            }

            if (!string.IsNullOrEmpty(remarksBy))
            {
                param[1] = new SqlParameter("@paramRemarksBy", SqlDbType.VarChar, 50);
                param[1].Value = remarksBy;
            }

            if (!string.IsNullOrEmpty(remakrs_startDt))
            {
                param[2] = new SqlParameter("@paramRemarksFromDate", SqlDbType.Date);
                param[2].Value = remakrs_startDt;
            }
            if (!string.IsNullOrEmpty(remakrs_endDt))
            {
                param[3] = new SqlParameter("@paramRemarksEndDate", SqlDbType.Date);
                param[3].Value = remakrs_endDt;
            }
            if (!string.IsNullOrEmpty(remaks))
            {
                param[4] = new SqlParameter("@paramRemarks", SqlDbType.VarChar, 250);
                param[4].Value = remaks;
            }



            using (SqlConnection conection = DataConnection.GetConnection())
            {
                ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "Search_In_Remarks1", param);

                return ds.Tables[0];
            }


        }
        catch (Exception ex) { return null; }
        finally
        { }

    }


    public DataTable GET_CredencialByCampaign(string campaignId)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            using (SqlConnection connection = DataConnection.GetConnection())
            {

                param[0] = new SqlParameter("@CampaignCode", SqlDbType.VarChar);
                param[0].Value = campaignId;


                DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "GET_CredencialByCampaign", param);
                if (ds.Tables[0] != null)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
        }
        catch
        {

            return null;
        }
    }


    #region Search booking by Status
    public DataTable GET_BookingByStatus(string BookingStatus, int NoDays)
    {
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(BookingStatus))
                {
                    param[0] = new SqlParameter("@BookingStaus", SqlDbType.VarChar, 50);
                    param[0].Value = BookingStatus;
                }
                if (!string.IsNullOrEmpty(NoDays.ToString()))
                {
                    param[1] = new SqlParameter("@NoDays", NoDays);
                    param[1].Value = NoDays;
                }
                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_BOOKING_BY_STATUS", param);
                if (ds != null)
                    return ds.Tables[0];
                else
                    return null;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    #endregion



    #region SET_Manual_Flight_Details New...

    public int SET_FlightManuals(string FromD, string ToD, string FromDt, string ToDt, int JTyhpe,
       int Adt, int Chi, int Inf, double basefare, double tax, double Markup, string Currency, string Carrier, string faretype)
    {
        SqlParameter[] param = new SqlParameter[15];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnectionFareManual())
            {

                if (!string.IsNullOrEmpty(FromD))
                {
                    param[0] = new SqlParameter("@From", SqlDbType.VarChar, 10);
                    param[0].Value = FromD;
                }

                if (!string.IsNullOrEmpty(ToD))
                {
                    param[1] = new SqlParameter("@To", SqlDbType.VarChar, 10);
                    param[1].Value = ToD;
                }
                if (!string.IsNullOrEmpty(FromDt))
                {
                    param[2] = new SqlParameter("@Fdate", SqlDbType.DateTime);
                    param[2].Value = FromDt;
                }
                if (!string.IsNullOrEmpty(ToDt))
                {
                    param[3] = new SqlParameter("@Tdate", SqlDbType.DateTime);
                    param[3].Value = ToDt;
                }
                if (!string.IsNullOrEmpty(JTyhpe.ToString()))
                {
                    param[4] = new SqlParameter("@Jtype", SqlDbType.Bit);
                    param[4].Value = JTyhpe;
                }
                if (!string.IsNullOrEmpty(Adt.ToString()))
                {
                    param[5] = new SqlParameter("@Adt", SqlDbType.Int);
                    param[5].Value = Adt;
                }
                if (!string.IsNullOrEmpty(Chi.ToString()))
                {
                    param[6] = new SqlParameter("@Chd", SqlDbType.Int);
                    param[6].Value = Chi;
                }
                if (!string.IsNullOrEmpty(Inf.ToString()))
                {
                    param[7] = new SqlParameter("@Inf", SqlDbType.Int);
                    param[7].Value = Inf;
                }
                if (!string.IsNullOrEmpty(basefare.ToString()))
                {
                    param[8] = new SqlParameter("@BaseFare", SqlDbType.Money);
                    param[8].Value = Convert.ToDecimal(basefare);
                }
                if (!string.IsNullOrEmpty(tax.ToString()))
                {
                    param[9] = new SqlParameter("@Tax", SqlDbType.Money);
                    param[9].Value = Convert.ToDecimal(tax);
                }
                if (!string.IsNullOrEmpty(tax.ToString()))
                {
                    param[10] = new SqlParameter("@Markup", SqlDbType.Money);
                    param[10].Value = Convert.ToDecimal(Markup);
                }

                if (!string.IsNullOrEmpty(Currency))
                {
                    param[11] = new SqlParameter("@Currency", SqlDbType.VarChar, 3);
                    param[11].Value = Currency;
                }
                if (!string.IsNullOrEmpty(Carrier))
                {
                    param[12] = new SqlParameter("@ValCarrier", SqlDbType.VarChar, 3);
                    param[12].Value = Carrier;
                }
                if (!string.IsNullOrEmpty(faretype))
                {
                    param[13] = new SqlParameter("@FareType", SqlDbType.VarChar, 50);
                    param[13].Value = faretype;
                }
                param[14] = new SqlParameter("@ID", SqlDbType.Int);
                param[14].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "SP_FlightManual", param);
                return Convert.ToInt32(param[14].Value);
            }
        }
        catch (Exception ex)
        {
            //string Pth = @"D:\Websites\TravelJunction.co.uk\TravelJunction_V2\App_Data\Errors\" + BM_BookingID + "-GetSetInDB.txt";
            //File.WriteAllText(Pth, Convert.ToString(ex.Message + "\\n\\t" + ex.StackTrace + "\\n\\t" + ex.Source));

            return 0;
        }
    }


    public DataTable GET_Flight_Manuals(string fromD, string toD, string airline)
    {
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnectionFareManual())
            {
                if (!string.IsNullOrEmpty(fromD))
                {
                    param[0] = new SqlParameter("@From", SqlDbType.VarChar, 3);
                    param[0].Value = fromD;
                }
                if (!string.IsNullOrEmpty(toD))
                {
                    param[0] = new SqlParameter("@To", SqlDbType.VarChar, 3);
                    param[0].Value = toD;
                }
                if (!string.IsNullOrEmpty(airline))
                {
                    param[0] = new SqlParameter("@ValCarrier", SqlDbType.VarChar, 2);
                    param[0].Value = airline;
                }

                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "SP_GET_FlightManual", param);
                return ds.Tables[0];
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    #endregion

    #region GET SET SAGE CREDENTIAL
    public int SET_SCredential(string statements, string CompName, string vendor)
    {
        int i = 0;
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {

                if (!string.IsNullOrEmpty(statements))
                {
                    param[0] = new SqlParameter("@Statement", SqlDbType.VarChar, 30);
                    param[0].Value = statements;
                }

                if (!string.IsNullOrEmpty(CompName))
                {
                    param[1] = new SqlParameter("@CompanyName", SqlDbType.VarChar, 30);
                    param[1].Value = CompName;
                }
                if (!string.IsNullOrEmpty(vendor))
                {
                    param[2] = new SqlParameter("@VName", SqlDbType.VarChar, 30);
                    param[2].Value = vendor;
                }

                i = SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_Sage_Credential", param);
            }

        }
        catch (Exception ex)
        {
            i = 0;
        }

        return i;
    }
    #endregion

    #region GET SET FOLLOWING DATE
    public int SET_Booking_Follow(string BookingID, string BookingRemarks, string BookingFollowTime, string BookingBy)
    {
        SqlParameter[] param = new SqlParameter[5];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                param[0] = new SqlParameter("@Statement", SqlDbType.VarChar, 50);
                param[0].Value = "INSERT";
                if (!string.IsNullOrEmpty(BookingID))
                {
                    param[1] = new SqlParameter("@BKG_FLW_Ref_No", SqlDbType.VarChar, 500);
                    param[1].Value = BookingID;
                }
                if (!string.IsNullOrEmpty(BookingRemarks))
                {
                    param[2] = new SqlParameter("@BKG_FLW_Remarks", SqlDbType.VarChar, 50);
                    param[2].Value = BookingRemarks;
                }
                if (!string.IsNullOrEmpty(BookingFollowTime))
                {
                    param[3] = new SqlParameter("@BKG_FLW_DateTime", SqlDbType.DateTime);
                    param[3].Value = Convert.ToDateTime(BookingFollowTime);
                }
                if (!string.IsNullOrEmpty(BookingBy))
                {
                    param[4] = new SqlParameter("@BKG_FLW_Remarks_By", SqlDbType.VarChar, 50);
                    param[4].Value = BookingBy;
                }


                int i = SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "USP_GETSET_BOK_Follow", param);
                return i;

            }
        }
        catch (Exception exx)
        {
            return 0;
        }
    }
    public DataTable GET_Booking_Follow(string BookingID)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            using (SqlConnection connection = DataConnection.GetConnection())
            {

                param[0] = new SqlParameter("@Statement", SqlDbType.VarChar, 50);
                param[0].Value = "SELECT";

                param[1] = new SqlParameter("@BKG_FLW_Ref_No", SqlDbType.VarChar, 50);
                param[1].Value = BookingID;


                DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "USP_GETSET_BOK_Follow", param);
                if (ds.Tables[0] != null)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
        }
        catch
        {

            return null;
        }
    }


    public int TruncateLog()
    {
        int i = 0;
        try
        {
            using (SqlConnection connection = DataConnection.GetConnectionLog())
            {
                i = SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "USP_CLEARLOG");
                return i;
            }
        }
        catch
        {

            return 0;
        }
    }


    public int HideDupe(string BookingID)
    {
        int i = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            using (SqlConnection connection = DataConnection.GetConnection())
            {
                param[0] = new SqlParameter("@BOK_MST_Booking_ID", SqlDbType.VarChar, 15);
                param[0].Value = BookingID;
                i = SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "USP_HideDupe", param);
                return i;
            }
        }
        catch
        {

            return 0;
        }
    }
    #endregion


    #region For UK Own Fare


    public bool CheckDiscountOwnFare_UK()
    {
        bool finalstatus = true;
        SqlConnection connection = DataConnection.GetConnection();
        try
        {
            connection.Open();
            SqlCommand command = new SqlCommand("select top 1 * from ChangeFare where isActive='true' and Change_Fare_Campaign in ('TJCA_JETC','C2BCA_JC','C2BCA','TRVJUNCTION_CA')", connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                finalstatus = true;
            }
            else
            {
                finalstatus = false;
            }
        }
        catch (Exception ex)
        {
            return true;
        }
        finally
        {
            connection.Close();
        }
        return finalstatus;
    }
    public string UpdateDiscountOwnFare_UK(bool UseStatus, string Origin, string Destination, string Airline, string AirlineClass,
       string TripType, string OfferType, string Pax, string FlightType, string Campaign)
    {
        try
        {
            using (SqlConnection connection = DataConnection.GetConnection())
            {
                //StringBuilder strcmd = new StringBuilder("UPDATE ChangeFare_US SET isActive = '" + UseStatus + "' Where Change_Fare_TripType = '" + TripType + "'");

                StringBuilder strcmd = new StringBuilder("UPDATE ChangeFare SET isActive = '" + UseStatus + "' where Change_Fare_Campaign in ('TJCA_JETC','C2BCA_JC','C2BCA','TRVJUNCTION_CA')");
                //if (Origin != "")
                //{
                //    strcmd.Append("and Change_Fare_Origin ='" + Origin.ToUpper() + "'");
                //}
                //if (Destination != "")
                //{
                //    strcmd.Append("and Change_Fare_Destination ='" + Destination.ToUpper() + "'");
                //}
                //if (Airline != "")
                //{
                //    strcmd.Append("and Change_Fare_Airline ='" + Airline.ToUpper() + "'");
                //}
                //if (OfferType != "")
                //{
                //    strcmd.Append("and Change_Fare_OfferType ='" + OfferType.ToUpper() + "'");
                //}
                //if (Campaign != "")
                //{
                //    strcmd.Append("and Change_Fare_Campaign ='" + Campaign.ToUpper() + "'");
                //}
                //if (Pax != "")
                //{
                //    strcmd.Append("and Change_Fare_No_Of_Pax ='" + Pax + "'");
                //}
                //if (FlightType != "")
                //{
                //    strcmd.Append("and Change_Fare_Flight_type ='" + FlightType + "'");
                //}
                if (SqlHelper.ExecuteNonQuery(connection, CommandType.Text, strcmd.ToString()) >= 1)
                {
                    return "true";
                }
                else
                {
                    return "false";
                }
            }
        }
        catch (Exception ex)
        {
            return "false";
        }


    }
    #endregion

    #region New Methods For US Own Fare

    public DataTable GET_ChangeFareUS(string SrNo, string Origin, string Destination, string FromDateStart, string FromDateEnd, string Airline, string CabinClass,
     string aClass, string TravelDate, string Counter, string ModifyBy, string ModifyDate, string Campaign, string journeyType, bool Active, int PaxCount, string DTD, string FlightType)
    {
        SqlParameter[] param = new SqlParameter[18];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnectionMarkupUS())
            {
                if (!string.IsNullOrEmpty(SrNo))
                {
                    param[0] = new SqlParameter("@ParamSrNo", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(SrNo);
                }
                if (!string.IsNullOrEmpty(Origin))
                {
                    param[1] = new SqlParameter("@ParamOrigin", SqlDbType.VarChar, 50);
                    param[1].Value = Origin;
                }
                if (!string.IsNullOrEmpty(Destination))
                {
                    param[2] = new SqlParameter("@ParamDestination", SqlDbType.VarChar, 50);
                    param[2].Value = Destination;
                }
                if (!string.IsNullOrEmpty(FromDateStart))
                {
                    param[3] = new SqlParameter("@ParamFromDateStart", SqlDbType.DateTime);
                    param[3].Value = Convert.ToDateTime(FromDateStart);
                }
                if (!string.IsNullOrEmpty(FromDateEnd))
                {
                    param[4] = new SqlParameter("@ParamFromDateEnd", SqlDbType.DateTime);
                    param[4].Value = Convert.ToDateTime(FromDateEnd);
                }
                if (!string.IsNullOrEmpty(Airline))
                {
                    param[5] = new SqlParameter("@ParamAirline", SqlDbType.VarChar, 50);
                    param[5].Value = Airline;
                }
                if (!string.IsNullOrEmpty(CabinClass))
                {
                    param[6] = new SqlParameter("@ParamCabinClass", SqlDbType.VarChar, 50);
                    param[6].Value = CabinClass;
                }
                if (!string.IsNullOrEmpty(aClass))
                {
                    param[7] = new SqlParameter("@ParamClass", SqlDbType.VarChar, 50);
                    param[7].Value = aClass;
                }
                if (!string.IsNullOrEmpty(TravelDate))
                {
                    param[8] = new SqlParameter("@ParamTravelDate", SqlDbType.DateTime);
                    param[8].Value = Convert.ToDateTime(TravelDate);
                }
                param[9] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[9].Value = Counter;

                if (!string.IsNullOrEmpty(ModifyBy))
                {
                    param[10] = new SqlParameter("@ParamModifyBy", SqlDbType.VarChar, 50);
                    param[10].Value = ModifyBy;
                }
                if (!string.IsNullOrEmpty(ModifyDate))
                {
                    param[11] = new SqlParameter("@ParamModifyDate", SqlDbType.DateTime);
                    param[11].Value = Convert.ToDateTime(ModifyDate);
                }
                if (!string.IsNullOrEmpty(Campaign))
                {
                    param[12] = new SqlParameter("@ParamCampaign", SqlDbType.VarChar, 100);
                    param[12].Value = Campaign;
                }
                if (!string.IsNullOrEmpty(journeyType))
                {
                    param[13] = new SqlParameter("@ParamTripType", SqlDbType.VarChar, 10);
                    param[13].Value = journeyType;
                }
                if (Active)
                {
                    param[14] = new SqlParameter("@ParamActive", SqlDbType.Bit);
                    param[14].Value = 1;
                }
                else
                {
                    param[14] = new SqlParameter("@ParamActive", SqlDbType.Bit);
                    param[14].Value = 0;
                }
                param[15] = new SqlParameter("@paramPaxCount", SqlDbType.Int, 10);
                param[15].Value = PaxCount;


                if (!string.IsNullOrEmpty(DTD))
                {
                    param[16] = new SqlParameter("@paramDTD", SqlDbType.Int, 10);
                    param[16].Value = Convert.ToInt32(DTD);
                }
                else
                {
                    param[16] = new SqlParameter("@paramDTD", SqlDbType.Int, 10);
                    param[16].Value = Convert.ToInt32("0");
                }


                param[17] = new SqlParameter("@ParamFlightType", SqlDbType.VarChar, 10);
                param[17].Value = FlightType;


                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_ChangeFare_US", param);
                return ds.Tables[0];
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public string SET_ChangeFareUS(string SrNo, string Origin, string Destination, string FromDateStart, string FromDateEnd, string Airline, string CabinClass,
        string aClass, string BaseFare, string Tax, string Commission, string ModifyBy, string Counter, string Campaign,
        string offerType, string journeyType, string active, int PaxCount, string flightType, int DTD, string MarkUp)
    {
        string[] classes = aClass.Split(',');
        foreach (string cls in classes)
        {
            SqlParameter[] param = new SqlParameter[23];
            try
            {
                using (SqlConnection conection = DataConnection.GetConnectionMarkupUS())
                {

                    if (!string.IsNullOrEmpty(SrNo))
                    {
                        param[0] = new SqlParameter("@ParamSrNo", SqlDbType.Int);
                        param[0].Value = Convert.ToInt32(SrNo);
                    }
                    if (!string.IsNullOrEmpty(Origin))
                    {
                        param[1] = new SqlParameter("@ParamOrigin", SqlDbType.VarChar, 50);
                        param[1].Value = Origin;
                    }
                    if (!string.IsNullOrEmpty(Destination))
                    {
                        param[2] = new SqlParameter("@ParamDestination", SqlDbType.VarChar, 50);
                        param[2].Value = Destination;
                    }
                    if (!string.IsNullOrEmpty(FromDateStart))
                    {
                        param[3] = new SqlParameter("@ParamFromDateStart", SqlDbType.DateTime);
                        param[3].Value = Convert.ToDateTime(FromDateStart);
                    }
                    if (!string.IsNullOrEmpty(FromDateEnd))
                    {
                        param[4] = new SqlParameter("@ParamFromDateEnd", SqlDbType.DateTime);
                        param[4].Value = Convert.ToDateTime(FromDateEnd);
                    }
                    if (!string.IsNullOrEmpty(Airline))
                    {
                        param[5] = new SqlParameter("@ParamAirline", SqlDbType.VarChar, 50);
                        param[5].Value = Airline;
                    }
                    if (!string.IsNullOrEmpty(CabinClass))
                    {
                        param[6] = new SqlParameter("@ParamCabinClass", SqlDbType.VarChar, 50);
                        param[6].Value = CabinClass;
                    }
                    if (!string.IsNullOrEmpty(cls))
                    {
                        param[7] = new SqlParameter("@ParamClass", SqlDbType.VarChar, 50);
                        param[7].Value = cls;
                    }
                    if (!string.IsNullOrEmpty(BaseFare))
                    {
                        param[8] = new SqlParameter("@ParamBaseFare", SqlDbType.Money);
                        param[8].Value = Convert.ToDouble(BaseFare);
                    }
                    if (!string.IsNullOrEmpty(Tax))
                    {
                        param[9] = new SqlParameter("@ParamTax", SqlDbType.Money);
                        param[9].Value = Convert.ToDouble(Tax);
                    }

                    if (!string.IsNullOrEmpty(Commission))
                    {
                        param[10] = new SqlParameter("@ParamCommission", SqlDbType.Money);
                        param[10].Value = Convert.ToDouble(Commission);
                    }
                    if (!string.IsNullOrEmpty(ModifyBy))
                    {
                        param[11] = new SqlParameter("@ParamModifyBy", SqlDbType.VarChar, 50);
                        param[11].Value = ModifyBy;
                    }
                    param[12] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                    param[12].Value = Counter;

                    param[13] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 500);
                    param[13].Direction = ParameterDirection.Output;

                    if (!string.IsNullOrEmpty(Campaign))
                    {
                        param[14] = new SqlParameter("@ParamCampaign", SqlDbType.VarChar, 100);
                        param[14].Value = Campaign;
                    }
                    if (!string.IsNullOrEmpty(offerType))
                    {
                        param[15] = new SqlParameter("@ParamOfferType", SqlDbType.VarChar, 50);
                        param[15].Value = offerType;
                    }
                    if (!string.IsNullOrEmpty(journeyType))
                    {
                        param[16] = new SqlParameter("@ParamTripType", SqlDbType.VarChar, 10);
                        param[16].Value = journeyType;
                    }
                    if (active.ToLower() == "true" || active.ToLower() == "false")
                    {

                        if (active.ToLower() == "true")
                        {
                            param[17] = new SqlParameter("@ParamActive", SqlDbType.Bit);
                            param[17].Value = 1;
                        }
                        else
                        {
                            param[17] = new SqlParameter("@ParamActive", SqlDbType.Bit);
                            param[17].Value = 0;
                        }
                    }

                    param[18] = new SqlParameter("@paramPaxCount", SqlDbType.Int, 10);
                    param[18].Value = PaxCount;

                    param[19] = new SqlParameter("@ParamModifyDate", SqlDbType.DateTime, 40);
                    param[19].Value = DateTime.Now;

                    param[20] = new SqlParameter("@ParamFlightType", SqlDbType.VarChar, 50);
                    param[20].Value = flightType;

                    param[21] = new SqlParameter("@ParamDTD", SqlDbType.Int, 10);
                    param[21].Value = DTD;

                    param[22] = new SqlParameter("@ParamMarkup", SqlDbType.Decimal, 10);
                    param[22].Value = MarkUp;

                    SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_ChangeFare_US", param);
                    string output = param[13].Value.ToString();
                    if (output != "true")
                    {
                        return output;
                    }
                }
            }
            catch (Exception ex)
            {
                return "false";
            }
        }
        return "true";
    }


    public string DeleteOwnFaresUS(string ID, string Counter)
    {
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnectionMarkupUS())
            {
                if (!string.IsNullOrEmpty(ID))
                {
                    param[0] = new SqlParameter("@ParamSrNo", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(ID);
                }

                param[1] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[1].Value = Counter;

                param[2] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 500);
                param[2].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_ChangeFare_US", param);
                return param[2].Value.ToString();
            }
        }
        catch
        {
            return "false";
        }
    }


    public string UpdateDiscountOwnFare_US(bool UseStatus, string Origin, string Destination, string Airline, string AirlineClass,
        string TripType, string OfferType, string Pax, string FlightType, string Campaign)
    {
        try
        {
            using (SqlConnection connection = DataConnection.GetConnectionMarkupUS())
            {
                //StringBuilder strcmd = new StringBuilder("UPDATE ChangeFare_US SET isActive = '" + UseStatus + "' Where Change_Fare_TripType = '" + TripType + "'");

                StringBuilder strcmd = new StringBuilder("UPDATE ChangeFare_US SET isActive = '" + UseStatus + "'");
                //if (Origin != "")
                //{
                //    strcmd.Append("and Change_Fare_Origin ='" + Origin.ToUpper() + "'");
                //}
                //if (Destination != "")
                //{
                //    strcmd.Append("and Change_Fare_Destination ='" + Destination.ToUpper() + "'");
                //}
                //if (Airline != "")
                //{
                //    strcmd.Append("and Change_Fare_Airline ='" + Airline.ToUpper() + "'");
                //}
                //if (OfferType != "")
                //{
                //    strcmd.Append("and Change_Fare_OfferType ='" + OfferType.ToUpper() + "'");
                //}
                //if (Campaign != "")
                //{
                //    strcmd.Append("and Change_Fare_Campaign ='" + Campaign.ToUpper() + "'");
                //}
                //if (Pax != "")
                //{
                //    strcmd.Append("and Change_Fare_No_Of_Pax ='" + Pax + "'");
                //}
                //if (FlightType != "")
                //{
                //    strcmd.Append("and Change_Fare_Flight_type ='" + FlightType + "'");
                //}
                if (SqlHelper.ExecuteNonQuery(connection, CommandType.Text, strcmd.ToString()) >= 1)
                {
                    return "true";
                }
                else
                {
                    return "false";
                }
            }
        }
        catch (Exception ex)
        {
            return "false";
        }


    }



    public bool CheckDiscountOwnFare_US()
    {
        bool finalstatus = true;
        SqlConnection connection = DataConnection.GetConnectionMarkupUS();
        try
        {
            connection.Open();
            SqlCommand command = new SqlCommand("select top 1 * from ChangeFare_US where isActive='true'", connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                finalstatus = true;
            }
            else
            {
                finalstatus = false;
            }
        }
        catch (Exception ex)
        {
            return true;
        }
        finally
        {
            connection.Close();
        }
        return finalstatus;
    }


    public string UpdateOwnFare_US_Excel(string ID, string UpdateField, string Value, string UpdetedBy)
    {
        try
        {
            string FieldName = "";
            string result = string.Empty;
            switch (UpdateField)
            {
                case "BaseFare": FieldName = "Change_Fare_BaseFare"; break;
                case "Tax": FieldName = "Change_Fare_Tax"; break;
                case "Markup": FieldName = "Change_Fare_Markup"; break;
                case "Commission": FieldName = "Change_Fare_Commission"; break;
                case "FromDateStart": FieldName = "Change_Fare_FromDateStart"; break;
                case "FromDateEnd": FieldName = "Change_Fare_FromDateEnd"; break;
                case "Active": FieldName = "isActive"; break;
                case "FlightType": FieldName = "Change_Fare_Flight_type"; break;
                case "Airline": FieldName = "Change_Fare_Airline"; break;
                case "Origin": FieldName = "Change_Fare_Origin"; break;
                case "Destination": FieldName = "Change_Fare_Destination"; break;
                case "AirlineClass": FieldName = "Change_Fare_Class"; break;
                case "OfferType": FieldName = "Change_Fare_OfferType"; break;
                case "TripType": FieldName = "Change_Fare_TripType"; break;
                case "PaxCount": FieldName = "Change_Fare_No_Of_Pax"; break;
                case "DTD": FieldName = "Change_Fare_Days_To_Departure"; break;
            }
            string Query = "";

            if (UpdateField == "FromDateStart" || UpdateField == "FromDateEnd")
            {
                DateTime dt;

                if (DateTime.TryParse(Value, out dt))
                {
                    Query = "UPDATE ChangeFare_US set " + FieldName + "='" + Convert.ToDateTime(Value).ToString("yyyy-MM-dd") + "'," +
                        "Change_Fare_ModifyBy='" + UpdetedBy + "'," +
                        "Change_Fare_ModifyDateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                        "Where Change_Fare_SrNo=" + ID;
                }
                else
                {
                    result = "Invalid date format";
                }
            }
            else
            {
                Query = "UPDATE ChangeFare_US set " + FieldName + "='" + Value.ToUpper() + "'," +
                    "Change_Fare_ModifyBy='" + UpdetedBy + "'," +
                    "Change_Fare_ModifyDateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                    "Where Change_Fare_SrNo=" + ID;
            }

            using (SqlConnection connection = DataConnection.GetConnectionMarkupUS())
            {
                if (SqlHelper.ExecuteNonQuery(connection, CommandType.Text, Query) == 1)
                {
                    result = "true";
                }
                else
                {
                    result = "false";
                }
            }
            return result;
        }
        catch (Exception ex)
        {
            return "false";
        }
    }
    #endregion

    #region New Methods for Rule_FT

    public string SET_RuleFTDetail(string ID, string From, string To, string AirV, string Provider, string Category,
        string CClass, string FareType, string Amount, string AmountType, string ValidFromDate, string ValidToDate,
        string CompanyID, string CampID, string JourneyType, string ModifiedBy, string Counter)
    {
        SqlParameter[] param = new SqlParameter[18];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnectionMarkup())
            {

                if (!string.IsNullOrEmpty(ID))
                {
                    param[0] = new SqlParameter("@ParamID", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(ID);
                }

                param[1] = new SqlParameter("@ParamFrom", SqlDbType.VarChar, 50);
                param[1].Value = string.IsNullOrEmpty(From) ? "ANY" : From.ToUpper();

                param[2] = new SqlParameter("@ParamTo", SqlDbType.VarChar, 50);
                param[2].Value = string.IsNullOrEmpty(To) ? "ANY" : To.ToUpper();

                param[3] = new SqlParameter("@ParamAirV", SqlDbType.VarChar, 50);
                param[3].Value = string.IsNullOrEmpty(AirV) ? "ANY" : AirV.ToUpper();

                param[4] = new SqlParameter("@ParamProvider", SqlDbType.VarChar, 50);
                param[4].Value = string.IsNullOrEmpty(Provider) ? "ANY" : Provider.ToUpper();

                param[5] = new SqlParameter("@ParamCategory", SqlDbType.VarChar, 50);
                param[5].Value = string.IsNullOrEmpty(Category) ? "ANY" : Category.ToUpper();

                param[6] = new SqlParameter("@ParamClass", SqlDbType.VarChar, 50);
                param[6].Value = string.IsNullOrEmpty(CClass) ? "ANY" : CClass.ToUpper();

                param[7] = new SqlParameter("@ParamFareType", SqlDbType.VarChar, 50);
                param[7].Value = string.IsNullOrEmpty(FareType) ? "ANY" : FareType.ToUpper();

                param[8] = new SqlParameter("@ParamAmount", SqlDbType.Money);
                param[8].Value = Convert.ToDouble(Amount);

                param[9] = new SqlParameter("@ParamAmountType", SqlDbType.VarChar, 50);
                param[9].Value = AmountType.ToUpper();

                param[10] = new SqlParameter("@ParamValidFromDate", SqlDbType.DateTime);
                param[10].Value = string.IsNullOrEmpty(ValidFromDate) ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(ValidFromDate);

                param[11] = new SqlParameter("@ParamValidToDate", SqlDbType.DateTime);
                param[11].Value = string.IsNullOrEmpty(ValidToDate) ? Convert.ToDateTime("01-01-2100") : Convert.ToDateTime(ValidToDate);

                param[12] = new SqlParameter("@ParamCompanyID", SqlDbType.VarChar, 50);
                param[12].Value = CompanyID;

                param[13] = new SqlParameter("@ParamCampID", SqlDbType.VarChar, 50);
                param[13].Value = string.IsNullOrEmpty(CampID) ? "ANY" : CampID;

                param[14] = new SqlParameter("@ParamJourneyType", SqlDbType.VarChar, 50);
                param[14].Value = string.IsNullOrEmpty(JourneyType) ? "ANY" : JourneyType.ToUpper();

                param[15] = new SqlParameter("@ParamModifiedBy", SqlDbType.VarChar, 50);
                param[15].Value = string.IsNullOrEmpty(ModifiedBy) ? "" : ModifiedBy;

                param[16] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[16].Value = Counter;

                param[17] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 500);
                param[17].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_RULES_FT_Detail", param);
                return param[17].Value.ToString();
            }
        }
        catch
        {
            return "false";
        }
    }

    public string DeleteRulesFT(string ID, string Counter)
    {
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnectionMarkup())
            {

                if (!string.IsNullOrEmpty(ID))
                {
                    param[0] = new SqlParameter("@ParamID", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(ID);
                }

                param[1] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[1].Value = Counter;

                param[2] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 500);
                param[2].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_RULES_FT_Detail", param);
                return param[2].Value.ToString();
            }
        }
        catch
        {
            return "false";
        }
    }

    public string UpdateRulesFTExcel(string ID, string UpdateField, string Value, string UpdetedBy)
    {
        try
        {
            string FieldName = "";
            switch (UpdateField)
            {
                case "RulesFrom": FieldName = "Rules_FT_From"; break;
                case "RulesTo": FieldName = "Rules_FT_To"; break;
                case "AirV": FieldName = "Rules_FT_AirV"; break;
                case "RulesClass": FieldName = "Rules_FT_Class"; break;
                case "JourneyType": FieldName = "Rules_FT_JourneyType"; break;
                case "Amount": FieldName = "Rules_FT_Amount"; break;
                case "AmountType": FieldName = "Rules_FT_Amount_Type"; break;
                case "ValidFromDate": FieldName = "Rules_FT_ValidFromDate"; break;
                case "ValidToDate": FieldName = "Rules_FT_ValidToDate"; break;


                case "Provider": FieldName = "Rules_FT_Provider"; break;
                case "Category": FieldName = "Rules_FT_Category"; break;
                case "FareType": FieldName = "Rules_FT_Fare_Type"; break;
                    //case "DaysToDeparture": FieldName = "DaysToDeparture"; break;
                    //case "PaxCount": FieldName = "Agnt_AirF_No_Of_Pax"; break;

            }
            string Query = "";
            if (UpdateField == "Amount")
            {
                Query = "update Rules_FT set " + FieldName + "=" + Value.ToUpper() + ",Rules_FT_ModifiedBy='" + UpdetedBy + "',Rules_FT_LastModifiedDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' Where Rules_FT_ID=" + ID;
            }
            else
            {
                if (UpdateField == "ValidFromDate" || UpdateField == "ValidToDate")
                {
                    Query = "update Rules_FT set " + FieldName + "='" + Convert.ToDateTime(Value).ToString("yyyy-MM-dd") + "',Rules_FT_ModifiedBy='" + UpdetedBy + "',Rules_FT_LastModifiedDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' Where Rules_FT_ID=" + ID;
                }
                else
                {
                    Query = "update Rules_FT set " + FieldName + "='" + Value.ToUpper() + "',Rules_FT_ModifiedBy='" + UpdetedBy + "',Rules_FT_LastModifiedDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' Where Rules_FT_ID=" + ID;
                }
            }
            using (SqlConnection connection = DataConnection.GetConnectionMarkup())
            {
                if (SqlHelper.ExecuteNonQuery(connection, CommandType.Text, Query) == 1)
                {
                    return "true";
                }
                else
                    return "false";

            }
        }
        catch (Exception ex) { return "false"; }
    }

    public DataTable GET_RulesFTDetail(string ID, string From, string To, string AirV, string Provider, string Category,
     string CClass, string FareType, string CompanyID, string CampID, string JourneyType, string RollName, string PageName, string AmountType)
    {
        SqlParameter[] param = new SqlParameter[15];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnectionMarkup())
            {
                if (!string.IsNullOrEmpty(ID))
                {
                    param[0] = new SqlParameter("@ParamID", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(ID);
                }
                if (!string.IsNullOrEmpty(From))
                {
                    param[1] = new SqlParameter("@ParamFrom", SqlDbType.VarChar, 50);
                    param[1].Value = From.ToUpper();
                }
                if (!string.IsNullOrEmpty(To))
                {
                    param[2] = new SqlParameter("@ParamTo", SqlDbType.VarChar, 50);
                    param[2].Value = To.ToUpper();
                }
                if (!string.IsNullOrEmpty(AirV))
                {
                    param[3] = new SqlParameter("@ParamAirV", SqlDbType.VarChar, 50);
                    param[3].Value = AirV.ToUpper();
                }
                if (!string.IsNullOrEmpty(Provider))
                {
                    param[4] = new SqlParameter("@ParamProvider", SqlDbType.VarChar, 50);
                    param[4].Value = Provider.ToUpper();
                }
                if (!string.IsNullOrEmpty(Category))
                {
                    param[5] = new SqlParameter("@ParamCategory", SqlDbType.VarChar, 50);
                    param[5].Value = Category.ToUpper();
                }
                if (!string.IsNullOrEmpty(CClass))
                {
                    param[6] = new SqlParameter("@ParamClass", SqlDbType.VarChar, 50);
                    param[6].Value = CClass.ToUpper();
                }
                if (!string.IsNullOrEmpty(FareType))
                {
                    param[7] = new SqlParameter("@ParamFareType", SqlDbType.VarChar, 50);
                    param[7].Value = FareType.ToUpper();
                }
                if (!string.IsNullOrEmpty(CompanyID))
                {
                    param[8] = new SqlParameter("@ParamCompanyID", SqlDbType.VarChar);
                    param[8].Value = CompanyID;
                }
                if (!string.IsNullOrEmpty(CampID))
                {
                    param[9] = new SqlParameter("@ParamCampID", SqlDbType.VarChar, 50);
                    param[9].Value = CampID;
                }
                if (!string.IsNullOrEmpty(JourneyType))
                {
                    param[10] = new SqlParameter("@ParamJourneyType", SqlDbType.VarChar, 50);
                    param[10].Value = JourneyType.ToUpper();
                }
                if (!string.IsNullOrEmpty(RollName))
                {
                    param[11] = new SqlParameter("@ParamRollName", SqlDbType.NVarChar, 50);
                    param[11].Value = RollName;
                }
                if (!string.IsNullOrEmpty(PageName))
                {
                    param[12] = new SqlParameter("@ParamPageName", SqlDbType.NVarChar, 500);
                    param[12].Value = PageName;
                }
                if (!string.IsNullOrEmpty(AmountType))
                {
                    param[13] = new SqlParameter("@ParamAmountType", SqlDbType.NVarChar, 500);
                    param[13].Value = AmountType;
                }
                param[14] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[14].Value = "Select";

                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_RULES_FT_Detail", param);
                return ds.Tables[0];
            }
        }
        catch
        {
            return null;
        }
    }


    public bool SET_FlightRuleDetail(string Counter, string rules_ID)
    {
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnectionMarkup())
            {
                param[0] = new SqlParameter("@Counter", SqlDbType.VarChar, 500)
                {
                    Value = Counter
                };

                if (!string.IsNullOrEmpty(rules_ID))
                {
                    param[1] = new SqlParameter("@ParamID", SqlDbType.VarChar, 500)
                    {
                        Value = rules_ID
                    };
                }
                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_RULES_FT_Detail", param);
                return true;
            }
        }
        catch
        {
            return false;
        }
    }


    public string UpdateDisCountRules_FT(bool UseStatus)
    {
        string finalstatus = string.Empty;
        SqlConnection connection = DataConnection.GetConnectionMarkup();
        try
        {
            connection.Open();



            string query = "Update Rules_FT set isActive='" + UseStatus + "' where Rules_FT_Amount < 0";
            SqlCommand command = new SqlCommand(query, connection);
            int i = command.ExecuteNonQuery();

            if (i > 0)
            {
                finalstatus = "true";
            }
            else
            {
                finalstatus = "false";
            }
        }
        catch (Exception ex)
        {
            finalstatus = "false";
        }
        finally
        {
            connection.Close();
        }
        return finalstatus;
    }

    public bool CheckDisCountRules_FT()
    {
        bool finalstatus = true;
        SqlConnection connection = DataConnection.GetConnectionMarkup();
        try
        {
            connection.Open();
            SqlCommand command = new SqlCommand("select top 1 * from Rules_FT where isActive='true' and Rules_FT_Amount<0", connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                finalstatus = true;
            }
            else
            {
                finalstatus = false;
            }
        }
        catch (Exception ex)
        {
            return true;
        }
        finally
        {
            connection.Close();
        }
        return finalstatus;
    }


    public string UpdateOwnFare_FT_Excel(string ID, string UpdateField, string Value, string UpdetedBy)
    {
        try
        {
            string FieldName = "";
            string result = string.Empty;
            switch (UpdateField)
            {
                case "BaseFare": FieldName = "Change_Fare_BaseFare"; break;
                case "Tax": FieldName = "Change_Fare_Tax"; break;
                case "Markup": FieldName = "Change_Fare_Markup"; break;
                case "Commission": FieldName = "Change_Fare_Commission"; break;
                case "FromDateStart": FieldName = "Change_Fare_FromDateStart"; break;
                case "FromDateEnd": FieldName = "Change_Fare_FromDateEnd"; break;
                case "Active": FieldName = "isActive"; break;
                case "FlightType": FieldName = "Change_Fare_Flight_type"; break;
                case "Airline": FieldName = "Change_Fare_Airline"; break;
                case "Origin": FieldName = "Change_Fare_Origin"; break;
                case "Destination": FieldName = "Change_Fare_Destination"; break;
                case "AirlineClass": FieldName = "Change_Fare_Class"; break;
                case "OfferType": FieldName = "Change_Fare_OfferType"; break;
                case "TripType": FieldName = "Change_Fare_TripType"; break;
            }
            string Query = "";

            if (UpdateField == "FromDateStart" || UpdateField == "FromDateEnd")
            {
                DateTime dt;

                if (DateTime.TryParse(Value, out dt))
                {
                    Query = "UPDATE ChangeFare_FT set " + FieldName + "='" + Convert.ToDateTime(Value).ToString("yyyy-MM-dd") + "'," +
                        "Change_Fare_ModifyBy='" + UpdetedBy + "'," +
                        "Change_Fare_ModifyDateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                        "Where Change_Fare_SrNo=" + ID;
                }
                else
                {
                    result = "Invalid date format";
                }
            }
            else
            {
                Query = "UPDATE ChangeFare_FT set " + FieldName + "='" + Value.ToUpper() + "'," +
                    "Change_Fare_ModifyBy='" + UpdetedBy + "'," +
                    "Change_Fare_ModifyDateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                    "Where Change_Fare_SrNo=" + ID;
            }

            using (SqlConnection connection = DataConnection.GetConnection())
            {
                if (SqlHelper.ExecuteNonQuery(connection, CommandType.Text, Query) == 1)
                {
                    result = "true";
                }
                else
                {
                    result = "false";
                }
            }
            return result;
        }
        catch (Exception ex)
        {
            return "false";
        }
    }

    #endregion

    #region Get Ip Address Based on Booking ID 

    public DataTable GET_SET_IPAddress(string BookingID, string Counter, string IpCity, string IpCountry)
    {
        SqlParameter[] param = new SqlParameter[4];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(BookingID))
                {
                    param[0] = new SqlParameter("@ParamBookingID", SqlDbType.VarChar, 50);
                    param[0].Value = BookingID;
                }

                param[1] = new SqlParameter("@ParamCounter", SqlDbType.VarChar, 50);
                param[1].Value = Counter;

                if (!string.IsNullOrEmpty(IpCity))
                {
                    param[2] = new SqlParameter("@ParamIpCity", SqlDbType.VarChar, 50);
                    param[2].Value = IpCity;
                }
                if (!string.IsNullOrEmpty(IpCountry))
                {
                    param[3] = new SqlParameter("@ParamIpCountry", SqlDbType.VarChar, 50);
                    param[3].Value = IpCountry;
                }
                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_IP_ADDRESS_DETAIL", param);
                return ds.Tables[0];
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    #endregion


    #region Own  Fares For Flight trotters

    public string SET_OwnFares_FT(string SrNo, string Origin, string Destination, string FromDateStart, string FromDateEnd, string Airline, string CabinClass,
        string aClass, string BaseFare, string Tax, string Markup, string MarkupTJ, string MarkupXpt, string Commission,
        string ModifyBy, string Counter, string Campaign, string offerType, string journeyType, string active, string FlightType, string modifiedDate)
    {
        string[] classes = aClass.Split(',');
        foreach (string cls in classes)
        {
            SqlParameter[] param = new SqlParameter[20];
            try
            {
                using (SqlConnection conection = DataConnection.GetConnection())
                {

                    if (!string.IsNullOrEmpty(SrNo))
                    {
                        param[0] = new SqlParameter("@ParamSrNo", SqlDbType.Int);
                        param[0].Value = Convert.ToInt32(SrNo);
                    }
                    if (!string.IsNullOrEmpty(Origin))
                    {
                        param[1] = new SqlParameter("@ParamOrigin", SqlDbType.VarChar, 50);
                        param[1].Value = Origin;
                    }
                    if (!string.IsNullOrEmpty(Destination))
                    {
                        param[2] = new SqlParameter("@ParamDestination", SqlDbType.VarChar, 50);
                        param[2].Value = Destination;
                    }
                    if (!string.IsNullOrEmpty(FromDateStart))
                    {
                        param[3] = new SqlParameter("@ParamFromDateStart", SqlDbType.DateTime);
                        param[3].Value = Convert.ToDateTime(FromDateStart);
                    }
                    if (!string.IsNullOrEmpty(FromDateEnd))
                    {
                        param[4] = new SqlParameter("@ParamFromDateEnd", SqlDbType.DateTime);
                        param[4].Value = Convert.ToDateTime(FromDateEnd);
                    }
                    if (!string.IsNullOrEmpty(Airline))
                    {
                        param[5] = new SqlParameter("@ParamAirline", SqlDbType.VarChar, 50);
                        param[5].Value = Airline;
                    }
                    if (!string.IsNullOrEmpty(CabinClass))
                    {
                        param[6] = new SqlParameter("@ParamCabinClass", SqlDbType.VarChar, 50);
                        param[6].Value = CabinClass;
                    }
                    if (!string.IsNullOrEmpty(cls))
                    {
                        param[7] = new SqlParameter("@ParamClass", SqlDbType.VarChar, 50);
                        param[7].Value = cls;
                    }
                    if (!string.IsNullOrEmpty(BaseFare))
                    {
                        param[8] = new SqlParameter("@ParamBaseFare", SqlDbType.Money);
                        param[8].Value = Convert.ToDouble(BaseFare);
                    }
                    if (!string.IsNullOrEmpty(Tax))
                    {
                        param[9] = new SqlParameter("@ParamTax", SqlDbType.Money);
                        param[9].Value = Convert.ToDouble(Tax);
                    }
                    if (!string.IsNullOrEmpty(Markup))
                    {
                        param[10] = new SqlParameter("@ParamMarkup", SqlDbType.Money);
                        param[10].Value = Convert.ToDouble(Markup);
                    }

                    if (!string.IsNullOrEmpty(Commission))
                    {
                        param[11] = new SqlParameter("@ParamCommission", SqlDbType.Money);
                        param[11].Value = Convert.ToDouble(Commission);
                    }
                    if (!string.IsNullOrEmpty(ModifyBy))
                    {
                        param[12] = new SqlParameter("@ParamModifyBy", SqlDbType.VarChar, 50);
                        param[12].Value = ModifyBy;
                    }
                    param[13] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                    param[13].Value = Counter;

                    param[14] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 500);
                    param[14].Direction = ParameterDirection.Output;

                    if (!string.IsNullOrEmpty(Campaign))
                    {
                        param[15] = new SqlParameter("@ParamCampaign", SqlDbType.VarChar, 100);
                        param[15].Value = Campaign;
                    }
                    if (!string.IsNullOrEmpty(offerType))
                    {
                        param[16] = new SqlParameter("@ParamOfferType", SqlDbType.VarChar, 50);
                        param[16].Value = offerType;
                    }
                    if (!string.IsNullOrEmpty(journeyType))
                    {
                        param[17] = new SqlParameter("@ParamTripType", SqlDbType.VarChar, 10);
                        param[17].Value = journeyType;
                    }
                    if (active.ToLower() == "true" || active.ToLower() == "false")
                    {

                        if (active.ToLower() == "true")
                        {
                            param[18] = new SqlParameter("@ParamActive", SqlDbType.Bit);
                            param[18].Value = 1;
                        }
                        else
                        {
                            param[18] = new SqlParameter("@ParamActive", SqlDbType.Bit);
                            param[18].Value = 0;
                        }
                    }

                    if (!string.IsNullOrEmpty(FlightType))
                    {
                        param[19] = new SqlParameter("@ParamFlightType", SqlDbType.VarChar, 50);
                        param[19].Value = FlightType;
                    }

                    if (!string.IsNullOrEmpty(modifiedDate))
                    {
                        param[20] = new SqlParameter("@ParamModifyDate", SqlDbType.DateTime);
                        param[20].Value = Convert.ToDateTime(modifiedDate);
                    }

                    SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_ChangeFare_FT", param);
                    string output = param[14].Value.ToString();
                    if (output != "true")
                        return output;
                }

            }
            catch (Exception ex)
            {
                return "false";
            }
        }
        return "true";
    }

    public DataTable GET_OwnFares_FT(string SrNo, string Origin, string Destination, string FromDateStart, string FromDateEnd, string Airline, string CabinClass,
        string aClass, string TravelDate, string Counter, string ModifyBy, string ModifyDate, string Campaign, string journeyType, bool Active, string flightType)
    {
        SqlParameter[] param = new SqlParameter[18];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(SrNo))
                {
                    param[0] = new SqlParameter("@ParamSrNo", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(SrNo);
                }
                if (!string.IsNullOrEmpty(Origin))
                {
                    param[1] = new SqlParameter("@ParamOrigin", SqlDbType.VarChar, 50);
                    param[1].Value = Origin;
                }
                if (!string.IsNullOrEmpty(Destination))
                {
                    param[2] = new SqlParameter("@ParamDestination", SqlDbType.VarChar, 50);
                    param[2].Value = Destination;
                }
                if (!string.IsNullOrEmpty(FromDateStart))
                {
                    param[3] = new SqlParameter("@ParamFromDateStart", SqlDbType.DateTime);
                    param[3].Value = Convert.ToDateTime(FromDateStart);
                }
                if (!string.IsNullOrEmpty(FromDateEnd))
                {
                    param[4] = new SqlParameter("@ParamFromDateEnd", SqlDbType.DateTime);
                    param[4].Value = Convert.ToDateTime(FromDateEnd);
                }
                if (!string.IsNullOrEmpty(Airline))
                {
                    param[5] = new SqlParameter("@ParamAirline", SqlDbType.VarChar, 50);
                    param[5].Value = Airline;
                }
                if (!string.IsNullOrEmpty(CabinClass))
                {
                    param[6] = new SqlParameter("@ParamCabinClass", SqlDbType.VarChar, 50);
                    param[6].Value = CabinClass;
                }
                if (!string.IsNullOrEmpty(aClass))
                {
                    param[7] = new SqlParameter("@ParamClass", SqlDbType.VarChar, 50);
                    param[7].Value = aClass;
                }
                if (!string.IsNullOrEmpty(TravelDate))
                {
                    param[8] = new SqlParameter("@ParamTravelDate", SqlDbType.DateTime);
                    param[8].Value = Convert.ToDateTime(TravelDate);
                }
                param[9] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[9].Value = Counter;

                if (!string.IsNullOrEmpty(ModifyBy))
                {
                    param[10] = new SqlParameter("@ParamModifyBy", SqlDbType.VarChar, 50);
                    param[10].Value = ModifyBy;
                }
                if (!string.IsNullOrEmpty(ModifyDate))
                {
                    param[11] = new SqlParameter("@ParamModifyDate", SqlDbType.DateTime);
                    param[11].Value = Convert.ToDateTime(ModifyDate);
                }
                if (!string.IsNullOrEmpty(Campaign))
                {
                    param[12] = new SqlParameter("@ParamCampaign", SqlDbType.VarChar, 100);
                    param[12].Value = Campaign;
                }
                if (!string.IsNullOrEmpty(journeyType))
                {
                    param[13] = new SqlParameter("@ParamTripType", SqlDbType.VarChar, 10);
                    param[13].Value = journeyType;
                }
                if (Active)
                {
                    param[14] = new SqlParameter("@ParamActive", SqlDbType.Bit);
                    param[14].Value = 1;
                }
                else
                {
                    param[14] = new SqlParameter("@ParamActive", SqlDbType.Bit);
                    param[14].Value = 0;
                }

                if (!string.IsNullOrEmpty(flightType))
                {
                    param[15] = new SqlParameter("@ParamFlightType", SqlDbType.VarChar, 100);
                    param[15].Value = flightType;
                }


                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_ChangeFare_FT", param);
                return ds.Tables[0];
            }
        }
        catch (Exception e)
        {
            return null;
        }
    }


    public string DeletOwnFares_FT(string ID, string Counter)
    {
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(ID))
                {
                    param[0] = new SqlParameter("@ParamSrNo", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(ID);
                }

                param[1] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[1].Value = Counter;

                param[2] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 500);
                param[2].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_ChangeFare_FT", param);
                return param[2].Value.ToString();
            }
        }
        catch
        {
            return "false";
        }
    }

    public string Update_Discount_OwnFares_FT(bool UseStatus)
    {
        string finalstatus = string.Empty;
        SqlConnection connection = DataConnection.GetConnection();
        try
        {
            connection.Open();
            string query = "Update ChangeFare_FT set isActive='" + UseStatus + "'";
            SqlCommand command = new SqlCommand(query, connection);
            int i = command.ExecuteNonQuery();

            if (i > 0)
            {
                finalstatus = "true";
            }
            else
            {
                finalstatus = "false";
            }
        }
        catch (Exception ex)
        {
            finalstatus = "false";
        }
        finally
        {
            connection.Close();
        }
        return finalstatus;
    }

    public bool CheckDiscount_OwnFares_FT()
    {
        bool finalstatus = true;
        SqlConnection connection = DataConnection.GetConnection();
        try
        {
            connection.Open();
            SqlCommand command = new SqlCommand("select top 1 * from ChangeFare_FT where isActive='true'", connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                finalstatus = true;
            }
            else
            {
                finalstatus = false;
            }
        }
        catch (Exception ex)
        {
            return true;
        }
        finally
        {
            connection.Close();
        }
        return finalstatus;
    }

    #endregion


    #region Itinerary Section for all FT,UK,US
    public string UpdateUseStatusOwnItineraryUS(bool UseStatus, string from, string to, string airline, string journey, string campaign, string uniqueId)
    {
        try
        {
            using (SqlConnection connection = DataConnection.GetConnectionCustomItinerary())
            {


                StringBuilder strcmd = new StringBuilder("UPDATE CustomItinBaseUS SET STATUS = '" + UseStatus + "' Where JourneyType = '" + journey + "'");

                if (from != "")
                {
                    strcmd.Append("and Origin ='" + from.ToUpper() + "'");
                }
                if (to != "")
                {
                    strcmd.Append("and Destination ='" + to.ToUpper() + "'");
                }
                if (airline != "")
                {
                    strcmd.Append("and ValCarrier ='" + airline.ToUpper() + "'");
                }
                if (campaign != "")
                {
                    strcmd.Append("and Camp_ID ='" + campaign.ToUpper() + "'");
                }
                if (uniqueId != "")
                {
                    strcmd.Append("and UniqueId ='" + uniqueId + "'");
                }

                if (SqlHelper.ExecuteNonQuery(connection, CommandType.Text, strcmd.ToString()) >= 1)
                {
                    return "true";
                }
                else
                {
                    return "false";
                }
            }
        }
        catch (Exception ex)
        {
            return "false";
        }
    }

    public string UpdateUseStatusOwnItinerary(bool UseStatus, string from, string to, string airline, string journey, string campaign, string uniqueId)
    {
        try
        {
            using (SqlConnection connection = DataConnection.GetConnectionCustomItinerary())
            {
                //string query = "Update CustomItinBase set status='" + UseStatus + "' Where    ";
                // Update as per Per Parbhat While all the Filtered Iternery Status are updated
                // 10 Sep 2020

                StringBuilder strcmd = new StringBuilder("UPDATE CustomItinBase SET STATUS = '" + UseStatus + "' Where JourneyType = '" + journey + "'");

                if (from != "")
                {
                    strcmd.Append("and Origin ='" + from.ToUpper() + "'");
                }
                if (to != "")
                {
                    strcmd.Append("and Destination ='" + to.ToUpper() + "'");
                }
                if (airline != "")
                {
                    strcmd.Append("and ValCarrier ='" + airline.ToUpper() + "'");
                }
                if (campaign != "")
                {
                    strcmd.Append("and Camp_ID ='" + campaign.ToUpper() + "'");
                }
                if (uniqueId != "")
                {
                    strcmd.Append("and UniqueId ='" + uniqueId + "'");
                }

                if (SqlHelper.ExecuteNonQuery(connection, CommandType.Text, strcmd.ToString()) >= 1)
                {
                    return "true";
                }
                else
                {
                    return "false";
                }
            }
        }
        catch (Exception ex)
        {
            return "false";
        }
    }


    #region CustomItinerary

    public string InsertCustomItinerary(string UniqueId, string A_Base_Fare, string A_Taxes, string C_Base_Fare, string C_Taxes, string I_Base_Fare,
        string I_Taxes, string Currency, string FareType, string Provider, string ValCarrier, string PCC, string OfferType,
        string AccCode, string Origin, string Destination, string JourneyType, string ValidFrom, string ValidTo,
        string Company_ID, string ddlCampaign, string ModifiedBy, string status, string SerialId, string IsConnect, string AirV, string AirlineName,
        string Class, string CabinClassCode, string CabinClassName, string NoSeats, string FltNum, string D_AirportCode, string D_AirportName,
        string D_AirportCityCode, string D_AirportCityName, string D_AirportCountryCode, string D_AirportCountryName, string D_Terminal,
        string D_Time, string D_DateTimeStamp, string A_AirportCode, string A_AirportName, string A_AirportCityCode, string A_AirportCityName,
        string A_AirportCountryCode, string A_AirportCountryName, string A_Terminal, string A_Time, string A_DateTimeStamp, string IsNextDayCount,
        string EquipType, string ElapsedTime, string ActualTime, string TechStopOver, string ddlIsReturn, string BaggageInfo,
        string BI_Pieces, string BI_Price, string BI_Description, string TransitTime, string Sector_Key, string Distance,
        string ETicket, string ChangeOfPlane, string ParticipantLevel, string OptionalServicesIndicator, string AvailabilitySource,
        string Sector_Group, string LinkAvailability, string PolledAvailabilityOption, string BookingCodeInfo, string FareBasisCode,
        string ItinKey, string SupCode, string Counter)
    {
        SqlParameter[] param = new SqlParameter[77];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnectionMarkupUS())
            {

                if (!string.IsNullOrEmpty(UniqueId))
                {
                    param[0] = new SqlParameter("@ParamUniqueId", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(UniqueId);
                }

                param[1] = new SqlParameter("@ParamPax_A_BaseFare", SqlDbType.VarChar, 50);
                param[1].Value = A_Base_Fare;

                param[2] = new SqlParameter("@ParamPax_A_Taxes", SqlDbType.VarChar, 50);
                param[2].Value = A_Taxes;

                param[3] = new SqlParameter("@ParamPax_C_BaseFare", SqlDbType.VarChar, 50);
                param[3].Value = C_Base_Fare;

                param[4] = new SqlParameter("@ParamPax_C_Taxes", SqlDbType.VarChar, 50);
                param[4].Value = C_Taxes;

                param[5] = new SqlParameter("@ParamPax_I_BaseFare", SqlDbType.VarChar, 50);
                param[5].Value = I_Base_Fare;

                param[6] = new SqlParameter("@ParamPax_I_Taxes", SqlDbType.VarChar, 50);
                param[6].Value = I_Taxes;

                param[7] = new SqlParameter("@ParamCurrency", SqlDbType.VarChar, 50);
                param[7].Value = Currency;

                param[8] = new SqlParameter("@ParamFareType", SqlDbType.VarChar, 50);
                param[8].Value = FareType;

                param[9] = new SqlParameter("@ParamProvider", SqlDbType.VarChar, 50);
                param[9].Value = Provider;

                param[10] = new SqlParameter("@ParamValCarrier", SqlDbType.VarChar, 50);
                param[10].Value = ValCarrier;

                param[11] = new SqlParameter("@ParamPCC", SqlDbType.VarChar, 50);
                param[11].Value = PCC;

                param[12] = new SqlParameter("@ParamOfferType", SqlDbType.VarChar, 50);
                param[12].Value = OfferType;

                param[13] = new SqlParameter("@ParamAccCode", SqlDbType.VarChar, 50);
                param[13].Value = AccCode;

                param[14] = new SqlParameter("@ParamOrigin", SqlDbType.VarChar, 50);
                param[14].Value = Origin;

                param[15] = new SqlParameter("@ParamDestination", SqlDbType.VarChar, 50);
                param[15].Value = Destination;

                param[16] = new SqlParameter("@ParamJourneyType", SqlDbType.VarChar, 50);
                param[16].Value = JourneyType;

                param[17] = new SqlParameter("@ParamValidFrom", SqlDbType.VarChar, 50);
                param[17].Value = ValidFrom;

                param[18] = new SqlParameter("@ParamValidTo", SqlDbType.VarChar, 50);
                param[18].Value = ValidTo;

                param[19] = new SqlParameter("@ParamCompany_ID", SqlDbType.VarChar, 50);
                param[19].Value = Company_ID;

                param[20] = new SqlParameter("@ParamCamp_ID", SqlDbType.VarChar, 50);
                param[20].Value = ddlCampaign;

                param[21] = new SqlParameter("@ParamModifiedBy", SqlDbType.VarChar, 50);
                param[21].Value = ModifiedBy;

                param[22] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 50);
                param[22].Value = status;

                param[23] = new SqlParameter("@ParamSerialId", SqlDbType.VarChar, 50);
                param[23].Value = SerialId;

                param[24] = new SqlParameter("@ParamIsConnect", SqlDbType.VarChar, 50);
                param[24].Value = IsConnect;

                param[25] = new SqlParameter("@ParamAirV", SqlDbType.VarChar, 50);
                param[25].Value = AirV;

                param[26] = new SqlParameter("@ParamAirlineName", SqlDbType.VarChar, 50);
                param[26].Value = AirlineName;

                param[27] = new SqlParameter("@ParamClass", SqlDbType.VarChar, 50);
                param[27].Value = Class;

                param[28] = new SqlParameter("@ParamCabinClassCode", SqlDbType.VarChar, 50);
                param[28].Value = CabinClassCode;

                param[29] = new SqlParameter("@ParamCabinClassName", SqlDbType.VarChar, 50);
                param[29].Value = CabinClassName;

                param[30] = new SqlParameter("@ParamNoSeats", SqlDbType.VarChar, 50);
                param[30].Value = NoSeats;

                param[31] = new SqlParameter("@ParamFltNum", SqlDbType.VarChar, 50);
                param[31].Value = FltNum;

                param[32] = new SqlParameter("@ParamD_AirportCode", SqlDbType.VarChar, 50);
                param[32].Value = D_AirportCode;

                param[33] = new SqlParameter("@ParamD_AirportName", SqlDbType.VarChar, 50);
                param[33].Value = D_AirportName;

                param[34] = new SqlParameter("@ParamD_AirportCityCode", SqlDbType.VarChar, 50);
                param[34].Value = D_AirportCityCode;

                param[35] = new SqlParameter("@ParamD_AirportCityName", SqlDbType.VarChar, 50);
                param[35].Value = D_AirportCityName;

                param[36] = new SqlParameter("@ParamD_AirportCountryCode", SqlDbType.VarChar, 50);
                param[36].Value = D_AirportCountryCode;

                param[37] = new SqlParameter("@ParamD_AirportCountryName", SqlDbType.VarChar, 50);
                param[37].Value = D_AirportCountryName;

                param[38] = new SqlParameter("@ParamD_Terminal", SqlDbType.VarChar, 50);
                param[38].Value = D_Terminal;

                param[39] = new SqlParameter("@ParamD_Time", SqlDbType.VarChar, 50);
                param[39].Value = D_Time;

                param[40] = new SqlParameter("@ParamD_DateTimeStamp", SqlDbType.VarChar, 50);
                param[40].Value = D_DateTimeStamp;

                param[41] = new SqlParameter("@ParamA_AirportCode", SqlDbType.VarChar, 50);
                param[41].Value = A_AirportCode;

                param[42] = new SqlParameter("@ParamA_AirportName", SqlDbType.VarChar, 50);
                param[42].Value = A_AirportName;

                param[43] = new SqlParameter("@ParamA_AirportCityCode", SqlDbType.VarChar, 50);
                param[43].Value = A_AirportCityCode;

                param[44] = new SqlParameter("@ParamA_AirportCityName", SqlDbType.VarChar, 50);
                param[44].Value = A_AirportCityName;

                param[45] = new SqlParameter("@ParamA_AirportCountryCode", SqlDbType.VarChar, 50);
                param[45].Value = A_AirportCountryCode;

                param[46] = new SqlParameter("@ParamA_AirportCountryName", SqlDbType.VarChar, 50);
                param[46].Value = A_AirportCountryName;

                param[47] = new SqlParameter("@ParamA_Terminal", SqlDbType.VarChar, 50);
                param[47].Value = A_Terminal;

                param[48] = new SqlParameter("@ParamA_Time", SqlDbType.VarChar, 50);
                param[48].Value = A_Time;

                param[49] = new SqlParameter("@ParamA_DateTimeStamp", SqlDbType.VarChar, 50);
                param[49].Value = A_DateTimeStamp;

                param[50] = new SqlParameter("@ParamIsNextDayCount", SqlDbType.Int);
                param[50].Value = IsNextDayCount;

                param[51] = new SqlParameter("@ParamEquipType", SqlDbType.VarChar, 50);
                param[51].Value = EquipType;

                param[52] = new SqlParameter("@ParamElapsedTime", SqlDbType.VarChar, 50);
                param[52].Value = ElapsedTime;

                param[53] = new SqlParameter("@ParamActualTime", SqlDbType.VarChar, 50);
                param[53].Value = ActualTime;

                param[54] = new SqlParameter("@ParamTechStopOver", SqlDbType.VarChar, 50);
                param[54].Value = TechStopOver;

                param[55] = new SqlParameter("@ParamIsReturn", SqlDbType.VarChar, 50);
                param[55].Value = ddlIsReturn;

                param[56] = new SqlParameter("@ParamBaggageInfo", SqlDbType.VarChar, 50);
                param[56].Value = BaggageInfo;

                param[57] = new SqlParameter("@ParamBI_Pieces", SqlDbType.VarChar, 50);
                param[57].Value = BI_Pieces;

                param[58] = new SqlParameter("@ParamBI_Price", SqlDbType.VarChar, 50);
                param[58].Value = BI_Price;

                param[59] = new SqlParameter("@ParamBI_Description", SqlDbType.VarChar, 50);
                param[59].Value = BI_Description;

                param[60] = new SqlParameter("@ParamTransitTime", SqlDbType.VarChar, 50);
                param[60].Value = TransitTime;

                param[61] = new SqlParameter("@ParamSector_Key", SqlDbType.VarChar, 50);
                param[61].Value = Sector_Key;

                param[62] = new SqlParameter("@ParamDistance", SqlDbType.VarChar, 50);
                param[62].Value = Distance;

                param[63] = new SqlParameter("@ParamETicket", SqlDbType.VarChar, 50);
                param[63].Value = ETicket;

                param[64] = new SqlParameter("@ParamChangeOfPlane", SqlDbType.VarChar, 50);
                param[64].Value = ChangeOfPlane;

                param[65] = new SqlParameter("@ParamParticipantLevel", SqlDbType.VarChar, 50);
                param[65].Value = ParticipantLevel;

                param[66] = new SqlParameter("@ParamOptionalServicesIndicator", SqlDbType.VarChar, 50);
                param[66].Value = OptionalServicesIndicator;

                param[67] = new SqlParameter("@ParamAvailabilitySource", SqlDbType.VarChar, 50);
                param[67].Value = AvailabilitySource;

                param[68] = new SqlParameter("@ParamSector_Group", SqlDbType.VarChar, 50);
                param[68].Value = Sector_Group;

                param[69] = new SqlParameter("@ParamLinkAvailability", SqlDbType.VarChar, 50);
                param[69].Value = LinkAvailability;

                param[70] = new SqlParameter("@ParamPolledAvailabilityOption", SqlDbType.VarChar, 50);
                param[70].Value = PolledAvailabilityOption;

                param[71] = new SqlParameter("@ParamBookingCodeInfo", SqlDbType.VarChar, 50);
                param[71].Value = BookingCodeInfo;

                param[72] = new SqlParameter("@ParamFareBasisCode", SqlDbType.VarChar, 50);
                param[72].Value = FareBasisCode;

                param[73] = new SqlParameter("@ParamItinKey", SqlDbType.VarChar, 50);
                param[73].Value = ItinKey;

                param[74] = new SqlParameter("@ParamSupCode", SqlDbType.VarChar, 50);
                param[74].Value = SupCode;

                param[75] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[75].Value = Counter;



                param[76] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 500);
                param[76].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_CustomItinerary", param);
                return param[2].Value.ToString();
            }
        }
        catch
        {
            return "false";
        }
    }



    #endregion

    #region SET_Itinerary_Details

    public bool SET_Flight_Itinerary_Details(string Flight_ID, double Adult_Total, string Cabin_Class, string SourceIATA, string DestIATA,
       string Operator, DateTime Valid_from, DateTime valid_till, string Modify_By, string Company, double Markup, DataTable ItineraryDetails)
    {
        SqlParameter[] param = new SqlParameter[15];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnectionXL())
            {

                if (!string.IsNullOrEmpty(Flight_ID))
                {
                    param[0] = new SqlParameter("@Flight_ID", SqlDbType.VarChar, 30);
                    param[0].Value = Flight_ID;
                }

                if (!string.IsNullOrEmpty(Adult_Total.ToString()))
                {
                    param[1] = new SqlParameter("@Adult_Total", SqlDbType.Decimal);
                    param[1].Value = Convert.ToDouble(Adult_Total);
                }
                if (!string.IsNullOrEmpty(Cabin_Class))
                {
                    param[2] = new SqlParameter("@Cabin_Class", SqlDbType.VarChar, 1);
                    param[2].Value = Cabin_Class;
                }
                if (!string.IsNullOrEmpty(SourceIATA))
                {
                    param[3] = new SqlParameter("@SourceIATA", SqlDbType.VarChar, 3);
                    param[3].Value = SourceIATA;
                }
                if (!string.IsNullOrEmpty(DestIATA))
                {
                    param[4] = new SqlParameter("@DestIATA", SqlDbType.VarChar, 3);
                    param[4].Value = DestIATA;
                }
                if (!string.IsNullOrEmpty(Operator))
                {
                    param[5] = new SqlParameter("@Operator", SqlDbType.VarChar, 2);
                    param[5].Value = Operator;
                }
                if (!string.IsNullOrEmpty(Valid_from.ToString()))
                {
                    param[6] = new SqlParameter("@Valid_from", SqlDbType.Date);
                    param[6].Value = Convert.ToDateTime(Valid_from).ToString("yyyy/MM/dd");
                }
                if (!string.IsNullOrEmpty(valid_till.ToString()))
                {
                    param[7] = new SqlParameter("@valid_till", SqlDbType.Date);
                    param[7].Value = Convert.ToDateTime(valid_till).ToString("yyyy/MM/dd");
                }
                if (!string.IsNullOrEmpty(Modify_By))
                {
                    param[8] = new SqlParameter("@Modify_By", SqlDbType.VarChar, 30);
                    param[8].Value = Modify_By;
                }
                if (!string.IsNullOrEmpty(Company))
                {
                    param[9] = new SqlParameter("@Company", SqlDbType.VarChar, 10);
                    param[9].Value = Company;
                }

                if (!string.IsNullOrEmpty(Markup.ToString()))
                {
                    param[10] = new SqlParameter("@Markup", SqlDbType.Decimal);
                    param[10].Value = Convert.ToDouble(Markup);
                }

                param[11] = new SqlParameter("@tblFID", ItineraryDetails);

                param[12] = new SqlParameter("@ParamStatus", SqlDbType.Bit);
                param[12].Direction = ParameterDirection.Output;

                param[13] = new SqlParameter("@ParamErrorNO", SqlDbType.VarChar, 500);
                param[13].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "SP_Flight_Itinerary_Details", param);


                string log = "Status=" + param[12].Value + "  ErrorNO=" + param[13].Value;

                //string Pth = @"D:\Websites\TravelJunction.co.uk\TravelJunction_V2\App_Data\Errors\" + BM_BookingID + "-GetSetInDB.txt";
                //File.WriteAllText(Pth, Convert.ToString(log));
                return Convert.ToBoolean(param[14].Value);
            }
        }
        catch (Exception ex)
        {
            //string Pth = @"D:\Websites\TravelJunction.co.uk\TravelJunction_V2\App_Data\Errors\" + BM_BookingID + "-GetSetInDB.txt";
            //File.WriteAllText(Pth, Convert.ToString(ex.Message + "\\n\\t" + ex.StackTrace + "\\n\\t" + ex.Source));

            return false;
        }
    }

    #endregion

    #region GET_Flight_Itinerary_Details
    public DataTable GET_Flight_Itinerary(string Source, string Destination, string Operator)
    {
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnectionXL())
            {
                if (!string.IsNullOrEmpty(Source))
                {
                    param[0] = new SqlParameter("@SourceIATA", SqlDbType.VarChar, 3);
                    param[0].Value = Source;
                }
                if (!string.IsNullOrEmpty(Destination))
                {
                    param[1] = new SqlParameter("@DestIATA", SqlDbType.VarChar, 3);
                    param[1].Value = Destination;
                }
                if (!string.IsNullOrEmpty(Operator))
                {
                    param[2] = new SqlParameter("@Operator", SqlDbType.VarChar, 3);
                    param[2].Value = Operator;
                }
                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_Flight_Itinerary", param);
                return ds.Tables[0];
            }
        }
        catch
        {
            return null;
        }
    }

    public DataTable GET_Flight_Itinerary_Details(string MainFlightID)
    {
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnectionXL())
            {
                if (!string.IsNullOrEmpty(MainFlightID))
                {
                    param[0] = new SqlParameter("@MainFlight_GUID", SqlDbType.VarChar, 20);
                    param[0].Value = MainFlightID;
                }

                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_Flight_Itinerary_Details", param);
                return ds.Tables[0];
            }
        }
        catch
        {
            return null;
        }
    }

    #endregion


    public DataTable GET_SET_Itinerary_FT(string Id, string UniqueId, string Camp_ID, string ValidTo, string ValidFrom, string ValCarrier,
        string Destination, string Origin, string JourneyType, string Counter, DataTable CustomItinBaseFT, DataTable CustomItinFT)
    {
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[12];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnectionCustomItinerary())
            {
                if (!string.IsNullOrEmpty(Id))
                {
                    param[0] = new SqlParameter("@ParamId", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(Id);
                }
                if (!string.IsNullOrEmpty(UniqueId))
                {
                    param[1] = new SqlParameter("@ParamUniqueId", SqlDbType.Int);
                    param[1].Value = Convert.ToInt32(UniqueId);
                }
                if (!string.IsNullOrEmpty(Camp_ID))
                {
                    param[2] = new SqlParameter("@ParamCamp_ID", SqlDbType.Int);
                    param[2].Value = Convert.ToInt32(Camp_ID);
                }
                if (!string.IsNullOrEmpty(ValidTo))
                {
                    param[3] = new SqlParameter("@ParamValidTo", SqlDbType.DateTime);
                    param[3].Value = (ValidTo);
                }
                if (!string.IsNullOrEmpty(ValidFrom))
                {
                    param[4] = new SqlParameter("@ParamValidFrom", SqlDbType.DateTime);
                    param[4].Value = (ValidFrom);
                }
                if (!string.IsNullOrEmpty(ValCarrier))
                {
                    param[5] = new SqlParameter("@ParamValCarrier", SqlDbType.VarChar);
                    param[5].Value = (ValCarrier);
                }
                if (!string.IsNullOrEmpty(ValidFrom))
                {
                    param[6] = new SqlParameter("@ParamDestination", SqlDbType.VarChar);
                    param[6].Value = (Destination);
                }
                if (!string.IsNullOrEmpty(Origin))
                {
                    param[7] = new SqlParameter("@ParamOrigin", SqlDbType.VarChar);
                    param[7].Value = (Origin);
                }
                if (!string.IsNullOrEmpty(JourneyType))
                {
                    param[8] = new SqlParameter("@ParamJourneyType", SqlDbType.VarChar);
                    param[8].Value = (JourneyType);
                }

                param[9] = new SqlParameter("@Counter", SqlDbType.VarChar);
                param[9].Value = Counter;

                if (CustomItinBaseFT != null && CustomItinBaseFT.Rows.Count > 0)
                {
                    param[10] = new SqlParameter("@tblCustomItinBaseFT", CustomItinBaseFT);
                }

                if (CustomItinFT != null && CustomItinFT.Rows.Count > 0)
                {
                    param[11] = new SqlParameter("@tblCustomItinFT", CustomItinFT);
                }

                ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_Itinerary_FT", param);
            }

            #endregion
        }
        catch (Exception ex)
        {

        }
        finally
        {

        }
        return ds.Tables[0];
    }




    public static DataTable GET_SET_Error_Log(Exception exception, string PageName, string MethodName)
    {
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[11];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(exception.Message))
                {
                    param[0] = new SqlParameter("@ParamExceptionMessage", SqlDbType.VarChar);
                    param[0].Value = Convert.ToString(exception.Message);
                }

                if (!string.IsNullOrEmpty(exception.StackTrace))
                {
                    param[1] = new SqlParameter("@ParamExceptionStackTrace", SqlDbType.VarChar);
                    param[1].Value = Convert.ToString(exception.StackTrace);
                }
                if (!string.IsNullOrEmpty(exception.Source))
                {
                    param[2] = new SqlParameter("@ParamExceptionSource", SqlDbType.VarChar);
                    param[2].Value = Convert.ToString(exception.Source);
                }
                if (!string.IsNullOrEmpty(exception.GetType().ToString()))
                {
                    param[3] = new SqlParameter("@ParamExceptionType", SqlDbType.VarChar);
                    param[3].Value = Convert.ToString(exception.GetType().ToString());
                }
                if (!string.IsNullOrEmpty(PageName))
                {
                    param[4] = new SqlParameter("@ParamExceptionPageName", SqlDbType.VarChar);
                    param[4].Value = Convert.ToString(PageName);
                }
                if (!string.IsNullOrEmpty(MethodName))
                {
                    param[5] = new SqlParameter("@ParamExceptionMethodName", SqlDbType.VarChar);
                    param[5].Value = Convert.ToString(MethodName);
                }
                ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_Error_Log", param);
            }

        }
        catch (Exception ex)
        {

        }
        finally
        {

        }
        return ds.Tables[0];
    }

    public static string GetCurrentMethodName()
    {
        return System.Reflection.MethodBase.GetCurrentMethod().Name;
    }

    public static string GetCurrentPageName(string urlPath)
    {
        string sPath = urlPath;
        System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
        string sRet = oInfo.Name;
        return sRet;
    }


    #region Tracker
    public DataTable SearchPageTracker(string _Origin, string _Destination, string _FromDepartDate, string _ToDepartDate,
                 string _fromReturnDate, string _toReturnDate, string _company, string _fromHitDate, string _toHitDate,
                 string _Site, string _IPAddress, string _Page, string _Page_Url, string _IPCountry, string _IPCiry,
                 string _Browser, string _SessionID, string redirectFrom, string _remarks, string SourceMediaList, string Counter)
    {

        try
        {

            SqlParameter[] param = new SqlParameter[20];

            if (!string.IsNullOrEmpty(_Origin))
            {
                param[0] = new SqlParameter("@paramOrigin", SqlDbType.NVarChar, 50);
                param[0].Value = _Origin;
            }
            if (!string.IsNullOrEmpty(_Destination))
            {
                param[1] = new SqlParameter("@paramDestination", SqlDbType.NVarChar, 50);
                param[1].Value = _Destination;
            }

            if (!string.IsNullOrEmpty(_FromDepartDate))
            {
                param[2] = new SqlParameter("@paramFromDepartDate", SqlDbType.DateTime);
                param[2].Value = Convert.ToDateTime(_FromDepartDate);
            }
            if (!string.IsNullOrEmpty(_ToDepartDate))
            {
                param[3] = new SqlParameter("@paramToDepartDate", SqlDbType.DateTime);
                param[3].Value = Convert.ToDateTime(_ToDepartDate);
            }
            if (!string.IsNullOrEmpty(_fromReturnDate))
            {
                param[4] = new SqlParameter("@paramFromReturnDate", SqlDbType.DateTime);
                param[4].Value = Convert.ToDateTime(_fromReturnDate);
            }

            if (!string.IsNullOrEmpty(_toReturnDate))
            {
                param[5] = new SqlParameter("@paramToReturnDate", SqlDbType.DateTime);
                param[5].Value = Convert.ToDateTime(_toReturnDate);
            }
            if (!string.IsNullOrEmpty(_company))
            {
                param[6] = new SqlParameter("@paramCompany", SqlDbType.NVarChar);
                param[6].Value = _company;
            }
            if (!string.IsNullOrEmpty(_fromHitDate))
            {
                param[7] = new SqlParameter("@paramFromDatenTime", SqlDbType.DateTime);
                param[7].Value = Convert.ToDateTime(_fromHitDate);
            }

            if (!string.IsNullOrEmpty(_toHitDate))
            {
                param[8] = new SqlParameter("@paramToDatenTime", SqlDbType.DateTime);
                param[8].Value = Convert.ToDateTime(_toHitDate);
            }
            if (!string.IsNullOrEmpty(_Site))
            {
                param[9] = new SqlParameter("@paramSite", SqlDbType.NVarChar, 100);
                param[9].Value = _Site;
            }
            if (!string.IsNullOrEmpty(_IPAddress))
            {
                param[10] = new SqlParameter("@paramIP", SqlDbType.NVarChar, 50);
                param[10].Value = _IPAddress;
            }

            if (!string.IsNullOrEmpty(_Page))
            {
                param[11] = new SqlParameter("@paramPage", SqlDbType.NVarChar, 50);
                param[11].Value = _Page;
            }
            if (!string.IsNullOrEmpty(_Page_Url))
            {
                param[12] = new SqlParameter("@paramPageUrl", SqlDbType.NVarChar, 500);
                param[12].Value = _Page_Url;
            }
            if (!string.IsNullOrEmpty(_IPCountry))
            {
                param[13] = new SqlParameter("@paramIPCountry", SqlDbType.NVarChar, 50);
                param[13].Value = _IPCountry;
            }

            if (!string.IsNullOrEmpty(_IPCiry))
            {
                param[14] = new SqlParameter("@paramIPCity", SqlDbType.NVarChar, 50);
                param[14].Value = _IPCiry;
            }
            if (!string.IsNullOrEmpty(_Browser))
            {
                param[15] = new SqlParameter("@paramBrowser", SqlDbType.NVarChar, 50);
                param[15].Value = _Browser;
            }
            if (!string.IsNullOrEmpty(_SessionID))
            {
                param[16] = new SqlParameter("@paramSessionID", SqlDbType.NVarChar, 200);
                param[16].Value = _SessionID;
            }
            if (!string.IsNullOrEmpty(redirectFrom))
            {
                param[16] = new SqlParameter("@paramRedirectFrom", SqlDbType.NVarChar, 100);
                param[16].Value = redirectFrom;
            }
            if (!string.IsNullOrEmpty(_remarks))
            {
                param[17] = new SqlParameter("@paramRemarks", SqlDbType.NVarChar, 500);
                param[17].Value = _remarks;
            }
            if (!string.IsNullOrEmpty(SourceMediaList))
            {
                param[18] = new SqlParameter("@ParamSourceMediaList", SqlDbType.VarChar, 4000);
                param[18].Value = SourceMediaList;
            }
            if (!string.IsNullOrEmpty(Counter))
            {
                param[19] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[19].Value = Counter;
            }
            using (SqlConnection connection = DataConnection.GetConnection())
            {
                DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ST_PageTracker_Get", param);
                return ds.Tables[0] != null ? ds.Tables[0] : null;
            }
        }
        catch
        {
            return null;
        }
    }

    #endregion

    public DataTable GET_SET_LastMonthData_LeadDashboard(double spendData, double returnData, string ratio, string Counter, string CreatedDate)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[5];

            param[0] = new SqlParameter("@paramSpend", SqlDbType.Float);
            param[0].Value = spendData;

            param[1] = new SqlParameter("@paramReturn", SqlDbType.Float);
            param[1].Value = returnData;

            param[2] = new SqlParameter("@paramRatio", SqlDbType.VarChar, 50);
            param[2].Value = ratio;

            param[3] = new SqlParameter("@paramCounter", SqlDbType.VarChar, 500);
            param[3].Value = Counter;

            param[4] = new SqlParameter("@paramCreatedDate", SqlDbType.VarChar, 500);
            param[4].Value = CreatedDate;

            using (SqlConnection connection = DataConnection.GetConnection())
            {
                DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "GET_SET_LastMonthData_LeadDashboard", param);
                return ds.Tables[0] != null ? ds.Tables[0] : null;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }


    public DataTable GET_SET_LastWeekData_LeadDashboard(DataTable dt, string Counter)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ParamLastWeekData", dt);

            param[1] = new SqlParameter("@paramCounter", SqlDbType.VarChar, 500);
            param[1].Value = Counter;

            using (SqlConnection connection = DataConnection.GetConnection())
            {
                DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "GET_SET_LastWeekData_LeadDashboard", param);
                return ds.Tables[0] != null ? ds.Tables[0] : null;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public DataTable GET_SET_LeadDashBoard_Sales_Data(string CampaignList, string StatusList, DateTime fromDate, DateTime toDate)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@ParamBookingCampaign", SqlDbType.NVarChar);
            param[0].Value = CampaignList;

            param[1] = new SqlParameter("@ParamBookingStatus", SqlDbType.NVarChar);
            param[1].Value = StatusList;

            param[2] = new SqlParameter("@ParamDateFrom", SqlDbType.DateTime);
            param[2].Value = fromDate;

            param[3] = new SqlParameter("@ParamDateTo", SqlDbType.DateTime);
            param[3].Value = toDate;

            using (SqlConnection connection = DataConnection.GetConnection())
            {
                DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "GET_SET_LeadDashBoard_Sales_Data", param);
                return ds.Tables[0] != null ? ds.Tables[0] : null;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public DataTable GET_User_CampaignUser_Access_For_FT(string UserID)
    {
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(UserID))
                {
                    param[0] = new SqlParameter("@ParamUserID", SqlDbType.VarChar, 50);
                    param[0].Value = UserID;
                }
                param[1] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[1].Value = "Select_FT_Campaign";
                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_Campaign_For_FT", param);
                return ds.Tables[0];
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataSet GET_Passenger_Details_For_Sales(string BookingID, string AgentName, string FromDate)
    {
        SqlParameter[] param = new SqlParameter[4];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(BookingID))
                {
                    param[0] = new SqlParameter("@ParamBookingID", SqlDbType.VarChar, 50);
                    param[0].Value = BookingID;
                }
                if (!string.IsNullOrEmpty(AgentName))
                {
                    param[1] = new SqlParameter("@ParamAgentName", SqlDbType.VarChar, 50);
                    param[1].Value = AgentName;
                }
                if (!string.IsNullOrEmpty(FromDate))
                {
                    param[2] = new SqlParameter("@ParamFromDate", SqlDbType.DateTime);
                    param[2].Value = Convert.ToDateTime(FromDate);
                }
                param[3] = new SqlParameter("@ParamCounter", SqlDbType.VarChar, 500);
                param[3].Value = "Select";

                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_Passenger_Details_For_Sales", param);
                return ds;
            }
        }
        catch
        {
            return null;
        }
    }

    #region Page Tracker FOR FT
    public DataTable SearchPageTrackerOnline_FORFT(string _Origin, string _Destination, string _FromDepartDate, string _ToDepartDate,
   string _fromReturnDate, string _toReturnDate, string _company, string _fromHitDate, string _toHitDate,
   string _Site, string _IPAddress, string _Page, string _Page_Url, string _IPCountry, string _IPCiry,
   string _Browser, string _SessionID, string redirectFrom, string _remarks, string SourceMediaList, string Counter)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[20];

            if (!string.IsNullOrEmpty(_Origin))
            {
                param[0] = new SqlParameter("@paramOrigin", SqlDbType.NVarChar, 50);
                param[0].Value = _Origin;
            }
            if (!string.IsNullOrEmpty(_Destination))
            {
                param[1] = new SqlParameter("@paramDestination", SqlDbType.NVarChar, 50);
                param[1].Value = _Destination;
            }

            if (!string.IsNullOrEmpty(_FromDepartDate))
            {
                param[2] = new SqlParameter("@paramFromDepartDate", SqlDbType.DateTime);
                param[2].Value = Convert.ToDateTime(_FromDepartDate);
            }
            if (!string.IsNullOrEmpty(_ToDepartDate))
            {
                param[3] = new SqlParameter("@paramToDepartDate", SqlDbType.DateTime);
                param[3].Value = Convert.ToDateTime(_ToDepartDate);
            }
            if (!string.IsNullOrEmpty(_fromReturnDate))
            {
                param[4] = new SqlParameter("@paramFromReturnDate", SqlDbType.DateTime);
                param[4].Value = Convert.ToDateTime(_fromReturnDate);
            }

            if (!string.IsNullOrEmpty(_toReturnDate))
            {
                param[5] = new SqlParameter("@paramToReturnDate", SqlDbType.DateTime);
                param[5].Value = Convert.ToDateTime(_toReturnDate);
            }
            if (!string.IsNullOrEmpty(_company))
            {
                param[6] = new SqlParameter("@paramCompany", SqlDbType.NVarChar);
                param[6].Value = _company;
            }
            if (!string.IsNullOrEmpty(_fromHitDate))
            {
                param[7] = new SqlParameter("@paramFromDatenTime", SqlDbType.DateTime);
                param[7].Value = Convert.ToDateTime(_fromHitDate);
            }

            if (!string.IsNullOrEmpty(_toHitDate))
            {
                param[8] = new SqlParameter("@paramToDatenTime", SqlDbType.DateTime);
                param[8].Value = Convert.ToDateTime(_toHitDate);
            }
            if (!string.IsNullOrEmpty(_Site))
            {
                param[9] = new SqlParameter("@paramSite", SqlDbType.NVarChar, 100);
                param[9].Value = _Site;
            }
            if (!string.IsNullOrEmpty(_IPAddress))
            {
                param[10] = new SqlParameter("@paramIP", SqlDbType.NVarChar, 50);
                param[10].Value = _IPAddress;
            }

            if (!string.IsNullOrEmpty(_Page))
            {
                param[11] = new SqlParameter("@paramPage", SqlDbType.NVarChar, 50);
                param[11].Value = _Page;
            }
            if (!string.IsNullOrEmpty(_Page_Url))
            {
                param[12] = new SqlParameter("@paramPageUrl", SqlDbType.NVarChar, 500);
                param[12].Value = _Page_Url;
            }
            if (!string.IsNullOrEmpty(_IPCountry))
            {
                param[13] = new SqlParameter("@paramIPCountry", SqlDbType.NVarChar, 50);
                param[13].Value = _IPCountry;
            }

            if (!string.IsNullOrEmpty(_IPCiry))
            {
                param[14] = new SqlParameter("@paramIPCity", SqlDbType.NVarChar, 50);
                param[14].Value = _IPCiry;
            }
            if (!string.IsNullOrEmpty(_Browser))
            {
                param[15] = new SqlParameter("@paramBrowser", SqlDbType.NVarChar, 50);
                param[15].Value = _Browser;
            }
            if (!string.IsNullOrEmpty(_SessionID))
            {
                param[16] = new SqlParameter("@paramSessionID", SqlDbType.NVarChar, 200);
                param[16].Value = _SessionID;
            }
            if (!string.IsNullOrEmpty(redirectFrom))
            {
                param[16] = new SqlParameter("@paramRedirectFrom", SqlDbType.NVarChar, 100);
                param[16].Value = redirectFrom;
            }
            if (!string.IsNullOrEmpty(_remarks))
            {
                param[17] = new SqlParameter("@paramRemarks", SqlDbType.NVarChar, 500);
                param[17].Value = _remarks;
            }
            if (!string.IsNullOrEmpty(SourceMediaList))
            {
                param[18] = new SqlParameter("@ParamSourceMediaList", SqlDbType.VarChar, 4000);
                param[18].Value = SourceMediaList;
            }
            if (!string.IsNullOrEmpty(Counter))
            {
                if (_company.Split(',')[0] == "FLTTROTT" || (_company.Split(',')[1] == "TRAVELOFLIUK"))
                {
                    param[19] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                    param[19].Value = "Select_FT";
                }
                else
                {
                    param[19] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                    param[19].Value = "Select";
                }
            }
            using (SqlConnection connection = DataConnection.GetConnection())
            {
                DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ST_PageTracker_Get", param);
                return ds.Tables[0] != null ? ds.Tables[0] : null;
            }
        }
        catch
        {
            return null;
        }
    }
    #endregion

    public string UpdateUseStatusOwnItineraryFT(bool UseStatus, string from, string to, string airline, string journey, string campaign, string uniqueId)
    {
        try
        {
            using (SqlConnection connection = DataConnection.GetConnectionCustomItinerary())
            {
                StringBuilder strcmd = new StringBuilder("UPDATE CustomItinBaseFT SET STATUS = '" + UseStatus + "' Where JourneyType = '" + journey + "'");

                if (from != "")
                {
                    strcmd.Append("and Origin ='" + from.ToUpper() + "'");
                }
                if (to != "")
                {
                    strcmd.Append("and Destination ='" + to.ToUpper() + "'");
                }
                if (airline != "")
                {
                    strcmd.Append("and ValCarrier ='" + airline.ToUpper() + "'");
                }
                if (campaign != "")
                {
                    strcmd.Append("and Camp_ID ='" + campaign.ToUpper() + "'");
                }
                if (uniqueId != "")
                {
                    strcmd.Append("and UniqueId ='" + uniqueId + "'");
                }

                if (SqlHelper.ExecuteNonQuery(connection, CommandType.Text, strcmd.ToString()) >= 1)
                {
                    return "true";
                }
                else
                {
                    return "false";
                }
            }
        }
        catch (Exception ex)
        {
            return "false";
        }
    }

    public DataTable GET_User_CampaignUser_Access_For_FT(string UserID, string Counter)
    {
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                if (!string.IsNullOrEmpty(UserID))
                {
                    param[0] = new SqlParameter("@ParamUserID", SqlDbType.VarChar, 50);
                    param[0].Value = UserID;
                }
                param[1] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[1].Value = Counter;
                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_Campaign_For_FT", param);
                return ds.Tables[0];
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }


    #region New Dashboard Data 
    public DataSet Get_Agents_P_L_NEW_Dashboard(string BookingRef, string BookingBy, string fromDate, string toDate, string company, string status)
    {
        SqlParameter[] param = new SqlParameter[6];
        DataSet ds;
        try
        {
            if (!string.IsNullOrEmpty(BookingRef))
            {
                param[0] = new SqlParameter("@ParamBookingID", SqlDbType.VarChar, 50);
                param[0].Value = BookingRef;
            }

            if (!string.IsNullOrEmpty(BookingBy))
            {
                param[1] = new SqlParameter("@ParamBookingBy", SqlDbType.VarChar, 50);
                param[1].Value = BookingBy;
            }

            if (!string.IsNullOrEmpty(fromDate))
            {
                param[2] = new SqlParameter("@paramFromDate", SqlDbType.Date);
                param[2].Value = fromDate;
            }

            if (!string.IsNullOrEmpty(toDate))
            {
                param[3] = new SqlParameter("@paramToDate", SqlDbType.Date);
                param[3].Value = toDate;
            }
            if (!string.IsNullOrEmpty(company))
            {
                param[4] = new SqlParameter("@paramCompany", SqlDbType.VarChar, 5000);
                param[4].Value = company;
            }
            if (!string.IsNullOrEmpty(status))
            {
                param[5] = new SqlParameter("@paramStatus", SqlDbType.VarChar, 5000);
                param[5].Value = status;
            }
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "Get_Agents_P_L_NEW_Dashboard", param);
                return ds;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public DataTable Get_Offline_Leads_Company_Based(string fromDate, string toDate, string company)
    {
        SqlParameter[] param = new SqlParameter[3];
        DataSet ds;
        try
        {
            if (!string.IsNullOrEmpty(fromDate))
            {
                param[0] = new SqlParameter("@paramFromDate", SqlDbType.Date);
                param[0].Value = fromDate;
            }

            if (!string.IsNullOrEmpty(toDate))
            {
                param[1] = new SqlParameter("@paramToDate", SqlDbType.Date);
                param[1].Value = toDate;
            }
            if (!string.IsNullOrEmpty(company))
            {
                param[2] = new SqlParameter("@paramCompany", SqlDbType.VarChar, 5000);
                param[2].Value = company;
            }
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "Get_Offline_Leads_Company_Based", param);
                return ds.Tables[0];
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    #endregion



    #region Markup Jazz US For Lokesh 09 March 2021


    public DataTable GET_FlightMarkupDetailJazz_US(string ID, string From, string To, string AirV, string Provider, string Category,
    string CClass, string FareType, string CompanyID, string CampID, string JourneyType, string RollName, string PageName, int PaxCount, string ModifyBy)
    {
        SqlParameter[] param = new SqlParameter[16];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnectionMarkupUS())
            {
                if (!string.IsNullOrEmpty(ID))
                {
                    param[0] = new SqlParameter("@ParamID", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(ID);
                }
                if (!string.IsNullOrEmpty(From))
                {
                    param[1] = new SqlParameter("@ParamFrom", SqlDbType.VarChar, 50);
                    param[1].Value = From.ToUpper();
                }
                if (!string.IsNullOrEmpty(To))
                {
                    param[2] = new SqlParameter("@ParamTo", SqlDbType.VarChar, 50);
                    param[2].Value = To.ToUpper();
                }
                if (!string.IsNullOrEmpty(AirV))
                {
                    param[3] = new SqlParameter("@ParamAirV", SqlDbType.VarChar, 50);
                    param[3].Value = AirV.ToUpper();
                }
                if (!string.IsNullOrEmpty(Provider))
                {
                    param[4] = new SqlParameter("@ParamProvider", SqlDbType.VarChar, 50);
                    param[4].Value = Provider.ToUpper();
                }
                if (!string.IsNullOrEmpty(Category))
                {
                    param[5] = new SqlParameter("@ParamCategory", SqlDbType.VarChar, 50);
                    param[5].Value = Category.ToUpper();
                }
                if (!string.IsNullOrEmpty(CClass))
                {
                    param[6] = new SqlParameter("@ParamClass", SqlDbType.VarChar, 50);
                    param[6].Value = CClass.ToUpper();
                }
                if (!string.IsNullOrEmpty(FareType))
                {
                    param[7] = new SqlParameter("@ParamFareType", SqlDbType.VarChar, 50);
                    param[7].Value = FareType.ToUpper();
                }
                if (!string.IsNullOrEmpty(CompanyID))
                {
                    param[8] = new SqlParameter("@ParamCompanyID", SqlDbType.VarChar);
                    param[8].Value = CompanyID;
                }
                if (!string.IsNullOrEmpty(CampID))
                {
                    param[9] = new SqlParameter("@ParamCampID", SqlDbType.VarChar, 50);
                    param[9].Value = CampID;
                }
                if (!string.IsNullOrEmpty(JourneyType))
                {
                    param[10] = new SqlParameter("@ParamJourneyType", SqlDbType.VarChar, 50);
                    param[10].Value = JourneyType.ToUpper();
                }
                if (!string.IsNullOrEmpty(RollName))
                {
                    param[11] = new SqlParameter("@ParamRollName", SqlDbType.NVarChar, 50);
                    param[11].Value = RollName;
                }
                if (!string.IsNullOrEmpty(PageName))
                {
                    param[12] = new SqlParameter("@ParamPageName", SqlDbType.NVarChar, 500);
                    param[12].Value = PageName;
                }

                param[13] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[13].Value = "Select";

                param[14] = new SqlParameter("@paramPaxCount", SqlDbType.VarChar, 500);
                param[14].Value = PaxCount;

                if (!string.IsNullOrEmpty(PageName))
                {
                    param[14] = new SqlParameter("@paramPaxCount", SqlDbType.VarChar, 500);
                    param[14].Value = PaxCount;
                }

                if (!string.IsNullOrEmpty(ModifyBy))
                {
                    param[15] = new SqlParameter("@ParamModifiedBy", SqlDbType.VarChar, 500);
                    param[15].Value = ModifyBy;
                }

                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_FlightMarkupDetail_Jazz", param);
                return ds.Tables[0];
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public string SET_FlightMarkupDetailJazz_US(string ID, string From, string To, string AirV, string Provider, string Category,
    string CClass, string FareType, string Amount, string AmountType, string ValidFromDate, string ValidToDate,
    string CompanyID, string CampID, string JourneyType, string ModifiedBy, string Counter, int paxCount, int DTD, string RestrictedClass)
    {
        SqlParameter[] param = new SqlParameter[21];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnectionMarkupUS())
            {

                if (!string.IsNullOrEmpty(ID))
                {
                    param[0] = new SqlParameter("@ParamID", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(ID);
                }

                param[1] = new SqlParameter("@ParamFrom", SqlDbType.VarChar, 50);
                param[1].Value = string.IsNullOrEmpty(From) ? "ANY" : From.ToUpper();

                param[2] = new SqlParameter("@ParamTo", SqlDbType.VarChar, 50);
                param[2].Value = string.IsNullOrEmpty(To) ? "ANY" : To.ToUpper();

                param[3] = new SqlParameter("@ParamAirV", SqlDbType.VarChar, 50);
                param[3].Value = string.IsNullOrEmpty(AirV) ? "ANY" : AirV.ToUpper();

                param[4] = new SqlParameter("@ParamProvider", SqlDbType.VarChar, 50);
                param[4].Value = string.IsNullOrEmpty(Provider) ? "ANY" : Provider.ToUpper();

                param[5] = new SqlParameter("@ParamCategory", SqlDbType.VarChar, 50);
                param[5].Value = string.IsNullOrEmpty(Category) ? "ANY" : Category.ToUpper();

                param[6] = new SqlParameter("@ParamClass", SqlDbType.VarChar, 50);
                param[6].Value = string.IsNullOrEmpty(CClass) ? "ANY" : CClass.ToUpper();

                param[7] = new SqlParameter("@ParamFareType", SqlDbType.VarChar, 50);
                param[7].Value = string.IsNullOrEmpty(FareType) ? "ANY" : FareType.ToUpper();

                param[8] = new SqlParameter("@ParamAmount", SqlDbType.Money);
                param[8].Value = Convert.ToDouble(Amount);

                param[9] = new SqlParameter("@ParamAmountType", SqlDbType.VarChar, 50);
                param[9].Value = AmountType.ToUpper();

                param[10] = new SqlParameter("@ParamValidFromDate", SqlDbType.DateTime);
                param[10].Value = string.IsNullOrEmpty(ValidFromDate) ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(ValidFromDate);

                param[11] = new SqlParameter("@ParamValidToDate", SqlDbType.DateTime);
                param[11].Value = string.IsNullOrEmpty(ValidToDate) ? Convert.ToDateTime("01-01-2100") : Convert.ToDateTime(ValidToDate);

                param[12] = new SqlParameter("@ParamCompanyID", SqlDbType.VarChar, 50);
                param[12].Value = CompanyID;

                param[13] = new SqlParameter("@ParamCampID", SqlDbType.VarChar, 50);
                param[13].Value = string.IsNullOrEmpty(CampID) ? "ANY" : CampID;

                param[14] = new SqlParameter("@ParamJourneyType", SqlDbType.VarChar, 50);
                param[14].Value = string.IsNullOrEmpty(JourneyType) ? "ANY" : JourneyType.ToUpper();

                param[15] = new SqlParameter("@ParamModifiedBy", SqlDbType.VarChar, 50);
                param[15].Value = string.IsNullOrEmpty(ModifiedBy) ? "" : ModifiedBy;

                param[16] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[16].Value = Counter;

                param[17] = new SqlParameter("@paramPaxCount", SqlDbType.VarChar, 500);
                param[17].Value = paxCount;

                param[18] = new SqlParameter("@ParamDaysToDeparture", SqlDbType.VarChar, 500);
                param[18].Value = DTD;

                param[19] = new SqlParameter("@paramRestrictedClass", SqlDbType.VarChar, 200);
                param[19].Value = RestrictedClass;

                param[20] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 500);
                param[20].Direction = ParameterDirection.Output;


                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_FlightMarkupDetail_Jazz", param);
                return param[20].Value.ToString();
            }
        }
        catch (Exception ex)
        {
            return "false";
        }
    }

    public bool CheckDiscountMarkupJazz_US()
    {
        bool finalstatus = true;
        try
        {
            using (SqlConnection connection = DataConnection.GetConnectionMarkupUS())
            {
                SqlCommand command = new SqlCommand("select top 1 * from FlightMarkupDetail_Jazz where isActive='true' and Agnt_AirF_Markup_Amount<0", connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    finalstatus = true;
                }
                else
                {
                    finalstatus = false;
                }


            }
        }
        catch (Exception ex)
        {
            return true;
        }

        return finalstatus;
    }

    public string UpdateDiscountMarkupJazz_US(bool UseStatus)
    {
        try
        {
            using (SqlConnection connection = DataConnection.GetConnectionMarkupUS())
            {
                string query = "Update FlightMarkupDetail_Jazz set isActive='" + UseStatus + "' where Agnt_AirF_Markup_Amount<0";

                if (SqlHelper.ExecuteNonQuery(connection, CommandType.Text, query) > 1)
                {
                    return "true";
                }
                else
                    return "false";

            }
        }
        catch (Exception ex)
        {
            return "false";
        }
    }

    public string UpdateMarkupExcelJazz_US(string ID, string UpdateField, string Value, string UpdetedBy)
    {
        try
        {
            string FieldName = "";
            switch (UpdateField)
            {
                case "MarkupFrom": FieldName = "Agnt_AirF_Markup_From"; break;
                case "MarkupTo": FieldName = "Agnt_AirF_Markup_To"; break;
                case "AirV": FieldName = "Agnt_AirF_Markup_AirV"; break;
                case "Provider": FieldName = "Agnt_AirF_Markup_Provider"; break;
                case "Category": FieldName = "Agnt_AirF_Markup_Category"; break;
                case "MarkupClass": FieldName = "Agnt_AirF_Markup_Class"; break;
                case "FareType": FieldName = "Agnt_AirF_Markup_Fare_Type"; break;
                case "JourneyType": FieldName = "Agnt_AirF_Markup_JourneyType"; break;
                case "Amount": FieldName = "Agnt_AirF_Markup_Amount"; break;
                case "AmountType": FieldName = "Agnt_AirF_Markup_Amount_Type"; break;
                case "ValidFromDate": FieldName = "Agnt_AirF_Markup_ValidFromDate"; break;
                case "ValidToDate": FieldName = "Agnt_AirF_Markup_ValidToDate"; break;
                case "DaysToDeparture": FieldName = "DaysToDeparture"; break;
                case "PaxCount": FieldName = "Agnt_AirF_No_Of_Pax"; break;
                case "RestrictedClass": FieldName = "Agnt_AirF_Restricted_Class"; break;
            }
            string Query = "";
            if (UpdateField == "Amount")
            {
                Query = "update FlightMarkupDetail_Jazz set " + FieldName + "=" + Value.ToUpper() + ",Agnt_AirF_Markup_ModifiedBy='" + UpdetedBy + "',Agnt_AirF_Markup_LastModifiedDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' Where Agnt_AirF_Markup_ID=" + ID;
            }
            else
            {
                if (UpdateField == "ValidFromDate" || UpdateField == "ValidToDate")
                {
                    Query = "update FlightMarkupDetail_Jazz set " + FieldName + "='" + Convert.ToDateTime(Value).ToString("yyyy-MM-dd") + "',Agnt_AirF_Markup_ModifiedBy='" + UpdetedBy + "',Agnt_AirF_Markup_LastModifiedDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' Where Agnt_AirF_Markup_ID=" + ID;
                }
                else
                {
                    Query = "update FlightMarkupDetail_Jazz set " + FieldName + "='" + Value.ToUpper() + "',Agnt_AirF_Markup_ModifiedBy='" + UpdetedBy + "',Agnt_AirF_Markup_LastModifiedDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' Where Agnt_AirF_Markup_ID=" + ID;
                }
            }
            using (SqlConnection connection = DataConnection.GetConnectionMarkupUS())
            {
                if (SqlHelper.ExecuteNonQuery(connection, CommandType.Text, Query) == 1)
                {
                    return "true";
                }
                else
                    return "false";

            }
        }
        catch (Exception ex) { return "false"; }
    }

    public string DeleteFlightMarkupJazz_US(string ID, string Counter)
    {
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnectionMarkupUS())
            {

                if (!string.IsNullOrEmpty(ID))
                {
                    param[0] = new SqlParameter("@ParamID", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(ID);
                }

                param[1] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[1].Value = Counter;

                param[2] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 500);
                param[2].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_FlightMarkupDetail_Jazz", param);
                return param[2].Value.ToString();
            }
        }
        catch
        {
            return "false";
        }
    }


    public bool DeleteAllFlightMarkupJazz_US(string Counter)
    {
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnectionMarkupUS())
            {
                param[0] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[0].Value = Counter;

                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_FlightMarkupDetail_Jazz", param);
                return true;
            }
        }
        catch
        {
            return false;
        }
    }

    #endregion



    #region Own Fare Jazz US For Lokesh 09 March 2021


    public bool CheckDiscountOwnFareJazz_US()
    {
        bool finalstatus = true;
        SqlConnection connection = DataConnection.GetConnectionMarkupUS();
        try
        {
            connection.Open();
            SqlCommand command = new SqlCommand("select top 1 * from ChangeFare_JazzUS where isActive='true'", connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                finalstatus = true;
            }
            else
            {
                finalstatus = false;
            }
        }
        catch (Exception ex)
        {
            return true;
        }
        finally
        {
            connection.Close();
        }
        return finalstatus;
    }

    public DataTable GET_ChangeFare_JazzUS(string SrNo, string Origin, string Destination, string FromDateStart, string FromDateEnd, string Airline, string CabinClass,
    string aClass, string TravelDate, string Counter, string ModifyBy, string ModifyDate, string Campaign, string journeyType, bool Active, int PaxCount, string DTD, string FlightType)
    {
        SqlParameter[] param = new SqlParameter[18];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnectionMarkupUS())
            {
                if (!string.IsNullOrEmpty(SrNo))
                {
                    param[0] = new SqlParameter("@ParamSrNo", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(SrNo);
                }
                if (!string.IsNullOrEmpty(Origin))
                {
                    param[1] = new SqlParameter("@ParamOrigin", SqlDbType.VarChar, 50);
                    param[1].Value = Origin;
                }
                if (!string.IsNullOrEmpty(Destination))
                {
                    param[2] = new SqlParameter("@ParamDestination", SqlDbType.VarChar, 50);
                    param[2].Value = Destination;
                }
                if (!string.IsNullOrEmpty(FromDateStart))
                {
                    param[3] = new SqlParameter("@ParamFromDateStart", SqlDbType.DateTime);
                    param[3].Value = Convert.ToDateTime(FromDateStart);
                }
                if (!string.IsNullOrEmpty(FromDateEnd))
                {
                    param[4] = new SqlParameter("@ParamFromDateEnd", SqlDbType.DateTime);
                    param[4].Value = Convert.ToDateTime(FromDateEnd);
                }
                if (!string.IsNullOrEmpty(Airline))
                {
                    param[5] = new SqlParameter("@ParamAirline", SqlDbType.VarChar, 50);
                    param[5].Value = Airline;
                }
                if (!string.IsNullOrEmpty(CabinClass))
                {
                    param[6] = new SqlParameter("@ParamCabinClass", SqlDbType.VarChar, 50);
                    param[6].Value = CabinClass;
                }
                if (!string.IsNullOrEmpty(aClass))
                {
                    param[7] = new SqlParameter("@ParamClass", SqlDbType.VarChar, 50);
                    param[7].Value = aClass;
                }
                if (!string.IsNullOrEmpty(TravelDate))
                {
                    param[8] = new SqlParameter("@ParamTravelDate", SqlDbType.DateTime);
                    param[8].Value = Convert.ToDateTime(TravelDate);
                }
                param[9] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[9].Value = Counter;

                if (!string.IsNullOrEmpty(ModifyBy))
                {
                    param[10] = new SqlParameter("@ParamModifyBy", SqlDbType.VarChar, 50);
                    param[10].Value = ModifyBy;
                }
                if (!string.IsNullOrEmpty(ModifyDate))
                {
                    param[11] = new SqlParameter("@ParamModifyDate", SqlDbType.DateTime);
                    param[11].Value = Convert.ToDateTime(ModifyDate);
                }
                if (!string.IsNullOrEmpty(Campaign))
                {
                    param[12] = new SqlParameter("@ParamCampaign", SqlDbType.VarChar, 100);
                    param[12].Value = Campaign;
                }
                if (!string.IsNullOrEmpty(journeyType))
                {
                    param[13] = new SqlParameter("@ParamTripType", SqlDbType.VarChar, 10);
                    param[13].Value = journeyType;
                }
                if (Active)
                {
                    param[14] = new SqlParameter("@ParamActive", SqlDbType.Bit);
                    param[14].Value = 1;
                }
                else
                {
                    param[14] = new SqlParameter("@ParamActive", SqlDbType.Bit);
                    param[14].Value = 0;
                }
                param[15] = new SqlParameter("@paramPaxCount", SqlDbType.Int, 10);
                param[15].Value = PaxCount;


                if (!string.IsNullOrEmpty(DTD))
                {
                    param[16] = new SqlParameter("@paramDTD", SqlDbType.Int, 10);
                    param[16].Value = Convert.ToInt32(DTD);
                }
                else
                {
                    param[16] = new SqlParameter("@paramDTD", SqlDbType.Int, 10);
                    param[16].Value = Convert.ToInt32("0");
                }


                param[17] = new SqlParameter("@ParamFlightType", SqlDbType.VarChar, 10);
                param[17].Value = FlightType;


                DataSet ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "GET_SET_ChangeFare_JazzUS", param);
                return ds.Tables[0];
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public string SET_ChangeFare_JazzUS(string SrNo, string Origin, string Destination, string FromDateStart, string FromDateEnd, string Airline, string CabinClass,
        string aClass, string BaseFare, string Tax, string Commission, string ModifyBy, string Counter, string Campaign,
        string offerType, string journeyType, string active, int PaxCount, string flightType, int DTD, string MarkUp)
    {
        string[] classes = aClass.Split(',');
        foreach (string cls in classes)
        {
            SqlParameter[] param = new SqlParameter[23];
            try
            {
                using (SqlConnection conection = DataConnection.GetConnectionMarkupUS())
                {

                    if (!string.IsNullOrEmpty(SrNo))
                    {
                        param[0] = new SqlParameter("@ParamSrNo", SqlDbType.Int);
                        param[0].Value = Convert.ToInt32(SrNo);
                    }
                    if (!string.IsNullOrEmpty(Origin))
                    {
                        param[1] = new SqlParameter("@ParamOrigin", SqlDbType.VarChar, 50);
                        param[1].Value = Origin;
                    }
                    if (!string.IsNullOrEmpty(Destination))
                    {
                        param[2] = new SqlParameter("@ParamDestination", SqlDbType.VarChar, 50);
                        param[2].Value = Destination;
                    }
                    if (!string.IsNullOrEmpty(FromDateStart))
                    {
                        param[3] = new SqlParameter("@ParamFromDateStart", SqlDbType.DateTime);
                        param[3].Value = Convert.ToDateTime(FromDateStart);
                    }
                    if (!string.IsNullOrEmpty(FromDateEnd))
                    {
                        param[4] = new SqlParameter("@ParamFromDateEnd", SqlDbType.DateTime);
                        param[4].Value = Convert.ToDateTime(FromDateEnd);
                    }
                    if (!string.IsNullOrEmpty(Airline))
                    {
                        param[5] = new SqlParameter("@ParamAirline", SqlDbType.VarChar, 50);
                        param[5].Value = Airline;
                    }
                    if (!string.IsNullOrEmpty(CabinClass))
                    {
                        param[6] = new SqlParameter("@ParamCabinClass", SqlDbType.VarChar, 50);
                        param[6].Value = CabinClass;
                    }
                    if (!string.IsNullOrEmpty(cls))
                    {
                        param[7] = new SqlParameter("@ParamClass", SqlDbType.VarChar, 50);
                        param[7].Value = cls;
                    }
                    if (!string.IsNullOrEmpty(BaseFare))
                    {
                        param[8] = new SqlParameter("@ParamBaseFare", SqlDbType.Money);
                        param[8].Value = Convert.ToDouble(BaseFare);
                    }
                    if (!string.IsNullOrEmpty(Tax))
                    {
                        param[9] = new SqlParameter("@ParamTax", SqlDbType.Money);
                        param[9].Value = Convert.ToDouble(Tax);
                    }

                    if (!string.IsNullOrEmpty(Commission))
                    {
                        param[10] = new SqlParameter("@ParamCommission", SqlDbType.Money);
                        param[10].Value = Convert.ToDouble(Commission);
                    }
                    if (!string.IsNullOrEmpty(ModifyBy))
                    {
                        param[11] = new SqlParameter("@ParamModifyBy", SqlDbType.VarChar, 50);
                        param[11].Value = ModifyBy;
                    }
                    param[12] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                    param[12].Value = Counter;

                    param[13] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 500);
                    param[13].Direction = ParameterDirection.Output;

                    if (!string.IsNullOrEmpty(Campaign))
                    {
                        param[14] = new SqlParameter("@ParamCampaign", SqlDbType.VarChar, 100);
                        param[14].Value = Campaign;
                    }
                    if (!string.IsNullOrEmpty(offerType))
                    {
                        param[15] = new SqlParameter("@ParamOfferType", SqlDbType.VarChar, 50);
                        param[15].Value = offerType;
                    }
                    if (!string.IsNullOrEmpty(journeyType))
                    {
                        param[16] = new SqlParameter("@ParamTripType", SqlDbType.VarChar, 10);
                        param[16].Value = journeyType;
                    }
                    if (active.ToLower() == "true" || active.ToLower() == "false")
                    {

                        if (active.ToLower() == "true")
                        {
                            param[17] = new SqlParameter("@ParamActive", SqlDbType.Bit);
                            param[17].Value = 1;
                        }
                        else
                        {
                            param[17] = new SqlParameter("@ParamActive", SqlDbType.Bit);
                            param[17].Value = 0;
                        }
                    }

                    param[18] = new SqlParameter("@paramPaxCount", SqlDbType.Int, 10);
                    param[18].Value = PaxCount;

                    param[19] = new SqlParameter("@ParamModifyDate", SqlDbType.DateTime, 40);
                    param[19].Value = DateTime.Now;

                    param[20] = new SqlParameter("@ParamFlightType", SqlDbType.VarChar, 50);
                    param[20].Value = flightType;

                    param[21] = new SqlParameter("@ParamDTD", SqlDbType.Int, 10);
                    param[21].Value = DTD;

                    param[22] = new SqlParameter("@ParamMarkup", SqlDbType.Decimal, 10);
                    param[22].Value = MarkUp;

                    SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_ChangeFare_JazzUS", param);
                    string output = param[13].Value.ToString();
                    if (output != "true")
                    {
                        return output;
                    }
                }
            }
            catch (Exception ex)
            {
                return "false";
            }
        }
        return "true";
    }


    public string DeleteOwnFares_JazzUS(string ID, string Counter)
    {
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnectionMarkupUS())
            {
                if (!string.IsNullOrEmpty(ID))
                {
                    param[0] = new SqlParameter("@ParamSrNo", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(ID);
                }

                param[1] = new SqlParameter("@Counter", SqlDbType.VarChar, 500);
                param[1].Value = Counter;

                param[2] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 500);
                param[2].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_ChangeFare_JazzUS", param);
                return param[2].Value.ToString();
            }
        }
        catch
        {
            return "false";
        }
    }


    public string UpdateDiscountOwnFare_JazzUS(bool UseStatus, string Origin, string Destination, string Airline, string AirlineClass,
        string TripType, string OfferType, string Pax, string FlightType, string Campaign)
    {
        try
        {
            using (SqlConnection connection = DataConnection.GetConnectionMarkupUS())
            {

                StringBuilder strcmd = new StringBuilder("UPDATE ChangeFare_JazzUS SET isActive = '" + UseStatus + "'");

                if (SqlHelper.ExecuteNonQuery(connection, CommandType.Text, strcmd.ToString()) >= 1)
                {
                    return "true";
                }
                else
                {
                    return "false";
                }
            }
        }
        catch (Exception ex)
        {
            return "false";
        }


    }



    public string UpdateOwnFare_US_Excel_JazzUS(string ID, string UpdateField, string Value, string UpdetedBy)
    {
        try
        {
            string FieldName = "";
            string result = string.Empty;
            switch (UpdateField)
            {
                case "BaseFare": FieldName = "Change_Fare_BaseFare"; break;
                case "Tax": FieldName = "Change_Fare_Tax"; break;
                case "Markup": FieldName = "Change_Fare_Markup"; break;
                case "Commission": FieldName = "Change_Fare_Commission"; break;
                case "FromDateStart": FieldName = "Change_Fare_FromDateStart"; break;
                case "FromDateEnd": FieldName = "Change_Fare_FromDateEnd"; break;
                case "Active": FieldName = "isActive"; break;
                case "FlightType": FieldName = "Change_Fare_Flight_type"; break;
                case "Airline": FieldName = "Change_Fare_Airline"; break;
                case "Origin": FieldName = "Change_Fare_Origin"; break;
                case "Destination": FieldName = "Change_Fare_Destination"; break;
                case "AirlineClass": FieldName = "Change_Fare_Class"; break;
                case "OfferType": FieldName = "Change_Fare_OfferType"; break;
                case "TripType": FieldName = "Change_Fare_TripType"; break;
                case "PaxCount": FieldName = "Change_Fare_No_Of_Pax"; break;
                case "DTD": FieldName = "Change_Fare_Days_To_Departure"; break;
            }
            string Query = "";

            if (UpdateField == "FromDateStart" || UpdateField == "FromDateEnd")
            {
                DateTime dt;

                if (DateTime.TryParse(Value, out dt))
                {
                    Query = "UPDATE ChangeFare_JazzUS set " + FieldName + "='" + Convert.ToDateTime(Value).ToString("yyyy-MM-dd") + "'," +
                        "Change_Fare_ModifyBy='" + UpdetedBy + "'," +
                        "Change_Fare_ModifyDateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                        "Where Change_Fare_SrNo=" + ID;
                }
                else
                {
                    result = "Invalid date format";
                }
            }
            else
            {
                Query = "UPDATE ChangeFare_JazzUS set " + FieldName + "='" + Value.ToUpper() + "'," +
                    "Change_Fare_ModifyBy='" + UpdetedBy + "'," +
                    "Change_Fare_ModifyDateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                    "Where Change_Fare_SrNo=" + ID;
            }

            using (SqlConnection connection = DataConnection.GetConnectionMarkupUS())
            {
                if (SqlHelper.ExecuteNonQuery(connection, CommandType.Text, Query) == 1)
                {
                    result = "true";
                }
                else
                {
                    result = "false";
                }
            }
            return result;
        }
        catch (Exception ex)
        {
            return "false";
        }
    }

    public string Update_Pax_Details(string BookingID, string ProdID, string PaxID, string PaxType, string PaxTitle,
      string PaxFirstName, string PaxMiddleName, string PaxLastName, string PaxTickets, string PaxDOB, string UpdatedBy)
    {

        SqlParameter[] param = new SqlParameter[12];

        using (SqlConnection conection = DataConnection.GetConnection())
        {

            param[0] = new SqlParameter("@ParamBookingID", SqlDbType.VarChar, 50);
            param[0].Value = BookingID;

            param[1] = new SqlParameter("@ParamProdID", SqlDbType.VarChar, 50);
            param[1].Value = ProdID;

            param[2] = new SqlParameter("@ParamPaxID", SqlDbType.VarChar, 50);
            param[2].Value = PaxID;

            param[3] = new SqlParameter("@ParamPaxType", SqlDbType.VarChar, 50);
            param[3].Value = PaxType;

            param[4] = new SqlParameter("@ParamPaxTitle", SqlDbType.VarChar, 50);
            param[4].Value = PaxTitle;

            param[5] = new SqlParameter("@ParamPaxFirstName", SqlDbType.VarChar, 50);
            param[5].Value = PaxFirstName;

            param[6] = new SqlParameter("@ParamPaxMiddleName", SqlDbType.VarChar, 50);
            param[6].Value = PaxMiddleName;

            param[7] = new SqlParameter("@ParamPaxLastName", SqlDbType.VarChar, 50);
            param[7].Value = PaxLastName;

            param[8] = new SqlParameter("@ParamPaxTickets", SqlDbType.VarChar, 50);
            param[8].Value = PaxTickets;

            param[9] = new SqlParameter("@ParamPaxDOB", SqlDbType.VarChar, 50);
            param[9].Value = PaxDOB;

            param[10] = new SqlParameter("@ParamUpdatedBy", SqlDbType.VarChar, 50);
            param[10].Value = UpdatedBy;

            param[11] = new SqlParameter("@ParamStatus", SqlDbType.VarChar, 500);
            param[11].Direction = ParameterDirection.Output;


            SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "Update_Passenger_Detail", param);
            return param[11].Value.ToString();
        }
    }

    #endregion
}

