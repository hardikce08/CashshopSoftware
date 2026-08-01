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
    public class CompanyService : ConnectionHelper,IDisposable
    {
        EF.CashLoanShopEntities db = null;
        public CompanyService()
        {
            db = new EF.CashLoanShopEntities(EntityConnectionString);
        }
        public CompanyService(ObjectContext context)
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
        public IQueryable<Company> Companys
        {
            get
            {
                return from c in db.Companies
                       select new Company
                       {
                           Id = c.Id,
                           Name = c.Name,
                           Address = c.Address,
                           City = c.City,
                           Province = c.Province,
                           PostCode = c.PostCode,
                           Phone = c.Phone,
                           BankTransitNumber = c.BankTransitNumber,
                           BankAccountNumber = c.BankAccountNumber,
                           Status = c.Status,
                           CreatedDate = c.CreatedDate,
                           CreatedBy = c.CreatedBy,
                       };
            }
        }

        public void Company_InsertOrUpdate(Company c)
        {
            if (c.Id == 0)
            {
                var i = new EF.Company
                {
                    Name = c.Name,
                    Address = c.Address,
                    City = c.City,
                    Province = c.Province,
                    PostCode = c.PostCode,
                    Phone = c.Phone,
                    BankTransitNumber = c.BankTransitNumber,
                    BankAccountNumber = c.BankAccountNumber,
                    Status = c.Status,
                    CreatedDate = c.CreatedDate,
                    CreatedBy = c.CreatedBy,
                };

                db.Companies.AddObject(i);
                db.SaveChanges();
                c.Id = i.Id;
            }


            else
            {
                var u = db.Companies.Where(p => p.Id == c.Id).Single();
                u.Name = c.Name;
                u.Address = c.Address;
                u.City = c.City;
                u.Province = c.Province;
                u.PostCode = c.PostCode;
                u.Phone = c.Phone;
                u.BankTransitNumber = c.BankTransitNumber;
                u.BankAccountNumber = c.BankAccountNumber;
                u.Status = c.Status;


                db.SaveChanges();
            }
        }

        #region CompanyStore
        public IQueryable<CompanyStore> CompanyStores
        {
            get
            {
                return from c in db.CompanyStores
                       select new CompanyStore
                       {
                           Id = c.Id,
                           Address = c.Address,
                           PhoneNo = c.PhoneNo,
                           Email = c.Email,
                           PostCode = c.PostCode,
                           Name = c.Name,
                           City = c.City,
                           Businessname = c.BusinessName,
                           Province = c.Province,
                           Fax = c.Fax,
                           NewAddress = c.NewAddress,
                           CreatedDate=c.CreatedDate,
                           NSFCharge=c.NSFCharge,
                           InterestRate=c.InterestRate,
                           MaximumTermLoanAmount=c.MaximumTermLoanAmount,
                           AdminFeePercentage=c.AdminFeePercentage,
                           TermInterestRate=c.TermInterestRate,
                           TermLateInterestRate=c.TermLateInterestRate,
                           TermNSFCharge=c.TermNSFCharge
                       };
            }
        }
        public CompanyStore CompanyStoresbyId(int Id)
        {
             
                var a=  from c in db.CompanyStores
                        where c.Id==Id
                       select new CompanyStore
                       {
                           Id = c.Id,
                           Address = c.Address,
                           PhoneNo = c.PhoneNo,
                           Email = c.Email,
                           PostCode = c.PostCode,
                           Name = c.Name,
                           City = c.City,
                           Businessname = c.BusinessName,
                           Province = c.Province,
                           Fax = c.Fax,
                           NewAddress = c.NewAddress,
                           CreatedDate = c.CreatedDate,
                           NSFCharge = c.NSFCharge,
                           InterestRate = c.InterestRate,
                           MaximumTermLoanAmount=c.MaximumTermLoanAmount,
                           AdminFeePercentage=c.AdminFeePercentage,
                           TermInterestRate=c.TermInterestRate,
                           TermLateInterestRate=c.TermLateInterestRate,
                           TermNSFCharge=c.TermNSFCharge
                       };
                return a.FirstOrDefault();
        }
        public void CompanyStore_InsertOrUpdate(CompanyStore c)
        {
            if (c.Id == 0)
            {
                var i = new EF.CompanyStore
                {
                    Address = c.Address,
                    PhoneNo = c.PhoneNo,
                    Email = c.Email,
                    PostCode = c.PostCode,
                    Name = c.Name,
                    City = c.City,
                    BusinessName = c.Businessname,
                    Province = c.Province,
                    Fax = c.Fax,
                    NewAddress = c.NewAddress,
                    CreatedDate=c.CreatedDate,
                    NSFCharge=c.NSFCharge,
                    InterestRate=c.InterestRate,
                    MaximumTermLoanAmount=c.MaximumTermLoanAmount,
                    AdminFeePercentage=c.AdminFeePercentage,
                    TermInterestRate=c.TermInterestRate,
                    TermLateInterestRate=c.TermLateInterestRate,
                    TermNSFCharge=c.TermNSFCharge
                };

                db.CompanyStores.AddObject(i);
                db.SaveChanges();
                c.Id = i.Id;
            }


            else
            {
                var u = db.CompanyStores.Where(p => p.Id == c.Id).Single();
                u.Address = c.Address;
                u.PhoneNo = c.PhoneNo;
                u.Email = c.Email;
                u.PostCode = c.PostCode;
                u.Name = c.Name;
                u.City = c.City;
                u.BusinessName = c.Businessname;
                u.Province = c.Province;
                u.Fax = c.Fax;
                u.NewAddress = c.NewAddress;
                u.NSFCharge = c.NSFCharge;
                u.InterestRate = c.InterestRate;
                u.MaximumTermLoanAmount = c.MaximumTermLoanAmount;
                u.AdminFeePercentage = c.AdminFeePercentage;
                u.TermInterestRate = c.TermInterestRate;
                u.TermLateInterestRate = c.TermLateInterestRate;
                u.TermNSFCharge = c.TermNSFCharge;
                db.SaveChanges();
            }
        }

        #endregion

        public IQueryable<LoanStatus> LoanStatus
        {
            get
            {
                return from l in db.LoanStatus
                       select new LoanStatus
                       {
                           Id = l.Id,
                           Status = l.Status,
                           StatusId = l.StatusId,
                       };
            }
        }
        public void Dispose() // helper finalize function
        {
            // here you can free the resources you allocated explicitly
            System.GC.SuppressFinalize(this);
        }
    }
}
