using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashLoanShop.Model
{
    public class CashCheque
    {

        public long Id { get; set; }

        public int CustomerId { get; set; }

        public int ChequeIssuerId { get; set; }

        public string ChequeType { get; set; }

        public int ShopStoreId { get; set; }

        public string ChequeNumber { get; set; }

        public decimal ChequeAmount { get; set; }

        public decimal AmountIssued { get; set; }

        public decimal Charges { get; set; }

        public decimal AdminFee { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CustomerName { get; set; }
        public string CompanyName { get; set; }

        public string CompanyStoreAddres { get; set; }

        public decimal? CustomPercentage { get; set; }
        public string StoreAddress { get; set; }
    }
}
