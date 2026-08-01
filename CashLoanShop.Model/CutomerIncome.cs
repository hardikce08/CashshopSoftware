using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashLoanShop.Model
{
    public class CustomerIncome
    {

        public int Id { get; set; }

        public int? CustomerId { get; set; }

        public decimal? TakehomePay { get; set; }

        public bool? IsComputerized { get; set; }

        public string FrequencyofPay { get; set; }

        public bool? IsDirectDeposit { get; set; }

        public DateTime? Createdate { get; set; }

        public int? CreatedBy { get; set; }
    }
}
