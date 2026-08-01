using CashLoanShop.Model;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF = CashLoanShop.DataModel;
namespace CashLoanShop.DataAccess
{
   public class CurrencyExchangeService : ConnectionHelper,IDisposable
    {
       
         EF.CashLoanShopEntities db = null;
        public CurrencyExchangeService()
        {
            db = new EF.CashLoanShopEntities(EntityConnectionString);
        }
        public CurrencyExchangeService(ObjectContext context)
        {
            db = context as EF.CashLoanShopEntities;
        }
        public ObjectContext DbContext
        {
            get
            {
                return db as ObjectContext;
            }
        }

        public IQueryable<CurrencyExchange> CurrencyExchanges
        {
            get
            {
                return from c in db.CurrencyExchanges
                       select new CurrencyExchange
                       {
                           Id = c.Id,
                           CustomerId = c.CustomerId,
                           TransactionType = c.TransactionType,
                           CurrencyType = c.CurrencyType,
                           ShopStoreId = c.ShopStoreId,
                           ExchangeRate = c.ExchangeRate,
                           ExchangeAmount = c.ExchangeAmount,
                           ServiceCharge = c.ServiceCharge,
                           DueAmount = c.DueAmount,
                           ConvertedAmount = c.ConvertedAmount,
                           CreatedBy = c.CreatedBy,
                           CreatedDate = c.CreatedDate,
                       };
            }
        }

        public IQueryable<CurrencyExchange> CurrencyExchangesGriddata
        {
            get
            {
                return from c in db.CurrencyExchanges
                       join t in db.CustomerMasters on c.CustomerId equals t.Id
                       select new CurrencyExchange
                       {
                           Id = c.Id,
                           DueAmount = c.DueAmount,
                           CustomerName = t.FirstName + " " + t.LastName,
                           ExchangeAmount = c.ExchangeAmount,
                           ExchangeRate = c.ExchangeRate,
                           ConvertedAmount = c.ConvertedAmount,
                           CreatedDate = c.CreatedDate,
                           CustomerId = c.CustomerId,
                           CurrencyType = c.CurrencyType,
                           TransactionType = c.TransactionType,
                           ServiceCharge=c.ServiceCharge,
                           ShopStoreId=c.ShopStoreId,
                       };
            }
        }
        public List<CurrencyExchange> CurrencyExchangesGriddatabyCustomerId(int ID)
        {
           
                var a= from c in db.CurrencyExchanges
                       join t in db.CustomerMasters on c.CustomerId equals t.Id
                       where t.Id==ID
                       select new CurrencyExchange
                       {
                           Id = c.Id,
                           DueAmount = c.DueAmount,
                           CustomerName = t.FirstName + " " + t.LastName,
                           ExchangeAmount = c.ExchangeAmount,
                           ExchangeRate = c.ExchangeRate,
                           ConvertedAmount = c.ConvertedAmount,
                           CreatedDate = c.CreatedDate,
                           CustomerId = c.CustomerId,
                           CurrencyType = c.CurrencyType,
                           TransactionType = c.TransactionType,
                           ServiceCharge = c.ServiceCharge,
                           ShopStoreId = c.ShopStoreId,
                       };
                return a.ToList();
        }
        public List<CurrencyExchange> CurrencyExchangesReportdata(int StoreId, DateTime FromDate, DateTime ToDate)
        {
            
                var data= from c in db.CurrencyExchanges
                       join t in db.CustomerMasters on c.CustomerId equals t.Id
                       where c.ShopStoreId == StoreId 
                       //&& (c.CreatedDate >= FromDate && c.CreatedDate <= ToDate)
                 
                       select new CurrencyExchange
                       {
                           Id = c.Id,
                           DueAmount = c.DueAmount,
                           CustomerName = t.FirstName + " " + t.LastName,
                           ExchangeAmount = c.ExchangeAmount,
                           ExchangeRate = c.ExchangeRate,
                           ConvertedAmount = c.ConvertedAmount,
                           CreatedDate = c.CreatedDate,
                           CustomerId = c.CustomerId,
                           CurrencyType = c.CurrencyType,
                           TransactionType = c.TransactionType,
                           ServiceCharge = c.ServiceCharge,
                       };
                return data.ToList();
        }


        public void CurrencyExchange_InsertOrUpdate(CurrencyExchange c)
        {
            if (c.Id == 0)
            {
                var i = new EF.CurrencyExchange
                {
                    CustomerId = c.CustomerId,
                    TransactionType = c.TransactionType,
                    CurrencyType = c.CurrencyType,
                    ShopStoreId = c.ShopStoreId,
                    ExchangeRate = c.ExchangeRate,
                    ExchangeAmount = c.ExchangeAmount,
                    ServiceCharge = c.ServiceCharge,
                    DueAmount = c.DueAmount,
                    ConvertedAmount = c.ConvertedAmount,
                    CreatedBy = c.CreatedBy,
                    CreatedDate = c.CreatedDate,
                };

                db.CurrencyExchanges.AddObject(i);
                db.SaveChanges();
                c.Id = i.Id;
            }


            else
            {
                var u = db.CurrencyExchanges.Where(p => p.Id == c.Id).Single();
                u.CustomerId = c.CustomerId;
                u.TransactionType = c.TransactionType;
                u.CurrencyType = c.CurrencyType;
                u.ShopStoreId = c.ShopStoreId;
                u.ExchangeRate = c.ExchangeRate;
                u.ExchangeAmount = c.ExchangeAmount;
                u.ServiceCharge = c.ServiceCharge;
                u.DueAmount = c.DueAmount;
                u.ConvertedAmount = c.ConvertedAmount;
                u.CreatedBy = c.CreatedBy;
                u.CreatedDate = c.CreatedDate;

                db.SaveChanges();
            }
        }

        #region CurrencyTypeMaster
        public IQueryable<CurrencyTypeMaster> CurrencyTypeMasters
        {
            get
            {
                return from c in db.CurrencyTypeMasters
                       select new CurrencyTypeMaster
                       {
                           Id = c.Id,
                           Name = c.Name,
                           Code=c.Code,
                       };
            }
        }
        #endregion

        public void Dispose() // helper finalize function
        {
            // here you can free the resources you allocated explicitly
            System.GC.SuppressFinalize(this);
        }
    }
}
