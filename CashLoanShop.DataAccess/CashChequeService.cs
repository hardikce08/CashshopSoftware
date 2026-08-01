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
    public class CashChequeService : ConnectionHelper,IDisposable
    {
        EF.CashLoanShopEntities db = null;
        public CashChequeService()
        {
            db = new EF.CashLoanShopEntities(EntityConnectionString);
        }
        public CashChequeService(ObjectContext context)
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

        public IQueryable<CashCheque> CashCheques
        {
            get
            {
                return from c in db.CashCheques
                       select new CashCheque
                       {
                           Id = c.Id,
                           CustomerId = c.CustomerId,
                           ChequeIssuerId = c.ChequeIssuerId,
                           ChequeType = c.ChequeType,
                           ShopStoreId = c.ShopStoreId,
                           ChequeNumber = c.ChequeNumber,
                           ChequeAmount = c.ChequeAmount,
                           AmountIssued = c.AmountIssued,
                           Charges = c.Charges,
                           AdminFee = c.AdminFee,
                           CreatedBy = c.CreatedBy,
                           CreatedDate = c.CreatedDate,
                           CustomPercentage=c.CustomPercentage
                       };
            }
        }
        public List<CashCheque> CashChequesbyId(int Id)
        {
            
                var a= from c in db.CashCheques
                       where c.Id==Id
                       select new CashCheque
                       {
                           Id = c.Id,
                           CustomerId = c.CustomerId,
                           ChequeIssuerId = c.ChequeIssuerId,
                           ChequeType = c.ChequeType,
                           ShopStoreId = c.ShopStoreId,
                           ChequeNumber = c.ChequeNumber,
                           ChequeAmount = c.ChequeAmount,
                           AmountIssued = c.AmountIssued,
                           Charges = c.Charges,
                           AdminFee = c.AdminFee,
                           CreatedBy = c.CreatedBy,
                           CreatedDate = c.CreatedDate,
                           CustomPercentage = c.CustomPercentage
                       };
            return a.ToList();
        }
        public IQueryable<CashCheque> CashChequeGriddata
        {
            get
            {
                return from c in db.CashCheques
                       join p in db.Companies on c.ChequeIssuerId equals p.Id
                       join t in db.CustomerMasters on c.CustomerId equals t.Id

                       select new CashCheque
                       {
                           Id = c.Id,
                           AmountIssued = c.AmountIssued,
                           CustomerName = t.FirstName + " " + t.LastName,
                           CompanyName = p.Name,
                           ChequeNumber = c.ChequeNumber,
                           ChequeAmount = c.ChequeAmount,
                           ChequeType = c.ChequeType,
                           CreatedDate = c.CreatedDate,
                           CustomerId = c.CustomerId,
                           ChequeIssuerId = c.ChequeIssuerId,
                           ShopStoreId=c.ShopStoreId,
                           CustomPercentage=c.CustomPercentage,
                           Charges=c.Charges
                       };
            }
        }
        public List<CashCheque> CashChequeGriddatabyCustomerId(int Id)
        {
            
                var b= from c in db.CashCheques
                       join p in db.Companies on c.ChequeIssuerId equals p.Id
                       join t in db.CustomerMasters on c.CustomerId equals t.Id
                       where t.Id==Id
                       select new CashCheque
                       {
                           Id = c.Id,
                           AmountIssued = c.AmountIssued,
                           CustomerName = t.FirstName + " " + t.LastName,
                           CompanyName = p.Name,
                           ChequeNumber = c.ChequeNumber,
                           ChequeAmount = c.ChequeAmount,
                           ChequeType = c.ChequeType,
                           CreatedDate = c.CreatedDate,
                           CustomerId = c.CustomerId,
                           ChequeIssuerId = c.ChequeIssuerId,
                           ShopStoreId = c.ShopStoreId,
                           CustomPercentage = c.CustomPercentage,
                           Charges = c.Charges
                       };
                return b.ToList();
        }
        public IQueryable<CashCheque> CashChequeGriddataCompanyHistory
        {
            get
            {
                return from c in db.CashCheques
                       join p in db.Companies on c.ChequeIssuerId equals p.Id
                       join t in db.CustomerMasters on c.CustomerId equals t.Id
                       join s in db.CompanyStores on c.ShopStoreId equals s.Id
                       select new CashCheque
                       {
                           Id = c.Id,
                           AmountIssued = c.AmountIssued,
                           CustomerName = t.FirstName + " " + t.LastName,
                           CompanyName = p.Name,
                           ChequeNumber = c.ChequeNumber,
                           ChequeAmount = c.ChequeAmount,
                           ChequeType = c.ChequeType,
                           CreatedDate = c.CreatedDate,
                           CustomerId = c.CustomerId,
                           ChequeIssuerId = c.ChequeIssuerId,
                           ShopStoreId = c.ShopStoreId,
                           CustomPercentage = c.CustomPercentage,
                           Charges = c.Charges,
                           StoreAddress = s.Name + "<br/>" + s.Address.Replace("$", " , ") + "<br/>" + s.PhoneNo + "<br/>" + s.Email
                       };
            }
        }
        public List<CashCheque> CashChequeReportGriddata(int StoreId, DateTime FromDate, DateTime ToDate)
        {
            var data = from c in db.CashCheques
                       join p in db.Companies on c.ChequeIssuerId equals p.Id
                       join t in db.CustomerMasters on c.CustomerId equals t.Id
                       where c.ShopStoreId == StoreId  
                       //(Convert.ToDateTime(c.CreatedDate).Date >= FromDate && Convert.ToDateTime(c.CreatedDate).Date <= ToDate)
                 
                       select new CashCheque
                       {
                           Id = c.Id,
                           AmountIssued = c.AmountIssued,
                           CustomerName = t.FirstName + " " + t.LastName,
                           CompanyName = p.Name,
                           ChequeNumber = c.ChequeNumber,
                           ChequeAmount = c.ChequeAmount,
                           ChequeType = c.ChequeType,
                           CreatedDate = c.CreatedDate,
                           CustomerId = c.CustomerId,
                           ChequeIssuerId = c.ChequeIssuerId,
                           Charges=c.Charges,
                           CustomPercentage=c.CustomPercentage
                       };

            return data.ToList();

        }

        public void CashCheque_InsertOrUpdate(CashCheque c)
        {
            if (c.Id == 0)
            {
                var i = new EF.CashCheque
                {
                    CustomerId = c.CustomerId,
                    ChequeIssuerId = c.ChequeIssuerId,
                    ChequeType = c.ChequeType,
                    ShopStoreId = c.ShopStoreId,
                    ChequeNumber = c.ChequeNumber,
                    ChequeAmount = c.ChequeAmount,
                    AmountIssued = c.AmountIssued,
                    Charges = c.Charges,
                    AdminFee = c.AdminFee,
                    CreatedBy = c.CreatedBy,
                    CreatedDate = c.CreatedDate,
                    CustomPercentage = c.CustomPercentage == null ? 0 : c.CustomPercentage
                };

                db.CashCheques.AddObject(i);
                db.SaveChanges();
                c.Id = i.Id;
            }


            else
            {
                var u = db.CashCheques.Where(p => p.Id == c.Id).Single();
                u.CustomerId = c.CustomerId;
                u.ChequeIssuerId = c.ChequeIssuerId;
                u.ChequeType = c.ChequeType;
                u.ShopStoreId = c.ShopStoreId;
                u.ChequeNumber = c.ChequeNumber;
                u.ChequeAmount = c.ChequeAmount;
                u.AmountIssued = c.AmountIssued;
                u.Charges = c.Charges;
                u.AdminFee = c.AdminFee;
                u.CreatedBy = c.CreatedBy;
                u.CreatedDate = c.CreatedDate;
                u.CustomPercentage = c.CustomPercentage == null ? 0 : c.CustomPercentage;
                db.SaveChanges();
            }
        }

        public void Dispose() // helper finalize function
        {
            // here you can free the resources you allocated explicitly
            System.GC.SuppressFinalize(this);
        }
    }
}
