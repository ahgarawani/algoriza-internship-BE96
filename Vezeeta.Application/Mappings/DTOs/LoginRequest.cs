using System.ComponentModel.DataAnnotations;

namespace Vezeeta.Application.Mappings.DTOs
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
