using FinalProjectAviation.Data;

namespace FinalProjectAviation.Repositories
{
    public interface IPassengerRepository
    {
        Task<List<Flight>> GetPassengerFlightsAsync(int id);
        Task<Passenger?> GetByPhoneNumber(string? phoneNumber);
        Task<List<User>> GetAllUsersPassengersAsync();
        Task<int> GetCountAsync();  
    }
}
