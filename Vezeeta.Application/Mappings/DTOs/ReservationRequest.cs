using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Application.Mappings.DTOs
{
    public class ReservationRequest
    {
        public int appointmentHourId { get; set; }
        public string DiscountCode { get; set; }
    }
}
