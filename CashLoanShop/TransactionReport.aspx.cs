using CashLoanShop.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CashLoanShop
{
    public partial class TransactionReport : System.Web.UI.Page
    {
        public int UserId { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            //HttpCookie myCookie = Request.Cookies["UserId"];
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
                txtFromDate.Text = ConvertEasternTime(DateTime.Now).ToString("MM/dd/yyyy").Replace("-", "/");
                txtToDate.Text = ConvertEasternTime(DateTime.Now).ToString("MM/dd/yyyy").Replace("-","/");
                BindCombo();
            }
        }
        public DateTime ConvertEasternTime(DateTime date)
        {
            TimeZoneInfo timeZoneInfo;
            DateTime dateTime;
            //Set the time zone information to US Mountain Standard Time 
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            //Get date and time in US Mountain Standard Time 
            dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            //Print out the date and time
            //Console.WriteLine(dateTime.ToString("yyyy-MM-dd HH-mm-ss"));
            return dateTime;
        }
        public void BindCombo()
        {
            CompanyService cs = new CompanyService();
           // HttpCookie UserStoreCookie = Request.Cookies["UserStoreId"];
            List<Model.CompanyStore> lstCompanyStores = cs.CompanyStores.ToList().Where(p => p.Id == Convert.ToInt32(Session["UserStoreId"])).ToList();
//            lstCompanyStores[0].Address = lstCompanyStores[0].Address.Replace("$", ",");
            lstCompanyStores.FirstOrDefault().Name = lstCompanyStores.FirstOrDefault().Name.Replace("<br/>", "-");
            ddlShopStore.DataSource = lstCompanyStores;
            ddlShopStore.DataTextField = "Name";
            ddlShopStore.DataValueField = "Id";
            //lstCompanyStores.Insert(0, new Model.CompanyStore { Id = 0, Address = "--Select--" });
            ddlShopStore.DataBind();
        }
    }
}