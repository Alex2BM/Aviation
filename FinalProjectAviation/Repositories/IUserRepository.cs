using FinalProjectAviation.Data;

namespace FinalProjectAviation.Repositories
{
    public interface IUserRepository
    {

        Task<User?> GetUserAsync(string username, string password);
        Task<User?> UpdateUserAsync(int userIdUser, User request);
        Task<User?> GetByUsernameAsync(string username);
        Task<List<User>> GetAllUsersFilteredAsync(int pageNumber, int pageSize,
            List<Func<User, bool>> predicate);
    }
}
