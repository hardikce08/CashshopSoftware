using CashLoanShop.DataAccess;
using CashLoanShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CashLoanShop
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            CustomerService cs = new CustomerService();
            CashLoanShop.Model.User cm = cs.Users.ToList().Where(p => p.Password.ToLower() == txtOldPassword.Text.ToLower()).FirstOrDefault();
            if (cm != null)
            {
                cm.Password = txtNewPassword.Text;
                cm.PasswordHash = MD5Hash(txtNewPassword.Text.ToLower());
                cs.User_InsertOrUpdate(cm);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "runtest", "alert('Invalid Old Password');", true);
            }
        }
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
    }
}