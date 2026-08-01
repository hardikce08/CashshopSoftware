<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="InterestEarnedReport.aspx.cs" Inherits="CashLoanShop.Admin.InterestEarnedReport" %>
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
                                    <h3 class="panel-title">Report</h3>
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
                                            <label class="col-md-5 control-label">Month :</label>
                                            <div class="controls col-md-7">
                                                <asp:DropDownList ID="ddlMonth" class="form-control" runat="server">
                                                    <asp:ListItem Value="01" Text="January"></asp:ListItem>
                                                    <asp:ListItem Value="02" Text="February"></asp:ListItem>
                                                    <asp:ListItem Value="03" Text="March"></asp:ListItem>
                                                    <asp:ListItem Value="04" Text="April"></asp:ListItem>
                                                    <asp:ListItem Value="05" Text="May"></asp:ListItem>
                                                    <asp:ListItem Value="06" Text="June"></asp:ListItem>
                                                    <asp:ListItem Value="07" Text="July"></asp:ListItem>
                                                    <asp:ListItem Value="08" Text="August"></asp:ListItem>
                                                    <asp:ListItem Value="09" Text="September"></asp:ListItem>
                                                    <asp:ListItem Value="10" Text="October"></asp:ListItem>
                                                    <asp:ListItem Value="11" Text="November"></asp:ListItem>
                                                    <asp:ListItem Value="12" Text="December"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Year :</label>
                                            <div class="controls col-md-7">
                                                <asp:DropDownList ID="ddlYear" class="form-control" runat="server" />
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
    <script>
        $(document).ready(function () {
            $('.historybutton').click(function () {
                var StoreId = $('#' + '<%= ddlShopStore.ClientID %>').val();

                var Month = $('#' + '<%= ddlMonth.ClientID %>').val();
                var Year = $('#' + '<%= ddlYear.ClientID %>').val();
                //openhistory(id);
                //alert(FromDate);
                if (StoreId == "0")
                {
                    alert('Please select store');
                    return;
                }
                window.open('/ViewInterestEarnedReport.aspx?StoreId=' + StoreId + '&FromDate=' + Month + '-01-' + Year + '', '_blank', 'width=1400,height=700,location=no,left=100px');
            });

        });

    </script>
</asp:Content>
