<xsl:stylesheet version="1.0"
 xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
 xmlns:ext="http://exslt.org/common"
 xmlns:msxsl="urn:schemas-microsoft-com:xslt"
 >
  <xsl:output omit-xml-declaration="yes" indent="yes"/>
  <xsl:strip-space elements="*"/>
  <xsl:key name="Origin" match="//Sectors/Sector[1]/Departure/AirpCode/text()" use="." />
  <xsl:key name="DestinationRT" match="//Sectors/Sector[isReturn='true'][1]/Departure/AirpCode/text()" use="." />
  <xsl:key name="DestinationOW" match="//Sectors/Sector[last()]/Arrival/AirpCode/text()" use="." />
  <xsl:key name="Class" match="//Sectors/Sector[1]/Class/text()" use="." />
  <xsl:key name="Providers" match="//FareType/text()" use="." />
  <xsl:key name="Airlines" match="//Sectors/Sector[1]/AirV/text()" use="." />
  <xsl:param name ="JType" select ="'RT'"></xsl:param>
  <xsl:param name ="Applicable" select ="'3517_CT'"></xsl:param>
  <xsl:param name ="Category" select ="'Y'"></xsl:param>  

  <xsl:template match="node()|@*" name="identity">
    <MarkupData>
      <Origin>
        <Airport>ANY</Airport>
        <xsl:for-each select="//Sectors/Sector[1]/Departure/AirpCode/text()[generate-id()=generate-id(key('Origin',.)[1])]">
          <Airport>
            <xsl:value-of select="."/>
          </Airport>
        </xsl:for-each>
      </Origin>
      <Destination>
        <Airport>ANY</Airport>
        <xsl:choose>
          <xsl:when test ="$JType='RT'">           
            <xsl:for-each select="//Sectors/Sector[isReturn='true'][1]/Departure/AirpCode/text()[generate-id()=generate-id(key('DestinationRT',.)[1])]">
              <Airport>
                <xsl:value-of select="."/>
              </Airport>
            </xsl:for-each>
          </xsl:when>
          <xsl:when test ="$JType='OW'">
            <xsl:for-each select="//Sectors/Sector[last()]/Arrival/AirpCode/text()[generate-id()=generate-id(key('DestinationOW',.)[1])]">
              <Airport>
                <xsl:value-of select="."/>
              </Airport>
            </xsl:for-each>
          </xsl:when>
        </xsl:choose>        
      </Destination>
      <Airlines>
        <Airline>ANY</Airline>
        <xsl:for-each select="//Sectors/Sector[1]/AirV/text()[generate-id()=generate-id(key('Airlines',.)[1])]">
          <Airline>
            <xsl:value-of select="."/>
          </Airline>
        </xsl:for-each>
      </Airlines>
      <Providers>
        <Code>ANY</Code>
        <xsl:for-each select="//FareType/text()[generate-id()=generate-id(key('Providers',.)[1])]">
          <Code>
            <xsl:value-of select="."/>
          </Code>
        </xsl:for-each>
      </Providers>
      <Category>
        <Code>ANY</Code>
        <Code>
          <xsl:choose>
            <xsl:when test ="$Category='Y'">
              <xsl:value-of select ="'ECONOMY'"/>
            </xsl:when>
            <xsl:when test ="$Category='C'">
              <xsl:value-of select ="'BUSINESS'"/>
            </xsl:when>
            <xsl:when test ="$Category='F'">
              <xsl:value-of select ="'FIRSTCLASS'"/>
            </xsl:when>
            <xsl:when test ="$Category='W'">
              <xsl:value-of select ="'PREMIUM'"/>
            </xsl:when>
          </xsl:choose>
        </Code>
      </Category>
      <Class>
        <Code>ANY</Code>
        <xsl:for-each select="//Sectors/Sector[1]/Class/text()[generate-id()=generate-id(key('Class',.)[1])]">
        
          <Code>
            <xsl:value-of select="."/>
          </Code>
        </xsl:for-each>
      </Class>
      <Applicablefor>
        <Code>ANY</Code>
        <Code>
          <xsl:value-of select ="$Applicable"/>
        </Code>
      </Applicablefor>
      <Jtype>
        <Code>ANY</Code>
        <Code>
          <xsl:value-of select ="$JType"/>
        </Code>
      </Jtype>            
    </MarkupData>
  </xsl:template>    
</xsl:stylesheet>
