using TY.Services.Bank.Models.Response;

namespace TY.Services.Bank.Cqrs.Authentication.Commands
{
    public class SignOutCommand : ICommand<BaseResponse<bool>>
    {
        public string Token { get; set; }
    }
}