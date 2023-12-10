using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Application.Interfaces;
using Vezeeta.Application.Mappings.DTOs;

namespace Vezeeta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentsService _appointmentsService;
        private readonly IJwtParserService _jwtParserService;

        public AppointmentsController(IAppointmentsService appointmentsService, IJwtParserService jwtParserService)
        {
            _appointmentsService = appointmentsService;
            _jwtParserService = jwtParserService;
        }

        [HttpPost("")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> AddAppointments([FromBody] AppointmentsRequest appointmentsRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Parsing JWT.
            var authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
            var token = authorizationHeader.Substring("Bearer ".Length);
            var jwtClaims = _jwtParserService.ParseJwt(token);

            var result = await _appointmentsService.AddAppointmentsAsync(jwtClaims.UserId, appointmentsRequest);
            if (!result.Succeeded)
                return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpPatch("{id:int}")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> UpdateAppointment([FromRoute] int Id, [FromBody] AppointmentsHourDTO appointmentsHour)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Parsing JWT.
            var authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
            var token = authorizationHeader.Substring("Bearer ".Length);
            var jwtClaims = _jwtParserService.ParseJwt(token);

            var result = await _appointmentsService.UpdateAppointmentAsync(jwtClaims.UserId, Id, appointmentsHour);

            if (result.Status == 204)
                return NoContent();
            else if (result.Status == 403)
                return Forbid();
            else if (result.Status == 400)
                return BadRequest(result.Message);
            else
                return Ok(new { Succeeded = true, Message = result.Message });
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> UpdateAppointment([FromRoute] int Id)
        {

            //Parsing JWT.
            var authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
            var token = authorizationHeader.Substring("Bearer ".Length);
            var jwtClaims = _jwtParserService.ParseJwt(token);

            var result = await _appointmentsService.DeleteAppointmentAsync(jwtClaims.UserId, Id);

            if (result.Status == 204)
                return NoContent();
            else if (result.Status == 403)
                return Forbid();
            else if (result.Status == 400)
                return BadRequest(result.Message);
            else
                return Ok(new { Succeeded = true, Message = result.Message });
        }
    }
}
