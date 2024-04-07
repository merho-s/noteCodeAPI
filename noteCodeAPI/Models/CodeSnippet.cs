using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace noteCodeAPI.Models
{
    [Table("code_snippets")]
    public class CodeSnippet
    {
        private Guid id;
        private string code;
        private string description;
        private string language;

        [Column("id")]
        public Guid Id { get => id; set => id = value; }

        [Column("code")]
        public string Code { get => code; set => code = value; }

        [Column("description")]
        public string? Description { get => description; set => description = value; }

        [Column("language")]
        public string Language { get => language; set => language = value; }

        [JsonIgnore]
        [ForeignKey("NoteId")]
        public Note Note { get; set; }

        [Column("note_id")]
        public Guid NoteId { get; set; }
    }
}
