using CashLoanShop.DataAccess;
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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           // HttpCookie myCookie = Request.Cookies["UserId"];
            // Read the cookie information and display it.
            if (Session["UserId"] != null)
            {
                Response.Redirect("~/Menu.aspx");
            }
           

            if (!IsPostBack)
            {
                //Update();
            }
        }
        public void Update()
        {
            CustomerService cs = new CustomerService();
            List<CashLoanShop.Model.User> lst = cs.Users.ToList();
            foreach (CashLoanShop.Model.User u in lst)
            {
                u.PasswordHash = MD5Hash(u.Password.ToLower());
                cs.User_InsertOrUpdate(u);
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            CustomerService cs = new CustomerService();
            //string test = MD5Hash("finch5200");
            CashLoanShop.Model.User u = cs.Users.ToList().Where(p => p.UserName.ToLower() == txtUserName.Text.ToLower() && p.PasswordHash.ToLower() == txtPassword.Text.ToLower()).FirstOrDefault();
            if (u != null)
            {
                Session["UserId"] = u.Id;
                Session["UserRoleId"] = u.UserType;
                Session["UserStoreId"] = u.StoreId;
                Session["UserName"] = u.UserName;
                //HttpCookie myCookie = new HttpCookie("UserId");
                //HttpCookie Usernamecookie = new HttpCookie("UserName");
                //HttpCookie myUserStoreCookie = new HttpCookie("UserStoreId");
                //HttpCookie myUserRoleCookie = new HttpCookie("UserRoleId");
                //DateTime now = DateTime.Now;

                //// Set the cookie value.
                //myCookie.Value =u.Id.ToString();
                //myUserStoreCookie.Value = u.StoreId.ToString();
                //myUserRoleCookie.Value = u.UserType.ToString();
                //Usernamecookie.Value = u.UserName;
                //// Set the cookie expiration date.
                //myCookie.Expires = ConvertEasternTime(now).AddHours(12); // For a cookie to effectively never expire
                //myUserStoreCookie.Expires = ConvertEasternTime(now).AddHours(12);
                //myUserRoleCookie.Expires = ConvertEasternTime(now).AddHours(12);
                //Usernamecookie.Expires = ConvertEasternTime(now).AddHours(12);
                //// Add the cookie.
                //Response.Cookies.Add(myCookie);
                //Response.Cookies.Add(Usernamecookie);
                //Response.Cookies.Add(myUserStoreCookie);
                //Response.Cookies.Add(myUserRoleCookie);

                Response.Redirect("~/Menu.aspx");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "run", "alert('Invalid username or password');", true);
            }
            
        }
        public static string EncryptString(string Password, string UserName)
        {
            byte[] dataToHashBytes = System.Text.Encoding.ASCII.GetBytes(Password);
            var md5 = MD5.Create();
             
                var hashed= md5.ComputeHash(dataToHashBytes);
                return BitConverter.ToString(hashed).Replace("-", "");
                //Console.WriteLine(BitConverter.ToString(hashed).Replace("-", ""));
             
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