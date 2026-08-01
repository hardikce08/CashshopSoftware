<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="CustomMessage.aspx.cs" Inherits="CashLoanShop.Admin.CustomMessage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">

    <div class="page-content">
        <div class="row">
            <!-- /.page-header -->
            <div class="col-xs-8">
                <div class="row">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">Custom Message</h3>
                        </div>
                        <div class="panel-body">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-5 control-label" for="Carrier_RootCarrierName">Message Content:</label>
                                    <div class="controls col-md-7">
                                        <asp:TextBox CssClass="form-control" ID="txtMessage" TextMode="MultiLine" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-offset-3 col-md-9">
                                <asp:Button ID="btnSubmit" class="btn btn-primary" Text="Save" OnClientClick="javascript:return validateform();" runat="server" OnClick="btnSubmit_Click"></asp:Button>
                                <asp:Button ID="btnCancel" class="button2" Text="Cancel" runat="server" Visible="false" OnClick="btnCancel_Click"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

   
</asp:Content>
