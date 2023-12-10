using Vezeeta.Application.Mappings.DTOs;

namespace Vezeeta.Application.Interfaces
{
    public interface IAppointmentsService
    {
        Task<GenericResponse> AddAppointmentsAsync(int userId, AppointmentsRequest appointmentsRequest);
        Task<(int Status, string Message)> UpdateAppointmentAsync(int userId, int appointmentId, AppointmentsHourDTO appointmentsHour);
        Task<(int Status, string Message)> DeleteAppointmentAsync(int userId, int appointmentId);
    }

}
