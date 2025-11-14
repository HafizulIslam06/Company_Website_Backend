using System.ComponentModel.DataAnnotations;

namespace Company_Backend.Models
{
    public class BlogPost
    {
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Title { get; set; } = null!;

        [Required, MaxLength(300)]
        public string ShortDescription { get; set; } = null!;

        [Required]
        public string LongDescription { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public string? VideoUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
