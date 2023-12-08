using Vezeeta.Domain.Entities;

namespace Vezeeta.Domain.Repositories
{
    public interface IDoctorRepository : IBaseRepository<User>
    {
        Task<int> CountAsync();
    }
}
