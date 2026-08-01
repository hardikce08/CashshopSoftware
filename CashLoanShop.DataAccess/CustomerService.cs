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
    public class CustomerService : ConnectionHelper,IDisposable
    {
        EF.CashLoanShopEntities db = null;
        public CustomerService()
        {
            db = new EF.CashLoanShopEntities(EntityConnectionString);
        }
        public CustomerService(ObjectContext context)
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
        #region CustomerMaster
        public IQueryable<CustomerMaster> CustomerMasters
        {
            get
            {
                return from c in db.CustomerMasters
                       where c.FirstName.Contains("test")
                       select new CustomerMaster
                       {
                           Id = c.Id,
                           FirstName = c.FirstName,
                           LastName = c.LastName,
                           Mi = c.Mi,
                           Dateofbirth = c.Dateofbirth,
                           Gender = c.Gender,
                           Address = c.Address,
                           City = c.City,
                           Province = c.Province,
                           PostCode = c.PostCode,
                           DurationYears = c.DurationYears,
                           DurationMonth = c.DurationMonth,
                           HomeType = c.HomeType,
                           HomePhone = c.HomePhone,
                           WorkPhone = c.WorkPhone,
                           CellPhone = c.CellPhone,
                           PhoneListedunder = c.PhoneListedunder,
                           SocialSecurityNumber = c.SocialSecurityNumber,
                           DrivingLicenseNumber = c.DrivingLicenseNumber,
                           IssuedAt = c.IssuedAt,
                           Comments = c.Comments,
                           Createdat = c.Createdat,
                           CreatedBy = c.CreatedBy,
                           ImageName = c.ImageName,
                       };
            }
        }
        public List<CustomerMaster> CustomerMastersById(int Id)
        {
            var a= from c in db.CustomerMasters
                   where c.Id == Id
                   select new CustomerMaster
                   {
                       Id = c.Id,
                       FirstName = c.FirstName,
                       LastName = c.LastName,
                       Mi = c.Mi,
                       Dateofbirth = c.Dateofbirth,
                       Gender = c.Gender,
                       Address = c.Address,
                       City = c.City,
                       Province = c.Province,
                       PostCode = c.PostCode,
                       DurationYears = c.DurationYears,
                       DurationMonth = c.DurationMonth,
                       HomeType = c.HomeType,
                       HomePhone = c.HomePhone,
                       WorkPhone = c.WorkPhone,
                       CellPhone = c.CellPhone,
                       PhoneListedunder = c.PhoneListedunder,
                       SocialSecurityNumber = c.SocialSecurityNumber,
                       DrivingLicenseNumber = c.DrivingLicenseNumber,
                       IssuedAt = c.IssuedAt,
                       Comments = c.Comments,
                       Createdat = c.Createdat,
                       CreatedBy = c.CreatedBy,
                       ImageName = c.ImageName,
                   };
            return a.ToList();
        }
        public void CustomerMaster_InsertOrUpdate(CustomerMaster c)
        {
            if (c.Id == 0)
            {
                var i = new EF.CustomerMaster
                {
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Mi = c.Mi,
                    Dateofbirth = c.Dateofbirth,
                    Gender = c.Gender,
                    Address = c.Address,
                    City = c.City,
                    Province = c.Province,
                    PostCode = c.PostCode,
                    DurationYears = c.DurationYears,
                    DurationMonth = c.DurationMonth,
                    HomeType = c.HomeType,
                    HomePhone = c.HomePhone,
                    WorkPhone = c.WorkPhone,
                    CellPhone = c.CellPhone,
                    PhoneListedunder = c.PhoneListedunder,
                    SocialSecurityNumber = c.SocialSecurityNumber,
                    DrivingLicenseNumber = c.DrivingLicenseNumber,
                    IssuedAt = c.IssuedAt,
                    Comments = c.Comments,
                    Createdat = c.Createdat,
                    CreatedBy = c.CreatedBy,
                    ImageName = c.ImageName,
                };
                db.proc_UpdateIdentityCustimerMaster();
                db.CustomerMasters.AddObject(i);
                db.SaveChanges();
                c.Id = i.Id;
            }


            else
            {
                var u = db.CustomerMasters.Where(p => p.Id == c.Id).Single();
                u.FirstName = c.FirstName;
                u.LastName = c.LastName;
                u.Mi = c.Mi;
                u.Dateofbirth = c.Dateofbirth;
                u.Gender = c.Gender;
                u.Address = c.Address;
                u.City = c.City;
                u.Province = c.Province;
                u.PostCode = c.PostCode;
                u.DurationYears = c.DurationYears;
                u.DurationMonth = c.DurationMonth;
                u.HomeType = c.HomeType;
                u.HomePhone = c.HomePhone;
                u.WorkPhone = c.WorkPhone;
                u.CellPhone = c.CellPhone;
                u.PhoneListedunder = c.PhoneListedunder;
                u.SocialSecurityNumber = c.SocialSecurityNumber;
                u.DrivingLicenseNumber = c.DrivingLicenseNumber;
                u.IssuedAt = c.IssuedAt;
                u.Comments = c.Comments;
                u.Createdat = c.Createdat;
                u.CreatedBy = c.CreatedBy;
                u.ImageName = c.ImageName;
                db.SaveChanges();
            }
        }
        #endregion

        #region Customer Bank information
        public IQueryable<CustomerBankInformation> CustomerBankInformations
        {
            get
            {
                return from c in db.CustomerBankInformations
                       select new CustomerBankInformation
                       {
                           Id = c.Id,
                           CustomerId = c.CustomerId,
                           BankName = c.BankName,
                           AccountNumber = c.AccountNumber,
                           Address = c.Address,
                           City = c.City,
                           Province = c.Province,
                           PostCode = c.PostCode,
                           NoofSNF = c.NoofSNF,
                           IsBankrutcy = c.IsBankrutcy,
                           IsAssignments = c.IsAssignments,
                           CreatedDate = c.CreatedDate,
                           CreatedBy = c.CreatedBy,
                           TransitNo = c.TransitNo,
                           InstitutionNo = c.InstitutionNo,
                       };
            }
        }
        public List<CustomerBankInformation> CustomerBankInformationsByCustomerId(int Id)
        {
                var a = from c in db.CustomerBankInformations
                       where c.CustomerId==Id
                       select new CustomerBankInformation
                       {
                           Id = c.Id,
                           CustomerId = c.CustomerId,
                           BankName = c.BankName,
                           AccountNumber = c.AccountNumber,
                           Address = c.Address,
                           City = c.City,
                           Province = c.Province,
                           PostCode = c.PostCode,
                           NoofSNF = c.NoofSNF,
                           IsBankrutcy = c.IsBankrutcy,
                           IsAssignments = c.IsAssignments,
                           CreatedDate = c.CreatedDate,
                           CreatedBy = c.CreatedBy,
                           TransitNo = c.TransitNo,
                           InstitutionNo = c.InstitutionNo,
                       };
                return a.ToList();
        }
        public void CustomerBankInformation_InsertOrUpdate(CustomerBankInformation c)
        {
            if (c.Id == 0)
            {
                var i = new EF.CustomerBankInformation
                {
                    CustomerId = c.CustomerId,
                    BankName = c.BankName,
                    AccountNumber = c.AccountNumber,
                    Address = c.Address,
                    City = c.City,
                    Province = c.Province,
                    PostCode = c.PostCode,
                    NoofSNF = c.NoofSNF,
                    IsBankrutcy = c.IsBankrutcy,
                    IsAssignments = c.IsAssignments,
                    CreatedDate = c.CreatedDate,
                    CreatedBy = c.CreatedBy,
                    TransitNo = c.TransitNo,
                    InstitutionNo = c.InstitutionNo,
                };

                db.CustomerBankInformations.AddObject(i);
                db.SaveChanges();
                c.Id = i.Id;
            }


            else
            {
                var u = db.CustomerBankInformations.Where(p => p.Id == c.Id).Single();
                u.CustomerId = c.CustomerId;
                u.BankName = c.BankName;
                u.AccountNumber = c.AccountNumber;
                u.Address = c.Address;
                u.City = c.City;
                u.Province = c.Province;
                u.PostCode = c.PostCode;
                u.NoofSNF = c.NoofSNF;
                u.IsBankrutcy = c.IsBankrutcy;
                u.IsAssignments = c.IsAssignments;
                u.CreatedDate = c.CreatedDate;
                u.CreatedBy = c.CreatedBy;
                u.TransitNo = c.TransitNo;
                u.InstitutionNo = c.InstitutionNo;
                db.SaveChanges();
            }
        }

        #endregion

        #region Customer employment
        public IQueryable<CustomerEmployment> CustomerEmployments
        {
            get
            {
                return from c in db.CustomerEmployments
                       select new CustomerEmployment
                       {
                           Id = c.Id,
                           CustomerId = c.CustomerId,
                           EmployerName = c.EmployerName,
                           SupervisorName = c.SupervisorName,
                           Address = c.Address,
                           City = c.City,
                           Province = c.Province,
                           PostCode = c.PostCode,
                           EmployerDurationYears = c.EmployerDurationYears,
                           EmployerDurationMonth = c.EmployerDurationMonth,
                           EmployerType = c.EmployerType,
                           SupervisorPhone = c.SupervisorPhone,
                           SupervisorExt = c.SupervisorExt,
                           HRPhone = c.HRPhone,
                           HRExt = c.HRExt,
                           CreatedDate = c.CreatedDate,
                           CreatedBy = c.CreatedBy,
                       };
            }
        }
        public List<CustomerEmployment> CustomerEmploymentsbyCustomerId(int Id)
        {
            
                var a= from c in db.CustomerEmployments
                       where c.CustomerId==Id
                       select new CustomerEmployment
                       {
                           Id = c.Id,
                           CustomerId = c.CustomerId,
                           EmployerName = c.EmployerName,
                           SupervisorName = c.SupervisorName,
                           Address = c.Address,
                           City = c.City,
                           Province = c.Province,
                           PostCode = c.PostCode,
                           EmployerDurationYears = c.EmployerDurationYears,
                           EmployerDurationMonth = c.EmployerDurationMonth,
                           EmployerType = c.EmployerType,
                           SupervisorPhone = c.SupervisorPhone,
                           SupervisorExt = c.SupervisorExt,
                           HRPhone = c.HRPhone,
                           HRExt = c.HRExt,
                           CreatedDate = c.CreatedDate,
                           CreatedBy = c.CreatedBy,
                       };
                return a.ToList();
        }

        public void CustomerEmployment_InsertOrUpdate(CustomerEmployment c)
        {
            if (c.Id == 0)
            {
                var i = new EF.CustomerEmployment
                {
                    CustomerId = c.CustomerId,
                    EmployerName = c.EmployerName,
                    SupervisorName = c.SupervisorName,
                    Address = c.Address,
                    City = c.City,
                    Province = c.Province,
                    PostCode = c.PostCode,
                    EmployerDurationYears = c.EmployerDurationYears,
                    EmployerDurationMonth = c.EmployerDurationMonth,
                    EmployerType = c.EmployerType,
                    SupervisorPhone = c.SupervisorPhone,
                    SupervisorExt = c.SupervisorExt,
                    HRPhone = c.HRPhone,
                    HRExt = c.HRExt,
                    CreatedDate = c.CreatedDate,
                    CreatedBy = c.CreatedBy,
                };

                db.CustomerEmployments.AddObject(i);
                db.SaveChanges();
                c.Id = i.Id;
            }


            else
            {
                var u = db.CustomerEmployments.Where(p => p.Id == c.Id).Single();
                u.CustomerId = c.CustomerId;
                u.EmployerName = c.EmployerName;
                u.SupervisorName = c.SupervisorName;
                u.Address = c.Address;
                u.City = c.City;
                u.Province = c.Province;
                u.PostCode = c.PostCode;
                u.EmployerDurationYears = c.EmployerDurationYears;
                u.EmployerDurationMonth = c.EmployerDurationMonth;
                u.EmployerType = c.EmployerType;
                u.SupervisorPhone = c.SupervisorPhone;
                u.SupervisorExt = c.SupervisorExt;
                u.HRPhone = c.HRPhone;
                u.HRExt = c.HRExt;
                u.CreatedDate = c.CreatedDate;
                u.CreatedBy = c.CreatedBy;

                db.SaveChanges();
            }
        }


        #endregion

        #region Customer Income
        public IQueryable<CustomerIncome> CutomerIncomes
        {
            get
            {
                return from c in db.CustomerIncomes
                       select new CustomerIncome
                       {
                           Id = c.Id,
                           CustomerId = c.CustomerId,
                           TakehomePay = c.TakehomePay,
                           IsComputerized = c.IsComputerized,
                           FrequencyofPay = c.FrequencyofPay,
                           IsDirectDeposit = c.IsDirectDeposit,
                           Createdate = c.Createdate,
                           CreatedBy = c.CreatedBy,
                       };
            }
        }
        public List<CustomerIncome> CutomerIncomesbyCustomerId(int Id)
        {
             
                var a= from c in db.CustomerIncomes
                       where c.CustomerId==Id
                       select new CustomerIncome
                       {
                           Id = c.Id,
                           CustomerId = c.CustomerId,
                           TakehomePay = c.TakehomePay,
                           IsComputerized = c.IsComputerized,
                           FrequencyofPay = c.FrequencyofPay,
                           IsDirectDeposit = c.IsDirectDeposit,
                           Createdate = c.Createdate,
                           CreatedBy = c.CreatedBy,
                       };
                return a.ToList();
        }
        public void CutomerIncome_InsertOrUpdate(CustomerIncome c)
        {
            if (c.Id == 0)
            {
                var i = new EF.CustomerIncome
                {
                    CustomerId = c.CustomerId,
                    TakehomePay = c.TakehomePay,
                    IsComputerized = c.IsComputerized,
                    FrequencyofPay = c.FrequencyofPay,
                    IsDirectDeposit = c.IsDirectDeposit,
                    Createdate = c.Createdate,
                    CreatedBy = c.CreatedBy,
                };

                db.CustomerIncomes.AddObject(i);
                db.SaveChanges();
                c.Id = i.Id;
            }


            else
            {
                var u = db.CustomerIncomes.Where(p => p.Id == c.Id).Single();
                u.CustomerId = c.CustomerId;
                u.TakehomePay = c.TakehomePay;
                u.IsComputerized = c.IsComputerized;
                u.FrequencyofPay = c.FrequencyofPay;
                u.IsDirectDeposit = c.IsDirectDeposit;
                u.Createdate = c.Createdate;
                u.CreatedBy = c.CreatedBy;

                db.SaveChanges();
            }
        }


        #endregion

        #region Customer Reference
        public IQueryable<CustomerReference> CustomerReferences
        {
            get
            {
                return from c in db.CustomerReferences
                       select new CustomerReference
                       {
                           Id = c.Id,
                           CustomerId = c.CustomerId,
                           Reference1Name = c.Reference1Name,
                           Reference1Phone = c.Reference1Phone,
                           Reference1Relation = c.Reference1Relation,
                           Reference2Name = c.Reference2Name,
                           Reference2Phone = c.Reference2Phone,
                           Reference2Relation = c.Reference2Relation,
                           Reference3Name = c.Reference3Name,
                           Reference3Phone = c.Reference3Phone,
                           Reference3Relation = c.Reference3Relation,
                           CreatedDate = c.CreatedDate,
                           CreatedBy = c.CreatedBy,
                       };
            }
        }
        public List<CustomerReference> CustomerReferencesbyCustomerId(int Id)
        {
                var a= from c in db.CustomerReferences
                       where c.CustomerId==Id
                       select new CustomerReference
                       {
                           Id = c.Id,
                           CustomerId = c.CustomerId,
                           Reference1Name = c.Reference1Name,
                           Reference1Phone = c.Reference1Phone,
                           Reference1Relation = c.Reference1Relation,
                           Reference2Name = c.Reference2Name,
                           Reference2Phone = c.Reference2Phone,
                           Reference2Relation = c.Reference2Relation,
                           Reference3Name = c.Reference3Name,
                           Reference3Phone = c.Reference3Phone,
                           Reference3Relation = c.Reference3Relation,
                           CreatedDate = c.CreatedDate,
                           CreatedBy = c.CreatedBy,
                       };
                return a.ToList();
        }


        public void CustomerReference_InsertOrUpdate(CustomerReference c)
        {
            if (c.Id == 0)
            {
                var i = new EF.CustomerReference
                {
                    CustomerId = c.CustomerId,
                    Reference1Name = c.Reference1Name,
                    Reference1Phone = c.Reference1Phone,
                    Reference1Relation = c.Reference1Relation,
                    Reference2Name = c.Reference2Name,
                    Reference2Phone = c.Reference2Phone,
                    Reference2Relation = c.Reference2Relation,
                    Reference3Name = c.Reference3Name,
                    Reference3Phone = c.Reference3Phone,
                    Reference3Relation = c.Reference3Relation,
                    CreatedDate = c.CreatedDate,
                    CreatedBy = c.CreatedBy,
                };

                db.CustomerReferences.AddObject(i);
                db.SaveChanges();
                c.Id = i.Id;
            }


            else
            {
                var u = db.CustomerReferences.Where(p => p.Id == c.Id).Single();
                u.CustomerId = c.CustomerId;
                u.Reference1Name = c.Reference1Name;
                u.Reference1Phone = c.Reference1Phone;
                u.Reference1Relation = c.Reference1Relation;
                u.Reference2Name = c.Reference2Name;
                u.Reference2Phone = c.Reference2Phone;
                u.Reference2Relation = c.Reference2Relation;
                u.Reference3Name = c.Reference3Name;
                u.Reference3Phone = c.Reference3Phone;
                u.Reference3Relation = c.Reference3Relation;
                u.CreatedDate = c.CreatedDate;
                u.CreatedBy = c.CreatedBy;

                db.SaveChanges();
            }
        }
        #endregion

        #region User
        public IQueryable<User> Users
        {
            get
            {
                return from r in db.Users
                       select new User
                       {
                           Id = r.Id,
                           UserName = r.UserName,
                           Password = r.Password,
                           CreatedDate = r.CreatedDate,
                           UserType = r.UserType,
                           PasswordHash = r.PasswordHash,
                           StoreId = r.StoreId,
                       };
            }
        }
        public IQueryable<UserStoreDetails> UserStoreDetail
        {
            get
            {
                return from r in db.Users
                       join s in db.CompanyStores on r.StoreId equals s.Id
                       select new UserStoreDetails
                       {
                           Id = r.Id,
                           UserName = r.UserName,
                           Password = r.Password,
                           CreatedDate = r.CreatedDate,
                           UserType = r.UserType,
                           PasswordHash = r.PasswordHash,
                           StoreId = r.StoreId,
                           StoreName = s.Name + "(" + s.Address + ")"
                       };
            }
        }

        public void User_InsertOrUpdate(User r)
        {
            if (r.Id == 0)
            {
                var i = new EF.User
                {
                    UserName = r.UserName,
                    Password = r.Password,
                    CreatedDate = r.CreatedDate,
                    UserType = r.UserType,
                    PasswordHash = r.PasswordHash,
                    StoreId = r.StoreId
                };

                db.Users.AddObject(i);
                db.SaveChanges();
                r.Id = i.Id;
            }
            else
            {
                var u = db.Users.Where(p => p.Id == r.Id).Single();
                u.UserName = r.UserName;
                u.Password = r.Password;
                u.CreatedDate = r.CreatedDate;
                u.UserType = r.UserType;
                u.PasswordHash = r.PasswordHash;
                u.StoreId = r.StoreId;
                db.SaveChanges();
            }
        }


        public IQueryable<AdminUser> AdminUsers
        {
            get
            {
                return from a in db.AdminUsers
                       select new AdminUser
                       {
                           Id = a.Id,
                           Username = a.Username,
                           Password = a.Password,
                       };
            }
        }

        public void AdminUser_InsertOrUpdate(AdminUser a)
        {
            if (a.Id == 0)
            {
                var i = new EF.AdminUser
                {
                    Username = a.Username,
                    Password = a.Password,
                };

                db.AdminUsers.AddObject(i);
                db.SaveChanges();
                a.Id = i.Id;
            }
            else
            {
                var u = db.AdminUsers.Where(p => p.Id == a.Id).Single();
                u.Username = a.Username;
                u.Password = a.Password;

                db.SaveChanges();
            }
        }


        #endregion

        #region CustomMessage
        public IQueryable<CustomMessage> CustomMessages
        {
            get
            {
                return from c in db.CustomMessages
                       select new CustomMessage
                       {
                           Id = c.Id,
                           Message = c.Message,
                       };
            }
        }

        public void CustomMessage_InsertOrUpdate(CustomMessage c)
        {
            if (c.Id == 0)
            {
                var i = new EF.CustomMessage
                {
                    Message = c.Message,
                };

                db.CustomMessages.AddObject(i);
                db.SaveChanges();
                c.Id = i.Id;
            }


            else
            {
                var u = db.CustomMessages.Where(p => p.Id == c.Id).Single();
                u.Message = c.Message;

                db.SaveChanges();
            }
        }

        #endregion

        #region RoleMenu
        public IQueryable<Menu> Menus
        {
            get
            {
                return from m in db.Menus
                       select new Menu
                       {
                           Id = m.Id,
                           MenuName = m.MenuName,
                           MenuLink = m.MenuLink,
                           DisplayOrder = m.DisplayOrder,
                       };
            }
        }

        public void Menu_InsertOrUpdate(Menu m)
        {
            if (m.Id == 0)
            {
                var i = new EF.Menu
                {
                    MenuName = m.MenuName,
                    MenuLink = m.MenuLink,
                    DisplayOrder = m.DisplayOrder,
                };

                db.Menus.AddObject(i);
                db.SaveChanges();
                m.Id = i.Id;
            }


            else
            {
                var u = db.Menus.Where(p => p.Id == m.Id).Single();
                u.MenuName = m.MenuName;
                u.MenuLink = m.MenuLink;
                u.DisplayOrder = m.DisplayOrder;

                db.SaveChanges();
            }
        }

        public IQueryable<UserRoleMenu> UserRoleMenus
        {
            get
            {
                return from r in db.UserRoleMenus
                       select new UserRoleMenu
                       {
                           Id = r.Id,
                           UserRoleTypeId = r.UserRoleTypeId,
                           MenuId = r.MenuId,
                       };
            }
        }

        public void UserRoleMenu_InsertOrUpdate(UserRoleMenu r)
        {
            if (r.Id == 0)
            {
                var i = new EF.UserRoleMenu
                {
                    UserRoleTypeId = r.UserRoleTypeId,
                    MenuId = r.MenuId,
                };

                db.UserRoleMenus.AddObject(i);
                db.SaveChanges();
                r.Id = i.Id;
            }


            else
            {
                var u = db.UserRoleMenus.Where(p => p.Id == r.Id).Single();
                u.UserRoleTypeId = r.UserRoleTypeId;
                u.MenuId = r.MenuId;

                db.SaveChanges();
            }
        }

        public IQueryable<UserMenu> UserMenus
        {
            get
            {
                return from r in db.UserRoleMenus
                       join m in db.Menus on r.MenuId equals m.Id
                       select new UserMenu
                       {
                           MenuId = m.Id,
                           DisplayOrder = m.DisplayOrder,
                           RoleId = r.UserRoleTypeId,
                           MenuName = m.MenuName,
                           MenuLink = m.MenuLink
                       };
            }
        }
        #endregion

        public DataSet GetRegistrarReport(DateTime FromDate, DateTime ToDate, int StoreId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(ConnectionString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "proc_GetReportData";
            cmd.Parameters.Add(new SqlParameter("@FromDate", FromDate));
            cmd.Parameters.Add(new SqlParameter("@ToDate", ToDate));
            cmd.Parameters.Add(new SqlParameter("@StoreId", StoreId));
            cmd.Connection.Open();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet("Data");
            adp.Fill(ds);
            return ds;
        }

        public DataSet GetLoanCloseReport(int UserId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(ConnectionString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Proc_GetLoanCloseReport";
            cmd.Parameters.Add(new SqlParameter("@UserId", UserId));
            cmd.Connection.Open();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet("Data");
            adp.Fill(ds);
            return ds;
        }
        public DataSet DownloadReport(int StoreId, DateTime fromdate, DateTime todate)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(ConnectionString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "proc_GetAdminStoreReport";
            cmd.Parameters.Add(new SqlParameter("@StoreId", StoreId));
            //cmd.Parameters.Add(new SqlParameter("@CreatedDate", date));
            cmd.Parameters.Add(new SqlParameter("@fromdate", fromdate));
            cmd.Parameters.Add(new SqlParameter("@todate", todate.AddDays(1)));
            cmd.Connection.Open();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet("Data");
            adp.Fill(ds);
            return ds;
        }
        public DataSet DownloadTermReport(int StoreId, DateTime fromdate,DateTime todate)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(ConnectionString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "proc_GetTermLoanStoreReport";
            cmd.Parameters.Add(new SqlParameter("@StoreId", StoreId));
            cmd.Parameters.Add(new SqlParameter("@fromdate", fromdate));
            cmd.Parameters.Add(new SqlParameter("@todate", todate));
            cmd.Connection.Open();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet("Data");
            adp.Fill(ds);
            return ds;
        }
        public DataSet DownloadReport_Old(int StoreId, DateTime date)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection("Data Source=209.17.116.11;Initial Catalog=cashshopdatabase;Persist Security Info=True;User ID=rarora;Password=Finbond123;");
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = " SELECT loan.loan_store_id StoreId, customer.cust_customer_id [Customer Id], cust_first_name FirstName , ISNULL(cust_middle_initial,'') [Middle Initial], " +
 "cust_last_name LastName, customer.cust_street_address Address, customer.cust_city City, customer.cust_province Province, customer.cust_postal_code PostCode," +
 "CONVERT(varChar(10),loan_date,103) AS CreatedDate, loan_amount_applied [Amount Applied], loan_amount_approved [Approved Amount], loan_due_amount DueAmount," +
 "CONVERT(varChar(10),loan_due_date,103) AS due_date" +
 " FROM customer JOIN loan on  customer.cust_customer_id = loan.loan_customer_id" +
 " WHERE loan_store_id = " + StoreId.ToString() + " AND loan_status_type NOT IN ('Loan CANCELLED','Loan Denied')" +
 " AND loan_date = '" + date.ToString("yyyy-MM-dd") + "'";
            cmd.Connection.Open();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet("Data");
            adp.Fill(ds);
            return ds;
        }
        public DataSet Get24hrStats(int StoreId,int UserId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(ConnectionString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "proc_Get24hrStats";
            cmd.Parameters.Add(new SqlParameter("@StoreId", StoreId));
            cmd.Parameters.Add(new SqlParameter("@UserId", UserId));
            cmd.Connection.Open();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet("Data");
            adp.Fill(ds);
            return ds;
        }
        public DataSet Get7daysPaydayLoanStats(int StoreId, int UserId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(ConnectionString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetLast7DayPaydayLoanStats";
            cmd.Parameters.Add(new SqlParameter("@StoreId", StoreId));
            cmd.Parameters.Add(new SqlParameter("@UserId", UserId));
            cmd.Connection.Open();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet("Data");
            adp.Fill(ds);
            return ds;
        }
        public void Dispose() // helper finalize function
        {
            // here you can free the resources you allocated explicitly
            System.GC.SuppressFinalize(this);
        }
        // Fetch Messages
        public DataSet GetCustomerMessages(int customerId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(ConnectionString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_GetCustomerMessages";
            cmd.Parameters.Add(new SqlParameter("@CustomerId", customerId));

            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet("Messages");
            adp.Fill(ds);
            return ds;
        }
        // Save/Update Message
        public void SaveCustomerMessage(int id, int customerId, string message, bool isPinned)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_UpsertCustomerMessage", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", customerId));
                cmd.Parameters.Add(new SqlParameter("@MessageText", message));
                cmd.Parameters.Add(new SqlParameter("@IsPinned", isPinned));

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void DeleteCustomerMessage(int id)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_DeleteCustomerMessage", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
