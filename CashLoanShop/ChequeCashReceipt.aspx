<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChequeCashReceipt.aspx.cs" Inherits="CashLoanShop.ChequeCashReceipt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <link href="css/screen.css" rel="stylesheet" />
<%--    <link href="css/print.css" rel="stylesheet" />
  <script type="text/javascript">
      /*--This JavaScript method for Print command--*/
      function PrintDoc() {
          var toPrint = document.getElementById('printarea');
          var popupWin = window.open('', '_blank', 'width=350,height=150,location=no,left=200px');
          popupWin.document.open();
          popupWin.document.write('<html><link rel="stylesheet" type="text/css" href="css/print.css" /><body onload="window.print()">')
          popupWin.document.write(toPrint.innerHTML);
          popupWin.document.write('</body></html>');
          popupWin.document.close();
      }
      /*--This JavaScript method for Print Preview command--*/
      function PrintPreview() {
          var toPrint = document.getElementById('printarea');
          var popupWin = window.open('', '_blank', 'width=350,height=150,location=no,left=200px');
          popupWin.document.open();
          popupWin.document.write('<html><link rel="stylesheet" type="text/css" href="css/Print.css" media="screen"/><body>')
          popupWin.document.write(toPrint.innerHTML);
          popupWin.document.write('</body></html>');
          popupWin.document.close();
      }
</script>--%>
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
      
        <div style="text-align: right; width: 50%; font-weight: bold; font-size: 15px;">

            <%--<input type="button" onclick="PrintPartOfPage('dvprint')" value="Print" />--%>
            <%--<input type="button" value="Print" class="btn" onclick="PrintDoc()"/>--%>
             <input type="button" id="btnPrint" value="Print" class="btn" />
           <%-- <input type="button" value="Print Preview" class="btn" onclick="PrintPreview()"/>
            <input type="button" value="Close" />--%>

        </div>
        <div id="printarea">

            <div style="text-align: center;width: 100%; font-weight: bold;padding-bottom:60px; font-size: 15px;">
               <%-- <table>
                    <tr>
                        Payday Loan Mart<br />
                    764 Guelph Line Unit 2,
                    <br />
                        Burlington,Ontario L6R 3N5
                    <br />
                        905-634-6278

                    </tr>
                    paydayloanmart@gmail.com
                </table>--%>
                <asp:Label ID="lblStoreInfo" runat="server"></asp:Label><br />
                 
            </div>
            <div style="width: 100%; font-size: 15px;">
                <table style="width: 50%;">
                    <tr>
                        <td style="width: 40%;">Transaction Type:
                        </td>
                        <td style="width: 60%;">
                            <asp:Label ID="lblTransactionType" runat="server" Text="Cheque Cash" Style="font-weight: bold;"></asp:Label>
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
                <asp:Label ID="lblCustomerId" runat="server"></asp:Label>
            </div>
            <div style="width: 50%; font-weight: bold; font-size: 20px; text-transform: uppercase; padding-top: 15px;">
                <asp:Label ID="lblCustomerName" runat="server"></asp:Label>
            </div>
            <div class="hr">
                <hr />
            </div>
            <div style="width: 70%; font-size: 15px;">
                <table style="width: 40%;">
                      <tr>
                        <td>Cheque Type:
                        </td>
                        <td>
                            <asp:Label ID="lblChequeType" runat="server" Style="font-weight: bold;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Cheque Amount:
                        </td>
                        <td>
                            <asp:Label ID="lblChqAmount" runat="server" Style="font-weight: bold;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Charges:
                        </td>
                        <td>
                            <asp:Label ID="lblCharges" runat="server" Style="font-weight: bold;"></asp:Label>
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
            <div style="padding-top: 45px;">_________________________</div>
            <div><b>Customer Signature</b></div>
        </div>
    </form>
</body>
</html>
