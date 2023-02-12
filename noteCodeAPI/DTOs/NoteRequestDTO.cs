using noteCodeAPI.Models;

namespace noteCodeAPI.DTOs
{
    public class NoteRequestDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }

        //public IFormFile Image { get; set; }

        public List<CodetagDTO> Codetags { get; set; }

        public NoteRequestDTO() 
        {
            Codetags = new();
        }
    }
}
