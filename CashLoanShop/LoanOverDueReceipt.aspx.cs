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
    public partial class LoanOverDueReceipt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["Id"]))
                {
                    int CustomerLoanId = Convert.ToInt32(Request.QueryString["Id"]);
                    CustomerLoanService cc = new CustomerLoanService();
                    Model.CustomerLoan objcc = cc.CustomerLoansbyId(CustomerLoanId).FirstOrDefault();
                    if (objcc != null)
                    {
                        CustomerService cs = new CustomerService();
                        CustomerMaster cm = cs.CustomerMastersById(objcc.CustomerId).FirstOrDefault();


                        int DayDiff = Convert.ToDateTime(objcc.NextPayDate).Subtract(objcc.CreatedDate).Days;
                        string MailTemplate = System.IO.File.ReadAllText(Server.MapPath("~/OverDueReceipt.html"));
                        CompanyService cmp = new CompanyService();
                        Model.CompanyStore CompanyStores = cmp.CompanyStores.Where(p => p.Id == objcc.ShopStoreId).FirstOrDefault();
                        if (CompanyStores != null)
                        {
                            //MailTemplate = MailTemplate.Replace("@storeaddress", CompanyStores.Name + " O/A " + CompanyStores.Businessname + "<br/>" + CompanyStores.NewAddress + "<br/>" + CompanyStores.Email);
                            MailTemplate = MailTemplate.Replace("@storeaddress", CompanyStores.Name + " O/A " + CompanyStores.Businessname + "<br/>" + CompanyStores.NewAddress + "<br/>" + CompanyStores.City + " , " + GetProvince(Convert.ToInt32(CompanyStores.Province)) + " , " + CompanyStores.PostCode + "<br/> Email:" + CompanyStores.Email);
                            MailTemplate = MailTemplate.Replace("@storename", CompanyStores.Name + " O/A " + CompanyStores.Businessname);
                            MailTemplate = MailTemplate.Replace("@storesubaddress", CompanyStores.NewAddress + " , " + CompanyStores.City + " , " + GetProvince(Convert.ToInt32(CompanyStores.Province)) + " , " + CompanyStores.PostCode);
                        }
                        List<Model.LoanPartialPayment> lstpartialpayment = cc.LoanPartialPaymentsnew.Where(p => p.LoanId == objcc.Id).ToList();
                        if (lstpartialpayment.Count > 0)
                        {
                            MailTemplate = MailTemplate.Replace("@PartialAmount", "$" + lstpartialpayment.Sum(p => p.PartialAmount).ToString());
                        }
                        else
                        {
                            MailTemplate = MailTemplate.Replace("@PartialAmount", "$0.00");
                        }
                        MailTemplate = MailTemplate.Replace("@interestpercentage", Convert.ToInt32(CompanyStores.InterestRate).ToString());
                        MailTemplate = MailTemplate.Replace("@customername", cm.FirstName + " " + cm.LastName);
                        MailTemplate = MailTemplate.Replace("@currentdate", objcc.UpdatedDate.ToString("dddd, MMMM d, yyyy"));
                        MailTemplate = MailTemplate.Replace("@address", cm.Address + "<br/>" + cm.City + "," + cm.PostCode);
                        MailTemplate = MailTemplate.Replace("@loanamount", "$" + objcc.DueAmount.ToString());
                        MailTemplate = MailTemplate.Replace("@lateinterestcharge", "$" + (objcc.LateInterestCharge == null ? 0 : objcc.LateInterestCharge).ToString());
                        MailTemplate = MailTemplate.Replace("@NSFCharge", "$" + (objcc.NSFCharge == null ? 0 : objcc.NSFCharge).ToString());
                        MailTemplate = MailTemplate.Replace("@totaldueamount", ((objcc.DueAmount + (objcc.NSFCharge == null ? 0 : objcc.NSFCharge) + (objcc.LateInterestCharge == null ? 0 : objcc.LateInterestCharge)) - objcc.DiscountAmount - (lstpartialpayment.Count > 0 ? lstpartialpayment.Sum(p => p.PartialAmount) : 0)).ToString());
                        MailTemplate = MailTemplate.Replace("@DiscAmount", "$" + objcc.DiscountAmount.ToString());


                        content.InnerHtml = MailTemplate.ToString();
                    }
                }
            }
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
    }
}