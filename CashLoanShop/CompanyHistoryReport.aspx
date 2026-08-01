<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyHistoryReport.aspx.cs" Inherits="CashLoanShop.CompanyHistoryReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/screen.css" rel="stylesheet" />
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
                //frameDoc.document.write('<link href="css/screen.css" rel="stylesheet" type="text/css" />');
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
            <div style="text-align: center; width: 100%; font-weight: bold; font-size: 20px;padding-bottom:35px;">
                <table>
                    <tr>
                        <b>Company History</b>

                    </tr>
                </table>
            </div>
            <table>
                <asp:Repeater ID="rptCompany" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>Company Name:</td>
                            <td>&nbsp;</td>
                            <td colspan="2" class="data">
                                <b><%# Eval("Name") %>  </b></td>
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
                                 <td>&nbsp;Phone:</td>
                                 <td>&nbsp;</td>
                                 <td colspan="2" class="data"><%# Eval("Phone") %>  </td>
                             </tr>
                        <tr>
                            <td>Bank Transit Number:</td>
                            <td>&nbsp;</td>
                            <td colspan="2" class="data">
                                <%# Eval("BankTransitNumber") %>    </td>
                            <td>&nbsp;Bank Account Number:</td>
                            <td>&nbsp;</td>
                            <td colspan="2" class="data"><%# Eval("BankAccountNumber") %> </td>
                        </tr>
                        <tr>
                            <td>Company Status:</td>
                            <td>&nbsp;</td>
                            <td colspan="2" class="data">
                                <%# Eval("Status") %>    </td>

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
                    <table style="width:100%;">
                        <tr>
                            <td style="width:60%;">
                                <table style="width:100%;">
                                    <tr>
                                        <td>Date:</td>
                                        <td>&nbsp;</td>
                                        <td colspan="2" class="data">
                                            <b><%# DataBinder.Eval(Container.DataItem, "CreatedDate", "{0:MM/dd/yyyy}") %>   </b></td>
                                        <td>&nbsp;Customer Name:</td>
                                        <td>&nbsp;</td>
                                        <td colspan="2" class="data"><%# Eval("CustomerName") %></td>
                                    </tr>
                                    <tr>
                                        <td>Cheque Type:</td>
                                        <td>&nbsp;</td>
                                        <td colspan="2" class="data">
                                            <%# Eval("ChequeType") %>    </td>
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
                                </table>
                            </td>
                            <td style="padding-left: 15px;width:40%;">
                                <table style="width:100%;">
                                    <tr>
                                        <td>
<%# Eval("StoreAddress") %>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>

                    <div class="fhr">
                        <hr />
                    </div>
                </ItemTemplate>

            </asp:Repeater>
        </div>
    </form>
</body>
</html>
