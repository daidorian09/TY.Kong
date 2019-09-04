using Newtonsoft.Json;
using TY.Services.Bank.Models.Response;

namespace TY.Services.Bank.Cqrs.Account.Queries
{
    public class GetAccountById : ICommand<BaseResponse<Models.Account.Account>>
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}