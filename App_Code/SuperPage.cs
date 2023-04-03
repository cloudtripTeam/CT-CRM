using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Data;


public class ApiCredentionl
{
    public ApiCredentionl()
    { }
    public static string companyID
    {
        get { return ""; }
        
    }
    public static string CredentialId
    {
        get { return ""; }
       
    }
    public static string CredentialPassword
    {
        get { return ""; }
       
    }
    public static string CredentialType
    {
        get { return "LIVE"; }
    }

    public static string WebsiteUrl
    {

        get
        {

            if (HttpContext.Current.Request.Url.Host.ToLower().Contains("localhost"))
            {


                return HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port.ToString() + "/";
            }
            else { return HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host + "/"; }

        }


    }
}




