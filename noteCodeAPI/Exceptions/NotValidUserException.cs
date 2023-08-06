namespace noteCodeAPI.Exceptions
{
    public class NotValidUserException: Exception
    {
        private static readonly string defaultMessage = "User not valid.";

        public NotValidUserException(string message) : base(message)
        {
        }
        public NotValidUserException() : base(defaultMessage)
        {
        }
    }
}
