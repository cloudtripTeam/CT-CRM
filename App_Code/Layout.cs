using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Layout
/// </summary>
public class Layout
{
    FandHServices.FandHServicesClient obj = new FandHServices.FandHServicesClient();
    public string xpHeder { get; set; }
    public string PayNow { get; set; }
    public string xpFooter { get; set; }   
    public string HeaderLayout(string comp)
    {
        CompanyDetails objc = SetCompanyDetail(comp);

        PayNow = objc.PayNow;
        FooterLayout(objc);
        if (comp.ToUpper() == "C2BUS")
        {

            xpHeder = @"<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                             "<tr>" +
                               "<td align='left' valign='top' style='background:#98a5bd; padding:30px 10px;'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                                 "<tr>" +
                                 
                                   "<td align='left' valign='middle'><a href='#'><img src='" + BLL.WebsiteStaticData.WebsiteUrl + "images/logoes/" + objc.Comp_logo + ".png' alt='Logo' width='250' height='50' border='0' /></a></td>" +
                                   "<td align='left' valign='middle'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                                     "<tr>" +
                                       "<td style='font-family:Arial, Helvetica, sans-serif; font-size:20px; color:#FFF;'>Call <span><a href='#' style='color:#efbf00; text-decoration:none;'>" + objc.Comp_contact + "</a></span></td>" +
                                     "</tr>" +
                                     "<tr>" +
                                       "<td style='font-family:Arial, Helvetica, sans-serif; font-size:11px; color:#FFF;'>Opening Time: 10:00 AM to 10:30 PM Eastern Time</td>" +
                                     "</tr>" +
                                   "</table></td>" +
                                 "</tr>" +
                               "</table>";
        }
        else
        {
            xpHeder = @"<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                             "<tr>" +
                               "<td align='left' valign='top' style='background:#98a5bd; padding:30px 10px;'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                                 "<tr>" +
                                  
                                   "<td align='center' valign='middle'><a href='#'><img src='" + BLL.WebsiteStaticData.WebsiteUrl + "images/logoes/" + objc.Comp_logo + ".png' alt='Logo' width='250' height='50' border='0' /></a></td>" +
                                   "<td align='left' valign='middle'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                                     "<tr>" +
                                       "<td style='font-family:Arial, Helvetica, sans-serif; font-size:20px; color:#FFF;'>Call <span><a href='#' style='color:#efbf00; text-decoration:none;'>" + objc.Comp_contact + "</a></span></td>" +
                                     "</tr>" +
                                     "<tr>" +
                                       "<td style='font-family:Arial, Helvetica, sans-serif; font-size:11px; color:#FFF;'>Opening Time : 08AM - 11PM All days</td>" +
                                     "</tr>" +
                                   "</table></td>" +
                                 "</tr>" +
                               "</table>";
        }
        return xpHeder;
    }
    public string FooterLayout(CompanyDetails cp)
    {
        xpFooter = @"<table style='width:100%;'>" +
                    "<tr>" +
                           "<td align='center' valign='top' style='background:#1c2d4c; padding:10px; color:#FFF; font-size:11px; font-family:Arial, Helvetica, sans-serif;'>" +
                               "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                           "<tr><td align='center'>Tel:<a href='#' style='color:#FFF; text-decoration:none;'>" + cp.Comp_contact + "</a></td>" +
                           "</tr><tr>" +
                           "<td align='center'>E-Mail: <a href='#' style='color:#FFF; text-decoration:none;'>" + cp.Comp_Emailid + "</a></td>" +
                           "</tr><tr>" +
                       "<td align='center'><strong>Address:</strong>" + cp.Comp_Address + "</td>" +
                         "</tr><tr><td align='center'>&nbsp;</td></tr><tr>" +
                           "<td align='center'>Copyright © " + System.DateTime.Now.Year + " "+ cp.Comp_Emailid.Split('@')[1]+"</td>" +
                         "</tr></table></td></tr></table></td>" +   
                         "</tr>" +
                "</table>";
        return xpFooter;
    }


    public CompanyDetails SetCompanyDetail(string Comp)
    {

        Comp = Comp.ToUpper();
        CompanyDetails objComp = new CompanyDetails();
        string add1 = "651N broad St, suite 20, MiddleTown New Castle 19709";
        string add2 = "Profile West 950 Great West Road Brentford,Middlesex, TW8 9ES";
    
        if (Comp == "CT") { objComp.Brand_Name = "Cloud Trip";  objComp.Comp_Emailid = "info@cloudtrip.us"; objComp.Comp_logo = "CT"; objComp.Comp_contact = "+1833 703 1003"; objComp.Comp_atol = "atol1.png"; objComp.Comp_Name = "Cloud Trip LLC."; objComp.Comp_Address = add1; objComp.Comp_AtolNumber = "10950"; objComp.Comp_RegNumber = "09162028"; objComp.Comp_VatNumber = "204729911"; objComp.isEticket = true; objComp.isSMS = false; objComp.isFeedBack = true; objComp.isVoucher = true; objComp.PayNow = "http://payment.cloudtrip.us/"; }
        if (Comp == "BKOFFICE") { objComp.Brand_Name = "Offline"; objComp.Comp_Emailid = "info@cloudtrip.us"; objComp.Comp_logo = "CT"; objComp.Comp_contact = "+1833 703 1003"; objComp.Comp_atol = "atol1.png"; objComp.Comp_Name = "Cloud Trip LLC."; objComp.Comp_Address = add1; objComp.Comp_AtolNumber = "10950"; objComp.Comp_RegNumber = "09162028"; objComp.Comp_VatNumber = "204729911"; objComp.isEticket = true; objComp.isSMS = false; objComp.isFeedBack = true; objComp.isVoucher = true; objComp.PayNow = "http://payment.cloudtrip.us/"; }

        return objComp;
    }

    public DataTable Supplier(string SuppCode)
    {
        DataTable dtName = new DataTable();
        dtName = obj.GET_Supplier_Master("SELECT", "", SuppCode, "");
        //string[] list = {"Major Travel","1000"};        
        return dtName;
    }
    
}
public class CompanyDetails
{
    public string Comp_Name { get; set; }
    public string Comp_logo { get; set; }
    public string Comp_contact { get; set; }
    public string Comp_atol { get; set; }
    public string Comp_Emailid { get; set; }
    public string Comp_Address { get; set; }
    public string Comp_AtolNumber { get; set; }
    public string Comp_RegNumber { get; set; }
    public string Comp_VatNumber { get; set; }
    public string Brand_Name { get; set; }
    public bool isEticket { get; set; }
    public bool isSMS { get; set; }
    public bool isFeedBack { get; set; }
    public bool isVoucher { get; set; }
    public string PayNow { get; set; }
}