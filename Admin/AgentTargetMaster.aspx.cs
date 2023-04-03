using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AgentTargetMaster : System.Web.UI.Page
{
    SqlConnection con = DataConnection.GetConnection();
    GetSetDatabase objGetSetDatabase = new GetSetDatabase();
    UserDetail objUserDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserDetails"] != null)
        {
            objUserDetail = Session["UserDetails"] as UserDetail;
            if (!IsPostBack)
            {
                if (!objUserDetail.isAuth("amend Booking"))
                {
                    Response.Redirect("Default.aspx");
                    return;
                }
                else
                {

                    ddlMonth.SelectedValue = DateTime.Today.Month.ToString();
                    ddlYear.SelectedValue = DateTime.Today.Year.ToString();
                    BindRollMst();
                }
            }
        }               
       

    }
    
    

    protected void GetTargets()
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand cmd = new SqlCommand("USP_Agent_Target_Master", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Query", "SELECT");
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            if(dt.Rows.Count>0)
            {
                rptAgentTarget.DataSource = dt;
                rptAgentTarget.DataBind();
            }

        }
        catch (Exception ex) { }
        finally
        { con.Close(); }
        
    }
    private int SetAgentsTarget(string query,int id, string agentid, decimal profit, int noBooking, int noSectors, string assignBy, int Months, int Years)
    {
        
        int i = 0;
        try
        {
            SqlCommand cmd = new SqlCommand("USP_Agent_Target_Master", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Query", query);
            cmd.Parameters.AddWithValue("@Agent_Tar_ID", id);
            cmd.Parameters.AddWithValue("@Agent_Tar_Agent_ID", agentid);
            cmd.Parameters.AddWithValue("@Agent_Tar_Profit", profit);
            cmd.Parameters.AddWithValue("@Agent_Tar_No_Booking", noBooking);
            cmd.Parameters.AddWithValue("@Agent_Tar_No_Sectors", noSectors);
            cmd.Parameters.AddWithValue("@Agent_Tar_Assign_By", assignBy);
            cmd.Parameters.AddWithValue("@Agent_Tar_Month", Months);
            cmd.Parameters.AddWithValue("@Agent_Tar_Year", Years);
            con.Open();
            i = cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            ltrMsg.Text = "Try Agin";
        }
        finally { con.Close(); }
        return i;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int re = 0;
        string str = "";
        int count = 0;
        objUserDetail = Session["UserDetails"] as UserDetail;

        for (int i = 0; i < chkAgentList.Items.Count; i++)
        {
            if (chkAgentList.Items[i].Selected == true) 
            {
                ++count;
                str = chkAgentList.Items[i].Text;
                re = SetAgentsTarget("INSERT", 0, str, Convert.ToDecimal(txtProfit.Text), Convert.ToInt32(txtNoBooking.Text), Convert.ToInt32(txtSectors.Text), objUserDetail.userID, Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue));
            }

            

        }

        ltrMsg.Text = count + " Record insert successfully.";
    }

    protected void rptAgentTarget_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "update")
        {
            objUserDetail = Session["UserDetails"] as UserDetail;
            SetAgentsTarget("UPDATE", Convert.ToInt32(((HiddenField)e.Item.FindControl("hfid")).Value), ((TextBox)e.Item.FindControl("Label1")).Text,
                Convert.ToDecimal(((TextBox)e.Item.FindControl("Label2")).Text), Convert.ToInt32(((TextBox)e.Item.FindControl("Label3")).Text), 
                Convert.ToInt32(((TextBox)e.Item.FindControl("Label4")).Text), objUserDetail.userID,
                Convert.ToInt32(((TextBox)e.Item.FindControl("Label6")).Text), Convert.ToInt32(((TextBox)e.Item.FindControl("Label7")).Text));
            ltrMsg.Text = "Record Updated.";
        }
    }

    public void BindRollMst()
    {
      
        DataTable dt = objGetSetDatabase.GET_Auth_Roll_Master("", "", "", "Select");
        ddlRoll.Items.Clear();
        if (dt != null)
        {
           
            foreach (DataRow dr in dt.Rows)
            {
                if (!dr["MstName"].ToString().Equals("superadmin", StringComparison.OrdinalIgnoreCase))
                    ddlRoll.Items.Add(new ListItem(dr["MstDescription"].ToString(), dr["MstName"].ToString()));
            }
            ddlRoll.Items.Insert(0, "Select");
        }
    }

    protected void ddlRoll_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtUser = objGetSetDatabase.GET_UserAccount("", "", "", "", "INTR", "", ddlRoll.SelectedValue);
        if (dtUser != null)
        {
            List<string> listAgents = new List<string>();
           

            foreach (DataRow dr in dtUser.Rows)
            {
                if (dr["UserID"].ToString().ToLower() != "adminsup" && dr["UserID"].ToString().ToLower() != "admin")
                    listAgents.Add(dr["UserID"].ToString());
            }
            chkAgentList.DataSource = listAgents;
            chkAgentList.DataBind();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataTable dtTg = new DataTable();
        try
        {
            SqlCommand cmd = new SqlCommand("USP_Agent_Target_Master", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Query", "SELECT");
            cmd.Parameters.AddWithValue("@Agent_Tar_Month", ddlMonth.SelectedValue);
            cmd.Parameters.AddWithValue("@Agent_Tar_Year", ddlYear.SelectedValue);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dtTg);
            if (dtTg.Rows.Count > 0)
            {
                rptAgentTarget.DataSource = dtTg;
                rptAgentTarget.DataBind();
            }
            else
                ltrMsg.Text = "No data found.";
        }
        catch (Exception ex)
        {
            ltrMsg.Text = "Try Agin";
        }
        finally { con.Close(); }

    }


}