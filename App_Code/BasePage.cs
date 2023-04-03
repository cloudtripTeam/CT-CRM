using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// A custom base page class that sets the page Title, if needed.
/// </summary>
public class BasePage : System.Web.UI.Page
{
    protected override void OnLoadComplete(EventArgs e)
    {
        
            // Is this page defined in the site map?
            string newTitle = null;
            Uri url = Request.Url;
            string path = String.Format("{0}{1}{2}{3}", url.Scheme,  Uri.SchemeDelimiter, url.Authority, url.AbsolutePath);               

            Page.Title = newTitle;
            HtmlMeta keywords = new HtmlMeta();
            keywords.Name = "keywords";
            keywords.Content = "master page,asp.net,tutorial";

            Page.Header.Controls.Add(keywords);
       

        base.OnLoadComplete(e);
    }
}
