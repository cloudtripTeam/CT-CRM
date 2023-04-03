using System;
using PdfFileWriter;
using System.Diagnostics;


public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        System.Web.HttpBrowserCapabilities browser = Request.Browser;
        string s = "Browser Capabilities\n"
            + "Type = " + browser.Type + "\n"
            + "Name = " + browser.Browser + "\n"
            + "Version = " + browser.Version + "\n"
            + "Major Version = " + browser.MajorVersion + "\n"
            + "Minor Version = " + browser.MinorVersion + "\n"
            + "Platform = " + browser.Platform + "\n"
            + "Is Beta = " + browser.Beta + "\n"
            + "Is Crawler = " + browser.Crawler + "\n"
            + "Is AOL = " + browser.AOL + "\n"
            + "Is Win16 = " + browser.Win16 + "\n"
            + "Is Win32 = " + browser.Win32 + "\n"
            + "Supports Frames = " + browser.Frames + "\n"
            + "Supports Tables = " + browser.Tables + "\n"
            + "Supports Cookies = " + browser.Cookies + "\n"
            + "Supports VBScript = " + browser.VBScript + "\n"
            + "Supports JavaScript = " +
                browser.EcmaScriptVersion.ToString() + "\n"
            + "Supports Java Applets = " + browser.JavaApplets + "\n"
            + "Supports ActiveX Controls = " + browser.ActiveXControls
                  + "\n"
            + "Supports JavaScript Version = " +
                browser["JavaScriptVersion"] + "\n"
                  + "isMobile = " +
                browser["IsMobile"] + "\n"

        + "isTablet = " +
               browser["isTablet"] + "\n";



        Response.Write(s);

        PdfDocument pdfDoc = new PdfDocument(PaperType.A4, false, UnitOfMeasure.cm, "ATOL");
        PdfPage page = new PdfPage(pdfDoc);
        PdfContents content = new PdfContents(page);
        content.BeginTextMode();
        PdfFont.CreatePdfFont(pdfDoc,"Arial",System.Drawing.FontStyle.Bold,true);
        content.DrawText(PdfFont.CreatePdfFont(pdfDoc, "Arial", System.Drawing.FontStyle.Regular, true), 10, "This is an important document. Make sure that you take it with you when you travel");

        content.DrawText(PdfFont.CreatePdfFont(pdfDoc, "Arial", System.Drawing.FontStyle.Bold, true), 20, "ATOL Certificate");

        content.DrawText(PdfFont.CreatePdfFont(pdfDoc, "Arial", System.Drawing.FontStyle.Bold, true), 14, "This confirms that your money is protected by the ATOL scheme and that you can get home if your travel company collapses.");

        content.DrawText(PdfFont.CreatePdfFont(pdfDoc, "Arial", System.Drawing.FontStyle.Regular, true), 14, "This certificate sets out how the ATOL scheme will protect the people named on it for the parts of their trip listed below.");
        LineD ld = new LineD(new PointD(1,1),new PointD(3,3));
        content.DrawLine(ld, 400);
        content.EndTextMode();

        pdfDoc.CreateFile();

        // start default PDF reader and display the file
        Process Proc = new Process();
        Proc.StartInfo = new ProcessStartInfo("ATOL");
        Proc.Start();

    }
}