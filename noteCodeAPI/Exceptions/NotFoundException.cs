namespace noteCodeAPI.Exceptions
{
    public class NotFoundException: Exception
    {
        private static readonly string defaultMessage = "Resource not found.";

        public NotFoundException(string message) : base(message)
        {
        }
        public NotFoundException() : base(defaultMessage)
        {
        }
    }
}
