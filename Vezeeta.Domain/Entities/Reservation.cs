using Vezeeta.Domain.Enums;

namespace Vezeeta.Domain.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ClinicDayHourId { get; set; }
        public ClinicDayHour DayHour { get; set; }
        public Status Status { get; set; }
        public DiscountCodeUser? DiscountCodeUser { get; set; }
        public float FinalPrice
        {
            get
            {
                return this.DiscountCodeUser != null ?
                            (this.DiscountCodeUser.DiscountCode.Type == 0 ?
                                this.DayHour.WeekDay.Doctor.VisitPrice * this.DiscountCodeUser.DiscountCode.Value :
                                this.DayHour.WeekDay.Doctor.VisitPrice - this.DiscountCodeUser.DiscountCode.Value) :
                            this.DayHour.WeekDay.Doctor.VisitPrice;
            }
        }

    }
}
