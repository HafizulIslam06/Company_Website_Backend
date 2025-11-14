using System.ComponentModel.DataAnnotations;

namespace Company_Backend.Models
{
    public class AboutSection
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string? ImageUrl { get; set; }

        public string? VideoUrl { get; set; }
    }
}
