namespace noteCodeAPI.Exceptions
{
    public class DatabaseException: Exception
    {
        private static readonly string defaultMessage = "Database error.";
        public DatabaseException() : base(defaultMessage)
        {
        }
    }
}
