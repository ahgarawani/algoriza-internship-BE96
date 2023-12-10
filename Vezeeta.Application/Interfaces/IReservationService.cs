using Vezeeta.Application.Mappings.DTOs;

namespace Vezeeta.Application.Interfaces
{
    public interface IReservationService
    {
        Task<GenericResponse> AddAsync(int userId, ReservationRequest reservationRequest);
        Task<IEnumerable<ReservationPatientResponse>> GetAllByPatientAsync(int userId);
        Task<IEnumerable<ReservationDoctorResponse>> GetAllByDoctorAsync(int userId, UserPaginatedSearchQuery queries);
        Task<(int Status, string Message)> PatientCancellation(int userId, int reservationId);
        Task<(int Status, string Message)> DoctorConfirmation(int userId, int reservationId);

        Task<int> CountAsync();
    }
}
