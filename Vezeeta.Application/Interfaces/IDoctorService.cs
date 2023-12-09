using Vezeeta.Application.Mappings.DTOs;

namespace Vezeeta.Application.Interfaces
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorResponseDTO>> GetAllAsync(UserPaginatedSearchQueryDTO queries);
        Task<DoctorResponseDTO> GetByIdAsync(int id);
        Task<(bool Succeeded, string Message)> AddAsync(DoctorRegisterRequestDTO doctorRegisterRequest);
        Task<bool> DeleteAsync(int Id);
        Task<(bool Succeeded, string Message)> EditAsync(int Id, DoctorEditRequestDTO doctorEditRequest);
        Task<(bool Succeeded, string Message)> ChangeVisitPrice(int Id, float newPrice);
    }
}
