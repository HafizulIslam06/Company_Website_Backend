using Company_Backend.Data;
using Company_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TestimonialsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Testimonials
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Testimonial>>> GetTestimonials()
        {
            return await _context.Testimonials
                                 .Where(t => t.IsPublished)
                                 .OrderByDescending(t => t.Id)
                                 .ToListAsync();
        }

        // GET: api/Testimonials/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Testimonial>> GetTestimonial(int id)
        {
            var testimonial = await _context.Testimonials.FindAsync(id);
            if (testimonial == null)
                return NotFound();

            return testimonial;
        }

        // POST: api/Testimonials
        [HttpPost]
        public async Task<ActionResult<Testimonial>> CreateTestimonial(Testimonial testimonial)
        {
            _context.Testimonials.Add(testimonial);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTestimonial), new { id = testimonial.Id }, testimonial);
        }

        // PUT: api/Testimonials/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTestimonial(int id, Testimonial testimonial)
        {
            if (id != testimonial.Id)
                return BadRequest();

            _context.Entry(testimonial).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Testimonials.Any(e => e.Id == id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Testimonials/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTestimonial(int id)
        {
            var testimonial = await _context.Testimonials.FindAsync(id);
            if (testimonial == null)
                return NotFound();

            _context.Testimonials.Remove(testimonial);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
