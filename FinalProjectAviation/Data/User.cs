using FinalProjectAviation.Models;

namespace FinalProjectAviation.Data
{
    public class User
    {
        public int Id {  get; set; }
        public string? Username { get; set; }
        public string? Password { get; set;}
        public string? Email { get; set; }
        public string? Firstname { get; set; } 
        public string? Lastname { get; set; }   

        public UserRole? UserRole { get; set; }

        public virtual Pilot? Pilot { get; set; }
        public virtual Passenger? Passenger { get; set; }   


    }
}
