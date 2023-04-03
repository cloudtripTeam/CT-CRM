using HiQPdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ATOL_Report_Summary : System.Web.UI.Page
{
    string fromDate = string.Empty; string toDate = string.Empty;
    GetSetDatabase db = new GetSetDatabase();
    public string output = string.Empty;


    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserDetails"] != null)
        {

            if (!IsPostBack)
            {
                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
                CommanBinding.BindCompanyDetails(ref ddlCompany, objUserDetail.userID);
                if (!objUserDetail.isAuth("Atol_Report"))
                {
                    Response.Redirect("Default.aspx");
                    return;
                }
                else
                {



                    string role = objUserDetail.userRole.ToLower();
                    #region set Quaterly Date
                    btnfive.Text = "Fourth Quarter";
                    fromDate = "01-01-" + DateTime.Today.Year;
                    toDate = "31-03-" + DateTime.Today.Year;                   
                    Literal1.Text = "(" + fromDate + " to " + toDate + ")";

                    fromDate = "01-01-" + DateTime.Today.AddYears(1).Year;
                    toDate = "31-03-" + DateTime.Today.AddYears(1).Year;
                    ltFourth.Text = "(" + fromDate + " - " + toDate + ")";

                    fromDate = "01-04-" + DateTime.Now.Year;
                    toDate = "30-06-" + DateTime.Now.Year;
                    ltFirst.Text = "(" + fromDate + " - " + toDate + ")";

                    fromDate = "01-07-" + DateTime.Now.Year;
                    toDate = "30-09-" + DateTime.Now.Year;
                    ltSecond.Text = "(" + fromDate + " - " + toDate + ")";

                    fromDate = "01-10-" + DateTime.Now.Year;
                    toDate = "31-12-" + DateTime.Now.Year;
                    ltThird.Text = "(" + fromDate + " - " + toDate + ")";


                
                    #endregion




                }
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }


        
    }

    protected void btnfirst_Click(object sender, EventArgs e)
    {
        fromDate = DateTime.Now.Year + "-04-01";
        toDate = DateTime.Now.Year + "-06-30";

        DataTable dtTurnOver = db.GET_Atol_Report("", "", fromDate, Convert.ToDateTime(toDate).AddDays(1).ToString("yyyy-MM-dd"), CommanBinding.GetCompanyCodes(ddlCompany), "issued,completed,ETicket Sent", "", "", "", "10950");
        DataTable dtTurnOverbyDepartDate = db.GET_Atol_Report("", "", "", toDate, CommanBinding.GetCompanyCodes(ddlCompany), "issued,completed,ETicket Sent", fromDate, toDate, "", "10950");
        DataTable dtTurnOverbyDepartDueDate = db.GET_Atol_Report("", "", "", toDate, CommanBinding.GetCompanyCodes(ddlCompany), "issued,completed,ETicket Sent", Convert.ToDateTime(toDate).AddDays(1).ToString("yyyy-MM-dd"), "", "", "10950");
        GenerateAtolSummary(dtTurnOver, dtTurnOverbyDepartDate, dtTurnOverbyDepartDueDate, fromDate, toDate);
        ViewState["dtTurnOver"] = dtTurnOver;
        ViewState["dtTurnOverbyDepartDate"] = dtTurnOverbyDepartDate;
        ViewState["dtTurnOverbyDepartDueDate"] = dtTurnOverbyDepartDueDate;
        ViewState["fromDate"] = fromDate;
        ViewState["toDate"] = toDate;
    }

    protected void btnsecond_Click(object sender, EventArgs e)
    {
        fromDate = DateTime.Now.Year + "-07-01";
        toDate = DateTime.Now.Year + "-09-30";
        DataTable dtTurnOver = db.GET_Atol_Report("", "", fromDate, Convert.ToDateTime(toDate).AddDays(1).ToString("yyyy-MM-dd"), CommanBinding.GetCompanyCodes(ddlCompany), "issued,completed,ETicket Sent", "", "", "", "10950");
        DataTable dtTurnOverbyDepartDate = db.GET_Atol_Report("", "", "", toDate, CommanBinding.GetCompanyCodes(ddlCompany), "issued,completed,ETicket Sent", fromDate, toDate, "", "10950");
        DataTable dtTurnOverbyDepartDueDate = db.GET_Atol_Report("", "", "", toDate, CommanBinding.GetCompanyCodes(ddlCompany), "issued,completed,ETicket Sent", Convert.ToDateTime(toDate).AddDays(1).ToString("yyyy-MM-dd"), "", "", "10950");
        GenerateAtolSummary(dtTurnOver, dtTurnOverbyDepartDate, dtTurnOverbyDepartDueDate, fromDate, toDate);
        ViewState["dtTurnOver"] = dtTurnOver;
        ViewState["dtTurnOverbyDepartDate"] = dtTurnOverbyDepartDate;
        ViewState["dtTurnOverbyDepartDueDate"] = dtTurnOverbyDepartDueDate;
        ViewState["fromDate"] = fromDate;
        ViewState["toDate"] = toDate;

    }

    protected void btnthird_Click(object sender, EventArgs e)
    {
        fromDate = DateTime.Now.Year + "-10-01";
        toDate = DateTime.Now.Year + "-12-31";
        DataTable dtTurnOver = db.GET_Atol_Report("", "", fromDate, Convert.ToDateTime(toDate).AddDays(1).ToString("yyyy-MM-dd"), CommanBinding.GetCompanyCodes(ddlCompany), "issued,completed,ETicket Sent", "", "", "", "10950");
        DataTable dtTurnOverbyDepartDate = db.GET_Atol_Report("", "", "", toDate, CommanBinding.GetCompanyCodes(ddlCompany), "issued,completed,ETicket Sent", fromDate, toDate, "", "10950");
        DataTable dtTurnOverbyDepartDueDate = db.GET_Atol_Report("", "", "", toDate, CommanBinding.GetCompanyCodes(ddlCompany), "issued,completed,ETicket Sent", Convert.ToDateTime(toDate).AddDays(1).ToString("yyyy-MM-dd"), "", "", "10950");
        GenerateAtolSummary(dtTurnOver, dtTurnOverbyDepartDate, dtTurnOverbyDepartDueDate, fromDate, toDate);
        ViewState["dtTurnOver"] = dtTurnOver;
        ViewState["dtTurnOverbyDepartDate"] = dtTurnOverbyDepartDate;
        ViewState["dtTurnOverbyDepartDueDate"] = dtTurnOverbyDepartDueDate;
        ViewState["fromDate"] = fromDate;
        ViewState["toDate"] = toDate;
    }

    protected void btnfourth_Click(object sender, EventArgs e)
    {
        fromDate = DateTime.Today.AddYears(1).Year + "-01-01";
        toDate = DateTime.Today.AddYears(1).Year + "-03-31";
        DataTable dtTurnOver = db.GET_Atol_Report("", "", fromDate, Convert.ToDateTime(toDate).AddDays(1).ToString("yyyy-MM-dd"), CommanBinding.GetCompanyCodes(ddlCompany), "issued,completed,ETicket Sent", "", "", "", "10950");
        DataTable dtTurnOverbyDepartDate = db.GET_Atol_Report("", "", "", toDate, CommanBinding.GetCompanyCodes(ddlCompany), "issued,completed,ETicket Sent", fromDate, toDate, "", "10950");
        DataTable dtTurnOverbyDepartDueDate = db.GET_Atol_Report("", "", "", toDate, CommanBinding.GetCompanyCodes(ddlCompany), "issued,completed,ETicket Sent", Convert.ToDateTime(toDate).AddDays(1).ToString("yyyy-MM-dd"), "", "", "10950");
        GenerateAtolSummary(dtTurnOver, dtTurnOverbyDepartDate, dtTurnOverbyDepartDueDate, fromDate, toDate);
        ViewState["dtTurnOver"] = dtTurnOver;
        ViewState["dtTurnOverbyDepartDate"] = dtTurnOverbyDepartDate;
        ViewState["dtTurnOverbyDepartDueDate"] = dtTurnOverbyDepartDueDate;
        ViewState["fromDate"] = fromDate;
        ViewState["toDate"] = toDate;

    }

    private void GenerateAtolSummary(DataTable dtTurnOverByBooking, DataTable dtTurnOverByDepartDate, DataTable dtTurnOverbyDepartDueDate, string fromdate, string todate)
    {
        string atol = "10950";
        object totalPax = null; object totalSale = null; object totalPaxDepart = null; object totalSaleDepart = null;
        object totalPaxDepartDue = null; object totalSaleDepartDue = null;
        #region Filter Atol Type && Booking Date
        DataTable copydt = dtTurnOverByBooking.Copy();
        if (copydt != null && copydt.Rows.Count > 0)
        {
            string atoltype = "ATOL_Type = 'Flights Only/PUBLIC BONDED'";
            DataRow[] dr = copydt.Select(atoltype);
            if (dr != null && dr.Count() > 0)
            {
                copydt = dr.CopyToDataTable();
               
                totalPax = copydt.Compute("Sum(NoOfPax)", string.Empty);

                
                totalSale = copydt.Compute("Sum(Sell_Price)", string.Empty);
               
            }
            else
            {
                copydt = null;
            }

        }
       // ExcelExport(copydt);
        #endregion


        #region Fliter Atol type && Departure date
        DataTable copydtDpt = dtTurnOverByDepartDate.Copy();
        if (copydtDpt != null && copydtDpt.Rows.Count > 0)
        {
            string atoltype = "ATOL_Type = 'Flights Only/PUBLIC BONDED'";
            DataRow[] dr = copydtDpt.Select(atoltype);
            if (dr != null && dr.Count() > 0)
            {
                copydtDpt = dr.CopyToDataTable();

                totalPaxDepart = copydtDpt.Compute("Sum(NoOfPax)", string.Empty);


                totalSaleDepart = copydtDpt.Compute("Sum(Sell_Price)", string.Empty);
            }
            else
            {
                copydtDpt = null;
            }

        }

        #endregion

        #region Fliter Atol type && due Departure date

        DataTable copydtDptdue = dtTurnOverbyDepartDueDate.Copy();
        if (copydtDptdue != null && copydtDptdue.Rows.Count > 0)
        {
            string atoltype = "ATOL_Type = 'Flights Only/PUBLIC BONDED'";
            DataRow[] dr = copydtDptdue.Select(atoltype);
            if (dr != null && dr.Count() > 0)
            {
                copydtDptdue = dr.CopyToDataTable();

                totalPaxDepartDue = copydtDptdue.Compute("Sum(NoOfPax)", string.Empty);


                totalSaleDepartDue = copydtDptdue.Compute("Sum(Sell_Price)", string.Empty);
            }
            else
            {
                copydtDptdue = null;
            }

        }

        #endregion



        output = "<table width = '1000' border = '0' align = 'center' cellpadding = '0' cellspacing = '0' style = 'font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:normal; background:url(atol-logo.png) center 200px no-repeat; border:#666 solid 1px;'>" +
              "<tr>" +
                "<td style='padding:30px;'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
              "<tr>" +
                "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                  "<tr>" +
                    "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                      "<tr>" +
                        "<td width='100' align='left' valign='top'><strong>ATOL Holder:</strong>" +
                        "</td>" +
            "<td width='20' align='left' valign='top'>&nbsp;</td>" +
            "<td align='left' valign='top' style='border-bottom: dotted 1px; padding-bottom:5px;'>Flights & Holidays UK Ltd</td>" +
            "<td width='20' align='left' valign='top'>&nbsp;</td>" +
            "<td width='100' align='left' valign='top'><strong>ATOL No:</strong></td>" +
            "<td width='20' align='left' valign='top'>&nbsp;</td>" +
            "<td align='left' valign='top' style='border-bottom: dotted 1px; padding-bottom:5px;'>10950</td>" +
          "</tr>" +
        "</table ></td>" +
      "</tr>" +
      "<tr>" +
      "<td> &nbsp;</td>" +
         "</tr>" +
         "<tr>" +
           "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
             "<tr>" +
               "<td width='100' align='left' valign='top'>Period From:</td>" +
               "<td width='20' align='left' valign='top'>&nbsp;</td>" +
               "<td align='left' valign='top' style='border-bottom: dotted 1px; padding-bottom:5px;'>"+ Convert.ToDateTime(fromdate).ToString("dd/MM/yyyy") + "</td>" +
               "<td width='20' align='left' valign='top'>&nbsp;</td>" +
               "<td align='left' valign='top'>To:</td>" +
               "<td width='20' align='left' valign='top'>&nbsp;</td>" +
               "<td align='left' valign='top' style='border-bottom: dotted 1px; padding-bottom:5px;'>"+ Convert.ToDateTime(todate).ToString("dd/MM/yyyy") + "</td>" +
               "<td width='300' align='left' valign='top'>&nbsp;</td>" +
             "</tr>" +
             "<tr>" +
               "<td width='100' align='left' valign='top'>&nbsp;</td>" +
               "<td width='20' align='left' valign='top'>&nbsp;</td>" +
               "<td align='left' valign='top' style='font-size:11px; font-style:italic;'>Start Date (dd/mm/yyyy)</td>" +
               "<td width='20' align='left' valign='top'>&nbsp;</td>" +
               "<td align='left' valign='top'>&nbsp;</td>" +
               "<td width='20' align='left' valign='top'>&nbsp;</td>" +
               "<td align='left' valign='top' style='font-size:11px; font-style:italic;'>End Date (dd/mm/yyyy)</td>" +
               "<td width='300' align='left' valign='top'>&nbsp;</td>" +
             "</tr>" +
           "</table></td> " +
         "</tr>" +
       "</table></td>" +
     "</tr>" +
     "<tr>" +
       "<td align='left' valign='top'>&nbsp;</td>" +
     "</tr>" +
     "<tr>" +
       "<td align='left' valign='top' style='border:#999  solid 1px;'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
         "<tr>" +
           "<td align='left' valign='top' style='background:#28303d; padding:10px 10px; color:#FFF;'>PART A- LICENSABLE PASSENGER NUMBERS AND RELATED REVENUE ON A BOOKING DATE BASIS</td>" +
         "</tr>" +
         "<tr>" +
           "<td align='left' valign='top' style='padding:10px 20px;'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
             "<tr>" +
               "<td align='left' valign='top'>Complete the number of <strong>passengers booked</strong> during the period set out above. If any of the types of Public Sales do not apply to you insert a zero in the relevant boxes. If the number of passengers booked is nil a ZERO return is still required.</td>" +
             "</tr>" +
             "<tr>" +
               "<td align='left' valign='top' style='padding:20px 0px;'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                 "<tr>" +
                   "<td height='30' align='left' valign='middle'><strong>Sub Category Of Business</strong></td>" +
                   "<td width='30' height='30' align='left' valign='middle'>&nbsp;</td>" +
                   "<td height='30' align='left' valign='middle'><strong>Passengers Booked*</strong></td>" +
                   "<td width='30' height='30' align='left' valign='middle'>&nbsp;</td>" +
                   "<td height='30' align='left' valign='middle'><strong>Revenue (Gross invoice Value)</strong></td>" +

                 "</tr>" +

                 "<tr>" +
                   "<td height='30' align='left' valign='middle'>Flight-Only</td>" +
                   "<td width='30' height='30' align='left' valign='middle'>&nbsp;</td>" +
                   "<td height='30' align='left' valign='middle' style='border-bottom: dotted 1px; padding-bottom:5px;'>"+ Convert.ToString(totalPax)+"</td>" +
                   "<td width='30' height='30' align='left' valign='middle'>&nbsp;</td>" +
                   "<td height='30' align='left' valign='middle' style='border-bottom: dotted 1px; padding-bottom:5px;'>"+ Convert.ToString(totalSale)+"</td>" +

                 "</tr>" +

                 "<tr>" +
                   "<td height='30' align='left' valign='middle'>Flight inclusive package</td>" +
                   "<td width='30' height='30' align='left' valign='middle'>&nbsp;</td>" +
                   "<td height='30' align='left' valign='middle' style='border-bottom: dotted 1px; padding-bottom:5px;'>0</td>" +
                   "<td width='30' height='30' align='left' valign='middle'>&nbsp;</td>" +
                   "<td height='30' align='left' valign='middle' style='border-bottom: dotted 1px; padding-bottom:5px;'>0</td>" +

                 "</tr>" +

                 "<tr>" +
                   "<td height='30' align='left' valign='middle'>Flight- Plus</td>" +
                   "<td width='30' height='30' align='left' valign='middle'>&nbsp;</td>" +
                   "<td height='30' align='left' valign='middle' style='border-bottom: dotted 1px; padding-bottom:5px;'>0</td>" +
                   "<td width='30' height='30' align='left' valign='middle'>&nbsp;</td>" +
                   "<td height='30' align='left' valign='middle' style='border-bottom: dotted 1px; padding-bottom:5px;'>0</td>" +

                 "</tr>" +

                 "<tr>" +
                   "<td height='30' align='left' valign='middle'>Total Public Sales Category</td>" +
                   "<td width='30' height='30' align='left' valign='middle'>&nbsp;</td>" +
                   "<td height='30' align='left' valign='middle' style='border-bottom: dotted 1px; padding-bottom:5px;'>" + Convert.ToString(totalPax) + "</td>" +
                   "<td width='30' height='30' align='left' valign='middle'>&nbsp;</td>" +
                   "<td height='30' align='left' valign='middle' style='border-bottom: dotted 1px; padding-bottom:5px;'>" + Convert.ToString(totalSale) + "</td>" +
                 "</tr>" +
               "</table></td> " +
             "</tr>" +
             "<tr>" +
               "<td align='left' valign='top' style='font-size:12px;'>* The figure for passengers booked should include all bookings taken during the period and<strong> cancellations should not be deducted.</strong> The number of passengers entered in the Total Public Sales category box will be used to calculate the ATOL Protection Contribution (APC) payable.</td>" +
             "</tr>" +
           "</table></td>" +
         "</tr>" +
       "</table></td>" +
     "</tr>" +
   "  <tr>" +
       "<td align='left' valign='top'>&nbsp;</td>" +
     "</tr>" +
   "  <tr>" +
       "<td align='left' valign='top' style='border:#999  solid 1px;'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
         "<tr>" +
           "<td align='left' valign='top' style='background:#28303d; padding:10px 10px; color:#FFF;'>PART B- LICENSABLE PASSENGER NUMBERS AND RELATED REVENUE ON A DEPARTURE DATE BASIS</td>" +
         "</tr>" +
         "<tr>" +
           "<td align='left' valign='top' style='padding:10px 20px;'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
          "<tr>" +
            "<td align='left' valign='top'>Complete the number of <strong>passengers departed</strong> during the period set out above. If any of the types of public sales do not apply to you insert a zero in the relevant boxes. If the number of passengers departed is nil a ZERO return is still required.</td>" +
          "</tr>" +
"<tr>" +
            "<td align='left' valign='top' style='padding:20px 0px;'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
              "<tr>" +
                "<td height='30' align='left' valign='middle'><strong>Sub Category Of Business</strong></td>" +
                "<td width='30' height='30' align='left' valign='middle'>&nbsp;</td>" +
                "<td height='30' align='left' valign='middle'><strong>Passengers Departed</strong></td>" +
                "<td width='30' height='30' align='left' valign='middle'>&nbsp;</td>" +
                "<td height='30' align='left' valign='middle'><strong>Revenue (Gross invoice Value)</strong></td>" +
              "</tr>" +
              "<tr>" +
                "<td height='30' align='left' valign='middle'>Flight-Only</td>" +
                "<td width='30' height='30' align='left' valign='middle'>&nbsp;</td>" +
                "<td height='30' align='left' valign='middle' style='border-bottom: dotted 1px; padding-bottom:5px;'>"+ totalPaxDepart.ToString() + "</td>" +
                "<td width='30' height='30' align='left' valign='middle'>&nbsp;</td>" +
                "<td height='30' align='left' valign='middle' style='border-bottom: dotted 1px; padding-bottom:5px;'>" + totalSaleDepart.ToString() + "</td>" +
              "</tr>" +
              "<tr>" +
                "<td height='30' align='left' valign='middle'>Flight inclusive package</td>" +
                "<td width='30' height='30' align='left' valign='middle'>&nbsp;</td>" +
                "<td height='30' align='left' valign='middle' style='border-bottom: dotted 1px; padding-bottom:5px;'>0</td>" +
                "<td width='30' height='30' align='left' valign='middle'>&nbsp;</td>" +
                "<td height='30' align='left' valign='middle' style='border-bottom: dotted 1px; padding-bottom:5px;'>0</td>" +
              "</tr>" +
              "<tr>" +
                "<td height='30' align='left' valign='middle'>Flight- Plus</td>" +
                "<td width='30' height='30' align='left' valign='middle'>&nbsp;</td>" +
                "<td height='30' align='left' valign='middle' style='border-bottom: dotted 1px; padding-bottom:5px;'>0</td>" +
                "<td width='30' height='30' align='left' valign='middle'>&nbsp;</td>" +
                "<td height='30' align='left' valign='middle' style='border-bottom: dotted 1px; padding-bottom:5px;'>0</td>" +
              "</tr>" +
              "<tr>" +
                "<td height='30' align='left' valign='middle'>Total Public Sales Category</td>" +
                "<td width='30' height='30' align='left' valign='middle'>&nbsp;</td>" +
                "<td height='30' align='left' valign='middle' style='border-bottom: dotted 1px; padding-bottom:5px;'>" + totalPaxDepart.ToString() + "</td>" +
                "<td width='30' height='30' align='left' valign='middle'>&nbsp;</td>" +
                "<td height='30' align='left' valign='middle' style='border-bottom: dotted 1px; padding-bottom:5px;'>" + totalSaleDepart.ToString() + "</td>" +
              "</tr>" +
              "<tr>" +
                "<td height='30' align='left' valign='middle'>ATOL to ATOL (External Only)</td>" +
                "<td height='30' align='left' valign='middle'>&nbsp;</td>" +
                "<td height='30' align='left' valign='middle' style='border-bottom: dotted 1px; padding-bottom:5px;'>0</td>" +
                "<td height='30' align='left' valign='middle'>&nbsp;</td>" +
                "<td height='30' align='left' valign='middle'>&nbsp;</td>" +
              "</tr>" +

            "</table></td>" +
          "</tr>" +
          "<tr>" +
            "<td align='left' valign='top' style='font-size:12px;'>**Licensable transaction constituting seats sold to another ATOL holder(whether as a seat only or as part of a package), other than ATOL holder in the same Group, for resale under the buying ATOL holder's licence. </td>" +
          "</tr>" +
        "</table ></td> " +
      "</tr>" +
    "</table></td>" +
  "</tr>" +
  "<tr>" +
    "<td align='left' valign='top'>&nbsp;</td>" +
  "</tr>" +


  "<tr>" +
    "<td align='left' valign='top' style='border:#999  solid 1px;'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
      "<tr>" +
        "<td align='left' valign='top' style='background:#28303d; padding:10px 10px; color:#FFF;'>PART C- LICENSABLE PASSENGERS DUE TO DEPART</td>" +
      "</tr>" +
      "<tr>" +
        "<td align='left' valign='top' style='padding:10px 20px;'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
          "<tr>" +
            "<td align='left' valign='top'>In the boxes below, enter the total number of passengers booked to depart after the period specified at the top of the Form. If any of the types of Public Sales do not apply to you insert a zero in the relevant boxes.</td>" +
          "</tr>" +
          "<tr>" +
            "<td align='left' valign='top' style='padding: 20px 0px; '><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
              "<tr>" +
                "<td height='30' align='left' valign='middle'><strong>Sub Category Of Business</strong></td>" +
                "<td width='30' height='30' align='left' valign='middle'>&nbsp;</td>" +
                "<td height='30' align='left' valign='middle'><strong>Passengers Due to Depart</strong></td>" +
                "<td width='30' height='30' align='left' valign='middle'>&nbsp;</td>" +
                "<td height='30' align='left' valign='middle'><strong>Revenue (Gross invoice Value)</strong></td>" +
              "</tr>" +
              "<tr>" +
                "<td height='30' align='left' valign='middle'>Flight-Only</td>" +
                "<td width='30' height='30' align='left' valign='middle'>&nbsp;</td>" +
                "<td height='30' align='left' valign='middle' style='border - bottom: dotted 1px; padding - bottom:5px;'>"+ totalPaxDepartDue.ToString() + "</td>" +
                "<td width='30' height='30' align='left' valign='middle'>&nbsp;</td>" +
                "<td height='30' align='left' valign='middle' style='border - bottom: dotted 1px; padding - bottom:5px;'>"+totalSaleDepartDue.ToString()+"</td>" +
              "</tr>" +
              "<tr>" +
                "<td height='30' align='left' valign='middle'>Flight inclusive package</td>" +
                "<td width='30' height='30' align='left' valign='middle'>&nbsp;</td>" +
                "<td height='30' align='left' valign='middle' style='border - bottom: dotted 1px; padding - bottom:5px; '>0</td>" +
                "<td width='30' height='30' align='left' valign='middle'>&nbsp;</td>" +
                "<td height='30' align='left' valign='middle' style='border - bottom: dotted 1px; padding - bottom:5px; '>0</td>" +
              "</tr>" +
              "<tr>" +
                "<td height='30' align='left' valign='middle'>Flight- Plus</td>" +
                "<td width='30' height='30' align='left' valign='middle'>&nbsp;</td>" +
                "<td height='30' align='left' valign='middle' style='border - bottom: dotted 1px; padding - bottom:5px; '>0</td>" +
                "<td width='30' height='30' align='left' valign='middle'>&nbsp;</td>" +
                "<td height='30' align='left' valign='middle' style='border - bottom: dotted 1px; padding - bottom:5px; '>0</td>" +
                " </tr>" +
                "<tr>" +
                "<td height='30' align='left' valign='middle'>Total Public Sales Category</td>" +
                "<td width='30' height='30' align='left' valign='middle'>&nbsp;</td>" +
                "<td height='30' align='left' valign='middle' style='border - bottom: dotted 1px; padding - bottom:5px; '>" + totalPaxDepartDue.ToString() + "</td>" +
                "<td width='30' height='30' align='left' valign='middle'>&nbsp;</td>" +
                "<td height='30' align='left' valign='middle' style='border - bottom: dotted 1px; padding - bottom:5px;'>"+ totalSaleDepartDue.ToString() + "</td>" +
              "</tr>" +
            "</table></td>" +
          "</tr>" +
        "</table></td>" +
      "</tr>" +
    "</table></td>" +
  "</tr>" +


"</table>" +
"</td>" +
  "</tr>" +
"</table>";

        if (output != "")
            btnPdf.Visible = true;
        else
            btnPdf.Visible = false;
        // return output;
    }

    private void generate_Pdf(string html, string xp)
    {

        // create the HTML to PDF converter
        HtmlToPdf htmlToPdfConverter = new HtmlToPdf();


        //// set browser width
        htmlToPdfConverter.BrowserWidth = int.Parse("793");


        // set PDF page size and orientation
        htmlToPdfConverter.Document.FitPageWidth = false;
        htmlToPdfConverter.Document.PageSize = GetSelectedPageSize();
        htmlToPdfConverter.Document.PageOrientation = PdfPageOrientation.Portrait;

        

        // set PDF page margins
        htmlToPdfConverter.Document.Margins = new PdfMargins(5);


        // set a wait time before starting the conversion
        htmlToPdfConverter.WaitBeforeConvert = 2;

        // convert HTML to PDF
        byte[] pdfBuffer = null;


        // convert HTML code
        string htmlCode = html;
        string baseUrl = "";

        // convert HTML code to a PDF memory buffer
        pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(htmlCode, baseUrl);


        // inform the browser about the binary data format
        HttpContext.Current.Response.AddHeader("Content-Type", "application/pdf");

        // let the browser know how to open the PDF document, attachment or inline, and the file name
        HttpContext.Current.Response.AddHeader("Content-Disposition", String.Format("{0}; filename={2}.pdf; size={1}",
             "attachment", pdfBuffer.Length.ToString(), "Atol_Summary"));

        // write the PDF buffer to HTTP response
        HttpContext.Current.Response.BinaryWrite(pdfBuffer);

        // call End() method of HTTP response to stop ASP.NET page processing
        HttpContext.Current.Response.End();

    }

    private PdfPageSize GetSelectedPageSize()
    {
        switch ("A3")
        {
            case "A0":
                return PdfPageSize.A0;
            case "A1":
                return PdfPageSize.A1;
            case "A10":
                return PdfPageSize.A10;
            case "A2":
                return PdfPageSize.A2;
            case "A3":
                return PdfPageSize.A3;
            case "A4":
                return PdfPageSize.A4;
            case "A5":
                return PdfPageSize.A5;
            case "A6":
                return PdfPageSize.A6;
            case "A7":
                return PdfPageSize.A7;
            case "A8":
                return PdfPageSize.A8;
            case "A9":
                return PdfPageSize.A9;
            case "ArchA":
                return PdfPageSize.ArchA;
            case "ArchB":
                return PdfPageSize.ArchB;
            case "ArchC":
                return PdfPageSize.ArchC;
            case "ArchD":
                return PdfPageSize.ArchD;
            case "ArchE":
                return PdfPageSize.ArchE;
            case "B0":
                return PdfPageSize.B0;
            case "B1":
                return PdfPageSize.B1;
            case "B2":
                return PdfPageSize.B2;
            case "B3":
                return PdfPageSize.B3;
            case "B4":
                return PdfPageSize.B4;
            case "B5":
                return PdfPageSize.B5;
            case "Flsa":
                return PdfPageSize.Flsa;
            case "HalfLetter":
                return PdfPageSize.HalfLetter;
            case "Ledger":
                return PdfPageSize.Ledger;
            case "Legal":
                return PdfPageSize.Legal;
            case "Letter":
                return PdfPageSize.Letter;
            case "Letter11x17":
                return PdfPageSize.Letter11x17;
            case "Note":
                return PdfPageSize.Note;
            default:
                return PdfPageSize.A4;
        }
    }

    private void ExcelExport(DataTable dt)
    {
       // DataTable dt = (DataTable)ViewState["AtolByBookingDate"];
        if (dt != null)
        {


            string attachment = "attachment; filename=" + "Atol_" + DateTime.Now.ToString("ddMMMyyyy") + ".xls";
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

    protected void btnPdf_Click(object sender, EventArgs e)
    {
      DataTable dtTurnOver = (DataTable) ViewState["dtTurnOver"];
        DataTable dtTurnOverbyDepartDate = (DataTable)ViewState["dtTurnOverbyDepartDate"];
        DataTable dtTurnOverbyDepartDueDate = (DataTable)ViewState["dtTurnOverbyDepartDueDate"];
        fromDate = (string)ViewState["fromDate"];
        toDate = (string)ViewState["toDate"];
        GenerateAtolSummary(dtTurnOver, dtTurnOverbyDepartDate, dtTurnOverbyDepartDueDate,fromDate,toDate);
        generate_Pdf(output, "");
    }

    protected void btnfive_Click(object sender, EventArgs e)
    {
        fromDate = DateTime.Today.Year + "-01-01";
        toDate = DateTime.Today.Year + "-03-31";
       
        DataTable dtTurnOver = db.GET_Atol_Report("", "", fromDate, Convert.ToDateTime(toDate).AddDays(1).ToString("yyyy-MM-dd"), CommanBinding.GetCompanyCodes(ddlCompany), "issued,completed,ETicket Sent", "", "", "", "10950");
        DataTable dtTurnOverbyDepartDate = db.GET_Atol_Report("", "", "", toDate, CommanBinding.GetCompanyCodes(ddlCompany), "issued,completed,ETicket Sent", fromDate, toDate, "", "10950");
        DataTable dtTurnOverbyDepartDueDate = db.GET_Atol_Report("", "", "", toDate, CommanBinding.GetCompanyCodes(ddlCompany), "issued,completed,ETicket Sent", Convert.ToDateTime(toDate).AddDays(1).ToString("yyyy-MM-dd"), "", "", "10950");
        GenerateAtolSummary(dtTurnOver, dtTurnOverbyDepartDate, dtTurnOverbyDepartDueDate, fromDate, toDate);
        ViewState["dtTurnOver"] = dtTurnOver;
        ViewState["dtTurnOverbyDepartDate"] = dtTurnOverbyDepartDate;
        ViewState["dtTurnOverbyDepartDueDate"] = dtTurnOverbyDepartDueDate;
        ViewState["fromDate"] = fromDate;
        ViewState["toDate"] = toDate;
        
    }

    protected void btnCustome_Click(object sender, EventArgs e)
    {
        fromDate = txtDateFrom.Text;
        toDate = txtDateTo.Text;

        DataTable dtTurnOver = db.GET_Atol_Report("", "", fromDate, Convert.ToDateTime(toDate).AddDays(1).ToString("yyyy-MM-dd"), CommanBinding.GetCompanyCodes(ddlCompany), "issued,completed,ETicket Sent", "", "", "", "10950");
        DataTable dtTurnOverbyDepartDate = db.GET_Atol_Report("", "", "", toDate, CommanBinding.GetCompanyCodes(ddlCompany), "issued,completed,ETicket Sent", fromDate, toDate, "", "10950");
        DataTable dtTurnOverbyDepartDueDate = db.GET_Atol_Report("", "", "", Convert.ToDateTime(toDate).AddDays(1).ToString("yyyy-MM-dd"), CommanBinding.GetCompanyCodes(ddlCompany), "issued,completed,ETicket Sent", Convert.ToDateTime(toDate).AddDays(1).ToString("yyyy-MM-dd"), "", "", "10950");
        GenerateAtolSummary(dtTurnOver, dtTurnOverbyDepartDate, dtTurnOverbyDepartDueDate, fromDate, toDate);
        ViewState["dtTurnOver"] = dtTurnOver;
        ViewState["dtTurnOverbyDepartDate"] = dtTurnOverbyDepartDate;
        ViewState["dtTurnOverbyDepartDueDate"] = dtTurnOverbyDepartDueDate;
        ViewState["fromDate"] = fromDate;
        ViewState["toDate"] = toDate;
    }
}