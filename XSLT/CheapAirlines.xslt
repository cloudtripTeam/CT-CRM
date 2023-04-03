<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl" xmlns:func="urn:actl-xslt">
  <xsl:output method="html" indent="yes"/>
  <xsl:key name="GrandTotal" match="//GrandTotal/text()" use="." />
  <xsl:key name="Airlines" match="//ValCarrier/text()" use="." />
  <xsl:key name="keyEmpByName" match="Itineraries/Itinerary/Sectors/Sector/AirlineName/text()" use="."/>

  <xsl:template match="/">
    <xsl:variable name="FareTypes" select="Itineraries/Availability"></xsl:variable>
    <div style="width:800px; float:left; height:130px; overflow:auto;">
      <xsl:for-each select="Itineraries/Itinerary">
        <xsl:if test ="Sectors/Sector/AirlineName/text()[generate-id()=generate-id(key('keyEmpByName',.)[1])] or $FareTypes='AAF'">
          <xsl:variable name="SelectedProvides" select="Provider"></xsl:variable>
          <xsl:variable name="SelectedIndexNo" select="IndexNumber"></xsl:variable>
          <xsl:if test ="GrandTotal/text()[generate-id()=generate-id(key('GrandTotal',.)[1])] or ValCarrier/text()[generate-id()=generate-id(key('Airlines',.)[1])]">
            <div style="width:110px; float:left; height:130px;">
              <table cellspacing="0" cellpadding="0" border="0" width="100px">
                <tr>
                  <td>
                    <a onclick="">
                      <xsl:element name="img">
                        <xsl:attribute name="src">
                          <xsl:value-of select="Sectors/Sector[isReturn='false'][1]/AirlineLogoPath"/>
                        </xsl:attribute>
                        <xsl:attribute name="alt">
                          <xsl:value-of select="Sectors/Sector[isReturn='false'][1]/AirlineName"/>
                        </xsl:attribute>
                        <xsl:attribute name="title">
                          <xsl:value-of select="Sectors/Sector[isReturn='false'][1]/AirlineName"/>
                        </xsl:attribute>
                      </xsl:element>
                    </a>
                  </td>
                </tr>
                <tr>
                  <td style="height:35px; vertical-align:top;">
                    <span style="width:90px; height:35px; color:#000;word-wrap:break-word;">
                      <xsl:value-of select="Sectors/Sector[isReturn='false'][1]/AirlineName"/>
                    </span>
                  </td>
                </tr>
                <tr>
                  <td style="height:20px;">
                    <xsl:variable name ="Sec" select="count(Sectors/Sector[isReturn='false'])"/>
                    <xsl:choose>
                      <xsl:when test="$Sec=1">
                        <xsl:text> Direct Flight</xsl:text>
                      </xsl:when>
                      <xsl:when test="$Sec=2">
                        <xsl:text> 1 Stop </xsl:text>
                      </xsl:when>
                      <xsl:when test="$Sec=3">
                        <xsl:text> 2 Stop </xsl:text>
                      </xsl:when>
                      <xsl:otherwise>
                      </xsl:otherwise>
                    </xsl:choose>
                  </td>
                </tr>
                <tr>
                  <td>
                    £ <xsl:value-of select="GrandTotal"/>
                  </td>
                </tr>
              </table>
            </div>
          </xsl:if>
        </xsl:if>
      </xsl:for-each>
    </div>
  </xsl:template>
</xsl:stylesheet>
