using System;
using System.IO.Compression;
using System.Web;
using System.Web.Security;

public class HttpCompressionModule : IHttpModule
{
    private bool _isDisposed = false;

    public void Init(HttpApplication context)
    {
        context.BeginRequest += new EventHandler(context_BeginRequest);
    }

    void context_BeginRequest(object sender, EventArgs e)
    {
        HttpApplication app = sender as HttpApplication;
        HttpContext ctx = app.Context;

       //if (!ctx.Request.Url.PathAndQuery.ToLower().Contains(".aspx") || ctx.Request.Url.PathAndQuery.ToLower().Contains("markup") || ctx.Request.Url.PathAndQuery.ToLower().Contains("ownfare"))
        //    return;

        if (ctx.Request.Url.PathAndQuery.ToLower().Contains(".aspx") || ctx.Request.Url.PathAndQuery.ToLower().Contains("markup") || ctx.Request.Url.PathAndQuery.ToLower().Contains("ownfare"))
        {
            return;
        }

        if (IsEncodingAccepted("gzip"))
        {
            //app.Request.Filter =
            //    new System.IO.Compression.GZipStream(
            //        app.Request.Filter, CompressionMode.Decompress);

            app.Response.Filter = new GZipStream(app.Response.Filter,
      CompressionMode.Compress);
            SetEncoding("gzip");
        }
        else if (IsEncodingAccepted("deflate"))
        {
            app.Response.Filter = new DeflateStream(app.Response.Filter,
      CompressionMode.Compress);
            SetEncoding("deflate");
        }
    }
    private bool IsEncodingAccepted(string encoding)
    {
        return HttpContext.Current.Request.Headers["Accept-encoding"] != null &&
          HttpContext.Current.Request.Headers["Accept-encoding"].Contains(encoding);
    }
    private void SetEncoding(string encoding)
    {
        HttpContext.Current.Response.AppendHeader("Content-encoding", encoding);
    }
    private void Dispose(bool dispose)
    {
        _isDisposed = dispose;
    }
    ~HttpCompressionModule()
    {
        Dispose(false);
    }
    public void Dispose()
    {
        Dispose(true);
    }
}
