<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerLoanReceipt.aspx.cs" Inherits="CashLoanShop.CustomerLoanReceipt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/screen.css" rel="stylesheet" />
    <style>
        .w3-check, .w3-radio {
            width: 30px;
            height: 30px;
            position: relative;
            top: 6px;
            margin-bottom: 8px;
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
                        <td style="width: 40%;">Transaction Type:
                        </td>
                        <td style="width: 60%;">
                            <asp:Label ID="lblTransactionType" runat="server" Text="Payday Loan" Style="font-weight: bold;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Receipt Number:
                        </td>
                        <td>
                            <asp:Label ID="lblReceiptNumber" runat="server" Style="font-weight: bold;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Date/Time:
                        </td>
                        <td>
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
                        <td>Loan Amount:
                        </td>
                        <td>
                            <asp:Label ID="lblLoanAmount" runat="server" Style="font-weight: bold;"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td>Borrowing Cost:
                        </td>
                        <td>
                            <asp:Label ID="lblAdminFee" runat="server" Style="font-weight: bold;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Due Amount:
                        </td>
                        <td>
                            <asp:Label ID="lblDueAmount" runat="server" Style="font-weight: bold;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Due Date:
                        </td>
                        <td>
                            <asp:Label ID="lblDueDate" runat="server" Style="font-weight: bold;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Cash Out:
                        </td>
                        <td>
                            <asp:Label ID="lblCashout" runat="server" Style="font-weight: bold;"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="hr">
                <hr />
            </div>
            <div style="padding-top: 45px; padding-bottom: 45px;">I hereby consent to entering into the agreement and have received the amount borrowed in cash and have received copies of all agreements.</div>
            <div id="dvextra" runat="server">
                <div style="padding-top: 45px; padding-bottom: 10px;">The lender '<asp:Label ID="lblBusinessname" runat="server" Style="font-weight: bold;"></asp:Label>' has offered me to repay my loan in installments  </div>
                <div>
                    <input type="checkbox" class="w3-check" />I have declined the offer</div>
                <div>
                    <input type="checkbox"  class="w3-check"  />I want to pay my full loan on my next pay</div>
            </div>
            <div style="padding-top: 45px;">_________________________</div>
            <div><b>Customer Signature</b></div>
            <div>
                Name: &nbsp;<asp:Label ID="lblCustomerNames" runat="server"></asp:Label><br />Address: &nbsp;<asp:Label ID="lblAddress" runat="server"></asp:Label><br />Phone: <asp:Label ID="lblPhone" runat="server"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
