<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <xsl:output method="html" indent="yes"/>
  <xsl:template match="/">
    <xsl:for-each select="Itinerary">
      <xsl:variable name ="DivFlightProvider" select ="Provider"></xsl:variable>
      <xsl:variable name ="DivFlightIndex" select ="IndexNumber"></xsl:variable>
      <xsl:variable name="InboundNo" select="count(Sectors/Sector[isReturn='false'])"/>
      <xsl:variable name="OutboundNo" select="count(Sectors/Sector[isReturn='true'])"/>
      <xsl:variable name ="DivFlightID" select ="concat($DivFlightProvider,$DivFlightIndex)"></xsl:variable>

      <div class="panel panel-default">
        <div class="panel-heading">
          <big>
            <xsl:for-each select="Sectors/Sector[position()=1]">
              <xsl:value-of select="Departure/CityName"/>&#160;
            </xsl:for-each>-
            <xsl:for-each select="Sectors/Sector[position()=$InboundNo]">
              <xsl:value-of select="Arrival/CityName"/>
            </xsl:for-each>
          </big>
          <span style="float: right; padding-right:20px; " class="bluetext text-primary  text-right  ">
            <xsl:for-each select="Sectors/Sector[position()=$InboundNo]">
              Flights Duration:<xsl:value-of select="concat(substring(ActualTime,1,2),' ')"/>hrs&#160;<xsl:value-of select="concat(substring(ActualTime,4,2),' ')"/>mins
            </xsl:for-each>
          </span>
        </div>
        <div class="panel-body">
          <table width="100%" border="0" cellspacing="0" cellpadding="0"   >

            <tr>
              <td height="5px" colspan="14"></td>
            </tr>
            <tr>
              <td width="1%"></td>
              <td width="99%">
                <table width="100%" cellsapcing="0" cellpading="0" border="0" style="font-size:13px;">
                  <xsl:for-each select="Sectors/Sector[isReturn='false']">
                    <xsl:if test="TransitTime/@time!='00:00'">
                      <tr>
                        <td colspan="11" align="center" valign="top" class="bg-warning text-center text-danger" style="background-color:#eee; padding-bottom:5px; padding-top:5px;">
                          <xsl:value-of select="TransitTime"/>
                        </td>
                      </tr>
                    </xsl:if>
                    <tr >
                      <td width="3%" align="left"  >
                        <i class="fa fa-plane fa-2x"></i>
                      </td>
                      <td width="6%" class="small-text-color" align="left">
                        <xsl:element name="img">
                          <xsl:attribute name="src">
                            <xsl:value-of select="AirlineLogoPath"/>
                          </xsl:attribute>
                          <xsl:attribute name="alt">
                            <xsl:value-of select="AirlineName"/>
                          </xsl:attribute>
                          <xsl:attribute name="title">
                            <xsl:value-of select="AirlineName"/>
                          </xsl:attribute>
                          <xsl:attribute name="style">
                            <xsl:text>float:left;</xsl:text>
                          </xsl:attribute>
                        </xsl:element>
                      </td>
                      <td width="1%"></td>
                      <td width="34%" align="left" valign="top" style="padding-top:10px;">
                        <strong>
                          <xsl:value-of select="Departure/AirpName"/>,&#160;<xsl:value-of select="Departure/CityName"/> <xsl:text>(</xsl:text>
                          <xsl:value-of select="Departure/AirpCode"/>
                          <xsl:text>)</xsl:text>
                        </strong>
                        <br />
                        <b>Departure</b>&#160;&#160; <xsl:value-of select="Departure/Time"/>&#160;
                        <xsl:value-of select="Departure/Day"></xsl:value-of>,&#160;<xsl:value-of select="concat(substring(Departure/Date, 1,2),' ')"></xsl:value-of>
                        <xsl:variable name="RoomDayDate" select="substring(Departure/Date,4,2)"></xsl:variable>
                        <xsl:choose>
                          <xsl:when test ="$RoomDayDate=01">
                            <xsl:value-of select ="'Jan'"/>
                          </xsl:when>
                          <xsl:when test ="$RoomDayDate=02">
                            <xsl:value-of select ="'Feb'"/>
                          </xsl:when>
                          <xsl:when test ="$RoomDayDate=03">
                            <xsl:value-of select ="'Mar'"/>
                          </xsl:when>
                          <xsl:when test ="$RoomDayDate=04">
                            <xsl:value-of select ="'Apr'"/>
                          </xsl:when>
                          <xsl:when test ="$RoomDayDate=05">
                            <xsl:value-of select ="'May'"/>
                          </xsl:when>
                          <xsl:when test ="$RoomDayDate=06">
                            <xsl:value-of select ="'Jun'"/>
                          </xsl:when>
                          <xsl:when test ="$RoomDayDate=07">
                            <xsl:value-of select ="'Jul'"/>
                          </xsl:when>
                          <xsl:when test ="$RoomDayDate=08">
                            <xsl:value-of select ="'Aug'"/>
                          </xsl:when>
                          <xsl:when test ="$RoomDayDate=09">
                            <xsl:value-of select ="'Sep'"/>
                          </xsl:when>
                          <xsl:when test ="$RoomDayDate=10">
                            <xsl:value-of select ="'Oct'"/>
                          </xsl:when>
                          <xsl:when test ="$RoomDayDate=11">
                            <xsl:value-of select ="'Nov'"/>
                          </xsl:when>
                          <xsl:when test ="$RoomDayDate=12">
                            <xsl:value-of select ="'Dec'"/>
                          </xsl:when>
                        </xsl:choose>&#160;
                        <xsl:value-of select="concat(substring(Departure/Date, 7,4),' ')"></xsl:value-of>
                        <br />
                      </td>
                      <td width="1%"></td>
                      <td width="3%" align="left" valign="middle">
                        <img src="images/re-arrow.png" ></img>
                      </td>
                      <td width="1%"></td>
                      <td width="33%" align="left" valign="top" class="small-text-color" style="padding-top:10px;">
                        <strong>
                          <xsl:value-of select="Arrival/AirpName"/>,&#160;<xsl:value-of select="Arrival/CityName"/>
                        </strong><br />
                        <xsl:if test="$InboundNo=1">
                          <b>
                            <xsl:text>Arrival</xsl:text>
                          </b>
                        </xsl:if>
                        <xsl:if test="$InboundNo=2">
                          <xsl:choose>
                            <xsl:when test="position()=1">
                              <b>
                                <xsl:text>1Stop</xsl:text>
                              </b>
                            </xsl:when>
                            <xsl:when test="position()=2">
                              <b>
                                <xsl:text>Arrival</xsl:text>
                              </b>
                            </xsl:when>
                            <xsl:otherwise></xsl:otherwise>
                          </xsl:choose>
                        </xsl:if>
                        <xsl:if test="$InboundNo=3">
                          <xsl:choose>
                            <xsl:when test="position()=1">
                              <b>
                                <xsl:text>1Stop</xsl:text>
                              </b>
                            </xsl:when>
                            <xsl:when test="position()=2">
                              <b>
                                <xsl:text>2Stop</xsl:text>
                              </b>
                            </xsl:when>
                            <xsl:when test="position()=3">
                              <b>
                                <xsl:text>Arrival</xsl:text>
                              </b>
                            </xsl:when>
                            <xsl:otherwise></xsl:otherwise>
                          </xsl:choose>
                        </xsl:if>
                        &#160;&#160;
                        <xsl:value-of select="Arrival/Time"/>&#160;
                        <xsl:value-of select="Arrival/Day"></xsl:value-of>,&#160;<xsl:value-of select="concat(substring(Arrival/Date, 1,2),' ')"></xsl:value-of>
                        <xsl:variable name="RoomDayDate" select="substring(Arrival/Date,4,2)"></xsl:variable>
                        <xsl:choose>
                          <xsl:when test ="$RoomDayDate=01">
                            <xsl:value-of select ="'Jan'"/>
                          </xsl:when>
                          <xsl:when test ="$RoomDayDate=02">
                            <xsl:value-of select ="'Feb'"/>
                          </xsl:when>
                          <xsl:when test ="$RoomDayDate=03">
                            <xsl:value-of select ="'Mar'"/>
                          </xsl:when>
                          <xsl:when test ="$RoomDayDate=04">
                            <xsl:value-of select ="'Apr'"/>
                          </xsl:when>
                          <xsl:when test ="$RoomDayDate=05">
                            <xsl:value-of select ="'May'"/>
                          </xsl:when>
                          <xsl:when test ="$RoomDayDate=06">
                            <xsl:value-of select ="'Jun'"/>
                          </xsl:when>
                          <xsl:when test ="$RoomDayDate=07">
                            <xsl:value-of select ="'Jul'"/>
                          </xsl:when>
                          <xsl:when test ="$RoomDayDate=08">
                            <xsl:value-of select ="'Aug'"/>
                          </xsl:when>
                          <xsl:when test ="$RoomDayDate=09">
                            <xsl:value-of select ="'Sep'"/>
                          </xsl:when>
                          <xsl:when test ="$RoomDayDate=10">
                            <xsl:value-of select ="'Oct'"/>
                          </xsl:when>
                          <xsl:when test ="$RoomDayDate=11">
                            <xsl:value-of select ="'Nov'"/>
                          </xsl:when>
                          <xsl:when test ="$RoomDayDate=12">
                            <xsl:value-of select ="'Dec'"/>
                          </xsl:when>
                        </xsl:choose>&#160;
                        <xsl:value-of select="concat(substring(Arrival/Date, 7,4),' ')"></xsl:value-of>
                        <br />
                        <br />
                      </td>
                      <td width="1%"></td>
                      <td width="" align="right" valign="top" class="small-text-color">
                        <xsl:value-of select="CabinClass/Des"/>
                        <br />
                        <span style=" float:right;margin: 8px 5px; 0;">
                          <xsl:value-of select="AirV"/>
                          <xsl:text>-</xsl:text>
                          <xsl:value-of select="FltNum"/>
                        </span>
                      </td>
                      <td width="1%"></td>
                    </tr>
                  </xsl:for-each>
                </table>
              </td>
            </tr>
            <tr>
              <td colspan="10" height="5px"></td>
            </tr>
            <xsl:if test="count(Sectors/Sector[isReturn='true'])>0">


              <tr>
                <td colspan="14" class="avilability-main-top-text-color2">
                  <table width="100%" border="0" cellspacing="0" cellpadding="0" style="padding-bottom:10px; height:30px;" >
                    <tr class="big-text-black">
                      <td width="0%"></td>
                      <td style="width:55%; color: #333;background-color: #f5f5f5;border-color: #ddd;padding-left:10px;padding-right:10px;">
                        <xsl:for-each select="Sectors/Sector[isReturn='true'][position()=1]">
                          <xsl:value-of select="Departure/CityName"/>&#160;
                          <!--<xsl:value-of select="Departure/AirpName"/>-->
                        </xsl:for-each>-
                        <xsl:for-each select="Sectors/Sector[isReturn='true'][last()]">
                          <xsl:value-of select="Arrival/CityName"/>
                        </xsl:for-each>
                        <!--London heathrow - Manila-->
                      </td>
                      <td style="color: #333;
  background-color: #f5f5f5;
  border-color: #ddd;" width="44%" align="right" >
                        <xsl:for-each select="Sectors/Sector[last()]">
                          Flights Duration:<xsl:value-of select="concat(substring(ActualTime,1,2),' ')"/>hrs&#160;<xsl:value-of select="concat(substring(ActualTime,4,2),' ')"/>mins
                        </xsl:for-each>
                        <!--12hrs 34mins-->
                      </td>
                      <td width="1%" align="right"></td>
                    </tr>
                  </table>
                </td>
              </tr>
            </xsl:if>
            <tr>
              <td height="5px" colspan="14" align="left" valign="top"></td>
            </tr>

            <xsl:if test="count(Sectors/Sector[isReturn='true'])>0">
              <tr>
                <td width="1%"></td>

                <td width="99%">
                  <table cellspacing="0" cellpading="0" border="0" width="100%" style="font-size:13px;">
                    <xsl:for-each select="Sectors/Sector[isReturn='true']">

                      <xsl:if test="TransitTime/@time!='00:00'">
                        <tr>
                          <td colspan="11" align="center" valign="top" style="background-color:#eee; padding-bottom:5px; padding-top:5px;" class="bg-warning text-center text-danger">
                            <xsl:value-of select="TransitTime"/>
                          </td>
                        </tr>
                      </xsl:if>

                      <tr>
                        <td width="3%" align="left">
                          <i class="fa fa-plane fa-2x fa-flip-vertical"></i>
                        </td>
                        <td width="6%" align="left">
                          <xsl:element name="img">
                            <xsl:attribute name="src">
                              <xsl:value-of select="AirlineLogoPath"/>
                            </xsl:attribute>
                            <xsl:attribute name="alt">
                              <xsl:value-of select="AirlineName"/>
                            </xsl:attribute>
                            <xsl:attribute name="title">
                              <xsl:value-of select="AirlineName"/>
                            </xsl:attribute>
                            <xsl:attribute name="style">
                              <xsl:text>float:left;</xsl:text>
                            </xsl:attribute>
                          </xsl:element>
                        </td>
                        <td width="1%"></td>
                        <td width="34%"  align="left" valign="top" class="small-text-color">
                          <strong>
                            <xsl:value-of select="Departure/AirpName"/>,&#160;<xsl:value-of select="Departure/CityName"/> <xsl:text>(</xsl:text>
                            <xsl:value-of select="Departure/AirpCode"/>
                            <xsl:text>)</xsl:text>
                          </strong><br />
                          <xsl:choose>
                            <xsl:when test ="position()=1">
                              <b>
                                <xsl:text>Return</xsl:text>
                              </b>
                            </xsl:when>

                            <xsl:otherwise>
                              <b>
                                <xsl:text>Departure</xsl:text>
                              </b>
                            </xsl:otherwise>
                          </xsl:choose>
                          &#160;&#160;
                          <xsl:value-of select="Departure/Time"/>&#160;
                          <xsl:value-of select="Departure/Day"></xsl:value-of>,&#160;<xsl:value-of select="concat(substring(Departure/Date, 1,2),' ')"></xsl:value-of>
                          <xsl:variable name="RoomDayDate" select="substring(Departure/Date,4,2)"></xsl:variable> <xsl:choose>
                            <xsl:when test ="$RoomDayDate=01">
                              <xsl:value-of select ="'Jan'"/>
                            </xsl:when>
                            <xsl:when test ="$RoomDayDate=02">
                              <xsl:value-of select ="'Feb'"/>
                            </xsl:when>
                            <xsl:when test ="$RoomDayDate=03">
                              <xsl:value-of select ="'Mar'"/>
                            </xsl:when>
                            <xsl:when test ="$RoomDayDate=04">
                              <xsl:value-of select ="'Apr'"/>
                            </xsl:when>
                            <xsl:when test ="$RoomDayDate=05">
                              <xsl:value-of select ="'May'"/>
                            </xsl:when>
                            <xsl:when test ="$RoomDayDate=06">
                              <xsl:value-of select ="'Jun'"/>
                            </xsl:when>
                            <xsl:when test ="$RoomDayDate=07">
                              <xsl:value-of select ="'Jul'"/>
                            </xsl:when>
                            <xsl:when test ="$RoomDayDate=08">
                              <xsl:value-of select ="'Aug'"/>
                            </xsl:when>
                            <xsl:when test ="$RoomDayDate=09">
                              <xsl:value-of select ="'Sep'"/>
                            </xsl:when>
                            <xsl:when test ="$RoomDayDate=10">
                              <xsl:value-of select ="'Oct'"/>
                            </xsl:when>
                            <xsl:when test ="$RoomDayDate=11">
                              <xsl:value-of select ="'Nov'"/>
                            </xsl:when>
                            <xsl:when test ="$RoomDayDate=12">
                              <xsl:value-of select ="'Dec'"/>
                            </xsl:when>

                          </xsl:choose>&#160;
                          <xsl:value-of select="concat(substring(Departure/Date, 7,4),' ')"></xsl:value-of>
                          <br />
                        </td>
                        <td width="1%"></td>
                        <td width="3%" align="left" valign="middle">
                          <img src="images/re-arrow.png"/>
                        </td>
                        <td width="1%"></td>
                        <td width="33%"  align="left" valign="top" class="small-text-color">
                          <strong>
                            <xsl:value-of select="Arrival/AirpName"/>,&#160;<xsl:value-of select="Arrival/CityName"/>
                          </strong>
                          <br />
                          <xsl:if test="$OutboundNo=1">
                            <b>
                              <xsl:text>Arrival</xsl:text>
                            </b>
                          </xsl:if>
                          <xsl:if test="$OutboundNo=2">
                            <xsl:choose>
                              <xsl:when test="position()=1">
                                <b>
                                  <xsl:text>1Stop</xsl:text>
                                </b>
                              </xsl:when>
                              <xsl:when test="position()=2">
                                <b>
                                  <xsl:text>Arrival</xsl:text>
                                </b>
                              </xsl:when>
                              <xsl:otherwise></xsl:otherwise>
                            </xsl:choose>
                          </xsl:if>
                          <xsl:if test="$OutboundNo=3">
                            <xsl:choose>
                              <xsl:when test="position()=1">
                                <b>
                                  <xsl:text>1Stop</xsl:text>
                                </b>
                              </xsl:when>
                              <xsl:when test="position()=2">
                                <b>
                                  <xsl:text>2Stop</xsl:text>
                                </b>
                              </xsl:when>
                              <xsl:when test="position()=3">
                                <b>
                                  <xsl:text>Arrival</xsl:text>
                                </b>
                              </xsl:when>
                              <xsl:otherwise></xsl:otherwise>
                            </xsl:choose>
                          </xsl:if>
                          &#160;&#160;
                          <xsl:value-of select="Arrival/Time"/>&#160;
                          <xsl:value-of select="Arrival/Day"></xsl:value-of>,&#160;<xsl:value-of select="concat(substring(Arrival/Date, 1,2),' ')"></xsl:value-of>
                          <xsl:variable name="RoomDayDate" select="substring(Arrival/Date,4,2)"></xsl:variable>
                          <xsl:choose>
                            <xsl:when test ="$RoomDayDate=01">
                              <xsl:value-of select ="'Jan'"/>
                            </xsl:when>
                            <xsl:when test ="$RoomDayDate=02">
                              <xsl:value-of select ="'Feb'"/>
                            </xsl:when>
                            <xsl:when test ="$RoomDayDate=03">
                              <xsl:value-of select ="'Mar'"/>
                            </xsl:when>
                            <xsl:when test ="$RoomDayDate=04">
                              <xsl:value-of select ="'Apr'"/>
                            </xsl:when>
                            <xsl:when test ="$RoomDayDate=05">
                              <xsl:value-of select ="'May'"/>
                            </xsl:when>
                            <xsl:when test ="$RoomDayDate=06">
                              <xsl:value-of select ="'Jun'"/>
                            </xsl:when>
                            <xsl:when test ="$RoomDayDate=07">
                              <xsl:value-of select ="'Jul'"/>
                            </xsl:when>
                            <xsl:when test ="$RoomDayDate=08">
                              <xsl:value-of select ="'Aug'"/>
                            </xsl:when>
                            <xsl:when test ="$RoomDayDate=09">
                              <xsl:value-of select ="'Sep'"/>
                            </xsl:when>
                            <xsl:when test ="$RoomDayDate=10">
                              <xsl:value-of select ="'Oct'"/>
                            </xsl:when>
                            <xsl:when test ="$RoomDayDate=11">
                              <xsl:value-of select ="'Nov'"/>
                            </xsl:when>
                            <xsl:when test ="$RoomDayDate=12">
                              <xsl:value-of select ="'Dec'"/>
                            </xsl:when>

                          </xsl:choose>&#160;
                          <xsl:value-of select="concat(substring(Arrival/Date, 7,4),' ')"></xsl:value-of>
                          <br />
                        </td>
                        <td width="1%"></td>
                        <td  align="right" valign="top" class="small-text-color">
                          <xsl:value-of select="CabinClass/Des"/>
                          <br />
                          <span style=" float:right;margin: 8px 0 0;">
                            <xsl:value-of select="AirV"/>
                            <xsl:text>-</xsl:text>
                            <xsl:value-of select="FltNum"/>
                          </span>
                        </td>
                        <td width="1%"></td>
                      </tr>
                    </xsl:for-each>
                  </table>
                </td>
              </tr>
              <tr>
                <td height="5px" colspan="10"></td>
              </tr>
            </xsl:if>
          </table>

        </div>
      </div>

    </xsl:for-each>
  </xsl:template>
</xsl:stylesheet>
