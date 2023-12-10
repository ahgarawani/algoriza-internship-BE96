using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Domain.Entities;
using Vezeeta.Domain.Enums;

namespace Vezeeta.Domain.Repositories
{
    public interface IReservationRepository
    {
        Task AddAsync(Reservation reservation);
        void ChangeStatus(int reservationId, Status status);
        Task<IEnumerable<Reservation>> GetAllByPatientAsync(int patientId);
        Task<IEnumerable<Reservation>> GetAllByDoctorAsync(int doctorId);
        Task<int> CountAsync();
        Task<bool> AuthenticatePatientAsync(int reservationId, int patientId);
        Task<bool> AuthenticateDoctorAsync(int reservationId, int doctorId);
    }
}
