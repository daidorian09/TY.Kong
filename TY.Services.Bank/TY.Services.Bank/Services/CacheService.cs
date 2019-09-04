using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using TY.Services.Bank.Constants;
using TY.Services.Bank.Services.Interfaces;

namespace TY.Services.Bank.Services
{
    public class CacheService : ICacheService
    {
        #region Fields

        private readonly ConnectionMultiplexer _redis;
        private IDatabase _database;
        private readonly ILogger<CacheService> _logger;

        #endregion

        #region Ctor

        public CacheService(ILogger<CacheService> logger)
        {
            _logger = logger;
            _redis = ConnectionMultiplexer.Connect(CacheConstants.Host);
            _database = _redis.GetDatabase();
        }

        #endregion
        public async Task SetKeyAsync(string key, string value)
        {
            try
            {
                await _database.StringSetAsync(key, value);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occured on saving key in CacheService/SetKey", e.Message);
            }
        }

        public async Task DeleteKeyAsync(string key)
        {
            var cachedKey = await _database.StringGetAsync(key);

            if (string.IsNullOrEmpty(cachedKey))
            {
                _logger.LogWarning($"Key is not found in Redis or expired");
                throw new ArgumentNullException("401");
            }

            await _database.KeyDeleteAsync(key, CommandFlags.FireAndForget);
        }
    }
}