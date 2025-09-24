using Microsoft.AspNetCore.Mvc;

namespace DansTCG.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
    // Trigger sweep test: backend change for CI/CD validation.
        [HttpGet]
        public IActionResult Get() => Ok(new { status = "Healthy" });
    }
}
