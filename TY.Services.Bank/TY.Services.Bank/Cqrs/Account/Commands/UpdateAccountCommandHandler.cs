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
    public class UpdateAccountCommandHandler : ICommandHandler<UpdateAccountCommand, BaseResponse<Models.Account.Account>>
    {
        #region Fields

        private readonly IRestClient _restClient;
        private readonly ILogger<UpdateAccountCommandHandler> _logger;

        #endregion


        #region Ctor

        public UpdateAccountCommandHandler(IRestClient restClient, ILogger<UpdateAccountCommandHandler> logger)
        {
            _restClient = restClient;
            _logger = logger;
            _restClient.Configure(EndpointConstants.AccountsBaseUrl, EndpointConstants.AccountsEndpointTimeout);
        }

        #endregion

        public async Task<BaseResponse<Models.Account.Account>> HandleAsync(UpdateAccountCommand command, CancellationToken cancellationToken)
        {
            var endpoint = $"{command.Id}";
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
                _logger.LogError($"AccountCommand/UpdateAccount returned null body");
                return baseResponse;
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                _logger.LogError($"{EndpointConstants.AccountsBaseUrl}/{endpoint} is called method - Put, status code : ${response.StatusCode} and result : {response.Result}");
                return baseResponse;
            }

            baseResponse = response;
            return baseResponse;
        }
    }
}