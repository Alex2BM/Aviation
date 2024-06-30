using AutoMapper;
using FinalProjectAviation.Repositories;

namespace FinalProjectAviation.Services
{
    public class ApplicationService : IApplicationService
    {
        protected readonly IUnitOfWork _unitOfWork; 
        private readonly IMapper _mapper;
        private readonly ILogger<UserService>? _logger;

        public ApplicationService(IUnitOfWork unitOfWork, ILogger<UserService>? logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public UserService UserService => new(_unitOfWork, _logger, _mapper);

        public PilotService PilotService => new(_unitOfWork, _logger, _mapper);

        public PassengerService passengerService => new(_unitOfWork, _logger, _mapper);
    }
}
