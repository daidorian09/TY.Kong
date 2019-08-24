using System;

namespace TY.Services.Bank.Models.Account
{
    public class Account : IBaseEntity
    {
        public string Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; }
    }
}