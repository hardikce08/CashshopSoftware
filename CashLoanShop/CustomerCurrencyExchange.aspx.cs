using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CashLoanShop.DataAccess;
using CashLoanShop.Model;
namespace CashLoanShop
{
    public partial class CustomerCurrencyExchange : System.Web.UI.Page
    {
        CurrencyExchangeService cs = new CurrencyExchangeService();
        public int UserId { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            //HttpCookie myCookie = Request.Cookies["UserId"];
            if (Session["UserId"] != null && this.UserId == 0)
            {
                this.UserId = Convert.ToInt32(Session["UserId"]);
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                //BindGrid();
                BindCombo();
                if (Session["CustomerId"] != null)
                {
                    int CustomerId = Convert.ToInt32(Session["CustomerId"].ToString());
                    SelectCustomer(CustomerId);
                    Session["CustomerId"] = null;

                }
            }
        }
        public void BindCombo()
        {
            CompanyService cs = new CompanyService();
           // HttpCookie UserStoreCookie = Request.Cookies["UserStoreId"];
            List<Model.CompanyStore> lstCompanyStores = cs.CompanyStores.ToList().Where(p => p.Id == Convert.ToInt32(Session["UserStoreId"])).ToList();
            ddlShopStore.DataSource = lstCompanyStores;
            ddlShopStore.DataTextField = "Address";
            ddlShopStore.DataValueField = "Id";
            //lstCompanyStores.Insert(0, new Model.CompanyStore { Id = 0, Address = "--Select--" });
            ddlShopStore.DataBind();

            CurrencyExchangeService ces = new CurrencyExchangeService();
            List<Model.CurrencyTypeMaster> lstCurrencyType = ces.CurrencyTypeMasters.ToList();
            ddlCurrencyType.DataSource = lstCurrencyType;
            ddlCurrencyType.DataTextField = "Name";
            ddlCurrencyType.DataValueField = "Code";
            lstCurrencyType.Insert(0, new Model.CurrencyTypeMaster { Id = 0, Name = "--Select--" });
            ddlCurrencyType.DataBind();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        private void ClearTextBoxes(ControlCollection cc)
        {
            foreach (Control ctrl in cc)
            {
                TextBox tb = ctrl as TextBox;
                if (tb != null)
                    tb.Text = "";
                DropDownList tb1 = ctrl as DropDownList;
                if (tb1 != null)
                    tb1.SelectedIndex = 0;
                else
                    ClearTextBoxes(ctrl.Controls);
            }
        }
        private void BindGrid()
        {
            CustomerService cs = new CustomerService();
            List<CustomerMaster> lst = new List<CustomerMaster>();
            if (txtSearchName.Text == string.Empty && txtSearchLastName.Text == string.Empty && txtSearchSINNumber.Text == string.Empty && txtSearchId.Text == string.Empty && txtSearchPhoneNumber.Text == string.Empty) { }
          
            else
            {
                //return;
                lst = cs.CustomerMasters.ToList();
            }
            if (txtSearchName.Text != string.Empty)
            {
                lst = lst.Where(p => p.FirstName.ToLower().StartsWith(txtSearchName.Text.ToLower())).ToList();
            }
            if (txtSearchLastName.Text != string.Empty)
            {
                lst = lst.Where(p => p.LastName.ToLower().StartsWith(txtSearchLastName.Text.ToLower())).ToList();
            }
            if (txtSearchId.Text != string.Empty)
            {
                lst = lst.Where(p => p.Id == Convert.ToInt32(txtSearchId.Text)).ToList();
            }
            if (txtSearchSINNumber.Text != string.Empty)
            {
                lst = lst.Where(p => p.SocialSecurityNumber.ToLower() == txtSearchSINNumber.Text.ToLower()).ToList();
            }
            if (txtSearchPhoneNumber.Text != string.Empty)
            {
                lst = lst.Where(p => p.HomePhone.ToLower().StartsWith(txtSearchPhoneNumber.Text.ToLower()) || p.WorkPhone.ToLower().StartsWith(txtSearchPhoneNumber.Text.ToLower()) || p.CellPhone.ToLower().StartsWith(txtSearchPhoneNumber.Text.ToLower())).ToList();
            }
            dgvCustomer.DataSource = lst;
            dgvCustomer.DataBind();
        }
        protected void dgvCustomer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                int CustomerId = Convert.ToInt32(e.CommandArgument);
                SelectCustomer(CustomerId);
            }
        }
        public void SelectCustomer(int CustomerId)
        {
            CustomerService cs = new CustomerService();
            List<CustomerMaster> cm = cs.CustomerMasters.ToList().Where(p => p.Id == CustomerId).ToList();
            hdnCustomerId.Value = CustomerId.ToString();
            mvView.ActiveViewIndex = 1;
            rptCustomer.DataSource = cm;
            rptCustomer.DataBind();
            hdnId.Value = "0";
            ClearTextBoxes(this.Controls);
            txtServiceCharge.Text = "2";
            rbtnlstTransactionType.SelectedValue = "Buy";
        }
        protected void dgvCustomer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvCustomer.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void dgvCustomer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            mvView.ActiveViewIndex = 0;
            BindGrid();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            CashLoanShop.Model.CurrencyExchange cm = cs.CurrencyExchanges.ToList().Where(p => p.Id == Convert.ToInt32(hdnId.Value)).FirstOrDefault();
            if (cm == null)
            {
                cm = new CashLoanShop.Model.CurrencyExchange();
                cm.CreatedDate = ConvertEasternTime(DateTime.Now);
                cm.CreatedBy = this.UserId;
            }
            cm.DueAmount = Convert.ToDecimal(hdnDueAmount.Value);

            cm.CustomerId = Convert.ToInt32(hdnCustomerId.Value);
            cm.CurrencyType = ddlCurrencyType.SelectedItem.Text.ToString();
            cm.ExchangeAmount = Convert.ToDecimal(txtAmounttoExchange.Text);
            cm.ExchangeRate = Convert.ToDecimal(txtExchangeRate.Text);
            cm.ServiceCharge = Convert.ToDecimal(txtServiceCharge.Text);
            cm.TransactionType = rbtnlstTransactionType.SelectedValue.ToString();
            cm.ConvertedAmount = Convert.ToDecimal(cm.ExchangeAmount * cm.ExchangeRate);
            cm.ShopStoreId = Convert.ToInt32(ddlShopStore.SelectedValue);

            cs.CurrencyExchange_InsertOrUpdate(cm);

            //BindCurrencyExchangeData();
            string Url = "/CurrencyExchangeReceipt.aspx?Id=" + cm.Id.ToString();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "runprint1", " window.open('" + Url + "', '_blank', 'width=800,height=550,location=no,left=200px');", true);
            ClearTextBoxes(this.Controls);
            mvView.ActiveViewIndex = 0;
            BindGrid();
            //Response.Redirect("~/CustomerCurrencyExchange.aspx");
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
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Customer.aspx?mode=Add&returnurl=CustomerCurrencyExchange.aspx");
        }
        protected void btnSearchCashEntryData_Click(object sender, EventArgs e)
        {
            BindCurrencyExchangeData();
        }

        protected void dgvCashEntry_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                int CurrencyExchangeId = Convert.ToInt32(e.CommandArgument);
                CurrencyExchangeService cc = new CurrencyExchangeService();
                Model.CurrencyExchange objcc = cc.CurrencyExchanges.ToList().Where(p => p.Id == CurrencyExchangeId).FirstOrDefault();
                if (objcc != null)
                {
                    CustomerService cs = new CustomerService();
                    List<CustomerMaster> cm = cs.CustomerMasters.ToList().Where(p => p.Id == objcc.CustomerId).ToList();
                    hdnCustomerId.Value = objcc.CustomerId.ToString();
                    mvView.ActiveViewIndex = 1;
                    rptCustomer.DataSource = cm;
                    rptCustomer.DataBind();
                    txtAmounttoExchange.Text = objcc.ExchangeAmount.ToString();
                    txtExchangeRate.Text = objcc.ExchangeRate.ToString();
                    txtServiceCharge.Text = objcc.ServiceCharge.ToString();
                    rbtnlstTransactionType.SelectedValue = objcc.TransactionType.ToString();
                    ddlCurrencyType.SelectedIndex = ddlCurrencyType.Items.IndexOf(ddlCurrencyType.Items.FindByText(objcc.CurrencyType.ToString()));
                    ddlShopStore.SelectedIndex = ddlShopStore.Items.IndexOf(ddlShopStore.Items.FindByValue(objcc.ShopStoreId.ToString()));

                    hdnId.Value = CurrencyExchangeId.ToString();
                }
            }
        }

        protected void dgvCashEntry_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvCashEntry.PageIndex = e.NewPageIndex;
            BindCurrencyExchangeData();
        }

        protected void dgvCashEntry_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        protected void btnCustomerSearch_Click(object sender, EventArgs e)
        {
            mvView.ActiveViewIndex = 0;
        }

        protected void btnViewTransaction_Click(object sender, EventArgs e)
        {
            BindCurrencyExchangeData();
        }

        public void BindCurrencyExchangeData()
        {
            CurrencyExchangeService cs = new CurrencyExchangeService();
            List<Model.CurrencyExchange> lst = cs.CurrencyExchangesGriddata.ToList();

            if (ddlSearchExchangeType.SelectedValue != "0")
            {
                lst = lst.Where(p => p.TransactionType.ToLower() == ddlSearchExchangeType.SelectedValue.ToLower()).ToList();
            }
            if (txtSearchCustomerName.Text != string.Empty)
            {
                lst = lst.Where(p => p.CustomerName.ToLower() == txtSearchCustomerName.Text.ToLower()).ToList();
            }
            dgvCashEntry.DataSource = lst;
            dgvCashEntry.DataBind();
            mvView.ActiveViewIndex = 2;
        }
        public string GetProvince(int Id)
        {
            switch (Id)
            {
                case 1:
                    return "Alberta";

                case 2:
                    return "British Columbia";
                case 3:
                    return "Manitoba";
                case 4:
                    return "New Brunswick";
                case 5:
                    return "Newfoundland";
                case 6:
                    return "Nova Scotia";
                case 7:
                    return "Northwest Territories";
                case 8:
                    return "Ontario";
                case 9:
                    return "Prince Edward Island";
                case 10:
                    return "Quebec";
                case 11:
                    return "Saskatchewan";
                case 12:
                    return "Yukon";
                case 13:
                    return "Other";
                default:
                    return "Not Selected";

            }
        }
    }
}