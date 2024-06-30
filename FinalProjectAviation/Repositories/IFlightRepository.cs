using FinalProjectAviation.Data;
using FinalProjectAviation.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectAviation.Repositories
{
    public interface IFlightRepository 
    {
        Task<List<Passenger>> GetFlightPassengersAsync(int id);
        Task<Pilot?> GetFlightPilotAsync(int id);
    }
}
