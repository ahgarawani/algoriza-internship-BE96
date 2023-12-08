using Microsoft.AspNetCore.Identity;
using Vezeeta.Domain.Entities;
using Vezeeta.Domain.Repositories;

namespace Vezeeta.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<(bool Succeeded, string errorsMessage)> CreateAsync(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return (result.Succeeded, result.ToString());
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<User> MatchEmailAndPasswordAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, password)) return null;

            return user;
        }
    }
}
