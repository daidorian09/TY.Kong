using Newtonsoft.Json;
using TY.Services.Bank.Models.Response;

namespace TY.Services.Bank.Cqrs.Account.Commands
{
    public class DeleteAccountCommand : ICommand<BaseResponse<bool>>
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}