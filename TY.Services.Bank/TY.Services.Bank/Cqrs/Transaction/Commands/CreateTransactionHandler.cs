using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using TY.Services.Bank.Constants;
using TY.Services.Bank.Cqrs.Account.Commands;
using TY.Services.Bank.Cqrs.Account.Queries;
using TY.Services.Bank.Cqrs.Handlers;
using TY.Services.Bank.Http;
using TY.Services.Bank.Models.Response;

namespace TY.Services.Bank.Cqrs.Transaction.Commands
{
    public class CreateTransactionHandler : ICommandHandler<CreateTransactionCommand, BaseResponse<bool>>
    {
        #region Fields

        private readonly IRestClient _restClient;
        private readonly ILogger<CreateTransactionHandler> _logger;
        private readonly ICommandHandler<GetAccountById, BaseResponse<Models.Account.Account>> _getAccountByUdCommandHandler;
        private readonly ICommandHandler<UpdateAccountBalance, BaseResponse<Models.Account.Account>> _updateAccountBalanceCommandHandler;

        #endregion

        #region Ctor

        public CreateTransactionHandler(IRestClient restClient, ILogger<CreateTransactionHandler> logger,
            ICommandHandler<GetAccountById, BaseResponse<Models.Account.Account>> getAccountByUdCommandHandler,
            ICommandHandler<UpdateAccountBalance, BaseResponse<Models.Account.Account>> updateAccountBalanceCommandHandler)
        {
            _restClient = restClient;
            _logger = logger;
            _getAccountByUdCommandHandler = getAccountByUdCommandHandler;
            _updateAccountBalanceCommandHandler = updateAccountBalanceCommandHandler;
            _restClient.Configure(EndpointConstants.TransactionsBaseUrl, EndpointConstants.TransactionsEndpointTimeout);
        }

        #endregion
        public async Task<BaseResponse<bool>> HandleAsync(CreateTransactionCommand command, CancellationToken cancellationToken)
        {
            var endpoint = string.Empty;
            var baseResponse = new BaseResponse<bool>()
            {
                StatusCode = HttpStatusCode.OK,
                Errors = string.Empty,
                IsSuccess = true,
                Result = true
            };

            var getAccountById = await _getAccountByUdCommandHandler.HandleAsync(new GetAccountById()
            {
                Id = command.Account
            }, cancellationToken);

            if (getAccountById.Result is null)
            {
                _logger.LogError($"_getAccountByUdCommandHandler/HandleAsync account is not found");
                baseResponse.Result = false;
                baseResponse.IsSuccess = getAccountById.IsSuccess;
                baseResponse.Errors = getAccountById.Errors;
                return baseResponse;
            }

            command.CurrentBalance = command.Amount + getAccountById.Result.Balance;
            command.OldBalance = getAccountById.Result.Balance;
            
            var response = (BaseResponse<bool>) await _restClient.PostAsync<BaseResponse<bool>>(endpoint, command);

            if (response is null)
            {
                _logger.LogError($"CreateTransactionHandler/CreateAccount returned null body");
                return baseResponse;
            }

            var updateAccountBalance = await _updateAccountBalanceCommandHandler.HandleAsync(new UpdateAccountBalance()
            {
                Balance = command.CurrentBalance,
                Account = command.Account
            }, CancellationToken.None);

            if (updateAccountBalance.Result is null)
            {
                _logger.LogError($"_updateAccountBalanceCommandHandler/HandleAsync returned null body");
                return baseResponse;
            }

            baseResponse = response;
            return baseResponse;
        }
    }
}