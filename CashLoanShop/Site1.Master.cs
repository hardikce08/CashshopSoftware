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
    public partial class Site1 : System.Web.UI.MasterPage
    {
        public int UserId { get; set; }
        CustomerService cs = new CustomerService();
        protected void Page_Load(object sender, EventArgs e)
        {
            //var aa = ConvertNumbertoWords(123).ToString();
            if (!IsPostBack)
            {
                //HttpCookie myCookie = Request.Cookies["UserId"];
                //HttpCookie UsernameCookie = Request.Cookies["UserName"];
                if (Session["UserId"] != null && this.UserId == 0)
                {
                    this.UserId = Convert.ToInt32(Session["UserId"]);
                    lblUserName.Text = Session["UserName"] != null ? Session["UserName"].ToString() : "Admin";
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }

                //set user menu 
                //HttpCookie UserRoleCookie = Request.Cookies["UserRoleId"];
                List<UserMenu> lstMenu = cs.UserMenus.ToList().Where(p => p.RoleId == Convert.ToInt32(Session["UserRoleId"].ToString())).ToList();
                liTermLoan.Style.Add(HtmlTextWriterStyle.Display, "none");
                foreach (UserMenu a in lstMenu)
                {
                    if (a.MenuName.Contains("Term Loan"))
                    {
                        lstMenu.Remove(a);
                        liTermLoan.Style.Add(HtmlTextWriterStyle.Display, "");
                        break;
                    }
                }
                rptMenu.DataSource = lstMenu.OrderBy(p => p.DisplayOrder).ToList();
                rptMenu.DataBind();
            }

            CashLoanShop.Model.CustomMessage cm = cs.CustomMessages.ToList().Where(p => p.Id == Convert.ToInt32(1)).FirstOrDefault();
            if (cm != null)
            {
                lblMessage.Text = !string.IsNullOrEmpty(cm.Message) ? "Important Message from Admin : " + cm.Message : "";
            }
            else
            {
                lblMessage.Text = "";
            }

        }
      
        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            //Response.Cookies.Clear();
            //HttpCookie myCookie = Request.Cookies["UserId"];
            //myCookie.Expires = ConvertEasternTime(DateTime.Now);
            //HttpCookie UsernameCookie = Request.Cookies["UserName"];
            //if (UsernameCookie != null)
            //{
            //    UsernameCookie.Expires = ConvertEasternTime(DateTime.Now);
            //    Response.Cookies.Add(UsernameCookie);
            //}
            //HttpCookie UserRoleCookie = Request.Cookies["UserRoleId"];
            //UserRoleCookie.Expires = ConvertEasternTime(DateTime.Now);
            //HttpCookie UserStoreCookie = Request.Cookies["UserStoreId"];
            //UserStoreCookie.Expires = ConvertEasternTime(DateTime.Now);
            //Response.Cookies.Add(myCookie);

            //Response.Cookies.Add(UserStoreCookie);
            //Response.Cookies.Add(UserRoleCookie);
            Response.Redirect("~/Login.aspx");
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