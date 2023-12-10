using Vezeeta.Domain.Enums;

namespace Vezeeta.Domain.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int AppointmentsHourId { get; set; }
        public AppointmentsHour AppointmentsHour { get; set; }
        public Status Status { get; set; }
        public DiscountCodeUser? DiscountCodeUser { get; set; }
        public float FinalPrice
        {
            get
            {
                return this.DiscountCodeUser != null ?
                            (this.DiscountCodeUser.DiscountCode.Type == 0 ?
                                this.AppointmentsHour.AppointmentsDay.Doctor.VisitPrice * this.DiscountCodeUser.DiscountCode.Value :
                                this.AppointmentsHour.AppointmentsDay.Doctor.VisitPrice - this.DiscountCodeUser.DiscountCode.Value) :
                            this.AppointmentsHour.AppointmentsDay.Doctor.VisitPrice;
            }
        }

    }
}
