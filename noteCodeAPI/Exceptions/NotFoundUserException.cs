namespace noteCodeAPI.Exceptions
{
    public class NotFoundUserException: Exception
    {
        private static readonly string defaultMessage = "User not found.";

        public NotFoundUserException(string message) : base(message)
        {
        }
        public NotFoundUserException() : base(defaultMessage)
        {
        }
    }
}
