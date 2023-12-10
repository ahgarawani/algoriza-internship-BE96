using Microsoft.EntityFrameworkCore;
using Vezeeta.Domain.Entities;
using Vezeeta.Domain.Repositories;

namespace Vezeeta.Infrastructure.Repositories
{
    public class AppointmentsRepository: IAppointmentsRepository
    {
        private readonly ApplicationDbContext _context;

        public AppointmentsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAppointmentsAsync(Doctor doctor, List<AppointmentsDay> appointmentsDays)
        {
            doctor.AppointmentsDays = appointmentsDays;
            _context.Doctors.Update(doctor);
        }

        public async Task<AppointmentsHour> GetAppointmentsHourByIdAsync(int Id)
        {
            return await _context.AppointmentsHours.Include(ah => ah.AppointmentsDay).FirstOrDefaultAsync(ah => ah.Id == Id);
        }

        public void UpdateHour(AppointmentsHour hour)
        {
            _context.AppointmentsHours.Update(hour);
        }
        public void DeleteHour(AppointmentsHour hour)
        {
            _context.AppointmentsHours.Remove(hour);
        }

    }
}
