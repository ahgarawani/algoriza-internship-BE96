using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Application.Interfaces;
using Vezeeta.Application.Mappings.DTOs;
using Vezeeta.Domain;

namespace Vezeeta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPatientService _patientService;

        public PatientsController(IUnitOfWork unitOfWork, IPatientService patientService)
        {
            _unitOfWork = unitOfWork;
            _patientService = patientService;
        }

        [HttpGet("")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll([FromQuery] UserPaginatedSearchQuery queries)
        {
            var patients = await _patientService.GetAllAsync(queries);
            return Ok(patients);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var patient = await _patientService.GetByIdAsync(id);
            return Ok(patient);
        }

        [HttpGet("NumOfPatients")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> NumberOfPatients()
        {
            return Ok(await _unitOfWork.Patients.CountAsync());
        }
    }
}
