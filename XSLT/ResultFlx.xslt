<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl" xmlns:func="urn:actl-xslt">
  <xsl:output method="html" indent="yes"/>
  <xsl:param name="SourceMedia" select="'SourceMedia'"/>
  <xsl:param name="IsCalender" select="'IsCalender'"/>
  <xsl:template match="Itinerary">
    <xsl:variable name="InboundNo" select="count(Sectors/Sector[isReturn='false'])"/>
    <xsl:variable name="OutboundNo" select="count(Sectors/Sector[isReturn='true'])"/>
    <table width="960" border="0" align="center" cellpadding="0" cellspacing="0" class="inside-table">
      <tr>
        <td>
          <table width="100%" border="0" cellspacing="0" cellpadding="0" style="border: 1px Solid #c9c7c7;
                                                background-color: #f9e5d3;">
            <tr class="big-text-black">
              <td colspan="4" >
                <div class="avilability-main-top-text-color">  Outbound Flight Detail</div>
              </td>
              <td width="80%" style="color:#000000; padding-right:5px;" align="right">
                <xsl:for-each select="Sectors/Sector[position()=$InboundNo]">
                  Flights Duration:<xsl:value-of select="concat(substring(ActualTime,1,2),'')"/>hrs&#160;<xsl:value-of select="concat(substring(ActualTime,4,2),'')"/>mins
                </xsl:for-each>
              </td>
            </tr>
          </table>
        </td>
      </tr>
      <tr>
        <td width="100%">
          <xsl:for-each select="Sectors/Sector[isReturn = 'false']">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
              <xsl:if test ="position() !=1">
                <tr>
                  <td colspan="13" align="center" valign="top" style="color:#d41515; font-size: 12px; line-height: 10px; width:90%;height:  auto; text-align:center; padding-bottom: 5px;">
                    <xsl:text >Stop over time </xsl:text>
                    <xsl:value-of select ="substring(TransitTime/@time,1,2)"/>
                    <xsl:text>hrs </xsl:text>
                    <xsl:value-of select="substring(TransitTime/@time,4,2)"/>
                    <xsl:text>min</xsl:text>
                    <xsl:text > at </xsl:text>
                    <xsl:value-of select="Departure/CityCode"/>
                  </td>
                </tr>
              </xsl:if>
              <tr>
                <td colspan="13" style="height: 2px; border-top: 1px dotted #424242;">
                </td>
              </tr>
              <tr>
                <td width="1%"></td>
                <td width="4%">
                  Depart
                </td>
                <td width="2%"></td>
                <td width="31%" rowspan="3" align="left" valign="top" class="small-text-color">
                  <span>
                    <xsl:value-of select="Departure/AirpName"/><xsl:text>, </xsl:text>
                    <xsl:value-of select="Departure/CityName" /><xsl:text> </xsl:text>(<xsl:value-of select="Departure/AirpCode" />)
                  </span>
                  <div>
                    <xsl:element name="span">
                      <xsl:variable name="month">
                        <xsl:call-template name ="GetMonthName">
                          <xsl:with-param name ="Month" select ="substring(Departure/Date,4,2)"></xsl:with-param>
                        </xsl:call-template>
                      </xsl:variable>
                      <xsl:value-of select="concat(substring(Departure/Date,1,2),' ',$month,' ',substring(Departure/Date,7,4))"/>
                    </xsl:element>
                    <span>
                      <strong>
                        <xsl:text> at: </xsl:text>
                        <xsl:value-of select="Departure/Time"/>
                      </strong>
                    </span>
                  </div>
                  <!--<br/>-->

                </td>
                <td width="120px;">                 
                  <img src="images/result-arrow1.png"/>                 
                </td>
                <td width="5%" align="left" valign="middle">
                  Arrive
                </td>
                <td width="30%" rowspan="3" align="left" valign="top" class="small-text-color">
                  <span title="Bahrain Intl Arpt, Bahrain (BAH)">
                    <xsl:value-of select="Arrival/AirpName"/><xsl:text>, </xsl:text>
                    <xsl:value-of select="Arrival/CityName" /><xsl:text> </xsl:text>(<xsl:value-of select="Arrival/AirpCode" />)
                  </span>
                  <div>
                    <xsl:element name="span">
                      <xsl:variable name="month">
                        <xsl:call-template name ="GetMonthName">
                          <xsl:with-param name ="Month" select ="substring(Arrival/Date,4,2)"></xsl:with-param>
                        </xsl:call-template>
                      </xsl:variable>
                      <xsl:value-of select="concat(substring(Arrival/Date,1,2),' ',$month,' ',substring(Arrival/Date,7,4))"/>
                    </xsl:element>
                    <span>
                      <strong>
                        <xsl:text> at: </xsl:text>
                        <xsl:value-of select="Arrival/Time"/>
                      </strong>
                    </span>
                  </div>

                  <br/>
                  <span style="float: left; width: 42px;"></span>
                </td>
                <td width="6%" class="small-text-color">
                  <xsl:element name="img">                    
                    <xsl:attribute name ="src">
                      <xsl:call-template name="string-replace-all">
                        <xsl:with-param name="text" select="AirlineLogoPath" />
                        <xsl:with-param name="replace" select="'http:'" />
                        <xsl:with-param name="by" select="'https:'" />
                      </xsl:call-template>
                    </xsl:attribute>
                    <xsl:attribute name="title">
                      <xsl:value-of select="AirlineName"/>
                    </xsl:attribute>
                    <xsl:attribute name="alt">
                      <xsl:value-of select="AirlineName"/>
                    </xsl:attribute>
                  </xsl:element>
                </td>
                <td width="12%" class="small-texteco">
                  <xsl:value-of select="AirlineName"/>
                  <br/>
                  <xsl:value-of select="AirV"/>
                  <xsl:text>-</xsl:text>
                  <xsl:value-of select="FltNum"/>
                  <xsl:text> | </xsl:text>
                  <xsl:value-of select="CabinClass/Des"/>
                </td>
                <td width="1%"></td>
              </tr>
            </table>
          </xsl:for-each>
        </td>
      </tr>
      <xsl:if test="count(Sectors/Sector[isReturn='true']) &gt; 0">
        <tr>
          <td>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" style="border: 1px Solid #c9c7c7;
                                                background-color: #f9e5d3;">

              <tr class="big-text-black">
                <td width="0%">
                  <div class="avilability-main-top-text-color">Inbound Flights </div>
                </td>
                <td width="80%" style="color:#000000; padding-right:5px;" align="right">
                  <xsl:for-each select="Sectors/Sector[last()]">
                    Flights Duration:<xsl:value-of select="concat(substring(ActualTime,1,2),'')"/>hrs&#160;<xsl:value-of select="concat(substring(ActualTime,4,2),'')"/>mins
                  </xsl:for-each>
                </td>
              </tr>
            </table>
          </td>
        </tr>
        <tr>
          <td>
            <xsl:for-each select="Sectors/Sector[isReturn = 'true']">
              <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <xsl:if test ="position() !=1">
                  <tr>
                    <td colspan="13" align="center" valign="top" style="color:#d41515; font-size: 12px; line-height: 10px; width:90%;height:  auto; text-align:center; padding-bottom: 5px;">
                      <xsl:text >Stop over time </xsl:text>
                      <xsl:value-of select ="substring(TransitTime/@time,1,2)"/>
                      <xsl:text>hrs </xsl:text>
                      <xsl:value-of select="substring(TransitTime/@time,4,2)"/>
                      <xsl:text>min</xsl:text>
                      <xsl:text > at </xsl:text>
                      <xsl:value-of select="Departure/CityCode"/>
                    </td>
                  </tr>
                </xsl:if>
                <tr>
                  <td colspan="13" style="height: 2px; border-top: 1px dotted #424242;">
                  </td>
                </tr>
                <tr>
                  <td width="1%"></td>
                  <td width="4%">
                    Depart
                  </td>
                  <td width="2%"></td>
                  <td width="31%" rowspan="3" align="left" valign="top" class="small-text-color">

                    <span>
                      <xsl:value-of select="Departure/AirpName"/><xsl:text>, </xsl:text>
                      <xsl:value-of select="Departure/CityName" /><xsl:text> </xsl:text>(<xsl:value-of select="Departure/AirpCode" />)
                    </span>
                    <div>
                      <xsl:element name="span">
                        <xsl:variable name="month">
                          <xsl:call-template name ="GetMonthName">
                            <xsl:with-param name ="Month" select ="substring(Departure/Date,4,2)"></xsl:with-param>
                          </xsl:call-template>
                        </xsl:variable>
                        <xsl:value-of select="concat(substring(Departure/Date,1,2),$month,' ',substring(Departure/Date,7,4))"/>
                      </xsl:element>
                      <span>
                        <strong>
                          <xsl:text> at: </xsl:text>
                          <xsl:value-of select="Departure/Time"/>
                        </strong>
                      </span>
                    </div>

                    <br/>
                    <span style="float: left; width: 42px;"></span>
                  </td>
                  <td  width="150px" valign="middle">
                    <img src="images/result-arrow1.png"/>
                  </td>
                  <td width="5%" align="left" valign="middle">
                    Arrive
                  </td>
                  <td width="30%" rowspan="3" align="left" valign="top" class="small-text-color">
                    <span title="Bahrain Intl Arpt, Bahrain (BAH)">
                      <xsl:value-of select="Arrival/AirpName"/><xsl:text>, </xsl:text><!--<br/>-->
                      <xsl:value-of select="Arrival/CityName" /><xsl:text> </xsl:text>(<xsl:value-of select="Arrival/AirpCode" />)
                    </span>
                    <div>
                      <xsl:element name="span">
                        <xsl:variable name="month">
                          <xsl:call-template name ="GetMonthName">
                            <xsl:with-param name ="Month" select ="substring(Arrival/Date,4,2)"></xsl:with-param>
                          </xsl:call-template>
                        </xsl:variable>
                        <xsl:value-of select="concat(substring(Arrival/Date,1,2),' ',$month,' ',substring(Arrival/Date,7,4))"/>
                      </xsl:element>
                      <span>
                        <strong>
                          <xsl:text> at: </xsl:text>
                          <xsl:value-of select="Arrival/Time"/>
                        </strong>
                      </span>
                    </div>

                    <br/>
                    <span style="float: left; width: 42px;"></span>
                  </td>
                  <td width="6%" class="small-text-color">
                    <xsl:element name="img">
                      <xsl:attribute name ="src">
                        <xsl:call-template name="string-replace-all">
                          <xsl:with-param name="text" select="AirlineLogoPath" />
                          <xsl:with-param name="replace" select="'http:'" />
                          <xsl:with-param name="by" select="'https:'" />
                        </xsl:call-template>
                      </xsl:attribute>
                      <xsl:attribute name="title">
                        <xsl:value-of select="AirlineName"/>
                      </xsl:attribute>
                      <xsl:attribute name="alt">
                        <xsl:value-of select="AirlineName"/>
                      </xsl:attribute>
                    </xsl:element>
                  </td>
                  <td width="12%" class="small-texteco">
                    <xsl:value-of select="AirlineName"/>
                    <br/>
                    <xsl:value-of select="AirV"/>
                    <xsl:text>-</xsl:text>
                    <xsl:value-of select="FltNum"/>
                    <xsl:text> | </xsl:text>
                    <xsl:value-of select="CabinClass/Des"/>
                  </td>
                  <td width="1%"></td>
                </tr>
              </table>
            </xsl:for-each>
          </td>
        </tr>
      </xsl:if>
    </table>
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
