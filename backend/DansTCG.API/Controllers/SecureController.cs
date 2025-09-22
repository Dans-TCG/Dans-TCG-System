using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DansTCG.API.Controllers
{
    [ApiController]
    [Route("api/secure")]
    public class SecureController : ControllerBase
    {
        [HttpGet("ping")]
        [Authorize]
        public IActionResult Ping()
        {
            var name = User.Identity?.Name ?? "unknown";
            var claims = User.Claims.Select(c => new { c.Type, c.Value });
            return Ok(new { message = "pong", name, claims });
        }
    }
}
