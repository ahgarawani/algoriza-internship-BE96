using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Domain.Enums;

namespace Vezeeta.Application.Mappings.DTOs
{
    public class DoctorEditRequestDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public IFormFile? Image { get; set; }
        public string? PhoneNumber { get; set; }
        public int SpecializationId { get; set; }
        public float VisitPrice { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }

    }
}
