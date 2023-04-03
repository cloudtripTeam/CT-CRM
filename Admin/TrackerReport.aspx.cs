using System;
using System.IO;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;
using MoreLinq;
using System.Linq;
using ExcelLibrary;

using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;


public partial class Admin_TrackerReport : System.Web.UI.Page
{
    GetSetDatabase objGetSetDatabase = new GetSetDatabase();
    private static string _ipAddress = string.Empty;
    int totResultHit = 0, totChkAvailHit = 0, totBookingDetailsHit = 0, totHit1 = 0,
        TotalLandingPage = 0 ;
    double totCost = 0;
    public DataTable dtFlight { get; set; }
    private static  DataTable dtcpc = new DataTable();
    public static string ipAddress
    {
        get { return _ipAddress; }
        set { _ipAddress = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserDetails"] == null)
        {
            if (User.Identity.IsAuthenticated)
            {
                GetSetCache.ContinueSession(HttpContext.Current.User.Identity.Name);
            }
        }
        if (Session["UserDetails"] != null)
        {
            if (!IsPostBack)
            {
                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
                if (!objUserDetail.isAuth("Tracker Report"))
                {
                    Response.Redirect("~/Admin/AccessDenied.aspx");
                    return;
                }
                BindCompany();

                txtHitFrom.Text = DateTime.Today.ToString("dd/MM/yyyy");
        txtHitTo.Text = DateTime.Today.ToString("dd/MM/yyyy");
        BindCompany();
        BindCampaign();
        bindDetails();
    }
}
        else
        {
            Response.Redirect("Login.aspx", false);
        }
    }
    private void BindCompany()
    {
        UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
        CommanBinding.BindCompanyDetails(ref ddlCompany, objUserDetail.userID);

    }
    private void BindCampaign()
    {
        if (ddlCompany.SelectedValue != "")
        {
            GetSetDatabase objGetSetDatabase = new GetSetDatabase();
            DataTable dt = objGetSetDatabase.GET_Campaign_Master("", ddlCompany.SelectedValue);
            ddlSourceMedia.Items.Clear();

            if (dt != null)
            {
                ddlSourceMedia.DataSource = dt;
                ddlSourceMedia.DataValueField = "CampID";
                ddlSourceMedia.DataTextField = "CampName";
                ddlSourceMedia.DataBind();
            }
            foreach (ListItem lst in ddlSourceMedia.Items)
            {
                lst.Selected = true;
            }

        }
    }
    private void bindDetails()
    {
        string SourceMediaList = string.Empty;
        foreach (ListItem lst in ddlSourceMedia.Items)
        {
            if (lst.Selected)
            {
                if (string.IsNullOrEmpty(SourceMediaList))
                {
                    SourceMediaList = lst.Value;
                }
                else
                {
                    SourceMediaList += "," + lst.Value;
                }
            }
        }

        if (!string.IsNullOrEmpty(SourceMediaList))
        {
            FandHServices.FandHServicesClient objGetSetDatabase1 = new FandHServices.FandHServicesClient();
            ViewState["HitFromDate"] = txtHitFrom.Text.Trim();
            ViewState["HitToDate"] = txtHitTo.Text.Trim();
            ViewState["SourceMediaList"] = SourceMediaList;
            //GetSetDatabase objGetSetDatabase = new GetSetDatabase();

            if (ddlCompany.SelectedValue == "FLTTROTT")
            {
                dtFlight = objGetSetDatabase1.SearchPageTracker("", "", "", "", "", "", "", txtHitFrom.Text.Trim(), txtHitTo.Text.Trim(), "", "", "", "", "", "", "", "", SourceMediaList, "", SourceMediaList, "Report_FT");
            }
            else
            {
                dtFlight = objGetSetDatabase1.SearchPageTracker("", "", "", "", "", "", "", txtHitFrom.Text.Trim(), txtHitTo.Text.Trim(), "", "", "", "", "", "", "", "", SourceMediaList, "", SourceMediaList, "Report");
            }

            //dtFlight = objGetSetDatabase1.SearchPageTracker("", "", "", "", "", "", "", txtHitFrom.Text.Trim(), txtHitTo.Text.Trim(), "", "", "", "", "", "", "", "", SourceMediaList, "", SourceMediaList, "Report");
            #region Get CPC cost
           
             dtcpc = objGetSetDatabase.GetCPC_Cost("","");
            #endregion

            if (dtFlight != null && dtFlight.Rows.Count > 0)
            {
                Session["TrackerReport"] = dtFlight;
                btnExport.Visible = true;
                var FlightList = from g in dtFlight.AsEnumerable()
                                 group g by new { Destination = g.Field<string>("Destination"), Origin = g.Field<string>("Origin"), ReqSource = g.Field<string>("ReqSource"),
                                  
                                    DatenTime = Convert.ToDateTime(g.Field<string>("DatenTime")).ToString("dd-MM-yyyy"),
                                 } into FlightGroup
                                 select new { Destination = FlightGroup.Key.Destination,
                                 Origin = FlightGroup.Key.Origin,
                                 ReqSource = FlightGroup.Key.ReqSource,
                                    DatenTime =FlightGroup.Key.DatenTime,
                                     NoOfHits = FlightGroup.Count()
                                 };

                                //select new { Destination = Group1.Key, Total = Group1.Count() };

                rptr.DataSource = FlightList.OrderByDescending(x=>x.NoOfHits).ToList();
                rptr.DataBind();
            }
            else
            {
                rptr.DataSource = null;
                rptr.DataBind();
                lblMsg.Text = "There is no record found as per your searching criteria.";
                btnExport.Visible = false;
            }
        }
        else
        {
            lblMsg.Text = "Please check any source media";
        }
    }
    protected void btnSearchTrack_Click(object sender, EventArgs e)
    {
        bindDetails();
    }


    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex == 0)
        {
            ddlSourceMedia.Items.Clear();
            ddlSourceMedia.Items.Insert(0, new ListItem("Select", ""));
        }
        else
        {
            BindCampaign();
        }
    }
    protected void rptr_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-GB");
        //dt.Select("FrIATA='" + ddlDepart.SelectedValue + "'").CopyToDataTable();
        DataTable objFilter = new DataTable();
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            //Literal ltrOrg = (()(Repeater)e.Item.FindControl("ltrOrigin")).Text;
            Literal lblOrigin = (Literal)e.Item.FindControl("lblOrigin");
            Literal lblDest = (Literal)e.Item.FindControl("lblDestination");
            Literal lblReq = (Literal)e.Item.FindControl("lblReqSource");

            Label lbtNo = (Label)e.Item.FindControl("lbtNo");
            Literal lbtCPC = (Literal)e.Item.FindControl("lbtCPC");
            Literal lbtTotal = (Literal)e.Item.FindControl("lbtTotal");
            Literal lbtDatenTime = (Literal)e.Item.FindControl("lbtDatenTime");
            if (dtcpc != null && dtcpc.Rows.Count != 0)
            {
                DataRow[] dr = dtcpc.Select("Destination='" + lblDest.Text + "' and Campaign='" + lblReq.Text + "' and  (Valid_To >= #" + Convert.ToDateTime(lbtDatenTime.Text).ToString("MM/dd/yyyy") + "#  AND Valid_From <= #" + Convert.ToDateTime(lbtDatenTime.Text).ToString("MM/dd/yyyy") + "#)");
                //if (dr == null || dr.Count() == 0)
                //{
                //    dr = dtcpc.Select("(Destination='' OR Destination='ANY' OR Destination='ALL') AND Campaign ='" + lblReq.Text + "' and  (Valid_To >= #" + Convert.ToDateTime(lbtDatenTime.Text).ToString("MM/dd/yyyy") + "#  AND Valid_From <= #" + Convert.ToDateTime(lbtDatenTime.Text).ToString("MM/dd/yyyy") + "#)");
                //}
                totHit1 += Convert.ToInt32(lbtNo.Text);
                if (dr != null && dr.Count() > 0)
                {
                    try
                    {
                        lbtCPC.Text = dr[0]["CPC_Cost"].ToString();
                       
                        totCost += Convert.ToDouble(lbtCPC.Text);
                        lbtTotal.Text = (Convert.ToDouble(lbtCPC.Text) * Convert.ToInt32(lbtNo.Text)).ToString();
                    }
                    catch { }
                }
            }

            objFilter = dtFlight.Select("Origin='" + lblOrigin.Text + "' and Destination='" + lblDest.Text + "' and ReqSource='" + lblReq.Text + "'").CopyToDataTable();
            //Repeater rptrDetails = (Repeater)e.Item.FindControl("rptrDetails");
            //rptrDetails.DataSource = objFilter;//dtFlight.AsEnumerable().Where(x => x["Destination"].Equals("BKK"));
            //rptrDetails.DataBind();

        }
        else if (e.Item.ItemType == ListItemType.Footer )
        {
            Literal lbtHits = (Literal)e.Item.FindControl("lbtHits");
            Literal lbtCost = (Literal)e.Item.FindControl("lbtCost");
            lbtHits.Text = totHit1.ToString();
            lbtCost.Text = totCost.ToString("f2");
        }

        }
    protected void rptr_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Details")
        {
            pnlReport.Visible = false;
            pnlDetails.Visible = true;

            string aa = e.CommandArgument.ToString();
            string[] aaa = aa.Split('~');
            string page = string.Empty;
            if (aaa[0] == "result")
            {
                page = "Result";
            }
            else if (aaa[0] == "Passenger")
            {
                page = "PassengerDetails";
            }
            else if (aaa[0] == "Confirmation")
            {
                page = "Confirmation";
            }

           // GetSetDatabase objGetSetDatabase = new GetSetDatabase();
            FandHServices.FandHServicesClient objGetSetDatabase = new FandHServices.FandHServicesClient();
            DataTable dt = objGetSetDatabase.SearchPageTracker("", "", "", "", "", "", aaa[1], ViewState["HitFromDate"].ToString(), ViewState["HitToDate"].ToString(), "", "", page, "", "", "", "", "", "", "", ViewState["SourceMediaList"].ToString(), "Select");
            if (dt != null)
            {
                rptrTrack.DataSource = dt;
                rptrTrack.DataBind();
            }
            else
            {
                lblMsg.Text = "There is no record found as per your searching criteria.";
            }
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        pnlReport.Visible = true;
        pnlDetails.Visible = false;
    }


    public void WriteExcelWithNPOI(DataTable dt, String extension)
    {

        IWorkbook workbook;

        if (extension == "xlsx")
        {
            workbook = new XSSFWorkbook();
        }
        else if (extension == "xls")
        {
            workbook = new HSSFWorkbook();
        }
        else
        {
            throw new Exception("This format is not supported");
        }

        ISheet sheet1 = workbook.CreateSheet("Sheet 1");

        //make a header row
        IRow row1 = sheet1.CreateRow(0);

        for (int j = 0; j < dt.Columns.Count; j++)
        {

            ICell cell = row1.CreateCell(j);
            String columnName = dt.Columns[j].ToString();
            cell.SetCellValue(columnName);
        }

        //loops through data
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            IRow row = sheet1.CreateRow(i + 1);
            for (int j = 0; j < dt.Columns.Count; j++)
            {

                ICell cell = row.CreateCell(j);
                String columnName = dt.Columns[j].ToString();
                cell.SetCellValue(dt.Rows[i][columnName].ToString());
            }
        }

        using (var exportData = new MemoryStream())
        {
            Response.Clear();
            workbook.Write(exportData);
            string fileName = "TrackerReport_" + DateTime.Now.ToString("ddMMMyyyy");
            if (extension == "xlsx") //xlsx file format
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", fileName + ".xlsx"));
                Response.BinaryWrite(exportData.ToArray());
            }
            else if (extension == "xls")  //xls file format
            {
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", fileName + ".xls"));
                Response.BinaryWrite(exportData.GetBuffer());
            }
            Response.End();
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
       
        try
        {
            DataTable dt = (DataTable)Session["TrackerReport"];
                WriteExcelWithNPOI(dt, "xlsx");
                
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
