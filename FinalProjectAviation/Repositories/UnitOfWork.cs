
using FinalProjectAviation.Data;

namespace FinalProjectAviation.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AviationDBDbContext _context;
        public UnitOfWork(AviationDBDbContext context)
        {
            _context = context;
        }

        public UserRepository UserRepository => new (_context);

        public PassengerRepository PassengerRepository => new(_context);

        public PilotRepository PilotRepository => new(_context);

        public FlightRepository FlightRepository => new(_context);

        public Task<bool> SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
