using System.Threading.Tasks;

namespace TY.Services.Bank.Services.Interfaces
{
    public interface ICacheService
    {
        Task SetKeyAsync(string key, string value);

        Task DeleteKeyAsync(string value);
    }
}