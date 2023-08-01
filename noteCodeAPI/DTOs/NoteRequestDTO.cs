using noteCodeAPI.Models;

namespace noteCodeAPI.DTOs
{
    public class NoteRequestDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public ICollection<CodeSnippetDTO> Codes { get; set; }

        //public IFormFile Image { get; set; }

        public ICollection<string> Codetags { get; set; }

    }
}
