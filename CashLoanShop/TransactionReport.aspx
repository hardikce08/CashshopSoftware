<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="TransactionReport.aspx.cs" Inherits="CashLoanShop.TransactionReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:MultiView ID="mvView" runat="server" ActiveViewIndex="0">
        <asp:View ID="vList" runat="server">
            <div class="page-content">
                <div class="row">
                    <!-- /.page-header -->
                    <div class="col-xs-12">
                        <div class="row">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Store Transaction Details</h3>
                                </div>
                                <div class="panel-body">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Store :</label>
                                            <div class="controls col-md-7">
                                                <asp:DropDownList ID="ddlShopStore" class="form-control" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Report Type :</label>
                                            <div class="controls col-md-7">
                                                <asp:DropDownList ID="ddlReportType" class="form-control" runat="server">
                                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="PayDay Loan"></asp:ListItem>
                                                    <asp:ListItem Value="4" Text="Term Loan"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Cheque Cash"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Currency Exchange"></asp:ListItem>
                                                       
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">From Date :</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox ID="txtFromDate" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">To Date :</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox ID="txtToDate" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label"></label>
                                            <div class="controls col-md-7">
                                                <input type="button" value="View Report" class="btn btn-primary historybutton" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

        </asp:View>
    </asp:MultiView>
    <script type="text/javascript">

        $(function () {
            $('#' + '<%= txtFromDate.ClientID %>').datetimepicker({
                format: 'L'
            });
            $('#' + '<%= txtToDate.ClientID %>').datetimepicker({
                format: 'L'
            });
        });

    </script>
    <script>
        $(document).ready(function () {
            $('.historybutton').click(function () {
                var StoreId = $('#' + '<%= ddlShopStore.ClientID %>').val();
                var ReportType = $('#' + '<%= ddlReportType.ClientID %>').val();
                var FromDate = $('#' + '<%= txtFromDate.ClientID %>').val();
                var ToDate = $('#' + '<%= txtToDate.ClientID %>').val();
                //openhistory(id);
                //alert(FromDate);
                window.open('/ViewTransactionReport.aspx?StoreId=' + StoreId + '&ReportType=' + ReportType + '&FromDate=' + FromDate + '&ToDate=' + ToDate + '', '_blank', 'width=1400,height=750,location=no,left=200px');
            });

        });

    </script>

</asp:Content>
