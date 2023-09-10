using noteCodeAPI.Models;

namespace noteCodeAPI.DTOs
{
    public class NoteRequestDTO
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ICollection<CodeSnippetDTO> Codes { get; set; }

        //public IFormFile Image { get; set; }

        public ICollection<CodetagDTO> Codetags { get; set; }

    }
}
