using Newtonsoft.Json;
using TY.Services.Bank.Models.Response;

namespace TY.Services.Bank.Cqrs.Authentication.Commands
{
    public class SignInCommand : ICommand<BaseResponse<string>>
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}