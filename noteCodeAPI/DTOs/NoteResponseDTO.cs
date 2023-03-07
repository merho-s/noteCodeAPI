using noteCodeAPI.Models;

namespace noteCodeAPI.DTOs
{
    public class NoteResponseDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        //public string Image { get; set; }

        public List<CodeSnippetDTO> Codes { get; set; }

        public List<CodetagDTO> Codetags { get; set; }

        public NoteResponseDTO() 
        {
            Codetags = new();
            Codes = new();
        }
        
    }
}
