using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ChargebackUploadingReports : System.Web.UI.Page
{
    SqlConnection con = DataConnection.GetConnection();
    string idxp = string.Empty;
    string idempty = string.Empty;
  
    protected void Page_Load(object sender, EventArgs e)
    {
    
        if (!string.IsNullOrEmpty(Request.QueryString.Get("BID")))
        {
            idxp = Request.QueryString.Get("BID");
            
        }
        if (Session["UserDetails"] != null)
        {
            if (!IsPostBack)
            {
                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
        if (!Page.IsPostBack)
        {

            bindMarkUp();
        }
        if (idxp == "")
        {
            btnInsert.Visible = false;
        }
        else
        {
            btnInsert.Visible = true;
        }

    }
    protected void btnInsert_Click(object sender, EventArgs e)
    {
        string message = "Please Enter Chargeback Reason";
       
        if (btnInsert.Text == "Insert")
        {         
            if (string.IsNullOrEmpty(drpChargebakCode.SelectedValue))
            {               
                 ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + message + "');", true);
                 drpChargebakCode.Focus();
                 return;
            }
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Insert_ChargeBkpReport", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PBooking_id", idxp);
                cmd.Parameters.AddWithValue("@PChargeback_Received", txtChegRecivDate.Text);
                cmd.Parameters.AddWithValue("@PChargebackDisputereport", txtDisputeDate.Text);
                cmd.Parameters.AddWithValue("@PChargeback_Dispute", drpCardType.SelectedValue);
                cmd.Parameters.AddWithValue("@PChargeback_Type", drpChageTpe.SelectedValue);
                cmd.Parameters.AddWithValue("@PDisputed_Amount", txtDisbuteAmunt.Text);
                cmd.Parameters.AddWithValue("@PChargeback_Reason", drpChargebakCode.SelectedValue);
                cmd.Parameters.AddWithValue("@PChargeback_Status", drpChrgBackStus.SelectedValue);
                cmd.Parameters.AddWithValue("@PDocumentType", drpDocumentType.SelectedValue);
                cmd.Parameters.AddWithValue("@PCaseNo", txtCaseNo.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script LANGUAGE='JavaScript'>alert('Data Successful Insert')</script>");
                txtDisputeDate.Text = "";
                drpChargebakCode.SelectedValue = "";
                txtDisbuteAmunt.Text = "";
                txtChegRecivDate.Text = "";
                txtCaseNo.Text = "";
                drpChageTpe.SelectedValue = "0";
                drpChrgBackStus.SelectedValue = "0";
                drpDocumentType.SelectedValue = "0";
                bindMarkUp();
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong.");
            }
            finally
            {
                Console.WriteLine("The 'try catch' is finished.");
            }
        }
        else
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update_ChargeBkpReport", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PID", ViewState["VSV"]);
                cmd.Parameters.AddWithValue("@PBooking_id", idxp);
                cmd.Parameters.AddWithValue("@PChargeback_Received", txtChegRecivDate.Text);
                cmd.Parameters.AddWithValue("@PChargebackDisputereport", txtDisputeDate.Text);
                cmd.Parameters.AddWithValue("@PChargeback_Dispute", drpCardType.SelectedValue);
                cmd.Parameters.AddWithValue("@PChargeback_Type", drpChageTpe.SelectedValue);
                cmd.Parameters.AddWithValue("@PDisputed_Amount", txtDisbuteAmunt.Text);
                cmd.Parameters.AddWithValue("@PChargeback_Reason", drpChargebakCode.SelectedValue);
                cmd.Parameters.AddWithValue("@PDocumentType ", drpDocumentType.SelectedValue);
                cmd.Parameters.AddWithValue("@PChargeback_Status", drpChrgBackStus.SelectedValue);
                cmd.Parameters.AddWithValue("@PCaseNo", txtCaseNo.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script LANGUAGE='JavaScript' >alert('Post Successful Updated !')</script>");
                txtDisputeDate.Text = "";
                drpChargebakCode.SelectedValue = "";
                txtChegRecivDate.Text = "";
                txtDisbuteAmunt.Text = "";
                drpChageTpe.SelectedValue = "0";
                drpChrgBackStus.SelectedValue = "0";
                txtCaseNo.Text = "";
                drpDocumentType.SelectedValue = "0";
                bindMarkUp();
                btnInsert.Text = "Insert";
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong.");
            }
            finally
            {
                Console.WriteLine("The 'try catch' is finished.");
            }
        }
    }
    private void bindMarkUp()
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        UserDetail objUserDetail = HttpContext.Current.Session["UserDetails"] as UserDetail;
        if (idxp == "")
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("GET_ChargeBkpReport_DynSearch", con);
            cmd.Parameters.AddWithValue("@action", "ALL");
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            rptrEdit.DataSource = dt;
            rptrEdit.DataBind();
            //ViewState["DATAEXEAL"] = dt;
        }
        else
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("GET_ChargeBkpReport_DynSearch", con);
            cmd.Parameters.AddWithValue("@Action", "BYID");
            cmd.Parameters.AddWithValue("@Booking_id", idxp);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            rptrEdit.DataSource = dt;
            rptrEdit.DataBind();
            ViewState["DATAEXEAL"] = dt;
        }

    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        UserDetail objUserDetail = HttpContext.Current.Session["UserDetails"] as UserDetail;
        SqlCommand cmd = new SqlCommand("Select * from Export_ChargeBack WHERE Booking_ID='" + idxp+"'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();   
        if (dt != null)
        {
            string attachment = "attachment; filename=" + "ChargebackDisputereport" + DateTime.Now.ToString("dd/MM/yyyy") + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }
    }

    protected void rptrEdit_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "ED")
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SelectByIdChargeReport", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PID ", e.CommandArgument);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            txtDisputeDate.Text = dt.Rows[0]["ChargebackDisputereport"].ToString();
            txtChegRecivDate.Text = dt.Rows[0]["Chargeback_Received"].ToString();
            drpChageTpe.SelectedValue = dt.Rows[0]["Chargeback_Type"].ToString();
            drpCardType.SelectedValue = dt.Rows[0]["Chargeback_Dispute"].ToString();           
            txtDisbuteAmunt.Text = dt.Rows[0]["Disputed_Amount"].ToString();

            ChargeBackRcode(drpCardType.SelectedValue);

            drpChargebakCode.SelectedValue = dt.Rows[0]["Chargeback_Reason"].ToString();
            drpChrgBackStus.SelectedValue = dt.Rows[0]["Chargeback_Status"].ToString();
            drpDocumentType.SelectedValue = dt.Rows[0]["DocumentType"].ToString();
            txtCaseNo.Text = dt.Rows[0]["CaseNo"].ToString();
            ViewState["VSV"] = dt.Rows[0]["ID"].ToString(); ;
            con.Close();
            btnInsert.Text = "Update";
        }
    }
 
    public void ChargeBackRcode(string value_)
    {
        var tupleList = new List<Tuple<string, string,string,string>>
        {
        new Tuple<string, string,string,string>("VISA", "Fraud","10.1","EMV Liability Shift Counterfeit Fraud"),
        new Tuple<string, string,string,string>("VISA", "Fraud","10.2","EMV Liability Shift Non-Counterfeit Fraud"),
        new Tuple<string, string,string,string>("VISA", "Fraud","10.3","Other Fraud: Card-Present Environment / Condition"),
        new Tuple<string, string,string,string>("VISA", "Fraud","10.4","Other Fraud: Card-absent Environment / Condition"),
        new Tuple<string, string,string,string>("VISA", "Fraud","10.5","Visa Fraud Monitoring Program"),

        new Tuple<string, string,string,string>("VISA", "Authorization","11.1","Card Recovery Bulletin"),
        new Tuple<string, string,string,string>("VISA", "Authorization","11.2","Declined Authorization"),
        new Tuple<string, string,string,string>("VISA", "Authorization","10.3","No Authorization"),

        new Tuple<string, string,string,string>("VISA", "Processing Errors","12.1"," Late Presentment"),
        new Tuple<string, string,string,string>("VISA", "Processing Errors","12.2","Incorrect Transaction Code"),
        new Tuple<string, string,string,string>("VISA", "Processing Errors","12.3","Incorrect Currency"),
        new Tuple<string, string,string,string>("VISA", "Processing Errors","12.4","Incorrect Account Number"),
        new Tuple<string, string,string,string>("VISA", "Processing Errors","12.5","Incorrect Amount"),
        new Tuple<string, string,string,string>("VISA", "Processing Errors","12.6","Duplicate Processing / Paid by Other Means"),
        new Tuple<string, string,string,string>("VISA", "Processing Errors","12.7","Invalid Data"),

        new Tuple<string, string,string,string>("VISA", "Customer Disputes","13.1","Merchandise / Services Not Received"),
        new Tuple<string, string,string,string>("VISA", "Customer Disputes","13.2","Canceled Recurring Transaction"),
        new Tuple<string, string,string,string>("VISA", "Customer Disputes","13.3","Not as Described or Defective Merchandise / Services"),
        new Tuple<string, string,string,string>("VISA", "Customer Disputes","13.4","Counterfeit Merchandise"),
        new Tuple<string, string,string,string>("VISA", "Customer Disputes","13.5","Counterfeit Merchandise"),
        new Tuple<string, string,string,string>("VISA", "Customer Disputes","13.6","Credit Not Processed"),
        new Tuple<string, string,string,string>("VISA", "Customer Disputes","13.7","Cancelled Merchandise / Services"),
        new Tuple<string, string,string,string>("VISA", "Customer Disputes","13.8","Original Credit Transaction Not Accepted"),
        new Tuple<string, string,string,string>("VISA", "Customer Disputes","13.9","Non-Receipt of Cash or Load Transaction Value"),
        //===============Amex======================================================================
        new Tuple<string, string,string,string>("Amex", "Authorization","A01","Charge amount exceeds authorization amount"),
        new Tuple<string, string,string,string>("Amex", "Authorization","A02","No valid authorization"),
        new Tuple<string, string,string,string>("Amex", "Authorization","A08","Authorization approval expired"),

        new Tuple<string, string,string,string>("Amex", "Cardmember Dispute","C02","Credit not processed"),
        new Tuple<string, string,string,string>("Amex", "Cardmember Dispute","C04","Goods / services returned or refused"),
        new Tuple<string, string,string,string>("Amex", "Cardmember Dispute","C05","Goods / services canceled"),
        new Tuple<string, string,string,string>("Amex", "Cardmember Dispute","C08","Goods / Services Not Received or Only Partially Received"),
        new Tuple<string, string,string,string>("Amex", "Cardmember Dispute","C14","Paid by Other Means"),
        new Tuple<string, string,string,string>("Amex", "Cardmember Dispute","C18","No Show or CARDeposit Canceled"),
        new Tuple<string, string,string,string>("Amex", "Cardmember Dispute","C28","Canceled Recurring Billing"),
        new Tuple<string, string,string,string>("Amex", "Cardmember Dispute","C31","Goods / Services Not as DescribedGoods / Services Not as Described"),
        new Tuple<string, string,string,string>("Amex", "Cardmember Dispute","C32","Goods / Services Damaged or Defective"),
        new Tuple<string, string,string,string>("Amex", "Cardmember Dispute","M10","Vehicle Rental - Capital Damages"),
        new Tuple<string, string,string,string>("Amex", "Cardmember Dispute","M49","Vehicle Rental - Theft or Loss of Use"),
        new Tuple<string, string,string,string>("Amex", "Fraud","FR2","Fraud Full Recourse Program"),
        new Tuple<string, string,string,string>("Amex", "Fraud","FR4","Immediate Chargeback Program"),
        new Tuple<string, string,string,string>("Amex", "Fraud","FR6","Partial Immediate Chargeback Program"),
        new Tuple<string, string,string,string>("Amex", "Fraud","F10","Missing Imprint"),
        new Tuple<string, string,string,string>("Amex", "Fraud","F14","Missing Signature"),
        new Tuple<string, string,string,string>("Amex", "Fraud","F24"," No Cardmember Authorization"),
        new Tuple<string, string,string,string>("Amex", "Fraud","F29"," Card Not Present"),
        new Tuple<string, string,string,string>("Amex", "Fraud","F30"," EMV Counterfeit"),
        new Tuple<string, string,string,string>("Amex", "Fraud","F31","EMV List / Stolen / Non-received."),
        new Tuple<string, string,string,string>("Amex", "Processing Errors","P01","Unassigned Card Number"),
        new Tuple<string, string,string,string>("Amex", "Processing Errors","P03","Credit Processed as Charge"),
        new Tuple<string, string,string,string>("Amex", "Processing Errors","P04","Charge Processed as Credit"),
        new Tuple<string, string,string,string>("Amex", "Processing Errors","P05","Incorrect Charge Amount"),
        new Tuple<string, string,string,string>("Amex", "Processing Errors","P07","Late Submission"),
        new Tuple<string, string,string,string>("Amex", "Processing Errors","P08","Duplicate Charge"),
        new Tuple<string, string,string,string>("Amex", "Processing Errors","P22","Non-Matching Card Number"),
        new Tuple<string, string,string,string>("Amex", "Processing Errors","P23","Currency Discrepancy"),
        //===============DICOVER============================================
        new Tuple<string, string,string,string>("Discover", "Cardholder Dispute","AA"," Cardholder Does Not Recognize"),
        new Tuple<string, string,string,string>("Discover", "Cardholder Dispute","AP"," Canceled Recurring Transaction"),
        new Tuple<string, string,string,string>("Discover", "Cardholder Dispute","AW"," Altered Amount"),
        new Tuple<string, string,string,string>("Discover", "Cardholder Dispute","CD"," Credit Posted as Card Sale"),
        new Tuple<string, string,string,string>("Discover", "Cardholder Dispute","DP"," Duplicate Processing"),
        new Tuple<string, string,string,string>("Discover", "Cardholder Dispute","IC"," Illegible Sales Data"),
        new Tuple<string, string,string,string>("Discover", "Cardholder Dispute","NF"," Non-Receipt of Cash from ATM"),
        new Tuple<string, string,string,string>("Discover", "Cardholder Dispute","PM"," Paid by Other Means"),
        new Tuple<string, string,string,string>("Discover", "Cardholder Dispute","RG"," Non-Receipt of Goods or Services"),
        new Tuple<string, string,string,string>("Discover", "Cardholder Dispute","RM","Quality Discrepancy"),
        new Tuple<string, string,string,string>("Discover", "Cardholder Dispute","RN2"," Credit Not Received"),

        new Tuple<string, string,string,string>("Discover", "Authorization","AT"," Authorization Non-compliance"),
        new Tuple<string, string,string,string>("Discover", "Authorization","DA"," Declined Authorization"),
        new Tuple<string, string,string,string>("Discover", "Authorization","EX"," Expired Card"),
        new Tuple<string, string,string,string>("Discover", "Authorization","NA ","No Authorization"),

        new Tuple<string, string,string,string>("Discover", "Processing Errors","IN","Invalid Card Number"),
        new Tuple<string, string,string,string>("Discover", "Processing Errors","LP","Late Presentment"),

        new Tuple<string, string,string,string>("Discover", "Fraud","UA01"," Fraud / Card Present Environment"),
        new Tuple<string, string,string,string>("Discover", "Fraud","UA02"," Fraud / Card-Not-Present Environment"),
        new Tuple<string, string,string,string>("Discover", "Fraud","UA05","Fraud / Counterfeit Chip Transaction"),
        new Tuple<string, string,string,string>("Discover", "Fraud","UA06"," Fraud / Chip-and-Pin Transaction"),
        new Tuple<string, string,string,string>("Discover", "Fraud","UA10"," Request Transaction Receipt (swiped card transactions)"),
        new Tuple<string, string,string,string>("Discover", "Fraud","UA11"," Cardholder claims fraud (swiped transaction, no signature)"),
              //===============Master============================================
        new Tuple<string, string,string,string>("Master Card", "Authorization-Related Chargebacks","4808","Warning Bulletin File Authorization-Related Chargeback Account Number Not on File Required Authorization Not Obtained Expired Chargeback Protection Period Multiple Authorization Requests Cardholder-Activated Terminal (CAT) 3 Device"),
            
        new Tuple<string, string,string,string>("Master Card", "Point of Interaction Error","4834","Point of Interaction Error Transaction Amount Differs Late Presentment Point-of-Interaction Currency Conversion Duplication/Paid by Other Means ATM Disputes Loss, Theft, or Damages No Cardholder Authorization/Fraud-related Chargebacks"),
        new Tuple<string, string,string,string>("Master Card", "No Cardholder Authorization/Fraud-related Chargebacks","4837","No Cardholder Authorization"),
        new Tuple<string, string,string,string>("Master Card", "No Cardholder Authorization/Fraud-related Chargebacks","4849","Questionable Merchant Activity"),
        new Tuple<string, string,string,string>("Master Card", "No Cardholder Authorization/Fraud-related Chargebacks","4870","Chip Liability Shift"),
        new Tuple<string, string,string,string>("Master Card", "No Cardholder Authorization/Fraud-related Chargebacks","4871","Chip / PIN Liability Shift--Lost / Stolen / Never Received Issue (NRI) Fraud"),
       
        new Tuple<string, string,string,string>("Master Card", "Cardholder Disputes","4853","Cardholder Dispute of a Recurring Transaction Goods or Services Not Provided No-Show Hotel Charge Addendum Dispute Credit Not Processed Goods/Services not as Described or Defective Digital Goods $25 or less Counterfeit Goods Transaction Did Not Complete Credit Posted as a Purchase"),
        new Tuple<string, string,string,string>("Master Card", "Cardholder Disputes","4854","Cardholder Dispute Not Classified Elsewhere (US)"),
        new Tuple<string, string,string,string>("Master Card", "Cardholder Disputes","4860","Credit Not Processed"),
       // new Tuple<string, string,string,string>("Master Card", "Cardholder Disputes","LP","Presentment"),
       };

        foreach(var rt in tupleList)
        {
            if(rt.Item1==value_)
            {

                drpChargebakCode.Items.Add(new ListItem(rt.Item4, rt.Item3));
                
            }
        }
      

    }
    protected void drpCardType_SelectedIndexChanged(object sender, EventArgs e)
    {
        drpChargebakCode.Items.Clear();
        drpChargebakCode.Items.Insert(0, new ListItem("---------Select------", ""));
        if (drpCardType.SelectedIndex != 0)
        {
            ChargeBackRcode(drpCardType.SelectedValue);
        }
    }
    protected void btnSerch_Click(object sender, EventArgs e)
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        UserDetail objUserDetail = HttpContext.Current.Session["UserDetails"] as UserDetail;
        con.Open();
        SqlCommand cmd = new SqlCommand("GET_ChargeBkpReport_DynSearch", con);
        cmd.Parameters.AddWithValue("@Booking_id", idxp);
        cmd.Parameters.AddWithValue("@Chargeback_Received", txtChegRecivDate.Text);
        cmd.Parameters.AddWithValue("@ChargebackDisputereport", txtDisputeDate.Text);
        cmd.Parameters.AddWithValue("@Chargeback_Type", drpChageTpe.SelectedValue);
        cmd.Parameters.AddWithValue("@Disputed_Amount", txtDisbuteAmunt.Text);
        cmd.Parameters.AddWithValue("@Chargeback_Reason", drpChargebakCode.SelectedValue);
        cmd.Parameters.AddWithValue("@DocumentType ", drpDocumentType.SelectedValue);
        cmd.Parameters.AddWithValue("@Chargeback_Status", drpChrgBackStus.SelectedValue);
        cmd.Parameters.AddWithValue("@PCaseNo", txtCaseNo.Text);
        cmd.Parameters.AddWithValue("@Action", "DYNEMIC");
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        rptrEdit.DataSource = dt;
        rptrEdit.DataBind();
        ViewState["DATAEXEAL"] = dt;
    }

  
}
