using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using TY.Services.Bank.Constants;
using TY.Services.Bank.Cqrs.Account.Queries;
using TY.Services.Bank.Cqrs.Handlers;
using TY.Services.Bank.Models.Response;
using TY.Services.Bank.Services.Interfaces;

namespace TY.Services.Bank.Cqrs.Authentication.Commands
{
    public class SignInHandler : ICommandHandler<SignInCommand, BaseResponse<string>>
    {
        #region Fields

        private readonly ICommandHandler<GetAccountByEmail, BaseResponse<Models.Account.Account>> _getAccountByEmailCommandHandler;
        private readonly ILogger<SignInHandler> _logger;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ICacheService _cacheService;

        #endregion

        #region Ctor

        public SignInHandler(
            ICommandHandler<GetAccountByEmail, BaseResponse<Models.Account.Account>> getAccountByEmailCommandHandler,
            ILogger<SignInHandler> logger, ITokenService tokenService, IPasswordHasher passwordHasher, ICacheService cacheService)
        {
            _getAccountByEmailCommandHandler = getAccountByEmailCommandHandler;
            _logger = logger;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
            _cacheService = cacheService;
        }

        #endregion
        public async Task<BaseResponse<string>> HandleAsync(SignInCommand command, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<string>()
            {
                Errors = string.Empty,
                IsSuccess = false,
                Result = string.Empty,
                StatusCode = HttpStatusCode.OK
            };

            var account = await _getAccountByEmailCommandHandler.HandleAsync(new GetAccountByEmail() {Email = command.Email},
                CancellationToken.None);

            if (account?.Result is null || account?.StatusCode != HttpStatusCode.OK)
            {
                _logger.LogError($"SignInHandler/HandleAsync - Account not found");
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Errors = "Account is not found";
                return response;
            }

            var password = _passwordHasher.HashPassword(account.Result.Salt, command.Password);
            var isAccountFound = _passwordHasher.ValidatePassword(account.Result.Password, password);

            if (!isAccountFound)
            {
                _logger.LogError($"SignInHandler/HandleAsync - Account credentials invalid");
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Errors = "Account credentials are invalid";
                return response;
            }

            response.Result = _tokenService.GenerateJwtToken(account.Result);

            await _cacheService.SetKeyAsync($"{CacheConstants.AuthorizationKeyPrefix}{response.Result}", account.Result.Id);

            return response;
        }
    }
}