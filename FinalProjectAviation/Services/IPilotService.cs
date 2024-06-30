using FinalProjectAviation.Data;

namespace FinalProjectAviation.Services
{
    public interface IPilotService
    {
        Task<List<User>> GetAllUsersPilotsAsync();
        Task<List<User>> GetAllUsersPilotsAsync(int pageNumber, int pageSize);
        Task<int> GetPilotCountAsync();
        Task<User?>GetUserByUsernameAsync(string? username);
    }
}
