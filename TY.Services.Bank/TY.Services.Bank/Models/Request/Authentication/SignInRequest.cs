using Newtonsoft.Json;

namespace TY.Services.Bank.Models.Request.Authentication
{
    public class SignInRequest
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}