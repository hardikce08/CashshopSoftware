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
    public partial class CustomerLoanReceipt : System.Web.UI.Page
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
                        lblCustomerName.Text = cm.FirstName + " " + cm.Mi + " " + cm.LastName;
                        lblCustomerNames.Text = cm.FirstName + " " + cm.Mi + " " + cm.LastName;
                        lblAddress.Text = cm.Address + "<br/>" + cm.City + "," + cm.ProvinceName + "<br/>" + cm.PostCode;
                        lblPhone.Text = cm.CellPhone;
                        lblDateTime.Text = Convert.ToDateTime(objcc.CreatedDate).ToString("MM/dd/yyyy").Replace("-", "/");

                        lblReceiptNumber.Text = objcc.Id.ToString();
                        lblLoanAmount.Text = "$" + objcc.LoanAmountApproved.ToString();
                        lblCashout.Text = "$" + objcc.LoanAmountApproved.ToString();
                        lblAdminFee.Text = "$" + objcc.AdminFee.ToString();
                        lblDueAmount.Text = "$" + objcc.DueAmount.ToString();
                        lblDueDate.Text = Convert.ToDateTime(objcc.NextPayDate).ToString("MM/dd/yyyy").Replace("-", "/");
                        CompanyService cmp = new CompanyService();
                        Model.CompanyStore CompanyStores = cmp.CompanyStoresbyId(objcc.ShopStoreId);
                        if (CompanyStores != null)
                        {
                            lblStoreInfo.Text = CompanyStores.Name + " O/A " + CompanyStores.Businessname + "<br/>" + CompanyStores.NewAddress + "<br/>" + CompanyStores.City + " , " + GetProvince(Convert.ToInt32(CompanyStores.Province)) + " , " + CompanyStores.PostCode + "<br/>Phone:" + CompanyStores.PhoneNo + ", Fax:" + CompanyStores.Fax + "<br/> Email:" + CompanyStores.Email;
                            lblBusinessname.Text = CompanyStores.Businessname;
                            //lblTransactionType.Text = CompanyStores.Businessname;
                        }
                        if (objcc.Last63DaysLoanCount >= 2 && objcc.LoanPaymentType == "1 Installment")
                        {
                            dvextra.Style.Add(HtmlTextWriterStyle.Display, "");
                        }
                        else
                        {
                            dvextra.Style.Add(HtmlTextWriterStyle.Display, "none");
                        }
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