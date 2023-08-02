using System.ComponentModel.DataAnnotations.Schema;

namespace noteCodeAPI.Models
{
    [Table("notes_codetags")]
    public class NoteTag
    {
        [Column("id")]
        public int Id { get; set; }
        
        [Column("note_id")]
        public int NoteId { get; set; }

        [Column("tag_id")]
        public int TagId { get; set; }

        [ForeignKey("NoteId")]
        public Note Note { get; set; }

        [ForeignKey("TagId")]
        public Codetag Tag { get; set; }
        
    }
}
