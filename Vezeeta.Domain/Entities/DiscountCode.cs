using System.ComponentModel.DataAnnotations;
using Vezeeta.Domain.Enums;

namespace Vezeeta.Domain.Entities
{
    public class DiscountCode
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Code { get; set; }
        public float Value { get; set; }
        public DiscountType Type { get; set; }
        public int RemainingUsage { get; set; } = 50;
        public bool IsActive { get; set; } = true;
        public List<User> Users { get; } = new();
        public List<DiscountCodeUser> DiscountCodeUsers { get; } = new();
    }
}
