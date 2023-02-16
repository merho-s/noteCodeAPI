namespace noteCodeAPI.DTOs
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }

        public string Username { get; set; }

        public DateTime ExpirationDate { get; set; }
        //public string ExpirationDate { get; set; }
    }
}
