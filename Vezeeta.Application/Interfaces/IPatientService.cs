using Vezeeta.Application.Mappings.DTOs;

namespace Vezeeta.Application.Interfaces
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientResponse>> GetAllAsync(UserPaginatedSearchQuery queries);
        Task<PatientResponse> GetByIdAsync(int id);
    }
}
