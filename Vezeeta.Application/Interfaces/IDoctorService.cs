using Vezeeta.Application.Mappings.DTOs;

namespace Vezeeta.Application.Interfaces
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorResponse>> GetAllAsync(UserPaginatedSearchQuery queries);
        Task<DoctorResponse> GetByIdAsync(int id);
        Task<GenericResponse> AddAsync(DoctorRegisterRequest doctorRegisterRequest);
        Task<bool> DeleteAsync(int Id);
        Task<GenericResponse> EditAsync(int Id, DoctorEditRequest doctorEditRequest);
        Task<GenericResponse> ChangeVisitPrice(int Id, float newPrice);
    }
}
