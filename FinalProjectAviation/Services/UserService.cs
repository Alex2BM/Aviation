using AutoMapper;
using FinalProjectAviation.Data;
using FinalProjectAviation.DTO;
using FinalProjectAviation.Models;
using FinalProjectAviation.Repositories;
using FinalProjectAviation.Security;
using FinalProjectAviation.Services.Exceptions;

namespace FinalProjectAviation.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork? _unitOfWork;
        private readonly ILogger<UserService>? _logger;
        private readonly IMapper? _mapper;

        public UserService(IUnitOfWork? unitOfWork, ILogger<UserService>? logger, IMapper? mapper) {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<User>> GetAllUsersFiltered(int pageNumber, int pageSize, UserFiltersDTO userFiltersDTO)
        {
            List<User> users = new();
            List<Func<User, bool>> predicates = new();

            try {
                if(!string.IsNullOrEmpty(userFiltersDTO.Username)) {predicates.Add(u => u.Username == userFiltersDTO.Username); }
                if (!string.IsNullOrEmpty(userFiltersDTO.Email)) { predicates.Add(u => u.Email == userFiltersDTO.Email); }
                if (!string.IsNullOrEmpty(userFiltersDTO.UserRole)) { predicates.Add(u => u.UserRole!.Value.ToString() == userFiltersDTO.UserRole); }
                users = await _unitOfWork!.UserRepository.GetAllUsersFilteredAsync(pageNumber, pageSize, predicates);
            }
            catch (Exception e) {
                _logger!.LogInformation("{Message}{Exception}", e.Message, e.StackTrace);
                throw;
            }
            return users;
           
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            User? user;

            try {
                user = await _unitOfWork!.UserRepository.GetByUsernameAsync(username);
                _logger!.LogInformation("{Message}", "User: " + user + "has been found and returned successfully!");

            }
            catch (Exception e) {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
                throw;
            }

            return user;
        }

        public async Task SignUpUserAsync(UserSignupDTO request)
        {
            Pilot? pilot;
            Passenger? passenger;
            User? user;

            try
            {
                user = ExtractUser(request);
                User? existingUser = await _unitOfWork!.UserRepository.GetByUsernameAsync(user.Username!);

                if(existingUser != null) {
                    throw new UserAlreadyExistsException("UserExists: " + existingUser.Username);
                }

                user.Password = EncryptionUtil.Encrypt(user.Password!);
                await _unitOfWork!.UserRepository.AddAsync(user);

                if (user.UserRole == UserRole.Passenger)
                {
                    passenger = ExtractPassenger(request);
                    if (await _unitOfWork!.PassengerRepository.GetByPhoneNumber(passenger.PhoneNumber) is not null)
                    {
                        throw new PassengerAlreadyExistsException("PassengerExists: " + passenger.PhoneNumber);
                    }
                    await _unitOfWork!.PassengerRepository.AddAsync(passenger);
                    user.Passenger = passenger;

                }
                else if (user.UserRole == UserRole.Pilot)
                {
                    pilot = ExtractPilot(request);
                    if (await _unitOfWork!.PilotRepository.GetByPhoneNumber(pilot.PhoneNumber) is not null)
                    {
                        throw new PilotAlreadyExistsException("PilotExists: " + pilot.PhoneNumber);
                    }
                    await _unitOfWork!.PilotRepository.AddAsync(pilot);
                    user.Pilot = pilot;
                }
                else if (user.UserRole == UserRole.Admin)
                {
                    //admin = ExtractAdmin(signupDTO);
                    // if (await _unitOfWork!.AdminRepository.GetByPhoneNumber(admin.PhoneNumber) is not null)
                    // {
                    //    throw new AdminAlreadyExistsException("AdminPhoneNumberExists");
                    // }
                    // await _unitOfWork!.AdminRepository.AddAsync(admin);
                    // user.Admin = admin;
                    // admin.User = user;   // EF manages the other end since both entities are attached 
                }
                else
                {
                    throw new InvalidRoleException("InvalidRole");
                }
                await _unitOfWork.SaveAsync();
                _logger!.LogInformation("{Message}", "User: " + user + "signup successfull");
            }
            catch (Exception e) {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
                throw;
            }
        }
          

        public async Task<User?> UpdateUserAsync(int userId, UserDTO userDTO)
        {
            User? existingUser;
            User? user;

            try 
            {
                existingUser = await _unitOfWork!.UserRepository.GetAsync(userId);
                if (existingUser == null) return null;

                var userToUpdate = _mapper!.Map<User>(userDTO);

                user = await _unitOfWork.UserRepository.UpdateUserAsync(userId,userToUpdate);
                await _unitOfWork!.SaveAsync();
                _logger!.LogInformation("{Message}", "User: " + user + "has been updated successfully!");
                
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
                throw;
            }
            return user;
        }

        public async Task<User?> UpdateUserPatchAsync(int userId, UserPatchDTO request)
        {
            User? user; 
            User? existingUser;

            try {
                existingUser = await _unitOfWork!.UserRepository.GetAsync(userId);
                if(existingUser == null) return null;


                existingUser.Username = request.Username;
                existingUser.Email = request.Email;
                existingUser.Password = EncryptionUtil.Encrypt(request.Password!);
                user = existingUser;
                await _unitOfWork!.SaveAsync();
                _logger!.LogInformation("{Message}", "User: has been updated successfully!");
            }
            catch (Exception e) { 
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace); 
                throw; }
            return user;

        }

        public async Task<User?> VerifyAndGetUserAsync(UserLoginDTO credentials)
        {
            User? user = null;

            try {
                await _unitOfWork!.UserRepository.GetUserAsync(credentials.Username!, credentials.Password!);
                _logger!.LogInformation("{Message}", "User: " + user + "found and returned");
            }catch (Exception e) {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
                throw;
            }
            return user;
        }

        private User ExtractUser(UserSignupDTO userSignupDTO)
        {
            return new User()
            {
                Username = userSignupDTO.Username,
                Password = userSignupDTO.Password,
                Email = userSignupDTO.Email,
                Firstname = userSignupDTO.Firstname,
                Lastname = userSignupDTO.Lastname,
                UserRole = userSignupDTO.UserRole

            };

        }

        private Pilot ExtractPilot (UserSignupDTO userSignupDTO)
        {
            return new Pilot()
            {
                PhoneNumber = userSignupDTO.PhoneNumber,
                Ethnicity = userSignupDTO.Ethnicity
            };
        }

        private Passenger ExtractPassenger(UserSignupDTO userSignupDTO)
        {
            return new Passenger()
            {
                PhoneNumber = userSignupDTO.PhoneNumber,
                Ethnicity = userSignupDTO.Ethnicity
            };
        }

    }
}
