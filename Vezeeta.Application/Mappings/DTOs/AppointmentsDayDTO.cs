using Vezeeta.Domain.Enums;

namespace Vezeeta.Application.Mappings.DTOs
{
    public class AppointmentsDayDTO
    {
        public Day Day { get; set; }
        public List<AppointmentsHourDTO> Hours { get; set; }
    }
}
