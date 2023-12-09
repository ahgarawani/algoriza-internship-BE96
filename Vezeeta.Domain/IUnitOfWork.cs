using Vezeeta.Domain.Repositories;

namespace Vezeeta.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IPatientRepository Patients { get; }
        IDoctorRepository Doctors { get; }
        ISpecializationRepository Specializations { get; }
        int Complete();
    }
}
