<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl" xmlns:func="urn:actl-xslt">
  <xsl:output method="html" indent="yes"/>
  <xsl:template match="Itinerary">
    <xsl:variable name="InboundNo" select="count(Sectors/Sector[isReturn='false'])"/>
    <xsl:variable name="OutboundNo" select="count(Sectors/Sector[isReturn='true'])"/>
    <table width="960" border="0" align="center" cellpadding="0" cellspacing="0" class="inside-table">
      <tr>
        <td>
          <table width="100%" border="0" cellspacing="0" cellpadding="0" style="border: 1px Solid #c9c7c7;
                                                background-color: #f9e5d3;">
            <tr class="big-text-black">
              <td width="0%">
              </td>
              <td width="20%" class="avilability-main-top-text-color">
                <xsl:variable name ="CountS" select ="count(Sectors/Sector[isReturn='false'])"></xsl:variable>
                <xsl:value-of select="Sectors/Sector[1]/Departure/CityName"/>
                <xsl:text> - </xsl:text>
                <xsl:value-of select="Sectors/Sector[$CountS]/Arrival/CityName"/>
              </td>
              <td width="80%" align="right">
                <xsl:for-each select="Sectors/Sector[position()=$InboundNo]">
                  Flights Duration:<xsl:value-of select="concat(substring(ActualTime,1,2),'')"/>hrs&#160;<xsl:value-of select="concat(substring(ActualTime,4,2),'')"/>mins
                </xsl:for-each>

              </td>
              <td width="1%" align="right">
              </td>
            </tr>
          </table>
        </td>
      </tr>
      <tr>
        <td width="100%">

          <xsl:for-each select="Sectors/Sector[isReturn = 'false']">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td colspan="12" style="height: 15px; border-top: 1px dotted #424242;">
                </td>
              </tr>
              <tr>
                <td width="1%">
                </td>
                <td width="4%">
                  <img src="https://crystaltravel.co.uk/images/small-plane-left.png"/>
                </td>
                <td width="1%">
                </td>
                <td width="6%" class="small-text-color">
                  <xsl:element name="img">
                    <xsl:attribute name="src">
                      <xsl:value-of select="AirlineLogoPath"/>
                    </xsl:attribute>
                    <xsl:attribute name="title">
                      <xsl:value-of select="AirlineName"/>
                    </xsl:attribute>
                    <xsl:attribute name="alt">
                      <xsl:value-of select="AirlineName"/>
                    </xsl:attribute>
                  </xsl:element>
                </td>
                <td width="12%" class="small-text-color">
                  <xsl:value-of select="AirlineName"/>
                </td>
                <td width="34%" rowspan="3" align="left" valign="top" class="small-text-color">
                  <div>
                    <xsl:element name="span">
                      <xsl:variable name="month">
                        <xsl:call-template name ="GetMonthName">
                          <xsl:with-param name ="Month" select ="substring(Departure/Date,4,2)"></xsl:with-param>
                        </xsl:call-template>
                      </xsl:variable>
                      <xsl:value-of select="concat($month,substring(Departure/Date,1,2), ',', ' ',substring(Departure/Date,7,4))"/>
                    </xsl:element>
                  </div>
                  <span>
                    <strong>
                      <xsl:value-of select="Departure/Time"/>
                    </strong>
                  </span>
                  <br/>
                  <span>
                    <xsl:value-of select="Departure/CityName"/><xsl:text> </xsl:text>
                    <xsl:value-of select="Departure/AirpName" /><xsl:text> </xsl:text>(<xsl:value-of select="Departure/AirpCode" />)
                  </span>
                </td>
                <td width="1%">
                </td>
                <td width="6%" align="left" valign="top">
                  <img src="https://crystaltravel.co.uk/images/arrow.png"/>
                </td>
                <td width="1%">
                </td>
                <td width="30%" rowspan="3" align="left" valign="top" class="small-text-color">
                  <div>
                    <xsl:element name="span">
                      <xsl:variable name="month">
                        <xsl:call-template name ="GetMonthName">
                          <xsl:with-param name ="Month" select ="substring(Arrival/Date,4,2)"></xsl:with-param>
                        </xsl:call-template>
                      </xsl:variable>
                      <xsl:value-of select="concat($month,substring(Arrival/Date,1,2), ',', ' ',substring(Arrival/Date,7,4))"/>
                    </xsl:element>
                  </div>
                  <span>
                    <strong>
                      <xsl:value-of select="Arrival/Time"/>
                    </strong>
                  </span>
                  <br/>
                  <span title="Bahrain Intl Arpt, Bahrain (BAH)">
                    <xsl:value-of select="Arrival/CityName"/><xsl:text> </xsl:text><!--<br/>-->
                    <xsl:value-of select="Arrival/AirpName" /><xsl:text> </xsl:text>(<xsl:value-of select="Arrival/AirpCode" />)
                  </span>
                  <br/>
                  <span style="float: left; width: 42px;"></span>
                </td>
                <td width="1%">
                </td>
                <td colspan="2" rowspan="3" valign="top" style="font-family: arial; padding-right: 10px;
                                            font-size: 12px; width: 14%;" align="right">
                  <xsl:value-of select="CabinClass/Des"/>
                  <br/>
                  <strong>
                    <xsl:value-of select="AirV"/>
                    <xsl:text>-</xsl:text>
                    <xsl:value-of select="FltNum"/>

                  </strong>
                </td>

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
                </td>
                <td width="20%" class="avilability-main-top-text-color">
                  <xsl:for-each select="Sectors/Sector[isReturn='true'][position()=1]">
                    <xsl:value-of select="Departure/CityName"/>&#160;
                  </xsl:for-each>
                  <xsl:text> - </xsl:text>
                  <xsl:for-each select="Sectors/Sector[isReturn='true'][last()]">
                    <xsl:value-of select="Arrival/CityName"/>
                  </xsl:for-each>

                </td>
                <td width="80%" align="right">
                  <xsl:for-each select="Sectors/Sector[last()]">
                    Flights Duration:<xsl:value-of select="concat(substring(ActualTime,1,2),'')"/>hrs&#160;<xsl:value-of select="concat(substring(ActualTime,4,2),'')"/>mins
                  </xsl:for-each>
                </td>
                <td width="1%" align="right">
                </td>
              </tr>
            </table>
          </td>
        </tr>
        <tr>
          <td>
            <xsl:for-each select="Sectors/Sector[isReturn = 'true']">
              <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                  <td colspan="12" style="height: 15px; border-top: 1px dotted #424242;">
                  </td>
                </tr>
                <tr>
                  <td width="1%">
                  </td>
                  <td width="4%">
                    <img src="https://crystaltravel.co.uk/images/small-plane-left.png"/>
                  </td>
                  <td width="1%">
                  </td>
                  <td width="6%" class="small-text-color">
                    <xsl:element name="img">
                      <xsl:attribute name="src">
                        <xsl:value-of select="AirlineLogoPath"/>
                      </xsl:attribute>
                      <xsl:attribute name="title">
                        <xsl:value-of select="AirlineName"/>
                      </xsl:attribute>
                      <xsl:attribute name="alt">
                        <xsl:value-of select="AirlineName"/>
                      </xsl:attribute>
                    </xsl:element>
                  </td>
                  <td width="12%" class="small-text-color">
                    <xsl:value-of select="AirlineName"/>
                  </td>
                  <td width="34%" rowspan="3" align="left" valign="top" class="small-text-color">
                    <div>
                      <xsl:element name="span">
                        <xsl:variable name="month">
                          <xsl:call-template name ="GetMonthName">
                            <xsl:with-param name ="Month" select ="substring(Departure/Date,4,2)"></xsl:with-param>
                          </xsl:call-template>
                        </xsl:variable>
                        <xsl:value-of select="concat($month,substring(Departure/Date,1,2), ',', ' ',substring(Departure/Date,7,4))"/>
                      </xsl:element>
                    </div>
                    <span>
                      <strong>
                        <xsl:value-of select="Departure/Time"/>
                      </strong>
                    </span>
                    <br/>
                    <span>
                      <xsl:value-of select="Departure/CityName"/><xsl:text> </xsl:text>
                      <xsl:value-of select="Departure/AirpName" /><xsl:text> </xsl:text>(<xsl:value-of select="Departure/AirpCode" />)
                    </span>
                    <br/>
                    <span style="float: left; width: 42px;"></span>
                  </td>
                  <td width="1%">
                  </td>
                  <td width="6%" align="left" valign="top">
                    <img src="https://crystaltravel.co.uk/images/arrow.png"/>
                  </td>
                  <td width="1%">
                  </td>
                  <td width="30%" rowspan="3" align="left" valign="top" class="small-text-color">
                    <div>
                      <xsl:element name="span">
                        <xsl:variable name="month">
                          <xsl:call-template name ="GetMonthName">
                            <xsl:with-param name ="Month" select ="substring(Arrival/Date,4,2)"></xsl:with-param>
                          </xsl:call-template>
                        </xsl:variable>
                        <xsl:value-of select="concat($month,substring(Arrival/Date,1,2), ',', ' ',substring(Arrival/Date,7,4))"/>
                      </xsl:element>
                    </div>
                    <span>
                      <strong>
                        <xsl:value-of select="Arrival/Time"/>
                      </strong>
                    </span>
                    <br/>
                    <span title="Bahrain Intl Arpt, Bahrain (BAH)">
                      <xsl:value-of select="Arrival/CityName"/><xsl:text> </xsl:text><!--<br/>-->
                      <xsl:value-of select="Arrival/AirpName" /><xsl:text> </xsl:text>(<xsl:value-of select="Arrival/AirpCode" />)
                    </span>
                    <br/>
                    <span style="float: left; width: 42px;"></span>
                  </td>
                  <td width="1%">
                  </td>
                  <td colspan="2" rowspan="3" valign="top" style="font-family: arial; padding-right: 10px;
                                            font-size: 12px; width: 14%;" align="right">
                    <xsl:value-of select="CabinClass/Des"/>
                    <br/>
                    <strong>
                      <xsl:value-of select="AirV"/>
                      <xsl:text>-</xsl:text>
                      <xsl:value-of select="FltNum"/>
                      <!--GF-131-->
                    </strong>
                  </td>
                </tr>
                <tr>
                  <td colspan="12" style="padding: 6px;">
                  </td>
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
</xsl:stylesheet>
