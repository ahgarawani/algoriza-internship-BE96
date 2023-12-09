﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Vezeeta.Application.Interfaces;
using Vezeeta.Application.Mappings.DTOs;
using Vezeeta.Application.Services;
using Vezeeta.Domain;

namespace Vezeeta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDoctorService _doctorService;

        public DoctorsController(IUnitOfWork unitOfWork, IDoctorService doctorService)
        {
            _unitOfWork = unitOfWork;
            _doctorService = doctorService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll([FromQuery] UserPaginatedSearchQueryDTO queries)
        {
            var Doctors = await _doctorService.GetAllAsync(queries);
            return Ok(Doctors);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var patient = await _doctorService.GetByIdAsync(id);
            return Ok(patient);
        }
        [HttpPost("")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add([FromForm]DoctorRegisterRequestDTO doctorRegisterRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (doctorRegisterRequest.Image != null)
            {
                string fileExtension = Path.GetExtension(doctorRegisterRequest.Image.FileName).ToLower();
                if (!(fileExtension.Contains("png") || fileExtension.Contains("jpg") || fileExtension.Contains("jpeg")))
                    return BadRequest("Invalid Image Format! Image must be PNG or JPEG!");
            }

            var result = await _doctorService.AddAsync(doctorRegisterRequest);

            if (!result.Succeeded)
                return BadRequest(result.Message);

            return Ok(new { Succeeded = result.Succeeded, Message = result.Message });
        }

        [HttpGet("NumOfDoctors")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> NumberOfDoctors()
        {
            return Ok(await _unitOfWork.Doctors.CountAsync());
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            
            if (await _doctorService.DeleteAsync(id)) return Ok(new { Succeeded= true });
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [FromForm] DoctorEditRequestDTO doctorEditRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _doctorService.EditAsync(id, doctorEditRequest);
            if (result.Succeeded) return Ok(new { Succeeded = true });
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [Authorize(Roles = "Admin, Doctor")]
        public async Task<IActionResult> Edit(int id, [FromBody] ChangeDoctorVisitPriceDTO changeDoctorVisitPriceDTO)
        {
            var result = await _doctorService.ChangeVisitPrice(id, changeDoctorVisitPriceDTO.price);
            if (result.Succeeded) return Ok(new { Succeeded = true });
            return NoContent();
        }
    }
}