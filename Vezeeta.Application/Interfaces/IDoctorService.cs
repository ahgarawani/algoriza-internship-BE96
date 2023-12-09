using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Application.Mappings.DTOs;
using Vezeeta.Domain.Entities;

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
