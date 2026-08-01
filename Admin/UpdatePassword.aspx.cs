using CashLoanShop.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CashLoanShop.Admin
{
    public partial class UpdatePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            CustomerService cs = new CustomerService();
            CashLoanShop.Model.AdminUser cm = cs.AdminUsers.ToList().Where(p => p.Password.ToLower() == txtOldPassword.Text.ToLower()).FirstOrDefault();
            if (cm != null)
            {
                cm.Password = txtNewPassword.Text;
                cs.AdminUser_InsertOrUpdate(cm);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "runtestasdsdsds", "alert('Pasword Chagned Successfully');", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "runtest", "alert('Invalid Old Password');", true);
            }
        }
    }
}