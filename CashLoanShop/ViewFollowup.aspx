<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewFollowup.aspx.cs" Inherits="CashLoanShop.ViewFollowup" %>

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
        <div id="masterprintarea" runat="server">
            <div id="printarea" runat="server">
                <div style="text-align: right; width: 100%; font-weight: bold; font-size: 15px;">
                    <input type="button" id="btnprintarea" runat="server" value="Print" class="btn" />
                </div>
                <div id="printloandata">
                         <table style="width: 100%; padding-top: 20px;" class="EU_DataTable table table-hover table-bordered">
                        <tr>
                            <h4 style="text-align: center; font-size: 25px;">Followup</h4>
                        </tr>
                        <asp:Repeater ID="rptGridData" runat="server" >
                            <HeaderTemplate>
                                <th>Date
                                </th>
                                <th>Time
                                </th>
                                <th>Customer Name
                                </th>
                                <th>Customer Id
                                </th>
                                <th>Contact No
                                </th>
                                <%--<th>Final Status
                                </th>
                                <th>Followup Code
                                </th>
                                <th>Comment
                                </th>
                                <th>Followup Done by
                                </th>--%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "FollowupDate", "{0:MM/dd/yyyy}")).Replace("-","/") %>
                                    </td>
                                    <td>
                                        <%# Eval("FollowupTime") %>
                                    </td>
                                    <td>
                                        <%# Eval("CustomerName") %>
                                    </td>
                                    <td>
                                        <%# Eval("CustomerId") %>
                                    </td>
                                    <td>
                                        <%# Eval("ContactNo") %>
                                    </td>
                                   <%-- <td>
                                        <%# Eval("FinalStatus") %>
                                    </td>
                                    <td>
                                        <%# Eval("FollowupCode") %>
                                    </td>
                                    <td>
                                        <%# Eval("Comments") %>
                                    </td>
                                    <td>
                                        <%# Eval("FollowupDoneBy") %>
                                    </td>--%>
                                   
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
