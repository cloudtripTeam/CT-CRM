using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Drawing;
using System.IO;


public partial class Admin_Default_US : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //string VisitorsIPAddress = string.Empty;
        //try
        //{
        //    if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
        //    {
        //        VisitorsIPAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
        //    }
        //    else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
        //    {
        //        VisitorsIPAddress = HttpContext.Current.Request.UserHostAddress;
        //    }
        //}
        //catch (Exception ex)
        //{

        //    //Handle Exceptions  
        //}
        if (Session["UserDetails"] != null)
        {
            if (!IsPostBack)
            {
                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
                if (!objUserDetail.isAuth("OwnItineraryUS"))
                {
                    Response.Redirect("~/Admin/AccessDenied.aspx");
                    return;
                }
                else
                {
                    ddlCampaign.Items.Clear();
                    ddlCampaign.Items.Clear();
                    if (objUserDetail.userID.ToLower() == "samft" || objUserDetail.userID.ToLower() == "seanft" || objUserDetail.userID.ToLower() == "robft" || objUserDetail.userID.ToLower() == "TIWARI".ToLower() || objUserDetail.userID.ToLower() == "Dinesh".ToLower())
                    {
                        CommanBinding.BindCampaignDetails(ref ddlCampaign, "FLTTROTT");
                        ddlCampaign.Items.Add("FT_DLCKR");
                        ddlCampaign.Items.Add("FT_JC");
                        ddlCampaign.Items.RemoveAt(0);
                    }
                    else
                    {

                        CommanBinding.BindPreferedCampaign(ref ddlCampaign);
                    }
                   
                }

            }
            CheckStatusCustomItinerary();
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Bind_Itin_details();
    }

    protected void Bind_Itin_details()
    {
        DataTable dt = new DataTable();
        SqlConnection sqlcon = DataConnection.GetConnectionCustomItinerary();

        try
        {
            sqlcon.Open();
            SqlCommand cmd = new SqlCommand();
            StringBuilder strcmd = new StringBuilder("SELECT top(1000) * FROM dbo.CustomItinUS INNER JOIN dbo.CustomItinBaseUS ON dbo.CustomItinUS.UniqueId = dbo.CustomItinBaseUS.UniqueId");

            strcmd.Append(" and CustomItinBaseUS.JourneyType ='" + ddlJourneyType.SelectedValue + "'");



            if (TxtFrom.Text.Trim().ToUpper() != "")
            {
                strcmd.Append(" and CustomItinBaseUS.Origin='" + TxtFrom.Text.Trim().ToUpper() + "'");
            }
            if (TxtTo.Text.Trim().ToUpper() != "")
            {
                strcmd.Append(" and CustomItinBaseUS.Destination='" + TxtTo.Text.Trim().ToUpper() + "'");
            }
            if (TxtAir.Text.Trim().ToUpper() != "")
            {
                strcmd.Append(" and CustomItinBaseUS.ValCarrier='" + TxtAir.Text.Trim().ToUpper() + "'");
            }

            if (txtvalidFrom.Text != "")
            {
                strcmd.Append(" and CustomItinBaseUS.ValidFrom='" + txtvalidFrom.Text.Trim().ToUpper() + "'");
            }

            if (txtvalidTo.Text != "")
            {
                strcmd.Append(" and CustomItinBaseUS.ValidTo='" + txtvalidTo.Text.Trim().ToUpper() + "'");
            }
            if (ddlCampaign.SelectedIndex != 0)
            {
                strcmd.Append(" and CustomItinBaseUS.Camp_ID='" + ddlCampaign.SelectedValue + "'");
            }
            if (txtUniqueId.Text != "")
            {
                strcmd.Append(" and CustomItinBaseUS.Uniqueid='" + txtUniqueId.Text.Trim().ToUpper() + "'");
            }



            strcmd.Append(" order by CustomItinUS.Uniqueid desc, CustomItinUS.Serialid asc");
            cmd.Connection = sqlcon;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strcmd.ToString();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);

            adp.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                //BindList(dt);
                gvDetails2.DataSource = dt;
                gvDetails2.DataBind();
                MultiView1.SetActiveView(View1);

            }
            else
            {
                MultiView1.SetActiveView(View2);

            }

        }
        catch (Exception Ex)
        {

        }
        finally
        {
            sqlcon.Close();
            //checkUseStatus();
        }

    }


    protected void Update(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvDetails2.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                bool isChecked = row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked;

                if (isChecked)
                {

                    SqlConnection sqlcon = DataConnection.GetConnectionCustomItinerary();
                    try
                    {
                        sqlcon.Open();

                        StringBuilder strcmd = new StringBuilder("UPDATE CustomItinUS SET UniqueId=@UniqueId, SerialId=@SerialId, IsConnect=@IsConnect, AirV=@AirV, AirlineName=@AirlineName, Class=@Class, FltNum=@FltNum, D_AirportCode=@D_AirportCode, D_AirportName=@D_AirportName, D_AirportCityCode=@D_AirportCityCode, D_AirportCityName=@D_AirportCityName, D_AirportCountryCode=@D_AirportCountryCode,");
                        strcmd.Append("D_AirportCountryName=@D_AirportCountryName, D_Terminal=@D_Terminal, D_Time=@D_Time, D_DateTimeStamp=@D_DateTimeStamp, A_AirportCode=@A_AirportCode, A_AirportName=@A_AirportName, A_AirportCityCode=@A_AirportCityCode, A_AirportCityName=@A_AirportCityName, A_AirportCountryCode=@A_AirportCountryCode, A_AirportCountryName=@A_AirportCountryName, A_Terminal=@A_Terminal,");
                        strcmd.Append("A_Time=@A_Time, A_DateTimeStamp=@A_DateTimeStamp, EquipType=@EquipType,IsNextDayCount=@IsNextDayCount, IsReturn=@IsReturn, BaggageInfo=@BaggageInfo, BI_Pieces=@BI_Pieces, BI_Price=@BI_Price, BI_Description=@BI_Description,");
                        strcmd.Append("Sector_Group=@Sector_Group, OperatingCarrierCode=@OperatingCarrierCode, OperatingCarrierName=@OperatingCarrierName,");
                        strcmd.Append("ElapsedTime=@ElapsedTime,ActualTime=@ActualTime,TechStopOver=@TechStopOver,TransitTime= @TransitTime,Sector_Key=@Sector_Key,Distance=@Distance,ETicket=@ETicket,ChangeOfPlane=@ChangeOfPlane,ParticipantLevel=@ParticipantLevel, OptionalServicesIndicator = @OptionalServicesIndicator,AvailabilitySource=@AvailabilitySource, BookingCodeInfo=@BookingCodeInfo,FareBasisCode=@FareBasisCode,ItinKey=@ItinKey,SupCode=@SupCode WHERE Id = @Id");

                        SqlCommand cmd = new SqlCommand(strcmd.ToString());

                        cmd.Parameters.AddWithValue("@UniqueId", row.Cells[3].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper());
                        cmd.Parameters.AddWithValue("@SerialId", row.Cells[4].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper());
                        cmd.Parameters.AddWithValue("@IsConnect", "false");
                        cmd.Parameters.AddWithValue("@AirV", row.Cells[5].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper());
                        cmd.Parameters.AddWithValue("@AirlineName", GetAirlineData(row.Cells[5].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper()));
                        cmd.Parameters.AddWithValue("@Class", "Y");
                        cmd.Parameters.AddWithValue("@FltNum", row.Cells[6].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper());
                        cmd.Parameters.AddWithValue("@D_AirportCode", row.Cells[7].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper());
                        cmd.Parameters.AddWithValue("@D_AirportName", GetValueUsingCode(row.Cells[7].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper()).Split(':')[0]);
                        cmd.Parameters.AddWithValue("@D_AirportCityCode", row.Cells[8].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper());
                        cmd.Parameters.AddWithValue("@D_AirportCityName", GetValueUsingCode(row.Cells[8].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper()).Split(':')[0]);
                        cmd.Parameters.AddWithValue("@D_Terminal", row.Cells[9].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper());
                        cmd.Parameters.AddWithValue("@D_AirportCountryCode", row.Cells[10].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper());
                        cmd.Parameters.AddWithValue("@D_AirportCountryName", GetValueUsingCode(row.Cells[7].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper()).Split(',')[1]);
                        cmd.Parameters.AddWithValue("@D_Time", row.Cells[11].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper());
                        cmd.Parameters.AddWithValue("@D_DateTimeStamp", "T" + row.Cells[11].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper() + ":00.000" + row.Cells[12].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper());
                        cmd.Parameters.AddWithValue("@A_AirportCode", row.Cells[13].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper());
                        cmd.Parameters.AddWithValue("@A_AirportName", GetValueUsingCode(row.Cells[13].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper()).Split(':')[0]);
                        cmd.Parameters.AddWithValue("@A_AirportCityCode", row.Cells[14].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper());
                        cmd.Parameters.AddWithValue("@A_AirportCityName", GetValueUsingCode(row.Cells[14].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper()).Split(':')[0]);
                        cmd.Parameters.AddWithValue("@A_AirportCountryCode", row.Cells[15].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper());
                        cmd.Parameters.AddWithValue("@A_AirportCountryName", GetValueUsingCode(row.Cells[13].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper()).Split(',')[1]);
                        cmd.Parameters.AddWithValue("@A_Terminal", row.Cells[16].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper());
                        cmd.Parameters.AddWithValue("@A_Time", row.Cells[17].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper());
                        cmd.Parameters.AddWithValue("@A_DateTimeStamp", "T" + row.Cells[17].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper() + ":00.000" + row.Cells[18].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper());
                        cmd.Parameters.AddWithValue("@EquipType", row.Cells[19].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper());
                        cmd.Parameters.AddWithValue("@IsNextDayCount", row.Cells[20].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper());
                        cmd.Parameters.AddWithValue("@IsReturn", row.Cells[21].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper());
                        cmd.Parameters.AddWithValue("@BaggageInfo", row.Cells[22].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper() + " Pcs included");
                        cmd.Parameters.AddWithValue("@BI_Pieces", row.Cells[22].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper());
                        cmd.Parameters.AddWithValue("@BI_Price", "0");
                        if (row.Cells[22].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper() == "0")
                        {
                            cmd.Parameters.AddWithValue("@BI_Description", "No check-in baggage included in the fare");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@BI_Description", row.Cells[22].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper() + " Pc check-in baggage included in the fare");
                        }

                        if (row.Cells[21].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper().ToLower() == "true")
                        {
                            cmd.Parameters.AddWithValue("@Sector_Group", "1");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Sector_Group", "0");
                        }
                        cmd.Parameters.AddWithValue("@OperatingCarrierCode", row.Cells[29].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper());
                        cmd.Parameters.AddWithValue("@OperatingCarrierName", GetAirlineData(row.Cells[29].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper()));

                        //new code
                        cmd.Parameters.AddWithValue("@ElapsedTime", "07:01");
                        cmd.Parameters.AddWithValue("@ActualTime", "15:21");
                        cmd.Parameters.AddWithValue("@TechStopOver", 0);
                        cmd.Parameters.AddWithValue("@TransitTime", "NA");
                        cmd.Parameters.AddWithValue("@Sector_Key", Guid.NewGuid().ToString());
                        cmd.Parameters.AddWithValue("@Distance", 00);
                        cmd.Parameters.AddWithValue("@ETicket", 1);
                        cmd.Parameters.AddWithValue("@ChangeOfPlane", 0);
                        cmd.Parameters.AddWithValue("@ParticipantLevel", "Secure Sell");
                        cmd.Parameters.AddWithValue("@OptionalServicesIndicator", "True");
                        cmd.Parameters.AddWithValue("@AvailabilitySource", "RT");


                        cmd.Parameters.AddWithValue("@BookingCodeInfo", "NA");
                        cmd.Parameters.AddWithValue("@FareBasisCode", "NA");
                        cmd.Parameters.AddWithValue("@ItinKey", Convert.ToString(Guid.NewGuid()));
                        cmd.Parameters.AddWithValue("@SupCode", "NA");
                        //BookingCodeInfo@BookingCodeInfo,FareBasisCode=@FareBasisCode,ItinKey=@ItinKey,SupCode=@SupCode
                        //end code

                        cmd.Parameters.AddWithValue("@Id", gvDetails2.DataKeys[row.RowIndex].Value);
                        cmd.Connection = sqlcon;
                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            if (CheckIfExist(row.Cells[3].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper()))
                            {
                                bool ReturnResult = UpdateSingleRowBase(row.Cells[3].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper(),
                                     row.Cells[23].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper(),
                                     row.Cells[24].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper(),
                                     row.Cells[1].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper(),
                                     row.Cells[28].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper(),
                                     row.Cells[25].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper(),
                                     row.Cells[26].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper(),
                                     row.Cells[2].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper(),


                                     row.Cells[30].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper(),
                                     row.Cells[31].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper(),


                                     row.Cells[27].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper(),
                                     Convert.ToBoolean(row.Cells[32].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim()),
                                     Convert.ToString(row.Cells[33].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim())
                                    );
                                if (ReturnResult)
                                {
                                    lblMsg.Text = "Rows Updated successfully";
                                }
                                else
                                {
                                    lblMsg.Text = "Rows Not Updated";
                                }
                            }
                            else
                            {
                                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;

                                InsertSingleRowBase(row.Cells[3].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper(),
                                    row.Cells[23].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper(),
                                     row.Cells[24].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper(),
                                     row.Cells[33].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper(),
                                    "AgencyPrivateFare",
                                    "1R",
                                    row.Cells[1].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper(),
                                    "3QP",
                                    row.Cells[28].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper(),
                                    "MT", row.Cells[25].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper(),
                                     row.Cells[26].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper(),
                                    row.Cells[2].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper(),

                                     row.Cells[30].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper(),
                                     row.Cells[31].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper(),

                                    row.Cells[27].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper(),
                                    objUserDetail.userID,
                                    "false");
                            }

                        }

                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        sqlcon.Close();
                    }
                }
            }
        }
        btnUpdate.Visible = false;

        Bind_Itin_details();
        // BindList(dt);

    }

    protected void gvDetails2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("AddNew"))
        {
            SqlConnection sqlcon = DataConnection.GetConnectionCustomItinerary();

            try
            {
                sqlcon.Open();
                UserDetail objUserDetail = HttpContext.Current.Session["UserDetails"] as UserDetail;
                TextBox txtftr_AirlineId = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_AirlineId");
                TextBox txtftr_PCC = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_PCC");
                TextBox txtftr_Provider = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_Provider");
                TextBox txtftr_OfferType = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_OfferType");
                TextBox txtftr_ACC = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_ACC");
                TextBox txtftr_UniqueId = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_UniqueId");
                TextBox txtftr_SerialId = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_SerialId");

                TextBox txtftr_AirV = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_AirV");
                TextBox txtftr_AirlineName = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_AirlineName");
                TextBox txtftr_Class = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_Class");
                TextBox txtftr_CabinClassCode = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_CabinClassCode");
                TextBox txtftr_CabinClassName = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_CabinClassName");
                TextBox txtftr_NoSeats = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_NoSeats");
                TextBox txtftr_FltNum = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_FltNum");

                TextBox txtftr_D_AirportCode = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_D_AirportCode");
                TextBox txtftr_D_AirportCityCode = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_D_AirportCityCode");
                TextBox txtftr_D_AirportCountryCode = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_D_AirportCountryCode");
                TextBox txtftr_D_Terminal = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_D_Terminal");
                TextBox txtftr_D_Time = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_D_Time");
                TextBox txtftr_D_DateTimeStamp = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_D_DateTimeStamp");
                TextBox txtftr_A_AirportCode = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_A_AirportCode");
                TextBox txtftr_A_AirportCityCode = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_A_AirportCityCode");
                TextBox txtftr_A_AirportCountryCode = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_A_AirportCountryCode");
                TextBox txtftr_A_Terminal = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_A_Terminal");
                TextBox txtftr_A_Time = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_A_Time");
                TextBox txtftr_A_DateTimeStamp = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_A_DateTimeStamp");
                TextBox txtftr_EquipType = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_EquipType");
                TextBox txtftr_ElapsedTime = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_ElapsedTime");
                TextBox txtftr_ActualTime = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_ActualTime");
                TextBox txtftr_TechStopOver = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_TechStopOver");

                TextBox txtftr_IsReturn = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_IsReturn");
                TextBox txtftr_BaggageInfo = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_BaggageInfo");

                TextBox txtftr_TransitTime = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_TransitTime");
                TextBox txtftr_Sector_Key = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_Sector_Key");
                TextBox txtftr_Distance = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_Distance");


                TextBox txtftr_Pax_A_BaseFare = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_Pax_A_BaseFare");
                TextBox txtftr_Pax_A_Taxes = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_Pax_A_Taxes");
                TextBox txtftr_Origin = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_Origin");
                TextBox txtftr_Destination = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_Destination");
                TextBox txtftr_JourneyType = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_JourneyType");

                TextBox txtftr_Company_ID = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_Company_ID");
                TextBox txtftr_Camp_ID = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_Camp_ID");
                TextBox txtftr_IsNextDayCount = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_IsNextDayCount");
                TextBox txtftr_OptCode = (TextBox)gvDetails2.FooterRow.FindControl("txtftr_OptCode");


                //DropDownList ddlftr_status = (DropDownList)gvDetails2.FooterRow.FindControl("ddlftr_status");
                //DropDownList ddlftr_payment = (DropDownList)gvDetails2.FooterRow.FindControl("ddlftr_payment");



                if (CheckIfExist(txtftr_UniqueId.Text.Trim().ToUpper()) == false)
                {
                    InsertSingleRowBase(txtftr_UniqueId.Text.Trim().ToUpper(), txtftr_Pax_A_BaseFare.Text.Trim().ToUpper(), txtftr_Pax_A_Taxes.Text.Trim().ToUpper(), "GBP", "AgencyPrivateFare", "1R", txtftr_AirlineId.Text.Trim().ToUpper(), "3QP", txtftr_OfferType.Text.Trim().ToUpper(), "MT", txtftr_Origin.Text.Trim().ToUpper(), txtftr_Destination.Text.Trim().ToUpper(), txtftr_JourneyType.Text, "01/01/2019", "01/01/2050", txtftr_Camp_ID.Text.Trim().ToUpper(), objUserDetail.userID, "false");
                }

                StringBuilder str = new StringBuilder("insert into CustomItinUS (UniqueId, SerialId, IsConnect, AirV, AirlineName, Class, CabinClassCode, CabinClassName, NoSeats, FltNum, D_AirportCode, D_AirportName, D_AirportCityCode, D_AirportCityName, D_AirportCountryCode,");
                str.Append("D_AirportCountryName, D_Terminal, D_Time, D_DateTimeStamp, A_AirportCode, A_AirportName, A_AirportCityCode, A_AirportCityName, A_AirportCountryCode, A_AirportCountryName, A_Terminal, IsNextDayCount, A_Time,");
                str.Append("A_DateTimeStamp, EquipType, ElapsedTime, ActualTime, TechStopOver, IsReturn, BaggageInfo, BI_Pieces, BI_Price, BI_Description, TransitTime, Sector_Key, Distance, ETicket, ChangeOfPlane, ParticipantLevel,");
                str.Append("OptionalServicesIndicator, AvailabilitySource, Sector_Group, BookingCodeInfo, FareBasisCode, ItinKey, SupCode,OperatingCarrierCode,OperatingCarrierName) ");

                str.Append(" values (@UniqueId, @SerialId, @IsConnect, @AirV, @AirlineName, @Class, @CabinClassCode, @CabinClassName, @NoSeats, @FltNum, @D_AirportCode, @D_AirportName, @D_AirportCityCode, @D_AirportCityName, @D_AirportCountryCode,");
                str.Append(" @D_AirportCountryName, @D_Terminal, @D_Time, @D_DateTimeStamp, @A_AirportCode, @A_AirportName, @A_AirportCityCode, @A_AirportCityName, @A_AirportCountryCode, @A_AirportCountryName, @A_Terminal, @IsNextDayCount, @A_Time,");
                str.Append(" @A_DateTimeStamp, @EquipType, @ElapsedTime, @ActualTime, @TechStopOver, @IsReturn, @BaggageInfo, @BI_Pieces, @BI_Price, @BI_Description, @TransitTime, @Sector_Key, @Distance, @ETicket, @ChangeOfPlane, @ParticipantLevel,");
                str.Append(" @OptionalServicesIndicator, @AvailabilitySource, @Sector_Group, @BookingCodeInfo, @FareBasisCode, @ItinKey, @SupCode,@OperatingCarrierCode,@OperatingCarrierName) ");

                SqlCommand cmd = new SqlCommand(str.ToString())
                {
                    Connection = sqlcon
                };

                cmd.Parameters.AddWithValue("@UniqueId", Convert.ToInt32(txtftr_UniqueId.Text.Trim().ToUpper()));
                cmd.Parameters.AddWithValue("@SerialId", Convert.ToInt32(txtftr_SerialId.Text.Trim().ToUpper()));
                cmd.Parameters.AddWithValue("@IsConnect", 0);
                cmd.Parameters.AddWithValue("@AirV", txtftr_AirV.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@AirlineName", GetAirlineData(txtftr_AirV.Text.Trim().ToUpper()));
                cmd.Parameters.AddWithValue("@Class", "Y");
                cmd.Parameters.AddWithValue("@CabinClassCode", "Y");
                cmd.Parameters.AddWithValue("@CabinClassName", "Economy");
                cmd.Parameters.AddWithValue("@NoSeats", 5);
                cmd.Parameters.AddWithValue("@FltNum", txtftr_FltNum.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@D_AirportCode", txtftr_D_AirportCode.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@D_AirportName", GetValueUsingCode(txtftr_D_AirportCode.Text.Trim().ToUpper()).Split(':')[0]);
                cmd.Parameters.AddWithValue("@D_AirportCityCode", txtftr_D_AirportCityCode.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@D_AirportCityName", GetValueUsingCode(txtftr_D_AirportCityCode.Text.Trim().ToUpper()).Split(':')[0]);
                cmd.Parameters.AddWithValue("@D_AirportCountryCode", txtftr_D_AirportCountryCode.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@D_AirportCountryName", GetValueUsingCode(txtftr_D_AirportCode.Text.Trim().ToUpper()).Split(',')[1]);
                cmd.Parameters.AddWithValue("@D_Terminal", txtftr_D_Terminal.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@D_Time", txtftr_D_Time.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@D_DateTimeStamp", "T" + txtftr_D_Time.Text.Trim().ToUpper() + ":00.000" + txtftr_D_DateTimeStamp.Text);
                cmd.Parameters.AddWithValue("@A_AirportCode", txtftr_A_AirportCode.Text);
                cmd.Parameters.AddWithValue("@A_AirportName", GetValueUsingCode(txtftr_A_AirportCode.Text.Trim().ToUpper()).Split(':')[0]);
                cmd.Parameters.AddWithValue("@A_AirportCityCode", txtftr_A_AirportCityCode.Text);
                cmd.Parameters.AddWithValue("@A_AirportCityName", GetValueUsingCode(txtftr_A_AirportCityCode.Text.Trim().ToUpper()).Split(':')[0]);
                cmd.Parameters.AddWithValue("@A_AirportCountryCode", txtftr_A_AirportCountryCode.Text);
                cmd.Parameters.AddWithValue("@A_AirportCountryName", GetValueUsingCode(txtftr_A_AirportCode.Text.Trim().ToUpper()).Split(',')[1]);
                cmd.Parameters.AddWithValue("@A_Terminal", txtftr_A_Terminal.Text);
                cmd.Parameters.AddWithValue("@IsNextDayCount", Convert.ToInt32(txtftr_IsNextDayCount.Text));
                cmd.Parameters.AddWithValue("@A_Time", txtftr_A_Time.Text);
                cmd.Parameters.AddWithValue("@A_DateTimeStamp", "T" + txtftr_A_Time.Text.Trim().ToUpper() + ":00.000" + txtftr_A_DateTimeStamp.Text);
                cmd.Parameters.AddWithValue("@EquipType", txtftr_EquipType.Text);
                cmd.Parameters.AddWithValue("@ElapsedTime", "07:01");
                cmd.Parameters.AddWithValue("@ActualTime", "15:21");
                cmd.Parameters.AddWithValue("@TechStopOver", 0);
                cmd.Parameters.AddWithValue("@IsReturn", Convert.ToBoolean(txtftr_IsReturn.Text));
                cmd.Parameters.AddWithValue("@BaggageInfo", txtftr_BaggageInfo.Text.Trim().ToUpper() + " Pcs included");
                cmd.Parameters.AddWithValue("@BI_Pieces", txtftr_BaggageInfo.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@BI_Price", "0");
                cmd.Parameters.AddWithValue("@BI_Description", txtftr_BaggageInfo.Text.Trim().ToUpper() == "0" ? "No check-in baggage included in the fare" : txtftr_BaggageInfo.Text.Trim().ToUpper() + " Pc check-in baggage included in the fare");
                cmd.Parameters.AddWithValue("@TransitTime", "NA");
                cmd.Parameters.AddWithValue("@Sector_Key", Guid.NewGuid().ToString());
                cmd.Parameters.AddWithValue("@Distance", 00);
                cmd.Parameters.AddWithValue("@ETicket", 1);
                cmd.Parameters.AddWithValue("@ChangeOfPlane", 0);
                cmd.Parameters.AddWithValue("@ParticipantLevel", "Secure Sell");
                cmd.Parameters.AddWithValue("@OptionalServicesIndicator", 0);
                cmd.Parameters.AddWithValue("@AvailabilitySource", "");
                cmd.Parameters.AddWithValue("@Sector_Group", txtftr_IsReturn.Text.Trim().ToUpper().ToLower() == "true" ? 1 : 0);
                cmd.Parameters.AddWithValue("@OperatingCarrierCode", txtftr_OptCode.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@OperatingCarrierName", txtftr_OptCode.Text.Trim().ToUpper() != "" ? GetAirlineData(txtftr_OptCode.Text.Trim().ToUpper()) : "");
                cmd.Parameters.AddWithValue("@BookingCodeInfo", "");
                cmd.Parameters.AddWithValue("@FareBasisCode", "");
                cmd.Parameters.AddWithValue("@ItinKey", Convert.ToString(Guid.NewGuid()));
                cmd.Parameters.AddWithValue("@SupCode", "");


                cmd.ExecuteNonQuery();

            }

            catch (Exception ex)
            {

            }
            finally
            {
                sqlcon.Close();

            }

            Bind_Itin_details();



        }

    }

    protected void copy_row(object sender, EventArgs e)
    {
        SqlConnection sqlcon = DataConnection.GetConnectionCustomItinerary();

        sqlcon.Open();

        try
        {
            LinkButton button = sender as LinkButton;
            string id = button.ToolTip;

            StringBuilder str = new StringBuilder("insert into CustomItinUS (UniqueId, SerialId, IsConnect, AirV, AirlineName, Class, CabinClassCode, CabinClassName, NoSeats, FltNum, D_AirportCode, D_AirportName, D_AirportCityCode, D_AirportCityName, D_AirportCountryCode, D_AirportCountryName, D_Terminal, D_Time,");
            str = str.Append(" D_DateTimeStamp, A_AirportCode, A_AirportName, A_AirportCityCode, A_AirportCityName, A_AirportCountryCode, A_AirportCountryName, A_Terminal, IsNextDayCount, A_Time, A_DateTimeStamp, EquipType, ElapsedTime, ActualTime, TechStopOver, IsReturn, BaggageInfo,");
            str = str.Append(" BI_Pieces, BI_Price, BI_Description, TransitTime, Sector_Key, Distance, ETicket, ChangeOfPlane, ParticipantLevel, OptionalServicesIndicator, AvailabilitySource, Sector_Group, BookingCodeInfo, FareBasisCode, ItinKey,SupCode,OperatingCarrierCode,OperatingCarrierName)");

            str = str.Append(" select UniqueId, SerialId, IsConnect, AirV, AirlineName, Class, CabinClassCode, CabinClassName, NoSeats, FltNum, D_AirportCode, D_AirportName, D_AirportCityCode, D_AirportCityName, D_AirportCountryCode, D_AirportCountryName, D_Terminal, D_Time,");
            str = str.Append(" D_DateTimeStamp, A_AirportCode, A_AirportName, A_AirportCityCode, A_AirportCityName, A_AirportCountryCode, A_AirportCountryName, A_Terminal, IsNextDayCount, A_Time, A_DateTimeStamp, EquipType, ElapsedTime, ActualTime, TechStopOver, IsReturn, BaggageInfo,");
            str = str.Append(" BI_Pieces, BI_Price, BI_Description, TransitTime, Sector_Key, Distance, ETicket, ChangeOfPlane, ParticipantLevel, OptionalServicesIndicator, AvailabilitySource, Sector_Group, BookingCodeInfo, FareBasisCode, ItinKey,SupCode,OperatingCarrierCode,OperatingCarrierName");
            str = str.Append(" from CustomItinUS where id='" + id + "'");
            SqlCommand cmd = new SqlCommand(str.ToString(), sqlcon);
            int result = cmd.ExecuteNonQuery();

            if (result == 1)
            {
                Bind_Itin_details();
                lblMsg.Visible = true;
                lblMsg.ForeColor = Color.Green;
                lblMsg.Text = "A row inserted successfully";
            }
            else
            {
                lblMsg.Visible = true;
                lblMsg.ForeColor = Color.Red;
                lblMsg.Text = "Copy failed";
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
            sqlcon.Close();
        }

    }

    protected void gvDetails_RowBound(object sender, GridViewRowEventArgs e)
    {

        DataRowView drv = e.Row.DataItem as DataRowView;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lbl_BaggageInfo = (Label)e.Row.FindControl("lbl_BaggageInfo");
            lbl_BaggageInfo.Text = lbl_BaggageInfo.Text.Substring(0, lbl_BaggageInfo.Text.IndexOf(" "));

            TextBox txt_BaggageInfo = (TextBox)e.Row.FindControl("txt_BaggageInfo");
            txt_BaggageInfo.Text = lbl_BaggageInfo.Text;


            Label lbl_D_DateTimeStamp = (Label)e.Row.FindControl("lbl_D_DateTimeStamp");
            lbl_D_DateTimeStamp.Text = lbl_D_DateTimeStamp.Text.Substring(13);

            TextBox txt_D_DateTimeStamp = (TextBox)e.Row.FindControl("txt_D_DateTimeStamp");
            txt_D_DateTimeStamp.Text = lbl_D_DateTimeStamp.Text;

            Label lbl_A_DateTimeStamp = (Label)e.Row.FindControl("lbl_A_DateTimeStamp");
            lbl_A_DateTimeStamp.Text = lbl_A_DateTimeStamp.Text.Substring(13);

            TextBox txt_A_DateTimeStamp = (TextBox)e.Row.FindControl("txt_A_DateTimeStamp");
            txt_A_DateTimeStamp.Text = lbl_A_DateTimeStamp.Text;


            Label lbl_Camp_ID = (Label)e.Row.FindControl("lbl_Camp_ID");
            if (lbl_Camp_ID.Text.Length > 7)
            {
                lbl_Camp_ID.Text = "<marquee direction='left' scrollamount='1' behavior='alternate'>" + lbl_Camp_ID.Text + "</marquee>";
            }
            else
            {
                lbl_Camp_ID.Text = lbl_Camp_ID.Text;
            }


        }


    }

    protected bool InsertSingleRowBase(string UniqueId, string Pax_A_BaseFare, string Pax_A_Taxes, string Currency, string FareType, string Provider, string ValCarrier, string PCC, string OfferType, string AccCode, string Origin, string Destination, string JourneyType, string ValidFrom, string ValidTo, string Camp_ID, string ModifiedBy, string Status)
    {
        bool ReturnStatus = false;
        SqlConnection sqlcon = DataConnection.GetConnectionCustomItinerary();

        try
        {
            sqlcon.Open();
            SqlCommand cmd = new SqlCommand("insert into CustomItinBaseUS ( UniqueId,Pax_A_BaseFare, Pax_A_Taxes, Currency, FareType, Provider, ValCarrier, PCC, OfferType, AccCode, Origin, Destination, JourneyType, ValidFrom, ValidTo, Company_ID, Camp_ID, ModifiedBy, LastModifiedDate, Status) values (@UniqueId, @Pax_A_BaseFare, @Pax_A_Taxes, @Currency, @FareType, @Provider, @ValCarrier, @PCC, @OfferType, @AccCode, @Origin, @Destination, @JourneyType, @ValidFrom, @ValidTo, @Company_ID, @Camp_ID, @ModifiedBy, @LastModifiedDate, @Status)");
            cmd.Parameters.AddWithValue("@UniqueId", UniqueId);
            cmd.Parameters.AddWithValue("@Pax_A_BaseFare", Pax_A_BaseFare);
            cmd.Parameters.AddWithValue("@Pax_A_Taxes", Pax_A_Taxes);
            cmd.Parameters.AddWithValue("@Currency", Currency);
            cmd.Parameters.AddWithValue("@FareType", FareType);
            cmd.Parameters.AddWithValue("@Provider", Provider);
            cmd.Parameters.AddWithValue("@ValCarrier", ValCarrier);
            cmd.Parameters.AddWithValue("@PCC", PCC);
            cmd.Parameters.AddWithValue("@OfferType", OfferType);
            cmd.Parameters.AddWithValue("@AccCode", AccCode);
            cmd.Parameters.AddWithValue("@Origin", Origin);
            cmd.Parameters.AddWithValue("@Destination", Destination);
            cmd.Parameters.AddWithValue("@JourneyType", JourneyType);
            cmd.Parameters.AddWithValue("@ValidFrom", ValidFrom);
            cmd.Parameters.AddWithValue("@ValidTo", ValidTo);
            cmd.Parameters.AddWithValue("@Company_ID", "NA");
            cmd.Parameters.AddWithValue("@Camp_ID", Camp_ID);
            cmd.Parameters.AddWithValue("@ModifiedBy", ModifiedBy);
            cmd.Parameters.AddWithValue("@LastModifiedDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@Status", Status);
            cmd.Connection = sqlcon;
            int i = cmd.ExecuteNonQuery();
            ReturnStatus = true;
        }
        catch (Exception ex)
        {
            ReturnStatus = false;
        }
        finally
        {
            sqlcon.Close();

        }
        return ReturnStatus;
    }

    protected bool UpdateSingleRowBase(string UniqueId, string Pax_A_BaseFare, string Pax_A_Taxes, string ValCarrier, string OfferType, string Origin, string Destination, string JourneyType, string ValidFrom, string ValidTo, string Camp_ID, bool Status, string Currency)
    {

        bool ReturnStatus = false;
        SqlConnection sqlcon = DataConnection.GetConnectionCustomItinerary();

        try
        {
            sqlcon.Open();

            var datefrom = ValidFrom.Trim().ToUpper().Substring(0, 10);
            //DateTime s = datefrom.ToString("d/M/yyyy");
            DateTime asDate1 = DateTime.ParseExact(datefrom, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            datefrom = asDate1.ToString("MM/dd/yyyy");

            var datetill = ValidTo.Trim().ToUpper().Substring(0, 10);
            DateTime asDate2 = DateTime.ParseExact(datetill, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            datetill = asDate2.ToString("MM/dd/yyyy");


            UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
            SqlCommand cmd = new SqlCommand("Update CustomItinBaseUS set  Pax_A_BaseFare=@Pax_A_BaseFare, Pax_A_Taxes=@Pax_A_Taxes, ValCarrier=@ValCarrier,OfferType=@OfferType,  Origin=@Origin, Destination=@Destination, JourneyType=@JourneyType, ValidFrom=@ValidFrom, ValidTo=@ValidTo, Company_ID=@Company_ID, Camp_ID=@Camp_ID, ModifiedBy=@ModifiedBy, LastModifiedDate=@LastModifiedDate,Status=@Status,Currency=@Currency where Uniqueid=@Uniqueid");
            cmd.Parameters.AddWithValue("@UniqueId", UniqueId);
            cmd.Parameters.AddWithValue("@Pax_A_BaseFare", Pax_A_BaseFare);
            cmd.Parameters.AddWithValue("@Pax_A_Taxes", Pax_A_Taxes);
            cmd.Parameters.AddWithValue("@ValCarrier", ValCarrier);
            cmd.Parameters.AddWithValue("@offertype", OfferType);
            cmd.Parameters.AddWithValue("@Origin", Origin);
            cmd.Parameters.AddWithValue("@Destination", Destination);
            cmd.Parameters.AddWithValue("@JourneyType", JourneyType);
            cmd.Parameters.AddWithValue("@ValidFrom", datefrom);
            cmd.Parameters.AddWithValue("@ValidTo", datetill);
            cmd.Parameters.AddWithValue("@Company_ID", "NA");
            cmd.Parameters.AddWithValue("@Camp_ID", Camp_ID);
            cmd.Parameters.AddWithValue("@ModifiedBy", objUserDetail.userID);
            cmd.Parameters.AddWithValue("@LastModifiedDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@Status", Status);
            cmd.Parameters.AddWithValue("@Currency", Currency);
            cmd.Connection = sqlcon;
            int i = cmd.ExecuteNonQuery();
            ReturnStatus = true;
        }
        catch (Exception ex)
        {
            ReturnStatus = false;
        }
        finally
        {
            sqlcon.Close();

        }
        return ReturnStatus;

    }

    protected void gvDetails2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvDetails2.EditIndex = -1;
        Bind_Itin_details();

    }

    protected void gvDetails2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SqlConnection sqlcon = DataConnection.GetConnectionCustomItinerary();
        try
        {
            sqlcon.Open();
            int id = Convert.ToInt32(gvDetails2.DataKeys[e.RowIndex].Value);
            int result = 0;

            var uniqueid = gvDetails2.Rows[e.RowIndex].Cells[3].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim().ToUpper();

            SqlCommand cmd = new SqlCommand("select * from CustomItinUS where uniqueid=" + uniqueid, sqlcon);

            DataTable dt = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);

            adp.Fill(dt);

            if (dt.Rows.Count > 1)
            {
                SqlCommand cmd2 = new SqlCommand("delete from CustomItinUS where id=" + id, sqlcon);
                result = cmd2.ExecuteNonQuery();
            }
            else if (dt.Rows.Count == 1)
            {
                SqlCommand cmd2 = new SqlCommand("delete from CustomItinUS where id=" + id, sqlcon);
                result = cmd2.ExecuteNonQuery();

                SqlCommand cmd3 = new SqlCommand("delete from CustomItinBaseUS where uniqueid=" + uniqueid, sqlcon);
                result = cmd3.ExecuteNonQuery();
            }


            //int result = cmd.ExecuteNonQuery();

            if (result == 1)
            {

                lblMsg.Visible = true;
                lblMsg.ForeColor = Color.Green;
                lblMsg.Text = "A row deleted successfully";

            }
            else
            {
                lblMsg.Visible = true;
                lblMsg.ForeColor = Color.Red;
                lblMsg.Text = "Deletion failed";
            }
        }
        catch
        {

        }
        finally
        {
            sqlcon.Close();
        }
        Bind_Itin_details();

    }

    protected bool CheckIfExist(string UniqueId)
    {
        DataTable dt = new DataTable();
        SqlConnection sqlcon = DataConnection.GetConnectionCustomItinerary();
        bool returnstatus = false;
        try
        {
            sqlcon.Open();
            SqlCommand cmd = new SqlCommand();
            StringBuilder strcmd = new StringBuilder("SELECT * from  dbo.CustomItinBaseUS WHERE Uniqueid=" + UniqueId);

            cmd.Connection = sqlcon;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strcmd.ToString();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);

            adp.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                returnstatus = true;

            }
            else
            {
                returnstatus = false;
            }

        }
        catch (Exception Ex)
        {

        }
        finally
        {
            sqlcon.Close();
            //checkUseStatus();
        }
        return returnstatus;

    }

    protected void OnCheckedChanged(object sender, EventArgs e)
    {
        bool isUpdateVisible = false;
        CheckBox chk = (sender as CheckBox);
        if (chk.ID == "chkAll")
        {
            foreach (GridViewRow row in gvDetails2.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked = chk.Checked;
                }
            }
        }
        CheckBox chkAll = (gvDetails2.HeaderRow.FindControl("chkAll") as CheckBox);
        chkAll.Checked = true;
        foreach (GridViewRow row in gvDetails2.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                bool isChecked = row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                for (int i = 1; i < row.Cells.Count; i++)
                {


                    if (row.Cells[i].Controls.OfType<Label>().ToList().Count > 0)
                    {
                        row.Cells[i].Controls.OfType<Label>().FirstOrDefault().Visible = !isChecked;
                    }
                    if (row.Cells[i].Controls.OfType<TextBox>().ToList().Count > 0)
                    {
                        row.Cells[i].Controls.OfType<TextBox>().FirstOrDefault().Visible = isChecked;
                    }
                    if (row.Cells[i].Controls.OfType<DropDownList>().ToList().Count > 0)
                    {
                        row.Cells[i].Controls.OfType<DropDownList>().FirstOrDefault().Visible = isChecked;
                    }
                    if (row.Cells[i].Controls.OfType<CheckBoxList>().ToList().Count > 0)
                    {
                        row.Cells[i].Controls.OfType<CheckBoxList>().FirstOrDefault().Enabled = isChecked;
                    }
                    if (isChecked && !isUpdateVisible)
                    {
                        isUpdateVisible = true;
                    }
                    if (!isChecked)
                    {
                        chkAll.Checked = false;
                    }
                }
            }
        }
        ImageButton btnsave = (gvDetails2.FooterRow.FindControl("imgbtnAdd") as ImageButton);
        btnsave.Visible = !isUpdateVisible;
        btnUpdate.Visible = isUpdateVisible;
        gvDetails2.FooterRow.Visible = !isUpdateVisible;
        // Bind_psg_details();

    }

    [WebMethod]
    public static List<string> GetAutoCompleteData(string prefix)
    {

        List<string> result = new List<string>();
        DataTable dt = FlightsBL.AutoCompleteAirport(prefix);
        if (dt != null)
        {
            if (dt.Rows.Count <= 0)
                result.Add("Record not found");
            else
            {

                foreach (DataRow dr in dt.Rows)
                    result.Add(dr["City_Name"].ToString() + "-" + dr["Airport_Name"].ToString() + ":" + " [" + dr["Airport_Code"] + "]" + ", " + dr["Country_Name"]);
                //London-Heathrow Airport:[LHR], United Kingdom
                //result.Add( dr["Airport_Name"].ToString());
            }
        }
        return result;


    }

    public string GetAirlineData(string prefix)
    {

        string result = "";

        DataTable dt = new DataTable();
        SqlConnection sqlcon = DataConnection.GetConnection();

        try
        {
            sqlcon.Open();
            SqlCommand cmd = new SqlCommand();
            StringBuilder strcmd = new StringBuilder("SELECT * from  Airline_Detail WHERE Airline_Code='" + prefix + "'");

            cmd.Connection = sqlcon;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strcmd.ToString();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);

            adp.Fill(dt);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow dr in dt.Rows)
                        result = dr["Airline_Name"].ToString();

                }
            }

        }
        catch (Exception Ex)
        {

        }
        finally
        {
            sqlcon.Close();
            //checkUseStatus();
        }





        return result;


    }

    [WebMethod(EnableSession = true)]
    public static string UpdateUseStatusUS(string UseStatus, string from, string to, string airline, string journey, string campaign, string uniqueId)
    {
        try
        {
            GetSetDatabase objGetSetDatabase = new GetSetDatabase();
            return objGetSetDatabase.UpdateUseStatusOwnItineraryUS(Convert.ToBoolean(UseStatus), from, to, airline, journey, campaign, uniqueId);
        }
        catch
        {
            return "false";
        }
    }

    
    public void CheckStatusCustomItinerary()
    {

        SqlConnection sqlcon = DataConnection.GetConnectionCustomItinerary();
        sqlcon.Open();
        try
        {
            StringBuilder strcmd = new StringBuilder("select top 1 * from CustomItinBaseUS where status='True' and JourneyType = '" + ddlJourneyType.SelectedValue + "'");

            if (TxtFrom.Text != "")
            {
                strcmd.Append("and Origin ='" + TxtFrom.Text.ToUpper() + "'");
            }
            if (TxtTo.Text != "")
            {
                strcmd.Append("and Destination ='" + TxtTo.Text.ToUpper() + "'");
            }
            if (TxtAir.Text != "")
            {
                strcmd.Append("and ValCarrier ='" + TxtAir.Text.ToUpper() + "'");
            }
            if (ddlCampaign.SelectedIndex != 0)
            {
                strcmd.Append("and Camp_ID ='" + ddlCampaign.SelectedValue.ToUpper() + "'");
            }
            if (txtUniqueId.Text != "")
            {
                strcmd.Append("and UniqueId ='" + txtUniqueId.Text + "'");
            }

            SqlCommand command = new SqlCommand(strcmd.ToString(), sqlcon);

            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                checkboxisActive.Checked = true;
            }
            else
            {
                checkboxisActive.Checked = false;
            }


        }
        catch (Exception ex)
        {

        }
        finally
        {
            sqlcon.Close();
        }

    }
  

    protected string GetValueUsingCode(string Code)
    {
        var FinalString = "NA:[NA], NA";
        try
        {
            var result = GetAutoCompleteData(Code);
            foreach (var re in result)
            {
                if (re.Contains("[" + Code + "]"))
                {
                    FinalString = re;
                }
            }
        }
        catch(Exception ex)
        {

        } 
        finally
        {

        }
       
        return FinalString;
    }





    protected void uploadcsvbtn_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            GetAutoCompleteAirline();
            GetAutoCompleteAirport("");

            InitialInsertCustomItinBase();
            InitialInsertCustomItin();
            Bind_Itin_details();
        }
        else

        {
            lblMsg.Text = "Please select file!";
        }
    }

    #region Insert Data into Custom Itinerary Base


    private void InitialInsertCustomItinBase()
    {
        UserDetail objUserDetail = Session["UserDetails"] as UserDetail;

        DataTable tblcsvfor_CustomItinBaseUS = new DataTable();
        tblcsvfor_CustomItinBaseUS.Columns.Add("UniqueId");
        tblcsvfor_CustomItinBaseUS.Columns.Add("Pax_A_BaseFare");
        tblcsvfor_CustomItinBaseUS.Columns.Add("Pax_A_Taxes");
        tblcsvfor_CustomItinBaseUS.Columns.Add("Currency");
        tblcsvfor_CustomItinBaseUS.Columns.Add("FareType");
        tblcsvfor_CustomItinBaseUS.Columns.Add("Provider");
        tblcsvfor_CustomItinBaseUS.Columns.Add("ValCarrier");
        tblcsvfor_CustomItinBaseUS.Columns.Add("PCC");
        tblcsvfor_CustomItinBaseUS.Columns.Add("OfferType");
        tblcsvfor_CustomItinBaseUS.Columns.Add("AccCode");
        tblcsvfor_CustomItinBaseUS.Columns.Add("Origin");
        tblcsvfor_CustomItinBaseUS.Columns.Add("Destination");
        tblcsvfor_CustomItinBaseUS.Columns.Add("JourneyType");
        tblcsvfor_CustomItinBaseUS.Columns.Add("ValidFrom");
        tblcsvfor_CustomItinBaseUS.Columns.Add("ValidTo");
        tblcsvfor_CustomItinBaseUS.Columns.Add("Company_ID");
        tblcsvfor_CustomItinBaseUS.Columns.Add("Camp_ID");
        tblcsvfor_CustomItinBaseUS.Columns.Add("ModifiedBy");
        tblcsvfor_CustomItinBaseUS.Columns.Add("LastModifiedDate");
        tblcsvfor_CustomItinBaseUS.Columns.Add("Status");
        //getting full file path of Uploaded file   


        if (!Directory.Exists(Server.MapPath("~/CSVfiles/")))
        {
            Directory.CreateDirectory(Server.MapPath("~/CSVfiles/"));
        }

        string csvPath = Server.MapPath("~/CSVfiles/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
        FileUpload1.SaveAs(csvPath);

        //Reading All text  
        string ReadCSV = File.ReadAllText(csvPath);
        //spliting row after new line  
        int Rowcount = 0;
        foreach (string csvRow in ReadCSV.Split('\n'))
        {
            if (Rowcount > 0)
            {
                if (!string.IsNullOrEmpty(csvRow))
                {
                    //Adding each row into datatable  
                    tblcsvfor_CustomItinBaseUS.Rows.Add();
                    int count = 0;
                    string FileRec = string.Empty;
                    foreach (string value in csvRow.Split(','))
                    {
                        var rt = csvRow.Split(',');
                        FileRec = value.Trim();


                        if (count == 0)
                        {
                            tblcsvfor_CustomItinBaseUS.Rows[tblcsvfor_CustomItinBaseUS.Rows.Count - 1]["UniqueId"] = FileRec;
                        }
                        else if (count == 1)
                        {
                            tblcsvfor_CustomItinBaseUS.Rows[tblcsvfor_CustomItinBaseUS.Rows.Count - 1]["ValCarrier"] = FileRec;
                        }
                        else if (count == 2)
                        {
                            tblcsvfor_CustomItinBaseUS.Rows[tblcsvfor_CustomItinBaseUS.Rows.Count - 1]["JourneyType"] = FileRec;
                        }
                        else if (count == 3)
                        {
                            tblcsvfor_CustomItinBaseUS.Rows[tblcsvfor_CustomItinBaseUS.Rows.Count - 1]["Pax_A_BaseFare"] = FileRec;
                        }
                        else if (count == 4)
                        {
                            tblcsvfor_CustomItinBaseUS.Rows[tblcsvfor_CustomItinBaseUS.Rows.Count - 1]["Pax_A_Taxes"] = FileRec;
                        }
                        else if (count == 5)
                        {
                            tblcsvfor_CustomItinBaseUS.Rows[tblcsvfor_CustomItinBaseUS.Rows.Count - 1]["Origin"] = FileRec;
                        }
                        else if (count == 6)
                        {
                            tblcsvfor_CustomItinBaseUS.Rows[tblcsvfor_CustomItinBaseUS.Rows.Count - 1]["Destination"] = FileRec;
                        }
                        else if (count == 7)
                        {
                            tblcsvfor_CustomItinBaseUS.Rows[tblcsvfor_CustomItinBaseUS.Rows.Count - 1]["Camp_ID"] = FileRec;
                        }
                        else if (count == 8)
                        {
                            tblcsvfor_CustomItinBaseUS.Rows[tblcsvfor_CustomItinBaseUS.Rows.Count - 1]["ValidFrom"] = FileRec;
                        }
                        else if (count == 9)
                        {
                            tblcsvfor_CustomItinBaseUS.Rows[tblcsvfor_CustomItinBaseUS.Rows.Count - 1]["ValidTo"] = FileRec;
                        }
                        else if (count == 31)
                        {
                            tblcsvfor_CustomItinBaseUS.Rows[tblcsvfor_CustomItinBaseUS.Rows.Count - 1]["Currency"] = FileRec;
                        }


                        if (count == 0)
                        {
                            //tblcsvfor_CustomItinBaseUS.Rows[tblcsvfor_CustomItinBaseUS.Rows.Count - 1]["Currency"] = "GBP";
                            tblcsvfor_CustomItinBaseUS.Rows[tblcsvfor_CustomItinBaseUS.Rows.Count - 1]["FareType"] = "AgencyPrivateFare";
                            tblcsvfor_CustomItinBaseUS.Rows[tblcsvfor_CustomItinBaseUS.Rows.Count - 1]["Provider"] = "1R";
                            tblcsvfor_CustomItinBaseUS.Rows[tblcsvfor_CustomItinBaseUS.Rows.Count - 1]["PCC"] = "3QP";
                            tblcsvfor_CustomItinBaseUS.Rows[tblcsvfor_CustomItinBaseUS.Rows.Count - 1]["AccCode"] = "MT";
                            tblcsvfor_CustomItinBaseUS.Rows[tblcsvfor_CustomItinBaseUS.Rows.Count - 1]["Company_ID"] = "Demo";
                            tblcsvfor_CustomItinBaseUS.Rows[tblcsvfor_CustomItinBaseUS.Rows.Count - 1]["LastModifiedDate"] = DateTime.Now;
                            tblcsvfor_CustomItinBaseUS.Rows[tblcsvfor_CustomItinBaseUS.Rows.Count - 1]["OfferType"] = "CALL";
                            tblcsvfor_CustomItinBaseUS.Rows[tblcsvfor_CustomItinBaseUS.Rows.Count - 1]["Status"] = "TRUE";
                            tblcsvfor_CustomItinBaseUS.Rows[tblcsvfor_CustomItinBaseUS.Rows.Count - 1]["ModifiedBy"] = objUserDetail.userID.ToString();
                        }

                        count++;
                    }
                }
            }
            Rowcount++;

        }
        //Calling insert Functions  
        InsertCustomItinBase(GetDistinctRecords(tblcsvfor_CustomItinBaseUS));
    }

    public static DataTable GetDistinctRecords(DataTable dt)
    {
        string[] distColumns = { "UniqueId", "Pax_A_BaseFare", "Pax_A_Taxes", "Currency", "FareType", "Provider", "ValCarrier", "PCC", "OfferType", "AccCode", "Origin", "Destination", "JourneyType", "ValidFrom", "ValidTo", "Company_ID", "Camp_ID", "ModifiedBy", "LastModifiedDate", "Status" };
        DataTable dtUniqRecords = dt.DefaultView.ToTable(true, distColumns);
        return dtUniqRecords;
    }


    private void InsertCustomItinBase(DataTable csvdt)
    {
        SqlConnection sqlcon = DataConnection.GetConnectionCustomItinerary();
        sqlcon.Open();

        SqlCommand sqlCommand = new SqlCommand("Truncate table CustomItinBaseUS", sqlcon);
        sqlCommand.ExecuteNonQuery();
        try
        {
            SqlBulkCopy objbulk = new SqlBulkCopy(sqlcon)
            {
                //assigning Destination table name    
                DestinationTableName = "CustomItinBaseUS"
            };

            //Mapping Table column    
            objbulk.ColumnMappings.Add("UniqueId", "UniqueId");
            objbulk.ColumnMappings.Add("Pax_A_BaseFare", "Pax_A_BaseFare");
            objbulk.ColumnMappings.Add("Pax_A_Taxes", "Pax_A_Taxes");
            objbulk.ColumnMappings.Add("Currency", "Currency");
            objbulk.ColumnMappings.Add("FareType", "FareType");
            objbulk.ColumnMappings.Add("Provider", "Provider");
            objbulk.ColumnMappings.Add("ValCarrier", "ValCarrier");
            objbulk.ColumnMappings.Add("PCC", "PCC");
            objbulk.ColumnMappings.Add("OfferType", "OfferType");
            objbulk.ColumnMappings.Add("AccCode", "AccCode");
            objbulk.ColumnMappings.Add("Origin", "Origin");
            objbulk.ColumnMappings.Add("Destination", "Destination");
            objbulk.ColumnMappings.Add("JourneyType", "JourneyType");
            objbulk.ColumnMappings.Add("ValidFrom", "ValidFrom");
            objbulk.ColumnMappings.Add("ValidTo", "ValidTo");
            objbulk.ColumnMappings.Add("Company_ID", "Company_ID");
            objbulk.ColumnMappings.Add("Camp_ID", "Camp_ID");
            objbulk.ColumnMappings.Add("ModifiedBy", "ModifiedBy");
            objbulk.ColumnMappings.Add("LastModifiedDate", "LastModifiedDate");
            objbulk.ColumnMappings.Add("Status", "Status");
            //inserting Datatable Records to DataBase    

            objbulk.WriteToServer(csvdt);
        }
        catch (Exception ex)
        {

        }
        finally
        {
            sqlcon.Close();
        }
    }

    #endregion


    #region Insert data into Custom Itinerary


    private void InitialInsertCustomItin()
    {
        try
        {
            DataTable tblcsvfor_CustomItin = new DataTable();
            tblcsvfor_CustomItin.Columns.Add("UniqueId");
            tblcsvfor_CustomItin.Columns.Add("SerialId");
            tblcsvfor_CustomItin.Columns.Add("IsConnect");
            tblcsvfor_CustomItin.Columns.Add("AirV");
            tblcsvfor_CustomItin.Columns.Add("AirlineName");
            tblcsvfor_CustomItin.Columns.Add("Class");
            tblcsvfor_CustomItin.Columns.Add("CabinClassCode");
            tblcsvfor_CustomItin.Columns.Add("CabinClassName");
            tblcsvfor_CustomItin.Columns.Add("NoSeats");
            tblcsvfor_CustomItin.Columns.Add("FltNum");
            tblcsvfor_CustomItin.Columns.Add("D_AirportCode");
            tblcsvfor_CustomItin.Columns.Add("D_AirportName");
            tblcsvfor_CustomItin.Columns.Add("D_AirportCityCode");
            tblcsvfor_CustomItin.Columns.Add("D_AirportCityName");
            tblcsvfor_CustomItin.Columns.Add("D_AirportCountryCode");
            tblcsvfor_CustomItin.Columns.Add("D_AirportCountryName");
            tblcsvfor_CustomItin.Columns.Add("D_Terminal");
            tblcsvfor_CustomItin.Columns.Add("D_Time");
            tblcsvfor_CustomItin.Columns.Add("D_DateTimeStamp");
            tblcsvfor_CustomItin.Columns.Add("A_AirportCode");
            tblcsvfor_CustomItin.Columns.Add("A_AirportName");
            tblcsvfor_CustomItin.Columns.Add("A_AirportCityCode");
            tblcsvfor_CustomItin.Columns.Add("A_AirportCityName");
            tblcsvfor_CustomItin.Columns.Add("A_AirportCountryCode");
            tblcsvfor_CustomItin.Columns.Add("A_AirportCountryName");
            tblcsvfor_CustomItin.Columns.Add("A_Terminal");
            tblcsvfor_CustomItin.Columns.Add("IsNextDayCount");
            tblcsvfor_CustomItin.Columns.Add("A_Time");
            tblcsvfor_CustomItin.Columns.Add("A_DateTimeStamp");
            tblcsvfor_CustomItin.Columns.Add("EquipType");
            tblcsvfor_CustomItin.Columns.Add("ElapsedTime");
            tblcsvfor_CustomItin.Columns.Add("ActualTime");
            tblcsvfor_CustomItin.Columns.Add("TechStopOver");
            tblcsvfor_CustomItin.Columns.Add("IsReturn");
            tblcsvfor_CustomItin.Columns.Add("BaggageInfo");
            tblcsvfor_CustomItin.Columns.Add("BI_Pieces");
            tblcsvfor_CustomItin.Columns.Add("BI_Price");
            tblcsvfor_CustomItin.Columns.Add("BI_Description");
            tblcsvfor_CustomItin.Columns.Add("TransitTime");
            tblcsvfor_CustomItin.Columns.Add("Sector_Key");
            tblcsvfor_CustomItin.Columns.Add("Distance");
            tblcsvfor_CustomItin.Columns.Add("ETicket");
            tblcsvfor_CustomItin.Columns.Add("ChangeOfPlane");
            tblcsvfor_CustomItin.Columns.Add("ParticipantLevel");
            tblcsvfor_CustomItin.Columns.Add("OptionalServicesIndicator");
            tblcsvfor_CustomItin.Columns.Add("AvailabilitySource");
            tblcsvfor_CustomItin.Columns.Add("Sector_Group");
            tblcsvfor_CustomItin.Columns.Add("OperatingCarrierCode");
            tblcsvfor_CustomItin.Columns.Add("OperatingCarrierName");
            tblcsvfor_CustomItin.Columns.Add("BookingCodeInfo");
            tblcsvfor_CustomItin.Columns.Add("FareBasisCode");
            tblcsvfor_CustomItin.Columns.Add("ItinKey");
            tblcsvfor_CustomItin.Columns.Add("SupCode");


            if (!Directory.Exists(Server.MapPath("~/CSVfiles/")))
            {
                Directory.CreateDirectory(Server.MapPath("~/CSVfiles/"));
            }

            string csvPath = Server.MapPath("~/CSVfiles/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
            FileUpload1.SaveAs(csvPath);

            //Reading All text  
            string ReadCSV = File.ReadAllText(csvPath);
            //spliting row after new line  
            int Rowcount = 0;
            foreach (string csvRow in ReadCSV.Split('\n'))
            {
                if (Rowcount > 0)
                {
                    if (!string.IsNullOrEmpty(csvRow))
                    {
                        //Adding each row into datatable  
                        tblcsvfor_CustomItin.Rows.Add();
                        int count = 0;
                        string FileRec = string.Empty;
                        foreach (string value in csvRow.Split(','))
                        {
                            var rt = csvRow.Split(',');
                            FileRec = value.Trim();

                            if (count == 0)
                            {
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["UniqueId"] = FileRec;
                            }
                            else if (count == 10)
                            {
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["OperatingCarrierCode"] = FileRec;
                                string lenOperatingCode = string.Empty;
                                if (!string.IsNullOrEmpty(FileRec))
                                {
                                    if (FileRec.Length == 2)
                                    {
                                        lenOperatingCode = dictAirLine[FileRec.ToUpper()];
                                    }
                                }
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["OperatingCarrierName"] = lenOperatingCode;
                            }
                            else if (count == 11)
                            {
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["SerialId"] = FileRec;
                            }
                            else if (count == 12)
                            {
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["Airv"] = FileRec;
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["AirlineName"] = ((FileRec == "" || FileRec == null || FileRec == "TRUE") ? "" : dictAirLine[FileRec.ToUpper()]);
                            }
                            else if (count == 13)
                            {
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["FltNum"] = FileRec;
                            }

                            else if (count == 14)
                            {
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["D_AirportCode"] = FileRec;
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["D_AirportName"] =CheckAirportCode(FileRec.ToUpper(), "Airport_Name");  //GetValueUsingCode(FileRec).ToUpper().Split(':')[0];    //GetValueUsingCode(FileRec).ToUpper().Split(':')[0];
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["D_AirportCountryName"] = CheckAirportCode(FileRec.ToUpper(), "Country_Name");   //GetValueUsingCode(FileRec).ToUpper().Split(',')[1];
                            }

                            else if (count == 15)
                            {
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["D_AirportCityCode"] = FileRec;
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["D_AirportCityName"] = CheckAirportCode(FileRec.ToUpper(), "City_Name");  //GetValueUsingCode(FileRec).ToUpper().Split(':')[0];
                            }
                            else if (count == 16)
                            {
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["D_AirportCountryCode"] = FileRec;
                            }
                            else if (count == 17)
                            {
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["D_Terminal"] = FileRec;
                            }
                            else if (count == 18)
                            {
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["D_Time"] = FileRec;
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["D_DateTimeStamp"] = "T" + FileRec + ":00.000+01:00";
                            }
                            else if (count == 19)
                            {
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["A_AirportCode"] = FileRec;
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["A_AirportName"] = CheckAirportCode(FileRec.ToUpper(), "Airport_Name");  //GetValueUsingCode(FileRec).ToUpper().Split(':')[0];
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["A_AirportCountryName"] = CheckAirportCode(FileRec.ToUpper(), "Country_Name");   //GetValueUsingCode(FileRec).ToUpper().Split(',')[1];
                            }
                            else if (count == 20)
                            {
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["A_AirportCityCode"] = FileRec;
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["A_AirportCityName"] = CheckAirportCode(FileRec.ToUpper(), "City_Name");  //GetValueUsingCode(FileRec).ToUpper().Split(':')[0];
                            }
                            else if (count == 21)
                            {
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["A_AirportCountryCode"] = FileRec;
                            }
                            else if (count == 22)
                            {
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["A_Terminal"] = FileRec;
                            }
                            else if (count == 23)
                            {
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["IsNextDayCount"] = FileRec;
                            }
                            else if (count == 24)
                            {
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["A_Time"] = FileRec;
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["A_DateTimeStamp"] = "T" + FileRec + ":00.000+01:00";
                            }
                            else if (count == 25)
                            {
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["EquipType"] = FileRec;
                            }
                            else if (count == 26)
                            {
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["IsReturn"] = FileRec.ToLower() == "true" ? 1 : 0;
                            }
                            else if (count == 27)
                            {
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["BI_Pieces"] = FileRec;
                            }
                            else if (count == 28)
                            {
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["BI_Description"] = FileRec;
                            }
                            else if (count == 29)
                            {
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["Sector_Group"] = FileRec;
                            }
                            else if (count == 30)
                            {
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["BaggageInfo"] = FileRec;
                            }
                            //else if (count == 31)
                            //{
                            //    tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["Currency"] = FileRec;
                            //}


                            if (count == 0)
                            {
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["IsConnect"] = 0;
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["Class"] = "Y";
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["CabinClassCode"] = "Y";

                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["CabinClassName"] = "Economy";
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["NoSeats"] = 5;
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["ElapsedTime"] = "7:01";
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["ActualTime"] = "15:21";

                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["TechStopOver"] = 0;
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["BI_Price"] = 0;
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["TransitTime"] = "NA";
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["Distance"] = 0;

                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["ETicket"] = 1;
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["ChangeOfPlane"] = 0;
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["ParticipantLevel"] = "Secure Sell";
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["OptionalServicesIndicator"] = "true";

                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["BookingCodeInfo"] = "NA";
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["FareBasisCode"] = "NA";
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["SupCode"] = "NA";

                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["Sector_Key"] = Guid.NewGuid().ToString();
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["AvailabilitySource"] = "NA";
                                tblcsvfor_CustomItin.Rows[tblcsvfor_CustomItin.Rows.Count - 1]["ItinKey"] = Guid.NewGuid().ToString();
                            }

                            count++;
                        }
                    }
                }
                Rowcount++;
            }

            //Calling insert Functions  
            InsertCustomItin(tblcsvfor_CustomItin);
        }
        catch (Exception ex)
        {

        }
        finally
        {

        }
    }


    public string CheckAirportCode(string FileRec,string DesiredVal)
    { 
        string FinalValue = "NA";
        try
        {
            switch(DesiredVal)
            {
                case "Airport_Name":
                    FinalValue = lstclsAirport.Where(z => z.Airport_Code == FileRec.ToUpper()).FirstOrDefault().Airport_Name;
                    break;
                case "Country_Name":
                    FinalValue = lstclsAirport.Where(z => z.Airport_Code == FileRec.ToUpper()).FirstOrDefault().Country_Name;
                    break;
                case "City_Name":
                    FinalValue = lstclsAirport.Where(z => z.Airport_Code == FileRec.ToUpper()).FirstOrDefault().City_Name;
                    break;

            }
                
            
        }
        catch(Exception ex)
        {

        }
        finally
        {
             
        }
        return FinalValue;
    }

    private void InsertCustomItin(DataTable csvCustomItindt)
    {
        SqlConnection sqlcon = DataConnection.GetConnectionCustomItinerary();
        using (SqlConnection con = new SqlConnection(sqlcon.ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Insert_CustomItinExcelUS"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@tblCustomItinUS", csvCustomItindt);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                lblMsg.Text = "Rows Updated successfully";
            }
        }
    }


    #endregion




    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        SqlConnection sqlcon = DataConnection.GetConnectionCustomItinerary();

        try
        {
            sqlcon.Open();
            SqlCommand cmd = new SqlCommand();
            StringBuilder strcmd = new StringBuilder("Select CustomItinUS.UniqueId, ValCarrier, JourneyType, Pax_A_BaseFare, Pax_A_Taxes, Origin, Destination, Camp_ID, ValidFrom, ValidTo, OperatingCarrierCode, SerialId, AirV, FltNum, D_AirportCode, D_AirportCityCode, D_AirportCountryCode, D_Terminal, D_Time, A_AirportCode, A_AirportCityCode, A_AirportCountryCode, A_Terminal, IsNextDayCount, A_Time, EquipType, IsReturn, BI_Pieces,BI_Description, Sector_Group, BaggageInfo,Currency FROM CustomItinUS INNER JOIN CustomItinBaseUS ON CustomItinUS.UniqueId = CustomItinBaseUS.UniqueId WHERE 1 = 1");

            strcmd.Append(" and CustomItinBaseUS.JourneyType ='" + ddlJourneyType.SelectedValue + "'");

            if (TxtFrom.Text.Trim().ToUpper() != "")
            {
                strcmd.Append(" and CustomItinBaseUS.Origin='" + TxtFrom.Text.Trim().ToUpper() + "'");
            }
            if (TxtTo.Text.Trim().ToUpper() != "")
            {
                strcmd.Append(" and CustomItinBaseUS.Destination='" + TxtTo.Text.Trim().ToUpper() + "'");
            }
            if (TxtAir.Text.Trim().ToUpper() != "")
            {
                strcmd.Append(" and CustomItinBaseUS.ValCarrier='" + TxtAir.Text.Trim().ToUpper() + "'");
            }
            if (txtUniqueId.Text != "")
            {
                strcmd.Append(" and CustomItinBaseUS.Uniqueid='" + txtUniqueId.Text.Trim().ToUpper() + "'");
            }


            if (ddlCampaign.SelectedIndex != 0)
            {
                strcmd.Append(" and CustomItinBaseUS.CampId='" + ddlCampaign.SelectedValue + "'");
            }
            strcmd.Append(" order by CustomItinUS.Uniqueid desc, CustomItinUS.Serialid asc");
            cmd.Connection = sqlcon;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strcmd.ToString();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            ExportToCSV(dt);
        }
        catch (Exception ex)
        {

        }
        finally
        {

        }
    }

    private void ExportToCSV(DataTable dt)
    {
        //old code
        string fileName = "USItinerary_" + DateTime.Now.ToString("ddMMMyyyy");
        //string csv = string.Empty;

        //foreach (DataColumn column in dt.Columns)
        //{
        //    //Add the Header row for CSV file.
        //    csv += column.ColumnName + ',';
        //}

        ////Add new line.
        //csv += "\r\n";

        //foreach (DataRow row in dt.Rows)
        //{
        //    foreach (DataColumn column in dt.Columns)
        //    {
        //        //Add the Data rows.
        //        csv += row[column.ColumnName].ToString().Replace(",", ";") + ',';
        //    }

        //    //Add new line.
        //    csv += "\r\n";
        //}

        ////Download the CSV file.
        //Response.Clear();
        //Response.Buffer = true;
        //Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".csv");
        //Response.Charset = "";
        //Response.ContentType = "application/text";
        //Response.Output.Write(csv);
        //Response.Flush();
        //Response.End();
        //end code


        StringBuilder sb = new StringBuilder();

        IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().
                                          Select(column => column.ColumnName);
        sb.AppendLine(string.Join(",", columnNames));

        foreach (DataRow row in dt.Rows)
        {
            IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
            sb.AppendLine(string.Join(",", fields));
        }
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".csv");
        Response.Charset = "";
        Response.ContentType = "application/text";
        Response.Output.Write(sb.ToString());
        Response.Flush();
        Response.End();


    }

    protected void btnDeleteAll_Click(object sender, EventArgs e)
    {
        SqlConnection connection = DataConnection.GetConnectionCustomItinerary();
        try
        {
            connection.Open();
            string query = "Truncate table CustomItinBaseUS";
            SqlCommand command = new SqlCommand(query, connection);
            int i = command.ExecuteNonQuery();

            string query2 = "Truncate table CustomItinUS";
            SqlCommand command2 = new SqlCommand(query2, connection);
            int i2 = command2.ExecuteNonQuery();

            lblMsg.Text = "All Itinerary are successfully deleted";
        }
        catch (Exception ex)
        {
            lblMsg.Text = "All Itinerary are not deleted successfully ";
            throw ex;
        }
        finally
        {
            connection.Close();
        }
        Bind_Itin_details();
    }

    Dictionary<string, string> dictAirLine = new Dictionary<string, string>();
    List<clsAirport> lstclsAirport = new List<clsAirport>();
    clsAirport objAirport = null;


    public class clsAirport
    {
        public string City_Name { get; set; }
        public string Airport_Name { get; set; }
        public string Airport_Code { get; set; }
        public string Country_Code { get; set; }
        public string Country_Name { get; set; }
        public string City_Code { get; set; }
    }



    public void GetAutoCompleteAirport(string Prefix)
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
    }

    public void GetAutoCompleteAirline()
    {
        DataTable dt = new DataTable();
        SqlConnection sqlcon = DataConnection.GetConnection();
        try
        {
            sqlcon.Open();
            SqlCommand cmd = new SqlCommand();
            StringBuilder strcmd = new StringBuilder("SELECT * from  Airline_Detail");
            cmd.Connection = sqlcon;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strcmd.ToString();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);

            adp.Fill(dt);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        dictAirLine.Add(Convert.ToString(dr["Airline_Code"]), Convert.ToString(dr["Airline_Name"]));
                    }
                }
            }

        }
        catch (Exception Ex)
        {

        }
        finally
        {
            sqlcon.Close();
        }
    }
}
