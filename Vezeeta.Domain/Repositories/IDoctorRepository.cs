using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Domain.Entities;

namespace Vezeeta.Domain.Repositories
{
    public interface IDoctorRepository: IBaseRepository<User>
    {
        Task<int> CountAsync();
    }
}
