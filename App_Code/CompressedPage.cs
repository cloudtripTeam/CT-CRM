using System;
using System.Web.UI;
using System.IO;

/// <summary>
/// Summary description for CompressedPage
/// </summary>
public class CompressedPage : System.Web.UI.Page
{
	public CompressedPage()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    protected override object LoadPageStateFromPersistenceMedium()
    {
        string viewState = Request.Form["__VSTATE"];
        byte[] bytes = Convert.FromBase64String(viewState);
        bytes = Compressor.Decompress(bytes);
        LosFormatter formatter = new LosFormatter();
        return formatter.Deserialize(Convert.ToBase64String(bytes));
    }

    protected override void SavePageStateToPersistenceMedium(object viewState)
    {
        LosFormatter formatter = new LosFormatter();
        StringWriter writer = new StringWriter();
        formatter.Serialize(writer, viewState);
        string viewStateString = writer.ToString();
        byte[] bytes = Convert.FromBase64String(viewStateString);
        bytes = Compressor.Compress(bytes);
        ClientScript.RegisterHiddenField("__VSTATE", Convert.ToBase64String(bytes));
    }
}
