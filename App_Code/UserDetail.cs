using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Summary description for UserDetail
/// </summary>
public class UserDetail
{

    public UserDetail()
    {


    }

    //public static string companyID
    //{
    //    get { return "BKG"; }
    //}
    //public static string companyName
    //{
    //    get { return "Flightspro"; }
    //}
    //public static string CredentialId
    //{
    //    get { return "BKG"; }
    //}
    //public static string CredentialPassword
    //{
    //    get { return "12345"; }
    //}
    //public static string CredentialType
    //{
    //    get { return "LIVE"; }
    //}

    public string userID { set; get; }
    public string userType { set; get; }
    public string userRole { set; get; }
    public string AtolNo { set; get; }
    public string userTitle { set; get; }
    public string userFirstName { set; get; }
    public string userLastName { set; get; }
    public string ContactNo { set; get; }
    public string Fax { set; get; }
    public string Email { set; get; }
    public string Country { set; get; }
    public string City { set; get; }
    public string Address { set; get; }
    public string PostCode { set; get; }
    public List<AdminPermission> PermissionDetails { set; get; }

    public bool isAuth(string PageName)
    {
        bool ret = false;
        if ((from AdminPermission str in PermissionDetails
             where str.PageName.StartsWith(PageName, StringComparison.OrdinalIgnoreCase)
             select str).ToList().Count > 0)
        {
            ret = true;
        }
        return ret;
    }

}
