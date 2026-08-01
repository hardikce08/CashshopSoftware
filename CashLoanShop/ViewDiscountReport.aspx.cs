using CashLoanShop.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CashLoanShop
{
    public partial class ViewDiscountReport : System.Web.UI.Page
    {
        public decimal TotalLoanAmount, TotalDiscount, TotalLoanAmountTerm, TotalDiscountTerm;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(Request.QueryString["StoreId"]) && !string.IsNullOrEmpty(Request.QueryString["FromDate"]))
            {
                CompanyService cmp = new CompanyService();
                //Model.CompanyStore CompanyStores = cmp.CompanyStores.ToList().Where(p => p.Id == Convert.ToInt32(Request.QueryString["StoreId"])).FirstOrDefault();
                //if (CompanyStores != null)
                //{
                //    lblStoreInfo.Text = CompanyStores.Name + "<br/>" + CompanyStores.Address.Replace(",", "<br/>").Replace("$", " , ") + "<br/>" + CompanyStores.Email;
                //}
                DateTime Fromdate = Convert.ToDateTime(Request.QueryString["FromDate"]);
                DateTime Todate = Convert.ToDateTime(Request.QueryString["ToDate"]);
                lblDateRange.Text = Fromdate.ToString("dd/MM/yyyy") + " to " + Todate.ToString("dd/MM/yyyy");
                TermLoanService ts = new TermLoanService();
                DataTable dt = ts.GetDiscountReport(Fromdate, Todate.AddDays(1), Request.QueryString["StoreId"]);
                DataView dv1 = dt.DefaultView;
                dv1.RowFilter = "LoanType='Payday Loan'";
                DataTable dtPaydayLoan = dv1.ToTable();
                DataView dv2 = dt.DefaultView;
                dv2.RowFilter = "LoanType='Term Loan'";
                DataTable dtTermLoan = dv2.ToTable();

                rptGridData.DataSource = dtPaydayLoan;
                rptGridData.DataBind();

                rptTermLoanData.DataSource = dtTermLoan;
                rptTermLoanData.DataBind();

            }
        }
        protected void rptGridData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView item = (DataRowView)e.Item.DataItem;

                TotalLoanAmount += Convert.ToDecimal(item["LoanAmount"]);

                TotalDiscount += Convert.ToDecimal(item["DiscountAmount"]);
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                if (rptGridData.Items.Count == 0)
                {
                    if (e.Item.ItemType == ListItemType.Footer)
                    {
                        e.Item.FindControl("trEmpty").Visible = true;
                        e.Item.FindControl("trData").Visible = false;
                    }
                }
            }
        }
        protected void rptTermLoanData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView item = (DataRowView)e.Item.DataItem;

                TotalLoanAmountTerm += Convert.ToDecimal(item["LoanAmount"]);

                TotalDiscountTerm += Convert.ToDecimal(item["DiscountAmount"]);
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                if (rptTermLoanData.Items.Count == 0)
                {
                    if (e.Item.ItemType == ListItemType.Footer)
                    {

                        //Control FooterTemplate = rptTermLoanData.Controls[rptTermLoanData.Controls.Count - 1].Controls[0];
                        e.Item.FindControl("trEmpty").Visible = true;
                        e.Item.FindControl("trData").Visible = false;
                    }
                }
            }
        }

        protected void ExportToExcel(object sender, EventArgs e)
        {
            DateTime Fromdate = Convert.ToDateTime(Request.QueryString["FromDate"]);
            DateTime Todate = Convert.ToDateTime(Request.QueryString["ToDate"]);
            TermLoanService ts = new TermLoanService();
            DataTable dt = ts.GetDiscountReport(Fromdate, Todate.AddDays(1), Request.QueryString["StoreId"]);
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "LoanType='Payday Loan'";
            DataTable dtPaydayLoan = dv1.ToTable();
            //Build the CSV file data as a Comma separated string.
            string csv = string.Empty;

            foreach (DataColumn column in dtPaydayLoan.Columns)
            {
                //Add the Header row for CSV file.
                csv += column.ColumnName + ',';
            }

            //Add new line.
            csv += "\r\n";

            foreach (DataRow row in dtPaydayLoan.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    //Add the Data rows.
                    csv += row[column.ColumnName].ToString().Replace(",", ";") + ',';
                }

                //Add new line.
                csv += "\r\n";
            }

            //Download the CSV file.
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=ExportPaydayLoanDiscount.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(csv);
            Response.Flush();
            Response.End();
        }
        protected void ExportToExcelTermLoan(object sender, EventArgs e)
        {
            DateTime Fromdate = Convert.ToDateTime(Request.QueryString["FromDate"]);
            DateTime Todate = Convert.ToDateTime(Request.QueryString["ToDate"]);
            TermLoanService ts = new TermLoanService();
            DataTable dt = ts.GetDiscountReport(Fromdate, Todate.AddDays(1), Request.QueryString["StoreId"]);
            DataView dv2 = dt.DefaultView;
            dv2.RowFilter = "LoanType='Term Loan'";
            DataTable dtTermLoan = dv2.ToTable();
            //Build the CSV file data as a Comma separated string.
            string csv = string.Empty;

            foreach (DataColumn column in dtTermLoan.Columns)
            {
                //Add the Header row for CSV file.
                csv += column.ColumnName + ',';
            }

            //Add new line.
            csv += "\r\n";

            foreach (DataRow row in dtTermLoan.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    //Add the Data rows.
                    csv += row[column.ColumnName].ToString().Replace(",", ";") + ',';
                }

                //Add new line.
                csv += "\r\n";
            }

            //Download the CSV file.
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=ExportTermLoanDiscount.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(csv);
            Response.Flush();
            Response.End();
        }
    }
}