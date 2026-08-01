<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewDiscountReport.aspx.cs" Inherits="CashLoanShop.ViewDiscountReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Discount Report</title>
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

        tr {
            line-height: 20px !important;
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
        <div id="printarea" runat="server">
            <div style="text-align: right; width: 100%; font-weight: bold; font-size: 15px;">
                <input type="button" id="btnprintarea" runat="server" value="Print" class="btn" />
            </div>
            <div id="printloandata">
                <div style="text-align: center; width: 100%; font-weight: bold; font-size: 15px;">
                    <asp:Label ID="lblStoreInfo" runat="server"></asp:Label><br />
                    <br />
                    DISCOUNT REPORT<br />
                    ( From: 
                                <asp:Label ID="lblDateRange" runat="server"></asp:Label>
                    )<br />
                    <br />
                </div>
                <table style="width: 100%; padding-top: 20px;" class="EU_DataTable table table-hover table-bordered">
                    <tr>
                        <h4 style="text-align: center; font-size: 25px;">Payday Loan</h4> <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="ExportToExcel" />
                    </tr>
                    <asp:Repeater ID="rptGridData" runat="server" OnItemDataBound="rptGridData_ItemDataBound">
                        <HeaderTemplate>
                            <th></th>
                            <th>CONTRACT NUMBER
                            </th>
                            <th>CUSTOMER ID
                            </th>
                             <th>STORE ID
                            </th>
                             <th>DISCOUNT GIVEN DATE
                            </th>
                            <th>LOAN STATUS
                            </th>
                            <th>CUSTOMER NAME
                            </th>
                            <th>LOAN AMOUNT
                            </th>
                            <th>TOTAL DISCOUNT GIVEN
                            </th>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td></td>
                                <td>
                                    <%--  <%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "CreatedDate", "{0:MM/dd/yyyy}")).Replace("-","/") %>--%>
                                    <%# Eval("LoanId") %>
                                </td>
                                <td>
                                    <%# Eval("CustomerId") %>
                                </td>
                                 <td>
                                    <%# Eval("StoreId") %>
                                </td>
                                 <td>
                                     <%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "UpdatedDate", "{0:MM/dd/yyyy}")).Replace("-","/") %>
                                </td>
                                <td>
                                    <%# Eval("LoanStatus") %>
                                </td>

                                <td style="width: 15%">
                                    <%# Eval("Name") %>
                                </td>

                                <td>
                                    <%# Eval("LoanAmount") %>
                                </td>
                                <td>
                                    <%# Eval("DiscountAmount") %>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr id="trData" runat="server" style="border: 1px solid black; font-weight: bold; font-family: Verdana;">
                                <td><span class="footerrow">Grand Total</span></td>
                                <td></td>
                                    <td></td>
                                    <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td style="font-size: 15px !important;">
                                    <%= TotalLoanAmount  %>
                                </td>
                                <td style="font-size: 15px !important;"><%= TotalDiscount  %></td>
                            </tr>
                            <tr id="trEmpty" runat="server" visible="false">
            <td colspan = "7" align = "center">
                No records found.
            </td>
        </tr>
                        </FooterTemplate>
                    </asp:Repeater>
                </table>
                  <table style="width: 100%; padding-top: 20px;" class="EU_DataTable table table-hover table-bordered">
                    <tr>
                        <h4 style="text-align: center; font-size: 25px;">Term Loan</h4> <asp:Button ID="Button1" runat="server" Text="Export" OnClick="ExportToExcelTermLoan" />
                    </tr>
                    <asp:Repeater ID="rptTermLoanData" runat="server" OnItemDataBound="rptTermLoanData_ItemDataBound">
                        <HeaderTemplate>
                            <th></th>
                            <th>CONTRACT NUMBER
                            </th>

                            <th>CUSTOMER ID
                            </th>
                             <th>STORE ID
                            </th>
                             <th>DISCOUNT GIVEN DATE
                            </th>
                            <th>LOAN STATUS
                            </th>
                            <th>CUSTOMER NAME
                            </th>
                            <th>LOAN AMOUNT
                            </th>
                            <th>TOTAL DISCOUNT GIVEN
                            </th>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td></td>
                                <td>
                                    <%--  <%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "CreatedDate", "{0:MM/dd/yyyy}")).Replace("-","/") %>--%>
                                    <%# Eval("LoanId") %>
                                </td>
                                <td>
                                    <%# Eval("CustomerId") %>
                                </td>
                                <td>
                                    <%# Eval("StoreId") %>
                                </td>
                                 <td>
                                     <%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "UpdatedDate", "{0:MM/dd/yyyy}")).Replace("-","/") %>
                                </td>
                                <td>
                                    <%# Eval("LoanStatus") %>
                                </td>

                                <td style="width: 15%">
                                    <%# Eval("Name") %>
                                </td>

                                <td>
                                    <%# Eval("LoanAmount") %>
                                </td>
                                <td>
                                    <%# Eval("DiscountAmount") %>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr id="trData" runat="server" style="border: 1px solid black; font-weight: bold; font-family: Verdana;">
                                <td><span class="footerrow">Grand Total</span></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td style="font-size: 15px !important;">
                                    <%= TotalLoanAmountTerm  %>
                                </td>
                                <td style="font-size: 15px !important;"><%= TotalDiscountTerm  %></td>
                            </tr>
                            <tr id="trEmpty" runat="server" visible="false">
            <td colspan = "9" align = "center">
                No records found.
            </td>
        </tr>
                        </FooterTemplate>
                        
                    </asp:Repeater>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
