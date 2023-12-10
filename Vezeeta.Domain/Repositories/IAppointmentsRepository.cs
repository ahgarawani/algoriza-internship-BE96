using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Domain.Entities;

namespace Vezeeta.Domain.Repositories
{
    public interface IAppointmentsRepository
    {
        Task AddAppointmentsAsync(Doctor doctor, List<AppointmentsDay> appointmentsDays);
        Task<AppointmentsHour> GetAppointmentsHourByIdAsync(int Id);
        void UpdateHour(AppointmentsHour hour);
        void DeleteHour(AppointmentsHour hour);
    }
}
