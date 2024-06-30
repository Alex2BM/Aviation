using FinalProjectAviation.Data;

namespace FinalProjectAviation.Repositories
{
    public interface IPilotRepository
    {
        Task<List<Flight>> GetPilotFlightsAsync(int id);
        Task<Pilot?> GetByPhoneNumber(string? phoneNumber);
        Task<List<User>> GetAllUsersPilotsAsync();
        Task<List<User>> GetAllUsersPilotsAsync(int pageNumber, int pageSize);
        Task<User?> GetPilotByUsernameAync(string username);

    }
}
