using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Vezeeta.Application.Interfaces;
using Vezeeta.Application.Mappings.DTOs;
using Vezeeta.Application.Services;

namespace Vezeeta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        //[HttpPost("")]
        //[Authorize(Roles = "Patient")]
        //public async Task<IActionResult> RequestReservation([FromBody] ReservationRequest reservationRequest)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    //Retrieving JWT from the Authorization Header.
        //    var authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
        //    var token = authorizationHeader.Substring("Bearer ".Length);

        //    var result = await _reservationService.AddRservationAsync(token, reservationRequest);
        //    if (!result.Succeeded)
        //        return BadRequest(result.Message);
        //    return Ok(result);
        //}
    }
}
