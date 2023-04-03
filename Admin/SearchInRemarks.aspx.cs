using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.IO;

public partial class Admin_SearchInRemarks : System.Web.UI.Page
{
    DataTable dtRemarksBooking = new DataTable();
    


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserDetails"] != null)
        {

            if (!IsPostBack)
            {
                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
                if (!objUserDetail.isAuth("Agents P & L"))
                {
                    Response.Redirect("Default.aspx");
                    return;
                }
                else
                {
                  //  CommanBinding.BindCompanyDetails(ref ddlCompany, objUserDetail.userID);
                    txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    string role = objUserDetail.userRole.ToLower();
                    if (role != "superadmin" && role != "admin" && role != "team head" && role != "team head ft" && role != "marketing head" && role != "onlinetl" && role != "team head ca")
                    {
                        txtAgent.Text = objUserDetail.userID;
                        txtAgent.Enabled = false;
                    }

                    else { txtAgent.Enabled = true; }

                }
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }

    protected void btnFind_Click(object sender, EventArgs e)
    {
        this.gvAssignedBooking.DataSource = null;
        dtRemarksBooking.Clear();
        GetSetDatabase db = new GetSetDatabase();
        UserDetail objUserDetail = Session["UserDetails"] as UserDetail;

        if (objUserDetail != null)
        {
            string role = objUserDetail.userRole.ToLower();
            if (role != "superadmin" && role != "admin" && role != "team head" && role != "team head ft" && role != "team head ca" && role != "marketing head" && role != "onlinetl")
            {
                dtRemarksBooking = db.SearchInRemarks(txtInvNo.Text == "" ? null : txtInvNo.Text, txtRemarks.Text,objUserDetail.userID, txtFromDate.Text == "" ? null : txtFromDate.Text, txtToDate.Text == "" ? null : Convert.ToDateTime(txtToDate.Text).AddDays(1).ToString("dd/MM/yyyy"));
            }
            else
            {
                dtRemarksBooking = db.SearchInRemarks(txtInvNo.Text == "" ? null : txtInvNo.Text,txtRemarks.Text, txtAgent.Text == "" ? null : txtAgent.Text, txtFromDate.Text == "" ? null : txtFromDate.Text, txtToDate.Text == "" ? null : Convert.ToDateTime(txtToDate.Text).AddDays(1).ToString("dd/MM/yyyy") );

            }
           //ViewState["RemarksBooking"] = dtRemarksBooking;


            if (dtRemarksBooking.Rows.Count > 0)
            {
               
                gvAssignedBooking.DataSource = dtRemarksBooking;
                gvAssignedBooking.DataBind();
                gvAssignedBooking.Visible = true;
              
            }
            else
            {
                gvAssignedBooking.DataSource = dtRemarksBooking;
                gvAssignedBooking.DataBind();
                gvAssignedBooking.Visible = false;
                ltrInvc.Text = "No Record Found.";
              

            }
        }

    }
}