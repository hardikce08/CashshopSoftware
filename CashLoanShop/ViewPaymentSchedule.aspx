<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewPaymentSchedule.aspx.cs" Inherits="CashLoanShop.ViewPaymentSchedule" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/screen.css" rel="stylesheet" />
        <link href="css/gridview.css" rel="stylesheet" />
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
                frameDoc.document.write('<link href="css/gridview.css" rel="stylesheet" type="text/css" />');
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
            <input type="button" id="btnPrint" value="Print" class="btn" />
        </div>
        <br />
        <div id="printarea">
            <table style="width: 100%; padding-top: 20px;" class="EU_DataTable table table-hover table-bordered">
                <asp:Repeater ID="rptGridData" runat="server" >
                    <HeaderTemplate>
                        <th>Installment Number
                        </th>
                        <th>Date
                        </th>
                        <th>Amount
                        </th>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Label ID="lblRowNumber" Text='<%# Container.ItemIndex + 1 %>' runat="server" />
                            </td>
                            <td>
                                <%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "Date", "{0:MM/dd/yyyy}")).Replace("-","/") %>
                            </td>
                            <td>
                                <%# Eval("Amount") %>
                            </td>
                            
                        </tr>
                    </ItemTemplate>

                </asp:Repeater>
            </table>
        </div>
    </form>
</body>
</html>
