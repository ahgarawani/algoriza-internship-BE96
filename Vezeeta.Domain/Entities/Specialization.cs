using System.ComponentModel.DataAnnotations;

namespace Vezeeta.Domain.Entities
{
    public class Specialization
    {
        public int Id { get; set; }
        
        [MaxLength(128)]
        public string NameEn { get; set; }
        
        [MaxLength(128)]
        public string NameAr { get; set; }
        public List<Doctor> Doctors { get; } = new();
    }
}
