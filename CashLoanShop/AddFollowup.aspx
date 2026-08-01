<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AddFollowup.aspx.cs" Inherits="CashLoanShop.AddFollowup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            $('.historybutton').click(function () {
                var id = $(this).attr('id');
                openhistory(id);
            });

        });
        function openhistory(obj) {
            window.open('/CustomerCashHistory.aspx?Id=' + obj + '&IsCurrencyExchange=false&IsLoan=false&IsTermLoan=false&IsFollowup=true', '_blank', 'width=1400,height=750,location=no,left=200px');
        }
    </script>
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
                                            <asp:Button ID="btnViewTransaction" class="btn btn-purple" Text="List Followup" runat="server" OnClick="btnViewTransaction_Click"></asp:Button>
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
                                             <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="">
                                                <ItemTemplate>
                                                    <a id='<%# Eval("Id") %>' class="historybutton"   target="_blank" >View History</a>
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
                <div>
                    <div class="col-md-10">
                        <div class="row">
                            <div class="panel panel-primary">
                                <div class="panel-heading" runat="server" id="dvIsMoreloan">
                                    <h3 class="panel-title"></h3>
                                </div>
                                <div class="panel-body">
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Customer Name:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="txtcustomerName" Enabled="false" MaxLength="100" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Phone No:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control numerictext" ID="txtPhoneNo" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Customer Id:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control numerictext" ID="txtCustomerId" Enabled="false" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Followup Date:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="txtFollowupDate" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Followup Code:</label>
                                            <div class="controls col-md-7">
                                                <asp:DropDownList ID="ddlFollowupCode" runat="server" CssClass="form-control" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="--Select Option--" Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Left Voicemail (LVM)" Value="Left Voicemail (LVM)"></asp:ListItem>
                                                    <asp:ListItem Text="Call back (CB)" Value="Call back (CB)"></asp:ListItem>
                                                    <asp:ListItem Text="No Answer and Not available (NA)" Value="No Answer and Not available (NA)"></asp:ListItem>
                                                    <asp:ListItem Text="Will Come and pay on another date (WCP)" Value="Will Come and pay on another date (WCP)"></asp:ListItem>
                                                    <asp:ListItem Text="Emailed for Payment (Email)" Value="Emailed for Payment (Email)"></asp:ListItem>
                                                    <asp:ListItem Text="Left message on work for call back (LMW)" Value="Left message on work for call back (LMW)"></asp:ListItem>
                                                    <asp:ListItem Text="Left message with Reference one (LMR1)" Value="Left message with Reference one (LMR1)"></asp:ListItem>
                                                    <asp:ListItem Text="Left message with Reference two (LMR2)" Value="Left message with Reference two (LMR2)"></asp:ListItem>
                                                    <asp:ListItem Text="Approved later (AAL)" Value="Approved later (AAL)"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Comment:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="txtComments" TextMode="MultiLine" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Next Followup Date:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="txtNextFollowupDate" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Next Followup Time:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="txtNextFollowupTime" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Final Status:</label>
                                            <div class="controls col-md-7">
                                                <asp:DropDownList ID="ddlFinalStatus" runat="server" CssClass="form-control" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="--Select Option--" Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Bankruptcy" Value="Bankruptcy"></asp:ListItem>
                                                    <asp:ListItem Text="Consumer Proposal" Value="Consumer Proposal"></asp:ListItem>
                                                    <asp:ListItem Text="Debt Management(Credit Proposal)" Value="Debt Management(Credit Proposal)"></asp:ListItem>
                                                    <asp:ListItem Text="Collections" Value="Collections"></asp:ListItem>
                                                    <asp:ListItem Text="Reported to credit buerue" Value="Reported to credit buerue"></asp:ListItem>
                                                    <asp:ListItem Text="Legal(Plaintiff claim)" Value="Legal(Plaintiff claim)"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Followup Time:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="txtFollowupTime" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Followup Done by:</label>
                                            <div class="controls col-md-7">
                                                <asp:TextBox CssClass="form-control" ID="txtFollowupDoneby" runat="server" />
                                            </div>
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
        </asp:View>
        <asp:View ID="vFolloupList" runat="server">
            <div class="page-content">
                <div>
                    <div class="col-md-10">
                        <asp:Button ID="btnBack" class="btn btn-primary" Text="Back to Search" runat="server" OnClick="btnBack_Click"></asp:Button>
                    </div>
                    <div class="col-md-10">
                        <table style="width: 100%; padding-top: 20px;" class="EU_DataTable">
                            <asp:Repeater ID="rptGridData" runat="server" OnItemCommand="rptGridData_ItemCommand">
                                <HeaderTemplate>
                                    <th>Followup Date
                                    </th>
                                    <th>Followup Time
                                    </th>
                                    <th>Customer Name
                                    </th>
                                    <th>Customer Id
                                    </th>
                                    <th>Contact No
                                    </th>
                                    <th>Final Status
                                    </th>
                                    <th>Followup Code
                                    </th>
                                    <th>Comment
                                    </th>
                                    <th>Followup Done by
                                    </th>
                                    <th></th>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "FollowupDate", "{0:MM/dd/yyyy}")).Replace("-","/") %>
                                        </td>
                                        <td>
                                            <%# Eval("FollowupTime") %>
                                        </td>
                                        <td>
                                            <%# Eval("CustomerName") %>
                                        </td>
                                        <td>
                                            <%# Eval("CustomerId") %>
                                        </td>
                                        <td>
                                            <%# Eval("ContactNo") %>
                                        </td>
                                        <td>
                                            <%# Eval("FinalStatus") %>
                                        </td>
                                        <td>
                                            <%# Eval("FollowupCode") %>
                                        </td>
                                        <td>
                                            <%# Eval("Comments") %>
                                        </td>
                                        <td>
                                            <%# Eval("FollowupDoneBy") %>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("Id") %>' CommandName="Edit" Text="Edit"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>

                            </asp:Repeater>
                        </table>
                    </div>
                </div>
            </div>
        </asp:View>
    </asp:MultiView>
    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
    <asp:HiddenField ID="hdnCustomerId" runat="server" Value="0" />
    <script type="text/javascript">

        $(document).ready(function () {
            $('#' + '<%= txtFollowupDate.ClientID %>').datetimepicker({
                format: 'L'
            });
            $('#' + '<%= txtFollowupTime.ClientID %>').datetimepicker({
                format: 'LT'
            });
            $('#' + '<%= txtNextFollowupDate.ClientID %>').datetimepicker({
                format: 'L'
            });
            $('#' + '<%= txtNextFollowupTime.ClientID %>').datetimepicker({
                format: 'LT'
            });
        });
    </script>

   
</asp:Content>
