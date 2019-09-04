using Newtonsoft.Json;
using TY.Services.Bank.Models.Transaction;

namespace TY.Services.Bank.Models.Request.Transaction
{
    public class CreateTransactionRequest
    {
        [JsonProperty("transactionType")]
        public int TransactionType { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
}