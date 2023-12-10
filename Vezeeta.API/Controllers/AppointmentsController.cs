using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        public AppointmentsController(IAppointmentsService appointmentsService)
        {
            _appointmentsService = appointmentsService;
        }

        [HttpPost("")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> AddAppointments([FromBody] AppointmentsRequest appointmentsRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Retrieving JWT from the Authorization Header.
            var authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
            var token = authorizationHeader.Substring("Bearer ".Length);

            var result = await _appointmentsService.AddAppointmentsAsync(token, appointmentsRequest);
            if (!result.Succeeded)
                return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpPatch("{id:int}")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> UpdateAppointment(int Id, [FromBody] AppointmentsHourDTO appointmentsHour)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Retrieving JWT from the Authorization Header.
            var authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
            var token = authorizationHeader.Substring("Bearer ".Length);

            var result = await _appointmentsService.UpdateAppointmentAsync(token, Id, appointmentsHour);

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
        public async Task<IActionResult> UpdateAppointment(int Id)
        {

            //Retrieving JWT from the Authorization Header.
            var authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
            var token = authorizationHeader.Substring("Bearer ".Length);

            var result = await _appointmentsService.DeleteAppointmentAsync(token, Id);

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
