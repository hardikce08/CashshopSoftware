using CashLoanShop.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF = CashLoanShop.DataModel;
namespace CashLoanShop.DataAccess
{
    public class CustomerLoanService : ConnectionHelper,IDisposable
    {
        EF.CashLoanShopEntities db = null;
        public CustomerLoanService()
        {
            db = new EF.CashLoanShopEntities(EntityConnectionString);
        }
        public CustomerLoanService(ObjectContext context)
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
        public IQueryable<CustomerLoan> CustomerLoans
        {
            get
            {
                return from c in db.CustomerLoans
                       select new CustomerLoan
                       {
                           Id = c.Id,
                           CustomerId = c.CustomerId,
                           ShopStoreId = c.ShopStoreId,
                           LoanAmountApplied = c.LoanAmountApplied,
                           NextPayDate = c.NextPayDate,
                           LateInterestRate = c.LateInterestRate,
                           IsLoanApproved = c.IsLoanApproved,
                           LoanAmountApproved = c.LoanAmountApproved,
                           PaymentOption = c.PaymentOption,
                           DueAmount = c.DueAmount,
                           CreatedBy = c.CreatedBy,
                           CreatedDate = c.CreatedDate,
                           AdminFee = c.AdminFee,
                           LoanStatus = c.LoanStatus,
                           StatusUpdatedby = c.StatusUpdatedby,
                           LoanType = c.LoanType,
                           OverdueCount = c.OverdueCount,
                           OverDueReason = c.OverDueReason,
                           ModeofPayment = c.ModeofPayment,
                           RemainingAmount = c.RemainingAmount == null ? 0 : c.RemainingAmount,
                           UpdatedDate = c.UpdatedDate,
                           NSFCharge = c.NSFCharge,
                           LateInterestCharge = c.LateInterestCharge,
                           Settlement = c.Settlement,
                           DiscountAmount = c.DiscountAmount,
                           LastDueAmount = c.LastDueAmount,
                           LastStatus = c.LastStatus,
                           MonthlyIncome = c.MonthlyIncome,
                           Installment1Date = c.Installment1Date,
                           Installment2Date = c.Installment2Date,
                           Last63DaysLoanCount = c.Last63DaysLoanCount,
                           Installment1Amount = c.Installment1Amount,
                           Installment2Amount = c.Installment2Amount,
                           Installment3Amount = c.Installment3Amount,
                           LoanPaymentType=c.LoanPaymentType
                           //LoanDeniedReason=c.LoanDeniedReason,
                       };
            }
        }
        public List<CustomerLoan> CustomerLoansbyId(int Id)
        {
              
                var a= from c in db.CustomerLoans
                       where c.Id==Id
                       select new CustomerLoan
                       {
                           Id = c.Id,
                           CustomerId = c.CustomerId,
                           ShopStoreId = c.ShopStoreId,
                           LoanAmountApplied = c.LoanAmountApplied,
                           NextPayDate = c.NextPayDate,
                           LateInterestRate = c.LateInterestRate,
                           IsLoanApproved = c.IsLoanApproved,
                           LoanAmountApproved = c.LoanAmountApproved,
                           PaymentOption = c.PaymentOption,
                           DueAmount = c.DueAmount,
                           CreatedBy = c.CreatedBy,
                           CreatedDate = c.CreatedDate,
                           AdminFee = c.AdminFee,
                           LoanStatus = c.LoanStatus,
                           StatusUpdatedby = c.StatusUpdatedby,
                           LoanType = c.LoanType,
                           OverdueCount = c.OverdueCount,
                           OverDueReason = c.OverDueReason,
                           ModeofPayment = c.ModeofPayment,
                           RemainingAmount = c.RemainingAmount == null ? 0 : c.RemainingAmount,
                           UpdatedDate = c.UpdatedDate,
                           NSFCharge = c.NSFCharge,
                           LateInterestCharge = c.LateInterestCharge,
                           Settlement = c.Settlement,
                           DiscountAmount = c.DiscountAmount,
                           LastDueAmount = c.LastDueAmount,
                           LastStatus = c.LastStatus,
                           MonthlyIncome = c.MonthlyIncome,
                           Installment1Date = c.Installment1Date,
                           Installment2Date = c.Installment2Date,
                           Last63DaysLoanCount = c.Last63DaysLoanCount,
                           Installment1Amount = c.Installment1Amount,
                           Installment2Amount = c.Installment2Amount,
                           Installment3Amount = c.Installment3Amount,
                           LoanPaymentType = c.LoanPaymentType
                           //LoanDeniedReason=c.LoanDeniedReason,
                       };
            return a.ToList();
        }

        public List<CustomerLoan> CustomerLoansbyCustomerId(int Id)
        {
            var a = from c in db.CustomerLoans
                    where c.CustomerId == Id
                    select new CustomerLoan
                    {
                        Id = c.Id,
                        CustomerId = c.CustomerId,
                        ShopStoreId = c.ShopStoreId,
                        LoanAmountApplied = c.LoanAmountApplied,
                        NextPayDate = c.NextPayDate,
                        LateInterestRate = c.LateInterestRate,
                        IsLoanApproved = c.IsLoanApproved,
                        LoanAmountApproved = c.LoanAmountApproved,
                        PaymentOption = c.PaymentOption,
                        DueAmount = c.DueAmount,
                        CreatedBy = c.CreatedBy,
                        CreatedDate = c.CreatedDate,
                        AdminFee = c.AdminFee,
                        LoanStatus = c.LoanStatus,
                        StatusUpdatedby = c.StatusUpdatedby,
                        LoanType = c.LoanType,
                        OverdueCount = c.OverdueCount,
                        OverDueReason = c.OverDueReason,
                        ModeofPayment = c.ModeofPayment,
                        RemainingAmount = c.RemainingAmount == null ? 0 : c.RemainingAmount,
                        UpdatedDate = c.UpdatedDate,
                        NSFCharge = c.NSFCharge,
                        LateInterestCharge = c.LateInterestCharge,
                        Settlement = c.Settlement,
                        DiscountAmount = c.DiscountAmount,
                        LastDueAmount = c.LastDueAmount,
                        LastStatus = c.LastStatus,
                        MonthlyIncome = c.MonthlyIncome,
                        Installment1Date = c.Installment1Date,
                        Installment2Date = c.Installment2Date,
                        Last63DaysLoanCount = c.Last63DaysLoanCount,
                        Installment1Amount = c.Installment1Amount,
                        Installment2Amount = c.Installment2Amount,
                        Installment3Amount = c.Installment3Amount,
                        LoanPaymentType = c.LoanPaymentType
                        //LoanDeniedReason=c.LoanDeniedReason,
                    };
            return a.ToList();
        }
        public IQueryable<LoanDenyReason> LoanDenyReasons
        {
            get
            {
                return from c in db.LoanDenyReasons
                       select new LoanDenyReason
                       {
                           Id = c.Id,
                           LoanId = c.LoanId,
                           DenyReason = c.DenyReason,
                       };
            }
        }
        public IQueryable<CustomerLoan> CustomerLoanGriddata
        {
            get
            {
                return from c in db.CustomerLoans
                       join t in db.CustomerMasters on c.CustomerId equals t.Id

                       select new CustomerLoan
                       {
                           Id = c.Id,
                           DueAmount = c.DueAmount,
                           CustomerName = t.FirstName + " " + t.LastName,
                           LateInterestRate = c.LateInterestRate,
                           LoanAmountApplied = c.LoanAmountApplied,
                           LoanAmountApproved = c.LoanAmountApproved,
                           CreatedDate = c.CreatedDate,
                           CustomerId = c.CustomerId,
                           NextPayDate = c.NextPayDate,
                           PaymentOption = c.PaymentOption,
                           LoanStatus = c.LoanStatus,
                       };
            }
        }


        public IQueryable<CustomerLoan> CustomerReportGridData
        {
            get
            {
                return (from c in db.CustomerLoans
                        join t in db.CustomerMasters on c.CustomerId equals t.Id
                        select new CustomerLoan
                        {
                            Id = c.Id,
                            DueAmount = c.DueAmount,
                            CustomerName = t.FirstName + " " + t.LastName,
                            LateInterestRate = c.LateInterestRate,
                            LoanAmountApplied = c.LoanAmountApplied,
                            LoanAmountApproved = c.LoanAmountApproved,
                            CreatedDate = c.CreatedDate,
                            CustomerId = c.CustomerId,
                            NextPayDate = c.NextPayDate,
                            PaymentOption = c.PaymentOption,
                            LoanStatus = c.LoanStatus,
                            UpdatedDate = c.UpdatedDate,
                            LoanDeniedReason = "",
                            OverdueCount = c.OverdueCount,
                            PartialPayment = "",
                            OverDueReason = c.OverDueReason,
                            OverDueLoanAmount = 0,
                            LateInterestCharge = c.LateInterestCharge,
                            NSFCharge = c.NSFCharge,
                            ShopStoreId = c.ShopStoreId,
                            Last63DaysLoanCount = c.Last63DaysLoanCount
                        });
            }
        }
        public List<CustomerLoan> CustomerReportGridDatabyStoreId(int Id)
        {
             
                var a= (from c in db.CustomerLoans
                        join t in db.CustomerMasters on c.CustomerId equals t.Id
                        where c.ShopStoreId==Id
                        select new CustomerLoan
                        {
                            Id = c.Id,
                            DueAmount = c.DueAmount,
                            CustomerName = t.FirstName + " " + t.LastName,
                            LateInterestRate = c.LateInterestRate,
                            LoanAmountApplied = c.LoanAmountApplied,
                            LoanAmountApproved = c.LoanAmountApproved,
                            CreatedDate = c.CreatedDate,
                            CustomerId = c.CustomerId,
                            NextPayDate = c.NextPayDate,
                            PaymentOption = c.PaymentOption,
                            LoanStatus = c.LoanStatus,
                            UpdatedDate = c.UpdatedDate,
                            LoanDeniedReason = "",
                            OverdueCount = c.OverdueCount,
                            PartialPayment = "",
                            OverDueReason = c.OverDueReason,
                            OverDueLoanAmount = 0,
                            LateInterestCharge = c.LateInterestCharge,
                            NSFCharge = c.NSFCharge,
                            ShopStoreId = c.ShopStoreId,
                            Last63DaysLoanCount = c.Last63DaysLoanCount
                        });
            return a.ToList();
        }
        public List<CustomerLoan> CustomerReportGridDatabyStoreIdandType(int Id,string status)
        {

            var a = (from c in db.CustomerLoans
                     join t in db.CustomerMasters on c.CustomerId equals t.Id
                     where c.ShopStoreId == Id && c.LoanStatus.ToLower() == status
                     select new CustomerLoan
                     {
                         Id = c.Id,
                         DueAmount = c.DueAmount,
                         CustomerName = t.FirstName + " " + t.LastName,
                         LateInterestRate = c.LateInterestRate,
                         LoanAmountApplied = c.LoanAmountApplied,
                         LoanAmountApproved = c.LoanAmountApproved,
                         CreatedDate = c.CreatedDate,
                         CustomerId = c.CustomerId,
                         NextPayDate = c.NextPayDate,
                         PaymentOption = c.PaymentOption,
                         LoanStatus = c.LoanStatus,
                         UpdatedDate = c.UpdatedDate,
                         LoanDeniedReason = "",
                         OverdueCount = c.OverdueCount,
                         PartialPayment = "",
                         OverDueReason = c.OverDueReason,
                         OverDueLoanAmount = 0,
                         LateInterestCharge = c.LateInterestCharge,
                         NSFCharge = c.NSFCharge,
                         ShopStoreId = c.ShopStoreId,
                         Last63DaysLoanCount = c.Last63DaysLoanCount,
                         Installment2Amount=c.Installment2Amount
                     });
            return a.ToList();
        }
        public List<CustomerLoan> CustomerLoanTransactionReportdata(int StoreId, DateTime FromDate, DateTime ToDate)
        {

            var data = from c in db.CustomerLoans
                       join t in db.CustomerMasters on c.CustomerId equals t.Id
                       where c.ShopStoreId == StoreId
                       //&& (c.CreatedDate.Date >= FromDate.Date && c.CreatedDate.Date <= ToDate.Date)
                       select new CustomerLoan
                       {
                           Id = c.Id,
                           DueAmount = c.DueAmount,
                           CustomerName = t.FirstName + " " + t.LastName,
                           LateInterestRate = c.LateInterestRate,
                           LoanAmountApplied = c.LoanAmountApplied,
                           LoanAmountApproved = c.LoanAmountApproved,
                           CreatedDate = c.CreatedDate,
                           CustomerId = c.CustomerId,
                           NextPayDate = c.NextPayDate,
                           PaymentOption = c.PaymentOption,
                           AdminFee = c.AdminFee,
                           ShopStoreId = c.ShopStoreId,
                           ModeofPayment = c.ModeofPayment,
                           LoanStatus = c.LoanStatus,
                           UpdatedDate = c.UpdatedDate,
                           LastStatus = c.LastStatus,
                           NSFCharge = c.NSFCharge,
                           LateInterestCharge = c.LateInterestCharge,
                           DiscountAmount = c.DiscountAmount,
                           Installment1Date=c.Installment1Date,
                           Installment2Date=c.Installment2Date,
                           Installment1Amount=c.Installment1Amount,
                           Installment2Amount=c.Installment2Amount,
                           Installment3Amount=c.Installment3Amount
                       };

            return data.ToList();

        }
        public List<CustomerLoan> CustomerTermLoanTransactionReportdata(int StoreId, DateTime FromDate, DateTime ToDate)
        {

            var data = from c in db.CustomerTermLoans
                       join t in db.CustomerMasters on c.CustomerId equals t.Id
                       where c.ShopStoreId == StoreId
                       //&& (c.CreatedDate.Date >= FromDate.Date && c.CreatedDate.Date <= ToDate.Date)
                       select new CustomerLoan
                       {
                           Id = c.Id,
                           DueAmount = c.DueAmount,
                           CustomerName = t.FirstName + " " + t.LastName,
                           LateInterestRate = c.LateInterestRate,
                           LoanAmountApplied = c.LoanAmountApplied,
                           LoanAmountApproved = c.LoanAmountApproved,
                           CreatedDate = c.CreatedDate,
                           CustomerId = c.CustomerId,
                           NextPayDate = c.DueDate,
                           PaymentOption = c.PaymentOption,
                           AdminFee = c.AdminFee,
                           ShopStoreId = c.ShopStoreId,
                           ModeofPayment = c.ModeofPayment,
                           LoanStatus = c.LoanStatus,
                           UpdatedDate = c.UpdatedDate,
                           LastStatus = c.LastStatus,
                           NSFCharge = c.NSFCharge,
                           LateInterestCharge = c.LateInterestCharge,
                           DiscountAmount = c.DiscountAmount,
                           Installment1Amount=c.InstallmentAmount
                       };

            return data.ToList();

        }

        public List<CustomerLoan> CustomerLoanTransactionReportdata1(int StoreId, DateTime FromDate, DateTime ToDate, List<int> test)
        {

            var data = from c in db.CustomerLoans
                       join t in db.CustomerMasters on c.CustomerId equals t.Id
                       where c.ShopStoreId == StoreId
                       //&& (c.CreatedDate.Date >= FromDate.Date && c.CreatedDate.Date <= ToDate.Date)
                       select new CustomerLoan
                       {
                           Id = c.Id,
                           DueAmount = c.DueAmount,
                           CustomerName = t.FirstName + " " + t.LastName,
                           LateInterestRate = c.LateInterestRate,
                           LoanAmountApplied = c.LoanAmountApplied,
                           LoanAmountApproved = c.LoanAmountApproved,
                           CreatedDate = c.CreatedDate,
                           CustomerId = c.CustomerId,
                           NextPayDate = c.NextPayDate,
                           PaymentOption = c.PaymentOption,
                           AdminFee = c.AdminFee,
                           ShopStoreId = c.ShopStoreId,
                           ModeofPayment = c.ModeofPayment,
                           LoanStatus = c.LoanStatus,
                           UpdatedDate = c.UpdatedDate,
                           LateInterestCharge = c.LateInterestCharge,
                           NSFCharge = c.NSFCharge,
                           PartialAmount = 0,
                       };

            var result = (from r in data.AsEnumerable()
                          where test.Contains((int)r.Id)
                          select r).ToList();

            return result;

        }
        public List<CustomerLoan> CustomerLoanTransactionReportdata2(int StoreId, DateTime FromDate, DateTime ToDate)
        {

            var data = from p in db.LoanPartialPayments
                       join c in db.CustomerLoans on p.LoanId equals c.Id
                       join t in db.CustomerMasters on c.CustomerId equals t.Id
                       where c.ShopStoreId == StoreId
                       && c.LoanStatus != "Close" 
                       //&& (p.CreatedDate.Date >= FromDate.Date && p.CreatedDate.Date <= ToDate.Date)
                       select new CustomerLoan
                       {
                           Id = c.Id,
                           DueAmount = c.DueAmount,
                           CustomerName = t.FirstName + " " + t.LastName,
                           LateInterestRate = c.LateInterestRate,
                           LoanAmountApplied = c.LoanAmountApplied,
                           LoanAmountApproved = c.LoanAmountApproved,
                           CreatedDate = c.CreatedDate,
                           CustomerId = c.CustomerId,
                           NextPayDate = c.NextPayDate,
                           PaymentOption = c.PaymentOption,
                           AdminFee = c.AdminFee,
                           ShopStoreId = c.ShopStoreId,
                           ModeofPayment = c.ModeofPayment,
                           LoanStatus = c.LoanStatus,
                           UpdatedDate = c.UpdatedDate,
                           LateInterestCharge = c.LateInterestCharge,
                           NSFCharge = c.NSFCharge,
                           PartialAmount = p.PartialAmount,
                           PartialLoanCreatedDate = p.CreatedDate,
                           DiscountAmount = c.DiscountAmount,
                           PartialPaymentMethod = p.PartialPaymentMethod
                       };


            return data.ToList().Where(p => p.PartialLoanCreatedDate.Date >= FromDate.Date && p.PartialLoanCreatedDate.Date <= ToDate.Date).ToList();

        }
        public List<CustomerLoan> CustomerTermLoanTransactionReportdata2(int StoreId, DateTime FromDate, DateTime ToDate)
        {

            var data = from p in db.TermLoanPartialPayments
                       join c in db.CustomerTermLoans on p.LoanId equals c.Id
                       join t in db.CustomerMasters on c.CustomerId equals t.Id
                       where c.ShopStoreId == StoreId
                       //&& (p.CreatedDate.Date >= FromDate.Date && p.CreatedDate.Date <= ToDate.Date)
                       select new CustomerLoan
                       {
                           Id = c.Id,
                           DueAmount = c.DueAmount,
                           CustomerName = t.FirstName + " " + t.LastName,
                           LateInterestRate = c.LateInterestRate,
                           LoanAmountApplied = c.LoanAmountApplied,
                           LoanAmountApproved = c.LoanAmountApproved,
                           CreatedDate = c.CreatedDate,
                           CustomerId = c.CustomerId,
                           NextPayDate = c.DueDate,
                           PaymentOption = c.PaymentOption,
                           AdminFee = c.AdminFee,
                           ShopStoreId = c.ShopStoreId,
                           ModeofPayment = c.ModeofPayment,
                           LoanStatus = c.LoanStatus,
                           UpdatedDate = c.UpdatedDate,
                           LateInterestCharge = c.LateInterestCharge,
                           NSFCharge = c.NSFCharge,
                           PartialAmount = p.PartialAmount,
                           PartialLoanCreatedDate = p.CreatedDate,
                           DiscountAmount = p.DiscountAmount,
                           PartialPaymentMethod = p.PartialPaymentMethod
                       };


            return data.ToList().Where(p => p.PartialLoanCreatedDate.Date >= FromDate.Date && p.PartialLoanCreatedDate.Date <= ToDate.Date).ToList();

        }
        public DataSet CustomerTermLoanTransactionReportdata2_New(int StoreId, DateTime FromDate, DateTime ToDate)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(ConnectionString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "proc_GetPartialTermLoanReport";
            cmd.Parameters.Add(new SqlParameter("@StoreId", StoreId));
            cmd.Parameters.Add(new SqlParameter("@FromDate", FromDate.Date));
            cmd.Parameters.Add(new SqlParameter("@ToDate", ToDate.Date));
            cmd.Connection.Open();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet("Data");
            adp.Fill(ds);
            cmd.Connection.Close();
            return ds;

        }
        public DataSet GetTermLoanSchedule(long LoanId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(ConnectionString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "proc_GetPaymentScheduleByLoanId";
            cmd.Parameters.Add(new SqlParameter("@Id", LoanId));
            cmd.Connection.Open();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet("Data");
            adp.Fill(ds);
            cmd.Connection.Close();
            return ds;

        }
        public DateTime GetTermLoanScheduleNextINstallmentDay(long LoanId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(ConnectionString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "proc_GetDisplayTermLoanInstallmentDate";
            cmd.Parameters.Add(new SqlParameter("@TermLoanId", LoanId));
            cmd.Connection.Open();
            //SqlDataAdapter adp = new SqlDataAdapter(cmd);
            //DataSet ds = new DataSet("Data");
            //adp.Fill(ds);
            var date= cmd.ExecuteScalar();
            cmd.Connection.Close();
            return Convert.ToDateTime(date);

        }

        public IQueryable<CustomerLoan> CustomerLoanCashHistoryData
        {

            get
            {
                return from c in db.CustomerLoans
                       join t in db.CustomerMasters on c.CustomerId equals t.Id
                       //where c.ShopStoreId == StoreId && (c.CreatedDate >= FromDate && c.CreatedDate <= ToDate)
                       select new CustomerLoan
                       {
                           Id = c.Id,
                           DueAmount = c.DueAmount,
                           CustomerName = t.FirstName + " " + t.LastName,
                           LateInterestRate = c.LateInterestRate,
                           LoanAmountApplied = c.LoanAmountApplied,
                           LoanAmountApproved = c.LoanAmountApproved,
                           CreatedDate = c.CreatedDate,
                           CustomerId = c.CustomerId,
                           NextPayDate = c.NextPayDate,
                           PaymentOption = c.PaymentOption,
                           AdminFee = c.AdminFee,
                           ShopStoreId = c.ShopStoreId,
                           LoanType = c.LoanType,
                           LoanStatus = c.LoanStatus,
                           UpdatedDate = c.UpdatedDate,
                           OverdueCount = c.OverdueCount,
                           ModeofPayment = c.ModeofPayment,
                       };
            }



        }
        public List<CustomerLoan> CustomerLoanCashHistoryDataByCustomerId(int Id)
        {
            var cl = from c in db.CustomerLoans
                     join t in db.CustomerMasters on c.CustomerId equals t.Id
                     where t.Id == Id
                     //where c.ShopStoreId == StoreId && (c.CreatedDate >= FromDate && c.CreatedDate <= ToDate)
                     select new CustomerLoan
                     {
                         Id = c.Id,
                         DueAmount = c.DueAmount,
                         CustomerName = t.FirstName + " " + t.LastName,
                         LateInterestRate = c.LateInterestRate,
                         LoanAmountApplied = c.LoanAmountApplied,
                         LoanAmountApproved = c.LoanAmountApproved,
                         CreatedDate = c.CreatedDate,
                         CustomerId = c.CustomerId,
                         NextPayDate = c.NextPayDate,
                         PaymentOption = c.PaymentOption,
                         AdminFee = c.AdminFee,
                         ShopStoreId = c.ShopStoreId,
                         LoanType = c.LoanType,
                         LoanStatus = c.LoanStatus,
                         UpdatedDate = c.UpdatedDate,
                         OverdueCount = c.OverdueCount,
                         ModeofPayment = c.ModeofPayment,
                     };

            return cl.ToList();
        }
        public void CustomerLoan_InsertOrUpdate(CustomerLoan c)
        {
            if (c.Id == 0)
            {
                var i = new EF.CustomerLoan
                {
                    CustomerId = c.CustomerId,
                    ShopStoreId = c.ShopStoreId,
                    LoanAmountApplied = c.LoanAmountApplied,
                    NextPayDate = c.NextPayDate,
                    LateInterestRate = c.LateInterestRate,
                    IsLoanApproved = c.IsLoanApproved,
                    LoanAmountApproved = c.LoanAmountApproved,
                    PaymentOption = c.PaymentOption,
                    DueAmount = c.DueAmount,
                    CreatedBy = c.CreatedBy,
                    CreatedDate = c.CreatedDate,
                    AdminFee = c.AdminFee,
                    LoanStatus = c.LoanStatus,
                    StatusUpdatedby = c.CreatedBy,
                    LoanType = c.LoanType,
                    UpdatedDate = c.CreatedDate,
                    RemainingAmount = c.RemainingAmount,
                    OverdueCount = c.OverdueCount,
                    DiscountAmount = c.DiscountAmount,
                    NSFCharge = c.NSFCharge == null ? 0 : c.NSFCharge,
                    LateInterestCharge = c.LateInterestCharge == null ? 0 : c.LateInterestCharge,
                    Settlement = c.Settlement,
                    LastDueAmount = c.LastDueAmount == null ? c.DueAmount : c.LastDueAmount,
                    LastStatus = c.LastStatus,
                    MonthlyIncome = c.MonthlyIncome,
                    Installment1Date = c.Installment1Date,
                    Installment2Date = c.Installment2Date,
                    Last63DaysLoanCount = c.Last63DaysLoanCount,
                    Installment1Amount = c.Installment1Amount,
                    Installment2Amount = c.Installment2Amount,
                    Installment3Amount = c.Installment3Amount,
                    LoanPaymentType = c.LoanPaymentType
                };

                db.CustomerLoans.AddObject(i);
                db.SaveChanges();
                c.Id = i.Id;
            }


            else
            {
                var u = db.CustomerLoans.Where(p => p.Id == c.Id).Single();
                u.CustomerId = c.CustomerId;
                u.ShopStoreId = c.ShopStoreId;
                u.LoanAmountApplied = c.LoanAmountApplied;
                u.NextPayDate = c.NextPayDate;
                u.LateInterestRate = c.LateInterestRate;
                u.IsLoanApproved = c.IsLoanApproved;
                u.LoanAmountApproved = c.LoanAmountApproved;
                u.PaymentOption = c.PaymentOption;
                u.DueAmount = c.DueAmount;
                u.CreatedBy = c.CreatedBy;
                u.CreatedDate = c.CreatedDate;
                u.AdminFee = c.AdminFee;
                u.LoanStatus = c.LoanStatus;
                u.StatusUpdatedby = c.StatusUpdatedby;
                u.LoanType = c.LoanType;
                u.UpdatedDate = c.UpdatedDate;
                u.ModeofPayment = c.ModeofPayment;
                u.OverDueReason = c.OverDueReason;
                u.RemainingAmount = c.RemainingAmount;
                u.DiscountAmount = c.DiscountAmount;
                u.OverdueCount = c.OverdueCount;
                u.NSFCharge = c.NSFCharge == null ? 0 : c.NSFCharge;
                u.LateInterestCharge = c.LateInterestCharge == null ? 0 : c.LateInterestCharge;
                u.Settlement = c.Settlement;
                u.LastDueAmount = c.LastDueAmount == null ? c.DueAmount : c.LastDueAmount;
                u.LastStatus = c.LastStatus;
                //u.LoanDeniedReason = c.LoanDeniedReason;
                db.SaveChanges();
            }
        }
        public void CustomerLoanReason_InsertOrUpdate(LoanDenyReason c)
        {
            if (c.Id == 0)
            {
                var i = new EF.LoanDenyReason
                {
                    LoanId = c.LoanId,
                    DenyReason = c.DenyReason
                };
                db.LoanDenyReasons.AddObject(i);
                db.SaveChanges();
                c.Id = i.Id;
            }
            else
            {
                var u = db.LoanDenyReasons.Where(p => p.Id == c.Id).Single();
                u.LoanId = c.LoanId;
                u.DenyReason = c.DenyReason;
                db.SaveChanges();
            }
        }
        #region PartialPayment
        public IQueryable<LoanPartialPayment> LoanPartialPayments
        {
            get
            {
                return from l in db.LoanPartialPayments
                       select new LoanPartialPayment
                       {
                           Id = l.Id,
                           LoanId = l.LoanId,
                           PartialAmount = Convert.ToDecimal(l.PartialAmount),
                           CreatedDate = l.CreatedDate,
                           Createdby = l.Createdby,
                           IntrestCharge = l.IntrestCharge,
                           DueAmount = l.DueAmount,
                           PartialPaymentMethod = l.PartialPaymentMethod
                       };
            }
        }
        public IQueryable<LoanPartialPayment> LoanPartialPaymentsnew
        {
            get
            {
                return from l in db.LoanPartialPayments
                       select new LoanPartialPayment
                       {
                           Id = l.Id,
                           LoanId = l.LoanId,
                           PartialAmount = l.PartialAmount,
                           CreatedDate = l.CreatedDate,
                           Createdby = l.Createdby,
                           IntrestCharge = l.IntrestCharge,
                           DueAmount = l.DueAmount,
                           PartialPaymentMethod = l.PartialPaymentMethod
                       };
            }
        }
        public List<LoanPartialPayment> LoanPartialPaymentsnewbyLoanId(long Id)
        {

            var a = from l in db.LoanPartialPayments
                    where l.LoanId == Id
                    select new LoanPartialPayment
                    {
                        Id = l.Id,
                        LoanId = l.LoanId,
                        PartialAmount = l.PartialAmount,
                        CreatedDate = l.CreatedDate,
                        Createdby = l.Createdby,
                        IntrestCharge = l.IntrestCharge,
                        DueAmount = l.DueAmount,
                        PartialPaymentMethod = l.PartialPaymentMethod
                    };
            return a.ToList();
        }
        public List<LoanPartialPayment> LoanPartialPaymentslist()
        {

            var lst = from l in db.LoanPartialPayments
                      select new LoanPartialPayment
                      {
                          Id = l.Id,
                          LoanId = l.LoanId,
                          PartialAmount = l.PartialAmount,
                          CreatedDate = l.CreatedDate,
                          Createdby = l.Createdby,
                          IntrestCharge = l.IntrestCharge,
                          DueAmount = l.DueAmount,
                          PartialPaymentMethod = l.PartialPaymentMethod
                      };
            return lst.ToList();

        }
        public void LoanPartialPayment_InsertOrUpdate(LoanPartialPayment l)
        {
            if (l.Id == 0)
            {
                var i = new EF.LoanPartialPayment
                {
                    LoanId = l.LoanId,
                    PartialAmount = l.PartialAmount,
                    CreatedDate = l.CreatedDate,
                    Createdby = l.Createdby,
                    IntrestCharge = l.IntrestCharge,
                    DueAmount = l.DueAmount,
                    PartialPaymentMethod = l.PartialPaymentMethod
                };

                db.LoanPartialPayments.AddObject(i);
                db.SaveChanges();
                l.Id = i.Id;
            }


            else
            {
                var u = db.LoanPartialPayments.Where(p => p.Id == l.Id).Single();
                u.LoanId = l.LoanId;
                u.PartialAmount = l.PartialAmount;
                u.CreatedDate = l.CreatedDate;
                u.Createdby = l.Createdby;
                u.IntrestCharge = l.IntrestCharge;
                u.DueAmount = l.DueAmount;
                u.PartialPaymentMethod = l.PartialPaymentMethod;
                db.SaveChanges();
            }
        }
        #endregion

        #region LoanEXception

        public IQueryable<CustomerLoanException> CustomerLoanExceptions
        {
            get
            {
                return from c in db.CustomerLoanExceptions
                       select new CustomerLoanException
                       {
                           Id = c.Id,
                           CustomerId = c.CustomerId,
                           LoanId = c.LoanId,
                       };
            }
        }

        public void CustomerLoanException_InsertOrUpdate(CustomerLoanException c)
        {
            if (c.Id == 0)
            {
                var i = new EF.CustomerLoanException
                {
                    CustomerId = c.CustomerId,
                    LoanId = c.LoanId,
                };

                db.CustomerLoanExceptions.AddObject(i);
                db.SaveChanges();
                c.Id = i.Id;
            }


            else
            {
                var u = db.CustomerLoanExceptions.Where(p => p.Id == c.Id).Single();
                u.CustomerId = c.CustomerId;
                u.LoanId = c.LoanId;

                db.SaveChanges();
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
