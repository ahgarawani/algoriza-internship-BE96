using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Vezeeta.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class TestController : ControllerBase
    {
        [HttpGet("patient")]
        [Authorize]
        public IActionResult IsAuthenticated()
        {
            return Ok("You are authenticated");
        }

        [HttpGet("doctor")]
        [Authorize(Roles = "Doctor")]
        public IActionResult IsDoctor()
        {
            return Ok("You are a doctor");
        }

        [HttpGet("admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult IsAdmin()
        {
            return Ok("You are an admin");
        }

    }
}