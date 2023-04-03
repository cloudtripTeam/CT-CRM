using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for eTicketLayout
/// </summary>
public class eTicketLayout : Layout
{
    public eTicketLayout()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string Eticket_Heder { get; set; }
    public string Eticket_Footer { get; set; }
    public string FeedbackLink { get; set; }
    public string BrandName { get; set; }
    public string Email { get; set; }
    public string phone { get; set; }
    FandHServices.FandHServicesClient obj = new FandHServices.FandHServicesClient();

    public string Eticket_HeaderLayout(string comp)
    {
        CompanyDetails objc = SetCompanyDetail(comp);
        BrandName = objc.Brand_Name;
        Email = objc.Comp_Emailid;
        phone = objc.Comp_contact;
        Eticket_FooterLayout(objc);
        Eticket_Feedback(objc);

        //Change by Dinesh 
        //13 Oct 2020
        //Change by Dinesh  13 Oct 2020 Again Change By Dinesh Change Timings according to Website || By Jack 
        if (comp.ToUpper() == "AIRXPERTCA" || comp.ToUpper() == "C2BCA" ||
            comp.ToUpper() == "CALL4CHEAP" || comp.ToUpper() == "FLTTROTT_CA" ||
            comp.ToUpper() == "TPCA" || comp.ToUpper() == "TRVCART" || comp.ToUpper() == "TRVJUNCTION_CA")
        {
            Eticket_Heder = @"<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                "<tr>" +
                    "<td align='left' valign='middle'>" +
                        // "<img  src='" + BLL.WebsiteStaticData.WebsiteUrl + "images/" + objc.Comp_atol + "' width='' height='57' alt='atol' />" +
                        "</td>" +
                    "<td align='center' valign='middle'><a href='#'>" +
                        "<img  src='" + BLL.WebsiteStaticData.WebsiteUrl + "images/logoes/" + objc.Comp_logo + ".png' alt='Logo' width='200' border='0' /></a></td>" +
                    "<td align='left' valign='middle'>" +
                        "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                            "<tr>" +
                                "<td style='font-family: Arial, Helvetica, sans-serif; font-size: 20px; color: #FFF;'>Call <span><a href='#' style='color: #efbf00; text-decoration: none;'>" + objc.Comp_contact + "</a></span></td>" +
                            "</tr>" +
                            "<tr>" +
                                "<td style='font-family: Arial, Helvetica, sans-serif; font-size: 11px; color: #FFF;'>Opening Time: 0800 till 2230 All days </td>" +
                            "</tr>" +
                        "</table>" +
                    "</td>" +
                "</tr>" +
            "</table>";
        }
        //USA 
        else if (comp.ToUpper() == "C2BUS" || comp.ToUpper() == "DIAL4TRVUS" ||
                 comp.ToUpper() == "FLTTROTT_USA" || comp.ToUpper() == "JAZTRAVEL_US" ||
                 comp.ToUpper() == "JAZTRAVELUSCOM" || comp.ToUpper() == "JRXPTUS" ||
                 comp.ToUpper() == "TPUSA" || comp.ToUpper() == "TRVJUNCTION_USA")
        {
            Eticket_Heder = @"<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                "<tr>" +
                    "<td align='left' valign='middle'>" +
                        // "<img  src='" + BLL.WebsiteStaticData.WebsiteUrl + "images/" + objc.Comp_atol + "' width='' height='57' alt='atol' />" +
                        "</td>" +
                    "<td align='center' valign='middle'><a href='#'>" +
                        "<img  src='" + BLL.WebsiteStaticData.WebsiteUrl + "images/logoes/" + objc.Comp_logo + ".png' alt='Logo' width='200' border='0' /></a></td>" +
                    "<td align='left' valign='middle'>" +
                        "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                            "<tr>" +
                                "<td style='font-family: Arial, Helvetica, sans-serif; font-size: 20px; color: #FFF;'>Call <span><a href='#' style='color: #efbf00; text-decoration: none;'>" + objc.Comp_contact + "</a></span></td>" +
                            "</tr>" +
                            "<tr>" +
                                "<td style='font-family: Arial, Helvetica, sans-serif; font-size: 11px; color: #FFF;'>Opening Time: 0930 To 2230 Eastern Time All Days </td>" +
                            "</tr>" +
                        "</table>" +
                    "</td>" +
                "</tr>" +
            "</table>";
        }
        else
        {
            Eticket_Heder = @"<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
            "<tr>" +
                "<td align='left' valign='middle'>" +
                    // "<img  src='" + BLL.WebsiteStaticData.WebsiteUrl + "images/" + objc.Comp_atol + "' width='' height='57' alt='atol' />" +
                    "</td>" +
                "<td align='center' valign='middle'><a href='#'>" +
                    "<img  src='" + BLL.WebsiteStaticData.WebsiteUrl + "images/logoes/" + objc.Comp_logo + ".png' alt='Logo' width='200' border='0' /></a></td>" +
                "<td align='left' valign='middle'>" +
                    "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                        "<tr>" +
                            "<td style='font-family: Arial, Helvetica, sans-serif; font-size: 20px; color: #FFF;'>Call <span><a href='#' style='color: #efbf00; text-decoration: none;'>" + objc.Comp_contact + "</a></span></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td style='font-family: Arial, Helvetica, sans-serif; font-size: 11px; color: #FFF;'>Opening Time : 24 hours a day / 7 days a week </td>" +
                        "</tr>" +
                    "</table>" +
                "</td>" +
            "</tr>" +
            "</table>";
        }
        return Eticket_Heder;
    }
    public string Eticket_FooterLayout(CompanyDetails cp)
    {       

        Eticket_Footer = @"<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                                                                        "<tr>" +

                                                                            "<td align='center'>Tel:<a href='tel:" + cp.Comp_contact + "' style='color: #FFF; text-decoration: none;'>" + cp.Comp_contact + "</a></td>" +
                                                                        "</tr>" +
                                                                        "<tr>" +
                                                                            "<td align='center'>E-Mail: <a href='mailto:" + cp.Comp_Emailid + "' style='color: #FFF; text-decoration: none;'>" + cp.Comp_Emailid + "</a></td>" +
                                                                        "</tr>" +
                                                                        "<tr>" +
                                                                            "<td align='center'><strong>Address:</strong> " + cp.Comp_Address + "</td>" +
                                                                        "</tr>" +
                                                                        "<tr>" +
                                                                            "<td align='center'>&nbsp;</td>" +
                                                                        "</tr>" +
                                                                        "<tr>" +
                                                                            "<td align='center'>Copyright © " + System.DateTime.Now.Year + " " + cp.Comp_Emailid.Split('@')[1] + "</td>" +
                                                                        "</tr>" +
                                                                    "</table>";
        return Eticket_Footer;
    }

    public string Eticket_Feedback(CompanyDetails cp)//https://www.reviewcentre.com/Travel-Agents/Click2book-www-click2book-co-uk-reviews_9614427
    {
        if (cp.isFeedBack)
        {
            if (cp.Comp_logo == "TRVJUNCTION")
            {
                return FeedbackLink = @"<tr>" +
                                                                                     "<td align='left' valign='top' style='font-size: 14px;'><strong>To  serve you better, Please share your feedback on below links</strong></td>" +
                                                                                 "</tr>" +
                                                                                "<tr>" +
                                                                                     "<td align='left' valign='top' style='font-size: 14px;'>&nbsp;</td>" +
                                                                                 "</tr>" +
                                                                                "<tr>" +
                                                                                     "<td align='left' valign='top' style='font-size: 14px;'><a href='https://www.trustpilot.com/review/www.traveljunction.co.uk'>https://www.trustpilot.com/review/www.traveljunction.co.uk</a></td>" +
                                                                                 "</tr>" +
                                                                                "<tr>" +
                                                                                     "<td align='left' valign='top' style='font-size: 14px;'>&nbsp;</td>" +
                                                                                 "</tr>" +
                                                                                "<tr>" +
                                                                                     "<td align='left' valign='top' style='font-size: 14px;'><a href='https://www.trustpilot.com/review/www.traveljunction.co.uk'>" +
                                                                                         "<img runat='server' src='" + BLL.WebsiteStaticData.WebsiteUrl + "images/rcLogo.png' width='600' height='179' border='0' /></a></td>" +
                                                                                 "</tr>";
            }
            else if (cp.Comp_logo == "C2B")
            {

                return FeedbackLink = @"<tr>" +
                                                                                     "<td align='left' valign='top' style='font-size: 14px;'><strong>To  serve you better, Please share your feedback on below links</strong></td>" +
                                                                                 "</tr>" +
                                                                                "<tr>" +
                                                                                     "<td align='left' valign='top' style='font-size: 14px;'>&nbsp;</td>" +
                                                                                 "</tr>" +
                                                                                "<tr>" +
                                                                                     "<td align='left' valign='top' style='font-size: 14px;'><a href='https://www.reviewcentre.com/Travel-Agents/Click2book-www-click2book-co-uk-reviews_9614427'>https://www.reviewcentre.com/Travel-Agents/Click2book-www-click2book-co-uk-reviews_9614427</a></td>" +
                                                                                 "</tr>" +
                                                                                "<tr>" +
                                                                                     "<td align='left' valign='top' style='font-size: 14px;'>&nbsp;</td>" +
                                                                                 "</tr>" +
                                                                                "<tr>" +
                                                                                     "<td align='left' valign='top' style='font-size: 14px;'><a href='https://www.reviewcentre.com/Travel-Agents/Click2book-www-click2book-co-uk-reviews_9614427'>" +
                                                                                         "<img runat='server' src='" + BLL.WebsiteStaticData.WebsiteUrl + "images/rcLogo.png' width='600' height='179' border='0' /></a></td>" +
                                                                                 "</tr>";



            }
            else
                return "";
        }
        else
            return "";


    }

}