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
    public partial class LoanCloseReport : System.Web.UI.Page
    {
        //HttpCookie myCookie;
        protected void Page_Load(object sender, EventArgs e)
        {
            //myCookie = Request.Cookies["UserId"];
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        private void BindGrid()
        {
            CustomerService cs = new CustomerService();
            dgvCustomer.DataSource = cs.GetLoanCloseReport(Convert.ToInt32(Session["UserId"])).Tables[0];
            dgvCustomer.DataBind();
            
        }
        protected void dgvCustomer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvCustomer.PageIndex = e.NewPageIndex;
            BindGrid();
        }
    }
}