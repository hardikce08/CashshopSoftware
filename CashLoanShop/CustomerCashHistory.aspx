<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerCashHistory.aspx.cs" Inherits="CashLoanShop.CustomerCashHistory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/screen.css" rel="stylesheet" media="all" />
    <style>
        .data {
            font-weight: bold;
        }
    </style>
    <script src="js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#btnPrint").click(function () {
                var contents = $("#printarea").html();
                var frame1 = $('<iframe />');
                frame1[0].name = "frame1";
                frame1.css({ "position": "absolute", "top": "-1000000px" });
                $("body").append(frame1);
                var frameDoc = frame1[0].contentWindow ? frame1[0].contentWindow : frame1[0].contentDocument.document ? frame1[0].contentDocument.document : frame1[0].contentDocument;
                frameDoc.document.open();
                //Create a new HTML document.
                frameDoc.document.write('<html><head>');

                //Append the external CSS file.
                frameDoc.document.write('<link href="css/screen.css" rel="stylesheet" type="text/css" />');
                frameDoc.document.write('<style>.data {font-weight: bold;}</style>');
                frameDoc.document.write('</head><body>');
                //Append the DIV contents.
                frameDoc.document.write(contents);
                frameDoc.document.write('</body></html>');
                frameDoc.document.close();
                setTimeout(function () {
                    window.frames["frame1"].focus();
                    window.frames["frame1"].print();
                    frame1.remove();
                }, 500);
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: right; width: 100%; font-weight: bold; font-size: 15px;">

            <%--<input type="button" onclick="PrintPartOfPage('dvprint')" value="Print" />--%>
            <%--<input type="button" value="Print" class="btn" onclick="PrintDoc()"/>--%>
            <input type="button" id="btnPrint" value="Print" class="btn" />
            <%-- <input type="button" value="Print Preview" class="btn" onclick="PrintPreview()"/>
            <input type="button" value="Close" />--%>
        </div>

        <div id="printarea">
            <div style="text-align: center; width: 100%; font-weight: bold; font-size: 15px;">
                <table>
                    <tr>
                        <b>Customer History</b>

                    </tr>
                </table>
            </div>
            <table>
                <asp:Repeater ID="rptCustomer" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>Name:</td>
                            <td>&nbsp;</td>
                            <td colspan="2" class="data">
                                <b><%# Eval("FirstName") %> <%# Eval("LastName") %> </b></td>
                            <td>&nbsp;Gender:</td>
                            <td>&nbsp;</td>
                            <td colspan="2" class="data"><%# Eval("Gender") %></td>
                        </tr>
                        <tr>
                            <td>Date of Birth:</td>
                            <td>&nbsp;</td>
                            <td colspan="2" class="data">
                                <%# DataBinder.Eval(Container.DataItem, "DateofBirth", "{0:MM/dd/yyyy}").Replace("-","/") %> 
                            </td>
                            <td>&nbsp;Address:</td>
                            <td>&nbsp;</td>
                            <td colspan="2" class="data"><%# Eval("Address") %></td>
                        </tr>
                        <tr>
                            <td>City:</td>
                            <td>&nbsp;</td>
                            <td colspan="2" class="data">
                                <%# Eval("City") %>    </td>
                            <td>&nbsp;Province:</td>
                            <td>&nbsp;</td>
                            <td colspan="2" class="data">
                                <%#Eval("Province").ToString() =="0" ? "Not Selected": Eval("Province") %>
                            </td>
                        </tr>
                        </tr>
                             <tr>
                                 <td>PostCode:</td>
                                 <td>&nbsp;</td>
                                 <td colspan="2" class="data">
                                     <%# Eval("PostCode") %>    </td>
                                 <td>&nbsp;Duration at this address:</td>
                                 <td>&nbsp;</td>
                                 <td colspan="2" class="data"><%# Eval("DurationYears") %> Years <%# Eval("DurationMonth") %> Months </td>
                             </tr>
                        <tr>
                            <td>Own a Home Or Rent:</td>
                            <td>&nbsp;</td>
                            <td colspan="2" class="data">
                                <%# Eval("HomeType") %>    </td>
                            <td>&nbsp;Home Phone:</td>
                            <td>&nbsp;</td>
                            <td colspan="2" class="data"><%# Eval("HomePhone") %> </td>
                        </tr>
                        <tr>
                            <td>Pay Cycle Comment:</td>
                            <td>&nbsp;</td>
                            <td colspan="2" class="data">
                                <%# Eval("PhoneListedunder") %>    </td>
                            <td>&nbsp;Work Phone:</td>
                            <td>&nbsp;</td>
                            <td colspan="2" class="data"><%# Eval("WorkPhone") %> </td>
                        </tr>
                        <tr>
                            <td>Cell Phone:</td>
                            <td>&nbsp;</td>
                            <td colspan="2" class="data">
                                <%# Eval("CellPhone") %>    </td>
                            <td>&nbsp;Social Insurance Number:</td>
                            <td>&nbsp;</td>
                            <td colspan="2" class="data"><%# Eval("SocialSecurityNumber") %> </td>
                        </tr>
                        <tr>

                            <td>&nbsp;Driving License Number:</td>
                            <td>&nbsp;</td>
                            <td colspan="2" class="data"><%# Eval("DrivingLicenseNumber") %> </td>
                            <td>&nbsp;Issues At:</td>
                            <td>&nbsp;</td>
                            <td colspan="2">
                                <%# Eval("IssuedAt") %>    </td>
                        </tr>
                        <tr>
                            <td>Comments:</td>
                            <td>&nbsp;</td>
                            <td colspan="6">
                                <%# Eval("Comments") %>    </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <table>
                <tr>
                    <td style="font-weight: bold; padding-top: 10px; padding-bottom: 10px;">Customer Employment Details</td>
                </tr>
            </table>
            <table>
                <asp:Repeater ID="rptEmploymentInformation" runat="server">
                    <ItemTemplate>
                        <tr>

                            <td>&nbsp;Employer Name:</td>
                            <td>&nbsp;</td>
                            <td colspan="2" class="data"><%# Eval("EmployerName") %> </td>
                            <td>&nbsp;Supervisor Name:</td>
                            <td>&nbsp;</td>
                            <td colspan="2">
                                <%# Eval("SupervisorName") %>    </td>
                        </tr>
                        <tr>

                            <td>&nbsp;Address:</td>
                            <td>&nbsp;</td>
                            <td colspan="2" class="data"><%# Eval("Address") %> </td>
                            <td>&nbsp;City:</td>
                            <td>&nbsp;</td>
                            <td colspan="2">
                                <%# Eval("City") %>    </td>
                        </tr>
                        <tr>

                            <td>&nbsp;Province:</td>
                            <td>&nbsp;</td>
                            <td colspan="2" class="data"><%# Eval("Province") %> </td>
                            <td>&nbsp;PostCode:</td>
                            <td>&nbsp;</td>
                            <td colspan="2">
                                <%# Eval("PostCode") %>    </td>
                        </tr>
                        <tr>

                            <td>&nbsp;EmployerDurationYears:</td>
                            <td>&nbsp;</td>
                            <td colspan="2" class="data"><%# Eval("EmployerDurationYears") %> </td>
                            <td>&nbsp;EmployerDurationMonth:</td>
                            <td>&nbsp;</td>
                            <td colspan="2">
                                <%# Eval("EmployerDurationMonth") %>    </td>
                        </tr>
                        <tr>

                            <td>&nbsp;Supervisor Phone:</td>
                            <td>&nbsp;</td>
                            <td colspan="2" class="data"><%# Eval("SupervisorPhone") %> </td>
                            <td>&nbsp;Supervisor Ext:</td>
                            <td>&nbsp;</td>
                            <td colspan="2">
                                <%# Eval("SupervisorExt") %>    </td>
                        </tr>
                        <tr>

                            <td>&nbsp;HR Phone:</td>
                            <td>&nbsp;</td>
                            <td colspan="2" class="data"><%# Eval("HRPhone") %> </td>
                            <td>&nbsp;HR Ext:</td>
                            <td>&nbsp;</td>
                            <td colspan="2">
                                <%# Eval("HRExt") %>    </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <div class="fhr">
                <hr />
            </div>
            <div style="font-weight: bold; font-size: 20px; padding-top: 15px; padding-bottom: 15px;">
                Cheque Cashing
            </div>

            <asp:Repeater ID="rptGridData" runat="server">
                <ItemTemplate>
                    <table>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>Date:</td>
                                        <td>&nbsp;</td>
                                        <td colspan="2" class="data">
                                            <b><%# DataBinder.Eval(Container.DataItem, "CreatedDate", "{0:MM/dd/yyyy}").Replace("-","/") %>   </b></td>
                                        <td>&nbsp;Cheque Issuer:</td>
                                        <td>&nbsp;</td>
                                        <td colspan="2" class="data"><%# Eval("CompanyName") %></td>
                                    </tr>
                                    <tr>
                                        <td>Cheque Type:</td>
                                        <td>&nbsp;</td>
                                        <td colspan="2" class="data">
                                            <%# (Eval("ChequeType").ToString() == "Custom" ? Eval("ChequeType").ToString()+" - "+Eval("CustomPercentage").ToString() +"%" : Eval("ChequeType").ToString()) %>    </td>
                                        <td>&nbsp;Cheque Number:</td>
                                        <td>&nbsp;</td>
                                        <td colspan="2" class="data"><%# Eval("ChequeNumber") %></td>
                                    </tr>
                                    <tr>
                                        <td>Cheque Amount:</td>
                                        <td>&nbsp;</td>
                                        <td colspan="2" class="data">$<%# Eval("ChequeAmount") %>    </td>
                                        <td>&nbsp;Cheque Amount Issued:</td>
                                        <td>&nbsp;</td>
                                        <td colspan="2" class="data">$<%# Eval("AmountIssued") %></td>
                                    </tr>
                                    <tr>
                                        <td>Charge %:</td>
                                        <td>&nbsp;</td>
                                        <td colspan="2" class="data"><%# Math.Round((Convert.ToDecimal(Convert.ToDecimal(Eval("Charges")) * 100)/ Convert.ToDecimal(Eval("ChequeAmount"))),1).ToString() %> %   </td>
                                        <td>&nbsp;Charges:</td>
                                        <td>&nbsp;</td>
                                        <td colspan="2" class="data">$<%# Math.Round(Convert.ToDecimal(Eval("Charges"))+2,2) %></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="padding-left: 15px;"><%# Eval("CompanyStoreAddres") %>
                            </td>
                        </tr>
                    </table>

                    <div class="fhr">
                        <hr />
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblEmptyData" runat="server" Visible='<%# ((Repeater)Container.NamingContainer).Items.Count == 0 %>' Font-Bold="true" Style="text-align: center;" Text="No records found" />
                </FooterTemplate>

            </asp:Repeater>
            <div id="loandiv" runat="server">
                <div style="font-weight: bold; font-size: 20px; padding-top: 15px; padding-bottom: 15px;">
                    <asp:Label ID="lblBusinessName" runat="server"></asp:Label>
                </div>
                <div style="font-weight: bold; font-size: 15px; padding-top: 15px; padding-bottom: 15px; display: none;" id="divempty" runat="server">
                    This user might have any old loan open with another store.
                </div>
                <asp:Repeater ID="rptLoan" runat="server">
                    <ItemTemplate>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>Loan Applied Date:</td>
                                            <td>&nbsp;</td>
                                            <td colspan="6" class="data">
                                                <b><%# DataBinder.Eval(Container.DataItem, "CreatedDate", "{0:MM/dd/yyyy}").Replace("-","/") %>   </b></td>

                                        </tr>
                                        <tr>
                                            <td>Status:</td>
                                            <td>&nbsp;</td>
                                            <td colspan="2" class="data">Loan <%# Eval("LoanStatus") %></td>
                                            <td>Payment Option:</td>
                                            <td>&nbsp;</td>
                                            <td colspan="2" class="data"><%# Eval("PaymentOption").ToString() %>     </td>
                                        </tr>
                                        <tr>
                                            <td>Loan Amount Applied:</td>
                                            <td>&nbsp;</td>
                                            <td colspan="2" class="data"><%# Eval("LoanAmountApplied") %></td>
                                            <td>Loan Amount Approved:</td>
                                            <td>&nbsp;</td>
                                            <td colspan="2" class="data">$<%# Eval("LoanAmountApproved") %>    </td>

                                        </tr>
                                        <tr>
                                            <td>Total Amount Due:</td>
                                            <td>&nbsp;</td>
                                            <td colspan="2" class="data"><%#Eval("LoanStatus").ToString() =="Denied" ? "": "$"+Eval("DueAmount") %> </td>
                                            <td>Loan Type:</td>
                                            <td>&nbsp;</td>
                                            <td colspan="2" class="data"><%#Eval("LoanStatus").ToString() =="Denied" ? "":  Eval("LoanType") %>    </td>

                                        </tr>
                                        <tr>
                                            <td>Due Date:</td>
                                            <td>&nbsp;</td>
                                            <td colspan="2" class="data"><%# Eval("LoanStatus").ToString() =="Denied" ? "":  DataBinder.Eval(Container.DataItem, "NextPayDate", "{0:MM/dd/yyyy}").Replace("-","/") %>     </td>

                                            <td><%# Eval("LoanStatus").ToString() == "Cancelled" ? "Cancelled Date:": (Eval("LoanStatus").ToString() == "Close" ? "Closed Date:" : "") %>   </td>
                                            <td>&nbsp;</td>
                                            <td colspan="2" class="data"><%# (Eval("LoanStatus").ToString() =="Cancelled" || Eval("LoanStatus").ToString() =="Close")   ? DataBinder.Eval(Container.DataItem, "UpdatedDate", "{0:MM/dd/yyyy}") : ""   %>     </td>

                                        </tr>
                                        <tr>
                                            <td>No. of Times Loan Overdue:</td>
                                            <td>&nbsp;</td>
                                            <td colspan="6" class="data"><%#Eval("LoanStatus").ToString() =="Denied"  ? "":  Eval("OverdueCount") %>    </td>


                                        </tr>
                                        <tr>

                                            <td><%# int.Parse(Eval("OverdueCount")==null?"0":Eval("OverdueCount").ToString()) >0 ? ((Eval("LoanStatus").ToString() =="Denied" || Eval("LoanStatus").ToString() =="Open" || Eval("LoanStatus").ToString() =="Payment In Process" || Eval("LoanStatus").ToString() =="Cancelled" || (Eval("LoanStatus").ToString() =="Close" && Convert.ToDateTime(Eval("UpdatedDate")).Date < Convert.ToDateTime(Eval("NextPayDate")).Date)) ? "":  "Total Late Amount Due:"):"" %> </td>
                                            <td>&nbsp;</td>
                                            <td colspan="2" class="data"><%# int.Parse(Eval("OverdueCount")==null?"0":Eval("OverdueCount").ToString()) >0 ? ((Eval("LoanStatus").ToString() =="Denied" || Eval("LoanStatus").ToString() =="Open" || Eval("LoanStatus").ToString() =="Payment In Process" || Eval("LoanStatus").ToString() =="Cancelled" || (Eval("LoanStatus").ToString() =="Close" && Convert.ToDateTime(Eval("UpdatedDate")).Date < Convert.ToDateTime(Eval("NextPayDate")).Date)) ? "":  "$"+Eval("RemainingAmount")):"" %>   </td>
                                            <td>Mode of Payment:</td>
                                            <td>&nbsp;</td>
                                            <td colspan="2" class="data"><%#(Eval("LoanStatus").ToString() =="Denied" || Eval("LoanStatus").ToString() =="Open") ? "":  Eval("ModeofPayment") %>     </td>

                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <%# Eval("PartialPayment")==null ? "":  Eval("PartialPayment").ToString().Replace("-","/").Replace("$$","-") %>
                                            </td>


                                        </tr>
                                    </table>
                                </td>
                                <td style="padding-left: 15px;"><%# Eval("StoreAddress") %></td>
                            </tr>
                        </table>

                        <div class="fhr">
                            <hr />
                        </div>

                    </ItemTemplate>

                </asp:Repeater>
            </div>
            <div id="termloandiv" runat="server">
                <div style="font-weight: bold; font-size: 20px; padding-top: 15px; padding-bottom: 15px;">
                    <asp:Label ID="lblTermLoanBusinessName" runat="server"></asp:Label>
                </div>
                <div style="font-weight: bold; font-size: 15px; padding-top: 15px; padding-bottom: 15px; display: none;" id="divtermempty" runat="server">
                    This user might have any old term loan open with another store.
                </div>
                <asp:Repeater ID="rptTermLoan" runat="server">
                    <ItemTemplate>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>Loan Applied Date:</td>
                                            <td>&nbsp;</td>
                                            <td colspan="6" class="data">
                                                <b><%# DataBinder.Eval(Container.DataItem, "CreatedDate", "{0:MM/dd/yyyy}").Replace("-","/") %>   </b></td>

                                        </tr>
                                        <tr>
                                            <td>Status:</td>
                                            <td>&nbsp;</td>
                                            <td colspan="2" class="data">Loan <%# Eval("LoanStatus") %></td>
                                            <td>Payment Option:</td>
                                            <td>&nbsp;</td>
                                            <td colspan="2" class="data"><%# Eval("PaymentOption").ToString() %>     </td>
                                        </tr>
                                        <tr>
                                            <td>Loan Amount Applied:</td>
                                            <td>&nbsp;</td>
                                            <td colspan="2" class="data"><%# Eval("LoanAmountApplied") %></td>
                                            <td>Loan Amount Approved:</td>
                                            <td>&nbsp;</td>
                                            <td colspan="2" class="data">$<%# Eval("LoanAmountApproved") %>    </td>

                                        </tr>
                                        <tr>
                                            <td>Total Amount Due:</td>
                                            <td>&nbsp;</td>
                                            <td colspan="2" class="data"><%#Eval("LoanStatus").ToString() =="Denied" ? "": "$"+Eval("DueAmount") %> </td>
                                            <td>Loan Type:</td>
                                            <td>&nbsp;</td>
                                            <td colspan="2" class="data"><%#Eval("LoanStatus").ToString() =="Denied" ? "":  Eval("LoanType") %>    </td>

                                        </tr>
                                        <tr>
                                            <td>Due Date:</td>
                                            <td>&nbsp;</td>
                                            <td colspan="2" class="data"><%# Eval("LoanStatus").ToString() =="Denied" ? "":  DataBinder.Eval(Container.DataItem, "DueDate", "{0:MM/dd/yyyy}").Replace("-","/") %>     </td>

                                            <td><%# Eval("LoanStatus").ToString() == "Cancelled" ? "Cancelled Date:": (Eval("LoanStatus").ToString() == "Close" ? "Closed Date:" : "") %>   </td>
                                            <td>&nbsp;</td>
                                            <td colspan="2" class="data"><%# (Eval("LoanStatus").ToString() =="Cancelled" || Eval("LoanStatus").ToString() =="Close")   ? DataBinder.Eval(Container.DataItem, "UpdatedDate", "{0:MM/dd/yyyy}") : ""   %>     </td>

                                        </tr>
                                        <tr>
                                            <td>Loan Term:</td>
                                            <td>&nbsp;</td>
                                            <td colspan="2" class="data"><%# Eval("LoanTerm").ToString() %> Year(s)   </td>
                                             <td>Total Amount Received:</td>
                                            <td>&nbsp;</td>
                                            <td colspan="2" class="data">$<%# Eval("TotalReceivedAmount") %>    </td>

                                        </tr>
                                        <%-- <tr>

                                            <td><%# int.Parse(Eval("OverdueCount")==null?"0":Eval("OverdueCount").ToString()) >0 ? ((Eval("LoanStatus").ToString() =="Denied" || Eval("LoanStatus").ToString() =="Open" || Eval("LoanStatus").ToString() =="Payment In Process" || Eval("LoanStatus").ToString() =="Cancelled" || (Eval("LoanStatus").ToString() =="Close" && Convert.ToDateTime(Eval("UpdatedDate")).Date < Convert.ToDateTime(Eval("NextPayDate")).Date)) ? "":  "Total Late Amount Due:"):"" %> </td>
                                            <td>&nbsp;</td>
                                            <td colspan="2" class="data"><%# int.Parse(Eval("OverdueCount")==null?"0":Eval("OverdueCount").ToString()) >0 ? ((Eval("LoanStatus").ToString() =="Denied" || Eval("LoanStatus").ToString() =="Open" || Eval("LoanStatus").ToString() =="Payment In Process" || Eval("LoanStatus").ToString() =="Cancelled" || (Eval("LoanStatus").ToString() =="Close" && Convert.ToDateTime(Eval("UpdatedDate")).Date < Convert.ToDateTime(Eval("NextPayDate")).Date)) ? "":  "$"+Eval("RemainingAmount")):"" %>   </td>
                                            <td>Mode of Payment:</td>
                                            <td>&nbsp;</td>
                                            <td colspan="2" class="data"><%#  Eval("PaymentOption") %>     </td>

                                        </tr>--%>
                                        <tr>
                                            <td colspan="6">
                                                <%# Eval("PartialPayment")==null ? "":  Eval("PartialPayment").ToString().Replace("-","/").Replace("$$","-") %>
                                            </td>


                                        </tr>
                                    </table>
                                </td>
                                <td style="padding-left: 15px;"><%# Eval("StoreAddress") %></td>
                            </tr>
                        </table>

                        <div class="fhr">
                            <hr />
                        </div>

                    </ItemTemplate>

                </asp:Repeater>
            </div>
            <div id="currencydiv" runat="server">

                <div style="font-weight: bold; font-size: 20px; padding-top: 15px; padding-bottom: 15px;">
                    Currency Exchange
                </div>
                <asp:Repeater ID="rptCurrencyExchange" runat="server">
                    <ItemTemplate>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>Date:</td>
                                            <td>&nbsp;</td>
                                            <td colspan="6" class="data">
                                                <b><%# DataBinder.Eval(Container.DataItem, "CreatedDate", "{0:MM/dd/yyyy}") %>   </b></td>

                                        </tr>
                                        <tr>
                                            <td>Transaction Type:</td>
                                            <td>&nbsp;</td>
                                            <td colspan="2" class="data"><%# Eval("TransactionType") %></td>
                                            <td>Currency:</td>
                                            <td>&nbsp;</td>
                                            <td colspan="2" class="data">
                                                <%#  Eval("CurrencyType").ToString().Substring(0,3) %>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>Exchange Amount:</td>
                                            <td>&nbsp;</td>
                                            <td colspan="2" class="data"><%#  Eval("CurrencyType").ToString().Substring(0,3) %>&nbsp;<%# Eval("ExchangeAmount") %></td>
                                            <td>Exchange Rate:</td>
                                            <td>&nbsp;</td>
                                            <td colspan="2" class="data">$<%# Eval("ExchangeRate") %>    </td>

                                        </tr>
                                        <tr>
                                            <td>Service Charge:</td>
                                            <td>&nbsp;</td>
                                            <td colspan="2" class="data">C$<%# Eval("ServiceCharge") %></td>
                                            <td>Amount Due:</td>
                                            <td>&nbsp;</td>
                                            <td colspan="2" class="data">C$<%# Eval("DueAmount") %>    </td>

                                        </tr>
                                    </table>
                                </td>
                                <td style="padding-left: 15px;"><%# Eval("StoreAddress") %>
                                </td>
                            </tr>
                        </table>

                        <div class="fhr">
                            <hr />
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblEmptyData" runat="server" Visible='<%# ((Repeater)Container.NamingContainer).Items.Count == 0 %>' Font-Bold="true" Style="text-align: center;" Text="No records found" />
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
    </form>
</body>
</html>
