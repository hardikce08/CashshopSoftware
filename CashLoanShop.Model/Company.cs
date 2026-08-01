using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashLoanShop.Model
{
    public class Company
    {
      
        public int Id { get; set; }
      
        public string Name { get; set; }
      
        public string Address { get; set; }
      
        public string City { get; set; }
      
        public string Province { get; set; }
 
        public string PostCode { get; set; }
    
        public string Phone { get; set; }
      
        public string BankTransitNumber { get; set; }
      
        public string BankAccountNumber { get; set; }
       
        public string Status { get; set; }
      
        public DateTime? CreatedDate { get; set; }
      
        public int? CreatedBy { get; set; }

    }

    public class CompanyStore
    {

        public int Id { get; set; }

        public string Address { get; set; }
        public string PhoneNo { get; set; }

        public string Email { get; set; }

        public string PostCode { get; set; }

        public string Name { get; set; }
        public string City { get; set; }
        public string Businessname { get; set; }
        public string Province { get; set; }
        public string Fax { get; set; }
        public string NewAddress { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal NSFCharge { get; set; }
        public decimal InterestRate { get; set; }
        public decimal MaximumTermLoanAmount { get; set; }
        public decimal AdminFeePercentage { get; set; }
        public decimal TermInterestRate { get; set; }
        public decimal TermLateInterestRate { get; set; }
        public decimal TermNSFCharge { get; set; }
    }
    public class LoanStatus
    {

        public int Id { get; set; }

        public string Status { get; set; }

        public int StatusId { get; set; }
    }
}
