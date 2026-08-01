<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewTransactionReport.aspx.cs" Inherits="CashLoanShop.ViewTransactionReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/screen.css" rel="stylesheet" media="all" />

    <link href="css/gridview.css" rel="stylesheet" media="all" />
    <style>
        tr {
            line-height: 30px;
        }

            tr > td {
                padding-top: 1em;
            }

        #searchtbl tr {
            border-top: 1px solid black;
        }

        th {
            border-bottom: 2px solid gray;
            text-align: left;
        }

        .headerrow {
            font-weight: bold !important;
            color: black;
            font-size: 30px;
        }

        .footerrow {
            font-weight: bold !important;
            color: black;
            font-size: 17px;
            font-family: verdana;
        }

        .data {
            font-weight: bold;
            color: black;
        }

            .data > span {
                font-weight: bold;
                color: black;
            }
    </style>
    <script src="js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#btnprintarea").click(function () {
                printwindow('printloandata');
            });
            $("#btnprintareacashcheque").click(function () {
                printwindow('printcash');
            });
            $("#btnprintareacurrency").click(function () {
                printwindow('printcurrency');
            });
            $("#btnprintareatermloan").click(function () {
                printwindow('printtermloandata');
            });
            $("#btnmasterprintarea").click(function () {
                printwindow('masterprintarea');
            });

            function printwindow(contentarea) {
                var contents = $("#" + contentarea).html();
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
                frameDoc.document.write('<link href="css/gridview.css" rel="stylesheet" type="text/css" />');
                frameDoc.document.write('<style>.data {font-weight: bold;} th{border-bottom: 2px solid gray; text-align: left;}</style>');
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

            }
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: right; width: 100%; font-weight: bold; font-size: 15px;">
            <input type="button" id="btnmasterprintarea" runat="server" value="Print" class="btn" />
        </div>
        <div id="masterprintarea" runat="server">
            <div id="printarea" runat="server">
                <div style="text-align: right; width: 100%; font-weight: bold; font-size: 15px;">
                    <input type="button" id="btnprintarea" runat="server" value="Print" class="btn" />
                </div>
                <div id="printloandata">
                    <div style="text-align: center; width: 100%; font-weight: bold; font-size: 15px;">
                        <asp:Label ID="lblStoreInfo" runat="server"></asp:Label><br />
                    </div>
                    <div style="width: 100%; font-size: 15px; padding-top: 20px;">
                        <table style="width: 90%;">
                            <tr>
                                <td style="width: 20%;">Date Range:
                                </td>
                                <td style="width: 30%;">
                                    <asp:Label ID="lblDateRange" runat="server"></asp:Label>
                                </td>
                                <td style="width: 50%;" class="data">Transaction Report (
                                    <asp:Label ID="lblTransactionType" runat="server" Text="Payday Loan" Style="font-weight: bold;"></asp:Label>)
                                </td>
                            </tr>

                        </table>
                    </div>
                    <table style="width: 100%; padding-top: 20px;" class="EU_DataTable table table-hover table-bordered">
                        <tr>
                            <h4 style="text-align: center; font-size: 25px;">Loan Open</h4>
                        </tr>
                        <asp:Repeater ID="rptGridData" runat="server" OnItemDataBound="rptGridData_ItemDataBound">
                            <HeaderTemplate>
                                <th>Date
                                </th>
                                <th>Customer Name
                                </th>
                                <th>Customer Id
                                </th>
                                <th>Loan Amount
                                </th>
                                <th>Charges
                                </th>
                                <th>Loan Due Amount
                                </th>
                                <th>Installment Amount
                                </th>
                                <th>Total Installment Received
                                </th>
                                <th>Due Date
                                </th>
                                <th>Loan Payment Option
                                </th>
                                <th>Loan Status
                                </th>
                                <th>Method of Payment
                                </th>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "CreatedDate", "{0:MM/dd/yyyy}")).Replace("-","/") %>
                                    </td>
                                    <td>
                                        <%# Eval("CustomerName") %>
                                    </td>
                                    <td>
                                        <%# Eval("CustomerId") %>
                                    </td>
                                    <td>
                                        <%# Eval("LoanAmountApproved") %>
                                    </td>
                                    <td>
                                        <%# Eval("AdminFee") %>
                                    </td>
                                    <td>
                                        <%# Eval("DueAmount") %>
                                    </td>
                                    <td>
                                        <%# Eval("DisplayInstallmentAmount") %>
                                    </td>
                                    <td>
                                        <%# Eval("DisplayPartialAmount") %>
                                    </td>
                                    <td>
                                        <%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "NextPayDate", "{0:MM/dd/yyyy}")).Replace("-","/") %>
                                    </td>
                                    <td>
                                        <%# Eval("PaymentOption") %>
                                    </td>
                                    <td>Loan <%# Eval("LoanStatus") %>
                                    </td>
                                    <td><%# Eval("ModeofPayment") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                <tr style="border: 1px solid black; font-weight: bold; font-family: Verdana;">
                                    <td></td>
                                    <td><span class="footerrow">Grand Total</span>
                                        <td></td>
                                        <td>
                                            <%= TotalApprovedAmount  %>
                                        </td>
                                        <td>
                                            <%= TotalCharges  %>
                                        </td>
                                        <td>
                                            <%= TotalDue  %>
                                        </td>
                                        <td><%= TotalInstallmentAmount  %></td>
                                        <td><%= TotalInstallmentReceivedAmount  %></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                </tr>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                    <table style="width: 100%; padding-top: 20px;" class="EU_DataTable table table-hover table-bordered">
                        <tr>
                            <h4 style="text-align: center; font-size: 25px;">Loan Close</h4>
                        </tr>
                        <asp:Repeater ID="rptClose" runat="server" OnItemDataBound="rptClose_ItemDataBound">
                            <HeaderTemplate>
                                <th>Date
                                </th>
                                <th>Customer Name
                                </th>
                                <th>Customer Id
                                </th>
                                <th>Loan Amount
                                </th>
                                <th>Charges
                                </th>
                                <th>Original Due Amount
                                </th>
                                <th>Discount Amount
                                </th>
                                <th>After Close Remaining Amount
                                </th>
                                <th>Due Date
                                </th>
                                <th>Loan Payment Option
                                </th>
                                <th>Loan Status
                                </th>
                                <th>Method of Payment
                                </th>
                                <th>Loan Close From
                                </th>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "UpdatedDate", "{0:MM/dd/yyyy}")).Replace("-","/") %>
                                    </td>
                                    <td>
                                        <%# Eval("CustomerName") %>
                                    </td>
                                    <td>
                                        <%# Eval("CustomerId") %>
                                    </td>
                                    <td>
                                        <%# Eval("LoanAmountApproved") %>
                                    </td>
                                    <td>
                                        <%# Eval("AdminFee") %>
                                    </td>
                                    <td>
                                        <%# Eval("DueAmount") %>
                                    </td>
                                    <td>
                                        <%# Eval("DiscountAmount") %>
                                    </td>
                                    <td>
                                        <%# Eval("RemainingAmount") %>
                                    </td>
                                    <td>
                                        <%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "NextPayDate", "{0:MM/dd/yyyy}")).Replace("-","/") %>
                                    </td>
                                    <td>
                                        <%# Eval("PaymentOption") %>
                                    </td>
                                    <td>Loan <%# Eval("LoanStatus") %>
                                    </td>
                                    <td><%# Eval("ModeofPayment") ==null ? Eval("PaymentOption"):Eval("ModeofPayment") %>
                                    </td>
                                    <td><%# Eval("LastStatus") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                <tr style="border: 1px solid black; font-weight: bold; font-family: Verdana;">
                                    <td></td>
                                    <td><span class="footerrow">Grand Total</span>
                                        <td></td>
                                        <td>
                                            <%= TotalApprovedAmountClose  %>
                                        </td>
                                        <td>
                                            <%= TotalChargesClose  %>
                                        </td>
                                        <td>
                                            <%= TotalDueClose  %>
                                        </td>
                                        <td></td>
                                        <td><%= TotalRemainingClose %></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                </tr>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                    <table style="width: 100%; padding-top: 20px;" class="EU_DataTable table table-hover table-bordered">
                        <tr>
                            <h4 style="text-align: center; font-size: 25px;">Loan Cancelled</h4>
                        </tr>
                        <asp:Repeater ID="rptCancelled" runat="server" OnItemDataBound="rptCancelled_ItemDataBound">
                            <HeaderTemplate>
                                <th>Cancelled Date
                                </th>
                                <th>Open Date
                                </th>
                                <th>Customer Name
                                </th>
                                <th>Customer Id
                                </th>
                                <th>Loan Amount
                                </th>
                                <th>Charges
                                </th>
                                <th>Original Due Amount
                                </th>
                                <th>After Close Remaining Amount
                                </th>
                                <th>Due Date
                                </th>
                                <th>Loan Payment Option
                                </th>
                                <th>Loan Status
                                </th>
                                <th>Method of Payment
                                </th>
                                <th>Loan Close From
                                </th>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "UpdatedDate", "{0:MM/dd/yyyy}")).Replace("-","/") %>
                                    </td>
                                    <td>
                                        <%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "CreatedDate", "{0:MM/dd/yyyy}")).Replace("-","/") %>
                                    </td>
                                    <td>
                                        <%# Eval("CustomerName") %>
                                    </td>
                                    <td>
                                        <%# Eval("CustomerId") %>
                                    </td>
                                    <td>
                                        <%# Eval("LoanAmountApproved") %>
                                    </td>
                                    <td>
                                        <%# Eval("AdminFee") %>
                                    </td>
                                    <td>
                                        <%# Eval("DueAmount") %>
                                    </td>
                                    <td>
                                        <%# Eval("RemainingAmount") %>
                                    </td>
                                    <td>
                                        <%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "NextPayDate", "{0:MM/dd/yyyy}")).Replace("-","/") %>
                                    </td>
                                    <td>
                                        <%# Eval("PaymentOption") %>
                                    </td>
                                    <td>Loan <%# Eval("LoanStatus") %>
                                    </td>
                                    <td><%# Eval("ModeofPayment") %>
                                    </td>
                                    <td><%# Eval("LastStatus") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                <tr style="border: 1px solid black; font-weight: bold; font-family: Verdana;">
                                    <td></td>
                                    <td></td>
                                    <td><span class="footerrow">Grand Total</span>
                                        <td></td>
                                        <td>
                                            <%= TotalApprovedAmountCancelled  %>
                                        </td>
                                        <td>
                                            <%= TotalChargesCancelled  %>
                                        </td>
                                        <td>
                                            <%= TotalDueCancelled  %>
                                        </td>
                                        <td><%= TotalRemainingCancelled %></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                </tr>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                    <table style="width: 100%; padding-top: 20px;" class="EU_DataTable table table-hover table-bordered">
                        <tr>
                            <h4 style="text-align: center; font-size: 25px;">Loan Denied</h4>
                        </tr>
                        <asp:Repeater ID="rptDenied" runat="server" OnItemDataBound="rptDenied_ItemDataBound">
                            <HeaderTemplate>
                                <th>Date
                                </th>
                                <th>Customer Name
                                </th>
                                <th>Customer Id
                                </th>
                                <th>Loan Amount
                                </th>
                                <th>Charges
                                </th>
                                <th>Due Amount
                                </th>
                                <th>Due Date
                                </th>
                                <th>Loan Payment Option
                                </th>
                                <th>Loan Status
                                </th>
                                <th>Method of Payment
                                </th>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "UpdatedDate", "{0:MM/dd/yyyy}")).Replace("-","/") %>
                                    </td>
                                    <td>
                                        <%# Eval("CustomerName") %>
                                    </td>
                                    <td>
                                        <%# Eval("CustomerId") %>
                                    </td>
                                    <td>
                                        <%# Eval("LoanAmountApproved") %>
                                    </td>
                                    <td>
                                        <%# Eval("AdminFee") %>
                                    </td>
                                    <td>
                                        <%# Eval("DueAmount") %>
                                    </td>
                                    <td>
                                        <%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "NextPayDate", "{0:MM/dd/yyyy}")).Replace("-","/") %>
                                    </td>
                                    <td>
                                        <%# Eval("PaymentOption") %>
                                    </td>
                                    <td>Loan <%# Eval("LoanStatus") %>
                                    </td>
                                    <td><%# Eval("ModeofPayment") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                <tr style="border: 1px solid black; font-weight: bold; font-family: Verdana;">
                                    <td></td>
                                    <td><span class="footerrow">Grand Total</span>
                                        <td></td>
                                        <td>
                                            <%= TotalApprovedAmountDenied  %>
                                        </td>
                                        <td>
                                            <%= TotalChargesDenied  %>
                                        </td>
                                        <td>
                                            <%= TotalDueDenied  %>
                                        </td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                </tr>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                    <table style="width: 100%; padding-top: 20px;" class="EU_DataTable table table-hover table-bordered">
                        <tr>
                            <h4 style="text-align: center; font-size: 25px;">Loan Partial Payment</h4>
                        </tr>
                        <asp:Repeater ID="rptPartialPaid" runat="server" OnItemDataBound="rptPartialPaid_ItemDataBound">
                            <HeaderTemplate>
                                <th>Date
                                </th>
                                <th>Customer Name
                                </th>
                                <th>Customer Id
                                </th>
                                <th>Loan Amount
                                </th>
                                <th>Charges
                                </th>
                                <th>Due Amount
                                </th>
                                <th>Discount Amount
                                </th>
                                <th>Due Date
                                </th>
                                <th>Partial Payment Amount
                                </th>
                                <th>Partial Payment Method
                                </th>
                                <th>Loan Status
                                </th>

                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "UpdatedDate", "{0:MM/dd/yyyy}")).Replace("-","/") %>
                                    </td>
                                    <td>
                                        <%# Eval("CustomerName") %>
                                    </td>
                                    <td>
                                        <%# Eval("CustomerId") %>
                                    </td>
                                    <td>
                                        <%# Eval("LoanAmountApproved") %>
                                    </td>
                                    <td>
                                        <%# Convert.ToDecimal(Eval("AdminFee")) + Convert.ToDecimal(Eval("LateInterestCharge")==null? "0":Eval("LateInterestCharge")) + Convert.ToDecimal(Eval("NSFCharge")==null?"0":Eval("NSFCharge")) %>
                                    </td>
                                    <td>
                                        <%# Convert.ToDecimal(Eval("LoanAmountApproved")) + (Convert.ToDecimal(Eval("AdminFee")) + Convert.ToDecimal(Eval("LateInterestCharge")==null? "0":Eval("LateInterestCharge")) + Convert.ToDecimal(Eval("NSFCharge")==null?"0":Eval("NSFCharge"))) %>
                                    </td>
                                    <td>
                                        <%# Eval("DiscountAmount") %>
                                    </td>
                                    <td>
                                        <%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "NextPayDate", "{0:MM/dd/yyyy}")).Replace("-","/") %>
                                    </td>
                                    <td>
                                        <%# Eval("PartialAmount") %>
                                    </td>
                                    <td>
                                        <%# Eval("PartialPaymentMethod") %>
                                    </td>
                                    <td>Loan <%# Eval("LoanStatus") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                <tr style="border: 1px solid black; font-weight: bold; font-family: Verdana;">
                                    <td></td>
                                    <td><span class="footerrow">Grand Total</span>
                                        <td></td>
                                        <td>
                                            <%= TotalApprovedAmountPartial %>
                                        </td>
                                        <td>
                                            <%= TotalChargesPartial  %>
                                        </td>
                                        <td>
                                            <%= TotalDuePartial  %>
                                        </td>
                                        <td></td>
                                        <td></td>

                                        <td><%= TotalPartialPaymentSum  %></td>
                                        <td></td>
                                        <td></td>
                                </tr>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>

                </div>
            </div>
            <div id="printtermloanarea" runat="server">
                <div style="text-align: right; width: 100%; font-weight: bold; font-size: 15px;">
                    <input type="button" id="btnprintareatermloan" runat="server" value="Print" class="btn" />
                </div>
                <div id="printtermloandata">
                    <div style="text-align: center; width: 100%; font-weight: bold; font-size: 15px;">
                        <asp:Label ID="lblTermStoreInfo" runat="server"></asp:Label><br />
                    </div>
                    <div style="width: 100%; font-size: 15px; padding-top: 20px;">
                        <table style="width: 90%;">
                            <tr>
                                <td style="width: 20%;">Date Range:
                                </td>
                                <td style="width: 30%;">
                                    <asp:Label ID="lblTermDateRange" runat="server"></asp:Label>
                                </td>
                                <td style="width: 50%;" class="data">Transaction Report (
                                    <asp:Label ID="lblTermTransactionType" runat="server" Text="Term Loan" Style="font-weight: bold;"></asp:Label>)
                                </td>
                            </tr>

                        </table>
                    </div>
                    <table style="width: 100%; padding-top: 20px;" class="EU_DataTable table table-hover table-bordered">
                        <tr>
                            <h4 style="text-align: center; font-size: 25px;">Term Loan Open</h4>
                        </tr>
                        <asp:Repeater ID="rptTermGridData" runat="server" OnItemDataBound="rptTermGridData_ItemDataBound">
                            <HeaderTemplate>
                                <th>Date
                                </th>
                                <th>Customer Name
                                </th>
                                <th>Customer Id
                                </th>
                                <th>Loan Amount
                                </th>
                                <th>Charges
                                </th>
                                <th>Loan Due Amount
                                </th>
                                <th>Installment Amount
                                </th>
                                <th>Installment Amount Received
                                </th>
                                <th>Discount
                                </th>
                                <th>Remaining Balance Due
                                </th>
                                <th>Next Installment Date
                                </th>
                                <th>Due Date
                                </th>
                                <th>Loan Payment Option
                                </th>
                                <th>Loan Status
                                </th>
                                <th>Method of Payment
                                </th>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "CreatedDate", "{0:MM/dd/yyyy}")).Replace("-","/") %>
                                    </td>
                                    <td>
                                        <%# Eval("CustomerName") %>
                                    </td>
                                    <td>
                                        <%# Eval("CustomerId") %>
                                    </td>
                                    <td>
                                        <%# Eval("LoanAmountApproved") %>
                                    </td>
                                    <td>
                                        <%# Eval("AdminFee") %>
                                    </td>
                                    <td>
                                        <%# Eval("DueAmount") %>
                                    </td>
                                    <td>
                                        <%# Eval("Installment1Amount") %>
                                    </td>
                                    <td>
                                        <%# Eval("DisplayPartialAmount") %>
                                    </td>
                                    <td>
                                        <%# Eval("DiscountAmount") %>
                                    </td>
                                    <td>
                                        <%# Convert.ToDecimal(Eval("DueAmount")) - (Convert.ToDecimal(Eval("DisplayPartialAmount")) + Convert.ToDecimal(Eval("DiscountAmount"))) %>
                                    </td>
                                    <td>
                                        <%# Eval("DisplayInstallmentDate") %>
                                    </td>
                                    <td>
                                        <%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "NextPayDate", "{0:MM/dd/yyyy}")).Replace("-","/") %>
                                    </td>
                                    <td>
                                        <%# Eval("PaymentOption") %>
                                    </td>
                                    <td>Loan <%# Eval("LoanStatus") %>
                                    </td>
                                    <td><%# Eval("ModeofPayment") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                <tr style="border: 1px solid black; font-weight: bold; font-family: Verdana;">
                                    <td></td>
                                    <td><span class="footerrow">Grand Total</span>
                                        <td></td>
                                        <td>
                                            <%= TotalTermApprovedAmount  %>
                                        </td>
                                        <td>
                                            <%= TotalTermCharges  %>
                                        </td>
                                        <td>
                                            <%= TotalTermDue  %>
                                        </td>
                                        <td>
                                            <%= TotalTermInstallmentAmount  %>
                                        </td>
                                        <td>
                                            <%= TotalTermInstallmentReceived  %>
                                        </td>
                                        <td><%= TotalDiscount  %></td>
                                        <td><%= TotalTermDue -(TotalTermInstallmentReceived +  TotalDiscount ) %></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                </tr>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                    <table style="width: 100%; padding-top: 20px;" class="EU_DataTable table table-hover table-bordered">
                        <tr>
                            <h4 style="text-align: center; font-size: 25px;">Term Loan Close</h4>
                        </tr>
                        <asp:Repeater ID="rptTermClose" runat="server" OnItemDataBound="rptTermClose_ItemDataBound">
                            <HeaderTemplate>
                                <th>Date
                                </th>
                                <th>Customer Name
                                </th>
                                <th>Customer Id
                                </th>
                                <th>Loan Amount
                                </th>
                                <th>Charges
                                </th>
                                <th>Original Due Amount
                                </th>
                                <th>Discount Amount
                                </th>
                                <th>Payment Received
                                </th>
                                <th>Due Date
                                </th>
                                <th>Loan Payment Option
                                </th>
                                <th>Loan Status
                                </th>
                                <th>Method of Payment
                                </th>
                                <th>Loan Close From
                                </th>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "UpdatedDate", "{0:MM/dd/yyyy}")).Replace("-","/") %>
                                    </td>
                                    <td>
                                        <%# Eval("CustomerName") %>
                                    </td>
                                    <td>
                                        <%# Eval("CustomerId") %>
                                    </td>
                                    <td>
                                        <%# Eval("LoanAmountApproved") %>
                                    </td>
                                    <td>
                                        <%# Eval("AdminFee") %>
                                    </td>
                                    <td>
                                        <%# Eval("DueAmount") %>
                                    </td>
                                    <td>
                                        <%# Eval("DiscountAmount") %>
                                    </td>
                                    <td>
                                        <%# Eval("PartialAmount") %>
                                    </td>
                                    <td>
                                        <%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "NextPayDate", "{0:MM/dd/yyyy}")).Replace("-","/") %>
                                    </td>
                                    <td>
                                        <%# Eval("PaymentOption") %>
                                    </td>
                                    <td>Loan <%# Eval("LoanStatus") %>
                                    </td>
                                    <td><%# Eval("ModeofPayment")==null ? Eval("PaymentOption"):Eval("ModeofPayment") %>
                                    </td>
                                    <td><%# Eval("LastStatus") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                <tr style="border: 1px solid black; font-weight: bold; font-family: Verdana;">
                                    <td></td>
                                    <td><span class="footerrow">Grand Total</span>
                                        <td></td>
                                        <td>
                                            <%= TotalTermApprovedAmountClose  %>
                                        </td>
                                        <td>
                                            <%= TotalTermChargesClose  %>
                                        </td>
                                        <td>
                                            <%= TotalTermDueClose  %>
                                        </td>
                                        <td></td>
                                        <td><%= TotalTermRemainingClose %></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                </tr>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                    <table style="width: 100%; padding-top: 20px;" class="EU_DataTable table table-hover table-bordered">
                        <tr>
                            <h4 style="text-align: center; font-size: 25px;">Term Loan Cancelled</h4>
                        </tr>
                        <asp:Repeater ID="rptTermCancelled" runat="server" OnItemDataBound="rptTermCancelled_ItemDataBound">
                            <HeaderTemplate>
                                <th>Cancelled Date
                                </th>
                                <th>Open Date
                                </th>
                                <th>Customer Name
                                </th>
                                <th>Customer Id
                                </th>
                                <th>Loan Amount
                                </th>
                                <th>Charges
                                </th>
                                <th>Original Due Amount
                                </th>
                                <th>After Close Remaining Amount
                                </th>
                                <th>Due Date
                                </th>
                                <th>Loan Payment Option
                                </th>
                                <th>Loan Status
                                </th>
                                <th>Method of Payment
                                </th>
                                <th>Loan Close From
                                </th>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "UpdatedDate", "{0:MM/dd/yyyy}")).Replace("-","/") %>
                                    </td>
                                    <td>
                                        <%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "CreatedDate", "{0:MM/dd/yyyy}")).Replace("-","/") %>
                                    </td>
                                    <td>
                                        <%# Eval("CustomerName") %>
                                    </td>
                                    <td>
                                        <%# Eval("CustomerId") %>
                                    </td>
                                    <td>
                                        <%# Eval("LoanAmountApproved") %>
                                    </td>
                                    <td>
                                        <%# Eval("AdminFee") %>
                                    </td>
                                    <td>
                                        <%# Eval("DueAmount") %>
                                    </td>
                                    <td>
                                        <%# Eval("RemainingAmount") %>
                                    </td>
                                    <td>
                                        <%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "NextPayDate", "{0:MM/dd/yyyy}")).Replace("-","/") %>
                                    </td>
                                    <td>
                                        <%# Eval("PaymentOption") %>
                                    </td>
                                    <td>Loan <%# Eval("LoanStatus") %>
                                    </td>
                                    <td><%# Eval("ModeofPayment") %>
                                    </td>
                                    <td><%# Eval("LastStatus") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                <tr style="border: 1px solid black; font-weight: bold; font-family: Verdana;">
                                    <td></td>
                                    <td></td>
                                    <td><span class="footerrow">Grand Total</span>
                                        <td></td>
                                        <td>
                                            <%= TotalTermApprovedAmountCancelled  %>
                                        </td>
                                        <td>
                                            <%= TotalTermChargesCancelled  %>
                                        </td>
                                        <td>
                                            <%= TotalTermDueCancelled  %>
                                        </td>
                                        <td><%= TotalTermRemainingCancelled %></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                </tr>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                    <table style="width: 100%; padding-top: 20px;" class="EU_DataTable table table-hover table-bordered">
                        <tr>
                            <h4 style="text-align: center; font-size: 25px;">Term Loan Denied</h4>
                        </tr>
                        <asp:Repeater ID="rptTermDenied" runat="server" OnItemDataBound="rptTermDenied_ItemDataBound">
                            <HeaderTemplate>
                                <th>Date
                                </th>
                                <th>Customer Name
                                </th>
                                <th>Customer Id
                                </th>
                                <th>Loan Amount
                                </th>
                                <th>Charges
                                </th>
                                <th>Due Amount
                                </th>
                                <th>Due Date
                                </th>
                                <th>Loan Payment Option
                                </th>
                                <th>Loan Status
                                </th>
                                <th>Method of Payment
                                </th>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "UpdatedDate", "{0:MM/dd/yyyy}")).Replace("-","/") %>
                                    </td>
                                    <td>
                                        <%# Eval("CustomerName") %>
                                    </td>
                                    <td>
                                        <%# Eval("CustomerId") %>
                                    </td>
                                    <td>
                                        <%# Eval("LoanAmountApproved") %>
                                    </td>
                                    <td>
                                        <%# Eval("AdminFee") %>
                                    </td>
                                    <td>
                                        <%# Eval("DueAmount") %>
                                    </td>
                                    <td>
                                        <%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "NextPayDate", "{0:MM/dd/yyyy}")).Replace("-","/") %>
                                    </td>
                                    <td>
                                        <%# Eval("PaymentOption") %>
                                    </td>
                                    <td>Loan <%# Eval("LoanStatus") %>
                                    </td>
                                    <td><%# Eval("ModeofPayment") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                <tr style="border: 1px solid black; font-weight: bold; font-family: Verdana;">
                                    <td></td>
                                    <td><span class="footerrow">Grand Total</span>
                                        <td></td>
                                        <td>
                                            <%= TotalTermApprovedAmountDenied  %>
                                        </td>
                                        <td>
                                            <%= TotalTermChargesDenied  %>
                                        </td>
                                        <td>
                                            <%= TotalTermDueDenied  %>
                                        </td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                </tr>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                    <table style="width: 100%; padding-top: 20px;" class="EU_DataTable table table-hover table-bordered">
                        <tr>
                            <h4 style="text-align: center; font-size: 25px;">Term Loan Partial Payment</h4>
                        </tr>
                        <asp:Repeater ID="rptTermPartialPaid" runat="server" OnItemDataBound="rptTermPartialPaid_ItemDataBound">
                            <HeaderTemplate>
                                <th>Date
                                </th>
                                <th>Customer Name
                                </th>
                                <th>Customer Id
                                </th>
                                <th>Loan Amount
                                </th>
                                <th>Charges
                                </th>
                                <th>Total Due Amount
                                </th>
                                <th>Discount Amount
                                </th>
                                <th>Remaining Balance Due
                                </th>
                                <th>Partial Payment Date
                                </th>
                                <th>Partial Payment Amount
                                </th>
                                <th>Partial Payment Method
                                </th>
                                <th>Loan Status
                                </th>

                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "UpdatedDate", "{0:MM/dd/yyyy}")).Replace("-","/") %>
                                    </td>
                                    <td>
                                        <%# Eval("CustomerName") %>
                                    </td>
                                    <td>
                                        <%# Eval("CustomerId") %>
                                    </td>
                                    <td>
                                        <%# Eval("LoanAmountApproved") %>
                                    </td>
                                    <td>
                                        <%# Convert.ToDecimal(Eval("AdminFee")) + Convert.ToDecimal(Eval("LateInterestCharge")==null? "0":Eval("LateInterestCharge")) + Convert.ToDecimal(Eval("NSFCharge")==null?"0":Eval("NSFCharge")) %>
                                    </td>
                                    <td>
                                        <%--<%# Convert.ToDecimal(Eval("LoanAmountApproved")) + (Convert.ToDecimal(Eval("AdminFee")) + Convert.ToDecimal(Eval("LateInterestCharge")==null? "0":Eval("LateInterestCharge")) + Convert.ToDecimal(Eval("NSFCharge")==null?"0":Eval("NSFCharge"))) %>--%>
                                        <%# Convert.ToDecimal(Eval("DueAmount")) %>
                                    </td>
                                    <td>
                                        <%# Eval("DiscountAmount") %>
                                    </td>
                                    <td>
                                        <%# Eval("BalanceDue") %>
                                    </td>
                                    <td>
                                        <%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "PartialLoanCreatedDate", "{0:MM/dd/yyyy}")).Replace("-","/") %>
                                    </td>
                                    <td>
                                        <%# Eval("PartialAmount") %>
                                    </td>
                                    <td>
                                        <%# Eval("PartialPaymentMethod") %>
                                    </td>
                                    <td>Loan <%# Eval("LoanStatus") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                <tr style="border: 1px solid black; font-weight: bold; font-family: Verdana;">
                                    <td></td>
                                    <td><span class="footerrow">Grand Total</span>
                                        <td></td>
                                        <td>
                                            <%= TotalTermApprovedAmountPartial %>
                                        </td>
                                        <td>
                                            <%= TotalTermChargesPartial  %>
                                        </td>
                                        <td>
                                            <%--  <%= TotalTermDuePartial  %>--%>
                                        </td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td><%= TotalTermPartialPaymentSum  %></td>
                                        <td></td>
                                        <td></td>
                                </tr>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>

                </div>
            </div>
            <div id="printareacashcheque" runat="server">
                <div style="text-align: right; width: 100%; font-weight: bold; font-size: 15px;">
                    <input type="button" id="btnprintareacashcheque" runat="server" value="Print" class="btn" />
                </div>
                <div id="printcash">
                    <div style="text-align: center; width: 100%; font-weight: bold; font-size: 15px;">
                        <asp:Label ID="lblCashStoreInfo" runat="server"></asp:Label><br />
                    </div>
                    <div style="width: 100%; font-size: 15px; padding-bottom: 15px; padding-top: 15px;">
                        <table style="width: 70%;">
                            <tr>
                                <td style="width: 10%;">Date Range:
                                </td>
                                <td style="width: 36%;">
                                    <asp:Label ID="lblDateRangeCheque" runat="server"></asp:Label>
                                </td>
                                <td style="width: 50%;" class="data">Transaction Report (Cheque Cash)
                                </td>
                            </tr>

                        </table>
                    </div>
                    <table style="width: 100%;" class="EU_DataTable table table-hover table-bordered">
                        <asp:Repeater ID="rptCashChequeData" runat="server" OnItemDataBound="rptCashChequeData_ItemDataBound">
                            <HeaderTemplate>
                                <th style="text-align: left; width: 8%;">Date
                                </th>
                                <th style="text-align: left; width: 25%;">Customer Name
                                </th>
                                <th style="text-align: left; width: 9%;">Customer Id
                                </th>
                                <th style="text-align: right; width: 12%;">Cheque Type
                                </th>
                                <th style="text-align: right; width: 12%;">Cheque Amount
                                </th>
                                <th style="text-align: right; width: 10%;">Charges
                                </th>
                                <th style="text-align: right; width: 10%;">Cash Out
                                </th>
                                <th style="text-align: right; width: 10%;">Charges %
                                </th>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "CreatedDate", "{0:MM/dd/yyyy}")).Replace("-","/") %>
                                    </td>
                                    <td>
                                        <%# Eval("CustomerName") %>
                                    </td>
                                    <td style="text-align: center;">
                                        <%# Eval("CustomerId") %>
                                    </td>
                                    <td style="text-align: right;">
                                        <%# (Eval("ChequeType").ToString() == "Custom" ? Eval("ChequeType").ToString()+" - "+Eval("CustomPercentage").ToString() +"%" : Eval("ChequeType").ToString()) %>    </td>
                                    </td>
                                    <td style="text-align: right;">
                                        <%# Eval("ChequeAmount") %>
                                    </td>
                                    <td style="text-align: right;">
                                        <%# Math.Round( Convert.ToDecimal( Eval("Charges"))+2,2) %>
                                    </td>
                                    <td style="text-align: right;">
                                        <%# Eval("AmountIssued") %>
                                    </td>
                                    <td style="text-align: right;">
                                        <%# Math.Round((Convert.ToDecimal(Convert.ToDecimal(Eval("Charges")) * 100)/ Convert.ToDecimal(Eval("ChequeAmount"))),1).ToString() %> %
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                <tr style="border: 1px solid black; font-weight: bold; font-family: Verdana;">
                                    <td></td>
                                    <td><span class="footerrow">Grand Total</span>
                                    </td>
                                    <td></td>
                                    <td></td>
                                    <td style="text-align: right;">
                                        <%= TotalChequeAmount %>
                                    </td>
                                    <td style="text-align: right;">
                                        <%= TotalCashChequeCharges  %>
                                    </td>
                                    <td style="text-align: right;">
                                        <%= TotalCashOut  %>
                                    </td>
                                    <td></td>
                                </tr>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </div>
            <div id="printareacurrency" runat="server">
                <div style="text-align: right; width: 100%; font-weight: bold; font-size: 15px;">
                    <input type="button" id="btnprintareacurrency" runat="server" value="Print" class="btn" />
                </div>
                <div id="printcurrency">
                    <div style="text-align: center; width: 100%; font-weight: bold; font-size: 15px;">
                        <asp:Label ID="lblCurrencyStoreInfo" runat="server"></asp:Label><br />
                    </div>
                    <div style="width: 100%; font-size: 15px; padding-bottom: 15px; padding-top: 15px;">
                        <table style="width: 80%;">
                            <tr>
                                <td style="width: 10%;">Date Range:
                                </td>
                                <td style="width: 36%;">
                                    <asp:Label ID="lblCurrencyDate" runat="server"></asp:Label>
                                </td>
                                <td style="width: 50%;" class="data">Transaction Report (Currency Exchange)
                                </td>
                            </tr>

                        </table>
                    </div>
                    <table style="width: 100%;" class="EU_DataTable table table-hover table-bordered">
                        <asp:Repeater ID="rptCurrency" runat="server">
                            <HeaderTemplate>

                                <th style="text-align: left; width: 8%;">Date
                                </th>
                                <th style="text-align: left; width: 20%;">Customer Name
                                </th>
                                <th style="text-align: left; width: 9%;">Customer Id
                                </th>
                                <th style="text-align: left; width: 12%;">Currency
                                </th>
                                <th style="text-align: center; width: 10%;">Currency Amount
                                </th>
                                <th style="text-align: right; width: 8%;">Exchange Rate
                                </th>
                                <th style="text-align: center; width: 10%;">Buy/Sell
                                </th>
                                <th style="text-align: right; width: 8%;">Service Charge
                                </th>
                                <th style="text-align: center; width: 12%;">Cash In/Out
                                </th>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>

                                        <%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "CreatedDate", "{0:MM/dd/yyyy}")).Replace("-","/") %>
                                    </td>
                                    <td>
                                        <%# Eval("CustomerName") %>
                                    </td>
                                    <td style="text-align: center;">
                                        <%# Eval("CustomerId") %>
                                    </td>
                                    <td style="text-align: left;">
                                        <%#  Eval("CurrencyType").ToString().Substring(5,(Eval("CurrencyType").ToString().Length-5)) %>
                                    </td>
                                    <td style="text-align: right;">
                                        <%#  Eval("CurrencyType").ToString().Substring(0,3) %>&nbsp;<%# Eval("ExchangeAmount") %>
                                    </td>
                                    <td style="text-align: right;">
                                        <%# Eval("ExchangeRate") %>
                                    </td>
                                    <td style="text-align: center;">
                                        <%# Eval("TransactionType") %>
                                    </td>
                                    <td style="text-align: right;">
                                        <%# Eval("ServiceCharge") %>
                                    </td>
                                    <td style="text-align: center;">C$ <%# Eval("DueAmount") %> 
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
