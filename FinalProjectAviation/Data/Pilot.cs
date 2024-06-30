namespace FinalProjectAviation.Data
{
    public class Pilot
    {
        public int Id { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Ethnicity { get; set; }

        public int UserId { get; set; }
        public virtual User? User { get; set; }

        public virtual ICollection<Flight> Flights { get; } = new HashSet<Flight>();

    }
}
