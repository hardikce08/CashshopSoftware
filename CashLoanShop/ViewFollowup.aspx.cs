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
    public partial class ViewFollowup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (FollowupService cls = new FollowupService())
                {
                    var currdate =ConvertEasternTime(DateTime.Now).Date;
                    List<CashLoanShop.Model.CustomerFollowup> cm = cls.CustomerFollowups.Where(p => p.NextFolloupDate == currdate).ToList();
                   foreach (var obj in cm)
                   {
                       using (CustomerService cs = new CustomerService())
                       {
                           List<CustomerMaster> cust = cs.CustomerMastersById(Convert.ToInt32(obj.CustomerId)).ToList();
                           obj.CustomerName = cust.FirstOrDefault().FirstName + " " + cust.FirstOrDefault().Mi + " " + cust.FirstOrDefault().LastName.ToString();
                           obj.ContactNo = cust.FirstOrDefault().CellPhone;
                       }
                   }
                   rptGridData.DataSource = cm;
                   rptGridData.DataBind();
                }
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
    }
}