<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="Failure.aspx.cs" Inherits="Admin_Payment_emerchant_Failure" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <link rel="stylesheet" type="text/css" href="content/css/result-page.css" />

    <section class="flight-details-bg page-banner" style="top: -350px; margin-bottom: -350px;">
        <div class="container">
            <div class="page-title-wrapper">
                <div class="page-title-content">

                    <h2 class="captions">Transaction Failed</h2>
                </div>
            </div>
        </div>
    </section>
    <div class="clearfix"></div>

<section>
        <div class="container p-30">
            <div class="pay-now-outer">
                <h3 class="text-center">Transaction Failed</h3>
            <div class="pay-now-box">

                <div class="pay-now-inner"><p></p>
                    <h3>Sorry</h3>
                    <p>Your Transaction could not be successful please contact us on </p>
                    <h1><a href="tel:<%= BLL.WebsiteStaticData.ContactNo1 %>"><%= BLL.WebsiteStaticData.ContactNo1 %></a></h1>
                    <p>for further information.</p>

                </div>
            </div>
                </div>
        </div>

    </section>
</asp:Content>

