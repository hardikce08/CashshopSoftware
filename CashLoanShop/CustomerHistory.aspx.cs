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
    public partial class CustomerHistory : System.Web.UI.Page
    {
        CurrencyExchangeService cs = new CurrencyExchangeService();
        public int UserId { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
           // HttpCookie myCookie = Request.Cookies["UserId"];
            if (Session["UserId"] != null && this.UserId == 0)
            {
                this.UserId = Convert.ToInt32(Session["UserId"]);
            }
            else
            {
                if (Request.QueryString["Mode"] == null)
                    Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                //BindGrid();

            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void BindGrid()
        {
            CustomerService cs = new CustomerService();
            List<CustomerMaster> lst = new List<CustomerMaster>();
            if (txtSearchName.Text == string.Empty && txtSearchLastName.Text == string.Empty && txtSearchSINNumber.Text == string.Empty && txtSearchId.Text == string.Empty && txtSearchPhoneNumber.Text == string.Empty) { }
            else
            {
                lst = cs.CustomerMasters.ToList();
            }
            if (txtSearchName.Text != string.Empty)
            {
                lst = lst.Where(p => p.FirstName.ToLower().StartsWith(txtSearchName.Text.ToLower())).ToList();
            }
            if (txtSearchLastName.Text != string.Empty)
            {
                lst = lst.Where(p => p.LastName.ToLower().StartsWith(txtSearchLastName.Text.ToLower())).ToList();
            }
            if (txtSearchId.Text != string.Empty)
            {
                lst = lst.Where(p => p.Id == Convert.ToInt32(txtSearchId.Text)).ToList();
            }
            if (txtSearchSINNumber.Text != string.Empty)
            {
                lst = lst.Where(p => p.SocialSecurityNumber.ToLower() == txtSearchSINNumber.Text.ToLower()).ToList();
            }
            if (txtSearchPhoneNumber.Text != string.Empty)
            {
                lst = lst.Where(p => p.HomePhone.ToLower().StartsWith(txtSearchPhoneNumber.Text.ToLower()) || p.WorkPhone.ToLower().StartsWith(txtSearchPhoneNumber.Text.ToLower()) || p.CellPhone.ToLower().StartsWith(txtSearchPhoneNumber.Text.ToLower())).ToList();
            }
            dgvCustomer.DataSource = lst;
            dgvCustomer.DataBind();
            cs = null;
        }
        protected void dgvCustomer_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        protected void dgvCustomer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvCustomer.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void dgvCustomer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink lnk = (HyperLink)e.Row.FindControl("lnkPOI");
                CustomerMaster cm = (CustomerMaster)e.Row.DataItem;
                if (!string.IsNullOrEmpty(cm.ImageName))
                {
                    lnk.NavigateUrl = GetRelativeRootPath() + "/ProofofIdentity/" + cm.ImageName;
                    lnk.Visible = true;
                }
                else
                {
                    lnk.Visible = false;
                }
                cm = null;
            }
        }

        public string GetRelativeRootPath()
        {
            string Port = System.Web.HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
            if (Port == null || Port == "80" || Port == "443")
                Port = "";
            else
                Port = ":" + Port;

            string Protocol = System.Web.HttpContext.Current.Request.ServerVariables["SERVER_PORT_SECURE"];
            if (Protocol == null || Protocol == "0")
                Protocol = "http://";
            else
                Protocol = "https://";

            string appPath = System.Web.HttpContext.Current.Request.ApplicationPath;
            if (appPath == "/")
                appPath = "";

            //string sOut = Protocol + System.Web.HttpContext.Current.Request.ServerVariables["HTTP_HOST"] + appPath;
            string sOut = Protocol + System.Web.HttpContext.Current.Request.ServerVariables["HTTP_HOST"];
            //sOut = sOut.Replace("/booksforyou", "");
            return sOut;
        }

    }
}