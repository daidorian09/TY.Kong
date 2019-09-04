using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using TY.Services.Bank.Constants;
using TY.Services.Bank.Cqrs.Handlers;
using TY.Services.Bank.Http;
using TY.Services.Bank.Models.Response;

namespace TY.Services.Bank.Cqrs.Account.Commands
{
    public class UpdateAccountBalanceCommandHandler : ICommandHandler<UpdateAccountBalance, BaseResponse<Models.Account.Account>>
    {
        #region Fields

        private readonly IRestClient _restClient;
        private readonly ILogger<UpdateAccountBalanceCommandHandler> _logger;

        #endregion


        #region Ctor

        public UpdateAccountBalanceCommandHandler(IRestClient restClient, ILogger<UpdateAccountBalanceCommandHandler> logger)
        {
            _restClient = restClient;
            _logger = logger;
            _restClient.Configure(EndpointConstants.AccountsBaseUrl, EndpointConstants.AccountsEndpointTimeout);
        }

        #endregion

        public async Task<BaseResponse<Models.Account.Account>> HandleAsync(UpdateAccountBalance command, CancellationToken cancellationToken)
        {
            var endpoint = $"{command.Account}/balance";
            var baseResponse = new BaseResponse<Models.Account.Account>()
            {
                StatusCode = HttpStatusCode.OK,
                Errors = string.Empty,
                IsSuccess = true,
                Result = default
            };

            var response = (BaseResponse<Models.Account.Account>) await _restClient.PutAsync<BaseResponse<Models.Account.Account>>(endpoint, command);

            if (response is null)
            {
                _logger.LogError($"UpdateAccountCommandHandler/HandleAsync returned null body");
                return baseResponse;
            }

            baseResponse = response;
            return baseResponse;
        }
    }
}