using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vezeeta.Domain.Entities;
using Vezeeta.Domain.Repositories;

namespace Vezeeta.Infrastructure.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly ApplicationDbContext _context;

        public DoctorRepository(UserManager<User> userManager, IUserRepository userRepository, ApplicationDbContext context)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _context = context;
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            return await _context.Doctors.Include(doctor => doctor.User).Include(doctor => doctor.Specialization).ToListAsync();
        }

        public async Task<Doctor> GetByIdAsync(int Id)
        {
            return await _context.Doctors.Include(doctor => doctor.User).Include(doctor => doctor.Specialization).FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<(bool Succeeded, string errorsMessage)> AddAsync(Doctor doctor, string password)
        {
            var result = await _userRepository.CreateAsync(doctor.User, password);
            if (!result.Succeeded) return result;
            await _userManager.AddToRoleAsync(doctor.User, "Doctor");
            await _context.Doctors.AddAsync(doctor);
            return result;
        }

        public async Task<int> CountAsync()
        {
            return await _context.Doctors.CountAsync();
        }

        public async Task DeleteAsync(int Id)
        {
            var doctorUser = _userManager.Users.Where(u => u.Doctor.Id == Id).First();

            await _userManager.DeleteAsync(doctorUser);
        }

        public void Edit(Doctor doctor)
        {
            _context.Doctors.Update(doctor);

        }
        public async Task<Doctor> GetByUserIdAsync(int Id)
        {
            return await _context.Doctors.FirstOrDefaultAsync(x => x.UserId == Id);
        }
    }
}
