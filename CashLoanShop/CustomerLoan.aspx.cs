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
    public partial class CustomerLoan : System.Web.UI.Page
    {

        public int UserId { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

            //HttpCookie myCookie = Request.Cookies["UserId"];
            // Read the cookie information and display it.
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
                using (CustomerService cs = new CustomerService())
                {
                    CashLoanShop.Model.CustomMessage cmm = cs.CustomMessages.ToList().Where(p => p.Id == Convert.ToInt32(1)).FirstOrDefault();
                    if (cmm.Message != string.Empty)
                    {
                        datanew.Style.Add(HtmlTextWriterStyle.Display, "");
                        // lblMessage.Text = cmm.Message;
                    }
                    else
                    {
                        datanew.Style.Add(HtmlTextWriterStyle.Display, "none");
                        // lblMessage.Text = "";
                    }
                }
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
        public void BindCombo()
        {
            using (CompanyService cs = new CompanyService())
            {
                //HttpCookie UserStoreCookie = Request.Cookies["UserStoreId"];
                List<Model.CompanyStore> lstCompanyStores = cs.CompanyStores.ToList().Where(p => p.Id == Convert.ToInt32(Session["UserStoreId"])).ToList();
                if (lstCompanyStores.Count > 0)
                {
                    lstCompanyStores[0].Address = lstCompanyStores[0].Address.Replace("$", " - ");
                }
                ddlShopStore.DataSource = lstCompanyStores;
                ddlShopStore.DataTextField = "Address";
                ddlShopStore.DataValueField = "Id";
                //lstCompanyStores.Insert(0, new Model.CompanyStore { Id = 0, Address = "--Select--" });
                ddlShopStore.DataBind();

                rbtnInterestRate.Items[0].Text = Convert.ToInt32(lstCompanyStores[0].InterestRate).ToString() + "% Late Interest Rate";
                rbtnInterestRate.Items[0].Value = Convert.ToInt32(lstCompanyStores[0].InterestRate).ToString();
            }
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
            using (CustomerService cs = new CustomerService())
            {
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
            using (CustomerService cs = new CustomerService())
            {
                List<CustomerMaster> cm = cs.CustomerMastersById(CustomerId).ToList();
                hdnCustomerId.Value = CustomerId.ToString();
                mvView.ActiveViewIndex = 1;
                rptCustomer.DataSource = cm;
                rptCustomer.DataBind();
                //bind bank info
                List<CustomerBankInformation> cb = cs.CustomerBankInformationsByCustomerId(CustomerId).ToList();
                rptBakInformation.DataSource = cb;
                rptBakInformation.DataBind();
                //

                if (!string.IsNullOrEmpty(cm.FirstOrDefault().ImageName))
                {
                    imgProof.ImageUrl = GetRelativeRootPath() + "/ProofofIdentity/" + cm.FirstOrDefault().ImageName;
                    //aimage.HRef = GetRelativeRootPath() + "/ProofofIdentity/" + cm.FirstOrDefault().ImageName;
                }
                else
                {
                    imgProof.ImageUrl = GetRelativeRootPath() + "/ProofofIdentity/NoImage.png";
                }
                hdnId.Value = "0";
                ClearTextBoxes(this.Controls);

                txtComments.Text = cm.FirstOrDefault().Comments;

                using (CustomerLoanService cls = new CustomerLoanService())
                {
                    List<Model.CustomerLoan> lstloan = cls.CustomerLoansbyCustomerId(CustomerId).ToList();
                    Model.CustomerLoan objcc = lstloan.Where(p => p.CustomerId == CustomerId && (p.LoanStatus != "Close" && p.LoanStatus != "Soft Close" && p.LoanStatus != "Cancelled")).OrderByDescending(p => p.Id).FirstOrDefault();
                    Model.CustomerLoan objccopen = lstloan.Where(p => p.CustomerId == CustomerId && p.LoanStatus == "Open").FirstOrDefault();

                    int count = lstloan.Where(p => p.CustomerId == CustomerId && p.CreatedDate >= ConvertEasternTime(DateTime.Now).AddDays(-63).Date && (p.LoanStatus != "Cancelled")).ToList().Count;
                    hdnLoanCount.Value = count.ToString();
                    if (objccopen != null)
                    {
                        datanew.Style.Add(HtmlTextWriterStyle.Display, "none");
                        exist.Style.Add(HtmlTextWriterStyle.Display, "");
                        lblcustomername.Text = cm.FirstOrDefault().FirstName + " " + cm.FirstOrDefault().LastName;
                        lblLoanStatus.Text = "Loan " + objcc.LoanStatus;
                        CompanyService cmp = new CompanyService();
                        Model.CompanyStore CompanyStores = cmp.CompanyStores.Where(p => p.Id == objcc.ShopStoreId).FirstOrDefault();
                        if (CompanyStores != null)
                        {
                            lblStoreInfo.Text = CompanyStores.Name + "<br/>" + CompanyStores.Address.Replace(",", "<br/>").Replace("$", " , ") + "<br/>" + CompanyStores.PhoneNo + "<br/>" + CompanyStores.Email;
                        }
                        return;
                    }
                    if (objcc != null)
                    {
                        if (objcc.LoanStatus == "Denied")
                        {
                            CustomerLoanService css = new CustomerLoanService();
                            CustomerLoanException cl = css.CustomerLoanExceptions.ToList().Where(p => p.CustomerId == objcc.CustomerId && p.LoanId == objcc.Id).FirstOrDefault();
                            if (cl != null)
                            {
                                datanew.Style.Add(HtmlTextWriterStyle.Display, "");
                                exist.Style.Add(HtmlTextWriterStyle.Display, "none");
                            }
                            else
                            {
                                datanew.Style.Add(HtmlTextWriterStyle.Display, "none");
                                exist.Style.Add(HtmlTextWriterStyle.Display, "");
                                lblcustomername.Text = cm.FirstOrDefault().FirstName + " " + cm.FirstOrDefault().LastName;
                                lblLoanStatus.Text = "Loan " + objcc.LoanStatus;
                                CompanyService cmp = new CompanyService();
                                Model.CompanyStore CompanyStores = cmp.CompanyStores.Where(p => p.Id == objcc.ShopStoreId).FirstOrDefault();
                                if (CompanyStores != null)
                                {
                                    lblStoreInfo.Text = CompanyStores.Name + "<br/>" + CompanyStores.Address.Replace(",", "<br/>").Replace("$", " , ") + "<br/>" + CompanyStores.PhoneNo + "<br/>" + CompanyStores.Email;
                                }
                            }
                        }
                        else
                        {
                            datanew.Style.Add(HtmlTextWriterStyle.Display, "none");
                            exist.Style.Add(HtmlTextWriterStyle.Display, "");
                            lblcustomername.Text = cm.FirstOrDefault().FirstName + " " + cm.FirstOrDefault().LastName;
                            lblLoanStatus.Text = "Loan " + objcc.LoanStatus;
                            CompanyService cmp = new CompanyService();
                            Model.CompanyStore CompanyStores = cmp.CompanyStores.Where(p => p.Id == objcc.ShopStoreId).FirstOrDefault();
                            if (CompanyStores != null)
                            {
                                lblStoreInfo.Text = CompanyStores.Name + "<br/>" + CompanyStores.Address.Replace(",", "<br/>").Replace("$", " , ") + "<br/>" + CompanyStores.PhoneNo + "<br/>" + CompanyStores.Email;
                            }
                        }

                    }
                    else
                    {
                        datanew.Style.Add(HtmlTextWriterStyle.Display, "");
                        exist.Style.Add(HtmlTextWriterStyle.Display, "none");

                    }
                    txtInstallment1Date.Enabled = false;
                    txtInstallment2Date.Enabled = false;
                    //ddlLoanType.Items.Add(new ListItem { Text = "--Select--", Value = "0", Selected = true });
                    if (count >= 2)
                    {
                        dvIsMoreloan.Style.Add(HtmlTextWriterStyle.Display, "");
                        ddlLoanType.Items.Add(new ListItem { Text = "3 Installment", Value = "3 Installment" });
                        ddlLoanType.Items.Add(new ListItem { Text = "2 Installment", Value = "2 Installment" });
                        ddlLoanType.Items.Add(new ListItem { Text = "1 Installment", Value = "1 Installment" });
                    }
                    else
                    {
                        dvIsMoreloan.Style.Add(HtmlTextWriterStyle.Display, "none");
                        ddlLoanType.Items.Add(new ListItem { Text = "1 Installment", Value = "1 Installment" });
                    }


                }
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
            using (CustomerLoanService cls = new CustomerLoanService())
            {
                CashLoanShop.Model.CustomerLoan cm = cls.CustomerLoansbyId(Convert.ToInt32(hdnId.Value)).FirstOrDefault();
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
                        this.UserId = Convert.ToInt32(Session["UserId"]);
                    }
                    cm = new CashLoanShop.Model.CustomerLoan();
                    cm.CreatedDate = ConvertEasternTime(DateTime.Now);
                    cm.CreatedBy = this.UserId;
                    cm.LoanStatus = "Open";
                }
                cm.DueAmount = Convert.ToDecimal(hdnDueAmount.Value);
                cm.LoanAmountApplied = Convert.ToDecimal(txtAppliedAmount.Text);
                cm.CustomerId = Convert.ToInt32(hdnCustomerId.Value);
                cm.IsLoanApproved = rbtnlstLoanType.SelectedValue.ToString() == "1" ? true : false;
                if (cm.IsLoanApproved == false)
                {
                    cm.LoanStatus = "Denied";
                }
                cm.AdminFee = Convert.ToDecimal(hdnCharge.Value);
                cm.LateInterestRate = rbtnInterestRate.SelectedValue.ToString();
                cm.LoanAmountApproved = Convert.ToDecimal(txtLoanAmount.Text);
                cm.NextPayDate = Convert.ToDateTime(txtNextPayDate.Text);
                cm.PaymentOption = ddlPaymentOption.SelectedValue.ToString();
                cm.ShopStoreId = Convert.ToInt32(ddlShopStore.SelectedValue);
                cm.LoanPaymentType = ddlLoanType.SelectedValue.ToString();
                cm.MonthlyIncome = string.IsNullOrEmpty(txtMonthlyIncome.Text) ? 0 : Convert.ToDecimal(txtMonthlyIncome.Text);
                if (!string.IsNullOrEmpty(txtInstallment1Date.Text))
                    cm.Installment1Date = Convert.ToDateTime(txtInstallment1Date.Text);
                if (!string.IsNullOrEmpty(txtInstallment2Date.Text))
                    cm.Installment2Date = Convert.ToDateTime(txtInstallment2Date.Text);
                cm.Last63DaysLoanCount = Convert.ToInt32(hdnLoanCount.Value);
                if (Convert.ToInt32(hdnLoanCount.Value) >= 2)
                {
                    if (cm.LoanPaymentType == "3 Installment")
                    {
                        cm.Installment1Amount = Math.Round((cm.DueAmount / 3), 2);
                        cm.Installment2Amount = Math.Round((cm.DueAmount / 3), 2);
                        cm.Installment3Amount = Math.Round((cm.DueAmount / 3), 2);
                    }
                    else if (cm.LoanPaymentType == "2 Installment")
                    {
                        cm.Installment1Amount = Math.Round((cm.DueAmount / 2), 2);
                        cm.Installment2Amount = Math.Round((cm.DueAmount / 2), 2);
                    }
                    else
                    { cm.Installment1Amount = cm.DueAmount; }
                }
                else
                {
                    cm.Installment1Amount = cm.DueAmount;
                }
                //calculateLoanType
                //int DayDiff = Convert.ToDateTime(txtNextPayDate.Text).Subtract(cm.CreatedDate).Days;
                //string status = "";
                //if (DayDiff <= 7)
                //{
                //    status = "Weekly";
                //}
                //else if (DayDiff <= 14)
                //{
                //    status = "BiWeekly";
                //}
                //else if (DayDiff > 14 && DayDiff <= 30)
                //{
                //    status = "Twice Monthly";
                //}
                //else if (DayDiff > 30)
                //{
                //    status = "Monthly";
                //}
                string loantype = "Not Selected";
                using (CustomerService css = new CustomerService())
                {
                    CustomerIncome ci = css.CutomerIncomesbyCustomerId(Convert.ToInt32(cm.CustomerId)).FirstOrDefault();
                    if (ci != null)
                    {
                        loantype = ((LoanType)Convert.ToInt32(ci.FrequencyofPay)).ToString().Replace("_", " ");
                    }
                    cm.LoanType = loantype;
                    cls.CustomerLoan_InsertOrUpdate(cm);

                    //update loan comments in customer table 
                    CustomerMaster objcustomer = css.CustomerMastersById(cm.CustomerId).FirstOrDefault();
                    if (objcustomer != null)
                    {
                        objcustomer.Comments = txtComments.Text;
                    }
                    css.CustomerMaster_InsertOrUpdate(objcustomer);
                    //BindLoanEntryData();
                    //string Url = "/CustomerLoanReceipt.aspx?Id=" + cm.Id.ToString();
                    if (cm.LoanStatus == "Open")
                    {
                        string Url = "/CustomerLoanContract.aspx?Id=" + cm.Id.ToString();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "runprint2", " window.open('" + Url + "', '_blank', 'width=800,height=550,location=no,left=200px');", true);
                        ClearTextBoxes(this.Controls);
                    }
                    mvView.ActiveViewIndex = 0;
                    BindGrid();
                    //css = null;
                }
                //else
                //{

                //}
            }

        }
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Customer.aspx?mode=Add&returnurl=CustomerLoan.aspx");
        }
        protected void btnSearchCashEntryData_Click(object sender, EventArgs e)
        {
            BindLoanEntryData();
        }

        protected void dgvCashEntry_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                int CashLoanId = Convert.ToInt32(e.CommandArgument);
                using (CustomerLoanService cc = new CustomerLoanService())
                {
                    Model.CustomerLoan objcc = cc.CustomerLoansbyId(CashLoanId).FirstOrDefault();
                    if (objcc != null)
                    {
                        CustomerService cs = new CustomerService();
                        List<CustomerMaster> cm = cs.CustomerMastersById(objcc.CustomerId).ToList();
                        hdnCustomerId.Value = objcc.CustomerId.ToString();
                        mvView.ActiveViewIndex = 1;
                        rptCustomer.DataSource = cm;
                        rptCustomer.DataBind();
                        txtAppliedAmount.Text = objcc.LoanAmountApplied.ToString();
                        txtLoanAmount.Text = objcc.LoanAmountApproved.ToString();
                        txtNextPayDate.Text = objcc.NextPayDate.ToString("dd-MMM-yyyy");
                        ddlPaymentOption.SelectedIndex = ddlPaymentOption.Items.IndexOf(ddlPaymentOption.Items.FindByValue(objcc.PaymentOption.ToString()));
                        rbtnInterestRate.SelectedValue = objcc.LateInterestRate.ToString();
                        rbtnlstLoanType.SelectedValue = Convert.ToInt32(objcc.IsLoanApproved).ToString();
                        //ddlCompany.SelectedIndex = ddlCompany.Items.IndexOf(ddlCompany.Items.FindByValue(objcc.ChequeIssuerId.ToString()));
                        //ddlChequeType.SelectedIndex = ddlChequeType.Items.IndexOf(ddlChequeType.Items.FindByValue(objcc.ChequeType));
                        ddlShopStore.SelectedIndex = ddlShopStore.Items.IndexOf(ddlShopStore.Items.FindByValue(objcc.ShopStoreId.ToString()));
                        lblTotalAmountDue.Text = objcc.DueAmount.ToString();

                        hdnId.Value = CashLoanId.ToString();

                    }
                }
            }
        }

        protected void dgvCashEntry_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvCashEntry.PageIndex = e.NewPageIndex;
            BindLoanEntryData();
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
            BindLoanEntryData();
        }

        public void BindLoanEntryData()
        {
            using (CustomerLoanService cs = new CustomerLoanService())
            {
                List<Model.CustomerLoan> lst = cs.CustomerLoanGriddata.ToList();

                if (txtSearchPaymentoption.Text != string.Empty)
                {
                    lst = lst.Where(p => p.PaymentOption.ToLower() == txtSearchPaymentoption.Text.ToLower()).ToList();
                }
                if (txtSearchCustomerName.Text != string.Empty)
                {
                    lst = lst.Where(p => p.CustomerName.ToLower() == txtSearchCustomerName.Text.ToLower()).ToList();
                }
                dgvCashEntry.DataSource = lst;
                dgvCashEntry.DataBind();
                mvView.ActiveViewIndex = 2;
            }
        }

        protected void rptCustomer_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item)
            {
                using (CustomerService cs = new CustomerService())
                {
                    CustomerMaster cm = (CustomerMaster)e.Item.DataItem;
                    CustomerIncome cmm = cs.CutomerIncomesbyCustomerId(cm.Id).FirstOrDefault();
                    if (cmm != null)
                    {
                        switch (cmm.FrequencyofPay)
                        {
                            case "0":
                                cmm.FrequencyofPay = "Not Selected";
                                break;
                            case "1":
                                cmm.FrequencyofPay = "Weekly";
                                break;
                            case "2":
                                cmm.FrequencyofPay = "Bi-Weekly";
                                break;
                            case "3":
                                cmm.FrequencyofPay = "Twice Monthly";
                                break;
                            case "4":
                                cmm.FrequencyofPay = "Monthly";
                                break;
                            case "5":
                                cmm.FrequencyofPay = "No Fix Date";
                                break;
                            default:
                                break;
                        }
                        Label lblTakeHomePay = (Label)e.Item.FindControl("lblTakeHomePay");
                        Label lblFrequencyofPay = (Label)e.Item.FindControl("lblFrequencyofPay");
                        Label lblPaymentType = (Label)e.Item.FindControl("lblPaymentType");
                        lblTakeHomePay.Text = "$" + cmm.TakehomePay.ToString();
                        lblFrequencyofPay.Text = cmm.FrequencyofPay.ToString();
                        lblPaymentType.Text = cmm.IsComputerized == true ? "Cheque Payment" : "Direct Deposit";
                    }
                }
            }
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