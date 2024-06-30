namespace FinalProjectAviation.Services.Exceptions
{
    public class PilotAlreadyExistsException : Exception
    {
        public PilotAlreadyExistsException(string? s) : base(s) { }
    }
}
