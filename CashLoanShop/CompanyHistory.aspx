<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CompanyHistory.aspx.cs" Inherits="CashLoanShop.CompanyHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script>
         $(document).ready(function () {
             $('.historybutton').click(function () {
                 var id = $(this).attr('id');
                 openhistory(id);
             });

         });
         function openhistory(obj) {
             window.open('/CompanyHistoryReport.aspx?Id=' + obj + '', '_blank', 'width=1200,height=750,location=no,left=200px');
         }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:MultiView ID="mvView" runat="server" ActiveViewIndex="0">
        <asp:View ID="vList" runat="server">
              <div class="page-content">
               <%-- <div class="page-header">
                    <h1>Company
                    </h1>
                </div>--%>
                <div class="row">
                    <!-- /.page-header -->
                    <div class="col-xs-12">
                        <div class="row">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Search Company</h3>
                                </div>
                                <div class="panel-body">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label" for="Carrier_RootCarrierName">Enter Company Name :</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox ID="txtSearchName" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label" for="Carrier_RootCarrierName"></label>
                                            <div class="controls col-md-7">
                                                <asp:Button ID="btnSearch" class="btn btn-primary" Text="Search" runat="server" OnClick="btnSearch_Click"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <asp:GridView Width="100%" ID="dgvCompany" CssClass="EU_DataTable" AllowPaging="true" PageSize="15" class="table table-hover table-bordered" AutoGenerateColumns="false" runat="server" OnRowCommand="dgvCompany_RowCommand" OnPageIndexChanging="dgvCompany_PageIndexChanging" OnRowDataBound="dgvCompany_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                         <input type="button" value="Select" class="historybutton" id='<%# Eval("Id") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Id" HeaderStyle-HorizontalAlign="Left" HeaderText="Company ID" />
                                                <asp:BoundField DataField="Name" HeaderStyle-HorizontalAlign="Left" HeaderText="Company Name" />
                                                <asp:BoundField DataField="Address" HeaderStyle-HorizontalAlign="Left" HeaderText="Company Address" />
                                                <asp:BoundField DataField="Phone" HeaderStyle-HorizontalAlign="Left" HeaderText="Phone" />
                                                <asp:BoundField DataField="BankTransitNumber" HeaderStyle-HorizontalAlign="Left" HeaderText="Bank Transit Number" />
                                                <asp:BoundField DataField="BankAccountNumber" HeaderStyle-HorizontalAlign="Left" HeaderText="Bank Account Number" />
                                                <asp:BoundField DataField="Status" HeaderStyle-HorizontalAlign="Left" HeaderText="Status" />

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
                    <!-- /span -->
                </div>

            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
