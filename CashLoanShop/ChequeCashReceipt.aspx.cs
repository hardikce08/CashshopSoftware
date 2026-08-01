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
    public partial class ChequeCashReceipt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["Id"]))
                {
                    int CashChequeId = Convert.ToInt32(Request.QueryString["Id"]);
                    CashChequeService cc = new CashChequeService();
                    CashCheque objcc = cc.CashChequesbyId(CashChequeId).FirstOrDefault();
                    if (objcc != null)
                    {
                        CustomerService cs = new CustomerService();
                        CustomerMaster cm = cs.CustomerMasters.ToList().Where(p => p.Id == objcc.CustomerId).FirstOrDefault();
                        lblCustomerName.Text = cm.FirstName + " " + cm.LastName;
                        lblCustomerId.Text = cm.Id.ToString();
                        lblDateTime.Text = Convert.ToDateTime(objcc.CreatedDate).ToString("MM/dd/yyyy hh:mm:ss tt").Replace("-", "/");
                        lblChequeType.Text = objcc.ChequeType == "Custom" ? objcc.ChequeType + " - " + objcc.CustomPercentage.ToString()+" % " : objcc.ChequeType;
                        lblReceiptNumber.Text = objcc.Id.ToString();
                        lblChqAmount.Text = "$" + objcc.ChequeAmount.ToString();
                        lblCharges.Text = "$" + objcc.Charges.ToString();
                        lblCashout.Text = "$" + objcc.AmountIssued.ToString();
                        lblAdminFee.Text = "$" + objcc.AdminFee.ToString();
                        CompanyService cmp = new CompanyService();
                        Model.CompanyStore CompanyStores = cmp.CompanyStoresbyId(objcc.ShopStoreId);
                        if (CompanyStores != null)
                        {
                            lblStoreInfo.Text = CompanyStores.Name + " O/A " + CompanyStores.Businessname + "<br/>" + CompanyStores.NewAddress + "<br/>" + CompanyStores.City + " , " + GetProvince(Convert.ToInt32(CompanyStores.Province)) + " , " + CompanyStores.PostCode + "<br/>Phone:" + CompanyStores.PhoneNo + ", Fax:" + CompanyStores.Fax + "<br/> Email:" + CompanyStores.Email;
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