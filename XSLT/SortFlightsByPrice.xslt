<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
  <xsl:output method="xml" indent="yes"/>
  <xsl:param name="CompanyID"></xsl:param>
  <xsl:template match="Itineraries">
    <Itineraries>
      <xsl:choose>
      <xsl:when test="$CompanyID='3517_CT'">
        <xsl:for-each select ="Itinerary">
			<xsl:sort select="GrandTotal" order="ascending" data-type ="number" />
          <xsl:sort select="Provider" order="descending" data-type ="text" />
          <xsl:copy-of select ="."/>
        </xsl:for-each>
      </xsl:when>
        <xsl:otherwise>
          <xsl:for-each select ="Itinerary">
            <xsl:sort select="GrandTotal" order="ascending" data-type ="number" />
            <xsl:copy-of select ="."/>
          </xsl:for-each>  
        </xsl:otherwise> 
  </xsl:choose>
      <xsl:copy-of select ="IsCache"/>
    </Itineraries>
  </xsl:template>
</xsl:stylesheet>
