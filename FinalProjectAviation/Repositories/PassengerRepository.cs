using FinalProjectAviation.Data;
using FinalProjectAviation.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectAviation.Repositories
{
    public class PassengerRepository : BaseRepository<Passenger>, IPassengerRepository
    {

        public PassengerRepository( AviationDBDbContext context) : base(context) { }
        public async Task<List<User>> GetAllUsersPassengersAsync()
        {
            var usersWithPassengerRole = await _context.Users.Where(u => u.UserRole == UserRole.Passenger).Include(u => u.Passenger).ToListAsync();
            return usersWithPassengerRole;
        }

        public async Task<Passenger?> GetByPhoneNumber(string? phoneNumber)
        {
            return await _context.Passengers.Where(p => p.PhoneNumber == phoneNumber).FirstOrDefaultAsync();
        }

        public async Task<int> GetCountAsync()
        {
            int count = 0;
            List<User> allPassengers = await _context.Users.Where(u => u.UserRole == UserRole.Passenger).Include(u => u.Passenger).ToListAsync();
            foreach(var user in allPassengers)
            {
                count++;
            }
            return count;
        }

        public async Task<List<Flight>> GetPassengerFlightsAsync(int id)
        {
           List<Flight> flights;  
           flights =  await _context.Passengers.Where(p => p.Id == id).SelectMany(p => p.Flights).ToListAsync();    
           return flights;
        }
    }
}
