using CashLoanShop.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CashLoanShop.Admin
{
    public partial class InterestEarnedReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
            List<Model.CompanyStore> lstCompanyStores = cs.CompanyStores.ToList();
            if (lstCompanyStores.Count > 0)
            {
                foreach (var item in lstCompanyStores)
                {
                    item.Name = item.Name + " (" + item.Address.Replace("$", " , ") + ")";
                }
            }
            lstCompanyStores.Insert(0, new Model.CompanyStore { Id = 0, Name = "--Select--" });
            ddlShopStore.DataSource = lstCompanyStores;
            ddlShopStore.DataTextField = "Name";
            ddlShopStore.DataValueField = "Id";
            //lstCompanyStores.Insert(0, new Model.CompanyStore { Id = 0, Address = "--Select--" });
            ddlShopStore.DataBind();
        }
    }
}