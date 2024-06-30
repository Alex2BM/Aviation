namespace FinalProjectAviation.Data
{
    public class Flight
    { public int Id { get; set; }
      public string? FromCity { get; set; }
      public string? ToCity { get; set; }
      public int TicketPrice { get; set; }

      public int PilotId { get; set; }    
      public virtual Pilot? Pilot { get; set; }
      public virtual ICollection<Passenger> Passengers { get; } = new HashSet<Passenger>();    
     }
}
