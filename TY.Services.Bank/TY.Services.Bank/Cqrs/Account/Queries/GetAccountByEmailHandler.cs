using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TY.Services.Bank.Constants;
using TY.Services.Bank.Cqrs.Handlers;
using TY.Services.Bank.Http;
using TY.Services.Bank.Models.Response;

namespace TY.Services.Bank.Cqrs.Account.Queries
{
    public class GetAccountByEmailHandler : ICommandHandler<GetAccountByEmail, BaseResponse<Models.Account.Account>>
    {
        #region Fields

        private readonly IRestClient _restClient;
        private readonly ILogger<GetAccountByEmail> _logger;

        #endregion


        #region Ctor

        public GetAccountByEmailHandler(IRestClient restClient, ILogger<GetAccountByEmail> logger)
        {
            _restClient = restClient;
            _logger = logger;
            _restClient.Configure(EndpointConstants.AccountsBaseUrl, EndpointConstants.AccountsEndpointTimeout);
        }

        #endregion

        public async Task<BaseResponse<Models.Account.Account>> HandleAsync(GetAccountByEmail command,
            CancellationToken cancellationToken)
        {
            var endpoint = $"{command.Email}/email";
            var baseResponse = new BaseResponse<Models.Account.Account>()
            {
                StatusCode = HttpStatusCode.OK,
                Errors = string.Empty,
                IsSuccess = true,
                Result = default
            };

            var response =
                (BaseResponse<Models.Account.Account>) await _restClient.GetAsync<BaseResponse<Models.Account.Account>>(
                    endpoint);

            if (response is null)
            {
                _logger.LogError($"GetAccountByEmailHandler/HandleAsync returned null body");
                return baseResponse;
            }

            baseResponse = response;
            return baseResponse;
        }
    }
}