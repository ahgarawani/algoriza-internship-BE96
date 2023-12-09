using Vezeeta.Application.Mappings.DTOs;

namespace Vezeeta.Application.Interfaces
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientResponseDTO>> GetAllAsync(UserPaginatedSearchQueryDTO queries);
        Task<PatientResponseDTO> GetByIdAsync(int id);
    }
}
