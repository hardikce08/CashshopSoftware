using CashLoanShop.DataAccess;
using CashLoanShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CashLoanShop
{
    public partial class TermLoanPartialPaidReceipt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["Id"]))
                {
                    int CustomerLoanId = Convert.ToInt32(Request.QueryString["Id"]);
                    using (TermLoanService cc = new TermLoanService())
                    {
                        Model.CustomerTermLoan objcc = cc.CustomerTermLoansbyId(CustomerLoanId).FirstOrDefault();
                        if (objcc != null)
                        {
                            TermLoanPartialPayment objpp = cc.TermLoanPartialPaymentsnewbyLoanId(objcc.Id).ToList().OrderByDescending(t => t.Id).ToList().FirstOrDefault();
                            CustomerService cs = new CustomerService();
                            CustomerMaster cm = cs.CustomerMastersById(objcc.CustomerId).FirstOrDefault();
                            decimal Partialamountpaid = objpp.PartialAmount;

                            List<TermLoanPartialPayment> lstpaymetlst = cc.TermLoanPartialPaymentsnewbyLoanId(objcc.Id).ToList();
                            decimal TotalAmount = lstpaymetlst.Sum(p => p.PartialAmount);

                            lblCustomerName.Text = cm.FirstName + " " + cm.LastName;
                            lblDateTime.Text = Convert.ToDateTime(DateTime.Now).ToString("MM/dd/yyyy hh:mm:ss tt").Replace("-", "/");

                            lblReceiptNumber.Text = objcc.Id.ToString();
                            decimal loanprincipal = Math.Round(Convert.ToDecimal(objcc.LoanAmountApproved + objcc.AdminFee), 2);
                            lblLoanAmount.Text = "$" + loanprincipal.ToString();
                            lblCashpaid.Text = "$" + Partialamountpaid.ToString();
                            lblAdminFee.Text = "$" + objcc.AdminFee.ToString();
                            lblDueAmount.Text = "$" + objcc.DueAmount.ToString();
                            lblDueDate.Text = Convert.ToDateTime(objcc.DueDate).ToString("MM/dd/yyyy").Replace("-", "/");
                            lblLateInterestCharges.Text = "$" + (objcc.LateInterestCharge == null ? "0.00" : objcc.LateInterestCharge.ToString());
                            lblNSFCharges.Text = "$" + (objcc.NSFCharge == null ? "0.00" : objcc.NSFCharge.ToString());
                            lblTotalDueAmount.Text = "$" + (objcc.DueAmount + (objcc.LateInterestCharge == null ? 0 : objcc.LateInterestCharge) + (objcc.NSFCharge == null ? 0 : objcc.NSFCharge)).ToString();
                            //lblLastDueAmount.Text = "$" + (objcc.LastDueAmount == null ? objcc.DueAmount : objcc.LastDueAmount).ToString();
                            //lblLastDueAmount.Text = "$" + objpp.DueAmount.ToString();
                            lblBalanceDue.Text = "$" + (((objcc.DueAmount + (objcc.LateInterestCharge == null ? 0 : objcc.LateInterestCharge) + (objcc.NSFCharge == null ? 0 : objcc.NSFCharge)) - TotalAmount) - (objcc.DiscountAmount == null ? 0 : objcc.DiscountAmount)).ToString();
                            //lblNextPaymentDate.Text = Convert.ToDateTime(DateTime.Now.AddMonths(1)).ToString("MM/dd/yyyy hh:mm:ss tt").Replace("-", "/");
                            lblDiscount.Text = "$" + (objcc.DiscountAmount == null ? "0.00" : objcc.DiscountAmount.ToString());
                            //objcc.DueAmount = Convert.ToDecimal(lblBalanceDue.Text.Replace("$", ""));
                            //cc.CustomerTermLoan_InsertOrUpdate(objcc);


                            lblTotalPartialAmountPaid.Text = TotalAmount.ToString();
                            CompanyService cmp = new CompanyService();
                            Model.CompanyStore CompanyStores = cmp.CompanyStores.Where(p => p.Id == objcc.ShopStoreId).FirstOrDefault();
                            if (CompanyStores != null)
                            {
                                lblStoreInfo.Text = CompanyStores.Name + "<br/>" + CompanyStores.Address.Replace(",", "<br/>").Replace("$", " , ") + "<br/>" + CompanyStores.PhoneNo + "<br/>" + CompanyStores.Email;
                                //lblTransactionType.Text = CompanyStores.Businessname;
                            }

                        }
                    }
                }
            }
        }
    }
}