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
    public partial class ViewPaymentSchedule : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        public void BindGrid()
        {
            using (TermLoanService ts = new TermLoanService())
            {
                if (string.IsNullOrEmpty(Request.QueryString["TermLoanId"]))
                {
                    DateTime DueDate = Convert.ToDateTime(Request.QueryString["FirstDate"]).AddYears(Convert.ToInt32(Request.QueryString["Duration"]));

                    System.Data.DataSet ds = ts.GetPaymentSchedule(Convert.ToDateTime(Request.QueryString["FirstDate"]), (Request.QueryString["SecondDate"] == string.Empty ? Convert.ToDateTime(DateTime.Now) : Convert.ToDateTime(Request.QueryString["SecondDate"])), DueDate, Request.QueryString["PaymentType"], Convert.ToDecimal(Request.QueryString["Amount"]));
                    rptGridData.DataSource = ds.Tables[0];
                    rptGridData.DataBind();
                }
                else
                {
                    CustomerTermLoan objloan = ts.CustomerTermLoansbyId(Convert.ToInt32(Request.QueryString["TermLoanId"])).FirstOrDefault();
                    if (objloan != null)
                    {
                        //DateTime DueDate = Convert.ToDateTime(Request.QueryString["FirstDate"]).AddYears(Convert.ToInt32(Request.QueryString["Duration"]));

                        System.Data.DataSet ds = ts.GetPaymentSchedule(Convert.ToDateTime(objloan.FirstInstallmentDate), (!objloan.SecondInstallmentDate.HasValue ? Convert.ToDateTime(DateTime.Now) : Convert.ToDateTime(objloan.SecondInstallmentDate)), objloan.DueDate, objloan.LoanType, Convert.ToDecimal(objloan.DueAmount));
                        rptGridData.DataSource = ds.Tables[0];
                        rptGridData.DataBind();
                    }
                }
            }
        }
    }
}