using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Application.Mappings.DTOs
{
    public class DoctorResponseDTO
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Image { get; set; }
        public string SpeciallizationEn { get; set; }
        public string SpeciallizationAr { get; set; }
    }
}
