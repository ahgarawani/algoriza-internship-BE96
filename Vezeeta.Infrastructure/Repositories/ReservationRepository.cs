using Microsoft.EntityFrameworkCore;
using Vezeeta.Domain.Entities;
using Vezeeta.Domain.Enums;
using Vezeeta.Domain.Repositories;

namespace Vezeeta.Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ApplicationDbContext _context;

        public ReservationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Reservation reservation)
        {
            await _context.AddAsync(reservation);
        }

        public async void ChangeStatus(int reservationId, Status status)
        {
            await _context.Reservations
                .Where(res => res.Id == reservationId)
                .ExecuteUpdateAsync(s => s.SetProperty(b => b.Status, status));
        }

        public async Task<int> CountAsync()
        {
            return await _context.Reservations.CountAsync();
        }

        public async Task<IEnumerable<Reservation>> GetAllByDoctorAsync(int doctorId)
        {
            return await _context.Reservations
                .Include(res => res.User)
                .Where(res => res.AppointmentsHour.AppointmentsDay.DoctorId == doctorId).ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetAllByPatientAsync(int patientId)
        {
            return await _context.Reservations
                .Include(res => res.AppointmentsHour.AppointmentsDay.Doctor.User)
                .Include(res => res.AppointmentsHour.AppointmentsDay.Doctor.Specialization)
                .Where(res => res.UserId == patientId).ToListAsync();
        }

        public async Task<bool> AuthenticateDoctorAsync(int reservationId, int doctorId)
        {
            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(res => res.Id == reservationId && res.AppointmentsHour.AppointmentsDay.DoctorId == doctorId);
            if (reservation == null)
                return false;
            return true;
        }

        public async Task<bool> AuthenticatePatientAsync(int reservationId, int patientId)
        {
            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(res => res.Id == reservationId && res.UserId == patientId);
            if (reservation == null)
                return false;
            return true;
        }
    }
}
