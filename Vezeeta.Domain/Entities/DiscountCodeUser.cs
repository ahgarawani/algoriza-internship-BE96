﻿namespace Vezeeta.Domain.Entities
{
    public class DiscountCodeUser
    {
        public int DiscountCodeId { get; set; }
        public int UserId { get; set; }
        public DiscountCode DiscountCode { get; set; } = null!;
        public User User { get; set; } = null!;
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
    }
}
