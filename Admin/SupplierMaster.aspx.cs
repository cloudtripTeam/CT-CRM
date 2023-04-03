using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_SupplierMaster : System.Web.UI.Page
{
    FandHServices.FandHServicesClient objlos = new FandHServices.FandHServicesClient();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnInsert_Click(object sender, EventArgs e)
    {
        bool b;
        b = objlos.SET_Supplier_Master("INSERT", 0, txtCode.Text, txtName.Text, txtEmailID.Text, txtPhone.Text, txtCPerson.Text, txtRemarks.Text, "",txtAtol.Text);
        if (b == true)
        {
            lblMsg.Text = "Record Inserted.";
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = objlos.GET_Supplier_Master("SELECT", txtName.Text,txtCode.Text,txtAtol.Text);
        if (dt.Rows.Count > 0)
        { rptSupplier.DataSource = dt; rptSupplier.DataBind(); rptSupplier.Visible = true;}
        else
        {lblMsg.Text = "Record not found."; rptSupplier.Visible = false;}
        
    }
    protected void rptSupplier_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
               
        int id = Convert.ToInt32(((HiddenField)(rptSupplier.Items[e.Item.ItemIndex].FindControl("hfID"))).Value);
        string code = ((TextBox)(rptSupplier.Items[e.Item.ItemIndex].FindControl("txtCode"))).Text;
        string name = ((TextBox)(rptSupplier.Items[e.Item.ItemIndex].FindControl("txtName"))).Text;
        string email = ((TextBox)(rptSupplier.Items[e.Item.ItemIndex].FindControl("txtEmailID"))).Text;
        string phone = ((TextBox)(rptSupplier.Items[e.Item.ItemIndex].FindControl("txtPhone"))).Text;
        string cperson = ((TextBox)(rptSupplier.Items[e.Item.ItemIndex].FindControl("txtCPerson"))).Text;
        string remarks = ((TextBox)(rptSupplier.Items[e.Item.ItemIndex].FindControl("txtRemarks"))).Text;
        string atol = ((TextBox)(rptSupplier.Items[e.Item.ItemIndex].FindControl("txtAtol"))).Text;
        bool b;
        b = objlos.SET_Supplier_Master("UPDATE", id, code, name, email, phone, cperson, remarks, "", atol);
        if (b == true){lblMsg.Text = "Record Updated.";}
    }
}