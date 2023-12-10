using Microsoft.AspNetCore.Mvc;
using Vezeeta.Application.Interfaces;
using Vezeeta.Application.Mappings.DTOs;

namespace Vezeeta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {

            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> SignUp([FromForm] RegisterRequest registerRequest)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (registerRequest.Image != null)
            {
                string fileExtension = Path.GetExtension(registerRequest.Image.FileName).ToLower();
                if (!(fileExtension.Contains("png") || fileExtension.Contains("jpg") || fileExtension.Contains("jpeg")))
                    return BadRequest("Invalid Image Format! Image must be PNG or JPEG!");
            }

            var result = await _authenticationService.RegisterAsync(registerRequest);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> GetTokenAsync([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authenticationService.LoginAsync(loginRequest);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
