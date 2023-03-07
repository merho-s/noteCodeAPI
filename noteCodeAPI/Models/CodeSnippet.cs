using System.ComponentModel.DataAnnotations.Schema;

namespace noteCodeAPI.Models
{
    [Table("code_snippet")]
    public class CodeSnippet
    {
        private int id;
        private string code;
        private string description;

        [Column("id")]
        public int Id { get => id; set => id = value; }

        [Column("code")]
        public string Code { get => code; set => code = value ?? throw new ArgumentNullException(); }

        [Column("description")]
        public string Description { get => description; set => description = value; }
    }
}
