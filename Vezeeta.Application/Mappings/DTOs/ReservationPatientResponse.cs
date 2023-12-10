namespace Vezeeta.Application.Mappings.DTOs
{
    public class ReservationPatientResponse
    {
        public string Image { get; set; }
        public string DoctorName { get; set; }
        public string SpecializationEn { get; set; }
        public string SpecializationAr { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
        public float Price { get; set; }
        public string DiscountCode { get; set; }
        public float FinalPrice { get; set; }
        public string Status { get; set; }
    }
}
