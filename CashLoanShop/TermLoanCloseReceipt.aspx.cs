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
    public partial class TermLoanCloseReceipt : System.Web.UI.Page
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
                            //LoanPartialPayment objpp = cc.LoanPartialPaymentsnew.Where(p => p.LoanId == objcc.Id).ToList().OrderByDescending(t => t.Id).ToList().FirstOrDefault();
                            using (CustomerService cs = new CustomerService())
                            {
                                CustomerMaster cm = cs.CustomerMastersById(objcc.CustomerId).FirstOrDefault();
                                lblCustomerName.Text = cm.FirstName + " " + cm.LastName;
                            }
                            using (CompanyService cmp = new CompanyService())
                            {
                                Model.CompanyStore CompanyStores = cmp.CompanyStoresbyId(objcc.ShopStoreId);
                                if (CompanyStores != null)
                                {
                                    lblStoreInfo.Text = CompanyStores.Name + "<br/>" + CompanyStores.Address.Replace(",", "<br/>").Replace("$", " , ") + "<br/>" + CompanyStores.Email;
                                    //lblTransactionType.Text = CompanyStores.Businessname;
                                }
                            }
                            //decimal Partialamountpaid = objpp.PartialAmount;
                           
                            lblDateTime.Text = Convert.ToDateTime(objcc.UpdatedDate).ToString("MM/dd/yyyy hh:mm:ss tt").Replace("-", "/");

                            lblReceiptNumber.Text = objcc.Id.ToString();
                            lblLoanAmount.Text = "$" + objcc.LoanAmountApproved.ToString();
                            lblLastDueAmount.Text = "$0.00";
                            lblAdminFee.Text = "$" + objcc.AdminFee.ToString();
                            lblDueAmount.Text = "$" + objcc.DueAmount.ToString();
                            lblDueDate.Text = Convert.ToDateTime(objcc.DueDate).ToString("MM/dd/yyyy").Replace("-", "/");
                            lblLateInterestCharges.Text = "$" + (objcc.LateInterestCharge == null ? "0.00" : objcc.LateInterestCharge.ToString());
                            lblNSFCharges.Text = "$" + (objcc.NSFCharge == null ? "0.00" : objcc.NSFCharge.ToString());
                            lblTotalDueAmount.Text = "$" + (objcc.DueAmount + (objcc.LateInterestCharge == null ? 0 : objcc.LateInterestCharge) + (objcc.NSFCharge == null ? 0 : objcc.NSFCharge)).ToString();
                            lblDiscount.Text = "$" + objcc.DiscountAmount.ToString();
                            lblCashpaid.Text = "$" + ((objcc.DueAmount + (objcc.LateInterestCharge == null ? 0 : objcc.LateInterestCharge) + (objcc.NSFCharge == null ? 0 : objcc.NSFCharge)) - objcc.DiscountAmount).ToString();

                            //objcc.RemainingAmount = 0;

                            //cc.CustomerTermLoan_InsertOrUpdate(objcc);
                            //CompanyService cmp = new CompanyService();
                            //Model.CompanyStore CompanyStores = cmp.CompanyStores.Where(p=>p.Id==objcc.ShopStoreId).FirstOrDefault();
                            //if (CompanyStores != null)
                            //{
                            //    lblAddress.Text = CompanyStores.Address;
                            //}

                        }
                    }
                }
            }
        }
    }
}