using CashLoanShop.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CashLoanShop
{
    public partial class ViewInterestEarnedReport : System.Web.UI.Page
    {
        public decimal TotalLoanAmount, TotalServiceFee, TotalCashPaid, TotalAmountInstallmentReceived, TotalEarnedInterest, TotalPrincipalReceived, TotalBalanceDue,TotalDiscount,TotalAmountThisMonth;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(Request.QueryString["StoreId"]) && !string.IsNullOrEmpty(Request.QueryString["FromDate"]))
            {
                CompanyService cmp = new CompanyService();
                Model.CompanyStore CompanyStores = cmp.CompanyStores.ToList().Where(p => p.Id == Convert.ToInt32(Request.QueryString["StoreId"])).FirstOrDefault();
                if (CompanyStores != null)
                {
                    lblStoreInfo.Text = CompanyStores.Name + "<br/>" + CompanyStores.Address.Replace(",", "<br/>").Replace("$", " , ") + "<br/>" + CompanyStores.Email;
                }
                DateTime Fromdate = Convert.ToDateTime(Request.QueryString["FromDate"]);
                lblDateRange.Text = Fromdate.ToString("dd/MM/yyyy") + " to " + Fromdate.AddMonths(1).AddDays(-1).ToString("dd/MM/yyyy");
                TermLoanService ts = new TermLoanService();
                DataTable dt = ts.GetInterestEarnedReport(Fromdate, Fromdate.AddMonths(1), Convert.ToInt32(Request.QueryString["StoreId"]));
                if (dt.Rows.Count > 0)
                {
                    rptGridData.DataSource = dt;
                    rptGridData.DataBind();
                }
            }
        }
        protected void rptGridData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView item = (DataRowView)e.Item.DataItem;
                TotalAmountInstallmentReceived += Convert.ToDecimal(item["Amount of Installment Received"]);
                TotalAmountThisMonth += Convert.ToDecimal(item["Amount Received This Month"]);
                TotalBalanceDue += Convert.ToDecimal(item["Balance Due"]);
                TotalCashPaid += Convert.ToDecimal(item["CashPaid"]);
                TotalEarnedInterest += Convert.ToDecimal(item["Interest Earned"]);
                TotalLoanAmount += Convert.ToDecimal(item["LoanAmount"]);
                TotalPrincipalReceived += Convert.ToDecimal(item["Principle Received"]);
                TotalServiceFee += Convert.ToDecimal(item["ServiceFee"]);
                TotalDiscount += Convert.ToDecimal(item["TotalDiscount"]);
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
            }
        }
    }
}