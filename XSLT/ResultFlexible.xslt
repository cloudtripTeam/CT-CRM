<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl" xmlns:func="urn:actl-xslt">
  <xsl:output method="html" indent="yes"/>
  <xsl:key name="GrandTotal" match="//GrandTotal/text()" use="." />
  <xsl:key name="Airlines" match="//ValCarrier/text()" use="." />
  <xsl:key name="keyEmpByName" match="Itineraries/Itinerary/Sectors/Sector/AirlineName/text()" use="."/>
  <xsl:param name="sourceMedia" select="'sourceMedia'"/>
  <xsl:param name="Ddate" select="'Ddate'"/>
  <xsl:param name="Rdate" select="'Rdate'"/>

  <xsl:template match="Itineraries">
    <xsl:variable name="FareTypes" select="Availability"></xsl:variable>

    <div class="r-wraps">
      <xsl:for-each select="Itinerary">
        <xsl:variable name='Provider' select='Provider'></xsl:variable>
        <xsl:variable name='Indx' select='IndexNumber'></xsl:variable>
        <xsl:variable name="AirCode" select="Sectors/Sector[isReturn='false'][1]/AirV"></xsl:variable>
        <xsl:variable name="AirClass" select="Sectors/Sector[isReturn='false'][1]/CabinClass/Des"></xsl:variable>
        <xsl:if test="position()!=1">
          <div class="r-wrapin">
            <div class="r-top">
              <xsl:element name="img">
                <xsl:attribute name="src">
                  <xsl:value-of select="Sectors/Sector[isReturn='false'][1]/AirlineLogoPath"/>
                </xsl:attribute>
                <xsl:attribute name="title">
                  <xsl:value-of select="Sectors/Sector[isReturn='false'][1]/AirlineName"/>
                </xsl:attribute>
              </xsl:element>
            </div>
            <div class="r-topout">
              <div class="r-topdes">
                <xsl:value-of select="Sectors/Sector[isReturn='false'][1]/Departure/CityName"/>
                <xsl:text>(</xsl:text>
                <xsl:value-of select="Sectors/Sector[isReturn='false'][1]/Departure/AirpCode"/>
                <xsl:text>)</xsl:text>
                <!--London (LON)-->
                <br />
                <span style=" height:15px;line-height:25px;font-family: arial;font-size: 12px;">
                  <xsl:value-of select="Sectors/Sector[isReturn='false'][1]/CabinClass/Des"></xsl:value-of>
                </span>
              </div>
              <div class="r-topfr">
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
                <xsl:if test="number($noOfPax)= 1">
                  <xsl:text>£</xsl:text>
                  <xsl:value-of select="GrandTotal"/>
                  <!--<br />
                      <span>
                          <xsl:value-of select="Sectors/Sector[isReturn='false'][1]/CabinClass/Des"></xsl:value-of>
                      </span>-->
                </xsl:if>
                <xsl:if test="number($noOfPax)> 1">
                  <xsl:text>£</xsl:text>
                  <xsl:value-of select ="format-number(number(GrandTotal) div number($noOfPax),'#.00')"></xsl:value-of>
                  <span style=" height:15px;  font-family: arial;font-size: 12px;">Avg per person</span>
                  <!--<br />
                      <span style=" height:15px;">
                          <xsl:value-of select="Sectors/Sector[isReturn='false'][1]/CabinClass/Des"></xsl:value-of>
                      </span>-->

                  <div style=" width:100%">
                    <span style="font-weight:bold;font-size: 13px;">
                      total £<xsl:value-of select="GrandTotal"/>
                    </span>
                  </div>

                </xsl:if>

              </div>
            </div>
            <table class="r-table">
              <tr>
                <td class="r-blues">
                  Destination:
                </td>
                <td>
                  <xsl:value-of select="Sectors/Sector[isReturn='false'][last()]/Arrival/CityName"/>, <xsl:value-of select="Sectors/Sector[isReturn='false'][last()]/Arrival/CountryName"/>
                </td>
              </tr>
              <tr>
                <td class="r-blues">
                  Travel Date:
                </td>
                <td>
                  <!--<xsl:element name="span">
                    <xsl:variable name="month">
                      <xsl:call-template name ="GetMonthName">
                        <xsl:with-param name ="Month" select ="substring(Sectors/Sector[isReturn='false'][1]/Departure/Date,4,2)"></xsl:with-param>
                      </xsl:call-template>
                    </xsl:variable>
                    <xsl:value-of select="concat(substring(Sectors/Sector[isReturn='false'][1]/Departure/Date,1,2),' ',$month,' ',substring(Sectors/Sector[isReturn='false'][1]/Departure/Date,7,4))"/>
                  </xsl:element>-->
                  <xsl:value-of select="$Ddate"/>
                  <xsl:if test="$Rdate!='none'">
                    <xsl:text> - </xsl:text>
                    <xsl:value-of select="$Rdate"/>
                  </xsl:if>

                  <!--<xsl:element name="span">
                    <xsl:variable name="month">
                      <xsl:call-template name ="GetMonthName">
                        <xsl:with-param name ="Month" select ="substring(Sectors/Sector[isReturn='true'][1]/Departure/Date,4,2)"></xsl:with-param>
                      </xsl:call-template>
                    </xsl:variable>
                    <xsl:value-of select="concat(substring(Sectors/Sector[isReturn='true'][1]/Departure/Date,1,2),' ',$month,' ',substring(Sectors/Sector[isReturn='true'][1]/Departure/Date,7,4))"/>
                  </xsl:element>-->
                  <!--27 Dec 2014                         
                        -26 Apr 2015-->
                </td>
              </tr>
            </table>
            <p>
              Talk to a travel expert now to book:<br />
              <b>Call FREE</b>
            </p>
            <div class="r-topdes2">
              <img src="images/r-calls.png" />
            </div>
            <div class="r-topfr2">
              <xsl:choose>
                <xsl:when test="$sourceMedia = 'DC_JT'">
                  <span class="number">0203-023-7791</span>
                </xsl:when>
                <xsl:when test="$sourceMedia = 'CF_JT'">
                  <span class="number">0208-477-7144</span>
                </xsl:when>
                <xsl:when test="$sourceMedia = '3517_JT_NWS'">
                  <span class="number">0203-733-3619</span>
                </xsl:when>
                <xsl:otherwise>
                  <span class="number">0203-023-7791</span>
                </xsl:otherwise>
              </xsl:choose>
            </div>
            <div class="r-wrapssss">
              <input type="button">
                <xsl:attribute name="value">
                  <xsl:value-of select ="'ENQUIRE NOW'"/>
                </xsl:attribute>
                <xsl:attribute name="class">
                  <xsl:value-of select ="'book_btn1'"/>
                </xsl:attribute>
                <xsl:attribute name ="onclick">
                  <xsl:value-of select ="func:GetQueryString1($Indx,$Provider,$AirCode,$AirClass)"/>
                </xsl:attribute>
              </input>
            </div>
          </div>
        </xsl:if>
      </xsl:for-each>
    </div>

    <!--End Sorting area  -->
  </xsl:template>
  <xsl:template name ="GetMonthName">
    <xsl:param name ="Month"></xsl:param>
    <xsl:choose>
      <xsl:when test ="$Month='01'">Jan</xsl:when>
      <xsl:when test ="$Month='02'">Feb</xsl:when>
      <xsl:when test ="$Month='03'">Mar</xsl:when>
      <xsl:when test ="$Month='04'">Apr</xsl:when>
      <xsl:when test ="$Month='05'">May</xsl:when>
      <xsl:when test ="$Month='06'">Jun</xsl:when>
      <xsl:when test ="$Month='07'">Jul</xsl:when>
      <xsl:when test ="$Month='08'">Aug</xsl:when>
      <xsl:when test ="$Month='09'">Sep</xsl:when>
      <xsl:when test ="$Month='10'">Oct</xsl:when>
      <xsl:when test ="$Month='11'">Nov</xsl:when>
      <xsl:when test ="$Month='12'">Dec</xsl:when>
    </xsl:choose>
  </xsl:template>
</xsl:stylesheet>
