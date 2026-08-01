using CashLoanShop.DataAccess;
using CashLoanShop.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CashLoanShop
{
    public partial class Customer : System.Web.UI.Page
    {
        public int UserId { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               // HttpCookie myCookie = Request.Cookies["UserId"];
                if (Session["UserId"] != null && this.UserId == 0)
                {
                    this.UserId = Convert.ToInt32(Session["UserId"].ToString());
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
                if (Request.QueryString["mode"] != null)
                {
                    btnAddNew_Click(null, null);
                    hdnvalidationmode.Value = "notcustomer";
                    if (Request.QueryString["returnurl"] == "CustomerLoan.aspx")
                    {
                        hdnvalidationmode.Value = "loancustomer";
                    }
                }
                //HttpCookie myUserRoleCookie = Request.Cookies["UserRoleId"];
                if (Session["UserRoleId"] != null)
                {
                    if (Convert.ToInt32(Session["UserRoleId"].ToString()) == 4)
                    {
                        btnAddNew.Enabled = false;
                    }
                    else btnAddNew.Enabled = true;
                }
                //BindGrid();
            }
        }
        private void DisableControls(System.Web.UI.Control control)
        {
            foreach (System.Web.UI.Control c in control.Controls)
            {
                // Get the Enabled property by reflection.
                Type type = c.GetType();
                PropertyInfo prop = type.GetProperty("Enabled");

                // Set it to False to disable the control.
                if (prop != null)
                {
                    prop.SetValue(c, false, null);
                }

                // Recurse into child controls.
                if (c.Controls.Count > 0)
                {
                    this.DisableControls(c);
                }
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            using (CustomerService cs = new CustomerService())
            {
                CustomerMaster cm = cs.CustomerMastersById(Convert.ToInt32(hdnId.Value)).FirstOrDefault();
                if (cm == null)
                {
                    //HttpCookie myCookie = Request.Cookies["UserId"];
                    if (Session["UserId"] == null)
                    {
                        Response.Redirect("~/Login.aspx");
                    }
                    else
                    {
                        this.UserId = Convert.ToInt32(Session["UserId"].ToString());
                    }

                    cm = new CustomerMaster();
                    cm.Createdat = DateTime.UtcNow;
                    cm.CreatedBy = this.UserId;
                }
                cm.HomePhone = TxtHomePhone.Text;
                cm.FirstName = TxtFirstName.Text;
                cm.LastName = TxtLastName.Text;
                cm.Address = TxtAddress.Text;
                cm.CellPhone = TxtCellPhone.Text;
                cm.City = TxtCity.Text;
                cm.Comments = TxtComments.Text;
                if (!string.IsNullOrEmpty(TxtDateofbirth.Text))
                {
                    cm.Dateofbirth = Convert.ToDateTime(TxtDateofbirth.Text);
                }
                else
                {
                    cm.Dateofbirth = null;
                }
                cm.DrivingLicenseNumber = TxtDrivingLicenseNumber.Text;
                cm.DurationMonth = TxtDurationMonth.Text;
                cm.DurationYears = TxtDurationYears.Text;
                cm.Gender = ddlGender.SelectedValue.ToString();
                cm.HomeType = ddlHomeType.SelectedValue.ToString();
                cm.IssuedAt = TxtIssuedAt.Text;
                cm.Mi = TxtMi.Text;
                cm.PhoneListedunder = TxtPhoneListedunder.Text;
                cm.PostCode = TxtPostCode.Text;
                cm.Province = ddlProvince.SelectedValue.ToString();
                cm.SocialSecurityNumber = TxtSocialSecurityNumber.Text;
                cm.WorkPhone = TxtWorkPhone.Text;
                /*if fileupload has value than upload it */
                if (fupImage.HasFile)
                {
                    int TotalCount = cs.CustomerMasters.ToList().Count + 1;
                    Byte[] imgByte = null;
                    if (fupImage.HasFile && fupImage.PostedFile != null)
                    {
                        HttpPostedFile File = fupImage.PostedFile;
                        imgByte = new Byte[File.ContentLength];
                        //File.InputStream.Read(imgByte, 0, File.ContentLength);
                        var fileName = Path.GetFileName(File.FileName);
                        var path = Path.Combine(Server.MapPath("~/ProofofIdentity"), TotalCount.ToString() + "_" + fileName);
                        File.SaveAs(path);
                        cm.ImageName = TotalCount.ToString() + "_" + fileName;
                    }
                }
                cs.CustomerMaster_InsertOrUpdate(cm);
                //insert bank information
                CustomerBankInformation cb = cs.CustomerBankInformationsByCustomerId(Convert.ToInt32(cm.Id)).FirstOrDefault();
                if (cb == null)
                {

                    cb = new CustomerBankInformation();
                    cb.CreatedDate = DateTime.UtcNow;
                    cb.CreatedBy = this.UserId;
                }
                cb.AccountNumber = TxtAccountNumber.Text;
                cb.Address = txtBankAddress.Text;
                cb.BankName = TxtBankName.Text;
                cb.City = txtBankCity.Text;
                cb.CustomerId = cm.Id;
                cb.IsAssignments = CbxIsAssignments.Checked;
                cb.IsBankrutcy = CbxIsBankrutcy.Checked;
                cb.Province = ddlBankProvince.SelectedItem.Text.ToString();
                cb.PostCode = txtBankPostcode.Text;
                cb.NoofSNF = string.IsNullOrEmpty(TxtNoofSNF.Text) ? 0 : Convert.ToInt32(TxtNoofSNF.Text);
                cb.TransitNo = txtTransitNo.Text;
                cb.InstitutionNo = txtInstitutionNo.Text;
                cs.CustomerBankInformation_InsertOrUpdate(cb);

                //insert customeremployment
                CustomerEmployment ce = cs.CustomerEmploymentsbyCustomerId(Convert.ToInt32(cm.Id)).FirstOrDefault();
                if (ce == null)
                {

                    ce = new CustomerEmployment();
                    ce.CreatedDate = DateTime.UtcNow;
                    ce.CreatedBy = this.UserId;
                }
                ce.Address = txtEmployerAddress.Text;
                ce.City = txtEmployerCity.Text;
                ce.CustomerId = cm.Id;
                ce.EmployerDurationMonth = TxtEmployerDurationMonth.Text;
                ce.EmployerDurationYears = TxtEmployerDurationYears.Text;
                ce.EmployerName = TxtEmployerName.Text;
                ce.EmployerType = ddlEmployerType.SelectedValue.ToString();
                ce.HRExt = TxtHRExt.Text;
                ce.HRPhone = TxtHRPhone.Text;
                ce.PostCode = txtEmployerPostCode.Text;
                ce.Province = ddlEmploymentProvince.SelectedValue.ToString();
                ce.SupervisorExt = TxtSupervisorExt.Text;
                ce.SupervisorName = TxtSupervisorName.Text;
                ce.SupervisorPhone = TxtSupervisorPhone.Text;
                cs.CustomerEmployment_InsertOrUpdate(ce);

                //insert customer income details
                CustomerIncome ci = cs.CutomerIncomesbyCustomerId(Convert.ToInt32(cm.Id)).FirstOrDefault();
                if (ci == null)
                {

                    ci = new CustomerIncome();
                    ci.Createdate = DateTime.UtcNow;
                    ci.CreatedBy = this.UserId;
                }
                ci.CustomerId = cm.Id;
                ci.FrequencyofPay = ddlFreuencyofPay.SelectedValue.ToString();
                ci.IsComputerized = CbxIsComputerized.Checked;
                ci.IsDirectDeposit = CbxIsDirectDeposit.Checked;
                ci.TakehomePay = string.IsNullOrEmpty(TxtTakehomePay.Text) ? 0 : Convert.ToDecimal(TxtTakehomePay.Text);
                cs.CutomerIncome_InsertOrUpdate(ci);

                //insert customer reference details
                CustomerReference cr = cs.CustomerReferencesbyCustomerId(Convert.ToInt32(cm.Id)).FirstOrDefault();
                if (cr == null)
                {

                    cr = new CustomerReference();
                    cr.CreatedDate = DateTime.UtcNow;
                    cr.CreatedBy = this.UserId;
                }
                cr.CustomerId = cm.Id;
                cr.Reference1Name = TxtReference1Name.Text.ToString();
                cr.Reference1Phone = TxtReference1Phone.Text.ToString();
                cr.Reference1Relation = TxtReference1Relation.Text.ToString();
                cr.Reference2Name = TxtReference2Name.Text.ToString();
                cr.Reference2Phone = TxtReference2Phone.Text.ToString();
                cr.Reference2Relation = TxtReference2Relation.Text.ToString();
                //cr.Reference3Name = TxtReference3Name.Text.ToString();
                //cr.Reference3Phone = TxtReference3Phone.Text.ToString();
                //cr.Reference3Relation = TxtReference3Relation.Text.ToString();
                cs.CustomerReference_InsertOrUpdate(cr);

                mvView.ActiveViewIndex = 0;
                //BindGrid();
                if (Request.QueryString["mode"] != null)
                {
                    if (Request.QueryString["returnurl"] != null)
                    {
                        Session["CustomerId"] = cm.Id.ToString();
                        Response.Redirect("~/" + Request.QueryString["returnurl"].ToString());
                    }
                }
                else
                {
                    Response.Redirect("~/Menu.aspx");
                }
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            mvView.ActiveViewIndex = 1;
            ClearTextBoxes(this.Controls);
            hdnId.Value = "0";
            hdnvalidationmode.Value = "customer";
            CbxIsDirectDeposit.Checked = true;
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
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void BindGrid()
        {
            CustomerService cs = new CustomerService();
            List<CustomerMaster> lst = new List<CustomerMaster>();
            if (txtSearchName.Text == string.Empty && txtSearchLastName.Text == string.Empty && txtSearchSINNumber.Text == string.Empty && txtSearchId.Text == string.Empty && txtSearchPhoneNumber.Text == string.Empty) { }
            else
            {
                //lst = cs.CustomerMasters.ToList().Where(p => p.CreatedBy == Convert.ToInt32(Session["UserId"])).ToList();
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
               // HttpCookie myUserRoleCookie = Request.Cookies["UserRoleId"];
                if (Session["UserRoleId"] != null)
                {
                    if (Convert.ToInt32(Session["UserRoleId"].ToString()) == 4)
                    {
                        DisableControls(vAdd);
                        btnCancel.Enabled = true;
                    }
                }

                using (CustomerService cs = new CustomerService())
                {
                    int CustomerId = Convert.ToInt32(e.CommandArgument);

                    CustomerMaster cm = cs.CustomerMastersById(CustomerId).FirstOrDefault();
                    CustomerBankInformation cb = cs.CustomerBankInformationsByCustomerId(CustomerId).FirstOrDefault();
                    CustomerEmployment ce = cs.CustomerEmploymentsbyCustomerId(CustomerId).FirstOrDefault();
                    CustomerIncome ci = cs.CutomerIncomesbyCustomerId(CustomerId).FirstOrDefault();
                    CustomerReference cr = cs.CustomerReferencesbyCustomerId(CustomerId).FirstOrDefault();
                    hdnId.Value = CustomerId.ToString();

                    mvView.ActiveViewIndex = 1;
                    TxtHomePhone.Text = cm.HomePhone;
                    TxtFirstName.Text = cm.FirstName;
                    TxtLastName.Text = cm.LastName;
                    TxtAddress.Text = cm.Address;
                    TxtCellPhone.Text = cm.CellPhone;
                    TxtCity.Text = cm.City;
                    TxtComments.Text = cm.Comments;
                    if (cm.Dateofbirth != null)
                    {
                        TxtDateofbirth.Text = Convert.ToDateTime(cm.Dateofbirth).ToString("MM/dd/yyyy");
                    }
                    else
                    {
                        TxtDateofbirth.Text = string.Empty;
                    }
                    TxtDrivingLicenseNumber.Text = cm.DrivingLicenseNumber;
                    TxtDurationMonth.Text = cm.DurationMonth;
                    TxtDurationYears.Text = cm.DurationYears;
                    ddlGender.SelectedIndex = ddlGender.Items.IndexOf(ddlGender.Items.FindByValue(cm.Gender));
                    ddlHomeType.SelectedIndex = ddlHomeType.Items.IndexOf(ddlHomeType.Items.FindByValue(cm.HomeType));

                    TxtIssuedAt.Text = cm.IssuedAt;
                    TxtMi.Text = cm.Mi;
                    TxtPhoneListedunder.Text = cm.PhoneListedunder;
                    TxtPostCode.Text = cm.PostCode;
                    ddlProvince.SelectedIndex = ddlProvince.Items.IndexOf(ddlProvince.Items.FindByValue(cm.Province));
                    TxtSocialSecurityNumber.Text = cm.SocialSecurityNumber;
                    TxtWorkPhone.Text = cm.WorkPhone;

                    //edit bank information
                    if (cb != null)
                    {
                        TxtAccountNumber.Text = cb.AccountNumber;
                        txtBankAddress.Text = cb.Address;
                        TxtBankName.Text = cb.BankName;
                        txtBankCity.Text = cb.City;
                        txtInstitutionNo.Text = cb.InstitutionNo;
                        txtTransitNo.Text = cb.TransitNo;
                        CbxIsAssignments.Checked = (bool)cb.IsAssignments;
                        CbxIsBankrutcy.Checked = (bool)cb.IsBankrutcy;
                        ddlBankProvince.SelectedIndex = ddlBankProvince.Items.IndexOf(ddlBankProvince.Items.FindByText(cb.Province));
                        txtBankPostcode.Text = cb.PostCode;
                        TxtNoofSNF.Text = cb.NoofSNF.ToString();
                    }
                    if (ce != null)
                    {
                        //edit employment
                        txtEmployerAddress.Text = ce.Address;
                        txtEmployerCity.Text = ce.City;
                        TxtEmployerDurationMonth.Text = ce.EmployerDurationMonth;
                        TxtEmployerDurationYears.Text = ce.EmployerDurationYears;
                        TxtEmployerName.Text = ce.EmployerName;
                        ddlEmployerType.SelectedIndex = ddlEmployerType.Items.IndexOf(ddlEmployerType.Items.FindByValue(ce.EmployerType));
                        TxtHRExt.Text = ce.HRExt;
                        TxtHRPhone.Text = ce.HRPhone;
                        txtEmployerPostCode.Text = ce.PostCode;
                        ddlEmploymentProvince.SelectedIndex = ddlEmploymentProvince.Items.IndexOf(ddlEmploymentProvince.Items.FindByValue(ce.Province));
                        TxtSupervisorExt.Text = ce.SupervisorExt;
                        TxtSupervisorName.Text = ce.SupervisorName;
                        TxtSupervisorPhone.Text = ce.SupervisorPhone;
                    }
                    //edit income details
                    if (ci != null)
                    {
                        ddlFreuencyofPay.SelectedIndex = ddlFreuencyofPay.Items.IndexOf(ddlFreuencyofPay.Items.FindByValue(ci.FrequencyofPay));
                        CbxIsComputerized.Checked = (bool)ci.IsComputerized;
                        CbxIsDirectDeposit.Checked = (bool)ci.IsDirectDeposit;
                        TxtTakehomePay.Text = ci.TakehomePay.ToString();
                    }
                    if (cr != null)
                    {
                        //edit customer reference
                        TxtReference1Name.Text = cr.Reference1Name;
                        TxtReference1Phone.Text = cr.Reference1Phone;
                        TxtReference1Relation.Text = cr.Reference1Relation;
                        TxtReference2Name.Text = cr.Reference2Name;
                        TxtReference2Phone.Text = cr.Reference2Phone;
                        TxtReference2Relation.Text = cr.Reference2Relation;
                        //TxtReference3Name.Text = "";// cr.Reference3Name;
                        //TxtReference3Phone.Text = "";// cr.Reference3Phone;
                        //TxtReference3Relation.Text = "";// cr.Reference3Relation;
                    }

                     ScriptManager.RegisterStartupScript(this, GetType(), "loadMsgs", "loadCustomerProfileMessages(" + CustomerId + ");", true);
                }
            }
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
        [WebMethod]
        public static string GetMessages(int customerId)
        {
            CustomerService service = new CustomerService();
            DataSet ds = service.GetCustomerMessages(customerId);

            StringBuilder html = new StringBuilder();
            html.Append("<table class='table table-bordered table-striped'><thead><tr>");
            html.Append("<th style='width:7%;'>Pin</th><th style='width:71%;'>Message</th><th style='width:22%;'>Actions</th></tr></thead><tbody>");

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    string pinnedIcon = Convert.ToBoolean(row["IsPinned"]) ? "📌" : "";
                    string msgClean = row["MessageText"].ToString().Replace("'", "\\'"); // Escape for JS

                    html.Append("<tr>");
                    html.Append($"<td>{pinnedIcon}</td>");
                    html.Append($"<td>{row["MessageText"]}</td>");
                    html.Append("<td>");
                    html.Append($"<button type='button' class='btn btn-warning btn-xs' onclick=\"editMsg({row["Id"]}, '{msgClean}', {row["IsPinned"].ToString().ToLower()})\">Edit</button>");
                    html.Append($"<button type='button' class='btn btn-danger btn-xs' style='margin-left:10px;' onclick='deleteMsg({row["Id"]})'>Delete</button>");
                    html.Append("</td></tr>");
                }
            }
            else
            {
                html.Append("<tr><td colspan='3' class='text-center'>No messages yet.</td></tr>");
            }

            html.Append("</tbody></table>");
            return html.ToString();
        }

        [WebMethod]
        public static string SaveMessage(int id, int customerId, string text, bool isPinned)
        {
            try
            {
                CustomerService service = new CustomerService();
                service.SaveCustomerMessage(id, customerId, text, isPinned);
                return "Success";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
        [WebMethod]
        public static string DeleteMessage(int id)
        {
            try
            {
                CustomerService service = new CustomerService();
                service.DeleteCustomerMessage(id);
                return "Deleted";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        [WebMethod]
        public static string GetMessagesHTML(int customerId)
        {
            CustomerService service = new CustomerService();
            DataSet ds = service.GetCustomerMessages(customerId); // Uses the SP above
            StringBuilder sb = new StringBuilder();

            if (ds.Tables[0].Rows.Count > 0)
            {
                sb.Append("<ul class='list-group'>");
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    bool isPinned = Convert.ToBoolean(row["IsPinned"]);
                    string pinStyle = isPinned ? "background-color: #fff9c4; border-left: 5px solid #fbc02d;" : "";
                    string pinIcon = isPinned ? "<i class='fa fa-thumb-tack text-warning'></i> " : "";
 
                    sb.Append($"<li class='list-group-item force-wrap' style='{pinStyle}'>");
                    sb.Append($"<strong>{pinIcon}{Convert.ToDateTime(row["CreatedDate"]):MMM dd, yyyy}:</strong> ");
                    sb.Append(row["MessageText"].ToString());
                    sb.Append("</li>");
                }
                sb.Append("</ul>");
            }
            else
            {
                sb.Append("<div class='alert alert-warning'>No messages found for this customer.</div>");
            }
            return sb.ToString();
        }
    }
}