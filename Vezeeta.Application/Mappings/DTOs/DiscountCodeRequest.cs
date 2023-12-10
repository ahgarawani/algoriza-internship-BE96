using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Domain.Enums;

namespace Vezeeta.Application.Mappings.DTOs
{
    public class DiscountCodeRequest
    {
        public string Code { get; set; }
        public int RemainingUsage { get; set; }
        public DiscountType Type { get; set; }
        public float Value { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
