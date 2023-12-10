using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Application.Mappings.DTOs
{
    public class AppointmentsRequest
    {
        public List<AppointmentsDayDTO> Days { get; set; }
    }
}
