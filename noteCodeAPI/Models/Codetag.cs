using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace noteCodeAPI.Models
{
    [Table("codetags")]
    public class Codetag
    {
        private int id;
        private string? name;
        private ICollection<Note>? notes;
        private ICollection<TagAlias>? aliases;

        [Column("id")]
        public int Id { get => id; set => id = value; }

        [Column("name")]
        public string? Name { get => name; set => name = value ?? throw new ArgumentNullException(nameof(value)); }

        public ICollection<TagAlias>? Aliases { get => aliases; set => aliases = value; }

        public ICollection<Note>? Notes { get => notes; set => notes = value; }

        public Codetag()
        {
            Aliases = new List<TagAlias>();
            Notes = new List<Note>();
        }

    }
}
