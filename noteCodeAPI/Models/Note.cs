using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace noteCodeAPI.Models
{
    [Table("notes")]
    public class Note
    {
        private Guid id;
        private string title;
        private string description;
        private DateTimeOffset creationDate;
        //private string? image;
        private ICollection<CodeSnippet>? codes;
        private ICollection<Codetag>? codetags;
        private UserApp? user;

        [Column("id")]
        public Guid Id { get => id; set => id = value; }

        [Column("title")]
        public string Title { get => title; set => title = value; }

        [Column("description")]
        public string Description { get => description; set => description = value; }

        [Column("creation_date")]
        public DateTimeOffset CreationDate { get => creationDate; set => creationDate = value; }

        //[Column("image")]
        //public string? Image { get => image; set => image = value; }

        public ICollection<Codetag>? Codetags { get => codetags; set => codetags = value; }

        public ICollection<CodeSnippet>? Codes { get => codes; set => codes = value; }

        [JsonIgnore]
        [ForeignKey("UserId")]
        public UserApp? User { get => user; set => user = value; }
       
        [Column("user_id")]
        public int? UserId { get; set; }

        public Note()
        {
            Codetags = new List<Codetag>();
            Codes = new List<CodeSnippet>();
        }
    }
}
