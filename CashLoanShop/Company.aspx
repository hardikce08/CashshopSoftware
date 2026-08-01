<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Company.aspx.cs" Inherits="CashLoanShop.Company" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function validateform() {
            //alert($("#TxtFirstName").val());
            if ($('#' + '<%= txtCompanyName.ClientID %>').val() == '' || $('#' + '<%= txtCompanyName.ClientID %>').val() == undefined) {
                alert('Please enter CompanyName');

                $('#' + '<%= txtCompanyName.ClientID %>').focus();
                $('#' + '<%= txtCompanyName.ClientID %>').attr("style", "border-color:red;");
                return false;
            }

            else {
                $('#' + '<%= txtCompanyName.ClientID %>').removeAttr("style");
            }
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
                                                <asp:Button ID="btnAddNew" class="btn btn-pink" Text="Add New +" runat="server" OnClick="btnAddNew_Click"></asp:Button>
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
                                                        <asp:Button ID="btnSelect" runat="server" Text="Select" CommandName="Select" CommandArgument='<%# Eval("Id") %>'></asp:Button>
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
        <asp:View ID="vAdd" runat="server">
          <%--  <div class="page-header">
                <h1>Company Information
                </h1>
            </div>--%>
            <div class="page-content">
                <div class="row">
                    <!-- /.page-header -->
                    <div class="col-xs-12">
                        <div class="row">

                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Add/Edit Company Information</h3>
                                </div>
                                <div class="panel-body">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label" for="Carrier_RootCarrierName">Company Name:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="txtCompanyName" MaxLength="100" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label" for="Carrier_RootCarrierName">City:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="TxtCity" MaxLength="100" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label" for="Carrier_RootCarrierName">PostCode:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="TxtPostCode" MaxLength="100" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label" for="Carrier_RootCarrierName">Bank Account Number:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="txtBankAccountNumber" MaxLength="100" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label" for="Carrier_RootCarrierName">Status:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="txtStatus" TextMode="MultiLine" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label" for="Carrier_RootCarrierName">Address:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="txtAddress" TextMode="MultiLine" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label" for="Carrier_RootCarrierName">Province:</label>
                                            <div class="controls col-md-7">
                                                <asp:DropDownList ID="ddlProvince" CssClass="select" runat="server">
                                                    <asp:ListItem Text="Select Province" Value="0"></asp:ListItem>
                                                    <asp:ListItem Value="1">Alberta</asp:ListItem>
                                                    <asp:ListItem Value="2">British Columbia</asp:ListItem>
                                                    <asp:ListItem Value="3">Manitoba</asp:ListItem>
                                                    <asp:ListItem Value="4">New Brunswick</asp:ListItem>
                                                    <asp:ListItem Value="5">Newfoundland</asp:ListItem>
                                                    <asp:ListItem Value="6">Nova Scotia</asp:ListItem>
                                                    <asp:ListItem Value="7">Northwest Territories</asp:ListItem>
                                                    <asp:ListItem Value="8">Ontario</asp:ListItem>
                                                    <asp:ListItem Value="9">Prince Edward Island</asp:ListItem>
                                                    <asp:ListItem Value="10">Quebec</asp:ListItem>
                                                    <asp:ListItem Value="11">Saskatchewan</asp:ListItem>
                                                    <asp:ListItem Value="12">Yukon</asp:ListItem>
                                                    <asp:ListItem Value="13">Other</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label" for="Carrier_RootCarrierName">Phone:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="txtPhone" MaxLength="100" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label" for="Carrier_RootCarrierName">Bank Transit Number:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="txtBankTransitNumber" MaxLength="100" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="btnSubmit" class="btn btn-info" Text="Save" OnClientClick="javascript:return validateform();" runat="server" OnClick="btnSubmit_Click"></asp:Button>
                                        &nbsp; &nbsp; &nbsp;
												 <asp:Button ID="btnCancel" Text="Cancel" class="btn" runat="server" OnClick="btnCancel_Click"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:View>
    </asp:MultiView>
    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
</asp:Content>
