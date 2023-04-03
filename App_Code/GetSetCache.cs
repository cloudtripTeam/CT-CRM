using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GetSetCache
/// </summary>
public class GetSetCache
{
    public GetSetCache()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static List<Permission> getAllPermission()
    {
        if (HttpContext.Current.Cache["Permission"] != null)
        {
            List<Permission> lst = (List<Permission>)HttpContext.Current.Cache["Permission"];
            return lst;
        }
        else
        {
            GetSetDatabase objGetSetDatabase = new GetSetDatabase();
            DataTable dt = objGetSetDatabase.GET_Auth_Roll_Authorization("", "", "", "", "", "", "Select");
            List<Permission> lst = new List<Permission>();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    lst.Add(new Permission(dr));
                }
            }
            HttpContext.Current.Cache["Permission"] = lst;
            return lst;
        }

    }
    public static void UpdateAllPermission()
    {

        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        DataTable dt = objGetSetDatabase.GET_Auth_Roll_Authorization("", "", "", "", "", "", "Select");
        List<Permission> lst = new List<Permission>();
        if (dt != null)
        {
            foreach (DataRow dr in dt.Rows)
            {
                lst.Add(new Permission(dr));
            }
        }
        HttpContext.Current.Cache["Permission"] = lst;


    }
    public static bool CheckPagePermission(string PageName)
    {
        if (HttpContext.Current.Session["UserDetails"] != null)
        {
            UserDetail objUserDetail = HttpContext.Current.Session["UserDetails"] as UserDetail;
            List<Permission> lstPermission = getAllPermission();
            var PerMissionList = from per in lstPermission
                                 where per.PageName.ToUpper() == PageName.ToUpper() && per.RollMstName.ToUpper() == objUserDetail.userRole.ToUpper()
                                 select per;

            if (PerMissionList.ToList().Count > 0)
                return true;
            else
                return false;
        }
        else
            return false;
    }
    public static List<Permission> getPagePermission(string PageName)
    {
        List<Permission> lst = new List<Permission>();
        if (HttpContext.Current.Session["UserDetails"] != null)
        {
            UserDetail objUserDetail = HttpContext.Current.Session["UserDetails"] as UserDetail;
            List<Permission> lstPermission = getAllPermission();
            var PerMissionList = from per in lstPermission
                                 where per.PageName.ToUpper() == PageName.ToUpper() && per.RollMstName.ToUpper() == objUserDetail.userRole.ToUpper()
                                 select per;

            foreach (Permission per in PerMissionList)
            {
                lst.Add(per);
            }
        }
        return lst;
    }

    public static void ContinueSession(string UserID)
    {
        try
        {
            GetSetDatabase objGetSetDatabase = new GetSetDatabase();
            DataTable dt = objGetSetDatabase.GET_UserAccount("", UserID, "", "", "", "", "");
            if (dt.Rows.Count > 0)
            {
                UserDetail objUserDetail = new UserDetail();
                objUserDetail.userID = dt.Rows[0]["UserID"].ToString();
                objUserDetail.userType = dt.Rows[0]["UserType"].ToString();
                objUserDetail.userRole = dt.Rows[0]["UserRole"].ToString();
                objUserDetail.userTitle = dt.Rows[0]["UserTitle"].ToString();
                objUserDetail.userFirstName = dt.Rows[0]["UserFirstName"].ToString();
                objUserDetail.userLastName = dt.Rows[0]["UserLastName"].ToString();
                HttpContext.Current.Session["UserDetails"] = objUserDetail;
            }
        }
        catch { }
    }
}
public class Permission
{
    public Permission(DataRow dr)
    {
        AuthID = dr["AuthID"].ToString();
        RollMstName = dr["RollMstName"].ToString();
        PageOption = dr["PageOption"].ToString();
        PageOptionFullName = dr["PageOptionFullName"].ToString();
        PageID = dr["PageID"].ToString();
        PageName = dr["PageName"].ToString();
        CompanyID = dr["CompanyID"].ToString();
        IsView = dr["IsView"].ToString();
        IsAdd = dr["IsAdd"].ToString();
        IsUpdate = dr["IsUpdate"].ToString();
        IsDelete = dr["IsDelete"].ToString();
        BookingStatus = dr["BookingStatus"].ToString();
        CompanyName = dr["CompanyName"].ToString();
    }
    public string AuthID { set; get; }
    public string RollMstName { set; get; }
    public string PageOption { set; get; }
    public string PageOptionFullName { set; get; }
    public string PageID { set; get; }
    public string PageName { set; get; }
    public string CompanyID { set; get; }
    public string CompanyName { set; get; }
    public string IsView { set; get; }
    public string IsAdd { set; get; }
    public string IsUpdate { set; get; }
    public string IsDelete { set; get; }
    public string BookingStatus { set; get; }
}