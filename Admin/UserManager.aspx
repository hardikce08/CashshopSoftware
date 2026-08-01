<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="UserManager.aspx.cs" Inherits="CashLoanShop.Admin.UserManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="page-content">
        <%-- <div id="divMsg" style="width: 100%; text-align: center; padding-bottom: 24px; font-size: 22px;" runat="server"></div>--%>
        <asp:MultiView ID="mvView" runat="server" ActiveViewIndex="0">
            <asp:View ID="vList" runat="server">
                <div class="col-xs-12">
                    <%-- <div class="row">
                        <div class="form-group">
                            <div class="controls col-md-3">
                              

                            </div>
                        </div>
                    </div>--%>
                    <div class="row">
                        <div class="form-group">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Search User</h3>
                                </div>
                                <div class="panel-body">
                                    <div class="col-md-8">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Username/Password :</label>
                                            <div class="controls col-md-5">
                                                <asp:TextBox ID="txtSearch" class="form-control" runat="server"></asp:TextBox>
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
                                            <div class="controls col-md-5">
                                                <asp:Button ID="btnSearch" class="btn btn-primary" Text="Search User" runat="server" OnClick="btnSearch_Click"></asp:Button>
                                                <asp:Button ID="btnAddNew" class="btn btn-pink" Text="Add New User +" runat="server" OnClick="btnAddNew_Click"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <table style="width: 75%;">
                            <tr>
                                <td>
                                    <asp:GridView Width="100%" ID="dgvCustomer" CssClass="EU_DataTable" AllowPaging="true" PageSize="20" class="table table-hover table-bordered" AutoGenerateColumns="false" runat="server" OnRowCommand="dgvCustomer_RowCommand" OnPageIndexChanging="dgvCustomer_PageIndexChanging" OnRowDataBound="dgvCustomer_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnSelect" runat="server" Text="Select" CommandName="Select" CommandArgument='<%# Eval("Id") %>'></asp:Button>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="UserName" HeaderStyle-HorizontalAlign="Left" HeaderText="UserName" />
                                            <asp:BoundField DataField="Password" HeaderStyle-HorizontalAlign="Left" HeaderText="Password" />
                                            <asp:BoundField DataField="StoreId" HeaderStyle-HorizontalAlign="Left" HeaderText="Store Id" />
                                            <asp:TemplateField HeaderText="StoreName" HeaderStyle-HorizontalAlign="Left">
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
            </asp:View>
            <asp:View ID="vAdd" runat="server">
                <div class="page-content">
                    <div class="row">
                        <!-- /.page-header -->
                        <div class="col-xs-12">
                            <div class="row">
                                <div class="panel panel-primary">
                                    <div class="panel-heading">
                                        <h3 class="panel-title">Add/Edit User Information</h3>
                                    </div>
                                    <div class="panel-body">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label" for="Carrier_RootCarrierName">User Name:</label>
                                                <div class="controls col-md-7">
                                                    <asp:TextBox CssClass="form-control" ID="txtUserName" MaxLength="100" runat="server" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label" for="Carrier_RootCarrierName">User Type:</label>
                                                <div class="controls col-md-7">
                                                    <asp:DropDownList ID="ddlUserType" CssClass="select" runat="server">
                                                        <asp:ListItem Text="Master" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Parent" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Reporting" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="Collection" Value="4"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-8">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label" for="Carrier_RootCarrierName">Password:</label>
                                                <div class="controls col-md-5">
                                                    <asp:TextBox CssClass="form-control" ID="txtPassword" MaxLength="100" runat="server" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-3 control-label" for="Carrier_RootCarrierName">Store:</label>
                                                <div class="controls col-md-7">
                                                    <asp:DropDownList ID="ddlShopStore" CssClass="select" runat="server">
                                                    </asp:DropDownList>
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
</asp:Content>
