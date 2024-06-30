namespace FinalProjectAviation.Repositories
{
    public interface IUnitOfWork
    {
        public UserRepository UserRepository { get; }
        public PassengerRepository PassengerRepository { get; }
        public PilotRepository PilotRepository { get; }
        public FlightRepository FlightRepository { get; }

        Task<bool> SaveAsync();
    }
}
