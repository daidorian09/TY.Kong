using TY.Services.Bank.Models.Response;

namespace TY.Services.Bank.Cqrs.Account.Commands
{
    public class CrateAccountCommand : ICommand<BaseResponse<string>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; }
    }
}