using Vezeeta.Domain.Entities;

namespace Vezeeta.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<(bool Succeeded, string errorsMessage)> CreateAsync(User user, string password);
        Task<User> FindByEmailAsync(string email);
        Task<User> MatchEmailAndPasswordAsync(string email, string password);
    }
}
