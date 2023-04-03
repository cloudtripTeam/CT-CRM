<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
  <xsl:output method="xml" indent="yes"/>
  <xsl:param name ="Departure" select ="''"></xsl:param>
  <xsl:param name ="CabinClass" select ="''"></xsl:param>
  <xsl:template match="Itineraries">
    <Itineraries>
      <xsl:for-each select ="Itinerary">
        <xsl:sort select="TotalPrice" order="ascending" data-type ="number" />
        <Itinerary>
          <xsl:copy-of select ="BaseFare"/>
          <xsl:copy-of select ="Taxes"/>
          <xsl:copy-of select ="TotalPrice"/>
          <xsl:copy-of select ="markUp"/>
          <xsl:copy-of select ="Commission"/>
          <xsl:choose>
            <xsl:when test ="Safi">
              <xsl:copy-of select ="Safi"/>
            </xsl:when>
          </xsl:choose>
          <xsl:copy-of select ="ExtraCharges"/>
          <xsl:choose>
            <xsl:when test ="GrandTotal">
              <xsl:copy-of select ="GrandTotal"/>
            </xsl:when>
            <xsl:otherwise>
              <GrandTotal></GrandTotal>
            </xsl:otherwise>
          </xsl:choose>
          <xsl:copy-of select ="Currency"/>
          <xsl:copy-of select ="FareType"/>
          <xsl:copy-of select ="Adult"/>
          <xsl:copy-of select ="Child"/>
          <xsl:copy-of select ="Infant"/>
          <xsl:copy-of select ="IndexNumber"/>
          <xsl:copy-of select ="ItineraryId"/>
          <xsl:copy-of select ="URL"/>
          <xsl:copy-of select ="Provider"/>
          <xsl:copy-of select ="AirOfferID"/>
          <xsl:copy-of select ="ValCarrier"/>
          <xsl:copy-of select ="LastTicketingDate"/>
          <xsl:variable name ="PosSectors" select ="position()"></xsl:variable>
          <xsl:variable name ="Provider" select ="Provider"></xsl:variable>
          <Sectors>
            <xsl:for-each select ="Sectors/Sector">
              <Sector>
                <xsl:attribute name ="nearby">
                  <xsl:value-of select ="@nearby"/>
                </xsl:attribute>
                <xsl:attribute name ="isConnect">
                  <xsl:value-of select ="@isConnect"/>
                </xsl:attribute>
                <xsl:attribute name ="isStopover">
                  <xsl:value-of select ="@isStopover"/>
                </xsl:attribute>
                <xsl:copy-of select ="AirV"/>
                <xsl:copy-of select ="AirlineName"/>
                <xsl:copy-of select ="AirlineLogoPath"/>
                <xsl:copy-of select ="Class"/>
                <xsl:choose>
                  <xsl:when test ="$Provider='1S' or $Provider='1A'">
                    <xsl:copy-of select ="CabinClass"/>
                  </xsl:when>
                  <xsl:otherwise>
                    <CabinClass>
                      <Code>
                        <xsl:value-of select ="$CabinClass"/>
                      </Code>
                      <Des>
                        <xsl:call-template name ="GetClassDescription">
                          <xsl:with-param name ="ClassCode" select ="$CabinClass"></xsl:with-param>
                        </xsl:call-template>
                      </Des>
                    </CabinClass>
                  </xsl:otherwise>
                </xsl:choose>

                <xsl:copy-of select ="NoSeats"/>
                <xsl:copy-of select ="FltNum"/>
                <xsl:copy-of select ="Departure"/>
                <xsl:copy-of select ="Arrival"/>
                <xsl:copy-of select ="EquipType"/>
                <xsl:copy-of select ="ElapsedTime"/>
                <xsl:copy-of select ="ActualTime"/>
                <xsl:copy-of select ="TechStopOver"/>
                <xsl:copy-of select ="TechStopOverDetail"/>
                <xsl:copy-of select ="Meal"/>
                <xsl:copy-of select ="Status"/>
                <isReturn>
                  <!--<xsl:choose>
                    <xsl:when test ="$Provider='1P'">
                      <xsl:variable name ="isRet">
                        <xsl:if test ="$Departure = Departure/AirpCode">
                          <xsl:value-of select ="true"/>
                        </xsl:if>
                      </xsl:variable>
                      <xsl:call-template name ="IsReturn">
                        <xsl:with-param name ="Departure" select ="$Departure"></xsl:with-param>
                        <xsl:with-param name ="Arrival" select ="DEP_ARP"></xsl:with-param>
                        <xsl:with-param name ="Pos" select ="position()"></xsl:with-param>
                        <xsl:with-param name ="isret" select ="$isRet"></xsl:with-param>
                        <xsl:with-param name ="PosSectors" select ="$PosSectors"></xsl:with-param>
                      </xsl:call-template>
                    </xsl:when>
                    <xsl:otherwise>-->
                  <xsl:value-of select ="isReturn"/>
                  <!--</xsl:otherwise>
                  </xsl:choose>-->

                </isReturn>
                <xsl:element name ="OptrCarrier">
                  <xsl:attribute name ="OptrCarrierDes">
                    <xsl:value-of select ="OptrCarrierDes"/>
                  </xsl:attribute>
                  <xsl:value-of select ="OptrCarrier"/>
                </xsl:element>
                <xsl:element name ="MrktCarrier">
                  <xsl:attribute name ="MrktCarrierDes">
                    <xsl:value-of select ="MrktCarrierDes"/>
                  </xsl:attribute>
                  <xsl:value-of select ="MrktCarrier"/>
                </xsl:element>
                <xsl:copy-of select ="HostToken"/>
              </Sector>
            </xsl:for-each>
          </Sectors>
          <xsl:copy-of select ="FareBasisCodes"/>
        </Itinerary>
      </xsl:for-each>
      <xsl:copy-of select ="IsCache"/>
    </Itineraries>
  </xsl:template>
  <xsl:template name ="IsReturn">
    <xsl:param name ="Departure"></xsl:param>
    <xsl:param name ="Arrival"></xsl:param>
    <xsl:param name ="Pos"></xsl:param>
    <xsl:param name ="isret"></xsl:param>
    <xsl:param name ="PosSectors"></xsl:param>
    <xsl:variable name ="exexPos">
      <xsl:call-template name ="Match">
        <xsl:with-param name ="PosSectors" select ="$PosSectors"></xsl:with-param>
        <xsl:with-param name ="depart" select ="$Departure"></xsl:with-param>
        <xsl:with-param name ="arr" select ="$Arrival"></xsl:with-param>
      </xsl:call-template>
    </xsl:variable>
    <xsl:choose>
      <xsl:when test ="$Pos &lt; $exexPos">
        <xsl:value-of select ="'false'"/>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select ="'true'"/>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template name ="Match">
    <xsl:param name ="depart"></xsl:param>
    <xsl:param name ="arr"></xsl:param>
    <xsl:param name ="PosSectors"></xsl:param>
    <xsl:for-each select ="//Itinerary[$PosSectors]/Sectors/Sector">
      <xsl:if test ="$depart= Departure/AirpCode or $depart= Departure/CityCode">
        <xsl:value-of select ="position()"/>
      </xsl:if>
    </xsl:for-each>
  </xsl:template>
  <xsl:template name="GetClassDescription">
    <xsl:param name="ClassCode"/>
    <xsl:choose>
      <xsl:when test="$ClassCode = 'Y'">ECONOMY</xsl:when>
      <xsl:when test="$ClassCode = 'W'">PREMIUM (ECO)</xsl:when>
      <xsl:when test="$ClassCode = 'C'">BUSINESS</xsl:when>
      <xsl:when test="$ClassCode = 'F'">FIRST</xsl:when>
      <xsl:when test="$ClassCode = 'M'">STANDARD</xsl:when>
    </xsl:choose>
  </xsl:template>
</xsl:stylesheet>
