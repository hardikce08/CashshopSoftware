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
    public partial class CustomerLoanContract : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["Id"]))
                {
                    int CustomerLoanId = Convert.ToInt32(Request.QueryString["Id"]);
                    hdnLoanId.Value = CustomerLoanId.ToString();
                    CustomerLoanService cc = new CustomerLoanService();
                    Model.CustomerLoan objcc = cc.CustomerLoansbyId(CustomerLoanId).FirstOrDefault();

                    if (objcc != null)
                    {
                        // List<Model.CustomerLoan> lstprevloan = cc.CustomerLoans.Where(p => p.Id != CustomerLoanId && p.CustomerId == objcc.CustomerId).Select(p=>p.CreatedDate);
                        string prevdates = string.Join(" , ", cc.CustomerLoansbyCustomerId(objcc.CustomerId).Where(p => p.Id != CustomerLoanId && p.CreatedDate >= ConvertEasternTime(DateTime.Now).AddDays(-63).Date).Select(p => p.CreatedDate.Date.ToString("MM/dd/yyyy")));
                        CustomerService cs = new CustomerService();
                        CustomerMaster cm = cs.CustomerMastersById(objcc.CustomerId).FirstOrDefault();
                        cm.ProvinceName = GetProvince(Convert.ToInt32(cm.Province));
                        int DayDiff = Convert.ToDateTime(objcc.NextPayDate).Date.Subtract(objcc.CreatedDate.Date).Days;
                        string MailTemplate = System.IO.File.ReadAllText(Server.MapPath("~/PAYDAYLOANMARTCONTRACTblank.html"));
                        CompanyService cmp = new CompanyService();
                        Model.CompanyStore CompanyStores = cmp.CompanyStores.Where(p => p.Id == objcc.ShopStoreId).FirstOrDefault();
                        decimal APR = Math.Round(Convert.ToDecimal((15 * 365 / (DayDiff == 0 ? 1 : DayDiff))), 2);
                        if (CompanyStores != null)
                        {
                            MailTemplate = MailTemplate.Replace("@storeaddress", CompanyStores.Name + " O/A " + CompanyStores.Businessname + "<br/>" + CompanyStores.NewAddress + "<br/>" + CompanyStores.City + " , " + GetProvince(Convert.ToInt32(CompanyStores.Province)) + " , " + CompanyStores.PostCode + "<br/>Phone:" + CompanyStores.PhoneNo + ", Fax:" + CompanyStores.Fax + "<br/> Email:" + CompanyStores.Email);
                            MailTemplate = MailTemplate.Replace("@storesubaddress", CompanyStores.Address.Split(',')[0].ToString().Replace("$", " , "));
                            MailTemplate = MailTemplate.Replace("@storename", CompanyStores.Name + " O/A " + CompanyStores.Businessname);
                            MailTemplate = MailTemplate.Replace("@storecity", CompanyStores.City);
                        }
                        MailTemplate = MailTemplate.Replace("@IsMoreLoan", objcc.Last63DaysLoanCount >= 2 ? "YES" : "NO");
                        MailTemplate = MailTemplate.Replace("@APR", APR + " % ");
                        // MailTemplate = MailTemplate.Replace("@MonthlyIncome", objcc.MonthlyIncome.ToString());
                        if (objcc.MonthlyIncome <= 3000)
                        {
                            MailTemplate = MailTemplate.Replace("@MaximumLoanStatement", "Based on the information you provided to us, your net pay is $" + String.Format("{0:N}", objcc.MonthlyIncome) + " . The maximum amount we can lend you is $" + String.Format("{0:N}", (objcc.MonthlyIncome / 2)) + " which is 50 percent of your net pay.");
                        }
                        else
                        {
                            MailTemplate = MailTemplate.Replace("@MaximumLoanStatement", "Based on the information you provided to us, your net pay is $" + String.Format("{0:N}", objcc.MonthlyIncome) + " . The maximum amount we can lend you is either 50 percent of your net pay or $1,500.00 whichever one is lower.");
                        }
                        if (objcc.Last63DaysLoanCount >= 2)
                        {
                            MailTemplate = MailTemplate.Replace("@payment2", "$ " + String.Format("{0:N}",Math.Round(Convert.ToDecimal(objcc.Installment2Amount), 2))+ (objcc.Installment1Date.HasValue ? " , " + Convert.ToDateTime(objcc.Installment1Date).ToString("dddd, MMMM d, yyyy") : Convert.ToDateTime(objcc.NextPayDate).ToString("dddd, MMMM d, yyyy")));
                            MailTemplate = MailTemplate.Replace("@payment3", "$ " + String.Format("{0:N}",Math.Round(Convert.ToDecimal(objcc.Installment3Amount), 2))+ (objcc.Installment2Date.HasValue ? " , " + Convert.ToDateTime(objcc.Installment2Date).ToString("dddd, MMMM d, yyyy") : Convert.ToDateTime(objcc.NextPayDate).ToString("dddd, MMMM d, yyyy")));
                            //MailTemplate = MailTemplate.Replace("@payment3", (objcc.Installment3Amount.HasValue ? ("$ " + Math.Round(Convert.ToDecimal(objcc.Installment3Amount), 2).ToString() + " , " + (objcc.Installment2Date != null ? Convert.ToDateTime(objcc.Installment2Date).ToString("dddd, MMMM d, yyyy") : "")) : ""));
                            MailTemplate = MailTemplate.Replace("@payment1", Math.Round((Convert.ToDecimal(objcc.Installment1Amount)), 2).ToString());
                        }
                        else
                        {
                            MailTemplate = MailTemplate.Replace("@payment1", Math.Round((Convert.ToDecimal(objcc.DueAmount)), 2).ToString());
                            MailTemplate = MailTemplate.Replace("@payment2", "");
                            MailTemplate = MailTemplate.Replace("@payment3", "");
                            MailTemplate = MailTemplate.Replace("@installment1Date", "");
                            MailTemplate = MailTemplate.Replace("@installment2Date", "");
                        }
                        MailTemplate = MailTemplate.Replace("@loanpercentagerate", Convert.ToInt32(CompanyStores.InterestRate).ToString());
                        MailTemplate = MailTemplate.Replace("@nsfcharge", "$" + CompanyStores.NSFCharge.ToString());
                        MailTemplate = MailTemplate.Replace("@totaldueamount", objcc.DueAmount.ToString());
                        MailTemplate = MailTemplate.Replace("@previousloans", prevdates == string.Empty ? "NONE" : prevdates);
                        MailTemplate = MailTemplate.Replace("@customername", cm.FirstName + " " + cm.Mi + " " + cm.LastName);
                        MailTemplate = MailTemplate.Replace("@customerid", cm.Id.ToString());
                        MailTemplate = MailTemplate.Replace("@days", DayDiff.ToString());
                        MailTemplate = MailTemplate.Replace("@borrowedamount", objcc.LoanAmountApproved.ToString());
                        MailTemplate = MailTemplate.Replace("@costofborrowing", objcc.AdminFee.ToString());
                        MailTemplate = MailTemplate.Replace("@duedate", objcc.NextPayDate.ToString("dddd, MMMM d, yyyy"));
                        MailTemplate = MailTemplate.Replace("@currentdate", objcc.CreatedDate.ToString("dddd, MMMM d, yyyy"));
                        MailTemplate = MailTemplate.Replace("@address", cm.Address + "<br/>" + cm.City + "," + cm.ProvinceName + "<br/>" + cm.PostCode);
                        MailTemplate = MailTemplate.Replace("@phone", cm.CellPhone);
                        MailTemplate = MailTemplate.Replace("@workphone", cm.WorkPhone);
                        MailTemplate = MailTemplate.Replace("@Id", Request.QueryString["Id"].ToString());
                        MailTemplate = MailTemplate.Replace("@contractdate", objcc.CreatedDate.ToString("dd/MM/yyyy hh:mm tt"));
                        if (objcc.ShopStoreId == 1)
                        {
                            MailTemplate = MailTemplate.Replace("@faxnumber", "416-326-8810");
                        }
                        else
                        {
                            MailTemplate = MailTemplate.Replace("@faxnumber", CompanyStores.Fax);
                        }


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
                        content.InnerHtml = MailTemplate.ToString();
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
        public DateTime ConvertEasternTime(DateTime date)
        {
            TimeZoneInfo timeZoneInfo;
            DateTime dateTime;
            //Set the time zone information to US Mountain Standard Time 
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            //Get date and time in US Mountain Standard Time 
            dateTime = TimeZoneInfo.ConvertTime(date, timeZoneInfo);
            //Print out the date and time
            //Console.WriteLine(dateTime.ToString("yyyy-MM-dd HH-mm-ss"));
            return dateTime;
        }
    }
}