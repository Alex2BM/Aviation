using AutoMapper;
using FinalProjectAviation.Data;
using FinalProjectAviation.Repositories;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FinalProjectAviation.Services
{
    public class PilotService : IPilotService
    {

        private readonly IUnitOfWork? _unitOfWork;
        private readonly IMapper? _mapper;
        private readonly ILogger<UserService>? _logger;

        public PilotService(IUnitOfWork? unitOfWork, ILogger<UserService>? logger, IMapper? mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;

        }

        public async Task<List<User>> GetAllUsersPilotsAsync()
        {
            List<User> users = new List<User>();

            try
            {
                users = await _unitOfWork!.PilotRepository.GetAllUsersPilotsAsync();
                _logger!.LogInformation("{Message}", "All Pilots that are users returned succesfully!");

            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
                throw;
            }
            return users;
        }

        public async Task<List<User>> GetAllUsersPilotsAsync(int pageNumber, int pageSize)
        {
            List<User> users = new List<User>();
            int skip = 1;
            try
            {
                skip = pageNumber * pageSize;
                users = await _unitOfWork!.PilotRepository.GetAllUsersPilotsAsync(pageNumber, pageSize);
                _logger!.LogInformation("{Message}", "All Pilots paginated and returned succesfully!");

            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
                throw;
            }
            return users;
        }

        public async Task<int> GetPilotCountAsync()
        {
            int count = 0;
            List<User> totalPilots = new List<User>();
            try
            {
                totalPilots = await _unitOfWork!.PilotRepository.GetAllUsersPilotsAsync();
                if (totalPilots is null) return count;
                foreach (User pilot in totalPilots)
                {
                    count++;
                }
                _logger!.LogInformation("{Message}", "Pilots have been counted and returned successfully!");
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
                throw;

            }
            return count;

        }

        public async Task<User?> GetUserByUsernameAsync(string? username)
        {
            return await _unitOfWork!.UserRepository.GetByUsernameAsync(username!);


        }
    }
}
