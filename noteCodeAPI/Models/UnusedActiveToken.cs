using System.ComponentModel.DataAnnotations.Schema;

namespace noteCodeAPI.Models
{
    [Table("unused_active_token")]
    public class UnusedActiveToken
    {
        private int id;
        private string token;

        [Column("id")]
        public int Id { get => id; set => id = value; }

        [Column("token")]
        public string Token { get => token; set => token = value; }
    }
}
