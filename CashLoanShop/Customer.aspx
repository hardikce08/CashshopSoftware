<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" EnableEventValidation="false" EnableViewStateMac="false" AutoEventWireup="true" CodeBehind="Customer.aspx.cs" Inherits="CashLoanShop.Customer" %>

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
                                                    <button type="button" class="btn btn-info btn-sm" 
                onclick='openMessageModal(<%# Eval("Id") %>)'>
            Add Message
        </button>
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
                <button type="button" class="btn btn-success btn-sm" onclick="openMessageModal($('#content_hdnId').val())">
                    <i class="fa fa-plus"></i> Manage Messages
                </button>
            </div>
        </div>
    </div>
</div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Personal Information</h3>
                                </div>
                                <div class="panel-body">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">First Name :</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox ID="TxtFirstName" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Mi:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox ID="TxtMi" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Gender:</label>
                                            <div class="controls col-md-7">
                                                <asp:DropDownList ID="ddlGender" CssClass="select" runat="server">
                                                    <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                                                    <asp:ListItem Text="FeMale" Value="FeMale"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">City:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="TxtCity" MaxLength="100" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">PostCode:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="TxtPostCode" MaxLength="100" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Do You:</label>
                                            <div class="controls col-md-7">
                                                <asp:DropDownList ID="ddlHomeType" CssClass="select" runat="server">
                                                    <asp:ListItem Text="Select Home Type" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="Rent" Value="Rent"></asp:ListItem>
                                                    <asp:ListItem Text="Own a Home" Value="Own a Home"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Work Phone:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="TxtWorkPhone" MaxLength="100" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Pay Cycle Comment:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="TxtPhoneListedunder" MaxLength="100" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Driving License:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="TxtDrivingLicenseNumber" MaxLength="100" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Comments:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="TxtComments" TextMode="MultiLine" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Last Name :</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox ID="TxtLastName" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Date of Birth :</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox ID="TxtDateofbirth" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Address:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="TxtAddress" TextMode="MultiLine" runat="server" />
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
                                            <label class="col-md-5 control-label">Duration at this Address:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox ID="TxtDurationYears" CssClass="numerictext" Style="width: 30px;" runat="server" />
                                                Years<asp:TextBox Style="width: 30px;" CssClass="numerictext" ID="TxtDurationMonth" runat="server" />
                                                Months
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Home Phone:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="TxtHomePhone" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Cell Phone:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="TxtCellPhone" MaxLength="100" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Social Security Number:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="TxtSocialSecurityNumber" MaxLength="100" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Issues At:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="TxtIssuedAt" MaxLength="100" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Proof of Identity:</label>
                                            <div class="controls col-md-7">
                                                <asp:FileUpload ID="fupImage" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Income Information</h3>
                                </div>
                                <div class="panel-body">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Take Home Pay:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control numerictext" ID="TxtTakehomePay" MaxLength="100" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Frequency of Pay:</label>
                                            <div class="controls col-md-7">
                                                <asp:DropDownList ID="ddlFreuencyofPay" CssClass="select" runat="server">
                                                    <asp:ListItem Text="Select Frequency of Pay" Value="0"></asp:ListItem>
                                                    <asp:ListItem Value="1">Weekly</asp:ListItem>
                                                    <asp:ListItem Value="2">Bi-Weekly</asp:ListItem>
                                                    <asp:ListItem Value="3">Twice Monthly</asp:ListItem>
                                                    <asp:ListItem Value="4">Monthly</asp:ListItem>
                                                    <asp:ListItem Value="5">No Fix Date</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:RadioButton ID="CbxIsDirectDeposit" GroupName="abc" runat="server" />
                                            Direct Deposit
                                    <asp:RadioButton ID="CbxIsComputerized" GroupName="abc" runat="server" />
                                            Cheque Payment 
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Employment Information</h3>
                                </div>
                                <div class="panel-body">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Employer Name:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="TxtEmployerName" MaxLength="100" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">City:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="txtEmployerCity" MaxLength="100" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">PostCode:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="txtEmployerPostCode" MaxLength="100" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Employer Type:</label>
                                            <div class="controls col-md-7">
                                                <asp:DropDownList ID="ddlEmployerType" CssClass="form-control" runat="server">
                                                    <asp:ListItem Text="Select Type" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Full Time" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Part Time" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Supervisor Phone:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="TxtSupervisorPhone" MaxLength="100" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">HR/Work Phone:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="TxtHRPhone" MaxLength="100" runat="server" />
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Supervisor Name:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="TxtSupervisorName" MaxLength="100" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Province:</label>
                                            <div class="controls col-md-7">
                                                <asp:DropDownList ID="ddlEmploymentProvince" CssClass="form-control" runat="server">
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
                                            <label class="col-md-5 control-label">Employer Duration:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox ID="TxtEmployerDurationYears" CssClass="numerictext" Style="width: 30px;" runat="server" />
                                                Years<asp:TextBox Style="width: 30px;" CssClass="numerictext" ID="TxtEmployerDurationMonth" runat="server" />
                                                Months
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Ext:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="TxtSupervisorExt" MaxLength="100" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Ext:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="TxtHRExt" MaxLength="100" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Address:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="txtEmployerAddress" TextMode="MultiLine" MaxLength="100" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Banking Information</h3>
                                </div>
                                <div class="panel-body">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Bank Name:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="TxtBankName" MaxLength="100" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Transit No:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="txtTransitNo" MaxLength="100" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">City:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="txtBankCity" MaxLength="100" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">PostCode:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="txtBankPostcode" MaxLength="100" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Ever claimed Bankrutcy:</label>
                                            <div class="controls col-md-7">
                                                <asp:CheckBox ID="CbxIsBankrutcy" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Address:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="txtBankAddress" TextMode="MultiLine" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Account Number:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="TxtAccountNumber" MaxLength="100" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Institution No:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="txtInstitutionNo" MaxLength="100" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Province:</label>
                                            <div class="controls col-md-7">
                                                <asp:DropDownList ID="ddlBankProvince" CssClass="form-control" runat="server">
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
                                            <label class="col-md-5 control-label">No. of NSF:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control numerictext" ID="TxtNoofSNF" MaxLength="100" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">
                                                Any Wage Assignments /<br />
                                                Garnishes against Pay:</label>
                                            <div class="controls col-md-7">
                                                <asp:CheckBox ID="CbxIsAssignments" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>


                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Reference Information</h3>
                                </div>
                                <div class="panel-body">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Reference1 Name:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="TxtReference1Name" MaxLength="100" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Reference2 Name:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="TxtReference2Name" MaxLength="100" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Reference1 Phone:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="TxtReference1Phone" MaxLength="100" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Reference2 Phone:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="TxtReference2Phone" MaxLength="100" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Reference1 Relation:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="TxtReference1Relation" MaxLength="100" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Reference2 Relation:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="TxtReference2Relation" MaxLength="100" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group">
                            <label class="col-md-3 control-label"></label>
                            <div class="controls col-md-9">
                                <asp:Button ID="btnSubmit" class="btn btn-primary" Text="Save" OnClientClick="javascript:return validateform();" runat="server" OnClick="btnSubmit_Click"></asp:Button>
                                <asp:Button ID="btnCancel" class="btn btn-pink" Text="Cancel" runat="server" OnClick="btnCancel_Click"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:View>
    </asp:MultiView>

    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
    <asp:HiddenField ID="hdnvalidationmode" runat="server" Value="0" />
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
    <script type="text/javascript">


        $(document).ready(function () {

            $('#' + '<%= TxtDateofbirth.ClientID %>').datetimepicker({
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
        });


        function validateform() {
            //alert($("#TxtFirstName").val());
            var validationtype = $('#' + '<%= hdnvalidationmode.ClientID %>').val();
            if ($('#' + '<%= TxtFirstName.ClientID %>').val() == '' || $('#' + '<%= TxtFirstName.ClientID %>').val() == undefined) {
                alert('Please enter FirstName');

                $('#' + '<%= TxtFirstName.ClientID %>').focus();
                $('#' + '<%= TxtFirstName.ClientID %>').attr("style", "border-color:red;");
                return false;
            }

            else {
                $('#' + '<%= TxtFirstName.ClientID %>').removeAttr("style");
            }


            if ($('#' + '<%= TxtLastName.ClientID %>').val() == '' || $('#' + '<%= TxtLastName.ClientID %>').val() == undefined) {
                alert('Please enter LastName');
                $('#' + '<%= TxtLastName.ClientID %>').focus();
                $('#' + '<%= TxtLastName.ClientID %>').attr("style", "border-color:red;");
                return false;
            }

            else {
                $('#' + '<%= TxtLastName.ClientID %>').removeAttr("style");
            }

         <%--   if (($('#' + '<%= TxtAddress.ClientID %>').val() == '' || $('#' + '<%= TxtAddress.ClientID %>').val() == undefined) && (validationtype == 'customer' || validationtype == 'loancustomer')) {
                alert('Please enter Address');
                $('#' + '<%= TxtAddress.ClientID %>').focus();
                $('#' + '<%= TxtAddress.ClientID %>').attr("style", "border-color:red;");
                return false;
            }

            else {
                $('#' + '<%= TxtAddress.ClientID %>').removeAttr("style");
            }--%>
            //if ((!$('input[type="file"]').val()) && (validationtype == 'customer' || validationtype == 'loancustomer')) {
            //    alert('Please select Proof of Identity');
            //    $('input[type="file"]').focus();
            //    $('input[type="file"]').attr("style", "border-color:red;");
            //    return false;
            //}

            //else {
            //    $('input[type="file"]').removeAttr("style");
            //}

           <%-- if (($('#' + '<%= TxtDateofbirth.ClientID %>').val() == '' || $('#' + '<%= TxtDateofbirth.ClientID %>').val() == undefined) && validationtype == 'notcustomer') {
                alert('Please enter Dateofbirth');
                $('#' + '<%= TxtDateofbirth.ClientID %>').focus();
                $('#' + '<%= TxtDateofbirth.ClientID %>').attr("style", "border-color:red;");
                return false;
            }

            else {
                $('#' + '<%= TxtDateofbirth.ClientID %>').removeAttr("style");
            }

            if ($('#' + '<%= TxtCellPhone.ClientID %>').val() == '' || $('#' + '<%= TxtCellPhone.ClientID %>').val() == undefined) {
                alert('Please enter CellPhone');
                $('#' + '<%= TxtCellPhone.ClientID %>').focus();
                $('#' + '<%= TxtCellPhone.ClientID %>').attr("style", "border-color:red;");
                return false;
            }

            else {
                $('#' + '<%= TxtCellPhone.ClientID %>').removeAttr("style");
            }--%>

     <%--       if (($('#' + '<%= TxtEmployerName.ClientID %>').val() == '' || $('#' + '<%= TxtEmployerName.ClientID %>').val() == undefined) && validationtype == 'customer') {
                alert('Please enter EmployerName');
                $('#' + '<%= TxtEmployerName.ClientID %>').focus();
                $('#' + '<%= TxtEmployerName.ClientID %>').attr("style", "border-color:red;");
                return false;
            }

            else {
                $('#' + '<%= TxtEmployerName.ClientID %>').removeAttr("style");
            }--%>

            if (($('#' + '<%= TxtTakehomePay.ClientID %>').val() == '' || $('#' + '<%= TxtTakehomePay.ClientID %>').val() == undefined) && (validationtype == 'customer' || validationtype == 'loancustomer')) {
                alert('Please enter TakehomePay');
                $('#' + '<%= TxtTakehomePay.ClientID %>').focus();
                $('#' + '<%= TxtTakehomePay.ClientID %>').attr("style", "border-color:red;");
                return false;
            }

            else {
                $('#' + '<%= TxtTakehomePay.ClientID %>').removeAttr("style");
            }

            if (($('#' + '<%= ddlFreuencyofPay.ClientID %>').val() == '0' || $('#' + '<%= ddlFreuencyofPay.ClientID %>').val() == undefined) && (validationtype == 'customer' || validationtype == 'loancustomer')) {
                alert('Please select Freuency of Pay');

                $('#' + '<%= ddlFreuencyofPay.ClientID %>').focus();
                $('#' + '<%= ddlFreuencyofPay.ClientID %>').attr("style", "border-color:red;");
                return false;
            }

            else {
                $('#' + '<%= ddlFreuencyofPay.ClientID %>').removeAttr("style");
            }

            if (($('#' + '<%= TxtBankName.ClientID %>').val() == '' || $('#' + '<%= TxtBankName.ClientID %>').val() == undefined) && (validationtype == 'customer' || validationtype == 'loancustomer')) {
                alert('Please enter Bank Name');
                $('#' + '<%= TxtBankName.ClientID %>').focus();
                $('#' + '<%= TxtBankName.ClientID %>').attr("style", "border-color:red;");
                return false;
            }

            else {
                $('#' + '<%= TxtBankName.ClientID %>').removeAttr("style");
            }

            if (($('#' + '<%= TxtAccountNumber.ClientID %>').val() == '' || $('#' + '<%= TxtAccountNumber.ClientID %>').val() == undefined) && (validationtype == 'customer' || validationtype == 'loancustomer')) {
                alert('Please enter Account Number');
                $('#' + '<%= TxtAccountNumber.ClientID %>').focus();
                $('#' + '<%= TxtAccountNumber.ClientID %>').attr("style", "border-color:red;");
                return false;
            }

            else {
                $('#' + '<%= TxtAccountNumber.ClientID %>').removeAttr("style");
            }

            if (($('#' + '<%= txtTransitNo.ClientID %>').val() == '' || $('#' + '<%= txtTransitNo.ClientID %>').val() == undefined) && (validationtype == 'customer' || validationtype == 'loancustomer')) {
                alert('Please enter Transit Number');
                $('#' + '<%= txtTransitNo.ClientID %>').focus();
                $('#' + '<%= txtTransitNo.ClientID %>').attr("style", "border-color:red;");
                return false;
            }

            else {
                $('#' + '<%= txtTransitNo.ClientID %>').removeAttr("style");
            }

            if (($('#' + '<%= txtInstitutionNo.ClientID %>').val() == '' || $('#' + '<%= txtInstitutionNo.ClientID %>').val() == undefined) && (validationtype == 'customer' || validationtype == 'loancustomer')) {
                alert('Please enter Institution Number');
                $('#' + '<%= txtInstitutionNo.ClientID %>').focus();
                $('#' + '<%= txtInstitutionNo.ClientID %>').attr("style", "border-color:red;");
                return false;
            }

            else {
                $('#' + '<%= txtInstitutionNo.ClientID %>').removeAttr("style");
            }
               $('#cover-spin').show(0);
        }

        function openMessageModal(customerId) {
            $('#hdnCustId').val(customerId);
            clearForm();
            loadMessages(customerId);
            $('#messageModal').modal('show');
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
                var custId = $('#content_hdnId').val(); // Get current Customer ID from your hidden field

                if (custId && custId !== "0") {
                    // Refresh the background list in the Edit Form
                    loadCustomerProfileMessages(custId);
                }
            });
        });
    </script>
</asp:Content>
