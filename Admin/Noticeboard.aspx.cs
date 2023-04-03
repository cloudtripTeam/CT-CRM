using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;

public partial class Admin_Noticeboard : System.Web.UI.Page
{
    UserDetail objUserDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserDetails"] != null)
            {
                objUserDetail = Session["UserDetails"] as UserDetail;
                txtApplicableDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtExpiryDate.Text = DateTime.Today.AddDays(30).ToString("dd/MM/yyyy");
            }
        if (!Page.IsPostBack)
        {
            

            if (objUserDetail.userRole.ToLower() != "admin" && objUserDetail.userRole.ToLower() != "superadmin" && objUserDetail.userRole.ToLower() != "team head" && objUserDetail.userRole.ToLower() != "onlinetl")
            {
                noticeboard.Style.Add(HtmlTextWriterStyle.Display, "none");

            }
            if (File.Exists(Server.MapPath("~/App_Data/ReqXml/Noticeboard.xml")))
            {
                string xmlFile = Server.MapPath("~/App_Data/ReqXml/Noticeboard.xml");
                DataSet dataSet = new DataSet();
                dataSet.ReadXml(xmlFile);
                ViewState["notification"] = dataSet;
                if (dataSet != null)
                {
                    if (dataSet.Tables.Count > 0)
                    {
                        if (dataSet.Tables[0] != null)
                        {
                            GetNotification(dataSet);
                        }
                    }

                }

            }
        }
    }


    private void GetNotification(DataSet dataSet)
    {
        gdvnoticeboard.DataSource = dataSet.Tables[0];
        gdvnoticeboard.DataBind();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {

        string ApplicableDate = DateTime.Now.ToString("dd/MM/yyyy");
        string ExpiryDate = DateTime.Now.AddDays(7).ToString("dd/MM/yyyy");

        try
        {
            if (txtApplicableDate.Text.Trim() != "")
            {

                ApplicableDate = Convert.ToDateTime(txtApplicableDate.Text.Trim()).ToString("dd/MM/yyyy");
            }
            else
            {
                ApplicableDate = DateTime.Today.ToString("dd/MM/yyyy");
            
            }
        }
        catch (Exception ex) { }

        try
        {
            if (txtExpiryDate.Text.Trim() != "")
            {
                ExpiryDate = Convert.ToDateTime(txtExpiryDate.Text.Trim()).ToString("dd/MM/yyyy");
            }
            else
            {
                ExpiryDate = DateTime.Today.AddDays(30).ToString("dd/MM/yyyy");
            
            }
        }
        catch (Exception ex) { }

        try
        {
            if (File.Exists(Server.MapPath("~/App_Data/ReqXml/Noticeboard.xml")))
            {
                string xmlFile = Server.MapPath("~/App_Data/ReqXml/Noticeboard.xml");
                XDocument xDocument = XDocument.Load(xmlFile);
                XElement root = xDocument.Element("NoticeBoard");
                IEnumerable<XElement> rows = root.Descendants("Notice");
                XElement firstRow = rows.First();
                firstRow.AddBeforeSelf(
                   new XElement("Notice",
                   new XElement("NoticeMessage", txtNotice.Text.Trim()),
                   new XElement("Applicable_Date", ApplicableDate),
                   new XElement("Expiry_Date", ExpiryDate),
                   new XElement("NoticeBy", objUserDetail.userID),
                    new XElement("Created_Date", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"))
                   ));
                xDocument.Save(xmlFile);
            }
            else
            {

                using (XmlWriter xWr = XmlWriter.Create(Server.MapPath("~/App_Data/ReqXml/Noticeboard.xml")))
                {
                    xWr.WriteStartDocument();
                        xWr.WriteStartElement("NoticeBoard");

                        xWr.WriteStartElement("Notice");

                        xWr.WriteElementString("NoticeMessage", txtNotice.Text.Trim());
                        xWr.WriteElementString("Applicable_Date", txtApplicableDate.Text.Trim());
                        xWr.WriteElementString("Expiry_Date", txtExpiryDate.Text.Trim());
                        xWr.WriteElementString("NoticeBy", objUserDetail.userID);
                        xWr.WriteElementString("Created_Date", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));                       


                        xWr.WriteEndElement();

                        xWr.WriteEndElement();
                    xWr.WriteEndDocument();

                    // FLUSH AND CLOSE.
                    xWr.Flush();
                    xWr.Close();


                }
            
            }
        }
        catch {

            lblMsg.Text = "Sorry, unable to save notice";

        }


    }

    protected void gdvnoticeboard_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
        int i = gvr.RowIndex;
        DataSet ds = (DataSet)ViewState["notification"];
        ds.ReadXml(Server.MapPath("~/App_Data/ReqXml/Noticeboard.xml"));
        if (e.CommandName == "edit")
        {
            string messages = (gdvnoticeboard.Rows[i].FindControl("txtMessages") as TextBox).Text;
            ds.Tables[0].Rows[i]["NoticeMessage"] = messages;
            ds.Tables[0].Rows[i]["NoticeBy"] = objUserDetail.userID;
            ds.WriteXml(Server.MapPath("~/App_Data/ReqXml/Noticeboard.xml"));
            GetNotification(ds);
            ltrMsg.Text = "Record Updated.";
        }
        else if (e.CommandName == "delete")
        {
            ds.Tables[0].Rows.RemoveAt(i);
            ds.WriteXml(Server.MapPath("~/App_Data/ReqXml/Noticeboard.xml"));
            GetNotification(ds);
            ltrMsg.Text = "Record Deleted.";

        }
    }

    protected void gdvnoticeboard_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void gdvnoticeboard_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void gdvnoticeboard_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (objUserDetail.userRole.ToLower() != "admin" && objUserDetail.userRole.ToLower() != "superadmin" && objUserDetail.userRole.ToLower() != "team head" && objUserDetail.userRole.ToLower() != "onlinetl")
            {
                ImageButton imgBtnUpdate = (ImageButton)e.Row.FindControl("btnUpdate");
                imgBtnUpdate.Visible = false;
                ImageButton imgBtnDelete = (ImageButton)e.Row.FindControl("btnDelete");
                imgBtnDelete.Visible = false;
            }
        }
    }
}