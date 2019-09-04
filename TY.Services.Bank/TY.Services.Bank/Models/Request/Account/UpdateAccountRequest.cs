using Newtonsoft.Json;

namespace TY.Services.Bank.Models.Request.Account
{
    public class UpdateAccountRequest
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }
    }
}