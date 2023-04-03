using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Permission
/// </summary>
public class AdminPermission
{
    public AdminPermission()
    { }
    public AdminPermission(DataRow dr)
    {
        //AuthID = dr["AuthID"].ToString();
        //RollMstName = dr["RollMstName"].ToString();
        //OptionID = dr["OptionID"].ToString();
        //OptionName = dr["OptionName"].ToString();
        //PageID = dr["PageID"].ToString();
        PageName = dr["PageName"].ToString();
        PageUrl = dr["PageUrl"].ToString();
        GroupID = dr["GroupID"].ToString();
        GroupName = dr["GroupName"].ToString();
        //IsAuthentication = dr["IsAuthentication"].ToString();
    }
    //public string AuthID { set; get; }
    //public string RollMstName { set; get; }
    //public string OptionID { set; get; }
    //public string OptionName { set; get; }
    //public string PageID { set; get; }
    public string PageName { set; get; }
    public string PageUrl { set; get; }
    public string GroupID { set; get; }
    public string GroupName { set; get; }
    //public string IsAuthentication { set; get; }


}