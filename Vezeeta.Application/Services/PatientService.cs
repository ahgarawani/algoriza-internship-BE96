using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Application.Interfaces;
using Vezeeta.Application.Mappings.DTOs;
using Vezeeta.Domain;
using Vezeeta.Domain.Entities;


namespace Vezeeta.Application.Services
{
    public class PatientService : IPatientService
    {
        readonly private IUnitOfWork _unitOfWork;
        readonly private IMapper _mapper;

        public PatientService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PatientResponseDTO>> GetAllAsync(UserPaginatedSearchQueryDTO queries)
        {
            var patients = await _unitOfWork.Patients.GetAllAsync();
            var properties = typeof(User).GetProperties();
            var paginated = patients
                .Where(entity =>
                        properties.Any(p =>
                            p.GetValue(entity)?.ToString()?.IndexOf(queries.Search, StringComparison.OrdinalIgnoreCase) >= 0
                        ))
                .Skip((queries.Page - 1) * queries.PageSize)
                .Take(queries.PageSize)
                .ToList();
            return _mapper.Map<List<PatientResponseDTO>>(paginated);
        }

        public async Task<PatientResponseDTO> GetByIdAsync(int id)
        {
            var patient = await _unitOfWork.Patients.GetByIdAsync(id);
            return _mapper.Map<PatientResponseDTO>(patient);
        }
    }
}
