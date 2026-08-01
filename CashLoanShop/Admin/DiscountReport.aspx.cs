using CashLoanShop.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CashLoanShop.Admin
{
    public partial class DiscountReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtFromDate.Text = ConvertEasternTime(DateTime.Now.AddMonths(-1)).ToString("MM/dd/yyyy").Replace("-", "/");
                txtToDate.Text = ConvertEasternTime(DateTime.Now).ToString("MM/dd/yyyy").Replace("-", "/");
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
            using (CompanyService cs = new CompanyService())
            {
                List<Model.CompanyStore> lstCompanyStores = cs.CompanyStores.ToList();
                if (lstCompanyStores.Count > 0)
                {
                    foreach (var item in lstCompanyStores)
                    {
                        item.Name = item.Id.ToString() + " - " + item.Name + " (" + item.Address.Replace("$", " , ") + ")";
                    }
                }
                lstCompanyStores.Insert(0, new Model.CompanyStore { Id = 0, Name = "--Select--" });
                ddlShopStore.DataSource = lstCompanyStores;
                ddlShopStore.DataTextField = "Name";
                ddlShopStore.DataValueField = "Id";
                ddlShopStore.DataBind();
            }
        }
    }
}