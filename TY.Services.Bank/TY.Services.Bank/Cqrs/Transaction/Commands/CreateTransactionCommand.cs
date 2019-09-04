using Newtonsoft.Json;
using TY.Services.Bank.Models.Response;

namespace TY.Services.Bank.Cqrs.Transaction.Commands
{
    public class CreateTransactionCommand : ICommand<BaseResponse<bool>>
    {
        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("oldBalance")]
        public decimal OldBalance { get; set; }

        [JsonProperty("currentBalance")]
        public decimal CurrentBalance { get; set; }

        [JsonProperty("transactionType")]
        public int TransactionType { get; set; }
    }
}