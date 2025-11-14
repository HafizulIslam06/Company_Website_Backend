using Company_Backend.Data;
using Company_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroSectionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HeroSectionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/HeroSection
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HeroSection>>> GetHeroSections()
        {
            return await _context.HeroSections.ToListAsync();
        }

        // GET: api/HeroSection/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HeroSection>> GetHeroSection(int id)
        {
            var heroSection = await _context.HeroSections.FindAsync(id);

            if (heroSection == null)
                return NotFound();

            return heroSection;
        }

        // POST: api/HeroSection
        [HttpPost]
        public async Task<ActionResult<HeroSection>> PostHeroSection(HeroSection heroSection)
        {
            _context.HeroSections.Add(heroSection);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHeroSection), new { id = heroSection.Id }, heroSection);
        }

        // PUT: api/HeroSection/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHeroSection(int id, HeroSection heroSection)
        {
            if (id != heroSection.Id)
                return BadRequest();

            _context.Entry(heroSection).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.HeroSections.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/HeroSection/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHeroSection(int id)
        {
            var heroSection = await _context.HeroSections.FindAsync(id);
            if (heroSection == null)
                return NotFound();

            _context.HeroSections.Remove(heroSection);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
