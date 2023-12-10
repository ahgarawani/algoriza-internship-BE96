using AutoMapper;
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

        public async Task<IEnumerable<PatientResponse>> GetAllAsync(UserPaginatedSearchQuery queries)
        {
            var patients = await _unitOfWork.Patients.GetAllAsync();
            var PatientProperties = typeof(User).GetProperties();
            var filteredPatients = patients
                .Where(patient =>
                    patient.FirstName.IndexOf(queries.Search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    patient.LastName.IndexOf(queries.Search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    patient.Gender.ToString().ToLower().Contains(queries.Search.ToLower()) ||
                    patient.Email.IndexOf(queries.Search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    patient.PhoneNumber.IndexOf(queries.Search, StringComparison.OrdinalIgnoreCase) >= 0)
                .Skip((queries.Page - 1) * queries.PageSize)
                .Take(queries.PageSize)
                .ToList();
            return _mapper.Map<List<PatientResponse>>(filteredPatients);
        }

        public async Task<PatientResponse> GetByIdAsync(int id)
        {
            var patient = await _unitOfWork.Patients.GetByIdAsync(id);
            return _mapper.Map<PatientResponse>(patient);
        }
    }
}
