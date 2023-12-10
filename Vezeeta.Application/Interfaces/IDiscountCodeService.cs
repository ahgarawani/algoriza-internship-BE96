using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Application.Mappings.DTOs;

namespace Vezeeta.Application.Interfaces
{
    public interface IDiscountCodeService
    {
        Task<GenericResponse> AddCodeAsync(DiscountCodeRequest discountCodeRequest);
        Task<GenericResponse> UpdateCodeAsync(DiscountCodeRequest discountCodeRequest);
        Task<GenericResponse> DeleteCodeAsync(int codeId);
        Task<GenericResponse> DeactivateCodeAsync(int codeId);
    }
}
