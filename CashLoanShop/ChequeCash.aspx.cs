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
    public partial class ChequeCash : System.Web.UI.Page
    {
        CashChequeService cs = new CashChequeService();
        CompanyService cms = new CompanyService();
        public int UserId { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
           // HttpCookie myCookie = Request.Cookies["UserId"];

            // Read the cookie information and display it.
            //if (myCookie != null)

            if (Session["UserId"] != null && this.UserId == 0)
            {
                this.UserId = Convert.ToInt32(Session["UserId"].ToString());
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
                if (Session["CompanyId"] != null)
                {
                    int CompanyId = Convert.ToInt32(Session["CompanyId"].ToString());
                    Model.Company cm = cms.Companys.ToList().Where(p => p.Id == CompanyId).FirstOrDefault();
                    if (cm != null)
                    {
                        lblCompany.Text = "Issuer Name:  " + cm.Name;
                        hdnCompanyId.Value = cm.Id.ToString();
                        Session["CompanyId"] = null;

                    }
                }

                //CustomerService cs = new CustomerService();
                //CashLoanShop.Model.CustomMessage cmm = cs.CustomMessages.ToList().Where(p => p.Id == Convert.ToInt32(1)).FirstOrDefault();
                //if (cmm.Message != string.Empty)
                //{
                //    data.Style.Add(HtmlTextWriterStyle.Display, "");
                //    lblMessage.Text = cmm.Message;
                //}
                //else
                //{
                //    data.Style.Add(HtmlTextWriterStyle.Display, "none");
                //    lblMessage.Text = "";
                //}

            }
        }
        public void BindCombo()
        {

            //List<Model.Company> lstCompany = cs.Companys.ToList();
            //ddlCompany.DataSource = lstCompany;
            //ddlCompany.DataTextField = "Name";
            //ddlCompany.DataValueField = "Id";
            //lstCompany.Insert(0, new Model.Company { Id = 0, Name = "--Select--" });
            //ddlCompany.DataBind();

            //List<Model.CompanyStore> lstCompanyStores = cms.CompanyStores.ToList();
            //HttpCookie UserStoreCookie = Request.Cookies["UserStoreId"];
            List<Model.CompanyStore> lstCompanyStores = cms.CompanyStores.ToList().Where(p => p.Id == Convert.ToInt32(Session["UserStoreId"].ToString())).ToList();

            ddlShopStore.DataSource = lstCompanyStores;
            ddlShopStore.DataTextField = "Name";
            ddlShopStore.DataValueField = "Id";
            //lstCompanyStores.Insert(0, new Model.CompanyStore { Id = 0, Address = "--Select--" });
            ddlShopStore.DataBind();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
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
        public string GetRelativeRootPath()
        {
            string Port = System.Web.HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
            if (Port == null || Port == "80" || Port == "443")
                Port = "";
            else
                Port = ":" + Port;

            string Protocol = System.Web.HttpContext.Current.Request.ServerVariables["SERVER_PORT_SECURE"];
            if (Protocol == null || Protocol == "0")
                Protocol = "http://";
            else
                Protocol = "https://";

            string appPath = System.Web.HttpContext.Current.Request.ApplicationPath;
            if (appPath == "/")
                appPath = "";

            //string sOut = Protocol + System.Web.HttpContext.Current.Request.ServerVariables["HTTP_HOST"] + appPath;
            string sOut = Protocol + System.Web.HttpContext.Current.Request.ServerVariables["HTTP_HOST"];
            //sOut = sOut.Replace("/booksforyou", "");
            return sOut;
        }
        public void SelectCustomer(int CustomerId)
        {
            CustomerService cs = new CustomerService();
            List<CustomerMaster> cm = cs.CustomerMastersById(CustomerId).ToList();
            if (!string.IsNullOrEmpty(cm.FirstOrDefault().ImageName))
            {
                imgProof.ImageUrl = GetRelativeRootPath() + "/ProofofIdentity/" + cm.FirstOrDefault().ImageName;
                // aimage.HRef = GetRelativeRootPath() + "/ProofofIdentity/" + cm.FirstOrDefault().ImageName;
            }
            else {
                imgProof.ImageUrl = GetRelativeRootPath() + "/ProofofIdentity/NoImage.png";
            }

            hdnCustomerId.Value = CustomerId.ToString();
            mvView.ActiveViewIndex = 1;
            rptCustomer.DataSource = cm;
            rptCustomer.DataBind();
            hdnId.Value = "0";
            hdnCompanyId.Value = "0";
            lblCompany.Text = "";
            ClearTextBoxes(this.Controls);
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
            //CashLoanShop.Model.CashCheque cm = cs.CashCheques.ToList().Where(p => p.Id == Convert.ToInt32(hdnId.Value)).FirstOrDefault();
            CashLoanShop.Model.CashCheque cm = null;
            if (cm == null)
            {
               // HttpCookie myCookie = Request.Cookies["UserId"];
                // Read the cookie information and display it.
                if (Session["UserId"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                else
                {
                    this.UserId = Convert.ToInt32(Session["UserId"].ToString());
                }
                cm = new CashLoanShop.Model.CashCheque();
                cm.CreatedDate = ConvertEasternTime(DateTime.Now);
                cm.CreatedBy = this.UserId;
            }
            cm.AdminFee = Convert.ToDecimal(hdnAdminCharge.Value);
            cm.AmountIssued = Convert.ToDecimal(hdnTotalValue.Value);
            cm.Charges = Convert.ToDecimal(hdnCharge.Value);
            cm.ChequeAmount = Convert.ToDecimal(txtChequeAmount.Text);
            cm.ChequeIssuerId = Convert.ToInt32(hdnCompanyId.Value);
            cm.ChequeNumber = txtChequeNumber.Text;
            cm.ChequeType = ddlChequeType.SelectedValue;
            cm.CustomerId = Convert.ToInt32(hdnCustomerId.Value);
            cm.ShopStoreId = Convert.ToInt32(ddlShopStore.SelectedValue);
            if (cm.ChequeType == "Custom")
            {
                cm.CustomPercentage = Convert.ToDecimal(txtCustomPer.Text == "" ? "0" : txtCustomPer.Text);
            }
            cs.CashCheque_InsertOrUpdate(cm);

            //BindCashEntryData();
            string Url = "/ChequeCashReceipt.aspx?Id=" + cm.Id.ToString();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "runprint", " window.open('" + Url + "', '_blank', 'width=800,height=550,location=no,left=200px');", true);
            mvView.ActiveViewIndex = 0;
        }
        public void BindCashEntryData()
        {
            CashChequeService cs = new CashChequeService();
            List<CashCheque> lst = cs.CashChequeGriddata.ToList();
            if (txtSearchChequeNumber.Text != string.Empty)
            {
                lst = lst.Where(p => p.ChequeNumber.ToLower().StartsWith(txtSearchChequeNumber.Text.ToLower())).ToList();
            }
            if (txtSearchCompanyName.Text != string.Empty)
            {
                lst = lst.Where(p => p.CompanyName.ToLower() == txtSearchCompanyName.Text.ToLower()).ToList();
            }
            if (txtSearchCustomerName.Text != string.Empty)
            {
                lst = lst.Where(p => p.CustomerName.ToLower() == txtSearchCustomerName.Text.ToLower()).ToList();
            }
            dgvCashEntry.DataSource = lst;
            dgvCashEntry.DataBind();
            mvView.ActiveViewIndex = 2;
        }

        protected void btnSearchCashEntryData_Click(object sender, EventArgs e)
        {
            BindCashEntryData();
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Customer.aspx?mode=Add&returnurl=ChequeCash.aspx");
        }

        protected void dgvCashEntry_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                int CashChequeId = Convert.ToInt32(e.CommandArgument);
                CashChequeService cc = new CashChequeService();
                CashCheque objcc = cc.CashChequesbyId(CashChequeId).FirstOrDefault();
                if (objcc != null)
                {
                    CustomerService cs = new CustomerService();
                    List<CustomerMaster> cm = cs.CustomerMastersById(objcc.CustomerId).ToList();
                    hdnCustomerId.Value = objcc.CustomerId.ToString();
                    mvView.ActiveViewIndex = 1;
                    rptCustomer.DataSource = cm;
                    rptCustomer.DataBind();
                    txtChequeAmount.Text = objcc.ChequeAmount.ToString();
                    txtChequeNumber.Text = objcc.ChequeNumber.ToString();

                    //ddlCompany.SelectedIndex = ddlCompany.Items.IndexOf(ddlCompany.Items.FindByValue(objcc.ChequeIssuerId.ToString()));

                    ddlChequeType.SelectedIndex = ddlChequeType.Items.IndexOf(ddlChequeType.Items.FindByValue(objcc.ChequeType));
                    ddlShopStore.SelectedIndex = ddlShopStore.Items.IndexOf(ddlShopStore.Items.FindByValue(objcc.ShopStoreId.ToString()));
                    lblTotalValue.Text = objcc.AmountIssued.ToString() + "  (Charges: " + objcc.Charges.ToString() + " + Admin Fee: " + objcc.AdminFee.ToString() + ")";

                    hdnId.Value = CashChequeId.ToString();
                }
            }
        }

        protected void dgvCashEntry_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvCashEntry.PageIndex = e.NewPageIndex;
            BindCashEntryData();
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
            BindCashEntryData();
        }

        protected void btnSearchCompany_Click(object sender, EventArgs e)
        {
            Session["CustomerId"] = hdnCustomerId.Value.ToString();
            Response.Redirect("~/Company.aspx?mode=search&returnurl=ChequeCash.aspx");
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