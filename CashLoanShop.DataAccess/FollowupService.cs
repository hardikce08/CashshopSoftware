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
    public class FollowupService : ConnectionHelper, IDisposable
    {
         EF.CashLoanShopEntities db = null;
        public FollowupService()
        {
            db = new EF.CashLoanShopEntities(EntityConnectionString);
        }
        public FollowupService(ObjectContext context)
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

        public IQueryable<CustomerFollowup> CustomerFollowups
        {
            get
            {
                return from c in db.CustomerFollowups
                       select new CustomerFollowup
                       {
                           Id = c.Id,
                           CustomerId = c.CustomerId,
                           FollowupDate = c.FollowupDate,
                           FollowupTime = c.FollowupTime,
                           FollowupCode = c.FollowupCode,
                           FollowupDoneBy = c.FollowupDoneBy,
                           NextFolloupDate = c.NextFolloupDate,
                           NextFollowupTime = c.NextFollowupTime,
                           Comments = c.Comments,
                           FinalStatus = c.FinalStatus,
                           CreatedDate = c.CreatedDate,
                           CreatedBy = c.CreatedBy,
                       };
            }
        }

        public void CustomerFollowup_InsertOrUpdate(CustomerFollowup c)
        {
            if (c.Id == 0)
            {
                var i = new EF.CustomerFollowup
                {
                    CustomerId = c.CustomerId,
                    FollowupDate = c.FollowupDate,
                    FollowupTime = c.FollowupTime,
                    FollowupCode = c.FollowupCode,
                    FollowupDoneBy = c.FollowupDoneBy,
                    NextFolloupDate = c.NextFolloupDate,
                    NextFollowupTime = c.NextFollowupTime,
                    Comments = c.Comments,
                    FinalStatus = c.FinalStatus,
                    CreatedDate = c.CreatedDate,
                    CreatedBy = c.CreatedBy,
                };

                db.CustomerFollowups.AddObject(i);
                db.SaveChanges();
                c.Id = i.Id;
            }


            else
            {
                var u = db.CustomerFollowups.Where(p => p.Id == c.Id).Single();
                u.CustomerId = c.CustomerId;
                u.FollowupDate = c.FollowupDate;
                u.FollowupTime = c.FollowupTime;
                u.FollowupCode = c.FollowupCode;
                u.FollowupDoneBy = c.FollowupDoneBy;
                u.NextFolloupDate = c.NextFolloupDate;
                u.NextFollowupTime = c.NextFollowupTime;
                u.Comments = c.Comments;
                u.FinalStatus = c.FinalStatus;
                u.CreatedDate = c.CreatedDate;
                u.CreatedBy = c.CreatedBy;

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
