using AutoMapper;
using FinalProjectAviation.Data;
using FinalProjectAviation.Repositories;
using FinalProjectAviation.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectAviation.Services
{
    public class PassengerService : IPassengerService
    {
        private readonly IUnitOfWork? _unitOfWork;
        private readonly IMapper? _mapper;
        private readonly ILogger<UserService>? _logger;

        public PassengerService(IUnitOfWork? unitOfWork, ILogger<UserService>? logger, IMapper? mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
    
        public async Task<bool> DeletePassengerAsync(int id)
        {
            bool studentDeleted;  
            try {
                studentDeleted = await _unitOfWork!.PassengerRepository.DeleteAsync(id);
                _logger!.LogInformation("{Message}", "Passenger was successfully deleted!");
            }
            catch (Exception e) { 
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
                throw;
            }
            return studentDeleted;

        }

        public async Task<IEnumerable<User>> GetAllPassengersUsersAsync()
        {
            List<User> PassengersUsers = new();
            try {
                PassengersUsers = await _unitOfWork.PassengerRepository.GetAllUsersPassengersAsync();
                _logger!.LogInformation("{Message}", "All passenger that are also users have returned successfully!");
            }
            catch (Exception e) {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
                throw;
            }
            return PassengersUsers;
        }

        public async Task<Passenger?> GetPassengerAsync(int id)
        {
            Passenger? passenger = null;
            try
            {
                passenger = await _unitOfWork!.PassengerRepository.GetAsync(id);
                _logger!.LogInformation("{Message}", "Passenger with id: " + id + " has been found and returned successfully!");
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
                throw;
            }
            return passenger;
        }

        public async Task<int> GetPassengerCountAsync()
        {
            int count = 0;
            try
            {
                count = await _unitOfWork!.PassengerRepository.GetCountAsync();
                _logger!.LogInformation("{Message}", "Student count retrieved with success");
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
            }
            return count;
        }

        public async Task<List<Flight>> GetPassengerFlightsAsync(int id)
        {
            List<Flight> flights = new();
            try 
            {
                flights = await _unitOfWork!.PassengerRepository.GetPassengerFlightsAsync(id);
                _logger!.LogInformation("{Message}", "The flights of the Passenger with id: " + id + "have been found and returned successfully!");
            }
            catch(Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
                throw;
            }
            return flights;
        }
    }
}
