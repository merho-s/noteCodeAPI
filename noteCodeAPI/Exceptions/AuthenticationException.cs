namespace noteCodeAPI.Exceptions
{
    public class AuthenticationException: Exception
    {
        private static readonly string defaultMessage = "Your username or password is wrong.";
        public AuthenticationException(string message) : base(message)
        {
        }
        public AuthenticationException() : base(defaultMessage)
        {
        }
    }
}
