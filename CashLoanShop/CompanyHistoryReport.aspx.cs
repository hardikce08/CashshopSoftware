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
    public partial class CompanyHistoryReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(Request.QueryString["Id"]))
            {
                CompanyService cs = new CompanyService();
                List<Model.Company> cm = cs.Companys.ToList().Where(p => p.Id == Convert.ToInt32(Request.QueryString["Id"])).ToList();
                cm.FirstOrDefault().Province = GetProvince(Convert.ToInt32(cm.FirstOrDefault().Province));
                rptCompany.DataSource = cm;
                rptCompany.DataBind();

                CashChequeService cc = new CashChequeService();
                List<CashCheque> lsthistory = cc.CashChequeGriddataCompanyHistory.ToList().Where(p => p.ChequeIssuerId == Convert.ToInt32(Request.QueryString["Id"])).ToList();

                rptGridData.DataSource = lsthistory;
                rptGridData.DataBind();

                
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