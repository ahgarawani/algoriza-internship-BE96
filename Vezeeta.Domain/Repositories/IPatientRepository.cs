using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Domain.Entities;

namespace Vezeeta.Domain.Repositories
{
    public interface IPatientRepository: IBaseRepository<User>
    {
        Task<(bool Succeeded, string errorsMessage)> AddAsync(User user, string password);
        Task<int> CountAsync();
    }
}
