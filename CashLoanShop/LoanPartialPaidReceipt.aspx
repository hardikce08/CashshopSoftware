<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoanPartialPaidReceipt.aspx.cs" Inherits="CashLoanShop.LoanPartialPaidReceipt" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/screen.css" rel="stylesheet" />
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
    <style type="text/css">
         body table td {
                font-size: 12px !important;
                font-family: Verdana;
            }
        body table {
            border:1px solid black;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: right; width: 80%; font-weight: bold; font-size: 15px;">

            <%--<input type="button" onclick="PrintPartOfPage('dvprint')" value="Print" />--%>
            <%--<input type="button" value="Print" class="btn" onclick="PrintDoc()"/>--%>
            <input type="button" id="btnPrint" value="Print" class="btn" />
            <%-- <input type="button" value="Print Preview" class="btn" onclick="PrintPreview()"/>
            <input type="button" value="Close" />--%>
        </div>
        <div id="printarea">

            <div style="text-align: center; width: 100%; font-weight: bold; font-size: 15px;">
                <asp:Label ID="lblStoreInfo" runat="server"></asp:Label><br />
            </div>
            <div style="width: 100%; font-size: 15px;">
                <table style="width: 50%;">
                    <tr>
                        <td style="width: 40%;border:1px solid black;">Transaction Type:
                        </td>
                        <td style="width: 60%;border:1px solid black;"">
                            <asp:Label ID="lblTransactionType" runat="server" Text="Payday Loan" Style="font-weight: bold;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="border:1px solid black;">Receipt Number:
                        </td>
                        <td style="border:1px solid black;">
                            <asp:Label ID="lblReceiptNumber" runat="server" Style="font-weight: bold;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="border:1px solid black;">Date/Time:
                        </td>
                        <td style="border:1px solid black;">
                            <asp:Label ID="lblDateTime" runat="server" Style="font-weight: bold;"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="width: 50%; font-weight: bold; font-size: 20px; text-transform: uppercase; padding-top: 15px;">
                <asp:Label ID="lblCustomerName" runat="server"></asp:Label>
            </div>
            <div class="hr">
                <hr />
            </div>
            <div style="width: 70%; font-size: 15px;">
                <table style="width: 60%;">
                    <tr>
                        <td style="border:1px solid black;">Loan Amount:
                        </td>
                        <td style="border:1px solid black;">
                            <asp:Label ID="lblLoanAmount" runat="server" Style="font-weight: bold;"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td style="border:1px solid black;">Borrowing Cost:
                        </td>
                        <td style="border:1px solid black;">
                            <asp:Label ID="lblAdminFee" runat="server" Style="font-weight: bold;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="border:1px solid black;">Original Due Amount:
                        </td>
                        <td style="border:1px solid black;">
                            <asp:Label ID="lblDueAmount" runat="server" Style="font-weight: bold;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="border:1px solid black;">Due Date:
                        </td>
                        <td style="border:1px solid black;">
                            <asp:Label ID="lblDueDate" runat="server" Style="font-weight: bold;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="border:1px solid black;">Late Interest Charges:
                        </td>
                        <td style="border:1px solid black;">
                            <asp:Label ID="lblLateInterestCharges" runat="server" Style="font-weight: bold;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="border:1px solid black;">NSF Charges:
                        </td>
                        <td style="border:1px solid black;">
                            <asp:Label ID="lblNSFCharges" Text="$0.00" runat="server" Style="font-weight: bold;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="border:1px solid black;">Total Amount Due:
                        </td>
                        <td style="border:1px solid black;">
                            <asp:Label ID="lblTotalDueAmount" Text="$45.00" runat="server" Style="font-weight: bold;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="border:1px solid black;">Last Due Amount:
                        </td>
                        <td style="border:1px solid black;">
                            <asp:Label ID="lblLastDueAmount" Text="$45.00" runat="server" Style="font-weight: bold;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="border:1px solid black;">Cash Paid:
                        </td>
                        <td style="border:1px solid black;">
                            <asp:Label ID="lblCashpaid" runat="server" Style="font-weight: bold;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="border:1px solid black;">Total Partial Amount Paid:
                        </td>
                        <td style="border:1px solid black;">
                            <asp:Label ID="lblTotalPartialAmountPaid" runat="server" Style="font-weight: bold;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="border:1px solid black;">Discount Amount:
                        </td>
                        <td style="border:1px solid black;">
                            <asp:Label ID="lblDiscount" runat="server" Style="font-weight: bold;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="border:1px solid black;">Balance Due:
                        </td>
                        <td style="border:1px solid black;">
                            <asp:Label ID="lblBalanceDue" runat="server" Style="font-weight: bold;"></asp:Label>
                        </td>
                    </tr>
                  <%--  <tr>
                        <td style="border:1px solid black;">Next Payment Date:
                        </td>
                        <td style="border:1px solid black;">
                            <asp:Label ID="lblNextPaymentDate" runat="server" Style="font-weight: bold;"></asp:Label>
                        </td>
                    </tr>--%>
                </table>
            </div>
            <div class="hr">
                <hr />
            </div>
            <div style="padding-top: 45px; padding-bottom: 45px;">I hereby consent to entering into the agreement and have received the amount borrowed in cash and have received copies of all agreements.</div>
            <div style="padding-top: 45px;">_________________________</div>
            <div><b>Customer Signature</b></div>
        </div>
    </form>
</body>
</html>

