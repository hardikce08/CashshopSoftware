<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CustomerLoan.aspx.cs" EnableViewStateMac="false" Inherits="CashLoanShop.CustomerLoan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #cover-spin {
            position: fixed;
            width: 100%;
            left: 0;
            right: 0;
            top: 0;
            bottom: 0;
            background-color: rgba(255,255,255,0.7);
            z-index: 9999;
            display: none;
        }

        @-webkit-keyframes spin {
            from {
                -webkit-transform: rotate(0deg);
            }

            to {
                -webkit-transform: rotate(360deg);
            }
        }

        @keyframes spin {
            from {
                transform: rotate(0deg);
            }

            to {
                transform: rotate(360deg);
            }
        }

        #cover-spin::after {
            content: '';
            display: block;
            position: absolute;
            left: 48%;
            top: 40%;
            width: 40px;
            height: 40px;
            border-style: solid;
            border-color: black;
            border-top-color: transparent;
            border-width: 4px;
            border-radius: 50%;
            -webkit-animation: spin .8s linear infinite;
            animation: spin .8s linear infinite;
        }
         #messageListContainer table {
    width: 100%;
    table-layout: fixed;
}

#messageListContainer td {
    white-space: normal;
    word-break: break-all;
    overflow-wrap: anywhere;
}

#messageListArea {
    width: 100%;
    overflow-x: hidden;
}

#messageListArea .list-group {
    width: 100%;
    margin-left: 0px;
}

#messageListArea .list-group-item {
    white-space: normal !important;
    word-break: break-all !important;
    overflow-wrap: anywhere !important;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:MultiView ID="mvView" runat="server" ActiveViewIndex="0">
        <asp:View ID="vList" runat="server">
            <div class="page-content">
                <div class="col-xs-12">
                    <div class="row">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Search Customer</h3>
                            </div>
                            <div class="panel-body">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-3 control-label">Last Name :</label>
                                        <div class="controls col-md-5">
                                            <asp:TextBox ID="txtSearchLastName" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-3 control-label">First Name :</label>
                                        <div class="controls col-md-5">
                                            <asp:TextBox ID="txtSearchName" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-3 control-label">CustomerId :</label>
                                        <div class="controls col-md-5">
                                            <asp:TextBox ID="txtSearchId" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-3 control-label">SIN Number :</label>
                                        <div class="controls col-md-5">
                                            <asp:TextBox ID="txtSearchSINNumber" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-3 control-label">Phone Number :</label>
                                        <div class="controls col-md-5">
                                            <asp:TextBox ID="txtSearchPhoneNumber" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-3 control-label"></label>
                                        <div class="controls col-md-9">
                                            <asp:Button ID="btnSearch" class="btn btn-primary" Text="Search" runat="server" OnClick="btnSearch_Click"></asp:Button>
                                            <asp:Button ID="btnAddNew" class="btn btn-pink" Text="Add New Customer" runat="server" OnClick="btnAddNew_Click"></asp:Button>
                                            <asp:Button ID="btnViewTransaction" class="btn btn-purple" Visible="false" Text="View Loan Entry" runat="server" OnClick="btnViewTransaction_Click"></asp:Button>
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
                                    <asp:GridView ID="dgvCustomer" CssClass="EU_DataTable" AllowPaging="true" PageSize="15" class="table table-hover table-bordered" AutoGenerateColumns="false" runat="server" OnRowCommand="dgvCustomer_RowCommand" OnPageIndexChanging="dgvCustomer_PageIndexChanging" OnRowDataBound="dgvCustomer_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnSelect" runat="server" Text="Select" CommandName="Select" CommandArgument='<%# Eval("Id") %>'></asp:Button>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Id" HeaderStyle-HorizontalAlign="Left" HeaderText="Customer ID" />
                                            <asp:BoundField DataField="SocialSecurityNumber" HeaderStyle-HorizontalAlign="Left" HeaderText="SIN Number" />
                                            <asp:BoundField DataField="LastName" HeaderStyle-HorizontalAlign="Left" HeaderText="Last Name" />
                                            <asp:BoundField DataField="FirstName" HeaderStyle-HorizontalAlign="Left" HeaderText="First Name" />
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Birth Date">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "Dateofbirth", "{0:MM/dd/yyyy}").ToString().Replace("-","/") %>
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
        <asp:View ID="vAdd" runat="server">

            <div class="page-content">
                <div id="datanew" runat="server">
                    <div>
                        <div class="col-md-10">
                            <div class="row">
                                 <div class="panel panel-info">
                                        <div class="panel-heading" role="tab" id="headingMessages">
                                            <h3 class="panel-title">
                                                <a role="button" data-toggle="collapse" href="#collapseMessages" aria-expanded="true" aria-controls="collapseMessages">
                                                    Customer Messages (Click to Expand/Collapse)
                                                </a>
                                            </h3>
                                        </div>
                                        <div id="collapseMessages" class="panel-collapse collapse in" role="tabpanel">
                                            <div class="panel-body">
                                                <div id="messageListArea">
                                                    <div class="text-center">Loading messages...</div>
                                                </div>
                                                <hr />
                                                <div class="form-inline text-right">
                                                    <button type="button" class="btn btn-success btn-sm" onclick="openMessageModal($('#content_hdnCustomerId').val())">
                                                        <i class="fa fa-plus"></i> Manage Messages
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                   </div>
                                <asp:Repeater ID="rptCustomer" runat="server" OnItemDataBound="rptCustomer_ItemDataBound">
                                    <ItemTemplate>
                                        <div class="panel panel-primary">
                                            <div class="panel-heading">
                                                <h3 class="panel-title">Customer Information&nbsp;
                            <a href="javascript:void(0);" style="margin-left: 200px; color: white; background-color: black; border: 1px solid; padding-top: 6px; padding-bottom: 6px; padding-left: 5px; padding-right: 5px;"
                                onclick="javascript:return openhistory('<%# Eval("Id") %>');">View Customer History</a></h3>
                                            </div>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-md-5">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Name:</label>
                                                            <label class="col-md-7">
                                                                <%# Eval("FirstName") %> <%# Eval("LastName") %>
                                                            </label>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Date of Birth:</label>
                                                            <label class="col-md-7">
                                                                <%# Eval("DateofBirth") %>
                                                            </label>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">City:</label>
                                                            <label class="col-md-7">
                                                                <%# Eval("City") %>
                                                            </label>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">PostCode:</label>
                                                            <label class="col-md-7">
                                                                <%# Eval("PostCode") %>
                                                            </label>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Own a Home Or Rent:</label>
                                                            <label class="col-md-7">
                                                                <%#Eval("HomeType").ToString() =="Select Home Type" ? "Not Selected": Eval("HomeType") %>
                                                            </label>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Pay Cycle Comment:</label>
                                                            <label class="col-md-7">
                                                                <%# Eval("PhoneListedunder") %>
                                                            </label>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Cell Phone:</label>
                                                            <label class="col-md-7">
                                                                <%# Eval("CellPhone") %>
                                                            </label>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Driving License Number:</label>
                                                            <label class="col-md-7">
                                                                <%# Eval("DrivingLicenseNumber") %>
                                                            </label>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Take Home Pay:</label>
                                                            <label class="col-md-7">
                                                                <asp:Label ID="lblTakeHomePay" runat="server"></asp:Label>
                                                            </label>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Frequency of  Pay:</label>
                                                            <label class="col-md-7">
                                                                <asp:Label ID="lblFrequencyofPay" runat="server"></asp:Label>
                                                            </label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-5">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Gender:</label>
                                                            <label class="col-md-7">
                                                                <%# Eval("Gender") %>
                                                            </label>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Address:</label>
                                                            <label class="col-md-7">
                                                                <%# Eval("Address") %>
                                                            </label>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Province:</label>
                                                            <label class="col-md-7">
                                                                <%# GetProvince(Convert.ToInt32(  Eval("Province")  )) %>
                                                            </label>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Duration at this address:</label>
                                                            <label class="col-md-7">
                                                                <%# Eval("DurationYears") %> Years <%# Eval("DurationMonth") %> Months
                                                            </label>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Home Phone:</label>
                                                            <label class="col-md-7">
                                                                <%# Eval("HomePhone") %>
                                                            </label>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Work Phone:</label>
                                                            <label class="col-md-7">
                                                                <%# Eval("WorkPhone") %>
                                                            </label>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Social Security Number:</label>
                                                            <label class="col-md-7">
                                                                <%# Eval("SocialSecurityNumber") %>
                                                            </label>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Comments:</label>
                                                            <label class="col-md-7">
                                                                <%# Eval("Comments") %>
                                                            </label>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Payment Type:</label>
                                                            <label class="col-md-7">
                                                                <asp:Label ID="lblPaymentType" runat="server"></asp:Label>
                                                            </label>
                                                        </div>
                                                          <div class="form-group">
                                                            <label class="col-md-5 control-label">Issued At:</label>
                                                            <label class="col-md-7">
                                                                <%# Eval("IssuedAt") %>
                                                            </label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>

                                <asp:Repeater ID="rptBakInformation" runat="server">
                                    <ItemTemplate>
                                        <div class="panel panel-primary">
                                            <div class="panel-heading">
                                                <h3 class="panel-title">Customer Bank Information</h3>
                                            </div>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-md-5">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Bank Name:</label>
                                                            <label class="col-md-7">
                                                                <%# Eval("BankName") %>
                                                            </label>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Transit Number:</label>
                                                            <label class="col-md-7">
                                                                <%# Eval("TransitNo") %>
                                                            </label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-5">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Account Number:</label>
                                                            <label class="col-md-7">
                                                                <%# Eval("AccountNumber") %>
                                                            </label>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Institution Number:</label>
                                                            <label class="col-md-7">
                                                                <%# Eval("InstitutionNo") %>
                                                            </label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="row">
                                <table>
                                    <tr>
                                        <td style="padding-bottom: 200px !important; margin-bottom: 22px !important; vertical-align: bottom;">&nbsp;
                                                 <a href="#" class="pop" id="aimage" runat="server">
                                                     <asp:Image ID="imgProof" AlternateText="" ImageUrl="/ProofOfIdentity/NoImage.png" runat="server" Height="150" Width="150" />
                                                 </a>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div>
                        <div class="col-md-10">
                            <div class="row">
                                <div class="panel panel-primary">
                                    <div class="panel-heading" runat="server" id="dvIsMoreloan">
                                        <h3 class="panel-title">** This Customer has taken more than 2 loans in last 63 Days and qualifies for an extended payment plan</h3>
                                    </div>
                                    <div class="panel-body">
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Loan Amount Applied:</label>
                                                <div class="controls col-md-7">
                                                    <asp:TextBox CssClass="form-control numerictext" ID="txtAppliedAmount" MaxLength="100" runat="server" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Next Pay Date:</label>
                                                <div class="controls col-md-7">
                                                    <asp:TextBox CssClass="form-control" ID="txtNextPayDate" runat="server" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Loan Amount Approved:</label>
                                                <div class="controls col-md-7">
                                                    <asp:TextBox CssClass="form-control numerictext" ID="txtLoanAmount" runat="server" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Net Income:</label>
                                                <div class="controls col-md-7">
                                                    <asp:TextBox CssClass="form-control numerictext" ID="txtMonthlyIncome" runat="server" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Installment2 Date:</label>
                                                <div class="controls col-md-7">
                                                    <asp:TextBox CssClass="form-control" ID="txtInstallment1Date" runat="server" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Maximum Applicable Loan:</label>
                                                <div class="controls col-md-7">
                                                    <asp:TextBox CssClass="form-control numerictext" Enabled="false" ID="txtMaxLoan" runat="server" />
                                                </div>
                                            </div>
                                            <div class="form-group">

                                                <div class="controls col-md-10">
                                                    <asp:RadioButtonList ID="rbtnlstLoanType" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Text="Approve Loan" Value="1" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Deny Loan" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Store:</label>
                                                <div class="controls col-md-7">
                                                    <asp:DropDownList ID="ddlShopStore" CssClass="form-control" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Payment Option:</label>
                                                <div class="controls col-md-7">
                                                    <asp:DropDownList ID="ddlPaymentOption" runat="server" CssClass="form-control" RepeatDirection="Horizontal">
                                                        <asp:ListItem Text="--Select Option--" Value="0" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="EFT" Value="EFT" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="CP" Value="CP"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Comments:</label>
                                                <div class="controls col-md-7">
                                                    <asp:TextBox CssClass="form-control" ID="txtComments" TextMode="MultiLine" runat="server" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Loan Pay Type:</label>
                                                <div class="controls col-md-7">
                                                    <asp:DropDownList ID="ddlLoanType" onchange="javascript:return validatetype();" runat="server" CssClass="form-control" RepeatDirection="Horizontal">
                                                        <asp:ListItem Text="--Select Option--" Value="0" Selected="True"></asp:ListItem>
                                                         
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Installment3 Date:</label>
                                                <div class="controls col-md-7">
                                                    <asp:TextBox CssClass="form-control" ID="txtInstallment2Date" runat="server" />
                                                </div>
                                            </div>
                                            <div class="form-group">

                                                <div class="controls col-md-10">
                                                    <asp:RadioButtonList ID="rbtnInterestRate" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Text="32% Late Interest Rate" Selected="True" Value="32"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <div class="col-md-2">
                                                    <input type="button" value="Calculate Due Amount" id="btnCalculate" name="btnCalculate" class="btn btn-primary" />
                                                </div>
                                                <label class=" col-md-1 control-label">
                                                    <asp:Label ID="lblTotalAmountDue" Style="font-weight: bold; font-size: 25px;" runat="server"></asp:Label>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">

                                                <div class="col-md-offset-3 col-md-9">
                                                    <asp:Button ID="btnSubmit" class="btn btn-primary" Text="Save" OnClientClick="javascript:return validateform('submit');" runat="server" OnClick="btnSubmit_Click"></asp:Button>
                                                    <asp:Button ID="btnCancel" class="btn btn-pink" Text="Cancel" runat="server" OnClick="btnCancel_Click"></asp:Button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="exist" runat="server" style="text-align: left !important;">
                    <div class="col-md-7">
                        <div class="row">
                            <div class="panel panel-primary">
                                <div class="panel-body">
                                    <div class="form-group">
                                        <label class="col-md-12" style="font-size: 20px;">
                                            <asp:Label ID="lblcustomername" runat="server"></asp:Label>
                                            has a Loan with status '<asp:Label ID="lblLoanStatus" runat="server"></asp:Label>' at:<br />
                                        </label>
                                        <label class="col-md-12" style="font-size: 16px;">
                                            <asp:Label ID="lblStoreInfo" runat="server"></asp:Label><br />
                                        </label>
                                        <label class="col-md-12" style="font-size: 20px;">
                                            Customer can not apply for new loan at this store.
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>





        </asp:View>
        <asp:View ID="vCashCheque" runat="server">
            <table style="width: 50%; border: 1px solid black;" id="searchtbl">

                <tr>
                    <td style="width: 20%;">Customer's FirstName / LastName :
                    </td>
                    <td style="width: 80%;">
                        <asp:TextBox ID="txtSearchCustomerName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%;">Payment Option :
                    </td>
                    <td style="width: 80%;">
                        <asp:TextBox ID="txtSearchPaymentoption" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnSearchCashEntryData" class="button2" Text="Search" runat="server" OnClick="btnSearchCashEntryData_Click"></asp:Button>
                        <asp:Button ID="btnCustomerSearch" class="button2" Text="Back to Customer" runat="server" OnClick="btnCustomerSearch_Click"></asp:Button>
                    </td>

                </tr>
            </table>
            <table style="width: 100%;">
                <tr>
                    <td>
                        <asp:GridView Width="90%" ID="dgvCashEntry" CssClass="EU_DataTable" AllowPaging="true" PageSize="15" class="table table-hover table-bordered" AutoGenerateColumns="false" runat="server" OnRowCommand="dgvCashEntry_RowCommand" OnPageIndexChanging="dgvCashEntry_PageIndexChanging" OnRowDataBound="dgvCashEntry_RowDataBound">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnSelect" runat="server" Text="Select" CommandName="Select" CommandArgument='<%# Eval("Id") %>'></asp:Button>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <a href="javascript:void(0);" onclick="javascript:return call('<%# Eval("Id") %>');">View / Print Receipt</a>
                                        <%-- <asp:HyperLink ID="lnkReceipt" runat="server" Text="View/Print Receipt" NavigateUrl='<%# Eval("Id","~/ChequeCashReceipt.aspx?Id={0}") %>'  Target="_blank"></asp:HyperLink>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Id" HeaderStyle-HorizontalAlign="Left" HeaderText="Loan ID" />
                                <asp:BoundField DataField="CustomerName" HeaderStyle-HorizontalAlign="Left" HeaderText="Customer Name" />
                                <asp:BoundField DataField="LoanAmountApplied" HeaderStyle-HorizontalAlign="Left" HeaderText="Applied Amount" />
                                <asp:BoundField DataField="LoanAmountApproved" HeaderStyle-HorizontalAlign="Left" HeaderText="Approved Amount" />
                                <asp:BoundField DataField="DueAmount" HeaderStyle-HorizontalAlign="Left" HeaderText="Due Amount" />
                                <asp:BoundField DataField="NextPayDate" HeaderStyle-HorizontalAlign="Left" HeaderText="Next PayDate" DataFormatString="{0:MM/dd/yyyy}" />
                                <asp:BoundField DataField="PaymentOption" HeaderStyle-HorizontalAlign="Left" HeaderText="Payment Option" />

                            </Columns>
                            <EmptyDataTemplate>
                                <div style="color: red; text-align: center;">No records found</div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
    <asp:HiddenField ID="hdnTotalValue" runat="server" />
    <asp:HiddenField ID="hdnDueAmount" runat="server" />
    <asp:HiddenField ID="hdnCharge" runat="server" />
    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
    <asp:HiddenField ID="hdnCustomerId" runat="server" Value="0" />
    <asp:HiddenField ID="hdnLoanCount" runat="server" Value="0" />
    <div class="modal fade" id="imagemodal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-body">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <img src="" class="imagepreview" style="width: 100%;" />
                </div>
            </div>
        </div>
    </div>
    <div id="cover-spin"></div>
      <div class="modal fade" id="messageModal" tabindex="-1" role="dialog">
    <div class="modal-dialog modal-lg" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title">Manage Messages</h5>
                <button type="button" class="close" data-dismiss="modal">&times;</button>
            </div>
            <div class="modal-body">
                <div class="well">
                    <input type="hidden" id="hdnMsgId" value="0" />
                    <input type="hidden" id="hdnCustId" />
                    <div class="form-group">
                        <label>Message:</label>
                        <textarea id="txtMessage" class="form-control"></textarea>
                    </div>
                    <div class="checkbox">
                        <label><input type="checkbox" id="chkIsPinned" /> Is Pinned</label>
                    </div>
                    <button type="button" onclick="saveMessage()" class="btn btn-success">Save Message</button>
                    <button type="button" onclick="clearForm()" class="btn btn-default">Clear</button>
                </div>
                <hr />
                <div id="messageListContainer">
                    </div>
            </div>
        </div>
    </div>
</div>
    <script>

        $(document).ready(function () {
            $('.pop').click(function () {
                $('.imagepreview').attr('src', $(this).find('img').attr('src'));
                $('#imagemodal').modal('show');
            });

            // $(".fancybox").fancybox();
            //var date = new Date();
            //date.setDate(date.getDate() + 1);
            //,
            //    defaultDate: date
            $('#' + '<%= txtNextPayDate.ClientID %>').datetimepicker({
                format: 'L'
            });
            $('#' + '<%= txtInstallment1Date.ClientID %>').datetimepicker({
                format: 'L'
            });
            $('#' + '<%= txtInstallment2Date.ClientID %>').datetimepicker({
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


            $('#btnCalculate').click(function () {
                validateform('calculate');
            });
        });
        function validatetype() {
            var loantype = $('#' + '<%= ddlLoanType.ClientID %>').val();
            var totalpastloancount = $('#' + '<%= hdnLoanCount.ClientID %>').val();
            $('#' + '<%= txtInstallment1Date.ClientID %>').prop("disabled", true);
            $('#' + '<%= txtInstallment2Date.ClientID %>').prop("disabled", true);
            if (loantype == '2 Installment') {
                if (totalpastloancount >= 2) {
                    $('#' + '<%= txtInstallment1Date.ClientID %>').prop("disabled", false);
                    $('#' + '<%= txtInstallment2Date.ClientID %>').prop("disabled", true);
                }
            }
            else if (loantype == '3 Installment') {
                if (totalpastloancount >= 2) {
                    $('#' + '<%= txtInstallment1Date.ClientID %>').prop("disabled", false);
                    $('#' + '<%= txtInstallment2Date.ClientID %>').prop("disabled", false);
                }
            }
        }
        function validateform(fromname) {

            //var checked_radio = $("[id*=rbtnlstLoanType] input:checked");
            //document.getElementById("myBtn").disabled = true;
            $("#content_btnSubmit").prop("disabled", true);
            //var value = checked_radio.closest("td").find("label").html();
            //if (value == "Approve Loan") {
            if ($('#' + '<%= txtAppliedAmount.ClientID %>').val() == '' || $('#' + '<%= txtAppliedAmount.ClientID %>').val() == undefined) {
                alert('Please enter Applied Loan Amount');

                $('#' + '<%= txtAppliedAmount.ClientID %>').focus();
                $('#' + '<%= txtAppliedAmount.ClientID %>').attr("style", "border-color:red;");
                $("#content_btnSubmit").prop("disabled", false);
                return false;
            }

            else {
                $('#' + '<%= txtAppliedAmount.ClientID %>').removeAttr("style");
            }


            if ($('#' + '<%= txtNextPayDate.ClientID %>').val() == '' || $('#' + '<%= txtNextPayDate.ClientID %>').val() == undefined) {
                alert('Please select Next Pay Date');

                $('#' + '<%= txtNextPayDate.ClientID %>').focus();
                $('#' + '<%= txtNextPayDate.ClientID %>').attr("style", "border-color:red;");
                $("#content_btnSubmit").prop("disabled", false);
                return false;
            }

            else {
                $('#' + '<%= txtNextPayDate.ClientID %>').removeAttr("style");
            }

            if ($('#' + '<%= txtNextPayDate.ClientID %>').val() != '' || $('#' + '<%= txtNextPayDate.ClientID %>').val() != undefined) {
                //alert(moment().format('MM/DD/YYYY'));
                var c = new Date(moment().format('MM/DD/YYYY'));
                var d = new Date($('#' + '<%= txtNextPayDate.ClientID %>').val());
               // alert(c);
               // alert(d);
                if (d <= c) {
                     alert('Next Pay date should be greater than Current Date');
                    $('#' + '<%= txtNextPayDate.ClientID %>').focus();
                    $('#' + '<%= txtNextPayDate.ClientID %>').attr("style", "border-color:red;");
                    $("#content_btnSubmit").prop("disabled", false);
                    return false;
                }
            }

            else {
                $('#' + '<%= txtNextPayDate.ClientID %>').removeAttr("style");
            }

            if ($('#' + '<%= ddlShopStore.ClientID %>').val() == '0' || $('#' + '<%= ddlShopStore.ClientID %>').val() == undefined) {
                alert('Please select Shop Store');

                $('#' + '<%= ddlShopStore.ClientID %>').focus();
                $('#' + '<%= ddlShopStore.ClientID %>').attr("style", "border-color:red;");
                $("#content_btnSubmit").prop("disabled", false);
                return false;
            }

            else {
                $('#' + '<%= ddlShopStore.ClientID %>').removeAttr("style");
            }

            if ($('#' + '<%= txtLoanAmount.ClientID %>').val() == '' || $('#' + '<%= txtLoanAmount.ClientID %>').val() == undefined) {
                alert('Please enter Loan Amount');

                $('#' + '<%= txtLoanAmount.ClientID %>').focus();
                $('#' + '<%= txtLoanAmount.ClientID %>').attr("style", "border-color:red;");
                $("#content_btnSubmit").prop("disabled", false);
                return false;
            }

            else {
                $('#' + '<%= txtLoanAmount.ClientID %>').removeAttr("style");
            }


            if ($('#' + '<%= ddlPaymentOption.ClientID %>').val() == '0' || $('#' + '<%= ddlPaymentOption.ClientID %>').val() == undefined) {
                alert('Please select Payment Option');
                $('#' + '<%= ddlPaymentOption.ClientID %>').focus();
                $('#' + '<%= ddlPaymentOption.ClientID %>').attr("style", "border-color:red;");
                $("#content_btnSubmit").prop("disabled", false);
                return false;
            }

            else {
                $('#' + '<%= ddlPaymentOption.ClientID %>').removeAttr("style");
            }

            if ($('#' + '<%= ddlLoanType.ClientID %>').val() == '0' || $('#' + '<%= ddlLoanType.ClientID %>').val() == undefined) {
                alert('Please select Loan Payment Type');

                $('#' + '<%= ddlLoanType.ClientID %>').focus();
                $('#' + '<%= ddlLoanType.ClientID %>').attr("style", "border-color:red;");
                $("#content_btnSubmit").prop("disabled", false);
                return false;
            }

            else {
                $('#' + '<%= ddlLoanType.ClientID %>').removeAttr("style");
            }

            if ($('#' + '<%= txtAppliedAmount.ClientID %>').val() != '' && $('#' + '<%= txtLoanAmount.ClientID %>').val()) {
                if (parseFloat($('#' + '<%= txtAppliedAmount.ClientID %>').val()) < parseFloat($('#' + '<%= txtLoanAmount.ClientID %>').val())) {
                    alert('Loan Amount Approved can not be greater than Loan Amount Applied');
                    $('#' + '<%= txtLoanAmount.ClientID %>').focus();
                    $('#' + '<%= txtLoanAmount.ClientID %>').attr("style", "border-color:red;");
                    $("#content_btnSubmit").prop("disabled", false);
                    return false;
                }
            }
            if ($('#' + '<%= txtMonthlyIncome.ClientID %>').val() == '' || $('#' + '<%= txtMonthlyIncome.ClientID %>').val() == undefined) {
                alert('Please enter Net Income of the user');
                $('#' + '<%= txtMonthlyIncome.ClientID %>').focus();
                $('#' + '<%= txtMonthlyIncome.ClientID %>').attr("style", "border-color:red;");
                $("#content_btnSubmit").prop("disabled", false);
                return false;
            }
            else {
                $('#' + '<%= txtMonthlyIncome.ClientID %>').removeAttr("style");
            }
            if ($('#' + '<%= txtMonthlyIncome.ClientID %>').val() != '' || $('#' + '<%= txtMonthlyIncome.ClientID %>').val() != undefined) {

                var a = $('#' + '<%= txtAppliedAmount.ClientID %>').val();
                var b = $('#' + '<%= txtMonthlyIncome.ClientID %>').val();
                var Maxloanamt = (b / 2);
                if (Maxloanamt > 1500)
                {
                    Maxloanamt = 1500;
                }
                $('#' + '<%= txtMaxLoan.ClientID %>').val(Maxloanamt.toString());
                if (a > Maxloanamt) {
                    alert('User Can not Applied More than' + Maxloanamt.toString());

                    $('#' + '<%= txtAppliedAmount.ClientID %>').focus();
                $('#' + '<%= txtAppliedAmount.ClientID %>').attr("style", "border-color:red;");
                    $("#content_btnSubmit").prop("disabled", false);
                    return false;
                }
            }

            else {
                $('#' + '<%= txtAppliedAmount.ClientID %>').removeAttr("style");
            }

            if ($('#' + '<%= hdnLoanCount.ClientID %>').val() != '0' || $('#' + '<%= hdnLoanCount.ClientID %>').val() != undefined) {

                var a = $('#' + '<%= hdnLoanCount.ClientID %>').val();

                if (a >= 2) {
                    var loantype = $('#' + '<%= ddlLoanType.ClientID %>').val();
                    if (loantype == '2 Installment'){
                        if ($('#' + '<%= txtInstallment1Date.ClientID %>').val() == '' || $('#' + '<%= txtInstallment1Date.ClientID %>').val() == undefined) {
                            alert('Please select Second Installment Date');
                            $('#' + '<%= txtInstallment1Date.ClientID %>').focus();
                            $('#' + '<%= txtInstallment1Date.ClientID %>').attr("style", "border-color:red;");
                            $("#content_btnSubmit").prop("disabled", false);
                            return false;
                        }
                }
                else {
                    $('#' + '<%= txtInstallment1Date.ClientID %>').removeAttr("style");
                }
                if ($('#' + '<%= txtInstallment2Date.ClientID %>').val() == '' || $('#' + '<%= txtInstallment2Date.ClientID %>').val() == undefined) {
                    var loantype = $('#' + '<%= ddlLoanType.ClientID %>').val();
                    if (loantype == '3 Installment') {
                            alert('Please select Third Installment Date');
                            $('#' + '<%= txtInstallment2Date.ClientID %>').focus();
                            $('#' + '<%= txtInstallment2Date.ClientID %>').attr("style", "border-color:red;");
                            $("#content_btnSubmit").prop("disabled", false);
                            return false;
                        }
                    }
                    else {
                        $('#' + '<%= txtInstallment2Date.ClientID %>').removeAttr("style");
                    }

                }
            }



            //}
            // $('#btnCalculate').click();
            var approvedamount = parseFloat($('#' + '<%= txtLoanAmount.ClientID %>').val());
            var charge = (approvedamount * 0.15);
            var Totaldueamount = parseFloat(approvedamount + charge);
            //alert(Totalcharge);
            $('#' + '<%= hdnCharge.ClientID %>').val(charge);
            $('#' + '<%= hdnDueAmount.ClientID %>').val(parseFloat(Totaldueamount));
            //alert(parseFloat(Totaldueamount));
            $('#' + '<%= lblTotalAmountDue.ClientID %>').text(parseFloat(Totaldueamount));
            $("#content_btnSubmit").prop("disabled", false);
            if (fromname == 'submit') {
                $('#cover-spin').show(0);
            }
        }

        function call(obj) {
            window.open('/CustomerLoanReceipt.aspx?Id=' + obj + '', '_blank', 'width=800,height=550,location=no,left=200px');
        }
        function openhistory(obj) {
            window.open('/CustomerCashHistory.aspx?Id=' + obj + '&IsCurrencyExchange=true&IsLoan=true', '_blank', 'width=1200,height=750,location=no,left=200px');
        }
    </script>
      <script>
          $(document).ready(function () {
              var custId = $('#content_hdnCustomerId').val(); // Get current Customer ID from your hidden field

              if (custId && custId !== "0") {
                  // Refresh the background list in the Edit Form
                  loadCustomerProfileMessages(custId);
              }

          });
          function openMessageModal(customerId) {
              // alert(customerId);
              $('#hdnCustId').val(customerId);
              clearForm();
              loadMessages(customerId);
              $('#messageModal').modal('show');
          }
          function loadMessages(customerId) {
              $.ajax({
                  type: "POST",
                  url: "Customer.aspx/GetMessages",
                  data: JSON.stringify({ customerId: customerId }),
                  contentType: "application/json; charset=utf-8",
                  success: function (response) {
                      // Build a dynamic table inside #messageListContainer
                      // including 'Edit' and 'Delete' buttons that call JS functions
                      $('#messageListContainer').html(response.d);
                  }
              });
          }
          function clearForm() {
              $('#hdnMsgId').val("0");
              $('#txtMessage').val("");
              $('#chkIsPinned').prop('checked', false);
          }
          function saveMessage() {
              var custid = $('#hdnCustId').val();

              if (custid == "") {
                  custid = $('#content_hdnId').val()
              }
              var msgData = {
                  id: $('#hdnMsgId').val() == "" ? 0 : $('#hdnMsgId').val(),
                  customerId: custid,
                  text: $('#txtMessage').val(),
                  isPinned: $('#chkIsPinned').is(':checked')
              };
              console.log(msgData);
              $.ajax({
                  type: "POST",
                  url: "Customer.aspx/SaveMessage",
                  data: JSON.stringify(msgData),
                  contentType: "application/json; charset=utf-8",
                  dataType: "json",
                  success: function (response) {
                      if (response.d === "Success") {
                          // Refresh only the message list inside the modal
                          loadMessages(msgData.customerId);

                          // Reset form for next entry
                          $('#hdnMsgId').val("0");
                          $('#txtMessage').val("");
                          $('#chkIsPinned').prop('checked', false);
                          alert('Saved successfully!');
                      }
                  }
              });
          }
          // EDIT FUNCTION: Fills the inputs without a postback
          function editMsg(id, text, isPinned) {
              // 1. Put the ID into the hidden field so the system knows this is an UPDATE
              $('#hdnMsgId').val(id);

              // 2. Put the text and checkbox status back into the form
              $('#txtMessage').val(text);
              $('#chkIsPinned').prop('checked', isPinned);

              // 3. Focus the user on the textbox
              $('#txtMessage').focus();
          }

          // DELETE FUNCTION: Removes record and refreshes table
          function deleteMsg(id) {
              if (confirm("Are you sure you want to delete this message?")) {
                  $.ajax({
                      type: "POST",
                      url: "Customer.aspx/DeleteMessage",
                      data: JSON.stringify({ id: id }),
                      contentType: "application/json; charset=utf-8",
                      dataType: "json",
                      success: function (response) {
                          if (response.d === "Deleted") {
                              // Refresh the message list for the current customer
                              loadMessages($('#hdnCustId').val());
                          }
                      }
                  });
              }
          }

          function loadCustomerProfileMessages(custId) {
              if (!custId || custId == 0) return;
              if (custId == "") {
                  custId = $('#content_hdnId').val()
              }
              $('#hdnCustId').val(custId);

              $.ajax({
                  type: "POST",
                  url: "Customer.aspx/GetMessagesHTML",
                  data: JSON.stringify({ customerId: custId }),
                  contentType: "application/json; charset=utf-8",
                  success: function (r) {
                      $('#messageListArea').html(r.d);
                      loadMessages(custId);
                  }
              });
          }
          $(document).ready(function () {
              // This listener triggers every time the modal is closed
              $('#messageModal').on('hidden.bs.modal', function () {
                  var custId = $('#content_hdnCustomerId').val(); // Get current Customer ID from your hidden field

                  if (custId && custId !== "0") {
                      // Refresh the background list in the Edit Form
                      loadCustomerProfileMessages(custId);
                  }
              });
          });
      </script>
</asp:Content>
