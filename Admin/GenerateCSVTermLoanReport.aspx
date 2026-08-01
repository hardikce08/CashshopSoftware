<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="GenerateCSVTermLoanReport.aspx.cs" Inherits="CashLoanShop.Admin.GenerateCSVTermLoanReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
      <asp:MultiView ID="mvView" runat="server" ActiveViewIndex="0">
        <asp:View ID="vList" runat="server">
            <div class="page-content">
                <div class="col-xs-12">
                    <div class="row">
                        <div class="form-group">
                            <label class="col-md-2 control-label">From Date :</label>
                            <div class="controls col-md-2">
                                <asp:TextBox ID="txtFromDate" class="form-control" runat="server"></asp:TextBox>
                            </div>
                             <label class="col-md-2 control-label">To Date :</label>
                            <div class="controls col-md-2">
                                <asp:TextBox ID="txtToDate" class="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <table style="width: 60%;">
                            <tr>
                                <td>
                                    <asp:GridView Width="100%" ID="dgvCustomer" CssClass="EU_DataTable" AllowPaging="true" PageSize="150" class="table table-hover table-bordered" AutoGenerateColumns="false" runat="server">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HyperLink   ID="lnkDownload" runat="server" Text="Download" style="cursor:pointer;" data-Id='<%# Eval("StoreId")%>' CssClass="download"></asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:BoundField DataField="StoreId" HeaderStyle-HorizontalAlign="Left" HeaderText="StoreId" />
                                       <%--     <asp:BoundField DataField="UserName" HeaderStyle-HorizontalAlign="Left" HeaderText="UserName" />--%>
                                            <asp:TemplateField HeaderText="StoreName & Address" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStoreName" Text='<%# Eval("StoreName").ToString().Replace("$",",") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <div style="color: red; text-align: center;">No records found</div>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </asp:View>


    </asp:MultiView>
    <script type="text/javascript">


        $(document).ready(function () {

            $('#' + '<%= txtFromDate.ClientID %>').datetimepicker({
                format: 'L'
            });
            $('#' + '<%= txtToDate.ClientID %>').datetimepicker({
                format: 'L'
            });
            $('.numerictext').keydown(function (e) {
                //alert(e.keyCode);
                //return (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57) && e.which != 46 && e.which != 110 && e.which != 190) ? false : true;
                if (e.shiftKey || e.ctrlKey || e.altKey) {
                    e.preventDefault();
                } else {
                    var key = e.keyCode;
                    if (!((key == 8) || (key == 9) || (key == 46) || (key == 190) || (key == 110) || (key == 188) || (key >= 35 && key <= 40) || (key >= 48 && key <= 57) || (key >= 96 && key <= 105))) {
                        e.preventDefault();
                    }
                }
            });
            $('.download').click(function () {
                //alert($(this).data("id"));
                window.open('/Admin/DownloadTermLoanReport.aspx?Id=' + $(this).data("id") + '&fromDate=' + $('#' + '<%= txtFromDate.ClientID %>').val() +'&ToDate=' + $('#' + '<%= txtToDate.ClientID %>').val(), '_self', 'width=1200,height=750,location=no,left=200px');
            });
        });

    </script>
</asp:Content>
