using FinalProjectAviation.Data;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectAviation.Repositories
{
    public class FlightRepository : BaseRepository<Flight>, IFlightRepository
    {
        public FlightRepository(AviationDBDbContext context) : base(context) { }
        public async Task<List<Passenger>> GetFlightPassengersAsync(int id)
        {
           List<Passenger> passengersInFlight = await _context.Flights.Where(f => f.Id == id).SelectMany(f => f.Passengers!).ToListAsync(); 
           return passengersInFlight;
        }

        public async Task<Pilot?> GetFlightPilotAsync(int id)
        {
            var flight = await _context.Flights.Where(f => f.Id == id).FirstOrDefaultAsync();
            if(flight == null) { return null;}
            if(flight.Pilot == null) { return null;}
            return flight.Pilot;
        }
    }
}
