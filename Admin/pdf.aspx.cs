using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp;
using iTextSharp.text;
using System.Drawing.Imaging;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

public partial class Admin_pdf : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Document DOC = new Document();
        var document = new Document(PageSize.A4, 50, 50, 25, 25);

        // Create a new PdfWriter object, specifying the output stream
        var output = new MemoryStream();
        var writer = PdfWriter.GetInstance(document, output);

        // Open the Document for writing
        document.Open();


        var titleFont = FontFactory.GetFont("Arial", 18, Font.BOLD);
        var subTitleFont = FontFactory.GetFont("Arial", 14, Font.BOLD);
        var boldTableFont = FontFactory.GetFont("Arial", 12, Font.BOLD);
        var endingMessageFont = FontFactory.GetFont("Arial", 10, Font.ITALIC);
        var bodyFont = FontFactory.GetFont("Arial", 12, Font.NORMAL);

        document.Add(new Paragraph("Northwind Traders Receipt", titleFont));
        var logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/images/logo.png"));
        logo.SetAbsolutePosition(440, 800);
        document.Add(logo);


        //create New FileStream with image "WM.JPG"

        FileStream fs1 = new FileStream(Server.MapPath(@"~/images/first.jpg"), FileMode.Open);


        iTextSharp.text.Image JPG = iTextSharp.text.Image.GetInstance(System.Drawing.Image.FromStream(fs1), ImageFormat.Jpeg);


        //Scale image

        JPG.ScalePercent(35f);


        //Set position

        JPG.SetAbsolutePosition(130f, 240f);

        //Close Stream

        fs1.Close();


        document.Add(JPG);



        string contents = File.ReadAllText(Server.MapPath("~/images/invoice.htm"));

        var parsedHtmlElements = HTMLWorker.ParseToList(new StringReader(contents), null);

        // Enumerate the elements, adding each one to the Document...
        foreach (var htmlElement in parsedHtmlElements)
            document.Add(htmlElement as IElement);
        // open Document


      


        

        document.Close();
        Response.ContentType = "application/pdf";
        Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Receipt-{0}.pdf", "invoice"));
        Response.BinaryWrite(output.ToArray());
       
    }
}