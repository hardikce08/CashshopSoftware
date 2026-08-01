using CashLoanShop.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CashLoanShop.Admin
{
    public partial class GenerateCSVTermLoanReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
                txtFromDate.Text = ConvertEasternTime(DateTime.Now.AddDays(-7)).ToString("MM/dd/yyyy");
                txtToDate.Text = ConvertEasternTime(DateTime.Now).ToString("MM/dd/yyyy");
            }
        }
        public DateTime ConvertEasternTime(DateTime date)
        {
            TimeZoneInfo timeZoneInfo;
            DateTime dateTime;
            //Set the time zone information to US Mountain Standard Time 
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            //Get date and time in US Mountain Standard Time 
            dateTime = TimeZoneInfo.ConvertTime(date, timeZoneInfo);
            //Print out the date and time
            //Console.WriteLine(dateTime.ToString("yyyy-MM-dd HH-mm-ss"));
            return dateTime;
        }
        private void BindGrid()
        {
            CustomerService cs = new CustomerService();
            List<CashLoanShop.Model.UserStoreDetails> lst = cs.UserStoreDetail.ToList();
            var t = lst.Select(x => new { x.StoreId, x.StoreName }).Distinct().ToList();
            dgvCustomer.DataSource = t;
            dgvCustomer.DataBind();
            mvView.ActiveViewIndex = 0;
        }
    }
}