<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
  <xsl:output method="xml" indent="yes"/>
  <xsl:param name ="Adult" select ="1"></xsl:param>
  <xsl:param name ="Child" select ="1"></xsl:param>
  <xsl:param name ="Infant" select ="1"></xsl:param>
  <xsl:template match="AirOffers">
    <Itineraries>
      <xsl:for-each select ="AirOffer">
        <Itinerary>
          <xsl:variable name ="basefare" select ="format-number((AirPrice[PaxType='ADT']/BaseFare * $Adult) + (AirPrice[PaxType='CHD']/BaseFare * $Child) + (AirPrice[PaxType='INF']/BaseFare * $Infant),'0.00')"></xsl:variable>
          <xsl:variable name ="Taxes" select ="format-number((AirPrice[PaxType='ADT']/Taxes * $Adult) + (AirPrice[PaxType='CHD']/Taxes * $Child) + (AirPrice[PaxType='INF']/Taxes * $Infant),'0.00')"></xsl:variable>
          <BaseFare>
            <xsl:value-of select ="format-number((AirPrice[PaxType='ADT']/BaseFare * $Adult) + (AirPrice[PaxType='CHD']/BaseFare * $Child) + (AirPrice[PaxType='INF']/BaseFare * $Infant),'0.00')"/>
          </BaseFare>
          <Taxes>
            <xsl:value-of select ="format-number((AirPrice[PaxType='ADT']/Taxes * $Adult) + (AirPrice[PaxType='CHD']/Taxes * $Child) + (AirPrice[PaxType='INF']/Taxes * $Infant),'0.00')"/>
          </Taxes>
          <TotalPrice>
            <xsl:value-of select ="format-number(($basefare+$Taxes),'0.00')"/>
          </TotalPrice>
          <markUp>0.00</markUp>
          <!--<xsl:value-of select ="format-number((AirPrice[PaxType='ADT']/Markup * $Adult) + (AirPrice[PaxType='CHD']/Markup * $Child) + (AirPrice[PaxType='INF']/Markup * $Infant),'0.00')"/>-->
          <Commission>0.00</Commission>
          <GrandTotal>
            <xsl:value-of select ="format-number(($basefare+$Taxes),'0.00')"/>
          </GrandTotal>
          <xsl:copy-of select ="AirSector[1]/AirOfferID"/>
          <FareType>RA</FareType>
          <xsl:for-each select ="AirPrice">
            <xsl:if test ="$Adult &gt; 0 and PaxType='ADT'">
              <Adult>
                <NoAdult>
                  <xsl:value-of select ="$Adult"/>
                </NoAdult>
                <AdTax>
                  <xsl:value-of select ="format-number(Taxes,'0.00')"/>
                </AdTax>
                <AdtBFare>
                  <xsl:value-of select ="format-number(BaseFare,'0.00')"/>
                </AdtBFare>
                <!--<markUp>
                    <xsl:value-of select ="format-number(Markup,'0.00')"/>
                  </markUp>
                  <Commission>0.00</Commission>-->
              </Adult>
            </xsl:if>
            <xsl:if test ="$Child &gt; 0 and PaxType='CHD'">
              <Child>
                <NoChild>
                  <xsl:value-of select ="$Child"/>
                </NoChild>
                <CHTax>
                  <xsl:value-of select ="format-number(Taxes,'0.00')"/>
                </CHTax>
                <ChdBFare>
                  <xsl:value-of select ="format-number(BaseFare,'0.00')"/>
                </ChdBFare>
                <!--<markUp>
                    <xsl:value-of select ="format-number(Markup,'0.00')"/>
                  </markUp>
                  <Commission>0.00</Commission>-->
              </Child>
            </xsl:if>
            <xsl:if test ="$Infant &gt; 0 and PaxType='INF'">
              <Infant>
                <NoInfant>
                  <xsl:value-of select ="$Infant"/>
                </NoInfant>
                <InfBFare>
                  <xsl:value-of select ="format-number(BaseFare,'0.00')"/>
                </InfBFare>
                <InTax>
                  <xsl:value-of select ="format-number(Taxes,'0.00')"/>
                </InTax>
                <!--<markUp>
                    <xsl:value-of select ="format-number(Markup,'0.00')"/>
                  </markUp>
                  <Commission>0.00</Commission>-->
              </Infant>
            </xsl:if>
          </xsl:for-each>
          <IndexNumber>
            <xsl:value-of select ="position()-1"/>
          </IndexNumber>
          <ValCarrier>
            <xsl:value-of select ="AirSector[1]/CarierName"/>
          </ValCarrier>
          <Provider>
            <xsl:value-of select ="//Provider[1]"/>
          </Provider>
          <LastTicketingDate></LastTicketingDate>
          <Sectors>
            <xsl:for-each select ="AirSector">
              <Sector>
                <AirV>
                  <xsl:value-of select ="CarierName"/>
                </AirV>
                <Class>
                  <xsl:value-of select ="Class"/>
                </Class>
                <NoSeats></NoSeats>
                <FltNum>
                  <xsl:value-of select ="FltNumber"/>
                </FltNum>
                <Departure>
                  <AirpCode>
                    <xsl:value-of select ="FromDestination"/>
                  </AirpCode>
                  <Terminal></Terminal>
                  <Date>
                    <xsl:call-template name ="StringReplace">
                      <xsl:with-param name="text" select ="DepartDate" />
                      <xsl:with-param name="replace" select ="'/'" />
                      <xsl:with-param name="by" select ="'-'" />
                    </xsl:call-template>
                  </Date>
                  <Time>
                    <xsl:value-of select ="concat(substring(DepartTime,1,2),':',substring(DepartTime,3,2))"/>
                  </Time>
                </Departure>
                <Arrival>
                  <AirpCode>
                    <xsl:value-of select ="ToDestination"/>
                  </AirpCode>
                  <Terminal></Terminal>
                  <Date>
                    <xsl:call-template name ="StringReplace">
                      <xsl:with-param name="text" select ="ArrivalDate" />
                      <xsl:with-param name="replace" select ="'/'" />
                      <xsl:with-param name="by" select ="'-'" />
                    </xsl:call-template>
                  </Date>
                  <Time>
                    <xsl:value-of select ="concat(substring(ArrivalTime,1,2),':',substring(ArrivalTime,3,2))"/>
                  </Time>
                </Arrival>
                <EquipType>
                  <xsl:call-template name ="EquipType">
                    <xsl:with-param name ="Val" select ="Equipment"></xsl:with-param>
                  </xsl:call-template>
                </EquipType>
                <ElapsedTime></ElapsedTime>
                <ActualTime></ActualTime>
                <TechStopOver>0</TechStopOver>
                <MealCode>
                  <xsl:value-of select ="Meal"/>
                </MealCode>
                <Status>OK</Status>
                <isReturn>false</isReturn>
                <OptrCarrier>
                  <xsl:value-of select ="CarierName"/>
                </OptrCarrier>
                <MrktCarrier>
                  <xsl:value-of select ="CarierName"/>
                </MrktCarrier>
              </Sector>
            </xsl:for-each>
          </Sectors>
        </Itinerary>
      </xsl:for-each>
    </Itineraries>
  </xsl:template>
  <xsl:template name="StringReplace">
    <xsl:param name="text" />
    <xsl:param name="replace" />
    <xsl:param name="by" />
    <xsl:choose>
      <xsl:when test="contains($text, $replace)">
        <xsl:value-of select="substring-before($text,$replace)" />
        <xsl:value-of select="$by" />
        <xsl:call-template name="StringReplace">
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
  <xsl:template name ="EquipType">
    <xsl:param name ="Val"></xsl:param>
    <xsl:variable name ="Type" select ="substring($Val,1,1)"/>
    <xsl:if test ="$Type = A">
      <xsl:value-of select ="concat('Airbus A',$Val)"/>
    </xsl:if>
    <xsl:if test ="$Type = 3">
      <xsl:value-of select ="concat('Airbus A',$Val)"/>
    </xsl:if>
    <xsl:if test ="$Type = 7">
      <xsl:value-of select ="concat('Boeing ',$Val)"/>
    </xsl:if>
    <xsl:if test ="$Type = M">
      <xsl:value-of select ="concat('Boeing ',$Val)"/>
    </xsl:if>
    <xsl:if test ="$Type = E">
      <xsl:value-of select ="concat('Embraer ',$Val)"/>
    </xsl:if>
    <xsl:if test ="$Type = C">
      <xsl:value-of select ="concat('Canadair Regional Jet ',$Val)"/>
    </xsl:if>
  </xsl:template>
</xsl:stylesheet>
