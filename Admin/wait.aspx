<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="~/Admin/wait.aspx.cs" Inherits="Admin_wait" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<link href="../css/StyleSheet.css" rel="stylesheet" />--%>
    <link href="../../css/StyleSheet.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
<!--onload="checkStatus();"
    var pollInterval = 1000;

    var checkStatusUrl = "checkStatus.aspx";
    var req;
    var nextPageUrl;
    // this tells the wait page to check the status every so often
    window.setInterval("checkStatus()", pollInterval);

    function checkStatus() {
        nextPageUrl = document.getElementById("<% =hidNextPage.ClientID%>").value;
        createRequester();

        if (req != null) {
            req.onreadystatechange = process;
            req.open("GET", checkStatusUrl, true);
            req.send(null);
        }
        else { alert("Sorry, Unbale to redirect on next page.") }
    }

    function process() {
        if (req.readyState == 4) {
            // only if "OK"
            if (req.status == 200) {
                if (req.responseText == "1") {
                    // a "1" means it is done, so here is where you redirect
                    // to the confirmation page
                    document.location.replace(nextPageUrl);
                }
                // NOTE: any status other than 200 or any response other than
                // "1" require no action
            }
        }
    }

    /*
    Note that this tries several methods of creating the XmlHttpRequest object,
    depending on the browser in use. Also note that as of this writing, the
    Opera browser does not support the XmlHttpRequest.
    */
    function createRequester() {
        try {
            req = new ActiveXObject("Msxml2.XMLHTTP");
        }
        catch (e) {
            try {
                req = new ActiveXObject("Microsoft.XMLHTTP");
            }
            catch (oc) {
                req = null;
            }
        }

        if (!req && typeof XMLHttpRequest != "undefined") {
            req = new XMLHttpRequest();
        }

        return req;
    }
    //-->
    </script>
  
     <asp:HiddenField ID="hidNextPage" runat="server" />
        <div id="wrapper-content">
            
            <section class="homepage-wait text-center">
                <div class="homepage-banner-warpper">

                    <div class="homepage-banner-content ">
                        <div class="outer-container">

                            <div class="group-title">
                                <p>
                                    <div style="margin-bottom: 20px;" id="progressbar">
                                        <div id="progressLine">
                                            <div id="pbaranim"></div>
                                        </div>
                                    </div>
                                </p>
                                <h2 class="banner title">
                                    <asp:Label ID="lblfrom" runat="server" Text=""></asp:Label>
                                    <span class="indicator"><i class="fa fa-arrow-left"></i>
                                        <i class="fa fa-arrow-right"></i></span>
                                    <asp:Label ID="lblTo" runat="server" Text=""></asp:Label></h2>

                                <div class="group-btn mt-0"><a class="date-band"><span class="icons fa fa-calendar"></span><span class="text">
                                    <asp:Label ID="lblDates" runat="server" Text=""></asp:Label></span> </a></div>
                                
                               
                            </div>
                            <br />
                            <div >
                                <img src="../images/loader1.gif" /> &nbsp; Searching ...

                            </div>
                        </div>
                    </div>

                </div>
                <div class="clearfix"></div>
            </section>
            

        </div>
   
</asp:Content>