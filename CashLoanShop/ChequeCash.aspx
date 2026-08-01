<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ChequeCash.aspx.cs" Inherits="CashLoanShop.ChequeCash" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .col-md-7 {
            font-family: Verdana;
            font-weight: bold !important;
        }

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
    </style>
    <script type="text/javascript" src="js/jquery.fancybox-1.3.4.js"></script>
    <link href="css/jquery.fancybox-1.3.4.css" rel="stylesheet" type="text/css" media="screen" />
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
                                            <asp:Button ID="btnViewTransaction" class="btn btn-purple" Visible="false" Text="View Cash/Cheque Entry" runat="server" OnClick="btnViewTransaction_Click"></asp:Button>
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
                <div>
                    <div class="col-md-10">
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
                                                            <%# Convert.ToDateTime(Eval("DateofBirth")).ToString("dd/MM/yyyy") %>
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
                <div class="row">
                    <div class="col-md-10">
                        <div class="panel panel-primary">
                            <div class="panel-body">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <div class="col-md-3">
                                            <asp:Button runat="server" Text="Search Cheque Issuer" ID="btnSearchCompany" CssClass="btn btn-purple" OnClick="btnSearchCompany_Click" />
                                        </div>
                                        <label class="control-label">
                                            <asp:Label ID="lblCompany" Style="font-weight: bold; font-size: 18px;" runat="server">
                                            </asp:Label></label>

                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Cheque Type:</label>
                                            <div class="controls col-md-7">
                                                <asp:DropDownList ID="ddlChequeType" CssClass="select" onchange="javascript:return Hideshow();" runat="server">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Govt-Payroll - 2.5%" Value="Govt-Payroll"></asp:ListItem>
                                                    <asp:ListItem Text="Misc - 4%" Value="Misc"></asp:ListItem>
                                                    <asp:ListItem Text="PostDated - 3%" Value="PostDated"></asp:ListItem>
                                                    <asp:ListItem Text="Other - 5%" Value="Other"></asp:ListItem>
                                                    <asp:ListItem Text="Extra - 10%" Value="Extra-10%"></asp:ListItem>
                                                    <asp:ListItem Text="Custom" Value="Custom"></asp:ListItem>
                                                </asp:DropDownList>
                                                <div id="divcustom" style="display: none; padding-top: 5px;">
                                                    Custom
                            <asp:TextBox CssClass="input" ID="txtCustomPer" Style="width: 100px !important;" runat="server"></asp:TextBox>
                                                    %
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Cheque Number:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="txtChequeNumber" MaxLength="100" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Select Store:</label>
                                            <div class="controls col-md-7">
                                                <asp:DropDownList ID="ddlShopStore" CssClass="select" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Cheque Amount:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control numerictext" ID="txtChequeAmount" MaxLength="12" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <div class="col-md-2">
                                            <input type="button" value="Calculate" id="btnCalculate" name="btnCalculate" class="btn btn-primary" />
                                        </div>
                                        <label class="control-label">
                                            <asp:Label ID="lblTotalValue" Style="font-weight: bold; font-size: 18px;" runat="server"></asp:Label>
                                        </label>
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
                                <h3 class="panel-title">Search History</h3>
                            </div>
                            <div class="panel-body">
                                <div class="col-md-10">
                                    <div class="form-group">
                                        <label class="col-md-3 control-label">Customer's FirstName / LastName :</label>
                                        <div class="controls col-md-5">
                                            <asp:TextBox ID="txtSearchCustomerName" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-3 control-label">Company Name :</label>
                                        <div class="controls col-md-5">
                                            <asp:TextBox ID="txtSearchCompanyName" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-3 control-label">Cheque Number :</label>
                                        <div class="controls col-md-5">
                                            <asp:TextBox ID="txtSearchChequeNumber" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-3 control-label"></label>
                                        <div class="controls col-md-9">
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
                                            <asp:BoundField DataField="Id" HeaderStyle-HorizontalAlign="Left" HeaderText="CashCheque ID" />
                                            <asp:BoundField DataField="CustomerName" HeaderStyle-HorizontalAlign="Left" HeaderText="Customer Name" />
                                            <asp:BoundField DataField="CompanyName" HeaderStyle-HorizontalAlign="Left" HeaderText="Company Name" />
                                            <asp:BoundField DataField="ChequeType" HeaderStyle-HorizontalAlign="Left" HeaderText="Cheque Type" />
                                            <asp:BoundField DataField="ChequeNumber" HeaderStyle-HorizontalAlign="Left" HeaderText="Cheque Number" />
                                            <asp:BoundField DataField="ChequeAmount" HeaderStyle-HorizontalAlign="Left" HeaderText="ChequeAmount" />

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
    <asp:HiddenField ID="hdnTotalValue" runat="server" />
    <asp:HiddenField ID="hdnAdminCharge" runat="server" />
    <asp:HiddenField ID="hdnCharge" runat="server" />
    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
    <asp:HiddenField ID="hdnCustomerId" runat="server" Value="0" />
    <asp:HiddenField ID="hdnCompanyId" runat="server" Value="0" />
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
    <script>
        $(document).ready(function () {
            $('.pop').click(function () {
                $('.imagepreview').attr('src', $(this).find('img').attr('src'));
                $('#imagemodal').modal('show');
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

                var ChequeType = $(<%=ddlChequeType.ClientID %>).val();
                var Chequeamt = $(<%=txtChequeAmount.ClientID %>).val();
                var admincharge = parseFloat(0);
                var charges = parseFloat(0);
                if (ChequeType == "Govt-Payroll") {
                    admincharge = 2;
                    charges = ((Chequeamt * 2.5) / 100);    // 2.5%
                }
                if (ChequeType == "Misc") {
                    admincharge = 2;
                    charges = ((Chequeamt * 4) / 100);   // 4%
                }
                if (ChequeType == "Other") {
                    admincharge = 2;
                    charges = ((Chequeamt * 5) / 100);   // 4%
                }
                if (ChequeType == "PostDated") {
                    admincharge = 2;
                    charges = ((Chequeamt * 3) / 100);   // 4%
                }
                if (ChequeType == "Extra-10%") {
                    admincharge = 2;
                    charges = ((Chequeamt * 10) / 100);   // 4%
                }
                if (ChequeType == "Custom") {
                    admincharge = 2;
                    var customper = $('#' + '<%= txtCustomPer.ClientID %>').val();
                    //alert(customper);
                    if (customper == "") {
                        customper = "0";
                        charges = 0;
                    }
                    else {
                        charges = ((Chequeamt * parseFloat($('#' + '<%= txtCustomPer.ClientID %>').val())) / 100);   // 4%
                    }
                }
                var totalvalue = parseFloat(Chequeamt - (admincharge + charges)).toFixed(2);


                $(<%=lblTotalValue.ClientID %>).text(' Amount to be issued:  ' + parseFloat(totalvalue).toFixed(2).toString() + '  (Charges: ' + parseFloat(charges).toFixed(2).toString() + ' + Admin Fee: ' + parseFloat(admincharge).toFixed(2).toString() + ')');
                $(<%=hdnTotalValue.ClientID%>).val(parseFloat(totalvalue).toFixed(2).toString());
                $(<%=hdnCharge.ClientID%>).val(parseFloat(charges).toFixed(2).toString());
                $(<%=hdnAdminCharge.ClientID%>).val(parseFloat(admincharge).toFixed(2).toString());

            });
        });
        function Hideshow() {
            var ChequeType = $(<%=ddlChequeType.ClientID %>).val();
            if (ChequeType == "Custom") {
                $('#divcustom').show();
            }
            else {
                $('#divcustom').hide();
            }
        }
        function validateform() {
            //alert($("#TxtFirstName").val());
            if ($('#' + '<%= hdnCompanyId.ClientID %>').val() == '0' || $('#' + '<%= hdnCompanyId.ClientID %>').val() == undefined) {
                alert('Please select Cheque Issuer');

                $('#' + '<%= btnSearchCompany.ClientID %>').focus();
                $('#' + '<%= btnSearchCompany.ClientID %>').attr("style", "border-color:red;");
                return false;
            }

            else {
                $('#' + '<%= btnSearchCompany.ClientID %>').removeAttr("style");
            }

            if ($('#' + '<%= ddlChequeType.ClientID %>').val() == '0' || $('#' + '<%= ddlChequeType.ClientID %>').val() == undefined) {
                alert('Please select Cheque Type');

                $('#' + '<%= ddlChequeType.ClientID %>').focus();
                $('#' + '<%= ddlChequeType.ClientID %>').attr("style", "border-color:red;");
                return false;
            }

            else {
                $('#' + '<%= ddlChequeType.ClientID %>').removeAttr("style");
            }

            if ($('#' + '<%= ddlChequeType.ClientID %>').val() != '0' && $('#' + '<%= ddlChequeType.ClientID %>').val() != undefined) {
                var ChequeType = $(<%=ddlChequeType.ClientID %>).val();
                if (ChequeType == "Custom") {
                    if ($('#' + '<%= txtCustomPer.ClientID %>').val() == '') {
                        alert('Please enter custom % amount');

                        $('#' + '<%= txtCustomPer.ClientID %>').focus();
                        $('#' + '<%= txtCustomPer.ClientID %>').attr("style", "border-color:red;width:100px;");
                        return false;
                    }
                    else {
                        $('#' + '<%= txtCustomPer.ClientID %>').removeAttr("style");
                        $('#' + '<%= txtCustomPer.ClientID %>').attr("style", "width:100px;");
                    }

                }
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

            if ($('#' + '<%= txtChequeAmount.ClientID %>').val() == '' || $('#' + '<%= txtChequeAmount.ClientID %>').val() == undefined) {
                alert('Please enter Cheque Amount');

                $('#' + '<%= txtChequeAmount.ClientID %>').focus();
                $('#' + '<%= txtChequeAmount.ClientID %>').attr("style", "border-color:red;");
                return false;
            }

            else {
                $('#' + '<%= txtChequeAmount.ClientID %>').removeAttr("style");
            }


            if ($('#' + '<%= txtChequeNumber.ClientID %>').val() == '' || $('#' + '<%= txtChequeNumber.ClientID %>').val() == undefined) {
                alert('Please enter Cheque Number');
                $('#' + '<%= txtChequeNumber.ClientID %>').focus();
                $('#' + '<%= txtChequeNumber.ClientID %>').attr("style", "border-color:red;");
                return false;
            }

            else {
                $('#' + '<%= txtChequeNumber.ClientID %>').removeAttr("style");
            }
            $('#btnCalculate').click();
            $('#cover-spin').show(0);
        }

        function call(obj) {
            window.open('/ChequeCashReceipt.aspx?Id=' + obj + '', '_blank', 'width=800,height=550,location=no,left=200px');
        }
        function openhistory(obj) {
            window.open('/CustomerCashHistory.aspx?Id=' + obj + '', '_blank', 'width=1200,height=750,location=no,left=200px');
        }
    </script>
</asp:Content>
