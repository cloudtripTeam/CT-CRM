using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Collections.Generic;
using System.Linq;

public partial class Admin_TaskIT : System.Web.UI.Page
{
    GetSetDatabase objGetSet = new GetSetDatabase();
    IEnumerable<DataRow> filterData = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        ButtonUpdate.Visible = false;
        lblMsg.Text = "";
        btnExport.Visible = false;
        divMsg.Visible = false;

        if (Session["UserDetails"] != null)
        {
            if (!IsPostBack)
            {
                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;

                if (!objUserDetail.isAuth("TaskIT.aspx"))
                {
                    Response.Redirect("~/Admin/AccessDenied.aspx");
                return;
                } 
                ddlBrand.Items.Clear();
                //CommanBinding.BindCompanyDetails(ref ddlBrand, objUserDetail.userID);
                CommanBinding.BindCompanyDetails(ref ddlBrand, "AdminSup");

                if (objUserDetail.userID == "AdminSup")
                {
                    ddlAgentName.Items.Add(new ListItem("-Select User-", ""));
                    ddlAgentName.Items.Add(new ListItem("Dinesh", "Dinesh"));
                    ddlAgentName.Items.Add(new ListItem("Hrishi", "Hrishi"));
                    ddlAgentName.Items.Add(new ListItem("Amit", "Amit"));
                    ddlAgentName.Items.Add(new ListItem("Manav", "Manav"));
                    ddlAgentName.Items.Add(new ListItem("Rohit", "Rohit"));
                    ddlAgentName.Items.Add(new ListItem("Sandeep", "Sandeep"));
                }
                else
                {
                    ddlAgentName.Items.Add(new ListItem(objUserDetail.userID, objUserDetail.userID));
                }

                ddlBrand.Items.Add(new ListItem("Flight Trotters", "FLTTROTT"));
                ddlBrand.Items.Add(new ListItem("AirfareCache", "AirfareCache"));
                ddlBrand.Items.Add(new ListItem("Dial4travel", "DIAL4TRV"));
                ddlBrand.Items.Add(new ListItem("Travel Junction", "TRVJUNCTION"));
                ddlBrand.Items.Add(new ListItem("Flight XpertUk", "FLTXPT"));
                ddlBrand.Items.Add(new ListItem("Other", "OTHER"));
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }
  
     
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        UserDetail objUserDetail = Session["UserDetails"] as UserDetail;

        if (!string.IsNullOrEmpty(txtTicketDetail.InnerText) && !string.IsNullOrEmpty(ddlBrand.SelectedValue))
        {
            objGetSet.Get_SET_Ticket_Details(txtTicketDetail.InnerText, ddlBrand.SelectedValue, objUserDetail.userID, ddlAgentName.SelectedValue, "Insert");
            ClearInputControls();
            BindTaskGrid();
            lblMsg.Text = "Task Created succesfully";
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindTaskGrid();
    }

    public void BindTaskGrid()
    {
        DataTable dt = objGetSet.Get_Ticket_Details(txtTicketDetail.InnerText, ddlBrand.SelectedValue, ddlAgentName.SelectedValue, "Select");
        if (dt != null && dt.Rows.Count > 0)
        {
            var fromDate = Request.Form["ctl00$ContentPlaceHolder1$txtDateTime"];
            var filterDt = new DataTable();

            if (!string.IsNullOrEmpty(fromDate))
            {
                filterData = dt.AsEnumerable().Where(z => Convert.ToDateTime(z.Field<string>("Created_Date")).ToString("dd/MM/yyyy") == Convert.ToDateTime(fromDate).ToString("dd/MM/yyyy"));
                if(filterData.Count() > 0)
                {
                    filterDt = filterData.CopyToDataTable();
                }
                else
                {
                    filterDt = new DataTable();
                    lblMsg.Text = "No Record found";
                    lblMsg.Style.Add("color", "red");
                    divMsg.Visible = true;
                }
            }
            else
            {
                filterDt = dt;
            }

            ViewState["TaskData"] = filterDt;
            rptrDetails.DataSource = filterDt;
            rptrDetails.DataBind();
            rptrDetails.Visible = true;
            btnExport.Visible = true;
        }
        else
        {
            lblMsg.Text = "No Record found";
            rptrDetails.DataSource = dt;
            rptrDetails.DataBind();
            rptrDetails.Visible = false;
            lblMsg.Style.Add("color", "green");
            divMsg.Visible = true;
        }
    }

    protected void rptrDetails_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case ("Delete"):
                int id = Convert.ToInt32(e.CommandArgument);
                DeleteTask(id);
                break;
            case ("Edit"):
                id = Convert.ToInt32(e.CommandArgument);
                BindTaskDetailToEdit(id);
                break;
        }
    }
    void DeleteTask(int TaskId)
    {
        DataTable dt = objGetSet.Get_Ticket_Details(txtTicketDetail.InnerText, ddlBrand.SelectedValue,Convert.ToString(TaskId), "DELETE");
        if (dt != null && dt.Rows.Count > 0)
        {
            rptrDetails.DataSource = dt;
            rptrDetails.DataBind();
            rptrDetails.Visible = true;
        }
        ClearInputControls();
        BindTaskGrid();
    }

    void BindTaskDetailToEdit(int TaskId)
    {
        DataTable dt = objGetSet.Get_Ticket_Details(txtTicketDetail.InnerText, ddlBrand.SelectedValue,Convert.ToString(TaskId), "Select_Edit");

        txtTicketDetail.InnerText = Convert.ToString(dt.Rows[0]["Ticket_Detail"]);
        ddlBrand.SelectedValue = Convert.ToString(dt.Rows[0]["Company_Name"]);
        ddlAgentName.SelectedValue = Convert.ToString(dt.Rows[0]["Assigned_To"]);
        hdnUserId.Value = Convert.ToString(dt.Rows[0]["Id"]);

        ButtonCreate.Visible = false;
        ButtonUpdate.Visible = true;

        
    }

    void ClearInputControls()
    {
        txtTicketDetail.InnerText = string.Empty;
        ddlBrand.SelectedIndex = 0;
        ddlAgentName.SelectedIndex = 0;
        btnExport.Visible = false;
        ButtonCreate.Visible = true;
        ButtonUpdate.Visible = false;
        txtDateTime.Value = "";
    }

    protected void ButtonUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            objGetSet.Get_SET_Ticket_Details(txtTicketDetail.InnerText, ddlBrand.SelectedValue, Convert.ToString(hdnUserId.Value), ddlAgentName.SelectedValue, "Update"); 
            BindTaskGrid();
            ClearInputControls();
        }
        catch (Exception ex)
        {
            lblMsg.Text =  Convert.ToString(ex.Message);

        }
        finally
        {
            lblMsg.Text = "Task updated succesfully";
            lblMsg.Style.Add("color", "green");
            ButtonCreate.Visible = true;
            ButtonUpdate.Visible = false;
            
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        rptrDetails.DataSource = null;
        rptrDetails.DataBind();
        rptrDetails.Visible = true;

        ClearInputControls();
        //BindTaskGrid();
    }

    public void ExporttoExcel(DataTable table, string filename)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename= WorkStatusReport.xlsx");


        using (ExcelPackage pack = new ExcelPackage())
        {
            ExcelWorksheet ws = pack.Workbook.Worksheets.Add(filename);
            ws.Cells["A1"].LoadFromDataTable(table, true);
            var ms = new System.IO.MemoryStream();
            ws.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;
            ws.Cells.AutoFitColumns();
            pack.SaveAs(ms);
            ms.WriteTo(HttpContext.Current.Response.OutputStream);
        }

        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();

    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        ExporttoExcel(ViewState["TaskData"] as DataTable, "WorkStatusReport");
    }
}