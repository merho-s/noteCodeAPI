using System.ComponentModel.DataAnnotations.Schema;

namespace noteCodeAPI.Models
{
    [Table("notes_tags")]
    public class NotesTags
    {
        [ForeignKey("Note")]
        [Column("note_id")]
        public int NoteId { get; set; }

        [ForeignKey("Tag")]
        [Column("tag_id")]
        public int TagId { get; set; }

        public Note Note { get; set; }

        public Codetag Tag { get; set; }
        
    }
}
