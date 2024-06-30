using FinalProjectAviation.Data;

namespace FinalProjectAviation.Services
{
    public interface IPassengerService
    {
        Task<IEnumerable<User>> GetAllPassengersUsersAsync();
        Task<List<Flight>> GetPassengerFlightsAsync( int id);
        Task<Passenger?> GetPassengerAsync(int id);
        Task<bool> DeletePassengerAsync(int id);
        Task<int> GetPassengerCountAsync();
    }
}
