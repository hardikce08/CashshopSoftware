<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" EnableEventValidation="false" EnableViewStateMac="false" Inherits="CashLoanShop.Admin.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payday Loan Mart - Login</title>
  <link rel="stylesheet" href="http://fonts.googleapis.com/css?family=Open+Sans:400,600,800" />
    <link href="~/css/font-awesome.css" rel="stylesheet" />
    <link href="~/css/bootstrap.css" rel="stylesheet" />
    <link href="~/css/bootstrap-responsive.css" rel="stylesheet" />
    <link href="~/css/jquery-ui-1.8.21.custom.css" rel="stylesheet" />
    <link href="~/css/application-ocean-breeze.css" rel="stylesheet" />
    <link href="~/css/dashboard.css" rel="stylesheet" />
    <link href="~/css/theme.css" rel="stylesheet" />


</head>
<body class="login"> 
       <div class="account-container login stacked">

        <div class="content clearfix">


            <form id="form1" runat="server">
                <h1>Super Admin</h1>
                <div class="login-fields">
                    <div class="field">
                        <label for="UserName">Username</label>
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="login username-field" data-val="true" data-val-required="The Username field is required." placeholder="Username"></asp:TextBox>
                        <span class="field-validation-valid" data-valmsg-for="UserName" data-valmsg-replace="true"></span>
                    </div>
                    <div class="field">
                        <label for="Password">Password</label>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="login password-field" data-val="true" data-val-required="The Password field is required." placeholder="Password"></asp:TextBox>
                        <span class="field-validation-valid" data-valmsg-for="Password" data-valmsg-replace="true"></span>
                    </div>
                </div>
                <!-- /login-fields -->
                <div class="login-actions">
                     <asp:Button ID="btnLogin" runat="server" Text="Sign In" class="button btn btn-primary btn-large"   OnClick="btnLogin_Click" />

                </div>
        </form>
    </div>
    </div>
     <!-- jQuery -->
    <script src="~/Js/jquery-2.0.3.min.js"></script>
    <!-- Bootstrap 4 -->
    <script src="~/Js/signin.js"></script>
</body>
</html>
