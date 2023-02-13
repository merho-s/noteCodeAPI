namespace noteCodeAPI.DTOs
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public int UserId { get; set; }

        public DateTime ExpirationDate { get; set; }
        //public string ExpirationDate { get; set; }
    }
}
