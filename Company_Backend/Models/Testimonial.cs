namespace Company_Backend.Models
{
    public class Testimonial
    {
        public int Id { get; set; }
        public string ClientName { get; set; } = null!;
        public string? CompanyName { get; set; }
        public string Quote { get; set; } = null!;
        public string? LogoUrl { get; set; }
        public bool IsPublished { get; set; } = true;
    }
}
