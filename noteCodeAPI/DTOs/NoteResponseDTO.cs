using noteCodeAPI.Models;

namespace noteCodeAPI.DTOs
{
    public class NoteResponseDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public string Code { get; set; }

        public List<CodetagDTO> Codetags { get; set; }

        public NoteResponseDTO() 
        {
            Codetags = new();
        }
        
    }
}
