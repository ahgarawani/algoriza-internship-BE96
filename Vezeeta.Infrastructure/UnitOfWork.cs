using Microsoft.AspNetCore.Identity;
using Vezeeta.Domain;
using Vezeeta.Domain.Entities;
using Vezeeta.Domain.Repositories;
using Vezeeta.Infrastructure.Repositories;

namespace Vezeeta.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly ApplicationDbContext _context;
        public readonly UserManager<User> _userManager;

        public IUserRepository Users { get; private set; }
        public IPatientRepository Patients { get; private set; }
        public IDoctorRepository Doctors { get; private set; }
        public ISpecializationRepository Specializations { get; private set; }

        public UnitOfWork(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;

            Users = new UserRepository(_userManager);
            Patients = new PatientRepository(_userManager, Users);
            Doctors = new DoctorRepository(_userManager, Users, _context);
            Specializations = new SpecializationRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
