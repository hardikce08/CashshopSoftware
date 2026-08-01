using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashLoanShop.Model
{
    public class CustomerReference
    {

        public int Id { get; set; }

        public int? CustomerId { get; set; }

        public string Reference1Name { get; set; }

        public string Reference1Phone { get; set; }

        public string Reference1Relation { get; set; }

        public string Reference2Name { get; set; }

        public string Reference2Phone { get; set; }

        public string Reference2Relation { get; set; }

        public string Reference3Name { get; set; }

        public string Reference3Phone { get; set; }

        public string Reference3Relation { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }
    }
}
