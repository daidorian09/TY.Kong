using System.Collections.Generic;
using System.Threading.Tasks;

namespace TY.Services.Bank.Http
{
    public interface IRestClient
    {
        Task<object> PostAsync<T>(string endpoint, object request, Dictionary<string, object> headers = null);
        Task<object> GetAsync<T>(string endpoint, object request, Dictionary<string, object> headers = null);
        Task<object> PutAsync<T>(string endpoint, object request, Dictionary<string, object> headers = null);
        Task<object> DeleteAsync(string endpoint, object request, Dictionary<string, object> headers = null);
        void Configure(string baseUrl, int timeout);
    }
}