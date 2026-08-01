using CashLoanShop.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CashLoanShop
{
    public partial class InterestEarnedReport : System.Web.UI.Page
    {
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
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                for (int i = 2018; i <= DateTime.Now.Year; i++)
                {
                    ddlYear.Items.Add(new ListItem { Text = i.ToString(), Value = i.ToString() });
                }
                BindCombo();
            }
        }
        public void BindCombo()
        {
            CompanyService cs = new CompanyService();
           // HttpCookie UserStoreCookie = Request.Cookies["UserStoreId"];
            List<Model.CompanyStore> lstCompanyStores = cs.CompanyStores.ToList().Where(p => p.Id == Convert.ToInt32(Session["UserStoreId"])).ToList();
            lstCompanyStores.FirstOrDefault().Name = lstCompanyStores.FirstOrDefault().Name.Replace("<br/>", "-");
            ddlShopStore.DataSource = lstCompanyStores;
            ddlShopStore.DataTextField = "Name";
            ddlShopStore.DataValueField = "Id";
            ddlShopStore.DataBind();
        }
    }
}