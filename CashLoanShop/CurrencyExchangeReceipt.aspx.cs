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
    public partial class CurrencyExchangeReceipt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["Id"]))
                {
                    int CustomerLoanId = Convert.ToInt32(Request.QueryString["Id"]);
                    CurrencyExchangeService cc = new CurrencyExchangeService();
                    Model.CurrencyExchange objcc = cc.CurrencyExchanges.ToList().Where(p => p.Id == CustomerLoanId).FirstOrDefault();
                    if (objcc != null)
                    {
                        CustomerService cs = new CustomerService();
                        CustomerMaster cm = cs.CustomerMasters.ToList().Where(p => p.Id == objcc.CustomerId).FirstOrDefault();
                        lblCustomerName.Text = cm.FirstName + " " + cm.LastName;
                        lblDateTime.Text = Convert.ToDateTime(objcc.CreatedDate).ToString("MM/dd/yyyy hh:mm:ss tt").Replace("-","/");
                        lblTransactionType.Text = "Currency Exchange";
                        lblReceiptNumber.Text = objcc.Id.ToString();
                        lblConvertedAmount.Text = "$" + objcc.ConvertedAmount.ToString();
                        lblDueAmount.Text = "$" + objcc.DueAmount.ToString();
                        lblExchangeRate.Text = "$" + objcc.ExchangeRate.ToString();
                        lblServiceCharge.Text = "$" + objcc.ServiceCharge.ToString();
                        lblSummary.Text = objcc.TransactionType + " " + objcc.CurrencyType.Substring(0, objcc.CurrencyType.IndexOf("-")).Trim() + " " + objcc.ExchangeAmount.ToString();
                        CompanyService cmp = new CompanyService();
                        Model.CompanyStore CompanyStores = cmp.CompanyStores.Where(p => p.Id == objcc.ShopStoreId).FirstOrDefault();
                        if (CompanyStores != null)
                        {
                            lblStoreInfo.Text = CompanyStores.Name   + "<br/>" + CompanyStores.Address.Replace(",", "<br/>").Replace("$", " , ")+ "<br/>" + CompanyStores.Email;
                        }

                    }
                }
            }
        }
    }
}