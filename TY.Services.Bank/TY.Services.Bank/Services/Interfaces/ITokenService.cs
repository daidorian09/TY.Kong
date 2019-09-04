using TY.Services.Bank.Models.Account;

namespace TY.Services.Bank.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(Account account);
    }
}