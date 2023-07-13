namespace noteCodeAPI.Exceptions
{
    public class SameUsernameException : Exception
    {
        private static readonly string defaultMessage = "Username already exists.";

        public SameUsernameException(string message) : base(message)
        {
        }
        public SameUsernameException() : base(defaultMessage)
        {
        }
    }
}
