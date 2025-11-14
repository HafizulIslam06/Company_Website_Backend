using Company_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Company_Backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<HeroSection> HeroSections { get; set; }
        public DbSet<AboutSection> AboutSections { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; } = null!;
    }
}
