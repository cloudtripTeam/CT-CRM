<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Scripts/ion.calendar.css" rel="stylesheet" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
    <script src="Scripts/moment.js"></script>

    <script src="Scripts/ion.calendar.js"></script>
</head>
<body>
    <table width='1000' border='0' align='center' cellpadding='0' cellspacing='0' style='font-family: Tahoma, Arial, Helvetica, sans-serif;'>
        <tr>
            <td style='padding: 30px;'>
                <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                    <tr>
                        <td align='left' valign='top'>
                            <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                <tr>
                                    <td align='left' valign='top'>
                                        <img src='https://www.flightsandholidays.biz/images/logoes/TRVJUNCTION.png' width='251' height='79' alt='Logo' /></td>
                                    <td align='right' valign='top' style='font-size: 12px; font-family: Tahoma, Arial, Helvetica, sans-serif;'>Suite B2:11 Vista Centre Salisbury Road, Hounslow TW4 6JQ United Kingdom<br />
                                        Tel :0207 183 1571<br />
                                        info@traveljunction.co.uk
                                        <br />
                                        www.traveljunction.co.uk</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align='left' valign='top' style='font-size: 24px; text-align: center; border-bottom: #666 solid 1px; font-family: Tahoma, Arial, Helvetica, sans-serif;'>Confirmation Invoice</td>
                    </tr>
                    <tr>
                        <td align='left' valign='top'>&nbsp;</td>
                    </tr>
                    <tr>
                        <td align='left' valign='top'>
                            <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                <tr>
                                    <td align='left' valign='top'>
                                        <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                            
                                                <tr>
                                                    <td align='left' valign='top'>
                                                        
                                                    </td>
                                                    <td align='left' valign='top'>&nbsp;</td>
                                                    <td align='left' valign='top' style='background: #2e4b6b; padding: 10px;'>
                                                        <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                                            <tr>
                                                                <td height='21' align='left' valign='top' style='color: #c5d100; font-size: 12px; font-family: Tahoma, Arial, Helvetica, sans-serif;'><strong>Booking Ref</strong></td>
                                                                <td height='21' align='left' valign='top' style='color: #c5d100; font-size: 12px; font-family: Tahoma, Arial, Helvetica, sans-serif;'><strong>Invoice No</strong></td>
                                                                <td align='left' valign='top' style='color: #c5d100; font-size: 12px; font-family: Tahoma, Arial, Helvetica, sans-serif;'><strong>Invoice Date</strong></td>
                                                                <td align='left' valign='top' style='color: #c5d100; font-size: 12px; font-family: Tahoma, Arial, Helvetica, sans-serif;'><strong>PNR</strong></td>
                                                                <td align='left' valign='top' style='color: #c5d100; font-size: 12px; font-family: Tahoma, Arial, Helvetica, sans-serif;'><strong></strong></td>
                                                            </tr>
                                                        05 Sep 2018</td>
                                                    <td align='left' valign='top' style='color: #FFF; font-size: 12px; font-family: Tahoma, Arial, Helvetica, sans-serif;'>Z2XGJY</td>
                                                    <td align='left' valign='top' style='color: #FFF; font-size: 12px; font-family: Tahoma, Arial, Helvetica, sans-serif;'></td>
                                                </tr>
                                            </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align='left' valign='top'>&nbsp;</td>
                    </tr>
                    <tr>
                        <td align='left' valign='top' style='padding-bottom: 10px; padding-top: 10px; font-size: 13px; font-weight: 600; font-family: Tahoma, Arial, Helvetica, sans-serif;'><strong>Flight Details :</strong></td>
                    </tr>
                    <tr>
                        <td align='left' valign='top'>
                            <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                <tr>
                                    <td align='left' valign='top' style='background: #2e4b6b; color: #FFF; padding: 5px 5px; border-bottom: #000 solid 5px;'>
                                        <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                            <tr>
                                                <td width='200' align='left' valign='top' style='color: #FFF; font-size: 12px; font-family: Tahoma, Arial, Helvetica, sans-serif;'>From</td>
                                                <td width='200' align='left' valign='top' style='color: #FFF; font-size: 12px; font-family: Tahoma, Arial, Helvetica, sans-serif;'>To</td>
                                                <td width='120' align='left' valign='top' style='color: #FFF; font-size: 12px; font-family: Tahoma, Arial, Helvetica, sans-serif;'>Dep.Date</td>
                                                <td width='100' align='center' valign='top' style='color: #FFF; font-size: 12px; font-family: Tahoma, Arial, Helvetica, sans-serif;'>Flight No</td>
                                                <td align='center' valign='top' style='color: #FFF; font-size: 12px; font-family: Tahoma, Arial, Helvetica, sans-serif;'>Dep.Time</td>
                                                <td align='left' valign='top' style='color: #FFF; font-size: 12px; font-family: Tahoma, Arial, Helvetica, sans-serif;'>Arvl.Time</td>
                                                <td align='left' valign='top' style='color: #FFF; font-size: 12px; font-family: Tahoma, Arial, Helvetica, sans-serif;'>Status</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
</body>
</html>
