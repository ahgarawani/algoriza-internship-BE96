using Microsoft.AspNetCore.Identity;
using Vezeeta.Domain.Entities;
using Vezeeta.Domain.Repositories;

namespace Vezeeta.Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;

        public PatientRepository(UserManager<User> userManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userManager.GetUsersInRoleAsync("Patient");
        }

        public async Task<User> GetByIdAsync(int Id)
        {
            var allPatients = await GetAllAsync();
            Dictionary<int, User> patientsDictionary = allPatients.ToDictionary(user => user.Id, user => user);
            patientsDictionary.TryGetValue(Id, out var user);
            return user;
        }

        public async Task<int> CountAsync()
        {
            return (await GetAllAsync()).Count();
        }

        public async Task<(bool Succeeded, string errorsMessage)> AddAsync(User user, string password)
        {
            var result = await _userRepository.CreateAsync(user, password);
            if (!result.Succeeded) return result;
            await _userManager.AddToRoleAsync(user, "Patient");
            return result;
        }
    }
}
