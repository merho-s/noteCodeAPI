using noteCodeAPI.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace noteCodeAPI.Models
{
    [Table("users")]
    public class UserApp
    {
        private int id;
        private string? username;
        private string? passwordHashed;
        private string? passwordSalt;
        private string? email;
        private bool isValid;
        private Role role;
        private ICollection<Note> notes;

        [Column("id")]
        public int Id { get => id; set => id = value; }

        [Column("username")]
        public string? Username { get => username; set => username = value; }

        [Column("password_hashed")]
        public string? PasswordHashed { get => passwordHashed; set => passwordHashed = value; }

        [Column("password_salt")]
        public string? PasswordSalt { get => passwordSalt; set => passwordSalt = value; }

        [Column("email")]
        public string? Email { get => email; set => email = value; }

        [Column("is_valid")]
        public bool IsValid { get => isValid; set => isValid = value; }

        [Column("role")]
        public Role Role { get => role; set => role = value; }

        public ICollection<Note> Notes { get => notes; set => notes = value; }

        public UserApp() 
        {
            Notes = new List<Note>();
        }
    }
}
