using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace noteCodeAPI.Models
{
    [Table("codetag_alias")]
    public class TagAlias
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [JsonIgnore]
        [ForeignKey("CodetagId")]
        public Codetag Codetag { get; set; }

        [Column("codetag_id")]
        public int CodetagId { get; set; }

        public List<CodeSnippet> Codes { get; set; }

        public TagAlias()
        {
            Codes = new();
        }
    }
}
