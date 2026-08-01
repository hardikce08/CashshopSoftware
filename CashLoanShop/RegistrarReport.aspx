<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RegistrarReport.aspx.cs" Inherits="CashLoanShop.RegistrarReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="page-content">
        <div class="row">
            <!-- /.page-header -->
            <div class="col-xs-12">
                <div class="row">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">Registrar Details</h3>
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
                                        <asp:Button ID="btnSearch" class="btn btn-primary" Text="Search" runat="server" OnClick="btnSearch_Click"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <table border="0" style="width: 50%;">
                        <tr>
                            <td style="align-content: center;"><span style="font-weight: bold;">Loan Activity Report<br />
                                Reporting Period:
                <asp:Label ID="lblFromDate" runat="server"></asp:Label>
                                to
                <asp:Label ID="lblToDate" runat="server"></asp:Label></span></td>
                        </tr>
                        <tr>
                            <td style="align-content: center;">
                                <span style="font-weight: bold;">
                                    <asp:Label ID="lblStoreAddress" runat="server"></asp:Label></span>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <asp:GridView   ID="dgvData" CssClass="EU_DataTable" AllowPaging="true" PageSize="35" class="table table-hover table-bordered" AutoGenerateColumns="false" runat="server">
                                    <Columns>
                                        <asp:BoundField DataField="Key" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="" />
                                        <asp:BoundField DataField="Value" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderText="" />
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
    </div>


    <%-- <div id="content" runat="server"></div>--%>

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
</asp:Content>
