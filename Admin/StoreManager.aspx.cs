using CashLoanShop.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CashLoanShop.Admin
{
    public partial class StoreManager : System.Web.UI.Page
    {
        CompanyService cs = new CompanyService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
                BindCombo();
            }
        }
        public void BindCombo()
        {
            CompanyService cs = new CompanyService();
            List<Model.CompanyStore> lstCompanyStores = cs.CompanyStores.ToList();
            if (lstCompanyStores.Count > 0)
            {
                foreach (var item in lstCompanyStores)
                {
                    item.Name = item.Name + " (" + item.Address.Replace("$", " , ") + ")";
                }
            }
            lstCompanyStores.Insert(0, new Model.CompanyStore { Id = 0, Name = "--Select--" });
            ddlSearchStore.DataSource = lstCompanyStores;
            ddlSearchStore.DataTextField = "Name";
            ddlSearchStore.DataValueField = "Id";
            ddlSearchStore.DataBind();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            CashLoanShop.Model.CompanyStore cm = cs.CompanyStores.ToList().Where(p => p.Id == Convert.ToInt32(hdnId.Value)).FirstOrDefault();
            if (cm == null)
            {
                cm = new CashLoanShop.Model.CompanyStore();
                cm.CreatedDate = DateTime.UtcNow;
            }
            cm.NewAddress = txtNewAddress.Text;
            cm.Name = txtStoreName.Text;
            cm.PhoneNo = txtPhoneno.Text;
            cm.PostCode = txtPostCode.Text;
            cm.Province = ddlProvince.SelectedValue.ToString();
            cm.Fax = txtFax.Text;
            cm.Email = txtEmail.Text;
            cm.Businessname = txtBusinessName.Text;
            cm.City = txtCity.Text;
            cm.NSFCharge = string.IsNullOrEmpty(txtNSFCharge.Text) ? 20 : Convert.ToDecimal(txtNSFCharge.Text);
            cm.InterestRate = string.IsNullOrEmpty(txtInterestRate.Text) ? 32 : Convert.ToDecimal(txtInterestRate.Text);
            cm.TermInterestRate = string.IsNullOrEmpty(txtTermInterestRate.Text) ? 0 : Convert.ToDecimal(txtTermInterestRate.Text);
            cm.TermLateInterestRate = string.IsNullOrEmpty(txtTermLateInterestRate.Text) ? 0 : Convert.ToDecimal(txtTermLateInterestRate.Text);
            cm.TermNSFCharge = string.IsNullOrEmpty(txtTermNSFCharge.Text) ? 0 : Convert.ToDecimal(txtTermNSFCharge.Text);
            cm.MaximumTermLoanAmount = string.IsNullOrEmpty(txtMaximumLoanAmount.Text) ? 0 : Convert.ToDecimal(txtMaximumLoanAmount.Text);
            cm.AdminFeePercentage = string.IsNullOrEmpty(txtAdminFeePercentage.Text) ? 0 : Convert.ToDecimal(txtAdminFeePercentage.Text);
            cm.Address = txtNewAddress.Text + "," + txtCity.Text + "$" + ddlProvince.SelectedItem.Text + "$" + txtPostCode.Text;
            cs.CompanyStore_InsertOrUpdate(cm);
            BindGrid();
        }
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            mvView.ActiveViewIndex = 1;
            hdnId.Value = "0";
            ddlProvince.SelectedIndex = 0;
            txtPostCode.Text = "";
            txtStoreName.Text = "";
            txtPhoneno.Text = "";
            txtNewAddress.Text = "";
            txtFax.Text = "";
            txtEmail.Text = "";
            txtCity.Text = "";
            txtBusinessName.Text = "";
            txtInterestRate.Text = "";
            txtTermInterestRate.Text = "";
            txtAdminFeePercentage.Text = "";
            txtTermLateInterestRate.Text = "";
            txtMaximumLoanAmount.Text = "";
            txtTermNSFCharge.Text = "";
        }
        private void BindGrid()
        {
            CompanyService cs = new CompanyService();
            List<CashLoanShop.Model.CompanyStore> lst = cs.CompanyStores.ToList();
            if (txtSearch.Text != string.Empty)
            {
                lst = lst.Where(p => p.Id == Convert.ToInt32(txtSearch.Text.ToLower())).ToList();
            }
            if (txtSearchStoreName.Text != string.Empty)
            {
                lst = lst.Where(p => p.Name.ToLower().Contains(txtSearchStoreName.Text.ToLower())).ToList();
            }
            if (ddlSearchStore.SelectedValue.ToString() != "0" && ddlSearchStore.SelectedValue.ToString() != string.Empty)
            {
                lst = lst.Where(p => p.Id == Convert.ToInt32(ddlSearchStore.SelectedValue.ToString())).ToList();
            }
            dgvCustomer.DataSource = lst;
            dgvCustomer.DataBind();
            mvView.ActiveViewIndex = 0;
        }

        protected void dgvCustomer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                int Storeid = Convert.ToInt32(e.CommandArgument);
                CashLoanShop.Model.CompanyStore cm = cs.CompanyStores.ToList().Where(p => p.Id == Convert.ToInt32(Storeid)).FirstOrDefault();

                hdnId.Value = Storeid.ToString();
                mvView.ActiveViewIndex = 1;
                if (cm != null)
                {
                    txtNewAddress.Text = cm.NewAddress;
                    txtStoreName.Text = cm.Name;
                    txtPhoneno.Text = cm.PhoneNo;
                    txtPostCode.Text = cm.PostCode;
                    if (cm.Province != null)
                        ddlProvince.SelectedIndex = ddlProvince.Items.IndexOf(ddlProvince.Items.FindByValue(cm.Province.ToString()));
                    txtFax.Text = cm.Fax;
                    txtEmail.Text = cm.Email;
                    txtBusinessName.Text = cm.Businessname;
                    txtCity.Text = cm.City;
                    txtNSFCharge.Text = cm.NSFCharge.ToString();
                    txtInterestRate.Text = cm.InterestRate.ToString();
                    txtTermInterestRate.Text = cm.TermInterestRate.ToString();
                    txtAdminFeePercentage.Text = cm.AdminFeePercentage.ToString();
                    txtMaximumLoanAmount.Text = cm.MaximumTermLoanAmount.ToString();
                    txtTermLateInterestRate.Text = cm.TermLateInterestRate.ToString();
                    txtTermNSFCharge.Text = cm.TermNSFCharge.ToString();
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
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}