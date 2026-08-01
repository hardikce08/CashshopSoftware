using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashLoanShop.Model
{
    public class CustomerBankInformation
    {

        public int Id { get; set; }

        public int? CustomerId { get; set; }

        public string BankName { get; set; }

        public string AccountNumber { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

        public string PostCode { get; set; }

        public int? NoofSNF { get; set; }

        public bool? IsBankrutcy { get; set; }

        public bool? IsAssignments { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public string TransitNo { get; set; }

        public string InstitutionNo { get; set; }
    }
}
