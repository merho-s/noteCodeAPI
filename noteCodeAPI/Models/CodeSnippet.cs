using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace noteCodeAPI.Models
{
    [Table("code_snippet")]
    public class CodeSnippet
    {
        private int id;
        private string? code;
        private string? description;
        private string? language;

        [Column("id")]
        public int Id { get => id; set => id = value; }

        [Column("code")]
        public string? Code { get => code; set => code = value ?? throw new ArgumentNullException(); }

        [Column("description")]
        public string? Description { get => description; set => description = value; }

        [Column("language")]
        public string? Language { get => language; set => language = value; }

        [JsonIgnore]
        [ForeignKey("NoteId")]
        public Note Note { get; set; }

        [Column("note_id")]
        public int NoteId { get; set; }
    }
}
