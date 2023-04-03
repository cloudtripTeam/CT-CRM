using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiQPdf;
using mailServices;
using NPOI.SS.Formula.Functions;

public partial class Admin_FlightQuote : System.Web.UI.Page
{
    FandHServices.FandHServicesClient obj = new FandHServices.FandHServicesClient();
    DataTable dtPax = new DataTable();
    DataTable dtSector = new DataTable();
    DataTable dtComp = new DataTable();
    DataTable dtContact = new DataTable();
    DataTable dtPrice = new DataTable();
    DataTable dtTrans = new DataTable();
    DataTable dtBD = new DataTable();
    public string XPDetails { get; set; }
    public string CovidURL { get; set; }
    public string terms { get; set; }
    MailMessage msg = new MailMessage();
    Layout lo = new Layout();
    UserDetail objUserDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        objUserDetail = Session["UserDetails"] as UserDetail;
        if (Session["UserDetails"] != null)
        {
            if (!IsPostBack)
            {
               
                //if (!objUserDetail.isAuth("FlightQuote"))
                //{
                //    Response.Redirect("Default.aspx");
                //    return;
                //}

                if (!string.IsNullOrEmpty(Request.QueryString.Get("BID")))
                {

                    string xp = Request.QueryString.Get("BID");
                    txtInvoice.Text = xp;
                    XPDetails = BookingDetails(xp);

                }
                //ddlBrand.Items.Clear();
                //if (objUserDetail.userRole.ToLower() == "agentft" || objUserDetail.userRole.ToLower() == "team head ft")
                //{
                //    ddlBrand.Items.Add(new ListItem("Flight Trotters", "FLTTROTT"));

                //}
                //else
                //{
                //    ddlBrand.Items.Add(new ListItem("Dial4travel", "DIAL4TRV"));
                //    ddlBrand.Items.Add(new ListItem("Travel Junction", "TRVJUNCTION"));
                //    ddlBrand.Items.Add(new ListItem("Flight XpertUk", "FLTXPT"));
                //    ddlBrand.Items.Add(new ListItem("Other", "OTHER"));


                //}
            }
        }
        else
        {
            //Response.Redirect("~/Login.aspx", false);
        }
    }
 
    private string termcondition()
    {
       // terms += "<p><a href= '" + CovidURL+ ">"+ CovidURL + "</a></p>";
        terms += @"<p>&nbsp;</p><p><a href= 'https://www.gov.uk/guidance/coronavirus-covid-19-declaration-form-for-international-travel#travel-declaration-form'>https://www.gov.uk/guidance/coronavirus-covid-19-declaration-form-for-international-travel#travel-declaration-form</a></p>
<p><strong><strong><u>Brief Terms &amp; Conditions-</u></strong></strong></p>
<ul>
<li>The fare is subjected to the availability &amp; it cannot be guaranteed until the tickets are issued.</li>
<li>All the tickets are non-changeable &amp; non-refundable unless specified. Please contact your travel agent to get the detailed description about your ticket conditions.</li>
<li>The local authorities in certain countries may impose additional taxes (tourist tax, etc.), which generally have to be paid locally. The Customer is exclusively responsible for paying such additional taxes. The amount of taxes can change between booking and stay dates. In the event that taxes have increased as at your stay date, you will be liable to pay taxes at the new higher rate.</li>
<li>It is your responsibility to ensure that your&nbsp;travel documentation&nbsp;is in order. Please note that if you are travelling on a&nbsp;one-way ticket, most countries will not allow you to enter without&nbsp;relevant visas&nbsp;or documentation. If you have a connecting flight&nbsp;via a third country, you may also require a transit visa. For further information, please contact the consulate of the country you are travelling through. For the most up to date information on visas, passports, health and&nbsp;travel advice&nbsp;worldwide, please see the following web sites for UK Visas, Home Office and Foreign and Commonwealth Office.&nbsp;</li>
</ul>
<p><strong><strong>The official British Government website for visa service,&nbsp;<a href= 'https://www.gov.uk/apply-to-come-to-the-uk'><u>https://www.gov.uk/apply-to-come-to-the-uk</u></a></strong></strong></p>
<p>Travel advice by country for Travel and living abroad,&nbsp;<a href= 'http://www.fco.gov.uk/en/travel-and-living-abroad/travel-advice-by-country/'><u>http://www.fco.gov.uk/en/travel-and-living-abroad/travel-advice-by-country/</u></a>&nbsp;or call on 0207 008 0232/0233</p>
<p>&nbsp;</p>
<table width= '100% '>
<tbody>
<tr>
<td>
<p><strong><strong>We advise you to&nbsp;reconfirm your flight 72 Hours prior to departure</strong></strong>&nbsp;</p>
</td>
</tr>
</tbody>
</table>
<p>According to European law, we strongly recommend you to buy insurance before travelling.</p>
<p>Kindly&nbsp;reconfirm&nbsp;all the terms and conditions regarding your reservation before confirming with our&nbsp;travel consultant.</p>
<p>The tickets will be issued as electronically. Please get your VISA if required before travelling.&nbsp;</p>
<table>
<tbody>
<tr>
<td>
<p>An&nbsp;electronic ticket&nbsp;(e-ticket) is a paperless ticketing method. Because your electronic ticket is held in the airlines computer, you cannot forget it or lose it. More importantly, your electronic ticket cannot be stolen, saving you the cost of a replacement ticket. When you arrive at the airline check in desk, you will be required to present the following to receive your boarding pass, an official form of identification i.e. your passport, a print out of your confirmation email to show to the airline.&nbsp;</p>
</td>
</tr>
</tbody>
</table>
<p><strong><strong>Please note that you must have a valid Passport with a minimum of 6 months on it or you will not be able to travel. Your passport must also be in excellent condition - the presentation of damaged passports may mean you are unable to travel. It is mandatory to carry a machine readable passport or valid visa for travel to USA; otherwise you will be denied boarding.</strong></strong><br /><br /><strong><strong><u>Check in Times</u>:&nbsp;</strong></strong>Due to security measures we currently recommend 3 hours for intercontinental flights and 2 hours for European and Domestic flights.</p>
<p>Check-in will normally close 1 Hour before scheduled departure so please allow sufficient time to get to the airport in time.</p>
<p>Seats where possible, we will send your seat request to the airline, please note that not all airlines will pre-assign seats and actual seat allocation is entirely at the airlines discretion and an early check-in will help you the most in getting the seat you want. Exit row and extra leg room seats cannot normally be requested in advance and are usually only assigned at check-in.</p>
<p>Meal Requests if you haven&rsquo;t already done so please let us know and we will send the forward the request to the airline but remember meal requests are not guaranteed but the airline will make every effort to meet your request.&nbsp;&nbsp;</p>
<p><strong><strong><em>BOOKING CONDITIONS</em></strong></strong></p>
<p>&nbsp;</p>
<p><strong><strong><u>CONDITIONS A&nbsp;</u></strong></strong></p>
<ol>
<li><u> RESERVING YOUR FLIGHT OR HOLIDAY</u></li>
</ol>
<p>On receipt of your request and deposit we will confirm your booking and from that point cancellation charges will apply and we&rsquo;ll send you a confirmation with details of your arrangements.</p>
<p>Please note that a telephone booking confirmation is a firm booking as if it were made or confirmed in writing.</p>
<ol start= '2 '>
<li><u> PRICE GUARANTEE</u></li>
</ol>
<p>CHARTER FLIGHT ARRANGEMENTS: - The price shown on this confirmation invoice will not be subject to any surcharges.</p>
<p>SCHEDULED FLIGHT ARRANGEMENTS: - As scheduled airlines reserve the right to increase prices at any time the price shown on this confirmation invoice will ONLY be guaranteed once full payment is received. The payment of a deposit guarantees your seat, not the price.</p>
<p>GOVERNMENT ACTION: - Our Price Guarantee cannot cover increases due to direct Government action. E.g. the imposition of VAT or Passenger Levy.</p>
<ol start= '3 '>
<li><u> MINOR CHANGES TO YOUR HOLIDAY</u></li>
</ol>
<p>If we are obliged to make any minor change in the arrangements for your holiday, we will inform you as soon as possible.</p>
<ol start= '4 '>
<li><u> MAJOR CHANGES TO YOUR HOLIDAY</u></li>
</ol>
<p>If we are obliged to make any major changes to your holiday arrangements prior to the departure e.g. change of departure time of more than 12 hours, change of airport (but excluding changes between airports in the London region, aircraft type or airline) it will only be because we are forced to do so by circumstances usually beyond our control. In such an unlikely event we will inform you immediately and our objective will be to minimize your inconvenience. We will wherever possible offer you alternative arrangements as close as possible to your original choice.</p>
<p>You will then have a choice of accepting, taking another available holiday of similar price or canceling. Should you choose to cancel you will be reimbursed all monies paid to us.</p>
<ol start= '5 '>
<li><u> GROUP HOLIDAYS</u></li>
</ol>
<p>Some of our holidays are based on minimum number of participants and in the unlikely event that these numbers are not reached we reserve the right to cancel the tour and refund all payments made, Prices are subject to increase if the group size is reduced.</p>
<ol start= '6 '>
<li><u> FLIGHTS</u></li>
</ol>
<p>Details of airlines, flight numbers/schedules and destination airport will be shown on your invoice/confirmation. Please note that a flight described as quote &lsquo;direct&rsquo; will not necessarily be non-stop. Flight schedules may change at any time and we will advise you of any changes prior to departure. However, we strongly suggest you to reconfirm your reservation on any flights during your journey at least 72 hours prior to departure. We cannot accept any responsibility for delays or missed flights.&nbsp;</p>
<ol start= '7'>
<li><u> INSURANCE</u></li>
</ol>
<p>The Company strongly recommends that the Client takes out adequate insurance. The Client is herewith recommended to read the terms of any insurance to satisfy them as to the fitness of cover. The Company will be pleased to quote you for insurance.</p>
<ol start= '8'>
<li><u> MAKING A BOOKING</u></li>
</ol>
<p>The person, who makes the booking, must be aged 18 years or over, accepts these conditions on behalf of all members of the party and is responsible for all payments due from the party.</p>
<ol start= '9'>
<li><u> DEPOSIT</u></li>
</ol>
<p>No booking will be confirmed unless the required deposit has been received by The Company. Deposits do not guarantee price only confirmation of seats. Deposits are non-refundable.</p>
<ol start= '10'>
<li><u> CHANGING YOUR ARRANGEMENTS</u></li>
</ol>
<p>If you wish to change any item - other than increasing the number of persons in your party - and providing we can accommodate the change, you will have to confirm the change in writing and pay amendment fee of &pound;35 per booking + the airline/supplier charges depending upon the terms &amp; conditions of your ticket. From time to time we are required to collect additional taxes. You will be informed of any additional taxes prior to ticket issuance. Once the ticket is issued, most airlines do not allow changes.</p>
<ol start= '11'>
<li><u> CANCELLATION BEFORE TICKET ISSUE</u></li>
</ol>
<p>Should you or any member of your party be forced to cancel your flight or holiday, we must be notified, in writing, by the person who made the booking and who is therefore, responsible for the payment of the cancellation charges.</p>
<p>CANCELLATION AFTER TICKET ISSUE: Cancellation will result in loss of 100% of total cost of all travel arrangements in most cases. Please consult your travel consultant. Charter flights carry a 100% cancellation fee both before and after ticket issuance.</p>
<ol start= '12'>
<li><u> NAME CHANGE/CORRECTION</u></li>
</ol>
<p>In any case, the complete name change is not possible which means we cannot transfer the ticket to someone else. However, name correction can be done depending upon the supplier/airline&rsquo;s conditions.</p>
<ol start='13'>
<li><u> LEGAL JURISDICTION</u></li>
</ol>
<p>We accept the jurisdiction of the Courts in part of the UK in which the client is domiciled. For clients not domiciled in the UK the Courts of England shall have sole jurisdiction.</p>
<p><strong><strong><u>CONDITIONS B</u></strong></strong></p>
<p>&nbsp;</p>
<p>Please read the following terms and conditions carefully as they apply to all bookings made. No variations shall be valid unless agreed and confirmed in writing by a Director of The Company. A verbal variation will not be valid.</p>
<p>The Company act as agents only in transactions relating to flights, car hire, accommodation, package holidays etc. and book those facilities for you (the client) on behalf of the Supplier or Operator (the Principal). The Company are not the Principal and do not act as the Principal nor shall they be construed as being such&nbsp;<em>by&nbsp;</em>inference or otherwise. This confirmation does not constitute a contract. Your contract is with the Principal named overleaf. The Company is not liable for the Principal&rsquo;s actions, failures or omissions.</p>
<p>No booking will be confirmed unless the required deposit has been received by The Company. Principals reserve the right to increase prices up to the date on which they receive the balance. Payment of a deposit guarantees your seat, not the price.</p>
<p>Bookings made will be immediately subject to the Principal's terms and conditions and The Company have no authority to vary them in the Client's favor.</p>
<p>All amendments/cancellations will incur charges.</p>
<p>Please note that a telephone booking confirmation is as firmly confirmed as if it were made/confirmed in writing at that time.</p>
<p>The Company will attempt to fulfill Client&rsquo;s requirements to the best of its ability and in the event of a complaint, will pass such complaints to the Principal concerned on the Client&rsquo;s behalf, as agent only. The Company will not be able to commit the Principal as to their correct course of actions.</p>
<p>The Company strongly recommends that the Client takes out adequate insurance whether or not it is a Principal's condition of booking. The Client is herewith recommended to read the terms of any insurance to satisfy them as to the fitness of cover. The Company will be pleased to quote you for insurance. Should insurance be declined you will be asked to sign our indemnity form.</p>
<p><strong><strong><u>CONDITIONS APPLYING TO A AND B</u></strong></strong></p>
<p><em>Please remember that the person making the booking accepts ALL the booking Conditions and is liable for any amendment fees, payments or cancellation charges that arise on behalf of ALL the passengers in their party. In addition they are also responsible for checking this and all future documentation and for advising us immediately if anything is missing&nbsp;</em>or&nbsp;<em>incorrect.&nbsp;</em>The details overleaf are given in good faith based on information from the Principal at the time of booking. Should it transpire that any of these details differ you will be advised immediately.</p>
<p><strong><strong><u>PAYMENT</u></strong></strong></p>
<p>You must pay the balance by the due date shown on the confirmation. Please note that for some telephone bookings full payment may be required IMMEDIATELY i.e. before you receive confirmation. If this applies, you will be advised when the booking is made. It is very important that you pay balance when due as failure to do so may lead to the cancellation of your holiday and still leave you liable to the cancellation charges. Where an extra  'booking charge ' is applicable, you will be advised at the time of booking. Credit card payments are subject to a minimum of 3% extra charge. Payment by Debit cards are accepted without any additional surcharges. However, a late payment fee of &pound;20 will be applied to your balance where cancellation can be avoided by the Principal.</p>
<p><strong><strong><u>PASSPORTS, VISA AND HEALTH REQUIREMENTS</u></strong></strong>&nbsp;&ndash; Though we are happy to provide general information about the passport and visa requirements related to your trip, it is entirely your responsibility to get them confirmed from the relevant Embassies and/or Consulates. Please take special note that for all air travel within the British Isles, airlines require photographic identification of a specific type.</p>
<p><em>Passport and Visa:&nbsp;</em>Passports normally need to be valid for at least 6 months beyond the period of your stay. If you are traveling to the USA you must have a machine readable passport. You must insure that your passport and visas are valid for all destinations on your journey, this includes transiting airports. Please consult the relevant Embassy or Consulate for this information. We regret that neither we nor the principal(s) or supplier(s) accept any responsibility if you are denied to board any flight or to enter any country due to failure on your part to carry the correct passport, visa or other documents required by any airline, authority or country.</p>
<p><em>Health:&nbsp;</em>Recommended inoculations for travel may change at any time. It is your responsibility to ensure that you obtain all recommended inoculations, take all recommended medication and follow all medical advice in relation to your trip.</p>
<p><strong><strong><u>SPECIAL REQUESTS AND MEDICAL PROBLEMS</u></strong></strong></p>
<p>If you have any special requests, please advise us at time of booking. Although we will endeavor topass any such request on to the relevant supplier, we regret we cannot guarantee any request will be met. Infant meals must be requested by the client at the time of booking. Failure to meet any special request will not be a breach of contract on our part. If you have any medical problem or disability which may affect your booked arrangements, you must advise us in writing at the time of booking with full details. If we are unable to properly accommodate your particular need, we reserve the right to decline/cancel your booking.</p>
<p><strong><strong><u>FORCE MAJEURE</u></strong></strong></p>
<p>We accept no responsibility for and shall not be liable in respect of any loss or damage or alterations, delays or changes arising from unusual and unforeseen circumstances beyond our control, such as war or threat of war, riot, civil strife, industrial dispute including air traffic control disputes, terrorist activity, natural and nuclear disaster, fire or adverse weather conditions, technical problems with transport, closure or congestion of airports or ports, cancellations of schedules by scheduled airlines. You can check the current position on any country by contacting the Foreign and Commonwealth Office.</p>
<table width='100%'>
<tbody>
<tr>
<td>
<p><strong><strong><u>CANCELLATIONS / AMENDMENTS BY THE TRAVEL SUPPLIER</u></strong></strong></p>
</td>
</tr>
<tr>
<td>
<p>Airlines reserve the right to make time changes, or in rare cases, to cancel flights, for operational reasons. Whilst we are not responsible for, and has no control over, such changes, we will do our best to assist when such situations arise.</p>
<p><br />In the unlikely event that your flight is cancelled by the airline or tour operator, your rights and remedies will be governed by the supplier's conditions/ airline's conditions of carriage. As a result you may be entitled to:</p>
<p><br />(a) carriage on another flight with the same airline;<br />(b) re-routing to your destination with another carrier;<br />(c) <br /> some other right or remedy (like credit note or vouchers etc.).</p>
<p><br />In the event of schedule changes made prior to commencement of your journey, it is not always necessary to have your tickets reissued or revalidated, but we will advise you should this be necessary. we take no responsibility for any flight rescheduling en route.</p>
</td>
</tr>
</tbody>
</table>
<p><strong><strong><u>RECONFIRMING RETURN/ONWARD FLIGHTS</u></strong></strong></p>
<p>It is your responsibility to ensure that you follow ALL RECONFIRMATION INSTRUCTIONS which will be shown EITHER on the FRONT of this invoice or on your travel documents. However, we strongly recommend you to reconfirm all the flights 72hrs prior to the departure. Should there be any changes to the schedule, the company will not be liable for any additional costs due to your failure to reconfirm your flights.</p>
<p><strong><strong><u>DOCUMENT DISPATCH</u></strong></strong></p>
<p>Most airline tickets are now &lsquo;e-tickets&rsquo; and as such will normally be e-mailed, or a copy posted, within 48hrs of receipt of full payment. &lsquo;E-tickets&rsquo; can also be collected at the departure airport. All other documentation, payment receipts, hotel vouchers, transfers etc. will be sent at the same time either by e-mail or to the address of the person paying for tickets. It is the customer&rsquo;s responsibility for any documents lost in the post; therefore we strongly suggest that all documents are sent by recorded delivery. Prices can be obtained while booking.</p>
<p>Have a great trip, if you need any other travel arrangements. Let us know as it&rsquo;s our pleasure to assist you.</p>";
        return terms;
    }
    private string termconditionUSANEW()
    {
        terms = @"<p>Introduction</p>
<p>There are some terms and conditions that are especially designed for the users of company. The company requests its users to go through each and every terms and conditions before utilizing its services. If a user utilizes the services then, it is considered as the person consent to the Terms of Use. On the other hand, if user doesn&rsquo;t want to give consent over the terms and conditions mentioned over the website, then don&rsquo;t utilize the website.</p>
<h3>Price</h3>
<p>All prices displayed on our website are subject to change at any time without prior notice. Airfare is only guaranteed once the purchase has been completed and the tickets have been issued. Airlines and other travel suppliers may change their prices without notice.<br /> If a price increase occurs after you have made a reservation that affects your travel package, we will notify you of the price increase before taking any further steps. However, no price increases will affect your travel package once your reservation has been finalized.<br /> All reservations are non-refundable unless otherwise stated. If you find that you must cancel a reservation for any reason, please contact us. We will do all we can to assist you in this process. However, please be aware that even if your cancellation is allowed and your reservation is thus refundable, it may be subject to an administrative cancellation fee of USD 200.00 per passenger for international flights, USD 300.00 for trans-border flights between USA and the Canada and USD 150.00 for domestic flights.</p>
<h3>For US bookings, even if your ticket is nonrefundable:</h3>
<ul>
<li>Within same day midnight you may cancel your booking, &ldquo;subject to our cancellation fees&rdquo;.<br /> &bull; All Airline Basic Economy tickets and Promotion tickets cancellation are not permitted Non-refundable tickets<br /> &bull; All reservations are also non-changeable and non-transferable unless otherwise stated. If you need to make a change to your reservation and that change is allowed, please be aware that such change is subject to a fee of $150.00 per passenger for domestic flights, $200.00 for trans-border flights and $300.00 for all other flights. There may also be fees or differences in price charged by any third-party suppliers (e.g., airlines, hotels, cruise lines, etc.) included in your reservation.<br /> <br /> Please be aware that once you have made a reservation, name changes are not allowed. If you find you need to change or correct the spelling of a name after you&rsquo;ve made a reservation, you will have to cancel your original reservation&mdash;if allowed&mdash;and then make a new reservation with a new flight at the then-current rate using the correct spelling of the name. This will likely incur fees and penalties. Therefore, it is imperative&mdash;and your responsibility&mdash;to verify the spelling of the names of all passengers before making your reservation.<br /> The rate applied on the date of issuance of the ticket is only valid for a ticket fully utilized and in the sequential order of flight segments on the dates indicated. Improper use may void the ticket and result in cancellation of the entire trip.<br /> Pricing is displayed in US currency.</li>
</ul>
<h3>PAYMENT AND FLIGHT INFORMATION AND CONFIRMATION</h3>
<p>Some banks and credit card companies charge a fee for international transactions. They will appear on your credit or bank card statement as a foreign or international transaction fee. For example, if you make a travel reservation through our website from outside the United States using a U.S. credit card, your bank may convert the payment amount to your local currency and may charge you a fee for the conversion. The amount of the charge appearing on your credit or bank card statement may be in your local currency and different than the purchase amount shown on the billing summary page for the reservation.</p>
<p>In addition, a foreign transaction fee may be assessed if the bank that issued your credit card is located outside the United States.</p>
<p>Booking international travel through our website may be considered an international transaction by the bank or credit card company since company may pass your payment on to an international travel supplier.</p>
<p>Your bank or credit card company determines the currency exchange rate and the amount of the foreign transaction fee on the day it processes the transaction. Please contact your bank or credit card company should you have any questions about these fees or the exchange rate applied to your transaction.</p>
<p>Booking notification: Once your purchase is complete, you should receive an email titled &ldquo;Booking Notification.&rdquo; Your booking may provide you with a confirmation number before a ticket has been issued. If this is the case, the booking process is not complete and the fare is subject to change until a ticket is issued.</p>
<p>Once your ticket has been issued, you should receive your electronic ticket.</p>
<p>We strongly recommend that you re-confirm your flight reservation with the airline 24 hours prior to departure for domestic flights, and 72 hours prior to departure for international flights.</p>
<h3>Booking Amendments and Charges</h3>
<p>Booking made on us may have some charges that are being charged as per the directive of airline. The charges may vary by flight and booking class. The amendment fee may also vary by airline to airline, flight and booking class. All changes must be made at least 72 hours prior to the departure date. Time duration can be different from airline to airline, therefore it is advised to cross check it with customer support for accurate information.</p>
<h3>Cancel &amp; Exchange</h3>
<p>Most of the aircraft tickets are non-refundable. In some situations where the carrier allows cancellation, then a credit might be considerable against future ticket by same traveler on the same airlines. The airlines have their own policy of credit termination date, which can&rsquo;t be utilized after its expiry. So, in case of cancellation, we request customers to examine the limitations with customer service specialist. Cancellation of tickets must be done before flight takes off else, we don&rsquo;t assure or promise for any cancellation. If you are already prepare to make a new reservation and wish to utilize the airline credit, then you will be required to bear the difference in the fare if applicable. Such kind of strategies and policies are made by the management of carriers, which are not in our control.</p>
<p>We can accept refund requests only if the following conditions have been met:</p>
<ul>
<li>You have applied for a cancellation and refund with us and if the fare rules provide for cancellation and refunds;</li>
<li>You are not a 'no show' (most 'no show' bookings are in-eligible for any waiver from suppliers for refund processing); and</li>
<li>We are able to secure waivers from suppliers to process this requested cancellation and refund.</li>
</ul>
<p>We are unable to provide a specific time line for how long it may take for this requested refund to be processed. All refund requests are processed in a sequential format. Once you have provided our customer service agent with your cancellation request, we will then send you an email notification that your request has been received. This notification does not automatically qualify you for a refund. This only provides you with an acknowledgement of your request and provides you with a tracking number. Upon receipt of your request we will work with the suppliers such as airlines, hotels, car-rental companies to generate a waiver based on airline and other supplier rules and notify you of the supplier decision. Our services fees associated with the original travel reservation or booking are not refundable. Please note that we are dependent on the suppliers for receiving the requested refunds. Once the refund has been approved by the supplier it may take additional time for this to appear on your credit card statement. Generally, all suppliers will charge a penalty for refund. This entire process may take 60-90 days from receipt of your request to receiving credit on your statement. Apart from the airlines and other suppliers refund penalties, Company will charge a post-ticketing services fee, as applicable. All refund fees are charged on per-passenger, per-ticket basis. These fees will only be assessed if a refund has been authorized by the supplier or a waiver has been received and when the airline/supplier rules permit such refunds. If such refund is not processed by the supplier, we will refund you our post-ticketing service fees applicable to your agent assisted refund request , but not our booking fees for the original travel reservation or booking.</p>
<h3>The company accept request under some of the guidelines such as follows:</h3>
<ul>
<li>If the ticketing fare rules will allow the cancellation on a particular booking, then only request will be accepted.</li>
<li>You should not be a NO SHOW passenger. In case, you are a NO SHOW and you are not allowed to board the airline, then you are not eligible for refund.</li>
<li>If refund is processed, it maximum takes up to 21 working days.</li>
</ul>
<h3>Payment Policy</h3>
<ul>
<li>We accept credit cards and debit cards of major countries including US, Canada etc.</li>
<li>All costs appear in U.S. dollars.</li>
<li>We may accept payment into two separate transactions which includes Airline Base Fare and Taxes. However, aggregate sum will be same as advised to people.</li>
<li>Once your payment gets through, the ticket purchased is guarantee. In case we didn&rsquo;t get payment, we will notify you within 24 hours.</li>
<li>If the credit card details are not approved by our verification department, we won&rsquo;t process any booking.</li>
</ul>
<h3>VISA AND ENTRY REQUIREMENTS</h3>
<p>All customers are advised to verify travel documents (transit visa/entry visa) for the country through which they are transiting and/or entering. Reliable information regarding international travel can be found at govt. website and also with the consulate/embassy of the country(s) you are visiting or transiting through. We will not be responsible if proper travel documents are not available and you are denied entry or transit into a Country.<br /> Your transaction with us does not guarantee entrance to the country of destination. Traveler understands that we accepts no responsibility for determining passenger's eligibility to enter or transit through any specific country. Information, if any, given by company's employees must be verified with government authorities. Such information does not imply responsibility company behalf.</p>
<h3>ETA FOR CANADA</h3>
<p>Travelers who fly to or transit through Canada may need an Electronic Travel Authorization (ETA), this is an automated system that allows Canadian authorities to screen passengers before their arrival in Canada and determine the eligibility of visitors to enter Canada and whether such travel involves any security risk.</p>
<h3>CREDIT CARD DECLINES</h3>
<p>At the time of processing the transaction, user's credit card declines for various reasons. We endeavour to notify you by e-mail within 72 hours. Please note in any case if your credit card has been declined, the transaction will not be processed. We do not guarantee for the fare change and any other booking details. If there is a fare change from the TSP, we provide user(s) with the alternative options either accept or cancel the booking.</p>
<p>&nbsp;</p>";
        return terms;
    }
    private string termconditionCA()
    {
        terms = @"<div>
        <p>
            <strong>
                <strong>
                    <u>Brief Terms &amp; Conditions-</u>
                </strong>
            </strong>
        </p>
        <ul>
            <li>The fare is subjected to the availability &amp; it cannot be guaranteed until the tickets are issued.</li>
            <li>All the tickets are non-changeable &amp; non-refundable unless specified. Please contact your travel agent to get the detailed description about your ticket conditions.</li>
            <li>The local authorities in certain countries may impose additional taxes (tourist tax, etc.), which generally have to be paid locally. The Customer is exclusively responsible for paying such additional taxes. The amount of taxes can change between booking and stay dates. In the event that taxes have increased as at your stay date, you will be liable to pay taxes at the new higher rate.</li>
            <span>
                <li>It is your responsibility to ensure that your&nbsp;travel documentation&nbsp;is in order. Please note that if you are travelling on a&nbsp;one-way ticket, most countries will not allow you to enter without&nbsp;relevant visas&nbsp;or documentation. If you have a connecting flight&nbsp;via a third country, you may also require a transit visa. For further information, please contact the consulate of the country you are travelling through. For the most up to date information on visas,contact your local embassy of the country you are planning to travel to.</li>
            </span>
        </ul>
    </div>
    <div>
        <table width='100%'>
            <tbody>
                <tr>
                    <td>
                        <p>
                            <strong>
                                <strong>We advise you to&nbsp;reconfirm your flight 72 Hours prior to departure</strong>
                            </strong>&nbsp;

                        </p>
                        <p>The tickets will be issued as electronically. Please get your VISA if required before travelling.&nbsp;</p>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <p>An&nbsp;electronic ticket&nbsp;(e-ticket) is a paperless ticketing method. Because your electronic ticket is held in the airlines computer, you cannot forget it or lose it. More importantly, your electronic ticket cannot be stolen, saving you the cost of a replacement ticket. When you arrive at the airline check in desk, you will be required to present the following to receive your boarding pass, an official form of identification i.e. your passport, a print out of your confirmation email to show to the airline.&nbsp;</p>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <p>
                            <strong>
                                <strong>Please note that you must have a valid Passport with a minimum of 6 months on it or you will not be able to travel. Your passport must also be in excellent condition - the presentation of damaged passports may mean you are unable to travel. It is mandatory to carry a machine readable passport or valid visa for travel to USA; otherwise you will be denied boarding.</strong>
                            </strong>
                            <br>
                            <br>
                            <strong>
                                <strong>
                                    <u>Check in Times</u>:&nbsp;

                                </strong>
                            </strong>Due to security measures we currently recommend 3 hours for intercontinental flights and 2 hours for European and Domestic flights.

                        </p>
                        <p>Check-in will normally close 1 Hour before scheduled departure so please allow sufficient time to get to the airport in time.</p>
                        <p>Seats where possible, we will send your seat request to the airline, please note that not all airlines will pre-assign seats and actual seat allocation is entirely at the airlines discretion and an early check-in will help you the most in getting the seat you want. Exit row and extra leg room seats cannot normally be requested in advance and are usually only assigned at check-in.</p>
                        <p>Meal Requests if you haven’t already done so please let us know and we will send the forward the request to the airline but remember meal requests are not guaranteed but the airline will make every effort to meet your request.</p>
                        <br>
                        <p>This website was designed to be as user-friendly, informative and secure as possible. Please read these terms and conditions to learn more about the website as well as our responsibilities and yours in using it. If you are not willing to agree to these provisions, please do not use the website.</p>
                        <p>This website is intended to provide services primarily for residents of North America and Canada.</p>
                        <p>
                            ******************************

                            <wbr>******************************

                            <wbr>******************************

                            <wbr>******************************

                            <wbr>******************************

                            <wbr>******************************

                            <wbr>**************************

                        </p>
                        <p>
                            <span>
                                <b>PRICES , Refund and Cancellation Policies</b>
                            </span>
                            <br>
                        </p>
                        <p>
                            All prices displayed on our website are subject to change at any time without prior notice. Airfare is only guaranteed once the purchase has been completed and the tickets have been issued. Airlines and other travel suppliers may change their prices without notice.

                            <br />
                            <br />If a price increase occurs after you have made a reservation that affects your travel package, we will notify you of the price increase before taking any further steps. However, no price increases will affect your travel package once your reservation has been finalized.

                            <br />
                            <br />All reservations are non-refundable unless otherwise stated. If you find that you must cancel a reservation for any reason, please contact us. We will do all we can to assist you in this process. However, please be aware that even if your cancellation is allowed and your reservation is thus refundable, it may be subject to an administrative cancellation fee of CAD150.00 per passenger for international flights, CAD125 for trans-border flights between Canada and the USA and CAD 75 for domestic flights.

                            <br />
                            <br>For U.S. and Canada bookings, even if your ticket is nonrefundable:

                        </p>
                        <ul>
                            <li>Within same day midnight you may cancel your booking and receive a full refund, subject to our cancellation fees.</li>
                            <li>All reservations are also non-changeable and non-transferable unless otherwise stated. If you need to make a change to your reservation and that change is allowed, please be aware that such change is subject to a fee of $75 per passenger for domestic flights, $125.00 for trans-border flights and $150.00 for all other flights. There may also be fees or differences in price charged by any third-party suppliers (e.g., airlines, hotels, cruise lines, etc.) included in your reservation.</li>
                        </ul>
                        <p>
                           
                            <span>Please be aware that once you have made a reservation, name changes are not allowed. If you find you need to change or correct the spelling of a name after you’ve made a reservation, you will have to cancel your original reservation—if allowed—and then make a new reservation with a new flight at the then-current rate using the correct spelling of the name. This will likely incur fees and penalties. Therefore, it is imperative—and your responsibility—to verify the spelling of the names of all passengers before making your reservation.</span>
                            <br />
                            <br />
                            <span>The rate applied on the date of issuance of the ticket is only valid for a ticket fully utilized and in the sequential order of flight segments on the dates indicated. Improper use may void the ticket and result in cancellation of the entire trip.</span>
                            <br />
                            <br />
                            <span>Pricing is displayed in US and/or Canadian currency.</span>
                        </p>
                       
                        <h3>
                            FORCE MAJEURE
                        </h3>
                        <p>
                            <span></span>
                        </p>
                        <p,Helvetica,Arial,sans-serif;outline:0px'>
                            We accept no responsibility for and shall not be liable in respect of any loss or damage or alterations, delays or changes arising from unusual and unforeseen circumstances beyond our control, such as war or threat of war, riot, civil strife, industrial dispute including air traffic control disputes, terrorist activity, natural and nuclear disaster, fire or adverse weather conditions, technical problems with transport, closure or congestion of airports or ports, cancellations of schedules by scheduled airlines. You can check the current position on any country by contacting the Foreign and Commonwealth Office.
                            </p>
                            <p>
                                <br>
                            </p>
                            <h3>
                                CANCELLATIONS / AMENDMENTS BY THE TRAVEL SUPPLIER
                            </h3>
                            <p>
                                Airlines reserve the right to make time changes, or in rare cases, to cancel flights, for operational reasons. Whilst we are&nbsp;not responsible for, and has no control over, such changes, we will do our best to assist when such situations arise.

                                <br />
                                <br />In the unlikely event that your flight is cancelled by the airline or tour operator, your rights and remedies will be governed by the supplier's conditions/ airline's conditions of carriage. As a result you may be entitled to:

                                <br />
                                <br />(a) carriage on another flight with the same airline without additional costs;

                                <br />(b) re-routing to your destination with another carrier without additional costs;

                                <br />(d) some other right or remedy, for example a future credit note, voucher, etc

                                <br />
                                <br>In the event of schedule changes made prior to commencement of your journey, it is not always necessary to have your tickets reissued or revalidated, but we will advise you should this be necessary. we&nbsp;take no responsibility for any flight rescheduling en route.

                            </p>
                            <h3>
                            </h3>
                            <p>
                                ******************************

                                <wbr>******************************

                                <wbr>******************************

                                <wbr>******************************

                                <wbr>******************************

                                <wbr>******************************

                                <wbr>******************************

                                <wbr>*

                            </p>
                            <h3>
                                TRAVEL INFORMATION
                            </h3>
                            <p>
                                Our website provides extensive information related to travel for you, our customer. It contains information about vacation destinations, tour packages and travel providers as well as airfares, flight schedules and cruise details. It also contains information for travelers about insurance and foreign currencies.

                                <br />
                                <br />We receive this information from third-party sources such as airlines, hotels, tour operators and transportation providers. We always take reasonable care to make sure this information is accurate and up-to-date. However, we cannot guarantee the accuracy of this information or that it is the most current information available.

                                <br />
                                <br />As a traveler, you must know and understand the applicable legal requirements related to travel, including passport, visa and health requirements. We will assist you in this regard, both through our website and with live support. However, the ultimate responsibility for obtaining this information and complying with any and all passport, visa, health or other requirements remains solely and exclusively with you.

                                <br />
                                <br />We strive to provide you with the most current information available concerning tour packages, flight schedules, travel destinations and prices on our website. However, please understand that all the information on our website is subject to change without prior notice. Also, travel products, packages and services described on our website are subject to availability.

                                <br />
                                <br />
                                <span>Baggage Policy:</span>&nbsp;Each airline has its own policies regarding baggage allowances, fees and restrictions. These policies differ from airline to airline and can change at any time. We try our best to display current baggage fee information on this website, but we cannot guarantee the accuracy of this information. Ultimately, you are responsible for verifying your airline’s baggage policies and fees before your departure. Also, please be aware that baggage fees are not included in the cost of your trip.

                                <br />
                                <br />Schedule change: Changes to flight schedules, including flight cancellations, can occur for any number of reasons, including bad weather, mechanical problems, crew issues and civil unrest. When this happens, we do our best to notify our customers of any changes to their itinerary, by phone and/or email. However, sometimes the airline does not provide advance notice of the change or cancellation. For this reason, we recommend that you telephone your airline or check your flight status online 24 hours before your scheduled departure.

                                <br />
                                <br />If your flight has been cancelled, please call us at our toll free number as on our website.We will work directly with the airline on your behalf to find out what options are available and figure out a solution for you. However, if you don’t find out about the cancellation until you’re already at the airport, or are in-between flights, we recommend you work directly with the airline staff to figure out a solution. Please note that in some cases, especially during bad weather, your options may be limited.

                            </p>
                            <h3>
                                PAYMENT AND FLIGHT INFORMATION AND CONFIRMATION
                            </h3>
                            <p>
                                Some banks and credit card companies charge a fee for international transactions. They will appear on your credit or bank card statement as a foreign or international transaction fee. For example, if you make a travel reservation through our website from outside the United States using a U.S. credit card, your bank may convert the payment amount to your local currency and may charge you a fee for the conversion. The amount of the charge appearing on your credit or bank card statement may be in your local currency and different than the purchase amount shown on the billing summary page for the reservation.

                                <br />
                                <br />In addition, a foreign transaction fee may be assessed if the bank that issued your credit card is located outside the United States.

                                <br />
                                <br />Booking international travel through our website may be considered an international transaction by the bank or credit card company since Traveljunction.ca may pass your payment on to an international travel supplier.

                                <br />
                                <br />Your bank or credit card company determines the currency exchange rate and the amount of the foreign transaction fee on the day it processes the transaction. Please contact your bank or credit card company should you have any questions about these fees or the exchange rate applied to your transaction.

                                <br />
                                <br />Booking notification: Once your purchase is complete, you should receive an email titled “Booking Notification.” Your booking may provide you with a confirmation number before a ticket has been issued. If this is the case, the booking process is not complete and the fare is subject to change until a ticket is issued.

                                <br />
                                <br />Once your ticket has been issued, you should receive your electronic ticket.

                                <br />
                                <br>We strongly recommend that you re-confirm your flight reservation with the airline 24 hours prior to departure for domestic flights, and 72 hours prior to departure for international flights.

                            </p>
                            <h3>
                                SPECIAL REQUEST; SEATS, MEALS AND FREQUENT FLYER
                            </h3>
                            <p>Please note that requesting specific seats, meals, frequent flyers etc. are requests only. The airline reserves the right to make revisions to the seat allocation without notification. All requests should be confirmed with the airline and we cannot guarantee that passengers will be assigned the seats they’ve requested. Furthermore, we are unable to promise that your meal/frequent flyer/other special requests will be confirmed by the airline in question. Please ensure that you contact the airline you’ve booked with in order to confirm the requests you’ve made.</p>
                            <h3>
                                SUITABILITY OF TRAVEL PRODUCTS AND SERVICES
                            </h3>
                            <p>
                                On our website, we offer a variety of travel products and services for our customers. However, we do not represent or warrant that any of these travel products and services are or will be suitable and proper for you.

                                <br />
                                <br>You agree to release us from any claims relative to the travel products and services detailed on our website, including but not limited to claims that these travel products and services are not or were not suitable for you.

                            </p>
                            <h3>
                                SPECIALS
                            </h3>
                            <p>
                                From time to time we offer “specials” on our website. This section applies to all specials we offer on this website. As well, all the terms and conditions spelled out above apply to specials we offer on this website.

                                <br />
                                <br />Specials are only available for a limited time. Please contact us if need be to determine whether a special shown on our website is still available.

                                <br />
                                <br />Specific terms and conditions may apply to any special shown on our website. Please contact either us or the third-party provider of the special to determine what terms and conditions apply to that special, if any, and how they may affect you.

                                <br />
                                <br>Payment for any special that you book and that we confirm is due within 72 hours of our confirmation, unless otherwise agreed to by us in writing. If you fail to pay for the special within this 72-hour timeframe, your booking may be cancelled. We accept no responsibility for any loss you incur as a result of cancellation for non-payment within 72 hours.

                            </p>
                            <h3>
                                INTELLECTUAL PROPERTY
                            </h3>
                            <p>
                                This website, including its underlying software and its text, design, graphics, layout and content, is owned or licensed by us or by the respective owners. All this material is protected by Canadian and international intellectual property laws.

                                <br />
                                <br />As a visitor to or user of this website, you have permission to view, use and electronically copy the pages and content of this website through the usual and ordinary use of a web browser.

                                <br />
                                <br />Any other use of this website and its contents, such as copying, distributing, selling, modifying, transmitting, re-using, re-posting or publishing, is not permitted and is strictly prohibited without the specific written permission of the owner(s) of such material.

                                <br />
                                <br />Any unauthorised use of our website or its contents will breach this agreement and may void your permission to use this website. It may also violate copyright and other laws.

                                <br />
                                <br>Certain trademarks, service-marks, business names, company names, logos, trade names and presentation techniques (trade dress) used on this website are owned by us or by our licensors. In particular, we own the trademark “Traveljunction.ca.” You do not have a right, license or permission to use any of them.

                            </p>
                            <br>
                            <p>
                                As a visitor to or user of this website, you must use it in a responsible and co-operative manner.

                                <br />
                                <br>You must not:

                            </p>
                            <ul>
                                <li>make any fraudulent, speculative or false enquiries, bookings, or reservations, or make any reservations in anticipation of demand;</li>
                                <li>use any form of robot, spider, scraper or other automated means, or any comparable manual process, for the purpose of accessing, monitoring or copying any of the content or information on this website without our prior written consent;</li>
                                <li>reproduce, upload, post, display, republish, distribute, or transmit any content of this website in any form or manner whatsoever;</li>
                                <li>place or enter false, misleading or incorrect information on the website;</li>
                                <li>make any form of booking, reservation or request through this website without fully intending to use that booking, reservation or request for legitimate travel purposes;</li>
                                <li>use another person’s name, user ID or password to make bookings, reservations or inquiries on this website without that person’s prior permission;</li>
                                <li>use this website while impersonating or acting as another person;</li>
                                <li>post on or transmit through this website any unlawful, threatening, defamatory, libelous, obscene, indecent, inflammatory or pornographic material or images, or any other material that could give rise to or result in civil or criminal proceedings;</li>
                                <li>access or use this website in any manner that, in our opinion, could impair, impede or otherwise negatively affect the proper functioning and performance of this website and its systems, or that could negatively impact other visitors to or users of this website;</li>
                                <li>tamper with or hinder the operation of this website or make unauthorised modifications to the website;</li>
                                <li>delete data from this website without our permission;</li>
                                <li>knowingly transmit any virus, malware or other disabling feature or software to or through this website;</li>
                                <li>breach the rights of any third party (including rights in intellectual property or contract as well as obligations of confidentiality or nondisclosure) or break any related laws in visiting or using this website;</li>
                                <li>frame this website as part of another website, or cache this website for commercial gain or advantage;</li>
                                <li>disguise or mask the origin device and/or IP address information of the data being transmitted through this website;</li>
                                <li>knowingly permit or allow another person to do any of the above acts.</li>
                            </ul>
                            <p>We reserve the right to restrict or terminate your access to any or all of the features and components of this website if we believe you have violated, or are violating, any of the above prohibitions. In the event of any such restriction or termination, you must immediately cease any prohibited use of this website. Attempting to access or use the website in violation of any restrictions or terminations shall constitute an act of trespass. We will pursue legal action to the fullest extent possible against anyone whom we believe is in breach of the above prohibitions or is committing trespass on the website, and we reserve the right to do so.</p>
                            <h3>
                                YOUR WARRANTIES
                            </h3>
                            <p>You declare and affirm the following:</p>
                            <ul>
                                <li>you have reached the age of majority and are therefore old enough to legally use this website and enter into legally-binding contractual obligations;</li>
                                <li>you agree to be responsible (financially and otherwise) for all uses you make of this website as well as the uses of those whom you allow to use your user ID and password to access this website;</li>
                                <li>all information you provide on or through this website will be correct, accurate, not misleading, not deceptive and not be likely or intended to mislead or deceive others.</li>
                            </ul>
                            <h3>
                                INDEMNITY
                            </h3>
                            <p>You agree to indemnify and hold harmless both our company and the officers, employees and agents of our company from and against any and all losses, damages, claims, costs and expenses arising from any or all of the following:</p>
                            <ul>
                                <li>any violations by you of these Terms &amp; Conditions;</li>
                                <li>any act or omission by you personally or by an officer, employee or agent of your company;</li>
                                <li>any claim, demand, cause of action or legal proceeding by a third party against us or our officers, employees and/or agents that arose by reason of an act or omission by you personally or by an officer, employee or agent of your company.</li>
                            </ul>
                            <h3>
                                YOUR PRIVACY
                            </h3>
                            <p>
                                Subject to the terms of our 'Privacy Policy' (found on a separate page on this website), we will not disclose your personal information without your permission unless we have to in order to comply with your request or instructions, or unless otherwise required by law. “Personal information” in this context includes such things as your name, contact information and browsing habits provided to us by you or by your web browser.

                                <br />
                                <br />In the course of providing you with travel-related products and services, we and our third-party providers of such products and services may disclose personal information about you to others in order to set up your travel package. For example, we may disclose information about you to airlines, hotels or car rental companies to complete your travel arrangements.

                                <br />
                                <br>Separately, we may disclose aggregated information about users and use statistics from our website as well as aggregated information about our sales and trading patterns to others in the ordinary course of our business.

                            </p>
                            <h3>
                                DISCLAIMERS
                            </h3>
                            <p>
                                This website and all its content is provided for your use on an “as is” basis and at no charge. We make no warranties or representations of any kind with respect to the website, its contents or any of the products or services offered, provided or made available on or through this website. Moreover, we do not warrant or represent that the content of this website is accurate, current or complete, or that it does not infringe the rights of others.

                                <br />
                                <br />We disclaim all implied warranties and representations to the maximum extent permitted by law, including, without limitation, implied warranties that the products and services offered, sold and provided through this website will be of merchantable quality, are fit for any purpose or comply with the descriptions and samples displayed on this website.

                                <br />
                                <br />We do not warrant or represent that this website, the server on which it resides or any of the products and services offered, sold or provided on or through this website are or will be free of errors, defects, viruses or other malicious software.

                                <br />
                                <br />We have endeavoured to make this website secure and safe to use, and will continue to do so. We have implemented security measures and technology for this purpose. However, because of the proliferation of viruses, malware and other malicious software on the Internet, we cannot and do not warrant or represent that this website is or will remain secure.<br /><br />The ability to access and use this website through the Internet is subject to factors over which we have no control. We therefore cannot and do not warrant or represent that you will be able to access this website at any time you want, or that access to the website will be uninterrupted or timely.<br /><br />If you are unable to access this website, or if the website fails to operate properly, or at all, and you incur loss or damage as a result, your sole remedy is the refund of the money you paid us to use this website, if any.<br /><br />Our role through this website is to help you make travel arrangements, including placing reservations and processing payments. The travel-related products and services offered, promoted and sold through this website are provided by third parties. We are acting as an agent for these third-party providers. As such, your legal relationship regarding these products and services is with the actual providers of these products and services, and not with us. Therefore, you release us from all liability, claims, damages, costs and expenses to the extent permitted by law arising out of the provision or failure to provide, as well as the use or non-use, of these third-party travel products and services. This includes direct, indirect, special and consequential loss or damage, whether in negligence or otherwise.<br /><br />Neither will we nor will any of our officers, employees, agents, shareholders or other representatives be liable in damages or otherwise to the maximum extent permitted by law in connection with your use of or inability to use or access this website or your purchase and use of any products and services offered, promoted or sold on or through this website.<br /><br />This limitation of liability applies to all damages of any kind, including compensatory, direct, indirect, special or consequential damages; loss of data, income or profit; loss of or damage to property; personal injury; and claims of third parties.<br /><br />If any warranties implied by law cannot be excluded, then our liability for breach of such warranties is limited, at our option, to:<br /><br />a. in the case of products: the replacement of the products or the supply of equivalent products; or the payment of the cost of replacing the products or acquiring equivalent products;<br /><br>b. in the case of services: the supply of the services again; or the payment of the cost of having the services supplied again.
                            </p><h3>CONFIDENTIALITY</h3><p>You can communicate with us through this website. The website also lists other ways you can communicate with us.<br /><br />We do not accept information that is confidential or proprietary, other than for making travel arrangements or reservations. Please understand that this is our policy.<br /><br>If you are concerned about the confidentiality of information you are sending us being compromised, do not transmit that information to us through this website; rather, mail or email the information to us instead. Please note, however, that any ideas or suggestions that you send or reveal to us through this website or otherwise are ours to use or disclose without limitation or restriction, even if you have marked the information as being confidential or proprietary or if you include statements that are contrary to these Terms &amp; Conditions.</p><h3>LINKING</h3><p>We may link our website to other websites on the Internet. We do this strictly for your convenience as you explore different travel options online. However, the inclusion of any such links does not indicate that we endorse the website or the business to which we have linked. Further, we have not verified the content of any website to which we have linked, and we bear no responsibility whatsoever for the content of any linked website. Should you incur any loss or damage from visiting or doing business with any linked website or business, we are not liable for that loss or damage.</p><h3>AMENDMENTS</h3><p>We may amend these Terms and Conditions at any time without prior notice to you, except as otherwise specified. We will post the amended Terms and Conditions on this website, and they will take effect immediately upon being posted on the website.</p><h3>TERMINATION</h3><p>We reserve the right to immediately terminate this Agreement as well as any other agreement between you and us if you breach any of these Terms and Conditions.</p><h3>OUR RELATIONSHIP</h3><p>No agency, partnership, joint venture, employer-employee or franchisor-franchisee relationship exists between you and us, nor is such a relationship created between you and us by these Terms and Conditions or by our Agreement with you.</p><br><h3>GOVERNING LAW</h3><p>Should any legal dispute arise concerning the interpretation or application of these Terms and Conditions and/or this Agreement, or should any legal dispute arise because of your use of this website, we will select the applicable legal jurisdiction and venue in our sole discretion.</p><h3>GENERAL</h3><p><span></span><span></span><span></span><span></span><span></span><span></span><span></span><span></span><span></span><span></span><span></span><span></span><span></span><span></span><span></span><span></span><span></span><span></span><span></span></p><p>If any of these Terms and Conditions is found by a court or other legal authority to be invalid or unenforceable, the invalid or unenforceable provisions will be stricken. The remaining terms and conditions will remain in full force and effect.<br /><br />The headings used in these Terms and Conditions are for reference purposes only.<br /><br />If we take no action in response to a violation by you or others of one or more of these Terms and Conditions, that inaction shall not constitute a waiver of the violated terms and conditions and shall not impair our right to take action in response to subsequent or similar violations.<br />In this Agreement, the term “website” includes any e-mail bulletins or other content that we provide to you through this website or otherwise initiated from this website.</p><p></p>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>";
        return terms;
    }

    private string termconditionEU()
    {
        terms = @"<h3>About Us</h3>

                                    <p>ie.traveljunction.co.uk When you making a booking you confirm that you have authority to accept these terms and conditions on behalf of yourself and all member of your party. This user agreement is between you and ie.traveljunction.co.uk, and is governed by the laws of England.</p>

                                    <h3>Contract</h3>
                                    <p>

We act as a disclosed agent for third party suppliers, such as airlines, tour operators, car hire companies, hotel companies, consolidators and insurance companies. This means that, when you make a booking, the contract for the product is between you and the supplier. As such, the supplier's booking conditions (including airlines’ conditions of carriage) apply to your booking, in addition to those set out here. Please make sure that you have read the supplier’s conditions before completing your transaction with us, as they do contain important information about your booking. (You can easily request a copy of the supplier's conditions from us – see ‘Contacting ie.traveljunction.co.uk’ below). Please read your ticket wallet/ travel documents for the airline’s conditions of carriage.<br><br>

In a small number of cases, for some products, we act as principal; in these cases the contract for the product is between you and us. We will let you know in instances where we act as principal.<br><br>

All travel arrangements which we provide or which are sold through us are not an offer by us to sell any travel arrangements, but an invitation to you to make an offer to the suppliers of the arrangements. We are free to accept that offer on behalf of those suppliers or to reject it.

                                    </p>

                                    <h3>Liability Protection</h3>
<p>As agent we accept no responsibility for the acts or omissions of the supplier, or for the products and services provided by them. However, we may still be liable to you if we have been negligent, have misrepresented important information.</p>


<h3>Your Obligations</h3>
<p>You agree to be bound by the following obligations, including without limitation: You accept financial responsibility for all transactions made under your name or account. In order to make a purchase you must be at least 18 years old, be purchasing for yourself and have the legal capacity to make the transaction. In case you making booking for a group/part/family will responsible for payment of all.<br><br>
You must make sure that all the information you provide to us is true and accurate. (Please ensure that you notify us in writing immediately of any change to your address, email address or telephone number).<br><br>
Failure to supply correct and complete credit or debit card details, including cardholder name/ billing address, may result in delays to the issue of your tickets, increases in the fare, or at worst cancellation of the booking, so please ensure that the details you give match those on the card/ statement.
You must not use the website for speculative, false or fraudulent bookings.</p>

<h3>Supplier Conditions</h3>
<p>You are responsible for complying with any airline's terms, for example in relation to check-in times, reconfirmation of flights and other matters. ie.traveljunction.co.uk accepts no responsibility for bookings cancelled due to non-compliance with the airline's rules.<br><br>
For scheduled flight tickets there are additional terms which apply to each fare. These terms may include, for example, conditions relating to changes, refunds, minimum and maximum duration of stay. Generally, the more flexible the ticket, the more expensive it is likely to be and you need to take various points into consideration before deciding between the fares on offer.<br><br>
The flights confirmed on your ticket must be used in order of sequence (in the order set out in your itinerary). If this requirement is not met, the airline may cancel any onward flights, and you are unlikely to receive any refund.<br><br>
Some airlines require you to reconfirm each flight 72 hours prior to departure, and may cancel your flight if you do not do so. If you fail to reconfirm you may be refused permission to board the aircraft and you are unlikely to receive any refund. In any case, ie.traveljunction.co.uk strongly recommends that you reconfirm all flights in this way in case of last minute schedule changes.</p>

<h3>Information on the Site</h3>
<p>ie.traveljunction.co.uk does not guarantee that information on the site (including without limitation prices, descriptions or dates) is free from errors or omissions, or that it is suitable for your intended use. We will use all reasonable endeavours to correct any errors or omissions as soon as practicable once they have been drawn to our attention.<br><br>

ie.traveljunction.co.uk offers all general information on the site for guidance only. We reserve the right to change at any time any aspect of the site or its content, including the availability of any suppliers, features, information or content.</p>


<h3>Links</h3>
<p>The ie.traveljunction.co.uk website contains links to websites owned and operated by third parties. Such linked websites are not controlled or maintained by us; as such ie.traveljunction.co.uk has no responsibility in relation to the accuracy, completeness and quality of the information contained within them.<br><br>
ie.traveljunction.co.uk shall not be liable for any loss or damage caused by use of or reliance upon any content, goods or services available on linked websites.</p>


<h3>Pricing</h3>
<p>All our prices are quoted in British Pounds Sterling and subject to availability.<br><br>

All fares quoted at the 'confirmed quotation' stage include pre-payable taxes and applicable transaction fees. These are added together to form your final quotation.<br><br>

Please note that for some destinations a departure and/ or arrival tax is payable locally. It is the passenger's responsibility to pay such taxes, and ie.traveljunction.co.uk accepts no responsibility for denied boarding resulting from failure/ inability to pay such taxes.<br><br>

The price you pay for your tickets is likely to include a booking/ transaction fee made by ie.traveljunction.co.uk. This is our fee for making and administering your booking, and is non-refundable.<br><br>

Service fees are also charged for other forms of administration, including, but not limited to, reservation changes and refund applications.<br><br>

Fares are subject to change without prior notice. Should the fare for your journey be changed by the airline/s with which you are due to travel, or if we discover that the fare you have paid is incorrect, prior to the start of your journey, we will do our best to notify you as soon as this comes to our attention. In such circumstances, you will be liable for any difference in fare. The airline may refuse travel if the correct fare has not been paid. Alternatively we may cancel the contract, without any liability to you.<br><br>

For our 'Special Fares' for scheduled flights, these are subject to verification by our ticketing staff. Should we be unable to honour the fare quoted, we will endeavour to notify you within three working days.<br><br>

Where we book a product priced in a different currency, we reserve the right to include an additional amount to cover the costs of currency conversion and exchange rate fluctuations.</p>

<h3>Denied Boarding, Flight Delays and Cancellations</h3>
<p>Details of these rights are available from airlines and are also displayed at European airports.<br>
Any payment/ reimbursement in such cases is the responsibility of the airline.</p>

<h3>Community List</h3>
<p>Under European regulations we are required to bring to your attention the existence of a Community List of air carriers which are subject to an operating ban within the EU. The list can be viewed at <a href='http://www.ec.europa.eu/transport/air-ban/list_en.htm' target='_blank'>www.ec.europa.eu/transport/air-ban/list_en.htm.</a></p>

<h3>Making a booking / booking details</h3>
<p>All travel products and services featured on the site are subject to availability.</p>


<h3>Online Booking</h3>
<p>If you book online, you must provide us with all information which we require. You must also ensure that the credit or debit card you are using is your own (or, subject to our agreement, if it is a third party's you have their express authorisation, to use their credit or debit card) and that sufficient funds are available to cover the cost of the arrangements which you book with us.<br><br>

When we receive and accept your booking we will send you a booking confirmation e-mail and debit payment from you. We do not make any representation or warranty as to the availability of any package holiday, flight or Individual Components nor that our booking services are free from infection of viruses or anything else that has a contaminating or destructive effect on your property.</p>

<h3>Bookings made by telephone</h3>
<p>If you make a booking by telephone you must provide us with all information which we require. You must also ensure that all information which you provide is accurate and that the credit or debit card you are using is your own or, subject to our agreement,if it is a third party's you have their express authorisation to use their credit or debit card and that sufficient funds are available to cover the cost of the arrangements which you book with us.<br><br>

If we accept your booking, we shall debit payment from you and send you a confirmation .From this point cancellation charges will apply: Please note the details of phone booking always confirmed in writing to avoid any mistake. As soon as you receive the confirmation request, please check the details carefully and inform us immediately if anything appears to be incorrect as it may not be possible to make changes later<br><br>

Once you have confirmed these details we will proceed to confirm the booking with the supplier/s.</p>


<h3>Special Requests</h3>
<p>We are happy to forward details of any additional requirements you may have (for example seating or diet preferences, assistance, etc.) to the relevant travel supplier. These are treated as special requests, and as such fulfilment of these requests cannot be guaranteed.</p>

<h3>Pregnancy</h3>
<p>If you will be traveling while pregnant, please note that regulations apply to the carriage of passengers who are more than a certain number of weeks pregnant at the time of travel. These regulations vary between airlines, so it is your responsibility to check the airline's policy before making a booking.<br><br>

In some cases travel may not be permitted, or you may be required to travel with a doctor's letter, or even obtain a medical clearance from the airline.<br><br>

You must ensure that your travel insurance covers you if you are pregnant. We recommend that you also confirm with your doctor before booking that you are fit to travel.<br><br>

<strong>Travelling with Child</strong><br><br>

Airlines require original  Date of Birth Certificate for child travelling to South Africa.<br><br>

<strong>Travelling with Infants</strong><br><br>

Airlines require that infants must be of a minimum age (typically six weeks) before they will be permitted to travel. Please ensure that you are aware of the airline's policy before making a booking.<br><br>

Infants must sit on an adult's lap or occupy an infant seat – please contact the airline you are traveling with for details of appropriate seats.<br><br>

To qualify for an infant fare, the infant must generally be under two years old on the date of return travel. Children aged two years or above must occupy a seat.</p>


<h3>Paying for your booking</h3>
<p>Payment can be made by various Debit / Credit cards mentioned on site , by E commerce link . ie.traveljunction.co.uk may be required to pass your card details to the relevant supplier for fulfilment of the booking.<br><br>

Scheduled flights &amp; webfares: Full payment is required at the time of booking, by debit or credit card. We reserve the right to apply a surcharge for all credit card transactions.
We reserve the right to cancel bookings before or after ticket issue if payment is declined by the card issuer, or if incorrect card details or billing information have been supplied.<br><br>

ie.traveljunction.co.uk will not be liable for any increase in fare due to payment failure.<br><br>

Further, in an effort to minimise the effects of credit card fraud, we reserve the right to carry out random checks, including checks of the electoral roll, and may request you to either fax or email to us proof of your address and a copy of the credit/ debit card and recent statement, or a copy of the cardholder's and/or passenger's passport/s, before issuing any tickets. We reserve the right to insist that travel documents be sent to the billing address of the card used to pay for the booking. ie.traveljunction.co.uk reserve the right to pass on any charges relating to card chargebacks. As a British company it is not always possible for us to successfully complete the necessary security and identity checks on cards which are registered to billing addresses outside the United Kingdom. If you make a booking using a credit card registered outside the UK, we reserve the right to reject your booking if we are unable to satisfactorily complete the relevant checks.</p>

<h3>Managing your booking</h3>
<p>Please ensure that all your travel, passport, visa and insurance documents are in order and that you arrive in plenty of time for checking in at the airport. It may be necessary to reconfirm your flight with the airline prior to departure. Please ask us for details at least 72 hours before your outbound flight. You should take a note of any reference number or contact name when reconfirming. If you fail to reconfirm you may be refused permission to board the aircraft and you are unlikely to receive any refund.</p>


<h3>Cancellations / Amendments by You</h3>
<p>Any cancellation or amendment you wish to make to your booking will be subject to the relevant supplier's conditions. Some tickets are non-refundable and/ or non-changeable; in other cases it may be possible to amend or cancel your booking subject to a penalty charge.<br><br>

It must be emphasised that the vast majority of airline tickets are non-transferable, and as such name changes are not normally permitted. It is therefore essential that you enter passengers' names as per passport at the time of booking. For online scheduled flight bookings, the rules regarding cancellations and amendments for the fare purchased are shown during the booking process, please ensure you read these carefully.<br><br>

ie.traveljunction.co.uk, as agents for the airlines/consolidator must abide by the airlines' terms and conditions – we are unable to deviate from the fare rules/ supplier's conditions.

Where changes or refunds are permitted, ie.traveljunction.co.uk will apply administration fees (in line with our current rates) in addition to any fees charged by the supplier, if you ask us to amend or cancel your booking. This is to cover the costs we incur in administering cancellations and amendments.<br><br>

Any request to amend or cancel a reservation must be notified to ie.traveljunction.co.uk in writing, and can not be actioned until received by us. Requests received outside our opening hours can not be processed until we reopen the next working day, and if we need to contact the travel supplier to effect the change/cancellation, until we are able to contact them.</p>

<h3>Cancellation</h3>
<p>

Admin fees of £35 per passenger will be charged as cancellation penalty in case of pre/post ticket issuance on top of airline penalty.<br><br>

If you wish to cancel your booking and attempt to claim a refund, please telephone/ email us in the first instance, so we can advise you if the conditions of your ticket permit any refund. If you wish to proceed with the cancellation, you must notify us in writing. (If you do not advise us of your intention to cancel a booking before the scheduled departure time, or do not check-in, this will be recorded by the airline as a 'no show' and is likely to result in the forfeit of all monies paid).
Where a refund is permitted, this may take some time, typically 10-16 weeks, to be authorized by the airline. Once authorized, any refund will be made to the debit or credit card used to make the original booking. It is vital that you advise us when you cancel the booking if that card is no longer valid.<br><br>

Any refund made will be nett of any cancellation charge from the airline or tour operator. Many airlines also charge an additional fee to process refunds. Refunds of any kind will also be subject to ie.traveljunction.co.uk usual administration fees. Booking fees, credit card charges, postage costs and any amendment fees you may have paid for any previous changes are also non-refundable.<br><br>

In the case of non-refundable scheduled flight tickets, it may be possible to claim back any unused taxes. Please note that not all taxes are refundable. Some airlines do make a fee for processing such requests, and in some cases the charge exceeds what you would get back. Please ask for details when you cancel your booking.
</p>

<h3>Amendment</h3>
<p>If you wish to change your booking, please telephone/ email us in the first instance, so we can advise you if the conditions of your ticket permit any changes. If a change is permitted, we may ask you to confirm in writing that you wish to change the booking.<br><br>

Any changes are subject to availability, limitations and restrictions of the relevant travel supplier.<br><br>

If a change increases the cost of your booking, you will need to pay such extra costs.<br><br>

Changes of any kind will also be subject to ie.traveljunction.co.uk's usual administration fees.</p>


<h3>Cancellations / Amendments by the Travel Supplier</h3>
<p>Airlines reserve the right to make time changes, or in rare cases, to cancel flights, for operational reasons. Whilst ie.traveljunction.co.uk is not responsible for, and has no control over, such changes, we will do our best to assist when such situations arise.<br><br>

In the unlikely event that your flight is cancelled by the airline or tour operator, your rights and remedies will be governed by the supplier's conditions/ airline's conditions of carriage. As a result you may be entitled to:<br><br>

(a) carriage on another flight with the same airline without additional costs;<br>
(b) re-routing to your destination with another carrier without additional costs;<br>
(c) some other right or remedy.<br><br>

In the event of schedule changes made prior to commencement of your journey, it is not always necessary to have your tickets reissued or revalidated, but we will advise you should this be necessary.
ie.traveljunction.co.uk take no responsibility for any flight rescheduling en route. </p>

<h3>Insurance</h3>
<p>Many suppliers require you to take out travel insurance as a condition of booking with them. In any case, we strongly recommend that all our customers arrange adequate travel insurance for the duration of the trip, since circumstances may arise where neither Wayfarers nor the supplier are liable.<br><br>

It is recommended that insurance is taken immediately upon making the booking, in order to cover you and your party against the cost of cancellation by you; the cost of assistance (including repatriation) in the event of accident or illness; loss of baggage and money; and other expenses. In most cases cancellation fees will apply if you need to cancel your booking before you travel.<br><br>

ie.traveljunction.co.uk does sell travel insurance, and will be pleased to quote a premium for your journey on request. If we have issued your policy please check it carefully to ensure that all the details are correct and that all relevant information has been provided by you (eg. pre-existing medical conditions). Failure to disclose relevant information will affect your insurance. Please read the policy carefully to ensure that this is suitable and adequate for your needs.</p>


<h3>Tickets</h3>
<p>The type of tickets which will be issued for your booking depends on the airline and route you are booking. The majority of airlines are increasingly issuing electronic tickets, and ie.traveljunction.co.uk will issue electronic tickets whenever the itinerary permits (airlines now insist that e-tickets are issued in these circumstances). For charter flights, paper tickets will be sent by normal post.</p>


<h3>Lost Tickets</h3>
<p>If you lose your paper tickets, you must notify us as soon as possible. It may be possible to reissue them for a fee (depending on the airline's/ supplier's rules). If so, the cost may depend on the circumstances of the loss and how close to the departure date you discover it. In all cases, the charge, and procedure to be followed, will vary according to the airline's policy. ie.traveljunction.co.uk reserves the right to charge fees to cover the administration costs of this.<br><br>

However, not all tickets can be reissued, in which case you may need to purchase a completely new ticket. If a lost ticket can not be reissued, a refund can be requested from the airline: any refund is at the airline's sole discretion and is not guaranteed. Such refunds may take up to a year to be authorised by the airline.</p>


<h3>Non-Delivery / Non-Receipt of Tickets</h3>
<p>It is your responsibility to advise us if you do not receive your tickets/ e-tickets.<br>
ie.traveljunction.co.uk can not accept responsibility if you fail to receive your e-tickets due to providing an inaccurate email address or your junk email settings. We recommend that you add the respective agent email address / info@traveljunction.co.uk to your safe list. </p>

<h3>Passport / Visas / Health Requirements</h3>
<p>It is your responsibility to ensure that you understand and comply with all the passport, visa and health requirements of all the countries involved in your itinerary (including those that you transit).<br><br>

It is your responsibility to ensure that you are in possession of a valid passport for your journey. Your passport must also be legible and intact. When making your booking you must ensure that the names you provide match those shown on the passengers' passports. Most countries require that your passport is valid for a period of at least six months after your return travel date: we recommend that you check with the embassy to confirm exact requirements.<br><br>

It is strongly recommended that children hold their own individual passports; where a child is still included on a parent's passport you are advised to check that this will be suitable for the destination you are visiting before making a booking. Many countries still require passengers to obtain a visa, and in some cases transit visas may be required for countries which you pass through en route to your destination (even when you do not leave the aircraft). ie.traveljunction.co.uk can provide general information about the passport and visa requirements for your trip. Alternatively, for the most up to date information, we recommend that you contact the embassies of the countries you are travelling to/ through. We recommend that you do this well in advance of travel, as visas for certain countries can take some time to obtain.<br><br>

Some countries also have additional immigration requirements, for example South Africa requires passengers to have at least 2 blank pages in their passports. For travel to the USA, a machine-readable passport is required, among other requirements, details of which may be found at www.usembassy.org.uk. Most destinations will require proof of return travel. Please take special note that for all air travel within the British Isles, airlines require photographic identification of a specific type. Please check all details in respective country website.<br><br>

Regarding health, you are strongly advised to check with your GP prior to travel for up to date information regarding vaccinations which may be required or recommended for your destination/s. Please note that some countries may require proof of certain vaccinations as a condition of entry. We can provide general information about any health formalities required for your trip but you should check with your own doctor for your specific circumstances.<br><br>

Please note that health and immigration requirements can change at short notice. Neither ie.traveljunction.co.uk nor the suppliers can accept any responsibility if you are denied boarding or are deported due to failure to comply with the above. You will be responsible for any costs you or ie.traveljunction.co.uk incur as a result of such failure. </p>


<h3>Service Charges</h3>
<p>We apply a service charge for certain services we provide. These charges are non-refundable. These are in addition to any fees charged by the supplier.</p>


<h3>Your Financial Protection (ie.traveljunction.co.uk ATOL 10950)</h3>
<p>When you buy an ATOL protected flight or flight inclusive holiday from us you will receive an ATOL Certificate. This lists what is financially protected, where you can get information on what this means for you and who to contact if things go wrong.<br><br>

We, or the suppliers identified on your ATOL Certificate, will provide you with the services listed on the ATOL Certificate (or a suitable alternative). In some cases, where neither we nor the supplier are able to do so for reasons of insolvency, an alternative ATOL holder may provide you with the services you have bought or a suitable alternative (at no extra cost to you). You agree to accept that in those circumstances the alternative ATOL holder will perform those obligations and you agree to pay any money outstanding to be paid by you under your contract to that alternative ATOL holder. However, you also agree that in some cases it will not be possible to appoint an alternative ATOL holder, in which case you will be entitled to make a claim under the ATOL scheme (or your credit card issuer where applicable).<br><br>

If we, or the suppliers identified on your ATOL certificate, are unable to provide the services listed (or a suitable alternative, through an alternative ATOL holder or otherwise) for reasons of insolvency, the Trustees of the Air Travel Trust may make a payment to (or confer a benefit on) you under the ATOL scheme. You agree that in return for such a payment or benefit you assign absolutely to those Trustees any claims which you have or may have arising out of or relating to the non-provision of the services, including any claim against us, the travel agent (or your credit card issuer where applicable). You also agree that any such claims may be re-assigned to another body, if that other body has paid sums you have claimed under the ATOL scheme.</p>";
        return terms;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

        XPDetails=BookingDetails(txtInvoice.Text.Trim());
    }

    public string BookingDetails(string XP)
    {
        

        dtPax = obj.GET_Passenger_Detail(XP, "001");
        GetSetDatabase gs = new GetSetDatabase();
        dtSector = gs.GET_Sector_Detail(XP, "001");
       // dtSector = obj.GET_Sector_Detail(XP, "001");
        dtComp = obj.GET_Booking_Master(XP);
        dtContact= obj.GET_Contact_Detail(XP, "001");
        //dtPrice = obj.GET_Amount_Charges_Detail(XP, "001", "", "", "", "", "", "", "");
        //dtTrans = obj.GET_Transaction_Master(XP, "");
        //obj.GET_Transaction_Details(XP, "");
        string price = dtComp.Rows[0]["CurrencyType"].ToString() + " " + txtPrice.Text.Trim();
        string note=txtNote.Text==""?"":"<strong>Notes :"+txtNote.Text+ "</strong>";
         note += "<br/><p style='font-weight:bold;'>Please note that before your departure, please recheck with Airlines for any Covid restrictions or any other formalities to be fulfilled. We as company will not be responsible if airlines deny boarding due to these reasons.</p><br/><br/>";
        
        
        //FLTUK
        string authorizeTravelPerformance = string.Empty;
        
        if  (dtComp.Rows[0]["BookingByCompany"].ToString() == "FLTUK" 
            || dtComp.Rows[0]["BookingByCompany"].ToString() == "SkyWorld"
            || dtComp.Rows[0]["BookingByCompany"].ToString() == "FLTTROTT"
            || dtComp.Rows[0]["BookingByCompany"].ToString() == "TRAVELOFLIUK"
            || dtComp.Rows[0]["BookingByCompany"].ToString() == "TOF_GOOGLE")
        {
            IncludeAuth.Style.Add("Display", "block");
             authorizeTravelPerformance = "<tr ><td><input type='checkbox' checked='checked' /> I authorize  to change the flights on my behalf.</td></tr><tr style='border-bottom: 1pt solid black;'><td></td> </tr>";
            if(Page.IsPostBack)
            {
                if(IncludeAuth.Checked==false)
                {
                    authorizeTravelPerformance = string.Empty;
                }
            }
        }
        StringBuilder sb = new StringBuilder();

        if (txtXPTo.Text.Trim() == "")
        { txtXPTo.Text = dtContact.Rows[0]["EmailID"].ToString(); }

        
        sb.Append("<table width='800' border='0' align='center' cellpadding='0' cellspacing='0' style='font-size: 12px; font-family: Arial, Helvetica, sans-serif; color:#000;'>" +
    "<tr>" +
        "<td style='padding: 20px; border: #e6e6e6 solid 1px;'>" +
            lo.HeaderLayout(dtComp.Rows[0]["BookingByCompany"].ToString()) +
                   " </td>" +
                " </tr>" +
                "<tr>" +
                    "<td>&nbsp;</td>" +
                " </tr>");
       
        sb.Append("<tr>" +
                    "<td>" +
                        "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                            "<tr>" +
                             
                                "<td width='10' align='left' valign='middle'></td>" +
                                "<td align='left' valign='middle' style='font-size: 14px; color: #333333; padding-bottom:15px; font-weight:bold;'>Dear " + dtPax.Rows[0]["Title"] + " " + dtPax.Rows[0]["FName"] + " " + dtPax.Rows[0]["LName"] + "," +

                                "</td>" +
                            " </tr>" +
                             "<tr>" +
                              

                                "<td width='10' align='left' valign='middle'>&nbsp;</td>" +
                                "<td align='left' valign='middle' style='font-size: 14px; color: #333333;'><p>Please find below the details of your flight and price we have agreed upon. We would like to inform you that we are in the process of issuing your ticket. Hence, you are requested to kindly go through your flight itinerary including names, flight connection and airlines &amp; bring into our attention straightaway in case of any discrepancy as we might not be able to change it after the issuance of the ticket.<strong> Once you verify all the information including passenger(s) name, flight connection, airlines, number of passengers, price and our Terms and Condition etc</strong>, we would request you to kindly send us an email with your acknowledgement to proceed further with the booking.</p><br/>" +

                                note+

                                "<p style='color:red;font-weight: bold;'>Acknowledgement on this e-mail implies that you have read and accepted the terms &amp; conditions associated with the booking. Hence, you must crosscheck the complete details before confirming the reservation.</p><br/><br/></td>" +
                            //"<td align='left' valign='middle' style='font-size: 14px; color: #333333;'>We request you to kindly look into the details of flight and pricing and confirm it. So, that we can proceed for the same. You must cross check the details including passenger(s) name, flight connection, airlines, number of passengers, price and our Terms and Condition etc. Your confirmation enables us to get the booking done in a day or so. In case of any amendment, do let us know.<br/><br/></td>" +
                            " </tr>" +
                        "</table>" +
                   " </td>" +
                " </tr>" +
                "<tr>" +
                    "<td>" +
                        "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                            "<tr>" +
                                "<td align='left' valign='top' style='background: #e6e6e6;'>" +
                                    "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                                        "<tr>" +
                                            "<td align='left' valign='top' style='background: #333333; border-bottom: #FFF solid 1px; color: #FFF; padding: 5px 10px; font-size: 12px; font-weight: bold; font-family: Arial, Helvetica, sans-serif;'>ReferenceNumber</td>" +
                                        " </tr>" +
                                        "<tr>" +
                                            "<td align='left' valign='top' style='padding: 10px;'>" +
            // <!--passenger contact-->
                                                "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                                                    "<tr>" +
                                                        "<td align='center' valign='middle' style='background: #FFF; border: #000 solid 1px; color: #F90; font-size: 18px; font-weight: bold; text-transform: uppercase; padding: 5px 10px;'>" + XP + "</td>" +
                                                        "<td width='10' align='left' valign='middle'>&nbsp;</td>" +
                                                        "<td align='center' valign='middle'>" +
                                                            
                                                       " </td>" +
                                                    " </tr>" +
                                                    "<tr>" +
                                                        "<td align='left' valign='middle'>&nbsp;</td>" +
                                                        "<td width='10' align='left' valign='middle'>&nbsp;</td>" +
                                                        "<td align='left' valign='middle'>&nbsp;</td>" +
                                                    " </tr>" +
                                                    "<tr>" +
                                                        "<td align='left' valign='middle'>Name</td>" +
                                                        "<td width='10' align='left' valign='middle'>:</td>" +
                                                        "<td align='left' valign='middle'>" + dtPax.Rows[0]["Title"] + " " + dtPax.Rows[0]["FName"] + " " + dtPax.Rows[0]["LName"] + "</td>" +
                                                    " </tr>" +
                                                    "<tr>" +
                                                        "<td align='left' valign='middle'>Phone No</td>" +
                                                        "<td width='10' align='left' valign='middle'>:</td>" +
                                                        "<td align='left' valign='middle'>" + dtContact.Rows[0]["PhoneNo"] + "</td>" +
                                                    " </tr>" +
                                                    "<tr>" +
                                                        "<td align='left' valign='middle'>Email Address</td>" +
                                                        "<td align='left' valign='middle'>:</td>" +
                                                        "<td align='left' valign='middle'>" + dtContact.Rows[0]["EmailID"] + "</td>" +
                                                    " </tr>" +
                                                    "<tr>" +
                                                        "<td align='left' valign='middle'>Booking Date</td>" +
                                                        "<td align='left' valign='middle'>:</td>" +
                                                        "<td align='left' valign='middle'>" + DateTime.Today + "</td>" +
                                                    " </tr>" +

                                                "</table>" +
                                           " </td>" +
                                        " </tr>" +
                                    "</table>" +
                               " </td>" +
                                "<td width='10' align='left' valign='top'>&nbsp;</td>" +
                                "<td align='left' valign='top' style='background: #e6e6e6;'>" +

                                    "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                                        "<tr>" +
                                            "<td style='background: #ED8323; border-bottom: #FFF solid 1px; color: #FFF; padding: 5px 10px; font-size: 12px; font-weight: bold; font-family: Arial, Helvetica, sans-serif;'>Name of Passenger(s)</td>" +
                                        " </tr>" +
                                        "<tr>" +
                                            "<td style='padding: 10px;'>" +
                                                "<table width='100%' border='0' cellspacing='0' cellpadding='0'>");
        //Passenegr details
        foreach (DataRow drPax in dtPax.Rows)
        {
            sb.Append("<tr>" +
                        "<td height='30' align='left' valign='middle'>" +
                          "<img src='" + BLL.WebsiteStaticData.WebsiteUrl + "images/pax.png' width='24' height='24' />" +
                            " </td>" +
                             "<td width='5' height='30' align='left' valign='middle'>&nbsp;</td>" +
                             "<td height='30' align='left' valign='middle'>" + drPax["Title"] + " " + drPax["FName"] + " " + drPax["MName"] + " " + drPax["LName"] + "&nbsp;&nbsp;&nbsp;"+ drPax["DOB"].ToString().Replace("00:00:00", "") + " </td>" +
                               " </tr>");

        }


        sb.Append("</table>" +
          " </td>" +
       " </tr>" +
   "</table>" +
" </td>" +
" </tr>" +
"</table>" +
" </td>" +
" </tr>" +
"<tr>" +
"<td>&nbsp;</td>" +
" </tr>" +
"<tr>" +
"<td>" +
"<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
"<tr>" +
"<td width='32' align='left' valign='middle'>" +
   "<img src='" + BLL.WebsiteStaticData.WebsiteUrl + "images/airplane-ico.png' style='width:32px; height:32px;' />" +
" </td>" +
"<td width='10' align='left' valign='middle'>&nbsp;</td>" +
"<td align='left' valign='middle' style='font-size: 18px; color: #333333; font-weight: bold;'>Your flight itinerary</td>" +
//"<td valign='middle' style='font-size: 18px; color: #333333; font-weight: bold;'>" + lo.PayNow != "" ? "<a href='" + lo.PayNow + "'>Pay Now</a>" : "" + "</td>" +
"<td width='450' align='center' valign='middle' style='background: #FFF; border: #000 solid 0px; color: #F90; font-size: 18px; font-weight: bold; text-transform: uppercase; padding: 5px 10px;'>");
           if (lo.PayNow != "")
        {
            sb.Append(" <a href='" + lo.PayNow + "'>Pay Now</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Total Price : " + price);
                }
        else {
            //sb.Append(" &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Total Price : " + price);
            sb.Append(" <a href='" + lo.PayNow + "'>Pay Now</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Total Price : " + price);
        }
        sb.Append("</td>" +
            " </tr>" +
            "</table>" +
            " </td>" +
            " </tr>");
        if (dtComp.Rows[0]["CurrencyType"].ToString() == "GBP" || dtComp.Rows[0]["CurrencyType"].ToString() == "GBP " || dtComp.Rows[0]["CurrencyType"].ToString() == "EUR")
        {
           // CovidURL = "https://www.gov.uk/guidance/coronavirus-covid-19-declaration-form-for-international-travel#travel-declaration-form";
            sb.Append("<tr>" +
            "<td style='background: #eeeeee;'>" +
            // <!--Itinerary Details-->
            "<table class='table table-bordered' width='100%' cellpadding='5'>" +
            "<thead>" +
            "<tr>" +
              " <th align='left' valign='middle' style='background: #ED8323'>Date</th>" +
               "<th align='left' valign='middle' style='background: #ED8323'>Flight Number</th>" +
               "<th align='left' valign='middle' style='background: #ED8323'>Departing</th>" +
               "<th align='left' valign='middle' style='background: #ED8323'>Arriving</th>" +
               "<th align='left' valign='middle' style='background: #ED8323'>Cabin</th>" +
            " </tr>" +
            "</thead>" +
            "<tbody>");
            foreach (DataRow drFlight in dtSector.Rows)
            {
                sb.Append("<tr>" +
                      "<td align='left' valign='top' style='border-right: #FFF solid 1px; border-bottom: #FFF solid 1px;'>" +
                          "<strong>" + Convert.ToDateTime(drFlight["FromDateTime"]).ToString("ddd, dd MMM yyyyy") + "</strong><br />" +
                     " </td>" +
                      "<td align='left' valign='top' style='border-right: #FFF solid 1px; border-bottom: #FFF solid 1px;'>" +
                          "<p>" + drFlight["CarierName"] + drFlight["FlightNo"] + "</p>" +
                          drFlight["AirlineName"] +
                     " </td>" +
                      "<td align='left' valign='top' style='border-right: #FFF solid 1px; border-bottom: #FFF solid 1px;'>" +
                          "<strong>" + Convert.ToDateTime(drFlight["FromDateTime"]).ToString("H:mm") + ' ' + drFlight["FromDest"] + "</strong><br/>" +
                drFlight["FromDestName"] +
                          "<p>" +
                               drFlight["FromCityName"] +
                          "</p>" +
                     " </td>" +
                      "<td align='left' valign='top' style='border-bottom: #FFF solid 1px;'>" +
                          "<strong>" + Convert.ToDateTime(drFlight["ToDateTime"]).ToString("H:mm") + ' ' + drFlight["ToDest"] + "</strong><br/>" +
                           drFlight["ToDestName"] +
                          "<p>" +

                              drFlight["ToCityName"] +

                          "</p>" +
                     " </td>" +
                      "<td align='left' valign='top' style='border-right: #FFF solid 1px; border-bottom: #FFF solid 1px;'>" +
                          "<strong>" + Common.GetCabinClassChangeValue(Convert.ToString(drFlight["CabinClass"])) + "</strong><br />" +
                     " </td>" +
                  " </tr>");
            }

            sb.Append("</tbody>" +
           "</table>" +
      " </td>" +
    " </tr>");
        }
        else
        {
            sb.Append(txtOverviews.Text);
        }
        sb.Append("<tr>" +
           "<td style='background: #eeeeee;'>");

        sb.Append("" +
       //     "<td>&nbsp;</td>" +
       // " </tr>" +
       // "<tr style='border-bottom: 1pt solid black;'>" +
       //     "<td></td>" +
       // " </tr>" +
       //"<tr>" +
       //     "<td>" + note + "<br/><br/><br/></td>" +
       // " </tr>" +
       
       // "<tr style='border-bottom: 1pt solid black;'>" +
       //     "<td></td>" +
       // " </tr>" +

        authorizeTravelPerformance+


        "<tr>" +
            "<td><br/></td>" +
        " </tr>" +
        "<tr>" +
            "<td><br/></td>" +
        " </tr>" +
        "<tr>" +
            "<td><br/></td>" +
        " </tr>" +
        "<tr>" +
            "<td style='background-color:#feffbf; padding:10px;'>");
        //Add If-Else condition as per Geogry
        if (dtComp.Rows[0]["CurrencyType"].ToString().Trim() == "CAD")
        {
           sb.Append(termconditionCA());
        }
        else if (dtComp.Rows[0]["CurrencyType"].ToString().Trim() == "USD")
        {
            sb.Append(termconditionUSANEW());
        }
        else if(dtComp.Rows[0]["CurrencyType"].ToString().Trim() == "EUR")
        {
            sb.Append(termconditionEU());
        }
        else
        {
            //sb.Append("<tr>" +
            //        "<td style='padding: 10px 0px;'>" +
            //        "<a href='"+CovidURL+"'>"+ CovidURL + "</a>"+
            //                " </td></tr>");
            sb.Append(termcondition());
        }
        
                   sb.Append("</td>" +
                " </tr>" +
                "<tr>" +
                    "<td style='padding: 10px 0px;'>" +
                       lo.xpFooter +
                            " </tr>" +
                        "</table>" +
                   " </td>" +
                " </tr>" +
            "</table>" +
        " </td>" +
        " </tr>" +
        "</table>");

                dvXP.Visible = true;
                return sb.ToString();


    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        
        XPDetails = BookingDetails(txtInvoice.Text.Trim());
        mailServices.DataServiceSoapClient objmail = new DataServiceSoapClient();
        
        if (objmail.Sendcustomermail(txtXPFrom.Text.Trim(), txtXPTo.Text.Trim(), "Flight Confirmation", XPDetails, txtXPFrom.Text.Trim(), "dev@traveljunction.co.uk") == true)
        {
          
            ltrMsg.Text = "Quotation Sent Successfully.";

            GetSetDatabase objGetSetDatabase = new GetSetDatabase();
            objGetSetDatabase.SET_Booking_Detail(txtInvoice.Text.Trim(), "001", "", "", "", "", "", "", "", "Flight Quotation sent to " + txtXPTo.Text.Trim() + " with total price =" + txtPrice.Text, "", "", "", "", "", objUserDetail.userID, "", "", "Update","","");
           
           
        }
       
    }
    protected void btnSendXP_Click(object sender, EventArgs e)
    {
        try
        {
            string abc = txtOverviews.Text;
        }
        catch { }
    }


    #region Generate Invoice for Booking

    public void BookingInvoiceDetails(string XP)
    {
        dtPax = obj.GET_Passenger_Detail(XP, "001");
        dtSector = obj.GET_Sector_Detail(XP, "001");
        dtComp = obj.GET_Booking_Master(XP);
        dtBD = obj.GET_Booking_Detail1(XP, "001", "", "", "", "", "", "", "", "");
        dtContact = obj.GET_Contact_Detail(XP, "001");
        dtPrice = obj.GET_Amount_Charges_Detail(XP, "001", "", "", "", "", "", "", "");
        dtTrans = obj.GET_Transaction_Master(XP, "");
        obj.GET_Transaction_Details(XP, "");
    }

    private string generate_Invoice(string xp)
    {
        double AtolCharges = dtPax.Rows.Count * 2.5;
        string sectorStatus = "Confirm";
        string tran = string.Empty;
        double subTotal = 0;
        double tranTotal = 0;
        string heading = string.Empty;

        if (dtComp.Rows[0]["InvoiceNo"].ToString() != "")
        { heading = "Confirmation Invoice"; }
        else
        { heading = "Booking Confirmation"; }
        if (dtBD.Rows.Count > 0)
        {
            //if (dtBD.Rows[0]["ATOL_Type"].ToString().ToLower() == "flights only/public bonded")
            //{ AtolCharges = dtPax.Rows.Count * 2.5; }
            //else { AtolCharges = 0; }
            AtolCharges = dtPax.Rows.Count * 2.5;
        }
        foreach (DataRow drprc in dtPrice.Rows)
        {
            subTotal += (Convert.ToDouble(drprc["SellPrice"]) * Convert.ToInt32(drprc["NoOfPax"]));
        }
        if (dtTrans.Rows.Count > 0)
        {
            tranTotal = Convert.ToDouble(dtTrans.Compute("Sum(TrnsAmount)", ""));
        }
        CompanyDetails objc = lo.SetCompanyDetail(dtComp.Rows[0]["BookingByCompany"].ToString());

        if (dtBD.Rows[0]["BookingStatus"].ToString().ToLower() == "refund" || dtBD.Rows[0]["BookingStatus"].ToString().ToLower() == "cancelled")
        {
            heading = "Cancellation Invoice";
            subTotal = 0.00;
            AtolCharges = 0.00;
            tranTotal = 0.00;
            sectorStatus = "Cancelled";

        }
        #region
        string inv = "<table width='1000' border='0' align='center' cellpadding='0' cellspacing='0' style='font-family:Tahoma, Arial, Helvetica, sans-serif;'>" +
  "<tr>" +
    "<td style='padding:30px;'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
        "<tr>" +
          "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
              "<tr>" +
                "<td align='left' valign='top'><img src='https://www.flightsandholidays.biz/images/logoes/" + objc.Comp_logo + ".png' width='251' height='79' alt='Logo' /></td>" +
                "<td align='right' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + objc.Comp_Address + "<br />" +
                  "Tel :" + objc.Comp_contact + "<br/>" +
                   objc.Comp_Emailid + " <br/>" +
                  "www." + objc.Comp_Emailid.Split('@')[1].ToString() + "</td>" +
              "</tr>" +
            "</table></td>" +
        "</tr>" +
        "<tr>" +
          "<td align='left' valign='top' style='font-size:24px; text-align:center; border-bottom:#666 solid 1px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + heading + "</td>" +
        "</tr>" +
        "<tr>" +
          "<td align='left' valign='top'>&nbsp;</td>" +
        "</tr>" +
        "<tr>" +
          "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
              "<tr>" +
                "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                    "<tr>" +
                      "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                          "<tr>" +
                            "<td style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Invoice To :</td>" +
                          "</tr>" +
                          "<tr>" +
                            "<td style='font-size:13px; font-weight:bold; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>" + dtPax.Rows[0]["Title"] + " " + dtPax.Rows[0]["FName"] + " " + dtPax.Rows[0]["LName"] + "</strong></td>" +
                          "</tr>" +
                          "<tr>" +
                            "<td>" + dtContact.Rows[0]["PAddress"] + " " + dtContact.Rows[0]["City"] + " " + dtContact.Rows[0]["PostCode"] + "</td>" +
                          "</tr>" +
                          "<tr>" +
                            "<td>" + dtContact.Rows[0]["Country"] + "<br/></td>" +
                          "</tr>" +
                           "<tr>" +
                            "<td style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>E-Mail &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp:&nbsp" + dtContact.Rows[0]["EmailID"] + "</td>" +
                          "</tr>" +
                           "<tr>" +
                            "<td style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Contact Number : " + dtContact.Rows[0]["MobileNo"] + "</td>" +
                          "</tr>" +
                        "</table></td>" +
                      "<td align='left' valign='top'>&nbsp;</td>" +
                      "<td align='left' valign='top' style='background:#2e4b6b; padding:10px;'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                          "<tr>" +
                          "<td height='21' align='left' valign='top' style='color:#c5d100; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Booking Ref</strong></td>" +

                            "<td height='21' align='left' valign='top' style='color:#c5d100; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Invoice No</strong></td>" +
                            "<td align='left' valign='top' style='color:#c5d100; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Invoice Date</strong></td>" +
                            "<td align='left' valign='top' style='color:#c5d100; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>PNR</strong></td>" +
                          "</tr>" +
        "<tr>" +
                          "<td align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + xp + "</td>" +
                            "<td align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + dtComp.Rows[0]["InvoiceNo"].ToString() + "</td>";
        string invoicedate = string.Empty;
        if (dtComp.Rows[0]["Invoice_Date"].ToString() != "")
        {
            invoicedate = Convert.ToDateTime(dtComp.Rows[0]["Invoice_Date"].ToString()).ToString("dd MMM yyyy");
        }

        inv += "<td align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + invoicedate + "</td>";
        inv += "<td align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + dtBD.Rows[0]["PNR"] + "</td>" +
        "<td align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'></td>" +
                  "</tr>" +
                "</table></td>" +
            "</tr>" +
          "</table></td>" +
      "</tr>" +
      "<tr>" +
        "<td align='left' valign='top'>&nbsp;</td>" +
      "</tr>" +

      "<tr>" +
        "<td align='left' valign='top' style='padding-bottom:10px; padding-top:10px; font-size:13px; font-weight:600; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Flight Details :</strong></td>" +
      "</tr>";
        //"<tr>" +
        //  "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
        //      "<tr>" +
        //        "<td align='left' valign='top' style='background:#2e4b6b; color:#FFF; padding:5px 5px; border-bottom:#000 solid 5px;'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
        //            "<tr>" +
        //              "<td width='200' align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>From</td>" +
        //              "<td width='200' align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>To</td>" +
        //              "<td width='120' align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Dep.Date</td>" +
        //              //"<td width='80' align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Class</td>" +
        //              "<td width='100' align='center' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Flight No</td>" +
        //              "<td align='center' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Dep.Time</td>" +
        //              "<td align='center' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Arvl.Time</td>" +
        //              "<td align='center' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Cabin</td>" +
        //              "<td align='center' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Status</td>" +
        //            "</tr>" +
        //          "</table></td>" +
        //      "</tr>";
        //foreach (DataRow drFlight in dtSector.Rows)
        //{
        //    inv += "<tr>" +
        //          "<td align='left' valign='top' style='background:#e3e3e3; padding:5px 5px;'>" +
        //              "<table width='100%' border='0' cellspacing='0' cellpadding='0' style='margin-bottom:10px; border-bottom:#cbcbcb solid 1px; padding-bottom:10px;'>" +
        //                  "<tr>" +
        //                      "<td width='200' align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" +
        //                          "<strong>" + drFlight["FromCityName"] + "</strong><br />" +
        //                          "" + drFlight["FromDestName"] + " (" + drFlight["FromDest"] + ")" +
        //                      "</td>" +
        //                      "<td width='200' align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" +
        //                          "<strong>" + drFlight["ToCityName"] + "</strong><br />" +
        //                          drFlight["ToDestName"] + " (" + drFlight["ToDest"] + ")" +
        //                      "</td>" +
        //                      "<td width='120' align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + Convert.ToDateTime(drFlight["FromDateTime"]).ToString("ddd, dd MMM yy") + "</td>" +
        //                      //"<td width='80' align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + drFlight["FClass"] +"</td>" +
        //                      "<td width='100' align='center' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + drFlight["CarierName"] + drFlight["FlightNo"] + "</td>" +
        //                      "<td align='center' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;width: 90px;'>" + Convert.ToDateTime(drFlight["FromDateTime"]).ToString("HH:mm") + "</td>" +
        //                      "<td align='center' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;width: 66px;'>" + Convert.ToDateTime(drFlight["ToDateTime"]).ToString("HH:mm") + "</td>" +
        //                      "<td align='right' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;width: 90px;'>" + Common.GetCabinClassChangeValue(Convert.ToString(drFlight["CabinClass"])) + "</td>" +
        //                      "<td align='right' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;width: 60px;'>" + sectorStatus + "</td>" +
        //                  "</tr>" +
        //              "</table>" +
        //          "</td>" +
        //      "</tr>";
        //}
        inv += "<tr>" +
              "<td align='left' valign='top' style='background:#fff; color:#000; padding:5px 5px; border-bottom:#000 solid 5px;'>" +
              txtOverviews.Text+
                "</td>" +
            "</tr>";
       
        inv += "</table>" +

                        "</td>" +
                    "</tr>" +
                    "<tr>" +
                      "<td align='left' valign='top'>&nbsp;</td>" +
                    "</tr>" +
                  "</table></td>" +
              "</tr>" +
              "<tr>" +
                "<td align='left' valign='top' style='padding-bottom:10px; padding-top:10px; font-size:13px; font-weight:600; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Passenger & Ticket Details :</strong></td>" +
              "</tr>" +
              "<tr>" +
                "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                    "<tr>" +
                      "<td align='left' valign='top' style='background:#2e4b6b; color:#FFF; padding:5px 5px; border-bottom:#000 solid 5px;'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                          "<tr>" +
                            "<td width='25%' align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>First Name</td>" +
                            "<td width='25%'  align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Last Name</td>" +
                            "<td width='25%' align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Ticket No</td>" +
                            "<td align='right' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'></td>" +
                          "</tr>" +
                        "</table></td>" +
                    "</tr>" +
                    "<tr>" +
                      "<td align='left' valign='top' style='background:#e3e3e3; padding:8px 5px;'><table width='100%' border='0' cellspacing='0' cellpadding='0' style='margin-bottom:10px; border-bottom:#cbcbcb solid 1px; padding-bottom:10px;'>" +
                          "<tr>" +
                            "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0' style='margin-bottom:10px; padding-bottom:10px;'>";
        foreach (DataRow drPax in dtPax.Rows)
        {

            inv += "<tr>" +
                                  "<td width='25%' align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif; padding-bottom:10px;'>" + drPax["Title"] + " " + drPax["FName"] + "</td>" +
                                  "<td width='25%'  align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif; padding-bottom:10px;'>  " + drPax["LName"] + "</td>" +
                                  "<td width='25%' align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif; padding-bottom:10px;'> " + drPax["Tickets"] + "</td>" +
                                  "<td align='right' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif; padding-bottom:10px;'></td>" +
                                "</tr>";
        }
        inv += "</table></td>" +
                          "</tr>" +
                        "</table></td>" +
                    "</tr>" +

                     "<tr>" +
                "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                    "<tr>" +
                      "<td width='70%' align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
  "<tr>" +
    "<td>&nbsp;</td>" +
  "</tr>" +
  "<tr>" +
    "<td align='left' valign='top' style='font-size:13px; font-weight:600; font-family:Tahoma, Arial, Helvetica, sans-serif; border-bottom:#333 solid 1px; padding-bottom:5px;'><strong style='background:#2e4b6b; color:#FFF; padding:5px 10px;'>Transaction Details :</strong></td>" +
  "</tr>" +
  "<tr>" +
    "<td>&nbsp;</td>" +
  "</tr>" +
  "<tr>" +
    "<td><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                          "<tr>" +
                            "<td height='35' align='left' valign='top' style='font-size:15px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Transaction ID</strong></td>" +
                            "<td height='35' align='left' valign='top' style='font-size:15px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Date</strong></td>" +
                            //"<td height='35' align='left' valign='top' style='font-size:15px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Card Type</strong></td>" +
                            //"<td height='35' align='left' valign='top' style='font-size:15px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Card Holder</strong></td>" +
                            "<td height='35' align='left' valign='top' style='font-size:15px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Amount</strong></td>" +
                          "</tr>";
        if (dtBD.Rows[0]["BookingStatus"].ToString().ToLower() != "refund" && dtBD.Rows[0]["BookingStatus"].ToString().ToLower() != "cancelled")
        {
            foreach (DataRow drTrans in dtTrans.Rows)
            {
                tran += "<tr>" +
              "<td height='35' align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + drTrans["TrnsNo"] + "</td>" +
                 "<td height='35' align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + Convert.ToDateTime(drTrans["TrnsDateTime"]).ToString("dd MMM yyyy HH:mm") + "</td>" +
                 //"<td height='35' align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + drTrans["Card_Type"] + "</td>" +
                 //"<td height='35' align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + drTrans["Holder_Name"] + "</td>" +
                 "<td height='35' align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + drTrans["TrnsCurrencyType"] + Convert.ToDouble(drTrans["TrnsAmount"]).ToString("f2") + "</td>" +
               "</tr>";
            }
        }
        inv += tran;
        inv += "</table></td>" +
     "</tr>" +
   "</table>" +
   "</td>" +
        #endregion
        #region
 "<td width='3%'>&nbsp;</td>" +
                         "<td style='background:#e3e3e3; padding:8px 5px;'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                             "<tr>" +
                               "<td height='35' align='right' style='font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Sub-Total</td>" +
                               "<td height='35' align='right' style='font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>:</td>" +
                               "<td height='35' align='right' style='font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + Convert.ToDouble(subTotal - AtolCharges).ToString("f2") + "</td>" +
                             "</tr>" +
                             "<tr>" +
                               "<td height='35' align='right' style='font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Other Charges</td>" +
                               "<td height='35' align='right' style='font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>:</td>" +
                               "<td height='35' align='right' style='font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>0.00</td>" +
                             "</tr>" +
                             "<tr>" +
                               "<td height='35' align='right' style='font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>ATOL Package</td>" +
                               "<td height='35' align='right' style='font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>:</td>" +
                               "<td height='35' align='right' style='font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>0.00</td>" +
                             "</tr>" +
                             "<tr>" +
                               "<td height='35' align='right' style='font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>ATOL Charges</td>" +
                               "<td height='35' align='right' style='font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>:</td>" +
                               "<td height='35' align='right' style='font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + AtolCharges.ToString("f2") + "</td>" +
                             "</tr>" +
                             "<tr>" +
                               "<td height='35' align='right' style='border-bottom:#666 double 3px; font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Net Invoice Amount</td>" +
                               "<td height='35' align='right' style='border-bottom:#666 double 3px; font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>:</td>" +
                               "<td height='35' align='right' style='border-bottom:#666 double 3px; font-size:15px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + Convert.ToDouble(subTotal).ToString("f2") + "</td>" +
                             "</tr>" +
                             "<tr>" +
                               "<td height='35' align='right' style='border-bottom:#666 solid 1px; font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Paid Amount</td>" +
                               "<td height='35' align='right' style='border-bottom:#666 solid 1px; font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>:</td>" +
                               "<td height='35' align='right' style='border-bottom:#666 solid 1px; font-size:15px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + tranTotal.ToString("f2") + "</td>" +
                             "</tr>" +
                             "<tr>" +
                               "<td height='35' align='right' style='border-bottom:#666 double 3px; font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Due Amount</strong></td>" +
                               "<td height='35' align='right' style='border-bottom:#666 double 3px; font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>:</td>" +
                               "<td height='35' align='right' style='border-bottom:#666 double 3px; font-size:15px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>" + Convert.ToDouble(subTotal - tranTotal).ToString("f2") + "</strong></td>" +
                             "</tr>" +
                         "</table></td>" +
                       "</tr>" +
                     "</table></td>" +
                 "</tr>" +

                 "<tr>" +
                   "<td align='left' valign='top'>&nbsp;</td>" +
                 "</tr>" +
                 "<tr>" +
                   "<td align='left' valign='top'></td>" +
                 "</tr>" +
                 "<tr>" +
                   "<td align='left' valign='top' style='color:#F00; padding:10px 0px; font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>IMPORTANT NOTICE</td>" +
                 "</tr>" +
                 "<tr>" +

                   "<td align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>We would like to draw your attention to the booking conditions of the travel provider." +
                     " You have informed us you have adequate travel insurance cover for this booking." +
                     " Please note that it may be a legal requirement on your part to have such cover in force.</td> " +
                 "</tr>" +

               "</table></td>" +
           "</tr>" +
           "<tr>" +
             "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                 "<tr>" +
                   "<td align='center' valign='top' style='border-top:#666 solid 1px; padding-top:20px; font-size:11px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>YOUR FINANCIAL PROTECTION</strong></td>" +
                 "</tr>" +
                 "<tr>" +
                   "<td align='center' valign='top' style='font-size:11px; font-family:Tahoma, Arial, Helvetica, sans-serif; padding:10px 0px;'>When you buy an ATOL protected flight or flight inclusive holiday from us you will receive an ATOL certificate. This lists what is financially protected, where" +
                     " you can get information on what this means for you and who to contact if things go wrong. Our ATOL number is " + objc.Comp_AtolNumber + "  for more information see our" +
                     " booking teams and conditions. Cancellation rules: NON REFUNDABLE. Please read the confirmation invoice carefully.</td>" +
                 "</tr>" +
                 "<tr>" +
                   "<td align='center' valign='top' style='font-size:11px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>www." + objc.Comp_Emailid.Split('@')[1].ToString() + " is a trading name of " + objc.Comp_Name + ". Company Reg:" + objc.Comp_RegNumber + "</strong></td>" +
                 "</tr>" +
                 "<tr>" +
                   "<td align='right' valign='top' style='font-size:13px; font-weight:bold; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Prepared by " + objUserDetail.userID.ToUpper() + "</td>" +
                 "</tr>" +
               "</table></td>" +
           "</tr>" +
         "</table></td>" +
     "</tr>" +
   "</table>";
        #endregion

        return inv;

    }

    private void generate_Invoice_Pdf(string html, string xp)
    {

        // create the HTML to PDF converter
        HtmlToPdf htmlToPdfConverter = new HtmlToPdf();


        //// set browser width
        htmlToPdfConverter.BrowserWidth = int.Parse("793");

        //// set browser height if specified, otherwise use the default
        //if (textBoxBrowserHeight.Text.Length > 0)
        //    htmlToPdfConverter.BrowserHeight = int.Parse(textBoxBrowserHeight.Text);

        //// set HTML Load timeout
        //htmlToPdfConverter.HtmlLoadedTimeout = int.Parse(textBoxLoadHtmlTimeout.Text);

        // set PDF page size and orientation
        htmlToPdfConverter.Document.FitPageWidth = false;
        htmlToPdfConverter.Document.PageSize = GetSelectedPageSize();
        htmlToPdfConverter.Document.PageOrientation = PdfPageOrientation.Portrait;

        // set the PDF standard used by the document
        //htmlToPdfConverter.Document. =  PdfStandard.PdfA;

        // set PDF page margins
        htmlToPdfConverter.Document.Margins = new PdfMargins(5);


        // set a wait time before starting the conversion
        htmlToPdfConverter.WaitBeforeConvert = 2;

        // convert HTML to PDF
        byte[] pdfBuffer = null;


        // convert HTML code
        string htmlCode = html;
        string baseUrl = "";

        // convert HTML code to a PDF memory buffer
        pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(htmlCode, baseUrl);


        // inform the browser about the binary data format
        HttpContext.Current.Response.AddHeader("Content-Type", "application/pdf");

        // let the browser know how to open the PDF document, attachment or inline, and the file name
        HttpContext.Current.Response.AddHeader("Content-Disposition", String.Format("{0}; filename={2}.pdf; size={1}",
             "attachment", pdfBuffer.Length.ToString(), xp));

        // write the PDF buffer to HTTP response
        HttpContext.Current.Response.BinaryWrite(pdfBuffer);

        // call End() method of HTTP response to stop ASP.NET page processing
        HttpContext.Current.Response.End();

    }

    private PdfPageSize GetSelectedPageSize()
    {
        switch ("A3")
        {
            case "A0":
                return PdfPageSize.A0;
            case "A1":
                return PdfPageSize.A1;
            case "A10":
                return PdfPageSize.A10;
            case "A2":
                return PdfPageSize.A2;
            case "A3":
                return PdfPageSize.A3;
            case "A4":
                return PdfPageSize.A4;
            case "A5":
                return PdfPageSize.A5;
            case "A6":
                return PdfPageSize.A6;
            case "A7":
                return PdfPageSize.A7;
            case "A8":
                return PdfPageSize.A8;
            case "A9":
                return PdfPageSize.A9;
            case "ArchA":
                return PdfPageSize.ArchA;
            case "ArchB":
                return PdfPageSize.ArchB;
            case "ArchC":
                return PdfPageSize.ArchC;
            case "ArchD":
                return PdfPageSize.ArchD;
            case "ArchE":
                return PdfPageSize.ArchE;
            case "B0":
                return PdfPageSize.B0;
            case "B1":
                return PdfPageSize.B1;
            case "B2":
                return PdfPageSize.B2;
            case "B3":
                return PdfPageSize.B3;
            case "B4":
                return PdfPageSize.B4;
            case "B5":
                return PdfPageSize.B5;
            case "Flsa":
                return PdfPageSize.Flsa;
            case "HalfLetter":
                return PdfPageSize.HalfLetter;
            case "Ledger":
                return PdfPageSize.Ledger;
            case "Legal":
                return PdfPageSize.Legal;
            case "Letter":
                return PdfPageSize.Letter;
            case "Letter11x17":
                return PdfPageSize.Letter11x17;
            case "Note":
                return PdfPageSize.Note;
            default:
                return PdfPageSize.A4;
        }
    }




    private void generate_etickets_Pdf(string html, string xp)
    {

        // create the HTML to PDF converter
        HtmlToPdf htmlToPdfConverter = new HtmlToPdf();
        //// set browser width
        htmlToPdfConverter.BrowserWidth = int.Parse("793");
        // set PDF page size and orientation
        htmlToPdfConverter.Document.FitPageWidth = false;
        htmlToPdfConverter.Document.PageSize = GetSelectedPageSize();
        htmlToPdfConverter.Document.PageOrientation = PdfPageOrientation.Portrait;

        // set PDF page margins
        htmlToPdfConverter.Document.Margins = new PdfMargins(5);

        // set a wait time before starting the conversion
        htmlToPdfConverter.WaitBeforeConvert = 2;

        // convert HTML to PDF
        byte[] pdfBuffer = null;

        // convert HTML code
        string htmlCode = html;
        string baseUrl = "";

        // convert HTML code to a PDF memory buffer
        pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(htmlCode, baseUrl);

        System.IO.MemoryStream ms = new System.IO.MemoryStream(pdfBuffer);

        ms.Position = 0;

        System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf);
        System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(ms, ct);
        attach.ContentDisposition.FileName = "eticket.pdf";
        msg.Attachments.Add(attach);
        

    }
    #endregion
    private bool SendMail(string subject, string mailbody)
    {
        try
        {

            msg.From = new MailAddress(txtXPFrom.Text.Trim());
            msg.To.Add(new MailAddress(txtXPTo.Text.Trim()));
            msg.CC.Add(new MailAddress(txtXPFrom.Text.Trim()));

            msg.Subject = subject;
            msg.Body = mailbody;
            msg.IsBodyHtml = true;

            string fromAddress = txtXPFrom.Text;

            if (fromAddress.ToLower().Contains("@traveljunction.co.uk"))
            {
                SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["HostT"]);
                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["PortT"]);
                smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["UserNameT"], ConfigurationManager.AppSettings["PasswordT"]);
                smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSslT"]);
                smtp.Send(msg);
            }
            else if (fromAddress.ToLower().Contains("@flighttrotters.co.uk"))
            {
                SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["HostFLTUK"]);
                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["PortFLTUK"]);
                smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["UserNameFLTUK"], ConfigurationManager.AppSettings["PasswordFLTUK"]);
                smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSslFLTUK"]);
                smtp.Send(msg);
            }
            else if (fromAddress.ToLower().Contains("@flighttrotters.ca"))
            {
                SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["HostFTCA"]);
                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["PortFTCA"]);
                smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["UserNameFTCA"], ConfigurationManager.AppSettings["PasswordFTCA"]);
                smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSslFTCA"]);
                smtp.Send(msg);
            }
            else if (fromAddress.ToLower().Contains("@click2book.ca"))
            {
                SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["HostC2BCA"]);
                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["PortC2BCA"]);
                smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["UserNameC2BCA"], ConfigurationManager.AppSettings["PasswordC2BCA"]);
                smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSslC2BCA"]);
                smtp.Send(msg);
            }
            else
            {
                try
                {
                    SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["Host"]);
                    smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["UserName"], ConfigurationManager.AppSettings["Password"]);
                    smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
                    smtp.Send(msg);
                }
                catch
                {
                    SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["Host"]);
                    smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["UserName1"], ConfigurationManager.AppSettings["Password"]);
                    smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
                    smtp.Send(msg);
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }

    }
    protected void btnDownloadInvoice_Click(object sender, EventArgs e)
    {
        string xp = Request.QueryString.Get("BID");

        BookingInvoiceDetails(xp);
        generate_Invoice_Pdf(generate_Invoice(xp), xp);
    }

    protected void btneticket_Click(object sender, EventArgs e)
    {
        string xp = Request.QueryString.Get("BID");

        BookingInvoiceDetails(xp);
        generate_etickets_Pdf(generate_Invoice(xp), xp);
        ltrMsg.Text= SendMail("E-Ticket : " + xp, txtNote.Text)==true?"E Ticket send successfully.":"Unable to send e-ticket.";
        XPDetails = BookingDetails(xp);

    }

    protected void btnPdf_Click(object sender, EventArgs e)
    {
        XPDetails = BookingDetails(txtInvoice.Text.Trim());
        BookingDetail BOD = new BookingDetail();
        BOD.generate_Pdf(XPDetails, txtInvoice.Text);
    }
}