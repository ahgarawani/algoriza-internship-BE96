using Vezeeta.Domain.Entities;

namespace Vezeeta.Domain.Repositories
{
    public interface IDoctorRepository
    {
        Task<User> GetByIdAsync(int Id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<int> CountAsync();
    }
}
