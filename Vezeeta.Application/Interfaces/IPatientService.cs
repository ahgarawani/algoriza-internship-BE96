using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Application.Mappings.DTOs;
using Vezeeta.Domain.Entities;

namespace Vezeeta.Application.Interfaces
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientResponseDTO>> GetAllAsync(UserPaginatedSearchQueryDTO queries);
        Task<PatientResponseDTO> GetByIdAsync(int id);
    }
}
