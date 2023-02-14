using noteCodeAPI.Models.Interfaces;

namespace noteCodeAPI.Models
{
    public class Token : IToken
    {
        private UserApp user;
        private DateTime expirationDate;
        private string jwtToken;

        public UserApp User { get => user; set => user = value; }
        public DateTime ExpirationDate { get => expirationDate; set => expirationDate = value; }
        public string JwtToken { get => jwtToken; set => jwtToken = value; }
    }
}
