using noteCodeAPI.Models;

namespace noteCodeAPI.DTOs
{
    public class NoteResponseDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        //public string Image { get; set; }

        public ICollection<CodeSnippetDTO> Codes { get; set; }

        public ICollection<string> Codetags { get; set; }
        
    }
}
