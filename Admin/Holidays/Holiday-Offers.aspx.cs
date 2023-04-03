using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_Holidays_Holiday_Offers : System.Web.UI.Page
{
    FandHServices.FandHServicesClient objlos = new FandHServices.FandHServicesClient();
    public string Category { set; get; }
    public string Holidays { set; get; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserDetails"] != null)
        {

            if (!IsPostBack)
            {
                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
                if (!objUserDetail.isAuth("BookingDetails.aspx"))
                {
                    Response.Redirect("Default.aspx");
                    return;
                }
                else
                { CommanBinding.BindCompanyDetails(ref ddlCompany, objUserDetail.userID); }

            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }

       

    }

    private void AddCategory()
    {
        foreach (ListItem item in ddlCategory.Items)
        {
            if (item.Selected)
            {
                if (string.IsNullOrEmpty(Category))
                {
                    Category = item.Value;
                }
                else
                {
                    Category += "|" + item.Value;
                }
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.Category = "|";
        AddCategory();

        bool qr;
        if (fpImage.HasFile)
        {
            string fileName = Path.GetFileName(fpImage.PostedFile.FileName);
           
            fpImage.PostedFile.SaveAs(Server.MapPath("~/Admin/MainHotel/") + txtDestCode.Text + "_" + fileName);
            fileName = "//backoffice.flightxpertuk.com/Admin/MainHotel/" + txtDestCode.Text + "_" + fileName;
            qr = objlos.SET_Holiday_Offers("INSERT", 0, txtDestCode.Text, txtHotelName.Text, Convert.ToInt32(txtRating.Text), ddlOfferType.SelectedValue, Category, Convert.ToInt32(txtNoofNights.Text),
                Convert.ToDouble(txtPrice.Text), ddlBasicBoard.SelectedValue, Convert.ToInt32(txtDiscount.Text), txtOverviews.Text, fileName, "", "", "", System.DateTime.Now);
            if (qr == true) { ltrMsg.Text = "Record Inserted"; }
            else { ltrMsg.Text = "Not Insert!!!"; }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();

        dt = objlos.GET_Holiday_Offers("SELECT", txtDestCode.Text, null, txtHotelName.Text, null, null, null);
        
        if (dt.Rows.Count > 0)
        {
            foreach(DataRow dr in dt.Rows)
            {
                Holidays += @"<div class='col-md-12 mt-10 row-bordered'>" +
                                     "<div class='col-md-1'>" + dr["HLD_OFR_HolidayID"].ToString() + "</div>" +
                                    "<div class='col-md-1'>" + dr["HLD_OFR_Destination_Code"].ToString() + "</div>" +
                                    "<div class='col-md-2'>" + dr["HLD_OFR_Hotel_Name"].ToString() + "</div>" +
                                    "<div class='col-md-1'>" + dr["HLD_OFR_Star_Rating"].ToString() + "</div>" +
                                    "<div class='col-md-1'>" + dr["HLD_OFR_Offer_Type"].ToString() + "</div>" +
                                    "<div class='col-md-1'>" + dr["HLD_OFR_No_Of_Nights"].ToString() + "</div>" +
                                    "<div class='col-md-1'>" + dr["HLD_OFR_Price"].ToString() + "</div>" +
                                    "<div class='col-md-2'>" + dr["HLD_OFR_Hotel_Board_Basic"].ToString() + "</div>" +
                                    "<div class='col-md-1'>" + dr["HLD_OFR_Discount"].ToString() + "</div>" +
                                    "<div class='col-md-1'> " + dr["HLD_OFR_Company_ID"].ToString() + " <a class='btn btn-info btn-xs' href='Holiday_Special_Offers.aspx?ID=" + dr["HLD_OFR_HolidayID"].ToString() + "'>View Details</a></div>" +
                                "</div>";
            }
        }

    }

   

}
