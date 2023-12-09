using Vezeeta.Domain.Repositories;

namespace Vezeeta.Infrastructure.Repositories
{
    public class SpecializationRepository : ISpecializationRepository
    {
        readonly private ApplicationDbContext _context;

        public SpecializationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DoesExist(int id)
        {
            var specialization = await _context.Specializations.FindAsync(id);
            return specialization != null;
        }
    }
}
