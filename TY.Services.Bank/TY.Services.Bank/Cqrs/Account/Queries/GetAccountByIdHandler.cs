using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using TY.Services.Bank.Constants;
using TY.Services.Bank.Cqrs.Handlers;
using TY.Services.Bank.Http;
using TY.Services.Bank.Models.Response;

namespace TY.Services.Bank.Cqrs.Account.Queries
{
    public class GetAccountByIdHandler : ICommandHandler<GetAccountById, BaseResponse<Models.Account.Account>>
    {
        #region Fields

        private readonly IRestClient _restClient;
        private readonly ILogger<GetAccountByIdHandler> _logger;

        #endregion


        #region Ctor

        public GetAccountByIdHandler(IRestClient restClient, ILogger<GetAccountByIdHandler> logger)
        {
            _restClient = restClient;
            _logger = logger;
            _restClient.Configure(EndpointConstants.AccountsBaseUrl, EndpointConstants.AccountsEndpointTimeout);
        }

        #endregion
        public async Task<BaseResponse<Models.Account.Account>> HandleAsync(GetAccountById command, CancellationToken cancellationToken)
        {
            var endpoint = $"{command.Id}";
            var baseResponse = new BaseResponse<Models.Account.Account>()
            {
                StatusCode = HttpStatusCode.OK,
                Errors = string.Empty,
                IsSuccess = true,
                Result = default
            };

            var response = (BaseResponse<Models.Account.Account>) await _restClient.GetAsync<BaseResponse<Models.Account.Account>>(endpoint);

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