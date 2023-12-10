using Vezeeta.Application.Mappings.DTOs;

namespace Vezeeta.Application.Interfaces
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorResponse>> GetAllAsync(UserPaginatedSearchQuery queries);
        Task<DoctorResponse> GetByIdAsync(int id);
        Task<(bool Succeeded, string Message)> AddAsync(DoctorRegisterRequest doctorRegisterRequest);
        Task<bool> DeleteAsync(int Id);
        Task<(bool Succeeded, string Message)> EditAsync(int Id, DoctorEditRequest doctorEditRequest);
        Task<(bool Succeeded, string Message)> ChangeVisitPrice(int Id, float newPrice);
    }
}
