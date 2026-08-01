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
    public class TermLoanService : ConnectionHelper, IDisposable
    {
        EF.CashLoanShopEntities db = null;
        public TermLoanService()
        {
            db = new EF.CashLoanShopEntities(EntityConnectionString);
        }
        public TermLoanService(ObjectContext context)
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
        public DataSet GetPaymentSchedule(DateTime FirstDate, DateTime SecondDate, DateTime DueDate, string PaymentType, decimal Amount)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(ConnectionString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "proc_GetPaymentSchedule";
            cmd.Parameters.Add(new SqlParameter("@startDate", FirstDate));
            cmd.Parameters.Add(new SqlParameter("@Secondintallmentdate", SecondDate));
            cmd.Parameters.Add(new SqlParameter("@endDate", DueDate));
            cmd.Parameters.Add(new SqlParameter("@PaymentType", PaymentType));
            cmd.Parameters.Add(new SqlParameter("@LoanAmount", Amount));
            cmd.Connection.Open();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet("Data");
            adp.Fill(ds);
            return ds;
        }
        public void InsertPaymentScheduleByLoanId(long LoanId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(ConnectionString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "InsertTermLoanPaymentSchedule";
            cmd.Parameters.Add(new SqlParameter("@LoanId", LoanId));
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }



        public IQueryable<CustomerTermLoan> CustomerTermLoans
        {
            get
            {
                return from c in db.CustomerTermLoans
                       select new CustomerTermLoan
                       {
                           Id = c.Id,
                           CustomerId = c.CustomerId,
                           ShopStoreId = c.ShopStoreId,
                           LoanAmountApplied = c.LoanAmountApplied,
                           DueDate = c.DueDate,
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
                           UpdatedDate = c.UpdatedDate,
                           FirstInstallmentDate = c.FirstInstallmentDate,
                           SecondInstallmentDate = c.SecondInstallmentDate,
                           LoanTerm = c.LoanTerm,
                           ReportText = c.ReportText,
                           InstallmentAmount = c.InstallmentAmount,
                           InterestCharge = c.InterestCharge,
                           LateInterestCharge = c.LateInterestCharge,
                           NSFCharge = c.NSFCharge,
                           NoofInstallment = c.NoofInstallment,
                           LoanOverDueDate = c.LoanOverDueDate,
                           LastStatus = c.LastStatus
                       };
            }
        }
        public List<CustomerTermLoan> CustomerTermLoansbyId(int Id)
        {

            var a = from c in db.CustomerTermLoans
                    where c.Id == Id
                    select new CustomerTermLoan
                    {
                        Id = c.Id,
                        CustomerId = c.CustomerId,
                        ShopStoreId = c.ShopStoreId,
                        LoanAmountApplied = c.LoanAmountApplied,
                        DueDate = c.DueDate,
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
                        UpdatedDate = c.UpdatedDate,
                        FirstInstallmentDate = c.FirstInstallmentDate,
                        SecondInstallmentDate = c.SecondInstallmentDate,
                        LoanTerm = c.LoanTerm,
                        ReportText = c.ReportText,
                        InstallmentAmount = c.InstallmentAmount,
                        InterestCharge = c.InterestCharge,
                        LateInterestCharge = c.LateInterestCharge,
                        NSFCharge = c.NSFCharge,
                        DiscountAmount = c.DiscountAmount,
                        NoofInstallment = c.NoofInstallment,
                        LoanOverDueDate = c.LoanOverDueDate,
                        LastStatus = c.LastStatus
                    };
            return a.ToList();
        }
        public List<CustomerTermLoan> CustomerTermLoansbyCustomerId(int Id)
        {

            var a = from c in db.CustomerTermLoans
                    where c.CustomerId == Id
                    select new CustomerTermLoan
                    {
                        Id = c.Id,
                        CustomerId = c.CustomerId,
                        ShopStoreId = c.ShopStoreId,
                        LoanAmountApplied = c.LoanAmountApplied,
                        DueDate = c.DueDate,
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
                        UpdatedDate = c.UpdatedDate,
                        FirstInstallmentDate = c.FirstInstallmentDate,
                        SecondInstallmentDate = c.SecondInstallmentDate,
                        LoanTerm = c.LoanTerm,
                        ReportText = c.ReportText,
                        InstallmentAmount = c.InstallmentAmount,
                        InterestCharge = c.InterestCharge,
                        LateInterestCharge = c.LateInterestCharge,
                        NSFCharge = c.NSFCharge,
                        NoofInstallment = c.NoofInstallment,
                        LoanOverDueDate = c.LoanOverDueDate,
                        LastStatus = c.LastStatus
                    };
            return a.ToList();
        }
        public void CustomerTermLoan_InsertOrUpdate(CustomerTermLoan c)
        {
            if (c.Id == 0)
            {
                var i = new EF.CustomerTermLoan
                {
                    CustomerId = c.CustomerId,
                    ShopStoreId = c.ShopStoreId,
                    LoanAmountApplied = c.LoanAmountApplied,
                    DueDate = c.DueDate,
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
                    FirstInstallmentDate = c.FirstInstallmentDate,
                    SecondInstallmentDate = c.SecondInstallmentDate,
                    LoanTerm = c.LoanTerm,
                    ReportText = c.ReportText,
                    InterestCharge = c.InterestCharge,
                    InstallmentAmount = c.InstallmentAmount,
                    LateInterestCharge = c.LateInterestCharge,
                    NSFCharge = c.NSFCharge,
                    NoofInstallment = c.NoofInstallment,
                    LoanOverDueDate = c.LoanOverDueDate,
                    LastStatus = c.LastStatus
                };
                ResetTermLoanIdentity();
                db.CustomerTermLoans.AddObject(i);
                db.SaveChanges();
                c.Id = i.Id;
            }
            else
            {
                var u = db.CustomerTermLoans.Where(p => p.Id == c.Id).Single();
                u.CustomerId = c.CustomerId;
                u.ShopStoreId = c.ShopStoreId;
                u.LoanAmountApplied = c.LoanAmountApplied;
                u.DueDate = c.DueDate;
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
                u.FirstInstallmentDate = c.FirstInstallmentDate;
                u.SecondInstallmentDate = c.SecondInstallmentDate;
                u.LoanTerm = c.LoanTerm;
                u.ModeofPayment = c.ModeofPayment;
                u.RemainingAmount = c.RemainingAmount;
                u.DiscountAmount = c.DiscountAmount;
                u.InterestCharge = c.InterestCharge;
                u.InstallmentAmount = c.InstallmentAmount;
                u.LateInterestCharge = c.LateInterestCharge;
                u.NSFCharge = c.NSFCharge;
                u.LoanOverDueDate = c.LoanOverDueDate;
                u.LastStatus = c.LastStatus;
                db.SaveChanges();
            }
        }
        public List<CustomerTermLoan> CustomerReportGridDatabyStoreIdandType(int Id, string status)
        {

            var a = (from c in db.CustomerTermLoans
                     join t in db.CustomerMasters on c.CustomerId equals t.Id
                     where c.ShopStoreId == Id && c.LoanStatus.ToLower() == status
                     select new CustomerTermLoan
                     {
                         Id = c.Id,
                         DueAmount = c.DueAmount,
                         CustomerName = t.FirstName + " " + t.LastName,
                         LateInterestRate = c.LateInterestRate,
                         LoanAmountApplied = c.LoanAmountApplied,
                         LoanAmountApproved = c.LoanAmountApproved,
                         CreatedDate = c.CreatedDate,
                         CustomerId = c.CustomerId,
                         DueDate = c.DueDate,
                         PaymentOption = c.PaymentOption,
                         LoanStatus = c.LoanStatus,
                         UpdatedDate = c.UpdatedDate,
                         LoanDeniedReason = "",
                         LoanType = c.LoanType,
                         InterestCharge = c.InterestCharge,
                         AdminFee = c.AdminFee,
                         InstallmentAmount = c.InstallmentAmount,
                         NSFCharge = c.NSFCharge,
                         LateInterestCharge = c.LateInterestCharge,
                         //OverdueCount = c.OverdueCount,
                         PartialPayment = "",
                         //OverDueReason = c.OverDueReason,
                         //OverDueLoanAmount = 0,
                         //LateInterestCharge = c.LateInterestCharge,
                         //NSFCharge = c.NSFCharge,
                         ShopStoreId = c.ShopStoreId,
                         //Last63DaysLoanCount = c.Last63DaysLoanCount
                     });
            return a.ToList();
        }
        public List<CustomerTermLoan> CustomerReportGridDatabyStoreId(int Id)
        {
            var a = (from c in db.CustomerTermLoans
                     join t in db.CustomerMasters on c.CustomerId equals t.Id
                     where c.ShopStoreId == Id
                     select new CustomerTermLoan
                     {
                         Id = c.Id,
                         DueAmount = c.DueAmount,
                         CustomerName = t.FirstName + " " + t.LastName,
                         LateInterestRate = c.LateInterestRate,
                         LoanAmountApplied = c.LoanAmountApplied,
                         LoanAmountApproved = c.LoanAmountApproved,
                         CreatedDate = c.CreatedDate,
                         CustomerId = c.CustomerId,
                         DueDate = c.DueDate,
                         PaymentOption = c.PaymentOption,
                         LoanStatus = c.LoanStatus,
                         UpdatedDate = c.UpdatedDate,
                         LoanDeniedReason = "",
                         LoanType = c.LoanType,
                         InterestCharge = c.InterestCharge,
                         AdminFee = c.AdminFee,
                         InstallmentAmount = c.InstallmentAmount,
                         NSFCharge = c.NSFCharge,
                         LateInterestCharge = c.LateInterestCharge,
                         //OverdueCount = c.OverdueCount,
                         PartialPayment = "",
                         //OverDueReason = c.OverDueReason,
                         //OverDueLoanAmount = 0,
                         //LateInterestCharge = c.LateInterestCharge,
                         //NSFCharge = c.NSFCharge,
                         ShopStoreId = c.ShopStoreId,
                         //Last63DaysLoanCount = c.Last63DaysLoanCount
                     });
            return a.ToList();
        }
        public List<CustomerTermLoan> CustomerLoanCashHistoryDataByCustomerId(int Id)
        {
            var cl = from c in db.CustomerTermLoans
                     join t in db.CustomerMasters on c.CustomerId equals t.Id
                     where t.Id == Id
                     //where c.ShopStoreId == StoreId && (c.CreatedDate >= FromDate && c.CreatedDate <= ToDate)
                     select new CustomerTermLoan
                     {
                         Id = c.Id,
                         DueAmount = c.DueAmount,
                         CustomerName = t.FirstName + " " + t.LastName,
                         LateInterestRate = c.LateInterestRate,
                         LoanAmountApplied = c.LoanAmountApplied,
                         LoanAmountApproved = c.LoanAmountApproved,
                         CreatedDate = c.CreatedDate,
                         CustomerId = c.CustomerId,
                         DueDate = c.DueDate,
                         PaymentOption = c.PaymentOption,
                         AdminFee = c.AdminFee,
                         ShopStoreId = c.ShopStoreId,
                         LoanType = c.LoanType,
                         LoanStatus = c.LoanStatus,
                         UpdatedDate = c.UpdatedDate,
                         ModeofPayment = c.PaymentOption,
                         LoanTerm = c.LoanTerm,
                         DiscountAmount=c.DiscountAmount
                     };

            return cl.ToList();
        }

        public DataSet CustomerReportGridOverLAbel(DateTime? FromDate, DateTime? ToDate, int StoreId, int LoanId, string Status)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(ConnectionString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "proc_GetOverDueReport";
            cmd.Parameters.Add(new SqlParameter("@ShopStoreId", StoreId));
            cmd.Parameters.Add(new SqlParameter("@Loanstatus", Status));
            cmd.Parameters.Add(new SqlParameter("@LoanId", LoanId));
            cmd.Connection.Open();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet("Data");
            adp.Fill(ds);
            return ds;
        }
        public void ResetTermLoanIdentity()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(ConnectionString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "proc_UpdateIdentityTermLoan";
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        public void LoadEFTPayment(DataTable dt)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(ConnectionString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "proc_LoadEFTPayment";
            cmd.Parameters.Add(new SqlParameter("@EFTLoanPayment", dt));
            // tvpParam.SqlDbType = SqlDbType.Structured;
            // tvpParam.TypeName = "dbo.EFTLoanPayment";
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        public void LoadEFTPayment_PaydayLoan(DataTable dt)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(ConnectionString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "proc_LoadEFTPayment_CustomerLoan";
            cmd.Parameters.Add(new SqlParameter("@EFTLoanPayment", dt));
            // tvpParam.SqlDbType = SqlDbType.Structured;
            // tvpParam.TypeName = "dbo.EFTLoanPayment";
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        public DataTable GetInterestEarnedReport(DateTime fromdate, DateTime todate, int StoreId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(ConnectionString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "proc_GetTermLoanInterestReport";
            cmd.Parameters.Add(new SqlParameter("@StoreId", StoreId));
            cmd.Parameters.Add(new SqlParameter("@FromDate", fromdate));
            cmd.Parameters.Add(new SqlParameter("@Todate", todate));
            cmd.Connection.Open();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            return dt;
        }
        public DataTable GetDiscountReport(DateTime fromdate, DateTime todate, string StoreId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(ConnectionString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "proc_GetAdminDiscountReport";
            cmd.Parameters.Add(new SqlParameter("@StoreId", StoreId));
            cmd.Parameters.Add(new SqlParameter("@FromDate", fromdate));
            cmd.Parameters.Add(new SqlParameter("@Todate", todate));
            cmd.Connection.Open();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            return dt;
        }

        #region TermLoanPartialPayment
        public IQueryable<TermLoanPartialPayment> TermLoanPartialPayments
        {
            get
            {
                return from l in db.TermLoanPartialPayments
                       select new TermLoanPartialPayment
                       {
                           Id = l.Id,
                           LoanId = l.LoanId,
                           PartialAmount = Convert.ToDecimal(l.PartialAmount),
                           CreatedDate = l.CreatedDate,
                           Createdby = l.Createdby,
                           IntrestCharge = l.IntrestCharge,
                           DueAmount = l.DueAmount,
                           PartialPaymentMethod = l.PartialPaymentMethod,
                           InstallmentInterestAmount = l.InstallmentInterestAmount,
                           InstallmentPrincipalAmount = l.InstallmentPrincipalAmount,
                           DiscountAmount = l.DiscountAmount,
                           Balance = l.Balance
                       };
            }
        }
        public IQueryable<TermLoanPartialPayment> TermLoanPartialPaymentsnew
        {
            get
            {
                return from l in db.TermLoanPartialPayments
                       select new TermLoanPartialPayment
                       {
                           Id = l.Id,
                           LoanId = l.LoanId,
                           PartialAmount = l.PartialAmount,
                           CreatedDate = l.CreatedDate,
                           Createdby = l.Createdby,
                           IntrestCharge = l.IntrestCharge,
                           DueAmount = l.DueAmount,
                           PartialPaymentMethod = l.PartialPaymentMethod,
                           InstallmentInterestAmount = l.InstallmentInterestAmount,
                           InstallmentPrincipalAmount = l.InstallmentPrincipalAmount,
                           DiscountAmount = l.DiscountAmount,
                           Balance = l.Balance
                       };
            }
        }
        public List<TermLoanPartialPayment> TermLoanPartialPaymentsnewbyLoanId(long Id)
        {

            var a = from l in db.TermLoanPartialPayments
                    where l.LoanId == Id
                    select new TermLoanPartialPayment
                    {
                        Id = l.Id,
                        LoanId = l.LoanId,
                        PartialAmount = l.PartialAmount,
                        CreatedDate = l.CreatedDate,
                        Createdby = l.Createdby,
                        IntrestCharge = l.IntrestCharge,
                        DueAmount = l.DueAmount,
                        PartialPaymentMethod = l.PartialPaymentMethod,
                        InstallmentInterestAmount = l.InstallmentInterestAmount,
                        InstallmentPrincipalAmount = l.InstallmentPrincipalAmount,
                        DiscountAmount = l.DiscountAmount,
                        Balance = l.Balance
                    };
            return a.ToList();
        }
        public List<TermLoanPartialPayment> TermLoanPartialPaymentslist()
        {

            var lst = from l in db.TermLoanPartialPayments
                      select new TermLoanPartialPayment
                      {
                          Id = l.Id,
                          LoanId = l.LoanId,
                          PartialAmount = l.PartialAmount,
                          CreatedDate = l.CreatedDate,
                          Createdby = l.Createdby,
                          IntrestCharge = l.IntrestCharge,
                          DueAmount = l.DueAmount,
                          PartialPaymentMethod = l.PartialPaymentMethod,
                          InstallmentInterestAmount = l.InstallmentInterestAmount,
                          InstallmentPrincipalAmount = l.InstallmentPrincipalAmount,
                          DiscountAmount = l.DiscountAmount,
                          Balance = l.Balance
                      };
            return lst.ToList();

        }
        public void TermLoanPartialPayment_InsertOrUpdate(TermLoanPartialPayment l)
        {
            if (l.Id == 0)
            {
                var i = new EF.TermLoanPartialPayment
                {
                    LoanId = l.LoanId,
                    PartialAmount = l.PartialAmount,
                    CreatedDate = l.CreatedDate,
                    Createdby = l.Createdby,
                    IntrestCharge = l.IntrestCharge,
                    DueAmount = l.DueAmount,
                    PartialPaymentMethod = l.PartialPaymentMethod,
                    InstallmentPrincipalAmount = l.InstallmentPrincipalAmount,
                    InstallmentInterestAmount = l.InstallmentInterestAmount,
                    DiscountAmount = l.DiscountAmount,
                    Balance = l.Balance
                };
                //var date = l.CreatedDate.AddMinutes(-5);
                //if (db.TermLoanPartialPayments.Where(p => p.LoanId == l.LoanId && p.PartialAmount == l.PartialAmount && p.CreatedDate >= date).OrderByDescending(c => c.CreatedDate).FirstOrDefault() == null)
                //{
                db.TermLoanPartialPayments.AddObject(i);
                db.SaveChanges();
                l.Id = i.Id;
                //}
            }


            else
            {
                var u = db.TermLoanPartialPayments.Where(p => p.Id == l.Id).Single();
                u.LoanId = l.LoanId;
                u.PartialAmount = l.PartialAmount;
                u.CreatedDate = l.CreatedDate;
                u.Createdby = l.Createdby;
                u.IntrestCharge = l.IntrestCharge;
                u.DueAmount = l.DueAmount;
                u.PartialPaymentMethod = l.PartialPaymentMethod;
                u.DiscountAmount = l.DiscountAmount;
                u.Balance = l.Balance;
                db.SaveChanges();
            }
        }
        #endregion

        #region TermLoanSchedule

        public IQueryable<TermLoanSchedule> TermLoanSchedules
        {
            get
            {
                return from t in db.TermLoanSchedules
                       select new TermLoanSchedule
                       {
                           Id = t.Id,
                           LoanId = t.LoanId,
                           Date = t.Date,
                           Amount = t.Amount,
                           InstallmentNo = t.InstallmentNo,
                           Principal = t.Principal,
                           Interest = t.Interest,
                           Balance = t.Balance,
                           IsPaid = t.IsPaid,
                       };
            }
        }

        public void TermLoanSchedule_InsertOrUpdate(TermLoanSchedule t)
        {
            if (t.Id == 0)
            {
                var i = new EF.TermLoanSchedule
                {
                    LoanId = t.LoanId,
                    Date = t.Date,
                    Amount = t.Amount,
                    InstallmentNo = t.InstallmentNo,
                    Principal = t.Principal,
                    Interest = t.Interest,
                    Balance = t.Balance,
                    IsPaid = t.IsPaid,
                };

                db.TermLoanSchedules.AddObject(i);
                db.SaveChanges();
                t.Id = i.Id;
            }


            else
            {
                var u = db.TermLoanSchedules.Where(p => p.Id == t.Id).Single();
                u.LoanId = t.LoanId;
                u.Date = t.Date;
                u.Amount = t.Amount;
                u.InstallmentNo = t.InstallmentNo;
                u.Principal = t.Principal;
                u.Interest = t.Interest;
                u.Balance = t.Balance;
                u.IsPaid = t.IsPaid;

                db.SaveChanges();
            }
        }
        public void UpdateTermLoanSChedule(long LoanId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(ConnectionString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pro_UpdateAllSchedule";
            cmd.Parameters.Add(new SqlParameter("@LoanId",Convert.ToInt32(LoanId)));
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        #endregion
        public void Dispose()
        {
            System.GC.SuppressFinalize(this);
        }
    }
}
