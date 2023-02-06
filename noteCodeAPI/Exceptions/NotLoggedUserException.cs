namespace noteCodeAPI.Exceptions
{
    public class NotLoggedUserException: Exception
    {
        private static readonly string defaultMessage = "User is not logged.";

        public NotLoggedUserException(string message) : base(message)
        {
        }
        public NotLoggedUserException() : base(defaultMessage)
        {
        }
    }
}
