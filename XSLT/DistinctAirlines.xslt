<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" 
xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:msxsl="urn:schemas-microsoft-com:xslt" 
exclude-result-prefixes="msxsl"
xmlns:func="urn:actl-xslt">
  
<xsl:output method="xml" indent="yes"/>

  <xsl:key name="GrandTotal" match="//GrandTotal/text()" use="." />
  <xsl:key name="Airlines" match="//ValCarrier/text()" use="." />
  
  <xsl:template match="/">
    <Itineraries>
      <xsl:for-each select="Itineraries/Itinerary">
        <xsl:if test="GrandTotal/text()[generate-id()=generate-id(key('GrandTotal',.)[1])] and ValCarrier/text()[generate-id()=generate-id(key('Airlines',.)[1])]">
          <xsl:copy-of select="."/>
        </xsl:if>
      </xsl:for-each>
    </Itineraries>
  </xsl:template>
</xsl:stylesheet>
