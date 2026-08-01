using CashLoanShop.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CashLoanShop.Admin
{
    public partial class CustomMessage : System.Web.UI.Page
    {
        CustomerService cs = new CustomerService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CashLoanShop.Model.CustomMessage cm = cs.CustomMessages.ToList().Where(p => p.Id == Convert.ToInt32(1)).FirstOrDefault();
                if (cm != null)
                {
                    txtMessage.Text = cm.Message;
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            CashLoanShop.Model.CustomMessage cm = cs.CustomMessages.ToList().Where(p => p.Id == Convert.ToInt32(1)).FirstOrDefault();
            cm.Message = txtMessage.Text;
            cs.CustomMessage_InsertOrUpdate(cm);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}