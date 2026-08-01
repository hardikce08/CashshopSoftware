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
    public partial class TermLoan : System.Web.UI.Page
    {
        public int UserId { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
           // HttpCookie myCookie = Request.Cookies["UserId"];
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
                //using (CustomerService cs = new CustomerService())
                //{
                //    CashLoanShop.Model.CustomMessage cmm = cs.CustomMessages.ToList().Where(p => p.Id == Convert.ToInt32(1)).FirstOrDefault();
                //    if (cmm.Message != string.Empty)
                //    {
                //        datanew.Style.Add(HtmlTextWriterStyle.Display, "");
                //        // lblMessage.Text = cmm.Message;
                //    }
                //    else
                //    {
                //        datanew.Style.Add(HtmlTextWriterStyle.Display, "none");
                //        // lblMessage.Text = "";
                //    }
                //}
            }
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

                rbtnInterestRate.Items[0].Text = Convert.ToInt32(lstCompanyStores[0].TermLateInterestRate).ToString() + "% Late Interest Rate";
                rbtnInterestRate.Items[0].Value = Convert.ToInt32(lstCompanyStores[0].TermLateInterestRate).ToString();

                hdnPercentage.Value = (Convert.ToDecimal(lstCompanyStores[0].TermInterestRate) + Convert.ToDecimal(lstCompanyStores[0].AdminFeePercentage)).ToString();
                hdnMaxLimit.Value = lstCompanyStores[0].MaximumTermLoanAmount.ToString();
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
            Response.Redirect("~/Customer.aspx?mode=Add&returnurl=TermLoan.aspx");
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
                txtInstallment2Date.Enabled = false;
                //txtComments.Text = cm.FirstOrDefault().Comments;
                using (TermLoanService cls = new TermLoanService())
                {
                    Model.CustomerTermLoan objcc = cls.CustomerTermLoansbyCustomerId(CustomerId).Where(p => p.LoanStatus != "Close" && p.LoanStatus != "Denied" && p.LoanStatus != "Cancelled").OrderByDescending(p => p.Id).FirstOrDefault();
                    if (objcc != null)
                    {
                        datanew.Style.Add(HtmlTextWriterStyle.Display, "none");
                        exist.Style.Add(HtmlTextWriterStyle.Display, "");
                        lblcustomername.Text = cm.FirstOrDefault().FirstName + " " + cm.FirstOrDefault().LastName;
                        lblLoanStatus.Text = "Loan " + objcc.LoanStatus;
                        using (CompanyService cmp = new CompanyService())
                        {
                            Model.CompanyStore CompanyStores = cmp.CompanyStoresbyId(objcc.ShopStoreId);
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
                }
                using (CustomerLoanService cls = new CustomerLoanService())
                {
                    List<Model.CustomerLoan> lstloan = cls.CustomerLoansbyCustomerId(CustomerId).Where(p => p.LoanStatus == "Open").ToList();
                    if (lstloan.Count > 0)
                    {
                        lblIsPAydayLoan.Text = "Yes";
                    }
                    else
                    { lblIsPAydayLoan.Text = "No"; }
                }
                //using (CustomerLoanService cls = new CustomerLoanService())
                //{
                //    List<Model.CustomerLoan> lstloan = cls.CustomerLoansbyCustomerId(CustomerId).ToList();
                //    Model.CustomerLoan objcc = lstloan.Where(p => p.CustomerId == CustomerId && (p.LoanStatus != "Close" && p.LoanStatus != "Cancelled")).OrderByDescending(p => p.Id).FirstOrDefault();
                //    int count = lstloan.Where(p => p.CustomerId == CustomerId && p.CreatedDate >= ConvertEasternTime(DateTime.Now).AddDays(-63).Date && (p.LoanStatus != "Cancelled")).ToList().Count;
                //    //hdnLoanCount.Value = count.ToString();
                //    if (objcc != null)
                //    {
                //        if (objcc.LoanStatus == "Denied")
                //        {
                //            CustomerLoanService css = new CustomerLoanService();
                //            CustomerLoanException cl = css.CustomerLoanExceptions.ToList().Where(p => p.CustomerId == objcc.CustomerId && p.LoanId == objcc.Id).FirstOrDefault();
                //            if (cl != null)
                //            {
                //                datanew.Style.Add(HtmlTextWriterStyle.Display, "");
                //                exist.Style.Add(HtmlTextWriterStyle.Display, "none");
                //            }
                //            else
                //            {
                //                datanew.Style.Add(HtmlTextWriterStyle.Display, "none");
                //                exist.Style.Add(HtmlTextWriterStyle.Display, "");
                //                lblcustomername.Text = cm.FirstOrDefault().FirstName + " " + cm.FirstOrDefault().LastName;
                //                lblLoanStatus.Text = "Loan " + objcc.LoanStatus;
                //                CompanyService cmp = new CompanyService();
                //                Model.CompanyStore CompanyStores = cmp.CompanyStores.Where(p => p.Id == objcc.ShopStoreId).FirstOrDefault();
                //                if (CompanyStores != null)
                //                {
                //                    lblStoreInfo.Text = CompanyStores.Name + "<br/>" + CompanyStores.Address.Replace(",", "<br/>").Replace("$", " , ") + "<br/>" + CompanyStores.PhoneNo + "<br/>" + CompanyStores.Email;
                //                }
                //            }
                //        }
                //        else
                //        {
                //            datanew.Style.Add(HtmlTextWriterStyle.Display, "none");
                //            exist.Style.Add(HtmlTextWriterStyle.Display, "");
                //            lblcustomername.Text = cm.FirstOrDefault().FirstName + " " + cm.FirstOrDefault().LastName;
                //            lblLoanStatus.Text = "Loan " + objcc.LoanStatus;
                //            CompanyService cmp = new CompanyService();
                //            Model.CompanyStore CompanyStores = cmp.CompanyStores.Where(p => p.Id == objcc.ShopStoreId).FirstOrDefault();
                //            if (CompanyStores != null)
                //            {
                //                lblStoreInfo.Text = CompanyStores.Name + "<br/>" + CompanyStores.Address.Replace(",", "<br/>").Replace("$", " , ") + "<br/>" + CompanyStores.PhoneNo + "<br/>" + CompanyStores.Email;
                //            }
                //        }

                //    }
                //    else
                //    {
                //        datanew.Style.Add(HtmlTextWriterStyle.Display, "");
                //        exist.Style.Add(HtmlTextWriterStyle.Display, "none");

                //    }
                //    txtInstallment1Date.Enabled = false;
                //    txtInstallment2Date.Enabled = false;
                //    //ddlLoanType.Items.Add(new ListItem { Text = "--Select--", Value = "0", Selected = true });
                //    if (count >= 2)
                //    {
                //        dvIsMoreloan.Style.Add(HtmlTextWriterStyle.Display, "");
                //        ddlLoanType.Items.Add(new ListItem { Text = "3 Installment", Value = "3 Installment" });
                //        ddlLoanType.Items.Add(new ListItem { Text = "2 Installment", Value = "2 Installment" });
                //        ddlLoanType.Items.Add(new ListItem { Text = "1 Installment", Value = "1 Installment" });
                //    }
                //    else
                //    {
                //        dvIsMoreloan.Style.Add(HtmlTextWriterStyle.Display, "none");
                //        ddlLoanType.Items.Add(new ListItem { Text = "1 Installment", Value = "1 Installment" });
                //    }


                //}
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            mvView.ActiveViewIndex = 0;
            BindGrid();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            using (TermLoanService ts = new TermLoanService())
            {
                //HttpCookie myCookie = Request.Cookies["UserId"];
                // Read the cookie information and display it.
                if (Session["UserId"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                else
                {
                    this.UserId = Convert.ToInt32(Session["UserId"]);
                }
                CashLoanShop.Model.CustomerTermLoan cm = new CashLoanShop.Model.CustomerTermLoan();
                cm.CreatedDate = ConvertEasternTime(DateTime.Now);
                cm.CreatedBy = this.UserId;
                cm.LoanStatus = "Open";
                cm.LastStatus = "Open";
                cm.DueAmount = Convert.ToDecimal(hdnDueAmount.Value);
                cm.LoanAmountApplied = Convert.ToDecimal(txtAppliedAmount.Text);
                cm.CustomerId = Convert.ToInt32(hdnCustomerId.Value);
                cm.IsLoanApproved = rbtnlstLoanType.SelectedValue.ToString() == "1" ? true : false;
                cm.ShopStoreId = Convert.ToInt32(ddlShopStore.SelectedValue);
                cm.LoanAmountApproved = Convert.ToDecimal(txtApprovedAmount.Text);
                if (cm.IsLoanApproved == false)
                {
                    cm.LoanStatus = "Denied";
                }
                using (CompanyService cmp = new CompanyService())
                {
                    Model.CompanyStore CompanyStores = cmp.CompanyStoresbyId(cm.ShopStoreId);
                    cm.AdminFee = Math.Round(Convert.ToDecimal((cm.LoanAmountApproved * (CompanyStores.AdminFeePercentage / 100))), 2);
                    cm.InterestCharge = cm.DueAmount - (cm.AdminFee + cm.LoanAmountApproved);
                }

                cm.ReportText = txtReportText.Text;
                cm.LateInterestRate = rbtnInterestRate.SelectedValue.ToString();

                cm.DueDate = Convert.ToDateTime(txtFirstInstallmentDate.Text).AddYears(Convert.ToInt32(ddlLoanTerm.SelectedValue.ToString()));
                cm.PaymentOption = ddlPaymentOption.SelectedValue.ToString();

                cm.LoanType = ddlLoanPayType.SelectedValue.ToString();
                cm.LoanTerm = ddlLoanTerm.SelectedValue.ToString();
                //cm.MonthlyIncome = string.IsNullOrEmpty(txtMonthlyIncome.Text) ? 0 : Convert.ToDecimal(txtMonthlyIncome.Text);
                if (!string.IsNullOrEmpty(txtFirstInstallmentDate.Text))
                    cm.FirstInstallmentDate = Convert.ToDateTime(txtFirstInstallmentDate.Text);
                if (!string.IsNullOrEmpty(txtInstallment2Date.Text))
                    cm.SecondInstallmentDate = Convert.ToDateTime(txtInstallment2Date.Text);

                System.Data.DataSet ds = ts.GetPaymentSchedule(Convert.ToDateTime(cm.FirstInstallmentDate), (!cm.SecondInstallmentDate.HasValue ? Convert.ToDateTime(DateTime.Now) : Convert.ToDateTime(cm.SecondInstallmentDate)), cm.DueDate, cm.LoanType, Convert.ToDecimal(cm.DueAmount));
                cm.InstallmentAmount = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[0][2].ToString()), 2);
                cm.LateInterestCharge = 0;
                cm.NSFCharge = 0;
                cm.NoofInstallment = ds.Tables[0].Rows.Count;
                ts.CustomerTermLoan_InsertOrUpdate(cm);
                if (cm.LoanStatus == "Open")
                {
                    string Url = "/TermLoanContract.aspx?Id=" + cm.Id.ToString();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "runtermloanprint", " window.open('" + Url + "', '_blank', 'width=800,height=550,location=no,left=200px');", true);
                    ClearTextBoxes(this.Controls);
                }
                //Make Schedule Entry in TAble
                ts.InsertPaymentScheduleByLoanId(cm.Id);
                mvView.ActiveViewIndex = 0;
                BindGrid();
            }

        }
    }
}