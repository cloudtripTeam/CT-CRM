<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl" xmlns:func="urn:actl-xslt">
  <xsl:output method="html" indent="yes"/>
  <xsl:key name="GrandTotal" match="//GrandTotal/text()" use="." />
  <xsl:key name="Airlines" match="//ValCarrier/text()" use="." />
  <xsl:key name="keyEmpByName" match="Itineraries/Itinerary/Sectors/Sector/AirlineName/text()" use="."/>
  <xsl:param name="sourceMedia" select="'sourceMedia'"/>

  <xsl:template match="Itineraries">
    <xsl:variable name="FareTypes" select="Availability"></xsl:variable>

    <input type="hidden" id="CountItin" value="{count(//Itineraries/Itinerary)}" />
    <input type="hidden" id="hdnSorOrder" value="desc" />
    <xsl:variable name="TotalRecords" select="count(Itinerary)"></xsl:variable>
    <input type="hidden" id="hdnTotalRecords" value="{count(Itinerary)}" />
    <div id="table1">
      <xsl:for-each select="Itinerary">
        <xsl:sort select="GrandTotal" order="ascending" data-type ="number" />
        <xsl:variable name="InboundNo" select="count(Sectors/Sector[isReturn='false'])"/>
        <xsl:variable name="OutboundNo" select="count(Sectors/Sector[isReturn='true'])"/>
        <xsl:variable name="idval" select="generate-id()"></xsl:variable>
        <xsl:variable name='Provider' select='Provider'></xsl:variable>
        <xsl:variable name='DivFlightProvider' select='Provider'></xsl:variable>
        <xsl:variable name='Indx' select='IndexNumber'></xsl:variable>
        <xsl:variable name='DivFlightIndex' select='IndexNumber'></xsl:variable>
        <xsl:variable name ="DivFlightID" select ="concat($DivFlightProvider,$DivFlightIndex)"></xsl:variable>
        <div id="{concat('MainDivFlight',$DivFlightID)}" >
          <div id="{concat('DivFlight',$DivFlightID)}" class="booking-item">
            <div style="display:none;">
              <div id="{concat('GrandTotal','DivFlight',$DivFlightID)}" class="pond_value">
                <xsl:value-of select='format-number((number(Adult/AdTax)+number(Adult/AdtBFare)+number(Adult/markUp)),"#.00")'/>
              </div>
              <div id="{concat('AirLineStops','DivFlight',$DivFlightID)}" style="display:none;">
                <xsl:value-of select="number(count(Sectors/Sector[isReturn='false']))-number(1)+sum(Sectors/Sector[isReturn='false']/TechStopOver)"/>
              </div>
              <xsl:for-each select="Sectors/Sector[isReturn='false']">
                <div id="{concat('DepartureTime',position(),'DivFlight',$DivFlightID)}" style="float:left;">
                  <xsl:value-of select="Departure/Time"/>
                </div>
              </xsl:for-each>
              <xsl:for-each select="Sectors/Sector[isReturn='true']">
                <div  id="{concat('ReturnAirpCode',position(),'DivFlight',$DivFlightID)}" class="norm_fnt" style="float:left; display:none;">
                  ( <xsl:value-of select="Departure/AirpCode"/>) :<xsl:value-of select="Departure/CountryName"/>
                </div>
                <div id="{concat('ReturnTime',position(),'DivFlight',$DivFlightID)}" style="float:left;">
                  <xsl:value-of select="Departure/Time"/>
                </div>
              </xsl:for-each>
              <div id="{concat('ActualInTimeDivFlight',$DivFlightID)}" style="display:none;">
                <xsl:value-of select="Sectors/Sector[isReturn='true'][last()]/ActualTime"/>
              </div>
              <div id="{concat('ActualOutTimeDivFlight',$DivFlightID)}" style="display:none;">
                <xsl:value-of select="Sectors/Sector[isReturn='false'][last()]/ActualTime"/>
              </div>

              <xsl:for-each select ="Sectors/Sector[1][isReturn='false']">
                <div id="{concat('AirpCode',position(),'DivFlight',$DivFlightID)}" class="norm_fnt" style="float:left; display:none;">
                  ( <xsl:value-of select="Departure/AirpCode"/>) <xsl:value-of select="Departure/CityName"/>
                </div>
              </xsl:for-each>

              <div id="{concat('AirLineName2','DivFlight',$DivFlightID)}" style="display:none; color:#fff;">
                <xsl:value-of select="Sectors/Sector[1]/AirV"/>
              </div>

              <xsl:for-each select="Sectors/Sector[isReturn='false']">
                <div id="{concat('AirLineName',position(),'DivFlight',$DivFlightID)}" style="float:left;">
                  <xsl:value-of select="AirlineName"/>
                </div>
              </xsl:for-each>

            </div>

            <div style="display:none;" id="ResultAirVDiv">
              <xsl:value-of select ="Sectors/Sector[1]/AirV"/>
            </div>
            <div id="{concat('Div',position())}" class="row ">
              <div class="col-md-10">
                <div class="row booking-outer">
                  <xsl:variable name ="StopInfoOut" select ="concat('StopInfoOut',$DivFlightIndex)"></xsl:variable>
                  <div class="col-md-3">
                    <div class="booking-item-airline-logo">
                      <div style="width:40%; float:left;">
                        <i class="fa fa-plane fa-2x"></i>
                      </div>
                      <div style="width:59%; text-align:center; float:right;">
                        <xsl:element name="img">
                          <xsl:attribute name ="src">
                            <xsl:value-of select="concat('AirlineLogo/',Sectors/Sector[isReturn='false'][1]/AirV,'s.gif')"/>
                            <!--<xsl:call-template name="string-replace-all">
                          <xsl:with-param name="text" select="Sectors/Sector[isReturn='false'][1]/AirlineLogoPath" />
                          <xsl:with-param name="replace" select="'http:'" />
                          <xsl:with-param name="by" select="'https:'" />
                        </xsl:call-template>-->
                          </xsl:attribute>
                          <xsl:attribute name="alt">
                            <xsl:value-of select="Sectors/Sector[isReturn='false'][1]/AirlineName"/>
                          </xsl:attribute>
                          <xsl:attribute name="title">
                            <xsl:value-of select="Sectors/Sector[isReturn='false'][1]/AirlineName"/>
                          </xsl:attribute>
                        </xsl:element>
                       
                      </div>
                      <div style="clear:both"></div>

                      <div style="width:100%; text-align:right;">
                        <xsl:value-of select="Sectors/Sector[isReturn='false'][1]/AirlineName"/>                    
                      </div> 
                        <xsl:variable name="varAirName">
                          <xsl:value-of select="Sectors/Sector[isReturn='false'][1]/AirlineName"/>
                        </xsl:variable>
                        
                        <input type="hidden" id="{concat('hdnAirName',position())}" value="{$varAirName}" />
                        <xsl:variable name="SecCount" select="count(Sectors/Sector)"/>
                        <xsl:variable name="Sec1" select="count(Sectors/Sector[isReturn='true'])"/>
                        <input type="hidden" id="{concat('hdnStarRating',position())}" value="{$InboundNo}" />
                        <span style="color:white;">
                          <xsl:value-of select='Provider'/>
                          <xsl:text>_</xsl:text>
                          <xsl:value-of select='FareType'/>
                        </span>
                    </div>
                  </div>
                  <div class="col-md-9">
                    <div class="booking-item-flight-details">
                      <div class="booking-item-arrival right-border">
                        <p class="booking-item-destination">
                          <xsl:value-of select="Sectors/Sector[isReturn='false'][1]/Departure/CityName"/>
                          <xsl:text>, </xsl:text>
                          <xsl:value-of select="Sectors/Sector[isReturn='false'][1]/Departure/CountryCode"/>
                          <xsl:text>, </xsl:text>
                          <xsl:value-of select="Sectors/Sector[isReturn='false'][1]/Departure/CountryName"/>
                          <xsl:text> (</xsl:text>
                          <xsl:value-of select="Sectors/Sector[isReturn='false'][1]/Departure/AirpCode"/>
                          <xsl:text>)</xsl:text>
                        </p>
                        <p class="booking-item-date">
                          <xsl:element name="span">
                            <xsl:value-of select="Sectors/Sector[isReturn='false'][1]/Departure/Time"/>
                            <xsl:text>, </xsl:text>
                            <xsl:variable name="month">
                              <xsl:call-template name ="GetMonthName">
                                <xsl:with-param name ="Month" select ="substring(Sectors/Sector[isReturn='false'][1]/Departure/Date,4,2)"></xsl:with-param>
                              </xsl:call-template>
                            </xsl:variable>
                            <xsl:value-of select="concat(substring(Sectors/Sector[isReturn='false'][1]/Departure/Date,1,2),' ',$month,' ',substring(Sectors/Sector[isReturn='false'][1]/Departure/Date,7,4))"/>
                          </xsl:element>
                        </p>
                      </div>
                      <div class="booking-item-arrival-stop right-border">
                        <h5>
                          <xsl:for-each select="Sectors/Sector[position()=$InboundNo]">
                            <xsl:value-of select="concat(substring(ActualTime,1,2),' ')"/>h&#160;<xsl:value-of select="concat(substring(ActualTime,4,2),' ')"/>m
                          </xsl:for-each>
                          <!--22h 50m-->
                        </h5>
                        <img src="images/re-arrow.png" ></img>
                        <p>
                          <!--non-stop-->
                          <span id="lblStopOver">
                            <xsl:if test="count(Sectors/Sector[isReturn='false'])-1 =0 and Sectors/Sector[isReturn='false']/TechStopOver =0">

                              <a class="tooltip101" style="cursor:pointer;color:#0A4B80; margin-left:30px;text-decoration:underline;font-weight:normal;" onmouseover="tooltip.pop(this, '{concat('#',$StopInfoOut,$DivFlightID)}')">
                                <div id="{concat('FltStops',$DivFlightID)}" style="float:left;display:none">
                                  <xsl:value-of select="number(count(Sectors/Sector[isReturn='false']))-number(1)"/>
                                  <xsl:text>Non Stop</xsl:text>
                                </div>
                              </a>
                            </xsl:if>
                            <xsl:if test="count(Sectors/Sector[isReturn='false'])-1 =0 and Sectors/Sector[isReturn='false']/TechStopOver >0">
                              <a class="tooltip101" style="cursor:pointer;color:#0A4B80;margin-left:30px; text-decoration:underline;font-weight:normal;" onmouseover="tooltip.pop(this, '{concat('#',$StopInfoOut,$DivFlightID)}')">
                                <div id="{concat('FltStops',$DivFlightID)}" style="float:left;display:none">
                                  <xsl:value-of select="number(count(Sectors/Sector[isReturn='false']))-number(1)"/>
                                  <xsl:text>Tech. Stop</xsl:text>
                                </div>
                              </a>
                            </xsl:if>
                            <xsl:if test="count(Sectors/Sector[isReturn='false'])-1 >0">
                              <a class="tooltip101" style="cursor:pointer;color:#0A4B80;text-decoration:underline;font-weight:normal;" onmouseover="tooltip.pop(this, '{concat('#',$StopInfoOut,$DivFlightID)}')">
                                <div id="{concat('FltStops',$DivFlightID)}" style="float:left; margin-left:30px; font-weight:normal;color:#0A4B80;">
                                  <xsl:value-of select="number(count(Sectors/Sector[isReturn='false']))-number(1)"/>
                                  <xsl:text> Stop</xsl:text>
                                </div>
                              </a>
                            </xsl:if>
                          </span>
                          <div style="display:none;height: auto; width: auto;">
                            <div align="center" style="width:auto; height: auto;" >
                              <div id="{concat($StopInfoOut,$DivFlightID)}" style="width:auto;font-size:11px;">
                                <table width="100%" cellpadding="0" cellspacing="0">
                                  <tr valign="middle" style="height:22px;background-color: #1e4b86; color:#fff; font-size:14px;font-weight:bold;">
                                    <td colspan="2" align="center">
                                      <xsl:if test="count(Sectors/Sector[isReturn='false'])-1 =0 and Sectors/Sector[isReturn='false']/TechStopOver =0">
                                        Non Stop Flight
                                      </xsl:if>
                                      <xsl:if test="count(Sectors/Sector[isReturn='false'])-1 =0 and Sectors/Sector[isReturn='false']/TechStopOver >0">
                                        Technical Stop Flight
                                      </xsl:if>
                                      <xsl:if test="count(Sectors/Sector[isReturn='false'])-1 >0">
                                        Stop Over Information
                                      </xsl:if>
                                    </td>
                                  </tr>
                                  <tr>
                                    <td style="height:5px;">
                                    </td>
                                  </tr>
                                  <xsl:for-each select ="Sectors/Sector[isReturn='false']">
                                    <xsl:if test ="position() !=1">
                                      <tr valign="top" style="height:22px; font-size:13px;">
                                        <td style="padding-left:10px;">
                                          <b> This flight stop at:-</b>
                                        </td>
                                        <td style="padding-left:10px;padding-right:10px;">
                                          <xsl:value-of select ="concat(Departure/CityName,' ','-',' ',Departure/AirpName)"/>
                                        </td>
                                      </tr>
                                      <tr style="height:22px; font-size:13px;">
                                        <td style="padding-left:10px;">
                                          <b>  Stop over time:-</b>
                                        </td>
                                        <td style="padding-left:10px;">
                                          <xsl:value-of select ="substring(TransitTime/@time,1,2)"/>
                                          <xsl:text>hrs </xsl:text>
                                          <xsl:value-of select="substring(TransitTime/@time,4,2)"/>
                                          <xsl:text>min</xsl:text>
                                        </td>
                                      </tr>
                                    </xsl:if>
                                  </xsl:for-each>
                                  <tr style="height:22px; font-size:13px; padding-bottom:10px; ">
                                    <td style="padding-left:10px;">
                                      <b>  Total trip time:-</b>
                                    </td>
                                    <td style="padding-left:10px;">
                                      <xsl:value-of select="substring(Sectors/Sector[isReturn='false'][last()]/ActualTime,1,2)"/>
                                      <xsl:text>hrs </xsl:text>
                                      <xsl:value-of select="substring(Sectors/Sector[isReturn='false'][last()]/ActualTime,4,2)"/>
                                      <xsl:text>min</xsl:text>
                                    </td>
                                  </tr>
                                </table>
                              </div>
                            </div>
                          </div>
                        </p>
                      </div>
                      <div class="booking-item-arrival ">
                        <p class="booking-item-destination">
                          <xsl:value-of select="Sectors/Sector[isReturn='false'][last()]/Arrival/CityName"/>
                          <xsl:text>, </xsl:text>
                          <xsl:value-of select="Sectors/Sector[isReturn='false'][last()]/Arrival/CountryCode"/>
                          <xsl:text>, </xsl:text>
                          <xsl:value-of select="Sectors/Sector[isReturn='false'][last()]/Arrival/CountryName"/>
                          <xsl:text> (</xsl:text>
                          <xsl:value-of select="Sectors/Sector[isReturn='false'][last()]/Arrival/AirpCode"/>
                          <xsl:text>)</xsl:text>
                        </p>
                        <p class="booking-item-date">
                          <!--12:25 PM , Sat, Mar 23-->
                          <xsl:element name="span">
                            <xsl:value-of select="Sectors/Sector[isReturn='false'][last()]/Arrival/Time"/>
                            <xsl:text>, </xsl:text>
                            <xsl:variable name="month">
                              <xsl:call-template name ="GetMonthName">
                                <xsl:with-param name ="Month" select ="substring(Sectors/Sector[isReturn='false'][last()]/Arrival/Date,4,2)"></xsl:with-param>
                              </xsl:call-template>
                            </xsl:variable>
                            <xsl:value-of select="concat(substring(Sectors/Sector[isReturn='false'][last()]/Arrival/Date,1,2),' ',$month,' ',substring(Sectors/Sector[isReturn='false'][last()]/Arrival/Date,7,4))"/>
                          </xsl:element>
                        </p>

                      </div>
                    </div>
                  </div>
                </div>
                <xsl:if test ="count(Sectors/Sector[isReturn='true']) &gt; 0">
                  <xsl:variable name ="StopInfoIn" select ="concat('StopInfoIn',$DivFlightIndex)"></xsl:variable>
                  <div class="row booking-outers">                  
                    <div class="col-md-3">
                    <div class="booking-item-airline-logo">
                      <div style="width:40%; float:left;">
                          <i class="fa fa-plane fa-2x fa-flip-vertical"></i>
                      </div>
                      <div style="width:59%; text-align:center; float:right;">
                        <xsl:element name="img">
                          <xsl:attribute name ="src">
                            <xsl:value-of select="concat('AirlineLogo/',Sectors/Sector[isReturn='true'][1]/AirV,'s.gif')"/>
                            <!--<xsl:call-template name="string-replace-all">
                            <xsl:with-param name="text" select="Sectors/Sector[isReturn='true'][1]/AirlineLogoPath" />
                            <xsl:with-param name="replace" select="'http:'" />
                            <xsl:with-param name="by" select="'https:'" />
                          </xsl:call-template>-->
                          </xsl:attribute>
                          <xsl:attribute name="alt">
                            <xsl:value-of select="Sectors/Sector[isReturn='true'][1]/AirlineName"/>
                          </xsl:attribute>
                          <xsl:attribute name="title">
                            <xsl:value-of select="Sectors/Sector[isReturn='true'][1]/AirlineName"/>
                          </xsl:attribute>
                        </xsl:element>                       
                      </div>
                      <div style="clear:both"></div>
                      <div style="width:100%; text-align:right;"> 
                        <xsl:value-of select="Sectors/Sector[isReturn='true'][1]/AirlineName"/>                       
                      </div>
                    </div>
                  </div>
                    <div class="col-md-9">
                      <div class="booking-item-flight-details">
                        <div class="booking-item-arrival right-border">
                          <p class="booking-item-destination">
                            <!--New York, NY, United States (JFK)-->
                            <xsl:value-of select="Sectors/Sector[isReturn='true'][1]/Departure/CityName"/>
                            <xsl:text>, </xsl:text>
                            <xsl:value-of select="Sectors/Sector[isReturn='true'][1]/Departure/CountryCode"/>
                            <xsl:text>, </xsl:text>
                            <xsl:value-of select="Sectors/Sector[isReturn='true'][1]/Departure/CountryName"/>
                            <xsl:text> (</xsl:text>
                            <xsl:value-of select="Sectors/Sector[isReturn='true'][1]/Departure/AirpCode"/>
                            <xsl:text>)</xsl:text>

                          </p>
                          <p class="booking-item-date">
                            <!--12:25 PM , Sat, Mar 23-->

                            <xsl:element name="span">
                              <xsl:value-of select="Sectors/Sector[isReturn='true'][1]/Departure/Time"/>
                              <xsl:text>, </xsl:text>
                              <xsl:variable name="month">
                                <xsl:call-template name ="GetMonthName">
                                  <xsl:with-param name ="Month" select ="substring(Sectors/Sector[isReturn='true'][1]/Departure/Date,4,2)"></xsl:with-param>
                                </xsl:call-template>
                              </xsl:variable>
                              <xsl:value-of select="concat(substring(Sectors/Sector[isReturn='true'][1]/Departure/Date,1,2),' ',$month,' ',substring(Sectors/Sector[isReturn='true'][1]/Departure/Date,7,4))"/>
                            </xsl:element>
                          </p>
                        </div>
                        <div class="booking-item-arrival-stop right-border">
                          <h5>
                            <!--22h 50m-->
                            <xsl:for-each select="Sectors/Sector[last()]">
                              <xsl:value-of select="concat(substring(ActualTime,1,2),' ')"/>h&#160;<xsl:value-of select="concat(substring(ActualTime,4,2),' ')"/>m
                            </xsl:for-each>
                          </h5>
                          <img src="images/re-arrow.png" ></img>
                          <p>
                            <span id="lblRStopOver">
                              <xsl:if test="count(Sectors/Sector[isReturn='true'])-1 =0 and Sectors/Sector[isReturn='true']/TechStopOver =0">
                                <a class="tooltip101" style="cursor:pointer;color:#0A4B80; margin-left:30px;text-decoration:underline;font-weight:normal;" onmouseover="tooltip.pop(this, '{concat('#',$StopInfoIn,$DivFlightID)}')">
                                  <xsl:text>Non Stop</xsl:text>
                                </a>
                              </xsl:if>
                              <xsl:if test="count(Sectors/Sector[isReturn='true'])-1 =0 and Sectors/Sector[isReturn='true']/TechStopOver >0">
                                <a class="tooltip101" style="cursor:pointer;color:#0A4B80;margin-left:30px; text-decoration:underline;font-weight:normal;" onmouseover="tooltip.pop(this, '{concat('#',$StopInfoIn,$DivFlightID)}')">
                                  <xsl:text>Tech. Stop</xsl:text>
                                </a>
                              </xsl:if>
                              <xsl:if test="count(Sectors/Sector[isReturn='true'])-1 >0">
                                <a class="tooltip101" style="cursor:pointer;color:#0A4B80;text-decoration:underline;font-weight:normal;" onmouseover="tooltip.pop(this, '{concat('#',$StopInfoIn,$DivFlightID)}')">
                                  <div style="float:left;color:#0A4B80;font-weight:normal; margin-left:30px;">
                                    <xsl:value-of select="number(count(Sectors/Sector[isReturn='true']))-number(1)"/>
                                    <xsl:text>Stop</xsl:text>
                                  </div>
                                </a>
                              </xsl:if>
                            </span>
                            <div style="display:none;height: auto; width: auto;">
                              <div align="center" class="filtarHead" style="width:auto; height: auto;" >
                                <div id="{concat($StopInfoIn,$DivFlightID)}" style="width: auto;font-size:11px;">
                                  <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr valign="middle" style="height:22px;background-color: #1e4b86; color:#fff; font-size:14px;font-weight:bold;">
                                      <td colspan="2" align="center">
                                        <xsl:if test="count(Sectors/Sector[isReturn='true'])-1 =0 and Sectors/Sector[isReturn='true']/TechStopOver =0">
                                          Non Stop Flight
                                        </xsl:if>
                                        <xsl:if test="count(Sectors/Sector[isReturn='true'])-1 =0 and Sectors/Sector[isReturn='true']/TechStopOver >0">
                                          Technical Stop Flight
                                        </xsl:if>
                                        <xsl:if test="count(Sectors/Sector[isReturn='true'])-1 >0">
                                          Stop Over Information
                                        </xsl:if>
                                      </td>
                                    </tr>
                                    <tr>
                                      <td style="height:5px;">
                                      </td>
                                    </tr>
                                    <xsl:for-each select ="Sectors/Sector[isReturn='true']">
                                      <xsl:if test ="position() !=1">
                                        <tr valign="top" style="height:22px; font-size:13px;">
                                          <td style="padding-left:10px;">
                                            This flight stop at:-
                                          </td>
                                          <td style="padding-left:10px;padding-right:10px;">
                                            <xsl:value-of select ="concat(Departure/CityName,' ','-',' ',Departure/AirpName)"/>
                                          </td>
                                        </tr>
                                        <tr style="height:22px; font-size:13px;">
                                          <td style="padding-left:10px;">
                                            Stop over time:-
                                          </td>
                                          <td style="padding-left:10px;">
                                            <xsl:value-of select ="substring(TransitTime/@time,1,2)"/>
                                            <xsl:text>hrs </xsl:text>
                                            <xsl:value-of select="substring(TransitTime/@time,4,2)"/>
                                            <xsl:text>min</xsl:text>
                                          </td>
                                        </tr>
                                      </xsl:if>
                                    </xsl:for-each>
                                    <tr style="height:22px; font-size:13px;">
                                      <td style="padding-left:10px;">
                                        Total trip time:-
                                      </td>
                                      <td style="padding-left:10px;">
                                        <xsl:value-of select="substring(Sectors/Sector[isReturn='true'][last()]/ActualTime,1,2)"/>
                                        <xsl:text>hrs </xsl:text>
                                        <xsl:value-of select="substring(Sectors/Sector[isReturn='true'][last()]/ActualTime,4,2)"/>
                                        <xsl:text>min</xsl:text>
                                      </td>
                                    </tr>
                                  </table>
                                </div>
                              </div>
                            </div>
                          </p>
                        </div>
                        <div class="booking-item-arrival ">
                          <p class="booking-item-destination">
                            <!--New York, NY, United States (JFK)-->
                            <xsl:value-of select="Sectors/Sector[isReturn='true'][last()]/Arrival/CityName"/>
                            <xsl:text>, </xsl:text>
                            <xsl:value-of select="Sectors/Sector[isReturn='true'][last()]/Arrival/CountryCode"/>
                            <xsl:text>, </xsl:text>
                            <xsl:value-of select="Sectors/Sector[isReturn='true'][last()]/Arrival/CountryName"/>
                            <xsl:text> (</xsl:text>
                            <xsl:value-of select="Sectors/Sector[isReturn='true'][last()]/Arrival/AirpCode"/>
                            <xsl:text>)</xsl:text>
                          </p>
                          <p class="booking-item-date">
                            <!--12:25 PM , Sat, Mar 23-->
                            <xsl:element name="span">
                              <xsl:value-of select="Sectors/Sector[isReturn='true'][last()]/Arrival/Time"/>
                              <xsl:text>, </xsl:text>
                              <xsl:variable name="month">
                                <xsl:call-template name ="GetMonthName">
                                  <xsl:with-param name ="Month" select ="substring(Sectors/Sector[isReturn='true'][last()]/Arrival/Date,4,2)"></xsl:with-param>
                                </xsl:call-template>
                              </xsl:variable>
                              <xsl:value-of select="concat(substring(Sectors/Sector[isReturn='true'][last()]/Arrival/Date,1,2),' ',$month,' ',substring(Sectors/Sector[isReturn='true'][last()]/Arrival/Date,7,4))"/>
                            </xsl:element>
                          </p>
                        </div>
                      </div>
                    </div>
                  </div>
                </xsl:if>
              </div>
              <div class="col-md-2">
                <xsl:variable name="AirCode" select="Sectors/Sector[isReturn='false'][1]/AirV"></xsl:variable>
                <xsl:variable name="AirClass" select="Sectors/Sector[isReturn='false'][1]/CabinClass/Des"></xsl:variable>
                <xsl:variable name="noOfPax">
                  <xsl:variable name="noOfAdult">
                    <xsl:value-of select="Adult/NoAdult"></xsl:value-of>
                  </xsl:variable>
                  <xsl:variable name="noOfChild">
                    <xsl:choose>
                      <xsl:when test ="Child">
                        <xsl:value-of select="Child/NoChild"></xsl:value-of>
                      </xsl:when>
                      <xsl:otherwise>
                        <xsl:value-of select ="'0'"/>
                      </xsl:otherwise>
                    </xsl:choose>
                  </xsl:variable>
                  <xsl:variable name="noOfInfant">
                    <xsl:choose>
                      <xsl:when test ="Infant">
                        <xsl:value-of select="Infant/NoInfant"></xsl:value-of>
                      </xsl:when>
                      <xsl:otherwise>
                        <xsl:value-of select ="'0'"/>
                      </xsl:otherwise>
                    </xsl:choose>
                  </xsl:variable>
                  <xsl:value-of select="number($noOfAdult)+number($noOfChild)+number($noOfInfant)"></xsl:value-of>
                </xsl:variable>

                <xsl:if test ="count(Sectors/Sector[isReturn='true']) &gt; 0">
                  <xsl:if test="number($noOfPax)= 1">
                    <span class="booking-item-price">
                      £<xsl:value-of select="GrandTotal"/>
                      <div id="{concat('CostDivFlight',$DivFlightID)}" style="display:none;">
                        <xsl:value-of select="GrandTotal"/>
                      </div>
                    </span>
                    <p class="booking-item-flight-class">
                      (including all taxes)
                    </p>
                  </xsl:if>
                  <xsl:if test="number($noOfPax)> 1">
                    <span class="booking-item-price">
                      £<xsl:value-of select ="format-number(number(GrandTotal) div number($noOfPax),'#.00')"></xsl:value-of>
                    </span>
                    <p class="booking-item-flight-class">
                      Best Avg Price
                    </p>
                    <p class="booking-item-flight-class">
                      Total £<xsl:value-of select="GrandTotal"/>
                    </p>
                    <p class="booking-item-flight-class">
                      (including all taxes)
                    </p>
                  </xsl:if>
                  <xsl:if test="$Provider = '1D'">
                <a onclick="ShowProgressAnimation()">
                  <input type="button">
                    <xsl:attribute name="value">
                      <xsl:value-of select ="'Call Now'"/>
                    </xsl:attribute>
                    <xsl:attribute name="class">
                      <xsl:value-of select ="'btn  btn-success'"/>
                    </xsl:attribute>
                    <xsl:attribute name ="onclick">
                      <xsl:value-of select ="func:GetQueryStringEnquiry($Indx,$Provider,$AirCode,$AirClass)"/>
                    </xsl:attribute>
                  </input>
                </a>
              </xsl:if><!---->
                  <xsl:if test="$Provider != '1D'">
                    <a onclick="ShowProgressAnimation()" >
                      <input type="button" style="margin-bottom:7px; margin-left:10px;">
                        <xsl:attribute name="value">
                          <xsl:value-of select ="'Book Now'"/>
                        </xsl:attribute>
                        <xsl:attribute name="class">
                          <xsl:value-of select ="'btn  btn-success'"/>
                        </xsl:attribute>
                        <xsl:attribute name ="onclick">
                          <xsl:value-of select ="func:GetQueryString($Indx,$Provider,$AirCode,$AirClass)"/>
                        </xsl:attribute>
                      </input>
                    </a>
                  </xsl:if>

                 
                </xsl:if>
                <xsl:if test ="count(Sectors/Sector[isReturn='true']) = 0">
                  <xsl:if test="number($noOfPax)= 1">
                    <span class="booking-item-price">
                      £<xsl:value-of select="GrandTotal"/>
                      <div id="{concat('CostDivFlight',$DivFlightID)}" style="display:none;">
                        <xsl:value-of select="GrandTotal"/>
                      </div>
                    </span>
                    <p class="booking-item-flight-class">
                      (including all taxes)
                    </p>
                  </xsl:if>
                  <xsl:if test="number($noOfPax)> 1">
                    <span class="booking-item-price">
                      £<xsl:value-of select ="format-number(number(GrandTotal) div number($noOfPax),'#.00')"></xsl:value-of>
                    </span>
                    <p class="booking-item-flight-class">
                      Best Avg Price
                    </p>
                    <p class="booking-item-flight-class">
                      Total £<xsl:value-of select="GrandTotal"/>
                    </p>
                    <p class="booking-item-flight-class">
                      (including all taxes)
                    </p>
                  </xsl:if>
                  <!--<xsl:if test="$Provider = '1D'">
                  <a onclick="ShowProgressAnimation()">
                    <input type="button">
                      <xsl:attribute name="value">
                        <xsl:value-of select ="'Book Now'"/>
                      </xsl:attribute>
                      <xsl:attribute name="class">
                        <xsl:value-of select ="'btn  btn-success'"/>
                      </xsl:attribute>
                      <xsl:attribute name ="onclick">
                        <xsl:value-of select ="func:GetQueryStringEnquiry($Indx,$Provider,$AirCode,$AirClass)"/>
                      </xsl:attribute>
                    </input>
                  </a>
                </xsl:if>-->
                  <xsl:if test="$Provider != '1D'">
                    <a onclick="ShowProgressAnimation()">
                      <input type="button">
                        <xsl:attribute name="value">
                          <xsl:value-of select ="'Book Now'"/>
                        </xsl:attribute>
                        <xsl:attribute name="class">
                          <xsl:value-of select ="'btn  btn-success'"/>
                        </xsl:attribute>
                        <xsl:attribute name ="onclick">
                          <xsl:value-of select ="func:GetQueryString($Indx,$Provider,$AirCode,$AirClass)"/>
                        </xsl:attribute>
                      </input>
                    </a>
                  </xsl:if>
                
                </xsl:if>
              </div>
            </div>
            <div class="row" >
              <div class="col-md-3"></div>
              <div class="col-md-9 res-font" >
                <a id="{concat($Indx,'-',$Provider)}" href="javascript:(void)"  data-toggle="modal" data-target=".bs-example-modal-lg"  onclick="FlightInfo(this); return false">
                  Show flight details
                </a>
              </div>
            </div>
          </div>
        </div>
      </xsl:for-each>
    </div>
  </xsl:template>

  <xsl:template name ="GetMonthName">
    <xsl:param name ="Month"></xsl:param>
    <xsl:choose>
      <xsl:when test ="$Month='01'">January</xsl:when>
      <xsl:when test ="$Month='02'">February</xsl:when>
      <xsl:when test ="$Month='03'">March</xsl:when>
      <xsl:when test ="$Month='04'">April</xsl:when>
      <xsl:when test ="$Month='05'">May</xsl:when>
      <xsl:when test ="$Month='06'">Jun</xsl:when>
      <xsl:when test ="$Month='07'">July</xsl:when>
      <xsl:when test ="$Month='08'">August</xsl:when>
      <xsl:when test ="$Month='09'">September</xsl:when>
      <xsl:when test ="$Month='10'">October</xsl:when>
      <xsl:when test ="$Month='11'">November</xsl:when>
      <xsl:when test ="$Month='12'">December</xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="string-replace-all">
    <xsl:param name="text" />
    <xsl:param name="replace" />
    <xsl:param name="by" />
    <xsl:choose>
      <xsl:when test="contains($text, $replace)">
        <xsl:value-of select="substring-before($text,$replace)" />
        <xsl:value-of select="$by" />
        <xsl:call-template name="string-replace-all">
          <xsl:with-param name="text"
					select="substring-after($text,$replace)" />
          <xsl:with-param name="replace" select="$replace" />
          <xsl:with-param name="by" select="$by" />
        </xsl:call-template>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="$text" />
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
</xsl:stylesheet>
