using System.ComponentModel.DataAnnotations;

namespace Vezeeta.Domain.Entities
{
    public class Specialization
    {
        public int Id { get; set; }
        [Required]
        public string NameEn { get; set; }
        [Required]
        public string NameAr { get; set; }
        public List<Doctor> Doctors { get; } = new List<Doctor>();
    }
}
