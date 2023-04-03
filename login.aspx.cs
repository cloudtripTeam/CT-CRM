
using Spire.Pdf;
using Spire.Pdf.Graphics;
using Spire.Pdf.Security;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using WhatsAppApi;

public partial class _login : CompressedPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((User.Identity.IsAuthenticated) && (Session["UserDetails"] != null))
        {
            Response.Redirect("~/admin/Default.aspx", false);
        }
       
    }
    public void signature()
    {
        var pth = Server.MapPath("~/docs/CT120024__CT120024 (28).pdf");
        Spire.Pdf.PdfDocument doc = new Spire.Pdf.PdfDocument(pth);
        Spire.Pdf.PdfPageBase page = doc.Pages[0];
   
        var pth2 = Server.MapPath("~/docs/Demo.pfx");
        Spire.Pdf.Security.PdfCertificate cert = new Spire.Pdf.Security.PdfCertificate(pth2, "cloudtrip");

        Spire.Pdf.Security.PdfSignature signature = new Spire.Pdf.Security.PdfSignature(doc, page, cert, "Hrishi");

        signature.ContactInfo = "Harry";
        signature.Certificated = true;

        signature.DocumentPermissions = Spire.Pdf.Security.PdfCertificationFlags.AllowFormFill;
        var pth3 = Server.MapPath("~/docs/Result.pdf");
        doc.SaveToFile(pth3);
    }
    public void test()
    {
        //Create a PdfDocument object
        PdfDocument doc = new PdfDocument();

        //Load a sample PDF file
        doc.LoadFromFile(Server.MapPath("~/docs/CT120024__CT120024 (28).pdf"));

        //Load the certificate 
        var pth2 = Server.MapPath("~/docs/Demo.pfx");
        Spire.Pdf.Security.PdfCertificate cert = new Spire.Pdf.Security.PdfCertificate(pth2, "cloudtrip");

        //Create a PdfSignature object and specify its position and size 
        PdfSignature signature = new PdfSignature(doc, doc.Pages[doc.Pages.Count - 1], cert, "MySignature");
        RectangleF rectangleF = new RectangleF(doc.Pages[0].ActualSize.Width - 260 - 54, 200, 260, 110);
        signature.Bounds = rectangleF;
        signature.Certificated = true;

        //Set the graphics mode to ImageAndSignDetail
        signature.GraphicsMode = GraphicMode.SignImageAndSignDetail;

        //Set the signature content 
        signature.NameLabel = "Signer:";
        signature.Name = "Gary";
        signature.ContactInfoLabel = "Phone:";
        signature.ContactInfo = "0123456";
        signature.DateLabel = "Date:";
        signature.Date = DateTime.Now;
        signature.LocationInfoLabel = "Location:";
        signature.LocationInfo = "USA";
        signature.ReasonLabel = "Reason:";
        signature.Reason = "I am the author";
        signature.DistinguishedNameLabel = "DN:";
        signature.DistinguishedName = signature.Certificate.IssuerName.Name;

        //Set the signature image source
        var pth3 = Server.MapPath("~/images/atol1.png");
        signature.SignImageSource = PdfImage.FromFile(pth3);

        //Set the signature font 
        signature.SignDetailsFont = new PdfTrueTypeFont(new Font("Arial Unicode MS", 12f, FontStyle.Regular));

        //Set the document permission to forbid changes but allow form fill
        signature.DocumentPermissions = PdfCertificationFlags.ForbidChanges | PdfCertificationFlags.AllowFormFill;

        //Save to file 
        var pth33 = Server.MapPath("~/docs/Result.pdf");
        doc.SaveToFile(pth33);
        doc.Close();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            GetSetDatabase objGetSetDatabase = new GetSetDatabase();
            DataTable dt = objGetSetDatabase.GET_UserAccount("", txtUserId.Text.Trim(), "", "", "", "", "");
            if (dt.Rows.Count > 0)
            {
                UserDetail objUserDetail = new UserDetail();
                if (dt.Rows[0]["UserPassword"].ToString() == txtPwd.Text)
                {
                    if (Convert.ToBoolean(dt.Rows[0]["IsIpCheck"]))
                    {
                        DataTable dtIP = objGetSetDatabase.GET_BlackListedIP("", Request.UserHostAddress.ToString(), "CMS", "true");
                        if (dtIP != null && dtIP.Rows.Count > 0)
                        {
                            objUserDetail.userID = dt.Rows[0]["UserID"].ToString();
                            objUserDetail.userType = dt.Rows[0]["UserType"].ToString();
                            objUserDetail.userRole = dt.Rows[0]["UserRole"].ToString();
                            objUserDetail.userTitle = dt.Rows[0]["UserTitle"].ToString();
                            objUserDetail.userFirstName = dt.Rows[0]["UserFirstName"].ToString();
                            objUserDetail.userLastName = dt.Rows[0]["UserLastName"].ToString();
                            objUserDetail.Email = dt.Rows[0]["Email_ID"].ToString();
                            
                            FormsAuthentication.RedirectFromLoginPage(objUserDetail.userID, false);
                            objUserDetail.PermissionDetails = new System.Collections.Generic.List<AdminPermission>();
                            DataTable dtPermission = objGetSetDatabase.GET_Auth_Roll_Authorization_New(objUserDetail.userRole, "true", objUserDetail.userRole.ToLower() == "superadmin" ? "SELECTAllPAGE" : "Select");
                            foreach (DataRow dr in dtPermission.Rows)
                            {
                                objUserDetail.PermissionDetails.Add(new AdminPermission(dr));
                            }
                            Session["UserDetails"] = objUserDetail;
                            Response.Redirect("~/Admin/Default.aspx", false);
                        }
                        else
                        {
                            lblMsg.Text = "Sorry, Access denied. Your IP - " + Request.UserHostAddress.ToString();
                        }
                    }
                    else
                    {
                        objUserDetail.userID = dt.Rows[0]["UserID"].ToString();
                        objUserDetail.userType = dt.Rows[0]["UserType"].ToString();
                        objUserDetail.userRole = dt.Rows[0]["UserRole"].ToString();
                        objUserDetail.userTitle = dt.Rows[0]["UserTitle"].ToString();
                        objUserDetail.userFirstName = dt.Rows[0]["UserFirstName"].ToString();
                        objUserDetail.userLastName = dt.Rows[0]["UserLastName"].ToString();
                        FormsAuthentication.RedirectFromLoginPage(objUserDetail.userID, false);
                        objUserDetail.PermissionDetails = new System.Collections.Generic.List<AdminPermission>();
                        DataTable dtPermission = objGetSetDatabase.GET_Auth_Roll_Authorization_New(objUserDetail.userRole, "true", objUserDetail.userRole.ToLower() == "superadmin" ? "SELECTAllPAGE" : "Select");
                        foreach (DataRow dr in dtPermission.Rows)
                        {
                            objUserDetail.PermissionDetails.Add(new AdminPermission(dr));
                        }
                        Session["UserDetails"] = objUserDetail;
                       // Response.Redirect("~/Admin/Default.aspx", false);
                        Response.Redirect("~/Admin/bookingdetails.aspx", false);
                    }
                }
                else
                {
                     lblMsg.Text = "Invalid userid & password";
                }
            }
            else
            {
               lblMsg.Text = "You aren't a authorized user, Please contact admin";
            }
        }
        catch
        {
            lblMsg.Text = "you are not authorized,please contact admin";
            txtUserId.Text = "";
            txtUserId.Focus();
        }
    }

    private bool sendWhatsAppMessage()
    {
        string from = "919711001181";
        string to = "919711001181";//Sender Mobile
        string msg = "Logged In successfully";

        WhatsApp wa = new WhatsApp(from, "BnXk*******B0=", "NickName", true, true);

        wa.OnConnectSuccess += () =>
        {
           // MessageBox.Show("Connected to whatsapp...");

            wa.OnLoginSuccess += (phoneNumber, data) =>
            {
                wa.SendMessage(to, msg);
               // MessageBox.Show("Message Sent...");
            };

            wa.OnLoginFailed += (data) =>
            {
                //MessageBox.Show("Login Failed : {0}", data);
            };

            wa.Login();
        };

        wa.OnConnectFailed += (ex) =>
        {
           // MessageBox.Show("Connection Failed...");
        };

        wa.Connect();

        return true;
    
    }
}
