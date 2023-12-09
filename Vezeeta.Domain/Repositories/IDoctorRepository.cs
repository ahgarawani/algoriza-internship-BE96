using Vezeeta.Domain.Entities;

namespace Vezeeta.Domain.Repositories
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAllAsync();
        Task<Doctor> GetByIdAsync(int Id);
        Task<(bool Succeeded, string errorsMessage)> AddAsync(Doctor Doctor, string password);
        void Edit(Doctor Doctor);
        Task DeleteAsync(int Id);
        Task<int> CountAsync();
    }
}
