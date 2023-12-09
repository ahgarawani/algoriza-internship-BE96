using System.ComponentModel.DataAnnotations;

namespace Vezeeta.Domain.Entities
{
    public class AppointmentsHour
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Hour { get; set; }
        public int AppointmentsDayId { get; set; }
        public AppointmentsDay AppointmentsDay { get; set; }
        public List<Reservation> Reservations { get; } = new();
    }
}
