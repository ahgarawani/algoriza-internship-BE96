namespace Vezeeta.Domain.Entities
{
    public class Doctor
    {

        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public Specialization Specialization { get; set; }
        public int SpecializationId { get; set; }
        public float VisitPrice { get; set; }
        public List<AppointmentsDay> AppointmentsDays { get; set; }

    }
}
