 <%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="LoanCloseReport.aspx.cs" Inherits="CashLoanShop.LoanCloseReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            $('.historybutton').click(function () {
                var id = $(this).attr('id');
                openhistory(id);
            });

        });
        function openhistory(obj) {
            window.open('/CustomerCashHistory.aspx?Id=' + obj + '&IsCurrencyExchange=true&IsLoan=true', '_blank', 'width=1400,height=750,location=no,left=200px');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <table style="width: 100%;">
        <tr>
            <td>
                <asp:GridView Width="99%" ID="dgvCustomer" CssClass="EU_DataTable" AllowPaging="true" PageSize="15" class="table table-hover table-bordered" AutoGenerateColumns="false" runat="server" OnPageIndexChanging="dgvCustomer_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <%--<asp:Button ID="btnSelect" runat="server" Text="Select" CommandName="Select" CommandArgument='<%# Eval("Id") %>'></asp:Button>--%>
                                <input type="button" value="View History" class="historybutton" id='<%# Eval("Id") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Id" HeaderStyle-HorizontalAlign="Left" HeaderText="Customer ID" />
                        <asp:BoundField DataField="FirstName" HeaderStyle-HorizontalAlign="Left" HeaderText="FirstName" />
                        <asp:BoundField DataField="LastName" HeaderStyle-HorizontalAlign="Left" HeaderText="Last Name" />
                          <asp:BoundField DataField="HomePhone" HeaderStyle-HorizontalAlign="Left" HeaderText="HomePhone" />
                        <asp:BoundField DataField="CellPhone" HeaderStyle-HorizontalAlign="Left" HeaderText="CellPhone" />
   <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Address">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "Address").ToString() %>,
 <%# DataBinder.Eval(Container.DataItem, "City").ToString() %>
                                        , <%# DataBinder.Eval(Container.DataItem, "Province").ToString() %>
, <%# DataBinder.Eval(Container.DataItem, "PostCode").ToString() %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                          <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Last Close Date">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "LoanCloseDate", "{0:MM/dd/yyyy}").ToString().Replace("-","/") %>
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
</asp:Content>
