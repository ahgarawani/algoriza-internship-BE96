using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Application.Interfaces;
using Vezeeta.Application.Mappings.DTOs;

namespace Vezeeta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class DiscountCodesController : ControllerBase
    {
        private readonly IDiscountCodeService _discountCodeService;

        public DiscountCodesController(IDiscountCodeService discountCodeService)
        {
            _discountCodeService = discountCodeService;
        }

        [HttpPost("")]
        public async Task<IActionResult> AddCode([FromBody] DiscountCodeRequest discountCodeRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _discountCodeService.AddCodeAsync(discountCodeRequest);
            if (!result.Succeeded)
                return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateCode([FromBody] DiscountCodeRequest discountCodeRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _discountCodeService.UpdateCodeAsync(discountCodeRequest);
            if (!result.Succeeded)
                return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCode([FromRoute] int Id)
        {
            var result = await _discountCodeService.DeleteCodeAsync(Id);
            if (!result.Succeeded)
                return NoContent();
            return Ok(result);
        }

        [HttpGet("deactivate/{id:int}")]
        public async Task<IActionResult> DeactivateCode([FromRoute] int Id)
        {
            var result = await _discountCodeService.DeactivateCodeAsync(Id);
            if (!result.Succeeded)
                return NoContent();
            return Ok(result);
        }
    }
}
