using CashLoanShop.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CashLoanShop
{
    public partial class CompanyHistory : System.Web.UI.Page
    {
        CompanyService cs = new CompanyService();
        public int UserId { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            //HttpCookie myCookie = Request.Cookies["UserId"];
            if (Session["UserId"] != null && this.UserId == 0)
            {
                this.UserId = Convert.ToInt32(Session["UserId"].ToString());
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
               // BindGrid();

            }
        }



        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        private void BindGrid()
        {

            List<CashLoanShop.Model.Company> lst = cs.Companys.ToList();
            if (txtSearchName.Text != string.Empty)
            {
                lst = lst.Where(p => p.Name.ToLower().StartsWith(txtSearchName.Text.ToLower())).ToList();
            }
            dgvCompany.DataSource = lst;
            dgvCompany.DataBind();
            mvView.ActiveViewIndex = 0;
        }
        protected void dgvCompany_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }
        protected void dgvCompany_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvCompany.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void dgvCompany_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }
    }
}