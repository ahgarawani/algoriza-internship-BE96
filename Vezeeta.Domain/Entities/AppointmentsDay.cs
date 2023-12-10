using Vezeeta.Domain.Enums;

namespace Vezeeta.Domain.Entities
{
    public class AppointmentsDay
    {
        public int Id { get; set; }
        public Day Day { get; set; }
        public Doctor Doctor { get; set; }
        public int DoctorId { get; set; }
        public List<AppointmentsHour> ApointmentsHours { get; set; }
    }
}
