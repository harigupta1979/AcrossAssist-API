using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Module.Onboard
{
    public class OnboardClass : commonClass
    {
        public Nullable<Int32> OnBoardId { get; set; }
        public string OnBoardName { get; set; }
        public string Address { get; set; }
        [Required(ErrorMessage = "*")]
        public string MobileNo { get; set; }
        [Required(ErrorMessage = "*")]
        public string EmailId { get; set; }
        public string PinCode { get; set; }
        public Nullable<Int32> CountryId { get; set; }
        public Nullable<Int32> StateId { get; set; }
        public Nullable<Int32> CityId { get; set; }
        public Nullable<Int32> LocationId { get; set; }
        public string PANNumber { get; set; }
        public string TANNumber { get; set; }
        public string GSTNumber { get; set; }
        public string MSMENumber { get; set; }
        public string AccountName { get; set; }
        public string AccountNo { get; set; }
        public string IFSCCode { get; set; }
        public Nullable<Int64> BankId { get; set; }
        public Nullable<Int64> AccountTypeId { get; set; }

    }
    public class OnboardOtp
    {
        public Nullable<Int32> OnBoardId { get; set; }
        public string Username { get; set; }
        public int Userotp { get; set; }
        public string Type { get; set; }
        public int Otpid { get; set; }
    }
    public class ApprovalSearch : commonSearchClass
    {
        public string OnBoardName { get; set; }
    }
    public class OnboardSearch : commonSearchClass
    {
        public string OnBoardName { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public Nullable<Int64> OnBoardId { get; set; }
    }
}
