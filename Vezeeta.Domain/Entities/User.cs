using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vezeeta.Domain.Enums;

namespace Vezeeta.Domain.Entities
{
    public class User : IdentityUser<int>
    {
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        [Column(TypeName = "Date")]
        public DateTime BirthDate { get; set; }
        public string? ImagePath { get; set; }
        public Doctor? Doctor { get; set; }
        public List<DiscountCode> DiscountCodes { get; } = new();
        public List<DiscountCodeUser> DiscountCodeUsers { get; } = new();
    }
}

