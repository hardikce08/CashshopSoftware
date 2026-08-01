using CashLoanShop.DataAccess;
using CashLoanShop.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CashLoanShop
{
    public partial class Menu : System.Web.UI.Page
    {
        public int UserId { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            //HttpCookie myCookie1 = Request.Cookies["UserId"];
            //if (myCookie1 == null) Response.Redirect("~/Login.aspx");
            if (!IsPostBack)
            {
                // HttpCookie myCookie = Request.Cookies["UserId"];
                if (Session["UserId"] != null && this.UserId == 0)
                {
                    UserId = Convert.ToInt32(Session["UserId"]);
                    hdnUserId.Value = Session["UserId"].ToString();
                    hdnStoreId.Value = Session["UserStoreId"].ToString();
                }
                else
                {
                    Response.Redirect("~/Login.aspx",true);
                }

                CustomerService cs = new CustomerService();
                CashLoanShop.Model.CustomMessage cm = cs.CustomMessages.ToList().Where(p => p.Id == Convert.ToInt32(1)).FirstOrDefault();
                if (cm.Message != string.Empty)
                {
                    data.Style.Add(HtmlTextWriterStyle.Display, "");
                    lblMessage.Text = cm.Message;
                }
                else
                {
                    data.Style.Add(HtmlTextWriterStyle.Display, "none");
                    lblMessage.Text = "";
                }

                DataSet ds = cs.Get24hrStats(Convert.ToInt32(Session["UserStoreId"]), Convert.ToInt32(Session["UserId"]));
                DataTable dt=ds.Tables[0];
                lblCustomerAdded.Text = dt.Rows[0]["NoofCustomerAdded"].ToString();
                lblPaydayLoanOpen.Text = dt.Rows[0]["NoofLoanOpen"].ToString();
                lblPaydayClosed.Text = dt.Rows[0]["NoofLoanClosed"].ToString();
                lblTermOpen.Text = dt.Rows[0]["NoofTermLoanOpen"].ToString();
                lblTermClosed.Text = dt.Rows[0]["NoofTermLoanClosed"].ToString();
                lblAmountofLoan.Text = dt.Rows[0]["AmountofLoanGiven"].ToString();

            }

        }
        [WebMethod]
        public static List<object> GetChartData(int storeid,int userid)
        {
            List<object> data = new List<object>();
            CustomerService cs = new CustomerService();
            DataSet ds = cs.Get7daysPaydayLoanStats(storeid, userid);
            DataTable dt = ds.Tables[0];
            data.Add(dt.Rows.OfType<DataRow>().Select(dr => (string)dr["LoanDate"]).ToList());
            data.Add(dt.Rows.OfType<DataRow>().Select(dr => Convert.ToDecimal(dr["LoanAmountApproved"])).ToList());
            data.Add(dt.Rows.OfType<DataRow>().Select(dr => Convert.ToInt32(dr["NoofLoan"])).ToList());
            return data;
           // return "This string is from Code behind";
        }  
    }
}