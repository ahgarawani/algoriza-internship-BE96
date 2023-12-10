using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Application.Mappings.DTOs;

namespace Vezeeta.Application.Interfaces
{
    public interface IAppointmentsService
    {
        Task<(bool Succeeded, string Message)> AddAppointmentsAsync(string jwtToken, AppointmentsRequest appointmentsRequest);
        Task<(int Status, string Message)> UpdateAppointmentAsync(string jwtToken, int appointmentId, AppointmentsHourDTO appointmentsHour);
        Task<(int Status, string Message)> DeleteAppointmentAsync(string jwtToken, int appointmentId);
    }

}
