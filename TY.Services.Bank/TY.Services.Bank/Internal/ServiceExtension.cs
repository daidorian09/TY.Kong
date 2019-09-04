using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using TY.Services.Bank.Http;
using TY.Services.Bank.Services;
using TY.Services.Bank.Services.Interfaces;

namespace TY.Services.Bank.Internal
{
    public static class ServiceExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<HttpClient>();
            services.AddTransient<IRestClient, RestClient>();
            services.AddSingleton<IPasswordHasher, PasswordHasherService>();
            services.AddSingleton<ITokenService, TokenService>();
            services.AddSingleton<ICacheService, CacheService>();
        }
    }
}