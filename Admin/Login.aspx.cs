using CashLoanShop.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CashLoanShop.Admin
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            CustomerService cs = new CustomerService();
            CashLoanShop.Model.AdminUser u = cs.AdminUsers.ToList().Where(p => p.Username.ToLower() == txtUserName.Text.ToLower() && p.Password.ToLower() == txtPassword.Text.ToLower()).FirstOrDefault();
            if(u!=null)
            {
                Session["UserId"] = u.Id.ToString();
                Response.Redirect("~/Admin/UserManager.aspx");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "run", "alert('Invalid username or password');", true);
            }
            
        }
    }
}