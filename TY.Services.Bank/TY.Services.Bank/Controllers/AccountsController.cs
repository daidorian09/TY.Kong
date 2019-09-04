using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using TY.Services.Bank.Cqrs.Account.Commands;
using TY.Services.Bank.Cqrs.Account.Queries;
using TY.Services.Bank.Cqrs.Handlers;
using TY.Services.Bank.Models.Account;
using TY.Services.Bank.Models.Request.Account;
using TY.Services.Bank.Models.Response;

namespace TY.Services.Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        #region Fields

        private readonly ICommandHandler<CrateAccountCommand, BaseResponse<bool>> _createAccountCommandHandler;
        private readonly ICommandHandler<GetAccountById, BaseResponse<Account>> _getAccountByIdCommandHandler;
        private readonly ICommandHandler<DeleteAccountCommand, BaseResponse<bool>> _deleteAccountCommandHandler;
        private readonly ICommandHandler<UpdateAccountCommand, BaseResponse<Account>> _updateAccountCommandHandler;

        #endregion

        #region Ctor

        public AccountsController(ICommandHandler<CrateAccountCommand, BaseResponse<bool>> createAccountCommandHandler,
            ICommandHandler<GetAccountById, BaseResponse<Account>> getAccountByIdCommandHandler,
            ICommandHandler<DeleteAccountCommand, BaseResponse<bool>> deleteAccountCommandHandler,
            ICommandHandler<UpdateAccountCommand, BaseResponse<Account>> updateAccountCommandHandler)
        {
            _createAccountCommandHandler = createAccountCommandHandler;
            _getAccountByIdCommandHandler = getAccountByIdCommandHandler;
            _deleteAccountCommandHandler = deleteAccountCommandHandler;
            _updateAccountCommandHandler = updateAccountCommandHandler;
        }

        #endregion

        // POST api/accounts
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateAccountRequest request)
        {
            var response = await _createAccountCommandHandler.HandleAsync(new CrateAccountCommand()
            {
                Address = request.Address,
                Age = request.Age,
                Balance = request.Balance,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Password = request.Password,
                PhoneNumber = request.PhoneNumber
            }, CancellationToken.None);

            return Ok(response);
        }

        // Get api/accounts/id
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult> Get([FromRoute] string id)
        {
            var response = await _getAccountByIdCommandHandler.HandleAsync(new GetAccountById()
            {
                Id = id
            }, CancellationToken.None);

            return Ok(response);
        }

        // Get api/accounts/id
        [HttpDelete("{id}", Name = "Delete")]
        public async Task<ActionResult> Delete([FromRoute] string id)
        {
            var response = await _deleteAccountCommandHandler.HandleAsync(new DeleteAccountCommand()
            {
                Id = id
            }, CancellationToken.None);

            return Ok(response);
        }

        // Get api/accounts/id
        [HttpPut("{id}", Name = "Put")]
        public async Task<ActionResult> Put([FromRoute] string id, [FromBody] UpdateAccountCommand request)
        {
            var response = await _updateAccountCommandHandler.HandleAsync(new UpdateAccountCommand()
            {
                Id = id,
                Address = request.Address,
                Age = request.Age,
                FirstName = request.FirstName,
                LastName = request.LastName
            }, CancellationToken.None);

            return Ok(response);
        }
    }
}