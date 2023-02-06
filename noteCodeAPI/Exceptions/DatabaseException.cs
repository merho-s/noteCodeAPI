namespace noteCodeAPI.Exceptions
{
    public class DatabaseException: Exception
    {
        private static readonly string defaultMessage = "Database error.";
        public DatabaseException(string message) : base(message)
        {
        }
        public DatabaseException() : base(defaultMessage)
        {
        }
    }
}
