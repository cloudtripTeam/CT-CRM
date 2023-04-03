<%@ Application Language="C#" %>

<script runat="server">

    protected void Application_BeginRequest(object sender, EventArgs e)
    {
        /* string path = HttpContext.Current.Request.Url.AbsolutePath;
         if (HttpContext.Current.Request.Url.AbsoluteUri.ToLower().IndexOf("www.") == -1)
         {
             Response.Status = "301 Moved Permanently";
             Response.AddHeader("Location", HttpContext.Current.Request.Url.AbsoluteUri.Replace("http://", "https://www."));

             HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri.Replace("http://", "https://www."));
             return;
         }
         else if (HttpContext.Current.Request.Url.AbsoluteUri.ToLower().IndexOf("https") == -1)
         {
             Response.Status = "301 Moved Permanently";
             Response.AddHeader("Location", HttpContext.Current.Request.Url.AbsoluteUri.Replace("http://", "https://"));

             HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri.Replace("http://", "https://"));
         }*/


      

    }
    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown
    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs
        Session["error"] = "";
        Exception exc = Server.GetLastError();
        Session["error"] = exc;
        Response.Redirect("/Admin/Error.aspx");
    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

</script>
