<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl" xmlns:func="urn:actl-xslt">
  <xsl:output method="html" indent="yes"/>
  <xsl:template match="Itinerary">
    <xsl:variable name="InboundNo" select="count(Sectors/Sector[isReturn='false'])"/>
    <xsl:variable name="OutboundNo" select="count(Sectors/Sector[isReturn='true'])"/>     
      <div class="panel-body">
        <ul class="booking-list">
          <li>
            <div class="booking-item-container">
              <div class="row ">
                <div class="col-md-12">
                  <xsl:for-each select="Sectors/Sector[isReturn = 'false']">
                    <xsl:if test ="position() !=1">
                      <div class="row">
                        <div class="col-md-12">
                          <p class="bg-warning text-center text-danger">
                            <small>
                              <xsl:text >Layover: Connection of </xsl:text>
                              <xsl:value-of select ="substring(TransitTime/@time,1,2)"/>
                              <xsl:text>hrs </xsl:text>
                              <xsl:value-of select="substring(TransitTime/@time,4,2)"/>
                              <xsl:text>min</xsl:text>
                              <xsl:text > in </xsl:text>
                              <xsl:value-of select="Departure/AirpName"/>
                              <xsl:text>, </xsl:text>
                              <xsl:value-of select="Departure/CityName"/>
                              <xsl:text>, </xsl:text>
                              <xsl:value-of select="Departure/CountryCode"/>
                            </small>
                          </p>
                        </div>
                      </div>
                    </xsl:if>
                    <div class="row ">
                      <div class="col-md-1">
                        <i class="fa fa-plane fa-2x"></i>
                      </div>
                      <div class="col-md-2 right-border">
                        <div class="booking-item-airline-logo">
                          <!--<img src="images/TK.gif" alt="Image Alternative text" title="Image Title" />-->
                          <xsl:element name="img">
                            <xsl:attribute name ="src">
                              <xsl:call-template name="string-replace-all">
                                <xsl:with-param name="text" select="AirlineLogoPath" />
                                <xsl:with-param name="replace" select="'http:'" />
                                <xsl:with-param name="by" select="'https:'" />
                              </xsl:call-template>
                            </xsl:attribute>
                            <xsl:attribute name="alt">
                              <xsl:value-of select="AirlineName"/>
                            </xsl:attribute>
                            <xsl:attribute name="title">
                              <xsl:value-of select="AirlineName"/>
                            </xsl:attribute>
                          </xsl:element>
                          <p>
                            <xsl:value-of select="AirlineName"/>
                          </p>
                        </div>
                      </div>
                      <div class="col-md-9">
                        <div class="booking-item-flight-details">
                          <div class="booking-item-arrival right-border">
                            <p class="booking-item-destination">
                              <!--New York, NY, United States (JFK)-->
                              <xsl:value-of select="Departure/CityName"/>
                              <xsl:text>, </xsl:text>
                              <xsl:value-of select="Departure/CountryCode"/>
                              <xsl:text>, </xsl:text>
                              <xsl:value-of select="Departure/CountryName"/>
                              <xsl:text> (</xsl:text>
                              <xsl:value-of select="Departure/AirpCode"/>
                              <xsl:text>)</xsl:text>
                            </p>
                            <p class="booking-item-date">
                              <!--12:25 PM , Sat, Mar 23-->
                              <xsl:element name="span">
                                <xsl:value-of select="Departure/Time"/>
                                <xsl:text>, </xsl:text>
                                <xsl:variable name="month">
                                  <xsl:call-template name ="GetMonthName">
                                    <xsl:with-param name ="Month" select ="substring(Departure/Date,4,2)"></xsl:with-param>
                                  </xsl:call-template>
                                </xsl:variable>
                                <xsl:value-of select="concat(substring(Departure/Date,1,2),' ',$month,' ',substring(Departure/Date,7,4))"/>
                              </xsl:element>
                            </p>
                          </div>
                          <div class="booking-item-arrival-stop right-border">
                            <h5>
                              <!--22h 50m-->
                              <xsl:value-of select="concat(substring(ActualTime,1,2),' ')"/>h&#160;<xsl:value-of select="concat(substring(ActualTime,4,2),' ')"/>m
                            </h5>
                            <img src="images/re-arrow.png" ></img>
                            <p>
                              <!--non-stop-->
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
                            </p>
                          </div>
                          <div class="booking-item-arrival ">
                            <p class="booking-item-destination">
                              <!--New York, NY, United States (JFK)-->
                              <xsl:value-of select="Arrival/CityName"/>
                              <xsl:text>, </xsl:text>
                              <xsl:value-of select="Arrival/CountryCode"/>
                              <xsl:text>, </xsl:text>
                              <xsl:value-of select="Arrival/CountryName"/>
                              <xsl:text> (</xsl:text>
                              <xsl:value-of select="Arrival/AirpCode"/>
                              <xsl:text>)</xsl:text>
                            </p>
                            <p class="booking-item-date">
                              <!--12:25 PM , Sat, Mar 23-->
                              <xsl:element name="span">
                                <xsl:value-of select="Arrival/Time"/>
                                <xsl:text>, </xsl:text>
                                <xsl:variable name="month">
                                  <xsl:call-template name ="GetMonthName">
                                    <xsl:with-param name ="Month" select ="substring(Arrival/Date,4,2)"></xsl:with-param>
                                  </xsl:call-template>
                                </xsl:variable>
                                <xsl:value-of select="concat(substring(Arrival/Date,1,2),' ',$month,' ',substring(Arrival/Date,7,4))"/>
                              </xsl:element>
                            </p>
                          </div>
                        </div>
                      </div>
                    </div>
                  </xsl:for-each>
                  <xsl:if test="count(Sectors/Sector[isReturn='true']) &gt; 0">
                    <xsl:for-each select="Sectors/Sector[isReturn='true']">
                      <xsl:if test ="position() =1">
                        <div class="row">
                          <div class="col-md-12">
                            <p class="bg-warning text-center text-danger">
                              <hr/>
                            </p>
                          </div>
                        </div>
                      </xsl:if>
                      <xsl:if test ="position() !=1">
                        <div class="row">
                          <div class="col-md-12">
                            <p class="bg-warning text-center text-danger">
                              <small>
                                <!--Layover: Connection of 2 Hours 5 Mins in Doha International Arpt, Doha, Qatar-->
                                <xsl:text >Layover: Connection of </xsl:text>
                                <xsl:value-of select ="substring(TransitTime/@time,1,2)"/>
                                <xsl:text>hrs </xsl:text>
                                <xsl:value-of select="substring(TransitTime/@time,4,2)"/>
                                <xsl:text>min</xsl:text>
                                <xsl:text > in </xsl:text>
                                <xsl:value-of select="Departure/AirpName"/>
                                <xsl:text>, </xsl:text>
                                <xsl:value-of select="Departure/CityName"/>
                                <xsl:text>, </xsl:text>
                                <xsl:value-of select="Departure/CountryCode"/>
                              </small>
                            </p>
                          </div>
                        </div>
                      </xsl:if>
                      <div class="row ">
                        <div class="col-md-1">
                          <i class="fa fa-plane fa-2x fa-flip-vertical"></i>
                        </div>
                        <div class="col-md-2 right-border">
                          <div class="booking-item-airline-logo">
                            <!--<img src="images/TK.gif" alt="Image Alternative text" title="Image Title" />
                          <p>
                            Lufthansa
                          </p>-->
                            <xsl:element name="img">
                              <xsl:attribute name ="src">
                                <xsl:call-template name="string-replace-all">
                                  <xsl:with-param name="text" select="AirlineLogoPath" />
                                  <xsl:with-param name="replace" select="'http:'" />
                                  <xsl:with-param name="by" select="'https:'" />
                                </xsl:call-template>
                              </xsl:attribute>
                              <xsl:attribute name="alt">
                                <xsl:value-of select="AirlineName"/>
                              </xsl:attribute>
                              <xsl:attribute name="title">
                                <xsl:value-of select="AirlineName"/>
                              </xsl:attribute>
                            </xsl:element>
                            <p>
                              <xsl:value-of select="AirlineName"/>
                            </p>
                          </div>
                        </div>
                        <div class="col-md-9">
                          <div class="booking-item-flight-details">
                            <div class="booking-item-arrival right-border">
                              <p class="booking-item-destination">
                                <!--New York, NY, United States (JFK)-->
                                <xsl:value-of select="Departure/CityName"/>
                                <xsl:text>, </xsl:text>
                                <xsl:value-of select="Departure/CountryCode"/>
                                <xsl:text>, </xsl:text>
                                <xsl:value-of select="Departure/CountryName"/>
                                <xsl:text> (</xsl:text>
                                <xsl:value-of select="Departure/AirpCode"/>
                                <xsl:text>)</xsl:text>
                              </p>
                              <p class="booking-item-date">
                                <!--12:25 PM , Sat, Mar 23-->
                                <xsl:element name="span">
                                  <xsl:value-of select="Departure/Time"/>
                                  <xsl:text>, </xsl:text>
                                  <xsl:variable name="month">
                                    <xsl:call-template name ="GetMonthName">
                                      <xsl:with-param name ="Month" select ="substring(Departure/Date,4,2)"></xsl:with-param>
                                    </xsl:call-template>
                                  </xsl:variable>
                                  <xsl:value-of select="concat(substring(Departure/Date,1,2),' ',$month,' ',substring(Departure/Date,7,4))"/>
                                </xsl:element>
                              </p>
                            </div>
                            <div class="booking-item-arrival-stop right-border">
                              <h5>
                                <!--22h 50m-->
                                <xsl:value-of select="concat(substring(ActualTime,1,2),' ')"/>h&#160;<xsl:value-of select="concat(substring(ActualTime,4,2),' ')"/>m
                              </h5>
                              <img src="images/re-arrow.png" ></img>
                              <p>
                                <!--non-stop-->
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
                              </p>
                            </div>
                            <div class="booking-item-arrival ">
                              <p class="booking-item-destination">
                                <xsl:value-of select="Arrival/CityName"/>
                                <xsl:text>, </xsl:text>
                                <xsl:value-of select="Arrival/CountryCode"/>
                                <xsl:text>, </xsl:text>
                                <xsl:value-of select="Arrival/CountryName"/>
                                <xsl:text> (</xsl:text>
                                <xsl:value-of select="Arrival/AirpCode"/>
                                <xsl:text>)</xsl:text>
                              </p>
                              <p class="booking-item-date">
                                <xsl:element name="span">
                                  <xsl:value-of select="Arrival/Time"/>
                                  <xsl:text>, </xsl:text>
                                  <xsl:variable name="month">
                                    <xsl:call-template name ="GetMonthName">
                                      <xsl:with-param name ="Month" select ="substring(Arrival/Date,4,2)"></xsl:with-param>
                                    </xsl:call-template>
                                  </xsl:variable>
                                  <xsl:value-of select="concat(substring(Arrival/Date,1,2),' ',$month,' ',substring(Arrival/Date,7,4))"/>
                                </xsl:element>
                              </p>
                            </div>
                          </div>
                        </div>
                      </div>
                    </xsl:for-each>
                  </xsl:if>
                </div>
              </div>
            </div>
          </li>
        </ul>
      </div>    
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
