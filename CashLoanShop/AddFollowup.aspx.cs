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
    public partial class AddFollowup : System.Web.UI.Page
    {
        public int UserId { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
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
                hdnId.Value = "0";
                SelectCustomer(CustomerId);
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
        public void SelectCustomer(int CustomerId)
        {
            using (CustomerService cs = new CustomerService())
            {
                List<CustomerMaster> cm = cs.CustomerMastersById(CustomerId).ToList();
                mvView.ActiveViewIndex = 1;
                hdnId.Value = "0";
                txtCustomerId.Text = cm.FirstOrDefault().Id.ToString();
                txtPhoneNo.Text = cm.FirstOrDefault().CellPhone.ToString();
                txtcustomerName.Text = cm.FirstOrDefault().FirstName + " " + cm.FirstOrDefault().Mi + " " + cm.FirstOrDefault().LastName.ToString();
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
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            mvView.ActiveViewIndex = 0;
            BindGrid();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            using (FollowupService cls = new FollowupService())
            {
                int id = Convert.ToInt32(hdnId.Value);
                CashLoanShop.Model.CustomerFollowup cm = cls.CustomerFollowups.Where(p => p.Id == id).FirstOrDefault();
                if (cm == null)
                {
                    cm = new CustomerFollowup();
                    cm.CreatedDate = ConvertEasternTime(DateTime.Now);
                    cm.CreatedBy = this.UserId;
                }
                cm.CustomerId = Convert.ToInt32(txtCustomerId.Text);
                cm.Comments = txtComments.Text;
                cm.FinalStatus = ddlFinalStatus.SelectedValue.ToString();
                cm.FollowupCode = ddlFollowupCode.SelectedValue.ToString();
                cm.FollowupDate = Convert.ToDateTime(txtFollowupDate.Text).Date;
                cm.FollowupTime = txtFollowupTime.Text;
                cm.FollowupDoneBy = txtFollowupDoneby.Text;
                cm.NextFolloupDate = Convert.ToDateTime(txtNextFollowupDate.Text).Date;
                cm.NextFollowupTime = txtNextFollowupTime.Text;
                cls.CustomerFollowup_InsertOrUpdate(cm);
                mvView.ActiveViewIndex = 0;
            }
        }

        protected void btnViewTransaction_Click(object sender, EventArgs e)
        {
            mvView.ActiveViewIndex = 2;
            using (FollowupService cls = new FollowupService())
            {
                List<CashLoanShop.Model.CustomerFollowup> cm = cls.CustomerFollowups.OrderByDescending(p => p.Id).ToList();
                foreach (var obj in cm)
                {
                    using (CustomerService cs = new CustomerService())
                    {
                        List<CustomerMaster> cust = cs.CustomerMastersById(Convert.ToInt32(obj.CustomerId)).ToList();
                        obj.CustomerName = cust.FirstOrDefault().FirstName + " " + cust.FirstOrDefault().Mi + " " + cust.FirstOrDefault().LastName.ToString();
                        obj.ContactNo = cust.FirstOrDefault().CellPhone;
                    }
                }
                rptGridData.DataSource = cm;
                rptGridData.DataBind();
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            mvView.ActiveViewIndex = 0;
        }

        protected void rptGridData_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                using (FollowupService cls = new FollowupService())
                {
                    var Id = Convert.ToInt32(e.CommandArgument);
                    hdnId.Value = Id.ToString();
                    mvView.ActiveViewIndex = 1;
                    CashLoanShop.Model.CustomerFollowup cm = cls.CustomerFollowups.Where(p => p.Id == Id).FirstOrDefault();
                    if (cm != null)
                    {
                        using (CustomerService cs = new CustomerService())
                        {
                            txtCustomerId.Text = cm.CustomerId.ToString();
                            List<CustomerMaster> cmm = cs.CustomerMastersById(Convert.ToInt32( cm.CustomerId)).ToList();
                            txtPhoneNo.Text = cmm.FirstOrDefault().CellPhone.ToString();
                            txtcustomerName.Text = cmm.FirstOrDefault().FirstName + " " + cmm.FirstOrDefault().Mi + " " + cmm.FirstOrDefault().LastName.ToString();
                        }
                        txtComments.Text = cm.Comments.ToString();
                        txtFollowupDate.Text = cm.FollowupDate.ToString();
                        txtFollowupDoneby.Text = cm.FollowupDoneBy.ToString();
                        txtFollowupTime.Text = cm.FollowupTime.ToString();
                        txtNextFollowupDate.Text = cm.NextFolloupDate.ToString();
                        txtNextFollowupTime.Text = cm.NextFollowupTime.ToString();
                        ddlFinalStatus.SelectedValue = cm.FinalStatus;
                        ddlFollowupCode.SelectedValue = cm.FollowupCode;
                    }
                }

            }
        }
    }
}