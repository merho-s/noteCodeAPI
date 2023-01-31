using System.ComponentModel.DataAnnotations.Schema;

namespace noteCodeAPI.Models
{
    [Table("note")]
    public class Note
    {
        private int id;
        private string? title;
        private string? description;
        private string? image;
        private string? code;
        private List<Tag>? tags;
        private UserApp user;

        [Column("id")]
        public int Id { get => id; set => id = value; }

        [Column("title")]
        public string? Title { get => title; set => title = value ?? throw new ArgumentNullException(nameof(value)); }

        [Column("description")]
        public string? Description { get => description; set => description = value ?? throw new ArgumentNullException(nameof(value)); }

        [Column("image")]
        public string? Image { get => image; set => image = value; }

        [Column("code")]
        public string? Code { get => code; set => code = value; }
        public List<Tag>? Tags { get => tags; set => tags = value; }
        public UserApp User { get => user; set => user = value; }

        [ForeignKey("User")]
        [Column("user_id")]
        public int UserId { get; set; }

        public Note()
        {
            Tags = new();
        }
    }
}
