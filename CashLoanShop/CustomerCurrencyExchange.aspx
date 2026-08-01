<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CustomerCurrencyExchange.aspx.cs" Inherits="CashLoanShop.CustomerCurrencyExchange" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>

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
            $('#btnCalculate').click(function () {
                var TransactionType = $("[id*=rbtnlstTransactionType] input:checked").val();
                //alert(TransactionType);
                var ExchangeRate = $(<%=txtExchangeRate.ClientID %>).val();
                var AmounttoExchange = $(<%=txtAmounttoExchange.ClientID %>).val();
                var ServiceCharge = $(<%=txtServiceCharge.ClientID %>).val();
                var CurrencyCode = $(<%=ddlCurrencyType.ClientID %>).val();
                var ConvertedValue = parseFloat(0);
                if (TransactionType == "Buy") {
                    ConvertedValue = parseFloat((ExchangeRate * AmounttoExchange) - parseFloat(ServiceCharge).toFixed(2)).toFixed(2);    // if buy then minus the service charge
                }
                else if (TransactionType == "Sell") {
                    ConvertedValue = parseFloat((parseFloat(ExchangeRate).toFixed(2) * parseFloat(AmounttoExchange).toFixed(2))).toFixed(2);
                    ConvertedValue = parseFloat(ConvertedValue) + parseFloat(ServiceCharge);    //  if sell than plus
                }

                $(<%=lblTotalAmountDue.ClientID %>).text(TransactionType.toString() + ' ' + CurrencyCode + ' ' + parseFloat(AmounttoExchange).toFixed(2).toString() + ' for CAD ' + parseFloat(ConvertedValue).toFixed(2).toString());

                $('#' + '<%= hdnDueAmount.ClientID %>').val(parseFloat(ConvertedValue).toFixed(2).toString());
            });
        });
        function validateform() {
            //alert($("#TxtFirstName").val());
            if ($('#' + '<%= txtAmounttoExchange.ClientID %>').val() == '' || $('#' + '<%= txtAmounttoExchange.ClientID %>').val() == undefined) {
                alert('Please enter  Amount to Exchange');

                $('#' + '<%= txtAmounttoExchange.ClientID %>').focus();
                $('#' + '<%= txtAmounttoExchange.ClientID %>').attr("style", "border-color:red;");
                return false;
            }

            else {
                $('#' + '<%= txtAmounttoExchange.ClientID %>').removeAttr("style");
            }


            if ($('#' + '<%= txtExchangeRate.ClientID %>').val() == '' || $('#' + '<%= txtExchangeRate.ClientID %>').val() == undefined) {
                alert('Please enter Exchange Rate');

                $('#' + '<%= txtExchangeRate.ClientID %>').focus();
                $('#' + '<%= txtExchangeRate.ClientID %>').attr("style", "border-color:red;");
                return false;
            }

            else {
                $('#' + '<%= txtExchangeRate.ClientID %>').removeAttr("style");
            }

            if ($('#' + '<%= ddlShopStore.ClientID %>').val() == '0' || $('#' + '<%= ddlShopStore.ClientID %>').val() == undefined) {
                alert('Please select Shop Store');

                $('#' + '<%= ddlShopStore.ClientID %>').focus();
                $('#' + '<%= ddlShopStore.ClientID %>').attr("style", "border-color:red;");
                return false;
            }

            else {
                $('#' + '<%= ddlShopStore.ClientID %>').removeAttr("style");
            }

            if ($('#' + '<%= txtServiceCharge.ClientID %>').val() == '' || $('#' + '<%= txtServiceCharge.ClientID %>').val() == undefined) {
                alert('Please enter Service Charge');

                $('#' + '<%= txtServiceCharge.ClientID %>').focus();
                $('#' + '<%= txtServiceCharge.ClientID %>').attr("style", "border-color:red;");
                return false;
            }

            else {
                $('#' + '<%= txtServiceCharge.ClientID %>').removeAttr("style");
            }


            if ($('#' + '<%= ddlCurrencyType.ClientID %>').val() == '' || $('#' + '<%= ddlCurrencyType.ClientID %>').val() == undefined) {
                alert('Please select Exchange Currency');
                $('#' + '<%= ddlCurrencyType.ClientID %>').focus();
                $('#' + '<%= ddlCurrencyType.ClientID %>').attr("style", "border-color:red;");
                return false;
            }

            else {
                $('#' + '<%= ddlCurrencyType.ClientID %>').removeAttr("style");
            }

           <%-- if ($('#' + '<%= rbtnlstLoanType.ClientID %>').val() == '' || $('#' + '<%= rbtnlstLoanType.ClientID %>').val() == undefined) {
                alert('Please select Loan Type');

                $('#' + '<%= rbtnlstLoanType.ClientID %>').focus();
                $('#' + '<%= rbtnlstLoanType.ClientID %>').attr("style", "border-color:red;");
                 return false;
             }

             else {
                 $('#' + '<%= rbtnlstLoanType.ClientID %>').removeAttr("style");
            }--%>

            $('#btnCalculate').click();
        }

        function call(obj) {
            window.open('/CurrencyExchangeReceipt.aspx?Id=' + obj + '', '_blank', 'width=800,height=550,location=no,left=200px');
        }
        function openhistory(obj) {
            window.open('/CustomerCashHistory.aspx?Id=' + obj + '&IsCurrencyExchange=true&IsLoan=true', '_blank', 'width=1200,height=750,location=no,left=200px');
        }
    </script>
    <style type="text/css">
        .col-md-7 {
            font-family: Verdana;
            font-weight: bold !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:MultiView ID="mvView" runat="server" ActiveViewIndex="0">
        <asp:View ID="vList" runat="server">
            <div class="page-content">
                <div class="col-xs-10">
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
                                            <asp:Button ID="btnViewTransaction" class="btn btn-purple" Visible="false" Text="View Currency Entry" runat="server" OnClick="btnViewTransaction_Click"></asp:Button>
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
                                            <asp:BoundField DataField="Dateofbirth" HeaderStyle-HorizontalAlign="Left" HeaderText="Birth Date" DataFormatString="{0:MM/dd/yyyy}" />

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
                <div class="col-xs-12">
                    <div class="row">
                        <asp:Repeater ID="rptCustomer" runat="server">
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
                                                    <label class="col-md-5 control-label">Email:</label>
                                                    <label class="col-md-7">
                                                        <%# Eval("DrivingLicenseNumber") %>
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
                                            </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                </div>
            </div>

            <div class="panel panel-primary">
                <div class="panel-body">
                    <div class="col-md-5">
                        <div class="form-group">
                            <label class="col-md-5 control-label">Transaction Type:</label>
                            <div class="controls col-md-7">
                                <asp:RadioButtonList ID="rbtnlstTransactionType" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Sell" Value="Sell" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Buy" Value="Buy"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-5 control-label">Exchange Currency:</label>
                            <div class="controls col-md-7">
                                <asp:DropDownList ID="ddlCurrencyType" CssClass="select" Width="300" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-5 control-label">Amount to Exchange:</label>
                            <div class="controls col-md-7">
                                <asp:TextBox CssClass="form-control numerictext" ID="txtAmounttoExchange" MaxLength="100" runat="server" />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-5">
                                <input type="button" value="Calculate Due Amount" id="btnCalculate" name="btnCalculate" class="btn btn-primary" />
                            </div>
                            <label class="col-md-7 control-label">
                                <asp:Label ID="lblTotalAmountDue" Text="" Style="font-weight: bold;" runat="server" /></label>

                        </div>
                    </div>
                    <div class="col-md-5">
                        <div class="form-group">
                            <label class="col-md-5 control-label">Select Store:</label>
                            <div class="controls col-md-7">
                                <asp:DropDownList ID="ddlShopStore" class="form-control" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-5 control-label">Exchange Rate:</label>
                            <div class="controls col-md-7">
                                <asp:TextBox CssClass="form-control numerictext" ID="txtExchangeRate" MaxLength="100" runat="server" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-5 control-label">Service Charge:</label>
                            <div class="controls col-md-7">
                                <asp:TextBox CssClass="form-control numerictext" ID="txtServiceCharge" MaxLength="100" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group">

                            <div class="col-md-offset-3 col-md-9">
                                <asp:Button ID="btnSubmit" class="btn btn-primary" Text="Save" OnClientClick="javascript:return validateform();" runat="server" OnClick="btnSubmit_Click"></asp:Button>
                                <asp:Button ID="btnCancel" class="btn btn-pink" Text="Cancel" runat="server" OnClick="btnCancel_Click"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


        </asp:View>
        <asp:View ID="vCashCheque" runat="server">
            <div class="page-content">
                <div class="col-xs-12">
                    <div class="row">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Currency Records Search</h3>
                            </div>
                            <div class="panel-body">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-5 control-label">Customer's FirstName / LastName :</label>
                                        <div class="controls col-md-7">
                                            <asp:TextBox ID="txtSearchCustomerName" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-5 control-label">Exchange Type :</label>
                                        <div class="controls col-md-7">
                                            <asp:DropDownList ID="ddlSearchExchangeType" class="form-control" runat="server">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Sell" Value="Sell"></asp:ListItem>
                                                <asp:ListItem Text="Buy" Value="Buy"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-5 control-label"></label>
                                        <div class="controls col-md-7">
                                            <asp:Button ID="btnSearchCashEntryData" class="btn btn-primary" Text="Search" runat="server" OnClick="btnSearchCashEntryData_Click"></asp:Button>
                                            <asp:Button ID="btnCustomerSearch" class="btn btn-pink" Text="Back to Customer" runat="server" OnClick="btnCustomerSearch_Click"></asp:Button>
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
                                    <asp:GridView ID="dgvCashEntry" CssClass="EU_DataTable" AllowPaging="true" PageSize="15" class="table table-hover table-bordered" AutoGenerateColumns="false" runat="server" OnRowCommand="dgvCashEntry_RowCommand" OnPageIndexChanging="dgvCashEntry_PageIndexChanging" OnRowDataBound="dgvCashEntry_RowDataBound">
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
                                            <asp:BoundField DataField="Id" HeaderStyle-HorizontalAlign="Left" HeaderText="ID" />
                                            <asp:BoundField DataField="CustomerName" HeaderStyle-HorizontalAlign="Left" HeaderText="Customer Name" />
                                            <asp:BoundField DataField="TransactionType" HeaderStyle-HorizontalAlign="Left" HeaderText="Transaction Type" />
                                            <asp:BoundField DataField="CurrencyType" HeaderStyle-HorizontalAlign="Left" HeaderText="Currency Type" />
                                            <asp:BoundField DataField="ExchangeRate" HeaderStyle-HorizontalAlign="Left" HeaderText="Exchange Rate" />
                                            <asp:BoundField DataField="ExchangeAmount" HeaderStyle-HorizontalAlign="Left" HeaderText="Exchange Amount" />
                                            <asp:BoundField DataField="DueAmount" HeaderStyle-HorizontalAlign="Left" HeaderText="Due Amount" />
                                            <asp:BoundField DataField="ConvertedAmount" HeaderStyle-HorizontalAlign="Left" HeaderText="CAD Converted Amount" />


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
    </asp:MultiView>

    <asp:HiddenField ID="hdnDueAmount" runat="server" />
    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
    <asp:HiddenField ID="hdnCustomerId" runat="server" Value="0" />
</asp:Content>

