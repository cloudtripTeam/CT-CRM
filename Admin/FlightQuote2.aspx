<%@ Page Title="" ValidateRequest="false" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" 
    AutoEventWireup="true" CodeFile="FlightQuote2.aspx.cs" Inherits="Admin_FlightQuote2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="../stylesheets/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/client_quotes/client-quote.css" rel="stylesheet" />
    <%-- <script src="../js/client_quote_js/jquery-1.9.1.min.js"></script>
    <script src="../js/client_quote_js/bootstrap.min.js"></script>--%>


    <header class="flight-pattern-bg">
        <div class="container pt-20">
            <div>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center">
                    <div class="q-logo mb-40">
                        <img src="#" runat="server" id="Company_Logo_Tab" />
                        <b style="display: block;">
                            <i class="fa fa-map-marker"></i>
                            <asp:Label Text="" runat="server" ID="Company_Address_Tab" Style="display: -webkit-inline-box;" />
                        </b>
                    </div>
                </div>
                <div class="clearfix"></div>
            </div>
        </div>
    </header>
    <div class="clearfix"></div>
    <div class="container">
        <ul class="tabs">
            <li class="tab-link current" data-tab="tab-1">Flight By XP</li>
            <li class="tab-link" data-tab="tab-2">No XP</li>
        </ul>

        <div id="tab-1" class="tab-contentmain current">
            <section style="margin-top: 0px">
                <div class="col-md-8">
                    <div class="col-md-4">
                        <asp:TextBox ID="txtInvoice" ClientIDMode="Static" placeholder="Search by Invoice No."
                            autocomplete="off" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <asp:Button ID="btnSearch" ClientIDMode="Static" runat="server" Text="Search" style="height: 28px;padding: 0px 6px 0px 5px;"
                            OnClientClick="return SendSearchQuotation()"
                            CssClass="btn btn-danger btn-lg" OnClick="btnSearch_Click" />
                    </div>
                </div>
                <div class="col-md-9">
                    <div class="">
                        <div id="content" class="quote-content">
                            <ul class="heading nav nav-tabs">
                                <li class="res"><a data-toggle="tab" href="#res" aria-expanded="false">Reservation</a></li>
                                <li class="iti active"><a data-toggle="tab" href="#iti" aria-expanded="true">Itinerary</a></li>
                                <li class="pax"><a data-toggle="tab" href="#pax" aria-expanded="false">Passengers</a></li>
                                <li class="atol"><a data-toggle="tab" href="#atol" aria-expanded="false">Financial Protection</a></li>
                                <li class="bc"><a data-toggle="tab" href="#bc" aria-expanded="false">Booking Conditions</a></li>
                                <li class="price" style="width: 7%!important"><a data-toggle="tab" href="#price" aria-expanded="false">Price</a></li>
                                <li class="sr" style="width: 16.5%!important"><a data-toggle="tab" href="#sr" aria-expanded="false">Special Request</a></li>
                                <li class="tc" style="width: 5%!important"><a data-toggle="tab" href="#tc" aria-expanded="false">T&C</a></li>
                            </ul>
                            <div class="tab-content">
                                <div id="res" class="tab-pane fade">
                                    <asp:Label Text="" runat="server" ID="Itin_Res_Tab" />
                                </div>


                                <div id="iti" class="tab-pane fade  active in">
                                    <div class="itinerary">
                                        <h3 class="mt-0">Flight Details 
                                        </h3>
                                        <hr class="mb-10 mt-10" />
                                        <asp:Label Text="" runat="server" ID="Itin_Pax_Detail_Tab" />
                                        <hr class="mb-10 mt-10" />
                                        <asp:Label Text="" runat="server" ID="Itin_Section_Tab"></asp:Label>


                                        <!--<div class="stopover mb-15">
                                    <span>
                                        <img src="images/connection.png" width="10" height="17" alt="stopover">
                                        Layover - 0hrs 30mins at Vienna Intl Arpt, Vienna
                                    </span>
                                </div>-->

                                        <div class="clearfix"></div>
                                       <%-- <div class="rseg-brek"></div>
                                        <div class="clearfix"></div>--%>


                                        <div class="clearfix"></div>
                                    </div>
                                    <asp:Label Text="" runat="server" ID="Baggage_Deatil_Tab" />

                                    <asp:Label Text="" runat="server" ID="Payment_Details_Tab" />




                                    <%--   <div class="col-md-12 col-sm-12 col-xs-12 offset-m-0">
                                <br />
                                <b>Payment Conditions 1:</b>
                            </div>
                            <div class="col-md-12 col-sm-12 col-xs-12 mb-20">
                                <br />
                                •	There is no card processing fee<br />
                            </div>--%>
                                </div>


                                <div id="pax" class="tab-pane fade">
                                    <div class="widget stacked widget-table action-table">
                                        <div class="widget-content">
                                            <div class="scroll-x">
                                                <asp:Label Text="" runat="server" ID="Pax_Details_Tab" />
                                            </div>
                                        </div>
                                    </div>
                                </div>



                                <div id="atol" class="tab-pane fade">
                                    <div class="col-md-6 col-sm-6 col-xs-12 p-15 fp-box">
                                        <div align="center">
                                            <img src="../images/client_quotes_images/atol-protected.png" alt="ATOL" title="ATOL" class="img-responsive" />
                                        </div>
                                        <br />
                                        Traveljunction is ATOL protected.
                                        <%--Our Atol number is 10950.<br />
                                        <br />
                                        When you buy an ATOL protected flight or flight inclusive holiday from us you will receive an ATOL Certificate. This lists what is financially protected, where you can get information on what this means for you and who to contact if things go wrong.
                                   <br />
                                        <br />
                                        <br />
                                        Please use the link below to verify an ATOL number <a href="http://publicapps.caa.co.uk/modalapplication.aspx?appid=2" style="font-size: 11px; font-weight: bold;" target="_blank">http://publicapps.caa.co.uk/modalapplication.aspx?appid=2</a>
                                   --%> </div>
                                    <div class="col-md-6 col-sm-6 col-xs-12 p-15 fp-box">
                                        <div align="center">
                                            <img src="../images/client_quotes_images/iata-logo.png" alt="TTA" title="TTA" class="img-responsive" />
                                        </div>
                                        <br />
                                        Traveljunction is a member of International Air Transport Association (IATA).
                                        <%--Our IATA membership number is U8550.<br>
                                        <br />
                                        It means that you can book holiday secure in the knowledge that Travel Trust Association will protect you in the unlikely event
                                    a member becoming insolvent.<br />
                                        Should a member of the Travel Trust Association for any reason financially fail or cease trading, the Travel Trust Association will liaise with the suppliers and Tour Operators to ensure that your holiday goes ahead unaffected. If for any reason this is not possible,we will administer a claim for a refund of money that you have paid to memeber for your holiday.
                                   <br />
                                        <br />
                                        Please use the link below to verify a TTA number <a href="http://www.traveltrust.co.uk/VerifyMember.aspx" style="font-size: 12px; font-weight: bold;" target="_blank">http://www.traveltrust.co.uk/VerifyMember.aspx</a>
                                    --%></div>

                                    <div class="col-md-12 col-sm-12 col-xs-12 offset-m-0">
                                        <div align="center" style="font-size: 16px; padding-bottom: 10px; color: #999; border-top: 1px solid #162849; margin: 40px 0px 10px 0px; display: block;">
                                            <br />
                                            <img src="../images/client_quotes_images/paymentoption.png" alt="secure payment" title="secure payment" />
                                        </div>
                                    </div>
                                </div>

                                <div id="bc" class="tab-pane fade">
                                    <asp:Label Text="" runat="server" ID="Baggage_Deatil_Tab2" />


                                    <div class="clearfix itinerary-info">&nbsp;</div>
                                    <div class="col-md-12 col-sm-12 col-xs-12 offset-m-0" style="text-align: justify;">
                                        Please note prices are only guaranteed once ticketed.
                                <br />
                                        <br />
                                        Please note that your passport should be valid for 6 months from the date of your return.
                                <br />
                                        <br />
                                        Traveling to or transiting through USA please enroll yourself in ESTA program by using link below.
                                <br />
                                        <br />
                                        <a href="https://esta.cbp.dhs.gov/esta/" style="font-size: 12px; font-weight: bold;" target="_blank">https://esta.cbp.dhs.gov/esta/</a><br>
                                        Please check visa requirements with relevant Embassy / High Commission.
                                <br />
                                        <br />
                                        Traveling to or transiting through Canada please enroll yourself in ETA program by using link below.
                                <br />
                                        <br />
                                        <a href="http://www.cic.gc.ca/english/visit/eta-start-int.asp/" style="font-size: 12px; font-weight: bold;" target="_blank">http://www.cic.gc.ca/english/visit/eta-start-int.asp/</a><br>
                                        Please check visa requirements with relevant Embassy / High Commission.
                                <br />
                                        <br />
                                        Traveling to South Africa please procure the below list of documents before your travel:
                                <br />
                                        <br />
                                        <a href="https://www.gov.uk/foreign-travel-advice/south-africa/entry-requirements/" style="font-size: 12px; font-weight: bold;" target="_blank">https://www.gov.uk/foreign-travel-advice/south-africa/entry-requirements/</a><br>
                                        Please check visa requirements with relevant Embassy / High Commission.
                                <br />
                                        <br />
                                        Traveling to or transiting through New Zealand please apply for the Visa using the below mentioned link:
                                <br />
                                        <br />
                                        <a href="https://www.immigration.govt.nz/new-zealand-visas/apply-for-a-visa/about-visa/nzeta" style="font-size: 12px; font-weight: bold;" target="_blank">https://www.immigration.govt.nz/new-zealand-visas/apply-for-a-visa/about-visa/nzeta</a><br>
                                        Please check visa requirements with relevant Embassy / High Commission.
                                <br />
                                        <br />
                                        <span style="font-size: 12px !important;">*We price match if you find anything cheaper within 24 hours of booking </span>

                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>


                                <div id="price" class="tab-pane fade">
                                    <asp:Label Text="" runat="server" ID="Payment_Details_Tab2" />

                                    <div class="col-md-12 col-sm-12 col-xs-12 offset-m-0">
                                        <br />
                                        <b>Payment Conditions :</b>
                                    </div>
                                    <div class="col-md-12 col-sm-12 col-xs-12 offset-m-0">
                                        <br />
                                        There is no card processing fee<br />
                                    </div>
                                    <div class="clearfix"></div>
                                </div>


                                <div id="sr" class="tab-pane fade special-request">
                                    <div class="col-md-12 col-sm-12 col-xs-12 mb-10">**Please note that all Special Request are subject to availability of the Airlines. </div>

                                    <asp:Label Text="" runat="server" ID="Special_Request_Tab" />

                                </div>


                                <div id="tc" class="tab-pane fade">
                                    <asp:Label Text="" runat="server" ID="lblTerms_Condition_Tab" />
                                    <div class="clearfix"></div>
                                </div>

                            </div>
                            <div class="clearfix"></div>
                        </div>
                        <div class="clearfix"></div>
                        <div class="col-md-12 p-0">
                            <div class="self-payment" id="divSuccessMsg" runat="server">
                                <asp:Label Text="" runat="server" ID="lblSuccessMessage"></asp:Label>

                            </div>
                            <div class="clearfix"></div>

                            <div class="self-payment">
                                <div class="col-md-6 col-sm-6 col-xs-12 mt-10">
                                    <h3>Self Payment</h3>
                                    <a href="http://payment.traveljunction.co.uk/" target="_blank" class="payment-btn-pay">Pay Now</a>
                                </div>
                                <div class="col-md-6 col-sm-6 col-xs-12 text-right">
                                    <img src="../images/client_quotes_images/paymentoption.png" alt="secure payment" title="secure payment" />
                                </div>
                                <div class="clearfix"></div>
                            </div>
                            <div class="why-book-box">
                                <div class="col-md-12 col-sm-12 col-xs-12 pt-10 pb-20">
                                    <div class="col-md-12 col-sm-12 col-xs-12 p-0 font20">
                                        <h3>Why Book with us</h3>
                                    </div>

                                    <div class="clearfix"></div>
                                    <hr>
                                    <div class="col-md-12 col-sm-12 col-xs-12 offset-m-0">
                                        <ul style="list-style-type: decimal; margin: 0px; padding: 0px;">
                                            <li>Lowest Price Assurance with Price Match Policy</li>
                                            <li>Personalized service &amp; Experienced Support</li>
                                            <li>Departure Reminders</li>
                                            <li>Proper Documentation</li>
                                            <li>Price Drop Advantage (For Refundable Fare Types)</li>
                                            <li>Book now pay later (Bookings on Deposit for Refundable Fares Types)</li>
                                            <li>One Point Contact with Direct contact numbers for the designated consultants</li>
                                            <li>Vast range of Services with best prices</li>
                                            <li>Book with Confidence (All Holidays Protected by ATOL &amp; IATA)</li>
                                            <li>Extended Support 24 hours a day / 7 days a week</li>
                                            <li>Reward Points &amp; Benefits for repeat clients</li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                            </div>


                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 p-0">
                                <div class="weather-bg-box">
                                    <asp:Label Text="" runat="server" ID="lblWeatherDetails" />
                                    <asp:HiddenField runat="server" ID="hdnTimer" ClientIDMode="Static" />
                                    <%--<div class="weather-info-details text-center">
                                    <h3>New York, NY</h3>
                                    <p>
                                        <span class="temprature"><b>18<sup>°</sup>
                                        </b></span>
                                        <span class="weather-time-info">9:56 am EST</span>
                                        <span class="weather-icon-box">
                                            <img src="images/client_quotes_images/10d.png" /></span>
                                        <span class="weather-type">Mostly cloudy</span>
                                    </p>
                                </div>--%>
                                </div>
                            </div>

                            <%--                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 offset-0 destination">
                                <img src="../images/client_quotes_images/New Yo_599223.jpg" />
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 offset-0 weather-info">
                                <a href="http://www.accuweather.com/en/us/new-york-ny/10007/weather-forecast/349727" class="aw-widget-legal"></a>
                                <div id="awcc1486058327989" class="aw-widget-current lt-350 lt-479" data-locationkey="349727" data-unit="c" data-language="en-us" data-useip="false" data-uid="awcc1486058327989" style="display: block;">

                                    <div class="aw-widget-current-inner">
                                        <div class="aw-widget-content bg-s">
                                            <a id="link_current" href="https://www.accuweather.com/en/us/new-york-ny/10007/weather-forecast/349727?utm_source=online-travelunravel-com&amp;utm_medium=oap_weather_widget&amp;utm_term=link_current&amp;utm_content=accuweather&amp;utm_campaign=current" target="_blank" class="aw-current-weather">
                                                <div class="aw-current-weather-inner">
                                                    <h3>New York, NY</h3>
                                                    <span class="aw-icon aw-icon-7-l" data-icon="7"></span>
                                                    <p class="aw-temp-time-desc">
                                                        <span class="aw-temperature-today"><b>18<sup>°</sup></b></span>
                                                        <time>4:51 am EDT</time>
                                                        <span class="aw-weather-description">Cloudy</span>
                                                    </p>
                                                </div>
                                            </a>
                                        </div>
                                        <a id="link_get_widget" href="https://www.accuweather.com/en/free-weather-widgets?utm_source=online-travelunravel-com&amp;utm_medium=oap_weather_widget&amp;utm_term=link_get_widget&amp;utm_content=accuweather&amp;utm_campaign=current" target="_blank" class="aw-toggle"><i>Get this widget</i></a>
                                        <div class="aw-get-widget-footer">
                                            <p class="aw-get-info">Want weather on your site?</p>
                                            <a id="link_get_widget_footer" href="https://www.accuweather.com/en/free-weather-widgets?utm_source=online-travelunravel-com&amp;utm_medium=oap_weather_widget&amp;utm_term=link_get_widget_footer&amp;utm_content=accuweather&amp;utm_campaign=current" target="_blank" class="aw-get-this-widget"><i class="aw-get-widget-icon"></i><span>Get widget</span></a>
                                        </div>
                                    </div>
                                </div>
                                <script src="../js/client_quote_js/launch.js"></script>
                            </div>--%>
                        </div>




                    </div>
                </div>
                <div class="col-md-3">
                    <div class="quote-sidebar">
                        <ul class="info-tab">
                            <li class="client">
                                <asp:Label Text="" runat="server" ID="Pax_Main_Div_Tab" />
                            </li>
                            <li class="agent">
                                <asp:Label Text="" runat="server" ID="Pax_Main_Div_Agent_Tab" />
                            </li>
                            <li class="destination hidden"></li>
                        </ul>
                    </div>

                    <div class="clearfix"></div>
                    <div class="self-payment">
                        <header class="section-header text-center">
                            <h3 style="color: #3f3f3f; font-size: 14px;">WE ARE ATOL &amp; IATA BONDED</h3>
                            <img src="../images/client_quotes_images/atollogo.png" alt="Atol Protected" class="img-responsive" />
                            <h3 style="color: #3f3f3f; font-size: 11px;">YOUR MONEY 100% SECURE  WITH US</h3>
                        </header>

                    </div>
                    <div class="quote-sidebar">
                        <ul class="info-tab">
                            <li class="client" style="margin-top: 34px!important">
                                <label>Note : </label>
                                <asp:TextBox ID="txtNote" ClientIDMode="Static" Style="width: 100%!important" CssClass="form-control" TextMode="MultiLine" Rows="5" runat="server">
                                </asp:TextBox>

                                <label>Amount:</label>
                                <asp:TextBox ID="txtPrice" ClientIDMode="Static" Style="width: 100%!important" CssClass="form-control mb-10" runat="server"></asp:TextBox>

                                <label>From Email ID:</label>
                                <asp:TextBox ID="txtXPFrom" ClientIDMode="Static" pattern="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$" Style="width: 100%!important"
                                    CssClass="form-control mb-10" runat="server"></asp:TextBox>

                                <label>To Email ID:</label>
                                <asp:TextBox ID="txtXPTo" ClientIDMode="Static" pattern="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$" Style="width: 100%!important"
                                    CssClass="form-control mb-10" runat="server"></asp:TextBox>

                                <asp:Button ID="btnSend" ClientIDMode="Static" runat="server" Text="Send" OnClientClick="return SendEmailQuotation();"
                                    CssClass="btn btn-success btn-block" OnClick="btnSend_Click" />
                                <asp:Label ID="ltrMsg" runat="server" Style="color: green"></asp:Label>
                            </li>
                        </ul>
                    </div>
                </div>
            </section>
            <section class="mt-30 flight-pattern-bg">
                <asp:Label Text="" runat="server" ID="Footer_Layout_Tab" />
            </section>
        </div>

        <div id="tab-2" class="tab-contentmain">
            <asp:TextBox CssClass="textEditor1" ClientIDMode="Static" ID="txtEditor" TextMode="MultiLine" runat="server" placeholder="" />
            <br />
            <asp:Button ID="btnSendXP" Visible="false" runat="server" Text="Send" CssClass="btn btn-default" />
        </div>

    </div>
    <br />
    <br />



    <style type="text/css">
        .aw-icon-7-t {
            background-image: url(https://vortex.accuweather.com/adc2010/../images/client_quotes_images/icons-numbered/07-t.png)
        }

        .aw-icon-7-h {
            background-image: url(https://vortex.accuweather.com/adc2010/images/icons-numbered/07-h.png)
        }

        .aw-icon-7-s {
            background-image: url(https://vortex.accuweather.com/adc2010/images/icons-numbered/07-s.png)
        }

        .aw-icon-7-m {
            background-image: url(https://vortex.accuweather.com/adc2010/images/icons-numbered/07-m.png)
        }

        .aw-icon-7-l {
            background-image: url(https://vortex.accuweather.com/adc2010/images/icons-numbered/07-l.png)
        }

        .aw-icon-7-xl {
            background-image: url(https://vortex.accuweather.com/adc2010/images/icons-numbered/07-xl.png)
        }

        .affix {
            top: 20px;
            width: 265px;
        }

        .textss {
            overflow: hidden;
            display: -webkit-box;
            -webkit-line-clamp: 3;
        }

        ul.tabs {
            margin: 0px;
            padding: 0px;
        }

            ul.tabs li {
                background: none;
                color: #222;
                padding: 10px 15px;
                cursor: pointer;
                display: inline-block;
            }

                ul.tabs li.current {
                    background: #1e4b86;
                    color: #fff;
                    border: solid;
                    border-width: thin;
                }

        .tab-contentmain {
            display: none;
            background: #efefef;
            padding: 10px 5px;
        }

            .tab-contentmain.current {
                display: inherit;
            }

        .jqte_tool.jqte_tool_1 .jqte_tool_label {
            position: relative;
            display: block;
            padding: 3px;
            width: 70px;
            height: 21px; /*change*/
            overflow: hidden;
        }

        .jqte_tool_label {
            padding-top: 1px !important; /*add*/
            height: 25px !important; /*add*/
        }

        .weather-info-details {
            position: absolute;
            right: 5%;
            top: 10%;
        }
        
            .weather-bg-box {
            position: relative;
            width: 100%;
            height: 182px;
            background: url(../images/client_quotes_images/weather-info-bg-banner.jpg) left top no-repeat;
            }
            </style>


    <script lang="javascript" type="text/javascript">
        var awxWidgetInfo = awxWidgetInfo ||
        {
        };
        awxWidgetInfo.awcc1486058327989 =
        {
        };
        awxWidgetInfo.awcc1486058327989.userInfo = { country: 'US', city: "New York", state: 'NY', metro: 'NYC', zip: '10007', fcode: 'NYC', partner: 'accuweather', sessionPartner: 'accuweather', referer: 'http://online.travelunravel.com/', lang: 'en-us', langid: '1', lat: '40.779', lon: '-73.969', dma: '501', ip: '1.22.119.36', ipLocation: '', sessionViews: 0, geo_dma: '', geo_city: '', geo_state: '', geo_zip: '', geo_country: '' };
        awxWidgetInfo.awcc1486058327989.wxInfo = { nwsalrt: '', hdln: '', ut: '', cu: { wx: '', hi: '', wd: '', hd: '', uv: '' }, fc: [{ wx: '', hi: '', lo: '' }], ix: {}, mcpct: "1" };
    </script>

    <script>



        //Speical Request Checkbox
        $(document).ready(function () {
            $('input[type="checkbox"]').on('change', function () {
                $('input[name="' + this.name + '"]').not(this).prop('checked', false);

            });
        });

        //NO XP Editor
        $('.textEditor1').jqte();


        //Main Tabs Switching
        $(document).ready(function () {
            $('ul.tabs li').click(function () {
                var tab_id = $(this).attr('data-tab');
                $('ul.tabs li').removeClass('current');
                $('.tab-contentmain').removeClass('current');
                $(this).addClass('current');
                $("#" + tab_id).addClass('current');
            })
        });
        function SendEmailQuotation() {
            waitingDialog.show('Sending Quotation...');
            return true;
        }
        function SendSearchQuotation() {
            if (txtInvoice != null && txtInvoice != "" && txtInvoice != undefined) {
                waitingDialog.show('Sending Quotation...');
            }
            else {
                alert("Please enter Invoice No.");
            }
            return true;
        }

    </script>

    <script lang='javascript'> 
        function alertMessage() {
            alert("Quotation Sent Successfully! Please check your Email for futher process.");
        }

    </script>
</asp:Content>


