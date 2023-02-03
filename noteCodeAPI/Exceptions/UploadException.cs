namespace noteCodeAPI.Exceptions
{
    public class UploadException: Exception
    {
        private static readonly string defaultMessage = "Image uploading error.";
        public UploadException() : base(defaultMessage)
        {
        }
    }
}
