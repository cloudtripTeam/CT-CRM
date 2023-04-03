<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="~/Admin/flight-result.aspx.cs" Inherits="Admin_flight_result" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="https://fonts.googleapis.com/css?family=Anton" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css?family=Roboto+Condensed" rel="stylesheet">
    <link rel="stylesheet" href="../css/searchResult.css" />

    <!-- main-cont -->
    <br />
    <div class="wrapper-padding">
        <div class="two-colls">
            <div class="two-colls-left">
                <!-- // side // -->
                <div class="side-block fly-in">
                    <div class="side-stars">
                        <div class="side-padding">
                            <div class="side-lbl">Stops</div>
                            <%=stopOversFilter %>
                        </div>
                    </div>
                </div>
                <!-- \\ side \\ -->
                <!-- // side // -->
                <div class="side-block fly-in">
                    <div class="side-stars">
                        <div class="side-padding">
                            <div class="side-lbl">Airlines</div>
                            <%=airlinesFilter %>
                        </div>
                    </div>
                </div>
                <!-- \\ side \\ -->

            </div>
            <div class="two-colls-right">
                <div class="two-colls-right-b">
                    <div class="padding">

                         <%=fares %>

                    </div>
                </div>
                <br class="clear" />
            </div>
        </div>
        <div class="clear"></div>

    </div>


    
    <script type="text/javascript">
        $('.button-cl').click(function () {
            $("#demo").removeClass("in");
        });
    </script>
    <script type="text/javascript">


        function filterAirlin(airCode) {
            if ($("#" + airCode).is(':checked'))
                $(".cls" + airCode).show();
            else
                $(".cls" + airCode).hide();
        }
        function filterStop(StopCode) {
            if ($("#" + StopCode).is(':checked'))
                $(".clas" + StopCode).show();
            else
                $(".clas" + StopCode).hide();
        }
    </script>


    <asp:HiddenField runat="server" ID="_hfFrom"></asp:HiddenField>
    <asp:HiddenField runat="server" ID="_hfTo"></asp:HiddenField>
    <asp:HiddenField runat="server" ID="_hfDepart"></asp:HiddenField>
    <asp:HiddenField runat="server" ID="_hfReturn"></asp:HiddenField>
    <asp:HiddenField runat="server" ID="_hfAdult"></asp:HiddenField>
    <asp:HiddenField runat="server" ID="_hfChild"></asp:HiddenField>
    <asp:HiddenField runat="server" ID="_hfInfant"></asp:HiddenField>
    <asp:HiddenField runat="server" ID="_hfAirline"></asp:HiddenField>
    <asp:HiddenField runat="server" ID="_hfClass"></asp:HiddenField>
    <asp:HiddenField runat="server" ID="_hfDirect"></asp:HiddenField>
    <asp:HiddenField runat="server" ID="_hfFlexible"></asp:HiddenField>
    <asp:HiddenField runat="server" ID="_hfjtype"></asp:HiddenField>
</asp:Content>

