using System.ComponentModel.DataAnnotations.Schema;

namespace noteCodeAPI.Models
{
    [Table("users")]
    public class UserApp
    {
        private int id;
        private string? username;
        private string? password;

        [Column("id")]
        public int Id { get => id; set => id = value; }

        [Column("username")]
        public string? Username { get => username; set => username = value ?? throw new ArgumentNullException(nameof(value)); }

        [Column("password")]
        public string? Password { get => password; set => password = value ?? throw new ArgumentNullException(nameof(value)); }
    }
}
