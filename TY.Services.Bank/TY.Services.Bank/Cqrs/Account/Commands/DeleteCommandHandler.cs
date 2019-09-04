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
    public class DeleteCommandHandler : ICommandHandler<DeleteAccountCommand, BaseResponse<bool>>
    {
        #region Fields

        private readonly IRestClient _restClient;
        private readonly ILogger<DeleteCommandHandler> _logger;

        #endregion


        #region Ctor

        public DeleteCommandHandler(IRestClient restClient, ILogger<DeleteCommandHandler> logger)
        {
            _restClient = restClient;
            _logger = logger;
            _restClient.Configure(EndpointConstants.AccountsBaseUrl, EndpointConstants.AccountsEndpointTimeout);
        }

        #endregion

        public async Task<BaseResponse<bool>> HandleAsync(DeleteAccountCommand command, CancellationToken cancellationToken)
        {
            var endpoint = $"{command.Id}";
            var baseResponse = new BaseResponse<bool>()
            {
                StatusCode = HttpStatusCode.OK,
                Errors = string.Empty,
                IsSuccess = true,
                Result = true
            };

            var response = await _restClient.DeleteAsync(endpoint);

            if (response != HttpStatusCode.NoContent)
            {
                _logger.LogError($"DeleteCommandHandler/DeleteAsync returned {response}");
            }

            return baseResponse;
        }
    }
}