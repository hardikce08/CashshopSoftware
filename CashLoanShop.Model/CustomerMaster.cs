using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashLoanShop.Model
{
    public class CustomerMaster
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Mi { get; set; }
        public DateTime? Dateofbirth { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostCode { get; set; }
        public string DurationYears { get; set; }
        public string DurationMonth { get; set; }
        public string HomeType { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string CellPhone { get; set; }
        public string PhoneListedunder { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string DrivingLicenseNumber { get; set; }
        public string IssuedAt { get; set; }
        public string Comments { get; set; }
        public DateTime? Createdat { get; set; }
        public int? CreatedBy { get; set; }
        public string ImageName { get; set; }
        public string ProvinceName { get; set; }
    }

    public class CustomMessage
    {

        public int Id { get; set; }

        public string Message { get; set; }
    }

    public class CustomerFollowup
    {

        public int Id { get; set; }

        public int? CustomerId { get; set; }

        public DateTime? FollowupDate { get; set; }

        public string FollowupTime { get; set; }

        public string FollowupCode { get; set; }

        public string FollowupDoneBy { get; set; }

        public DateTime? NextFolloupDate { get; set; }

        public string NextFollowupTime { get; set; }

        public string Comments { get; set; }

        public string FinalStatus { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public string CustomerName { get; set; }
        public string ContactNo { get; set; }
    }
}
