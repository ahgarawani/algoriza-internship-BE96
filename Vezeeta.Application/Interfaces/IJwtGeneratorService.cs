using Vezeeta.Domain.Entities;

namespace Vezeeta.Application.Interfaces
{
    public interface IJwtGeneratorService
    {
        Task<(string Token, DateTime ExpiresOn)> CreateJwt(User user);
    }
}
