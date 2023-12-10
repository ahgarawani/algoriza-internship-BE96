using Vezeeta.Domain.Entities;

namespace Vezeeta.Domain.Repositories
{
    public interface IDiscountCodeRepository
    {
        Task AddCodeAsync(DiscountCode discountCode);
        void UpdateCode(DiscountCode discountCode);
        void DeactivateCode(DiscountCode discountCode);
        void Deactivate(DiscountCode discountCode);
        Task<DiscountCode> GetByIdAsync(int Id);
        Task<DiscountCode> GetByCodeAsync(string Code);
    }
}
