<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="ChargeMail.aspx.cs" Inherits="Admin_Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script src="../../js/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../js/jquery-ui.js"></script>
    <link href="../../css/jquery.my-message.1.1.min.css" rel="stylesheet" />
    <link href="../../css/StyleSheet.css" rel="stylesheet" />
    <script src="../../js/CalendarAnyYear.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style>
        
    .fl1-table tbody tr:nth-child(odd) {
         background-color: #4fc3a1;
            color: #fff;
    }
     .fl1-table tbody tr:first-child td {
        background-color: #adf0ff;
         height: 0px;
    }
    .fl1-table tr:nth-child(even) {
        background: transparent;
    }
    .fl1-table tr td:nth-child(odd) {
        
        border-right: 1px solid #E6E4E4;
    }
    .fl1-table tr td:nth-child(even) {
        border-right: 1px solid #E6E4E4;
    }
   
        .fl1-table {
    border-radius: 5px;
    font-size: 12px;
    font-weight: normal;
    border: none;
    border-collapse: collapse;
    width: 100%;
    max-width: 100%;
    white-space: nowrap;
    background-color: white;
}
        .fl1-table thead th {
    color: #ffffff;
    background: #4FC3A1;
}


.fl1-table thead th:nth-child(odd) {
    color: #ffffff;
    background: #00366c;
   
}

.fl1-table tr:nth-child(even) {
    background: #F8F8F8;
}
        .blink_me
        {
            animation: blinker 1s linear infinite;
        }
        @keyframes blinker 
        {
            50% {
                opacity: 0;
            }
        }
         .blink_me tr
        {
            animation: blinker 1s linear infinite;
           color:red;
        }
        @keyframes blinker 
        {
            50% {
                opacity: 0;
            }
        }

          .blink_me_charge
        {
            animation: blinker 1s linear infinite;
             color:red;
             font-size:14px;
             background:rgba(253, 174, 2, 0.48);
             border-radius:25px;
             padding:5px 10px;
             text-align:center;
             font-weight:bold;
        }
        @keyframes blinker 
        {
            50% {
                opacity: 0;
            }
        }
         .blink_me_charge tr
        {
            animation: blinker 1s linear infinite;
          
        }
        @keyframes blinker 
        {
            50% {
                opacity: 0;
            }
        }
      

        .fl1-table tbody tr:nth-child(odd) {
        background: none;
    }
     .fl1-table tbody tr:first-child td {
        background-color: none;
    }
    .fl1-table tr:nth-child(even) {
        background: transparent;
    }
    .fl1-table tr td:nth-child(odd) {
        
        border-right: 1px solid #E6E4E4;
    }
    .fl1-table tr td:nth-child(even) {
        border-right: 1px solid #E6E4E4;
    }
   
        .fl1-table {
    border-radius: 5px;
    font-size: 12px;
    font-weight: normal;
    border: none;
    border-collapse: collapse;
    width: 100%;
    max-width: 100%;
    white-space: nowrap;
    background-color: white;
}
        .fl1-table thead th {
    color: #ffffff;
    background: #4FC3A1;
}


.fl1-table thead th:nth-child(odd) {
    color: #ffffff;
    background: #00366c;
}

.fl1-table tr:nth-child(even) {
    background: none;
}
      
      .btn-group > .btn:first-child
      {
          width:100%;
      }
       .btn-group
      {
          width:100%;
      }


    </style>
    <script>
        $(document).ready(function () {
            $("#ContentPlaceHolder1_Booking_Type").on("change keyup paste", function () {
                $("#subjectline").text($(this).val());
            })
            $("#ContentPlaceHolder1_Airline").on("change keyup paste", function () {
                $("#airlineName").text($(this).val());
            })
            $("#ContentPlaceHolder1_Remarks").on("change keyup paste", function () {
                $("#remarkstext").text($(this).val());
            })
            
        });
    </script>

       <div class="container-fluid" >
            <div style="height: 15px;"></div>

            <div class="panel panel-default">
                <div class="panel-heading">
                   Charge Summary  -
                    <asp:Label ID="lblBookingID" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblStatus" runat="server" Text="" ></asp:Label>
                </div>
                <div class="panel-body" style="line-height: 34px;">
                    <input type="text" id="Booking_Type" name="Booking_Type" required="required" runat="server" placeholder="Booking Type" />
                    <input type="text" id="Airline" name="Airline" runat="server" required="required" placeholder="Airline Name" />
                     <input type="text" id="Remarks" name="Remarks" runat="server" required="required" placeholder="Remarks" />

<%= emailContent %>



                    
             

                  
                            <asp:Repeater ID="rptrAuthdoc" runat="server" >
                                <HeaderTemplate>
                                    <table width='100%' cellpadding='0' cellspacing='0' height="100px" class="fl1-table" style='margin-bottom: 0px'>
                                        <tr>
                                           <td class='gdvh'>Doc Type</td>
                                    <td class='gdvh'>Document</td>
                                    <td class='gdvh'>Action</td>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td class="gdvr exelcss">
                                            <asp:Label ID="lbtChargeID" runat="server" Text='<%# Eval("DocType")%>'></asp:Label>
                                        </td>
                                        <td class="gdvr exelcss">
                                            <asp:Label ID="lblChargeFor" runat="server" Text='<%# Eval("DocPath")%>'></asp:Label>
                                        </td>
                                       <td class="gdvr exelcss"><a class="btn btn-success" role="button" href='<%# Eval("DocPath")%>' download="PAX_Document">Download</a></td>
                                    </tr>
                                </ItemTemplate>

                            </asp:Repeater>
                          
                 
                  
                   
           </div>
            
         
           </div>
    </div>
   <br />
   <div class="container-fluid" >
                 <asp:Button ID="Button2" CssClass="btn btn-danger btn-lg" runat="server"  Text="Send Now" OnClick="SendBtn_Click" />
           </div>
    <br />
</asp:Content>

