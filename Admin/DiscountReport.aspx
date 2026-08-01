<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="DiscountReport.aspx.cs" Inherits="CashLoanShop.Admin.DiscountReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
     <div class="page-content">
        <div class="col-xs-12">
            <div class="row">
                <div class="form-group">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">Select Criteria</h3>
                        </div>
                        <div class="panel-body">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label class="col-md-5 control-label">Store :</label>
                                    <div class="controls col-md-7">
                                        <asp:DropDownList ID="ddlShopStore" class="form-control" multiple="multiple" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                              <%--  <div class="form-group">
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
                                </div>--%>
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
    <script type="text/javascript" src="../js/bootstrap-multiselect.js"></script>
<link rel="stylesheet" href="../css/bootstrap-multiselect.css" type="text/css"/>
    <script type="text/javascript">

        $(function () {
            $('#' + '<%= txtFromDate.ClientID %>').datetimepicker({
                format: 'L'
            });
            $('#' + '<%= txtToDate.ClientID %>').datetimepicker({
                format: 'L'
            });
            $('#content_ddlShopStore').multiselect();
        });

    </script>
    <script>
        $(document).ready(function () {
            $('.historybutton').click(function () {
                var StoreId = $('#' + '<%= ddlShopStore.ClientID %>').val();
                <%--var ReportType = $('#' + '<%= ddlReportType.ClientID %>').val();--%>
                var FromDate = $('#' + '<%= txtFromDate.ClientID %>').val();
                var ToDate = $('#' + '<%= txtToDate.ClientID %>').val();
                //openhistory(id);
                //alert(FromDate);
                var selected = '';
                $('#content_ddlShopStore :selected').each(function () {
                    selected = selected+','+ $(this).val();
                });
                
                //alert(selected.substring(1,selected.length));
                //return false;
                window.open('/ViewDiscountReport.aspx?StoreId=' + StoreId + '&FromDate=' + FromDate + '&ToDate=' + ToDate + '', '_blank', 'width=1400,height=750,location=no,left=200px');
            });

        });

    </script>
</asp:Content>
