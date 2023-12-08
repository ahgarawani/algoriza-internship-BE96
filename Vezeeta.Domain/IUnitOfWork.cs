using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Domain.Repositories;

namespace Vezeeta.Domain
{
    public interface IUnitOfWork: IDisposable
    {
        IUserRepository Users { get; }
        IPatientRepository Patients { get; }

        int Complete();
    }
}
