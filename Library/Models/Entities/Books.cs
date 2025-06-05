using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models.Entities
{
    public class Books
    {
        public  Guid Id { get; set; } 
        public required string Title { get; set; }
        public string? Author { get; set; }
        public string? Description { get; set; }
    }
}
