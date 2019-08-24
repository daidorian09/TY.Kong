using Newtonsoft.Json;

namespace TY.Services.Bank.Models.Request.Account
{
    public class CreateAccountRequest
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

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("balance")]
        public decimal Balance { get; set; }
    }
}