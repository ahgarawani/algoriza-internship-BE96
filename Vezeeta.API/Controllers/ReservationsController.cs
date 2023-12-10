using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Application.Interfaces;
using Vezeeta.Application.Mappings.DTOs;

namespace Vezeeta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IJwtParserService _jwtParserService;

        public ReservationsController(IReservationService reservationService, IJwtParserService jwtParserService)
        {
            _reservationService = reservationService;
            _jwtParserService = jwtParserService;
        }

        [HttpGet("")]
        [Authorize(Roles = "Doctor, Patient")]
        public async Task<IActionResult> GetAll([FromQuery] UserPaginatedSearchQuery queries)
        {
            //Parsing JWT.
            var authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
            var token = authorizationHeader.Substring("Bearer ".Length);
            var jwtClaims = _jwtParserService.ParseJwt(token);

            if (jwtClaims.UserRole == "Patient")
            {
                return Ok(await _reservationService.GetAllByPatientAsync(jwtClaims.UserId));

            }
            else
            {
                return Ok(await _reservationService.GetAllByDoctorAsync(jwtClaims.UserId, queries));
            }
        }

        [HttpPost("")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> RequestReservation([FromBody] ReservationRequest reservationRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Parsing JWT.
            var authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
            var token = authorizationHeader.Substring("Bearer ".Length);
            var jwtClaims = _jwtParserService.ParseJwt(token);

            var result = await _reservationService.AddAsync(jwtClaims.UserId, reservationRequest);
            if (!result.Succeeded)
                return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpGet("cancel/{id:int}")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> CancelReservation([FromRoute] int Id)
        {
            //Parsing JWT.
            var authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
            var token = authorizationHeader.Substring("Bearer ".Length);
            var jwtClaims = _jwtParserService.ParseJwt(token);

            var result = await _reservationService.PatientCancellation(jwtClaims.UserId, Id);

            if (result.Status == 403)
                return Forbid();
            if (result.Status == 400)
                return BadRequest(result.Message);
            return Ok(new GenericResponse { Succeeded = true, Message = result.Message });
        }

        [HttpGet("confirm/{id:int}")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> ConfirmReservation([FromRoute] int Id)
        {
            //Parsing JWT.
            var authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
            var token = authorizationHeader.Substring("Bearer ".Length);
            var jwtClaims = _jwtParserService.ParseJwt(token);

            var result = await _reservationService.DoctorConfirmation(jwtClaims.UserId, Id);

            if (result.Status == 403)
                return Forbid();
            if (result.Status == 400)
                return BadRequest(result.Message);
            return Ok(new GenericResponse { Succeeded = true, Message = result.Message });
        }

        [HttpGet("NumOfReservations")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> ReservationsCount()
        {
            return Ok(await _reservationService.CountAsync());
        }
    }
}
