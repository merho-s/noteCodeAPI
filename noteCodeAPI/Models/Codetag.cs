using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace noteCodeAPI.Models
{
    [Table("codetags")]
    public class Codetag
    {
        private int id;
        private string name;
        private ICollection<Note>? notes;

        [Column("id")]
        public int Id { get => id; set => id = value; }

        [Column("name")]
        public string Name { get => name; set => name = value; }

        [JsonIgnore]
        public ICollection<Note>? Notes { get => notes; set => notes = value; }

        public Codetag()
        {
            Notes = new List<Note>();
        }

    }
}
