<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <xsl:param name="type" select="'type'"/>
  <xsl:output method="html" indent="yes"/>
  <xsl:template match="BookingXML">
    <table border="0" align="center" cellpadding="0" cellspacing="0" width="900px">
      <tr>
        <td style="font-family: Arial, Helvetica, sans-serif;">
          <table border="0" cellpadding="0" cellspacing="0" width="900px">
            <tr>
              <td style="font-size: 13px; color: #000000; font-weight: bold; padding: 5px 10px 5px 10px;"
                  width="190px">
                Booking Reference
              </td>
              <td style="font-size: 13px; color: #000000; font-weight: bold;" width="10px">
                :-
              </td>
              <td style="font-size: 12px; color: #000000; padding: 5px 10px 5px 10px;" width="250px">
                <xsl:value-of select="TempBookingId"/>
              </td>
              <td style="font-size: 13px; color: #000000; font-weight: bold; padding: 5px 10px 5px 10px;"
                  width="190px">
                Journey Locator
              </td>
              <td style="font-size: 13px; color: #000000; font-weight: bold;" width="10px">
                :-
              </td>
              <td style="font-size: 12px; color: #000000; padding: 5px 10px 5px 10px;" width="250px">
                <xsl:value-of select="TempBookingId"/>
              </td>
            </tr>
            <tr>
              <td style="font-size: 13px; color: #000000; font-weight: bold; padding: 5px 10px 5px 10px;"
                  width="190px">
                Email Address
              </td>
              <td style="font-size: 13px; color: #000000; font-weight: bold;" width="10px">
                :-
              </td>
              <td style="font-size: 12px; color: #000000; padding: 5px 10px 5px 10px;" width="250px">
                <xsl:value-of select="ClientDetail/Email"></xsl:value-of>
              </td>
              <td style="font-size: 13px; color: #000000; font-weight: bold; padding: 5px 10px 5px 10px;"
                  width="190px">
                Destination
              </td>
              <td style="font-size: 13px; color: #000000; font-weight: bold;" width="10px">
                :-
              </td>
              <td style="font-size: 12px; color: #000000; padding: 5px 10px 5px 10px;" width="250px">
                <xsl:value-of select="FlightDetail/Itinerary/Sectors/Sector[isReturn='false'][last()]/Arrival/CityName"></xsl:value-of>
                <xsl:text>, </xsl:text>
                <xsl:value-of select="FlightDetail/Itinerary/Sectors/Sector[isReturn='false'][last()]/Arrival/AirpName"></xsl:value-of>
                <xsl:text>, </xsl:text>
                <xsl:value-of select="FlightDetail/Itinerary/Sectors/Sector[isReturn='false'][last()]/Arrival/AirpCode"></xsl:value-of>

              </td>
            </tr>
            <tr>
              <td style="font-size: 13px; color: #000000; font-weight: bold; padding: 5px 10px 5px 10px;"
                  width="190px">
                Phone No.
              </td>
              <td style="font-size: 13px; color: #000000; font-weight: bold;" width="10px">
                :-
              </td>
              <td style="font-size: 12px; color: #000000; padding: 5px 10px 5px 10px;" width="250px">
                <xsl:value-of select="ClientDetail/Phone"></xsl:value-of>
              </td>
              <td style="font-size: 13px; color: #000000; font-weight: bold; padding: 5px 10px 5px 10px;"
                  width="190px">
                Booking Date
              </td>
              <td style="font-size: 13px; color: #000000; font-weight: bold;" width="10px">
                :-
              </td>
              <td style="font-size: 12px; color: #000000; padding: 5px 10px 5px 10px;" width="250px">
                <xsl:value-of select="Payment/BookingDate"/>
              </td>
            </tr>
          </table>
        </td>
      </tr>
      <tr>
        <td style="font-size: 12px; color: #ff0000; padding: 5px 10px 5px 10px;">
          Your bookings NOT generated. This is due to Non Availability of the seats while
          confirming the booking. Our consultants will try to get the same seats and if not,
          they will advise you. You can call them as well at +44 (0) 208 478 8911,+44 (0)
          800 368 0311 (08:00 am to 08:00 pm). Our consultants will not issue this ticket
          unless they get the confirmation from you. You can try for higher class.
        </td>
      </tr>
      <tr>
        <td style="font-size: 12px; color: #000000; padding: 5px 10px 5px 10px;">
          <br />
          <fieldset>
            <legend style="font-size: 13px; color: #000000; font-weight: bold;">Passenger(s)</legend>
            <table width="800px">
              <tr style="font-size: 13px; font-family: Arial, Helvetica, sans-serif; font-weight: bold; color: #005193;">
                <td>
                  S.No
                </td>
                <td>
                  Pax Type
                </td>
                <td>
                  Name
                </td>
                <td>
                  Date Of Birth
                </td>
              </tr>
              <xsl:for-each select="PaxDetail/Adult">
                <tr style="font-size: 12px; font-family: Arial, Helvetica, sans-serif; font-weight: normal; color: #000000;">
                  <td>
                    <xsl:value-of select="position()" />
                  </td>
                  <td>
                    Adult
                  </td>
                  <td>
                    <xsl:value-of select="concat(Title,' ',FirstName,' ',LastName)"/>
                  </td>
                  <td>
                    <xsl:value-of select="DOB"/>
                  </td>
                </tr>
              </xsl:for-each>
              <xsl:for-each select="PaxDetail/Child">
                <tr style="font-size: 12px; font-family: Arial, Helvetica, sans-serif; font-weight: normal; color: #000000;">
                  <td>
                    <xsl:value-of select="count(//PaxDetail/Adult)+position()" />
                  </td>
                  <td>
                    Child
                  </td>
                  <td>
                    <xsl:value-of select="concat(Title,' ',FirstName,' ',LastName)"/>
                  </td>
                  <td>
                    <xsl:value-of select="DOB"/>
                  </td>
                </tr>
              </xsl:for-each>
              <xsl:for-each select="PaxDetail/Infant">
                <tr style="font-size: 12px; font-family: Arial, Helvetica, sans-serif; font-weight: normal;
                        color: #000000;">
                  <td>
                    <xsl:value-of select="position()+count(//PaxDetail/Adult)+count(//PaxDetail/Child)" />
                  </td>
                  <td>
                    Infant
                  </td>
                  <td>
                    <xsl:value-of select="concat(Title,' ',FirstName,' ',LastName)"/>
                  </td>
                  <td>
                    <xsl:value-of select="DOB"/>
                  </td>
                </tr>
              </xsl:for-each>
            </table>
          </fieldset>
        </td>
      </tr>
      <tr>
        <td style="font-size: 12px; color: #000000; padding: 5px 10px 5px 10px;">
          <br />
          <fieldset>
            <legend style="font-size: 13px; color: #000000; font-weight: bold;">
              Flight Details
            </legend>
            <table cellpadding="2" cellspacing="2" width="100%">
              <tr style="font-size: 12px; font-family: Arial, Helvetica, sans-serif; font-weight: bold;
                        color: #005193;">
                <td width="19%">
                  From
                </td>
                <td width="19%">
                  To
                </td>
                <td width="7%">
                  Flight No
                </td>
                <td width="9%">
                  Cabin Class
                </td>
                <td width="5%">
                  Ter.In
                </td>
                <td width="5%">
                  Ter.Out
                </td>
                <td width="18%">
                  Dept. Date And Time
                </td>
                <td width="18%">
                  Arrival Date And Time
                </td>
              </tr>
              <xsl:for-each select="FlightDetail/Itinerary/Sectors/Sector">
                <tr style="font-size: 12px; font-family: Arial, Helvetica, sans-serif; font-weight: normal;
                        color: #000000;">
                  <td>
                    <xsl:value-of select="Departure/CityName"/>,<br />
                    <xsl:value-of select="Departure/AirpName"/> (<xsl:value-of select="Departure/AirpCode"/>)
                  </td>
                  <td>
                    <xsl:value-of select="Arrival/CityName"/>,<br />
                    <xsl:value-of select="Arrival/AirpName"/> (<xsl:value-of select="Arrival/AirpCode"/>)
                  </td>
                  <td valign="top">
                    <xsl:value-of select="AirV"/>
                    <xsl:text> </xsl:text>
                    <xsl:value-of select="FltNum"/>
                  </td>
                  <td valign="top">
                    <xsl:value-of select="Class"/>
                  </td>
                  <td valign="top">
                    <xsl:value-of select="Departure/Terminal"/>
                  </td>
                  <td valign="top">
                    <xsl:value-of select="Arrival/Terminal"/>
                  </td>
                  <td valign="top" style="font-size: 11px;">
                    <xsl:variable name ="ConvertedDepDate">
                      <xsl:variable name ="TravDay">
                        <xsl:value-of select ="concat(Departure/Day,',')"></xsl:value-of>
                      </xsl:variable>
                      <xsl:variable name ="TripDate">
                        <xsl:call-template name ="DateFormatFull">
                          <xsl:with-param name ="Date" select ="Departure/Date"></xsl:with-param>
                        </xsl:call-template>
                      </xsl:variable>
                      <xsl:value-of select ="concat($TravDay,' ',$TripDate,' ',Departure/Time)"/>
                    </xsl:variable>
                    <xsl:value-of select="$ConvertedDepDate"/>
                  </td>
                  <td valign="top" style="font-size: 11px;">
                    <xsl:variable name ="ConvertedArrDate">
                      <xsl:variable name ="TravDay">
                        <xsl:value-of select ="concat(Arrival/Day,',')"></xsl:value-of>
                      </xsl:variable>
                      <xsl:variable name ="TripDate">
                        <xsl:call-template name ="DateFormatFull">
                          <xsl:with-param name ="Date" select ="Arrival/Date"></xsl:with-param>
                        </xsl:call-template>
                      </xsl:variable>
                      <xsl:value-of select ="concat($TravDay,' ',$TripDate,' ',Arrival/Time)"/>
                    </xsl:variable>
                    <xsl:value-of select="$ConvertedArrDate"/>
                  </td>
                </tr>
              </xsl:for-each>
            </table>
          </fieldset>
        </td>
      </tr>
      <tr>
        <td style="padding: 5px 10px 5px 10px;" align="left">
          <br />
          <fieldset>
            <legend style="font-size: 13px; color: #000000; font-weight: bold;">Price Details</legend>
            <table width="800" border="0" cellspacing="0" cellpadding="0">
              <tr style="font-size: 13px; font-family: Arial, Helvetica, sans-serif; font-weight: bold;
                        color: #005193;">
                <td width="150">
                  Pax Type
                </td>
                <td width="200" height="20">
                  Fare per person
                </td>
                <td width="50">

                </td>
                <td width="100">
                  Number of pax
                </td>
                <td width="50">

                </td>
                <td width="150">
                  Total Price
                </td>
              </tr>
              <tr>
                <td height="10" colspan="6">
                </td>
              </tr>
              <xsl:for-each select="FlightDetail/Itinerary/Adult">
                <tr style="font-size: 12px; font-family: Arial, Helvetica, sans-serif; color: #000000;">
                  <td>
                    Adult
                  </td>
                  <td>
                    <xsl:value-of select="format-number(AdtBFare+AdTax+markUp+Commission,'#.00')"/>
                  </td>
                  <td>
                    X
                  </td>
                  <td>
                    <xsl:value-of select="number(NoAdult)"/>
                  </td>
                  <td>
                    =
                  </td>
                  <td>
                    <xsl:value-of select="format-number(number(AdtBFare+AdTax+markUp+Commission)*number(NoAdult),'#.00')"/>
                  </td>
                </tr>
              </xsl:for-each>
              <xsl:for-each select="FlightDetail/Itinerary/Child">
                <tr style="font-size: 12px; font-family: Arial, Helvetica, sans-serif; color: #000000;">
                  <td>
                    Child
                  </td>
                  <td>
                    <xsl:value-of select="format-number(ChdBFare+CHTax+markUp+Commission,'#.00')"/>
                  </td>
                  <td>
                    X
                  </td>
                  <td>
                    <xsl:value-of select="number(NoChild)"/>
                  </td>
                  <td>
                    =
                  </td>
                  <td>
                    <xsl:value-of select="format-number(number(ChdBFare+CHTax+markUp+Commission)*number(NoChild),'#.00')"/>
                  </td>
                </tr>
              </xsl:for-each>
              <xsl:for-each select="FlightDetail/Itinerary/Infant">
                <tr style="font-size: 12px; font-family: Arial, Helvetica, sans-serif; color: #000000;">
                  <td>
                    Infant
                  </td>
                  <td>
                    <xsl:value-of select="format-number(InfBFare+InTax+markUp+Commission,'#.00')"/>
                  </td>
                  <td>
                    X
                  </td>
                  <td>
                    <xsl:value-of select="number(NoInfant)"/>
                  </td>
                  <td>
                    =
                  </td>
                  <td>
                    <xsl:value-of select="format-number(number(InfBFare+InTax+markUp+Commission)*number(NoInfant),'#.00')"/>
                  </td>
                </tr>
              </xsl:for-each>
              <tr>
                <td height="10" colspan="6">
                </td>
              </tr>
              <tr>
                <td height="10" colspan="6">
                  <hr />
                </td>
              </tr>
              <xsl:if test="Payment/AtolStatus='true'">
                <tr style="font-size: 12px; font-family: Arial, Helvetica, sans-serif; color: #000000;">
                  <td>
                    Airlines Failure Fee
                  </td>
                  <td>
                    £1.5
                  </td>
                  <td>
                    X
                  </td>
                  <td>
                    <xsl:value-of select="PaxDetail/NoOfAdult+PaxDetail/NoOfChild+PaxDetail/NoOfInfant"/>
                  </td>
                  <td>
                    =
                  </td>
                  <td>
                    £<xsl:value-of select="format-number((PaxDetail/NoOfAdult+PaxDetail/NoOfChild+PaxDetail/NoOfInfant)*1.5,'#.00')"/>
                  </td>
                </tr>
                <tr>
                  <td height="10" colspan="6">
                  </td>
                </tr>
                <tr style="font-size: 12px; font-family: Arial, Helvetica, sans-serif; color: #000000;">
                  <td>
                    ATOL Protection Charges.
                  </td>
                  <td>
                    £2.5
                  </td>
                  <td>
                    X
                  </td>
                  <td>
                    <xsl:value-of select="PaxDetail/NoOfAdult+PaxDetail/NoOfChild+PaxDetail/NoOfInfant"/>
                  </td>
                  <td>
                    =
                  </td>
                  <td>
                    £<xsl:value-of select="format-number((PaxDetail/NoOfAdult+PaxDetail/NoOfChild+PaxDetail/NoOfInfant)*2.5,'#.00')"/>
                  </td>
                </tr>
                <tr>
                  <td height="10" colspan="6">
                    <hr />
                  </td>
                </tr>
              </xsl:if>
              <tr style="font-size: 13px; font-family: Arial, Helvetica, sans-serif; color: black;
                        font-weight: bold;">
                <td align="right" colspan="4" style="padding-right: 15px;">
                  <b>Total Cost</b>
                </td>
                <td>
                  =
                </td>
                <td>
                  <xsl:if test="Payment/AtolStatus='true'">
                    £<xsl:value-of select="format-number(FlightDetail/Itinerary/GrandTotal+((PaxDetail/NoOfAdult+PaxDetail/NoOfChild+PaxDetail/NoOfInfant)*4),'#.00')"/>
                  </xsl:if>
                  <xsl:if test="Payment/AtolStatus='false'">
                    £<xsl:value-of select="FlightDetail/Itinerary/GrandTotal"/>
                  </xsl:if>
                </td>
              </tr>
            </table>
          </fieldset>
        </td>
      </tr>

      <tr>
        <td style="font-size: 12px; color: #000000; padding: 5px 10px 5px 10px;">
          <br />
          <fieldset>
            <legend style="font-size: 13px; color: #000000; font-weight: bold;">Your Financial Protection</legend>
            <table cellpadding="2" cellspacing="2" width="100%">
              <tr>
                <td>
                  All monies paid by you for the (air holiday package/flights - as appropriate) shownare
                  ATOL protected by the Civil Aviation Authority. Our ATOL number is 3517. Formore
                  information see our booking terms and conditions.
                </td>
              </tr>
            </table>
          </fieldset>
        </td>
      </tr>
      <tr>
        <td style="font-size: 12px; color: #000000; padding: 5px 10px 5px 10px;">
          <br />
          <fieldset>
            <legend style="font-size: 13px; color: #000000; font-weight: bold;">Payment Type:</legend>
            <table cellpadding="2" cellspacing="2" width="100%">
              <tr style="font-size: 12px; color: #000000;">
                <td>
                  Card Holder Name:
                </td>
                <td>
                  <xsl:value-of select="Payment/CardHolderName"/>
                </td>
                <td>
                  Card Type:
                </td>
                <td>
                  <xsl:value-of select="Payment/CardType"/>
                </td>
              </tr>
              <tr style="font-size: 12px; color: #000000;">
                <td>
                  Credit Card No. :
                </td>
                <td>
                  <xsl:value-of select="Payment/CardNumber"/>
                </td>
                <td>
                  Card Charge:
                </td>
                <td>
                  <xsl:value-of select="concat('£ ',Payment/CardCharge)"/>
                </td>
              </tr>
              <tr>
                <td colspan="3" align="right" style="color: #FF0404; font-family: Verdana; font-size: 10pt;
                            font-weight: bold">
                  Total for Services =
                </td>
                <td style="color: #FF0404; font-family: Verdana; font-size: 10pt; font-weight: bold">
                  <xsl:if test="Payment/AtolStatus='true'">
                    £<xsl:value-of select="format-number(FlightDetail/Itinerary/GrandTotal+Payment/CardCharge+((PaxDetail/NoOfAdult+PaxDetail/NoOfChild+PaxDetail/NoOfInfant)*4),'#.00')"/>
                  </xsl:if>
                  <xsl:if test="Payment/AtolStatus='false'">
                    £<xsl:value-of select="format-number(FlightDetail/Itinerary/GrandTotal+Payment/CardCharge,'#.00')"/>
                  </xsl:if>
                </td>
              </tr>
            </table>
          </fieldset>
        </td>
      </tr>
     
    </table>
  </xsl:template>

  <xsl:template name="DateFormat">
    <xsl:param name ="Date"></xsl:param>
    <xsl:variable name ="day">
      <xsl:value-of select ="substring($Date,1,2)"/>
    </xsl:variable>
    <xsl:variable name ="Month">
      <xsl:call-template name ="MonthName">
        <xsl:with-param name ="Month" select ="substring($Date,4,2)"></xsl:with-param>
      </xsl:call-template>
    </xsl:variable>
    <xsl:value-of select ="concat($day,' ',$Month)"/>
  </xsl:template>
  <xsl:template name="DateFormatFull">
    <xsl:param name ="Date"></xsl:param>
    <xsl:variable name ="day">
      <xsl:value-of select ="substring($Date,1,2)"/>
    </xsl:variable>
    <xsl:variable name ="Month">
      <xsl:call-template name ="MonthName">
        <xsl:with-param name ="Month" select ="substring($Date,4,2)"></xsl:with-param>
      </xsl:call-template>
    </xsl:variable>
    <xsl:variable name ="Year">
      <xsl:value-of  select ="substring($Date,7,4)"></xsl:value-of>
    </xsl:variable>
    <xsl:value-of select ="concat($day,' ',$Month,' ',$Year)"/>
  </xsl:template>
  <xsl:template name ="MonthName">
    <xsl:param name ="Month"></xsl:param>
    <xsl:choose>
      <xsl:when test="$Month = '01'">Jan</xsl:when>
      <xsl:when test="$Month = '02'">Feb</xsl:when>
      <xsl:when test="$Month = '03'">Mar</xsl:when>
      <xsl:when test="$Month = '04'">Apr</xsl:when>
      <xsl:when test="$Month = '05'">May</xsl:when>
      <xsl:when test="$Month = '06'">Jun</xsl:when>
      <xsl:when test="$Month = '07'">Jul</xsl:when>
      <xsl:when test="$Month = '08'">Aug</xsl:when>
      <xsl:when test="$Month = '09'">Sep</xsl:when>
      <xsl:when test="$Month = '10'">Oct</xsl:when>
      <xsl:when test="$Month = '11'">Nov</xsl:when>
      <xsl:when test="$Month = '12'">Dec</xsl:when>
    </xsl:choose>
  </xsl:template>
</xsl:stylesheet>
