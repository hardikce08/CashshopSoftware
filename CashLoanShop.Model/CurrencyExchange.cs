using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashLoanShop.Model
{
    public class CurrencyExchange
    {

        public long Id { get; set; }

        public int CustomerId { get; set; }

        public string TransactionType { get; set; }

        public string CurrencyType { get; set; }

        public int ShopStoreId { get; set; }

        public decimal ExchangeRate { get; set; }

        public decimal ExchangeAmount { get; set; }

        public decimal ServiceCharge { get; set; }

        public decimal DueAmount { get; set; }

        public decimal ConvertedAmount { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CustomerName { get; set; }

        public string StoreAddress { get; set; }
    }
    public class CurrencyTypeMaster
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }
    }
}
