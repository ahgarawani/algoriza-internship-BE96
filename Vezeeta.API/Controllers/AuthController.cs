using Microsoft.AspNetCore.Mvc;
using Vezeeta.Application.Interfaces;
using Vezeeta.Application.Mappings.DTOs;
using Vezeeta.Infrastructure.Identity;

namespace Vezeeta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthManager authManager, IAuthenticationService authenticationService)
        {
            _authManager = authManager;
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> SignUp([FromForm] RegisterRequestDTO registerRequest)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var result = await _authenticationService.RegisterAsync(registerRequest);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> GetTokenAsync([FromBody] LoginRequestDTO loginRequest)
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
