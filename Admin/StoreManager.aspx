<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="StoreManager.aspx.cs" Inherits="CashLoanShop.Admin.StoreManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="page-content">
        <%-- <div id="divMsg" style="width: 100%; text-align: center; padding-bottom: 24px; font-size: 22px;" runat="server"></div>--%>
        <asp:MultiView ID="mvView" runat="server" ActiveViewIndex="0">
            <asp:View ID="vList" runat="server">
                <div class="col-xs-12">
                    <div class="row">
                        <div class="form-group">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Search Store</h3>
                                </div>
                                <div class="panel-body">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Store Id :</label>
                                            <div class="controls col-md-5">
                                                <asp:TextBox ID="txtSearch" class="form-control numerictext" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Store Name :</label>
                                            <div class="controls col-md-5">
                                                <asp:TextBox ID="txtSearchStoreName" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Store :</label>
                                            <div class="controls col-md-5">
                                                <asp:DropDownList ID="ddlSearchStore" CssClass="select" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-3 control-label"></label>
                                            <div class="controls col-md-8">
                                                <asp:Button ID="btnSearch" class="btn btn-primary" Text="Search Store" runat="server" OnClick="btnSearch_Click"></asp:Button>
                                                <asp:Button ID="btnAddNew" class="btn btn-pink" Text="Add New Store +" runat="server" OnClick="btnAddNew_Click"></asp:Button>
                                            </div>
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
                                    <asp:GridView Width="100%" ID="dgvCustomer" CssClass="EU_DataTable" AllowPaging="true" PageSize="20" class="table table-hover table-bordered" AutoGenerateColumns="false" runat="server" OnRowCommand="dgvCustomer_RowCommand" OnPageIndexChanging="dgvCustomer_PageIndexChanging" OnRowDataBound="dgvCustomer_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnSelect" runat="server" Text="Select" CommandName="Select" CommandArgument='<%# Eval("Id") %>'></asp:Button>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Id" HeaderStyle-HorizontalAlign="Left" HeaderText="Store Id" />
                                            <asp:BoundField DataField="Name" HeaderStyle-HorizontalAlign="Left" HeaderText="Store Name" />
                                            <asp:BoundField DataField="BusinessName" HeaderStyle-HorizontalAlign="Left" HeaderText="Business Name" />
                                            <asp:BoundField DataField="NewAddress" HeaderStyle-HorizontalAlign="Left" HeaderText="Address" />
                                            <asp:BoundField DataField="PhoneNo" HeaderStyle-HorizontalAlign="Left" HeaderText="Phone Number" />
                                            <asp:BoundField DataField="Fax" HeaderStyle-HorizontalAlign="Left" HeaderText="Alt. Phone Number" />
                                            <asp:BoundField DataField="Email" HeaderStyle-HorizontalAlign="Left" HeaderText="Email Address" />
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
            </asp:View>
            <asp:View ID="vAdd" runat="server">
                <div class="page-content">
                    <div class="row">
                        <!-- /.page-header -->
                        <div class="col-xs-8">
                            <div class="row">
                                <div class="panel panel-primary">
                                    <div class="panel-heading">
                                        <h3 class="panel-title">Add/Edit Store Information</h3>
                                    </div>
                                    <div class="panel-body">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label" for="Carrier_RootCarrierName">Store Name:</label>
                                                <div class="controls col-md-7">
                                                    <asp:TextBox CssClass="form-control" ID="txtStoreName" MaxLength="100" runat="server" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label" for="Carrier_RootCarrierName">Address:</label>
                                                <div class="controls col-md-7">
                                                    <asp:TextBox CssClass="form-control" ID="txtNewAddress" runat="server" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Province:</label>
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
                                                <label class="col-md-5 control-label" for="Carrier_RootCarrierName">PhoneNumber:</label>
                                                <div class="controls col-md-7">
                                                    <asp:TextBox CssClass="form-control" ID="txtPhoneno" runat="server" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label" for="Carrier_RootCarrierName">Email:</label>
                                                <div class="controls col-md-7">
                                                    <asp:TextBox CssClass="form-control" ID="txtEmail" runat="server" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label" for="Carrier_RootCarrierName">Interest Rate(%):</label>
                                                <div class="controls col-md-7">
                                                    <asp:TextBox CssClass="form-control" ID="txtInterestRate" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label" for="Carrier_RootCarrierName">Business Name:</label>
                                                <div class="controls col-md-7">
                                                    <asp:TextBox CssClass="form-control" ID="txtBusinessName" MaxLength="100" runat="server" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label" for="Carrier_RootCarrierName">City:</label>
                                                <div class="controls col-md-7">
                                                    <asp:TextBox CssClass="form-control" ID="txtCity" MaxLength="100" runat="server" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label" for="Carrier_RootCarrierName">Postal Code:</label>
                                                <div class="controls col-md-7">
                                                    <asp:TextBox CssClass="form-control" ID="txtPostCode" MaxLength="50" runat="server" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label" for="Carrier_RootCarrierName">Fax:</label>
                                                <div class="controls col-md-7">
                                                    <asp:TextBox CssClass="form-control" ID="txtFax" runat="server" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label" for="Carrier_RootCarrierName">NSF Charge:</label>
                                                <div class="controls col-md-7">
                                                    <asp:TextBox CssClass="form-control" ID="txtNSFCharge" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                      
                                    </div>
                                    <div class="panel-heading">
                                        <h3 class="panel-title">Term Loan Settings</h3>
                                    </div>
                                    <div class="panel-body">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label" for="Carrier_RootCarrierName">Admin Fee(%):</label>
                                                <div class="controls col-md-7">
                                                    <asp:TextBox CssClass="form-control numerictext" ID="txtAdminFeePercentage" MaxLength="100" runat="server" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label" for="Carrier_RootCarrierName">Max. Loan Amount:</label>
                                                <div class="controls col-md-7">
                                                    <asp:TextBox CssClass="form-control numerictext" ID="txtMaximumLoanAmount" MaxLength="100" runat="server" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label" for="Carrier_RootCarrierName">NSF Charge:</label>
                                                <div class="controls col-md-7">
                                                    <asp:TextBox CssClass="form-control numerictext" ID="txtTermNSFCharge" MaxLength="100" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label" for="Carrier_RootCarrierName">Interest Rate(%):</label>
                                                <div class="controls col-md-7">
                                                    <asp:TextBox CssClass="form-control numerictext" ID="txtTermInterestRate" MaxLength="100" runat="server" />
                                                </div>
                                            </div>
                                              <div class="form-group">
                                                <label class="col-md-5 control-label" for="Carrier_RootCarrierName">Late Interest Rate(%):</label>
                                                <div class="controls col-md-7">
                                                    <asp:TextBox CssClass="form-control numerictext" ID="txtTermLateInterestRate" MaxLength="100" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                          <div class="col-md-offset-3 col-md-9">
                                            <asp:Button ID="btnSubmit" class="btn btn-primary" Text="Save" OnClientClick="javascript:return validateform();" runat="server" OnClick="btnSubmit_Click"></asp:Button>
                                            <asp:Button ID="btnCancel" class="btn btn-pink" Text="Cancel" runat="server" OnClick="btnCancel_Click"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:View>
        </asp:MultiView>
    </div>
    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
    <script type="text/javascript">


        $(document).ready(function () {

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
        });
    </script>
</asp:Content>
