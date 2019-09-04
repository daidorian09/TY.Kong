using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TY.Services.Bank.Cqrs.Account.Commands;
using TY.Services.Bank.Cqrs.Account.Queries;
using TY.Services.Bank.Cqrs.Handlers;
using TY.Services.Bank.Cqrs.Transaction.Commands;
using TY.Services.Bank.Models.Account;
using TY.Services.Bank.Models.Request.Transaction;
using TY.Services.Bank.Models.Response;

namespace TY.Services.Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        #region Fields

        private readonly ICommandHandler<CreateTransactionCommand, BaseResponse<bool>> _createTransactionCommandHandler;

        #endregion

        #region Ctor

        public TransactionsController(ICommandHandler<CreateTransactionCommand, BaseResponse<bool>> createTransactionCommandHandler,
            ICommandHandler<GetAccountById, BaseResponse<Account>> getAccountByIdCommandHandler,
            ICommandHandler<DeleteAccountCommand, BaseResponse<bool>> deleteAccountCommandHandler,
            ICommandHandler<UpdateAccountCommand, BaseResponse<Account>> updateAccountCommandHandler)
        {
            _createTransactionCommandHandler = createTransactionCommandHandler;
        }

        #endregion

        // POST api/transactions
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post([FromBody] CreateTransactionRequest request)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;

            var response = await _createTransactionCommandHandler.HandleAsync(new CreateTransactionCommand()
            {
                Account = claimsIdentity?.Claims.FirstOrDefault(q=> q.Type.Equals(ClaimTypes.NameIdentifier))?.Value,
                TransactionType = request.TransactionType,
                Amount = request.Amount,
                CurrentBalance = default,
                OldBalance = default
            }, CancellationToken.None);

            return Ok(response);
        }
    }
}