using Company_Backend.Data;
using Company_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutSectionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AboutSectionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AboutSection
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AboutSection>>> GetAboutSections()
        {
            return await _context.AboutSections.ToListAsync();
        }

        // GET: api/AboutSection/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AboutSection>> GetAboutSection(int id)
        {
            var about = await _context.AboutSections.FindAsync(id);
            if (about == null) return NotFound();
            return about;
        }

        // POST: api/AboutSection
        [HttpPost]
        public async Task<ActionResult<AboutSection>> CreateAboutSection(AboutSection about)
        {
            _context.AboutSections.Add(about);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAboutSection), new { id = about.Id }, about);
        }

        // PUT: api/AboutSection/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAboutSection(int id, AboutSection about)
        {
            if (id != about.Id) return BadRequest();

            _context.Entry(about).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.AboutSections.Any(e => e.Id == id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // DELETE: api/AboutSection/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAboutSection(int id)
        {
            var about = await _context.AboutSections.FindAsync(id);
            if (about == null) return NotFound();

            _context.AboutSections.Remove(about);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
