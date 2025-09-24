using Microsoft.AspNetCore.Mvc;

namespace DansTCG.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        // Trivial comment change to trigger CI/CD deploy workflow.
        [HttpGet]
        public IActionResult Get() => Ok(new { status = "Healthy" });
    }
}
