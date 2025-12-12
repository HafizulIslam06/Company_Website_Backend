using System.ComponentModel.DataAnnotations;

namespace Company_Backend.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string ShortDescription { get; set; } = null!;
        public string LongDescription { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }

        public string? Category { get; set; }
        public string? Tags { get; set; }  // comma-separated "dotnet,blazor,webdev"

        public string? AuthorName { get; set; }
        public string? AuthorImageUrl { get; set; }

        public int ReadTimeMinutes { get; set; }

        public bool IsFeatured { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
