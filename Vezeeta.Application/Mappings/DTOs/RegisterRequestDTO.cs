using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Vezeeta.Domain.Enums;

namespace Vezeeta.Application.Mappings.DTOs
{
    public class RegisterRequestDTO
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public IFormFile? Image { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public Gender Gender { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
