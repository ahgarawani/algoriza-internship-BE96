using Vezeeta.Domain.Entities;

namespace Vezeeta.Domain.Repositories
{
    public interface IPatientRepository
    {
        Task<User> GetByIdAsync(int Id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<(bool Succeeded, string errorsMessage)> AddAsync(User user, string password);
        Task<int> CountAsync();
    }
}
