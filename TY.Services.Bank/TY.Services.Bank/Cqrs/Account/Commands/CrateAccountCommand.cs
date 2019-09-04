using Newtonsoft.Json;
using TY.Services.Bank.Models.Response;

namespace TY.Services.Bank.Cqrs.Account.Commands
{
    public class CrateAccountCommand : ICommand<BaseResponse<bool>>
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("salt")]
        public string Salt { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("balance")]
        public decimal Balance { get; set; }
    }
}