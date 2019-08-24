using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using TY.Services.Bank.Cqrs.Account.Commands;
using TY.Services.Bank.Cqrs.Handlers;
using TY.Services.Bank.Models.Request.Account;
using TY.Services.Bank.Models.Response;

namespace TY.Services.Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        #region Fields

        private readonly ICommandHandler<CrateAccountCommand, BaseResponse<string>> _commandHandler;

        #endregion

        #region Ctor

        public AccountsController(ICommandHandler<CrateAccountCommand, BaseResponse<string>> commandHandler)
        {
            _commandHandler = commandHandler;
        }

        #endregion

        // POST api/accounts
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateAccountRequest request)
        {
            var response = await _commandHandler.HandleAsync(new CrateAccountCommand()
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
    }
}