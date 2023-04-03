using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.IO;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for HotelRes
/// </summary>
public class HotelRes
{
    public HotelRes()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public int HotelPackageAdd(string HTL_MSTR_HotID, string DATA_MSTR_ProdCode, string HTL_MSTR_CityCode, string HTL_MSTR_CityName, string HTL_MSTR_HotelCode,
                             string HTL_MSTR_HotelName, string DATA_MSTR_Category, string DATA_MSTR_Destination, string HTL_DTL_Description, string HTL_DTL_StarRating, string HTL_DTL_Category, string HTL_DTL_LocationCode, string HTL_DTL_LocationName,
                             string HTL_DTL_Address1, string HTL_DTL_Address2, string HTL_DTL_Area, string HTL_DTL_TelNo, string HTL_DTL_Fax, string HTL_DTL_EmailID, string HTL_DTL_Website, string HTL_DTL_Image,
                             DataTable HotelFacilities, DataTable HotelRooms, DataTable HotelRoomPrice, DataTable HotelRoomFacilities, DataTable Sector_Detail, string ModifyBy,
                             string Org, string Dest, string ValidatingCarrier, string CabinClass, string LastTktDate, string Journey_Type, string Country, string Company, string Heading, string savepercent, string comment, string startdate, string endDate, string soldout, string RemainingDays, string MapUrl, string HTL_DTL_AltTag)
    {

        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[46];
        try
        {
            _CommandText = "sp_Package_Hotel";

            param[0] = new SqlParameter("@DATA_MSTR_ID", SqlDbType.VarChar, 50);
            param[0].Value = HTL_MSTR_HotID;
            param[1] = new SqlParameter("@DATA_MSTR_ProdCode", SqlDbType.VarChar, 10);
            param[1].Value = DATA_MSTR_ProdCode;
            if (!string.IsNullOrEmpty(HTL_MSTR_CityCode))
            {
                param[2] = new SqlParameter("@DATA_MSTR_CityCode", SqlDbType.VarChar, 5);
                param[2].Value = HTL_MSTR_CityCode;
            }
            if (!string.IsNullOrEmpty(HTL_MSTR_CityName))
            {
                param[3] = new SqlParameter("@DATA_MSTR_CityName", SqlDbType.VarChar, 200);
                param[3].Value = HTL_MSTR_CityName;
            }
            if (!string.IsNullOrEmpty(HTL_MSTR_HotelCode))
            {
                param[4] = new SqlParameter("@DATA_MSTR_HotelCode", SqlDbType.VarChar, 5);
                param[4].Value = HTL_MSTR_HotelCode;
            }
            if (!string.IsNullOrEmpty(HTL_MSTR_HotelName))
            {
                param[5] = new SqlParameter("@DATA_MSTR_HotelName", SqlDbType.VarChar, 200);
                param[5].Value = HTL_MSTR_HotelName;
            }
            if (!string.IsNullOrEmpty(DATA_MSTR_Category))
            {
                param[6] = new SqlParameter("@DATA_MSTR_Category", SqlDbType.VarChar, 50);
                param[6].Value = DATA_MSTR_Category;
            }
            if (!string.IsNullOrEmpty(DATA_MSTR_Destination))
            {
                param[7] = new SqlParameter("@DATA_MSTR_Destination", SqlDbType.VarChar, 50);
                param[7].Value = DATA_MSTR_Destination;
            }
            if (!string.IsNullOrEmpty(HTL_DTL_Description))
            {
                param[8] = new SqlParameter("@HTL_DTL_Description", SqlDbType.Text);
                param[8].Value = HTL_DTL_Description;
            }
            if (!string.IsNullOrEmpty(HTL_DTL_StarRating))
            {
                param[9] = new SqlParameter("@HTL_DTL_StarRating", SqlDbType.VarChar, 2);
                param[9].Value = HTL_DTL_StarRating;
            }
            if (!string.IsNullOrEmpty(HTL_DTL_Category))
            {
                param[10] = new SqlParameter("@HTL_DTL_Category", SqlDbType.VarChar, 200);
                param[10].Value = HTL_DTL_Category;
            }
            if (!string.IsNullOrEmpty(HTL_DTL_LocationCode))
            {
                param[11] = new SqlParameter("@HTL_DTL_LocationCode", SqlDbType.VarChar, 5);
                param[11].Value = HTL_DTL_LocationCode;
            }
            if (!string.IsNullOrEmpty(HTL_DTL_LocationName))
            {
                param[12] = new SqlParameter("@HTL_DTL_LocationName", SqlDbType.VarChar, 500);
                param[12].Value = HTL_DTL_LocationName;
            }
            if (!string.IsNullOrEmpty(HTL_DTL_Address1))
            {
                param[13] = new SqlParameter("@HTL_DTL_Address1", SqlDbType.VarChar, 1000);
                param[13].Value = HTL_DTL_Address1;
            }
            if (!string.IsNullOrEmpty(HTL_DTL_Address2))
            {
                param[14] = new SqlParameter("@HTL_DTL_Address2", SqlDbType.VarChar, 1000);
                param[14].Value = HTL_DTL_Address2;
            }
            if (!string.IsNullOrEmpty(HTL_DTL_Area))
            {
                param[15] = new SqlParameter("@HTL_DTL_Area", SqlDbType.VarChar, 500);
                param[15].Value = HTL_DTL_Area;
            }
            if (!string.IsNullOrEmpty(HTL_DTL_TelNo))
            {
                param[16] = new SqlParameter("@HTL_DTL_TelNo", SqlDbType.VarChar, 20);
                param[16].Value = HTL_DTL_TelNo;
            }
            if (!string.IsNullOrEmpty(HTL_DTL_Fax))
            {
                param[17] = new SqlParameter("@HTL_DTL_Fax", SqlDbType.VarChar, 20);
                param[17].Value = HTL_DTL_Fax;
            }
            if (!string.IsNullOrEmpty(HTL_DTL_EmailID))
            {
                param[18] = new SqlParameter("@HTL_DTL_EmailID", SqlDbType.VarChar, 200);
                param[18].Value = HTL_DTL_EmailID;
            }
            if (!string.IsNullOrEmpty(HTL_DTL_Website))
            {
                param[19] = new SqlParameter("@HTL_DTL_Website", SqlDbType.VarChar, 200);
                param[19].Value = HTL_DTL_Website;
            }
            if (!string.IsNullOrEmpty(HTL_DTL_Image))
            {
                param[20] = new SqlParameter("@HTL_DTL_Image", SqlDbType.VarChar, 200);
                param[20].Value = HTL_DTL_Image;
            }
            param[21] = new SqlParameter("@paramHotelFacilities", HotelFacilities);
            param[22] = new SqlParameter("@paramHotelRooms", HotelRooms);
            param[23] = new SqlParameter("@paramHotelRoomPrice", HotelRoomPrice);
            param[24] = new SqlParameter("@paramHotelRoomFacilities", HotelRoomFacilities);
            param[25] = new SqlParameter("@paramSector_Detail", Sector_Detail);

            if (!string.IsNullOrEmpty(ModifyBy))
            {
                param[26] = new SqlParameter("@paramModifyBy", SqlDbType.VarChar, 50);
                param[26].Value = ModifyBy;
            }

            param[27] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[27].Value = "INSERT";
            param[28] = new SqlParameter("@paramStatusOut", SqlDbType.Int);
            param[28].Direction = ParameterDirection.Output;

            if (!string.IsNullOrEmpty(Org))
            {
                param[29] = new SqlParameter("@SEC_MST_Origin", SqlDbType.VarChar, 50);
                param[29].Value = Org;
            }
            if (!string.IsNullOrEmpty(Dest))
            {
                param[30] = new SqlParameter("@SEC_MST_Destination", SqlDbType.VarChar, 50);
                param[30].Value = Dest;
            }
            if (!string.IsNullOrEmpty(ValidatingCarrier))
            {
                param[31] = new SqlParameter("@SEC_MST_Validating_Carrier", SqlDbType.VarChar, 50);
                param[31].Value = ValidatingCarrier;
            }
            if (!string.IsNullOrEmpty(CabinClass))
            {
                param[32] = new SqlParameter("@SEC_MST_CabinClass", SqlDbType.VarChar, 50);
                param[32].Value = CabinClass;
            }
            if (!string.IsNullOrEmpty(LastTktDate))
            {
                param[33] = new SqlParameter("@SEC_MST_Last_Tkt_Date", SqlDbType.VarChar, 50);
                param[33].Value = LastTktDate;
            }
            if (!string.IsNullOrEmpty(Journey_Type))
            {
                param[34] = new SqlParameter("@SEC_MST_Journey_Type", SqlDbType.VarChar, 50);
                param[34].Value = Journey_Type;
            }
            if (!string.IsNullOrEmpty(Country))
            {
                param[35] = new SqlParameter("@DATA_MSTR_Country", SqlDbType.VarChar, 50);
                param[35].Value = Country;
            }
            if (!string.IsNullOrEmpty(Company))
            {
                param[36] = new SqlParameter("@DATA_MSTR_Company", SqlDbType.VarChar, 50);
                param[36].Value = Company;
            }
            if (!string.IsNullOrEmpty(Heading))
            {
                param[37] = new SqlParameter("@DATA_MSTR_Heading", SqlDbType.VarChar, 500);
                param[37].Value = Heading;
            }
            if (!string.IsNullOrEmpty(savepercent))
            {
                param[38] = new SqlParameter("@DATA_MSTR_SavePercent", SqlDbType.Money);
                param[38].Value = Convert.ToDouble(savepercent);
            }
            if (!string.IsNullOrEmpty(comment))
            {
                param[39] = new SqlParameter("@DATA_MSTR_Comment", SqlDbType.VarChar);
                param[39].Value = comment;
            }
            if (!string.IsNullOrEmpty(startdate))
            {
                param[40] = new SqlParameter("@DATA_MSTR_StartDate", SqlDbType.DateTime);
                param[40].Value = Convert.ToDateTime(startdate);
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                param[41] = new SqlParameter("@DATA_MSTR_EndDate", SqlDbType.DateTime);
                param[41].Value = Convert.ToDateTime(endDate);
            }

            if (!string.IsNullOrEmpty(soldout))
            {
                param[42] = new SqlParameter("@DATA_MSTR_SoldOut", SqlDbType.Int);
                param[42].Value = Convert.ToInt16(soldout);
            }
            if (!string.IsNullOrEmpty(RemainingDays))
            {
                param[43] = new SqlParameter("@DATA_MSTR_RemainingDays", SqlDbType.Int);
                param[43].Value = Convert.ToInt16(RemainingDays);
            }
            if (!string.IsNullOrEmpty(MapUrl))
            {
                param[44] = new SqlParameter("@DATA_MSTR_MapURL", SqlDbType.NVarChar);
                param[44].Value = MapUrl;
            }
            if (!string.IsNullOrEmpty(HTL_DTL_AltTag))
            {
                param[45] = new SqlParameter("@HTL_DTL_AltTag", SqlDbType.VarChar);
                param[45].Value = HTL_DTL_AltTag;
            }

            int iM = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            return Convert.ToInt16(param[28].Value);


        }
        catch
        {
            return 0;
        }

    }

    public bool HotelMasterAdd(string HTL_MSTR_HotID, string DATA_MSTR_ProdCode, string HTL_MSTR_CityCode, string HTL_MSTR_CityName, string HTL_MSTR_HotelCode, string HTL_MSTR_HotelName, string DATA_MSTR_Category, string DATA_MSTR_Destination)
    {
        bool bMod = false;
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[9];
        try
        {
            _CommandText = "HotelReservationInsert";

            param[0] = new SqlParameter("@DATA_MSTR_ID", SqlDbType.VarChar, 50);
            param[0].Value = HTL_MSTR_HotID;
            param[1] = new SqlParameter("@DATA_MSTR_ProdCode", SqlDbType.VarChar, 10);
            param[1].Value = DATA_MSTR_ProdCode;
            param[2] = new SqlParameter("@DATA_MSTR_CityCode", SqlDbType.VarChar, 5);
            param[2].Value = HTL_MSTR_CityCode;
            param[3] = new SqlParameter("@DATA_MSTR_CityName", SqlDbType.VarChar, 200);
            param[3].Value = HTL_MSTR_CityName;
            param[4] = new SqlParameter("@DATA_MSTR_HotelCode", SqlDbType.VarChar, 5);
            param[4].Value = HTL_MSTR_HotelCode;
            param[5] = new SqlParameter("@DATA_MSTR_HotelName", SqlDbType.VarChar, 200);
            param[5].Value = HTL_MSTR_HotelName;

            param[6] = new SqlParameter("@DATA_MSTR_Category", SqlDbType.VarChar, 50);
            param[6].Value = DATA_MSTR_Category;
            param[7] = new SqlParameter("@DATA_MSTR_Destination", SqlDbType.VarChar, 50);
            param[7].Value = DATA_MSTR_Destination;

            param[8] = new SqlParameter("@Counter", SqlDbType.Int);
            param[8].Value = 1;

            int iM = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            if (iM != -1)
            {
                bMod = true;
            }
            return bMod;
        }
        catch
        {
            return bMod;
        }

    }

    public bool HotelDetailsAdd(string HTL_MSTR_HotID, string HTL_DTL_Description, string HTL_DTL_StarRating, string HTL_DTL_Category, string HTL_DTL_LocationCode, string HTL_DTL_LocationName,
            string HTL_DTL_Address1, string HTL_DTL_Address2, string HTL_DTL_Area, string HTL_DTL_TelNo, string HTL_DTL_Fax, string HTL_DTL_EmailID, string HTL_DTL_Website, string HTL_DTL_Image)
    {
        bool bMod = false;
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[15];
        try
        {
            _CommandText = "HotelReservationInsert";

            param[0] = new SqlParameter("@DATA_MSTR_ID", SqlDbType.VarChar, 50);
            param[0].Value = HTL_MSTR_HotID;
            param[1] = new SqlParameter("@HTL_DTL_Description", SqlDbType.Text);
            param[1].Value = HTL_DTL_Description;
            param[2] = new SqlParameter("@HTL_DTL_StarRating", SqlDbType.VarChar, 2);
            param[2].Value = HTL_DTL_StarRating;
            param[3] = new SqlParameter("@HTL_DTL_Category", SqlDbType.VarChar, 200);
            param[3].Value = HTL_DTL_Category;
            param[4] = new SqlParameter("@HTL_DTL_LocationCode", SqlDbType.VarChar, 5);
            param[4].Value = HTL_DTL_LocationCode;
            param[5] = new SqlParameter("@HTL_DTL_LocationName", SqlDbType.VarChar, 500);
            param[5].Value = HTL_DTL_LocationName;
            param[6] = new SqlParameter("@HTL_DTL_Address1", SqlDbType.VarChar, 1000);
            param[6].Value = HTL_DTL_Address1;
            param[7] = new SqlParameter("@HTL_DTL_Address2", SqlDbType.VarChar, 1000);
            param[7].Value = HTL_DTL_Address2;
            param[8] = new SqlParameter("@HTL_DTL_Area", SqlDbType.VarChar, 500);
            param[8].Value = HTL_DTL_Area;
            param[9] = new SqlParameter("@HTL_DTL_TelNo", SqlDbType.VarChar, 20);
            param[9].Value = HTL_DTL_TelNo;
            param[10] = new SqlParameter("@HTL_DTL_Fax", SqlDbType.VarChar, 20);
            param[10].Value = HTL_DTL_Fax;
            param[11] = new SqlParameter("@HTL_DTL_EmailID", SqlDbType.VarChar, 200);
            param[11].Value = HTL_DTL_EmailID;
            param[12] = new SqlParameter("@HTL_DTL_Website", SqlDbType.VarChar, 200);
            param[12].Value = HTL_DTL_Website;

            param[13] = new SqlParameter("@HTL_DTL_Image", SqlDbType.VarChar, 200);
            param[13].Value = HTL_DTL_Image;

            param[14] = new SqlParameter("@Counter", SqlDbType.Int);
            param[14].Value = 2;

            int iM = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            if (iM != -1)
            {
                bMod = true;
            }
            return bMod;
        }
        catch
        {
            return bMod;
        }

    }


    public bool HotelFacilitiesAdd(string HTL_MSTR_HotID, string HTL_FAC_Code, string HTL_FAC_Name, string Modifyby)
    {
        bool bMod = false;
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[5];
        try
        {
            _CommandText = "HotelReservationInsert";

            param[0] = new SqlParameter("@DATA_MSTR_ID", SqlDbType.VarChar, 50);
            param[0].Value = HTL_MSTR_HotID;
            if (!string.IsNullOrEmpty(HTL_FAC_Code))
            {
                param[1] = new SqlParameter("@HTL_FAC_Code", SqlDbType.VarChar, 5);
                param[1].Value = HTL_FAC_Code;
            }
            param[2] = new SqlParameter("@HTL_FAC_Name", SqlDbType.VarChar, 200);
            param[2].Value = HTL_FAC_Name;
            param[3] = new SqlParameter("@paramModifyby", SqlDbType.VarChar, 50);
            param[3].Value = Modifyby;

            param[4] = new SqlParameter("@Counter", SqlDbType.Int);
            param[4].Value = 3;

            int iM = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            if (iM != -1)
            {
                bMod = true;
            }
            return bMod;
        }
        catch
        {
            return bMod;
        }

    }

    public bool HotelRoomsAdd(string HTL_MSTR_HotID, string HTL_RM_ID, string HTL_RM_Code, string HTL_RM_Name, string HTL_RM_Board, string HTL_RM_FromDate, string HTL_RM_ToDate, string Adt, string Chd, string Inf, bool MON, bool TUE, bool WED, bool THU, bool FRI, bool SAT, bool SUN, string Modifyby)
    {
        bool bMod = false;
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[19];
        try
        {
            _CommandText = "HotelReservationInsert";

            param[0] = new SqlParameter("@DATA_MSTR_ID", SqlDbType.VarChar, 50);
            param[0].Value = HTL_MSTR_HotID;
            param[1] = new SqlParameter("@HTL_RM_ID", SqlDbType.VarChar, 50);
            param[1].Value = HTL_RM_ID;
            param[2] = new SqlParameter("@HTL_RM_Code", SqlDbType.VarChar, 5);
            param[2].Value = HTL_RM_Code;
            param[3] = new SqlParameter("@HTL_RM_Name", SqlDbType.VarChar, 200);
            param[3].Value = HTL_RM_Name;
            param[4] = new SqlParameter("@HTL_RM_BoardType", SqlDbType.VarChar, 200);
            param[4].Value = HTL_RM_Board;
            if (!string.IsNullOrEmpty(HTL_RM_FromDate))
            {
                param[5] = new SqlParameter("@HTL_RM_FromDate", SqlDbType.DateTime);
                param[5].Value = Convert.ToDateTime(HTL_RM_FromDate);
            }
            if (!string.IsNullOrEmpty(HTL_RM_ToDate))
            {
                param[6] = new SqlParameter("@HTL_RM_ToDate", SqlDbType.DateTime);
                param[6].Value = Convert.ToDateTime(HTL_RM_ToDate);
            }
            param[7] = new SqlParameter("@HTL_RM_Adt", SqlDbType.Int);
            param[7].Value = Adt;
            param[8] = new SqlParameter("@HTL_RM_Chd", SqlDbType.Int);
            param[8].Value = Chd;
            param[9] = new SqlParameter("@HTL_RM_Inf", SqlDbType.Int);
            param[9].Value = Inf;
            param[10] = new SqlParameter("@HTL_RM_MON", SqlDbType.Bit);
            param[10].Value = MON;
            param[11] = new SqlParameter("@HTL_RM_TUE", SqlDbType.Bit);
            param[11].Value = TUE;
            param[12] = new SqlParameter("@HTL_RM_WED", SqlDbType.Bit);
            param[12].Value = WED;
            param[13] = new SqlParameter("@HTL_RM_THU", SqlDbType.Bit);
            param[13].Value = THU;
            param[14] = new SqlParameter("@HTL_RM_FRI", SqlDbType.Bit);
            param[14].Value = FRI;
            param[15] = new SqlParameter("@HTL_RM_SAT", SqlDbType.Bit);
            param[15].Value = SAT;
            param[16] = new SqlParameter("@HTL_RM_SUN", SqlDbType.Bit);
            param[16].Value = SUN;
            param[17] = new SqlParameter("@paramModifyby", SqlDbType.VarChar);
            param[17].Value = Modifyby;
            param[18] = new SqlParameter("@Counter", SqlDbType.Int);
            param[18].Value = 4;

            int iM = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            if (iM != -1)
            {
                bMod = true;
            }
            return bMod;
        }
        catch (Exception ee)
        {

            return bMod;
        }

    }

    public bool HotelRoomPrice(string HTL_MSTR_HotID, string HTL_RM_PRC_RoomID, string HTL_RM_PRC_BaseFare, string HTL_RM_PRC_Taxes, string HTL_RM_PRC_MarkUp, string Modifyby)
    {
        bool bMod = false;
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[7];
        try
        {
            _CommandText = "HotelReservationInsert";

            param[0] = new SqlParameter("@DATA_MSTR_ID", SqlDbType.VarChar, 50);
            param[0].Value = HTL_MSTR_HotID;
            param[1] = new SqlParameter("@HTL_RM_ID", SqlDbType.VarChar, 50);
            param[1].Value = HTL_RM_PRC_RoomID;
            if (!string.IsNullOrEmpty(HTL_RM_PRC_BaseFare))
            {
                param[2] = new SqlParameter("@HTL_RM_PRC_BaseFare", SqlDbType.Money);
                param[2].Value = Convert.ToSingle(HTL_RM_PRC_BaseFare);

            }
            if (!string.IsNullOrEmpty(HTL_RM_PRC_Taxes))
            {
                param[3] = new SqlParameter("@HTL_RM_PRC_Taxes", SqlDbType.Money);
                param[3].Value = Convert.ToSingle(HTL_RM_PRC_Taxes);
            }
            if (!string.IsNullOrEmpty(HTL_RM_PRC_MarkUp))
            {
                param[4] = new SqlParameter("@HTL_RM_PRC_MarkUp", SqlDbType.Money);
                param[4].Value = Convert.ToSingle(HTL_RM_PRC_MarkUp);
            }
            param[5] = new SqlParameter("@paramModifyby", SqlDbType.VarChar);
            param[5].Value = Modifyby;
            param[6] = new SqlParameter("@Counter", SqlDbType.Int);
            param[6].Value = 5;

            int iM = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            if (iM != -1)
            {
                bMod = true;
            }
            return bMod;
        }
        catch
        {
            return bMod;
        }

    }

    public bool HotelRoomFacilities(string HTL_MSTR_HotID, string HTL_RM_ID, string HTL_RM_FAC_Code, string HTL_RM_FAC_Name, string ModifyBy)
    {
        bool bMod = false;
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[6];
        try
        {
            _CommandText = "HotelReservationInsert";

            param[0] = new SqlParameter("@DATA_MSTR_ID", SqlDbType.VarChar, 50);
            param[0].Value = HTL_MSTR_HotID;
            param[1] = new SqlParameter("@HTL_RM_ID", SqlDbType.VarChar, 50);
            param[1].Value = HTL_RM_ID;
            param[2] = new SqlParameter("@HTL_RM_FAC_Code", SqlDbType.VarChar, 5);
            param[2].Value = HTL_RM_FAC_Code;
            param[3] = new SqlParameter("@HTL_RM_FAC_Name", SqlDbType.VarChar, 200);
            param[3].Value = HTL_RM_FAC_Name;
            param[4] = new SqlParameter("@paramModifyby", SqlDbType.VarChar, 50);
            param[4].Value = ModifyBy;
            param[5] = new SqlParameter("@Counter", SqlDbType.Int);
            param[5].Value = 6;

            int iM = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            if (iM != -1)
            {
                bMod = true;
            }
            return bMod;
        }
        catch
        {
            return bMod;
        }
    }

    public bool HotelImages(string HTL_MSTR_HotID, string HTL_IMG_Text, string HTL_IMG_IMGPath, string HTL_IMG_ThumbNailPath, string DATA_MSTR_Category, string DATA_MSTR_Destination, string Modifyby, string Company, string alttag)
    {
        bool bMod = false;
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[10];
        try
        {
            _CommandText = "HotelReservationInsert";
            param[0] = new SqlParameter("@DATA_MSTR_ID", SqlDbType.VarChar, 50);
            param[0].Value = HTL_MSTR_HotID;
            param[1] = new SqlParameter("@HTL_IMG_Text", SqlDbType.VarChar, 100);
            param[1].Value = HTL_IMG_Text;
            param[2] = new SqlParameter("@HTL_IMG_IMGPath", SqlDbType.VarChar, 200);
            param[2].Value = HTL_IMG_IMGPath;
            param[3] = new SqlParameter("@HTL_IMG_ThumbNailPath", SqlDbType.VarChar, 200);
            param[3].Value = HTL_IMG_ThumbNailPath;
            param[4] = new SqlParameter("@DATA_MSTR_Category", SqlDbType.VarChar, 50);
            param[4].Value = DATA_MSTR_Category;
            param[5] = new SqlParameter("@DATA_MSTR_Destination", SqlDbType.VarChar, 50);
            param[5].Value = DATA_MSTR_Destination;
            param[6] = new SqlParameter("@paramModifyby", SqlDbType.VarChar, 50);
            param[6].Value = Modifyby;
            param[7] = new SqlParameter("@HTL_IMG_Company", SqlDbType.VarChar, 50);
            param[7].Value = Company;
            param[8] = new SqlParameter("@HTL_AltTag", SqlDbType.VarChar, 200);
            param[8].Value = alttag;
            param[9] = new SqlParameter("@Counter", SqlDbType.Int);
            param[9].Value = 7;
            int iM = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            if (iM != -1)
            {
                bMod = true;
            }
            return bMod;
        }
        catch
        {
            return bMod;
        }

    }

    public DataTable GetHotelDetails(string HotelID, string HotelName, string Rating, string productCode, string Category, string Destination, string country,
        string RefNO, string startFrom, string startTo, string EndFrom, string EndTo, string company)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[14];
        try
        {
            _CommandText = "HotelsRoomsDetailsEdit";
            if (!String.IsNullOrEmpty(HotelID))
            {
                param[0] = new SqlParameter("@DATA_MSTR_ID", SqlDbType.VarChar, 50);
                param[0].Value = HotelID;
            }
            if (!String.IsNullOrEmpty(HotelName))
            {
                param[1] = new SqlParameter("@DATA_MSTR_HotelName", SqlDbType.VarChar, 500);
                param[1].Value = HotelName;
            }
            if (!String.IsNullOrEmpty(Rating))
            {
                param[2] = new SqlParameter("@HTL_DTL_StarRating", SqlDbType.VarChar, 2);
                param[2].Value = Rating;
            }

            if (!String.IsNullOrEmpty(productCode))
            {
                param[3] = new SqlParameter("@DATA_MSTR_ProdCode", SqlDbType.VarChar);
                param[3].Value = productCode;
            }
            if (!String.IsNullOrEmpty(Category))
            {
                param[4] = new SqlParameter("@DATA_MSTR_Category", SqlDbType.VarChar, 50);
                param[4].Value = Category;
            }
            if (!String.IsNullOrEmpty(Destination))
            {
                param[5] = new SqlParameter("@DATA_MSTR_Destination", SqlDbType.VarChar, 50);
                param[5].Value = Destination;
            }
            if (!String.IsNullOrEmpty(country))
            {
                param[6] = new SqlParameter("@DATA_MSTR_Country", SqlDbType.VarChar, 50);
                param[6].Value = country;
            }

            if (!String.IsNullOrEmpty(RefNO))
            {
                param[7] = new SqlParameter("@DATA_MSTR_RefNO", SqlDbType.VarChar, 50);
                param[7].Value = RefNO;
            }

            if (!String.IsNullOrEmpty(startFrom))
            {
                param[8] = new SqlParameter("@DATA_MSTR_StartDate", SqlDbType.DateTime);
                param[8].Value = Convert.ToDateTime(startFrom);
            }
            if (!String.IsNullOrEmpty(startTo))
            {
                param[9] = new SqlParameter("@DATA_MSTR_StartDateE", SqlDbType.DateTime);
                param[9].Value = Convert.ToDateTime(startTo);
            }
            if (!String.IsNullOrEmpty(EndFrom))
            {
                param[10] = new SqlParameter("@DATA_MSTR_EndDate", SqlDbType.DateTime);
                param[10].Value = Convert.ToDateTime(EndFrom);
            }
            if (!String.IsNullOrEmpty(EndTo))
            {
                param[11] = new SqlParameter("@DATA_MSTR_EndDateE", SqlDbType.DateTime);
                param[11].Value = Convert.ToDateTime(EndTo);
            }
            if (!String.IsNullOrEmpty(company))
            {
                param[12] = new SqlParameter("@DATA_MSTR_Company", SqlDbType.VarChar);
                param[12].Value = company;
            }
            param[13] = new SqlParameter("@Counter", SqlDbType.Int);
            param[13].Value = 1;

            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
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

    public DataTable GetHotelImages(string HotelID, string Category, string Destination, string date, string Company)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[6];
        try
        {
            _CommandText = "HotelsRoomsDetailsEdit";
            if (!String.IsNullOrEmpty(HotelID))
            {
                param[0] = new SqlParameter("@DATA_MSTR_ID", SqlDbType.VarChar, 50);
                param[0].Value = HotelID;
            }
            if (!String.IsNullOrEmpty(Category))
            {
                param[1] = new SqlParameter("@DATA_MSTR_Category", SqlDbType.VarChar, 50);
                param[1].Value = Category;
            }
            if (!String.IsNullOrEmpty(Destination))
            {
                param[2] = new SqlParameter("@DATA_MSTR_Destination", SqlDbType.VarChar, 50);
                param[2].Value = Destination;
            }
            if (!String.IsNullOrEmpty(date))
            {
                param[3] = new SqlParameter("@paramModifyDate", SqlDbType.DateTime);
                param[3].Value = Convert.ToDateTime(date);
            }
            if (!String.IsNullOrEmpty(Company))
            {
                param[4] = new SqlParameter("@DATA_MSTR_Company", SqlDbType.VarChar, 50);
                param[4].Value = Company;
            }

            param[5] = new SqlParameter("@Counter", SqlDbType.Int);
            param[5].Value = 3;

            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
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

    public DataTable GetHotelFacilities(string HotelID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            _CommandText = "HotelsRoomsDetailsEdit";
            if (!String.IsNullOrEmpty(HotelID))
            {
                param[0] = new SqlParameter("@DATA_MSTR_ID", SqlDbType.VarChar, 50);
                param[0].Value = HotelID;
            }

            param[1] = new SqlParameter("@Counter", SqlDbType.Int);
            param[1].Value = 2;

            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
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

    public DataTable GetHotelRoomDetails(string HotelID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            _CommandText = "HotelsRoomsDetailsEdit";
            if (!String.IsNullOrEmpty(HotelID))
            {
                param[0] = new SqlParameter("@DATA_MSTR_ID", SqlDbType.VarChar, 50);
                param[0].Value = HotelID;
            }

            param[1] = new SqlParameter("@Counter", SqlDbType.Int);
            param[1].Value = 4;

            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
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

    public DataTable HotelRoomFacilities(string HotelID, string RoomID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            _CommandText = "HotelsRoomsDetailsEdit";
            if (!String.IsNullOrEmpty(HotelID))
            {
                param[0] = new SqlParameter("@DATA_MSTR_ID", SqlDbType.VarChar, 50);
                param[0].Value = HotelID;
            }
            if (!String.IsNullOrEmpty(RoomID))
            {
                param[1] = new SqlParameter("@HTL_RM_ID", SqlDbType.VarChar, 50);
                param[1].Value = RoomID;
            }
            param[2] = new SqlParameter("@Counter", SqlDbType.Int);
            param[2].Value = 5;

            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
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

    public bool UpdateHotelMaster(string HTL_MSTR_HotID, string HTL_MSTR_CityCode, string HTL_MSTR_CityName, string HTL_MSTR_HotelCode, string HTL_MSTR_HotelName, string ModifyBy, string Heading, string savepercent, string comment, string startdate, string endDate, string soldout, string RemainingDays, string MapUrl, string company)
    {
        bool bMod = false;
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[16];
        try
        {
            _CommandText = "HotelsRoomsDetailsEdit";

            param[0] = new SqlParameter("@DATA_MSTR_ID", SqlDbType.VarChar, 50);
            param[0].Value = HTL_MSTR_HotID;
            param[1] = new SqlParameter("@DATA_MSTR_CityCode", SqlDbType.VarChar, 5);
            param[1].Value = HTL_MSTR_CityCode;
            param[2] = new SqlParameter("@DATA_MSTR_CityName", SqlDbType.VarChar, 200);
            param[2].Value = HTL_MSTR_CityName;
            param[3] = new SqlParameter("@DATA_MSTR_HotelCode", SqlDbType.VarChar, 5);
            param[3].Value = HTL_MSTR_HotelCode;
            param[4] = new SqlParameter("@DATA_MSTR_HotelName", SqlDbType.VarChar, 200);
            param[4].Value = HTL_MSTR_HotelName;
            param[5] = new SqlParameter("@paramModifyBy", SqlDbType.VarChar, 50);
            param[5].Value = ModifyBy;
            if (!string.IsNullOrEmpty(Heading))
            {
                param[6] = new SqlParameter("@DATA_MSTR_Heading", SqlDbType.VarChar, 500);
                param[6].Value = Heading;
            }
            if (!string.IsNullOrEmpty(savepercent))
            {
                param[7] = new SqlParameter("@DATA_MSTR_SavePercent", SqlDbType.Money);
                param[7].Value = Convert.ToDouble(savepercent);
            }
            if (!string.IsNullOrEmpty(comment))
            {
                param[8] = new SqlParameter("@DATA_MSTR_Comment", SqlDbType.VarChar);
                param[8].Value = comment;
            }
            if (!string.IsNullOrEmpty(startdate))
            {
                param[9] = new SqlParameter("@DATA_MSTR_StartDate", SqlDbType.DateTime);
                param[9].Value = Convert.ToDateTime(startdate);
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                param[10] = new SqlParameter("@DATA_MSTR_EndDate", SqlDbType.DateTime);
                param[10].Value = Convert.ToDateTime(endDate);
            }
            if (!string.IsNullOrEmpty(soldout))
            {
                param[11] = new SqlParameter("@DATA_MSTR_SoldOut", SqlDbType.Int);
                param[11].Value = Convert.ToInt16(soldout);
            }
            if (!string.IsNullOrEmpty(RemainingDays))
            {
                param[12] = new SqlParameter("@DATA_MSTR_RemainingDays", SqlDbType.Int);
                param[12].Value = Convert.ToInt16(RemainingDays);
            }
            if (!string.IsNullOrEmpty(MapUrl))
            {
                param[13] = new SqlParameter("@DATA_MSTR_MapURL", SqlDbType.NVarChar);
                param[13].Value = MapUrl;
            }
            if (!string.IsNullOrEmpty(company))
            {
                param[14] = new SqlParameter("@DATA_MSTR_Company", SqlDbType.NVarChar);
                param[14].Value = company;
            }

            param[15] = new SqlParameter("@Counter", SqlDbType.Int);
            param[15].Value = 6;

            int iM = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            if (iM != -1)
            {
                bMod = true;
            }
            return bMod;
        }
        catch
        {
            return bMod;
        }

    }


    public bool UpdateHotelDetails(string HTL_MSTR_HotID, string HTL_DTL_Description, string HTL_DTL_StarRating, string HTL_DTL_Category, string HTL_DTL_LocationCode, string HTL_DTL_LocationName,
        string HTL_DTL_Address1, string HTL_DTL_Address2, string HTL_DTL_Area, string HTL_DTL_TelNo, string HTL_DTL_Fax, string HTL_DTL_EmailID, string HTL_DTL_Website, string Image, string ModifyBy, string HTL_DTL_AltTag)
    {
        bool bMod = false;
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[17];
        try
        {
            _CommandText = "HotelsRoomsDetailsEdit";

            param[0] = new SqlParameter("@DATA_MSTR_ID", SqlDbType.VarChar, 50);
            param[0].Value = HTL_MSTR_HotID;
            if (!String.IsNullOrEmpty(HTL_DTL_Description))
            {
                param[1] = new SqlParameter("@HTL_DTL_Description", SqlDbType.Text);
                param[1].Value = HTL_DTL_Description;
            }
            if (!String.IsNullOrEmpty(HTL_DTL_StarRating))
            {
                param[2] = new SqlParameter("@HTL_DTL_StarRating", SqlDbType.VarChar, 2);
                param[2].Value = HTL_DTL_StarRating;
            }
            if (!String.IsNullOrEmpty(HTL_DTL_Category))
            {
                param[3] = new SqlParameter("@HTL_DTL_Category", SqlDbType.VarChar, 200);
                param[3].Value = HTL_DTL_Category;
            }
            if (!String.IsNullOrEmpty(HTL_DTL_LocationCode))
            {
                param[4] = new SqlParameter("@HTL_DTL_LocationCode", SqlDbType.VarChar, 5);
                param[4].Value = HTL_DTL_LocationCode;
            }
            if (!String.IsNullOrEmpty(HTL_DTL_LocationName))
            {

                param[5] = new SqlParameter("@HTL_DTL_LocationName", SqlDbType.VarChar, 500);
                param[5].Value = HTL_DTL_LocationName;
            }
            if (!String.IsNullOrEmpty(HTL_DTL_Address1))
            {
                param[6] = new SqlParameter("@HTL_DTL_Address1", SqlDbType.VarChar, 1000);
                param[6].Value = HTL_DTL_Address1;
            }
            if (!String.IsNullOrEmpty(HTL_DTL_Address2))
            {
                param[7] = new SqlParameter("@HTL_DTL_Address2", SqlDbType.VarChar, 1000);
                param[7].Value = HTL_DTL_Address2;
            }
            if (!String.IsNullOrEmpty(HTL_DTL_Area))
            {
                param[8] = new SqlParameter("@HTL_DTL_Area", SqlDbType.VarChar, 500);
                param[8].Value = HTL_DTL_Area;
            }
            if (!String.IsNullOrEmpty(HTL_DTL_TelNo))
            {
                param[9] = new SqlParameter("@HTL_DTL_TelNo", SqlDbType.VarChar, 20);
                param[9].Value = HTL_DTL_TelNo;
            }
            if (!String.IsNullOrEmpty(HTL_DTL_Fax))
            {
                param[10] = new SqlParameter("@HTL_DTL_Fax", SqlDbType.VarChar, 20);
                param[10].Value = HTL_DTL_Fax;
            }
            if (!String.IsNullOrEmpty(HTL_DTL_EmailID))
            {
                param[11] = new SqlParameter("@HTL_DTL_EmailID", SqlDbType.VarChar, 200);
                param[11].Value = HTL_DTL_EmailID;
            }
            if (!String.IsNullOrEmpty(HTL_DTL_Website))
            {
                param[12] = new SqlParameter("@HTL_DTL_Website", SqlDbType.VarChar, 200);
                param[12].Value = HTL_DTL_Website;
            }
            if (!String.IsNullOrEmpty(Image))
            {
                param[13] = new SqlParameter("@HTL_IMG_IMGPath", SqlDbType.VarChar, 200);
                param[13].Value = Image;
            }
            param[14] = new SqlParameter("@paramModifyBy", SqlDbType.VarChar, 50);
            param[14].Value = ModifyBy;
            if (!String.IsNullOrEmpty(HTL_DTL_AltTag))
            {
                param[15] = new SqlParameter("@HTL_DTL_AltTag", SqlDbType.VarChar, 200);
                param[15].Value = HTL_DTL_AltTag;
            }
            param[16] = new SqlParameter("@Counter", SqlDbType.Int);
            param[16].Value = 7;

            int iM = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            if (iM != -1)
            {
                bMod = true;
            }
            return bMod;
        }
        catch
        {
            return bMod;
        }

    }

    public bool UpdateHotelFac(string FacID, string FName, string modifyBy)
    {
        bool bMod = false;
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[4];
        try
        {
            _CommandText = "HotelsRoomsDetailsEdit";

            param[0] = new SqlParameter("@HTL_FAC_ID", SqlDbType.Int);
            param[0].Value = FacID;
            param[1] = new SqlParameter("@HTL_FAC_Name", SqlDbType.VarChar, 200);
            param[1].Value = FName;
            param[2] = new SqlParameter("@paramModifyBy", SqlDbType.VarChar, 50);
            param[2].Value = modifyBy;
            param[3] = new SqlParameter("@Counter", SqlDbType.Int);
            param[3].Value = 8;

            int iM = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            if (iM != -1)
            {
                bMod = true;
            }
            return bMod;
        }
        catch
        {
            return bMod;
        }

    }


    public bool UpdateHotelImages(string ImgID, string ImgText, string ImgPath, string ThumbPath)
    {
        bool bMod = false;
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[5];
        try
        {
            _CommandText = "HotelsRoomsDetailsEdit";

            param[0] = new SqlParameter("@HTL_IMG_ID", SqlDbType.Int);
            param[0].Value = ImgID;
            param[1] = new SqlParameter("@HTL_IMG_Text", SqlDbType.VarChar, 100);
            param[1].Value = ImgText;
            param[2] = new SqlParameter("@HTL_IMG_IMGPath", SqlDbType.VarChar, 200);
            param[2].Value = ImgPath;
            param[3] = new SqlParameter("@HTL_IMG_ThumbNailPath", SqlDbType.VarChar, 200);
            param[3].Value = ThumbPath;
            param[4] = new SqlParameter("@Counter", SqlDbType.Int);
            param[4].Value = 9;

            int iM = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            if (iM != -1)
            {
                bMod = true;
            }
            return bMod;
        }
        catch
        {
            return bMod;
        }

    }

    public bool UpdateHotelRoom(string RoomID, string RoomCode, string RoomName, string FromDate, string ToDate, string BoardType, string BaseFare, string Taxes, string MarkUp, string RoomAutoID, string Adt, string Chd, string Inf, bool MON, bool TUE, bool WED, bool THU, bool FRI, bool SAT, bool SUN, string ModifyBy)
    {
        bool bMod = false;
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[22];
        try
        {
            _CommandText = "HotelsRoomsDetailsEdit";

            param[0] = new SqlParameter("@HTL_RM_ID", SqlDbType.VarChar, 50);
            param[0].Value = RoomID;
            param[1] = new SqlParameter("@HTL_RM_Code", SqlDbType.VarChar, 5);
            param[1].Value = RoomCode;
            param[2] = new SqlParameter("@HTL_RM_Name", SqlDbType.VarChar, 200);
            param[2].Value = RoomName;
            param[3] = new SqlParameter("@HTL_RM_FromDate", SqlDbType.Date);
            param[3].Value = Convert.ToDateTime(FromDate);
            param[4] = new SqlParameter("@HTL_RM_ToDate", SqlDbType.Date);
            param[4].Value = Convert.ToDateTime(ToDate);
            param[5] = new SqlParameter("@HTL_RM_BoardType", SqlDbType.VarChar, 200);
            param[5].Value = BoardType;
            param[6] = new SqlParameter("@HTL_RM_PRC_BaseFare", SqlDbType.Money);
            param[6].Value = BaseFare;
            param[7] = new SqlParameter("@HTL_RM_PRC_Taxes", SqlDbType.Money);
            param[7].Value = Taxes;
            param[8] = new SqlParameter("@HTL_RM_PRC_MarkUp", SqlDbType.Money);
            param[8].Value = MarkUp;
            param[9] = new SqlParameter("@HTL_RM_Adt", SqlDbType.Int);
            param[9].Value = Adt;
            param[10] = new SqlParameter("@HTL_RM_Chd", SqlDbType.Int);
            param[10].Value = Chd;
            param[11] = new SqlParameter("@HTL_RM_Inf", SqlDbType.Int);
            param[11].Value = Inf;
            param[12] = new SqlParameter("@HTL_RM_MON", SqlDbType.Bit);
            param[12].Value = MON;
            param[13] = new SqlParameter("@HTL_RM_TUE", SqlDbType.Bit);
            param[13].Value = TUE;
            param[14] = new SqlParameter("@HTL_RM_WED", SqlDbType.Bit);
            param[14].Value = WED;
            param[15] = new SqlParameter("@HTL_RM_THU", SqlDbType.Bit);
            param[15].Value = THU;
            param[16] = new SqlParameter("@HTL_RM_FRI", SqlDbType.Bit);
            param[16].Value = FRI;
            param[17] = new SqlParameter("@HTL_RM_SAT", SqlDbType.Bit);
            param[17].Value = SAT;
            param[18] = new SqlParameter("@HTL_RM_SUN", SqlDbType.Bit);
            param[18].Value = SUN;
            param[19] = new SqlParameter("@HTL_RM_AUTOID", SqlDbType.Int);
            param[19].Value = RoomAutoID;
            param[20] = new SqlParameter("@paramModifyBy", SqlDbType.VarChar);
            param[20].Value = ModifyBy;
            param[21] = new SqlParameter("@Counter", SqlDbType.Int);
            param[21].Value = 10;

            int iM = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            if (iM != -1)
            {
                bMod = true;
            }
            return bMod;
        }
        catch
        {
            return bMod;
        }

    }


    public bool UpdateRoomFacility(string RoomFacID, string RFName, string ModifyBy)
    {
        bool bMod = false;
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[4];
        try
        {
            _CommandText = "HotelsRoomsDetailsEdit";

            param[0] = new SqlParameter("@HTL_RM_FAC_ID", SqlDbType.Int);
            param[0].Value = RoomFacID;
            param[1] = new SqlParameter("@HTL_RM_FAC_Name", SqlDbType.VarChar, 200);
            param[1].Value = RFName;
            param[2] = new SqlParameter("@paramModifyBy", SqlDbType.VarChar, 50);
            param[2].Value = ModifyBy;
            param[3] = new SqlParameter("@Counter", SqlDbType.Int);
            param[3].Value = 11;

            int iM = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            if (iM != -1)
            {
                bMod = true;
            }
            return bMod;
        }
        catch
        {
            return bMod;
        }

    }

    public bool DeleteHotelRoom(string DATA_MSTR_ID, string RM_ID, int Counter)
    {
        bool bMod = false;
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            _CommandText = "HotelsRoomsDetailsEdit";

            param[0] = new SqlParameter("@DATA_MSTR_ID", SqlDbType.VarChar, 50);
            param[0].Value = DATA_MSTR_ID;
            if (!string.IsNullOrEmpty(RM_ID))
            {
                param[1] = new SqlParameter("@HTL_RM_ID", SqlDbType.VarChar, 50);
                param[1].Value = RM_ID;
            }
            param[2] = new SqlParameter("@Counter", SqlDbType.Int);
            param[2].Value = Counter;

            int iM = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            if (iM != -1)
            {
                bMod = true;
            }
            return bMod;
        }
        catch
        {
            return bMod;
        }

    }
    public bool DeleteRoomFacility(int ID, int Counter)
    {
        bool bMod = false;
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            _CommandText = "HotelsRoomsDetailsEdit";

            param[0] = new SqlParameter("@HTL_RM_ID", SqlDbType.Int);
            param[0].Value = ID;
            param[1] = new SqlParameter("@Counter", SqlDbType.Int);
            param[1].Value = Counter;

            int iM = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            if (iM != -1)
            {
                bMod = true;
            }
            return bMod;
        }
        catch
        {
            return bMod;
        }

    }

    public bool DeleteHotel(string ID, int Counter)
    {
        bool bMod = false;
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            _CommandText = "HotelsRoomsDetailsEdit";

            param[0] = new SqlParameter("@DATA_MSTR_ID", SqlDbType.VarChar, 50);
            param[0].Value = ID;
            param[1] = new SqlParameter("@Counter", SqlDbType.Int);
            param[1].Value = Counter;

            int iM = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            if (iM != -1)
            {
                bMod = true;
            }
            return bMod;
        }
        catch
        {
            return bMod;
        }

    }


    public bool DeleteHotelImage(string ID, int Counter)
    {
        bool bMod = false;
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            _CommandText = "HotelsRoomsDetailsEdit";

            param[0] = new SqlParameter("@HTL_IMG_ID", SqlDbType.Int);
            param[0].Value = Convert.ToInt32(ID);
            param[1] = new SqlParameter("@Counter", SqlDbType.Int);
            param[1].Value = Counter;

            int iM = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            if (iM != -1)
            {
                bMod = true;
            }
            return bMod;
        }
        catch
        {
            return bMod;
        }

    }

    public string UploadHotelImage(FileUpload Uploader, string NameOfFile)
    {
        string sPath = string.Empty;
        string filename = Path.GetFileName(Uploader.FileName);
        string Extention = filename.Split('.')[1];
        filename = NameOfFile + "." + Extention;

        if (!File.Exists(HttpContext.Current.Server.MapPath("HotelImages/") + filename))
        {
            Uploader.SaveAs(HttpContext.Current.Server.MapPath("HotelImages/") + filename);
            sPath = PathHotelImage() + filename;
        }
        else
        {
            File.Delete(HttpContext.Current.Server.MapPath("HotelImages/") + filename);
            Uploader.SaveAs(HttpContext.Current.Server.MapPath("HotelImages/") + filename);
            sPath = PathHotelImage() + filename;

        }
        return sPath;
    }

    public string PathHotelImage()
    {
        string ImgPathHotel = ConfigurationSettings.AppSettings["HotelImgPath"];
        return ImgPathHotel;
    }

    public DataSet GetHotels(string CityCode)
    {
        DataSet ds = new DataSet();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            _CommandText = "GetHotelDetails";
            if (!String.IsNullOrEmpty(CityCode))
            {
                param[0] = new SqlParameter("@DATA_MSTR_CityCode", SqlDbType.VarChar, 5);
                param[0].Value = CityCode;
            }

            param[1] = new SqlParameter("@Counter", SqlDbType.Int);
            param[1].Value = 3;

            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);

            return ds;
        }

        catch
        {
            return ds;
        }
    }

    public DataTable GetHotelCities()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            _CommandText = "GetHotelDetails";
            param[0] = new SqlParameter("@Counter", SqlDbType.Int);
            param[0].Value = 1;

            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
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

    public DataTable GetHotelNames(string CityCode)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            _CommandText = "GetHotelDetails";
            if (!String.IsNullOrEmpty(CityCode))
            {
                param[0] = new SqlParameter("@DATA_MSTR_CityCode", SqlDbType.VarChar, 5);
                param[0].Value = CityCode;
            }

            param[1] = new SqlParameter("@Counter", SqlDbType.Int);
            param[1].Value = 2;

            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
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
    public DataTable GetHotelName_New(string CityCode, string company, string continent, string country)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        SqlParameter[] param = new SqlParameter[5];
        try
        {
            if (!String.IsNullOrEmpty(CityCode))
            {
                param[0] = new SqlParameter("@DATA_MSTR_CityCode", SqlDbType.VarChar);
                param[0].Value = CityCode;
            }
            if (!String.IsNullOrEmpty(company))
            {
                param[1] = new SqlParameter("@paramCompany", SqlDbType.VarChar);
                param[1].Value = company;
            }
            if (!String.IsNullOrEmpty(continent))
            {
                param[2] = new SqlParameter("@paramCategory", SqlDbType.VarChar);
                param[2].Value = continent;
            }
            if (!String.IsNullOrEmpty(country))
            {
                param[3] = new SqlParameter("@paramCountry", SqlDbType.VarChar);
                param[3].Value = country;
            }
            param[4] = new SqlParameter("@Counter", SqlDbType.Int);
            param[4].Value = 2;

            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, "GetHotelDetails", param);
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
    public int GetImageID()
    {
        int ID = 0;
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            _CommandText = "GetHotelDetails";
            param[0] = new SqlParameter("@Counter", SqlDbType.Int);
            param[0].Value = 4;

            object ImgID = SqlHelper.ExecuteScalar(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            if (ImgID != null && ImgID != "")
            {
                ID = int.Parse(ImgID.ToString());

            }
            else
            {
                ID = 0;
            }
            return ID;
        }
        catch
        {
            return ID;
        }
    }
    public bool SectorAdd(string DATA_MSTR_ID, string Prod_Booking_ID, string Org, string Dest, string Airline, string FltNum,
       string Class, string DepDate, string ArrDate, string Term, string Status, string ModifyBy)
    {
        bool bMod = false;
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[13];
        try
        {
            _CommandText = "HotelReservationInsert";

            param[0] = new SqlParameter("@DATA_MSTR_ID", SqlDbType.VarChar, 50);
            param[0].Value = DATA_MSTR_ID;
            param[1] = new SqlParameter("@paramProdBookingID", SqlDbType.VarChar, 5);
            param[1].Value = Prod_Booking_ID;
            if (!string.IsNullOrEmpty(Org))
            {
                param[2] = new SqlParameter("@paramFromDestination", SqlDbType.NVarChar);
                param[2].Value = Org;
            }
            if (!string.IsNullOrEmpty(Dest))
            {
                param[3] = new SqlParameter("@paramToDestination", SqlDbType.NVarChar);
                param[3].Value = Dest;
            }
            if (!string.IsNullOrEmpty(Airline))
            {
                param[4] = new SqlParameter("@paramCarierName", SqlDbType.NVarChar);
                param[4].Value = Airline;
            }
            if (!string.IsNullOrEmpty(FltNum))
            {
                param[5] = new SqlParameter("@paramFlightNo", SqlDbType.NVarChar);
                param[5].Value = FltNum;
            }
            if (!string.IsNullOrEmpty(Class))
            {
                param[6] = new SqlParameter("@paramClass", SqlDbType.NVarChar);
                param[6].Value = Class;
            } if (!string.IsNullOrEmpty(DepDate))
            {
                param[7] = new SqlParameter("@paramFromDateTime", SqlDbType.DateTime);
                param[7].Value = Convert.ToDateTime(DepDate);
            }
            if (!string.IsNullOrEmpty(ArrDate))
            {
                param[8] = new SqlParameter("@paramToDateTime", SqlDbType.DateTime);
                param[8].Value = Convert.ToDateTime(ArrDate);

            }
            if (!string.IsNullOrEmpty(Term))
            {
                param[9] = new SqlParameter("@paramAirportTerminal ", SqlDbType.NVarChar);
                param[9].Value = Term;
            }
            if (!string.IsNullOrEmpty(Status))
            {
                param[10] = new SqlParameter("@paramStatus ", SqlDbType.NVarChar);
                param[10].Value = Status;
            }
            if (!string.IsNullOrEmpty(ModifyBy))
            {
                param[11] = new SqlParameter("@paramModifyby ", SqlDbType.NVarChar);
                param[11].Value = ModifyBy;
            }
            param[12] = new SqlParameter("@Counter", SqlDbType.Int);
            param[12].Value = 9;

            int iM = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            if (iM != -1)
            {
                bMod = true;
            }
            return bMod;
        }
        catch
        {
            return bMod;
        }

    }
    public DataTable GetSectorDetails(string HotelID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            _CommandText = "HotelsRoomsDetailsEdit";
            if (!String.IsNullOrEmpty(HotelID))
            {
                param[0] = new SqlParameter("@DATA_MSTR_ID", SqlDbType.VarChar, 50);
                param[0].Value = HotelID;
            }

            param[1] = new SqlParameter("@Counter", SqlDbType.Int);
            param[1].Value = 20;

            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
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

    public bool SectorUpdate(string SecDetailIdID, string Org, string Dest, string Airline, string FltNum,
      string Class, string DepDate, string ArrDate, string Term, string Status, string ModifyBy)
    {
        bool bMod = false;
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[12];
        try
        {
            _CommandText = "HotelsRoomsDetailsEdit";

            param[0] = new SqlParameter("@paramSectorDetailId", SqlDbType.Int);
            param[0].Value = SecDetailIdID;
            if (!string.IsNullOrEmpty(Org))
            {
                param[1] = new SqlParameter("@paramFromDestination", SqlDbType.NVarChar);
                param[1].Value = Org;
            }
            if (!string.IsNullOrEmpty(Dest))
            {
                param[2] = new SqlParameter("@paramToDestination", SqlDbType.NVarChar);
                param[2].Value = Dest;
            }
            if (!string.IsNullOrEmpty(Airline))
            {
                param[3] = new SqlParameter("@paramCarierName", SqlDbType.NVarChar);
                param[3].Value = Airline;
            }
            if (!string.IsNullOrEmpty(FltNum))
            {
                param[4] = new SqlParameter("@paramFlightNo", SqlDbType.NVarChar);
                param[4].Value = FltNum;
            }
            if (!string.IsNullOrEmpty(Class))
            {
                param[5] = new SqlParameter("@paramClass", SqlDbType.NVarChar);
                param[5].Value = Class;
            } if (!string.IsNullOrEmpty(DepDate))
            {
                param[6] = new SqlParameter("@paramFromDateTime", SqlDbType.DateTime);
                param[6].Value = Convert.ToDateTime(DepDate);
            }
            if (!string.IsNullOrEmpty(ArrDate))
            {
                param[7] = new SqlParameter("@paramToDateTime", SqlDbType.DateTime);
                param[7].Value = Convert.ToDateTime(ArrDate);

            }
            if (!string.IsNullOrEmpty(Term))
            {
                param[8] = new SqlParameter("@paramAirportTerminal ", SqlDbType.NVarChar);
                param[8].Value = Term;
            }
            if (!string.IsNullOrEmpty(Status))
            {
                param[9] = new SqlParameter("@paramStatus ", SqlDbType.NVarChar);
                param[9].Value = Status;
            }

            if (!string.IsNullOrEmpty(ModifyBy))
            {
                param[10] = new SqlParameter("@paramModifyBy", SqlDbType.NVarChar);
                param[10].Value = ModifyBy;
            }
            param[11] = new SqlParameter("@Counter", SqlDbType.Int);
            param[11].Value = 21;

            int iM = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            if (iM != -1)
            {
                bMod = true;
            }
            return bMod;
        }
        catch
        {
            return bMod;
        }

    }
    public bool DeleteSector(string ID, int Counter)
    {
        bool bMod = false;
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            _CommandText = "HotelsRoomsDetailsEdit";

            param[0] = new SqlParameter("@paramSectorDetailId", SqlDbType.Int);
            param[0].Value = Convert.ToInt32(ID);
            param[1] = new SqlParameter("@Counter", SqlDbType.Int);
            param[1].Value = Counter;

            int iM = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            if (iM != -1)
            {
                bMod = true;
            }
            return bMod;
        }
        catch
        {
            return bMod;
        }

    }
    #region Hotelbeds Price Download

    public DataTable BindCountry(string language)
    {
        DataSet dsBindCountry;
        string _CommandText = string.Empty;

        try
        {

            SqlParameter[] param = new SqlParameter[1];
            _CommandText = "Get_country";
            if (!String.IsNullOrEmpty(language))
            {
                param[0] = new SqlParameter("@paramLanguageCode", SqlDbType.NVarChar, 50);
                param[0].Value = language;
            }
            dsBindCountry = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            return dsBindCountry.Tables[0];
        }

        catch (Exception ex)
        {
            dsBindCountry = null;
            return dsBindCountry.Tables[0];
        }
    }
    public DataTable BindDestination(string CountaryCode)
    {
        DataSet dsBindDestination;
        string _CommandText = string.Empty;

        try
        {
            SqlParameter[] param = new SqlParameter[1];
            _CommandText = "Get_Destination";

            param[0] = new SqlParameter("@paramCountaryCode", SqlDbType.NVarChar, 50);
            param[0].Value = CountaryCode;

            dsBindDestination = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            return dsBindDestination.Tables[0];
        }

        catch (Exception ex)
        {
            dsBindDestination = null;
            return dsBindDestination.Tables[0];
        }
    }
    public DataTable BindDestination()
    {
        DataSet dsBindDest;
        string _CommandText = string.Empty;

        try
        {

            _CommandText = "GET_DESTINATIONCODE";
            dsBindDest = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText);
            return dsBindDest.Tables[0];
        }

        catch (Exception ex)
        {
            dsBindDest = null;
            return dsBindDest.Tables[0];
        }
    }
    public DataTable BindHotel(string DestCode)
    {
        DataSet dsBindHotel;
        string _CommandText = string.Empty;

        try
        {
            SqlParameter[] param = new SqlParameter[1];
            _CommandText = "GET_HOTELNAME";

            param[0] = new SqlParameter("@paramDestCode", SqlDbType.NVarChar, 50);
            param[0].Value = DestCode;

            dsBindHotel = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            return dsBindHotel.Tables[0];
        }

        catch (Exception ex)
        {
            dsBindHotel = null;
            return dsBindHotel.Tables[0];
        }
    }
    public DataTable BindRoomType(string HotelCode)
    {
        DataSet dsBindRoom;
        string _CommandText = string.Empty;

        try
        {
            SqlParameter[] param = new SqlParameter[1];
            _CommandText = "GET_ROOMTYPE";

            param[0] = new SqlParameter("@paramHotelCode", SqlDbType.NVarChar, 50);
            param[0].Value = HotelCode;

            dsBindRoom = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            return dsBindRoom.Tables[0];
        }

        catch (Exception ex)
        {
            dsBindRoom = null;
            return dsBindRoom.Tables[0];
        }
    }
    public DataTable BindRoomTypeChartis(string HotelCode)
    {
        DataSet dsBindRoom;
        string _CommandText = string.Empty;

        try
        {
            SqlParameter[] param = new SqlParameter[1];
            _CommandText = "Get-RoomTypeChartis";

            param[0] = new SqlParameter("@paramHotelCode", SqlDbType.NVarChar, 50);
            param[0].Value = HotelCode;

            dsBindRoom = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            return dsBindRoom.Tables[0];
        }

        catch (Exception ex)
        {
            dsBindRoom = null;
            return dsBindRoom.Tables[0];
        }
    }
    public DataTable BindBoardBasisCode(string RoomType, string HotelCode)
    {
        DataSet dsBindBoardBasis;
        string _CommandText = string.Empty;

        try
        {
            SqlParameter[] param = new SqlParameter[2];
            _CommandText = "GET_BOARDBASISCODE";

            param[0] = new SqlParameter("@paramRoomtype", SqlDbType.NVarChar, 50);
            param[0].Value = RoomType;
            param[1] = new SqlParameter("@paramHotelCode", SqlDbType.NVarChar, 50);
            param[1].Value = HotelCode;

            dsBindBoardBasis = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            return dsBindBoardBasis.Tables[0];
        }

        catch (Exception ex)
        {
            dsBindBoardBasis = null;
            return dsBindBoardBasis.Tables[0];
        }
    }
    public DataTable SearchHotelPrice(string DestinationCode, string HotelCode, string RoomType, string BoardBasisCode, string RoomTypechartis, string StartPrice, string EndPrice)
    {
        DataSet dsSearchHotelPrice;
        string _CommandText = string.Empty;

        try
        {
            SqlParameter[] param = new SqlParameter[7];
            _CommandText = "sp_HotelSearch";

            if (!String.IsNullOrEmpty(DestinationCode))
            {
                param[0] = new SqlParameter("@paramDestinationCode", SqlDbType.VarChar);
                param[0].Value = DestinationCode;
            }
            if (!String.IsNullOrEmpty(HotelCode))
            {
                param[1] = new SqlParameter("@paramHotelCode", SqlDbType.VarChar);
                param[1].Value = HotelCode;
            }
            if (!String.IsNullOrEmpty(RoomType))
            {
                param[2] = new SqlParameter("@paramRoomType", SqlDbType.VarChar);
                param[2].Value = RoomType;
            }
            if (!String.IsNullOrEmpty(BoardBasisCode))
            {
                param[3] = new SqlParameter("@paramBoardBasisCode", SqlDbType.VarChar);
                param[3].Value = BoardBasisCode;
            }
            if (!String.IsNullOrEmpty(RoomTypechartis))
            {
                param[4] = new SqlParameter("@paramRoomTypeChartis", SqlDbType.VarChar);
                param[4].Value = RoomTypechartis;
            }
            if (!String.IsNullOrEmpty(StartPrice))
            {
                param[5] = new SqlParameter("@paramStartPrice", SqlDbType.Money);
                param[5].Value = Convert.ToDouble(StartPrice);
            }
            if (!String.IsNullOrEmpty(EndPrice))
            {
                param[6] = new SqlParameter("@paramEndPrice", SqlDbType.Money);
                param[6].Value = Convert.ToDouble(EndPrice);
            }


            dsSearchHotelPrice = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            return dsSearchHotelPrice.Tables[0];
        }

        catch (Exception ex)
        {
            dsSearchHotelPrice = null;
            return dsSearchHotelPrice.Tables[0];
        }
    }
    #endregion

    #region Deals Suppiler

    public bool Supplier_Insert(string SupplierCode, string SupplierName, int counter)
    {
        string _CommandText = "sp_Supplier";
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            if (!string.IsNullOrEmpty(SupplierCode))
            {
                param[0] = new SqlParameter("@paramSupplierCode", SqlDbType.NVarChar);
                param[0].Value = SupplierCode;
            }
            if (!string.IsNullOrEmpty(SupplierName))
            {
                param[1] = new SqlParameter("@paramSupplier_Name", SqlDbType.NVarChar);
                param[1].Value = SupplierName;
            }
            param[2] = new SqlParameter("@Counter", SqlDbType.Int);
            param[2].Value = counter;

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            if (count > 0)
                return true;
            else
                return false;

        }
        catch { return false; }
    }
    public DataTable ViewSupplier(string SupplierCode, string SupplierName, int counter)
    {
        DataSet dsSupplier;
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[3];
        try
        {

            _CommandText = "sp_Supplier";

            if (!string.IsNullOrEmpty(SupplierCode))
            {
                param[0] = new SqlParameter("@paramSupplierCode", SqlDbType.NVarChar);
                param[0].Value = SupplierCode;
            }
            if (!string.IsNullOrEmpty(SupplierName))
            {
                param[1] = new SqlParameter("@paramSupplier_Name", SqlDbType.NVarChar);
                param[1].Value = SupplierName;
            }
            param[2] = new SqlParameter("@Counter", SqlDbType.Int);
            param[2].Value = counter;

            dsSupplier = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            return dsSupplier.Tables[0];
        }

        catch (Exception ex)
        {
            dsSupplier = null;
            return dsSupplier.Tables[0];
        }
    }
    public bool ValidateSupplier(string SupplierName, int counter)
    {
        int count;
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[2];

        try
        {
            _CommandText = "sp_Supplier";


            if (!string.IsNullOrEmpty(SupplierName))
            {
                param[0] = new SqlParameter("@paramSupplier_Name", SqlDbType.NVarChar);
                param[0].Value = SupplierName;
            }
            param[1] = new SqlParameter("@Counter", SqlDbType.Int);
            param[1].Value = counter;
            count = Convert.ToInt16(SqlHelper.ExecuteScalar(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param));
            if (count > 0)
                return true;
            else
                return false;

        }
        catch { return false; }
    }
    public bool Supplier_Delete(string SupplierId, int counter)
    {
        string _CommandText = "sp_Supplier";
        SqlParameter[] param = new SqlParameter[2];
        try
        {

            if (!string.IsNullOrEmpty(SupplierId))
            {
                param[0] = new SqlParameter("@paramSupplierId", SqlDbType.Int);
                param[0].Value = Convert.ToInt32(SupplierId);
            }

            param[1] = new SqlParameter("@Counter", SqlDbType.Int);
            param[1].Value = counter;

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            if (count > 0)
                return true;
            else
                return false;

        }
        catch { return false; }
    }
    public bool Supplier_Update(string SupplierName, string SupplierID, int counter)
    {
        string _CommandText = "sp_Supplier";
        SqlParameter[] param = new SqlParameter[3];
        try
        {

            if (!string.IsNullOrEmpty(SupplierID))
            {
                param[0] = new SqlParameter("@paramSupplierId", SqlDbType.Int);
                param[0].Value = Convert.ToInt64(SupplierID);
            }
            if (!string.IsNullOrEmpty(SupplierName))
            {
                param[1] = new SqlParameter("@paramSupplier_Name", SqlDbType.NVarChar);
                param[1].Value = SupplierName;
            }

            param[2] = new SqlParameter("@Counter", SqlDbType.Int);
            param[2].Value = counter;

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            if (count > 0)
                return true;
            else
                return false;

        }
        catch { return false; }
    }
    public int Deal_Insert(string SupplierCode, string DealCode, string DealName, string Commision, string DealStartDate
    , string DealEndDate, string HotelName, string HotelStarRating, string HotelRoomType, string HotelBoardBasis
    , string FlightDepartAirPort, string FlightArrivalAirport, string FlightLuggPolicy, string TransferType
    , string HotelPrice, string FlightPrice, string TransferPrice, string TotalPrice, string Remarks,
     string Tourname, string TourPrice, string BookWithHtlSupp, string BookWithfltSupp, string BookWithTransSupp, string BookwithTourSupp, int counter)
    {
        int strDealId;
        string _CommandText = "sp_Deal_Mst";
        SqlParameter[] param = new SqlParameter[27];
        try
        {
            if (!string.IsNullOrEmpty(SupplierCode))
            {
                param[0] = new SqlParameter("@paramSupplierCode", SqlDbType.NVarChar);
                param[0].Value = SupplierCode;
            }
            if (!string.IsNullOrEmpty(DealCode))
            {
                param[1] = new SqlParameter("@paramDealCode", SqlDbType.NVarChar);
                param[1].Value = DealCode;
            }
            if (!string.IsNullOrEmpty(DealName))
            {
                param[2] = new SqlParameter("@paramDealName", SqlDbType.NVarChar);
                param[2].Value = DealName;
            }
            if (!string.IsNullOrEmpty(Commision))
            {
                param[3] = new SqlParameter("@paramCommision", SqlDbType.Money);
                param[3].Value = Convert.ToDouble(Commision);
            }
            if (!string.IsNullOrEmpty(DealStartDate))
            {
                param[4] = new SqlParameter("@paramDealStartDate", SqlDbType.DateTime);
                param[4].Value = Convert.ToDateTime(DealStartDate);
            }
            if (!string.IsNullOrEmpty(DealEndDate))
            {
                param[5] = new SqlParameter("@paramDealEndDate", SqlDbType.DateTime);
                param[5].Value = Convert.ToDateTime(DealEndDate);
            }
            if (!string.IsNullOrEmpty(HotelName))
            {
                param[6] = new SqlParameter("@paramHotelName", SqlDbType.NVarChar);
                param[6].Value = HotelName;
            }
            if (!string.IsNullOrEmpty(HotelStarRating))
            {
                param[7] = new SqlParameter("@paramHtlStarRating", SqlDbType.NVarChar);
                param[7].Value = HotelStarRating;
            }
            if (!string.IsNullOrEmpty(HotelRoomType))
            {
                param[8] = new SqlParameter("@paramHtlRoomType", SqlDbType.NVarChar);
                param[8].Value = HotelRoomType;
            }
            if (!string.IsNullOrEmpty(HotelBoardBasis))
            {
                param[9] = new SqlParameter("@paramHtlBoardBasis", SqlDbType.NVarChar);
                param[9].Value = HotelBoardBasis;
            }
            if (!string.IsNullOrEmpty(FlightDepartAirPort))
            {
                param[10] = new SqlParameter("@paramFltDepartAirPort", SqlDbType.NVarChar);
                param[10].Value = FlightDepartAirPort;
            }
            if (!string.IsNullOrEmpty(FlightArrivalAirport))
            {
                param[11] = new SqlParameter("@paramFltArrivalAirport", SqlDbType.NVarChar);
                param[11].Value = FlightArrivalAirport;
            }
            if (!string.IsNullOrEmpty(FlightLuggPolicy))
            {
                param[12] = new SqlParameter("@paramFltLuggPolicy", SqlDbType.NVarChar);
                param[12].Value = FlightLuggPolicy;
            }
            if (!string.IsNullOrEmpty(TransferType))
            {
                param[13] = new SqlParameter("@paramTransferType", SqlDbType.NVarChar);
                param[13].Value = TransferType;
            }
            if (!string.IsNullOrEmpty(HotelPrice))
            {
                param[14] = new SqlParameter("@paramHotelPrice", SqlDbType.Money);
                param[14].Value = Convert.ToDouble(HotelPrice);
            }
            if (!string.IsNullOrEmpty(FlightPrice))
            {
                param[15] = new SqlParameter("@paramFlightPrice", SqlDbType.Money);
                param[15].Value = Convert.ToDouble(FlightPrice);
            }
            if (!string.IsNullOrEmpty(TransferPrice))
            {
                param[16] = new SqlParameter("@paramTransferPrice", SqlDbType.Money);
                param[16].Value = Convert.ToDouble(TransferPrice);
            }
            if (!string.IsNullOrEmpty(TotalPrice))
            {
                param[17] = new SqlParameter("@paramTotalPrice", SqlDbType.Money);
                param[17].Value = Convert.ToDouble(TotalPrice);
            }
            if (!string.IsNullOrEmpty(Remarks))
            {
                param[18] = new SqlParameter("@paramRemarks", SqlDbType.NVarChar);
                param[18].Value = Remarks;
            }


            if (!string.IsNullOrEmpty(Tourname))
            {
                param[19] = new SqlParameter("@paramTourName", SqlDbType.NVarChar);
                param[19].Value = Tourname;
            }
            if (!string.IsNullOrEmpty(TourPrice))
            {
                param[20] = new SqlParameter("@paramTourPrice", SqlDbType.Money);
                param[20].Value = Convert.ToDouble(TourPrice);
            }
            if (!string.IsNullOrEmpty(BookWithHtlSupp))
            {
                param[21] = new SqlParameter("@paramBookWithHotelSupp", SqlDbType.NVarChar);
                param[21].Value = BookWithHtlSupp;
            }
            if (!string.IsNullOrEmpty(BookWithfltSupp))
            {
                param[22] = new SqlParameter("@paramBookWithFlightSupp", SqlDbType.NVarChar);
                param[22].Value = BookWithfltSupp;
            }
            if (!string.IsNullOrEmpty(BookWithTransSupp))
            {
                param[23] = new SqlParameter("@paramBookWithTransferSupp", SqlDbType.NVarChar);
                param[23].Value = BookWithTransSupp;
            }
            if (!string.IsNullOrEmpty(BookwithTourSupp))
            {
                param[24] = new SqlParameter("@paramBookWithTourSupp", SqlDbType.NVarChar);
                param[24].Value = BookwithTourSupp;
            }

            param[25] = new SqlParameter("@Counter", SqlDbType.Int);
            param[25].Value = counter;

            param[26] = new SqlParameter("@idcategory", SqlDbType.Int);
            param[26].Direction = ParameterDirection.Output;

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);

            if (count > 0)
            {
                strDealId = Convert.ToInt32(param[26].Value.ToString());
                return strDealId;
            }

            else
            {
                return 0;
            }

        }
        catch { return 0; }
    }

    public DataTable ViewDeals(string SupplierCode, string DealCode, string Commision, string DealStartDateFrom
    , string DealStartDateTo, string DealEndDateFrom, string DealEndDateTo, string dealId, int counter)
    {
        DataSet dsDeals;
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[9];
        try
        {

            _CommandText = "sp_Deal_Mst";

            if (!string.IsNullOrEmpty(SupplierCode))
            {
                param[0] = new SqlParameter("@paramSupplierCode", SqlDbType.NVarChar);
                param[0].Value = SupplierCode;
            }
            if (!string.IsNullOrEmpty(DealCode))
            {
                param[1] = new SqlParameter("@paramDealCode", SqlDbType.NVarChar);
                param[1].Value = DealCode;
            }

            if (!string.IsNullOrEmpty(Commision))
            {
                param[2] = new SqlParameter("@paramCommision", SqlDbType.Money);
                param[2].Value = Convert.ToDouble(Commision);
            }
            if (!string.IsNullOrEmpty(DealStartDateFrom))
            {
                param[3] = new SqlParameter("@paramDealStartDate", SqlDbType.DateTime);
                param[3].Value = Convert.ToDateTime(DealStartDateFrom);
            }
            if (!string.IsNullOrEmpty(DealStartDateTo))
            {
                param[4] = new SqlParameter("@paramDealStartDateE", SqlDbType.DateTime);
                param[4].Value = Convert.ToDateTime(DealStartDateTo);
            }
            if (!string.IsNullOrEmpty(DealEndDateFrom))
            {
                param[5] = new SqlParameter("@paramDealEndDate", SqlDbType.DateTime);
                param[5].Value = Convert.ToDateTime(DealEndDateFrom);
            }
            if (!string.IsNullOrEmpty(DealEndDateTo))
            {
                param[6] = new SqlParameter("@paramDealEndDateE", SqlDbType.DateTime);
                param[6].Value = Convert.ToDateTime(DealEndDateTo);
            }
            if (!string.IsNullOrEmpty(dealId))
            {
                param[7] = new SqlParameter("@paramDealId", SqlDbType.Int);
                param[7].Value = Convert.ToInt32(dealId);
            }
            param[8] = new SqlParameter("@Counter", SqlDbType.Int);
            param[8].Value = counter;

            dsDeals = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            return dsDeals.Tables[0];
        }

        catch (Exception ex)
        {
            dsDeals = null;
            return dsDeals.Tables[0];
        }
    }
    public bool Deals_Delete(string DealId, int counter)
    {
        string _CommandText = "sp_Deal_Mst";
        SqlParameter[] param = new SqlParameter[2];
        try
        {

            if (!string.IsNullOrEmpty(DealId))
            {
                param[0] = new SqlParameter("@paramDealId", SqlDbType.Int);
                param[0].Value = Convert.ToInt32(DealId);
            }

            param[1] = new SqlParameter("@Counter", SqlDbType.Int);
            param[1].Value = counter;

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            if (count > 0)
                return true;
            else
                return false;

        }
        catch { return false; }
    }
    public bool Deal_Update(string DealName, string Commision, string DealStartDate
 , string DealEndDate, string HotelName, string HotelStarRating, string HotelRoomType, string HotelBoardBasis
 , string FlightDepartAirPort, string FlightArrivalAirport, string FlightLuggPolicy, string TransferType
 , string HotelPrice, string FlightPrice, string TransferPrice, string TotalPrice, string Remarks, string DealCode,
  string Tourname, string TourPrice, string BookWithHtlSupp, string BookWithfltSupp, string BookWithTransSupp, string BookwithTourSupp,
        int counter)
    {
        string _CommandText = "sp_Deal_Mst";
        SqlParameter[] param = new SqlParameter[25];
        try
        {

            if (!string.IsNullOrEmpty(DealName))
            {
                param[0] = new SqlParameter("@paramDealName", SqlDbType.NVarChar);
                param[0].Value = DealName;
            }
            if (!string.IsNullOrEmpty(Commision))
            {
                param[1] = new SqlParameter("@paramCommision", SqlDbType.Money);
                param[1].Value = Convert.ToDouble(Commision);
            }
            if (!string.IsNullOrEmpty(DealStartDate))
            {
                param[2] = new SqlParameter("@paramDealStartDate", SqlDbType.DateTime);
                param[2].Value = Convert.ToDateTime(DealStartDate);
            }
            if (!string.IsNullOrEmpty(DealEndDate))
            {
                param[3] = new SqlParameter("@paramDealEndDate", SqlDbType.DateTime);
                param[3].Value = Convert.ToDateTime(DealEndDate);
            }
            if (!string.IsNullOrEmpty(HotelName))
            {
                param[4] = new SqlParameter("@paramHotelName", SqlDbType.NVarChar);
                param[4].Value = HotelName;
            }
            if (!string.IsNullOrEmpty(HotelStarRating))
            {
                param[5] = new SqlParameter("@paramHtlStarRating", SqlDbType.NVarChar);
                param[5].Value = HotelStarRating;
            }
            if (!string.IsNullOrEmpty(HotelRoomType))
            {
                param[6] = new SqlParameter("@paramHtlRoomType", SqlDbType.NVarChar);
                param[6].Value = HotelRoomType;
            }
            if (!string.IsNullOrEmpty(HotelBoardBasis))
            {
                param[7] = new SqlParameter("@paramHtlBoardBasis", SqlDbType.NVarChar);
                param[7].Value = HotelBoardBasis;
            }
            if (!string.IsNullOrEmpty(FlightDepartAirPort))
            {
                param[8] = new SqlParameter("@paramFltDepartAirPort", SqlDbType.NVarChar);
                param[8].Value = FlightDepartAirPort;
            }
            if (!string.IsNullOrEmpty(FlightArrivalAirport))
            {
                param[9] = new SqlParameter("@paramFltArrivalAirport", SqlDbType.NVarChar);
                param[9].Value = FlightArrivalAirport;
            }
            if (!string.IsNullOrEmpty(FlightLuggPolicy))
            {
                param[10] = new SqlParameter("@paramFltLuggPolicy", SqlDbType.NVarChar);
                param[10].Value = FlightLuggPolicy;
            }
            if (!string.IsNullOrEmpty(TransferType))
            {
                param[11] = new SqlParameter("@paramTransferType", SqlDbType.NVarChar);
                param[11].Value = TransferType;
            }
            if (!string.IsNullOrEmpty(HotelPrice))
            {
                param[12] = new SqlParameter("@paramHotelPrice", SqlDbType.Money);
                param[12].Value = Convert.ToDouble(HotelPrice);
            }
            if (!string.IsNullOrEmpty(FlightPrice))
            {
                param[13] = new SqlParameter("@paramFlightPrice", SqlDbType.Money);
                param[13].Value = Convert.ToDouble(FlightPrice);
            }
            if (!string.IsNullOrEmpty(TransferPrice))
            {
                param[14] = new SqlParameter("@paramTransferPrice", SqlDbType.Money);
                param[14].Value = Convert.ToDouble(TransferPrice);
            }
            if (!string.IsNullOrEmpty(TotalPrice))
            {
                param[15] = new SqlParameter("@paramTotalPrice", SqlDbType.Money);
                param[15].Value = Convert.ToDouble(TotalPrice);
            }
            if (!string.IsNullOrEmpty(Remarks))
            {
                param[16] = new SqlParameter("@paramRemarks", SqlDbType.NVarChar);
                param[16].Value = Remarks;
            }
            if (!string.IsNullOrEmpty(DealCode))
            {
                param[17] = new SqlParameter("@paramDealCode", SqlDbType.NVarChar);
                param[17].Value = DealCode;
            }
            if (!string.IsNullOrEmpty(Tourname))
            {
                param[18] = new SqlParameter("@paramTourName", SqlDbType.NVarChar);
                param[18].Value = Tourname;
            }
            if (!string.IsNullOrEmpty(TourPrice))
            {
                param[19] = new SqlParameter("@paramTourPrice", SqlDbType.Money);
                param[19].Value = Convert.ToDouble(TourPrice);
            }
            if (!string.IsNullOrEmpty(BookWithHtlSupp))
            {
                param[20] = new SqlParameter("@paramBookWithHotelSupp", SqlDbType.NVarChar);
                param[20].Value = BookWithHtlSupp;
            }
            if (!string.IsNullOrEmpty(BookWithfltSupp))
            {
                param[21] = new SqlParameter("@paramBookWithFlightSupp", SqlDbType.NVarChar);
                param[21].Value = BookWithfltSupp;
            }
            if (!string.IsNullOrEmpty(BookWithTransSupp))
            {
                param[22] = new SqlParameter("@paramBookWithTransferSupp", SqlDbType.NVarChar);
                param[22].Value = BookWithTransSupp;
            }
            if (!string.IsNullOrEmpty(BookwithTourSupp))
            {
                param[23] = new SqlParameter("@paramBookWithTourSupp", SqlDbType.NVarChar);
                param[23].Value = BookwithTourSupp;
            }


            param[24] = new SqlParameter("@Counter", SqlDbType.Int);
            param[24].Value = counter;

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            if (count > 0)
                return true;
            else
                return false;

        }
        catch { return false; }
    }

    public bool ValidateDeal(string DealName, string supplierCode, string DealStartDate, string DealEndDate, int counter)
    {
        int count;
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[5];

        try
        {
            _CommandText = "sp_Deal_Mst";


            if (!string.IsNullOrEmpty(DealName))
            {
                param[0] = new SqlParameter("@paramDealName", SqlDbType.NVarChar);
                param[0].Value = DealName;
            }
            if (!string.IsNullOrEmpty(supplierCode))
            {
                param[1] = new SqlParameter("@paramSupplierCode", SqlDbType.NVarChar);
                param[1].Value = supplierCode;
            }
            if (!string.IsNullOrEmpty(DealStartDate))
            {
                param[2] = new SqlParameter("@paramDealStartDate", SqlDbType.DateTime);
                param[2].Value = Convert.ToDateTime(DealStartDate);
            }
            if (!string.IsNullOrEmpty(DealEndDate))
            {
                param[3] = new SqlParameter("@paramDealEndDate", SqlDbType.DateTime);
                param[3].Value = Convert.ToDateTime(DealEndDate);
            }
            param[4] = new SqlParameter("@Counter", SqlDbType.Int);
            param[4].Value = counter;
            count = Convert.ToInt16(SqlHelper.ExecuteScalar(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param));
            if (count > 0)
                return true;
            else
                return false;

        }
        catch { return false; }
    }
    public DataTable BindDeals(string SupplierCode, int counter)
    {
        DataSet dsBindDeals;
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[2];
        try
        {

            _CommandText = "sp_Deal_Mst";

            if (!string.IsNullOrEmpty(SupplierCode))
            {
                param[0] = new SqlParameter("@paramSupplierCode", SqlDbType.NVarChar);
                param[0].Value = SupplierCode;
            }
            param[1] = new SqlParameter("@Counter", SqlDbType.Int);
            param[1].Value = counter;

            dsBindDeals = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            return dsBindDeals.Tables[0];
        }

        catch (Exception ex)
        {
            dsBindDeals = null;
            return dsBindDeals.Tables[0];
        }
    }

    public bool HotelDeal_Insert(string dealId, string HotelName, string HotelStarRating, string HotelRoomType, string HotelBoardBasis, string HotelPrice, string BookWithHtlSupp, int counter)
    {
        string _CommandText = "sp_Deal_Mst";
        SqlParameter[] param = new SqlParameter[8];
        try
        {
            if (!string.IsNullOrEmpty(dealId))
            {
                param[0] = new SqlParameter("@paramDealId ", SqlDbType.Int);
                param[0].Value = Convert.ToInt32(dealId);
            }
            if (!string.IsNullOrEmpty(HotelName))
            {
                param[1] = new SqlParameter("@paramHotelNameD", SqlDbType.NVarChar);
                param[1].Value = HotelName;
            }
            if (!string.IsNullOrEmpty(HotelStarRating))
            {
                param[2] = new SqlParameter("@paramHotelStarRatingD", SqlDbType.NVarChar);
                param[2].Value = HotelStarRating;
            }
            if (!string.IsNullOrEmpty(HotelRoomType))
            {
                param[3] = new SqlParameter("@paramHotelRoomTypeD", SqlDbType.NVarChar);
                param[3].Value = HotelRoomType;
            }
            if (!string.IsNullOrEmpty(HotelBoardBasis))
            {
                param[4] = new SqlParameter("@paramHotelBoardBasisD", SqlDbType.NVarChar);
                param[4].Value = HotelBoardBasis;
            }

            if (!string.IsNullOrEmpty(HotelPrice))
            {
                param[5] = new SqlParameter("@paramHotelPriceD", SqlDbType.Money);
                param[5].Value = Convert.ToDouble(HotelPrice);
            }

            if (!string.IsNullOrEmpty(BookWithHtlSupp))
            {
                param[6] = new SqlParameter("@paramBookWithHotelSuppD", SqlDbType.NVarChar);
                param[6].Value = BookWithHtlSupp;
            }

            param[7] = new SqlParameter("@Counter", SqlDbType.Int);
            param[7].Value = counter;

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            if (count > 0)
                return true;
            else
                return false;

        }
        catch { return false; }
    }

    public DataTable ViewHotelDeals(string dealId, int counter)
    {
        DataSet dsHotelDeals;
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            _CommandText = "sp_Deal_Mst";

            if (!string.IsNullOrEmpty(dealId))
            {
                param[0] = new SqlParameter("@paramDealId", SqlDbType.Int);
                param[0].Value = Convert.ToInt32(dealId);
            }
            param[1] = new SqlParameter("@Counter", SqlDbType.Int);
            param[1].Value = counter;

            dsHotelDeals = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            return dsHotelDeals.Tables[0];
        }

        catch (Exception ex)
        {
            dsHotelDeals = null;
            return dsHotelDeals.Tables[0];
        }
    }
    public bool HotelDeal_Update(string HoteldealId, string HotelName, string HotelStarRating, string HotelRoomType, string HotelBoardBasis, string HotelPrice, string BookWithHtlSupp, int counter)
    {
        string _CommandText = "sp_Deal_Mst";
        SqlParameter[] param = new SqlParameter[8];
        try
        {
            if (!string.IsNullOrEmpty(HoteldealId))
            {
                param[0] = new SqlParameter("@paraHotalDealId", SqlDbType.Int);
                param[0].Value = Convert.ToInt32(HoteldealId);
            }
            if (!string.IsNullOrEmpty(HotelName))
            {
                param[1] = new SqlParameter("@paramHotelNameD", SqlDbType.NVarChar);
                param[1].Value = HotelName;
            }
            if (!string.IsNullOrEmpty(HotelStarRating))
            {
                param[2] = new SqlParameter("@paramHotelStarRatingD", SqlDbType.NVarChar);
                param[2].Value = HotelStarRating;
            }
            if (!string.IsNullOrEmpty(HotelRoomType))
            {
                param[3] = new SqlParameter("@paramHotelRoomTypeD", SqlDbType.NVarChar);
                param[3].Value = HotelRoomType;
            }
            if (!string.IsNullOrEmpty(HotelBoardBasis))
            {
                param[4] = new SqlParameter("@paramHotelBoardBasisD", SqlDbType.NVarChar);
                param[4].Value = HotelBoardBasis;
            }

            if (!string.IsNullOrEmpty(HotelPrice))
            {
                param[5] = new SqlParameter("@paramHotelPriceD", SqlDbType.Money);
                param[5].Value = Convert.ToDouble(HotelPrice);
            }

            if (!string.IsNullOrEmpty(BookWithHtlSupp))
            {
                param[6] = new SqlParameter("@paramBookWithHotelSuppD", SqlDbType.NVarChar);
                param[6].Value = BookWithHtlSupp;
            }

            param[7] = new SqlParameter("@Counter", SqlDbType.Int);
            param[7].Value = counter;

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            if (count > 0)
                return true;
            else
                return false;

        }
        catch { return false; }
    }
    public bool HotelDeal_Delete(string HotelDealId, int counter)
    {
        string _CommandText = "sp_Deal_Mst";
        SqlParameter[] param = new SqlParameter[2];
        try
        {

            if (!string.IsNullOrEmpty(HotelDealId))
            {
                param[0] = new SqlParameter("@paraHotalDealId", SqlDbType.Int);
                param[0].Value = Convert.ToInt32(HotelDealId);
            }

            param[1] = new SqlParameter("@Counter", SqlDbType.Int);
            param[1].Value = counter;

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            if (count > 0)
                return true;
            else
                return false;

        }
        catch { return false; }
    }

    #endregion

    public DataTable ViewHotelDestination(string id, string Destination, string Month, string Year, string Provider, int counter)
    {
        DataSet dsHotelDestination;
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[6];
        try
        {
            _CommandText = "sp_HotelPriceDestinations";

            if (!string.IsNullOrEmpty(id))
            {
                param[0] = new SqlParameter("@paramId", SqlDbType.Int);
                param[0].Value = Convert.ToInt32(id);
            }
            if (!string.IsNullOrEmpty(Destination))
            {
                param[1] = new SqlParameter("@paramDestination", SqlDbType.NVarChar);
                param[1].Value = Destination;
            }
            if (!string.IsNullOrEmpty(Month))
            {
                param[2] = new SqlParameter("@paramMonth", SqlDbType.NVarChar);
                param[2].Value = Month;
            }
            if (!string.IsNullOrEmpty(Year))
            {
                param[3] = new SqlParameter("@paramYear", SqlDbType.NVarChar);
                param[3].Value = Year;
            }
            if (!string.IsNullOrEmpty(Provider))
            {
                param[4] = new SqlParameter("@paramProvider", SqlDbType.NVarChar);
                param[4].Value = Provider;
            }
            param[5] = new SqlParameter("@Counter", SqlDbType.Int);
            param[5].Value = counter;

            dsHotelDestination = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            return dsHotelDestination.Tables[0];
        }

        catch (Exception ex)
        {
            dsHotelDestination = null;
            return dsHotelDestination.Tables[0];
        }
    }
    public bool HotelDestination_Insert(string Destination, string Month, string Year, string Provider, int counter)
    {
        string _CommandText = "sp_HotelPriceDestinations";
        SqlParameter[] param = new SqlParameter[5];
        try
        {
            if (!string.IsNullOrEmpty(Destination))
            {
                param[0] = new SqlParameter("@paramDestination", SqlDbType.NVarChar);
                param[0].Value = Destination;
            }
            if (!string.IsNullOrEmpty(Month))
            {
                param[1] = new SqlParameter("@paramMonth", SqlDbType.NVarChar);
                param[1].Value = Month;
            }
            if (!string.IsNullOrEmpty(Year))
            {
                param[2] = new SqlParameter("@paramYear", SqlDbType.NVarChar);
                param[2].Value = Year;
            }
            if (!string.IsNullOrEmpty(Provider))
            {
                param[3] = new SqlParameter("@paramProvider", SqlDbType.NVarChar);
                param[3].Value = Provider;
            }
            param[4] = new SqlParameter("@Counter", SqlDbType.Int);
            param[4].Value = counter;

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            if (count > 0)
                return true;
            else
                return false;

        }
        catch { return false; }
    }
    public bool HotelDestination_Update(string Destination, string Month, string Year, string Provider, string id, int counter)
    {
        string _CommandText = "sp_HotelPriceDestinations";
        SqlParameter[] param = new SqlParameter[6];
        try
        {
            if (!string.IsNullOrEmpty(Destination))
            {
                param[0] = new SqlParameter("@paramDestination", SqlDbType.NVarChar);
                param[0].Value = Destination;
            }
            if (!string.IsNullOrEmpty(Month))
            {
                param[1] = new SqlParameter("@paramMonth", SqlDbType.NVarChar);
                param[1].Value = Month;
            }
            if (!string.IsNullOrEmpty(Year))
            {
                param[2] = new SqlParameter("@paramYear", SqlDbType.NVarChar);
                param[2].Value = Year;
            }
            if (!string.IsNullOrEmpty(Provider))
            {
                param[3] = new SqlParameter("@paramProvider", SqlDbType.NVarChar);
                param[3].Value = Provider;
            }
            if (!string.IsNullOrEmpty(id))
            {
                param[4] = new SqlParameter("@paramId", SqlDbType.Int);
                param[4].Value = Convert.ToInt32(id);
            }
            param[5] = new SqlParameter("@Counter", SqlDbType.Int);
            param[5].Value = counter;

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            if (count > 0)
                return true;
            else
                return false;

        }
        catch { return false; }
    }
    public bool HotelDestination_Delete(string id, int counter)
    {
        string _CommandText = "sp_HotelPriceDestinations";
        SqlParameter[] param = new SqlParameter[2];
        try
        {

            if (!string.IsNullOrEmpty(id))
            {
                param[0] = new SqlParameter("@paramId", SqlDbType.Int);
                param[0].Value = Convert.ToInt32(id);
            }

            param[1] = new SqlParameter("@Counter", SqlDbType.Int);
            param[1].Value = counter;

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            if (count > 0)
                return true;
            else
                return false;

        }
        catch { return false; }
    }



    public DataTable GetCategoryDealPage(string counter)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            _CommandText = "sp_PageByDeal";

            param[0] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[0].Value = counter;

            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
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
    public DataTable GetDestDealPage(string counter)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            _CommandText = "sp_PageByDeal";

            param[0] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[0].Value = counter;

            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
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

    public int GetHotelImageID()
    {
        int ID = 0;
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            _CommandText = "HotelReservationInsert";
            param[0] = new SqlParameter("@Counter", SqlDbType.Int);
            param[0].Value = 10;

            object ImgID = SqlHelper.ExecuteScalar(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            if (ImgID != null && ImgID != "")
            {
                ID = int.Parse(ImgID.ToString());

            }
            else
            {
                ID = 0;
            }
            return ID;
        }
        catch
        {
            return ID;
        }
    }
    public string UploadHotelImageCategory(FileUpload Uploader, string NameOfFile)
    {
        string sPath = string.Empty;
        string filename = Path.GetFileName(Uploader.FileName);
        string Extention = filename.Split('.')[1];
        filename = NameOfFile + "." + Extention;

        if (!File.Exists(HttpContext.Current.Server.MapPath("CategoryImage/") + filename))
        {
            Uploader.SaveAs(HttpContext.Current.Server.MapPath("CategoryImage/") + filename);
            sPath = PathHotelCateImage() + filename;
        }
        else
        {
            File.Delete(HttpContext.Current.Server.MapPath("CategoryImage/") + filename);
            Uploader.SaveAs(HttpContext.Current.Server.MapPath("CategoryImage/") + filename);
            sPath = PathHotelCateImage() + filename;

        }
        return sPath;
    }
    public string PathHotelCateImage()
    {
        string ImgPathHotel = ConfigurationSettings.AppSettings["HotelCateImagePath"];
        return ImgPathHotel;
    }
    public DataTable GetContinent(string Company, string counter)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            _CommandText = "SP_Country";
            if (!String.IsNullOrEmpty(Company))
            {
                param[0] = new SqlParameter("@PARAM_Company", SqlDbType.VarChar);
                param[0].Value = Company;
            }
            param[1] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[1].Value = counter;

            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
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
    public bool HolidayCountry_Insert(string CAT_CODE, string Country_CODE, string Country_NAME, string DESCRIPTION, string DEST_TITLE, string URL, string MATADATA, string IMAGE, string IMAGE_TITLE, string OVERVIEW, string PLACE_TO_GO, string THINGS_TO_GO, string ESSENTIAL_INFO, string REMARKS, string CREATED_BY, string company, string Counter)
    {
        string _CommandText = "SP_Country";

        SqlParameter[] param = new SqlParameter[17];
        try
        {
            if (!String.IsNullOrEmpty(CAT_CODE))
            {
                param[0] = new SqlParameter("@PARAMCAT_CODE", SqlDbType.VarChar);
                param[0].Value = CAT_CODE;
            }
            if (!String.IsNullOrEmpty(Country_CODE))
            {
                param[1] = new SqlParameter("@PARAM_CODE", SqlDbType.NVarChar);
                param[1].Value = Country_CODE;
            }
            if (!String.IsNullOrEmpty(Country_NAME))
            {
                param[2] = new SqlParameter("@PARAM_NAME", SqlDbType.NVarChar);
                param[2].Value = Country_NAME;
            }
            if (!String.IsNullOrEmpty(DESCRIPTION))
            {
                param[3] = new SqlParameter("@PARAM_DESCRIPTION", SqlDbType.NVarChar);
                param[3].Value = DESCRIPTION;
            }
            if (!String.IsNullOrEmpty(DEST_TITLE))
            {
                param[4] = new SqlParameter("@PARAM_TITLE", SqlDbType.NVarChar);
                param[4].Value = DEST_TITLE;
            }
            if (!String.IsNullOrEmpty(URL))
            {
                param[5] = new SqlParameter("@PARAM_URL", SqlDbType.NVarChar);
                param[5].Value = URL;
            }
            if (!String.IsNullOrEmpty(MATADATA))
            {
                param[6] = new SqlParameter("@PARAM_METATAG", SqlDbType.NVarChar);
                param[6].Value = MATADATA;
            }
            if (!String.IsNullOrEmpty(IMAGE))
            {
                param[7] = new SqlParameter("@PARAM_IMAGE", SqlDbType.NVarChar);
                param[7].Value = IMAGE;
            }

            if (!String.IsNullOrEmpty(IMAGE_TITLE))
            {
                param[8] = new SqlParameter("@PARAM_IMAGE_TITLE", SqlDbType.NVarChar);
                param[8].Value = IMAGE_TITLE;
            }

            if (!String.IsNullOrEmpty(OVERVIEW))
            {
                param[9] = new SqlParameter("@PARAM_OVERVIEW", SqlDbType.NVarChar);
                param[9].Value = OVERVIEW;
            }
            if (!String.IsNullOrEmpty(PLACE_TO_GO))
            {
                param[10] = new SqlParameter("@PARAM_PLACE_TO_GO", SqlDbType.NVarChar);
                param[10].Value = PLACE_TO_GO;
            }
            if (!String.IsNullOrEmpty(THINGS_TO_GO))
            {
                param[11] = new SqlParameter("@PARAM_THINGS_TO_GO", SqlDbType.NVarChar);
                param[11].Value = THINGS_TO_GO;
            }
            if (!String.IsNullOrEmpty(ESSENTIAL_INFO))
            {
                param[12] = new SqlParameter("@PARAM_ESSENTIAL_INFO", SqlDbType.NVarChar);
                param[12].Value = ESSENTIAL_INFO;
            }
            if (!String.IsNullOrEmpty(REMARKS))
            {
                param[13] = new SqlParameter("@PARAM_REMARKS", SqlDbType.NVarChar);
                param[13].Value = REMARKS;
            }
            if (!String.IsNullOrEmpty(CREATED_BY))
            {
                param[14] = new SqlParameter("@PARAM_CREATERD_BY", SqlDbType.NVarChar);
                param[14].Value = CREATED_BY;
            }
            if (!String.IsNullOrEmpty(company))
            {
                param[15] = new SqlParameter("@PARAM_Company", SqlDbType.NVarChar);
                param[15].Value = company;
            }
            if (!String.IsNullOrEmpty(Counter))
            {
                param[16] = new SqlParameter("@Counter", SqlDbType.NVarChar);
                param[16].Value = Counter;
            }

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
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

    public bool HolidayCountry_Update(string CountryId, string Country_CODE, string Country_NAME, string DESCRIPTION, string DEST_TITLE, string URL, string MATADATA, string IMAGE_TITLE, string OVERVIEW, string PLACE_TO_GO, string THINGS_TO_GO, string ESSENTIAL_INFO, string REMARKS, string CREATED_BY, string company, string Image, string Counter)
    {
        string _CommandText = "SP_Country";

        SqlParameter[] param = new SqlParameter[17];
        try
        {
            if (!String.IsNullOrEmpty(CountryId))
            {
                param[0] = new SqlParameter("@PARAM_ID", SqlDbType.Int);
                param[0].Value = Convert.ToInt32(CountryId);
            }
            if (!String.IsNullOrEmpty(Country_CODE))
            {
                param[1] = new SqlParameter("@PARAM_CODE", SqlDbType.NVarChar);
                param[1].Value = Country_CODE;
            }
            if (!String.IsNullOrEmpty(Country_NAME))
            {
                param[2] = new SqlParameter("@PARAM_NAME", SqlDbType.NVarChar);
                param[2].Value = Country_NAME;
            }
            if (!String.IsNullOrEmpty(DESCRIPTION))
            {
                param[3] = new SqlParameter("@PARAM_DESCRIPTION", SqlDbType.NVarChar);
                param[3].Value = DESCRIPTION;
            }
            if (!String.IsNullOrEmpty(DEST_TITLE))
            {
                param[4] = new SqlParameter("@PARAM_TITLE", SqlDbType.NVarChar);
                param[4].Value = DEST_TITLE;
            }
            if (!String.IsNullOrEmpty(URL))
            {
                param[5] = new SqlParameter("@PARAM_URL", SqlDbType.NVarChar);
                param[5].Value = URL;
            }
            if (!String.IsNullOrEmpty(MATADATA))
            {
                param[6] = new SqlParameter("@PARAM_METATAG", SqlDbType.NVarChar);
                param[6].Value = MATADATA;
            }

            if (!String.IsNullOrEmpty(IMAGE_TITLE))
            {
                param[7] = new SqlParameter("@PARAM_IMAGE_TITLE", SqlDbType.NVarChar);
                param[7].Value = IMAGE_TITLE;
            }

            if (!String.IsNullOrEmpty(OVERVIEW))
            {
                param[8] = new SqlParameter("@PARAM_OVERVIEW", SqlDbType.NVarChar);
                param[8].Value = OVERVIEW;
            }
            if (!String.IsNullOrEmpty(PLACE_TO_GO))
            {
                param[9] = new SqlParameter("@PARAM_PLACE_TO_GO", SqlDbType.NVarChar);
                param[9].Value = PLACE_TO_GO;
            }
            if (!String.IsNullOrEmpty(THINGS_TO_GO))
            {
                param[10] = new SqlParameter("@PARAM_THINGS_TO_GO", SqlDbType.NVarChar);
                param[10].Value = THINGS_TO_GO;
            }
            if (!String.IsNullOrEmpty(ESSENTIAL_INFO))
            {
                param[11] = new SqlParameter("@PARAM_ESSENTIAL_INFO", SqlDbType.NVarChar);
                param[11].Value = ESSENTIAL_INFO;
            }
            if (!String.IsNullOrEmpty(REMARKS))
            {
                param[12] = new SqlParameter("@PARAM_REMARKS", SqlDbType.NVarChar);
                param[12].Value = REMARKS;
            }
            if (!String.IsNullOrEmpty(CREATED_BY))
            {
                param[13] = new SqlParameter("@PARAM_CREATERD_BY", SqlDbType.NVarChar);
                param[13].Value = CREATED_BY;
            }
            if (!String.IsNullOrEmpty(company))
            {
                param[14] = new SqlParameter("@PARAM_Company", SqlDbType.NVarChar);
                param[14].Value = company;
            }
            if (!String.IsNullOrEmpty(Image))
            {
                param[15] = new SqlParameter("@PARAM_IMAGE", SqlDbType.NVarChar);
                param[15].Value = Image;
            }
            if (!String.IsNullOrEmpty(Counter))
            {
                param[16] = new SqlParameter("@Counter", SqlDbType.NVarChar);
                param[16].Value = Counter;
            }

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
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

    public DataTable GetHolidayCountry(string CAT_CODE, string Country_CODE, string date, string Country_ID, string CountryName, string Company, string counter)
    {
        DataSet ds = new DataSet();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[7];
        try
        {
            _CommandText = "SP_Country";
            if (!String.IsNullOrEmpty(CAT_CODE))
            {
                param[0] = new SqlParameter("@PARAMCAT_CODE", SqlDbType.NVarChar);
                param[0].Value = CAT_CODE;
            }
            if (!String.IsNullOrEmpty(Country_CODE))
            {
                param[1] = new SqlParameter("@PARAM_CODE", SqlDbType.NVarChar);
                param[1].Value = Country_CODE;
            }
            if (!String.IsNullOrEmpty(date))
            {
                param[2] = new SqlParameter("@PARAM_CREATED_DATE", SqlDbType.DateTime);
                param[2].Value = Convert.ToDateTime(date);
            }
            if (!String.IsNullOrEmpty(Country_ID))
            {
                param[3] = new SqlParameter("@PARAM_ID", SqlDbType.Int);
                param[3].Value = Convert.ToInt32(Country_ID);
            }
            if (!String.IsNullOrEmpty(CountryName))
            {
                param[4] = new SqlParameter("@PARAM_NAME", SqlDbType.VarChar);
                param[4].Value = CountryName;
            }
            if (!String.IsNullOrEmpty(Company))
            {
                param[5] = new SqlParameter("@PARAM_Company", SqlDbType.VarChar);
                param[5].Value = Company;
            }
            param[6] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[6].Value = counter;
            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            return ds.Tables[0];

        }
        catch (Exception ex)
        {
            ds = null;
            return ds.Tables[0];
        }
    }
    public bool HolidayCountry_Delete(int Id, string counter)
    {
        string _CommandText = "SP_Country";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            param[0] = new SqlParameter("@PARAM_ID", SqlDbType.Int);
            param[0].Value = Id;
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
    public bool HolidayCategory_Insert(string CAT_CODE, string CAT_NAME, string DESCRIPTION, string CAT_TITLE, string URL, string MATADATA, string IMAGE, string IMAGE_TITLE, string OVERVIEW, string PLACE_TO_GO, string THINGS_TO_GO, string ESSENTIAL_INFO, string REMARKS, string CREATED_BY, string Company, string Counter)
    {
        string _CommandText = "SP_CATEGORY";

        SqlParameter[] param = new SqlParameter[16];
        try
        {
            if (!String.IsNullOrEmpty(CAT_CODE))
            {
                param[0] = new SqlParameter("@PARAM_CODE", SqlDbType.NVarChar);
                param[0].Value = CAT_CODE;
            }
            if (!String.IsNullOrEmpty(CAT_NAME))
            {
                param[1] = new SqlParameter("@PARAM_NAME", SqlDbType.NVarChar);
                param[1].Value = CAT_NAME;
            }
            if (!String.IsNullOrEmpty(DESCRIPTION))
            {
                param[2] = new SqlParameter("@PARAM_DESCRIPTION", SqlDbType.NVarChar);
                param[2].Value = DESCRIPTION;
            }
            if (!String.IsNullOrEmpty(CAT_TITLE))
            {
                param[3] = new SqlParameter("@PARAM_TITLE", SqlDbType.NVarChar);
                param[3].Value = CAT_TITLE;
            }
            if (!String.IsNullOrEmpty(URL))
            {
                param[4] = new SqlParameter("@PARAM_URL", SqlDbType.NVarChar);
                param[4].Value = URL;
            }
            if (!String.IsNullOrEmpty(MATADATA))
            {
                param[5] = new SqlParameter("@PARAM_MATADATA", SqlDbType.NVarChar);
                param[5].Value = MATADATA;
            }
            if (!String.IsNullOrEmpty(IMAGE))
            {
                param[6] = new SqlParameter("@PARAM_IMAGE", SqlDbType.NVarChar);
                param[6].Value = IMAGE;
            }

            if (!String.IsNullOrEmpty(IMAGE_TITLE))
            {
                param[7] = new SqlParameter("@PARAM_IMAGE_TITLE", SqlDbType.NVarChar);
                param[7].Value = IMAGE_TITLE;
            }
            if (!String.IsNullOrEmpty(OVERVIEW))
            {
                param[8] = new SqlParameter("@PARAM_OVERVIEW", SqlDbType.NVarChar);
                param[8].Value = OVERVIEW;
            }
            if (!String.IsNullOrEmpty(PLACE_TO_GO))
            {
                param[9] = new SqlParameter("@PARAM_PLACE_TO_GO", SqlDbType.NVarChar);
                param[9].Value = PLACE_TO_GO;
            }
            if (!String.IsNullOrEmpty(THINGS_TO_GO))
            {
                param[10] = new SqlParameter("@PARAM_THINGS_TO_GO", SqlDbType.NVarChar);
                param[10].Value = THINGS_TO_GO;
            }
            if (!String.IsNullOrEmpty(ESSENTIAL_INFO))
            {
                param[11] = new SqlParameter("@PARAM_ESSENTIAL_INFO", SqlDbType.NVarChar);
                param[11].Value = ESSENTIAL_INFO;
            }
            if (!String.IsNullOrEmpty(REMARKS))
            {
                param[12] = new SqlParameter("@PARAM_REMARKS", SqlDbType.NVarChar);
                param[12].Value = REMARKS;
            }
            if (!String.IsNullOrEmpty(CREATED_BY))
            {
                param[13] = new SqlParameter("@PARAM_CREATED_BY", SqlDbType.NVarChar);
                param[13].Value = CREATED_BY;
            }
            if (!String.IsNullOrEmpty(Company))
            {
                param[14] = new SqlParameter("@PARAM_COMPANY", SqlDbType.NVarChar);
                param[14].Value = Company;
            }
            if (!String.IsNullOrEmpty(Counter))
            {
                param[15] = new SqlParameter("@Counter", SqlDbType.NVarChar);
                param[15].Value = Counter;
            }

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
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
    public DataTable GetHolidayCategory(string CAT_CODE, string CAT_NAME, string date, string cat_ID, string company, string counter)
    {
        DataSet ds = new DataSet();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[6];
        try
        {
            _CommandText = "SP_CATEGORY";
            if (!String.IsNullOrEmpty(CAT_CODE))
            {
                param[0] = new SqlParameter("@PARAM_CODE", SqlDbType.NVarChar);
                param[0].Value = CAT_CODE;
            }
            if (!String.IsNullOrEmpty(CAT_NAME))
            {
                param[1] = new SqlParameter("@PARAM_NAME", SqlDbType.NVarChar);
                param[1].Value = CAT_NAME;
            }
            if (!String.IsNullOrEmpty(date))
            {
                param[2] = new SqlParameter("@PARAM_CREATED_DATE", SqlDbType.DateTime);
                param[2].Value = Convert.ToDateTime(date);
            }
            if (!String.IsNullOrEmpty(cat_ID))
            {
                param[3] = new SqlParameter("@PARAM_ID", SqlDbType.Int);
                param[3].Value = Convert.ToInt32(cat_ID);
            }
            if (!String.IsNullOrEmpty(company))
            {
                param[4] = new SqlParameter("@PARAM_COMPANY", SqlDbType.NVarChar);
                param[4].Value = company;
            }
            param[5] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[5].Value = counter;
            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            return ds.Tables[0];

        }
        catch (Exception ex)
        {
            ds = null;
            return ds.Tables[0];
        }
    }
    public bool HolidayCategory_Delete(int Id, string counter)
    {
        string _CommandText = "SP_CATEGORY";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            param[0] = new SqlParameter("@PARAM_ID", SqlDbType.Int);
            param[0].Value = Id;
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
    public bool HolidayCategory_Update(string DESCRIPTION, string CAT_TITLE, string URL, string MATADATA, string IMAGE_TITLE, string OVERVIEW, string PLACE_TO_GO, string THINGS_TO_GO, string ESSENTIAL_INFO, string REMARKS, string CREATED_BY, string Id, string Company, string Image, string Counter)
    {
        string _CommandText = "SP_CATEGORY";

        SqlParameter[] param = new SqlParameter[15];
        try
        {

            if (!String.IsNullOrEmpty(DESCRIPTION))
            {
                param[0] = new SqlParameter("@PARAM_DESCRIPTION", SqlDbType.NVarChar);
                param[0].Value = DESCRIPTION;
            }
            if (!String.IsNullOrEmpty(CAT_TITLE))
            {
                param[1] = new SqlParameter("@PARAM_TITLE", SqlDbType.NVarChar);
                param[1].Value = CAT_TITLE;
            }
            if (!String.IsNullOrEmpty(URL))
            {
                param[2] = new SqlParameter("@PARAM_URL", SqlDbType.NVarChar);
                param[2].Value = URL;
            }
            if (!String.IsNullOrEmpty(MATADATA))
            {
                param[3] = new SqlParameter("@PARAM_MATADATA", SqlDbType.NVarChar);
                param[3].Value = MATADATA;
            }
            if (!String.IsNullOrEmpty(IMAGE_TITLE))
            {
                param[4] = new SqlParameter("@PARAM_IMAGE_TITLE", SqlDbType.NVarChar);
                param[4].Value = IMAGE_TITLE;
            }
            if (!String.IsNullOrEmpty(OVERVIEW))
            {
                param[5] = new SqlParameter("@PARAM_OVERVIEW", SqlDbType.NVarChar);
                param[5].Value = OVERVIEW;
            }
            if (!String.IsNullOrEmpty(PLACE_TO_GO))
            {
                param[6] = new SqlParameter("@PARAM_PLACE_TO_GO", SqlDbType.NVarChar);
                param[6].Value = PLACE_TO_GO;
            }
            if (!String.IsNullOrEmpty(THINGS_TO_GO))
            {
                param[7] = new SqlParameter("@PARAM_THINGS_TO_GO", SqlDbType.NVarChar);
                param[7].Value = THINGS_TO_GO;
            }
            if (!String.IsNullOrEmpty(ESSENTIAL_INFO))
            {
                param[8] = new SqlParameter("@PARAM_ESSENTIAL_INFO", SqlDbType.NVarChar);
                param[8].Value = ESSENTIAL_INFO;
            }
            if (!String.IsNullOrEmpty(REMARKS))
            {
                param[9] = new SqlParameter("@PARAM_REMARKS", SqlDbType.NVarChar);
                param[9].Value = REMARKS;
            }
            if (!String.IsNullOrEmpty(CREATED_BY))
            {
                param[10] = new SqlParameter("@PARAM_CREATED_BY", SqlDbType.NVarChar);
                param[10].Value = CREATED_BY;
            }
            if (!String.IsNullOrEmpty(Id))
            {
                param[11] = new SqlParameter("@PARAM_ID", SqlDbType.Int);
                param[11].Value = Convert.ToInt32(Id);
            }
            if (!String.IsNullOrEmpty(Company))
            {
                param[12] = new SqlParameter("@PARAM_COMPANY", SqlDbType.NVarChar);
                param[12].Value = Company;
            }

            if (!String.IsNullOrEmpty(Image))
            {
                param[13] = new SqlParameter("@PARAM_IMAGE", SqlDbType.NVarChar);
                param[13].Value = Image;
            }
            if (!String.IsNullOrEmpty(Counter))
            {
                param[14] = new SqlParameter("@Counter", SqlDbType.NVarChar);
                param[14].Value = Counter;
            }

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
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


    public bool HolidayDest_Insert(string CAT_CODE, string DEST_CODE, string DEST_NAME, string DESCRIPTION, string DEST_TITLE, string URL, string MATADATA, string IMAGE, string IMAGE_TITLE, string Dest, string OVERVIEW, string PLACE_TO_GO, string THINGS_TO_GO, string ESSENTIAL_INFO, string REMARKS, string CREATED_BY, string comp, string country, string Counter)
    {
        string _CommandText = "SP_DESTINATION";

        SqlParameter[] param = new SqlParameter[19];
        try
        {
            if (!String.IsNullOrEmpty(CAT_CODE))
            {
                param[0] = new SqlParameter("@PARAMCAT_CODE", SqlDbType.VarChar);
                param[0].Value = CAT_CODE;
            }
            if (!String.IsNullOrEmpty(DEST_CODE))
            {
                param[1] = new SqlParameter("@PARAM_CODE", SqlDbType.NVarChar);
                param[1].Value = DEST_CODE;
            }
            if (!String.IsNullOrEmpty(DEST_NAME))
            {
                param[2] = new SqlParameter("@PARAM_NAME", SqlDbType.NVarChar);
                param[2].Value = DEST_NAME;
            }
            if (!String.IsNullOrEmpty(DESCRIPTION))
            {
                param[3] = new SqlParameter("@PARAM_DESCRIPTION", SqlDbType.NVarChar);
                param[3].Value = DESCRIPTION;
            }
            if (!String.IsNullOrEmpty(DEST_TITLE))
            {
                param[4] = new SqlParameter("@PARAM_TITLE", SqlDbType.NVarChar);
                param[4].Value = DEST_TITLE;
            }
            if (!String.IsNullOrEmpty(URL))
            {
                param[5] = new SqlParameter("@PARAM_URL", SqlDbType.NVarChar);
                param[5].Value = URL;
            }
            if (!String.IsNullOrEmpty(MATADATA))
            {
                param[6] = new SqlParameter("@PARAM_METATAG", SqlDbType.NVarChar);
                param[6].Value = MATADATA;
            }
            if (!String.IsNullOrEmpty(IMAGE))
            {
                param[7] = new SqlParameter("@PARAM_IMAGE", SqlDbType.NVarChar);
                param[7].Value = IMAGE;
            }

            if (!String.IsNullOrEmpty(IMAGE_TITLE))
            {
                param[8] = new SqlParameter("@PARAM_IMAGE_TITLE", SqlDbType.NVarChar);
                param[8].Value = IMAGE_TITLE;
            }
            if (!String.IsNullOrEmpty(Dest))
            {
                param[9] = new SqlParameter("@PARAM_POPULAR_DEST", SqlDbType.NVarChar);
                param[9].Value = Dest;
            }

            if (!String.IsNullOrEmpty(OVERVIEW))
            {
                param[10] = new SqlParameter("@PARAM_OVERVIEW", SqlDbType.NVarChar);
                param[10].Value = OVERVIEW;
            }
            if (!String.IsNullOrEmpty(PLACE_TO_GO))
            {
                param[11] = new SqlParameter("@PARAM_PLACE_TO_GO", SqlDbType.NVarChar);
                param[11].Value = PLACE_TO_GO;
            }
            if (!String.IsNullOrEmpty(THINGS_TO_GO))
            {
                param[12] = new SqlParameter("@PARAM_THINGS_TO_GO", SqlDbType.NVarChar);
                param[12].Value = THINGS_TO_GO;
            }
            if (!String.IsNullOrEmpty(ESSENTIAL_INFO))
            {
                param[13] = new SqlParameter("@PARAM_ESSENTIAL_INFO", SqlDbType.NVarChar);
                param[13].Value = ESSENTIAL_INFO;
            }
            if (!String.IsNullOrEmpty(REMARKS))
            {
                param[14] = new SqlParameter("@PARAM_REMARKS", SqlDbType.NVarChar);
                param[14].Value = REMARKS;
            }
            if (!String.IsNullOrEmpty(CREATED_BY))
            {
                param[15] = new SqlParameter("@PARAM_CREATERD_BY", SqlDbType.NVarChar);
                param[15].Value = CREATED_BY;
            }

            if (!String.IsNullOrEmpty(country))
            {
                param[16] = new SqlParameter("@PARAM_Country", SqlDbType.NVarChar);
                param[16].Value = country;
            }
            if (!String.IsNullOrEmpty(comp))
            {
                param[17] = new SqlParameter("@PARAM_Company", SqlDbType.NVarChar);
                param[17].Value = comp;
            }
            if (!String.IsNullOrEmpty(Counter))
            {
                param[18] = new SqlParameter("@Counter", SqlDbType.NVarChar);
                param[18].Value = Counter;
            }

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
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

    public bool HolidayDest_Update(string DEST_Id, string DEST_CODE, string DEST_NAME, string DESCRIPTION, string DEST_TITLE, string URL, string MATADATA, string IMAGE_TITLE, string Dest, string OVERVIEW, string PLACE_TO_GO, string THINGS_TO_GO, string ESSENTIAL_INFO, string REMARKS, string CREATED_BY, string comp, string Image, string Counter)
    {
        string _CommandText = "SP_DESTINATION";

        SqlParameter[] param = new SqlParameter[18];
        try
        {
            if (!String.IsNullOrEmpty(DEST_Id))
            {
                param[0] = new SqlParameter("@PARAM_ID", SqlDbType.Int);
                param[0].Value = Convert.ToInt32(DEST_Id);
            }
            if (!String.IsNullOrEmpty(DEST_CODE))
            {
                param[1] = new SqlParameter("@PARAM_CODE", SqlDbType.NVarChar);
                param[1].Value = DEST_CODE;
            }
            if (!String.IsNullOrEmpty(DEST_NAME))
            {
                param[2] = new SqlParameter("@PARAM_NAME", SqlDbType.NVarChar);
                param[2].Value = DEST_NAME;
            }
            if (!String.IsNullOrEmpty(DESCRIPTION))
            {
                param[3] = new SqlParameter("@PARAM_DESCRIPTION", SqlDbType.NVarChar);
                param[3].Value = DESCRIPTION;
            }
            if (!String.IsNullOrEmpty(DEST_TITLE))
            {
                param[4] = new SqlParameter("@PARAM_TITLE", SqlDbType.NVarChar);
                param[4].Value = DEST_TITLE;
            }
            if (!String.IsNullOrEmpty(URL))
            {
                param[5] = new SqlParameter("@PARAM_URL", SqlDbType.NVarChar);
                param[5].Value = URL;
            }
            if (!String.IsNullOrEmpty(MATADATA))
            {
                param[6] = new SqlParameter("@PARAM_METATAG", SqlDbType.NVarChar);
                param[6].Value = MATADATA;
            }

            if (!String.IsNullOrEmpty(IMAGE_TITLE))
            {
                param[7] = new SqlParameter("@PARAM_IMAGE_TITLE", SqlDbType.NVarChar);
                param[7].Value = IMAGE_TITLE;
            }
            if (!String.IsNullOrEmpty(Dest))
            {
                param[8] = new SqlParameter("@PARAM_POPULAR_DEST", SqlDbType.NVarChar);
                param[8].Value = Dest;
            }

            if (!String.IsNullOrEmpty(OVERVIEW))
            {
                param[9] = new SqlParameter("@PARAM_OVERVIEW", SqlDbType.NVarChar);
                param[9].Value = OVERVIEW;
            }
            if (!String.IsNullOrEmpty(PLACE_TO_GO))
            {
                param[10] = new SqlParameter("@PARAM_PLACE_TO_GO", SqlDbType.NVarChar);
                param[10].Value = PLACE_TO_GO;
            }
            if (!String.IsNullOrEmpty(THINGS_TO_GO))
            {
                param[11] = new SqlParameter("@PARAM_THINGS_TO_GO", SqlDbType.NVarChar);
                param[11].Value = THINGS_TO_GO;
            }
            if (!String.IsNullOrEmpty(ESSENTIAL_INFO))
            {
                param[12] = new SqlParameter("@PARAM_ESSENTIAL_INFO", SqlDbType.NVarChar);
                param[12].Value = ESSENTIAL_INFO;
            }
            if (!String.IsNullOrEmpty(REMARKS))
            {
                param[13] = new SqlParameter("@PARAM_REMARKS", SqlDbType.NVarChar);
                param[13].Value = REMARKS;
            }
            if (!String.IsNullOrEmpty(CREATED_BY))
            {
                param[14] = new SqlParameter("@PARAM_CREATERD_BY", SqlDbType.NVarChar);
                param[14].Value = CREATED_BY;
            }
            if (!String.IsNullOrEmpty(comp))
            {
                param[15] = new SqlParameter("@PARAM_Company", SqlDbType.NVarChar);
                param[15].Value = comp;
            }

            if (!String.IsNullOrEmpty(Image))
            {
                param[16] = new SqlParameter("@PARAM_IMAGE", SqlDbType.NVarChar);
                param[16].Value = Image;
            }
            if (!String.IsNullOrEmpty(Counter))
            {
                param[17] = new SqlParameter("@Counter", SqlDbType.NVarChar);
                param[17].Value = Counter;
            }

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
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

    public DataTable GetHolidayDest(string CAT_CODE, string DEST_CODE, string date, string DEST_ID, string DestName, string Country, string company, string counter)
    {
        DataSet ds = new DataSet();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[8];
        try
        {
            _CommandText = "SP_DESTINATION";
            if (!String.IsNullOrEmpty(CAT_CODE))
            {
                param[0] = new SqlParameter("@PARAMCAT_CODE", SqlDbType.NVarChar);
                param[0].Value = CAT_CODE;
            }
            if (!String.IsNullOrEmpty(DEST_CODE))
            {
                param[1] = new SqlParameter("@PARAM_CODE", SqlDbType.NVarChar);
                param[1].Value = DEST_CODE;
            }
            if (!String.IsNullOrEmpty(date))
            {
                param[2] = new SqlParameter("@PARAM_CREATED_DATE", SqlDbType.DateTime);
                param[2].Value = Convert.ToDateTime(date);
            }
            if (!String.IsNullOrEmpty(DEST_ID))
            {
                param[3] = new SqlParameter("@PARAM_ID", SqlDbType.Int);
                param[3].Value = Convert.ToInt32(DEST_ID);
            }
            if (!String.IsNullOrEmpty(DestName))
            {
                param[4] = new SqlParameter("@PARAM_NAME", SqlDbType.VarChar);
                param[4].Value = DestName;
            }
            if (!String.IsNullOrEmpty(Country))
            {
                param[5] = new SqlParameter("@PARAM_Country", SqlDbType.VarChar);
                param[5].Value = Country;
            }
            if (!String.IsNullOrEmpty(company))
            {
                param[6] = new SqlParameter("@PARAM_Company", SqlDbType.VarChar);
                param[6].Value = company;
            }
            param[7] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[7].Value = counter;
            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            return ds.Tables[0];
        }
        catch (Exception ex)
        {
            ds = null;
            return ds.Tables[0];
        }
    }
    public bool HolidayDEST_Delete(int Id, string counter)
    {
        string _CommandText = "SP_DESTINATION";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            param[0] = new SqlParameter("@PARAM_ID", SqlDbType.Int);
            param[0].Value = Id;
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

    public DataTable GetDest(string Company, string Continent, string Country, string counter)
    {
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[4];
        try
        {
            if (!String.IsNullOrEmpty(Company))
            {
                param[0] = new SqlParameter("@PARAM_Company", SqlDbType.NVarChar);
                param[0].Value = Company;
            }
            if (!String.IsNullOrEmpty(Continent))
            {
                param[1] = new SqlParameter("@PARAMCAT_CODE", SqlDbType.NVarChar);
                param[1].Value = Continent;
            }
            if (!String.IsNullOrEmpty(Country))
            {
                param[2] = new SqlParameter("@PARAM_Country", SqlDbType.NVarChar);
                param[2].Value = Country;
            }
            param[3] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[3].Value = counter;

            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, "SP_DESTINATION", param);
            return ds.Tables[0];

        }
        catch (Exception ex)
        {
            ds = null;
            return ds.Tables[0];
        }
    }

    public DataTable GetCountry(string CAT_CODE, string company, string counter)
    {
        DataSet ds = new DataSet();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            _CommandText = "SP_DESTINATION";
            if (!String.IsNullOrEmpty(CAT_CODE))
            {
                param[0] = new SqlParameter("@PARAMCAT_CODE", SqlDbType.NVarChar);
                param[0].Value = CAT_CODE;
            }

            if (!String.IsNullOrEmpty(company))
            {
                param[1] = new SqlParameter("@PARAM_Company", SqlDbType.NVarChar);
                param[1].Value = company;
            }
            param[2] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[2].Value = counter;
            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            return ds.Tables[0];

        }
        catch (Exception ex)
        {
            ds = null;
            return ds.Tables[0];
        }
    }
    public DataTable GetCat(string counter)
    {
        DataSet ds = new DataSet();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            _CommandText = "SP_DESTINATION";

            param[0] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[0].Value = counter;
            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            return ds.Tables[0];

        }
        catch (Exception ex)
        {
            ds = null;
            return ds.Tables[0];
        }
    }

    public bool UpdateHolidayOrder(string dealOrder, string HolidayCode, string Heading, string ModifyBy, string Counter)
    {
        bool bMod = false;
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[5];
        try
        {
            _CommandText = "MapHoliDay";
            if (!String.IsNullOrEmpty(dealOrder))
            {
                param[0] = new SqlParameter("@paramHT_DealOrder", SqlDbType.Int);
                param[0].Value = Convert.ToInt16(dealOrder);
            }
            param[1] = new SqlParameter("@paramHT_TypeCode", SqlDbType.VarChar);
            param[1].Value = HolidayCode;
            param[2] = new SqlParameter("@paramHT_Heading", SqlDbType.VarChar);
            param[2].Value = Heading;
            param[3] = new SqlParameter("@paramHT_ModifyBy", SqlDbType.VarChar, 50);
            param[3].Value = ModifyBy;
            param[4] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[4].Value = Counter;

            int iM = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            if (iM != -1)
            {
                bMod = true;
            }
            return bMod;
        }
        catch
        {
            return bMod;
        }

    }

    public bool MapCate_Insert(DataTable CateMapDetails, string Counter)
    {
        string _CommandText = "MapHoliDay";

        SqlParameter[] param = new SqlParameter[2];
        try
        {
            param[0] = new SqlParameter("@paramHolidayType", CateMapDetails);
            param[1] = new SqlParameter("@Counter", SqlDbType.NVarChar);
            param[1].Value = Counter;
            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
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
    public DataTable GetMapCat(string category, string destination, string date, string Heading, string DisplayType, string company, string counter)
    {
        DataSet ds = new DataSet();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[7];
        try
        {
            _CommandText = "MapHoliDay";
            if (!String.IsNullOrEmpty(category))
            {
                param[0] = new SqlParameter("@paramHT_TypeCode", SqlDbType.NVarChar);
                param[0].Value = category;
            }
            if (!String.IsNullOrEmpty(destination))
            {
                param[1] = new SqlParameter("@paramHT_Destination", SqlDbType.NVarChar);
                param[1].Value = destination;
            }
            if (!String.IsNullOrEmpty(date))
            {
                param[2] = new SqlParameter("@paramHT_ModifyDate", SqlDbType.DateTime);
                param[2].Value = Convert.ToDateTime(date);
            }
            if (!String.IsNullOrEmpty(Heading))
            {
                param[3] = new SqlParameter("@paramHT_Heading", SqlDbType.NVarChar);
                param[3].Value = Heading;
            }
            if (!String.IsNullOrEmpty(DisplayType))
            {
                param[4] = new SqlParameter("@paramHT_DisplayType", SqlDbType.NVarChar);
                param[4].Value = DisplayType;
            }
            if (!String.IsNullOrEmpty(company))
            {
                param[5] = new SqlParameter("@paramHT_Company", SqlDbType.NVarChar);
                param[5].Value = company;
            }
            param[6] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[6].Value = counter;
            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            return ds.Tables[0];

        }
        catch (Exception ex)
        {
            ds = null;
            return ds.Tables[0];
        }
    }

    public bool MapCate_Delete(int Id, string counter)
    {
        string _CommandText = "MapHoliDay";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            param[0] = new SqlParameter("@paramHT_ID", SqlDbType.Int);
            param[0].Value = Id;
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

    public bool AddHoliday(string CAT_CODE, string CAT_NAME, string DESCRIPTION, string CAT_TITLE, string URL, string MATADATA, string IMAGE, string IMAGE_TITLE, string OVERVIEW, string Company, string CREATED_BY, string Counter)
    {
        string _CommandText = "Insert_Holiday";

        SqlParameter[] param = new SqlParameter[12];
        try
        {
            if (!String.IsNullOrEmpty(CAT_CODE))
            {
                param[0] = new SqlParameter("@ParamHD_Code", SqlDbType.NVarChar);
                param[0].Value = CAT_CODE;
            }
            if (!String.IsNullOrEmpty(CAT_NAME))
            {
                param[1] = new SqlParameter("@ParamHD_Name", SqlDbType.NVarChar);
                param[1].Value = CAT_NAME;
            }
            if (!String.IsNullOrEmpty(DESCRIPTION))
            {
                param[2] = new SqlParameter("@ParamHD_Description", SqlDbType.NVarChar);
                param[2].Value = DESCRIPTION;
            }
            if (!String.IsNullOrEmpty(CAT_TITLE))
            {
                param[3] = new SqlParameter("@ParamHD_Title", SqlDbType.NVarChar);
                param[3].Value = CAT_TITLE;
            }
            if (!String.IsNullOrEmpty(URL))
            {
                param[4] = new SqlParameter("@ParamHD_URL", SqlDbType.NVarChar);
                param[4].Value = URL;
            }
            if (!String.IsNullOrEmpty(MATADATA))
            {
                param[5] = new SqlParameter("@ParamHD_Metadata", SqlDbType.NVarChar);
                param[5].Value = MATADATA;
            }
            if (!String.IsNullOrEmpty(IMAGE))
            {
                param[6] = new SqlParameter("@ParamHD_Image", SqlDbType.NVarChar);
                param[6].Value = IMAGE;
            }

            if (!String.IsNullOrEmpty(IMAGE_TITLE))
            {
                param[7] = new SqlParameter("@ParamHD_Image_Title", SqlDbType.NVarChar);
                param[7].Value = IMAGE_TITLE;
            }
            if (!String.IsNullOrEmpty(OVERVIEW))
            {
                param[8] = new SqlParameter("@ParamHD_Overview", SqlDbType.NVarChar);
                param[8].Value = OVERVIEW;
            }

            if (!String.IsNullOrEmpty(Company))
            {
                param[9] = new SqlParameter("@ParamHD_Company", SqlDbType.NVarChar);
                param[9].Value = Company;
            }
            if (!String.IsNullOrEmpty(CREATED_BY))
            {
                param[10] = new SqlParameter("@ParamHD_Created_By", SqlDbType.NVarChar);
                param[10].Value = CREATED_BY;
            }
            if (!String.IsNullOrEmpty(Counter))
            {
                param[11] = new SqlParameter("@Counter", SqlDbType.NVarChar);
                param[11].Value = Counter;
            }

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
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


    public bool Update_Holiday(string Cat_ID, string DESCRIPTION, string CAT_TITLE, string URL, string MATADATA, string IMAGE_TITLE, string OVERVIEW, string Company, string CREATED_BY, string image, string Counter)
    {
        string _CommandText = "Insert_Holiday";

        SqlParameter[] param = new SqlParameter[11];
        try
        {
            if (!String.IsNullOrEmpty(Cat_ID))
            {
                param[0] = new SqlParameter("@ParamHD_Id", SqlDbType.Int);
                param[0].Value = Convert.ToInt32(Cat_ID);
            }

            if (!String.IsNullOrEmpty(DESCRIPTION))
            {
                param[1] = new SqlParameter("@ParamHD_Description", SqlDbType.NVarChar);
                param[1].Value = DESCRIPTION;
            }
            if (!String.IsNullOrEmpty(CAT_TITLE))
            {
                param[2] = new SqlParameter("@ParamHD_Title", SqlDbType.NVarChar);
                param[2].Value = CAT_TITLE;
            }
            if (!String.IsNullOrEmpty(URL))
            {
                param[3] = new SqlParameter("@ParamHD_URL", SqlDbType.NVarChar);
                param[3].Value = URL;
            }
            if (!String.IsNullOrEmpty(MATADATA))
            {
                param[4] = new SqlParameter("@ParamHD_Metadata", SqlDbType.NVarChar);
                param[4].Value = MATADATA;
            }

            if (!String.IsNullOrEmpty(IMAGE_TITLE))
            {
                param[5] = new SqlParameter("@ParamHD_Image_Title", SqlDbType.NVarChar);
                param[5].Value = IMAGE_TITLE;
            }
            if (!String.IsNullOrEmpty(OVERVIEW))
            {
                param[6] = new SqlParameter("@ParamHD_Overview", SqlDbType.NVarChar);
                param[6].Value = OVERVIEW;
            }

            if (!String.IsNullOrEmpty(Company))
            {
                param[7] = new SqlParameter("@ParamHD_Company", SqlDbType.NVarChar);
                param[7].Value = Company;
            }
            if (!String.IsNullOrEmpty(CREATED_BY))
            {
                param[8] = new SqlParameter("@ParamHD_Created_By", SqlDbType.NVarChar);
                param[8].Value = CREATED_BY;
            }
            if (!String.IsNullOrEmpty(image))
            {
                param[9] = new SqlParameter("@ParamHD_Image", SqlDbType.NVarChar);
                param[9].Value = image;
            }
            if (!String.IsNullOrEmpty(Counter))
            {
                param[10] = new SqlParameter("@Counter", SqlDbType.NVarChar);
                param[10].Value = Counter;
            }

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
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


    public DataTable GetHolidayDetail(string CAT_CODE, string cat_ID, string date, string Company, string counter)
    {
        DataSet ds = new DataSet();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[5];
        try
        {
            _CommandText = "Insert_Holiday";
            if (!String.IsNullOrEmpty(CAT_CODE))
            {
                param[0] = new SqlParameter("@ParamHD_Code", SqlDbType.NVarChar);
                param[0].Value = CAT_CODE;
            }
            if (!String.IsNullOrEmpty(cat_ID))
            {
                param[1] = new SqlParameter("@ParamHD_Id", SqlDbType.NVarChar);
                param[1].Value = cat_ID;
            }
            if (!String.IsNullOrEmpty(date))
            {
                param[2] = new SqlParameter("@ParamHD_Modify_Date", SqlDbType.DateTime);
                param[2].Value = Convert.ToDateTime(date);
            }
            if (!String.IsNullOrEmpty(Company))
            {
                param[3] = new SqlParameter("@ParamHD_Company", SqlDbType.VarChar);
                param[3].Value = Company;
            }
            param[4] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[4].Value = counter;
            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            return ds.Tables[0];

        }
        catch (Exception ex)
        {
            ds = null;
            return ds.Tables[0];
        }
    }
    public bool Holiday_Del(int Id, string counter)
    {
        string _CommandText = "Insert_Holiday";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            param[0] = new SqlParameter("@ParamHD_Id", SqlDbType.Int);
            param[0].Value = Id;
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

    public DataTable GetHolidayTypeDest(string company, string counter)
    {
        DataSet ds = new DataSet();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            _CommandText = "MapHoliDay";
            if (!String.IsNullOrEmpty(company))
            {
                param[0] = new SqlParameter("@paramHT_Company", SqlDbType.VarChar);
                param[0].Value = company;
            }
            param[1] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[1].Value = counter;
            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            return ds.Tables[0];

        }
        catch (Exception ex)
        {
            ds = null;
            return ds.Tables[0];
        }
    }

    public bool MarketingHolidayDeal_Insert(string HMD_Name, string HMD_Link, string HMD_StartDate, string HMD_EndDate, string HMD_Merchant, string HMD_Sold,
                      string HMD_Redeemed, string MerchantMax, string MerchantMin, string HMD_Refund_Percent, string HMD_Quick_Info, string HDM_ModifiedBy,
        string Maker, string DealId, string ExpireDate, string Place, string Video, string Map, string Calender, string Comparision, string QuickCard, string TripAdvisor,
        string Excursion, string Wowchers, string night, string Depart, string Counter)
    {
        string _CommandText = "sp_HolidayMarketingDeal";

        SqlParameter[] param = new SqlParameter[27];
        try
        {
            if (!String.IsNullOrEmpty(HMD_Name))
            {
                param[0] = new SqlParameter("@paramHMD_Name", SqlDbType.NVarChar);
                param[0].Value = HMD_Name;
            }
            if (!String.IsNullOrEmpty(HMD_Link))
            {
                param[1] = new SqlParameter("@paramHMD_Link", SqlDbType.NVarChar);
                param[1].Value = HMD_Link;
            }
            if (!String.IsNullOrEmpty(HMD_StartDate))
            {
                param[2] = new SqlParameter("@paramHMD_StartDate", SqlDbType.DateTime);
                param[2].Value = Convert.ToDateTime(HMD_StartDate);
            }
            if (!String.IsNullOrEmpty(HMD_EndDate))
            {
                param[3] = new SqlParameter("@paramHMD_EndDate", SqlDbType.DateTime);
                param[3].Value = Convert.ToDateTime(HMD_EndDate);
            }
            if (!String.IsNullOrEmpty(HMD_Merchant))
            {
                param[4] = new SqlParameter("@paramHMD_Merchant", SqlDbType.NVarChar);
                param[4].Value = HMD_Merchant;
            }
            if (!String.IsNullOrEmpty(HMD_Sold))
            {
                param[5] = new SqlParameter("@paramHMD_Sold", SqlDbType.Int);
                param[5].Value = Convert.ToInt16(HMD_Sold);
            }
            if (!String.IsNullOrEmpty(HMD_Redeemed))
            {
                param[6] = new SqlParameter("@paramHMD_Redeemed", SqlDbType.Int);
                param[6].Value = Convert.ToInt16(HMD_Redeemed);
            }

            if (!String.IsNullOrEmpty(MerchantMax))
            {
                param[7] = new SqlParameter("@paramHMD_MerchantMax_Percent", SqlDbType.Int);
                param[7].Value = Convert.ToInt16(MerchantMax);
            }
            if (!String.IsNullOrEmpty(MerchantMin))
            {
                param[8] = new SqlParameter("@paramHMD_MerchantMin_Percent", SqlDbType.Int);
                param[8].Value = Convert.ToInt16(MerchantMin);
            }
            if (!String.IsNullOrEmpty(HMD_Refund_Percent))
            {
                param[9] = new SqlParameter("@paramHMD_Refund_Percent", SqlDbType.Int);
                param[9].Value = Convert.ToInt16(HMD_Refund_Percent);
            }

            if (!String.IsNullOrEmpty(HMD_Quick_Info))
            {
                param[10] = new SqlParameter("@paramHMD_Quick_Info", SqlDbType.NVarChar);
                param[10].Value = HMD_Quick_Info;
            }
            if (!String.IsNullOrEmpty(HDM_ModifiedBy))
            {
                param[11] = new SqlParameter("@paramHDM_ModifiedBy", SqlDbType.NVarChar);
                param[11].Value = HDM_ModifiedBy;
            }
            if (!String.IsNullOrEmpty(Maker))
            {
                param[12] = new SqlParameter("@paramHDM_Maker", SqlDbType.VarChar);
                param[12].Value = Maker;
            }
            if (!String.IsNullOrEmpty(DealId))
            {
                param[13] = new SqlParameter("@paramHDM_DealId", SqlDbType.VarChar);
                param[13].Value = DealId;
            }
            if (!String.IsNullOrEmpty(ExpireDate))
            {
                param[14] = new SqlParameter("@paramHDM_ExpireDate", SqlDbType.DateTime);
                param[14].Value = Convert.ToDateTime(ExpireDate);
            }
            if (!String.IsNullOrEmpty(Place))
            {
                param[15] = new SqlParameter("@paramHDM_Place", SqlDbType.VarChar);
                param[15].Value = Place;
            }
            if (!String.IsNullOrEmpty(Video))
            {
                param[16] = new SqlParameter("@paramHDM_Video", SqlDbType.VarChar);
                param[16].Value = Video;
            }
            if (!String.IsNullOrEmpty(Map))
            {
                param[17] = new SqlParameter("@paramHDM_Map", SqlDbType.NVarChar);
                param[17].Value = Map;
            }
            if (!String.IsNullOrEmpty(Calender))
            {
                param[18] = new SqlParameter("@paramHDM_Calender", SqlDbType.NVarChar);
                param[18].Value = Calender;
            }
            if (!String.IsNullOrEmpty(Comparision))
            {
                param[19] = new SqlParameter("@paramHDM_Comparision", SqlDbType.NVarChar);
                param[19].Value = Comparision;
            }
            if (!String.IsNullOrEmpty(QuickCard))
            {
                param[20] = new SqlParameter("@paramHDM_QuickCard", SqlDbType.NVarChar);
                param[20].Value = QuickCard;
            }
            if (!String.IsNullOrEmpty(TripAdvisor))
            {
                param[21] = new SqlParameter("@paramHDM_TripAdvisor", SqlDbType.NVarChar);
                param[21].Value = TripAdvisor;
            }
            if (!String.IsNullOrEmpty(Excursion))
            {
                param[22] = new SqlParameter("@paramHDM_Excursion", SqlDbType.NVarChar);
                param[22].Value = Excursion;
            }
            if (!String.IsNullOrEmpty(Wowchers))
            {
                param[23] = new SqlParameter("@paramHDM_Wowchers", SqlDbType.NVarChar);
                param[23].Value = Wowchers;
            }
            if (!String.IsNullOrEmpty(night))
            {
                param[24] = new SqlParameter("@paramHDM_Night", SqlDbType.NVarChar);
                param[24].Value = night;
            }
            if (!String.IsNullOrEmpty(Depart))
            {
                param[25] = new SqlParameter("@paramHDM_Department", SqlDbType.VarChar);
                param[25].Value = Depart;
            }
            if (!String.IsNullOrEmpty(Counter))
            {
                param[26] = new SqlParameter("@Counter", SqlDbType.NVarChar);
                param[26].Value = Counter;
            }
            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
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

    public DataTable GetMarketingHolidayDeal(string HMD_Name, string Merchant, string month, string year, string monthE, string yearE, string date, string DealID, string Dept, string counter)
    {
        DataSet ds = new DataSet();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[10];
        try
        {
            _CommandText = "sp_HolidayMarketingDeal";
            if (!String.IsNullOrEmpty(HMD_Name))
            {
                param[0] = new SqlParameter("@paramHMD_Name", SqlDbType.VarChar);
                param[0].Value = HMD_Name;
            }
            if (!String.IsNullOrEmpty(Merchant))
            {
                param[1] = new SqlParameter("@paramHMD_Merchant", SqlDbType.VarChar);
                param[1].Value = Merchant;
            }
            if (!String.IsNullOrEmpty(month))
            {
                param[2] = new SqlParameter("@paramMonth", SqlDbType.Int);
                param[2].Value = Convert.ToInt16(month);
            }
            if (!String.IsNullOrEmpty(year))
            {
                param[3] = new SqlParameter("@paramYear", SqlDbType.Int);
                param[3].Value = Convert.ToInt32(year);
            }
            if (!String.IsNullOrEmpty(monthE))
            {
                param[4] = new SqlParameter("@paramMonthE", SqlDbType.Int);
                param[4].Value = Convert.ToInt16(monthE);
            }
            if (!String.IsNullOrEmpty(yearE))
            {
                param[5] = new SqlParameter("@paramYearE", SqlDbType.Int);
                param[5].Value = Convert.ToInt32(yearE);
            }
            if (!String.IsNullOrEmpty(date))
            {
                param[6] = new SqlParameter("@paramHDM_ModifiedDate", SqlDbType.DateTime);
                param[6].Value = Convert.ToDateTime(date);
            }

            if (!String.IsNullOrEmpty(DealID))
            {
                param[7] = new SqlParameter("@paramHDM_DealId", SqlDbType.VarChar);
                param[7].Value = DealID;
            }

            if (!String.IsNullOrEmpty(Dept))
            {
                param[8] = new SqlParameter("@paramHDM_Department", SqlDbType.VarChar);
                param[8].Value = Dept;
            }
            param[9] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[9].Value = counter;
            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            return ds.Tables[0];

        }
        catch (Exception ex)
        {
            ds = null;
            return ds.Tables[0];
        }
    }



    public bool MarketingHolidayDeal_Del(string Id, string Dest, string counter)
    {
        string _CommandText = "sp_HolidayMarketingDeal";
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            param[0] = new SqlParameter("@paramHDM_DealId", SqlDbType.VarChar);
            param[0].Value = Id;
            if (!String.IsNullOrEmpty(Dest))
            {
                param[1] = new SqlParameter("@paramHMD_Name", SqlDbType.NVarChar);
                param[1].Value = Dest;
            }
            param[2] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[2].Value = counter;

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            if (count > 0)
                return true;
            else
                return false;
        }
        catch { return false; }
    }
    public bool MarketingHolidayDeal_Update(string HMD_Link, string HMD_StartDate, string HMD_EndDate, string HMD_Merchant, string HMD_Sold,
        string HMD_Redeemed, string MerchantMax, string MerchantMin, string HMD_Refund_Percent, string HMD_Quick_Info, string HDM_ModifiedBy, string Counter,
        string Maker, string DealId, string ExpireDate, string Place, string Video, string Map, string Calender, string Comparision, string QuickCard, string TripAdvisor,
        string Excursion, string Wowchers, string night, string HMD_Name, string Dept)
    {
        string _CommandText = "sp_HolidayMarketingDeal";

        SqlParameter[] param = new SqlParameter[27];
        try
        {

            if (!String.IsNullOrEmpty(HMD_Link))
            {
                param[0] = new SqlParameter("@paramHMD_Link", SqlDbType.NVarChar);
                param[0].Value = HMD_Link;
            }
            if (!String.IsNullOrEmpty(HMD_StartDate))
            {
                param[1] = new SqlParameter("@paramHMD_StartDate", SqlDbType.DateTime);
                param[1].Value = Convert.ToDateTime(HMD_StartDate);
            }
            if (!String.IsNullOrEmpty(HMD_EndDate))
            {
                param[2] = new SqlParameter("@paramHMD_EndDate", SqlDbType.DateTime);
                param[2].Value = Convert.ToDateTime(HMD_EndDate);
            }
            if (!String.IsNullOrEmpty(HMD_Merchant))
            {
                param[3] = new SqlParameter("@paramHMD_Merchant", SqlDbType.NVarChar);
                param[3].Value = HMD_Merchant;
            }
            if (!String.IsNullOrEmpty(HMD_Sold))
            {
                param[4] = new SqlParameter("@paramHMD_Sold", SqlDbType.Int);
                param[4].Value = Convert.ToInt16(HMD_Sold);
            }
            if (!String.IsNullOrEmpty(HMD_Redeemed))
            {
                param[5] = new SqlParameter("@paramHMD_Redeemed", SqlDbType.Int);
                param[5].Value = Convert.ToInt16(HMD_Redeemed);
            }

            if (!String.IsNullOrEmpty(MerchantMax))
            {
                param[6] = new SqlParameter("@paramHMD_MerchantMax_Percent", SqlDbType.Int);
                param[6].Value = Convert.ToInt16(MerchantMax);
            }
            if (!String.IsNullOrEmpty(MerchantMin))
            {
                param[7] = new SqlParameter("@paramHMD_MerchantMin_Percent", SqlDbType.Int);
                param[7].Value = Convert.ToInt16(MerchantMin);
            }
            if (!String.IsNullOrEmpty(HMD_Refund_Percent))
            {
                param[8] = new SqlParameter("@paramHMD_Refund_Percent", SqlDbType.Int);
                param[8].Value = Convert.ToInt16(HMD_Refund_Percent);
            }
            if (!String.IsNullOrEmpty(HMD_Quick_Info))
            {
                param[9] = new SqlParameter("@paramHMD_Quick_Info", SqlDbType.NVarChar);
                param[9].Value = HMD_Quick_Info;
            }
            if (!String.IsNullOrEmpty(HDM_ModifiedBy))
            {
                param[10] = new SqlParameter("@paramHDM_ModifiedBy", SqlDbType.NVarChar);
                param[10].Value = HDM_ModifiedBy;
            }

            if (!String.IsNullOrEmpty(Counter))
            {
                param[11] = new SqlParameter("@Counter", SqlDbType.NVarChar);
                param[11].Value = Counter;
            }
            if (!String.IsNullOrEmpty(Maker))
            {
                param[12] = new SqlParameter("@paramHDM_Maker", SqlDbType.VarChar);
                param[12].Value = Maker;
            }
            if (!String.IsNullOrEmpty(DealId))
            {
                param[13] = new SqlParameter("@paramHDM_DealId", SqlDbType.VarChar);
                param[13].Value = DealId;
            }
            if (!String.IsNullOrEmpty(ExpireDate))
            {
                param[14] = new SqlParameter("@paramHDM_ExpireDate", SqlDbType.DateTime);
                param[14].Value = Convert.ToDateTime(ExpireDate);
            }
            if (!String.IsNullOrEmpty(Place))
            {
                param[15] = new SqlParameter("@paramHDM_Place", SqlDbType.VarChar);
                param[15].Value = Place;
            }
            if (!String.IsNullOrEmpty(Video))
            {
                param[16] = new SqlParameter("@paramHDM_Video", SqlDbType.VarChar);
                param[16].Value = Video;
            }
            if (!String.IsNullOrEmpty(Map))
            {
                param[17] = new SqlParameter("@paramHDM_Map", SqlDbType.NVarChar);
                param[17].Value = Map;
            }
            if (!String.IsNullOrEmpty(Calender))
            {
                param[18] = new SqlParameter("@paramHDM_Calender", SqlDbType.NVarChar);
                param[18].Value = Calender;
            }
            if (!String.IsNullOrEmpty(Comparision))
            {
                param[19] = new SqlParameter("@paramHDM_Comparision", SqlDbType.NVarChar);
                param[19].Value = Comparision;
            }
            if (!String.IsNullOrEmpty(QuickCard))
            {
                param[20] = new SqlParameter("@paramHDM_QuickCard", SqlDbType.NVarChar);
                param[20].Value = QuickCard;
            }
            if (!String.IsNullOrEmpty(TripAdvisor))
            {
                param[21] = new SqlParameter("@paramHDM_TripAdvisor", SqlDbType.NVarChar);
                param[21].Value = TripAdvisor;
            }
            if (!String.IsNullOrEmpty(Excursion))
            {
                param[22] = new SqlParameter("@paramHDM_Excursion", SqlDbType.NVarChar);
                param[22].Value = Excursion;
            }
            if (!String.IsNullOrEmpty(Wowchers))
            {
                param[23] = new SqlParameter("@paramHDM_Wowchers", SqlDbType.NVarChar);
                param[23].Value = Wowchers;
            }
            if (!String.IsNullOrEmpty(night))
            {
                param[24] = new SqlParameter("@paramHDM_Night", SqlDbType.NVarChar);
                param[24].Value = night;
            }
            if (!String.IsNullOrEmpty(HMD_Name))
            {
                param[25] = new SqlParameter("@paramHMD_Name", SqlDbType.NVarChar);
                param[25].Value = HMD_Name;
            }
            if (!String.IsNullOrEmpty(Dept))
            {
                param[26] = new SqlParameter("@paramHDM_Department", SqlDbType.VarChar);
                param[26].Value = Dept;
            }
            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
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

    public DataTable GetPromotionalDeal(string voucherNo, string bookingFrom, string BookingTo, string Dest, string DealRefNo, string counter)
    {
        DataSet ds = new DataSet();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[6];
        try
        {
            _CommandText = "PromotionDeal_Details";
            if (!String.IsNullOrEmpty(voucherNo))
            {
                param[0] = new SqlParameter("@paramVoucher", SqlDbType.NVarChar);
                param[0].Value = voucherNo;
            }
            if (!String.IsNullOrEmpty(bookingFrom))
            {
                param[1] = new SqlParameter("@paramFromDate", SqlDbType.DateTime);
                param[1].Value = Convert.ToDateTime(bookingFrom);
            }
            if (!String.IsNullOrEmpty(BookingTo))
            {
                param[2] = new SqlParameter("@paramToDate", SqlDbType.DateTime);
                param[2].Value = Convert.ToDateTime(BookingTo);
            }
            if (!String.IsNullOrEmpty(Dest))
            {
                param[3] = new SqlParameter("@paramDestination", SqlDbType.NVarChar);
                param[3].Value = Dest;
            }
            if (!String.IsNullOrEmpty(DealRefNo))
            {
                param[4] = new SqlParameter("@paramDealReferenceNo", SqlDbType.NVarChar);
                param[4].Value = DealRefNo;
            }
            param[5] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[5].Value = counter;
            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            return ds.Tables[0];

        }
        catch (Exception ex)
        {
            ds = null;
            return ds.Tables[0];
        }
    }
    public DataTable GetPromotionalDest(string counter)
    {
        DataSet ds = new DataSet();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            _CommandText = "PromotionDeal_Details";
            param[0] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[0].Value = counter;

            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            return ds.Tables[0];

        }
        catch (Exception ex)
        {
            ds = null;
            return ds.Tables[0];
        }
    }

    public string GetPromotionalDealXML(string voucherNo)
    {
        DataSet dsHotelDetails;
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            _CommandText = "PromotionDeal_Details";
            param[0] = new SqlParameter("@paramVoucher", SqlDbType.NVarChar, 50);
            param[0].Value = voucherNo;

            param[1] = new SqlParameter("@Counter", SqlDbType.NVarChar, 50);
            param[1].Value = "DETAILS";

            dsHotelDetails = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            return dsHotelDetails.Tables[0].Rows[0][0].ToString();
        }
        catch { return ""; }
    }
    public bool MarketingHolidayDeal_UpdateNight(string DealId, string night, string NightUPD, string Counter)
    {
        string _CommandText = "sp_HolidayMarketingDeal";

        SqlParameter[] param = new SqlParameter[4];
        try
        {
            if (!String.IsNullOrEmpty(DealId))
            {
                param[0] = new SqlParameter("@paramHDM_DealId", SqlDbType.VarChar);
                param[0].Value = DealId;
            }

            if (!String.IsNullOrEmpty(night))
            {
                param[1] = new SqlParameter("@paramHDM_Night", SqlDbType.NVarChar);
                param[1].Value = night;
            }

            if (!String.IsNullOrEmpty(NightUPD))
            {
                param[2] = new SqlParameter("@paramHDM_NightUpd", SqlDbType.NVarChar);
                param[2].Value = NightUPD;
            }

            if (!String.IsNullOrEmpty(Counter))
            {
                param[3] = new SqlParameter("@Counter", SqlDbType.NVarChar);
                param[3].Value = Counter;
            }

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
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
    public DataTable GetBannerDetails(string BannerCode, string modifyDate, string company, string counter)
    {
        DataSet ds = new DataSet();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[4];
        try
        {
            _CommandText = "sp_BannerUpload";
            if (!String.IsNullOrEmpty(BannerCode))
            {
                param[0] = new SqlParameter("@ParamBNR_Type", SqlDbType.NVarChar);
                param[0].Value = BannerCode;
            }
            if (!String.IsNullOrEmpty(modifyDate))
            {
                param[1] = new SqlParameter("@ParamBNR_ModifyDate", SqlDbType.DateTime);
                param[1].Value = Convert.ToDateTime(modifyDate);
            }
            if (!String.IsNullOrEmpty(company))
            {
                param[2] = new SqlParameter("@paramBNR_Company", SqlDbType.NVarChar);
                param[2].Value = company;
            }
            param[3] = new SqlParameter("@counter", SqlDbType.VarChar);
            param[3].Value = counter;
            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            return ds.Tables[0];

        }
        catch (Exception ex)
        {
            ds = null;
            return ds.Tables[0];
        }
    }
    public bool Banner_Insert(DataTable BannerDetails, string Counter)
    {
        string _CommandText = "sp_BannerUpload";

        SqlParameter[] param = new SqlParameter[2];
        try
        {
            param[0] = new SqlParameter("@paramBannerUpload", BannerDetails);
            param[1] = new SqlParameter("@counter", SqlDbType.NVarChar);
            param[1].Value = Counter;
            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
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
    public bool Banner_Del(int Id, string counter)
    {
        string _CommandText = "sp_BannerUpload";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            param[0] = new SqlParameter("@paramBNR_Id", SqlDbType.Int);
            param[0].Value = Id;
            param[1] = new SqlParameter("@counter", SqlDbType.VarChar);
            param[1].Value = counter;

            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            if (count > 0)
                return true;
            else
                return false;

        }
        catch { return false; }
    }
    public DataTable GetHolidayPKGDest(string heading, string holidaytype, string counter)
    {
        DataSet ds = new DataSet();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            _CommandText = "sp_HDPKG_Mapping";
            if (!String.IsNullOrEmpty(heading))
            {
                param[0] = new SqlParameter("@paramHT_Heading", SqlDbType.VarChar);
                param[0].Value = heading;
            }

            param[1] = new SqlParameter("@paramHT_TypeCode", SqlDbType.VarChar);
            param[1].Value = holidaytype;

            param[2] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[2].Value = counter;
            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            return ds.Tables[0];

        }
        catch (Exception ex)
        {
            ds = null;
            return ds.Tables[0];
        }
    }
    public DataTable GetMulCityDest(string counter)
    {
        DataSet ds = new DataSet();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            _CommandText = "sp_MultiCityBannerUpload";
            param[0] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[0].Value = counter;
            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            return ds.Tables[0];

        }
        catch (Exception ex)
        {
            ds = null;
            return ds.Tables[0];
        }
    }
    public DataTable Get_MCTBannerDetails(string BannerCode, string Dest, string modifyDate, string company, string counter)
    {
        DataSet ds = new DataSet();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[5];
        try
        {
            _CommandText = "sp_MultiCityBannerUpload";
            if (!String.IsNullOrEmpty(BannerCode))
            {
                param[0] = new SqlParameter("@ParamBNR_Type", SqlDbType.NVarChar);
                param[0].Value = BannerCode;
            }
            if (!String.IsNullOrEmpty(Dest))
            {
                param[1] = new SqlParameter("@ParamBNR_Dest", SqlDbType.NVarChar);
                param[1].Value = Dest;
            }
            if (!String.IsNullOrEmpty(modifyDate))
            {
                param[2] = new SqlParameter("@ParamBNR_ModifyDate", SqlDbType.DateTime);
                param[2].Value = Convert.ToDateTime(modifyDate);
            }
            if (!String.IsNullOrEmpty(company))
            {
                param[3] = new SqlParameter("@paramBNR_Company", SqlDbType.NVarChar);
                param[3].Value = company;
            }
            param[4] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[4].Value = counter;
            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            return ds.Tables[0];

        }
        catch (Exception ex)
        {
            ds = null;
            return ds.Tables[0];
        }
    }
    public bool MCTBanner_Insert(DataTable BannerDetails, string Counter)
    {
        string _CommandText = "sp_MultiCityBannerUpload";

        SqlParameter[] param = new SqlParameter[2];
        try
        {
            param[0] = new SqlParameter("@paramBannerUpload", BannerDetails);
            param[1] = new SqlParameter("@Counter", SqlDbType.NVarChar);
            param[1].Value = Counter;
            int count = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
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
    public bool MCTBanner_Del(int Id, string counter)
    {
        string _CommandText = "sp_MultiCityBannerUpload";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            param[0] = new SqlParameter("@paramBNR_Id", SqlDbType.Int);
            param[0].Value = Id;
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
    public DataSet HolidayReport(string refNo)
    {
        DataSet ds = new DataSet();
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            _CommandText = "sp_HolidayReport";

            param[0] = new SqlParameter("@paramRefNo", SqlDbType.VarChar);
            param[0].Value = refNo;
            ds = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            return ds;

        }
        catch (Exception ex)
        {
            ds = null;
            return ds;
        }
    }

    public bool InsertRemarks(string bookingRef, string ProdID, string Remarks, string ModifiedBy, int Counter)
    {
        bool bMod = false;

        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[5];
        try
        {
            _CommandText = "sp_Remarks";

            param[0] = new SqlParameter("@Param", SqlDbType.Int);
            param[0].Value = Counter;

            param[1] = new SqlParameter("@BookingRef", SqlDbType.NVarChar, 50);
            param[1].Value = bookingRef;

            param[2] = new SqlParameter("@ProdID", SqlDbType.NVarChar, 50);
            param[2].Value = ProdID;

            param[3] = new SqlParameter("@Remarks", SqlDbType.VarChar, 8000);
            param[3].Value = Remarks;

            param[4] = new SqlParameter("@ModifiedBy", SqlDbType.VarChar, 200);
            param[4].Value = ModifiedBy;

            int iM = SqlHelper.ExecuteNonQuery(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);

            if (iM == 1)
            {
                bMod = true;
            }
            //return bMod;
        }
        catch
        {
            bMod = false;
            return bMod;
        }
        return bMod;

    }
    public DataTable GetRemarks(string bookingRef, string userid, string fromDate, string ToDate)
    {
        DataSet dsbokDetail;
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[5];
        try
        {
            _CommandText = "sp_Remarks";
            param[0] = new SqlParameter("@Param", SqlDbType.Int);
            param[0].Value = 2;

            if (!String.IsNullOrEmpty(bookingRef))
            {
                param[1] = new SqlParameter("@BookingRef", SqlDbType.NVarChar, 50);
                param[1].Value = bookingRef;
            }
            if (!String.IsNullOrEmpty(userid))
            {
                param[2] = new SqlParameter("@ModifiedBy", SqlDbType.NVarChar, 50);
                param[2].Value = userid;
            }
            if (!String.IsNullOrEmpty(fromDate))
            {
                param[3] = new SqlParameter("@FromDate", SqlDbType.DateTime);
                param[3].Value = Convert.ToDateTime(fromDate);
            }
            if (!String.IsNullOrEmpty(ToDate))
            {
                param[4] = new SqlParameter("@ToDate", SqlDbType.DateTime);
                param[4].Value = Convert.ToDateTime(ToDate);
            }

            dsbokDetail = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            return dsbokDetail.Tables[0];
        }

        catch (Exception ex)
        {
            dsbokDetail = null;
            return dsbokDetail.Tables[0];
        }
    }

    public DataTable GetEnquiryReport(string EmailType, string FromDate, string toDate, string Counter)
    {
        DataSet dsbokDetail;
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[4];
        try
        {
            _CommandText = "sp_Enquiry";

            if (!String.IsNullOrEmpty(EmailType))
            {
                param[0] = new SqlParameter("@paramEmailType", SqlDbType.VarChar);
                param[0].Value = EmailType;
            }
            if (!String.IsNullOrEmpty(FromDate))
            {
                param[1] = new SqlParameter("@ParamFromDate", SqlDbType.DateTime);
                param[1].Value = Convert.ToDateTime(FromDate);
            }
            if (!String.IsNullOrEmpty(toDate))
            {
                param[2] = new SqlParameter("@paramToDate", SqlDbType.DateTime);
                param[2].Value = Convert.ToDateTime(toDate);
            }

            param[3] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[3].Value = Counter;


            dsbokDetail = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            return dsbokDetail.Tables[0];
        }

        catch (Exception ex)
        {
            dsbokDetail = null;
            return dsbokDetail.Tables[0];
        }
    }
    public DataTable GetEnquiryReportDetail(int id, string Counter)
    {
        DataSet dsbokDetail;
        string _CommandText = string.Empty;
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            _CommandText = "sp_Enquiry";

            param[0] = new SqlParameter("@paramID", SqlDbType.Int);
            param[0].Value = id;
            param[1] = new SqlParameter("@Counter", SqlDbType.VarChar);
            param[1].Value = Counter;


            dsbokDetail = SqlHelper.ExecuteDataset(DataConnection.GetConnectionHotel(), CommandType.StoredProcedure, _CommandText, param);
            return dsbokDetail.Tables[0];
        }

        catch (Exception ex)
        {
            dsbokDetail = null;
            return dsbokDetail.Tables[0];
        }
    }
}



