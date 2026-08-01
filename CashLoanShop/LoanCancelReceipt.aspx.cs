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
    public partial class LoanCancelReceipt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["Id"]))
                {
                    int CustomerLoanId = Convert.ToInt32(Request.QueryString["Id"]);
                    if (string.IsNullOrEmpty(Request.QueryString["Type"]))
                    {
                        CustomerLoanService cc = new CustomerLoanService();
                        Model.CustomerLoan objcc = cc.CustomerLoans.ToList().Where(p => p.Id == CustomerLoanId).FirstOrDefault();
                        if (objcc != null)
                        {
                            //LoanPartialPayment objpp = cc.LoanPartialPaymentsnew.Where(p => p.LoanId == objcc.Id).ToList().OrderByDescending(t => t.Id).ToList().FirstOrDefault();
                            CustomerService cs = new CustomerService();
                            CustomerMaster cm = cs.CustomerMasters.ToList().Where(p => p.Id == objcc.CustomerId).FirstOrDefault();

                            CompanyService cmp = new CompanyService();
                            Model.CompanyStore CompanyStores = cmp.CompanyStores.Where(p => p.Id == objcc.ShopStoreId).FirstOrDefault();
                            if (CompanyStores != null)
                            {
                                lblStoreInfo.Text = CompanyStores.Name + "<br/>" + CompanyStores.Address.Replace(",", "<br/>").Replace("$", " , ") + "<br/>" + CompanyStores.PhoneNo + "<br/>" + CompanyStores.Email;
                                lblTransactionType.Text = CompanyStores.Businessname;
                                //lblNSFCharges.Text = "$" + CompanyStores.NSFCharge.ToString();
                            }
                            //decimal Partialamountpaid = objpp.PartialAmount;
                            lblCustomerName.Text = cm.FirstName + " " + cm.LastName;
                            lblDateTime.Text = ConvertEasternTime(DateTime.Now).ToString("MM/dd/yyyy hh:mm:ss tt").Replace("-", "/");

                            lblReceiptNumber.Text = objcc.Id.ToString();
                            lblLoanAmount.Text = "$" + objcc.LoanAmountApproved.ToString();
                            lblAdminFee.Text = "$ 0.00";
                            lblDueAmount.Text = "$" + objcc.LoanAmountApproved.ToString();
                            lblDueDate.Text = Convert.ToDateTime(objcc.NextPayDate).ToString("MM/dd/yyyy").Replace("-", "/");
                            lblLateInterestCharges.Text = "$ 0.00";
                            lblTotalDueAmount.Text = "$" + objcc.LoanAmountApproved.ToString();
                            lblLastDueAmount.Text = "$" + objcc.LoanAmountApproved.ToString();
                            lblCashpaid.Text = "$" + objcc.LoanAmountApproved.ToString();
                            lblNSFCharges.Text = "$ 0.00";
                            lblDiscount.Text = "$ 0.00";

                            objcc.RemainingAmount = 0;
                            objcc.LoanStatus = "Cancelled";
                            objcc.UpdatedDate = ConvertEasternTime(DateTime.Now);
                            if (!string.IsNullOrEmpty(Request.QueryString["data"]))
                            {
                                objcc.ModeofPayment = Request.QueryString["data"].ToString();
                            }
                            cc.CustomerLoan_InsertOrUpdate(objcc);
                        }
                    }
                    else
                    {
                        using (TermLoanService cc = new TermLoanService())
                        {
                            Model.CustomerTermLoan objcc = cc.CustomerTermLoansbyId(CustomerLoanId).FirstOrDefault();
                            if (objcc != null)
                            {
                                //LoanPartialPayment objpp = cc.LoanPartialPaymentsnew.Where(p => p.LoanId == objcc.Id).ToList().OrderByDescending(t => t.Id).ToList().FirstOrDefault();
                                CustomerService cs = new CustomerService();
                                CustomerMaster cm = cs.CustomerMasters.ToList().Where(p => p.Id == objcc.CustomerId).FirstOrDefault();

                                CompanyService cmp = new CompanyService();
                                Model.CompanyStore CompanyStores = cmp.CompanyStores.Where(p => p.Id == objcc.ShopStoreId).FirstOrDefault();
                                if (CompanyStores != null)
                                {
                                    lblStoreInfo.Text = CompanyStores.Name + "<br/>" + CompanyStores.Address.Replace(",", "<br/>").Replace("$", " , ") + "<br/>" + CompanyStores.PhoneNo + "<br/>" + CompanyStores.Email;
                                    lblTransactionType.Text = CompanyStores.Businessname;
                                    //lblNSFCharges.Text = "$" + CompanyStores.NSFCharge.ToString();
                                }
                                //decimal Partialamountpaid = objpp.PartialAmount;
                                lblCustomerName.Text = cm.FirstName + " " + cm.LastName;
                                lblDateTime.Text = ConvertEasternTime(DateTime.Now).ToString("MM/dd/yyyy hh:mm:ss tt").Replace("-", "/");

                                lblReceiptNumber.Text = objcc.Id.ToString();
                                lblLoanAmount.Text = "$" + objcc.LoanAmountApproved.ToString();
                                lblAdminFee.Text = "$ 0.00";
                                lblDueAmount.Text = "$" + objcc.LoanAmountApproved.ToString();
                                lblDueDate.Text = Convert.ToDateTime(objcc.DueDate).ToString("MM/dd/yyyy").Replace("-", "/");
                                lblLateInterestCharges.Text = "$ 0.00";
                                lblTotalDueAmount.Text = "$" + objcc.LoanAmountApproved.ToString();
                                lblLastDueAmount.Text = "$" + objcc.LoanAmountApproved.ToString();
                                lblCashpaid.Text = "$" + objcc.LoanAmountApproved.ToString();
                                lblNSFCharges.Text = "$ 0.00";
                                lblDiscount.Text = "$ 0.00";

                               // objcc.RemainingAmount = 0;
                                objcc.LoanStatus = "Cancelled";
                                objcc.UpdatedDate = ConvertEasternTime(DateTime.Now);
                                if (!string.IsNullOrEmpty(Request.QueryString["data"]))
                                {
                                    objcc.PaymentOption = Request.QueryString["data"].ToString();
                                }
                                cc.CustomerTermLoan_InsertOrUpdate(objcc);
                            }
                        }
                    }
                }
            }
        }
        public DateTime ConvertEasternTime(DateTime date)
        {
            TimeZoneInfo timeZoneInfo;
            DateTime dateTime;
            //Set the time zone information to US Mountain Standard Time 
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            //Get date and time in US Mountain Standard Time 
            dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            //Print out the date and time
            //Console.WriteLine(dateTime.ToString("yyyy-MM-dd HH-mm-ss"));
            return dateTime;
        }
    }
}