using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using BLL;

/// <summary>
/// Summary description for SearchAsynch
/// </summary>
public class SearchAsynch
{
    public string confirmationNumber = string.Empty;
    public string confirmationNumber1 = string.Empty;
    string filePath = string.Empty;
    string filePathFlaxi = string.Empty;


    #region Search
    public IAsyncResult FlightSearchAsync(SearchDetails someParameter, string path)
    {
        filePath = path;
        DoSomethingDelegate doSomethingDelegate = new DoSomethingDelegate(Search);
        IAsyncResult ar = doSomethingDelegate.BeginInvoke(someParameter, ref confirmationNumber, new AsyncCallback(FlightResultCallback), null);
        return ar;
    }
    private delegate void DoSomethingDelegate(SearchDetails someParameter, ref string confirmationNumber);
    private void FlightResultCallback(IAsyncResult ar)
    {
        AsyncResult aResult = (AsyncResult)ar;
        DoSomethingDelegate doSomethingDelegate = (DoSomethingDelegate)aResult.AsyncDelegate;
        doSomethingDelegate.EndInvoke(ref confirmationNumber, ar);
    }
    private void Search(SearchDetails searchDeatails, ref string confirmationNumber)
    {
        BLL.FlightsBL OLowfareSearch = new BLL.FlightsBL();

        OLowfareSearch.CallLowFareSearch(searchDeatails, filePath);
       

    }



    #endregion

}