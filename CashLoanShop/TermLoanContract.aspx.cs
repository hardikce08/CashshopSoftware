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
    public partial class TermLoanContract : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["Id"]))
                {
                    int CustomerLoanId = Convert.ToInt32(Request.QueryString["Id"]);
                    hdnLoanId.Value = CustomerLoanId.ToString();
                    using (TermLoanService ts = new TermLoanService())
                    {
                        CustomerTermLoan objcc = ts.CustomerTermLoansbyId(CustomerLoanId).FirstOrDefault();
                        string MailTemplate = System.IO.File.ReadAllText(Server.MapPath("~/TermLoanContract.html"));
                        using (CustomerService cs = new CustomerService())
                        {
                            CustomerMaster cm = cs.CustomerMastersById(objcc.CustomerId).FirstOrDefault();
                            if (cm != null)
                            {
                                MailTemplate = MailTemplate.Replace("@customername", cm.FirstName + " " + cm.Mi + " " + cm.LastName);
                                MailTemplate = MailTemplate.Replace("@customeraddress", cm.Address + ", " + cm.City + " , " + GetProvince(Convert.ToInt32(cm.Province)) + " , " + cm.PostCode + "<br/> Phone:" + (cm.CellPhone == string.Empty ? cm.WorkPhone : cm.CellPhone));
                                MailTemplate = MailTemplate.Replace("@bcustomeraddress", cm.Address + "<br/>" + cm.City + " , " + GetProvince(Convert.ToInt32(cm.Province)) + " , " + cm.PostCode + "<br/> Phone:" + (cm.CellPhone == string.Empty ? cm.WorkPhone : cm.CellPhone));

                            }
                            using (CompanyService cmp = new CompanyService())
                            {
                                CompanyStore CompanyStores = cmp.CompanyStoresbyId(objcc.ShopStoreId);
                                decimal loanprincipal = Math.Round(Convert.ToDecimal(objcc.LoanAmountApproved + (objcc.LoanAmountApproved * (CompanyStores.AdminFeePercentage / 100))), 2);
                                MailTemplate = MailTemplate.Replace("@storename", CompanyStores.Name);
                                MailTemplate = MailTemplate.Replace("@storeaddress", CompanyStores.NewAddress + ", " + CompanyStores.City + " , " + GetProvince(Convert.ToInt32(CompanyStores.Province)) + " , " + CompanyStores.PostCode);
                                MailTemplate = MailTemplate.Replace("@rateofinterest", (CompanyStores.TermInterestRate).ToString() + "%(" + ConvertNumbertoWords(Convert.ToInt32((CompanyStores.TermInterestRate))) + " percent)");
                                MailTemplate = MailTemplate.Replace("@storedetails", CompanyStores.Name + " O/A " + CompanyStores.Businessname + "<br/>" + CompanyStores.NewAddress + "<br/>" + CompanyStores.City + " , " + GetProvince(Convert.ToInt32(CompanyStores.Province)) + " , " + CompanyStores.PostCode + "<br/>Phone:" + CompanyStores.PhoneNo + ", Fax:" + CompanyStores.Fax + "<br/> Email:" + CompanyStores.Email);
                                MailTemplate = MailTemplate.Replace("@loanprincipal", "$" + String.Format("{0:N}", loanprincipal));
                                MailTemplate = MailTemplate.Replace("@loantotal", "$" + String.Format("{0:N}", objcc.DueAmount));
                                MailTemplate = MailTemplate.Replace("@laterateofinterest", CompanyStores.TermLateInterestRate.ToString() + "%");
                                MailTemplate = MailTemplate.Replace("@borrowerloanprincipal", "$" + String.Format("{0:N}", loanprincipal) + "(" + ConvertNumbertoWords(Convert.ToInt32((loanprincipal))) + " Canadian dollars)");
                                MailTemplate = MailTemplate.Replace("@storesubaddress", CompanyStores.NewAddress + ", " + CompanyStores.City + " , " + GetProvince(Convert.ToInt32(CompanyStores.Province)) + " , " + CompanyStores.PostCode);
                                MailTemplate = MailTemplate.Replace("@storecity", CompanyStores.City);
                                MailTemplate = MailTemplate.Replace("@nsfcharge", "$" + String.Format("{0:N}", CompanyStores.TermNSFCharge));
                            }
                            if (objcc != null)
                            {

                                MailTemplate = MailTemplate.Replace("@loancontractdate", objcc.CreatedDate.ToString("dddd, MMMM d, yyyy"));
                                MailTemplate = MailTemplate.Replace("@duedate", objcc.DueDate.ToString("dddd, MMMM d, yyyy"));
                                MailTemplate = MailTemplate.Replace("@loanamount", "$" + String.Format("{0:N}", Math.Round(Convert.ToDecimal(objcc.LoanAmountApproved), 2)) + " (" + ConvertNumbertoWords(Convert.ToInt32((objcc.LoanAmountApproved))) + " Canadian dollars)");
                                MailTemplate = MailTemplate.Replace("@calendaryear", objcc.LoanTerm == "1" ? "12" : "24");
                                MailTemplate = MailTemplate.Replace("@Id", Request.QueryString["Id"].ToString());
                                System.Data.DataSet ds = ts.GetPaymentSchedule(Convert.ToDateTime(objcc.FirstInstallmentDate), (!objcc.SecondInstallmentDate.HasValue ? Convert.ToDateTime(DateTime.Now) : Convert.ToDateTime(objcc.SecondInstallmentDate)), objcc.DueDate, objcc.LoanType, Convert.ToDecimal(objcc.DueAmount));
                                string t = Convert.ToDateTime(objcc.FirstInstallmentDate).DayOfWeek.ToString();
                                MailTemplate = MailTemplate.Replace("@loantype", objcc.LoanType);
                                //MailTemplate = MailTemplate.Replace("@installmentamount", String.Format("{0:N}", Math.Round(objcc.InstallmentAmount, 2)));
                                MailTemplate = MailTemplate.Replace("@loanterm", objcc.LoanTerm == "1" ? "One Year" : "Two Year");
                                MailTemplate = MailTemplate.Replace("@totaltermlyamount", "$" + String.Format("{0:N}", Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[0][2].ToString()), 2)));
                                MailTemplate = MailTemplate.Replace("@firstinstallmentday", Convert.ToDateTime(objcc.FirstInstallmentDate).ToString("dddd, MMMM d, yyyy"));
                                MailTemplate = MailTemplate.Replace("@duedatestatement", objcc.ReportText);
                                MailTemplate = MailTemplate.Replace("@totaldueamount", "$" + String.Format("{0:N}", Math.Round(Convert.ToDecimal(objcc.DueAmount), 2)));
                                MailTemplate = MailTemplate.Replace("@loanperiodmonths", ConvertNumbertoWords(Convert.ToInt32((objcc.LoanTerm == "1" ? "12" : "24"))) + "(" + objcc.LoanTerm == "1" ? "12" : "24" + ") months");

                                CustomerBankInformation cb = cs.CustomerBankInformationsByCustomerId(cm.Id).FirstOrDefault();
                                if (cb != null)
                                {
                                    MailTemplate = MailTemplate.Replace("@bankaccountnumber", cb.AccountNumber);
                                    MailTemplate = MailTemplate.Replace("@institutionnumber", cb.InstitutionNo);
                                    MailTemplate = MailTemplate.Replace("@banktansitnumber", cb.TransitNo);
                                    MailTemplate = MailTemplate.Replace("@bankname", cb.BankName);
                                    MailTemplate = MailTemplate.Replace("@bankaddress", cb.Address);
                                    MailTemplate = MailTemplate.Replace("@bankcitystate", (cb.City == string.Empty ? "" : "<u>" + cb.City + "</u>") + " , " + (cb.Province == "Select Province" ? "" : "<u>" + cb.Province + "</u>") + " , " + (cb.PostCode == string.Empty ? "" : "<u>" + cb.PostCode + "</u>"));
                                }
                                cb = null;
                                content.InnerHtml = MailTemplate.ToString();
                            }
                        }
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
        public string ConvertNumbertoWords(long number)
        {
            if (number == 0) return "ZERO";
            if (number < 0) return "minus " + ConvertNumbertoWords(Math.Abs(number));
            string words = "";
            if ((number / 1000000) > 0)
            {
                words += ConvertNumbertoWords(number / 100000) + " LAKES ";
                number %= 1000000;
            }
            if ((number / 1000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000) + " THOUSAND ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += ConvertNumbertoWords(number / 100) + " HUNDRED ";
                number %= 100;
            }
            //if ((number / 10) > 0)  
            //{  
            // words += ConvertNumbertoWords(number / 10) + " RUPEES ";  
            // number %= 10;  
            //}  
            if (number > 0)
            {
                if (words != "") words += "AND ";
                var unitsMap = new[]
        {
            "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN"
        };
                var tensMap = new[]
        {
            "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY"
        };
                if (number < 20) words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0) words += " " + unitsMap[number % 10];
                }
            }
            return words;
        }
    }
}