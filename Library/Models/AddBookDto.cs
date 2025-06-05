namespace Library.Models
{
    public class AddBookDto
    {
        public required string Title { get; set; }
        public string? Author { get; set; }
        public string? Description { get; set; }
    }
}
