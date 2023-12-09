using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Application.Mappings.DTOs
{
    public class DoctorRegisterRequestDTO: RegisterRequestDTO
    {
        [Required]
        public override IFormFile Image { get; set; }
        [Range(1, 256)]
        public int SpecializationId { get; set; }
        public float VisitPrice { get; set; }
    }
}
