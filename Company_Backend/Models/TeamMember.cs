using System.ComponentModel.DataAnnotations;

namespace Company_Backend.Models
{
    public class TeamMember
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Role { get; set; } = string.Empty;  
        public string? Bio { get; set; }    
        public byte[]? Photo { get; set; }
        public string? FacebookUrl { get; set; }
        public string? InstagramUrl { get; set; }
        public string? LinkedInUrl { get; set; }
        public string? GitHubUrl { get; set; }
    }
}
