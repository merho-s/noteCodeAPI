using noteCodeAPI.Models.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace noteCodeAPI.Models
{
    [Table("unused_active_tokens")]
    public class UnusedActiveToken: IToken
    {
        private int id;
        private string jwtToken;
        private DateTime expirationDate;

        [Column("id")]
        public int Id { get => id; set => id = value; }

        [Column("token")]
        public string JwtToken { get => jwtToken; set => jwtToken = value; }

        [Column("expiration_date")]
        public DateTime ExpirationDate { get => expirationDate; set => expirationDate = value; }

        public UserApp User => throw new NotImplementedException();
    }
}
