using CashLoanShop.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CashLoanShop.Admin
{
    public partial class GenerateCSVReport_Old : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
                txtDate.Text = ConvertEasternTime(DateTime.Now).ToString("MM/dd/yyyy");
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
        private void BindGrid()
        {
            CustomerService cs = new CustomerService();
            List<CashLoanShop.Model.UserStoreDetails> lst = new List<Model.UserStoreDetails>();
            lst.Add(new Model.UserStoreDetails { StoreId = 123, UserName = "Finch" });
            lst.Add(new Model.UserStoreDetails { StoreId = 124, UserName = "Jane" });
            lst.Add(new Model.UserStoreDetails { StoreId = 125, UserName = "Fowler" });
            lst.Add(new Model.UserStoreDetails { StoreId = 126, UserName = "Rexdale" });
            lst.Add(new Model.UserStoreDetails { StoreId = 127, UserName = "Yonge" });
            lst.Add(new Model.UserStoreDetails { StoreId = 128, UserName = "Queen" });
            lst.Add(new Model.UserStoreDetails { StoreId = 1129, UserName = "Dundas" });
            lst.Add(new Model.UserStoreDetails { StoreId = 1130, UserName = "Steels" });
            lst.Add(new Model.UserStoreDetails { StoreId = 1131, UserName = "Dufferin" });
            lst.Add(new Model.UserStoreDetails { StoreId = 1132, UserName = "Main" });
            lst.Add(new Model.UserStoreDetails { StoreId = 1133, UserName = "London" });
            lst.Add(new Model.UserStoreDetails { StoreId = 1134, UserName = "Berrie" });
            dgvCustomer.DataSource = lst;
            dgvCustomer.DataBind();
            mvView.ActiveViewIndex = 0;
        }
    }
}