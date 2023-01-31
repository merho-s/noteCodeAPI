using noteCodeAPI.Models;

namespace noteCodeAPI.DTOs
{
    public class NoteRequestDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Code { get; set; }
        public List<Tag> Tags { get; set; }

        public NoteRequestDTO() 
        {
            Tags = new();
        }
    }
}
