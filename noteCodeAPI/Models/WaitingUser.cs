using System.ComponentModel.DataAnnotations.Schema;

namespace noteCodeAPI.Models
{
    [Table("waiting_users")]
    public class WaitingUser
    {
        private int id;
        private string? username;
        private string? passwordHashed;
        private string? passwordSalt;

        [Column("id")]
        public int Id { get => id; set => id = value; }

        [Column("username")]
        public string? Username { get => username; set => username = value; }

        [Column("password_hashed")]
        public string? PasswordHashed { get => passwordHashed; set => passwordHashed = value; }

        [Column("password_salt")]
        public string? PasswordSalt { get => passwordSalt; set => passwordSalt = value; }
    }
}
