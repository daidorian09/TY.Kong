using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TY.Services.Bank.Constants;
using TY.Services.Bank.Cqrs.Handlers;
using TY.Services.Bank.Http;
using TY.Services.Bank.Models.Response;
using TY.Services.Bank.Services.Interfaces;

namespace TY.Services.Bank.Cqrs.Account.Commands
{
    public class CrateAccountHandler : ICommandHandler<CrateAccountCommand, BaseResponse<bool>>
    {
        #region Fields

        private readonly IRestClient _restClient;
        private readonly ILogger<CrateAccountHandler> _logger;
        private readonly IPasswordHasher _passwordHasher;

        #endregion


        #region Ctor

        public CrateAccountHandler(IRestClient restClient, ILogger<CrateAccountHandler> logger, IPasswordHasher passwordHasher)
        {
            _restClient = restClient;
            _logger = logger;
            _passwordHasher = passwordHasher;
            _restClient.Configure(EndpointConstants.AccountsBaseUrl, EndpointConstants.AccountsEndpointTimeout);
        }

        #endregion

        public async Task<BaseResponse<bool>> HandleAsync(CrateAccountCommand command, CancellationToken cancellationToken)
        {
            var endpoint = string.Empty;
            var baseResponse = new BaseResponse<bool>()
            {
                StatusCode = HttpStatusCode.OK,
                Errors = string.Empty,
                IsSuccess = true,
                Result = true
            };

            command.Salt = _passwordHasher.GenerateSalt();
            command.Password = _passwordHasher.HashPassword(command.Salt, command.Password);

            var response = (BaseResponse<bool>) await _restClient.PostAsync<BaseResponse<bool>>(endpoint, command);

            if (response is null)
            {
                _logger.LogError($"AccountCommand/CreateAccount returned null body");
                return baseResponse;

            }

            baseResponse = response;
            return baseResponse;
        }
    }
}