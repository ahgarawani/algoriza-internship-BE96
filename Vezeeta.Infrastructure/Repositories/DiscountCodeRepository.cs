using Microsoft.EntityFrameworkCore;
using Vezeeta.Domain.Entities;
using Vezeeta.Domain.Repositories;

namespace Vezeeta.Infrastructure.Repositories
{
    public class DiscountCodeRepository : IDiscountCodeRepository
    {
        private readonly ApplicationDbContext _context;

        public DiscountCodeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddCodeAsync(DiscountCode discountCode)
        {
            await _context.DiscountCodes.AddAsync(discountCode);
        }

        public void UpdateCode(DiscountCode discountCode)
        {
            _context.DiscountCodes.Update(discountCode);
        }

        public void DeactivateCode(DiscountCode discountCode)
        {
            _context.DiscountCodes.Remove(discountCode);
        }

        public void Deactivate(DiscountCode discountCode)
        {
            discountCode.IsActive = false;
            _context.DiscountCodes.Update(discountCode);
        }

        public async Task<DiscountCode> GetByIdAsync(int Id)
        {
            return await _context.DiscountCodes.Include(dc => dc.DiscountCodeUsers).ThenInclude(dcu => dcu.Reservation).FirstOrDefaultAsync(c => c.Id == Id);
        }

        public async Task<DiscountCode> GetByCodeAsync(string Code)
        {
            return await _context.DiscountCodes.FirstOrDefaultAsync(c => c.Code == Code);
        }
    }
}
