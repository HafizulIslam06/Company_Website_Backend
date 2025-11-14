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

        // GET: api/BlogPosts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogPost>>> GetAll()
        {
            var blogs = await _context.BlogPosts
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
            return Ok(blogs);
        }

        // GET: api/BlogPosts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPost>> GetById(int id)
        {
            var blog = await _context.BlogPosts.FindAsync(id);
            if (blog == null) return NotFound();
            return Ok(blog);
        }

        // POST: api/BlogPosts
        [HttpPost]
        public async Task<ActionResult<BlogPost>> Create(BlogPost blog)
        {
            blog.CreatedAt = DateTime.UtcNow;

            _context.BlogPosts.Add(blog);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = blog.Id }, blog);
        }

        // PUT: api/BlogPosts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BlogPost updatedBlog)
        {
            var blog = await _context.BlogPosts.FindAsync(id);
            if (blog == null) return NotFound();

            blog.Title = updatedBlog.Title;
            blog.ShortDescription = updatedBlog.ShortDescription;
            blog.LongDescription = updatedBlog.LongDescription;
            blog.ImageUrl = updatedBlog.ImageUrl;
            blog.VideoUrl = updatedBlog.VideoUrl;

            _context.Entry(blog).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/BlogPosts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var blog = await _context.BlogPosts.FindAsync(id);
            if (blog == null) return NotFound();

            _context.BlogPosts.Remove(blog);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
