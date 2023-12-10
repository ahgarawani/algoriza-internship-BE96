using Vezeeta.Domain.Enums;

namespace Vezeeta.Domain.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int AppointmentsHourId { get; set; }
        public AppointmentsHour AppointmentsHour { get; set; }
        public Status Status { get; set; } = Status.Pending;
        public DiscountCodeUser? DiscountCodeUser { get; set; }
        public float FinalPrice { get; set; }
    }
}
