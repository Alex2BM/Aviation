namespace FinalProjectAviation.Services
{
    public interface IApplicationService
    {
        UserService UserService { get; }
        PilotService PilotService { get; }
        PassengerService passengerService { get; }
    }
}
