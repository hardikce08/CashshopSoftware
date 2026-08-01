using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashLoanShop.Model
{
    public class User
    {

        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int UserType { get; set; }
        public string PasswordHash { get; set; }

        public int? StoreId { get; set; }
    }
    public class UserStoreDetails
    {

        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int UserType { get; set; }
        public string PasswordHash { get; set; }
        public int? StoreId { get; set; }
        public string StoreName { get; set; }
        public string UserTypeDesc { get; set; }
    }

    public class Menu
    {
        public int Id { get; set; }

        public string MenuName { get; set; }

        public string MenuLink { get; set; }

        public int DisplayOrder { get; set; }
    }
    public class UserRoleMenu
    {

        public int Id { get; set; }

        public int UserRoleTypeId { get; set; }

        public int MenuId { get; set; }
    }

    public class UserMenu {
        public int MenuId { get; set; }
        public int RoleId { get; set; }
        public string MenuName { get; set; }
        public string MenuLink { get; set; }
        public int DisplayOrder { get; set; }
    }

    public class AdminUser
    {

        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
