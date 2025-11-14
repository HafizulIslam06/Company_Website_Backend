using System.ComponentModel.DataAnnotations;

namespace Company_Backend.Models
{
    public class HeroSection
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string ShortDescription { get; set; }

        [MaxLength(300)]
        public string ImageUrl { get; set; }
    }
}
