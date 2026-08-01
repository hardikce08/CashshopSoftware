using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashLoanShop.Model
{
    public class CustomerEmployment
    {

        public int Id { get; set; }

        public int? CustomerId { get; set; }

        public string EmployerName { get; set; }

        public string SupervisorName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

        public string PostCode { get; set; }

        public string EmployerDurationYears { get; set; }

        public string EmployerDurationMonth { get; set; }

        public string EmployerType { get; set; }

        public string SupervisorPhone { get; set; }

        public string SupervisorExt { get; set; }

        public string HRPhone { get; set; }

        public string HRExt { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }
    }

}
