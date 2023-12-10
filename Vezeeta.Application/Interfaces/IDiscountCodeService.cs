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
