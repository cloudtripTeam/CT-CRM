using HiQPdf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BookingDetail
/// </summary>
public class BookingDetail
{
    public string BookingDetailForTest()
    {
        string _Queary = string.Empty;
        DataTable dt = new DataTable();
        string _JsonDtPrice = string.Empty;
        using (SqlConnection conection = DataConnection.GetConnection())
        {
            dt = SqlHelper.ExecuteDataset(conection, CommandType.Text, "select BD.BOK_DTL_Prod_Booking_ID,BD.BOK_MST_Booking_ID,BD.BOK_DTL_Prod_PNR_Confirmation,SM.SEC_MST_Destination,BM.BOK_MST_Booking_By_Company,BD.BOK_DTL_Prod_Source_Media,BD.BOK_DTL_Prod_Supplier,BM.BOK_MST_BookingStatus,SM.SEC_MST_Ticket_IssuedBy,BD.BOK_DTL_Prod_ATOL_Type,BD.BOK_DTL_Prod_Booking_Status as 'BookingStatus',concat(PD.PAX_DTL_Title,' ',PD.PAX_DTL_Pax_First_Name,' ',PD.PAX_DTL_Pax_Last_Name) as PAXName,sum(ISNULL(TM.TRN_MST_Trns_Amount,0)) as TRN_MST_Trns_Amount,sum(ISNULL(ACD.AMT_CHG_DTL_Sell_Price,0)) as AMT_CHG_DTL_Sell_Price,sum(ISNULL(ACD.AMT_CHG_DTL_Cost_Price,0)) as AMT_CHG_DTL_Cost_Price,CONVERT(varchar(8),BD.BOK_DTL_Prod_Booking_Date_Time,11) as BDT  from Booking_Detail BD inner join Booking_Master BM on BD.BOK_MST_Booking_ID=BM.BOK_MST_Booking_ID  inner join Contact_Detail CD on BD.BOK_MST_Booking_ID=CD.BOK_MST_Booking_ID  inner join Sectors_Master SM on SM.BOK_MST_Booking_ID=BD.BOK_MST_Booking_ID inner join Amount_Charges_Detail ACD ON ACD.BOK_MST_Booking_ID=BM.BOK_MST_Booking_ID inner join Passenger_Detail PD on PD.BOK_MST_Booking_ID=BM.BOK_MST_Booking_ID left join Transaction_Master TM on TM.BOK_MST_Booking_ID=BM.BOK_MST_Booking_ID    where  CONVERT(varchar(10),BOK_DTL_Prod_Booking_Date_Time,23)=CONVERT(varchar(10),GETDATE(),23) and PD.PAX_DTL_Pax_ID=1 group by  BD.BOK_DTL_Prod_Booking_ID,BD.BOK_DTL_Prod_Booking_Date_Time,BD.BOK_MST_Booking_ID,BD.BOK_DTL_Prod_PNR_Confirmation,SM.SEC_MST_Destination,BM.BOK_MST_Booking_By_Company,BD.BOK_DTL_Prod_Source_Media,BD.BOK_DTL_Prod_Supplier,BM.BOK_MST_BookingStatus,SM.SEC_MST_Ticket_IssuedBy,BD.BOK_DTL_Prod_ATOL_Type,BD.BOK_DTL_Prod_Booking_Status,concat(PD.PAX_DTL_Title,' ',PD.PAX_DTL_Pax_First_Name,' ',PD.PAX_DTL_Pax_Last_Name),CONVERT(varchar(8),BD.BOK_DTL_Prod_Booking_Date_Time,11) order by BD.BOK_DTL_Prod_Booking_Date_Time desc").Tables[0];
            _JsonDtPrice = JsonConvert.SerializeObject(dt);
        }
        return _JsonDtPrice;
    }

    public string BookingSumary(string _XPID)
    {
        string _Queary = string.Empty;
        DataTable dt = new DataTable();
        string _JsonDtPrice = string.Empty;
        using (SqlConnection conection = DataConnection.GetConnection())
        {
            dt = SqlHelper.ExecuteDataset(conection, CommandType.Text, "select BM.BOK_MST_Booking_ID,BM.BOK_MST_Currency_Type,BD.BOK_DTL_Prod_PNR_Confirmation,BD.BOK_DTL_Prod_Product_Type,BD.BOK_DTL_Prod_Booking_Status,SM.SEC_MST_CabinClass,BM.BOK_MST_Booking_By_Company,SM.SEC_MST_Origin,BD.BOK_DTL_Prod_Source_Media,SM.SEC_MST_Destination,CD.CNT_DTL_Address,SM.SEC_MST_Journey_Type,CD.CNT_DTL_Email_Address,BD.BOK_DTL_Prod_Booking_By_Type,CD.CNT_DTL_PhoneNo,CD.CNT_DTL_MobileNo,BD.BOK_DTL_Prod_Booking_Date_Time,BD.BOK_DTL_Prod_Booking_By,BD.BOK_DTL_Prod_IP_Address,CD.CNT_DTL_City,CD.CNT_DTL_Country,BD.BOK_DTL_Prod_Booking_Remarks from Booking_Detail BD inner join Booking_Master BM on BD.BOK_MST_Booking_ID=BM.BOK_MST_Booking_ID inner join Sectors_Master SM on BD.BOK_MST_Booking_ID=SM.BOK_MST_Booking_ID inner join Contact_Detail CD on CD.BOK_MST_Booking_ID=BM.BOK_MST_Booking_ID where BM.BOK_MST_Booking_ID='"+_XPID+"'").Tables[0];
            _JsonDtPrice = JsonConvert.SerializeObject(dt);
        }
        return _JsonDtPrice;
    }

    public void generate_Pdf(string html, string xp)
    {

        // create the HTML to PDF converter
        HtmlToPdf htmlToPdfConverter = new HtmlToPdf();


        //// set browser width
        htmlToPdfConverter.BrowserWidth = int.Parse("793");

        //// set browser height if specified, otherwise use the default
        //if (textBoxBrowserHeight.Text.Length > 0)
        //    htmlToPdfConverter.BrowserHeight = int.Parse(textBoxBrowserHeight.Text);

        //// set HTML Load timeout
        //htmlToPdfConverter.HtmlLoadedTimeout = int.Parse(textBoxLoadHtmlTimeout.Text);

        // set PDF page size and orientation
        htmlToPdfConverter.Document.FitPageWidth = false;
        htmlToPdfConverter.Document.PageSize = GetSelectedPageSize();
        htmlToPdfConverter.Document.PageOrientation = PdfPageOrientation.Portrait;

        // set the PDF standard used by the document
        //htmlToPdfConverter.Document. =  PdfStandard.PdfA;

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
        "attachment", pdfBuffer.Length.ToString(), xp));

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
}