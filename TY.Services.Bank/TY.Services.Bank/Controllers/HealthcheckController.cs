using Microsoft.AspNetCore.Mvc;

namespace TY.Services.Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthcheckController : ControllerBase
    {
        // POST api/healthcheck
        public ActionResult Get()
        {
            return Ok(new { Message = "App is up & run" });
        }
    }
}