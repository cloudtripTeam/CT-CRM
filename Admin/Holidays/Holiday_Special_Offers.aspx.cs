using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class Admin_Holidays_Holiday_Special_Offers : System.Web.UI.Page
{
    FandHServices.FandHServicesClient objlos = new FandHServices.FandHServicesClient();
    static int ron;
    public string Category { set; get; }
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
                CommanBinding.BindCompanyDetails(ref ddlCompany, objUserDetail.userID);

                BindOffer("SELECT", null, Convert.ToInt32(Request.QueryString["ID"]),null,null,null,null);
                BindOfferDetails("SELECT", Convert.ToInt32(Request.QueryString["ID"]));
                BindImages("SELECT", Convert.ToInt32(Request.QueryString["ID"]));
               
            }
        }
    }
    private void BindOffer(string Query, string DestCode, int ID, string HName, int? night, int? rating, int? discount)
    {
        DataTable dt = new DataTable();
        dt = objlos.GET_Holiday_Offers(Query, DestCode, ID, HName, night,rating,discount);
        if (dt.Rows.Count > 0)
        {
            txtDestCode.Text = dt.Rows[0]["HLD_OFR_Destination_Code"].ToString();
            txtHotelName.Text = dt.Rows[0]["HLD_OFR_Hotel_Name"].ToString();
            txtRating.Text = dt.Rows[0]["HLD_OFR_Star_Rating"].ToString();
            ddlOfferTypes.SelectedValue = dt.Rows[0]["HLD_OFR_Offer_Type"].ToString();
            string[] cat = dt.Rows[0]["HLD_OFR_Holiday_Category"].ToString().Split('|');
            for (int i = 0; i < chkCategory.Items.Count; i++)
            {
                foreach (string ct in cat)
                {
                    if (chkCategory.Items[i].Text == ct.ToString())
                    {
                        chkCategory.Items[i].Selected = true;
                    }
                }
            }
            chkCategory.SelectedValue = dt.Rows[0]["HLD_OFR_Holiday_Category"].ToString();
            txtNoofNights.Text = dt.Rows[0]["HLD_OFR_No_Of_Nights"].ToString();
            txtPrice.Text = Convert.ToDecimal(dt.Rows[0]["HLD_OFR_Price"]).ToString("0.00");
            ddlBasicBoard.SelectedValue = dt.Rows[0]["HLD_OFR_Hotel_Board_Basic"].ToString();
            txtDiscount.Text = dt.Rows[0]["HLD_OFR_Discount"].ToString();
            txtOverviews.Text = dt.Rows[0]["HLD_OFR_Holiday_Overviews"].ToString();
            ron = Convert.ToInt32(dt.Rows[0]["HLD_OFR_HolidayID"]);
            ltrImages.Text = dt.Rows[0]["HLD_OFR_Main_Image"].ToString();
            try { ddlCompany.SelectedValue = dt.Rows[0]["HLD_OFR_Company_ID"].ToString(); }
            catch { }
        }
    }

    private void BindOfferDetails(string Query, int ID)
    {
        DataTable dt = new DataTable();
        dt = objlos.GET_Holiday_Special_Offers(Query, ID);
        if (dt.Rows.Count > 0)
        {
            rptrHolidaysDetails.DataSource = dt;
            rptrHolidaysDetails.DataBind();
        }

    }

    //private HolidayOfferDetails GetHolidayOfferDetails(DataRow dr)
    //{
    //    HolidayOfferDetails objHoffer = new HolidayOfferDetails();
    //    objHoffer.SrNo = Convert.ToInt32(dr["HLD_OFR_HolidayID"]);
    //    objHoffer.HolidayID = Convert.ToInt32(dr["HLD_OFR_HolidayID"]);
    //    objHoffer.OfferType = dr["HLD_SPL_OFR_Offer_Type"].ToString();
    //    objHoffer.Icon = dr["HLD_SPL_OFR_Icon"].ToString();
    //    objHoffer.MainPage = Convert.ToBoolean(dr["HLD_SPL_OFR_Show_On_Main_Page"]);
    //    objHoffer.ModBy = dr["HLD_SPL_OFR_Modify_By"].ToString();
    //    objHoffer.ModDate = Convert.ToDateTime(dr["HLD_SPL_OFR_Last_Modified"]);
    //    objHoffer.SpecialOffer = dr["HLD_SPL_OFR_Offer"].ToString();
    //    return objHoffer;
    //}


    private void BindImages(string Query, int ID)
    {
        DataTable dt = new DataTable();
        dt = objlos.GET_Holiday_Offer_Images(Query, ID);
        if (dt.Rows.Count > 0)
        {
            rptrHolidayImages.DataSource = dt;
            rptrHolidayImages.DataBind();
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            bool qr;
            string fileName = "";
            bool ab = chkMainPage.Checked == true ? true : false;
            if (fpIcon.HasFile)
            {
                fileName = Path.GetFileName(fpIcon.PostedFile.FileName);

                fpIcon.PostedFile.SaveAs(Server.MapPath("~/Admin/MainHotel/") + ron.ToString() + "_" + fileName);

                fileName = "//backoffice.flightxpertuk.com/Admin/MainHotel/" + ron.ToString() + "_" + fileName;
            }
            qr = objlos.SET_Holiday_Special_Offers("INSERT", 0, ron, ddlOfferType.SelectedItem.Text, fileName, ab, "", System.DateTime.Now, txtOffer.Text);
            if (qr == true) { ltrMsg.Text = "Record Inserted"; }
            else { ltrMsg.Text = "Not Insert!!!"; }
            BindOfferDetails("SELECT", ron);
            System.Web.UI.HtmlControls.HtmlGenericControl dynDiv = new System.Web.UI.HtmlControls.HtmlGenericControl(Request.Form["collapse2"]);
            //dynDiv.Attributes.Add("class", "panel-collapse collapse in");
            dynDiv.Attributes["class"] = "panel-collapse collapse in";
        }
        catch (Exception ex) {
            ltrMsg.Text = ex.Message;
        }   
    }
    protected void btnImages_Click(object sender, EventArgs e)
    {
        try
        {
            bool qr;
            if (fupImages.HasFile)
            {

                string fileName = Path.GetFileName(fupImages.PostedFile.FileName);


                fupImages.PostedFile.SaveAs(Server.MapPath("~/Admin/MainHotel/") + "Img_" + ron.ToString() + "_" + fileName);
                fileName = "//backoffice.flightxpertuk.com/Admin/MainHotel/" + "Img_" + ron.ToString() + "_" + fileName;

                qr = objlos.SET_Holiday_Offer_Images("INSERT", ron, fileName, Convert.ToInt32(txtImageSec.Text), "", System.DateTime.Now);
                if (qr == true) { ltrMsg.Text = "Record Inserted"; }
                else { ltrMsg.Text = "Not Insert!!!"; }
                BindImages("SELECT", ron);
            }
        }
        catch (Exception ex)
        {
            ltrMsg.Text = ex.Message;
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            this.Category = "|";

            foreach (ListItem val in chkCategory.Items)
            {
                if (val.Selected)
                {
                    Category += "|" + val.Value;
                }
            }
            bool qr;
            if (fpImage.HasFile)
            {
                string fileName = Path.GetFileName(fpImage.PostedFile.FileName);

                fpImage.PostedFile.SaveAs(Server.MapPath("~/Admin/MainHotel/") + txtDestCode.Text + "_" + fileName);
                ltrImages.Text = "//backoffice.flightxpertuk.com/Admin/MainHotel/" + txtDestCode.Text + "_" + fileName;

            }

            qr = objlos.SET_Holiday_Offers("UPDATE", ron, txtDestCode.Text, txtHotelName.Text, Convert.ToInt32(txtRating.Text), ddlOfferTypes.SelectedValue, Category, Convert.ToInt32(txtNoofNights.Text),
            Convert.ToDouble(txtPrice.Text), ddlBasicBoard.SelectedValue, Convert.ToInt32(txtDiscount.Text), txtOverviews.Text, ltrImages.Text, ddlCompany.SelectedValue, "", "", System.DateTime.Now);
            if (qr == true) { ltrMsg.Text = "Record Updated"; }
            else { ltrMsg.Text = "Not Update!!!"; }

        }
        catch (Exception ex)
        {
            ltrMsg.Text = ex.Message;
        }
    }

    protected void rptrHolidaysDetails_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if(e.CommandName=="delete")
        {
            bool qr;
            int ltr = Convert.ToInt32(((Literal)(rptrHolidaysDetails.Items[e.Item.ItemIndex].FindControl("ltrDID"))).Text);
            qr = objlos.SET_Holiday_Special_Offers("DELETE", ltr, ltr, "", "", true, "", System.DateTime.Now, "");
            if (qr == true) { ltrMsg.Text = "Record Deleted";
            BindOfferDetails("SELECT", ron);
            }
            else { ltrMsg.Text = "Not Deleted!!!"; }

        }
    }
    protected void rptrHolidayImages_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
         if(e.CommandName=="delete")
        {
            bool qr;
            int ltr = Convert.ToInt32(((Literal)(rptrHolidayImages.Items[e.Item.ItemIndex].FindControl("ltrDIDImage"))).Text);
            qr = objlos.SET_Holiday_Offer_Images("DELETE", ltr, "", 1, "", System.DateTime.Now);
            if (qr == true) { ltrMsg.Text = "Record Deleted";
            BindImages("SELECT", ron);
            }
            else { ltrMsg.Text = "Not Deleted!!!"; }
        }
        
    }
}

