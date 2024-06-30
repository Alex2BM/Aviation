using FinalProjectAviation.Data;
using FinalProjectAviation.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectAviation.Repositories
{
    public class PilotRepository : BaseRepository<Pilot>, IPilotRepository
    {
        public PilotRepository( AviationDBDbContext context) : base(context) { }
        public async Task<List<User>> GetAllUsersPilotsAsync()
        {
           
           var pilotUsers = await _context.Users.Where(p => p.UserRole == Models.UserRole.Pilot).Include(p => p.Pilot).ToListAsync();
           return pilotUsers;

        }

        public async Task<List<User>> GetAllUsersPilotsAsync(int pageNumber, int pageSize)
        {
            int skip = pageSize * pageNumber;
            var usersWithPilotRole = await _context.Users
                   .Where(u => u.UserRole == UserRole.Pilot)
                   .Include(u => u.Pilot) 
                   .Skip(skip)
                   .Take(pageSize)
                   .ToListAsync();

            return usersWithPilotRole;
        }

        public async Task<Pilot?> GetByPhoneNumber(string? phoneNumber)
        {
            return await _context.Pilots.Where(p => p.PhoneNumber == phoneNumber).FirstOrDefaultAsync();
        }

        public async Task<User?> GetPilotByUsernameAync(string username)
        {
            var usePilot = await _context.Users.Where(u => u.Username == username && u.UserRole == UserRole.Pilot).SingleOrDefaultAsync();
            return usePilot;    
        }

        public async Task<List<Flight>> GetPilotFlightsAsync(int id)
        {
            List<Flight> pilotFlights = await _context.Pilots.Where(p => p.Id == id).SelectMany(p => p.Flights).ToListAsync();
            return (pilotFlights);
        }
    }
}
