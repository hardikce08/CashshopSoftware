using CashLoanShop.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CashLoanShop
{
    public partial class Company : System.Web.UI.Page
    {
        CompanyService cs = new CompanyService();
        public int UserId { get; set; }
        protected void Page_Load(object sender, EventArgs e)
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

            if (!IsPostBack)
            {
                //BindGrid();

            }
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
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            ClearTextBoxes(this.Controls);
            mvView.ActiveViewIndex = 1;
            hdnId.Value = "0";

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        private void BindGrid()
        {
            if (txtSearchName.Text == string.Empty) return;
            //List<CashLoanShop.Model.Company> lst = cs.Companys.ToList().Where(p => p.CreatedBy == Convert.ToInt32(Session["UserId"])).ToList();
            List<CashLoanShop.Model.Company> lst = cs.Companys.ToList();
            if (txtSearchName.Text != string.Empty)
            {
                lst = lst.Where(p => p.Name.ToLower().StartsWith(txtSearchName.Text.ToLower())).ToList();
            }
            dgvCompany.DataSource = lst;
            dgvCompany.DataBind();
            mvView.ActiveViewIndex = 0;
        }
        protected void dgvCompany_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                cs = new CompanyService();
                int Companyid = Convert.ToInt32(e.CommandArgument);

                if (Request.QueryString["mode"] != null)
                {
                    if (Request.QueryString["returnurl"] != null)
                    {
                        Session["CompanyId"] = Companyid.ToString();
                        Response.Redirect("~/" + Request.QueryString["returnurl"].ToString());
                    }
                }

                CashLoanShop.Model.Company cm = cs.Companys.ToList().Where(p => p.Id == Convert.ToInt32(Companyid)).FirstOrDefault();

                hdnId.Value = Companyid.ToString();
                mvView.ActiveViewIndex = 1;
                if (cm != null)
                {
                    txtCompanyName.Text = cm.Name;
                    txtAddress.Text = cm.Address;
                    txtBankAccountNumber.Text = cm.BankAccountNumber;
                    txtBankTransitNumber.Text = cm.BankTransitNumber;
                    TxtCity.Text = cm.City;
                    txtPhone.Text = cm.Phone;
                    TxtPostCode.Text = cm.PostCode;
                    txtStatus.Text = cm.Status;
                    ddlProvince.SelectedIndex = ddlProvince.Items.IndexOf(ddlProvince.Items.FindByValue(cm.Province));
                }

            }
        }
        protected void dgvCompany_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvCompany.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void dgvCompany_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            mvView.ActiveViewIndex = 0;
            BindGrid();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            CashLoanShop.Model.Company cm = cs.Companys.ToList().Where(p => p.Id == Convert.ToInt32(hdnId.Value)).FirstOrDefault();
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
                cm = new CashLoanShop.Model.Company();
                cm.CreatedDate = DateTime.UtcNow;
                cm.CreatedBy = this.UserId;
            }
            cm.Address = txtAddress.Text;
            cm.BankAccountNumber = txtBankAccountNumber.Text;
            cm.BankTransitNumber = txtBankTransitNumber.Text;
            cm.City = TxtCity.Text;
            cm.Name = txtCompanyName.Text;
            cm.Phone = txtPhone.Text;
            cm.PostCode = TxtPostCode.Text;
            cm.Province = ddlProvince.SelectedValue.ToString();
            cm.Status = txtStatus.Text;

            cs.Company_InsertOrUpdate(cm);
            txtSearchName.Text = "";
            //BindGrid();
            if (Request.QueryString["mode"] != null)
            {
                if (Request.QueryString["returnurl"] != null)
                {
                    Session["CompanyId"] = cm.Id.ToString();
                    Response.Redirect("~/" + Request.QueryString["returnurl"].ToString());
                }
            }
            else
            {
                //Response.Redirect("~/Menu.aspx");
            }
        }
    }
}