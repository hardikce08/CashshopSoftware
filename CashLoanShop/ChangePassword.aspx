<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="CashLoanShop.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function validateform() {

            if ($('#' + '<%= txtOldPassword.ClientID %>').val() == '') {
                alert('Please enter Old Password');
                $('#' + '<%= txtOldPassword.ClientID %>').focus();
                $('#' + '<%= txtOldPassword.ClientID %>').attr("style", "border-color:red;");
                return false;
            }

            else {
                $('#' + '<%= txtOldPassword.ClientID %>').removeAttr("style");
            }

            if ($('#' + '<%= txtNewPassword.ClientID %>').val() == '') {
                alert('Please enter New Password');
                $('#' + '<%= txtNewPassword.ClientID %>').focus();
                $('#' + '<%= txtNewPassword.ClientID %>').attr("style", "border-color:red;");
                return false;
            }

            else {
                $('#' + '<%= txtNewPassword.ClientID %>').removeAttr("style");
            }
            if ($('#' + '<%= txtConfirmPassword.ClientID %>').val() == '') {
                alert('Please enter Confirm Password');
                $('#' + '<%= txtConfirmPassword.ClientID %>').focus();
                $('#' + '<%= txtConfirmPassword.ClientID %>').attr("style", "border-color:red;");
                return false;
            }

            else {
                $('#' + '<%= txtConfirmPassword.ClientID %>').removeAttr("style");
            }
            if ($('#' + '<%= txtNewPassword.ClientID %>').val() != $('#' + '<%= txtConfirmPassword.ClientID %>').val()) {
                alert('New Password and Confirm Password must be same');

                $('#' + '<%= txtConfirmPassword.ClientID %>').focus();
                $('#' + '<%= txtConfirmPassword.ClientID %>').attr("style", "border-color:red;");
                return false;
            }

            else {
                $('#' + '<%= txtConfirmPassword.ClientID %>').removeAttr("style");
            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="page-content">
    <%--    <div class="page-header">
            <h1>Change Password
            </h1>
        </div>--%>

       
            <!-- /.page-header -->
            <div class="col-xs-12">
                <div class="row">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">Change Password</h3>
                        </div>
                        <div class="panel-body">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label class="col-md-5 control-label">Old Password :</label>
                                    <div class="controls col-md-7">
                                        <asp:TextBox ID="txtOldPassword" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-5 control-label">New Password :</label>
                                    <div class="controls col-md-7">
                                        <asp:TextBox ID="txtNewPassword" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-5 control-label">Confirm Password :</label>
                                    <div class="controls col-md-7">
                                        <asp:TextBox ID="txtConfirmPassword" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-5 control-label"></label>
                                    <div class="controls col-md-7">
                                        <asp:Button ID="btnSubmit" class="btn btn-primary" Text="Save" OnClientClick="javascript:return validateform();" runat="server" OnClick="btnSubmit_Click"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        
    </div>
</asp:Content>
