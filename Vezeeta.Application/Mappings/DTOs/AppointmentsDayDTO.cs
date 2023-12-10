using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Domain.Enums;

namespace Vezeeta.Application.Mappings.DTOs
{
    public class AppointmentsDayDTO
    {
        public Day Day { get; set; }
        public List<AppointmentsHourDTO> Hours { get; set; }
    }
}
