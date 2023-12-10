using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Vezeeta.Application.Mappings.DTOs
{
    public class DoctorRegisterRequest : RegisterRequest
    {
        [Required]
        public override IFormFile Image { get; set; }
        [Range(1, 256)]
        public int SpecializationId { get; set; }
        public float VisitPrice { get; set; }
    }
}
