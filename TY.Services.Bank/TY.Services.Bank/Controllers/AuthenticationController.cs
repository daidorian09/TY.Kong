using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TY.Services.Bank.Cqrs.Authentication.Commands;
using TY.Services.Bank.Cqrs.Handlers;
using TY.Services.Bank.Models.Request.Authentication;
using TY.Services.Bank.Models.Response;

namespace TY.Services.Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        #region Fields

        private readonly ICommandHandler<SignInCommand, BaseResponse<string>> _signInCommandHandler;
        private readonly ICommandHandler<SignOutCommand, BaseResponse<bool>> _signOutCommandHandler;

        #endregion

        #region Ctor

        public AuthenticationController(ICommandHandler<SignInCommand, BaseResponse<string>> signInCommandHandler, ICommandHandler<SignOutCommand, BaseResponse<bool>> signOutCommandHandler)
        {
            _signInCommandHandler = signInCommandHandler;
            _signOutCommandHandler = signOutCommandHandler;
        }

        #endregion

        // POST api/authentication
        [HttpPost("sign-in")]
        public async Task<ActionResult> SignIn([FromBody] SignInRequest request)
        {
            var response = await _signInCommandHandler.HandleAsync(new SignInCommand()
            {
                Email = request.Email,
                Password = request.Password,
            }, CancellationToken.None);

            return Ok(response);
        }

        // POST api/authentication
        [HttpGet("sign-out")]
        [Authorize]
        public async Task<ActionResult> SignOut([FromHeader] string authorization)
        {
            var response = await _signOutCommandHandler.HandleAsync(new SignOutCommand()
            {
                Token = authorization,
            }, CancellationToken.None);

            return Ok(response);
        }
    }
}