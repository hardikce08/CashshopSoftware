<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ViewTermReport.aspx.cs" Inherits="CashLoanShop.ViewTermReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .headerrow {
            font-family: Verdana;
            font-size: 23px;
        }

        #content_rbtnlstCollectionOverdue label {
            padding-left: 5px;
        }

        #content_rbtnCollectionStatus label {
            padding-left: 5px;
        }

        #content_rbtnDeptStatus label {
            padding-left: 5px;
        }

        td #contractbtn {
            display: block !important;
        }

        td a#contractbtnnew {
            display: block !important;
            font-size: 12px !important;
            padding: 3px 0px;
            color: white;
            background-color: black;
            border: 1px solid;
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
        <asp:View ID="vopenloan" runat="server">
            <div class="page-content">
                <div class="col-xs-12">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">Loan Report Details</h3>
                        </div>
                        <div class="panel-body">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label class="col-md-5 control-label">Report Type :</label>
                                    <div class="controls col-md-7">
                                        <asp:DropDownList ID="ddlReportType" class="form-control" runat="server">
                                            <asp:ListItem Value="0">--Select Type--</asp:ListItem>
                                            <asp:ListItem Value="Open">Loan Open</asp:ListItem>
                                            <asp:ListItem Value="Due">Loan Due</asp:ListItem>
                                            <asp:ListItem Value="Payment in Process">Payment in Process</asp:ListItem>
                                            <asp:ListItem Value="Over Due">Overdue Loan</asp:ListItem>
                                            <asp:ListItem Value="Sent for Collection">Loan Sent for Collection</asp:ListItem>
                                            <asp:ListItem Value="DEPT Management">DEPT Management</asp:ListItem>
                                            <asp:ListItem Value="Consumer Proposal">Consumer Proposal</asp:ListItem>
                                            <asp:ListItem Value="Bankrupt">Bankrupt</asp:ListItem>
                                            <asp:ListItem Value="Legal">Legal</asp:ListItem>
                                            <asp:ListItem Value="Close">Closed Loan</asp:ListItem>
                                            <asp:ListItem Value="Denied">Denied Loan</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-5 control-label">From Date :</label>
                                    <div class="controls col-md-7">
                                        <asp:TextBox ID="txtFromDate" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-5 control-label">To Date :</label>
                                    <div class="controls col-md-7">
                                        <asp:TextBox ID="txtToDate" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-5 control-label"></label>
                                    <div class="controls col-md-7">
                                        <asp:Button ID="btnSubmit" class="btn btn-primary" Text="Search" OnClientClick="javascript:return ShowProgress();" runat="server" OnClick="btnSubmit_Click"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <table style="width: 100%;" id="loanopengrid" runat="server">
                    <tr class="headerrow">
                        <td style="text-align: center;">
                            <asp:Label ID="lblHeader" runat="server"></asp:Label>&nbsp;<asp:Button ID="btnExport" class="btn btn-primary" runat="server" Text="Export to Excel" OnClick="btnExport_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView Width="100%" ShowFooter="true" ID="dgvLoan" CssClass="EU_DataTable" AllowPaging="true" PageSize="1500" class="table table-hover table-bordered" AutoGenerateColumns="false" runat="server" OnRowCommand="dgvLoan_RowCommand" OnPageIndexChanging="dgvLoan_PageIndexChanging" OnRowDataBound="dgvLoan_RowDataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnSelect" runat="server" Text="Select" CommandName="Select" CommandArgument='<%# Eval("Id") %>'></asp:Button>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:BoundField DataField="Id" HeaderStyle-HorizontalAlign="Left" HeaderText="Contract Number" />
                                    <asp:TemplateField HeaderText="Customer Name" HeaderStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <%# Eval("CustomerName") %>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <span class="footerrow">Grand Total</span>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:BoundField DataField="LoanAmountApproved" HeaderStyle-HorizontalAlign="Left" HeaderText="Loan Amount Approved" />--%>
                                    <asp:TemplateField HeaderText="Loan Amount Approved" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            $<%# Convert.ToDecimal(Eval("LoanAmountApproved"))  + Convert.ToDecimal(Eval("AdminFee")) %>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <span class="footerrow">$</span><asp:Label ID="lblTotalApproved" runat="server" CssClass="footerrow"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Due Amount" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            $<%# Eval("DueAmount") %>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <span class="footerrow">$</span><asp:Label ID="lblTotalDueAmount" runat="server" CssClass="footerrow"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Due Date" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "DueDate", "{0:MM/dd/yyyy}").Replace("-","/") %>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:BoundField DataField="InstallmentAmount" HeaderStyle-HorizontalAlign="Left" HeaderText="Next Installment Amount" />
                                    <asp:TemplateField HeaderText="Remaining Balance Due" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <%--$<%# (Convert.ToDecimal(Eval("DueAmount"))  + (Eval("LateInterestCharge")==null? 0: Convert.ToDecimal( Eval("LateInterestCharge")) ) + (Eval("NSFCharge")==null? 0: Convert.ToDecimal( Eval("NSFCharge")) )) - (Eval("PartialPayment")=="" ? 0:Convert.ToDecimal(Eval("PartialPayment").ToString().Replace("$",""))) %>--%>
                                            <asp:Label ID="lblTotalRemainingDueAmount" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <span class="footerrow">$<asp:Label ID="lblTotalBalanceDue" runat="server" CssClass="footerrow"></asp:Label></span>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remaining Principal amount" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <%--$<%# (Convert.ToDecimal(Eval("DueAmount"))  + (Eval("LateInterestCharge")==null? 0: Convert.ToDecimal( Eval("LateInterestCharge")) ) + (Eval("NSFCharge")==null? 0: Convert.ToDecimal( Eval("NSFCharge")) )) - (Eval("PartialPayment")=="" ? 0:Convert.ToDecimal(Eval("PartialPayment").ToString().Replace("$",""))) %>--%>
                                            <asp:Label ID="lblTotalRemainingPrincipalAmount" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <span class="footerrow">$<asp:Label ID="lblTotalRemainingPrincipalAmountFooter" runat="server" CssClass="footerrow"></asp:Label></span>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remaining Interest amount" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <%--$<%# (Convert.ToDecimal(Eval("DueAmount"))  + (Eval("LateInterestCharge")==null? 0: Convert.ToDecimal( Eval("LateInterestCharge")) ) + (Eval("NSFCharge")==null? 0: Convert.ToDecimal( Eval("NSFCharge")) )) - (Eval("PartialPayment")=="" ? 0:Convert.ToDecimal(Eval("PartialPayment").ToString().Replace("$",""))) %>--%>
                                            <asp:Label ID="lblTotalRemainingInterestAmount" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <span class="footerrow">$<asp:Label ID="lblTotalRemainingInterestAmountFooter" runat="server" CssClass="footerrow"></asp:Label></span>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="PaymentOption" HeaderStyle-HorizontalAlign="Left" HeaderText="Payment Option" />
                                    <asp:TemplateField HeaderText="Loan Type" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <%#   Eval("LoanType")  %>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="Mode of Payment" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <input type="text" class="input" style="background-color: white !important; border: 1px solid black; width: 150px !important;" id='<%# (string.Format("txtmodeofpaymentcancel_{0}", Eval("Id"))) %>' />
                                        </ItemTemplate>

                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <a href="javascript:void(0);" style="margin-left: 2px; color: white; background-color: black; border: 1px solid; padding-top: 6px; padding-bottom: 6px; padding-left: 5px; padding-right: 5px;"
                                                onclick="javascript:return opencancelreceipt('<%# Eval("Id") %>');" id="contractbtn">Cancel Loan</a>
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
                <table style="width: 100%;" id="loanoverduegrid" runat="server">
                    <tr class="headerrow">
                        <td style="text-align: center;">
                            <asp:Label ID="Label1" Text="Loan Over Due" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView Width="100%" ShowFooter="true" ID="dgvOverDue" CssClass="EU_DataTable" AllowPaging="true" PageSize="1500" class="table table-hover table-bordered" AutoGenerateColumns="false" runat="server" OnRowCommand="dgvLoan_RowCommand" OnPageIndexChanging="dgvLoan_PageIndexChanging" OnRowDataBound="dgvOverDue_RowDataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnSelect" runat="server" Text="Select" CommandName="Select" CommandArgument='<%# Eval("Id") %>'></asp:Button>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:BoundField DataField="" HeaderStyle-HorizontalAlign="Left" HeaderText="Customer Name" />--%>
                                    <asp:TemplateField HeaderText="Customer Name" HeaderStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <%# Eval("Customer Name") %>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <span class="footerrow">Grand Total</span>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:BoundField DataField="LoanAmountApproved" HeaderStyle-HorizontalAlign="Left" HeaderText="Loan Amount Approved" />--%>
                                    <%-- <asp:TemplateField HeaderText="Loan Amount Approved" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            $<%# Convert.ToDecimal(Eval("LoanAmountApproved"))  + Convert.ToDecimal(Eval("AdminFee")) %>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <span class="footerrow">$</span><asp:Label ID="lblTotalApproved" runat="server" CssClass="footerrow"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Origional Due Amount" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            $<%# Eval("Origional Due Amount") %>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <span class="footerrow">$</span><asp:Label ID="lblTotalDueAmount" runat="server" CssClass="footerrow"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Due Date" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <%#  Eval("LoanOverDueDate") !=null ? DataBinder.Eval(Container.DataItem, "LoanOverDueDate", "{0:MM/dd/yyyy}").Replace("-","/") : "" %>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:BoundField DataField="InstallmentAmount" HeaderStyle-HorizontalAlign="Left" HeaderText="Next Installment Amount" />
                                    <asp:TemplateField HeaderText="Remaining Balance Due" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <%--$<%# (Convert.ToDecimal(Eval("DueAmount"))  + (Eval("LateInterestCharge")==null? 0: Convert.ToDecimal( Eval("LateInterestCharge")) ) + (Eval("NSFCharge")==null? 0: Convert.ToDecimal( Eval("NSFCharge")) )) - (Eval("PartialPayment")=="" ? 0:Convert.ToDecimal(Eval("PartialPayment").ToString().Replace("$",""))) %>--%>
                                             $<%# Eval("Remaining Due Balance") %>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <span class="footerrow">$</span><asp:Label ID="lblTotalDuePendingAmount" runat="server" CssClass="footerrow"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remaining Principal amount" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            $<%# Eval("remainingPricipal") %>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <span class="footerrow">$<asp:Label ID="lblTotalRemainingPrincipalAmountFooter" runat="server" CssClass="footerrow"></asp:Label></span>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Interest with NSFCharge" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <%--$<%# (Convert.ToDecimal(Eval("DueAmount"))  + (Eval("LateInterestCharge")==null? 0: Convert.ToDecimal( Eval("LateInterestCharge")) ) + (Eval("NSFCharge")==null? 0: Convert.ToDecimal( Eval("NSFCharge")) )) - (Eval("PartialPayment")=="" ? 0:Convert.ToDecimal(Eval("PartialPayment").ToString().Replace("$",""))) %>--%>
                                             $<%# Eval("RemainingInterest") %>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <span class="footerrow">$<asp:Label ID="lblTotalRemainingInterestAmountFooter" runat="server" CssClass="footerrow"></asp:Label></span>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Days" HeaderStyle-HorizontalAlign="Left" HeaderText="Due Days" />
                                    <%--  <asp:TemplateField HeaderText="Total Interest" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            $<%# Math.Round( Convert.ToDecimal (Eval("NSFCharge")) + Convert.ToDecimal (Eval("TillDateInterest")),2)  %>
                                        </ItemTemplate>
                                          <FooterTemplate>
                                            <span class="footerrow">$<asp:Label ID="lblTotalInterestFooter" runat="server" CssClass="footerrow"></asp:Label></span>
                                        </FooterTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Loan Type" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <%#   Eval("LoanType")  %>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:BoundField DataField="PaymentOption" HeaderStyle-HorizontalAlign="Left" HeaderText="Payment Option" />

                                    <%-- <asp:TemplateField HeaderText="Mode of Payment" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <input type="text" class="input" style="background-color: white !important; border: 1px solid black; width: 150px !important;" id='<%# (string.Format("txtmodeofpaymentcancel_{0}", Eval("Id"))) %>' />
                                        </ItemTemplate>

                                    </asp:TemplateField>--%>
                                    <%--   <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <a href="javascript:void(0);" style="margin-left: 2px; color: white; background-color: black; border: 1px solid; padding-top: 6px; padding-bottom: 6px; padding-left: 5px; padding-right: 5px;"
                                                onclick="javascript:return opencancelreceipt('<%# Eval("Id") %>');" id="contractbtn">Cancel Loan</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div style="color: red; text-align: center;">No records found</div>
                                </EmptyDataTemplate>

                            </asp:GridView>
                        </td>
                    </tr>

                </table>
                <table style="width: 100%; display: none;" id="loanclosegrid" runat="server">
                    <tr class="headerrow">
                        <td style="text-align: center;">Loan Closed
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView Width="100%" ShowFooter="true" ID="dgvLoanClose" CssClass="EU_DataTable" AllowPaging="true" PageSize="1500" class="table table-hover table-bordered" AutoGenerateColumns="false" runat="server" OnRowCommand="dgvLoan_RowCommand" OnPageIndexChanging="dgvLoanClose_PageIndexChanging" OnRowDataBound="dgvLoanClose_RowDataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnSelect" runat="server" Text="Select" CommandName="Select" CommandArgument='<%# Eval("Id") %>'></asp:Button>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:BoundField DataField="" HeaderStyle-HorizontalAlign="Left" HeaderText="Customer Name" />--%>
                                    <asp:TemplateField HeaderText="Customer Name" HeaderStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <%# Eval("CustomerName") %>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <span class="footerrow">Grand Total</span>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Loan Amount Approved" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            $<%# Eval("LoanAmountApproved") %>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <span class="footerrow">$<asp:Label ID="lblTotalApprovedAmount" runat="server" CssClass="footerrow"></asp:Label></span>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Loan Amount Due" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            $<%# Eval("DueAmount") %>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <span class="footerrow">$<asp:Label ID="lblTotalDueAmount" runat="server" CssClass="footerrow"></asp:Label></span>
                                        </FooterTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Due Date" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "DueDate", "{0:MM/dd/yyyy}").Replace("-","/") %>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Loan Closed Date" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "UpdatedDate", "{0:MM/dd/yyyy}").Replace("-","/") %>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Over Due Loan Amount" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            $<%# Convert.ToDecimal(Eval("DueAmount"))  + (Eval("LateInterestCharge")==null? 0: Convert.ToDecimal( Eval("LateInterestCharge")) ) + (Eval("NSFCharge")==null? 0: Convert.ToDecimal( Eval("NSFCharge")) ) %>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <span class="footerrow">$<asp:Label ID="lblTotalOverDueLoanAmount" runat="server" CssClass="footerrow"></asp:Label></span>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                   <%-- <asp:TemplateField HeaderText="No. of Loan Over Due" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <%# Eval("OverdueCount")==null ?"0":Eval("OverdueCount") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="           " HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <a href="javascript:void(0);"
                                                onclick="javascript:return openclosereceipt('<%# Eval("Id") %>');" id="contractbtnnew">Print Receipt</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                </Columns>
                                <EmptyDataTemplate>
                                    <div style="color: red; text-align: center;">No records found</div>
                                </EmptyDataTemplate>
                                <FooterStyle Font-Bold="True" />
                            </asp:GridView>
                        </td>
                    </tr>

                </table>
                 <table style="width: 100%; display: none;" id="loanLegalgrid" runat="server">
                    <tr class="headerrow">
                         <td style="text-align: center;">
                            <asp:Label ID="lblLegalHeader" Text="Loan sent to Legal" runat="server"></asp:Label>&nbsp;<asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="Export to Excel" OnClick="btnExportLegal_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView Width="100%" ShowFooter="true" ID="dgvLegal" CssClass="EU_DataTable" AllowPaging="true" PageSize="1500" class="table table-hover table-bordered" AutoGenerateColumns="false" runat="server" OnRowCommand="dgvLoan_RowCommand" OnPageIndexChanging="dgvLoanSentforCollection_PageIndexChanging" OnRowDataBound="dgvLoanSentforCollection_RowDataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnSelect" runat="server" Text="Select" CommandName="Select" CommandArgument='<%# Eval("Id") %>'></asp:Button>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:BoundField DataField="" HeaderStyle-HorizontalAlign="Left" HeaderText="Customer Name" />--%>
                                    <asp:TemplateField HeaderText="Customer Name" HeaderStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <%# Eval("CustomerName") %>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <span class="footerrow">Grand Total</span>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:BoundField DataField="LoanAmountApproved" HeaderStyle-HorizontalAlign="Left" HeaderText="Loan Amount Approved" />--%>
                                    <asp:TemplateField HeaderText="Loan Amount Approved" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            $<%# Eval("LoanAmountApproved") %>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <span class="footerrow">$</span><asp:Label ID="lblTotalApproved" runat="server" CssClass="footerrow"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Due Date" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "DueDate", "{0:MM/dd/yyyy}").Replace("-","/") %>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Loan Amount Due" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            $<%# Eval("DueAmount") %>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <span class="footerrow">$</span><asp:Label ID="lblTotalCollectionDueAmount" runat="server" CssClass="footerrow"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Over Due Loan Amount" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <%--$<%# Convert.ToDecimal(Eval("DueAmount"))  + (Eval("LateInterestCharge")==null? 0: Convert.ToDecimal( Eval("LateInterestCharge")) ) + (Eval("NSFCharge")==null? 0: Convert.ToDecimal( Eval("NSFCharge")) ) %>--%>
                                            <asp:Label ID="lblCollectionDueAmount" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <span class="footerrow">$<asp:Label ID="lblTotalOverDueLoanAmount" runat="server" CssClass="footerrow"></asp:Label></span>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Partial Payments Received" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <%# Eval("PartialPayment") %>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <span class="footerrow">$<asp:Label ID="lblTotalPartialAmountReceived" runat="server" CssClass="footerrow"></asp:Label></span>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Balance Due" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <%--$<%# (Convert.ToDecimal(Eval("DueAmount"))  + (Eval("LateInterestCharge")==null? 0: Convert.ToDecimal( Eval("LateInterestCharge")) ) + (Eval("NSFCharge")==null? 0: Convert.ToDecimal( Eval("NSFCharge")) )) - (Eval("PartialPayment")=="" ? 0:Convert.ToDecimal(Eval("PartialPayment").ToString().Replace("$",""))) %>--%>
                                            <asp:Label ID="lblTotalRemainingDueAmount" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <span class="footerrow">$<asp:Label ID="lblTotalBalanceDue" runat="server" CssClass="footerrow"></asp:Label></span>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="PaymentOption" HeaderStyle-HorizontalAlign="Left" HeaderText="Payment Option" />

                                </Columns>
                                <EmptyDataTemplate>
                                    <div style="color: red; text-align: center;">No records found</div>
                                </EmptyDataTemplate>

                            </asp:GridView>
                        </td>
                    </tr>

                </table>
                    <table style="width: 100%; display: none;" id="loanConsumerProposalgrid" runat="server">
                   <tr class="headerrow">
                         <td style="text-align: center;">
                            <asp:Label ID="Label2" Text="Loan sent to Consumer Proposal" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView Width="100%" ShowFooter="true" ID="dgvConsumerProposal" CssClass="EU_DataTable" AllowPaging="true" PageSize="1500" class="table table-hover table-bordered" AutoGenerateColumns="false" runat="server" OnRowCommand="dgvLoan_RowCommand" OnPageIndexChanging="dgvConsumerProposal_PageIndexChanging" OnRowDataBound="dgvConsumerProposal_RowDataBound" OnRowCancelingEdit="dgvConsumerProposal_RowCancelingEdit">
                                                           
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnSelect" runat="server" Text="Select" CommandName="Select" CommandArgument='<%# Eval("Id") %>'></asp:Button>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:BoundField DataField="CustomerId" HeaderStyle-HorizontalAlign="Left" HeaderText="CustomerId" />
                                    <asp:TemplateField HeaderText="Customer Name" HeaderStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <%# Eval("CustomerName") %>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <span class="footerrow">Grand Total</span>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Loan Due Amount" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            $<%# Eval("DueAmount") %>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <span class="footerrow">$<asp:Label ID="lblTotalDueAmount" runat="server" CssClass="footerrow"></asp:Label></span>
                                        </FooterTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Due Date" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "DueDate", "{0:MM/dd/yyyy}").Replace("-","/") %>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Over Due Loan Amount" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <%-- $<%# Convert.ToDecimal(Eval("DueAmount"))  + (Eval("LateInterestCharge")==null? 0: Convert.ToDecimal( Eval("LateInterestCharge")) ) + (Eval("NSFCharge")==null? 0: Convert.ToDecimal( Eval("NSFCharge")) ) %>--%>
                                            <asp:Label ID="lblLateDueAmount" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <span class="footerrow">$<asp:Label ID="lblTotalOverDueLoanAmount" runat="server" CssClass="footerrow"></asp:Label></span>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Partial Payments Received" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <%# Eval("PartialPayment") %>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <span class="footerrow">$<asp:Label ID="lblTotalPartialAmountReceived" runat="server" CssClass="footerrow"></asp:Label></span>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Balance Due" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <%-- $<%# (Convert.ToDecimal(Eval("DueAmount"))  + (Eval("LateInterestCharge")==null? 0: Convert.ToDecimal( Eval("LateInterestCharge")) ) + (Eval("NSFCharge")==null? 0: Convert.ToDecimal( Eval("NSFCharge")) )) - (Eval("PartialPayment")=="" ? 0:Convert.ToDecimal(Eval("PartialPayment").ToString().Replace("$",""))) %>--%>
                                            <asp:Label ID="lblTotalRemainingDueAmount" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <span class="footerrow">$<asp:Label ID="lblTotalBalanceDue" runat="server" CssClass="footerrow"></asp:Label></span>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                   <%-- <asp:TemplateField HeaderText="Num Times Loan Over Due" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <%# Eval("OverdueCount")==null ?"0":Eval("OverdueCount") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <%--<asp:TemplateField HeaderText="Loan OverDue Reason" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Left" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <%# Eval("OverDueReason") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Mode of Payment" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <%-- <input type="text" class="input" style="background-color: white !important; border: 1px solid black;" id='<%# (string.Format("txtmodeofpaymentcancel_{0}", Eval("Id"))) %>' />--%>
                                            <asp:TextBox ID="txtModeofPayment" Style="background-color: white !important; border: 1px solid black;" CssClass="input" runat="server"></asp:TextBox>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Left" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <%--   <a href="javascript:void(0);" style="margin-left: 2px; color: white; background-color: black; border: 1px solid; padding-top: 6px; padding-bottom: 6px; padding-left: 5px; padding-right: 5px;"
                                            onclick="javascript:return opencancelreceipt('<%# Eval("Id") %>');" id="contractbtn">Cancel Loan</a>--%>
                                            <asp:LinkButton ID="lnkCancel" runat="server" OnClientClick="return confirm('are you sure you want to cancel this loan?');" CommandName="CancelConsumer" CommandArgument='<%# Container.DataItemIndex + "," + Eval("Id") %>' Text="Cancel Loan" Style="margin-left: 2px; color: white; background-color: black; border: 1px solid; padding-top: 6px; padding-bottom: 6px; padding-left: 5px; padding-right: 5px;"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div style="color: red; text-align: center;">No records found</div>
                                </EmptyDataTemplate>
                                <FooterStyle Font-Bold="True" />
                            </asp:GridView>
                        </td>
                    </tr>

                </table>
            </div>
        </asp:View>
        <asp:View ID="vdetails" runat="server">
            <div class="page-content">
                <div id="data" runat="server">
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
                            <asp:Repeater ID="rptCustomer" runat="server">
                                <ItemTemplate>
                                    <div class="panel panel-primary">
                                        <div class="panel-heading">
                                            <h3 class="panel-title">Customer Information</h3>
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
                            <asp:Repeater ID="rptLoan" runat="server" OnItemDataBound="rptLoan_ItemDataBound">
                                <ItemTemplate>
                                    <div class="panel panel-primary">
                                        <div class="panel-heading">
                                            <h3 class="panel-title">Loan Information
                            <a href="javascript:void(0);" style="margin-left: 200px; color: white; background-color: black; border: 1px solid; padding-top: 6px; padding-bottom: 6px; padding-left: 5px; padding-right: 5px;"
                                onclick="javascript:return call('<%# Eval("Id") %>');" id="contractbtn">View Contract</a></h3>
                                        </div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Loan Amount Applied:</label>
                                                        <label class="col-md-7">
                                                            <%# Eval("LoanAmountApplied") %>
                                                        </label>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Total Amount Due:</label>
                                                        <label class="col-md-7">
                                                            <%# Eval("DueAmount") %>
                                                        </label>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Store:</label>
                                                        <label class="col-md-7">
                                                            <%# Eval("StoreAddress").ToString().Replace(",", "<br/>").Replace("$", " , ")  %>
                                                        </label>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Re-Payment Type:</label>
                                                        <label class="col-md-7">
                                                            <%# Eval("LoanType") %> - <a href="javascript:void(0);" onclick="javascript:return openpaymentschedule('<%# Eval("Id") %>');">View Payment Schedule</a>
                                                        </label>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Total Partial Amount Paid:</label>
                                                        <label class="col-md-7">
                                                            <asp:Label ID="lblProcessPArtialAmountPaid" runat="server"></asp:Label>
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Loan Amount Approved:</label>
                                                        <label class="col-md-7">
                                                            <%# Eval("LoanAmountApproved") %>
                                                        </label>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Due Date:</label>
                                                        <label class="col-md-7">
                                                            <%# DataBinder.Eval(Container.DataItem, "DueDate", "{0:MM/dd/yyyy}").Replace("-","/") %>
                                                        </label>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Loan Term:</label>
                                                        <label class="col-md-7">
                                                            <%# Eval("LoanTerm")  %> Year
                                                        </label>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Second InstallmentDate (if any):</label>
                                                        <label class="col-md-7">
                                                            <%# DataBinder.Eval(Container.DataItem, "SecondInstallmentDate", "{0:MM/dd/yyyy}").Replace("-","/") %>
                                                        </label>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Total Remaining Due Amount:</label>
                                                        <label class="col-md-7">
                                                            <asp:Label ID="lblProcessTotalRemainingDueAmount" runat="server"></asp:Label>
                                                        </label>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Pending Due Amount:</label>
                                                        <label class="col-md-7">
                                                            <asp:Label ID="lblPendingDuemount" runat="server"></asp:Label>
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
                </div>
                <div id="loanopened" runat="server">
                    <div class="row">
                        <div class="col-md-10">
                            <div class="panel panel-primary">
                                <div class="panel-body">
                                    <div class="col-md-12">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-md-5">Loan Status :</label>
                                                <div class="col-md-7">
                                                    <asp:DropDownList ID="ddlLoanStatus" class="form-control" onchange="javascript: return changestatus('dropdown');" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5">Mode Of Payment:</label>
                                                <div class="col-md-7">
                                                    <asp:TextBox ssClass="form-control" ID="txtModeofPayment" runat="server" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5">Paid Amount:</label>
                                                <div class="col-md-7">
                                                    <asp:TextBox ssClass="form-control numerictext" ID="txtPartialAmount" runat="server" />
                                                </div>
                                            </div>
                                            <%--  <div class="form-group">
                                                <label class="col-md-5">Late Inerest Charge:</label>
                                                <div class="col-md-7">
                                                    <asp:TextBox ssClass="form-control numerictext" ID="txtLateInterestCharge" runat="server" />
                                                </div>
                                            </div>--%>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <div class="col-md-2">
                                                    OR
                                                </div>
                                                <div class="col-md-10">
                                                    <asp:RadioButtonList ID="rbtnlstLoanType" runat="server" onchange="javascript: return changestatus('radio');" RepeatDirection="Horizontal">
                                                        <asp:ListItem Text="Partial Payment" Value="Open" style="padding: 14px;" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Installment Payment" Value="Open" style="padding: 14px;"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-3">Discount:</label>
                                                <div class="col-md-9">
                                                    <asp:TextBox CssClass="form-control" ID="txtopenDiscount" runat="server" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-3">OverDue Date:</label>
                                                <div class="col-md-7">
                                                    <asp:TextBox CssClass="form-control" ID="txtOverDueDate" Enabled="false" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <div class="col-md-offset-3 col-md-9">
                                                    <asp:Button ID="btnOpenSubmit" class="btn btn-primary" Text="Next" OnClientClick="javascript:return validateform();" runat="server" OnClick="btnOpenLoanSubmit_Click"></asp:Button>
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
            </div>
        </asp:View>
    </asp:MultiView>
    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
    <asp:HiddenField ID="hdnCustomerId" runat="server" Value="0" />
    <asp:HiddenField ID="hdnCurrentstatus" runat="server" Value="0" />
    <asp:HiddenField ID="hdnishidecontractbutton" runat="server" Value="0" />
    <asp:HiddenField ID="hdnInstallmentamount" runat="server" Value="0" />
    <asp:HiddenField ID="hdnTotalLateInstallmentwihtAmount" runat="server" Value="0" />

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


            $('#' + '<%= txtFromDate.ClientID %>').datetimepicker({
                format: 'L'
            });
            $('#' + '<%= txtToDate.ClientID %>').datetimepicker({
                format: 'L'
            });
            $('#' + '<%= txtOverDueDate.ClientID %>').datetimepicker({
                format: 'L'
            });
        });
        function call(obj) {
            window.open('/TermLoanContract.aspx?Id=' + obj + '', '_blank', 'width=800,height=550,location=no,left=200px');
        }
        function openclosereceipt(obj) {
            window.open('/TermLoanCloseReceipt.aspx?Id=' + obj + '', '_blank', 'width=800,height=550,location=no,left=200px');
        }
        function opencancelreceipt(obj) {
            if (confirm('Are you sure you want to cancel this Loan ?')) {
                var objtextbox = $('#txtmodeofpaymentcancel_' + obj).val();
                location.reload(true);
                //window.open('/LoanCancelReceipt.aspx?Id=' + obj + '&Type=TermLoan&data=' + $('#txtmodeofpaymentcancel_' + obj).val(), '_blank', 'width=800,height=550,location=no,left=200px');
                window.open('LoanCancelReceipt.aspx?Id=' + obj + '&Type=TermLoan&data=', '_blank', 'width=800,height=550,location=no,left=200px');

            }
        }
        function changestatus(current) {

            if (current == 'radio') {
                $('#' + '<%= ddlLoanStatus.ClientID %>').prop('selectedIndex', 0);
                var checked_radio = $("[id*=rbtnlstLoanType] input:checked");
                var value = checked_radio.closest("td").find("label").html();
                if (value == 'Installment Payment') {
                    $('#' + '<%= txtPartialAmount.ClientID %>').prop("disabled", true);
                    $('#' + '<%= txtPartialAmount.ClientID %>').val($('#' + '<%= hdnInstallmentamount.ClientID %>').val());
                }
                else {
                    $('#' + '<%= txtPartialAmount.ClientID %>').prop("disabled", false);
                    $('#' + '<%= txtPartialAmount.ClientID %>').val('');
                }
                $('#' + '<%= txtOverDueDate.ClientID %>').prop("disabled", true);
            }
            if (current == 'dropdown') {

                $("[id*=rbtnlstLoanType]").each(function () {
                    $(this).attr('checked', false);
                    $('#' + '<%= txtPartialAmount.ClientID %>').prop("disabled", false);
                    $('#' + '<%= txtPartialAmount.ClientID %>').val('');
                });
                if ($('#' + '<%= ddlLoanStatus.ClientID %>').val() == 'Over Due') {
                    $('#' + '<%= txtOverDueDate.ClientID %>').prop("disabled", false);
                }
                else {
                    $('#' + '<%= txtOverDueDate.ClientID %>').prop("disabled", true);
                }
            }
        }
        function openpaymentschedule(obj) {
            window.open('/ViewPaymentSchedule.aspx?TermLoanId=' + obj, '_blank', 'width=400,height=600,location=no,left=200px');
        }
        function ShowProgress() {
            $('#cover-spin').show(0);
        }
        function resetback()
        {
            $("[id*=rbtnlstLoanType]").each(function () {
                $(this).attr('checked', false);
           });
        }
        function validateform() {
            
            if ($('#' + '<%= ddlLoanStatus.ClientID %>').val() == 'Over Due') {

                if ($('#' + '<%= txtOverDueDate.ClientID %>').val() == '' || $('#' + '<%= txtOverDueDate.ClientID %>').val() == undefined) {
                    alert('Please select OverDue Date');

                    $('#' + '<%= txtOverDueDate.ClientID %>').focus();
                $('#' + '<%= txtOverDueDate.ClientID %>').attr("style", "border-color:red;");
                return false;
            }

            else {
                $('#' + '<%= txtOverDueDate.ClientID %>').removeAttr("style");
            }
        }

        var checked_radio = $("[id*=rbtnlstLoanType] input:checked");
        var value = checked_radio.closest("td").find("label").html();
        if (value == "Partial Payment") {
            if ($('#' + '<%= txtPartialAmount.ClientID %>').val() == '' || $('#' + '<%= txtPartialAmount.ClientID %>').val() == undefined) {
                alert('Please enter  Partial Payment amount');

                $('#' + '<%= txtPartialAmount.ClientID %>').focus();
                $('#' + '<%= txtPartialAmount.ClientID %>').attr("style", "border-color:red;");
                return false;
            }
            else {
                $('#' + '<%= txtPartialAmount.ClientID %>').removeAttr("style");
            }
        }
            $('#cover-spin').show(0);
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
