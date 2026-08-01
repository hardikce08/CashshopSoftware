using CashLoanShop.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CashLoanShop
{
    public partial class RegistrarReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //HttpCookie myCookie = Request.Cookies["UserId"];
            // Read the cookie information and display it.
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                BindCombo();
            }
        }
        public void BindCombo()
        {
            CompanyService cs = new CompanyService();
            //HttpCookie UserStoreCookie = Request.Cookies["UserStoreId"];
            List<Model.CompanyStore> lstCompanyStores = cs.CompanyStores.ToList().Where(p => p.Id == Convert.ToInt32(Session["UserStoreId"])).ToList();
            lstCompanyStores.FirstOrDefault().Name = lstCompanyStores.FirstOrDefault().Name.Replace("<br/>", "-");
            ddlShopStore.DataSource = lstCompanyStores;
            ddlShopStore.DataTextField = "Name";
            ddlShopStore.DataValueField = "Id";
            //lstCompanyStores.Insert(0, new Model.CompanyStore { Id = 0, Address = "--Select--" });
            ddlShopStore.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CustomerService cs = new CustomerService();
            System.Data.DataSet ds = cs.GetRegistrarReport(Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text), Convert.ToInt32(ddlShopStore.SelectedValue));
            dgvData.DataSource = ds.Tables[0];
            dgvData.DataBind();
            CompanyService cmp = new CompanyService();
            Model.CompanyStore CompanyStores = cmp.CompanyStores.ToList().Where(p => p.Id == Convert.ToInt32(ddlShopStore.SelectedValue.ToString())).FirstOrDefault();
            lblStoreAddress.Text = CompanyStores.Businessname + "<br/>" + CompanyStores.Name + "<br/>" + CompanyStores.Address.Replace("$", " , ") + "<br/> Phone No:" + CompanyStores.PhoneNo + " , FAX No:" + CompanyStores.Fax;
            lblFromDate.Text = Convert.ToDateTime(txtFromDate.Text).ToString("MMM dd, yyyy");
            lblToDate.Text = Convert.ToDateTime(txtToDate.Text).ToString("MMM dd, yyyy");
            //string MailTemplate = System.IO.File.ReadAllText(Server.MapPath("~/RegistrarReport.html"));
            //MailTemplate = MailTemplate.Replace("@storeaddress",   CompanyStores.Address.Replace(",", ",").Replace("$", " , ") + "," + CompanyStores.PhoneNo);
            //MailTemplate = MailTemplate.Replace("@1", ds.Tables[0].Rows[0][1].ToString());
            //MailTemplate = MailTemplate.Replace("@2", ds.Tables[0].Rows[1][1].ToString());
            //MailTemplate = MailTemplate.Replace("@3", ds.Tables[0].Rows[2][1].ToString());
            //MailTemplate = MailTemplate.Replace("@4", ds.Tables[0].Rows[3][1].ToString());
            //MailTemplate = MailTemplate.Replace("@5", ds.Tables[0].Rows[4][1].ToString());
            //MailTemplate = MailTemplate.Replace("@6", ds.Tables[0].Rows[5][1].ToString());
            //MailTemplate = MailTemplate.Replace("@7", ds.Tables[0].Rows[6][1].ToString());
            //MailTemplate = MailTemplate.Replace("@8", ds.Tables[0].Rows[7][1].ToString());
            //MailTemplate = MailTemplate.Replace("@9", ds.Tables[0].Rows[8][1].ToString());

            //MailTemplate = MailTemplate.Replace("@10", ds.Tables[1].Rows[0][1].ToString());
            //MailTemplate = MailTemplate.Replace("@11", ds.Tables[1].Rows[1][1].ToString());
            //MailTemplate = MailTemplate.Replace("@12", ds.Tables[1].Rows[2][1].ToString());
            //MailTemplate = MailTemplate.Replace("@13", ds.Tables[1].Rows[3][1].ToString());
            //MailTemplate = MailTemplate.Replace("@14", ds.Tables[1].Rows[4][1].ToString());
            //MailTemplate = MailTemplate.Replace("@15", ds.Tables[1].Rows[5][1].ToString());
            //MailTemplate = MailTemplate.Replace("@16", ds.Tables[1].Rows[6][1].ToString());
            //MailTemplate = MailTemplate.Replace("@17", ds.Tables[1].Rows[7][1].ToString());
            //MailTemplate = MailTemplate.Replace("@18", ds.Tables[1].Rows[8][1].ToString());

            //MailTemplate = MailTemplate.Replace("@19", ds.Tables[2].Rows[0][1].ToString());
            //MailTemplate = MailTemplate.Replace("@20", ds.Tables[2].Rows[3][1].ToString());
            //MailTemplate = MailTemplate.Replace("@21", ds.Tables[2].Rows[4][1].ToString());
            //MailTemplate = MailTemplate.Replace("@22", ds.Tables[2].Rows[1][1].ToString());
            //MailTemplate = MailTemplate.Replace("@23", ds.Tables[2].Rows[2][1].ToString());

            //MailTemplate = MailTemplate.Replace("@fromdate", Convert.ToDateTime(txtFromDate.Text).ToString("MMM dd, yyyy"));
            //MailTemplate = MailTemplate.Replace("@todate", Convert.ToDateTime(txtToDate.Text).ToString("MMM dd, yyyy"));
            //content.InnerHtml = MailTemplate.ToString();
        }
    }
}
