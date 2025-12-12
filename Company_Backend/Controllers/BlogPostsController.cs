using Company_Backend.Data;
using Company_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Company_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BlogPostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/blogposts
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _context.BlogPosts
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            return Ok(posts);
        }

        // GET: api/blogposts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var post = await _context.BlogPosts.FindAsync(id);
            if (post == null) return NotFound();

            return Ok(post);
        }

        // GET: api/blogposts/slug/my-blog-title
        [HttpGet("slug/{slug}")]
        public async Task<IActionResult> GetBySlug(string slug)
        {
            var post = await _context.BlogPosts
                .FirstOrDefaultAsync(x => x.Slug == slug);

            if (post == null) return NotFound();

            return Ok(post);
        }

        // GET: api/blogposts/category/Technology
        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetByCategory(string category)
        {
            var posts = await _context.BlogPosts
                .Where(x => x.Category == category)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            return Ok(posts);
        }

        // GET: api/blogposts/featured
        [HttpGet("featured")]
        public async Task<IActionResult> GetFeatured()
        {
            var posts = await _context.BlogPosts
                .Where(x => x.IsFeatured)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            return Ok(posts);
        }

        // GET: api/blogposts/search?query=blazor
        [HttpGet("search")]
        public async Task<IActionResult> Search(string query)
        {
            var posts = await _context.BlogPosts
                .Where(x =>
                    x.Title.Contains(query) ||
                    x.ShortDescription.Contains(query) ||
                    x.LongDescription.Contains(query)
                )
                .ToListAsync();

            return Ok(posts);
        }

        // POST: api/blogposts
        [HttpPost]
        public async Task<IActionResult> Create(BlogPost post)
        {
            post.Slug = post.Title.ToLower().Replace(" ", "-");
            post.CreatedAt = DateTime.UtcNow;

            _context.BlogPosts.Add(post);
            await _context.SaveChangesAsync();

            return Ok(post);
        }

        // PUT: api/blogposts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BlogPost updated)
        {
            var post = await _context.BlogPosts.FindAsync(id);
            if (post == null) return NotFound();

            post.Title = updated.Title;
            post.ShortDescription = updated.ShortDescription;
            post.LongDescription = updated.LongDescription;
            post.ImageUrl = updated.ImageUrl;
            post.VideoUrl = updated.VideoUrl;
            post.Category = updated.Category;
            post.Tags = updated.Tags;
            post.AuthorName = updated.AuthorName;
            post.AuthorImageUrl = updated.AuthorImageUrl;
            post.ReadTimeMinutes = updated.ReadTimeMinutes;
            post.IsFeatured = updated.IsFeatured;

            post.UpdatedAt = DateTime.UtcNow;

            // regenerate slug
            post.Slug = updated.Title.ToLower().Replace(" ", "-");

            await _context.SaveChangesAsync();

            return Ok(post);
        }

        // DELETE: api/blogposts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _context.BlogPosts.FindAsync(id);
            if (post == null) return NotFound();

            _context.BlogPosts.Remove(post);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Blog post deleted successfully" });
        }
    }
}
