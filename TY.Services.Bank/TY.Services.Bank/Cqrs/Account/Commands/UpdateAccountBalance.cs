using Newtonsoft.Json;
using TY.Services.Bank.Models.Response;

namespace TY.Services.Bank.Cqrs.Account.Commands
{
    public class UpdateAccountBalance : ICommand<Models.Account.Account>, ICommand<BaseResponse<Models.Account.Account>>
    {
        public string Account { get; set; }

        [JsonProperty("balance")]
        public decimal Balance { get; set; }
    }
}