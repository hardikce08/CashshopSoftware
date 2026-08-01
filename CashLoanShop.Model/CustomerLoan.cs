using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashLoanShop.Model
{
    public class CustomerLoan
    {

        public long Id { get; set; }

        public int CustomerId { get; set; }

        public int ShopStoreId { get; set; }

        public decimal LoanAmountApplied { get; set; }

        public DateTime NextPayDate { get; set; }

        public string LateInterestRate { get; set; }

        public bool IsLoanApproved { get; set; }

        public decimal LoanAmountApproved { get; set; }

        public string PaymentOption { get; set; }

        public decimal DueAmount { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public decimal AdminFee { get; set; }

        public string CustomerName { get; set; }

        public string LoanStatus { get; set; }

        public int StatusUpdatedby { get; set; }

        public string LoanType { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string StoreAddress { get; set; }

        public string OverDueReason { get; set; }
        public string ModeofPayment { get; set; }

        public int? OverdueCount { get; set; }
        public decimal? RemainingAmount { get; set; }
        public decimal? DiscountAmount { get; set; }
        public string PartialPayment { get; set; }
        public string LoanDeniedReason { get; set; }
        public decimal? OverDueLoanAmount { get; set; }
        public decimal? LateInterestCharge { get; set; }
        public decimal? NSFCharge { get; set; }
        public string Settlement { get; set; }
        public decimal? LastDueAmount { get; set; }
        public decimal PartialAmount { get; set; }
        public string LastStatus { get; set; }
        public DateTime PartialLoanCreatedDate { get; set; }
        public string PartialPaymentMethod { get; set; }
        public DateTime? Installment1Date { get; set; }
        public DateTime? Installment2Date { get; set; }
        public decimal? MonthlyIncome { get; set; }
        public int? Last63DaysLoanCount { get; set; }
        public decimal? Installment1Amount { get; set; }
        public decimal? Installment2Amount { get; set; }
        public decimal? Installment3Amount { get; set; }
        public string LoanPaymentType { get; set; }
        public string DisplayInstallmentAmount { get; set; }
        public string DisplayPartialAmount { get; set; }
        public string DisplayInstallmentDate { get; set; }
    }

    public class LoanPartialPayment
    {

        public int Id { get; set; }

        public int LoanId { get; set; }

        public decimal PartialAmount { get; set; }

        public DateTime CreatedDate { get; set; }

        public int Createdby { get; set; }
        public decimal? IntrestCharge { get; set; }
        public decimal? DueAmount { get; set; }
        public string PartialPaymentMethod { get; set; }
    }

    public enum LoanType
    {
        Not_Selected=0,
        Weekly = 1,
        Bi_Weekly = 2,
        Twice_Monthly = 3,
        Monthly = 4,
        No_Fix_Date = 5

    }
    public class CustomerLoanException
    {

        public int Id { get; set; }

        public int? CustomerId { get; set; }

        public int? LoanId { get; set; }
    }
    public class LoanDenyReason
    {

        public int Id { get; set; }

        public int? LoanId { get; set; }

        public string DenyReason { get; set; }
    }

    public class CustomerTermLoan
    {

        public long Id { get; set; }

        public int CustomerId { get; set; }

        public int ShopStoreId { get; set; }

        public decimal LoanAmountApplied { get; set; }

        public DateTime DueDate { get; set; }

        public string LateInterestRate { get; set; }

        public bool IsLoanApproved { get; set; }

        public decimal LoanAmountApproved { get; set; }

        public string PaymentOption { get; set; }
        public string ModeofPayment { get; set; }
        public decimal DueAmount { get; set; }
        public decimal? RemainingAmount { get; set; }
        public decimal? DiscountAmount { get; set; }
        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public decimal AdminFee { get; set; }

        public string LoanStatus { get; set; }

        public int StatusUpdatedby { get; set; }

        public string LoanType { get; set; }

        public DateTime UpdatedDate { get; set; }

        public DateTime? FirstInstallmentDate { get; set; }

        public DateTime? SecondInstallmentDate { get; set; }

        public string LoanTerm { get; set; }

        public string CustomerName { get; set; }

        public string LoanDeniedReason { get; set; }
        public string PartialPayment { get; set; }
        public string StoreAddress { get; set; }
        public string ReportText { get; set; }
        public decimal InstallmentAmount { get; set; }
        public decimal InterestCharge { get; set; }
        public decimal LateInterestCharge { get; set; }
        public decimal NSFCharge { get; set; }
        public int NoofInstallment { get; set; }

        public decimal InstallmentPrincipalAmount { get; set; }
        public decimal InstallmentInterestAmount { get; set; }
        public DateTime? LoanOverDueDate { get; set; }
        public string LastStatus { get; set; }
        public decimal TotalReceivedAmount { get; set; }
    }
    public class TermLoanPartialPayment
    {

        public int Id { get; set; }

        public int LoanId { get; set; }

        public decimal PartialAmount { get; set; }

        public DateTime CreatedDate { get; set; }

        public int Createdby { get; set; }
        public decimal? IntrestCharge { get; set; }
        public decimal? DueAmount { get; set; }
        public string PartialPaymentMethod { get; set; }
        public decimal InstallmentPrincipalAmount { get; set; }
        public decimal InstallmentInterestAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Balance { get; set; }
    }
    public class TermLoanSchedule
    {

        public int Id { get; set; }

        public int LoanId { get; set; }

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public int InstallmentNo { get; set; }

        public decimal Principal { get; set; }

        public decimal Interest { get; set; }

        public decimal Balance { get; set; }

        public bool IsPaid { get; set; }
    }
}
