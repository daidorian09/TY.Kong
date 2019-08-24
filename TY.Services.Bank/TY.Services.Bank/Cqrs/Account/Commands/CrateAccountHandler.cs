using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TY.Services.Bank.Constants;
using TY.Services.Bank.Cqrs.Handlers;
using TY.Services.Bank.Http;
using TY.Services.Bank.Models.Response;

namespace TY.Services.Bank.Cqrs.Account.Commands
{
    public class CrateAccountHandler : ICommandHandler<CrateAccountCommand, BaseResponse<string>>
    {
        #region Fields

        private readonly IRestClient _restClient;
        private readonly ILogger<CrateAccountHandler> _logger;

        #endregion


        #region Ctor

        public CrateAccountHandler(IRestClient restClient, ILogger<CrateAccountHandler> logger)
        {
            _restClient = restClient;
            _logger = logger;
            _restClient.Configure(EndpointConstants.AccountsBaseUrl, EndpointConstants.AccountsEndpointTimeout);
        }

        #endregion


        public async Task<BaseResponse<string>> HandleAsync(CrateAccountCommand command, CancellationToken cancellationToken)
        {
            var endpoint = "/accounts";
            var baseResponse = new BaseResponse<string>()
            {
                StatusCode = HttpStatusCode.OK,
                Errors = string.Empty,
                IsSuccess = true,
                Result = null
            };

            var postAsync = (BaseResponse<string>) await _restClient.PostAsync<BaseResponse<string>>(endpoint, command);

            if (postAsync is null)
            {
                _logger.LogError($"AccountCommand/CreateAccount returned null body");
                return baseResponse;

            }

            baseResponse = postAsync;
            return baseResponse;
        }
    }
}