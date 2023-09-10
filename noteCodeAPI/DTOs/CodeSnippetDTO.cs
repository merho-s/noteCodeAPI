namespace noteCodeAPI.DTOs
{
    public class CodeSnippetDTO
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
    }
}
