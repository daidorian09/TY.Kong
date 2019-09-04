using Newtonsoft.Json;
using TY.Services.Bank.Models.Response;

namespace TY.Services.Bank.Cqrs.Account.Queries
{
    public class GetAccountByEmail : ICommand<BaseResponse<Models.Account.Account>>
    {
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}