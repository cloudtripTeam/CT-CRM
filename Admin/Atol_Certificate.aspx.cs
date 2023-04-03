using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiQPdf;
using System.Data;


public partial class Admin_Atol_Certificate : System.Web.UI.Page
{
    UserDetail objUserDetail;
    DataTable dtPax = new DataTable();
    DataTable dtSector = new DataTable();
    DataTable dtSectorMaster = new DataTable();
    
    DataTable dtComp = new DataTable();
    DataTable dtContact = new DataTable();
    DataTable dtPrice = new DataTable();
    FandHServices.FandHServicesClient obj = new FandHServices.FandHServicesClient();
    Layout lot = new Layout();
    public static string Atol_Certificate { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {

        objUserDetail = Session["UserDetails"] as UserDetail;
        if (Session["UserDetails"] != null)
        {
            if (!IsPostBack)
            {

                if (!objUserDetail.isAuth("Atol_Certificate"))
                {
                    Response.Redirect("~/Admin/AccessDenied.aspx");
                    return;
                }

        if (!string.IsNullOrEmpty(Request.QueryString.Get("BID")))
        {

            string xp = Request.QueryString.Get("BID");

            BookingDetails(xp);
                    if (dtSectorMaster != null && dtSectorMaster.Rows.Count != 0)
                    {
                        if (dtSectorMaster.Rows[0]["Ticket_IssuedBy"].ToString() != "")
                        {
                            generate_Atol_Pdf(generate_Atol_Certificate(xp), xp);
                        }
                        else
                        {
                            Atol_Certificate = "Sorry, System not able to generate ATOL Certificate because Supplier Ref is not availble";

                        }
                    }
                    else {

                        Atol_Certificate = "Sorry, System not able to generate ATOL Certificate because Supplier Ref is not availble";
                    }
        }

            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }


    }

    private string generate_Atol_Certificate(string xp)

    {
        if (dtSectorMaster.Rows.Count == 0)
        {
            return "";
        }

        DataTable dtSupp = new DataTable();

        
        dtSupp = lot.Supplier(dtSectorMaster.Rows[0]["Ticket_IssuedBy"].ToString());
        string SupName = dtSupp.Rows[0]["SUP_Name"].ToString();
        string SupCode = dtSupp.Rows[0]["Sup_AtolNo"].ToString();
        // double AtolCharges = dtPax.Rows.Count * 2.5;
        double AtolCharges = 0;
        double subTotal = 0;

        foreach (DataRow drprc in dtPrice.Rows)
        {
            subTotal += (Convert.ToDouble(drprc["SellPrice"]) * Convert.ToInt32(drprc["NoOfPax"]));
        }

        CompanyDetails objc = lot.SetCompanyDetail(dtComp.Rows[0]["BookingByCompany"].ToString());

        string atol = @"<div style='background: #fffcd5; width:100%; height:100%; '><table width='1300px' border='0' align='center' cellpadding='0' cellspacing='0' style='font-family: Arial, Helvetica, sans-serif; font-size: 14px; font-weight: normal; background: #fffcd5 url(https://www.flightsandholidays.biz/images/atol-logo.png) 700px -200px no-repeat;'>" +
        "<tr>" +
            "<td style='padding: 50px;'>" +
                "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                    "<tr>" +
                        "<td align='center' valign='top' style='border: #000 solid 1px; padding: 5px 5px;'>This is an important document. Make sure that you take it with you when you travel.</td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td align='center' valign='top' style='font-size: 60px; font-weight: bold;'>ATOL Certificate</td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td align='center' valign='top' style='font-weight: bold; font-size: 18px;'>This confirms that your money is protected by the ATOL scheme<br /> if your travel company collapses.</td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td align='center' valign='top'>This certificate sets out how the ATOL scheme will protect the people<br /> named on it for the parts of their trip listed below.</td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td align='left' valign='top'>&nbsp;</td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td align='left' valign='top' style='border: #000 solid 1px; padding: 20px;'>" +
                            "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                                "<tr>" +
                                    "<td>" +
                                        "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                                            "<tr>" +
                                                "<td align='left' valign='top' style='font-weight: bold;'>Who is protected?</td>" +
                                                "<td align='right' valign='top' style='font-weight: bold;'>Number of passengers: [" + dtPax.Rows.Count + "]</td>" +
                                            "</tr>" +
                                        "</table>" +
                                    "</td>" +
                                "</tr>";
        foreach (DataRow dr in dtPax.Rows)
        {

            atol += "<tr>" +
                                       "<td style='padding: 10px 0px;'>" + dr["Title"] + " " + dr["FName"] + " " + dr["LName"] + "</td>" +
                                   "</tr>";
        
        }


        atol += "<tr>" +
                                    "<td style='font-weight: bold;'>What is protected?</td>" +
                                "</tr>";

        foreach (DataRow dr in dtSector.Rows)
        {
            atol += "<tr>" +
                                        "<td style='padding: 10px 0px; font-size: 12px;'>" + Convert.ToDateTime(dr["FromDateTime"]).ToString("dd MMM yy") + " " + dr["FromCityName"] + " (" + dr["FromDest"] + ") / " + dr["ToCityName"] + " (" + dr["ToDest"] + ") " + dr["CarierName"] + dr["FlightNo"] + "  " + dr["AirlineName"] + "</td>" +
                                    "</tr>";

        }
       

        atol += "<tr>" +
                                    "<td style='font-weight: bold;'>Who is protecting your flight?</td>" +
                                "</tr>"+
                                "<tr>"+
                                    "<td style='padding: 10px 0px;'>" + SupName + " ATOL NO " + SupCode + "  </td>" +
                                "</tr>"+
                                "<tr>"+
                                    "<td style='font-size: 18px; font-weight: bold;'>ATOL protected cost £ " + Convert.ToDouble(subTotal - AtolCharges).ToString("f2") + "</td>" +
                                "</tr>"+
                                "<tr>"+
                                    "<td align='center' style='font-size: 25px; font-weight: bold; padding: 10px 0px;'>Your protection</td>" +
                                "</tr>"+
                                "<tr>"+
                                    "<td>You are protected from when you were given this certificate until you return to the UK." +
                                        "<br />"+
                                        "<br />"+
                                        "If " + SupName + " stops trading, the passengers named above will either:<br /><br />" +

                                        "1. be returned to the UK; or<br />" +

                                        "receive a refund for the amount above (or your deposit if that is all you have paid so far).<br />" +
                                        "<br />"+


                                        "Your protection depends on the terms of the ATOL scheme (available at www.atol.org.uk)." +
"If " + SupName + "<br />" +

                                        " stops trading, you must follow the instructions at <br />www.atol.org.uk(where there will be details of arrangements to bring people back to the <br/>UK, and information on how people can claim money back). <br/>Or, you can call(+44) 333 103 6350.</td>"+
                                "</tr>"+
                                "<tr>"+
                                    "<td align='center' style='font-size: 25px; font-weight: bold; padding: 10px 0px;'>Warning</td>"+
                                "</tr>"+
                                "<tr>"+
                                    "<td>This certificate only protects the above flight/s you have booked. Any other travel services <br />you booked are not protected by this certificate.</td>"+
                                "</tr>"+
                            "</table>"+
                       " </td>"+
                    "</tr>"+
                    "<tr>"+
                        "<td align='left' valign='top'>&nbsp;</td>"+
                    "</tr>"+
                    "<tr>"+
                        "<td align='left' valign='top' style='border: #000 solid 1px; padding: 20px;'>By issuing this ATOL certificate, under Regulation 17 of the Civil Aviation (Air Travel Organisers’ Licensing) Regulations<br />" +
                        "2012, " + objc.Comp_Name + " confirms that the flight to which it applies is sold in line with the ATOL held by " +

                            "" + SupName + " <br />" +

                            "The <strong>ATOL</strong> scheme is run by the Civil Aviation Authority and paid for by the Air Travel Trust. To see what that is and what<br />" +

                            "you can expect, together with full information on its terms and conditions, go to www.atol.org.uk.</td>" +
                    "</tr>"+
                    "<tr>"+
                        "<td align='left' valign='top'>&nbsp;</td>"+
                    "</tr>"+
                    "<tr>"+
                        "<td align='left' valign='top' style='border: #000 solid 1px; padding: 20px;'>"+
                            "<table width='100%' border='1' cellspacing='0' cellpadding='0'>"+
                                "<tr>"+
                                    "<td height='30' align='center' valign='middle' style='font-weight: bold;'>Unique reference number:</td>"+
                                    "<td height='30' align='center' valign='middle' style='font-weight: bold;'>Date of issue</td>"+
                                    "<td height='30' align='center' valign='middle' style='font-weight: bold;'>ATOL Certificate Issuer:</td>"+
                                    "<td height='30' align='center' valign='middle' style='font-weight: bold;'>ATOL number:</td>"+
                                    "<td rowspan='2' align='center' valign='middle' style='text-align: center; background: #808080; font-weight: bold;'>Flight Only Sale</td>"+
                                "</tr>"+
                                "<tr>"+
                                    "<td height='30' align='center' valign='middle'>"+xp+"</td>"+
                                    "<td height='30' align='center' valign='middle'>"+DateTime.Today.ToString("dd MMM yyyy")+"</td>"+
                                    "<td height='30' align='center' valign='middle'>" + objc.Comp_Name + "</td>" +
                                    "<td height='30' align='center' valign='middle'>" + SupCode + " </td>" +
                                "</tr>"+
                            "</table>"+
                        "</td>"+
                    "</tr>"+
                    "<tr>"+
                        "<td align='right' valign='top' style='padding: 10px; font-size: 12px;'>Copyright UK Civil Aviation Authority. The ATOL Logo is a registered trade mark.</td>"+
                    "</tr>"+
                    "<tr>"+
                        "<td align='left' valign='top'>&nbsp;</td>"+
                    "</tr>"+
                "</table>"+
            "</td>"+
        "</tr>"+
    "</table></div><div style='min-height: 230px; background: #fffcd5;'></div>";
      Atol_Certificate = atol;
        return atol;
    
    }

    private void generate_Atol_Pdf(string html,string xp)
    {
        if (html != "")
        {
            // create the HTML to PDF converter
            HtmlToPdf htmlToPdfConverter = new HtmlToPdf();


            //// set browser width
            htmlToPdfConverter.BrowserWidth = int.Parse("900");

            //// set browser height if specified, otherwise use the default
            //if (textBoxBrowserHeight.Text.Length > 0)
            //    htmlToPdfConverter.BrowserHeight = int.Parse(textBoxBrowserHeight.Text);

            //// set HTML Load timeout
            //htmlToPdfConverter.HtmlLoadedTimeout = int.Parse(textBoxLoadHtmlTimeout.Text);

            // set PDF page size and orientation
            htmlToPdfConverter.Document.FitPageWidth = false;
            htmlToPdfConverter.Document.PageSize = GetSelectedPageSize();
            htmlToPdfConverter.Document.PageOrientation = PdfPageOrientation.Portrait;
            htmlToPdfConverter.Document.FitPageHeight = false;

            // set the PDF standard used by the document
            //htmlToPdfConverter.Document. =  PdfStandard.PdfA;

            // set PDF page margins
            htmlToPdfConverter.Document.Margins = new PdfMargins(-5);
           
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
            HttpContext.Current.Response.AddHeader("Content-Disposition", String.Format("{0}; filename={2}_Atol.pdf; size={1}",
                    "attachment", pdfBuffer.Length.ToString(), xp));

            // write the PDF buffer to HTTP response
            HttpContext.Current.Response.BinaryWrite(pdfBuffer);

            // call End() method of HTTP response to stop ASP.NET page processing
            HttpContext.Current.Response.End();
        }
        else
        {
            Response.Write("Sorry, system unable to generate ATOL certificate, please check a ticket supplier added with booking.");
        }

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

    public void BookingDetails(string XP)
    {
        dtPax = obj.GET_Passenger_Detail(XP, "001");
        dtSectorMaster = obj.GET_Sectors_Master(XP, "001");

        dtSector = obj.GET_Sector_Detail(XP, "001");
        dtComp = obj.GET_Booking_Master(XP);
        dtContact = obj.GET_Contact_Detail(XP, "001");
        dtPrice = obj.GET_Amount_Charges_Detail(XP, "001", "", "", "", "", "", "", "");
        
    }

}