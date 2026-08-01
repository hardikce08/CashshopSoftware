using CashLoanShop.DataAccess;
using CashLoanShop.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CashLoanShop
{
    public partial class ViewTermReport : System.Web.UI.Page
    {
        public int UserId { get; set; }
        public decimal FooterSumApproved, TotalDueAmount, TotalPartialAmount, TotalPrincipalamount, TotalremainingInterest, TotalOverDueAmount, TotalOriginalDueAMount, totalBalanceDueAMount;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            BindLoanEntryData();
        }
        private void GetUserId()
        {
           // HttpCookie myCookie = Request.Cookies["UserId"];
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                this.UserId = Convert.ToInt32(Session["UserId"]);
            }
        }
        public void BindLoanEntryData()
        {
            GetUserId();
            using (TermLoanService cs = new TermLoanService())
            {
                //HttpCookie UserStoreCookie = Request.Cookies["UserStoreId"];
                int shopstoreid = Convert.ToInt32(Session["UserStoreId"]);
                List<Model.CustomerTermLoan> lst = new List<Model.CustomerTermLoan>(); //= cs.CustomerReportGridDatabyStoreId(shopstoreid);

                if (ddlReportType.SelectedValue.ToString() != "0")
                {
                    string status = ddlReportType.SelectedValue.ToString().ToLower();
                    if (status != "over due")
                    {
                        //if (ddlReportType.SelectedValue == "Due") status = "Open";
                        if (ddlReportType.SelectedValue != "Due")
                            lst = cs.CustomerReportGridDatabyStoreIdandType(shopstoreid, status);//.Where(p => p.LoanStatus.ToLower() == status).ToList();
                        else
                            lst = cs.CustomerReportGridDatabyStoreId(shopstoreid).Where(p => p.LoanStatus == "Open" || p.LoanStatus == "Payment in Process").ToList();
                        if (ddlReportType.SelectedValue.ToString() == "Denied")
                        {
                            lst = lst.GroupBy(x => x.CustomerId).Select(g => g.Last()).ToList();
                        }

                        if (txtFromDate.Text != string.Empty)
                        {
                            if (ddlReportType.SelectedValue.ToString() == "Open" || ddlReportType.SelectedValue.ToString() == "Denied")
                            {
                                lst = lst.Where(p => Convert.ToDateTime(p.CreatedDate).Date >= Convert.ToDateTime(txtFromDate.Text.ToLower()).Date && Convert.ToDateTime(p.CreatedDate).Date <= Convert.ToDateTime(txtToDate.Text.ToLower()).Date).ToList();
                            }
                            else if (ddlReportType.SelectedValue.ToString() == "Due")
                            {
                                //lst = lst.Where(p => Convert.ToDateTime(p.NextPayDate).Date >= Convert.ToDateTime(txtFromDate.Text.ToLower()).Date && Convert.ToDateTime(p.NextPayDate).Date <= Convert.ToDateTime(txtToDate.Text.ToLower()).Date && p.LoanStatus != "Close" && p.LoanStatus != "Cancelled").ToList();
                                lst = lst.Where(p => Convert.ToDateTime(p.DueDate).Date >= Convert.ToDateTime(txtFromDate.Text.ToLower()).Date && Convert.ToDateTime(p.DueDate).Date <= Convert.ToDateTime(txtToDate.Text.ToLower()).Date && p.LoanStatus == "Open").ToList();
                            }
                            else if (ddlReportType.SelectedValue.ToString() == "Close")
                            {
                                lst = lst.Where(p => Convert.ToDateTime(p.UpdatedDate).Date >= Convert.ToDateTime(txtFromDate.Text.ToLower()).Date && Convert.ToDateTime(p.UpdatedDate).Date <= Convert.ToDateTime(txtToDate.Text.ToLower()).Date && p.LoanStatus == "Close").ToList();
                            }
                            else if (ddlReportType.SelectedValue.ToString() == "Cancelled")
                            {
                                lst = lst.Where(p => Convert.ToDateTime(p.UpdatedDate).Date >= Convert.ToDateTime(txtFromDate.Text.ToLower()).Date && Convert.ToDateTime(p.UpdatedDate).Date <= Convert.ToDateTime(txtToDate.Text.ToLower()).Date && p.LoanStatus == "Cancelled").ToList();
                            }
                            else if (ddlReportType.SelectedValue.ToString() == "Legal")
                            {
                                lst = lst.Where(p => Convert.ToDateTime(p.CreatedDate).Date >= Convert.ToDateTime(txtFromDate.Text.ToLower()).Date && Convert.ToDateTime(p.CreatedDate).Date <= Convert.ToDateTime(txtToDate.Text.ToLower()).Date && p.LoanStatus == "Legal").ToList();
                            }
                            else if (ddlReportType.SelectedValue.ToString() == "Consumer Proposal")
                            {
                                lst = lst.Where(p => Convert.ToDateTime(p.CreatedDate).Date >= Convert.ToDateTime(txtFromDate.Text.ToLower()).Date && Convert.ToDateTime(p.CreatedDate).Date <= Convert.ToDateTime(txtToDate.Text.ToLower()).Date && p.LoanStatus == "Consumer Proposal").ToList();
                            }
                            else
                            {
                                lst = lst.Where(p => Convert.ToDateTime(p.CreatedDate).Date >= Convert.ToDateTime(txtFromDate.Text.ToLower()).Date && Convert.ToDateTime(p.CreatedDate).Date <= Convert.ToDateTime(txtToDate.Text.ToLower()).Date && p.LoanStatus != "Close" && p.LoanStatus != "Cancelled").ToList();
                            }
                        }
                        using (TermLoanService css = new TermLoanService())
                        {
                            foreach (Model.CustomerTermLoan cl in lst)
                            {

                                List<Model.TermLoanPartialPayment> lstpartialpayment = css.TermLoanPartialPaymentsnewbyLoanId(cl.Id).ToList();

                                if (lstpartialpayment.Count > 0)
                                {
                                    cl.PartialPayment = "$" + lstpartialpayment.Sum(p => p.PartialAmount).ToString();
                                    cl.InstallmentInterestAmount = lstpartialpayment.Sum(p => p.InstallmentInterestAmount);
                                    cl.InstallmentPrincipalAmount = lstpartialpayment.Sum(p => p.InstallmentPrincipalAmount);
                                }
                            }
                        }
                    }
                    else
                    {
                        DataSet ds = cs.CustomerReportGridOverLAbel(null, null, shopstoreid, 0, "Over Due");
                        dgvOverDue.DataSource = ds.Tables[0];
                        dgvOverDue.DataBind();
                        loanopengrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        loanoverduegrid.Style.Add(HtmlTextWriterStyle.Display, "");
                    }
                }
                if (ddlReportType.SelectedValue.ToString() != "0")
                {
                    if (ddlReportType.SelectedValue == "Open")
                    {
                        dgvLoan.DataSource = lst;
                        dgvLoan.DataBind();
                        loanopengrid.Style.Add(HtmlTextWriterStyle.Display, "");
                        lblHeader.Text = "Loan Open";
                        loanoverduegrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        loanclosegrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        loanLegalgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        loanConsumerProposalgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanclosegrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loandeniedgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loaninprocessgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loansentforcollectiongrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanduegrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanConsumerProposalgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanBankruptgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loandeptgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanLegalgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                    }
                    if (ddlReportType.SelectedValue == "Denied")
                    {
                        //dgvLoanDenied.DataSource = lst;
                        //dgvLoanDenied.DataBind();
                        //loanopengrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanoverduegrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanclosegrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loandeniedgrid.Style.Add(HtmlTextWriterStyle.Display, "");
                        //loaninprocessgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loansentforcollectiongrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanduegrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanConsumerProposalgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanBankruptgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loandeptgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanLegalgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                    }
                    if (ddlReportType.SelectedValue == "Close")
                    {
                        dgvLoanClose.DataSource = lst;
                        dgvLoanClose.DataBind();
                        loanopengrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        loanoverduegrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        loanLegalgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        loanclosegrid.Style.Add(HtmlTextWriterStyle.Display, "");
                        loanConsumerProposalgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                    }
                    if (ddlReportType.SelectedValue == "Over Due")
                    {
                        //dgvLoanOverDue.DataSource = lst;
                        //dgvLoanOverDue.DataBind();
                        //loanopengrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanoverduegrid.Style.Add(HtmlTextWriterStyle.Display, "");
                        //loanclosegrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loandeniedgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loaninprocessgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loansentforcollectiongrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanduegrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanConsumerProposalgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanBankruptgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loandeptgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanLegalgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                    }
                    if (ddlReportType.SelectedValue == "Payment in Process")
                    {
                        dgvLoan.DataSource = lst;
                        dgvLoan.DataBind();
                        loanopengrid.Style.Add(HtmlTextWriterStyle.Display, "");
                        lblHeader.Text = "Loan Payment in Process";
                        loanoverduegrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        loanclosegrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        loanLegalgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        loanConsumerProposalgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanclosegrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loandeniedgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loaninprocessgrid.Style.Add(HtmlTextWriterStyle.Display, "");
                        //loansentforcollectiongrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanduegrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanConsumerProposalgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanBankruptgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loandeptgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanLegalgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                    }
                    if (ddlReportType.SelectedValue == "Sent for Collection")
                    {
                        //dgvLoanSentforCollection.DataSource = lst;
                        //dgvLoanSentforCollection.DataBind();
                        //loanopengrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanoverduegrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanclosegrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loandeniedgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loaninprocessgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanduegrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loansentforcollectiongrid.Style.Add(HtmlTextWriterStyle.Display, "");
                        //loanConsumerProposalgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanBankruptgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loandeptgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanLegalgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                    }
                    if (ddlReportType.SelectedValue == "Due")
                    {
                        dgvLoan.DataSource = lst;
                        dgvLoan.DataBind();
                        loanopengrid.Style.Add(HtmlTextWriterStyle.Display, "");
                        lblHeader.Text = "Loan Due";
                        loanoverduegrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        loanclosegrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        loanLegalgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        loanConsumerProposalgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanoverduegrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanclosegrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loandeniedgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loaninprocessgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loansentforcollectiongrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanConsumerProposalgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanBankruptgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loandeptgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanLegalgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                    }
                    if (ddlReportType.SelectedValue == "Consumer Proposal")
                    {
                        dgvConsumerProposal.DataSource = lst;
                        dgvConsumerProposal.DataBind();
                        loanopengrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        loanoverduegrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        loanLegalgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        loanclosegrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        loanConsumerProposalgrid.Style.Add(HtmlTextWriterStyle.Display, "");
                    }
                    if (ddlReportType.SelectedValue == "Bankrupt")
                    {
                        //dgvLoanBankrupt.DataSource = lst;
                        //dgvLoanBankrupt.DataBind();
                        //loanopengrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanduegrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanoverduegrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanclosegrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loandeniedgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loaninprocessgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loansentforcollectiongrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanConsumerProposalgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanBankruptgrid.Style.Add(HtmlTextWriterStyle.Display, "");
                        //loandeptgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanLegalgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                    }
                    if (ddlReportType.SelectedValue == "DEPT Management")
                    {
                        //dgvdeptmgmt.DataSource = lst;
                        //dgvdeptmgmt.DataBind();
                        //loanopengrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanduegrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanoverduegrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanclosegrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loandeniedgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loaninprocessgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loansentforcollectiongrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanConsumerProposalgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanBankruptgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loandeptgrid.Style.Add(HtmlTextWriterStyle.Display, "");
                        //loanLegalgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                    }
                    if (ddlReportType.SelectedValue == "Legal")
                    {
                        dgvLegal.DataSource = lst;
                        dgvLegal.DataBind();
                        loanopengrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        loanoverduegrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        loanclosegrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        loanConsumerProposalgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanopengrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanduegrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanoverduegrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanclosegrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loandeniedgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loaninprocessgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loansentforcollectiongrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanConsumerProposalgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loanBankruptgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        //loandeptgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        loanLegalgrid.Style.Add(HtmlTextWriterStyle.Display, "");
                    }

                }
                else
                {
                    loanopengrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                    loanoverduegrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                    loanConsumerProposalgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                    //    loanclosegrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                    //    loandeniedgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                    //    loaninprocessgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                    //    loansentforcollectiongrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                    //    loanduegrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                    //    loanConsumerProposalgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                    //    loanBankruptgrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                    //    loandeptgrid.Style.Add(HtmlTextWriterStyle.Display, "none");

                }
                //mvView.ActiveViewIndex = 2;
            }

        }
        protected void dgvLoanClose_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvLoanClose.PageIndex = e.NewPageIndex;
            BindLoanEntryData();
        }
        protected void dgvLoanClose_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Model.CustomerTermLoan item = (Model.CustomerTermLoan)e.Row.DataItem;
                //FooterSumApproved += item.LoanAmountApproved;
                TotalDueAmount += item.DueAmount;
                FooterSumApproved += item.LoanAmountApproved;
                TotalOverDueAmount += Convert.ToDecimal(item.DueAmount + (item.NSFCharge == null ? 0 : item.NSFCharge) + (item.LateInterestCharge == null ? 0 : item.LateInterestCharge));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl = (Label)e.Row.FindControl("lblTotalDueAmount");
                lbl.Text = TotalDueAmount.ToString();
                Label lblTotalOverDueLoanAmount = (Label)e.Row.FindControl("lblTotalOverDueLoanAmount");
                lblTotalOverDueLoanAmount.Text = TotalOverDueAmount.ToString();
                Label lblTotalApprovedAmount = (Label)e.Row.FindControl("lblTotalApprovedAmount");
                lblTotalApprovedAmount.Text = FooterSumApproved.ToString();

            }
        }
        protected void dgvLoan_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                int LoanId = Convert.ToInt32(e.CommandArgument);

                TermLoanService cc = new TermLoanService();
                Model.CustomerTermLoan objtloan = cc.CustomerTermLoansbyId(LoanId).FirstOrDefault();
                //Model.CustomerLoan objtloan = objcc.FirstOrDefault();
                FillDropdownStatus(objtloan.LoanStatus);
                hdnCustomerId.Value = objtloan.CustomerId.ToString();
                hdnCurrentstatus.Value = objtloan.LoanStatus;
                using (CustomerService cs = new CustomerService())
                {
                    CustomerMaster cm = cs.CustomerMastersById(objtloan.CustomerId).FirstOrDefault();
                    cm.ProvinceName = GetProvince(Convert.ToInt32(cm.Province));
                    rptCustomer.DataSource = cs.CustomerMastersById(objtloan.CustomerId);
                    rptCustomer.DataBind();
                }
                var a = cc.CustomerTermLoansbyId(LoanId);
                using (CompanyService cms = new CompanyService())
                {
                    Model.CompanyStore CompanyStores = cms.CompanyStores.Where(p => p.Id == objtloan.ShopStoreId).FirstOrDefault();
                    a.FirstOrDefault().StoreAddress = CompanyStores.Address;
                }
                rptLoan.DataSource = a;
                rptLoan.DataBind();
                hdnId.Value = e.CommandArgument.ToString();
                hdnInstallmentamount.Value = objtloan.InstallmentAmount.ToString();
                mvView.ActiveViewIndex = 1;
                rbtnlstLoanType.Items[1].Enabled = true;
                if (hdnCurrentstatus.Value.ToLower() == "over due")
                {
                    rbtnlstLoanType.Items[1].Enabled = false;
                    DataSet ds = cc.CustomerReportGridOverLAbel(null, null, objtloan.ShopStoreId, LoanId, "Over Due");
                    hdnTotalLateInstallmentwihtAmount.Value = Math.Round((Convert.ToDecimal(ds.Tables[0].Rows[0]["InstallmentAmount"]) * (Convert.ToInt32(ds.Tables[0].Rows[0]["DueInstallments"]) == 0 ? 1 : Convert.ToInt32(ds.Tables[0].Rows[0]["DueInstallments"])) + Convert.ToDecimal(ds.Tables[0].Rows[0]["NSFCharge"]) + Convert.ToDecimal(ds.Tables[0].Rows[0]["TillDateInterest"])), 2).ToString();
                    hdnInstallmentamount.Value = Math.Round((Convert.ToDecimal(ds.Tables[0].Rows[0]["InstallmentAmount"]) * (Convert.ToInt32(ds.Tables[0].Rows[0]["DueInstallments"]) == 0 ? 1 : Convert.ToInt32(ds.Tables[0].Rows[0]["DueInstallments"]))), 2).ToString();
                    ds = null;
                }
            }
            else if (e.CommandName == "CancelConsumer")
            {
                string[] arg = e.CommandArgument.ToString().Split(',');
                int LoanId = Convert.ToInt32(arg[1]);
                int Index = Convert.ToInt32(arg[0]);
                string val = ((TextBox)dgvConsumerProposal.Rows[Index].FindControl("txtModeofPayment")).Text.ToString();
                TermLoanService cc = new TermLoanService();
                List<Model.CustomerTermLoan> objcc = cc.CustomerTermLoansbyId(LoanId).ToList();
                if (objcc.FirstOrDefault() != null)
                {
                    Model.CustomerTermLoan ln = objcc.FirstOrDefault();
                    ln.LoanStatus = "Cancelled";
                    ln.ModeofPayment = val;
                    ln.UpdatedDate = ConvertEasternTime(DateTime.Now);
                    cc.CustomerTermLoan_InsertOrUpdate(ln);
                }
                ResetForm();
            }
        }
        protected void dgvLoan_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvLoan.PageIndex = e.NewPageIndex;
            BindLoanEntryData();
        }
        protected void dgvLoan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Model.CustomerTermLoan item = (Model.CustomerTermLoan)e.Row.DataItem;
                Label lblTotalBalanceDue = (Label)e.Row.FindControl("lblTotalRemainingDueAmount");
                Label lblTotalRemainingPrincipalAmount = (Label)e.Row.FindControl("lblTotalRemainingPrincipalAmount");
                Label lblTotalRemainingInterestAmount = (Label)e.Row.FindControl("lblTotalRemainingInterestAmount");
                lblTotalBalanceDue.Text = "$" + item.DueAmount.ToString();
                lblTotalRemainingPrincipalAmount.Text = "$" + (item.LoanAmountApproved + item.AdminFee).ToString();
                lblTotalRemainingInterestAmount.Text = "$" + item.InterestCharge.ToString();
                FooterSumApproved += item.LoanAmountApproved + item.AdminFee;
                TotalDueAmount += item.DueAmount;
                TotalPartialAmount += string.IsNullOrEmpty(item.PartialPayment) ? 0 : Convert.ToDecimal(item.PartialPayment.Replace("$", ""));
                //TotalPrincipalamount += TotalDueAmount - item.InstallmentPrincipalAmount;
                if (ConvertEasternTime(DateTime.Now).Date > item.DueDate.Date)
                {
                    using (CompanyService cmp = new CompanyService())
                    {
                        CompanyStore CompanyStores = cmp.CompanyStoresbyId(item.ShopStoreId);
                        int days = (ConvertEasternTime(DateTime.Now).Date - item.DueDate.Date).Days;
                        double perdaycharge = (((double)item.DueAmount) * ((double)(CompanyStores.TermLateInterestRate / 100))) / 365;
                        double totalcharge = days * perdaycharge;
                        //lblOverDueLAteInterestCharge.Text = Convert.ToDecimal(totalcharge).ToString();
                        decimal finalval = Convert.ToDecimal(item.DueAmount) + Convert.ToDecimal(totalcharge) + Convert.ToDecimal((item.NSFCharge == null ? 0 : item.NSFCharge));
                        TotalDueAmount += Math.Round(Convert.ToDecimal(totalcharge) + Convert.ToDecimal((item.NSFCharge == null ? 0 : item.NSFCharge)), 2); TotalDueAmount += finalval;
                        decimal partialamount = string.IsNullOrEmpty(item.PartialPayment) ? 0 : Convert.ToDecimal(item.PartialPayment.Replace("$", ""));
                        lblTotalBalanceDue.Text = "$" + Math.Round(finalval - partialamount, 2).ToString();
                        lblTotalRemainingPrincipalAmount.Text = "$" + (Convert.ToDecimal(lblTotalBalanceDue.Text.Replace("$", "")) - item.InstallmentPrincipalAmount).ToString();
                        lblTotalRemainingInterestAmount.Text = "$" + (Convert.ToDecimal(item.InterestCharge) - item.InstallmentInterestAmount).ToString();
                        TotalPrincipalamount += Convert.ToDecimal(lblTotalRemainingPrincipalAmount.Text.Replace("$", ""));
                        TotalremainingInterest += Convert.ToDecimal(lblTotalRemainingInterestAmount.Text.Replace("$", ""));
                        //totalBalanceDueAMount += Math.Round(Convert.ToDecimal(totalcharge) + Convert.ToDecimal((item.NSFCharge == null ? 0 : item.NSFCharge)), 2) - partialamount;
                    }
                }
                else
                {
                    decimal partialamount = string.IsNullOrEmpty(item.PartialPayment) ? 0 : Convert.ToDecimal(item.PartialPayment.Replace("$", ""));
                    lblTotalBalanceDue.Text = "$" + Math.Round(Convert.ToDecimal(item.DueAmount) - partialamount, 2).ToString();
                    if (item.InstallmentPrincipalAmount > 0)
                    {
                        lblTotalRemainingPrincipalAmount.Text = "$" + (Convert.ToDecimal(item.LoanAmountApproved + item.AdminFee) - item.InstallmentPrincipalAmount).ToString();
                        lblTotalRemainingInterestAmount.Text = "$" + (Convert.ToDecimal(item.InterestCharge) - item.InstallmentInterestAmount).ToString();
                    }
                    TotalPrincipalamount += Convert.ToDecimal(lblTotalRemainingPrincipalAmount.Text.Replace("$", ""));
                    TotalremainingInterest += Convert.ToDecimal(lblTotalRemainingInterestAmount.Text.Replace("$", ""));
                    //totalBalanceDueAMount += Math.Round(Convert.ToDecimal(item.DueAmount) + Convert.ToDecimal((item.NSFCharge == null ? 0 : item.NSFCharge)), 2) - partialamount;
                }


            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl = (Label)e.Row.FindControl("lblTotalApproved");
                lbl.Text = FooterSumApproved.ToString();
                Label lblTotalDue = (Label)e.Row.FindControl("lblTotalDueAmount");
                lblTotalDue.Text = TotalDueAmount.ToString();
                Label lblTotalRemainingPrincipalAmountFooter = (Label)e.Row.FindControl("lblTotalRemainingPrincipalAmountFooter");
                lblTotalRemainingPrincipalAmountFooter.Text = TotalPrincipalamount.ToString();
                Label lblTotalRemainingInterestAmountFooter = (Label)e.Row.FindControl("lblTotalRemainingInterestAmountFooter");
                lblTotalRemainingInterestAmountFooter.Text = TotalremainingInterest.ToString();
                Label lblTotalBalanceDue = (Label)e.Row.FindControl("lblTotalBalanceDue");
                lblTotalBalanceDue.Text = (Math.Round(TotalDueAmount, 2) - TotalPartialAmount).ToString();
            }
        }
        protected void rptLoan_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                using (TermLoanService cc = new TermLoanService())
                {
                    Model.CustomerTermLoan objloan = (Model.CustomerTermLoan)e.Item.DataItem;
                    Label lblProcessPArtialAmountPaid = (Label)e.Item.FindControl("lblProcessPArtialAmountPaid");
                    Label lblProcessTotalRemainingDueAmount = (Label)e.Item.FindControl("lblProcessTotalRemainingDueAmount");
                    Label lblPendingDuemount = (Label)e.Item.FindControl("lblPendingDuemount");
                    lblProcessPArtialAmountPaid.Text = "$0.00";
                    lblPendingDuemount.Text = "--";
                    List<Model.TermLoanPartialPayment> lstpartialpayment = cc.TermLoanPartialPaymentsnewbyLoanId(objloan.Id).ToList();
                    lblProcessTotalRemainingDueAmount.Text = "<b>$" + (objloan.DueAmount - lstpartialpayment.Sum(p => p.PartialAmount)).ToString() + "</b>";
                    if (objloan.LoanStatus == "Over Due")
                    {
                        DataSet ds = cc.CustomerReportGridOverLAbel(null, null, objloan.ShopStoreId, Convert.ToInt32(objloan.Id), "Over Due");
                        lblProcessTotalRemainingDueAmount.Text = "$" + Convert.ToDecimal(ds.Tables[0].Rows[0]["TotalDuePending"]).ToString();
                        lblPendingDuemount.Text = "$" + Math.Round((Convert.ToDecimal(ds.Tables[0].Rows[0]["InstallmentAmount"]) * (Convert.ToInt32(ds.Tables[0].Rows[0]["DueInstallments"]) == 0 ? 1 : Convert.ToInt32(ds.Tables[0].Rows[0]["DueInstallments"])) + Convert.ToDecimal(ds.Tables[0].Rows[0]["NSFCharge"]) + Convert.ToDecimal(ds.Tables[0].Rows[0]["TillDateInterest"])), 2).ToString();
                        ds = null;
                    }
                    if (lstpartialpayment.Count > 0)
                    {
                        lblProcessPArtialAmountPaid.Text = "";
                        foreach (Model.TermLoanPartialPayment obj in lstpartialpayment)
                        {
                            lblProcessPArtialAmountPaid.Text += "Partial Payment on " + obj.CreatedDate.ToString("MM/dd/yyyy") + ":  <b>$" + obj.PartialAmount.ToString() + "</b><br/>";
                        }
                        //lblOverDuePArtialAmountPaid.Text = "$" + lstpartialpayment.Sum(p => p.PartialAmount).ToString();
                    }
                }
            }
        }
        public void FillDropdownStatus(string status)
        {
            int Statusid = 1;
            switch (status)
            {
                case "Open":
                    Statusid = 1;
                    break;
                case "Payment in Process":
                    Statusid = 2;
                    break;
                case "Due":
                    Statusid = 3;
                    break;
                case "Over Due":
                    Statusid = 4;
                    break;
                case "Sent for Collection":
                    Statusid = 5;
                    break;
                case "DEPT Management":
                    Statusid = 6;
                    break;
                case "Consumer Proposal":
                    Statusid = 7;
                    break;
                case "Bankrupt":
                    Statusid = 8;
                    break;
                case "Legal":
                    Statusid = 9;
                    break;
                case "Close":
                    Statusid = 10;
                    break;
                default:
                    Statusid = 1;
                    break;
            }
            using (CompanyService cs = new CompanyService())
            {
                List<LoanStatus> lst = new List<LoanStatus>();
                if (Statusid != 4)
                {
                    lst = cs.LoanStatus.Where(p => p.StatusId > Statusid && p.Status != "Due").ToList();
                }
                else
                {
                    lst = cs.LoanStatus.Where(p => p.Status != "Due").ToList();
                }
                ddlLoanStatus.DataSource = lst;
                ddlLoanStatus.DataTextField = "Status";
                ddlLoanStatus.DataTextField = "Status";
                ddlLoanStatus.DataBind();
                ddlLoanStatus.Items.Insert(0, new ListItem { Text = "--Select Status--", Value = "0", Selected = true });
            }
        }
        protected void btnOpenLoanSubmit_Click(object sender, EventArgs e)
        {
            GetUserId();
            using (TermLoanService cs = new TermLoanService())
            {
                CashLoanShop.Model.CustomerTermLoan cm = cs.CustomerTermLoansbyId(Convert.ToInt32(hdnId.Value)).FirstOrDefault();
                if (ddlLoanStatus.SelectedValue != "0")
                {
                    if (cm != null)
                    {
                        List<Model.TermLoanPartialPayment> lstpartialpayment = cs.TermLoanPartialPaymentsnew.Where(p => p.LoanId == cm.Id).ToList();
                        cm.LastStatus = cm.LoanStatus;
                        cm.LoanStatus = ddlLoanStatus.SelectedValue.ToString();
                        cm.UpdatedDate = ConvertEasternTime(DateTime.Now);
                        cm.ModeofPayment = txtModeofPayment.Text;
                        cm.DiscountAmount = string.IsNullOrEmpty(txtopenDiscount.Text) ? Convert.ToDecimal(0) : Convert.ToDecimal(txtopenDiscount.Text);
                        cm.RemainingAmount = cm.DueAmount - lstpartialpayment.Sum(p => p.PartialAmount) - (string.IsNullOrEmpty(txtopenDiscount.Text) ? Convert.ToDecimal(0) : Convert.ToDecimal(txtopenDiscount.Text)) - (txtPartialAmount.Text == string.Empty ? Convert.ToDecimal(0) : Convert.ToDecimal(txtPartialAmount.Text));
                        if (cm.LoanStatus == "Over Due")
                            cm.LoanOverDueDate = Convert.ToDateTime(txtOverDueDate.Text);
                        if (cm.RemainingAmount > 0 && cm.LoanStatus == "Close")
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "runprintclosefromopencbvnew", "resetback(); alert('You can not Close the Loan untill full Payment Received.After giving your stated discount still outstanding amount is: " + cm.RemainingAmount.ToString() + "');", true);
                            return;
                        }
                        cs.CustomerTermLoan_InsertOrUpdate(cm);
                        ResetForm();
                        if (cm.LoanStatus == "Close")
                        {
                            //if (cm.RemainingAmount > 0)
                            //{
                            //    TermLoanPartialPayment pp = new TermLoanPartialPayment();
                            //    pp.Createdby = this.UserId;
                            //    pp.CreatedDate = ConvertEasternTime(DateTime.Now);
                            //    pp.DiscountAmount = string.IsNullOrEmpty(txtopenDiscount.Text) ? Convert.ToDecimal(0) : Convert.ToDecimal(txtopenDiscount.Text);
                            //    pp.PartialAmount = Convert.ToDecimal(cm.RemainingAmount);
                            //    pp.IntrestCharge = 0;
                            //    pp.LoanId = Convert.ToInt32(hdnId.Value);
                            //    pp.PartialPaymentMethod = txtModeofPayment.Text;
                            //    pp.InstallmentInterestAmount = Math.Round(Convert.ToDecimal(((double)pp.PartialAmount / 1.59) * 0.36), 2);//Math.Round((pp.PartialAmount * percentage) / 100, 2);
                            //    pp.InstallmentPrincipalAmount = Math.Round(Convert.ToDecimal(((double)pp.PartialAmount / 1.59) * 1.23), 2);
                            //    pp.Balance = Convert.ToDecimal(0);
                            //    cs.TermLoanPartialPayment_InsertOrUpdate(pp);
                            //}
                            cs.UpdateTermLoanSChedule(cm.Id);
                            string Url = "/TermLoanCloseReceipt.aspx?Id=" + cm.Id.ToString();
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "runprintclosefromopencbv", " window.open('" + Url + "', '_blank', 'width=800,height=550,location=no,left=200px');", true);
                        }
                    }
                    cm = null;
                }
                else
                {

                    TermLoanPartialPayment pp = new TermLoanPartialPayment();
                    pp.Createdby = this.UserId;
                    pp.CreatedDate = ConvertEasternTime(DateTime.Now).Date;
                    pp.DiscountAmount = string.IsNullOrEmpty(txtopenDiscount.Text) ? Convert.ToDecimal(0) : Convert.ToDecimal(txtopenDiscount.Text);
                    pp.PartialAmount = Convert.ToDecimal(txtPartialAmount.Text == string.Empty ? hdnInstallmentamount.Value : txtPartialAmount.Text);
                    pp.IntrestCharge = 0;
                    if (hdnCurrentstatus.Value == "Over Due")
                    {
                        if (Convert.ToDecimal(pp.DiscountAmount) + Convert.ToDecimal(txtPartialAmount.Text) == Convert.ToDecimal(hdnTotalLateInstallmentwihtAmount.Value))
                        {
                            pp.PartialAmount = Convert.ToDecimal(hdnInstallmentamount.Value);
                            pp.IntrestCharge = pp.PartialAmount - pp.DiscountAmount;
                            // update loan status back to previous stage 
                            cm.LoanStatus = cm.LastStatus;  // back loan to rpevious stage 
                            cs.CustomerTermLoan_InsertOrUpdate(cm);
                        }
                    }
                    pp.LoanId = Convert.ToInt32(hdnId.Value);
                    pp.PartialPaymentMethod = txtModeofPayment.Text;
                    decimal percentage = Math.Round(((cm.InterestCharge * 100) / cm.LoanAmountApproved), 2);

                    //
                    List<Model.TermLoanPartialPayment> lstpartialpayment = cs.TermLoanPartialPaymentsnew.Where(p => p.LoanId == cm.Id).ToList();
                    TermLoanSchedule ts = cs.TermLoanSchedules.Where(p => p.LoanId == pp.LoanId && p.IsPaid == false).OrderBy(p => p.InstallmentNo).FirstOrDefault();
                    if (ts != null)
                    {
                        ts.IsPaid = true;
                        cs.TermLoanSchedule_InsertOrUpdate(ts);
                        //pp.InstallmentInterestAmount = ts.Interest;
                        //pp.InstallmentPrincipalAmount = ts.Principal;
                        //pp.Balance = ts.Balance;
                        if (Convert.ToDecimal(hdnInstallmentamount.Value) == Convert.ToDecimal(txtPartialAmount.Text)) // if the Installment Amoint is paid and its exist in SChedule then TAke the detail from there 
                        {
                            pp.InstallmentInterestAmount = Math.Round(ts.Interest, 2);//Math.Round(Convert.ToDecimal(((double)pp.PartialAmount / 1.59) * 0.36), 2);//Math.Round((pp.PartialAmount * percentage) / 100, 2);
                            pp.InstallmentPrincipalAmount = Math.Round(ts.Principal, 2);// Math.Round(Convert.ToDecimal(((double)pp.PartialAmount / 1.59) * 1.23), 2);
                        }
                        else // schedule is present but the Amount is not installment amount or Higher than that then calculate manually 
                        {
                            if (cm.CreatedDate < new DateTime(2018, 12, 21)) //calculate based on 26% 
                            {
                                pp.InstallmentInterestAmount = Math.Round(Convert.ToDecimal(((double)pp.PartialAmount / 1.59) * 0.36), 2);//Math.Round((pp.PartialAmount * percentage) / 100, 2);
                                pp.InstallmentPrincipalAmount = Math.Round(Convert.ToDecimal(((double)pp.PartialAmount / 1.59) * 1.23), 2);
                            }
                            else //based on new calculation 20%
                            {
                                pp.InstallmentInterestAmount = Math.Round(Convert.ToDecimal(((double)pp.PartialAmount / 1.59) * 0.28), 2);//Math.Round((pp.PartialAmount * percentage) / 100, 2);
                                pp.InstallmentPrincipalAmount = Math.Round(Convert.ToDecimal(((double)pp.PartialAmount / 1.59) * 1.23), 2);
                            }
                        }
                        pp.Balance = (cm.AdminFee + cm.LoanAmountApproved) - (lstpartialpayment.Sum(p => p.InstallmentPrincipalAmount) + pp.InstallmentPrincipalAmount);
                    }
                    else
                    {
                        pp.InstallmentInterestAmount = Math.Round(Convert.ToDecimal(((double)pp.PartialAmount / 1.59) * 0.36), 2);//Math.Round((pp.PartialAmount * percentage) / 100, 2); //(lstpartialpayment.Sum(p => p.DiscountAmount) + pp.DiscountAmount)
                        pp.InstallmentPrincipalAmount = Math.Round(Convert.ToDecimal(((double)pp.PartialAmount / 1.59) * 1.23), 2);
                        pp.Balance = (cm.AdminFee + cm.LoanAmountApproved) - (lstpartialpayment.Sum(p => p.InstallmentPrincipalAmount) + pp.InstallmentPrincipalAmount);
                    }
                    //if (lstpartialpayment.Count > 0)
                    //{
                    //    pp.DueAmount = pp.DueAmount - lstpartialpayment.Sum(p => p.PartialAmount);
                    //}
                    cs.TermLoanPartialPayment_InsertOrUpdate(pp);

                    string Url = "/TermLoanPartialPaidReceipt.aspx?Id=" + cm.Id.ToString();
                    //If last payment is done then Update loan status to Close from Loan Open
                    lstpartialpayment = cs.TermLoanPartialPaymentsnew.Where(p => p.LoanId == cm.Id).ToList();
                    cm.RemainingAmount = cm.DueAmount - lstpartialpayment.Sum(p => p.PartialAmount) - lstpartialpayment.Sum(p => p.DiscountAmount);
                    if ((Math.Round(lstpartialpayment.Sum(p => p.PartialAmount), 1) + Math.Round(lstpartialpayment.Sum(p => p.DiscountAmount), 1)) == Math.Round(cm.DueAmount, 1))
                    {
                        cm.LoanStatus = "Close";
                        cm.UpdatedDate = ConvertEasternTime(DateTime.Now);
                        cm.RemainingAmount = 0;
                        cs.UpdateTermLoanSChedule(cm.Id);
                    }
                    cs.CustomerTermLoan_InsertOrUpdate(cm);

                    //end
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "runprintas12cv", " window.open('" + Url + "', '_blank', 'width=800,height=550,location=no,left=200px');", true);
                }
            }
            ResetForm();
        }

        protected void dgvOverDue_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                DataTable dt = (DataTable)dgvOverDue.DataSource;
                Label lblTotalDueAmount = (Label)e.Row.FindControl("lblTotalDueAmount");
                lblTotalDueAmount.Text = String.Format("{0:N}", dt.Select().Sum(p => Convert.ToDecimal(p["Origional Due Amount"]))).ToString();
                Label lblTotalDuePendingAmount = (Label)e.Row.FindControl("lblTotalDuePendingAmount");
                lblTotalDuePendingAmount.Text = String.Format("{0:N}", dt.Select().Sum(p => Convert.ToDecimal(p["TotalDuePending"]))).ToString();
                Label lblTotalRemainingPrincipalAmountFooter = (Label)e.Row.FindControl("lblTotalRemainingPrincipalAmountFooter");
                lblTotalRemainingPrincipalAmountFooter.Text = String.Format("{0:N}", dt.Select().Sum(p => Convert.ToDecimal(p["remainingPricipal"]))).ToString();
                Label lblTotalRemainingInterestAmountFooter = (Label)e.Row.FindControl("lblTotalRemainingInterestAmountFooter");
                lblTotalRemainingInterestAmountFooter.Text = String.Format("{0:N}", dt.Select().Sum(p => Convert.ToDecimal(p["RemainingInterest"]))).ToString();
                //Label lblTotalInterestFooter = (Label)e.Row.FindControl("lblTotalInterestFooter");
                //lblTotalInterestFooter.Text = String.Format("{0:N}", dt.Select().Sum(p => Convert.ToDecimal(p["NSFCharge"]) + Convert.ToDecimal(p["TillDateInterest"]))).ToString();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ResetForm();
        }
        public void ResetForm()
        {
            mvView.ActiveViewIndex = 0;
            ddlReportType.SelectedIndex = 0;
            txtFromDate.Text = "";
            txtToDate.Text = "";
            btnSubmit_Click(null, null);
            //Response.Redirect("~/ViewReport.aspx");
        }
        public DateTime ConvertEasternTime(DateTime date)
        {
            TimeZoneInfo timeZoneInfo;
            DateTime dateTime;
            //Set the time zone information to US Mountain Standard Time 
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            //Get date and time in US Mountain Standard Time 
            dateTime = TimeZoneInfo.ConvertTime(date, timeZoneInfo);
            //Print out the date and time
            //Console.WriteLine(dateTime.ToString("yyyy-MM-dd HH-mm-ss"));
            return dateTime;
        }
        public string GetProvince(int Id)
        {
            switch (Id)
            {
                case 1:
                    return "Alberta";

                case 2:
                    return "British Columbia";
                case 3:
                    return "Manitoba";
                case 4:
                    return "New Brunswick";
                case 5:
                    return "Newfoundland";
                case 6:
                    return "Nova Scotia";
                case 7:
                    return "Northwest Territories";
                case 8:
                    return "Ontario";
                case 9:
                    return "Prince Edward Island";
                case 10:
                    return "Quebec";
                case 11:
                    return "Saskatchewan";
                case 12:
                    return "Yukon";
                case 13:
                    return "Other";
                default:
                    return "Not Selected";

            }
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "TermLoanData.xls"));
            Response.ContentType = "application/vnd.ms-excel";
            //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            dgvLoan.AllowPaging = false;
            //Change the Header Row back to white color
            dgvLoan.HeaderRow.Style.Add("background-color", "#FFFFFF");
            dgvLoan.HeaderRow.Cells.RemoveAt(0);
            dgvLoan.HeaderRow.Cells.RemoveAt(11);

            dgvLoan.FooterRow.Cells.RemoveAt(0);
            dgvLoan.FooterRow.Cells.RemoveAt(11);
            //Applying stlye to gridview header cells
            for (int i = 0; i < dgvLoan.HeaderRow.Cells.Count; i++)
            {
                dgvLoan.HeaderRow.Cells[i].Style.Add("background-color", "#507CD1");
            }
            int j = 1;
            //This loop is used to apply stlye to cells based on particular row
            foreach (GridViewRow gvrow in dgvLoan.Rows)
            {
                gvrow.BackColor = Color.White;
                if (j <= dgvLoan.Rows.Count)
                {
                    gvrow.Cells.RemoveAt(0);
                    gvrow.Cells.RemoveAt(11);
                    if (j % 2 != 0)
                    {
                        for (int k = 0; k < gvrow.Cells.Count; k++)
                        {
                            gvrow.Cells[k].Style.Add("background-color", "#EFF3FB");
                        }
                    }
                }
                j++;
            }
            dgvLoan.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();

        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void dgvLoanSentforCollection_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvLegal.PageIndex = e.NewPageIndex;
            BindLoanEntryData();
        }

        protected void dgvLoanSentforCollection_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                Model.CustomerTermLoan item = (Model.CustomerTermLoan)e.Row.DataItem;

                FooterSumApproved += item.LoanAmountApproved;
                TotalOriginalDueAMount += item.DueAmount;
                TotalDueAmount += item.DueAmount;
                TotalPartialAmount += string.IsNullOrEmpty(item.PartialPayment) ? 0 : Convert.ToDecimal(item.PartialPayment.Replace("$", ""));
                TotalOverDueAmount += Convert.ToDecimal(item.DueAmount + (item.NSFCharge == null ? 0 : item.NSFCharge) + (item.LateInterestCharge == null ? 0 : item.LateInterestCharge));
                Label lblCollectionDueAmount = (Label)e.Row.FindControl("lblCollectionDueAmount");
                lblCollectionDueAmount.Text = "$" + item.DueAmount.ToString();
                Label lblTotalBalanceDue = (Label)e.Row.FindControl("lblTotalRemainingDueAmount");
                lblTotalBalanceDue.Text = "$" + item.DueAmount.ToString();
                if (ConvertEasternTime(DateTime.Now).Date > item.DueDate.Date)
                {
                    CompanyService cmp = new CompanyService();
                    Model.CompanyStore CompanyStores = cmp.CompanyStoresbyId(item.ShopStoreId);
                    int days = (ConvertEasternTime(DateTime.Now).Date - item.DueDate.Date).Days;
                    double perdaycharge = (((double)item.DueAmount) * ((double)(CompanyStores.InterestRate / 100))) / 365;
                    double totalcharge = days * perdaycharge;
                    //lblOverDueLAteInterestCharge.Text = Convert.ToDecimal(totalcharge).ToString();
                    decimal finalval = Convert.ToDecimal(item.DueAmount) + Convert.ToDecimal(totalcharge) + Convert.ToDecimal((item.NSFCharge == null ? 0 : item.NSFCharge));
                    lblCollectionDueAmount.Text = "$" + Math.Round(finalval, 2).ToString();
                    TotalDueAmount += Math.Round(Convert.ToDecimal(totalcharge) + Convert.ToDecimal((item.NSFCharge == null ? 0 : item.NSFCharge)), 2);

                    decimal partialamount = string.IsNullOrEmpty(item.PartialPayment) ? 0 : Convert.ToDecimal(item.PartialPayment.Replace("$", ""));
                    lblTotalBalanceDue.Text = "$" + Math.Round(finalval - partialamount, 2).ToString();
                    totalBalanceDueAMount += Math.Round(Convert.ToDecimal(totalcharge) + Convert.ToDecimal((item.NSFCharge == null ? 0 : item.NSFCharge)), 2) - partialamount;
                }
                else
                {
                    decimal partialamount = string.IsNullOrEmpty(item.PartialPayment) ? 0 : Convert.ToDecimal(item.PartialPayment.Replace("$", ""));
                    lblTotalBalanceDue.Text = "$" + Math.Round(Convert.ToDecimal(item.DueAmount) - partialamount, 2).ToString();
                    
                    //totalBalanceDueAMount += Math.Round(Convert.ToDecimal(item.DueAmount) + Convert.ToDecimal((item.NSFCharge == null ? 0 : item.NSFCharge)), 2) - partialamount;
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalApproved = (Label)e.Row.FindControl("lblTotalApproved");
                lblTotalApproved.Text = FooterSumApproved.ToString();


                Label lblTotalOverDueLoanAmount = (Label)e.Row.FindControl("lblTotalOverDueLoanAmount");
                lblTotalOverDueLoanAmount.Text = Math.Round(TotalDueAmount, 2).ToString();
                Label lblTotalPartialAmountReceived = (Label)e.Row.FindControl("lblTotalPartialAmountReceived");
                lblTotalPartialAmountReceived.Text = TotalPartialAmount.ToString();
                Label lblTotalBalanceDue = (Label)e.Row.FindControl("lblTotalBalanceDue");
                lblTotalBalanceDue.Text = (Math.Round(TotalDueAmount, 2) - TotalPartialAmount).ToString();

                Label lblTotalCollectionDueAmount = (Label)e.Row.FindControl("lblTotalCollectionDueAmount");
                lblTotalCollectionDueAmount.Text = Math.Round(TotalOriginalDueAMount, 2).ToString();
            }
        }
        protected void btnExportLegal_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "TermLoanLegalExportData.xls"));
            Response.ContentType = "application/vnd.ms-excel";
            //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            dgvLegal.AllowPaging = false;
            //Change the Header Row back to white color
            dgvLegal.HeaderRow.Style.Add("background-color", "#FFFFFF");
            dgvLegal.HeaderRow.Cells.RemoveAt(0);

            dgvLegal.FooterRow.Cells.RemoveAt(0);
            //Applying stlye to gridview header cells
            for (int i = 0; i < dgvLegal.HeaderRow.Cells.Count; i++)
            {
                dgvLegal.HeaderRow.Cells[i].Style.Add("background-color", "#507CD1");
            }
            int j = 1;
            //This loop is used to apply stlye to cells based on particular row
            foreach (GridViewRow gvrow in dgvLegal.Rows)
            {
                gvrow.BackColor = Color.White;
                if (j <= dgvLegal.Rows.Count)
                {
                    gvrow.Cells.RemoveAt(0);
                    
                    if (j % 2 != 0)
                    {
                        for (int k = 0; k < gvrow.Cells.Count; k++)
                        {
                            gvrow.Cells[k].Style.Add("background-color", "#EFF3FB");
                        }
                    }
                }
                j++;
            }
            dgvLegal.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();

        }


        protected void dgvConsumerProposal_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvConsumerProposal.PageIndex = e.NewPageIndex;
            BindLoanEntryData();
        }
        protected void dgvConsumerProposal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Model.CustomerTermLoan item = (Model.CustomerTermLoan)e.Row.DataItem;
                //FooterSumApproved += item.LoanAmountApproved;
                TotalOriginalDueAMount += item.DueAmount;
                totalBalanceDueAMount += item.DueAmount;

                TotalDueAmount += item.DueAmount;
                TotalPartialAmount += string.IsNullOrEmpty(item.PartialPayment) ? 0 : Convert.ToDecimal(item.PartialPayment.Replace("$", ""));
                TotalOverDueAmount += Convert.ToDecimal(item.DueAmount + (item.NSFCharge == null ? 0 : item.NSFCharge) + (item.LateInterestCharge == null ? 0 : item.LateInterestCharge));
                Label lbl = (Label)e.Row.FindControl("lblLateDueAmount");
                lbl.Text = "$" + item.DueAmount.ToString();
                Label lblTotalBalanceDue = (Label)e.Row.FindControl("lblTotalRemainingDueAmount");
                //lblTotalBalanceDue.Text = "$" + item.DueAmount.ToString();
                decimal partialamount = string.IsNullOrEmpty(item.PartialPayment) ? 0 : Convert.ToDecimal(item.PartialPayment.Replace("$", ""));
                lblTotalBalanceDue.Text = "$" + Math.Round(item.DueAmount - partialamount, 2).ToString();
                if (ConvertEasternTime(DateTime.Now).Date > item.DueDate.Date)
                {
                    CompanyService cmp = new CompanyService();
                    CompanyStore CompanyStores = cmp.CompanyStoresbyId(item.ShopStoreId);
                    int days = (ConvertEasternTime(DateTime.Now).Date - item.DueDate.Date).Days;
                    double perdaycharge = (((double)item.DueAmount) * ((double)(CompanyStores.InterestRate / 100))) / 365;
                    double totalcharge = days * perdaycharge;
                    //lblOverDueLAteInterestCharge.Text = Convert.ToDecimal(totalcharge).ToString();
                    decimal finalval = Convert.ToDecimal(item.DueAmount) + Convert.ToDecimal(totalcharge) + Convert.ToDecimal((item.NSFCharge == null ? 0 : item.NSFCharge));
                    lbl.Text = "$" + Math.Round(finalval, 2).ToString();
                    TotalDueAmount += Math.Round(Convert.ToDecimal(totalcharge) + Convert.ToDecimal((item.NSFCharge == null ? 0 : item.NSFCharge)), 2);
                    lblTotalBalanceDue.Text = "$" + Math.Round(finalval - partialamount, 2).ToString();
                    totalBalanceDueAMount += Math.Round(Convert.ToDecimal(totalcharge) + Convert.ToDecimal((item.NSFCharge == null ? 0 : item.NSFCharge)), 2) - partialamount;
                }

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl = (Label)e.Row.FindControl("lblTotalDueAmount");
                lbl.Text = Math.Round(TotalOriginalDueAMount, 2).ToString();
                Label lblTotalOverDueLoanAmount = (Label)e.Row.FindControl("lblTotalOverDueLoanAmount");
                lblTotalOverDueLoanAmount.Text = Math.Round(TotalDueAmount, 2).ToString();
                Label lblTotalPartialAmountReceived = (Label)e.Row.FindControl("lblTotalPartialAmountReceived");
                lblTotalPartialAmountReceived.Text = TotalPartialAmount.ToString();
                Label lblTotalBalanceDue = (Label)e.Row.FindControl("lblTotalBalanceDue");
                lblTotalBalanceDue.Text = (Math.Round(TotalDueAmount, 2) - TotalPartialAmount).ToString();
            }
        }
        protected void dgvConsumerProposal_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }
    }
}