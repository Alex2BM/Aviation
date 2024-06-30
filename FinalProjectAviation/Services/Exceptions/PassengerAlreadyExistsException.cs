namespace FinalProjectAviation.Services.Exceptions
{
    public class PassengerAlreadyExistsException : Exception
    {
        public PassengerAlreadyExistsException( string? s) : base(s) { }
    }
}
