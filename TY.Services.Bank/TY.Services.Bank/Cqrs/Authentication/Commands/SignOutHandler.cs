using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using TY.Services.Bank.Constants;
using TY.Services.Bank.Cqrs.Handlers;
using TY.Services.Bank.Internal;
using TY.Services.Bank.Models.Response;
using TY.Services.Bank.Services.Interfaces;

namespace TY.Services.Bank.Cqrs.Authentication.Commands
{
    public class SignOutHandler : ICommandHandler<SignOutCommand, BaseResponse<bool>>
    {
        #region Fields

        private readonly ILogger<SignOutHandler> _logger;
        private readonly ICacheService _cacheService;

        #endregion

        #region Ctor

        public SignOutHandler(ILogger<SignOutHandler> logger, ICacheService cacheService)
        {
            _logger = logger;
            _cacheService = cacheService;
        }

        #endregion
        public async Task<BaseResponse<bool>> HandleAsync(SignOutCommand command, CancellationToken cancellationToken)
        {
            BaseResponse<bool> response = new BaseResponse<bool>()
            {
                Result = false,
                Errors = string.Empty,
                IsSuccess = false,
                StatusCode = HttpStatusCode.Unauthorized
            };

            var token = command.Token.ParseBearerFromHeader();

            if (string.IsNullOrEmpty(token))
            {
                _logger.LogWarning($"Unauthorized access attempt in SignOutHandler/HandleAsync");
                response.Errors = "Token is invalid";
            }

            try
            {
                await _cacheService.DeleteKeyAsync($"{CacheConstants.AuthorizationKeyPrefix}{token}");
            }
            catch (ArgumentException ex) when (ex.ParamName.Equals("401"))
            {
                _logger.LogWarning($"Token is expired", ex.Message);
                response.Errors = "Token is invalid";
                return response;
            }

            response.Result = true;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;

            return response;
        }
    }
}